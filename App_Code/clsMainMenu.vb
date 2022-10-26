Imports Infragistics.Web.UI.NavigationControls

Public Class clsMainMenu
    Public UserId As String = ""
    Public ErrorMsg As String = ""
    Public Function GetMenus(UserId As String, ExplorerBar As WebExplorerBar) As Boolean
        Dim ban As Boolean = False
        Dim cxn As New cxnSQL
        Dim sqlMenuRol As String = "select idWebRol from Users_Profiles where proUserId='" & UserId & "' and webAccess=1"
        Dim sqlParentMenus As String = "  select  wm.idMenu,MenuName,MenuDescription,URLImage,URLNavigate,'' as Parameter
								          from tb_WEBMenus wm
								          where idMenuParent=0 and status=1
								                and idMenu in (
													select idMenuParent 
													from tb_WEBProfiles wp 
														inner join tb_WEBMenus wm on wm.idMenu = wp.idMenu
													where wp.idWebRol=<$ROL$>
												)"
        Dim SqlChildMenus As String = "select distinct wm.idMenu,wm.MenuName,wm.MenuDescription,wm2.idMenu,wm.URLNavigate, wm.Parameter
                                        from tb_WEBProfiles wp 
	                                        inner join tb_WEBMenus wm on wm.idMenu = wp.idMenu
	                                        left join tb_WEBMenus wm2 on wm2.idMenu = wm.idMenuParent 
                                        where wp.idWebRol=<$ROL$> and wm.idMenuParent >0 and wm.status=1"

        Try
            ExplorerBar.Groups.Clear()

            If cxn.Select_SQL(sqlMenuRol) AndAlso cxn.arrayValores(0) IsNot Nothing Then
                Dim readerMenu As System.Data.IDataReader
                Dim idrol As String = cxn.arrayValores(0)
                readerMenu = cxn.GetReader(sqlParentMenus.Replace("<$ROL$>", idrol))
                While (readerMenu.Read())
                    Dim grpItem As New ExplorerBarGroup
                    grpItem.CssClass = "Padre"
                    grpItem.Value = readerMenu(0)
                    grpItem.Text = readerMenu(2)
                    ExplorerBar.Groups.Add(grpItem)
                End While
                readerMenu.Close()
                readerMenu.Dispose()
                readerMenu = Nothing
                readerMenu = cxn.GetReader(SqlChildMenus.Replace("<$ROL$>", idrol))
                While (readerMenu.Read())
                    For Each grp As ExplorerBarGroup In ExplorerBar.Groups
                        If grp.Value = readerMenu(3) Then
                            Dim chldItem As New ExplorerBarItem
                            chldItem.CssClass = "MenuHijo"
                            chldItem.Value = readerMenu(0)
                            chldItem.Text = readerMenu(2)
                            chldItem.ToolTip = readerMenu(1) & "-" & readerMenu(2)
                            If Not IsDBNull(readerMenu(4)) Then
                                chldItem.NavigateUrl = readerMenu(4)
                                If Not IsDBNull(readerMenu(5)) Then
                                    chldItem.NavigateUrl += "?" & readerMenu(5)
                                End If
                            End If
                            grp.Items.Add(chldItem)
                        End If
                    Next
                End While
                Dim grpPSW As New ExplorerBarGroup
                grpPSW.Value = "opcPSW"
                grpPSW.Text = "<i class=""glyphicon glyphicon-eye-close""></i> Cambiar Password"
                grpPSW.NavigateUrl = "~/Password.aspx"
                ExplorerBar.Groups.Add(grpPSW)
                Dim grpOpciones As New ExplorerBarGroup
                grpOpciones.Value = "opcSalir"
                grpOpciones.Text = "<i class=""glyphicon glyphicon-log-out""></i> Cerrar Sesion"
                grpOpciones.NavigateUrl = "~/Logout.aspx"
                ExplorerBar.Groups.Add(grpOpciones)
            Else
                ErrorMsg = "Error: Usuario sin acceso al sistema"
            End If
        Catch ex As Exception
            ErrorMsg = "Error: " & ex.Message.ToString
        End Try
        Return ban
    End Function
End Class
