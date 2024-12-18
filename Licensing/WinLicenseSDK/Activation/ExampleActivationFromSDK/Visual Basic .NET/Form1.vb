Option Strict Off
Option Explicit On
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
    Public WithEvents Frame4 As System.Windows.Forms.GroupBox
    Public WithEvents RegExpDateEdit As System.Windows.Forms.TextBox
    Public WithEvents CustomEdit As System.Windows.Forms.TextBox
    Public WithEvents CompanyEdit As System.Windows.Forms.TextBox
    Public WithEvents NameEdit As System.Windows.Forms.TextBox
    Public WithEvents Label19 As System.Windows.Forms.Label
    Public WithEvents RegExecLeftLabel As System.Windows.Forms.Label
    Public WithEvents Label17 As System.Windows.Forms.Label
    Public WithEvents RegDaysLeftLabel As System.Windows.Forms.Label
    Public WithEvents Label15 As System.Windows.Forms.Label
    Public WithEvents Label14 As System.Windows.Forms.Label
    Public WithEvents Label13 As System.Windows.Forms.Label
    Public WithEvents Label12 As System.Windows.Forms.Label
    Public WithEvents Frame3 As System.Windows.Forms.GroupBox
    Public WithEvents ExpDateEdit As System.Windows.Forms.TextBox
    Public WithEvents RuntimeLabel As System.Windows.Forms.Label
    Public WithEvents MinutesLabel As System.Windows.Forms.Label
    Public WithEvents TrialExecLeftLabel As System.Windows.Forms.Label
    Public WithEvents TrialDaysLeftLabel As System.Windows.Forms.Label
    Public WithEvents Label7 As System.Windows.Forms.Label
    Public WithEvents Label6 As System.Windows.Forms.Label
    Public WithEvents lael2 As System.Windows.Forms.Label
    Public WithEvents Label4 As System.Windows.Forms.Label
    Public WithEvents Label3 As System.Windows.Forms.Label
    Public WithEvents Frame2 As System.Windows.Forms.GroupBox
    Public WithEvents CheckStatusButton As System.Windows.Forms.Button
    Public WithEvents StatusLabel As System.Windows.Forms.Label
    Public WithEvents Label1 As System.Windows.Forms.Label
    Private WithEvents DeactivationEdit As System.Windows.Forms.TextBox
    Private WithEvents DeactivateButton As System.Windows.Forms.Button
    Private WithEvents ActivationEdit As System.Windows.Forms.TextBox
    Private WithEvents ActivateButton As System.Windows.Forms.Button
    Public WithEvents Frame1 As System.Windows.Forms.GroupBox
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.Frame4 = New System.Windows.Forms.GroupBox()
        Me.DeactivationEdit = New System.Windows.Forms.TextBox()
        Me.DeactivateButton = New System.Windows.Forms.Button()
        Me.ActivationEdit = New System.Windows.Forms.TextBox()
        Me.ActivateButton = New System.Windows.Forms.Button()
        Me.Frame3 = New System.Windows.Forms.GroupBox()
        Me.RegExpDateEdit = New System.Windows.Forms.TextBox()
        Me.CustomEdit = New System.Windows.Forms.TextBox()
        Me.CompanyEdit = New System.Windows.Forms.TextBox()
        Me.NameEdit = New System.Windows.Forms.TextBox()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.RegExecLeftLabel = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.RegDaysLeftLabel = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Frame2 = New System.Windows.Forms.GroupBox()
        Me.ExpDateEdit = New System.Windows.Forms.TextBox()
        Me.RuntimeLabel = New System.Windows.Forms.Label()
        Me.MinutesLabel = New System.Windows.Forms.Label()
        Me.TrialExecLeftLabel = New System.Windows.Forms.Label()
        Me.TrialDaysLeftLabel = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.lael2 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Frame1 = New System.Windows.Forms.GroupBox()
        Me.CheckStatusButton = New System.Windows.Forms.Button()
        Me.StatusLabel = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Frame4.SuspendLayout()
        Me.Frame3.SuspendLayout()
        Me.Frame2.SuspendLayout()
        Me.Frame1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Frame4
        '
        Me.Frame4.BackColor = System.Drawing.SystemColors.Control
        Me.Frame4.Controls.Add(Me.DeactivationEdit)
        Me.Frame4.Controls.Add(Me.DeactivateButton)
        Me.Frame4.Controls.Add(Me.ActivationEdit)
        Me.Frame4.Controls.Add(Me.ActivateButton)
        Me.Frame4.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame4.Location = New System.Drawing.Point(29, 526)
        Me.Frame4.Name = "Frame4"
        Me.Frame4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame4.Size = New System.Drawing.Size(452, 132)
        Me.Frame4.TabIndex = 28
        Me.Frame4.TabStop = False
        Me.Frame4.Text = "Hardwaer ID"
        '
        'DeactivationEdit
        '
        Me.DeactivationEdit.Location = New System.Drawing.Point(157, 79)
        Me.DeactivationEdit.Name = "DeactivationEdit"
        Me.DeactivationEdit.Size = New System.Drawing.Size(280, 21)
        Me.DeactivationEdit.TabIndex = 7
        '
        'DeactivateButton
        '
        Me.DeactivateButton.Location = New System.Drawing.Point(22, 77)
        Me.DeactivateButton.Name = "DeactivateButton"
        Me.DeactivateButton.Size = New System.Drawing.Size(112, 23)
        Me.DeactivateButton.TabIndex = 6
        Me.DeactivateButton.Text = "Deactivate"
        '
        'ActivationEdit
        '
        Me.ActivationEdit.Location = New System.Drawing.Point(157, 38)
        Me.ActivationEdit.Name = "ActivationEdit"
        Me.ActivationEdit.Size = New System.Drawing.Size(280, 21)
        Me.ActivationEdit.TabIndex = 5
        '
        'ActivateButton
        '
        Me.ActivateButton.Location = New System.Drawing.Point(22, 36)
        Me.ActivateButton.Name = "ActivateButton"
        Me.ActivateButton.Size = New System.Drawing.Size(112, 23)
        Me.ActivateButton.TabIndex = 4
        Me.ActivateButton.Text = "Activate"
        '
        'Frame3
        '
        Me.Frame3.BackColor = System.Drawing.SystemColors.Control
        Me.Frame3.Controls.Add(Me.RegExpDateEdit)
        Me.Frame3.Controls.Add(Me.CustomEdit)
        Me.Frame3.Controls.Add(Me.CompanyEdit)
        Me.Frame3.Controls.Add(Me.NameEdit)
        Me.Frame3.Controls.Add(Me.Label19)
        Me.Frame3.Controls.Add(Me.RegExecLeftLabel)
        Me.Frame3.Controls.Add(Me.Label17)
        Me.Frame3.Controls.Add(Me.RegDaysLeftLabel)
        Me.Frame3.Controls.Add(Me.Label15)
        Me.Frame3.Controls.Add(Me.Label14)
        Me.Frame3.Controls.Add(Me.Label13)
        Me.Frame3.Controls.Add(Me.Label12)
        Me.Frame3.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame3.Location = New System.Drawing.Point(29, 327)
        Me.Frame3.Name = "Frame3"
        Me.Frame3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame3.Size = New System.Drawing.Size(452, 182)
        Me.Frame3.TabIndex = 15
        Me.Frame3.TabStop = False
        Me.Frame3.Text = "License Information"
        '
        'RegExpDateEdit
        '
        Me.RegExpDateEdit.AcceptsReturn = True
        Me.RegExpDateEdit.BackColor = System.Drawing.SystemColors.Window
        Me.RegExpDateEdit.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.RegExpDateEdit.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RegExpDateEdit.ForeColor = System.Drawing.SystemColors.WindowText
        Me.RegExpDateEdit.Location = New System.Drawing.Point(321, 129)
        Me.RegExpDateEdit.MaxLength = 0
        Me.RegExpDateEdit.Name = "RegExpDateEdit"
        Me.RegExpDateEdit.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.RegExpDateEdit.Size = New System.Drawing.Size(116, 21)
        Me.RegExpDateEdit.TabIndex = 27
        '
        'CustomEdit
        '
        Me.CustomEdit.AcceptsReturn = True
        Me.CustomEdit.BackColor = System.Drawing.SystemColors.Window
        Me.CustomEdit.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.CustomEdit.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CustomEdit.ForeColor = System.Drawing.SystemColors.WindowText
        Me.CustomEdit.Location = New System.Drawing.Point(321, 34)
        Me.CustomEdit.MaxLength = 0
        Me.CustomEdit.Multiline = True
        Me.CustomEdit.Name = "CustomEdit"
        Me.CustomEdit.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CustomEdit.Size = New System.Drawing.Size(116, 62)
        Me.CustomEdit.TabIndex = 21
        '
        'CompanyEdit
        '
        Me.CompanyEdit.AcceptsReturn = True
        Me.CompanyEdit.BackColor = System.Drawing.SystemColors.Window
        Me.CompanyEdit.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.CompanyEdit.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CompanyEdit.ForeColor = System.Drawing.SystemColors.WindowText
        Me.CompanyEdit.Location = New System.Drawing.Point(86, 69)
        Me.CompanyEdit.MaxLength = 0
        Me.CompanyEdit.Name = "CompanyEdit"
        Me.CompanyEdit.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CompanyEdit.Size = New System.Drawing.Size(136, 21)
        Me.CompanyEdit.TabIndex = 19
        '
        'NameEdit
        '
        Me.NameEdit.AcceptsReturn = True
        Me.NameEdit.BackColor = System.Drawing.SystemColors.Window
        Me.NameEdit.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.NameEdit.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.NameEdit.ForeColor = System.Drawing.SystemColors.WindowText
        Me.NameEdit.Location = New System.Drawing.Point(86, 34)
        Me.NameEdit.MaxLength = 0
        Me.NameEdit.Name = "NameEdit"
        Me.NameEdit.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.NameEdit.Size = New System.Drawing.Size(136, 21)
        Me.NameEdit.TabIndex = 18
        '
        'Label19
        '
        Me.Label19.BackColor = System.Drawing.SystemColors.Control
        Me.Label19.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label19.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label19.Location = New System.Drawing.Point(230, 138)
        Me.Label19.Name = "Label19"
        Me.Label19.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label19.Size = New System.Drawing.Size(98, 27)
        Me.Label19.TabIndex = 26
        Me.Label19.Text = "Expiration Date:"
        '
        'RegExecLeftLabel
        '
        Me.RegExecLeftLabel.BackColor = System.Drawing.SystemColors.Control
        Me.RegExecLeftLabel.Cursor = System.Windows.Forms.Cursors.Default
        Me.RegExecLeftLabel.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RegExecLeftLabel.ForeColor = System.Drawing.SystemColors.Highlight
        Me.RegExecLeftLabel.Location = New System.Drawing.Point(115, 138)
        Me.RegExecLeftLabel.Name = "RegExecLeftLabel"
        Me.RegExecLeftLabel.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.RegExecLeftLabel.Size = New System.Drawing.Size(69, 27)
        Me.RegExecLeftLabel.TabIndex = 25
        '
        'Label17
        '
        Me.Label17.BackColor = System.Drawing.SystemColors.Control
        Me.Label17.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label17.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label17.Location = New System.Drawing.Point(19, 146)
        Me.Label17.Name = "Label17"
        Me.Label17.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label17.Size = New System.Drawing.Size(97, 27)
        Me.Label17.TabIndex = 24
        Me.Label17.Text = "Executions Left:"
        '
        'RegDaysLeftLabel
        '
        Me.RegDaysLeftLabel.BackColor = System.Drawing.SystemColors.Control
        Me.RegDaysLeftLabel.Cursor = System.Windows.Forms.Cursors.Default
        Me.RegDaysLeftLabel.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RegDaysLeftLabel.ForeColor = System.Drawing.SystemColors.Highlight
        Me.RegDaysLeftLabel.Location = New System.Drawing.Point(96, 103)
        Me.RegDaysLeftLabel.Name = "RegDaysLeftLabel"
        Me.RegDaysLeftLabel.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.RegDaysLeftLabel.Size = New System.Drawing.Size(68, 27)
        Me.RegDaysLeftLabel.TabIndex = 23
        '
        'Label15
        '
        Me.Label15.BackColor = System.Drawing.SystemColors.Control
        Me.Label15.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label15.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label15.Location = New System.Drawing.Point(19, 112)
        Me.Label15.Name = "Label15"
        Me.Label15.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label15.Size = New System.Drawing.Size(69, 18)
        Me.Label15.TabIndex = 22
        Me.Label15.Text = "Days Left:"
        '
        'Label14
        '
        Me.Label14.BackColor = System.Drawing.SystemColors.Control
        Me.Label14.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label14.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label14.Location = New System.Drawing.Point(249, 34)
        Me.Label14.Name = "Label14"
        Me.Label14.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label14.Size = New System.Drawing.Size(79, 19)
        Me.Label14.TabIndex = 20
        Me.Label14.Text = "Custom:"
        '
        'Label13
        '
        Me.Label13.BackColor = System.Drawing.SystemColors.Control
        Me.Label13.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label13.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label13.Location = New System.Drawing.Point(19, 69)
        Me.Label13.Name = "Label13"
        Me.Label13.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label13.Size = New System.Drawing.Size(69, 18)
        Me.Label13.TabIndex = 17
        Me.Label13.Text = "Company:"
        '
        'Label12
        '
        Me.Label12.BackColor = System.Drawing.SystemColors.Control
        Me.Label12.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label12.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label12.Location = New System.Drawing.Point(38, 43)
        Me.Label12.Name = "Label12"
        Me.Label12.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label12.Size = New System.Drawing.Size(50, 18)
        Me.Label12.TabIndex = 16
        Me.Label12.Text = "Name:"
        '
        'Frame2
        '
        Me.Frame2.BackColor = System.Drawing.SystemColors.Control
        Me.Frame2.Controls.Add(Me.ExpDateEdit)
        Me.Frame2.Controls.Add(Me.RuntimeLabel)
        Me.Frame2.Controls.Add(Me.MinutesLabel)
        Me.Frame2.Controls.Add(Me.TrialExecLeftLabel)
        Me.Frame2.Controls.Add(Me.TrialDaysLeftLabel)
        Me.Frame2.Controls.Add(Me.Label7)
        Me.Frame2.Controls.Add(Me.Label6)
        Me.Frame2.Controls.Add(Me.lael2)
        Me.Frame2.Controls.Add(Me.Label4)
        Me.Frame2.Controls.Add(Me.Label3)
        Me.Frame2.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame2.Location = New System.Drawing.Point(29, 172)
        Me.Frame2.Name = "Frame2"
        Me.Frame2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame2.Size = New System.Drawing.Size(452, 139)
        Me.Frame2.TabIndex = 4
        Me.Frame2.TabStop = False
        Me.Frame2.Text = "Trial Information"
        '
        'ExpDateEdit
        '
        Me.ExpDateEdit.AcceptsReturn = True
        Me.ExpDateEdit.BackColor = System.Drawing.SystemColors.Window
        Me.ExpDateEdit.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.ExpDateEdit.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ExpDateEdit.ForeColor = System.Drawing.SystemColors.WindowText
        Me.ExpDateEdit.Location = New System.Drawing.Point(115, 86)
        Me.ExpDateEdit.MaxLength = 0
        Me.ExpDateEdit.Name = "ExpDateEdit"
        Me.ExpDateEdit.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ExpDateEdit.Size = New System.Drawing.Size(145, 21)
        Me.ExpDateEdit.TabIndex = 14
        '
        'RuntimeLabel
        '
        Me.RuntimeLabel.BackColor = System.Drawing.SystemColors.Control
        Me.RuntimeLabel.Cursor = System.Windows.Forms.Cursors.Default
        Me.RuntimeLabel.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RuntimeLabel.ForeColor = System.Drawing.SystemColors.Highlight
        Me.RuntimeLabel.Location = New System.Drawing.Point(298, 52)
        Me.RuntimeLabel.Name = "RuntimeLabel"
        Me.RuntimeLabel.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.RuntimeLabel.Size = New System.Drawing.Size(68, 27)
        Me.RuntimeLabel.TabIndex = 13
        '
        'MinutesLabel
        '
        Me.MinutesLabel.BackColor = System.Drawing.SystemColors.Control
        Me.MinutesLabel.Cursor = System.Windows.Forms.Cursors.Default
        Me.MinutesLabel.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MinutesLabel.ForeColor = System.Drawing.SystemColors.Highlight
        Me.MinutesLabel.Location = New System.Drawing.Point(307, 17)
        Me.MinutesLabel.Name = "MinutesLabel"
        Me.MinutesLabel.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.MinutesLabel.Size = New System.Drawing.Size(69, 27)
        Me.MinutesLabel.TabIndex = 12
        '
        'TrialExecLeftLabel
        '
        Me.TrialExecLeftLabel.BackColor = System.Drawing.SystemColors.Control
        Me.TrialExecLeftLabel.Cursor = System.Windows.Forms.Cursors.Default
        Me.TrialExecLeftLabel.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TrialExecLeftLabel.ForeColor = System.Drawing.SystemColors.Highlight
        Me.TrialExecLeftLabel.Location = New System.Drawing.Point(125, 52)
        Me.TrialExecLeftLabel.Name = "TrialExecLeftLabel"
        Me.TrialExecLeftLabel.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.TrialExecLeftLabel.Size = New System.Drawing.Size(68, 27)
        Me.TrialExecLeftLabel.TabIndex = 11
        '
        'TrialDaysLeftLabel
        '
        Me.TrialDaysLeftLabel.BackColor = System.Drawing.SystemColors.Control
        Me.TrialDaysLeftLabel.Cursor = System.Windows.Forms.Cursors.Default
        Me.TrialDaysLeftLabel.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TrialDaysLeftLabel.ForeColor = System.Drawing.SystemColors.Highlight
        Me.TrialDaysLeftLabel.Location = New System.Drawing.Point(106, 17)
        Me.TrialDaysLeftLabel.Name = "TrialDaysLeftLabel"
        Me.TrialDaysLeftLabel.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.TrialDaysLeftLabel.Size = New System.Drawing.Size(68, 27)
        Me.TrialDaysLeftLabel.TabIndex = 10
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.SystemColors.Control
        Me.Label7.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label7.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label7.Location = New System.Drawing.Point(19, 95)
        Me.Label7.Name = "Label7"
        Me.Label7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label7.Size = New System.Drawing.Size(97, 18)
        Me.Label7.TabIndex = 9
        Me.Label7.Text = "Expiration Date:"
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.SystemColors.Control
        Me.Label6.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label6.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label6.Location = New System.Drawing.Point(211, 60)
        Me.Label6.Name = "Label6"
        Me.Label6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label6.Size = New System.Drawing.Size(88, 19)
        Me.Label6.TabIndex = 8
        Me.Label6.Text = "Runtime Left:"
        '
        'lael2
        '
        Me.lael2.BackColor = System.Drawing.SystemColors.Control
        Me.lael2.Cursor = System.Windows.Forms.Cursors.Default
        Me.lael2.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lael2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lael2.Location = New System.Drawing.Point(211, 26)
        Me.lael2.Name = "lael2"
        Me.lael2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lael2.Size = New System.Drawing.Size(88, 18)
        Me.lael2.TabIndex = 7
        Me.lael2.Text = "Minutes Left:"
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.SystemColors.Control
        Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label4.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(29, 60)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.Size = New System.Drawing.Size(107, 27)
        Me.Label4.TabIndex = 6
        Me.Label4.Text = "Executions Left:"
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.SystemColors.Control
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(29, 26)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(78, 18)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "Days Left:"
        '
        'Frame1
        '
        Me.Frame1.BackColor = System.Drawing.SystemColors.Control
        Me.Frame1.Controls.Add(Me.CheckStatusButton)
        Me.Frame1.Controls.Add(Me.StatusLabel)
        Me.Frame1.Controls.Add(Me.Label1)
        Me.Frame1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame1.Location = New System.Drawing.Point(29, 26)
        Me.Frame1.Name = "Frame1"
        Me.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame1.Size = New System.Drawing.Size(452, 122)
        Me.Frame1.TabIndex = 0
        Me.Frame1.TabStop = False
        Me.Frame1.Text = "Application Status"
        '
        'CheckStatusButton
        '
        Me.CheckStatusButton.BackColor = System.Drawing.SystemColors.Control
        Me.CheckStatusButton.Cursor = System.Windows.Forms.Cursors.Default
        Me.CheckStatusButton.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CheckStatusButton.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CheckStatusButton.Location = New System.Drawing.Point(125, 26)
        Me.CheckStatusButton.Name = "CheckStatusButton"
        Me.CheckStatusButton.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CheckStatusButton.Size = New System.Drawing.Size(193, 27)
        Me.CheckStatusButton.TabIndex = 1
        Me.CheckStatusButton.Text = "Check Application Status"
        Me.CheckStatusButton.UseVisualStyleBackColor = False
        '
        'StatusLabel
        '
        Me.StatusLabel.BackColor = System.Drawing.SystemColors.Control
        Me.StatusLabel.Cursor = System.Windows.Forms.Cursors.Default
        Me.StatusLabel.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.StatusLabel.ForeColor = System.Drawing.SystemColors.Highlight
        Me.StatusLabel.Location = New System.Drawing.Point(67, 69)
        Me.StatusLabel.Name = "StatusLabel"
        Me.StatusLabel.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StatusLabel.Size = New System.Drawing.Size(385, 44)
        Me.StatusLabel.TabIndex = 3
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(19, 78)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(49, 26)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Status:"
        '
        'Form1
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 14)
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(486, 670)
        Me.Controls.Add(Me.Frame4)
        Me.Controls.Add(Me.Frame3)
        Me.Controls.Add(Me.Frame2)
        Me.Controls.Add(Me.Frame1)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Location = New System.Drawing.Point(4, 23)
        Me.Name = "Form1"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Trial-Registration SDK"
        Me.Frame4.ResumeLayout(False)
        Me.Frame4.PerformLayout()
        Me.Frame3.ResumeLayout(False)
        Me.Frame3.PerformLayout()
        Me.Frame2.ResumeLayout(False)
        Me.Frame2.PerformLayout()
        Me.Frame1.ResumeLayout(False)
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
        Set(value As Form1)
            m_vb6FormDefInstance = value
        End Set
    End Property
