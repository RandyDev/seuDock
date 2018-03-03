Imports System.Data
Imports System.Windows.Forms
Imports System.Threading
Imports System
Imports System.Reflection
Imports System.Xml
Imports System.Collections

' Base custom control for DrawFontList 
Class OwnerDrawnListBox
    Inherits Control
    Private Const SCROLL_WIDTH As Integer = 20
    Private itemH As Integer = -1
    Private selIndex As Integer = -1

    Private offScreenBitmap As Bitmap
    Private vs As VScrollBar
    Private itemsAList As ArrayList


    Public Sub New()
        Me.vs = New VScrollBar
        Me.vs.Parent = Me
        Me.vs.Visible = False
        Me.vs.SmallChange = 1
        AddHandler Me.vs.ValueChanged, AddressOf Me.ScrollValueChanged

        Me.itemsAList = New ArrayList
    End Sub


    Public ReadOnly Property Items() As ArrayList
        Get
            Return Me.itemsAList
        End Get
    End Property


    Protected ReadOnly Property OffScreen() As Bitmap
        Get
            Return Me.offScreenBitmap
        End Get
    End Property


    Protected ReadOnly Property VScrollBar() As VScrollBar
        Get
            Return Me.vs
        End Get
    End Property

    Friend Event SelectedIndexChanged As EventHandler


    Protected Overridable Sub OnSelectedIndexChanged(ByVal e As EventArgs)
        RaiseEvent SelectedIndexChanged(Me, e)
    End Sub

    Protected Overrides Sub OnMouseDown(ByVal e As MouseEventArgs)
        Me.SelectedIndex = Me.vs.Value + e.Y \ Me.ItemHeight
        EnsureVisible(Me.SelectedIndex)
        ' Invalidate the control so we can draw the item as selected. 
        Me.Refresh()
    End Sub

    ' Get or set index of selected item. 
    Public Property SelectedIndex() As Integer
        Get
            Return Me.selIndex
        End Get

        Set(ByVal Value As Integer)
            Me.selIndex = Value

            RaiseEvent SelectedIndexChanged(Me, Nothing)
        End Set
    End Property


    Protected Sub ScrollValueChanged(ByVal o As Object, ByVal e As EventArgs)
        Me.Refresh()
    End Sub


    Protected Overridable Property ItemHeight() As Integer
        Get
            Return Me.itemH
        End Get

        Set(ByVal Value As Integer)
            Me.itemH = Value
        End Set
    End Property


    ' If the requested index is before the first visible index then set the 
    ' first item to be the requested index. If it is after the last visible 
    ' index, then set the last visible index to be the requested index. 
    Public Sub EnsureVisible(ByVal index As Integer)
        If index < Me.vs.Value Then
            Me.vs.Value = index
            Me.Refresh()
        ElseIf index >= Me.vs.Value + Me.DrawCount Then
            Me.vs.Value = index - Me.DrawCount + 1
            Me.Refresh()
        End If
    End Sub


    ' Need to set focus to the control when it  
    ' is clicked so that keyboard events occur. 
    Protected Overrides Sub OnClick(ByVal e As EventArgs)
        Me.Focus()
        MyBase.OnClick(e)
    End Sub

    ' Selected item moves when you use the keyboard up/down keys. 
    Protected Overrides Sub OnKeyDown(ByVal e As KeyEventArgs)
        Select Case e.KeyCode
            Case Keys.Down
                If Me.SelectedIndex < Me.vs.Maximum Then
                    Me.SelectedIndex = Me.SelectedIndex + 1
                    EnsureVisible(Me.SelectedIndex + 1)
                    Me.Refresh()
                End If
            Case Keys.Up
                If Me.SelectedIndex > Me.vs.Minimum Then
                    Me.SelectedIndex = Me.SelectedIndex - 1
                    EnsureVisible(Me.SelectedIndex - 1)
                    Me.Refresh()
                End If
            Case Keys.PageDown
                Me.SelectedIndex = Math.Min(Me.vs.Maximum, Me.SelectedIndex + Me.DrawCount)
                EnsureVisible(Me.SelectedIndex)
                Me.Refresh()
            Case Keys.PageUp
                Me.SelectedIndex = Math.Max(Me.vs.Minimum, Me.SelectedIndex - Me.DrawCount)
                EnsureVisible(Me.SelectedIndex)
                Me.Refresh()
            Case Keys.Home
                Me.SelectedIndex = 0
                EnsureVisible(Me.SelectedIndex)
                Me.Refresh()
            Case Keys.End
                Me.SelectedIndex = Me.itemsAList.Count - 1
                EnsureVisible(Me.SelectedIndex)
                Me.Refresh()
        End Select

        MyBase.OnKeyDown(e)
    End Sub

    ' Calculate how many items we can draw given the height of the control. 
    Protected ReadOnly Property DrawCount() As Integer
        Get
            If Me.vs.Value + Me.vs.LargeChange > Me.vs.Maximum Then
                Return Me.vs.Maximum - Me.vs.Value + 1
            Else
                Return Me.vs.LargeChange
            End If
        End Get
    End Property

    Protected Overrides Sub OnResize(ByVal e As EventArgs)
        Dim viewableItemCount As Integer = Me.ClientSize.Height \ Me.ItemHeight

        Me.vs.Bounds = New Rectangle(Me.ClientSize.Width - SCROLL_WIDTH, 0, SCROLL_WIDTH, Me.ClientSize.Height)


        ' Determine if scrollbars are needed 
        If Me.itemsAList.Count > viewableItemCount Then
            Me.vs.Visible = True
            Me.vs.LargeChange = viewableItemCount
            Me.offScreenBitmap = New Bitmap(Me.ClientSize.Width - SCROLL_WIDTH - 1, Me.ClientSize.Height - 2)
        Else
            Me.vs.Visible = False
            Me.vs.LargeChange = Me.itemsAList.Count
            Me.offScreenBitmap = New Bitmap(Me.ClientSize.Width - 1, Me.ClientSize.Height - 2)
        End If

        Me.vs.Maximum = Me.itemsAList.Count - 1
    End Sub

    Private Sub InitializeComponent()
        Me.SuspendLayout()
        Me.ResumeLayout(False)

    End Sub
