Public Class LiquidaTraslado
    Inherits System.Web.UI.Page

    Protected Sub Button2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.DatCont.Visible = False
        Me.DatLiq.Visible = False
        Dim datosCont As New cxnSQL
        Session("suma") = 0
        Session("NumLiq") = 0
        Session("NumRec") = 0
        If Me.txtCveCat.Text.Trim.Length = "16" Then
            datosCont.Select_SQL("select Propietario, Ubicacion,ult_año_pag,ult_mes_pag,forma_pago from arc_predial where cve_Catastral='" & Me.txtCveCat.Text.Trim & "'")
            If datosCont.arrayValores(0) IsNot Nothing Then
                Me.DatCont.Visible = True
                Me.DatLiq.Visible = True
                Me.TxtPropietario.Text = datosCont.arrayValores(0).ToString
                Me.TxtUbicacion.Text = datosCont.arrayValores(1).ToString
                Me.TxtAño.Text = datosCont.arrayValores(2).ToString
                Me.TxtMes.Text = datosCont.arrayValores(3).ToString
                Dim Coopropiedad As String = IIf(Me.chkCopropiedad.Checked = False, 0, 1).ToString
                Dim CambioUsu As String = IIf(Me.chkUsufructo.Checked = False, 0, 1).ToString
                Dim requerido As String = IIf(Me.chkNotificado.Checked = False, 0, 1).ToString
                Try
                    Dim cxn1 As New cxnSQL
                    cxn1.Select_SQL(grdresults, "exec Calcula_traslado '" & txtCveCat.Text.Trim & "','" & (Me.DDLTipoTraslado.SelectedItemIndex + 1).ToString & "','" & (Me.DDLTipoVivienda.SelectedItemIndex + 1).ToString & "',0,'" & WDFechaOperacion.Text.Trim & "','" & Me.txtMontoOperacion.Value.ToString & "' ,'" & CambioUsu & "' ,'" & Coopropiedad & "', '" & Me.txtporcentaje.Value.ToString & "','" & requerido & "','WEBUSER'")
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
                Me.lblTotal.Text = Session("suma")
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
                    Me.grdresults.HeaderRow.Cells(10).Text = "Sub IMP"
                    Me.grdresults.HeaderRow.Cells(11).Text = "Sub REC"
                    Me.grdresults.HeaderRow.Cells(12).Text = "Sub MUL"
                    Me.grdresults.HeaderRow.Cells(13).Text = "Sub GST"
                    Me.grdresults.HeaderRow.Cells(14).Text = "TOTAL"
                    Me.grdresults.HeaderRow.Cells(15).Visible = False
                    Me.grdresults.HeaderRow.Cells(16).Visible = False
                Else
                    alerts("No se pudo generar la liquidación, verifique la información en el padrón", False, Me.litalert)
                End If
            Else
                alerts("Clave catastral inexistente, verifique", False, Me.litalert)
            End If
        Else
            alerts("La clave catastral digitada está incompleta, verifique", False, Me.litalert)
        End If
        Me.txtCveCat.Focus()
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("Autenticated") Is Nothing Then
            Me.Response.Redirect("~/Login.aspx")
        End If
        If Not IsPostBack Then
            Me.DatCont.Visible = False
            Me.DatLiq.Visible = False
            Me.Button2.Visible = True
            Me.txtCveCat.Visible = True
            Me.WDFechaOperacion.Visible = True
            Me.txtMontoOperacion.Visible = True
            Me.chkCopropiedad.Visible = True
            Me.chkUsufructo.Visible = True
            Dim cxn As New cxnSQL
            cxn.Select_SQL(Me.ddlFmaPago, "SELECT cve_fma_pago, FormaPagoDesc FROM  tbl_SAT_FmaPago order by id asc", "FormaPagoDesc", "cve_fma_pago")
        End If

    End Sub

    Protected Sub chkCopropiedad_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkCopropiedad.CheckedChanged
        If Me.chkCopropiedad.Checked Then
            Me.lblPorcentaje.Visible = True
            Me.txtporcentaje.Visible = True
        Else
            Me.lblPorcentaje.Visible = False
            Me.txtporcentaje.Text = "0"
            Me.txtporcentaje.Visible = False
        End If
    End Sub

    Private Sub alerts(ByVal msg As String, ByVal redirect As Boolean, ByVal litalert As Literal)
        Dim txtJS As String = String.Format("<script>alert('{0}');</script>", msg)
                ScriptManager.RegisterClientScriptBlock(litalert, litalert.GetType(), "script", txtJS, False)
    End Sub

    Protected Sub DDLTipoTraslado_SelectionChanged(sender As Object, e As Infragistics.Web.UI.ListControls.DropDownSelectionChangedEventArgs) Handles DDLTipoTraslado.SelectionChanged
        If Me.DDLTipoTraslado.SelectedItemIndex = 1 Then
            Me.lblTipoVivienda.Visible = True
            Me.DDLTipoVivienda.Visible = True
        Else
            Me.DDLTipoVivienda.Visible = False
            Me.lblTipoVivienda.Visible = False
        End If
    End Sub

    Private Sub LiquidaTraslado_Init(sender As Object, e As EventArgs) Handles Me.Init
        Me.WDFechaOperacion.Value = Now
    End Sub


    Protected Sub btnContinuar_Click(sender As Object, e As EventArgs) Handles btnContinuar.Click
        Me.txtTotalModal.Text = Session("suma")
        Me.windowModal.Visible = True
    End Sub
    Protected Sub btnPagar_Click(sender As Object, e As EventArgs) Handles btnPagar.Click
        If Me.btnPagar.Text = "Finalizar" Then
            Session("ReportFileName") = "Reportes\rptPago.rdlc"
            Session("ReportTitle") = "RECIBO DE PAGO "
            Me.Response.Redirect("~/App_Forms/Reportes.aspx")
        Else
            Me.lblErrorModal.ForeColor = Drawing.Color.Green
                Me.lblErrorModal.Visible = True

                Dim cxnPago As New cxnSQL
            If cxnPago.Execute_SQL("Exec [App_InsertaTransaccion] " & Session("NumLiq") & "," &
                                                                    Session("CajaFolio") &
                                                                    ",'" & Session("CajaNum") & "'," &
                                                                    Session("idOficina") & ",1," &
                                                                    Me.ddlFmaPago.SelectedValue.ToString()
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

End Class