<%@ Page Language="C#" AutoEventWireup="true" CodeFile="IndiceCAnunciantesNew.aspx.cs" Inherits="IndiceCAnunciantesNew" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:Repeater ID="rptLetters" runat="server" OnItemDataBound="rptLetters_ItemDataBound">
        <ItemTemplate>
        <asp:Label ID="lblLetter" runat="server" ForeColor="#660066" Font-Bold="true" Font-Size="30px" Text='<%# DataBinder.Eval(Container.DataItem, "LetterCN") %>'></asp:Label> <br />
         
           <asp:Repeater ID="rptClientN" runat="server" OnItemDataBound="rptClientN_ItemDataBound">
           <ItemTemplate>
           <asp:Label ID="lblLetter" runat="server" ForeColor="#993366" Font-Bold="true" Font-Size="15px" Text='<%# DataBinder.Eval(Container.DataItem, "CompanyName") %>'></asp:Label> <br />
           <asp:Label ID="Label1" runat="server" ForeColor="#993366" Font-Bold="true" Font-Size="15px" Text='<%# DataBinder.Eval(Container.DataItem, "Page") %>'></asp:Label>  

                 <asp:Repeater ID="rptDirClientN" runat="server" OnItemDataBound="rptDirClientN_ItemDataBound">
                 <ItemTemplate>
                 <asp:Label ID="lblLetter" runat="server" ForeColor="#ff99ff"  Font-Size="13px" Text='<%# DataBinder.Eval(Container.DataItem, "Address") %>'></asp:Label> <br />
                 <asp:Label ID="Label1" runat="server" ForeColor="#ff99ff"  Font-Size="13px" Text='<%# DataBinder.Eval(Container.DataItem, "Suburb") %>'></asp:Label>  <br />
                 </ItemTemplate>
                 </asp:Repeater>

               <asp:Label ID="Label2" runat="server" ForeColor="#ff99ff"  Font-Bold="true" Font-Size="13px" >SUCURSALES</asp:Label>  <br />


           </ItemTemplate>
           </asp:Repeater>

        </ItemTemplate>
        </asp:Repeater>

    </div>
    </form>
</body>
</html>
