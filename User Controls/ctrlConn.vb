Imports System.DateTime
Imports System.IO
Imports System.Data.SqlServerCe
Imports SEUdock.Utils
Imports System.Xml
Imports System.Data
Imports System.Runtime
Imports System.Runtime.InteropServices
Imports System.Net


Public Class ctrlConn
    Inherits UserControl

    Public utl As New Utils
    Private ser As Integer
    Private repl As New SqlCeReplication
    Dim syncInterval As Integer = 45000 '90 seconds
    Public Event connFailed(ByVal Reason As Integer)

    Public Enum ConnectionStatus
        Offline
        OnlineTargetNotFound
        OnlineTargetFound
    End Enum

    Public Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
    End Sub

End Class
