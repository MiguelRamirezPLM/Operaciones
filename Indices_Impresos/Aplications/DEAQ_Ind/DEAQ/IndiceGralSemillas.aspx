<%@ Page Language="C#" AutoEventWireup="true" CodeFile="IndiceGralSemillas.aspx.cs" Inherits="DEAQ_IndiceGral" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Página sin título</title> 
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Size="X-Large" Font-Strikeout="False"
            ForeColor="#0000C0" Text="ÍNDICE DE SEmillas DEAQ 20"></asp:Label><br />
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
                <asp:Repeater runat="server" ID="rptProd" OnItemDataBound="rptProd_ItemDataBound" >
                    <ItemTemplate>
                        <table>
                        <tr>
                        <td ><asp:Label runat="server" ID="lblProd" CssClass="label"><%# DataBinder.Eval(Container.DataItem, "Nombre")%>.</asp:Label>
                        <asp:Label runat="server" ID="lblDesc" CssClass="label"><%# DataBinder.Eval(Container.DataItem, "Registro")%>.</asp:Label>
                        <asp:Label runat="server" ID="lblActive" CssClass="label"><%# DataBinder.Eval(Container.DataItem, "Usos")%>.</asp:Label>
                        <asp:Label runat="server" ID="lblPharma" CssClass="label"><%# DataBinder.Eval(Container.DataItem, "FFarmaceutica")%>.</asp:Label>
                        <asp:Label runat="server" ID="Label3" CssClass="label"><%# DataBinder.Eval(Container.DataItem, "Laboratorio")%>.</asp:Label>                            
                        </td><br />    
                        </tr></table>
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
