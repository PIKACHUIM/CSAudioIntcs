# 版本差异

`drivers/` 里的包以能拿到的最新版本为准，但有几个落后上游。核对日期 2026-11-09。

| 包 | 当前 | 上游 | 没跟上的原因 |
|---|---|---|---|
| `csaudiointcsof` | 1.0.4.0（2024-04-23） | 1.0.5-1（2026-03） | 上游按机器授权下载。1.0.5 修了偶发无声和 HDMI 静音，1.0.5-1 又修了 Redrix 四扬声器映射和左右声道反相。安装器是反头 NSIS，7-Zip 解不开，只能跑一次让它自己释放 |
| `csaudiosstavs` | 1.0.4.0（2023-11-07） | 1.0.6（2025-05） | 同上。版本号取自安装包文件名，没有解包核对 |
| `crostouchpad` | 4.1（2017-08-03） | 4.1.6 | 安装包是反头 NSIS 加 WinLicense，7-Zip 19.00 / 26.04 都不认 |
| `crostouchscreen` | — | 2.9.5 | 同上 |
| `cr50` | — | 1.0.1 | 同上 |

Redrix（HP Elite Dragonfly Chromebook 这类 Alder Lake 机型）四扬声器只有两个出声、左右声道相反、低音单元不响，对上的就是 1.0.5-1 里那两条修复。1.0.4 上改不掉：映射既不在可读字符串里，也不在返回值那条路径上，补丁改的是授权校验。遇到这类反馈先让对方跑 [`scripts/collect-audio-info.ps1`](../scripts/collect-audio-info.ps1)，看 `ACPI\MX98390` 设备数（Redrix 应为 4 个）和 `CoolStar Speaker` 端点的声道数。

两个音频包的第三方镜像：

```
https://archive.org/download/sof_1.0.5.1/csaudiointcsof-1.0.5-1.zip
https://archive.org/download/csaudiosstavs_202505/csaudiosstavs.zip
```

两个 zip 里都带 `gui/GenLicense.exe` 和 `sof-intellicense.txt`，本仓库不分发这两样。

`crosecbus` 不算落后：仓库里是 2.1.0.0，比流传的 2.0.6 / 2.0.7 新。

## 故意没收的包

| 包 | 状态 |
|---|---|
| Intel Serial IO `iaLPSS2i_I2C_{SKL,BXT_P,GLK,CNL}`、`iaLPSS2i_GPIO2_*` | 缺了会直接影响功能，这是 codec、功放、触摸板挂的 I2C/GPIO 总线（SKL 包覆盖 `PCI\VEN_8086&DEV_9D60..9D65`，chell 的 `9D64` 就在里面）。手头只有 DriverStore 导出，没有 `.cat`，x64 上装不进去，需要原厂安装器 |
| `dptf_cpu` | 本地有带 catalog 的包（20 MB 策略 DLL），和仓库里的 `dptf_acpi` 配套。只管散热，不影响声音，为了让 payload 小一点没收 |
| Redrix 摄像头（`hi556`、`ov2740`、`ov5675_extension`、`iacamera64`） | 268 MB，绑定 Lenovo / Dell 特定主板。`drivers/camera/` 里留的是两个通用包 |
