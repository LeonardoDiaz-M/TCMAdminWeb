<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="usrConfirmaPago.ascx.vb" Inherits="WebTCMAdmin.usrConfirmaPago" %>
<%@ Register Assembly="Infragistics4.Web.v19.2, Version=19.2.20192.8, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI.LayoutControls" tagprefix="ig2" %>
<ig2:WebDialogWindow ID="windowModal" runat="server" InitialLocation="Centered" Modal="True" Visible="False" CssClass="modalPnl">
                                    <contentpane>
                                        <template>
                                        <table class="tablaModalPago">
                                            <tr>
                                                <td>
                                                    <br />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <div class="modalTitulo"><asp:Label ID="lblTotalHdr" runat="server" Font-Bold="True" Font-Size="Medium" ForeColor="Black" Visible="False" Width="100%">TOTAL A PAGAR</asp:Label></div><br />
                                                    <asp:TextBox ID="txtTotalModal" runat="server" CssClass="modallblTotal" ReadOnly="True"></asp:TextBox>
                                                </td>
                                            <tr>
                                                <td>
                                                    <br />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="align-items:center">
                                                   
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="align-items:center">
                                                    <asp:Label ID="lblErrorModal" runat="server" Font-Bold="True" Font-Size="Medium" ForeColor="Red" Visible="False" Width="100%"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="ColumnTexto"><asp:Label ID="lblFmaPago" runat="server" Font-Bold="True" Font-Size="Medium" ForeColor="Black" Visible="False" Width="100%">Seleccione Forma de Pago:</asp:Label>
                                                   <asp:DropDownList ID="ddlFmaPago" runat="server" AutoPostBack="True" CssClass="txtBx" DataSourceID="DsDerechos" DataTextField="descripcion" DataValueField="Derecho_Id" Height="22px" Width="430px">
                                                   </asp:DropDownList>
                                                 </td>
                                            </tr>
                                            <tr>
                                                <td class="btn btn-danger glyphicon glyphicon-credit-card" >
                                                    <asp:Button ID="btnPagar" runat="server" BackColor="Transparent" Text="REGISTRAR PAGO" Width="100%"  BorderStyle="None" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Literal ID="litalert" runat="server"></asp:Literal>
                                                    <br />
                                                </td>
                                            </tr>
                                        </table>
                                        </template>
                                    </contentpane>
                                    <header captiontext="PAGO DE SERVICIOS" cssclass="modalHeader" Font-Bold="True" ForeColor="White" >
                                    </header>
                                    <resizer enabled="True" />
                                </ig2:WebDialogWindow>