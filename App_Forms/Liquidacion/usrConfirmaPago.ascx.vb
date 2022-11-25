Imports Infragistics.Web.UI.Framework
Imports Infragistics.Web.UI.LayoutControls

Public Class usrConfirmaPago
    Inherits System.Web.UI.UserControl
    Public Property modal() As Boolean
        Get
            Return Me.windowModal.Visible
        End Get
        Set(ByVal Value As Boolean)
            Me.windowModal.Visible = Value
            If Value Then
                Me.windowModal.WindowState = DialogWindowState.Normal
                Me.txtTotalModal.Text =  FormatCurrency(Session("Suma").ToString, , , TriState.True, TriState.True)
                Me.btnPagar.Text = "Realizar Pago"
            Else
                Me.windowModal.WindowState = DialogWindowState.Hidden
            End If
            Me.ddlFmaPago.Visible = Value
            Me.lblFmaPago.Visible = Value
            Me.lblTotalHdr.Visible = Value
            Me.txtTotalModal.Visible = Value
            Me.lblErrorModal.Visible = Not Value
        End Set
    End Property
    Public Property Total() As String
        Get
            Return Me.txtTotalModal.Text
        End Get
        Set(ByVal Value As String)
            Me.txtTotalModal.Text = Value
        End Set
    End Property
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Me.IsPostBack Then
            Dim cxn As New cxnSQL
            cxn.Select_SQL(Me.ddlFmaPago, "SELECT cve_fma_pago, FormaPagoDesc FROM  tbl_SAT_FmaPago order by id asc", "FormaPagoDesc", "cve_fma_pago")
        End If

    End Sub

    Protected Sub btnPagar_Click(sender As Object, e As EventArgs) Handles btnPagar.Click
        Dim cxnPago As New cxnSQL
        If Session("ModalVisble") = 2 Then
            Me.windowModal.Visible = False
            Me.windowModal.WindowState = DialogWindowState.Hidden
            Session("suma") = 0
            Session("NumLiq") = 0
            Session("NumRec") = 0
            Session("ModalVisble") = 3
        ElseIf Session("ModalVisble") = 1 Then
            Session("ImprimePago") = 2  '1-Imprime, 2-Paga
            If cxnPago.Select_SQL("Exec  Rpt_liq_web '" & Session("NumLiq").ToString & "','" & Session("NumRec").ToString & "'") Then
                If cxnPago.Execute_SQL("Exec " & Session("SQLStore") & " " & Session("NumLiq") & "," &
                                                                    Session("NumCaja") & "," &
                                                                    Session("idOficina") & "," &
                                                                     Me.ddlFmaPago.SelectedValue.ToString()) Then
                    Session("idSATCuenta") = Me.ddlFmaPago.SelectedValue
                    Me.lblErrorModal.ForeColor = Drawing.Color.Green
                    Me.lblErrorModal.Visible = True
                    Me.lblErrorModal.Text = "Liquidación Realizada!"
                    Me.windowModal.Header.CloseBox.Visible = False
                    Me.btnPagar.Text = "Finalizar"
                    Me.lblFmaPago.Visible = False
                    Me.lblTotalHdr.Visible = False
                    Me.ddlFmaPago.Visible = False
                    Me.txtTotalModal.Visible = False
                    ReportWindow()
                    Session("ModalVisble") = 2
                Else
                    Me.lblErrorModal.ForeColor = Drawing.Color.Red
                    Me.lblErrorModal.Visible = True
                    Me.lblErrorModal.Text = "Error al procesar pago, " & cxnPago.arrayValores(0)
                    Me.btnPagar.Visible = False
                End If
            Else
                Me.lblErrorModal.ForeColor = Drawing.Color.Red
                Me.lblErrorModal.Visible = True
                Me.lblErrorModal.Text = "Error al procesar pago, " & cxnPago.arrayValores(0)
                Me.btnPagar.Visible = False
            End If

        End If
    End Sub
    Private Sub ReportWindow()
        Dim txtJS As String = "<script>window.open(""http://" & Request.ServerVariables("HTTP_HOST") & "/Reports/Reporte.aspx"",""Reporte de Liquidación"", 'width=600,height=600,left=200,top=200,scrollbars=1,resizable=1,toolbar=0,status=0,menubar=0');</script>"
        ScriptManager.RegisterClientScriptBlock(litalert, litalert.GetType(), "script", txtJS, False)
    End Sub
End Class