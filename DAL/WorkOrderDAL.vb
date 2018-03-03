Imports System.Data
Imports System
Imports System.Net
Imports System.Threading
Imports System.ComponentModel
Imports SEUdock.Utils

Public Class WorkOrderDAL
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

    Public Sub IndexCarrierTable()
        Dim a As New SqlCeAdapter("\Program Files\SEUdock\SEUdock.sdf")
        Dim strSQL As String = String.Empty
        strSQL = "CREATE INDEX idxCarrierName ON Carrier (Name,ID)"
        a.ExecuteNonQuery(strSQL)
        strSQL = "CREATE INDEX idxCarrierName ON Carrier (Name,ID)"

    End Sub

    Public Function GetLoadByID(ByVal loadID As String) As WorkOrder
        Dim wo As WorkOrder
        Dim dt As DataTable = New DataTable
        Dim a As New SqlCeAdapter("\Program Files\SEUdock\SEUdock.sdf")
        Dim strSQL As String = String.Empty
        strSQL = "SELECT WO.Status, WO.LogDate, WO.LogNumber, WO.LoadNumber, WO.LocationID, WO.DepartmentID, " & _
        "Dept.Name AS Department, WO.LoadTypeID, " & _
        "LT.Name AS LoadType, WO.CustomerID, WO.VendorNumber, V.Name AS VendorName, " & _
        "WO.ReceiptNumber, WO.PurchaseOrder, WO.Amount, WO.IsCash,WO.PaymentType,WO.SplitPaymentAmount, WO.LoadDescriptionID, " & _
        "Des.Name AS LoadDescription, WO.CarrierID, Carrier.Name AS CarrierName, " & _
        "WO.TruckNumber, WO.TrailerNumber, WO.AppointmentTime, WO.GateTime, WO.DockTime, " & _
        "WO.StartTime, WO.CompTime, WO.TTLTime, WO.PalletsUnloaded, " & _
        "WO.DoorNumber, WO.Pieces, WO.Weight, WO.Comments, WO.Restacks, WO.PalletsReceived, WO.BadPallets, " & _
        "WO.NumberOfItems, WO.CheckNumber, WO.BOL, WO.ID, WO.CreatedBy " & _
        "FROM WorkOrder AS WO LEFT OUTER JOIN " & _
        "Vendor AS V ON WO.CustomerID = V.ID LEFT OUTER JOIN " & _
        "Department AS Dept ON WO.DepartmentID = Dept.ID LEFT OUTER JOIN " & _
        "Carrier ON WO.CarrierID = Carrier.ID LEFT OUTER JOIN " & _
        "Description AS Des ON WO.LoadDescriptionID = Des.ID LEFT OUTER JOIN " & _
        "LoadType AS LT ON WO.LoadTypeID = LT.ID " & _
        "WHERE (WO.ID = '" & loadID & "')"

        dt = a.QueryDatabaseForTable(strSQL)
        If Not dt Is Nothing Then

            If dt.Rows.Count > 0 Then
                Dim utl As New Utils
                wo = New WorkOrder
                wo.Status = IIf(IsDBNull(dt.Rows(0).Item("Status")), 0, dt.Rows(0).Item("Status"))
                wo.LogDate = IIf(IsDBNull(dt.Rows(0).Item("LogDate")), "", dt.Rows(0).Item("LogDate"))
                wo.LogNumber = IIf(IsDBNull(dt.Rows(0).Item("LogNumber")), "", dt.Rows(0).Item("LogNumber"))

                wo.LoadNumber = IIf(IsDBNull(dt.Rows(0).Item("LoadNumber")), "", dt.Rows(0).Item("LoadNumber"))

                wo.LocationID = IIf(IsDBNull(dt.Rows(0).Item("LocationID")), "", dt.Rows(0).Item("LocationID"))
                wo.DepartmentID = IIf(IsDBNull(dt.Rows(0).Item("DepartmentID")), "", dt.Rows(0).Item("DepartmentID"))
                wo.Department = IIf(IsDBNull(dt.Rows(0).Item("Department")), "", dt.Rows(0).Item("Department"))
                wo.LoadTypeID = IIf(IsDBNull(dt.Rows(0).Item("LoadTypeID")), "", dt.Rows(0).Item("LoadTypeID"))
                wo.LoadType = IIf(IsDBNull(dt.Rows(0).Item("LoadType")), "", dt.Rows(0).Item("LoadType"))
                wo.CustomerID = IIf(IsDBNull(dt.Rows(0).Item("CustomerID")), "", dt.Rows(0).Item("CustomerID"))
                wo.VendorNumber = IIf(IsDBNull(dt.Rows(0).Item("VendorNumber")), "", dt.Rows(0).Item("VendorNumber"))
                Dim pid As String = utl.getxmldata("Parent_ID")

                wo.VendorName = utl.getVendorName(wo.VendorNumber, pid)
                '            wo.VendorName = IIf(IsDBNull(dt.Rows(0).Item("VendorName")), "", dt.Rows(0).Item("VendorName"))
                Dim rn As Integer = IIf(IsDBNull(dt.Rows(0).Item("ReceiptNumber")), 0, dt.Rows(0).Item("ReceiptNumber"))
                wo.ReceiptNumber = IIf(IsNumeric(rn), dt.Rows(0).Item("ReceiptNumber"), Nothing)
                wo.PurchaseOrder = IIf(IsDBNull(dt.Rows(0).Item("PurchaseOrder")), "", dt.Rows(0).Item("PurchaseOrder"))
                wo.Amount = IIf(IsDBNull(dt.Rows(0).Item("Amount")), "", dt.Rows(0).Item("Amount"))
                If Not IsDBNull(dt.Rows(0).Item("SplitPaymentAmount")) Then
                    If IsNumeric(dt.Rows(0).Item("SplitPaymentAmount")) Then
                        wo.SplitPaymentAmount = dt.Rows(0).Item("SplitPaymentAmount")
                    Else
                        wo.SplitPaymentAmount = 0
                    End If
                Else
                    wo.SplitPaymentAmount = 0
                End If
                wo.IsCash = IIf(IsDBNull(dt.Rows(0).Item("IsCash")), "", dt.Rows(0).Item("IsCash"))
                wo.PaymentType = IIf(IsDBNull(dt.Rows(0).Item("PaymentType")), "", dt.Rows(0).Item("PaymentType"))
                wo.LoadDescriptionID = IIf(IsDBNull(dt.Rows(0).Item("LoadDescriptionID")), "", dt.Rows(0).Item("LoadDescriptionID"))
                wo.LoadDescription = IIf(IsDBNull(dt.Rows(0).Item("LoadDescription")), "", dt.Rows(0).Item("LoadDescription"))

                If IsDBNull(dt.Rows(0).Item("CarrierID")) Then
                    wo.CarrierID = Nothing
                Else
                    wo.CarrierID = dt.Rows(0).Item("CarrierID")
                End If

                wo.CarrierName = IIf(IsDBNull(dt.Rows(0).Item("CarrierName")), "", dt.Rows(0).Item("CarrierName"))
                Dim cid As String = wo.CarrierID.ToString
                If wo.CarrierID.ToString = "DA6E74EA-4335-43AD-993E-8C10F1081568" Then
                    wo.CarrierName = "NOT LISTED"
                Else
                    If utl.isValidGUID(wo.CarrierID.ToString) Then
                        Dim b As New SqlCeAdapter("\Program Files\SEUdock\SEUdock.sdf")
                        strSQL = "SELECT Name from Carrier where id = '" & wo.CarrierID.ToString & "'"
                        wo.CarrierName = utl.carriernamebyid(wo.CarrierID.ToString)
                    End If
                End If

                wo.TruckNumber = IIf(IsDBNull(dt.Rows(0).Item("TruckNumber")), "", dt.Rows(0).Item("TruckNumber"))
                wo.TrailerNumber = IIf(IsDBNull(dt.Rows(0).Item("TrailerNumber")), "", dt.Rows(0).Item("TrailerNumber"))
                wo.AppointmentTime = IIf(IsDBNull(dt.Rows(0).Item("AppointmentTime")), "", dt.Rows(0).Item("AppointmentTime"))
                wo.GateTime = IIf(IsDBNull(dt.Rows(0).Item("GateTime")), "", dt.Rows(0).Item("GateTime"))
                wo.DockTime = IIf(IsDBNull(dt.Rows(0).Item("DockTime")), "", dt.Rows(0).Item("DockTime"))
                wo.StartTime = IIf(IsDBNull(dt.Rows(0).Item("StartTime")), "1/1/1900", dt.Rows(0).Item("StartTime"))
                wo.CompTime = IIf(IsDBNull(dt.Rows(0).Item("CompTime")), "", dt.Rows(0).Item("CompTime"))
                wo.TTLTime = IIf(IsDBNull(dt.Rows(0).Item("TTLTime")), "", dt.Rows(0).Item("TTLTime"))
                wo.PalletsUnloaded = IIf(IsDBNull(dt.Rows(0).Item("PalletsUnloaded")), "", dt.Rows(0).Item("PalletsUnloaded"))
                wo.DoorNumber = IIf(IsDBNull(dt.Rows(0).Item("DoorNumber")), "", dt.Rows(0).Item("DoorNumber"))
                wo.Pieces = IIf(IsDBNull(dt.Rows(0).Item("Pieces")), "", dt.Rows(0).Item("Pieces"))
                wo.Weight = IIf(IsDBNull(dt.Rows(0).Item("Weight")), "", dt.Rows(0).Item("Weight"))
                wo.Comments = IIf(IsDBNull(dt.Rows(0).Item("Comments")), "", dt.Rows(0).Item("Comments"))
                wo.Restacks = IIf(IsDBNull(dt.Rows(0).Item("Restacks")), "", dt.Rows(0).Item("Restacks"))
                wo.PalletsReceived = IIf(IsDBNull(dt.Rows(0).Item("PalletsReceived")), "", dt.Rows(0).Item("PalletsReceived"))
                wo.BadPallets = IIf(IsDBNull(dt.Rows(0).Item("BadPallets")), "", dt.Rows(0).Item("BadPallets"))
                wo.NumberOfItems = IIf(IsDBNull(dt.Rows(0).Item("NumberOfItems")), "", dt.Rows(0).Item("NumberOfItems"))
                wo.CheckNumber = IIf(IsDBNull(dt.Rows(0).Item("CheckNumber")), "", dt.Rows(0).Item("CheckNumber"))
                wo.BOL = IIf(IsDBNull(dt.Rows(0).Item("BOL")), "", dt.Rows(0).Item("BOL"))
                wo.ID = IIf(IsDBNull(dt.Rows(0).Item("ID")), "", dt.Rows(0).Item("ID"))
                '            wo.isClosed = Not IsDBNull(dt.Rows(0).Item("userID"))
                '            wo.CreatedBy = IIf(IsDBNull(dt.Rows(0).Item("CreatedBy")), "", dt.Rows(0).Item("CreatedBy"))
                wo.Unloaders = getUnloaderList(wo.ID.ToString)
            Else
                'call user an idiot

            End If
        End If

        Return wo
    End Function

    Public Function getUnloaderList(ByVal loadid As String) As List(Of Unloader)
        Dim listofunloaders As List(Of Unloader) = New List(Of Unloader)
        Dim tul As Unloader = New Unloader
        Dim a As New SqlCeAdapter("\Program Files\SEUdock\SEUdock.sdf")
        Dim strSQL As String = "SELECT employee.id, Employee.FirstName + ' ' + Employee.LastName as Name, Employee.Login" & _
        " FROM Unloader INNER JOIN" & _
        " Employee ON Unloader.EmployeeID = Employee.ID INNER JOIN" & _
        " WorkOrder ON Unloader.LoadID = WorkOrder.ID" & _
        " WHERE (WorkOrder.Status < 200) AND (Unloader.LoadID = '" & loadid & "')"
        Try
            Dim dt As DataTable = a.QueryDatabaseForTable(strSQL)
            For Each row As DataRow In dt.Rows
                tul = New Unloader
                Dim strid As String = row.Item("ID").ToString
                Dim strname As String = row.Item("Name")
                tul.EmployeeID = row.Item("ID")
                tul.EmployeeName = row.Item("Name")
                tul.EmployeeLogin = row.Item("Login")
                listofunloaders.Add(tul)
            Next

        Catch ex As Exception

        End Try
        Return listofunloaders



    End Function

    Public Function AddWorkOrder(ByVal wo As WorkOrder, Optional ByVal writeComments As Boolean = False) As String
        Dim a As New SqlCeAdapter("\Program Files\SEUdock\SEUdock.sdf")
        Dim strSQL As String = String.Empty
        If wo.CarrierName = "NOT LISTED" Then
            wo.CarrierID = New Guid("DA6E74EA-4335-43AD-993E-8C10F1081568")
        End If
        Dim carstr As String = wo.CarrierID.ToString
        If wo.CreatedBy = "" Then wo.CreatedBy = "Hand-Held"
        strSQL = "INSERT INTO WorkOrder " & _
        "(Status, LogDate, LogNumber, LoadNumber, LocationID, DepartmentID, LoadTypeID,  CustomerID, VendorNumber, " & _
        "ReceiptNumber, PurchaseOrder, Amount, SplitPaymentAmount, IsCash, PaymentType, LoadDescriptionID, CarrierID, TruckNumber, TrailerNumber, " & _
        "AppointmentTime, GateTime, DockTime, StartTime, CompTime, TTLTime, PalletsUnloaded, DoorNumber, Pieces, Weight, Comments, " & _
        "Restacks, PalletsReceived, BadPallets, NumberOfItems, CheckNumber, BOL, ID, CreatedBy) VALUES " & _
        "('" & wo.Status & "','" & wo.LogDate & "','" & wo.LogNumber & "','" & wo.LoadNumber & "','" & wo.LocationID.ToString & "','" & wo.DepartmentID.ToString & "','" & wo.LoadTypeID.ToString & "','" & wo.CustomerID.ToString & "','" & wo.VendorNumber & "','" & _
        wo.ReceiptNumber & "','" & wo.PurchaseOrder & "'," & wo.Amount & "," & wo.SplitPaymentAmount & ",'" & wo.IsCash & "','" & wo.PaymentType & "','" & wo.LoadDescriptionID.ToString & "','" & wo.CarrierID.ToString & "','" & wo.TruckNumber & "','" & wo.TrailerNumber & "','" & _
        wo.AppointmentTime & "','" & wo.GateTime & "','" & wo.DockTime & "','" & wo.StartTime & "','" & wo.CompTime & "','" & wo.TTLTime & "','" & wo.PalletsUnloaded & "','" & wo.DoorNumber & "','" & wo.Pieces & "','" & wo.Weight & "','" & wo.Comments & "','" & _
        wo.Restacks & "','" & wo.PalletsReceived & "','" & wo.BadPallets & "','" & wo.NumberOfItems & "','" & wo.CheckNumber & "','" & wo.BOL & "','" & wo.ID.ToString & "','" & wo.CreatedBy & "')"
        Try
            a.ExecuteNonQuery(strSQL)
        Catch ex As Exception
            Return ex.Message
        End Try


        If Not wo.Unloaders Is Nothing Then
            If wo.Unloaders.Count > 0 Then
                Dim art As Integer = 0
                Dim gid As String = String.Empty
                For Each unl As Unloader In wo.Unloaders
                    strSQL = "SELECT COUNT(ID) FROM Unloader WHERE LoadID='" & wo.ID.ToString & "' and EmployeeID='" & unl.EmployeeID.ToString & "'"
                    Try
                        art = a.QueryDatabaseForScalar(strSQL)
                        If art = 0 Then
                            gid = Guid.NewGuid.ToString
                            a.ExecuteNonQuery("INSERT INTO Unloader (LoadID, EmployeeID, ID) VALUES ('" & wo.ID.ToString & "', '" & unl.EmployeeID.ToString & "', '" & gid & "')")
                        End If
                    Catch ex As Exception
                        Return "unloaders " & ex.Message
                    End Try
                Next
            End If
        End If
        Return "OK"
    End Function

    Public Function GetholdLoadByID(ByVal loadID As String) As WorkOrder
        Dim wo As WorkOrder = Nothing
        Dim dt As DataTable = New DataTable
        Dim a As New SqlCeAdapter("\Program Files\SEUdock\SEUdockhold.sdf")
        Dim strSQL As String = String.Empty
        strSQL = "SELECT WO.Status, WO.LogDate, WO.LogNumber, WO.LoadNumber, WO.LocationID, WO.DepartmentID, " & _
        "Dept.Name AS Department, WO.LoadTypeID, " & _
        "LT.Name AS LoadType, WO.CustomerID, WO.VendorNumber, V.Name AS VendorName, " & _
        "WO.ReceiptNumber, WO.PurchaseOrder, WO.Amount, WO.IsCash,WO.PaymentType,WO.SplitPaymentAmount, WO.LoadDescriptionID, " & _
        "Des.Name AS LoadDescription, WO.CarrierID, Carrier.Name AS CarrierName, " & _
        "WO.TruckNumber, WO.TrailerNumber, WO.AppointmentTime, WO.GateTime, WO.DockTime, " & _
        "WO.StartTime, WO.CompTime, WO.TTLTime, WO.PalletsUnloaded, " & _
        "WO.DoorNumber, WO.Pieces, WO.Weight, WO.Comments, WO.Restacks, WO.PalletsReceived, WO.BadPallets, " & _
        "WO.NumberOfItems, WO.CheckNumber, WO.BOL, WO.ID, WO.CreatedBy " & _
        "FROM WorkOrder AS WO LEFT OUTER JOIN " & _
        "Vendor AS V ON WO.CustomerID = V.ID LEFT OUTER JOIN " & _
        "Department AS Dept ON WO.DepartmentID = Dept.ID LEFT OUTER JOIN " & _
        "Carrier ON WO.CarrierID = Carrier.ID LEFT OUTER JOIN " & _
        "Description AS Des ON WO.LoadDescriptionID = Des.ID LEFT OUTER JOIN " & _
        "LoadType AS LT ON WO.LoadTypeID = LT.ID " & _
        "WHERE (WO.ID = '" & loadID & "')"

        dt = a.QueryDatabaseForTable(strSQL)
        If Not dt Is Nothing Then

            If dt.Rows.Count > 0 Then
                Dim utl As New Utils

                wo.Status = IIf(IsDBNull(dt.Rows(0).Item("Status")), "", dt.Rows(0).Item("Status"))
                wo.LogDate = IIf(IsDBNull(dt.Rows(0).Item("LogDate")), "", dt.Rows(0).Item("LogDate"))
                wo.LogNumber = IIf(IsDBNull(dt.Rows(0).Item("LogNumber")), "", dt.Rows(0).Item("LogNumber"))

                wo.LoadNumber = IIf(IsDBNull(dt.Rows(0).Item("LoadNumber")), "", dt.Rows(0).Item("LoadNumber"))

                wo.LocationID = IIf(IsDBNull(dt.Rows(0).Item("LocationID")), "", dt.Rows(0).Item("LocationID"))
                wo.DepartmentID = IIf(IsDBNull(dt.Rows(0).Item("DepartmentID")), "", dt.Rows(0).Item("DepartmentID"))
                wo.Department = IIf(IsDBNull(dt.Rows(0).Item("Department")), "", dt.Rows(0).Item("Department"))
                wo.LoadTypeID = IIf(IsDBNull(dt.Rows(0).Item("LoadTypeID")), "", dt.Rows(0).Item("LoadTypeID"))
                wo.LoadType = IIf(IsDBNull(dt.Rows(0).Item("LoadType")), "", dt.Rows(0).Item("LoadType"))
                wo.CustomerID = IIf(IsDBNull(dt.Rows(0).Item("CustomerID")), "", dt.Rows(0).Item("CustomerID"))
                wo.VendorNumber = IIf(IsDBNull(dt.Rows(0).Item("VendorNumber")), "", dt.Rows(0).Item("VendorNumber"))
                Dim pid As String = utl.getxmldata("Parent_ID")

                wo.VendorName = utl.getVendorName(wo.VendorNumber, pid)
                '            wo.VendorName = IIf(IsDBNull(dt.Rows(0).Item("VendorName")), "", dt.Rows(0).Item("VendorName"))
                Dim rn As Integer = IIf(IsDBNull(dt.Rows(0).Item("ReceiptNumber")), 0, dt.Rows(0).Item("ReceiptNumber"))
                wo.ReceiptNumber = IIf(IsNumeric(rn), dt.Rows(0).Item("ReceiptNumber"), Nothing)
                wo.PurchaseOrder = IIf(IsDBNull(dt.Rows(0).Item("PurchaseOrder")), "", dt.Rows(0).Item("PurchaseOrder"))
                wo.Amount = IIf(IsDBNull(dt.Rows(0).Item("Amount")), "", dt.Rows(0).Item("Amount"))
                If Not IsDBNull(dt.Rows(0).Item("SplitPaymentAmount")) Then
                    If IsNumeric(dt.Rows(0).Item("SplitPaymentAmount")) Then
                        wo.SplitPaymentAmount = dt.Rows(0).Item("SplitPaymentAmount")
                    Else
                        wo.SplitPaymentAmount = 0
                    End If
                Else
                    wo.SplitPaymentAmount = 0
                End If
                wo.IsCash = IIf(IsDBNull(dt.Rows(0).Item("IsCash")), "", dt.Rows(0).Item("IsCash"))
                wo.PaymentType = IIf(IsDBNull(dt.Rows(0).Item("PaymentType")), "", dt.Rows(0).Item("PaymentType"))
                wo.LoadDescriptionID = IIf(IsDBNull(dt.Rows(0).Item("LoadDescriptionID")), "", dt.Rows(0).Item("LoadDescriptionID"))
                wo.LoadDescription = IIf(IsDBNull(dt.Rows(0).Item("LoadDescription")), "", dt.Rows(0).Item("LoadDescription"))

                If IsDBNull(dt.Rows(0).Item("CarrierID")) Then
                    wo.CarrierID = Nothing
                Else
                    wo.CarrierID = dt.Rows(0).Item("CarrierID")
                End If
                wo.CarrierName = IIf(IsDBNull(dt.Rows(0).Item("CarrierName")), "", dt.Rows(0).Item("CarrierName"))
                Dim cid As String = wo.CarrierID.ToString
                If utl.isValidGUID(wo.CarrierID.ToString) Then
                    Dim b As New SqlCeAdapter("\Program Files\SEUdock\SEUdock.sdf")
                    strSQL = "SELECT Name from Carrier where id = '" & wo.CarrierID.ToString & "'"
                    wo.CarrierName = utl.carriernamebyid(wo.CarrierID.ToString)
                End If
                wo.TruckNumber = IIf(IsDBNull(dt.Rows(0).Item("TruckNumber")), "", dt.Rows(0).Item("TruckNumber"))
                wo.TrailerNumber = IIf(IsDBNull(dt.Rows(0).Item("TrailerNumber")), "", dt.Rows(0).Item("TrailerNumber"))
                wo.AppointmentTime = IIf(IsDBNull(dt.Rows(0).Item("AppointmentTime")), "", dt.Rows(0).Item("AppointmentTime"))
                wo.GateTime = IIf(IsDBNull(dt.Rows(0).Item("GateTime")), "", dt.Rows(0).Item("GateTime"))
                wo.DockTime = IIf(IsDBNull(dt.Rows(0).Item("DockTime")), "", dt.Rows(0).Item("DockTime"))
                wo.StartTime = IIf(IsDBNull(dt.Rows(0).Item("StartTime")), "1/1/1900", dt.Rows(0).Item("StartTime"))
                wo.CompTime = IIf(IsDBNull(dt.Rows(0).Item("CompTime")), "", dt.Rows(0).Item("CompTime"))
                wo.TTLTime = IIf(IsDBNull(dt.Rows(0).Item("TTLTime")), "", dt.Rows(0).Item("TTLTime"))
                wo.PalletsUnloaded = IIf(IsDBNull(dt.Rows(0).Item("PalletsUnloaded")), "", dt.Rows(0).Item("PalletsUnloaded"))
                wo.DoorNumber = IIf(IsDBNull(dt.Rows(0).Item("DoorNumber")), "", dt.Rows(0).Item("DoorNumber"))
                wo.Pieces = IIf(IsDBNull(dt.Rows(0).Item("Pieces")), "", dt.Rows(0).Item("Pieces"))
                wo.Weight = IIf(IsDBNull(dt.Rows(0).Item("Weight")), "", dt.Rows(0).Item("Weight"))
                wo.Comments = IIf(IsDBNull(dt.Rows(0).Item("Comments")), "", dt.Rows(0).Item("Comments"))
                wo.Restacks = IIf(IsDBNull(dt.Rows(0).Item("Restacks")), "", dt.Rows(0).Item("Restacks"))
                wo.PalletsReceived = IIf(IsDBNull(dt.Rows(0).Item("PalletsReceived")), "", dt.Rows(0).Item("PalletsReceived"))
                wo.BadPallets = IIf(IsDBNull(dt.Rows(0).Item("BadPallets")), "", dt.Rows(0).Item("BadPallets"))
                wo.NumberOfItems = IIf(IsDBNull(dt.Rows(0).Item("NumberOfItems")), "", dt.Rows(0).Item("NumberOfItems"))
                wo.CheckNumber = IIf(IsDBNull(dt.Rows(0).Item("CheckNumber")), "", dt.Rows(0).Item("CheckNumber"))
                wo.BOL = IIf(IsDBNull(dt.Rows(0).Item("BOL")), "", dt.Rows(0).Item("BOL"))
                wo.ID = IIf(IsDBNull(dt.Rows(0).Item("ID")), "", dt.Rows(0).Item("ID"))
                '            wo.isClosed = Not IsDBNull(dt.Rows(0).Item("userID"))
                '            wo.CreatedBy = IIf(IsDBNull(dt.Rows(0).Item("CreatedBy")), "", dt.Rows(0).Item("CreatedBy"))
                wo.Unloaders = getholdUnloaderList(wo.ID.ToString)
            Else
                'call user an idiot

            End If
        End If

        Return wo
    End Function

    Public Function getholdUnloaderList(ByVal loadid As String) As List(Of Unloader)
        Dim listofunloaders As List(Of Unloader) = New List(Of Unloader)
        Dim tul As Unloader = New Unloader
        Dim a As New SqlCeAdapter("\Program Files\SEUdock\SEUdockhold.sdf")
        Dim strSQL As String = "SELECT employee.id, Employee.FirstName + ' ' + Employee.LastName as Name, Employee.Login" & _
        " FROM Unloader INNER JOIN" & _
        " Employee ON Unloader.EmployeeID = Employee.ID INNER JOIN" & _
        " WorkOrder ON Unloader.LoadID = WorkOrder.ID" & _
        " WHERE (WorkOrder.Status < 200) AND (Unloader.LoadID = '" & loadid & "')"
        Try
            Dim dt As DataTable = a.QueryDatabaseForTable(strSQL)
            For Each row As DataRow In dt.Rows
                tul = New Unloader
                Dim strid As String = row.Item("ID").ToString
                Dim strname As String = row.Item("Name")
                tul.EmployeeID = row.Item("ID")
                tul.EmployeeName = row.Item("Name")
                tul.EmployeeLogin = row.Item("Login")
                listofunloaders.Add(tul)
            Next

        Catch ex As Exception

        End Try
        Return listofunloaders



    End Function

    Public Function AddHoldWorkOrder(ByVal wo As WorkOrder, Optional ByVal writeComments As Boolean = False) As String
        Dim a As New SqlCeAdapter("\Program Files\SEUdock\SEUdockHold.sdf")
        Dim strSQL As String = String.Empty
        If wo.CreatedBy = "" Then wo.CreatedBy = "Hand-Held"
        strSQL = "INSERT INTO WorkOrder " & _
        "(Status, LogDate, LogNumber, LoadNumber, LocationID, DepartmentID, LoadTypeID,  CustomerID, VendorNumber, " & _
        "ReceiptNumber, PurchaseOrder, Amount, SplitPaymentAmount, IsCash, PaymentType, LoadDescriptionID, CarrierID, TruckNumber, TrailerNumber, " & _
        "AppointmentTime, GateTime, DockTime, StartTime, CompTime, TTLTime, PalletsUnloaded, DoorNumber, Pieces, Weight, Comments, " & _
        "Restacks, PalletsReceived, BadPallets, NumberOfItems, CheckNumber, BOL, ID, CreatedBy) VALUES " & _
        "('" & wo.Status & "','" & wo.LogDate & "','" & wo.LogNumber & "','" & wo.LoadNumber & "','" & wo.LocationID.ToString & "','" & wo.DepartmentID.ToString & "','" & wo.LoadTypeID.ToString & "','" & wo.CustomerID.ToString & "','" & wo.VendorNumber & "','" & _
        wo.ReceiptNumber & "','" & wo.PurchaseOrder & "'," & wo.Amount & "," & wo.SplitPaymentAmount & ",'" & wo.IsCash & "','" & wo.PaymentType & "','" & wo.LoadDescriptionID.ToString & "','" & wo.CarrierID.ToString & "','" & wo.TruckNumber & "','" & wo.TrailerNumber & "','" & _
        wo.AppointmentTime & "','" & wo.GateTime & "','" & wo.DockTime & "','" & wo.StartTime & "','" & wo.CompTime & "','" & wo.TTLTime & "','" & wo.PalletsUnloaded & "','" & wo.DoorNumber & "','" & wo.Pieces & "','" & wo.Weight & "','" & wo.Comments & "','" & _
        wo.Restacks & "','" & wo.PalletsReceived & "','" & wo.BadPallets & "','" & wo.NumberOfItems & "','" & wo.CheckNumber & "','" & wo.BOL & "','" & wo.ID.ToString & "','" & wo.CreatedBy & "')"
        Try
            a.ExecuteNonQuery(strSQL)
        Catch ex As Exception
            Return ex.Message
        End Try


        If Not wo.Unloaders Is Nothing Then
            If wo.Unloaders.Count > 0 Then
                Dim art As Integer = 0
                Dim gid As String = String.Empty
                For Each unl As Unloader In wo.Unloaders
                    strSQL = "SELECT COUNT(ID) FROM Unloader WHERE LoadID='" & wo.ID.ToString & "' and EmployeeID='" & unl.EmployeeID.ToString & "'"
                    Try
                        art = a.QueryDatabaseForScalar(strSQL)
                        If art = 0 Then
                            gid = Guid.NewGuid.ToString
                            a.ExecuteNonQuery("INSERT INTO Unloader (LoadID, EmployeeID, ID) VALUES ('" & wo.ID.ToString & "', '" & unl.EmployeeID.ToString & "', '" & gid & "')")
                        End If
                    Catch ex As Exception
                        Return "unloaders " & ex.Message
                    End Try
                Next
            End If
        End If
        Return "OK"
    End Function

    Public Function UpdateWorkOrder(ByVal wo As WorkOrder) As String
        Dim retstr As String = String.Empty
        If wo.CarrierName = "NOT LISTED" Then
            wo.CarrierID = New Guid("DA6E74EA-4335-43AD-993E-8C10F1081568")
        End If

        ' changing the date properties from '1/1/1900' to '12:00 AM' so that it is like the ADDworkorder method
        If wo.CompTime = "1/1/1900" Then wo.CompTime = "12:00 AM"
        If wo.StartTime = "1/1/1900" Then wo.StartTime = "12:00 AM"
        Dim a As New SqlCeAdapter("\Program Files\SEUdock\SEUdock.sdf")
        Dim strSQL As String = String.Empty
        strSQL = "UPDATE WorkOrder Set Status=" & wo.Status & _
        ", LogDate='" & wo.LogDate & _
        "', LogNumber=" & wo.LogNumber & _
        ", LoadNumber=" & wo.LoadNumber & _
        ", LocationID='" & wo.LocationID.ToString & _
        "', DepartmentID='" & wo.DepartmentID.ToString & _
        "', LoadTypeID='" & wo.LoadTypeID.ToString & _
        "', CustomerID='" & wo.CustomerID.ToString & _
        "', VendorNumber='" & wo.VendorNumber & _
        "', ReceiptNumber=" & wo.ReceiptNumber & _
        ", PurchaseOrder='" & wo.PurchaseOrder & _
        "', Amount=" & wo.Amount & _
        ", SplitPaymentAmount=" & wo.SplitPaymentAmount & _
        ", IsCash='" & wo.IsCash & _
        "', PaymentType='" & wo.PaymentType & _
        "', LoadDescriptionID='" & wo.LoadDescriptionID.ToString & _
        "', CarrierID='" & wo.CarrierID.ToString & _
        "', TruckNumber='" & wo.TruckNumber & _
        "', TrailerNumber='" & wo.TrailerNumber & _
        "', AppointmentTime='" & wo.AppointmentTime & _
        "', GateTime='" & wo.GateTime & _
        "', DockTime='" & wo.DockTime & _
        "', StartTime='" & wo.StartTime & _
        "', CompTime='" & wo.CompTime & _
        "', PalletsUnloaded=" & wo.PalletsUnloaded & _
        ", DoorNumber='" & wo.DoorNumber & _
        "', Pieces=" & wo.Pieces & _
        ", Weight=" & wo.Weight & _
        ", Comments='" & wo.Comments & _
        "', Restacks=" & wo.Restacks & _
        ", PalletsReceived=" & wo.PalletsReceived & _
        ", BadPallets=" & wo.BadPallets & _
        ", NumberOfItems=" & wo.NumberOfItems & _
        ", CheckNumber='" & wo.CheckNumber & _
        "', BOL='" & wo.BOL & _
        "', CreatedBy='" & wo.CreatedBy & "' WHERE ID ='" & wo.ID.ToString & "'"
        Try
            a.ExecuteNonQuery(strSQL)
        Catch ex As Exception
            Return "workorder" & ex.Message
        End Try
        If Not wo.Unloaders Is Nothing Then
            If wo.Unloaders.Count > 0 Then
                Dim art As Integer = 0 'already there
                Dim gid As String = String.Empty 'guid tostring
                'get
                'Dim dt As DataTable = a.QueryDatabaseForTable("Select LoadID,EmployeeID from unloaders")
                'Dim unl As List(Of Unloader) = wo.Unloaders
                'wo.Unloaders.Contains(

                strSQL = "delete from Unloader where loadID='" & wo.ID.ToString & "'"
                Try
                    a.ExecuteNonQuery(strSQL)
                Catch ex As Exception
                End Try
                For Each unl As Unloader In wo.Unloaders
                    strSQL = "SELECT COUNT(ID) FROM Unloader WHERE LoadID='" & wo.ID.ToString & "' and EmployeeID='" & unl.EmployeeID.ToString & "'"
                    Try
                        art = a.QueryDatabaseForScalar(strSQL)
                        If art = 0 Then
                            gid = Guid.NewGuid.ToString
                            a.ExecuteNonQuery("INSERT INTO Unloader (LoadID, EmployeeID, ID) VALUES ('" & wo.ID.ToString & "', '" & unl.EmployeeID.ToString & "', '" & gid & "')")
                        Else
                            strSQL = "DELETE FROM Unloader WHERE LoadID='" & wo.ID.ToString & "' and EmployeeID='" & unl.EmployeeID.ToString & "'"
                            a.ExecuteNonQuery(strSQL)
                        End If
                    Catch ex As Exception
                        Return "unloaders " & ex.Message
                    End Try
                Next
            End If
        End If
        Return retstr
    End Function

    Public Function UpdateHoldWorkOrder(ByVal wo As WorkOrder) As String
        Dim retstr As String = String.Empty
        ' changing the date properties from '1/1/1900' to '12:00 AM' so that it is like the ADDworkorder method
        If wo.CompTime = "1/1/1900" Then wo.CompTime = "12:00 AM"
        If wo.StartTime = "1/1/1900" Then wo.StartTime = "12:00 AM"
        Dim a As New SqlCeAdapter("\Program Files\SEUdock\SEUdockHold.sdf")
        Dim strSQL As String = String.Empty
        strSQL = "UPDATE WorkOrder Set Status=" & wo.Status & _
        ", LogDate='" & wo.LogDate & _
        "', LogNumber=" & wo.LogNumber & _
        ", LoadNumber=" & wo.LoadNumber & _
        ", LocationID='" & wo.LocationID.ToString & _
        "', DepartmentID='" & wo.DepartmentID.ToString & _
        "', LoadTypeID='" & wo.LoadTypeID.ToString & _
        "', CustomerID='" & wo.CustomerID.ToString & _
        "', VendorNumber='" & wo.VendorNumber & _
        "', ReceiptNumber=" & wo.ReceiptNumber & _
        ", PurchaseOrder='" & wo.PurchaseOrder & _
        "', Amount=" & wo.Amount & _
        ", SplitPaymentAmount=" & wo.SplitPaymentAmount & _
        ", IsCash='" & wo.IsCash & _
        "', PaymentType='" & wo.PaymentType & _
        "', LoadDescriptionID='" & wo.LoadDescriptionID.ToString & _
        "', CarrierID='" & wo.CarrierID.ToString & _
        "', TruckNumber='" & wo.TruckNumber & _
        "', TrailerNumber='" & wo.TrailerNumber & _
        "', AppointmentTime='" & wo.AppointmentTime & _
        "', GateTime='" & wo.GateTime & _
        "', DockTime='" & wo.DockTime & _
        "', StartTime='" & wo.StartTime & _
        "', CompTime='" & wo.CompTime & _
        "', PalletsUnloaded=" & wo.PalletsUnloaded & _
        ", DoorNumber='" & wo.DoorNumber & _
        "', Pieces=" & wo.Pieces & _
        ", Weight=" & wo.Weight & _
        ", Comments='" & wo.Comments & _
        "', Restacks=" & wo.Restacks & _
        ", PalletsReceived=" & wo.PalletsReceived & _
        ", BadPallets=" & wo.BadPallets & _
        ", NumberOfItems=" & wo.NumberOfItems & _
        ", CheckNumber='" & wo.CheckNumber & _
        "', BOL='" & wo.BOL & _
        "', CreatedBy='" & wo.CreatedBy & "' WHERE ID ='" & wo.ID.ToString & "'"
        Try
            a.ExecuteNonQuery(strSQL)
        Catch ex As Exception
            Return "workorder" & ex.Message
        End Try
        If Not wo.Unloaders Is Nothing Then
            If wo.Unloaders.Count > 0 Then
                Dim art As Integer = 0
                Dim gid As String = String.Empty
                strSQL = "delete from Unloader where loadID='" & wo.ID.ToString & "'"
                Try
                    a.ExecuteNonQuery(strSQL)
                Catch ex As Exception
                End Try
                For Each unl As Unloader In wo.Unloaders
                    strSQL = "SELECT COUNT(ID) FROM Unloader WHERE LoadID='" & wo.ID.ToString & "' and EmployeeID='" & unl.EmployeeID.ToString & "'"
                    Try
                        art = a.QueryDatabaseForScalar(strSQL)
                        If art = 0 Then
                            gid = Guid.NewGuid.ToString
                            a.ExecuteNonQuery("INSERT INTO Unloader (LoadID, EmployeeID, ID) VALUES ('" & wo.ID.ToString & "', '" & unl.EmployeeID.ToString & "', '" & gid & "')")
                        End If
                    Catch ex As Exception
                        Return "unloaders " & ex.Message
                    End Try
                Next
            End If
        End If
        Return retstr
    End Function
