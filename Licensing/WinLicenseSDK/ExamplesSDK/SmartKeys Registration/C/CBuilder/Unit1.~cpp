//---------------------------------------------------------------------------

#include <vcl.h>
#pragma hdrstop

#include "Unit1.h"
#include "..\..\..\..\Include\C\WinlicenseSDK.h"
#pragma comment (lib,"..\\..\\..\\..\\Lib\\OMF\\WinlicenseSDK.lib")

//---------------------------------------------------------------------------
#pragma package(smart_init)
#pragma resource "*.dfm"
TForm1 *Form1;
//---------------------------------------------------------------------------
__fastcall TForm1::TForm1(TComponent* Owner)
    : TForm(Owner)
{

    // check if application has been activated (registered)

    if (WLRegGetStatus(NULL) == 1)
    {
        Label1->Caption = "This application has been ACTIVATED";
        Label2->Caption = "";
        Button1->Visible = False;
        GroupBox1->Visible = False;
    }

}
//---------------------------------------------------------------------------

void __fastcall TForm1::Button1Click(TObject *Sender)
{

    if (!WLRegSmartKeyCheck(NameEdit->Text.Length()? NameEdit->Text.c_str() : NULL,
         OrganizationEdit->Text.Length()? OrganizationEdit->Text.c_str() : NULL,
         ExtraDataEdit->Text.Length()? ExtraDataEdit->Text.c_str(): NULL, ActivationCodeEdit->Text.c_str()))
    {
        MessageBox(NULL, "Activation code is INVALID", "Error", MB_OK | MB_ICONERROR);
    }
    else
    {
        // if activation is valid, insert the registration info in a file
        // So, winlicense will be able to finish the registration process

        WLRegSmartKeyInstallToFile(NameEdit->Text.c_str(), OrganizationEdit->Text.c_str(), ExtraDataEdit->Text.c_str(), ActivationCodeEdit->Text.c_str());

        MessageBox(NULL, "Activation code is correct. Please, restart\n this application to finish the registration process", "Success", MB_OK);
    }

}
//---------------------------------------------------------------------------
