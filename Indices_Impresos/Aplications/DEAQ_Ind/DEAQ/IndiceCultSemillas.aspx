<%@ Page Language="C#" AutoEventWireup="true" CodeFile="IndiceCultSemillas.aspx.cs" Inherits="DEAQ_IndiceCult" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Página sin título</title>
</head>
<body>
    <form id="form1" runat="server">
<div>
        <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Size="X-Large" Font-Strikeout="False"
            ForeColor="#0000C0" Text="ÍNDICE DE CULTIVOS DEAQ 20"></asp:Label><br />
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
                <asp:Repeater runat="server" ID="rptCult" OnItemDataBound="rptCult_ItemDataBound">
                <ItemTemplate>
                    <table>
                    <tr><td style="width:600px">
                     </td></tr><tr><br /><td><asp:Label runat="server" ID="lblIndic" CssClass="labelSubTitle"><%# DataBinder.Eval(Container.DataItem, "Cultivo")%></asp:Label></td></tr><br />
                     <tr><td>
                        <asp:Repeater runat="server" ID="rptProds" OnItemDataBound="rptProds_ItemDataBound" >
                            <ItemTemplate>                   
                                <table>
                                <tr>
                                <td ><asp:Label runat="server" ID="lblProd" CssClass="label"><%# DataBinder.Eval(Container.DataItem, "Nombre")%>.</asp:Label>
                                <asp:Label runat="server" ID="lblPharma" CssClass="label"><%# DataBinder.Eval(Container.DataItem, "Usos")%>.</asp:Label>                    
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
