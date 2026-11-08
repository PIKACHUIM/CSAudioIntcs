# 签名根证书

`drivers/` 里的驱动 catalog 不是签给公共信任根的，Windows 默认不认。装之前必须先把对应的
根证书导进系统，否则 `pnputil` 会停在这一句：

```
0xE0000242  尚未建立对 Authenticode(TM)签名的目录的发布者信任
```

## 怎么装

用这里的安装器，它会同时写 `LocalMachine\Root` 和 `LocalMachine\TrustedPublisher`
（后者是 setupapi 对驱动包额外的检查），并注册自己的卸载项：

```bat
pikachu-certificate-install.exe /VERYSILENT /SUPPRESSMSGBOXES /NORESTART
```

安装器（`installer/Install.iss`）在装驱动之前会自动跑这一步，不用手动执行。
手动装驱动的情况下才需要自己跑，或者调 `scripts\trust-certs.bat`。

卸载驱动时 `scripts\uninstall-drivers.bat` 会顺手把这个 CA 一起卸掉。

## 里面是什么

| 项 | 值 |
|---|---|
| 文件 | `pikachu-certificate-install.exe`（3.3 MB，Inno Setup 打包） |
| 产品名 | `Pikacu Test CA Truster` |
| SHA-256 | `239f9f2b883fce7865d2808bece618c51efd3e794a541ef15abd0d8b2092d550` |
| 导入位置 | `LocalMachine\Root`、`LocalMachine\TrustedPublisher` |

它导入的根是 `CN=Pikachu Test CA RSA`，有效期到 2100 年。证书本身不跟着仓库走，
全部封在这个 exe 里；更新 CA 时换 exe 即可，脚本和 iss 都不用改。

## 为什么只需要信任根

三个音频栈的 `.sys` 用的是 DigiCert 公共时间戳，链能追到公共根，所以关掉 Secure Boot
就能加载。`.cat` 不一样：它的时间戳来自自建的 `Pikachu Time Sub CA`，公开根不认这条链。
所以只有 catalog 这一步需要额外信任，而 catalog 决定的是「这个驱动包能不能被安装」，
不是驱动的运行。

中间证书（`Pikachu Time Sub CA`）本身写在 `.cat` 里，验签时会从 catalog 内部取，
所以只需要根。

细节见 [../docs/PATCH-ANALYSIS.md](../docs/PATCH-ANALYSIS.md) 第四节。

## 想手动清理

```bat
"%ProgramFiles%\Pikacu Test CA Truster\unins000.exe" /VERYSILENT
```

或者直接删证书存储里的条目（产品名里 `Pikacu` 少一个 h，是原作者写的，卸载路径得照抄）。
