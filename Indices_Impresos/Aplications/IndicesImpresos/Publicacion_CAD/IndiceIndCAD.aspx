<%@ page language="C#" autoeventwireup="true" inherits="CAD_IndiceIndCAD, App_Web_nszxji13" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Página sin título</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Size="X-Large" Font-Strikeout="False"
            ForeColor="#0000C0" Text="ÍNDICE DE INDICACIONES"></asp:Label><br />
        <br />
        
        <br />
        <br />
        <table>
        <tr>
        <td align="left" style="width:178%">
        <asp:Repeater runat="server" ID="rptLet" OnItemDataBound="rptLet_ItemDataBound">
            <ItemTemplate>
                <table>
                <tr><td style="width:600px">
                </td></tr>
                <tr><br />
                <td>
                   <%--<img alt="" src='\\mmedina001\MARCAS - GUIA 55\Logos\<%# DataBinder.Eval(Container.DataItem, "Nombre del Archivo") %>'style="border:0; "/> --%>
                </td></tr><br />
                <tr><td>   
                <asp:Repeater runat="server" ID="rptInd" OnItemDataBound="rptInd_ItemDataBound">
                <ItemTemplate>
                    <table>
                    <tr><td style="width:600px">
                     </td></tr><tr><br /><td><asp:Label runat="server" ID="lblIndic" CssClass="labelSubTitle"><%# DataBinder.Eval(Container.DataItem, "INDICATIONS")%></asp:Label></td></tr><br />
                     <tr><td>
                        <asp:Repeater runat="server" ID="rptProds" OnItemDataBound="rptProds_ItemDataBound" >
                            <ItemTemplate>                   
                                <table>
                                <tr>
                                <td ><asp:Label runat="server" ID="lblProd" CssClass="label"><%# DataBinder.Eval(Container.DataItem, "Brand")%>.</asp:Label>
                                <asp:Label runat="server" ID="lblPharma" CssClass="label"><%# DataBinder.Eval(Container.DataItem, "PHARMAFORMS")%>.</asp:Label>                         
                                </td><br />    
                                </tr></table>
                            </ItemTemplate>
                        </asp:Repeater>
                        </td></tr></table>          
                </ItemTemplate>
            </asp:Repeater>
            </td><br />    
               </tr></table>
            </ItemTemplate>
            </asp:Repeater>
               
        </td>
        </tr>
        </table>
    </div>
    </form>
</body>
</html>
