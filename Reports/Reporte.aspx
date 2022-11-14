<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Reporte.aspx.vb" Inherits="WebTCMAdmin.Reporte" %>
<%@ Register assembly="CrystalDecisions.Web, Version=13.0.4000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>    

    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:Literal ID="litalert" runat="server"></asp:Literal>
     <rsweb:ReportViewer ID="ReportViewer1" runat="server" Height="900px" Width="80%"></rsweb:ReportViewer>    
    <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" 
                                AutoDataBind="True" Height="900px"  Width="600px" 
                                EnableDatabaseLogonPrompt="False" EnableDrillDown="False" 
                                HasCrystalLogo="False" HasDrilldownTabs="False" 
                                HasDrillUpButton="False" HasToggleGroupTreeButton="False" ToolPanelView="None" />
    
        
        
    </form>

</body>
</html>
