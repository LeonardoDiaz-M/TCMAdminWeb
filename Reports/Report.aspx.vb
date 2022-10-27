Imports System.Data
Imports System.Configuration
Imports System.Data.SqlClient
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared

Public Class Report
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("Autenticated") Is Nothing Then
            Me.Response.Redirect("~/Login.aspx")
        End If
        Dim crystalReport As New ReportDocument
        Dim dsMpio As New DataSet
        Dim tblMpio As New DataTable
        Dim dstabLiq As New DataSet
        Dim tblLiq As New DataTable
        Dim dstabLiqDet As New DataSet
        Dim tblLiqDet As New DataTable
        Dim ParamLists As ParameterFields = New ParameterFields()
        Dim cxn1 As New cxnSQL
        Dim dsPago As New DataSet
        Dim store As String = ""
        Select Case Request.Params("Modulo")
            Case "Predial"
                store = "App_InsTranPredial"
            Case "Lic"
            Case "Agua"
                store = "App_InsertaTransaccion"
            Case "Otros"
        End Select

        If cxn1.Select_SQL("Exec " & store & " " & Session("NumLiq").ToString & ",'" & Session("NumCaja") & "','" & Session("idOficina") & "','" & Session("idSATCuenta").ToString & "'") Then
            If Session("ImprimePago") = 2 Then
                cxn1.Select_SQL("Exec  Rpt_liq_web '" & Session("NumLiq").ToString & "','" & Session("NumRec").ToString & "'")
            End If
            dsMpio = cxn1.Select_SQL("select * from datos_mpio", dsMpio)
            dstabLiq = cxn1.Select_SQL("select * from tab_liquidaciones where id_Liq=" & Session("NumLiq").ToString, dstabLiq)
            dstabLiqDet = cxn1.Select_SQL("select * from tab_det_liquidaciones where id_Liq=" & Session("NumLiq").ToString & " and num_rec<=" & Session("NumRec").ToString, dstabLiqDet)
            tblMpio = dsMpio.Tables(0).Copy()
            tblMpio.TableName = "datos_mpio"
            tblLiq = dstabLiq.Tables(0).Copy()
            tblLiq.TableName = "tab_liquidaciones"
            tblLiqDet = dstabLiqDet.Tables(0).Copy()
            tblLiqDet.TableName = "tab_det_liquidaciones"
            dsPago.Tables.Add(tblMpio)
            dsPago.Tables.Add(tblLiq)
            dsPago.Tables.Add(tblLiqDet)

            crystalReport.Load(Server.MapPath("~/Reports/RptPagos.rpt"))
            crystalReport.SetDatabaseLogon("MpioSQL", "Administrator1#", "svrsqltricat.database.windows.net", "TCM_Admin")
            crystalReport.SetDataSource(dsPago)
            crystalReport.Refresh()
            'crystalReport.ExportToDisk(ExportFormatType.PortableDocFormat, "c:\temp\data.pdf")
            CrystalReportViewer1.ReportSource = crystalReport
            'CrystalReportViewer1.RefreshReport()
        Else
            alerts("No se pudo generar el reporte<br/>" & cxn1.arrayValores(0), False, Me.litalert)
        End If
    End Sub
    Private Sub alerts(ByVal msg As String, ByVal redirect As Boolean, ByVal litalert As Literal)
        Dim txtJS As String = String.Format("<script>alert('{0}');</script>", msg)
        ScriptManager.RegisterClientScriptBlock(litalert, litalert.GetType(), "script", txtJS, False)
    End Sub
End Class