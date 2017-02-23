<%@ Page Language="C#" MasterPageFile="~/medinet.master" AutoEventWireup="true" CodeFile="laboratoryInformation.aspx.cs" Inherits="laboratoryInformation" %>
<%@ MasterType VirtualPath="~/medinet.master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <asp:GridView ID="grdDivision" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3"
        Font-Names="Trebuchet MS" Font-Size="Smaller" OnRowEditing="grdDivision_RowEditing" OnRowUpdating="grdDivision_RowUpdating">
        <Columns>
            <asp:TemplateField HeaderText="Razón Social">
                <HeaderStyle HorizontalAlign="Left" />
                <ItemStyle  Width="400px" />
                <ItemTemplate>
                    <asp:Label ID="lblDivisionName" runat="server">
                        <%# DataBinder.Eval(Container.DataItem, "Description") %>
                    </asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtDivisionDescription" runat="server" CssClass="label" Text='<%# displayDivisionDescription((string)DataBinder.Eval(Container.DataItem, "Description")) %>'
                        Width="400px" />
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Nombre corto">
                <HeaderStyle HorizontalAlign="Left" />
                <ItemStyle  Width="150px" />
                <ItemTemplate>
                    <asp:Label ID="lblDivisionShortName" runat="server">
                        <%# DataBinder.Eval(Container.DataItem, "ShortName") %>
                    </asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtDivisionShortName" runat="server" CssClass="label" Text='<%# displayShortName((string)DataBinder.Eval(Container.DataItem, "ShortName")) %>'
                        Width="150px" />
                </EditItemTemplate>
           </asp:TemplateField>
           <asp:TemplateField HeaderText="Editar">
                <HeaderStyle Width="45px" HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" Wrap="True" />
                <ItemTemplate>
                    <asp:ImageButton ID="btnEdit" runat="server" CommandName="Edit" ImageUrl="~/Images/editar.gif" AlternateText="Editar Divisi&#243;n" Enabled="true" />
                </ItemTemplate>
                <EditItemTemplate >
                    <asp:ImageButton ID="btnSave" runat="server" CommandName="Update" ImageUrl="~/Images/salvar.jpg" AlternateText="Guardar Divisi&#243;n" />
                </EditItemTemplate>
            </asp:TemplateField>
             <asp:TemplateField HeaderText="Agregar direccion">
                <HeaderStyle Width="45px" HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" Wrap="True" />
                <ItemTemplate>
                    <asp:ImageButton ID="btnAddDir" runat="server" ImageUrl="~/Images/agregar.png" AlternateText="Agregar direccion" Enabled="true" Width="20" Height="20" OnClick="AddAddress_Click" />
                </ItemTemplate>
                
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    
    <br />
    <asp:Panel ID="pnlAddress" runat="server" Height="300px" Width="922px" 
        BorderStyle="Ridge" Visible="False">
        <table style="width:100%; height: 83px;">
            <tr>
                <td style="width: 473px">
                    <asp:Label ID="Label1" runat="server" Text="Dirección:" Font-Italic="False" 
                        Font-Size="Medium"></asp:Label>
                </td>
                <td style="width: 764px">
                    <asp:TextBox ID="txtAddress" runat="server" Width="547px"></asp:TextBox>
                </td>
                <td style="width: 499px">
                    <asp:RequiredFieldValidator ID="rfvAddress" runat="server" 
                        ControlToValidate="txtAddress" ErrorMessage="Dato Requerido" Font-Italic="True" 
                        ForeColor="Red" SetFocusOnError="True" ValidationGroup="insertAddress"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td style="width: 473px">
                    <asp:Label ID="Label2" runat="server" Text="Colonia:" Font-Italic="False" 
                        Font-Size="Medium"></asp:Label>
                </td>
                <td style="width: 764px">
                    <asp:TextBox ID="txtSuburb" runat="server" Width="287px"></asp:TextBox>
                </td>
                <td style="width: 499px">
                    &nbsp;</td>
            </tr>
            <tr>
                <td style="width: 473px">
                    <asp:Label ID="Label3" runat="server" Text="Código Postal:" Font-Italic="False" 
                        Font-Size="Medium"></asp:Label>
                </td>
                <td style="width: 764px">
                    <asp:TextBox ID="txtPostalCode" runat="server" MaxLength="5"></asp:TextBox>
                </td>
                <td style="width: 499px">
                    &nbsp;</td>
            </tr>
            <tr>
                <td style="width: 473px">
                    <asp:Label ID="Label4" runat="server" Text="Teléfono:" Font-Italic="False" 
                        Font-Size="Medium"></asp:Label>
                </td>
                <td style="width: 764px">
                    <asp:TextBox ID="txtPhone" runat="server"></asp:TextBox>
                </td>
                <td style="width: 499px">
                    &nbsp;</td>
            </tr>
            <tr>
                <td style="width: 473px">
                    <asp:Label ID="Label5" runat="server" Text="Fax:" Font-Italic="False" 
                        Font-Size="Medium"></asp:Label>
                </td>
                <td style="width: 764px">
                    <asp:TextBox ID="txtFax" runat="server"></asp:TextBox>
                </td>
                <td style="width: 499px">
                    &nbsp;</td>
            </tr>
            <tr>
                <td style="width: 473px">
                    <asp:Label ID="Label6" runat="server" Text="Página Web:" Font-Italic="False" 
                        Font-Size="Medium"></asp:Label>
                </td>
                <td style="width: 764px">
                    <asp:TextBox ID="txtWeb" runat="server"></asp:TextBox>
                </td>
                <td style="width: 499px">
                    &nbsp;</td>
            </tr>
            <tr>
                <td style="width: 473px">
                    <asp:Label ID="Label10" runat="server" Text="Email:" Font-Italic="False" 
                        Font-Size="Medium"></asp:Label>
                </td>
                <td style="width: 764px">
                    <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
                </td>
                <td style="width: 499px">
                    &nbsp;</td>
            </tr>
            <tr>
                <td style="width: 473px">
                    <asp:Label ID="Label7" runat="server" Text="Ciudad:" Font-Italic="False" 
                        Font-Size="Medium"></asp:Label>
                </td>
                <td style="width: 764px">
                    <asp:TextBox ID="txtCity" runat="server" Width="278px"></asp:TextBox>
                </td>
                <td style="width: 499px">
                    &nbsp;</td>
            </tr>
            <tr>
                <td style="width: 473px">
                    <asp:Label ID="Label8" runat="server" Text="Estado:" Font-Italic="False" 
                        Font-Size="Medium"></asp:Label>
                </td>
                <td style="width: 764px">
                    <asp:TextBox ID="txtState" runat="server" Width="279px"></asp:TextBox>
                </td>
                <td style="width: 499px">
                    &nbsp;</td>
            </tr>
            <tr>
                <td style="width: 473px">
                    <asp:Label ID="Label9" runat="server" Text="Contacto:" Font-Italic="False" 
                        Font-Size="Medium"></asp:Label>
                </td>
                <td style="width: 764px">
                    <asp:TextBox ID="txtContact" runat="server" Width="277px"></asp:TextBox>
                </td>
                <td style="width: 499px">
                    <asp:Button ID="btnSaveAddress" runat="server" onclick="btnSaveAddress_Click" 
                        Text="Guardar" Width="115px" ValidationGroup="insertAddress" />
                </td>
            </tr>
            
          
        </table>
    </asp:Panel>
    <br />

    <asp:GridView ID="grdDivisionInformation" runat="server" 
        AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" 
        BorderStyle="None" BorderWidth="1px" CellPadding="3"
        Font-Names="Trebuchet MS" Font-Size="Smaller" 
        OnRowEditing="grdDivisionInformation_RowEditing" 
        OnRowUpdating="grdDivisionInformation_RowUpdating" 
        onrowdatabound="grdDivisionInformation_RowDataBound"
        DataKeyNames="DivisionInfId" 
        onrowdeleting="grdDivisionInformation_RowDeleting">
        <Columns>
            <asp:TemplateField HeaderText="Dirección">
                <HeaderStyle HorizontalAlign="Left" />
                <ItemStyle  Width="200px" />
                <ItemTemplate>
                    <asp:Label ID="lblDivisionAddress" runat="server">
                        <%# DataBinder.Eval(Container.DataItem, "Address") %>
                    </asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtDivisionAddress" runat="server" CssClass="label" TextMode="MultiLine" Text='<%# displayAddress((string)DataBinder.Eval(Container.DataItem, "Address")) %>'
                        Width="200px" />
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Colonia">
                <HeaderStyle HorizontalAlign="Left" />
                <ItemStyle  Width="200px" />
                <ItemTemplate>
                    <asp:Label ID="lblDivisionSuburb" runat="server">
                        <%# DataBinder.Eval(Container.DataItem, "Suburb") %>
                    </asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtDivisionSuburb" runat="server" CssClass="label" TextMode="MultiLine" Text='<%# displaySuburb((string)DataBinder.Eval(Container.DataItem, "Suburb")) %>'
                        Width="200px" />
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Código Postal">
                <HeaderStyle HorizontalAlign="Left" />
                <ItemStyle  Width="60px" />
                <ItemTemplate>
                    <asp:Label ID="lblDivisionZipCode" runat="server">
                        <%# DataBinder.Eval(Container.DataItem, "ZipCode") %>
                    </asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtDivisionZipCode" runat="server" CssClass="label" TextMode="MultiLine" Text='<%# displayZipCode((string)DataBinder.Eval(Container.DataItem, "ZipCode")) %>'
                        Width="60px" />
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Teléfono">
                <HeaderStyle HorizontalAlign="Left" />
                <ItemStyle  Width="100px" />
                <ItemTemplate>
                    <asp:Label ID="lblDivisionTelephone" runat="server">
                        <%# DataBinder.Eval(Container.DataItem, "Telephone") %>
                    </asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtDivisionTelephone" runat="server" CssClass="label" TextMode="MultiLine" Text='<%# displayTelephone((string)DataBinder.Eval(Container.DataItem, "Telephone")) %>'
                        Width="100px" />
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Fax">
                <HeaderStyle HorizontalAlign="Left" />
                <ItemStyle  Width="100px" />
                <ItemTemplate>
                    <asp:Label ID="lblDivisionFax" runat="server">
                        <%# DataBinder.Eval(Container.DataItem, "Fax") %>
                    </asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtDivisionFax" runat="server" CssClass="label" TextMode="MultiLine" Text='<%# displayFax((string)DataBinder.Eval(Container.DataItem, "Fax")) %>'
                        Width="100px" />
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Página Web">
                <HeaderStyle HorizontalAlign="Left" />
                <ItemStyle  Width="100px" />
                <ItemTemplate>
                    <asp:Label ID="lblDivisionWeb" runat="server">
                        <%# DataBinder.Eval(Container.DataItem, "Web")%>
                    </asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtDivisionWeb" runat="server" CssClass="label" TextMode="MultiLine" Text='<%# displayWeb((string)DataBinder.Eval(Container.DataItem, "Web")) %>'
                        Width="100px" />
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Email">
                <HeaderStyle HorizontalAlign="Left" />
                <ItemStyle  Width="100px" />
                <ItemTemplate>
                    <asp:Label ID="lblDivisionEmail" runat="server">
                        <%# DataBinder.Eval(Container.DataItem, "Email")%>
                    </asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtDivisionEmail" runat="server" CssClass="label" TextMode="MultiLine" Text='<%# displayEmail((string)DataBinder.Eval(Container.DataItem, "Email")) %>'
                        Width="100px" />
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Ciudad">
                <HeaderStyle HorizontalAlign="Left" />
                <ItemStyle  Width="100px" />
                <ItemTemplate>
                    <asp:Label ID="lblDivisionCity" runat="server">
                        <%# DataBinder.Eval(Container.DataItem, "City")%>
                    </asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtDivisionCity" runat="server" CssClass="label" TextMode="MultiLine" Text='<%# displayCity((string)DataBinder.Eval(Container.DataItem, "City")) %>'
                        Width="100px" />
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Estado">
                <HeaderStyle HorizontalAlign="Left" />
                <ItemStyle  Width="100px" />
                <ItemTemplate>
                    <asp:Label ID="lblDivisionState" runat="server">
                        <%# DataBinder.Eval(Container.DataItem, "State")%>
                    </asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtDivisionState" runat="server" CssClass="label" TextMode="MultiLine" Text='<%# displayState((string)DataBinder.Eval(Container.DataItem, "State")) %>'
                        Width="100px" />
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Contacto">
                <HeaderStyle HorizontalAlign="Left" />
                <ItemStyle  Width="100px" />
                <ItemTemplate>
                    <asp:Label ID="lblDivisionContact" runat="server">
                        <%# DataBinder.Eval(Container.DataItem, "Contact")%>
                    </asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtDivisionContact" runat="server" CssClass="label" TextMode="MultiLine" Text='<%# displayContact((string)DataBinder.Eval(Container.DataItem, "Contact")) %>'
                        Width="100px" />
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Editar">
                <HeaderStyle Width="45px" HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" Wrap="True" />
                <ItemTemplate>
                    <asp:ImageButton ID="btnEditDivInf" runat="server" CommandName="Edit" ImageUrl="~/Images/editar.gif" AlternateText="Editar Dirección" Enabled="true" />
                </ItemTemplate>
                <EditItemTemplate >
                    <asp:ImageButton ID="btnSaveDivInf" runat="server" CommandName="Update" ImageUrl="~/Images/salvar.jpg" AlternateText="Guardar Dirección" />
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Eliminar">
                <HeaderStyle Width="45px" HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" Wrap="True" />
                <ItemTemplate>
                    <asp:ImageButton ID="btnElimDivInf" runat="server"  ImageUrl="~/Images/delete.gif" CommandName="Delete" AlternateText="Eliminar Dirección" Enabled="true"  />
                </ItemTemplate>
                
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    
    <br />
    <br />
    <asp:Button ID="btnLabReturn" runat="server" ToolTip="Regresar a Productos" Text="Regresar" OnClick="btnLabReturn_Click"/>
    <br />
    <br />
</asp:Content>