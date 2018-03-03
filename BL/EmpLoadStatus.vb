Imports System.Data
Imports System.Data.SqlServerCe

Public Class EmpLoadStatus

    Public Function getFirstNameByeid(ByVal eid As String) As String
        Dim retstr As String = String.Empty
        Dim a As New SqlCeAdapter("\Program Files\SEUdock\SEUdock.sdf")
        Dim strSQL As String = "SELECT firstname from employee where id ='" & eid & "'"
        Try
            retstr = a.QueryDatabaseForScalar(strSQL)
        Catch ex As Exception
            retstr = "--"
        End Try
        Return retstr

    End Function

    Public Function isOnClock(ByVal eid As String) As Boolean
        Dim a As New SqlCeAdapter("\Program Files\SEUdock\SEUdock.sdf")
        Dim strSQL As String = "SELECT e.id,e.FirstName, e.LastName " & _
        "FROM TimePunche INNER JOIN " & _
        "Employee e ON TimePunche.EmployeeID = e.ID INNER JOIN " & _
        "TimeInOut Tio ON TimePunche.ID = Tio.TimepuncheID " & _
        "WHERE ((Tio.TimeOut = '1/1/1900') OR " & _
        "(Tio.TimeOut IS NULL)) AND (e.id = '" & eid & "')"
        Dim dtemponclock As New DataTable
        dtemponclock = a.QueryDatabaseForTable(strSQL)
        Dim empName As String = String.Empty
        If dtemponclock.Rows.Count > 0 Then empName = dtemponclock.Rows(0).Item("FirstName") & dtemponclock.Rows(0).Item("LastName")
        isOnClock = dtemponclock.Rows.Count > 0
        Return isOnClock
    End Function

    Public Function isoutforday(ByVal eid As String) As Boolean
        Dim retbool As Boolean = True
        Dim a As New SqlCeAdapter("\Program Files\SEUdock\SEUdock.sdf")
        Dim strSQL As String = "SELECT e.id,e.FirstName, e.LastName " & _
        "FROM TimePunche INNER JOIN " & _
        "Employee e ON TimePunche.EmployeeID = e.ID INNER JOIN " & _
        "TimeInOut Tio ON TimePunche.ID = Tio.TimepuncheID " & _
        "WHERE (Tio.TimeOut > '1/1/1900') AND (e.id = '" & eid & "') and timepunche.isclosed = 1"
        Dim dtemponclock As New DataTable
        dtemponclock = a.QueryDatabaseForTable(strSQL)
        Dim empName As String = String.Empty
        If dtemponclock.Rows.Count > 0 Then empName = dtemponclock.Rows(0).Item("FirstName") & dtemponclock.Rows(0).Item("LastName")
        retbool = dtemponclock.Rows.Count > 0
        Return retbool
    End Function

    Public Function isOnBreak(ByVal eid As String) As Boolean
        Dim retbool As Boolean = True
        Dim a As New SqlCeAdapter("\Program Files\SEUdock\SEUdock.sdf")
        Dim strSQL As String = "SELECT e.id,e.FirstName, e.LastName " & _
        "FROM TimePunche INNER JOIN " & _
        "Employee e ON TimePunche.EmployeeID = e.ID INNER JOIN " & _
        "TimeInOut Tio ON TimePunche.ID = Tio.TimepuncheID " & _
        "WHERE (Tio.TimeOut > '1/1/1900') AND (e.id = '" & eid & "') and timepunche.isclosed = 0"
        Dim dtemponclock As New DataTable
        dtemponclock = a.QueryDatabaseForTable(strSQL)
        Dim empName As String = String.Empty
        If dtemponclock.Rows.Count > 0 Then empName = dtemponclock.Rows(0).Item("FirstName") & dtemponclock.Rows(0).Item("LastName")
        retbool = dtemponclock.Rows.Count > 0
        Return retbool
    End Function



    Public Function isUnloader(ByVal eid As String) As Boolean


    End Function

    Public Function isOnLoad(ByVal eid As String) As Boolean
        Dim numloads As Integer = 0
        numloads = countUnloaderLoads(eid)
        isOnLoad = numloads > 0
        Return isOnLoad
    End Function


    Public Function getOpenLoads() As DataTable
        Dim dt As New DataTable
        Dim a As New SqlCeAdapter("\Program Files\SEUdock\SEUdock.sdf")
        Dim strSQL As String = "SELECT id from workorder where status < 74"
        Return dt
    End Function

    Public Function getOpenLoaders() As DataTable
        Dim dt As DataTable = New DataTable

        Return dt

    End Function

    Public Function countUnloaderLoads(ByVal eid As String) As Integer
        Dim retstr As Integer = 0
        Dim a As New SqlCeAdapter("\Program Files\SEUdock\SEUdock.sdf")
        Dim strSQL As String = "SELECT COUNT(WorkOrder.ID) AS Expr1 " & _
            "FROM WorkOrder INNER JOIN " & _
            "Unloader ON WorkOrder.ID = Unloader.LoadID " & _
            "WHERE (WorkOrder.Status < 74) " & _
            "AND (Unloader.EmployeeID = '" & eid & "')"
        retstr = a.QueryDatabaseForScalar(strSQL)
        Return retstr
    End Function



End Class
