VERSION 5.00
Begin VB.Form Form1 
   Caption         =   "SmartActivate SDK"
   ClientHeight    =   5130
   ClientLeft      =   60
   ClientTop       =   345
   ClientWidth     =   7260
   LinkTopic       =   "Form1"
   ScaleHeight     =   5130
   ScaleWidth      =   7260
   StartUpPosition =   2  'CenterScreen
   Begin VB.CommandButton ActivateButton 
      Caption         =   "Activate"
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
      Left            =   2040
      TabIndex        =   11
      Top             =   4440
      Width           =   3015
   End
   Begin VB.Frame Frame1 
      Caption         =   "Activation Informaiton"
      Height          =   2655
      Left            =   360
      TabIndex        =   2
      Top             =   1560
      Width           =   6495
      Begin VB.TextBox ActivationEdit 
         BeginProperty Font 
            Name            =   "MS Sans Serif"
            Size            =   9.75
            Charset         =   0
            Weight          =   400
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   375
         Left            =   360
         TabIndex        =   10
         Top             =   2040
         Width           =   5655
      End
      Begin VB.TextBox CustomEdit 
         Height          =   285
         Left            =   1440
         TabIndex        =   8
         Top             =   1080
         Width           =   4455
      End
      Begin VB.TextBox CompanyEdit 
         Height          =   285
         Left            =   4080
         TabIndex        =   6
         Top             =   600
         Width           =   1815
      End
      Begin VB.TextBox NameEdit 
         Height          =   285
         Left            =   1080
         TabIndex        =   4
         Top             =   600
         Width           =   1815
      End
      Begin VB.Label Label6 
         Caption         =   "Activation Code"
         BeginProperty Font 
            Name            =   "MS Sans Serif"
            Size            =   12
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   255
         Left            =   2280
         TabIndex        =   9
         Top             =   1680
         Width           =   2055
      End
      Begin VB.Label Label5 
         Caption         =   "Extra Data:"
         Height          =   255
         Left            =   480
         TabIndex        =   7
         Top             =   1200
         Width           =   855
      End
      Begin VB.Label Label4 
         Caption         =   "Company:"
         Height          =   255
         Left            =   3240
         TabIndex        =   5
         Top             =   600
         Width           =   855
      End
      Begin VB.Label Label3 
         Caption         =   "Name:"
         Height          =   255
         Left            =   480
         TabIndex        =   3
         Top             =   600
         Width           =   735
      End
   End
   Begin VB.Label Label2 
      Caption         =   "Please, insert your personal activation code"
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
      Width           =   5535
   End
   Begin VB.Label Label1 
      Caption         =   "This application nees to be Activated"
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
      Width           =   5175
   End
End
Attribute VB_Name = "Form1"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Private Sub ActivateButton_Click()
    
    Dim Status As Boolean
   
    Status = WLRegSmartKeyCheck(NameEdit.Text, CompanyEdit.Text, CustomEdit.Text, ActivationEdit.Text)
 
    If Status = 0 Then
    
        MsgBox "Activation code is INVALID", vbExclamation + vbOKOnly, "ERROR"

    Else
    
        ' if activation is valid, insert the registration info in a file
        ' So, winlicense will be able to finish the registration process
        
        WLRegSmartKeyInstallToFile NameEdit.Text, CompanyEdit.Text, CustomEdit.Text, ActivationEdit.Text
    
        MsgBox "Activation code is correct. Please, restart this application to finish the registration process", vbInformation + vbOKOnly, "Success"
    
    End If
    

End Sub

Private Sub Form_Load()

    Dim Status As Integer
    
    ' check if application has been activated (registered)
    
    Status = WLRegGetStatus(0)
    
    If Status = 1 Then
        
        Label1.Caption = "This application has been ACTIVATED"
        Label2.Caption = ""
        Frame1.Visible = False
        ActivateButton.Visible = False
        
    End If
    
        
    
    
    

End Sub
