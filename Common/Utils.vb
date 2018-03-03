Imports System
Imports System.Data
Imports Microsoft.WindowsCE.Forms
Imports System.Xml
Imports System.Data.SqlServerCe
Imports System.Runtime.InteropServices
Imports System.Net
Imports OpenNETCF.Net.NetworkInformation

Public Class Utils
    Private Shared count As Int16 = 1

    Public Function isValidGUID(ByVal str As String) As Boolean
        If str = "00000000-0000-0000-0000-000000000000" Then
            Return False
        End If
        Dim retbool As Boolean = Nothing
        Dim myguid As Guid = Nothing
        Try
            myguid = New Guid(str)
            retbool = True
        Catch ex As Exception
            retbool = False
        End Try
        Return retbool
    End Function

    Public Function zeroGuid() As Guid
        Return New Guid("00000000-0000-0000-0000-000000000000")
    End Function

    Public Function getempname(ByVal empid As String) As String
        Dim retstr As String = String.Empty
        Dim a As New SqlCeAdapter("\Program Files\SEUdock\SEUdock.sdf")
        Dim strSQL As String = "Select firstname + ' ' + lastname from employee where id = '" & empid & "'"
        retstr = a.QueryDatabaseForScalar(strSQL)
        Return retstr
    End Function

    Public Function getxmldata(ByVal itm As String) As String
        Dim strRet As String = String.Empty
        Dim locaconfig As String = "\Program Files\SEUdock\SEUdockConfig.xml"
        Dim xmldoc As New XmlDocument
        xmldoc.Load(locaconfig)
        '        Dim txt As XmlNode = xmldoc.SelectSingleNode("/SEUdockConfig/" & itm)
        '        strRet = txt.InnerText
        Dim txt As XmlNode = xmldoc.SelectSingleNode("/SEUdockConfig/DefaultLocation")
        With txt
            Select Case itm
                Case "Location_Name"
                    strRet = .SelectSingleNode("Location_Name").InnerText
                Case "Location_ID"
                    strRet = .SelectSingleNode("Location_ID").InnerText
                Case "Parent_Name"
                    strRet = .SelectSingleNode("Parent_Name").InnerText
                Case "Parent_ID"
                    strRet = .SelectSingleNode("Parent_ID").InnerText
                Case "Print_TimeStamp"
                    strRet = .SelectSingleNode("Print_TimeStamp").InnerText
                Case "Sync_Interval"
                    strRet = .SelectSingleNode("Sync_Interval").InnerText
            End Select
        End With
        If strRet = "00000000-0000-0000-0000-000000000000" Then
            'do something?
        End If
        Return strRet
    End Function

    Public Function getVendorName(ByVal vid As String, ByVal pid As String) As String
        Dim retstr As String = String.Empty
        Dim a As New SqlCeAdapter("\Program Files\SEUdock\SEUdock.sdf")
        Dim strSQL As String = "Select Name from Vendor where number = '" & vid & "' AND ParentCompanyID = '" & pid & "'"
        retstr = a.QueryDatabaseForScalar(strSQL)
        If vid > "" Then
            If retstr = "" Then retstr = "NOT LISTED"
        End If
        Return retstr
    End Function

    '*********************************************************************************************************
    Public Function getCustomerID(ByVal parentID As String, ByVal vnum As String) As String
        Dim cid As String = Nothing
        '*****************
        '*****************
        Dim conn As New SqlCeConnection("DataSource = \Program Files\SEUdock\SEUdock.sdf;")
        conn.Open()
        Dim command As SqlCeCommand = conn.CreateCommand
        command.CommandText = "SELECT Vendor.ID FROM Vendor WHERE ParentCompanyID = @parentID  AND (Vendor.Number = @vnum) "
        Dim param As SqlCeParameter
        param = New SqlCeParameter("@parentID", parentID)
        command.Parameters.Add(param)
        param = New SqlCeParameter("@vnum", vnum)
        command.Parameters.Add(param)

        Dim Vendor As DataTable = New DataTable
        command.Prepare()
        Dim sqlreader = command.ExecuteReader
        Vendor.Load(sqlreader)
        sqlreader.Close()

        Dim dt As DataTable = Vendor
        If dt.Rows.Count > 0 Then
            Dim row As DataRow = dt.Rows(0)
            cid = row.Item("ID").ToString
        Else
            cid = "00000000-0000-0000-0000-000000000000"

        End If
        Return cid
    End Function

    Public Function getstatusText(ByVal status As Integer, ByVal sched As Boolean) As String
        Dim retstr As String = String.Empty
        Select Case status
            Case 1, 9, 3
                If sched Then
                    retstr = "Scheduled"
                Else
                    retstr = "Checked In"
                End If
            Case 10
                retstr = "Assigned"
            Case 74
                retstr = "Not Printed"
            Case 76
                retstr = "Re-Print"
            Case 78
                retstr = "Complete"
        End Select
        Return retstr
    End Function

    Public Function carriernamebyid(ByVal cid As String) As String
        Dim a As New SqlCeAdapter("\Program Files\SEUdock\SEUdock.sdf")
        Dim strSQL As String = "Select name from carrier where id = '" & cid & "'"
        Dim retstr As String = a.QueryDatabaseForScalar(strSQL)
        retstr = IIf(retstr = "", "NOT LISTED", retstr)
        Return retstr
    End Function

    Public Function deptartmentnamebyid(ByVal did As String) As String
        Dim a As New SqlCeAdapter("\Program Files\SEUdock\SEUdock.sdf")
        Dim strSQL As String = "Select department from department where id = '" & did & "'"
        Dim retstr As String = a.QueryDatabaseForScalar(strSQL)
        Return retstr
    End Function

    ''create the SEUdockConfig.xml file
    Public Sub createConfig(ByVal LocationName As String, ByVal LocationID As String, ByVal LocationPrefix As String, ByVal ParentName As String, ByVal ParentID As String, ByVal SyncInterval As String, ByVal PrintTimeStamp As String)
        Dim xwriter As New XmlTextWriter("\Program Files\SEUdock\SEUdockConfig.xml", System.Text.Encoding.UTF8)
        xwriter.WriteStartDocument(True)
        xwriter.Formatting = Formatting.Indented
        xwriter.Indentation = 4
        xwriter.WriteStartElement("SEUdockConfig")

        xwriter.WriteStartElement("DefaultLocation")

        xwriter.WriteStartElement("Location_Name")
        xwriter.WriteString(LocationName)
        xwriter.WriteEndElement()

        xwriter.WriteStartElement("Location_ID")
        xwriter.WriteString(LocationID)
        xwriter.WriteEndElement()

        xwriter.WriteStartElement("Location_Prefix")
        xwriter.WriteString(LocationPrefix)
        xwriter.WriteEndElement()

        xwriter.WriteStartElement("Parent_Name")
        xwriter.WriteString(ParentName)
        xwriter.WriteEndElement()

        xwriter.WriteStartElement("Parent_ID")
        xwriter.WriteString(ParentID)
        xwriter.WriteEndElement()

        xwriter.WriteStartElement("Sync_Interval")
        xwriter.WriteString(SyncInterval)
        xwriter.WriteEndElement()

        xwriter.WriteStartElement("Print_TimeStamp")
        xwriter.WriteString(PrintTimeStamp)
        xwriter.WriteEndElement()

        xwriter.WriteEndElement() 'Default Location
        xwriter.WriteEndElement()
        xwriter.WriteEndDocument()
        xwriter.Close()

    End Sub

    <System.Runtime.InteropServices.DllImport("wininet.dll")> _
