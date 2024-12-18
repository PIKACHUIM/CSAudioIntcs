Attribute VB_Name = "Module1"

Public Declare Function WLGenTrialExtensionFileKey Lib "WinlicenseSDK.dll" (ByVal TrialHash As String, ByVal Level As Integer, ByVal NumDays As Integer, ByVal NumExec As Integer, NewDate As Any, ByVal NumMinutes As Integer, ByVal TimeRuntime As Integer, ByVal BufferOut As String) As Integer
Public Declare Function WLGenTrialExtensionRegistryKey Lib "WinlicenseSDK.dll" (ByVal TrialHash As String, ByVal Level As Integer, ByVal NumDays As Integer, ByVal NumExec As Integer, NewDate As Any, ByVal NumMinutes As Integer, ByVal TimeRuntime As Integer, ByVal pKeyName As String, ByVal pKeyValueName As String, ByVal BufferOut As String) As Integer
Public Declare Function WLGenPassword Lib "WinlicenseSDK.dll" (ByVal PasswordHash As String, ByVal UserName As String, ByVal BufferOut As String) As Integer
Public Declare Function WLGenLicenseTextKey Lib "WinlicenseSDK.dll" (ByVal LicenseHash As String, ByVal UserName As String, ByVal Organization As String, ByVal CustomData As String, ByVal MachineID As String, ByVal NumDays As Integer, ByVal NumExec As Integer, NewDate As Any, ByVal CountryId As Integer, ByVal Runtime As Integer, ByVal GlobalMinutes As Integer, ByVal BufferOut As String) As Integer
Public Declare Function WLGenLicenseSmartKey Lib "WinlicenseSDK.dll" (ByVal LicenseHash As String, ByVal UserName As String, ByVal Organization As String, ByVal CustomData As String, ByVal MachineID As String, ByVal NumDays As Integer, ByVal NumExec As Integer, NewDate As Any, ByVal BufferOut As String) As Integer
Public Declare Function WLGenLicenseFileKey Lib "WinlicenseSDK.dll" (ByVal LicenseHash As String, ByVal UserName As String, ByVal Organization As String, ByVal CustomData As String, ByVal MachineID As String, ByVal NumDays As Integer, ByVal NumExec As Integer, NewDate As Any, ByVal CountryId As Integer, ByVal Runtime As Integer, ByVal GlobalMinutes As Integer,ByVal BufferOut As String) As Integer
Public Declare Function WLGenLicenseRegistryKey Lib "WinlicenseSDK.dll" (ByVal LicenseHash As String, ByVal UserName As String, ByVal Organization As String, ByVal CustomData As String, ByVal MachineID As String, ByVal NumDays As Integer, ByVal NumExec As Integer, NewDate As Any, ByVal CountryId As Integer, ByVal Runtime As Integer, ByVal GlobalMinutes As Integer, ByVal RegName As String, ByVal RegValueName As String, ByVal BufferOut As String) As Integer
Public Declare Function WLRegGetStatus Lib "WinlicenseSDK.dll" (ByRef ExtendedInfo As Integer) As Integer
Public Declare Function WLTrialGetStatus Lib "WinlicenseSDK.dll" (ByRef ExtendedInfo As Integer) As Integer
Public Declare Function WLTrialExtGetStatus Lib "WinlicenseSDK.dll" () As Integer
Public Declare Function WLRegGetLicenseInfo Lib "WinlicenseSDK.dll" (ByVal UserName As String, ByVal Organization As String, ByVal CustomData As String) As Boolean
Public Declare Function WLTrialTotalDays Lib "WinlicenseSDK.dll" () As Integer
Public Declare Function WLTrialTotalExecutions Lib "WinlicenseSDK.dll" () As Integer
Public Declare Function WLTrialDaysLeft Lib "WinlicenseSDK.dll" () As Integer
Public Declare Function WLTrialExecutionsLeft Lib "WinlicenseSDK.dll" () As Integer
Public Declare Function WLTrialExpirationDate Lib "WinlicenseSDK.dll" (ExpDate As Any) As Integer
Public Declare Function WLTrialGlobalTimeLeft Lib "WinlicenseSDK.dll" () As Integer
Public Declare Function WLTrialRuntimeLeft Lib "WinlicenseSDK.dll" () As Integer
Public Declare Function WLTrialLockedCountry Lib "WinlicenseSDK.dll" () As Integer
Public Declare Function WLRegDaysLeft Lib "WinlicenseSDK.dll" () As Integer
Public Declare Function WLRegExecutionsLeft Lib "WinlicenseSDK.dll" () As Integer
Public Declare Function WLRegExpirationDate Lib "WinlicenseSDK.dll" (ExpDate As Any) As Integer
Public Declare Function WLRegTotalExecutions Lib "WinlicenseSDK.dll" () As Integer
Public Declare Function WLRegTotalDays Lib "WinlicenseSDK.dll" () As Integer
Public Declare Function WLHardwareGetID Lib "WinlicenseSDK.dll" (ByVal HardId As String) As Boolean
Public Declare Function WLHardwareCheckID Lib "WinlicenseSDK.dll" (ByVal HardId As String) As Boolean
Public Declare Function WLRegSmartKeyCheck Lib "WinlicenseSDK.dll" (ByVal UserName As String, ByVal Organization As String, ByVal CustomData As String, ByVal AsciiKey) As Boolean
Public Declare Function WLRegNormalKeyCheck Lib "WinlicenseSDK.dll" (ByVal AsciiKey As String) As Boolean
Public Declare Function WLRegNormalKeyInstallToFile Lib "WinlicenseSDK.dll" (ByVal AsciiKey As String) As Boolean
Public Declare Function WLRegNormalKeyInstallToRegistry Lib "WinlicenseSDK.dll" (ByVal AsciiKey As String) As Boolean
Public Declare Function WLRegSmartKeyInstallToFile Lib "WinlicenseSDK.dll" (ByVal UserName As String, ByVal Organization As String, ByVal CustomData As String, ByVal AsciiKey) As Boolean
Public Declare Function WLRegSmartKeyInstallToRegistry Lib "WinlicenseSDK.dll" (ByVal UserName As String, ByVal Organization As String, ByVal CustomData As String, ByVal AsciiKey) As Boolean
Public Declare Function WLLoadWinlicenseDll Lib "WinlicenseSDK.dll" () As Integer 
Public Declare Function WLTrialCustomCounterInc Lib "WinlicenseSDK.dll" (ByVal Value As Integer, ByVal CounterID As Integer) As Integer 
Public Declare Function WLTrialCustomCounterDec Lib "WinlicenseSDK.dll" (ByVal Value As Integer, ByVal CounterID As Integer) As Integer 
Public Declare Function WLTrialCustomCounter Lib "WinlicenseSDK.dll" (ByVal CounterID As Integer) As Integer 
Public Declare Function WLRestartApplication Lib "WinlicenseSDK.dll" () As Boolean 
Public Declare Function WLRegLockedCountry Lib "WinlicenseSDK.dll" () As integer 
Public Declare Function WLRegRuntimeLeft Lib "WinlicenseSDK.dll" () As integer 
Public Declare Function WLRegGlobalTimeLeft Lib "WinlicenseSDK.dll" () As integer 
Public Declare Function WLRegDisableCurrentKey Lib "WinlicenseSDK.dll" (ByVal DisableFlags As Integer) As Boolean 

Global GlobalBuffer As String * 1000
Global RegName As String * 200
Global RegCompany As String * 200
Global RegCustom As String * 200

