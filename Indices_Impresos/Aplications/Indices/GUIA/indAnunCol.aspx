<%@ Page Language="C#" AutoEventWireup="true" CodeFile="indAnunCol.aspx.cs" Inherits="GUIA_indAnunCol" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Guía Colombia</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <asp:Repeater ID="rptSpeciality" runat="server" OnItemDataBound="rptSpeciality_ItemDataBound">
        <ItemTemplate>
        <asp:Label ID="lblSpeciality" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "SpecialityName") %>'></asp:Label><br /><br />
        
        <table>
        <tr>
        <td align="left" style="width:178%">
        <asp:Repeater runat="server" ID="rptComp" OnItemDataBound="rptComp_ItemDataBound">
         <ItemTemplate>
            <asp:Label ID="lblLab" runat="server" Font-Size="Larger" Font-Bold="true"><%# DataBinder.Eval(Container.DataItem, "ShortName") %>, 
            <%# DataBinder.Eval(Container.DataItem, "City") %>, <%# DataBinder.Eval(Container.DataItem, "Address") %><br />
            </asp:Label>            
       
        
        <asp:Repeater runat="server" ID="rptPhone" OnItemDataBound="rptPhone_ItemDataBound">
         <ItemTemplate>
            <asp:Label runat="server" ID="lblPhone" CssClass="labelSubTitle"><%# DataBinder.Eval(Container.DataItem, "Number") %></asp:Label><br /><br />
            </ItemTemplate>
        </asp:Repeater>
            
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
