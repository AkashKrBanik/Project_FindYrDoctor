Imports Guna.UI2.WinForms
Public Class Login_Form
    Private Sub Login_btn_Click(sender As Object, e As EventArgs) Handles Login_btn.Click
        If Username_Txtbox.Text = "Akash" And Password_Txtbox.Text = "akash12" Or Username_Txtbox.Text = "Achint" And Password_Txtbox.Text = "achint12" Then
            MessageBox.Show("Login Successfull!!")
            Me.Hide()
            Main_Form.Show()
        Else
            MessageBox.Show("Invalid Credentials!!")
        End If
    End Sub

    Private Sub Clear_btn_Click(sender As Object, e As EventArgs) Handles Clear_btn.Click
        Username_Txtbox.Clear()
        Password_Txtbox.Clear()
    End Sub

    Private Sub Login_Form_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        ' Check if the form is being closed by the user
        If e.CloseReason = CloseReason.UserClosing Then
            ' Ask the user for confirmation or provide any necessary checks
            Dim result As DialogResult = MessageBox.Show("Are you sure you want to exit the application?", "Confirm Exit", MessageBoxButtons.YesNo)

            If result = DialogResult.Yes Then
                ' Close the entire application
                Application.Exit()
            Else
                ' Cancel the form closing
                e.Cancel = True
            End If
        End If
    End Sub

End Class
