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
	Public WithEvents Command1 As System.Windows.Forms.Button
	Public WithEvents PasswordEdit As System.Windows.Forms.TextBox
	Public WithEvents NameEdit As System.Windows.Forms.TextBox
	Public WithEvents Label2 As System.Windows.Forms.Label
	Public WithEvents Label1 As System.Windows.Forms.Label
	'NOTE: The following procedure is required by the Windows Form Designer
	'It can be modified using the Windows Form Designer.
	'Do not modify it using the code editor.
	<System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
		Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(Form1))
		Me.components = New System.ComponentModel.Container()
		Me.ToolTip1 = New System.Windows.Forms.ToolTip(components)
		Me.ToolTip1.Active = True
		Me.Command1 = New System.Windows.Forms.Button
		Me.PasswordEdit = New System.Windows.Forms.TextBox
		Me.NameEdit = New System.Windows.Forms.TextBox
		Me.Label2 = New System.Windows.Forms.Label
		Me.Label1 = New System.Windows.Forms.Label
		Me.Text = "Password Generator SDK"
		Me.ClientSize = New System.Drawing.Size(362, 189)
		Me.Location = New System.Drawing.Point(424, 299)
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
		Me.Command1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
		Me.Command1.Text = "Generate Password"
		Me.Command1.Size = New System.Drawing.Size(209, 33)
		Me.Command1.Location = New System.Drawing.Point(88, 120)
		Me.Command1.TabIndex = 4
		Me.Command1.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Command1.BackColor = System.Drawing.SystemColors.Control
		Me.Command1.CausesValidation = True
		Me.Command1.Enabled = True
		Me.Command1.ForeColor = System.Drawing.SystemColors.ControlText
		Me.Command1.Cursor = System.Windows.Forms.Cursors.Default
		Me.Command1.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.Command1.TabStop = True
		Me.Command1.Name = "Command1"
		Me.PasswordEdit.AutoSize = False
		Me.PasswordEdit.Size = New System.Drawing.Size(249, 19)
		Me.PasswordEdit.Location = New System.Drawing.Point(80, 72)
		Me.PasswordEdit.TabIndex = 3
		Me.PasswordEdit.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.PasswordEdit.AcceptsReturn = True
		Me.PasswordEdit.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
		Me.PasswordEdit.BackColor = System.Drawing.SystemColors.Window
		Me.PasswordEdit.CausesValidation = True
		Me.PasswordEdit.Enabled = True
		Me.PasswordEdit.ForeColor = System.Drawing.SystemColors.WindowText
		Me.PasswordEdit.HideSelection = True
		Me.PasswordEdit.ReadOnly = False
		Me.PasswordEdit.Maxlength = 0
		Me.PasswordEdit.Cursor = System.Windows.Forms.Cursors.IBeam
		Me.PasswordEdit.MultiLine = False
		Me.PasswordEdit.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.PasswordEdit.ScrollBars = System.Windows.Forms.ScrollBars.None
		Me.PasswordEdit.TabStop = True
		Me.PasswordEdit.Visible = True
		Me.PasswordEdit.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
		Me.PasswordEdit.Name = "PasswordEdit"
		Me.NameEdit.AutoSize = False
		Me.NameEdit.Size = New System.Drawing.Size(249, 19)
		Me.NameEdit.Location = New System.Drawing.Point(80, 32)
		Me.NameEdit.TabIndex = 2
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
		Me.Label2.Text = "Password:"
		Me.Label2.Size = New System.Drawing.Size(57, 25)
		Me.Label2.Location = New System.Drawing.Point(24, 80)
		Me.Label2.TabIndex = 1
		Me.Label2.Font = New System.Drawing.Font("Arial", 8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopLeft
		Me.Label2.BackColor = System.Drawing.SystemColors.Control
		Me.Label2.Enabled = True
		Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
		Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
		Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
		Me.Label2.UseMnemonic = True
		Me.Label2.Visible = True
		Me.Label2.AutoSize = False
		Me.Label2.BorderStyle = System.Windows.Forms.BorderStyle.None
		Me.Label2.Name = "Label2"
		Me.Label1.Text = "Name:"
		Me.Label1.Size = New System.Drawing.Size(41, 17)
		Me.Label1.Location = New System.Drawing.Point(40, 40)
		Me.Label1.TabIndex = 0
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
		Me.Controls.Add(Command1)
		Me.Controls.Add(PasswordEdit)
		Me.Controls.Add(NameEdit)
		Me.Controls.Add(Label2)
		Me.Controls.Add(Label1)
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
	Const PasswordHash As String = "J76sG573dB2394JHYTEOsdkfdf8973bhus2kcx392id82ksdfui2QTEJdg274018435g"
	Private Sub Command1_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles Command1.Click
		
		' Generate password for the specified user
		
        WLGenPassword(PasswordHash, NameEdit.Text, GlobalBuffer.Value)
		
		' show password on GUI
		
		PasswordEdit.Text = GlobalBuffer.Value
		
	End Sub
End Class