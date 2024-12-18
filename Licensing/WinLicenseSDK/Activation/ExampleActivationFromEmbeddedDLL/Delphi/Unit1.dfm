object Form1: TForm1
  Left = 1576
  Top = 375
  BorderIcons = [biSystemMenu, biMinimize]
  BorderStyle = bsDialog
  Caption = 'Activation'
  ClientHeight = 196
  ClientWidth = 438
  Color = 13730326
  Font.Charset = DEFAULT_CHARSET
  Font.Color = clWindowText
  Font.Height = -12
  Font.Name = 'MS Sans Serif'
  Font.Style = []
  OldCreateOrder = False
  Position = poScreenCenter
  PixelsPerInch = 106
  TextHeight = 13
  object lbTitle: TLabel
    Left = 16
    Top = 8
    Width = 190
    Height = 24
    Caption = 'Enter Activation Key'
    Color = clWhite
    Font.Charset = DEFAULT_CHARSET
    Font.Color = clWhite
    Font.Height = -21
    Font.Name = 'Arial'
    Font.Style = []
    ParentColor = False
    ParentFont = False
    Transparent = True
  end
  object lbLine1: TLabel
    Left = 16
    Top = 48
    Width = 405
    Height = 15
    Caption = 
      'This application requires online activation. Internet connection' +
      ' is required. '
    Font.Charset = DEFAULT_CHARSET
    Font.Color = clWhite
    Font.Height = -12
    Font.Name = 'Arial'
    Font.Style = []
    ParentFont = False
  end
  object lbLine2: TLabel
    Left = 16
    Top = 72
    Width = 322
    Height = 15
    Caption = 'Please, insert your Activation key to activate this application.'
    Font.Charset = DEFAULT_CHARSET
    Font.Color = clWhite
    Font.Height = -12
    Font.Name = 'Arial'
    Font.Style = []
    ParentFont = False
  end
  object Label4: TLabel
    Left = 16
    Top = 88
    Width = 3
    Height = 15
    Font.Charset = DEFAULT_CHARSET
    Font.Color = clWhite
    Font.Height = -12
    Font.Name = 'Arial'
    Font.Style = []
    ParentFont = False
  end
  object Label5: TLabel
    Left = 16
    Top = 109
    Width = 75
    Height = 15
    Caption = 'Activation key:'
    Font.Charset = DEFAULT_CHARSET
    Font.Color = clWhite
    Font.Height = -12
    Font.Name = 'Arial'
    Font.Style = []
    ParentFont = False
  end
  object btnActivate: TButton
    Left = 176
    Top = 160
    Width = 75
    Height = 25
    Caption = 'OK'
    TabOrder = 0
    OnClick = btnActivateClick
  end
  object edActivationKey: TEdit
    Left = 16
    Top = 128
    Width = 409
    Height = 22
    Ctl3D = False
    Font.Charset = DEFAULT_CHARSET
    Font.Color = clNavy
    Font.Height = -13
    Font.Name = 'Arial'
    Font.Style = []
    ParentCtl3D = False
    ParentFont = False
    TabOrder = 1
  end
end