End Class
''Public Function GetActiveWorkOrders(ByVal locaID As Guid, ByVal HideCompleted As Boolean) As DataTable
''    Dim todaysDate As DateTime = Date.Now.ToShortDateString
''    Dim endDate As DateTime = DateAdd(DateInterval.Day, 1, todaysDate)
''    Dim ldal As New locaDAL
''    Dim bdOffSet As Integer = ldal.getLocaBDOffset(locaID)
''    '        Dim getDate As DateTime = DateAdd(DateInterval.Day, -1, todaysDate)
''    Dim sql As String = "SELECT W.ID, W.LogDate, W.DoorNumber AS 'DoorNum', V.Name AS Vendor, W.PurchaseOrder, C.Name AS Carrier, W.TruckNumber, W.TrailerNumber,  " & _
''        "W.AppointmentTime, W.StartTime, W.CompTime, W.DockTime, D.Name AS Department, LT.Name AS LoadType " & _
''        "FROM ParentCompany AS PC RIGHT OUTER JOIN " & _
''        "Location AS L ON PC.ID = L.ParentCompanyID RIGHT OUTER JOIN " & _
''        "WorkOrder AS W LEFT OUTER JOIN " & _
''        "LoadType AS LT ON W.LoadTypeID = LT.ID LEFT OUTER JOIN " & _
''        "Carrier AS C ON W.CarrierID = C.ID LEFT OUTER JOIN " & _
''        "Vendor AS V ON W.CustomerID = V.ID ON L.ID = W.LocationID LEFT OUTER JOIN " & _
''        "Department AS D ON W.DepartmentID = D.ID " & _
''        "WHERE     (W.LocationID = @locaID) "
''    If HideCompleted Then
''        sql &= "AND DATEPART(year, w.Comptime) < DATEPART(year, GETDATE()) "
''    End If
''    If bdOffSet <> 0 Then
''        todaysDate = DateAdd(DateInterval.Hour, bdOffSet, todaysDate)
''        sql &= "AND ((W.DockTime > @dt) and (W.DockTime < @edt)) "
''    Else
''        sql &= "AND (CAST(CONVERT(char(10), W.LogDate, 101) AS DATETIME) = @dt) "
''    End If
''    sql &= "ORDER BY W.CompTime, W.DockTime DESC, W.AppointmentTime "
''    'sql &= "ORDER BY W.DockTime DESC,D.Name, W.DoorNumber, W.AppointmentTime "

