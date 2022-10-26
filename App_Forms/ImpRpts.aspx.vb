Imports Microsoft.VisualBasic
Imports Microsoft.Reporting.WebForms
Imports System.Data.SqlClient
Imports System.Data
Imports System.IO


Partial Class App_Forms_ImpRpts
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim cxn1 As New cxnSQL
        cxn1.Select_SQL("Exec  Rpt_liq_web '" & Session("NumLiq").ToString & "','" & Session("NumRec").ToString & "'")
        Dim Nombre As String = Request.Params("Nombre")
        Me.ReportViewer1.LocalReport.EnableExternalImages = True
        Me.ReportViewer1.LocalReport.ReportPath = Nombre
    End Sub
End Class
