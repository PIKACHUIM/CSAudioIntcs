/****************************************************************************** 
/* Module: Example
/* Description: Shows how to call WLStringDecrypt in Visual C++ language
/*
/* Authors: Oreans Technologies  
/* (c) 2010 Oreans Technologies
/*****************************************************************************/ 


/****************************************************************************** 
/*                   Libraries used by this module
/*****************************************************************************/ 

#include <windows.h>
#include <stdio.h>
#include "WinlicenseSDK.h"


/****************************************************************************** 
/*                           Global Variables
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

int WINAPI WinMain (HINSTANCE hInstance, HINSTANCE hPrevInstance,
                       PSTR szCmdLine, int iCmdShow)
{
	wchar_t	OurStringBuff[256];

    // Write our custom string

	WLTrialStringWriteW(L"Our String", L"Hello 123");

	// Read our custom string

	WLTrialStringReadW(L"Our String", OurStringBuff);

	MessageBoxW(NULL, OurStringBuff, L"Custom String Value:", MB_ICONINFORMATION | MB_OK);

	return 0;

}


