<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="LiquidaInfraccionesTransito.aspx.vb" Inherits="WebTCMAdmin.LiquidaInfraccionesTransito" %>

<%@ Register Assembly="Infragistics4.Web.v19.2, Version=19.2.20192.8, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI.LayoutControls" tagprefix="ig2" %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="System.Data.OleDb" %>
<%@ Register Assembly="Infragistics4.Web.v19.2, Version=19.2.20192.8, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI.EditorControls" tagprefix="ig" %>
<%@ Register Assembly="Infragistics4.WebUI.WebDataInput.v19.2, Version=19.2.20192.8, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.WebUI.WebDataInput" tagprefix="igtxt" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">    
    <div class="Titulo">
        <asp:Label ID="lblTitulo" runat="server"></asp:Label>
    </div>    
    <hr />
    <div class="SubTitulo"> <strong>INFORMACIÓN REQUERIDA</strong></div>        
     <table class="tablarequerimientos">
            <tr>
                   <td>&nbsp;</td> 
                   <td>&nbsp;</td>
                   <td>&nbsp;</td>                   
                   <td></td>                 
             </tr>    
             <tr>                                 
                 <td>&nbsp;</td>
                  <td class="ColumnLabels" style="text-align:right;">
                       <asp:Label ID="Label1" runat="server" Text="Nombre: " CssClass="lbl"></asp:Label>
                  </td>                  
                  <td class="ColumnLabels">
                        <asp:TextBox ID="TxtNombre" runat="server" CssClass="txtBx" MaxLength="100"></asp:TextBox>  
                   </td>
                   <td class="ColumnTexto" style="text-align:left;">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                ControlToValidate="TxtNombre" ErrorMessage=" Digite el Nombre" 
                                Width="100px" CssClass="txtBx" ForeColor="Red" 
                                SetFocusOnError="True">
                            </asp:RequiredFieldValidator>
                   </td>                 
             </tr>
             <tr> 
                   <td>&nbsp;</td> 
                   <td class="ColumnLabels" style="text-align:right;">                     
                        <asp:Label ID="Label8" runat="server" Text="Dirección:" CssClass="lbl"></asp:Label>
                   </td>                   
                   <td class="ColumnTexto">
                       <asp:TextBox ID="TxtDireccion" runat="server" CssClass="txtBx" Width="318px" MaxLength="100"></asp:TextBox>
                   </td>
                 <td class="ColumnTexto" style="text-align:left;">
                     <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                                ControlToValidate="TxtDireccion" ErrorMessage="Digite Dirección" 
                                Width="100px" CssClass="txtBx" ForeColor="Red"
                                SetFocusOnError="True">
                    </asp:RequiredFieldValidator>
                 </td>                 
             </tr>    
           
             <tr>
                   <td>&nbsp;</td>
                   <td class="ColumnLabels" style="text-align:right;">               
                        <asp:Label ID="Label10" runat="server" Text="RFC:" CssClass="lbl"></asp:Label>
                   </td>                   
                   <td class="ColumnLabels">
                       <asp:TextBox ID="TxtRFC" runat="server" MaxLength="16" Width="114px" CssClass="txtBx"></asp:TextBox>
                   </td>
                 <td class="ColumnTexto">&nbsp;</td>
             </tr>    
           
             <tr>
                 <td>&nbsp;</td>
                 <td class="ColumnLabels" style="text-align:right;">
                        <asp:Label ID="Label9" runat="server" Text="Concepto de pago:" CssClass="lbl"></asp:Label>
                   </td>                   
                   <td class="ColumnTexto">
                       <asp:DropDownList ID="ddlDerechos" runat="server" AutoPostBack="True" CssClass="txtBx" DataSourceID="DsDerechos" DataTextField="descripcion" DataValueField="Derecho_Id" Height="22px" Width="430px">
                       </asp:DropDownList>
                   </td>
                 <td class="ColumnTexto">&nbsp;</td>
             </tr>    
           
             <tr>
                   <td>&nbsp;</td>
                   <td class="ColumnLabels">&nbsp;</td>                   
                   <td class="ColumnLabels" style="text-align:left;">
                       <table>
                           <tr>
                               <td>&nbsp;</td>
                               <td><asp:Label ID="Label2" runat="server" Visible="True" CssClass="lbl" Text="Fecha Infracción:"></asp:Label></td>
                               <td>&nbsp;</td>
                               <td>     
                                        <ig:WebDatePicker ID="WDFechaInfraccion" runat="server" DataMode="Text" DisplayModeFormat="d" OpenCalendarOnFocus="True" SelectionOnFocus="NotSet" Width="107px">
                                        </ig:WebDatePicker>                    
                                </td>
                               <td>&nbsp;</td>
                               <td>&nbsp;</td>
                               <td><asp:Label ID="lblDatoUno" runat="server" Visible="False" CssClass="lbl" Text="123aaasaaa"></asp:Label></td>
                               <td>&nbsp;</td>
                               <td>
                                   <ig:WebNumericEditor ID="TxtDatoUno" runat="server" Height="19px" MaxDecimalPlaces="2" MinDecimalPlaces="2" MinValue="0" Nullable="False" NullValue="0" Width="70px" Visible="False">
                                    </ig:WebNumericEditor>
                               </td>
                               <td>&nbsp;</td>
                               <td>&nbsp;</td>
                               <td><asp:Label ID="lblDatoDos" runat="server" Visible="False" CssClass="lbl" Text="123aaasaaa"></asp:Label></td>
                               <td>&nbsp;</td>
                               <td>
                                   <ig:WebNumericEditor ID="TxtDatoDos" runat="server" Height="19px" MaxDecimalPlaces="2" MinDecimalPlaces="2" MinValue="0" Nullable="False" NullValue="0" Width="70px" Visible="False">
                                   </ig:WebNumericEditor>
                               </td>
                           </tr>
                       </table>                                                                                            
                   </td>
                 <td class="ColumnTexto">&nbsp;</td>
             </tr>    
           
             <tr>
                  <td>&nbsp;</td> 
                  <td class="ColumnLabels">                        
                        <asp:Label ID="Label11" runat="server" Text="Observación:" CssClass="lbl"></asp:Label>
                   </td>                   
                   <td class="ColumnTexto">
                       <asp:TextBox ID="TxtObservacion" runat="server" Height="16px" CssClass="txtBx"></asp:TextBox>
                   </td>
                 <td class="ColumnTexto" style="text-align:left;">
                     &nbsp;</td>
             </tr>    
           
             <tr>
                   <td>&nbsp;</td> 
                   <td>&nbsp;</td>
                   <td>&nbsp;</td>                   
                   <td></td>                 
             </tr>    
           
             <tr>
                  <td>&nbsp;</td>
                  <td>&nbsp;</td>
                   <td class="btn btn-success glyphicon glyphicon-arrow-right" >
                       <asp:Button  ID="Button2"  runat="server" BackColor="Transparent" ForeColor="White"  Text="Enviar" BorderWidth="0px" ViewStateMode="Enabled" />
                   </td>                                       
                   <td>&nbsp;</td>                      
             </tr>    
             <tr>
                   <td>&nbsp;</td> 
                   <td>&nbsp;</td>
                   <td>&nbsp;</td>                   
                   <td></td>                 
             </tr>    
             </table>  
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
                                <asp:LinkButton ID="btnimprime" runat="server"  BackColor="Transparent" ForeColor="White" PostBackUrl="~/Reports/Report.aspx?Nombre=Reports/rpliquidapredial.rdlc" OnClientClick="window.document.forms[0].target='_blank'">Imprimir</asp:LinkButton>
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
                                    <header captiontext="PAGO DE SERVICIOS" cssclass="modalHeader" Font-Bold="True" ForeColor="White"  >
                                    </header>
                                    <resizer enabled="True" />
                                </ig2:WebDialogWindow>
    </asp:Panel>
    
    <asp:Literal ID="litalert" runat="server"></asp:Literal>
    <asp:SqlDataSource ID="DsDerechos" runat="server" ConnectionString="<%$ ConnectionStrings:SQLDB %>" SelectCommand="SELECT Derecho_Id, descripcion FROM tbl_derechos WHERE (cve_cuenta = @Param1) AND (ejfiscal = CAST(YEAR(GETDATE()) AS CHAR(4))) ORDER BY descripcion">
        <SelectParameters>
            <asp:QueryStringParameter DefaultValue="020201" Name="Param1" QueryStringField="CveCuenta" />
        </SelectParameters>
    </asp:SqlDataSource>
    <br />    
    </asp:Content>
