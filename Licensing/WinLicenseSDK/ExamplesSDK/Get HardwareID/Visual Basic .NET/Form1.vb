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
    Public WithEvents UsbIdsMemo As System.Windows.Forms.TextBox
    Public WithEvents Button1 As System.Windows.Forms.Button
    Public WithEvents Frame3 As System.Windows.Forms.GroupBox
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.Frame4 = New System.Windows.Forms.GroupBox()
        Me.HardIdEdit = New System.Windows.Forms.TextBox()
        Me.HardIdButton = New System.Windows.Forms.Button()
        Me.Frame3 = New System.Windows.Forms.GroupBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.UsbIdsMemo = New System.Windows.Forms.TextBox()
        Me.Frame4.SuspendLayout()
        Me.Frame3.SuspendLayout()
        Me.SuspendLayout()
        '
        'Frame4
        '
        Me.Frame4.BackColor = System.Drawing.SystemColors.Control
        Me.Frame4.Controls.Add(Me.HardIdEdit)
        Me.Frame4.Controls.Add(Me.HardIdButton)
        Me.Frame4.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame4.Location = New System.Drawing.Point(12, 232)
        Me.Frame4.Name = "Frame4"
        Me.Frame4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame4.Size = New System.Drawing.Size(474, 87)
        Me.Frame4.TabIndex = 28
        Me.Frame4.TabStop = False
        Me.Frame4.Text = "PC Hardware ID"
        '
        'HardIdEdit
        '
        Me.HardIdEdit.AcceptsReturn = True
        Me.HardIdEdit.BackColor = System.Drawing.SystemColors.Window
        Me.HardIdEdit.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.HardIdEdit.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.HardIdEdit.ForeColor = System.Drawing.SystemColors.WindowText
        Me.HardIdEdit.Location = New System.Drawing.Point(151, 34)
        Me.HardIdEdit.MaxLength = 0
        Me.HardIdEdit.Name = "HardIdEdit"
        Me.HardIdEdit.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.HardIdEdit.Size = New System.Drawing.Size(304, 21)
        Me.HardIdEdit.TabIndex = 30
        '
        'HardIdButton
        '
        Me.HardIdButton.BackColor = System.Drawing.SystemColors.Control
        Me.HardIdButton.Cursor = System.Windows.Forms.Cursors.Default
        Me.HardIdButton.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.HardIdButton.ForeColor = System.Drawing.SystemColors.ControlText
        Me.HardIdButton.Location = New System.Drawing.Point(29, 34)
        Me.HardIdButton.Name = "HardIdButton"
        Me.HardIdButton.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.HardIdButton.Size = New System.Drawing.Size(116, 27)
        Me.HardIdButton.TabIndex = 29
        Me.HardIdButton.Text = "Get Hardware ID"
        Me.HardIdButton.UseVisualStyleBackColor = False
        '
        'Frame3
        '
        Me.Frame3.BackColor = System.Drawing.SystemColors.Control
        Me.Frame3.Controls.Add(Me.Button1)
        Me.Frame3.Controls.Add(Me.UsbIdsMemo)
        Me.Frame3.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Frame3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame3.Location = New System.Drawing.Point(12, 24)
        Me.Frame3.Name = "Frame3"
        Me.Frame3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame3.Size = New System.Drawing.Size(474, 182)
        Me.Frame3.TabIndex = 15
        Me.Frame3.TabStop = False
        Me.Frame3.Text = "USB Hardware ID"
        '
        'Button1
        '
        Me.Button1.BackColor = System.Drawing.SystemColors.Control
        Me.Button1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Button1.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Button1.Location = New System.Drawing.Point(22, 26)
        Me.Button1.Name = "Button1"
        Me.Button1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Button1.Size = New System.Drawing.Size(152, 27)
        Me.Button1.TabIndex = 30
        Me.Button1.Text = "Get USB Hardware IDs"
        Me.Button1.UseVisualStyleBackColor = False
        '
        'UsbIdsMemo
        '
        Me.UsbIdsMemo.AcceptsReturn = True
        Me.UsbIdsMemo.BackColor = System.Drawing.SystemColors.Window
        Me.UsbIdsMemo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.UsbIdsMemo.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UsbIdsMemo.ForeColor = System.Drawing.SystemColors.WindowText
        Me.UsbIdsMemo.Location = New System.Drawing.Point(22, 59)
        Me.UsbIdsMemo.MaxLength = 0
        Me.UsbIdsMemo.Multiline = True
        Me.UsbIdsMemo.Name = "UsbIdsMemo"
        Me.UsbIdsMemo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.UsbIdsMemo.Size = New System.Drawing.Size(433, 99)
        Me.UsbIdsMemo.TabIndex = 21
        '
        'Form1
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 14)
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(498, 341)
        Me.Controls.Add(Me.Frame4)
        Me.Controls.Add(Me.Frame3)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Location = New System.Drawing.Point(4, 23)
        Me.Name = "Form1"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Get Hardware ID SDK"
        Me.Frame4.ResumeLayout(False)
        Me.Frame4.PerformLayout()
        Me.Frame3.ResumeLayout(False)
        Me.Frame3.PerformLayout()
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
            m_vb6FormDefInstance = Value
        End Set
    End Property
#End Region

	
	Private Sub HardIdButton_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles HardIdButton.Click

        Dim HardwareId As New StringBuilder(100)

        WinlicenseSDK.WLHardwareGetIDW(HardwareId)
        HardIdEdit.Text = HardwareId.ToString()

    End Sub

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click

        Dim NumberUsbIds As Integer = WinlicenseSDK.WLHardwareGetNumberUsbDrives()
        Dim CurrentUsbId As New StringBuilder(100)
        Dim CurrentUsbName As New StringBuilder(100)

        UsbIdsMemo.Text = ""

        For index As Integer = 0 To NumberUsbIds - 1
            WinlicenseSDK.WLHardwareGetUsbNameAt(index, CurrentUsbName)
            WinlicenseSDK.WLHardwareGetUsbIdAt(index, CurrentUsbId)
            UsbIdsMemo.Text = UsbIdsMemo.Text + CurrentUsbName.ToString() +
                              "=" + CurrentUsbId.ToString() + vbCrLf
        Next
    End Sub
End Class