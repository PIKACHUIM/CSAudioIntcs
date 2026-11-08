# ChromeBook CoolStar 音频驱动整合包

**简体中文** | [English](README.en.md) | [繁體中文](README.zh-TW.md) | [日本語](README.ja.md)

把 Chromebook 刷成 Windows 之后，最难搞定的往往不是显卡、不是触摸板，而是**声音**。
这个仓库把让 Chromebook 发声所需的驱动打成一个安装包：CoolStar 的音频栈、
它下面挂的 codec 与功放、以及 ChromeOS 的嵌入式控制器总线、触摸板、指纹、摄像头驱动。

仓库里全部是**裸驱动文件**（`INF + SYS + CAT`，少数带固件 blob），没有压缩包、
没有自解压安装器。整棵工作树 13 MB，跑起来的外来程序只有微软的 `DPInst` 和系统自带的 `certutil`。

> 这是一个**第三方重新打包**项目。驱动本体是 CoolStar 的作品（<https://coolstar.org/chromebook/>），
> 上游是按机器授权的收费组件。详见 [声明](#声明)。

---

## 目录

- [这是什么](#这是什么)
- [快速开始](#快速开始)
- [驱动列表](#驱动列表)
- [修补原理](#修补原理)
- [仓库结构](#仓库结构)
- [开发方式](#开发方式)
- [已验证硬件](#已验证硬件)
- [声明](#声明)

---

## 这是什么

Chromebook 的音频硬件和普通笔记本完全不是一回事：

| 普通笔记本 | Chromebook |
|---|---|
| HD Audio codec 挂在 HDA 总线上，插上就出声 | 音频控制器是 Intel **AVS / SOF** 的 ADSP，codec 和功放挂在 **I2C / I2S** 上 |
| codec 由 BIOS 初始化 | codec 需要驱动通过 I2C 逐寄存器配置 |
| 功放集成在 codec 里 | 功放是独立芯片（MAX98357A / SSM4567 / RT1015…），要靠 GPIO + I2S 时钟使能 |

所以缺了驱动，现象不是"设备管理器里有个黄叹号"，而是更迷惑的一种：
**设备全绿、4 个音频端点齐全、播放时端点电平表满幅，但喇叭一点声音都没有** ——
因为 ADSP 没起来，I2S 没有时钟，功放一直处于掉电/静音状态。

这个包要解决的就是这件事。

---

## 快速开始

**直接装**：到 [Releases](../../releases) 下载最新的
`ChromeBook-CoolStar-Audio-Driver-<版本>-x64.exe`，右键以管理员身份运行，重启。

**自己编译**（需要 [Inno Setup 6](https://jrsoftware.org/isinfo.php)，不需要 WDK / VS / 编译器）：

```bat
ISCC.exe installer\Install.iss

:: 或指定版本号
ISCC.exe /DMyAppVersion=1.0.5 installer\Install.iss
```

产物落在 `dist\`。

**只装某几个驱动**（例如只补扬声器功放）：

```bat
:: 管理员 PowerShell，在解压后的目录里
certutil -addstore -f Root             certs\pikachu-testca-root.cer
certutil -addstore -f TrustedPublisher certs\pikachu-testca-root.cer
pnputil /add-driver drivers\audio\ssm4567\ssm4567.inf /install
```

---

## 驱动列表

37 个驱动包、198 个硬件 ID。完整清单（含每个包的 DriverVer、Provider、全部 HWID）
在 **[docs/DRIVERS.md](docs/DRIVERS.md)**，由 `python tools/gen-driver-docs.py` 从 INF 自动生成。

### 音频（`drivers/audio/`）

| 包 | 版本 | 适用平台 |
|---|---|---|
| `csaudiosstavs` | 1.0.4.0 | **AVS**：Skylake / Kaby Lake / Amber Lake / Braswell / Apollo Lake |
| `csaudiointcsof` | 1.0.4.0 | **SOF**：Gemini Lake / Jasper Lake / Ice Lake / Tiger Lake / Alder Lake… |
| `csaudioamdsof` | 1.0.1.0 | **AMD SOF**：Rembrandt / Phoenix 等 AMD Chromebook |
| `ssm4567` | 1.0.1 | ADI SSM4567 I2S 功放（glados / chell，即 HP Chromebook 13 G1） |

> **只装一个**。装错平台是"设备在但没声音"最常见的原因：
> AVS 与 SOF 是两套完全不同的架构，硬件 ID 不重叠，DPInst 会自动挑匹配的那个。

### Codec（`drivers/codec/`）

`nau8825` · `nau88l25rvp` · `cs42l42` · `da7219` · `rt5682` · `rt5682s` · `croseccodec`

### 功放（`drivers/amp/`）

`max98357a` · `max98390` · `opengmaxcodec` · `rt1011` · `rt1015` · `rt1019`

### 总线与系统（`drivers/bus/`、`drivers/system/`）

`acpbus` · `cbtable` · `crosecbus` · `croseci2c` · `crosectypec` · `sklhdaudbus` ·
`intelpmc` · `inteltcss` · `iaLPSS2_SPI` · `iaLPSS2_UART2` · `dptf_acpi` · `gnodriver`

### 输入与摄像头（`drivers/input/`、`drivers/camera/`）

`crostouchpad-{synaptics,cypress,cypress-smbus,atmel,elan}` · `crosfingerprint` ·
`ov5675` · `iaisp64_46xx_ADL`

### 装了但不默认启用

| 包 | 原因 |
|---|---|
| `codec/nau88l25rvp` | Nuvoton 官方参考 codec，和 `codec/nau8825` 抢同一个 `ACPI\10508825`，互斥 |
| `system/gnodriver` | 只有 INF 的占位驱动，没有 `.sys` |

安装顺序写在 [`installer/payload.list`](installer/payload.list) 里，安装器和卸载器共用这一份清单。

---

## 修补原理

这一节回答两个问题：**为什么原版装上也无声**，以及**这个包到底改了什么**。
完整证据链（逐字节 diff、反汇编、签名链对比、与 akibaPatcher 的路线对照）在
**[docs/PATCH-ANALYSIS.md](docs/PATCH-ANALYSIS.md)**。

### 1. 卡点：按机器授权的 license 校验

CoolStar 的音频驱动是授权收费的。原始 `.sys` 里直接能读到：

```
AVS: License not found. Please install it to C:\Windows\system32\drivers\avs_signedlicense.bin
AVS: License too small. Make sure the license is signed.
SystemSKU      Chell
```

`DriverEntry` 阶段会读授权文件、验签、并和 SMBIOS 的 `SystemSKU`（板卡代号）比对。
校验失败就返回 NTSTATUS `0xC000026A`，上层据此**不启动 ADSP** —— 于是 I2S 没时钟，
功放不上电，端点电平满幅但完全没声。

### 2. 补丁：只把"失败"改成"成功"

三个栈一共改 5 / 12 / 12 处，合计 153 个字节，全部是同一个模式：

```asm
; 原始 —— 校验失败则返回错误码
mov    eax, 0xC000026A
; 修补 —— 校验失败也返回成功
mov    eax, 0
```

编译器的 `return cond ? STATUS_X : STATUS_SUCCESS;` 会生成 `and reg, 0xC000026A`，
补丁把它变成 `and reg, 0`（后面用 `nop` 补齐长度）。

| 驱动栈 | 补丁点 | 改动字节 |
|---|---|---|
| `csaudiosstavs` | 5 | 25 B |
| `csaudiointcsof` | 12 | 64 B |
| `csaudioamdsof` | 12 | 64 B |

**INF、HWID 表、服务安装段、DSP 固件一个字都没动**，`CsAudioCallbackAPI` 回调照旧
向 CoolStar 总线驱动注册端点 —— `ssm4567` 功放驱动正是靠这条回调拿到 Speaker 端点。

### 3. 签名：重新生成 catalog 并重签

改一个字节，WHQL 签名就废了。所以修补包是**重新签**的，而且两条链性质不同：

| 文件 | 原始签名者 | 修补后签名者 | 时间戳 |
|---|---|---|---|
| `.sys` | Microsoft WHQL | Airspace Intelligent Technology (Changzhou) | DigiCert（**公共 TSA**） |
| `.cat` | Microsoft WHQL | 福建六壬网安股份有限公司 | Pikachu Time Sub CA（**自建 TSA**） |

`.sys` 的链能追到公共信任根，所以关掉 Secure Boot 就能加载；
但 `.cat` 的时间戳来自自建 TSA，公共同不认，`pnputil` 会报：

```
0xE0000242  尚未建立对 Authenticode(TM)签名的目录的发布者信任
```

**所以必须先把自建 TSA 的根导入目标机**（`certs/pikachu-testca-root.cer`，
安装器会自动做）：

```bat
certutil -addstore -f Root             certs\pikachu-testca-root.cer
certutil -addstore -f TrustedPublisher certs\pikachu-testca-root.cer
```

> 两个坑，踩过就知道多浪费时间的坑：
> ① `Get-AuthenticodeSignature` 返回 `Valid` **不等于** `pnputil` 会接受 ——
>    前者只验签名本身，后者还要过 setupapi 的发布者信任检查。
> ② `.inf` 的字节一个都不能动（**包括换行符**），因为 catalog 里存的是它的哈希。
>    这就是 `.gitattributes` 把 `*.inf`/`*.sys`/`*.cat` 全标成 `binary` 的原因。

### 4. 和 akibaPatcher 的区别

[akibaPatcher](https://github.com/akibaGumi/akibaPatcher) 也在做同一件事（SOF 1.0.5.0），
但路线更"粗"：直接砍掉 `.sys` 的 Authenticode 证书表、伪造一张 20 年自签证书、
把 4 处函数改成 `xor eax,eax; ret` / `mov eax,1; ret`，**并且强制要求开 testsigning**。

本仓库的包是"最小语义补丁 + 真实证书链重签"，代价是需要一条可用代码签名证书和
一个自己的时间戳服务，好处是**不需要 testsigning**，机器上不留测试模式水印。
逐条对照见 [docs/PATCH-ANALYSIS.md 第五节](docs/PATCH-ANALYSIS.md#五和-akibapatcher-的技术路线对比)。

---

## 仓库结构

```
.
├── .github/workflows/release.yml   CI：填版本 → 构建 → 发 Release
├── certs/                          签名链的根（安装器会导入 Root + TrustedPublisher）
├── docs/
│   ├── DRIVERS.md                  驱动清单（生成，37 包 / 198 HWID）
│   ├── PATCH-ANALYSIS.md           修补原理与逐字节差异
│   └── VENDOR.md                   哪些东西不能进仓库、为什么
├── drivers/                        驱动本体，按功能分组
│   ├── audio/  codec/  amp/        ← 出声相关的核心
│   ├── bus/    input/  camera/  system/
├── installer/
│   ├── Install.iss                 Inno Setup 脚本（版本从命令行注入）
│   ├── payload.list                安装顺序，安装/卸载共用
│   ├── License.rtf  dpinsts.ico
├── reference/originals/            CoolStar 官方原始包（WHQL 签名，做 diff 的基准）
├── scripts/
│   ├── preflight.ps1               打包前校验 payload（CI 里也跑）
│   ├── trust-certs.bat             导入签名根
│   ├── install-drivers.bat         DPInst 装 payload.list
│   └── uninstall-drivers.bat       DPInst /U 卸 payload.list
├── src/ssm4567/                    ADI SSM4567 功放驱动源码（KMDF + I2C）
├── tools/
│   ├── dpinst/dpinst.exe           Microsoft Driver Package Installer 2.1
│   ├── gen-driver-docs.py          生成 docs/DRIVERS.md
│   └── compare-drivers.py          原始包 vs 修补包逐字节对比
└── vendor/                         本地暂存，git-ignored（见 docs/VENDOR.md）
```

---

## 开发方式

### 环境

- **打包**：Inno Setup 6（`choco install innosetup`）。不需要 WDK、不需要 Visual Studio。
- **改驱动源码**（`src/ssm4567`）：VS 2022 + WDK，注意 WDK 对 VS 版本有要求。
- **跑辅助脚本**：PowerShell 5.1 或 7，Python 3.9+（`capstone` 只在需要反汇编时用到）。

### 本地构建与自检

```bat
:: 1) 校验 payload（缺目录、缺 inf、cat 引用的文件不存在都会报错）
powershell -ExecutionPolicy Bypass -File scripts\preflight.ps1

:: 2) 打包
ISCC.exe /DMyAppVersion=1.0.5 installer\Install.iss

:: 3) 改了驱动包之后，刷新清单
python tools\gen-driver-docs.py

:: 4) 想知道某个包和官方原版差在哪
python tools\compare-drivers.py reference\originals\csaudiosstavs drivers\audio\csaudiosstavs
```

### CI

[`.github/workflows/release.yml`](.github/workflows/release.yml)：

| 触发 | 用法 |
|---|---|
| `workflow_dispatch` | 填 `version`（如 `1.0.5`），可选「预发布」→ 打 tag `v1.0.5` 并发 Release |
| push `v*` tag | 版本号取自 tag 名 |

流水线：checkout → 解析版本 → `preflight.ps1` → choco 装 Inno Setup →
`ISCC /DMyAppVersion=<v>` → 生成 `SHA256SUMS.txt` → 上传 artifact →
`gh release create`（同名 tag 已存在则 `gh release upload --clobber`）。

`preflight` 失败会直接中断构建，坏 payload 进不了 Release。

### 新增 / 升级一个驱动

1. 把包放进 `drivers/<类别>/<名字>/`（`.inf` + `.sys` + `.cat`，附属固件放同一目录）
2. 需要默认安装的话，把 `<类别>\<名字>` 追加到 `installer/payload.list`
   （顺序有意义：总线类在前，音频控制器靠后，文件里有注释说明）
3. `python tools/gen-driver-docs.py` 刷新清单
4. `powershell -File scripts/preflight.ps1` 自检
5. 提交。驱动版本从 INF 读，安装包版本从 CI 输入读

### 卸载

「应用和功能」里卸载，或管理员命令行：

```bat
scripts\uninstall-drivers.bat
```

它遍历同一份 `payload.list`，对每个 INF 调 `DPInst /U`。

---

## 已验证硬件

| 机型 | 板卡代号 | 平台 | 结果 |
|---|---|---|---|
| HP Chromebook 13 G1 | `chell` | Skylake | ✅ 扬声器 + 耳机正常（需 `ssm4567` + `csaudiosstavs`） |

其余机型按驱动栈的平台归属应当可用，但**没有逐台验证**。
如果你的机器不在表里，装完请回报机型 + 板卡代号（`wmic csproduct get name` /
`SystemSKU`）+ 设备管理器里的硬件 ID。

---

## 声明

- **CoolStar 音频驱动** — © CoolStar，通过 <https://coolstar.org/chromebook/> 分发，
  上游为按机器授权的收费组件。本仓库是**非官方重新打包**，面向已经持有授权的硬件，
  **不包含授权文件、授权生成器或激活工具**。
- **`DPInst.exe`** — © Microsoft，随 WDK 分发。
- **`src/ssm4567`** — © CoolStar，来自 <https://github.com/coolstar/ssm4567>
  （见 `src/ssm4567/LICENSE.txt`）。
- **ChromeOS EC / 触摸板 / 摄像头 / DPTF 驱动** — 版权归各自厂商。

与 Google、CoolStar 及任何硬件厂商均无隶属关系。
Chromebook 刷 Windows 有变砖风险（固件操作尤其）；仅装驱动是可逆的。
