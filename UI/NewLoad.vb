Imports System.Data
Imports System.Threading
Imports System
Imports System.Reflection
Imports System.Xml
Imports System.Collections
Imports System.ComponentModel
Imports System.Data.SqlServerCe
Imports Microsoft.WindowsCE.Forms
Imports SEUdock.Utils
Imports Microsoft.VisualBasic
Imports Microsoft.WindowsCE
Public Class NewLoad
    Public utl As New Utils

    '*********************************************************************************************************
    '*********************************************************************************************************
#Region "Class Variables / Properties / Constants"
    '*********************************************************************************************************
    Public fromAddUnloaders = False
    Private cwo As WorkOrder
    Private emptbl As DataTable
    Private ischanged As Boolean = False
    '*********************************************************************************************************
    'for accessing/setting dropdown box settings
    Public Const CB_SHOWDROPDOWN As Integer = &H14F
    '    Public Const CB_GETDROPPEDSTATE = "&H14f" '&H157
    Private _ErrorText As String
    Private _CommandExecuted As Boolean
    Private m_fOkToUpadateAutoComplete As Boolean
    Private m_sLastSearchedFor As String = ""

    '*********************************************************************************************************
    <Flags()> _
    Enum LoadStatus     'track load status via bitwise operations
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
    '*********************************************************************************************************

    '*********************************************************************************************************
    Public Property curwo() As WorkOrder
        Get
            Return cwo
        End Get
        Set(ByVal value As WorkOrder)
            cwo = value
        End Set
    End Property
    Public Property curemptbl() As DataTable
        Get
            Return emptbl
        End Get
        Set(ByVal value As DataTable)
            emptbl = value
        End Set
    End Property
    '*********************************************************************************************************
#End Region 'Class Variables / Properties / Constants
    '*********************************************************************************************************
    '*********************************************************************************************************
    'this will open a combobox
    Public Shared Function SetDroppedDown(ByVal comboBox As ComboBox) As Boolean
        Dim comboBoxDroppedMsg As Message = Message.Create(comboBox.Handle, CB_SHOWDROPDOWN, CType(1, IntPtr), IntPtr.Zero)
        MessageWindow.SendMessage(comboBoxDroppedMsg)
        Return comboBoxDroppedMsg.Result <> IntPtr.Zero
    End Function

    '*********************************************************************************************************
    '*********************************************************************************************************
#Region "Page Events"
    Private Sub NewLoad_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Cursor.Current = Cursors.WaitCursor
        txtBadPallets.Enabled = False
        txtRestacks.Enabled = False
        btnAddUnloader.Enabled = False
        btnDelUnloader.Enabled = False
        loadDepartments()
        loadDescriptions()
        '  loadAssignedUnloaders()
        loadLoadTypes()
        If curwo Is Nothing Then
            curwo = New WorkOrder
            curwo.ID = Guid.NewGuid
            curwo.Status = LoadStatus.Undefined
            Dim loadnum As String = String.Empty
            loadnum = Format(Date.Now, "MMddHHmmss")
            curwo.LoadNumber = loadnum
        Else
            Dim idstr As String = curwo.ID.ToString
            populateForm(curwo)
        End If
        Cursor.Current = Cursors.Default

    End Sub

    Private Sub NewLoad_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Activated

        '    If CommentCarrier Then
        '        populateForm(curwo)
        '    End If

        '    loadtwo = False
        '    If Not curwo.Unloaders Is Nothing Then
        '        TabControl1.SelectedIndex = 1
        '    End If

        'ElseIf Not loadone And Not loadtwo Then
        '    populateForm(curwo)
    End Sub

    Private Sub NewLoad_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles MyBase.Paint

    End Sub

    Private Sub NewLoad_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.GotFocus
        btnAddUnloader.Enabled = txtDoor.Text > ""
        btnDelUnloader.Enabled = txtDoor.Text > ""
        loadAssignedUnloaders()
        If fromAddUnloaders Then TabControl1.SelectedIndex = 1
    End Sub
