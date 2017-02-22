<%@ Page Language="C#" AutoEventWireup="true" CodeFile="IndProducts.aspx.cs" Inherits="IndProducts" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Índice de Productos</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table>
        <tr>
        <td align="left" style="width:178%">
        <asp:Repeater runat="server" ID="rptLetter" OnItemDataBound="rptLetter_ItemDataBound">
         <ItemTemplate>
            <asp:Label ID="lblLetter" runat="server" Font-Size="Larger" Font-Bold="true" ForeColor="DeepPink" Text='<%# DataBinder.Eval(Container.DataItem, "Letter") %>'></asp:Label>
            <asp:Repeater runat="server" ID="rptProds" OnItemDataBound="rptProds_ItemDataBound">
                <ItemTemplate>
                    <table>
                    <tr><br /><td>
                    <asp:Label runat="server" ID="lblProd" ForeColor="HotPink" CssClass="labelSubTitle"><%# DataBinder.Eval(Container.DataItem, "Description")%></asp:Label></td></tr><br />
                     <tr><td>
                     <asp:Repeater ID="rptCompany" runat="server" OnItemDataBound="rptCompa_ItemDataBound">
                     <ItemTemplate>
                            <table>
                            <tr><br /><td>
                            <asp:Label runat="server" ID="lblProd" CssClass="labelSubTitle"><%# DataBinder.Eval(Container.DataItem, "companyName")%></asp:Label></td></tr><br />
                            <tr><td>
                            </td></tr></table>    
                     </ItemTemplate>
                     </asp:Repeater>       
                     </td></tr>
                     <tr><td>
               <asp:Repeater ID="rptSubProds" runat="server" OnItemDataBound="rptSubProds_ItemDataBound">
                     <ItemTemplate>
                            <table>
                            <tr><br /><td>
                            <asp:Label runat="server" ID="lblProd" ForeColor="DarkViolet" CssClass="labelSubTitle"><%# DataBinder.Eval(Container.DataItem, "Description")%></asp:Label></td></tr><br />
                             <tr><td>
                              <asp:Repeater ID="rptCompanySP" runat="server" OnItemDataBound="rptCompaSP_ItemDataBound">
                             <ItemTemplate>
                                    <table>
                                    <tr><br /><td>
                                    <asp:Label runat="server" ID="lblProd" ForeColor="Chocolate" CssClass="labelSubTitle"><%# DataBinder.Eval(Container.DataItem, "CompanyName")%></asp:Label></td></tr><br />
                                    <tr><td>
                                    </td></tr></table>    
                             </ItemTemplate>
                             </asp:Repeater>     
                             </td></tr>
                             </table>
                     </ItemTemplate>
                     </asp:Repeater>
                     </td> 
                     </tr>
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
