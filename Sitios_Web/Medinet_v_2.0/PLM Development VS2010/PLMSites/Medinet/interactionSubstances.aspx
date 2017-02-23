<%@ Page Title="" Language="C#" MasterPageFile="~/medinet.master" AutoEventWireup="true" CodeFile="interactionSubstances.aspx.cs" Inherits="interactionSubstances" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script type="text/javascript">

    function confirmDelete(deleteName) {
        var agree = confirm('Has elegido eliminar a: ' + deleteName + ', ¿Confirmas que deseas eliminar esa interaccion con la sustancia de éste producto?');
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
 <script language="javascript"  type="text/javascript">

     function imprSelec(muestra) {
         var ficha = document.getElementById(muestra);
         var ventimp = window.open(' ', 'popimpr');
         var tex = ficha.innerHTML;
         var tex = tex + "";
         var fin = tex.replace("overflow:auto;", " ");
         fin = fin.replace("border:1px solid #999999"," ");
         ventimp.document.write(fin);
         ventimp.document.close();
         ventimp.print();
         ventimp.close();

     }
     function viewMax(muestra) {
         var ficha = document.getElementById(muestra);
         var ventimp = window.open(' ', 'popimpr');
         var tex = ficha.innerHTML;
         
         var tex = tex + "";
         var fin = tex.replace("overflow:auto;", " ");
         fin = fin.replace("border:1px solid #999999", " ");
         fin = "<html> <head><link href='México/estilos.css' rel='stylesheet' type='text/css' media='screen' /><title>Untitled Page</title>"
        +"</head><body> "
        +"<p align='right'><input id='btnPP' type='button' value='Imprimir' onclick='javascript:window.print()' /></p>"
        +fin;
        fin=fin+"</body></html>"
        ventimp.document.write(fin);
     }
 </script>
       <asp:Table ID="Table5" runat="server" BackColor="White" BorderColor="#000000" BorderStyle="None" BorderWidth="1px" CellPadding="3" 
        Font-Names="Trebuchet MS" Font-Size="Smaller">
        <asp:TableRow HorizontalAlign="Center">
            <asp:TableCell>
                <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="images/regresar.png" ToolTip="Regresar a productos"  OnClick="imgBtnBackProducts_Click"/>
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
            <asp:Button ID="btnContraindications" runat="server" Text="Ir al índice de Contraindicaciones"  OnClick="btnContraindications_Click"  />        
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
            <asp:TableCell>
            <asp:Button ID="btnOMSThera" runat="server" Text="Ir al índice OMS Terapéutico"  OnClick="btnOMSThera_Click" />        
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
   
    <table width="85%" >
        <tr>
            <td style="width: 392px" valign="top">
 
            <asp:Label ID="Label1" runat="server" Font-Names="Trebuchet MS" Font-Size="X-Large" Font-Bold="true" Text="Interacciones medicamentosas" />
            </td>
            <td align="center">
                <input id="btnP" type="button" value="Imprimir" onclick="javascript:imprSelec('print')" />
                <input id="btnM" type="button" value="Visualizar" onclick="javascript:viewMax('print')" />
                </td>
            
        </tr>
        <tr>
            <td style="width: 392px" valign="top">
    <asp:Label ID="Label4" runat="server" Font-Names="Trebuchet MS" Font-Size="Medium" Text="Producto: " />
    <asp:Label ID="lblBrand" runat="server" Font-Names="Trebuchet MS" Font-Size="Medium" Font-Bold="true" />
            </td>
                   <td  align="right" rowspan="3" style="width: 734px"  >
                   <div id="print">
                <div runat="server" id="div" 
            style="width: 100%; height: 212px; overflow:auto; border:1px solid #999999"></div>
            </div>
                       <asp:Button ID="btnNoIM" runat="server" Text="No reporta interacciones" 
                           onclick="btnNoIM_Click" />
&nbsp;</td>
            
        </tr>
        
        <tr>
            <td style="width: 392px" valign="top">
    <asp:Label ID="Label5" runat="server" Font-Names="Trebuchet MS" Font-Size="Medium" Text="Forma farmacéutica: " />
    <asp:Label ID="lblPharmaForm" runat="server" Font-Names="Trebuchet MS" Font-Size="Medium" Font-Bold="true" />
            </td>
            
        </tr>
        <tr>
        <td valign="top" style="width: 392px">
        <asp:GridView ID="grdSubstancesProduct" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                    Font-Names="Trebuchet MS" Font-Size="Smaller" DataKeyNames="ActiveSubstanceId" HorizontalAlign="Left" >
                    <Columns>
                        <asp:BoundField DataField="ActiveSubstanceId" Visible="False" />
                        <asp:TemplateField HeaderText="Sustancias del producto">
                            <HeaderStyle HorizontalAlign="Left" BackColor="#2694B4" ForeColor="White" />
                            <ItemStyle  Width="250px" />
                            <ItemTemplate>
                                <asp:Label ID="lblActiveSubstance" runat="server">
                                    <%# DataBinder.Eval(Container.DataItem, "Description")%>
                                </asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
        </td>
        </tr>
    </table>
   
    <asp:Table ID="Table1" runat="server" BackColor="White" BorderColor="#999999" 
        BorderStyle="None" BorderWidth="5px" CellPadding="3"
        Font-Names="Trebuchet MS" Font-Size="Smaller" Width="155px" Height="31px">
        <asp:TableRow HorizontalAlign="Left">
            <asp:TableCell>
                <asp:ImageButton ID="imgBtnBackProducts" runat="server" ImageUrl="images/regresar.png" ToolTip="Regresar a productos" OnClick="imgBtnBackProducts_Click" />
            </asp:TableCell>
        
           
        </asp:TableRow>
        

      
    </asp:Table>
     
    
     
    <table cellspacing="25px">
    
        <tr>
            <td style="width: 439px" valign="top" align="center">
                <asp:Label ID="Label6" runat="server" Font-Names="Trebuchet MS" 
                    Font-Size="Medium" Font-Bold="True" 
                    Text="Interacciones con sustancias" />
                <br />
                <asp:GridView ID="grdProductSubstances" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                    Font-Names="Trebuchet MS" Font-Size="Smaller" DataKeyNames="ActiveSubstanceId" HorizontalAlign="Left" OnRowDataBound="grdProductSubstances_RowDataBound">
                    <Columns>
                        <asp:BoundField DataField="ActiveSubstanceId" Visible="False" />
                        <asp:TemplateField HeaderText="Sustancia del producto">
                            <HeaderStyle HorizontalAlign="Left" BackColor="#2694B4" ForeColor="White" />
                            <ItemStyle  Width="250px" />
                            <ItemTemplate>
                                <asp:Label ID="lblActiveSubstance" runat="server">
                                    <%# DataBinder.Eval(Container.DataItem, "ActiveSubstance")%>
                                </asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Sustancia">
                            <HeaderStyle HorizontalAlign="Left" BackColor="#2694B4" ForeColor="White" />
                            <ItemStyle  Width="250px" />
                            <ItemTemplate>
                                <asp:Label ID="lblActiveSubstances" runat="server">
                                    <%# DataBinder.Eval(Container.DataItem, "SubstanceInteraction")%>
                                </asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Eliminar">
                            <HeaderStyle Width="45px" HorizontalAlign="Center" BackColor="#2694B4" ForeColor="White" />
                            <ItemStyle HorizontalAlign="Center" Wrap="True" />
                            <ItemTemplate>
                                <asp:ImageButton ID="btnDeleteSubstance" runat="server" CommandName="Delete" ImageUrl="~/Images/delete.gif" AlternateText="Eliminar sustancia" Enabled="true" 
                                  OnClick="btnDeleteSubstance_OnClick" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </td>

            <td style="width: 546px" valign="top" align="center">
                <asp:Label ID="Label8" runat="server" Font-Names="Trebuchet MS" 
                    Font-Size="Medium" Font-Bold="True" 
                    Text="Interacciones con grupos farmacológicos" />
                <br />
                <asp:GridView ID="gdvpharmacol" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                    Font-Names="Trebuchet MS" Font-Size="Smaller" DataKeyNames="ActiveSubstanceId" HorizontalAlign="Left" OnRowDataBound="grdProductPharmacol_RowDataBound">
                    <Columns>
                        <asp:BoundField DataField="ActiveSubstanceId" Visible="False" />
                        <asp:TemplateField HeaderText="Sustancia del producto">
                            <HeaderStyle HorizontalAlign="Left" BackColor="#2694B4" ForeColor="White" />
                            <ItemStyle  Width="250px" />
                            <ItemTemplate>
                                <asp:Label ID="lblActiveSubstance" runat="server">
                                    <%# DataBinder.Eval(Container.DataItem, "ActiveSubstance")%>
                                </asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Grupo farmacólogico">
                            <HeaderStyle HorizontalAlign="Left" BackColor="#2694B4" ForeColor="White" />
                            <ItemStyle  Width="250px" />
                            <ItemTemplate>
                                <asp:Label ID="lblActiveSubstances" runat="server">
                                    <%# DataBinder.Eval(Container.DataItem, "GroupName")%>
                                </asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Eliminar">
                            <HeaderStyle Width="45px" HorizontalAlign="Center" BackColor="#2694B4" ForeColor="White" />
                            <ItemStyle HorizontalAlign="Center" Wrap="True" />
                            <ItemTemplate>
                                <asp:ImageButton ID="btnDeleteGroup" runat="server" CommandName="Delete" ImageUrl="~/Images/delete.gif" AlternateText="Eliminar sustancia" Enabled="true" 
                                      OnClick="btnDeleteGroup_OnClick" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </td>
            <td style="width: 474px" valign="top" align="center">
                <asp:Label ID="Label12" runat="server" Font-Names="Trebuchet MS" 
                    Font-Size="Medium" Font-Bold="True" 
                    Text="Interacciones con otros elementos" />
                <br />
                <asp:GridView ID="grdProductOthers" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                    Font-Names="Trebuchet MS" Font-Size="Smaller" DataKeyNames="ElementId" HorizontalAlign="Left" OnRowDataBound="grdProductOthers_RowDataBound">
                    <Columns>
                        <asp:BoundField DataField="ElementId" Visible="False" />
                        <asp:TemplateField HeaderText="Sustancia del producto">
                            <HeaderStyle HorizontalAlign="Left" BackColor="#2694B4" ForeColor="White" />
                            <ItemStyle  Width="250px" />
                            <ItemTemplate>
                                <asp:Label ID="lblActiveSubstance" runat="server">
                                    <%# DataBinder.Eval(Container.DataItem, "ActiveSubstance")%>
                                </asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Elemento">
                            <HeaderStyle HorizontalAlign="Left" BackColor="#2694B4" ForeColor="White" />
                            <ItemStyle  Width="250px" />
                            <ItemTemplate>
                                <asp:Label ID="lblActiveSubstances" runat="server">
                                    <%# DataBinder.Eval(Container.DataItem, "ElementName")%>
                                </asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Eliminar">
                            <HeaderStyle Width="45px" HorizontalAlign="Center" BackColor="#2694B4" ForeColor="White" />
                            <ItemStyle HorizontalAlign="Center" Wrap="True" />
                            <ItemTemplate>
                                <asp:ImageButton ID="btnDeleteElement" runat="server" CommandName="Delete" ImageUrl="~/Images/delete.gif" AlternateText="Eliminar sustancia" Enabled="true" 
                                    OnClick="btnDeleteElement_OnClick"  />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
        
        <tr>
        <td></td>
        <td style="width: 546px" >
        <asp:Label ID="lblAdvert" runat="server" Font-Names="Trebuchet MS" 
                Font-Size="Medium" Font-Bold="True" Font-Italic="True" Font-Strikeout="False" 
                Font-Underline="False" Visible="False"></asp:Label>
        <asp:Label ID="lblItem" runat="server" Font-Names="Trebuchet MS" 
                Font-Size="Medium" Font-Bold="True" Font-Italic="True" Font-Strikeout="False" 
                Font-Underline="False" Visible="False" ForeColor="Red"></asp:Label>
            <asp:CheckBoxList ID="BtnListSubstances" runat="server">
            </asp:CheckBoxList>
            <asp:Button ID="btnContinuar" runat="server" Text="Continuar" Visible="False" 
                onclick="btnContinuar_Click" />
            <asp:Button ID="btnContinuarGroup" runat="server" 
                onclick="btnContinuarGroup_Click" Text="Continuar" Visible="False" />
            <asp:Button ID="btnContinuarOther" runat="server" Text="Continuar" 
                Visible="False" onclick="btnContinuarOther_Click" />
            <asp:Button ID="btnCancel" runat="server" onclick="btnCancel_Click" 
                Text="Cancelar" Visible="False" />
        </td>
        <td style="width: 474px"></td>
        </tr>
        <tr>
            <td style="width: 439px" valign="top"   >
                <asp:Panel ID="pnlSearch" runat="server" DefaultButton="btnSubstance">
                    <asp:Label ID="Label7" runat="server" Font-Names="Trebuchet MS" Font-Size="Large" Font-Bold="true" Text="Catálogo de sustancias activas" />
                    <asp:Table ID="Table2" runat="server" Width="300px">
                        <asp:TableRow>
                            <asp:TableCell HorizontalAlign="Left">
                                <asp:Label ID="Label2" runat="server" Text="Buscar una sustancia" Font-Names="Trebuchet MS" Font-Size="Smaller" Font-Bold="true" />
                            </asp:TableCell><asp:TableCell HorizontalAlign="Right">
                                <asp:Label ID="Label3" runat="server" Text="Registros por página" Font-Names="Trebuchet MS" Font-Size="Smaller" Font-Bold="true" />
                            </asp:TableCell></asp:TableRow><asp:TableRow>
                            <asp:TableCell HorizontalAlign="Left">
                                <asp:TextBox ID="txtSubstanceName" runat="server" Text="" Enabled="true" />
                                <asp:Button ID="btnSubstance" runat="server" ToolTip="Consultar" Text="Consultar"  OnClick="btnFindSubstance_Click" />
                            </asp:TableCell><asp:TableCell HorizontalAlign="Right">
                                <asp:DropDownList ID="ddlPageSize" runat="server" AutoPostBack="True" Font-Names="Arial" Font-Size="10pt" OnSelectedIndexChanged="ddlPageSubstance_SelectedIndexChanged" >
                                    <asp:ListItem Value="-1" >Ilimitado</asp:ListItem>
                                    <asp:ListItem Value="5">5</asp:ListItem>
                                    <asp:ListItem Value="15" Selected="True">15</asp:ListItem>
                                    <asp:ListItem Value="25">25</asp:ListItem>
                                    <asp:ListItem Value="50">50</asp:ListItem>
                                    <asp:ListItem Value="100">100</asp:ListItem>
                                </asp:DropDownList>
                            </asp:TableCell></asp:TableRow></asp:Table></asp:Panel><asp:GridView ID="grdSubstances" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                    Font-Names="Trebuchet MS" Font-Size="Smaller" DataKeyNames="ActiveSubstanceId" HorizontalAlign="Left" OnPageIndexChanging="grdSubstances_PageIndexChanging"
                    OnRowDataBound="grdSubstances_RowDataBound">
                    <Columns>
                        <asp:BoundField DataField="ActiveSubstanceId" Visible="False" />
                        <asp:TemplateField HeaderText="Nombre de la sustancia">
                            <HeaderStyle HorizontalAlign="Left" BackColor="#2694B4" ForeColor="White" />
                            <ItemStyle  Width="250px" />
                            <ItemTemplate>
                                <asp:Label ID="lblActiveSubstance" runat="server">
                                    <%# DataBinder.Eval(Container.DataItem, "Description")%>
                                </asp:Label></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="Agregar interacción">
                            <HeaderStyle Width="100px" HorizontalAlign="Center" BackColor="#2694B4" ForeColor="White" />
                            <ItemStyle HorizontalAlign="Center" Wrap="True" />
                            <ItemTemplate>
                                <asp:ImageButton ID="btnAddSubstance" runat="server" CommandName="Add" ImageUrl="~/Images/agregar.png" AlternateText="Agregar a producto" 
                                    Enabled="true" Width="20px" OnClick="btnAddSubstance_click"/>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </td>
             <td style="width: 546px" valign="top"  ><asp:Panel ID="Panel1" runat="server" DefaultButton="btnFindPharma">
                    <asp:Label ID="Label9" runat="server" Font-Names="Trebuchet MS" Font-Size="Large" Font-Bold="true" Text="Catálogo de grupos farmacológicos IPPA" />
                    <asp:Table ID="Table3" runat="server" Width="300px">
                        <asp:TableRow>
                            <asp:TableCell HorizontalAlign="Left">
                                <asp:Label ID="Label10" runat="server" Text="Buscar grupo farmacologico" Font-Names="Trebuchet MS" Font-Size="Smaller" Font-Bold="true" />
                            </asp:TableCell><asp:TableCell HorizontalAlign="Right">
                                <asp:Label ID="Label11" runat="server" Text="Registros por página" Font-Names="Trebuchet MS" Font-Size="Smaller" Font-Bold="true" />
                            </asp:TableCell></asp:TableRow><asp:TableRow>
                            <asp:TableCell HorizontalAlign="Left">
                                <asp:TextBox ID="txtPharmaGroup" runat="server" Text="" Enabled="true" />
                                <asp:Button ID="btnFindPharma" runat="server" ToolTip="Consultar" Text="Consultar"  OnClick="btnFindGroup_Click" />
                            </asp:TableCell><asp:TableCell HorizontalAlign="Right">
                                <asp:DropDownList ID="ddlPharmaGroup" runat="server" AutoPostBack="True" Font-Names="Arial" Font-Size="10pt" OnSelectedIndexChanged="ddlPageGroup_SelectedIndexChanged" >
                                    <asp:ListItem Value="-1" >Ilimitado</asp:ListItem>
                                    <asp:ListItem Value="5">5</asp:ListItem>
                                    <asp:ListItem Value="15" Selected="True">15</asp:ListItem>
                                    <asp:ListItem Value="25">25</asp:ListItem>
                                    <asp:ListItem Value="50">50</asp:ListItem>
                                    <asp:ListItem Value="100">100</asp:ListItem>
                                </asp:DropDownList>
                            </asp:TableCell></asp:TableRow></asp:Table></asp:Panel><asp:GridView ID="grdPharmaGroup" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                    Font-Names="Trebuchet MS" Font-Size="Smaller" DataKeyNames="PharmaGroupId" HorizontalAlign="Left" OnPageIndexChanging="grdPharmaGroup_PageIndexChanging"
                    OnRowDataBound="grdPharmaGroup_RowDataBound">
                    <Columns>
                        <asp:BoundField DataField="PharmaGroupId" Visible="False" />
                        <asp:TemplateField HeaderText="Nombre del grupo farmacológico">
                            <HeaderStyle HorizontalAlign="Left" BackColor="#2694B4" ForeColor="White" />
                            <ItemStyle  Width="250px" />
                            <ItemTemplate>
                                <asp:Label ID="lblActiveSubstance" runat="server">
                                    <%# DataBinder.Eval(Container.DataItem, "GroupName")%>
                                </asp:Label></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="Agregar interacción">
                            <HeaderStyle Width="100px" HorizontalAlign="Center" BackColor="#2694B4" ForeColor="White" />
                            <ItemStyle HorizontalAlign="Center" Wrap="True" />
                            <ItemTemplate>
                                <asp:ImageButton ID="btnAddGroup" runat="server" CommandName="Add" ImageUrl="~/Images/agregar.png" AlternateText="Agregar a producto" 
                                    Enabled="true" Width="20px" OnClick="btnAddGroup_click"/>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </td>
            <td style="width: 474px" valign="top" ><asp:Panel ID="Panel2" runat="server" DefaultButton="btnFindOther">
                    <asp:Label ID="Label13" runat="server" Font-Names="Trebuchet MS" Font-Size="Large" Font-Bold="true" Text="Catálogo de otros elementos" />
                    <asp:Table ID="Table4" runat="server" Width="300px">
                        <asp:TableRow>
                            <asp:TableCell HorizontalAlign="Left">
                                <asp:Label ID="Label14" runat="server" Text="Buscar elemento" Font-Names="Trebuchet MS" Font-Size="Smaller" Font-Bold="true" />
                            </asp:TableCell><asp:TableCell HorizontalAlign="Right">
                                <asp:Label ID="Label15" runat="server" Text="Registros por página" Font-Names="Trebuchet MS" Font-Size="Smaller" Font-Bold="true" />
                            </asp:TableCell></asp:TableRow><asp:TableRow>
                            <asp:TableCell HorizontalAlign="Left">
                                <asp:TextBox ID="txtotherName" runat="server" Text="" Enabled="true" />
                                <asp:Button ID="btnFindOther" runat="server" ToolTip="Consultar" Text="Consultar"  OnClick="btnFindElement_Click" />
                            </asp:TableCell><asp:TableCell HorizontalAlign="Right">
                                <asp:DropDownList ID="ddlOtherPages" runat="server" AutoPostBack="True" Font-Names="Arial" Font-Size="10pt"  OnSelectedIndexChanged="ddlPageOther_SelectedIndexChanged">
                                    <asp:ListItem Value="-1" >Ilimitado</asp:ListItem>
                                    <asp:ListItem Value="5">5</asp:ListItem>
                                    <asp:ListItem Value="15" Selected="True">15</asp:ListItem>
                                    <asp:ListItem Value="25">25</asp:ListItem>
                                    <asp:ListItem Value="50">50</asp:ListItem>
                                    <asp:ListItem Value="100">100</asp:ListItem>
                                </asp:DropDownList>
                            </asp:TableCell></asp:TableRow></asp:Table></asp:Panel><asp:GridView ID="grdOtherElements" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                    Font-Names="Trebuchet MS" Font-Size="Smaller" DataKeyNames="ElementId" HorizontalAlign="Left" OnPageIndexChanging="grdOtherElements_PageIndexChanging"
                    OnRowDataBound="grdOtherElements_RowDataBound">
                    <Columns>
                        <asp:BoundField DataField="ElementId" Visible="False" />
                        <asp:TemplateField HeaderText="Nombre del elemento">
                            <HeaderStyle HorizontalAlign="Left" BackColor="#2694B4" ForeColor="White" />
                            <ItemStyle  Width="250px" />
                            <ItemTemplate>
                                <asp:Label ID="lblActiveSubstance" runat="server">
                                    <%# DataBinder.Eval(Container.DataItem, "ElementName")%>
                                </asp:Label></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="Agregar interacción">
                            <HeaderStyle Width="100px" HorizontalAlign="Center" BackColor="#2694B4" ForeColor="White" />
                            <ItemStyle HorizontalAlign="Center" Wrap="True" />
                            <ItemTemplate>
                                <asp:ImageButton ID="btnAddElement" runat="server" CommandName="Add" ImageUrl="~/Images/agregar.png" AlternateText="Agregar a producto" 
                                    Enabled="true" Width="20px" OnClick="btnAddElement_click"/>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
    </table>
   
</asp:Content>

