<%@ Page Language="C#" AutoEventWireup="true" CodeFile="IndInternacional.aspx.cs" Inherits="IndInternacional" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Directorio Internacional</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Repeater ID="rptLetter" runat="server" OnItemDataBound="rptLetter_ItemDataBound">
        <ItemTemplate>
            <asp:Label ID="lblLetter" runat="server"><%# DataBinder.Eval(Container.DataItem,"Letter") %></asp:Label>
            <br />
            <asp:Repeater ID="rptCompany" runat="server" OnItemDataBound="rptCompany_ItemDataBound">
            <ItemTemplate>
                <asp:Label ID="lblCompany" runat="server"><%# DataBinder.Eval(Container.DataItem, "Nombre de la EMPRESA INTERNACIONAL")%></asp:Label>
                <br />
                <asp:Repeater ID="rptSCompany" runat="server" OnItemDataBound="rptSCompany_ItemDataBound">
                <ItemTemplate>
                    <asp:Label ID="lblSCompany" runat="server"><%# DataBinder.Eval(Container.DataItem, "Nombre de la EMPRESA INTERNACIONAL")%></asp:Label>
                </ItemTemplate>
                </asp:Repeater>
                <br />
                <asp:Repeater ID="rptProductN0" runat="server" OnItemDataBound="rptProductN0_ItemDataBound">
                <ItemTemplate>
                    <asp:Label ID="lblProdN0" runat="server"><%# DataBinder.Eval(Container.DataItem,"ProductName") %></asp:Label>
                    <br />
                    <asp:Repeater ID="rptProductN1" runat="server" OnItemDataBound="rptProductN1_ItemDataBound">
                    <ItemTemplate>
                        <asp:Label ID="lblProdN1" runat="server"><%# DataBinder.Eval(Container.DataItem,"ProductName") %></asp:Label>
                        <br />
                        <asp:Repeater ID="rptProductN2" runat="server" OnItemDataBound="rptProductN2_ItemDataBound">
                        <ItemTemplate>
                            <asp:Label ID="lblProdN2" runat="server"><%# DataBinder.Eval(Container.DataItem,"ProductName") %></asp:Label>
                            <br />
                            <asp:Repeater ID="rptProductN3" runat="server" OnItemDataBound="rptProductN3_ItemDataBound">
                            <ItemTemplate>
                                <asp:Label ID="lblProdN3" runat="server"><%# DataBinder.Eval(Container.DataItem,"ProductName") %></asp:Label>
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
        </ItemTemplate>
        </asp:Repeater>
    </div>
    </form>
</body>
</html>
