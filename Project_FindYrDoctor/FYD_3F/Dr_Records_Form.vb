Public Class Dr_Records_Form

    Private Sub Dr_Records_Form_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        Dr_Reg_Form.LoadData()
    End Sub

    Private Sub Dr_DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles Dr_DataGridView1.CellClick
        Dr_Reg_Form.Dr_FName_Upd_txtbox.Text = Dr_DataGridView1.CurrentRow.Cells(0).Value
        Dr_Reg_Form.Dr_Id_Upd_txtbox.Text = Dr_DataGridView1.CurrentRow.Cells(1).Value
        Dr_Reg_Form.Dr_Upd_Category.Text = Dr_DataGridView1.CurrentRow.Cells(2).Value
        Dr_Reg_Form.Dr_Ph_No_Upd_txtbox.Text = Dr_DataGridView1.CurrentRow.Cells(3).Value
        Dr_Reg_Form.Dr_Qualification_Upd_txtbox.Text = Dr_DataGridView1.CurrentRow.Cells(4).Value
        Dr_Reg_Form.Dr_Exp_Upd_txtbox.Text = Dr_DataGridView1.CurrentRow.Cells(5).Value
        Dr_Reg_Form.Dr_WrkHrs_Upd_Time.Text = Dr_DataGridView1.CurrentRow.Cells(6).Value
    End Sub

End Class