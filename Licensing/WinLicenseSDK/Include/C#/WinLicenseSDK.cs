using System;
using System.Text;
using System.Runtime.InteropServices;

// Required structures for the WinLicense SDK
[StructLayout(LayoutKind.Sequential)]
public class SystemTime
{
    public short wYear;
    public short wMonth;
    public short wDayOfWeek;
    public short wDay;
    public short wHour;
    public short wMinute;
    public short wSecond;
    public short wMilliseconds;
}

[StructLayout(LayoutKind.Sequential)]
public class sLicenseFeatures
{
    public int          cb;
    public int          NumDays;
    public int          NumExec;
    public SystemTime   ExpDate;
    public int          CountryId;
    public int          Runtime;
    public int          GlobalMinutes;
    public SystemTime   InstallDate;
    public int          NetInstances;
    public int          EmbedLicenseInfoInKey;
    public int          EmbedCreationDate;
}

class WinlicenseSDK
{
#if WIN64
    public const string WINLICENSE_SDK_DLL = "WinlicenseSDK64.dll";
#else
    public const string WINLICENSE_SDK_DLL = "WinlicenseSDK.dll";
#endif

    // Trial extension constants 
    public const int WL_TRIAL_EXTENSION_NOT_PRESENT         = 0;
    public const int WL_TRIAL_EXTENSION_APPLIED             = 1;
    public const int WL_TRIAL_EXTENSION_INVALID             = 2;
    public const int WL_TRIAL_EXTENSION_NO_MORE_EXTENSIONS  = 3;
    
    // Trial status constants   
    public const int WL_TRIAL_STATUS_OK                     = 0;
    public const int WL_TRIAL_STATUS_EXPIRED_DAYS           = 1;
    public const int WL_TRIAL_STATUS_EXPIRED_EXECUTIONS     = 2;
    public const int WL_TRIAL_STATUS_EXPIRED_DATE           = 3;
    public const int WL_TRIAL_STATUS_EXPIRED_RUNTIME        = 4;
    public const int WL_TRIAL_STATUS_EXPIRED_GLOBAL_TIME    = 5;
    public const int WL_TRIAL_STATUS_INVALID_COUNTRY        = 6;
    public const int WL_TRIAL_STATUS_MANIPULATED            = 7;

    // Registration status constants
    public const int WL_REG_STATUS_TRIAL                                 = 0;
    public const int WL_REG_STATUS_REGISTERED                            = 1;
    public const int WL_REG_STATUS_LICENSE_INVALID                       = 2;
    public const int WL_REG_STATUS_LICENSE_HARDWARE_ID_INVALID           = 3;
    public const int WL_REG_STATUS_LICENSE_HARDWARE_ID_NO_MORE_CHANGES   = 4;
    public const int WL_REG_STATUS_LICENSE_EXPIRED                       = 5;
    public const int WL_REG_STATUS_LICENSE_COUNTRY_INVALID               = 6;
    public const int WL_REG_STATUS_LICENSE_STOLEN                        = 7;
    public const int WL_REG_STATUS_LICENSE_EXPIRATION_REQUIRED           = 8;
    public const int WL_REG_STATUS_LICENSE_HARDWARE_ID_REQUIRED          = 9;
    public const int WL_REG_STATUS_NETWORK_INSTANCES_NO_MORE_ALLOWED     = 12;
    public const int WL_REG_STATUS_NETWORK_INSTANCES_NO_SERVER_RUNNING   = 13;
    public const int WL_REG_STATUS_INSTALL_BEFORE_DATE_EXPIRED           = 14;
    public const int WL_REG_STATUS_LICENSE_DISABLED                      = 15;
    public const int WL_REG_STATUS_TRIAL_NOT_SUPPORTED                   = 16;
    public const int WL_REG_STATUS_NETWORT_INSTANCES_CANNOT_START_SERVER = 17;

    // Registration status (extended) constants
    public const int WL_REG_EXPIRED_DAYS                    = 1;
    public const int WL_REG_EXTENDED_EXPIRED_EXECUTIONS     = 2;
    public const int WL_REG_EXTENDED_EXPIRED_DATE           = 3;
    public const int WL_REG_EXTENDED_EXPIRED_GLOBAL_TIME    = 4;
    public const int WL_REG_EXTENDED_EXPIRED_RUNTIME        = 5;
    
