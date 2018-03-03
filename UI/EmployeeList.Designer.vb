<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class EmployeeList
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
        Me.gridEmployeeList = New System.Windows.Forms.DataGrid
        Me.btnExit = New System.Windows.Forms.Button
        Me.lblTitle = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'gridEmployeeList
        '
        Me.gridEmployeeList.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.gridEmployeeList.Location = New System.Drawing.Point(5, 26)
        Me.gridEmployeeList.Name = "gridEmployeeList"
        Me.gridEmployeeList.Size = New System.Drawing.Size(230, 186)
        Me.gridEmployeeList.TabIndex = 19
        '
        'btnExit
        '
        Me.btnExit.Location = New System.Drawing.Point(5, 218)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(72, 20)
        Me.btnExit.TabIndex = 18
        Me.btnExit.Text = "Exit"
        '
        'lblTitle
        '
        Me.lblTitle.Font = New System.Drawing.Font("Tahoma", 14.0!, System.Drawing.FontStyle.Bold)
        Me.lblTitle.Location = New System.Drawing.Point(0, 0)
        Me.lblTitle.Name = "lblTitle"
        Me.lblTitle.Size = New System.Drawing.Size(148, 30)
        Me.lblTitle.Text = "Employee List"
        '
        'EmployeeList
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(240, 240)
        Me.Controls.Add(Me.gridEmployeeList)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.lblTitle)
        Me.Location = New System.Drawing.Point(0, 0)
        Me.Name = "EmployeeList"
        Me.Text = "EmployeeList"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents gridEmployeeList As System.Windows.Forms.DataGrid
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents lblTitle As System.Windows.Forms.Label
End Class
