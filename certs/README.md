# 签名根证书

`drivers/` 里部分驱动包的 catalog 签名时间来自一条公开根不信任的证书链，Windows 默认不认。
装之前必须先把对应的根证书导入系统，否则 `pnputil` 会停在这一句：

```
0xE0000242  尚未建立对 Authenticode(TM)签名的目录的发布者信任
```

哪些包需要这一步，见 [../docs/SIGNATURES.md](../docs/SIGNATURES.md)（37 个里有 7 个）。

## 怎么装

用这里的安装器，它会同时写 `LocalMachine\Root` 和 `LocalMachine\TrustedPublisher`
（后者是 setupapi 对驱动包额外的检查），并注册自己的卸载项：

```bat
pikachu-certificate-install.exe /VERYSILENT /SUPPRESSMSGBOXES /NORESTART
```

安装器（`installer/Install.iss`）在装驱动之前会自动跑这一步，不用手动执行。
手动装驱动的情况下才需要自己跑，或者调 `scripts\trust-certs.bat`。

卸载驱动时 `scripts/uninstall-drivers.bat` 会顺手把这个 CA 一起卸掉。

## 里面是什么

| 项 | 值 |
|---|---|
| 文件 | `pikachu-certificate-install.exe`（3.3 MB，Inno Setup 打包） |
| 产品名 | `Pikacu Test CA Truster` |
| SHA-256 | `239f9f2b883fce7865d2808bece618c51efd3e794a541ef15abd0d8b2092d550` |
| 导入位置 | `LocalMachine\Root`、`LocalMachine\TrustedPublisher` |

证书本身不跟着仓库走，全部封在这个 exe 里；换证书时换 exe 即可，脚本和 iss 都不用改。

## 为什么只需要根

catalog 里已经带着中间证书，验签时从 catalog 内部取，所以系统里缺的只有链末端那个根。
而 catalog 决定的是「这个驱动包能不能被安装」，不是驱动运行时能不能加载，所以只影响安装
这一步。

## 想手动清理

```bat
"%ProgramFiles%\Pikacu Test CA Truster\unins000.exe" /VERYSILENT
```

或者直接删证书存储里的条目（产品名里 `Pikacu` 少一个 h，是安装器原作者写的，卸载路径得照抄）。

## 自己重签名

改过 `.sys` 之后需要重建 catalog 并重新签名，流程见
[FakeSign](https://github.com/PIKACHUIM/FakeSign)。仓库不附带任何签名证书或私钥。
