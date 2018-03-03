<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class Time
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
        Me.Label1 = New System.Windows.Forms.Label
        Me.btnExit = New System.Windows.Forms.Button
        Me.btnReport = New System.Windows.Forms.Button
        Me.btnClockOut = New System.Windows.Forms.Button
        Me.btnClockIn = New System.Windows.Forms.Button
        Me.btnEndOfDay = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 14.0!, System.Drawing.FontStyle.Bold)
        Me.Label1.Location = New System.Drawing.Point(-1, 15)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(192, 20)
        Me.Label1.Text = "Time && Attendance"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'btnExit
        '
        Me.btnExit.Font = New System.Drawing.Font("Tahoma", 16.0!, System.Drawing.FontStyle.Regular)
        Me.btnExit.Location = New System.Drawing.Point(10, 157)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(218, 34)
        Me.btnExit.TabIndex = 25
        Me.btnExit.Text = "< Back to Main Menu"
        '
        'btnReport
        '
        Me.btnReport.Font = New System.Drawing.Font("Tahoma", 16.0!, System.Drawing.FontStyle.Regular)
        Me.btnReport.Location = New System.Drawing.Point(5, 40)
        Me.btnReport.Name = "btnReport"
        Me.btnReport.Size = New System.Drawing.Size(230, 30)
        Me.btnReport.TabIndex = 24
        Me.btnReport.Text = "Report / ICE"
        '
        'btnClockOut
        '
        Me.btnClockOut.Font = New System.Drawing.Font("Tahoma", 16.0!, System.Drawing.FontStyle.Regular)
        Me.btnClockOut.Location = New System.Drawing.Point(5, 105)
        Me.btnClockOut.Name = "btnClockOut"
        Me.btnClockOut.Size = New System.Drawing.Size(230, 30)
        Me.btnClockOut.TabIndex = 21
        Me.btnClockOut.Text = "Clock Out"
        '
        'btnClockIn
        '
        Me.btnClockIn.Font = New System.Drawing.Font("Tahoma", 16.0!, System.Drawing.FontStyle.Regular)
        Me.btnClockIn.Location = New System.Drawing.Point(5, 72)
        Me.btnClockIn.Name = "btnClockIn"
        Me.btnClockIn.Size = New System.Drawing.Size(230, 30)
        Me.btnClockIn.TabIndex = 20
        Me.btnClockIn.Text = "Clock In"
        '
        'btnEndOfDay
        '
        Me.btnEndOfDay.Enabled = False
        Me.btnEndOfDay.Font = New System.Drawing.Font("Tahoma", 16.0!, System.Drawing.FontStyle.Regular)
        Me.btnEndOfDay.Location = New System.Drawing.Point(11, 199)
        Me.btnEndOfDay.Name = "btnEndOfDay"
        Me.btnEndOfDay.Size = New System.Drawing.Size(218, 34)
        Me.btnEndOfDay.TabIndex = 26
        Me.btnEndOfDay.Text = "End of Day"
        '
        'Time
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(240, 240)
        Me.Controls.Add(Me.btnEndOfDay)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.btnReport)
        Me.Controls.Add(Me.btnClockOut)
        Me.Controls.Add(Me.btnClockIn)
        Me.Location = New System.Drawing.Point(0, 0)
        Me.Name = "Time"
        Me.Text = "Time"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents btnReport As System.Windows.Forms.Button
    Friend WithEvents btnClockOut As System.Windows.Forms.Button
    Friend WithEvents btnClockIn As System.Windows.Forms.Button
    Friend WithEvents btnEndOfDay As System.Windows.Forms.Button
End Class
