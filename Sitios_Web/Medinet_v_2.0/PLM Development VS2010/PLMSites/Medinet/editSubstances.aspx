<%@ Page Language="C#" MasterPageFile="~/medinet.master" AutoEventWireup="true" CodeFile="editSubstances.aspx.cs" Inherits="editSubstances" %>
<%@ MasterType VirtualPath="~/medinet.master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">

    <script type="text/javascript">

        function confirmDelete(deleteName) {
            var agree = confirm('Has elegido eliminar a: ' + deleteName + ', ¿Confirmas que deseas borrar esa sustancia?');
            if (agree)
                return true;
            else
                return false;
        }

        function confirmSubstitute(substituteName) {
            var agree = confirm('Has elegido como sustancia sustituto a: ' + substituteName + ', ¿Confirmas que esa sustancia es correcta?');
            if (agree)
                return true;
            else
                return false;
        }

    </script>

    <cc1:AutoCompleteExtender ID="autoCompleteSubstances" runat="server" Enabled="true"
        TargetControlID="txtNewSubstanceName" ServiceMethod="getSubstanceList"
        UseContextKey="True"></cc1:AutoCompleteExtender>

    <table cellspacing="3" cellpadding="3" border="0">
        <tr>
            <td>
                <asp:ImageButton ID="imgBtnBackProducts" runat="server" ImageUrl="images/regresar.png" ToolTip="Regresar a productos" OnClick="imgBtnBackProducts_Click" />
            </td>
        </tr>
    </table>

    <br />
    <br />

    <asp:Label ID="lblTitle" runat="server" Text="Agregar nueva sustancia" Font-Names="Trebuchet MS" Font-Size="Medium" Font-Bold="true" />
    <br />

    <asp:Table ID="Table1" runat="server" >
        <asp:TableRow BorderWidth="1px" Font-Names="Trebuchet MS" Font-Size="Smaller" Font-Bold="true">
            <asp:TableCell BorderWidth="1px">
                <asp:Label ID="lblSubstanceName" runat="server" Text="Nombre de la sustancia" />
            </asp:TableCell>
            <asp:TableCell BorderWidth="1px" >
                <asp:Label ID="lblEnglishSubstance" runat="server" Text="Nombre en inglés" />
            </asp:TableCell>
            <asp:TableCell BorderWidth="1px" Width="55px">
                
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow BorderWidth="1px">
            <asp:TableCell BorderWidth="1px">
                <asp:TextBox ID="txtNewSubstanceName" runat="server" Width="350px" />
            </asp:TableCell>
            <asp:TableCell BorderWidth="1px">
                <asp:TextBox ID="txtNewEnglishDescription" runat="server" Width="350px" />
            </asp:TableCell>
            <asp:TableCell BorderWidth="1px" Width="55px">
                <asp:Button ID="btnAddNewSubstance" runat="server" Text="Agregar Sustancia" OnClick="btnAddNewSubstance_Click" />
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>

    <br />
    <br />

    <table cellspacing="25px">
        <tr>
            <td valign="top">
                <asp:Panel ID="pnlSearch" runat="server" DefaultButton="btnSearchSubstance">
                    <asp:Label ID="Label7" runat="server" Font-Names="Trebuchet MS" Font-Size="Large" Font-Bold="true" Text="Catálogo de sustancias activas" />
                    <asp:Table ID="Table2" runat="server" Width="250px">
                        <asp:TableRow>
                            <asp:TableCell HorizontalAlign="Left">
                                <asp:Label ID="Label2" runat="server" Text="Buscar una sustancia" Font-Names="Trebuchet MS" Font-Size="Smaller" Font-Bold="true" />
                            </asp:TableCell>
                            <asp:TableCell HorizontalAlign="Right">
                                <asp:Label ID="Label3" runat="server" Text="Registros por página" Font-Names="Trebuchet MS" Font-Size="Smaller" Font-Bold="true" />
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell HorizontalAlign="Left">
                                <asp:TextBox ID="txtSearchSubstance" runat="server" Text="" Enabled="true" />
                                <asp:Button ID="btnSearchSubstance" runat="server" ToolTip="Consultar" 
                                    Text="Consultar" OnClick="btnSearchSubstance_Click" />
                            </asp:TableCell>
                            <asp:TableCell HorizontalAlign="Right">
                                <asp:DropDownList ID="ddlPageSize" runat="server" AutoPostBack="True" Font-Names="Arial" Font-Size="10pt" OnSelectedIndexChanged="ddlPageSize_SelectedIndexChanged">
                                    <asp:ListItem Value="-1" >Ilimitado</asp:ListItem>
                                    <asp:ListItem Value="5">5</asp:ListItem>
                                    <asp:ListItem Value="15">15</asp:ListItem>
                                    <asp:ListItem Value="25" Selected="True">25</asp:ListItem>
                                    <asp:ListItem Value="50">50</asp:ListItem>
                                    <asp:ListItem Value="100">100</asp:ListItem>
                                </asp:DropDownList>
                            </asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                </asp:Panel>
    
                <asp:GridView ID="grdSubstances" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" 
                    CellPadding="3" Font-Names="Trebuchet MS" HorizontalAlign="Left" Font-Size="Smaller" DataKeyNames="ActiveSubstanceId" OnRowDataBound="grdSubstances_RowDataBound"
                    OnPageIndexChanging="grdSubstances_PageIndexChanging" OnRowEditing="grdSubstances_RowEditing" OnRowUpdating="grdSubstances_RowUpdating">
                    <Columns>
                        <asp:BoundField DataField="ActiveSubstanceId" Visible="False" />
                        <asp:TemplateField HeaderText="Nombre de la sustancia activa">
                            <HeaderStyle HorizontalAlign="Left" BackColor="#2694B4" ForeColor="White" />
                            <ItemStyle  Width="350px" />
                            <ItemTemplate>
                                <asp:Label ID="lblSubstanceName" runat="server">
                                    <%# DataBinder.Eval(Container.DataItem, "Description")%>
                                </asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtSubstanceName" runat="server" CssClass="label" Text='<%# displaySubstanceName(Convert.ToString(DataBinder.Eval(Container.DataItem, "Description"))) %>'
                                    Width="350px" />
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Nombre en inglés">
                            <HeaderStyle HorizontalAlign="Left" BackColor="#2694B4" ForeColor="White" />
                            <ItemStyle  Width="200px" />
                            <ItemTemplate>
                                <asp:Label ID="lblSubstanceEnglishName" runat="server">
                                    <%# DataBinder.Eval(Container.DataItem, "EnglishDescription")%>
                                </asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtSubstanceEnglishName" runat="server" CssClass="label" Text='<%# displaySubstanceEnglishName(Convert.ToString(DataBinder.Eval(Container.DataItem, "EnglishDescription"))) %>'
                                    Width="200px" />
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Editar sustancia">
                            <HeaderStyle Width="45px" HorizontalAlign="Center" BackColor="#2694B4" ForeColor="White" />
                            <ItemStyle HorizontalAlign="Center" Wrap="True" />
                            <ItemTemplate>
                                <asp:ImageButton ID="btnEditSubstances" runat="server" CommandName="Edit" ImageUrl="~/Images/editar.gif" AlternateText="Editar sustancia" Enabled="true" />
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:ImageButton ID="btnSaveSubstance" runat="server" CommandName="Update" ImageUrl="~/Images/salvar.jpg" AlternateText="Guardar sustancia" />
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <%--<asp:TemplateField HeaderText="Eliminar sustancia del catálogo">
                            <HeaderStyle Width="100px" HorizontalAlign="Center" BackColor="#2694B4" ForeColor="White" />
                            <ItemStyle HorizontalAlign="Center" Wrap="True" />
                            <ItemTemplate>
                                <asp:ImageButton ID="btnDeleteSubstance" runat="server" ImageUrl="~/Images/delete.gif" AlternateText="Eliminar sustancia" Enabled="true" 
                                    OnClick="btnDeleteSubstance_OnClick" />
                            </ItemTemplate>
                        </asp:TemplateField>--%>
                    </Columns>
                </asp:GridView>
            </td>

            <td id="tdConfirmDelete" runat="server" valign="top" visible="false">
                
                <asp:Label ID="Label6" runat="server" Font-Names="Trebuchet MS" Font-Size="Medium" Font-Bold="true" 
                    Text="Sustancia: " />
                <asp:Label ID="lblSubstanceToDelete" runat="server" Font-Size="Medium" />

                <br />
                <br />

                <asp:Label ID="Label1" runat="server" Font-Names="Trebuchet MS" Font-Size="Small" Font-Bold="true" 
                    Text="La sustancia que elegiste tiene productos y/o interacciones asociadas." />

                <br />

                <asp:Label ID="Label4" runat="server" Font-Names="Trebuchet MS" Font-Size="Small" Font-Bold="true" 
                    Text="¿Confirmas que deseas eliminarla?" />

                <br />

                <asp:Button ID="btnConfirmDelete" runat="server" Text="Sí, Borrar" OnClick="btnConfirmDelete_OnClick" /> 
                <asp:Button ID="btnCancelDelete" runat="server" Text="No, Cancelar" OnClick="btnCancelDelete_OnClick" />

                <br />
                <br />

                <asp:Label ID="Label5" runat="server" Font-Names="Trebuchet MS" Font-Size="Small" Font-Bold="true" 
                    Text="¿O prefieres elegir una sustancia sustituto?" />

                <br />
                <br />

                <asp:Table ID="Table3" runat="server" Width="250px">
                    <asp:TableRow>
                        <asp:TableCell HorizontalAlign="Left">
                            <asp:Label ID="Label8" runat="server" Text="Buscar una sustancia" Font-Names="Trebuchet MS" Font-Size="Smaller" Font-Bold="true" />
                        </asp:TableCell>
                        <asp:TableCell HorizontalAlign="Right">
                            <asp:Label ID="Label9" runat="server" Text="Registros por página" Font-Names="Trebuchet MS" Font-Size="Smaller" Font-Bold="true" />
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell HorizontalAlign="Left">
                            <asp:TextBox ID="txtSubstituteSubstance" runat="server" Text="" Enabled="true" />
                            <asp:Button ID="btnSearchSubstitute" runat="server" ToolTip="Consultar" Text="Consultar" OnClick="btnSearchSubstitute_Click" />
                        </asp:TableCell>
                        <asp:TableCell HorizontalAlign="Right">
                            <asp:DropDownList ID="ddlPageSubstitute" runat="server" AutoPostBack="True" Font-Names="Arial" Font-Size="10pt" OnSelectedIndexChanged="ddlPageSubstitute_SelectedIndexChanged">
                                <asp:ListItem Value="-1" >Ilimitado</asp:ListItem>
                                <asp:ListItem Value="5">5</asp:ListItem>
                                <asp:ListItem Value="15">15</asp:ListItem>
                                <asp:ListItem Value="25" Selected="True">25</asp:ListItem>
                                <asp:ListItem Value="50">50</asp:ListItem>
                                <asp:ListItem Value="100">100</asp:ListItem>
                            </asp:DropDownList>
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>

                <asp:GridView ID="grdSubstituteSubstances" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" 
                    CellPadding="3" Font-Names="Trebuchet MS" HorizontalAlign="Left" Font-Size="Smaller" DataKeyNames="ActiveSubstanceId" OnRowDataBound="grdSubstituteSubstances_RowDataBound"
                    OnPageIndexChanging="grdSubstituteSubstances_PageIndexChanging" >
                    <Columns>
                        <asp:BoundField DataField="ActiveSubstanceId" Visible="False" />
                        <asp:TemplateField HeaderText="Nombre de la sustancia activa">
                            <HeaderStyle HorizontalAlign="Left" BackColor="#2694B4" ForeColor="White" />
                            <ItemStyle  Width="350px" />
                            <ItemTemplate>
                                <asp:Label ID="lblSubstanceName" runat="server">
                                    <%# DataBinder.Eval(Container.DataItem, "Description")%>
                                </asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Agregar sustancia sustituto">
                            <HeaderStyle Width="100px" HorizontalAlign="Center" BackColor="#2694B4" ForeColor="White" />
                            <ItemStyle HorizontalAlign="Center" Wrap="True" />
                            <ItemTemplate>
                                <asp:ImageButton ID="btnReplaceSubstance" runat="server" CommandName="Add" ImageUrl="~/Images/agregar.png" AlternateText="Agregar a producto" 
                                    Enabled="true" Width="20px" OnClick="btnReplaceSubstance_OnClick" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                
            </td>
        </tr>
    </table>
    
</asp:Content>



