Imports System.Data
Imports System.Windows.Forms
Imports System.Threading
Imports System
Imports System.Text
Imports System.Reflection
Imports System.Xml
Imports System.Collections
Imports System.ComponentModel
Imports System.Data.SqlServerCe

Public Class SelectUnloader
#Region "Class Properties"
    <Flags()> _
    Enum statusEnums
        is_done = finished
        todo = Undefined
        Undefined = 0
        CheckedIn = 1
        Assigned = 2
        Printed = 4
        AddDataChanged = 8
        Complete = 64
        finished = 128
    End Enum

    Public isEdit As Boolean = False

    Private LoadStatus As statusEnums

    Private _cwo As WorkOrder
    Public Property cwo() As WorkOrder
        Get
            Return _cwo
        End Get
        Set(ByVal value As WorkOrder)
            _cwo = value
        End Set
    End Property

    Private _emptbl As DataTable
    Public Property emptbl() As DataTable
        Get
            Return _emptbl
        End Get
        Set(ByVal value As DataTable)
            _emptbl = value
        End Set
    End Property
#End Region
#Region "Page Events"
    Private Sub SelectUnloader_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        getAvailableUnloaders()
    End Sub
#End Region


    ' Shared variables  
    Dim con As SqlCeConnection = New SqlCeConnection("Data Source=\Program Files\SEUdock\SEUdock.sdf")
    Dim cmd As SqlCeCommand
    Dim myDA As SqlCeDataAdapter
    Dim myDataSet As DataSet
    'Binding database table to DataGridView  

    Public Sub ShowData()
        Dim param As SqlCeParameter = New SqlCeParameter
        param = New SqlCeParameter("@locaID", "xx")
        cmd.Parameters.Add(param)
        If con.State = Data.ConnectionState.Closed Then con.Open()
        myDA = New SqlCeDataAdapter(cmd)
        myDataSet = New DataSet()
        myDA.Fill(myDataSet, "MyTable")
        '        DataGridView1.DataSource = myDataSet.Tables("MyTable").DefaultView
    End Sub

    Private Sub getAvailableUnloaders()
        Dim els As New EmpLoadStatus
        lbAvailableUnloaders.DataSource = Nothing
        Dim a As New SqlCeAdapter("\Program Files\SEUdock\SEUdock.sdf")
        Dim dtUnloaders As New DataTable
        Dim strSQL As String = "SELECT Employee.ID, Employee.FirstName + ' ' + Employee.LastName AS Name,+ + Employee.Login , JobDescriptions.JobDescription" & _
        " FROM TimePunche INNER JOIN" & _
        " Employee ON TimePunche.EmployeeID = Employee.ID INNER JOIN" & _
        " TimeInOut ON TimePunche.ID = TimeInOut.TimepuncheID INNER JOIN" & _
        " JobDescriptions ON TimeInOut.JobDescriptionID = JobDescriptions.ID" & _
        " WHERE (JobDescriptions.JobDescription LIKE N'%unload%') AND (TimeInOut.TimeOut = '1/1/1900 12:00:00 AM' OR TimeInOut.TimeOut IS NULL)"
        dtUnloaders = a.QueryDatabaseForTable(strSQL)
        'create table for available unloaders (dtUnloaders - wo.unladers)
        '        dtUnloaders = a.QueryDatabaseForTable(strSQL)
        Dim dtAvailableUnloaders As New DataTable
        dtAvailableUnloaders.Columns.Add("ID", GetType(Guid))
        dtAvailableUnloaders.Columns.Add("Name", GetType(String))
        Dim tul As New Unloader
        If Not cwo.Unloaders Is Nothing Then
            Dim ulList As List(Of Unloader) = cwo.Unloaders
            'DirectCast(wo.Unloaders, List(Of Unloader))
            For Each row As DataRow In dtUnloaders.Rows
                tul = New Unloader
                Dim strid As String = row.Item("ID").ToString
                Dim strname As String = row.Item("Name")
                tul.EmployeeID = row.Item("ID")
                tul.EmployeeName = row.Item("Name")
                tul.EmployeeLogin = row.Item("Login")
                If ulList.Find(Function(m As Unloader) m.EmployeeID = tul.EmployeeID) IsNot Nothing Then
                Else
                    Dim ecount As String = els.countUnloaderLoads(tul.EmployeeID.ToString)
                    dtAvailableUnloaders.Rows.Add(tul.EmployeeID, tul.EmployeeName & " (" & tul.EmployeeLogin & ") " & ecount)
                End If
            Next
        Else
            For Each row As DataRow In dtUnloaders.Rows
                tul = New Unloader
                Dim strid As String = row.Item("ID").ToString
                Dim strname As String = row.Item("Name")
                tul.EmployeeID = row.Item("ID")
                tul.EmployeeName = row.Item("Name")
                tul.EmployeeLogin = row.Item("Login")
                Dim ecount As String = els.countUnloaderLoads(tul.EmployeeID.ToString).ToString
                dtAvailableUnloaders.Rows.Add(tul.EmployeeID, tul.EmployeeName & " (" & tul.EmployeeLogin & ") " & ecount)
            Next
        End If
        lbAvailableUnloaders.DataSource = dtAvailableUnloaders
        lbAvailableUnloaders.DisplayMember = "Name"
        lbAvailableUnloaders.ValueMember = "ID"
        lbAvailableUnloaders.SelectedIndex = -1
    End Sub

