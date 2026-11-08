# ChromeBook CoolStar 音訊驅動整合包

[简体中文](README.md) | [English](README.en.md) | **繁體中文** | [日本語](README.ja.md)

把 Chromebook 裝上 Windows 之後，最難搞定的通常不是顯示卡、也不是觸控板，而是**聲音**。
這個倉庫把讓 Chromebook 出聲所需的驅動打成一個安裝包：CoolStar 的音訊堆疊、
它底下掛的 codec 與功率放大器，以及 ChromeOS 的嵌入式控制器匯流排、觸控板、指紋、攝影機驅動。

倉庫裡全部是**裸驅動檔案**（`INF + SYS + CAT`，少數附帶韌體 blob），沒有壓縮檔、
沒有自解壓安裝器。整棵工作樹 13 MB，會實際執行的外來程式只有微軟的 `DPInst` 與系統內建的 `certutil`。

> 這是**第三方重新打包**專案。驅動本體是 CoolStar 的作品（<https://coolstar.org/chromebook/>），
> 上游是依機器授權的付費元件。詳見 [聲明](#聲明)。

---

## 目錄

- [這是什麼](#這是什麼)
- [快速開始](#快速開始)
- [驅動清單](#驅動清單)
- [修補原理](#修補原理)
- [倉庫結構](#倉庫結構)
- [開發方式](#開發方式)
- [已驗證硬體](#已驗證硬體)
- [聲明](#聲明)

---

## 這是什麼

Chromebook 的音訊硬體和一般筆電完全不是同一回事：

| 一般筆電 | Chromebook |
|---|---|
| HD Audio codec 掛在 HDA 匯流排上，插上就出聲 | 音訊控制器是 Intel **AVS / SOF** 的 ADSP，codec 與放大器掛在 **I2C / I2S** 上 |
| codec 由 BIOS 初始化 | codec 需要驅動透過 I2C 逐暫存器設定 |
| 放大器整合在 codec 內 | 放大器是獨立晶片（MAX98357A / SSM4567 / RT1015…），要靠 GPIO + I2S 時脈啟動 |

所以缺驅動的現象不是「裝置管理員裡有個黃色驚嘆號」，而是更令人困惑的一種：
**裝置全綠、4 個音訊端點齊全、播放時端點電平表滿格，但喇叭一點聲音都沒有** ——
因為 ADSP 沒起來，I2S 沒有時脈，放大器一直處於斷電／靜音狀態。

這個包要解決的就是這件事。

---

## 快速開始

**直接安裝**：到 [Releases](../../releases) 下載最新的
`ChromeBook-CoolStar-Audio-Driver-<版本>-x64.exe`，右鍵以系統管理員身分執行，重新開機。

**自己編譯**（需要 [Inno Setup 6](https://jrsoftware.org/isinfo.php)，不需要 WDK / VS / 編譯器）：

```bat
ISCC.exe installer\Install.iss

:: 或指定版本號
ISCC.exe /DMyAppVersion=1.0.5 installer\Install.iss
```

產物會落在 `dist\`。

**只裝某幾個驅動**（例如只補喇叭放大器）：

```bat
:: 系統管理員 PowerShell，在解壓後的目錄裡
certutil -addstore -f Root             certs\pikachu-testca-root.cer
certutil -addstore -f TrustedPublisher certs\pikachu-testca-root.cer
pnputil /add-driver drivers\audio\ssm4567\ssm4567.inf /install
```

---

## 驅動清單

37 個驅動包、198 個硬體 ID。完整清單（含每個包的 DriverVer、Provider、全部 HWID）
在 **[docs/DRIVERS.md](docs/DRIVERS.md)**，由 `python tools/gen-driver-docs.py` 從 INF 自動產生。

> **已知缺口**：有 4 個包上游版本比這裡新、3 類包因故未收錄（其中包含 chell 需要的
> Intel Serial IO I2C/GPIO）。逐條原因與替代方案見 **[docs/VENDOR.md](docs/VENDOR.md)**。

### 音訊（`drivers/audio/`）

| 包 | 版本 | 適用平台 |
|---|---|---|
| `csaudiosstavs` | 1.0.4.0 | **AVS**：Skylake / Kaby Lake / Amber Lake / Braswell / Apollo Lake |
| `csaudiointcsof` | 1.0.4.0 | **SOF**：Gemini Lake / Jasper Lake / Ice Lake / Tiger Lake / Alder Lake… |
| `csaudioamdsof` | 1.0.1.0 | **AMD SOF**：Rembrandt / Phoenix 等 AMD Chromebook |
| `ssm4567` | 1.0.1 | ADI SSM4567 I2S 放大器（glados / chell，即 HP Chromebook 13 G1） |

> **只裝一個**。裝錯平台是「裝置在但沒聲音」最常見的原因：
> AVS 與 SOF 是兩套完全不同的架構，硬體 ID 不重疊，DPInst 會自動挑符合的那個。

### Codec（`drivers/codec/`）

`nau8825` · `nau88l25rvp` · `cs42l42` · `da7219` · `rt5682` · `rt5682s` · `croseccodec`

### 放大器（`drivers/amp/`）

`max98357a` · `max98390` · `opengmaxcodec` · `rt1011` · `rt1015` · `rt1019`

### 匯流排與系統（`drivers/bus/`、`drivers/system/`）

`acpbus` · `cbtable` · `crosecbus` · `croseci2c` · `crosectypec` · `sklhdaudbus` ·
`intelpmc` · `inteltcss` · `iaLPSS2_SPI` · `iaLPSS2_UART2` · `dptf_acpi` · `gnodriver`

### 輸入與攝影機（`drivers/input/`、`drivers/camera/`）

`crostouchpad-{synaptics,cypress,cypress-smbus,atmel,elan}` · `crosfingerprint` ·
`ov5675` · `iaisp64_46xx_ADL`

### 有裝但預設不啟用

| 包 | 原因 |
|---|---|
| `codec/nau88l25rvp` | Nuvoton 官方參考 codec，和 `codec/nau8825` 搶同一個 `ACPI\10508825`，互斥 |
| `system/gnodriver` | 只有 INF 的佔位驅動，沒有 `.sys` |

安裝順序寫在 [`installer/payload.list`](installer/payload.list)，安裝器與卸載器共用這一份清單。

---

## 修補原理

這一節回答兩個問題：**為什麼原版裝上也無聲**，以及**這個包到底改了什麼**。
完整證據鏈（逐位元組 diff、反組譯、簽章鏈對比、與 akibaPatcher 的路線對照）在
**[docs/PATCH-ANALYSIS.md](docs/PATCH-ANALYSIS.md)**。

### 1. 卡點：依機器授權的 license 檢查

CoolStar 的音訊驅動是授權收費的。原始 `.sys` 裡直接讀得到：

```
AVS: License not found. Please install it to C:\Windows\system32\drivers\avs_signedlicense.bin
AVS: License too small. Make sure the license is signed.
SystemSKU      Chell
```

`DriverEntry` 階段會讀授權檔、驗簽，並和 SMBIOS 的 `SystemSKU`（板卡代號）比對。
檢查失敗就回傳 NTSTATUS `0xC000026A`，上層據此**不啟動 ADSP** —— 於是 I2S 沒時脈、
放大器不上電，端點電平滿格卻完全無聲。

### 2. 補丁：只把「失敗」改成「成功」

三個堆疊一共改 5 / 12 / 12 處，合計 153 個位元組，全部是同一個模式：

```asm
; 原始 —— 驗證失敗就回傳錯誤碼
mov    eax, 0xC000026A
; 修補 —— 驗證失敗也回傳成功
mov    eax, 0
```

編譯器的 `return cond ? STATUS_X : STATUS_SUCCESS;` 會產生 `and reg, 0xC000026A`，
補丁把它變成 `and reg, 0`（後面用 `nop` 補齊長度）。

| 驅動堆疊 | 補丁點 | 改動位元組 |
|---|---|---|
| `csaudiosstavs` | 5 | 25 B |
| `csaudiointcsof` | 12 | 64 B |
| `csaudioamdsof` | 12 | 64 B |

**INF、HWID 表、服務安裝段、DSP 韌體一個字都沒動**，`CsAudioCallbackAPI` 回呼照舊
向 CoolStar 匯流排驅動註冊端點 —— `ssm4567` 放大器驅動正是靠這條回呼拿到 Speaker 端點。

### 3. 簽章：重新產生 catalog 並重簽

改一個位元組，WHQL 簽章就廢了。所以修補包是**重新簽**的，而且兩條鏈性質不同：

| 檔案 | 原始簽章者 | 修補後簽章者 | 時間戳 |
|---|---|---|---|
| `.sys` | Microsoft WHQL | Airspace Intelligent Technology (Changzhou) | DigiCert（**公開 TSA**） |
| `.cat` | Microsoft WHQL | 福建六壬網安股份有限公司 | Pikachu Time Sub CA（**自建 TSA**） |

`.sys` 的鏈可以追到公開信任根，所以關掉 Secure Boot 就能載入；
但 `.cat` 的時間戳來自自建 TSA，公開根不認，`pnputil` 會報：

```
0xE0000242  尚未建立對 Authenticode(TM)簽署的目錄的發行者信任
```

**所以必須先把自建 TSA 的根匯入目標機**（`certs/pikachu-testca-root.cer`，
安裝器會自動處理）：

```bat
certutil -addstore -f Root             certs\pikachu-testca-root.cer
certutil -addstore -f TrustedPublisher certs\pikachu-testca-root.cer
```

> 兩個坑，踩過就知道多浪費時間：
> ① `Get-AuthenticodeSignature` 回傳 `Valid` **不等於** `pnputil` 會接受 ——
>    前者只驗簽章本身，後者還要過 setupapi 的發行者信任檢查。
> ② `.inf` 的位元組一個都不能動（**包括換行符號**），因為 catalog 裡存的是它的雜湊。
>    這就是 `.gitattributes` 把 `*.inf`/`*.sys`/`*.cat` 全標成 `binary` 的原因。

### 4. 和 akibaPatcher 的差異

[akibaPatcher](https://github.com/akibaGumi/akibaPatcher) 也在做同一件事（SOF 1.0.5.0），
但路線更「粗」：直接砍掉 `.sys` 的 Authenticode 憑證表、偽造一張 20 年自簽憑證、
把 4 處函式改成 `xor eax,eax; ret` / `mov eax,1; ret`，**並且強制要求開 testsigning**。

本倉庫的包是「最小語意補丁 + 真實憑證鏈重簽」，代價是需要一條可用程式碼簽章憑證與
一個自己的時間戳服務，好處是**不需要 testsigning**，機器上不會留下測試模式浮水印。

---

## 倉庫結構

```
.
├── .github/workflows/release.yml   CI：填版本 → 建置 → 發 Release
├── certs/                          簽章鏈的根（安裝器會匯入 Root + TrustedPublisher）
├── docs/                           DRIVERS.md / PATCH-ANALYSIS.md / VENDOR.md
├── drivers/                        驅動本體，依功能分組
│   ├── audio/  codec/  amp/        ← 出聲相關的核心
│   ├── bus/    input/  camera/  system/
├── installer/                      Install.iss + payload.list + License.rtf + 圖示
├── reference/originals/            CoolStar 官方原始包（WHQL 簽章，做 diff 的基準）
├── scripts/                        preflight / trust-certs / install / uninstall
├── src/ssm4567/                    ADI SSM4567 放大器驅動原始碼（KMDF + I2C）
├── tools/                          dpinst.exe + gen-driver-docs.py + compare-drivers.py
└── vendor/                         本機暫存，git-ignored（見 docs/VENDOR.md）
```

---

## 開發方式

### 環境

- **打包**：Inno Setup 6（`choco install innosetup`）。不需要 WDK、不需要 Visual Studio。
- **改驅動原始碼**（`src/ssm4567`）：VS 2022 + WDK，注意 WDK 對 VS 版本有要求。
- **跑輔助腳本**：PowerShell 5.1 或 7，Python 3.9+（`capstone` 只在需要反組譯時用到）。

### 本機建置與自檢

```bat
:: 1) 驗證 payload（缺目錄、缺 inf、cat 引用的檔案不存在都會報錯）
powershell -ExecutionPolicy Bypass -File scripts\preflight.ps1

:: 2) 打包
ISCC.exe /DMyAppVersion=1.0.5 installer\Install.iss

:: 3) 改了驅動包之後，更新清單
python tools\gen-driver-docs.py

:: 4) 想知道某個包和官方原版差在哪
python tools\compare-drivers.py reference\originals\csaudiosstavs drivers\audio\csaudiosstavs
```

### CI

[`.github/workflows/release.yml`](.github/workflows/release.yml)：

| 觸發 | 用法 |
|---|---|
| `workflow_dispatch` | 填 `version`（如 `1.0.5`），可選「預發佈」→ 打 tag `v1.0.5` 並發 Release |
| push `v*` tag | 版本號取自 tag 名稱 |

流程：checkout → 解析版本 → `preflight.ps1` → choco 裝 Inno Setup →
`ISCC /DMyAppVersion=<v>` → 產生 `SHA256SUMS.txt` → 上傳 artifact →
`gh release create`（同名 tag 已存在則 `gh release upload --clobber`）。

`preflight` 失敗會直接中斷建置，壞 payload 進不了 Release。

### 新增 / 升級一個驅動

1. 把包放進 `drivers/<類別>/<名字>/`（`.inf` + `.sys` + `.cat`，附屬韌體放同一目錄）
2. 需要預設安裝的話，把 `<類別>\<名字>` 追加到 `installer/payload.list`
3. `python tools/gen-driver-docs.py` 更新清單
4. `powershell -File scripts/preflight.ps1` 自檢
5. 提交。驅動版本從 INF 讀，安裝包版本從 CI 輸入讀

### 卸載

「應用程式與功能」裡卸載，或系統管理員命令列：

```bat
scripts\uninstall-drivers.bat
```

---

## 已驗證硬體

| 機型 | 板卡代號 | 平台 | 結果 |
|---|---|---|---|
| HP Chromebook 13 G1 | `chell` | Skylake | ✅ 喇叭 + 耳機正常（需 `ssm4567` + `csaudiosstavs`） |

其餘機型依各堆疊的平台歸屬應當可用，但**沒有逐台驗證**。

---

## 聲明

- **CoolStar 音訊驅動** — © CoolStar，透過 <https://coolstar.org/chromebook/> 發佈，
  上游為依機器授權的付費元件。本倉庫是**非官方重新打包**，面向已持有授權的硬體，
  **不包含授權檔、授權產生器或啟用工具**。
- **`DPInst.exe`** — © Microsoft，隨 WDK 發佈。
- **`src/ssm4567`** — © CoolStar，來自 <https://github.com/coolstar/ssm4567>。
- **ChromeOS EC / 觸控板 / 攝影機 / DPTF 驅動** — 版權歸各自廠商。

與 Google、CoolStar 及任何硬體廠商均無隸屬關係。
