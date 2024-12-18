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
	Public WithEvents HardIdEdit As System.Windows.Forms.TextBox
	Public WithEvents HardIdButton As System.Windows.Forms.Button
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
	Public WithEvents Frame1 As System.Windows.Forms.GroupBox
	'NOTE: The following procedure is required by the Windows Form Designer
	'It can be modified using the Windows Form Designer.
	'Do not modify it using the code editor.
	<System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
		Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(Form1))
		Me.components = New System.ComponentModel.Container()
		Me.ToolTip1 = New System.Windows.Forms.ToolTip(components)
		Me.ToolTip1.Active = True
		Me.Frame4 = New System.Windows.Forms.GroupBox
		Me.HardIdEdit = New System.Windows.Forms.TextBox
		Me.HardIdButton = New System.Windows.Forms.Button
		Me.Frame3 = New System.Windows.Forms.GroupBox
		Me.RegExpDateEdit = New System.Windows.Forms.TextBox
		Me.CustomEdit = New System.Windows.Forms.TextBox
		Me.CompanyEdit = New System.Windows.Forms.TextBox
		Me.NameEdit = New System.Windows.Forms.TextBox
		Me.Label19 = New System.Windows.Forms.Label
		Me.RegExecLeftLabel = New System.Windows.Forms.Label
		Me.Label17 = New System.Windows.Forms.Label
		Me.RegDaysLeftLabel = New System.Windows.Forms.Label
		Me.Label15 = New System.Windows.Forms.Label
		Me.Label14 = New System.Windows.Forms.Label
		Me.Label13 = New System.Windows.Forms.Label
		Me.Label12 = New System.Windows.Forms.Label
		Me.Frame2 = New System.Windows.Forms.GroupBox
		Me.ExpDateEdit = New System.Windows.Forms.TextBox
		Me.RuntimeLabel = New System.Windows.Forms.Label
		Me.MinutesLabel = New System.Windows.Forms.Label
		Me.TrialExecLeftLabel = New System.Windows.Forms.Label
		Me.TrialDaysLeftLabel = New System.Windows.Forms.Label
		Me.Label7 = New System.Windows.Forms.Label
		Me.Label6 = New System.Windows.Forms.Label
		Me.lael2 = New System.Windows.Forms.Label
		Me.Label4 = New System.Windows.Forms.Label
		Me.Label3 = New System.Windows.Forms.Label
		Me.Frame1 = New System.Windows.Forms.GroupBox
		Me.CheckStatusButton = New System.Windows.Forms.Button
		Me.StatusLabel = New System.Windows.Forms.Label
		Me.Label1 = New System.Windows.Forms.Label
		Me.Text = "Trial-Registration SDK"
		Me.ClientSize = New System.Drawing.Size(438, 595)
		Me.Location = New System.Drawing.Point(4, 23)
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
		Me.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
		Me.BackColor = System.Drawing.SystemColors.Control
		Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable
		Me.ControlBox = True
		Me.Enabled = True
		Me.KeyPreview = False
		Me.MaximizeBox = True
		Me.MinimizeBox = True
		Me.Cursor = System.Windows.Forms.Cursors.Default
		Me.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.ShowInTaskbar = True
		Me.HelpButton = False
		Me.WindowState = System.Windows.Forms.FormWindowState.Normal
		Me.Name = "Form1"
		Me.Frame4.Text = "Hardwaer ID"
		Me.Frame4.Size = New System.Drawing.Size(393, 81)
		Me.Frame4.Location = New System.Drawing.Point(24, 488)
		Me.Frame4.TabIndex = 28
		Me.Frame4.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Frame4.BackColor = System.Drawing.SystemColors.Control
		Me.Frame4.Enabled = True
		Me.Frame4.ForeColor = System.Drawing.SystemColors.ControlText
		Me.Frame4.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.Frame4.Visible = True
		Me.Frame4.Name = "Frame4"
		Me.HardIdEdit.AutoSize = False
		Me.HardIdEdit.Size = New System.Drawing.Size(225, 21)
		Me.HardIdEdit.Location = New System.Drawing.Point(144, 32)
		Me.HardIdEdit.TabIndex = 30
		Me.HardIdEdit.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.HardIdEdit.AcceptsReturn = True
		Me.HardIdEdit.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
		Me.HardIdEdit.BackColor = System.Drawing.SystemColors.Window
		Me.HardIdEdit.CausesValidation = True
		Me.HardIdEdit.Enabled = True
		Me.HardIdEdit.ForeColor = System.Drawing.SystemColors.WindowText
		Me.HardIdEdit.HideSelection = True
		Me.HardIdEdit.ReadOnly = False
		Me.HardIdEdit.Maxlength = 0
		Me.HardIdEdit.Cursor = System.Windows.Forms.Cursors.IBeam
		Me.HardIdEdit.MultiLine = False
		Me.HardIdEdit.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.HardIdEdit.ScrollBars = System.Windows.Forms.ScrollBars.None
		Me.HardIdEdit.TabStop = True
		Me.HardIdEdit.Visible = True
		Me.HardIdEdit.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
		Me.HardIdEdit.Name = "HardIdEdit"
		Me.HardIdButton.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
		Me.HardIdButton.Text = "Get Hardware ID"
		Me.HardIdButton.Size = New System.Drawing.Size(97, 25)
		Me.HardIdButton.Location = New System.Drawing.Point(24, 32)
		Me.HardIdButton.TabIndex = 29
		Me.HardIdButton.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.HardIdButton.BackColor = System.Drawing.SystemColors.Control
		Me.HardIdButton.CausesValidation = True
		Me.HardIdButton.Enabled = True
		Me.HardIdButton.ForeColor = System.Drawing.SystemColors.ControlText
		Me.HardIdButton.Cursor = System.Windows.Forms.Cursors.Default
		Me.HardIdButton.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.HardIdButton.TabStop = True
		Me.HardIdButton.Name = "HardIdButton"
		Me.Frame3.Text = "License Information"
		Me.Frame3.Size = New System.Drawing.Size(393, 169)
		Me.Frame3.Location = New System.Drawing.Point(24, 304)
		Me.Frame3.TabIndex = 15
		Me.Frame3.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Frame3.BackColor = System.Drawing.SystemColors.Control
		Me.Frame3.Enabled = True
		Me.Frame3.ForeColor = System.Drawing.SystemColors.ControlText
		Me.Frame3.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.Frame3.Visible = True
		Me.Frame3.Name = "Frame3"
		Me.RegExpDateEdit.AutoSize = False
		Me.RegExpDateEdit.Size = New System.Drawing.Size(97, 19)
		Me.RegExpDateEdit.Location = New System.Drawing.Point(280, 120)
		Me.RegExpDateEdit.TabIndex = 27
		Me.RegExpDateEdit.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.RegExpDateEdit.AcceptsReturn = True
		Me.RegExpDateEdit.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
		Me.RegExpDateEdit.BackColor = System.Drawing.SystemColors.Window
		Me.RegExpDateEdit.CausesValidation = True
		Me.RegExpDateEdit.Enabled = True
		Me.RegExpDateEdit.ForeColor = System.Drawing.SystemColors.WindowText
		Me.RegExpDateEdit.HideSelection = True
		Me.RegExpDateEdit.ReadOnly = False
		Me.RegExpDateEdit.Maxlength = 0
		Me.RegExpDateEdit.Cursor = System.Windows.Forms.Cursors.IBeam
		Me.RegExpDateEdit.MultiLine = False
		Me.RegExpDateEdit.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.RegExpDateEdit.ScrollBars = System.Windows.Forms.ScrollBars.None
		Me.RegExpDateEdit.TabStop = True
		Me.RegExpDateEdit.Visible = True
		Me.RegExpDateEdit.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
		Me.RegExpDateEdit.Name = "RegExpDateEdit"
		Me.CustomEdit.AutoSize = False
		Me.CustomEdit.Size = New System.Drawing.Size(97, 57)
		Me.CustomEdit.Location = New System.Drawing.Point(280, 32)
		Me.CustomEdit.MultiLine = True
		Me.CustomEdit.TabIndex = 21
		Me.CustomEdit.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.CustomEdit.AcceptsReturn = True
		Me.CustomEdit.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
		Me.CustomEdit.BackColor = System.Drawing.SystemColors.Window
		Me.CustomEdit.CausesValidation = True
		Me.CustomEdit.Enabled = True
		Me.CustomEdit.ForeColor = System.Drawing.SystemColors.WindowText
		Me.CustomEdit.HideSelection = True
		Me.CustomEdit.ReadOnly = False
		Me.CustomEdit.Maxlength = 0
		Me.CustomEdit.Cursor = System.Windows.Forms.Cursors.IBeam
		Me.CustomEdit.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.CustomEdit.ScrollBars = System.Windows.Forms.ScrollBars.None
		Me.CustomEdit.TabStop = True
		Me.CustomEdit.Visible = True
		Me.CustomEdit.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
		Me.CustomEdit.Name = "CustomEdit"
		Me.CompanyEdit.AutoSize = False
		Me.CompanyEdit.Size = New System.Drawing.Size(113, 19)
		Me.CompanyEdit.Location = New System.Drawing.Point(72, 64)
		Me.CompanyEdit.TabIndex = 19
		Me.CompanyEdit.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.CompanyEdit.AcceptsReturn = True
		Me.CompanyEdit.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
		Me.CompanyEdit.BackColor = System.Drawing.SystemColors.Window
		Me.CompanyEdit.CausesValidation = True
		Me.CompanyEdit.Enabled = True
		Me.CompanyEdit.ForeColor = System.Drawing.SystemColors.WindowText
		Me.CompanyEdit.HideSelection = True
		Me.CompanyEdit.ReadOnly = False
		Me.CompanyEdit.Maxlength = 0
		Me.CompanyEdit.Cursor = System.Windows.Forms.Cursors.IBeam
		Me.CompanyEdit.MultiLine = False
		Me.CompanyEdit.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.CompanyEdit.ScrollBars = System.Windows.Forms.ScrollBars.None
		Me.CompanyEdit.TabStop = True
		Me.CompanyEdit.Visible = True
		Me.CompanyEdit.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
		Me.CompanyEdit.Name = "CompanyEdit"
		Me.NameEdit.AutoSize = False
		Me.NameEdit.Size = New System.Drawing.Size(113, 19)
		Me.NameEdit.Location = New System.Drawing.Point(72, 32)
		Me.NameEdit.TabIndex = 18
		Me.NameEdit.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.NameEdit.AcceptsReturn = True
		Me.NameEdit.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
		Me.NameEdit.BackColor = System.Drawing.SystemColors.Window
		Me.NameEdit.CausesValidation = True
		Me.NameEdit.Enabled = True
		Me.NameEdit.ForeColor = System.Drawing.SystemColors.WindowText
		Me.NameEdit.HideSelection = True
		Me.NameEdit.ReadOnly = False
		Me.NameEdit.Maxlength = 0
		Me.NameEdit.Cursor = System.Windows.Forms.Cursors.IBeam
		Me.NameEdit.MultiLine = False
		Me.NameEdit.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.NameEdit.ScrollBars = System.Windows.Forms.ScrollBars.None
		Me.NameEdit.TabStop = True
		Me.NameEdit.Visible = True
		Me.NameEdit.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
		Me.NameEdit.Name = "NameEdit"
		Me.Label19.Text = "Expiration Date:"
		Me.Label19.Size = New System.Drawing.Size(81, 25)
		Me.Label19.Location = New System.Drawing.Point(192, 128)
		Me.Label19.TabIndex = 26
		Me.Label19.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label19.TextAlign = System.Drawing.ContentAlignment.TopLeft
		Me.Label19.BackColor = System.Drawing.SystemColors.Control
		Me.Label19.Enabled = True
		Me.Label19.ForeColor = System.Drawing.SystemColors.ControlText
		Me.Label19.Cursor = System.Windows.Forms.Cursors.Default
		Me.Label19.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.Label19.UseMnemonic = True
		Me.Label19.Visible = True
		Me.Label19.AutoSize = False
		Me.Label19.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.Label19.Name = "Label19"
		Me.RegExecLeftLabel.Font = New System.Drawing.Font("Arial", 12!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.RegExecLeftLabel.ForeColor = System.Drawing.SystemColors.Highlight
		Me.RegExecLeftLabel.Size = New System.Drawing.Size(57, 25)
		Me.RegExecLeftLabel.Location = New System.Drawing.Point(96, 128)
		Me.RegExecLeftLabel.TabIndex = 25
		Me.RegExecLeftLabel.TextAlign = System.Drawing.ContentAlignment.TopLeft
		Me.RegExecLeftLabel.BackColor = System.Drawing.SystemColors.Control
		Me.RegExecLeftLabel.Enabled = True
		Me.RegExecLeftLabel.Cursor = System.Windows.Forms.Cursors.Default
		Me.RegExecLeftLabel.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.RegExecLeftLabel.UseMnemonic = True
		Me.RegExecLeftLabel.Visible = True
		Me.RegExecLeftLabel.AutoSize = False
		Me.RegExecLeftLabel.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.RegExecLeftLabel.Name = "RegExecLeftLabel"
		Me.Label17.Text = "Executions Left:"
		Me.Label17.Size = New System.Drawing.Size(81, 25)
		Me.Label17.Location = New System.Drawing.Point(16, 136)
		Me.Label17.TabIndex = 24
		Me.Label17.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label17.TextAlign = System.Drawing.ContentAlignment.TopLeft
		Me.Label17.BackColor = System.Drawing.SystemColors.Control
		Me.Label17.Enabled = True
		Me.Label17.ForeColor = System.Drawing.SystemColors.ControlText
		Me.Label17.Cursor = System.Windows.Forms.Cursors.Default
		Me.Label17.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.Label17.UseMnemonic = True
		Me.Label17.Visible = True
		Me.Label17.AutoSize = False
		Me.Label17.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.Label17.Name = "Label17"
		Me.RegDaysLeftLabel.Font = New System.Drawing.Font("Arial", 12!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.RegDaysLeftLabel.ForeColor = System.Drawing.SystemColors.Highlight
		Me.RegDaysLeftLabel.Size = New System.Drawing.Size(57, 25)
		Me.RegDaysLeftLabel.Location = New System.Drawing.Point(80, 96)
		Me.RegDaysLeftLabel.TabIndex = 23
		Me.RegDaysLeftLabel.TextAlign = System.Drawing.ContentAlignment.TopLeft
		Me.RegDaysLeftLabel.BackColor = System.Drawing.SystemColors.Control
		Me.RegDaysLeftLabel.Enabled = True
		Me.RegDaysLeftLabel.Cursor = System.Windows.Forms.Cursors.Default
		Me.RegDaysLeftLabel.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.RegDaysLeftLabel.UseMnemonic = True
		Me.RegDaysLeftLabel.Visible = True
		Me.RegDaysLeftLabel.AutoSize = False
		Me.RegDaysLeftLabel.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.RegDaysLeftLabel.Name = "RegDaysLeftLabel"
		Me.Label15.Text = "Days Left:"
		Me.Label15.Size = New System.Drawing.Size(57, 17)
		Me.Label15.Location = New System.Drawing.Point(16, 104)
		Me.Label15.TabIndex = 22
		Me.Label15.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label15.TextAlign = System.Drawing.ContentAlignment.TopLeft
		Me.Label15.BackColor = System.Drawing.SystemColors.Control
		Me.Label15.Enabled = True
		Me.Label15.ForeColor = System.Drawing.SystemColors.ControlText
		Me.Label15.Cursor = System.Windows.Forms.Cursors.Default
		Me.Label15.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.Label15.UseMnemonic = True
		Me.Label15.Visible = True
		Me.Label15.AutoSize = False
		Me.Label15.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.Label15.Name = "Label15"
		Me.Label14.Text = "Custom:"
		Me.Label14.Size = New System.Drawing.Size(41, 17)
		Me.Label14.Location = New System.Drawing.Point(232, 32)
		Me.Label14.TabIndex = 20
		Me.Label14.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label14.TextAlign = System.Drawing.ContentAlignment.TopLeft
		Me.Label14.BackColor = System.Drawing.SystemColors.Control
		Me.Label14.Enabled = True
		Me.Label14.ForeColor = System.Drawing.SystemColors.ControlText
		Me.Label14.Cursor = System.Windows.Forms.Cursors.Default
		Me.Label14.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.Label14.UseMnemonic = True
		Me.Label14.Visible = True
		Me.Label14.AutoSize = False
		Me.Label14.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.Label14.Name = "Label14"
		Me.Label13.Text = "Company:"
		Me.Label13.Size = New System.Drawing.Size(57, 17)
		Me.Label13.Location = New System.Drawing.Point(16, 64)
		Me.Label13.TabIndex = 17
		Me.Label13.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label13.TextAlign = System.Drawing.ContentAlignment.TopLeft
		Me.Label13.BackColor = System.Drawing.SystemColors.Control
		Me.Label13.Enabled = True
		Me.Label13.ForeColor = System.Drawing.SystemColors.ControlText
		Me.Label13.Cursor = System.Windows.Forms.Cursors.Default
		Me.Label13.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.Label13.UseMnemonic = True
		Me.Label13.Visible = True
		Me.Label13.AutoSize = False
		Me.Label13.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.Label13.Name = "Label13"
		Me.Label12.Text = "Name:"
		Me.Label12.Size = New System.Drawing.Size(41, 17)
		Me.Label12.Location = New System.Drawing.Point(32, 40)
		Me.Label12.TabIndex = 16
		Me.Label12.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label12.TextAlign = System.Drawing.ContentAlignment.TopLeft
		Me.Label12.BackColor = System.Drawing.SystemColors.Control
		Me.Label12.Enabled = True
		Me.Label12.ForeColor = System.Drawing.SystemColors.ControlText
		Me.Label12.Cursor = System.Windows.Forms.Cursors.Default
		Me.Label12.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.Label12.UseMnemonic = True
		Me.Label12.Visible = True
		Me.Label12.AutoSize = False
		Me.Label12.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.Label12.Name = "Label12"
		Me.Frame2.Text = "Trial Information"
		Me.Frame2.Size = New System.Drawing.Size(393, 129)
		Me.Frame2.Location = New System.Drawing.Point(24, 160)
		Me.Frame2.TabIndex = 4
		Me.Frame2.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Frame2.BackColor = System.Drawing.SystemColors.Control
		Me.Frame2.Enabled = True
		Me.Frame2.ForeColor = System.Drawing.SystemColors.ControlText
		Me.Frame2.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.Frame2.Visible = True
		Me.Frame2.Name = "Frame2"
		Me.ExpDateEdit.AutoSize = False
		Me.ExpDateEdit.Size = New System.Drawing.Size(121, 19)
		Me.ExpDateEdit.Location = New System.Drawing.Point(96, 80)
		Me.ExpDateEdit.TabIndex = 14
		Me.ExpDateEdit.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.ExpDateEdit.AcceptsReturn = True
		Me.ExpDateEdit.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
		Me.ExpDateEdit.BackColor = System.Drawing.SystemColors.Window
		Me.ExpDateEdit.CausesValidation = True
		Me.ExpDateEdit.Enabled = True
		Me.ExpDateEdit.ForeColor = System.Drawing.SystemColors.WindowText
		Me.ExpDateEdit.HideSelection = True
		Me.ExpDateEdit.ReadOnly = False
		Me.ExpDateEdit.Maxlength = 0
		Me.ExpDateEdit.Cursor = System.Windows.Forms.Cursors.IBeam
		Me.ExpDateEdit.MultiLine = False
		Me.ExpDateEdit.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.ExpDateEdit.ScrollBars = System.Windows.Forms.ScrollBars.None
		Me.ExpDateEdit.TabStop = True
		Me.ExpDateEdit.Visible = True
		Me.ExpDateEdit.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
		Me.ExpDateEdit.Name = "ExpDateEdit"
		Me.RuntimeLabel.Font = New System.Drawing.Font("Arial", 12!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.RuntimeLabel.ForeColor = System.Drawing.SystemColors.Highlight
		Me.RuntimeLabel.Size = New System.Drawing.Size(57, 25)
		Me.RuntimeLabel.Location = New System.Drawing.Point(248, 48)
		Me.RuntimeLabel.TabIndex = 13
		Me.RuntimeLabel.TextAlign = System.Drawing.ContentAlignment.TopLeft
		Me.RuntimeLabel.BackColor = System.Drawing.SystemColors.Control
		Me.RuntimeLabel.Enabled = True
		Me.RuntimeLabel.Cursor = System.Windows.Forms.Cursors.Default
		Me.RuntimeLabel.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.RuntimeLabel.UseMnemonic = True
		Me.RuntimeLabel.Visible = True
		Me.RuntimeLabel.AutoSize = False
		Me.RuntimeLabel.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.RuntimeLabel.Name = "RuntimeLabel"
		Me.MinutesLabel.Font = New System.Drawing.Font("Arial", 12!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.MinutesLabel.ForeColor = System.Drawing.SystemColors.Highlight
		Me.MinutesLabel.Size = New System.Drawing.Size(57, 25)
		Me.MinutesLabel.Location = New System.Drawing.Point(256, 16)
		Me.MinutesLabel.TabIndex = 12
		Me.MinutesLabel.TextAlign = System.Drawing.ContentAlignment.TopLeft
		Me.MinutesLabel.BackColor = System.Drawing.SystemColors.Control
		Me.MinutesLabel.Enabled = True
		Me.MinutesLabel.Cursor = System.Windows.Forms.Cursors.Default
		Me.MinutesLabel.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.MinutesLabel.UseMnemonic = True
		Me.MinutesLabel.Visible = True
		Me.MinutesLabel.AutoSize = False
		Me.MinutesLabel.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.MinutesLabel.Name = "MinutesLabel"
		Me.TrialExecLeftLabel.Font = New System.Drawing.Font("Arial", 12!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.TrialExecLeftLabel.ForeColor = System.Drawing.SystemColors.Highlight
		Me.TrialExecLeftLabel.Size = New System.Drawing.Size(57, 25)
		Me.TrialExecLeftLabel.Location = New System.Drawing.Point(104, 48)
		Me.TrialExecLeftLabel.TabIndex = 11
		Me.TrialExecLeftLabel.TextAlign = System.Drawing.ContentAlignment.TopLeft
		Me.TrialExecLeftLabel.BackColor = System.Drawing.SystemColors.Control
		Me.TrialExecLeftLabel.Enabled = True
		Me.TrialExecLeftLabel.Cursor = System.Windows.Forms.Cursors.Default
		Me.TrialExecLeftLabel.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.TrialExecLeftLabel.UseMnemonic = True
		Me.TrialExecLeftLabel.Visible = True
		Me.TrialExecLeftLabel.AutoSize = False
		Me.TrialExecLeftLabel.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.TrialExecLeftLabel.Name = "TrialExecLeftLabel"
		Me.TrialDaysLeftLabel.Font = New System.Drawing.Font("Arial", 12!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.TrialDaysLeftLabel.ForeColor = System.Drawing.SystemColors.Highlight
		Me.TrialDaysLeftLabel.Size = New System.Drawing.Size(57, 25)
		Me.TrialDaysLeftLabel.Location = New System.Drawing.Point(88, 16)
		Me.TrialDaysLeftLabel.TabIndex = 10
		Me.TrialDaysLeftLabel.TextAlign = System.Drawing.ContentAlignment.TopLeft
		Me.TrialDaysLeftLabel.BackColor = System.Drawing.SystemColors.Control
		Me.TrialDaysLeftLabel.Enabled = True
		Me.TrialDaysLeftLabel.Cursor = System.Windows.Forms.Cursors.Default
		Me.TrialDaysLeftLabel.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.TrialDaysLeftLabel.UseMnemonic = True
		Me.TrialDaysLeftLabel.Visible = True
		Me.TrialDaysLeftLabel.AutoSize = False
		Me.TrialDaysLeftLabel.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.TrialDaysLeftLabel.Name = "TrialDaysLeftLabel"
		Me.Label7.Text = "Expiration Date:"
		Me.Label7.Size = New System.Drawing.Size(81, 17)
		Me.Label7.Location = New System.Drawing.Point(16, 88)
		Me.Label7.TabIndex = 9
		Me.Label7.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label7.TextAlign = System.Drawing.ContentAlignment.TopLeft
		Me.Label7.BackColor = System.Drawing.SystemColors.Control
		Me.Label7.Enabled = True
		Me.Label7.ForeColor = System.Drawing.SystemColors.ControlText
		Me.Label7.Cursor = System.Windows.Forms.Cursors.Default
		Me.Label7.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.Label7.UseMnemonic = True
		Me.Label7.Visible = True
		Me.Label7.AutoSize = False
		Me.Label7.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.Label7.Name = "Label7"
		Me.Label6.Text = "Runtime Left:"
		Me.Label6.Size = New System.Drawing.Size(73, 17)
		Me.Label6.Location = New System.Drawing.Point(176, 56)
		Me.Label6.TabIndex = 8
		Me.Label6.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label6.TextAlign = System.Drawing.ContentAlignment.TopLeft
		Me.Label6.BackColor = System.Drawing.SystemColors.Control
		Me.Label6.Enabled = True
		Me.Label6.ForeColor = System.Drawing.SystemColors.ControlText
		Me.Label6.Cursor = System.Windows.Forms.Cursors.Default
		Me.Label6.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.Label6.UseMnemonic = True
		Me.Label6.Visible = True
		Me.Label6.AutoSize = False
		Me.Label6.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.Label6.Name = "Label6"
		Me.lael2.Text = "Minutes Left:"
		Me.lael2.Size = New System.Drawing.Size(73, 17)
		Me.lael2.Location = New System.Drawing.Point(176, 24)
		Me.lael2.TabIndex = 7
		Me.lael2.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.lael2.TextAlign = System.Drawing.ContentAlignment.TopLeft
		Me.lael2.BackColor = System.Drawing.SystemColors.Control
		Me.lael2.Enabled = True
		Me.lael2.ForeColor = System.Drawing.SystemColors.ControlText
		Me.lael2.Cursor = System.Windows.Forms.Cursors.Default
		Me.lael2.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.lael2.UseMnemonic = True
		Me.lael2.Visible = True
		Me.lael2.AutoSize = False
		Me.lael2.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.lael2.Name = "lael2"
		Me.Label4.Text = "Executions Left:"
		Me.Label4.Size = New System.Drawing.Size(89, 25)
		Me.Label4.Location = New System.Drawing.Point(24, 56)
		Me.Label4.TabIndex = 6
		Me.Label4.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label4.TextAlign = System.Drawing.ContentAlignment.TopLeft
		Me.Label4.BackColor = System.Drawing.SystemColors.Control
		Me.Label4.Enabled = True
		Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
		Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
		Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.Label4.UseMnemonic = True
		Me.Label4.Visible = True
		Me.Label4.AutoSize = False
		Me.Label4.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.Label4.Name = "Label4"
		Me.Label3.Text = "Days Left:"
		Me.Label3.Size = New System.Drawing.Size(65, 17)
		Me.Label3.Location = New System.Drawing.Point(24, 24)
		Me.Label3.TabIndex = 5
		Me.Label3.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label3.TextAlign = System.Drawing.ContentAlignment.TopLeft
		Me.Label3.BackColor = System.Drawing.SystemColors.Control
		Me.Label3.Enabled = True
		Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
		Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
		Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.Label3.UseMnemonic = True
		Me.Label3.Visible = True
		Me.Label3.AutoSize = False
		Me.Label3.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.Label3.Name = "Label3"
		Me.Frame1.Text = "Application Status"
		Me.Frame1.Size = New System.Drawing.Size(393, 113)
		Me.Frame1.Location = New System.Drawing.Point(24, 24)
		Me.Frame1.TabIndex = 0
		Me.Frame1.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Frame1.BackColor = System.Drawing.SystemColors.Control
		Me.Frame1.Enabled = True
		Me.Frame1.ForeColor = System.Drawing.SystemColors.ControlText
		Me.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.Frame1.Visible = True
		Me.Frame1.Name = "Frame1"
		Me.CheckStatusButton.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
		Me.CheckStatusButton.Text = "Check Application Status"
		Me.CheckStatusButton.Size = New System.Drawing.Size(161, 25)
		Me.CheckStatusButton.Location = New System.Drawing.Point(104, 24)
		Me.CheckStatusButton.TabIndex = 1
		Me.CheckStatusButton.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.CheckStatusButton.BackColor = System.Drawing.SystemColors.Control
		Me.CheckStatusButton.CausesValidation = True
		Me.CheckStatusButton.Enabled = True
		Me.CheckStatusButton.ForeColor = System.Drawing.SystemColors.ControlText
		Me.CheckStatusButton.Cursor = System.Windows.Forms.Cursors.Default
		Me.CheckStatusButton.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.CheckStatusButton.TabStop = True
		Me.CheckStatusButton.Name = "CheckStatusButton"
		Me.StatusLabel.Font = New System.Drawing.Font("Arial", 12!, System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.StatusLabel.ForeColor = System.Drawing.SystemColors.Highlight
		Me.StatusLabel.Size = New System.Drawing.Size(321, 41)
		Me.StatusLabel.Location = New System.Drawing.Point(56, 64)
		Me.StatusLabel.TabIndex = 3
		Me.StatusLabel.TextAlign = System.Drawing.ContentAlignment.TopLeft
		Me.StatusLabel.BackColor = System.Drawing.SystemColors.Control
		Me.StatusLabel.Enabled = True
		Me.StatusLabel.Cursor = System.Windows.Forms.Cursors.Default
		Me.StatusLabel.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.StatusLabel.UseMnemonic = True
		Me.StatusLabel.Visible = True
		Me.StatusLabel.AutoSize = False
		Me.StatusLabel.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.StatusLabel.Name = "StatusLabel"
		Me.Label1.Text = "Status:"
		Me.Label1.Size = New System.Drawing.Size(41, 25)
		Me.Label1.Location = New System.Drawing.Point(16, 72)
		Me.Label1.TabIndex = 2
		Me.Label1.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopLeft
		Me.Label1.BackColor = System.Drawing.SystemColors.Control
		Me.Label1.Enabled = True
		Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
		Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
		Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.Label1.UseMnemonic = True
		Me.Label1.Visible = True
		Me.Label1.AutoSize = False
		Me.Label1.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.Label1.Name = "Label1"
		Me.Controls.Add(Frame4)
		Me.Controls.Add(Frame3)
		Me.Controls.Add(Frame2)
		Me.Controls.Add(Frame1)
		Me.Frame4.Controls.Add(HardIdEdit)
		Me.Frame4.Controls.Add(HardIdButton)
		Me.Frame3.Controls.Add(RegExpDateEdit)
		Me.Frame3.Controls.Add(CustomEdit)
		Me.Frame3.Controls.Add(CompanyEdit)
		Me.Frame3.Controls.Add(NameEdit)
		Me.Frame3.Controls.Add(Label19)
		Me.Frame3.Controls.Add(RegExecLeftLabel)
		Me.Frame3.Controls.Add(Label17)
		Me.Frame3.Controls.Add(RegDaysLeftLabel)
		Me.Frame3.Controls.Add(Label15)
		Me.Frame3.Controls.Add(Label14)
		Me.Frame3.Controls.Add(Label13)
		Me.Frame3.Controls.Add(Label12)
		Me.Frame2.Controls.Add(ExpDateEdit)
		Me.Frame2.Controls.Add(RuntimeLabel)
		Me.Frame2.Controls.Add(MinutesLabel)
		Me.Frame2.Controls.Add(TrialExecLeftLabel)
		Me.Frame2.Controls.Add(TrialDaysLeftLabel)
		Me.Frame2.Controls.Add(Label7)
		Me.Frame2.Controls.Add(Label6)
		Me.Frame2.Controls.Add(lael2)
		Me.Frame2.Controls.Add(Label4)
		Me.Frame2.Controls.Add(Label3)
		Me.Frame1.Controls.Add(CheckStatusButton)
		Me.Frame1.Controls.Add(StatusLabel)
		Me.Frame1.Controls.Add(Label1)
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
		Set
			m_vb6FormDefInstance = Value
		End Set
	End Property
#End Region


    Private Sub CheckStatusButton_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles CheckStatusButton.Click
		
		Dim AppStatus As Short
        Dim BufferVarValue As New System.Text.StringBuilder(100)
        Dim Status As Short
        Dim LicenseInfo(3) As String
        Dim UserName As New StringBuilder(100)
        Dim CompanyName As New StringBuilder(100)
        Dim CustomData As New StringBuilder(100)
        Dim ExpirationDate As New SystemTime()

        Status = WinlicenseSDK.WLRegGetStatus(AppStatus)

        ' show application status
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
            TrialDaysLeftLabel.Text = WinlicenseSDK.WLTrialDaysLeft().ToString()
            TrialExecLeftLabel.Text = WinlicenseSDK.WLTrialExecutionsLeft().ToString()
            MinutesLabel.Text = WinlicenseSDK.WLTrialGlobalTimeLeft().ToString()
            RuntimeLabel.Text = WinlicenseSDK.WLTrialRuntimeLeft().ToString()

            WinlicenseSDK.WLTrialExpirationDate(ExpirationDate)
            ExpDateEdit.Text = ExpirationDate.wYear & "-" & ExpirationDate.wMonth &
                               "-" & ExpirationDate.wDay

            ' set as empty the registration data

            NameEdit.Text = ""
            CompanyEdit.Text = ""
            CustomEdit.Text = ""
            RegDaysLeftLabel.Text = ""
            RegExecLeftLabel.Text = ""

        Else

            ' application is registered

            WinlicenseSDK.WLRegGetLicenseInfo(UserName, CompanyName, CustomData)
            NameEdit.Text = UserName.ToString()
            CompanyEdit.Text = CompanyName.ToString()
            CustomEdit.Text = CustomData.ToString()

            RegDaysLeftLabel.Text = WinlicenseSDK.WLRegDaysLeft().ToString()
            RegExecLeftLabel.Text = WinlicenseSDK.WLRegExecutionsLeft().ToString()

            WinlicenseSDK.WLRegExpirationDate(ExpirationDate)
            ExpDateEdit.Text = ExpirationDate.wYear & "-" & ExpirationDate.wMonth &
                               "-" & ExpirationDate.wDay

            ' set as empty the trial data

            TrialDaysLeftLabel.Text = ""
            TrialExecLeftLabel.Text = ""
            MinutesLabel.Text = ""
            RuntimeLabel.Text = ""

        End If

    End Sub
	
	Private Sub HardIdButton_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles HardIdButton.Click

        Dim HardwareId As New StringBuilder(100)

        WinlicenseSDK.WLHardwareGetIDW(HardwareId)
        HardIdEdit.Text = HardwareId.ToString()

    End Sub
End Class