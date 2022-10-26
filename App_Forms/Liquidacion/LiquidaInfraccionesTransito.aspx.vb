Imports Infragistics.Web.UI.LayoutControls

Public Class LiquidaInfraccionesTransito
    Inherits System.Web.UI.Page

    Protected Sub Button2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button2.Click
        Session("suma") = 0
        Session("NumLiq") = 0
        Session("NumRec") = 0
        Me.DatLiq.Visible = True
        Me.TxtNombre.Text = Me.TxtNombre.Text.ToUpper
        Me.TxtDireccion.Text = Me.TxtDireccion.Text.ToUpper
        Me.TxtRFC.Text = Me.TxtRFC.Text.ToUpper
        Me.TxtObservacion.Text = Me.TxtObservacion.Text.ToUpper
        Try
            Dim cxn1 As New cxnSQL
            cxn1.Select_SQL(Me.grdresults, "CalculaInfraccionesTransito '" & Me.ddlDerechos.SelectedValue & "','" & Me.TxtDatoUno.Value.ToString & "','" & Me.TxtDatoDos.Value.ToString & "','" & Me.TxtNombre.Text & "','" & Me.TxtDireccion.Text & "','" & Me.TxtRFC.Text & "','" & Me.WDFechaInfraccion.Value.ToString & "'")
        Catch ex As Exception
        End Try
        For Each row As GridViewRow In Me.grdresults.Rows
            Session("suma") = Session("suma") + row.Cells(14).Text
            Session("NumLiq") = row.Cells(15).Text
            Session("NumRec") = row.Cells(16).Text
            row.Cells(15).Visible = False
            row.Cells(16).Visible = False
            Dim chk As CheckBox = CType(row.FindControl("chkSelect"), CheckBox)
            chk.Checked = True
        Next
        If CType(Session("NumLiq").ToString, Integer) > 0 Then
            Me.grdresults.HeaderRow.Cells(1).Text = "AÑO"
            Me.grdresults.HeaderRow.Cells(2).Text = "PER INI"
            Me.grdresults.HeaderRow.Cells(3).Text = "PER FIN"
            Me.grdresults.HeaderRow.Cells(4).Text = "BASE"
            Me.grdresults.HeaderRow.Cells(5).Text = "IMPORTE"
            Me.grdresults.HeaderRow.Cells(6).Text = "ACT IMP"
            Me.grdresults.HeaderRow.Cells(7).Text = "RECARGOS"
            Me.grdresults.HeaderRow.Cells(8).Text = "MULTA"
            Me.grdresults.HeaderRow.Cells(9).Text = "GASTOS"
            Me.grdresults.HeaderRow.Cells(10).Text = "SUB IMP"
            Me.grdresults.HeaderRow.Cells(11).Text = "SUB REC"
            Me.grdresults.HeaderRow.Cells(12).Text = "SUB MUL"
            Me.grdresults.HeaderRow.Cells(13).Text = "SUB GST"
            Me.grdresults.HeaderRow.Cells(14).Text = "TOTAL"
            Me.grdresults.HeaderRow.Cells(15).Visible = False
            Me.grdresults.HeaderRow.Cells(16).Visible = False
        Else

            alerts("No se pudo generar la liquidación, verifique la información", False, Me.litalert)
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("Autenticated") Is Nothing Then
            Me.Response.Redirect("~/App_Forms/Logins.aspx")
        End If
        Me.TxtNombre.Focus()
        Dim cxn As New cxnSQL
        cxn.Select_SQL(Me.ddlFmaPago, "SELECT cve_fma_pago, FormaPagoDesc FROM  tbl_SAT_FmaPago order by id asc", "FormaPagoDesc", "cve_fma_pago")
    End Sub

    Protected Sub ddlDerechos_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlDerechos.SelectedIndexChanged
        Dim datosCont As New cxnSQL
        Session("suma") = 0
        datosCont.Select_SQL("select dato_uno,msj_uno,dato_dos,msj_dos from tbl_derechos where derecho_id=" & Me.ddlDerechos.SelectedValue.ToString)
        If datosCont.arrayValores(0) = True Then
            Me.lblDatoUno.Text = datosCont.arrayValores(1).ToString
            Me.lblDatoUno.Visible = True
            Me.TxtDatoUno.Visible = True
        Else
            Me.lblDatoUno.Visible = False
            Me.TxtDatoUno.Visible = False
        End If
        If datosCont.arrayValores(2) = True Then
            Me.lblDatoDos.Text = datosCont.arrayValores(3).ToString
            Me.lblDatoDos.Visible = True
            Me.TxtDatoDos.Visible = True
        Else
            Me.lblDatoDos.Visible = False
            Me.TxtDatoDos.Visible = False
        End If
    End Sub
    Private Sub ObtenNumRec()
        For Each row As GridViewRow In Me.grdresults.Rows
            Dim chk As CheckBox = CType(row.FindControl("chkSelect"), CheckBox)
            If chk.Checked Then
                Session("NumRec") = CType(row.Cells(14).Text, Integer)
            Else
                Exit For
            End If
        Next
    End Sub

    Protected Sub chkSelect_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        ObtenNumRec()
    End Sub
    Private Sub alerts(ByVal msg As String, ByVal redirect As Boolean, ByVal litalert As Literal)
        Dim txtJS As String = String.Format("<script>alert('{0}');</script>", msg)
        ScriptManager.RegisterClientScriptBlock(litalert, litalert.GetType(), "script", txtJS, False)
    End Sub

    Private Sub LiquidaInfraccionesTransito_Init(sender As Object, e As EventArgs) Handles Me.Init
        Me.WDFechaInfraccion.Value = Now
        Me.ddlDerechos.SelectedIndex = 2
    End Sub

    Protected Sub btnContinuar_Click(sender As Object, e As EventArgs) Handles btnContinuar.Click
        Me.txtTotalModal.Text = Session("suma")
        Me.windowModal.Visible = True
    End Sub
    Protected Sub btnPagar_Click(sender As Object, e As EventArgs) Handles btnPagar.Click
        If Me.btnPagar.Text = "Finalizar" Then
            Session("ReportFileName") = "Reportes\rptPago.rdlc"
            Session("ReportTitle") = "COMPROBANTE DE PAGO "
            Me.Response.Redirect("~/Reports/Reportes.aspx")
        Else
            Me.lblErrorModal.ForeColor = Drawing.Color.Green
            Me.lblErrorModal.Visible = True
            Dim cxnPago As New cxnSQL
            If cxnPago.Execute_SQL("Exec [App_InsertaTransaccion] " & Session("NumLiq") & "," &
                                                                    Session("CajaFolio") &
                                                                    ",'" & Session("CajaNum") & "'," &
                                                                    Session("idOficina") & ",1," &
                                                                    Me.ddlFmaPago.SelectedValue.ToString
                                                                    ) Then
                Me.windowModal.Header.CloseBox.Visible = False
                Me.btnPagar.Text = "Finalizar"
            Else
                Me.lblErrorModal.ForeColor = Drawing.Color.Red
                    Me.lblErrorModal.Visible = True
                    Me.lblErrorModal.Text = "Error al procesar pago, " & cxnPago.arrayValores(0)
                    Me.btnPagar.Visible = False
                End If
            End If
    End Sub

    Private Sub TxtObservacion_TextChanged(sender As Object, e As EventArgs) Handles TxtObservacion.TextChanged
        Me.TxtObservacion.Text = Me.TxtObservacion.Text.ToUpper
    End Sub
End Class