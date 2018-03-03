Imports System.Data
Imports System.Data.SqlServerCe
Imports Microsoft.WindowsCE.Forms

Public Class LoadEditor

    '*********************************************************************************************************
    '*********************************************************************************************************
#Region "Class Variables / Properties / Constants"
    '*********************************************************************************************************
    Public curwo As WorkOrder = Nothing
    Private ischanged As Boolean = False
    Public dups = False
    Public eid As String = String.Empty 'this came from list of employee eid
    Public fromAddUnloaders = False 'this is returned from Select Unloader page
    Public woid As String = String.Empty 'this is for a specific workorder id

    '*********************************************************************************************************
    'for accessing/setting dropdown box settings
    Public Const CB_SHOWDROPDOWN As Integer = &H14F
    '    Public Const CB_GETDROPPEDSTATE = "&H14f<true" '0x0157<false
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
#End Region 'Class Variables / Properties / Constants
    '*********************************************************************************************************
    '*********************************************************************************************************

    '*********************************************************************************************************
    Public Shared Function SetDroppedDown(ByVal comboBox As ComboBox) As Boolean 'to open combo(drop down) boxes
        Dim comboBoxDroppedMsg As Message = Message.Create(comboBox.Handle, CB_SHOWDROPDOWN, CType(1, IntPtr), IntPtr.Zero)
        '        Dim comboBoxDroppedMsg As Message = Message.Create(comboBox.Handle, CB_SHOWDROPDOWN, DirectCast(0, IntPtr), IntPtr.Zero) 'to close
        MessageWindow.SendMessage(comboBoxDroppedMsg)
        Return comboBoxDroppedMsg.Result <> IntPtr.Zero
    End Function
    '*********************************************************************************************************

    '*********************************************************************************************************
    '*********************************************************************************************************
#Region "Page Events"

    '*********************************************************************************************************
    Private Sub LoadEditor_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Cursor.Current = Cursors.WaitCursor
        loadDepartments()
        loadDescriptions()
        '  loadAssignedUnloaders()
        loadLoadTypes()
        If woid > "" Then
            Dim wodal As New WorkOrderDAL
            curwo = wodal.GetLoadByID(woid)
        ElseIf fromAddUnloaders Then
            'use passed curwo
        Else
            MessageBox.Show("WorkOrderID Not Found", "Error - Not Found")
        End If
        ischanged = False
        loadAssignedUnloaders()
        populateForm(curwo)
        Cursor.Current = Cursors.Default
    End Sub 'LoadEditor_Load
    '*********************************************************************************************************

    '*********************************************************************************************************
    Private Sub LoadEditor_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.GotFocus

        '        loadAssignedUnloaders()
        If eid > "" Then
            TabControl1.SelectedIndex = 1
        End If
        If fromAddUnloaders Then
            loadAssignedUnloaders()
            ischanged = True
            TabControl1.SelectedIndex = 1
        End If
        If curwo.Status > 73 Then
            TabControl1.SelectedIndex = 3
        End If
    End Sub 'LoadEditor_GotFocus
    '*********************************************************************************************************
