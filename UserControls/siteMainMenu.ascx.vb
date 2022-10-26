Imports Infragistics.Web.UI.NavigationControls

Public Class siteMainMenu
    Inherits System.Web.UI.UserControl
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Session("Autenticated") IsNot Nothing Then
                If Session("Autenticated") IsNot Nothing AndAlso Session("Autenticated") = True Then
                    Me.mnuMainExploreBar.Visible = Session("Autenticated")
                    Dim mnu As New clsMainMenu
                    mnu.GetMenus(Session("UserId"), Me.mnuMainExploreBar)
                Else
                    Me.mnuMainExploreBar.Visible = False
                End If
            Else
                Me.mnuMainExploreBar.Visible = False
            End If
        Catch ex As Exception
            Me.mnuMainExploreBar.Visible = False
        End Try
    End Sub

End Class