object Form1: TForm1
  Left = 192
  Top = 140
  Width = 420
  Height = 168
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
    Left = 120
    Top = 32
    Width = 169
    Height = 25
    Caption = 'Write Custom String'
    TabOrder = 0
    OnClick = Button1Click
  end
  object Button2: TButton
    Left = 120
    Top = 72
    Width = 169
    Height = 25
    Caption = 'Read Custom String'
    TabOrder = 1
    OnClick = Button2Click
  end
end
