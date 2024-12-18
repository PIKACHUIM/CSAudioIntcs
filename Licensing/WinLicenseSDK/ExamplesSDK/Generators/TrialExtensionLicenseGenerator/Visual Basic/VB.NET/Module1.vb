Option Strict Off
Option Explicit On 

Module Module1

    Structure SYSTEMTIME
        Dim wYear As Long
        Dim wMonth As Long
        Dim wDayOfWeek As Long
        Dim wDay As Long
        Dim wHour As Long
        Dim wMinute As Long
        Dim wSecond As Long
        Dim wMilliseconds As Long
    End Structure

    Public Declare Function WLGenTrialExtensionFileKey Lib "WinlicenseSDK.dll" (ByRef TrialHash As String, ByVal Level As Short, ByVal NumDays As Short, ByVal NumExec As Short, ByRef NewDate As Integer, ByVal NumMinutes As Short, ByVal TimeRuntime As Short, ByVal BufferOut As String) As Short
    Public Declare Function WLGenTrialExtensionFileKey Lib "WinlicenseSDK.dll" (ByRef TrialHash As String, ByVal Level As Short, ByVal NumDays As Short, ByVal NumExec As Short, ByRef NewDate As SYSTEMTIME, ByVal NumMinutes As Short, ByVal TimeRuntime As Short, ByVal BufferOut As String) As Short

    Public GlobalBuffer As New VB6.FixedLengthString(1000)
End Module