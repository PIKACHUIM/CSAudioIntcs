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

char *LicenseHashX = "zQ3tS0p9h889t9748RR5A79C6mQ988k8U4M18k2N01m339g892sprHKrah3nmljOVo0WP8uEzX037HZk3q4TwD0V9h92495z4794"
              "30QtXQ8xWWWl9nWxeN6RNhhrU27w4UftwWoVw7hK1zB4cJf56*eccsAAAQEAABCQKAAAAAY3PJXOMVBEOFENNFUP3V6L6CPTZ427"
              "OWAA*eccpAAAQEAAACQKAAAAAY3PJXOMVBEOFENNFUP3V6L6CPTZ427OWAAKAAAAA4752IAPAQIX3UXP2QWWZUK2YHNOPXOWG*ec"
              "cefffbfffb0201000281810066fc0e761cffeff6efa461f5583ec1444d3997fba8559d36f8d0b6270212c87b532fc4ebc2ed"
              "a3e3e60e9bea1be6a479eee7fc07f860f9816033a69a6f4ee4645939e2f01716caf013fa3742487e6061c0cb06ace0ceab73"
              "1cc8ee4b717053aa2c100e8d202da38d0ad1cba5efd2a81e0deda825f3d4784efe2e481f45a3fcf702030100010281803341"
              "e344c07c568876903fbd68959b860e55cb22938f4137ba31cf67aced8261776f00394405199c75c5f1ae5d02b7fc847f6c7e"
              "df0e0d416a8b6ab1d512d433c48dd6eecb9a8b90e2d38d000a3738b0de0fa6f63a4101d20cf0beacb1140483fc2f733bee02"
              "030db50a06aff22006772b96ef61f56ca484fa7a3b7c0da788b1024082b3c0360ed25f547070810bec3a33c3ef23b65ae6b4"
              "2336e3c7818a1acd3ea3b352e1b7c36de25b602148dd513ae695755913fc68fff4f7ae04fc69e7fea6af0240c9b6017a0eb9"
              "41457e79b1ea2d26ce9e389e2195d0a3cbded6c77e16a46e384d880e223c440d1f2193c65ec6c2cdf774f9a15712a48f42d6"
              "fcc9d2a5df552039024019288e7c1958473f52b16535ad4ffabce160476eb1efc4c3810edb11815d368c7e2d1b03ab5b9071"
              "b44c53e13e0c8f6bff7d990a420fdad07e2fb9828d4488430240094c4f831ddca24a9ecae34da5292dcb13570ebb4633d6b8"
              "fdf392375efb09b3613b2d8ed3a03b382404b7e7b7a120f00e75c67ac0852e1294f284b84ab434d90241005e2991a29aaeb6"
              "d49161376ab102564519fff65bdb2e5c522ffe24ce7da8365c107bc6f2dd2ae2200d4e8b2b63173a8d2cbf21becf3bde570a"
              "c3a95eac71282bfffefffe0201000282010100726b77fff85c37fd9b3b778c124524d4f05fb0034f4dafd967e80994a77da0"
              "6a02bead21cbe2f565857d25c50d134de8e2ed0510bd0ebbe9da5623de74fd6d50a45f4db928421b358dd2511f1bbed5c70c"
              "1dda9f484bda8cdb9c1ef48bd6ab81ce32e1300acde7eb0c9ac792bda80713e0b24013d59fcd8f58bc22433c59f9f3148172"
              "5df70e00715ec77b5277ebdb839dfe415e502281987befe1fcaf90e805531901ee7ddec0c037ae664c807d03722f7c7792f1"
              "248fb477b9f60b809db49b7b2eb5538500ad25bb95d35e27be7b8c6f38d3d0611d883c323a8eb8da2e7334df3a32662a40bb"
              "d974a2951634aa17e08d0dc03b53519c93aa59ccffe5c08f2d0203010001028201000ae0b1af5996b6d1faec8be07aaa2d26"
              "bb55757fd30dc12e8b84d1485b2603d6a03d2d6b7d9d2d9441bd1496a7b8129b95db4fb4782ccaeba010b967403b09a5f582"
              "83fa9adbb20784dc9e4eb54e0eb7d62c8e68bf304aee424094e6cdd692468c6a64e332dbc17524a5205d107fc5707bd7a664"
              "dc55cd6e97d286f55a6af96aa38375036133d05579414daa834fe0c4c938a59d0ceea37b04f88f6a98ead3f4427e0e6ddfc0"
              "11f341f9be9c7207094f5097955b356976b1ec1ae6813e2677d3881d0b4aa68a62539c261776f3da8c8b932b62764d96aed8"
              "b67d8cbcff08b9ebcc1f527b4fa0399a37342b967bf8d1315296fd54baf039ebb3b0b33f3e17e007028180dd69c70cee6b08"
              "f451f57d9635a602c62b3df94bc638ead72ae11b9afaa415b47b47fff68c4c0ec362a96ad414c21be60b762b5d8790a4590a"
              "7c62be17a5f29eaf20fbe758af8a74065642e72808a39adb102431163fa947af93465414825f16ddd6bbed88d7571add575b"
              "62b28aefc8fb394701b794d220061a16ade7a3c35b028180844b11c6d321be2dde95ad4ad0b5a4f10e30c46af02714b99d55"
              "ad5f7eabd3471b79c7da25b75bb5a11abc254b3061df83402c2b1bda3784cf8d235f4c2d46ae8c0e3de747bdec06cd7b6794"
              "8c7a79a96604bfbddc27b5859eb9f7f0e9ad45a0a4c012406add9a241b7ab0d9c24a013cc8b0c0279f06bc1d69d895596fa7"
              "a6170281808e08820918948efcf3ea9032a09dab1fe6b36dcd9f0dfa1027509a7dd39518db5b4e37b3b8123e52366349c55c"
              "bf9c57f93847df4acc8db114d9f019d35fc000ed85323319201aa80d3c9ed45c4916728b5e9268eec5cdd8cf97a140e49b31"
              "56a0de5455af37bc87de3f60bf329b8ff094c00c7ddc7f5a3ff2ca1f8c5591270b0281802f87b119581dd570317a614a71f5"
              "f10ed2645f0d74bef5c94267d7e469645e823bb2e344d32c2f7cf026e4d6194f86ba81337b2af9d4cb01a27838ef32354a21"
              "c81c0dcd9c8ef5cff3df9475608b584eae62a2691a42020bc1dd5ae169736182b7ee722046c4db6e8198f1152ba2df12f19f"
              "df49dceed48354d605c29e45037d0281800f2742c76bd3364bdd8e5635854c46d6cb362e52bc4998c260df76f208454d4180"
              "fe45ede09c0efba85477433d3f70f343e2a488c4eceacbf296a8e0a0a2f5825c3d347ef004569acd4531eba448b252843e75"
              "6978440917f7a5adce1de45aa1348ca8d70317517dbcd6e8993969f4c04e31f3cfe8573d0aa2268196395ac2e2";



