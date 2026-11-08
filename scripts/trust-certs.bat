@echo off
rem ---------------------------------------------------------------------------
rem  trust-certs.bat
rem
rem  Installs the CA the driver catalogs are signed against by running the
rem  bundled installer. Without it pnputil refuses every package with
rem      "publisher trust for the Authenticode-signed catalog has not been
rem       established"                                        (0xE0000242)
rem
rem  The installer drops the root into LocalMachine\Root and
rem  LocalMachine\TrustedPublisher, and registers itself for removal.
rem  Needs elevation.
rem ---------------------------------------------------------------------------
setlocal
chcp 65001 >nul

set "ROOT=%~dp0.."
set "CA=%ROOT%\certs\pikachu-certificate-install.exe"

if not exist "%CA%" (
    echo [ERROR] CA installer not found: "%CA%"
    exit /b 1
)

echo === installing signing roots ===
"%CA%" /VERYSILENT /SUPPRESSMSGBOXES /NORESTART
if errorlevel 1 (
    echo [warn   ] CA installer returned %errorlevel%
    exit /b 1
)
echo [done  ] signing roots installed

exit /b 0
