@echo off
rem ---------------------------------------------------------------------------
rem  uninstall-drivers.bat
rem  Removes every driver package listed in ..\installer\payload.list.
rem  Runs from the Inno Setup uninstaller or by hand from an admin console.
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

echo === removing driver payload ===

for /f "usebackq eol=# delims=" %%D in ("%LIST%") do (
    call :remove_one "%%D"
)

echo === uninstall pass finished ===
exit /b 0

:remove_one
set "REL=%~1"
if "%REL%"=="" exit /b 0
set "PATHREL=%REL:/=\%"
if not exist "%ROOT%\drivers\%PATHREL%\" exit /b 0

for %%I in ("%ROOT%\drivers\%PATHREL%\*.inf") do (
    echo [remove ] %REL%  %%~nxI
    "%DPINST%" /F /A /S /U "%%~fI"
)
exit /b 0
