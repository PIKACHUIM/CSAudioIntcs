unit Unit1;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, StdCtrls, ComCtrls, WinlicenseSDK;

type
  TForm1 = class(TForm)
    GroupBox1: TGroupBox;
    StatusLabel: TLabel;
    Label6: TLabel;
    Button5: TButton;
    GroupBox2: TGroupBox;
    DaysLeftLabel: TLabel;
    Label1: TLabel;
    Label8: TLabel;
    ExecLeftLabel: TLabel;
    Label13: TLabel;
    Label15: TLabel;
    MinutesLeftLabel: TLabel;
    Label17: TLabel;
    RuntimeLeftLabel: TLabel;
    ExpDatePicker: TDateTimePicker;
    GroupBox3: TGroupBox;
    RegDaysLeftLabel: TLabel;
    Label9: TLabel;
    Label10: TLabel;
    RegExecLeftLabel: TLabel;
    Label19: TLabel;
    Label2: TLabel;
    Label4: TLabel;
    Label5: TLabel;
    RegNameEdit: TEdit;
    CompanyNameEdit: TEdit;
    CustomEdit: TMemo;
    RegDateTimePicker1: TDateTimePicker;
    GroupBox5: TGroupBox;
    btnActivate: TButton;
    ActivationEdit: TEdit;
    DeactivationEdit: TEdit;
    Button1: TButton;
    procedure Button5Click(Sender: TObject);
    procedure btnActivateClick(Sender: TObject);
    procedure Button1Click(Sender: TObject);
  private
    { Private declarations }
  public
    { Public declarations }
  end;

var
  Form1: TForm1;

implementation

{$R *.dfm}

procedure TForm1.Button5Click(Sender: TObject);
var
  Status, ExtStatus:integer;
  RegName: ARRAY [0..255] of AnsiChar;
  CompanyName: ARRAY [0..255] of AnsiChar;
  CustomData: ARRAY [0..255] of AnsiChar;
  TrialDate: SYSTEMTIME;

begin
    // Reset TrialDate

   TrialDate.wYear := 0;
   TrialDate.wMonth := 0;
   TrialDate.wDay := 0;
   TrialDate.wSecond := 0;
   TrialDate.wDayOfWeek := 0;
   TrialDate.wHour := 0;
   TrialDate.wMinute := 0;
   TrialDate.wSecond := 0;
   TrialDate.wMilliseconds := 0;
          
   // Get application status              

   Status := WLRegGetStatus(ExtStatus);

   case Status of

      0: StatusLabel.Caption := 'Trial';
      1: StatusLabel.Caption := 'Registered';
      2: StatusLabel.Caption := 'Invalid License';
      3: StatusLabel.Caption := 'License Locked to different machine';
      4: StatusLabel.Caption := 'No more HW-ID changes allowed';
      5: StatusLabel.Caption := 'License Key expired';

   end;

   // show information about trial if application is not registered

   if Status <> 1 then
   begin

        // set trial labels data

        DaysLeftLabel.Caption := IntToStr(WLTrialDaysLeft());
        ExecLeftLabel.Caption := IntToStr(WLTrialExecutionsLeft());
        MinutesLeftLabel.Caption := IntToStr(WLTrialGlobalTimeLeft());
        RuntimeLeftLabel.Caption := IntToStr(WLTrialRuntimeLeft());

        WLTrialExpirationDate(TrialDate);
        ExpDatePicker.Date := SystemTimeToDateTime(TrialDate);

        // set registration label data as empty

        RegNameEdit.Text := '';
        CompanyNameEdit.Text := '';
        CustomEdit.Text := '';
        RegDaysLeftLabel.Caption := '';
        RegExecLeftLabel.Caption := '';

   end
   else
   begin

        // set license labels data

        WLRegGetLicenseInfo(RegName, CompanyName, CustomData);
        RegNameEdit.Text := RegName;
        CompanyNameEdit.Text := CompanyName;
        CustomEdit.Text := CustomData;
        RegDaysLeftLabel.Caption := IntToStr(WLRegDaysLeft());
        RegExecLeftLabel.Caption := IntToStr(WLRegExecutionsLeft());

        WLRegExpirationDate(TrialDate);
        RegDateTimePicker1.Date := SystemTimeToDateTime(TrialDate);

        // set registration label data as empty

        DaysLeftLabel.Caption := '';
        ExecLeftLabel.Caption := '';
        MinutesLeftLabel.Caption := '';
        RuntimeLeftLabel.Caption := '';

   end;

end;

procedure TForm1.btnActivateClick(Sender: TObject);
const
  SIZE_SERVER_OUTPUT = $10000;

var
  ActivationErrorCode: Integer;
  WinSockErrorCode: Integer;
  ServerOutput: array of AnsiChar;

begin
  SetLength(ServerOutput, SIZE_SERVER_OUTPUT);
  ActivationErrorCode := WLRegActivateSoftware(PAnsiChar(AnsiString(ActivationEdit.Text)), 
                                WinSockErrorCode, PAnsiChar(ServerOutput), SIZE_SERVER_OUTPUT);

  if (ActivationErrorCode = wlActivationOk) then
  begin
    ShowMessage('Application activated successfully.' + #13#10 + 'Application will restart now');
    WLRestartApplication();
  end
  else
  begin
    ShowMessage('There was an error activating your software. Error code = ' + IntToStr(ActivationErrorCode));
    ShowMessage('Server output: ' + PAnsiChar(ServerOutput));
  end;

  SetLength(ServerOutput, 0);
end;

procedure TForm1.Button1Click(Sender: TObject);
const
  SIZE_SERVER_OUTPUT = $10000;

var
  DeactivationErrorCode: Integer;
  WinSockErrorCode: Integer;
  ServerOutput: array of AnsiChar;

begin
  SetLength(ServerOutput, SIZE_SERVER_OUTPUT);
  DeactivationErrorCode := WLRegDeactivateSoftware(PAnsiChar(AnsiString(DeactivationEdit.Text)), 
                                WinSockErrorCode, PAnsiChar(ServerOutput), SIZE_SERVER_OUTPUT);

  if (DeactivationErrorCode = wlActivationOk) then
  begin
    ShowMessage('Application *deactivated*!' + #13#10 + 'Application will restart now');
    WLRestartApplication();
  end
  else
  begin
    ShowMessage('There was an error deactivating your software. Error code = ' + IntToStr(DeactivationErrorCode));
    ShowMessage('Server output: ' + PAnsiChar(ServerOutput));
  end;

  SetLength(ServerOutput, 0);
end;

end.
