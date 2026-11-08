<div align="center">

# ChromeBook CoolStar オーディオドライバーパック

Chromebook に Windows を入れたあと、スピーカーを鳴らすまで

[简体中文](README.md) · [English](README.en.md) · [繁體中文](README.zh-TW.md) · **日本語**

![platform](docs/badges/platform.svg) ![drivers](docs/badges/drivers.svg) ![installer](docs/badges/installer.svg) ![upstream](docs/badges/upstream.svg)

</div>

---

## これは何か

もともとは HP Chromebook 13 G1 を 1 台鳴らすためのものでした。Windows は問題なく入り、
デバイスマネージャーもきれいで、音量スライダーも動き、再生中はエンドポイントの
レベルメーターが振り切れる。それでもスピーカーは無音。原因はドライバー内部の
ライセンス検証で、これが失敗すると ADSP が起動せず、アンプはシャットダウン状態のまま
になります。

いまは他の機種もカバーしています。CoolStar のオーディオスタック 3 種、その下にぶら下がる
コーデックとアンプ、さらに ChromeOS の EC バス、タッチパッド、指紋認証、カメラの
ドライバーが入っています。

中身はすべて生の `INF + SYS + CAT` です。アーカイブも自己解凍インストーラーもありません。
作業ツリーは 13 MB ほどで、インストール時に実行される外部バイナリは Microsoft の
`DPInst` と `certs/` の CA インストーラーだけです。

上流はマシン単位の有償ライセンスです。ここにあるのはすでにライセンスを持っている
ハードウェア向けで、ライセンスファイルも生成ツールもアクティベーションツールも
含みません。

## まず自分のプラットフォームを確認する

Chromebook のオーディオは 3 つのアーキテクチャに分かれていて、互換性がなく、
ハードウェア ID も重複しません。間違ったものを入れると「デバイスはあるのに無音」という
いちばん時間を食う状態になります。

| プラットフォーム | 入れるもの | サービス名 |
|---|---|---|
| Skylake / Kaby Lake / Amber Lake / Braswell / Apollo Lake | `drivers/audio/csaudiosstavs` | `CsAudioSSTAVS` |
| Gemini Lake / Jasper Lake / Ice Lake / Tiger Lake / Alder Lake 以降 | `drivers/audio/csaudiointcsof` | `CsAudioIntcSOF` |
| AMD Chromebook（Rembrandt、Phoenix など） | `drivers/audio/csaudioamdsof` | `CsAudioAmdSOF` |
| glados / chell ボードのスピーカー（HP Chromebook 13 G1 など） | 追加で `drivers/audio/ssm4567` | `ssm4567` |

プラットフォームが分からなければ全部入りのパッケージを使ってください。3 種とも
同梱されていて、DPInst はハードウェア ID が一致したものだけをバインドします。

## インストール

**ビルド済みを使う**：[Releases](../../releases) から
`ChromeBook-CoolStar-Audio-Driver-<バージョン>-x64.exe` を取得し、管理者として実行して
再起動します。

