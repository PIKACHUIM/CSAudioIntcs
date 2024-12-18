unit Unit1;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, StdCtrls, ComCtrls, SecureEnginePluginsSdk;

type
  TForm1 = class(TForm)
    Button1: TButton;
    labelStatus: TLabel;
    lbMessage: TLabel;
    procedure Button1Click(Sender: TObject);

  private
    { Private declarations }
  public
    { Public declarations }
  end;

var
  Form1: TForm1;

function SecureEngineInitialize(): Boolean; stdcall;
function SecureEngineFinalize(): Boolean; stdcall;
function SecureEngineShowCustomMessageA(MsgId: Integer; pMsg:PAnsiChar):Boolean; stdcall;
function SecureEngineProcessHardwareIdA(HardwareId:PAnsiChar):Boolean; stdcall;
function SecureEngineFirstRunTrialA(HardwareId:PAnsiChar; var TrialExpInfo: TTrialExpInfo): Boolean; stdcall;
function SecureEngineFirstRunRegisteredA(HardwareId:PAnsiChar; pRegName, pCompany, pCustomData: PAnsiChar;
                var RegExpInfo: TRegExpInfo): Boolean; stdcall;
function SecureEngineGetLicenseInfoA(pRegName, pCompany, pCustomData: PAnsiChar):Boolean; stdcall;
function SecureEngineDoRegistrationA(var WDCfunctions:TWDCfunctionsArray):Boolean; stdcall;
function SecureEngineGetEncryptionKey(ZoneId: Integer; pOutputEncryptionKey:PAnsiChar):Boolean; stdcall;

exports
  SecureEngineInitialize,
  SecureEngineShowCustomMessageA,
  SecureEngineProcessHardwareIdA,
  SecureEngineFirstRunTrialA,
  SecureEngineFirstRunRegisteredA,
  SecureEngineGetLicenseInfoA,
  SecureEngineDoRegistrationA,
  SecureEngineGetEncryptionKey,
  SecureEngineFinalize;

implementation

uses Unit2;

{$R *.dfm}


function SecureEngineInitialize(): Boolean;
begin

  Form1 := TForm1.Create(application);
  Form1.LabelStatus.Caption := 'SecureEngineInitialize';

  Form1.labelStatus.Width := Form1.Width;

  Form1.ShowModal;
  Result := True;

end;

function SecureEngineFinalize(): Boolean;
begin
  Form1 := TForm1.Create(application);
  Form1.LabelStatus.Caption := 'SecureEngineFinalize';

  Form1.labelStatus.Width := Form1.Width;
  Form1.ShowModal;
  Result := True;
end;

function SecureEngineShowCustomMessageA(MsgId: Integer; pMsg:PAnsiChar):Boolean;
begin

  Form1 := TForm1.Create(application);
  Form1.LabelStatus.Caption := 'Calling Message (A): ' + IntToStr(MsgId);

  if pMsg <> nil then
    Form1.lbMessage.Caption := 'Original Message: ' + AnsiString(pMsg);

  Form1.labelStatus.Width := Form1.Width;
  Form1.ShowModal;

  Result := True;

end;

function SecureEngineProcessHardwareIdA(HardwareId:PAnsiChar):Boolean;
var
  NewHardwareId: String;
begin
  Result := False;  // Default: We are not changing the Hardware ID

  if MessageDlg('Do you want to change the Hardware ID (' + HardwareId + ')',
               mtConfirmation, [mbYes, mbNo], 0) = mrYes then
  begin
    NewHardwareId := InputBox('Enter new Hardware ID', '', HardwareId);
    StrPLCopy(HardwareId, NewHardwareId, Length(NewHardwareId));
    Result := True;   // Return that hardware ID has been changed
  end;
end;     

