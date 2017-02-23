<%@ Page Title="" Language="C#" MasterPageFile="~/medinet.master" AutoEventWireup="true" CodeFile="productDesign.aspx.cs" Inherits="productDesign" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <asp:Table ID="tblOptions" runat="server">
        <asp:TableRow BorderWidth="1px" Font-Names="Trebuchet MS" Font-Size="Smaller" Font-Bold="true">
            <asp:TableCell BorderWidth="1px" Width="220px" HorizontalAlign="Left">
                <asp:Label ID="lblChooseOther" runat="server" Text="Elegir otro laboratorio" />
            </asp:TableCell><asp:TableCell BorderWidth="1px" Width="220px" HorizontalAlign="Right">
                <asp:Label ID="lblQtyRecords" runat="server" Text="Registros por página" />
            </asp:TableCell></asp:TableRow><asp:TableRow BorderWidth="1px">
            <asp:TableCell BorderWidth="1px" Width="220px" HorizontalAlign="Left">
                <asp:ImageButton ID="imgBtnBackLabs" runat="server" ImageUrl="images/regresar.png" ToolTip="Elegir otro laboratorio"  />
            </asp:TableCell><asp:TableCell BorderWidth="1px" Width="220px" HorizontalAlign="Right">
                <asp:DropDownList ID="ddlPageSize" runat="server" AutoPostBack="True" Font-Names="Arial" Font-Size="10pt" >
                    <asp:ListItem Value="-1" >Ilimitado</asp:ListItem>
                    <asp:ListItem Value="5">5</asp:ListItem>
                    <asp:ListItem Value="15" Selected="True">15</asp:ListItem>
                    <asp:ListItem Value="25">25</asp:ListItem>
                    <asp:ListItem Value="50">50</asp:ListItem>
                    <asp:ListItem Value="100">100</asp:ListItem>
                </asp:DropDownList>
            </asp:TableCell></asp:TableRow></asp:Table><br />
            <br />

            <asp:GridView ID="grdProducts" runat="server" 
        AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" 
        BorderStyle="None" BorderWidth="1px" CellPadding="3"
        Font-Names="Trebuchet MS" Font-Size="Smaller" 
        DataKeyNames="ProductId,PharmaFormId,DivisionId,CategoryId" 
        OnRowDataBound="grdProducts_RowDataBound"><Columns>
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
                    </asp:Label></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="SIDEF">
                <HeaderStyle HorizontalAlign="Left" BackColor="#2694B4" ForeColor="White" />
                <ItemStyle  Width="300px" />
                <ItemTemplate>
                
                 <asp:DataList ID="dlSidef" runat="server" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="0px" CellPadding="3" Font-Names="Trebuchet MS" Font-Size="Smaller"
        RepeatDirection="Horizontal" RepeatLayout="Table" RepeatColumns="10"  Visible="true"
         OnItemDataBound="dlSidef_DataBound">

       <ItemStyle VerticalAlign="Top" HorizontalAlign="Center"  BorderStyle="Inset" BorderWidth="1px" BorderColor="#999999"/>
        
        <ItemTemplate>
        <asp:Image ID="imgSidef" runat="server" Width="30px" Height="30px" /><br />
        <b><%# DataBinder.Eval(Container.DataItem, "ProductShot")%></b>
         
         </ItemTemplate>
         
    </asp:DataList>    
                    
                </ItemTemplate>
                <%--<EditItemTemplate>
                <table>
                <tr>
                <td>320x480</td><td>384x512</td><td>400x400</td></tr><tr>
                <td><asp:FileUpload ID="fUp320X480" runat="server" /></td>
                <td><asp:FileUpload ID="fUp384X512" runat="server" /></td>
                <td><asp:FileUpload ID="fUp400X400" runat="server" /></td>
                </tr>
                
                
                </table>
                </EditItemTemplate>--%>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Editar">
                <HeaderStyle HorizontalAlign="Left" BackColor="#2694B4" ForeColor="White" />
                <ItemStyle  Width="40px"  HorizontalAlign="Center"/>
                
                <ItemTemplate>
                    <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Images/editar.gif" AlternateText="Editar" OnClick="editShot_click" />
                    </ItemTemplate>
                    
                   <%-- <EditItemTemplate >
                    <asp:ImageButton ID="btnSave" runat="server" CommandName="Update" ImageUrl="~/Images/salvar.jpg" AlternateText="Guardar Rubros" />
                </EditItemTemplate>--%>
                    </asp:TemplateField>
                    </Columns></asp:GridView><br />

</asp:Content>

