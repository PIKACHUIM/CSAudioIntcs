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
    char    Name[200];
    char    Company[200];
    char    ExtraData[200];

    // check if application has been registered

    if (WLRegGetStatus(NULL) == 1)
    {
        Panel1->Visible = True;
        WLRegGetLicenseInfo(Name, Company, ExtraData);
        NameEdit->Text = Name;
        CompanyEdit->Text = Company;
        ExtradataEdit->Text = ExtraData;
    }

}
//---------------------------------------------------------------------------

void __fastcall TForm1::Button1Click(TObject *Sender)
{
    if (!WLRegNormalKeyCheck(TextKeyEdit->Text.c_str()))
    {
        MessageBox(NULL, "The key that you entered is invalid.\nPlease, try it again", "Error", MB_OK | MB_ICONERROR);
    }
    else
    {
        // if key is valid, insert the text key in a file
        // So, winlicense will be able to finish the registration process

        WLRegNormalKeyInstallToFile(TextKeyEdit->Text.c_str());
        MessageBox(NULL, "Registration key is correct. Please, restart\n this application to finish the registration process", "Success", MB_OK);
    }

}
//---------------------------------------------------------------------------
