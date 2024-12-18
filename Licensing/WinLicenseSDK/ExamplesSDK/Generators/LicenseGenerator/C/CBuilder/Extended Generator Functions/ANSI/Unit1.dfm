object Form1: TForm1
  Left = 361
  Top = 54
  Width = 518
  Height = 665
  Caption = 'License Key Generator SDK'
  Color = clBtnFace
  Font.Charset = DEFAULT_CHARSET
  Font.Color = clWindowText
  Font.Height = -11
  Font.Name = 'MS Sans Serif'
  Font.Style = []
  OldCreateOrder = False
  PixelsPerInch = 96
  TextHeight = 13
  object GroupBox1: TGroupBox
    Left = 24
    Top = 16
    Width = 473
    Height = 145
    Caption = 'User'#39's Information'
    TabOrder = 0
    object Label5: TLabel
      Left = 72
      Top = 40
      Width = 31
      Height = 13
      Caption = 'Name:'
    end
    object Label6: TLabel
      Left = 40
      Top = 72
      Width = 62
      Height = 13
      Caption = 'Organization:'
    end
    object Label7: TLabel
      Left = 40
      Top = 104
      Width = 63
      Height = 13
      Caption = 'Hardware ID:'
    end
    object Label8: TLabel
      Left = 248
      Top = 40
      Width = 64
      Height = 13
      Caption = 'Custom Data:'
    end
    object NameEdit: TEdit
      Left = 112
      Top = 32
      Width = 121
      Height = 21
      TabOrder = 0
    end
    object OrgEdit: TEdit
      Left = 112
      Top = 64
      Width = 121
      Height = 21
      TabOrder = 1
    end
    object HardIdEdit: TEdit
      Left = 112
      Top = 96
      Width = 121
      Height = 21
      TabOrder = 2
    end
    object CustomEdit: TEdit
      Left = 320
      Top = 32
      Width = 121
      Height = 89
      AutoSize = False
      TabOrder = 3
    end
  end
  object GroupBox2: TGroupBox
    Left = 24
    Top = 176
    Width = 473
    Height = 97
    Caption = 'Key Expiration Information'
    TabOrder = 1
    object Label1: TLabel
      Left = 48
      Top = 32
      Width = 67
      Height = 13
      Caption = 'Number Days:'
    end
    object Label2: TLabel
      Left = 32
      Top = 64
      Width = 81
      Height = 13
      Caption = 'Max. Executions:'
    end
    object Label3: TLabel
      Left = 272
      Top = 32
      Width = 75
      Height = 13
      Caption = 'Expiration Date:'
    end
    object NumDaysEdit: TEdit
      Left = 136
      Top = 24
      Width = 121
      Height = 21
      TabOrder = 0
    end
    object NumExecEdit: TEdit
      Left = 136
      Top = 56
      Width = 121
      Height = 21
      TabOrder = 1
    end
    object ExpDate: TDateTimePicker
      Left = 352
      Top = 24
      Width = 97
      Height = 21
      CalAlignment = dtaLeft
      Date = 38142.3262738657
      Time = 38142.3262738657
      DateFormat = dfShort
      DateMode = dmComboBox
      Enabled = False
      Kind = dtkDate
      ParseInput = False
      TabOrder = 2
    end
    object EnableDateCheck: TCheckBox
      Left = 280
      Top = 56
      Width = 137
      Height = 17
      Caption = 'Enable Expiration Date'
      TabOrder = 3
      OnClick = EnableDateCheckClick
    end
  end
  object GroupBox3: TGroupBox
    Left = 24
    Top = 288
    Width = 473
    Height = 337
    Caption = 'Type Generated Key'
    TabOrder = 2
    object GroupBox4: TGroupBox
      Left = 16
      Top = 32
      Width = 177
      Height = 89
      Caption = 'File Key'
      TabOrder = 0
      object Label4: TLabel
        Left = 16
        Top = 24
        Width = 53
        Height = 13
        Caption = 'File Name: '
      end
      object FileNameEdit: TEdit
        Left = 72
        Top = 16
        Width = 81
        Height = 21
        TabOrder = 0
        Text = 'License.dat'
      end
      object Button1: TButton
        Left = 32
        Top = 56
        Width = 105
        Height = 17
        Caption = 'Generate File Key'
        TabOrder = 1
        OnClick = Button1Click
      end
    end
    object GroupBox5: TGroupBox
      Left = 224
      Top = 32
      Width = 233
      Height = 153
      Caption = 'Registry  Key'
      TabOrder = 1
      object Label9: TLabel
        Left = 16
        Top = 24
        Width = 55
        Height = 13
        Caption = 'Key Name: '
      end
      object Label10: TLabel
        Left = 16
        Top = 56
        Width = 82
        Height = 13
        Caption = 'Key Value Name:'
      end
      object Label11: TLabel
        Left = 16
        Top = 88
        Width = 76
        Height = 13
        Caption = 'File name (*.reg)'
      end
      object RegistryEdit: TEdit
        Left = 112
        Top = 16
        Width = 113
        Height = 21
        TabOrder = 0
        Text = 'Software\Mysoft'
      end
      object Button2: TButton
        Left = 48
        Top = 120
        Width = 129
        Height = 17
        Caption = 'Generate Reg Key File'
        TabOrder = 1
        OnClick = Button2Click
      end
      object Edit3: TEdit
        Left = 72
        Top = 16
        Width = 41
        Height = 21
        Color = clInactiveBorder
        ReadOnly = True
        TabOrder = 2
        Text = 'HKLM\'
      end
      object RegValueEdit: TEdit
        Left = 104
        Top = 48
        Width = 121
        Height = 21
        TabOrder = 3
        Text = 'reg_value'
      end
    end
    object LicRegEdit: TEdit
      Left = 328
      Top = 112
      Width = 121
      Height = 21
      TabOrder = 2
      Text = 'License.reg'
    end
    object RadioGroup1: TRadioGroup
      Left = 16
      Top = 136
      Width = 185
      Height = 177
      Caption = 'Text Key'
      TabOrder = 3
    end
    object Button3: TButton
      Left = 48
      Top = 160
      Width = 105
      Height = 17
      Caption = 'Generate Text Key'
      TabOrder = 4
      OnClick = Button3Click
    end
    object GroupBox6: TGroupBox
      Left = 224
      Top = 200
      Width = 233
      Height = 113
      Caption = 'SmartActivate Key'
      TabOrder = 5
      object Button4: TButton
        Left = 56
        Top = 32
        Width = 105
        Height = 17
        Caption = 'Generate Smart Key'
        TabOrder = 0
        OnClick = Button4Click
      end
      object SmartEdit: TEdit
        Left = 16
        Top = 64
        Width = 201
        Height = 21
        Color = clSilver
        TabOrder = 1
      end
    end
    object TextKeyMemo: TMemo
      Left = 32
      Top = 192
      Width = 153
      Height = 89
      Color = clSilver
      ParentShowHint = False
      ReadOnly = True
      ScrollBars = ssVertical
      ShowHint = False
      TabOrder = 6
    end
  end
end
