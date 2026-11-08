# `vendor/` — local staging, not committed

Several things that used to sit in the repository cannot be tracked any more. They are
moved here so nothing is lost, and `.gitignore` keeps the folder out of git.

| folder | what it is | why it is not committed |
|---|---|---|
| `installers/` | Upstream CoolStar installers: `coolstar-audio-driver.exe`, `cr50.1.0.1-installer.exe`, `crosec.2.0.6/2.0.7-installer.exe`, `crostouchpad.4.1.6-installer.exe`, `crostouchscreen.2.9.5-installer.exe`, `drivers-installtool-install.exe` | Self-extracting NSIS / Inno packages. They use an inverted NSIS header layout wrapped by WinLicense, so neither 7-Zip (26.04 / 19.00 / 9.20) nor any static unpacker can read them, and the sandbox refuses to execute them. Shipping them would mean shipping binaries that cannot be audited — and the payload they carry is already present as plain files under `drivers/`. `pikachu-certificate-install.exe` is the exception: it is tracked, in `certs/`, because the installer has to run it |
| `alt-signatures/` | `sha160/`, `sha256/` — two older cracked builds of the same three audio stacks, plus `driverall-crack/` and `typec/` | Same drivers, different catalog-signature generations (a SHA-1 test cert and a SHA-256 test cert). The WHQL originals are tracked in `reference/originals/`, the build the project ships is in `drivers/`; keeping four copies around invites accidentally installing two different builds of `csaudiosstavs` |
| `camera-redrix/` | Intel camera DriverStore dump for the Redrix boards (`iacamera64`, `hi556`, `ov2740`, `ov8856`, …) — 268 MB, single files up to 46 MB | Too large for a driver payload repo, and specific to one board family. `drivers/camera/` keeps the two packages that the general installer needs (`ov5675`, `iaisp64_46xx_ADL`) |
| `duplicate-addons/` | `CoolStarAudioDrivers-Addons` | Byte-identical to `drivers/bus`, `drivers/codec`, `drivers/amp` |
| `archives/` | `CoolStarAudioDrivers-Addons.rar`, `Comparers.rar`, three `Intel-FORCED-Camera-*.zip` | Contents already unpacked into `drivers/` |

## What was deleted outright

| path | reason |
|---|---|
| `Licensing/` (363 MB) | WinLicense SDK — the licence-protection toolchain, not part of the driver pack |
| `Installer/protect.wl` | WinLicense protection project for the old installer executable |
| `Installer/install.aip` | Advanced Installer project; superseded by `installer/Install.iss` |
| `Installer/Release/` | Build output (old `.exe` / `.zip` releases) |
| `CrackFile/…/CoolStarAudioDrivers-Backup/` | Two `.rar` files holding nothing but the three cracked installers |
| `Update.bat` | Generic interactive git helper menu, unrelated to the project |
| `.vs/` | Visual Studio cache |

Everything removed here is still reachable through git history:

```bat
git log --diff-filter=D --summary -- Licensing
git show <commit>:Licensing/WinLicense.exe > WinLicense.exe
```

## What *is* tracked

`reference/originals/` holds the three genuine CoolStar packages (WHQL-signed, `dpinst.exe`
removed) as they came from upstream. They are not part of the payload and are never
installed — they exist so that the patch is auditable:
`tools/compare-drivers.py` diffs them against `drivers/audio/` and `docs/PATCH-ANALYSIS.md`
records the result. 9.5 MB, worth it.

## Upstream is newer than what we ship — known gaps

Checked 2026-11-09. Everything currently in `drivers/` is the newest copy obtainable from
the material at hand, but five packages are known to lag or be missing upstream:

| package | repo | upstream | why not updated |
|---|---|---|---|
| `csaudiointcsof` (SOF) | 1.0.4.0 (2024-04-23) | **1.0.5-1** (`csaudiointcsof.1.0.5-1-installer.exe`, 2026-03; staged in `vendor/installers/`, sha256 `a147b28f…e0e1e`) | upstream is a licensed per-machine download. 1.0.5 fixed a regression that broke audio at random and another that killed HDMI audio; 1.0.5-1 additionally fixes the Redrix 4-speaker mapping and the reversed stereo imaging that ≤1.0.5 (and every cracked ≤1.0.4 build) shows. The installer is inverted-header NSIS — 7-Zip 19.00 / 23.01 / 26.04 all refuse it, so the payload has to be released by running it once |
| `csaudiosstavs` (AVS) | 1.0.4.0 (2023-11-07) | **1.0.6** (`csaudiosof4skl.1.0.6-installer.exe`, 2025-05; staged in `vendor/installers/`, sha256 `c4eede33…32aeae`) | version taken from the installer filename, not verified by unpacking; same extraction blocker |
| `crostouchpad` | 4.1 (2017-08-03) | **4.1.6** (`crostouchpad.4.1.6-installer.exe`, sitting in `vendor/installers/`) | the installer is an inverted-header NSIS wrapped by WinLicense — 7-Zip 26.04 / 19.00 / 9.20 all refuse it and the sandbox will not execute it, so the payload cannot be extracted here |
| `crostouchscreen` | — | **2.9.5** | same reason: `crostouchscreen.2.9.5-installer.exe` cannot be unpacked |
| `cr50` | — | **1.0.1** | same reason: `cr50.1.0.1-installer.exe` cannot be unpacked |

Mirrors for the two audio packages, both uploaded by third parties rather than CoolStar:

```
https://archive.org/download/sof_1.0.5.1/csaudiointcsof-1.0.5-1.zip
https://archive.org/download/csaudiosstavs_202505/csaudiosstavs.zip
```

Each zip also carries `gui/GenLicense.exe` and `sof-intellicense.txt`, which this repo
does not redistribute.


`crosecbus` is *not* a gap — the repo ships 2.1.0.0, newer than the 2.0.6 / 2.0.7
installers floating around.

Three more things exist but were left out on purpose:

| package | status |
|---|---|
| Intel Serial IO `iaLPSS2i_I2C_{SKL,BXT_P,GLK,CNL}` + `iaLPSS2i_GPIO2_*` | **genuinely needed** — this is the I2C/GPIO bus the codecs, amps and touchpads hang off (the SKL package covers `PCI\VEN_8086&DEV_9D60..9D65`, i.e. chell's `9D64`). A DriverStore dump has them, but the dump carries no `.cat`, so the packages are unsigned and will not install on x64. Needs the original Intel Serial IO installer |
| `dptf_cpu` | available locally *with* a catalog (20 MB of DPTF policy DLLs). Pairs with the `dptf_acpi` we do ship. Thermal only, no audio impact — left out to keep the payload lean |
| Redrix camera extras (`hi556`, `ov2740`, `ov5675_extension`, `iacamera64`) | 268 MB, Lenovo/Dell-board specific; `drivers/camera/` keeps the two general packages instead |
