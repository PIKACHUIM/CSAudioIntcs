# Chromebook 专有驱动补充包

从实际运行的 Chromebook 上导出的 DriverStore 驱动，补充主驱动包中缺失的
Chromebook 专有组件。与 `chromebookwin.1.0.0-drivers/` 互补，不重复前者已有的条目。

导出方式：`pnputil /export-driver * <dir>`，保留原始 `.inf / .sys / .cat`，已移除编译缓存 `.PNF`。

---

## 目录内容

### 触摸板（CoolStar，v4.1，2017-08-03）

五种触摸板各一份，INF 与 SYS 均不相同，按硬件选用。

| 目录 | 设备 | 硬件 ID |
|---|---|---|
| `crostouchpad-synaptics/` | Chromebook Synaptics RMI Touchpad | `ACPI\SYNA0000` |
| `crostouchpad-cypress/` | Chromebook Cypress v3 Touchpad | `ACPI\CYAP0000` |
| `crostouchpad-atmel/` | Chromebook Atmel MaxTouch Touchpad | `ACPI\ATML0000` |
| `crostouchpad-cypress-smbus/` | Chromebook Cypress v3 SMBus Touchpad | `PCI\VEN_8086&DEV_1E22&SUBSYS_04B418D1`<br>`PCI\VEN_8086&DEV_1C22&SUBSYS_04B418D1` |
| `crostouchpad-elan/` | Chromebook Elan Touchpad | `ACPI\ELAN0000` |

> 例如 HP Chromebook 13 G1（CHELL）用的是 ELAN，对应 `crostouchpad-elan/`。

### 音频编解码器

| 目录 | 设备 | 硬件 ID | 版本 |
|---|---|---|---|
| `nau88l25rvp/` | Nuvoton SST NAU88L25 Codec（官方参考驱动） | `ACPI\10508825` | 17.51.14.673（2015-11-30） |

这是 Nuvoton 原厂参考驱动，与本项目 `chromebookwin.1.0.0-drivers/nau8825/`（CoolStar 重写版）
是两条不同实现，可互为备选。含 `WdfCoInstaller01011.dll`，安装时需要一并复制。

### 系统设备

| 目录 | 设备 | 硬件 ID | 说明 |
|---|---|---|---|
| `gnodriver/` | Quanta gNoDriver | `ACPI\VEN_BOOT&DEV_0000&SUBSYS_1AE0006B`<br>`ACPI\VEN_PRP&DEV_0001&SUBSYS_1AE0006B`<br>`ACPI\VEN_GGL&DEV_0001&SUBSYS_1AE0006B` | 空驱动占位（无 `.sys`），用于吃掉 `GGL0001`（ChromeOS ACPI Device）等节点，避免设备管理器残留未知设备 |
| `dptf_acpi/` | Intel(R) Dynamic Tuning Manager | `ACPI\INT3400` / `INT3401` / `INT3402` | 版本 8.6.10401.9906（2019-06-14）。Chromebook 上也走这套 DPTF |

---

## 安装

### 单目录手动安装

以管理员身份执行：

```powershell
pnputil /add-driver .\<目录>\*.inf /install
```

`gnodriver` 只有 `.inf` 与 `.cat`，同样用 `pnputil /add-driver` 即可。

### 批量安装全部

```powershell
Get-ChildItem -Path . -Filter *.inf -Recurse |
    ForEach-Object { pnputil /add-driver $_.FullName /install }
```

### 注意

- 这些驱动均来自公开渠道导出的现成二进制，若目标机未关闭 Secure Boot，
  非微软签名的部分（CoolStar 系列）需要 `testsigning` 才能加载。
- `nau88l25rvp` 与 `nau8825` 功能重叠，**不要同时安装到同一设备**；
  若要切换，先卸载已有的那个。
- 触摸板驱动只装与硬件匹配的那一个，其余装不上属正常（硬件 ID 不匹配）。
