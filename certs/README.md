# Signing roots

The driver catalogs in `drivers/` chain to a test root CA, not to a publicly trusted
root. Windows refuses to add such a package until the root is trusted:

```
pnputil /add-driver ssm4567.inf /install
无法添加驱动程序包: 尚未建立对 Authenticode(TM)签名的目录的发布者信任。
exit = 0xE0000242
```

`scripts\trust-certs.bat` imports every `*.cer` in this folder into

* `LocalMachine\Root` — chain building
* `LocalMachine\TrustedPublisher` — the extra check setupapi performs on driver packages

and the installer runs it before touching any driver.

## Doing it by hand

```bat
certutil -addstore -f Root             certs\pikachu-testca-root.cer
certutil -addstore -f TrustedPublisher certs\pikachu-testca-root.cer
```

## What is here

| file | subject | valid until |
|---|---|---|
| `pikachu-testca-root.cer` | `CN=Pikachu Test CA RSA, O=Pikachu Trust Network CA, C=CN` | 2100-01-01 |

Only the root is needed. The intermediate issued by it
(`CN=Pikachu Time Sub CA`) travels inside the `.cat` files themselves, which is where a
catalog verification looks for it — that is also why the timestamp on the package stays
valid even though the code-signing certificate it was signed with has expired.

## Removing it again

```bat
certutil -delstore Root             "Pikachu Test CA RSA"
certutil -delstore TrustedPublisher "Pikachu Test CA RSA"
```

Installed drivers keep working; only re-installation needs the root again.
