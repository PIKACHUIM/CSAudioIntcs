<div align="center">

# ChromeBook CoolStar ドライバーパック

Chromebook に Windows を入れると、サウンドカードもタッチパッドもカメラも動かないことが多い。
このリポジトリは CoolStar と各 ChromeOS ベンダーのドライバーを集め、1 つのインストーラーにまとめたものです。

[简体中文](README.md) · [English](README.en.md) · [繁體中文](README.zh-TW.md) · **日本語**

![platform](docs/badges/platform.svg) ![drivers](docs/badges/drivers.svg) ![installer](docs/badges/installer.svg) ![license](docs/badges/license.svg)

[ダウンロード](../../releases) · [ドライバー一覧](docs/DRIVERS.md) · [パッチ記録](docs/PATCH-ANALYSIS.md) · [バージョン差](docs/UPSTREAM.md) · [ライセンス](LICENSE)

</div>

---

## これは何か

ドライバーパッケージ 36 個、ハードウェア ID 171 個。このうち 35 個が既定でインストールされます。

| 項目 | 内容 |
|---|---|
| パッケージ | 36 個（インストール対象は 35 個） |
| ハードウェア ID | 171 個 |
| 対応デバイス | オーディオコントローラー、codec、アンプ、タッチパッド、タッチスクリーン、指紋認証、カメラ、EC バス、DPTF |
| プラットフォーム | Intel Skylake から Alder Lake、AMD Rembrandt / Phoenix |
| ファイル形式 | `INF` + `SYS` + `CAT` のみ。アーカイブや自己解凍形式は使っていません |
| ツール | Microsoft DPInst 2.1、Inno Setup 6 |

ドライバーは CoolStar と ChromeOS 各社の公開パッケージから取得しています。バージョンとハードウェア ID はそのまま、変えたのはディレクトリ構成とインストール順だけです。

インストール中に外部プログラムは 2 つだけ実行されます。`tools/dpinst/dpinst.exe` と `certs/pikachu-certificate-install.exe` です。前者がドライバーを入れ、後者が catalog の署名ルートを証明書ストアに追加します。

CoolStar のオーディオドライバーは 1 台ごとのライセンス販売です。このリポジトリはライセンスを購入済みのハードウェア向けで、ライセンスファイル、生成ツール、アクティベーションツールは含みません。

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

## インストール

### リリース版を使う

1. [Releases](../../releases) から `ChromeBook-CoolStar-Audio-Driver-<バージョン>-x64.exe` をダウンロード
2. 右クリックして管理者として実行
3. 終わったら再起動

インストーラーは最初に署名ルートを登録し、`installer/payload.list` の順に DPInst を呼びます。同じバージョンが既に入っているパッケージは飛ばします。

x64 用です。32 ビット版のパッケージはありません。

### 1 つだけ入れたい場合

タッチパッドだけ動かない、といったケース。ルートを登録してから、INF を直接指定します。

```bat
:: 管理者 PowerShell
certs\pikachu-certificate-install.exe /VERYSILENT

pnputil /add-driver drivers\input\crostouchpad-synaptics\crostouchpad.inf /install
```

削除するときは `scripts\uninstall-drivers.bat` を実行します。登録したルート証明書も一緒に消えます。

### オーディオスタックの選び方

3 つのアーキテクチャは互換性がなく、ハードウェア ID も重なっていません。DPInst は一致したものだけを紐付けるので、全部入れる場合は選ぶ必要はありません。

| プラットフォーム | パッケージ | サービス名 |
|---|---|---|
| Skylake、Kaby Lake、Amber Lake、Braswell、Apollo Lake | `csaudiosstavs` | `CsAudioSSTAVS` |
| Gemini Lake、Jasper Lake、Ice Lake、Tiger Lake、Alder Lake 以降 | `csaudiointcsof` | `CsAudioIntcSOF` |
| AMD Rembrandt、Phoenix など | `csaudioamdsof` | `CsAudioAmdSOF` |
| glados、chell などのスピーカーアンプ | `ssm4567` | `ssm4567` |

