Public Class Pay_Records_Form2

    Private Sub Pay_Records_Form2_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        Payment_Form.LoadData()
    End Sub

    Private Sub Pay_DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles Pay_DataGridView1.CellClick
        Payment_Form.Pay_Id_txtbox.Text = Pay_DataGridView1.CurrentRow.Cells(0).Value
        Payment_Form.Pt_Id_txtbox.Text = Pay_DataGridView1.CurrentRow.Cells(1).Value
        Payment_Form.Pt_Name_txtbox.Text = Pay_DataGridView1.CurrentRow.Cells(2).Value
        Payment_Form.Pay_Amt_txtbox.Text = Pay_DataGridView1.CurrentRow.Cells(3).Value
        Payment_Form.Pay_Mode.Text = Pay_DataGridView1.CurrentRow.Cells(4).Value
        Payment_Form.Pay_Date.Text = Pay_DataGridView1.CurrentRow.Cells(5).Value
    End Sub

End Class