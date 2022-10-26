<%@ Page Language="VB" MasterPageFile="~/Site.Master" AutoEventWireup="false" CodeFile="LiquidaVideoJuegos.aspx.vb" Inherits="App_Forms_LiquidaVideoJuegos" Title="Juegos de Video y Otros" AspCompat="true" %>

<%@ Register Assembly="Infragistics4.Web.v19.2, Version=19.2.20192.8, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI.LayoutControls" tagprefix="ig2" %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="System.Data.OleDb" %>
<%@ Register Assembly="Infragistics4.Web.v19.2, Version=19.2.20192.8, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI.EditorControls" tagprefix="ig" %>
<%@ Register Assembly="Infragistics4.WebUI.WebDataInput.v19.2, Version=19.2.20192.8, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.WebUI.WebDataInput" tagprefix="igtxt" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">

  <div class="Titulo">DIVERSIONES JUEGOS Y ESPECTÁCULOS</div>    
    <hr />
    <div class="SubTitulo"> <strong>INFORMACIÓN REQUERIDA</strong></div>        
          <table class="tablarequerimientos">            
            <tr>
                <td style="text-align: right; height: 20px;">
                </td>
                <td style="text-align: left; width: 114px; height: 20px;">
                </td>
                <td style="text-align: left; height: 20px;">
                    &nbsp;&nbsp;&nbsp; </td>
                <td style="text-align: left; height: 20px;">
                    &nbsp;</td>
                <td style="text-align: left; height: 20px;">
                </td>
                <td style ="text-align:left; height: 20px;" >
                </td>
             </tr>
            <tr>
                <td style="text-align: right">
                    <asp:Label ID="lblCveCat" runat="server" Text="Núm. de Licencia: " Font-Bold="True"></asp:Label>
                </td>
                <td style="text-align: left; width: 114px;">                    
                    <ig:WebMaskEditor ID="txtCveCta" runat="server" InputMask="LA-999999" Width="120px" BackColor="White" Font-Names="Arial Narrow" Font-Size="Small">
                    </ig:WebMaskEditor>
                </td>
                <td style="text-align: left">
                    &nbsp;</td>
                <td style="text-align: left">                   
                </td>
                <td style="text-align: left">
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
                    &nbsp; </td>
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
    <div id="datosContribuyente" class="datos">
        <span style="color: #333300; font-family: Tahoma; padding-left:185px"><strong>DATOS DEL RESPONSABLE</strong></span>
        <table class="tabladatos">
             <tr>
                  <td style="width: 100px; height: 26px; text-align: right;">
                            <asp:Label ID="Label3" runat="server" Text="Nombre:"></asp:Label>
                  </td>
                  <td style="height: 26px; text-align: left;" colspan="3">
                        <asp:TextBox ID="TxtNombre" runat="server" Width="430px" ReadOnly="True" 
                            BorderStyle="None"></asp:TextBox>
                  </td>
             </tr>
             <tr>
                  <td style="width: 100px; height: 26px; text-align: right;">
                       <asp:Label ID="Label1" runat="server" Text="Propietario:"></asp:Label>
                  </td>
                  <td style="height: 26px; text-align: left">
                        <asp:TextBox ID="TxtPropietario" runat="server" Width="430px" ReadOnly="True" 
                            BorderStyle="None"></asp:TextBox>
                   </td>
             </tr>
             <tr>
                   <td style="width: 100px; height: 26px; text-align: right;">
                        <asp:Label ID="Label2" runat="server" Text="  Ubicación:"></asp:Label>
                        <br />
                   </td>
                   <td style="height: 26px; text-align: left;">
                        <asp:TextBox ID="TxtUbicacion" runat="server" Width="430px" ReadOnly="True" 
                            BorderStyle="None"></asp:TextBox>
                   </td>
             </tr>
             <tr>
                   <td style="height: 26px; text-align: right;" colspan="2">
                        <table style="width: 100%">
                            <tr>
                                <td style="width: 100px">
                                     Año Pago:</td>
                                <td style="text-align: left; width: 45px;">
                                    <asp:TextBox ID="TxtAño" runat="server" Width="53px" ReadOnly="True" 
                                        BorderStyle="None"></asp:TextBox>
                                </td>
                                <td style="width: 100px">
                                    Mes Pago:</td>
                                <td style="text-align: left">
                                    <asp:TextBox ID="TxtMes" runat="server" Width="41px" 
                                    ReadOnly="True" BorderStyle="None"></asp:TextBox>
                                </td>
                                <td style="width: 132px">
                                    Núm. Máquinas</td>
                                <td style="text-align: left">
                                    <asp:TextBox ID="TxtNumMaquinas" runat="server" BorderStyle="None" 
                                        ReadOnly="True" style="text-align: left" Width="41px"></asp:TextBox>
                                </td>
                            </tr>
                         </table>
                   </td>
             </tr>
             </table>
    </div>
    </asp:Panel>
        
    <asp:Panel ID="DatLiq" runat="server" Visible="false">
    <div id="datosLiquidacion" class="datosliq">
        <table cellpadding="0" cellspacing="0" class="Tablaliquida">
            <tr>
               <td class="EncabezadoLiquida">
                    DETALLES DEL ADEUDO CORRESPONDIENTE
               </td>
            </tr>
            <tr>
               <td>
                 <asp:Panel ID="Panel1" runat="server" ScrollBars="Vertical" 
                        Width="650px" style="text-align: center">
                    <asp:GridView ID="grdresults" runat="server" AllowSorting="True"
                        CellPadding="3" Width="633px" 
                         Font-Names="Arial Narrow" Font-Size="X-Small" BackColor="#DEBA84" BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" CellSpacing="2">
                        <EmptyDataRowStyle CssClass="labelerror" />
                        <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
                        <RowStyle CssClass="gridRows" BackColor="#FFF7E7" ForeColor="#8C4510" />
                        <PagerStyle BorderColor="Silver" BorderStyle="Solid" 
                            ForeColor="#8C4510" HorizontalAlign="Center" />
                        <SelectedRowStyle BackColor="#738A9C" ForeColor="White" Font-Bold="True" />
                        <HeaderStyle BackColor="#A55129" Font-Bold="True" Font-Size="XX-Small" 
                            ForeColor="White" />
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
             <tr>
                <td style=" padding-right :18px">
                    <table style="width: 100%">
                        <tr>
                            <td style="width: 408px">
                                &nbsp;</td>
                            <td>
                                <hr style="border-top: firebrick thin solid; width:215px; text-align:right;" />
                            </td>
                        </tr>
                    </table>
                </td>
             </tr>
             <tr>
                <td>
                
                    <table>
                        <tr>
                            <td style=" width:286px; text-align:right">
                                <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/App_Forms/ImpRpts.aspx?Nombre=Reportes/rpliquidaVideoJuegos.rdlc" 
                                    ToolTip="Impresión de la liquidación generada" Target="_blank">
                                <asp:Image runat="server" ID="Image1" ImageUrl="~/imgs/img_imprimir.jpg" AlternateText="Imagen del Municipio " />
                                </asp:HyperLink>
                             </td>
                            <td align="right" valign="middle" style="width:340px; font-size:medium">
                                Total:<input style="border-style: none; font-size:medium; text-align:right; width: 150px;" 
                                id="Text2" type="text" readonly="readOnly"  value="<%=Session("suma")%>" />&nbsp;
                            </td>
                            <td>

                            </td>
                        </tr>
                    </table>
                
                </td>
             </tr>
        </table>
    </div>
    </asp:Panel>    
    
    <asp:Literal ID="litalert" runat="server"></asp:Literal>
    
</asp:Content>
