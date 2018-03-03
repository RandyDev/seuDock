Public Class EmployeeList

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Dim f1 As Form1 = New Form1
        Form1.Show()
        Me.Hide()
    End Sub
End Class