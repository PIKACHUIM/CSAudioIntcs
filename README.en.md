<div align="center">

# ChromeBook CoolStar Drivers

A collection of ChromeBook-specific drivers, including audio, touchpad and related devices

[简体中文](README.md) · **English** · [繁體中文](README.zh-TW.md) · [日本語](README.ja.md)

![platform](docs/badges/platform.svg) ![drivers](docs/badges/drivers.svg) ![installer](docs/badges/installer.svg) ![license](docs/badges/license.svg)

</div>

[Overview](#overview) · [Quick start](#quick-start) · [Driver list](#driver-list) · [Patch details](#patch-details) · [License](#license)

---

## Overview

This repository packages ChromeBook drivers for Windows. The tree is reorganised by function and contains 37 driver packages covering 198 hardware IDs.

| Item | Detail |
|---|---|
| Packages | 37, of which 35 install by default |
| Hardware IDs | 198 |
| Coverage | Audio (AVS / SOF), codecs, amplifiers, touchpad, touchscreen, fingerprint, camera, EC bus, chipset components |
| Platforms | Intel Skylake through Alder Lake, AMD Rembrandt / Phoenix |
| Format | Plain `INF` + `SYS` + `CAT`, no archives, no self-extracting installers |
| Tooling | Microsoft DPInst 2.1, Inno Setup 6 |

The drivers come from CoolStar and the respective ChromeOS hardware vendors. Versions and hardware IDs are unchanged; only the directory layout and install order are new. Two external binaries execute during installation: `tools/dpinst/dpinst.exe` and `certs/pikachu-certificate-install.exe`.

The upstream audio drivers are commercial software licensed per machine. This repository targets hardware that already holds a licence and contains no licence file, keygen or activation tool.

```
.
├── drivers/            audio codec amp bus input camera system
├── installer/          Install.iss, payload.list (install order)
├── scripts/            preflight, install, uninstall, trust-certs
├── certs/              signing root installer
├── reference/originals/  genuine CoolStar packages, diff baseline only
├── src/ssm4567/        ADI SSM4567 amplifier source
├── tools/              DPInst, doc generator, binary diff
├── docs/               DRIVERS.md, PATCH-ANALYSIS.md, VENDOR.md
└── vendor/             local scratch, git-ignored
```

## Quick start

### From a release

1. Download `ChromeBook-CoolStar-Audio-Driver-<version>-x64.exe` from [Releases](../../releases)
2. Run it as administrator
3. Reboot

The installer imports the signing root first, then calls DPInst for each entry in `payload.list`. Drivers already present at the same version are skipped.

### Building from source

The only dependency is [Inno Setup 6](https://jrsoftware.org/isinfo.php). No WDK, no Visual Studio.

```bat
:: optional: validate the payload
powershell -ExecutionPolicy Bypass -File scripts\preflight.ps1

:: build, output goes to dist\
ISCC.exe installer\Install.iss
ISCC.exe /DMyAppVersion=1.0.5 installer\Install.iss
```

CI is defined in `.github/workflows/release.yml`. Trigger it with a version, or push a `v*` tag. It runs preflight, builds, writes `SHA256SUMS.txt` and creates the Release.

### Installing a single driver

For the case where only one device is missing. Import the signing root first, then point `pnputil` at the INF.

```bat
:: elevated PowerShell
certs\pikachu-certificate-install.exe /VERYSILENT

pnputil /add-driver drivers\audio\ssm4567\ssm4567.inf /install
```

### Choosing an audio stack

The three audio architectures are mutually incompatible and their hardware IDs do not overlap.

| Platform | Package | Service |
|---|---|---|
| Skylake, Kaby Lake, Amber Lake, Braswell, Apollo Lake | `csaudiosstavs` | `CsAudioSSTAVS` |
| Gemini Lake, Jasper Lake, Ice Lake, Tiger Lake, Alder Lake and newer | `csaudiointcsof` | `CsAudioIntcSOF` |
| AMD: Rembrandt, Phoenix, ... | `csaudioamdsof` | `CsAudioAmdSOF` |
| Speaker amplifiers on glados-family boards | `ssm4567` | `ssm4567` |

The full installer ships all three stacks; DPInst binds only the matching one. `ssm4567` is an independent amplifier driver and installs alongside any of them.

## Driver list

Versions and the complete HWID list are in [docs/DRIVERS.md](docs/DRIVERS.md), generated from the INFs by `python tools/gen-driver-docs.py`.

### Audio

| Package | Version | Description |
|---|---|---|
| `csaudiosstavs` | 1.0.4.0 | AVS architecture, Skylake through Apollo Lake |
| `csaudiointcsof` | 1.0.4.0 | SOF architecture, Gemini Lake and newer |
| `csaudioamdsof` | 1.0.1.0 | SOF for AMD platforms |
| `ssm4567` | 1.0.1 | ADI I2S amplifier, glados-family boards |

### Codecs

`nau8825` · `nau88l25rvp` · `cs42l42` · `da7219` · `rt5682` · `rt5682s` · `croseccodec`

### Amplifiers

`max98357a` · `max98390` · `opengmaxcodec` · `rt1011` · `rt1015` · `rt1019`

### Bus and system

`acpbus` · `cbtable` · `crosecbus` · `croseci2c` · `crosectypec` · `sklhdaudbus` · `intelpmc` · `inteltcss` · `iaLPSS2_SPI` · `iaLPSS2_UART2` · `dptf_acpi` · `gnodriver`

### Input and camera

`crostouchpad-{synaptics,cypress,cypress-smbus,atmel,elan}` · `crosfingerprint` · `ov5675` · `iaisp64_46xx_ADL`

Two packages ship but do not install by default. `nau88l25rvp` and `nau8825` share hardware ID `ACPI\10508825`, and the CoolStar build wins by default. `gnodriver` has no `SYS` and is INF-only.

Install and uninstall both read [installer/payload.list](installer/payload.list) as the single source of truth.

## Patch details

The CoolStar audio driver reads a licence file in `DriverEntry`, verifies its signature and compares it against the board name in SMBIOS. On failure it returns NTSTATUS `0xC000026A`, and the caller stops before starting the ADSP. Without the ADSP there is no I2S clock, and the amplifier stays shut down.

The original binary contains these strings in plain ASCII:

```
AVS: License not found. Please install it to C:\Windows\system32\drivers\avs_signedlicense.bin
AVS: License too small. Make sure the license is signed.
SystemSKU      Chell
```

Device enumeration, endpoint creation and level meters all behave normally, and the speakers stay silent.

### Patch sites

The three audio stacks are patched at 5, 12 and 12 sites, 153 bytes in total, all in the same form:

```asm
mov eax, 0xC000026A    ; returned when the check failed
mov eax, 0              ; now returns success directly
```

In source this is `return cond ? STATUS_X : STATUS_SUCCESS;`, which compiles to `and reg, 0xC000026A`. The patch changes it to `and reg, 0` and pads the remaining bytes with `nop`, so instruction lengths are unchanged.

The INF, HWID tables, service sections and DSP firmware are untouched. The `CsAudioCallbackAPI` callback still registers, and `ssm4567` depends on it to register the Speaker endpoint.

### Signatures

A single modified byte voids the original WHQL signature, so both files are re-signed, by different parties.

| File | Original | Current | Timestamp |
|---|---|---|---|
| `.sys` | Microsoft WHQL | Airspace Intelligent Technology (Changzhou) | DigiCert public TSA |
| `.cat` | Microsoft WHQL | Fujian Liuren Network Security | Pikachu Time Sub CA (self-hosted) |

The `.sys` chain reaches a public root and loads with Secure Boot off. The `.cat` timestamp comes from a self-hosted TSA that public roots do not trust, so `pnputil` reports:

```
0xE0000242  A certificate chain processed, but terminated in a root certificate
            which is not trusted by the trust provider
```

`certs/pikachu-certificate-install.exe` writes that root into both `LocalMachine\Root` and `LocalMachine\TrustedPublisher`; either store missing reproduces the error.

A `Valid` result from `Get-AuthenticodeSignature` does not mean `pnputil` accepts the package: the former verifies the PKCS#7 structure and chain, the latter also requires setupapi's publisher trust check. The INF must not change by a single byte, line endings included, because the catalog stores its hash, which is why `.gitattributes` marks `*.inf`, `*.sys` and `*.cat` as `binary`.

Byte-level diffs, disassembly and an implementation comparison are in [docs/PATCH-ANALYSIS.md](docs/PATCH-ANALYSIS.md); the tool is `tools/compare-drivers.py`.

## License

The scripts, documentation and build configuration in this repository are released under the MIT License, see [LICENSE](LICENSE).

The driver binaries retain their vendors' terms and are not covered by MIT:

| Component | Terms |
|---|---|
| CoolStar audio drivers | Commercial, licensed per machine, see <https://coolstar.org/chromebook/> |
| `src/ssm4567` | CoolStar, see `src/ssm4567/LICENSE.txt` |
| `tools/dpinst/dpinst.exe` | Microsoft, redistributed with the WDK |
| ChromeOS EC, touchpad, camera, DPTF packages | Respective vendors |

MIT covers only the material added by this repository and does not alter the licensing of the components above. This project is not affiliated with Google, CoolStar or any hardware vendor. Installing Windows on a Chromebook carries a bricking risk, and modifying firmware carries more; driver installation is reversible and the uninstall script removes both the drivers and the imported root certificate.