#End Region



    Private Sub CheckStatusButton_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CheckStatusButton.Click

        Dim AppStatus As Short
        Dim BufferVarValue As New System.Text.StringBuilder(100)
        Dim Status As Short
        Dim LicenseInfo(3) As String

        GetEnvironmentVariable("WLRegGetStatus", BufferVarValue, 100)

        ' show application status

        Status = System.Convert.ToInt32(BufferVarValue.ToString())

        Select Case Status

            Case 0
                StatusLabel.Text = "Trial"

            Case 1
                StatusLabel.Text = "Registered"

            Case 2
                StatusLabel.Text = "Invalid License"

            Case 3
                StatusLabel.Text = "License Locked to different machine"

            Case 4
                StatusLabel.Text = "No more HW-ID changes allowed"

            Case 5
                StatusLabel.Text = "License Key expired"

        End Select

        ' show information on the GUI

        If Status <> 1 Then

            ' application is in trial mode

            GetEnvironmentVariable("WLTrialDaysLeft", BufferVarValue, 100)
            TrialDaysLeftLabel.Text = BufferVarValue.ToString()

            GetEnvironmentVariable("WLTrialExecutionsLeft", BufferVarValue, 100)
            TrialExecLeftLabel.Text = BufferVarValue.ToString()

            GetEnvironmentVariable("WLTrialGlobalTimeLeft", BufferVarValue, 100)
            MinutesLabel.Text = BufferVarValue.ToString()

            GetEnvironmentVariable("WLTrialRuntimeLeft", BufferVarValue, 100)
            RuntimeLabel.Text = BufferVarValue.ToString()

            GetEnvironmentVariable("WLTrialExpirationDate", BufferVarValue, 100)
            ExpDateEdit.Text = BufferVarValue.ToString()

            ' set as empty the registration data

            NameEdit.Text = ""
            CompanyEdit.Text = ""
            CustomEdit.Text = ""
            RegDaysLeftLabel.Text = ""
            RegExecLeftLabel.Text = ""

        Else

            ' application is registered

            GetEnvironmentVariable("WLRegGetLicenseInfo", BufferVarValue, 100)
            LicenseInfo = BufferVarValue.ToString().Split(",")

            NameEdit.Text = LicenseInfo(0)
            CompanyEdit.Text = LicenseInfo(1)
            CustomEdit.Text = LicenseInfo(2)

            GetEnvironmentVariable("WLRegDaysLeft", BufferVarValue, 100)
            RegDaysLeftLabel.Text = BufferVarValue.ToString()

            GetEnvironmentVariable("WLRegExecutionsLeft", BufferVarValue, 100)
            RegExecLeftLabel.Text = BufferVarValue.ToString()

            GetEnvironmentVariable("WLRegExpirationDate", BufferVarValue, 100)
            RegExpDateEdit.Text = BufferVarValue.ToString()

            ' set as empty the trial data

            TrialDaysLeftLabel.Text = ""
            TrialExecLeftLabel.Text = ""
            MinutesLabel.Text = ""
            RuntimeLabel.Text = ""
        End If
    End Sub

    Private Sub ActivateButton_Click(sender As System.Object, e As System.EventArgs) Handles ActivateButton.Click
        Dim BufferVarValue As New System.Text.StringBuilder(4096)

        BufferVarValue.Append(ActivationEdit.Text)
        GetEnvironmentVariable("WLRegActivateSoftware", BufferVarValue, 4096)
        Dim ParsedBufferOut As String() = BufferVarValue.ToString.Split(",")

        If ParsedBufferOut(0) = "0" Then
            MessageBox.Show("Activation code is valid. Application is going to restart", "Activation")
            GetEnvironmentVariable("WLRestartApplication", BufferVarValue, 10)
        Else
            MessageBox.Show("Activation code is INVALID. Error code = " & ParsedBufferOut(0) & vbCrLf &
                             vbCrLf & "Server Output = " + BufferVarValue.ToString(), "ERROR")
        End If
    End Sub

    Private Sub DeactivateButton_Click(sender As System.Object, e As System.EventArgs) Handles DeactivateButton.Click
        Dim BufferVarValue As New System.Text.StringBuilder(4096)

        BufferVarValue.Append(DeactivationEdit.Text)
        GetEnvironmentVariable("WLRegDeactivateSoftware", BufferVarValue, 4096)
        Dim ParsedBufferOut As String() = BufferVarValue.ToString.Split(",")

        If ParsedBufferOut(0) = "0" Then
            MessageBox.Show("Application has been deactivated. Application is going to restart", "Activation")
            GetEnvironmentVariable("WLRestartApplication", BufferVarValue, 10)
        Else
            MessageBox.Show("Activation code is INVALID. Error code = " & ParsedBufferOut(0) & vbCrLf &
                             vbCrLf & "Server Output = " + BufferVarValue.ToString(), "ERROR")
        End If
    End Sub

    Private Sub Form1_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        ' We make the following call to allow testing in an unprotected state.
        ' NOTE: In your release version, before protecting, you have to REMOVE this call!!!

        'WLLoadWinlicenseDll()
    End Sub
End Class