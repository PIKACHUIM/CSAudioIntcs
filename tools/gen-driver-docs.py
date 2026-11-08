#!/usr/bin/env python3
"""Regenerate docs/DRIVERS.md from the .inf files under drivers/.

Usage:  python tools/gen-driver-docs.py
"""
import os
import re

ROOT = os.path.dirname(os.path.dirname(os.path.abspath(__file__)))
DRV = os.path.join(ROOT, 'drivers')

ORDER = ['audio', 'codec', 'amp', 'bus', 'input', 'camera', 'system']
CATEGORY = {
    'audio':  ('Audio controllers',
               'CoolStar audio stacks: **AVS** (`csaudiosstavs`) for Skylake / Kaby Lake / '
               'Braswell / Apollo Lake, **SOF** (`csaudiointcsof`) for Gemini Lake and newer, '
               'and **AMD SOF** (`csaudioamdsof`) for AMD Chromebooks.'),
    'codec':  ('Audio codecs',
               'Headset / speaker codecs sitting behind the audio controller.'),
    'amp':    ('Speaker amplifiers',
               'I2S speaker amplifiers. Most have no ACPI node of their own and are bound '
               'through the audio controller instead.'),
    'bus':    ('Platform / EC bus',
               'ChromeOS embedded controller, I2C, SPI, PMC, Type-C and HDA glue. '
               'Install these before codecs and amplifiers.'),
    'input':  ('Input devices',
               'Touchpads (one package per controller vendor) and the fingerprint reader.'),
    'camera': ('Cameras',
               'Intel IPU / MIPI camera packages.'),
    'system': ('System',
               'Intel Dynamic Tuning (DPTF) and the Quanta placeholder driver.'),
}
NOT_DEFAULT = {
    'nau88l25rvp': 'Nuvoton reference codec, binds the same `ACPI\\10508825` as `nau8825` — mutually exclusive',
    'gnodriver':   'placeholder INF only, ships no `.sys`',
}


def read_text(path):
    raw = open(path, 'rb').read()
    if raw[:2] in (b'\xff\xfe', b'\xfe\xff'):
        return raw.decode('utf-16')
    if raw[:3] == b'\xef\xbb\xbf':
        return raw[3:].decode('utf-8', 'replace')
    if raw.count(b'\x00') > len(raw) // 4:
        return raw.decode('utf-16-le', 'replace')
    return raw.decode('latin-1')


def parse_inf(path):
    txt = read_text(path)

    def grab(pat):
        m = re.search(pat, txt, re.I | re.M)
        return m.group(1).strip().strip('"') if m else ''

    strings = {}
    for m in re.finditer(r'^\s*([A-Za-z0-9_.]+)\s*=\s*"([^"]*)"', txt, re.M):
        strings[m.group(1).lower()] = m.group(2)

    def resolve(name):
        v = name.strip()
        if v.startswith('%') and v.endswith('%'):
            return strings.get(v[1:-1].lower(), v)
        return v

    prov = resolve(grab(r'^\s*Provider\s*=\s*([^\r\n;]+)'))
    ver = grab(r'^\s*DriverVer\s*=\s*([^\r\n;]+)')
    cls = resolve(grab(r'^\s*Class\s*=\s*([^\r\n;]+)'))

    hw = []
    for m in re.finditer(r'^[^;\r\n]*?=\s*[^,\r\n]+,\s*([^\r\n;]+)', txt, re.M):
        v = m.group(1).strip().strip('\r\n\t ')
        if '\\' not in v or ' ' in v or len(v) < 6:
            continue
        if not re.match(r'^(ACPI|PCI|USB|HID|ROOT|SWD|CSAUDIO|INT|GOOG)', v, re.I):
            continue
        if v not in hw:
            hw.append(v)
    return prov, ver, cls, hw


rows = []
for cat in ORDER:
    cdir = os.path.join(DRV, cat)
    if not os.path.isdir(cdir):
        continue
    for name in sorted(os.listdir(cdir)):
        d = os.path.join(cdir, name)
        if not os.path.isdir(d):
            continue
        infs = sorted(f for f in os.listdir(d) if f.lower().endswith('.inf'))
        if not infs:
            continue
        provs, vers, clss, hws = [], [], [], []
        for i in infs:
            p, v, c, h = parse_inf(os.path.join(d, i))
            for src, dst in ((p, provs), (v, vers), (c, clss)):
                if src and src not in dst:
                    dst.append(src)
            for x in h:
                if x not in hws:
                    hws.append(x)
        nfiles = len([f for f in os.listdir(d) if os.path.isfile(os.path.join(d, f))])
        rows.append({
            'cat': cat, 'name': name, 'infs': infs,
            'prov': ', '.join(provs) or '-',
            'ver': ', '.join(vers) or '-',
            'cls': ', '.join(clss) or '-',
            'hw': hws, 'files': nfiles,
        })

out = []
w = out.append
w('# Driver payload')
w('')
w('Generated from the `.inf` files under `drivers/` — run `python tools/gen-driver-docs.py`')
w('after adding or updating a package. Every entry is a plain `INF + SYS + CAT` set')
w('(a few also carry firmware blobs); nothing here is an archive or a self-extracting exe.')
w('')
w('Install order is defined in [`installer/payload.list`](../installer/payload.list);')
w('it is the same file the installer and uninstaller iterate over.')
w('')
w('| | |')
w('|---|---|')
w('| packages | %d |' % len(rows))
w('| hardware IDs | %d |' % sum(len(r['hw']) for r in rows))
w('')

for cat in ORDER:
    sel = [r for r in rows if r['cat'] == cat]
    if not sel:
        continue
    title, desc = CATEGORY[cat]
    w('## %s' % title)
    w('')
    w('`drivers/%s/` — %s' % (cat, desc))
    w('')
    w('| package | driver version | provider | class | files |')
    w('|---|---|---|---|---|')
    for r in sel:
        w('| `%s` | %s | %s | %s | %d |' % (r['name'], r['ver'], r['prov'], r['cls'], r['files']))
    w('')
    for r in sel:
        if not r['hw']:
            continue
        label = r['name'] if len(r['hw']) < 6 else '%s — %d IDs' % (r['name'], len(r['hw']))
        w('<details><summary><code>%s</code></summary>' % label)
        w('')
        w('```')
        for h in r['hw']:
            w(h)
        w('```')
        w('')
        w('</details>')
        w('')

w('## Shipped but not installed by default')
w('')
w('| package | why |')
w('|---|---|')
for k, v in NOT_DEFAULT.items():
    w('| `%s` | %s |' % (k, v))
w('')
w('Both folders stay in the tree so the payload is complete; they are simply excluded')
w('from `payload.list`.')
w('')

os.makedirs(os.path.join(ROOT, 'docs'), exist_ok=True)
with open(os.path.join(ROOT, 'docs', 'DRIVERS.md'), 'w', encoding='utf-8', newline='\n') as f:
    f.write('\n'.join(out) + '\n')
print('docs/DRIVERS.md: %d packages, %d lines, %d hardware IDs'
      % (len(rows), len(out), sum(len(r['hw']) for r in rows)))
