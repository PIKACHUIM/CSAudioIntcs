
Public Class Form1

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Dim AppStatus As Short
        Dim Status As Short
        ' Check if the application is registered

        Status = WinlicenseSDK.WLRegGetStatus(AppStatus)

        ' if application registered, we display a messagebox and exit

        If Status = 1 Then
            MsgBox("The application is already REGISTERED", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "SmartKeys")
            Me.Close()
        End If

    End Sub

    Private Sub Label3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label3.Click

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click

        Dim Status As Short

        Status = WinlicenseSDK.WLRegSmartKeyCheck(txtName.Text.ToString, txtCompanyName.Text.ToString,
                                                  txtCustomData.Text.ToString, txtActivationKey.Text.ToString)
        If Not Status Then

            MsgBox("Activation code is INVALID", MsgBoxStyle.Exclamation + MsgBoxStyle.OkOnly, "ERROR")

        Else

            ' activation data is correct. Now, we are going to install the smart key as File key 
            ' (via WLRegSmartKeyInstallToFile)
            WinlicenseSDK.WLRegSmartKeyInstallToFile(txtName.Text.ToString, txtCompanyName.Text.ToString,
                                                  txtCustomData.Text.ToString, txtActivationKey.Text.ToString)
            MsgBox("Activation code is correct. Product registered." & Chr(13) & Chr(10) &
                   "You must RESTART the application to finish the registration",
                   MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "SmartKeys")

        End If

    End Sub
End Class