#End Region 'Page Events
    '*********************************************************************************************************
    '*********************************************************************************************************

    '*********************************************************************************************************
    Private Sub populateForm(ByVal wo As WorkOrder)
        If wo.Status = 74 Then
            '            btnPrint.Visible = wo.Status = 74
            '            btnClearPage42.Visible = False
            btnClearPage4.Visible = False
        End If
        If wo.CarrierName > "" Then
            Dim a As New SqlCeAdapter("\Program Files\SEUdock\SEUdock.sdf")
            Dim strSQL As String = "Select ID, Name from Carrier where id='" & wo.CarrierID.ToString & "'"
            Dim ctbl As DataTable = a.QueryDatabaseForTable(strSQL)
            cbCarrier.DataSource = ctbl
            cbCarrier.DisplayMember = "Name"
            cbCarrier.ValueMember = "ID"
        End If
        txtDoor.Text = curwo.DoorNumber
        If curwo.LoadNumber < 1 Then
            txtLoadNumber.Text = ""
        Else
            txtLoadNumber.Text = curwo.LoadNumber
        End If

        cbCarrier.Text = curwo.CarrierName
        cbCarrier.SelectedValue = curwo.CarrierID
        txtTruckNumber.Text = curwo.TruckNumber
        txtTrailerNumber.Text = curwo.TrailerNumber
        txtPurchaseOrder.Text = curwo.PurchaseOrder
        cbDepartment.SelectedText = curwo.Department
        cbDepartment.SelectedValue = curwo.DepartmentID
        txtVendorNumber.Text = curwo.VendorNumber
        txtVendor.Text = curwo.VendorName
        txtVendor.ReadOnly = True
        cbLoadType.SelectedValue = curwo.LoadTypeID
        If curwo.Pieces < 1 Then
            numPieces.Text = ""
        Else
            numPieces.Text = curwo.Pieces
        End If
        If curwo.PalletsReceived < 1 Then
            numPallets.Text = ""
        Else
            numPallets.Text = curwo.PalletsReceived
        End If
        cbLoadDescription.SelectedValue = curwo.LoadDescriptionID

        If curwo.AppointmentTime < Date.Now Then
            TimePickerAppointmentTime.Text = ""
        End If
        If curwo.PalletsUnloaded < 0 Then
            txtPalletsUnloaded.Text = ""
        Else
            txtPalletsUnloaded.Text = curwo.PalletsUnloaded.ToString
        End If
        If curwo.BadPallets < 0 Then
            txtBadPallets.Text = ""
        Else
            txtBadPallets.Text = curwo.BadPallets.ToString
        End If
        If curwo.Restacks < 0 Then
            txtRestacks.Text = ""
        Else
            txtRestacks.Text = curwo.Restacks.ToString
        End If
        If curwo.Weight < 0 Then
            txtWeight.Text = ""
        Else
            txtWeight.Text = curwo.Weight.ToString
        End If
        If curwo.NumberOfItems < 0 Then
            txtTotalItems.Text = ""
        Else
            txtTotalItems.Text = curwo.NumberOfItems.ToString
        End If
        cbLoadType.SelectedValue = curwo.LoadTypeID
        If curwo.ReceiptNumber < 0 Then
            txtRecieptNumber.Text = ""
        Else
            txtRecieptNumber.Text = curwo.ReceiptNumber.ToString
            txtRecieptNumber.ReadOnly = True
        End If
        rbCash.Checked = curwo.PaymentType = "Cash"
        rbCheck.Checked = curwo.PaymentType = "Check"
        rbCard.Checked = curwo.PaymentType = "Card"
        rbsplit.Checked = curwo.PaymentType = "Split"
        '        rbCash.Checked = Not rbCash.Checked And Not rbCheck.Checked And Not rbCard.Checked And Not rbsplit.Checked
        If curwo.Amount = 0 Then
            txtAmount.Text = ""
        Else
            txtAmount.Text = curwo.Amount.ToString
        End If
        If curwo.SplitPaymentAmount = 0 Then
            txtAddCash.Text = ""
        Else
            txtAddCash.Text = curwo.SplitPaymentAmount.ToString
        End If
        txtCheckNumber.Text = curwo.CheckNumber
        txtBOLSEAL.Text = ""
        txtComments.Text = curwo.Comments
    End Sub 'populateForm
    '*********************************************************************************************************

    '*********************************************************************************************************
    Function buildWorkOrder(ByRef wo As WorkOrder) As WorkOrder

        'Page 1*********************************************************************************************************
        wo.LoadNumber = txtLoadNumber.Text
        
        wo.DoorNumber = IIf(Not txtDoor.Text Is Nothing, txtDoor.Text, String.Empty)
        'door number starts docktime ... this ensures it doesn't get reset w every save or even if the door # changes
        'wo.dockTime is set in the txtDoorNumber_LostFocus event handler

        If cbCarrier.Text = "NOT LISTED" Then
            wo.CarrierName = cbCarrier.Text
            wo.CarrierID = New Guid("DA6E74EA-4335-43AD-993E-8C10F1081568")
        ElseIf cbCarrier.SelectedIndex > -1 Then
            Dim vcarrierID As String = cbCarrier.SelectedValue.ToString
            wo.CarrierID = New Guid("{" & vcarrierID & "}")
            vcarrierID = wo.CarrierID.ToString
            wo.CarrierName = cbCarrier.Text
        Else
            wo.CarrierID = Nothing
            wo.CarrierName = Nothing
        End If
        wo.TruckNumber = IIf(Not txtTruckNumber.Text Is Nothing, txtTruckNumber.Text, String.Empty)
        wo.TrailerNumber = IIf(Not txtTrailerNumber.Text Is Nothing, txtTrailerNumber.Text, String.Empty)
        If cbDepartment.SelectedIndex > -1 Then
            wo.DepartmentID = New Guid(cbDepartment.SelectedValue.ToString)
            wo.Department = cbDepartment.Text
        Else
            wo.DepartmentID = Nothing
            wo.Department = Nothing
        End If
        If page1validate() = String.Empty Then
        End If

        'Page 2*********************************************************************************************************
        wo.VendorNumber = IIf(Not txtVendorNumber.Text Is Nothing, txtVendorNumber.Text, String.Empty)
        wo.VendorName = IIf(Not txtVendor.Text Is Nothing, txtVendor.Text, String.Empty)
        If IsNumeric(numPieces.Text) Then
            wo.Pieces = CType(numPieces.Text, Integer)
        Else
            wo.Pieces = -1
        End If
        If IsNumeric(txtPalletsUnloaded.Text) Then
            wo.PalletsUnloaded = CType(txtPalletsUnloaded.Text, Integer)
        Else
            wo.PalletsUnloaded = -1
        End If
        If cbLoadDescription.SelectedIndex > -1 Then
            wo.LoadDescriptionID = New Guid(cbLoadDescription.SelectedValue.ToString)
            wo.LoadDescription = cbLoadDescription.Text
        Else
            wo.LoadDescriptionID = Nothing
            wo.LoadDescription = Nothing
        End If
        'wo.Employee
        'wo.StartTime
        'Page 3*********************************************************************************************************
        If numPallets.Text > "" Then
            wo.PalletsReceived = CType(numPallets.Text, Integer)
        Else
            wo.PalletsReceived = -1
        End If
        Dim surnull As DateTime = "1900-1-1 00:00"
        If TimePickerAppointmentTime.Value > surnull Then
            Dim vappointmentTime As Date = Date.Now.ToShortDateString
            vappointmentTime = DateAdd(DateInterval.Hour, DatePart(DateInterval.Hour, TimePickerAppointmentTime.Value), vappointmentTime)
            vappointmentTime = DateAdd(DateInterval.Minute, DatePart(DateInterval.Minute, TimePickerAppointmentTime.Value), vappointmentTime)
            wo.AppointmentTime = vappointmentTime
        Else
            wo.AppointmentTime = "12:00 AM"
        End If
        wo.GateTime = "12:00 AM"
        If IsNumeric(txtBadPallets.Text) Then
            wo.BadPallets = CType(txtBadPallets.Text, Integer)
        Else
            wo.BadPallets = -1
        End If
        If IsNumeric(txtRestacks.Text) Then
            wo.Restacks = CType(txtRestacks.Text, Integer)
        Else
            wo.Restacks = -1
        End If
        If wo.Restacks > -1 And wo.BadPallets >= -1 Then
        End If
        If IsNumeric(txtWeight.Text) Then
            wo.Weight = CType(txtWeight.Text, Integer)
        Else
            wo.Weight = -1
        End If
        If IsNumeric(txtTotalItems.Text) Then
            If txtTotalItems.Text > 0 Then
                wo.NumberOfItems = CType(txtTotalItems.Text, Integer)
            Else
                wo.NumberOfItems = -1

            End If
        Else
            wo.NumberOfItems = -1
        End If
        If cbLoadType.SelectedIndex > -1 Then
            wo.LoadTypeID = New Guid(cbLoadType.SelectedValue.ToString)
            wo.LoadType = cbLoadType.Text
        Else
            wo.LoadTypeID = Nothing
            wo.LoadType = Nothing
        End If
        If wo.BadPallets > 0 Or wo.Restacks > 0 Then
            wo.CompTime = Date.Now.ToShortTimeString
        End If
        If wo.ReceiptNumber < 1 Then
            wo.ReceiptNumber = wo.LoadNumber
        End If

        'Page 4*********************************************************************************************************
        wo.IsCash = rbCash.Checked
        If IsNumeric(txtAmount.Text) Then
            wo.Amount = CType(txtAmount.Text, Decimal)
        Else
            'throw exception
            wo.Amount = Nothing
        End If
        If rbCash.Checked Then wo.PaymentType = "Cash"
        If rbCheck.Checked Then wo.PaymentType = "Check"
        If rbCard.Checked Then wo.PaymentType = "Card"
        If rbsplit.Checked Then
            wo.PaymentType = "Split"
            wo.SplitPaymentAmount = CType(txtAddCash.Text, Decimal)
        End If
        wo.CheckNumber = IIf(Not txtCheckNumber.Text Is Nothing, txtCheckNumber.Text, String.Empty)
        wo.BOL = IIf(Not txtBOLSEAL.Text Is Nothing, txtBOLSEAL.Text, String.Empty)
        wo.Comments = IIf(Not txtComments.Text Is Nothing, txtComments.Text, String.Empty)
        'wo.isClosed
        wo.LocationID = New Guid(utl.getxmldata("Location_ID"))
        wo.CustomerID = New Guid(utl.getCustomerID(utl.getxmldata("Parent_ID"), wo.VendorNumber))
        wo.PurchaseOrder = IIf(Not txtPurchaseOrder.Text Is Nothing, txtPurchaseOrder.Text, String.Empty)
        wo.CreatedBy = "Hand-Held"
        'Set Status *********************************************************************************************************
        Dim status As LoadStatus = LoadStatus.Undefined
        Dim strpg1 As String = page1validate()
        If (strpg1 = "") Or (strpg1 = "" And txtVendor.Text > "" And txtVendorNumber.Text > "") Then
            status = LoadStatus.CheckedIn
        End If
        If page1validate() = "" And AddDataChanged() Then
            status = LoadStatus.CheckedIn Or LoadStatus.AddDataChanged
        End If

        If Not curwo.Unloaders Is Nothing Then
            status = LoadStatus.AddDataChanged Or LoadStatus.Assigned
            If wo.StartTime < wo.LogDate Then
                wo.StartTime = Date.Now
            End If


        End If
        If curwo.BadPallets > -1 And curwo.Restacks > -1 Then
            wo.CompTime = Date.Now()
            status = LoadStatus.AddDataChanged Or LoadStatus.Assigned Or LoadStatus.Complete
            If Not rbCash.Checked And Not rbCheck.Checked And Not rbCard.Checked And Not rbsplit.Checked And cbLoadType.Text <> "Invoice" Then
                status = LoadStatus.AddDataChanged Or LoadStatus.Assigned Or LoadStatus.Complete Or LoadStatus.Printed
            End If
        End If
        wo.Status = status
        Return wo
    End Function 'buildWorkOrder
    '*********************************************************************************************************

    '*********************************************************************************************************
    Private Sub loadAssignedUnloaders()
        Dim dtempty As New DataTable
        dtempty.Columns.Add("Name", GetType(String))
        Dim dtAvailableUnloaders As New DataTable
        dtAvailableUnloaders.Columns.Add("ID", GetType(Guid))
        dtAvailableUnloaders.Columns.Add("Name", GetType(String))
        If Not curwo.Unloaders Is Nothing Then
            For Each ul As Unloader In curwo.Unloaders
                dtAvailableUnloaders.Rows.Add(ul.EmployeeID, ul.EmployeeName & " (" & ul.EmployeeLogin & ")")
            Next
            Me.lbUnloaders.DataSource = dtAvailableUnloaders
            Me.lbUnloaders.DisplayMember = "Name"
            Me.lbUnloaders.ValueMember = "ID"
        Else
        End If

        'End If
        '        addedUnloader = String.Empty
    End Sub 'loadAssignedUnloaders
    '*********************************************************************************************************

    '*********************************************************************************************************
    '*********************************************************************************************************