    // Invalid key constants    
    public const int WL_MARK_KEY_STOLEN                     = 0;
    public const int WL_MARK_KEY_INVALID                    = 1;

    // License restrictions constants
    public const int WL_REG_RESTRICTION_DAYS                = 1 << 0;
    public const int WL_REG_RESTRICTION_EXECUTIONS          = 1 << 1;
    public const int WL_REG_RESTRICTION_DATE                = 1 << 2;
    public const int WL_REG_RESTRICTION_RUNTIME             = 1 << 3;
    public const int WL_REG_RESTRICTION_GLOBAL_TIME         = 1 << 4;
    public const int WL_REG_RESTRICTION_COUNTRY             = 1 << 5;
    public const int WL_REG_RESTRICTION_HARDWARE_ID         = 1 << 6;
    public const int WL_REG_RESTRICTION_NETWORK_INSTANCES   = 1 << 7;
    public const int WL_REG_RESTRICTION_INSTALL_BEFORE_DATE = 1 << 8;
    public const int WL_REG_RESTRICTION_CREATION_DATE       = 1 << 9;
    public const int WL_REG_RESTRICTION_EMBED_USER_INFO     = 1 << 10;

    // Trial restrictions constants
    public const int WL_TRIAL_RESTRICTION_UNLIMITED         = 0;
    public const int WL_TRIAL_RESTRICTION_DAYS              = 1 << 0;
    public const int WL_TRIAL_RESTRICTION_EXECUTIONS        = 1 << 1;
    public const int WL_TRIAL_RESTRICTION_DATE              = 1 << 2;
    public const int WL_TRIAL_RESTRICTION_RUNTIME           = 1 << 3;
    public const int WL_TRIAL_RESTRICTION_GLOBAL_TIME       = 1 << 4;
    public const int WL_TRIAL_RESTRICTION_COUNTRY           = 1 << 5;

    // Miscellaneous constants 
    public const int WL_INFO_PERMANENT_KEY                  = -1;
    public const int WL_INFO_NO_LICENSE_PRESENT             = -2;
    public const int WL_INFO_NO_TRIAL_DATE                  = -1;
    public const int WL_INFO_INVALID_COUNTER                = -1;
    
    // Hardware Machine types   
    public const int WL_HARDWARE_TYPE_PC                    = 0;
    public const int WL_HARDWARE_TYPE_USB                   = 1;
    public const int WL_HARDWARE_TYPE_CUSTOM                = 2;

    // Return values for WLGetLastError
    public const int WL_ERROR_SUCCESS                       = 0;
    public const int WL_ERROR_NOT_MATCHING_HARDWARE_ID      = 1;
    public const int WL_ERROR_NOT_MATCHING_USER_INFO        = 2;
    public const int WL_ERROR_INVALID_KEY                   = 3;
    public const int WL_ERROR_REQUIRED_HARDWARE_ID          = 4;
    public const int WL_ERROR_REQUIRED_EXPIRATION           = 5;
    public const int WL_ERROR_WRONG_KEY_SIZE                = 6;


    // Trial Functions

    [DllImport(WINLICENSE_SDK_DLL, CallingConvention = CallingConvention.StdCall)]
    public static extern int WLTrialCustomCounter(int CounterId);

    [DllImport(WINLICENSE_SDK_DLL, CallingConvention = CallingConvention.StdCall)]
    public static extern int WLTrialCustomCounterDec(int Value, int CounterId);

    [DllImport(WINLICENSE_SDK_DLL, CallingConvention = CallingConvention.StdCall)]
    public static extern int WLTrialCustomCounterInc(int Value, int CounterId);

    [DllImport(WINLICENSE_SDK_DLL, CallingConvention = CallingConvention.StdCall)]
    public static extern int WLTrialCustomCounterSet(int Value, int CounterId);

    [DllImport(WINLICENSE_SDK_DLL, CallingConvention = CallingConvention.StdCall)]
    public static extern int WLTrialDateDaysLeft();

