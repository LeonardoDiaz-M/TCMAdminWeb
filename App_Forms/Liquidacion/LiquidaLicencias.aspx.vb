﻿Public Class LiquidaLicencias
    Inherits System.Web.UI.Page

    Protected Sub Button2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button2.Click
        Dim datosCont As New cxnSQL
        Session("suma") = 0
        Session("NumLiq") = 0
        Session("NumRec") = 0
        Me.txtCveCta.Text = Me.txtCveCta.Text.ToUpper
        If Me.txtCveCta.Text.Substring(0, 2) = "LA" Then
            If Me.txtCveCta.Text.Trim.Length = "9" Then
                Dim Cta As String
                Cta = Me.txtCveCta.Text.Trim
                datosCont.Select_SQL("select Nombre,Responsable,calle,no_ext,ult_año_pago,status from tbl_lic_municipales where cve_Licencia='" & Cta & "'")
                If datosCont.arrayValores(0) IsNot Nothing Then
                    Me.DatCont.Visible = True
                    Me.DatLiq.Visible = True
                    Me.TxtNombre.Text = datosCont.arrayValores(0).ToString
                    Me.TxtPropietario.Text = datosCont.arrayValores(1).ToString
                    Me.TxtUbicacion.Text = datosCont.arrayValores(2).Trim & " " & datosCont.arrayValores(3).Trim
                    Me.TxtAño.Text = datosCont.arrayValores(4).ToString
                    Me.rbTipoTramite.SelectedValue = IIf(CType(datosCont.arrayValores(5).ToString, Integer) = 2, 1, 2)
                    If datosCont.arrayValores(5).ToString <> "2" And datosCont.arrayValores(5).ToString <> "1" Then
                        alerts("No se puede generar la liquidación, status de la licencia = " & CType(datosCont.arrayValores(5), String), False, Me.litalert)
                        Return
                    End If
                    If CType(Me.TxtAño.Text, Integer) = Now.Year Then
                        alerts("El usuario no tiene adeudos fiscales registrados", False, Me.litalert)
                    Else
                        Try
                            Dim cxn1 As New cxnSQL
                            cxn1.Select_SQL(Me.grdresults, "Exec Calc_Liq_Lic_Alcoholes '" & Cta & "'")
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
                        If ObNumReg() > 5 Then
                            Me.Panel1.Height = 195
                        Else
                            Me.Panel1.Height = 95%
                        End If
                        If CType(Session("NumLiq").ToString, Integer) > 0 Then
                            ChecaEstado()
                            ObtenNumRec()
                        Else
                            alerts("No se pudo generar la liquidación, verifique la información en el padrón", False, Me.litalert)
                            Me.DatCont.Visible = False
                            Me.DatLiq.Visible = False
                        End If
                    End If
                Else
                    alerts("Número de licencia inexistente, verifique", False, Me.litalert)
                End If
            Else
                alerts("El número de licencia digitado está incompleto, verifique", False, Me.litalert)
            End If
        Else
            alerts("El número de licencia digitado es incorrecto, verifique", False, Me.litalert)
        End If
        Me.txtCveCta.Focus()
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("Autenticated") Is Nothing Then
            Me.Response.Redirect("~/Login.aspx")
        End If
        Me.DatCont.Visible = False
        Me.DatLiq.Visible = False
        Me.txtCveCta.Focus()
        Dim cxn As New cxnSQL
        cxn.Select_SQL(Me.ddlFmaPago, "SELECT cve_fma_pago, FormaPagoDesc FROM  tbl_SAT_FmaPago order by id asc", "FormaPagoDesc", "cve_fma_pago")
    End Sub
    Private Sub ChecaEstado()
        Dim i As Integer = 0
        Dim Estado As String() = {""}
        For Each row As GridViewRow In Me.grdresults.Rows
            Dim chk As CheckBox = CType(row.FindControl("chkSelect"), CheckBox)
            Estado(i) = chk.Checked.ToString
            Array.Resize(Estado, Estado.Length + 1)
            i = i + 1
        Next
        Session("Estado") = Estado
    End Sub
    Private Sub ObtenNumRec()
        Session("Suma") = 0
        Dim Estado As String() = Session("Estado")
        Dim i As Integer = 0
        Dim band As Boolean = True
        For Each row As GridViewRow In Me.grdresults.Rows
            Dim chk As CheckBox = CType(row.FindControl("chkSelect"), CheckBox)
            If chk.Checked.ToString <> Estado(i).ToString Then
                Session("NumRec") = CType(row.Cells(15).Text, Integer)
                Exit For
            Else
                i = i + 1
            End If
        Next
        For Each row As GridViewRow In Me.grdresults.Rows
            Dim chk As CheckBox = CType(row.FindControl("chkSelect"), CheckBox)
            If CType(row.Cells(16).Text, Integer) <= Session("NumRec") Then
                chk.Checked = True
                Session("Suma") = Session("Suma") + CType(row.Cells(15).Text, Integer)
            Else
                chk.Checked = False
            End If
        Next
        ChecaEstado()
        Me.grdresults.HeaderRow.Cells(1).Text = "AÑO"
        Me.grdresults.HeaderRow.Cells(2).Text = "PER INI"
        Me.grdresults.HeaderRow.Cells(3).Text = "PER FIN"
        Me.grdresults.HeaderRow.Cells(4).Text = "CUENTA"
        Me.grdresults.HeaderRow.Cells(5).Text = "BASE GRAV"
        Me.grdresults.HeaderRow.Cells(6).Text = "IMPORTE"
        Me.grdresults.HeaderRow.Cells(7).Text = "ACT IMP"
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

    Private Function ObNumReg() As Integer
        Dim Numreg As Integer
        Numreg = 0
        For Each row As GridViewRow In Me.grdresults.Rows
            Dim chk As CheckBox = CType(row.FindControl("chkSelect"), CheckBox)
            If chk.Checked Then
                Numreg = Numreg + 1
            Else
                Exit For
            End If
        Next
        Return Numreg
    End Function

    Protected Sub chkSelect_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        ObtenNumRec()
        Me.DatLiq.Visible = True
    End Sub
    Private Sub alerts(ByVal msg As String, ByVal redirect As Boolean, ByVal litalert As Literal)
        Dim txtJS As String = String.Format("<script>alert('{0}');</script>", msg)
        ScriptManager.RegisterClientScriptBlock(litalert, litalert.GetType(), "script", txtJS, False)
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