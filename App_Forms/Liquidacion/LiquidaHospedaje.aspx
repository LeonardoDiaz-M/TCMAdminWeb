﻿<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="LiquidaHospedaje.aspx.vb" Inherits="WebTCMAdmin.LiquidaHospedaje" %>
<%@ Register assembly="Infragistics4.Web.v15.1, Version=15.1.20151.2400, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI.LayoutControls" tagprefix="ig2" %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="System.Data.OleDb" %>
<%@ Register assembly="Infragistics4.Web.v15.1, Version=15.1.20151.2400, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI.EditorControls" tagprefix="ig" %>
<%@ Register assembly="Infragistics4.WebUI.WebDataInput.v15.1, Version=15.1.20151.2400, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.WebUI.WebDataInput" tagprefix="igtxt" %>
<%@ Register assembly="Infragistics4.Web.v15.1, Version=15.1.20151.2400, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI.LayoutControls" tagprefix="ig2" %>


<%@ Register namespace="AjaxControlToolkit" tagprefix="AjaxControlToolkit" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
   <div class="Titulo">Estacionamientos Privados de Servicio Público</div>    
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
                    <asp:Label ID="Label6" runat="server" Font-Bold="true" Text="Número de registro:"></asp:Label>
                </td>
                <td style="text-align: left; width: 114px;">                    
                    <ig:WebMaskEditor ID="txtCveCta" runat="server" InputMask="99999999" Width="120px" BackColor="White" Font-Names="Arial Narrow" Font-Size="Small">
                    </ig:WebMaskEditor>
                </td>
                <td style="text-align: right">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
                    <asp:Label ID="Label5" runat="server" Font-Bold="true" Text="Base gravable:"></asp:Label></td>
                <td style="text-align: left">                                        
                    <ig:WebMaskEditor ID="txtBaseGravable" runat="server" InputMask="999,999,999" Width="144px" BackColor="White" Font-Names="Arial Narrow" Font-Size="Small">
                    </ig:WebMaskEditor>
                </td>
                <td class="btn btn-success glyphicon glyphicon-arrow-right" >
                    <asp:Button  ID="Button2"  runat="server" BackColor="Transparent" ForeColor="White"  Text="Enviar" BorderWidth="0px" ViewStateMode="Enabled" />
                </td>
             </tr>   
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
            </table>
        
    <br />
    <asp:Panel ID="DatCont" runat="server" Visible="false">
    <div class="SubTitulo">DATOS DEL PROPIETARIO</div>              
        <table class="tabladatos">
             <tr>
                   <td>&nbsp;</td>
                  <td class="ColumnLabels">
                       <asp:Label ID="Label3" runat="server" Text="Nombre:" CssClass="lbl"></asp:Label>
                  </td>
                  <td>&nbsp;</td>
                  <td class="ColumnTexto">
                        <asp:TextBox ID="txtNombre" runat="server" CssClass="txtBx" ReadOnly="True"></asp:TextBox>
                   </td>
             </tr>
             <tr>
                   <td>&nbsp;</td>
                  <td class="ColumnLabels">
                       <asp:Label ID="Label1" runat="server" Text="Propietario:" CssClass="lbl"></asp:Label>
                  </td>
                  <td>&nbsp;</td>
                  <td class="ColumnTexto">
                        <asp:TextBox ID="TxtPropietario" runat="server" CssClass="txtBx" ReadOnly="True"></asp:TextBox>
                   </td>
             </tr>
             <tr>
                  <td>&nbsp;</td>
                   <td class="ColumnLabels">
                        <asp:Label ID="Label2" runat="server" Text="  Ubicación:" CssClass="lbl"></asp:Label>                       
                   </td>
                   <td>&nbsp;</td>
                   <td class="ColumnTexto">
                        <asp:TextBox ID="TxtUbicacion" runat="server" CssClass="txtBx" ReadOnly="True"></asp:TextBox>
                   </td>
             </tr>
             <tr>                    
                   <td colspan="4" class="ColumnTexto">
                        <table class="SubTable">
                        <tr>
                            <td class="ColumnLabels"><div class="lbl">Año Pago:</div></td>
                            <td class="ColumnLabels">
                                <asp:TextBox ID="TxtAño" runat="server" CssClass="nmBx" ReadOnly="True" ></asp:TextBox>
                            </td>
                            <td class="ColumnLabels"><div class="lbl">Mes Pago:</div></td>
                            <td class="ColumnLabels">
                                <asp:TextBox ID="TxtMes" runat="server" CssClass="nmBx" ReadOnly="True"></asp:TextBox>
                            </td>
                            <td class="ColumnLabels">&nbsp;</td>
                            <td class="ColumnLabels">
                                    &nbsp;</td>
                        </tr>
                        </table>
                   </td>
             </tr>
            <tr>
                <td>&nbsp;</td>
                <td class="ColumnLabels"><div class="lbl">Observación:</div></td>
                <td>&nbsp;</td>
                <td class="ColumnTexto">                                                
                        <ig:WebTextEditor ID="txtObservacion" runat="server" CssClass="txtBx" >
                        </ig:WebTextEditor>
                </td>               
            </tr>
             </table>    
         
    </asp:Panel>           
    <br />
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
                                <asp:LinkButton ID="btnimprime" runat="server"  BackColor="Transparent" ForeColor="White" PostBackUrl="~/Reports/Report.aspx?Nombre=Reportes/rpliquidaHospedaje.rdlc" OnClientClick="window.document.forms[0].target='_blank'" Height="100%" Width="100%">Imprimir</asp:LinkButton>
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
                                                <td>
                                                    <br />
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
                                    <header captiontext="PAGO DE SERVICIOS" cssclass="modalHeader" Font-Bold="True" ForeColor="White" Font-Size="X-Large">
                                    </header>
                                    <resizer enabled="True" />
                                </ig2:WebDialogWindow>
        </asp:Panel>
    
    <asp:Literal ID="litalert" runat="server"></asp:Literal>
    <br />    
</asp:Content>
