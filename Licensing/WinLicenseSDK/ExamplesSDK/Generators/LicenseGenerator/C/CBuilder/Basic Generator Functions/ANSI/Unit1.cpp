//---------------------------------------------------------------------------

#include <vcl.h>
#pragma hdrstop

#include "Unit1.h"
#include <stdio.h>
#include "WinlicenseSDK.h"


//---------------------------------------------------------------------------
#pragma package(smart_init)
#pragma resource "*.dfm"
TForm1 *Form1;
//---------------------------------------------------------------------------
__fastcall TForm1::TForm1(TComponent* Owner)
        : TForm(Owner)
{
        LicenseHash = "yuhsdf873hJHslk99423jhdsfhGSDKURKCFNB78934hGDMNF5634GfghwyIPahfurH6394hfJfhUYfikfhu8whj3yHYFIOOFhNFAPWRE";
}
//---------------------------------------------------------------------------


void __fastcall TForm1::Button1Click(TObject *Sender)
{

        SYSTEMTIME      ExpDateSysTime;
        byte            LicenseKeyBuff[4000];
        int             SizeKey;
        FILE*           hFileKey;

        char*           pName = NULL, *pOrg = NULL, *pCustom = NULL, *pHardId = NULL;
        char            Name[100], Org[100], Custom[100], HardId[100];

        int             mDays = NumDaysEdit->Text.Length() == 0?  0 :  StrToInt(NumDaysEdit->Text.c_str());
        int             mExec = NumExecEdit->Text.Length() == 0?  0 :  StrToInt(NumExecEdit->Text.c_str());
        SYSTEMTIME*     pExpDateSysTime;

        // set up parameters before passing to GenerateLicenseFileKey

        if (NameEdit->Text.Length())
        {
            strcpy(Name, NameEdit->Text.c_str());
            pName = Name;
        }

        if (OrgEdit->Text.Length())
        {
            strcpy(Org, OrgEdit->Text.c_str());
            pOrg = Org;
        }

        if (CustomEdit->Text.Length())
        {
            strcpy(Custom, CustomEdit->Text.c_str());
            pCustom = Custom;
        }

        if (HardIdEdit->Text.Length())
        {
            strcpy(HardId, HardIdEdit->Text.c_str());
            pHardId = HardId;
        }

        if (ExpDate->Enabled)
        {
               DateTimeToSystemTime(ExpDate->Date, ExpDateSysTime);
               pExpDateSysTime = &ExpDateSysTime;
        }
        else
        {
               pExpDateSysTime = NULL;
        }

        SizeKey = WLGenLicenseFileKey(LicenseHash, pName, pOrg, \
                                pCustom, pHardId, mDays, mExec \
                                , pExpDateSysTime, 0, 0, 0, LicenseKeyBuff) ;


        // store key in file

        hFileKey = fopen(FileNameEdit->Text.c_str(),"wb+");

        fwrite(LicenseKeyBuff, SizeKey, 1, hFileKey);

        fclose(hFileKey);

        MessageBox(NULL, "A License key file has been generated", "License Key Generator SDK", MB_OK);


}
//---------------------------------------------------------------------------
void __fastcall TForm1::Button3Click(TObject *Sender)
{

        SYSTEMTIME      ExpDateSysTime;
        char            LicenseKeyBuff[4000];
        char*           pName = NULL, *pOrg = NULL, *pCustom = NULL, *pHardId = NULL;
        char            Name[100], Org[100], Custom[100], HardId[100];

        int             mDays = NumDaysEdit->Text.Length() == 0?  0 :  StrToInt(NumDaysEdit->Text.c_str());
        int             mExec = NumExecEdit->Text.Length() == 0?  0 :  StrToInt(NumExecEdit->Text.c_str());
        SYSTEMTIME*     pExpDateSysTime;

        // set up parameters before passing to GenerateTextKey

        if (NameEdit->Text.Length())
        {
            strcpy(Name, NameEdit->Text.c_str());
            pName = Name;
        }

        if (OrgEdit->Text.Length())
        {
            strcpy(Org, OrgEdit->Text.c_str());
            pOrg = Org;
        }

        if (CustomEdit->Text.Length())
        {
            strcpy(Custom, CustomEdit->Text.c_str());
            pCustom = Custom;
        }

        if (HardIdEdit->Text.Length())
        {
            strcpy(HardId, HardIdEdit->Text.c_str());
            pHardId = HardId;
        }

        if (ExpDate->Enabled)
        {
               DateTimeToSystemTime(ExpDate->Date, ExpDateSysTime);
               pExpDateSysTime = &ExpDateSysTime;
        }
        else
        {
               pExpDateSysTime = NULL;
        }

        WLGenLicenseTextKey(LicenseHash, pName, pOrg, \
                                pCustom, pHardId, mDays, mExec \
                                , pExpDateSysTime, 0, 0, 0, LicenseKeyBuff);

        TextKeyMemo->Text = LicenseKeyBuff;


}
//---------------------------------------------------------------------------