function SecureEngineFirstRunTrialA(HardwareId:PAnsiChar; var TrialExpInfo: TTrialExpInfo): Boolean; stdcall;
begin
  if MessageDlg('SecureEngineFirstRunTrial called' + #13#10 +
             'Hardware ID = ' + HardwareId + #13#10 +
             'Days Left = ' + IntToStr(TrialExpInfo.DaysLeft) + #13#10 +
             'Executions Left = ' + IntToStr(TrialExpInfo.ExecutionsLeft) + #13#10 + #13#10 +
             'Continue execution?', mtConfirmation, [mbYes, mbNo], 0) = mrYes then
    Result := True
  else
    Result := False;
end;

function SecureEngineFirstRunRegisteredA(HardwareId:PAnsiChar; pRegName, pCompany, pCustomData: PAnsiChar;
                var RegExpInfo: TRegExpInfo): Boolean; stdcall;
begin
  if MessageDlg('SecureEngineFirstRunRegistered called' + #13#10 +
             'Hardware ID = ' + HardwareId + #13#10 +
             'User Name = ' + pRegName + #13#10 +
             'Company Name = ' + pCompany + #13#10 +
             'Custom Data = ' + pCustomData + #13#10 +
             'Days Left = ' + IntToStr(RegExpInfo.DaysLeft) + #13#10 +
             'Executions Left = ' + IntToStr(RegExpInfo.ExecutionsLeft) + #13#10 + #13#10 +
             'Continue execution?', mtConfirmation, [mbYes, mbNo], 0) = mrYes then
    Result := True
  else
    Result := False;
end;

function SecureEngineGetLicenseInfoA(pRegName, pCompany, pCustomData: PAnsiChar):Boolean; stdcall;
begin
  MessageDlg('SecureEngineGetLicenseInfoA called' + #13#10 +
             'User Name = ' + pRegName + #13#10 +
             'Company Name = ' + pCompany + #13#10 +
             'Custom Data = ' + pCustomData, mtConfirmation, [mbOK], 0);
  Result := True;
end;

function SecureEngineGetApplicationStatus(AppStatus:Integer; ExtendeAppStatus:Integer;
                                   var TrialExpInfo: TTrialExpInfo; var RegExpInfo: TRegExpInfo):Boolean; stdcall;
begin
  // display Application Status
  if AppStatus = wdcRegistered then
    Form1.Caption := 'Registered'
  else
    Form1.Caption := 'Trial';

  // Display Extended Information

  case ExtendeAppStatus of

    wdcNoError: Form1.LabelStatus.Caption := 'No Error';

    // license related cases

    wdcInvalidLicense: Form1.LabelStatus.Caption := 'Invalid License Key';
    wdcInvalidHardwareLicense: Form1.LabelStatus.Caption := 'Invalid Machine ID';
    wdcNoMoreHwdChanges: Form1.LabelStatus.Caption := 'No more hardware changes allowed';
    wdcLicenseExpired: Form1.LabelStatus.Caption := 'License key expired';
    wdcInvalidCountryLicense: Form1.LabelStatus.Caption := 'License locked to different country';
    wdcLicenseStolen: Form1.LabelStatus.Caption := 'Stolen license key';
    wdcWrongLicenseExp: Form1.LabelStatus.Caption := 'License has not expiration info. Only licenses with expiration info allowed';
    wdcWrongLicenseHardware: Form1.LabelStatus.Caption := 'License not locked to any machine. Only license with hardware locking allowed';

    // trial related cases

    wdcTrialExpired: Form1.LabelStatus.Caption := 'Trial expired';
    wdcTrialManipulated: Form1.LabelStatus.Caption := 'Trial Manipulated';

  end;

  Result := True;
end;

function SecureEngineDoRegistrationA(var WDCfunctions:TWDCfunctionsArray):Boolean;
begin

  Form2 := TForm2.Create(application);
  Form2.ArrayFunctions := WDCfunctions;
  Form2.ShowModal;
  Result := True;

end;

function SecureEngineGetEncryptionKey(ZoneId: Integer; pOutputEncryptionKey:PAnsiChar):Boolean;
begin
    StrCopy(pOutputEncryptionKey, 'My Encryption Key');
    Result := True;
end;

procedure TForm1.Button1Click(Sender: TObject);
begin
  Close;
end;

end.