Public Shared Function InternetGetConnectedState(ByRef Description As Integer, ByVal ReservedValue As Integer) As Boolean
    End Function
    Public Shared Function CheckNet() As Boolean
        Return InternetGetConnectedState(0&, 0&) = 1
    End Function


    Public Shared Function Connected() As Boolean
        ' Call this class as follows: bool bResponse = Net.IsWebAccessible();
        Dim hwrRequest As HttpWebRequest
        Dim hwrResponse As HttpWebResponse
        Dim bConnected As Boolean = False
        Dim strurl As String = String.Empty
        Try
            If (count = 1 Or count = 4) Then
                strurl = "http://seu.div-log.com" + "/"
            ElseIf (count = 2 Or count = 5) Then
                strurl = "http://google.com" + "/"
            ElseIf (count = 3 Or count = 6) Then
                strurl = "http://yahoo.com" + "/"
            End If
            ' If count = 17 Then count = 1
            count = count + 1
            If count = 7 Then
                count = 1
            End If
            ' hwrRequest = DirectCast(WebRequest.Create(strUrl), HttpWebRequest)
            hwrRequest = WebRequest.Create(strurl)

            hwrRequest.Timeout = 10000
            'hwrRequest.Method = "GET"
            'hwrRequest.Proxy = System.Net.GlobalProxySelection.GetEmptyWebProxy()
            'hwrResponse = DirectCast(hwrRequest.GetResponse(), HttpWebResponse)
            hwrResponse = hwrRequest.GetResponse()
            If hwrResponse.StatusCode = HttpStatusCode.OK Then
                bConnected = True
            End If

            hwrRequest.Abort()

            hwrRequest = Nothing
            hwrResponse = Nothing
        Catch we As WebException
            bConnected = False
            hwrRequest = Nothing
            hwrResponse = Nothing
        Catch ex As Exception
            bConnected = False
            hwrRequest = Nothing
            hwrResponse = Nothing


        End Try

        Return bConnected
    End Function
    Dim vrecordcount As Integer = 0
    Dim seudockadapter As New SqlCeAdapter("\Program Files\SEUdock\SEUdock.sdf")
    Dim seuholdadapter As New SqlCeAdapter("\Program Files\SEUdock\SEUdockHold.sdf")
    Dim strSQL As String

    Public Sub CreateyesterdayDB()
        Dim dt As DataTable = New DataTable
        Dim wodal As New WorkOrderDAL
        If Not System.IO.File.Exists("\Program Files\SEUdock\SEUdockHold.sdf") Then
            CreateHoldDB()
        End If
        ' do any of these tables have records?
        vrecordcount = 0
        strSQL = "Select Count(ID) from WorkOrder"
        vrecordcount += seudockadapter.QueryDatabaseForScalar(strSQL)
        strSQL = "Select Count(ID) from Unloader"
        vrecordcount += seudockadapter.QueryDatabaseForScalar(strSQL)
        strSQL = "Select Count(ID) from TimePunche"
        vrecordcount += seudockadapter.QueryDatabaseForScalar(strSQL)
        strSQL = "Select Count(ID) from TimeInOut"
        vrecordcount += seudockadapter.QueryDatabaseForScalar(strSQL)
        If vrecordcount > 0 Then 'we need to save the records
            Dim woList As List(Of WorkOrder) = New List(Of WorkOrder)
            dt = New DataTable
            strSQL = "Select ID FROM WorkOrder"
            dt = seudockadapter.QueryDatabaseForTable(strSQL)
            For Each row As DataRow In dt.Rows
                Dim wo As WorkOrder
                wo = wodal.GetLoadByID(row.Item(0).ToString)
                woList.Add(wo)
            Next
            For Each existwo As WorkOrder In woList
                wodal.AddHoldWorkOrder(existwo)
            Next

            Dim tpdal As New TimePuncheDAL
            Dim tpList As List(Of TimePunche) = New List(Of TimePunche)
            strSQL = "Select ID FROM TimePunche"
            dt = seudockadapter.QueryDatabaseForTable(strSQL)
            For Each row As DataRow In dt.Rows
                Dim tp As New TimePunche
                tp = tpdal.getTimePuncheByID(row.Item(0).ToString)
                tpList.Add(tp)
            Next
            For Each vtp As TimePunche In tpList
                tpdal.insertHoldTimePunche(vtp)
            Next

            Dim tioList As List(Of TimeInOut) = New List(Of TimeInOut)
            strSQL = "Select ID FROM TimeInOut"
            dt = seudockadapter.QueryDatabaseForTable(strSQL)
            For Each row As DataRow In dt.Rows
                Dim tio As New TimeInOut
                tio = tpdal.getTimeInOutID(row.Item(0).ToString)
                tioList.Add(tio)
            Next
            For Each vtio As TimeInOut In tioList
                tpdal.insertHoldTIO(vtio)
            Next
        End If
    End Sub

    Public Sub getholdrecords()
        Dim dt As DataTable = New DataTable
        Dim wodal As New WorkOrderDAL
        vrecordcount = 0
        strSQL = "Select Count(ID) from WorkOrder"
        vrecordcount += seuholdadapter.QueryDatabaseForScalar(strSQL)
        'strSQL = "Select Count(ID) from Unloader"
        'vrecordcount += seuholdadapter.QueryDatabaseForScalar(strSQL)
        'strSQL = "Select Count(ID) from TimePunche"
        'vrecordcount += seuholdadapter.QueryDatabaseForScalar(strSQL)
        'strSQL = "Select Count(ID) from TimeInOut"
        'vrecordcount += seuholdadapter.QueryDatabaseForScalar(strSQL)
        If vrecordcount > 0 Then 'we need to save the records
            Dim woHoldList As List(Of WorkOrder) = New List(Of WorkOrder)
            strSQL = "Select ID FROM WorkOrder"
            dt = seuholdadapter.QueryDatabaseForTable(strSQL)
            For Each row As DataRow In dt.Rows
                Dim Holdwo As WorkOrder
                Holdwo = wodal.GetholdLoadByID(row.Item(0).ToString)
                woHoldList.Add(Holdwo)
            Next
            For Each holdwo As WorkOrder In woHoldList
                Dim existwo As WorkOrder = wodal.GetLoadByID(holdwo.ID.ToString)
                If Not existwo Is Nothing Then
                    wodal.UpdateWorkOrder(holdwo)
                Else
                    wodal.AddWorkOrder(holdwo)
                End If
            Next
            Dim tpdal As New TimePuncheDAL
            Dim tpHoldList As List(Of TimePunche) = New List(Of TimePunche)
            strSQL = "Select ID FROM TimePunche"
            dt = seuholdadapter.QueryDatabaseForTable(strSQL)
            For Each row As DataRow In dt.Rows
                Dim existtp As TimePunche = tpdal.getholdTimePuncheByID(row.Item(0).ToString)
                Dim holdtp As New TimePunche
                holdtp = tpdal.getholdTimePuncheByID(row.Item(0).ToString)
                tpHoldList.Add(holdtp)
            Next
            For Each holdtp As TimePunche In tpHoldList
                Dim existtp As TimePunche = tpdal.getholdTimePuncheByID(holdtp.ID.ToString)
                If Not existtp Is Nothing Then
                    tpdal.insertTimePunche(holdtp)
                Else
                    tpdal.updateTimePunche(holdtp)
                End If
            Next

            Dim tioList As List(Of TimeInOut) = New List(Of TimeInOut)
            strSQL = "Select ID FROM TimeInOut"
            dt = seuholdadapter.QueryDatabaseForTable(strSQL)
            For Each row As DataRow In dt.Rows
                Dim tio As New TimeInOut
                tio = tpdal.getholdTimeInOutID(row.Item(0).ToString)
                tioList.Add(tio)
            Next
            For Each vtio As TimeInOut In tioList
                Dim existtio As TimeInOut = tpdal.getTimeInOutID(vtio.ID.ToString)
                If Not existtio Is Nothing Then
                    tpdal.insertTIO(vtio)
                Else
                    tpdal.UpdateTIO(vtio)
                End If
            Next
        End If
        clearHoldRecords()
    End Sub

    Public Sub clearHoldRecords()
        Dim strSQL As String = String.Empty
        strSQL = "Delete from workorder"
        seuholdadapter.ExecuteNonQuery(strSQL)
        strSQL = "Delete from Unloader"
        seuholdadapter.ExecuteNonQuery(strSQL)
        strSQL = "Delete from TimePunche"
        seuholdadapter.ExecuteNonQuery(strSQL)
        strSQL = "Delete from TimeInOut"
        seuholdadapter.ExecuteNonQuery(strSQL)
    End Sub


    Protected Sub CreateHoldDB()
        Dim strSQL As String = String.Empty
        Dim connString As String = "Data Source='\Program Files\SEUdock\SEUdockHold.sdf'"
        Dim engine As New SqlCeEngine(connString)
        engine.CreateDatabase()
        Dim a2 As New SqlCeAdapter("\Program Files\SEUdock\SEUdockhold.sdf")
        strSQL = "CREATE TABLE [WorkOrder] ([Status] int NULL, [LogDate] datetime NULL, [LogNumber] bigint NULL, [LoadNumber] bigint NULL " & _
        ", [LocationID] uniqueidentifier NULL, [DepartmentID] uniqueidentifier NULL, [LoadTypeID] uniqueidentifier NULL " & _
        ", [CustomerID] uniqueidentifier NULL, [VendorNumber] nvarchar(50) NULL, [ReceiptNumber] bigint NULL, [PurchaseOrder] nvarchar(50) NULL " & _
        ", [Amount] float NULL, [IsCash] bit NULL, [LoadDescriptionID] uniqueidentifier NULL, [CarrierID] uniqueidentifier NULL " & _
        ", [TruckNumber] nvarchar(50) NULL, [TrailerNumber] nvarchar(50) NULL, [AppointmentTime] datetime NULL, [GateTime] datetime NULL " & _
        ", [DockTime] datetime NULL, [StartTime] datetime NULL, [CompTime] datetime NULL, [TTLTime] int NULL, [PalletsUnloaded] bigint NULL " & _
        ", [DoorNumber] nvarchar(64) NULL, [Pieces] bigint NULL, [Weight] bigint NULL, [Comments] ntext NULL " & _
        ", [Restacks] bigint NULL, [PalletsReceived] bigint NULL, [BadPallets] bigint NULL, [NumberOfItems] bigint NULL " & _
        ", [CheckNumber] nvarchar(50) NULL, [BOL] nvarchar(50) NULL, [ID] uniqueidentifier NOT NULL, [CreatedBy] nvarchar(128) NULL " & _
        ", [ccFee] float NULL, [rowguid] uniqueidentifier DEFAULT (  NewId  ( ) ) NOT NULL, [PaymentType] nvarchar(5) NULL " & _
        ", [SplitPaymentAmount] float NULL)"
        a2.ExecuteNonQuery(strSQL)
        strSQL = "CREATE TABLE [Unloader] ([LoadID] uniqueidentifier NULL, [EmployeeID] uniqueidentifier NULL " & _
        ", [ID] uniqueidentifier NOT NULL, [rowguid] uniqueidentifier DEFAULT (  NewId  ( ) ) NOT NULL)"
        a2.ExecuteNonQuery(strSQL)
        strSQL = "CREATE TABLE [TimePunche] ([EmployeeID] uniqueidentifier NULL, [DepartmentID] uniqueidentifier NULL " & _
        ", [DateWorked] datetime NULL, [IsClosed] bit NULL, [ID] uniqueidentifier NOT NULL, [LocationID] uniqueidentifier NULL " & _
        ", [rowguid] uniqueidentifier DEFAULT (  NewId  ( ) ) NOT NULL)"
        a2.ExecuteNonQuery(strSQL)
        strSQL = "CREATE TABLE [TimeInOut] ([ID] uniqueidentifier NOT NULL, [TimepuncheID] uniqueidentifier NULL, [TimeIn] datetime NULL " & _
        ", [TimeOut] datetime NULL, [IsHourly] bit NULL, [JobDescriptionID] uniqueidentifier NULL " & _
        ", [rowguid] uniqueidentifier DEFAULT (  NewId  ( ) ) NOT NULL)"
        a2.ExecuteNonQuery(strSQL)
        strSQL = "CREATE UNIQUE INDEX [MSmerge_index_965578478] ON [WorkOrder] ([rowguid] ASC)"
        a2.ExecuteNonQuery(strSQL)
        strSQL = "CREATE UNIQUE INDEX [MSmerge_index_837578022] ON [Unloader] ([rowguid] ASC)"
        a2.ExecuteNonQuery(strSQL)
        strSQL = "CREATE UNIQUE INDEX [MSmerge_index_805577908] ON [TimePunche] ([rowguid] ASC)"
        a2.ExecuteNonQuery(strSQL)
    End Sub

    'Public Sub getholdData()
    '    Dim a2 As New SqlCeAdapter("\Program Files\SEUdock\SEUdock.sdf")
    '    Dim a1 As New SqlCeAdapter("\Program Files\SEUdock\SEUdockhold.sdf")
    '    Dim strSQL As String

    '    strSQL = "Select * From workorder"
    '    Dim dt1 As DataTable = New DataTable
    '    dt1 = a1.QueryDatabaseForTable(strSQL)
    '    Dim wodal As WorkOrderDAL = New WorkOrderDAL
    '    Dim tpdal As TimePuncheDAL = New TimePuncheDAL
    '    If Not dt1 Is Nothing Then
    '        If dt1.Rows.Count > 0 Then
    '            For Each row As DataRow In dt1.Rows
    '                Dim wo As WorkOrder = wodal.GetholdLoadByID(row.Item("ID").ToString)
    '                wodal.AddWorkOrder(wo)
    '            Next
    '        End If
    '    End If
    '    strSQL = "Select * From TimePunche"
    '    dt1 = a1.QueryDatabaseForTable(strSQL)
    '    If Not dt1 Is Nothing Then
    '        If dt1.Rows.Count > 0 Then
    '            For Each row As DataRow In dt1.Rows
    '                Dim tp As TimePunche = tpdal.getholdTimePuncheByID(row.Item("ID").ToString)
    '                tpdal.insertTimePunche(tp)
    '            Next
    '        End If
    '    End If

    '    strSQL = "Select * From TimeInOut"
    '    dt1 = a1.QueryDatabaseForTable(strSQL)
    '    If Not dt1 Is Nothing Then
    '        If dt1.Rows.Count > 0 Then
    '            For Each row As DataRow In dt1.Rows
    '                Dim tio As TimeInOut = tpdal.getholdTimeInOutID(row.Item("ID").ToString)
    '                tpdal.insertTIO(tio)
    '            Next
    '        End If
    '    End If
    '    Dim filename As String = "\Program Files\SEUdock\SEUdockhold.sdf"
    '    If System.IO.File.Exists(filename) Then
    '        System.IO.File.Delete(filename)
    '    End If
    'End Sub


End Class

