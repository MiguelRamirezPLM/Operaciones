<%@ Page Title="" Language="C#" MasterPageFile="~/medinet.master" AutoEventWireup="true" CodeFile="symptoms.aspx.cs" Inherits="symptoms" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script type="text/javascript">

       function confirmDelete(deleteName) {
           var agree = confirm('Has elegido eliminar a: ' + deleteName + ', ¿Confirmas que deseas eliminar este Síntoma de éste producto?');
           if (agree)
               return true;
           else
               return false;
       }

    </script>

    <asp:Label ID="Label1" runat="server" Font-Names="Trebuchet MS" Font-Size="X-Large" Font-Bold="true" Text="Índice de Síntomas" />
    <br />
    <br />
    <asp:Label ID="Label4" runat="server" Font-Names="Trebuchet MS" Font-Size="Medium" Text="Producto: " />
    <asp:Label ID="lblBrand" runat="server" Font-Names="Trebuchet MS" Font-Size="Medium" Font-Bold="true" />
    <br />
    <asp:Label ID="Label5" runat="server" Font-Names="Trebuchet MS" Font-Size="Medium" Text="Forma farmacéutica: " />
    <asp:Label ID="lblPharmaForm" runat="server" Font-Names="Trebuchet MS" Font-Size="Medium" Font-Bold="true" />
    <br />
    <br />

    <%--<asp:DataList ID="dlProductSpinoffs" runat="server" BackColor="White" BorderColor="#000000" BorderStyle="None" BorderWidth="1px" CellPadding="3" Font-Names="Trebuchet MS" Font-Size="Smaller"
        RepeatDirection="Horizontal" RepeatLayout="Table" RepeatColumns="10" DataKeyField="EditionId">

        <HeaderStyle HorizontalAlign="Left" BackColor="#2694B4" ForeColor="White" Font-Size="Medium" />
        
        <HeaderTemplate>
            Lista de Spinoffs
        </HeaderTemplate>
        
        <ItemStyle VerticalAlign="Top" HorizontalAlign="Center" />
        
        <ItemTemplate>

             <b><%# DataBinder.Eval(Container.DataItem, "ShortName")%></b>
             <br />
             <%--<asp:CheckBox ID="chkParticipant" runat="server" AutoPostBack="false"  Enabled="true" Checked='<%# displayParticipate((int)DataBinder.Eval(Container.DataItem, "Participate")) %>' />
            
        </ItemTemplate>
         
    </asp:DataList>
    <br />
    <asp:Button ID="btnSaveSpinoffs" runat="server" Text="Guardar Spinoffs"   />

    <br />
    <br />
    <br />
    <br />--%>

    <asp:Table ID="Table1" runat="server" BackColor="White" BorderColor="#000000" BorderStyle="None" BorderWidth="1px" CellPadding="3" 
        Font-Names="Trebuchet MS" Font-Size="Smaller">
        <asp:TableRow HorizontalAlign="Center">
            <asp:TableCell>
                <asp:ImageButton ID="imgBtnBackProducts" runat="server" ImageUrl="images/regresar.png" ToolTip="Regresar a productos"  OnClick="imgBtnBackProducts_Click"/>
            </asp:TableCell>
            <asp:TableCell>
            <asp:Button ID="btnIndications" runat="server" Text="Ir al índice de Indicaciones"  OnClick="btnIndicationsIndex_Click" />        
            </asp:TableCell>
            <asp:TableCell>
                <asp:Button ID="btnTherapeuticsIndex" runat="server" Text="Ir al índice Terapéutico" OnClick="btnTherapeuticsIndex_Click"/>
            </asp:TableCell>
            <asp:TableCell>
                <asp:Button ID="btnSubstancesIndex" runat="server" Text="Ir al índice de Sustancias" OnClick="btnSubstancesIndex_Click"  />
            </asp:TableCell>
             <asp:TableCell>
            <asp:Button ID="btnInteractions" runat="server" Text="Ir al índice de Interacciones"  OnClick="btnInteractions_Click"  />        
            </asp:TableCell>
            <asp:TableCell>
            <asp:Button ID="btnContraindication" runat="server" Text="Ir al índice de Contraindicaciones" OnClick="btnContraindications_Click"  />        
            </asp:TableCell> 
            <asp:TableCell>
            <asp:Button ID="btnICD" runat="server" Text="Ir al índice CIE-10" OnClick="btnIcd_Click"  />        
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
                <asp:Label ID="Label6" runat="server" Font-Names="Trebuchet MS" Font-Size="Medium" Font-Bold="true" Text="Síntomas asociados" />
                <br />
                <asp:GridView ID="grdProductSymptoms" runat="server" 
                    AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" 
                    BorderStyle="None" BorderWidth="1px" CellPadding="3"
                    Font-Names="Trebuchet MS" Font-Size="Smaller" DataKeyNames="SymptomId" 
                    HorizontalAlign="Left" onrowdatabound="grdProductSymptoms_RowDataBound">
                    <Columns>
                        <asp:BoundField DataField="SymptomId" Visible="False" />
                        <asp:TemplateField HeaderText="Nombre del Síntoma">
                            <HeaderStyle HorizontalAlign="Left" BackColor="#2694B4" ForeColor="White" />
                            <ItemStyle  Width="250px" />
                            <ItemTemplate>
                                <asp:Label ID="lblSymptom" runat="server">
                                    <%# DataBinder.Eval(Container.DataItem, "SymptomName")%>
                                </asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Eliminar">
                            <HeaderStyle Width="45px" HorizontalAlign="Center" BackColor="#2694B4" ForeColor="White" />
                            <ItemStyle HorizontalAlign="Center" Wrap="True" />
                            <ItemTemplate>
                                <asp:ImageButton ID="btnDeleteSymptom" runat="server"  OnClick="btnDeleteSymptom_OnClick" ImageUrl="~/Images/delete.gif" AlternateText="Eliminar Síntoma" 
                                Enabled="true" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Panel ID="pnlSearch" runat="server" DefaultButton="btnSymptom">
                    <asp:Label ID="Label7" runat="server" Font-Names="Trebuchet MS" Font-Size="Large" Font-Bold="true" Text="Catálogo de Síntomas" />
                    <asp:Table ID="Table2" runat="server" Width="250px">
                        <asp:TableRow>
                            <asp:TableCell HorizontalAlign="Left">
                                <asp:Label ID="Label2" runat="server" Text="Buscar Síntoma" Font-Names="Trebuchet MS" Font-Size="Smaller" Font-Bold="true" />
                            </asp:TableCell><asp:TableCell HorizontalAlign="Right">
                                <asp:Label ID="Label3" runat="server" Text="Registros por página" Font-Names="Trebuchet MS" Font-Size="Smaller" Font-Bold="true" />
                            </asp:TableCell></asp:TableRow><asp:TableRow>
                            <asp:TableCell HorizontalAlign="Left">
                                <asp:TextBox ID="txtSymptomName" runat="server" Text="" Enabled="true" />
                                <asp:Button ID="btnSymptom" runat="server" ToolTip="Consultar" Text="Consultar"  OnClick="btnSymptom_Click" />
                            </asp:TableCell><asp:TableCell HorizontalAlign="Right">
                                <asp:DropDownList ID="ddlPageSize" runat="server" AutoPostBack="True" Font-Names="Arial" Font-Size="10pt" OnSelectedIndexChanged="ddlPageSize_SelectedIndexChanged">
                                    <asp:ListItem Value="-1" >Ilimitado</asp:ListItem>
                                    <asp:ListItem Value="5">5</asp:ListItem>
                                    <asp:ListItem Value="15" Selected="True">15</asp:ListItem>
                                    <asp:ListItem Value="25">25</asp:ListItem>
                                    <asp:ListItem Value="50">50</asp:ListItem>
                                    <asp:ListItem Value="100">100</asp:ListItem>
                                </asp:DropDownList>
                            </asp:TableCell></asp:TableRow></asp:Table></asp:Panel><asp:GridView 
                    ID="grdSymptom" runat="server" AutoGenerateColumns="False" BackColor="White" 
                    BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                    Font-Names="Trebuchet MS" Font-Size="Smaller" DataKeyNames="SymptomId" 
                    HorizontalAlign="Left" onpageindexchanging="grdSymptom_PageIndexChanging" onrowdatabound="grdSymptom_RowDataBound" 
                  ><Columns>
                        <asp:BoundField DataField="SymptomId" Visible="False" />
                        <asp:TemplateField HeaderText="Nombre del Síntoma">
                            <HeaderStyle HorizontalAlign="Left" BackColor="#2694B4" ForeColor="White" />
                            <ItemStyle  Width="250px" />
                            <ItemTemplate>
                                <asp:Label ID="lblSymptom" runat="server">
                                    <%# DataBinder.Eval(Container.DataItem, "SymptomName")%>
                                </asp:Label></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="Agregar a producto">
                            <HeaderStyle Width="100px" HorizontalAlign="Center" BackColor="#2694B4" ForeColor="White" />
                            <ItemStyle HorizontalAlign="Center" Wrap="True" />
                            <ItemTemplate>
                                <asp:ImageButton ID="btnAddSymptom" runat="server" CommandName="Add" ImageUrl="~/Images/agregar.png" AlternateText="Agregar a producto" 
                                    Enabled="true" Width="20px" OnClick="btnAddSym_OnClick" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
    </table>
</asp:Content>

