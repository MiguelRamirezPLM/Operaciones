<%@ Page Language="C#" MasterPageFile="~/medinet.master" AutoEventWireup="true" CodeFile="productPresentations.aspx.cs" Inherits="productPresentations" %>
<%@ MasterType VirtualPath="~/medinet.master" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">

    <asp:Label ID="lblBrand" runat="server" Font-Names="Trebuchet MS" Font-Size="Large" Font-Bold="true" />
    <br />
    <asp:Label ID="lblPharmaForm" runat="server" Font-Names="Trebuchet MS" Font-Size="Large" Font-Italic="true" />
    <br />
    <br />

    <asp:ImageButton ID="imgBtnBackProducts" runat="server" ImageUrl="images/regresar.png" ToolTip="Regresar a productos" OnClick="imgBtnBackProducts_Click" />

    <br />
    <br />
    <br />
    
    <asp:Label ID="lblTitle" runat="server" Text="Agregar presentación al producto" Font-Names="Trebuchet MS" Font-Size="Medium" Font-Bold="true" />
    
    <asp:Table ID="Table1" runat="server" >
        <asp:TableRow BorderWidth="1px" Font-Names="Trebuchet MS" Font-Size="Smaller" Font-Bold="true">
            <asp:TableCell BorderWidth="1px" Width="55px">
                <asp:Label ID="lblQtyExtPack" runat="server" Text="Cantidad" Width="55px" />
            </asp:TableCell><asp:TableCell BorderWidth="1px" Width="200px">
                <asp:Label ID="lblExtPack" runat="server" Text="Empaque externo" />
            </asp:TableCell><asp:TableCell BorderWidth="1px" Width="55px">
                <asp:Label ID="lblQtyIntPack" runat="server" Text="Cantidad" Width="55px" />
            </asp:TableCell><asp:TableCell BorderWidth="1px" Width="200px">
                <asp:Label ID="lblIntPack" runat="server" Text="Empaque interno" />
            </asp:TableCell><asp:TableCell BorderWidth="1px" Width="55px">
                <asp:Label ID="lblQtyContent" runat="server" Text="Cantidad" Width="55px" />
            </asp:TableCell><asp:TableCell BorderWidth="1px" Width="200px">
                <asp:Label ID="lblContent" runat="server" Text="Contenido del producto" />
            </asp:TableCell><asp:TableCell BorderWidth="1px" Width="55px">
                <asp:Label ID="lblQtyWeight" runat="server" Text="Cantidad" Width="55px" />
            </asp:TableCell><asp:TableCell BorderWidth="1px" Width="200px">
                <asp:Label ID="lblWeight" runat="server" Text="Concentración del producto" />
            </asp:TableCell></asp:TableRow><asp:TableRow BorderWidth="1px">
            <asp:TableCell BorderWidth="1px" Width="55px">
                <asp:TextBox ID="txtQtyExtPack" runat="server" Width="55px" />
            </asp:TableCell><asp:TableCell BorderWidth="1px" Width="200px">
                <asp:DropDownList ID="ddlExternalPacks" runat="server" Width="200px" />
            </asp:TableCell><asp:TableCell BorderWidth="1px" Width="55px">
                <asp:TextBox ID="txtQtyIntPack" runat="server" Width="55px" />
            </asp:TableCell><asp:TableCell BorderWidth="1px" Width="200px">
                <asp:DropDownList ID="ddlInternalPacks" runat="server" Width="200px" />
            </asp:TableCell><asp:TableCell BorderWidth="1px" Width="55px">
                <asp:TextBox ID="txtQtyContentUnit" runat="server" Width="55px" />
            </asp:TableCell><asp:TableCell BorderWidth="1px" Width="200px">
                <asp:DropDownList ID="ddlContentUnits" runat="server" Width="200px" />
            </asp:TableCell><asp:TableCell BorderWidth="1px" Width="55px">
                <asp:TextBox ID="txtQtyWeightUnit" runat="server" Width="55px" />
            </asp:TableCell><asp:TableCell BorderWidth="1px" Width="200px">
                <asp:DropDownList ID="ddlWeightUnits" runat="server" Width="200px" />
            </asp:TableCell></asp:TableRow><asp:TableRow>
            <asp:TableCell>
            <asp:Button ID="btnAddNewPresentation" runat="server" Text="Agregar presentación" OnClick="btnAddNewPresentation_Click" />
            </asp:TableCell></asp:TableRow></asp:Table><br />
    <br />

    <table cellspacing="25px">
        <tr>
            <td>
                <asp:Label ID="lblProductPresentations" runat="server" Font-Names="Trebuchet MS" Font-Size="Medium" Font-Bold="true" Text="Presentaciones asociadas para esta edición" Visible="false" />
                <br />
                <asp:GridView ID="grdProductPresentations" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" 
                    Font-Names="Trebuchet MS" HorizontalAlign="Left" Font-Size="Smaller" OnRowDataBound="grdProductPresentations_RowDataBound"
                    DataKeyNames="PresentationId,ProductId,PharmaFormId,DivisionId,CategoryId,ExternalPackId,InternalPackId,ContentUnitId,WeightUnitId" 
                    OnRowEditing="grdProductPresentations_RowEditing" OnRowUpdating="grdProductPresentations_RowUpdating">
                    <Columns>
                        <asp:BoundField DataField="PresentationId" Visible="False" />
                        <asp:BoundField DataField="ProductId" Visible="False" />
                        <asp:BoundField DataField="PharmaFormId" Visible="False" />
                        <asp:BoundField DataField="DivisionId" Visible="False" />
                        <asp:BoundField DataField="CategoryId" Visible="False" />
                        <asp:BoundField DataField="ExternalPackId" Visible="False" />
                        <asp:BoundField DataField="InternalPackId" Visible="False" />
                        <asp:BoundField DataField="ContentUnitId" Visible="False" />
                        <asp:BoundField DataField="WeightUnitId" Visible="False" />
                        <asp:TemplateField HeaderText="Nombre">
                            <HeaderStyle HorizontalAlign="Left" BackColor="#2694B4" ForeColor="White" />
                            <ItemStyle  Width="100px" />
                            <ItemTemplate>
                                <asp:Label ID="lblProdName" runat="server">
                                    <%# DataBinder.Eval(Container.DataItem, "Brand") %>
                                </asp:Label></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="Forma Farmacéutica">
                            <HeaderStyle HorizontalAlign="Left" BackColor="#2694B4" ForeColor="White" />
                            <ItemStyle  Width="100px" />
                            <ItemTemplate>
                                <asp:Label ID="lblPharmaForm" runat="server">
                                    <%# DataBinder.Eval(Container.DataItem, "PharmaForm")%>
                                </asp:Label></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="Laboratorio">
                            <HeaderStyle HorizontalAlign="Left" BackColor="#2694B4" ForeColor="White" />
                            <ItemStyle  Width="100px" />
                            <ItemTemplate>
                                <asp:Label ID="lblDivisionName" runat="server">
                                    <%# DataBinder.Eval(Container.DataItem, "DivisionShortName")%>
                                </asp:Label></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="Ediciones">
                            <HeaderStyle HorizontalAlign="Left" BackColor="#2694B4" ForeColor="White" />
                            <ItemStyle  Width="100px" />
                            <ItemTemplate>
                                <asp:Label ID="lblEditions" runat="server">
                                    <%# DataBinder.Eval(Container.DataItem, "Editions")%>
                                </asp:Label></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="Cantidad paquete externo">
                            <HeaderStyle HorizontalAlign="Left" BackColor="#2694B4" ForeColor="White" />
                            <ItemStyle  Width="100px" />
                            <ItemTemplate>
                                <asp:Label ID="lblQtyExternalPack" runat="server">
                                    <%# DataBinder.Eval(Container.DataItem, "QtyExternalPack")%>
                                </asp:Label></ItemTemplate><EditItemTemplate>
                                <asp:TextBox ID="txtQtyExternalPack" runat="server" CssClass="label" Text='<%# displayQtyExternalPack(Convert.ToString(DataBinder.Eval(Container.DataItem, "QtyExternalPack"))) %>'
                                    Width="100px" />
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Paquete externo">
                            <HeaderStyle HorizontalAlign="Left" BackColor="#2694B4" ForeColor="White" />
                            <ItemStyle  Width="100px" />
                            <ItemTemplate>
                                <asp:Label ID="lblExternalPackName" runat="server">
                                    <%# DataBinder.Eval(Container.DataItem, "ExternalPackName")%>
                                </asp:Label></ItemTemplate><EditItemTemplate>
                                <asp:DropDownList ID="ddlExternalPack" runat="server" CssClass="label" Width="100px" />
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Cantidad paquete interno">
                            <HeaderStyle HorizontalAlign="Left" BackColor="#2694B4" ForeColor="White" />
                            <ItemStyle  Width="100px" />
                            <ItemTemplate>
                                <asp:Label ID="lblQtyInternalPack" runat="server">
                                    <%# DataBinder.Eval(Container.DataItem, "QtyInternalPack")%>
                                </asp:Label></ItemTemplate><EditItemTemplate>
                                <asp:TextBox ID="txtQtyInternalPack" runat="server" CssClass="label" Text='<%# displayQtyInternalPack(Convert.ToString(DataBinder.Eval(Container.DataItem, "QtyInternalPack"))) %>'
                                    Width="100px" />
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Paquete interno">
                            <HeaderStyle HorizontalAlign="Left" BackColor="#2694B4" ForeColor="White" />
                            <ItemStyle  Width="100px" />
                            <ItemTemplate>
                                <asp:Label ID="lblInternalPackName" runat="server">
                                    <%# DataBinder.Eval(Container.DataItem, "InternalPackName")%>
                                </asp:Label></ItemTemplate><EditItemTemplate>
                                <asp:DropDownList ID="ddlInternalPack" runat="server" CssClass="label" Width="100px" />
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Cantidad de producto">
                            <HeaderStyle HorizontalAlign="Left" BackColor="#2694B4" ForeColor="White" />
                            <ItemStyle  Width="100px" />
                            <ItemTemplate>
                                <asp:Label ID="lblQtyContentUnit" runat="server">
                                    <%# DataBinder.Eval(Container.DataItem, "QtyContentUnit")%>
                                </asp:Label></ItemTemplate><EditItemTemplate>
                                <asp:TextBox ID="txtQtyContentUnit" runat="server" CssClass="label" Text='<%# displayQtyContentUnit(Convert.ToString(DataBinder.Eval(Container.DataItem, "QtyContentUnit"))) %>'
                                    Width="100px" />
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Contenido de producto">
                            <HeaderStyle HorizontalAlign="Left" BackColor="#2694B4" ForeColor="White" />
                            <ItemStyle  Width="100px" />
                            <ItemTemplate>
                                <asp:Label ID="lblContentUnitName" runat="server">
                                    <%# DataBinder.Eval(Container.DataItem, "ContentUnitName")%>
                                </asp:Label></ItemTemplate><EditItemTemplate>
                                <asp:DropDownList ID="ddlContentUnitName" runat="server" CssClass="label" Width="100px" />
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Cantidad de concentración de producto">
                            <HeaderStyle HorizontalAlign="Left" BackColor="#2694B4" ForeColor="White" />
                            <ItemStyle  Width="100px" />
                            <ItemTemplate>
                                <asp:Label ID="lblQtyWeightUnit" runat="server">
                                    <%# DataBinder.Eval(Container.DataItem, "QtyWeightUnit")%>
                                </asp:Label></ItemTemplate><EditItemTemplate>
                                <asp:TextBox ID="txtQtyWeightUnit" runat="server" CssClass="label" Text='<%# displayQtyWeightUnit(Convert.ToString(DataBinder.Eval(Container.DataItem, "QtyWeightUnit"))) %>'
                                    Width="100px" />
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Concentración de producto">
                            <HeaderStyle HorizontalAlign="Left" BackColor="#2694B4" ForeColor="White" />
                            <ItemStyle  Width="100px" />
                            <ItemTemplate>
                                <asp:Label ID="lblWeightUnitName" runat="server">
                                    <%# DataBinder.Eval(Container.DataItem, "WeightUnitName")%>
                                </asp:Label></ItemTemplate><EditItemTemplate>
                                <asp:DropDownList ID="ddlWeightUnitName" runat="server" CssClass="label" Width="100px" />
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Eliminar presentación de esta edición">
                            <HeaderStyle Width="100px" HorizontalAlign="Center" BackColor="#2694B4" ForeColor="White" />
                            <ItemStyle HorizontalAlign="Center" Wrap="True" />
                            <ItemTemplate>
                                <asp:ImageButton ID="btnDeletePresentation" runat="server" CommandName="Delete" ImageUrl="~/Images/delete.gif" AlternateText="Eliminar presentación de esta edición" 
                                    Enabled="true" OnClick="btnDeletePresentation_OnClick" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Editar Presentación">
                            <HeaderStyle Width="45px" HorizontalAlign="Center" BackColor="#2694B4" ForeColor="White" />
                            <ItemStyle HorizontalAlign="Center" Wrap="True" />
                            <ItemTemplate>
                                <asp:ImageButton ID="btnEditPresentations" runat="server" CommandName="Edit" ImageUrl="~/Images/editar.gif" AlternateText="Editar presentación" Enabled="true" />
                            </ItemTemplate>
                            <EditItemTemplate >
                                <asp:ImageButton ID="btnSavePresentation" runat="server" CommandName="Update" ImageUrl="~/Images/salvar.jpg" AlternateText="Guardar presentación" />
                            </EditItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblPresentations" runat="server" Font-Names="Trebuchet MS" Font-Size="Medium" Font-Bold="true" Text="Presentaciones existentes para este producto" Visible="false" />
                <br />
                <asp:GridView ID="grdPresentationCatalog" runat="server" AutoGenerateColumns="False" 
                                    BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" 
                    CellPadding="3" Font-Names="Trebuchet MS" OnRowDataBound="grdPresentationCatalog_RowDataBound"
                    HorizontalAlign="Left" Font-Size="Smaller" 
                                    DataKeyNames="PresentationId,ProductId,PharmaFormId,DivisionId,CategoryId,ExternalPackId,InternalPackId,ContentUnitId,WeightUnitId" 
                                    onrowediting="grdPresentationCatalog_RowEditing" 
                                    onrowupdating="grdPresentationCatalog_RowUpdating" ><Columns>
                        <asp:BoundField DataField="PresentationId" Visible="False" />
                        <asp:BoundField DataField="ProductId" Visible="False" />
                        <asp:BoundField DataField="PharmaFormId" Visible="False" />
                        <asp:BoundField DataField="DivisionId" Visible="False" />
                        <asp:BoundField DataField="CategoryId" Visible="False" />
                        <asp:BoundField DataField="ExternalPackId" Visible="False" />
                        <asp:BoundField DataField="InternalPackId" Visible="False" />
                        <asp:BoundField DataField="ContentUnitId" Visible="False" />
                        <asp:BoundField DataField="WeightUnitId" Visible="False" />
                        <asp:TemplateField HeaderText="Nombre">
                            <HeaderStyle HorizontalAlign="Left" BackColor="#2694B4" ForeColor="White" />
                            <ItemStyle  Width="100px" />
                            <ItemTemplate>
                                <asp:Label ID="lblProdName" runat="server">
                                    <%# DataBinder.Eval(Container.DataItem, "Brand") %>
                                </asp:Label></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="Forma Farmacéutica">
                            <HeaderStyle HorizontalAlign="Left" BackColor="#2694B4" ForeColor="White" />
                            <ItemStyle  Width="100px" />
                            <ItemTemplate>
                                <asp:Label ID="lblPharmaForm" runat="server">
                                    <%# DataBinder.Eval(Container.DataItem, "PharmaForm")%>
                                </asp:Label></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="Laboratorio">
                            <HeaderStyle HorizontalAlign="Left" BackColor="#2694B4" ForeColor="White" />
                            <ItemStyle  Width="100px" />
                            <ItemTemplate>
                                <asp:Label ID="lblDivisionName" runat="server">
                                    <%# DataBinder.Eval(Container.DataItem, "DivisionShortName")%>
                                </asp:Label></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="Ediciones">
                            <HeaderStyle HorizontalAlign="Left" BackColor="#2694B4" ForeColor="White" />
                            <ItemStyle  Width="100px" />
                            <ItemTemplate>
                                <asp:Label ID="lblEdition" runat="server">
                                    <%# DataBinder.Eval(Container.DataItem, "Editions")%>
                                </asp:Label></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="Cantidad paquete externo">
                            <HeaderStyle HorizontalAlign="Left" BackColor="#2694B4" ForeColor="White" />
                            <ItemStyle  Width="100px" />
                            <ItemTemplate>
                                <asp:Label ID="lblQtyExternalPack" runat="server">
                                    <%# DataBinder.Eval(Container.DataItem, "QtyExternalPack")%>
                                </asp:Label></ItemTemplate><EditItemTemplate>
                                <asp:TextBox ID="txtQtyExternalPack" runat="server" CssClass="label" Text='<%# displayQtyExternalPack(Convert.ToString(DataBinder.Eval(Container.DataItem, "QtyExternalPack"))) %>'
                                    Width="100px" />
                            </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Paquete externo">
                            <HeaderStyle HorizontalAlign="Left" BackColor="#2694B4" ForeColor="White" />
                            <ItemStyle  Width="100px" />
                            <ItemTemplate>
                                <asp:Label ID="lblExternalPackName" runat="server">
                                    <%# DataBinder.Eval(Container.DataItem, "ExternalPackName")%>
                                </asp:Label></ItemTemplate><EditItemTemplate>
                                <asp:DropDownList ID="ddlExternalPack" runat="server" CssClass="label" Width="100px" />
                            </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Cantidad paquete interno">
                            <HeaderStyle HorizontalAlign="Left" BackColor="#2694B4" ForeColor="White" />
                            <ItemStyle  Width="100px" />
                            <ItemTemplate>
                                <asp:Label ID="lblQtyInternalPack" runat="server">
                                    <%# DataBinder.Eval(Container.DataItem, "QtyInternalPack")%>
                                </asp:Label></ItemTemplate><EditItemTemplate>
                                <asp:TextBox ID="txtQtyInternalPack" runat="server" CssClass="label" Text='<%# displayQtyInternalPack(Convert.ToString(DataBinder.Eval(Container.DataItem, "QtyInternalPack"))) %>'
                                    Width="100px" />
                            </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Paquete interno">
                            <HeaderStyle HorizontalAlign="Left" BackColor="#2694B4" ForeColor="White" />
                            <ItemStyle  Width="100px" />
                            <ItemTemplate>
                                <asp:Label ID="lblInternalPackName" runat="server">
                                    <%# DataBinder.Eval(Container.DataItem, "InternalPackName")%>
                                </asp:Label></ItemTemplate><EditItemTemplate>
                                <asp:DropDownList ID="ddlInternalPack" runat="server" CssClass="label" Width="100px" />
                            </EditItemTemplate>
                                </asp:TemplateField>
                                
                                
                                <asp:TemplateField HeaderText="Cantidad de producto">
                            <HeaderStyle HorizontalAlign="Left" BackColor="#2694B4" ForeColor="White" />
                            <ItemStyle  Width="100px" />
                            <ItemTemplate>
                                <asp:Label ID="lblQtyContentUnit" runat="server">
                                    <%# DataBinder.Eval(Container.DataItem, "QtyContentUnit")%>
                                </asp:Label></ItemTemplate><EditItemTemplate>
                                <asp:TextBox ID="txtQtyContentUnit" runat="server" CssClass="label" Text='<%# displayQtyContentUnit(Convert.ToString(DataBinder.Eval(Container.DataItem, "QtyContentUnit"))) %>'
                                    Width="100px" /></EditItemTemplate>
                                </asp:TemplateField>
                               
                                <asp:TemplateField HeaderText="Contenido de producto">
                            <HeaderStyle HorizontalAlign="Left" BackColor="#2694B4" ForeColor="White" />
                            <ItemStyle  Width="100px" />
                            <ItemTemplate>
                                <asp:Label ID="lblContentUnitName" runat="server">
                                    <%# DataBinder.Eval(Container.DataItem, "ContentUnitName")%>
                                </asp:Label></ItemTemplate><EditItemTemplate>
                                <asp:DropDownList ID="ddlContentUnitName" runat="server" CssClass="label" Width="100px" />
                            </EditItemTemplate>
                                </asp:TemplateField>
                                
                                <asp:TemplateField HeaderText="Cantidad de concentración de producto">
                            <HeaderStyle HorizontalAlign="Left" BackColor="#2694B4" ForeColor="White" />
                            <ItemStyle  Width="100px" />
                            <ItemTemplate>
                                <asp:Label ID="lblQtyWeightUnit" runat="server">
                                    <%# DataBinder.Eval(Container.DataItem, "QtyWeightUnit")%>
                                </asp:Label></ItemTemplate><EditItemTemplate>
                                <asp:TextBox ID="txtQtyWeightUnit" runat="server" CssClass="label" Text='<%# displayQtyWeightUnit(Convert.ToString(DataBinder.Eval(Container.DataItem, "QtyWeightUnit"))) %>'
                                    Width="100px" />
                            </EditItemTemplate>
                                </asp:TemplateField>
                                
                                <asp:TemplateField HeaderText="Concentración de producto">
                            <HeaderStyle HorizontalAlign="Left" BackColor="#2694B4" ForeColor="White" />
                            <ItemStyle  Width="100px" />
                            <ItemTemplate>
                                <asp:Label ID="lblWeightUnitName" runat="server">
                                    <%# DataBinder.Eval(Container.DataItem, "WeightUnitName")%>
                                </asp:Label></ItemTemplate><EditItemTemplate>
                                <asp:DropDownList ID="ddlWeightUnitName" runat="server" CssClass="label" Width="100px" />
                            </EditItemTemplate>
                                </asp:TemplateField>
                                
                                <asp:TemplateField HeaderText="Agregar a la edición">
                            <HeaderStyle Width="100px" HorizontalAlign="Center" BackColor="#2694B4" ForeColor="White" />
                            <ItemStyle HorizontalAlign="Center" Wrap="True" />
                            <ItemTemplate>
                                <asp:ImageButton ID="btnAddPresentationToEdition" runat="server" CommandName="Add" ImageUrl="~/Images/agregar.png" AlternateText="Agregar a la edición" 
                                    Enabled="true" Width="20px" OnClick="btnAddPresentationToEdition_OnClick" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Editar Presentación">
                            <HeaderStyle Width="45px" HorizontalAlign="Center" BackColor="#2694B4" ForeColor="White" />
                            <ItemStyle HorizontalAlign="Center" Wrap="True" />
                            <ItemTemplate>
                                <asp:ImageButton ID="btnEditPresentations" runat="server" CommandName="Edit" ImageUrl="~/Images/editar.gif" AlternateText="Editar presentación" Enabled="true" />
                            </ItemTemplate>
                            <EditItemTemplate >
                                <asp:ImageButton ID="btnSavePresentation" runat="server" CommandName="Update" ImageUrl="~/Images/salvar.jpg" AlternateText="Guardar presentación" />
                            </EditItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
    </table>
</asp:Content>