#End Region 'Page Events
    '*********************************************************************************************************
    '*********************************************************************************************************

    '*********************************************************************************************************
    Private Sub populateForm(ByVal wo As WorkOrder)
        If wo.Status > 74 Then
            populatePrintForm(wo)
            Exit Sub
        End If
        If wo.Status = 74 Then
            btnPrint.Visible = wo.Status = 74
            btnClearPage42.Visible = False
            btnClearPage4.Visible = False
        End If
        If wo.CarrierName <> "NOT LISTED" Then
            Dim a As New SqlCeAdapter("\Program Files\SEUdock\SEUdock.sdf")
            Dim strSQL As String = "Select ID, Name from Carrier where id='" & wo.CarrierID.ToString & "'"
            Dim ctbl As DataTable = a.QueryDatabaseForTable(strSQL)
            cbCarrier.DataSource = ctbl

            cbCarrier.DisplayMember = "Name"
            cbCarrier.ValueMember = "ID"
        End If
        txtDoor.Text = wo.DoorNumber

        If wo.LoadNumber < 1 Then
            txtLoadNumber.Text = ""
        Else
            txtLoadNumber.Text = wo.LoadNumber
        End If
        If Not wo.CarrierName = "NOT LISTED" Then
            cbCarrier.SelectedValue = wo.CarrierID

        End If
        cbCarrier.Text = wo.CarrierName
        '        cbCarrier.SelectedValue = wo.CarrierID
        txtTruckNumber.Text = wo.TruckNumber
        txtTrailerNumber.Text = wo.TrailerNumber
        txtPurchaseOrder.Text = wo.PurchaseOrder
        cbDepartment.SelectedValue = wo.DepartmentID
        '        cbDepartment.SelectedText = deptartmentnamebyid(wo.DepartmentID.ToString)
        ' Page 2*********************************************************************************
        txtVendorNumber.Text = wo.VendorNumber
        txtVendor.Text = wo.VendorName
        txtVendor.ReadOnly = True
        cbLoadType.SelectedValue = wo.LoadTypeID
        If wo.PalletsUnloaded < 0 Then
            txtPalletsUnloaded.Text = ""
        Else
            txtPalletsUnloaded.Text = wo.PalletsUnloaded.ToString
        End If
        txtPalletsUnloaded.ReadOnly = False
        If wo.Pieces < 1 Then
            numPieces.Text = ""
        Else
            numPieces.Text = wo.Pieces
        End If
        If wo.NumberOfItems < 1 Then
            txtTotalItems.Text = ""
        Else
            txtTotalItems.Text = wo.NumberOfItems.ToString

        End If
        cbLoadDescription.SelectedValue = wo.LoadDescriptionID
        Dim surnull As Date = "1/1/1900 00:00 AM"
        If wo.AppointmentTime > surnull Then
            TimePickerAppointmentTime.Value = wo.AppointmentTime
        End If
        btnAddUnloaders.Enabled = txtDoor.Text > ""
        btnDelUnloader.Enabled = btnAddUnloaders.Enabled
        'Page 3 *********************************************************************************
        If wo.PalletsReceived < 0 Then
            numPallets.Text = ""
        Else
            numPallets.Text = wo.PalletsReceived.ToString
        End If

        If wo.Status < 74 And wo.BadPallets < 1 Then
            txtBadPallets.Text = ""
        Else
            txtBadPallets.Text = wo.BadPallets.ToString
        End If
        If wo.Status < 74 And wo.Restacks < 1 Then
            txtRestacks.Text = ""
        Else
            txtRestacks.Text = wo.Restacks.ToString
        End If
        If wo.Weight < 1 Then
            txtweight.Text = ""
        Else
            txtweight.Text = wo.Weight.ToString
        End If
        If wo.NumberOfItems < 1 Then
            txtTotalItems.Text = ""
        Else
            txtTotalItems.Text = wo.NumberOfItems.ToString
        End If
        cbLoadType.SelectedValue = wo.LoadTypeID
        If wo.ReceiptNumber < 0 Then
            txtRecieptNumber.Text = ""
        Else
            txtRecieptNumber.Text = wo.ReceiptNumber.ToString
            txtRecieptNumber.ReadOnly = True
        End If
        'Page 4**********************************************************************************************
        rbCash.Checked = wo.PaymentType = "Cash"
        rbCheck.Checked = wo.PaymentType = "Check"
        rbCard.Checked = wo.PaymentType = "Card"
        rbsplit.Checked = wo.PaymentType = "Split"
        txtAddCash.Text = wo.SplitPaymentAmount
        If rbsplit.Checked Then
            txtAddCash.Visible = True
            lblAddCash.Visible = True
        End If
        If wo.Amount = 0 Then
            txtAmount.Text = ""
        Else
            txtAmount.Text = wo.Amount
            txtAmount.TextAlign = HorizontalAlignment.Right
        End If
        txtCheckNumber.Text = wo.CheckNumber
        txtBOLSEAL.Text = wo.BOL
        txtComments.Text = wo.Comments
        pageindex = +1
    End Sub 'populateForm
    '*********************************************************************************************************

    '*********************************************************************************************************
    Private Sub populatePrintForm(ByVal wo As WorkOrder)
        btnPrint.Visible = wo.Status < 78
        btnClearPage42.Visible = False
        btnClearPage4.Visible = False
        btnAddUnloaders.Enabled = False
        btnDelUnloader.Enabled = False
        btnClearPage1.Visible = False
        btnClearPage2.Visible = False
        btnClearPage3.Visible = False
        btnClearPage4.Visible = False
        If wo.CarrierName > "" Then
            Dim a As New SqlCeAdapter("\Program Files\SEUdock\SEUdock.sdf")
            Dim strSQL As String = "Select ID, Name from Carrier where id='" & wo.CarrierID.ToString & "'"
            Dim ctbl As DataTable = a.QueryDatabaseForTable(strSQL)
            cbCarrier.DataSource = ctbl

            If Not ctbl Is Nothing Then
                Dim n As String = ctbl.Rows(0).Item(1)
                Dim i As String = ctbl.Rows(0).Item(0).ToString
            End If
            cbCarrier.DisplayMember = "Name"
            cbCarrier.ValueMember = "ID"
        End If

        cbCarrier.Enabled = False
        txtDoor.Text = wo.DoorNumber
        txtDoor.Enabled = False

        'If wo.LoadNumber < 1 Then
        '    txtLoadNumber.Text = ""
        'Else
        txtLoadNumber.Text = CType(wo.Status, String)
        '            txtLoadNumber.Text = wo.LoadNumber
        '        End If
        txtLoadNumber.Enabled = False


        cbCarrier.Text = wo.CarrierName
        cbCarrier.SelectedValue = New Guid(wo.CarrierID.ToString)
        'results in NO selected index = -1

        cbCarrier.Enabled = False
        txtTruckNumber.Text = wo.TruckNumber
        txtTruckNumber.Enabled = False
        txtTrailerNumber.Text = wo.TrailerNumber
        txtTrailerNumber.Enabled = False
        txtPurchaseOrder.Text = wo.PurchaseOrder
        txtPurchaseOrder.Enabled = False
        cbDepartment.SelectedValue = wo.DepartmentID
        '        cbDepartment.SelectedText = deptartmentnamebyid(wo.DepartmentID.ToString)
        cbDepartment.Enabled = False
        txtVendorNumber.Text = wo.VendorNumber
        wo.VendorName = IIf(Not txtVendor.Text Is Nothing, txtVendor.Text, String.Empty)
        txtVendorNumber.Enabled = False
        txtVendor.Text = wo.VendorName
        txtVendor.Enabled = False
        cbLoadType.SelectedValue = wo.LoadTypeID
        cbLoadType.Enabled = False
        If wo.Pieces < 1 Then
            numPieces.Text = ""
        Else
            numPieces.Text = wo.Pieces
        End If
        numPieces.Text = wo.Pieces
        numPieces.Enabled = False
        If wo.PalletsUnloaded < 0 Then
            txtPalletsUnloaded.Text = ""
        Else
            txtPalletsUnloaded.Text = wo.PalletsUnloaded.ToString
        End If
        txtPalletsUnloaded.Enabled = False
        lbUnloaders.Enabled = False
        numPieces.Enabled = False
        cbLoadDescription.SelectedValue = wo.LoadDescriptionID
        cbLoadDescription.Enabled = False
        Dim surnull As Date = "1/1/1900 00:00 AM"
        If wo.AppointmentTime > surnull Then
            TimePickerAppointmentTime.Value = wo.AppointmentTime
        End If
        TimePickerAppointmentTime.Enabled = False
        If wo.PalletsReceived < 1 Then
            numPallets.Text = ""
        Else
            numPallets.Text = wo.PalletsReceived.ToString
        End If
        numPallets.Enabled = False
        If wo.BadPallets < 0 Then
            txtBadPallets.Text = ""
        Else
            txtBadPallets.Text = wo.BadPallets.ToString
        End If
        txtBadPallets.Enabled = False
        If wo.Restacks < 0 Then
            txtRestacks.Text = ""
        Else
            txtRestacks.Text = wo.Restacks.ToString
        End If
        txtRestacks.Enabled = False
        If wo.Weight < 1 Then
            txtweight.Text = ""
        Else
            txtweight.Text = wo.Weight.ToString
        End If
        txtweight.Enabled = False
        If wo.NumberOfItems < 1 Then
            txtTotalItems.Text = ""
        Else
            txtTotalItems.Text = wo.NumberOfItems.ToString
        End If
        txtTotalItems.Enabled = False
        cbLoadType.SelectedValue = wo.LoadTypeID
        'cbLoadType.Enabled = False
        If wo.ReceiptNumber = 0 Then
            txtRecieptNumber.Text = ""
        Else
            txtRecieptNumber.Text = wo.ReceiptNumber.ToString
        End If
        txtRecieptNumber.Enabled = False
        rbCash.Checked = wo.PaymentType = "Cash"
        rbCash.Enabled = False
        rbCheck.Checked = wo.PaymentType = "Check"
        rbCheck.Enabled = False
        If rbCheck.Checked Then
            txtCheckNumber.Text = wo.CheckNumber
            txtCheckNumber.Enabled = True
            rbCheck.Enabled = False
        End If
        rbCard.Checked = wo.PaymentType = "Card"
        rbCard.Enabled = False
        rbsplit.Checked = wo.PaymentType = "Split"
        rbsplit.Enabled = False
        If rbsplit.Checked Then
            txtCheckNumber.Text = wo.CheckNumber
            txtCheckNumber.Enabled = True
            lblAddCash.Visible = True
            txtAddCash.Visible = True
            txtAddCash.Text = wo.SplitPaymentAmount
            txtAddCash.Enabled = True
        Else
            lblAddCash.Visible = False
            txtAddCash.Visible = False
            txtAddCash.Text = wo.SplitPaymentAmount
            txtAddCash.Enabled = False
        End If
        If IsNumeric(wo.Amount) Then
            txtAmount.Text = wo.Amount
        Else
            txtAmount.Text = ""
        End If
        txtAmount.Enabled = False
        txtBOLSEAL.Text = wo.BOL
        txtBOLSEAL.Enabled = False
        txtComments.Text = wo.Comments
        pageindex = +1
    End Sub 'populatePrintForm
    '*********************************************************************************************************

    Function buildPage4(ByRef wo As WorkOrder) As WorkOrder

        'Page4
        wo.IsCash = rbCash.Checked
        If IsNumeric(txtAmount.Text.Trim) Then
            wo.Amount = CType(txtAmount.Text.Trim, Decimal)
        Else
            'throw exception
            wo.Amount = Nothing
        End If
        If rbCash.Checked Then wo.PaymentType = "Cash"
        If rbCheck.Checked Then wo.PaymentType = "Check"
        If rbCard.Checked Then wo.PaymentType = "Card"
        If rbsplit.Checked Then
            wo.PaymentType = "Split"
            If IsNumeric(txtAddCash.Text.Trim) Then
                wo.SplitPaymentAmount = CType(txtAddCash.Text.Trim, Decimal)
            End If
        End If
        wo.CheckNumber = IIf(Not txtCheckNumber.Text Is Nothing, txtCheckNumber.Text, String.Empty)
        wo.BOL = IIf(Not txtBOLSEAL.Text Is Nothing, txtBOLSEAL.Text, String.Empty)
        wo.Comments = IIf(Not txtComments.Text Is Nothing, txtComments.Text, String.Empty)
        'wo.isClosed
        '        wo.LogDate = Date.Now
        Dim utl As New Utils
        wo.LocationID = New Guid(utl.getxmldata("Location_ID"))
        Dim pid As Guid = New Guid(utl.getxmldata("Parent_ID"))
        '        Dim vcustomerID As Guid = gimmeCustomerID(pid, txtVendor.Text)
        '        wo.CustomerID = vcustomerID
        wo.CustomerID = New Guid(utl.getCustomerID(utl.getxmldata("Parent_ID"), wo.VendorNumber))
        wo.PurchaseOrder = IIf(Not txtPurchaseOrder.Text Is Nothing, txtPurchaseOrder.Text, String.Empty)
        wo.CreatedBy = "Hand-Held"
        'Set Status***********************************************************************************************
        Dim status As LoadStatus = LoadStatus.Undefined
        Dim strpg1 As String = page1validate()
        If (strpg1 = "") Or (strpg1 = "" And txtVendor.Text > "" And txtVendorNumber.Text > "") Then
            status = LoadStatus.CheckedIn
        End If
        Return wo
    End Function

    '*********************************************************************************************************
    Function buildWorkOrder(ByRef wo As WorkOrder) As WorkOrder
        '*********************************************************************************************************
        'Page1
        wo.LoadNumber = txtLoadNumber.Text
        If wo.DoorNumber > "" And txtDoor.Text = "" Then
            MessageBox.Show("You can not delete door number", "Invalid Entry")
            TabControl1.SelectedIndex = 0
            txtDoor.Focus()
            wo.BOL = "Door Number Error"
            Return wo
        End If

        wo.DoorNumber = IIf(Not txtDoor.Text Is Nothing, txtDoor.Text, String.Empty)
        'door number starts docktime

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

        '*********************************************************************************************************
        'Page2
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

        '*********************************** **********************************************************************
        'Page3
        If numPallets.Text > "" Then
            wo.PalletsReceived = CType(numPallets.Text, Integer)
        End If
        Dim surnull As DateTime = "#1/1/1900 12:00AM#"
        If TimePickerAppointmentTime.Value > surnull Then
            Dim vappointmentTime As Date = Date.Now.ToShortDateString
            vappointmentTime = DateAdd(DateInterval.Hour, DatePart(DateInterval.Hour, TimePickerAppointmentTime.Value), vappointmentTime)
            vappointmentTime = DateAdd(DateInterval.Minute, DatePart(DateInterval.Minute, TimePickerAppointmentTime.Value), vappointmentTime)
            wo.AppointmentTime = vappointmentTime
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
        If IsNumeric(txtweight.Text) Then
            wo.Weight = CType(txtweight.Text, Integer)
        Else
            wo.Weight = -1
        End If
        If IsNumeric(txtTotalItems.Text) Then
            wo.NumberOfItems = CType(txtTotalItems.Text, Integer)
        Else
            wo.NumberOfItems = -1
        End If
        If cbLoadType.SelectedIndex > -1 Then
            wo.LoadTypeID = New Guid(cbLoadType.SelectedValue.ToString)
            wo.LoadType = cbLoadType.Text
            wo.IsCash = cbLoadType.Text = "Cash"
        Else
            wo.LoadTypeID = Nothing
            wo.LoadType = Nothing
        End If
        If IsNumeric(txtRecieptNumber.Text) Then
            wo.ReceiptNumber = CType(txtRecieptNumber.Text, Integer)
        End If

        '*********************************************************************************************************
        'Page4
        If IsNumeric(txtAmount.Text.Trim) Then
            wo.Amount = CType(txtAmount.Text.Trim, Decimal)
        Else
            'throw exception
            wo.Amount = Nothing
        End If
        If rbCash.Checked Then wo.PaymentType = "Cash"
        If rbCheck.Checked Then wo.PaymentType = "Check"
        If rbCard.Checked Then wo.PaymentType = "Card"
        If rbsplit.Checked Then
            wo.PaymentType = "Split"
            If IsNumeric(txtAddCash.Text.Trim) Then
                wo.SplitPaymentAmount = CType(txtAddCash.Text.Trim, Decimal)
            End If
        End If
        wo.CheckNumber = IIf(Not txtCheckNumber.Text Is Nothing, txtCheckNumber.Text, String.Empty)
        wo.BOL = IIf(Not txtBOLSEAL.Text Is Nothing, txtBOLSEAL.Text, String.Empty)
        wo.Comments = IIf(Not txtComments.Text Is Nothing, txtComments.Text, String.Empty)
        'wo.isClosed
        '        wo.LogDate = Date.Now
        Dim utl As New Utils
        wo.LocationID = New Guid(utl.getxmldata("Location_ID"))
        Dim pid As Guid = New Guid(utl.getxmldata("Parent_ID"))
        '        Dim vcustomerID As Guid = gimmeCustomerID(pid, txtVendor.Text)
        '        wo.CustomerID = vcustomerID
        wo.CustomerID = New Guid(utl.getCustomerID(utl.getxmldata("Parent_ID"), wo.VendorNumber))
        wo.PurchaseOrder = IIf(Not txtPurchaseOrder.Text Is Nothing, txtPurchaseOrder.Text, String.Empty)
        wo.CreatedBy = "Hand-Held"
        'Set Status***********************************************************************************************
        Dim status As LoadStatus = LoadStatus.Undefined
        If wo.Status < 76 Then

            Dim strpg1 As String = page1validate()
            If (strpg1 = "") Or (strpg1 = "" And txtVendor.Text > "" And txtVendorNumber.Text > "") Then
                status = LoadStatus.CheckedIn
            End If
            If page1validate() = "" And AddDataChanged() Then
                status = LoadStatus.CheckedIn Or LoadStatus.AddDataChanged
            End If

            If Not wo.Unloaders Is Nothing Then
                If wo.Unloaders.Count > 0 Then
                    status = LoadStatus.AddDataChanged Or LoadStatus.Assigned
                    status = LoadStatus.AddDataChanged Or LoadStatus.Assigned
                Else
                    '                wo.StartTime = "1/1/1900 12:00 AM"
                End If
            End If

            If wo.BadPallets > -1 And wo.Restacks > -1 Then
                If wo.CompTime = "1/1/1900 12:00 AM" Then
                    wo.CompTime = Date.Now()
                End If
                status = LoadStatus.AddDataChanged Or LoadStatus.Assigned Or LoadStatus.Complete
                If Not rbCash.Checked And Not rbCheck.Checked And Not rbCard.Checked And Not rbsplit.Checked And cbLoadType.Text <> "Invoice" Then
                    status = LoadStatus.AddDataChanged Or LoadStatus.Assigned Or LoadStatus.Complete Or LoadStatus.Printed
                End If
            Else
                Dim dat As Date = "1/1/1900 12:00 AM"
                wo.CompTime = dat.ToLongDateString
            End If
            wo.Status = status
        End If
        Return wo
    End Function 'buildWorkOrder
    '*********************************************************************************************************

    Private Function gimmeCustomerID(ByVal pid As Guid, ByVal cnum As String) As Guid
        Dim retguid As Guid = Nothing
        Dim a As New SqlCeAdapter("\Program Files\SEUdock\SEUdock.sdf")
        Dim stpid As String = pid.ToString
        Dim strSQL As String = "Select ID from Vendor where ParentCompanyID = '" & stpid & "' AND Number = '" & cnum & "'"
        Dim utl As New Utils
        Try
            stpid = a.QueryDatabaseForScalar(strSQL)
            retguid = New Guid(stpid)
        Catch ex As Exception
            retguid = utl.zeroGuid()
        End Try
        Dim cid As String = retguid.ToString
        Return retguid
    End Function


    '*********************************************************************************************************
    Private Sub loadAssignedUnloaders()
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
        Me.lbUnloaders.SelectedIndex = -1

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
        Or cbLoadDescription.SelectedIndex > 1 _
        Or cbLoadType.SelectedIndex > -1 _
        Or IsNumeric(txtAmount.Text) Then
            Return True
        Else
            Return False
        End If

    End Function

    '*********************************************************************************************************

    'allows for only numeric characters in handled controls***************************************************
    'Private Sub Numeric_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtLoadNumber.KeyPress, _
    'numPieces.KeyPress, txtBadPallets.KeyPress, txtRestacks.KeyPress, _
    'txtRecieptNumber.KeyPress, txtPalletsUnloaded.KeyPress, txtTotalItems.KeyPress, txtweight.KeyPress
    '    If IsNumeric(e.KeyChar) Or e.KeyChar = ControlChars.Back Then
    '        e.Handled = False
    '    Else
    '        e.Handled = True
    '    End If
    'End Sub
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
        If IsNumeric(txtBadPallets.Text) And IsNumeric(txtRestacks.Text) Then
            If txtDoor.Text.Trim() = "" Then
                retstr = "Door Number Required"
            End If
        End If
        If cbCarrier.SelectedIndex < 0 And cbCarrier.Text = "" Then
            retstr = retstr & vbCrLf & "Carrier Name Required"
        End If
        If txtTruckNumber.Text = "" Then
            retstr = retstr & vbCrLf & "Truck Number Required"
        End If
        If txtTrailerNumber.Text = "" Then
            retstr = retstr & vbCrLf & "Trailer Number Required"
        End If
        If txtPurchaseOrder.Text = "" Then
            retstr = retstr & vbCrLf & "Purchase Order Required"
        End If
        If cbDepartment.SelectedIndex < 0 Or cbDepartment.Text = "" Then
            retstr = retstr & vbCrLf & "Department Required"
        End If
        Return retstr
    End Function
    '*********************************************************************************************************

    '*********************************************************************************************************
    Private Function page2validate() As String
        Dim retstr As String = String.Empty
        '        If txtVendorNumber.Text = "" Then
        '            retstr = retstr & vbCrLf & "Vendor # Required"
        '        End If
        If txtVendor.Text = "" Then
            retstr = retstr & vbCrLf & "Vendor Name Required"
        End If
        If numPieces.Text = "" Then
            retstr = retstr & vbCrLf & "# Pieces Required"
        End If
        If numPieces.Text = "" Then
            retstr = retstr & vbCrLf & "# Pallets Required"
        End If
        If cbLoadDescription.Text = "" Then
            retstr = retstr & vbCrLf & "Load Description Required"
        End If
        If lbUnloaders.Items.Count < 1 Then
            retstr = retstr & vbCrLf & "Select Unloaders"
        End If
        Return retstr
    End Function
    '*********************************************************************************************************

    Private Sub txtBadPallets_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtBadPallets.GotFocus
        '    Dim a As String = "234"
        '    a = "33d"

    End Sub

    '*********************************************************************************************************
    Private Function page3validate() As String
        Dim retstr As String = String.Empty


        If cbLoadDescription.SelectedValue.ToString <> "1abfabfd-dfd9-4264-a51a-61d1316d91c5" Then
            If txtPalletsUnloaded.Text = "" Then
                retstr = retstr & vbCrLf & " # Pallets Unloaded Required"
            End If
        End If
        If TimePickerAppointmentTime.Text = "" Then
            retstr = retstr & vbCrLf & ""
        End If
        '        If DateTimePickerGateTime.Text = "" Then
        '        retstr = retstr & vbCrLf & ""
        '        End If
        If txtBadPallets.Text = "" Or txtRestacks.Text = "" Then
            If txtBadPallets.Text = "" Then
                retstr = retstr & vbCrLf & "Bad Pallets AND Restacks Required"
            Else
                retstr = retstr & vbCrLf & "Restacks AND Bad Pallets Required"
            End If
        End If
        If txtweight.Text = "" Then
            retstr = retstr & vbCrLf & "Weight Required"
        End If
        If txtTotalItems.Text = "" Then
            retstr = retstr & vbCrLf & "Total Items Required"
        End If
        If cbLoadType.SelectedIndex < 0 Or cbLoadType.Text = "" Then
            retstr = retstr & vbCrLf & "Load Type Required"
        End If
        Return retstr
    End Function
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
        And cbLoadType.SelectedValue.ToString <> "0369f50a-52ca-4c97-8323-650adc182e04" _
        And cbLoadType.SelectedValue.ToString <> "c150f229-91aa-433c-8180-3d5a7d4b52f4" _
        And cbLoadType.SelectedValue.ToString <> "55acef39-f005-4a6b-8ae9-80c2df9dcbb6" _
        And cbLoadType.SelectedValue.ToString <> "f9d8fade-56db-4b46-b183-d7cce538270f" Then

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
    End Function
    '*********************************************************************************************************

