unit Unit2;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, StdCtrls, SecureEnginePluginsSdk;

type
  TForm2 = class(TForm)
    GroupBox1: TGroupBox;
    Label1: TLabel;
    btCheckText: TButton;
    btInstallTextToFile: TButton;
    btRestartApplication: TButton;
    edTextKey: TEdit;
    GroupBox2: TGroupBox;
    Label2: TLabel;
    Label3: TLabel;
    Label4: TLabel;
    Label5: TLabel;
    btCheckSmart: TButton;
    btInstallSmartToFile: TButton;
    btRestartApplication2: TButton;
    edSmartKey: TEdit;
    edUserName: TEdit;
    edCompany: TEdit;
    edCustom: TEdit;
    Button1: TButton;
    procedure btInstallSmartToFileClick(Sender: TObject);
    procedure btCheckSmartClick(Sender: TObject);
    procedure btInstallTextToFileClick(Sender: TObject);
    procedure btCheckTextClick(Sender: TObject);
    procedure Button1Click(Sender: TObject);
    procedure btRestartApplicationClick(Sender: TObject);
  private
    { Private declarations }
  public
    { Public declarations }

    ArrayFunctions: TWDCfunctionsArray;
  end;

var
  Form2: TForm2;

implementation

{$R *.dfm}

procedure TForm2.btInstallSmartToFileClick(Sender: TObject);
var
  pName, pCompany, pCustom:PAnsiChar;

begin

  // set parameters for WLRegSmartKeyInstallToFile

  if edUserName.Text <> '' then
    pName := PAnsiChar(AnsiString(edUserName.Text))
  else
    pName := nil;

  if edCompany.Text <> '' then
    pCompany := PAnsiChar(AnsiString(edCompany.Text))
  else
    pCompany := nil;

  if edCustom.Text <> '' then
    pCustom := PAnsiChar(AnsiString(edCustom.Text))
  else
    pCustom := nil;

  if ArrayFunctions.pfWLRegSmartKeyInstallToFile(pName, pCompany, pCustom,
                PAnsiChar(AnsiString(edSmartKey.Text))) then
    ShowMessage('Key successfully installed. You need to restart the application')
  else
    ShowMessage('Error while installing the key');

end;

procedure TForm2.btCheckSmartClick(Sender: TObject);
var
  pName, pCompany, pCustom:PAnsiChar;

begin

  // set parameters for WLRegSmartKeyCheck

  if edUserName.Text <> '' then
    pName := PAnsiChar(AnsiString(edUserName.Text))
  else
    pName := nil;

  if edCompany.Text <> '' then
    pCompany := PAnsiChar(AnsiString(edCompany.Text))
  else
    pCompany := nil;

  if edCustom.Text <> '' then
    pCustom := PAnsiChar(AnsiString(edCustom.Text))
  else
    pCustom := nil;

  if ArrayFunctions.pfWLRegSmartKeyCheck(pName, pCompany, pCustom,
                      PAnsiChar(AnsiString(edSmartKey.Text))) then
    ShowMessage('SmartKey is correct')
  else
    ShowMessage('INVALID Smart key!');

end;

procedure TForm2.btInstallTextToFileClick(Sender: TObject);
begin

  if ArrayFunctions.pfWLRegNormalKeyInstallToFile(PAnsiChar(AnsiString(edTextKey.Text))) then
    ShowMessage('Key successfully installed. You need to restart the application')
  else
    ShowMessage('Error while installing the key');

end;

procedure TForm2.btCheckTextClick(Sender: TObject);
begin

  if ArrayFunctions.pfWLRegNormalKeyCheck(PAnsiChar(AnsiString(edTextKey.Text))) then
    ShowMessage('Text key is correct')
  else
    ShowMessage('INVALID text key!');

end;

procedure TForm2.Button1Click(Sender: TObject);
begin
  Close;
end;

procedure TForm2.btRestartApplicationClick(Sender: TObject);
begin
  ArrayFunctions.pfWLRestartApplication();
end;

end.
