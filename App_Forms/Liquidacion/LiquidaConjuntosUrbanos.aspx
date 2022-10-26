<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="LiquidaConjuntosUrbanos.aspx.vb" Inherits="WebTCMAdmin.LiquidaConjuntosUrbanos" %>
<%@ Register Assembly="Infragistics4.Web.v19.2, Version=19.2.20192.8, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI.LayoutControls" tagprefix="ig2" %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="System.Data.OleDb" %>
<%@ Register Assembly="Infragistics4.Web.v19.2, Version=19.2.20192.8, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI.EditorControls" tagprefix="ig" %>
<%@ Register Assembly="Infragistics4.WebUI.WebDataInput.v19.2, Version=19.2.20192.8, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.WebUI.WebDataInput" tagprefix="igtxt" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
        <div class="Titulo">IMPUESTO MUNICIPAL SOBRE CONJUNTOS URBANOS</div>    
    <hr />
    <div class="SubTitulo"> <strong>INFORMACIÓN REQUERIDA</strong></div>        
          <table class="tablarequerimientos">            
            <tr>
                <td style="text-align: right; height: 20px;">
                </td>
                <td style="text-align: left; width: 114px; height: 20px;">
                </td>
                <td style="text-align: left; height: 20px;">
                    &nbsp;</td>
                <td style="text-align: left; height: 20px;">
                </td>
                <td style ="text-align:left; height: 20px;" >
                </td>
             </tr>
            <tr>
                <td style="text-align: right">                    
                     <asp:Label ID="Label3" runat="server" Text="Nombre:" CssClass="lbl"></asp:Label>
                </td>
                <td style="text-align: left; width: 114px;">                    
                    <asp:TextBox ID="TxtNombre" runat="server" CssClass="txtBx" MaxLength="50"></asp:TextBox>                     
                </td>
                <td style="text-align: left"><asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                ControlToValidate="TxtNombre" ErrorMessage="Digite el nombre" 
                                SetFocusOnError="True"></asp:RequiredFieldValidator></td>
                <td style="text-align: left"> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;                   
                </td>
                <td >                    
                </td>
            </tr>   
            <tr>
                <td style="text-align: right">                                         
                     <asp:Label ID="Label7" runat="server" Text="Dirección:" CssClass="lbl"></asp:Label>
                </td>
                <td style="text-align: left; width: 114px;">                    
                     <asp:TextBox ID="TxtDireccion" runat="server" CssClass="txtBx" MaxLength="50"></asp:TextBox>
                </td>
                <td style="text-align: left">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                                ControlToValidate="TxtDireccion" ErrorMessage="Digite la dirección" 
                                SetFocusOnError="True"></asp:RequiredFieldValidator>

                </td>
                <td style="text-align: left"> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;                   
                </td>
                <td >                    
                </td>
            </tr>   
             <tr>
                <td style="text-align: right">                                         
                     <asp:Label ID="lblRfc" runat="server" Text="RFC:" CssClass="lbl"></asp:Label>
                </td>
                <td style="text-align: left; width: 114px;">                    
                        <asp:TextBox ID="TxtRFC" runat="server" CssClass="txtBx"  MaxLength="13"></asp:TextBox>
                </td>
                <td style="text-align: left">
                        <asp:CheckBox ID="chkNotificado" runat="server" Text="Notificado" CssClass="txtBx" />
                </td>
                <td style="text-align: left">
                             
                </td>
                <td  >                    
                </td>
            </tr> 
             <tr>
                <td style="text-align: right">                                         
                     <asp:Label ID="Label4" runat="server" Text="Tipo de Servicio:" CssClass="lbl"></asp:Label>
                </td>
                <td style="text-align: left; width: 114px;">                    
                        <asp:DropDownList ID="ddlDerechos" runat="server" CssClass="gridRows" 
                                Height="22px" Width="430px" AutoPostBack="True" DataSourceID="DsDerechos" 
                                DataTextField="descripcion" DataValueField="Derecho_Id">
                            </asp:DropDownList>
                </td>
                <td style="text-align: left">
                       
                </td>
                <td style="text-align: left">
                       
                </td>
                <td>                   
                </td>
            </tr> 
             <tr>
                <td style="text-align: right">                                         
                     <asp:Label ID="lblDatoAdicional" runat="server" Text="Cantidad:" CssClass="lbl"></asp:Label>
                </td>
                <td style="text-align: left;">                    
                        <asp:TextBox ID="TxtDatoAdicional" runat="server" CssClass="txtBx" AutoPostBack="True"></asp:TextBox>
                </td>
                <td style="text-align: left">
                       &nbsp;</td>
                <td style="text-align: left">
                       &nbsp;</td>
                <td>                   
                </td>
            </tr> 
            <tr>
                <td style="text-align: right">
                       <asp:Label ID="lblNumViv0" runat="server" Text="Fecha de inicio:" CssClass="lbl"></asp:Label>
                </td>
                <td style="text-align: left;">
                       <ig:WebDatePicker ID="WDFechaInicio" runat="server" DataMode="Text" DisplayModeFormat="d" CssClass="txtBx">
                        </ig:WebDatePicker>
                </td>                                
                <td class="btn btn-success glyphicon glyphicon-arrow-right" >
                    <asp:Button  ID="Button2"  runat="server" BackColor="Transparent" ForeColor="White"  Text="Enviar" BorderWidth="0px" ViewStateMode="Enabled" />
                </td>
                <td></td>
             </tr>
            <tr>
                <td></td>
                <td></td>
                <td>&nbsp;</td>
                <td></td>
                <td></td>
             </tr>
            </table>
        
    <br />
   
    &nbsp;&nbsp;&nbsp;
   
    <asp:Panel ID="DatLiq" runat="server" Visible="false">
       <table cellpadding="0" cellspacing="0" class="Tablaliquida">
            <tr>
               <td class="SubTitulo">
                    DETALLES DEL ADEUDO CORRESPONDIENTE
               </td>
            </tr>
            <tr>
               <td>
                 <asp:Panel ID="Panel1" runat="server" ScrollBars="Vertical" 
                        Width="100%" style="text-align: center">
                    <asp:GridView ID="grdresults" runat="server" CssClass="gridGral">
                        <EmptyDataRowStyle CssClass="labelerror" />
                        <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
                        <RowStyle CssClass="gridRows"/>
                        <AlternatingRowStyle CssClass="gridRowsAlt" />
                        <PagerStyle BorderColor="Silver" BorderStyle="Solid" 
                            ForeColor="#8C4510" HorizontalAlign="Center" />
                        <SelectedRowStyle BackColor="#738A9C" ForeColor="White" Font-Bold="True" />
                        <HeaderStyle CssClass="gridHeader" />
                        <Columns>
                            <asp:TemplateField HeaderText="Select">
                            <ItemTemplate>
                                <asp:CheckBox ID="chkSelect" runat="server" AutoPostBack="True" 
                                    oncheckedchanged="chkSelect_CheckedChanged"/>
                            </ItemTemplate>
                            <HeaderTemplate>
                            </HeaderTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <SortedAscendingCellStyle BackColor="#FFF1D4" />
                        <SortedAscendingHeaderStyle BackColor="#B95C30" />
                        <SortedDescendingCellStyle BackColor="#F1E5CE" />
                        <SortedDescendingHeaderStyle BackColor="#93451F" />
                    </asp:GridView>
                 </asp:Panel>
               </td>
             </tr>            
       </table>
       <table class="BarraPago">
             <tr>
                            <td Class="btn btn-small btn-primary glyphicon glyphicon-print" >                                 
                                <asp:LinkButton ID="btnimprime" runat="server"  BackColor="Transparent" ForeColor="White" OnClientClick="window.document.forms[0].target='_blank'" PostBackUrl="~/Reports/Report.aspx?Nombre=Reports/rpliquidaagua.rdlc" Height="100%" Width="100%">Imprimir</asp:LinkButton>
                             </td>           
                            <td>&nbsp;</td>
                            <td class="btn btn-warning glyphicon glyphicon-ok">                               
                                <asp:Button  ID="btnContinuar"  runat="server" BackColor="Transparent" ForeColor="White"  Text="Realizar Cobro" BorderWidth="0px" ViewStateMode="Enabled" />
                            </td>          
                            <td>&nbsp;</td>                            
                            <td Class="lblTotal">
                                <asp:Label ID="lblTotal" runat="server" Text="Total: 99999999.99" CssClass="lblTotal"/>                                
                            </td>
                            <td>&nbsp;</td>
                        </tr>
        </table>

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
                                                    <div class="modalTitulo">TOTAL A PAGAR</div><br />
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
                                               <td class="ColumnTexto">Forma de Pago:
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
        </asp:Panel>
    
    <asp:Literal ID="litalert" runat="server"></asp:Literal>
    <br />    
</asp:Content>
