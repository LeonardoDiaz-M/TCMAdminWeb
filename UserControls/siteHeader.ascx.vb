Public Class siteHeader
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        LoadImagenes()
    End Sub
    Private Sub LoadImagenes()
        Dim cxn As New cxnSQL
        Dim sql As String = "select Valor from tb_WEBParametros where Parametro  ='<$par$>'"
        cxn.Select_SQL(sql.Replace("<$par$>", "HdrImg1"))
        Session("HdrImg1") = ConvertPath(cxn.arrayValores(0))
        cxn.Select_SQL(sql.Replace("<$par$>", "HdrImg2"))
        Session("HdrImg2") = ConvertPath(cxn.arrayValores(0))
    End Sub
    Private Function ConvertPath(path) As String
        Dim v2 As String = System.Web.Hosting.HostingEnvironment.MapPath("~/")
        Dim url As String = path.Substring(v2.Length).Replace("\", "/")
        url = "http://" & Request("SERVER_NAME") & ":" & Request("SERVER_PORT") & "/" & url
        Return url
    End Function
End Class