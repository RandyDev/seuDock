<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class ClockInOutReport
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
        Me.btnOK = New System.Windows.Forms.Button
        Me.lblTitle = New System.Windows.Forms.Label
        Me.Button1 = New System.Windows.Forms.Button
        Me.gridcior = New System.Windows.Forms.DataGrid
        Me.SuspendLayout()
        '
        'btnOK
        '
        Me.btnOK.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Bold)
        Me.btnOK.Location = New System.Drawing.Point(5, 208)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(103, 28)
        Me.btnOK.TabIndex = 8
        Me.btnOK.Text = "<-- Go Back"
        '
        'lblTitle
        '
        Me.lblTitle.Font = New System.Drawing.Font("Tahoma", 16.0!, System.Drawing.FontStyle.Regular)
        Me.lblTitle.Location = New System.Drawing.Point(5, 7)
        Me.lblTitle.Name = "lblTitle"
        Me.lblTitle.Size = New System.Drawing.Size(211, 24)
        Me.lblTitle.Text = "Clock IN/OUT Report"
        '
        'Button1
        '
        Me.Button1.Enabled = False
        Me.Button1.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Bold)
        Me.Button1.Location = New System.Drawing.Point(138, 208)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(97, 28)
        Me.Button1.TabIndex = 21
        Me.Button1.Text = "View Loads"
        '
        'gridcior
        '
        Me.gridcior.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.gridcior.Location = New System.Drawing.Point(5, 34)
        Me.gridcior.Name = "gridcior"
        Me.gridcior.RowHeadersVisible = False
        Me.gridcior.Size = New System.Drawing.Size(230, 168)
        Me.gridcior.TabIndex = 24
        '
        'ClockInOutReport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(240, 240)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.btnOK)
        Me.Controls.Add(Me.lblTitle)
        Me.Controls.Add(Me.gridcior)
        Me.Location = New System.Drawing.Point(0, 0)
        Me.Name = "ClockInOutReport"
        Me.Text = "ClockInOutReport"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnOK As System.Windows.Forms.Button
    Friend WithEvents lblTitle As System.Windows.Forms.Label
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents gridcior As System.Windows.Forms.DataGrid
End Class
