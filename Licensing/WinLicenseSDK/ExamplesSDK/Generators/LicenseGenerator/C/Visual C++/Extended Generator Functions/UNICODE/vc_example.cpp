/****************************************************************************** 
/* Module: Example
/* Description: Shows how to use the License Generator functions
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

void GetDlgData(HWND hDlg, wchar_t* pName, wchar_t* pOrg, wchar_t* pCustom, wchar_t* pHardId,\
				int* pDays, int* pExec, SYSTEMTIME* pExpDateSysTime);


/****************************************************************************** 
/*                          Global variables
/*****************************************************************************/ 

wchar_t *LicenseHash = L"K8PYFM9t3k88C18I14cy6336Z8J26ZNi0w0Ebc65Ne422l8DIah7XF8cCtDwhN72q9Ylm48RP4ES1GvacknL38E526Q72Pe4t851"
              L"7K8L9tObA49972A8mkG8Lo714QsdC38r6E6tKc7Ha33W9612Y*eccsGA2AGAQHAABACFACCUAPI7W2VXDWYTQCVPPG26FSAVQIEG"
              L"Q57KVQEFB63QIYJUTNH2USY473ENULPJQQ3YZ22JA*eccpGBFAGAQHQABACFACCUAPI7W2VXDWYTQCVPPG26FSAVQIEGQ57KVQEF"
              L"B63QIYJUTNH2USY473ENULPJQQ3YZ22JACCR5YGTZGIOQZ7EQ5GCLHFUFBQRQT7Y4WHQ*eccefffbfffb0201000281810071139"
              L"49b5f92ea468bd89a6f2a9b846e0102ca67223c9f84ab76a9a6ebae61b9f05f881b8205111ff780d50f6638728886e80caaf"
              L"8cccfb47d0e4f26976aed699123501974b0b07add353e12141948d48cddd7fd3ef7695c451147ac14ec7e31487aa613370bc"
              L"6b6db18eae7f9d730b1fd29be1fd5c1bb2623f3d864c1c63ea7020301000102818019ff116c4325c89c9d6c17dd34e37a27b"
              L"c2dc310c0eb4515404250b0a2807dd13cb509daa122bf142557c623fb31410ba85053113f8ffeb49507a424d02cf215c3dd2"
              L"f605c9a0728e0077e046c35f0f11c919c77b0867506da799394ed935477d731b2d8041aca34ca2abbb41396eabbea1e4c903"
              L"0041efbac9aa5f3e89220910240ad0d8beb09b9d7eae79911364557805f30940354d49b22467173e28d539113eb877fbef05"
              L"eaaed0bbc1e2729c7e75969435b926a218da2bc676a11d23450afe90240a746a0b1fea88e779bc1b0a8525c7f99e1ca24f7d"
              L"8f36f6a84612967ee90ae32dda6bf366fbc45b84cad045cb2702f0b57344a6bbbb54b10e6133d12dba6700f02410062a3250"
              L"c6c37c4ad7d22cf1fbaf87c1045d527ecf35e15744778813a82d65f9f51556c150030b6fa88583b84621028f389d429295b4"
              L"4aaec6e9993dcd7bae1f90240a11f40adf2533d572c6a36d50af019e0894202b5d0fd3ab668e885cf9c5533d5bef65c7bfcc"
              L"866878d23a26dff5a5b961adb3d4be4344e27ead5612086fe1c350241005c2d70eacc39ef64cd677587f3f98631856e7a887"
              L"62b864ef43cce800e0d2d6b7de1d760a7994d2912c3441724158ddd1066e21cce5987240fd6e0dae8e2e3adfffefffe02010"
              L"002820100e4a77adf891655b655a8065a87f8ea00a3a925f1326d01ccdbf50e6835b049ce6ae6c169ed9b0faf2556dd49f34"
              L"13594c5c22c1c967d9d7bf30789a7f1b7d82ecc1e99c7e7a2b08692ab4071b9d57dfad8d061492d182240df249cc8f98bf1d"
              L"c99512fe0cc9d410927639aef8399b851202310e9ac6a350ca1cee7cd7234e04e71840f9b738467a4e023cab1689fa85ff82"
              L"b02cf0827608e860599e0d2a91a1138b6f1956bf1cad59988a599096ce75736e378702517025852096525e7aa3b45ee01bdf"
              L"bc19ac0df61c6551963dcaeeb323cf28ca026a8385a5b0ec8866bcbd8ef47dca49b414d6fadd6be82ad7b9b687c0e495fa99"
              L"7f34855db05dad38a8e5302030100010282010013ee92c0f777c64194d353b80f934cb1e75f26d7aa9cefca2326d5072d1e2"
              L"3d5b9154a42bc222d8421576f39d9f2b9904af216b4e875d82b7995dfb9f5d1b9c1b692d24ff47786d0d2b73e5731f7fa6cb"
              L"0c788a1e611536712dfca11d416423907f7324e2704d2aa37501760f4388b4bf9807ea208074817f7e758b3c322f90c7ce71"
              L"f13c35b85d0d6912914a7b09d792307443bda34391d8ed3f5f2a4bf99af309390a8dfffa443af4b7ec0200f383e1072669bc"
              L"a12f4bd963c6fce274528c4b8325cc116689a302901eedc90492c7b22b785e3f52d2e5f5e31389a2770463dedb553e1b3937"
              L"7a5012696c15f3d9fc1f0d882b89772f4ac57fd757bcc029621028180f7ccc20e620e5b08a066cc8a029c6c0e7d44cc17d8b"
              L"8a1432ce06534aae858e5c9f5c95bccf129b4bdcec21039d6d1f3a29370d173d763767d9af365e333aaff8aeb6354fe615b7"
              L"55a1f6f687f5f97eda8ff8fa0a90f9e63eba97cd779460116607476e33be83704c563e05032478992e6874df0e254ebb7d38"
              L"87e899e0d1aa1028180ec3887872db2749bf1edc0fb5d3a00296eba7e53b4165a3a8f647d1e3669c0b7aef62865e68d1cf64"
              L"59a06b810415ae67b385840c5033705a714eeca6ebd91b36d497e95408e614e8426787dd53970b94574cc9878787da9e890f"
              L"4fcf944ac0d0c9b7ba176eb7f47e12acfd2d8992f7a704aa79f740976ea3fbbba4e14d8987302818004001839e737581ce18"
              L"a074f4b0f7f67147aa1d8d8b4d255a549d2187191e2ad82ff26cb0699f61466e40999fd750c5aa48b40cd00d3bf55505dca0"
              L"42110da77d9a4472c3e171044eab98b30e2ad86645dd0125f839b2ef16a0288736a0fc940b539ec196b042aaf37088fbc7bb"
              L"34ff341297a06a380bf756e42cbd828e70941028180881be26b8f771e1da4febb8d9aa00aebd4b37c8aba5f8ec9e349a1964"
              L"1001a5d3c60f5d2115009fa150cb50665c7629b1f30ce1933ebb04e7f815e3e2224f4c91015a2ec58f0313d4e83e4358d20b"
              L"5c4eb326b99070156570a7c4bcd5fcba3df38fe75f409539459547298a102d0f13c796b6abe43dc183acd525bfd15a1dfe50"
              L"2818007c2cb4360fe83a17a6539432b2f65ee3c5e68a34a004965892f322096128301ac57a4bdf78d90e4be01b1c759195f4"
              L"42a2e7e268b2e4c6a352f22d0b37f4d9c260bb045ab6f73c672b699c65ab9bcf415047d6eb8e9860458da06d8ef8046aab05"
              L"a6cd00667fdb9890b4fd90cacc8678bfd44537bd79c372e5ffa9945bbc482";


