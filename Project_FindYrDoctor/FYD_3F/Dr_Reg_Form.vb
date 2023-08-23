Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports Guna.UI2.WinForms
Imports MySql.Data.MySqlClient
Imports System.Data.SqlClient


Public Class Dr_Reg_Form

    Private Sub Home_btn_Click(sender As Object, e As EventArgs) Handles Home_btn.Click
        Me.Hide()
        Main_Form.Show()
        Main_Form.Select()
    End Sub

    Private Sub Dr_Register_Menu_btn_Click(sender As Object, e As EventArgs) Handles Dr_Register_Menu_btn.Click
        Dr_RegPanel.Visible = True
        Dr_UpdatePanel.Visible = False
    End Sub

    Private Sub Dr_Update_Menu_btn_Click(sender As Object, e As EventArgs) Handles Dr_Update_Menu_btn.Click
        Dr_RegPanel.Visible = False
        Dr_UpdatePanel.Visible = True
    End Sub

    Private Sub Dr_RecordsView_btn_Click(sender As Object, e As EventArgs) Handles Dr_RecordsView_btn.Click
        Dr_Records_Form.Show()
        Dr_Records_Form.Select()
    End Sub

    Private Sub DrReg_Clear_btn_Click(sender As Object, e As EventArgs) Handles DrReg_Clear_btn.Click
        Dr_FName_txtbox.Clear()
        Dr_Id_txtbox.Clear()
        Dr_Category.SelectedIndex = -1 ' Clears the selected item
        Dr_Ph_No_txtbox.Clear()
        Dr_Qualification_txtbox.Clear()
        Dr_Exp_txtbox.Clear()
        Dr_WrkHrs_Time.SelectedIndex = -1 ' Clears the selected item
        RandomNumber()
    End Sub

    Private Sub DrUpd_Clear_btn_Click(sender As Object, e As EventArgs) Handles DrUpd_Clear_btn.Click
        Dr_FName_Upd_txtbox.Clear()
        Dr_Id_Upd_txtbox.Clear()
        Dr_Upd_Category.SelectedIndex = -1
        Dr_Ph_No_Upd_txtbox.Clear()
        Dr_Qualification_Upd_txtbox.Clear()
        Dr_Exp_Upd_txtbox.Clear()
        Dr_WrkHrs_Upd_Time.SelectedIndex = -1
    End Sub


    '-------------------------------DATABASE------------------------------------------------------------------------------------------------------------

    Dim conn As New MySqlConnection("Data Source=localhost;database=database_fyd3;userid=root;password=''")
    Dim cmd As MySqlCommand

    Public Sub RandomNumber()
        Dim rnd As New Random()
        Dim patientId As Integer = rnd.Next(1000, 9999)
        Dr_Id_txtbox.Text = "DR" & patientId.ToString()
    End Sub

    Private Sub Dr_Reg_Form_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        RandomNumber()
        ' Retrieve the current system date
        Dim currentDate As Date = Date.Now
        Dr_Reg_date.Text = currentDate.ToString("dd-MMM-yy")
        Dr_Records_Form.Show()
        LoadData()
    End Sub

    Public Sub LoadData()
        ' Select data from table and load into DataGridView
        Dim query As String = "SELECT * FROM doctor_table"
        Dim da As MySqlDataAdapter = New MySqlDataAdapter(query, conn)
        Dim dt As DataTable = New DataTable()
        da.Fill(dt)
        Dr_Records_Form.Dr_DataGridView1.DataSource = dt
    End Sub

    Private Sub InsertData()
        ' Insert data into table
        Dim query As String = "INSERT INTO doctor_table(Dr_Name,Doctor_Id,Dr_Category,Dr_Mobile,Qualifications,Dr_Exp,Dr_Reg_date,Dr_WorkingHrs) VALUES (@Dr_Name,@Doctor_Id,@Dr_Category,@Dr_Mobile,@Qualifications,@Dr_Exp,@Dr_Reg_date,@Dr_WorkingHrs)"
        cmd = New MySqlCommand(query, conn)
        cmd.Parameters.AddWithValue("Dr_Name", Dr_FName_txtbox.Text)
        cmd.Parameters.AddWithValue("Doctor_Id", Dr_Id_txtbox.Text)
        cmd.Parameters.AddWithValue("Dr_Category", Dr_Category.Text)
        cmd.Parameters.AddWithValue("Dr_Mobile", Dr_Ph_No_txtbox.Text)
        cmd.Parameters.AddWithValue("Qualifications", Dr_Qualification_txtbox.Text)
        cmd.Parameters.AddWithValue("Dr_Exp", Dr_Exp_txtbox.Text)
        cmd.Parameters.AddWithValue("Dr_Reg_date", Dr_Reg_date.Text)
        cmd.Parameters.AddWithValue("Dr_WorkingHrs", Dr_WrkHrs_Time.Text)
        conn.Open()
        If cmd.ExecuteNonQuery() = 1 Then
            MessageBox.Show("Doctor Registered Successfully!!")
        Else
            MessageBox.Show("Doctor Registration Unsuccessful!!")
        End If
        conn.Close()
        LoadData()
    End Sub

    Private Sub UpdateData()
        ' Update data in table
        Dim query As String = "UPDATE doctor_table SET Dr_Name=@Dr_Name, Dr_Category=@Dr_Category,Dr_Mobile=@Dr_Mobile,Qualifications=@Qualifications,Dr_Exp=@Dr_Exp,Dr_WorkingHrs=@Dr_WorkingHrs WHERE Doctor_Id=@Doctor_Id"
        cmd = New MySqlCommand(query, conn)
        cmd.Parameters.AddWithValue("Dr_Name", Dr_FName_Upd_txtbox.Text)
        cmd.Parameters.AddWithValue("Doctor_Id", Dr_Id_Upd_txtbox.Text)
        cmd.Parameters.AddWithValue("Dr_Category", Dr_Upd_Category.Text)
        cmd.Parameters.AddWithValue("Dr_Mobile", Dr_Ph_No_Upd_txtbox.Text)
        cmd.Parameters.AddWithValue("Qualifications", Dr_Qualification_Upd_txtbox.Text)
        cmd.Parameters.AddWithValue("Dr_Exp", Dr_Exp_Upd_txtbox.Text)
        cmd.Parameters.AddWithValue("Dr_WorkingHrs", Dr_WrkHrs_Upd_Time.Text)
        conn.Open()
        If cmd.ExecuteNonQuery() = 1 Then
            MessageBox.Show("Data updated successfully!!")
        Else
            MessageBox.Show("Data not updated!!")
        End If
        conn.Close()
        LoadData()
    End Sub

    Private Sub DeleteData()
        ' Delete data from table
        Dim query As String = "DELETE FROM doctor_table WHERE Doctor_Id=@Doctor_Id"
        cmd = New MySqlCommand(query, conn)
        cmd.Parameters.AddWithValue("@Doctor_Id", Dr_Id_Upd_txtbox.Text)
        conn.Open()
        If cmd.ExecuteNonQuery() = 1 Then
            MessageBox.Show("Data deleted successfully!!")
        Else
            MessageBox.Show("Data not deleted!!")
        End If
        conn.Close()
        LoadData()
    End Sub


    Private Sub Dr_Delete_btn_Click(sender As Object, e As EventArgs) Handles Dr_Delete_btn.Click
        DeleteData()
    End Sub


    Private Sub Guna2ControlBox1_Click(sender As Object, e As EventArgs) Handles Guna2ControlBox1.Click
        Dr_Records_Form.Close()
    End Sub




    '----------------------------------Buttons VALIDATION-------------------------------------------------------------

    Private Sub Dr_Register_main_btn_Click(sender As Object, e As EventArgs) Handles Dr_Register_main_btn.Click
        ' Check if any field is empty
        If String.IsNullOrEmpty(Dr_FName_txtbox.Text) OrElse
                String.IsNullOrEmpty(Dr_Category.Text) OrElse
                 String.IsNullOrEmpty(Dr_Id_txtbox.Text) OrElse
                  String.IsNullOrEmpty(Dr_Qualification_txtbox.Text) OrElse
                   String.IsNullOrEmpty(Dr_Exp_txtbox.Text) OrElse
                    String.IsNullOrEmpty(Dr_Reg_date.Text) OrElse
                     String.IsNullOrEmpty(Dr_WrkHrs_Time.Text) Then
            MessageBox.Show("Please fill in all the fields.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        Else
            InsertData()
        End If
    End Sub

    Private Sub Dr_Update_main_btn_Click(sender As Object, e As EventArgs) Handles Dr_Update_main_btn.Click
        If String.IsNullOrEmpty(Dr_Id_Upd_txtbox.Text) OrElse
            String.IsNullOrEmpty(Dr_FName_Upd_txtbox.Text) OrElse
             String.IsNullOrEmpty(Dr_Qualification_Upd_txtbox.Text) OrElse
              String.IsNullOrEmpty(Dr_Exp_Upd_txtbox.Text) OrElse
               String.IsNullOrEmpty(Dr_WrkHrs_Upd_Time.Text) OrElse
                String.IsNullOrEmpty(Dr_Upd_Category.Text) OrElse
                 String.IsNullOrEmpty(Dr_Ph_No_Upd_txtbox.Text) Then
            MessageBox.Show("Please fill in all the fields.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        Else
            UpdateData()
        End If
    End Sub


    '------------------------------------------Name Validation----------------------------------------

    Private Sub Dr_FName_txtbox_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Dr_FName_txtbox.KeyPress, Dr_FName_Upd_txtbox.KeyPress
        ' Check if the pressed key is a letter
        If Not Char.IsLetter(e.KeyChar) AndAlso Not Char.IsControl(e.KeyChar) Then
            e.Handled = True
            MessageBox.Show("Input should be in Letters")
        End If
    End Sub


    '-----------------------------------------Mobile Validation-------------------------------------

    Private Sub Dr_Ph_No_txtbox_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Dr_Ph_No_txtbox.KeyPress, Dr_Ph_No_Upd_txtbox.KeyPress
        ' Allow only digits and backspace key
        If Not Char.IsDigit(e.KeyChar) AndAlso e.KeyChar <> ControlChars.Back Then
            MessageBox.Show("Please enter your mobile number.", "Mobile Number Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            e.Handled = True
        End If
    End Sub

    Private Sub Dr_Ph_No_txtbox_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles Dr_Ph_No_txtbox.Validating, Dr_Ph_No_Upd_txtbox.Validating
        Dim mobileNumber As String = Dr_Ph_No_txtbox.Text.Trim()
        If String.IsNullOrEmpty(mobileNumber) Then
            ' Empty input
            MessageBox.Show("Please enter your mobile number.", "Mobile Number Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            e.Cancel = True
            Return
        End If

        If mobileNumber.Length <> 10 Then
            ' Incorrect length
            MessageBox.Show("Please enter a valid 10-digit mobile number.", "Mobile Number Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            e.Cancel = True
            Return
        End If
    End Sub


End Class