#Region "Validation"

    '*********************************************************************************************************
    Private Function AddDataChanged() As Boolean
        If numPieces.Text > "" _
        Or numPallets.Text > "" _
        Or cbLoadDescription.SelectedIndex > 1 Then
            Return True
        Else
            Return False
        End If
    End Function 'AddDataChanged
    '*********************************************************************************************************

    'allows for only numeric characters in handled controls***************************************************
    'Private Sub Numeric_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtLoadNumber.KeyPress, _
    ' numPieces.KeyPress, txtBadPallets.KeyPress, txtRestacks.KeyPress, _
    'txtRecieptNumber.KeyPress, txtPalletsUnloaded.KeyPress, txtWeight.KeyPress, txtTotalItems.KeyPress
    '    If IsNumeric(e.KeyChar) Or e.KeyChar = ControlChars.Back Then
    '        e.Handled = False
    '    Else
    '        e.Handled = True
    '    End If
    'End Sub 'Numeric_KeyPress
    '*********************************************************************************************************

    'allows for only decimal in handled controls***************************************************
    Private Sub DecimalNumeric_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtAmount.KeyPress, _
     txtAddCash.KeyPress
        If IsNumeric(e.KeyChar) Or e.KeyChar = "." Or e.KeyChar = ControlChars.Back Then
            e.Handled = False
        Else
            e.Handled = True
        End If
    End Sub 'DecimalNumeric_KeyPress
    '*********************************************************************************************************

    '*********************************************************************************************************
    Private Function page1validate() As String
        Dim retstr As String = String.Empty
        If txtDoor.Text = "" Then
            retstr = "Door Number Required"
        End If
        If cbCarrier.SelectedIndex < 0 And cbCarrier.Text = "" Then
            retstr = retstr & vbCrLf & "Carrier Name Required"
        End If
        If txtTruckNumber.Text = "" Then txtTruckNumber.Text = "DROP"
        If txtTrailerNumber.Text = "" Then
            retstr = retstr & vbCrLf & "Trailer Number Required"
        End If
        If txtPurchaseOrder.Text = "" Then
            retstr = retstr & vbCrLf & "Purchase Order Required"
        End If
        If cbDepartment.SelectedIndex < 0 Or cbDepartment.Text = "" Then
            retstr = retstr & vbCrLf & "Department Required"
        End If
        If retstr = String.Empty Then
            curwo.DoorNumber = txtDoor.Text
            curwo.CarrierID = cbCarrier.SelectedValue
            curwo.CarrierName = cbCarrier.Text
            curwo.TruckNumber = txtTruckNumber.Text
            curwo.TrailerNumber = txtTrailerNumber.Text
            curwo.PurchaseOrder = txtPurchaseOrder.Text
            curwo.Department = cbDepartment.Text
            curwo.DepartmentID = cbDepartment.SelectedValue
        End If

        Return retstr
    End Function 'page1validate
    '*********************************************************************************************************

    '*********************************************************************************************************
    Private Function page2validate() As String
        Dim retstr As String = String.Empty
        '       If txtVendorNumber.Text = "" Then
        '        retstr = retstr & vbCrLf & "Vendor # Required"
        '        End If
        If txtVendor.Text = "" Then
            retstr = retstr & vbCrLf & "Vendor Name Required"
        End If
        If numPieces.Text = "" Then
            retstr = retstr & vbCrLf & "# Pieces Required"
        End If
        If numPallets.Text = "" Then
            retstr = retstr & vbCrLf & "# Pallets Required"
        End If
        If cbLoadDescription.Text = "" Then
            retstr = retstr & vbCrLf & "Load Description Required"
        End If
        If lbUnloaders.Items.Count < 1 Then
            retstr = retstr & vbCrLf & "Select Unloaders"
        End If
        If IsNumeric(numPieces) Then
            If CType(numPieces.Text, Integer) < 1 Then
                retstr = retstr & vbCrLf & "Number of Pieces Required"
            End If
        End If
        If IsNumeric(numPallets.Text) Then
            If CType(numPallets.Text, Integer) < 1 Then
                retstr = retstr & vbCrLf & "# Pallets Received Required"
            End If
        End If
        Return retstr
    End Function 'page2validate
    '*********************************************************************************************************

    '*********************************************************************************************************
    Private Function page3validate() As String
        Dim retstr As String = String.Empty
        If txtPalletsUnloaded.Text = "" Then
            retstr = retstr & vbCrLf & " # Pallets Unloaded Required"
        End If
        '        If TimePickerAppointmentTime.Text = "" Then
        '        retstr = retstr & vbCrLf & ""
        '        End If
        '        If DateTimePickerGateTime.Text = "" Then
        '        retstr = retstr & vbCrLf & ""
        '        End If
        If txtBadPallets.Text = "" Then
            retstr = retstr & vbCrLf & "Number Bad Pallets Required"
        End If
        If txtRestacks.Text = "" Then
            retstr = retstr & vbCrLf & "Number Restacks Required"
        End If
        If txtWeight.Text = "" Then
            retstr = retstr & vbCrLf & "Weight Required"
        End If
        If txtTotalItems.Text = "" Then
            retstr = retstr & vbCrLf & "Total Items Required"
        End If
        If cbLoadType.SelectedIndex < 0 Or cbLoadType.Text = "" Then
            retstr = retstr & vbCrLf & "Load Type Required"
        End If
        Return retstr
    End Function 'page3validate
    '*********************************************************************************************************

    '*********************************************************************************************************
    Private Function page4validate() As String
        Dim retstr As String = String.Empty
        'Select Case cbLoadType.SelectedValue.ToString
        '    Case "c150f229-91aa-433c-8180-3d5a7d4b52f4" 'Inbound
        '    Case "0369f50a-52ca-4c97-8323-650adc182e04" 'Backhaul
        '    Case "55acef39-f005-4a6b-8ae9-80c2df9dcbb6" '3rd Party
        '    Case "e9af1c92-31b1-4849-b52b-93a9b618abc9" 'Driver Load
        '    Case "d62da4a5-fd15-4460-b62f-baa83ace65fd" 'Cash
        '    Case "f9d8fade-56db-4b46-b183-d7cce538270f" 'Manufacturers
        '    Case "fe3fabc8-5335-46f5-8be6-df3d5975d08c" 'Creditcard
        '    Case "6144c1a1-3657-4d91-a50a-f107c3a41847" 'Invoice
        '    Case Else
        'End Select

        'if loadtype is NOT Invoice or DriverLoad or Backhaul then radio selection required
        If cbLoadType.SelectedValue.ToString <> "6144c1a1-3657-4d91-a50a-f107c3a41847" _
        And cbLoadType.SelectedValue.ToString <> "e9af1c92-31b1-4849-b52b-93a9b618abc9" _
        And cbLoadType.SelectedValue.ToString <> "0369f50a-52ca-4c97-8323-650adc182e04" Then
            If Not rbCash.Checked And Not rbCheck.Checked And Not rbCard.Checked And Not rbsplit.Checked Then
                retstr = retstr & vbCrLf & "Select Cash, Check, Card or Split"
            End If
        End If

        'only if Driver Load is amount of zero ok        
        If cbLoadType.SelectedValue.ToString <> _
        "e9af1c92-31b1-4849-b52b-93a9b618abc9" Then     'Driver Load
            If txtAmount.Text = "" Then
                retstr = retstr & vbCrLf & "Amount Required"
            End If

        End If
        If (rbCheck.Checked Or rbCard.Checked) And txtCheckNumber.Text = "" Then
            retstr = retstr & vbCrLf & "Check/Transaction # Required"
        End If
        If rbsplit.Checked And txtAddCash.Text = "" Then
            retstr = retstr & vbCrLf & "Add Cash Required"
        End If
        If rbsplit.Checked And txtCheckNumber.Text = "" Then
            retstr = retstr & vbCrLf & "Check/Transaction # Required"
        End If
        '        txtBOLSEAL.Enabled = False
        '        txtComments.Enabled = False
        Return retstr
    End Function 'page4validate
    '*********************************************************************************************************

