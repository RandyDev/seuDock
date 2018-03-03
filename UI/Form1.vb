Imports System.DateTime
Imports System.Data.SqlServerCe
Imports System.IO
Imports System.Xml
Imports System.Data
Imports SEUdock.Utils
Imports System.Net


Public Class Form1
    Dim objThread1 As CallBackThread
    Dim objThread2 As CallBackThread
    Private locationID As String
    Public utl As New Utils
    Public con As Boolean = False
    Public connum As Integer = 0
#Region "Page Events"

    'Private Sub Form1_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Activated
    'End Sub

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Private Sub Form1_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Activated
        Dim utl As New Utils
        Dim strSyncInterval = utl.getxmldata("Sync_Interval")
        Dim intSyncInterval As Integer = 30000 '30 seconds
        If IsNumeric(strSyncInterval) Then
            intSyncInterval = CType(strSyncInterval, Integer)
        End If
        Timer1.Interval = intSyncInterval
    End Sub

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        lblVersion.Text = System.Reflection.Assembly.GetExecutingAssembly.GetName.Version.ToString()
        Cursor.Current = Cursors.Default
        objThread2 = New CallBackThread(Me, AddressOf ThreadMethod2, AddressOf CallBackMethod2)
        objThread2.Start()
        Dim utl As New Utils
        'do we have SEUdock db
        'do we have config file
        'do we have location db
        'TODO: This line of code loads data into the 'LocationsDataSet.Location' table. You can move, or remove it, as needed.
        '        Timer1.Enabled = Not haveLocations
        'Do I have the files to work with
        If (System.IO.File.Exists("\Program Files\SEUdock\SEUdockConfig.xml")) _
        And (utl.isValidGUID(utl.getxmldata("Location_ID"))) _
        And (System.IO.File.Exists("\Program Files\SEUdock\Locations.sdf")) _
        And (System.IO.File.Exists("\Program Files\SEUdock\SEUdock.sdf")) Then
            Dim file As New FileInfo("\Program Files\SEUdock\SEUdock.sdf")
            Dim sizeInBytes As Long = file.Length
            If sizeInBytes / 1000 > 20 Then 'and it's bigger than a placeholder database
                If con Then 'we have internet connection
                    Try
                        asyncConn.syncme()
                    Catch ex As Exception 'will never get here cuz async
                        CtrlConn1.ForeColor = Color.Red
                        CtrlConn1.lblStatus.Text = "wait"
                        MessageBox.Show("Unable to sync", "Error 101")
                    End Try
                End If  'con
                'SNAPSHOT***************************************************************************
            Else ' this is a placeholder database
                Cursor.Current = Cursors.WaitCursor
                Dim f As mySettings = New mySettings
                f.doconfig = False
                f.Show()
                Cursor.Current = Cursors.Default
                Me.Hide()
            End If 'this is placeholder
        Else ' NO 'Do I have the files to work withneed files
            Cursor.Current = Cursors.WaitCursor
            Dim frm As mySettings = New mySettings
            frm.doconfig = False
            frm.Show()
            Cursor.Current = Cursors.Default
            Me.Hide()
        End If ''Do I have the files to work with
    End Sub

    Private Sub Form1_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles MyBase.Paint
        lblLocalDate.Text = Format(Date.Now, "dd MMM yyyy")
        lblLocalTime.Text = Format(Date.Now, "ddd - h:mm tt")
        Dim localZone As TimeZone = TimeZone.CurrentTimeZone
        Dim baseUTC As DateTime = New DateTime(1900, 1, 1)
        Dim localTime As DateTime = _
        localZone.ToLocalTime(baseUTC)
        Dim localoffset As TimeSpan = _
        localZone.GetUtcOffset(localTime)
        Dim utcDateTime As DateTime = DateAdd(DateInterval.Hour, Math.Abs(localoffset.Hours), Date.Now) '
    End Sub
#End Region

