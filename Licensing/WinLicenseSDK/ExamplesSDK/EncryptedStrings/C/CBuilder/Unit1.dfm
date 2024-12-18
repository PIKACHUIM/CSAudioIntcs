object Form1: TForm1
  Left = 192
  Top = 114
  Width = 310
  Height = 218
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
  object Button1: TButton
    Left = 80
    Top = 32
    Width = 145
    Height = 25
    Caption = 'Display encrypted string 1'
    TabOrder = 0
    OnClick = Button1Click
  end
  object Button2: TButton
    Left = 80
    Top = 72
    Width = 145
    Height = 25
    Caption = 'Display encrypted string 2'
    TabOrder = 1
    OnClick = Button2Click
  end
  object Button3: TButton
    Left = 80
    Top = 112
    Width = 145
    Height = 25
    Caption = 'Display encrypted string 3'
    TabOrder = 2
    OnClick = Button3Click
  end
end
