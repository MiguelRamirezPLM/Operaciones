<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Indice-Laboratorios.aspx.cs" Inherits="Indice_Laboratorios" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <h1>Índice de Laboratorios</h1>
    <asp:Repeater ID="rptLabs" runat="server" onitemdatabound="rptLabs_ItemDataBound">
    <ItemTemplate>
        <asp:Label ID="lblLab" runat="server"><%# DataBinder.Eval(Container.DataItem,"Description") %></asp:Label>
        <asp:Repeater ID="rptProducts" runat="server" onitemdatabound="rptProducts_ItemDataBound">
        <ItemTemplate>
            <asp:Label ID="lblBrand" runat="server"><%# DataBinder.Eval(Container.DataItem,"Brand") %></asp:Label>
            <asp:Label ID="lblPharma" runat="server"><%# DataBinder.Eval(Container.DataItem,"PharmaForm") %></asp:Label>
            <asp:Label ID="lblPage" runat="server"><%# DataBinder.Eval(Container.DataItem,"Pages") %></asp:Label>
        </ItemTemplate>
        </asp:Repeater>
    </ItemTemplate>
    </asp:Repeater>
    </div>
    </form>
</body>
</html>