#Region "Button Events"
    Private Sub BtnNewLoad_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnNewLoad.Click
        Cursor.Current = Cursors.WaitCursor
        Dim nl As NewLoad = New NewLoad
        nl.Show()
        Cursor.Current = Cursors.Default
        Me.Hide()
    End Sub

    Private Sub BtnLoadEditor_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnLoadEditor.Click
        Cursor.Current = Cursors.WaitCursor
        Dim ll As LoadList = New LoadList
        ll.Show()
        Cursor.Current = Cursors.Default
        Me.Hide()
    End Sub

    Private Sub BtnTimeAttendance_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnTimeAttendance.Click
        Cursor.Current = Cursors.WaitCursor
        Dim tim As Time = New Time()
        tim.Show()
        Cursor.Current = Cursors.Default
        Me.Hide()
    End Sub

    Private Sub BtnSettings_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSettings.Click
        Cursor.Current = Cursors.WaitCursor
        Dim frm As mySettings = New mySettings
        frm.doconfig = False
        frm.Show()
        Cursor.Current = Cursors.Default
        Me.Hide()
    End Sub

    Private Sub BtnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnExit.Click
        Application.Exit()
    End Sub

    'Private Sub CtrlConn1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CtrlConn1.Click
    '    Cursor.Current = Cursors.WaitCursor
    '    Dim f As asyncConn = New asyncConn
    '    f.Show()
    '    Me.Hide()

    '    Cursor.Current = Cursors.Default

    'End Sub
#End Region

#Region "Threads"
    Private Sub ThreadMethod1()
        Timer1.Enabled = False
        Dim isconnected As Boolean = Utils.Connected
        objThread1.UpdateUI(isconnected)
    End Sub

    Private Sub CallBackMethod1(ByVal status As Boolean)
        'call back method for Thread 1 which will write in Green
        If status Then
            CtrlConn1.lblStatus.ForeColor = Color.Green
            asyncConn.lblNetworkStatus.ForeColor = Color.Green
            connum += 1

            CtrlConn1.lblStatus.Text = "Con" & CType(connum, String)
             con = True
            asyncConn.syncme()
        Else
            CtrlConn1.lblStatus.ForeColor = Color.Red
            asyncConn.lblNetworkStatus.ForeColor = Color.Red
            CtrlConn1.lblStatus.Text = "Con" & CType(connum, String)
            con = False
        End If
        Timer1.Enabled = True
    End Sub

    Private Sub ThreadMethod2()
        Timer1.Enabled = False
        Dim isconnected As Boolean = Utils.Connected
        objThread2.UpdateUI(isconnected)

    End Sub

    Private Sub CallBackMethod2(ByVal status As Boolean)
        'call back method for Thread 2 which will write in Green
        If status Then
            CtrlConn1.lblStatus.ForeColor = Color.Green
            CtrlConn1.lblStatus.Text = "Con" & CType(connum, String)
            con = True
            '            Timer1.Enabled = True
        Else
            CtrlConn1.lblStatus.ForeColor = Color.Red
            CtrlConn1.lblStatus.Text = "Working"
            con = False
            objThread2 = New CallBackThread(Me, AddressOf ThreadMethod2, AddressOf CallBackMethod2)
            objThread2.Start()
        End If
    End Sub
#End Region 'Threads

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Dim utl As New Utils
        Dim strSyncInterval = utl.getxmldata("Sync_Interval")
        Dim intSyncInterval As Integer = 30000 '30 seconds
        If IsNumeric(strSyncInterval) Then
            intSyncInterval = CType(strSyncInterval, Integer) * 1000
        End If
        Timer1.Interval = intSyncInterval
        Timer1.Enabled = False
        objThread1 = New CallBackThread(Me, AddressOf ThreadMethod1, AddressOf CallBackMethod1)
        objThread1.Start()
    End Sub


    Private Sub CtrlConn1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CtrlConn1.Click
        Timer1.Enabled = False
        Dim f As New asyncConn
        f.Show()
        Me.Hide()
    End Sub
End Class


