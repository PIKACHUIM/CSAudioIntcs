<div align="center">

# ChromeBook CoolStar ドライバーパック

ChromeBook 専用ドライバーを数十種類収録。サウンドカード、タッチパッドなどを含みます

[简体中文](README.md) · [English](README.en.md) · [繁體中文](README.zh-TW.md) · **日本語**

![platform](docs/badges/platform.svg) ![drivers](docs/badges/drivers.svg) ![installer](docs/badges/installer.svg) ![license](docs/badges/license.svg)

</div>

[プロジェクト概要](#プロジェクト概要) · [クイックインストール](#クイックインストール) · [ドライバー一覧](#ドライバー一覧) · [パッチの仕組み](#パッチの仕組み) · [ライセンス](#ライセンス)

---

## プロジェクト概要

本リポジトリは ChromeBook に Windows を導入する際に使用するドライバーの集合です。機能ごとに再分類し、37 パッケージ、198 個のハードウェア ID を収録しています。

| 項目 | 内容 |
|---|---|
| パッケージ数 | 37（うち 35 を既定でインストール） |
| ハードウェア ID | 198 |
| 収録範囲 | オーディオ（AVS / SOF）、codec、アンプ、タッチパッド、タッチスクリーン、指紋認証、カメラ、EC バス、チップセット関連 |
| 対象プラットフォーム | Intel Skylake から Alder Lake、AMD Rembrandt / Phoenix |
| ファイル形式 | 生の `INF` + `SYS` + `CAT`。アーカイブおよび自己解凍インストーラーなし |
| 使用ツール | Microsoft DPInst 2.1、Inno Setup 6 |

ドライバーは CoolStar および各 ChromeOS ハードウェアベンダーの公開パッケージから取得しています。バージョンとハードウェア ID は原文のままで、ディレクトリ構成とインストール順のみ変更しています。インストール中に実行される外部バイナリは `tools/dpinst/dpinst.exe` と `certs/pikachu-certificate-install.exe` の 2 つだけです。

上流のオーディオドライバーはマシン単位の有償ライセンスです。本リポジトリはライセンスを保有しているハードウェア向けであり、ライセンスファイル、生成ツール、アクティベーションツールは含みません。

```
.
├── drivers/            audio codec amp bus input camera system
├── installer/          Install.iss、payload.list（インストール順）
├── scripts/            preflight、install、uninstall、trust-certs
├── certs/              署名ルートのインストーラー
├── reference/originals/  CoolStar 公式パッケージ、差分比較専用
├── src/ssm4567/        ADI SSM4567 アンプドライバーのソース
├── tools/              DPInst、ドキュメント生成、バイナリ比較
├── docs/               DRIVERS.md、PATCH-ANALYSIS.md、VENDOR.md
└── vendor/             ローカル作業用、git 管理外
```

## クイックインストール

### リリース版を使用する

1. [Releases](../../releases) から `ChromeBook-CoolStar-Audio-Driver-<バージョン>-x64.exe` をダウンロード
2. 管理者として実行
3. インストール後に再起動

インストーラーは最初に署名ルートをインポートし、`payload.list` の順に DPInst を呼び出します。同一バージョンが既に存在する場合はスキップされます。

### ソースからビルドする

必要なのは [Inno Setup 6](https://jrsoftware.org/isinfo.php) のみです。WDK と Visual Studio は不要です。

```bat
:: 任意: payload の検証
powershell -ExecutionPolicy Bypass -File scripts\preflight.ps1

:: ビルド、出力は dist\ に生成
ISCC.exe installer\Install.iss
ISCC.exe /DMyAppVersion=1.0.5 installer\Install.iss
```

CI は `.github/workflows/release.yml` にあります。手動実行時にバージョンを指定するか、`v*` タグを push します。preflight、ビルド、`SHA256SUMS.txt` の生成、Release の作成という流れです。

### 単一のドライバーのみインストールする

一部のデバイスのみ不足している場合に使用します。先に署名ルートをインポートし、次に INF を指定します。

```bat
:: 管理者 PowerShell
certs\pikachu-certificate-install.exe /VERYSILENT

pnputil /add-driver drivers\audio\ssm4567\ssm4567.inf /install
```

### オーディオスタックの選択

3 つのアーキテクチャは相互に互換性がなく、ハードウェア ID も重なりません。

| プラットフォーム | パッケージ | サービス名 |
|---|---|---|
| Skylake、Kaby Lake、Amber Lake、Braswell、Apollo Lake | `csaudiosstavs` | `CsAudioSSTAVS` |
| Gemini Lake、Jasper Lake、Ice Lake、Tiger Lake、Alder Lake 以降 | `csaudiointcsof` | `CsAudioIntcSOF` |
| AMD: Rembrandt、Phoenix など | `csaudioamdsof` | `CsAudioAmdSOF` |
| glados、chell 系ボードのスピーカーアンプ | `ssm4567` | `ssm4567` |

完全版インストーラーには 3 種すべてが含まれ、DPInst はハードウェア ID が一致したものだけをバインドします。`ssm4567` は独立したアンプドライバーで、いずれのスタックとも併用できます。

## ドライバー一覧

バージョンと HWID の完全な一覧は [docs/DRIVERS.md](docs/DRIVERS.md) にあります。このファイルは `python tools/gen-driver-docs.py` が INF から生成します。

### オーディオ

| パッケージ | バージョン | 説明 |
|---|---|---|
| `csaudiosstavs` | 1.0.4.0 | AVS アーキテクチャ、Skylake から Apollo Lake |
| `csaudiointcsof` | 1.0.4.0 | SOF アーキテクチャ、Gemini Lake 以降 |
| `csaudioamdsof` | 1.0.1.0 | AMD プラットフォーム向け SOF |
| `ssm4567` | 1.0.1 | ADI I2S アンプ、glados 系ボード |

### Codec

`nau8825` · `nau88l25rvp` · `cs42l42` · `da7219` · `rt5682` · `rt5682s` · `croseccodec`

### アンプ

`max98357a` · `max98390` · `opengmaxcodec` · `rt1011` · `rt1015` · `rt1019`

### バスとシステム

`acpbus` · `cbtable` · `crosecbus` · `croseci2c` · `crosectypec` · `sklhdaudbus` · `intelpmc` · `inteltcss` · `iaLPSS2_SPI` · `iaLPSS2_UART2` · `dptf_acpi` · `gnodriver`

### 入力とカメラ

`crostouchpad-{synaptics,cypress,cypress-smbus,atmel,elan}` · `crosfingerprint` · `ov5675` · `iaisp64_46xx_ADL`

同梱されますが既定ではインストールされないパッケージが 2 つあります。`nau88l25rvp` と `nau8825` は同じ `ACPI\10508825` を使用するため、既定では CoolStar 版を有効にします。`gnodriver` は `SYS` を含まず INF のみです。

インストールとアンインストールは [installer/payload.list](installer/payload.list) を唯一のリストとして参照します。

## パッチの仕組み

CoolStar のオーディオドライバーは `DriverEntry` でライセンスファイルを読み込み、署名を検証し、SMBIOS のボード名と照合します。失敗すると NTSTATUS `0xC000026A` を返し、呼び出し側は ADSP を起動せずに処理を終了します。ADSP が起動しないと I2S クロックが出力されず、アンプはシャットダウン状態のままです。

元のバイナリには次の文字列が平文で含まれています。

```
AVS: License not found. Please install it to C:\Windows\system32\drivers\avs_signedlicense.bin
AVS: License too small. Make sure the license is signed.
SystemSKU      Chell
```

そのためデバイスの列挙、エンドポイントの作成、レベルメーターはいずれも正常で、スピーカーは無音になります。

### パッチ箇所

3 つのスタックでそれぞれ 5、12、12 箇所、合計 153 バイトを変更しています。形は 2 種類だけです。

**エラー分岐がそのまま戻る形**（4 箇所 / 24 バイト）：

```asm
mov eax, 0xC000026A    ; 検証失敗時に戻り値へ書き込む
jmp 0x140004970        ; 関数の後処理へジャンプ
```

**三項演算子の後処理**（25 箇所 / 129 バイト）：

```asm
sbb  ebx, ebx          ; 直前の比較に応じて 0 か -1
not  ebx               ; 反転
and  ebx, 0xC000026A   ; 条件成立なら失敗コード、そうでなければ 0
```

これは `return cond ? STATUS_X : STATUS_SUCCESS;` のコンパイル結果です。パッチは即値を 0 に
書き換え（`and ebx, 0`）、条件が戻り値に影響しなくなります。元の命令は 6 バイト、
置き換え後は 3 バイトで、余りは `nop` で埋めるため後続のオフセットは動きません。

箇所ごとのファイルオフセット、仮想アドレス、変更前後のバイトと命令は
[docs/PATCH-SITES.md](docs/PATCH-SITES.md) にあります。

INF、HWID テーブル、サービス定義、DSP ファームウェアは変更していません。`CsAudioCallbackAPI` のコールバックは登録されたままで、`ssm4567` はこのコールバック経由で Speaker エンドポイントを登録します。

### 署名

1 バイトでも変更すると元の WHQL 署名は無効になるため、パッチ後のバイナリはカタログを
作り直して再署名する必要があります。ここに含まれるオーディオパッケージは第三者が
再署名したもので、カタログのタイムスタンプは公開ルートが信頼しない証明書につながるため、
クリーンなシステムでは `pnputil` が拒否します。

```
0xE0000242  発行者の信頼が確立されていない Authenticode(TM) 署名付きカタログです
```

`certs/pikachu-certificate-install.exe` が必要なルートを `LocalMachine\Root` と
`LocalMachine\TrustedPublisher` の両方に書き込みます。片方でも欠けるとエラーが再現します。
インストーラーはドライバーを入れる前にこれを実行します。

自分で変更したバイナリに再署名する手順（カタログの再生成、署名、ルートのインポート、検証）は
[FakeSign](https://github.com/PIKACHUIM/FakeSign) にあります。本リポジトリにコード署名証明書は
含まれていません。

37 パッケージそれぞれの署名元と、ルートのインポートが必要かどうかは
[docs/SIGNATURES.md](docs/SIGNATURES.md) にあります。必要なのは 7 つで、残り 30 は
Microsoft WHQL か公開ルートにつながる第三者署名です。

誤解しやすい点として、`Get-AuthenticodeSignature` が `Valid` を返しても `pnputil` が
受け入れるとは限りません。前者は PKCS#7 の構造と証明書チェーンのみを検証し、後者は
setupapi の発行者信頼チェックも必要とします。また INF は改行コードを含め 1 バイトも
変更できません。カタログにハッシュが記録されているためです。

バイト単位の差分と逆アセンブルは [docs/PATCH-ANALYSIS.md](docs/PATCH-ANALYSIS.md)、
29 箇所のオフセット・バイト・前後の命令は [docs/PATCH-SITES.md](docs/PATCH-SITES.md) にあります。
比較ツールは `tools/compare-drivers.py` です。

## ライセンス

本リポジトリのスクリプト、ドキュメント、ビルド設定は MIT ライセンスで公開されています。全文は [LICENSE](LICENSE) を参照してください。

リポジトリ内のドライバーバイナリの著作権は各ベンダーに帰属し、MIT ライセンスの対象外です。

| コンポーネント | ライセンス |
|---|---|
| CoolStar オーディオドライバー | マシン単位の有償ライセンス、<https://coolstar.org/chromebook/> 参照 |
| `src/ssm4567` | CoolStar、`src/ssm4567/LICENSE.txt` 参照 |
| `tools/dpinst/dpinst.exe` | Microsoft、WDK に同梱 |
| ChromeOS EC、タッチパッド、カメラ、DPTF などのドライバー | 各ベンダー |

MIT は本リポジトリが追加した部分のみを対象とし、上記コンポーネントのライセンス状態を変更するものではありません。本プロジェクトは Google、CoolStar、いずれのハードウェアベンダーとも関係ありません。Chromebook への Windows 導入には文鎮化リスクがあり、ファームウェアの変更は特に危険です。ドライバーのインストール自体は可逆で、アンインストールスクリプトはドライバーとインポートしたルート証明書の両方を削除します。