#End Region 'Validation
    '*********************************************************************************************************
    '*********************************************************************************************************

    '*********************************************************************************************************
    '*********************************************************************************************************
#Region "Tabs Control Events"

    Public pageindex As Integer = 0

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
#Region "Tabs Page 1"

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
        If curwo.DoorNumber Is Nothing Or curwo.DoorNumber = String.Empty Then 'workorder never had doornumber
            If txtDoor.Text > "" Then   'is there a number now
                curwo.DoorNumber = txtDoor.Text.Trim() 'store it
                curwo.DockTime = Date.Now 'set DockTime
                curwo.LogDate = Date.Now

            End If
        Else 'changing doornumber
            If txtDoor.Text.Trim = "" Then
                Dim doorerror As String = chkDoorNumber(curwo)
                If doorerror > String.Empty Then
                    MessageBox.Show(doorerror, "Invalid Door Number")
                    TabControl1.SelectedIndex = 0
                    txtDoor.Focus()
                    Exit Sub
                End If
            End If
            curwo.DoorNumber = txtDoor.Text.Trim()
        End If
        btnAddUnloaders.Enabled = txtDoor.Text > ""
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

    '*********************************************************************************************************
    '*********************************************************************************************************

#End Region 'Tabs Page 1
    '*********************************************************************************************************
    '*********************************************************************************************************

    '*********************************************************************************************************
    '*********************************************************************************************************
