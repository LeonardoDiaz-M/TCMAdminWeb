Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Drawing
Imports System.Web
Imports System.Web.UI.Page
Imports System.Web.UI.WebControls
Imports System.Data.SqlClient

Public Class cxnSQL
    Private SqlPubsConnString As String = ConfigurationManager.ConnectionStrings("SQLDB").ConnectionString
    Private Cn As New SqlConnection(Me.SqlPubsConnString)
    Private SQLSelect As SqlDataReader
    Private sqlComm1 As New SqlCommand
    Public arrayValores() As String

    Private Function OpenCxn() As Boolean
        Dim ban As Boolean = False
        Try
            If Cn.State = System.Data.ConnectionState.Closed Then
                Cn.Open()
            End If
            ban = True
        Catch ex As System.Exception
            ReDim Me.arrayValores(1)
            Me.arrayValores(0) = "Ocurrio un error al intentar abrir la base de datos"
        End Try
        Return ban
    End Function

    Private Function CloseCxn() As Boolean
        Dim ban As Boolean = False
        Try
            If Cn.State = System.Data.ConnectionState.Open Then
                Cn.Close()
            End If
            ban = True
        Catch ex As System.Exception
            ReDim Me.arrayValores(1)
            Me.arrayValores(0) = "Ocurrio un error al intentar cerrar la base de datos"
        End Try
        Return ban
    End Function
    Public Function Select_SQL(ByVal SQLString As String, Optional bolupper As Boolean = False) As Boolean
        Dim adapter As New SqlDataAdapter()
        Dim i As Integer
        Dim ban As Boolean = False
        Try
            Me.OpenCxn()
            sqlComm1.Connection = Cn 'conecta bd
            sqlComm1.CommandText = SQLString
            adapter.SelectCommand = sqlComm1
            Dim reader As System.Data.SqlClient.SqlDataReader

            reader = adapter.SelectCommand.ExecuteReader()
            While reader.Read()
                i = i + 1
            End While
            If reader.FieldCount > 1 Then
                i = reader.FieldCount
            End If
            reader.Close()

            ReDim Me.arrayValores(i - 1)
            i = 0
            reader = adapter.SelectCommand.ExecuteReader()
            While reader.Read()
                For j As Integer = 0 To reader.FieldCount - 1
                    If reader.FieldCount = 1 Then
                        If bolupper Then
                            Me.arrayValores(i) = reader(j).ToString.Trim.ToUpper

                        Else
                            Me.arrayValores(i) = reader(j).ToString.Trim
                        End If

                    Else
                        If bolupper Then
                            Me.arrayValores(j) = reader(j).ToString.Trim.ToUpper
                        Else
                            Me.arrayValores(j) = reader(j).ToString.Trim
                        End If

                    End If
                Next
                i = i + 1
            End While
            Me.CloseCxn()
            ban = True
            If Me.arrayValores.Length = 0 Then
                ReDim Me.arrayValores(1)
            End If

        Catch ex As System.Exception
            ban = False
            ReDim Me.arrayValores(1)
            Me.arrayValores(0) = ex.Message + vbCrLf + "Error al ejecutar sentencia: " & vbCrLf & SQLString
            Me.CloseCxn()
        End Try
        Return ban
    End Function
    Public Function Select_SQL(SQLMainMenus As String, SQLChildMenus As String, menu As Menu, currentPage As String) As Boolean
        Dim ResultSet As Data.DataSet = Nothing
        Dim ban As Boolean = False
        Try
            ResultSet = Select_SQL(SQLMainMenus, ResultSet)
            menu.Items.Clear()
            If ResultSet.Tables.Count > 0 Then
                For Each row As Data.DataRow In ResultSet.Tables(0).Rows

                    Dim id As String = row(0).ToString()
                    Dim text As String = row(1).ToString()
                    Dim url As String = row(2).ToString()

                    Dim menuItem As MenuItem = New MenuItem With {
                                    .Value = id,
                                    .Text = text,
                                    .NavigateUrl = url,
                                    .Selected = url.ToString().EndsWith(currentPage, StringComparison.CurrentCultureIgnoreCase)
                                     }
                    menu.Items.Add(menuItem)
                Next
            End If
            Dim ResultSetChild As Data.DataSet = Nothing
            ResultSetChild = Select_SQL(SQLChildMenus, ResultSetChild)
            If ResultSetChild.Tables.Count > 0 Then
                For Each row As Data.DataRow In ResultSetChild.Tables(0).Rows

                    Dim idParent As String = row(0).ToString()
                    Dim id As String = row(1).ToString()
                    Dim text As String = row(2).ToString()
                    Dim url As String = row(3).ToString()

                    Dim SuBmenuItem As MenuItem = New MenuItem With {
                                    .Value = id,
                                    .Text = text,
                                    .NavigateUrl = url,
                                    .Selected = url.ToString().EndsWith(currentPage, StringComparison.CurrentCultureIgnoreCase)
                                     }
                    For Each mnu As MenuItem In menu.Items
                        If mnu.Value = idParent Then
                            mnu.Selectable = True
                            mnu.Selected = True
                            mnu.ChildItems.Add(SuBmenuItem)
                        End If
                    Next
                Next
            End If
            ban = True
        Catch ex As Exception
            arrayValores(0) = ex.Message
        End Try
        Return ban
    End Function
    Public Function Select_SQL(SQLMainMenus As String, SQLChildMenus As String, TREEMenu As TreeView, currentPage As String) As Boolean
        Dim ResultSet As Data.DataSet = Nothing
        Dim ban As Boolean = False
        Try
            ResultSet = Select_SQL(SQLMainMenus, ResultSet)
            TREEMenu.Nodes.Clear()
            If ResultSet.Tables.Count > 0 Then
                For Each row As Data.DataRow In ResultSet.Tables(0).Rows

                    Dim id As String = row(0).ToString()
                    Dim text As String = row(1).ToString()
                    Dim url As String = row(2).ToString()

                    Dim menuItem As TreeNode = New TreeNode With {
                                    .Value = id,
                                    .Text = text,
                                    .NavigateUrl = url,
                                    .Selected = url.ToString().EndsWith(currentPage, StringComparison.CurrentCultureIgnoreCase)
                                     }
                    TREEMenu.Nodes.Add(menuItem)
                Next
            End If
            Dim ResultSetChild As Data.DataSet = Nothing
            ResultSetChild = Select_SQL(SQLChildMenus, ResultSetChild)
            If ResultSetChild.Tables.Count > 0 Then
                For Each row As Data.DataRow In ResultSetChild.Tables(0).Rows

                    Dim idParent As String = row(0).ToString()
                    Dim id As String = row(1).ToString()
                    Dim text As String = row(2).ToString()
                    Dim url As String = row(3).ToString()

                    Dim TreeNodeChild As TreeNode = New TreeNode With {
                                    .Value = id,
                                    .Text = text,
                                    .NavigateUrl = url,
                                    .Selected = url.ToString().EndsWith(currentPage, StringComparison.CurrentCultureIgnoreCase)
                                     }
                    For Each treeIT As TreeNode In TREEMenu.Nodes
                        If treeIT.Value = idParent Then
                            treeIT.Selected = True
                            treeIT.ChildNodes.Add(TreeNodeChild)
                        End If
                    Next
                Next
            End If
            TREEMenu.ExpandAll()
            ban = True
        Catch ex As Exception
            arrayValores(0) = ex.Message
        End Try
        Return ban
    End Function

    Public Function Select_SQL(ByVal dropdown As DropDownList, ByVal SQLString As String, ByVal data As String, ByVal value As String) As Boolean
        Dim ban As Boolean = False
        Try
            Me.OpenCxn()
            Dim sqlDS As New SqlDataSource
            sqlDS.ConnectionString = Me.SqlPubsConnString
            sqlDS.SelectCommand = SQLString
            sqlDS.DataBind()
            dropdown.DataSourceID = Nothing
            dropdown.DataTextField = Nothing
            dropdown.DataValueField = Nothing
            dropdown.DataTextField = data
            dropdown.DataValueField = value
            dropdown.DataSource = sqlDS
            dropdown.DataBind()
            Me.CloseCxn()
            ban = True
        Catch ex As Exception
            Me.CloseCxn()
            Me.arrayValores(0) = ex.Message + vbCrLf + "Error al ejecutar sentencia: " & vbCrLf & SQLString
        End Try
        Return ban
    End Function
    Public Function Select_SQL(ByVal grdview As GridView, ByVal SQLString As String) As Boolean
        Dim ban As Boolean = False
        Try
            ReDim arrayValores(1)
            arrayValores(0) = Nothing
            Me.OpenCxn()
            Dim sqlDS As New SqlDataSource
            sqlDS.ConnectionString = Me.SqlPubsConnString
            sqlDS.SelectCommand = SQLString
            sqlDS.DataBind()
            grdview.DataSource = sqlDS
            grdview.DataBind()
            Me.CloseCxn()
            ban = True
        Catch ex As Exception
            Me.CloseCxn()
            Me.arrayValores(0) = ex.Message + vbCrLf + "Error al ejecutar sentencia: " & vbCrLf & SQLString
        End Try
        Return ban
    End Function
    Public Function Execute_SQL(ByVal SQLString As String) As Boolean
        Dim ban As Boolean = False
        Try
            SQLString = SQLString.Trim.ToUpper.ToString
            sqlComm1.Connection = Cn 'conecta bd
            sqlComm1.Connection.Open()
            sqlComm1.CommandText = SQLString
            sqlComm1.ExecuteNonQuery()
            ban = True
        Catch ex As System.Data.SqlClient.SqlException When ex.Number = 547
            ReDim Me.arrayValores(1)
            arrayValores(0) = "Error el dato existe en otra tabla "
        Catch ex As System.Data.SqlClient.SqlException When ex.Number = 2627
            ReDim Me.arrayValores(1)
            arrayValores(0) = "Registro Duplicado verifique"
        Catch ex As System.Exception
            ReDim Me.arrayValores(1)
            arrayValores(0) = ex.Message
        End Try
        Me.CloseCxn()
        Return ban
    End Function
    Private Sub ValuesAsign()
        Dim i As Integer
        For i = 0 To Me.arrayValores.Length
            arrayValores(i) = Nothing
        Next
    End Sub
    Private Function USER() As String
        Dim SQLString As String = ""
        Dim adapter As New SqlDataAdapter()
        Dim i As Integer
        Try
            Me.OpenCxn()
            SQLString = "Select User"
            sqlComm1.Connection = Cn 'conecta bd
            sqlComm1.CommandText = SQLString
            adapter.SelectCommand = sqlComm1
            Dim reader As System.Data.SqlClient.SqlDataReader
            reader = adapter.SelectCommand.ExecuteReader()
            While reader.Read()
                For i = 0 To reader.FieldCount - 1
                    Me.arrayValores(i) = reader(i).ToString.Trim.ToUpper
                Next
            End While
            Me.CloseCxn()
            Return Me.arrayValores(0).ToString.Trim.ToUpper
        Catch ex As System.Exception
            ReDim Me.arrayValores(1)
            Me.arrayValores(0) = ex.Message + vbCrLf + "Error al seleccionar usuario"
        End Try
        Return ""
    End Function
    Public Function GetReader(ByVal strSql As String) As System.Data.IDataReader
        Dim reader As System.Data.IDataReader = Nothing
        Dim adapter As New SqlDataAdapter()
        Dim ban As Boolean = False
        Try
            Me.OpenCxn()
            sqlComm1.Connection = Cn 'conecta bd
            sqlComm1.CommandText = strSql
            adapter.SelectCommand = sqlComm1
            reader = adapter.SelectCommand.ExecuteReader(System.Data.CommandBehavior.CloseConnection)
            ban = True
        Catch ex As System.Exception
            ReDim Me.arrayValores(1)
            Me.arrayValores(0) = ex.Message + vbCrLf + "Error al obtener Reader"
        End Try
        'Me.CloseCxn()
        Return reader
    End Function
    Function Select_SQL(ByVal SQL As String, ByVal dataset As Data.DataSet) As Data.DataSet
        Dim DBAdapter As SqlDataAdapter
        Dim ResultsDataSet As DataSet = New DataSet
        Try
            ' Run the query and create a DataSet.
            Me.OpenCxn()
            DBAdapter = New SqlDataAdapter(SQL, Cn)
            DBAdapter.Fill(ResultsDataSet)
            ' Close the database connection.
            Me.CloseCxn()
        Catch ex As Exception
            ReDim Me.arrayValores(1)
            Me.arrayValores(0) = ex.Message + vbCrLf + "Error al ejecutar sentencia: " & vbCrLf & SQL
            Me.CloseCxn()
        End Try
        Return ResultsDataSet

    End Function
    Function Select_SQL(ByVal tree As TreeView, ByVal SelectedNode As String, ByVal sql As String, ByVal Filter As String, ByVal Field As String) As Boolean
        Dim ResultSet As Data.DataSet = Nothing
        Dim ban As Boolean = False
        Try
            ResultSet = Select_SQL(sql, ResultSet)
            tree.Nodes.Clear()
            If ResultSet.Tables.Count > 0 Then

                For Each row As Data.DataRow In ResultSet.Tables(0).Rows
                    Dim newNode As TreeNode = New TreeNode()
                    Dim id As String = row(0).ToString()
                    Dim text As String = row(1).ToString()
                    Dim url As String = row(2).ToString()
                    Dim request_var As String = row(3).ToString()

                    create_node(newNode, id, text, url, request_var)
                    If SelectedNode.Trim <> "" AndAlso newNode.Value = SelectedNode Then
                        newNode.Selected = True
                    End If
                    tree.Nodes.Add(newNode)
                Next
            End If
            For i As Integer = 0 To tree.Nodes.Count - 1
                submenu(tree.Nodes(i), SelectedNode, sql, Filter, Field)
            Next
            tree.ExpandAll()
            ban = True
        Catch ex As Exception
            ReDim Me.arrayValores(1)
            Me.arrayValores(0) = ex.Message + vbCrLf + "Error al ejecutar sentencia: " & vbCrLf & sql
            Me.CloseCxn()
        End Try
        Return ban
    End Function

    Private Sub submenu(ByVal node As TreeNode, ByVal selectednode As String, ByVal sql As String, ByVal Filter As String, ByVal Field As String)
        Dim ResultSet As Data.DataSet = Nothing
        Dim sql1 As String = Replace(sql.ToLower, Filter, Field & "='" & node.Value & "' ")
        ResultSet = Select_SQL(sql1, ResultSet)
        ' Create the third-level nodes.
        If ResultSet.Tables.Count > 0 Then
            For Each row As Data.DataRow In ResultSet.Tables(0).Rows
                Dim newNode As TreeNode = New TreeNode()
                Dim id As String = row(0).ToString()
                Dim text As String = row(1).ToString()
                Dim url As String = row(2).ToString()
                Dim request_var As String = row(3).ToString()
                create_node(newNode, id, text, url, request_var)
                If selectednode.Trim <> "" AndAlso newNode.Value.ToString.ToLower = selectednode.ToLower Then
                    newNode.Selected = True
                End If

                Dim sql3 As String = Mid(sql, InStr(sql, " from "))
                sql3 = Mid(sql3, 1, InStr(sql3, " order by") - 1)
                sql3 = "Select count(*) " & Replace(sql3.ToLower, Filter.ToLower, Field.ToLower & "='" & newNode.Value & "' ")
                If Select_SQL(sql3) Then
                    If arrayValores(0) IsNot Nothing Then
                        If CInt(arrayValores(0)) > 0 Then
                            submenu(newNode, selectednode, sql, Filter.ToLower, Field.ToLower)
                        End If
                    End If
                End If
                node.ChildNodes.Add(newNode)
            Next
        End If
    End Sub
    Private Sub create_node(ByVal newNode As TreeNode, ByVal id As String, ByVal text As String,
                             ByVal URL As String, ByVal request_var As String)
        newNode.Value = id.Trim.ToUpper
        newNode.Text = text.Trim.ToUpper
        If URL <> "" Then
            newNode.NavigateUrl = URL
            If request_var <> "" Then
                newNode.NavigateUrl &= "?" & request_var & "=" & id
            End If
        End If
        newNode.PopulateOnDemand = True
        'newNode.SelectAction = TreeNodeSelectAction.Expand
    End Sub


    Public Function CargaBanner(ByVal tipo As Integer) As String
        Dim cxn As New cxnSQL
        Dim Texto As String = ""
        cxn.Select_SQL("Select * from tbl_imgs_page where tipo=" & tipo.ToString)
        If cxn.arrayValores(0) IsNot Nothing Then
            Texto = cxn.arrayValores(0)
        End If
        Return Texto
    End Function
End Class
