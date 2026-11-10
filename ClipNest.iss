#define MyAppName "ClipNest"
#define MyAppVersion "1.0.0"
#define MyAppPublisher "ClipNest contributors"
#define MyAppExeName "ClipNest.exe"

[Setup]
AppId={{5E7D5A61-6C63-4F42-9C9A-80B9AFD903A6}
AppName={#MyAppName}
AppVersion={#MyAppVersion}
AppPublisher={#MyAppPublisher}
DefaultDirName={localappdata}\Programs\ClipNest
DefaultGroupName={#MyAppName}
DisableProgramGroupPage=yes
PrivilegesRequired=lowest
ArchitecturesAllowed=x64compatible
ArchitecturesInstallIn64BitMode=x64compatible
OutputDir=..\ClipNest-Setup
OutputBaseFilename=ClipNest-Setup-{#MyAppVersion}-x64
SetupIconFile=Assets\ClipNest.ico
UninstallDisplayIcon={app}\{#MyAppExeName}
WizardStyle=modern
Compression=lzma2/ultra64
SolidCompression=yes
CloseApplications=yes
RestartApplications=no

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"

[Tasks]
Name: "desktopicon"; Description: "Create a desktop shortcut"; GroupDescription: "Additional shortcuts:"; Flags: unchecked

[Files]
Source: "..\ClipNest-win-x64\ClipNest.exe"; DestDir: "{app}"; Flags: ignoreversion

[Icons]
Name: "{autoprograms}\ClipNest"; Filename: "{app}\{#MyAppExeName}"; IconFilename: "{app}\{#MyAppExeName}"
Name: "{autodesktop}\ClipNest"; Filename: "{app}\{#MyAppExeName}"; IconFilename: "{app}\{#MyAppExeName}"; Tasks: desktopicon

[Run]
Filename: "{app}\{#MyAppExeName}"; Description: "Launch ClipNest"; Flags: postinstall nowait skipifsilent

[UninstallDelete]
; User history and preferences in %LOCALAPPDATA%\ClipNest are deliberately preserved.
