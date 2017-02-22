<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Carpetas.aspx.cs" Inherits="Carpetas" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Página sin título</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table>
        <tr>
        <td align="left" style="width:178%">
        <asp:Repeater runat="server" ID="rptLetter" OnItemDataBound="rptLetter_ItemDataBound">
         <ItemTemplate>
            <%--<img alt="" src='\\195.192.2.180\Imagenes\GUIA 56\Logos\<%# DataBinder.Eval(Container.DataItem, "Archivo") %>'style="border:0; " width="25%" />  --%>
            <asp:Repeater runat="server" ID="rptMarcas" OnItemDataBound="rptMarcas_ItemDataBound">
                <ItemTemplate>
                    <table>
                    <tr><td style="width:600px">
                     <img alt="" src='\\mmedina001\MARCAS - GUIA 56\Logos\<%# DataBinder.Eval(Container.DataItem, "logo") %>'style="border:0; " width="25%" />    
                    </td></tr><tr><br /><td><asp:Label runat="server" ID="lblMarca" CssClass="labelSubTitle"><%# DataBinder.Eval(Container.DataItem,"brandname") %><%# DataBinder.Eval(Container.DataItem,"expire") %> </asp:Label></td></tr><br />
                     <tr><td>
                        <%--<asp:Repeater runat="server" ID="rptEmpresas" OnItemDataBound="rptEmpresas_ItemDataBound">
                            <ItemTemplate>                   
                                <table>
                                <tr>
                                <td ><asp:Label runat="server" ID="lblCarta" CssClass="label"><%# DataBinder.Eval(Container.DataItem, "clientbrandtypeId")%></asp:Label></td>
                                <td><asp:Label runat="server" ID="lblEmpresa" CssClass="label"><%# DataBinder.Eval(Container.DataItem, "companyname")%></asp:Label></td>
                                <br />    
                                </tr></table>
                            </ItemTemplate>
                        </asp:Repeater>--%>
                        </td></tr></table>          
                </ItemTemplate>
            </asp:Repeater>
        </ItemTemplate>
        </asp:Repeater>
        
        </td>
        </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
