# 修补原理与差异分析

本文记录 `drivers/audio/` 里三个 CoolStar 音频栈与官方原始包的差异，以及这些差异在代码层面
意味着什么。逐点的偏移、字节与反汇编在 [PATCH-SITES.md](PATCH-SITES.md)，本文只讲为什么。

所有数据都能用 [`tools/compare-drivers.py`](../tools/compare-drivers.py) 重新生成：

```bat
python tools/compare-drivers.py reference\originals\csaudiosstavs drivers\audio\csaudiosstavs
```

原始包在 [`reference/originals/`](../reference/originals/)（未改动），修补后的包就是安装器
实际投放的 payload。

---

## 一、结论

| | 原始包 | 修补包 |
|---|---|---|
| `.inf` | 基准 | **完全一致**（DriverVer、Provider、HWID、CatalogFile 都没动） |
| `.sys` | WHQL 签名 | 改 5 / 12 / 12 处代码，重新签名 |
| `.cat` | WHQL 签名 | 重新生成并签名 |
| 额外文件 | 带 `dpinst.exe`、`cacert.exe`、`.sys.i64` | 只留驱动本体 |

| 驱动栈 | 补丁点 | 改动字节 | `.sys` 体积 |
|---|---|---|---|
| `csaudiosstavs`（AVS，Skylake 世代） | 5 | 25 B | 106 672 → 119 048 B |
| `csaudiointcsof`（SOF，Gemini Lake 及更新） | 12 | 64 B | 160 480 → 166 664 B |
| `csaudioamdsof`（AMD SOF） | 12 | 64 B | 116 856 → 123 144 B |

体积增加全部来自签名块，代码段没有增长。INF、HWID 列表、安装逻辑一个字都没改，改的只有
运行时行为（`.sys`）与签名（`.sys` + `.cat`）。

---

## 二、为什么"设备全绿、端点满幅，却没有声音"

CoolStar 的音频驱动是按机器授权的收费组件。原始 `.sys` 里能直接读到这些字符串：

```
AVS: License not found. Please install it to C:\Windows\system32\drivers\avs_signedlicense.bin
AVS: License too small. Make sure the license is signed.
\SystemRoot\system32\DRIVERS\avs_signedlicense.bin
SystemSKU
Chell
```

驱动在初始化（`DriverEntry` → ADSP/AVS 启动）阶段会：

1. 从 `System32\drivers\avs_signedlicense.bin`（SOF 栈是内置 payload）读授权，校验签名、
   长度、以及和本机 SMBIOS `SystemSKU`（板卡代号，例如 `Chell`）的绑定关系；
2. 校验失败 → 返回 NTSTATUS `0xC000026A`；
3. 上层看到失败 → **ADSP 不起来**，I2S 时钟不输出，功放（MAX98357A / SSM4567）拿不到
   BCLK/LRCLK，一直挂在关断状态。

所以设备管理器全绿、端点齐全、播放测试音时电平表满幅 0.35，喇叭却没有声音：软件栈每一步
都成功，只是硬件那一层没被点亮。

---

## 三、补丁改了什么

只有两种形态，都围绕同一个常量 `0xC000026A`。

**A 类：错误分支直接返回。** 校验失败时把失败码写进返回值再跳到收尾，改成立即数 0 即可，
指令等长，偏移不动。

**B 类：三元表达式收尾。** MSVC 给 `return cond ? STATUS_X : STATUS_SUCCESS;` 生成的是

```asm
sbb  ebx, ebx          ; 0 或 -1
not  ebx               ; 取反
and  ebx, 0xC000026A   ; 条件成立得到失败码，否则得到 0
```

把 `and` 的立即数改成 0，结果恒为 0。原指令 6 字节、新指令 3 字节，用 `nop` 补平长度。

合计 29 处、153 字节：AVS 是 4 处 A 类 + 1 处 B 类，两个 SOF 栈各是 4 处 A 类 + 8 处 B 类。
AMD 包有 4 处改成 `0x0A` 而不是 0，`0x0A` 是正数，同样被判为成功，功能等价。

逐点清单见 **[PATCH-SITES.md](PATCH-SITES.md)**。

### 没被改动的部分

- **INF、HWID 表、DeviceDesc、Service 安装段**：与原始包逐字节相同
- **`CsAudioCallbackAPI` 回调注册**：原样保留，`csaudiosstavs` / `csaudiointcsof` 仍通过
  `\CallBack\CsAudioCallbackAPI` 向 CoolStar 总线驱动注册端点；`ssm4567` 功放驱动正是靠
  这条回调拿到 Speaker 端点
- **DSP 固件**（`dsp_basefw_kbl.bin`、`sof-*.ri`）：未改动
- 原始包里的 `cacert.exe`、`dpinst.exe`、`.sys.i64`（32 位版）在 payload 里被剔除，
  前两个是工具，第三个在纯 64 位安装里用不到

