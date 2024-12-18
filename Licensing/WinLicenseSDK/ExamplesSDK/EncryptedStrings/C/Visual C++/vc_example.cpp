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

int WINAPI WinMain (HINSTANCE hInstance, HINSTANCE hPrevInstance,
                       PSTR szCmdLine, int iCmdShow)
{

    MessageBox(0, WLStringDecrypt("This is an encrypted string"), "WLStringDecrypt Test", MB_OK);

	return 0;

}