    [DllImport(WINLICENSE_SDK_DLL, CallingConvention = CallingConvention.StdCall)]
    public static extern int WLTrialDaysLeft();

    [DllImport(WINLICENSE_SDK_DLL, CallingConvention = CallingConvention.StdCall)]
    public static extern bool WLTrialDebugCheck();

    [DllImport(WINLICENSE_SDK_DLL, CallingConvention = CallingConvention.StdCall)]
    public static extern int WLTrialExecutionsLeft();

    [DllImport(WINLICENSE_SDK_DLL, CallingConvention = CallingConvention.StdCall)]
    public static extern int WLTrialExpirationDate(SystemTime ExpDate);

    [DllImport(WINLICENSE_SDK_DLL, CallingConvention = CallingConvention.StdCall)]
    public static extern bool WLTrialExpirationTimestamp(
            ref System.Runtime.InteropServices.ComTypes.FILETIME ExpDate);

    [DllImport(WINLICENSE_SDK_DLL, CallingConvention = CallingConvention.StdCall)]
    public static extern bool WLTrialExpireTrial();

    [DllImport(WINLICENSE_SDK_DLL, CallingConvention = CallingConvention.StdCall)]
    public static extern bool WLTrialExtendExpiration(int NumDays, int NumExec,
            SystemTime ExpDate, int Runtime, int GlobalTime);

    [DllImport(WINLICENSE_SDK_DLL, CallingConvention = CallingConvention.StdCall)]
    public static extern int WLTrialExtGetLevel();

    [DllImport(WINLICENSE_SDK_DLL, CallingConvention = CallingConvention.StdCall)]
    public static extern int WLTrialExtGetStatus();

    [DllImport(WINLICENSE_SDK_DLL, CallingConvention = CallingConvention.StdCall)]
    public static extern bool WLTrialFirstRun();

    [DllImport(WINLICENSE_SDK_DLL, CallingConvention = CallingConvention.StdCall)]
    public static extern int WLTrialGetTrialRestrictions();

    [DllImport(WINLICENSE_SDK_DLL, CallingConvention = CallingConvention.StdCall)]
    public static extern int WLTrialGetStatus(ref int Reserved);

    [DllImport(WINLICENSE_SDK_DLL, CallingConvention = CallingConvention.StdCall)]
    public static extern int WLTrialGlobalTimeLeft();

    [DllImport(WINLICENSE_SDK_DLL, CallingConvention = CallingConvention.StdCall)]
    public static extern int WLTrialLockedCountry();

    [DllImport(WINLICENSE_SDK_DLL, CallingConvention = CallingConvention.StdCall)]
    public static extern int WLTrialRuntimeLeft();

    [DllImport(WINLICENSE_SDK_DLL, CharSet = CharSet.Ansi, 
            CallingConvention = CallingConvention.StdCall)]
    public static extern bool WLTrialStringRead(string StringName,
            StringBuilder StringValue);

    [DllImport(WINLICENSE_SDK_DLL, CharSet = CharSet.Unicode, 
            CallingConvention = CallingConvention.StdCall)]
    public static extern bool WLTrialStringReadW(string StringName,
            StringBuilder StringValue);

    [DllImport(WINLICENSE_SDK_DLL, CharSet = CharSet.Ansi,
            CallingConvention = CallingConvention.StdCall)]
    public static extern bool WLTrialStringWrite(string StringName,
            string StringValue);

    [DllImport(WINLICENSE_SDK_DLL, CharSet = CharSet.Unicode,
            CallingConvention = CallingConvention.StdCall)]
    public static extern bool WLTrialStringWriteW(string StringName,
            string StringValue);

    [DllImport(WINLICENSE_SDK_DLL, CallingConvention = CallingConvention.StdCall)]
    public static extern int WLTrialTotalDays();

    [DllImport(WINLICENSE_SDK_DLL, CallingConvention = CallingConvention.StdCall)]
    public static extern int WLTrialTotalExecutions();

    // Registration Functions

