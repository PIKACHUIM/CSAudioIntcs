<div align="center">

# ChromeBook CoolStar Drivers

A Chromebook running Windows usually has no working sound card, touchpad or camera.
This repository collects the CoolStar and ChromeOS vendor drivers into one installer.

[简体中文](README.md) · **English** · [繁體中文](README.zh-TW.md) · [日本語](README.ja.md)

![platform](docs/badges/platform.svg) ![drivers](docs/badges/drivers.svg) ![installer](docs/badges/installer.svg) ![license](docs/badges/license.svg)

[Download](../../releases) · [Driver list](docs/DRIVERS.md) · [Patch notes](docs/PATCH-ANALYSIS.md) · [Version gaps](docs/UPSTREAM.md) · [License](LICENSE)

</div>

---

## What this is

36 driver packages, 171 hardware IDs, 35 of them installed by default.

| Item | Detail |
|---|---|
| Packages | 36, of which 35 are on the install list |
| Hardware IDs | 171 |
| Devices | Audio controllers, codecs, amplifiers, touchpads, touchscreens, fingerprint, cameras, EC bus, DPTF |
| Platforms | Intel Skylake through Alder Lake, AMD Rembrandt / Phoenix |
| Format | Plain `INF` + `SYS` + `CAT`, no archives or self-extracting installers |
| Tooling | Microsoft DPInst 2.1, Inno Setup 6 |

The drivers come from CoolStar and the ChromeOS hardware vendors. Version numbers and hardware IDs are untouched; only the directory layout and install order are new.

Two external programs run during installation: `tools/dpinst/dpinst.exe` and `certs/pikachu-certificate-install.exe`. The first installs drivers, the second adds the catalog signing root to the certificate stores. Nothing else runs.

The CoolStar audio drivers are commercial software licensed per machine. This repository is for hardware that already holds a licence. It ships no licence file, no key generator and no activation tool.

```
.
├── drivers/             the payload, sorted into audio codec amp bus input camera system
├── installer/           Install.iss (packaging), payload.list (install order)
├── scripts/             preflight, install, uninstall, trust roots
├── certs/               signing root installer
├── reference/originals/ pristine CoolStar packages, used as a diff baseline
├── src/ssm4567/         SSM4567 amplifier driver source
├── tools/               DPInst, doc generator, binary comparison
└── docs/                driver list, patch details, signature status, version gaps
```

## Install

### From a release

1. Download `ChromeBook-CoolStar-Audio-Driver-<version>-x64.exe` from [Releases](../../releases)
2. Right-click and run as administrator
3. Reboot when it finishes

The installer imports the signing root first, then calls DPInst for every entry in `installer/payload.list`, in order. A driver that is already installed at the same version is skipped, not overwritten.

The packages are x64 only. There is no 32-bit build.

### One driver only

Say the touchpad is the only thing missing. Import the root, then point `pnputil` at the INF:

```bat
:: elevated PowerShell
certs\pikachu-certificate-install.exe /VERYSILENT

pnputil /add-driver drivers\input\crostouchpad-synaptics\crostouchpad.inf /install
```

To remove them again, run `scripts\uninstall-drivers.bat`. It also deletes the imported root.

### Which audio stack

Three architectures, incompatible with each other, with no overlap in hardware IDs. DPInst binds whichever one matches, so a full install needs no thought. Pick by platform only when installing a single package.

| Platform | Package | Service |
|---|---|---|
| Skylake, Kaby Lake, Amber Lake, Braswell, Apollo Lake | `csaudiosstavs` | `CsAudioSSTAVS` |
| Gemini Lake, Jasper Lake, Ice Lake, Tiger Lake, Alder Lake and newer | `csaudiointcsof` | `CsAudioIntcSOF` |
| AMD Rembrandt, Phoenix and similar | `csaudioamdsof` | `CsAudioAmdSOF` |
| Speaker amp on glados, chell and similar boards | `ssm4567` | `ssm4567` |

`ssm4567` is a separate amplifier driver. It installs alongside any of the three stacks.

## Driver list

Full versions and every hardware ID are in [docs/DRIVERS.md](docs/DRIVERS.md). That file is generated from the INFs by `python tools/gen-driver-docs.py`, so edit the generator, not the output.

**Audio**: `csaudiosstavs` 1.0.4.0 · `csaudiointcsof` 1.0.4.0 · `csaudioamdsof` 1.0.1.0 · `ssm4567` 1.0.1

**Codecs**: `nau8825` · `nau88l25rvp` · `cs42l42` · `da7219` · `rt5682` · `rt5682s` · `croseccodec`

**Amplifiers**: `max98357a` · `max98390` · `opengmaxcodec` · `rt1011` · `rt1015` · `rt1019`

**Bus and system**: `acpbus` · `cbtable` · `crosecbus` · `croseci2c` · `crosectypec` · `sklhdaudbus` · `intelpmc` · `inteltcss` · `iaLPSS2_SPI` · `iaLPSS2_UART2` · `dptf_acpi`

**Input and cameras**: `crostouchpad-{synaptics,cypress,cypress-smbus,atmel,elan}` · `crosfingerprint` · `ov5675` · `iaisp64_46xx_ADL`

