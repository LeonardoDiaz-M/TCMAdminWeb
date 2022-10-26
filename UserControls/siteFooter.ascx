<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="siteFooter.ascx.vb" Inherits="WebTCMAdmin.siteFooter" %>
<link href="../Styles/MainSite.css" rel="stylesheet" />
<table  style="width:100%;height:70%;border:0px;grid-row-span:0;" cellspacing="0" cellpadding="0">
                    <tr>
                   <td>
                  <td bgcolor="gray"  rowspan="2" >
                      <img alt="" src="<%=Session("Img1")%>" Height="70px" ImageAlign="Left" Width="200px"/>                       
                  </td>
                  <td bgcolor="Gray" style="width:3%;height:100%;"> <br /></td>
                  <td bgcolor="gray" colspan="2" style="width:20%" >                        
                           <table  style="width:100%;height:100%;border:0px;grid-row-span:0;" cellspacing="0" cellpadding="0">
                               <tr><td><%=Session("Texto")(0) %></td></tr>
                               <tr><td><%=Session("Texto")(1) %></td></tr>
                               <tr><td><%=Session("Texto")(2) %></td></tr>
                               <tr><td><%=Session("Texto")(3) %></td></tr>                               
                          </table>                          
                        </td>
                  <td bgcolor="Gray" colspan="2">                      
                          <table  style="width:100%;height:100%;border:0px;grid-row-span:0;" cellspacing="0" cellpadding="0">                              
                               <tr><td><%=Session("Texto")(7) %></td></tr>
                               <tr><td><%=Session("Texto")(8) %></td></tr>
                               <tr><td><%=Session("Texto")(9) %></td></tr>
                               <tr><td><%=Session("Texto")(10)%></td></tr>                               
                          </table>                        
                  </td>                  
                  <td bgcolor="gray" style="width:10%">
                      <%--<asp:Image ID="Image5" runat="server" ImageUrl="~/imgs/marcalogo.png"  Height="50px" ImageAlign="Top" Width="180px" />   --%>
                      <img alt="" src="<%=Session("Img2")%>" Height="70px" ImageAlign="Top" Width="200px"/>                           
                  </td>
                  <td bgcolor="Gray" style="width:3%;height:100%;" rowspan="2"> <br /></td>
                  <td bgcolor="gray" rowspan="2" style="width:30%">                                                         
                      <asp:Label ID="Label8" runat="server" Text="Síguenos en:" Width="100%" Font-Names="AvenirNext LT Pro Bold" ForeColor="White" Font-Size="Large"></asp:Label>
                          <table>
                              <tr>
                                  <td>
                                      <img alt="" src="<%=Session("face")%>" Height="40px" ImageAlign="Left" Width="40px"/>                                       
                                  </td>
                                  <td>
                                      <img alt="" src="<%=Session("twit")%>" Height="40px" ImageAlign="Left" Width="40px"/>                                       
                                  </td>
                              </tr>
                          </table>
                      </td>  
                       </tr>
                    <tr>
                   <td>
                  <td bgcolor="#6C281B" style="width:10%">&nbsp;</td>
                  <td bgcolor="#B07C23" style="width:10%">&nbsp;</td>
                  <td bgcolor="#921764" style="width:15%">&nbsp;</td>
                  <td bgcolor="#828D5B" style="width:15%">&nbsp;</td>
                  <td bgcolor="#577A9B" style="width:15%">&nbsp;</td>
                  <td bgcolor="#6C281B" style="width:10%">&nbsp;</td>
            </tr>
            </table> 

 