''    Dim adapter As New SqlDataAdapter(sql, ConfigurationManager.ConnectionStrings("rtdsConnectionString").ConnectionString)
''    Dim param As New SqlParameter("locaID", locaID)
''    Dim param2 As New SqlParameter("dt", todaysDate)
''    adapter.SelectCommand.Parameters.Add(param)
''    adapter.SelectCommand.Parameters.Add(param2)
''    If bdOffSet <> 0 Then
''        Dim param3 As New SqlParameter("edt", DateAdd(DateInterval.Hour, bdOffSet, endDate))
''        adapter.SelectCommand.Parameters.Add(param3)
''    End If

''    Dim dt As DataTable = New DataTable()
''    adapter.Fill(dt)
''    '        go get thu records
''    Dim clm As DataColumn = New DataColumn()
''    clm.ColumnName = "Unloaders"
''    dt.Columns.Add(clm)

''    If dt.Rows.Count > 1 Then
''        For Each rw As DataRow In dt.Rows
''            adapter.SelectCommand.CommandText = "SELECT Employee.FirstName, Employee.LastName " & _
''                "FROM Employee INNER JOIN " & _
''                "Unloader ON Employee.ID = Unloader.EmployeeID INNER JOIN " & _
''                "WorkOrder ON Unloader.LoadID = WorkOrder.ID " & _
''                "WHERE(WorkOrder.ID = @woid) "
''            adapter.SelectCommand.Parameters.Clear()
''            Dim xparam As SqlParameter = New SqlParameter("woid", dt.rows.Item("ID"))
''            adapter.SelectCommand.Parameters.Add(xparam)
''            Dim dtunloaders As DataTable = New DataTable()
''            adapter.Fill(dtunloaders)
''            Dim uls As String = String.Empty
''            For Each urw As DataRow In dtunloaders.Rows
''                uls &= udt.rows.Item("FirstName") & " " & udt.rows.Item("LastName") & ", "
''            Next
''            If uls.Length > 3 Then uls = Left(uls, Len(uls) - 2)
''            dt.rows.Item("Unloaders") = uls
''        Next
''    End If
''    'put records in list
''    'iterate thru list and add employees

