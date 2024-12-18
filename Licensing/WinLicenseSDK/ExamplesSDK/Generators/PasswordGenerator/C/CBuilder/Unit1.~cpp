//---------------------------------------------------------------------------

#include <vcl.h>
#pragma hdrstop

#include "Unit1.h"
#include <stdio.h>
#include "..\..\..\..\..\Include\C\WinlicenseSDK.h"
#pragma comment (lib,"..\\..\\..\\..\\..\\Lib\\OMF\\WinlicenseSDK.lib")


//---------------------------------------------------------------------------
#pragma package(smart_init)
#pragma resource "*.dfm"
TForm1 *Form1;
//---------------------------------------------------------------------------
__fastcall TForm1::TForm1(TComponent* Owner)
        : TForm(Owner)
{
    PasswordHash = "kd7dkcdsflkjdhd63urbcmchGFFJ6sJFDBXKLAIO673jdehf7HFTGdfndj73jfkalasdj8thfgh73k394746sdfjhds6364odfjkhdsf86346kjfhf3";
}
//---------------------------------------------------------------------------

void __fastcall TForm1::Button1Click(TObject *Sender)
{
    char    PasswordBuffer[100];
    char*   NameString = new char[NameEdit->Text.Length()+1];
    char    PassBuffer[100];

    strcpy(NameString, NameEdit->Text.c_str() );

    // create a password for the current user

    WLGenPassword(PasswordHash, NameString, PasswordBuffer);

    strcpy(PassBuffer, PasswordBuffer);
    
    PassEdit->Text = PassBuffer;

    delete NameString;
  
}
//---------------------------------------------------------------------------

