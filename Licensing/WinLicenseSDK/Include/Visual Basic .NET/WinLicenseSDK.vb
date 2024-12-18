Imports System.Text
Imports System.Runtime.InteropServices

' Required structures for the WinLicense SDK
<StructLayout(LayoutKind.Sequential)>
Public Class SystemTime
    Public wYear As Short
    Public wMonth As Short
    Public wDayOfWeek As Short
    Public wDay As Short
    Public wHour As Short
    Public wMinute As Short
    Public wSecond As Short
    Public wMilliseconds As Short
End Class

<StructLayout(LayoutKind.Sequential)>
Public Class sLicenseFeatures
    Public cb As Integer
    Public NumDays As Integer
    Public NumExec As Integer
    Public ExpDate As SystemTime
    Public CountryId As Integer
    Public Runtime As Integer
    Public GlobalMinutes As Integer
    Public InstallDate As SystemTime
    Public NetInstances As Integer
    Public EmbedLicenseInfoInKey As Integer
    Public EmbedCreationDate As Integer
End Class

Public Class WinlicenseSDK

    'Trial extension constants 
    Public Const WL_TRIAL_EXTENSION_NOT_PRESENT As Integer = 0
    Public Const WL_TRIAL_EXTENSION_APPLIED As Integer = 1
    Public Const WL_TRIAL_EXTENSION_INVALID As Integer = 2
    Public Const WL_TRIAL_EXTENSION_NO_MORE_EXTENSIONS As Integer = 3

    'Trial status constants
    Public Const WL_TRIAL_STATUS_OK As Integer = 0
    Public Const WL_TRIAL_STATUS_EXPIRED_DAYS As Integer = 1
    Public Const WL_TRIAL_STATUS_EXPIRED_EXECUTIONS As Integer = 2
    Public Const WL_TRIAL_STATUS_EXPIRED_DATE As Integer = 3
    Public Const WL_TRIAL_STATUS_EXPIRED_RUNTIME As Integer = 4
    Public Const WL_TRIAL_STATUS_EXPIRED_GLOBAL_TIME As Integer = 5
    Public Const WL_TRIAL_STATUS_INVALID_COUNTRY As Integer = 6
    Public Const WL_TRIAL_STATUS_MANIPULATED As Integer = 7

    'Registration status constants

    Public Const WL_REG_STATUS_TRIAL As Integer = 0
    Public Const WL_REG_STATUS_REGISTERED As Integer = 1
    Public Const WL_REG_STATUS_LICENSE_INVALID As Integer = 2
    Public Const WL_REG_STATUS_LICENSE_HARDWARE_ID_INVALID As Integer = 3
    Public Const WL_REG_STATUS_LICENSE_HARDWARE_ID_NO_MORE_CHANGES As Integer = 4
    Public Const WL_REG_STATUS_LICENSE_EXPIRED As Integer = 5
    Public Const WL_REG_STATUS_LICENSE_COUNTRY_INVALID As Integer = 6
    Public Const WL_REG_STATUS_LICENSE_STOLEN As Integer = 7
    Public Const WL_REG_STATUS_LICENSE_EXPIRATION_REQUIRED As Integer = 8
    Public Const WL_REG_STATUS_LICENSE_HARDWARE_ID_REQUIRED As Integer = 9
    Public Const WL_REG_STATUS_NETWORK_INSTANCES_NO_MORE_ALLOWED As Integer = 12
    Public Const WL_REG_STATUS_NETWORK_INSTANCES_NO_SERVER_RUNNING As Integer = 13
    Public Const WL_REG_STATUS_INSTALL_BEFORE_DATE_EXPIRED As Integer = 14
    Public Const WL_REG_STATUS_LICENSE_DISABLED As Integer = 15
    Public Const WL_REG_STATUS_TRIAL_NOT_SUPPORTED As Integer = 16
    Public Const WL_REG_STATUS_NETWORT_INSTANCES_CANNOT_START_SERVER As Integer = 17

    'Registration status (extended) constants
    Public Const WL_REG_EXPIRED_DAYS As Integer = 1
    Public Const WL_REG_EXTENDED_EXPIRED_EXECUTIONS As Integer = 2
    Public Const WL_REG_EXTENDED_EXPIRED_DATE As Integer = 3
    Public Const WL_REG_EXTENDED_EXPIRED_GLOBAL_TIME As Integer = 4
    Public Const WL_REG_EXTENDED_EXPIRED_RUNTIME As Integer = 5

    'Invalid key constants
    Public Const WL_MARK_KEY_STOLEN As Integer = 0
    Public Const WL_MARK_KEY_INVALID As Integer = 1

    'License restrictions constants
    Public Const WL_REG_RESTRICTION_DAYS As Integer = 1 << 0
    Public Const WL_REG_RESTRICTION_EXECUTIONS As Integer = 1 << 1
    Public Const WL_REG_RESTRICTION_DATE As Integer = 1 << 2
    Public Const WL_REG_RESTRICTION_RUNTIME As Integer = 1 << 3
    Public Const WL_REG_RESTRICTION_GLOBAL_TIME As Integer = 1 << 4
    Public Const WL_REG_RESTRICTION_COUNTRY As Integer = 1 << 5
    Public Const WL_REG_RESTRICTION_HARDWARE_ID As Integer = 1 << 6
    Public Const WL_REG_RESTRICTION_NETWORK_INSTANCES As Integer = 1 << 7
    Public Const WL_REG_RESTRICTION_INSTALL_BEFORE_DATE As Integer = 1 << 8
    Public Const WL_REG_RESTRICTION_CREATION_DATE As Integer = 1 << 9
    Public Const WL_REG_RESTRICTION_EMBED_USER_INFO As Integer = 1 << 10

    'Trial restrictions constants
    Public Const WL_TRIAL_RESTRICTION_UNLIMITED As Integer = 0
    Public Const WL_TRIAL_RESTRICTION_DAYS As Integer = 1 << 0
    Public Const WL_TRIAL_RESTRICTION_EXECUTIONS As Integer = 1 << 1
    Public Const WL_TRIAL_RESTRICTION_DATE As Integer = 1 << 2
    Public Const WL_TRIAL_RESTRICTION_RUNTIME As Integer = 1 << 3
    Public Const WL_TRIAL_RESTRICTION_GLOBAL_TIME As Integer = 1 << 4
    Public Const WL_TRIAL_RESTRICTION_COUNTRY As Integer = 1 << 5

    'Miscellaneous constants 
    Public Const WL_INFO_PERMANENT_KEY As Integer = -1
    Public Const WL_INFO_NO_LICENSE_PRESENT As Integer = -2
    Public Const WL_INFO_NO_TRIAL_DATE As Integer = -1
    Public Const WL_INFO_INVALID_COUNTER As Integer = -1

    'Hardware Machine types
    Public Const WL_HARDWARE_TYPE_PC As Integer = 0
    Public Const WL_HARDWARE_TYPE_USB As Integer = 1
    Public Const WL_HARDWARE_TYPE_CUSTOM As Integer = 2

    'Return values for WLGetLastError
    Public Const WL_ERROR_SUCCESS As Integer = 0
    Public Const WL_ERROR_NOT_MATCHING_HARDWARE_ID As Integer = 1
    Public Const WL_ERROR_NOT_MATCHING_USER_INFO As Integer = 2
    Public Const WL_ERROR_INVALID_KEY As Integer = 3
    Public Const WL_ERROR_REQUIRED_HARDWARE_ID As Integer = 4
    Public Const WL_ERROR_REQUIRED_EXPIRATION As Integer = 5
    Public Const WL_ERROR_WRONG_KEY_SIZE As Integer = 6

    ' Trial Functions

    <DllImport("WinlicenseSDK.dll", CallingConvention:=CallingConvention.StdCall)>
    Public Shared Function WLTrialCustomCounter(CounterId As Integer) As Integer
    End Function

    <DllImport("WinlicenseSDK.dll", CallingConvention:=CallingConvention.StdCall)>
    Public Shared Function WLTrialCustomCounterDec(Value As Integer,
                           CounterId As Integer) As Integer
    End Function

    <DllImport("WinlicenseSDK.dll", CallingConvention:=CallingConvention.StdCall)>
    Public Shared Function WLTrialCustomCounterInc(Value As Integer,
                           CounterId As Integer) As Integer
    End Function

    <DllImport("WinlicenseSDK.dll", CallingConvention:=CallingConvention.StdCall)>
    Public Shared Function WLTrialCustomCounterSet(Value As Integer,
                           CounterId As Integer) As Integer
    End Function

    <DllImport("WinlicenseSDK.dll", CallingConvention:=CallingConvention.StdCall)>
    Public Shared Function WLTrialDateDaysLeft() As Integer
    End Function

    <DllImport("WinlicenseSDK.dll", CallingConvention:=CallingConvention.StdCall)>
    Public Shared Function WLTrialDaysLeft() As Integer
    End Function

    <DllImport("WinlicenseSDK.dll", CallingConvention:=CallingConvention.StdCall)>
    Public Shared Function WLTrialDebugCheck() As Boolean
    End Function

    <DllImport("WinlicenseSDK.dll", CallingConvention:=CallingConvention.StdCall)>
    Public Shared Function WLTrialExecutionsLeft() As Integer
    End Function

    <DllImport("WinlicenseSDK.dll", CallingConvention:=CallingConvention.StdCall)>
    Public Shared Function WLTrialExpirationDate(ExpDate As SystemTime) As Integer
    End Function

    <DllImport("WinlicenseSDK.dll", CallingConvention:=CallingConvention.StdCall)>
    Public Shared Function WLTrialExpirationTimestamp(
            ByRef ExpDate As System.Runtime.InteropServices.ComTypes.FILETIME) As Boolean
    End Function

    <DllImport("WinlicenseSDK.dll", CallingConvention:=CallingConvention.StdCall)>
    Public Shared Function WLTrialExpireTrial() As Boolean
    End Function

    <DllImport("WinlicenseSDK.dll", CallingConvention:=CallingConvention.StdCall)>
    Public Shared Function WLTrialExtendExpiration(NumDays As Integer, NumExec As Integer,
            ExpDate As SystemTime, Runtime As Integer,
            GlobalTime As Integer) As Boolean
    End Function

    <DllImport("WinlicenseSDK.dll", CallingConvention:=CallingConvention.StdCall)>
    Public Shared Function WLTrialExtGetLevel() As Integer
    End Function

    <DllImport("WinlicenseSDK.dll", CallingConvention:=CallingConvention.StdCall)>
    Public Shared Function WLTrialExtGetStatus() As Integer
    End Function

    <DllImport("WinlicenseSDK.dll", CallingConvention:=CallingConvention.StdCall)>
    Public Shared Function WLTrialFirstRun() As Boolean
    End Function

    <DllImport("WinlicenseSDK.dll", CallingConvention:=CallingConvention.StdCall)>
    Public Shared Function WLTrialGetTrialRestrictions() As Integer
    End Function

    <DllImport("WinlicenseSDK.dll", CallingConvention:=CallingConvention.StdCall)>
    Public Shared Function WLTrialGetStatus(ByRef Reserved As Integer) As Integer
    End Function

    <DllImport("WinlicenseSDK.dll", CallingConvention:=CallingConvention.StdCall)>
    Public Shared Function WLTrialGlobalTimeLeft() As Integer
    End Function

    <DllImport("WinlicenseSDK.dll", CallingConvention:=CallingConvention.StdCall)>
    Public Shared Function WLTrialLockedCountry() As Integer
    End Function

    <DllImport("WinlicenseSDK.dll", CallingConvention:=CallingConvention.StdCall)>
    Public Shared Function WLTrialRuntimeLeft() As Integer
    End Function

    <DllImport("WinlicenseSDK.dll", CharSet:=CharSet.Ansi,
            CallingConvention:=CallingConvention.StdCall)>
    Public Shared Function WLTrialStringRead(StringName As String,
            ByRef StringValue As String) As Boolean
    End Function

    <DllImport("WinlicenseSDK.dll", CharSet:=CharSet.Unicode,
            CallingConvention:=CallingConvention.StdCall)>
    Public Shared Function WLTrialStringReadW(StringName As String,
            ByRef StringValue As String) As Boolean
    End Function

    <DllImport("WinlicenseSDK.dll", CharSet:=CharSet.Ansi,
            CallingConvention:=CallingConvention.StdCall)>
    Public Shared Function WLTrialStringWrite(StringName As String,
            StringValue As String) As Boolean
    End Function

    <DllImport("WinlicenseSDK.dll", CharSet:=CharSet.Unicode,
            CallingConvention:=CallingConvention.StdCall)>
    Public Shared Function WLTrialStringWriteW(StringName As String,
            StringValue As String) As Boolean
    End Function

    <DllImport("WinlicenseSDK.dll", CallingConvention:=CallingConvention.StdCall)>
    Public Shared Function WLTrialTotalDays() As Integer
    End Function

    <DllImport("WinlicenseSDK.dll", CallingConvention:=CallingConvention.StdCall)>
    Public Shared Function WLTrialTotalExecutions() As Integer
    End Function

    ' Registration Functions

    <DllImport("WinlicenseSDK.dll", CallingConvention:=CallingConvention.StdCall)>
    Public Shared Function WLRegActivateSoftware(ActivationKey As String,
            ByRef WinsockErrorCode As Integer, ByRef ServerResponseBuffer As String,
            SizeServerResponseBuffer As Integer) As Boolean
    End Function

    <DllImport("WinlicenseSDK.dll", CallingConvention:=CallingConvention.StdCall)>
    Public Shared Function WLRegCheckMachineLocked() As Boolean
    End Function

    <DllImport("WinlicenseSDK.dll", CallingConvention:=CallingConvention.StdCall)>
    Public Shared Function WLRegDateDaysLeft() As Integer
    End Function

    <DllImport("WinlicenseSDK.dll", CallingConvention:=CallingConvention.StdCall)>
    Public Shared Function WLRegDaysLeft() As Integer
    End Function

    <DllImport("WinlicenseSDK.dll", CallingConvention:=CallingConvention.StdCall)>
    Public Shared Function WLRegDeactivateSoftware(ActivationKey As String,
            ByRef WinsockErrorCode As Integer, ByRef ServerResponseBuffer As String,
            SizeServerResponseBuffer As Integer) As Boolean
    End Function

    <DllImport("WinlicenseSDK.dll", CallingConvention:=CallingConvention.StdCall)>
    Public Shared Function WLRegDisableCurrentKey(DisableFlags As Integer) As Boolean
    End Function

    <DllImport("WinlicenseSDK.dll", CallingConvention:=CallingConvention.StdCall)>
    Public Shared Function WLRegDisableKeyCurrentInstance() As Boolean
    End Function

    <DllImport("WinlicenseSDK.dll", CallingConvention:=CallingConvention.StdCall)>
    Public Shared Function WLRegExecutionsLeft() As Integer
    End Function

    <DllImport("WinlicenseSDK.dll", CallingConvention:=CallingConvention.StdCall)>
    Public Shared Function WLRegExpirationDate(ExpDate As SystemTime) As Integer
    End Function

    <DllImport("WinlicenseSDK.dll", CallingConvention:=CallingConvention.StdCall)>
    Public Shared Function WLRegInstallBeforeDate(InstallDate As SystemTime) As Integer
    End Function

    <DllImport("WinlicenseSDK.dll", CallingConvention:=CallingConvention.StdCall)>
    Public Shared Function WLRegExpirationTimestamp(
            ByRef ExpDate As System.Runtime.InteropServices.ComTypes.FILETIME) As Boolean
    End Function

    <DllImport("WinlicenseSDK.dll", CallingConvention:=CallingConvention.StdCall)>
    Public Shared Function WLRegFirstRun() As Boolean
    End Function

    <DllImport("WinlicenseSDK.dll", CallingConvention:=CallingConvention.StdCall)>
    Public Shared Function WLRegGetDynSmartKey(ByRef DynSmartKey As String) As Boolean
    End Function

    <DllImport("WinlicenseSDK.dll", CallingConvention:=CallingConvention.StdCall)>
    Public Shared Function WLRegGetLicenseHardwareID(ByRef HardwareId As String) As Boolean
    End Function

    <DllImport("WinlicenseSDK.dll", CallingConvention:=CallingConvention.StdCall)>
    Public Shared Function WLRegGetLicenseInfo(Name As StringBuilder,
            CompanyName As StringBuilder, CustomData As StringBuilder) As Boolean
    End Function

    <DllImport("WinlicenseSDK.dll", CharSet:=CharSet.Unicode,
            CallingConvention:=CallingConvention.StdCall)>
    Public Shared Function WLRegGetLicenseInfoW(ByRef Name As String,
            ByRef CompanyName As String, ByRef CustomData As String) As Boolean
    End Function

    <DllImport("WinlicenseSDK.dll", CallingConvention:=CallingConvention.StdCall)>
    Public Shared Function WLRegGetLicenseRestrictions() As Integer
    End Function

    <DllImport("WinlicenseSDK.dll", CallingConvention:=CallingConvention.StdCall)>
    Public Shared Function WLRegGetLicenseType() As Integer
    End Function

    <DllImport("WinlicenseSDK.dll", CallingConvention:=CallingConvention.StdCall)>
    Public Shared Function WLRegGetStatus(ByRef ExtendedInfo As Integer) As Integer
    End Function

    <DllImport("WinlicenseSDK.dll", CallingConvention:=CallingConvention.StdCall)>
    Public Shared Function WLRegGlobalTimeLeft() As Integer
    End Function

    <DllImport("WinlicenseSDK.dll", CallingConvention:=CallingConvention.StdCall)>
    Public Shared Function WLRegLicenseCreationDate(ExpDate As SystemTime) As Boolean
    End Function

    <DllImport("WinlicenseSDK.dll", CallingConvention:=CallingConvention.StdCall)>
    Public Shared Function WLRegLicenseName(ByRef FileKeyName As String,
            ByRef RegKeyName As String, ByRef RegKeyValueName As String) As Boolean
    End Function

    <DllImport("WinlicenseSDK.dll", CallingConvention:=CallingConvention.StdCall)>
    Public Shared Function WLRegLockedCountry() As Integer
    End Function

    <DllImport("WinlicenseSDK.dll", CallingConvention:=CallingConvention.StdCall)>
    Public Shared Function WLRegNetInstancesGet() As Integer
    End Function

    <DllImport("WinlicenseSDK.dll", CallingConvention:=CallingConvention.StdCall)>
    Public Shared Function WLRegNetInstancesMax() As Integer
    End Function

    <DllImport("WinlicenseSDK.dll", CallingConvention:=CallingConvention.StdCall)>
    Public Shared Function WLRegNormalKeyCheck(TextKey As String) As Boolean
    End Function

    <DllImport("WinlicenseSDK.dll", CharSet:=CharSet.Unicode,
            CallingConvention:=CallingConvention.StdCall)>
    Public Shared Function WLRegNormalKeyCheckW(TextKey As String) As Boolean
    End Function

    <DllImport("WinlicenseSDK.dll", CallingConvention:=CallingConvention.StdCall)>
    Public Shared Function WLRegNormalKeyInstallToFile(TextKey As String) As Boolean
    End Function

    <DllImport("WinlicenseSDK.dll", CharSet:=CharSet.Unicode,
            CallingConvention:=CallingConvention.StdCall)>
    Public Shared Function WLRegNormalKeyInstallToFileW(TextKey As String) As Boolean
    End Function

    <DllImport("WinlicenseSDK.dll", CallingConvention:=CallingConvention.StdCall)>
    Public Shared Function WLRegNormalKeyInstallToRegistry(TextKey As String) As Boolean
    End Function

    <DllImport("WinlicenseSDK.dll", CharSet:=CharSet.Unicode,
            CallingConvention:=CallingConvention.StdCall)>
    Public Shared Function WLRegNormalKeyInstallToRegistryW(TextKey As String) As Boolean
    End Function

    <DllImport("WinlicenseSDK.dll", CallingConvention:=CallingConvention.StdCall)>
    Public Shared Function WLRegRemoveCurrentKey() As Boolean
    End Function

    <DllImport("WinlicenseSDK.dll", CallingConvention:=CallingConvention.StdCall)>
    Public Shared Function WLRegRuntimeLeft() As Integer
    End Function

    <DllImport("WinlicenseSDK.dll", CallingConvention:=CallingConvention.StdCall)>
    Public Shared Function WLRegSmartKeyCheck(UserName As String, Company As String,
            CustomData As String, SmartKey As String) As Boolean
    End Function

    <DllImport("WinlicenseSDK.dll", CharSet:=CharSet.Unicode,
            CallingConvention:=CallingConvention.StdCall)>
    Public Shared Function WLRegSmartKeyCheckW(UserName As String, Company As String,
            CustomData As String, SmartKey As String) As Boolean
    End Function

    <DllImport("WinlicenseSDK.dll", CallingConvention:=CallingConvention.StdCall)>
    Public Shared Function WLRegSmartKeyInstallToFile(UserName As String,
            Company As String, CustomData As String, SmartKey As String) As Boolean
    End Function

    <DllImport("WinlicenseSDK.dll", CharSet:=CharSet.Unicode,
            CallingConvention:=CallingConvention.StdCall)>
    Public Shared Function WLRegSmartKeyInstallToFileW(UserName As String,
            Company As String, CustomData As String, SmartKey As String) As Boolean
    End Function

    <DllImport("WinlicenseSDK.dll", CallingConvention:=CallingConvention.StdCall)>
    Public Shared Function WLRegSmartKeyInstallToFileInFolder(UserName As String,
            Company As String, CustomData As String, SmartKey As String,
            FolderPath As String) As Boolean
    End Function

    <DllImport("WinlicenseSDK.dll", CharSet:=CharSet.Unicode,
            CallingConvention:=CallingConvention.StdCall)>
    Public Shared Function WLRegSmartKeyInstallToFileInFolderW(UserName As String,
            Company As String, CustomData As String, SmartKey As String,
            FolderPath As String) As Boolean
    End Function

    <DllImport("WinlicenseSDK.dll", CallingConvention:=CallingConvention.StdCall)>
    Public Shared Function WLRegSmartKeyInstallToRegistry(UserName As String,
            Company As String, CustomData As String, SmartKey As String) As Boolean
    End Function

    <DllImport("WinlicenseSDK.dll", CharSet:=CharSet.Unicode,
            CallingConvention:=CallingConvention.StdCall)>
    Public Shared Function WLRegSmartKeyInstallToRegistryW(UserName As String,
            Company As String, CustomData As String, SmartKey As String) As Boolean
    End Function

    <DllImport("WinlicenseSDK.dll", CallingConvention:=CallingConvention.StdCall)>
    Public Shared Function WLRegTotalDays() As Integer
    End Function

    <DllImport("WinlicenseSDK.dll", CallingConvention:=CallingConvention.StdCall)>
    Public Shared Function WLRegTotalExecutions() As Integer
    End Function

    ' Miscellaneous

    <DllImport("WinlicenseSDK.dll", CallingConvention:=CallingConvention.StdCall)>
    Public Shared Sub WLBufferCrypt(Buffer As IntPtr, BufferSize As Integer,
            Password As String)
    End Sub

    <DllImport("WinlicenseSDK.dll", CallingConvention:=CallingConvention.StdCall)>
    Public Shared Sub WLBufferDecrypt(Buffer As IntPtr, BufferSize As Integer,
            Password As String)
    End Sub

    <DllImport("WinlicenseSDK.dll", CallingConvention:=CallingConvention.StdCall)>
    Public Shared Function WLCheckVirtualPC() As Boolean
    End Function

    <DllImport("WinlicenseSDK.dll", CallingConvention:=CallingConvention.StdCall)>
    Public Shared Function WLGetCurrentCountry() As Integer
    End Function

    <DllImport("WinlicenseSDK.dll", CallingConvention:=CallingConvention.StdCall)>
    Public Shared Sub WLGetVersion(ByRef BufferVersion As String)
    End Sub

    <DllImport("WinlicenseSDK.dll", CallingConvention:=CallingConvention.StdCall)>
    Public Shared Sub WLGetProtectionDate(ProtectionDate As SystemTime)
    End Sub

    <DllImport("WinlicenseSDK.dll", CallingConvention:=CallingConvention.StdCall)>
    Public Shared Function WLHardwareCheckID(HardwareId As String) As Boolean
    End Function

    <DllImport("WinlicenseSDK.dll", CallingConvention:=CallingConvention.StdCall)>
    Public Shared Function WLHardwareGetFormattedID(NumCharsTogether As Integer,
            IsUppercase As Integer, ByRef HardwareId As StringBuilder) As Boolean
    End Function

    <DllImport("WinlicenseSDK.dll", CallingConvention:=CallingConvention.StdCall)>
    Public Shared Function WLHardwareGetID(ByRef HardwareId As StringBuilder) As Boolean
    End Function

    <DllImport("WinlicenseSDK.dll", CharSet:=CharSet.Unicode,
            CallingConvention:=CallingConvention.StdCall)>
    Public Shared Function WLHardwareGetIDW(HardwareId As StringBuilder) As Boolean
    End Function

    <DllImport("WinlicenseSDK.dll", CallingConvention:=CallingConvention.StdCall)>
    Public Shared Function WLHardwareGetIdType() As Integer
    End Function

    <DllImport("WinlicenseSDK.dll", CallingConvention:=CallingConvention.StdCall)>
    Public Shared Function WLHardwareRuntimeCheckU3() As Boolean
    End Function

    <DllImport("WinlicenseSDK.dll", CallingConvention:=CallingConvention.StdCall)>
    Public Shared Function WLPasswordCheck(UserName As String,
           Password As String) As Boolean
    End Function

    <DllImport("WinlicenseSDK.dll", CallingConvention:=CallingConvention.StdCall)>
    Public Shared Function WLProtecCheckDebugger() As Boolean
    End Function

    <DllImport("WinlicenseSDK.dll", CallingConvention:=CallingConvention.StdCall)>
    Public Shared Function WLProtectCheckCodeIntegrity() As Boolean
    End Function

    <DllImport("WinlicenseSDK.dll", CallingConvention:=CallingConvention.StdCall)>
    Public Shared Function WLRestartApplication() As Boolean
    End Function

    <DllImport("WinlicenseSDK.dll", CallingConvention:=CallingConvention.StdCall)>
    Public Shared Function WLRestartApplicationArgs(Arguments As String) As Boolean
    End Function

    <DllImport("WinlicenseSDK.dll", CallingConvention:=CallingConvention.StdCall)>
    Public Shared Sub WLSplashHide()
    End Sub

    ' License Generation Functions 
    <DllImport("WinlicenseSDK.dll", CallingConvention:=CallingConvention.StdCall)>
    Public Shared Function WLGenPassword(PassHash As String, Name As String,
            ByRef PasswordBuffer As String) As Integer
    End Function

    <DllImport("WinlicenseSDK.dll", CallingConvention:=CallingConvention.StdCall)>
    Public Shared Function WLGenTrialExtensionFileKey(TrialHash As String, Level As Integer,
             NumDays As Integer, NumExec As Integer, NewDate As Integer, NumMinutes As Integer,
             TimeRuntime As Integer,
             ByRef BufferOut As Byte()) As Integer
    End Function

    <DllImport("WinlicenseSDK.dll", CallingConvention:=CallingConvention.StdCall)>
    Public Shared Function WLGenLicenseFileKey(LicenseHash As String,
            UserName As String, Organization As String, CustomData As String,
            MachineID As String, NumDays As Integer, NumExec As Integer, NewDate As SystemTime,
            CountryId As Integer, Runtime As Integer, GlobalTime As Integer,
            ByRef BufferOut As Byte()) As Integer
    End Function

    <DllImport("WinlicenseSDK.dll", CharSet:=CharSet.Unicode,
            CallingConvention:=CallingConvention.StdCall)>
    Public Shared Function WLGenLicenseFileKeyW(LicenseHash As String,
            UserName As String, Organization As String, CustomData As String,
            MachineID As String, NumDays As Integer, NumExec As Integer, NewDate As SystemTime,
            CountryId As Integer, Runtime As Integer, GlobalTime As Integer,
            ByRef BufferOut As Byte()) As Integer
    End Function

    <DllImport("WinlicenseSDK.dll", CallingConvention:=CallingConvention.StdCall)>
    Public Shared Function WLGenLicenseRegistryKey(LicenseHash As String,
            UserName As String, Organization As String, CustomData As String,
            MachineID As String, NumDays As Integer, NumExec As Integer, NewDate As SystemTime,
            CountryId As Integer, Runtime As Integer, GlobalTime As Integer,
            RegName As String, RegValueName As String,
            ByRef BufferOut As Byte()) As Integer
    End Function

    <DllImport("WinlicenseSDK.dll", CharSet:=CharSet.Unicode,
            CallingConvention:=CallingConvention.StdCall)>
    Public Shared Function WLGenLicenseRegistryKeyW(LicenseHash As String,
            UserName As String, Organization As String, CustomData As String,
            MachineID As String, NumDays As Integer, NumExec As Integer, NewDate As SystemTime,
            CountryId As Integer, Runtime As Integer, GlobalTime As Integer,
            RegName As String, RegValueName As String,
            ByRef BufferOut As Byte()) As Integer
    End Function

    <DllImport("WinlicenseSDK.dll", CallingConvention:=CallingConvention.StdCall)>
    Public Shared Function WLGenLicenseTextKey(LicenseHash As String,
            UserName As String, Organization As String, CustomData As String,
            MachineID As String, NumDays As Integer, NumExec As Integer, NewDate As SystemTime,
            CountryId As Integer, Runtime As Integer, GlobalTime As Integer,
            ByRef BufferOut As String) As Integer
    End Function

    <DllImport("WinlicenseSDK.dll", CharSet:=CharSet.Unicode,
            CallingConvention:=CallingConvention.StdCall)>
    Public Shared Function WLGenLicenseTextKeyW(LicenseHash As String,
            UserName As String, Organization As String, CustomData As String,
            MachineID As String, NumDays As Integer, NumExec As Integer, NewDate As SystemTime,
            CountryId As Integer, Runtime As Integer, GlobalTime As Integer,
            ByRef BufferOut As String) As Integer
    End Function

    <DllImport("WinlicenseSDK.dll", CallingConvention:=CallingConvention.StdCall)>
    Public Shared Function WLGenLicenseSmartKey(LicenseHash As String,
            UserName As String, Organization As String, CustomData As String,
            MachineID As String, NumDays As Integer, NumExec As Integer,
            NewDate As SystemTime, ByRef BufferOut As String) As Integer
    End Function

    <DllImport("WinlicenseSDK.dll", CharSet:=CharSet.Unicode,
            CallingConvention:=CallingConvention.StdCall)>
    Public Shared Function WLGenLicenseSmartKeyW(LicenseHash As String,
            UserName As String, Organization As String, CustomData As String,
            MachineID As String, NumDays As Integer, NumExec As Integer,
            NewDate As SystemTime, ByRef BufferOut As String) As Integer
    End Function

    <DllImport("WinlicenseSDK.dll", CallingConvention:=CallingConvention.StdCall)>
    Public Shared Function WLGenLicenseFileKeyEx(LicenseHash As String,
            UserName As String, Organization As String, CustomData As String,
            MachineID As String, LicenseFeatures As sLicenseFeatures,
            ByRef BufferOut As Byte()) As Integer
    End Function

    <DllImport("WinlicenseSDK.dll", CharSet:=CharSet.Unicode,
            CallingConvention:=CallingConvention.StdCall)>
    Public Shared Function WLGenLicenseFileKeyExW(LicenseHash As String,
            UserName As String, Organization As String, CustomData As String,
            MachineID As String, LicenseFeatures As sLicenseFeatures,
            ByRef BufferOut As Byte()) As Integer
    End Function

    <DllImport("WinlicenseSDK.dll", CallingConvention:=CallingConvention.StdCall)>
    Public Shared Function WLGenLicenseTextKeyExLicenseHash(LicenseHash As String,
            UserName As String, Organization As String, CustomData As String,
            MachineID As String, LicenseFeatures As sLicenseFeatures,
            ByRef LicenseBuffer As String) As Integer
    End Function


    <DllImport("WinlicenseSDK.dll", CharSet:=CharSet.Unicode,
            CallingConvention:=CallingConvention.StdCall)>
    Public Shared Function WLGenLicenseTextKeyExW(LicenseHash As String,
            UserName As String, Organization As String, CustomData As String,
            MachineID As String, LicenseFeatures As sLicenseFeatures,
            ByRef LicenseBuffer As String) As Integer
    End Function

    <DllImport("WinlicenseSDK.dll", CallingConvention:=CallingConvention.StdCall)>
    Public Shared Function WLGenLicenseRegistryKey(LicenseHash As String,
            UserName As String, Organization As String, CustomData As String,
            MachineID As String, LicenseFeatures As sLicenseFeatures,
            RegName As String, RegValueName As String,
            ByRef BufferOut As Byte()) As Integer
    End Function

    <DllImport("WinlicenseSDK.dll", CharSet:=CharSet.Unicode,
            CallingConvention:=CallingConvention.StdCall)>
    Public Shared Function WLGenLicenseRegistryKeyW(LicenseHash As String,
            UserName As String, Organization As String, CustomData As String,
            MachineID As String, LicenseFeatures As sLicenseFeatures,
            RegName As String, RegValueName As String,
            ByRef BufferOut As Byte()) As Integer
    End Function

    <DllImport("WinlicenseSDK.dll", CallingConvention:=CallingConvention.StdCall)>
    Public Shared Function WLGenLicenseDynSmartKey(LicenseHash As String,
            UserName As String, Organization As String, CustomData As String,
            MachineID As String, LicenseFeatures As sLicenseFeatures,
            ByRef LicenseBuffer As String) As Integer
    End Function

    <DllImport("WinlicenseSDK.dll", CharSet:=CharSet.Unicode,
            CallingConvention:=CallingConvention.StdCall)>
    Public Shared Function WLGenLicenseDynSmartKeyW(LicenseHash As String,
            UserName As String, Organization As String, CustomData As String,
            MachineID As String, LicenseFeatures As sLicenseFeatures,
            ByRef LicenseBuffer As String) As Integer
    End Function

    <DllImport("WinlicenseSDK.dll", CallingConvention:=CallingConvention.StdCall)>
    Public Shared Function WLHardwareGetNumberUsbDrives() As Integer
    End Function

    <DllImport("WinlicenseSDK.dll", CallingConvention:=CallingConvention.StdCall)>
    Public Shared Function WLHardwareGetUsbIdAt(Index As Integer, HardwareId As StringBuilder) As Boolean
    End Function

    <DllImport("WinlicenseSDK.dll", CharSet:=CharSet.Unicode,
            CallingConvention:=CallingConvention.StdCall)>
    Public Shared Function WLHardwareGetUsbIdAtW(Index As Integer, HardwareId As StringBuilder) As Boolean
    End Function

    <DllImport("WinlicenseSDK.dll", CallingConvention:=CallingConvention.StdCall)>
    Public Shared Function WLHardwareGetUsbNameAt(Index As Integer, HardwareId As StringBuilder) As Boolean
    End Function

    <DllImport("WinlicenseSDK.dll", CharSet:=CharSet.Unicode,
            CallingConvention:=CallingConvention.StdCall)>
    Public Shared Function WLHardwareGetUsbNameAtW(Index As Integer, HardwareId As StringBuilder) As Boolean
    End Function

    <DllImport("WinlicenseSDK.dll", CallingConvention:=CallingConvention.StdCall)>
    Public Shared Function WLGetLastError() As Integer
    End Function

    <DllImport("WinlicenseSDK.dll", CallingConvention:=CallingConvention.StdCall)>
    Public Shared Function WLIsProtected() As Boolean
    End Function    

End Class