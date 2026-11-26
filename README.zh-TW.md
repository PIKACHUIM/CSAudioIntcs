<div align="center">

# ChromeBook/Box Windows 專用驅動包

Chromebook/box 專用驅動包合集，包含數十種不常見的 Chrome 裝置驅動

![platform](docs/badges/platform.svg) ![drivers](docs/badges/drivers.svg) ![installer](docs/badges/installer.svg) ![license](docs/badges/license.svg)

[下載驅動](../../releases) · [驅動清單](docs/DRIVERS.md) · [修補記錄](docs/PATCH-ANALYSIS.md) · [版本差異](docs/UPSTREAM.md) · [授權](LICENSE)

[简体中文](README.md) · [English](README.en.md) · **繁體中文** · [日本語](README.ja.md)

</div>

---

## 專案簡介

ChromeBook / ChromeBox 專用驅動包，包含 36 個驅動，支援 171 個硬體

本專案針對不同裝置做了相容性調整，並支援音訊等專屬硬體設定驅動

| 項目 | 內容 |
|---|---|
| 驅動 | 36 個，支援硬體 171 個 |
| 裝置 | 音訊控制器、codec、放大器、觸控板、觸控螢幕、指紋、攝影機、EC 匯流排、DPTF |
| 平台 | Intel Skylake 到 Alder Lake，AMD Rembrandt / Phoenix |
| 檔案 | 純 `INF` + `SYS` + `CAT`，沒有壓縮檔和自解壓安裝程式 |
| 工具 | Microsoft DPInst 2.1、Inno Setup 6 |

## 環境需求
1. Windows 10 X64 1607 及以上版本
2. Windows 11 >= 2026 年 7 月更新，需關閉「程式碼完整性異常原則的交叉憑證」：
    ```pwsh
    CiTool.exe --remove-policy "{8F9CB695-5D48-48D6-A329-7202B44607E3}"
    ```


以下條件是**不必要**的：
  1. **無需關閉** SecureBoot
  2. **無需開啟** 測試模式
  3. **無需關閉**「Microsoft 易受攻擊的驅動程式封鎖清單」

## 風險提示
1. Chromebook 安裝 Windows 有變磚風險
2. 本驅動未經充分測試，使用風險自負


## 安裝方式

1. 在 [Releases](../../releases) 下載 `ChromeBook-Windows-Driver-<版本>-x64.exe`
2. 按右鍵以系統管理員身分執行，安裝完成後重新開機

## 安裝過程

1. 安裝程式會先匯入簽章根，包含補全 Windows 系統內建 CA
2. 匯入測試憑證和受信任交叉 CA 根憑證，用於簽署驅動
3. 依序逐一呼叫 DPInst，同版本的驅動已經裝過就跳過

只支援 x64，a64 和 x32 Windows 沒有對應的驅動包。

## 專案結構

```
.
├── drivers/             驅動本體，按 audio codec amp bus input camera system 分類
├── installer/           Install.iss（打包腳本）、payload.list（安裝順序）
├── scripts/             preflight、安裝、移除、匯入根憑證
├── certs/               簽章根安裝程式
├── reference/originals/ CoolStar 官方原包，用來做差異比對
├── src/ssm4567/         SSM4567 放大器驅動原始碼
├── tools/               DPInst、文件產生、二進位比對
└── docs/                驅動清單、修補記錄、簽章狀態、版本差異
```

## 驅動清單

完整版本號和每一條 HWID 在 [docs/DRIVERS.md](docs/DRIVERS.md)，那份檔案由 `python tools/gen-driver-docs.py` 從 INF 產生，不要手改。

### 音訊

- `csaudiosstavs` 1.0.4.0
- `csaudiointcsof` 1.0.4.0
- `csaudioamdsof` 1.0.1.0
- `ssm4567` 1.0.1

### Codec

- `nau8825`
- `nau88l25rvp`
- `cs42l42`
- `da7219`
- `rt5682`
- `rt5682s`
- `croseccodec`

### 放大器

- `max98357a`
- `max98390`
- `opengmaxcodec`
- `rt1011`
- `rt1015`
- `rt1019`

### 匯流排與系統

- `acpbus`
- `cbtable`
- `crosecbus`
- `croseci2c`
- `crosectypec`
- `sklhdaudbus`
- `intelpmc`
- `inteltcss`
- `iaLPSS2_SPI`
- `iaLPSS2_UART2`
- `dptf_acpi`

### 輸入與攝影機

- `crostouchpad-synaptics`
- `crostouchpad-cypress`
- `crostouchpad-cypress-smbus`
- `crostouchpad-atmel`
- `crostouchpad-elan`
- `crosfingerprint`
- `ov5675`
- `iaisp64_46xx_ADL`

`nau88l25rvp` 和 `nau8825` 搶同一個硬體 ID `ACPI\10508825`，預設用 CoolStar 那份，所以它隨倉庫發佈但不進安裝清單。安裝和移除都以 [installer/payload.list](installer/payload.list) 為準。




## 建置教學

使用 [Inno Setup 6](https://jrsoftware.org/isinfo.php) 建置安裝包。

```bat
:: 可選，檢查 payload 有沒有缺檔案
powershell -ExecutionPolicy Bypass -File scripts\preflight.ps1

ISCC.exe installer\Install.iss
ISCC.exe /DMyAppVersion=26.11.26 installer\Install.iss
```


## 開源授權

本專案基於 [MIT 授權](LICENSE) 發佈。

驅動二進位的著作權歸各原廠，不在 MIT 範圍內：

| 元件 | 授權 |
|---|---|
| CoolStar 音效驅動 | 商業授權，按機器計費 |
| `src/ssm4567` | `src/ssm4567/LICENSE.txt` |
| `tools/dpinst/dpinst.exe` | Microsoft，隨 WDK 散佈 |
| ChromeOS EC、觸控板、攝影機、DPTF 等 | 各廠商 |

MIT 只涵蓋本倉庫新增的部分，不改變上面這些元件的授權狀態。

本倉庫和 Google、CoolStar 以及任何硬體廠商都沒有隸屬關係。
