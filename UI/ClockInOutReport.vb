Imports System.Data

Public Class ClockInOutReport
    Public listcount As Integer = 0
    Dim ts As DataGridTableStyle

    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        Dim f As New Time
        f.Show()
        Me.Close()

    End Sub

    Private Sub ClockInOutReport_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
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
        gridcior.DataSource = dtINOUT
        gridcior.TableStyles.Clear()
        ts = New DataGridTableStyle
        ts.MappingName = dtINOUT.TableName
        For Each myItem As DataColumn In dtINOUT.Columns
            Dim tbcName As DataGridTextBoxColumn = New DataGridTextBoxColumn
            tbcName.HeaderText = myItem.ColumnName
            tbcName.MappingName = myItem.ColumnName
            ts.GridColumnStyles.Add(tbcName)
            gridcior.TableStyles.Add(ts)
            tbcName = Nothing
        Next

        ts.GridColumnStyles(0).Width = 0
        ts.GridColumnStyles(1).HeaderText = "Name"
        ts.GridColumnStyles(1).Width = 149
        ts.GridColumnStyles(2).HeaderText = "Login"
        ts.GridColumnStyles(2).Width = 60
        ts.GridColumnStyles(3).HeaderText = "HrsON"
        ts.GridColumnStyles(3).Width = 58
        ts.GridColumnStyles(4).HeaderText = "Lds"
        ts.GridColumnStyles(4).Width = 33
    End Sub


    Private Sub CtrlConn1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim f As New asyncConn
        'me.Parent.SendToBack()
        f.Show()

    End Sub

    Private Sub gridcior_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles gridcior.Click
        If gridcior.CurrentRowIndex >= 0 Then
            Dim els As New EmpLoadStatus
            gridcior.Select(gridcior.CurrentRowIndex)
            Dim emp As String = gridcior(gridcior.CurrentRowIndex, 0).ToString
            Dim hasloads As Integer = els.countUnloaderLoads(emp)
            Button1.Enabled = hasloads > 0
        End If
    End Sub

    Private Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim emp As String = gridcior(gridcior.CurrentRowIndex, 0).ToString
        Dim f As New LoadList
        f.eid = emp
        f.Show()
        Me.Close()
    End Sub

    Private Sub gridcior_CurrentCellChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles gridcior.CurrentCellChanged
        gridcior.Select(gridcior.CurrentRowIndex)
    End Sub
End Class