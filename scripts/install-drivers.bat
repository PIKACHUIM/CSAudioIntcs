@echo off
rem ---------------------------------------------------------------------------
rem  install-drivers.bat
rem  Installs every driver folder listed in ..\installer\payload.list via DPInst.
rem  Must run elevated. Silent by design (called from the Inno Setup installer
rem  or by hand from an admin console).
rem ---------------------------------------------------------------------------
setlocal
chcp 65001 >nul

set "ROOT=%~dp0.."
set "DPINST=%ROOT%\tools\dpinst\dpinst.exe"
set "LIST=%ROOT%\installer\payload.list"

if not exist "%DPINST%" (
    echo [ERROR] dpinst.exe not found: "%DPINST%"
    exit /b 1
)
if not exist "%LIST%" (
    echo [ERROR] payload.list not found: "%LIST%"
    exit /b 1
)

echo === installing driver payload ===

set /a FAILED=0
for /f "usebackq eol=# delims=" %%D in ("%LIST%") do (
    call :install_one "%%D"
)

echo === install pass finished (failures: %FAILED%) ===
exit /b 0

:install_one
set "REL=%~1"
if "%REL%"=="" exit /b 0
set "PATHREL=%REL:/=\%"
if not exist "%ROOT%\drivers\%PATHREL%\" (
    echo [skip   ] %REL%  ^(not present^)
    exit /b 0
)
echo [install] %REL%
"%DPINST%" /F /A /SW /PATH "%ROOT%\drivers\%PATHREL%"
if errorlevel 1 (
    echo [warn   ] %REL%  (dpinst exit %errorlevel%)
    set /a FAILED+=1
)
exit /b 0
