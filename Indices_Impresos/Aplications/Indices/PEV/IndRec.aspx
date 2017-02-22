<%@ Page Language="C#" AutoEventWireup="true" CodeFile="IndRec.aspx.cs" Inherits="PEV_IndRec" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Productos nuevos</title>
</head>
<body>
<h1>Productos nuevos</h1>
    <form id="form1" runat="server">
    <div>
        <asp:Repeater runat="server" ID="rptProd" OnItemDataBound="rptProd_OnItemDataBound">
        <ItemTemplate>
        
            <table>
                <tr>
                <asp:Label runat="server" ID="lblProd" CssClass="label"><%# DataBinder.Eval(Container.DataItem, "PRODUCTNAME")%>.</asp:Label>
                <asp:Label runat="server" ID="lblpharmaform" CssClass="label"><%# DataBinder.Eval(Container.DataItem, "PHARMAFORM")%>.</asp:Label>
                <asp:Label runat="server" ID="lblsubs" CssClass="label"><%#DataBinder.Eval(Container.DataItem, "PRODUCTSUBSTANCES")%>.</asp:Label>
                <asp:Label runat="server" ID="lbldivision" CssClass="label"><%# DataBinder.Eval(Container.DataItem, "DIVISION")%>.</asp:Label>
                <asp:Label runat="server" ID="Labelpage" CssClass="label"><%#DataBinder.Eval(Container.DataItem, "PAGE")%>.</asp:Label> 
                </tr>
            </table>
        </ItemTemplate>
        </asp:Repeater>
    
    </div>
    </form>
</body>
</html>
