<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Indice-General.aspx.cs" Inherits="Indice_General" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <h1>Indice General</h1>
    <asp:Repeater ID="rptAlphabet" runat="server" 
            onitemdatabound="rptAlphabet_ItemDataBound">
    <ItemTemplate>
        <asp:Label ID="letter" runat="server"></asp:Label><br />
        <asp:Repeater ID="rptProducts" runat="server" onitemdatabound="rptProducts_ItemDataBound">
        <ItemTemplate>
            <asp:Label ID="lblProd" runat="server"><%# DataBinder.Eval(Container.DataItem, "Brand") %></asp:Label>
            <asp:Label ID="lblSubs" runat="server"><%# DataBinder.Eval(Container.DataItem, "Substances") %></asp:Label>
            <asp:Label ID="lblDesc" runat="server"><%# DataBinder.Eval(Container.DataItem, "Description") %></asp:Label>
            <asp:Label ID="lblPharma" runat="server"><%# DataBinder.Eval(Container.DataItem, "PharmaForm") %></asp:Label>
            <asp:Label ID="lblDiv" runat="server"><%# DataBinder.Eval(Container.DataItem, "Division") %></asp:Label>
            <asp:Label ID="lblPage" runat="server"><%# DataBinder.Eval(Container.DataItem, "Pages") %></asp:Label><br />
        </ItemTemplate>
        </asp:Repeater>
    </ItemTemplate>
    </asp:Repeater>
    </div>
    </form>
</body>
</html>
