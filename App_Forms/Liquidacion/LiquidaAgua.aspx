<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="LiquidaAgua.aspx.vb" Inherits="WebTCMAdmin.LiquidaAgua" %>
<%@ Register Assembly="Infragistics4.Web.v19.2, Version=19.2.20192.8, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI.LayoutControls" tagprefix="ig2" %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="System.Data.OleDb" %>
<%@ Register Assembly="Infragistics4.Web.v19.2, Version=19.2.20192.8, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI.EditorControls" tagprefix="ig" %>
<%@ Register Assembly="Infragistics4.WebUI.WebDataInput.v19.2, Version=19.2.20192.8, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.WebUI.WebDataInput" tagprefix="igtxt" %>
<%@ Register Src="~/App_Forms/Liquidacion/usrConfirmaPago.ascx" TagPrefix="uc1" TagName="usrConfirmaPago" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
   <div class="Titulo">SUMINISTRO DE AGUA POTABLE DRENAJE Y ALCANTARILLADO</div>    
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
                    <asp:Label ID="lblCveCat" runat="server" Text="Clave catastral:" Font-Bold="True"></asp:Label>
                </td>
                <td style="text-align: left; width: 114px;">                    
                    <ig:WebMaskEditor ID="txtCveCta" runat="server" InputMask="99999999" Width="120px" BackColor="White" Font-Names="Arial Narrow" Font-Size="Small">
                    </ig:WebMaskEditor>
                </td>
                <td style="text-align: left">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;</td>
                <td style="text-align: left">
                    <asp:RadioButtonList ID="rbFmaLiq" runat="server" Font-Size="Small" Font-Bold="True" RepeatLayout="Table" RepeatDirection="Horizontal">
                        <asp:ListItem Value="1" Selected="True">Anual</asp:ListItem>
                        <asp:ListItem Value="2">Por período</asp:ListItem>
                    </asp:RadioButtonList>
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
       </asp:Panel>
       <asp:Panel ID="pnlBtns" runat="server" Visible="false">
       <table class="BarraPago">
             <tr>
                            <td Class="btn btn-small btn-primary glyphicon glyphicon-print" >                                 
                                <asp:Button ID="btnImprimir" runat="server" BackColor="Transparent" BorderWidth="0px" ForeColor="White" Text="Imprimir" ViewStateMode="Enabled" />
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
        </asp:Panel>
    <uc1:usrConfirmaPago runat="server" id="usrConfirmaPago" />
    <asp:Literal ID="litalert" runat="server"></asp:Literal>
    <br />    
</asp:Content>