---

## 四、签名

原包是 WHQL 签名的。改一个字节，`.sys` 的嵌入签名立刻失效；而 catalog 里存着每个成员的
哈希，改过的 `.sys` 和旧 catalog 也对不上。所以修补必须连签名一起重做：重新生成 catalog，
再给 `.sys` 和 `.cat` 各签一次。

本仓库不附带任何证书。驱动包里的 catalog 是第三方签的，其时间戳链终止于一条公开根不信任
的证书，因此在干净系统上 `pnputil` 会拒绝：

```
无法添加驱动程序包: 尚未建立对 Authenticode(TM)签名的目录的发布者信任。   (0xE0000242)
```

需要把这条链的根导入 `LocalMachine\Root` 和 `LocalMachine\TrustedPublisher` 两个存储，
少一个都不行。本仓库用 [`certs/pikachu-certificate-install.exe`](../certs/) 做这件事，
安装器在装驱动之前会自动调用；手动安装时用 `scripts/trust-certs.bat` 包一层。

自己重签的完整流程（生成 `.cdf`、`inf2cat`/`makecat`、签名、导入信任根、验证）见
[FakeSign](https://github.com/PIKACHUIM/FakeSign)。

> 注意：`Get-AuthenticodeSignature` 返回 `Valid` **不代表 `pnputil` 会接受**。前者只验
> PKCS#7 结构与证书链，后者还要过 setupapi 的发布者信任检查。排查时最容易在这里绕圈。

两个与之相关的错误码值得记住：

| 错误码 | 含义 | 常见原因 |
|---|---|---|
| `0xE0000242` | 未建立对签名目录的发布者信任 | 根没导入，或只导了 `Root` 没导 `TrustedPublisher` |
| `0xE000024B` | 第三方 INF 不包含数字签名信息 | INF 里删掉了 `CatalogFile`，或 catalog 不存在 |

`CatalogFile = xxx.cat` 这一行必须留在 INF 里。同时 `.inf` 的字节一个都不能动（包括换行符），
否则新 catalog 里的哈希又对不上。`.gitattributes` 把 `*.inf` / `*.sys` / `*.cat` / `*.ri`
全部标成 `binary`，就是为了防止 Git 在检出时改换行符把签名弄坏。

---

## 五、和 akibaPatcher 的技术路线对比

[akibaGumi/akibaPatcher](https://github.com/akibaGumi/akibaPatcher) 处理的是
**SOF 1.0.5.0**，目标相同（绕过授权校验，让功放上电），但手法更粗：

| | 本仓库的修补包 | akibaPatcher v1 |
|---|---|---|
| 补丁粒度 | 5~12 处，改**返回值常量**（`0xC000026A` → `0`） | 4 处固定偏移，改**函数级返回** |
| 典型补丁 | `mov eax, 0xC000026A` → `mov eax, 0` | `31 C0 C3`（`xor eax,eax; ret`）、`B8 01 00 00 00 C3`（`mov eax,1; ret`）、`EB`（短跳） |
| 原 `.sys` 签名 | 保留不了 → **重新签** | **直接砍掉**证书表（清 security 目录 + 截断文件） |
| catalog | 重新生成 + 重新签 | `makecat.exe` 按 `.cdf` 重建 + 重签 |
| 是否要 testsigning | **不要**（只需导入 catalog 的信任根） | **必须要**（`bcdedit /set testsigning on`） |
| INF | 不改 | 改 `DriverVer` 为当天时间，让 Windows 优先选它 |
| 安装顺序 | 直接 `pnputil /add-driver` | 先跑原厂安装器，再从 DriverStore 里 `delete-driver` 掉 WHQL 版本，最后注入 |

一句话概括：

- **akiba 路线**：把签名整段扔掉，靠 testsigning 让系统放行。暴力、通用，但机器上要常开
  测试模式，每次开机桌面右下角带水印。
- **本仓库路线**：只改必要的返回值，然后用一条能过信任检查的链重新签。代价是需要一套可用的
  签名材料（见 [FakeSign](https://github.com/PIKACHUIM/FakeSign)），换来的是不需要测试模式。

---

## 六、这套 patch 做不到的事

- **不改硬件行为**。如果某机型的问题是 I2S 引脚定义、功放 GPIO 极性、或 ADSP 固件版本不匹配，
  改返回值救不了 —— 那时看到的是"驱动正常但依然无声"，需要改代码或换固件。
- **不跨版本通用**。偏移和字节都绑定具体编译产物（`1.0.4.0` / `1.0.1.0`）。CoolStar 换个版本
  重编，这些偏移全部失效，必须按 [PATCH-SITES.md](PATCH-SITES.md) 第六节的办法重新定位。
- **不能省掉信任根**。catalog 的签名时间来自非公共链时，目标机必须先导入根，
  否则 `pnputil` 直接拒绝。
