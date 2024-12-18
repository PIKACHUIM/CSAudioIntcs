#define MyAppName "ChromeBook CoolStar Audio Drivers"
#define MyAppVersion "1.0.4.0"
#define MyAppPublisher "CoolStar"
#define MyAppURL "https://github.com/coolstar"

[Setup]
; 注: AppId的值为单独标识该应用程序。
AppId={{DAC7DC88-B76D-49E0-8BED-FB4CE5424E7C}
AppName={#MyAppName}
AppVersion={#MyAppVersion}
;AppVerName={#MyAppName} {#MyAppVersion}
AppPublisher={#MyAppPublisher}
AppPublisherURL={#MyAppURL}
AppSupportURL={#MyAppURL}
AppUpdatesURL={#MyAppURL}
DefaultDirName={autopf}\{#MyAppName}
DefaultGroupName={#MyAppName}
AllowNoIcons=yes
LicenseFile=.\License.rtf
; 以下行取消注释，以在非管理安装模式下运行（仅为当前用户安装）。
;PrivilegesRequired=lowest
PrivilegesRequiredOverridesAllowed=commandline
OutputDir=.\Release
OutputBaseFilename=ChromeBook-CoolStar-Audio-Drivers
Compression=zip
SolidCompression=yes
WizardStyle=modern
WizardSizePercent=150
ArchitecturesInstallIn64BitMode=x64

[Languages]
Name: "chinese"; MessagesFile: "compiler:Languages\ChineseSimplified.isl"
Name: "english"; MessagesFile: "compiler:Default.isl"

[Files]
; 通用-驱动文件
Source: "Drivers\*"; DestDir: "{app}\Drivers\"; Flags: ignoreversion recursesubdirs createallsubdirs

[Icons]
Name: "{group}\{cm:ProgramOnTheWeb,{#MyAppName}}"; Filename: "{#MyAppURL}"
Name: "{group}\{cm:UninstallProgram,{#MyAppName}}"; Filename: "{uninstallexe}"

[run]
Filename: "{app}\Drivers\drivers-removecerts-install.bat";Description:"安装驱动文件中...";StatusMsg:"安装CoolStar Audio Driver中，请耐心等待......";Flags: waituntilterminated runhidden


[UninstallRun]
Filename: "{app}\Drivers\drivers-removecerts-removed.bat";Flags: waituntilterminated runhidden;RunOnceId: "DelService"