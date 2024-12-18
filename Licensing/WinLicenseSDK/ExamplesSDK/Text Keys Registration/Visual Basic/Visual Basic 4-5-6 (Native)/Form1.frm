VERSION 5.00
Begin VB.Form Form1 
   Caption         =   "SmartActivate SDK"
   ClientHeight    =   6060
   ClientLeft      =   60
   ClientTop       =   345
   ClientWidth     =   6330
   LinkTopic       =   "Form1"
   ScaleHeight     =   6060
   ScaleWidth      =   6330
   StartUpPosition =   2  'CenterScreen
   Begin VB.Frame Frame2 
      Caption         =   "Registration Information"
      Height          =   2055
      Left            =   360
      TabIndex        =   5
      Top             =   3720
      Width           =   5535
      Begin VB.TextBox ExtraEdit 
         Enabled         =   0   'False
         Height          =   285
         Left            =   1320
         TabIndex        =   11
         Text            =   "Unregistered"
         Top             =   1320
         Width           =   3735
      End
      Begin VB.TextBox CompanyEdit 
         Enabled         =   0   'False
         Height          =   285
         Left            =   1320
         TabIndex        =   10
         Text            =   "Unregistered"
         Top             =   840
         Width           =   3735
      End
      Begin VB.TextBox NameEdit 
         Enabled         =   0   'False
         Height          =   285
         Left            =   1320
         TabIndex        =   9
         Text            =   "Unregistered"
         Top             =   360
         Width           =   3735
      End
      Begin VB.Label Label5 
         Caption         =   "Extra Data:"
         Height          =   255
         Left            =   360
         TabIndex        =   8
         Top             =   1440
         Width           =   975
      End
      Begin VB.Label Label4 
         Caption         =   "Company:"
         Height          =   255
         Left            =   480
         TabIndex        =   7
         Top             =   960
         Width           =   735
      End
      Begin VB.Label Label3 
         Caption         =   "Name:"
         Height          =   255
         Left            =   720
         TabIndex        =   6
         Top             =   480
         Width           =   615
      End
   End
   Begin VB.CommandButton ActivateButton 
      Caption         =   "Register"
      BeginProperty Font 
         Name            =   "MS Sans Serif"
         Size            =   9.75
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   375
      Left            =   1560
      TabIndex        =   4
      Top             =   3000
      Width           =   3015
   End
   Begin VB.Frame Frame1 
      Caption         =   "Registration Key"
      Height          =   1935
      Left            =   360
      TabIndex        =   2
      Top             =   1560
      Width           =   5535
      Begin VB.TextBox TextKeyEdit 
         Height          =   885
         Left            =   360
         TabIndex        =   3
         Top             =   360
         Width           =   4815
      End
   End
   Begin VB.Label Label2 
      Caption         =   "Please, insert your personal text Key"
      BeginProperty Font 
         Name            =   "MS Sans Serif"
         Size            =   12
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H8000000D&
      Height          =   375
      Left            =   840
      TabIndex        =   1
      Top             =   840
      Width           =   4695
   End
   Begin VB.Label Label1 
      Caption         =   "This application is not registered"
      BeginProperty Font 
         Name            =   "MS Sans Serif"
         Size            =   12
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H8000000D&
      Height          =   375
      Left            =   1200
      TabIndex        =   0
      Top             =   360
      Width           =   4095
   End
End
Attribute VB_Name = "Form1"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Private Sub ActivateButton_Click()
    
    Dim Status As Integer
    
    Status = WLRegNormalKeyCheck(TextKeyEdit.Text)
  
    If Status = 0 Then
    
        MsgBox "Registration Key is INVALID", vbExclamation + vbOKOnly, "ERROR"

    Else
    
        ' if key is valid, insert the registration info in a file
        ' So, winlicense will be able to finish the registration process
        
        WLRegNormalKeyInstallToFile TextKeyEdit.Text
    
        MsgBox "Registration key is correct. Please, restart this application to finish the registration process", vbInformation + vbOKOnly, "Success"
    
    End If
    

End Sub

Private Sub Form_Load()

    Dim Status As Integer
    
    ' check if application has been registered
    
    Status = WLRegGetStatus(0)
    
    If Status = 1 Then
        
        Label1.Caption = "This application is REGISTERED"
        Label2.Caption = ""
                
        WLRegGetLicenseInfo RegName, RegCompany, RegCustom
        
        NameEdit.Text = RegName
        CompanyEdit.Text = RegCompany
        ExtraEdit.Text = RegCustom
        
    End If
    
        
    
    
    

End Sub

