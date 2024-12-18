//---------------------------------------------------------------------------

#include <vcl.h>
#pragma hdrstop

#include "Unit1.h"
#include "WinlicenseSDK.h"
#pragma comment (lib,"WinlicenseSDK.lib")

//---------------------------------------------------------------------------
#pragma package(smart_init)
#pragma resource "*.dfm"
TForm1 *Form1;
//---------------------------------------------------------------------------
__fastcall TForm1::TForm1(TComponent* Owner)
        : TForm(Owner)
{
}
//---------------------------------------------------------------------------

void __fastcall TForm1::Button1Click(TObject *Sender)
{

  // the string inside WLStringDecrypt is encrypted in protection time
  // it will be decrypted when WLStringDecrypt is called

  ShowMessage(WLStringDecrypt("This is an encrypted string 1!"));

}
//---------------------------------------------------------------------------

void __fastcall TForm1::Button2Click(TObject *Sender)
{

  // the string inside WLStringDecrypt is encrypted in protection time
  // it will be decrypted when WLStringDecrypt is called

  ShowMessage(WLStringDecrypt("This is an encrypted string 2!"));
        
}
//---------------------------------------------------------------------------
void __fastcall TForm1::Button3Click(TObject *Sender)
{

  // the string inside WLStringDecrypt is encrypted in protection time
  // it will be decrypted when WLStringDecrypt is called

  ShowMessage(WLStringDecrypt("This is an encrypted string 3!"));

}
//---------------------------------------------------------------------------
