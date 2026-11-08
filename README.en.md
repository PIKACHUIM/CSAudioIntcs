<div align="center">

# ChromeBook CoolStar Audio Drivers

Windows on a Chromebook, speakers silent. This is the driver set that made them work.

[简体中文](README.md) · **English** · [繁體中文](README.zh-TW.md) · [日本語](README.ja.md)

![platform](docs/badges/platform.svg) ![drivers](docs/badges/drivers.svg) ![installer](docs/badges/installer.svg) ![upstream](docs/badges/upstream.svg)

</div>

[What this is](#what-this-is) · [Pick the right stack](#pick-the-right-stack) · [Install](#install) · [Driver list](#driver-list) · [Why the genuine driver stays silent](#why-the-genuine-driver-stays-silent) · [Layout](#layout) · [Development](#development) · [Machines tested](#machines-tested) · [Known gaps](#known-gaps) · [Notice](#notice)

---

## What this is

An HP Chromebook 13 G1 with Windows installed. Device Manager was clean, the volume slider moved, and the speaker endpoint's level meter peaked at 0.35 when I played a test tone. Nothing came out of the speakers.

My first guess was the amplifier never being powered. Swapping driver versions, flashing coreboot and turning off fast startup changed nothing. Then I opened the CoolStar `.sys` in a hex editor and found the string `AVS: License not found`, pointing at `C:\Windows\system32\drivers\avs_signedlicense.bin`. The licence check fails, `DriverEntry` returns `0xC000026A`, and the caller stops there instead of starting the ADSP. No ADSP means no I2S clock, and an amplifier without a clock stays shut down.

Devices enumerate, endpoints exist, meters move, sound does not. That combination looks like dead hardware. It is software cutting off the second half of the chain. The details are in [Why the genuine driver stays silent](#why-the-genuine-driver-stays-silent).

What is in here came out of that debugging session: the three CoolStar audio stacks, the codecs and amplifiers underneath them, and the ChromeOS EC bus, touchpad, fingerprint and camera drivers to go with them.

Everything is plain `INF` + `SYS` + `CAT`. No archives, no self-extracting installers. The only external binaries that run during installation are Microsoft's `DPInst` and the certificate installer in `certs/`, and both can be inspected.

Upstream sells a per-machine licence. This is for machines that already hold one. There is no licence file, no keygen and no activation tool in the repo.

## Pick the right stack

Chromebook audio splits into three architectures. They are not interchangeable and their hardware IDs do not overlap.

| Platform | Install | Service |
|---|---|---|
| Skylake, Kaby Lake, Amber Lake, Braswell, Apollo Lake | `drivers/audio/csaudiosstavs` | `CsAudioSSTAVS` |
| Gemini Lake, Jasper Lake, Ice Lake, Tiger Lake, Alder Lake and newer | `drivers/audio/csaudiointcsof` | `CsAudioIntcSOF` |
| AMD Chromebooks: Rembrandt, Phoenix, ... | `drivers/audio/csaudioamdsof` | `CsAudioAmdSOF` |
| Speakers on glados-family boards such as the HP Chromebook 13 G1 | plus `drivers/audio/ssm4567` | `ssm4567` |

That last row deserves a note. Skylake-era Chromebooks hang the speaker amplifiers off I2C and route headphones through a separate codec, so the two paths are independent. Headphones working while the speakers stay silent means `ssm4567` is missing. It has nothing to do with whether the main driver version is right.

If you are unsure, install the full pack. All three stacks are inside, `DPInst` only binds the one whose hardware IDs match, and the other two sit there doing nothing.

## Install

Grab `ChromeBook-CoolStar-Audio-Driver-<version>-x64.exe` from [Releases](../../releases), run it as administrator, reboot.

Building it yourself needs nothing beyond [Inno Setup 6](https://jrsoftware.org/isinfo.php). No WDK, no Visual Studio.

```bat
ISCC.exe installer\Install.iss

:: pass a version and the output name follows it
ISCC.exe /DMyAppVersion=1.0.5 installer\Install.iss
```

Output lands in `dist\`.

If only one path is broken you can install it on its own. Trust the signing root first, then add the package:

```bat
:: elevated PowerShell, from the unpacked directory
certs\pikachu-certificate-install.exe /VERYSILENT

pnputil /add-driver drivers\audio\ssm4567\ssm4567.inf /install
```

`pnputil` binds twice, once for `ACPI\INT343B\0` and once for `\1`. Device Manager then shows two `Analog Devices SSM4567 Amplifier` nodes in a working state.

## Driver list

37 packages, 198 hardware IDs. Version, provider and the full HWID list for every package are in [docs/DRIVERS.md](docs/DRIVERS.md). That file is generated from the INFs by `python tools/gen-driver-docs.py`, so editing it by hand only lasts until the next run.

### Audio

| Package | Version | What it is |
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

Two packages ship but are not installed by default. `nau88l25rvp` and `nau8825` claim the same `ACPI\10508825`, only one can win, and the CoolStar build is the default. `gnodriver` is INF-only with no `sys`, so there is nothing for it to bind.

Install and uninstall both read [installer/payload.list](installer/payload.list), which is also where the order lives.

## Why the genuine driver stays silent

The original `.sys` contains these strings in plain ASCII, with no packing and no obfuscation.

```
AVS: License not found. Please install it to C:\Windows\system32\drivers\avs_signedlicense.bin
AVS: License too small. Make sure the license is signed.
SystemSKU      Chell
```

`DriverEntry` opens the licence file, verifies the signature, then compares against the board name from SMBIOS. Any step failing returns NTSTATUS `0xC000026A`, and the caller gives up on starting the ADSP.

### What the patch changes

Five sites in the AVS stack, twelve in each SOF stack, 153 bytes in total. All of the same shape.

```asm
mov eax, 0xC000026A    ; what it returned on a failed check
mov eax, 0              ; what it returns now
```

`return cond ? STATUS_X : STATUS_SUCCESS;` compiles to `and reg, 0xC000026A`. The patch turns that into `and reg, 0` and pads the leftover bytes with `nop`, so instruction lengths and downstream offsets stay put.

The INF, the HWID tables, the service sections and the DSP firmware are untouched. The `CsAudioCallbackAPI` callback still registers, and the `ssm4567` amplifier driver depends on it to get its Speaker endpoint at all. What got patched is the licence decision; the audio path is factory code.

### Signatures and the trust chain

Changing a byte voids the WHQL signature, so the patched builds are re-signed. The two files are signed by different parties.

| File | Original | Now | Timestamp |
|---|---|---|---|
| `.sys` | Microsoft WHQL | Airspace Intelligent Technology, Changzhou | DigiCert, public TSA |
| `.cat` | Microsoft WHQL | Fujian Liuren Network Security | Pikachu Time Sub CA, private |

The `.sys` chain reaches a public root, so it loads with Secure Boot off. The `.cat` timestamp comes from a private TSA that no public root vouches for, and `pnputil` stops right there:

```
0xE0000242  A certificate chain processed, but terminated in a root certificate
            which is not trusted by the trust provider
```

The installer in `certs/` exists for exactly this. It runs before the drivers and writes the root into both `LocalMachine\Root` and `LocalMachine\TrustedPublisher`. Skip either store and you are back at 0xE0000242.

One trap worth mentioning up front. `Get-AuthenticodeSignature` returning `Valid` does not mean `pnputil` will accept the package. The former checks the PKCS#7 structure and the certificate chain; the latter also runs setupapi's publisher trust check. And no byte of the INF may change, line endings included, because the catalog stores its hash. That is why `.gitattributes` marks `*.inf`, `*.sys` and `*.cat` as `binary`.

Byte-level diffs, disassembly, and a comparison with the [akibaPatcher](https://github.com/akibaGumi/akibaPatcher) approach are in [docs/PATCH-ANALYSIS.md](docs/PATCH-ANALYSIS.md).

## Layout

```
.
├── .github/workflows/release.yml   CI: give it a version, get a Release
├── certs/                          signing root, a single Inno Setup installer
├── docs/
│   ├── DRIVERS.md                  driver table, 37 packages / 198 HWIDs, generated
│   ├── PATCH-ANALYSIS.md           what the patch does, byte by byte
│   └── VENDOR.md                   what could not be included, and why
├── drivers/                        the drivers themselves
│   ├── audio/  codec/  amp/        the parts that make sound
│   ├── bus/    input/  camera/  system/
├── installer/
│   ├── Install.iss                 Inno Setup script
│   ├── payload.list                install order
│   └── License.rtf  dpinsts.ico
├── reference/originals/            genuine CoolStar packages, diff baseline only
├── scripts/
│   ├── preflight.ps1               validate the payload before packaging
│   ├── trust-certs.bat             trust the root when installing by hand
│   ├── install-drivers.bat         drive DPInst from payload.list
│   └── uninstall-drivers.bat       same list, with /U
├── src/ssm4567/                    ADI SSM4567 amplifier source, KMDF over I2C
├── tools/
│   ├── dpinst/dpinst.exe           Microsoft DPInst 2.1
│   ├── gen-driver-docs.py          regenerate docs/DRIVERS.md
│   └── compare-drivers.py          byte-level diff, original vs patched
└── vendor/                         local scratch, git-ignored, see docs/VENDOR.md
```

## Development

Packaging needs Inno Setup 6 and nothing else, `choco install innosetup` is enough. Touching `src/ssm4567` needs VS 2022 plus a WDK, and the WDK only supports specific VS versions, so a mismatch simply will not compile. Helper scripts run on PowerShell 5.1 or 7 and Python 3.9+. The disassembly part of `compare-drivers.py` wants `pip install capstone`; without it the tool still runs, you just get no instructions.

```bat
:: validate the payload: missing folders, missing infs, catalog references that
:: point at nothing all fail here
powershell -ExecutionPolicy Bypass -File scripts\preflight.ps1

:: build
ISCC.exe /DMyAppVersion=1.0.5 installer\Install.iss

:: refresh the driver table after touching a package
python tools\gen-driver-docs.py

:: see how one package differs from the genuine build
python tools\compare-drivers.py reference\originals\csaudiosstavs drivers\audio\csaudiosstavs
```

CI lives in [.github/workflows/release.yml](.github/workflows/release.yml). Trigger it by hand with a version, or push a `v*` tag. It runs preflight, installs Inno Setup, calls `ISCC /DMyAppVersion=<v>`, writes `SHA256SUMS.txt`, uploads the artifact and calls `gh release create`, falling back to an upload with `--clobber` when the tag already exists. A failed preflight aborts the run, so a broken package cannot reach a Release.

To add a driver, drop the package into `drivers/<category>/<name>/`, add a line to `payload.list` if it should install by default, run the doc generator and preflight, commit. Driver versions come from the INF and the installer version comes from the CI input, so neither needs syncing by hand.

Uninstall through the Apps list, or run `scripts\uninstall-drivers.bat`, which walks the same list calling `DPInst /U` and then removes the CA.

## Machines tested

| Machine | Board | Platform | Result |
|---|---|---|---|
| HP Chromebook 13 G1 | `chell` | Skylake | Speakers and headphones both work, needs `ssm4567` plus `csaudiosstavs` |

That is the only one. Other machines should work if they belong to the same platform family, but I have no way to test them, so I am not promising it. If you try one, send me the model, the `SystemSKU`, and the hardware IDs of `ACPI\INT343B` and `ACPI\10508825`, and it goes in the table.

## Known gaps

Four packages are older here than upstream, and three things were deliberately left out. Both are explained in [docs/VENDOR.md](docs/VENDOR.md).

The one that matters is Intel Serial IO I2C and GPIO. The codec and amplifier on a chell sit on that I2C bus, and a clean install without it cannot even program the codec's registers. All I have is a DriverStore dump with no catalog, which is not enough to build a package that x64 will install, so it stops there.

## Notice

CoolStar audio drivers © CoolStar, distributed from <https://coolstar.org/chromebook/> under a per-machine licence. This repo is an unofficial repack for hardware that already holds one; it contains no licence file, generator or activation tool. `DPInst.exe` © Microsoft, shipped with the WDK. `src/ssm4567` © CoolStar, from <https://github.com/coolstar/ssm4567>, see `src/ssm4567/LICENSE.txt`. The ChromeOS EC, touchpad, camera and DPTF drivers belong to their respective vendors.

Not affiliated with Google, CoolStar or any hardware vendor. Putting Windows on a Chromebook carries a bricking risk, and touching the firmware is the dangerous part. Installing drivers is reversible; the uninstall script removes both the drivers and the trusted root.
