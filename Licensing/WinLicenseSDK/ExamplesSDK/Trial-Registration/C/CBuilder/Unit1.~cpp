//---------------------------------------------------------------------------

#include <vcl.h>
#pragma hdrstop

#include "Unit1.h"
#include <stdio.h>
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

}
//---------------------------------------------------------------------------



void __fastcall TForm1::Button5Click(TObject *Sender)
{

   int Status = WLRegGetStatus(NULL);
   char RegName[255];
   char CompanyName[255];
   char CustomData[255];
   SYSTEMTIME TrialDate;

   // Reset TrialDate

   TrialDate.wYear = TrialDate.wMonth = TrialDate.wDay =  TrialDate.wSecond = 0;

   switch (Status)
   {
        case 0:
            StatusLabel->Caption = "Trial";
            break;

        case 1:
            StatusLabel->Caption = "Registered";
            break;

        case 2:
            StatusLabel->Caption = "Invalid License";
            break;

        case 3:
            StatusLabel->Caption = "License Locked to different machine";
            break;

        case 4:
            StatusLabel->Caption = "No more HW-ID changes allowed";
            break;

         case 5:
            StatusLabel->Caption = "License Key expired";
            break;
   }

   // show information about trial if application is not registered

   if (Status != 1)
   {

        // set trial labels data

        DaysLeftLabel->Caption = IntToStr(WLTrialDaysLeft());
        ExecLeftLabel->Caption = IntToStr(WLTrialExecutionsLeft());
        MinutesLeftLabel->Caption = IntToStr(WLTrialGlobalTimeLeft());
        RuntimeLeftLabel->Caption = IntToStr(WLTrialRuntimeLeft());

        WLTrialExpirationDate(&TrialDate);
        ExpDatePicker->Date = SystemTimeToDateTime(TrialDate);

        // set registration label data as empty

        RegNameEdit->Text = "";
        CompanyNameEdit->Text = "";
        CustomEdit->Text = "";
        RegDaysLeftLabel->Caption = "";
        RegExecLeftLabel->Caption = "";

   }
   else
   {

        // set license labels data

        WLRegGetLicenseInfo(RegName, CompanyName, CustomData);
        RegNameEdit->Text = RegName;
        CompanyNameEdit->Text = CompanyName;
        CustomEdit->Text = CustomData;
        RegDaysLeftLabel->Caption = IntToStr(WLRegDaysLeft());
        RegExecLeftLabel->Caption = IntToStr(WLRegExecutionsLeft());

        WLRegExpirationDate(&TrialDate);
        RegDateTimePicker1->Date = SystemTimeToDateTime(TrialDate);

        // set registration label data as empty

        DaysLeftLabel->Caption = "";
        ExecLeftLabel->Caption = "";
        MinutesLeftLabel->Caption = "";
        RuntimeLeftLabel->Caption = "";
   }

}
//---------------------------------------------------------------------------

void __fastcall TForm1::Button1Click(TObject *Sender)
{
   char MachineId[100];

    WLHardwareGetID(MachineId);
    HardwareIdEdit->Text = MachineId;

}
//---------------------------------------------------------------------------

