/****************************************************************************** 
/* Module: ActivationDLL
/* Description: Shows how to generate a simple Activation Form
/*
/* Authors: Oreans Technologies
/* (c) 2016 Oreans Technologies
/*****************************************************************************/ 

/****************************************************************************** 
/*                   Libraries used by this module
/*****************************************************************************/ 
#include "stdafx.h"
#include "resource.h"
#include "ActivationSDK.h"

/****************************************************************************** 
/*								Constants
/*****************************************************************************/ 
#define	 MAX_ACTIVATION_KEY_SIZE								256

/******************************************************************************
;                          Functions Prototypes
;*****************************************************************************/
LRESULT CALLBACK MainHandler(HWND hDlg, UINT message, WPARAM wParam, LPARAM lParam);

/****************************************************************************** 
/*                          Global variables
/*****************************************************************************/ 
HMODULE DllhModule = NULL;
char 	DialogActivationKey[MAX_ACTIVATION_KEY_SIZE];

/****************************************************************************** 
/*                             Code Section
/*****************************************************************************/ 

/*****************************************************************************
* ActivationHandler
/*****************************************************************************/
extern "C" __declspec(dllexport) int __stdcall
ActivationHandler( 
	int  EventId,
	int  ActivationErrorCode,
	int  WinsockErrorCode,
	char *ServerResponseString,
	char *pActivationKey)
{
	int exit_code = 0;

	if ((EventId == EVENT_SHOW_ACTIVATION) || (EventId == EVENT_SHOW_DEACTIVATION)) 
	{
		DialogActivationKey[0] = '\0'; // Initialize retrieved activation key 
		DialogBox(DllhModule, (LPCTSTR)IDD_DIALOG1, NULL, (DLGPROC)MainHandler);

		if (DialogActivationKey[0])
		{
		  // copy Activation Key entered by user
		  strcpy_s(pActivationKey, MAX_ACTIVATION_KEY_SIZE, DialogActivationKey);
		  exit_code = FORM_RESPONSE_PROCESS_ACTIVATION_KEY;
		}
		else
		{
			exit_code = FORM_RESPONSE_TERMINATE_APPLICATION;
		}
	}
	else if (EventId == EVENT_CHECK_RESPONSE)
	{
		if (ActivationErrorCode == RESPONSE_ACTIVATION_OK)
		{
			MessageBoxA(NULL, "Application successfully activated!", "Activation", 
						MB_ICONINFORMATION | MB_OK);
			exit_code = FORM_RESPONSE_RESTART_APPLICATION; 
		}
		else 
		{
			if (ActivationErrorCode == RESPONSE_ERROR_KEY_NOT_FOUND)
			{
				MessageBoxA(NULL, "RESPONSE_ERROR_KEY_NOT_FOUND", "Activation", 
							MB_ICONERROR | MB_OK);
			}
		    else if (ActivationErrorCode == RESPONSE_ERROR_MAX_DEVICES_REACHED) 
			{
				MessageBoxA(NULL, "RESPONSE_ERROR_MAX_DEVICES_REACHED", "Activation", 
							MB_ICONERROR | MB_OK);
			}
		    else if (ActivationErrorCode == RESPONSE_ERROR_NO_MORE_DIFFERENT_DEVICES_ALLOWED) 
			{
				MessageBoxA(NULL, "RESPONSE_ERROR_NO_MORE_DIFFERENT_DEVICES_ALLOWED", "Activation", 
							MB_ICONERROR | MB_OK);
			}
		    else if (ActivationErrorCode == RESPONSE_ERROR_WRONG_DATA_RECEIVED) 
			{
				MessageBoxA(NULL, "RESPONSE_ERROR_WRONG_DATA_RECEIVED", "Activation", 
							MB_ICONERROR | MB_OK);
			}
		    else if (ActivationErrorCode == RESPONSE_ERROR_KEY_DISABLED_BY_SELLER) 
			{
				MessageBoxA(NULL, "RESPONSE_ERROR_KEY_DISABLED_BY_SELLER", "Activation", 
							MB_ICONERROR | MB_OK);
			}
		    else if (ActivationErrorCode == RESPONSE_ERROR_KEY_EXPIRED) 
			{
				MessageBoxA(NULL, "RESPONSE_ERROR_KEY_EXPIRED", "Activation", 
							MB_ICONERROR | MB_OK);
			}
		    else if (ActivationErrorCode == RESPONSE_ERROR_WINSOCK_ERROR) 
			{
				MessageBoxA(NULL, "RESPONSE_ERROR_WINSOCK_ERROR", "Activation", 
							MB_ICONERROR | MB_OK);
			}
		    else if (ActivationErrorCode == RESPONSE_ERROR_CANNOT_INSTALL_LICENSE) 
			{
				MessageBoxA(NULL, "Cannot write license to disk. File or Directory write protected", 
							"Activation", MB_ICONERROR | MB_OK);
			}
			else
			{
				char error_code_message[128];

				wsprintfA(error_code_message, "Unknown error: %d", ActivationErrorCode);
				MessageBoxA(NULL, error_code_message, "Activation", MB_ICONERROR | MB_OK);
			}

		  // displayed detailed information returned by the activation server
		  if (ActivationErrorCode != RESPONSE_ERROR_WINSOCK_ERROR)
		  {
			MessageBoxA(NULL, ServerResponseString, "Server Response", MB_ICONWARNING | MB_OK);
		  }

		  // ask again the user to re-enter the activation key
		  exit_code = FORM_RESPONSE_SHOW_ACTIVATION_FORM_AGAIN;
		}
	}
	return exit_code;
}

/*****************************************************************************
* MainHandler
/*****************************************************************************/
LRESULT CALLBACK MainHandler(HWND hDlg, UINT message, WPARAM wParam, LPARAM lParam)
{
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

        if (LOWORD(wParam) == IDOK) 
        {
			GetDlgItemTextA(hDlg, ID_ACTIVATION, DialogActivationKey, MAX_ACTIVATION_KEY_SIZE);
            EndDialog(hDlg, LOWORD(wParam));
            return TRUE;
        }
        break;
    }    
    return FALSE;
}

/*****************************************************************************
* SetDllhModule
/*****************************************************************************/
void
SetDllhModule( 
	HMODULE  hModule)
{
	DllhModule = hModule;	
}