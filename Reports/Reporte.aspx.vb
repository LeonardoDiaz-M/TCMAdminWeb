Imports System.IO
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.ReportAppServer.ReportDefModel
Imports CrystalDecisions.Shared
Imports Microsoft.Reporting.WebForms
'Imports CrystalDecisions.CrystalReports.Engine.ReportDocument

Public Class Reporte
    Inherits System.Web.UI.Page
    Dim crystalReport As New CrystalDecisions.CrystalReports.Engine.ReportDocument
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim cxn1 As New cxnSQL
        cxn1.Select_SQL("select Valor from tb_WEBParametros where Parametro  ='LogMpioRpt'")
        Session("imgpath") = cxn1.arrayValores(0)
        If Session("Autenticated") Is Nothing Then
            Me.Response.Redirect("~/Login.aspx")
        End If
        'If Not Me.IsPostBack Then
        Dim reporte As String = ""
            Select Case Session("Modulo")
                Case "Predial"
                    reporte = "rpliquidaPredial.rdlc"
                Case "LicAnuncio"
                    reporte = "rpliquidaAnuncios.rdlc"
                Case "Agua"
                    reporte = "rpliquidaAgua.rdlc"
                Case "Derecho"
                    reporte = "rpliquidaDerechos.rdlc"
                Case "Estacionamiento"
                    reporte = "rpliquidaEstacionamientos.rdlc"
                Case "Traslado"
                    reporte = "rpliquidaTraslado.rdlc"
                Case "VideoJuego"
                    reporte = "rpliquidaVideoJuegos.rdlc"
                Case "Licencias"
                    reporte = "rpliquidaLicencias.rdlc"
                Case "Convenios"
                    reporte = "rpliquidaDerechos.rdlc"
                Case Else
                    alerts("Error, no se encontro reporte para el modulo: " & Session("Modulo"), False, Me.litalert)
            End Select

        If Session("ImprimePago") = 1 Then
            If Not Me.IsPostBack Then
                Me.CrystalReportViewer1.Visible = False
                Me.ReportViewer1.Visible = True
                Me.pnlCrystal.Visible = False
                Load_RDLC(reporte) ' Se imprime el reporte de acuerdo al Modulo
            End If
        ElseIf Session("ImprimePago") = 2 Then
            Me.CrystalReportViewer1.Visible = True
            Me.pnlCrystal.Visible = True
            Me.ReportViewer1.Visible = False
            Load_Crystal() 'Se imprime el reporte generico para comprobante de pago
        End If
        'End If
    End Sub
    Private Sub alerts(ByVal msg As String, ByVal redirect As Boolean, ByVal litalert As Literal)
        Dim txtJS As String = String.Format("<script>alert('{0}');</script>", msg)
        ScriptManager.RegisterClientScriptBlock(litalert, litalert.GetType(), "script", txtJS, False)
    End Sub
    Private Sub Load_RDLC(reporte As String)
        Dim ds As New DataSet
        Dim cxn1 As New cxnSQL
        ds = cxn1.Select_SQL("select * 
                                from  datos_mpio d,tab_liquidaciones t
                                    inner join tab_det_liquidaciones tl on tl.id_liq=t.id_liq
                                where t.id_liq= " & Session("NumLiqReport").ToString &
                               "       and tl.num_rec<=" & Session("NumRecReport").ToString, ds)
        ReportViewer1.ProcessingMode = ProcessingMode.Local
        ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/" & reporte)
        ReportViewer1.LocalReport.DataSources.Clear()
        Dim DataSet1 As ReportDataSource = New ReportDataSource("Rpt_Liq_Web", ds.Tables(0))
        DataSet1.Name = "DataSet1"
        ReportViewer1.LocalReport.DataSources.Add(DataSet1)
        ReportViewer1.LocalReport.EnableExternalImages = True
        Dim parameter As New ReportParameter("Cajero", Session("UserName").ToString)
        Dim parameter2 As New ReportParameter("Img_Path", Session("imgpath").ToString)
        ReportViewer1.LocalReport.SetParameters(parameter)
        ReportViewer1.LocalReport.SetParameters(parameter2)
        ReportViewer1.LocalReport.Refresh()
    End Sub
    Private Sub Load_Crystal()

        Dim dsMpio As New DataSet
        Dim dsCmd As New DataSet
        Dim tblCmd As New DataTable
        Dim tblMpio As New DataTable
        Dim dstabLiq As New DataSet
        Dim tblLiq As New DataTable
        Dim dstabLiqDet As New DataSet
        Dim tblLiqDet As New DataTable
        Dim cxn1 As New cxnSQL
        Dim dsPago As New DataSet
        dsCmd = cxn1.Select_SQL("select '" & Session("UserName") & "' as Cajero,'" & Session("imgpath") & "' as imgPath", dsCmd)
        dsMpio = cxn1.Select_SQL("select * from datos_mpio", dsMpio)
        dstabLiq = cxn1.Select_SQL("select * from tab_liquidaciones where id_Liq=" & Session("NumLiqReport").ToString, dstabLiq)
        dstabLiqDet = cxn1.Select_SQL("select * from tab_det_liquidaciones where id_Liq=" & Session("NumLiqReport").ToString & " and num_rec<=" & Session("NumRecReport").ToString, dstabLiqDet)
        tblMpio = dsMpio.Tables(0).Copy()
        tblMpio.TableName = "datos_mpio"
        tblLiq = dstabLiq.Tables(0).Copy()
        tblLiq.TableName = "tab_liquidaciones"
        tblLiqDet = dstabLiqDet.Tables(0).Copy()
        tblLiqDet.TableName = "tab_det_liquidaciones"
        tblCmd = dsCmd.Tables(0).Copy()
        tblCmd.TableName = "Command"
        dsPago.Tables.Add(tblCmd)
        dsPago.Tables.Add(tblMpio)
        dsPago.Tables.Add(tblLiq)
        dsPago.Tables.Add(tblLiqDet)
        crystalReport.Load(Server.MapPath("~/Reports/RptPago.rpt"))
        crystalReport.SetDatabaseLogon("MpioSQL", "Administrator1#", "svrsqltricat.database.windows.net", "TCM_Admin")
        crystalReport.SetDataSource(dsPago)
        crystalReport.Refresh()
        CrystalReportViewer1.PrintMode = CrystalDecisions.Web.PrintMode.Pdf
        CrystalReportViewer1.ReportSource = crystalReport
        CrystalReportViewer1.DataBind()
    End Sub

    Protected Sub btnPDF_Click(sender As Object, e As EventArgs) Handles btnPDF.Click
        'crystalReport.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat)
        crystalReport.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, True, "ExportedReport")

    End Sub

    Protected Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        crystalReport.SetDatabaseLogon("MpioSQL", "Administrator1#", "svrsqltricat.database.windows.net", "TCM_Admin")
        crystalReport.PrintToPrinter(1, False, 0, 0)
    End Sub
End Class
