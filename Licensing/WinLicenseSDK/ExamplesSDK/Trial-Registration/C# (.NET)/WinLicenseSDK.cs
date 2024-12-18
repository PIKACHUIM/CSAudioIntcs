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
    public static extern bool WLProtecCheckDebugger();

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
}