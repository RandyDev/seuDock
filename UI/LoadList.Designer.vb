<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class LoadList
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
        Me.btnExit = New System.Windows.Forms.Button
        Me.Label1 = New System.Windows.Forms.Label
        Me.dgloadlist = New System.Windows.Forms.DataGrid
        Me.DataGridTableStyle1 = New System.Windows.Forms.DataGridTableStyle
        Me.DataGridTextBoxColumn1 = New System.Windows.Forms.DataGridTextBoxColumn
        Me.DataGridTextBoxColumn2 = New System.Windows.Forms.DataGridTextBoxColumn
        Me.DataGridTextBoxColumn3 = New System.Windows.Forms.DataGridTextBoxColumn
        Me.lblempName = New System.Windows.Forms.Label
        Me.btnInProgress = New System.Windows.Forms.Button
        Me.btnCompleted = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'btnExit
        '
        Me.btnExit.Location = New System.Drawing.Point(5, 203)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(107, 23)
        Me.btnExit.TabIndex = 17
        Me.btnExit.Text = "<Back to Main"
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 14.0!, System.Drawing.FontStyle.Bold)
        Me.Label1.Location = New System.Drawing.Point(0, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(100, 20)
        Me.Label1.Text = "Load List"
        '
        'dgloadlist
        '
        Me.dgloadlist.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.dgloadlist.Location = New System.Drawing.Point(0, 26)
        Me.dgloadlist.Name = "dgloadlist"
        Me.dgloadlist.RowHeadersVisible = False
        Me.dgloadlist.Size = New System.Drawing.Size(228, 171)
        Me.dgloadlist.TabIndex = 23
        Me.dgloadlist.TableStyles.Add(Me.DataGridTableStyle1)
        '
        'DataGridTableStyle1
        '
        Me.DataGridTableStyle1.GridColumnStyles.Add(Me.DataGridTextBoxColumn1)
        Me.DataGridTableStyle1.GridColumnStyles.Add(Me.DataGridTextBoxColumn2)
        Me.DataGridTableStyle1.GridColumnStyles.Add(Me.DataGridTextBoxColumn3)
        Me.DataGridTableStyle1.MappingName = "dtLoadList"
        '
        'DataGridTextBoxColumn1
        '
        Me.DataGridTextBoxColumn1.Format = ""
        Me.DataGridTextBoxColumn1.HeaderText = "po"
        Me.DataGridTextBoxColumn1.MappingName = "PO#"
        Me.DataGridTextBoxColumn1.NullText = """---"""
        '
        'DataGridTextBoxColumn2
        '
        Me.DataGridTextBoxColumn2.Format = ""
        Me.DataGridTextBoxColumn2.HeaderText = "dr"
        Me.DataGridTextBoxColumn2.MappingName = "Door#"
        Me.DataGridTextBoxColumn2.Width = 25
        '
        'DataGridTextBoxColumn3
        '
        Me.DataGridTextBoxColumn3.Format = ""
        Me.DataGridTextBoxColumn3.HeaderText = "Status"
        Me.DataGridTextBoxColumn3.MappingName = "Status"
        Me.DataGridTextBoxColumn3.Width = 100
        '
        'lblempName
        '
        Me.lblempName.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.lblempName.Location = New System.Drawing.Point(93, 4)
        Me.lblempName.Name = "lblempName"
        Me.lblempName.Size = New System.Drawing.Size(100, 14)
        Me.lblempName.Text = "In Progress"
        Me.lblempName.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'btnInProgress
        '
        Me.btnInProgress.Location = New System.Drawing.Point(116, 203)
        Me.btnInProgress.Name = "btnInProgress"
        Me.btnInProgress.Size = New System.Drawing.Size(117, 23)
        Me.btnInProgress.TabIndex = 26
        Me.btnInProgress.Text = "Loads in Progress"
        Me.btnInProgress.Visible = False
        '
        'btnCompleted
        '
        Me.btnCompleted.Location = New System.Drawing.Point(116, 203)
        Me.btnCompleted.Name = "btnCompleted"
        Me.btnCompleted.Size = New System.Drawing.Size(117, 23)
        Me.btnCompleted.TabIndex = 29
        Me.btnCompleted.Text = "Completed Loads"
        '
        'LoadList
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(240, 240)
        Me.Controls.Add(Me.lblempName)
        Me.Controls.Add(Me.dgloadlist)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.btnInProgress)
        Me.Controls.Add(Me.btnCompleted)
        Me.Location = New System.Drawing.Point(0, 0)
        Me.Name = "LoadList"
        Me.Text = "LoadList"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents dgloadlist As System.Windows.Forms.DataGrid
    Friend WithEvents lblempName As System.Windows.Forms.Label
    Friend WithEvents btnInProgress As System.Windows.Forms.Button
    Friend WithEvents btnCompleted As System.Windows.Forms.Button
    Friend WithEvents DataGridTableStyle1 As System.Windows.Forms.DataGridTableStyle
    Friend WithEvents DataGridTextBoxColumn1 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn2 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn3 As System.Windows.Forms.DataGridTextBoxColumn
End Class
