VERSION 5.00
Begin VB.Form Form1 
   Caption         =   "License Key Generator SDK"
   ClientHeight    =   9000
   ClientLeft      =   60
   ClientTop       =   345
   ClientWidth     =   7710
   LinkTopic       =   "Form1"
   ScaleHeight     =   9000
   ScaleWidth      =   7710
   StartUpPosition =   2  'CenterScreen
   Begin VB.Frame Frame3 
      Caption         =   "Type Generated Key"
      Height          =   4215
      Left            =   480
      TabIndex        =   14
      Top             =   4440
      Width           =   6615
      Begin VB.Frame Frame7 
         Caption         =   "SmartActivate Key"
         Height          =   1095
         Left            =   3240
         TabIndex        =   31
         Top             =   2880
         Width           =   2895
         Begin VB.TextBox SmartKeyEdit 
            Height          =   285
            Left            =   120
            TabIndex        =   33
            Top             =   720
            Width           =   2655
         End
         Begin VB.CommandButton GenerateSmartButton 
            Caption         =   "Generate Smart Key"
            Height          =   255
            Left            =   480
            TabIndex        =   32
            Top             =   360
            Width           =   1815
         End
      End
      Begin VB.Frame Frame6 
         Caption         =   "Registry Key"
         Height          =   2295
         Left            =   3240
         TabIndex        =   22
         Top             =   465
         Width           =   2895
         Begin VB.CommandButton GenerateRegButton 
            Caption         =   "Generate Reg Key File"
            Height          =   315
            Left            =   600
            TabIndex        =   30
            Top             =   1800
            Width           =   1815
         End
         Begin VB.TextBox RegNameEdit 
            Height          =   285
            Left            =   1440
            TabIndex        =   29
            Text            =   "License.reg"
            Top             =   1200
            Width           =   1335
         End
         Begin VB.TextBox KeyValueNameEdit 
            Height          =   285
            Left            =   1560
            TabIndex        =   27
            Text            =   "reg_value"
            Top             =   840
            Width           =   1215
         End
         Begin VB.TextBox KeyNameEdit 
            Height          =   300
            Left            =   1665
            TabIndex        =   25
            Text            =   "\Software\MyApp"
            Top             =   360
            Width           =   1080
         End
         Begin VB.TextBox Text9 
            Enabled         =   0   'False
            Height          =   300
            Left            =   1080
            TabIndex        =   24
            Text            =   "HKLM"
            Top             =   360
            Width           =   585
         End
         Begin VB.Label Label10 
            Caption         =   "File name (*.reg)"
            Height          =   255
            Left            =   240
            TabIndex        =   28
            Top             =   1320
            Width           =   1215
         End
         Begin VB.Label Label9 
            Caption         =   "Key Value Name:"
            Height          =   255
            Left            =   240
            TabIndex        =   26
            Top             =   840
            Width           =   1335
         End
         Begin VB.Label Label8 
            Caption         =   "KeyName:"
            Height          =   255
            Left            =   240
            TabIndex        =   23
            Top             =   480
            Width           =   735
         End
      End
      Begin VB.Frame Frame5 
         Caption         =   "Text Key"
         Height          =   1935
         Left            =   360
         TabIndex        =   19
         Top             =   2040
         Width           =   2655
         Begin VB.TextBox TextKeyEdit 
            Height          =   1095
            Left            =   360
            MultiLine       =   -1  'True
            ScrollBars      =   2  'Vertical
            TabIndex        =   21
            Top             =   720
            Width           =   1935
         End
         Begin VB.CommandButton GenerateTextButton 
            Caption         =   "Generate Text Key"
            Height          =   255
            Left            =   480
            TabIndex        =   20
            Top             =   360
            Width           =   1695
         End
      End
      Begin VB.Frame Frame4 
         Caption         =   "File Key"
         Height          =   1335
         Left            =   360
         TabIndex        =   15
         Top             =   480
         Width           =   2655
         Begin VB.CommandButton GenerateFileButton 
            Caption         =   "Generate File Key"
            Height          =   255
            Left            =   480
            TabIndex        =   18
            Top             =   840
            Width           =   1695
         End
         Begin VB.TextBox FileNameEdit 
            Height          =   285
            Left            =   1080
            TabIndex        =   17
            Text            =   "License.dat"
            Top             =   240
            Width           =   1455
         End
         Begin VB.Label Label7 
            Caption         =   "File Name:"
            Height          =   255
            Left            =   240
            TabIndex        =   16
            Top             =   360
            Width           =   855
         End
      End
   End
   Begin VB.Frame Frame2 
      Caption         =   "Key Expiration Info"
      Height          =   1215
      Left            =   480
      TabIndex        =   9
      Top             =   3000
      Width           =   6615
      Begin VB.TextBox MaxExecEdit 
         BeginProperty DataFormat 
            Type            =   1
            Format          =   "0"
            HaveTrueFalseNull=   0
            FirstDayOfWeek  =   0
            FirstWeekOfYear =   0
            LCID            =   1033
            SubFormatType   =   1
         EndProperty
         Height          =   285
         Left            =   4560
         TabIndex        =   13
         Top             =   360
         Width           =   1455
      End
      Begin VB.TextBox NumDaysEdit 
         Height          =   285
         Left            =   1560
         TabIndex        =   12
         Top             =   360
         Width           =   1455
      End
      Begin VB.Label Label6 
         Caption         =   "Max. Executions:"
         Height          =   255
         Left            =   3240
         TabIndex        =   11
         Top             =   480
         Width           =   1335
      End
      Begin VB.Label Label5 
         Caption         =   "Number Days:"
         Height          =   255
         Left            =   360
         TabIndex        =   10
         Top             =   480
         Width           =   1095
      End
   End
   Begin VB.Frame Frame1 
      Caption         =   "User's Information"
      Height          =   2295
      Left            =   480
      TabIndex        =   0
      Top             =   480
      Width           =   6615
      Begin VB.TextBox CustomName 
         Height          =   1335
         Left            =   4680
         MultiLine       =   -1  'True
         TabIndex        =   8
         Top             =   600
         Width           =   1575
      End
      Begin VB.TextBox HardIdName 
         Height          =   285
         Left            =   1440
         TabIndex        =   7
         Top             =   1560
         Width           =   1935
      End
      Begin VB.TextBox CompanyName 
         Height          =   285
         Left            =   1440
         TabIndex        =   6
         Top             =   1080
         Width           =   1935
      End
      Begin VB.TextBox NameEdit 
         Height          =   285
         Left            =   1440
         TabIndex        =   5
         Top             =   600
         Width           =   1935
      End
      Begin VB.Label Label4 
         Caption         =   "Custom Data:"
         Height          =   255
         Left            =   3600
         TabIndex        =   4
         Top             =   600
         Width           =   1095
      End
      Begin VB.Label Label3 
         Caption         =   "Hardware ID:"
         Height          =   255
         Left            =   360
         TabIndex        =   3
         Top             =   1680
         Width           =   1095
      End
      Begin VB.Label Label2 
         Caption         =   "Organization:"
         Height          =   255
         Left            =   360
         TabIndex        =   2
         Top             =   1080
         Width           =   1095
      End
      Begin VB.Label Label1 
         Caption         =   "Name:"
         Height          =   255
         Left            =   720
         TabIndex        =   1
         Top             =   600
         Width           =   615
      End
   End