#End Region 'Validation
    '*********************************************************************************************************
    '*********************************************************************************************************

    '*********************************************************************************************************
    '*********************************************************************************************************
#Region "Tabs Control Events"

    Public pageindex As Integer = 0 'prevents tab control events from firing.

    '*********************************************************************************************************
    Private Sub TabControl1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TabControl1.SelectedIndexChanged
        Dim selectedTab As Integer = TabControl1.SelectedIndex
        If selectedTab = 1 Or selectedTab = 2 Then
            If page1validate() > "" Then
                TabControl1.SelectedIndex = 0
                MessageBox.Show(page1validate, "Missing Information")
            Else
                '                txtVendorNumber.Focus()
            End If
            If selectedTab = 2 Or selectedTab = 3 Then
                If page2validate() > "" Then
                    '            TabControl1.SelectedIndex = 1
                    '            MessageBox.Show(page2validate, "Missing Information")
                    '        Else
                End If
            End If
        End If
    End Sub 'TabControl1_SelectedIndexChanged
    '*********************************************************************************************************

    '*********************************************************************************************************
    '*********************************************************************************************************
#Region "Tabs Control Events Page 1"

    '*********************************************************************************************************
    Private Sub txtLoadNumber_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtLoadNumber.GotFocus
        txtLoadNumber.Text = ""
    End Sub 'txtLoadNumber_GotFocus
    '*********************************************************************************************************

    '*********************************************************************************************************
    Private Sub txtLoadNumber_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtLoadNumber.LostFocus
        If txtLoadNumber.Text = "" Then txtLoadNumber.Text = curwo.LoadNumber
    End Sub 'txtLoadNumber_LostFocus
    '*********************************************************************************************************

    '*********************************************************************************************************
    Private Sub txtDoor_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtDoor.LostFocus
        If (curwo.DoorNumber Is Nothing Or curwo.DoorNumber = String.Empty) Then 'workorder never had doornumber
            If txtDoor.Text.Trim > "" Then 'there is something in the txtbox
                curwo.DoorNumber = txtDoor.Text 'store it
                If curwo.Unloaders Is Nothing Then
                    curwo.DockTime = Date.Now 'set DockTime
                    curwo.LogDate = Date.Now

                Else
                    curwo.DoorNumber = txtDoor.Text 'store it
                End If
            End If
        Else ' this is a change
            If txtDoor.Text.Trim > "" And curwo.Unloaders Is Nothing Then 'change both
                curwo.DoorNumber = txtDoor.Text 'store it
                curwo.DockTime = Date.Now 'set DockTime
            Else
                curwo.DoorNumber = txtDoor.Text 'store change
            End If
        End If

        btnAddUnloader.Enabled = txtDoor.Text > ""
        btnDelUnloader.Enabled = txtDoor.Text > ""

    End Sub 'txtDoor_LostFocus
    '*********************************************************************************************************


    '*********************************************************************************************************
    Private Sub cbCarrier_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cbCarrier.KeyUp
        Dim cb As ComboBox = DirectCast(sender, ComboBox)
        Dim sTypedText As String = cb.Text
        Dim ind As Integer = cb.SelectedIndex
        If sTypedText.Length > 2 Then
            Cursor.Current = Cursors.WaitCursor
            Dim a As New SqlCeAdapter("\Program Files\SEUdock\SEUdock.sdf")
            Dim carriertable As DataTable = New DataTable
            Dim strSelect As String = "SELECT DISTINCT Name,ID FROM Carrier where Name like '" & sTypedText & "%' ORDER BY Name"
            carriertable = a.QueryDatabaseForTable(strSelect)
            If carriertable.Rows.Count > 0 Then
                cb.DataSource = carriertable
                cb.DisplayMember = "Name"
                cb.ValueMember = "ID"
                Cursor.Current = Cursors.Default
                Dim dd As Boolean = SetDroppedDown(sender)
            Else
                '                cb.Items.Add("NOT LISTED")
                'cb.additem(.Items.Add(New NotListed("NOT LISTED", "DA6E74EA-4335-43AD-993E-8C10F1081568"))
                cb.Text = "NOT LISTED"
                '                sTypedText = cb.Text
            End If
            'Select Case cb.Items.Count
            '    Case Is > 1
            '        cb.Text = sTypedText
            cb.Select(cb.Text.Length, 0)
            'End Select
        Else
            Cursor.Current = Cursors.Default
            Return
        End If
        Cursor.Current = Cursors.Default
    End Sub 'cbCarrier_KeyUp
    '*********************************************************************************************************

    '*********************************************************************************************************
    Private Sub cbCarrier_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbCarrier.LostFocus
        If cbCarrier.Text = "NOT LISTED" Then
            If Not txtComments.Text.Contains("Carrier Name:") Then

                MessageBox.Show("Provide Carrier Name in Comments on Page 4", "Required Info")
                TabControl1.SelectedIndex = 3
                txtComments.Focus()
                If txtComments.Text.Length > 0 Then
                    txtComments.Text = txtComments.Text & vbCrLf & "Carrier Name: "
                Else
                    txtComments.Text = "Carrier Name: "
                End If
                txtComments.Select(txtComments.Text.Length, 0)
            End If
        End If
    End Sub
    '*********************************************************************************************************


