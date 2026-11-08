# ChromeBook CoolStar Audio Drivers

[简体中文](README.md) | **English** | [繁體中文](README.zh-TW.md) | [日本語](README.ja.md)

After turning a Chromebook into a Windows machine, the hard part is rarely the GPU or the
touchpad — it is **sound**. This repository packages everything a Chromebook needs to make
noise: the CoolStar audio stacks, the codecs and speaker amplifiers behind them, plus the
ChromeOS embedded-controller bus, touchpad, fingerprint and camera drivers.

Everything here is plain driver payload (`INF + SYS + CAT`, a few with firmware blobs) —
no archives, no self-extracting installers. The tracked tree is 13 MB and the only foreign
binaries that ever execute are Microsoft's `DPInst` and Windows' own `certutil`.

> This is an **unofficial repack**. The drivers are CoolStar's work
> (<https://coolstar.org/chromebook/>); upstream is a per-machine licensed commercial
> product. See [Notice](#notice).

---

## Contents

- [What this is](#what-this-is)
- [Quick start](#quick-start)
- [Driver list](#driver-list)
- [How the patch works](#how-the-patch-works)
- [Repository layout](#repository-layout)
- [Development](#development)
- [Verified hardware](#verified-hardware)
- [Notice](#notice)

---

## What this is

Chromebook audio is nothing like a normal laptop:

| Normal laptop | Chromebook |
|---|---|
| HD Audio codec on the HDA bus, works out of the box | The audio controller is an Intel **AVS / SOF** ADSP; codecs and amps sit on **I2C / I2S** |
| The BIOS initialises the codec | The driver has to program every codec register over I2C |
| The amp is inside the codec | The amp is a separate chip (MAX98357A / SSM4567 / RT1015…) gated by GPIO + I2S clocks |

So the missing-driver symptom is not a yellow bang in Device Manager — it is far more
confusing: **everything green, all four audio endpoints present, the endpoint level meter
pegs at full scale during playback, and the speakers stay completely silent**. The ADSP
never started, so there is no I2S clock and the amplifier never leaves power-down/mute.

That is the problem this package solves.

---

## Quick start

**Just install**: grab the latest `ChromeBook-CoolStar-Audio-Driver-<version>-x64.exe` from
[Releases](../../releases), run it as administrator, reboot.

**Build it yourself** (needs [Inno Setup 6](https://jrsoftware.org/isinfo.php); no WDK,
no Visual Studio, no compiler):

```bat
ISCC.exe installer\Install.iss

:: or pin a version
ISCC.exe /DMyAppVersion=1.0.5 installer\Install.iss
```

Output lands in `dist\`.

**Install a single package** (e.g. only the speaker amp):

```bat
:: elevated PowerShell, from the unpacked directory
certutil -addstore -f Root             certs\pikachu-testca-root.cer
certutil -addstore -f TrustedPublisher certs\pikachu-testca-root.cer
pnputil /add-driver drivers\audio\ssm4567\ssm4567.inf /install
```

---

## Driver list

37 packages, 198 hardware IDs. The full table (DriverVer, provider and every HWID per
package) lives in **[docs/DRIVERS.md](docs/DRIVERS.md)** and is generated from the INFs by
`python tools/gen-driver-docs.py`.

### Audio (`drivers/audio/`)

| Package | Version | Platform |
|---|---|---|
| `csaudiosstavs` | 1.0.4.0 | **AVS**: Skylake / Kaby Lake / Amber Lake / Braswell / Apollo Lake |
| `csaudiointcsof` | 1.0.4.0 | **SOF**: Gemini Lake / Jasper Lake / Ice Lake / Tiger Lake / Alder Lake… |
| `csaudioamdsof` | 1.0.1.0 | **AMD SOF**: Rembrandt / Phoenix and other AMD Chromebooks |
| `ssm4567` | 1.0.1 | ADI SSM4567 I2S amp (glados / chell, i.e. HP Chromebook 13 G1) |

> **Install exactly one.** Picking the wrong architecture is the most common cause of
> "device present, no sound": AVS and SOF are entirely different stacks with disjoint
> hardware IDs, and DPInst binds whichever one matches.

### Codecs (`drivers/codec/`)

`nau8825` · `nau88l25rvp` · `cs42l42` · `da7219` · `rt5682` · `rt5682s` · `croseccodec`

### Speaker amplifiers (`drivers/amp/`)

`max98357a` · `max98390` · `opengmaxcodec` · `rt1011` · `rt1015` · `rt1019`

### Bus and system (`drivers/bus/`, `drivers/system/`)

`acpbus` · `cbtable` · `crosecbus` · `croseci2c` · `crosectypec` · `sklhdaudbus` ·
`intelpmc` · `inteltcss` · `iaLPSS2_SPI` · `iaLPSS2_UART2` · `dptf_acpi` · `gnodriver`

### Input and camera (`drivers/input/`, `drivers/camera/`)

`crostouchpad-{synaptics,cypress,cypress-smbus,atmel,elan}` · `crosfingerprint` ·
`ov5675` · `iaisp64_46xx_ADL`

### Shipped but not installed by default

| Package | Why |
|---|---|
| `codec/nau88l25rvp` | Nuvoton reference codec; fights `codec/nau8825` over the same `ACPI\10508825` |
| `system/gnodriver` | INF-only placeholder, ships no `.sys` |

Install order lives in [`installer/payload.list`](installer/payload.list) — the installer
and the uninstaller iterate the same file.

---

## How the patch works

Two questions to answer: **why does the genuine driver stay silent**, and **what did this
build actually change**. The full evidence chain (byte-level diff, disassembly, signature
comparison, and a route comparison against akibaPatcher) is in
**[docs/PATCH-ANALYSIS.md](docs/PATCH-ANALYSIS.md)** (Chinese).

### 1. The blocker: a per-machine licence check

CoolStar's audio drivers are licensed. The original `.sys` contains, in plain sight:

```
AVS: License not found. Please install it to C:\Windows\system32\drivers\avs_signedlicense.bin
AVS: License too small. Make sure the license is signed.
SystemSKU      Chell
```

`DriverEntry` reads the licence file, verifies its signature, and compares it against the
SMBIOS `SystemSKU` (the board name). On failure it returns NTSTATUS `0xC000026A` and the
caller **never starts the ADSP** — so no I2S clock, no amplifier power, full-scale meter,
dead silence.

### 2. The patch: turn "failed" into "succeeded"

Across the three stacks, 5 / 12 / 12 sites are changed — 153 bytes in total, all the same
shape:

```asm
; original — return the error code when the check fails
mov    eax, 0xC000026A
; patched — report success even when it fails
mov    eax, 0
```

For `return cond ? STATUS_X : STATUS_SUCCESS;` the compiler emits `and reg, 0xC000026A`;
the patch turns it into `and reg, 0`, padding the leftover bytes with `nop`.

| Stack | Sites | Bytes changed |
|---|---|---|
| `csaudiosstavs` | 5 | 25 B |
| `csaudiointcsof` | 12 | 64 B |
| `csaudioamdsof` | 12 | 64 B |

**The INF, the HWID table, the service sections and the DSP firmware are untouched**, and
the `CsAudioCallbackAPI` callback is still registered with the CoolStar bus driver — which
is exactly how the `ssm4567` amp driver obtains its Speaker endpoint.

### 3. Signing: regenerate the catalog and re-sign

One changed byte destroys the WHQL signature, so the patched builds are **re-signed**, and
the two chains behave very differently:

| File | Original signer | Patched signer | Timestamp |
|---|---|---|---|
| `.sys` | Microsoft WHQL | Airspace Intelligent Technology (Changzhou) | DigiCert (**public TSA**) |
| `.cat` | Microsoft WHQL | Fujian Liuren Network Security Co., Ltd. | Pikachu Time Sub CA (**private TSA**) |

The `.sys` chain reaches a publicly trusted root, so it loads on any machine with Secure
Boot off. The `.cat` timestamp comes from a private TSA that nothing public trusts, so
`pnputil` refuses the package with:

```
0xE0000242  publisher trust for the Authenticode-signed catalog has not been established
```

**The private TSA root therefore has to be installed on the target first**
(`certs/pikachu-testca-root.cer`; the installer does it for you):

```bat
certutil -addstore -f Root             certs\pikachu-testca-root.cer
certutil -addstore -f TrustedPublisher certs\pikachu-testca-root.cer
```

> Two pitfalls that cost real time:
> ① `Get-AuthenticodeSignature` returning `Valid` does **not** mean `pnputil` will accept
>    the package — the former only verifies the PKCS#7, the latter also has to pass
>    setupapi's publisher-trust check.
> ② Not a single byte of the `.inf` may change (**including line endings**), because the
>    catalog stores its hash. That is why `.gitattributes` marks `*.inf`/`*.sys`/`*.cat`
>    as `binary`.

### 4. How this differs from akibaPatcher

[akibaPatcher](https://github.com/akibaGumi/akibaPatcher) solves the same problem for SOF
1.0.5.0, but far more bluntly: it deletes the `.sys` Authenticode certificate table
outright, forges a 20-year self-signed certificate, rewrites four functions into
`xor eax,eax; ret` / `mov eax,1; ret`, and **requires test-signing to be enabled**.

This repository's build is a minimal semantic patch plus a real re-sign: it costs a code
signing certificate and a timestamp service, and in exchange **needs no test-signing** and
leaves no watermark on the machine. Side-by-side table in
[docs/PATCH-ANALYSIS.md](docs/PATCH-ANALYSIS.md) (section 5).

---

## Repository layout

```
.
├── .github/workflows/release.yml   CI: version in → build → GitHub Release
├── certs/                          signing roots (installer imports Root + TrustedPublisher)
├── docs/
│   ├── DRIVERS.md                  generated driver table (37 packages / 198 HWIDs)
│   ├── PATCH-ANALYSIS.md           patch principle and byte-level diff
│   └── VENDOR.md                   what cannot live in git, and why
├── drivers/                        the payload, grouped by function
│   ├── audio/  codec/  amp/        ← the parts that make sound
│   ├── bus/    input/  camera/  system/
├── installer/
│   ├── Install.iss                 Inno Setup script (version injected on the command line)
│   ├── payload.list                install order, shared by install and uninstall
│   ├── License.rtf  dpinsts.ico
├── reference/originals/            genuine CoolStar packages (WHQL), the diff baseline
├── scripts/
│   ├── preflight.ps1               validates the payload before packaging (runs in CI)
│   ├── trust-certs.bat             imports the signing roots
│   ├── install-drivers.bat         DPInst over payload.list
│   └── uninstall-drivers.bat       DPInst /U over payload.list
├── src/ssm4567/                    ADI SSM4567 amp driver source (KMDF + I2C)
├── tools/
│   ├── dpinst/dpinst.exe           Microsoft Driver Package Installer 2.1
│   ├── gen-driver-docs.py          regenerates docs/DRIVERS.md
│   └── compare-drivers.py          byte-level diff, original vs patched
└── vendor/                         local staging, git-ignored (see docs/VENDOR.md)
```

---

## Development

### Requirements

- **Packaging**: Inno Setup 6 (`choco install innosetup`). No WDK, no Visual Studio.
- **Rebuilding the amp driver** (`src/ssm4567`): VS 2022 + WDK (check the WDK/VS
  compatibility matrix first).
- **Helper scripts**: PowerShell 5.1 or 7, Python 3.9+ (`capstone` only for disassembly).

### Build and self-check

```bat
:: 1) validate the payload (missing folder / missing inf / dangling catalog all fail)
powershell -ExecutionPolicy Bypass -File scripts\preflight.ps1

:: 2) package
ISCC.exe /DMyAppVersion=1.0.5 installer\Install.iss

:: 3) refresh the driver table after touching a package
python tools\gen-driver-docs.py

:: 4) see how a package differs from the genuine one
python tools\compare-drivers.py reference\originals\csaudiosstavs drivers\audio\csaudiosstavs
```

### CI

[`.github/workflows/release.yml`](.github/workflows/release.yml):

| Trigger | Behaviour |
|---|---|
| `workflow_dispatch` | takes a `version` input (e.g. `1.0.5`) and an optional pre-release flag → tags `v1.0.5` and publishes a Release |
| push `v*` tag | version comes from the tag name |

Pipeline: checkout → resolve version → `preflight.ps1` → install Inno Setup via Chocolatey
→ `ISCC /DMyAppVersion=<v>` → `SHA256SUMS.txt` → artifact upload → `gh release create`
(falls back to `gh release upload --clobber` when the tag already exists).

A preflight failure aborts the build, so a broken payload can never reach a release.

### Adding or updating a driver

1. Drop the package into `drivers/<category>/<name>/` (`.inf` + `.sys` + `.cat`, plus any
   firmware blobs in the same directory)
2. Append `<category>\<name>` to `installer/payload.list` if it should be installed —
   order matters (bus first, audio controller later; the file has comments)
3. `python tools/gen-driver-docs.py` to refresh the table
4. `powershell -File scripts/preflight.ps1` to self-check
5. Commit. The driver version comes from the INF, the installer version from the CI input

### Uninstalling

Apps & features, or from an elevated console:

```bat
scripts\uninstall-drivers.bat
```

It walks the same `payload.list` and calls `DPInst /U` for every INF.

---

## Verified hardware

| Machine | Board | Platform | Result |
|---|---|---|---|
| HP Chromebook 13 G1 | `chell` | Skylake | ✅ speakers + headphones (needs `ssm4567` + `csaudiosstavs`) |

Everything else should work based on the platform each stack targets, but has **not** been
verified machine by machine. If yours is not listed, please report the model + board name
(`SystemSKU`) + the hardware ID from Device Manager after installing.

---

## Notice

- **CoolStar audio drivers** — © CoolStar, distributed via
  <https://coolstar.org/chromebook/>. Upstream is a per-machine licensed commercial
  product. This repository is an **unofficial repack** for hardware that is already
  licensed, and contains **no licence files, licence generators or activation tooling**.
- **`DPInst.exe`** — © Microsoft, redistributed as part of the WDK.
- **`src/ssm4567`** — © CoolStar, from <https://github.com/coolstar/ssm4567>
  (see `src/ssm4567/LICENSE.txt`).
- **ChromeOS EC / touchpad / camera / DPTF drivers** — © their respective vendors.

Not affiliated with Google, CoolStar or any hardware vendor. Converting a Chromebook to
Windows carries a brick risk (firmware flashing especially); installing drivers is
reversible.
