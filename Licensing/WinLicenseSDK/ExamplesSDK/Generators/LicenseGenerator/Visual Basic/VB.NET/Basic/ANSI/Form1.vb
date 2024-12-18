Option Strict Off
Option Explicit On

Imports System.Text

Friend Class Form1
    Inherits System.Windows.Forms.Form

#Region "Windows Form Designer generated code "
    Public Sub New()
        MyBase.New()
        If m_vb6FormDefInstance Is Nothing Then
            If m_InitializingDefInstance Then
                m_vb6FormDefInstance = Me
            Else
                Try
                    'For the start-up form, the first instance created is the default instance.
                    If System.Reflection.Assembly.GetExecutingAssembly.EntryPoint.DeclaringType Is Me.GetType Then
                        m_vb6FormDefInstance = Me
                    End If
                Catch
                End Try
            End If
        End If
        'This call is required by the Windows Form Designer.
        InitializeComponent()
    End Sub
    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal Disposing As Boolean)
        If Disposing Then
            If Not components Is Nothing Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(Disposing)
    End Sub
    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer
    Public ToolTip1 As System.Windows.Forms.ToolTip
    Public WithEvents SmartKeyEdit As System.Windows.Forms.TextBox
    Public WithEvents GenerateSmartButton As System.Windows.Forms.Button
    Public WithEvents Frame7 As System.Windows.Forms.GroupBox
    Public WithEvents GenerateRegButton As System.Windows.Forms.Button
    Public WithEvents RegNameEdit As System.Windows.Forms.TextBox
    Public WithEvents KeyValueNameEdit As System.Windows.Forms.TextBox
    Public WithEvents KeyNameEdit As System.Windows.Forms.TextBox
    Public WithEvents Text9 As System.Windows.Forms.TextBox
    Public WithEvents Label10 As System.Windows.Forms.Label
    Public WithEvents Label9 As System.Windows.Forms.Label
    Public WithEvents Label8 As System.Windows.Forms.Label
    Public WithEvents Frame6 As System.Windows.Forms.GroupBox
    Public WithEvents TextKeyEdit As System.Windows.Forms.TextBox
    Public WithEvents GenerateTextButton As System.Windows.Forms.Button
    Public WithEvents Frame5 As System.Windows.Forms.GroupBox
    Public WithEvents GenerateFileButton As System.Windows.Forms.Button
    Public WithEvents FileNameEdit As System.Windows.Forms.TextBox
    Public WithEvents Label7 As System.Windows.Forms.Label
    Public WithEvents Frame4 As System.Windows.Forms.GroupBox
    Public WithEvents Frame3 As System.Windows.Forms.GroupBox
    Public WithEvents MaxExecEdit As System.Windows.Forms.TextBox
    Public WithEvents NumDaysEdit As System.Windows.Forms.TextBox
    Public WithEvents Label6 As System.Windows.Forms.Label
    Public WithEvents Label5 As System.Windows.Forms.Label
    Public WithEvents Frame2 As System.Windows.Forms.GroupBox
    Public WithEvents CustomName As System.Windows.Forms.TextBox
    Public WithEvents HardIdName As System.Windows.Forms.TextBox
    Public WithEvents CompanyName_Renamed As System.Windows.Forms.TextBox
    Public WithEvents NameEdit As System.Windows.Forms.TextBox
    Public WithEvents Label4 As System.Windows.Forms.Label
    Public WithEvents Label3 As System.Windows.Forms.Label
    Public WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents Frame1 As System.Windows.Forms.GroupBox
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.Frame3 = New System.Windows.Forms.GroupBox
        Me.Frame7 = New System.Windows.Forms.GroupBox
        Me.SmartKeyEdit = New System.Windows.Forms.TextBox
        Me.GenerateSmartButton = New System.Windows.Forms.Button
        Me.Frame6 = New System.Windows.Forms.GroupBox
        Me.GenerateRegButton = New System.Windows.Forms.Button
        Me.RegNameEdit = New System.Windows.Forms.TextBox
        Me.KeyValueNameEdit = New System.Windows.Forms.TextBox
        Me.KeyNameEdit = New System.Windows.Forms.TextBox
        Me.Text9 = New System.Windows.Forms.TextBox
        Me.Label10 = New System.Windows.Forms.Label
        Me.Label9 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.Frame5 = New System.Windows.Forms.GroupBox
        Me.TextKeyEdit = New System.Windows.Forms.TextBox
        Me.GenerateTextButton = New System.Windows.Forms.Button
        Me.Frame4 = New System.Windows.Forms.GroupBox
        Me.GenerateFileButton = New System.Windows.Forms.Button
        Me.FileNameEdit = New System.Windows.Forms.TextBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.Frame2 = New System.Windows.Forms.GroupBox
        Me.MaxExecEdit = New System.Windows.Forms.TextBox
        Me.NumDaysEdit = New System.Windows.Forms.TextBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.Frame1 = New System.Windows.Forms.GroupBox
        Me.CustomName = New System.Windows.Forms.TextBox
        Me.HardIdName = New System.Windows.Forms.TextBox
        Me.CompanyName_Renamed = New System.Windows.Forms.TextBox
        Me.NameEdit = New System.Windows.Forms.TextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.Frame3.SuspendLayout()
        Me.Frame7.SuspendLayout()
        Me.Frame6.SuspendLayout()
        Me.Frame5.SuspendLayout()
        Me.Frame4.SuspendLayout()
        Me.Frame2.SuspendLayout()
        Me.Frame1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Frame3
        '
        Me.Frame3.BackColor = System.Drawing.SystemColors.Control
        Me.Frame3.Controls.Add(Me.Frame7)
        Me.Frame3.Controls.Add(Me.Frame6)
        Me.Frame3.Controls.Add(Me.Frame5)
        Me.Frame3.Controls.Add(Me.Frame4)
        Me.Frame3.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame3.Location = New System.Drawing.Point(32, 296)
        Me.Frame3.Name = "Frame3"
        Me.Frame3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame3.Size = New System.Drawing.Size(441, 281)
        Me.Frame3.TabIndex = 14
        Me.Frame3.TabStop = False
        Me.Frame3.Text = "Type Generated Key"
        '
        'Frame7
        '
        Me.Frame7.BackColor = System.Drawing.SystemColors.Control
        Me.Frame7.Controls.Add(Me.SmartKeyEdit)
        Me.Frame7.Controls.Add(Me.GenerateSmartButton)
        Me.Frame7.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame7.Location = New System.Drawing.Point(216, 192)
        Me.Frame7.Name = "Frame7"
        Me.Frame7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame7.Size = New System.Drawing.Size(193, 73)
        Me.Frame7.TabIndex = 31
        Me.Frame7.TabStop = False
        Me.Frame7.Text = "SmartActivate Key"
        '
        'SmartKeyEdit
        '
        Me.SmartKeyEdit.AcceptsReturn = True
        Me.SmartKeyEdit.BackColor = System.Drawing.SystemColors.Window
        Me.SmartKeyEdit.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.SmartKeyEdit.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SmartKeyEdit.ForeColor = System.Drawing.SystemColors.WindowText
        Me.SmartKeyEdit.Location = New System.Drawing.Point(8, 48)
        Me.SmartKeyEdit.MaxLength = 0
        Me.SmartKeyEdit.Name = "SmartKeyEdit"
        Me.SmartKeyEdit.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.SmartKeyEdit.Size = New System.Drawing.Size(177, 20)
        Me.SmartKeyEdit.TabIndex = 33
        '
        'GenerateSmartButton
        '
        Me.GenerateSmartButton.BackColor = System.Drawing.SystemColors.Control
        Me.GenerateSmartButton.Cursor = System.Windows.Forms.Cursors.Default
        Me.GenerateSmartButton.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GenerateSmartButton.ForeColor = System.Drawing.SystemColors.ControlText
        Me.GenerateSmartButton.Location = New System.Drawing.Point(32, 24)
        Me.GenerateSmartButton.Name = "GenerateSmartButton"
        Me.GenerateSmartButton.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.GenerateSmartButton.Size = New System.Drawing.Size(121, 17)
        Me.GenerateSmartButton.TabIndex = 32
        Me.GenerateSmartButton.Text = "Generate Smart Key"
        Me.GenerateSmartButton.UseVisualStyleBackColor = False
        '
        'Frame6
        '
        Me.Frame6.BackColor = System.Drawing.SystemColors.Control
        Me.Frame6.Controls.Add(Me.GenerateRegButton)
        Me.Frame6.Controls.Add(Me.RegNameEdit)
        Me.Frame6.Controls.Add(Me.KeyValueNameEdit)
        Me.Frame6.Controls.Add(Me.KeyNameEdit)
        Me.Frame6.Controls.Add(Me.Text9)
        Me.Frame6.Controls.Add(Me.Label10)
        Me.Frame6.Controls.Add(Me.Label9)
        Me.Frame6.Controls.Add(Me.Label8)
        Me.Frame6.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame6.Location = New System.Drawing.Point(216, 31)
        Me.Frame6.Name = "Frame6"
        Me.Frame6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame6.Size = New System.Drawing.Size(193, 153)
        Me.Frame6.TabIndex = 22
        Me.Frame6.TabStop = False
        Me.Frame6.Text = "Registry Key"
        '
        'GenerateRegButton
        '
        Me.GenerateRegButton.BackColor = System.Drawing.SystemColors.Control
        Me.GenerateRegButton.Cursor = System.Windows.Forms.Cursors.Default
        Me.GenerateRegButton.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GenerateRegButton.ForeColor = System.Drawing.SystemColors.ControlText
        Me.GenerateRegButton.Location = New System.Drawing.Point(40, 120)
        Me.GenerateRegButton.Name = "GenerateRegButton"
        Me.GenerateRegButton.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.GenerateRegButton.Size = New System.Drawing.Size(121, 21)
        Me.GenerateRegButton.TabIndex = 30
        Me.GenerateRegButton.Text = "Generate Reg Key File"
        Me.GenerateRegButton.UseVisualStyleBackColor = False
        '
        'RegNameEdit
        '
        Me.RegNameEdit.AcceptsReturn = True
        Me.RegNameEdit.BackColor = System.Drawing.SystemColors.Window
        Me.RegNameEdit.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.RegNameEdit.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RegNameEdit.ForeColor = System.Drawing.SystemColors.WindowText
        Me.RegNameEdit.Location = New System.Drawing.Point(96, 80)
        Me.RegNameEdit.MaxLength = 0
        Me.RegNameEdit.Name = "RegNameEdit"
        Me.RegNameEdit.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.RegNameEdit.Size = New System.Drawing.Size(89, 20)
        Me.RegNameEdit.TabIndex = 29
        Me.RegNameEdit.Text = "License.reg"
        '
        'KeyValueNameEdit
        '
        Me.KeyValueNameEdit.AcceptsReturn = True
        Me.KeyValueNameEdit.BackColor = System.Drawing.SystemColors.Window
        Me.KeyValueNameEdit.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.KeyValueNameEdit.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.KeyValueNameEdit.ForeColor = System.Drawing.SystemColors.WindowText
        Me.KeyValueNameEdit.Location = New System.Drawing.Point(104, 56)
        Me.KeyValueNameEdit.MaxLength = 0
        Me.KeyValueNameEdit.Name = "KeyValueNameEdit"
        Me.KeyValueNameEdit.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.KeyValueNameEdit.Size = New System.Drawing.Size(81, 20)
        Me.KeyValueNameEdit.TabIndex = 27
        Me.KeyValueNameEdit.Text = "reg_value"
        '
        'KeyNameEdit
        '
        Me.KeyNameEdit.AcceptsReturn = True
        Me.KeyNameEdit.BackColor = System.Drawing.SystemColors.Window
        Me.KeyNameEdit.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.KeyNameEdit.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.KeyNameEdit.ForeColor = System.Drawing.SystemColors.WindowText
        Me.KeyNameEdit.Location = New System.Drawing.Point(111, 24)
        Me.KeyNameEdit.MaxLength = 0
        Me.KeyNameEdit.Name = "KeyNameEdit"
        Me.KeyNameEdit.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.KeyNameEdit.Size = New System.Drawing.Size(72, 20)
        Me.KeyNameEdit.TabIndex = 25
        Me.KeyNameEdit.Text = "\Software\MyApp"
        '
        'Text9
        '
        Me.Text9.AcceptsReturn = True
        Me.Text9.BackColor = System.Drawing.SystemColors.Window
        Me.Text9.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.Text9.Enabled = False
        Me.Text9.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Text9.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Text9.Location = New System.Drawing.Point(72, 24)
        Me.Text9.MaxLength = 0
        Me.Text9.Name = "Text9"
        Me.Text9.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Text9.Size = New System.Drawing.Size(39, 20)
        Me.Text9.TabIndex = 24
        Me.Text9.Text = "HKLM"
        '
        'Label10
        '
        Me.Label10.BackColor = System.Drawing.SystemColors.Control
        Me.Label10.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label10.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label10.Location = New System.Drawing.Point(16, 88)
        Me.Label10.Name = "Label10"
        Me.Label10.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label10.Size = New System.Drawing.Size(81, 17)
        Me.Label10.TabIndex = 28
        Me.Label10.Text = "File name (*.reg)"
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.SystemColors.Control
        Me.Label9.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label9.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label9.Location = New System.Drawing.Point(16, 56)
        Me.Label9.Name = "Label9"
        Me.Label9.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label9.Size = New System.Drawing.Size(89, 17)
        Me.Label9.TabIndex = 26
        Me.Label9.Text = "Key Value Name:"
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.SystemColors.Control
        Me.Label8.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label8.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label8.Location = New System.Drawing.Point(16, 32)
        Me.Label8.Name = "Label8"
        Me.Label8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label8.Size = New System.Drawing.Size(49, 17)
        Me.Label8.TabIndex = 23
        Me.Label8.Text = "KeyName:"
        '
        'Frame5
        '
        Me.Frame5.BackColor = System.Drawing.SystemColors.Control
        Me.Frame5.Controls.Add(Me.TextKeyEdit)
        Me.Frame5.Controls.Add(Me.GenerateTextButton)
        Me.Frame5.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame5.Location = New System.Drawing.Point(24, 136)
        Me.Frame5.Name = "Frame5"
        Me.Frame5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame5.Size = New System.Drawing.Size(177, 129)
        Me.Frame5.TabIndex = 19
        Me.Frame5.TabStop = False
        Me.Frame5.Text = "Text Key"
        '
        'TextKeyEdit
        '
        Me.TextKeyEdit.AcceptsReturn = True
        Me.TextKeyEdit.BackColor = System.Drawing.SystemColors.Window
        Me.TextKeyEdit.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.TextKeyEdit.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextKeyEdit.ForeColor = System.Drawing.SystemColors.WindowText
        Me.TextKeyEdit.Location = New System.Drawing.Point(24, 48)
        Me.TextKeyEdit.MaxLength = 0
        Me.TextKeyEdit.Multiline = True
        Me.TextKeyEdit.Name = "TextKeyEdit"
        Me.TextKeyEdit.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.TextKeyEdit.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.TextKeyEdit.Size = New System.Drawing.Size(129, 73)
        Me.TextKeyEdit.TabIndex = 21
        '
        'GenerateTextButton
        '
        Me.GenerateTextButton.BackColor = System.Drawing.SystemColors.Control
        Me.GenerateTextButton.Cursor = System.Windows.Forms.Cursors.Default
        Me.GenerateTextButton.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GenerateTextButton.ForeColor = System.Drawing.SystemColors.ControlText
        Me.GenerateTextButton.Location = New System.Drawing.Point(32, 24)
        Me.GenerateTextButton.Name = "GenerateTextButton"
        Me.GenerateTextButton.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.GenerateTextButton.Size = New System.Drawing.Size(113, 17)
        Me.GenerateTextButton.TabIndex = 20
        Me.GenerateTextButton.Text = "Generate Text Key"
        Me.GenerateTextButton.UseVisualStyleBackColor = False
        '
        'Frame4
        '
        Me.Frame4.BackColor = System.Drawing.SystemColors.Control
        Me.Frame4.Controls.Add(Me.GenerateFileButton)
        Me.Frame4.Controls.Add(Me.FileNameEdit)
        Me.Frame4.Controls.Add(Me.Label7)
        Me.Frame4.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame4.Location = New System.Drawing.Point(24, 32)
        Me.Frame4.Name = "Frame4"
        Me.Frame4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame4.Size = New System.Drawing.Size(177, 89)
        Me.Frame4.TabIndex = 15
        Me.Frame4.TabStop = False
        Me.Frame4.Text = "File Key"
        '
        'GenerateFileButton
        '
        Me.GenerateFileButton.BackColor = System.Drawing.SystemColors.Control
        Me.GenerateFileButton.Cursor = System.Windows.Forms.Cursors.Default
        Me.GenerateFileButton.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GenerateFileButton.ForeColor = System.Drawing.SystemColors.ControlText
        Me.GenerateFileButton.Location = New System.Drawing.Point(32, 56)
        Me.GenerateFileButton.Name = "GenerateFileButton"
        Me.GenerateFileButton.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.GenerateFileButton.Size = New System.Drawing.Size(113, 17)
        Me.GenerateFileButton.TabIndex = 18
        Me.GenerateFileButton.Text = "Generate File Key"
        Me.GenerateFileButton.UseVisualStyleBackColor = False
        '
        'FileNameEdit
        '
        Me.FileNameEdit.AcceptsReturn = True
        Me.FileNameEdit.BackColor = System.Drawing.SystemColors.Window
        Me.FileNameEdit.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.FileNameEdit.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FileNameEdit.ForeColor = System.Drawing.SystemColors.WindowText
        Me.FileNameEdit.Location = New System.Drawing.Point(72, 16)
        Me.FileNameEdit.MaxLength = 0
        Me.FileNameEdit.Name = "FileNameEdit"
        Me.FileNameEdit.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FileNameEdit.Size = New System.Drawing.Size(97, 20)
        Me.FileNameEdit.TabIndex = 17
        Me.FileNameEdit.Text = "License.dat"
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.SystemColors.Control
        Me.Label7.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label7.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label7.Location = New System.Drawing.Point(16, 24)
        Me.Label7.Name = "Label7"
        Me.Label7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label7.Size = New System.Drawing.Size(57, 17)
        Me.Label7.TabIndex = 16
        Me.Label7.Text = "File Name:"
        '
        'Frame2
        '
        Me.Frame2.BackColor = System.Drawing.SystemColors.Control
        Me.Frame2.Controls.Add(Me.MaxExecEdit)
        Me.Frame2.Controls.Add(Me.NumDaysEdit)
        Me.Frame2.Controls.Add(Me.Label6)
        Me.Frame2.Controls.Add(Me.Label5)
        Me.Frame2.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame2.Location = New System.Drawing.Point(32, 200)
        Me.Frame2.Name = "Frame2"
        Me.Frame2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame2.Size = New System.Drawing.Size(441, 81)
        Me.Frame2.TabIndex = 9
        Me.Frame2.TabStop = False
        Me.Frame2.Text = "Key Expiration Info"
        '
        'MaxExecEdit
        '
        Me.MaxExecEdit.AcceptsReturn = True
        Me.MaxExecEdit.BackColor = System.Drawing.SystemColors.Window
        Me.MaxExecEdit.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.MaxExecEdit.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MaxExecEdit.ForeColor = System.Drawing.SystemColors.WindowText
        Me.MaxExecEdit.Location = New System.Drawing.Point(304, 24)
        Me.MaxExecEdit.MaxLength = 0
        Me.MaxExecEdit.Name = "MaxExecEdit"
        Me.MaxExecEdit.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.MaxExecEdit.Size = New System.Drawing.Size(97, 20)
        Me.MaxExecEdit.TabIndex = 13
        '
        'NumDaysEdit
        '
        Me.NumDaysEdit.AcceptsReturn = True
        Me.NumDaysEdit.BackColor = System.Drawing.SystemColors.Window
        Me.NumDaysEdit.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.NumDaysEdit.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.NumDaysEdit.ForeColor = System.Drawing.SystemColors.WindowText
        Me.NumDaysEdit.Location = New System.Drawing.Point(104, 24)
        Me.NumDaysEdit.MaxLength = 0
        Me.NumDaysEdit.Name = "NumDaysEdit"
        Me.NumDaysEdit.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.NumDaysEdit.Size = New System.Drawing.Size(97, 20)
        Me.NumDaysEdit.TabIndex = 12
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.SystemColors.Control
        Me.Label6.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label6.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label6.Location = New System.Drawing.Point(216, 32)
        Me.Label6.Name = "Label6"
        Me.Label6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label6.Size = New System.Drawing.Size(89, 17)
        Me.Label6.TabIndex = 11
        Me.Label6.Text = "Max. Executions:"
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.SystemColors.Control
        Me.Label5.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label5.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label5.Location = New System.Drawing.Point(24, 32)
        Me.Label5.Name = "Label5"
        Me.Label5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label5.Size = New System.Drawing.Size(73, 17)
        Me.Label5.TabIndex = 10
        Me.Label5.Text = "Number Days:"
        '
        'Frame1
        '
        Me.Frame1.BackColor = System.Drawing.SystemColors.Control
        Me.Frame1.Controls.Add(Me.CustomName)
        Me.Frame1.Controls.Add(Me.HardIdName)
        Me.Frame1.Controls.Add(Me.CompanyName_Renamed)
        Me.Frame1.Controls.Add(Me.NameEdit)
        Me.Frame1.Controls.Add(Me.Label4)
        Me.Frame1.Controls.Add(Me.Label3)
        Me.Frame1.Controls.Add(Me.Label2)
        Me.Frame1.Controls.Add(Me.Label1)
        Me.Frame1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame1.Location = New System.Drawing.Point(32, 32)
        Me.Frame1.Name = "Frame1"
        Me.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame1.Size = New System.Drawing.Size(441, 153)
        Me.Frame1.TabIndex = 0
        Me.Frame1.TabStop = False
        Me.Frame1.Text = "User's Information"
        '
        'CustomName
        '
        Me.CustomName.AcceptsReturn = True
        Me.CustomName.BackColor = System.Drawing.SystemColors.Window
        Me.CustomName.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.CustomName.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CustomName.ForeColor = System.Drawing.SystemColors.WindowText
        Me.CustomName.Location = New System.Drawing.Point(312, 40)
        Me.CustomName.MaxLength = 0
        Me.CustomName.Multiline = True
        Me.CustomName.Name = "CustomName"
        Me.CustomName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CustomName.Size = New System.Drawing.Size(105, 89)
        Me.CustomName.TabIndex = 8
        '
        'HardIdName
        '
        Me.HardIdName.AcceptsReturn = True
        Me.HardIdName.BackColor = System.Drawing.SystemColors.Window
        Me.HardIdName.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.HardIdName.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.HardIdName.ForeColor = System.Drawing.SystemColors.WindowText
        Me.HardIdName.Location = New System.Drawing.Point(96, 104)
        Me.HardIdName.MaxLength = 0
        Me.HardIdName.Name = "HardIdName"
        Me.HardIdName.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.HardIdName.Size = New System.Drawing.Size(129, 20)
        Me.HardIdName.TabIndex = 7
        '
        'CompanyName_Renamed
        '
        Me.CompanyName_Renamed.AcceptsReturn = True
        Me.CompanyName_Renamed.BackColor = System.Drawing.SystemColors.Window
        Me.CompanyName_Renamed.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.CompanyName_Renamed.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CompanyName_Renamed.ForeColor = System.Drawing.SystemColors.WindowText
        Me.CompanyName_Renamed.Location = New System.Drawing.Point(96, 72)
        Me.CompanyName_Renamed.MaxLength = 0
        Me.CompanyName_Renamed.Name = "CompanyName_Renamed"
        Me.CompanyName_Renamed.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CompanyName_Renamed.Size = New System.Drawing.Size(129, 20)
        Me.CompanyName_Renamed.TabIndex = 6
        '
        'NameEdit
        '
        Me.NameEdit.AcceptsReturn = True
        Me.NameEdit.BackColor = System.Drawing.SystemColors.Window
        Me.NameEdit.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.NameEdit.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.NameEdit.ForeColor = System.Drawing.SystemColors.WindowText
        Me.NameEdit.Location = New System.Drawing.Point(96, 40)
        Me.NameEdit.MaxLength = 0
        Me.NameEdit.Name = "NameEdit"
        Me.NameEdit.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.NameEdit.Size = New System.Drawing.Size(129, 20)
        Me.NameEdit.TabIndex = 5
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.SystemColors.Control
        Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label4.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(240, 40)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.Size = New System.Drawing.Size(73, 17)
        Me.Label4.TabIndex = 4
        Me.Label4.Text = "Custom Data:"
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.SystemColors.Control
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(24, 112)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(73, 17)
        Me.Label3.TabIndex = 3
        Me.Label3.Text = "Hardware ID:"
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(24, 72)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(73, 17)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Organization:"
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(48, 40)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(41, 17)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Name:"
        '
        'Form1
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(514, 600)
        Me.Controls.Add(Me.Frame3)
        Me.Controls.Add(Me.Frame2)
        Me.Controls.Add(Me.Frame1)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Location = New System.Drawing.Point(4, 23)
        Me.Name = "Form1"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "License Key Generator SDK"
        Me.Frame3.ResumeLayout(False)
        Me.Frame7.ResumeLayout(False)
        Me.Frame7.PerformLayout()
        Me.Frame6.ResumeLayout(False)
        Me.Frame6.PerformLayout()
        Me.Frame5.ResumeLayout(False)
        Me.Frame5.PerformLayout()
        Me.Frame4.ResumeLayout(False)
        Me.Frame4.PerformLayout()
        Me.Frame2.ResumeLayout(False)
        Me.Frame2.PerformLayout()
        Me.Frame1.ResumeLayout(False)
        Me.Frame1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