`ssm4567` は独立したアンプドライバーで、上の 3 つと並べて入ります。

## ドライバー一覧

バージョンと全 HWID は [docs/DRIVERS.md](docs/DRIVERS.md) にあります。このファイルは `python tools/gen-driver-docs.py` が INF から生成するので、手で編集しないでください。

**オーディオ**: `csaudiosstavs` 1.0.4.0 · `csaudiointcsof` 1.0.4.0 · `csaudioamdsof` 1.0.1.0 · `ssm4567` 1.0.1

**Codec**: `nau8825` · `nau88l25rvp` · `cs42l42` · `da7219` · `rt5682` · `rt5682s` · `croseccodec`

**アンプ**: `max98357a` · `max98390` · `opengmaxcodec` · `rt1011` · `rt1015` · `rt1019`

**バスとシステム**: `acpbus` · `cbtable` · `crosecbus` · `croseci2c` · `crosectypec` · `sklhdaudbus` · `intelpmc` · `inteltcss` · `iaLPSS2_SPI` · `iaLPSS2_UART2` · `dptf_acpi`

**入力とカメラ**: `crostouchpad-{synaptics,cypress,cypress-smbus,atmel,elan}` · `crosfingerprint` · `ov5675` · `iaisp64_46xx_ADL`

`nau88l25rvp` と `nau8825` は同じハードウェア ID `ACPI\10508825` を取り合います。既定では CoolStar 版を使うため、`nau88l25rvp` は同梱されますがインストール対象から外しています。インストールも削除も [installer/payload.list](installer/payload.list) が基準です。

## 純正ドライバーが無音になる理由

CoolStar のオーディオドライバーは `DriverEntry` でライセンスファイルを読み、署名を検証し、SMBIOS のボード名と照合します。ここで失敗すると `0xC000026A` を返し、呼び出し側は ADSP を起動しません。ADSP が動かないと I2S クロックが出ず、アンプは切れたままです。

その結果、デバイスマネージャーは正常、エンドポイントも揃っている、テスト音のレベルメーターも動く、それでもスピーカーから音が出ない、という状態になります。

バイナリには次の文字列がそのまま入っています。

```
AVS: License not found. Please install it to C:\Windows\system32\drivers\avs_signedlicense.bin
AVS: License too small. Make sure the license is signed.
SystemSKU
Chell
```

3 つのスタックで合計 29 箇所、153 バイトを書き換えています。やっていることは同じで、失敗コードの即値を変えて戻り値を常に成功にしています。エラー分岐の `mov eax, 0xC000026A` か、`return cond ? STATUS_X : STATUS_SUCCESS;` に対して MSVC が出力する `sbb / not / and` のどちらかです。

INF、HWID テーブル、サービス定義、DSP ファームウェアは 1 バイトも変えていません。`CsAudioCallbackAPI` の登録も残しています。`ssm4567` がこれを経由してスピーカーエンドポイントを登録するためです。

各箇所のオフセットと逆アセンブルは [docs/PATCH-SITES.md](docs/PATCH-SITES.md)、変更の考え方は [docs/PATCH-ANALYSIS.md](docs/PATCH-ANALYSIS.md) にあります。

## 署名

各パッケージには catalog が付いていますが、署名元は 3 種類あり、追加作業が必要なのは 1 種類だけです。

| 署名元 | パッケージ数 | 追加作業 |
|---|---|---|
| Microsoft WHQL | 24 | 不要 |
| 第三者の署名、公開ルートにつながる | 5 | 不要（タッチパッドの 5 個） |
| 第三者の署名、非公開ルートにつながる | 7 | ルートを先に信頼ストアへ登録 |

最後の 7 個は壊れているわけではありません。catalog のタイムスタンプ証明書チェーンが、公開ルートから信頼されない証明書で終わっているだけです。登録しないと `pnputil` はここで止まります。

