/******************************************************************************
/* Module: PluginDll
/* Description: Example of plugin DLL for WinLicense
/*
/*  To test this example, please make sure that you enable the following options
/*  In the Registration panel:
/* 
/*   1) Check the option "This application can be registered..."
/*   2) Check the option "File License" and put for example "regkey.dat"
/*   3) Check the option "Enable SmartActivate System...."
/*   4) Set the option "SmartKey Type to: DYNAMIC_SMART_KEY
/*
/*   Now you have to create a Dynamic SmartKey license. The easiest way is to
/*   go to the "License Manager" and add a new Dynamic Smarkey. Make sure that
/*   you check the option "UNICODE License" (as this example works in UNICODE)
/*   The easiest would be to check also "Embed User Info in key", so you
/*   just deliver a single smartkey (no need to enter User/Company/Custom data
/*   in the registration form
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
#include "SecureEnginePluginsSdk.h"

/******************************************************************************
/*                      Local Function prototypes
/*****************************************************************************/

LRESULT CALLBACK  DialogProcHandler(HWND, UINT, WPARAM, LPARAM);

/******************************************************************************
/*                          Global variables
/*****************************************************************************/

HINSTANCE           DllInstance;
TWDCfunctionsArrayW RegistrationFunctionsArray;
BOOL                IsApplicationRegistered = FALSE;

/******************************************************************************
/*                            Code Section
/*****************************************************************************/

/******************************************************************************
; DllMain
;*****************************************************************************/
extern "C"
BOOL __stdcall
DllMain(
    IN HINSTANCE hInstance,
    IN DWORD     dwReason,
    IN LPVOID    lpReserved)
{
    UNREFERENCED_PARAMETER(lpReserved);

    if (dwReason == DLL_PROCESS_ATTACH)
    {
        DllInstance = hInstance;
    }
    else if (dwReason == DLL_PROCESS_DETACH)
    {
    }
    return TRUE;
}

/******************************************************************************
; SecureEngineInitialize
;*****************************************************************************/
extern "C" __declspec(dllexport)
bool __stdcall SecureEngineInitialize(void)
{
    MessageBoxA(NULL, "Calling SecureEngineInitialize", "TestPlugin", 
                MB_OK | MB_ICONINFORMATION);
    return true;		// Continue execution
}

/******************************************************************************
; SecureEngineFinalize
;*****************************************************************************/
extern "C" __declspec(dllexport)
bool __stdcall SecureEngineFinalize(void)
{
    MessageBoxA(NULL, "Calling SecureEngineFinalize", "TestPlugin", 
                MB_OK | MB_ICONINFORMATION);
    return true;		// Continue execution
}

/******************************************************************************
; SecureEngineShowCustomMessageA
;*****************************************************************************/
extern "C" __declspec(dllexport)
bool __stdcall SecureEngineShowCustomMessageA(
    IN int   MsgId,
    IN char* MsgBody)
{
    MessageBoxA(NULL, MsgBody, "SecureEngineShowCustomMessageA", 
                MB_OK | MB_ICONINFORMATION);
    return true;  // TRUE = Message has been handled. The protection won't display it
}

/******************************************************************************
; SecureEngineGetEncryptionKey
;*****************************************************************************/
extern "C" __declspec(dllexport)
bool __stdcall SecureEngineGetEncryptionKey(
    IN int   ZoneId,
    IN char* pOutputEncryptionKey)
{
    strcpy(pOutputEncryptionKey, "My Encryption Key");
    return true;    // Continue execution
}

/*****************************************************************************
* SecureEngineDoRegistrationW
******************************************************************************/
extern "C" __declspec(dllexport)
bool __stdcall SecureEngineGetApplicationStatus(
    IN int            ApplicationStatus,
    IN int            ExtendedStatus,
    IN TTrialExpInfo* TrialExpInfo,
    IN TRegExpInfo*   RegExpInfo)
{
    if (ApplicationStatus == wdcRegistered)
    {
        IsApplicationRegistered = TRUE;
    }
    return TRUE;    // Continue execution. You could return FALSE if
                    // license is not installed or incorrect
}


/*****************************************************************************
* SecureEngineDoRegistrationW
******************************************************************************/
extern "C" __declspec(dllexport)
bool __stdcall SecureEngineDoRegistrationW(
    IN TWDCfunctionsArrayW *pArrayFunctions)
{
    RegistrationFunctionsArray = *pArrayFunctions;

    // Display registration form if application is not registered
    if (!IsApplicationRegistered)
    {
        DialogBox(DllInstance, (LPCTSTR)IDD_ABOUTBOX, NULL,
            (DLGPROC)DialogProcHandler);
    }

    return TRUE;    // Continue execution. You could return FALSE if
                    // license is not installed or incorrect
}

/*****************************************************************************
* DialogProcHandler
******************************************************************************/
LRESULT CALLBACK 
DialogProcHandler(
    IN HWND hDlg,
    IN UINT message,
    IN WPARAM wParam,
    IN LPARAM lParam)
{
    wchar_t UserName[200];
    wchar_t Company[200];
    wchar_t ExtraData[200];
    wchar_t Activation[2000];

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
            GetDlgItemTextW(hDlg, IDC_EDITNAME, UserName, 200);
            GetDlgItemTextW(hDlg, IDC_EDITCOMPANY, Company, 200);
            GetDlgItemTextW(hDlg, IDC_EDITEXTRADATA, ExtraData, 200);
            GetDlgItemTextW(hDlg, IDC_EDITACTIVATION, Activation, 1024);

            // NOTE: This example works with a *UNICODE* SmartKey. You have
            // to make sure that when you generate the license, you set the
            // field "UNICODE"

            if (RegistrationFunctionsArray.pfWLRegSmartKeyCheck(
                UserName, Company, ExtraData, Activation))
            {
                MessageBoxA(NULL, "Activation code is valid", "Success", MB_OK);

                // Let's install the license in a file
                if (RegistrationFunctionsArray.pfWLRegSmartKeyInstallToFile(
                    UserName, Company, ExtraData, Activation))
                {
                    MessageBoxA(NULL, "Key installed in file. Application will restart", 
                                "Success", MB_OK);
                    RegistrationFunctionsArray.pfWLRestartApplication();
                }
                else
                {
                    MessageBoxA(NULL, "Cannot install key in file", "Error",
                        MB_OK | MB_ICONERROR);
                }
            }
            else
            {
                MessageBoxA(NULL, "Activation code is INVALID", "Error",
                    MB_OK | MB_ICONERROR);
            }
        }
        break;
    }
    return FALSE;
}
