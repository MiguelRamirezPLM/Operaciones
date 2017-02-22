<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Indice-Anuncios.aspx.cs" Inherits="Indice_Anuncios" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <h1>Índice de Anuncios</h1>
    <asp:Repeater ID="rptLabs" runat="server" onitemdatabound="rptLabs_ItemDataBound">
    <ItemTemplate>
        <asp:Label ID="lblLabName" runat="server"><%# DataBinder.Eval(Container.DataItem, "Description") %></asp:Label><br />
        <asp:Repeater ID="rptAds" runat="server" onitemdatabound="rptAds_ItemDataBound">
        <ItemTemplate>
            <asp:Label ID="lblAds" runat="server"><%# DataBinder.Eval(Container.DataItem,"AdName") %></asp:Label>
            <asp:Label ID="lblPage" runat="server"><%# DataBinder.Eval(Container.DataItem,"Page") %></asp:Label><br />
        </ItemTemplate>
        </asp:Repeater>
    </ItemTemplate>
    </asp:Repeater>
    </div>
    </form>
</body>
</html>
