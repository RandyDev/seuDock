Imports System.Data

Public Class Time

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Form1.Show()
        Me.Close()
    End Sub

    Private Sub btnClockIn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClockIn.Click
        Dim f As New ClockIn
        f.Show()
        Me.Close()

    End Sub

    Private Sub btnClockOut_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClockOut.Click
        Dim f As New ClockOut
        f.Show()
        Me.Close()

    End Sub

    Private Sub btnOutForDay_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        '        Dim f As New ClockOutForDay
        '        f.Show()
        '        Me.Close()
    End Sub

    Private Sub btnReport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReport.Click
        Cursor.Current = Cursors.WaitCursor
        Dim f As New ClockInOutReport
        f.Show()
        Me.Hide()
        Cursor.Current = Cursors.Default
    End Sub

    Private Sub Time_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim els As New EmpLoadStatus
        Dim a As New SqlCeAdapter("\Program Files\SEUdock\SEUdock.sdf")
        Dim strSQL As String = "Select * from employee"
        'get list of all employees
        Dim dtemps As DataTable = a.QueryDatabaseForTable(strSQL)
        'create virtual table to bind to grid
        Dim dtINOUT As New DataTable
        dtINOUT.Columns.Add("ID", GetType(Guid))
        dtINOUT.Columns.Add("Name", GetType(String))
        dtINOUT.Columns.Add("Login", GetType(String))
        dtINOUT.Columns.Add("TimeOn", GetType(String))
        dtINOUT.Columns.Add("Loads", GetType(String))
        Dim tul As Unloader = New Unloader
        If Not dtemps Is Nothing Then
            'if dtemps is not nothing, cycle through records

            For Each row As DataRow In dtemps.Rows
                tul = New Unloader
                Dim strid As String = row.Item("ID").ToString
                Dim strname As String = row.Item("FirstName") & " " & row.Item("Lastname")
                tul.EmployeeID = row.Item("ID")
                tul.EmployeeName = strname
                tul.EmployeeLogin = row.Item("Login")
                Dim str As String = row.Item("firstname") & " " & row.Item("lastname")
                Dim ecount As String = els.countUnloaderLoads(tul.EmployeeID.ToString)

                Dim tpdal As New TimePuncheDAL

                Dim vtimeon As String = tpdal.TimeOn(tul.EmployeeID.ToString)
                If vtimeon <> "NullReferenceException" Then
                    Dim minson As Integer = DateDiff(DateInterval.Minute, CType(vtimeon, DateTime), Date.Now)
                    Dim hrs As Integer = minson / 60
                    Dim mins As Integer = minson Mod 60
                    vtimeon = hrs.ToString & ":" & mins.ToString
                    dtINOUT.Rows.Add(tul.EmployeeID, tul.EmployeeName, tul.EmployeeLogin, vtimeon, ecount)
                ElseIf els.isOnBreak(tul.EmployeeID.ToString) Then
                    dtINOUT.Rows.Add(tul.EmployeeID, tul.EmployeeName, tul.EmployeeLogin, "OnBreak", ecount)
                End If
            Next
        End If
        btnEndOfDay.Enabled = dtINOUT.Rows.Count = 0
    End Sub

    Private Sub CtrlConn1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim f As New asyncConn
        'me.Parent.SendToBack()
        f.Show()
    End Sub



    Private Sub CtrlConn1_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Cursor.Current = Cursors.WaitCursor
        Dim f As asyncConn = New asyncConn
        f.Show()
        Cursor.Current = Cursors.Default
    End Sub

    Private Sub btnEndOfDay_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEndOfDay.Click
        Dim a As New SqlCeAdapter("\Program Files\SEUdock\SEUdock.sdf")
        Dim strSQL As String = "Update WorkOrder Set status=128"
        a.ExecuteNonQuery(strSQL)
        'get list of all employees
        Dim dtemps As DataTable = a.QueryDatabaseForTable(strSQL)
        Dim f As New asyncConn
        f.Show()
    End Sub
End Class