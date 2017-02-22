<%@ Page Language="C#" AutoEventWireup="true" CodeFile="IndMedHosp.aspx.cs" Inherits="IndMedHosp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Índice de Medicamentos Hospitalarios</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table>
        <tr>
        <td align="left" style="width:178%">
        <asp:Repeater runat="server" ID="rptLabs" OnItemDataBound="rptLabs_ItemDataBound">
         <ItemTemplate>
            <asp:Label ID="lblLab" runat="server" Font-Size="Larger" Font-Bold="true" Text='<%# DataBinder.Eval(Container.DataItem, "CompanyName") %>'></asp:Label>
            <asp:Label runat="server" ID="lblPhone" CssClass="labelSubTitle"><%# DataBinder.Eval(Container.DataItem, "Phone")%></asp:Label></td></tr><br />
             <asp:Repeater ID="rptSucs" runat="server" OnItemDataBound="rptSucs_ItemDataBound">
             <ItemTemplate>
                    <table>
                    <tr><br /><td>
                    <asp:Label runat="server" ID="lblProd" CssClass="labelSubTitle"><%# DataBinder.Eval(Container.DataItem, "CompanyName")%></asp:Label></td></tr><br />
                     <tr><td>
                      
                     </td></tr>
                     </table>
             </ItemTemplate>
             </asp:Repeater>
             <asp:Repeater ID="rptProds" runat="server" OnItemDataBound="rptProds_ItemDataBound">
             <ItemTemplate>
                    <table>
                    <tr><br /><td>
                    <asp:Label runat="server" ID="lblProd" CssClass="labelSubTitle"><%# DataBinder.Eval(Container.DataItem, "DRUGNAME")%></asp:Label></td></tr><br />
                     <tr><td>
                      
                     </td></tr>
                     </table>
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
