Option Strict Off
Option Explicit On

Imports System.Text

Module Module1
    Structure SYSTEMTIME
        Dim wYear As Short
        Dim wMonth As Short
        Dim wDayOfWeek As Short
        Dim wDay As Short
        Dim wHour As Short
        Dim wMinute As Short
        Dim wSecond As Short
        Dim wMilliseconds As Short
    End Structure

    Structure sLicenseFeatures
        Dim cb As Integer
        Dim NumDays As Integer
        Dim NumExec As Integer
        Dim ExpDate As SYSTEMTIME
        Dim CountryId As Integer
        Dim Runtime As Integer
        Dim GlobalMinutes As Integer
        Dim InstallDate As SYSTEMTIME
        Dim NetInstances As Integer
        Dim EmbedLicenseInfoInKey As Integer
        Dim EmbedCreationDate As Integer
    End Structure

    Public Declare Function WLGenLicenseTextKey Lib "WinlicenseSDK.dll" (ByVal LicenseHash As String, ByVal UserName As String, ByVal Organization As String, ByVal CustomData As String, ByVal MachineID As String, ByVal NumDays As Integer, ByVal NumExec As Integer, ByVal NewDate As Integer, ByVal CountryId As Integer, ByVal Runtime As Integer, ByVal GlobalMinutes As Integer, ByVal BufferOut As StringBuilder) As Integer
    Public Declare Function WLGenLicenseTextKey Lib "WinlicenseSDK.dll" (ByVal LicenseHash As String, ByVal UserName As String, ByVal Organization As String, ByVal CustomData As String, ByVal MachineID As String, ByVal NumDays As Integer, ByVal NumExec As Integer, ByRef NewDate As SYSTEMTIME, ByVal CountryId As Integer, ByVal Runtime As Integer, ByVal GlobalMinutes As Integer, ByVal BufferOut As StringBuilder) As Integer
    Public Declare Unicode Function WLGenLicenseTextKeyW Lib "WinlicenseSDK.dll" (ByVal LicenseHash As String, ByVal UserName As String, ByVal Organization As String, ByVal CustomData As String, ByVal MachineID As String, ByVal NumDays As Integer, ByVal NumExec As Integer, ByVal NewDate As Integer, ByVal CountryId As Integer, ByVal Runtime As Integer, ByVal GlobalMinutes As Integer, ByVal BufferOut As StringBuilder) As Integer
    Public Declare Unicode Function WLGenLicenseTextKeyW Lib "WinlicenseSDK.dll" (ByVal LicenseHash As String, ByVal UserName As String, ByVal Organization As String, ByVal CustomData As String, ByVal MachineID As String, ByVal NumDays As Integer, ByVal NumExec As Integer, ByRef NewDate As SYSTEMTIME, ByVal CountryId As Integer, ByVal Runtime As Integer, ByVal GlobalMinutes As Integer, ByVal BufferOut As StringBuilder) As Integer

    Public Declare Function WLGenLicenseSmartKey Lib "WinlicenseSDK.dll" (ByVal LicenseHash As String, ByVal UserName As String, ByVal Organization As String, ByVal CustomData As String, ByVal MachineID As String, ByVal NumDays As Integer, ByVal NumExec As Integer, ByVal NewDate As Integer, ByVal BufferOut As StringBuilder) As Integer
    Public Declare Function WLGenLicenseSmartKey Lib "WinlicenseSDK.dll" (ByVal LicenseHash As String, ByVal UserName As String, ByVal Organization As String, ByVal CustomData As String, ByVal MachineID As String, ByVal NumDays As Integer, ByVal NumExec As Integer, ByRef NewDate As SYSTEMTIME, ByVal BufferOut As StringBuilder) As Integer
    Public Declare Unicode Function WLGenLicenseSmartKeyW Lib "WinlicenseSDK.dll" (ByVal LicenseHash As String, ByVal UserName As String, ByVal Organization As String, ByVal CustomData As String, ByVal MachineID As String, ByVal NumDays As Integer, ByVal NumExec As Integer, ByVal NewDate As Integer, ByVal BufferOut As StringBuilder) As Integer
    Public Declare Unicode Function WLGenLicenseSmartKeyW Lib "WinlicenseSDK.dll" (ByVal LicenseHash As String, ByVal UserName As String, ByVal Organization As String, ByVal CustomData As String, ByVal MachineID As String, ByVal NumDays As Integer, ByVal NumExec As Integer, ByRef NewDate As SYSTEMTIME, ByVal BufferOut As StringBuilder) As Integer

    Public Declare Function WLGenLicenseFileKey Lib "WinlicenseSDK.dll" (ByVal LicenseHash As String, ByVal UserName As String, ByVal Organization As String, ByVal CustomData As String, ByVal MachineID As String, ByVal NumDays As Integer, ByVal NumExec As Integer, ByVal NewDate As Integer, ByVal CountryId As Integer, ByVal Runtime As Integer, ByVal GlobalMinutes As Integer, ByVal BufferOut As Byte()) As Integer
    Public Declare Function WLGenLicenseFileKey Lib "WinlicenseSDK.dll" (ByVal LicenseHash As String, ByVal UserName As String, ByVal Organization As String, ByVal CustomData As String, ByVal MachineID As String, ByVal NumDays As Integer, ByVal NumExec As Integer, ByRef NewDate As SYSTEMTIME, ByVal CountryId As Integer, ByVal Runtime As Integer, ByVal GlobalMinutes As Integer, ByVal BufferOut As Byte()) As Integer
    Public Declare Unicode Function WLGenLicenseFileKeyW Lib "WinlicenseSDK.dll" (ByVal LicenseHash As String, ByVal UserName As String, ByVal Organization As String, ByVal CustomData As String, ByVal MachineID As String, ByVal NumDays As Integer, ByVal NumExec As Integer, ByVal NewDate As Integer, ByVal CountryId As Integer, ByVal Runtime As Integer, ByVal GlobalMinutes As Integer, ByVal BufferOut As Byte()) As Integer
    Public Declare Unicode Function WLGenLicenseFileKeyW Lib "WinlicenseSDK.dll" (ByVal LicenseHash As String, ByVal UserName As String, ByVal Organization As String, ByVal CustomData As String, ByVal MachineID As String, ByVal NumDays As Integer, ByVal NumExec As Integer, ByRef NewDate As SYSTEMTIME, ByVal CountryId As Integer, ByVal Runtime As Integer, ByVal GlobalMinutes As Integer, ByVal BufferOut As Byte()) As Integer

    Public Declare Function WLGenLicenseRegistryKey Lib "WinlicenseSDK.dll" (ByVal LicenseHash As String, ByVal UserName As String, ByVal Organization As String, ByVal CustomData As String, ByVal MachineID As String, ByVal NumDays As Integer, ByVal NumExec As Integer, ByVal NewDate As Integer, ByVal CountryId As Integer, ByVal Runtime As Integer, ByVal GlobalMinutes As Integer, ByVal RegKeyName As String, ByVal RegKeyValueName As String, ByVal BufferOut As Byte()) As Integer
    Public Declare Function WLGenLicenseRegistryKey Lib "WinlicenseSDK.dll" (ByVal LicenseHash As String, ByVal UserName As String, ByVal Organization As String, ByVal CustomData As String, ByVal MachineID As String, ByVal NumDays As Integer, ByVal NumExec As Integer, ByRef NewDate As SYSTEMTIME, ByVal CountryId As Integer, ByVal Runtime As Integer, ByVal GlobalMinutes As Integer, ByVal RegKeyName As String, ByVal RegKeyValueName As String, ByVal BufferOut As Byte()) As Integer
    Public Declare Function WLGenLicenseRegistryKeyW Lib "WinlicenseSDK.dll" (ByVal LicenseHash As String, ByVal UserName As String, ByVal Organization As String, ByVal CustomData As String, ByVal MachineID As String, ByVal NumDays As Integer, ByVal NumExec As Integer, ByVal NewDate As Integer, ByVal CountryId As Integer, ByVal Runtime As Integer, ByVal GlobalMinutes As Integer, ByVal RegKeyName As String, ByVal RegKeyValueName As String, ByVal BufferOut As Byte()) As Integer
    Public Declare Function WLGenLicenseRegistryKeyW Lib "WinlicenseSDK.dll" (ByVal LicenseHash As String, ByVal UserName As String, ByVal Organization As String, ByVal CustomData As String, ByVal MachineID As String, ByVal NumDays As Integer, ByVal NumExec As Integer, ByRef NewDate As SYSTEMTIME, ByVal CountryId As Integer, ByVal Runtime As Integer, ByVal GlobalMinutes As Integer, ByVal RegKeyName As String, ByVal RegKeyValueName As String, ByVal BufferOut As Byte()) As Integer

End Module