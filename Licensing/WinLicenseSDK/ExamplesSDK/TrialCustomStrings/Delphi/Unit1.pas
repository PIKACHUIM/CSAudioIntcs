unit Unit1;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, StdCtrls, WinLicenseSDK;

type
  TForm1 = class(TForm)
    Button1: TButton;
    Button2: TButton;
    procedure Button1Click(Sender: TObject);
    procedure Button2Click(Sender: TObject);
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

  // We are going to write our custom string

  WLTrialStringWrite('Our String', 'test string');

end;

procedure TForm1.Button2Click(Sender: TObject);
var
  ReadString: array [0..255] of AnsiChar;

begin

  // We are going to read our custom string

  WLTrialStringRead('Our String', ReadString);

  ShowMessage('Our String = ' + String(ReadString));

end;

end.
