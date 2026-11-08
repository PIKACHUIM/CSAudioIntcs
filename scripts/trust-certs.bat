@echo off
rem ---------------------------------------------------------------------------
rem  trust-certs.bat
rem  Installs the test-root CA that the audio driver catalogs are signed
rem  against. Without it pnputil refuses the package with
rem      "publisher trust for the Authenticode-signed catalog has not been
rem       established"  (0xE0000242)
rem  Must run elevated.
rem ---------------------------------------------------------------------------
setlocal
chcp 65001 >nul

set "ROOT=%~dp0.."
set /a FAILED=0

echo === trusting signing roots ===
for %%C in ("%ROOT%\certs\*.cer") do (
    echo [trust  ] %%~nxC
    certutil -addstore -f Root "%%~fC" >nul 2>&1
    if errorlevel 1 (
        echo [warn   ] %%~nxC  (root store failed)
        set /a FAILED+=1
    )
    rem kernel-mode driver packages are additionally checked against the
    rem "Trusted Publishers" store, so put the same cert there as well
    certutil -addstore -f TrustedPublisher "%%~fC" >nul 2>&1
)

echo === cert pass finished (failures: %FAILED%) ===
exit /b 0
