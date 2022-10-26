Public Class SiteMaster
    Inherits MasterPage
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Session("Autenticated") Is Nothing Then
            Me.siteMainMenu.Visible = False
            Session("Ayuda") = ""
        ElseIf Session("Autenticated") = True Then
            Me.siteMainMenu.Visible = True
        Else
            Me.siteMainMenu.Visible = False
            Session("Ayuda") = ""
        End If
    End Sub
End Class