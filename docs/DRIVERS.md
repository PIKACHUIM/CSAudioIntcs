# Driver payload

Generated from the `.inf` files under `drivers/` — run `python tools/gen-driver-docs.py`
after adding or updating a package. Every entry is a plain `INF + SYS + CAT` set
(a few also carry firmware blobs); nothing here is an archive or a self-extracting exe.

Install order is defined in [`installer/payload.list`](../installer/payload.list);
it is the same file the installer and uninstaller iterate over.

| | |
|---|---|
| packages | 37 |
| hardware IDs | 198 |

## Audio controllers

`drivers/audio/` — CoolStar audio stacks: **AVS** (`csaudiosstavs`) for Skylake / Kaby Lake / Braswell / Apollo Lake, **SOF** (`csaudiointcsof`) for Gemini Lake and newer, and **AMD SOF** (`csaudioamdsof`) for AMD Chromebooks.

| package | driver version | provider | class | files |
|---|---|---|---|---|
| `csaudioamdsof` | 03/10/2024,1.0.1.0 | CoolStar | MEDIA | 5 |
| `csaudiointcsof` | 04/23/2024,1.0.4.0 | CoolStar | MEDIA | 8 |
| `csaudiosstavs` | 11/07/2023,1.0.4.0 | CoolStar | MEDIA | 7 |
| `ssm4567` | 11/8/2026,1.0.1 | CoolStar | Media | 3 |

<details><summary><code>csaudioamdsof</code></summary>

```
CSAUDIO\ACP&CLTR_REV_6F
CSAUDIO\ACP&CLTR_REV_01
```

</details>

<details><summary><code>csaudiointcsof — 9 IDs</code></summary>

```
CSAUDIO\ADSP&CTLR_VEN_8086&CTLR_DEV_5A98
CSAUDIO\ADSP&CTLR_VEN_8086&CTLR_DEV_3198
CSAUDIO\ADSP&CTLR_VEN_8086&CTLR_DEV_02C8
CSAUDIO\ADSP&CTLR_VEN_8086&CTLR_DEV_4DC8
CSAUDIO\ADSP&CTLR_VEN_8086&CTLR_DEV_A0C8
CSAUDIO\ADSP&CTLR_VEN_8086&CTLR_DEV_51C8
CSAUDIO\ADSP&CTLR_VEN_8086&CTLR_DEV_51CC
CSAUDIO\ADSP&CTLR_VEN_8086&CTLR_DEV_54C8
CSAUDIO\ADSP&CTLR_VEN_8086&CTLR_DEV_51CA
```

</details>

<details><summary><code>csaudiosstavs</code></summary>

```
CSAUDIO\ADSP&CTLR_VEN_8086&CTLR_DEV_9D70
CSAUDIO\ADSP&CTLR_VEN_8086&CTLR_DEV_9D71
CSAUDIO\ADSP&CTLR_VEN_8086&CTLR_DEV_5A98
```

</details>

<details><summary><code>ssm4567</code></summary>

```
ACPI\INT343B
```

</details>

## Audio codecs

`drivers/codec/` — Headset / speaker codecs sitting behind the audio controller.

| package | driver version | provider | class | files |
|---|---|---|---|---|
| `croseccodec` | 04/17/2024,1.0.1.0 | CoolStar | Media | 3 |
| `cs42l42` | 03/22/2023,1.0.0.0 | CoolStar | Media | 3 |
| `da7219` | 11/07/2023,1.0.1.0 | CoolStar | Media | 3 |
| `nau8825` | 11/07/2023,1.0.1.0 | CoolStar | Media | 3 |
| `nau88l25rvp` | 11/30/2015,17.51.14.673 | NuvotonIgx | MEDIA | 4 |
| `rt5682` | 10/09/2023,1.0.2.0 | CoolStar | Media | 3 |
| `rt5682s` | 10/09/2023,1.0.1.0 | CoolStar | Media | 3 |

<details><summary><code>croseccodec</code></summary>

```
ACPI\GOOG0013
```

</details>

<details><summary><code>cs42l42</code></summary>

```
ACPI\10134242
```

</details>

<details><summary><code>da7219</code></summary>

```
ACPI\DLGS7219
```

</details>

<details><summary><code>nau8825</code></summary>

```
ACPI\10508825
```

</details>

<details><summary><code>nau88l25rvp</code></summary>

```
ACPI\10508825
```

</details>

<details><summary><code>rt5682</code></summary>

```
ACPI\10EC5682
```

</details>

<details><summary><code>rt5682s</code></summary>

```
ACPI\RTL5682
```

</details>

## Speaker amplifiers

`drivers/amp/` — I2S speaker amplifiers. Most have no ACPI node of their own and are bound through the audio controller instead.

