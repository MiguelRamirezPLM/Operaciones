<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Index.aspx.cs" Inherits="Index" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>DEIA</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table>
            <thead>
                <tr>
                    <td>
                        <asp:Label ID="lblTitle" runat="server" Font-Names="Verdana" Font-Bold="true" Font-Size="18px">DICCIONARIO DE ESPECIALIDADES PARA LA INDUSTRIA ALIMENTARIA</asp:Label>
                    </td>
                </tr>
            </thead>
            <tr>
                <td colspan="4">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="btnIndex" runat="server" Text="Generar Índices" OnClick="btnIndex_Click" />
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="btnSections" runat="server" Text="Generar Secciones" OnClick="btnSections_Click" />
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="btnFab" runat="server" Text="Generar Guía de Fabricantes" OnClick="btnFab_Click" />
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="btnSuc" runat="server" Text="Generar Sucursales" OnClick="btnSuc_Click" />
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
