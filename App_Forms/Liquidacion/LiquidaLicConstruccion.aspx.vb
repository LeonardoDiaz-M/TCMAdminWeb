Public Class LiquidaLicConstruccion
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("Autenticated") Is Nothing Then
            Me.Response.Redirect("~/Login.aspx")
        End If
        If Not Me.IsPostBack Then
            Me.DatCont.Visible = False
            Me.DatLiq.Visible = False
            Me.usrConfirmaPago.Visible = True
            Me.usrConfirmaPago.modal = False
            Me.txtCveCta.Focus()
            Session("ImprimePago") = 0
            Session("ModalVisble") = 0
            Session("Modulo") = "Licencias"
            Session("SQLStore") = "App_InsTranLicencias"
        End If
        If Session("ModalVisble") IsNot Nothing Then
            If Session("ModalVisble") = 2 Then
                Me.txtCveCta.Text = ""
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
        Dim datosCont As New cxnSQL
        Session("suma") = 0
        Session("NumLiq") = 0
        Session("NumRec") = 0
        Me.txtCveCta.Text = Me.txtCveCta.Text.ToUpper
        Me.Panel1.Visible = False
        Me.pnlBtns.Visible = False
        If Me.txtCveCta.Text.Substring(0, 2) = "LC" Then
            If Me.txtCveCta.Text.Trim.Length = "9" Then
                Dim Cta As String
                Cta = Me.txtCveCta.Text.Trim
                datosCont.Select_SQL("select Nombre,Responsable,calle,no_ext,ult_año_pago,ult_mes_pago,dimension from tbl_lic_municipales where cve_Licencia='" & Cta & "'")
                If datosCont.arrayValores(0) IsNot Nothing Then
                    Me.DatCont.Visible = True
                    Me.DatLiq.Visible = True
                    Me.TxtNombre.Text = datosCont.arrayValores(0).ToString
                    Me.TxtPropietario.Text = datosCont.arrayValores(1).ToString
                    Me.TxtUbicacion.Text = datosCont.arrayValores(2).Trim & " " & datosCont.arrayValores(3).Trim
                    Me.TxtAño.Text = datosCont.arrayValores(4).ToString
                    Me.TxtMes.Text = datosCont.arrayValores(5).ToString
                    Me.TxtDimension.Text = datosCont.arrayValores(6).ToString
                    Try
                        Dim cxn1 As New cxnSQL
                        cxn1.Select_SQL(Me.grdresults, "CalcDerLicDeConstruccion '" & Cta & "','" & Me.rbTipoTramite.SelectedValue.ToString & "','" & Me.TxtNumMeses.Text & "'")
                    Catch ex As Exception
                    End Try
                    For Each row As GridViewRow In Me.grdresults.Rows
                        Session("suma") = Session("suma") + row.Cells(12).Text
                        Session("NumLiq") = row.Cells(15).Text
                        Session("NumRec") = row.Cells(16).Text
                        row.Cells(15).Visible = False
                        row.Cells(16).Visible = False
                        Dim chk As CheckBox = CType(row.FindControl("chkSelect"), CheckBox)
                        chk.Checked = True
                    Next
                    If CType(Session("NumLiq").ToString, Integer) > 0 Then
                        Me.lblTotal.Text = "Total: " & Session("Suma").ToString
                        Session("ModalVisble") = 1
                        Me.pnlBtns.Visible = True
                        Me.grdresults.HeaderRow.Cells(1).Text = "AÑO"
                        Me.grdresults.HeaderRow.Cells(2).Text = "PER INI"
                        Me.grdresults.HeaderRow.Cells(3).Text = "PER FIN"
                        Me.grdresults.HeaderRow.Cells(4).Text = "SUP CONST"
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
                        alerts("No se pudo generar la liquidación, verifique la información en el padrón", False, Me.litalert)
                        Me.Panel1.Visible = False
                        Me.DatCont.Visible = False
                        Me.DatLiq.Visible = False
                        Me.pnlBtns.Visible = False
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

    Protected Sub RadioButtonList1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbTipoTramite.SelectedIndexChanged
        If Me.rbTipoTramite.SelectedValue = 2 Then
            Me.TxtNumMeses.Visible = True
            Me.lblMeses.Visible = True
        Else
            Me.TxtNumMeses.Visible = False
            Me.lblMeses.Visible = False
        End If
    End Sub
    Private Sub alerts(ByVal msg As String, ByVal redirect As Boolean, ByVal litalert As Literal)
        Dim txtJS As String = String.Format("<script>alert('{0}');</script>", msg)
        ScriptManager.RegisterClientScriptBlock(litalert, litalert.GetType(), "script", txtJS, False)
    End Sub
    Protected Sub txtObservacion_TextChanged(sender As Object, e As EventArgs) Handles txtObservacion.TextChanged
        Me.txtObservacion.Text = Me.txtObservacion.Text.ToUpper
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
        Session("Modulo") = "Licencias"
        Session("NumRecReport") = Session("NumRec")
        Session("NumLiqReport") = Session("NumLiq")
        ReportWindow()
    End Sub
    Private Sub ReportWindow()
        Dim txtJS As String = "<script>window.open(""http://" & Request.ServerVariables("HTTP_HOST") & "/Reports/Reporte.aspx"",""Reporte de Liquidación"", 'toolbars=0,width=600,height=600,left=200,top=200,scrollbars=1,resizable=1,toolbar=0,status=0,menubar=0');</script>"
        ScriptManager.RegisterClientScriptBlock(litalert, litalert.GetType(), "script", txtJS, False)
    End Sub

End Class