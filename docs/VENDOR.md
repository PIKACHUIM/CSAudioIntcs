# `vendor/` — local staging, not committed

Several things that used to sit in the repository cannot be tracked any more. They are
moved here so nothing is lost, and `.gitignore` keeps the folder out of git.

| folder | what it is | why it is not committed |
|---|---|---|
| `installers/` | Upstream CoolStar installers: `coolstar-audio-driver.exe`, `cr50.1.0.1-installer.exe`, `crosec.2.0.6/2.0.7-installer.exe`, `crostouchpad.4.1.6-installer.exe`, `crostouchscreen.2.9.5-installer.exe`, `pikachu-certificate-install.exe`, `drivers-installtool-install.exe` | Self-extracting NSIS / Inno packages. They use an inverted NSIS header layout wrapped by WinLicense, so neither 7-Zip (26.04 / 19.00 / 9.20) nor any static unpacker can read them, and the sandbox refuses to execute them. Shipping them would mean shipping binaries that cannot be audited — and the payload they carry is already present as plain files under `drivers/` |
| `alt-signatures/` | `origin/`, `sha160/`, `sha256/` — three older builds of the same three audio stacks, plus `driverall-crack/` and `typec/` | Same drivers, different catalog-signature generations (WHQL, a SHA-1 test cert, a SHA-256 test cert). Only the variant the project actually ships is kept in `drivers/`; keeping four copies invites accidentally installing two different builds of `csaudiosstavs` |
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
