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

rem The bundled CA installer registers its own uninstaller. Drivers are already
rem gone at this point, so removing the trust is the last step.
if exist "%ProgramFiles(x86)%\Pikacu Test CA Truster\unins000.exe" (
    echo [remove ] signing roots
    "%ProgramFiles(x86)%\Pikacu Test CA Truster\unins000.exe" /VERYSILENT /SUPPRESSMSGBOXES /NORESTART
) else if exist "%ProgramFiles%\Pikacu Test CA Truster\unins000.exe" (
    echo [remove ] signing roots
    "%ProgramFiles%\Pikacu Test CA Truster\unins000.exe" /VERYSILENT /SUPPRESSMSGBOXES /NORESTART
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
