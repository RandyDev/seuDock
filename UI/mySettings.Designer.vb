<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class mySettings
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
        Me.lblTitle = New System.Windows.Forms.Label
        Me.btnReturn = New System.Windows.Forms.Button
        Me.cbLocation = New System.Windows.Forms.ComboBox
        Me.lblLocation = New System.Windows.Forms.Label
        Me.btnSnapShot = New System.Windows.Forms.Button
        Me.lblOnlyIT = New System.Windows.Forms.Label
        Me.labelLastSyncValue = New System.Windows.Forms.Label
        Me.lblNetworkConnection = New System.Windows.Forms.Label
        Me.btnLocations = New System.Windows.Forms.Button
        Me.Label1 = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'lblTitle
        '
        Me.lblTitle.Font = New System.Drawing.Font("Tahoma", 14.0!, System.Drawing.FontStyle.Bold)
        Me.lblTitle.ForeColor = System.Drawing.Color.Crimson
        Me.lblTitle.Location = New System.Drawing.Point(0, 0)
        Me.lblTitle.Name = "lblTitle"
        Me.lblTitle.Size = New System.Drawing.Size(88, 27)
        Me.lblTitle.Text = "Settings"
        '
        'btnReturn
        '
        Me.btnReturn.Location = New System.Drawing.Point(181, 202)
        Me.btnReturn.Name = "btnReturn"
        Me.btnReturn.Size = New System.Drawing.Size(55, 37)
        Me.btnReturn.TabIndex = 36
        Me.btnReturn.Text = "Return"
        '
        'cbLocation
        '
        Me.cbLocation.Location = New System.Drawing.Point(5, 51)
        Me.cbLocation.Name = "cbLocation"
        Me.cbLocation.Size = New System.Drawing.Size(230, 22)
        Me.cbLocation.TabIndex = 37
        '
        'lblLocation
        '
        Me.lblLocation.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.lblLocation.Location = New System.Drawing.Point(8, 34)
        Me.lblLocation.Name = "lblLocation"
        Me.lblLocation.Size = New System.Drawing.Size(66, 20)
        Me.lblLocation.Text = "Location"
        '
        'btnSnapShot
        '
        Me.btnSnapShot.Location = New System.Drawing.Point(5, 219)
        Me.btnSnapShot.Name = "btnSnapShot"
        Me.btnSnapShot.Size = New System.Drawing.Size(138, 20)
        Me.btnSnapShot.TabIndex = 50
        Me.btnSnapShot.Text = "Download Snapshot"
        '
        'lblOnlyIT
        '
        Me.lblOnlyIT.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.lblOnlyIT.ForeColor = System.Drawing.Color.Red
        Me.lblOnlyIT.Location = New System.Drawing.Point(3, 202)
        Me.lblOnlyIT.Name = "lblOnlyIT"
        Me.lblOnlyIT.Size = New System.Drawing.Size(155, 20)
        Me.lblOnlyIT.Text = "ONLY when directed by IT"
        '
        'labelLastSyncValue
        '
        Me.labelLastSyncValue.Location = New System.Drawing.Point(5, 159)
        Me.labelLastSyncValue.Name = "labelLastSyncValue"
        Me.labelLastSyncValue.Size = New System.Drawing.Size(234, 20)
        '
        'lblNetworkConnection
        '
        Me.lblNetworkConnection.Location = New System.Drawing.Point(8, 135)
        Me.lblNetworkConnection.Name = "lblNetworkConnection"
        Me.lblNetworkConnection.Size = New System.Drawing.Size(212, 42)
        '
        'btnLocations
        '
        Me.btnLocations.Location = New System.Drawing.Point(55, 98)
        Me.btnLocations.Name = "btnLocations"
        Me.btnLocations.Size = New System.Drawing.Size(138, 20)
        Me.btnLocations.TabIndex = 54
        Me.btnLocations.Text = "Download Locations"
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Regular)
        Me.Label1.ForeColor = System.Drawing.Color.Red
        Me.Label1.Location = New System.Drawing.Point(67, 82)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(125, 20)
        Me.Label1.Text = "if Location list is empty"
        '
        'mySettings
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(240, 240)
        Me.Controls.Add(Me.btnLocations)
        Me.Controls.Add(Me.lblNetworkConnection)
        Me.Controls.Add(Me.labelLastSyncValue)
        Me.Controls.Add(Me.btnSnapShot)
        Me.Controls.Add(Me.cbLocation)
        Me.Controls.Add(Me.lblLocation)
        Me.Controls.Add(Me.lblTitle)
        Me.Controls.Add(Me.btnReturn)
        Me.Controls.Add(Me.lblOnlyIT)
        Me.Controls.Add(Me.Label1)
        Me.Location = New System.Drawing.Point(0, 0)
        Me.Name = "mySettings"
        Me.Text = "Settings"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lblTitle As System.Windows.Forms.Label
    Friend WithEvents btnReturn As System.Windows.Forms.Button
    Friend WithEvents cbLocation As System.Windows.Forms.ComboBox
    Friend WithEvents lblLocation As System.Windows.Forms.Label
    Friend WithEvents btnSnapShot As System.Windows.Forms.Button
    Friend WithEvents lblOnlyIT As System.Windows.Forms.Label
    Friend WithEvents labelLastSyncValue As System.Windows.Forms.Label
    Friend WithEvents lblNetworkConnection As System.Windows.Forms.Label
    Friend WithEvents btnLocations As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
End Class
