unit Unit1;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, StdCtrls, ComCtrls, idHTTP;

const
  EVENT_SHOW_ACTIVATION                             = 1;
  EVENT_CHECK_RESPONSE                              = 2;
  EVENT_SHOW_DEACTIVATION                           = 3;

  RESPONSE_ACTIVATION_OK                            = 0;
  RESPONSE_DEACTIVATION_OK                          = 0;
  RESPONSE_ERROR_KEY_NOT_FOUND                      = 1;
  RESPONSE_ERROR_MAX_DEVICES_REACHED                = 2;
  RESPONSE_ERROR_NO_MORE_ACTIVATIONS_ALLOWED        = 3;
  RESPONSE_ERROR_NO_MORE_DEACTIVATIONS_ALLOWED      = 4;
  RESPONSE_ERROR_DEVICE_NOT_FOUND                   = 5;
  RESPONSE_ERROR_WRONG_DATA_RECEIVED                = 6;
  RESPONSE_ERROR_KEY_DISABLED_BY_SELLER             = 7;
  RESPONSE_ERROR_KEY_EXPIRED                        = 8;
  RESPONSE_ERROR_NO_MORE_DIFFERENT_DEVICES_ALLOWED  = 9;
  RESPONSE_ERROR_CANNOT_INSTALL_LICENSE             = 50;
  RESPONSE_ERROR_WINSOCK_ERROR                      = 100;

  FORM_RESPONSE_PROCESS_ACTIVATION_KEY              = 0;
  FORM_RESPONSE_TERMINATE_APPLICATION               = 1;
  FORM_RESPONSE_RESTART_APPLICATION                 = 2;
  FORM_RESPONSE_SHOW_ACTIVATION_FORM_AGAIN          = 3;

type
  TForm1 = class(TForm)
    btnActivate: TButton;
    lbTitle: TLabel;
    lbLine1: TLabel;
    lbLine2: TLabel;
    Label4: TLabel;
    edActivationKey: TEdit;
    Label5: TLabel;
    procedure btnActivateClick(Sender: TObject);

  private
    { Private declarations }
  public
    { Public declarations }
  end;

var
  Form1: TForm1;
  UserActivationKey: String;

function ActivationHandler(EventId: Integer; ActivationErrorCode: Integer;
                WinsockErrorCode: Integer; ServerResponseString: PAnsiChar;
                pActivationKey:PAnsiChar):Integer; stdcall;

exports
  ActivationHandler;

implementation

{$R *.dfm}
                                                   
// ---------------------------------------------------------------
// ActivationHandler
//   Our main activation handler called by WinLicense
// ---------------------------------------------------------------

function ActivationHandler(EventId: Integer; ActivationErrorCode: Integer;
                WinsockErrorCode: Integer; ServerResponseString: PAnsiChar;
                pActivationKey:PAnsiChar):Integer;
begin
  // Display activation form in case of "EVENT_SHOW_ACTIVATION"
  if (EventId = EVENT_SHOW_ACTIVATION) or (EventId = EVENT_SHOW_DEACTIVATION) then
  begin
    UserActivationKey := '';

    // display our activation form
    Form1 := TForm1.Create(application);

    // change form text in case of deactivation
    if EventId = EVENT_SHOW_DEACTIVATION then
    begin
      Form1.Caption := 'Deactivation';
      Form1.lbTitle.Caption := 'Deactivate Software';
      Form1.lbLine1.Caption := 'This application is going to be deactivated on this computer.';
      Form1.lbLine2.Caption := 'Please, insert your Activation key to deactivate this application.';
    end;

    Form1.ShowModal;

    if UserActivationKey <> '' then
    begin
      // copy Activation Key entered by user
      StrCopy(pActivationKey, PAnsiChar(UserActivationKey));
      Result := FORM_RESPONSE_PROCESS_ACTIVATION_KEY;
    end
    else
      Result := FORM_RESPONSE_TERMINATE_APPLICATION;

  end
  // Check response code and display message to user
  else if EventId = EVENT_CHECK_RESPONSE then
  begin
    if ActivationErrorCode = RESPONSE_ACTIVATION_OK then
    begin
      ShowMessage('Application successfully activated!');
      Result := FORM_RESPONSE_RESTART_APPLICATION; 
    end
    else
    begin
      if ActivationErrorCode = RESPONSE_ERROR_KEY_NOT_FOUND then
        ShowMessage('RESPONSE_ERROR_KEY_NOT_FOUND')

      else if ActivationErrorCode = RESPONSE_ERROR_MAX_DEVICES_REACHED then
        ShowMessage('RESPONSE_ERROR_MAX_DEVICES_REACHED')

      else if ActivationErrorCode = RESPONSE_ERROR_NO_MORE_ACTIVATIONS_ALLOWED then
        ShowMessage('RESPONSE_ERROR_NO_MORE_ACTIVATIONS_ALLOWED')

      else if ActivationErrorCode = RESPONSE_ERROR_NO_MORE_DEACTIVATIONS_ALLOWED then
        ShowMessage('RESPONSE_ERROR_NO_MORE_DEACTIVATIONS_ALLOWED')

      else if ActivationErrorCode = RESPONSE_ERROR_WRONG_DATA_RECEIVED then
        ShowMessage('RESPONSE_ERROR_WRONG_DATA_RECEIVED')

      else if ActivationErrorCode = RESPONSE_ERROR_KEY_DISABLED_BY_SELLER then
        ShowMessage('RESPONSE_ERROR_KEY_DISABLED_BY_SELLER')

      else if ActivationErrorCode = RESPONSE_ERROR_KEY_EXPIRED then
        ShowMessage('RESPONSE_ERROR_KEY_EXPIRED')
                              
      else if ActivationErrorCode = RESPONSE_ERROR_WINSOCK_ERROR then
        ShowMessage('Winsock Error: ' + IntToStr(WinsockErrorCode))

      else if ActivationErrorCode = RESPONSE_ERROR_CANNOT_INSTALL_LICENSE then
        ShowMessage('Cannot write license to disk. File or Directory write protected')

      else
        ShowMessage('Unknown error: ' + IntToStr(ActivationErrorCode));

      // displayed detailed information returned by the activation server
      if ActivationErrorCode <> RESPONSE_ERROR_WINSOCK_ERROR then
        ShowMessage('Server Response: ' + #13#10 + ServerResponseString);

      // ask again the user to re-enter the activation key
      Result := FORM_RESPONSE_SHOW_ACTIVATION_FORM_AGAIN;
    end;
  end;
end;

// ---------------------------------------------------------------
// btnActivateClick
// ---------------------------------------------------------------

procedure TForm1.btnActivateClick(Sender: TObject);
begin
  UserActivationKey := edActivationKey.Text;       
  Close;
end;

end.
