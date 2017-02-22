<%@ Page Language="C#" AutoEventWireup="true" CodeFile="IndiceImag.aspx.cs" Inherits="IndiceImag" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
<link href='../../estilos.css' rel='stylesheet' type='text/css' />
    <title>ÍNDICE GENERAL IMAGENOLOGÍA - RADIODIAGNÓSTICO</title>
</head>
<body>
    <form id="form1" runat="server">
    <p align='center' class='titulo'>ÍNDICE GENERAL IMAGENOLOGÍA - RADIODIAGNÓSTICO</p>
    <%--<div style='overflow:auto; width:728px; height:540px;'>--%>
    <div style='overflow:auto;'>
        <asp:Repeater ID="rptImag" runat="server" OnItemDataBound="rptImag_ItemDataBound"  >
        <ItemTemplate>
            <p class='subsection'><%# DataBinder.Eval(Container.DataItem, "Description").ToString().ToUpper() %></p>
            <ul>
                <asp:Repeater ID="rptProd" runat="server">
                    <ItemTemplate>
                        <%--<p class="producto"><a class='links' href='..\..\prods\<%# DataBinder.Eval(Container.DataItem, "HtmlFile") %>' style='text-decoration:none'> <%# DataBinder.Eval(Container.DataItem, "ProductName") %></a></b>. <%# DataBinder.Eval(Container.DataItem, "CompanyShortName") %></p>--%>
                        <%#getLink(DataBinder.Eval(Container.DataItem, "HtmlFile").ToString(), DataBinder.Eval(Container.DataItem, "ProductName").ToString(), DataBinder.Eval(Container.DataItem, "CompanyShortName").ToString())%>
                    </ItemTemplate>
                </asp:Repeater>
            </ul><br />
        </ItemTemplate>
        </asp:Repeater>
    </div>
    </form>
</body>
</html>
