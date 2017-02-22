<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Terceras.aspx.cs" Inherits="Terceras" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Terceras</title>
    <link href='../Estilos.css' rel='stylesheet' type='text/css' />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Label ID="lblTercera" runat="server"></asp:Label>
        <br />
        <br />
        <p class="Subtitulo">Productos:</p>
        <asp:Repeater runat="server" ID="rptAgro" OnItemDataBound="rptAgro_ItemDataBound">
        <ItemTemplate>
           
        </ItemTemplate>
        </asp:Repeater>
        <br />
        <p class="Subtitulo">Orgánicos:</p>
        <asp:Repeater runat="server" ID="rptOrg" OnItemDataBound="rptOrg_ItemDataBound">
        <ItemTemplate>
        
        </ItemTemplate>
        </asp:Repeater>
    </div>
    </form>
</body>
</html>
