<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="LiquidaTraslado.aspx.vb" Inherits="WebTCMAdmin.LiquidaTraslado" %>
<%@ Register Assembly="Infragistics4.Web.v19.2, Version=19.2.20192.8, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI.LayoutControls" tagprefix="ig2" %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="System.Data.OleDb" %>
<%@ Register Assembly="Infragistics4.Web.v19.2, Version=19.2.20192.8, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI.EditorControls" tagprefix="ig" %>
<%@ Register Assembly="Infragistics4.WebUI.WebDataInput.v19.2, Version=19.2.20192.8, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.WebUI.WebDataInput" tagprefix="igtxt" %>
<%@ Register Assembly="Infragistics4.WebUI.WebResizingExtender.v19.2, Version=19.2.20192.8, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.WebUI" tagprefix="igui" %>
<%@ Register Assembly="Infragistics4.Web.v19.2, Version=19.2.20192.8, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI.ListControls" tagprefix="ig" %>
<%@ Register Src="~/App_Forms/Liquidacion/usrConfirmaPago.ascx" TagPrefix="uc1" TagName="usrConfirmaPago" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
        <div class="Titulo">IMPUESTO SOBRE TRASLADO DE DOMINIO DE INMBUEBLES</div>    
    <hr />
    <div class="SubTitulo"> <strong>INFORMACIÓN REQUERIDA</strong></div>        
          <table class="tablarequerimientos">            
            <tr>
                <td>
                </td>
                <td>
                </td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
             </tr>
            <tr>
                <td class="ColumnLabels">                    
                     <asp:Label ID="Label3" runat="server" Text="Clave Catastral:" CssClass="lbl"></asp:Label>
                </td>
                <td class="ColumnLabels">
                    <ig:WebMaskEditor ID="txtCveCat" CssClass="txtBx" runat="server" InputMask="9999999999999999" Width="120px" BackColor="White" Font-Names="Arial Narrow" Font-Size="Small">
                    </ig:WebMaskEditor>
                </td>
                <td class="ColumnLabels">                    
                     <asp:Label ID="Label7" runat="server" Text="Tipo de Traslado:" CssClass="lbl"></asp:Label>
                </td>
                <td class="ColumnLabels">
                    <ig:WebDropDown ID="DDLTipoTraslado" runat="server" Font-Names="Arial Narrow" Font-Size="XX-Small" Width="200px" CssClass="txtBx" DisplayMode="DropDownList" AutoPostBack="True">
                        <autopostbackflags selectionchanged="On" />
                        <Items>
                            <ig:DropDownItem Selected="True" Text="Entre particulares" Value="">
                            </ig:DropDownItem>
                            <ig:DropDownItem Selected="False" Text="En programas de regularización " Value="">
                            </ig:DropDownItem>
                        </Items>
                    </ig:WebDropDown>         
                </td>
                <td ></td>
            </tr>   
            <tr>
                <td class="ColumnLabels">
                     <asp:Label ID="lblRfc" runat="server" Text="Fecha de Operación:" CssClass="lbl"></asp:Label>
                </td>
                <td class="ColumnLabels">
                     <ig:WebDatePicker ID="WDFechaOperacion" runat="server" DataMode="Text" DisplayModeFormat="d" Height="18px" OpenCalendarOnFocus="True" Width="107px">
                     </ig:WebDatePicker>
                </td>
                <td class="ColumnLabels">
                     <asp:Label ID="lblTipoVivienda" runat="server" Text="Tipo de Vivienda:" CssClass="lbl" Visible="False"></asp:Label>
                </td>
                <td class="ColumnLabels">
                    <ig:WebDropDown ID="DDLTipoVivienda" runat="server" Font-Names="Arial Narrow" 
                        Font-Size="X-Small" Height="21px" Visible="False" Width="200px" CssClass="txtBx" DisplayMode="DropDownList" AutoPostBack="True">
                        <Items>
                            <ig:DropDownItem Selected="True" Text="De interés social" Value="">
                            </ig:DropDownItem>
                            <ig:DropDownItem Selected="False" Text="Social progresiva" Value="">
                            </ig:DropDownItem>
                            <ig:DropDownItem Selected="False" Text="Popular nueva o usada" Value="">
                            </ig:DropDownItem>
                        </Items>
                    </ig:WebDropDown>
                </td>
                <td >                    
                </td>
            </tr>              
             <tr>
                <td class="ColumnLabels">
                     <asp:Label ID="Label4" runat="server" Text="Monto de la operación:" CssClass="lbl" Width="150px"></asp:Label>
                 </td>
                <td class="ColumnLabels">
                        <ig:WebNumericEditor ID="txtMontoOperacion" runat="server" DataMode="Decimal" MaxDecimalPlaces="2" MaxLength="10" MinValue="0" NullValue="0" ToolTip="{Ingrese el monto de la operación}" Visible="False" Width="100px" CssClass="txtBx" Nullable="False">
                            <AutoPostBackFlags ValueChanged="On" />
                        </ig:WebNumericEditor>
                 </td>
                <td class="ColumnLabels">                       
                                   <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtMontoOperacion" ErrorMessage="Mayor que cero" InitialValue="0" SetFocusOnError="True" CssClass="alert-danger"></asp:RequiredFieldValidator>
                </td>
                <td class="ColumnLabels">                       
                       <asp:CheckBox ID="chkNotificado" runat="server" Font-Bold="True" Text="Notificado" CssClass="lbl" />
                 </td>
                <td>                   
                </td>
            </tr> 
             <tr>
                <td class="ColumnLabels">&nbsp;</td>
                <td class="ColumnLabels">
                       <asp:CheckBox ID="chkUsufructo" runat="server" Font-Bold="True" Text="Transmisión de usufructo" CssClass="lbl" Width="200px" />
                 </td>
                <td class="ColumnLabels">&nbsp;</td>
                <td class="ColumnLabels">&nbsp;</td>
                <td class="ColumnLabels">&nbsp;</td>
            </tr> 
             <tr>
                <td class="ColumnLabels">&nbsp;</td>
                <td class="ColumnLabels">
                    <asp:CheckBox ID="chkCopropiedad" runat="server" AutoPostBack="True" Text="Traslado de copropiedad" CssClass="lbl" Width="200px" />
                 </td>
                <td class="ColumnLabels">
                    <asp:Label ID="lblPorcentaje" runat="server" style="text-align: left" Text="Porcentaje:" Visible="False" CssClass="lbl"></asp:Label>                       
                 </td>
                <td class="ColumnLabels">
                    <ig:WebNumericEditor ID="txtporcentaje" runat="server" DataMode="Decimal" MaxDecimalPlaces="2" MaxLength="5" MaxValue="99" MinValue="0" Nullable="False" NullText="0" NullValue="0" Visible="False" Width="60px" CssClass="txtBx">
                    </ig:WebNumericEditor>                       
                 </td>
                <td>&nbsp;</td>
            </tr> 
             <tr>
                <td class="ColumnLabels">&nbsp;</td>
                <td class="ColumnLabels">&nbsp;</td>
                <td class="ColumnLabels">&nbsp;</td>
                <td class="ColumnLabels">&nbsp;</td>
                <td class="ColumnLabels"></td>
            </tr> 
            <tr>
                <td class="ColumnLabels">&nbsp;</td>
                <td class="ColumnLabels">&nbsp;</td>                                
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
                                    Enabled="False" />
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
