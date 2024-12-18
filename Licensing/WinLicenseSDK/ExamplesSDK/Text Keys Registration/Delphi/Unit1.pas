unit Unit1;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, StdCtrls, WinlicenseSDK, ExtCtrls;

type
  TForm1 = class(TForm)
    Label1: TLabel;
    Label2: TLabel;
    Button1: TButton;
    GroupBox1: TGroupBox;
    TextKeyEdit: TMemo;
    Panel1: TPanel;
    Label3: TLabel;
    GroupBox2: TGroupBox;
    Label4: TLabel;
    Label5: TLabel;
    Label6: TLabel;
    NameEdit: TEdit;
    CompanyEdit: TEdit;
    ExtradataEdit: TEdit;
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

    if WLRegNormalKeyCheck(PAnsiChar(AnsiString(TextKeyEdit.Text))) = false  then

        MessageBox(0, 'The key that you entered is invalid. Please, try it again', 'Error', MB_OK or MB_ICONERROR)

    else
    begin

      // if activation is valid, insert the registration info in a file
      // So, winlicense will be able to finish the registration process

      WLRegNormalKeyInstallToFile(PAnsiChar(AnsiString(TextKeyEdit.Text)));

      MessageBox(0, 'Registration key is correct. Please, restart this application to finish the registration process', 'Success', MB_OK);

    end;

end;

procedure TForm1.FormCreate(Sender: TObject);

var

  Name : ARRAY[0..200] of AnsiChar;
  Company : ARRAY[0..200] of AnsiChar;
  ExtraData : ARRAY[0..200] of AnsiChar;
  ExtStatus: Integer;

begin

   // check if application has been activated (registered)

    if WLRegGetStatus(ExtStatus) = 1 then
    begin

        Panel1.Visible := True;
        WLRegGetLicenseInfo(Name, Company, ExtraData);
        NameEdit.Text := Name;
        CompanyEdit.Text := Company;
        ExtradataEdit.Text := ExtraData;
        
    end;
     
end;

end.
