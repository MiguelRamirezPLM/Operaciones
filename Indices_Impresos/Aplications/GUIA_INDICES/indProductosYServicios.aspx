<%@ Page Language="C#" AutoEventWireup="true" CodeFile="indProductosYServicios.aspx.cs" Inherits="indProductosYServicios" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <asp:Repeater ID="rptLetterPyS" runat="server" OnItemDataBound="rptLetterPyS_ItemDataBound">
    <ItemTemplate>
        <asp:Label ID="lblLetters" Font-Bold="true" Font-Size="X-Large" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "L") %>'></asp:Label><br />
        
        <asp:Repeater runat="server" ID="rptCatN3" OnItemDataBound="rptCatN3_ItemDataBound">
         <ItemTemplate>
            -<asp:Label ID="lblLab" runat="server" ForeColor="Purple"  Text='<%# DataBinder.Eval(Container.DataItem, "CategoryThree") %>'></asp:Label><br />
                
                 <asp:Repeater runat="server" ID="rptCatN4" OnItemDataBound="rptCatN4_ItemDataBound">
                 <ItemTemplate>
                   -- <asp:Label ID="lblLab" runat="server" ForeColor="#ff99ff"  Text='<%# DataBinder.Eval(Container.DataItem, "LeafCategory") %>'></asp:Label><br />

                     <asp:Repeater runat="server" ID="rptClientC" OnItemDataBound="rptClientC_ItemDataBound">
                       <ItemTemplate>
                         ---   <asp:Label ID="lblLab" runat="server" ForeColor="#ccccff"  Text='<%# DataBinder.Eval(Container.DataItem, "CompanyName") %>'></asp:Label><br />

                           <asp:Repeater runat="server" ID="rptProducto" OnItemDataBound="rptProducto_ItemDataBound">
                               <ItemTemplate>
                                 ----   <asp:Label ID="lblLab" runat="server" ForeColor="DeepSkyBlue"  Text='<%# DataBinder.Eval(Container.DataItem, "ProductName") %>'></asp:Label><br />
                               </ItemTemplate>
                           </asp:Repeater>

                       </ItemTemplate>
                     </asp:Repeater>

                 </ItemTemplate>
                 </asp:Repeater>

        </ItemTemplate>
        </asp:Repeater>
        
        
    </ItemTemplate>
    </asp:Repeater>
    </div>
    </form>
</body>
</html>
