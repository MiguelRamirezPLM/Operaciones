<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Indice-Sintomas.aspx.cs" Inherits="Indice_Sintomas" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <h1>Índice de Síntomas</h1>
    <asp:Repeater ID="rptSymtoms" runat="server" onitemdatabound="rptSymtoms_ItemDataBound">
    <ItemTemplate>
        <asp:Label ID="lblSymptom" runat="server"><%# DataBinder.Eval(Container.DataItem, "Symptom") %></asp:Label>
        <asp:Label ID="lblSPage" runat="server"><%# DataBinder.Eval(Container.DataItem, "Page") %></asp:Label><br />
        <asp:Repeater ID="rptProducts" runat="server" onitemdatabound="rptProducts_ItemDataBound">
        <ItemTemplate>
            <asp:Label ID="lblBrand" runat="server"><%# DataBinder.Eval(Container.DataItem, "Brand") %></asp:Label>
            <asp:Label ID="lblPPage" runat="server"><%# DataBinder.Eval(Container.DataItem, "Pages") %></asp:Label><br />
        </ItemTemplate>
        </asp:Repeater>
    </ItemTemplate>
    </asp:Repeater>
    </div>
    </form>
</body>
</html>