#End Region
#Region "Upgrade Support "
    Private Shared m_vb6FormDefInstance As Form1
    Private Shared m_InitializingDefInstance As Boolean
    Public Shared Property DefInstance() As Form1
        Get
            If m_vb6FormDefInstance Is Nothing OrElse m_vb6FormDefInstance.IsDisposed Then
                m_InitializingDefInstance = True
                m_vb6FormDefInstance = New Form1()
                m_InitializingDefInstance = False
            End If
            DefInstance = m_vb6FormDefInstance
        End Get
        Set(ByVal value As Form1)
            m_vb6FormDefInstance = Value
        End Set
    End Property
#End Region


    Const LicenseHash = "jpEV9ravOc8c18N0EL8sUGMZAUCV4185544B0gm9j12igCnyK3wK8W4bqb1488jtlqcKChjU6gd2GTN4v2D7qJcQjRfI2TM568XAt909Yu9kk2aNAzMfG86yjaxB2h3SUP11Uvr4c1AU8v928k3Oi*eccsGA2AGAQHAABACFACCQLALNL4YVGB6N5DUB7U7AACLSJCMDOTNEBBKAGNB7FZDEW6XTO2FFZ3HNRS7OEB3UGJYII*eccpGBFQGAQHQABACFACCQLALNL4YVGB6N5DUB7U7AACLSJCMDOTNEBBKAGNB7FZDEW6XTO2FFZ3HNRS7OEB3UGJYIICCUALM526QDW3SCAFJOATCHW3LXVKMXNCC6KQ*eccefffbfffb020100028180d46ddd8d68f53f9c60462d61442dfeccc3a52f5ad58c4b5eb588efd7368e269a19b3c30da7ad89799cb985060c4f44b0f7f56172f411f0e04d8162da6117d8560b1471a78dceb4149bcfe11669c80fb40cec7f16a8315287b7e1ebbac7d6d3efb71ae965a0ade24b917dea250b7b79389d00c0c58b3e2bdd4bfa252a747407f30203010001028181004e3f19b898cc9d5db068a91686d29a643a3c8230e730022324a3081531d73288d8a6ca5621495eb618f08cfb2955996bc629f0773347d87e068e7e270a548a2e9b629c2151b72b0aac0200d3aa2574d53320a96fd96e6bb725541ff9b896c2c0499a6a346840605087ab27c93a6e22e084747a2b92e46862ada8345e722add610240f616e197a9bdb7b8e81e2c361677463286820844e9dfc6aba7d275426bd7c22b8a2fc551d03b2f5644cf25543327421707f3be2439af696c463f2b19d670e87d0240dcfbf4b94052af63e61f9b1c309ad1cabdb5f6e0a5033a70c601119ebe263ab600f18c5b38e0382ff72b3b8b0abad0476284dbf17aca11bb92def820c0870d2f024021ee0db4a786acea25b6e349a1d7582a67394e8291ec74abfe3b9a474a2f396a5f672ec19500540a84fa3a9f5d5e3052949a1ac14be2cf9a4a253380207cf8d102401f3262a6147297d81e90afb1171b842a7f05c54889831f28f48b0a15c2fa5aa77f129aa1f631913d1443c2302adf9cafb734d26810d19e1740f0c14ebe5f52790240f2d64b7c7230f754fe170a85f45c6119671dcad68087bcc51591124a60b954bff641d92716d1d9352eeff330ba3632fa33cb41ec3c2985c47680be391a81e812fffefffe02010002820100b8e0db94eefaa9d34422a47d68bcbd46b2871846c18dd40a84d372c60c1f9a534ceed35e7c2e9564d13ccdb89ef672446c2ab17ce27b22fd504cf07dd29dd708c4a2e46c18ec3bf4979fb855a69783b602117f96a4c45aa65408e26ccfa101ff9af3d3f8a70f499056c669b43b90b5368048070a15c86d36b8028d10efd7fd136bbf8b2d31e337ac5bf842b97950e985fd34df00d457c259def8032405a236f949d5f3910f23e2cb48f390c07c8b98b5bd0d9c60991e4898c7a54881f6c7cade25fd6ce4e3bef4a837e8771ea7bd6977b0b51f453831aa44e5d93b77b96214285ce751076b9a6d4f19eba3a4caff1b2c529a2389a99ac664aad673e43879dfff020301000102820100257a0769106ca5a25aec7ed5a136a953bc7ae033efa675765068a09a7ef8a50affcc65f2804fdd1edbbf6ca06130ea06eeba54d25102cf1bf47c343ffd42dbc53e3f1af8c2b09892c36db02b44a2f023241631fbf2988f1baa6b46a2f20fdc5cd927562fd085abcee39842f33323e9680be70dee406e299d6efdad64e289b1163713a5788cd84dedb7e3e6f39e018de8719b9b113af05502296637afb7416206a884b4633be6a7717a9351640a106b6368372a78d977d71c16a0a1a6657fe1362b76562830a90267f9c848bc7d9c0757159a0de31b9636e0165bb6ede70711b49376ebf3b88b6da00af7f503738d75c1e2e36868441f54e64fdb8d02ffcf4341028180c426bb9ee61f123e717f0477d3d9a766ece9e8f5c9ad2ee2620b51ba88499b9b50321d26b6738fc977ed05c2dcf8bdd5acbd92b7830487310fab1ef3fb9fcd47727e7ac061129279e42d9ba2123b7ba9f0831769b1004cb72413270b19b10ecf9bf6a84cf1a67d8119c97a6077aaed2e54ce89ec5c41b4ff9059d5333f6de0e1028180f14999e2b11bf1ab8b5d6baa5c0b5e3781d4704175a9357a68bde2883ed5334a319ca6d6de629a2ff7aa603959e290ce26801f3e100e4c4dba8ba4f54d9b49a55550796d1590496b63bcf7913233317a2db40de2c8a0863a0171f619d761c4e557df09d60abe61ee52eaaf02d5ec1dd16d52d48757f43f66212335d55b967cdf0281803b756a463e8788a6390d41b3791a6f2e39fea5c96bbafd8d4e1f144530eeef266366ed8340a7431cd5279a29798645353249bce1b7eb75779fc2457dbbf6b2e94781a380116ce9cddd271e1840e29b17e67a366ca964e7dc5429221ab14d04aa5622acddb61abbcc989e84641da2488754d9cee1410e01e9be3e1cec8dfba6c10281801099ccc1a107d827c24dea236d73c6c6b0e309da271b3e37aecf29afd00ae11d80ae53784312aba3133a20a6b17d7a1304e919bd2e639d72579c55fc1e9bc9845b8b4edf677566e7c193f5c082a0ad0dc7e9346f5ef85463d7479cf408328cd66ea644c9fa314111a198cdf3b6fe28cb08e78301d0d6582a51ab577fd76e5227028180993a9f45c6fdf5419e7b1691733e86ada7fa40487c26e298bdcaf552617e10ae1b906276cbf31eca0dc9d29781663932eecbd1a2fa118bf2fccdd746e1fdecb10695063945e884feb93d5b06f6156dea489f82e5203cb93fead7aebb7d3c40329101b8218f3d1cd46bdf1f7837ccd68873f854fcb600423948f9c9d956298e2a"

    Private Sub GenerateFileButton_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles GenerateFileButton.Click

        Dim UserName As String
        Dim Organization As String
        Dim HardId As String
        Dim Custom As String
        Dim NumDays As Integer
        Dim NumExec As Integer
        Dim BufferOut(4000) As Byte
        Dim SizeKey As Integer

        Dim ExpDate As SYSTEMTIME = New SYSTEMTIME

        ' example expiration date 

        ExpDate.wDay = 14
        ExpDate.wMonth = 5
        ExpDate.wYear = 2008

        If NameEdit.Text = "" Then

            UserName = vbNullString

        Else

            UserName = NameEdit.Text

        End If

        If CompanyName_Renamed.Text = "" Then

            Organization = vbNullString

        Else

            Organization = CompanyName_Renamed.Text

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

            NumDays = CInt(NumDaysEdit.Text)

        End If

        If MaxExecEdit.Text = "" Then

            NumExec = 0

        Else

            NumExec = CInt(MaxExecEdit.Text)

        End If

        ' create the file key

        'SizeKey = WLGenLicenseFileKey(LicenseHash, UserName, Organization, Custom, HardId, NumDays, NumExec, ExpDate, 0, 0, 0, BufferOut)

        SizeKey = WLGenLicenseFileKey(LicenseHash, UserName, Organization, Custom, HardId, NumDays, NumExec, 0, 0, 0, 0, BufferOut)

        'store key in file

        Dim oFileStream As System.IO.FileStream

        oFileStream = New System.IO.FileStream(VB6.GetPath & "\" & FileNameEdit.Text, System.IO.FileMode.Create)
        oFileStream.Write(BufferOut, 0, SizeKey)
        oFileStream.Close()

        MsgBox("A license key file key has been generated", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "WinLicense SDK")

    End Sub

    Private Sub GenerateSmartButton_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles GenerateSmartButton.Click

        Dim UserName As String
        Dim Organization As String
        Dim HardId As String
        Dim Custom As String
        Dim NumDays As Integer
        Dim NumExec As Integer
        Dim BufferOut As StringBuilder = New StringBuilder(1000)
        Dim ExpDate As SYSTEMTIME = New SYSTEMTIME

        ' example expiration date 

        ExpDate.wDay = 14
        ExpDate.wMonth = 5
        ExpDate.wYear = 2008

        If NameEdit.Text = "" Then

            UserName = vbNullString

        Else

            UserName = NameEdit.Text

        End If

        If CompanyName_Renamed.Text = "" Then

            Organization = vbNullString

        Else

            Organization = CompanyName_Renamed.Text

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

            NumDays = CInt(NumDaysEdit.Text)

        End If

        If MaxExecEdit.Text = "" Then

            NumExec = 0

        Else

            NumExec = CInt(MaxExecEdit.Text)

        End If

        ' create the file key

        'WLGenLicenseSmartKey(LicenseHash, UserName, Organization, Custom, HardId, NumDays, NumExec, ExpDate, BufferOut)
        WLGenLicenseSmartKey(LicenseHash, UserName, Organization, Custom, HardId, NumDays, NumExec, 0, BufferOut)

        ' show key on GUI

        SmartKeyEdit.Text = BufferOut.ToString

    End Sub

    Private Sub GenerateTextButton_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles GenerateTextButton.Click
        Dim UserName As String
        Dim Organization As String
        Dim HardId As String
        Dim Custom As String
        Dim NumDays As Integer
        Dim NumExec As Integer
        Dim BufferOut As StringBuilder = New StringBuilder(4000)
        Dim ExpDate As SYSTEMTIME = New SYSTEMTIME

        ' example expiration date 

        ExpDate.wDay = 14
        ExpDate.wMonth = 5
        ExpDate.wYear = 2008

        If NameEdit.Text = "" Then

            UserName = vbNullString

        Else

            UserName = NameEdit.Text

        End If

        If CompanyName_Renamed.Text = "" Then

            Organization = vbNullString

        Else

            Organization = CompanyName_Renamed.Text

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

            NumDays = CInt(NumDaysEdit.Text)

        End If

        If MaxExecEdit.Text = "" Then

            NumExec = 0

        Else

            NumExec = CInt(MaxExecEdit.Text)

        End If

        ' create the file key

        'WLGenLicenseTextKey(LicenseHash, UserName, Organization, Custom, HardId, NumDays, NumExec, ExpDate, 0, 0, 0, BufferOut)
        WLGenLicenseTextKey(LicenseHash, UserName, Organization, Custom, HardId, NumDays, NumExec, 0, 0, 0, 0, BufferOut)

        ' show key on GUI

        TextKeyEdit.Text = BufferOut.ToString

    End Sub

    Private Sub GenerateRegButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GenerateRegButton.Click
        Dim UserName As String
        Dim Organization As String
        Dim HardId As String
        Dim Custom As String
        Dim NumDays As Integer
        Dim NumExec As Integer
        Dim BufferOut(4000) As Byte
        Dim SizeKey As Integer
        Dim ExpDate As SYSTEMTIME = New SYSTEMTIME

        ' example expiration date 

        ExpDate.wDay = 14
        ExpDate.wMonth = 5
        ExpDate.wYear = 2008

        If NameEdit.Text = "" Then

            UserName = vbNullString

        Else

            UserName = NameEdit.Text

        End If

        If CompanyName_Renamed.Text = "" Then

            Organization = vbNullString

        Else

            Organization = CompanyName_Renamed.Text

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

            NumDays = CInt(NumDaysEdit.Text)

        End If

        If MaxExecEdit.Text = "" Then

            NumExec = 0

        Else

            NumExec = CInt(MaxExecEdit.Text)

        End If

        ' create the file key

        'SizeKey = WLGenLicenseRegistryKey(LicenseHash, UserName, Organization, Custom, HardId, NumDays, NumExec, ExpDate, 0, 0, 0, "HKEY_LOCAL_MACHINE" + KeyNameEdit.Text, KeyValueNameEdit.Text, BufferOut)
        SizeKey = WLGenLicenseRegistryKey(LicenseHash, UserName, Organization, Custom, HardId, NumDays, NumExec, 0, 0, 0, 0, "HKEY_LOCAL_MACHINE" + KeyNameEdit.Text, KeyValueNameEdit.Text, BufferOut)

        'store key in file

        Dim oFileStream As System.IO.FileStream

        oFileStream = New System.IO.FileStream(VB6.GetPath & "\" & RegNameEdit.Text, System.IO.FileMode.Create)
        oFileStream.Write(BufferOut, 0, SizeKey)
        oFileStream.Close()

        MsgBox("A license key file key has been generated", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "WinLicense SDK")


    End Sub
End Class
