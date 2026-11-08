# 修补原理与差异分析

本文记录 `drivers/audio/` 里三个 CoolStar 音频栈与 CoolStar 官方原始包逐字节的差异，
以及这些差异在代码层面意味着什么。所有数据都可以用
[`tools/compare-drivers.py`](../tools/compare-drivers.py) 重新生成：

```bat
python tools/compare-drivers.py reference\originals\csaudiosstavs drivers\audio\csaudiosstavs
```

原始包放在 [`reference/originals/`](../reference/originals/)（微软 WHQL 签名，未改动），
修补后的包就是安装器实际投放的 payload。

---

## 一、结论摘要

| | 原始包 | 修补包 |
|---|---|---|
| `.inf` | 基准 | **完全一致**（DriverVer、Provider、HWID、CatalogFile 都没动） |
| `.sys` | WHQL 签名 | 改 5 / 12 / 12 处代码，重新签名 |
| `.cat` | WHQL 签名 | 重新生成 catalog，重新签名 |
| 额外文件 | 带 `dpinst.exe`、`cacert.exe`、`.sys.i64`（32 位版） | 只留驱动本体 |

也就是说：**INF、HWID 列表、安装逻辑一个字都没改**，改的只有运行时行为（`.sys`）
和签名（`.sys` + `.cat`）。这是"最小改动"路线，也是它能不经 testsigning 直接装上的原因。

| 驱动栈 | 补丁点 | 改动字节 | `.sys` 体积变化 |
|---|---|---|---|
| `csaudiosstavs`（AVS，Skylake 世代） | 5 | 25 B | 106 672 → 119 048 B（+12 376） |
| `csaudiointcsof`（SOF，Gemini Lake 及更新） | 12 | 64 B | 160 480 → 166 664 B（+6 184） |
| `csaudioamdsof`（AMD SOF） | 12 | 64 B | 116 856 → 123 144 B（+6 288） |

体积增加全部来自**签名块变大**（换了一条更长的证书链 + 时间戳），代码段没有增长。

---

## 二、为什么"设备全绿、端点满幅，却没有声音"

CoolStar 的音频驱动是**按机器授权**的收费组件。原始 `.sys` 里能直接读到这些字符串：

```
AVS: License not found. Please install it to C:\Windows\system32\drivers\avs_signedlicense.bin
AVS: License too small. Make sure the license is signed.
\SystemRoot\system32\DRIVERS\avs_signedlicense.bin
SystemSKU
Chell
```

驱动在初始化（`DriverEntry` → ADSP/AVS 启动）阶段会：

1. 从 `System32\drivers\avs_signedlicense.bin`（SOF 栈是内置 payload）读授权，
   校验签名、长度、以及和本机 SMBIOS `SystemSKU`（板卡代号，例如 `Chell`）的绑定关系；
2. 校验失败 → 返回一个 NTSTATUS 错误码 `0xC000026A`；
3. 上层看到失败 → **ADSP 不起来**，于是 I2S 时钟不输出，功放（MAX98357A / SSM4567）
   拿不到 BCLK/LRCLK，永久静音。

这就解释了那个很反直觉的现象：设备管理器全绿、4 个音频端点齐全、
播放测试音时端点电平表满幅 0.35，但喇叭一点声音都没有 ——
软件栈全都正常，只是**硬件那一层没被点亮**。

---

## 三、补丁到底改了什么

所有补丁点都是同一个模式：把编译器为

```c
return cond ? STATUS_XXX : STATUS_SUCCESS;
```

生成的 `and reg, 0xC000026A` / `mov reg, 0xC000026A`，改成 `and reg, 0` / `mov reg, 0`。
`0xC000026A` 是那个"授权校验失败"的返回码。

### 3.1 `csaudiosstavs`（5 处，25 字节）

| 文件偏移 | 位置 | 原始 | 修补 | 语义 |
|---|---|---|---|---|
| `0x003BCB` | `.text` VA `0x1400047CB` | `6A 02 00 C0` | `00 00 00 00` | `mov eax, 0xC000026A` → `mov eax, 0` |
| `0x003C66` | `.text` VA `0x140004866` | `6A 02 00 C0` | `00 00 00 00` | 同上 |
| `0x0042D9` | `.text` VA `0x140004ED9` | `25 6A 02 00 C0` | `83 E0 00 90 90` | `and eax, 0xC000026A` → `and eax, 0`（+2 nop） |
| `0x004A83` | `.text` VA `0x140005683` | `81 E3 6A 02 00 C0` | `83 E3 00 90 90 90` | `and ebx, 0xC000026A` → `and ebx, 0`（+3 nop） |
| `0x004DE0` | `.text` VA `0x1400059E0` | `81 E3 6A 02 00 C0` | `83 E3 00 90 90 90` | 同上 |