#End Region 'Tabs Control Events Page 1
    '*********************************************************************************************************
    '*********************************************************************************************************

    '*********************************************************************************************************
    '*********************************************************************************************************
#Region "Tabs Control Events Page 2"

    '*********************************************************************************************************
    Private Sub txtVendorNumber_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtVendorNumber.TextChanged
        If txtVendorNumber.Text.Length > 1 Then
            Dim a As New SqlCeAdapter("\Program Files\SEUdock\SEUdock.sdf")
            Dim Vendor As DataTable = New DataTable
            Dim strSelect As String = "select name, Number from Vendor where Number ='" & txtVendorNumber.Text & "' AND ParentCompanyID = '" & utl.getxmldata("Parent_ID") & "'"
            Dim vnam As String = a.QueryDatabaseForScalar(strSelect)
            If Not vnam Is Nothing Then
                txtVendor.Text = vnam
            Else
                txtVendor.Text = "NOT LISTED"
            End If
        End If
    End Sub 'txtVendorNumber_TextChanged
    '*********************************************************************************************************

    '*********************************************************************************************************
    Private Sub txtVendorNumber_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtVendorNumber.LostFocus
        If txtVendor.Text = "NOT LISTED" Or txtVendor.Text = "" Then
            If Not txtComments.Text.Contains("Vendor Name:") Then

                MessageBox.Show("Provide Vendor Name in Comments on Page 4", "Required Info")
                TabControl1.SelectedIndex = 3
                txtComments.Focus()
                If txtComments.Text.Length > 0 Then
                    txtComments.Text = txtComments.Text & vbCrLf & "Vendor Name: "
                Else
                    txtComments.Text = "Vendor Name: "
                End If
                txtComments.Select(txtComments.Text.Length, 0)
            End If
        End If
    End Sub 'txtVendorNumber_LostFocus
    '*********************************************************************************************************

    '*********************************************************************************************************
    Private Sub cbLoadDescription_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbLoadDescription.SelectedIndexChanged
        If cbLoadDescription.Text = "Floor load" Then
            txtPalletsUnloaded.Text = 0
            txtPalletsUnloaded.ReadOnly = True
            txtPalletsUnloaded.Enabled = False
        Else
            txtPalletsUnloaded.ReadOnly = False
            txtPalletsUnloaded.Enabled = True
        End If
    End Sub 'cbLoadDescription_SelectedIndexChanged
    '*********************************************************************************************************

    '*********************************************************************************************************
    Private Sub btnAddUnloader_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddUnloader.Click
        Dim f As New SelectUnloader
        curwo.DoorNumber = txtDoor.Text
        curwo = buildWorkOrder(curwo)
        f.isEdit = False
        f.cwo = curwo
        f.Show()
        Me.Close()
    End Sub 'btnAddUnloader_Click
    '*********************************************************************************************************

    '*********************************************************************************************************
    Private Sub btnDelUnloader_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelUnloader.Click
        If lbUnloaders.SelectedIndex = -1 Then Exit Sub
        If Not curwo.Unloaders Is Nothing Then 'if there are unloaders
            If curwo.Unloaders.Count < 2 Then
                MessageBox.Show("Cannot remove last remaining unloader" & vbCrLf & "Add another unloader first", "Unloader Exception")
            Else
                ischanged = True
                Dim ulid As String = lbUnloaders.SelectedValue.ToString 'get the selected unloaderID
                '            Dim i As Integer = 0
                Dim unloaders As List(Of Unloader) = New List(Of Unloader) 'create empty list of unloaders
                For Each ul As Unloader In curwo.Unloaders 'go thru curwo.unloaders
                    If ul.EmployeeID.ToString = ulid Then 'if this ul is the one I selected then

                        Dim a As String = String.Empty 'basically do nuthing

                    Else ' ad them to my new empty list
                        unloaders.Add(ul)
                    End If
                Next
                curwo.Unloaders = unloaders 'replace curwo.unloaders with my new list

                Dim dtAvailableUnloaders As New DataTable
                dtAvailableUnloaders.Columns.Add("ID", GetType(Guid))
                dtAvailableUnloaders.Columns.Add("Name", GetType(String))
                If Not curwo.Unloaders Is Nothing Then
                    For Each ul As Unloader In curwo.Unloaders
                        dtAvailableUnloaders.Rows.Add(ul.EmployeeID, ul.EmployeeName & " (" & ul.EmployeeLogin & ")")
                    Next
                End If
                Me.lbUnloaders.DataSource = dtAvailableUnloaders
                Me.lbUnloaders.DisplayMember = "Name"
                Me.lbUnloaders.ValueMember = "ID"
            End If
        End If


    End Sub 'btnDelUnloader_Click
    '*********************************************************************************************************


