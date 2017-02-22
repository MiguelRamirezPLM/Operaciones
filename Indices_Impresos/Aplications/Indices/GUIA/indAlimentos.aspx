<%@ Page Language="C#" AutoEventWireup="true" CodeFile="indAlimentos.aspx.cs" Inherits="indAlimentos" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Página sin título</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <br />
        <table>
        <tr>
        <td align="left" style="width:178%">
        <asp:Repeater runat="server" ID="rptStates" OnItemDataBound="rptStates_ItemDataBound">
         <ItemTemplate>
            <table>
            <tr>
                <td>
                    <asp:Label runat="server" ID="lblstate" CssClass="label"><%# DataBinder.Eval(Container.DataItem, "stateName")%></asp:Label>
                </td>
            </tr>
            <tr>
            <td>
            <asp:Repeater runat="server" ID="rptCities" OnItemDataBound="rptCities_ItemDataBound">
                <ItemTemplate>
                    <table>
                    <tr><td style="width:600px">
                    </td></tr><tr><br /><td><asp:Label runat="server" ID="lblCities" CssClass="labelSubTitle"><%# DataBinder.Eval(Container.DataItem,"City") %></asp:Label></td></tr><br />
                     <tr><td>
                        <asp:Repeater runat="server" ID="rptCompany" OnItemDataBound="rptCompany_ItemDataBound">
                            <ItemTemplate>                   
                                <table>
                                <tr>
                                <td ><asp:Label runat="server" ID="lblCompanyName" CssClass="label"><%# DataBinder.Eval(Container.DataItem, "CompanyName")%></asp:Label></td></tr>
                               <tr>
                                <td><asp:Label runat="server" ID="lblAddress" CssClass="label"><%# DataBinder.Eval(Container.DataItem, "address")%></asp:Label></td>
                                <br />    
                                </tr>
                                <tr>
                                    <td><asp:Label runat="server" ID="Label1" CssClass="label"><%# DataBinder.Eval(Container.DataItem, "Suburb")%></asp:Label></td>
                                </tr>
                                <tr>
                                    <td>
                                          <%--<asp:Repeater runat="server" ID="rptPhone" OnItemDataBound="rptPhone_ItemDataBound">
                                          <ItemTemplate>
                                          <table>
                                          <tr>
                                               <td><asp:Label runat="server" ID="Label1" CssClass="label"><%# DataBinder.Eval(Container.DataItem, "number")%></asp:Label></td>
                                          </tr>
                                          </table>
                                          </ItemTemplate>
                                          </asp:Repeater>--%>
                                    </td>
                                </tr>
                                </table>
                            </ItemTemplate>
                        </asp:Repeater>
                        </td></tr></table>          
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
