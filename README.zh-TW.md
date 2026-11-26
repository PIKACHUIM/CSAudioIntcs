<div align="center">

# ChromeBook CoolStar 驅動程式包

Chromebook 裝完 Windows，音效卡、觸控板、攝影機常常一個都認不出來。
這個倉庫把 CoolStar 和各 ChromeOS 廠商的驅動程式收在一起，做成一個安裝程式。

[简体中文](README.md) · [English](README.en.md) · **繁體中文** · [日本語](README.ja.md)

![platform](docs/badges/platform.svg) ![drivers](docs/badges/drivers.svg) ![installer](docs/badges/installer.svg) ![license](docs/badges/license.svg)

[下載安裝檔](../../releases) · [驅動清單](docs/DRIVERS.md) · [修補記錄](docs/PATCH-ANALYSIS.md) · [版本差異](docs/UPSTREAM.md) · [授權](LICENSE)

</div>

---

## 這是什麼

倉庫裡有 36 個驅動程式包、171 個硬體 ID，預設安裝 35 個。

| 項目 | 內容 |
|---|---|
| 驅動程式包 | 36 個，其中 35 個寫進安裝清單 |
| 硬體 ID | 171 個 |
| 涵蓋裝置 | 音訊控制器、codec、放大器、觸控板、觸控螢幕、指紋、攝影機、EC 匯流排、DPTF |
| 平台 | Intel Skylake 到 Alder Lake，AMD Rembrandt / Phoenix |
| 檔案形式 | 純 `INF` + `SYS` + `CAT`，沒有壓縮檔和自解壓安裝程式 |
| 安裝工具 | Microsoft DPInst 2.1、Inno Setup 6 |

驅動程式來自 CoolStar 與各 ChromeOS 硬體廠商的公開包。版本號和硬體 ID 都沒改，動的只有目錄結構和安裝順序。

安裝過程會執行兩個外部程式：`tools/dpinst/dpinst.exe` 和 `certs/pikachu-certificate-install.exe`。前者裝驅動程式，後者把驅動 catalog 的簽章根匯入系統。其他沒有了。

CoolStar 的音效驅動程式是按機器授權的商業軟體。本倉庫面向已經買過授權的機器，不包含授權檔、產生器或啟用工具。

```
.
├── drivers/             驅動程式本體，按 audio codec amp bus input camera system 分類
├── installer/           Install.iss（打包腳本）、payload.list（安裝順序）
├── scripts/             preflight、安裝、移除、匯入根憑證
├── certs/               簽章根安裝程式
├── reference/originals/ CoolStar 官方原包，用來做差異比對
├── src/ssm4567/         SSM4567 放大器驅動程式原始碼
├── tools/               DPInst、文件產生、二進位比對
└── docs/                驅動清單、修補記錄、簽章狀態、版本差異
```

## 安裝

### 用發佈包

1. 在 [Releases](../../releases) 下載 `ChromeBook-CoolStar-Audio-Driver-<版本>-x64.exe`
2. 按右鍵以系統管理員身分執行
3. 裝完重新開機

安裝程式先匯入簽章根，再照 `installer/payload.list` 的順序逐一呼叫 DPInst。同版本已經裝過就跳過，不會重複覆蓋。

裝的是 x64 系統。32 位元 Windows 沒有對應的驅動程式包。

### 只缺一個驅動程式

例如只有觸控板不工作。先匯入根，再用 `pnputil` 指定 INF：

```bat
:: 系統管理員 PowerShell
certs\pikachu-certificate-install.exe /VERYSILENT

pnputil /add-driver drivers\input\crostouchpad-synaptics\crostouchpad.inf /install
```

要移除的話跑 `scripts\uninstall-drivers.bat`，它會把匯入的根憑證一起刪掉。

### 音效架構選哪個

三套音效架構互不相容，硬體 ID 也不重疊，DPInst 只會綁定符合的那一個。整包安裝不用挑，只裝單一個時才需要按平台選。

| 平台 | 套件 | 服務名稱 |
|---|---|---|
| Skylake、Kaby Lake、Amber Lake、Braswell、Apollo Lake | `csaudiosstavs` | `CsAudioSSTAVS` |
| Gemini Lake、Jasper Lake、Ice Lake、Tiger Lake、Alder Lake 及更新 | `csaudiointcsof` | `CsAudioIntcSOF` |
| AMD Rembrandt、Phoenix 等 | `csaudioamdsof` | `CsAudioAmdSOF` |
| glados、chell 這類板卡的喇叭放大器 | `ssm4567` | `ssm4567` |

`ssm4567` 是獨立的放大器驅動程式，和上面三套並行安裝，不衝突。

## 驅動清單

完整版本號和每一條 HWID 在 [docs/DRIVERS.md](docs/DRIVERS.md)，那份檔案由 `python tools/gen-driver-docs.py` 從 INF 產生，不要手改。

**音訊**：`csaudiosstavs` 1.0.4.0 · `csaudiointcsof` 1.0.4.0 · `csaudioamdsof` 1.0.1.0 · `ssm4567` 1.0.1

**Codec**：`nau8825` · `nau88l25rvp` · `cs42l42` · `da7219` · `rt5682` · `rt5682s` · `croseccodec`

**放大器**：`max98357a` · `max98390` · `opengmaxcodec` · `rt1011` · `rt1015` · `rt1019`

**匯流排與系統**：`acpbus` · `cbtable` · `crosecbus` · `croseci2c` · `crosectypec` · `sklhdaudbus` · `intelpmc` · `inteltcss` · `iaLPSS2_SPI` · `iaLPSS2_UART2` · `dptf_acpi`

