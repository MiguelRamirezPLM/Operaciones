<%@ Page Title="" Language="C#" MasterPageFile="~/medinet.master" AutoEventWireup="true" CodeFile="ICD.aspx.cs" Inherits="ICD" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<script type="text/javascript">

    function confirmDelete(deleteName) {
        var agree = confirm('Has elegido desasociar a: ' + deleteName + ', ¿Confirmas que deseas desasociar este CIE-10 del producto?');
        if (agree)
            return true;
        else
            return false;
    }

    </script>
    <asp:Label ID="Label4" runat="server" Font-Names="Trebuchet MS" Font-Size="X-Large" Font-Bold="true" Text="Índice CIE-10" />
    <br />
    <br />
    <asp:Label ID="Label5" runat="server" Font-Names="Trebuchet MS" Font-Size="Medium" Text="Producto: " />
    <asp:Label ID="lblBrand" runat="server" Font-Names="Trebuchet MS" Font-Size="Medium" Font-Bold="true" />
    <br />
    <asp:Label ID="Label6" runat="server" Font-Names="Trebuchet MS" Font-Size="Medium" Text="Forma farmacéutica: " />
    <asp:Label ID="lblPharmaForm" runat="server" Font-Names="Trebuchet MS" Font-Size="Medium" Font-Bold="true" />
    <br />
    <br />
    <asp:Table ID="Table1" runat="server" BackColor="White" BorderColor="#000000" BorderStyle="None" BorderWidth="1px" CellPadding="3" Font-Names="Trebuchet MS" Font-Size="Smaller">
        <asp:TableRow>
            <asp:TableCell>
                <asp:ImageButton ID="imgBtnBackProducts" runat="server" ImageUrl="images/regresar.png" ToolTip="Regresar a productos" OnClick="imgBtnBackProducts_Click" />
            </asp:TableCell>
            <asp:TableCell>
                <asp:Button ID="btnTherapeuticsIndex" runat="server" Text="Ir al índice Terapéutico" OnClick="btnTherapeuticsIndex_Click" />
            </asp:TableCell>
            <asp:TableCell>
                <asp:Button ID="btnIndicationsIndex" runat="server" Text="Ir al índice de Indicaciones" OnClick="btnIndicationsIndex_Click" />
            </asp:TableCell>
            <asp:TableCell>
                <asp:Button ID="btnSubstancesIndex" runat="server" Text="Ir al índice de Sustancias" OnClick="btnSubstancesIndex_Click" />
            </asp:TableCell>
             <asp:TableCell>
            <asp:Button ID="btnInteractions" runat="server" Text="Ir al índice de Interacciones" OnClick="btnInteractions_Click" />        
            </asp:TableCell>
               <asp:TableCell>
            <asp:Button ID="btnContraindication" runat="server" Text="Ir al índice de Contraindicaciones" OnClick="btnContraindications_Click"  />        
            </asp:TableCell>
             <asp:TableCell>
            <asp:Button ID="btnSymptomps" runat="server" Text="Ir al índice de Síntomas"  OnClick="btnSymptoms_Click" />        
            </asp:TableCell>
                <asp:TableCell>
            <asp:Button ID="btnEncyclo" runat="server" Text="Ir al índice de Enciclopedias"  OnClick="btnEncyclo_Click" />        
            </asp:TableCell>
            <asp:TableCell>
            <asp:Button ID="btnOMSThera" runat="server" Text="Ir al índice OMS Terapéutico"  OnClick="btnOMSThera_Click" />        
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>

       <table cellspacing="0px" cellpadding="50px" style="margin-left:auto;margin-right:auto;width:100%">
        <tr>
            <td style="vertical-align:top">
                <asp:Label ID="Label1" runat="server" Font-Names="Trebuchet MS" Font-Size="Medium" Font-Bold="true" Text="CIE-10 Asociados" />
                <br />

                <asp:GridView ID="grdProductICD" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                    Font-Names="Trebuchet MS" Font-Size="Smaller" DataKeyNames="ICDId" HorizontalAlign="Left" OnRowDataBound="grdProductICD_RowDataBound">
                    <Columns>
                        <asp:BoundField DataField="ICDId" Visible="False" />
                        <asp:TemplateField HeaderText="Clave">
                            <HeaderStyle HorizontalAlign="Left" BackColor="#2694B4" ForeColor="White" />
                            <ItemStyle  Width="75px" />
                            <ItemTemplate>
                                <asp:Label ID="lblICDKey" runat="server">
                                    <%# DataBinder.Eval(Container.DataItem, "ICDKey")%>
                                </asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Descripcíon">
                            <HeaderStyle HorizontalAlign="Left" BackColor="#2694B4" ForeColor="White" />
                            <ItemStyle  Width="300px" />
                            <ItemTemplate>
                                <asp:Label ID="lblICDDescription" runat="server">
                                    <%# DataBinder.Eval(Container.DataItem, "SpanishDescription")%>
                                </asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Eliminar">
                            <HeaderStyle Width="45px" HorizontalAlign="Center" BackColor="#2694B4" ForeColor="White" />
                            <ItemStyle HorizontalAlign="Center" Wrap="True" />
                            <ItemTemplate>
                                <asp:ImageButton ID="btnDeleteICD" runat="server" CommandName="Delete" ImageUrl="~/Images/delete.gif" AlternateText="Eliminar ICD" 
                                    Enabled="true" OnClick="btnDeleteICD_OnClick" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </td>
            <td style="vertical-align:top">
            <asp:Panel ID="pnlSearch" runat="server" DefaultButton="btnSearch">
            <asp:Table ID="tbl" runat="server" Width="400px">
            <asp:TableRow><asp:TableCell> <asp:Label ID="Label2" runat="server" Text="Busqueda de ICE-10" Font-Names="Trebuchet MS" Font-Size="Smaller" Font-Bold="true" /></asp:TableCell>
            <asp:TableCell> <asp:Label ID="lblPage" runat="server" Text="Registros por página" Font-Names="Trebuchet MS" Font-Size="Smaller" Font-Bold="true" /></asp:TableCell></asp:TableRow><asp:TableRow>
            <asp:TableCell HorizontalAlign="Left">
            <asp:TextBox ID="txtSearch" runat="server"></asp:TextBox><asp:Button ID="btnSearch" 
                        runat="server" Text="Buscar" TabIndex="1" 
                        OnClick="btnSearch_OnClick" /><br /><br />
                        </asp:TableCell><asp:TableCell HorizontalAlign="Right">
                        <asp:DropDownList ID="ddlPageSize" runat="server" AutoPostBack="True" Font-Names="Arial" Font-Size="10pt" OnSelectedIndexChanged="ddlPage_SelectedIndexChanged" >
                                    <asp:ListItem Value="-1" >Ilimitado</asp:ListItem>
                                    <asp:ListItem Value="5">5</asp:ListItem>
                                    <asp:ListItem Value="15" Selected="True">15</asp:ListItem>
                                    <asp:ListItem Value="25">25</asp:ListItem>
                                    <asp:ListItem Value="50">50</asp:ListItem>
                                    <asp:ListItem Value="100">100</asp:ListItem>
                                </asp:DropDownList>
                        </asp:TableCell></asp:TableRow></asp:Table></asp:Panel><asp:Button ID="btnSaveCieAsociated" 
                        runat="server" Text="Asociar Seleccionados" TabIndex="1" 
                        OnClick="btnSaveCieAsociated_OnClick" />
                        <asp:Button ID="BtnShowTree" 
                        runat="server" Text="Mostrar Árbol" TabIndex="1" 
                        OnClick="btnShowTree_OnClick" /><br /><br />
                <asp:Label ID="lblHeadCIE" runat="server" Font-Names="Trebuchet MS" Font-Size="Large" Font-Bold="true" Text="Catálogo CIE-10" />
                <br />
                <asp:TreeView ID="ICDTree" runat="server" CssClass="label" ExpandDepth="0" Font-Names="Trebuchet MS" Font-Size="Small" ForeColor="#2694B4" Font-Bold="true" 
                    OnTreeNodePopulate="ICDTree_TreeNodePopulate" OnSelectedNodeChanged="ICDTree_SelectedNodeChanged" />
                    <br />
                <asp:GridView ID="grdResults" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                    Font-Names="Trebuchet MS" Font-Size="Smaller" DataKeyNames="ICDId" HorizontalAlign="Left" OnRowDataBound="grdResults_RowDataBound" OnPageIndexChanging="grdResults_PageIndexChanging">
                    <Columns>
                        <asp:BoundField DataField="ICDId" Visible="False" />
                        <asp:TemplateField HeaderText="Clave">
                            <HeaderStyle HorizontalAlign="Left" BackColor="#2694B4" ForeColor="White" />
                            <ItemStyle  Width="75px" />
                            <ItemTemplate>
                                <asp:Label ID="lblICDKey" runat="server">
                                    <%# DataBinder.Eval(Container.DataItem, "ICDKey")%>
                                </asp:Label></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="Descripcíon">
                            <HeaderStyle HorizontalAlign="Left" BackColor="#2694B4" ForeColor="White" />
                            <ItemStyle  Width="300px" />
                            <ItemTemplate>
                                <asp:Label ID="lblICDDescription" runat="server">
                                    <%# DataBinder.Eval(Container.DataItem, "SpanishDescription")%>
                                </asp:Label></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="Agregar">
                            <HeaderStyle Width="45px" HorizontalAlign="Center" BackColor="#2694B4" ForeColor="White" />
                            <ItemStyle HorizontalAlign="Center" Wrap="True" />
                            <ItemTemplate>
                                <asp:ImageButton ID="btnAddICD" runat="server" ImageUrl="~/Images/agregar.png" AlternateText="Agregar ICD" 
                                    Enabled="true" Width="20px" OnClick="btnAddICD_OnClick" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                
            </td>
        </tr>
        </table>
</asp:Content>

