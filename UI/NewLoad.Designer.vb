<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class NewLoad
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.TabControl1 = New System.Windows.Forms.TabControl
        Me.TabPage1 = New System.Windows.Forms.TabPage
        Me.cbCarrier = New System.Windows.Forms.ComboBox
        Me.txtTrailerNumber = New System.Windows.Forms.TextBox
        Me.cbDepartment = New System.Windows.Forms.ComboBox
        Me.txtDoor = New System.Windows.Forms.TextBox
        Me.txtLoadNumber = New System.Windows.Forms.TextBox
        Me.txtTruckNumber = New System.Windows.Forms.TextBox
        Me.txtPurchaseOrder = New System.Windows.Forms.TextBox
        Me.lblTrailerNum = New System.Windows.Forms.Label
        Me.lblDepartment = New System.Windows.Forms.Label
        Me.lblDoor = New System.Windows.Forms.Label
        Me.lblLoadNumber = New System.Windows.Forms.Label
        Me.lblCarrier = New System.Windows.Forms.Label
        Me.lblTruckNum = New System.Windows.Forms.Label
        Me.lblPurchaseOrder = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.btnClearPage1 = New System.Windows.Forms.Button
        Me.btnExit1 = New System.Windows.Forms.Button
        Me.TabPage2 = New System.Windows.Forms.TabPage
        Me.txtPalletsUnloaded = New System.Windows.Forms.TextBox
        Me.lblPalletsUnloaded = New System.Windows.Forms.Label
        Me.lbUnloaders = New System.Windows.Forms.ListBox
        Me.cbLoadDescription = New System.Windows.Forms.ComboBox
        Me.numPieces = New System.Windows.Forms.TextBox
        Me.txtVendor = New System.Windows.Forms.TextBox
        Me.txtVendorNumber = New System.Windows.Forms.TextBox
        Me.lblLoadDescription = New System.Windows.Forms.Label
        Me.btnDelUnloader = New System.Windows.Forms.Button
        Me.btnAddUnloader = New System.Windows.Forms.Button
        Me.lblVendor = New System.Windows.Forms.Label
        Me.lblVendorNum = New System.Windows.Forms.Label
        Me.btnClearPage2 = New System.Windows.Forms.Button
        Me.btnExit2 = New System.Windows.Forms.Button
        Me.lblPieces = New System.Windows.Forms.Label
        Me.TabPage3 = New System.Windows.Forms.TabPage
        Me.txtTotalItems = New System.Windows.Forms.TextBox
        Me.txtWeight = New System.Windows.Forms.TextBox
        Me.numPallets = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.txtRestacks = New System.Windows.Forms.TextBox
        Me.txtBadPallets = New System.Windows.Forms.TextBox
        Me.txtRecieptNumber = New System.Windows.Forms.TextBox
        Me.lblRecieptNum = New System.Windows.Forms.Label
        Me.cbLoadType = New System.Windows.Forms.ComboBox
        Me.lblLoadType = New System.Windows.Forms.Label
        Me.lblTotalItems = New System.Windows.Forms.Label
        Me.lblRestacks = New System.Windows.Forms.Label
        Me.lblWeight = New System.Windows.Forms.Label
        Me.lblBadPallets = New System.Windows.Forms.Label
        Me.lblGateTime = New System.Windows.Forms.Label
        Me.lblApptTime = New System.Windows.Forms.Label
        Me.btnClearPage3 = New System.Windows.Forms.Button
        Me.btnExit3 = New System.Windows.Forms.Button
        Me.DateTimePickerGateTime = New System.Windows.Forms.DateTimePicker
        Me.TabPage4 = New System.Windows.Forms.TabPage
        Me.txtAddCash = New System.Windows.Forms.TextBox
        Me.rbsplit = New System.Windows.Forms.RadioButton
        Me.txtBOLSEAL = New System.Windows.Forms.TextBox
        Me.txtCheckNumber = New System.Windows.Forms.TextBox
        Me.txtAmount = New System.Windows.Forms.TextBox
        Me.txtComments = New System.Windows.Forms.TextBox
        Me.rbCard = New System.Windows.Forms.RadioButton
        Me.rbCheck = New System.Windows.Forms.RadioButton
        Me.rbCash = New System.Windows.Forms.RadioButton
        Me.lblAmount = New System.Windows.Forms.Label
        Me.lblCheckNumber = New System.Windows.Forms.Label
        Me.lblBOLSEAL = New System.Windows.Forms.Label
        Me.lblComments = New System.Windows.Forms.Label
        Me.btnClearPage4 = New System.Windows.Forms.Button
        Me.btnExit4 = New System.Windows.Forms.Button
        Me.lblAddCash = New System.Windows.Forms.Label
        Me.myListBox = New SEUdock.FontListBox
        Me.TimePickerAppointmentTime = New System.Windows.Forms.DateTimePicker
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.TabPage3.SuspendLayout()
        Me.TabPage4.SuspendLayout()
        Me.SuspendLayout()
        '
        'TabControl1
        '
        Me.TabControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Controls.Add(Me.TabPage3)
        Me.TabControl1.Controls.Add(Me.TabPage4)
        Me.TabControl1.Dock = System.Windows.Forms.DockStyle.None
        Me.TabControl1.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Regular)
        Me.TabControl1.Location = New System.Drawing.Point(0, 0)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(240, 240)
        Me.TabControl1.TabIndex = 8
        '
        'TabPage1
        '
        Me.TabPage1.AutoScroll = True
        Me.TabPage1.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TabPage1.Controls.Add(Me.cbCarrier)
        Me.TabPage1.Controls.Add(Me.txtTrailerNumber)
        Me.TabPage1.Controls.Add(Me.cbDepartment)
        Me.TabPage1.Controls.Add(Me.txtDoor)
        Me.TabPage1.Controls.Add(Me.txtLoadNumber)
        Me.TabPage1.Controls.Add(Me.txtTruckNumber)
        Me.TabPage1.Controls.Add(Me.txtPurchaseOrder)
        Me.TabPage1.Controls.Add(Me.lblTrailerNum)
        Me.TabPage1.Controls.Add(Me.lblDepartment)
        Me.TabPage1.Controls.Add(Me.lblDoor)
        Me.TabPage1.Controls.Add(Me.lblLoadNumber)
        Me.TabPage1.Controls.Add(Me.lblCarrier)
        Me.TabPage1.Controls.Add(Me.lblTruckNum)
        Me.TabPage1.Controls.Add(Me.lblPurchaseOrder)
        Me.TabPage1.Controls.Add(Me.Label3)
        Me.TabPage1.Controls.Add(Me.btnClearPage1)
        Me.TabPage1.Controls.Add(Me.btnExit1)
        Me.TabPage1.Location = New System.Drawing.Point(0, 0)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Size = New System.Drawing.Size(240, 218)
        Me.TabPage1.Text = "Page  1"
        '
        'cbCarrier
        '
        Me.cbCarrier.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cbCarrier.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown
        Me.cbCarrier.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Regular)
        Me.cbCarrier.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cbCarrier.Location = New System.Drawing.Point(5, 83)
        Me.cbCarrier.Name = "cbCarrier"
        Me.cbCarrier.Size = New System.Drawing.Size(230, 19)
        Me.cbCarrier.TabIndex = 4
        '
        'txtTrailerNumber
        '
        Me.txtTrailerNumber.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtTrailerNumber.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Regular)
        Me.txtTrailerNumber.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtTrailerNumber.Location = New System.Drawing.Point(121, 124)
        Me.txtTrailerNumber.Name = "txtTrailerNumber"
        Me.txtTrailerNumber.Size = New System.Drawing.Size(114, 18)
        Me.txtTrailerNumber.TabIndex = 8
        '
        'cbDepartment
        '
        Me.cbDepartment.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cbDepartment.DisplayMember = "ID"
        Me.cbDepartment.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Regular)
        Me.cbDepartment.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cbDepartment.Location = New System.Drawing.Point(121, 166)
        Me.cbDepartment.Name = "cbDepartment"
        Me.cbDepartment.Size = New System.Drawing.Size(114, 19)
        Me.cbDepartment.TabIndex = 12
        Me.cbDepartment.ValueMember = "ID"
        '
        'txtDoor
        '
        Me.txtDoor.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtDoor.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Regular)
        Me.txtDoor.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtDoor.Location = New System.Drawing.Point(166, 44)
        Me.txtDoor.Name = "txtDoor"
        Me.txtDoor.Size = New System.Drawing.Size(69, 18)
        Me.txtDoor.TabIndex = 2
        '
        'txtLoadNumber
        '
        Me.txtLoadNumber.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtLoadNumber.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Regular)
        Me.txtLoadNumber.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtLoadNumber.Location = New System.Drawing.Point(5, 44)
        Me.txtLoadNumber.Name = "txtLoadNumber"
        Me.txtLoadNumber.Size = New System.Drawing.Size(99, 18)
        Me.txtLoadNumber.TabIndex = 1
        Me.txtLoadNumber.TabStop = False
        '
        'txtTruckNumber
        '
        Me.txtTruckNumber.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtTruckNumber.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Regular)
        Me.txtTruckNumber.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtTruckNumber.Location = New System.Drawing.Point(5, 124)
        Me.txtTruckNumber.Name = "txtTruckNumber"
        Me.txtTruckNumber.Size = New System.Drawing.Size(99, 18)
        Me.txtTruckNumber.TabIndex = 6
        '
        'txtPurchaseOrder
        '
        Me.txtPurchaseOrder.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtPurchaseOrder.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Regular)
        Me.txtPurchaseOrder.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtPurchaseOrder.Location = New System.Drawing.Point(5, 166)
        Me.txtPurchaseOrder.Name = "txtPurchaseOrder"
        Me.txtPurchaseOrder.Size = New System.Drawing.Size(99, 18)
        Me.txtPurchaseOrder.TabIndex = 10
        '
        'lblTrailerNum
        '
        Me.lblTrailerNum.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Bold)
        Me.lblTrailerNum.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTrailerNum.Location = New System.Drawing.Point(123, 113)
        Me.lblTrailerNum.Name = "lblTrailerNum"
        Me.lblTrailerNum.Size = New System.Drawing.Size(51, 12)
        Me.lblTrailerNum.Text = "Trailer #"
        '
        'lblDepartment
        '
        Me.lblDepartment.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Bold)
        Me.lblDepartment.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblDepartment.Location = New System.Drawing.Point(123, 154)
        Me.lblDepartment.Name = "lblDepartment"
        Me.lblDepartment.Size = New System.Drawing.Size(70, 12)
        Me.lblDepartment.Text = "Department"
        '
        'lblDoor
        '
        Me.lblDoor.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Bold)
        Me.lblDoor.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblDoor.Location = New System.Drawing.Point(168, 32)
        Me.lblDoor.Name = "lblDoor"
        Me.lblDoor.Size = New System.Drawing.Size(31, 12)
        Me.lblDoor.Text = "Door"
        '
        'lblLoadNumber
        '
        Me.lblLoadNumber.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Bold)
        Me.lblLoadNumber.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblLoadNumber.Location = New System.Drawing.Point(7, 32)
        Me.lblLoadNumber.Name = "lblLoadNumber"
        Me.lblLoadNumber.Size = New System.Drawing.Size(49, 12)
        Me.lblLoadNumber.Text = "Load#"
        '
        'lblCarrier
        '
        Me.lblCarrier.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Bold)
        Me.lblCarrier.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblCarrier.Location = New System.Drawing.Point(7, 70)
        Me.lblCarrier.Name = "lblCarrier"
        Me.lblCarrier.Size = New System.Drawing.Size(42, 12)
        Me.lblCarrier.Text = "Carrier"
        '
        'lblTruckNum
        '
        Me.lblTruckNum.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Bold)
        Me.lblTruckNum.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTruckNum.Location = New System.Drawing.Point(7, 113)
        Me.lblTruckNum.Name = "lblTruckNum"
        Me.lblTruckNum.Size = New System.Drawing.Size(43, 12)
        Me.lblTruckNum.Text = "Truck #"
        '
        'lblPurchaseOrder
        '
        Me.lblPurchaseOrder.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Bold)
        Me.lblPurchaseOrder.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblPurchaseOrder.Location = New System.Drawing.Point(7, 154)
        Me.lblPurchaseOrder.Name = "lblPurchaseOrder"
        Me.lblPurchaseOrder.Size = New System.Drawing.Size(87, 12)
        Me.lblPurchaseOrder.Text = "Purchase Order"
        '
        'Label3
        '
        Me.Label3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label3.Location = New System.Drawing.Point(3, -38)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(100, 20)
        Me.Label3.Text = "Purchase Order"
        '
        'btnClearPage1
        '
        Me.btnClearPage1.BackColor = System.Drawing.Color.FromArgb(CType(CType(201, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(233, Byte), Integer))
        Me.btnClearPage1.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.btnClearPage1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnClearPage1.Location = New System.Drawing.Point(163, 199)
        Me.btnClearPage1.Name = "btnClearPage1"
        Me.btnClearPage1.Size = New System.Drawing.Size(72, 19)
        Me.btnClearPage1.TabIndex = 0
        Me.btnClearPage1.TabStop = False
        Me.btnClearPage1.Text = "Clear Page"
        '
        'btnExit1
        '
        Me.btnExit1.BackColor = System.Drawing.Color.FromArgb(CType(CType(201, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(233, Byte), Integer))
        Me.btnExit1.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.btnExit1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnExit1.Location = New System.Drawing.Point(5, 199)
        Me.btnExit1.Name = "btnExit1"
        Me.btnExit1.Size = New System.Drawing.Size(72, 19)
        Me.btnExit1.TabIndex = 0
        Me.btnExit1.TabStop = False
        Me.btnExit1.Text = "Exit"
        '
        'TabPage2
        '
        Me.TabPage2.AutoScroll = True
        Me.TabPage2.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TabPage2.Controls.Add(Me.txtPalletsUnloaded)
        Me.TabPage2.Controls.Add(Me.lblPalletsUnloaded)
        Me.TabPage2.Controls.Add(Me.lbUnloaders)
        Me.TabPage2.Controls.Add(Me.cbLoadDescription)
        Me.TabPage2.Controls.Add(Me.numPieces)
        Me.TabPage2.Controls.Add(Me.txtVendor)
        Me.TabPage2.Controls.Add(Me.txtVendorNumber)
        Me.TabPage2.Controls.Add(Me.lblLoadDescription)
        Me.TabPage2.Controls.Add(Me.btnDelUnloader)
        Me.TabPage2.Controls.Add(Me.btnAddUnloader)
        Me.TabPage2.Controls.Add(Me.lblVendor)
        Me.TabPage2.Controls.Add(Me.lblVendorNum)
        Me.TabPage2.Controls.Add(Me.btnClearPage2)
        Me.TabPage2.Controls.Add(Me.btnExit2)
        Me.TabPage2.Controls.Add(Me.lblPieces)
        Me.TabPage2.Location = New System.Drawing.Point(0, 0)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Size = New System.Drawing.Size(232, 216)
        Me.TabPage2.Text = "Page 2"
        '
        'txtPalletsUnloaded
        '
        Me.txtPalletsUnloaded.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtPalletsUnloaded.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Regular)
        Me.txtPalletsUnloaded.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtPalletsUnloaded.Location = New System.Drawing.Point(95, 61)
        Me.txtPalletsUnloaded.Name = "txtPalletsUnloaded"
        Me.txtPalletsUnloaded.Size = New System.Drawing.Size(42, 18)
        Me.txtPalletsUnloaded.TabIndex = 148
        '
        'lblPalletsUnloaded
        '
        Me.lblPalletsUnloaded.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Bold)
        Me.lblPalletsUnloaded.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblPalletsUnloaded.Location = New System.Drawing.Point(8, 65)
        Me.lblPalletsUnloaded.Name = "lblPalletsUnloaded"
        Me.lblPalletsUnloaded.Size = New System.Drawing.Size(88, 12)
        Me.lblPalletsUnloaded.Text = "Pallets Unloaded"
        '
        'lbUnloaders
        '
        Me.lbUnloaders.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.lbUnloaders.Location = New System.Drawing.Point(5, 115)
        Me.lbUnloaders.Name = "lbUnloaders"
        Me.lbUnloaders.Size = New System.Drawing.Size(186, 80)
        Me.lbUnloaders.TabIndex = 135
        '
        'cbLoadDescription
        '
        Me.cbLoadDescription.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cbLoadDescription.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Regular)
        Me.cbLoadDescription.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cbLoadDescription.Location = New System.Drawing.Point(5, 93)
        Me.cbLoadDescription.Name = "cbLoadDescription"
        Me.cbLoadDescription.Size = New System.Drawing.Size(230, 19)
        Me.cbLoadDescription.TabIndex = 129
        '
        'numPieces
        '
        Me.numPieces.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.numPieces.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Regular)
        Me.numPieces.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numPieces.Location = New System.Drawing.Point(182, 62)
        Me.numPieces.Name = "numPieces"
        Me.numPieces.Size = New System.Drawing.Size(53, 18)
        Me.numPieces.TabIndex = 81
        '
        'txtVendor
        '
        Me.txtVendor.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtVendor.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.txtVendor.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtVendor.Location = New System.Drawing.Point(44, 39)
        Me.txtVendor.Name = "txtVendor"
        Me.txtVendor.Size = New System.Drawing.Size(191, 19)
        Me.txtVendor.TabIndex = 44
        '
        'txtVendorNumber
        '
        Me.txtVendorNumber.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtVendorNumber.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Regular)
        Me.txtVendorNumber.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtVendorNumber.Location = New System.Drawing.Point(85, 18)
        Me.txtVendorNumber.Name = "txtVendorNumber"
        Me.txtVendorNumber.Size = New System.Drawing.Size(82, 18)
        Me.txtVendorNumber.TabIndex = 43
        '
        'lblLoadDescription
        '
        Me.lblLoadDescription.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Bold)
        Me.lblLoadDescription.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblLoadDescription.Location = New System.Drawing.Point(7, 81)
        Me.lblLoadDescription.Name = "lblLoadDescription"
        Me.lblLoadDescription.Size = New System.Drawing.Size(160, 12)
        Me.lblLoadDescription.Text = "Load Description/Unloader List"
        '
        'btnDelUnloader
        '
        Me.btnDelUnloader.BackColor = System.Drawing.Color.FromArgb(CType(CType(201, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(233, Byte), Integer))
        Me.btnDelUnloader.Enabled = False
        Me.btnDelUnloader.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.btnDelUnloader.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnDelUnloader.Location = New System.Drawing.Point(197, 157)
        Me.btnDelUnloader.Name = "btnDelUnloader"
        Me.btnDelUnloader.Size = New System.Drawing.Size(38, 37)
        Me.btnDelUnloader.TabIndex = 50
        Me.btnDelUnloader.Text = "del"
        '
        'btnAddUnloader
        '
        Me.btnAddUnloader.BackColor = System.Drawing.Color.FromArgb(CType(CType(201, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(233, Byte), Integer))
        Me.btnAddUnloader.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.btnAddUnloader.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnAddUnloader.Location = New System.Drawing.Point(197, 116)
        Me.btnAddUnloader.Name = "btnAddUnloader"
        Me.btnAddUnloader.Size = New System.Drawing.Size(38, 38)
        Me.btnAddUnloader.TabIndex = 49
        Me.btnAddUnloader.Text = "add"
        '
        'lblVendor
        '
        Me.lblVendor.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Bold)
        Me.lblVendor.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblVendor.Location = New System.Drawing.Point(8, 44)
        Me.lblVendor.Name = "lblVendor"
        Me.lblVendor.Size = New System.Drawing.Size(45, 20)
        Me.lblVendor.Text = "Vendor"
        '
        'lblVendorNum
        '
        Me.lblVendorNum.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Bold)
        Me.lblVendorNum.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblVendorNum.Location = New System.Drawing.Point(8, 21)
        Me.lblVendorNum.Name = "lblVendorNum"
        Me.lblVendorNum.Size = New System.Drawing.Size(84, 20)
        Me.lblVendorNum.Text = "Vendor Number"
        '
        'btnClearPage2
        '
        Me.btnClearPage2.BackColor = System.Drawing.Color.FromArgb(CType(CType(201, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(233, Byte), Integer))
        Me.btnClearPage2.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.btnClearPage2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnClearPage2.Location = New System.Drawing.Point(163, 199)
        Me.btnClearPage2.Name = "btnClearPage2"
        Me.btnClearPage2.Size = New System.Drawing.Size(72, 19)
        Me.btnClearPage2.TabIndex = 29
        Me.btnClearPage2.Text = "Clear Page"
        '
        'btnExit2
        '
        Me.btnExit2.BackColor = System.Drawing.Color.FromArgb(CType(CType(201, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(233, Byte), Integer))
        Me.btnExit2.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.btnExit2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnExit2.Location = New System.Drawing.Point(5, 199)
        Me.btnExit2.Name = "btnExit2"
        Me.btnExit2.Size = New System.Drawing.Size(72, 19)
        Me.btnExit2.TabIndex = 28
        Me.btnExit2.Text = "Exit"
        '
        'lblPieces
        '
        Me.lblPieces.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Bold)
        Me.lblPieces.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblPieces.Location = New System.Drawing.Point(147, 65)
        Me.lblPieces.Name = "lblPieces"
        Me.lblPieces.Size = New System.Drawing.Size(45, 13)
        Me.lblPieces.Text = "Pieces"
        '
        'TabPage3
        '
        Me.TabPage3.AutoScroll = True
        Me.TabPage3.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TabPage3.Controls.Add(Me.TimePickerAppointmentTime)
        Me.TabPage3.Controls.Add(Me.txtTotalItems)
        Me.TabPage3.Controls.Add(Me.txtWeight)
        Me.TabPage3.Controls.Add(Me.numPallets)
        Me.TabPage3.Controls.Add(Me.Label1)
        Me.TabPage3.Controls.Add(Me.txtRestacks)
        Me.TabPage3.Controls.Add(Me.txtBadPallets)
        Me.TabPage3.Controls.Add(Me.txtRecieptNumber)
        Me.TabPage3.Controls.Add(Me.lblRecieptNum)
        Me.TabPage3.Controls.Add(Me.cbLoadType)
        Me.TabPage3.Controls.Add(Me.lblLoadType)
        Me.TabPage3.Controls.Add(Me.lblTotalItems)
        Me.TabPage3.Controls.Add(Me.lblRestacks)
        Me.TabPage3.Controls.Add(Me.lblWeight)
        Me.TabPage3.Controls.Add(Me.lblBadPallets)
        Me.TabPage3.Controls.Add(Me.lblGateTime)
        Me.TabPage3.Controls.Add(Me.lblApptTime)
        Me.TabPage3.Controls.Add(Me.btnClearPage3)
        Me.TabPage3.Controls.Add(Me.btnExit3)
        Me.TabPage3.Controls.Add(Me.DateTimePickerGateTime)
        Me.TabPage3.Location = New System.Drawing.Point(0, 0)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Size = New System.Drawing.Size(240, 218)
        Me.TabPage3.Text = "Page 3"
        '
        'txtTotalItems
        '
        Me.txtTotalItems.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtTotalItems.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Regular)
        Me.txtTotalItems.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtTotalItems.Location = New System.Drawing.Point(132, 113)
        Me.txtTotalItems.Name = "txtTotalItems"
        Me.txtTotalItems.Size = New System.Drawing.Size(73, 18)
        Me.txtTotalItems.TabIndex = 171
        '
        'txtWeight
        '
        Me.txtWeight.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtWeight.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Regular)
        Me.txtWeight.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtWeight.Location = New System.Drawing.Point(132, 76)
        Me.txtWeight.Name = "txtWeight"
        Me.txtWeight.Size = New System.Drawing.Size(73, 18)
        Me.txtWeight.TabIndex = 170
        '
        'numPallets
        '
        Me.numPallets.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.numPallets.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Regular)
        Me.numPallets.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.numPallets.Location = New System.Drawing.Point(132, 44)
        Me.numPallets.Name = "numPallets"
        Me.numPallets.Size = New System.Drawing.Size(73, 18)
        Me.numPallets.TabIndex = 160
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Bold)
        Me.Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label1.Location = New System.Drawing.Point(139, 29)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(88, 13)
        Me.Label1.Text = "Pallets Received"
        '
        'txtRestacks
        '
        Me.txtRestacks.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Regular)
        Me.txtRestacks.Location = New System.Drawing.Point(5, 113)
        Me.txtRestacks.Name = "txtRestacks"
        Me.txtRestacks.Size = New System.Drawing.Size(84, 18)
        Me.txtRestacks.TabIndex = 149
        '
        'txtBadPallets
        '
        Me.txtBadPallets.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Regular)
        Me.txtBadPallets.Location = New System.Drawing.Point(5, 76)
        Me.txtBadPallets.Name = "txtBadPallets"
        Me.txtBadPallets.Size = New System.Drawing.Size(84, 18)
        Me.txtBadPallets.TabIndex = 148
        '
        'txtRecieptNumber
        '
        Me.txtRecieptNumber.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Regular)
        Me.txtRecieptNumber.Location = New System.Drawing.Point(5, 176)
        Me.txtRecieptNumber.Name = "txtRecieptNumber"
        Me.txtRecieptNumber.Size = New System.Drawing.Size(211, 18)
        Me.txtRecieptNumber.TabIndex = 138
        '
        'lblRecieptNum
        '
        Me.lblRecieptNum.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Bold)
        Me.lblRecieptNum.Location = New System.Drawing.Point(7, 165)
        Me.lblRecieptNum.Name = "lblRecieptNum"
        Me.lblRecieptNum.Size = New System.Drawing.Size(68, 12)
        Me.lblRecieptNum.Text = "Reciept #"
        '
        'cbLoadType
        '
        Me.cbLoadType.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cbLoadType.DisplayMember = "ID"
        Me.cbLoadType.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Regular)
        Me.cbLoadType.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cbLoadType.Location = New System.Drawing.Point(5, 144)
        Me.cbLoadType.Name = "cbLoadType"
        Me.cbLoadType.Size = New System.Drawing.Size(230, 19)
        Me.cbLoadType.TabIndex = 128
        Me.cbLoadType.ValueMember = "ID"
        '
        'lblLoadType
        '
        Me.lblLoadType.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Bold)
        Me.lblLoadType.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblLoadType.Location = New System.Drawing.Point(7, 132)
        Me.lblLoadType.Name = "lblLoadType"
        Me.lblLoadType.Size = New System.Drawing.Size(80, 12)
        Me.lblLoadType.Text = "Load Type"
        '
        'lblTotalItems
        '
        Me.lblTotalItems.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Bold)
        Me.lblTotalItems.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblTotalItems.Location = New System.Drawing.Point(137, 101)
        Me.lblTotalItems.Name = "lblTotalItems"
        Me.lblTotalItems.Size = New System.Drawing.Size(77, 12)
        Me.lblTotalItems.Text = "Total Items"
        '
        'lblRestacks
        '
        Me.lblRestacks.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Bold)
        Me.lblRestacks.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblRestacks.Location = New System.Drawing.Point(7, 101)
        Me.lblRestacks.Name = "lblRestacks"
        Me.lblRestacks.Size = New System.Drawing.Size(64, 12)
        Me.lblRestacks.Text = "Restacks"
        '
        'lblWeight
        '
        Me.lblWeight.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Bold)
        Me.lblWeight.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblWeight.Location = New System.Drawing.Point(137, 64)
        Me.lblWeight.Name = "lblWeight"
        Me.lblWeight.Size = New System.Drawing.Size(50, 12)
        Me.lblWeight.Text = "Weight"
        '
        'lblBadPallets
        '
        Me.lblBadPallets.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Bold)
        Me.lblBadPallets.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblBadPallets.Location = New System.Drawing.Point(7, 64)
        Me.lblBadPallets.Name = "lblBadPallets"
        Me.lblBadPallets.Size = New System.Drawing.Size(68, 12)
        Me.lblBadPallets.Text = "Bad Pallets"
        '
        'lblGateTime
        '
        Me.lblGateTime.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Bold)
        Me.lblGateTime.ForeColor = System.Drawing.Color.Lime
        Me.lblGateTime.Location = New System.Drawing.Point(132, 8)
        Me.lblGateTime.Name = "lblGateTime"
        Me.lblGateTime.Size = New System.Drawing.Size(80, 12)
        Me.lblGateTime.Text = "Gate Time"
        Me.lblGateTime.Visible = False
        '
        'lblApptTime
        '
        Me.lblApptTime.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Bold)
        Me.lblApptTime.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblApptTime.Location = New System.Drawing.Point(7, 29)
        Me.lblApptTime.Name = "lblApptTime"
        Me.lblApptTime.Size = New System.Drawing.Size(101, 12)
        Me.lblApptTime.Text = "Appointment Time"
        '
        'btnClearPage3
        '
        Me.btnClearPage3.BackColor = System.Drawing.Color.FromArgb(CType(CType(201, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(233, Byte), Integer))
        Me.btnClearPage3.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.btnClearPage3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnClearPage3.Location = New System.Drawing.Point(163, 199)
        Me.btnClearPage3.Name = "btnClearPage3"
        Me.btnClearPage3.Size = New System.Drawing.Size(72, 19)
        Me.btnClearPage3.TabIndex = 29
        Me.btnClearPage3.Text = "Clear Page"
        '
        'btnExit3
        '
        Me.btnExit3.BackColor = System.Drawing.Color.FromArgb(CType(CType(201, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(233, Byte), Integer))
        Me.btnExit3.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.btnExit3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnExit3.Location = New System.Drawing.Point(5, 199)
        Me.btnExit3.Name = "btnExit3"
        Me.btnExit3.Size = New System.Drawing.Size(72, 19)
        Me.btnExit3.TabIndex = 28
        Me.btnExit3.Text = "Exit"
        '
        'DateTimePickerGateTime
        '
        Me.DateTimePickerGateTime.CustomFormat = "hh:mm tt"
        Me.DateTimePickerGateTime.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Regular)
        Me.DateTimePickerGateTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DateTimePickerGateTime.Location = New System.Drawing.Point(130, 4)
        Me.DateTimePickerGateTime.Name = "DateTimePickerGateTime"
        Me.DateTimePickerGateTime.Size = New System.Drawing.Size(92, 19)
        Me.DateTimePickerGateTime.TabIndex = 101
        Me.DateTimePickerGateTime.Visible = False
        '
        'TabPage4
        '
        Me.TabPage4.AutoScroll = True
        Me.TabPage4.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TabPage4.Controls.Add(Me.txtAddCash)
        Me.TabPage4.Controls.Add(Me.rbsplit)
        Me.TabPage4.Controls.Add(Me.txtBOLSEAL)
        Me.TabPage4.Controls.Add(Me.txtCheckNumber)
        Me.TabPage4.Controls.Add(Me.txtAmount)
        Me.TabPage4.Controls.Add(Me.txtComments)
        Me.TabPage4.Controls.Add(Me.rbCard)
        Me.TabPage4.Controls.Add(Me.rbCheck)
        Me.TabPage4.Controls.Add(Me.rbCash)
        Me.TabPage4.Controls.Add(Me.lblAmount)
        Me.TabPage4.Controls.Add(Me.lblCheckNumber)
        Me.TabPage4.Controls.Add(Me.lblBOLSEAL)
        Me.TabPage4.Controls.Add(Me.lblComments)
        Me.TabPage4.Controls.Add(Me.btnClearPage4)
        Me.TabPage4.Controls.Add(Me.btnExit4)
        Me.TabPage4.Controls.Add(Me.lblAddCash)
        Me.TabPage4.Location = New System.Drawing.Point(0, 0)
        Me.TabPage4.Name = "TabPage4"
        Me.TabPage4.Size = New System.Drawing.Size(232, 216)
        Me.TabPage4.Text = "Page 4"
        '
        'txtAddCash
        '
        Me.txtAddCash.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtAddCash.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Regular)
        Me.txtAddCash.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtAddCash.Location = New System.Drawing.Point(161, 74)
        Me.txtAddCash.Name = "txtAddCash"
        Me.txtAddCash.Size = New System.Drawing.Size(73, 18)
        Me.txtAddCash.TabIndex = 79
        Me.txtAddCash.Visible = False
        '
        'rbsplit
        '
        Me.rbsplit.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.rbsplit.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Bold)
        Me.rbsplit.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.rbsplit.Location = New System.Drawing.Point(178, 16)
        Me.rbsplit.Name = "rbsplit"
        Me.rbsplit.Size = New System.Drawing.Size(49, 20)
        Me.rbsplit.TabIndex = 77
        Me.rbsplit.Text = "Split"
        '
        'txtBOLSEAL
        '
        Me.txtBOLSEAL.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtBOLSEAL.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Regular)
        Me.txtBOLSEAL.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtBOLSEAL.Location = New System.Drawing.Point(5, 98)
        Me.txtBOLSEAL.Name = "txtBOLSEAL"
        Me.txtBOLSEAL.Size = New System.Drawing.Size(230, 18)
        Me.txtBOLSEAL.TabIndex = 29
        '
        'txtCheckNumber
        '
        Me.txtCheckNumber.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtCheckNumber.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Regular)
        Me.txtCheckNumber.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtCheckNumber.Location = New System.Drawing.Point(84, 51)
        Me.txtCheckNumber.Name = "txtCheckNumber"
        Me.txtCheckNumber.Size = New System.Drawing.Size(151, 18)
        Me.txtCheckNumber.TabIndex = 50
        '
        'txtAmount
        '
        Me.txtAmount.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtAmount.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Regular)
        Me.txtAmount.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtAmount.Location = New System.Drawing.Point(5, 51)
        Me.txtAmount.Name = "txtAmount"
        Me.txtAmount.Size = New System.Drawing.Size(73, 18)
        Me.txtAmount.TabIndex = 56
        '
        'txtComments
        '
        Me.txtComments.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.txtComments.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Regular)
        Me.txtComments.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.txtComments.Location = New System.Drawing.Point(5, 131)
        Me.txtComments.Multiline = True
        Me.txtComments.Name = "txtComments"
        Me.txtComments.Size = New System.Drawing.Size(230, 63)
        Me.txtComments.TabIndex = 30
        '
        'rbCard
        '
        Me.rbCard.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.rbCard.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Bold)
        Me.rbCard.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.rbCard.Location = New System.Drawing.Point(125, 15)
        Me.rbCard.Name = "rbCard"
        Me.rbCard.Size = New System.Drawing.Size(49, 20)
        Me.rbCard.TabIndex = 72
        Me.rbCard.Text = "Card"
        '
        'rbCheck
        '
        Me.rbCheck.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.rbCheck.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Bold)
        Me.rbCheck.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.rbCheck.Location = New System.Drawing.Point(66, 15)
        Me.rbCheck.Name = "rbCheck"
        Me.rbCheck.Size = New System.Drawing.Size(53, 20)
        Me.rbCheck.TabIndex = 71
        Me.rbCheck.Text = "Check"
        '
        'rbCash
        '
        Me.rbCash.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.rbCash.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Bold)
        Me.rbCash.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.rbCash.Location = New System.Drawing.Point(11, 15)
        Me.rbCash.Name = "rbCash"
        Me.rbCash.Size = New System.Drawing.Size(49, 20)
        Me.rbCash.TabIndex = 70
        Me.rbCash.Text = "Cash"
        '
        'lblAmount
        '
        Me.lblAmount.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Bold)
        Me.lblAmount.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblAmount.Location = New System.Drawing.Point(7, 40)
        Me.lblAmount.Name = "lblAmount"
        Me.lblAmount.Size = New System.Drawing.Size(70, 12)
        Me.lblAmount.Text = "Total Amount"
        '
        'lblCheckNumber
        '
        Me.lblCheckNumber.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Bold)
        Me.lblCheckNumber.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblCheckNumber.Location = New System.Drawing.Point(86, 40)
        Me.lblCheckNumber.Name = "lblCheckNumber"
        Me.lblCheckNumber.Size = New System.Drawing.Size(141, 12)
        Me.lblCheckNumber.Text = "Check/Transaction Number"
        '
        'lblBOLSEAL
        '
        Me.lblBOLSEAL.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Bold)
        Me.lblBOLSEAL.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblBOLSEAL.Location = New System.Drawing.Point(7, 86)
        Me.lblBOLSEAL.Name = "lblBOLSEAL"
        Me.lblBOLSEAL.Size = New System.Drawing.Size(86, 12)
        Me.lblBOLSEAL.Text = "BOL/PRO/Seal #"
        '
        'lblComments
        '
        Me.lblComments.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Bold)
        Me.lblComments.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblComments.Location = New System.Drawing.Point(7, 118)
        Me.lblComments.Name = "lblComments"
        Me.lblComments.Size = New System.Drawing.Size(61, 12)
        Me.lblComments.Text = "Comments"
        '
        'btnClearPage4
        '
        Me.btnClearPage4.BackColor = System.Drawing.Color.FromArgb(CType(CType(201, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(233, Byte), Integer))
        Me.btnClearPage4.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.btnClearPage4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnClearPage4.Location = New System.Drawing.Point(163, 199)
        Me.btnClearPage4.Name = "btnClearPage4"
        Me.btnClearPage4.Size = New System.Drawing.Size(72, 19)
        Me.btnClearPage4.TabIndex = 25
        Me.btnClearPage4.Text = "Clear Page"
        '
        'btnExit4
        '
        Me.btnExit4.BackColor = System.Drawing.Color.FromArgb(CType(CType(201, Byte), Integer), CType(CType(220, Byte), Integer), CType(CType(233, Byte), Integer))
        Me.btnExit4.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.btnExit4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.btnExit4.Location = New System.Drawing.Point(5, 199)
        Me.btnExit4.Name = "btnExit4"
        Me.btnExit4.Size = New System.Drawing.Size(72, 19)
        Me.btnExit4.TabIndex = 24
        Me.btnExit4.Text = "Exit"
        '
        'lblAddCash
        '
        Me.lblAddCash.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Bold)
        Me.lblAddCash.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblAddCash.Location = New System.Drawing.Point(84, 75)
        Me.lblAddCash.Name = "lblAddCash"
        Me.lblAddCash.Size = New System.Drawing.Size(80, 12)
        Me.lblAddCash.Text = "Additional Cash"
        Me.lblAddCash.Visible = False
        '
        'myListBox
        '
        Me.myListBox.Location = New System.Drawing.Point(5, 116)
        Me.myListBox.Name = "myListBox"
        Me.myListBox.SelectedIndex = -1
        Me.myListBox.Size = New System.Drawing.Size(186, 78)
        Me.myListBox.TabIndex = 137
        Me.myListBox.Text = "FontListBox1"
        '
        'TimePickerAppointmentTime
        '
        Me.TimePickerAppointmentTime.CustomFormat = "hh:mm tt"
        Me.TimePickerAppointmentTime.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Regular)
        Me.TimePickerAppointmentTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.TimePickerAppointmentTime.Location = New System.Drawing.Point(5, 42)
        Me.TimePickerAppointmentTime.MinDate = New Date(1900, 1, 1, 0, 0, 0, 0)
        Me.TimePickerAppointmentTime.Name = "TimePickerAppointmentTime"
        Me.TimePickerAppointmentTime.ShowUpDown = True
        Me.TimePickerAppointmentTime.Size = New System.Drawing.Size(84, 19)
        Me.TimePickerAppointmentTime.TabIndex = 181
        Me.TimePickerAppointmentTime.Value = New Date(1900, 1, 1, 0, 0, 0, 0)
        '
        'NewLoad
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(240, 240)
        Me.Controls.Add(Me.TabControl1)
        Me.Location = New System.Drawing.Point(0, 0)
        Me.Name = "NewLoad"
        Me.Text = "NewLoad"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage3.ResumeLayout(False)
        Me.TabPage4.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    '    Friend WithEvents CtrlConn9 As SEUdock.ctrlConn
    Friend WithEvents myListBox As SEUdock.FontListBox
    '    Friend WithEvents CtrlConn2 As SEUdock.ctrlConn
    '    Friend WithEvents CtrlConn4 As SEUdock.ctrlConn
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents cbCarrier As System.Windows.Forms.ComboBox
    Friend WithEvents txtTrailerNumber As System.Windows.Forms.TextBox
    Friend WithEvents cbDepartment As System.Windows.Forms.ComboBox
    Friend WithEvents txtDoor As System.Windows.Forms.TextBox
    Friend WithEvents txtLoadNumber As System.Windows.Forms.TextBox
    Friend WithEvents txtTruckNumber As System.Windows.Forms.TextBox
    Friend WithEvents txtPurchaseOrder As System.Windows.Forms.TextBox
    Friend WithEvents lblTrailerNum As System.Windows.Forms.Label
    Friend WithEvents lblDepartment As System.Windows.Forms.Label
    Friend WithEvents lblDoor As System.Windows.Forms.Label
    Friend WithEvents lblLoadNumber As System.Windows.Forms.Label
    Friend WithEvents lblCarrier As System.Windows.Forms.Label
    Friend WithEvents lblTruckNum As System.Windows.Forms.Label
    Friend WithEvents lblPurchaseOrder As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents btnClearPage1 As System.Windows.Forms.Button
    Friend WithEvents btnExit1 As System.Windows.Forms.Button
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents lbUnloaders As System.Windows.Forms.ListBox
    Friend WithEvents cbLoadDescription As System.Windows.Forms.ComboBox
    Friend WithEvents numPieces As System.Windows.Forms.TextBox
    Friend WithEvents txtVendor As System.Windows.Forms.TextBox
    Friend WithEvents txtVendorNumber As System.Windows.Forms.TextBox
    Friend WithEvents lblLoadDescription As System.Windows.Forms.Label
    Friend WithEvents btnDelUnloader As System.Windows.Forms.Button
    Friend WithEvents btnAddUnloader As System.Windows.Forms.Button
    Friend WithEvents lblPieces As System.Windows.Forms.Label
    Friend WithEvents lblVendor As System.Windows.Forms.Label
    Friend WithEvents lblVendorNum As System.Windows.Forms.Label
    Friend WithEvents btnClearPage2 As System.Windows.Forms.Button
    Friend WithEvents btnExit2 As System.Windows.Forms.Button
    Friend WithEvents TabPage3 As System.Windows.Forms.TabPage
    Friend WithEvents txtRestacks As System.Windows.Forms.TextBox
    Friend WithEvents txtBadPallets As System.Windows.Forms.TextBox
    Friend WithEvents txtRecieptNumber As System.Windows.Forms.TextBox
    Friend WithEvents lblRecieptNum As System.Windows.Forms.Label
    Friend WithEvents cbLoadType As System.Windows.Forms.ComboBox
    Friend WithEvents lblLoadType As System.Windows.Forms.Label
    Friend WithEvents lblTotalItems As System.Windows.Forms.Label
    Friend WithEvents lblRestacks As System.Windows.Forms.Label
    Friend WithEvents lblWeight As System.Windows.Forms.Label
    Friend WithEvents lblBadPallets As System.Windows.Forms.Label
    Friend WithEvents lblGateTime As System.Windows.Forms.Label
    Friend WithEvents lblApptTime As System.Windows.Forms.Label
    Friend WithEvents btnClearPage3 As System.Windows.Forms.Button
    Friend WithEvents btnExit3 As System.Windows.Forms.Button
    Friend WithEvents DateTimePickerGateTime As System.Windows.Forms.DateTimePicker
    Friend WithEvents TabPage4 As System.Windows.Forms.TabPage
    Friend WithEvents txtAddCash As System.Windows.Forms.TextBox
    Friend WithEvents lblAddCash As System.Windows.Forms.Label
    Friend WithEvents rbsplit As System.Windows.Forms.RadioButton
    Friend WithEvents txtBOLSEAL As System.Windows.Forms.TextBox
    Friend WithEvents txtCheckNumber As System.Windows.Forms.TextBox
    Friend WithEvents txtAmount As System.Windows.Forms.TextBox
    Friend WithEvents txtComments As System.Windows.Forms.TextBox
    Friend WithEvents rbCard As System.Windows.Forms.RadioButton
    Friend WithEvents rbCheck As System.Windows.Forms.RadioButton
    Friend WithEvents rbCash As System.Windows.Forms.RadioButton
    Friend WithEvents lblAmount As System.Windows.Forms.Label
    Friend WithEvents lblCheckNumber As System.Windows.Forms.Label
    Friend WithEvents lblBOLSEAL As System.Windows.Forms.Label
    Friend WithEvents lblComments As System.Windows.Forms.Label
    Friend WithEvents btnClearPage4 As System.Windows.Forms.Button
    Friend WithEvents btnExit4 As System.Windows.Forms.Button
    Friend WithEvents lblPalletsUnloaded As System.Windows.Forms.Label
    Friend WithEvents numPallets As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtTotalItems As System.Windows.Forms.TextBox
    Friend WithEvents txtWeight As System.Windows.Forms.TextBox
    Friend WithEvents txtPalletsUnloaded As System.Windows.Forms.TextBox
    Friend WithEvents TimePickerAppointmentTime As System.Windows.Forms.DateTimePicker
    '    Friend WithEvents CtrlConn1 As SEUdock.ctrlConn
End Class
