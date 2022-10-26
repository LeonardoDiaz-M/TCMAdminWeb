<%@ Page UICulture="auto" Culture="auto" Language="VB" AutoEventWireup="false"  CodeFile="ImpRpts.aspx.vb" Inherits="App_Forms_ImpRpts" %>
<%@ Register Assembly="Infragistics4.WebUI.WebResizingExtender.v19.2, Version=19.2.20192.8, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.WebUI" tagprefix="igui" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=15.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Impresión de formatos de pago</title>
    <style type="Estilos/css">
        #form1
        {
            height: 507px;
            width: 696px;
        }
        .style1
        {
            width: 100%;
        }
        .style2
        {
            width: 354px;
            background-color: #666666;
        }
        .style3
        {
            background-color: #666666;
        }
        .auto-style1 {
            width: 844px;
            height: 568px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server" class="auto-style2">
  
   
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:base_mpioConnectionString %>" SelectCommand="	SELECT        tab_liquidaciones.nombre, tab_liquidaciones.direccion, tab_liquidaciones.rfc, tab_liquidaciones.cve_cat_lic, tab_liquidaciones.lincap1, tab_liquidaciones.lincap2, tab_liquidaciones.lincap3, 
                         tab_liquidaciones.observacion, tab_liquidaciones.cant_letra, tab_liquidaciones.fecha, tab_liquidaciones.img_path, tab_det_liquidaciones.año, tab_det_liquidaciones.Per_ini, tab_det_liquidaciones.Per_fin, 
                         tab_det_liquidaciones.basegrav, tab_det_liquidaciones.impuesto, tab_det_liquidaciones.act_impuesto, tab_det_liquidaciones.recargos, tab_det_liquidaciones.multas, tab_det_liquidaciones.gastos, 
                         tab_det_liquidaciones.sub_imp, tab_det_liquidaciones.sub_rec, tab_det_liquidaciones.sub_multa, tab_det_liquidaciones.sub_gastos, tab_det_liquidaciones.total, tab_det_liquidaciones.clave, 
                         datos_mpio.nombre AS NomMpio, datos_mpio.nom_mpio, datos_mpio.direccion AS DirMpio, datos_mpio.nom_area_ing, datos_mpio.nom_presi, datos_mpio.nom_teso, tab_liquidaciones.fechavenc, 
                         tab_det_liquidaciones.c_iva, tab_det_liquidaciones.saneamiento
FROM            tab_liquidaciones INNER JOIN
                         tab_det_liquidaciones ON tab_liquidaciones.id_liq = tab_det_liquidaciones.id_liq INNER JOIN
                         datos_mpio ON tab_liquidaciones.num_mun = datos_mpio.num_mun
WHERE        (tab_det_liquidaciones.id_liq = @IdLiq)">
            <SelectParameters>
                <asp:SessionParameter DefaultValue="1" Name="IdLiq" SessionField="NumLiq" Type="Int32" />
            </SelectParameters>
        </asp:SqlDataSource>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
  
        <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="1375px" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt">
            <LocalReport ReportPath="Reportes\rpliquidaPredial.rdlc">
                <DataSources>
                    <rsweb:ReportDataSource DataSourceId="SqlDataSource1" Name="DataSet1" />
                </DataSources>
            </LocalReport>
        </rsweb:ReportViewer>
  
   
    </form>
   </body>
</html>
