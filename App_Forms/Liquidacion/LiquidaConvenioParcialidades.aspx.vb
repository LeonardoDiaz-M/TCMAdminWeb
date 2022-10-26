﻿Imports Infragistics.Web.UI.LayoutControls

Public Class LiquidaConvenioParcialidades
    Inherits System.Web.UI.Page
    Dim TipPago As Integer = 1
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("Autenticated") Is Nothing Then
            Me.Response.Redirect("~/Login.aspx")
        End If
        Me.txtClave.Focus()
        Dim cxn As New cxnSQL
        cxn.Select_SQL(Me.ddlFmaPago, "SELECT cve_fma_pago, FormaPagoDesc FROM  tbl_SAT_FmaPago order by id asc", "FormaPagoDesc", "cve_fma_pago")
    End Sub

    Protected Sub Button2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button2.Click
        Try
            Me.DatCont.Visible = False
            Me.DatLiq.Visible = False
            Dim datosCont As New cxnSQL
            Session("suma") = 0
            Session("NumLiq") = 0
            Session("NumRec") = 0
            Me.txtClave.Text = Me.txtClave.Text.ToUpper
            If Me.WDDConceptoPago.SelectedValue = 1 Or Me.WDDConceptoPago.SelectedValue = 4 Then
                If Me.txtClave.Text.Trim.Length = "16" Then
                    datosCont.Select_SQL("select Propietario, domicilio_fiscal,ult_año_pag,ult_mes_pag,forma_pago from arc_predial where cve_Catastral='" & Me.txtClave.Text.Trim.ToUpper & "'")
                Else
                    alerts("La clave catastral digitada está incompleta, verifique", False, Me.litalert)
                End If
            End If
            If Me.WDDConceptoPago.SelectedValue = 2 Then
                If Me.txtClave.Text.Trim.Length = "8" Then
                    datosCont.Select_SQL("select Nombre,domicilio_fiscal,ult_año_pago,ult_mes_pago from arc_agua where num_cuenta='" & Me.txtClave.Text.Trim & "'")
                Else
                    alerts("El número de cuenta digitado está incompleta, verifique", False, Me.litalert)
                End If
            End If
            If Me.WDDConceptoPago.SelectedValue = 3 Then
                If Me.txtClave.Text.Trim.Length = "9" Then
                    datosCont.Select_SQL("select Propietario, domicilio_fiscal,ult_año_pag,ult_mes_pag,forma_pago from arc_predial where cve_Catastral='" & Me.txtClave.Text.Trim.ToUpper & "'")
                Else
                    alerts("La clave digitada está incompleta, verifique", False, Me.litalert)
                End If
            End If
            If datosCont.arrayValores(0) IsNot Nothing Then
                Me.DatCont.Visible = True
                Me.DatLiq.Visible = False
                Me.TxtPropietario.Text = datosCont.arrayValores(0).ToString
                Me.TxtUbicacion.Text = datosCont.arrayValores(1).ToString
                Me.TxtAño.Text = datosCont.arrayValores(2).ToString
                Me.TxtMes.Text = datosCont.arrayValores(3).ToString
            Else
                alerts("Clave inexistente, verifique", False, Me.litalert)
            End If
            Me.txtClave.Focus()
        Catch ex As Exception

        End Try

    End Sub
    Private Sub alerts(ByVal msg As String, ByVal redirect As Boolean, ByVal litalert As Literal)
        Dim txtJS As String = String.Format("<script>alert('{0}');</script>", msg)
        ScriptManager.RegisterClientScriptBlock(litalert, litalert.GetType(), "script", txtJS, False)
    End Sub
    Protected Sub chkSelect_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        ObtenNumRec()
    End Sub

    Private Sub LiquidaConvenioParcialidades_Init(sender As Object, e As EventArgs) Handles Me.Init
        Dim PerIni As String
        Dim PerFin As String
        PerIni = "01-" + Now.Year.ToString
        PerFin = "12-" + Now.Year.ToString
        Me.TxtPerInicio.Text = PerIni
        Me.TxtPerFinal.Text = PerFin
    End Sub
    Protected Sub btnProcesar_Click(sender As Object, e As EventArgs) Handles btnProcesar.Click
        Dim datosCont As New cxnSQL
        Session("suma") = 0
        Session("NumLiq") = 0
        Session("NumRec") = 0
        'alerts("Clave: " + Me.txtClave.Text, False, Me.litalert)
        Try
            Dim cxn1 As New cxnSQL
            cxn1.Select_SQL(Me.grdresults, "exec PagoConveniosDiferencias '" & Me.txtClave.Text & "','" & TipPago.ToString & "','" & Me.WDDConceptoPago.SelectedValue & "','" & Me.WNEImporte.Value & "','" & Me.WNEActualizacion.Value & "','" & Me.WNESaneamiento.Value & "','" & Me.WNERecargos.Value & "','" & Me.WNEMultas.Value & "','" & Me.WNEGastos.Value & "','" & Me.TxtPerInicio.Text & "','" & Me.TxtPerFinal.Text & "','" & Me.txtObservacion.Text & "'")
        Catch ex As Exception
        End Try
        For Each row As GridViewRow In Me.grdresults.Rows
            Session("suma") = Session("suma") + row.Cells(15).Text
            Session("NumLiq") = row.Cells(16).Text
            Session("NumRec") = row.Cells(17).Text
            row.Cells(16).Visible = False
            row.Cells(17).Visible = False
            Dim chk As CheckBox = CType(row.FindControl("chkSelect"), CheckBox)
            chk.Checked = True
        Next
        If CType(Session("NumLiq").ToString, Integer) > 0 Then
            If Me.WDDConceptoPago.SelectedValue = 2 Then
                Me.grdresults.HeaderRow.Cells(1).Text = "AÑO"
                Me.grdresults.HeaderRow.Cells(2).Text = "PER INI"
                Me.grdresults.HeaderRow.Cells(3).Text = "PER FIN"
                Me.grdresults.HeaderRow.Cells(4).Text = "BASE GRAVABLE"
                Me.grdresults.HeaderRow.Cells(5).Text = "IMPORTE"
                Me.grdresults.HeaderRow.Cells(6).Text = "ACTUALIZACIÓN"
                Me.grdresults.HeaderRow.Cells(7).Text = "SANEAMIENTO"
                Me.grdresults.HeaderRow.Cells(8).Text = "RECARGOS"
                Me.grdresults.HeaderRow.Cells(9).Text = "MULTA"
                Me.grdresults.HeaderRow.Cells(10).Text = "GASTOS"
                Me.grdresults.HeaderRow.Cells(11).Text = "SUB IMP"
                Me.grdresults.HeaderRow.Cells(12).Text = "SUB REC"
                Me.grdresults.HeaderRow.Cells(13).Text = "SUB MUL"
                Me.grdresults.HeaderRow.Cells(14).Text = "SUB GST"
                Me.grdresults.HeaderRow.Cells(15).Text = "TOTAL"
                Me.grdresults.HeaderRow.Cells(16).Visible = False
                Me.grdresults.HeaderRow.Cells(17).Visible = False
            Else
                Me.grdresults.HeaderRow.Cells(1).Text = "AÑO"
                Me.grdresults.HeaderRow.Cells(2).Text = "PER INI"
                Me.grdresults.HeaderRow.Cells(3).Text = "PER FIN"
                Me.grdresults.HeaderRow.Cells(4).Text = "BASE GRAVABLE"
                Me.grdresults.HeaderRow.Cells(5).Text = "IMPORTE"
                Me.grdresults.HeaderRow.Cells(6).Text = "ACTUALIZACIÓN"
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
                Me.grdresults.HeaderRow.Cells(17).Visible = False
            End If
            Me.DatLiq.Visible = True
        Else
            alerts("No se pudo generar la liquidación, verifique la información en el padrón correspondiente", False, Me.litalert)
        End If
    End Sub
    Private Sub ObtenNumRec()
        Session("Suma") = 0
        Dim Estado As String() = Session("Estado")
        Dim i As Integer = 0
        Dim band As Boolean = True
        For Each row As GridViewRow In Me.grdresults.Rows
            Dim chk As CheckBox = CType(row.FindControl("chkSelect"), CheckBox)
            If chk.Checked.ToString <> Estado(i).ToString Then
                Session("NumRec") = CType(row.Cells(17).Text, Integer)
                Exit For
            Else
                i = i + 1
            End If
        Next
        For Each row As GridViewRow In Me.grdresults.Rows
            Dim chk As CheckBox = CType(row.FindControl("chkSelect"), CheckBox)
            If CType(row.Cells(17).Text, Integer) <= Session("NumRec") Then
                chk.Checked = True
                Session("Suma") = Session("Suma") + CType(row.Cells(15).Text, Integer)
            Else
                chk.Checked = False
            End If
        Next
        Me.grdresults.HeaderRow.Cells(1).Text = "AÑO"
        Me.grdresults.HeaderRow.Cells(2).Text = "INICIO"
        Me.grdresults.HeaderRow.Cells(3).Text = "FINAL"
        Me.grdresults.HeaderRow.Cells(4).Text = "BASE GRAVABLE"
        Me.grdresults.HeaderRow.Cells(5).Text = "IMPORTE"
        Me.grdresults.HeaderRow.Cells(6).Text = "ACTUALIZACIÓN"
        Me.grdresults.HeaderRow.Cells(7).Text = "SANEAMIENTO"
        Me.grdresults.HeaderRow.Cells(8).Text = "RECARGOS"
        Me.grdresults.HeaderRow.Cells(9).Text = "MULTA"
        Me.grdresults.HeaderRow.Cells(10).Text = "GASTOS"
        Me.grdresults.HeaderRow.Cells(11).Text = "SUB IMP"
        Me.grdresults.HeaderRow.Cells(12).Text = "SUB REC"
        Me.grdresults.HeaderRow.Cells(13).Text = "SUB MUL"
        Me.grdresults.HeaderRow.Cells(14).Text = "SUB GST"
        Me.grdresults.HeaderRow.Cells(15).Text = "TOTAL"
        Me.grdresults.HeaderRow.Cells(16).Visible = False
        Me.grdresults.HeaderRow.Cells(17).Visible = False
    End Sub

    Protected Sub WDDConceptoPago_SelectionChanged(sender As Object, e As Infragistics.Web.UI.ListControls.DropDownSelectionChangedEventArgs) Handles WDDConceptoPago.SelectionChanged
        'alerts("Valor seleccionado: " + Me.WDDConceptoPago.SelectedValue.ToString, False, Me.litalert)
        If Me.WDDConceptoPago.SelectedValue = 0 Then
            alerts("Seleccione un concepto de pago de la lista", False, Me.litalert)
        End If
        If Me.WDDConceptoPago.SelectedValue = 1 Then
            Me.WNEActualizacion.ReadOnly = True
            Me.WNESaneamiento.ReadOnly = True
        Else
            If Me.WDDConceptoPago.SelectedValue = 2 Then
                Me.WNEActualizacion.ReadOnly = False
                Me.WNESaneamiento.ReadOnly = False
            Else
                Me.WNEActualizacion.ReadOnly = False
                Me.WNESaneamiento.ReadOnly = True
            End If
        End If
    End Sub

    Protected Sub chkParcialidad_CheckedChanged(sender As Object, e As EventArgs) Handles chkParcialidad.CheckedChanged
        If Me.chkParcialidad.Checked Then
            Me.chkDiferencia.Checked = False
            TipPago = 1
        End If

    End Sub
    Protected Sub chkDiferencia_CheckedChanged(sender As Object, e As EventArgs) Handles chkDiferencia.CheckedChanged
        If Me.chkDiferencia.Checked Then
            Me.chkParcialidad.Checked = False
            TipPago = 2
        End If
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

End Class