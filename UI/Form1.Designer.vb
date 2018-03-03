<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class Form1
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
        Me.lblVersion = New System.Windows.Forms.Label
        Me.lbl = New System.Windows.Forms.Label
        Me.lblLocalTime = New System.Windows.Forms.Label
        Me.lblLocalDate = New System.Windows.Forms.Label
        Me.BtnSettings = New System.Windows.Forms.Button
        Me.BtnNewLoad = New System.Windows.Forms.Button
        Me.BtnLoadEditor = New System.Windows.Forms.Button
        Me.BtnTimeAttendance = New System.Windows.Forms.Button
        Me.BtnExit = New System.Windows.Forms.Button
        Me.CtrlConn1 = New SEUdock.ctrlConn
        Me.Timer1 = New System.Windows.Forms.Timer
        Me.SuspendLayout()
        '
        'lblVersion
        '
        Me.lblVersion.Font = New System.Drawing.Font("Tahoma", 7.0!, System.Drawing.FontStyle.Regular)
        Me.lblVersion.Location = New System.Drawing.Point(4, 16)
        Me.lblVersion.Name = "lblVersion"
        Me.lblVersion.Size = New System.Drawing.Size(34, 16)
        Me.lblVersion.Text = "v2.0.0"
        '
        'lbl
        '
        Me.lbl.Font = New System.Drawing.Font("Tahoma", 11.0!, System.Drawing.FontStyle.Bold)
        Me.lbl.Location = New System.Drawing.Point(0, 0)
        Me.lbl.Name = "lbl"
        Me.lbl.Size = New System.Drawing.Size(71, 18)
        Me.lbl.Text = "SEUdock"
        '
        'lblLocalTime
        '
        Me.lblLocalTime.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Bold)
        Me.lblLocalTime.Location = New System.Drawing.Point(80, 24)
        Me.lblLocalTime.Name = "lblLocalTime"
        Me.lblLocalTime.Size = New System.Drawing.Size(141, 18)
        Me.lblLocalTime.Text = "Mon - 15:07 PM "
        Me.lblLocalTime.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblLocalDate
        '
        Me.lblLocalDate.Font = New System.Drawing.Font("Tahoma", 10.0!, System.Drawing.FontStyle.Bold)
        Me.lblLocalDate.Location = New System.Drawing.Point(96, 1)
        Me.lblLocalDate.Name = "lblLocalDate"
        Me.lblLocalDate.Size = New System.Drawing.Size(92, 21)
        Me.lblLocalDate.Text = "2/22/14"
        Me.lblLocalDate.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'BtnSettings
        '
        Me.BtnSettings.Font = New System.Drawing.Font("Tahoma", 14.0!, System.Drawing.FontStyle.Regular)
        Me.BtnSettings.Location = New System.Drawing.Point(5, 155)
        Me.BtnSettings.Name = "BtnSettings"
        Me.BtnSettings.Size = New System.Drawing.Size(230, 30)
        Me.BtnSettings.TabIndex = 23
        Me.BtnSettings.Text = "Settings"
        '
        'BtnNewLoad
        '
        Me.BtnNewLoad.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.BtnNewLoad.Font = New System.Drawing.Font("Tahoma", 14.0!, System.Drawing.FontStyle.Regular)
        Me.BtnNewLoad.Location = New System.Drawing.Point(5, 50)
        Me.BtnNewLoad.Name = "BtnNewLoad"
        Me.BtnNewLoad.Size = New System.Drawing.Size(230, 30)
        Me.BtnNewLoad.TabIndex = 22
        Me.BtnNewLoad.Text = "New Load"
        '
        'BtnLoadEditor
        '
        Me.BtnLoadEditor.Font = New System.Drawing.Font("Tahoma", 14.0!, System.Drawing.FontStyle.Regular)
        Me.BtnLoadEditor.ForeColor = System.Drawing.Color.Black
        Me.BtnLoadEditor.Location = New System.Drawing.Point(5, 85)
        Me.BtnLoadEditor.Name = "BtnLoadEditor"
        Me.BtnLoadEditor.Size = New System.Drawing.Size(230, 30)
        Me.BtnLoadEditor.TabIndex = 20
        Me.BtnLoadEditor.Text = "Load Editor"
        '
        'BtnTimeAttendance
        '
        Me.BtnTimeAttendance.Font = New System.Drawing.Font("Tahoma", 14.0!, System.Drawing.FontStyle.Regular)
        Me.BtnTimeAttendance.Location = New System.Drawing.Point(5, 120)
        Me.BtnTimeAttendance.Name = "BtnTimeAttendance"
        Me.BtnTimeAttendance.Size = New System.Drawing.Size(230, 30)
        Me.BtnTimeAttendance.TabIndex = 17
        Me.BtnTimeAttendance.Text = "Time && Attendance"
        '
        'BtnExit
        '
        Me.BtnExit.Font = New System.Drawing.Font("Tahoma", 16.0!, System.Drawing.FontStyle.Regular)
        Me.BtnExit.Location = New System.Drawing.Point(30, 199)
        Me.BtnExit.Name = "BtnExit"
        Me.BtnExit.Size = New System.Drawing.Size(170, 30)
        Me.BtnExit.TabIndex = 13
        Me.BtnExit.Text = "Exit"
        '
        'CtrlConn1
        '
        Me.CtrlConn1.BackColor = System.Drawing.Color.LemonChiffon
        Me.CtrlConn1.Location = New System.Drawing.Point(192, 0)
        Me.CtrlConn1.Name = "CtrlConn1"
        Me.CtrlConn1.Size = New System.Drawing.Size(48, 18)
        Me.CtrlConn1.TabIndex = 24
        '
        'Timer1
        '
        Me.Timer1.Enabled = True
        Me.Timer1.Interval = 30000
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(240, 240)
        Me.Controls.Add(Me.lblLocalTime)
        Me.Controls.Add(Me.CtrlConn1)
        Me.Controls.Add(Me.BtnSettings)
        Me.Controls.Add(Me.BtnNewLoad)
        Me.Controls.Add(Me.BtnLoadEditor)
        Me.Controls.Add(Me.BtnTimeAttendance)
        Me.Controls.Add(Me.BtnExit)
        Me.Controls.Add(Me.lblVersion)
        Me.Controls.Add(Me.lblLocalDate)
        Me.Controls.Add(Me.lbl)
        Me.Location = New System.Drawing.Point(0, 0)
        Me.Name = "Form1"
        Me.Text = "Form1"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lblVersion As System.Windows.Forms.Label
    Friend WithEvents lbl As System.Windows.Forms.Label
    Friend WithEvents lblLocalTime As System.Windows.Forms.Label
    Friend WithEvents lblLocalDate As System.Windows.Forms.Label
    Friend WithEvents BtnSettings As System.Windows.Forms.Button
    Friend WithEvents BtnNewLoad As System.Windows.Forms.Button
    Friend WithEvents BtnLoadEditor As System.Windows.Forms.Button
    Friend WithEvents BtnTimeAttendance As System.Windows.Forms.Button
    Friend WithEvents BtnExit As System.Windows.Forms.Button
    Friend WithEvents CtrlConn1 As SEUdock.ctrlConn
    Friend WithEvents Timer1 As System.Windows.Forms.Timer

End Class
