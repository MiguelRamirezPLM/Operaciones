<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GuiaEstados.aspx.cs" Inherits="GuiaEstados" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
<link href='../../estilos.css' rel='stylesheet' type='text/css' />
    <title>Guia Nacional de Fabricantes y Distribuidores - Estados</title>
</head>
<body>
    <form id="form1" runat="server">
    <p id="lblTitulo" runat="server" align='center' class="titulo"></p>
    <asp:Repeater ID="rptStates" runat="server">
        <ItemTemplate>
            <p class="estado"><a class='linksEstado' href='<%# DataBinder.Eval(Container.DataItem, "StateId") %>.htm' style='text-decoration:none'><%# DataBinder.Eval(Container.DataItem, "Name") %></a></p>
        </ItemTemplate>
    </asp:Repeater>
    </form>
</body>
</html>
