﻿Imports Infragistics.Web.UI.LayoutControls

Public Class LiquidaAgua
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
            Session("Modulo") = "Agua"
            Session("SQLStore") = "App_InsertaTransaccion"
        Else
            If Session("ModalVisble") IsNot Nothing Then
                If Session("ModalVisble") >= 2 Then
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
        Session("suma") = 0
        Session("NumLiq") = 0
        Session("NumRec") = 0
        Me.pnlBtns.Visible = False
        If Me.txtCveCta.Value.Trim.Length = "8" Then
            datosCont.Select_SQL("select Nombre,Domicilio,ult_año_pago,ult_mes_pago,tipo_servicio from arc_agua where num_cuenta='" & Me.txtCveCta.Text.Trim & "'")
            If datosCont.arrayValores(0) IsNot Nothing Then
                Me.DatCont.Visible = True
                Me.DatLiq.Visible = True
                Me.TxtPropietario.Text = datosCont.arrayValores(0).ToString
                Me.TxtUbicacion.Text = datosCont.arrayValores(1).ToString
                Me.TxtAño.Text = datosCont.arrayValores(2).ToString
                Me.TxtMes.Text = datosCont.arrayValores(3).ToString
                If CType(Me.TxtAño.Text, Integer) = Now.Year And CType(Me.TxtMes.Text, Integer) = 12 And CType(datosCont.arrayValores(4), Integer) = 2 Then
                    alerts("El usuario no tiene adeudos fiscales registrados", False, Me.litalert)
                    Me.DatCont.Visible = False
                    Me.DatLiq.Visible = False
                Else
                    Try
                        Dim cxn1 As New cxnSQL
                        cxn1.Select_SQL(Me.grdresults, "Exec Calcula_Agua '" & Me.txtCveCta.Text.Trim & "','" & Me.rbFmaLiq.Text.Trim & "'")
                    Catch ex As Exception
                        alerts(ex.Message, False, Me.litalert)
                    End Try
                    For Each row As GridViewRow In Me.grdresults.Rows
                        Session("suma") = Session("suma") + row.Cells(16).Text
                        Session("NumLiq") = row.Cells(17).Text
                        Session("NumRec") = row.Cells(18).Text
                        row.Cells(17).Visible = False
                        row.Cells(18).Visible = False
                        Dim chk As CheckBox = CType(row.FindControl("chkSelect"), CheckBox)
                        chk.Checked = True
                    Next
                    If ObNumReg() > 5 Then
                        Me.Panel1.Height = 195
                    Else
                        Me.Panel1.Height = 95%
                    End If
                    If CType(Session("NumLiq").ToString, Integer) > 0 Then
                        Me.pnlBtns.Visible = True
                        ChecaEstado()
                        ObtenNumRec()
                    Else
                        Me.pnlBtns.Visible = False
                        alerts("No se pudo generar la liquidación, verifique la información en el padrón", False, Me.litalert)
                    End If
                End If
            Else
                alerts("Número de cuenta inexistente, verifique", False, Me.litalert)
            End If
        Else
            alerts("El número de cuenta digitado está incompleto, verifique", False, Me.litalert)
        End If
        datosCont = Nothing
        Me.txtCveCta.Focus()
    End Sub

    Protected Sub btnContinuar_Click(sender As Object, e As EventArgs) Handles btnContinuar.Click
        Session("ModalVisble") = 1
        Session("NumRecReport") = Session("NumRec")
        Session("NumLiqReport") = Session("NumLiq")
        Me.usrConfirmaPago.modal = True
        Me.usrConfirmaPago.Visible = True
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
        Dim txtJS As String = "<script>window.open(""http://" & Request.ServerVariables("HTTP_HOST") & "/Reports/Reporte.aspx"",""Reporte de Liquidación"", 'width=600,height=600,left=200,top=200,scrollbars=1,resizable=1');</script>"
        ScriptManager.RegisterClientScriptBlock(litalert, litalert.GetType(), "script", txtJS, False)
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
                Session("NumRec") = CType(row.Cells(18).Text, Integer)
                Exit For
            Else
                i = i + 1
            End If
        Next
        For Each row As GridViewRow In Me.grdresults.Rows
            Dim chk As CheckBox = CType(row.FindControl("chkSelect"), CheckBox)
            If CType(row.Cells(18).Text, Integer) <= Session("NumRec") Then
                chk.Checked = True
                Session("Suma") = Session("Suma") + CType(row.Cells(16).Text, Integer)
            Else
                chk.Checked = False
            End If
        Next
        ChecaEstado()
        Me.lblTotal.Text = "Total a Pagar: " & FormatCurrency(Session("Suma").ToString, , , TriState.True, TriState.True)
        Session("ModalVisble") = 1
        Me.grdresults.HeaderRow.Cells(1).Text = "AÑO"
        Me.grdresults.HeaderRow.Cells(2).Text = "INICIO"
        Me.grdresults.HeaderRow.Cells(3).Text = "FINAL"
        Me.grdresults.HeaderRow.Cells(4).Text = "BASE GRAVABLE"
        Me.grdresults.HeaderRow.Cells(5).Text = "IMPORTE"
        Me.grdresults.HeaderRow.Cells(6).Text = "ACTUALIZACIÓN"
        Me.grdresults.HeaderRow.Cells(7).Text = "SANEAMIENTO"
        Me.grdresults.HeaderRow.Cells(8).Text = "IVA"
        Me.grdresults.HeaderRow.Cells(9).Text = "RECARGOS"
        Me.grdresults.HeaderRow.Cells(10).Text = "MULTA"
        Me.grdresults.HeaderRow.Cells(11).Text = "GASTOS"
        Me.grdresults.HeaderRow.Cells(12).Text = "SUB IMP"
        Me.grdresults.HeaderRow.Cells(13).Text = "SUB REC"
        Me.grdresults.HeaderRow.Cells(14).Text = "SUB MUL"
        Me.grdresults.HeaderRow.Cells(15).Text = "SUB GST"
        Me.grdresults.HeaderRow.Cells(16).Text = "TOTAL"
        Me.grdresults.HeaderRow.Cells(17).Visible = False
        Me.grdresults.HeaderRow.Cells(18).Visible = False
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
        Session("NumRec") = 0
        Session("ModalVisble") = 0
        ObtenNumRec()
    End Sub
    Private Sub alerts(ByVal msg As String, ByVal redirect As Boolean, ByVal litalert As Literal)
        Dim txtJS As String = String.Format("<script>alert('{0}');</script>", msg)
        ScriptManager.RegisterClientScriptBlock(litalert, litalert.GetType(), "script", txtJS, False)
    End Sub
End Class