#Region "Tabs Control Events Page 2"

    '*********************************************************************************************************
    Private Sub txtVendorNumber_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtVendorNumber.TextChanged
        If txtVendorNumber.Text.Length > 1 Then
            If pageindex > 0 Then ischanged = True
            Dim a As New SqlCeAdapter("\Program Files\SEUdock\SEUdock.sdf")
            Dim Vendor As DataTable = New DataTable
            Dim utl As New Utils
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
        If pageindex > 0 Then ischanged = True
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
    Private Sub btnAddUnloaders_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddUnloaders.Click
        Dim f As New SelectUnloader
        curwo.DoorNumber = txtDoor.Text
        curwo = buildWorkOrder(curwo)
        f.isEdit = True
        f.cwo = curwo
        f.Show()
        Me.Close()
    End Sub 'btnAddUnloaders_Click
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

#End Region 'Tabs Control Events Page2
    '*********************************************************************************************************
    '*********************************************************************************************************

    '*********************************************************************************************************
    '*********************************************************************************************************
#Region "Tabs Control Events Page 3"

    '*********************************************************************************************************
    Private Sub cbLoadType_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbLoadType.SelectedIndexChanged
        If cbLoadType.SelectedIndex > -1 Then
            If pageindex > 0 Then ischanged = True
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
                    txtAddCash.Text = ""
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
                    lblCheckNumber.Text = "Check/Transaction Number"
                    lblCheckNumber.Enabled = False
                    txtCheckNumber.Text = ""
                    txtCheckNumber.Enabled = True
            End Select
        End If
    End Sub 'cbLoadType_SelectedIndexChanged
    '*********************************************************************************************************