**ソースからビルドする**：[Inno Setup 6](https://jrsoftware.org/isinfo.php) だけ必要です。
WDK も Visual Studio も要りません。

```bat
ISCC.exe installer\Install.iss

:: バージョンを指定すると出力ファイル名も変わる
ISCC.exe /DMyAppVersion=1.0.5 installer\Install.iss
```

出力は `dist\` に置かれます。

**1 つだけ入れたい場合**、たとえばスピーカーだけ鳴らないとき。先に署名ルートを信頼して
から、パッケージを追加します。

```bat
:: 管理者 PowerShell、展開したディレクトリで
certs\pikachu-certificate-install.exe /VERYSILENT

pnputil /add-driver drivers\audio\ssm4567\ssm4567.inf /install
```

## ドライバー一覧

37 パッケージ、198 個のハードウェア ID。各パッケージのバージョン、プロバイダー、
すべての HWID は [docs/DRIVERS.md](docs/DRIVERS.md) にあります。この表は
`python tools/gen-driver-docs.py` が INF から生成したもので、手書きではありません。

### オーディオ

| パッケージ | バージョン | 内容 |
|---|---|---|
| `csaudiosstavs` | 1.0.4.0 | AVS アーキテクチャ、Skylake から Apollo Lake |
| `csaudiointcsof` | 1.0.4.0 | SOF アーキテクチャ、Gemini Lake 以降 |
| `csaudioamdsof` | 1.0.1.0 | AMD プラットフォーム向け SOF |
| `ssm4567` | 1.0.1 | ADI の I2S アンプ、glados 系ボード用 |

### コーデック

`nau8825` · `nau88l25rvp` · `cs42l42` · `da7219` · `rt5682` · `rt5682s` · `croseccodec`

### アンプ

`max98357a` · `max98390` · `opengmaxcodec` · `rt1011` · `rt1015` · `rt1019`

### バスとシステム

`acpbus` · `cbtable` · `crosecbus` · `croseci2c` · `crosectypec` · `sklhdaudbus` ·
`intelpmc` · `inteltcss` · `iaLPSS2_SPI` · `iaLPSS2_UART2` · `dptf_acpi` · `gnodriver`

### 入力とカメラ

`crostouchpad-{synaptics,cypress,cypress-smbus,atmel,elan}` · `crosfingerprint` ·
`ov5675` · `iaisp64_46xx_ADL`

### 同梱されるが既定では入れないもの

`codec/nau88l25rvp` と `codec/nau8825` は同じ `ACPI\10508825` を取り合うので、
同時には使えません。既定では CoolStar 版を入れます。`system/gnodriver` は INF だけで
`sys` を持たないため、入れてもバインドするものがありません。

インストール順は [installer/payload.list](installer/payload.list) にあります。
インストーラーとアンインストーラーの両方がこの 1 ファイルを読みます。

## 正規版が無音になる理由

元の `.sys` には次の文字列がそのまま入っています。

```
AVS: License not found. Please install it to C:\Windows\system32\drivers\avs_signedlicense.bin
AVS: License too small. Make sure the license is signed.
SystemSKU      Chell
```

`DriverEntry` はライセンスを読み、署名を検証し、SMBIOS のボード名と照合します。
ここで失敗すると NTSTATUS `0xC000026A` を返し、呼び出し側は ADSP を起動しません。
ADSP が動かなければ I2S クロックが出ず、クロックのないアンプはミュートのままです。

症状が妙に見えるのはそのためです。デバイスもエンドポイントも正常、メーターも振り切れ、
それでも音が出ない。

### パッチが変えているもの

3 つのスタックで 5 箇所、12 箇所、12 箇所。合計 153 バイトで、形はすべて同じです。

```asm
mov eax, 0xC000026A    ; 変更前：検証に失敗したらこのエラーコードを返す
mov eax, 0              ; 変更後：そのまま成功として返す
```

`return cond ? STATUS_X : STATUS_SUCCESS;` は `and reg, 0xC000026A` にコンパイルされる
ので、これを `and reg, 0` に変え、余ったバイトを `nop` で埋めています。

INF、HWID テーブル、サービス定義、DSP ファームウェアは一切触っていません。
`CsAudioCallbackAPI` コールバックもそのまま登録されていて、`ssm4567` アンプドライバーは
このコールバック経由で Speaker エンドポイントを得ています。

### 署名

1 バイト変えた時点で WHQL 署名は無効になるため、パッチ版は再署名されています。
しかも 2 つのファイルで証明書が違います。

| ファイル | 変更前 | 変更後 | タイムスタンプ |
|---|---|---|---|
| `.sys` | Microsoft WHQL | Airspace Intelligent Technology（常州） | DigiCert、公開 TSA |
| `.cat` | Microsoft WHQL | 福建六壬網安 | Pikachu Time Sub CA、独自 |

`.sys` のチェーンは公開ルートまで辿れるので、Secure Boot を切っていれば読み込まれます。
`.cat` のタイムスタンプは独自 TSA のもので、公開ルートは認めないため `pnputil` は
`0xE0000242` で止まります。`certs/` のインストーラーはそのためにあります。ドライバーを
入れる前に走り、ルートを `LocalMachine\Root` と `LocalMachine\TrustedPublisher` に
書き込みます。

先に知っておくと助かる落とし穴が 2 つ。`Get-AuthenticodeSignature` が `Valid` を返しても
`pnputil` が受け入れるとは限りません。前者は PKCS#7 だけを見て、後者は setupapi の
発行者信頼チェックも通す必要があります。もう 1 つは INF のバイトを 1 つも変えられない
こと。改行コードも含みます。カタログにハッシュが入っているためです。
`.gitattributes` で `*.inf`、`*.sys`、`*.cat` を `binary` にしているのはそのためです。

バイトレベルの差分、逆アセンブル、[akibaPatcher](https://github.com/akibaGumi/akibaPatcher)
との手法の比較は [docs/PATCH-ANALYSIS.md](docs/PATCH-ANALYSIS.md) にあります。

## 構成

```
.
├── .github/workflows/release.yml   CI：バージョン入力 → ビルド → Release
├── certs/                          署名ルート、Inno Setup インストーラー 1 つ
├── docs/
│   ├── DRIVERS.md                  ドライバー表、37 パッケージ / 198 HWID、生成物
│   ├── PATCH-ANALYSIS.md           パッチの原理とバイトレベルの差分
│   └── VENDOR.md                   git に入らないもの、その理由
├── drivers/                        ドライバー本体
│   ├── audio/  codec/  amp/        音に直結する部分
│   ├── bus/    input/  camera/  system/
├── installer/
│   ├── Install.iss                 Inno Setup スクリプト
│   ├── payload.list                インストール順
│   └── License.rtf  dpinsts.ico
├── reference/originals/            CoolStar 公式の元パッケージ、diff の基準、導入しない
├── scripts/
│   ├── preflight.ps1               パッケージング前の payload 検証
│   ├── trust-certs.bat             手動での署名ルート信頼
│   ├── install-drivers.bat         payload.list を DPInst で処理
│   └── uninstall-drivers.bat       同じ一覧を /U で処理
├── src/ssm4567/                    ADI SSM4567 アンプドライバーのソース、KMDF + I2C
├── tools/
│   ├── dpinst/dpinst.exe           Microsoft DPInst 2.1
│   ├── gen-driver-docs.py          docs/DRIVERS.md の生成
│   └── compare-drivers.py          元パッケージとパッチ版のバイト比較
└── vendor/                         ローカル退避、git 管理外、docs/VENDOR.md 参照
```

## 開発

パッケージングに必要なのは Inno Setup 6（`choco install innosetup`）だけです。
`src/ssm4567` を作り直す場合だけ VS 2022 と WDK が要ります。WDK と VS の対応表は
先に確認してください。補助スクリプトは PowerShell 5.1 か 7、Python 3.9 以上。
`compare-drivers.py` の逆アセンブル部分だけ `pip install capstone` が必要です。

```bat
:: payload の検証。ディレクトリ欠落、inf 欠落、catalog 参照切れを検出する
powershell -ExecutionPolicy Bypass -File scripts\preflight.ps1

:: パッケージング
ISCC.exe /DMyAppVersion=1.0.5 installer\Install.iss

:: ドライバーを触ったら一覧を作り直す
python tools\gen-driver-docs.py

:: 公式版との差分を見る
python tools\compare-drivers.py reference\originals\csaudiosstavs drivers\audio\csaudiosstavs
```

CI は [.github/workflows/release.yml](.github/workflows/release.yml) です。手動実行で
バージョン番号を入れるか、`v*` タグを push します。流れは preflight、Inno Setup の導入、
`ISCC /DMyAppVersion=<v>`、`SHA256SUMS.txt` の生成、artifact のアップロード、
`gh release create`。同じタグがあれば上書きアップロードに切り替わります。preflight が
失敗するとビルドは中断されるので、壊れた payload が Release に出ることはありません。

ドライバーを追加するときは `drivers/<カテゴリ>/<名前>/` に置き、既定で入れるなら
`payload.list` に追記し、生成スクリプトと preflight を流してからコミットします。
ドライバーのバージョンは INF から、インストーラーのバージョンは CI の入力から取ります。

アンインストールは「アプリと機能」から、または `scripts\uninstall-drivers.bat`。
同じ一覧をたどって `DPInst /U` を実行し、最後に CA も削除します。

## 動作を確認した機種

| 機種 | ボード | プラットフォーム | 結果 |
|---|---|---|---|
| HP Chromebook 13 G1 | `chell` | Skylake | スピーカーもヘッドホンも動作、`ssm4567` と `csaudiosstavs` が必要 |

他の機種もプラットフォーム的には動くはずですが、1 台ずつは試していません。
試した方は機種、`SystemSKU`、デバイスマネージャーのハードウェア ID を共有してもらえると
助かります。

## 既知の欠落

4 つのパッケージは上流に新しい版があり、3 系統は意図的に入れていません。理由と代替案は
[docs/VENDOR.md](docs/VENDOR.md) にまとめてあります。いちばん影響が大きいのは
Intel Serial IO の I2C/GPIO ドライバーが無いことで、chell のコーデックとアンプは
その I2C バスに載っています。手元にあるのは DriverStore のダンプだけで、カタログが
含まれていないためインストール可能なパッケージに組み直せません。

## 注意事項

CoolStar オーディオドライバー © CoolStar、<https://coolstar.org/chromebook/> 経由で配布、
上流はマシン単位の有償ライセンスです。本リポジトリは非公式の再パッケージで、
すでにライセンスを持つハードウェア向けであり、ライセンスファイル、生成ツール、
アクティベーションツールは含みません。`DPInst.exe` © Microsoft、WDK に同梱。
`src/ssm4567` © CoolStar、<https://github.com/coolstar/ssm4567> より、
ライセンスは `src/ssm4567/LICENSE.txt`。ChromeOS EC、タッチパッド、カメラ、DPTF の
ドライバーは各ベンダーの著作物です。

Google、CoolStar、いかなるハードウェアベンダーとも関係ありません。Chromebook への
Windows 導入は、特にファームウェアを触る場合に文鎮化リスクがあります。ドライバーの
インストール自体は元に戻せます。
