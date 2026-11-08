; ---------------------------------------------------------------------------
;  ChromeBook CoolStar Audio Drivers -- Inno Setup script
;
;  Build:
;     ISCC.exe installer\Install.iss
;     ISCC.exe /DMyAppVersion=1.0.5 installer\Install.iss
;
;  The version comes from the command line so that CI can drive it
;  (see .github/workflows/release.yml).
; ---------------------------------------------------------------------------

#ifndef MyAppVersion
  #define MyAppVersion "1.0.4"
#endif

#define MyAppName        "ChromeBook CoolStar Audio Drivers"
#define MyAppShortName   "ChromeBook-CoolStar-Audio-Driver"
#define MyAppPublisher   "CoolStar"
#define MyAppURL         "https://github.com/PIKACHUIM/CSAudioIntcs"

[Setup]
AppId={{DAC7DC88-B76D-49E0-8BED-FB4CE5424E7C}
AppName={#MyAppName}
AppVersion={#MyAppVersion}
AppVerName={#MyAppName} {#MyAppVersion}
AppPublisher={#MyAppPublisher}
AppPublisherURL={#MyAppURL}
AppSupportURL={#MyAppURL}
AppUpdatesURL={#MyAppURL}/releases
DefaultDirName={autopf}\{#MyAppName}
DefaultGroupName={#MyAppName}
AllowNoIcons=yes
LicenseFile=License.rtf
SetupIconFile=dpinsts.ico
PrivilegesRequired=admin
PrivilegesRequiredOverridesAllowed=commandline
OutputDir=..\dist
OutputBaseFilename={#MyAppShortName}-{#MyAppVersion}-x64
Compression=lzma2/max
SolidCompression=yes
WizardStyle=modern
WizardSizePercent=150
ArchitecturesAllowed=x64
ArchitecturesInstallIn64BitMode=x64
ShowLanguageDialog=auto

[Languages]
Name: "chinese"; MessagesFile: "compiler:Languages\ChineseSimplified.isl"
Name: "english"; MessagesFile: "compiler:Default.isl"

[Files]
; driver payload -- the only thing that actually matters
Source: "..\drivers\*";       DestDir: "{app}\drivers";      Flags: ignoreversion recursesubdirs createallsubdirs
; install order manifest (both install and uninstall scripts read it)
Source: "..\installer\payload.list"; DestDir: "{app}\installer"; Flags: ignoreversion
; DPInst (Microsoft Driver Package Installer)
Source: "..\tools\dpinst\*";  DestDir: "{app}\tools\dpinst"; Flags: ignoreversion
; signing roots the catalogs chain to
Source: "..\certs\*.cer";     DestDir: "{app}\certs";        Flags: ignoreversion
; install / uninstall / cert helpers
Source: "..\scripts\*.bat";   DestDir: "{app}\scripts";      Flags: ignoreversion
; documentation
Source: "..\README.md";       DestDir: "{app}";              Flags: ignoreversion isreadme
Source: "..\docs\DRIVERS.md"; DestDir: "{app}\docs";         Flags: ignoreversion

[Icons]
Name: "{group}\{cm:UninstallProgram,{#MyAppName}}"; Filename: "{uninstallexe}"
Name: "{group}\Driver folder";                      Filename: "{app}\drivers"
Name: "{group}\Driver list (DRIVERS.md)";           Filename: "{app}\docs\DRIVERS.md"

[Run]
Filename: "{app}\scripts\trust-certs.bat";   StatusMsg: "Installing signing roots..."; Flags: waituntilterminated runhidden
Filename: "{app}\scripts\install-drivers.bat"; StatusMsg: "Installing drivers, please wait..."; Flags: waituntilterminated runhidden

[UninstallRun]
Filename: "{app}\scripts\uninstall-drivers.bat"; Flags: waituntilterminated runhidden; RunOnceId: "RemoveDrivers"

[Code]
function InitializeSetup(): Boolean;
begin
  Result := True;
end;