`nau88l25rvp` and `nau8825` claim the same hardware ID, `ACPI\10508825`. The CoolStar build is the default, so `nau88l25rvp` ships but stays off the install list. [installer/payload.list](installer/payload.list) drives both install and uninstall.

## Why the stock driver is silent

The CoolStar audio driver reads a licence file in `DriverEntry`, verifies the signature, and compares it against the board codename in SMBIOS. When that fails it returns `0xC000026A`, and the caller does not start the ADSP. No ADSP means no I2S clock, so the amplifier stays muted.

The symptom is confusing: Device Manager is clean, every endpoint exists, the level meter moves during a test tone, and the speakers do nothing.

The strings are right there in the binary:

```
AVS: License not found. Please install it to C:\Windows\system32\drivers\avs_signedlicense.bin
AVS: License too small. Make sure the license is signed.
SystemSKU
Chell
```

29 sites and 153 bytes are patched across the three audio stacks. They all do the same thing: change the immediate used for the failure code so the function always reports success. Either the `mov eax, 0xC000026A` on an error branch, or the `sbb / not / and` sequence that MSVC emits for `return cond ? STATUS_X : STATUS_SUCCESS;`.

The INF, hardware ID tables, service sections and DSP firmware are byte-identical to the originals. The `CsAudioCallbackAPI` registration stays, because `ssm4567` needs it to expose the speaker endpoint.

Offsets and disassembly: [docs/PATCH-SITES.md](docs/PATCH-SITES.md). Reasoning: [docs/PATCH-ANALYSIS.md](docs/PATCH-ANALYSIS.md).

## Signatures

Every package carries a catalog, but they come from three sources and only one of them needs preparation.

| Source | Packages | Extra step |
|---|---|---|
| Microsoft WHQL | 24 | none |
| Third party, public root | 5 | none (the five touchpads) |
| Third party, non-public root | 7 | import the root first |

The last seven are not broken. Their catalog timestamp chain simply ends at a certificate no public root trusts. Without the import, `pnputil` stops here:

```
0xE0000242  Publisher trust for the Authenticode(TM) signed catalog has not been established
```

`certs/pikachu-certificate-install.exe` writes to both `LocalMachine\Root` and `LocalMachine\TrustedPublisher`. Missing either one is enough to fail. The installer runs it before touching any driver.

One trap: `Get-AuthenticodeSignature` returning `Valid` does not mean `pnputil` will accept the package. The cmdlet checks the PKCS#7 structure and the chain; setupapi also checks publisher trust. Per-package status is in [docs/SIGNATURES.md](docs/SIGNATURES.md).

If you patch a `.sys` yourself, the catalog has to be rebuilt and both the binary and the catalog signed again. The procedure is documented in [FakeSign](https://github.com/PIKACHUIM/FakeSign). No certificates or private keys live in this repository.

### Signing the installer

The `exe` that Actions produces has no Authenticode signature. SmartScreen will warn once on first run; click through and it installs normally.

To sign it properly, use your own certificate locally. Signing changes the file hash, so `SHA256SUMS.txt` has to be regenerated afterwards:

```powershell
signtool sign /fd SHA256 /f mycert.pfx /p <password> `
  /tr http://timestamp.digicert.com /td SHA256 `
  dist\ChromeBook-CoolStar-Audio-Driver-<version>-x64.exe
```

```powershell
$out = "dist\SHA256SUMS.txt"
Remove-Item $out -ErrorAction SilentlyContinue
Get-ChildItem dist -Filter *.exe | ForEach-Object {
  "$((Get-FileHash $_.FullName -Algorithm SHA256).Hash.ToLower())  $($_.Name)"
} | Out-File $out -Encoding ascii
```

Then replace the release assets with `gh release upload v<version> dist\*.exe dist\SHA256SUMS.txt --clobber`.

## Build

All you need is [Inno Setup 6](https://jrsoftware.org/isinfo.php). No WDK, no Visual Studio.

```bat
:: optional, verifies that the payload has no missing pieces
powershell -ExecutionPolicy Bypass -File scripts\preflight.ps1

ISCC.exe installer\Install.iss
ISCC.exe /DMyAppVersion=26.11.26 installer\Install.iss
```

Output lands in `dist\`. Without a version override the script uses the one baked into `Install.iss`.

CI lives in `.github/workflows/release.yml`. Trigger it by hand with a version number, or push a `v*` tag. The job runs preflight, builds, writes `SHA256SUMS.txt`, then creates the release.

## Licence

The scripts, docs and build files in this repository are MIT. Full text in [LICENSE](LICENSE).

The driver binaries belong to their vendors and are not covered by MIT:

| Component | Licence |
|---|---|
| CoolStar audio drivers | Commercial, per machine, see <https://coolstar.org/chromebook/> |
| `src/ssm4567` | CoolStar, see `src/ssm4567/LICENSE.txt` |
| `tools/dpinst/dpinst.exe` | Microsoft, redistributed with the WDK |
| ChromeOS EC, touchpad, camera, DPTF drivers | Respective vendors |

MIT covers only what this repository adds. It does not change the licence of anything listed above. This project is not affiliated with Google, CoolStar or any hardware vendor.

Installing Windows on a Chromebook can brick it, and firmware changes are riskier still. A driver install is reversible: the uninstall script removes both the drivers and the imported root certificate.