    [DllImport(WINLICENSE_SDK_DLL, CallingConvention = CallingConvention.StdCall)]
    public static extern bool WLRegActivateSoftware(string ActivationKey, 
            ref int WinsockErrorCode, StringBuilder ServerResponseBuffer, 
            int SizeServerResponseBuffer);

    [DllImport(WINLICENSE_SDK_DLL, CallingConvention = CallingConvention.StdCall)]
    public static extern bool WLRegCheckMachineLocked();

    [DllImport(WINLICENSE_SDK_DLL, CallingConvention = CallingConvention.StdCall)]
    public static extern int WLRegDateDaysLeft();

    [DllImport(WINLICENSE_SDK_DLL, CallingConvention = CallingConvention.StdCall)]
    public static extern int WLRegDaysLeft();

    [DllImport(WINLICENSE_SDK_DLL, CallingConvention = CallingConvention.StdCall)]
    public static extern bool WLRegDeactivateSoftware(string ActivationKey,
            ref int WinsockErrorCode, StringBuilder ServerResponseBuffer,
            int SizeServerResponseBuffer);

    [DllImport(WINLICENSE_SDK_DLL, CallingConvention = CallingConvention.StdCall)]
    public static extern bool WLRegDisableCurrentKey(int DisableFlags);

    [DllImport(WINLICENSE_SDK_DLL, CallingConvention = CallingConvention.StdCall)]
    public static extern bool WLRegDisableKeyCurrentInstance();

    [DllImport(WINLICENSE_SDK_DLL, CallingConvention = CallingConvention.StdCall)]
    public static extern int WLRegExecutionsLeft();

    [DllImport(WINLICENSE_SDK_DLL, CallingConvention = CallingConvention.StdCall)]
    public static extern int WLRegExpirationDate(SystemTime ExpDate);

    [DllImport(WINLICENSE_SDK_DLL, CallingConvention = CallingConvention.StdCall)]
    public static extern int WLRegInstallBeforeDate(ref SystemTime InstallDate);

    [DllImport(WINLICENSE_SDK_DLL, CallingConvention = CallingConvention.StdCall)]
    public static extern bool WLRegExpirationTimestamp(
            ref System.Runtime.InteropServices.ComTypes.FILETIME ExpDate);

    [DllImport(WINLICENSE_SDK_DLL, CallingConvention = CallingConvention.StdCall)]
    public static extern bool WLRegFirstRun();

    [DllImport(WINLICENSE_SDK_DLL, CallingConvention = CallingConvention.StdCall)]
    public static extern bool WLRegGetDynSmartKey(StringBuilder DynSmartKey);

    [DllImport(WINLICENSE_SDK_DLL, CallingConvention = CallingConvention.StdCall)]
    public static extern bool WLRegGetLicenseHardwareID(StringBuilder HardwareId);

    [DllImport(WINLICENSE_SDK_DLL, CallingConvention = CallingConvention.StdCall)]
    public static extern bool WLRegGetLicenseInfo(StringBuilder Name,
            StringBuilder CompanyName, StringBuilder CustomData);

    [DllImport(WINLICENSE_SDK_DLL, CharSet = CharSet.Unicode, 
            CallingConvention = CallingConvention.StdCall)]
    public static extern bool WLRegGetLicenseInfoW(StringBuilder Name,
            StringBuilder CompanyName, StringBuilder CustomData);

    [DllImport(WINLICENSE_SDK_DLL, CallingConvention = CallingConvention.StdCall)]
    public static extern int WLRegGetLicenseRestrictions();

    [DllImport(WINLICENSE_SDK_DLL, CallingConvention = CallingConvention.StdCall)]
    public static extern int WLRegGetLicenseType();

    [DllImport(WINLICENSE_SDK_DLL, CallingConvention = CallingConvention.StdCall)]
    public static extern int WLRegGetStatus(ref int ExtendedInfo);

    [DllImport(WINLICENSE_SDK_DLL, CallingConvention = CallingConvention.StdCall)]
    public static extern int WLRegGlobalTimeLeft();

    [DllImport(WINLICENSE_SDK_DLL, CallingConvention = CallingConvention.StdCall)]
    public static extern bool WLRegLicenseCreationDate(SystemTime ExpDate);

