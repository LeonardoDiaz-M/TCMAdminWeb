<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="LiquidaConvenioParcialidades.aspx.vb" Inherits="WebTCMAdmin.LiquidaConvenioParcialidades" %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="System.Data.OleDb" %>
<%@ Register Assembly="Infragistics4.Web.v19.2, Version=19.2.20192.8, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI.LayoutControls" tagprefix="ig2" %>
<%@ Register Assembly="Infragistics4.Web.v19.2, Version=19.2.20192.8, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI.EditorControls" tagprefix="ig" %>
<%@ Register Assembly="Infragistics4.Web.v19.2, Version=19.2.20192.8, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI.ListControls" tagprefix="ig" %>



<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">    
    <div class="Titulo">PAGO DE CONVENIOS O DE DIFERENCIAS DETECTADAS POR LA AUTORIDAD</div>    
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
                    <asp:Label ID="lblConcepto" runat="server" Text="Concepto:" Font-Bold="True"></asp:Label>
                </td>
                <td style="text-align: left; width: 114px;">                    
                    <ig:WebDropDown ID="WDDConceptoPago" runat="server" AutoPostBack="True" Width="200px" DisplayMode="DropDownList" >
                        <Items>
                            <ig:DropDownItem Selected="True" Text="Seleccione " Value="0">
                            </ig:DropDownItem>
                            <ig:DropDownItem Selected="False" Text="Predial" Value="1">
                            </ig:DropDownItem>
                            <ig:DropDownItem Selected="False" Text="Agua Potable" Value="2">
                            </ig:DropDownItem>
                            <ig:DropDownItem Selected="False" Text="Licencias Municipales" Value="3">
                            </ig:DropDownItem>
                            <ig:DropDownItem Selected="True" Text="Traslado de Dominio" Value="4">
                            </ig:DropDownItem>
                        </Items>
                    </ig:WebDropDown>
                </td>
                <td style="text-align: right"><asp:Label ID="lblConcepto0" runat="server" Text="Clave:" Font-Bold="True"></asp:Label>
                </td>
                <td style="text-align: left">
                    <asp:TextBox ID="txtClave" runat="server" BackColor="White" Font-Names="Arial Narrow" Font-Size="Small" MaxLength="16" Width="120px"></asp:TextBox>
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
             </table>    
        <br />
        <div class="SubTitulo">CAPTURA DEL PAGO</div> 
         <table class="tabladatos" >  
            <tr>              
                 <td></td>
                    
                <td>&nbsp; &nbsp;</td>
             </tr>  
             <tr>
                 <td class="ColumnLabels">Observación:</td>
                 <td class="ColumnTexto">
                     <ig:WebTextEditor ID="txtObservacion" runat="server" CssClass="txtBx"  Height="23px" Width="416px">
                     </ig:WebTextEditor>
                 </td>
                </tr>
                 <tr>
                     <td colspan="3" style="align-content:center">
                         <table>
                             <tr>
                                 <td style="width:40%" >                     
                                    <asp:CheckBox ID="chkParcialidad"   runat="server" AutoPostBack="True" Font-Bold="False" Text="Pago de parcialidad"  Width="100%" /> 
                                <td/>
                                <td style="width:40%" >                     
                                    <asp:CheckBox ID="chkDiferencia" runat="server" AutoPostBack="True" Font-Bold="False" Text="Pago de diferencia" Width="100%" /> 
                                <td/>                                 
                             </tr>
                         </table>
                     </td>
                 </tr>
                 
            <tr>              
                 <td></td>
                    
                <td>&nbsp; &nbsp;</td>
             </tr>              
         </table>
         <table class="tabladatos">
                      <tr>
                         <td class="gridHeader">Periodo Inicial:</td>
                         <td class="gridHeader">Periodo Final:</td>
                         <td class="gridHeader">Importe</td>
                         <td class="gridHeader">Actualización</td>
                         <td class="gridHeader">Saneamiento</td>
                         <td class="gridHeader">Recargos</td>
                         <td class="gridHeader">Multas</td>
                         <td class="gridHeader">Gastos de cobro</td>
                        </tr>
                      <tr>
                          <td class="gridRows">
                             <ig:WebMaskEditor ID="TxtPerInicio" runat="server" DataMode="AllText" Height="20px" InputMask="99-9999" PromptChar=" " Width="90px">
                             </ig:WebMaskEditor>
                         </td>
                        <td class="gridRows">
                             <ig:WebMaskEditor ID="TxtPerFinal" runat="server" DataMode="AllText" Height="20px" InputMask="99-9999" PromptChar=" " Width="90px">
                             </ig:WebMaskEditor>
                         </td>
                         <td class="gridRows">
                             <ig:WebNumericEditor ID="WNEImporte" runat="server" DataMode="Decimal" Height="24px" MaxDecimalPlaces="2" MinDecimalPlaces="2" Nullable="False" Width="90px">
                             </ig:WebNumericEditor>
                         </td>
                         <td class="gridRows">
                             <ig:WebNumericEditor ID="WNEActualizacion" runat="server" DataMode="Decimal" Height="24px" MaxDecimalPlaces="2" MinDecimalPlaces="2" Nullable="False" Width="90px">
                             </ig:WebNumericEditor>
                         </td>
                         <td class="gridRows">
                             <ig:WebNumericEditor ID="WNESaneamiento" runat="server" DataMode="Decimal" Height="24px" MaxDecimalPlaces="2" MinDecimalPlaces="2" Nullable="False" Width="90px">
                             </ig:WebNumericEditor>
                         </td>
                        <td class="gridRows">
                             <ig:WebNumericEditor ID="WNERecargos" runat="server" DataMode="Decimal" MaxDecimalPlaces="2" MinDecimalPlaces="2" Height="19px" MinValue="0" NullValue="0" Width="90px" Nullable="False">
                             </ig:WebNumericEditor>
                         </td>
                        <td class="gridRows">
                             <ig:WebNumericEditor ID="WNEMultas" runat="server" DataMode="Decimal" MaxDecimalPlaces="2" MinDecimalPlaces="2" Height="19px" MinValue="0" NullValue="0" Width="90px" Nullable="False">
                             </ig:WebNumericEditor>
                         </td>
                          <td class="gridRows">
                         <ig:WebNumericEditor ID="WNEGastos" runat="server" DataMode="Decimal" MaxDecimalPlaces="2" MinDecimalPlaces="2" Height="20px" MinValue="0" NullValue="0" Width="90px" Nullable="False">
                             </ig:WebNumericEditor>
                         </td>
                     </tr>
                        <tr>
                            <td class="gridRows" colspan="8">
                                 
                                &nbsp;&nbsp; &nbsp;</td>
                        </tr>
                        <tr>
                            <td class="gridRows"  colspan="7"></td>
                            <td class="gridRows" style="width:20%" >
                                     <div style="align-content:center;width:100%" class="btn btn-success glyphicon glyphicon-arrow-right">
                                            <asp:Button ID="btnProcesar" BorderStyle="None" runat="server" BackColor="Transparent" Font-Bold="True" ForeColor="White" Text="Procesar pago" />                     
                                    </div>
                                <td/> 
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
                                <asp:LinkButton ID="btnimprime" runat="server"  BackColor="Transparent" ForeColor="White" PostBackUrl="~/Reports/Report.aspx?Nombre=Reports/rpliquidapredial.rdlc">Imprimir</asp:LinkButton>
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
                                    <header captiontext="PAGO DE SERVICIOS" cssclass="modalHeader" Font-Bold="True" ForeColor="White" Font-Size="X-Large">
                                    </header>
                                    <resizer enabled="True" />
                                </ig2:WebDialogWindow>
    </asp:Panel>
    
    <asp:Literal ID="litalert" runat="server"></asp:Literal>
    <br />    
    </asp:Content>