#End Region 'Tabs Control Events Page 2
    '*********************************************************************************************************
    '*********************************************************************************************************

    '*********************************************************************************************************
    '*********************************************************************************************************
#Region "Tabs Control Events Page 3"

    '*********************************************************************************************************
    Private Sub cbLoadType_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbLoadType.SelectedIndexChanged
        If cbLoadType.SelectedIndex > -1 Then
            Select Case cbLoadType.SelectedValue.ToString
                Case "e9af1c92-31b1-4849-b52b-93a9b618abc9" 'Driver Load
                    rbCash.Checked = False
                    rbCheck.Checked = False
                    rbCard.Checked = False
                    rbsplit.Checked = False
                    rbCash.Enabled = False
                    rbCheck.Enabled = False
                    rbCard.Enabled = False
                    rbsplit.Enabled = False
                    lblCheckNumber.Enabled = False
                    lblCheckNumber.Text = "Check/Transaction Number"
                    txtCheckNumber.Text = ""
                    txtCheckNumber.Enabled = False
                    lblAddCash.Visible = False
                    txtAddCash.Visible = False
                    txtAmount.Text = "0"
                    txtAmount.Enabled = False
                    lblAmount.Enabled = False
                    txtAmount.ReadOnly = True
                    txtAddCash.Text = ""
                    txtAddCash.ReadOnly = True
                Case "6144c1a1-3657-4d91-a50a-f107c3a41847", _
                "0369f50a-52ca-4c97-8323-650adc182e04", _
                "c150f229-91aa-433c-8180-3d5a7d4b52f4", _
                "55acef39-f005-4a6b-8ae9-80c2df9dcbb6", _
                "f9d8fade-56db-4b46-b183-d7cce538270f"
                    rbCash.Checked = False
                    rbCheck.Checked = False
                    rbCard.Checked = False
                    rbsplit.Checked = False
                    rbCash.Enabled = False
                    rbCheck.Enabled = False
                    rbCard.Enabled = False
                    rbsplit.Enabled = False
                    txtAmount.Text = ""
                    txtAmount.ReadOnly = False
                    txtAmount.Enabled = True
                    lblAmount.Enabled = True
                    lblCheckNumber.Text = "Check/Transaction Number"
                    lblCheckNumber.Enabled = False
                    txtCheckNumber.Text = ""
                    txtCheckNumber.Enabled = False
                    lblAddCash.Visible = False
                    txtAddCash.Visible = False
                    txtAddCash.Text = "0"
                Case "fe3fabc8-5335-46f5-8be6-df3d5975d08c" 'Creditcard
                    rbCash.Checked = False
                    rbCash.Enabled = False
                    rbCheck.Checked = False
                    rbCheck.Enabled = False
                    rbCard.Checked = True
                    rbCard.Enabled = True
                    rbsplit.Checked = False
                    rbsplit.Enabled = False
                    lblAmount.Enabled = True
                    txtAmount.Enabled = True
                    lblCheckNumber.Text = "Transaction Number"
                    lblCheckNumber.Enabled = True
                    txtCheckNumber.Text = ""
                    txtCheckNumber.Enabled = True
                    lblAddCash.Visible = False
                    txtAddCash.Visible = False
                    txtAddCash.Text = "0"
                Case "d62da4a5-fd15-4460-b62f-baa83ace65fd" 'Cash
                    rbCash.Checked = True
                    rbCash.Enabled = True
                    rbCheck.Checked = False
                    rbCheck.Enabled = True
                    rbCard.Checked = False
                    rbCard.Enabled = False
                    rbsplit.Checked = False
                    rbsplit.Enabled = True
                    lblAmount.Enabled = True
                    txtAmount.Text = ""
                    txtAmount.Enabled = True
                    lblCheckNumber.Text = "Check/Transaction Number"
                    lblCheckNumber.Enabled = False
                    txtCheckNumber.Text = ""
                    txtCheckNumber.Enabled = True
                    lblAddCash.Visible = False
                    txtAddCash.Visible = False
                    txtAddCash.Text = "0"
                Case Else 'same as cash  
                    rbCash.Checked = True
                    rbCash.Enabled = True
                    rbCheck.Checked = False
                    rbCheck.Enabled = True
                    rbCard.Checked = False
                    rbCard.Enabled = False
                    rbsplit.Checked = False
                    rbsplit.Enabled = True
                    txtAmount.ReadOnly = False
                    lblCheckNumber.Text = "Check/Transaction Number"
                    lblCheckNumber.Enabled = False
                    txtCheckNumber.Text = ""
                    txtCheckNumber.Enabled = True
                    txtAddCash.ReadOnly = False
            End Select
        End If
    End Sub 'cbLoadType_SelectedIndexChanged
    '*********************************************************************************************************