`mov eax, 0xC000026A` 出现在这样的上下文里（原始反汇编）：

```asm
1400047B5  call   0x1400077e8          ; 授权校验
1400047BA  test   eax, eax
1400047BC  jns    0x1400047d4          ; 成功 → 继续初始化 ADSP
1400047BE  lea    rcx, [rip+0x622b]    ; "AVS: License not found..."
1400047C5  call   0x1400091a0          ; 打日志
1400047CA  mov    eax, 0xC000026A      ; ← 返回失败
1400047CF  jmp    0x140004970          ; 直接跳去收尾
```

修补后 `mov eax, 0`，校验函数返回失败时代码**仍走错误分支**（会打日志、会跳过 ADSP
初始化的一部分），但对上层报告的 status 变成 `STATUS_SUCCESS`，调用方据此认为
"初始化完成"，继续把音频栈拉起来 —— 端点因此可以工作。

### 3.2 `csaudiointcsof` / `csaudioamdsof`（各 12 处，64 字节）

分布是 4 处 `mov`、8 处 `and`：

| 驱动 | `mov` 类（4 B） | `and` 类（6 B） |
|---|---|---|
| `csaudiointcsof` | `0x33AD` `0x428D` `0x62F9` `0x71D9` | `0x4C8B` `0x572D` `0x5FF5` `0x6156` `0x7BD7` `0x8679` `0x8F41` `0x90A2` |
| `csaudioamdsof` | `0x2C39` `0x3B2E` `0x5BB9` `0x6AAE` | `0x4525` `0x4FC5` `0x588D` `0x5A03` `0x74A5` `0x7F45` `0x880D` `0x8983` |

AMD 包有个细节：三处 `and ebx, 0xC000026A` 被改成 `and ebx, 0x0A`
（`83 E3 0A`）而不是 0，`mov eax` 也有一处改成 `mov eax, 0xA`。
`0x0A` 依然是**非负值**，而调用方用 `test/jns` 判成功，所以效果等价于 0；
推测是作者为了让某些"status != 0 就打 warning"的分支仍然留下痕迹。

### 3.3 没被改动的部分

- **HWID 表、DeviceDesc、Service 安装段**：INF 与原始包逐字节相同
- **`CsAudioCallbackAPI` 回调注册**：原样保留，`csaudiosstavs` / `csaudiointcsof`
  仍然通过 `\CallBack\CsAudioCallbackAPI` 向 CoolStar 总线驱动注册端点；
  `ssm4567` 功放驱动正是靠这条回调拿到 Speaker 端点
- **DSP 固件**（`dsp_basefw_kbl.bin`、`sof-*.ri`）：未改动
- 原始包里的 `cacert.exe`（装测试根证书）、`dpinst.exe`、`.sys.i64`（32 位）
  在 payload 里被剔除 —— 前两个是工具，第三个在纯 64 位安装里用不到

---

## 四、签名是怎么处理的（这才是关键）

CoolStar 原包是 WHQL 签名的，改一个字节签名就废。修补包的做法是**重新签**：

| 文件 | 原始包签名者 | 修补包签名者 | 时间戳 |
|---|---|---|---|
| `.sys`（三个栈） | `Microsoft Windows Hardware Compatibility Publisher`（WHQL） | `Airspace Intelligent Technology (Changzhou) Co., Ltd.` | DigiCert Timestamp 2024（**公共 TSA**） |
| `.cat`（三个栈） | 同上（WHQL） | `福建六壬网安股份有限公司` | `Pikachu Time Sub CA`（**自建 TSA**） |

两条链的性质完全不同，这直接决定了安装方式：

- `.sys` 的签名链能追到公共信任根（DigiCert），所以在任何**关掉 Secure Boot**
  的机器上都能加载，不需要 testsigning；