/****************************************************************************** 
/*                               Code Section
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

    wchar_t         Name[255], Org[255], Custom[255], HardId[255], KeyBufferW[4000];
	char			KeyBuffer[4000], AuxBuffer[100];
    wchar_t         RegName[100], RegValueName[100], FullRegName[120] = {0};
    int             mDays, mExec, SizeKey;
    SYSTEMTIME      ExpDateSysTime;
    FILE*           hFileKey;
    HWND            hwnd;
	sLicenseFeatures LicenseFeatures;


    switch (message)
    {

    case WM_INITDIALOG:
        
        // set default GUI data

        SetDlgItemText(hDlg, IDC_EDITFILENAME, "License.dat");
        SetDlgItemText(hDlg, IDC_EDITHKLM, "HKLM\\");
        SetDlgItemText(hDlg, IDC_EDITKEYNAME, "Software\\Mysoft");
        SetDlgItemText(hDlg, IDC_EDITKEYVALUENAME, "reg_value");
        SetDlgItemText(hDlg, IDC_EDITREGNAME, "License.reg");
        
        return TRUE;

    case WM_COMMAND:

        if (LOWORD(wParam) == IDCANCEL) 
        {
            EndDialog(hDlg, LOWORD(wParam));
            return TRUE;
        }

        //
        // Generate File Key
        //

        if (LOWORD(wParam) == IDC_BUTTON_GENFILE)
        {
            // read information from GUI

            GetDlgData(hDlg, Name, Org, Custom, HardId, &mDays, &mExec, &ExpDateSysTime);

			// Fill LicenseFeatures structure
		
			memset(&LicenseFeatures, 0, sizeof(sLicenseFeatures));     // IMPORTANT! Reset the structure
			LicenseFeatures.cb = sizeof(sLicenseFeatures);			 // IMPORTANT! Set size of structure

			LicenseFeatures.NumDays = mDays;
			LicenseFeatures.NumExec = mExec;

			if (IsDlgButtonChecked(hDlg, IDC_CHECK_DATEENABLE))
				memcpy(&LicenseFeatures.ExpDate, &ExpDateSysTime, sizeof(SYSTEMTIME));

            // Generate specific key

            SizeKey = WLGenLicenseFileKeyExW(LicenseHash, Name[0] == 0? NULL : Name, Org[0] == 0? NULL : Org,
                      Custom[0] == 0? NULL : Custom, HardId[0] == 0? NULL : HardId, &LicenseFeatures, KeyBuffer);

            // create key in file chosen by user

            GetDlgItemText(hDlg, IDC_EDITFILENAME, AuxBuffer, 100);
            
            hFileKey = fopen(AuxBuffer,"wb+");

            fwrite(KeyBuffer, SizeKey, 1, hFileKey);

            fclose(hFileKey);

            MessageBox(0, "A License key file has been generated", "License Key Generator SDK", MB_OK);
        }

        //
        // Generate Text Key
        //

        if (LOWORD(wParam) == IDC_BUTTON_GENTEXT)
        {

            // read information from GUI

            GetDlgData(hDlg, Name, Org, Custom, HardId, &mDays, &mExec, &ExpDateSysTime);
           
			// Fill LicenseFeatures structure
		
			memset(&LicenseFeatures, 0, sizeof(sLicenseFeatures));     // IMPORTANT! Reset the structure
			LicenseFeatures.cb = sizeof(sLicenseFeatures);			 // IMPORTANT! Set size of structure

			LicenseFeatures.NumDays = mDays;
			LicenseFeatures.NumExec = mExec;

			if (IsDlgButtonChecked(hDlg, IDC_CHECK_DATEENABLE))
				memcpy(&LicenseFeatures.ExpDate, &ExpDateSysTime, sizeof(SYSTEMTIME));

            // Generate specific key

            SizeKey = WLGenLicenseTextKeyExW(LicenseHash, Name[0] == 0? NULL : Name, Org[0] == 0? NULL : Org,
                      Custom[0] == 0? NULL : Custom, HardId[0] == 0? NULL : HardId, &LicenseFeatures, KeyBufferW);

            // show text key on GUI

            SetDlgItemTextW(hDlg, IDC_EDITTEXTKEY, KeyBufferW);
        }

        //
        // Generate Dynamic SmartActivate Key
        //

        if (LOWORD(wParam) == IDC_BUTTONGENSMART)
        {

            // read information from GUI

            GetDlgData(hDlg, Name, Org, Custom, HardId, &mDays, &mExec, &ExpDateSysTime);
           
			// Fill LicenseFeatures structure
		
			memset(&LicenseFeatures, 0, sizeof(sLicenseFeatures));     // IMPORTANT! Reset the structure
			LicenseFeatures.cb = sizeof(sLicenseFeatures);			   // IMPORTANT! Set size of structure

			LicenseFeatures.NumDays = mDays;
			LicenseFeatures.NumExec = mExec;
			LicenseFeatures.EmbedLicenseInfoInKey = 0;

            // Generate specific key

            SizeKey = WLGenLicenseDynSmartKeyW(LicenseHash, Name[0] == 0? NULL : Name, Org[0] == 0? NULL : Org,
                      Custom[0] == 0? NULL : Custom, HardId[0] == 0? NULL : HardId, &LicenseFeatures, KeyBufferW);

            // show text key on GUI

            SetDlgItemTextW(hDlg, IDC_EDIT_SMARTKEY, KeyBufferW);
        }

        //
        // Generate Registry file Key
        //

        if (LOWORD(wParam) == IDC_BUTTON_GENREGFILE)
        {
             
            // read information from GUI

            GetDlgData(hDlg, Name, Org, Custom, HardId, &mDays, &mExec, &ExpDateSysTime);

            // get registry key name and value name

            GetDlgItemTextW(hDlg, IDC_EDITKEYNAME, RegName, 100);
            GetDlgItemTextW(hDlg, IDC_EDITKEYVALUENAME, RegValueName, 100);

            wcscat(FullRegName, L"HKEY_LOCAL_MACHINE\\");
            wcscat(FullRegName, RegName);

			// Fill LicenseFeatures structure
		
			memset(&LicenseFeatures, 0, sizeof(sLicenseFeatures));     // IMPORTANT! Reset the structure
			LicenseFeatures.cb = sizeof(sLicenseFeatures);			   // IMPORTANT! Set size of structure

			LicenseFeatures.NumDays = mDays;
			LicenseFeatures.NumExec = mExec;

			if (IsDlgButtonChecked(hDlg, IDC_CHECK_DATEENABLE))
				memcpy(&LicenseFeatures.ExpDate, &ExpDateSysTime, sizeof(SYSTEMTIME));

            // Generate specific key

            SizeKey = WLGenLicenseRegistryKeyExW(LicenseHash, Name[0] == 0? NULL : Name, Org[0] == 0? NULL : Org,
                      Custom[0] == 0? NULL : Custom, HardId[0] == 0? NULL : HardId, &LicenseFeatures, FullRegName, RegValueName, KeyBufferW);

            // create registry key in file chosen by user

            GetDlgItemText(hDlg, IDC_EDITREGNAME, AuxBuffer, 100);
            
            hFileKey = fopen(AuxBuffer,"wb+");

            fwrite(KeyBuffer, SizeKey, 1, hFileKey);

            fclose(hFileKey);

            MessageBox(0, "A License registry key file has been generated", "License Key Generator SDK", MB_OK);
        }

        if (LOWORD(wParam) == IDC_CHECK_DATEENABLE)
        {
       
            hwnd = GetDlgItem(hDlg, IDC_DATETIMEPICKER1);
            EnableWindow(hwnd, IsDlgButtonChecked(hDlg, IDC_CHECK_DATEENABLE));
        }
        break;
    }
    
    return FALSE;
}


/*****************************************************************************
* GetDlgData
*
*  Reads all the information inserted by the user to generate the specific key
*
* Inputs
*  hDlg: dialog handle
* 
* Outputs
*  pName: address where to store the user name
*  pOrg: address where to store the company name
*  pCustom: address where to store the custom data
*  pHardId: address where to store the hardware id
*  pDays: address where to store the number of days
*  pExec: address where to store the number of executions
*  pExpDateSysTime: address where to store the expiration date
*
* Returns
*  None
******************************************************************************/

