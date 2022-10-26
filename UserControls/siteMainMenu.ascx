<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="siteMainMenu.ascx.vb" Inherits="WebTCMAdmin.siteMainMenu" %>
<%@ Register Assembly="Infragistics4.Web.v19.2, Version=19.2.20192.8, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI.NavigationControls" tagprefix="ig2" %>

<table  style="width:100%;height:100%;border:10px;">
    <tr>                
        <td>                                                                                                            
            <ig2:WebExplorerBar ID="mnuMainExploreBar" runat="server" Width="100%" Height="100%" GroupContentsHeight="" EnableExpandButtons="False" GroupExpandBehavior="AllExpanded">
                <Groups>
                    <ig2:ExplorerBarGroup GroupContentsHeight="" Text="Group" CssClass="Padre">
                        <Items>
                            <ig2:ExplorerBarItem Text="Item" CssClass="Hijo">
                                <Items>
                                    <ig2:ExplorerBarItem Text="Item">
                                    </ig2:ExplorerBarItem>
                                </Items>
                            </ig2:ExplorerBarItem>
                            <ig2:ExplorerBarItem Text="Item">
                            </ig2:ExplorerBarItem>
                            <ig2:ExplorerBarItem Text="Item">
                            </ig2:ExplorerBarItem>
                        </Items>
                    </ig2:ExplorerBarGroup>
                    <ig2:ExplorerBarGroup GroupContentsHeight="" Text="Group">
                        <Items>
                            <ig2:ExplorerBarItem Text="Item">
                            </ig2:ExplorerBarItem>
                            <ig2:ExplorerBarItem Text="Item">
                            </ig2:ExplorerBarItem>
                        </Items>
                    </ig2:ExplorerBarGroup>
                    <ig2:ExplorerBarGroup GroupContentsHeight="" Text="Group">
                        <Items>
                            <ig2:ExplorerBarItem Text="Item">
                            </ig2:ExplorerBarItem>
                            <ig2:ExplorerBarItem Text="Item">
                            </ig2:ExplorerBarItem>
                        </Items>
                    </ig2:ExplorerBarGroup>
                </Groups>
            </ig2:WebExplorerBar>
            <br />
        </td>                      
    </tr>
</table>