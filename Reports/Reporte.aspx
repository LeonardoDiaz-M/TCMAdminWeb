<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Reporte.aspx.vb" Inherits="WebTCMAdmin.Reporte" %>
<%@ Register assembly="CrystalDecisions.Web, Version=13.0.4000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <webopt:bundlereference runat="server" path="~/Content/css" />        
   <%-- <link href="Content/bootstrap-theme.css" rel="stylesheet" />   --%> 
     <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.1/css/bootstrap.min.css"/>
</head>
<body>    
    <form id="form1" runat="server">      
                        <asp:Panel ID="pnlCrystal" runat="server">
                         <table style="width:20%;">
                            <tr>
                                <td>&nbsp;&nbsp; &nbsp;</td>
                                <td class="btn btn-default glyphicon glyphicon-download-alt"> &nbsp;
                                    <asp:Button ID="btnPDF" runat="server" Text="Exportar" BackColor="Transparent" BorderStyle="None" ForeColor="Black" Font-Bold="True" />
                                </td>
                                 <td>&nbsp;&nbsp; &nbsp;</td>
                                <td class="btn btn-primary glyphicon glyphicon-print">
                                    <asp:Button ID="btnPrint" runat="server" Text="Imprimir" BackColor="Transparent" BorderStyle="None" ForeColor="White" Font-Bold="True"    />
                                </td>
                                 
                            </tr>                           
                        </table>
                        <hr/>                   
                            <table>
                                <tr>
                                    <td>
                                        <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="True" EnableDatabaseLogonPrompt="False" EnableDrillDown="False" EnableParameterPrompt="False" EnableTheming="False" EnableToolTips="False" GroupTreeStyle-ShowLines="False" HasCrystalLogo="False" HasDrilldownTabs="False" HasDrillUpButton="False" HasExportButton="False" HasPrintButton="False" HasSearchButton="False" HasToggleGroupTreeButton="False" Height="900px" ToolPanelView="None" Width="600px" />                                                                              
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>                            
                        <table>
                                <tr>
                                    <td>
                                       <rsweb:ReportViewer ID="ReportViewer1" runat="server" Height="900px" Width="80%"></rsweb:ReportViewer>  
                                    </td>
                                </tr>
                            </table>
                
                <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                <asp:Literal ID="litalert" runat="server"></asp:Literal>     
    </form>
</body>
</html>
