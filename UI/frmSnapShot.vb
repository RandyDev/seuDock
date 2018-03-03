Imports System.DateTime
Imports System.Data.SqlServerCe
Imports System.IO
Imports System.Xml
Imports System.Data
Imports SEUdock.ctrlConn
Imports System.Net
Imports System

Public Class frmSnapShot
    Public locaid As String
    Public locaname As String
    Public locaPrefix As String
    Public subscriber As String
    Private myUserInterfaceUpdateEvent As EventHandler
    Private tableName As String
    Private percentage As Integer
    Private eventStatus As SyncStatus

    Private repl As SqlCeReplication

    Friend Enum SyncStatus
        PercentComplete
        BeginUpload
        BeginDownload
        SyncComplete
    End Enum 'SyncStatus

    Private Sub frmSnapShot_Activated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Activated
        ' Fixing the Status message - 7-20
        '    If mblnUpdateUI Then
        '   End If

    End Sub

    Private Sub frmSnapShot_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.TopMost = True
        Form1.Timer1.Enabled = False
        Timer1.Enabled = True


    End Sub

    Public Sub getSnapShot()
        Form1.Timer1.Enabled = False
        Dim filename As String = "\Program Files\SEUdock\SEUdock.sdf"
        Me.repl = New SqlCeReplication()
        '        repl.SubscriberConnectionString = "Data Source=\Program Files\SEUdock\SEUdock.sdf"
        repl.SubscriberConnectionString = "Data Source='" + filename + "';Password='';" & "Max Database Size='4091';ssce:default lock timeout='4000';Default Lock Escalation ='100';"
        repl.InternetUrl = "http://seudockrepl.seuhh.com/SEUdock/sqlcesa35.dll"
        repl.Publisher = "SQL-SBS"
        repl.PublisherDatabase = "RTDS"
        repl.PublisherSecurityMode = SecurityType.DBAuthentication
        repl.PublisherLogin = "rtds"
        repl.PublisherPassword = "southeast1"
        repl.InternetLogin = "div-log\SEUdockSyncUser" '"MyInternetLogin"
        repl.InternetPassword = "2n@f1s#" '"<password>"
        repl.HostName = locaid 'Guid as string passed mySettings form
        repl.Publication = "SEUdock"

        Dim utl As New Utils
        locaid = utl.getxmldata("Location_ID")
        repl.Subscriber = locaid 'set to LocationName by mySettings Form
        If System.IO.File.Exists(filename) Then
            lblProgress.Visible = True
            lblProgress.Text = "Backing up database"
            '            utl.CreateyesterdayDB()
            System.IO.File.Delete(filename)
        End If
        repl.AddSubscription(AddOption.CreateDatabase)
        lblProgress.Text = String.Empty
        lblProgress.Visible = False
        Cursor.Current = Cursors.WaitCursor
        Try
            repl.Synchronize()
        Catch ex As SqlCeException
            '            MessageBox.Show(ex.Message)
            ' a mechanism to retry until con=true
        Finally
            utl.getholdrecords()
            utl.clearHoldRecords()
            Form1.Timer1.Enabled = True
            Cursor.Current = Cursors.Default
            repl.SaveProperties()
            repl.Dispose()
            Form1.Timer1.Enabled = True
        End Try
        Cursor.Current = Cursors.Default

        Form1.Show()
        Me.Close()
    End Sub 'New

    Private Sub Timer1_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Timer1.Enabled = False
        Cursor.Current = Cursors.WaitCursor
        getSnapShot()
        Cursor.Current = Cursors.Default
        asyncConn.labelLastSyncValue.Text = Date.Now
        Form1.connum += 1
        Dim f As New Form1
        f.CtrlConn1.lblStatus.Text = "Con" & CType(Form1.connum, String)
        f.Show()
        Me.Close()
    End Sub
End Class










