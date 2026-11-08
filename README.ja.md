<div align="center">

# ChromeBook CoolStar オーディオドライバーパック

Chromebook に Windows を入れたらスピーカーが鳴らない、そのためのドライバー一式

[简体中文](README.md) · [English](README.en.md) · [繁體中文](README.zh-TW.md) · **日本語**

![platform](docs/badges/platform.svg) ![drivers](docs/badges/drivers.svg) ![installer](docs/badges/installer.svg) ![upstream](docs/badges/upstream.svg)

</div>

[これは何か](#これは何か) · [まずどれを入れるか決める](#まずどれを入れるか決める) · [インストール](#インストール) · [ドライバー一覧](#ドライバー一覧) · [正規版が無音になる理由](#正規版が無音になる理由) · [構成](#構成) · [開発](#開発) · [確認した機種](#確認した機種) · [既知の欠落](#既知の欠落) · [注意事項](#注意事項)

---

## これは何か

HP Chromebook 13 G1 に Windows を入れたときの話です。デバイスマネージャーはきれいで、音量スライダーも動く。テスト音を流すとスピーカーエンドポイントのメーターは 0.35 まで振り切れる。スピーカーからは何も出ません。

最初はアンプに電源が入っていないのだと思いました。ドライバーのバージョンを変え、coreboot を焼き直し、高速スタートアップも切ってみた。どれも変化なし。CoolStar の `.sys` をバイナリエディタで開いて文字列を眺めたところ、`AVS: License not found` という行がありました。参照先は `C:\Windows\system32\drivers\avs_signedlicense.bin` です。ライセンス検証に失敗すると `DriverEntry` が `0xC000026A` を返し、呼び出し側はそこで止まって ADSP を起動しません。ADSP が動かなければ I2S のクロックが出ず、クロックのないアンプはシャットダウンのままです。

デバイスの列挙も、エンドポイントの生成も、メーターの表示も正常なのに音が出ない。この組み合わせはハード故障に見えますが、実際にはソフト側が経路の後半を切っています。詳しくは[正規版が無音になる理由](#正規版が無音になる理由)に書きました。

このリポジトリは、その調査で集まったものです。CoolStar のオーディオスタック 3 種、その下にぶら下がる codec とアンプ、それに ChromeOS の EC バス、タッチパッド、指紋認証、カメラ。

中身は生の `INF` + `SYS` + `CAT` だけです。アーカイブも自己解凍インストーラーもありません。インストール中に実行される外部バイナリは 2 つだけで、Microsoft の `DPInst` と `certs/` にある証明書インストーラーです。どちらも中身を確認できます。

上流はマシン単位の有償ライセンスです。ここにあるのは、すでにライセンスを持っているハード向けのもので、ライセンスファイルも生成ツールもアクティベーションツールも入っていません。

## まずどれを入れるか決める

Chromebook のオーディオはプラットフォームごとに 3 つのアーキテクチャに分かれていて、互換性がなく、ハードウェア ID も重なりません。

| プラットフォーム | 入れるもの | サービス名 |
|---|---|---|
| Skylake、Kaby Lake、Amber Lake、Braswell、Apollo Lake | `drivers/audio/csaudiosstavs` | `CsAudioSSTAVS` |
| Gemini Lake、Jasper Lake、Ice Lake、Tiger Lake、Alder Lake 以降 | `drivers/audio/csaudiointcsof` | `CsAudioIntcSOF` |
| AMD の Chromebook、Rembrandt、Phoenix など | `drivers/audio/csaudioamdsof` | `CsAudioAmdSOF` |
| glados、chell 系のボードのスピーカー、例として HP Chromebook 13 G1 | さらに `drivers/audio/ssm4567` | `ssm4567` |

最後の行は補足が要ります。Skylake 世代の Chromebook はスピーカーのアンプを I2C にぶら下げ、ヘッドホンは別の codec を通します。経路が 2 本に分かれているわけです。ヘッドホンは鳴るのにスピーカーが無音なら、不足しているのは `ssm4567` です。メインドライバーのバージョンの問題ではありません。

判断がつかなければ全部入りを入れてください。3 種とも同梱されていて、`DPInst` はハードウェア ID が一致したものだけをバインドします。残り 2 つは入っても何もしません。

## インストール

[Releases](../../releases) から `ChromeBook-CoolStar-Audio-Driver-<バージョン>-x64.exe` を取って、管理者として実行し、再起動します。

自分でビルドするなら [Inno Setup 6](https://jrsoftware.org/isinfo.php) だけで足ります。WDK も Visual Studio も要りません。

```bat
ISCC.exe installer\Install.iss

:: バージョンを渡すと出力名も変わる
ISCC.exe /DMyAppVersion=1.0.5 installer\Install.iss
```

出力は `dist\` に出ます。

片方だけ壊れている場合、たとえばスピーカーは無音でヘッドホンは鳴るときは、そのパッケージだけ入れられます。先に署名ルートを信頼してから追加します。

```bat
:: 管理者 PowerShell、展開したディレクトリで
certs\pikachu-certificate-install.exe /VERYSILENT

pnputil /add-driver drivers\audio\ssm4567\ssm4567.inf /install
```

`pnputil` は `ACPI\INT343B\0` と `\1` の 2 回バインドします。成功するとデバイスマネージャーに `Analog Devices SSM4567 Amplifier` が 2 つ、正常な状態で並びます。

## ドライバー一覧

37 パッケージ、198 個のハードウェア ID。各パッケージのバージョン、プロバイダー、すべての HWID は [docs/DRIVERS.md](docs/DRIVERS.md) にあります。あの表は `python tools/gen-driver-docs.py` が INF から生成したものなので、手で編集しても次の生成で上書きされます。

### オーディオ

| パッケージ | バージョン | 内容 |
|---|---|---|
| `csaudiosstavs` | 1.0.4.0 | AVS アーキテクチャ、Skylake から Apollo Lake |
| `csaudiointcsof` | 1.0.4.0 | SOF アーキテクチャ、Gemini Lake 以降 |
| `csaudioamdsof` | 1.0.1.0 | AMD プラットフォーム向け SOF |
| `ssm4567` | 1.0.1 | ADI の I2S アンプ、glados 系ボード用 |

### Codec

`nau8825` · `nau88l25rvp` · `cs42l42` · `da7219` · `rt5682` · `rt5682s` · `croseccodec`

### アンプ

`max98357a` · `max98390` · `opengmaxcodec` · `rt1011` · `rt1015` · `rt1019`

### バスとシステム

`acpbus` · `cbtable` · `crosecbus` · `croseci2c` · `crosectypec` · `sklhdaudbus` · `intelpmc` · `inteltcss` · `iaLPSS2_SPI` · `iaLPSS2_UART2` · `dptf_acpi` · `gnodriver`

### 入力とカメラ

`crostouchpad-{synaptics,cypress,cypress-smbus,atmel,elan}` · `crosfingerprint` · `ov5675` · `iaisp64_46xx_ADL`

リポジトリにはあるけれど、既定では入れないパッケージが 2 つあります。`nau88l25rvp` と `nau8825` は同じ `ACPI\10508825` を取り合うので同時には使えません。既定では CoolStar 版を入れます。`gnodriver` は INF だけで `sys` を持たないため、入れてもバインドする対象がありません。

インストールもアンインストールも、[installer/payload.list](installer/payload.list) という 1 つのリストを読みます。順序もそこに書いてあります。

## 正規版が無音になる理由

元の `.sys` には、次の文字列がそのまま入っています。平文の ASCII で、パックも難読化もされていません。

```
AVS: License not found. Please install it to C:\Windows\system32\drivers\avs_signedlicense.bin
AVS: License too small. Make sure the license is signed.
SystemSKU      Chell
```

`DriverEntry` はライセンスファイルを開き、署名を検証し、SMBIOS のボード名と突き合わせます。どこかで失敗すると NTSTATUS `0xC000026A` を返し、呼び出し側はそこで ADSP の起動を諦めます。

### パッチが触っている場所

3 つのスタックで 5 箇所、12 箇所、12 箇所。合計 153 バイトで、形はどれも同じです。

```asm
mov eax, 0xC000026A    ; 検証に失敗したときの戻り値
mov eax, 0              ; いまはそのまま成功を返す
```

`return cond ? STATUS_X : STATUS_SUCCESS;` は `and reg, 0xC000026A` にコンパイルされます。パッチはこれを `and reg, 0` に変え、余ったバイトを `nop` で埋めます。命令長が変わらないので、後続のオフセットもずれません。

INF、HWID テーブル、サービス定義、DSP ファームウェアには手を付けていません。`CsAudioCallbackAPI` のコールバックもそのまま登録されていて、`ssm4567` のアンプドライバーはこのコールバック経由で Speaker エンドポイントを得ています。書き換えたのはライセンス判定だけで、オーディオ経路そのものは純正のコードです。

### 署名と信頼チェーン

1 バイト変えた時点で WHQL 署名は無効になるので、パッチ版は再署名されています。しかも 2 つのファイルで署名者が違います。

| ファイル | 変更前 | 変更後 | タイムスタンプ |
|---|---|---|---|
| `.sys` | Microsoft WHQL | Airspace Intelligent Technology、常州 | DigiCert、公開 TSA |
| `.cat` | Microsoft WHQL | 福建六壬網安 | Pikachu Time Sub CA、自前 |

`.sys` のチェーンは公開ルートまで辿れるので、Secure Boot を切っていれば読み込まれます。`.cat` のタイムスタンプは自前の TSA のもので、公開ルートは認めません。`pnputil` はここで止まります。

```
0xE0000242  発行者の信頼が確立されていない Authenticode(TM) 署名付きカタログです
```

`certs/` のインストーラーはこのためにあります。ドライバーを入れる前に走り、ルートを `LocalMachine\Root` と `LocalMachine\TrustedPublisher` の両方に書き込みます。片方でも欠けると 0xE0000242 に戻ります。

先に言っておくと助かる落とし穴があります。`Get-AuthenticodeSignature` が `Valid` を返しても、`pnputil` がそのパッケージを受け取るとは限りません。前者は PKCS#7 の構造と証明書チェーンを見るだけで、後者は setupapi の発行者信頼チェックも通します。あと INF は 1 バイトも変えてはいけません。改行コードも含みます。カタログに INF のハッシュが入っているからです。`.gitattributes` で `*.inf`、`*.sys`、`*.cat` を `binary` にしているのはそのためです。

バイト単位の差分、逆アセンブル、[akibaPatcher](https://github.com/akibaGumi/akibaPatcher) のやり方との比較は [docs/PATCH-ANALYSIS.md](docs/PATCH-ANALYSIS.md) にあります。

## 構成

```
.
├── .github/workflows/release.yml   CI。バージョンを渡すと Release が出る
├── certs/                          署名ルート、Inno Setup のインストーラー 1 つ
├── docs/
│   ├── DRIVERS.md                  ドライバー表、37 パッケージ / 198 HWID、生成物
│   ├── PATCH-ANALYSIS.md           パッチの内容とバイト単位の差分
│   └── VENDOR.md                   入れられなかったもの、とその理由
├── drivers/                        ドライバー本体
│   ├── audio/  codec/  amp/        音を出す部分
│   ├── bus/    input/  camera/  system/
├── installer/
│   ├── Install.iss                 Inno Setup スクリプト
│   ├── payload.list                インストール順
│   └── License.rtf  dpinsts.ico
├── reference/originals/            正規の CoolStar パッケージ、diff 用で導入はしない
├── scripts/
│   ├── preflight.ps1               パッケージ前の payload 検証
│   ├── trust-certs.bat             手動導入時に署名ルートを信頼させる
│   ├── install-drivers.bat         payload.list を読んで DPInst を呼ぶ
│   └── uninstall-drivers.bat       同じリストを /U で辿る
├── src/ssm4567/                    ADI SSM4567 アンプのソース、KMDF + I2C
├── tools/
│   ├── dpinst/dpinst.exe           Microsoft DPInst 2.1
│   ├── gen-driver-docs.py          docs/DRIVERS.md の生成
│   └── compare-drivers.py          正規版とパッチ版のバイト比較
└── vendor/                         ローカル作業用、git 管理外、docs/VENDOR.md 参照
```

## 開発

パッケージングは Inno Setup 6 だけで済みます。`choco install innosetup` で入ります。`src/ssm4567` を触るときだけ VS 2022 と WDK が要ります。WDK は対応する VS のバージョンが決まっているので、ずれているとビルドが通りません。補助スクリプトは PowerShell 5.1 か 7、Python 3.9 以降。`compare-drivers.py` の逆アセンブル部分は `pip install capstone` が要りますが、なくても動きます。命令が見えないだけです。

```bat
:: payload の検証。ディレクトリ欠け、inf 欠け、カタログが参照する
:: ファイルの不在はここで落ちる
powershell -ExecutionPolicy Bypass -File scripts\preflight.ps1

:: ビルド
ISCC.exe /DMyAppVersion=1.0.5 installer\Install.iss

:: パッケージを触ったあとに表を更新
python tools\gen-driver-docs.py

:: あるパッケージが正規版とどう違うかを見る
python tools\compare-drivers.py reference\originals\csaudiosstavs drivers\audio\csaudiosstavs
```

CI は [.github/workflows/release.yml](.github/workflows/release.yml) にあります。手動でバージョンを渡して起動するか、`v*` タグを push します。流れは preflight、Inno Setup の導入、`ISCC /DMyAppVersion=<v>`、`SHA256SUMS.txt` の生成、artifact のアップロード、`gh release create`。同名タグがあるときは `--clobber` 付きのアップロードに切り替わります。preflight が落ちればその場で中断するので、壊れたパッケージが Release に届くことはありません。

ドライバーを足すときは `drivers/<カテゴリ>/<名前>/` に置き、既定で入れるなら `payload.list` に 1 行足して、生成スクリプトと preflight を流してコミットします。ドライバーのバージョンは INF から、インストーラーのバージョンは CI の入力から取るので、手で揃える必要はありません。

アンインストールはアプリ一覧からでも `scripts\uninstall-drivers.bat` でも構いません。同じリストを辿って `DPInst /U` を呼び、最後に CA も外します。

## 確認した機種

| 機種 | ボード | プラットフォーム | 結果 |
|---|---|---|---|
| HP Chromebook 13 G1 | `chell` | Skylake | スピーカーもヘッドホンも正常。`ssm4567` と `csaudiosstavs` の両方が必要 |

確認できたのはこの 1 台だけです。同じプラットフォームに属する機種なら動くはずですが、試す手段がないので保証はしません。試した方がいれば、機種名、`SystemSKU`、`ACPI\INT343B` と `ACPI\10508825` のハードウェア ID を送ってください。表に追加します。

## 既知の欠落

上流より古いパッケージが 4 つ、意図的に外したものが 3 つあります。理由と代替案は [docs/VENDOR.md](docs/VENDOR.md) にまとめました。

一番効くのは Intel Serial IO の I2C と GPIO ドライバーです。chell の codec とアンプはそのバスにぶら下がっていて、まっさらな状態から入れると codec のレジスタすら書けません。手元にあるのは DriverStore のダンプだけで、カタログが付いていません。x64 で入るパッケージが組めないので、ここで止まっています。

## 注意事項

CoolStar オーディオドライバー © CoolStar、<https://coolstar.org/chromebook/> で配布、上流はマシン単位の有償ライセンスです。このリポジトリは非公式の再パッケージで、ライセンスを保有しているハード向けです。ライセンスファイル、生成ツール、アクティベーションツールは含みません。`DPInst.exe` © Microsoft、WDK に同梱。`src/ssm4567` © CoolStar、<https://github.com/coolstar/ssm4567> より、ライセンスは `src/ssm4567/LICENSE.txt`。ChromeOS EC、タッチパッド、カメラ、DPTF のドライバーは各ベンダーの著作物です。

Google、CoolStar、いずれのハードウェアベンダーとも関係ありません。Chromebook に Windows を入れるのは文鎮化リスクを伴い、ファームウェアを触る場合は特に危険です。ドライバーの導入自体は元に戻せます。アンインストールスクリプトがドライバーと信頼したルートの両方を消します。
