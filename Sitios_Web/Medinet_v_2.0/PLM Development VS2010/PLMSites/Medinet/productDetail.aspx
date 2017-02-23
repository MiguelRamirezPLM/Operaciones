<%@ Page Language="C#" MasterPageFile="~/medinet.master" AutoEventWireup="true" CodeFile="productDetail.aspx.cs" Inherits="productDetail" %>
<%@ MasterType VirtualPath="~/medinet.master" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">

    <asp:Label ID="Label1" runat="server" Font-Names="Trebuchet MS" Font-Size="X-Large" Font-Bold="true" Text="Detalle del producto" />
    <br />
    <br />
    <asp:Label ID="Label3" runat="server" Font-Names="Trebuchet MS" Font-Size="Medium" Font-Bold="true" Text="Producto: " />
    <asp:Label ID="lblBrand" runat="server" Font-Names="Trebuchet MS" Font-Size="Medium" Font-Bold="true" Text="" />
    <br />
    <asp:Label ID="Label5" runat="server" Font-Names="Trebuchet MS" Font-Size="Medium" Font-Bold="true" Text="Forma Farmacéutica: " />
    <asp:Label ID="lblPharmaForm" runat="server" Font-Names="Trebuchet MS" Font-Size="Medium" Font-Bold="true" Text="" />
    <br />
    <br />

    <table width="100%">
        <tr>
            <td valign="top">
                <asp:Label ID="Label6" runat="server" Font-Names="Trebuchet MS" Font-Size="Medium" Font-Bold="true" Text="Sustancias asociadas" />
                <br />
                <asp:GridView ID="grdProductSubstances" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                    Font-Names="Trebuchet MS" Font-Size="Smaller" DataKeyNames="ActiveSubstanceId" HorizontalAlign="Left" Visible="false">
                    <Columns>
                        <asp:BoundField DataField="ActiveSubstanceId" Visible="False" />
                        <asp:TemplateField HeaderText="Nombre de la sustancia">
                            <HeaderStyle HorizontalAlign="Left" BackColor="#2694B4" ForeColor="White" />
                            <ItemStyle  Width="450px" />
                            <ItemTemplate>
                                <asp:Label ID="lblActiveSubstance" runat="server">
                                    <%# DataBinder.Eval(Container.DataItem, "Description")%>
                                </asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <br />
                <asp:Label ID="lblNotSubstances" runat="server" Font-Names="Trebuchet MS" Font-Size="Medium" Font-Bold="true" Visible="false" Text="Este producto no tiene sustancias asociadas." />
                <br />
            </td>
            
            <td valign="top">
                <asp:Label ID="Label2" runat="server" Font-Names="Trebuchet MS" Font-Size="Medium" Font-Bold="true" Text="Indicaciones asociadas" />
                <br />
                <asp:GridView ID="grdProductIndications" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                    Font-Names="Trebuchet MS" Font-Size="Smaller" DataKeyNames="IndicationId" HorizontalAlign="Left" Visible="false">
                    <Columns>
                        <asp:BoundField DataField="IndicationId" Visible="False" />
                        <asp:TemplateField HeaderText="Nombre de la indicación">
                            <HeaderStyle HorizontalAlign="Left" BackColor="#2694B4" ForeColor="White" />
                            <ItemStyle  Width="450px" />
                            <ItemTemplate>
                                <asp:Label ID="lblIndication" runat="server">
                                    <%# DataBinder.Eval(Container.DataItem, "Description")%>
                                </asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <br />
                <asp:Label ID="lblNotIndications" runat="server" Font-Names="Trebuchet MS" Font-Size="Medium" Font-Bold="true" Visible="false" Text="Este producto no tiene indicaciones asociadas." />
                <br />
            </td>
        </tr>
        <tr>
            <td valign="top" colspan="2">
                <br />
                <hr />
                <br />
                <asp:Label ID="Label4" runat="server" Font-Names="Trebuchet MS" Font-Size="Medium" Font-Bold="true" Text="Información para prescribir: " />
                <br />
                <br />
                <asp:Label ID="lblProductContent" runat="server" Text="" />
            </td>
        </tr>
    </table>

</asp:Content>

