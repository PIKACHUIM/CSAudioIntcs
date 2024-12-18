/****************************************************************************** 
/* Module: Example
/* Description: Shows how to work with password generators functions
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

LRESULT CALLBACK  PasswordHandler(HWND, UINT, WPARAM, LPARAM);


/****************************************************************************** 
/*                          Global variables
/*****************************************************************************/ 

char* PasswordHash = "432ab78bd7ar324ghjd87feg63vksyb375am35fj50abloqnxc543jd73hrgf5289d";


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
    DialogBox(GetModuleHandle(NULL), (LPCTSTR)IDD_ABOUTBOX, NULL, (DLGPROC)PasswordHandler);

    return 0;
}


/*****************************************************************************
* PasswordHandler
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


LRESULT CALLBACK PasswordHandler(HWND hDlg, UINT message, WPARAM wParam, LPARAM lParam)
{
    char NameBuffer[80];
    char PasswordBuffer[80];

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

        if (LOWORD(wParam) == IDC_GENPASSWORD)
        {
            GetDlgItemText(hDlg, IDC_EDIT1, NameBuffer, 80);
            WLGenPassword(PasswordHash, NameBuffer, PasswordBuffer);
            SetDlgItemText(hDlg, IDC_EDIT2, PasswordBuffer);
        }

        break;

    }
    
    return FALSE;
}
