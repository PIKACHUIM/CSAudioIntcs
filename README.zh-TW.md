<div align="center">

# ChromeBook CoolStar 驅動包

集合數十種 ChromeBook 專用驅動，包括音效卡、觸控板等驅動

[简体中文](README.md) · [English](README.en.md) · **繁體中文** · [日本語](README.ja.md)

![platform](docs/badges/platform.svg) ![drivers](docs/badges/drivers.svg) ![installer](docs/badges/installer.svg) ![license](docs/badges/license.svg)

</div>

[專案簡介](#專案簡介) · [快速安裝](#快速安裝) · [驅動清單](#驅動清單) · [修補原理](#修補原理) · [授權條款](#授權條款)

---

## 專案簡介

本倉庫是 ChromeBook 平台在 Windows 下使用的驅動集合，依功能重新分類整理，共 37 個驅動包、198 個硬體 ID。

| 項 | 內容 |
|---|---|
| 驅動數量 | 37 個包，預設安裝 35 個 |
| 硬體 ID | 198 個 |
| 涵蓋範圍 | 音訊（AVS / SOF）、codec、放大器、觸控板、觸控螢幕、指紋、攝影機、EC 匯流排、晶片組配套元件 |
| 目標平台 | Intel Skylake 至 Alder Lake，AMD Rembrandt / Phoenix |
| 檔案形式 | 裸 `INF` + `SYS` + `CAT`，無壓縮檔與自解壓安裝程式 |
| 安裝工具 | Microsoft DPInst 2.1、Inno Setup 6 |

驅動取自 CoolStar 與 ChromeOS 各廠商的公開驅動包，版本與硬體 ID 保持原樣，僅調整目錄結構與安裝順序。安裝過程中執行的外部程式只有兩個：`tools/dpinst/dpinst.exe` 與 `certs/pikachu-certificate-install.exe`。

上游音訊驅動為按機器授權的商業軟體。本倉庫面向已持有授權的硬體，不包含授權檔、產生器或啟用工具。

```
.
├── drivers/            audio codec amp bus input camera system
├── installer/          Install.iss、payload.list（安裝順序）
├── scripts/            preflight、install、uninstall、trust-certs
├── certs/              簽章根安裝程式
├── reference/originals/  CoolStar 官方原包，僅用於差異比對
├── src/ssm4567/        ADI SSM4567 放大器驅動原始碼
├── tools/              DPInst、文件產生、二進位比對
├── docs/               DRIVERS.md、PATCH-ANALYSIS.md、VENDOR.md
└── vendor/             本機暫存，git 忽略
```

## 快速安裝

### 使用發佈包

1. 在 [Releases](../../releases) 下載 `ChromeBook-CoolStar-Audio-Driver-<版本>-x64.exe`
2. 以系統管理員身分執行
3. 安裝完成後重新開機

安裝程式會先匯入簽章根，再依 `payload.list` 逐項呼叫 DPInst，已存在同版本的驅動不會重複安裝。

### 從原始碼建置

相依只有 [Inno Setup 6](https://jrsoftware.org/isinfo.php)，不需要 WDK 與 Visual Studio。

```bat
:: 選用，校驗 payload 完整性
powershell -ExecutionPolicy Bypass -File scripts\preflight.ps1

:: 建置，產物在 dist\
ISCC.exe installer\Install.iss
ISCC.exe /DMyAppVersion=1.0.5 installer\Install.iss
```

CI 在 `.github/workflows/release.yml`，手動觸發時填寫版本號，或推送 `v*` 標籤。流程為 preflight、建置、產生 `SHA256SUMS.txt`、建立 Release。

### 單獨安裝某個驅動

適用於只缺一路裝置的情況。先匯入簽章根，再指定 INF。

```bat
:: 系統管理員 PowerShell
certs\pikachu-certificate-install.exe /VERYSILENT

pnputil /add-driver drivers\audio\ssm4567\ssm4567.inf /install
```

### 音訊堆疊選擇

三套音訊架構互不相容，硬體 ID 不重疊，需依平台選擇。

| 平台 | 驅動包 | 服務名 |
|---|---|---|
| Skylake、Kaby Lake、Amber Lake、Braswell、Apollo Lake | `csaudiosstavs` | `CsAudioSSTAVS` |
| Gemini Lake、Jasper Lake、Ice Lake、Tiger Lake、Alder Lake 及更新 | `csaudiointcsof` | `CsAudioIntcSOF` |
| AMD：Rembrandt、Phoenix 等 | `csaudioamdsof` | `CsAudioAmdSOF` |
| glados、chell 等板卡的喇叭放大器 | `ssm4567` | `ssm4567` |

完整安裝包含全部三套，DPInst 只綁定硬體 ID 相符的一項。`ssm4567` 為獨立放大器驅動，與音訊堆疊並行安裝。

## 驅動清單

完整版本號與 HWID 見 [docs/DRIVERS.md](docs/DRIVERS.md)，該檔由 `python tools/gen-driver-docs.py` 從 INF 產生。

### 音訊

| 包 | 版本 | 說明 |
|---|---|---|
| `csaudiosstavs` | 1.0.4.0 | AVS 架構，Skylake 至 Apollo Lake |
| `csaudiointcsof` | 1.0.4.0 | SOF 架構，Gemini Lake 及更新 |
| `csaudioamdsof` | 1.0.1.0 | AMD 平台 SOF |
| `ssm4567` | 1.0.1 | ADI I2S 放大器，glados 系列板卡 |

### Codec

`nau8825` · `nau88l25rvp` · `cs42l42` · `da7219` · `rt5682` · `rt5682s` · `croseccodec`

### 放大器

`max98357a` · `max98390` · `opengmaxcodec` · `rt1011` · `rt1015` · `rt1019`

### 匯流排與系統

`acpbus` · `cbtable` · `crosecbus` · `croseci2c` · `crosectypec` · `sklhdaudbus` · `intelpmc` · `inteltcss` · `iaLPSS2_SPI` · `iaLPSS2_UART2` · `dptf_acpi` · `gnodriver`

### 輸入與攝影機

`crostouchpad-{synaptics,cypress,cypress-smbus,atmel,elan}` · `crosfingerprint` · `ov5675` · `iaisp64_46xx_ADL`

兩個包隨倉庫發佈但不預設安裝：`nau88l25rvp` 與 `nau8825` 使用同一硬體 ID `ACPI\10508825`，預設啟用 CoolStar 版本；`gnodriver` 不含 `SYS`，僅作 INF 佔位。

安裝與卸載均以 [installer/payload.list](installer/payload.list) 為唯一清單。

## 修補原理

CoolStar 音訊驅動在 `DriverEntry` 中讀取授權檔、驗證簽章，並與 SMBIOS 中的板卡代號比對。校驗失敗時回傳 NTSTATUS `0xC000026A`，呼叫方收到該狀態碼後不再啟動 ADSP。ADSP 未啟動則 I2S 無時脈輸出，放大器維持關斷。

原始二進位中可直接讀到以下字串：

```
AVS: License not found. Please install it to C:\Windows\system32\drivers\avs_signedlicense.bin
AVS: License too small. Make sure the license is signed.
SystemSKU      Chell
```

因此裝置列舉、端點建立與音量表均正常，喇叭無聲。

### 修補位置

三個音訊堆疊分別修改 5、12、12 處，合計 153 位元組，形式一致：

```asm
mov eax, 0xC000026A    ; 校驗失敗的回傳值
mov eax, 0              ; 修改後直接回傳成功
```

原始碼形式為 `return cond ? STATUS_X : STATUS_SUCCESS;`，編譯後對應 `and reg, 0xC000026A`，修補改為 `and reg, 0`，剩餘位元組以 `nop` 填滿，指令長度不變。

INF、HWID 表、服務安裝段與 DSP 韌體均未修改。`CsAudioCallbackAPI` 回呼保持註冊，`ssm4567` 依賴該回呼註冊 Speaker 端點。

### 簽章

修改後原 WHQL 簽章失效，兩個檔案分別重新簽章。

| 檔案 | 原始簽章 | 目前簽章 | 時戳 |
|---|---|---|---|
| `.sys` | Microsoft WHQL | Airspace Intelligent Technology（常州） | DigiCert 公共 TSA |
| `.cat` | Microsoft WHQL | 福建六壬網安 | Pikachu Time Sub CA（自建） |

`.sys` 的憑證鏈可追溯至公共根，Secure Boot 關閉狀態下可載入。`.cat` 的時戳來自自建 TSA，公開根不信任，`pnputil` 回傳：

```
0xE0000242  尚未建立對 Authenticode(TM)簽署的類別目錄的發行者信任
```

`certs/pikachu-certificate-install.exe` 用於將此根寫入 `LocalMachine\Root` 與 `LocalMachine\TrustedPublisher`，兩個存放區缺一不可。

`Get-AuthenticodeSignature` 回傳 `Valid` 不代表 `pnputil` 接受該包：前者僅校驗 PKCS#7 結構與憑證鏈，後者還需通過 setupapi 的發行者信任檢查。同時 INF 檔案不可作任何位元組修改，包括換行字元，因為類別目錄中記錄了其雜湊，故 `.gitattributes` 將 `*.inf`、`*.sys`、`*.cat` 標記為 `binary`。

逐位元組差異、反組譯與實作方式對照見 [docs/PATCH-ANALYSIS.md](docs/PATCH-ANALYSIS.md)，比對工具為 `tools/compare-drivers.py`。

## 授權條款

本倉庫的腳本、文件、建置設定以 MIT 授權條款發佈，全文見 [LICENSE](LICENSE)。

倉庫內的驅動二進位版權歸各原廠所有，不適用 MIT：

| 元件 | 授權 |
|---|---|
| CoolStar 音訊驅動 | 商業授權，按機器計費，見 <https://coolstar.org/chromebook/> |
| `src/ssm4567` | CoolStar，見 `src/ssm4567/LICENSE.txt` |
| `tools/dpinst/dpinst.exe` | Microsoft，隨 WDK 發佈 |
| ChromeOS EC、觸控板、攝影機、DPTF 等驅動 | 各自廠商 |

MIT 僅涵蓋本倉庫新增內容，不改變上述元件的授權狀態。本倉庫與 Google、CoolStar 及任何硬體廠商無隸屬關係。Chromebook 安裝 Windows 存在變磚風險，修改韌體風險更高；僅安裝驅動可逆，卸載腳本會同時移除驅動與匯入的根憑證。
