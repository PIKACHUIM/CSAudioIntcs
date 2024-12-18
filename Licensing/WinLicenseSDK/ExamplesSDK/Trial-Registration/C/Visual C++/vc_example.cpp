/****************************************************************************** 
/* Module: Example
/* Description: Shows how to work with Trial/Registration functions
/*
/* Authors: Oreans Technologies  
/* (c) 2010 Oreans Technologies 
/*****************************************************************************/ 


/****************************************************************************** 
/*                   Libraries used by this module
/*****************************************************************************/ 

#include "stdafx.h"
#include <stdio.h>
#include "Resource.h"
#include <commctrl.h>
#include <windows.h>
#include "WinlicenseSDK.h"


/****************************************************************************** 
/*                          Function prototypes
/*****************************************************************************/ 

LRESULT CALLBACK  MainHandler(HWND, UINT, WPARAM, LPARAM);


/****************************************************************************** 
/*                          Global variables
/*****************************************************************************/ 


/****************************************************************************** 
/*                             Code Section
/*****************************************************************************/ 

/*****************************************************************************
* WinMain
*
*  Main program function
*
* Inputs
*  Standard WinMain parameters
* 
* Outputs
*  None
*
* Returns
*  Exit Code
******************************************************************************/

int APIENTRY _tWinMain(HINSTANCE hInstance,
                     HINSTANCE hPrevInstance,
                     LPTSTR    lpCmdLine,
                     int       nCmdShow)
{
    DialogBox(GetModuleHandle(NULL), (LPCTSTR)IDD_ABOUTBOX, NULL, (DLGPROC)MainHandler);

    return 0;
}


/*****************************************************************************
* MainHandler
*
*  Message handler for dialog box
*
* Inputs
*  Standard Dlgbox message handle parameters
* 
* Outputs
*  None
*
* Returns
*  Exit Code
******************************************************************************/


LRESULT CALLBACK MainHandler(HWND hDlg, UINT message, WPARAM wParam, LPARAM lParam)
{
   int Status; 
   char AuxBuffer[255];
   char RegName[255];
   char CompanyName[255];
   char CustomData[255];
   SYSTEMTIME TrialDate = {0,0,0,0,0,0,0,0};
   int RegExtendedInfo;

    switch (message)
    {

    case WM_INITDIALOG:
        
        return TRUE;

    case WM_COMMAND:

        if (LOWORD(wParam) == IDCANCEL) 
        {
            EndDialog(hDlg, LOWORD(wParam));
            return TRUE;
        }

        if (LOWORD(wParam) == IDC_CHECK_APP_STATUS)
        {
            // get application status

            Status = WLRegGetStatus(&RegExtendedInfo);

            switch (Status)
            {
                case 0:
                    SetDlgItemText(hDlg, IDC_STATUS, "Trial");
                    break;

                case 1:
                    SetDlgItemText(hDlg, IDC_STATUS, "Registered");
                break;

                case 2:
                    SetDlgItemText(hDlg, IDC_STATUS, "Invalid License");
                    break;

                case 3:
                    SetDlgItemText(hDlg, IDC_STATUS, "License Locked to different machine");
                    break;

                case 4:
                    SetDlgItemText(hDlg, IDC_STATUS, "No more HW-ID changes allowed");
                    break;

                case 5:
                    SetDlgItemText(hDlg, IDC_STATUS, "License Key expired");
                    break;
            }

            if (Status != 1)
            {

                // set trial labels data
                
                sprintf(AuxBuffer, "%d", WLTrialDaysLeft());
                SetDlgItemText(hDlg, IDC_DAYSLEFT, AuxBuffer);
            
                sprintf(AuxBuffer, "%d", WLTrialExecutionsLeft());
                SetDlgItemText(hDlg, IDC_EXECLEFT, AuxBuffer);

                sprintf(AuxBuffer, "%d", WLTrialGlobalTimeLeft());
                SetDlgItemText(hDlg, IDC_MINUTESLEFT, AuxBuffer);

                sprintf(AuxBuffer, "%d", WLTrialRuntimeLeft());
                SetDlgItemText(hDlg, IDC_RUNTIMELEFT, AuxBuffer);

                WLTrialExpirationDate(&TrialDate);
                SendDlgItemMessage(hDlg, IDC_DATETIMEPICKER1, DTM_SETSYSTEMTIME, GDT_VALID, (LPARAM)&TrialDate);
  
                // set registration label data as empty  

                SetDlgItemText(hDlg, IDC_EDITNAME, "");
                SetDlgItemText(hDlg, IDC_EDITCOMPANY, "");
                SetDlgItemText(hDlg, IDC_EDITCUSTOM, "");
                SetDlgItemText(hDlg, IDC_REG_DAYSLEFT, "");
                SetDlgItemText(hDlg, IDC_REG_EXECLEFT, "");
            }    
            else
            {

                WLRegGetLicenseInfo(RegName, CompanyName, CustomData);

                SetDlgItemText(hDlg, IDC_EDITNAME, RegName);
                SetDlgItemText(hDlg, IDC_EDITCOMPANY, CompanyName);
                SetDlgItemText(hDlg, IDC_EDITCUSTOM, CustomData);

                sprintf(AuxBuffer, "%d", WLRegDaysLeft());
                SetDlgItemText(hDlg, IDC_REG_DAYSLEFT, AuxBuffer);

                sprintf(AuxBuffer, "%d", WLRegExecutionsLeft());
                SetDlgItemText(hDlg, IDC_REG_EXECLEFT, AuxBuffer);

                WLRegExpirationDate(&TrialDate);
                SendDlgItemMessage(hDlg, IDC_DATETIMEPICKER2, DTM_SETSYSTEMTIME, GDT_VALID, (LPARAM)&TrialDate);

                // set trial information data as empty

                SetDlgItemText(hDlg, IDC_DAYSLEFT, "");
                SetDlgItemText(hDlg, IDC_EXECLEFT, "");
                SetDlgItemText(hDlg, IDC_MINUTESLEFT, "");
                SetDlgItemText(hDlg, IDC_RUNTIMELEFT, "");

            }

        }

        if (LOWORD(wParam) == IDC_BUTTON_HARDID)
        {

            char MachineId[100];

            WLHardwareGetID(MachineId);
            SetDlgItemText(hDlg, IDC_EDIT_HARDID, MachineId);

        }

        break;

    }
    
    return FALSE;
}
