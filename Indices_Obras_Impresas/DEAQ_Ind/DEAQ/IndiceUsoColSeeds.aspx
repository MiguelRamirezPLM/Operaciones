<%@ Page Language="C#" AutoEventWireup="true" CodeFile="IndiceUsoColSeeds.aspx.cs" Inherits="IndiceUsoCol" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Página sin título</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Size="X-Large" Font-Strikeout="False"
            ForeColor="#0000C0" Text="ÍNDICE DE USOS Semillas"></asp:Label><br />
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
                   
                </td></tr><br />
                <tr><td>   
                <asp:Repeater runat="server" ID="rptUso" OnItemDataBound="rptUso_ItemDataBound">
                <ItemTemplate>
                    <table>
                    <tr><td style="width:600px">
                     </td></tr>
                     <tr><br /><td><asp:Label runat="server" ID="lblIndic" CssClass="labelSubTitle"><%# DataBinder.Eval(Container.DataItem, "Usos")%></asp:Label></td></tr><br />
                     <tr><br /><td>
                        <asp:Repeater runat="server" ID="rptProds" OnItemDataBound="rptProds_ItemDataBound" >
                            <ItemTemplate>                   
                                <table>
                                <tr>
                                <td ><asp:Label runat="server" ID="lblProd" CssClass="label"><%# DataBinder.Eval(Container.DataItem, "Nombre")%>.</asp:Label>
                                <asp:Label runat="server" ID="lblPharma" CssClass="label"><%# DataBinder.Eval(Container.DataItem, "Sustancias")%>.</asp:Label>                         
                                </td><br />    
                                </tr></table>
                            </ItemTemplate>
                        </asp:Repeater>
                     </td></tr>
                     <tr><td>
                        <asp:Repeater runat="server" ID="rptSubUso" OnItemDataBound="rptSubUso_ItemDataBound">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblSubUso" CssClass="labelSubTitle"><%# DataBinder.Eval(Container.DataItem,"Usos") %></asp:Label>
                            <br /><br />
                            <asp:Repeater runat="server" ID="rptProdsN2" OnItemDataBound="rptProdsN2_ItemDataBound" >
                            <ItemTemplate>                   
                                <table>
                                <tr>
                                <td ><asp:Label runat="server" ID="lblProdN2" CssClass="label"><%# DataBinder.Eval(Container.DataItem, "Nombre")%>.</asp:Label>
                                <asp:Label runat="server" ID="lblPharmaN2" CssClass="label"><%# DataBinder.Eval(Container.DataItem, "Laboratorio")%>.</asp:Label>                         
                                </td><br />    
                                </tr></table>
                            </ItemTemplate>
                        </asp:Repeater>
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
