Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports MySql.Data.MySqlClient
Imports Mysqlx.Datatypes.Scalar.Types

Public Class Payment_Form
    Private Sub Home_btn_Click(sender As Object, e As EventArgs) Handles Home_btn.Click
        Me.Hide()
        Main_Form.Show()
        Main_Form.Select()
    End Sub

    Private Sub Pay_RecView_btn_Click(sender As Object, e As EventArgs) Handles Pay_RecView_btn.Click
        Pay_Records_Form2.Show()
        Pay_Records_Form2.Select()
    End Sub

    Private Sub Pt_RecView_btn_Click(sender As Object, e As EventArgs) Handles Pt_RecView_btn.Click
        Pt_Records_Form.Show()
        Pt_Records_Form.Select()
    End Sub

    Private Sub Pay_clear_btn_Click(sender As Object, e As EventArgs) Handles Pay_clear_btn.Click
        Pay_Id_txtbox.Clear()
        Pt_Id_txtbox.Clear()
        Pt_Name_txtbox.Clear()
        Pay_Amt_txtbox.Clear()
        Pay_Mode.SelectedItem = "UPI"
        RandomNumber()
    End Sub


    '-------------------------------DATABASE-------------------------------------------------------------------------------------------------------------

    Dim conn As New MySqlConnection("Data Source=localhost;database=database_fyd3;userid=root;password=''")
    Dim cmd As MySqlCommand

    Public Sub RandomNumber()
        Dim rand As New Random()
        Dim customerId As Integer = rand.Next(100000, 999999)
        Dim formattedId As String = customerId.ToString("D3")
        Pay_Id_txtbox.Text = formattedId
    End Sub

    Private Sub Payment_Form_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        RandomNumber()
        ' Retrieve the current system date
        Dim currentDate As Date = Date.Now
        Pay_Date.Text = currentDate.ToString("dd-MMM-yy")
        Pay_Mode.SelectedItem = "UPI"
        LoadData()
    End Sub

    Public Sub LoadData()
        Dim query As String = "SELECT * FROM payment_table"
        Dim da As MySqlDataAdapter = New MySqlDataAdapter(query, conn)
        Dim dt As DataTable = New DataTable()
        da.Fill(dt)
        Pay_Records_Form2.Pay_DataGridView1.DataSource = dt
    End Sub

    Private Sub InsertData()
        ' Insert data into table
        Dim query As String = "INSERT INTO payment_table(Payment_Id,Patient_Id,Pt_Name,Pay_Amt,Pay_Mode,Date) values(@Payment_Id,@Patient_Id,@Pt_Name,@Pay_Amt,@Pay_Mode,@Date)"
        cmd = New MySqlCommand(query, conn)
        cmd.Parameters.AddWithValue("Payment_Id", Pay_Id_txtbox.Text)
        cmd.Parameters.AddWithValue("Patient_Id", Pt_Id_txtbox.Text)
        cmd.Parameters.AddWithValue("Pt_Name", Pt_Name_txtbox.Text)
        cmd.Parameters.AddWithValue("Pay_Amt", Pay_Amt_txtbox.Text)
        cmd.Parameters.AddWithValue("Pay_Mode", Pay_Mode.Text)
        cmd.Parameters.AddWithValue("Date", Pay_Date.Text)
        conn.Open()
        If cmd.ExecuteNonQuery() = 1 Then
            MessageBox.Show("Payment Success.")
        Else
            MessageBox.Show("Payment Failure.")
        End If
        conn.Close()
        LoadData()
    End Sub

    Private Sub Pay_main_btn_Click(sender As Object, e As EventArgs) Handles Pay_main_btn.Click
        If String.IsNullOrEmpty(Pay_Id_txtbox.Text) OrElse
            String.IsNullOrEmpty(Pt_Id_txtbox.Text) OrElse
             String.IsNullOrEmpty(Pt_Name_txtbox.Text) OrElse
              String.IsNullOrEmpty(Pay_Amt_txtbox.Text) OrElse
               String.IsNullOrEmpty(Pay_Mode.Text) OrElse
                String.IsNullOrEmpty(Pay_Date.Text) Then
            MessageBox.Show("Please fill in all the fields.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        Else
            InsertData()
        End If
    End Sub

    Private Sub Guna2ControlBox1_Click(sender As Object, e As EventArgs) Handles Guna2ControlBox1.Click
        Pay_Records_Form2.Close()
        Pt_Records_Form.Close()
    End Sub


    Private Sub Pay_Amt_txtbox_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Pay_Amt_txtbox.KeyPress
        ' Check if the entered key is a digit or a control key (like backspace)
        If Not Char.IsDigit(e.KeyChar) AndAlso Not Char.IsControl(e.KeyChar) Then
            MessageBox.Show("Amount can have only numbers as input!!")
            e.Handled = True              ' Marking the event as handled to prevent the character from being entered
        End If
    End Sub



    '--------------------------------------Id Verifying & Data Retrieving----------------------------------------------------------------------------

    Private Sub Pt_btnSearch_Click(sender As Object, e As EventArgs) Handles Pt_btnSearch.Click
        Dim patientId As String = Pt_Id_txtbox.Text.Trim()
        If Not String.IsNullOrEmpty(patientId) Then
            GetPatientDetails(patientId)
        Else
            MessageBox.Show("Please enter a valid Patient ID.")
        End If
    End Sub

    Private Sub GetPatientDetails(patientId As String)
        Dim query As String = "SELECT Pt_Name FROM patient_table WHERE Patient_ID = @Patient_ID"
        Using command As New MySqlCommand(query, conn)
            command.Parameters.AddWithValue("@Patient_ID", patientId)
            conn.Open()
            Dim reader As MySqlDataReader = command.ExecuteReader()
            If reader.Read() Then
                Pt_Name_txtbox.Text = reader("Pt_Name").ToString()
            Else
                MessageBox.Show("Patient ID not found!! Please enter a valid Patient ID.")
                Pt_Name_txtbox.Text = String.Empty
            End If
            reader.Close()
        End Using
        conn.Close()
    End Sub


End Class


