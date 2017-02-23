<%@ Page Title="" Language="C#" MasterPageFile="~/medinet.master" AutoEventWireup="true" CodeFile="productShots.aspx.cs" Inherits="productShots" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <asp:Label ID="Label1" runat="server" Font-Names="Trebuchet MS" Font-Size="X-Large" Font-Bold="true" Text="Índice de sustancias" />
    <br />
    <br />
    <asp:Label ID="Label4" runat="server" Font-Names="Trebuchet MS" Font-Size="Medium" Text="Producto: " />
    <asp:Label ID="lblBrand" runat="server" Font-Names="Trebuchet MS" Font-Size="Medium" Font-Bold="true" />
    <br />
    <asp:Label ID="Label5" runat="server" Font-Names="Trebuchet MS" Font-Size="Medium" Text="Forma farmacéutica: " />
    <asp:Label ID="lblPharmaForm" runat="server" Font-Names="Trebuchet MS" Font-Size="Medium" Font-Bold="true" />
    <br />
    <br /> 
    <asp:Table ID="Table1" runat="server" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3"
        Font-Names="Trebuchet MS" Font-Size="Smaller">
        <asp:TableRow HorizontalAlign="Center">
            <asp:TableCell>
                <asp:ImageButton ID="imgBtnBackProducts" runat="server" ImageUrl="images/regresar.png" ToolTip="Regresar a productos" />
            </asp:TableCell>
         </asp:TableRow>
    </asp:Table>
    <asp:Table ID="tblAddShot" runat="server">
    <asp:TableRow BorderWidth="1px" Font-Names="Trebuchet MS" Font-Size="Smaller" Font-Bold="true">
    <asp:TableCell BorderWidth="1px">320x480</asp:TableCell>
    <asp:TableCell BorderWidth="1px">384x512</asp:TableCell>
    <asp:TableCell BorderWidth="1px">400x400</asp:TableCell>
    </asp:TableRow>
    <asp:TableRow BorderWidth="1px" Font-Names="Trebuchet MS" Font-Size="Smaller" Font-Bold="true">
    <asp:TableCell BorderWidth="1px"><asp:FileUpload ID="fUp320" runat="server" /></asp:TableCell>
    <asp:TableCell BorderWidth="1px"><asp:FileUpload ID="fUp384" runat="server" /></asp:TableCell>
    <asp:TableCell BorderWidth="1px"><asp:FileUpload ID="fUp400" runat="server" /></asp:TableCell>
    <asp:TableCell BorderWidth="1px"><asp:Button ID="btnAddSho" runat="server" Text="Agregar"  OnClick="btnAddShot"/></asp:TableCell>
    </asp:TableRow>
    </asp:Table>
    <br /><br />
     <asp:GridView ID="grdShots" runat="server" AutoGenerateColumns="False" 
        BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" 
                    CellPadding="3" Font-Names="Trebuchet MS" 
        HorizontalAlign="Left" Font-Size="Smaller"  DataKeyNames="EditionProductShotId"
        onrowdatabound="grdShots_RowDataBound" 
         
        >
                    <Columns>
                        <asp:BoundField DataField="EditionProductShotId" Visible="False" />
                        <asp:TemplateField HeaderText="Nombre de la imagen">
                            <HeaderStyle HorizontalAlign="Left" BackColor="#2694B4" ForeColor="White" />
                            <ItemStyle  Width="350px" />
                            <ItemTemplate>
                                <asp:Label ID="lblImageName" runat="server">
                                    <%# DataBinder.Eval(Container.DataItem, "ProductShot")%>
                                </asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtImageName" runat="server" CssClass="label" Text='<%# DataBinder.Eval(Container.DataItem, "ProductShot") %>'
                                    Width="350px" />
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Imagen">
                            <HeaderStyle HorizontalAlign="Left" BackColor="#2694B4" ForeColor="White" />
                            <ItemStyle  Width="200px" />
                            <ItemTemplate>
                                <asp:Image ID="imgSidef" runat="server" Width="30px" Height="30px" />
                            </ItemTemplate>
                            <EditItemTemplate>
                                
                            <table>
                            <tr>
                            <td>320x480</td><td>384x512</td><td>400x400</td></tr><tr>
                            <td><asp:FileUpload ID="fUp320X480" runat="server" /></td>
                            <td><asp:FileUpload ID="fUp384X512" runat="server" /></td>
                            <td><asp:FileUpload ID="fUp400X400" runat="server" /></td>
                            </tr>
                            </table>
                            </EditItemTemplate>
                            
                        </asp:TemplateField>
                        <%--<asp:TemplateField HeaderText="Editar">
                            <HeaderStyle Width="45px" HorizontalAlign="Center" BackColor="#2694B4" ForeColor="White" />
                            <ItemStyle HorizontalAlign="Center" Wrap="True" />
                            <ItemTemplate>
                                <asp:ImageButton ID="btnEditShot" runat="server" CommandName="Edit" ImageUrl="~/Images/editar.gif" AlternateText="Editar sustancia" Enabled="true" />
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:ImageButton ID="btnSaveShot" runat="server" CommandName="Update" ImageUrl="~/Images/salvar.jpg" AlternateText="Guardar sustancia" />
                            </EditItemTemplate>
                        </asp:TemplateField>--%>
                        <asp:TemplateField HeaderText="Eliminar">
                            <HeaderStyle Width="45px" HorizontalAlign="Center" BackColor="#2694B4" ForeColor="White" />
                            <ItemStyle HorizontalAlign="Center" Wrap="True" />
                            <ItemTemplate>
                                <asp:ImageButton ID="btnDeleteShot" runat="server" ImageUrl="~/Images/delete.gif" AlternateText="Eliminar sustancia" Enabled="true" 
                                    />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
</asp:Content>

