<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Password.aspx.vb" Inherits="WebTCMAdmin.Password" %>
<%@ Register Assembly="Infragistics4.Web.v19.2, Version=19.2.20192.8, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI.EditorControls" tagprefix="ig" %>
<%@ Register Assembly="Infragistics4.Web.v19.2, Version=19.2.20192.8, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI.LayoutControls" tagprefix="ig" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <ig:WebDialogWindow ID="winPCAccess" runat="server" Height="250px" InitialLocation="Centered" 
                                        Modal="True" Width="350px">
                                        <contentpane>
                                            <template>
                                            <table cellpadding="2" cellpacing="2" style="width:100%; height:100%">
                                                <tr>
                                                    <td></td>                                                   
                                                    <td>Contraseña Actual</td>                                                   
                                                    <td>
                                                        <asp:TextBox ID="pswActual" runat="server" Height="90%" TextMode="Password" Width="90%"></asp:TextBox>
                                                    </td>                                                   
                                                    <td></td>                                                   
                                                </tr>                                                                                             
                                                <tr>
                                                    <td></td>                                                   
                                                    <td>Nueva Contraseña</td>                                                   
                                                    <td>
                                                        <asp:TextBox ID="pswNuevo" runat="server" Height="90%" TextMode="Password" Width="90%"></asp:TextBox>
                                                    </td>                                                   
                                                    <td></td>                                                   
                                                </tr>                                                           
                                                <tr>
                                                    <td></td>                                                   
                                                    <td>Confirmar Nueva Contraseña</td>                                                   
                                                    <td>
                                                        <asp:TextBox ID="pswConfirm" runat="server" Height="90%" TextMode="Password" Width="90%"></asp:TextBox>
                                                    </td>                                                   
                                                    <td></td>                                                   
                                                </tr>                                                           
                                                <tr>
                                                    <td></td>                                                   
                                                    <td colspan="2">
                                                         <asp:Label ID="lblErrorModal" runat="server" Font-Bold="True" Font-Size="Medium" ForeColor="Red" Visible="False" Width="100%"></asp:Label>
                                                    </td>                                                   
                                                    <td></td>                                                   
                                                </tr>               
                                                <tr>
                                                    <td></td>                                                   
                                                    <td colspan="2">
                                                        <asp:Button ID="btnActualizar" runat="server" Height="80%" Text="Cambiar Contraseña" Width="80%" />
                                                    </td>                                                   
                                                    <td></td>                                                   
                                                </tr>                                                           
                                            </table>
                                            </template>
                                        </contentpane>
                                        <header captiontext="CAMBIAR PASSWORD">
                                        </header>
                                        <resizer enabled="False " />                                    
                                    </ig:WebDialogWindow>
</asp:Content>
