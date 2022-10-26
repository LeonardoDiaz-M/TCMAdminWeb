Public Class Login1
    Inherits System.Web.UI.Page
    Private cxnSQL As New cxnSQL
    Private UUID As String = ""
    Private WinUsr As String = ""
    Private PCName As String = ""
    Private Sub usrLogin_Authenticate(sender As Object, e As AuthenticateEventArgs) Handles usrLogin.Authenticate
        Dim lgn As New clsLogin
        If GetCookieRegistered() Then
            If lgn.Validate_PCAccess(UUID) Then
                If lgn.Validate_UserAccess(Me.usrLogin.UserName.ToString, Me.usrLogin.Password.ToString) Then
                    Session("Autenticated") = True
                    Session("UserID") = Me.usrLogin.UserName
                    Session("UserName") = lgn.UserName
                    Session("UUID") = UUID
                    Session("PendingRegister") = False
                    Session("Oficina") = lgn.Oficina
                    Session("idOficina") = lgn.idOficina
                    Session("NumCaja") = lgn.NumCaja
                    Me.Response.Redirect("~/default.aspx")
                Else
                    Me.usrLogin.FailureText = lgn.ErrorLgn
                    Me.usrLogin.FailureAction = LoginFailureAction.Refresh
                End If
            ElseIf lgn.PendingRegister = False Then
                Me.usrLogin.FailureText = "Maquina NO perimitida"
                Me.usrLogin.FailureAction = LoginFailureAction.Refresh
                Me.winPCName.Visible = True
            ElseIf lgn.PendingRegister = True Then
                Me.usrLogin.FailureText = "Maquina Registrada, pendiente de aprobación"
                Me.usrLogin.FailureAction = LoginFailureAction.Refresh
            End If
        Else
            Me.usrLogin.FailureText = "Maquina NO registrada"
            Me.usrLogin.FailureAction = LoginFailureAction.Refresh
        End If
    End Sub
    Private Function GetCookieRegistered() As Boolean
        Dim ban As Boolean = False
        If Request.Cookies("UUID") Is Nothing Then
            Dim sGUID As String
            sGUID = System.Guid.NewGuid.ToString()
            Dim aCookie As New HttpCookie("UUID")
            aCookie.Value = sGUID
            aCookie.Expires = DateTime.Now.AddDays(30)
            Response.Cookies.Add(aCookie)
            Dim ckUser As New HttpCookie("WinUsr")
            ckUser.Value = Request.UserHostName
            ckUser.Expires = DateTime.Now.AddDays(30)
            Response.Cookies.Add(ckUser)
            UUID = sGUID
        Else
            Dim cookie As HttpCookie = Request.Cookies("UUID")
            UUID = Request.Cookies("UUID").Value
            cookie.Expires = DateTime.Now.AddDays(30)
            If Request.Cookies("WinUsr") IsNot Nothing Then
                WinUsr = Request.Cookies("WinUsr").Value
                ban = True
                If Request.Cookies("PCName") IsNot Nothing Then
                    PCName = Request.Cookies("PCName").Value
                End If
            End If
        End If
        Return ban
    End Function

    Protected Sub btnRegistrar_Click(sender As Object, e As EventArgs) Handles btnRegistrar.Click
        Dim ban As Boolean = False
        Dim usrWin As String = ""
        Dim Allowed As Boolean = False
        Dim PCName As Boolean = False
        UUID = Request.Cookies("UUID").Value
        If Me.txtPcName.Text.Trim <> "" And Me.txtPcName.Text.Trim.Length >= 7 Then
            If cxnSQL.Select_SQL("select UserId,Allowed,PCName from tbl_PCAccess where PCName='" & Me.txtPcName.Text.Trim & "Web'") Then
                Dim spSql As String = ""
                If cxnSQL.arrayValores(0) IsNot Nothing Then
                    spSql = "UPDATE [dbo].[tbl_PCAccess] set 
                                            [MacAddress]='" & UUID & "', 
                                            [Allowed]=0,
                                            [IPAddress]='" & Request.UserHostAddress & "',
                                            [WindowsUser]='" & Request.UserHostName & "',
                                            [UltimoIngreso]=getdate() " &
                            "where PCName='" & Me.txtPcName.Text.Trim & "Web' and WebAccess=1"
                Else
                    spSql = "INSERT INTO [dbo].[tbl_PCAccess] ([MacAddress],[Allowed],[IPAddress],[WindowsUser],[PCName],[FechaRegistro],[UltimoIngreso],WebAccess) " &
                           "VALUES('" & UUID & "',0,'" & Request.UserHostAddress & "','" & Request.UserHostName & "','" & Me.txtPcName.Text.Trim.ToUpper & "Web',getdate(),getdate(),1)"
                End If
                If cxnSQL.Execute_SQL(spSql) Then
                    Dim ckPC As New HttpCookie("PCName")
                    ckPC.Value = Me.txtPcName.Text.Trim.ToUpper
                    ckPC.Expires = DateTime.Now.AddDays(30)
                    Response.Cookies.Add(ckPC)
                    Allowed = False
                    Me.winPCName.Visible = False
                    Me.usrLogin.FailureText = "Equipo Registrado, solicite al Administrador el acceso al portal..."
                    Me.usrLogin.FailureAction = LoginFailureAction.Refresh
                Else
                    Me.lblErrorModal.Text = "Error al escribir en la base de datos: " & Me.cxnSQL.arrayValores(0)
                    Me.lblErrorModal.Visible = True
                End If
            End If
        Else
            Me.lblErrorModal.Text = "Error debe proporcionar el Nombre del Equipo  y debe ser mayor a 8 carácteres "
            Me.lblErrorModal.Visible = True
        End If

    End Sub

End Class