End
Attribute VB_Name = "Form1"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Const LicenseHash = "yuhsdf873hJHslk99423jhdsfhGSDKURKCFNB78934hGDMNF5634GfghwyIPahfurH6394hfJfhUYfikfhu8whj3yHYFIOOFhNFAPWRE"



Private Sub GenerateFileButton_Click()

    Dim UserName As String
    Dim Organization As String
    Dim HardId As String
    Dim Custom As String
    Dim NumDays As Integer
    Dim NumExec As Integer
    Dim NrOfBytes As Integer
    Dim NullDate As String

    
    If NameEdit.Text = "" Then
        
        UserName = vbNullString
    
    Else
    
        UserName = NameEdit.Text
        
    End If
        
    If CompanyName.Text = "" Then
        
        Organization = vbNullString
    
    Else
    
        Organization = CompanyName.Text
        
    End If
        
    If CustomName.Text = "" Then
        
        Custom = vbNullString
    
    Else
    
        Custom = CustomName.Text
        
    End If
        
    If HardIdName.Text = "" Then
        
        HardId = vbNullString
    
    Else
    
        HardId = HardIdName.Text
        
    End If
        
    If NumDaysEdit.Text = "" Then
        
        NumDays = 0
    
    Else
    
        NumDays = NumDaysEdit.Text
        
    End If
        
    If MaxExecEdit.Text = "" Then
        
        NumExec = 0
    
    Else
    
        NumExec = MaxExecEdit.Text
        
    End If
        
    ' create the file key
    
    
    NrOfBytes = WLGenLicenseFileKey(LicenseHash, UserName, Organization, Custom, HardId, NumDays, NumExec, NullDate, 0, 0, 0, GlobalBuffer)
    
    'store key in file

    Open App.Path & "\" & FileNameEdit.Text For Output As #1
        Print #1, Left(GlobalBuffer, NrOfBytes);
    Close #1
    
    MsgBox "A license key file key has been generated", vbInformation + vbOKOnly, "WinLicense SDK"
    
End Sub

