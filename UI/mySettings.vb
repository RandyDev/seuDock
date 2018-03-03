Imports System.Data
Imports System.Data.SqlServerCe
Imports System.IO
Imports System.Xml
Imports SEUdock.Utils
Imports System.Windows.Forms
Imports Microsoft.WindowsCE.Forms
Imports FieldSoftware.PrinterCE_NetCF
Imports System.Threading

Public Class mySettings
#Region "settings"
    Public doconfig As Boolean
    Public locaPrefix As String
    Public utl As New Utils
    Public init As Integer = 0 ' determines page's load count
    Public locaid As String
    Public subscriber As String
    Private tableName As String
    Private percentage As Integer
    Private eventStatus As SyncStatus
    Private repl As SqlCeReplication
    Private ar As IAsyncResult
    Private haveLocations As Boolean = False

    Friend Enum SyncStatus
        PercentComplete
        BeginUpload
        BeginDownload
        SyncComplete
    End Enum 'SyncStatus
    '*********************************************************************************************************
    'for accessing/setting dropdown box settings
    Public Const CB_SHOWDROPDOWN As Integer = &H14F
    '    Public Const CB_GETDROPPEDSTATE = "&H14f<true" '0x0157<false
    Private _ErrorText As String
    Private _CommandExecuted As Boolean
    Private m_fOkToUpadateAutoComplete As Boolean
    Private m_sLastSearchedFor As String = ""

    Public Sub New()
        InitializeComponent()
    End Sub 'New
    Public Shared Function SetDroppedDown(ByVal comboBox As ComboBox) As Boolean 'to open combo(drop down) boxes
        Dim comboBoxDroppedMsg As Message = Message.Create(comboBox.Handle, CB_SHOWDROPDOWN, CType(1, IntPtr), IntPtr.Zero)
        '        Dim comboBoxDroppedMsg As Message = Message.Create(comboBox.Handle, CB_SHOWDROPDOWN, DirectCast(0, IntPtr), IntPtr.Zero) 'to close
        MessageWindow.SendMessage(comboBoxDroppedMsg)
        Return comboBoxDroppedMsg.Result <> IntPtr.Zero
    End Function
