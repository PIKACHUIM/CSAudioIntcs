<div align="center">

# ChromeBook / ChromeBox 専用ドライバーパック

Chromebook/box 向けのドライバーパック。数十種類のドライバーをまとめて収録

![platform](docs/badges/platform.svg) ![drivers](docs/badges/drivers.svg) ![installer](docs/badges/installer.svg) ![license](docs/badges/license.svg)

[ダウンロード](../../releases) · [ドライバー一覧](docs/DRIVERS.md) · [パッチ記録](docs/PATCH-ANALYSIS.md) · [バージョン差](docs/UPSTREAM.md) · [ライセンス](LICENSE)

[简体中文](README.md) · [English](README.en.md) · [繁體中文](README.zh-TW.md) · **日本語**

</div>

---

## 概要

ChromeBook / ChromeBox 専用のドライバーパックです。36 個のドライバーで 171 個のハードウェア ID に対応します。

デバイスごとに互換性を調整し、オーディオなど専用ハードウェアの設定用ドライバーも含んでいます。

| 項目 | 内容 |
|---|---|
| ドライバー | 36 個、対応ハードウェア 171 個 |
| デバイス | オーディオコントローラー、codec、アンプ、タッチパッド、タッチスクリーン、指紋認証、カメラ、EC バス、DPTF |
| プラットフォーム | Intel Skylake から Alder Lake、AMD Rembrandt / Phoenix |
| ファイル | `INF` + `SYS` + `CAT` のみ。アーカイブや自己解凍形式は使っていません |
| ツール | Microsoft DPInst 2.1、Inno Setup 6 |

## 動作環境
1. Windows 10 X64 1607 以降
2. Windows 11 2026 年 7 月以降の更新プログラム適用済みの場合は、「コード整合性のクロス署名証明書」ポリシーを無効にする必要があります:
    ```pwsh
    CiTool.exe --remove-policy "{8F9CB695-5D48-48D6-A329-7202B44607E3}"
    ```


以下は**不要**です:
  1. SecureBoot を**無効にする必要はありません**
  2. テストモードを**有効にする必要はありません**
  3. 「Microsoft の脆弱なドライバーのブロックリスト」を**無効にする必要はありません**

## 注意事項
1. Chromebook に Windows を入れると文鎮化する可能性があります
2. 本ドライバーは十分な検証を行っていません。自己責任でご使用ください


## インストール方法

1. [Releases](../../releases) から `ChromeBook-CoolStar-Audio-Driver-<バージョン>-x64.exe` をダウンロード
2. 右クリックして管理者として実行し、完了後に再起動

## インストールの流れ

1. 最初に署名ルートを登録します。Windows 標準の CA ストアの補完も含みます
2. ドライバーの署名に使うテスト証明書と信頼されたクロス CA ルートを登録します
3. 順番に DPInst を呼び出します。同じバージョンが既に入っているパッケージは飛ばします

x64 のみです。a64 と x32 の Windows には対応するパッケージがありません。

## リポジトリ構成

```
.
├── drivers/             ドライバー本体。audio codec amp bus input camera system に分類
├── installer/           Install.iss（パッケージ定義）、payload.list（インストール順）
├── scripts/             preflight、インストール、アンインストール、ルート証明書の登録
├── certs/               署名ルートのインストーラー
├── reference/originals/ CoolStar 純正パッケージ。差分比較用
├── src/ssm4567/         SSM4567 アンプドライバーのソース
├── tools/               DPInst、ドキュメント生成、バイナリ比較
└── docs/                ドライバー一覧、パッチ記録、署名状態、バージョン差
```

## ドライバー一覧

バージョンと全 HWID は [docs/DRIVERS.md](docs/DRIVERS.md) にあります。このファイルは `python tools/gen-driver-docs.py` が INF から生成するので、手で編集しないでください。

### オーディオ

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

### アンプ

- `max98357a`
- `max98390`
- `opengmaxcodec`
- `rt1011`
- `rt1015`
- `rt1019`

### バスとシステム

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

### 入力とカメラ

- `crostouchpad-synaptics`
- `crostouchpad-cypress`
- `crostouchpad-cypress-smbus`
- `crostouchpad-atmel`
- `crostouchpad-elan`
- `crosfingerprint`
- `ov5675`
- `iaisp64_46xx_ADL`

`nau88l25rvp` と `nau8825` は同じハードウェア ID `ACPI\10508825` を取り合います。既定では CoolStar 版を使うため、`nau88l25rvp` は同梱されますがインストール対象から外しています。インストールも削除も [installer/payload.list](installer/payload.list) が基準です。




## ビルド

[Inno Setup 6](https://jrsoftware.org/isinfo.php) でインストーラーをビルドします。

```bat
:: 任意。payload に欠けているファイルがないか確認
powershell -ExecutionPolicy Bypass -File scripts\preflight.ps1

ISCC.exe installer\Install.iss
ISCC.exe /DMyAppVersion=26.11.26 installer\Install.iss
```


## ライセンス

本プロジェクトは [MIT ライセンス](LICENSE) で配布します。

ドライバーバイナリの著作権は各ベンダーにあり、MIT の対象外です。

| コンポーネント | ライセンス |
|---|---|
| CoolStar オーディオドライバー | 商業ライセンス、1 台ごと |
| `src/ssm4567` | `src/ssm4567/LICENSE.txt` |
| `tools/dpinst/dpinst.exe` | Microsoft。WDK に同梱 |
| ChromeOS EC、タッチパッド、カメラ、DPTF など | 各ベンダー |

MIT が適用されるのはこのリポジトリで追加した部分だけです。上記コンポーネントのライセンスは変わりません。

Google、CoolStar、各ハードウェアベンダーとの提携関係はありません。
