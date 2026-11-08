#!/usr/bin/env python3
"""Byte-level comparison of an original CoolStar driver package against a patched one.

    python tools/compare-drivers.py <original-dir> <patched-dir> [--sys NAME]

Both directories must contain the same `<name>.inf/.sys/.cat` set. The tool reports

  * file inventory and hashes
  * PE layout: where the certificate table sits and how big it is
  * every differing byte range, collapsing the signature blob into one line
  * a disassembly of each site inside a code section, so the patch is readable

Requires `capstone` (pip install capstone) for the disassembly part; everything
else runs on the standard library.
"""
import hashlib
import os
import struct
import sys

try:
    from capstone import Cs, CS_ARCH_X86, CS_MODE_64
    HAVE_CS = True
except ImportError:
    HAVE_CS = False


def sha1(b):
    return hashlib.sha1(b).hexdigest()


def pe_info(b):
    if b[:2] != b'MZ':
        return None
    pe = struct.unpack_from('<I', b, 0x3C)[0]
    if b[pe:pe + 4] != b'PE\0\0':
        return None
    nsec = struct.unpack_from('<H', b, pe + 6)[0]
    optsz = struct.unpack_from('<H', b, pe + 20)[0]
    magic = struct.unpack_from('<H', b, pe + 24)[0]
    dd = pe + 24 + (112 if magic == 0x20B else 96)
    cOff, cSize = struct.unpack_from('<II', b, dd + 8 * 4)
    csum = struct.unpack_from('<I', b, pe + 24 + 64)[0]
    imagebase = struct.unpack_from('<Q', b, pe + 24 + 24)[0]
    sec = pe + 24 + optsz
    sections = []
    for i in range(nsec):
        o = sec + i * 40
        name = b[o:o + 8].rstrip(b'\0').decode('latin-1')
        vsize, vaddr, rawsz, rawptr = struct.unpack_from('<IIII', b, o + 8)
        sections.append((name, imagebase + vaddr, vsize, rawptr, rawsz))
    return {'pe': pe, 'sections': sections, 'cert_off': cOff,
            'cert_size': cSize, 'checksum': csum, 'size': len(b),
            'imagebase': imagebase}


def which_section(info, off):
    for name, va, vsize, rawptr, rawsz in info['sections']:
        if rawptr <= off < rawptr + rawsz:
            return name, va + (off - rawptr)
    return None, None


def group(diffs, gap=24):
    out = []
    for off in diffs:
        if out and off - out[-1][1] <= gap:
            out[-1][1] = off
        else:
            out.append([off, off])
    return out


def disasm(info, b, off, back=32, fwd=32):
    if not HAVE_CS:
        return []
    name, va = which_section(info, off)
    if va is None:
        return []
    md = Cs(CS_ARCH_X86, CS_MODE_64)
    lines = []
    for ins in md.disasm(b[off - back:off + fwd], va - back):
        mark = '   <<< patch' if -1 <= ins.address - va < 8 else ''
        lines.append('    %016X  %-22s %-7s %s%s'
                     % (ins.address, ins.bytes.hex(' '), ins.mnemonic, ins.op_str, mark))
    return lines


def compare(orig_dir, patched_dir, sysname=None):
    files_o = sorted(f for f in os.listdir(orig_dir) if os.path.isfile(os.path.join(orig_dir, f)))
    files_p = sorted(f for f in os.listdir(patched_dir) if os.path.isfile(os.path.join(patched_dir, f)))
    if sysname is None:
        cands = [f for f in files_o if f.lower().endswith('.sys') and not f.endswith('.i64')]
        if not cands:
            raise SystemExit('no .sys found in ' + orig_dir)
        sysname = cands[0][:-4]

    out = []
    out.append('# %s' % sysname)
    out.append('')
    out.append('| | original | patched |')
    out.append('|---|---|---|')
    out.append('| files | %d | %d |' % (len(files_o), len(files_p)))
    out.append('| only in original | %s | |'
               % (', '.join('`%s`' % f for f in set(files_o) - set(files_p)) or '—'))
    out.append('| only in patched | | %s |'
               % (', '.join('`%s`' % f for f in set(files_p) - set(files_o)) or '—'))
    out.append('')

    so = os.path.join(orig_dir, sysname + '.sys')
    sp = os.path.join(patched_dir, sysname + '.sys')
    if not (os.path.exists(so) and os.path.exists(sp)):
        out.append('no `%s.sys` in both directories' % sysname)
        return '\n'.join(out)

    bo, bp = open(so, 'rb').read(), open(sp, 'rb').read()
    io_, ip = pe_info(bo), pe_info(bp)
    out.append('## Image')
    out.append('')
    out.append('| | original | patched |')
    out.append('|---|---|---|')
    out.append('| size | %d B | %d B (%+d) |' % (len(bo), len(bp), len(bp) - len(bo)))
    out.append('| sha1 | `%s` | `%s` |' % (sha1(bo), sha1(bp)))
    if io_ and ip:
        out.append('| PE checksum | 0x%08X | 0x%08X |' % (io_['checksum'], ip['checksum']))
        out.append('| cert table | off 0x%X, 0x%X B | off 0x%X, 0x%X B |'
                   % (io_['cert_off'], io_['cert_size'], ip['cert_off'], ip['cert_size']))
    out.append('')

    n = min(len(bo), len(bp))
    diffs = [i for i in range(n) if bo[i] != bp[i]]
    spans = group(diffs)
    code = []
    blob = []
    for a, b in spans:
        sec, _ = which_section(io_, a) if io_ else (None, None)
        if sec is None or (io_ and a >= io_['cert_off'] and io_['cert_off']):
            blob.append((a, b))
        else:
            code.append((a, b, sec))

    out.append('## Patched code (inside sections)')
    out.append('')
    out.append('| raw offset | location | original | patched | bytes changed |')
    out.append('|---|---|---|---|---|')
    for a, b, sec in code:
        _, va = which_section(io_, a)
        out.append('| `0x%06X` | `%s` (VA `0x%X`) | `%s` | `%s` | %d |'
                   % (a, sec, va, bo[a:b + 1].hex(' ').upper(),
                      bp[a:b + 1].hex(' ').upper(), b - a + 1))
    out.append('')
    out.append('%d site(s), %d byte(s) of code changed.' % (len(code), sum(b - a + 1 for a, b, _ in code)))
    out.append('')

    if blob:
        out.append('## Signature / overlay')
        out.append('')
        for a, b in blob:
            out.append('- `0x%06X..0x%06X` — %d bytes, the Authenticode catalog blob'
                       ' (different signer, so it can never match)'
                       % (a, b, b - a + 1))
        out.append('')

    if HAVE_CS and code:
        out.append('## Disassembly around each site')
        for a, b, sec in code[:12]:
            out.append('')
            out.append('### `0x%06X` (%s)' % (a, sec))
            out.append('')
            out.append('```asm')
            out.append('; original')
            out += disasm(io_, bo, a)
            out.append('; patched')
            out += disasm(ip, bp, a)
            out.append('```')
    return '\n'.join(out)


if __name__ == '__main__':
    args = [a for a in sys.argv[1:] if not a.startswith('--')]
    name = None
    for a in sys.argv[1:]:
        if a.startswith('--sys'):
            name = a.split('=', 1)[1] if '=' in a else None
    if len(args) < 2:
        print(__doc__)
        raise SystemExit(2)
    print(compare(args[0], args[1], name))
