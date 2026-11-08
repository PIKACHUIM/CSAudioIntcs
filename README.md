<div align="center">

# ChromeBook CoolStar 音频驱动包

Chromebook 装完 Windows 之后，把扬声器修好

[简体中文](README.md) · [English](README.en.md) · [繁體中文](README.zh-TW.md) · [日本語](README.ja.md)

![platform](docs/badges/platform.svg) ![drivers](docs/badges/drivers.svg) ![installer](docs/badges/installer.svg) ![upstream](docs/badges/upstream.svg)

</div>

---

## 简介

这个仓库最初只为了修好一台 HP Chromebook 13 G1 的扬声器。那台机器装完 Windows 之后，
设备管理器里一片正常，音量条会动，播放时端点电平表也能打满，喇叭就是不响。查到最后
发现是驱动里的授权校验没过，ADSP 没被启动，功放一直挂在关断状态。

后来把能覆盖到的机型都补齐了：CoolStar 的三套音频栈，挂在它们下面的 codec 和功放，
还有 ChromeOS 的 EC 总线、触摸板、指纹、摄像头。

仓库里全是裸的 `INF + SYS + CAT`，没有压缩包，也没有自解压安装器。工作树 13 MB，
运行时真正会执行的外来程序只有微软的 `DPInst` 和 `certs/` 里的 CA 安装器。

上游是收费的，按机器授权。这里的驱动面向已经买过授权的硬件，不包含授权文件，
也没有授权生成或激活工具。

## 先确定装哪一套

Chromebook 的音频架构按平台分成三套，互不兼容，硬件 ID 也不重叠。装错会出现
「设备在、没声音」这种最费时间排查的状态。

| 你的平台 | 装这个 | 服务名 |
|---|---|---|
| Skylake / Kaby Lake / Amber Lake / Braswell / Apollo Lake | `drivers/audio/csaudiosstavs` | `CsAudioSSTAVS` |
| Gemini Lake / Jasper Lake / Ice Lake / Tiger Lake / Alder Lake 及更新 | `drivers/audio/csaudiointcsof` | `CsAudioIntcSOF` |
| AMD Chromebook（Rembrandt、Phoenix 等） | `drivers/audio/csaudioamdsof` | `CsAudioAmdSOF` |
| glados / chell 板子的扬声器（HP Chromebook 13 G1 等） | 另加 `drivers/audio/ssm4567` | `ssm4567` |

不确定平台的话，装完整包就行。安装器把三套都放进去，DPInst 只会绑定硬件 ID 对上的那个。

## 安装

**用安装包**：到 [Releases](../../releases) 下 `ChromeBook-CoolStar-Audio-Driver-<版本>-x64.exe`，
右键管理员运行，重启。

