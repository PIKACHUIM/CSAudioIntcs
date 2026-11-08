<div align="center">

# ChromeBook CoolStar 音频驱动包

Chromebook 装完 Windows 扬声器不响，这里有一整套能出声的驱动

[简体中文](README.md) · [English](README.en.md) · [繁體中文](README.zh-TW.md) · [日本語](README.ja.md)

![platform](docs/badges/platform.svg) ![drivers](docs/badges/drivers.svg) ![installer](docs/badges/installer.svg) ![upstream](docs/badges/upstream.svg)

</div>

[这是什么](#这是什么) · [先确认装哪一套](#先确认装哪一套) · [安装](#安装) · [驱动清单](#驱动清单) · [原版为什么不出声](#原版为什么不出声) · [目录结构](#目录结构) · [开发](#开发) · [验证过的机器](#验证过的机器) · [已知缺口](#已知缺口) · [声明](#声明)

---

## 这是什么

手上这台 HP Chromebook 13 G1 装完 Windows 之后，设备管理器一片干净，音量滑块能拖，放测试音时扬声器端点的电平表打到 0.35 满格，喇叭一声不响。

先怀疑功放没上电，换驱动版本、刷 coreboot、关快速启动，一样没动。后来把 CoolStar 的 `.sys` 拖进十六进制编辑器翻字符串，看到一句 `AVS: License not found`，路径是 `C:\Windows\system32\drivers\avs_signedlicense.bin`。授权校验没过，`DriverEntry` 返回 `0xC000026A`，上层拿到这个码就不启动 ADSP。ADSP 不起来，I2S 没有时钟，功放一直挂在关断状态。

设备枚举、端点创建、电平显示都正常，声音没有。这个组合看起来像硬件故障，实际是软件把后半段掐掉了。细节在[原版为什么不出声](#原版为什么不出声)。

仓库里是这轮排查攒下来的东西。三套 CoolStar 音频栈，挂在它们底下的 codec 和功放，另外配齐了 ChromeOS 的 EC 总线、触摸板、指纹、摄像头。

里面全是裸的 `INF` + `SYS` + `CAT`，没有压缩包，也没有自解压安装器。安装过程中会执行的外部程序只有两个，微软的 `DPInst` 和 `certs/` 里那个证书安装器，都可以查。

上游是按机器卖的授权。这里的东西面向已经买过授权的机器，仓库里不含授权文件，也没有生成器和激活工具。

## 先确认装哪一套

Chromebook 的音频按平台分成三套，互不兼容，硬件 ID 也不重叠。

| 平台 | 装这个 | 服务名 |
|---|---|---|
| Skylake、Kaby Lake、Amber Lake、Braswell、Apollo Lake | `drivers/audio/csaudiosstavs` | `CsAudioSSTAVS` |
| Gemini Lake、Jasper Lake、Ice Lake、Tiger Lake、Alder Lake 及更新 | `drivers/audio/csaudiointcsof` | `CsAudioIntcSOF` |
| AMD 的 Chromebook，Rembrandt、Phoenix 这些 | `drivers/audio/csaudioamdsof` | `CsAudioAmdSOF` |
| glados、chell 这类板子的扬声器，例如 HP Chromebook 13 G1 | 再加一个 `drivers/audio/ssm4567` | `ssm4567` |

最后一行值得单独讲。Skylake 时代的 Chromebook 把扬声器功放挂在 I2C 上，耳机走另一颗 codec，两条路各管各的。耳机有声、扬声器没声，缺的就是 `ssm4567` 这个包，跟主驱动版本对不对没关系。

拿不准就装完整包。三套都在里面，`DPInst` 只绑硬件 ID 对得上的那个，装进去的另外两个不会生效。

## 安装

去 [Releases](../../releases) 取 `ChromeBook-CoolStar-Audio-Driver-<版本>-x64.exe`，以管理员身份运行，装完重启。

自己构建只要 [Inno Setup 6](https://jrsoftware.org/isinfo.php)，不用 WDK，也不用 Visual Studio。

```bat
ISCC.exe installer\Install.iss

:: 带版本号的话，产物名会跟着变
ISCC.exe /DMyAppVersion=1.0.5 installer\Install.iss
```

产物落在 `dist\`。

只坏了一路的话可以分开处理，先信任签名根，再上驱动包：

```bat
:: 管理员 PowerShell，在解压后的目录里执行
certs\pikachu-certificate-install.exe /VERYSILENT

pnputil /add-driver drivers\audio\ssm4567\ssm4567.inf /install
```

`pnputil` 会在 `ACPI\INT343B\0` 和 `\1` 上各绑一次，成功后设备管理器里出现两个 `Analog Devices SSM4567 Amplifier`，状态正常。

## 驱动清单

37 个包，198 个硬件 ID。每个包的版本、提供商、全部 HWID 在 [docs/DRIVERS.md](docs/DRIVERS.md)。那张表是 `python tools/gen-driver-docs.py` 从 INF 里读出来的，别手改，改完下次生成就覆盖了。

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

`acpbus` · `cbtable` · `crosecbus` · `croseci2c` · `crosectypec` · `sklhdaudbus` · `intelpmc` · `inteltcss` · `iaLPSS2_SPI` · `iaLPSS2_UART2` · `dptf_acpi` · `gnodriver`

### 输入与摄像头

`crostouchpad-{synaptics,cypress,cypress-smbus,atmel,elan}` · `crosfingerprint` · `ov5675` · `iaisp64_46xx_ADL`

有两个包在仓库里但默认不装。`nau88l25rvp` 和 `nau8825` 抢同一个 `ACPI\10508825`，只能活一个，默认给 CoolStar 那版；`gnodriver` 只有 INF 没有 `sys`，装上去也没东西可绑。

装和卸都读 [installer/payload.list](installer/payload.list) 这一份清单，顺序也在里面。

## 原版为什么不出声

原始 `.sys` 里能直接读到这几行字符串，用的是明文 ASCII，没加密也没混淆。

```
AVS: License not found. Please install it to C:\Windows\system32\drivers\avs_signedlicense.bin
AVS: License too small. Make sure the license is signed.
SystemSKU      Chell
```

`DriverEntry` 会读授权文件、验签、再拿 SMBIOS 里的板卡代号比对。任何一步不过就返回 NTSTATUS `0xC000026A`，调用方看到这个码直接放弃启动 ADSP。

### 补丁动了哪几个字节

三个栈分别改了 5 处、12 处、12 处，加起来 153 个字节。改法只有一种。

```asm
mov eax, 0xC000026A    ; 原来校验失败返回这个
mov eax, 0              ; 现在照样返回成功
```

`return cond ? STATUS_X : STATUS_SUCCESS;` 编译出来是 `and reg, 0xC000026A`，补丁把它变成 `and reg, 0`，剩下的字节用 `nop` 填平，长度不变，后面的指令偏移也不会错位。

INF、HWID 表、服务安装段、DSP 固件一个字节都没动。`CsAudioCallbackAPI` 回调照旧注册，`ssm4567` 功放驱动全靠这条回调才拿到 Speaker 端点。改的只是授权判断，音频通路本身是原厂的。

### 签名与信任链

改一个字节，WHQL 签名就废了，所以修补版是重新签的。两个文件用的证书还不一样。

| 文件 | 原始 | 现在 | 时间戳 |
|---|---|---|---|
| `.sys` | 微软 WHQL | Airspace Intelligent Technology，常州 | DigiCert，公共 TSA |
| `.cat` | 微软 WHQL | 福建六壬网安 | Pikachu Time Sub CA，自建 |

`.sys` 的链能追到公共根，Secure Boot 关着就能加载。`.cat` 的时间戳来自自建 TSA，公开根不认，`pnputil` 会卡在这里：

```
0xE0000242  尚未建立对 Authenticode(TM)签名的目录的发布者信任
```

`certs/` 里那个安装器就是干这个的。它在装驱动之前跑一遍，把根同时写进 `LocalMachine\Root` 和 `LocalMachine\TrustedPublisher`，少写一个都还是 0xE0000242。

有个坑可以提前说，省你半天。`Get-AuthenticodeSignature` 返回 `Valid`，不代表 `pnputil` 会收这个包。前者只验 PKCS#7 结构和证书链，后者还要过 setupapi 的发布者信任检查。另外 INF 的字节一个都不能改，换行符也算，因为 catalog 里存着它的哈希。`.gitattributes` 把 `*.inf`、`*.sys`、`*.cat` 都标成 `binary`，就是为了这个。

逐字节 diff、反汇编，以及和 [akibaPatcher](https://github.com/akibaGumi/akibaPatcher) 那条路线的对比，都在 [docs/PATCH-ANALYSIS.md](docs/PATCH-ANALYSIS.md)。

## 目录结构

```
.
├── .github/workflows/release.yml   CI，填版本进去，出的就是 Release
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
├── reference/originals/            CoolStar 官方原包，只用来 diff，不安装
├── scripts/
│   ├── preflight.ps1               打包前校验 payload
│   ├── trust-certs.bat             手动装驱动时信任签名根
│   ├── install-drivers.bat         按 payload.list 调 DPInst
│   └── uninstall-drivers.bat       同一份清单，走 /U
├── src/ssm4567/                    ADI SSM4567 功放驱动源码，KMDF 加 I2C
├── tools/
│   ├── dpinst/dpinst.exe           Microsoft DPInst 2.1
│   ├── gen-driver-docs.py          生成 docs/DRIVERS.md
│   └── compare-drivers.py          原包和修补包逐字节对比
└── vendor/                         本地暂存，git 忽略，见 docs/VENDOR.md
```

## 开发

打包只要 Inno Setup 6，`choco install innosetup` 完事。改 `src/ssm4567` 才需要 VS 2022 加 WDK，注意 WDK 对 VS 版本有要求，对不上会编不过。辅助脚本用 PowerShell 5.1 或 7，Python 3.9 以上。`compare-drivers.py` 的反汇编部分要 `pip install capstone`，不装也能跑，只是看不到指令。

```bat
:: 校验 payload，缺目录、缺 inf、catalog 引用的文件不存在都会报出来
powershell -ExecutionPolicy Bypass -File scripts\preflight.ps1

:: 打包
ISCC.exe /DMyAppVersion=1.0.5 installer\Install.iss

:: 动过驱动包之后刷新清单
python tools\gen-driver-docs.py

:: 看看某个包跟官方原版差在哪
python tools\compare-drivers.py reference\originals\csaudiosstavs drivers\audio\csaudiosstavs
```

CI 在 [.github/workflows/release.yml](.github/workflows/release.yml)。手动触发填个版本号，或者推一个 `v*` 标签。流程是跑 preflight、装 Inno Setup、`ISCC /DMyAppVersion=<v>`、算 `SHA256SUMS.txt`、传 artifact、`gh release create`，同名 tag 已存在就改成上传覆盖。preflight 挂了直接中断，坏包进不了 Release。

加驱动的话，包丢进 `drivers/<类别>/<名字>/`，需要默认装就往 `payload.list` 加一行，跑一遍生成脚本和 preflight，提交。驱动版本从 INF 读，安装包版本从 CI 输入读，两边不用手动同步。

卸载走系统里的应用列表，或者 `scripts\uninstall-drivers.bat`。它按同一份清单逐个 `DPInst /U`，最后把 CA 一起卸掉。

## 验证过的机器

| 机型 | 板卡 | 平台 | 结果 |
|---|---|---|---|
| HP Chromebook 13 G1 | `chell` | Skylake | 扬声器和耳机都正常，要 `ssm4567` 加 `csaudiosstavs` |

只有这一台。其它机型按平台归属应该能用，但我没有条件逐台试，所以不敢替你担保。试过的话把机型、`SystemSKU`、以及 `ACPI\INT343B` 和 `ACPI\10508825` 的硬件 ID 发我，我加进表里。

## 已知缺口

四个包的上游版本比这里新，三处东西故意没收，原因和替代方案写在 [docs/VENDOR.md](docs/VENDOR.md)。

影响最大的是 Intel Serial IO 的 I2C 和 GPIO 驱动。chell 的 codec 和功放就挂在那条 I2C 总线上，全新装机缺了它，codec 连寄存器都配不上。手上只有一份 DriverStore 转储，里面没有 catalog，拼不出能装进 x64 的包，所以这活儿卡在这儿。

## 声明

CoolStar 音频驱动 © CoolStar，从 <https://coolstar.org/chromebook/> 分发，上游按机器授权收费。这个仓库是非官方重新打包，面向已经持有授权的硬件，不含授权文件、生成器或激活工具。`DPInst.exe` © Microsoft，随 WDK 分发。`src/ssm4567` © CoolStar，来自 <https://github.com/coolstar/ssm4567>，许可见 `src/ssm4567/LICENSE.txt`。ChromeOS EC、触摸板、摄像头、DPTF 那些驱动版权归各自厂商。

跟 Google、CoolStar 和任何硬件厂商都没有隶属关系。Chromebook 刷 Windows 有变砖风险，动固件尤其危险。只装驱动是可逆的，卸载脚本能把驱动和信任的根一起清掉。
