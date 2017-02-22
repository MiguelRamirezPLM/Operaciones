<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Brands.aspx.cs" Inherits="Brands" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Página sin título</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Repeater  runat="server" ID="rptBrand" OnItemDataBound="rptBrand_ItemDataBound">
                    <ItemTemplate>
                        <table>
                            <tr>
                                <td>
                                <asp:Repeater runat="server" ID="rptProd" OnItemDataBound="rptProd_ItemDataBound">
                                    <ItemTemplate>
                                        <table>
                                            <tr>
                                            <asp:Label runat="server" ID="lblProd" CssClass="label">.</asp:Label>                                            
                                            </tr>
                                        </table>
                                    </ItemTemplate>
                                </asp:Repeater>
                                </td>
                            </tr>
                        </table>    
                    </ItemTemplate>
        </asp:Repeater>
    
    </div>
    </form>
</body>
</html>
