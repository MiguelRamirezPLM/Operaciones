<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Organicos.aspx.cs" Inherits="Organicos" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Página sin título</title>
    <link href="Estilos.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
           <p class="Titulo" align="center">Productos Orgánicos</p>
           <br />
           <br />
        <table width="100%" align="center" border="0" cellspacing="0" cellpadding="7">  
        <asp:Repeater ID="rptProds" runat="server">
        <ItemTemplate>
            <tr><td><p class="Links"><a href='<%# DataBinder.Eval(Container.DataItem,"File","prods/{0}") %>' target="adentro"><b><%# DataBinder.Eval(Container.DataItem,"Nombre") %>.</b> <i><%# DataBinder.Eval(Container.DataItem,"FFarmaceutica") %>.</i> <b><%# DataBinder.Eval(Container.DataItem,"Laboratorio") %></b></a></p></td></tr>
        </ItemTemplate>
        </asp:Repeater>
        </table>   
    </div>
    </form>
</body>
</html>