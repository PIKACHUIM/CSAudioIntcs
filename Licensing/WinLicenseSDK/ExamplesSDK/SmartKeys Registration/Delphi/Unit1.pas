unit Unit1;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, StdCtrls, WinlicenseSDK;

type
  TForm1 = class(TForm)
    Label1: TLabel;
    Label2: TLabel;
    Button1: TButton;
    GroupBox1: TGroupBox;
    Label4: TLabel;
    Label5: TLabel;
    Label6: TLabel;
    Label3: TLabel;
    NameEdit: TEdit;
    OrganizationEdit: TEdit;
    ExtraDataEdit: TEdit;
    ActivationCodeEdit: TEdit;
    procedure Button1Click(Sender: TObject);
    procedure FormCreate(Sender: TObject);
  private
    { Private declarations }
  public
    { Public declarations }
  end;

var
  Form1: TForm1;

implementation

{$R *.dfm}

procedure TForm1.Button1Click(Sender: TObject);
begin

    if WLRegSmartKeyCheck(PAnsiChar(AnsiString(NameEdit.Text)), PAnsiChar(AnsiString(OrganizationEdit.Text)), PAnsiChar(AnsiString(ExtraDataEdit.Text)), PAnsiChar(AnsiString(ActivationCodeEdit.Text))) = false  then

        MessageBox(0, 'Activation code is INVALID', 'Error', MB_OK or MB_ICONERROR)

    else
    begin

      // if activation is valid, insert the registration info in a file
      // So, winlicense will be able to finish the registration process

      WLRegSmartKeyInstallToFile(PAnsiChar(AnsiString(NameEdit.Text)), PAnsiChar(AnsiString(OrganizationEdit.Text)), PAnsiChar(AnsiString(ExtraDataEdit.Text)), PAnsiChar(AnsiString(ActivationCodeEdit.Text)));

      MessageBox(0, 'Activation code is correct. Please, restart this application to finish the registration process', 'Success', MB_OK);

    end;

end;

procedure TForm1.FormCreate(Sender: TObject);
var
   ExtStatus: Integer;
   
begin

   // check if application has been activated (registered)

    if WLRegGetStatus(ExtStatus) = 1 then
    begin

        Label1.Caption := 'This application has been ACTIVATED';
        Label2.Caption := '';
        Button1.Visible := False;
        GroupBox1.Visible := False;
    end;


end;

end.
