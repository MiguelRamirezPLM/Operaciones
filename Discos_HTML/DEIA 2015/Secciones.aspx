<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Secciones.aspx.cs" Inherits="Secciones" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Secciones</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" RenderMode="Block" UpdateMode="Conditional">
        <ContentTemplate>
            <table width="100%" cellpadding="1" cellspacing="1">
                <thead>
                <tr>
                    <td colspan="4">
                        <asp:Label ID="lblSections" runat="server" Font-Names="verdana" Font-Size="18px" >Secciones</asp:Label>
                    </td>
                </tr>
            </thead>
            <tr>
                <td colspan="4" style="height:15px;">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td style="width:300px;">
                    <asp:Label ID="lblEdition" runat="server" Font-Names="Verdana" Font-Size="12px" Width="150px" Font-Bold="False">Edición</asp:Label>
               </td>
               <td>
                   &nbsp;
               </td>
               <td align="center" style="width: 300px">
                    <asp:TextBox ID="txtEdition" runat="server" Width="150px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" Display="Dynamic" 
                    ForeColor="red" ValidationGroup="Sections" ControlToValidate="txtEdition" SetFocusOnError="true" ></asp:RequiredFieldValidator>
                   <%--<cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers" TargetControlID="txtEdition" >
                    </cc1:FilteredTextBoxExtender>--%>
               </td>
               <td rowspan="13" style="width:50%">
                   <asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="0" DynamicLayout="true" >
                    <ProgressTemplate>
                        <div>
                            <img src="img/progress1.gif" alt="" />
                            <asp:Label ID="lblProgress" runat="server" Font-Names="Verdana" Font-Size="14" Font-Bold="true">Generando...</asp:Label>
                        </div>
                    </ProgressTemplate>  
                    </asp:UpdateProgress>
               </td>     
            </tr>
            <tr>
                <td colspan="3" style="height:15px;">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblMP" runat="server"  Font-Names="Verdana" Font-Size="12px" Width="150px" Font-Bold="False">Sección de Materias Primas</asp:Label>
                </td>
                <td>
                    &nbsp;
                </td>
                <td align="center" style="width: 300px">
                    <asp:Button ID="btnMP" runat="server" Text="Generar" ValidationGroup="Sections" OnClick="btnMP_Click" />
                </td>
            </tr>
            <tr>
                <td colspan="3" style="height:15px;">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblMaq" runat="server"  Font-Names="Verdana" Font-Size="12px" Width="150px" Font-Bold="False">Sección de Maquinaria</asp:Label>
                </td>
                <td>
                    &nbsp;
                </td>
                <td align="center" style="width: 300px">
                    <asp:Button ID="btnMaq" runat="server" Text="Generar" ValidationGroup="Sections" OnClick="btnMaq_Click" />
                </td>
            </tr>
            <tr>
                <td colspan="3" style="height:15px;">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblCC" runat="server"  Font-Names="Verdana" Font-Size="12px" Width="150px" Font-Bold="False">Sección de Control de Calidad</asp:Label>
                </td>
                <td>
                    &nbsp;
                </td>
                <td align="center" style="width: 300px">
                    <asp:Button ID="btnCC" runat="server" Text="Generar" ValidationGroup="Sections" OnClick="btnCC_Click" />
                </td>
            </tr>
            <tr>
                <td colspan="3" style="height:15px;">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblEE" runat="server"  Font-Names="Verdana" Font-Size="12px" Width="150px" Font-Bold="False">Sección de Envases y Embalajes</asp:Label>
                </td>
                <td>
                    &nbsp;
                </td>
                <td align="center" style="width: 300px">
                    <asp:Button ID="btnEE" runat="server" Text="Generar" ValidationGroup="Sections" OnClick="btnEE_Click" />
                </td>
            </tr>
            <tr>
                <td colspan="3" style="height:15px;">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblServ" runat="server"  Font-Names="Verdana" Font-Size="12px" Width="150px" Font-Bold="False">Sección de Servicios</asp:Label>
                </td>
                <td>
                    &nbsp;
                </td>
                <td align="center" style="width: 300px">
                    <asp:Button ID="btnServ" runat="server" Text="Generar" ValidationGroup="Sections" OnClick="btnServ_Click" />
                </td>
            </tr>
            <tr>
                <td colspan="3" style="height:15px;">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="3" align="center">
                    <asp:Label ID="lblMessage" runat="server" Font-Names="verdana" Font-Size="14px" ForeColor="red"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="4" style="height:15px;">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="4" style="height:15px;">
                    <asp:Button ID="btnBack" runat="server" Text="Regresar" OnClientClick="javascript:history.back()" />
                </td>
            </tr>    
            </table>
        </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
