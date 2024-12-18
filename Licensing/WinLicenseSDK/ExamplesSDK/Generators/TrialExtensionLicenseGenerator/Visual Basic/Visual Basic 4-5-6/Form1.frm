VERSION 5.00
Begin VB.Form Form1 
   Caption         =   "Trial Extension SDK"
   ClientHeight    =   2565
   ClientLeft      =   5490
   ClientTop       =   4605
   ClientWidth     =   5910
   LinkTopic       =   "Form1"
   ScaleHeight     =   2565
   ScaleWidth      =   5910
   StartUpPosition =   2  'CenterScreen
   Begin VB.CommandButton Command1 
      Caption         =   "Generate Trial Extension Key"
      Height          =   495
      Left            =   1680
      TabIndex        =   0
      Top             =   1680
      Width           =   2415
   End
   Begin VB.Label Label1 
      Caption         =   "This program generates an extension trial key of 3 days and 5 executions extension"
      BeginProperty Font 
         Name            =   "MS Sans Serif"
         Size            =   12
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   855
      Left            =   360
      TabIndex        =   1
      Top             =   360
      Width           =   5295
   End
End
Attribute VB_Name = "Form1"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Const TrialHash = "432ab78bd7ar324ghjd87feg63vksyb375am35fj50abloqnxc543jd73hrgf5289dhf73hjdfjhgd783hjgsdmvbaismvhsm76354BfhBFHSgd7634"

Private Sub Command1_Click()

    ' Generate a trial extension key with 3 days and 5 executions extended
    
    WLGenTrialExtensionFileKey TrialHash, 1, 3, 5, 0, 0, 0, GlobalBuffer
    
     Open App.Path & "\" & "extendkey.dat" For Output As #1
        Print #1, GlobalBuffer
    Close #1

    MsgBox "An extended trial key has been generated", vbInformation + vbOKOnly, "WinLicense SDK"


End Sub
