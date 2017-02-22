<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Listado.aspx.cs" Inherits="Listado" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Productos Organicos</title>
    <link href='Estilos.css' rel='stylesheet' type='text/css' />
</head>
<body>
    <form id="form1" runat="server">
    <div>
           <p class="Titulo" align="center">Productos Orgánicos</p>
           <br />
           <br />
        <asp:Repeater ID="rptProds" runat="server">
        <ItemTemplate>
            <p class="Links"><a href='<%# DataBinder.Eval(Container.DataItem,"File","prods/{0}") %>' target="adentro"><b><%# DataBinder.Eval(Container.DataItem,"Nombre") %>.</b> <i><%# DataBinder.Eval(Container.DataItem,"FFarmaceutica") %>.</i> <b><%# DataBinder.Eval(Container.DataItem,"Laboratorio") %></b></a></p>
        </ItemTemplate>
        </asp:Repeater>
    </div>
    </form>
</body>
</html>