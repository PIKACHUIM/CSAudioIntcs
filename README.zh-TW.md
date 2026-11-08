<div align="center">

# ChromeBook CoolStar 音訊驅動包

Chromebook 裝完 Windows 之後，把喇叭修好

[简体中文](README.md) · [English](README.en.md) · **繁體中文** · [日本語](README.ja.md)

![platform](https://img.shields.io/badge/Windows-x64-0078D4?logo=windows&logoColor=white)
![packages](https://img.shields.io/badge/drivers-37-2E7D32)
![installer](https://img.shields.io/badge/Inno_Setup-6-1F6FEB)
![upstream](https://img.shields.io/badge/upstream-paid_licence-8A8A8A)

</div>

---

## 簡介

這個倉庫一開始只是為了一台 HP Chromebook 13 G1。Windows 裝好了，裝置管理員一切正常，
音量滑桿會動，播放時端點的電平表也能打到滿格，喇叭就是不出聲。追到最後發現是驅動裡的
授權檢查沒過，ADSP 沒被啟動，擴大器一直掛在關斷狀態。

後來把能涵蓋到的機型都補齊了：CoolStar 的三套音訊堆疊、掛在她們底下的 codec 與擴大器，
還有 ChromeOS 的 EC 匯流排、觸控板、指紋、攝影機。

倉庫裡全是裸的 `INF + SYS + CAT`，沒有壓縮檔，也沒有自解壓安裝器。工作樹 13 MB，
安裝時真正會執行的外來程式只有微軟的 `DPInst` 和 `certs/` 裡那個 CA 安裝器。

上游是按機器授權收費的。這裡的驅動面向已經買過授權的硬體，不含授權檔，
也沒有授權產生或啟用工具。

## 先確定裝哪一套

Chromebook 的音訊架構依平台分成三套，互不相容，硬體 ID 也不重疊。裝錯會變成
「裝置在、沒聲音」這種最花時間排查的狀態。

| 你的平台 | 裝這個 | 服務名稱 |
|---|---|---|
| Skylake / Kaby Lake / Amber Lake / Braswell / Apollo Lake | `drivers/audio/csaudiosstavs` | `CsAudioSSTAVS` |
| Gemini Lake / Jasper Lake / Ice Lake / Tiger Lake / Alder Lake 及更新 | `drivers/audio/csaudiointcsof` | `CsAudioIntcSOF` |
| AMD Chromebook（Rembrandt、Phoenix 等） | `drivers/audio/csaudioamdsof` | `CsAudioAmdSOF` |
| glados / chell 板子的喇叭（HP Chromebook 13 G1 之類） | 另加 `drivers/audio/ssm4567` | `ssm4567` |

不確定平台的話，裝完整包就好。三套都在裡面，DPInst 只會綁定硬體 ID 對得上的那一個。

## 安裝

**用安裝包**：到 [Releases](../../releases) 下載 `ChromeBook-CoolStar-Audio-Driver-<版本>-x64.exe`，
按右鍵以系統管理員身分執行，重新開機。

**從原始碼建置**：只需要 [Inno Setup 6](https://jrsoftware.org/isinfo.php)，不需要 WDK、
不需要 Visual Studio。

```bat
ISCC.exe installer\Install.iss

:: 或指定版本號，產檔名會跟著變
ISCC.exe /DMyAppVersion=1.0.5 installer\Install.iss
```

輸出在 `dist\`。

**只補單一驅動**，例如只有喇叭不響。先信任簽章根，再裝套件：

```bat
:: 系統管理員 PowerShell，在解壓後的目錄裡
certs\pikachu-certificate-install.exe /VERYSILENT

pnputil /add-driver drivers\audio\ssm4567\ssm4567.inf /install
```

## 驅動清單

37 個套件，198 個硬體 ID。每個套件的版本、供應商、全部 HWID 在
[docs/DRIVERS.md](docs/DRIVERS.md)，那張表由 `python tools/gen-driver-docs.py`
從 INF 讀出來產生，不用手寫。

### 音訊

| 套件 | 版本 | 說明 |
|---|---|---|
| `csaudiosstavs` | 1.0.4.0 | AVS 架構，Skylake 到 Apollo Lake |
| `csaudiointcsof` | 1.0.4.0 | SOF 架構，Gemini Lake 及更新 |
| `csaudioamdsof` | 1.0.1.0 | AMD 平台的 SOF |
| `ssm4567` | 1.0.1 | ADI 的 I2S 擴大器，glados 系列板子用 |

### Codec

`nau8825` · `nau88l25rvp` · `cs42l42` · `da7219` · `rt5682` · `rt5682s` · `croseccodec`

### 擴大器

`max98357a` · `max98390` · `opengmaxcodec` · `rt1011` · `rt1015` · `rt1019`

### 匯流排與系統

`acpbus` · `cbtable` · `crosecbus` · `croseci2c` · `crosectypec` · `sklhdaudbus` ·
`intelpmc` · `inteltcss` · `iaLPSS2_SPI` · `iaLPSS2_UART2` · `dptf_acpi` · `gnodriver`

### 輸入與攝影機

`crostouchpad-{synaptics,cypress,cypress-smbus,atmel,elan}` · `crosfingerprint` ·
`ov5675` · `iaisp64_46xx_ADL`

### 有裝但預設不啟用

`codec/nau88l25rvp` 和 `codec/nau8825` 搶同一個 `ACPI\10508825`，只能留一個，
預設裝 CoolStar 那版。`system/gnodriver` 只有 INF 沒有 `sys`，裝上去也沒東西可綁。

安裝順序寫在 [installer/payload.list](installer/payload.list)，安裝與卸載都讀這一份。

## 原版為什麼不出聲

三套堆疊的原始 `.sys` 裡直接讀得到這幾行：

```
AVS: License not found. Please install it to C:\Windows\system32\drivers\avs_signedlicense.bin
AVS: License too small. Make sure the license is signed.
SystemSKU      Chell
```

`DriverEntry` 會讀授權檔、驗簽、跟 SMBIOS 裡的板卡代號比對。檢查不過就回傳
NTSTATUS `0xC000026A`，上層看到失敗就不再啟動 ADSP。沒有 ADSP 就沒有 I2S 時脈，
擴大器拿不到時脈就一直靜音。

所以現象才那麼反常：裝置、端點、電平全對，聲音一點都沒有。

### 補丁改了什麼

三個堆疊分別改了 5、12、12 處，合計 153 位元組。改的全是同一個模式：

```asm
mov eax, 0xC000026A    ; 原本：檢查失敗就回傳這個錯誤碼
mov eax, 0              ; 現在：照樣回報成功
```

`return cond ? STATUS_X : STATUS_SUCCESS;` 編出來是 `and reg, 0xC000026A`，
補丁把它變成 `and reg, 0`，多出來的位元組用 `nop` 填平。

INF、HWID 表、服務安裝段、DSP 韌體一個字沒動。`CsAudioCallbackAPI` 回呼照舊註冊，
`ssm4567` 擴大器驅動就是靠這條回呼拿到 Speaker 端點。

### 簽章怎麼處理

改一個位元組，WHQL 簽章就廢了，所以修補版是重新簽的。兩個檔案用的憑證還不一樣：

| 檔案 | 原始 | 現在 | 時間戳 |
|---|---|---|---|
| `.sys` | 微軟 WHQL | Airspace Intelligent Technology（常州） | DigiCert，公開 TSA |
| `.cat` | 微軟 WHQL | 福建六壬網安 | Pikachu Time Sub CA，自建 |

`.sys` 的鏈能追到公開根，關掉 Secure Boot 就能載入。`.cat` 的時間戳來自自建 TSA，
公開根不認，`pnputil` 會報 `0xE0000242`。這就是 `certs/` 裡那個安裝器存在的理由：
它在裝驅動之前跑一遍，把根寫進 `LocalMachine\Root` 和 `LocalMachine\TrustedPublisher`。

有個坑值得先講：`Get-AuthenticodeSignature` 回傳 `Valid` 不代表 `pnputil` 會收。
前者只驗 PKCS#7 本身，後者還要過 setupapi 的發行者信任檢查，兩件事。另外 INF 的位元組
一個都不能動，包含換行符號，因為 catalog 裡存著它的雜湊。`.gitattributes` 把
`*.inf`/`*.sys`/`*.cat` 全標成 `binary` 就是為了防這個。

逐位元組 diff、反組譯、和 [akibaPatcher](https://github.com/akibaGumi/akibaPatcher)
的做法對照，都在 [docs/PATCH-ANALYSIS.md](docs/PATCH-ANALYSIS.md)。

## 目錄結構

```
.
├── .github/workflows/release.yml   CI：填版本 → 建置 → 發 Release
├── certs/                          簽章根，一個 Inno Setup 安裝器
├── docs/
│   ├── DRIVERS.md                  驅動表，37 套件 / 198 HWID，腳本產生
│   ├── PATCH-ANALYSIS.md           修補原理與逐位元組差異
│   └── VENDOR.md                   哪些東西進不了倉庫，以及為什麼
├── drivers/                        驅動本體
│   ├── audio/  codec/  amp/        出聲相關的核心
│   ├── bus/    input/  camera/  system/
├── installer/
│   ├── Install.iss                 Inno Setup 腳本
│   ├── payload.list                安裝順序
│   └── License.rtf  dpinsts.ico
├── reference/originals/            CoolStar 官方原包，diff 基準，不安裝
├── scripts/
│   ├── preflight.ps1               打包前校驗 payload
│   ├── trust-certs.bat             手動信任簽章根
│   ├── install-drivers.bat         依 payload.list 呼叫 DPInst
│   └── uninstall-drivers.bat       同上，走 /U
├── src/ssm4567/                    ADI SSM4567 擴大器驅動原始碼，KMDF + I2C
├── tools/
│   ├── dpinst/dpinst.exe           Microsoft DPInst 2.1
│   ├── gen-driver-docs.py          產生 docs/DRIVERS.md
│   └── compare-drivers.py          原包與修補包逐位元組對比
└── vendor/                         本機暫存，git 忽略，見 docs/VENDOR.md
```

## 開發

打包環境只要 Inno Setup 6（`choco install innosetup`）。改 `src/ssm4567` 才需要
VS 2022 加 WDK，注意 WDK 對 VS 版本有要求。輔助腳本用 PowerShell 5.1 或 7，
Python 3.9 以上；`compare-drivers.py` 的反組譯部分要 `pip install capstone`。

```bat
:: 校驗 payload，缺目錄、缺 inf、catalog 引用的檔案不存在都會報錯
powershell -ExecutionPolicy Bypass -File scripts\preflight.ps1

:: 打包
ISCC.exe /DMyAppVersion=1.0.5 installer\Install.iss

:: 動過驅動套件之後更新清單
python tools\gen-driver-docs.py

:: 看看某個套件和官方原版差在哪
python tools\compare-drivers.py reference\originals\csaudiosstavs drivers\audio\csaudiosstavs
```

CI 在 [.github/workflows/release.yml](.github/workflows/release.yml)。手動觸發時填版本號，
或推一個 `v*` 標籤。流程是 preflight、裝 Inno Setup、`ISCC /DMyAppVersion=<v>`、
產生 `SHA256SUMS.txt`、傳 artifact、`gh release create`。同名 tag 已存在就改成上傳覆蓋。
preflight 不過就中斷，壞包進不了 Release。

要加驅動，把套件放進 `drivers/<類別>/<名稱>/`，需要預設安裝就追加到 `payload.list`，
跑一遍產生腳本和 preflight，提交。驅動版本從 INF 讀，安裝包版本從 CI 輸入讀。

卸載走「應用程式與功能」，或者 `scripts\uninstall-drivers.bat`。它依同一份清單逐個
`DPInst /U`，最後把 CA 一起卸掉。

## 已驗證的機器

| 機型 | 板卡 | 平台 | 結果 |
|---|---|---|---|
| HP Chromebook 13 G1 | `chell` | Skylake | 喇叭和耳機都正常，需要 `ssm4567` 加 `csaudiosstavs` |

其他機型依平台歸屬應該可用，但我沒有逐台試。裝完可以回報機型、板卡代號
（`SystemSKU`）和裝置管理員裡的硬體 ID。

## 已知缺口

有四個套件的上游版本比這裡新，三處東西故意沒收，原因和替代方案寫在
[docs/VENDOR.md](docs/VENDOR.md)。影響最大的是 Intel Serial IO 的 I2C/GPIO 驅動缺失，
chell 的 codec 和擴大器就掛在那條 I2C 匯流排上；手上只有 DriverStore 轉儲，
裡面沒有 catalog，湊不出能裝的套件。

## 聲明

CoolStar 音訊驅動 © CoolStar，透過 <https://coolstar.org/chromebook/> 發佈，上游按機器授權收費。
本倉庫是非官方重新打包，面向已持有授權的硬體，不含授權檔、產生器或啟用工具。
`DPInst.exe` © Microsoft，隨 WDK 發佈。`src/ssm4567` © CoolStar，來自
<https://github.com/coolstar/ssm4567>，授權見 `src/ssm4567/LICENSE.txt`。
ChromeOS EC、觸控板、攝影機、DPTF 驅動版權歸各自廠商。

與 Google、CoolStar 及任何硬體廠商都沒有隸屬關係。Chromebook 裝 Windows 有變磚風險，
尤其動韌體的時候；只裝驅動是可逆的。
