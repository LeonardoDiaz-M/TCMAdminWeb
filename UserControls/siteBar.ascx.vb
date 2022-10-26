Public Class siteBar
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Session("Autenticated") IsNot Nothing Then
                If Session("Autenticated") = True Then
                    ActivaControles(Session("Autenticated"))
                    If Session("Autenticated") = True Then
                        Me.lblOficina.Text = "<i class='glyphicon glyphicon-home'></i>&nbsp;&nbsp;" & Session("Oficina")
                        Me.lblUser.Text = "<i class='glyphicon glyphicon-user'></i>&nbsp;&nbsp;" & Session("UserName")
                    End If
                Else
                    ActivaControles(False)
                End If
            Else
                ActivaControles(False)
            End If
        Catch ex As Exception
            ActivaControles(False)
        End Try
    End Sub
    Private Sub ActivaControles(visibles As Boolean)
        Me.lblOficina.Visible = visibles
        Me.lblUser.Visible = visibles
        Me.btnAyuda.Visible = visibles
    End Sub

    Protected Sub btnAyuda_Click(sender As Object, e As EventArgs) Handles btnAyuda.Click
        '{ASP.app_forms_liquidapredial_aspx}
        Dim pgnWeb As String = Page.Page.ToString.Replace("ASP.", "")
        Dim Delimiter As Int16

        pgnWeb = StrReverse(pgnWeb.ToString.Replace("_aspx", ""))
        Delimiter = StrReverse(pgnWeb.ToString.Replace("_aspx", "")).IndexOf("_")

        Dim cxn As New cxnSQL
        If Delimiter > 1 Then
            pgnWeb = StrReverse(Mid(pgnWeb, 1, Delimiter - 1))
        Else
            pgnWeb = StrReverse(pgnWeb)
        End If

        cxn.Select_SQL("select idMenu from [dbo].[tb_WEBMenus] where URLNavigate like '%" & pgnWeb & ".aspx'")
        If cxn.arrayValores(0) IsNot Nothing Then
            cxn.Select_SQL("Select Mensaje from tb_WEBMessages where Tipo='Ayuda' and idMenu=" & cxn.arrayValores(0))
            Session("Ayuda") = cxn.arrayValores(0)
        Else
            Session("Ayuda") = "<h1>No se encontro Información</h1>"
        End If
        Me.dlgAyuda.Visible = False
        Me.dlgAyuda.Visible = True
        Me.dlgAyuda.WindowState = Infragistics.Web.UI.LayoutControls.DialogWindowState.Normal
    End Sub
End Class