<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="siteHeader.ascx.vb" Inherits="WebTCMAdmin.siteHeader" %>
   <table style="width:100%;height:100%;border:0px;grid-row-span:0;" cellspacing="0" cellpadding="0" >
              <tr style="height: 80px;">
                  <td bgcolor="#6C281B" style="width:5%">
                       <div style="position: relative;left: 16px;">
                           <img alt="" src="<%=Session("HdrImg1")%>" Height="70px" ImageAlign="Left" Width="200px" />                                                                     
                        </div>              
                  </td>
                  <td bgcolor="#B07C23" style="width:20%"></td>
                  <td bgcolor="#921764" style="width:20%"></td>
                  <td bgcolor="#828D5B" style="width:20%"></td>
                  <td bgcolor="#577A9B" style="width:25%"></td>
                  <td bgcolor="#6C281B" style="width:10%">
                       <div style="position: relative;right: 3px;">        
                           <img alt="" src="<%=Session("HdrImg2")%>"   Height="50px" ImageAlign="Left" Width="200px" />                                                                  
                        </div>      
                   </td>   
              </tr>
    </table>