void GetDlgData(HWND hDlg, wchar_t* pName, wchar_t* pOrg, wchar_t* pCustom, wchar_t* pHardId, int* pDays, int* pExec, SYSTEMTIME* pExpDateSysTime)
{
    char		LocalBuffer[255];
	wchar_t		LocalBufferW[255];

    // copy user name

    GetDlgItemTextW(hDlg, IDC_EDITNAME, LocalBufferW, 255);
    
    if (wcscmp(LocalBufferW, L""))
    {
        wcscpy(pName, LocalBufferW);
    }
    else
    {
        *pName = 0;
    }

    // copy Custom data

    GetDlgItemTextW(hDlg, IDC_EDITORGANIZATION, LocalBufferW, 255);
    
    if (wcscmp(LocalBufferW, L""))
    {
        wcscpy(pOrg, LocalBufferW);
    }
    else
    {
        *pOrg = 0;
    }

    // copy company name

    GetDlgItemTextW(hDlg, IDC_EDITCUSTOM, LocalBufferW, 255);
    
    if (wcscmp(LocalBufferW, L""))
    {
        wcscpy(pCustom, LocalBufferW);
    }
    else
    {
        *pCustom = 0;
    }

    // copy hardware Id

    GetDlgItemTextW(hDlg, IDC_EDITHARDID, LocalBufferW, 255);
    
    if (wcscmp(LocalBufferW, L""))
    {
        wcscpy(pHardId, LocalBufferW);
    }
    else
    {
        *pHardId = 0;
    }

    // copy number of days

    GetDlgItemText(hDlg, IDC_EDITNUMDAYS, LocalBuffer, 255);

    if (strcmp(LocalBuffer, ""))
    {
        *pDays = atoi(LocalBuffer);
    }
    else
    {
        *pDays = 0;
    }

    // copy number of executions

    GetDlgItemText(hDlg, IDC_EDITMAXEXEC, LocalBuffer, 255);

    if (strcmp(LocalBuffer, ""))
    {
        *pExec = atoi(LocalBuffer);
    }
    else
    {
        *pExec = 0;
    }

    // copy expiration date

    SendDlgItemMessage(hDlg, IDC_DATETIMEPICKER1, DTM_GETSYSTEMTIME, GDT_VALID, (LPARAM)pExpDateSysTime);
}



