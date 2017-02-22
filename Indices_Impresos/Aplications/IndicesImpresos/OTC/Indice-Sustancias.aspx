<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Indice-Sustancias.aspx.cs" Inherits="Indice_Sustancias" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <h1>Índice de Sustancias</h1>
    <asp:Repeater ID="rptSubstances" runat="server" 
            onitemdatabound="rptSubstances_ItemDataBound">
    <ItemTemplate>
        <asp:Label ID="lblSubstance" runat="server"><%# DataBinder.Eval(Container.DataItem,"Substance") %></asp:Label><br />
        <h3>Solos</h3>
        <asp:Repeater ID="rptAloneProducts" runat="server" onitemdatabound="rptAloneProducts_ItemDataBound">
        <ItemTemplate>
            <asp:Label ID="lblProduct" runat="server"><%# DataBinder.Eval(Container.DataItem,"Brand") %></asp:Label>
            <asp:Label ID="lblPharmaForm" runat="server"><%# DataBinder.Eval(Container.DataItem,"PharmaForm") %></asp:Label>
            <asp:Label ID="lblLab" runat="server"><%# DataBinder.Eval(Container.DataItem,"Division") %></asp:Label>
        </ItemTemplate>
        </asp:Repeater>
        <h3>Combinados</h3>
        <asp:Repeater ID="rptCombinedProducts" runat="server" onitemdatabound="rptCombinedProducts_ItemDataBound">
        <ItemTemplate>
            <asp:Label ID="lblProduct" runat="server"><%# DataBinder.Eval(Container.DataItem,"Brand") %></asp:Label>
            <asp:Label ID="lblPharmaForm" runat="server"><%# DataBinder.Eval(Container.DataItem,"PharmaForm") %></asp:Label>
            <asp:Label ID="lblLab" runat="server"><%# DataBinder.Eval(Container.DataItem,"Division") %></asp:Label>
        </ItemTemplate>
        </asp:Repeater>
    </ItemTemplate>
    </asp:Repeater>
    </div>
    </form>
</body>
</html>
