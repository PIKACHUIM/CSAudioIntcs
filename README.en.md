<div align="center">

# ChromeBook CoolStar Audio Drivers

Getting the speakers to work after Windows lands on a Chromebook

[简体中文](README.md) · **English** · [繁體中文](README.zh-TW.md) · [日本語](README.ja.md)

![platform](docs/badges/platform.svg) ![drivers](docs/badges/drivers.svg) ![installer](docs/badges/installer.svg) ![upstream](docs/badges/upstream.svg)

</div>

---

## What this is

This started as one broken HP Chromebook 13 G1. Windows installed fine, Device Manager
looked clean, the volume slider moved, the endpoint level meter hit full scale during
playback, and the speakers stayed silent. The cause turned out to be a licence check inside
the driver: it failed, so the ADSP never started, so the amplifier never left shutdown.

Everything needed to cover the rest of the family is here now. Three CoolStar audio stacks,
the codecs and amplifiers behind them, plus the ChromeOS EC bus, touchpad, fingerprint
reader and camera drivers.

All of it is plain `INF + SYS + CAT` payload. No archives, no self-extracting installers.
The working tree is about 13 MB, and the only foreign binaries that execute at install
time are Microsoft's `DPInst` and the CA installer in `certs/`.

Upstream is a paid, per-machine licence. This pack is for hardware that already holds one.
There is no licence file in here, and no generator or activation tool.

## Pick the right stack first

Chromebook audio splits into three incompatible architectures with disjoint hardware IDs.
Installing the wrong one produces the most expensive symptom to debug: device present,
no sound.

| Your platform | Install | Service |
|---|---|---|
| Skylake / Kaby Lake / Amber Lake / Braswell / Apollo Lake | `drivers/audio/csaudiosstavs` | `CsAudioSSTAVS` |
| Gemini Lake / Jasper Lake / Ice Lake / Tiger Lake / Alder Lake and newer | `drivers/audio/csaudiointcsof` | `CsAudioIntcSOF` |
| AMD Chromebooks (Rembrandt, Phoenix, …) | `drivers/audio/csaudioamdsof` | `CsAudioAmdSOF` |
| Speakers on glados / chell boards (HP Chromebook 13 G1 and friends) | add `drivers/audio/ssm4567` | `ssm4567` |

If you are not sure which platform you have, install the full package. All three stacks
ship in it and DPInst binds only the one whose hardware IDs match.

## Install

**Prebuilt**: grab `ChromeBook-CoolStar-Audio-Driver-<version>-x64.exe` from
[Releases](../../releases), run it as administrator, reboot.

