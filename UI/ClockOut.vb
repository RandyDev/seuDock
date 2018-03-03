Imports System.Data
Imports System.Data.SqlServerCe
Public Class ClockOut

    Private Sub ClockOut_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        loademployees()
        lblTimeIn.Text = Format(Date.Now(), "h:mm tt")
    End Sub

    Private Sub loademployees()
        Dim els As New EmpLoadStatus
        Dim a As New SqlCeAdapter("\Program Files\SEUdock\SEUdock.sdf")
        Dim dtAllEmps As New DataTable
        Dim strSQL As String = "SELECT e.id, e.LastName + ', '+ e.FirstName as Name,e.login FROM Employee e"
        Dim dtoffclock As New DataTable
        dtAllEmps = a.QueryDatabaseForTable(strSQL)
        strSQL = "SELECT e.id,e.FirstName, e.LastName, e.login " & _
        "FROM TimePunche tp INNER JOIN " & _
        "Employee e ON tp.EmployeeID = e.ID INNER JOIN " & _
        "TimeInOut Tio ON tp.ID = Tio.TimepuncheID " & _
        "WHERE (Tio.TimeOut = '1/1/1900') OR " & _
        "(Tio.TimeOut IS NULL) Order by e.LastName"
        Dim tul As Unloader = New Unloader
        Dim dtclockout As DataTable = New DataTable
        dtclockout.Columns.Add("ID", GetType(Guid))
        dtclockout.Columns.Add("Name", GetType(String))
        For Each emp As DataRow In dtAllEmps.Rows
            'are they not on the clock or they are working a load
            Dim strEmpID As String = emp.Item("ID").ToString
            If (Not els.isOnClock(strEmpID)) Then
                emp.Delete() 'remove from list
            Else
                tul = New Unloader
                Dim strid As String = emp.Item("ID").ToString
                Dim strname As String = emp.Item("Name")
                tul.EmployeeID = emp.Item("ID")
                tul.EmployeeName = emp.Item("Name")
                tul.EmployeeLogin = emp.Item("Login")
                dtclockout.Rows.Add(tul.EmployeeID, tul.EmployeeName & " (" & tul.EmployeeLogin & ")")
            End If
        Next
        If dtAllEmps.Rows.Count > 0 Then
            lbEmployeeList.DataSource = dtclockout
            lbEmployeeList.DisplayMember = "Name"
            lbEmployeeList.ValueMember = "ID"
            '            lbEmployeeList.SelectedIndex = -1
        End If
    End Sub

    Private Function validform() As String
        Dim retstr As String = String.Empty
        If lbEmployeeList.SelectedIndex = -1 Then
            retstr = "You MUST select an Employee"
        End If
        Return retstr
    End Function

    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        'process changes
        Dim errstr As String = validform()
        If errstr > String.Empty Then
            Dim result1 As DialogResult = MessageBox.Show(errstr, "Required Information Missing", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1)
        Else
            Dim otime As DateTime = Date.Now
            Dim empid As String = lbEmployeeList.SelectedValue.ToString
            Dim pin As String = txtpin.Text
            Dim a As New SqlCeAdapter("\Program Files\SEUdock\SEUdock.sdf")
            Dim dtemployee As New DataTable
            Dim strSQL As String = "SELECT e.Login, e.Password FROM Employee e WHERE e.ID = '" & empid & "'"
            dtemployee = a.QueryDatabaseForTable(strSQL)
            Dim ba As New SqlCeAdapter("\Program Files\SEUdock\SEUdock.sdf")
            If dtemployee.Rows.Count > 0 Then
                Dim row As DataRow = dtemployee.Rows(0)
                Dim pass As String = row.Item("Password")
                If pin = pass Then

                    '                    tp.EmployeeID = New Guid(empid)
                    '                    tp.DateWorked = stime.ToShortDateString
                    '                    tp.IsClosed = False
                    '                   tp.LocationID = New Guid(NewLoad.getxmldata("Location_ID"))
                    'Find Timepunche record
                    strSQL = "SELECT e.id as eid, e.FirstName, e.LastName, tp.id as tpid, tio.id as tioid  " & _
                    "FROM TimePunche tp INNER JOIN " & _
                    "Employee e ON tp.EmployeeID = e.ID INNER JOIN " & _
                    "TimeInOut Tio ON tp.ID = Tio.TimepuncheID " & _
                    "WHERE ((Tio.TimeOut = '1/1/1900') OR " & _
                    "(Tio.TimeOut IS NULL)) AND (e.id = '" & empid & "') order by dateworked"
                    Dim dtemponclock As New DataTable
                    dtemponclock = a.QueryDatabaseForTable(strSQL)
                    Dim tioid As String = dtemponclock.Rows(0).Item("tioid").ToString
                    Dim tpid As String = dtemponclock.Rows(0).Item("tpid").ToString
                    If tioid > String.Empty Then
                        If ChkBxOutForDay.Checked Then
                            strSQL = "Update Timepunche set isclosed = 1  WHERE id ='" & tpid & "'"
                            Try
                                a.ExecuteNonQuery(strSQL)
                            Catch ex As Exception
                                Dim err As String = ex.Message
                            End Try
                        End If
                        strSQL = "UPDATE TimeInOut set TimeOut='" & otime & "' WHERE id ='" & tioid & "'"
                        Try
                            a.ExecuteNonQuery(strSQL)
                        Catch ex As Exception
                            Dim err As String = ex.Message
                        End Try
                    End If
                    'update tio record
                    loademployees()
                    ChkBxOutForDay.Checked = False
                    txtpin.Text = String.Empty
                    'create new timepunche EmployeeID, DepartmentID, DateWorked, IsClosed, ID, LocationID
                    'create new TimeInOut ID, TimepuncheID, TimeIn, TimeOUt, IsHourly
                Else
                    Dim result1 As DialogResult = MessageBox.Show("PIN Number Incorrect!", "Pin Number Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1)
                End If
            End If
        End If
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        '        If Utils.Connected Then
        'asyncConn.syncme()
        'End If
        Dim f As New Time
        f.Show()
        Me.Close()
    End Sub

    Private Sub CtrlConn1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub CtrlConn1_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim f As New asyncConn
        'me.Parent.SendToBack()
        f.Show()
    End Sub

    Private Sub ChkBxOutForDay_CheckStateChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkBxOutForDay.CheckStateChanged
        If lbEmployeeList.SelectedIndex > -1 Then
            Dim id As String = lbEmployeeList.SelectedValue.ToString
            Dim els As New EmpLoadStatus
            If ChkBxOutForDay.Checked Then
                If els.countUnloaderLoads(id) > 0 Then
                    MessageBox.Show(els.getFirstNameByeid(id) & " may not clock" & vbCrLf & " 'Out for Day'" & vbCrLf & " while load is open", "Not Allowed")
                    ChkBxOutForDay.Checked = False
                End If
            End If
        End If
    End Sub

    Private Sub lbEmployeeList_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbEmployeeList.SelectedIndexChanged
        ChkBxOutForDay.Checked = False
    End Sub
End Class