- `.cat` 的时间戳来自自建 TSA，公共根不认。所以 `pnputil` 添加驱动包时会报：

  ```
  无法添加驱动程序包: 尚未建立对 Authenticode(TM)签名的目录的发布者信任。   (0xE0000242)
  ```

  必须先把这条时间戳链的**根**装进 `LocalMachine\Root` 和
  `LocalMachine\TrustedPublisher`。本仓库用 [`certs/pikachu-certificate-install.exe`](../certs/)
  做这件事（Inno Setup 打包，`/VERYSILENT` 静默安装，同时写两个 store 并注册卸载项）。
  安装器在装驱动之前会自动调用它；手动装驱动时用 `scripts/trust-certs.bat` 包一层。

> 注意：`Get-AuthenticodeSignature` 返回 `Valid` **不代表 pnputil 会接受**。
> 前者只验 PKCS#7 本身，后者还要过 setupapi 的发布者信任检查。两者是两码事，
> 排查时最容易在这里绕圈。

`.cat` 是用 `Inf2Cat`/`makecat` 重新生成的（原始 `.cat` 里存的是**原始**
`.inf`/`.sys` 的哈希，改过 `.sys` 之后必然对不上），所以
`CatalogFile = xxx.cat` 这一行必须留在 INF 里 —— 删掉它反而会得到另一个错误
（`0xE000024B 第三方 INF 不包含数字签名信息`）。

同理，`.inf` 的字节**一个都不能动**（包括换行符），否则新 catalog 里的哈希又对不上。
这也是 `.gitattributes` 把 `*.inf`/`*.sys`/`*.cat` 全部标成 `binary` 的原因。

---

## 五、和 akibaPatcher 的技术路线对比

[akibaGumi/akibaPatcher](https://github.com/akibaGumi/akibaPatcher) 处理的是
**SOF 1.0.5.0**，目标相同（绕过授权 + 硬件 ID 校验，让功放上电），但手法更"粗"：

| | 本仓库的修补包 | akibaPatcher v1 |
|---|---|---|
| 补丁粒度 | 5~12 处，改**返回值常量**（`0xC000026A` → `0`） | 4 处固定偏移，改**函数级返回** |
| 典型补丁 | `mov eax, 0xC000026A` → `mov eax, 0` | `31 C0 C3`（`xor eax,eax; ret`）、`B8 01 00 00 00 C3`（`mov eax,1; ret`）、`EB`（短跳） |
| 原 `.sys` 签名 | 保留不了 → **重新签** | **直接砍掉**证书表（清 security 目录 + 截断文件） |
| catalog | 重新生成 + 重新签 | `makecat.exe` 按 `.cdf` 重建 + 重签 |
| 证书 | 第三方的代码签名证书（链可追到公共根） | 现场伪造 20 年自签 `CN=Akiba Audio Corporation` |
| 是否要 testsigning | **不要**（只需信任 cat 的 TSA 根） | **必须要**（`bcdedit /set testsigning on`） |
| INF | 不改 | 改 `DriverVer` 为当天时间，让 Windows 优先选它 |
| 安装顺序 | 直接 `pnputil /add-driver` | 先跑原厂安装器，再从 DriverStore 里 `delete-driver` 掉 WHQL 版本，最后注入 |

一句话概括两种思路：

- **akiba 路线**：把签名整段扔掉，靠 testsigning 让系统放行 → 暴力、通用，
  但机器上要常开测试模式，且每次开机桌面右下角会有水印；
- **本仓库路线**：只改必要的返回值，然后用一条**能过信任检查的链**重新签，
  代价是需要一条可用的代码签名证书 + 一个自己的时间戳服务。

---

## 六、这套 patch 做不到的事

- **不改硬件行为**。如果某机型的问题是 I2S 引脚定义、功放 GPIO 极性、或
  ADSP 固件版本不匹配，改返回值也救不了 —— 那时看到的是"驱动正常但依然无声"，
  需要改代码或换固件。
- **不跨版本通用**。偏移和字节都绑定具体编译产物（`1.0.4.0` / `1.0.1.0`）。
  CoolStar 换个版本重编，这些偏移全部失效，必须重新定位。
- **不能省掉信任根**。哪怕 `.sys` 的链是公共可信的，`.cat` 那条自建 TSA 依然
  要求目标机导入根证书，否则 `pnputil` 直接拒绝。