    [DllImport(WINLICENSE_SDK_DLL, CallingConvention = CallingConvention.StdCall)]
    public static extern bool WLRegLicenseName(StringBuilder FileKeyName,
            StringBuilder RegKeyName, StringBuilder RegKeyValueName);

    [DllImport(WINLICENSE_SDK_DLL, CallingConvention = CallingConvention.StdCall)]
    public static extern int WLRegLockedCountry();

    [DllImport(WINLICENSE_SDK_DLL, CallingConvention = CallingConvention.StdCall)]
    public static extern int WLRegNetInstancesGet();

    [DllImport(WINLICENSE_SDK_DLL, CallingConvention = CallingConvention.StdCall)]
    public static extern int WLRegNetInstancesMax();

    [DllImport(WINLICENSE_SDK_DLL, CallingConvention = CallingConvention.StdCall)]
    public static extern bool WLRegNormalKeyCheck(string TextKey);

    [DllImport(WINLICENSE_SDK_DLL, CharSet = CharSet.Unicode, 
            CallingConvention = CallingConvention.StdCall)]
    public static extern bool WLRegNormalKeyCheckW(string TextKey);

    [DllImport(WINLICENSE_SDK_DLL, CallingConvention = CallingConvention.StdCall)]
    public static extern bool WLRegNormalKeyInstallToFile(string TextKey);

    [DllImport(WINLICENSE_SDK_DLL, CharSet = CharSet.Unicode, 
            CallingConvention = CallingConvention.StdCall)]
    public static extern bool WLRegNormalKeyInstallToFileW(string TextKey);

    [DllImport(WINLICENSE_SDK_DLL, CallingConvention = CallingConvention.StdCall)]
    public static extern bool WLRegNormalKeyInstallToRegistry(string TextKey);

    [DllImport(WINLICENSE_SDK_DLL, CharSet = CharSet.Unicode, 
            CallingConvention = CallingConvention.StdCall)]
    public static extern bool WLRegNormalKeyInstallToRegistryW(string TextKey);

    [DllImport(WINLICENSE_SDK_DLL, CallingConvention = CallingConvention.StdCall)]
    public static extern bool WLRegRemoveCurrentKey();

    [DllImport(WINLICENSE_SDK_DLL, CallingConvention = CallingConvention.StdCall)]
    public static extern int WLRegRuntimeLeft();

    [DllImport(WINLICENSE_SDK_DLL, CallingConvention = CallingConvention.StdCall)]
    public static extern bool WLRegSmartKeyCheck(string UserName, string Company,
            string CustomData, string SmartKey);

    [DllImport(WINLICENSE_SDK_DLL, CharSet = CharSet.Unicode, 
            CallingConvention = CallingConvention.StdCall)]
    public static extern bool WLRegSmartKeyCheckW(string UserName, string Company,
            string CustomData, string SmartKey);

    [DllImport(WINLICENSE_SDK_DLL, CallingConvention = CallingConvention.StdCall)]
    public static extern bool WLRegSmartKeyInstallToFile(string UserName, 
            string Company, string CustomData, string SmartKey);

    [DllImport(WINLICENSE_SDK_DLL, CharSet = CharSet.Unicode, 
            CallingConvention = CallingConvention.StdCall)]
    public static extern bool WLRegSmartKeyInstallToFileW(string UserName,
            string Company, string CustomData, string SmartKey);

    [DllImport(WINLICENSE_SDK_DLL, CallingConvention = CallingConvention.StdCall)]
    public static extern bool WLRegSmartKeyInstallToFileInFolder(string UserName,
            string Company, string CustomData, string SmartKey, string FolderPath);

    [DllImport(WINLICENSE_SDK_DLL, CharSet = CharSet.Unicode, 
            CallingConvention = CallingConvention.StdCall)]
    public static extern bool WLRegSmartKeyInstallToFileInFolderW(string UserName,
            string Company, string CustomData, string SmartKey, string FolderPath);

    [DllImport(WINLICENSE_SDK_DLL, CallingConvention = CallingConvention.StdCall)]
    public static extern bool WLRegSmartKeyInstallToRegistry(string UserName,
            string Company, string CustomData, string SmartKey);