''    Return dt
''End Function

'Public Function GetActiveWorkOrders2(ByVal locaID As Guid, ByVal HideCompleted As Boolean) As DataTable
'    Dim ldal As New locaDAL
'    Dim loca As Location = ldal.getLocationByID(locaID)
'    Dim locationName As String = loca.Name
'    Dim bdOffSet As Integer = ldal.getLocaBDOffset(locaID)
'    Dim StartDate As DateTime = Date.Now.ToShortDateString
'    Dim EndDate As DateTime = DateAdd(DateInterval.Day, 1, StartDate)
'    Dim strSQL As String = "SELECT dbo.WorkOrder.DoorNumber, dbo.Vendor.Name AS Vendor, dbo.WorkOrder.PurchaseOrder AS PO, dbo.Carrier.Name AS Carrier,  " & _
'        "dbo.WorkOrder.TrailerNumber AS Trailer, CASE WHEN CONVERT(VARCHAR(19), dbo.WorkOrder.AppointmentTime, 121)  " & _
'        "= '1900-01-01 00:00:00' THEN '- - -' ELSE CONVERT(VARCHAR(5), dbo.WorkOrder.AppointmentTime, 8) END AS Appt,  " & _
'        "CASE WHEN CONVERT(VARCHAR(19), dbo.WorkOrder.DockTime, 121) = '1900-01-01 00:00:00' THEN '- - -' ELSE CONVERT(VARCHAR(5),  " & _
'        "dbo.WorkOrder.DockTime, 8) END AS DockTime, CASE WHEN CONVERT(VARCHAR(19), dbo.WorkOrder.StartTime, 121)  " & _
'        "= '1900-01-01 00:00:00' THEN '- - -' ELSE CONVERT(VARCHAR(5), dbo.WorkOrder.StartTime, 8) END AS StartTime, CASE WHEN CONVERT(VARCHAR(19),  " & _
'        "dbo.WorkOrder.CompTime, 121) = '1900-01-01 00:00:00' THEN '- - -' ELSE CONVERT(VARCHAR(5), dbo.WorkOrder.CompTime, 8) END AS Finish,  " & _
'        "dbo.Department.Name AS Department, dbo.WorkOrder.ID, CASE WHEN dbo.ufn_UnloaderList(dbo.WorkOrder.ID) IS NULL  " & _
'        "THEN '- - -' ELSE dbo.ufn_UnloaderList(dbo.WorkOrder.ID) END AS Unloaders, dbo.LoadType.Name " & _
'        "FROM dbo.WorkOrder INNER JOIN " & _
'        "dbo.LoadType ON dbo.WorkOrder.LoadTypeID = dbo.LoadType.ID LEFT OUTER JOIN " & _
'        "dbo.Carrier ON dbo.WorkOrder.CarrierID = dbo.Carrier.ID LEFT OUTER JOIN " & _
'        "dbo.Vendor ON dbo.WorkOrder.CustomerID = dbo.Vendor.ID LEFT OUTER JOIN " & _
'        "dbo.Department ON dbo.WorkOrder.DepartmentID = dbo.Department.ID LEFT OUTER JOIN " & _
'        "dbo.Location ON dbo.WorkOrder.LocationID = dbo.Location.ID " & _
'        "WHERE (dbo.Location.Name = @location) "
'    If HideCompleted Then
'        strSQL &= "AND DATEPART(year, dbo.WorkOrder.Comptime) < DATEPART(year, GETDATE()) "
'    End If
'    If bdOffSet <> 0 Then
'        StartDate = DateAdd(DateInterval.Hour, bdOffSet, StartDate)
'        EndDate = DateAdd(DateInterval.Hour, bdOffSet, EndDate)
'        strSQL &= "CASE WHEN W.AppointmentTime > '1900-01-01 00:00:00.000' "
'        strSQL &= "THEN ((W.AppointmentTime >= @StartDate) and (W.AppointmentTime < @EndDate)) "
'        strSQL &= "ELSE ((W.DockTime >= @StartDate) and (W.DockTime < @EndDate)) END "
'    Else
'        strSQL &= "AND (dbo.WorkOrder.LogDate >= @StartDate) AND (dbo.WorkOrder.LogDate < @EndDate) "
'    End If
'    strSQL &= "ORDER BY CASE WHEN CONVERT(VARCHAR(19), dbo.WorkOrder.CompTime, 121) = '1900-01-01 00:00:00' THEN '1' ELSE '0' END DESC, " & _
'        "CASE WHEN dbo.ufn_UnloaderList(dbo.WorkOrder.ID) IS NULL THEN '1' ELSE '0' END, Department, dbo.WorkOrder.DoorNumber, DockTime DESC, Appt "
'    Dim adapter As SqlDataAdapter = New SqlDataAdapter(strSQL, ConfigurationManager.ConnectionStrings("rtdsConnectionString").ConnectionString)
'    Dim param As New SqlParameter("location", locationName)
'    Dim param2 As New SqlParameter("StartDate", StartDate)
'    Dim param3 As New SqlParameter("EndDate", DateAdd(DateInterval.Hour, bdOffSet, EndDate))
'    adapter.SelectCommand.Parameters.Add(param)
'    adapter.SelectCommand.Parameters.Add(param2)
'    adapter.SelectCommand.Parameters.Add(param3)
'    adapter.SelectCommand.CommandTimeout = 120
'    Dim dt As DataTable = New DataTable()
'    adapter.Fill(dt)
'    Return dt
'End Function