<%@ Page Language="C#" AutoEventWireup="true" CodeFile="IndAnunciantes.aspx.cs" Inherits="IndAnunciantes" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Página sin título</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Repeater ID="rptLetters" runat="server" OnItemDataBound="rptLetters_ItemDataBound">
        <ItemTemplate>
        <asp:Label ID="lblLetter" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Letter") %>'></asp:Label>
        <table>
        <tr>
        <td align="left" style="width:178%">
        <asp:Repeater runat="server" ID="rptComp" OnItemDataBound="rptComp_ItemDataBound">
         <ItemTemplate>
            <asp:Label ID="lblLab" runat="server" Font-Size="Larger" Font-Bold="true" Text='<%# DataBinder.Eval(Container.DataItem, "CompanyName") %>'></asp:Label>
            <asp:Label runat="server" ID="lblPhone" CssClass="labelSubTitle"><%# DataBinder.Eval(Container.DataItem, "Phone")%></asp:Label></td></tr><br />
            
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
