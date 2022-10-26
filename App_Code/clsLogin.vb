Public Class clsLogin
    Public ErrorLgn As String = ""
    Public PCName As String = ""
    Public WinUsr As String = ""
    Public PendingRegister As Boolean = False
    Public UserName As String = ""
    Public idOficina As String = ""
    Public Oficina As String = ""
    Public NumCaja As String = ""
    Public Function Validate_PCAccess(UUid As String) As Boolean
        Dim ban As Boolean = False
        Try
            Dim usrWin As String = ""
            Dim Allowed As Boolean = False
            Dim PCNameVerif As String = ""
            Dim spSql As String = "select UserId,Allowed,PCName from tbl_PCAccess where MacAddress='" & UUid & "' and WebAccess=1"
            Dim cxn As New cxnSQL
            Try
                If cxn.Select_SQL(spSql) Then
                    If cxn.arrayValores(0) IsNot Nothing Then
                        usrWin = cxn.arrayValores(0)
                        Allowed = cxn.arrayValores(1)
                        PendingRegister = IIf(Allowed = True, False, True)
                        PCNameVerif = cxn.arrayValores(2)
                        If PCNameVerif.ToString.Trim = "" Or PCNameVerif.ToString Is Nothing Then
                            Return ban
                            Exit Function
                        End If
                    Else
                        Return ban
                        Exit Function
                    End If
                Else
                    ErrorLgn = cxn.arrayValores(0)
                End If
            Catch ex As Exception
                ErrorLgn = ex.Message
                ban = False
            End Try

            If Allowed Then
                'If usrWin = WinUsr And PCName = PCNameVerif Then
                ban = True
                'End If
            Else
                ban = False
            End If

        Catch ex As Exception
            ErrorLgn = ex.Message
            ban = False
        End Try
        Return ban
    End Function
    Public Function Validate_UserAccess(UserId As String, Password As String) As Boolean
        Dim ban As Boolean = False
        Dim cxn As New cxnSQL
        ' Se valida que el usuario tenga acceso al sistema y ademas que este activo
        Dim SQL_Login As String = "SELECT proUserId,concat( proNombres,' ', proApellidoPaterno) as Nombre,proNumeroCaja,proIdOficina FROM Users_Profiles WHERE proUserId='" & UserId.Trim & "' 
                                    AND Password=HASHBYTES('SHA2_512', '" & Password.Trim & "'+CAST(UsrKey AS NVARCHAR(36)))
                                    AND proActivo =1 and WebAccess=1"
        Try
            If cxn.Select_SQL(SQL_Login) AndAlso cxn.arrayValores(0) IsNot Nothing Then
                If cxn.arrayValores(0).ToUpperInvariant = UserId.ToUpperInvariant Then
                    idOficina = cxn.arrayValores(3)
                    NumCaja = cxn.arrayValores(2)
                    If Obtiene_Oficina(UserId) Then
                        UserName = cxn.arrayValores(1)
                        ban = True
                    End If
                Else
                    ErrorLgn = "Usuario y/o Password incorrecto..."
                End If
            Else
                ErrorLgn = "Usuario y/o Password incorrecto..."
            End If
        Catch ex As Exception
            ErrorLgn = "clsUser-LoginUser" & vbCrLf & ex.Message
        End Try
        Return ban
    End Function
    Public Function Obtiene_Oficina(UserId As String) As Boolean
        Dim ban As Boolean = False
        Dim cxn As New cxnSQL
        'Validamos que el usuario tenga asignada una oficina o puesto
        Dim SQL_Oficina As String = "select o.nombre as Oficina,o.comun
                                    from Users_Profiles up
	                                    inner join Puestos p on p.pueId = up.proIdPuesto 
	                                    inner join oficinas o on o.comun = up.proIdOficina 
                                    where up.proUserId= '" & UserId.Trim & "'"
        Try
            If cxn.Select_SQL(SQL_Oficina) And cxn.arrayValores(0) IsNot Nothing Then
                Oficina = cxn.arrayValores(0)
                idOficina = cxn.arrayValores(1)
                ban = True
            Else
                ErrorLgn = "El usuario no tiene asignado un puesto, contacte al Administrador..."
            End If
        Catch ex As Exception
            ErrorLgn = "clsUser-Obtiene_Oficina" & vbCrLf & ex.Message
        End Try
        Return ban
    End Function
End Class
