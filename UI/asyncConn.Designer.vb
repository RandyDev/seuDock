<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class asyncConn
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
        Me.Label2 = New System.Windows.Forms.Label
        Me.labelStatusValue = New System.Windows.Forms.Label
        Me.lblNetworkStatus = New System.Windows.Forms.Label
        Me.btnExit = New System.Windows.Forms.Button
        Me.labelLastSyncValue = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(2, 138)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(72, 18)
        Me.Label2.Text = "Last Sync:"
        '
        'labelStatusValue
        '
        Me.labelStatusValue.Location = New System.Drawing.Point(3, 79)
        Me.labelStatusValue.Name = "labelStatusValue"
        Me.labelStatusValue.Size = New System.Drawing.Size(234, 46)
        '
        'lblNetworkStatus
        '
        Me.lblNetworkStatus.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.lblNetworkStatus.ForeColor = System.Drawing.SystemColors.GrayText
        Me.lblNetworkStatus.Location = New System.Drawing.Point(43, 11)
        Me.lblNetworkStatus.Name = "lblNetworkStatus"
        Me.lblNetworkStatus.Size = New System.Drawing.Size(150, 20)
        Me.lblNetworkStatus.Text = "Network Status"
        Me.lblNetworkStatus.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'btnExit
        '
        Me.btnExit.Location = New System.Drawing.Point(164, 217)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(72, 20)
        Me.btnExit.TabIndex = 1
        Me.btnExit.Text = "Return"
        '
        'labelLastSyncValue
        '
        Me.labelLastSyncValue.Location = New System.Drawing.Point(1, 156)
        Me.labelLastSyncValue.Name = "labelLastSyncValue"
        Me.labelLastSyncValue.Size = New System.Drawing.Size(237, 20)
        '
        'asyncConn
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(240, 240)
        Me.ControlBox = False
        Me.Controls.Add(Me.labelLastSyncValue)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.lblNetworkStatus)
        Me.Controls.Add(Me.labelStatusValue)
        Me.Controls.Add(Me.Label2)
        Me.Location = New System.Drawing.Point(0, 0)
        Me.Name = "asyncConn"
        Me.Text = "ConnectionInfo"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents labelStatusValue As System.Windows.Forms.Label
    Friend WithEvents lblNetworkStatus As System.Windows.Forms.Label
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents labelLastSyncValue As System.Windows.Forms.Label
End Class