| package | driver version | provider | class | files |
|---|---|---|---|---|
| `max98357a` | 02/26/2024,1.0.3.0 | CoolStar | Media | 3 |
| `max98390` | 02/26/2024,1.0.1.0 | CoolStar | Media | 15 |
| `opengmaxcodec` | 02/26/2024,1.0.1.0 | CoolStar | Media | 3 |
| `rt1011` | 02/26/2024,1.0.1.0 | CoolStar | Media | 3 |
| `rt1015` | 10/09/2023,1.0.1.0 | CoolStar | Media | 3 |
| `rt1019` | 10/09/2023,1.0.0.0 | CoolStar | Media | 3 |

<details><summary><code>max98357a</code></summary>

```
ACPI\MX98357A
ACPI\MX98360A
ACPI\RTL1015
ACPI\RTL1019
```

</details>

<details><summary><code>max98390</code></summary>

```
ACPI\MX98390
```

</details>

<details><summary><code>opengmaxcodec</code></summary>

```
ACPI\MX98927
ACPI\MX98373
```

</details>

<details><summary><code>rt1011</code></summary>

```
ACPI\10EC1011
```

</details>

<details><summary><code>rt1015</code></summary>

```
ACPI\10EC1015
```

</details>

<details><summary><code>rt1019</code></summary>

```
ACPI\10EC1019
```

</details>

## Platform / EC bus

`drivers/bus/` — ChromeOS embedded controller, I2C, SPI, PMC, Type-C and HDA glue. Install these before codecs and amplifiers.

| package | driver version | provider | class | files |
|---|---|---|---|---|
| `acpbus` | 11/06/2023,1.0.0.0 | CoolStar | System | 3 |
| `cbtable` | 04/22/2024,1.0.1.0 | CoolStar | System | 3 |
| `crosecbus` | 05/03/2024,2.1.0.0 | CoolStar | System | 3 |
| `croseci2c` | 04/23/2024,1.0.2.0 | CoolStar | System | 3 |
| `crosectypec` | 04/17/2024,1.0.1.0 | CoolStar | USB | 3 |
| `iaLPSS2_SPI` | 08/09/2016,30.100.1633.03 | Intel Corporation | System | 3 |
| `iaLPSS2_UART2` | 03/05/2021,5.123.1.1023 | Intel Corporation | System | 3 |
| `intelpmc` | 03/22/2023,1.0.0.0 | CoolStar | System | 3 |
| `inteltcss` | 03/22/2023,1.0.0.0 | CoolStar | USB | 3 |
| `sklhdaudbus` | 05/05/2024,1.0.5.0 | CoolStar | System | 3 |

<details><summary><code>acpbus</code></summary>

```
PCI\VEN_1022&DEV_15E2&REV_6F
PCI\VEN_1022&DEV_15E2&REV_01
```

</details>

<details><summary><code>cbtable</code></summary>

```
ACPI\BOOT0000
```

</details>

<details><summary><code>crosecbus</code></summary>

```
ACPI\GOOG0004
```

</details>

<details><summary><code>croseci2c</code></summary>

```
ACPI\GOOG0012
```

</details>

<details><summary><code>crosectypec</code></summary>

```
ACPI\GOOG0003
ACPI\GOOG0014
```

</details>

<details><summary><code>iaLPSS2_SPI — 6 IDs</code></summary>

```
PCI\VEN_8086&DEV_9D29
PCI\VEN_8086&DEV_9D2A
PCI\VEN_8086&DEV_A129
PCI\VEN_8086&DEV_A12A
PCI\VEN_8086&DEV_A2A9
PCI\VEN_8086&DEV_A2AA
```

</details>

<details><summary><code>iaLPSS2_UART2 — 75 IDs</code></summary>

