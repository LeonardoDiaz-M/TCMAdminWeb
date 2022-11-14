Public Class LiquidaTraslado
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("Autenticated") Is Nothing Then
            Me.Response.Redirect("~/Login.aspx")
        End If
        If Not IsPostBack Then

        End If
        If Not Me.IsPostBack Then
            Me.DatCont.Visible = False
            Me.DatLiq.Visible = False
            Me.Button2.Visible = True
            Me.txtCveCat.Visible = True
            Me.WDFechaOperacion.Visible = True
            Me.txtMontoOperacion.Visible = True
            Me.chkCopropiedad.Visible = True
            Me.chkUsufructo.Visible = True
            Me.usrConfirmaPago.Visible = True
            Me.usrConfirmaPago.modal = False
            Me.txtCveCat.Focus()
            Session("ImprimePago") = 0
            Session("ModalVisble") = 0
            Session("Modulo") = "Agua"
            Session("SQLStore") = "App_InsertaTransaccion"
        End If
        If Session("ModalVisble") IsNot Nothing Then
            If Session("ModalVisble") = 2 Then
                Me.txtCveCat.Text = ""
                Me.DatCont.Visible = False
                Me.DatLiq.Visible = False
                Me.TxtPropietario.Text = ""
                Me.TxtUbicacion.Text = ""
                Me.TxtAño.Text = ""
                Me.TxtMes.Text = ""
                Session("suma") = 0
                Session("NumLiq") = 0
                Session("NumRec") = 0
                Me.pnlBtns.Visible = False
            End If
        End If
    End Sub
    Protected Sub Button2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.DatCont.Visible = False
        Me.DatLiq.Visible = False
        Dim datosCont As New cxnSQL
        Session("suma") = 0
        Session("NumLiq") = 0
        Session("NumRec") = 0
        Me.pnlBtns.Visible = False
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
                    Me.pnlBtns.Visible = True
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
                    Me.pnlBtns.Visible = False
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
        Session("ModalVisble") = 1
        Session("NumRecReport") = Session("NumRec")
        Session("NumLiqReport") = Session("NumLiq")
        Me.usrConfirmaPago.modal = True
    End Sub

    Protected Sub btnImprimir_Click(sender As Object, e As EventArgs) Handles btnImprimir.Click
        Session("ImprimePago") = 1  '1-Imprime, 2-Paga
        Session("idSATCuenta") = 1
        Session("Modulo") = "Agua"
        Session("NumRecReport") = Session("NumRec")
        Session("NumLiqReport") = Session("NumLiq")
        ReportWindow()
    End Sub
    Private Sub ReportWindow()
        Dim txtJS As String = "<script>window.open(""http://" & Request.ServerVariables("HTTP_HOST") & "/Reports/Reporte.aspx"",""Reporte de Liquidación"", 'toolbars=0,width=600,height=600,left=200,top=200,scrollbars=1,resizable=1,toolbar=0,status=0,menubar=0');</script>"
        ScriptManager.RegisterClientScriptBlock(litalert, litalert.GetType(), "script", txtJS, False)
    End Sub

End Class