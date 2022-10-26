Imports Infragistics.Web.UI.LayoutControls

Public Class LiquidaPredial
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("Autenticated") Is Nothing Then
            Me.Response.Redirect("~/Login.aspx")
        End If
        Me.txtCveCat.Focus()
        Dim cxn As New cxnSQL
        cxn.Select_SQL(Me.ddlFmaPago, "SELECT cve_fma_pago, FormaPagoDesc FROM  tbl_SAT_FmaPago order by id asc", "FormaPagoDesc", "cve_fma_pago")
        Session("idSATCuenta") = 1
        Session("ImprimePago") = 1  '1-Imprime, 2-Paga
    End Sub

    Protected Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.DatCont.Visible = False
        Me.DatLiq.Visible = False
        Dim datosCont As New cxnSQL
        Session("suma") = 0
        Session("NumLiq") = 0
        Session("NumRec") = 0
        Me.txtCveCat.Text = Me.txtCveCat.Text.ToUpper
        If Me.txtCveCat.Text.Trim.Length = "16" Then
            datosCont.Select_SQL("select Propietario, Ubicacion,ult_año_pag,ult_mes_pag,forma_pago from arc_predial where cve_Catastral='" & Me.txtCveCat.Text.Trim.ToUpper & "'")
            If datosCont.arrayValores(0) IsNot Nothing Then
                Me.DatCont.Visible = True
                Me.DatLiq.Visible = True
                Me.TxtPropietario.Text = datosCont.arrayValores(0).ToString
                Me.TxtUbicacion.Text = datosCont.arrayValores(1).ToString
                Me.TxtAño.Text = datosCont.arrayValores(2).ToString
                Me.TxtMes.Text = datosCont.arrayValores(3).ToString
                Session("mp_Customername") = Me.TxtPropietario.Text
                Session("mp_Referencia") = Me.txtCveCat.Text
                If CType(Me.TxtAño.Text, Integer) = Now.Year And CType(Me.TxtMes.Text, Integer) = 12 And CType(datosCont.arrayValores(4), Integer) = 1 Then
                    alerts("El inmueble no tiene adeudos fiscales registrados", False, Me.litalert)
                Else
                    Try
                        Dim cxn1 As New cxnSQL
                        cxn1.Select_SQL(Me.grdresults, "Exec Calcula_predial '" & Me.txtCveCat.Text.Trim & "','" & Me.rbFmaLiq.Text.Trim & "','" & Session("UserId") & "'")
                    Catch ex As Exception
                    End Try
                    For Each row As GridViewRow In Me.grdresults.Rows
                        Session("suma") = Session("suma") + row.Cells(12).Text
                        Session("NumLiq") = row.Cells(13).Text
                        Session("NumRec") = row.Cells(14).Text
                        row.Cells(13).Visible = False
                        row.Cells(14).Visible = False
                        Dim chk As CheckBox = CType(row.FindControl("chkSelect"), CheckBox)
                        chk.Checked = True
                    Next
                    If ObNumReg() > 5 Then
                        Me.Panel1.Height = 195
                    Else
                        Me.Panel1.Height = 90%
                    End If
                    If CType(Session("NumLiq").ToString, Integer) > 0 Then
                        ChecaEstado()
                        ObtenNumRec()
                    Else
                        alerts("No se pudo generar la liquidación, verifique la información en el padrón", False, Me.litalert)
                    End If
                End If
            Else
                alerts("Clave catastral inexistente, verifique", False, Me.litalert)
            End If
        Else
            alerts("La clave catastral digitada está incompleta, verifique", False, Me.litalert)
        End If
        Me.txtCveCat.Focus()
    End Sub
    Protected Sub btnPagar_Click(sender As Object, e As EventArgs) Handles btnPagar.Click
        If Me.btnPagar.Text = "Finalizar" Then
            Session("ReportFileName") = "Reportes\rptPago.rdlc"
            Session("ReportTitle") = "RECIBO DE PAGO "
            Session("ImprimePago") = 2  '1-Imprime, 2-Paga
            Me.Response.Redirect("~/App_Forms/Reportes.aspx?Modulo=Predial")
        Else
            Me.lblErrorModal.ForeColor = Drawing.Color.Green
            Me.lblErrorModal.Visible = True
            Dim cxnPago As New cxnSQL
			Session("idSATCuenta") = Me.ddlFmaPago.SelectedValue
            If cxnPago.Execute_SQL("Exec [App_InsTranPredial] " & Session("NumLiq") & "," &
                                                                    Session("NumCaja") & "," &
                                                                    Session("idOficina") & "," &
                                                                     Me.ddlFmaPago.SelectedValue.ToString()) Then
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
    Protected Sub btnContinuar_Click(sender As Object, e As EventArgs) Handles btnContinuar.Click
        Me.windowModal.Visible = False
        Me.txtTotalModal.Text = Session("suma")
        Me.windowModal.Visible = True
        Me.windowModal.WindowState = DialogWindowState.Normal
    End Sub
    Protected Sub chkSelect_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        ObtenNumRec()
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
    Private Sub alerts(ByVal msg As String, ByVal redirect As Boolean, ByVal litalert As Literal)
        Dim txtJS As String = String.Format("<script>alert('{0}');</script>", msg)
                ScriptManager.RegisterClientScriptBlock(litalert, litalert.GetType(), "script", txtJS, False)
    End Sub

    Private Sub ObtenNumRec()
        Session("Suma") = 0
        Dim Estado As String() = Session("Estado")
        Dim i As Integer = 0
        Dim band As Boolean = True
        For Each row As GridViewRow In Me.grdresults.Rows
            Dim chk As CheckBox = CType(row.FindControl("chkSelect"), CheckBox)
            If chk.Checked.ToString <> Estado(i).ToString Then
                Session("NumRec") = CType(row.Cells(14).Text, Integer)
                Exit For
            Else
                i = i + 1
            End If
        Next
        For Each row As GridViewRow In Me.grdresults.Rows
            Dim chk As CheckBox = CType(row.FindControl("chkSelect"), CheckBox)
            If CType(row.Cells(14).Text, Integer) <= Session("NumRec") Then
                chk.Checked = True
                Session("Suma") = Session("Suma") + CType(row.Cells(12).Text, Integer)
            Else
                chk.Checked = False
            End If
        Next
        ChecaEstado()
        Me.lblTotal.Text = "Total: " & Session("Suma").ToString
        Me.grdresults.HeaderRow.Cells(1).Text = "PER INI"
        Me.grdresults.HeaderRow.Cells(2).Text = "PER FIN"
        Me.grdresults.HeaderRow.Cells(3).Text = "VAL CAT"
        Me.grdresults.HeaderRow.Cells(4).Text = "IMPORTE"
        Me.grdresults.HeaderRow.Cells(5).Text = "RECARGOS"
        Me.grdresults.HeaderRow.Cells(6).Text = "MULTA"
        Me.grdresults.HeaderRow.Cells(7).Text = "GASTOS"
        Me.grdresults.HeaderRow.Cells(8).Text = "SUB IMP"
        Me.grdresults.HeaderRow.Cells(9).Text = "SUB REC"
        Me.grdresults.HeaderRow.Cells(10).Text = "SUB MUL"
        Me.grdresults.HeaderRow.Cells(11).Text = "SUB GST"
        Me.grdresults.HeaderRow.Cells(12).Text = "TOTAL"
        Me.grdresults.HeaderRow.Cells(13).Visible = False
        Me.grdresults.HeaderRow.Cells(14).Visible = False
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
End Class