Imports System.IO
Imports Infragistics.Web.UI.ListControls

Public Class MpioSite
    Inherits System.Web.UI.MasterPage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim path As String = "\Mpio\MpioImgs"
        Dim imgs() As String = System.IO.Directory.GetFiles(Server.MapPath(path), "*.jpg")
        ' Add a new ImageItem to the WebImageViewer per image
        Dim imgsDisplay As New ImageItemCollection
        For Each img As String In imgs
            Dim theFile As String = "~/Mpio/MpioImgs/" + System.IO.Path.GetFileName(img)
            Dim vwr As New Infragistics.Web.UI.ListControls.ImageItem
            vwr.ImageUrl = theFile & "?tr=w-50,h-50"
            vwr.AltText = "alt"
            vwr.CssClass = "imag1"
            Me.WebImageViewer1.Items.Add(vwr)
        Next
    End Sub
End Class