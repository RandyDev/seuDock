<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class ClockOut
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
        Me.btnCancel = New System.Windows.Forms.Button
        Me.btnOK = New System.Windows.Forms.Button
        Me.txtpin = New System.Windows.Forms.TextBox
        Me.lblPIN = New System.Windows.Forms.Label
        Me.lblEmployeeList = New System.Windows.Forms.Label
        Me.lblTitle = New System.Windows.Forms.Label
        Me.lblTimeIn = New System.Windows.Forms.Label
        Me.lbEmployeeList = New System.Windows.Forms.ListBox
        Me.ChkBxOutForDay = New System.Windows.Forms.CheckBox
        Me.SuspendLayout()
        '
        'btnCancel
        '
        Me.btnCancel.Location = New System.Drawing.Point(163, 216)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(72, 20)
        Me.btnCancel.TabIndex = 35
        Me.btnCancel.Text = "EXIT"
        '
        'btnOK
        '
        Me.btnOK.Location = New System.Drawing.Point(85, 216)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(72, 20)
        Me.btnOK.TabIndex = 34
        Me.btnOK.Text = "OK"
        '
        'txtpin
        '
        Me.txtpin.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.txtpin.Location = New System.Drawing.Point(8, 217)
        Me.txtpin.Name = "txtpin"
        Me.txtpin.Size = New System.Drawing.Size(62, 19)
        Me.txtpin.TabIndex = 33
        '
        'lblPIN
        '
        Me.lblPIN.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Bold)
        Me.lblPIN.Location = New System.Drawing.Point(8, 205)
        Me.lblPIN.Name = "lblPIN"
        Me.lblPIN.Size = New System.Drawing.Size(26, 10)
        Me.lblPIN.Text = "PIN"
        '
        'lblEmployeeList
        '
        Me.lblEmployeeList.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Bold)
        Me.lblEmployeeList.Location = New System.Drawing.Point(3, 26)
        Me.lblEmployeeList.Name = "lblEmployeeList"
        Me.lblEmployeeList.Size = New System.Drawing.Size(96, 12)
        Me.lblEmployeeList.Text = "Employee List"
        '
        'lblTitle
        '
        Me.lblTitle.Font = New System.Drawing.Font("Tahoma", 16.0!, System.Drawing.FontStyle.Regular)
        Me.lblTitle.Location = New System.Drawing.Point(0, 0)
        Me.lblTitle.Name = "lblTitle"
        Me.lblTitle.Size = New System.Drawing.Size(109, 23)
        Me.lblTitle.Text = "clock OUT"
        '
        'lblTimeIn
        '
        Me.lblTimeIn.Font = New System.Drawing.Font("Tahoma", 16.0!, System.Drawing.FontStyle.Bold)
        Me.lblTimeIn.ForeColor = System.Drawing.Color.Red
        Me.lblTimeIn.Location = New System.Drawing.Point(95, 0)
        Me.lblTimeIn.Name = "lblTimeIn"
        Me.lblTimeIn.Size = New System.Drawing.Size(128, 23)
        Me.lblTimeIn.Text = "12:55 PM"
        Me.lblTimeIn.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'lbEmployeeList
        '
        Me.lbEmployeeList.Location = New System.Drawing.Point(5, 37)
        Me.lbEmployeeList.Name = "lbEmployeeList"
        Me.lbEmployeeList.Size = New System.Drawing.Size(230, 156)
        Me.lbEmployeeList.TabIndex = 66
        '
        'ChkBxOutForDay
        '
        Me.ChkBxOutForDay.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.ChkBxOutForDay.ForeColor = System.Drawing.Color.Orange
        Me.ChkBxOutForDay.Location = New System.Drawing.Point(51, 195)
        Me.ChkBxOutForDay.Name = "ChkBxOutForDay"
        Me.ChkBxOutForDay.Size = New System.Drawing.Size(100, 20)
        Me.ChkBxOutForDay.TabIndex = 71
        Me.ChkBxOutForDay.Text = "Out for Day"
        '
        'ClockOut
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(240, 240)
        Me.Controls.Add(Me.lblTitle)
        Me.Controls.Add(Me.ChkBxOutForDay)
        Me.Controls.Add(Me.lbEmployeeList)
        Me.Controls.Add(Me.lblTimeIn)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnOK)
        Me.Controls.Add(Me.txtpin)
        Me.Controls.Add(Me.lblPIN)
        Me.Controls.Add(Me.lblEmployeeList)
        Me.Location = New System.Drawing.Point(0, 0)
        Me.Name = "ClockOut"
        Me.Text = "ClockOut"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents btnOK As System.Windows.Forms.Button
    Friend WithEvents txtpin As System.Windows.Forms.TextBox
    Friend WithEvents lblPIN As System.Windows.Forms.Label
    Friend WithEvents lblEmployeeList As System.Windows.Forms.Label
    Friend WithEvents lblTitle As System.Windows.Forms.Label
    Friend WithEvents lblTimeIn As System.Windows.Forms.Label
    Friend WithEvents lbEmployeeList As System.Windows.Forms.ListBox
    Friend WithEvents ChkBxOutForDay As System.Windows.Forms.CheckBox
End Class
