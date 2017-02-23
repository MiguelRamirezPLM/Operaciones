<%@ Page Language="C#" MasterPageFile="~/medinet.master" AutoEventWireup="true" CodeFile="productDivisions.aspx.cs" Inherits="productDivisions" %>
<%@ MasterType VirtualPath="~/medinet.master" %>

<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1">

    <table cellspacing="3" cellpadding="3" border="0">
        <tr>
            <td>
                <asp:ImageButton ID="imgBtnBackLaboratory" runat="server" ImageUrl="images/regresar.png" ToolTip="Regresar al laboratorio" OnClick="imgBtnBackLaboratory_Click" />
            </td>
        </tr>
    </table>

    <br />

    <asp:Panel ID="pnlSearch" runat="server" DefaultButton="btnConsult">
        <asp:Table ID="Table1" runat="server">
            <asp:TableRow>
                <asp:TableCell HorizontalAlign="Left">
                    <asp:Label ID="Label2" runat="server" Text="Buscar por producto" Font-Names="Trebuchet MS" Font-Size="Smaller" Font-Bold="true" />
                </asp:TableCell>
                <asp:TableCell HorizontalAlign="Left">
                    <asp:Label ID="Label1" runat="server" Text="Buscar por laboratorio" Font-Names="Trebuchet MS" Font-Size="Smaller" Font-Bold="true" />
                </asp:TableCell>
                <asp:TableCell HorizontalAlign="Right">
                    <asp:Label ID="Label3" runat="server" Text="Registros por página" Font-Names="Trebuchet MS" Font-Size="Smaller" Font-Bold="true" />
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell HorizontalAlign="Left">
                    <asp:TextBox ID="txtProductName" runat="server" Text="" Enabled="true" />
                </asp:TableCell>
                <asp:TableCell HorizontalAlign="Left">
                    <asp:DropDownList ID="ddlOtherDivisions" runat="server" CssClass="label" Enabled="true" AutoPostBack="false" />
                    <asp:Button ID="btnConsult" runat="server" ToolTip="Consultar" Text="Consultar" OnClick="btnConsult_Click" />
                </asp:TableCell>
                <asp:TableCell HorizontalAlign="Right">
                    <asp:DropDownList ID="ddlPageSize" runat="server" AutoPostBack="True" Font-Names="Arial" Font-Size="10pt" OnSelectedIndexChanged="ddlPageSize_SelectedIndexChanged">
                        <asp:ListItem Value="-1" >Ilimitado</asp:ListItem>
                        <asp:ListItem Value="5">5</asp:ListItem>
                        <asp:ListItem Value="15" Selected="True">15</asp:ListItem>
                        <asp:ListItem Value="25">25</asp:ListItem>
                        <asp:ListItem Value="50">50</asp:ListItem>
                        <asp:ListItem Value="100">100</asp:ListItem>
                    </asp:DropDownList>
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
    </asp:Panel>

    <br />
    <br />

    <table>
        <tr>
            <td>
                <asp:GridView ID="grdDivisionProducts" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                    Font-Names="Trebuchet MS" Font-Size="Smaller" HorizontalAlign="Left" OnPageIndexChanging="grdDivisionProducts_PageIndexChanging" DataKeyNames="DivisionId,ProductId,PharmaFormId">
                    <Columns>
                        <asp:BoundField DataField="DivisionId" Visible="False" />
                        <asp:BoundField DataField="ProductId" Visible="False" />
                        <asp:BoundField DataField="PharmaFormId" Visible="False" />
                        <asp:TemplateField HeaderText="Laboratorio">
                            <HeaderStyle HorizontalAlign="Left" BackColor="#2694B4" ForeColor="White" />
                            <ItemStyle  Width="100px" />
                            <ItemTemplate>
                                <asp:Label ID="lblDivisionName" runat="server">
                                    <%# DataBinder.Eval(Container.DataItem, "DivisionShortName")%>
                                </asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Nombre">
                            <HeaderStyle HorizontalAlign="Left" BackColor="#2694B4" ForeColor="White" />
                            <ItemStyle  Width="100px" />
                            <ItemTemplate>
                                <asp:Label ID="lblProdName" runat="server">
                                    <%# DataBinder.Eval(Container.DataItem, "Brand") %>
                                </asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Forma Farmacéutica">
                            <HeaderStyle HorizontalAlign="Left" BackColor="#2694B4" ForeColor="White" />
                            <ItemStyle  Width="100px" />
                            <ItemTemplate>
                                <asp:Label ID="lblPharmaForm" runat="server">
                                    <%# DataBinder.Eval(Container.DataItem, "PharmaForm")%>
                                </asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Cambiar de laboratorio">
                            <HeaderStyle HorizontalAlign="Left" BackColor="#2694B4" ForeColor="White" />
                            <ItemStyle HorizontalAlign="Center" Wrap="True" Width="100px" />
                            <ItemTemplate>
                                <asp:CheckBox ID="chkChangeDivision" runat="server" AutoPostBack="false" Enabled="true" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="btnSaveProducts" runat="server" ToolTip="Guardar" Text="Guardar" OnClick="btnSaveProducts_Click" />
            </td>
        </tr>
    </table>
    
    <br />
    <br />
    <br />

</asp:Content>