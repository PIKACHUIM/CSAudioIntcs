unit Unit1;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, WinlicenseSDK, StdCtrls;

const
PasswordHash = '432ab78bd7ar324ghjd87feg63vksyb375am35fj50abloqnxc543jd73hrgf5289d';

type
    TForm1 = class(TForm)
    Button1: TButton;
    Label1: TLabel;
    Label2: TLabel;
    NameEdit: TEdit;
    PassEdit: TEdit;
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

procedure TForm1.Button1Click(Sender: TObject);

var

    PasswordBuffer: ARRAY[0..100] of AnsiChar;

begin

    WLGenPassword(PasswordHash, PAnsiChar(NameEdit.Text), PasswordBuffer);
    PassEdit.Text := PasswordBuffer;

end;

end.
