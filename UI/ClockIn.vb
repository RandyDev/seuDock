Imports System.Data
Imports System.Data.SqlServerCe


Public Class ClockIn
    Public utl As New Utils
    Private Sub ClockIn_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        loadJobDescriptions()
        loadDepartments()
        loademployees()
        lblTimeIn.Text = Format(Date.Now(), "h:mm tt")
    End Sub

    Private Sub loadJobDescriptions()
        Dim a As New SqlCeAdapter("\Program Files\SEUdock\SEUdock.sdf")
        Dim dtJobDescription As New DataTable
        'Dim strSQL = "SELECT JobDescriptions.JobDescription, JobDescriptions.IsHourly, JobDescriptions.IsActive, JobDescriptions.ID, LocationJobDescriptions.CustBillingRate" & _
        '    "FROM JobDescriptions INNER JOIN " & _
        '    "LocationJobDescriptions ON JobDescriptions.ID = LocationJobDescriptions.JobDescriptionID " & _
        '    "WHERE LocationJobDescriptions.LocationID = HOST_NAME()"
        dtJobDescription = a.QueryDatabaseForTable("select JobDescription, ID from JobDescriptions Order By JobDescription")
        cbJobDescription.DataSource = dtJobDescription
        cbJobDescription.DisplayMember = "JobDescription"
        cbJobDescription.ValueMember = "ID"
        cbJobDescription.SelectedIndex = -1
    End Sub

    Private Sub loadDepartments()
        Dim a As New SqlCeAdapter("\Program Files\SEUdock\SEUdock.sdf")
        Dim dtDepartment As DataTable = New DataTable
        dtDepartment = a.QueryDatabaseForTable("select ID as DeptID, Name as DeptName from Department Order By DeptName")
        cbDepartment.DisplayMember = "DeptName"
        cbDepartment.ValueMember = "DeptID"
        cbDepartment.DataSource = dtDepartment
        cbDepartment.SelectedIndex = -1
        lblJobTask.Text = "Job Description"
        lblJobTask.ForeColor = Color.Black
    End Sub

    Private Sub cbJobDescription_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbJobDescription.SelectedIndexChanged
        If cbJobDescription.SelectedIndex > -1 Then

            lblJobTask.Text = cbJobDescription.Text
            lblJobTask.ForeColor = Color.Red
            If cbJobDescription.Text.Contains("Unload") Then
                If cbJobDescription.Text.Contains("Pct") Then
                    lblJobTask.Text = "Unloader PERCENT"
                Else
                    lblJobTask.Text = "Unloader HOURLY"
                End If
                lblDepartment.Enabled = False
                cbDepartment.Refresh()
                cbDepartment.SelectedIndex = -1
                cbDepartment.Enabled = False
            Else
                lblDepartment.Enabled = True
                cbDepartment.Enabled = True

                '                cbDepartment.ValueMember = "27d94af9-4e89-476b-b310-7d2c48165d7d"
            End If
            '            Dim descriptionID As String = cbJobDescription.SelectedValue.ToString
            '            Dim a As New SqlCeAdapter("\Program Files\SEUdock\SEUdock.sdf")
            '            Dim strSQL As String = "SELECT IsHourly from JobDescriptions where ID = " & descriptionID
            '            Dim chkd As String = a.QueryDatabaseForScalar(strSQL)
        End If
    End Sub
    Private Sub loademployees()
        Dim els As New EmpLoadStatus ' as set of utilities for dealing with emplo
        Dim a As New SqlCeAdapter("\Program Files\SEUdock\SEUdock.sdf")
        Dim dtAllEmps As New DataTable
        Dim strSQL As String = "SELECT e.id, e.LastName + ', '+ e.FirstName as Name, e.Login as Login FROM Employee e"
        dtAllEmps = a.QueryDatabaseForTable(strSQL)
        Dim dtoffclock As New DataTable
        strSQL = "SELECT e.id,e.LastName + ', ' + e.FirstName as Name, e.Login as Login " & _
        "FROM TimePunche INNER JOIN " & _
        "Employee e ON TimePunche.EmployeeID = e.ID INNER JOIN " & _
        "TimeInOut Tio ON TimePunche.ID = Tio.TimepuncheID " & _
        "WHERE (Tio.TimeOut = '1/1/1900') OR " & _
        "(Tio.TimeOut IS NULL) order by e.LastName,e.First"
        dtoffclock = a.QueryDatabaseForTable(strSQL)
        Dim tul As Unloader = New Unloader

        Dim dtclockin As DataTable = New DataTable
        dtclockin.Columns.Add("ID", GetType(Guid))
        dtclockin.Columns.Add("Name", GetType(String))
        If Not dtAllEmps Is Nothing Then
            For Each emp As DataRow In dtAllEmps.Rows
                If els.isOnClock(emp.Item("ID").ToString) Then
                    emp.Delete()
                Else
                    tul = New Unloader

                    Dim strid As String = emp.Item("ID").ToString
                    Dim strname As String = emp.Item("Name")
                    tul.EmployeeID = emp.Item("ID")
                    tul.EmployeeName = emp.Item("Name")
                    tul.EmployeeLogin = emp.Item("Login")

                    dtclockin.Rows.Add(tul.EmployeeID, tul.EmployeeName & " (" & tul.EmployeeLogin & ")")

                End If
            Next


            lbEmployeeList.DataSource = dtclockin
            lbEmployeeList.DisplayMember = "Name"
            lbEmployeeList.ValueMember = "ID"
            lbEmployeeList.SelectedIndex = -1
        End If

    End Sub




    Private Function validform() As String
        Dim retstr As String = String.Empty
        If cbJobDescription.SelectedIndex = -1 Then
            retstr &= "JobDescription" & vbCrLf
        End If
        If cbDepartment.Enabled Then
            If cbDepartment.SelectedIndex = -1 Then
                If Not cbJobDescription.Text.Contains("Unload") Then
                    retstr &= "Department" & vbCrLf
                End If
            End If
        End If
        If lbEmployeeList.SelectedIndex = -1 Then
            retstr &= "Employee" & vbCrLf
        End If
        If txtPIN.Text = "" Then
            retstr &= "PIN number" & vbCrLf
        End If
        If retstr > "" Then retstr = "Required Fields:" & vbCrLf & retstr
        Return retstr

    End Function
    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click

        'process changes
        Dim errstr As String = validform()
        If errstr > String.Empty Then
            Dim result1 As DialogResult = MessageBox.Show(errstr, "Required Stuff Missing", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1)
        Else
            Dim jobdescid As String = cbJobDescription.SelectedValue.ToString
            Dim stime As DateTime = Date.Now

            Dim empid As String = lbEmployeeList.SelectedValue.ToString

            Dim pin As String = txtPIN.Text
            Dim a As New SqlCeAdapter("\Program Files\SEUdock\SEUdock.sdf")
            Dim dtemployee As New DataTable
            Dim strSQL As String = "SELECT e.Login, e.Password FROM Employee e WHERE e.ID = '" & empid & "'"
            dtemployee = a.QueryDatabaseForTable(strSQL)
            If dtemployee.Rows.Count > 0 Then
                Dim row As DataRow = dtemployee.Rows(0)
                Dim pass As String = row.Item("Password")

                If pin = pass Then
                    Dim tpdal As New TimePuncheDAL
                    Dim tp As New TimePunche
                    'this will return the most recent timepunche where isclosed=false
                    'or it will return an empty timepunche object
                    tp = tpdal.getLatestOpenTimePunchByEmployeeID(empid)
                    Dim utl As New Utils
                    If utl.isValidGUID(tp.ID.ToString) Then 'if it is valid,then a timepunche was found
                        ' do nothing
                    Else 'otherwise, populate a new timepunche
                        tp.ID = Guid.NewGuid
                        tp.EmployeeID = New Guid(empid)
                        tp.DateWorked = stime.ToShortDateString
                        tp.IsClosed = False
                        tp.LocationID = New Guid(utl.getxmldata("Location_ID"))
                    End If
                    'Create new TimeInOut object

                    Dim tio As New TimeInOut
                    '************
                    ' Everything else is hourly!?!??!
                    tio.isHourly = cbJobDescription.Text <> ("Unloading Pct")
                    '************

                    tio.ID = Guid.NewGuid
                    tio.TimepuncheID = tp.ID
                    tio.TimeIn = stime
                    tio.JobDescriptionID = New Guid(jobdescid)
                    If cbJobDescription.Text.Contains("Unload") Then 'put em in General'
                        tp.DepartmentID = New Guid("27d94af9-4e89-476b-b310-7d2c48165d7d")
                        cbJobDescription.SelectedIndex = -1
                    Else
                        tp.DepartmentID = New Guid(cbDepartment.SelectedValue.ToString)
                    End If
                    'Create lists of TimeInOut objects to attach to TimePunche object
                    Dim tiolist As New List(Of TimeInOut)
                    'add the new TimeInOut object to the tiolist
                    tiolist.Add(tio)
                    'set the tiolist to theTimepunche.tpList property  
                    tp.tpList = tiolist
                    'spin TimePunche Data Access Layer
                    'insert TimePunch record
                    Dim str As String = tpdal.insertTimePunche(tp)
                    'insert TimeInOut record
                    Dim str2 As String = tpdal.insertTIO(tp.tpList.Item(0))
                    'reset controls
                    loademployees()
                    clearForm()
                Else
                    Dim result1 As DialogResult = MessageBox.Show("PIN Number Incorrect!", "ooophf", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1)
                End If
            End If
        End If
    End Sub

    Private Sub clearForm()
        loadJobDescriptions()
        cbJobDescription.SelectedIndex = -1
        cbDepartment.Enabled = True
        cbDepartment.SelectedIndex = -1
        txtPIN.Text = String.Empty
        lblJobTask.ForeColor = Color.Black
        lblJobTask.Text = "Job Description"

    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        'process changes

        Try
            'If Utils.Connected Then
            '    asyncConn.syncme()
            'End If
            Dim f As Time = New Time
            f.Show()
            Me.Close()
        Catch ex As Exception
            Dim er As String = ex.Message
        End Try

    End Sub


    Private Sub CtrlConn1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim f As asyncConn = New asyncConn
        'me.Parent.SendToBack()
        f.Show()
    End Sub

End Class