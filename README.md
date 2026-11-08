<div align="center">

# ChromeBook CoolStar 驱动包

集合数十种 ChromeBook 专用驱动，包括声卡、触摸板等驱动

[简体中文](README.md) · [English](README.en.md) · [繁體中文](README.zh-TW.md) · [日本語](README.ja.md)

![platform](docs/badges/platform.svg) ![drivers](docs/badges/drivers.svg) ![installer](docs/badges/installer.svg) ![license](docs/badges/license.svg)

</div>

[项目简介](#项目简介) · [快速安装](#快速安装) · [驱动列表](#驱动列表) · [修补原理](#修补原理) · [许可证](#许可证)

---

## 项目简介

本仓库是 ChromeBook 平台在 Windows 下使用的驱动集合，按功能重新分类组织，共 37 个驱动包、198 个硬件 ID。

| 项 | 内容 |
|---|---|
| 驱动数量 | 37 个包，默认安装 35 个 |
| 硬件 ID | 198 个 |
| 覆盖范围 | 音频（AVS / SOF）、codec、功放、触摸板、触摸屏、指纹、摄像头、EC 总线、芯片组配套组件 |
| 目标平台 | Intel Skylake 至 Alder Lake，AMD Rembrandt / Phoenix |
| 文件形式 | 裸 `INF` + `SYS` + `CAT`，无压缩包与自解压安装器 |
| 安装工具 | Microsoft DPInst 2.1、Inno Setup 6 |

驱动取自 CoolStar 与 ChromeOS 各厂商的公开驱动包，版本与硬件 ID 保持原样，仅调整了目录结构和安装顺序。安装过程中执行的外部程序只有两个：`tools/dpinst/dpinst.exe` 和 `certs/pikachu-certificate-install.exe`。

上游音频驱动为按机器授权的商业软件。本仓库面向已持有授权的硬件，不包含授权文件、生成器或激活工具。

```
.
├── drivers/            audio codec amp bus input camera system
├── installer/          Install.iss、payload.list（安装顺序）
├── scripts/            preflight、install、uninstall、trust-certs
├── certs/              签名根安装器
├── reference/originals/  CoolStar 官方原包，仅用于差异比对
├── src/ssm4567/        ADI SSM4567 功放驱动源码
├── tools/              DPInst、文档生成、二进制比对
├── docs/               DRIVERS.md、PATCH-ANALYSIS.md、VENDOR.md
└── vendor/             本地暂存，git 忽略
```

## 快速安装

### 使用发布包

1. 在 [Releases](../../releases) 下载 `ChromeBook-CoolStar-Audio-Driver-<版本>-x64.exe`
2. 右键以管理员身份运行
3. 安装完成后重启

安装程序会先导入签名根，再按 `payload.list` 逐个调用 DPInst，已存在的同版本驱动不会重复安装。

### 从源码构建

依赖只有 [Inno Setup 6](https://jrsoftware.org/isinfo.php)，不需要 WDK 和 Visual Studio。

```bat
:: 可选，校验 payload 完整性
powershell -ExecutionPolicy Bypass -File scripts\preflight.ps1

:: 构建，产物在 dist\
ISCC.exe installer\Install.iss
ISCC.exe /DMyAppVersion=1.0.5 installer\Install.iss
```

CI 在 `.github/workflows/release.yml`，手动触发时填写版本号，或推送 `v*` 标签。流程为 preflight、构建、生成 `SHA256SUMS.txt`、创建 Release。

### 单独安装某个驱动

适用于只缺一路设备的情况。先导入签名根，再指定 INF。

```bat
:: 管理员 PowerShell
certs\pikachu-certificate-install.exe /VERYSILENT

pnputil /add-driver drivers\audio\ssm4567\ssm4567.inf /install
```

### 音频栈选择

三套音频架构互不兼容，硬件 ID 不重叠，需按平台选择。

| 平台 | 驱动包 | 服务名 |
|---|---|---|
| Skylake、Kaby Lake、Amber Lake、Braswell、Apollo Lake | `csaudiosstavs` | `CsAudioSSTAVS` |
| Gemini Lake、Jasper Lake、Ice Lake、Tiger Lake、Alder Lake 及更新 | `csaudiointcsof` | `CsAudioIntcSOF` |
| AMD：Rembrandt、Phoenix 等 | `csaudioamdsof` | `CsAudioAmdSOF` |
| glados、chell 等板卡的扬声器功放 | `ssm4567` | `ssm4567` |

完整安装包含全部三套，DPInst 只绑定硬件 ID 匹配的一项。`ssm4567` 为独立功放驱动，与音频栈并行安装。

## 驱动列表

完整版本号与 HWID 见 [docs/DRIVERS.md](docs/DRIVERS.md)，该文件由 `python tools/gen-driver-docs.py` 从 INF 生成。

### 音频

| 包 | 版本 | 说明 |
|---|---|---|
| `csaudiosstavs` | 1.0.4.0 | AVS 架构，Skylake 至 Apollo Lake |
| `csaudiointcsof` | 1.0.4.0 | SOF 架构，Gemini Lake 及更新 |
| `csaudioamdsof` | 1.0.1.0 | AMD 平台 SOF |
| `ssm4567` | 1.0.1 | ADI I2S 功放，glados 系列板卡 |

### Codec

`nau8825` · `nau88l25rvp` · `cs42l42` · `da7219` · `rt5682` · `rt5682s` · `croseccodec`

### 功放

`max98357a` · `max98390` · `opengmaxcodec` · `rt1011` · `rt1015` · `rt1019`

### 总线与系统

`acpbus` · `cbtable` · `crosecbus` · `croseci2c` · `crosectypec` · `sklhdaudbus` · `intelpmc` · `inteltcss` · `iaLPSS2_SPI` · `iaLPSS2_UART2` · `dptf_acpi` · `gnodriver`

### 输入与摄像头

`crostouchpad-{synaptics,cypress,cypress-smbus,atmel,elan}` · `crosfingerprint` · `ov5675` · `iaisp64_46xx_ADL`

两个包随仓库分发但不默认安装：`nau88l25rvp` 与 `nau8825` 使用同一硬件 ID `ACPI\10508825`，默认启用 CoolStar 版本；`gnodriver` 不含 `SYS`，仅作 INF 占位。

安装与卸载均以 [installer/payload.list](installer/payload.list) 为唯一清单。

## 修补原理

CoolStar 音频驱动在 `DriverEntry` 中读取授权文件、验证签名，并与 SMBIOS 中的板卡代号比对。校验失败时返回 NTSTATUS `0xC000026A`，调用方收到该状态码后不再启动 ADSP。ADSP 未启动则 I2S 无时钟输出，功放保持关断。

原始二进制中可直接读到以下字符串：

```
AVS: License not found. Please install it to C:\Windows\system32\drivers\avs_signedlicense.bin
AVS: License too small. Make sure the license is signed.
SystemSKU      Chell
```

因此设备枚举、端点创建和电平表均正常，扬声器无声。

### 补丁位置

三个音频栈分别修改 5、12、12 处，合计 153 字节，只有两种形态。

**错误分支直接返回**（4 处 / 24 字节）：

```asm
mov eax, 0xC000026A    ; 校验失败时写入返回值
jmp 0x140004970        ; 跳到函数收尾
```

**三元表达式收尾**（25 处 / 129 字节）：

```asm
sbb  ebx, ebx          ; 0 或 -1，取决于上一处比较
not  ebx               ; 取反
and  ebx, 0xC000026A   ; 条件成立得到失败码，否则得到 0
```

这是 `return cond ? STATUS_X : STATUS_SUCCESS;` 的编译结果。补丁把立即数改成 0
（`and ebx, 0`），条件不再影响返回值。原指令 6 字节、新指令 3 字节，剩余用 `nop` 补齐，
后续偏移不变。

逐点的文件偏移、虚拟地址、原始与修补字节、前后指令，见 [docs/PATCH-SITES.md](docs/PATCH-SITES.md)。

INF、HWID 表、服务安装段与 DSP 固件均未修改。`CsAudioCallbackAPI` 回调保持注册，`ssm4567` 依赖该回调注册 Speaker 端点。

### 签名

改一个字节，原有的 WHQL 签名就失效，修补后的二进制必须重新生成 catalog 并签名。仓库里的音频包由第三方完成这一步，其 catalog 的时间戳链终止于一条公开根不信任的证书，因此在干净系统上 `pnputil` 会拒绝：

```
0xE0000242  尚未建立对 Authenticode(TM)签名的目录的发布者信任
```

`certs/pikachu-certificate-install.exe` 用于把所需的根写入 `LocalMachine\Root` 与 `LocalMachine\TrustedPublisher`，两个存储缺一不可。安装器会在装驱动之前自动执行。

要自己给改过的驱动重签名，流程（重建 catalog、签名、导入信任根、验证）见 [FakeSign](https://github.com/PIKACHUIM/FakeSign)。仓库不附带任何代码签名证书。

37 个包各自的签名来源与是否需要导入根，见 [docs/SIGNATURES.md](docs/SIGNATURES.md)。其中 7 个需要导入，其余 30 个是 Microsoft WHQL 或公共根的第三方签名。

另有一个容易误判的地方：`Get-AuthenticodeSignature` 返回 `Valid` 不代表 `pnputil` 接受该包，前者只校验 PKCS#7 结构与证书链，后者还要过 setupapi 的发布者信任检查。另外 INF 不可作任何字节修改，包括换行符，因为 catalog 里记录着它的哈希。

逐字节差异与反汇编见 [docs/PATCH-ANALYSIS.md](docs/PATCH-ANALYSIS.md)；29 处补丁点的偏移、字节与前后指令见 [docs/PATCH-SITES.md](docs/PATCH-SITES.md)。比对工具为 `tools/compare-drivers.py`。

## 许可证

本仓库的脚本、文档、构建配置以 MIT 许可证发布，全文见 [LICENSE](LICENSE)。

仓库内的驱动二进制版权归各原厂所有，不适用 MIT：

| 组件 | 授权 |
|---|---|
| CoolStar 音频驱动 | 商业授权，按机器计费，见 <https://coolstar.org/chromebook/> |
| `src/ssm4567` | CoolStar，见 `src/ssm4567/LICENSE.txt` |
| `tools/dpinst/dpinst.exe` | Microsoft，随 WDK 分发 |
| ChromeOS EC、触摸板、摄像头、DPTF 等驱动 | 各自厂商 |

MIT 仅覆盖本仓库新增内容，不改变上述组件的授权状态。本仓库与 Google、CoolStar 及任何硬件厂商无隶属关系。Chromebook 安装 Windows 存在变砖风险，修改固件风险更高；仅安装驱动可逆，卸载脚本会同时移除驱动与导入的根证书。