```
PCI\VEN_8086&DEV_7A28
PCI\VEN_8086&DEV_7A29
PCI\VEN_8086&DEV_7A47
PCI\VEN_8086&DEV_7AA8
PCI\VEN_8086&DEV_7AA9
PCI\VEN_8086&DEV_7AFE
PCI\VEN_8086&DEV_7ADC
ACPI\INT3434
ACPI\INT3435
PCI\VEN_8086&DEV_0ABC
PCI\VEN_8086&DEV_0ABE
PCI\VEN_8086&DEV_0AC0
PCI\VEN_8086&DEV_0AEE
PCI\VEN_8086&DEV_1ABC
PCI\VEN_8086&DEV_1ABE
PCI\VEN_8086&DEV_1AC0
PCI\VEN_8086&DEV_1AEE
PCI\VEN_8086&DEV_5ABC
PCI\VEN_8086&DEV_5ABE
PCI\VEN_8086&DEV_5AC0
PCI\VEN_8086&DEV_5AEE
PCI\VEN_8086&DEV_9DA8
PCI\VEN_8086&DEV_9DA9
PCI\VEN_8086&DEV_9DC7
PCI\VEN_8086&DEV_A328
PCI\VEN_8086&DEV_A329
PCI\VEN_8086&DEV_A347
PCI\VEN_8086&DEV_02A8
PCI\VEN_8086&DEV_02A9
PCI\VEN_8086&DEV_02C7
PCI\VEN_8086&DEV_06A8
PCI\VEN_8086&DEV_06A9
PCI\VEN_8086&DEV_06C7
PCI\VEN_8086&DEV_4B28
PCI\VEN_8086&DEV_4B29
PCI\VEN_8086&DEV_4B4D
PCI\VEN_8086&DEV_31BC
PCI\VEN_8086&DEV_31BE
PCI\VEN_8086&DEV_31C0
PCI\VEN_8086&DEV_31EE
ACPI\INT33C4
ACPI\INT33C5
PCI\VEN_8086&DEV_34A8
PCI\VEN_8086&DEV_34A9
PCI\VEN_8086&DEV_34C7
PCI\VEN_8086&DEV_3DA8
PCI\VEN_8086&DEV_3DA9
PCI\VEN_8086&DEV_3DAF
PCI\VEN_8086&DEV_38A8
PCI\VEN_8086&DEV_38A9
PCI\VEN_8086&DEV_38C7
PCI\VEN_8086&DEV_A2A8
PCI\VEN_8086&DEV_A2A9
PCI\VEN_8086&DEV_A2C7
PCI\VEN_8086&DEV_9D27
PCI\VEN_8086&DEV_9D28
PCI\VEN_8086&DEV_9D66
PCI\VEN_8086&DEV_98A8
PCI\VEN_8086&DEV_98A9
PCI\VEN_8086&DEV_98C7
PCI\VEN_8086&DEV_A127
PCI\VEN_8086&DEV_A128
PCI\VEN_8086&DEV_A166
PCI\VEN_8086&DEV_A2A7
PCI\VEN_8086&DEV_A2E6
PCI\VEN_8086&DEV_A0A8
PCI\VEN_8086&DEV_A0A9
PCI\VEN_8086&DEV_A0C7
PCI\VEN_8086&DEV_A0DA
PCI\VEN_8086&DEV_A0DB
PCI\VEN_8086&DEV_A0DC
PCI\VEN_8086&DEV_A0DD
PCI\VEN_8086&DEV_43A8
PCI\VEN_8086&DEV_43A9
PCI\VEN_8086&DEV_43A7
```

</details>

<details><summary><code>intelpmc</code></summary>

```
ACPI\INTC1026
```

</details>

<details><summary><code>inteltcss</code></summary>

```
ACPI\INTC1072
ACPI\INTC1079
ACPI\INTC107A
ACPI\INTC105C
```

</details>

<details><summary><code>sklhdaudbus — 14 IDs</code></summary>

```
PCI\VEN_8086&DEV_9D70&CC_0401
PCI\VEN_8086&DEV_9D71&CC_0401
PCI\VEN_8086&DEV_5A98&CC_0401
PCI\VEN_8086&DEV_3198&CC_0401
PCI\VEN_8086&DEV_02C8&CC_0401
PCI\VEN_8086&DEV_4DC8&CC_0401
PCI\VEN_8086&DEV_A0C8&CC_0401
PCI\VEN_8086&DEV_51C8&CC_0401
PCI\VEN_8086&DEV_51CC&CC_0401
PCI\VEN_8086&DEV_54C8&CC_0401
PCI\VEN_8086&DEV_51CA&CC_0401
PCI\VEN_8086&DEV_7E28&CC_0401
PCI\VEN_8086&DEV_7728&CC_0401
PCI\VEN_8086&DEV_A828&CC_0401
```

</details>

## Input devices

`drivers/input/` — Touchpads (one package per controller vendor) and the fingerprint reader.

| package | driver version | provider | class | files |
|---|---|---|---|---|
| `crosfingerprint` | 05/03/2024,1.0.0.0 | CoolStar | Biometric | 4 |
| `crostouchpad-atmel` | 08/03/2017,4.1 | CoolStar | HIDClass | 3 |
| `crostouchpad-cypress` | 08/03/2017,4.1 | CoolStar | HIDClass | 3 |
| `crostouchpad-cypress-smbus` | 08/03/2017,4.1 | CoolStar | HIDClass | 3 |
| `crostouchpad-elan` | 08/03/2017,4.1 | CoolStar | HIDClass | 3 |
| `crostouchpad-synaptics` | 08/03/2017,4.1 | CoolStar | HIDClass | 3 |

<details><summary><code>crosfingerprint</code></summary>

```
ACPI\CRFP0000
ACPI\CRFP0001
ACPI\PRP0000
ACPI\PRP0001
```