    [DllImport(WINLICENSE_SDK_DLL, CharSet = CharSet.Unicode, 
            CallingConvention = CallingConvention.StdCall)]
    public static extern bool WLRegSmartKeyInstallToRegistryW(string UserName,
            string Company, string CustomData, string SmartKey);

    [DllImport(WINLICENSE_SDK_DLL, CallingConvention = CallingConvention.StdCall)]
    public static extern int WLRegTotalDays();

    [DllImport(WINLICENSE_SDK_DLL, CallingConvention = CallingConvention.StdCall)]
    public static extern int WLRegTotalExecutions();

    // Miscellaneous

    [DllImport(WINLICENSE_SDK_DLL, CallingConvention = CallingConvention.StdCall)]
    public static extern void WLBufferCrypt(IntPtr Buffer, int BufferSize,
            string password);

    [DllImport(WINLICENSE_SDK_DLL, CallingConvention = CallingConvention.StdCall)]
    public static extern void WLBufferDecrypt(IntPtr Buffer, int BufferSize,
            string password);

    [DllImport(WINLICENSE_SDK_DLL, CallingConvention = CallingConvention.StdCall)]
    public static extern bool WLCheckVirtualPC();

    [DllImport(WINLICENSE_SDK_DLL, CallingConvention = CallingConvention.StdCall)]
    public static extern int WLGetCurrentCountry();

    [DllImport(WINLICENSE_SDK_DLL, CallingConvention = CallingConvention.StdCall)]
    public static extern void WLGetVersion(StringBuilder BufferVersion);

    [DllImport(WINLICENSE_SDK_DLL, CallingConvention = CallingConvention.StdCall)]
    public static extern void WLGetProtectionDate(SystemTime ProtectionDate);

    [DllImport(WINLICENSE_SDK_DLL, CallingConvention = CallingConvention.StdCall)]
    public static extern bool WLHardwareCheckID(string HardwareId);

    [DllImport(WINLICENSE_SDK_DLL, CallingConvention = CallingConvention.StdCall)]
    public static extern bool WLHardwareGetFormattedID(int NumCharsTogether,
            int IsUppercase, StringBuilder HardwareId);

    [DllImport(WINLICENSE_SDK_DLL, CallingConvention = CallingConvention.StdCall)]
    public static extern bool WLHardwareGetID(StringBuilder HardwareId);

    [DllImport(WINLICENSE_SDK_DLL, CharSet = CharSet.Unicode, 
            CallingConvention = CallingConvention.StdCall)]
    public static extern bool WLHardwareGetIDW(StringBuilder HardwareId);

    [DllImport(WINLICENSE_SDK_DLL, CallingConvention = CallingConvention.StdCall)]
    public static extern int WLHardwareGetIdType();

    [DllImport(WINLICENSE_SDK_DLL, CallingConvention = CallingConvention.StdCall)]
    public static extern bool WLHardwareRuntimeCheckU3();

    [DllImport(WINLICENSE_SDK_DLL, CallingConvention = CallingConvention.StdCall)]
    public static extern bool WLPasswordCheck(string UserName, string Password);

    [DllImport(WINLICENSE_SDK_DLL, CallingConvention = CallingConvention.StdCall)]
    public static extern bool WLProtectCheckDebugger();

    [DllImport(WINLICENSE_SDK_DLL, CallingConvention = CallingConvention.StdCall)]
    public static extern bool WLProtectCheckCodeIntegrity();

    [DllImport(WINLICENSE_SDK_DLL, CallingConvention = CallingConvention.StdCall)]
    public static extern bool WLRestartApplication();

    [DllImport(WINLICENSE_SDK_DLL, CallingConvention = CallingConvention.StdCall)]
    public static extern bool WLRestartApplicationArgs(string Arguments);

    [DllImport(WINLICENSE_SDK_DLL, CallingConvention = CallingConvention.StdCall)]
    public static extern void WLSplashHide();

    // License Generation Functions 

