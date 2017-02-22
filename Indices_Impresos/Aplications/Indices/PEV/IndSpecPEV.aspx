<%@ Page Language="C#" AutoEventWireup="true" CodeFile="IndSpecPEV.aspx.cs" Inherits="PEV_IndSpecPEV" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Página sin título</title>
</head>
<body>
    <form id="form1" runat="server">
     <div>
        <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Size="X-Large" Font-Strikeout="False"
                ForeColor="#0000C0" Text="ÍNDICE Species PEV"></asp:Label><br />
        <br />
        <br />
        <table>
        <tr>
            <td>
                <asp:Repeater  runat="server" ID="rptSpe" OnItemDataBound="rptSpe_OnItemDataBound">
                    <ItemTemplate>
                        <table>
                            <tr>
                                <td>
                                <asp:Repeater runat="server" ID="rptProd" OnItemDataBound="rptProd_OnItemDataBound">
                                    <ItemTemplate>
                                        <table>
                                            <tr>
                                            <asp:Label runat="server" ID="lblProd" CssClass="label"><%# DataBinder.Eval(Container.DataItem, "ProductName")%>.</asp:Label>
                                            <%--<asp:Label runat="server" ID="lblDesc" CssClass="label"><%# DataBinder.Eval(Container.DataItem, "ProductDescription")%>.</asp:Label>--%>
                                            <asp:Label runat="server" ID="lblActive" CssClass="label"><%#DataBinder.Eval(Container.DataItem, "PHARMAFORM")%>.</asp:Label>
                                            <asp:Label runat="server" ID="lblPharma" CssClass="label"><%# DataBinder.Eval(Container.DataItem, "DivisionShortName")%>.</asp:Label>
                                            <%--<asp:Label runat="server" ID="Label3" CssClass="label"><%#DataBinder.Eval(Container.DataItem, "Page")%>.</asp:Label> --%>
                                            </tr>
                                        </table>
                                    </ItemTemplate>
                                </asp:Repeater>
                                </td>
                            </tr>
                        </table>    
                    </ItemTemplate>
                </asp:Repeater>
            </td>
        </tr>
        
        
        
        </table>
    </div>
    </form>
</body>
</html>
