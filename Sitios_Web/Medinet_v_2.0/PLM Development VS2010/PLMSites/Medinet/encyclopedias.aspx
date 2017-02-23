<%@ Page Title="" Language="C#" MasterPageFile="~/medinet.master" AutoEventWireup="true" CodeFile="encyclopedias.aspx.cs" Inherits="encyclopedias" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <script type="text/javascript">

        function confirmDelete(deleteName) {
            var agree = confirm('Has elegido eliminar a: ' + deleteName + ', ¿Confirmas que deseas eliminar la enciclopedia de éste producto?');
            if (agree)
                return true;
            else
                return false;
        }

        function confirmDeleteInteracion() {
            var agree = confirm('Esta sustancia tiene interacciones medicamentosas asociadas, ¿Confirmas que quieres eliminarlo?');
            if (agree)
                return true;
            else
                return false;
        }

    </script>

    <asp:Label ID="Label1" runat="server" Font-Names="Trebuchet MS" Font-Size="X-Large" Font-Bold="true" Text="Índice de enciclopedias" />
    <br />
    <br />
    <asp:Label ID="Label4" runat="server" Font-Names="Trebuchet MS" Font-Size="Medium" Text="Producto: " />
    <asp:Label ID="lblBrand" runat="server" Font-Names="Trebuchet MS" Font-Size="Medium" Font-Bold="true" />
    <br />
    <asp:Label ID="Label5" runat="server" Font-Names="Trebuchet MS" Font-Size="Medium" Text="Forma farmacéutica: " />
    <asp:Label ID="lblPharmaForm" runat="server" Font-Names="Trebuchet MS" Font-Size="Medium" Font-Bold="true" />
    <br />
    <br /> 

    
    <br />

    

    <asp:Table ID="Table1" runat="server" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3"
        Font-Names="Trebuchet MS" Font-Size="Smaller">
        <asp:TableRow HorizontalAlign="Center">
            <asp:TableCell>
                <asp:ImageButton ID="imgBtnBackProducts" runat="server" ImageUrl="images/regresar.png" ToolTip="Regresar a productos" OnClick="imgBtnBackProducts_Click" />
            </asp:TableCell>
            <asp:TableCell>
                <asp:Button ID="btnTherapeuticsIndex" runat="server" Text="Ir al índice Terapéutico" OnClick="btnTherapeuticsIndex_Click" />
            </asp:TableCell>
            <asp:TableCell>
                <asp:Button ID="btnIndicatiosnIndex" runat="server" Text="Ir al índice de Indicaciones" OnClick="btnIndicationsIndex_Click" />
            </asp:TableCell>
            <asp:TableCell>
            <asp:Button ID="btnInteractions" runat="server" Text="Ir al índice de Interacciones" OnClick="btnInteractions_Click"  />        
            </asp:TableCell>
            <asp:TableCell>
            <asp:Button ID="btnContraindication" runat="server" Text="Ir al índice de Contraindicaciones" OnClick="btnContraindications_Click"  />        
            </asp:TableCell> 
            <asp:TableCell>
            <asp:Button ID="btnICD" runat="server" Text="Ir al índice CIE-10" OnClick="btnIcd_Click"  />        
            </asp:TableCell>
             <asp:TableCell>
            <asp:Button ID="btnSymptomps" runat="server" Text="Ir al índice de Síntomas"  OnClick="btnSymptoms_Click" />        
            </asp:TableCell>
             <asp:TableCell>
                <asp:Button ID="btnSubstancesIndex" runat="server" Text="Ir al índice de Sustancias" OnClick="btnSubstancesIndex_Click" />
            </asp:TableCell>
            <asp:TableCell>
            <asp:Button ID="btnOMSThera" runat="server" Text="Ir al índice OMS Terapéutico"  OnClick="btnOMSThera_Click" />        
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
    
    <br />
      
    <table cellspacing="10px" cellpadding="20px">
    <tr><td style="vertical-align:bottom"><asp:Label ID="Label6" runat="server" Font-Names="Trebuchet MS" Font-Size="Large" Font-Bold="true" Text="Encoclopedias asociadas" /></td>
    <td style="vertical-align:bottom"> <asp:Panel ID="pnlSearch" runat="server" DefaultButton="btnSearch">
                    <asp:Label ID="Label7" runat="server" Font-Names="Trebuchet MS" Font-Size="Large" Font-Bold="true" Text="Catálogo de enciclopedias" />
                    <asp:Table ID="Table2" runat="server" Width="250px">
                        <asp:TableRow>
                            <asp:TableCell HorizontalAlign="Left">
                                <asp:Label ID="Label2" runat="server" Text="Buscar enciclopedia" Font-Names="Trebuchet MS" Font-Size="Smaller" Font-Bold="true" />
                            </asp:TableCell><asp:TableCell HorizontalAlign="Right">
                                <asp:Label ID="Label3" runat="server" Text="Registros por página" Font-Names="Trebuchet MS" Font-Size="Smaller" Font-Bold="true" />
                            </asp:TableCell></asp:TableRow><asp:TableRow>
                            <asp:TableCell HorizontalAlign="Left">
                                <asp:TextBox ID="txtEncyclopediaSearch" runat="server" Text="" Enabled="true" />
                                <asp:Button ID="btnSearch" runat="server" ToolTip="Consultar" Text="Buscar" OnClick="btnSearch_Click" />
                            </asp:TableCell><asp:TableCell HorizontalAlign="Right">
                                <asp:DropDownList ID="ddlPageSize" runat="server" AutoPostBack="True" Font-Names="Arial" Font-Size="10pt" OnSelectedIndexChanged="ddlPageSize_SelectedIndexChanged">
                                    <asp:ListItem Value="-1" >Ilimitado</asp:ListItem>
                                    <asp:ListItem Value="5">5</asp:ListItem>
                                    <asp:ListItem Value="15" Selected="True">15</asp:ListItem>
                                    <asp:ListItem Value="25">25</asp:ListItem>
                                    <asp:ListItem Value="50">50</asp:ListItem>
                                    <asp:ListItem Value="100">100</asp:ListItem>
                                </asp:DropDownList>
                            </asp:TableCell></asp:TableRow></asp:Table></asp:Panel></td></tr><tr>
            <td style="vertical-align:top">
                
                
                <asp:GridView ID="grdProductEncyclopedias" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                    Font-Names="Trebuchet MS" Font-Size="Smaller" DataKeyNames="EncyclopediaId" HorizontalAlign="Left" OnRowDataBound="grdProductEncyclopedias_RowDataBound" ShowHeaderWhenEmpty="true">
                    <Columns>
                        <asp:BoundField DataField="EncyclopediaId" Visible="False" />
                        <asp:TemplateField HeaderText="Nombre de la enciclopedia">
                            <HeaderStyle HorizontalAlign="Left" BackColor="#2694B4" ForeColor="White" />
                            <ItemStyle  Width="250px" />
                            <ItemTemplate>
                                <asp:Label ID="lblActiveSubstance" runat="server">
                                    <%# DataBinder.Eval(Container.DataItem, "EncyclopediaName")%>
                                </asp:Label></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="Eliminar">
                            <HeaderStyle Width="45px" HorizontalAlign="Center" BackColor="#2694B4" ForeColor="White" />
                            <ItemStyle HorizontalAlign="Center" Wrap="True" />
                            <ItemTemplate>
                                <asp:ImageButton ID="btnDeleteEncyclo" runat="server" CommandName="Delete" ImageUrl="~/Images/delete.gif" AlternateText="Eliminar sustancia" Enabled="true" 
                                    OnClick="btnDeleteEncyclo_OnClick" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </td>
        
            <td style="vertical-align:top">
                <asp:GridView ID="grdEncyclopedias" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                    Font-Names="Trebuchet MS" Font-Size="Smaller" DataKeyNames="EncyclopediaId" HorizontalAlign="Left" OnPageIndexChanging="grdEncyclopedia_PageIndexChanging"
                    OnRowDataBound="grdEncyclopedias_RowDataBound">
                    <Columns>
                        <asp:BoundField DataField="EncyclopediaId" Visible="False" />
                        <asp:TemplateField HeaderText="Nombre de la enciclopedia">
                            <HeaderStyle HorizontalAlign="Left" BackColor="#2694B4" ForeColor="White" />
                            <ItemStyle  Width="250px" />
                            <ItemTemplate>
                                <asp:Label ID="lblActiveSubstance" runat="server">
                                    <%# DataBinder.Eval(Container.DataItem, "EncyclopediaName")%>
                                </asp:Label></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="Agregar">
                            <HeaderStyle Width="100px" HorizontalAlign="Center" BackColor="#2694B4" ForeColor="White" />
                            <ItemStyle HorizontalAlign="Center" Wrap="True" />
                            <ItemTemplate>
                                <asp:ImageButton ID="btnAddEncyclopedia" runat="server" CommandName="Add" ImageUrl="~/Images/agregar.png" AlternateText="Agregar a producto" 
                                    Enabled="true" Width="20px" OnClick="btnAddEncyclopedia_OnClick" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
    </table>
</asp:Content>

