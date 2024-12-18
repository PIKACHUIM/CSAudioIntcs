/****************************************************************************** 
/* Module: Example
/* Description: Shows how to work with SmartKeys
/*
/* Authors: Oreans Technologies  
/* (c) 2010 Oreans Technologies
/*****************************************************************************/ 


/****************************************************************************** 
/*                   Libraries used by this module
/*****************************************************************************/ 

#include "stdafx.h"
#include "Resource.h"
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
/*                            Code Section
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

    // check if application has been activated (registered)

    if (WLRegGetStatus(NULL) == 1)
    {
        MessageBox(NULL, "This application has been activated", "Winlicense", MB_OK);
    }
    else
    {
        DialogBox(GetModuleHandle(NULL), (LPCTSTR)IDD_ABOUTBOX, NULL, (DLGPROC)MainHandler);
    }

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
    char NameBuffer[200];
    char Company[200];
    char ExtraData[200];
    char Activation[200];
    char ActivationKey[80];

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

        if (LOWORD(wParam) == IDC_BUTTONACTIVATE)
        {
            GetDlgItemText(hDlg, IDC_EDITNAME, NameBuffer, 200);
            GetDlgItemText(hDlg, IDC_EDITCOMPANY, Company, 200);
            GetDlgItemText(hDlg, IDC_EDITEXTRADATA, ExtraData, 200);
            GetDlgItemText(hDlg, IDC_EDITACTIVATION, Activation, 200);

        if (!WLRegSmartKeyCheck(NameBuffer, Company, ExtraData, Activation))
        {
            MessageBox(NULL, "Activation code is INVALID", "Error", MB_OK | MB_ICONERROR);
        }
        else
        {
            // if activation is valid, insert the registration info in a file
            // So, winlicense will be able to finish the registration process

            WLRegSmartKeyInstallToFile(NameBuffer, Company, ExtraData, Activation);

            MessageBox(NULL, "Activation code is correct. Please, restart\n this application to finish the registration process", "Success", MB_OK);
        }

        }

        break;

    }
    
    return FALSE;
}
