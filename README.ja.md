# ChromeBook CoolStar オーディオドライバー統合パッケージ

[简体中文](README.md) | [English](README.en.md) | [繁體中文](README.zh-TW.md) | **日本語**

Chromebook に Windows を入れたあと、最後まで厄介なのは GPU でもタッチパッドでもなく
**音**です。このリポジトリは、Chromebook から音を出すために必要なドライバーを
1 つのインストーラーにまとめたものです。CoolStar のオーディオスタック、その下にぶら下がる
コーデックとスピーカーアンプ、さらに ChromeOS の EC バス、タッチパッド、指紋認証、
カメラのドライバーを含みます。

中身はすべて**生のドライバーファイル**（`INF + SYS + CAT`、一部にファームウェア blob）で、
アーカイブも自己解凍インストーラーもありません。追跡対象は 13 MB、実行される外部バイナリは
Microsoft の `DPInst` と Windows 標準の `certutil` だけです。

> これは**非公式の再パッケージ**です。ドライバー本体は CoolStar の成果物
> （<https://coolstar.org/chromebook/>）で、上流はマシン単位のライセンス販売製品です。
> 詳細は[注意事項](#注意事項)を参照してください。

---

## 目次

- [これは何か](#これは何か)
- [クイックスタート](#クイックスタート)
- [ドライバー一覧](#ドライバー一覧)
- [パッチの仕組み](#パッチの仕組み)
- [リポジトリ構成](#リポジトリ構成)
- [開発方法](#開発方法)
- [動作確認済みハードウェア](#動作確認済みハードウェア)
- [注意事項](#注意事項)

---

## これは何か

Chromebook のオーディオは一般的なノート PC とはまったく別物です。

| 一般的なノート PC | Chromebook |
|---|---|
| HD Audio コーデックが HDA バス上にあり、挿せば鳴る | オーディオコントローラーは Intel **AVS / SOF** の ADSP、コーデックとアンプは **I2C / I2S** 上 |
| コーデックは BIOS が初期化する | ドライバーが I2C 経由でコーデックのレジスタを 1 つずつ設定する |
| アンプはコーデック内蔵 | アンプは独立チップ（MAX98357A / SSM4567 / RT1015…）で、GPIO と I2S クロックで有効化する |

そのためドライバーが無いときの症状は「デバイスマネージャーに黄色い感嘆符」ではなく、
もっと厄介な形で現れます——
**デバイスはすべて正常、4 つのオーディオエンドポイントも揃っている、再生中は
エンドポイントのレベルメーターが振り切れる、なのにスピーカーからは無音**。
ADSP が起動していないので I2S クロックが出ず、アンプはパワーダウン／ミュートのままです。

このパッケージが解決するのはそこです。

---

## クイックスタート

**そのまま入れる**：[Releases](../../releases) から最新の
`ChromeBook-CoolStar-Audio-Driver-<バージョン>-x64.exe` を取得し、管理者として実行して再起動。

**自分でビルドする**（[Inno Setup 6](https://jrsoftware.org/isinfo.php) のみ。
WDK / Visual Studio / コンパイラーは不要）:

```bat
ISCC.exe installer\Install.iss

:: バージョンを指定する場合
ISCC.exe /DMyAppVersion=1.0.5 installer\Install.iss
```

出力は `dist\` に生成されます。

**特定のパッケージだけ入れる**（例：スピーカーアンプのみ）:

```bat
:: 管理者 PowerShell、展開したディレクトリで実行
certutil -addstore -f Root             certs\pikachu-testca-root.cer
certutil -addstore -f TrustedPublisher certs\pikachu-testca-root.cer
pnputil /add-driver drivers\audio\ssm4567\ssm4567.inf /install
```

---

## ドライバー一覧

37 パッケージ、198 個のハードウェア ID。各パッケージの DriverVer・Provider・
すべての HWID を含む完全な一覧は **[docs/DRIVERS.md](docs/DRIVERS.md)** にあり、
`python tools/gen-driver-docs.py` で INF から自動生成しています。

### オーディオ（`drivers/audio/`）

| パッケージ | バージョン | 対象プラットフォーム |
|---|---|---|
| `csaudiosstavs` | 1.0.4.0 | **AVS**：Skylake / Kaby Lake / Amber Lake / Braswell / Apollo Lake |
| `csaudiointcsof` | 1.0.4.0 | **SOF**：Gemini Lake / Jasper Lake / Ice Lake / Tiger Lake / Alder Lake… |
| `csaudioamdsof` | 1.0.1.0 | **AMD SOF**：Rembrandt / Phoenix など AMD Chromebook |
| `ssm4567` | 1.0.1 | ADI SSM4567 I2S アンプ（glados / chell = HP Chromebook 13 G1） |

> **必ず 1 つだけ**。プラットフォームを間違えるのが「デバイスはあるのに無音」の
> 最頻出原因です。AVS と SOF はまったく別のスタックでハードウェア ID も重複しないため、
> DPInst が一致するほうだけをバインドします。

### コーデック（`drivers/codec/`）

`nau8825` · `nau88l25rvp` · `cs42l42` · `da7219` · `rt5682` · `rt5682s` · `croseccodec`

### スピーカーアンプ（`drivers/amp/`）

`max98357a` · `max98390` · `opengmaxcodec` · `rt1011` · `rt1015` · `rt1019`

### バス / システム（`drivers/bus/`、`drivers/system/`）

`acpbus` · `cbtable` · `crosecbus` · `croseci2c` · `crosectypec` · `sklhdaudbus` ·
`intelpmc` · `inteltcss` · `iaLPSS2_SPI` · `iaLPSS2_UART2` · `dptf_acpi` · `gnodriver`

### 入力 / カメラ（`drivers/input/`、`drivers/camera/`）

`crostouchpad-{synaptics,cypress,cypress-smbus,atmel,elan}` · `crosfingerprint` ·
`ov5675` · `iaisp64_46xx_ADL`

### 同梱されるが既定では入れないもの

| パッケージ | 理由 |
|---|---|
| `codec/nau88l25rvp` | Nuvoton 公式リファレンスコーデック。`codec/nau8825` と同じ `ACPI\10508825` を奪い合うため排他 |
| `system/gnodriver` | INF のみのプレースホルダーで `.sys` が無い |

インストール順は [`installer/payload.list`](installer/payload.list) にあり、
インストーラーとアンインストーラーが同じファイルを読みます。

---

## パッチの仕組み

ここでは 2 つに答えます。**なぜ正規版でも無音なのか**、そして**このビルドは何を変えたのか**。
バイトレベルの diff、逆アセンブル、署名チェーンの比較、akibaPatcher との手法比較を含む
完全な証拠は **[docs/PATCH-ANALYSIS.md](docs/PATCH-ANALYSIS.md)**（中国語）にあります。

### 1. 障壁：マシン単位のライセンス検証

CoolStar のオーディオドライバーはライセンス販売製品です。元の `.sys` には
そのまま次の文字列が入っています。

```
AVS: License not found. Please install it to C:\Windows\system32\drivers\avs_signedlicense.bin
AVS: License too small. Make sure the license is signed.
SystemSKU      Chell
```

`DriverEntry` の段階でライセンスファイルを読み、署名を検証し、SMBIOS の `SystemSKU`
（ボード名）と照合します。失敗すると NTSTATUS `0xC000026A` を返し、呼び出し側は
**ADSP を起動しません**。結果として I2S クロックが出ず、アンプは通電せず、
メーターは振り切れたまま無音になります。

### 2. パッチ：失敗を成功に置き換えるだけ

3 つのスタックで合計 5 / 12 / 12 箇所、153 バイト。すべて同じ形です。

```asm
; 元 — 検証に失敗したらエラーコードを返す
mov    eax, 0xC000026A
; パッチ後 — 失敗しても成功として返す
mov    eax, 0
```

`return cond ? STATUS_X : STATUS_SUCCESS;` に対してコンパイラーは
`and reg, 0xC000026A` を生成するので、それを `and reg, 0` に変え、
余ったバイトを `nop` で埋めています。

| スタック | 箇所 | 変更バイト数 |
|---|---|---|
| `csaudiosstavs` | 5 | 25 B |
| `csaudiointcsof` | 12 | 64 B |
| `csaudioamdsof` | 12 | 64 B |

**INF、HWID テーブル、サービス定義、DSP ファームウェアは一切変更していません**。
`CsAudioCallbackAPI` コールバックもそのまま CoolStar バスドライバーに登録され、
`ssm4567` アンプドライバーはこのコールバック経由で Speaker エンドポイントを得ています。

### 3. 署名：カタログを再生成して再署名

1 バイト変えた時点で WHQL 署名は無効になるため、パッチ版は**再署名**されています。
しかも 2 つのチェーンは性質が異なります。

| ファイル | 元の署名者 | パッチ後の署名者 | タイムスタンプ |
|---|---|---|---|
| `.sys` | Microsoft WHQL | Airspace Intelligent Technology (Changzhou) | DigiCert（**公開 TSA**） |
| `.cat` | Microsoft WHQL | 福建六壬網安股份有限公司 | Pikachu Time Sub CA（**独自 TSA**） |

`.sys` のチェーンは公開の信頼されたルートまで辿れるので、Secure Boot を切っていれば
そのまま読み込まれます。一方 `.cat` のタイムスタンプは独自 TSA のもので公開ルートは
認めないため、`pnputil` は次のエラーで拒否します。

```
0xE0000242  発行者の信頼が確立されていない Authenticode(TM) 署名付きカタログです
```

**したがって対象機に独自 TSA のルートを先にインストールする必要があります**
（`certs/pikachu-testca-root.cer`、インストーラーが自動で行います）:

```bat
certutil -addstore -f Root             certs\pikachu-testca-root.cer
certutil -addstore -f TrustedPublisher certs\pikachu-testca-root.cer
```

> 時間を溶かす 2 つの落とし穴：
> ① `Get-AuthenticodeSignature` が `Valid` を返しても、**`pnputil` が受け入れるとは限りません**。
>    前者は PKCS#7 だけを検証し、後者は setupapi の発行者信頼チェックも通す必要があります。
> ② `.inf` は 1 バイトも変えてはいけません（**改行コードも含む**）。
>    カタログにそのハッシュが入っているためです。`.gitattributes` で
>    `*.inf`/`*.sys`/`*.cat` を `binary` にしているのはこの理由です。

### 4. akibaPatcher との違い

[akibaPatcher](https://github.com/akibaGumi/akibaPatcher) も同じ問題（SOF 1.0.5.0）を
解いていますが、手法はずっと乱暴です。`.sys` の Authenticode 証明書テーブルを丸ごと削除し、
20 年有効の自己署名証明書を偽造し、4 箇所の関数を `xor eax,eax; ret` /
`mov eax,1; ret` に書き換え、**さらに testsigning を必須**とします。

本リポジトリのビルドは「最小限の意味的パッチ + 実在する証明書チェーンでの再署名」で、
代わりにコード署名証明書とタイムスタンプサービスが必要になりますが、
**testsigning が不要**で、テストモードの透かしも残りません。

---

## リポジトリ構成

```
.
├── .github/workflows/release.yml   CI：バージョン入力 → ビルド → Release 公開
├── certs/                          署名チェーンのルート（Root + TrustedPublisher へ取込）
├── docs/                           DRIVERS.md / PATCH-ANALYSIS.md / VENDOR.md
├── drivers/                        ドライバー本体（機能別）
│   ├── audio/  codec/  amp/        ← 音に直結する部分
│   ├── bus/    input/  camera/  system/
├── installer/                      Install.iss + payload.list + License.rtf + アイコン
├── reference/originals/            CoolStar 公式の元パッケージ（WHQL、diff の基準）
├── scripts/                        preflight / trust-certs / install / uninstall
├── src/ssm4567/                    ADI SSM4567 アンプドライバーのソース（KMDF + I2C）
├── tools/                          dpinst.exe + gen-driver-docs.py + compare-drivers.py
└── vendor/                         ローカル退避（git 管理外、docs/VENDOR.md 参照）
```

---

## 開発方法

### 必要なもの

- **パッケージング**：Inno Setup 6（`choco install innosetup`）。WDK も Visual Studio も不要。
- **アンプドライバーの再ビルド**（`src/ssm4567`）：VS 2022 + WDK（対応バージョンを確認のこと）。
- **補助スクリプト**：PowerShell 5.1 または 7、Python 3.9+（逆アセンブル時のみ `capstone`）。

### ローカルビルドと自己チェック

```bat
:: 1) payload の検証（ディレクトリ欠落・inf 欠落・cat 参照切れを検出）
powershell -ExecutionPolicy Bypass -File scripts\preflight.ps1

:: 2) パッケージング
ISCC.exe /DMyAppVersion=1.0.5 installer\Install.iss

:: 3) ドライバーを更新したら一覧を再生成
python tools\gen-driver-docs.py

:: 4) 公式版との差分を見る
python tools\compare-drivers.py reference\originals\csaudiosstavs drivers\audio\csaudiosstavs
```

### CI

[`.github/workflows/release.yml`](.github/workflows/release.yml):

| トリガー | 動作 |
|---|---|
| `workflow_dispatch` | `version`（例 `1.0.5`）と任意のプレリリース指定 → `v1.0.5` タグで Release 公開 |
| `v*` タグの push | バージョンはタグ名から取得 |

流れ：checkout → バージョン解決 → `preflight.ps1` → Chocolatey で Inno Setup 導入 →
`ISCC /DMyAppVersion=<v>` → `SHA256SUMS.txt` 生成 → artifact アップロード →
`gh release create`（同名タグがあれば `gh release upload --clobber`）。

preflight が失敗するとビルドは中断されるため、壊れた payload が Release に届くことはありません。

### ドライバーの追加・更新

1. `drivers/<カテゴリ>/<名前>/` に配置（`.inf` + `.sys` + `.cat`、付属ファームも同階層）
2. 既定で入れるなら `installer/payload.list` に `<カテゴリ>\<名前>` を追記（順序に意味あり）
3. `python tools/gen-driver-docs.py` で一覧を更新
4. `powershell -File scripts/preflight.ps1` で自己チェック
5. コミット。ドライバー版は INF から、インストーラー版は CI 入力から

### アンインストール

「アプリと機能」から、または管理者コンソールで:

```bat
scripts\uninstall-drivers.bat
```

---

## 動作確認済みハードウェア

| 機種 | ボード | プラットフォーム | 結果 |
|---|---|---|---|
| HP Chromebook 13 G1 | `chell` | Skylake | ✅ スピーカー + ヘッドホン（`ssm4567` + `csaudiosstavs` が必要） |

その他の機種は各スタックの対象プラットフォーム的には動くはずですが、
**1 台ずつ検証はしていません**。

---

## 注意事項

- **CoolStar オーディオドライバー** — © CoolStar、<https://coolstar.org/chromebook/> 経由で配布。
  上流はマシン単位のライセンス販売製品です。本リポジトリは**非公式の再パッケージ**であり、
  すでにライセンスを持つハードウェア向けで、**ライセンスファイル・生成ツール・
  アクティベーションツールは一切含みません**。
- **`DPInst.exe`** — © Microsoft、WDK の一部として再配布。
- **`src/ssm4567`** — © CoolStar、<https://github.com/coolstar/ssm4567> より。
- **ChromeOS EC / タッチパッド / カメラ / DPTF ドライバー** — 各ベンダーの著作物。

Google、CoolStar、およびいかなるハードウェアベンダーとも関係ありません。
Chromebook の Windows 化は（特にファームウェア書き換えで）文鎮化リスクがありますが、
ドライバーのインストール自体は可逆です。