**从源码构建**：只需要 [Inno Setup 6](https://jrsoftware.org/isinfo.php)，不需要 WDK、
不需要 Visual Studio。

```bat
ISCC.exe installer\Install.iss

:: 或者指定版本号，产物名会跟着变
ISCC.exe /DMyAppVersion=1.0.5 installer\Install.iss
```

输出在 `dist\`。

**只补某一个驱动**，比如只有扬声器不响。先信任签名根，再装包：

```bat
:: 管理员 PowerShell，在解压后的目录里
certs\pikachu-certificate-install.exe /VERYSILENT

pnputil /add-driver drivers\audio\ssm4567\ssm4567.inf /install
```

## 驱动清单

37 个包，198 个硬件 ID。每个包的版本、提供商、全部 HWID 在
[docs/DRIVERS.md](docs/DRIVERS.md)，那份表是 `python tools/gen-driver-docs.py`
从 INF 里读出来生成的，不用手写。

### 音频

| 包 | 版本 | 说明 |
|---|---|---|
| `csaudiosstavs` | 1.0.4.0 | AVS 架构，Skylake 到 Apollo Lake |
| `csaudiointcsof` | 1.0.4.0 | SOF 架构，Gemini Lake 及更新 |
| `csaudioamdsof` | 1.0.1.0 | AMD 平台的 SOF |
| `ssm4567` | 1.0.1 | ADI 的 I2S 功放，glados 系列板子用 |

### Codec

`nau8825` · `nau88l25rvp` · `cs42l42` · `da7219` · `rt5682` · `rt5682s` · `croseccodec`

### 功放

`max98357a` · `max98390` · `opengmaxcodec` · `rt1011` · `rt1015` · `rt1019`

### 总线与系统

`acpbus` · `cbtable` · `crosecbus` · `croseci2c` · `crosectypec` · `sklhdaudbus` ·
`intelpmc` · `inteltcss` · `iaLPSS2_SPI` · `iaLPSS2_UART2` · `dptf_acpi` · `gnodriver`

### 输入与摄像头

`crostouchpad-{synaptics,cypress,cypress-smbus,atmel,elan}` · `crosfingerprint` ·
`ov5675` · `iaisp64_46xx_ADL`

### 装了但默认不启用

`codec/nau88l25rvp` 和 `codec/nau8825` 抢同一个 `ACPI\10508825`，只能留一个，
默认装 CoolStar 那版。`system/gnodriver` 只有 INF，没有 `sys`，装上去也没东西可绑。

安装顺序写在 [installer/payload.list](installer/payload.list)，装和卸都读这一份。

## 原版为什么不出声

三套音频栈的原始 `.sys` 里能直接读到这几行：

```
AVS: License not found. Please install it to C:\Windows\system32\drivers\avs_signedlicense.bin
AVS: License too small. Make sure the license is signed.
SystemSKU      Chell
```

`DriverEntry` 会读授权文件、验签、跟 SMBIOS 里的板卡代号比对。校验不过就返回
NTSTATUS `0xC000026A`，上层看到失败就不再启动 ADSP。没有 ADSP 就没有 I2S 时钟，
功放拿不到时钟就一直静音。

所以现象才那么反常：设备、端点、电平全对，声音一点没有。

### 补丁改了什么

三个栈分别改了 5、12、12 处，合计 153 字节。改的全是同一个模式：

```asm
mov eax, 0xC000026A    ; 原来：校验失败就返回这个错误码
mov eax, 0              ; 现在：照样返回成功
```

`return cond ? STATUS_X : STATUS_SUCCESS;` 编译出来是 `and reg, 0xC000026A`，
补丁把它变成 `and reg, 0`，多出来的字节用 `nop` 填平。

INF、HWID 表、服务安装段、DSP 固件一个字没动。`CsAudioCallbackAPI` 回调照旧注册，
`ssm4567` 功放驱动就是靠这条回调拿到 Speaker 端点的。

### 签名怎么处理

改一个字节，WHQL 签名就废了，所以修补版是重新签的。两个文件用的证书还不一样：

| 文件 | 原始 | 现在 | 时间戳 |
|---|---|---|---|
| `.sys` | 微软 WHQL | Airspace Intelligent Technology（常州） | DigiCert，公共 TSA |
| `.cat` | 微软 WHQL | 福建六壬网安 | Pikachu Time Sub CA，自建 |

`.sys` 的链能追到公共根，关掉 Secure Boot 就能加载。`.cat` 的时间戳来自自建 TSA，
公开根不认，`pnputil` 会报 `0xE0000242`。这就是 `certs/` 里那个安装器存在的理由：
它在装驱动之前跑一遍，把根写进 `LocalMachine\Root` 和 `LocalMachine\TrustedPublisher`。

有个坑值得提前说：`Get-AuthenticodeSignature` 返回 `Valid` 不代表 `pnputil` 会收。
前者只验 PKCS#7 本身，后者还要过 setupapi 的发布者信任检查，两回事。另外 INF 的字节
一个都不能动，包括换行符，因为 catalog 里存着它的哈希。`.gitattributes` 把
`*.inf`/`*.sys`/`*.cat` 全标成 `binary` 就是为了防这个。

逐字节 diff、反汇编、和 [akibaPatcher](https://github.com/akibaGumi/akibaPatcher)
的做法对比，都在 [docs/PATCH-ANALYSIS.md](docs/PATCH-ANALYSIS.md)。

## 目录结构

```
.
├── .github/workflows/release.yml   CI：填版本 → 构建 → 发 Release
├── certs/                          签名根，一个 Inno Setup 安装器
├── docs/
│   ├── DRIVERS.md                  驱动表，37 包 / 198 HWID，脚本生成
│   ├── PATCH-ANALYSIS.md           修补原理与逐字节差异
│   └── VENDOR.md                   哪些东西进不了仓库，以及为什么
├── drivers/                        驱动本体
│   ├── audio/  codec/  amp/        出声相关的核心
│   ├── bus/    input/  camera/  system/
├── installer/
│   ├── Install.iss                 Inno Setup 脚本
│   ├── payload.list                安装顺序
│   └── License.rtf  dpinsts.ico
├── reference/originals/            CoolStar 官方原包，做 diff 的基准，不安装
├── scripts/
│   ├── preflight.ps1               打包前校验 payload
│   ├── trust-certs.bat             手动信任签名根
│   ├── install-drivers.bat         按 payload.list 调 DPInst
│   └── uninstall-drivers.bat       同上，走 /U
├── src/ssm4567/                    ADI SSM4567 功放驱动源码，KMDF + I2C
├── tools/
│   ├── dpinst/dpinst.exe           Microsoft DPInst 2.1
│   ├── gen-driver-docs.py          生成 docs/DRIVERS.md
│   └── compare-drivers.py          原包和修补包逐字节对比
└── vendor/                         本地暂存，git 忽略，见 docs/VENDOR.md
```

## 开发

打包环境只要 Inno Setup 6（`choco install innosetup`）。改 `src/ssm4567` 才需要
VS 2022 加 WDK，注意 WDK 对 VS 版本有要求。辅助脚本用 PowerShell 5.1 或 7，
Python 3.9 以上；`compare-drivers.py` 的反汇编部分要 `pip install capstone`。

```bat
:: 校验 payload，缺目录、缺 inf、catalog 引用的文件不存在都会报错
powershell -ExecutionPolicy Bypass -File scripts\preflight.ps1

:: 打包
ISCC.exe /DMyAppVersion=1.0.5 installer\Install.iss

:: 动过驱动包之后刷新清单
python tools\gen-driver-docs.py

:: 看看某个包和官方原版差在哪
python tools\compare-drivers.py reference\originals\csaudiosstavs drivers\audio\csaudiosstavs
```

CI 在 [.github/workflows/release.yml](.github/workflows/release.yml)。手动触发时填版本号，
或者推一个 `v*` 标签。流程是 preflight、装 Inno Setup、`ISCC /DMyAppVersion=<v>`、
生成 `SHA256SUMS.txt`、传 artifact、`gh release create`。同名 tag 已存在就改成上传覆盖。
preflight 不过就中断，坏包进不了 Release。

加驱动的话，把包放进 `drivers/<类别>/<名字>/`，需要默认安装就追加到 `payload.list`，
跑一遍生成脚本和 preflight，提交。驱动版本从 INF 读，安装包版本从 CI 输入读。

卸载走「应用和功能」，或者 `scripts\uninstall-drivers.bat`。它按同一份清单逐个 `DPInst /U`，
最后把 CA 也一起卸掉。

## 已验证的机器

| 机型 | 板卡 | 平台 | 结果 |
|---|---|---|---|
| HP Chromebook 13 G1 | `chell` | Skylake | 扬声器和耳机都正常，需要 `ssm4567` 加 `csaudiosstavs` |

其它机型按平台归属应该能用，但我没有逐台试。装完可以回报一下机型、板卡代号
（`SystemSKU`）和设备管理器里的硬件 ID。

## 已知缺口

有四个包的上游版本比这里新，三处东西故意没收，原因和替代方案写在
[docs/VENDOR.md](docs/VENDOR.md)。影响最大的是 Intel Serial IO 的 I2C/GPIO 驱动缺失，
chell 的 codec 和功放就挂在那条 I2C 总线上；手上只有 DriverStore 转储，里面没有 catalog，
凑不出能装的包。

## 声明

CoolStar 音频驱动 © CoolStar，通过 <https://coolstar.org/chromebook/> 分发，上游按机器授权收费。
本仓库是非官方重新打包，面向已持有授权的硬件，不含授权文件、生成器或激活工具。
`DPInst.exe` © Microsoft，随 WDK 分发。`src/ssm4567` © CoolStar，来自
<https://github.com/coolstar/ssm4567>，许可见 `src/ssm4567/LICENSE.txt`。
ChromeOS EC、触摸板、摄像头、DPTF 驱动版权归各自厂商。

与 Google、CoolStar 及任何硬件厂商都没有隶属关系。Chromebook 刷 Windows 有变砖风险，
尤其动固件的时候；只装驱动是可逆的。
