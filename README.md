# ChromeBook CoolStar Audio Drivers

Windows driver pack for Intel / AMD Chromebooks: the CoolStar audio stacks, the codecs
and speaker amplifiers they talk to, plus the ChromeOS embedded-controller bus, touchpad,
fingerprint and camera packages that a converted Chromebook needs.

Everything in this repository is plain driver payload (`INF + SYS + CAT`, plus a few
firmware blobs) — no archives, no self-extracting installers. The only binaries that are
executed are Microsoft's `DPInst` and Windows' own `certutil`.

> This is a redistribution/repack. The drivers themselves are CoolStar's work
> (<https://coolstar.org/chromebook/>). See [NOTICE](#notice).

---

## Quick start

**End user** — download the latest installer from
[Releases](../../releases), run it, reboot.

**From source** — build the installer yourself:

```bat
:: requires Inno Setup 6 (choco install innosetup)
ISCC.exe installer\Install.iss

:: or with an explicit version
ISCC.exe /DMyAppVersion=1.0.5 installer\Install.iss
```

The installer lands in `dist\`. No WDK, no Visual Studio, no compiler is needed:
the payload is prebuilt.

---

## Repository layout

```
.
├── .github/workflows/release.yml   CI: version in -> installer + GitHub Release out
├── certs/                          signing roots the driver catalogs chain to
├── docs/
│   ├── DRIVERS.md                  generated driver table (versions + 198 hardware IDs)
│   └── VENDOR.md                   what lives in vendor/ and why it is not committed
├── drivers/                        the payload, grouped by function
│   ├── audio/                      CoolStar audio stacks (AVS / SOF / AMD SOF) + ssm4567
│   ├── codec/                      headset & speaker codecs
│   ├── amp/                        I2S speaker amplifiers
│   ├── bus/                        EC bus, I2C, SPI, PMC, Type-C, HDA bus
│   ├── input/                      touchpads, fingerprint reader
│   ├── camera/                     Intel IPU / MIPI cameras
│   └── system/                     DPTF, Quanta placeholder
├── installer/
│   ├── Install.iss                 Inno Setup script
│   ├── payload.list                install order, one folder per line
│   ├── License.rtf                 licence shown by the installer
│   └── dpinsts.ico
├── scripts/
│   ├── preflight.ps1               validates payload.list before packaging (runs in CI)
│   ├── trust-certs.bat             imports certs\*.cer into Root + TrustedPublisher
│   ├── install-drivers.bat         walks payload.list through DPInst
│   └── uninstall-drivers.bat       same list, DPInst /U
├── src/ssm4567/                    ADI SSM4567 amp driver source (KMDF + I2C)
├── tools/
│   ├── dpinst/dpinst.exe           Microsoft Driver Package Installer 2.1
│   └── gen-driver-docs.py          regenerates docs/DRIVERS.md
└── vendor/                         git-ignored local staging, see docs/VENDOR.md
```

`drivers/` is 11 MB; the whole tracked tree is around 13 MB.

---

## Which audio stack do I need?

Only one applies. Installing the wrong one is the most common reason for
"device present, no sound".

| platform | package | note |
|---|---|---|
| Skylake, Kaby Lake, Amber Lake, Braswell, Apollo Lake | `drivers/audio/csaudiosstavs` | **AVS** architecture (`CsAudioSSTAVS` service) |
| Gemini Lake, Jasper Lake, Ice Lake, Tiger Lake, Alder Lake, Mendocino, Cezanne | `drivers/audio/csaudiointcsof` | **SOF** architecture |
| AMD (Rembrandt, Phoenix…) | `drivers/audio/csaudioamdsof` | **AMD SOF** |
| glados / chell (HP Chromebook 13 G1) speakers | `drivers/audio/ssm4567` | separate I2C amp, see below |

The installer ships all three; DPInst binds whichever one matches the hardware ID, so a
single installer covers every supported machine.

### The SSM4567 case

On `glados`-family boards the speakers are driven by **two ADI SSM4567 amplifiers** on
I2C4 (`ACPI\INT343B\0` at 0x34 and `\1` at 0x35). `INT343B` there is *not* the Intel HD
Audio controller it means on a normal PC — see
`src/mainboard/google/glados/variants/chell/overridetree.cb` in coreboot.

The chip powers up in software power-down **and** DAC-muted state, so with no driver
bound the endpoints look perfect (Windows even reports full 0.35 peak level) while the
speakers stay silent. `drivers/audio/ssm4567` fixes exactly that.

Its catalog is signed against a test root that ships in `certs/`. If you install the
driver by hand instead of via the installer, run `scripts\trust-certs.bat` first,
otherwise `pnputil` fails with:

```
0xE0000242  发布者信任尚未建立 / publisher trust for the Authenticode-signed catalog
```

---

## Build pipeline

`.github/workflows/release.yml`

* **Trigger** — `workflow_dispatch` with a `version` input (and an optional
  *pre-release* toggle), or pushing a `v*` tag.
* **Steps** — checkout → version resolve → `scripts/preflight.ps1` → install Inno Setup
  via Chocolatey → `ISCC /DMyAppVersion=<version>` → SHA-256 sums → artifact upload →
  `gh release create` (falls back to `gh release upload --clobber` if the tag exists).

Version resolution:

| trigger | tag | installer name |
|---|---|---|
| dispatch, `version = 1.0.5` | `v1.0.5` | `ChromeBook-CoolStar-Audio-Driver-1.0.5-x64.exe` |
| push tag `v2.0.0` | `v2.0.0` | `ChromeBook-CoolStar-Audio-Driver-2.0.0-x64.exe` |

`preflight.ps1` fails the build when a folder listed in `payload.list` is missing, has no
`.inf`, or references a `.cat` that is not there — so a broken payload never reaches a
release.

---

## Adding or updating a driver

1. Drop the package into the right `drivers/<category>/<name>/` folder
   (`.inf` + `.sys` + `.cat`, plus any firmware blobs, all in the same directory).
2. Append `<category>\<name>` to `installer/payload.list` if it should be installed —
   order matters: bus drivers first, audio controller last-ish, see the comments.
3. `python tools/gen-driver-docs.py` to refresh `docs/DRIVERS.md`.
4. `powershell -File scripts/preflight.ps1` to check it.
5. Commit. The driver version is read from the `.inf`, the installer version from the
   workflow input.

Never edit `.inf` line endings after a catalog was generated — the `.cat` contains hashes
of the `.inf` bytes. `.gitattributes` marks driver files as `binary` for this reason.

---

## Uninstalling

Add/remove programs, or from an admin console:

```bat
scripts\uninstall-drivers.bat
```

It walks the same `payload.list` and calls `DPInst /U` for each `.inf`.

---

## Notice

* CoolStar audio drivers — © CoolStar, distributed through
  <https://coolstar.org/chromebook/>. The upstream packages are licence-gated; this
  repository is an unofficial repack for hardware that is already licensed, and contains
  no licence keys, licence generators or activation tooling.
* `DPInst.exe` — © Microsoft, redistributed as part of the WDK.
* `src/ssm4567` — © CoolStar, from <https://github.com/coolstar/ssm4567> (see
  `src/ssm4567/LICENSE.txt`).
* ChromeOS EC, touchpad, camera and DPTF drivers — © their respective vendors.

Not affiliated with Google, CoolStar or any hardware vendor. Chromebooks converted to
Windows can be bricked by a bad flash; driver installation itself is reversible.
