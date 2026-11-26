<div align="center">

# ChromeBook / ChromeBox Driver Pack

A driver pack for Chromebook and Chromebox, covering dozens of uncommon Chrome devices

![platform](docs/badges/platform.svg) ![drivers](docs/badges/drivers.svg) ![installer](docs/badges/installer.svg) ![license](docs/badges/license.svg)

[Download](../../releases) · [Driver list](docs/DRIVERS.md) · [Patch notes](docs/PATCH-ANALYSIS.md) · [Version gaps](docs/UPSTREAM.md) · [Licence](LICENSE)

[简体中文](README.md) · **English** · [繁體中文](README.zh-TW.md) · [日本語](README.ja.md)

</div>

---

## Overview

36 drivers covering 171 hardware IDs.

The pack is adapted per device and includes the audio-specific hardware setup drivers.

| Item | Detail |
|---|---|
| Drivers | 36, covering 171 hardware IDs |
| Devices | Audio controllers, codecs, amplifiers, touchpads, touchscreens, fingerprint, cameras, EC bus, DPTF |
| Platforms | Intel Skylake through Alder Lake, AMD Rembrandt / Phoenix |
| Files | Plain `INF` + `SYS` + `CAT`, no archives or self-extracting installers |
| Tooling | Microsoft DPInst 2.1, Inno Setup 6 |

## Requirements
1. Windows 10 x64 1607 or newer
2. Windows 11 with the July 2026 update or newer: turn off the "code integrity cross-signed certificate" policy:
    ```pwsh
    CiTool.exe --remove-policy "{8F9CB695-5D48-48D6-A329-7202B44607E3}"
    ```


None of the following is required:
  1. **No need** to turn off Secure Boot
  2. **No need** to enable test mode
  3. **No need** to turn off the "Microsoft vulnerable driver blocklist"

## Risks
1. Installing Windows on a Chromebook can brick it
2. These drivers have not been thoroughly tested, use at your own risk


## Install

1. Download `ChromeBook-CoolStar-Audio-Driver-<version>-x64.exe` from [Releases](../../releases)
2. Right-click and run as administrator, then reboot

## What the installer does

1. Imports the signing roots first, including the CAs that ship with Windows
2. Imports the test certificate and the trusted cross-CA root used to sign the drivers
3. Calls DPInst for each package in order, skipping packages already installed at the same version

x64 only. a64 and x32 Windows have no matching packages.

## Repository layout

```
.
├── drivers/             the drivers, sorted into audio codec amp bus input camera system
├── installer/           Install.iss (packaging), payload.list (install order)
├── scripts/             preflight, install, uninstall, trust roots
├── certs/               signing root installer
├── reference/originals/ pristine CoolStar packages, used as a diff baseline
├── src/ssm4567/         SSM4567 amplifier driver source
├── tools/               DPInst, doc generator, binary comparison
└── docs/                driver list, patch details, signature status, version gaps
```

## Driver list

Full versions and every hardware ID are in [docs/DRIVERS.md](docs/DRIVERS.md). That file is generated from the INFs by `python tools/gen-driver-docs.py`, so edit the generator, not the output.

### Audio

- `csaudiosstavs` 1.0.4.0
- `csaudiointcsof` 1.0.4.0
- `csaudioamdsof` 1.0.1.0
- `ssm4567` 1.0.1

### Codecs

- `nau8825`
- `nau88l25rvp`
- `cs42l42`
- `da7219`
- `rt5682`
- `rt5682s`
- `croseccodec`

### Amplifiers

- `max98357a`
- `max98390`
- `opengmaxcodec`
- `rt1011`
- `rt1015`
- `rt1019`

### Bus and system

- `acpbus`
- `cbtable`
- `crosecbus`
- `croseci2c`
- `crosectypec`
- `sklhdaudbus`
- `intelpmc`
- `inteltcss`
- `iaLPSS2_SPI`
- `iaLPSS2_UART2`
- `dptf_acpi`

### Input and cameras

- `crostouchpad-synaptics`
- `crostouchpad-cypress`
- `crostouchpad-cypress-smbus`
- `crostouchpad-atmel`
- `crostouchpad-elan`
- `crosfingerprint`
- `ov5675`
- `iaisp64_46xx_ADL`

`nau88l25rvp` and `nau8825` claim the same hardware ID, `ACPI\10508825`. The CoolStar build is the default, so `nau88l25rvp` ships but stays off the install list. [installer/payload.list](installer/payload.list) drives both install and uninstall.




## Build

Use [Inno Setup 6](https://jrsoftware.org/isinfo.php) to build the installer.

```bat
:: optional, verifies that the payload has no missing pieces
powershell -ExecutionPolicy Bypass -File scripts\preflight.ps1

ISCC.exe installer\Install.iss
ISCC.exe /DMyAppVersion=26.11.26 installer\Install.iss
```


## Licence

This project is distributed under the [MIT licence](LICENSE).

The driver binaries belong to their vendors and are not covered by MIT:

| Component | Licence |
|---|---|
| CoolStar audio drivers | Commercial, per machine |
| `src/ssm4567` | `src/ssm4567/LICENSE.txt` |
| `tools/dpinst/dpinst.exe` | Microsoft, redistributed with the WDK |
| ChromeOS EC, touchpad, camera, DPTF drivers | Respective vendors |

MIT covers only what this repository adds. It does not change the licence of anything listed above.

This project is not affiliated with Google, CoolStar or any hardware vendor.