#End Region 'Tabs Control Events Page 3
    '*********************************************************************************************************
    '*********************************************************************************************************

    '*********************************************************************************************************
    '*********************************************************************************************************
#Region "Tabs Control Events Page 4"

    '*********************************************************************************************************
    Private Sub txtAmount_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtAmount.GotFocus
        If page1validate() > "" Then
            TabControl1.SelectedIndex = 0
            Exit Sub
        End If
        If cbLoadType.SelectedIndex = -1 Then
            TabControl1.SelectedIndex = 2
            cbLoadType.Focus()
            Dim dr As DialogResult = MessageBox.Show("Select Load Type", "Page 3 Required Info")
        End If

    End Sub 'txtAmount_GotFocus
    '*********************************************************************************************************

    '*********************************************************************************************************
    Private Sub txtAmount_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtAmount.LostFocus
        If IsNumeric(txtAmount.Text.Trim()) And txtAmount.Text.Trim = txtPurchaseOrder.Text.Trim And txtPurchaseOrder.Text.Trim > "" Then
            MessageBox.Show("Is the Amount and the PO# really the same?", "Possible Data Enty Error")
        End If
    End Sub 'txtAmount_LostFocus
    '*********************************************************************************************************

    '*********************************************************************************************************
    Private Sub rbCash_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbCash.CheckedChanged
        If pageindex > 0 Then ischanged = True
        If rbCash.Checked Then
            lblCheckNumber.Text = "Check/Transaction Number"
            lblCheckNumber.Enabled = False
            txtCheckNumber.Enabled = False
            lblAddCash.Visible = False
            txtAddCash.Visible = False
            txtAddCash.Text = "0"
        Else

        End If
    End Sub 'rbCash_CheckedChanged
    '*********************************************************************************************************

    '*********************************************************************************************************
    Private Sub rbCheck_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbCheck.CheckedChanged
        If pageindex > 0 Then ischanged = True
        If rbCheck.Checked Then
            lblCheckNumber.Enabled = True
            lblCheckNumber.Text = "Check Number"
            txtCheckNumber.Enabled = True
            lblAddCash.Visible = False
            txtAddCash.Visible = False
            txtAddCash.Text = "0"
        End If
    End Sub 'rbCheck_CheckedChanged
    '*********************************************************************************************************

    '*********************************************************************************************************
    Private Sub rbCard_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbCard.CheckedChanged
        If pageindex > 0 Then ischanged = True
        If rbCard.Checked Then
            lblCheckNumber.Enabled = True
            lblCheckNumber.Text = "Transaction Number"
            txtCheckNumber.Enabled = True
            lblAddCash.Visible = False
            txtAddCash.Visible = False
            txtAddCash.Text = "0"
        End If
    End Sub 'rbCard_CheckedChanged
    '*********************************************************************************************************

    '*********************************************************************************************************
    Private Sub rbsplit_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbsplit.CheckedChanged
        If pageindex > 0 Then ischanged = True
        If rbsplit.Checked Then
            lblCheckNumber.Text = "Check Number" ' "Check/Transaction Number"
            lblCheckNumber.Enabled = True
            txtCheckNumber.Enabled = True
            lblAddCash.Visible = True
            txtAddCash.Visible = True
            txtAddCash.Text = ""
            txtAddCash.ReadOnly = False
        End If
    End Sub 'rbsplit_CheckedChanged
    '*********************************************************************************************************

#End Region 'Tabs Control Events Page 4
    '*********************************************************************************************************
    '*********************************************************************************************************

 
#End Region 'Tabs Control Events
    '*********************************************************************************************************
    '*********************************************************************************************************


    '*********************************************************************************************************
    '*********************************************************************************************************
