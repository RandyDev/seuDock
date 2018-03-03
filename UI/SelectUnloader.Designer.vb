<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class SelectUnloader
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
        Me.btnAddUnloader = New System.Windows.Forms.Button
        Me.BtnCancel = New System.Windows.Forms.Button
        Me.lblSelectItems = New System.Windows.Forms.Label
        Me.lbAvailableUnloaders = New System.Windows.Forms.ListBox
        Me.SuspendLayout()
        '
        'btnAddUnloader
        '
        Me.btnAddUnloader.Location = New System.Drawing.Point(5, 217)
        Me.btnAddUnloader.Name = "btnAddUnloader"
        Me.btnAddUnloader.Size = New System.Drawing.Size(105, 20)
        Me.btnAddUnloader.TabIndex = 138
        Me.btnAddUnloader.Text = "Add Selection"
        '
        'BtnCancel
        '
        Me.BtnCancel.Location = New System.Drawing.Point(164, 217)
        Me.BtnCancel.Name = "BtnCancel"
        Me.BtnCancel.Size = New System.Drawing.Size(72, 20)
        Me.BtnCancel.TabIndex = 139
        Me.BtnCancel.Text = "Cancel"
        '
        'lblSelectItems
        '
        Me.lblSelectItems.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold)
        Me.lblSelectItems.Location = New System.Drawing.Point(7, 3)
        Me.lblSelectItems.Name = "lblSelectItems"
        Me.lblSelectItems.Size = New System.Drawing.Size(186, 20)
        Me.lblSelectItems.Text = "Select Unloader"
        '
        'lbAvailableUnloaders
        '
        Me.lbAvailableUnloaders.Location = New System.Drawing.Point(5, 27)
        Me.lbAvailableUnloaders.Name = "lbAvailableUnloaders"
        Me.lbAvailableUnloaders.Size = New System.Drawing.Size(230, 184)
        Me.lbAvailableUnloaders.TabIndex = 140
        '
        'SelectUnloader
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(240, 240)
        Me.Controls.Add(Me.lbAvailableUnloaders)
        Me.Controls.Add(Me.lblSelectItems)
        Me.Controls.Add(Me.BtnCancel)
        Me.Controls.Add(Me.btnAddUnloader)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Location = New System.Drawing.Point(0, 0)
        Me.Name = "SelectUnloader"
        Me.Text = "Select Unloader"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnAddUnloader As System.Windows.Forms.Button
    Friend WithEvents BtnCancel As System.Windows.Forms.Button
    Friend WithEvents lblSelectItems As System.Windows.Forms.Label
    Friend WithEvents lbAvailableUnloaders As System.Windows.Forms.ListBox
End Class
