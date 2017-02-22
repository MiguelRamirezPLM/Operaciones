<%@ Page Language="C#" AutoEventWireup="true" CodeFile="IndiceIng.aspx.cs" Inherits="DEAQ_IndiceIng" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Página sin título</title>
    <link href="../Estilos.css" rel="Stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table align="center" cellSpacing="0" cellPadding="7" width="95%" border="0">
	    <tr><td><table width="100%">
		    <tr><td align="center" ><p class="Titulo">&Iacute;NDICE POR INGREDIENTES ACTIVOS</p></td></tr>
		    <tr><td align="center">  <asp:Image id="img" runat="server" /></td></tr></table>
	</td></tr>
	</table>
   <asp:Repeater ID="rptUsos" runat="server" OnItemDataBound="rptUsos_ItemDataBound">
	    <ItemTemplate>
	        <table width="100%" align="center" border="0"cellspacing="0" cellpadding="0">
	        <tr>
	        <td>
	            <span class="Rubro"><%# DataBinder.Eval(Container.DataItem, "IActivo")%></span>
	        </td>
	        </tr>
	        <tr>
	        <td>&nbsp;</td>
	       </tr>
	        <tr>
	        <td>&nbsp;&nbsp;&nbsp;<asp:Label id="LSolo" CssClass="Rubro" runat="server" visible="false">SOLO</asp:Label></td>
	       </tr> 
	        <tr>
	            <td>
	                <ul>
	                <asp:Repeater ID="rptSProds" runat="server">
	                <ItemTemplate>
	                    <p class="Links"><a style="text-decoration:none" href='<%# DataBinder.Eval(Container.DataItem,"File","../prods/{0}") %>' ><b><%# DataBinder.Eval(Container.DataItem,"Nombre") %>.</b> <i><%# DataBinder.Eval(Container.DataItem,"Laboratorio") %>.</i></a></p>
	                </ItemTemplate>
	                </asp:Repeater>
	                </ul>
	            </td>
	        </tr>
	       <tr>
	        <td>&nbsp;&nbsp;&nbsp;<asp:Label id="LCombinado" class="Rubro" runat="server" visible="false">COMBINADO</asp:Label></td>
	       </tr> 
	       <tr>
	           <td>
	                <ul>
	               <asp:Repeater ID="rptCProds" runat="server">
	                <ItemTemplate>
	                    <p class="Links"><a style="text-decoration:none" href='<%# DataBinder.Eval(Container.DataItem,"File","../prods/{0}") %>' ><b><%# DataBinder.Eval(Container.DataItem,"Nombre") %>.</b> <i><%# DataBinder.Eval(Container.DataItem,"Laboratorio") %>.</i></a></p>
	                </ItemTemplate>
	                </asp:Repeater>
	                </ul>
	            </td>
	        </tr>
	        </table>
	    </ItemTemplate>
	    </asp:Repeater>
      </div>  
    </form>
</body>
</html>