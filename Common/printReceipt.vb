Imports System.Data
Imports System.Data.SqlServerCe
Imports System.IO
Imports System.Xml
Imports SEUdock.Utils
Imports FieldSoftware.PrinterCE_NetCF

Public Class printReceipt

    Public Sub PrintReciept(ByVal wo As WorkOrder)
        Dim prce As PrinterCE = Nothing
        Try
            ' Load company data

            ' Prepare for printing.
            ' library initialization (licence key is transfered as a parameter)
            prce = New PrinterCE("721M5J968R")
            ' No License Key for evaluation: prce = new PrinterCE();
            prce.SelectPrinter(True)
            ' Select printer for current task.
            prce.ScaleMode = PrinterCE_Base.MEASUREMENT_UNITS.MILLIMETERS
            prce.DrawWidth = 0.5
            prce.FontName = "Tahoma"
            prce.FontSize = -14

            Dim strHeight As Double = prce.GetStringHeight
            Dim loadBoxTop As Double = 0
            Dim loadBoxLeft As Double = 0
            Dim loadBoxColumnWidth As Double = prce.PrPgWidth / 3
            Dim addrBoxWidth As Double = 60
            Dim addrBoxHeight As Double = 25
            Dim addrBox2Width As Double = 50
            Dim addrBox2Height As Double = 25
            Dim s As [String] = ""


            'Print top part of receipt.
            prce.FontBold = False
            If wo.LoadType = "Invoice" Then
                prce.DrawText("INVOICE RECEIPT")
            Else
                prce.DrawText("ORIGINAL RECEIPT")
            End If
            prce.FontSize = -12
            prce.FontBold = True
            's = ci.Name + " "
            s = "Southeast Unloading"
            prce.DrawText(s)

            '            s = ci.AddressOne + vbLf + ci.City + ", " + ci.State + " " + ci.Zip + vbLf + ci.AddressTwo
            s = "1984 South 14th St." & vbCrLf & "Fernandina Beach, 32034"
            loadBoxTop = prce.TextY
            prce.DrawTextFlow(s, 0, prce.TextY, addrBoxWidth, addrBoxHeight, -1, _
             PrinterCE.FLOW_OPTIONS.[DEFAULT])
            loadBoxTop = Math.Max(prce.TextY + strHeight, loadBoxTop + addrBoxHeight)

            Try
                Dim strAppDir As [String] = "\Program Files\SEUdock\"
                Dim strFullPathToLogo As [String] = Path.Combine(strAppDir, "seulogo.bmp")
                ' prce.DrawPicture(strFullPathToLogo, addrBoxWidth + 10, 0, 40, 20, true);
                prce.DrawPicture(strFullPathToLogo, addrBoxWidth + 10, 0)
            Catch ex As Exception
                MessageBox.Show(ex.Message, "Exception")
            End Try

            '' Line 1.
            prce.JustifyHoriz = PrinterCE_Base.JUSTIFY_HORIZ.CENTER
            prce.TextY = prce.TextY + strHeight
            prce.FontSize = -10
            prce.FontBold = False
            If wo.LoadType = "Invoice" Then
                prce.DrawText("This is NOT a Bill, do not Pay from this document", loadBoxLeft + 87, loadBoxTop - strHeight - 4)
            End If
            prce.DrawLine(loadBoxLeft, loadBoxTop - (strHeight / 2), prce.PrPgWidth, loadBoxTop - (strHeight / 2))
            prce.TextY = prce.TextY + strHeight

            s = "Phone: (904)491-6800" + vbLf + "Tax ID: 593746670" + vbLf + Date.Now.ToShortDateString

            prce.FontBold = False
            prce.TextY = 0
            prce.DrawTextFlow(s, prce.PrPgWidth - addrBox2Width, 0, addrBox2Width, addrBox2Height, -1, _
             PrinterCE.FLOW_OPTIONS.[DEFAULT])

            '' Print bottom part of receipt.
            '' Column 1
            prce.FontBold = True
            prce.JustifyHoriz = PrinterCE_Base.JUSTIFY_HORIZ.LEFT
            prce.DrawText("Date:", loadBoxLeft, loadBoxTop)
            prce.JustifyHoriz = PrinterCE_Base.JUSTIFY_HORIZ.LEFT
            prce.FontBold = False
            s = wo.LogDate.ToShortDateString
            prce.DrawText(s, loadBoxLeft + 26, loadBoxTop)

            Dim utl As New Utils

            s = utl.getxmldata("Location_Name")
            prce.FontBold = True
            prce.JustifyHoriz = PrinterCE_Base.JUSTIFY_HORIZ.LEFT
            prce.DrawText("Location:", loadBoxLeft, loadBoxTop + 1 * strHeight)
            prce.JustifyHoriz = PrinterCE_Base.JUSTIFY_HORIZ.LEFT
            prce.FontBold = False
            prce.DrawText(s, loadBoxLeft + 26, loadBoxTop + 1 * strHeight)

            prce.FontBold = True
            prce.JustifyHoriz = PrinterCE_Base.JUSTIFY_HORIZ.LEFT
            prce.DrawText("Department:", loadBoxLeft, loadBoxTop + 2 * strHeight)
            prce.JustifyHoriz = PrinterCE_Base.JUSTIFY_HORIZ.LEFT
            prce.FontBold = False
            prce.DrawText(wo.Department, loadBoxLeft + 26, loadBoxTop + 2 * strHeight)

            prce.FontBold = True
            prce.JustifyHoriz = PrinterCE_Base.JUSTIFY_HORIZ.LEFT
            prce.DrawText("Receipt:", loadBoxLeft, loadBoxTop + 3 * strHeight)
            prce.JustifyHoriz = PrinterCE_Base.JUSTIFY_HORIZ.LEFT
            prce.FontBold = False
            prce.DrawText(Convert.ToString(wo.ReceiptNumber), loadBoxLeft + 26, loadBoxTop + 3 * strHeight)

            prce.FontBold = True
            prce.JustifyHoriz = PrinterCE_Base.JUSTIFY_HORIZ.LEFT
            prce.DrawText("Door #:", loadBoxLeft, loadBoxTop + 4 * strHeight)
            prce.JustifyHoriz = PrinterCE_Base.JUSTIFY_HORIZ.LEFT
            prce.FontBold = False
            prce.DrawText(wo.DoorNumber, loadBoxLeft + 26, loadBoxTop + 4 * strHeight)

            prce.FontBold = True
            prce.JustifyHoriz = PrinterCE_Base.JUSTIFY_HORIZ.LEFT
            prce.DrawText("Load Description:", loadBoxLeft, loadBoxTop + 5.5 * strHeight)
            prce.DrawText(" ", loadBoxLeft, loadBoxTop + 1 + 4.5 * strHeight)
            prce.JustifyHoriz = PrinterCE_Base.JUSTIFY_HORIZ.LEFT
            prce.FontBold = False
            prce.DrawTextFlow(Convert.ToString(wo.LoadDescription), loadBoxLeft + 40, 30, prce.PrPgWidth - (loadBoxLeft + 45), loadBoxTop + 5.5 * strHeight, -1, _
             PrinterCE.FLOW_OPTIONS.[DEFAULT])
            Dim showtimes As Boolean = utl.getxmldata("Print_TimeStamp") = "True"
            If showtimes Then
                Dim stim As String = wo.StartTime.ToShortTimeString
                Dim ctim As String = wo.CompTime.ToShortTimeString
                prce.FontSize = -10
                prce.DrawText("Start Time: " & stim & "   Completion Time: " & ctim, loadBoxLeft, loadBoxTop + 7.4 * strHeight)
                prce.FontSize = -10
            End If
            '' loadBoxTop + 2.5 * strHeight,

            '' Column 2
            prce.FontBold = True
            prce.JustifyHoriz = PrinterCE_Base.JUSTIFY_HORIZ.LEFT
            prce.DrawText("Carrier:", loadBoxLeft + loadBoxColumnWidth, loadBoxTop)
            prce.JustifyHoriz = PrinterCE_Base.JUSTIFY_HORIZ.LEFT
            prce.FontBold = False
            prce.DrawText(wo.CarrierName, loadBoxLeft + loadBoxColumnWidth + 20, loadBoxTop)

            prce.FontBold = True
            prce.JustifyHoriz = PrinterCE_Base.JUSTIFY_HORIZ.LEFT
            prce.DrawText("Truck #:", loadBoxLeft + loadBoxColumnWidth, loadBoxTop + 1 * strHeight)
            prce.JustifyHoriz = PrinterCE_Base.JUSTIFY_HORIZ.LEFT
            prce.FontBold = False
            prce.DrawText(wo.TruckNumber, loadBoxLeft + loadBoxColumnWidth + 21, loadBoxTop + 1 * strHeight)

            prce.FontBold = True
            prce.JustifyHoriz = PrinterCE_Base.JUSTIFY_HORIZ.LEFT
            prce.DrawText("Trailer #:", loadBoxLeft + loadBoxColumnWidth, loadBoxTop + 2 * strHeight)
            prce.JustifyHoriz = PrinterCE_Base.JUSTIFY_HORIZ.LEFT
            prce.FontBold = False
            prce.DrawText(wo.TrailerNumber, loadBoxLeft + loadBoxColumnWidth + 23, loadBoxTop + 2 * strHeight)


            prce.FontBold = True
            prce.JustifyHoriz = PrinterCE_Base.JUSTIFY_HORIZ.LEFT
            prce.DrawText("Bad Pallets:", loadBoxLeft + loadBoxColumnWidth, loadBoxTop + 3 * strHeight)
            prce.JustifyHoriz = PrinterCE_Base.JUSTIFY_HORIZ.LEFT
            prce.FontBold = False
            prce.DrawText(Convert.ToString(wo.BadPallets), loadBoxLeft + loadBoxColumnWidth + 27, loadBoxTop + 3 * strHeight)

            prce.FontBold = True
            prce.JustifyHoriz = PrinterCE_Base.JUSTIFY_HORIZ.LEFT
            prce.DrawText("Restacks:", loadBoxLeft + loadBoxColumnWidth, loadBoxTop + 4 * strHeight)
            prce.JustifyHoriz = PrinterCE_Base.JUSTIFY_HORIZ.LEFT
            prce.FontBold = False
            prce.DrawText(Convert.ToString(wo.Restacks), loadBoxLeft + loadBoxColumnWidth + 27, loadBoxTop + 4 * strHeight)

            '' Column 3
            Dim x As Long = 0
            If wo.CheckNumber > "" Then
                If wo.LoadType = "Creditcard" Then
                    prce.FontBold = True
                    prce.JustifyHoriz = PrinterCE_Base.JUSTIFY_HORIZ.RIGHT
                    prce.DrawText("Transaction #:", loadBoxLeft + 2 * loadBoxColumnWidth + 30, loadBoxTop)
                Else
                    prce.FontBold = True
                    prce.JustifyHoriz = PrinterCE_Base.JUSTIFY_HORIZ.RIGHT
                    prce.DrawText("Check #:", loadBoxLeft + 2 * loadBoxColumnWidth + 30, loadBoxTop)
                End If
                prce.JustifyHoriz = PrinterCE_Base.JUSTIFY_HORIZ.LEFT
                prce.FontBold = False

                ' s = TimeStringFormat(load.GateTime.Hour, load.GateTime.Minute);
                s = wo.CheckNumber
                prce.DrawText(s, loadBoxLeft + 2 * loadBoxColumnWidth + 32, loadBoxTop + x)
                x += 1
            End If
            ''
            If wo.CheckNumber > "" And wo.SplitPaymentAmount > 0 Then
                Dim chamount As Decimal = wo.Amount - wo.SplitPaymentAmount.ToString
                prce.FontBold = True
                prce.JustifyHoriz = PrinterCE_Base.JUSTIFY_HORIZ.RIGHT
                prce.DrawText("Check Amount:", loadBoxLeft + 2 * loadBoxColumnWidth + 30, loadBoxTop + x * strHeight)
                prce.JustifyHoriz = PrinterCE_Base.JUSTIFY_HORIZ.LEFT
                prce.FontBold = False
                s = "$" & chamount.ToString
                prce.DrawText(s, loadBoxLeft + 2 * loadBoxColumnWidth + 32, loadBoxTop + x * strHeight)
                ''                 
                x += 1
            End If

            If wo.SplitPaymentAmount > 0 Then
                prce.FontBold = True
                prce.JustifyHoriz = PrinterCE_Base.JUSTIFY_HORIZ.RIGHT
                prce.DrawText("Cash Amount:", loadBoxLeft + 2 * loadBoxColumnWidth + 30, loadBoxTop + x * strHeight)
                prce.JustifyHoriz = PrinterCE_Base.JUSTIFY_HORIZ.LEFT
                prce.FontBold = False
                s = "$" + wo.SplitPaymentAmount.ToString
                prce.DrawText(s, loadBoxLeft + 2 * loadBoxColumnWidth + 32, loadBoxTop + x * strHeight)
                x += 1
            End If

            prce.FontBold = True
            prce.JustifyHoriz = PrinterCE_Base.JUSTIFY_HORIZ.RIGHT
            prce.DrawText(" " & vbCrLf)
            If wo.LoadType = "Invoice" Then
                prce.DrawText("Invoice Amount:", loadBoxLeft + 2 * loadBoxColumnWidth + 30, loadBoxTop + x * strHeight)
            Else
                prce.DrawText("Total Amount:", loadBoxLeft + 2 * loadBoxColumnWidth + 30, loadBoxTop + x * strHeight)

            End If

            prce.JustifyHoriz = PrinterCE_Base.JUSTIFY_HORIZ.LEFT
            prce.FontBold = False
            s = "$" + wo.Amount.ToString
            prce.DrawText(s, loadBoxLeft + 2 * loadBoxColumnWidth + 32, loadBoxTop + x * strHeight)
            x += 1

            prce.FontBold = True
            prce.JustifyHoriz = PrinterCE_Base.JUSTIFY_HORIZ.RIGHT
            prce.DrawText("Purchase Order:", loadBoxLeft + 2 * loadBoxColumnWidth + 30, loadBoxTop + x * strHeight)
            prce.JustifyHoriz = PrinterCE_Base.JUSTIFY_HORIZ.LEFT
            prce.FontBold = False
            s = Convert.ToString(wo.PurchaseOrder)
            prce.DrawText(s, loadBoxLeft + 2 * loadBoxColumnWidth + 32, loadBoxTop + x * strHeight)
            x += 1


            '' Line 2
            prce.DrawLine(loadBoxLeft, loadBoxTop + 7 * strHeight, prce.PrPgWidth, loadBoxTop + 7 * strHeight)
            prce.FontSize = -9

            prce.JustifyHoriz = PrinterCE_Base.JUSTIFY_HORIZ.RIGHT
            prce.DrawText("validation guid: " & wo.ID.ToString, loadBoxLeft + 2 * loadBoxColumnWidth + 63, loadBoxTop + 7.4 * strHeight)
            '' Final

            ''Done with this page - print it
            prce.EndDoc()
        Catch exc As PrinterCEException
            MessageBox.Show(exc.Message, "Exception")
        Finally
            'Need to always call ShutDown()
            If prce IsNot Nothing Then
                'Done - free PrinterCE resources
                prce.ShutDown()
            End If
            prce = Nothing
        End Try

    End Sub
End Class
