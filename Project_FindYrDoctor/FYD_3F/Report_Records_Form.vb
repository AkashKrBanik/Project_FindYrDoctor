Public Class Report_Records_Form
    Private Sub Report_Records_Form_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        Report_Form.LoadData()
    End Sub

    Private Sub Report_DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles Report_DataGridView1.CellClick
        If Report_Form.UpdReport_RadioButton.Checked = True Then
            Report_Form.Report_Id_txtbox.Text = Report_DataGridView1.CurrentRow.Cells(0).Value
            Report_Form.Pay_Id_txtbox.Text = Report_DataGridView1.CurrentRow.Cells(1).Value
            Report_Form.Pt_Id_txtbox.Text = Report_DataGridView1.CurrentRow.Cells(2).Value
            Report_Form.Pt_Name_txtbox.Text = Report_DataGridView1.CurrentRow.Cells(3).Value
            Report_Form.Dr_Id_txtbox.Text = Report_DataGridView1.CurrentRow.Cells(4).Value
            Report_Form.Dr_Name_txtbox.Text = Report_DataGridView1.CurrentRow.Cells(5).Value
            Report_Form.Report_RichTextBox.Text = Report_DataGridView1.CurrentRow.Cells(6).Value
            Report_Form.Report_Date.Text = Report_DataGridView1.CurrentRow.Cells(7).Value
        End If
    End Sub

End Class