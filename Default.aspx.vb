Public Class _Default
    Inherits Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Session("Autenticated") Is Nothing Then
            Me.Response.Redirect("~/Login.aspx")
        Else
            Dim cxn As New cxnSQL

            cxn.Select_SQL("Select Mensaje from tb_WEBMessages where Tipo='Main' and idMenu=0")
            Session("MainMsg") = cxn.arrayValores(0)
        End If
    End Sub
End Class