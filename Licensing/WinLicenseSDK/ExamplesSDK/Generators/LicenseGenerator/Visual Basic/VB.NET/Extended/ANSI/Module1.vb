Option Strict Off
Option Explicit On

Imports System.Text

Module Module1
    Structure SYSTEMTIME
        Dim wYear As UShort
        Dim wMonth As UShort
        Dim wDayOfWeek As UShort
        Dim wDay As UShort
        Dim wHour As UShort
        Dim wMinute As UShort
        Dim wSecond As UShort
        Dim wMilliseconds As UShort
    End Structure

    Structure sLicenseFeatures
        Dim cb As UInteger
        Dim NumDays As UInteger
        Dim NumExec As UInteger
        Dim ExpDate As SYSTEMTIME
        Dim CountryId As UInteger
        Dim Runtime As Integer
        Dim GlobalMinutes As UInteger
        Dim InstallDate As SYSTEMTIME
        Dim NetInstances As UInteger
        Dim EmbedLicenseInfoInKey As UInteger
        Dim EmbedCreationDate As UInteger
    End Structure

    Public Declare Function WLGenLicenseTextKeyEx Lib "WinlicenseSDK.dll" (ByVal LicenseHash As String, ByVal UserName As String, ByVal Organization As String, ByVal CustomData As String, ByVal MachineID As String, ByRef LicenseFeatures As sLicenseFeatures, ByVal BufferOut As StringBuilder) As Integer
    Public Declare Unicode Function WLGenLicenseTextKeyExW Lib "WinlicenseSDK.dll" (ByVal LicenseHash As String, ByVal UserName As String, ByVal Organization As String, ByVal CustomData As String, ByVal MachineID As String, ByRef LicenseFeatures As sLicenseFeatures, ByVal BufferOut As StringBuilder) As Integer
    Public Declare Function WLGenLicenseTextKey Lib "WinlicenseSDK.dll" (ByVal LicenseHash As String, ByVal UserName As String, ByVal Organization As String, ByVal CustomData As String, ByVal MachineID As String, ByVal NumDays As Integer, ByVal NumExec As Integer, ByVal NewDate As Integer, ByVal CountryId As Integer, ByVal Runtime As Integer, ByVal GlobalMinutes As Integer, ByVal BufferOut As StringBuilder) As Integer
    Public Declare Function WLGenLicenseTextKey Lib "WinlicenseSDK.dll" (ByVal LicenseHash As String, ByVal UserName As String, ByVal Organization As String, ByVal CustomData As String, ByVal MachineID As String, ByVal NumDays As Integer, ByVal NumExec As Integer, ByRef NewDate As SYSTEMTIME, ByVal CountryId As Integer, ByVal Runtime As Integer, ByVal GlobalMinutes As Integer, ByVal BufferOut As StringBuilder) As Integer
    Public Declare Unicode Function WLGenLicenseTextKeyW Lib "WinlicenseSDK.dll" (ByVal LicenseHash As String, ByVal UserName As String, ByVal Organization As String, ByVal CustomData As String, ByVal MachineID As String, ByVal NumDays As Integer, ByVal NumExec As Integer, ByVal NewDate As Integer, ByVal CountryId As Integer, ByVal Runtime As Integer, ByVal GlobalMinutes As Integer, ByVal BufferOut As StringBuilder) As Integer
    Public Declare Unicode Function WLGenLicenseTextKeyW Lib "WinlicenseSDK.dll" (ByVal LicenseHash As String, ByVal UserName As String, ByVal Organization As String, ByVal CustomData As String, ByVal MachineID As String, ByVal NumDays As Integer, ByVal NumExec As Integer, ByRef NewDate As SYSTEMTIME, ByVal CountryId As Integer, ByVal Runtime As Integer, ByVal GlobalMinutes As Integer, ByVal BufferOut As StringBuilder) As Integer

    Public Declare Function WLGenLicenseDynSmartKey Lib "WinlicenseSDK.dll" (ByVal LicenseHash As String, ByVal UserName As String, ByVal Organization As String, ByVal CustomData As String, ByVal MachineID As String, ByRef LicenseFeatures As sLicenseFeatures, ByVal BufferOut As StringBuilder) As Integer
    Public Declare Function WLGenLicenseDynSmartKeyW Lib "WinlicenseSDK.dll" (ByVal LicenseHash As String, ByVal UserName As String, ByVal Organization As String, ByVal CustomData As String, ByVal MachineID As String, ByRef LicenseFeatures As sLicenseFeatures, ByVal BufferOut As StringBuilder) As Integer
    Public Declare Function WLGenLicenseSmartKey Lib "WinlicenseSDK.dll" (ByVal LicenseHash As String, ByVal UserName As String, ByVal Organization As String, ByVal CustomData As String, ByVal MachineID As String, ByVal NumDays As Integer, ByVal NumExec As Integer, ByVal NewDate As Integer, ByVal BufferOut As StringBuilder) As Integer
    Public Declare Function WLGenLicenseSmartKey Lib "WinlicenseSDK.dll" (ByVal LicenseHash As String, ByVal UserName As String, ByVal Organization As String, ByVal CustomData As String, ByVal MachineID As String, ByVal NumDays As Integer, ByVal NumExec As Integer, ByRef NewDate As SYSTEMTIME, ByVal BufferOut As StringBuilder) As Integer
    Public Declare Unicode Function WLGenLicenseSmartKeyW Lib "WinlicenseSDK.dll" (ByVal LicenseHash As String, ByVal UserName As String, ByVal Organization As String, ByVal CustomData As String, ByVal MachineID As String, ByVal NumDays As Integer, ByVal NumExec As Integer, ByVal NewDate As Integer, ByVal BufferOut As StringBuilder) As Integer
    Public Declare Unicode Function WLGenLicenseSmartKeyW Lib "WinlicenseSDK.dll" (ByVal LicenseHash As String, ByVal UserName As String, ByVal Organization As String, ByVal CustomData As String, ByVal MachineID As String, ByVal NumDays As Integer, ByVal NumExec As Integer, ByRef NewDate As SYSTEMTIME, ByVal BufferOut As StringBuilder) As Integer

    Public Declare Function WLGenLicenseFileKeyEx Lib "WinlicenseSDK.dll" (ByVal LicenseHash As String, ByVal UserName As String, ByVal Organization As String, ByVal CustomData As String, ByVal MachineID As String, ByRef LicenseFeatures As sLicenseFeatures, ByVal BufferOut As Byte()) As Integer
    Public Declare Unicode Function WLGenLicenseFileKeyExW Lib "WinlicenseSDK.dll" (ByVal LicenseHash As String, ByVal UserName As String, ByVal Organization As String, ByVal CustomData As String, ByVal MachineID As String, ByRef LicenseFeatures As sLicenseFeatures, ByVal BufferOut As Byte()) As Integer
    Public Declare Function WLGenLicenseFileKey Lib "WinlicenseSDK.dll" (ByVal LicenseHash As String, ByVal UserName As String, ByVal Organization As String, ByVal CustomData As String, ByVal MachineID As String, ByVal NumDays As Integer, ByVal NumExec As Integer, ByVal NewDate As Integer, ByVal CountryId As Integer, ByVal Runtime As Integer, ByVal GlobalMinutes As Integer, ByVal BufferOut As Byte()) As Integer
    Public Declare Function WLGenLicenseFileKey Lib "WinlicenseSDK.dll" (ByVal LicenseHash As String, ByVal UserName As String, ByVal Organization As String, ByVal CustomData As String, ByVal MachineID As String, ByVal NumDays As Integer, ByVal NumExec As Integer, ByRef NewDate As SYSTEMTIME, ByVal CountryId As Integer, ByVal Runtime As Integer, ByVal GlobalMinutes As Integer, ByVal BufferOut As Byte()) As Integer
    Public Declare Unicode Function WLGenLicenseFileKeyW Lib "WinlicenseSDK.dll" (ByVal LicenseHash As String, ByVal UserName As String, ByVal Organization As String, ByVal CustomData As String, ByVal MachineID As String, ByVal NumDays As Integer, ByVal NumExec As Integer, ByVal NewDate As Integer, ByVal CountryId As Integer, ByVal Runtime As Integer, ByVal GlobalMinutes As Integer, ByVal BufferOut As Byte()) As Integer
    Public Declare Unicode Function WLGenLicenseFileKeyW Lib "WinlicenseSDK.dll" (ByVal LicenseHash As String, ByVal UserName As String, ByVal Organization As String, ByVal CustomData As String, ByVal MachineID As String, ByVal NumDays As Integer, ByVal NumExec As Integer, ByRef NewDate As SYSTEMTIME, ByVal CountryId As Integer, ByVal Runtime As Integer, ByVal GlobalMinutes As Integer, ByVal BufferOut As Byte()) As Integer

    Public Declare Function WLGenLicenseRegistryKeyEx Lib "WinlicenseSDK.dll" (ByVal LicenseHash As String, ByVal UserName As String, ByVal Organization As String, ByVal CustomData As String, ByVal MachineID As String, ByRef LicenseFeatures As sLicenseFeatures, ByVal RegKeyName As String, ByVal RegKeyValueName As String, ByVal BufferOut As Byte()) As Integer
    Public Declare Function WLGenLicenseRegistryKeyExW Lib "WinlicenseSDK.dll" (ByVal LicenseHash As String, ByVal UserName As String, ByVal Organization As String, ByVal CustomData As String, ByVal MachineID As String, ByRef LicenseFeatures As sLicenseFeatures, ByVal RegKeyName As String, ByVal RegKeyValueName As String, ByVal BufferOut As Byte()) As Integer
    Public Declare Function WLGenLicenseRegistryKey Lib "WinlicenseSDK.dll" (ByVal LicenseHash As String, ByVal UserName As String, ByVal Organization As String, ByVal CustomData As String, ByVal MachineID As String, ByVal NumDays As Integer, ByVal NumExec As Integer, ByVal NewDate As Integer, ByVal CountryId As Integer, ByVal Runtime As Integer, ByVal GlobalMinutes As Integer, ByVal RegKeyName As String, ByVal RegKeyValueName As String, ByVal BufferOut As Byte()) As Integer
    Public Declare Function WLGenLicenseRegistryKey Lib "WinlicenseSDK.dll" (ByVal LicenseHash As String, ByVal UserName As String, ByVal Organization As String, ByVal CustomData As String, ByVal MachineID As String, ByVal NumDays As Integer, ByVal NumExec As Integer, ByRef NewDate As SYSTEMTIME, ByVal CountryId As Integer, ByVal Runtime As Integer, ByVal GlobalMinutes As Integer, ByVal RegKeyName As String, ByVal RegKeyValueName As String, ByVal BufferOut As Byte()) As Integer
    Public Declare Function WLGenLicenseRegistryKeyW Lib "WinlicenseSDK.dll" (ByVal LicenseHash As String, ByVal UserName As String, ByVal Organization As String, ByVal CustomData As String, ByVal MachineID As String, ByVal NumDays As Integer, ByVal NumExec As Integer, ByVal NewDate As Integer, ByVal CountryId As Integer, ByVal Runtime As Integer, ByVal GlobalMinutes As Integer, ByVal RegKeyName As String, ByVal RegKeyValueName As String, ByVal BufferOut As Byte()) As Integer
    Public Declare Function WLGenLicenseRegistryKeyW Lib "WinlicenseSDK.dll" (ByVal LicenseHash As String, ByVal UserName As String, ByVal Organization As String, ByVal CustomData As String, ByVal MachineID As String, ByVal NumDays As Integer, ByVal NumExec As Integer, ByRef NewDate As SYSTEMTIME, ByVal CountryId As Integer, ByVal Runtime As Integer, ByVal GlobalMinutes As Integer, ByVal RegKeyName As String, ByVal RegKeyValueName As String, ByVal BufferOut As Byte()) As Integer

End Module