**輸入與攝影機**：`crostouchpad-{synaptics,cypress,cypress-smbus,atmel,elan}` · `crosfingerprint` · `ov5675` · `iaisp64_46xx_ADL`

`nau88l25rvp` 和 `nau8825` 搶同一個硬體 ID `ACPI\10508825`，預設用 CoolStar 那份，所以它隨倉庫發佈但不進安裝清單。安裝和移除都以 [installer/payload.list](installer/payload.list) 為準。

## 原版為什麼沒聲音

CoolStar 音效驅動程式在 `DriverEntry` 裡讀授權檔、驗簽章，再和 SMBIOS 裡的板卡代號比對。驗不過就回傳 `0xC000026A`，上層拿到這個狀態碼就不啟動 ADSP。ADSP 不跑，I2S 就沒有時鐘，放大器一直關著。

於是現象很怪：裝置管理員全綠，端點齊全，放測試音時電平表也在跳，喇叭就是沒聲音。

二進位裡能直接讀到這幾句：

```
AVS: License not found. Please install it to C:\Windows\system32\drivers\avs_signedlicense.bin
AVS: License too small. Make sure the license is signed.
SystemSKU
Chell
```

三個音效堆疊一共改了 29 處、153 位元組，都是同一套做法：把失敗碼的立即值改掉，讓回傳值恆為成功。不是錯誤分支裡的 `mov eax, 0xC000026A`，就是 `return cond ? STATUS_X : STATUS_SUCCESS;` 編譯出來的 `sbb / not / and`。

INF、HWID 表、服務安裝段和 DSP 韌體一個位元組都沒動。`CsAudioCallbackAPI` 回呼也留著，`ssm4567` 靠它註冊 Speaker 端點。

逐點偏移和反組譯見 [docs/PATCH-SITES.md](docs/PATCH-SITES.md)，為什麼這樣改見 [docs/PATCH-ANALYSIS.md](docs/PATCH-ANALYSIS.md)。

## 簽章

驅動程式包本身都有 catalog，但簽章來源分三種，差別在裝之前要不要多一步。

| 分類 | 套件數 | 額外動作 |
|---|---|---|
| Microsoft WHQL | 24 | 不需要 |
| 第三方簽章，鏈到公開根 | 5 | 不需要（觸控板那五個） |
| 第三方簽章，鏈到非公開根 | 7 | 先把根匯入信任存放區 |

最後那 7 個套件本身沒壞，只是 catalog 的時間戳鏈停在一張公開根不認的憑證上。沒匯入的話 `pnputil` 會停在這裡：

```
0xE0000242  尚未建立對 Authenticode(TM)簽名的目錄的發佈者信任
```

`certs/pikachu-certificate-install.exe` 同時寫 `LocalMachine\Root` 和 `LocalMachine\TrustedPublisher`，兩個存放區少一個都不行。安裝程式會在裝驅動程式之前自動跑它。

有個地方容易繞圈：`Get-AuthenticodeSignature` 回傳 `Valid` 不代表 `pnputil` 會收。前者只驗 PKCS#7 和憑證鏈，後者還要過 setupapi 的發佈者信任檢查。逐套件狀態在 [docs/SIGNATURES.md](docs/SIGNATURES.md)。

要自己幫改過的 `.sys` 重簽，得重建 catalog 再簽兩次，流程在 [FakeSign](https://github.com/PIKACHUIM/FakeSign)。倉庫裡沒有憑證，也沒有私鑰。

### 安裝檔本身的簽章

Actions 建出來的 `exe` 沒有 Authenticode 簽章，第一次執行會被 SmartScreen 攔一下，按「仍要執行」就好，不影響安裝。

要正式簽，用自己的憑證在本機簽。簽章會改變檔案雜湊，所以簽完要重算 `SHA256SUMS.txt`：

```powershell
signtool sign /fd SHA256 /f mycert.pfx /p <密碼> `
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

再用 `gh release upload v<版本> dist\*.exe dist\SHA256SUMS.txt --clobber` 覆蓋上去。

## 自己建置

只需要 [Inno Setup 6](https://jrsoftware.org/isinfo.php)，不用 WDK，也不用 Visual Studio。

```bat
:: 可選，檢查 payload 有沒有缺檔案
powershell -ExecutionPolicy Bypass -File scripts\preflight.ps1

ISCC.exe installer\Install.iss
ISCC.exe /DMyAppVersion=26.11.26 installer\Install.iss
```

產物在 `dist\`。不改版本號時用 `Install.iss` 裡寫的那一個。

CI 在 `.github/workflows/release.yml`：手動觸發時填版本號，或推 `v*` 標籤。流程是 preflight → 建置 → 產生 `SHA256SUMS.txt` → 建立 Release。

## 授權

倉庫裡的腳本、文件和建置設定用 MIT，全文見 [LICENSE](LICENSE)。

驅動程式二進位的著作權歸各原廠，不在 MIT 範圍內：

| 元件 | 授權 |
|---|---|
| CoolStar 音效驅動程式 | 商業授權，按機器計費，見 <https://coolstar.org/chromebook/> |
| `src/ssm4567` | CoolStar，見 `src/ssm4567/LICENSE.txt` |
| `tools/dpinst/dpinst.exe` | Microsoft，隨 WDK 散佈 |
| ChromeOS EC、觸控板、攝影機、DPTF 等 | 各廠商 |

MIT 只涵蓋本倉庫新增的部分，不改變上面這些元件的授權狀態。本倉庫和 Google、CoolStar 以及任何硬體廠商都沒有隸屬關係。

Chromebook 裝 Windows 有變磚風險，改韌體風險更高。只裝驅動程式是可逆的，移除腳本會把驅動程式和匯入的根憑證一起清掉。