**Build it**: you need [Inno Setup 6](https://jrsoftware.org/isinfo.php). No WDK, no Visual
Studio.

```bat
ISCC.exe installer\Install.iss

:: or pin a version; the output filename follows it
ISCC.exe /DMyAppVersion=1.0.5 installer\Install.iss
```

Output goes to `dist\`.

**One package only**, say the speakers are the only thing broken. Trust the signing root
first, then add the package:

```bat
:: elevated PowerShell, from the unpacked directory
certs\pikachu-certificate-install.exe /VERYSILENT

pnputil /add-driver drivers\audio\ssm4567\ssm4567.inf /install
```

## Driver list

37 packages, 198 hardware IDs. Version, provider and every HWID per package live in
[docs/DRIVERS.md](docs/DRIVERS.md); that table is generated from the INFs by
`python tools/gen-driver-docs.py`, not written by hand.

### Audio

| Package | Version | Notes |
|---|---|---|
| `csaudiosstavs` | 1.0.4.0 | AVS architecture, Skylake through Apollo Lake |
| `csaudiointcsof` | 1.0.4.0 | SOF architecture, Gemini Lake and newer |
| `csaudioamdsof` | 1.0.1.0 | SOF for AMD platforms |
| `ssm4567` | 1.0.1 | ADI I2S amplifier used on glados-family boards |

### Codecs

`nau8825` · `nau88l25rvp` · `cs42l42` · `da7219` · `rt5682` · `rt5682s` · `croseccodec`

### Amplifiers

`max98357a` · `max98390` · `opengmaxcodec` · `rt1011` · `rt1015` · `rt1019`

### Bus and system

`acpbus` · `cbtable` · `crosecbus` · `croseci2c` · `crosectypec` · `sklhdaudbus` ·
`intelpmc` · `inteltcss` · `iaLPSS2_SPI` · `iaLPSS2_UART2` · `dptf_acpi` · `gnodriver`

### Input and camera

`crostouchpad-{synaptics,cypress,cypress-smbus,atmel,elan}` · `crosfingerprint` ·
`ov5675` · `iaisp64_46xx_ADL`

### Shipped, not enabled

`codec/nau88l25rvp` and `codec/nau8825` both claim `ACPI\10508825`, so only one can be
active; the CoolStar build wins by default. `system/gnodriver` is an INF with no `sys`,
there is nothing for it to bind.

Install order is in [installer/payload.list](installer/payload.list). Both the installer
and the uninstaller read that one file.

## Why the genuine driver stays silent

The original `.sys` files contain these strings:

```
AVS: License not found. Please install it to C:\Windows\system32\drivers\avs_signedlicense.bin
AVS: License too small. Make sure the license is signed.
SystemSKU      Chell
```

`DriverEntry` reads the licence, verifies the signature, and compares it against the board
name in SMBIOS. When that fails it returns NTSTATUS `0xC000026A`. The caller sees a failure
and never starts the ADSP. No ADSP means no I2S clock, and an amplifier with no clock stays
muted.

That is why the symptom looks so wrong: devices fine, endpoints fine, meter at full scale,
no sound.

### What the patch changes

5, 12 and 12 sites across the three stacks. 153 bytes total, all the same shape:

```asm
mov eax, 0xC000026A    ; before: return this error when the check fails
mov eax, 0              ; now: report success anyway
```

`return cond ? STATUS_X : STATUS_SUCCESS;` compiles down to `and reg, 0xC000026A`. The patch
turns it into `and reg, 0` and pads the leftover bytes with `nop`.

The INF, the HWID table, the service sections and the DSP firmware are untouched. The
`CsAudioCallbackAPI` callback is still registered, which is how the `ssm4567` amplifier
driver gets its Speaker endpoint.

### Signing

One changed byte destroys the WHQL signature, so the patched builds are re-signed, and the
two files do not share a certificate:

| File | Before | Now | Timestamp |
|---|---|---|---|
| `.sys` | Microsoft WHQL | Airspace Intelligent Technology (Changzhou) | DigiCert, public TSA |
| `.cat` | Microsoft WHQL | Fujian Liuren Network Security | Pikachu Time Sub CA, private |

The `.sys` chain reaches a public root, so it loads with Secure Boot off. The `.cat`
timestamp comes from a private TSA that nothing public trusts, and `pnputil` stops with
`0xE0000242`. That is what the installer in `certs/` is for: it runs before the drivers and
writes the root into `LocalMachine\Root` and `LocalMachine\TrustedPublisher`.

One trap worth knowing in advance. `Get-AuthenticodeSignature` returning `Valid` does not
mean `pnputil` will accept the package; the former verifies the PKCS#7, the latter also has
to pass setupapi's publisher-trust check. Separately, not a single byte of the INF may
change, line endings included, because the catalog stores its hash. `.gitattributes` marks
`*.inf`, `*.sys` and `*.cat` as `binary` for that reason.

Byte-level diff, disassembly, and a comparison with
[akibaPatcher](https://github.com/akibaGumi/akibaPatcher) are in
[docs/PATCH-ANALYSIS.md](docs/PATCH-ANALYSIS.md).

## Layout

```
.
├── .github/workflows/release.yml   CI: version in, release out
├── certs/                          signing root, one Inno Setup installer
├── docs/
│   ├── DRIVERS.md                  driver table, 37 packages / 198 HWIDs, generated
│   ├── PATCH-ANALYSIS.md           patch principle and byte-level diff
│   └── VENDOR.md                   what cannot live in git, and why
├── drivers/                        the payload
│   ├── audio/  codec/  amp/        the parts that make sound
│   ├── bus/    input/  camera/  system/
├── installer/
│   ├── Install.iss                 Inno Setup script
│   ├── payload.list                install order
│   └── License.rtf  dpinsts.ico
├── reference/originals/            genuine CoolStar packages, diff baseline, never installed
├── scripts/
│   ├── preflight.ps1               validates the payload before packaging
│   ├── trust-certs.bat             manual signing-root install
│   ├── install-drivers.bat         DPInst over payload.list
│   └── uninstall-drivers.bat       same list, /U
├── src/ssm4567/                    ADI SSM4567 amplifier source, KMDF + I2C
├── tools/
│   ├── dpinst/dpinst.exe           Microsoft DPInst 2.1
│   ├── gen-driver-docs.py          regenerates docs/DRIVERS.md
│   └── compare-drivers.py          byte-level diff, original vs patched
└── vendor/                         local staging, git-ignored, see docs/VENDOR.md
```

## Development

Packaging needs Inno Setup 6 (`choco install innosetup`) and nothing else. Rebuilding
`src/ssm4567` needs VS 2022 plus WDK; check the WDK/VS compatibility matrix first. Helper
scripts run on PowerShell 5.1 or 7 and Python 3.9+; the disassembly part of
`compare-drivers.py` wants `pip install capstone`.

```bat
:: validate the payload: missing folder, missing inf, dangling catalog reference
powershell -ExecutionPolicy Bypass -File scripts\preflight.ps1

:: package
ISCC.exe /DMyAppVersion=1.0.5 installer\Install.iss

:: refresh the table after touching a driver
python tools\gen-driver-docs.py

:: see how a package differs from the genuine one
python tools\compare-drivers.py reference\originals\csaudiosstavs drivers\audio\csaudiosstavs
```

CI lives in [.github/workflows/release.yml](.github/workflows/release.yml). Trigger it by
hand with a version number, or push a `v*` tag. The pipeline runs preflight, installs Inno
Setup, calls `ISCC /DMyAppVersion=<v>`, writes `SHA256SUMS.txt`, uploads the artifact and
runs `gh release create`. If the tag already exists it uploads over it. A preflight failure
aborts the build, so a broken payload cannot reach a release.

To add a driver, drop the package into `drivers/<category>/<name>/`, append it to
`payload.list` if it should install by default, run the doc generator and preflight, commit.
Driver versions come from the INFs, the installer version comes from the CI input.

Uninstall through Apps & features, or run `scripts\uninstall-drivers.bat`. It walks the same
list with `DPInst /U` and removes the CA on the way out.

## Verified hardware

| Machine | Board | Platform | Result |
|---|---|---|---|
| HP Chromebook 13 G1 | `chell` | Skylake | speakers and headphones work, needs `ssm4567` + `csaudiosstavs` |

Other machines should work based on the platform each stack targets, but I have not tested
them one by one. If you try one, a report with the model, `SystemSKU` and the hardware ID
from Device Manager would be useful.

## Known gaps

Four packages are older than upstream and three families are deliberately absent; the
reasons and workarounds are in [docs/VENDOR.md](docs/VENDOR.md). The one that matters most
is the missing Intel Serial IO I2C/GPIO driver: chell's codec and amplifier sit on that bus.
The only local copy is a DriverStore dump with no catalog, which cannot be turned into an
installable package.

## Notice

CoolStar audio drivers © CoolStar, distributed via <https://coolstar.org/chromebook/> under
a per-machine paid licence. This repository is an unofficial repack for hardware that
already holds one, and contains no licence file, generator or activation tool.
`DPInst.exe` © Microsoft, shipped with the WDK. `src/ssm4567` © CoolStar, from
<https://github.com/coolstar/ssm4567>, see `src/ssm4567/LICENSE.txt`. ChromeOS EC, touchpad,
camera and DPTF drivers belong to their respective vendors.

Not affiliated with Google, CoolStar or any hardware vendor. Putting Windows on a Chromebook
carries a brick risk, especially when touching firmware; installing drivers is reversible.
