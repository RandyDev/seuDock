Imports System.Data

Public Class LoadList
    Dim ts As DataGridTableStyle
    Public eid As String = String.Empty
    Public dups As Boolean = Nothing
    <Flags()> _
Enum statusEnums
        is_done = 207
        todo = Undefined
        Undefined = 0
        CheckedIn = 1
        Assigned = 2
        Printed = 4
        AddDataChanged = 8
        Complete = 64
        finished = 128
    End Enum

    Private Sub LoadList_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
    End Sub
    Private Sub LoadList_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Activated
        populateLoadList()
    End Sub


    'Private Sub LoadList_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
    'End Sub

    Public Sub populateLoadList()
        dgloadlist.DataSource = Nothing
        'TO DO .... Allow for unloader specific completed loads
        Dim a As New SqlCeAdapter("\Program Files\SEUdock\SEUdock.sdf")
        Dim strSQL As String = String.Empty
        Dim utl As New Utils
        Dim dtLoadList As DataTable = New DataTable
        If dups Then
            lblempName.Text = "Completed"
            btnInProgress.Visible = True
            btnCompleted.Visible = False
            btnExit.Text = "<--Back To Main"
            strSQL = "Select WorkOrder.ID,WorkOrder.PurchaseOrder AS PO#,Workorder.DoorNumber AS Dr#, WorkOrder.Status AS Status From workorder where status > 73 order by PO#"
        ElseIf eid > "" Then
            lblempName.Text = utl.getempname(eid)
            btnCompleted.Visible = True
            btnInProgress.Visible = False
            btnExit.Text = "<--Back"
            strSQL = "Select WorkOrder.ID,WorkOrder.PurchaseOrder AS PO#,Workorder.DoorNumber AS Dr#, Status" & _
                " FROM WorkOrder INNER JOIN" & _
                " Unloader ON WorkOrder.ID = Unloader.LoadID" & _
                " WHERE (WorkOrder.Status < 78)" & _
                " AND (Unloader.EmployeeID = '" & eid & "') order by PO#"
        Else
            lblempName.Text = "In Progress"
            btnInProgress.Visible = False
            btnCompleted.Visible = True
            btnExit.Text = "<--Back To Main"
            '            btnCompleted.Visible = True
            strSQL = "Select WorkOrder.ID,WorkOrder.PurchaseOrder AS PO#,Workorder.DoorNumber AS Dr#, Status From workorder where status < " & 73 & " order by PO#"
        End If
        Dim dt As DataTable = a.QueryDatabaseForTable(strSQL)
        If Not dt Is Nothing Then

            If dt.Rows.Count > 0 Then
                dtLoadList.Columns.Add("ID", GetType(Guid))
                dtLoadList.Columns.Add("PO#", GetType(String))
                dtLoadList.Columns.Add("Dr#", GetType(String))
                dtLoadList.Columns.Add("Status", GetType(String))
                For Each row As DataRow In dt.Rows
                    Dim sched As Boolean = IsDBNull(row.Item("Dr#")) Or row.Item("Dr#") = ""
                    dtLoadList.Rows.Add(row.Item("ID"), row.Item("PO#"), row.Item("Dr#"), utl.getstatusText(row.Item("Status"), sched))
                Next
                dgloadlist.DataSource = dtLoadList
                dgloadlist.TableStyles.Clear()
                ts = New DataGridTableStyle
                ts.MappingName = dtLoadList.TableName
                For Each myItem As DataColumn In dt.Columns
                    Dim tbcName As DataGridTextBoxColumn = New DataGridTextBoxColumn
                    tbcName.HeaderText = myItem.ColumnName
                    tbcName.MappingName = myItem.ColumnName
                    ts.GridColumnStyles.Add(tbcName)
                    dgloadlist.TableStyles.Add(ts)
                    tbcName = Nothing
                Next
                ts.GridColumnStyles(0).Width = 0
                ts.GridColumnStyles(1).Width = 150
                ts.GridColumnStyles(2).Width = 35
                ts.GridColumnStyles(3).Width = 113
            End If
        End If
    End Sub


    Private Sub dgloadlist_CurrentCellChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgloadlist.CurrentCellChanged
        dgloadlist.Select(dgloadlist.CurrentRowIndex)
    End Sub

    Private Sub btnExit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExit.Click
        If btnExit.Text = "<--Back To Main" Then
            Form1.Show()
            Me.Close()
        Else
            Dim f As ClockInOutReport = New ClockInOutReport
            f.Show()
            Me.Close()
        End If
    End Sub


    Private Sub dgloadlist_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgloadlist.Click
        If dgloadlist.CurrentRowIndex > -1 Then
            Cursor.Current = Cursors.WaitCursor
            dgloadlist.Select(dgloadlist.CurrentRowIndex)
            Dim val As String = dgloadlist(dgloadlist.CurrentRowIndex, 0).ToString
            Try
                Dim f As LoadEditor = New LoadEditor
                f.woid = val
                f.dups = dups
                f.eid = eid
                f.Show()
                Me.Close()
            Catch ex As Exception
                MessageBox.Show(ex.Message & ": This Load has MOVED to the Server", "Unable to find Load")
            End Try
            Cursor.Current = Cursors.Default
        End If
    End Sub

    Private Sub btnInProgress_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnInProgress.Click
        eid = Nothing
        dups = False
        lblempName.Text = "In Progress"
        btnInProgress.Visible = False
        btnCompleted.Visible = True
        populateLoadList()
    End Sub
    
    Private Sub btnCompleted_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCompleted.Click
        eid = Nothing
        dups = True
        populateLoadList()
    End Sub

    Private Sub CtrlConn1_Click1(ByVal sender As Object, ByVal e As System.EventArgs)
        Cursor.Current = Cursors.WaitCursor
        Dim f As asyncConn = New asyncConn
        f.Show()
        Cursor.Current = Cursors.Default
    End Sub

End Class