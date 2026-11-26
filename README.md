<div align="center">

# ChromeBook CoolStar 驱动包

Chromebook 装完 Windows，声卡、触摸板、摄像头常常一个都认不出来。
这个仓库把 CoolStar 和 ChromeOS 各厂商的驱动收在一起，做成一个安装程序。

[简体中文](README.md) · [English](README.en.md) · [繁體中文](README.zh-TW.md) · [日本語](README.ja.md)

![platform](docs/badges/platform.svg) ![drivers](docs/badges/drivers.svg) ![installer](docs/badges/installer.svg) ![license](docs/badges/license.svg)

[下载安装包](../../releases) · [驱动清单](docs/DRIVERS.md) · [修补记录](docs/PATCH-ANALYSIS.md) · [版本差异](docs/UPSTREAM.md) · [许可证](LICENSE)

</div>

---

## 这是什么

仓库里有 36 个驱动包、171 个硬件 ID，默认安装 35 个。

| 项 | 内容 |
|---|---|
| 驱动包 | 36 个，其中 35 个写进了安装清单 |
| 硬件 ID | 171 个 |
| 覆盖设备 | 音频控制器、codec、功放、触摸板、触摸屏、指纹、摄像头、EC 总线、DPTF |
| 平台 | Intel Skylake 到 Alder Lake，AMD Rembrandt / Phoenix |
| 文件形式 | 裸 `INF` + `SYS` + `CAT`，没有压缩包和自解压安装器 |
| 安装工具 | Microsoft DPInst 2.1、Inno Setup 6 |

驱动来自 CoolStar 和 ChromeOS 各硬件厂商的公开包。版本号和硬件 ID 都没改，动的只有目录结构和安装顺序。

安装过程中会执行两个外部程序：`tools/dpinst/dpinst.exe` 和 `certs/pikachu-certificate-install.exe`。前者装驱动，后者把驱动 catalog 的签名根导入系统。别的没有。

CoolStar 的音频驱动是按机器授权的商业软件。本仓库面向已经买过授权的机器，不包含授权文件、生成器或激活工具。

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

## 安装

### 用发布包

1. 在 [Releases](../../releases) 下载 `ChromeBook-CoolStar-Audio-Driver-<版本>-x64.exe`
2. 右键以管理员身份运行
3. 装完重启

安装程序先导签名根，再按 `installer/payload.list` 的顺序逐个调 DPInst。同版本的驱动已经装过就跳过，不会重复覆盖。

只支持 x64。32 位 Windows 没有对应的驱动包。

### 只缺一个驱动

比如只有触摸板不工作。先导根，再用 `pnputil` 指定 INF：

```bat
:: 管理员 PowerShell
certs\pikachu-certificate-install.exe /VERYSILENT

pnputil /add-driver drivers\input\crostouchpad-synaptics\crostouchpad.inf /install
```

去掉设备，用 `scripts\uninstall-drivers.bat`，它会把导进去的根证书一起删掉。

### 音频栈选哪个

三套音频架构互不兼容，硬件 ID 也不重叠，DPInst 只会绑定匹配的那一个。全套安装不用挑，只装单个的时候按平台选。

| 平台 | 包 | 服务名 |
|---|---|---|
| Skylake、Kaby Lake、Amber Lake、Braswell、Apollo Lake | `csaudiosstavs` | `CsAudioSSTAVS` |
| Gemini Lake、Jasper Lake、Ice Lake、Tiger Lake、Alder Lake 及更新 | `csaudiointcsof` | `CsAudioIntcSOF` |
| AMD Rembrandt、Phoenix 等 | `csaudioamdsof` | `CsAudioAmdSOF` |
| glados、chell 这类板卡的扬声器功放 | `ssm4567` | `ssm4567` |

`ssm4567` 是独立的功放驱动，和上面三套并行装，不冲突。

## 驱动清单

完整版本号和每一条 HWID 在 [docs/DRIVERS.md](docs/DRIVERS.md)，那份文件由 `python tools/gen-driver-docs.py` 从 INF 生成，别手改。

**音频**：`csaudiosstavs` 1.0.4.0 · `csaudiointcsof` 1.0.4.0 · `csaudioamdsof` 1.0.1.0 · `ssm4567` 1.0.1

**Codec**：`nau8825` · `nau88l25rvp` · `cs42l42` · `da7219` · `rt5682` · `rt5682s` · `croseccodec`

**功放**：`max98357a` · `max98390` · `opengmaxcodec` · `rt1011` · `rt1015` · `rt1019`

**总线与系统**：`acpbus` · `cbtable` · `crosecbus` · `croseci2c` · `crosectypec` · `sklhdaudbus` · `intelpmc` · `inteltcss` · `iaLPSS2_SPI` · `iaLPSS2_UART2` · `dptf_acpi`

**输入与摄像头**：`crostouchpad-{synaptics,cypress,cypress-smbus,atmel,elan}` · `crosfingerprint` · `ov5675` · `iaisp64_46xx_ADL`

`nau88l25rvp` 和 `nau8825` 抢同一个硬件 ID `ACPI\10508825`，默认用 CoolStar 那份，所以它随仓库分发但不进安装清单。安装和卸载都以 [installer/payload.list](installer/payload.list) 为准。

## 原版为什么不出声

