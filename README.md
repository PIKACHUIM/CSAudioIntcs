<div align="center">

# ChromeBook/Box Windows 专用驱动包

Chromebook/box 专用驱动包合集，包括数十种不常见的Chrome设备驱动

![platform](docs/badges/platform.svg) ![drivers](docs/badges/drivers.svg) ![installer](docs/badges/installer.svg) ![license](docs/badges/license.svg)

[下载驱动](../../releases) · [驱动清单](docs/DRIVERS.md) · [修补记录](docs/PATCH-ANALYSIS.md) · [版本差异](docs/UPSTREAM.md) · [许可证](LICENSE)

[简体中文](README.md) · [English](README.en.md) · [繁體中文](README.zh-TW.md) · [日本語](README.ja.md)

</div>

---

## 项目简介

ChromeBook / ChromeBox 专用驱动包，包括36个驱动，支持171个硬件

本项目针对不同设备做了兼容适配，并支持了音频等专属硬件设置驱动

| 项 | 内容 |
|---|---|
| 驱动 | 36 个，支持硬件 171 个|
| 设备 | 音频控制器、codec、功放、触摸板、触摸屏、指纹、摄像头、EC 总线、DPTF |
| 平台 | Intel Skylake 到 Alder Lake，AMD Rembrandt / Phoenix |
| 文件 | 裸 `INF` + `SYS` + `CAT`，没有压缩包和自解压安装器 |
| 工具 | Microsoft DPInst 2.1、Inno Setup 6 |

## 环境需求
1. Windows 10 X64 1607 及以上版本
2. Windows 11 >= 26年7月更新，需关闭"代码完整性异常策略的交叉证书":
    ```pwsh
    CiTool.exe --remove-policy "{8F9CB695-5D48-48D6-A329-7202B44607E3}"
    ```


以下条件是**不必要**的：
  1. **无需关闭**SecureBoot
  2. **无需开启**测试模式
  3. **无需关闭**"Microsoft 易受攻击的驱动程序阻止列表"

## 风险提示
1. Chromebook 安装 Windows 有变砖风险
2. 本驱动未经充分的测试，使用风险自担


## 安装方式

1. 在 [Releases](../../releases) 下载 `ChromeBook-Windows-Driver-<版本>-x64.exe`
2. 右键以管理员身份运行，安装完成后重启

## 安装过程

1. 安装程序会先导签名根，包括补全Windows系统内置CA
2. 导入测试证书和受信任交叉CA根证书，用于签名驱动
3. 按顺序逐个调 DPInst，同版本的驱动已经装过就跳过

只支持 x64，a64 和 x32 Windows 没有对应的驱动包。

## 项目结构

```
.
├── drivers/             驱动本体，按 audio codec amp bus input camera system 分类
├── installer/           Install.iss（打包脚本）、payload.list（安装顺序）
├── scripts/             preflight、安装、卸载、导根证书
├── certs/               签名根安装器
├── reference/originals/ CoolStar 官方原包，用来做差异比对
├── src/ssm4567/         SSM4567 功放驱动源码
├── tools/               DPInst、文档生成、二进制比对
└── docs/                驱动清单、修补记录、签名状态、版本差异
```

## 驱动清单

完整版本号和每一条 HWID 在 [docs/DRIVERS.md](docs/DRIVERS.md)，那份文件由 `python tools/gen-driver-docs.py` 从 INF 生成，别手改。

| 分类 | 驱动 | 版本 |
|---|---|---|
| 音频 | `csaudiosstavs` | 1.0.4.0 |
| 音频 | `csaudiointcsof` | 1.0.5.0 |
| 音频 | `csaudioamdsof` | 1.0.1.0 |
| 音频 | `ssm4567` | 1.0.1 |
| Codec | `nau8825` | 1.0.1.0 |
| Codec | `nau88l25rvp` | 17.51.14.673 |
| Codec | `cs42l42` | 1.0.0.0 |
| Codec | `da7219` | 1.0.1.0 |
| Codec | `rt5682` | 1.0.2.0 |
| Codec | `rt5682s` | 1.0.1.0 |
| Codec | `croseccodec` | 1.0.1.0 |
| 功放 | `max98357a` | 1.0.3.0 |
| 功放 | `max98390` | 1.0.1.0 |
| 功放 | `opengmaxcodec` | 1.0.1.0 |
| 功放 | `rt1011` | 1.0.1.0 |
| 功放 | `rt1015` | 1.0.1.0 |
| 功放 | `rt1019` | 1.0.0.0 |
| 总线与系统 | `acpbus` | 1.0.0.0 |
| 总线与系统 | `cbtable` | 1.0.1.0 |
| 总线与系统 | `crosecbus` | 2.1.0.0 |
| 总线与系统 | `croseci2c` | 1.0.2.0 |
| 总线与系统 | `crosectypec` | 1.0.1.0 |
| 总线与系统 | `sklhdaudbus` | 1.0.5.0 |
| 总线与系统 | `intelpmc` | 1.0.0.0 |
| 总线与系统 | `inteltcss` | 1.0.0.0 |
| 总线与系统 | `iaLPSS2_SPI` | 30.100.1633.03 |
| 总线与系统 | `iaLPSS2_UART2` | 5.123.1.1023 |
| 总线与系统 | `dptf_acpi` | 8.6.10401.9906 |
| 输入与摄像头 | `crostouchpad-synaptics` | 4.1 |
| 输入与摄像头 | `crostouchpad-cypress` | 4.1 |
| 输入与摄像头 | `crostouchpad-cypress-smbus` | 4.1 |
| 输入与摄像头 | `crostouchpad-atmel` | 4.1 |
| 输入与摄像头 | `crostouchpad-elan` | 4.1 |
| 输入与摄像头 | `crosfingerprint` | 1.0.0.0 |
| 输入与摄像头 | `ov5675` | 63.22000.0.13186 |
| 输入与摄像头 | `iaisp64_46xx_ADL` | 63.22000.10.18461 |

`nau88l25rvp` 和 `nau8825` 抢同一个硬件 ID `ACPI\10508825`，默认用 CoolStar 那份，所以它随仓库分发但不进安装清单。安装和卸载都以 [installer/payload.list](installer/payload.list) 为准。




## 构建教程

使用 [Inno Setup 6](https://jrsoftware.org/isinfo.php) 构建安装包。

```bat
:: 可选，检查 payload 有没有缺文件
powershell -ExecutionPolicy Bypass -File scripts\preflight.ps1

ISCC.exe installer\Install.iss
ISCC.exe /DMyAppVersion=26.11.26 installer\Install.iss
```


## 开源许可

本项目基于[MIT 许可](LICENSE)分发。

驱动二进制的版权归各原厂，不在 MIT 范围内：

| 组件 | 授权 |
|---|---|
| CoolStar 音频驱动 | 商业授权，按机器计费 |
| `src/ssm4567` | `src/ssm4567/LICENSE.txt` |
| `tools/dpinst/dpinst.exe` | Microsoft，随 WDK 分发 |
| ChromeOS EC、触摸板、摄像头、DPTF 等 | 各自厂商 |

MIT 只覆盖本仓库新增的部分，不改变上面这些组件的授权状态。

本仓库和 Google、CoolStar 以及任何硬件厂商都没有隶属关系。
