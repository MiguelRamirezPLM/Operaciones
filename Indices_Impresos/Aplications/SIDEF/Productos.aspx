<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Productos.aspx.cs" Inherits="Productos" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Indice de Productos</title>
    <link href="..\Estilos.css' rel='stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table align="center" cellSpacing="0" cellPadding="7" width="95%" border="0">
	    <tr><td><table width="100%">
		    <tr><td align="center" class="Titulo">&Iacute;NDICE GENERAL DE PRODUCTOS AGROQU&Iacute;MICOS POR NOMBRE COMERCIAL</td></tr>
		    <tr><td align="center">  <img src="letras/A1.jpg" /></td></tr>
	    </table>
	    </td>
	    </tr>
	    </table>
     <asp:Repeater ID="rptProds" runat="server">
        <ItemTemplate>
            <div class="Links" align="left"><a style="text-decoration:none" href='<%# DataBinder.Eval(Container.DataItem,"File","prods/{0}") %>' ><b><%# DataBinder.Eval(Container.DataItem,"Nombre") %>.</b> <%# DataBinder.Eval(Container.DataItem,"Registro") %>. <%# DataBinder.Eval(Container.DataItem,"Usos") %>. <i><%# DataBinder.Eval(Container.DataItem,"FFarmaceutica") %></i>. <b><%# DataBinder.Eval(Container.DataItem,"Laboratorio") %>.</b></a></div><br>
        </ItemTemplate>
     </asp:Repeater>
    </div>
    </form>
</body>
</html>