    [DllImport(WINLICENSE_SDK_DLL, CallingConvention = CallingConvention.StdCall)]
    public static extern int WLGenPassword(string PassHash, string Name, 
            StringBuilder PasswordBuffer);

    [DllImport(WINLICENSE_SDK_DLL, CallingConvention = CallingConvention.StdCall)]
    public static extern int WLGenTrialExtensionFileKey(string TrialHash, int Level, 
            int NumDays, int NumExec, int NewDate, int NumMinutes, int TimeRuntime, 
            byte[] BufferOut);

    [DllImport(WINLICENSE_SDK_DLL, CallingConvention = CallingConvention.StdCall)]
    public static extern int WLGenLicenseFileKey(string LicenseHash, string UserName, 
            string Organization, string CustomData, string MachineID,
            int NumDays, int NumExec, SystemTime NewDate, int CountryId, 
            int Runtime, int GlobalTime, byte[] LicenseBuffer);

    [DllImport(WINLICENSE_SDK_DLL, CharSet = CharSet.Unicode, 
            CallingConvention = CallingConvention.StdCall)]
    public static extern int WLGenLicenseFileKeyW(string LicenseHash, 
            string UserName, string Organization, string CustomData, 
            string MachineID, int NumDays, int NumExec, SystemTime NewDate, 
            int CountryId, int Runtime, int GlobalTime, byte[] LicenseBuffer);

    [DllImport(WINLICENSE_SDK_DLL, CallingConvention = CallingConvention.StdCall)]
    public static extern int WLGenLicenseRegistryKey(string LicenseHash, 
            string UserName, string Organization, string CustomData, string MachineID,
            int NumDays, int NumExec, SystemTime NewDate, int CountryId, 
            int Runtime, int GlobalTime, string RegName, string RegValueName, 
            byte[] LicenseBuffer);

    [DllImport(WINLICENSE_SDK_DLL, CharSet = CharSet.Unicode, 
            CallingConvention = CallingConvention.StdCall)]
    public static extern int WLGenLicenseRegistryKeyW(string LicenseHash, 
            string UserName, string Organization, string CustomData, 
            string MachineID, int NumDays, int NumExec, SystemTime NewDate, 
            int CountryId, int Runtime, int GlobalTime, string RegName, 
            string RegValueName, byte[] LicenseBuffer);

    [DllImport(WINLICENSE_SDK_DLL, CallingConvention = CallingConvention.StdCall)]
    public static extern int WLGenLicenseTextKey(string LicenseHash, 
            string UserName, string Organization, string CustomData, string MachineID,
            int NumDays, int NumExec, SystemTime NewDate, int CountryId, int Runtime, 
            int GlobalTime, StringBuilder LicenseBuffer);

    [DllImport(WINLICENSE_SDK_DLL, CharSet = CharSet.Unicode, 
            CallingConvention = CallingConvention.StdCall)]
    public static extern int WLGenLicenseTextKeyW(string LicenseHash, 
            string UserName, string Organization, string CustomData, string MachineID,
            int NumDays, int NumExec, SystemTime NewDate, int CountryId, int Runtime, 
            int GlobalTime, StringBuilder LicenseBuffer);

    [DllImport(WINLICENSE_SDK_DLL, CallingConvention = CallingConvention.StdCall)]
    public static extern int WLGenLicenseSmartKey(string LicenseHash, 
            string UserName, string Organization, string CustomData, string MachineID,
            int NumDays, int NumExec, SystemTime NewDate, StringBuilder LicenseBuffer);

    [DllImport(WINLICENSE_SDK_DLL, CharSet = CharSet.Unicode, 
            CallingConvention = CallingConvention.StdCall)]
    public static extern int WLGenLicenseSmartKeyW(string LicenseHash, 
            string UserName, string Organization, string CustomData, string MachineID,
            int NumDays, int NumExec, SystemTime NewDate, StringBuilder LicenseBuffer);

    [DllImport(WINLICENSE_SDK_DLL, CallingConvention = CallingConvention.StdCall)]
    public static extern int WLGenLicenseFileKeyEx(string LicenseHash, 
            string UserName, string Organization, string CustomData, string MachineID,
            sLicenseFeatures LicenseFeatures, byte[] LicenseBuffer);

