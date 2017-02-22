<%@ page language="C#" autoeventwireup="true" inherits="DEF_RecentProducts, App_Web_iw2mfwhu" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Productos Recientes</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <h1>Productos Recientes</h1>
    <asp:Repeater ID="rptProducts" runat="server" OnItemDataBound="rptProducts_ItemDataBound">
    <ItemTemplate>
        <asp:Label ID="lblBrand" runat="server"><%# DataBinder.Eval(Container.DataItem, "Brand") %></asp:Label><br />
        <asp:Label ID="lblPharmaForm" runat="server"><%# DataBinder.Eval(Container.DataItem, "PharmaForm") %></asp:Label><br />
        <asp:Label ID="lblSubstances" runat="server"><%# DataBinder.Eval(Container.DataItem, "Substances") %></asp:Label><br />
        <asp:Label ID="lblDivision" runat="server"><%# DataBinder.Eval(Container.DataItem, "DivisionName") %></asp:Label><br />
        <asp:Label ID="lblPage" runat="server"><%# DataBinder.Eval(Container.DataItem, "Page") %></asp:Label><br />
        <hr />
    </ItemTemplate>
    </asp:Repeater>
       
    </div>
    </form>
</body>
</html>
