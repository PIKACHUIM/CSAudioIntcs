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
}
//---------------------------------------------------------------------------

void __fastcall TForm1::Button1Click(TObject *Sender)
{

    char    TrialHash[] =  "432ab78bd7ar324ghjd87feg63vksyb375am35fj50abloqnxc543jd73hrgf5289dhf73hjdfjhgd783hjgsdmvbaismvhsm76354BfhBFHSgd7634";
    byte    TrialKeyBuffer[100]; 
    int     SizeKey; 
    FILE*   hFileKey;
    int     mLevel = LevelEdit->Text.Length() == 0?  0 :  StrToInt(LevelEdit->Text.c_str());
    int     mDays = DaysEdit->Text.Length() == 0?  0 :  StrToInt(DaysEdit->Text.c_str());
    int     mGlobalMin = GlobalMinEdit->Text.Length() == 0?  0 :  StrToInt(GlobalMinEdit->Text.c_str());
    int     mRuntime = RuntimeEdit->Text.Length() == 0?  0 :  StrToInt(RuntimeEdit->Text.c_str());
    int     mExecutions = ExecEdit->Text.Length() == 0?  0 :  StrToInt(ExecEdit->Text.c_str());
   SYSTEMTIME* pExpDateSysTime;
   SYSTEMTIME  ExpDateSysTime;

    // generate key with current user data

    if (CheckDate->Checked ==  False)
    {
        pExpDateSysTime = NULL;
    }
    else
    {
        DateTimeToSystemTime(DateTimePicker1->Date, ExpDateSysTime);
        pExpDateSysTime = &ExpDateSysTime;
    }

    // create a trial extension key, extending 2 days and 5 executions

    SizeKey = WLGenTrialExtensionFileKey(TrialHash, mLevel, mDays, mExecutions, pExpDateSysTime, mGlobalMin, mRuntime, TrialKeyBuffer);

    // store key in file

    hFileKey = fopen("extendkey.dat","wb+");

    fwrite(TrialKeyBuffer, SizeKey, 1, hFileKey);

    fclose(hFileKey);

    MessageBox(NULL, "A trial extension key has been generated", "Trial Extension SDK", MB_OK);



}
//---------------------------------------------------------------------------

void __fastcall TForm1::CheckDateClick(TObject *Sender)
{

    if (CheckDate->Checked)
    {
        DateTimePicker1->Enabled = True;
    }
    else
    {
        DateTimePicker1->Enabled = False;
    }

}
//---------------------------------------------------------------------------
