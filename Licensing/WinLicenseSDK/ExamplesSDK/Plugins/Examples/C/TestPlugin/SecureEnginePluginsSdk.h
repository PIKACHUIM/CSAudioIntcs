/****************************************************************************** 
  Header: SecureEnginePluginsSdk.h
  Description:

  Author/s: Oreans Technologies  
  (c) 2018 Oreans Technologies
*****************************************************************************/ 

#pragma once

// WinLicense DLL Control constants definition

const int wdcRegistered             = 0;
const int wdcTrial                  = 1;

const int wdcNoError                = 0;
const int wdcInvalidLicense         = 2;
const int wdcInvalidHardwareLicense = 3;
const int wdcNoMoreHwdChanges       = 4;
const int wdcLicenseExpired         = 5;
const int wdcInvalidCountryLicense  = 6;
const int wdcLicenseStolen          = 7;
const int wdcWrongLicenseExp        = 8;
const int wdcWrongLicenseHardware   = 9;
const int wdcTrialExpired           = 10;
const int wdcTrialManipulated       = 11;


// definition for TWDCfunctionsArray

typedef bool (__stdcall *TWLRestartApplication)();
typedef bool (__stdcall *TWLRegNormalKeyCheck)(char* AsciiKey);
typedef bool (__stdcall *TWLRegNormalKeyInstallToFile)(char* AsciiKey);
typedef bool (__stdcall *TWLRegNormalKeyInstallToRegistry)(char* AsciiKey);
typedef bool (__stdcall *TWLRegSmartKeyCheck)(char* UserName, char* Organization, char* Custom, char* AsciiKey);
typedef bool (__stdcall *TWLRegSmartKeyInstallToFile)(char* UserName, char* Organization, char* Custom, char* AsciiKey);
typedef bool (__stdcall *TWLRegSmartKeyInstallToRegistry)(char* UserName, char* Organization, char* Custom, char* AsciiKey);


typedef struct TWDCfunctionsArray {    

	TWLRestartApplication  pfWLRestartApplication;
	TWLRegNormalKeyCheck  pfWLRegNormalKeyCheck;
	TWLRegNormalKeyInstallToFile  pfWLRegNormalKeyInstallToFile;
	TWLRegNormalKeyInstallToRegistry  pfWLRegNormalKeyInstallToRegistry;
	TWLRegSmartKeyCheck  pfWLRegSmartKeyCheck;
	TWLRegSmartKeyInstallToFile  pfWLRegSmartKeyInstallToFile;
	TWLRegSmartKeyInstallToRegistry  pfWLRegSmartKeyInstallToRegistry;
	
} TWDCfunctionsArray, *PTWDCfunctionsArray;


// definition for TWDCfunctionsArrayW

typedef bool (__stdcall *TWLRegNormalKeyCheckW)(wchar_t* AsciiKey);
typedef bool (__stdcall *TWLRegNormalKeyInstallToFileW)(wchar_t* AsciiKey);
typedef bool (__stdcall *TWLRegNormalKeyInstallToRegistryW)(wchar_t* AsciiKey);
typedef bool (__stdcall *TWLRegSmartKeyCheckW)(wchar_t* UserName, wchar_t* Organization, wchar_t* Custom, wchar_t* AsciiKey);
typedef bool (__stdcall *TWLRegSmartKeyInstallToFileW)(wchar_t* UserName, wchar_t* Organization, wchar_t* Custom, wchar_t* AsciiKey);
typedef bool (__stdcall *TWLRegSmartKeyInstallToRegistryW)(wchar_t* UserName, wchar_t* Organization, wchar_t* Custom, wchar_t* AsciiKey);

typedef struct TWDCfunctionsArrayW {    

	TWLRestartApplication  pfWLRestartApplication;
	TWLRegNormalKeyCheckW  pfWLRegNormalKeyCheck;
	TWLRegNormalKeyInstallToFileW  pfWLRegNormalKeyInstallToFile;
	TWLRegNormalKeyInstallToRegistryW  pfWLRegNormalKeyInstallToRegistry;
	TWLRegSmartKeyCheckW  pfWLRegSmartKeyCheck;
	TWLRegSmartKeyInstallToFileW  pfWLRegSmartKeyInstallToFile;
	TWLRegSmartKeyInstallToRegistryW  pfWLRegSmartKeyInstallToRegistry;
	
} TWDCfunctionsArrayW, *PTWDCfunctionsArrayW;


// definition for Trial and Registration expiration information

typedef struct TTrialExpInfo {    

    int  DaysLeft;
	int  ExecutionsLeft;
    SYSTEMTIME  DateExp;
    int GlobalTimeLeft;

} TTrialExpInfo, *PTTrialExpInfo;

typedef struct TRegExpInfo {    

    int  DaysLeft;
	int  ExecutionsLeft;
    SYSTEMTIME  DateExp;
    int GlobalTimeLeft;

} TRegExpInfo, *PTRegExpInfo;