End Class

Partial Class FontListBox
    Inherits OwnerDrawnListBox

    Private Const FONT_SIZE As Integer = 10
    Private Const DRAW_OFFSET As Integer = 5


    Public Sub New()

        ' Determine what the item height should be 
        ' by adding 30% padding after measuring 
        ' the letter A with the selected font. 
        Dim g As Graphics = Me.CreateGraphics()
        Me.ItemHeight = Fix(g.MeasureString("A", Me.Font).Height * 1.3)
        g.Dispose()
    End Sub

    ' Return the name of the selected font. 
    Public ReadOnly Property SelectedFaceName() As String
        Get
            Try
                'do compare to number of items
                Return CStr(Me.Items(Me.SelectedIndex))
            Catch ex As Exception
                Return CStr(Me.Items(Me.SelectedIndex - 1))

            End Try
        End Get
    End Property


    ' Determine what the text color should be 
    ' for the selected item drawn as highlighted 
    Function CalcTextColor(ByVal backgroundColor As Color) As Color
        If backgroundColor.Equals(Color.Empty) Then
            Return Color.Black
        End If
        Dim sum As Integer = CType(backgroundColor.R, Integer) + CType(backgroundColor.G, Integer) + CType(backgroundColor.B, Integer)
        Dim light = (255 * 3) / 2
        If sum > 256 Then
            Return Color.Black
        Else
            Return Color.White
        End If
    End Function

    Protected Overrides Sub OnPaint(ByVal e As PaintEventArgs)
        Dim font As Font
        Dim fontColor As Color
        ' The base class contains a bitmap, offScreen, for constructing 
        ' the control and is rendered when all items are populated. 
        ' This technique prevents flicker. 
        Dim gOffScreen As Graphics = Graphics.FromImage(Me.OffScreen)
        gOffScreen.FillRectangle(New SolidBrush(Me.BackColor), Me.ClientRectangle)

        Dim itemTop As Integer = 0

        ' Draw the fonts in the list. 
        Dim n As Integer
        For n = Me.VScrollBar.Value To (Me.VScrollBar.Value + DrawCount) - 1
            ' If the font name contains "dings" it needs to be displayed 
            ' in the list box with a readable font with the default font. 
            If CStr(Me.Items(n)).ToLower().IndexOf("dings") <> -1 Then
                font = New Font(Me.Font.Name, FONT_SIZE, FontStyle.Regular)
            Else
                font = New Font(CStr(Me.Items(n)), FONT_SIZE, FontStyle.Regular)
            End If
            ' Draw the selected item to appear highlighted 
            Dim clr As System.Drawing.Color = Me.ForeColor
            'TO DO - find n in unloadersxml file
            If n = Me.SelectedIndex Then 'you just clicked me
                clr = Color.LawnGreen 'SystemColors.Highlight
                fontColor = Color.Black 'CalcTextColor(clr)
            ElseIf isinCurrentLoad(n) Then 'if on this load
                clr = Color.LimeGreen
                fontColor = Color.Black 'CalcTextColor(clr)
                'ElseIf n = Me.SelectedIndex + 4 Then 'if on another load
                '    clr = Color.Red
                '    fontColor = Color.Black
            Else 'unassigned unloader
                clr = Color.White
                fontColor = Me.ForeColor
            End If
            gOffScreen.FillRectangle(New SolidBrush(clr), 1, itemTop + 1, Me.ClientSize.Width - IIf(Me.VScrollBar.Visible, Me.VScrollBar.Width, 2), Me.ItemHeight)
            ' If the scroll bar is visible, subtract the scrollbar width 
            ' otherwise subtract 2 for the width of the rectangle
            ' Draw the item
            gOffScreen.DrawString(CStr(Me.Items(n)), font, New SolidBrush(fontColor), DRAW_OFFSET, itemTop)
            itemTop += Me.ItemHeight
        Next n

        ' Draw the list box
        e.Graphics.DrawImage(Me.OffScreen, 1, 1)

        gOffScreen.Dispose()
    End Sub
    Public Function isinCurrentLoad(ByVal indx As Integer) As Boolean
        Dim emptbl As DataTable = New DataTable
        emptbl = NewLoad.curemptbl
        If Not emptbl Is Nothing Then

            If emptbl.Rows.Count > 0 Then
                For Each row As DataRow In emptbl.Rows()
                    'get id for this index
                    Dim Name As String = String.Empty
                    Dim id As String = String.Empty
                    ' is this id in the cwo.employee list
                    Dim n = 0
                    If n = indx Then
                        Name = row.Item("Name")
                        id = row.Item("ID").ToString
                        Exit For
                    End If
                    n += 1
                    Dim cwo As WorkOrder = NewLoad.curwo
                    Return cwo.Unloaders.Contains(New Unloader())
                Next

            Else
                Return False
            End If
        Else
            Return False
        End If
    End Function

    ' Draws the external border around the control. 
    Protected Overrides Sub OnPaintBackground(ByVal e As PaintEventArgs)
        e.Graphics.DrawRectangle(New Pen(Color.Black), 0, 0, Me.ClientSize.Width - 1, Me.ClientSize.Height - 1)
    End Sub
End Class



' Derive an implementation of the 
' OwnerDrawnListBox class 
