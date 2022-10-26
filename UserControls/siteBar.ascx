<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="siteBar.ascx.vb" Inherits="WebTCMAdmin.siteBar" %>
<%@ Register Assembly="Infragistics4.Web.v19.2, Version=19.2.20192.8, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI.LayoutControls" tagprefix="ig" %>
<%@ Register Assembly="Infragistics4.Web.v19.2, Version=19.2.20192.8, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI.LayoutControls" tagprefix="ig2" %>
<link href="../Styles/BarStyle.css" rel="stylesheet" />
<table style="width:100%;">    
    <tr><td colspan="2" style="padding:0px; border-spacing:0px;"> &nbsp;</td></tr>
    <tr>        
        <td style="width:70%;background-color:WhiteSmoke;padding:5px">                 
                <asp:HyperLink ID="lblOficina" runat="server" Font-Bold="True" Font-Size="Medium" ForeColor="Black" Height="100%" NavigateUrl="~/Default.aspx">[lblOficina]</asp:HyperLink>                
        </td>
        <td  style="width:20%;background-color:WhiteSmoke;padding:5px" >                
                <asp:Label ID="lblUser" runat="server"  BorderStyle="None" Font-Bold="True" Font-Size="Medium" ForeColor="#003366" Text=""></asp:Label>
        </td>            
        <td  style="width:10%;background-color:WhiteSmoke;padding:5px" >                                
                <asp:LinkButton ID="btnAyuda" runat="server"  CssClass="btn btn-small btn-primary"><i class="glyphicon glyphicon-question-sign"></i>&nbsp;Ayuda</asp:LinkButton>
        </td>            
    </tr>    
    <tr><td colspan="2" style="padding:0px; border-spacing:0px;"> &nbsp;</td></tr>
</table>
 <ig2:WebDialogWindow ID="dlgAyuda" runat="server" InitialLocation="Centered" Modal="True" CssClass="AyudaPnl" Visible="False">
    <contentpane>
    <template>                                                    
                <%=Session("Ayuda")%>                                                                                                             
    </template> 
    </contentpane> 
<header captiontext="GUIA DE USO..." CssClass="AyudaHeader" Font-Bold="True" Font-Size="Large">
</header>
<resizer enabled="True" />
</ig2:WebDialogWindow>
                            