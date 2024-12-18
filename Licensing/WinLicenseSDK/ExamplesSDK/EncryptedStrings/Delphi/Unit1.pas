unit Unit1;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, StdCtrls, WinLicenseSDK;

type
  TForm1 = class(TForm)
    Button1: TButton;
    Button2: TButton;
    Button3: TButton;
    procedure Button1Click(Sender: TObject);
    procedure Button2Click(Sender: TObject);
    procedure Button3Click(Sender: TObject);
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

  // the string inside WLStringDecrypt is encrypted in protection time
  // it will be decrypted when WLStringDecrypt is called

  ShowMessage(WLStringDecrypt('This is an encrypted string 1!'));

end;

procedure TForm1.Button2Click(Sender: TObject);
begin

  // the string inside WLStringDecrypt is encrypted in protection time
  // it will be decrypted when WLStringDecrypt is called

  ShowMessage(WLStringDecrypt('This is an encrypted string 2!'));

end;

procedure TForm1.Button3Click(Sender: TObject);
begin

  // the string inside WLStringDecrypt is encrypted in protection time
  // it will be decrypted when WLStringDecrypt is called

  ShowMessage(WLStringDecrypt('This is an encrypted string 3!'));

end;

end.
