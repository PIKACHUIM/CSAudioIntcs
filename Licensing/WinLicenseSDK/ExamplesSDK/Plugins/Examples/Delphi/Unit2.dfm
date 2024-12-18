object Form2: TForm2
  Left = 406
  Top = 257
  Caption = 'Form2'
  ClientHeight = 340
  ClientWidth = 591
  Color = clBtnFace
  Font.Charset = DEFAULT_CHARSET
  Font.Color = clWindowText
  Font.Height = -12
  Font.Name = 'Tahoma'
  Font.Style = []
  OldCreateOrder = False
  PixelsPerInch = 107
  TextHeight = 14
  object GroupBox1: TGroupBox
    Left = 48
    Top = 16
    Width = 537
    Height = 121
    Caption = 'Text Key Registration'
    TabOrder = 0
    object Label1: TLabel
      Left = 13
      Top = 53
      Width = 117
      Height = 14
      Caption = 'Insert your Text key:'
    end
    object btCheckText: TButton
      Left = 344
      Top = 24
      Width = 177
      Height = 22
      Caption = 'Check Text Key'
      TabOrder = 0
      OnClick = btCheckTextClick
    end
    object btInstallTextToFile: TButton
      Left = 344
      Top = 56
      Width = 177
      Height = 22
      Caption = 'Install to File'
      TabOrder = 1
      OnClick = btInstallTextToFileClick
    end
    object btRestartApplication: TButton
      Left = 344
      Top = 85
      Width = 177
      Height = 20
      Caption = 'Restart to Finish Registration'
      TabOrder = 2
      OnClick = btRestartApplicationClick
    end
    object edTextKey: TEdit
      Left = 136
      Top = 48
      Width = 185
      Height = 22
      TabOrder = 3
    end
  end
  object GroupBox2: TGroupBox
    Left = 48
    Top = 152
    Width = 537
    Height = 137
    Caption = 'Smart Key Registration'
    TabOrder = 1
    object Label2: TLabel
      Left = 16
      Top = 105
      Width = 123
      Height = 14
      Caption = 'Insert your Smart key:'
    end
    object Label3: TLabel
      Left = 56
      Top = 20
      Width = 63
      Height = 14
      Caption = 'User Name:'
    end
    object Label4: TLabel
      Left = 64
      Top = 47
      Width = 54
      Height = 14
      Caption = 'Company:'
    end
    object Label5: TLabel
      Left = 48
      Top = 76
      Width = 74
      Height = 14
      Caption = 'Custom Data:'
    end
    object btCheckSmart: TButton
      Left = 344
      Top = 24
      Width = 169
      Height = 22
      Caption = 'Check Smart Key'
      TabOrder = 0
      OnClick = btCheckSmartClick
    end
    object btInstallSmartToFile: TButton
      Left = 344
      Top = 64
      Width = 169
      Height = 22
      Caption = 'Install to File'
      TabOrder = 1
      OnClick = btInstallSmartToFileClick
    end
    object btRestartApplication2: TButton
      Left = 344
      Top = 101
      Width = 169
      Height = 20
      Caption = 'Restart to Finish Registration'
      TabOrder = 2
      OnClick = btRestartApplicationClick
    end
    object edSmartKey: TEdit
      Left = 142
      Top = 101
      Width = 187
      Height = 22
      TabOrder = 3
    end
    object edUserName: TEdit
      Left = 142
      Top = 16
      Width = 187
      Height = 22
      TabOrder = 4
    end
    object edCompany: TEdit
      Left = 142
      Top = 43
      Width = 187
      Height = 22
      TabOrder = 5
    end
    object edCustom: TEdit
      Left = 142
      Top = 72
      Width = 187
      Height = 22
      TabOrder = 6
    end
  end
  object Button1: TButton
    Left = 208
    Top = 304
    Width = 185
    Height = 25
    Caption = 'Continue in Trial Mode'
    TabOrder = 2
    OnClick = Button1Click
  end
end
