# ChromeBook Windows 声卡驱动开心版

本项目提供适用于在 Windows 上运行的 Chromebook 的声卡驱动程序。基于 CoolStar 的付费驱动进行修改（开心版），移除了授权限制，直接安装即可使用。

## ✨ 特性

- **广泛的硬件支持**：支持 AMD 和 Intel 处理器，覆盖 6 代至 14 代 Chromebook/Chromebox。
- **一键安装**：提供自动化安装脚本和安装包，简化驱动配置过程。
- **解锁限制**：集成破解版驱动，无需付费授权。
- **完整组件**：包含核心音频驱动、辅助总线驱动及多种编解码器驱动。

## 📦 包含的驱动组件

本项目集成了以下关键驱动组件：

### 核心音频驱动 (Cracked)
- **AMD SOF Audio**: `csaudioamdsof.1.0.1`
- **Intel SOF Audio**: `csaudiointsof.1.0.4`
- **Intel SST Audio**: `csaudiosstavs.1.0.4`

### 编解码器与放大器 (Codecs & Amps)
- **Cirrus Logic**: `cs42l42`
- **Dialog**: `da7219`
- **Maxim**: `max98357a`, `max98390`
- **Nuvoton**: `nau8825`
- **Realtek**: `rt1011`, `rt1015`, `rt1019`, `rt5682`, `rt5682s`
- **Other**: `opengmaxcodec`

### 总线与系统设备 (System & Bus)
- **Buses**: `acpbus`, `crosecbus`, `croseci2c`, `crosectypec`, `sklhdaudbus`
- **Intel Components**: `iaLPSS2_SPI`, `iaLPSS2_UART2`, `intelpmc`, `inteltcss`
- **Other**: `cbtable`, `crosfingerprint` (指纹识别支持)

## 🚀 安装说明

1. 下载最新发布的安装包 (`ChromeBook-CoolStar-Audio-Drivers.exe`)。
2. 运行安装程序。
3. 安装过程中会自动部署证书并安装所需驱动。
4. 安装完成后，建议重启系统以确保所有驱动正常加载。

## 📂 项目结构

- `CrackFile/`: 存放破解版驱动及原始驱动文件。
- `Installer/`: 安装程序源码 (Inno Setup) 及构建资源。
  - `Drivers/`: 待打包的驱动文件集合。
  - `Release/`: 构建生成的安装包。
- `Licensing/`: 包含授权相关的 SDK 和工具（主要用于研究或构建过程）。

## ⚠️ 免责声明

本项目仅供学习和研究使用，请勿用于商业用途。如果您觉得 CoolStar 的驱动对您有帮助，请考虑支持正版。
