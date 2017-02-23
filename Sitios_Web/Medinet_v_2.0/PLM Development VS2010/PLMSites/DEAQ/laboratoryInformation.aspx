<%@ Page Language="C#" MasterPageFile="~/deaq.master" AutoEventWireup="true" CodeFile="laboratoryInformation.aspx.cs" Inherits="laboratoryInformation" %>
<%@ MasterType VirtualPath="~/deaq.master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">  
    <asp:GridView ID="grdDivision" runat="server" AutoGenerateColumns="False"  BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3"
        Font-Names="Trebuchet MS" Font-Size="Smaller" OnRowEditing="grdDivision_RowEditing" OnRowUpdating="grdDivision_RowUpdating">
        <Columns>
            <asp:TemplateField HeaderText="Razón Social">
                <HeaderStyle HorizontalAlign="center" BackColor="#2694B4" ForeColor="White" Height="30px" />
                <ItemStyle  Width="600px"  />
                <ItemTemplate>
                    <asp:Label ID="lblDivisionName" runat="server">
                        <%# DataBinder.Eval(Container.DataItem, "DivisionName") %>
                    </asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtDivisionDescription" runat="server"  TextMode="MultiLine" Rows="3" CssClass="label" Text='<%# displayDivisionDescription((string)DataBinder.Eval(Container.DataItem, "DivisionName")) %>'
                        Width="600px" Font-Names="Trebuchet MS" Font-Size="13px"/>
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Nombre corto">
                <HeaderStyle HorizontalAlign="center"  BackColor="#2694B4" ForeColor="White"/>
                <ItemStyle  Width="350px" />
                <ItemTemplate>
                    <asp:Label ID="lblDivisionShortName" runat="server">
                        <%# DataBinder.Eval(Container.DataItem, "ShortName") %>
                    </asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtDivisionShortName" runat="server" CssClass="label" TextMode="MultiLine" Rows="3"  Text='<%# displayShortName((string)DataBinder.Eval(Container.DataItem, "ShortName")) %>'
                        Width="350px" Font-Names="Trebuchet MS" Font-Size="13px"  />
                </EditItemTemplate>
           </asp:TemplateField>
           <asp:TemplateField HeaderText="Editar">
                <HeaderStyle Width="55px" HorizontalAlign="Center"  BackColor="#2694B4" ForeColor="White"/>
                <ItemStyle HorizontalAlign="Center" Wrap="True" />
                <ItemTemplate>
                    <asp:ImageButton ID="btnEdit" runat="server" CommandName="Edit" ImageUrl="~/Images/editar.gif" AlternateText="Editar Divisi&#243;n" Enabled="true" />
                </ItemTemplate>
                <EditItemTemplate >
                    <asp:ImageButton ID="btnSave" runat="server" CommandName="Update" ImageUrl="~/Images/salvar.jpg" AlternateText="Guardar Divisi&#243;n" />
                </EditItemTemplate>
            </asp:TemplateField>
             <asp:TemplateField HeaderText="Agregar direccion">
                <HeaderStyle Width="125px" HorizontalAlign="Center" BackColor="#2694B4" ForeColor="White" />
                <ItemStyle HorizontalAlign="Center" Wrap="True" />
                <ItemTemplate>
                    <asp:ImageButton ID="btnAddDir" runat="server" ImageUrl="~/Images/agregar.png" AlternateText="Agregar direccion" Enabled="true" Width="20" Height="20" OnClick="AddAddress_Click" />
                </ItemTemplate>
                
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <p style="align-content:center;  width: 1135px;"> 
     <asp:Button ID="btnCdivision" runat="server" onclick="btnCdivision_Click" 
                        Text="Cancelar" Width="115px"   /></p>
    <br />
    <asp:Panel ID="pnlAddress" runat="server" Height="320px" Width="800px" 
        BorderStyle="Ridge" Visible="False"  >
        <br>
        <table  style="width:750px; height: 286px; font-family:'Trebuchet MS'; Font-Size:smaller;">
            <tr>
                <td style="width: 100px">
                    <asp:Label ID="Label1" runat="server" Text="Dirección:" Font-Italic="False" 
                        Font-Size="Medium"></asp:Label>
                </td>
                <td style="width: 7px">
                    <asp:TextBox ID="txtAddress" runat="server" Width="547px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvAddress" runat="server" 
                        ControlToValidate="txtAddress" ErrorMessage="*" Font-Italic="True" 
                        ForeColor="Red" SetFocusOnError="True" ValidationGroup="insertAddress"></asp:RequiredFieldValidator>
                </td>
                
            </tr>
            <tr>
                <td style="width: 100px">
                    <asp:Label ID="Label2" runat="server" Text="Colonia:" Font-Italic="False" 
                        Font-Size="Medium"></asp:Label>
                </td>
                <td style="width: 7px">
                    <asp:TextBox ID="txtSuburb" runat="server" Width="300px"></asp:TextBox>
                </td>
                
            </tr>
            <tr>
                <td style="width: 100px">
                    <asp:Label ID="Label3" runat="server" Text="Código Postal:" Font-Italic="False" 
                        Font-Size="Medium"></asp:Label>
                </td>
                <td style="width: 7px">
                    <asp:TextBox ID="txtPostalCode" runat="server" MaxLength="10" Width="300" ></asp:TextBox>
                </td>
                
            </tr>
            <tr>
                <td style="width: 100px">
                    <asp:Label ID="Label4" runat="server" Text="Teléfono:" Font-Italic="False" 
                        Font-Size="Medium"></asp:Label>
                </td>
                <td style="width: 7px">
                    <asp:TextBox ID="txtPhone" runat="server" Width="300"></asp:TextBox>
                </td>
                
            </tr>
            <tr>
                <td style="width: 100px">
                    <asp:Label ID="Label5" runat="server" Text="Fax:" Font-Italic="False" 
                        Font-Size="Medium"></asp:Label>
                </td>
                <td style="width: 7px">
                    <asp:TextBox ID="txtFax" runat="server" Width="300"></asp:TextBox>
                </td>
               
            </tr>
            <tr>
                <td style="width: 100px">
                    <asp:Label ID="Label6" runat="server" Text="Página Web:" Font-Italic="False" 
                        Font-Size="Medium"></asp:Label>
                </td>
                <td style="width: 7px">
                    <asp:TextBox ID="txtWeb" runat="server" Width="300"></asp:TextBox>
                </td>
                
            </tr>
            <tr>
                <td style="width: 100px">
                    <asp:Label ID="Label10" runat="server" Text="Email:" Font-Italic="False" 
                        Font-Size="Medium"></asp:Label>
                </td>
                <td style="width: 7px">
                    <asp:TextBox ID="txtEmail" runat="server" Width="300"></asp:TextBox>
                </td>
                
            </tr>
            <tr>
                <td style="width: 100px">
                    <asp:Label ID="Label7" runat="server" Text="Ciudad:" Font-Italic="False" 
                        Font-Size="Medium"></asp:Label>
                </td>
                <td style="width: 7px">
                    <asp:TextBox ID="txtCity" runat="server" Width="300px" ></asp:TextBox>
                </td>
                
            </tr>
            <tr>
                <td style="width: 100px">
                    <asp:Label ID="Label8" runat="server" Text="Estado:" Font-Italic="False" 
                        Font-Size="Medium"></asp:Label>
                </td>
                <td style="width: 7px">
                    <asp:TextBox ID="txtState" runat="server" Width="300px"></asp:TextBox>
                </td>
               
            </tr>
            <tr>
                <td style="width: 100px; height: 13px;">
                    <asp:Label ID="Label9" runat="server" Text="Lada:" Font-Italic="False" 
                        Font-Size="Medium"></asp:Label>
                </td>
                <td style="width: 7px; height: 13px;">
                    <asp:TextBox ID="txtLada" runat="server" Width="300px"></asp:TextBox>
                </td>
               
            </tr>
            <tr>
                <td colspan="2" align="center" style="height:40px;" >
                    <br />
                    <asp:Button ID="btnSaveAddress" runat="server" onclick="btnSaveAddress_Click" 
                        Text="Guardar" Width="115px" ValidationGroup="insertAddress" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                     <asp:Button ID="btnCancel" runat="server" onclick="btnCancel_Click" 
                        Text="Cancelar" Width="115px"   />

                </td>
            </tr>
            
          
        </table>
    </asp:Panel>
    <br />

    <asp:GridView ID="grdDivisionInformation" runat="server" 
        AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" 
        BorderStyle="None" BorderWidth="1px" CellPadding="4"
        Font-Names="Trebuchet MS" Font-Size="Smaller" 
        OnRowEditing="grdDivisionInformation_RowEditing" 
        OnRowUpdating="grdDivisionInformation_RowUpdating" 
        onrowdatabound="grdDivisionInformation_RowDataBound"
        DataKeyNames="DivisionInformationId" 
        onrowdeleting="grdDivisionInformation_RowDeleting">
        <Columns>
            <asp:TemplateField HeaderText="Dirección">
                <HeaderStyle HorizontalAlign="center" BackColor="#2694B4" ForeColor="White" Height="30px" />
                <ItemStyle  Width="200px" />
                <ItemTemplate>
                    <asp:Label ID="lblDivisionAddress" runat="server">
                        <%# DataBinder.Eval(Container.DataItem, "Address") %>
                    </asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtDivisionAddress" runat="server" CssClass="label" TextMode="MultiLine" Rows="7" Text='<%# displayAddress((string)DataBinder.Eval(Container.DataItem, "Address")) %>'
                        Width="200px" Font-Names="Trebuchet MS" Font-Size="13px"/>
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Colonia" >
                <HeaderStyle HorizontalAlign="center" BackColor="#2694B4" ForeColor="White" />
                <ItemStyle  Width="160px" />
                <ItemTemplate>
                    <asp:Label ID="lblDivisionSuburb" runat="server">
                        <%# DataBinder.Eval(Container.DataItem, "Suburb") %>
                    </asp:Label>
                </ItemTemplate>
                <EditItemTemplate >
                    <asp:TextBox ID="txtDivisionSuburb" runat="server" CssClass="label" TextMode="MultiLine" Rows="7" Text='<%# displaySuburb((string)DataBinder.Eval(Container.DataItem, "Suburb")) %> '
                        Width="160px" Font-Names="Trebuchet MS" Font-Size="13px"/>
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Código Postal">
                <HeaderStyle HorizontalAlign="center"  BackColor="#2694B4" ForeColor="White"/>
                <ItemStyle  Width="100px"  />
                <ItemTemplate>
                    <asp:Label ID="lblDivisionZipCode" runat="server">
                        <%# DataBinder.Eval(Container.DataItem, "ZipCode") %>
                    </asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtDivisionZipCode" runat="server" MaxLength="10" Height="105"  CssClass="label"   Text='<%# displayZipCode((string)DataBinder.Eval(Container.DataItem, "ZipCode")) %>'
                        Width="100px" Font-Names="Trebuchet MS" Font-Size="13px"/>
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Teléfono">
                <HeaderStyle HorizontalAlign="center" BackColor="#2694B4" ForeColor="White" />
                <ItemStyle  Width="150px"  />
                <ItemTemplate>
                    <asp:Label ID="lblDivisionTelephone" runat="server">
                        <%# DataBinder.Eval(Container.DataItem, "Telephone") %>
                    </asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtDivisionTelephone" runat="server" CssClass="label" TextMode="MultiLine" Rows="7" Text='<%# displayTelephone((string)DataBinder.Eval(Container.DataItem, "Telephone")) %>'
                        Width="150px" Font-Names="Trebuchet MS" Font-Size="13px"/>
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Fax">
                <HeaderStyle HorizontalAlign="center" BackColor="#2694B4" ForeColor="White" />
                <ItemStyle  Width="100px" />
                <ItemTemplate>
                    <asp:Label ID="lblDivisionFax" runat="server">
                        <%# DataBinder.Eval(Container.DataItem, "Fax") %>
                    </asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtDivisionFax" runat="server" CssClass="label" TextMode="MultiLine"  Rows="7" Text='<%# displayFax((string)DataBinder.Eval(Container.DataItem, "Fax")) %>'
                        Width="120px" Font-Names="Trebuchet MS" Font-Size="13px"/>
                </EditItemTemplate>
            </asp:TemplateField>

              <asp:TemplateField HeaderText="Lada">
                <HeaderStyle HorizontalAlign="center" BackColor="#2694B4" ForeColor="White" />
                <ItemStyle  Width="100px"  />
                <ItemTemplate>
                    <asp:Label ID="lbllada" runat="server">
                        <%# DataBinder.Eval(Container.DataItem, "Lada") %>
                    </asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtDivisionLada" runat="server" CssClass="label" TextMode="MultiLine" Rows="7" Text='<%# displayLada((string)DataBinder.Eval(Container.DataItem, "Lada")) %>'
                        Width="110px" Font-Names="Trebuchet MS" Font-Size="13px"/>
                </EditItemTemplate>
            </asp:TemplateField>


            <asp:TemplateField HeaderText="Página Web">
                <HeaderStyle HorizontalAlign="center"  BackColor="#2694B4" ForeColor="White" />
                <ItemStyle  Width="140px"  />
                <ItemTemplate>
                    <asp:Label ID="lblDivisionWeb" runat="server">
                        <%# DataBinder.Eval(Container.DataItem, "Web")%>
                    </asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtDivisionWeb" runat="server" CssClass="label" TextMode="MultiLine" Rows="7" Text='<%# displayWeb((string)DataBinder.Eval(Container.DataItem, "Web")) %>'
                        Width="140px" Font-Names="Trebuchet MS" Font-Size="13px"/>
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Email">
                <HeaderStyle HorizontalAlign="center" BackColor="#2694B4" ForeColor="White"/>
                <ItemStyle  Width="140px"  />
                <ItemTemplate>
                    <asp:Label ID="lblDivisionEmail" runat="server">
                        <%# DataBinder.Eval(Container.DataItem, "Email")%>
                    </asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtDivisionEmail" runat="server" CssClass="label" TextMode="MultiLine"  Rows="7" Text='<%# displayEmail((string)DataBinder.Eval(Container.DataItem, "Email")) %>'
                        Width="140px" Font-Names="Trebuchet MS" Font-Size="13px"/>
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Ciudad">
                <HeaderStyle HorizontalAlign="center" BackColor="#2694B4" ForeColor="White"/>
                <ItemStyle  Width="120px" />
                <ItemTemplate>
                    <asp:Label ID="lblDivisionCity" runat="server">
                        <%# DataBinder.Eval(Container.DataItem, "City")%>
                    </asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtDivisionCity" runat="server" CssClass="label" TextMode="MultiLine"  Rows="7" Text='<%# displayCity((string)DataBinder.Eval(Container.DataItem, "City")) %>'
                        Width="120px" Font-Names="Trebuchet MS" Font-Size="13px"/>
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Estado">
                <HeaderStyle HorizontalAlign="center" BackColor="#2694B4" ForeColor="White"/>
                <ItemStyle  Width="100px"  />
                <ItemTemplate>
                    <asp:Label ID="lblDivisionState" runat="server">
                        <%# DataBinder.Eval(Container.DataItem, "State")%>
                    </asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtDivisionState" runat="server" CssClass="label" TextMode="MultiLine" Rows="7" Text='<%# displayState((string)DataBinder.Eval(Container.DataItem, "State")) %>'
                        Width="110px" Font-Names="Trebuchet MS" Font-Size="13px" />
                </EditItemTemplate>
            </asp:TemplateField>
            
            <asp:TemplateField HeaderText="Editar">
                <HeaderStyle Width="45px" HorizontalAlign="Center" BackColor="#2694B4" ForeColor="White"/>
                <ItemStyle HorizontalAlign="Center" Wrap="True" />
                <ItemTemplate>
                    <asp:ImageButton ID="btnEditDivInf" runat="server" CommandName="Edit" ImageUrl="~/Images/editar.gif" AlternateText="Editar Dirección" Enabled="true" />
                </ItemTemplate>
                <EditItemTemplate >
                    <asp:ImageButton ID="btnSaveDivInf" runat="server" CommandName="Update" ImageUrl="~/Images/salvar.jpg" AlternateText="Guardar Dirección" />
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Eliminar">
                <HeaderStyle Width="65px" HorizontalAlign="Center" BackColor="#2694B4" ForeColor="White"/>
                <ItemStyle HorizontalAlign="Center" Wrap="True" />
                <ItemTemplate>
                    <asp:ImageButton ID="btnElimDivInf" runat="server"  ImageUrl="~/Images/delete.gif" CommandName="Delete" AlternateText="Eliminar Dirección" Enabled="true"  />
                </ItemTemplate>
                
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
     <p style="align-content:center"> 
     <asp:Button ID="btnCaddress" runat="server" onclick="btnCaddress_Click" 
                        Text="Cancelar" Width="115px"   /></p>

     <asp:ImageButton ID="imgBtnBackLaboratory" runat="server" ImageUrl="images/regresar.png" ToolTip="Regresar al laboratorio" OnClick="btnLabReturn_Click" />
    <br />
</asp:Content>