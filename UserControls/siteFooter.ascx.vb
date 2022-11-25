Public Class siteFooter
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Me.IsPostBack Then
            LoadTexto()
            LoadImagenes()
        End If
    End Sub

    Private Sub LoadTexto()
        Dim cxn As New cxnSQL
        Dim strValues(14) As String
        Dim i As Int16 = 0
        Dim sql As String = "select Parametro,Descripcion,Link from tb_WEBParametros where Parametro like 'texto%'"
        Dim readerMenu As System.Data.IDataReader
        readerMenu = cxn.GetReader(sql)
        While (readerMenu.Read()) And i < 14
            If Len(readerMenu(2)) > 0 Then
                If Len(readerMenu(1)) > 0 Then
                    strValues(i) = "<div class=""TextoFooter""><a href=""" & readerMenu(2) & """  target='_blank'>" & readerMenu(1) & "</a></div>"
                Else
                    strValues(i) = ""
                End If
            ElseIf Len(readerMenu(1)) > 0 Then
                strValues(i) = "<div class=""TextoFooter"">" & readerMenu(1) & "</div>"
            Else
                strValues(i) = ""
            End If
            i = i + 1
        End While
        readerMenu.Close()
        readerMenu.Dispose()
        readerMenu = Nothing
        Session("Texto") = strValues
    End Sub


    Private Sub LoadImagenes()
        Dim cxn As New cxnSQL
        Dim sql As String = "select Valor from tb_WEBParametros where Parametro  ='<$par$>'"

        cxn.Select_SQL(sql.Replace("<$par$>", "FTRImg1"))
        Session("Img1") = cxn.arrayValores(0) ' ConvertPath(cxn.arrayValores(0))
        cxn.Select_SQL(sql.Replace("<$par$>", "FTRImg2"))
        Session("Img2") = cxn.arrayValores(0) ' ConvertPath(cxn.arrayValores(0))
        cxn.Select_SQL(sql.Replace("<$par$>", "FTRFace"))
        Session("face") = cxn.arrayValores(0) 'ConvertPath(cxn.arrayValores(0))
        cxn.Select_SQL(sql.Replace("<$par$>", "FTRTWT"))
        Session("twit") = cxn.arrayValores(0) 'ConvertPath(cxn.arrayValores(0))
    End Sub
    Private Function ConvertPath(path) As String
        Dim v2 As String = System.Web.Hosting.HostingEnvironment.MapPath("~/")
        Dim url As String = path.Substring(v2.Length).Replace("\", "/")
        url = "http://" & Request("SERVER_NAME") & ":" & Request("SERVER_PORT") & "/" & url
        Return url
    End Function
End Class