Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.Button
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.ToolBar
Imports MySql.Data.MySqlClient

Public Class Report_Form
    Private Sub Home_btn_Click(sender As Object, e As EventArgs) Handles Home_btn.Click
        Me.Hide()
        Main_Form.Show()
        Main_Form.Select()
    End Sub

    Private Sub Report_RecView_btn_Click(sender As Object, e As EventArgs) Handles Report_RecView_btn.Click
        Report_Records_Form.Show()
        Report_Records_Form.Select()
    End Sub

    Private Sub Report_clear_btn_Click(sender As Object, e As EventArgs) Handles Report_clear_btn.Click
        Report_Id_txtbox.Clear()
        Pay_Id_txtbox.Clear()
        Pt_Id_txtbox.Clear()
        Pt_Name_txtbox.Clear()
        Dr_Id_txtbox.Clear()
        Dr_Name_txtbox.Clear()
        Report_RichTextBox.Clear()
    End Sub

    '-------------------------------DATABASE-------------------------------------------------------------------------------------------------------------

    Dim conn As New MySqlConnection("Data Source=localhost;database=database_fyd3;userid=root;password=''")
    Dim cmd As MySqlCommand

    Public Sub RandomNumber()
        Dim rand As New Random()
        Dim customerId As Integer = rand.Next(100000, 999999)
        Dim formattedId As String = customerId.ToString("D3")
        Report_Id_txtbox.Text = formattedId
    End Sub

    Private Sub Report_Form_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        NewReport_RadioButton.Checked = True
        RandomNumber()
        Dim currentDate As Date = Date.Now
        Report_Date.Text = currentDate.ToString("dd-MMM-yy")
        Report_Records_Form.Show()
        LoadData()
    End Sub

    Public Sub LoadData()
        ' Select data from table and load into DataGridView
        Dim query As String = "Select * From report_table"
        Dim da As MySqlDataAdapter = New MySqlDataAdapter(query, conn)
        Dim dt As DataTable = New DataTable()
        da.Fill(dt)
        Report_Records_Form.Report_DataGridView1.DataSource = dt
    End Sub

    Private Sub InsertData()
        ' Insert data into table
        Dim query As String = "INSERT INTO report_table(Report_Id,Payment_Id,Patient_Id,Pt_Name,Doctor_Id,Dr_Name,Report,Date) values(@Report_Id,@Payment_Id,@Patient_Id,@Pt_Name,@Doctor_Id,@Dr_Name,@Report,@Date)"
        cmd = New MySqlCommand(query, conn)
        cmd.Parameters.AddWithValue("Report_Id", Report_Id_txtbox.Text)
        cmd.Parameters.AddWithValue("Payment_Id", Pay_Id_txtbox.Text)
        cmd.Parameters.AddWithValue("Patient_Id", Pt_Id_txtbox.Text)
        cmd.Parameters.AddWithValue("Pt_Name", Pt_Name_txtbox.Text)
        cmd.Parameters.AddWithValue("Doctor_Id", Dr_Id_txtbox.Text)
        cmd.Parameters.AddWithValue("Dr_Name", Dr_Name_txtbox.Text)
        cmd.Parameters.AddWithValue("Report", Report_RichTextBox.Text)
        cmd.Parameters.AddWithValue("Date", Report_Date.Text)
        conn.Open()
        If cmd.ExecuteNonQuery() = 1 Then
            MessageBox.Show("Data inserted successfully.")
        Else
            MessageBox.Show("Data not inserted.")
        End If
        conn.Close()
        LoadData()
    End Sub

    Private Sub UpdateData()
        ' Update data in table
        Dim query As String = "UPDATE report_table SET Payment_Id=@Payment_Id,Patient_Id=@Patient_Id,Pt_Name=@Pt_Name, Doctor_Id=@Doctor_Id, Dr_Name=@Dr_Name,Report=@Report,Date=@Date WHERE Report_Id=@Report_Id"
        cmd = New MySqlCommand(query, conn)
        cmd.Parameters.AddWithValue("Report_Id", Report_Id_txtbox.Text)
        cmd.Parameters.AddWithValue("Payment_Id", Pay_Id_txtbox.Text)
        cmd.Parameters.AddWithValue("Patient_Id", Pt_Id_txtbox.Text)
        cmd.Parameters.AddWithValue("Pt_Name", Pt_Name_txtbox.Text)
        cmd.Parameters.AddWithValue("Doctor_Id", Dr_Id_txtbox.Text)
        cmd.Parameters.AddWithValue("Dr_Name", Dr_Name_txtbox.Text)
        cmd.Parameters.AddWithValue("Report", Report_RichTextBox.Text)
        cmd.Parameters.AddWithValue("Date", Report_Date.Text)
        conn.Open()
        If cmd.ExecuteNonQuery() = 1 Then
            MessageBox.Show("Report Updated!!")
        Else
            MessageBox.Show("Report Not updated!!")
        End If
        conn.Close()
        LoadData()
    End Sub

    Private Sub Guna2ControlBox1_Click(sender As Object, e As EventArgs) Handles Guna2ControlBox1.Click
        Report_Records_Form.Close()
    End Sub

    Private Sub Report_Update_btn_Click(sender As Object, e As EventArgs) Handles Report_Update_btn.Click
        ' Update button click event
        UpdateData()
    End Sub



    '-------------------------------------Id Verifying & Data Retrieving-------------------------------------------------------------------------------

    Private Sub Pymt_btnSearch_Click(sender As Object, e As EventArgs) Handles Pymt_btnSearch.Click
        Dim paymentId As String = Pay_Id_txtbox.Text.Trim()
        If Not String.IsNullOrEmpty(paymentId) Then
            GetPaymentDetails(paymentId)
        Else
            MessageBox.Show("Please enter a valid Payment ID.")
        End If
    End Sub

    Private Sub GetPaymentDetails(paymentId As String)
        Dim query As String = "SELECT Patient_Id,Pt_Name FROM payment_table WHERE Payment_Id = @Payment_Id"
        Using command As New MySqlCommand(query, conn)
            command.Parameters.AddWithValue("@Payment_Id", paymentId)
            conn.Open()
            Dim reader As MySqlDataReader = command.ExecuteReader()
            If reader.Read() Then
                Pt_Id_txtbox.Text = reader("Patient_Id").ToString()
                Pt_Name_txtbox.Text = reader("Pt_Name").ToString()
            Else
                MessageBox.Show("Payment ID not found!! Please enter a valid Payment ID.")
                Pay_Id_txtbox.Text = String.Empty
            End If
            reader.Close()
        End Using
        conn.Close()
    End Sub

    Private Sub Dr_btnSearch_Click(sender As Object, e As EventArgs) Handles Dr_btnSearch.Click
        Dim doctorId As String = Dr_Id_txtbox.Text.Trim()
        If Not String.IsNullOrEmpty(doctorId) Then
            GetDoctorDetails(doctorId)
        Else
            MessageBox.Show("Please enter a valid Doctor ID.")
        End If
    End Sub

    Private Sub GetDoctorDetails(doctorId As String)
        Dim query As String = "SELECT Dr_Name FROM doctor_table WHERE Doctor_Id = @Doctor_Id"
        Using command As New MySqlCommand(query, conn)
            command.Parameters.AddWithValue("@Doctor_Id", doctorId)
            conn.Open()
            Dim reader As MySqlDataReader = command.ExecuteReader()
            If reader.Read() Then
                Dr_Name_txtbox.Text = reader("Dr_Name").ToString()
            Else
                MessageBox.Show("Doctor ID not found!! Please enter a valid Doctor ID.")
                Dr_Name_txtbox.Text = String.Empty
            End If
            reader.Close()
        End Using
        conn.Close()
    End Sub



    '-------------------------------------Form Validation-------------------------------------------------------------------------------------------------------

    Private Sub Report_main_btn_Click(sender As Object, e As EventArgs) Handles Report_main_btn.Click
        ' Check if any field is empty
        If String.IsNullOrEmpty(Report_Id_txtbox.Text) OrElse
            String.IsNullOrEmpty(Pay_Id_txtbox.Text) OrElse
                String.IsNullOrEmpty(Dr_Id_txtbox.Text) OrElse
                 String.IsNullOrEmpty(Pt_Id_txtbox.Text) OrElse
                  String.IsNullOrEmpty(Pt_Name_txtbox.Text) OrElse
                   String.IsNullOrEmpty(Dr_Name_txtbox.Text) OrElse
                    String.IsNullOrEmpty(Report_RichTextBox.Text) OrElse
                     String.IsNullOrEmpty(Report_Date.Text) Then
            MessageBox.Show("Please fill in all the fields.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        Else
            InsertData()
        End If
    End Sub

    Private Sub NewReport_RadioButton_CheckedChanged(sender As Object, e As EventArgs) Handles NewReport_RadioButton.CheckedChanged
        If NewReport_RadioButton.Checked Then
            Report_clear_btn.PerformClick()
            Report_main_btn.Enabled = True
            Report_Update_btn.Enabled = False
            RandomNumber()
            Report_Id_txtbox.ReadOnly = True
            Report_main_btn.Visible = True
            Report_Update_btn.Hide()
            Report_Form_Load(sender, e)
        End If
    End Sub

    Private Sub UpdReport_RadioButton_CheckedChanged(sender As Object, e As EventArgs) Handles UpdReport_RadioButton.CheckedChanged
        If UpdReport_RadioButton.Checked Then
            Report_main_btn.Enabled = False
            Report_Update_btn.Enabled = True
            Report_Id_txtbox.Clear()
            Report_Id_txtbox.ReadOnly = True
            Report_main_btn.Hide()
            Report_Update_btn.Visible = True
            Report_clear_btn.PerformClick()
        End If
    End Sub


End Class

