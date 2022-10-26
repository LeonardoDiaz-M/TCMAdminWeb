Public Class Password
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("Autenticated") Is Nothing Then
            Me.Response.Redirect("~/Login.aspx")
        Else
            Me.winPCAccess.Visible = True
        End If
    End Sub
    Public Function Actualiza_Password(UserName As String, Password As String)
        Dim cxnSQL As New cxnSQL
        Dim ban As Boolean = False
        Try
            Dim SQL_Login As String = "Update Users_Profiles 
                                        set Password=HASHBYTES('SHA2_512', '" & Password.Trim & "'+CAST(UsrKey AS NVARCHAR(36))) ,
                                            UltimoAcceso=getdate()
                                        WHERE proUserId='" & UserName.Trim & "' "
            If cxnSQL.Select_SQL(SQL_Login) Then
                ban = True
            End If
        Catch ex As Exception
            MsgBox("clsUser-Actualiza_Password" & vbCrLf & ex.Message, vbCritical + vbOKOnly, "Error")
        End Try
        Return ban
    End Function
    Public Function LoginUser(UserName As String, Password As String) As Boolean
        Dim cxn As New cxnSQL
        Dim ban As Boolean = False
        ' Se valida que el usuario tenga acceso al sistema y ademas que este activo
        Dim SQL_Login As String = "SELECT proUserId,isnull(proValidaUbicacion,0) FROM Users_Profiles WHERE proUserId='" & UserName.Trim & "' 
                                    AND Password=HASHBYTES('SHA2_512', '" & Password.Trim & "'+CAST(UsrKey AS NVARCHAR(36)))
                                    AND proActivo =1"
        Try
            If cxn.Select_SQL(SQL_Login) AndAlso cxn.arrayValores(0) IsNot Nothing Then

                If cxn.arrayValores(0).ToUpperInvariant = UserName.ToUpperInvariant Then
                    ban = True
                End If
            End If
        Catch ex As Exception
            MsgBox("clsUser-LoginUser" & vbCrLf & ex.Message, vbCritical + vbOKOnly, "Error")
        End Try
        Return ban
    End Function
    Protected Sub btnActualizar_Click(sender As Object, e As EventArgs) Handles btnActualizar.Click
        Dim cxnSQL As New cxnSQL
        Dim userId As String = ""
        If Me.pswActual.Text.Trim = "" Or Me.pswConfirm.Text.Trim = "" Or Me.pswNuevo.Text.Trim = "" Then
            Me.lblErrorModal.Text = "Todos los campos son requeridos"
            Me.lblErrorModal.Visible = True
            Me.pswActual.Focus()
            Exit Sub
        End If

        userId = Session("UserID")
        If LoginUser(userId, Me.pswActual.Text.ToString) Then
            If StrComp(Me.pswNuevo.Text, Me.pswConfirm.Text, vbBinaryCompare) = 0 Then
                If Actualiza_Password(userId, Me.pswConfirm.Text) Then
                    Me.pswActual.Text = ""
                    Me.pswConfirm.Text = ""
                    Me.pswNuevo.Text = ""
                    Me.lblErrorModal.Text = "Contraseña Actualizada!!" & vbCrLf & ""
                    Me.lblErrorModal.Visible = True

                End If
            Else
                Me.lblErrorModal.Text = "La contraseña Nueva y la contraseña de Confirmación " & vbCrLf & "Deben ser iguales"
                Me.lblErrorModal.Visible = True
                Me.pswConfirm.Focus()
            End If
        Else
            Me.lblErrorModal.Text = "La Contraseña es incorrecta"
            Me.lblErrorModal.Visible = True
            Me.pswActual.Focus()
        End If

    End Sub
End Class