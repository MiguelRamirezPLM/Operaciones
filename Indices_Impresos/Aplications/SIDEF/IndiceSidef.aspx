<%@ Page Language="C#" AutoEventWireup="true" CodeFile="IndiceSidef.aspx.cs" Inherits="IndiceSidef" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Página sin título</title>
<link href="../estilos.css" rel="stylesheet" type="text/css">
<style type="text/css">
<!--
body {
	background-image: url(img/fondo-med.jpg);
	background-position:right;
	background-repeat: no-repeat;
}
a:visited {
	color: #999999;
}
a:hover {
	color: #990000;
	font-weight:bold;
}
-->
</style></head>

<body>
    <form id="form1" runat="server">
    <div>
        <p>&nbsp;</p>
        <p align="center"><strong> <font size="3" face="Arial, Helvetica, sans-serif">&Iacute;ndice 
        de Fotograf&iacute;as de productos.</font><font size="2" face="Arial, Helvetica, sans-serif"><br>
        </font></em></strong></p>
        <asp:Repeater runat="server" ID="rptProds"  >
            <ItemTemplate>                   
                <a href="div65-1.htm"><%# Eval("LaboratoryName") %></a><br>
            </ItemTemplate>
        </asp:Repeater>
              
        
        
    </div>
    </form>
</body>
</html>
