Option Strict Off
Option Explicit On
Module Module1
    Public Declare Function GetEnvironmentVariable Lib "kernel32.dll" Alias "GetEnvironmentVariableA" (ByVal lpName As String, ByVal lpBuffer As System.Text.StringBuilder, ByVal nSize As Integer) As Integer
	
	Public Declare Function WLLoadWinlicenseDll Lib "WinlicenseSDK.dll" () As Short
End Module