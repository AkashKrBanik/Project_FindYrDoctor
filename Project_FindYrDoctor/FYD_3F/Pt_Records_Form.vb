Public Class Pt_Records_Form

    Private Sub Pt_Records_Form_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        Pt_Reg_Form.LoadData()
    End Sub

    Private Sub Pt_DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles Pt_DataGridView1.CellClick
        Pt_Reg_Form.Pt_FName_Upd_txtbox.Text = Pt_DataGridView1.CurrentRow.Cells(0).Value
        Pt_Reg_Form.Pt_Id_Upd_txtbox.Text = Pt_DataGridView1.CurrentRow.Cells(1).Value
        Pt_Reg_Form.Pt_Gender_Upd.Text = Pt_DataGridView1.CurrentRow.Cells(2).Value
        Pt_Reg_Form.Pt_Ph_No_Upd_txtbox.Text = Pt_DataGridView1.CurrentRow.Cells(3).Value
        Pt_Reg_Form.Pt_Aadhar_Upd_txtbox.Text = Pt_DataGridView1.CurrentRow.Cells(4).Value
        Pt_Reg_Form.Pt_DOB_Upd.Text = Pt_DataGridView1.CurrentRow.Cells(5).Value
        Pt_Reg_Form.Pt_Age_Upd_txtbox.Text = Pt_DataGridView1.CurrentRow.Cells(6).Value
    End Sub

End Class