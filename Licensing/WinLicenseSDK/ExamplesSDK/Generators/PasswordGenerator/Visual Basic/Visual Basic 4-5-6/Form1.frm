VERSION 5.00
Begin VB.Form Form1 
   Caption         =   "Password Generator SDK"
   ClientHeight    =   2835
   ClientLeft      =   6360
   ClientTop       =   4485
   ClientWidth     =   5430
   LinkTopic       =   "Form1"
   ScaleHeight     =   2835
   ScaleWidth      =   5430
   StartUpPosition =   2  'CenterScreen
   Begin VB.CommandButton Command1 
      Caption         =   "Generate Password"
      Height          =   495
      Left            =   1320
      TabIndex        =   4
      Top             =   1800
      Width           =   3135
   End
   Begin VB.TextBox PasswordEdit 
      Height          =   285
      Left            =   1200
      TabIndex        =   3
      Top             =   1080
      Width           =   3735
   End
   Begin VB.TextBox NameEdit 
      Height          =   285
      Left            =   1200
      TabIndex        =   2
      Top             =   480
      Width           =   3735
   End
   Begin VB.Label Label2 
      Caption         =   "Password:"
      Height          =   375
      Left            =   360
      TabIndex        =   1
      Top             =   1200
      Width           =   855
   End
   Begin VB.Label Label1 
      Caption         =   "Name:"
      Height          =   255
      Left            =   600
      TabIndex        =   0
      Top             =   600
      Width           =   615
   End
End
Attribute VB_Name = "Form1"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Const PasswordHash = "kd7dkcdsflkjdhd63urbcmchGFFJ6sJFDBXKLAIO673jdehf7HFTGdfndj73jfkalasdj8thfgh73k394746sdfjhds6364odfjkhdsf86346kjfhf3"
Private Sub Command1_Click()

    ' Generate password for the specified user
    
    WLGenPassword PasswordHash, NameEdit.Text, GlobalBuffer
        
    ' show password on GUI
    
    PasswordEdit.Text = GlobalBuffer
      
End Sub
