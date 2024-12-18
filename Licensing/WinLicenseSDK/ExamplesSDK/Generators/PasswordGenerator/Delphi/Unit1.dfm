object Form1: TForm1
  Left = 438
  Top = 375
  Width = 385
  Height = 199
  Caption = 'Form1'
  Color = clBtnFace
  Font.Charset = DEFAULT_CHARSET
  Font.Color = clWindowText
  Font.Height = -11
  Font.Name = 'MS Sans Serif'
  Font.Style = []
  OldCreateOrder = False
  PixelsPerInch = 96
  TextHeight = 13
  object Label1: TLabel
    Left = 40
    Top = 48
    Width = 31
    Height = 13
    Caption = 'Name:'
  end
  object Label2: TLabel
    Left = 24
    Top = 80
    Width = 49
    Height = 13
    Caption = 'Password:'
  end
  object Button1: TButton
    Left = 88
    Top = 120
    Width = 177
    Height = 25
    Caption = 'Generate Password'
    TabOrder = 0
    OnClick = Button1Click
  end
  object NameEdit: TEdit
    Left = 88
    Top = 40
    Width = 225
    Height = 21
    TabOrder = 1
  end
  object PassEdit: TEdit
    Left = 88
    Top = 72
    Width = 225
    Height = 21
    TabOrder = 2
  end
end
