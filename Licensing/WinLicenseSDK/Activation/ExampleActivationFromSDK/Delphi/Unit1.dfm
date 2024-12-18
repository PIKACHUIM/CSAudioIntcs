object Form1: TForm1
  Left = 1481
  Top = 90
  Width = 421
  Height = 616
  Caption = 'Activation SDK'
  Color = clBtnFace
  Font.Charset = DEFAULT_CHARSET
  Font.Color = clWindowText
  Font.Height = -12
  Font.Name = 'MS Sans Serif'
  Font.Style = []
  OldCreateOrder = False
  PixelsPerInch = 107
  TextHeight = 13
  object GroupBox1: TGroupBox
    Left = 24
    Top = 25
    Width = 361
    Height = 81
    Caption = 'Application Status'
    TabOrder = 0
    object StatusLabel: TLabel
      Left = 56
      Top = 48
      Width = 6
      Height = 20
      Color = clBtnFace
      Font.Charset = DEFAULT_CHARSET
      Font.Color = clHotLight
      Font.Height = -16
      Font.Name = 'MS Sans Serif'
      Font.Style = [fsBold]
      ParentColor = False
      ParentFont = False
    end
    object Label6: TLabel
      Left = 16
      Top = 56
      Width = 33
      Height = 13
      Caption = 'Status:'
    end
    object Button5: TButton
      Left = 104
      Top = 16
      Width = 129
      Height = 25
      Caption = 'Check Application Status'
      TabOrder = 0
      OnClick = Button5Click
    end
  end
  object GroupBox2: TGroupBox
    Left = 24
    Top = 112
    Width = 361
    Height = 129
    Caption = 'Trial Information'
    TabOrder = 1
    object DaysLeftLabel: TLabel
      Left = 96
      Top = 24
      Width = 5
      Height = 24
      Font.Charset = DEFAULT_CHARSET
      Font.Color = clWindowText
      Font.Height = -19
      Font.Name = 'MS Sans Serif'
      Font.Style = []
      ParentFont = False
    end
    object Label1: TLabel
      Left = 32
      Top = 32
      Width = 48
      Height = 13
      Caption = 'Days Left:'
    end
    object Label8: TLabel
      Left = 8
      Top = 64
      Width = 76
      Height = 13
      Caption = 'Executions Left:'
    end
    object ExecLeftLabel: TLabel
      Left = 96
      Top = 56
      Width = 5
      Height = 24
      Font.Charset = DEFAULT_CHARSET
      Font.Color = clWindowText
      Font.Height = -19
      Font.Name = 'MS Sans Serif'
      Font.Style = []
      ParentFont = False
    end
    object Label13: TLabel
      Left = 8
      Top = 96
      Width = 75
      Height = 13
      Caption = 'Expiration Date:'
    end
    object Label15: TLabel
      Left = 184
      Top = 32
      Width = 61
      Height = 13
      Caption = 'Minutes Left:'
    end
    object MinutesLeftLabel: TLabel
      Left = 256
      Top = 24
      Width = 5
      Height = 24
      Font.Charset = DEFAULT_CHARSET
      Font.Color = clWindowText
      Font.Height = -19
      Font.Name = 'MS Sans Serif'
      Font.Style = []
      ParentFont = False
    end
    object Label17: TLabel
      Left = 184
      Top = 64
      Width = 63
      Height = 13
      Caption = 'Runtime Left:'
    end
    object RuntimeLeftLabel: TLabel
      Left = 256
      Top = 56
      Width = 5
      Height = 24
      Font.Charset = DEFAULT_CHARSET
      Font.Color = clWindowText
      Font.Height = -19
      Font.Name = 'MS Sans Serif'
      Font.Style = []
      ParentFont = False
    end
    object ExpDatePicker: TDateTimePicker
      Left = 96
      Top = 88
      Width = 97
      Height = 21
      Date = 38145.586395937500000000
      Time = 38145.586395937500000000
      Enabled = False
      TabOrder = 0
    end
  end
  object GroupBox3: TGroupBox
    Left = 24
    Top = 256
    Width = 361
    Height = 177
    Caption = 'License Information'
    TabOrder = 2
    object RegDaysLeftLabel: TLabel
      Left = 96
      Top = 104
      Width = 5
      Height = 24
      Font.Charset = DEFAULT_CHARSET
      Font.Color = clWindowText
      Font.Height = -19
      Font.Name = 'MS Sans Serif'
      Font.Style = []
      ParentFont = False
    end
    object Label9: TLabel
      Left = 32
      Top = 112
      Width = 48
      Height = 13
      Caption = 'Days Left:'
    end
    object Label10: TLabel
      Left = 184
      Top = 112
      Width = 76
      Height = 13
      Caption = 'Executions Left:'
    end
    object RegExecLeftLabel: TLabel
      Left = 272
      Top = 104
      Width = 5
      Height = 24
      Font.Charset = DEFAULT_CHARSET
      Font.Color = clWindowText
      Font.Height = -19
      Font.Name = 'MS Sans Serif'
      Font.Style = []
      ParentFont = False
    end
    object Label19: TLabel
      Left = 32
      Top = 144
      Width = 75
      Height = 13
      Caption = 'Expiration Date:'
    end
    object Label2: TLabel
      Left = 24
      Top = 40
      Width = 31
      Height = 13
      Caption = 'Name:'
    end
    object Label4: TLabel
      Left = 24
      Top = 72
      Width = 47
      Height = 13
      Caption = 'Company:'
    end
    object Label5: TLabel
      Left = 184
      Top = 40
      Width = 38
      Height = 13
      Caption = 'Custom:'
    end
    object RegNameEdit: TEdit
      Left = 80
      Top = 32
      Width = 81
      Height = 21
      Color = clSilver
      ReadOnly = True
      TabOrder = 0
    end
    object CompanyNameEdit: TEdit
      Left = 80
      Top = 64
      Width = 81
      Height = 21
      Color = clSilver
      ReadOnly = True
      TabOrder = 1
    end
    object CustomEdit: TMemo
      Left = 232
      Top = 32
      Width = 113
      Height = 57
      Color = clSilver
      Lines.Strings = (
        'CustomEdit')
      ReadOnly = True
      TabOrder = 2
    end
    object RegDateTimePicker1: TDateTimePicker
      Left = 112
      Top = 136
      Width = 97
      Height = 21
      Date = 38145.586395937500000000
      Time = 38145.586395937500000000
      Enabled = False
      TabOrder = 3
    end
  end
  object GroupBox5: TGroupBox
    Left = 24
    Top = 448
    Width = 361
    Height = 113
    Caption = 'Activation'
    TabOrder = 3
    object btnActivate: TButton
      Left = 24
      Top = 32
      Width = 105
      Height = 25
      Caption = 'Activate'
      TabOrder = 0
      OnClick = btnActivateClick
    end
    object ActivationEdit: TEdit
      Left = 151
      Top = 36
      Width = 201
      Height = 21
      Color = clWhite
      TabOrder = 1
    end
    object DeactivationEdit: TEdit
      Left = 151
      Top = 68
      Width = 201
      Height = 21
      Color = clWhite
      TabOrder = 2
    end
    object Button1: TButton
      Left = 24
      Top = 64
      Width = 105
      Height = 25
      Caption = 'Deactivate'
      TabOrder = 3
      OnClick = Button1Click
    end
  end
end