#End Region 'Tabs Control Events Page3
    '*********************************************************************************************************
    '*********************************************************************************************************

    '*********************************************************************************************************
    '*********************************************************************************************************
#Region "Tabs Control Events Page 4"

    '*********************************************************************************************************
    Private Sub txtAmount_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtAmount.GotFocus
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
            MessageBox.Show("This Amount and the PO# are same, Intentional?", "Possible Data Enty Error")
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
            txtCheckNumber.Text = String.Empty
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
    End Sub 'comboBoxes
    '*********************************************************************************************************

#End Region 'Load comboBoxes boxes
    '*********************************************************************************************************
    '*********************************************************************************************************

    '*********************************************************************************************************
    '*********************************************************************************************************
#Region "Buttons"
    Private Function chkDoorNumber(ByVal wo As WorkOrder) As String
        Dim retstr As String = String.Empty
        If wo.DoorNumber > "" And txtDoor.Text = "" Then
            retstr = "You can NOT remove door number"
        End If
        Return retstr
    End Function
    '*********************************************************************************************************
    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit4.Click, btnExit3.Click, btnExit2.Click, btnExit1.Click
        'Save load info
        If ischanged Then
            If IsNumeric(txtBadPallets.Text) And Not IsNumeric(txtRestacks.Text) Then
                MessageBox.Show("If you enter Bad Pallets you MUST also enter Restacks (0 is valid)", "Restacks REQUIRED!")
                TabControl1.SelectedIndex = 2
                txtRestacks.Focus()
                Exit Sub
            ElseIf Not IsNumeric(txtBadPallets.Text) And IsNumeric(txtRestacks.Text) Then
                MessageBox.Show("If you enter Restacks you MUST also enter Bad Pallets (0 is valid)", "BadPallets REQUIRED!")
                TabControl1.SelectedIndex = 2
                txtBadPallets.Focus()
                Exit Sub
            End If
            Dim validate1 As String = page1validate()
            If IsNumeric(txtBadPallets.Text) And IsNumeric(txtRestacks.Text) Then
                If validate1 > "" Then
                    MessageBox.Show(validate1, "Page 1 Required Fields")
                    TabControl1.SelectedIndex = 0
                    Exit Sub
                End If
                Dim validate2 As String = page2validate()
                If validate2 > "" Then
                    MessageBox.Show(validate2, "Page 2 Required Fields")
                    TabControl1.SelectedIndex = 1
                    Exit Sub
                End If
                Dim validate3 As String = page3validate()
                If validate3 > "" Then
                    MessageBox.Show(validate3, "Page 3 Required Fields")
                    TabControl1.SelectedIndex = 2
                    Exit Sub
                End If
                Dim validate4 As String = page4validate()
                If validate4 > "" Then
                    MessageBox.Show(validate4, "Page 4 Required Fields")
                    TabControl1.SelectedIndex = 3
                    Exit Sub
                End If
            End If

            Dim result1 As DialogResult = MessageBox.Show("Save Changes?", "Important Question", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
            If result1 = Windows.Forms.DialogResult.Yes Then
                curwo = buildWorkOrder(curwo)
                Dim wodal As New WorkOrderDAL
                Dim suc As String = wodal.UpdateWorkOrder(curwo)
                '                If Utils.Connected Then
                '                asyncConn.syncme()
                '            End If

                Dim f As LoadList = New LoadList
                f.eid = eid
                f.dups = dups
                f.Show()
                Me.Close()

            ElseIf result1 = Windows.Forms.DialogResult.No Then
                Dim f As LoadList = New LoadList
                f.eid = eid
                f.dups = dups
                f.Show()
                Me.Close()
            ElseIf result1 = Windows.Forms.DialogResult.Cancel Then
                Exit Sub
            End If
        Else
            Dim f As LoadList = New LoadList
            f.eid = eid
            f.dups = dups
            f.Show()
            Me.Close()
        End If
    End Sub 'btnExit_Click
    '*********************************************************************************************************

    '*********************************************************************************************************
    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        curwo.isClosed = True
        buildWorkOrder(curwo)
        curwo.Status = 76
        Dim wdal As New WorkOrderDAL
        wdal.UpdateWorkOrder(curwo)
        Dim pr As New printReceipt
        pr.PrintReciept(curwo)
        Dim f As LoadList = New LoadList
        f.eid = eid
        f.dups = dups
        f.Show()
        Me.Close()
    End Sub 'btnPrint_Click        
    '*********************************************************************************************************

    '*********************************************************************************************************
    '*********************************************************************************************************
#Region "Clear Buttons"

    '*********************************************************************************************************
    Private Sub btnClearPage1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearPage1.Click
        ischanged = True
        txtLoadNumber.Text = ""
        txtDoor.Text = ""
        cbCarrier.SelectedIndex = -1
        txtTruckNumber.Text = ""
        txtTrailerNumber.Text = ""
        txtPurchaseOrder.Text = ""
        cbDepartment.SelectedIndex = -1
    End Sub 'btnClearPage1_Click
    '*********************************************************************************************************

    '*********************************************************************************************************
    Private Sub btnClearPage2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearPage2.Click
        ischanged = True
        txtVendorNumber.Text = ""
        txtVendor.Text = ""
        numPieces.Text = ""
        numPieces.Text = ""
        cbLoadDescription.SelectedIndex = -1
        lbUnloaders.Text = ""
    End Sub 'btnClearPage2_Click
    '*********************************************************************************************************

    '*********************************************************************************************************
    Private Sub btnClearPage3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearPage3.Click
        ischanged = True
        txtPalletsUnloaded.Text = ""
        TimePickerAppointmentTime.Value = Date.Now
        DateTimePickerGateTime.Value = Date.Now
        txtBadPallets.Text = ""
        txtweight.Text = ""
        txtRestacks.Text = ""
        txtTotalItems.Text = ""
        cbLoadType.SelectedIndex = -1
    End Sub 'btnClearPage3_Click
    '*********************************************************************************************************

    '*********************************************************************************************************
    Private Sub btnClearPage4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearPage4.Click, btnClearPage42.Click
        ischanged = True
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
    Private Sub CtrlConn1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim f As asyncConn = New asyncConn
        'me.Parent.SendToBack()
        f.Show()
    End Sub 'CtrlConn1_Click
    '*********************************************************************************************************

#End Region 'Buttons
    '*********************************************************************************************************
    '*********************************************************************************************************

    '*********************************************************************************************************
    '*********************************************************************************************************
#Region "ischanged"

    'Page 1*********************************************************************************************************
    Private Sub page1_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtLoadNumber.TextChanged, _
    txtDoor.TextChanged, cbCarrier.TextChanged, txtTruckNumber.TextChanged, txtTrailerNumber.TextChanged, txtPurchaseOrder.TextChanged
        If pageindex > 0 Then ischanged = True
    End Sub 'page1_TextChanged
    '*********************************************************************************************************

    '*********************************************************************************************************
    Private Sub cbDepartment_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbDepartment.SelectedIndexChanged
        If pageindex > 0 Then ischanged = True
    End Sub 'cbDepartment_SelectedIndexChanged
    '*********************************************************************************************************

    'Page2*********************************************************************************************************
    Private Sub page2_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtVendor.TextChanged, _
     numPieces.TextChanged, txtPalletsUnloaded.TextChanged
        If pageindex > 0 Then ischanged = True
    End Sub 'page2_TextChanged
    '*********************************************************************************************************

    '*********************************************************************************************************
    Private Sub lbUnloaders_DataSourceChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbUnloaders.DataSourceChanged
        If curwo.Status < 73 Then
            If pageindex > 0 Then
                ischanged = True
            End If
        End If
    End Sub 'lbUnloaders_DataSourceChanged
    '*********************************************************************************************************

    'Page 3 *****************************************************************************************************
    Private Sub page3_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        If pageindex > 0 Then ischanged = True
    End Sub 'page3_ValueChanged
    '*********************************************************************************************************

    '*********************************************************************************************************
    Private Sub page3_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lblRecieptNum.TextChanged, txtBadPallets.TextChanged, txtRestacks.TextChanged, numPallets.TextChanged, txtTotalItems.TextChanged, txtweight.TextChanged
        If pageindex > 0 Then ischanged = True
    End Sub

    Private Sub txtAddCash_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtAddCash.LostFocus
        If txtAddCash.Text = "" And txtCheckNumber.Text > "" Then
            rbsplit.Checked = False
            rbsplit.Enabled = True
            rbCheck.Checked = True

        ElseIf txtAddCash.Text = "" And txtCheckNumber.Text = "" Then
            rbsplit.Checked = False
            rbsplit.Enabled = True
            rbCash.Checked = True
            rbCash.Enabled = True
            rbCheck.Enabled = True
            txtCheckNumber.Enabled = True
        End If
    End Sub 'page3_TextChanged
    '*********************************************************************************************************

    'Page 4*********************************************************************************************************
    Private Sub page4_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtAmount.TextChanged, _
    txtCheckNumber.TextChanged, txtAddCash.TextChanged, txtBOLSEAL.TextChanged, txtComments.TextChanged
        If pageindex > 0 Then ischanged = True
    End Sub 'page4_TextChanged
    '*********************************************************************************************************

#End Region 'ischanged
    '*********************************************************************************************************
    '*********************************************************************************************************

    Private Sub TimePickerAppointmentTime_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TimePickerAppointmentTime.ValueChanged
        ischanged = True
    End Sub
End Class

Public Class NotListed
    Private mText As String
    Private mValue As String
    Public Sub New(ByVal NAME As String, ByVal ID As String)
        mText = NAME
        mValue = ID
    End Sub

    Public ReadOnly Property Name() As String
        Get
            Return mText
        End Get
    End Property

    Public ReadOnly Property ID() As String
        Get
            Return mValue
        End Get
    End Property

    Public Overrides Function ToString() As String
        Return mText
    End Function
End Class