Imports System.Threading
Imports System.Timers
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports MySql.Data.MySqlClient
Imports Mysqlx.Datatypes.Scalar.Types

Public Class Appt_Form
    Private conn As MySqlConnection
    Private cmd As MySqlCommand

    Public Sub New()
        InitializeComponent()
        conn = New MySqlConnection("Data Source=localhost;database=database_fyd3;userid=root;password=''")
    End Sub

    Private Sub Pt_RecView_btn_Click(sender As Object, e As EventArgs) Handles Pt_RecView_btn.Click
        Pt_Records_Form.Show()
        Pt_Records_Form.Select()
    End Sub

    Private Sub Dr_RecView_btn_Click(sender As Object, e As EventArgs) Handles Dr_RecView_btn.Click
        Dr_Records_Form.Show()
        Dr_Records_Form.Select()
    End Sub

    Private Sub Home_btn_Click(sender As Object, e As EventArgs) Handles Home_btn.Click
        Me.Hide()
        Main_Form.Show()
        Main_Form.Select()
    End Sub

    Private Sub Appt_RecView_btn_Click(sender As Object, e As EventArgs) Handles Appt_RecView_btn.Click
        Appt_Records_Form.Show()
        Appt_Records_Form.Select()
    End Sub

    Private Sub Appt_Clear_btn_Click(sender As Object, e As EventArgs) Handles Appt_Clear_btn.Click
        Appt_Id_txtbox.Clear()
        Appt_Date.ResetText()
        Appt_Time.SelectedIndex = -1 ' Clears the selected item
        Pt_Id_txtbox.Clear()
        Pt_Name_txtbox.Clear()
        Pt_Mobile_txtbox.Clear()
        Dr_Id_txtbox.Clear()
        Dr_Name_txtbox.Clear()
        Dr_Category_txtbox.Clear()
        RandomNumber()
    End Sub

    Public Sub RandomNumber()
        'Set random appointment id 
        Dim rand As New Random()
        Dim customerId As Integer = rand.Next(100000, 999999)
        Dim formattedId As String = customerId.ToString("D3")
        Appt_Id_txtbox.Text = formattedId
    End Sub

    Private Sub Appt_Form_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        RandomNumber()
        Dim currentDate As Date = DateTime.Now
        Dim maxDate As Date = currentDate.AddMonths(3)
        Appt_Date.MinDate = currentDate
        Appt_Date.MaxDate = maxDate

        'Load data into datagridview on form load
        Appt_Records_Form.Show()
        LoadData()
    End Sub

    Public Sub LoadData()
        Dim query As String = "SELECT * FROM appointment_table"
        Using da As New MySqlDataAdapter(query, conn)
            Using dt As New DataTable()
                da.Fill(dt)
                Appt_Records_Form.Appt_DataGridView1.DataSource = dt
            End Using
        End Using
    End Sub

    Private Sub InsertData()
        ' Insert data into table
        Dim query As String = "INSERT INTO appointment_table(Appt_Id,Appt_Date,Appt_Time,Patient_Id,Pt_Name,Pt_Mobile,Doctor_Id,Dr_Name,Dr_Category) VALUES(@Appt_Id,@Appt_Date,@Appt_Time,@Patient_Id,@Pt_Name,@Pt_Mobile,@Doctor_Id,@Dr_Name,@Dr_Category)"
        Using cmd As New MySqlCommand(query, conn)
            cmd.Parameters.AddWithValue("@Appt_Id", Appt_Id_txtbox.Text)
            cmd.Parameters.AddWithValue("@Appt_Date", Appt_Date.Text)
            cmd.Parameters.AddWithValue("@Appt_Time", Appt_Time.Text)
            cmd.Parameters.AddWithValue("@Patient_Id", Pt_Id_txtbox.Text)
            cmd.Parameters.AddWithValue("@Pt_Name", Pt_Name_txtbox.Text)
            cmd.Parameters.AddWithValue("@Pt_Mobile", Pt_Mobile_txtbox.Text)
            cmd.Parameters.AddWithValue("@Doctor_Id", Dr_Id_txtbox.Text)
            cmd.Parameters.AddWithValue("@Dr_Name", Dr_Name_txtbox.Text)
            cmd.Parameters.AddWithValue("@Dr_Category", Dr_Category_txtbox.Text)
            conn.Open()
            If cmd.ExecuteNonQuery() = 1 Then
                MessageBox.Show("Appointment Booking Confirmed!!")
            Else
                MessageBox.Show("Appointment Booking Unsuccessful!!")
            End If
        End Using
        conn.Close()
        Appt_Clear_btn.PerformClick()
        LoadData()
    End Sub

    Private Sub UpdateData()
        ' Update data in table
        Dim query As String = "UPDATE appointment_table SET Appt_Date=@Appt_Date,Appt_Time=@Appt_Time,Patient_Id=@Patient_Id, Pt_Name=@Pt_Name, Pt_Mobile=@Pt_Mobile,Doctor_Id=@Doctor_Id,Dr_Name=@Dr_Name,Dr_Category=@Dr_Category WHERE Appt_Id=@Appt_Id"
        Using cmd As New MySqlCommand(query, conn)
            cmd.Parameters.AddWithValue("@Appt_Id", Appt_Id_txtbox.Text)
            cmd.Parameters.AddWithValue("@Appt_Date", Appt_Date.Text)
            cmd.Parameters.AddWithValue("@Appt_Time", Appt_Time.Text)
            cmd.Parameters.AddWithValue("@Patient_Id", Pt_Id_txtbox.Text)
            cmd.Parameters.AddWithValue("@Pt_Name", Pt_Name_txtbox.Text)
            cmd.Parameters.AddWithValue("@Pt_Mobile", Pt_Mobile_txtbox.Text)
            cmd.Parameters.AddWithValue("@Doctor_Id", Dr_Id_txtbox.Text)
            cmd.Parameters.AddWithValue("@Dr_Name", Dr_Name_txtbox.Text)
            conn.Open()
            If cmd.ExecuteNonQuery() = 1 Then
                MessageBox.Show("Appointment Updated!!")
            Else
                MessageBox.Show("Appointment Not updated!!")
            End If
        End Using
        conn.Close()
        LoadData()
    End Sub

    Private Sub DeleteData()
        ' Delete data from table
        Dim query As String = "DELETE FROM appointment_table WHERE Appt_Id=@Appt_Id"
        Using cmd As New MySqlCommand(query, conn)
            cmd.Parameters.AddWithValue("@Appt_Id", Appt_Id_txtbox.Text)
            conn.Open()
            If cmd.ExecuteNonQuery() = 1 Then
                MessageBox.Show("Appointment deleted successfully!!")
            Else
                MessageBox.Show("Appointment not deleted!!")
            End If
        End Using
        conn.Close()
        LoadData()
    End Sub

    Private Sub Appt_Book_btn_Click(sender As Object, e As EventArgs) Handles Appt_Book_btn.Click
        ' Check if any field is empty
        If String.IsNullOrEmpty(Appt_Id_txtbox.Text) OrElse
            String.IsNullOrEmpty(Appt_Time.Text) OrElse
            String.IsNullOrEmpty(Appt_Date.Text) OrElse
            String.IsNullOrEmpty(Pt_Id_txtbox.Text) OrElse
            String.IsNullOrEmpty(Pt_Mobile_txtbox.Text) OrElse
            String.IsNullOrEmpty(Pt_Name_txtbox.Text) OrElse
            String.IsNullOrEmpty(Dr_Name_txtbox.Text) OrElse
            String.IsNullOrEmpty(Dr_Category_txtbox.Text) OrElse
            String.IsNullOrEmpty(Dr_Id_txtbox.Text) Then
            MessageBox.Show("Please fill in all the fields.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        Else
            InsertData()
        End If
    End Sub

    Private Sub Appt_Update_btn_Click(sender As Object, e As EventArgs) Handles Appt_Update_btn.Click
        ' Update button click event
        UpdateData()
    End Sub

    Private Sub Appt_Delete_btn_Click_1(sender As Object, e As EventArgs) Handles Appt_Delete_btn.Click
        ' Delete button click event
        DeleteData()
    End Sub

    Private Sub Guna2ControlBox1_Click(sender As Object, e As EventArgs) Handles Guna2ControlBox1.Click
        Appt_Records_Form.Close()
    End Sub

    '-------------------------------ID Validation------------------------------------

    Private Sub Pt_Id_txtbox_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Pt_Id_txtbox.KeyPress, Dr_Id_txtbox.KeyPress
        If Not Char.IsLetterOrDigit(e.KeyChar) AndAlso Not Char.IsControl(e.KeyChar) Then
            MessageBox.Show("Special Characters such as @,#,$,%,etc are not allowed here!!")
            e.Handled = True
        End If
    End Sub

    '-------------------------------ID Verification------------------------------------

    Private Sub Dr_btnSearch_Click(sender As Object, e As EventArgs) Handles Dr_btnSearch.Click
        Dim doctorId As String = Dr_Id_txtbox.Text.Trim()
        If Not String.IsNullOrEmpty(doctorId) Then
            GetDoctorDetails(doctorId)
        Else
            MessageBox.Show("Please enter a valid Doctor ID.")
        End If
    End Sub

    Private Sub GetDoctorDetails(doctorId As String)
        Dim query As String = "SELECT Dr_Name,Dr_Category FROM doctor_table WHERE Doctor_ID = @Doctor_ID"
        Using command As New MySqlCommand(query, conn)
            command.Parameters.AddWithValue("@Doctor_ID", doctorId)
            conn.Open()
            Dim reader As MySqlDataReader = command.ExecuteReader()
            If reader.Read() Then
                Dr_Name_txtbox.Text = reader("Dr_Name").ToString()
                Dr_Category_txtbox.Text = reader("Dr_Category").ToString()
            Else
                MessageBox.Show("Doctor ID not found.")
                DrClearTextboxes()
            End If
            reader.Close()
        End Using
        conn.Close()
    End Sub

    Private Sub DrClearTextboxes()
        Dr_Name_txtbox.Text = String.Empty
        Dr_Category_txtbox.Text = String.Empty
    End Sub

    Private Sub Pt_btnSearch_Click(sender As Object, e As EventArgs) Handles Pt_btnSearch.Click
        Dim patientId As String = Pt_Id_txtbox.Text.Trim()
        If Not String.IsNullOrEmpty(patientId) Then
            GetPatientDetails(patientId)
        Else
            MessageBox.Show("Please enter a valid Patient ID.")
        End If
    End Sub

    Private Sub GetPatientDetails(patientId As String)
        Dim query As String = "SELECT Pt_Name,Pt_Mobile FROM patient_table WHERE Patient_ID = @Patient_ID"
        Using command As New MySqlCommand(query, conn)
            command.Parameters.AddWithValue("@Patient_ID", patientId)
            conn.Open()
            Dim reader As MySqlDataReader = command.ExecuteReader()
            If reader.Read() Then
                Pt_Name_txtbox.Text = reader("Pt_Name").ToString()
                Pt_Mobile_txtbox.Text = reader("Pt_Mobile").ToString()
            Else
                MessageBox.Show("Patient ID not found.")
                PtClearTextboxes()
            End If
            reader.Close()
        End Using
        conn.Close()
    End Sub

    Private Sub PtClearTextboxes()
        Pt_Name_txtbox.Text = String.Empty
        Pt_Mobile_txtbox.Text = String.Empty
    End Sub

    Private Sub Pt_Id_txtbox_TextChanged(sender As Object, e As EventArgs) Handles Pt_Id_txtbox.TextChanged
        Pt_Name_txtbox.Clear()
        Pt_Mobile_txtbox.Clear()
    End Sub

    Private Sub Dr_Id_txtbox_TextChanged(sender As Object, e As EventArgs) Handles Dr_Id_txtbox.TextChanged
        Dr_Name_txtbox.Clear()
        Dr_Category_txtbox.Clear()
    End Sub

End Class
