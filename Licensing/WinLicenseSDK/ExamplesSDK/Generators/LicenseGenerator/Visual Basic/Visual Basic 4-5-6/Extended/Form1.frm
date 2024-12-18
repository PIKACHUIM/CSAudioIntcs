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

Const LicenseHash = "TLF09y0G97r11qU2mw7251703kEJpQ036bV71M6DwAFLpOu1m3h8I08m0M0mqm2e0I18E8H05I0MgVHOp7I8R3dI8M9QKIN2Q10rkdSJ07u4KIlKvd59ueWdRPm94md8cs3N470WMzQ450HlcG0sQ*eccsGA2AGAQHAABACFACCQQJXO3EFD6XIJLI4JXIKSWGEN3COE" _
              & "4W2EBBKAF4DEN26EPDZNUNB7Q6F5LUXZDMH3OIIJI*eccpGBFAGAQHQABACFACCQQJXO3EFD6XIJLI4JXIKSWGEN3COE4W2EBBKAF4DEN26EPDZNUNB7Q6F5LUXZDMH3OIIJICCRSSKC3LEV3AGPE3TJKAJHLAXFLMRYHUGQ*eccefffbfffb020100028181004ef4a" _
              & "1a100ca292c670568c5962074c8e5855d50a144cdb9dd5eef458bef1724e6354fcd9d110af05de9ce1b90e542b2738d331f3e3980fac1fef8852311ad5e47d82f2211bd0dad3dcf9ecbc85f5b9e8739b851e42efe44e83a100d3d63564c8e9bce28bf8f9" _
              & "9743b022b5cad647540512874a39580e6e222fc15bd5c9e2ed90203010001027f2d6a088dd32675085539874a1f500bdc822873c42cfea656823e58e267dade351b350ef1f5a617e4aa2011ad496a95328fc467e350a742c8f94cef25a54645b190f00d5" _
              & "cbe52b4c9ff92928f641fb875955671090721e63db98588c7f0e6ce336ab0be96a8715ed4200e7493e6278fa0e16f02a3a0483ca7405dc387aef2e50240822120a2b75de5424644868f5baf9266de09212e7e1998fa53f1e6ea1708f0d87b21fbe121804" _
              & "6b66b49ed37712c60daa2b5e5a6aa55ce1ca05010252bc75f6d02409b53c10ba9272d7042f9cdaecbfc85b44eb43dd30b635263234130de8538803903955b92fa952432bc5c65077f9e49ccca9741ee56a8f1c43a0dc9cb91ccad9d0241005178fc3de42" _
              & "05d9188fd0e62633d21a4560fd13e92a4e42a96b435a70a9506760361db99b0a7e79dfebe624efc0494ff4e3024809cd4ec1ef020307b955556610240354ff3ec6aac95bdf85f0127c3bff2ac466f3ae11b01861f9cd41d4fe2edd7a4ba0423a038fdc3a" _
              & "4de5de01748c82d906ae0abb28070103b47a9bfdf6d3591490241004c650dd0c6eb554c003d23bfe1eec8a5a79681580369ce246ef05bde0fed50b018da8e3f5fe9f84723bf81dec29cb52e7827d1bdc0e7eadf2c8a21441e5990a0fffefffe020100028" _
              & "201010063ec9ed9894431660046430ad00b2fd81f29367f078943f3eae90ab8d68212a7feda162700349504e51e778ee74955da82bf25512ee7cefdcb4b7cc9ec3a9fd47939b3ec27ca6f8c9edf7b78e6f96139c640a8785fe9fd4ca826de4ad5b86d343" _
              & "ab002b57dbc4ba202dd94075dd793d1d18374305e2daf48d1714c896da06ccf797abd0d632fc6695bdb33522b8a0819dbac20c86e3f1589a003f3ea442625b321e588e3891694ca9c1c9304a455d7007ecd9897512008d06cf40c6d3f32a29f1148f5a3b" _
              & "cd85d5a988d00b0617b1a5c13f117c4cb8416f207407c3f1d522a9c179d222b3b81d5154fe3be28dc1ff7b5e50b9d398030c00a061a7051ae1a57f10203010001028201001d67c69f51abc5b5b4a2f89bb7f972ccf0ee99e0ec00d30476beb9af074123e" _
              & "85831298fb499c4b8ba0c2b36dc8d523c897962abef57bc2c27a54145d2c4b5c03b2465f43b0971a63a229e1d482b5f17376653ad88dc3cf4500f901f09a1ca51326c157fff4e681b772869a579523567eb75f9bd3a19cb996c392142d805fbbefa4f87b" _
              & "5ce3fb730ed62ea836a69f55a6d9299ffce1afd719c3f1376c6df028ba82edc0ce0093aa99b65245927d3167637b1dcc81aa66f80ea765d5b7f5570fe250637deacc827ff75b253a706a0b556f9505d7dc57a2660ed1abfafe0ef618ccdbf3e8e5534a73" _
              & "9ef59c139bb2b3e517381dd8b5454607241ddd55798df29f9028180c57391aa671e92d7661b2fa9a592343d44a2525f516da5284339793e48c2a037f777ba8a8743eb3d5bedd2bb6a862c4f5cd9345f280d95a56fe01b16965140491b9408e98a828b987" _
              & "4ac9b7fd734c6b801ddf1aa86f3be089fc0bab650dc245e0e4e641799cd6a37a71bab0b19a8026731be0188f6f72426226e1e449c66ab9b028180818dd1c52f71862da0da37df923037d5dc726fc9fbe36af4e477a76eeaee961756256f0b75c16c6d7fb" _
              & "392ac4cbc8c6fcbe98dbd9208e4012657b8b6c7a9d0ba1236d66a551340f8d523844252cb1e784faca3240648ab97fd3b92236790167cb2c9ae5db3fbaa776a8ec6e78b3c7f817d133e0364ac348ed3811c7c38c5216302818083db327a5e149fb0f241d" _
              & "36971e519b7947ee58d3bbe81dd4a4f36352eb7b1f0cdf48980fae593514d739b17870284d296c981dccdcd8c92870c5c6a9005406dd1d9e0bdbcbe82e4537df4f5c14c402c1f2b0cb879188a5e0df8d176f0b6a21ea1651e1858a0417f5abbf2541e550" _
              & "10dd2bcf51f76f50191e9ca29b25dd37e630281802e613d25e0943dbafc08e353d09c1e811b888468c51a676200a0ad7a51f4d41acaa66018b43168c5d29fd0f48a0c3e228fd7aefc9cd93df703690821b75d1df25f2eee97e6991bfb79225be8fa86e90" _
              & "39d526a9d2cfabb23906ed12487a0d7cc109d8f24d0985a30a21f372a1178b914fc57f52d75cf0889d142399667247b8d028180b94d256ebb343a4ef3613b6b60e335b7cb5f8336ee122c7b62fd3d282679757cb82b4b4e8a0fabfe0844db1d61fed9f9f" _
              & "e4a44783e651609ddd37e7a5a87631cd2177777603804e72bcd86a4ded019f5c9c6482594132039eb627afc34ff6134f67700248aae5d3b1b0acbb547a8b5eae999729b9664cbf3f7747aa9bdab7d87"
              