</details>

<details><summary><code>crostouchpad-atmel</code></summary>

```
ACPI\ATML0000
```

</details>

<details><summary><code>crostouchpad-cypress</code></summary>

```
ACPI\CYAP0000
```

</details>

<details><summary><code>crostouchpad-cypress-smbus</code></summary>

```
PCI\VEN_8086&DEV_1E22&SUBSYS_04B418D1
PCI\VEN_8086&DEV_1C22&SUBSYS_04B418D1
```

</details>

<details><summary><code>crostouchpad-elan</code></summary>

```
ACPI\ELAN0000
```

</details>

<details><summary><code>crostouchpad-synaptics</code></summary>

```
ACPI\SYNA0000
```

</details>

## Cameras

`drivers/camera/` — Intel IPU / MIPI camera packages.

| package | driver version | provider | class | files |
|---|---|---|---|---|
| `iaisp64_46xx_ADL` | 03/13/2025,63.22000.10.18461 | Intel Corporation | System | 4 |
| `ov5675` | 08/11/2023,63.22000.0.13186 | Intel Corporation | System | 12 |

<details><summary><code>iaisp64_46xx_ADL</code></summary>

```
PCI\VEN_8086&DEV_465D
PCI\VEN_8086&DEV_462E
PCI\VEN_8086&DEV_A75D
```

</details>

<details><summary><code>ov5675</code></summary>

```
ACPI\OVTI5675
ACPI\VEN_OVTI&DEV_5675&SUBSYS_MSHW0170
```

</details>

## System

`drivers/system/` — Intel Dynamic Tuning (DPTF) and the Quanta placeholder driver.

| package | driver version | provider | class | files |
|---|---|---|---|---|
| `dptf_acpi` | 06/14/2019,8.6.10401.9906 | Intel | System | 3 |
| `gnodriver` | 04/22/2009,1.0.0.2 | Quanta | System | 2 |

<details><summary><code>dptf_acpi — 17 IDs</code></summary>

```
ACPI\INT3400
ACPI\INT3401
ACPI\INT3402
ACPI\INT3403
ACPI\INT3404
ACPI\INT3405
ACPI\INT3406
ACPI\INT3407
ACPI\INT3408
ACPI\INT3409
ACPI\INT340A
ACPI\INT340B
ACPI\INT340C
ACPI\INT340D
ACPI\INT343D
ACPI\INT3530
ACPI\INT3532
```

</details>

<details><summary><code>gnodriver — 27 IDs</code></summary>

```
ACPI\VEN_BOOT&DEV_0000&SUBSYS_1AE0006B
ACPI\VEN_PRP&DEV_0001&SUBSYS_1AE0006B
ACPI\VEN_GGL&DEV_0001&SUBSYS_1AE0006B
ACPI\VEN_GOOG&DEV_0002&SUBSYS_1AE0006B
ACPI\VEN_GOOG&DEV_0003&SUBSYS_1AE0006B
ACPI\VEN_GOOG&DEV_0004&SUBSYS_1AE0006B
ACPI\VEN_GOOG&DEV_0005&SUBSYS_1AE0006B
ACPI\VEN_GOOG&DEV_0006&SUBSYS_1AE0006B
ACPI\VEN_GOOG&DEV_0008&SUBSYS_1AE0006B
ACPI\VEN_GOOG&DEV_9999&SUBSYS_1AE0006B
ACPI\VEN_BOOT&DEV_0000&SUBSYS_1AE0006C
ACPI\VEN_PRP&DEV_0001&SUBSYS_1AE0006C
ACPI\VEN_GOOG&DEV_0002&SUBSYS_1AE0006C
ACPI\VEN_GOOG&DEV_0003&SUBSYS_1AE0006C
ACPI\VEN_GOOG&DEV_0004&SUBSYS_1AE0006C
ACPI\VEN_GOOG&DEV_0005&SUBSYS_1AE0006C
ACPI\VEN_GOOG&DEV_0006&SUBSYS_1AE0006C
ACPI\VEN_GOOG&DEV_0008&SUBSYS_1AE0006C
ACPI\VEN_GOOG&DEV_9999&SUBSYS_1AE0006C
ACPI\GGL0001
ACPI\GOOG0002
ACPI\GOOG0003
ACPI\GOOG0004
ACPI\GOOG0005
ACPI\GOOG0006
ACPI\GOOG0008
ACPI\GOOG9999
```

</details>

## Shipped but not installed by default

| package | why |
|---|---|
| `nau88l25rvp` | Nuvoton reference codec, binds the same `ACPI\10508825` as `nau8825` — mutually exclusive |
| `gnodriver` | placeholder INF only, ships no `.sys` |

Both folders stay in the tree so the payload is complete; they are simply excluded
from `payload.list`.