Private Sub GenerateRegButton_Click()

    Dim UserName As String
    Dim Organization As String
    Dim HardId As String
    Dim Custom As String
    Dim NumDays As Integer
    Dim NumExec As Integer
    Dim NrOfBytes As Integer
    Dim NullDate As String

    
    If NameEdit.Text = "" Then
        
        UserName = vbNullString
    
    Else
    
        UserName = NameEdit.Text
        
    End If
        
    If CompanyName.Text = "" Then
        
        Organization = vbNullString
    
    Else
    
        Organization = CompanyName.Text
        
    End If
        
    If CustomName.Text = "" Then
        
        Custom = vbNullString
    
    Else
    
        Custom = CustomName.Text
        
    End If
        
    If HardIdName.Text = "" Then
        
        HardId = vbNullString
    
    Else
    
        HardId = HardIdName.Text
        
    End If
        
    If NumDaysEdit.Text = "" Then
        
        NumDays = 0
    
    Else
    
        NumDays = NumDaysEdit.Text
        
    End If
        
    If MaxExecEdit.Text = "" Then
        
        NumExec = 0
    
    Else
    
        NumExec = MaxExecEdit.Text
        
    End If
        
    ' create the file key
    
    NrOfBytes = WLGenLicenseRegistryKey LicenseHash, UserName, Organization, Custom, HardId, NumDays, NumExec, NullDate, 0, 0, 0, "HKEY_LOCAL_MACHINE" + KeyNameEdit.Text, KeyValueNameEdit.Text, GlobalBuffer

    'store key in file
    
    Open App.Path & "\" & FileNameEdit.Text For Output As #1
        Print #1, Left(GlobalBuffer, NrOfBytes);
    Close #1

    MsgBox "A license Registry file key has been generated", vbInformation + vbOKOnly, "WinLicense SDK"

End Sub

Private Sub GenerateSmartButton_Click()

    Dim UserName As String
    Dim Organization As String
    Dim HardId As String
    Dim Custom As String
    Dim NumDays As Integer
    Dim NumExec As Integer
    Dim NullDate As String
    
    If NameEdit.Text = "" Then
        
        UserName = vbNullString
    
    Else
    
        UserName = NameEdit.Text
        
    End If
        
    If CompanyName.Text = "" Then
        
        Organization = vbNullString
    
    Else
    
        Organization = CompanyName.Text
        
    End If
        
    If CustomName.Text = "" Then
        
        Custom = vbNullString
    
    Else
    
        Custom = CustomName.Text
        
    End If
        
    If HardIdName.Text = "" Then
        
        HardId = vbNullString
    
    Else
    
        HardId = HardIdName.Text
        
    End If
        
    If NumDaysEdit.Text = "" Then
        
        NumDays = 0
    
    Else
    
        NumDays = NumDaysEdit.Text
        
    End If
        
    If MaxExecEdit.Text = "" Then
        
        NumExec = 0
    
    Else
    
        NumExec = MaxExecEdit.Text
        
    End If
        
    ' create the file key
    
    WLGenLicenseSmartKey LicenseHash, UserName, Organization, Custom, HardId, NumDays, NumExec, NullDate, GlobalBuffer

    ' show key on GUI
    
    SmartKeyEdit.Text = GlobalBuffer


End Sub

Private Sub GenerateTextButton_Click()

    Dim UserName As String
    Dim Organization As String
    Dim HardId As String
    Dim Custom As String
    Dim NumDays As Integer
    Dim NumExec As Integer
    Dim NullDate As String
    
    If NameEdit.Text = "" Then
        
        UserName = vbNullString
    
    Else
    
        UserName = NameEdit.Text
        
    End If
        
    If CompanyName.Text = "" Then
        
        Organization = vbNullString
    
    Else
    
        Organization = CompanyName.Text
        
    End If
        
    If CustomName.Text = "" Then
        
        Custom = vbNullString
    
    Else
    
        Custom = CustomName.Text
        
    End If
        
    If HardIdName.Text = "" Then
        
        HardId = vbNullString
    
    Else
    
        HardId = HardIdName.Text
        
    End If
        
    If NumDaysEdit.Text = "" Then
        
        NumDays = 0
    
    Else
    
        NumDays = NumDaysEdit.Text
        
    End If
        
    If MaxExecEdit.Text = "" Then
        
        NumExec = 0
    
    Else
    
        NumExec = MaxExecEdit.Text
        
    End If
        
    ' create the file key
    
    WLGenLicenseTextKey LicenseHash, UserName, Organization, Custom, HardId, NumDays, NumExec, NullDate, 0, 0, 0, GlobalBuffer

    ' show key on GUI
    
    TextKeyEdit.Text = GlobalBuffer
    
End Sub
