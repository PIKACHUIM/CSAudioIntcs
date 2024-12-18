unit Unit1;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, WinlicenseSDK, StdCtrls;

const
TrialHash = '432ab78bd7ar324ghjd87feg63vksyb375am35fj50abloqnxc543jd73hrgf5289dhf73hjdfjhgd783hjgsdmvbaismvhsm76354BfhBFHSgd7634';

type
    TForm1 = class(TForm)
    Button1: TButton;
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

    TrialKeyBuffer: ARRAY[0..100] of AnsiChar;
    SizeKey, i: integer;
    KeyFile: FILE OF AnsiChar;

begin

 // generate the extended key

 SizeKey := WLGenTrialExtensionFileKey(TrialHash, 1, 2, 5, NIL, 0, 0, TrialKeyBuffer);

 // save generated key in 'extendkey.dat' file

 AssignFile(KeyFile, 'extendkey.dat');
 
 Rewrite(KeyFile);

 for i:= 0 TO SizeKey DO
   Write(KeyFile, TrialKeyBuffer[i]);

 CloseFile(KeyFile);

end;

end.
