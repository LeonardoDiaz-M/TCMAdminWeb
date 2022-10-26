<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master"  CodeBehind="Login.aspx.vb" Inherits="WebTCMAdmin.Login1" AspCompat="true"  %>


<%@ Register Assembly="Infragistics4.Web.v19.2, Version=19.2.20192.8, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI.LayoutControls" tagprefix="ig" %>
    
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">    
    <table style="width:100%;height:400px;border:0px;vertical-align:top;">
        <tr>
            <td>
                <asp:Login ID="usrLogin" runat="server" 
                       DisplayRememberMe="False" FailureText="Usuario o Contraseña Incorrectos" Height="122px" LoginButtonText=" Ingresar" 
                       PasswordLabelText="Contraseña: " PasswordRequiredErrorMessage="Contraseña requerida" RememberMeText="" 
                       UserNameLabelText="Usuario: " 
        UserNameRequiredErrorMessage="El usuario es requerido" Width="393px" LoginButtonType="Link" TitleText="Ingreso al Sistema                " BackColor="#FFFBD6" BorderColor="#FFDFAD" BorderPadding="4" BorderStyle="Solid" BorderWidth="1px" Font-Names="Verdana" Font-Size="0.8em" ForeColor="#333333" TextLayout="TextOnTop" >
                    <InstructionTextStyle Font-Italic="True" ForeColor="Black" />
    <LabelStyle HorizontalAlign="Left" VerticalAlign="Top"  />
    <LoginButtonStyle Height="30px" Width="110px" BackColor="White" BorderStyle="Solid" BorderWidth="1px" CssClass="glyphicon glyphicon-log-in" Font-Bold="True" Font-Size="0.8em" ForeColor="#990000" BorderColor="#CC9966" Font-Names="Verdana"  />
                    <TextBoxStyle Font-Size="0.8em" />
                    <TitleTextStyle Font-Bold="True" Font-Size="0.9em" ForeColor="White" HorizontalAlign="Left" VerticalAlign="Middle" BackColor="#990000" />
</asp:Login>
            </td>
        </tr>
        <tr>
            <td>
                <ig:WebDialogWindow ID="winPCName" runat="server" Height="300px" Width="400px" InitialLocation="Centered" Modal="True" Visible="false"  >
                      <contentpane>
                                        <template>
                                        <table cellpadding="3" cellspacing="3" style="width:100%;">
                                            <tr>
                                                <td style="height: 109px">
                                                    <h4>PROPORCIONE EL NOMBRE DEL EQUIPO&nbsp;</h4>
                                                    <h4>
                                                        <asp:TextBox ID="txtPcName" runat="server" BorderStyle="Inset" Font-Bold="True" Font-Size="Small"  Width="100%" BackColor="#CCCCCC"></asp:TextBox>
                                                    </h4>
                                                </td>
                                            </tr>                                          
                                            <tr>
                                                <td style="align-items:center; height: 23px;">
                                                    <asp:Label ID="lblErrorModal" runat="server" Font-Bold="True" Font-Size="Small" ForeColor="Red" Visible="False" Width="100%"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="align-items:center">
                                                    <asp:Button ID="btnRegistrar" runat="server" BackColor="#666666" Font-Bold="True" ForeColor="White" Height="100%" Text="REGISTRAR EQUIPO" Width="100%" />
                                                </td>
                                            </tr>
                                        </table>
                                        </template>
                                    </contentpane>
                                    <header captiontext="REGISTRO DE EQUIPO" ID="hdrPsw">
                                        <CloseBox Visible="False" />
                                    </header>
                </ig:WebDialogWindow>
            </td>
        </tr>
    </table>        
        </asp:Content>
