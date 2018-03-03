<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class ClockIn
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
        Me.cbJobDescription = New System.Windows.Forms.ComboBox
        Me.lblJobTask = New System.Windows.Forms.Label
        Me.btnCancel = New System.Windows.Forms.Button
        Me.btnOK = New System.Windows.Forms.Button
        Me.txtPIN = New System.Windows.Forms.TextBox
        Me.lblPIN = New System.Windows.Forms.Label
        Me.lblEmployeeList = New System.Windows.Forms.Label
        Me.lblTitle = New System.Windows.Forms.Label
        Me.lbEmployeeList = New System.Windows.Forms.ListBox
        Me.cbDepartment = New System.Windows.Forms.ComboBox
        Me.lblDepartment = New System.Windows.Forms.Label
        Me.lblTimeIn = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'cbJobDescription
        '
        Me.cbJobDescription.Location = New System.Drawing.Point(3, 44)
        Me.cbJobDescription.Name = "cbJobDescription"
        Me.cbJobDescription.Size = New System.Drawing.Size(121, 22)
        Me.cbJobDescription.TabIndex = 46
        '
        'lblJobTask
        '
        Me.lblJobTask.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Bold)
        Me.lblJobTask.Location = New System.Drawing.Point(7, 32)
        Me.lblJobTask.Name = "lblJobTask"
        Me.lblJobTask.Size = New System.Drawing.Size(102, 15)
        Me.lblJobTask.Text = "Job Description"
        '
        'btnCancel
        '
        Me.btnCancel.Location = New System.Drawing.Point(165, 217)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(72, 20)
        Me.btnCancel.TabIndex = 43
        Me.btnCancel.Text = "DONE"
        '
        'btnOK
        '
        Me.btnOK.Location = New System.Drawing.Point(87, 217)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(72, 20)
        Me.btnOK.TabIndex = 42
        Me.btnOK.Text = "OK"
        '
        'txtPIN
        '
        Me.txtPIN.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Regular)
        Me.txtPIN.Location = New System.Drawing.Point(5, 220)
        Me.txtPIN.Name = "txtPIN"
        Me.txtPIN.Size = New System.Drawing.Size(62, 18)
        Me.txtPIN.TabIndex = 41
        '
        'lblPIN
        '
        Me.lblPIN.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Bold)
        Me.lblPIN.Location = New System.Drawing.Point(7, 209)
        Me.lblPIN.Name = "lblPIN"
        Me.lblPIN.Size = New System.Drawing.Size(34, 12)
        Me.lblPIN.Text = "PIN"
        '
        'lblEmployeeList
        '
        Me.lblEmployeeList.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Bold)
        Me.lblEmployeeList.Location = New System.Drawing.Point(5, 66)
        Me.lblEmployeeList.Name = "lblEmployeeList"
        Me.lblEmployeeList.Size = New System.Drawing.Size(96, 10)
        Me.lblEmployeeList.Text = "Employee List"
        '
        'lblTitle
        '
        Me.lblTitle.Font = New System.Drawing.Font("Tahoma", 16.0!, System.Drawing.FontStyle.Regular)
        Me.lblTitle.Location = New System.Drawing.Point(1, 0)
        Me.lblTitle.Name = "lblTitle"
        Me.lblTitle.Size = New System.Drawing.Size(89, 23)
        Me.lblTitle.Text = "clock IN"
        '
        'lbEmployeeList
        '
        Me.lbEmployeeList.Location = New System.Drawing.Point(5, 79)
        Me.lbEmployeeList.Name = "lbEmployeeList"
        Me.lbEmployeeList.Size = New System.Drawing.Size(235, 128)
        Me.lbEmployeeList.TabIndex = 60
        '
        'cbDepartment
        '
        Me.cbDepartment.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.cbDepartment.DisplayMember = "ID"
        Me.cbDepartment.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cbDepartment.Location = New System.Drawing.Point(130, 44)
        Me.cbDepartment.Name = "cbDepartment"
        Me.cbDepartment.Size = New System.Drawing.Size(110, 22)
        Me.cbDepartment.TabIndex = 67
        Me.cbDepartment.ValueMember = "ID"
        '
        'lblDepartment
        '
        Me.lblDepartment.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Bold)
        Me.lblDepartment.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblDepartment.Location = New System.Drawing.Point(131, 32)
        Me.lblDepartment.Name = "lblDepartment"
        Me.lblDepartment.Size = New System.Drawing.Size(91, 12)
        Me.lblDepartment.Text = "Department"
        '
        'lblTimeIn
        '
        Me.lblTimeIn.Font = New System.Drawing.Font("Tahoma", 16.0!, System.Drawing.FontStyle.Bold)
        Me.lblTimeIn.ForeColor = System.Drawing.Color.Green
        Me.lblTimeIn.Location = New System.Drawing.Point(87, 0)
        Me.lblTimeIn.Name = "lblTimeIn"
        Me.lblTimeIn.Size = New System.Drawing.Size(122, 23)
        Me.lblTimeIn.Text = "12:45 PM"
        Me.lblTimeIn.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'ClockIn
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(240, 240)
        Me.Controls.Add(Me.cbDepartment)
        Me.Controls.Add(Me.lblDepartment)
        Me.Controls.Add(Me.cbJobDescription)
        Me.Controls.Add(Me.lblJobTask)
        Me.Controls.Add(Me.lblTimeIn)
        Me.Controls.Add(Me.lbEmployeeList)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnOK)
        Me.Controls.Add(Me.txtPIN)
        Me.Controls.Add(Me.lblPIN)
        Me.Controls.Add(Me.lblEmployeeList)
        Me.Controls.Add(Me.lblTitle)
        Me.Location = New System.Drawing.Point(0, 0)
        Me.Name = "ClockIn"
        Me.Text = "ClockIn"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents cbJobDescription As System.Windows.Forms.ComboBox
    Friend WithEvents lblJobTask As System.Windows.Forms.Label
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents btnOK As System.Windows.Forms.Button
    Friend WithEvents txtPIN As System.Windows.Forms.TextBox
    Friend WithEvents lblPIN As System.Windows.Forms.Label
    Friend WithEvents lblEmployeeList As System.Windows.Forms.Label
    Friend WithEvents lblTitle As System.Windows.Forms.Label
    Friend WithEvents lbEmployeeList As System.Windows.Forms.ListBox
    Friend WithEvents cbDepartment As System.Windows.Forms.ComboBox
    Friend WithEvents lblDepartment As System.Windows.Forms.Label
    Friend WithEvents lblTimeIn As System.Windows.Forms.Label
End Class
