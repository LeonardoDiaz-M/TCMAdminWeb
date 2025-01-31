﻿Public Class LiquidaConjuntosUrbanos
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("Autenticated") Is Nothing Then
            Me.Response.Redirect("~/Login.aspx")
        End If
        If Not Me.IsPostBack Then
            Me.DatLiq.Visible = False
            Me.usrConfirmaPago.Visible = True
            Me.usrConfirmaPago.modal = False
            Me.TxtNombre.Focus()
            Session("ImprimePago") = 0
            Session("ModalVisble") = 0
            Session("Modulo") = "Derecho"
            Session("SQLStore") = "App_InsertaDerechos"
            Dim cxn As New cxnSQL
            cxn.Select_SQL(Me.ddlDerechos, "descripcion", "descripcion", "Derecho_Id")

        Else
            If Session("ModalVisble") IsNot Nothing Then
                If Session("ModalVisble") = 2 Then
                    Me.DatLiq.Visible = False
                    Session("suma") = 0
                    Session("NumLiq") = 0
                    Session("NumRec") = 0
                    Me.pnlBtns.Visible = False
                    Me.usrConfirmaPago.Visible = False
                    Session.Remove("ModalVisble")
                End If
            Else
                Me.pnlBtns.Visible = False
                Me.usrConfirmaPago.Visible = False
            End If
        End If
    End Sub
    Protected Sub Button2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button2.Click
        Dim datosCont As New cxnSQL
        Dim FoxProLiquidacion As New Data.DataSet()
        Session("suma") = 0
        Session("NumLiq") = 0
        Session("NumRec") = 0
        Me.pnlBtns.Visible = False
        Me.DatLiq.Visible = True
        Me.TxtNombre.Text = Me.TxtNombre.Text.ToUpper
        Me.TxtDireccion.Text = Me.TxtDireccion.Text.ToUpper
        Me.TxtRFC.Text = Me.TxtRFC.Text.ToUpper
        Dim requerido As String = IIf(Me.chkNotificado.Checked = False, 0, 1).ToString
        Try
            Dim cxn1 As New cxnSQL
            cxn1.Select_SQL(Me.grdresults, "exec CalcImpConjuntosUrbanos '" & Me.ddlDerechos.SelectedValue & "','" & Val(Me.TxtDatoAdicional.Text) & "','" & WDFechaInicio.Text.Trim & "','" & requerido & "','" & Me.TxtNombre.Text & "','" & Me.TxtDireccion.Text & "','" & Me.TxtRFC.Text & "'")
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
            Me.lblTotal.Text = "Total a Pagar: " & FormatCurrency(Session("Suma").ToString, , , TriState.True, TriState.True)
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
            Me.grdresults.HeaderRow.Cells(10).Text = "SUB IMP"
            Me.grdresults.HeaderRow.Cells(11).Text = "SUB REC"
            Me.grdresults.HeaderRow.Cells(12).Text = "SUB MUL"
            Me.grdresults.HeaderRow.Cells(13).Text = "SUB GST"
            Me.grdresults.HeaderRow.Cells(14).Text = "TOTAL"
            Me.grdresults.HeaderRow.Cells(15).Visible = False
            Me.grdresults.HeaderRow.Cells(16).Visible = False
        Else
            alerts("No se pudo generar la liquidación, verifique la información", False, Me.litalert)
            Me.pnlBtns.Visible = False
        End If
    End Sub


    Protected Sub ddlDerechos_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlDerechos.SelectedIndexChanged
        Dim datosCont As New cxnSQL
        Session("suma") = 0
        datosCont.Select_SQL("select Sub_cuenta from tbl_derechos where alltrim(str(recno()))='" & Me.ddlDerechos.SelectedValue.ToString & "'")
        Select Case datosCont.arrayValores(0)
            Case "001" To "006"
                Me.lblDatoAdicional.Text = "Núm. de viviendas:"
            Case "007" To "010"
                Me.lblDatoAdicional.Text = "Superficie a vender:"
            Case "011"
                Me.lblDatoAdicional.Text = "Sup. recreativa:"
        End Select
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