Public Class Appt_Records_Form
    Private Sub Appt_Records_Form_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        Appt_Form.LoadData()
    End Sub

    Private Sub Appt_DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles Appt_DataGridView1.CellClick
        Appt_Form.Appt_Id_txtbox.Text = Appt_DataGridView1.CurrentRow.Cells(0).Value
        Appt_Form.Appt_Date.Text = Appt_DataGridView1.CurrentRow.Cells(1).Value
        Appt_Form.Appt_Time.Text = Appt_DataGridView1.CurrentRow.Cells(2).Value
        Appt_Form.Pt_Id_txtbox.Text = Appt_DataGridView1.CurrentRow.Cells(3).Value
        Appt_Form.Pt_Name_txtbox.Text = Appt_DataGridView1.CurrentRow.Cells(4).Value
        Appt_Form.Pt_Mobile_txtbox.Text = Appt_DataGridView1.CurrentRow.Cells(5).Value
        Appt_Form.Dr_Id_txtbox.Text = Appt_DataGridView1.CurrentRow.Cells(7).Value
        Appt_Form.Dr_Name_txtbox.Text = Appt_DataGridView1.CurrentRow.Cells(8).Value
    End Sub


End Class