void __fastcall TForm1::Button4Click(TObject *Sender)
{

        SYSTEMTIME      ExpDateSysTime;
        char            LicenseKeyBuff[1000];

        char*           pName = NULL, *pOrg = NULL, *pCustom = NULL, *pHardId = NULL;
        char            Name[100], Org[100], Custom[100], HardId[100];

        int             mDays = NumDaysEdit->Text.Length() == 0?  0 :  StrToInt(NumDaysEdit->Text.c_str());
        int             mExec = NumExecEdit->Text.Length() == 0?  0 :  StrToInt(NumExecEdit->Text.c_str());
        SYSTEMTIME*     pExpDateSysTime;

        // set up parameters before passing to GenerateSmartActivateKey

        if (NameEdit->Text.Length())
        {
            strcpy(Name, NameEdit->Text.c_str());
            pName = Name;
        }

        if (OrgEdit->Text.Length())
        {
            strcpy(Org, OrgEdit->Text.c_str());
            pOrg = Org;
        }

        if (CustomEdit->Text.Length())
        {
            strcpy(Custom, CustomEdit->Text.c_str());
            pCustom = Custom;
        }

        if (HardIdEdit->Text.Length())
        {
            strcpy(HardId, HardIdEdit->Text.c_str());
            pHardId = HardId;
        }

        // generate key with current user data

        if (ExpDate->Enabled)
        {
               DateTimeToSystemTime(ExpDate->Date, ExpDateSysTime);
               pExpDateSysTime = &ExpDateSysTime;
        }
        else
        {
               pExpDateSysTime = NULL;
        }

        WLGenLicenseSmartKey(LicenseHash, pName, pOrg, \
                                pCustom, pHardId, mDays, mExec \
                                , pExpDateSysTime, LicenseKeyBuff) ;

        SmartEdit->Text = LicenseKeyBuff;


}
//---------------------------------------------------------------------------

void __fastcall TForm1::EnableDateCheckClick(TObject *Sender)
{

  if (EnableDateCheck->State == cbChecked)
  {
        ExpDate->Enabled = 1;
  }
  else
  {
       ExpDate->Enabled = 0;
  }




}
//---------------------------------------------------------------------------


void __fastcall TForm1::Button2Click(TObject *Sender)
{

        SYSTEMTIME      ExpDateSysTime;
        char            LicenseKeyBuff[4000];
        FILE*           hFileKey;
        int             SizeKey;
        char*           pName = NULL, *pOrg = NULL, *pCustom = NULL, *pHardId = NULL;
        char            Name[100], Org[100], Custom[100], HardId[100];
        int             mDays = NumDaysEdit->Text.Length() == 0?  0 :  StrToInt(NumDaysEdit->Text.c_str());
        int             mExec = NumExecEdit->Text.Length() == 0?  0 :  StrToInt(NumExecEdit->Text.c_str());
        SYSTEMTIME*     pExpDateSysTime;
        char            RegKeyName[100] = {0};

        // set up parameters before passing to GenerateSmartActivateKey

        if (NameEdit->Text.Length())
        {
            strcpy(Name, NameEdit->Text.c_str());
            pName = Name;
        }

        if (OrgEdit->Text.Length())
        {
            strcpy(Org, OrgEdit->Text.c_str());
            pOrg = Org;
        }

        if (CustomEdit->Text.Length())
        {
            strcpy(Custom, CustomEdit->Text.c_str());
            pCustom = Custom;
        }

        if (HardIdEdit->Text.Length())
        {
            strcpy(HardId, HardIdEdit->Text.c_str());
            pHardId = HardId;
        }

        strcat(RegKeyName, "HKEY_LOCAL_MACHINE\\");
        strcat(RegKeyName, RegistryEdit->Text.c_str());

        // generate key with current user data

        if (ExpDate->Enabled)
        {
               DateTimeToSystemTime(ExpDate->Date, ExpDateSysTime);
               pExpDateSysTime = &ExpDateSysTime;
        }
        else
        {
               pExpDateSysTime = NULL;
        }

        SizeKey = WLGenLicenseRegistryKey(LicenseHash, pName, pOrg, \
                                pCustom, pHardId, mDays, mExec \
                                , pExpDateSysTime, 0, 0, 0, RegKeyName, RegValueEdit->Text.c_str(), LicenseKeyBuff) ;


        // store key in file

        hFileKey = fopen(LicRegEdit->Text.c_str(),"wb+");

        fwrite(LicenseKeyBuff, SizeKey, 1, hFileKey);

        fclose(hFileKey);

        MessageBox(NULL, "A License registry file has been generated", "License Key Generator SDK", MB_OK);



}
//---------------------------------------------------------------------------