    [DllImport(WINLICENSE_SDK_DLL, CharSet = CharSet.Unicode, 
            CallingConvention = CallingConvention.StdCall)]
    public static extern int WLGenLicenseFileKeyExW(string LicenseHash, 
            string UserName, string Organization, string CustomData, string MachineID,
            sLicenseFeatures LicenseFeatures, byte[] LicenseBuffer);

    [DllImport(WINLICENSE_SDK_DLL, CallingConvention = CallingConvention.StdCall)]
    public static extern int WLGenLicenseTextKeyEx(string LicenseHash, 
            string UserName, string Organization, string CustomData, string MachineID,
            sLicenseFeatures LicenseFeatures, StringBuilder LicenseBuffer);

    [DllImport(WINLICENSE_SDK_DLL, CharSet = CharSet.Unicode, 
            CallingConvention = CallingConvention.StdCall)]
    public static extern int WLGenLicenseTextKeyExW(string LicenseHash, 
            string UserName, string Organization, string CustomData, string MachineID,
            sLicenseFeatures LicenseFeatures, StringBuilder LicenseBuffer);

    [DllImport(WINLICENSE_SDK_DLL, CallingConvention = CallingConvention.StdCall)]
    public static extern int WLGenLicenseRegistryKey(string LicenseHash, 
            string UserName, string Organization, string CustomData, string MachineID,
            sLicenseFeatures LicenseFeatures, string RegName, string RegValueName, 
            byte[] LicenseBuffer);

    [DllImport(WINLICENSE_SDK_DLL, CharSet = CharSet.Unicode, 
            CallingConvention = CallingConvention.StdCall)]
    public static extern int WLGenLicenseRegistryKeyW(string LicenseHash, 
            string UserName, string Organization, string CustomData, string MachineID,
            sLicenseFeatures LicenseFeatures, string RegName, string RegValueName, 
            byte[] LicenseBuffer);

    [DllImport(WINLICENSE_SDK_DLL, CallingConvention = CallingConvention.StdCall)]
    public static extern int WLGenLicenseDynSmartKey(string LicenseHash, 
            string UserName, string Organization, string CustomData, string MachineID,
            sLicenseFeatures LicenseFeatures, StringBuilder LicenseBuffer);

    [DllImport(WINLICENSE_SDK_DLL, CharSet = CharSet.Unicode, 
            CallingConvention = CallingConvention.StdCall)]
    public static extern int WLGenLicenseDynSmartKeyW(string LicenseHash, 
            string UserName, string Organization, string CustomData, string MachineID,
            sLicenseFeatures LicenseFeatures, StringBuilder LicenseBuffer);

    [DllImport(WINLICENSE_SDK_DLL, CallingConvention = CallingConvention.StdCall)]
    public static extern int WLHardwareGetNumberUsbDrives();

    [DllImport(WINLICENSE_SDK_DLL, CallingConvention = CallingConvention.StdCall)]
    public static extern bool WLHardwareGetUsbIdAt(int Index, StringBuilder HardwareId);

    [DllImport(WINLICENSE_SDK_DLL, CharSet = CharSet.Unicode, 
            CallingConvention = CallingConvention.StdCall)]
    public static extern bool WLHardwareGetUsbIdAtW(int Index, StringBuilder HardwareId);

    [DllImport(WINLICENSE_SDK_DLL, CallingConvention = CallingConvention.StdCall)]
    public static extern bool WLHardwareGetUsbNameAt(int Index, StringBuilder HardwareId);

    [DllImport(WINLICENSE_SDK_DLL, CharSet = CharSet.Unicode,
            CallingConvention = CallingConvention.StdCall)]
    public static extern bool WLHardwareGetUsbNameAtW(int Index, StringBuilder HardwareId);

    [DllImport(WINLICENSE_SDK_DLL, CallingConvention = CallingConvention.StdCall)]
    public static extern int WLGetLastError();

    [DllImport(WINLICENSE_SDK_DLL, CallingConvention = CallingConvention.StdCall)]
    public static extern bool WLIsProtected();    
}