Private Sub GenerateFileButton_Click()

    Dim UserName As String
    Dim Organization As String
    Dim HardId As String
    Dim Custom As String
    Dim NumDays As Integer
    Dim NumExec As Integer
    Dim NrOfBytes As Integer
    Dim NullDate As String
    Dim LicenseFeatures As sLicenseFeatures
    Dim ExpDate As SYSTEMTIME
    Dim InstallDate As SYSTEMTIME
    
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
        
        ' set expiration date
    
    With ExpDate
    
        .wYear = 2008
        .wDay = 11
        .wMonth = 6
         
    End With
    
    ' dont set install date
    
    With InstallDate
    
        .wYear = 0
        .wDay = 0
        .wMonth = 0
        
    End With
        
    ' set License features structure
    
    With LicenseFeatures
    
        .cb = Len(sLicenseFeatures)
        .NumDays = NumDays
        .NumExec = NumExec
        .ExpDate = ExpDate
        .CountryId = 0
        .Runtime = 0
        .GlobalMinutes = 0
        .InstallDate = InstallDate
        .NetInstances = 0
        .EmbedLicenseInfoInKey = 0
        .EmbedCreationDate = 0
       
    End With
    
        
    ' create the file key
    
    NrOfBytes = WLGenLicenseFileKeyEx(LicenseHash, UserName, Organization, Custom, HardId, LicenseFeatures, GlobalBuffer)
    
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
    Dim LicenseFeatures As sLicenseFeatures
    Dim ExpDate As SYSTEMTIME
    Dim InstallDate As SYSTEMTIME
    
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
        
    ' set expiration date
    
    With ExpDate
    
        .wYear = 2008
        .wDay = 11
        .wMonth = 6
         
    End With
    
    ' dont set install date
    
    With InstallDate
    
        .wYear = 0
        .wDay = 0
        .wMonth = 0
        
    End With
        
    ' set License features structure
    
    With LicenseFeatures
    
        .cb = Len(sLicenseFeatures)
        .NumDays = NumDays
        .NumExec = NumExec
        .ExpDate = ExpDate
        .CountryId = 0
        .Runtime = 0
        .GlobalMinutes = 0
        .InstallDate = InstallDate
        .NetInstances = 0
        .EmbedLicenseInfoInKey = 0
        .EmbedCreationDate = 0
       
    End With
    
        
    ' create the file key
    
    NrOfBytes = WLGenLicenseRegistryKeyEx(LicenseHash, UserName, Organization, Custom, HardId, LicenseFeatures, GlobalBuffer)

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
    Dim LicenseFeatures As sLicenseFeatures
    Dim ExpDate As SYSTEMTIME
    Dim InstallDate As SYSTEMTIME
        
        
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
    
    ' set expiration date
    
    With ExpDate
    
        .wYear = 2008
        .wDay = 11
        .wMonth = 6
         
    End With
    
    ' dont set install date
    
    With InstallDate
    
        .wYear = 0
        .wDay = 0
        .wMonth = 0
        
    End With
        
    ' set License features structure
    
    With LicenseFeatures
    
        .cb = Len(sLicenseFeatures)
        .NumDays = NumDays
        .NumExec = NumExec
        .ExpDate = ExpDate
        .CountryId = 0
        .Runtime = 0
        .GlobalMinutes = 0
        .InstallDate = InstallDate
        .NetInstances = 0
        .EmbedLicenseInfoInKey = 0
        .EmbedCreationDate = 0
       
    End With
    
        
    ' create the file key
    
    WLGenLicenseDynSmartKey LicenseHash, UserName, Organization, Custom, HardId, LicenseFeatures, GlobalBuffer

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
    Dim LicenseFeatures As sLicenseFeatures
    Dim ExpDate As SYSTEMTIME
    Dim InstallDate As SYSTEMTIME
    
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
        
    ' set expiration date
    
    With ExpDate
    
        .wYear = 2008
        .wDay = 11
        .wMonth = 6
         
    End With
    
    ' dont set install date
    
    With InstallDate
    
        .wYear = 0
        .wDay = 0
        .wMonth = 0
        
    End With
        
    ' set License features structure
    
    With LicenseFeatures
    
        .cb = Len(sLicenseFeatures)
        .NumDays = NumDays
        .NumExec = NumExec
        .ExpDate = ExpDate
        .CountryId = 0
        .Runtime = 0
        .GlobalMinutes = 0
        .InstallDate = InstallDate
        .NetInstances = 0
        .EmbedLicenseInfoInKey = 0
        .EmbedCreationDate = 0
       
    End With
            
    ' create the file key
    
    WLGenLicenseTextKeyEx LicenseHash, UserName, Organization, Custom, HardId, LicenseFeatures, GlobalBuffer

    ' show key on GUI
    
    TextKeyEdit.Text = GlobalBuffer
    
End Sub
