Option Strict Off
Option Explicit On
Module Module1

    Public Declare Function WLGenPassword Lib "WinlicenseSDK.dll" (ByVal PasswordHash As String, ByVal UserName As String, ByVal BufferOut As String) As Short

    Public GlobalBuffer As New VB6.FixedLengthString(1000)
End Module