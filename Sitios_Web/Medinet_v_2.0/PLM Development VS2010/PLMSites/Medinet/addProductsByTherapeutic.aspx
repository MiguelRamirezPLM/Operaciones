<%@ Page Language="C#" MasterPageFile="~/medinet.master" AutoEventWireup="true" CodeFile="addProductsByTherapeutic.aspx.cs" Inherits="addProductsByTherapeutic" %>

<%@ MasterType VirtualPath="~/medinet.master" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">

    <script type="text/javascript">

        function confirmDelete(deleteName) {
            var agree = confirm('Has elegido eliminar a: ' + deleteName + ', ¿Confirmas que deseas borrar ese producto de este Spinoff?');
            if (agree)
                return true;
            else
                return false;
        }

    </script>
    
    <asp:Label ID="Label4" runat="server" Font-Names="Trebuchet MS" Font-Size="X-Large" Font-Bold="true" Text="Ingresar productos por ATC" />
    <br />
    <br />

    <asp:Table ID="Table1" runat="server" BackColor="White" BorderColor="#000000" BorderStyle="None" BorderWidth="1px" CellPadding="3" Font-Names="Trebuchet MS" Font-Size="Smaller">
        <asp:TableRow>
            <asp:TableCell>
                <asp:ImageButton ID="imgBtnBackProducts" runat="server" ImageUrl="images/regresar.png" ToolTip="Regresar a productos" OnClick="imgBtnBackProducts_Click" />
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>

    <asp:Panel ID="pnlSearch" runat="server" DefaultButton="btnConsultProducts">
        <table width="100%" cellspacing="25px">
            <tr>
                <td valign="top">
                    <table>
                        <tr>
                            <td>
                                <asp:Table ID="Table2" runat="server" Width="450px">
                                    <asp:TableRow>
                                        <asp:TableCell HorizontalAlign="Left">
                                            <asp:Label ID="Label2" runat="server" Text="Buscar un producto" Font-Names="Trebuchet MS" Font-Size="Smaller" Font-Bold="true" />
                                        </asp:TableCell>
                                        <asp:TableCell HorizontalAlign="Right">
                                            <asp:Label ID="Label3" runat="server" Text="Registros por página" Font-Names="Trebuchet MS" Font-Size="Smaller" Font-Bold="true" />
                                        </asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow>
                                        <asp:TableCell HorizontalAlign="Left">
                                            <asp:TextBox ID="txtProductName" runat="server" Text="" Enabled="true" />
                                            <asp:Button ID="btnConsultProducts" runat="server" ToolTip="Consultar" Text="Consultar" TabIndex="0"  OnClick="btnConsult_Click" />
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
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:GridView ID="grdProducts" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                                    Font-Names="Trebuchet MS" Font-Size="Smaller" DataKeyNames="ProductId,PharmaFormId,DivisionId,CategoryId" OnRowDataBound="grdProducts_RowDataBound"
                                    OnPageIndexChanging="grdProducts_PageIndexChanging">
                                    <Columns>
                                        <asp:BoundField DataField="ProductId" Visible="False" />
                                        <asp:BoundField DataField="PharmaFormId" Visible="False" />
                                        <asp:BoundField DataField="DivisionId" Visible="False" />
                                        <asp:BoundField DataField="CategoryId" Visible="False" />
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
                                        <asp:TemplateField HeaderText="Laboratorio">
                                            <HeaderStyle HorizontalAlign="Left" BackColor="#2694B4" ForeColor="White" />
                                            <ItemStyle  Width="100px" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblDivisionName" runat="server">
                                                    <%# DataBinder.Eval(Container.DataItem, "DivisionShortName")%>
                                                </asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Eliminar de este Spinoff">
                                            <HeaderStyle Width="100px" HorizontalAlign="Center" BackColor="#2694B4" ForeColor="White" />
                                            <ItemStyle HorizontalAlign="Center" Wrap="True" />
                                            <ItemTemplate>
                                                <asp:ImageButton ID="btnDeleteProductSpinoff" runat="server" ImageUrl="~/Images/delete.gif" AlternateText="Eliminar de este Spinoff" 
                                                    Enabled="true" OnClick="btnDeleteProductSpinoff_OnClick" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                </td>
        
                <td valign="top">
                    <asp:Button ID="btnSaveATCProducts" runat="server" Text="Guardar productos por ATC" TabIndex="1" OnClick="btnSaveATCProducts_OnClick" />
                    <br />
                    <br />
                    <asp:Label ID="Label7" runat="server" Font-Names="Trebuchet MS" Font-Size="Large" Font-Bold="true" Text="Árbol terapéutico" />
                    <br />
                    <asp:TreeView ID="ATCTree" runat="server" CssClass="label" ExpandDepth="0" Font-Names="Trebuchet MS" Font-Size="Small" ForeColor="#2694B4" Font-Bold="true" 
                        OnTreeNodePopulate="ATCTree_TreeNodePopulate" />
                </td>
            </tr>

        </table>
    </asp:Panel>



</asp:Content>