CoolStar 音频驱动在 `DriverEntry` 里读授权文件，验签名，再和 SMBIOS 里的板卡代号比对。校验不过就返回 `0xC000026A`，上层拿到这个状态码就不启动 ADSP。ADSP 不跑，I2S 就没有时钟，功放一直关着。

于是现象很怪：设备管理器全绿，端点齐全，放测试音时电平表也在跳，喇叭就是没声。

二进制里能直接读到这几句：

```
AVS: License not found. Please install it to C:\Windows\system32\drivers\avs_signedlicense.bin
AVS: License too small. Make sure the license is signed.
SystemSKU
Chell
```

三个音频栈一共改了 29 处、153 字节，都是同一个套路：把失败码的立即数改掉，让返回值恒为成功。要么是错误分支里的 `mov eax, 0xC000026A`，要么是 `return cond ? STATUS_X : STATUS_SUCCESS;` 编译出来的那段 `sbb / not / and`。

INF、HWID 表、服务安装段和 DSP 固件一个字节都没动。`CsAudioCallbackAPI` 回调也保留着，`ssm4567` 靠它注册 Speaker 端点。

逐点偏移和反汇编见 [docs/PATCH-SITES.md](docs/PATCH-SITES.md)，为什么这么改见 [docs/PATCH-ANALYSIS.md](docs/PATCH-ANALYSIS.md)。

## 签名

驱动包本身都有 catalog，但签名来源分三种，差别在于装之前要不要多一步。

| 分类 | 包数 | 额外操作 |
|---|---|---|
| Microsoft WHQL | 24 | 不需要 |
| 第三方签名，链到公共根 | 5 | 不需要（触摸板那五个） |
| 第三方签名，链到非公共根 | 7 | 先把根导入信任存储 |

最后那 7 个包本身没坏，只是 catalog 的时间戳链停在一张公开根不认的证书上。不导根的话 `pnputil` 会停在这里：

```
0xE0000242  尚未建立对 Authenticode(TM)签名的目录的发布者信任
```

`certs/pikachu-certificate-install.exe` 同时写 `LocalMachine\Root` 和 `LocalMachine\TrustedPublisher`，两个存储少一个都不行。安装程序会在装驱动之前自动跑它。

有个地方容易绕圈：`Get-AuthenticodeSignature` 返回 `Valid` 不代表 `pnputil` 会收。前者只验 PKCS#7 和证书链，后者还要过 setupapi 的发布者信任检查。逐包状态在 [docs/SIGNATURES.md](docs/SIGNATURES.md)。

要自己给改过的 `.sys` 重签，得重建 catalog 再签两遍，流程在 [FakeSign](https://github.com/PIKACHUIM/FakeSign)。仓库里没有证书，也没有私钥。

### 安装包本身的签名

Actions 构建出来的 `exe` 没有 Authenticode 签名，第一次运行会被 SmartScreen 拦一下，点"仍要运行"就行，不影响安装。

要正式签，用自己的证书在本地签。签名会改变文件哈希，所以签完要重算 `SHA256SUMS.txt`：

```powershell
signtool sign /fd SHA256 /f mycert.pfx /p <密码> `
  /tr http://timestamp.digicert.com /td SHA256 `
  dist\ChromeBook-CoolStar-Audio-Driver-<版本>-x64.exe
```

```powershell
$out = "dist\SHA256SUMS.txt"
Remove-Item $out -ErrorAction SilentlyContinue
Get-ChildItem dist -Filter *.exe | ForEach-Object {
  "$((Get-FileHash $_.FullName -Algorithm SHA256).Hash.ToLower())  $($_.Name)"
} | Out-File $out -Encoding ascii
```

然后 `gh release upload v<版本> dist\*.exe dist\SHA256SUMS.txt --clobber` 覆盖上去。

## 自己构建

只需要 [Inno Setup 6](https://jrsoftware.org/isinfo.php)，不用 WDK，也不用 Visual Studio。

```bat
:: 可选，检查 payload 有没有缺文件
powershell -ExecutionPolicy Bypass -File scripts\preflight.ps1

ISCC.exe installer\Install.iss
ISCC.exe /DMyAppVersion=26.11.26 installer\Install.iss
```

产物在 `dist\`。不改版本号时默认用 `Install.iss` 里写的那一个。

CI 在 `.github/workflows/release.yml`：手动触发时填版本号，或者推 `v*` 标签。流程是 preflight → 构建 → 生成 `SHA256SUMS.txt` → 建 Release。

## 许可证

仓库里的脚本、文档和构建配置用 MIT，全文见 [LICENSE](LICENSE)。

驱动二进制的版权归各原厂，不在 MIT 范围内：

| 组件 | 授权 |
|---|---|
| CoolStar 音频驱动 | 商业授权，按机器计费，见 <https://coolstar.org/chromebook/> |
| `src/ssm4567` | CoolStar，见 `src/ssm4567/LICENSE.txt` |
| `tools/dpinst/dpinst.exe` | Microsoft，随 WDK 分发 |
| ChromeOS EC、触摸板、摄像头、DPTF 等 | 各自厂商 |

MIT 只覆盖本仓库新增的部分，不改变上面这些组件的授权状态。本仓库和 Google、CoolStar 以及任何硬件厂商都没有隶属关系。

Chromebook 装 Windows 有变砖风险，改固件风险更高。只装驱动是可逆的，卸载脚本会把驱动和导入的根证书一起清掉。
