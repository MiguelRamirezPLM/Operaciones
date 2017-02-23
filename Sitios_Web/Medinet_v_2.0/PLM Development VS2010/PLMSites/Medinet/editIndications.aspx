<%@ Page Language="C#" MasterPageFile="~/medinet.master" AutoEventWireup="true" CodeFile="editIndications.aspx.cs" Inherits="editIndications" %>
<%@ MasterType VirtualPath="~/medinet.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <script type="text/javascript">

        function confirmDelete(deleteName) {
            var agree = confirm('Has elegido eliminar a: ' + deleteName + ', ¿Confirmas que deseas borrar esa indicación?');
            if (agree)
                return true;
            else
                return false;
        }

        function confirmSubstitute(substituteName) {
            var agree = confirm('Has elegido como Indicación sustituto a: ' + substituteName + ', ¿Confirmas que esa indicación es correcta?');
            if (agree)
                return true;
            else
                return false;
        }
        
    </script>
    
    <table cellspacing="3" cellpadding="3" border="0">
        <tr>
            <td>
                <asp:ImageButton ID="imgBtnBackProducts" runat="server" ImageUrl="images/regresar.png" ToolTip="Regresar a productos" OnClick="imgBtnBackProducts_Click" />
            </td>
        </tr>
    </table>

    <br />
    <br />

    <table cellspacing="25px" width="100%">
        <tr>
            <td valign="top">
                <table width="100%">
                    <tr>
                        <td valign="top">
                            <asp:Label ID="lblTitle" runat="server" Text="Agregar nueva indicación" Font-Names="Trebuchet MS" Font-Size="Medium" Font-Bold="true" />
                            <br />

                            <asp:Table ID="Table1" runat="server" >
                                <asp:TableRow BorderWidth="1px" Font-Names="Trebuchet MS" Font-Size="Smaller" Font-Bold="true">
                                    <asp:TableCell BorderWidth="1px">
                                        <asp:Label ID="lblNewIndicationName" runat="server" Text="Nombre de la indicación" />
                                    </asp:TableCell><asp:TableCell BorderWidth="1px" Width="55px">
                
                                    </asp:TableCell></asp:TableRow><asp:TableRow BorderWidth="1px">
                                    <asp:TableCell BorderWidth="1px">
                                        <asp:TextBox ID="txtNewIndicationName" runat="server" Width="350px" />
                                    </asp:TableCell><asp:TableCell BorderWidth="1px" Width="55px">
                                        <asp:Button ID="btnAddNewIndication" runat="server" Text="Agregar Indicación" OnClick="btnAddNewIndication_Click" />
                                    </asp:TableCell></asp:TableRow></asp:Table></td></tr><tr>
                        <td>
                            <table cellspacing="25px">
                                <tr>
                                    <td valign="top">
                                        <asp:Panel ID="Panel1" runat="server" DefaultButton="btnSearchIndication">
                                            <asp:Label ID="Label13" runat="server" Font-Names="Trebuchet MS" Font-Size="Large" Font-Bold="true" Text="Catálogo de indicaciones" />
                                            <asp:Table ID="Table4" runat="server" Width="250px">
                                                <asp:TableRow>
                                                    <asp:TableCell HorizontalAlign="Left">
                                                        <asp:Label ID="Label14" runat="server" Text="Buscar una indicación" Font-Names="Trebuchet MS" Font-Size="Smaller" Font-Bold="true" />
                                                    </asp:TableCell><asp:TableCell HorizontalAlign="Right">
                                                        <asp:Label ID="Label15" runat="server" Text="Registros por página" Font-Names="Trebuchet MS" Font-Size="Smaller" Font-Bold="true" />
                                                    </asp:TableCell></asp:TableRow><asp:TableRow>
                                                    <asp:TableCell HorizontalAlign="Left">
                                                        <asp:TextBox ID="txtSearchIndication" runat="server" Text="" Enabled="true" />
                                                        <asp:Button ID="btnSearchIndication" runat="server" ToolTip="Consultar" 
                                                            Text="Consultar" OnClick="btnSearchIndication_Click" />
                                                    </asp:TableCell><asp:TableCell HorizontalAlign="Right">
                                                        <asp:DropDownList ID="ddlPageSize" runat="server" AutoPostBack="True" Font-Names="Arial" Font-Size="10pt" OnSelectedIndexChanged="ddlPageSize_SelectedIndexChanged">
                                                            <asp:ListItem Value="-1" >Ilimitado</asp:ListItem>
                                                            <asp:ListItem Value="5">5</asp:ListItem>
                                                            <asp:ListItem Value="15">15</asp:ListItem>
                                                            <asp:ListItem Value="25" Selected="True">25</asp:ListItem>
                                                            <asp:ListItem Value="50">50</asp:ListItem>
                                                            <asp:ListItem Value="100">100</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </asp:TableCell></asp:TableRow></asp:Table></asp:Panel><asp:GridView ID="grdIndications" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" 
                                            CellPadding="3" Font-Names="Trebuchet MS" HorizontalAlign="Left" Font-Size="Smaller" DataKeyNames="IndicationId" OnRowDataBound="grdIndications_RowDataBound"
                                            OnPageIndexChanging="grdIndications_PageIndexChanging" OnRowEditing="grdIndications_RowEditing" OnRowUpdating="grdIndications_RowUpdating">
                    
                                            <Columns>
                                                <asp:BoundField DataField="IndicationId" Visible="False" />
                                                <asp:TemplateField HeaderText="Nombre de la indicación">
                                                    <HeaderStyle HorizontalAlign="Left" BackColor="#2694B4" ForeColor="White" />
                                                    <ItemStyle  Width="450px" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblIndicationName" runat="server">
                                                            <%# DataBinder.Eval(Container.DataItem, "Description")%>
                                                        </asp:Label></ItemTemplate><EditItemTemplate>
                                                        <asp:TextBox ID="txtIndicationName" runat="server" CssClass="label" 
                                                            Text='<%# displayIndicationName(Convert.ToString(DataBinder.Eval(Container.DataItem, "Description"))) %>' Width="450px" />
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Editar Indicación">
                                                    <HeaderStyle Width="45px" HorizontalAlign="Center" BackColor="#2694B4" ForeColor="White" />
                                                    <ItemStyle HorizontalAlign="Center" Wrap="True" />
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="btnEditIndications" runat="server" CommandName="Edit" ImageUrl="~/Images/editar.gif" AlternateText="Editar Indicación" Enabled="true" />
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:ImageButton ID="btnSaveIndications" runat="server" CommandName="Update" ImageUrl="~/Images/salvar.jpg" AlternateText="Guardar Indicación" />
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                                <%--<asp:TemplateField HeaderText="Eliminar indicación del catálogo">
                                                    <HeaderStyle Width="100px" HorizontalAlign="Center" BackColor="#2694B4" ForeColor="White" />
                                                    <ItemStyle HorizontalAlign="Center" Wrap="True" />
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="btnDeleteIndication" runat="server" ImageUrl="~/Images/delete.gif" AlternateText="Eliminar indicación" Enabled="true" 
                                                            OnClick="btnDeleteIndication_OnClick" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
                                            </Columns>
                                        </asp:GridView>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>

            <td id="tdExistsIndications" runat="server" valign="top" visible="false">
                <table>
                    <tr>
                        <td>
                            <asp:Label ID="Label1" runat="server" Text="Se encontraron las siguientes coincidencias: " Font-Names="Trebuchet MS" Font-Size="Medium" Font-Bold="true" />
                            <br />

                            <asp:GridView ID="grdExistsIndications" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" 
                                CellPadding="3" Font-Names="Trebuchet MS" HorizontalAlign="Left" Font-Size="Smaller" DataKeyNames="IndicationId">
                                <Columns>
                                    <asp:BoundField DataField="IndicationId" Visible="False" />

                                    <asp:TemplateField HeaderText="Nombre de la indicación">
                                        <HeaderStyle HorizontalAlign="Left" BackColor="#2694B4" ForeColor="White" />
                                        <ItemStyle  Width="450px" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblIndicationName" runat="server">
                                                <%# DataBinder.Eval(Container.DataItem, "Description")%>
                                            </asp:Label></ItemTemplate></asp:TemplateField></Columns></asp:GridView></td></tr><tr>
                        <td>
                            <table width="100%">
                                <tr>
                                    <td>
                                        <br />
                                        <asp:Label ID="Label4" runat="server" Text="¿Confirmas que deseas agregar la indicación?" Width="150px" Font-Names="Trebuchet MS" Font-Size="Medium" Font-Bold="true" />
                                        <br />
                                        <asp:Button ID="btnConfirmIndication" runat="server" Text="Agregar Indicación" OnClick="btnConfirmIndication_Click" />
                                    </td>
                                    <td>
                                        <br />
                                        <asp:Label ID="Label5" runat="server" Text="Cancelar operación." Width="150px" Font-Names="Trebuchet MS" Font-Size="Medium" Font-Bold="true" />
                                        <br />
                                        <asp:Button ID="btnCancelIndication" runat="server" Text="Cancelar" OnClick="btnCancelIndication_Click" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>

    <br />
    <br />

    <table cellspacing="25px">
        <tr>
            <td id="tdConfirmSubstitute" runat="server" valign="top" visible="false">
                <asp:Label ID="Label6" runat="server" Font-Names="Trebuchet MS" Font-Size="Medium" Font-Bold="true" Text="Indicación: " />
                <asp:Label ID="lblIndicationToDelete" runat="server" Font-Size="Medium" />

                <br />
                <br />

                <asp:Label ID="Label8" runat="server" Font-Names="Trebuchet MS" Font-Size="Small" Font-Bold="true" Text="La Indicación que elegiste tiene productos asociados." />
                <br />
                <asp:Label ID="Label9" runat="server" Font-Names="Trebuchet MS" Font-Size="Small" Font-Bold="true" Text="¿Confirmas que deseas eliminarla?" />
                <br />
                <asp:Button ID="btnConfirmDelete" runat="server" Text="Sí, Borrar" OnClick="btnConfirmDelete_OnClick" /> 
                <asp:Button ID="btnCancelDelete" runat="server" Text="No, Cancelar" OnClick="btnCancelDelete_OnClick" />
                <br />
                <br />
                <asp:Label ID="Label10" runat="server" Font-Names="Trebuchet MS" Font-Size="Small" Font-Bold="true" Text="¿O prefieres elegir una indicación sustituto?" />
                <br />
                <br />
                <asp:Table ID="Table3" runat="server" Width="250px">
                    <asp:TableRow>
                        <asp:TableCell HorizontalAlign="Left">
                            <asp:Label ID="Label11" runat="server" Text="Buscar una indicación" Font-Names="Trebuchet MS" Font-Size="Smaller" Font-Bold="true" />
                        </asp:TableCell><asp:TableCell HorizontalAlign="Right">
                            <asp:Label ID="Label12" runat="server" Text="Registros por página" Font-Names="Trebuchet MS" Font-Size="Smaller" Font-Bold="true" />
                        </asp:TableCell></asp:TableRow><asp:TableRow>
                        <asp:TableCell HorizontalAlign="Left">
                            <asp:TextBox ID="txtSubstituteIndication" runat="server" Text="" Enabled="true" />
                            <asp:Button ID="btnSearchSubstitute" runat="server" ToolTip="Consultar" Text="Consultar" OnClick="btnSearchSubstitute_Click" />
                        </asp:TableCell><asp:TableCell HorizontalAlign="Right">
                            <asp:DropDownList ID="ddlPageSubstitute" runat="server" AutoPostBack="True" Font-Names="Arial" Font-Size="10pt" OnSelectedIndexChanged="ddlPageSubstitute_SelectedIndexChanged">
                                <asp:ListItem Value="-1" >Ilimitado</asp:ListItem>
                                <asp:ListItem Value="5">5</asp:ListItem>
                                <asp:ListItem Value="15">15</asp:ListItem>
                                <asp:ListItem Value="25" Selected="True">25</asp:ListItem>
                                <asp:ListItem Value="50">50</asp:ListItem>
                                <asp:ListItem Value="100">100</asp:ListItem>
                            </asp:DropDownList>
                        </asp:TableCell></asp:TableRow></asp:Table><asp:GridView ID="grdSubstituteIndications" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" 
                    CellPadding="3" Font-Names="Trebuchet MS" HorizontalAlign="Left" Font-Size="Smaller" DataKeyNames="IndicationId" OnRowDataBound="grdSubstituteIndications_RowDataBound"
                    OnPageIndexChanging="grdSubstituteIndications_PageIndexChanging" >
                    <Columns>
                        <asp:BoundField DataField="IndicationId" Visible="False" />
                        <asp:TemplateField HeaderText="Nombre de la indicación">
                            <HeaderStyle HorizontalAlign="Left" BackColor="#2694B4" ForeColor="White" />
                            <ItemStyle  Width="350px" />
                            <ItemTemplate>
                                <asp:Label ID="lblIndicationName" runat="server">
                                    <%# DataBinder.Eval(Container.DataItem, "Description")%>
                                </asp:Label></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="Agregar indicación sustituto">
                            <HeaderStyle Width="100px" HorizontalAlign="Center" BackColor="#2694B4" ForeColor="White" />
                            <ItemStyle HorizontalAlign="Center" Wrap="True" />
                            <ItemTemplate>
                                <asp:ImageButton ID="btnReplaceIndication" runat="server" CommandName="Add" ImageUrl="~/Images/agregar.png" AlternateText="Agregar a productos" 
                                    Enabled="true" Width="20px" OnClick="btnReplaceIndication_OnClick" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
    </table>

</asp:Content>