#End Region

    Private Sub Settings_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Form1.Timer1.Enabled = False
        Me.TopMost = True
        Dim utl As New Utils
        If System.IO.File.Exists("\Program Files\SEUdock\Locations.sdf") _
        And (utl.isValidGUID(utl.getxmldata("Location_ID"))) Then 'we have good loca and config
            loadLocations()
            cbLocation.SelectedValue = utl.getxmldata("Location_ID")
        Else ' something not right
            If Not System.IO.File.Exists("\Program Files\SEUdock\Locations.sdf") Then
                If Form1.con Then
                    GetLocationSnapShot()
                    loadLocations()
                    SetDroppedDown(cbLocation)
                Else 'con is false
                    lblNetworkConnection.Text = "network not found"
                End If 'form1.con
            Else 'locations file exist
                loadLocations()
                SetDroppedDown(cbLocation)
            End If 'location sdf exist

        End If ' location files ok
        doconfig = True
    End Sub

    Private Sub btnReturn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReturn.Click
        Cursor.Current = Cursors.Default
        Form1.Show()
        Form1.Timer1.Enabled = True
        Me.Close()
    End Sub

    Private Sub loadLocations()
        If cbLocation.Items.Count > 0 Then
            cbLocation.DataSource = Nothing
        End If
        Dim a As New SqlCeAdapter("\Program Files\SEUdock\Locations.sdf")
        Dim dtlocations As New DataTable
        dtlocations = a.QueryDatabaseForTable("SELECT distinct(Location.Name)AS Name,Location.ID AS ID FROM Location INNER JOIN ParentCompany ON Location.ParentCompanyID = ParentCompany.ID ORDER BY Name")
        cbLocation.DisplayMember = "Name"
        cbLocation.ValueMember = "ID"
        cbLocation.DataSource = dtlocations
        Dim utl As New Utils
        '        doconfig = True
    End Sub

    Private Sub btnSnapShot_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSnapShot.Click
        Dim utl As New Utils
        ' utl.CreateyesterdayDB()

        btnReturn.Enabled = False
        If cbLocation.SelectedIndex = -1 Then
            MessageBox.Show("Select Location")
            Exit Sub
        Else
            Dim f As frmSnapShot = New frmSnapShot
            f.locaid = cbLocation.SelectedValue.ToString
            f.Show()
            Me.Close()
        End If

    End Sub

    Private Sub GetLocationSnapShot()
        System.IO.File.Delete("\Program Files\SEUdock\Locations.sdf")
        Dim repl As SqlCeReplication = New SqlCeReplication
        Me.Refresh()
        repl.InternetUrl = "http://seudockrepl.seuhh.com/SEUdock/sqlcesa35.dll"
        repl.Publisher = "SQL-SBS"
        repl.PublisherDatabase = "RTDS"
        repl.PublisherSecurityMode = SecurityType.DBAuthentication
        repl.PublisherLogin = "rtds"
        repl.PublisherPassword = "southeast1"
        repl.InternetLogin = "div-log\SEUdockSyncUser" '"MyInternetLogin"
        repl.InternetPassword = "2n@f1s#" '"<password>"
        'repl.HostName = "cfe1f703-f8d3-4f23-b30c-192672b13bcc"
        repl.Publication = "rtdsLocations"
        repl.Subscriber = "Locations"
        repl.SubscriberConnectionString = "Data Source=\Program Files\SEUdock\Locations.sdf"
        'delete existing .sdf file if exist
        'TO DO ... check for SEUdock.sdf location table.
        If System.IO.File.Exists("\Program Files\SEUdock\Locations.sdf") Then
            System.IO.File.Delete("\Program Files\SEUdock\Locations.sdf")
        Else
            Cursor.Current = Cursors.Default
        End If
        Cursor.Current = Cursors.WaitCursor
        Try
            'init new Locations Subscription for Settings Page.
            repl.AddSubscription(AddOption.CreateDatabase)
            repl.Synchronize()
        Catch ex As Exception
            MessageBox.Show("Failed to initialize 'Locations' Subscription" & vbCrLf & _
                            ex.Message, "Subscription Init Failed!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1)
        Finally
            repl.Dispose()
            Cursor.Current = Cursors.Default
        End Try
        lblNetworkConnection.Text = ""
        SetDroppedDown(cbLocation)
        Cursor.Current = Cursors.Default
        btnSnapShot.Enabled = True
        btnReturn.Enabled = True

    End Sub

    Private Sub btnLocations_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLocations.Click
        Cursor.Current = Cursors.WaitCursor
        btnLocations.Enabled = False
        lblNetworkConnection.Text = "Downloading Locations ..."
        GetLocationSnapShot()
        btnLocations.Enabled = True
        Cursor.Current = Cursors.Default

    End Sub

    Private Sub Label2_ParentChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim f As New mySettings
        f.Show()
        Me.Close()

    End Sub

    Private Sub cbLocation_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbLocation.SelectedIndexChanged
        If doconfig Then
            Cursor.Current = Cursors.WaitCursor
            If Not cbLocation.SelectedValue Is Nothing Then
                locaid = cbLocation.SelectedValue.ToString
                lblNetworkConnection.Text = "Building New Configuration File"
                Dim locationName As String = String.Empty
                ''start gathering information to create SEUdockConfig.xml
                Dim ParentCompanyName As String = String.Empty
                Dim ParentCompanyID As String = String.Empty
                Dim syncInterval As Integer = 20
                Dim printTimeStamp As Boolean = False
                'gather config data
                Dim a As New SqlCeAdapter("\Program Files\SEUdock\Locations.sdf")
                Dim dt As DataTable = New DataTable
                Dim strqry As String = "select Name,ParentCompanyID, hhSyncInterval, hhPrintTimeStamp  From Location WHERE (Location.ID = '" & locaid & "')"
                dt = a.QueryDatabaseForTable(strqry)
                If Not dt Is Nothing Then
                    If dt.Rows.Count > 0 Then
                        For Each row As DataRow In dt.Rows
                            ParentCompanyID = row.Item("ParentCompanyID").ToString
                            locationName = row.Item("Name")
                            syncInterval = row.Item("hhSyncInterval").ToString
                            printTimeStamp = row.Item("hhPrintTimeStamp").ToString
                        Next
                    End If
                    strqry = "select Name From ParentCompany where (id= '" & ParentCompanyID & "')"
                    ParentCompanyName = a.QueryDatabaseForScalar(strqry)
                    'finish config data gathering and create config xml file
                    utl.createConfig(locationName, locaid, "", ParentCompanyName, ParentCompanyID, syncInterval, printTimeStamp)
                    btnReturn.Enabled = True
                    lblNetworkConnection.Text = "Configuration File Created" & vbCrLf & locationName
                End If
                Cursor.Current = Cursors.Default
            End If
        End If
    End Sub

End Class



