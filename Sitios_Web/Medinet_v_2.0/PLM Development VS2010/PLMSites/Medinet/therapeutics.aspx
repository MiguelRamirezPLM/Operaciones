<%@ Page Language="C#" MasterPageFile="~/medinet.master" AutoEventWireup="true" CodeFile="therapeutics.aspx.cs" Inherits="therapeutics" %>
<%@ MasterType VirtualPath="~/medinet.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <script type="text/javascript">

        function confirmDelete(deleteName) {
            var agree = confirm('Has elegido eliminar a: ' + deleteName + ', ¿Confirmas que deseas eliminar ese rubro ATC de éste producto?');
            if (agree)
                return true;
            else
                return false;
        }

    </script>

    <asp:Label ID="Label4" runat="server" Font-Names="Trebuchet MS" Font-Size="X-Large" Font-Bold="true" Text="Índice terapéutico" />
    <br />
    <br />
    <asp:Label ID="Label5" runat="server" Font-Names="Trebuchet MS" Font-Size="Medium" Text="Producto: " />
    <asp:Label ID="lblBrand" runat="server" Font-Names="Trebuchet MS" Font-Size="Medium" Font-Bold="true" />
    <br />
    <asp:Label ID="Label6" runat="server" Font-Names="Trebuchet MS" Font-Size="Medium" Text="Forma farmacéutica: " />
    <asp:Label ID="lblPharmaForm" runat="server" Font-Names="Trebuchet MS" Font-Size="Medium" Font-Bold="true" />
    <br />
    <br />

    <asp:DataList ID="dlProductSpinoffs" runat="server" BackColor="White" BorderColor="#000000" BorderStyle="None" BorderWidth="1px" CellPadding="3" Font-Names="Trebuchet MS" Font-Size="Smaller"
        RepeatDirection="Horizontal" RepeatLayout="Table" RepeatColumns="10" DataKeyField="EditionId">

        <HeaderStyle HorizontalAlign="Left" BackColor="#2694B4" ForeColor="White" Font-Size="Medium" />
        
        <HeaderTemplate>
            Lista de Spinoffs
        </HeaderTemplate>
        
        <ItemStyle VerticalAlign="Top" HorizontalAlign="Center" />
        
        <ItemTemplate>

             <b><%# DataBinder.Eval(Container.DataItem, "ShortName") %></b>
             <br />
             <asp:CheckBox ID="chkParticipant" runat="server" AutoPostBack="false"  Enabled="true" Checked='<%# displayParticipate((int)DataBinder.Eval(Container.DataItem, "Participate")) %>' />
            
        </ItemTemplate>
         
    </asp:DataList>
    <br />
    <asp:Button ID="btnSaveSpinoffs" runat="server" Text="Guardar Spinoffs" OnClick="btnSaveSpinoffs_OnClick" />

    <br />
    <br />
    <br />
    <br />

    <asp:Table ID="Table1" runat="server" BackColor="White" BorderColor="#000000" BorderStyle="None" BorderWidth="1px" CellPadding="3" Font-Names="Trebuchet MS" Font-Size="Smaller">
        <asp:TableRow>
            <asp:TableCell>
                <asp:ImageButton ID="imgBtnBackProducts" runat="server" ImageUrl="images/regresar.png" ToolTip="Regresar a productos" OnClick="imgBtnBackProducts_Click" />
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
            <asp:Button ID="btnICD" runat="server" Text="Ir al índice CIE-10" OnClick="btnIcd_Click"  />        
            </asp:TableCell>
             <asp:TableCell>
            <asp:Button ID="btnSymptomps" runat="server" Text="Ir al índice de Síntomas"  OnClick="btnSymptoms_Click" />        
            </asp:TableCell>
                <asp:TableCell>
            <asp:Button ID="btnEncyclo" runat="server" Text="Ir al índice de Enciclopedias"  OnClick="btnEncyclo_Click" />        
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>

    <br />

    <table cellspacing="25px">
        <tr>
            <td>
                <asp:Label ID="Label1" runat="server" Font-Names="Trebuchet MS" Font-Size="Medium" Font-Bold="true" Text="Rubros asociados" />
                <br />

                <asp:GridView ID="grdProductTherapeutics" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                    Font-Names="Trebuchet MS" Font-Size="Smaller" DataKeyNames="TherapeuticId" HorizontalAlign="Left" OnRowDataBound="grdProductTherapeutics_RowDataBound">
                    <Columns>
                        <asp:BoundField DataField="TherapeuticId" Visible="False" />
                        <asp:TemplateField HeaderText="Clave">
                            <HeaderStyle HorizontalAlign="Left" BackColor="#2694B4" ForeColor="White" />
                            <ItemStyle  Width="75px" />
                            <ItemTemplate>
                                <asp:Label ID="lblTherapeuticKey" runat="server">
                                    <%# DataBinder.Eval(Container.DataItem, "TherapeuticKey")%>
                                </asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Nombre">
                            <HeaderStyle HorizontalAlign="Left" BackColor="#2694B4" ForeColor="White" />
                            <ItemStyle  Width="300px" />
                            <ItemTemplate>
                                <asp:Label ID="lblTheraDescription" runat="server">
                                    <%# DataBinder.Eval(Container.DataItem, "SpanishDescription")%>
                                </asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Eliminar">
                            <HeaderStyle Width="45px" HorizontalAlign="Center" BackColor="#2694B4" ForeColor="White" />
                            <ItemStyle HorizontalAlign="Center" Wrap="True" />
                            <ItemTemplate>
                                <asp:ImageButton ID="btnDeleteTherapeutic" runat="server" CommandName="Delete" ImageUrl="~/Images/delete.gif" AlternateText="Eliminar terapéutico" 
                                    Enabled="true" OnClick="btnDeleteTherapeutic_OnClick" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label7" runat="server" Font-Names="Trebuchet MS" Font-Size="Large" Font-Bold="true" Text="Árbol terapéutico" />
                <br />
                <asp:TreeView ID="ATCTree" runat="server" CssClass="label" ExpandDepth="0" Font-Names="Trebuchet MS" Font-Size="Small" ForeColor="#2694B4" Font-Bold="true" 
                    OnTreeNodePopulate="ATCTree_TreeNodePopulate" OnSelectedNodeChanged="ATCTree_SelectedNodeChanged" />
            </td>
        </tr>
    </table>

</asp:Content>
