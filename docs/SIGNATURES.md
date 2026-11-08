# 签名状态

`drivers/` 下 37 个驱动包全部带 catalog，没有裸 INF，也没有"未签名"的包。
但签名分三种情况，装之前要不要额外操作，看的是第二种里的第三类。

| 分类 | 包数 | 安装前是否需要额外步骤 |
|---|---|---|
| Microsoft WHQL 签名 | 25 | 不需要，`pnputil` 直接接受 |
| 第三方签名，证书链终止于公共根 | 5 | 不需要（触摸板那五个） |
| 第三方签名，时间戳链终止于非公共根 | 7 | 需要先把根导入信任存储 |

「需要导入」不表示包坏了，只表示 catalog 的签名时间来自一条公开根不认的链。
`certs/pikachu-certificate-install.exe` 做的就是这件事，安装器会在装驱动之前自动执行。

## 一、怎么自查

```powershell
# catalog 自身的签名
Get-AuthenticodeSignature .\drivers\audio\csaudiosstavs\csaudiosstavs.cat |
    Format-List Status, SignerCertificate

# catalog 里装了哪些证书（含时间戳链）
certutil -dump .\drivers\audio\csaudiosstavs\csaudiosstavs.cat
```

`Get-AuthenticodeSignature` 返回 `Valid` 只说明「本机当前信任这条链」，不说明
`pnputil` 会接受这个包。后者还要过 setupapi 的发布者信任检查，也就是
`TrustedPublisher` 存储。两个存储缺一个都会得到 `0xE0000242`。

## 二、逐包状态

`catalog` 列为签名来源，`sys` 列表示包内是否有 `.sys`（`crosfingerprint` 是 UMDF，
`gnodriver` 只有 INF 占位，两者本来就没有内核模块）。

| 包 | catalog | .sys | 需先导入根 |
|---|---|---|---|
| `amp/max98357a` | Microsoft WHQL | 有 | — |
| `amp/max98390` | Microsoft WHQL | 有 | — |
| `amp/opengmaxcodec` | Microsoft WHQL | 有 | — |
| `amp/rt1011` | Microsoft WHQL | 有 | — |
| `amp/rt1015` | Microsoft WHQL | 有 | — |
| `amp/rt1019` | Microsoft WHQL | 有 | — |
| `audio/csaudioamdsof` | 第三方 | 有 | **需要** |
| `audio/csaudiointcsof` | 第三方 | 有 | **需要** |
| `audio/csaudiosstavs` | 第三方 | 有 | **需要** |
| `audio/ssm4567` | 第三方 | 有 | **需要** |
| `bus/acpbus` | Microsoft WHQL | 有 | — |
| `bus/cbtable` | Microsoft WHQL | 有 | — |
| `bus/crosecbus` | Microsoft WHQL | 有 | — |
| `bus/croseci2c` | Microsoft WHQL | 有 | — |
| `bus/crosectypec` | Microsoft WHQL | 有 | — |
| `bus/iaLPSS2_SPI` | Microsoft WHQL | 有 | — |
| `bus/iaLPSS2_UART2` | Microsoft WHQL | 有 | — |
| `bus/intelpmc` | 第三方 | 有 | **需要** |
| `bus/inteltcss` | 第三方 | 有 | **需要** |
| `bus/sklhdaudbus` | Microsoft WHQL | 有 | — |
| `camera/iaisp64_46xx_ADL` | Microsoft WHQL | 有 | — |
| `camera/ov5675` | Microsoft WHQL | 有 | — |
| `codec/croseccodec` | Microsoft WHQL | 有 | — |
| `codec/cs42l42` | Microsoft WHQL | 有 | — |
| `codec/da7219` | Microsoft WHQL | 有 | — |
| `codec/nau8825` | Microsoft WHQL | 有 | — |
| `codec/nau88l25rvp` | Microsoft WHQL | 有 | — |
| `codec/rt5682` | Microsoft WHQL | 有 | — |
| `codec/rt5682s` | Microsoft WHQL | 有 | — |
| `input/crosfingerprint` | 第三方 | 无 | **需要** |
| `input/crostouchpad-atmel` | 第三方 | 有 | — |
| `input/crostouchpad-cypress` | 第三方 | 有 | — |
| `input/crostouchpad-cypress-smbus` | 第三方 | 有 | — |
| `input/crostouchpad-elan` | 第三方 | 有 | — |
| `input/crostouchpad-synaptics` | 第三方 | 有 | — |
| `system/dptf_acpi` | Microsoft WHQL | 有 | — |
| `system/gnodriver` | Microsoft WHQL | 无 | — |

### 需要导入根的 7 个

```
audio/csaudiosstavs       audio/csaudiointcsof      audio/csaudioamdsof
audio/ssm4567             bus/intelpmc              bus/inteltcss
input/crosfingerprint
```

这七个的 `.sys` 嵌入签名本身是完整的，问题只出在 catalog 的时间戳链。
只要把根导入 `LocalMachine\Root` 与 `LocalMachine\TrustedPublisher`，其余不用动。

### 触摸板那五个

`input/crostouchpad-*` 的签名者是个人开发者证书，链终止于一个公共受信任的 EV 根，
所以不在这七个里。设备管理器里看到签名者不是公司名时不要误判成"伪造"。

## 三、什么时候会变成"真的没签名"

只要改动包内任何文件的一个字节，这个包就失去有效签名，因为 catalog 里记录的是
`INF`、`SYS`、`.ri` 等每个成员的哈希。常见触发场景：

- 用文本编辑器保存了 `.inf`（换了换行符或编码）
- 打了代码补丁但没重新生成 catalog
- 删掉或改名了 `.ri` 固件

`.gitattributes` 把 `*.inf`、`*.sys`、`*.cat`、`*.ri` 全部标成 `binary`，就是为了防止
Git 在检出时改换行符把签名弄坏。处理这类包时不要用会做换行转换的编辑器。

## 四、重新签名

自己改过 `.sys` 之后需要重建 catalog 并签名，流程（生成 `.cdf`、`inf2cat`/`makecat`、
签名、导入信任根、验证）见：

<https://github.com/PIKACHUIM/FakeSign>

仓库不附带任何代码签名证书，也不提供私钥。
