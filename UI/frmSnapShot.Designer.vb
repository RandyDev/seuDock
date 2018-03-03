<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class frmSnapShot
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
        Me.lblSyncWait = New System.Windows.Forms.Label
        Me.lblProgress = New System.Windows.Forms.Label
        Me.Timer1 = New System.Windows.Forms.Timer
        Me.SuspendLayout()
        '
        'lblSyncWait
        '
        Me.lblSyncWait.Font = New System.Drawing.Font("Tahoma", 18.0!, System.Drawing.FontStyle.Bold)
        Me.lblSyncWait.ForeColor = System.Drawing.Color.Coral
        Me.lblSyncWait.Location = New System.Drawing.Point(0, 1)
        Me.lblSyncWait.Name = "lblSyncWait"
        Me.lblSyncWait.Size = New System.Drawing.Size(240, 66)
        Me.lblSyncWait.Text = "Downloading SnapShot" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        Me.lblSyncWait.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'lblProgress
        '
        Me.lblProgress.Location = New System.Drawing.Point(18, 160)
        Me.lblProgress.Name = "lblProgress"
        Me.lblProgress.Size = New System.Drawing.Size(205, 20)
        Me.lblProgress.Text = "Creating Index on Carrier Table ..."
        Me.lblProgress.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.lblProgress.Visible = False
        '
        'Timer1
        '
        Me.Timer1.Interval = 500
        '
        'frmSnapShot
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(240, 240)
        Me.ControlBox = False
        Me.Controls.Add(Me.lblProgress)
        Me.Controls.Add(Me.lblSyncWait)
        Me.Location = New System.Drawing.Point(0, 0)
        Me.MinimizeBox = False
        Me.Name = "frmSnapShot"
        Me.Text = "frmSync"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lblSyncWait As System.Windows.Forms.Label
    Friend WithEvents lblProgress As System.Windows.Forms.Label
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
End Class
