<div align="center">

# ChromeBook CoolStar 音訊驅動包

Chromebook 裝完 Windows 喇叭不響，這裡有一整套能出聲的驅動

[简体中文](README.md) · [English](README.en.md) · **繁體中文** · [日本語](README.ja.md)

![platform](docs/badges/platform.svg) ![drivers](docs/badges/drivers.svg) ![installer](docs/badges/installer.svg) ![upstream](docs/badges/upstream.svg)

</div>

[這是什麼](#這是什麼) · [先確認裝哪一套](#先確認裝哪一套) · [安裝](#安裝) · [驅動清單](#驅動清單) · [原版為什麼不出聲](#原版為什麼不出聲) · [目錄結構](#目錄結構) · [開發](#開發) · [跑過的機器](#跑過的機器) · [已知缺口](#已知缺口) · [聲明](#聲明)

---

## 這是什麼

手上這台 HP Chromebook 13 G1 裝完 Windows 之後，裝置管理員乾乾淨淨，音量滑桿能拖，播測試音時喇叭端點的音量表打到 0.35 滿格，喇叭一點聲音都沒有。

一開始猜是放大器沒上電，換驅動版本、刷 coreboot、關快速啟動，一樣沒動。後來把 CoolStar 的 `.sys` 丟進十六進位編輯器翻字串，看到一句 `AVS: License not found`，路徑是 `C:\Windows\system32\drivers\avs_signedlicense.bin`。授權檢查沒過，`DriverEntry` 回傳 `0xC000026A`，上層拿到這個碼就不啟動 ADSP。ADSP 不起來，I2S 沒有時脈，放大器就一直掛在關斷狀態。

裝置列舉、端點建立、音量表都正常，聲音沒有。這個組合看起來像硬體故障，實際上是軟體把後半段切掉了。細節在[原版為什麼不出聲](#原版為什麼不出聲)。

倉庫裡是這輪排查累積下來的東西。三套 CoolStar 音訊堆疊，掛在底下的 codec 和放大器，另外配齊了 ChromeOS 的 EC 匯流排、觸控板、指紋、攝影機。

裡面全是裸的 `INF` + `SYS` + `CAT`，沒有壓縮檔，也沒有自解壓安裝程式。安裝過程中會執行的外部程式只有兩個，微軟的 `DPInst` 和 `certs/` 裡那個憑證安裝程式，都可以查。

上游是按機器賣授權的。這裡的東西面向已經買過授權的機器，倉庫裡不含授權檔，也沒有產生器或啟用工具。

## 先確認裝哪一套

Chromebook 的音訊按平台分成三套，互不相容，硬體 ID 也不重疊。

| 平台 | 裝這個 | 服務名 |
|---|---|---|
| Skylake、Kaby Lake、Amber Lake、Braswell、Apollo Lake | `drivers/audio/csaudiosstavs` | `CsAudioSSTAVS` |
| Gemini Lake、Jasper Lake、Ice Lake、Tiger Lake、Alder Lake 及更新 | `drivers/audio/csaudiointcsof` | `CsAudioIntcSOF` |
| AMD 的 Chromebook，Rembrandt、Phoenix 這些 | `drivers/audio/csaudioamdsof` | `CsAudioAmdSOF` |
| glados、chell 這類板子的喇叭，例如 HP Chromebook 13 G1 | 再加一個 `drivers/audio/ssm4567` | `ssm4567` |

最後一行值得單獨講。Skylake 時代的 Chromebook 把喇叭放大器掛在 I2C 上，耳機走另一顆 codec，兩條路各管各的。耳機有聲、喇叭沒聲，缺的就是 `ssm4567` 這個包，跟主驅動版本對不對無關。

拿不準就裝完整包。三套都在裡面，`DPInst` 只綁硬體 ID 對得上的那一個，裝進去的另外兩個不會生效。

## 安裝

去 [Releases](../../releases) 取 `ChromeBook-CoolStar-Audio-Driver-<版本>-x64.exe`，以系統管理員身分執行，裝完重新開機。

自己建置只要 [Inno Setup 6](https://jrsoftware.org/isinfo.php)，不用 WDK，也不用 Visual Studio。

```bat
ISCC.exe installer\Install.iss

:: 帶版本號的話，產物名稱會跟著變
ISCC.exe /DMyAppVersion=1.0.5 installer\Install.iss
```

產物落在 `dist\`。

只壞了一路的話可以分開處理，先信任簽章根，再上驅動包：

```bat
:: 系統管理員 PowerShell，在解壓後的目錄裡執行
certs\pikachu-certificate-install.exe /VERYSILENT

pnputil /add-driver drivers\audio\ssm4567\ssm4567.inf /install
```

`pnputil` 會在 `ACPI\INT343B\0` 和 `\1` 上各綁一次，成功後裝置管理員裡出現兩個 `Analog Devices SSM4567 Amplifier`，狀態正常。

## 驅動清單

37 個包，198 個硬體 ID。每個包的版本、供應商、全部 HWID 在 [docs/DRIVERS.md](docs/DRIVERS.md)。那張表是 `python tools/gen-driver-docs.py` 從 INF 讀出來的，別手改，改完下次產生就被覆蓋。

### 音訊

| 包 | 版本 | 說明 |
|---|---|---|
| `csaudiosstavs` | 1.0.4.0 | AVS 架構，Skylake 到 Apollo Lake |
| `csaudiointcsof` | 1.0.4.0 | SOF 架構，Gemini Lake 及更新 |
| `csaudioamdsof` | 1.0.1.0 | AMD 平台的 SOF |
| `ssm4567` | 1.0.1 | ADI 的 I2S 放大器，glados 系列板子用 |

### Codec

`nau8825` · `nau88l25rvp` · `cs42l42` · `da7219` · `rt5682` · `rt5682s` · `croseccodec`

### 放大器

`max98357a` · `max98390` · `opengmaxcodec` · `rt1011` · `rt1015` · `rt1019`

### 匯流排與系統

`acpbus` · `cbtable` · `crosecbus` · `croseci2c` · `crosectypec` · `sklhdaudbus` · `intelpmc` · `inteltcss` · `iaLPSS2_SPI` · `iaLPSS2_UART2` · `dptf_acpi` · `gnodriver`

### 輸入與攝影機

`crostouchpad-{synaptics,cypress,cypress-smbus,atmel,elan}` · `crosfingerprint` · `ov5675` · `iaisp64_46xx_ADL`

有兩個包在倉庫裡但預設不裝。`nau88l25rvp` 和 `nau8825` 搶同一個 `ACPI\10508825`，只能活一個，預設給 CoolStar 那版；`gnodriver` 只有 INF 沒有 `sys`，裝上去也沒東西可綁。

裝和卸都讀 [installer/payload.list](installer/payload.list) 這一份清單，順序也在裡面。

## 原版為什麼不出聲

原始 `.sys` 裡可以直接讀到這幾行字串，用的是明文 ASCII，沒加密也沒混淆。

```
AVS: License not found. Please install it to C:\Windows\system32\drivers\avs_signedlicense.bin
AVS: License too small. Make sure the license is signed.
SystemSKU      Chell
```

`DriverEntry` 會讀授權檔、驗簽章、再拿 SMBIOS 裡的板卡代號比對。任何一步不過就回傳 NTSTATUS `0xC000026A`，呼叫方看到這個碼直接放棄啟動 ADSP。

### 補丁動了哪幾個位元組

三個堆疊分別改了 5 處、12 處、12 處，加起來 153 個位元組。改法只有一種。

```asm
mov eax, 0xC000026A    ; 原本檢查失敗回傳這個
mov eax, 0              ; 現在照樣回傳成功
```

`return cond ? STATUS_X : STATUS_SUCCESS;` 編出來是 `and reg, 0xC000026A`，補丁把它變成 `and reg, 0`，剩下的位元組用 `nop` 填平，長度不變，後面的指令位移也不會錯位。

INF、HWID 表、服務安裝段、DSP 韌體一個位元組都沒動。`CsAudioCallbackAPI` 回呼照舊註冊，`ssm4567` 放大器驅動全靠這條回呼才拿到 Speaker 端點。改的只是授權判斷，音訊通路本身是原廠的。

### 簽章與信任鏈

改一個位元組，WHQL 簽章就廢了，所以補丁版是重新簽的。兩個檔案用的憑證還不一樣。

| 檔案 | 原始 | 現在 | 時戳 |
|---|---|---|---|
| `.sys` | 微軟 WHQL | Airspace Intelligent Technology，常州 | DigiCert，公共 TSA |
| `.cat` | 微軟 WHQL | 福建六壬網安 | Pikachu Time Sub CA，自建 |

`.sys` 的鏈能追到公共根，Secure Boot 關著就能載入。`.cat` 的時戳來自自建 TSA，公開根不認，`pnputil` 會卡在這裡：

```
0xE0000242  尚未建立對 Authenticode(TM)簽署的類別目錄的發行者信任
```

`certs/` 裡那個安裝程式就是幹這個的。它在裝驅動之前跑一遍，把根同時寫進 `LocalMachine\Root` 和 `LocalMachine\TrustedPublisher`，少寫一個都還是 0xE0000242。

有個坑可以先講，省你半天。`Get-AuthenticodeSignature` 回傳 `Valid`，不代表 `pnputil` 會收這個包。前者只驗 PKCS#7 結構和憑證鏈，後者還要過 setupapi 的發行者信任檢查。另外 INF 的位元組一個都不能改，換行字元也算，因為類別目錄裡存著它的雜湊。`.gitattributes` 把 `*.inf`、`*.sys`、`*.cat` 都標成 `binary`，就是為了這個。

逐位元組 diff、反組譯，以及和 [akibaPatcher](https://github.com/akibaGumi/akibaPatcher) 那條路線的對照，都在 [docs/PATCH-ANALYSIS.md](docs/PATCH-ANALYSIS.md)。

## 目錄結構

```
.
├── .github/workflows/release.yml   CI，填版本進去，出來的就是 Release
├── certs/                          簽章根，一個 Inno Setup 安裝程式
├── docs/
│   ├── DRIVERS.md                  驅動表，37 包 / 198 HWID，腳本產生
│   ├── PATCH-ANALYSIS.md           補丁原理與逐位元組差異
│   └── VENDOR.md                   哪些東西進不了倉庫，以及為什麼
├── drivers/                        驅動本體
│   ├── audio/  codec/  amp/        出聲相關的核心
│   ├── bus/    input/  camera/  system/
├── installer/
│   ├── Install.iss                 Inno Setup 腳本
│   ├── payload.list                安裝順序
│   └── License.rtf  dpinsts.ico
├── reference/originals/            CoolStar 官方原包，只用來 diff，不安裝
├── scripts/
│   ├── preflight.ps1               打包前校驗 payload
│   ├── trust-certs.bat             手動裝驅動時信任簽章根
│   ├── install-drivers.bat         按 payload.list 叫 DPInst
│   └── uninstall-drivers.bat       同一份清單，走 /U
├── src/ssm4567/                    ADI SSM4567 放大器驅動原始碼，KMDF 加 I2C
├── tools/
│   ├── dpinst/dpinst.exe           Microsoft DPInst 2.1
│   ├── gen-driver-docs.py          產生 docs/DRIVERS.md
│   └── compare-drivers.py          原包和補丁包逐位元組對比
└── vendor/                         本機暫存，git 忽略，見 docs/VENDOR.md
```

## 開發

打包只要 Inno Setup 6，`choco install innosetup` 完事。改 `src/ssm4567` 才需要 VS 2022 加 WDK，注意 WDK 對 VS 版本有要求，對不上會編不過。輔助腳本用 PowerShell 5.1 或 7，Python 3.9 以上。`compare-drivers.py` 的反組譯部分要 `pip install capstone`，不裝也能跑，只是看不到指令。

```bat
:: 校驗 payload，缺目錄、缺 inf、類別目錄引用的檔案不存在都會報出來
powershell -ExecutionPolicy Bypass -File scripts\preflight.ps1

:: 打包
ISCC.exe /DMyAppVersion=1.0.5 installer\Install.iss

:: 動過驅動包之後更新清單
python tools\gen-driver-docs.py

:: 看看某個包跟官方原版差在哪
python tools\compare-drivers.py reference\originals\csaudiosstavs drivers\audio\csaudiosstavs
```

CI 在 [.github/workflows/release.yml](.github/workflows/release.yml)。手動觸發填個版本號，或者推一個 `v*` 標籤。流程是跑 preflight、裝 Inno Setup、`ISCC /DMyAppVersion=<v>`、算 `SHA256SUMS.txt`、傳 artifact、`gh release create`，同名標籤已存在就改成上傳覆蓋。preflight 掛了直接中斷，壞包進不了 Release。

要加驅動，包丟進 `drivers/<類別>/<名字>/`，需要預設裝就往 `payload.list` 加一行，跑一遍產生腳本和 preflight，提交。驅動版本從 INF 讀，安裝包版本從 CI 輸入讀，兩邊不用手動同步。

卸載走系統裡的應用程式清單，或者 `scripts\uninstall-drivers.bat`。它按同一份清單逐個 `DPInst /U`，最後把 CA 一起卸掉。

## 跑過的機器

| 機型 | 板卡 | 平台 | 結果 |
|---|---|---|---|
| HP Chromebook 13 G1 | `chell` | Skylake | 喇叭和耳機都正常，要 `ssm4567` 加 `csaudiosstavs` |

只有這一台。其他機型按平台歸屬應該能用，但我沒有條件逐台試，所以不敢替你保證。試過的話把機型、`SystemSKU`、以及 `ACPI\INT343B` 和 `ACPI\10508825` 的硬體 ID 發我，我加進表裡。

## 已知缺口

四個包的上游版本比這裡新，三處東西故意沒收，原因和替代方案寫在 [docs/VENDOR.md](docs/VENDOR.md)。

影響最大的是 Intel Serial IO 的 I2C 和 GPIO 驅動。chell 的 codec 和放大器就掛在那條 I2C 匯流排上，全新裝機缺了它，codec 連暫存器都配不上。手上只有一份 DriverStore 傾印，裡面沒有類別目錄，拼不出能裝進 x64 的包，所以這件事卡在這兒。

## 聲明

CoolStar 音訊驅動 © CoolStar，從 <https://coolstar.org/chromebook/> 發佈，上游按機器授權收費。這個倉庫是非官方重新打包，面向已經持有授權的硬體，不含授權檔、產生器或啟用工具。`DPInst.exe` © Microsoft，隨 WDK 發佈。`src/ssm4567` © CoolStar，來自 <https://github.com/coolstar/ssm4567>，授權見 `src/ssm4567/LICENSE.txt`。ChromeOS EC、觸控板、攝影機、DPTF 那些驅動版權歸各自廠商。

跟 Google、CoolStar 和任何硬體廠商都沒有隸屬關係。Chromebook 刷 Windows 有變磚風險，動韌體尤其危險。只裝驅動是可逆的，卸載腳本能把驅動和信任的根一起清掉。