```
0xE0000242  署名されたカタログの発行元の信頼が確立されていません
```

`certs/pikachu-certificate-install.exe` は `LocalMachine\Root` と `LocalMachine\TrustedPublisher` の両方に書き込みます。どちらか一方だけでは通りません。インストーラーはドライバーを入れる前にこれを実行します。

注意点として、`Get-AuthenticodeSignature` が `Valid` を返しても `pnputil` が受け入れるとは限りません。前者は PKCS#7 と証明書チェーンだけを見て、後者は setupapi の発行元信頼も確認します。パッケージごとの状態は [docs/SIGNATURES.md](docs/SIGNATURES.md) にあります。

自分で `.sys` を書き換えた場合は、catalog を作り直してバイナリと catalog の両方に署名する必要があります。手順は [FakeSign](https://github.com/PIKACHUIM/FakeSign) にまとめてあります。このリポジトリに証明書も秘密鍵もありません。

### インストーラー自体の署名

Actions が生成する `exe` には Authenticode 署名がありません。そのまま実行すると SmartScreen の警告が出ます。Releases にあるものはビルド後にローカルで署名し直して差し替えたものなので、ダウンロードしたファイルは検証が通ります。

正式に署名する場合は、手持ちの証明書でローカルに署名します。署名するとハッシュが変わるので、`SHA256SUMS.txt` も作り直します。

```powershell
signtool sign /fd SHA256 /f mycert.pfx /p <パスワード> `
  /tr http://timestamp.digicert.com /td SHA256 `
  dist\ChromeBook-CoolStar-Audio-Driver-<バージョン>-x64.exe
```

```powershell
$out = "dist\SHA256SUMS.txt"
Remove-Item $out -ErrorAction SilentlyContinue
Get-ChildItem dist -Filter *.exe | ForEach-Object {
  "$((Get-FileHash $_.FullName -Algorithm SHA256).Hash.ToLower())  $($_.Name)"
} | Out-File $out -Encoding ascii
```

あとは `gh release upload v<バージョン> dist\*.exe dist\SHA256SUMS.txt --clobber` でリリースを上書きします。

## ビルド

必要なのは [Inno Setup 6](https://jrsoftware.org/isinfo.php) だけです。WDK も Visual Studio も要りません。

```bat
:: 任意。payload に欠けているファイルがないか確認
powershell -ExecutionPolicy Bypass -File scripts\preflight.ps1

ISCC.exe installer\Install.iss
ISCC.exe /DMyAppVersion=26.11.26 installer\Install.iss
```

出力は `dist\` に入ります。バージョンを指定しない場合は `Install.iss` に書かれた値が使われます。

CI は `.github/workflows/release.yml` です。手動実行でバージョンを入力するか、`v*` タグを push します。流れは preflight、ビルド、`SHA256SUMS.txt` の生成、Release の作成です。

## ライセンス

このリポジトリのスクリプト、ドキュメント、ビルド設定は MIT です。全文は [LICENSE](LICENSE) にあります。

ドライバーバイナリの著作権は各ベンダーにあり、MIT の対象外です。

| コンポーネント | ライセンス |
|---|---|
| CoolStar オーディオドライバー | 商業ライセンス、1 台ごと。 <https://coolstar.org/chromebook/> |
| `src/ssm4567` | CoolStar。`src/ssm4567/LICENSE.txt` を参照 |
| `tools/dpinst/dpinst.exe` | Microsoft。WDK に同梱 |
| ChromeOS EC、タッチパッド、カメラ、DPTF など | 各ベンダー |

MIT が適用されるのはこのリポジトリで追加した部分だけです。上記コンポーネントのライセンスは変わりません。Google、CoolStar、各ハードウェアベンダーとの提携関係はありません。

Chromebook に Windows を入れると文鎮化する可能性があり、ファームウェアの変更はさらに危険です。ドライバーのインストールだけなら元に戻せます。アンインストーラーはドライバーと登録したルート証明書の両方を削除します。