#Region "Button Events"

    Private Sub btnAddUnloader_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddUnloader.Click
        Dim unldr As New Unloader
        If Not lbAvailableUnloaders.SelectedValue Is Nothing Then
            unldr = getUnloader(lbAvailableUnloaders.SelectedValue.ToString)
            If cwo.Unloaders Is Nothing Then
                Dim unloaders As List(Of Unloader) = New List(Of Unloader)
                cwo.Unloaders = unloaders
                unloaders = DirectCast(cwo.Unloaders, List(Of Unloader))
                unloaders.Add(unldr)
            Else
                Dim unloaders As List(Of Unloader) = DirectCast(cwo.Unloaders, List(Of Unloader))
                unloaders.Add(unldr)
                cwo.Unloaders = unloaders
            End If
            If cwo.Unloaders.Count = 1 Then
                cwo.StartTime = Date.Now
            End If
            If isEdit Then 'adding unloader from Load Editor form
                Dim f As New LoadEditor
                f.curwo = cwo
                f.fromAddUnloaders = True
                f.Show()
            Else
                Dim f As New NewLoad 'adding unloader via New Load form
                f.fromAddUnloaders = True
                f.curwo = cwo
                f.Show()
            End If
            Me.Close()
        Else
            MessageBox.Show("Select Unloader", "No Unloader Selected")
        End If
    End Sub

    Private Sub BtnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnCancel.Click
        If isEdit Then
            Dim f As New LoadEditor
            f.curwo = cwo
            f.fromAddUnloaders = True
            f.Show()
        Else
            Dim f As New NewLoad
            f.fromAddUnloaders = True
            f.curwo = cwo
            f.Show()
        End If
        Me.Close()
    End Sub
#End Region

    Public Function getUnloader(ByVal id As String) As Unloader
        Dim retUnloader As New Unloader
        Dim a As New SqlCeAdapter("\Program Files\SEUdock\SEUdock.sdf")
        Dim strSQL As String = "select id, firstname + ' ' + Lastname as name,Login, Password from employee where id ='" & id & "'"
        Dim dtUnloader As DataTable
        Try
            dtUnloader = a.QueryDatabaseForTable(strSQL)
            retUnloader.EmployeeName = dtUnloader.Rows(0).Item("Name")
            retUnloader.EmployeeID = New Guid(id)
            retUnloader.EmployeeLogin = dtUnloader.Rows(0).Item("Login")
        Catch ex As Exception

        End Try
        Return retUnloader
    End Function

End Class