//---------------------------------------------------------------------------
__fastcall TForm1::TForm1(TComponent* Owner)
        : TForm(Owner)
{
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
        sLicenseFeatures LicenseFeatures;

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

        // Fill LicenseFeatures structure

        memset(&LicenseFeatures, 0, sizeof(sLicenseFeatures));     // IMPORTANT! Reset the structure
        LicenseFeatures.cb = sizeof(sLicenseFeatures);		   // IMPORTANT! Set size of structure

        LicenseFeatures.NumDays = mDays;
        LicenseFeatures.NumExec = mExec;

        if (pExpDateSysTime)
           memcpy(&LicenseFeatures.ExpDate, pExpDateSysTime, sizeof(SYSTEMTIME));

        SizeKey = WLGenLicenseFileKeyEx(LicenseHashX, pName, pOrg, \
                                pCustom, pHardId, &LicenseFeatures, LicenseKeyBuff) ;

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
        sLicenseFeatures LicenseFeatures;

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

        // Fill LicenseFeatures structure

        memset(&LicenseFeatures, 0, sizeof(sLicenseFeatures));     // IMPORTANT! Reset the structure
        LicenseFeatures.cb = sizeof(sLicenseFeatures);		   // IMPORTANT! Set size of structure

        LicenseFeatures.NumDays = mDays;
        LicenseFeatures.NumExec = mExec;

        if (pExpDateSysTime)
           memcpy(&LicenseFeatures.ExpDate, pExpDateSysTime, sizeof(SYSTEMTIME));

        WLGenLicenseTextKeyEx(LicenseHashX, pName, pOrg, \
                                pCustom, pHardId, &LicenseFeatures, LicenseKeyBuff);

        TextKeyMemo->Text = LicenseKeyBuff;


}
//---------------------------------------------------------------------------

void __fastcall TForm1::Button4Click(TObject *Sender)
{

        SYSTEMTIME      ExpDateSysTime;
        char            LicenseKeyBuff[4000];

        char*           pName = NULL, *pOrg = NULL, *pCustom = NULL, *pHardId = NULL;
        char            Name[100], Org[100], Custom[100], HardId[100];

        int             mDays = NumDaysEdit->Text.Length() == 0?  0 :  StrToInt(NumDaysEdit->Text.c_str());
        int             mExec = NumExecEdit->Text.Length() == 0?  0 :  StrToInt(NumExecEdit->Text.c_str());
        SYSTEMTIME*     pExpDateSysTime;
        sLicenseFeatures LicenseFeatures;

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

         // Fill LicenseFeatures structure

        memset(&LicenseFeatures, 0, sizeof(sLicenseFeatures));     // IMPORTANT! Reset the structure
        LicenseFeatures.cb = sizeof(sLicenseFeatures);		   // IMPORTANT! Set size of structure

        LicenseFeatures.NumDays = mDays;
        LicenseFeatures.NumExec = mExec;

        if (pExpDateSysTime)
           memcpy(&LicenseFeatures.ExpDate, pExpDateSysTime, sizeof(SYSTEMTIME));

        WLGenLicenseDynSmartKey(LicenseHashX, pName, pOrg, \
                                pCustom, pHardId, &LicenseFeatures, LicenseKeyBuff) ;

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
        sLicenseFeatures LicenseFeatures;

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

        // Fill LicenseFeatures structure

        memset(&LicenseFeatures, 0, sizeof(sLicenseFeatures));     // IMPORTANT! Reset the structure
        LicenseFeatures.cb = sizeof(sLicenseFeatures);		   // IMPORTANT! Set size of structure

        LicenseFeatures.NumDays = mDays;
        LicenseFeatures.NumExec = mExec;

        if (pExpDateSysTime)
           memcpy(&LicenseFeatures.ExpDate, pExpDateSysTime, sizeof(SYSTEMTIME));

        SizeKey = WLGenLicenseRegistryKeyEx(LicenseHashX, pName, pOrg, \
                                pCustom, pHardId, &LicenseFeatures, RegKeyName, RegValueEdit->Text.c_str(), LicenseKeyBuff) ;


        // store key in file

        hFileKey = fopen(LicRegEdit->Text.c_str(),"wb+");

        fwrite(LicenseKeyBuff, SizeKey, 1, hFileKey);

        fclose(hFileKey);

        MessageBox(NULL, "A License registry file has been generated", "License Key Generator SDK", MB_OK);
}
//---------------------------------------------------------------------------