#Region "Load comboBoxes boxes"

    '*********************************************************************************************************
    Private Sub loadDepartments()
        Dim a As New SqlCeAdapter("\Program Files\SEUdock\SEUdock.sdf")
        Dim dtDepartment As DataTable = New DataTable
        dtDepartment = a.QueryDatabaseForTable("select ID as DeptID, Name as DeptName from Department Order By DeptName")
        cbDepartment.DisplayMember = "DeptName"
        cbDepartment.ValueMember = "DeptID"
        cbDepartment.DataSource = dtDepartment
        cbDepartment.SelectedIndex = -1
    End Sub 'loadDepartments
    '*********************************************************************************************************

    '*********************************************************************************************************
    Private Sub loadDescriptions()
        Dim a As New SqlCeAdapter("\Program Files\SEUdock\SEUdock.sdf")
        Dim dtDescription As DataTable = New DataTable
        dtDescription = a.QueryDatabaseForTable("select Name, ID from Description Order By Name")
        cbLoadDescription.DataSource = dtDescription
        cbLoadDescription.DisplayMember = "Name"
        cbLoadDescription.ValueMember = "ID"
        cbLoadDescription.SelectedIndex = -1
    End Sub 'loadDescriptions
    '*********************************************************************************************************

    '*********************************************************************************************************
    Private Sub loadLoadTypes()
        Dim a As New SqlCeAdapter("\Program Files\SEUdock\SEUdock.sdf")
        Dim dtloadtype As DataTable = New DataTable
        dtloadtype = a.QueryDatabaseForTable("Select Name, ID from LoadType Order By Name")
        cbLoadType.DataSource = dtloadtype
        cbLoadType.DisplayMember = "Name"
        cbLoadType.ValueMember = "ID"
        cbLoadType.SelectedIndex = -1
    End Sub 'loadLoadTypes
    '*********************************************************************************************************

#End Region 'Load comboBoxes boxes
    '*********************************************************************************************************
    '*********************************************************************************************************

    '*********************************************************************************************************
    '*********************************************************************************************************
#Region "buttons"

    '*********************************************************************************************************
    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit4.Click, btnExit3.Click, btnExit2.Click, btnExit1.Click
        'Save load info
        Dim result1 As DialogResult = MessageBox.Show("Save Changes?", "Important Question", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
        If result1 = 6 Then 'YES
            If IsNumeric(txtBadPallets.Text) And Not IsNumeric(txtRestacks.Text) Then
                MessageBox.Show("If you enter BadPallets you MUST also enter Restacks (0 is valid)", "Restacks REQUIRED!")
                TabControl1.SelectedIndex = 2
                txtRestacks.Focus()
                Exit Sub
            ElseIf Not IsNumeric(txtBadPallets.Text) And IsNumeric(txtRestacks.Text) Then
                MessageBox.Show("If you enter RestacksBadPallets you MUST also enter BadPallets (0 is valid)", "BadPallets REQUIRED!")
                TabControl1.SelectedIndex = 2
                txtBadPallets.Focus()
                Exit Sub
            End If
            If page1validate() > "" Then
                TabControl1.SelectedIndex = 0
                MessageBox.Show("Unable to save until" & vbCrLf & page1validate(), "Minimum Required Information")
            Else
                cwo = buildWorkOrder(cwo)
                Dim wodal As New WorkOrderDAL
                Dim suc As String = wodal.AddWorkOrder(cwo, True)
                '                If Utils.Connected Then
                '                asyncConn.syncme()
                '            End If

                Form1.Show()
                Me.Close()
            End If
        ElseIf result1 = 7 Then 'NO
            Form1.Show()
            cwo = New WorkOrder
            Me.Close()
        End If
    End Sub 'btnExit_Click
    '*********************************************************************************************************

    '*********************************************************************************************************
    '*********************************************************************************************************
#Region "Clear Buttons"

    '*********************************************************************************************************
    Private Sub btnClearPage1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearPage1.Click
        txtLoadNumber.Text = ""
        txtDoor.Text = ""
        cbCarrier.SelectedIndex = -1
        cbCarrier.Text = ""
        txtTruckNumber.Text = ""
        txtTrailerNumber.Text = ""
        txtPurchaseOrder.Text = ""
        cbDepartment.SelectedIndex = -1
    End Sub 'btnClearPage1_Click
    '*********************************************************************************************************

    '*********************************************************************************************************
    Private Sub btnClearPage2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearPage2.Click
        txtVendorNumber.Text = ""
        txtVendor.Text = ""
        numPallets.Text = ""
        numPieces.Text = ""
        cbLoadDescription.SelectedIndex = -1
        cwo.Unloaders = Nothing
        If lbUnloaders.Items.Count > 0 Then
            '            lbUnloaders.Items.Clear()
        End If
    End Sub 'btnClearPage2_Click
    '*********************************************************************************************************

    '*********************************************************************************************************
    Private Sub btnClearPage3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearPage3.Click
        txtPalletsUnloaded.Text = ""
        TimePickerAppointmentTime.Value = Date.Now
        DateTimePickerGateTime.Value = Date.Now
        txtBadPallets.Text = ""
        txtWeight.Text = ""
        txtRestacks.Text = ""
        txtTotalItems.Text = ""
        cbLoadType.SelectedIndex = -1
    End Sub 'btnClearPage3_Click
    '*********************************************************************************************************

    '*********************************************************************************************************
    Private Sub btnClearPage4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearPage4.Click
        rbCash.Checked = False
        rbCheck.Checked = False
        rbCard.Checked = False
        txtAmount.Text = ""
        txtCheckNumber.Text = ""
        txtBOLSEAL.Text = ""
        txtComments.Text = ""
    End Sub 'btnClearPage4_Click
    '*********************************************************************************************************

#End Region 'Clear Buttons
    '*********************************************************************************************************
    '*********************************************************************************************************

    '*********************************************************************************************************
    '    Public Sub CtrlConn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CtrlConn4.Click, CtrlConn2.Click, CtrlConn9.Click, CtrlConn1.Click
    '        ConnectionInfo.Show()
    '    End Sub 'CtrlConn_Click
    '*********************************************************************************************************

#End Region 'buttons
    '*********************************************************************************************************
    '*********************************************************************************************************


    Private Sub TimePickerAppointmentTime_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub
End Class

