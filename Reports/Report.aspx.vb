﻿Imports System.Data
Imports System.Configuration
Imports System.Data.SqlClient
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports Microsoft.Reporting.WebForms
Imports Microsoft.ReportingServices.ReportProcessing.OnDemandReportObjectModel
Imports log4net
Imports CrystalDecisions.Web

Public Class Report
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim cxn1 As New cxnSQL
        If Session("Autenticated") Is Nothing Then
            Me.Response.Redirect("~/Login.aspx")
        End If
        If Not Me.IsPostBack Then
            Dim reporte As String = ""
            Select Case Session("Modulo")
                Case "Predial"
                    reporte = "rpliquidaPredial.rdlc"
                Case "Licencias"
                    reporte = ""
                Case "Agua"
                    store = "App_InsertaTransaccion"
                Case "Otros"
            End Select
            If cxn1.Select_SQL("Exec  Rpt_liq_web '" & Session("NumLiq").ToString & "','" & Session("NumRec").ToString & "'") Then
                If Session("ImprimePago") = 1 Then
                    Me.CrystalReportViewer1.Visible = False
                    Me.ReportViewer1.Visible = True
                    Load_RDLC(reporte) ' Se imprime el reporte de acuerdo al Modulo
                ElseIf Session("ImprimePago") = 2 Then
                    Me.CrystalReportViewer1.Visible = True
                    Me.ReportViewer1.Visible = False
                    Load_Crystal() 'Se imprime el reporte generico para comprobante de pago
                End If
            Else
                alerts("No se pudo generar el reporte<br/>" & cxn1.arrayValores(0), False, Me.litalert)
            End If
        End If
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
                                where t.id_liq= " & Session("NumLiq").ToString &
                               "       and tl.num_rec<=" & Session("NumRec").ToString, ds)
        ReportViewer1.ProcessingMode = ProcessingMode.Local
        ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/" & reporte)
        ReportViewer1.LocalReport.DataSources.Clear()
        Dim DataSet1 As ReportDataSource = New ReportDataSource("Rpt_Liq_Web", ds.Tables(0))
        DataSet1.Name = "DataSet1"
        ReportViewer1.LocalReport.DataSources.Add(DataSet1)
        ReportViewer1.LocalReport.Refresh()
    End Sub
    Private Sub Load_Crystal()
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
        CrystalReportViewer1.PrintMode = CrystalDecisions.Web.PrintMode.ActiveX
        CrystalReportViewer1.ReportSource = crystalReport
    End Sub
End Class