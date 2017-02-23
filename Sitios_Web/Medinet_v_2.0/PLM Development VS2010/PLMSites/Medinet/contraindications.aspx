<%@ Page Title="" Language="C#" MasterPageFile="~/medinet.master" AutoEventWireup="true" CodeFile="contraindications.aspx.cs" Inherits="contraindications" MaintainScrollPositionOnPostback="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script  src="Scripts/jquery-1.10.2.min.js">
</script>

 <script type="text/javascript">
 
     function confirmDelete(deleteName) {
         var agree = confirm('Has elegido eliminar a: ' + deleteName + ', ¿Confirmas que deseas eliminar la contraindicación de este producto?');
         if (agree)
             return true;
         else
             return false;
     }

     function confirmStatus() {
         var agree = confirm('El producto tiene una clasificación previa , al cambiar el estatus a "No reporta" se eliminará la clasificación actual , ¿Confirmas que deseas cambiar el estatus a "No reporta" para este producto?');
         if (agree)
             return true;
         else
             return false;
     }
     function confirmDeleteAll(deleteSection) {
         var agree = confirm('Al realizar esta acción se eliminarán todas las contraindicaciones '+deleteSection+' asocidas al producto ¿Confirmas que deseas eliminar las contraindicaciones '+deleteSection+' para este producto?');
         if (agree)
             return true;
         else
             return false;
     }

     $(document).ready(function () {
         if ($('#panelHyper').is(':hidden')) { document.images.imgHiper.src = 'Images/downIcon.png'; } else { document.images.imgHiper.src = 'Images/upIcon.png'; }
         if ($('#panelMed').is(':hidden')) { document.images.imgMed.src = 'Images/downIcon.png'; } else { document.images.imgMed.src = 'Images/upIcon.png'; }
         if ($('#panelfis').is(':hidden')) { document.images.imgFis.src = 'Images/downIcon.png'; } else { document.images.imgFis.src = 'Images/upIcon.png'; }
         if ($('#panelPharma').is(':hidden')) { document.images.imgFar.src = 'Images/downIcon.png'; } else { document.images.imgFar.src = 'Images/upIcon.png'; }
         if ($('#panelGroups').is(':hidden')) { document.images.imgGru.src = 'Images/downIcon.png'; } else { document.images.imgGru.src = 'Images/upIcon.png'; }
         if ($('#panelSubs').is(':hidden')) { document.images.imgSus.src = 'Images/downIcon.png'; } else { document.images.imgSus.src = 'Images/upIcon.png'; }
         if ($('#panelOther').is(':hidden')) { document.images.imgOtr.src = 'Images/downIcon.png'; } else { document.images.imgOtr.src = 'Images/upIcon.png'; }
         if ($('#panelCom').is(':hidden')) { document.images.imgCom.src = 'Images/downIcon.png'; } else { document.images.imgCom.src = 'Images/upIcon.png'; }
         
     });
    </script>
    <script language="javascript"  type="text/javascript">

        function imprSelec(muestra) {
            var ficha = document.getElementById(muestra);
            var ventimp = window.open(' ', 'popimpr');
            var tex = ficha.innerHTML;
            var tex = tex + "";
            var fin = tex.replace("overflow:auto;", " ");
            fin = fin.replace("border:1px solid #999999", " ");
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
        + "</head><body> "
        + "<p align='right'><input id='btnPP' type='button' value='Imprimir' onclick='javascript:window.print()' /></p>"
        + fin;
            fin = fin + "</body></html>"
            ventimp.document.write(fin);
        }
 </script>
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
    <table >
    <tr>
    <td style="width: 392px"  ><asp:Label ID="Label1" runat="server" Font-Names="Trebuchet MS" Font-Size="X-Large" Font-Bold="true" Text="Índice de contraindicaciones" /></td>
    <td align="right"> <input id="btnP" type="button" value="Imprimir" onclick="javascript:imprSelec('print')" />
         <input id="btnM" type="button" value="Visualizar" onclick="javascript:viewMax('print')" /></td>
    </tr>
    <tr>
    <td><asp:Label ID="Label4" runat="server" Font-Names="Trebuchet MS" Font-Size="Medium" Text="Producto: " />
    <asp:Label ID="lblBrand" runat="server" Font-Names="Trebuchet MS" Font-Size="Medium" Font-Bold="true" /><br />
    <asp:Label ID="Label5" runat="server" Font-Names="Trebuchet MS" Font-Size="Medium" Text="Forma farmacéutica: " />
    <asp:Label ID="lblPharmaForm" runat="server" Font-Names="Trebuchet MS" Font-Size="Medium" Font-Bold="true" /></td>
    
    <td  align="right" rowspan="3" style="width: 734px"  >
                   <div id="print">
                <div runat="server" id="div" 
            style="width: 100%; height: 212px; overflow:auto; border:1px solid #999999"></div>
            </div>
        
             <asp:Button ID="btnNoCM" runat="server" Text="No reporta contraindicaciones"
                           onclick="btnNoCM_Click" />
             
            </td>
    </tr>
    <tr>

    <td >
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
    
   
 
 <table width="50%" style="margin-left:100px;"  >
 
 
  
 <tr>
 <td>
 <div id="flip2" class="titulodiv" onclick="$('#panelMed').slideToggle('slow',function() {if ($(this).is(':hidden')) {document.images.imgMed.src='Images/downIcon.png';} else {document.images.imgMed.src='Images/upIcon.png';} });">
 <table style="width:100%; margin-left:auto; margin-right:auto"><tr><td>Contraindicaciones  M&eacute;dicas</td><td align="right"><img alt="Desplegar" id="imgMed" src="Images/downIcon.png"  width="25px" height="25px" /></td></tr></table></div><div id="panelMed" class="contenidodiv">
 <table cellspacing="25px">
 <tr>
 <td valign="bottom"><asp:Panel ID="pnlSearchMed" runat="server" DefaultButton="btnMedical">
                    <asp:Label ID="Label8" runat="server" Font-Names="Trebuchet MS" Font-Size="Large" Font-Bold="true" Text="Catálogo de Contraindicaciones Médicas" />
                    <asp:Table ID="Table3" runat="server" Width="250px">
                        <asp:TableRow>
                            <asp:TableCell HorizontalAlign="Left">
                                <asp:Label ID="Label9" runat="server" Text="Buscar Contraindicación" Font-Names="Trebuchet MS" Font-Size="Smaller" Font-Bold="true" />
                            </asp:TableCell><asp:TableCell HorizontalAlign="Right">
                                <asp:Label ID="Label10" runat="server" Text="Registros por página" Font-Names="Trebuchet MS" Font-Size="Smaller" Font-Bold="true" />
                            </asp:TableCell></asp:TableRow><asp:TableRow>
                            <asp:TableCell HorizontalAlign="Left">
                                <asp:TextBox ID="txtContraMed" runat="server" Text="" Enabled="true" />
                                <asp:Button ID="btnMedical" runat="server" ToolTip="Consultar" Text="Consultar"  OnClick="btnMedical_Click"/>
                            </asp:TableCell><asp:TableCell HorizontalAlign="Right">
                                <asp:DropDownList ID="ddlPageSizeMed" runat="server" AutoPostBack="True" Font-Names="Arial" Font-Size="10pt" OnSelectedIndexChanged="ddlMedicalPageSize_SelectedIndexChanged">
                                    <asp:ListItem Value="-1" >Ilimitado</asp:ListItem>
                                    <asp:ListItem Value="5">5</asp:ListItem>
                                    <asp:ListItem Value="15" Selected="True">15</asp:ListItem>
                                    <asp:ListItem Value="25">25</asp:ListItem>
                                    <asp:ListItem Value="50">50</asp:ListItem>
                                    <asp:ListItem Value="100">100</asp:ListItem>
                                </asp:DropDownList>
                            </asp:TableCell></asp:TableRow></asp:Table></asp:Panel></td><td valign="bottom">
                            
                             <asp:Label ID="lblAdvertMed" runat="server" Font-Names="Trebuchet MS" 
                Font-Size="Medium" Font-Bold="True" Font-Italic="True" Font-Strikeout="False" 
                Font-Underline="False" Visible="False"></asp:Label><asp:Label ID="lblItemMed" runat="server" Font-Names="Trebuchet MS" 
                Font-Size="Medium" Font-Bold="True" Font-Italic="True" Font-Strikeout="False" 
                Font-Underline="False" Visible="False" ForeColor="Red"></asp:Label><asp:CheckBoxList ID="BtnListSubstancesMed" runat="server">
                </asp:CheckBoxList>
            <asp:Button ID="btnContinuarMed" runat="server" Text="Continuar" 
                Visible="False" onclick="btnContinuarMed_Click" />
            <asp:Button ID="btnCancelMed" runat="server" onclick="btnCancelMed_Click" 
                Text="Cancelar" Visible="False" /><br />
                            <table style="width:100%">
                                  <tr>
                                    <td style="text-align:left">
                            <asp:Label ID="Label11" runat="server" Font-Names="Trebuchet MS" Font-Size="Large" Font-Bold="true" Text="Contraindicaciones Médicas Asociadas" />
                                    </td>
                                    <td style="text-align:right">
                                    <asp:Button ID="btnDeleteAllMed" runat="server" onclick="btnDeleteAllMed_Click" Text="Eliminar clasificación" Visible="true" OnClientClick="return confirmDeleteAll('Médicas')"/>
                                    </td>
                                </tr>
                            </table>
                            </td>
 </tr>
        <tr>
             <td valign="top">
                <asp:GridView 
                    ID="grdMedical" runat="server" AutoGenerateColumns="False" BackColor="White" 
                    BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                    Font-Names="Trebuchet MS" Font-Size="Smaller" DataKeyNames="MedicalContraindicationId" 
                    HorizontalAlign="Left" 
                     onpageindexchanging="grdMedical_PageIndexChanging" 
                     onrowdatabound="grdMedical_RowDataBound" ShowHeaderWhenEmpty="true"><Columns>
                        <asp:BoundField DataField="MedicalContraindicationId" Visible="False" />
                        <asp:TemplateField HeaderText="Nombre de la Contraindicacíon">
                            <HeaderStyle HorizontalAlign="Left" BackColor="#2694B4" ForeColor="White" />
                            <ItemStyle  Width="250px" />
                           
                            <ItemTemplate>
                                <asp:Label ID="lblMedical" runat="server">
                                    <%# DataBinder.Eval(Container.DataItem, "MedicalContraindicationName")%>
                                </asp:Label></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="Agregar a producto">
                            <HeaderStyle Width="100px" HorizontalAlign="Center" BackColor="#2694B4" ForeColor="White" />
                            <ItemStyle HorizontalAlign="Center" Wrap="True" />
                            <ItemTemplate>
                                <asp:ImageButton ID="btnAddMedical" runat="server" CommandName="Add" ImageUrl="~/Images/agregar.png" AlternateText="Agregar a producto" 
                                    Enabled="true" Width="20px" OnClick="btnAddMedical_OnClick" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </td>
            <td valign="top">
                
                <asp:GridView ID="grdProductMedical" runat="server" 
                    AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" 
                    BorderStyle="None" BorderWidth="1px" CellPadding="3"
                    Font-Names="Trebuchet MS" Font-Size="Smaller" DataKeyNames="MedicalContraindicationId" 
                    HorizontalAlign="Left" 
                     onrowdatabound="grdProductMedical_RowDataBound" ShowHeaderWhenEmpty="true" ><Columns>
                        <asp:BoundField DataField="MedicalContraindicationId" Visible="False" />
                        <asp:TemplateField HeaderText="Sustancia del producto">
                            <HeaderStyle HorizontalAlign="Left" BackColor="#2694B4" ForeColor="White" />
                            <ItemStyle  Width="250px" />
                             <ItemTemplate>
                                <asp:Label ID="lblSubstanceMed" runat="server">
                                    <%# DataBinder.Eval(Container.DataItem, "ActiveSubstance")%>
                                </asp:Label></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="Contraindicación">
                            <HeaderStyle HorizontalAlign="Left" BackColor="#2694B4" ForeColor="White" />
                            <ItemStyle  Width="250px" />
                            <ItemTemplate>
                                <asp:Label ID="lblHypersensibiliy" runat="server">
                                    <%# DataBinder.Eval(Container.DataItem, "MedicalContraindicationName")%>
                                </asp:Label></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="Eliminar de producto">
                            <HeaderStyle Width="100px" HorizontalAlign="Center" BackColor="#2694B4" ForeColor="White" />
                            <ItemStyle HorizontalAlign="Center" Wrap="True" />
                            <ItemTemplate>
                                <asp:ImageButton ID="btnDeleteMedical" runat="server"  ImageUrl="~/Images/delete.gif" AlternateText="Eliminar Contraindicacíon" 
                                Enabled="true"  OnClick="btnDeleteMedical_OnClick"/>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
       </table>

 </div></td></tr>
 
 
 <tr>
 <td>
 <div id="flip3" class="titulodiv" onclick="$('#panelfis').slideToggle('slow',function() {if ($(this).is(':hidden')) {document.images.imgFis.src='Images/downIcon.png';} else {document.images.imgFis.src='Images/upIcon.png';} });">
 <table style="width:100%; margin-left:auto; margin-right:auto"><tr><td>Contraindicaciones Fisiológicas</td><td align="right"><img alt="Desplegar" id="imgFis" src="Images/downIcon.png"  width="25px" height="25px" /></td></tr></table></div><div id="panelfis" class="contenidodiv">
  <table cellspacing="25px">
 <tr>
 <td valign="bottom"><asp:Panel ID="pnlSearchfis" runat="server" DefaultButton="btnFisiologic">
                    <asp:Label ID="Label12" runat="server" Font-Names="Trebuchet MS" Font-Size="Large" Font-Bold="true" Text="Catálogo de Contraindicaciones Fisiológicas" />
                    <asp:Table ID="Table4" runat="server" Width="250px">
                        <asp:TableRow>
                            <asp:TableCell HorizontalAlign="Left">
                                <asp:Label ID="Label13" runat="server" Text="Buscar Contraindicación" Font-Names="Trebuchet MS" Font-Size="Smaller" Font-Bold="true" />
                            </asp:TableCell><asp:TableCell HorizontalAlign="Right">
                                <asp:Label ID="Label14" runat="server" Text="Registros por página" Font-Names="Trebuchet MS" Font-Size="Smaller" Font-Bold="true" />
                            </asp:TableCell></asp:TableRow><asp:TableRow>
                            <asp:TableCell HorizontalAlign="Left">
                                <asp:TextBox ID="txtfisiologic" runat="server" Text="" Enabled="true" />
                                <asp:Button ID="btnFisiologic" runat="server" ToolTip="Consultar" Text="Consultar"  OnClick="btnPhys_Click"/>
                            </asp:TableCell><asp:TableCell HorizontalAlign="Right">
                                <asp:DropDownList ID="ddlPageSizeFisiologic" runat="server" AutoPostBack="True" Font-Names="Arial" Font-Size="10pt" OnSelectedIndexChanged="ddlPhysPageSize_SelectedIndexChanged">
                                    <asp:ListItem Value="-1" >Ilimitado</asp:ListItem>
                                    <asp:ListItem Value="5">5</asp:ListItem>
                                    <asp:ListItem Value="15" Selected="True">15</asp:ListItem>
                                    <asp:ListItem Value="25">25</asp:ListItem>
                                    <asp:ListItem Value="50">50</asp:ListItem>
                                    <asp:ListItem Value="100">100</asp:ListItem>
                                </asp:DropDownList>

                            </asp:TableCell></asp:TableRow></asp:Table></asp:Panel></td><td valign="bottom">
                             <asp:Label ID="lblAdvertPhy" runat="server" Font-Names="Trebuchet MS" 
                Font-Size="Medium" Font-Bold="True" Font-Italic="True" Font-Strikeout="False" 
                Font-Underline="False" Visible="False"></asp:Label><asp:Label ID="lblItemPhy" runat="server" Font-Names="Trebuchet MS" 
                Font-Size="Medium" Font-Bold="True" Font-Italic="True" Font-Strikeout="False" 
                Font-Underline="False" Visible="False" ForeColor="Red"></asp:Label><asp:CheckBoxList ID="BtnListSubstancesPhy" runat="server">
                </asp:CheckBoxList>
            <asp:Button ID="btnContinuarPhy" runat="server" Text="Continuar" 
                Visible="False" onclick="btnContinuarPhy_Click" />
            <asp:Button ID="btnCancelPhy" runat="server" onclick="btnCancelPhy_Click" 
                Text="Cancelar" Visible="False" /><br />

                            <table style="width:100%">
                                  <tr>
                                    <td style="text-align:left">
                                    <asp:Label ID="Label15" runat="server" Font-Names="Trebuchet MS" Font-Size="Large" Font-Bold="true" Text="Contraindicaciones Fisiológicas Asociadas" />
                                    </td>
                                    <td style="text-align:right">
                                    <asp:Button ID="btnDeleteAllPhy" runat="server" onclick="btnDeleteAllPhy_Click" Text="Eliminar clasificación" Visible="true" OnClientClick="return confirmDeleteAll('Fisiológicas')"/>
                                    </td>
                                </tr>
                            </table>

                            
                            </td>
 </tr>
        <tr>
             <td valign="top">
                <asp:GridView 
                    ID="grdFisiologic" runat="server" AutoGenerateColumns="False" BackColor="White" 
                    BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                    Font-Names="Trebuchet MS" Font-Size="Smaller" DataKeyNames="PhysContraindicationId" 
                    HorizontalAlign="Left" 
                     onpageindexchanging="grdPhys_PageIndexChanging" 
                     onrowdatabound="grdPhys_RowDataBound" ShowHeaderWhenEmpty="true"><Columns>
                        <asp:BoundField DataField="PhysContraindicationId" Visible="False" />
                        <asp:TemplateField HeaderText="Nombre de la Contraindicación">
                            <HeaderStyle HorizontalAlign="Left" BackColor="#2694B4" ForeColor="White" />
                            <ItemStyle  Width="250px" />
                            <ItemTemplate>
                                <asp:Label ID="lblPhysContraind" runat="server">
                                    <%# DataBinder.Eval(Container.DataItem, "PhysContraindicationName")%>
                                </asp:Label></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="Agregar a producto">
                            <HeaderStyle Width="100px" HorizontalAlign="Center" BackColor="#2694B4" ForeColor="White" />
                            <ItemStyle HorizontalAlign="Center" Wrap="True" />
                            <ItemTemplate>
                                <asp:ImageButton ID="btnAddFisiol" runat="server" CommandName="Add" ImageUrl="~/Images/agregar.png" AlternateText="Agregar a producto" 
                                    Enabled="true" Width="20px" OnClick="btnAddPhys_OnClick" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </td>
            <td valign="top">
                
                <asp:GridView ID="grdProductFisi" runat="server" 
                    AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" 
                    BorderStyle="None" BorderWidth="1px" CellPadding="3"
                    Font-Names="Trebuchet MS" Font-Size="Smaller" DataKeyNames="PhysContraindicationId" 
                    HorizontalAlign="Left" 
                     onrowdatabound="grdProductPhys_RowDataBound" ShowHeaderWhenEmpty="true"><Columns>
                        <asp:BoundField DataField="PhysContraindicationId" Visible="False" />
                         <asp:TemplateField HeaderText="Sustancia del producto">
                            <HeaderStyle HorizontalAlign="Left" BackColor="#2694B4" ForeColor="White" />
                            <ItemStyle  Width="250px" />
                             <ItemTemplate>
                                <asp:Label ID="lblSubstanceFis" runat="server">
                                    <%# DataBinder.Eval(Container.DataItem, "ActiveSubstance")%>
                                </asp:Label></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="Contraindicación">
                            <HeaderStyle HorizontalAlign="Left" BackColor="#2694B4" ForeColor="White" />
                            <ItemStyle  Width="250px" />
                            <ItemTemplate>
                                <asp:Label ID="lblPhysContrain" runat="server">
                                    <%# DataBinder.Eval(Container.DataItem, "PhysContraindicationName")%>
                                </asp:Label></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="Eliminar de producto">
                            <HeaderStyle Width="100px" HorizontalAlign="Center" BackColor="#2694B4" ForeColor="White" />
                            <ItemStyle HorizontalAlign="Center" Wrap="True" />
                            <ItemTemplate>
                                <asp:ImageButton ID="btnDeleteFisiol" runat="server"  ImageUrl="~/Images/delete.gif" AlternateText="Eliminar Contraindicacíon" 
                                Enabled="true"  OnClick="btnDeletePhys_OnClick"/>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
       </table>
 
 </div></td>
 </tr>


 <tr>
 <td>
 <div id="flip4" class="titulodiv" onclick="$('#panelPharma').slideToggle('slow',function() {if ($(this).is(':hidden')) {document.images.imgFar.src='Images/downIcon.png';} else {document.images.imgFar.src='Images/upIcon.png';} });">
 <table style="width:100%; margin-left:auto; margin-right:auto"><tr><td>Contraindicaciones Farmacol&oacute;gicas</td><td align="right"><img alt="Desplegar" id="imgFar" src="Images/downIcon.png"  width="25px" height="25px" /></td></tr></table></div><div id="panelPharma" class="contenidodiv">
 <table cellspacing="25px">
 <tr>
 <td valign="bottom"><asp:Panel ID="pnlPharma" runat="server" DefaultButton="btnPharma">
                    <asp:Label ID="Label16" runat="server" Font-Names="Trebuchet MS" Font-Size="Large" Font-Bold="true" Text="Catálogo de Contraindicaciones Farmacológicas" />
                    <asp:Table ID="Table5" runat="server" Width="250px">
                        <asp:TableRow>
                            <asp:TableCell HorizontalAlign="Left">
                                <asp:Label ID="Label17" runat="server" Text="Buscar Contraindicación" Font-Names="Trebuchet MS" Font-Size="Smaller" Font-Bold="true" />
                            </asp:TableCell><asp:TableCell HorizontalAlign="Right">
                                <asp:Label ID="Label18" runat="server" Text="Registros por página" Font-Names="Trebuchet MS" Font-Size="Smaller" Font-Bold="true" />
                            </asp:TableCell></asp:TableRow><asp:TableRow>
                            <asp:TableCell HorizontalAlign="Left">
                                <asp:TextBox ID="txtPharma" runat="server" Text="" Enabled="true" />
                                <asp:Button ID="btnPharma" runat="server" ToolTip="Consultar" Text="Consultar"  OnClick="btnPharma_Click"/>
                            </asp:TableCell><asp:TableCell HorizontalAlign="Right">
                                <asp:DropDownList ID="ddlPageSizePharma" runat="server" AutoPostBack="True" Font-Names="Arial" Font-Size="10pt" OnSelectedIndexChanged="ddlPharmaPageSize_SelectedIndexChanged">
                                    <asp:ListItem Value="-1" >Ilimitado</asp:ListItem>
                                    <asp:ListItem Value="5">5</asp:ListItem>
                                    <asp:ListItem Value="15" Selected="True">15</asp:ListItem>
                                    <asp:ListItem Value="25">25</asp:ListItem>
                                    <asp:ListItem Value="50">50</asp:ListItem>
                                    <asp:ListItem Value="100">100</asp:ListItem>
                                </asp:DropDownList>
                            </asp:TableCell></asp:TableRow></asp:Table></asp:Panel></td><td valign="bottom">
                            
                             <asp:Label ID="lblAdvertPha" runat="server" Font-Names="Trebuchet MS" 
                Font-Size="Medium" Font-Bold="True" Font-Italic="True" Font-Strikeout="False" 
                Font-Underline="False" Visible="False"></asp:Label><asp:Label ID="lblItemPha" runat="server" Font-Names="Trebuchet MS" 
                Font-Size="Medium" Font-Bold="True" Font-Italic="True" Font-Strikeout="False" 
                Font-Underline="False" Visible="False" ForeColor="Red"></asp:Label><asp:CheckBoxList ID="BtnListSubstancesPha" runat="server">
                </asp:CheckBoxList>
            <asp:Button ID="btnContinuarPha" runat="server" Text="Continuar" 
                Visible="False" onclick="btnContinuarPha_Click" />
            <asp:Button ID="btnCancelPha" runat="server" onclick="btnCancelPha_Click" 
                Text="Cancelar" Visible="False" /><br />

                <table style="width:100%">
                                  <tr>
                                    <td style="text-align:left">
                                    <asp:Label ID="Label19" runat="server" Font-Names="Trebuchet MS" Font-Size="Large" Font-Bold="true" Text="Contraindicaciones Farmacológicas Asociadas" />
                                    </td>
                                    <td style="text-align:right">
                                    <asp:Button ID="btnDeleteAllPha" runat="server" onclick="btnDeleteAllPha_Click" Text="Eliminar clasificación" Visible="true" OnClientClick="return confirmDeleteAll('Farmacológicas')"/>
                                    </td>
                                </tr>
                            </table>


                            
                            
                            
                            </td>
 </tr>
        <tr>
             <td valign="top">
                <asp:GridView 
                    ID="grdPharma" runat="server" AutoGenerateColumns="False" BackColor="White" 
                    BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                    Font-Names="Trebuchet MS" Font-Size="Smaller" DataKeyNames="PharmaContraindicationId" 
                    HorizontalAlign="Left" 
                     onpageindexchanging="grdPharma_PageIndexChanging" 
                     onrowdatabound="grdPharma_RowDataBound" ShowHeaderWhenEmpty="true"><Columns>
                        <asp:BoundField DataField="PharmaContraindicationId" Visible="False" />
                        <asp:TemplateField HeaderText="Nombre de la Contraindicación">
                            <HeaderStyle HorizontalAlign="Left" BackColor="#2694B4" ForeColor="White" />
                            <ItemStyle  Width="250px" />
                            <ItemTemplate>
                                <asp:Label ID="lblPharmaContraind" runat="server">
                                    <%# DataBinder.Eval(Container.DataItem, "PharmaContraindicationName")%>
                                </asp:Label></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="Agregar a producto">
                            <HeaderStyle Width="100px" HorizontalAlign="Center" BackColor="#2694B4" ForeColor="White" />
                            <ItemStyle HorizontalAlign="Center" Wrap="True" />
                            <ItemTemplate>
                                <asp:ImageButton ID="btnAddPharma" runat="server" CommandName="Add" ImageUrl="~/Images/agregar.png" AlternateText="Agregar a producto" 
                                    Enabled="true" Width="20px" OnClick="btnAddPharma_OnClick" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </td>
            <td valign="top">
                
                <asp:GridView ID="grdProductPharma" runat="server" 
                    AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" 
                    BorderStyle="None" BorderWidth="1px" CellPadding="3"
                    Font-Names="Trebuchet MS" Font-Size="Smaller" DataKeyNames="PharmaContraindicationId" 
                    HorizontalAlign="Left" 
                     onrowdatabound="grdProductPharma_RowDataBound" ShowHeaderWhenEmpty="true"><Columns>
                        <asp:BoundField DataField="PharmaContraindicationId" Visible="False" />
                         <asp:TemplateField HeaderText="Sustancia del producto">
                            <HeaderStyle HorizontalAlign="Left" BackColor="#2694B4" ForeColor="White" />
                            <ItemStyle  Width="250px" />
                             <ItemTemplate>
                                <asp:Label ID="lblSubstancePha" runat="server">
                                    <%# DataBinder.Eval(Container.DataItem, "ActiveSubstance")%>
                                </asp:Label></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="Contraindicación">
                            <HeaderStyle HorizontalAlign="Left" BackColor="#2694B4" ForeColor="White" />
                            <ItemStyle  Width="250px" />
                            <ItemTemplate>
                                <asp:Label ID="lblPharmaContrain" runat="server">
                                    <%# DataBinder.Eval(Container.DataItem, "PharmaContraindicationName")%>
                                </asp:Label></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="Eliminar de producto">
                            <HeaderStyle Width="100px" HorizontalAlign="Center" BackColor="#2694B4" ForeColor="White" />
                            <ItemStyle HorizontalAlign="Center" Wrap="True" />
                            <ItemTemplate>
                                <asp:ImageButton ID="btnDeletePharma" runat="server"  ImageUrl="~/Images/delete.gif" AlternateText="Eliminar Contraindicacíon" 
                                Enabled="true"  OnClick="btnDeletePharma_OnClick"/>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
       </table>
 </div></td>
 </tr>

 <tr>
 <td>
 <div id="flip" class="titulodiv"  onclick="$('#panelHyper').slideToggle('slow',function() {if ($(this).is(':hidden')) {document.images.imgHiper.src='Images/downIcon.png';} else {document.images.imgHiper.src='Images/upIcon.png';} });">
 <table style="width:100%; margin-left:auto; margin-right:auto"><tr><td>Contraindicaciones por Hipersensibilidad</td><td align="right"><img alt="Desplegar" id="imgHiper" src="Images/downIcon.png"  width="25px" height="25px" /></td></tr></table></div><div id="panelHyper" class="contenidodiv">
 <table cellspacing="25px">
 <tr>
 <td valign="bottom"><asp:Panel ID="pnlSearch" runat="server" DefaultButton="btnHypersensibility">
                    <asp:Label ID="Label7" runat="server" Font-Names="Trebuchet MS" Font-Size="Large" Font-Bold="true" Text="Catálogo de Contraindicaciones por Hipersensibilidades" />
                    <asp:Table ID="Table2" runat="server" Width="250px">
                        <asp:TableRow>
                            <asp:TableCell HorizontalAlign="Left">
                                <asp:Label ID="Label2" runat="server" Text="Buscar Contraindicacíon" Font-Names="Trebuchet MS" Font-Size="Smaller" Font-Bold="true" />
                            </asp:TableCell><asp:TableCell HorizontalAlign="Right">
                                <asp:Label ID="Label3" runat="server" Text="Registros por página" Font-Names="Trebuchet MS" Font-Size="Smaller" Font-Bold="true" />
                            </asp:TableCell></asp:TableRow><asp:TableRow>
                            <asp:TableCell HorizontalAlign="Left">
                                <asp:TextBox ID="txtHypersensibilityName" runat="server" Text="" Enabled="true" />
                                <asp:Button ID="btnHypersensibility" runat="server" ToolTip="Consultar" Text="Consultar"  OnClick="btnHypersensibility_Click"/>
                            </asp:TableCell><asp:TableCell HorizontalAlign="Right">
                                <asp:DropDownList ID="ddlHypersensibilityPageSize" runat="server" AutoPostBack="True" Font-Names="Arial" Font-Size="10pt" OnSelectedIndexChanged="ddlHypersensibilityPageSize_SelectedIndexChanged">
                                    <asp:ListItem Value="-1" >Ilimitado</asp:ListItem>
                                    <asp:ListItem Value="5">5</asp:ListItem>
                                    <asp:ListItem Value="15" Selected="True">15</asp:ListItem>
                                    <asp:ListItem Value="25">25</asp:ListItem>
                                    <asp:ListItem Value="50">50</asp:ListItem>
                                    <asp:ListItem Value="100">100</asp:ListItem>
                                </asp:DropDownList>
                            </asp:TableCell></asp:TableRow></asp:Table></asp:Panel></td><td valign="bottom">

                            <asp:Label ID="lblAdvertHip" runat="server" Font-Names="Trebuchet MS" 
                Font-Size="Medium" Font-Bold="True" Font-Italic="True" Font-Strikeout="False" 
                Font-Underline="False" Visible="False"></asp:Label><asp:Label ID="lblItemHip" runat="server" Font-Names="Trebuchet MS" 
                Font-Size="Medium" Font-Bold="True" Font-Italic="True" Font-Strikeout="False" 
                Font-Underline="False" Visible="False" ForeColor="Red"></asp:Label><asp:CheckBoxList ID="BtnListSubstancesHip" runat="server">
                </asp:CheckBoxList>
            <asp:Button ID="btnContinuarHip" runat="server" Text="Continuar" 
                Visible="False" onclick="btnContinuarHip_Click" />
            <asp:Button ID="btnCancelHip" runat="server" onclick="btnCancelHip_Click" 
                Text="Cancelar" Visible="False" /><br />

                                <table style="width:100%">
                                  <tr>
                                    <td style="text-align:left">
                                    <asp:Label ID="Label6" runat="server" Font-Names="Trebuchet MS" Font-Size="Large" Font-Bold="true" Text="Contraindicaciones por Hipersensibilidad Asociadas" />
                                    </td>
                                    <td style="text-align:right">
                                    <asp:Button ID="btnDeleteAllHyp" runat="server" onclick="btnDeleteAllHyp_Click" Text="Eliminar clasificación" Visible="true" OnClientClick="return confirmDeleteAll('por Hipersensibilidad')"/>
                                    </td>
                                </tr>
                            </table>


                            
                            </td>
 </tr>
        <tr>
             <td valign="top">
                <asp:GridView 
                    ID="grdHypersensibilities" runat="server" AutoGenerateColumns="False" BackColor="White" 
                    BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                    Font-Names="Trebuchet MS" Font-Size="Smaller" DataKeyNames="HypersensibilityId" 
                    HorizontalAlign="Left" 
                     onpageindexchanging="grdHypersensibilities_PageIndexChanging" 
                     onrowdatabound="grdHypersensibilities_RowDataBound" ShowHeaderWhenEmpty="true"><Columns>
                        <asp:BoundField DataField="HypersensibilityId" Visible="False" />
                        <asp:TemplateField HeaderText="Nombre de la Contraindicacíon">
                            <HeaderStyle HorizontalAlign="Left" BackColor="#2694B4" ForeColor="White" />
                            <ItemStyle  Width="250px" />
                            <ItemTemplate>
                                <asp:Label ID="lblHypersensibility" runat="server">
                                    <%# DataBinder.Eval(Container.DataItem, "HypersensibilityName")%>
                                </asp:Label></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="Agregar a producto">
                            <HeaderStyle Width="100px" HorizontalAlign="Center" BackColor="#2694B4" ForeColor="White" />
                            <ItemStyle HorizontalAlign="Center" Wrap="True" />
                            <ItemTemplate>
                                <asp:ImageButton ID="btnAddHypersensibility" runat="server" CommandName="Add" ImageUrl="~/Images/agregar.png" AlternateText="Agregar a producto" 
                                    Enabled="true" Width="20px" OnClick="btnAddHyper_OnClick" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </td>
            <td valign="top">
                
                <asp:GridView ID="grdProductHypersensibilities" runat="server" 
                    AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" 
                    BorderStyle="None" BorderWidth="1px" CellPadding="3"
                    Font-Names="Trebuchet MS" Font-Size="Smaller" DataKeyNames="HypersensibilityId" 
                    HorizontalAlign="Left" 
                     onrowdatabound="grdProductHypersensibilities_RowDataBound" ShowHeaderWhenEmpty="true"><Columns>
                        <asp:BoundField DataField="HypersensibilityId" Visible="False" />
                         <asp:TemplateField HeaderText="Sustancia del producto">
                            <HeaderStyle HorizontalAlign="Left" BackColor="#2694B4" ForeColor="White" />
                            <ItemStyle  Width="250px" />
                             <ItemTemplate>
                                <asp:Label ID="lblSubstanceHip" runat="server">
                                    <%# DataBinder.Eval(Container.DataItem, "ActiveSubstance")%>
                                </asp:Label></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="Contraindicacíon">
                            <HeaderStyle HorizontalAlign="Left" BackColor="#2694B4" ForeColor="White" />
                            <ItemStyle  Width="250px" />
                            <ItemTemplate>
                                <asp:Label ID="lblHypersensibiliy" runat="server">
                                    <%# DataBinder.Eval(Container.DataItem, "HypersensibilityName")%>
                                </asp:Label></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="Eliminar de producto">
                            <HeaderStyle Width="100px" HorizontalAlign="Center" BackColor="#2694B4" ForeColor="White" />
                            <ItemStyle HorizontalAlign="Center" Wrap="True" />
                            <ItemTemplate>
                                <asp:ImageButton ID="btnDeleteHypersensibility" runat="server"  ImageUrl="~/Images/delete.gif" AlternateText="Eliminar Contraindicacíon" 
                                Enabled="true"  OnClick="btnDeleteHyper_OnClick"/>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
       </table>
 </div>
 </td>
 </tr>

 <tr>
 <td>
 <div id="flip5" class="titulodiv" onclick="$('#panelGroups').slideToggle('slow',function() {if ($(this).is(':hidden')) {document.images.imgGru.src='Images/downIcon.png';} else {document.images.imgGru.src='Images/upIcon.png';} });">
 <table style="width:100%; margin-left:auto; margin-right:auto"><tr><td>Contraindicaciones por Grupos Farmacológicos</td><td align="right"><img alt="Desplegar" id="imgGru" src="Images/downIcon.png"  width="25px" height="25px" /></td></tr></table></div><div id="panelGroups" class="contenidodiv">
 <table cellspacing="25px">
 <tr>
 <td valign="bottom"><asp:Panel ID="pnlGroups" runat="server" DefaultButton="btnGroups">
                    <asp:Label ID="Label20" runat="server" Font-Names="Trebuchet MS" Font-Size="Large" Font-Bold="true" Text="Catálogo de Contraindicaciones por Grupos Farmacológicos" />
                    <asp:Table ID="Table6" runat="server" Width="250px">
                        <asp:TableRow>
                            <asp:TableCell HorizontalAlign="Left">
                                <asp:Label ID="Label21" runat="server" Text="Buscar Contraindicacíon" Font-Names="Trebuchet MS" Font-Size="Smaller" Font-Bold="true" />
                            </asp:TableCell><asp:TableCell HorizontalAlign="Right">
                                <asp:Label ID="Label22" runat="server" Text="Registros por página" Font-Names="Trebuchet MS" Font-Size="Smaller" Font-Bold="true" />
                            </asp:TableCell></asp:TableRow><asp:TableRow>
                            <asp:TableCell HorizontalAlign="Left">
                                <asp:TextBox ID="txtGruops" runat="server" Text="" Enabled="true" />
                                <asp:Button ID="btnGroups" runat="server" ToolTip="Consultar" Text="Consultar"  OnClick="btnPharmaGroup_Click"/>
                            </asp:TableCell><asp:TableCell HorizontalAlign="Right">
                                <asp:DropDownList ID="ddlPageSizeGroups" runat="server" AutoPostBack="True" Font-Names="Arial" Font-Size="10pt" OnSelectedIndexChanged="ddlPharmaGroupPageSize_SelectedIndexChanged">
                                    <asp:ListItem Value="-1" >Ilimitado</asp:ListItem>
                                    <asp:ListItem Value="5">5</asp:ListItem>
                                    <asp:ListItem Value="15" Selected="True">15</asp:ListItem>
                                    <asp:ListItem Value="25">25</asp:ListItem>
                                    <asp:ListItem Value="50">50</asp:ListItem>
                                    <asp:ListItem Value="100">100</asp:ListItem>
                                </asp:DropDownList>
                            </asp:TableCell></asp:TableRow></asp:Table></asp:Panel></td><td valign="bottom">
                            
                             <asp:Label ID="lblAdvertGro" runat="server" Font-Names="Trebuchet MS" 
                Font-Size="Medium" Font-Bold="True" Font-Italic="True" Font-Strikeout="False" 
                Font-Underline="False" Visible="False"></asp:Label><asp:Label ID="lblItemGro" runat="server" Font-Names="Trebuchet MS" 
                Font-Size="Medium" Font-Bold="True" Font-Italic="True" Font-Strikeout="False" 
                Font-Underline="False" Visible="False" ForeColor="Red"></asp:Label><asp:CheckBoxList ID="BtnListSubstancesGro" runat="server">
                </asp:CheckBoxList>
            <asp:Button ID="btnContinuarGro" runat="server" Text="Continuar" 
                Visible="False" onclick="btnContinuarGro_Click" />
            <asp:Button ID="btnCancelGro" runat="server" onclick="btnCancelGro_Click" 
                Text="Cancelar" Visible="False" /><br />
                            
                                  <table style="width:100%">
                                  <tr>
                                    <td style="text-align:left">
                                    <asp:Label ID="Label23" runat="server" Font-Names="Trebuchet MS" Font-Size="Large" Font-Bold="true" Text="Contraindicaciones Asociadas por Grupos Farmacológicos" />
                                    </td>
                                    <td style="text-align:right">
                                    <asp:Button ID="btnDeleteAllGro" runat="server" onclick="btnDeleteAllGro_Click" Text="Eliminar clasificación" Visible="true" OnClientClick="return confirmDeleteAll('por Grupos Farmacológicos')"/>
                                    </td>
                                </tr>
                            </table>
                            
                            </td>
 </tr>
        <tr>
             <td valign="top">
                <asp:GridView 
                    ID="grdGroups" runat="server" AutoGenerateColumns="False" BackColor="White" 
                    BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                    Font-Names="Trebuchet MS" Font-Size="Smaller" DataKeyNames="PharmaGroupId" 
                    HorizontalAlign="Left" 
                     onpageindexchanging="grdPharmaGroups_PageIndexChanging" 
                     onrowdatabound="grdPharmaGroups_RowDataBound" ShowHeaderWhenEmpty="true"><Columns>
                        <asp:BoundField DataField="PharmaGroupId" Visible="False" />
                        <asp:TemplateField HeaderText="Nombre de la Contraindicación">
                            <HeaderStyle HorizontalAlign="Left" BackColor="#2694B4" ForeColor="White" />
                            <ItemStyle  Width="250px" />
                            <ItemTemplate>
                                <asp:Label ID="lblPharmaGroup" runat="server">
                                    <%# DataBinder.Eval(Container.DataItem, "GroupName")%>
                                </asp:Label></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="Agregar a producto">
                            <HeaderStyle Width="100px" HorizontalAlign="Center" BackColor="#2694B4" ForeColor="White" />
                            <ItemStyle HorizontalAlign="Center" Wrap="True" />
                            <ItemTemplate>
                                <asp:ImageButton ID="btnAddGroup" runat="server" CommandName="Add" ImageUrl="~/Images/agregar.png" AlternateText="Agregar a producto" 
                                    Enabled="true" Width="20px" OnClick="btnAddPharmaGroup_OnClick" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </td>
            <td valign="top">
                
                <asp:GridView ID="grdProductGroups" runat="server" 
                    AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" 
                    BorderStyle="None" BorderWidth="1px" CellPadding="3"
                    Font-Names="Trebuchet MS" Font-Size="Smaller" DataKeyNames="PharmaGroupId" 
                    HorizontalAlign="Left" 
                     onrowdatabound="grdProductPharmaGroup_RowDataBound" ShowHeaderWhenEmpty="true"><Columns>
                        <asp:BoundField DataField="PharmaGroupId" Visible="False" />
                        <asp:TemplateField HeaderText="Sustancia del producto">
                            <HeaderStyle HorizontalAlign="Left" BackColor="#2694B4" ForeColor="White" />
                            <ItemStyle  Width="250px" />
                             <ItemTemplate>
                                <asp:Label ID="lblSubstanceGro" runat="server">
                                    <%# DataBinder.Eval(Container.DataItem, "ActiveSubstance")%>
                                </asp:Label></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="Contraindicación">
                            <HeaderStyle HorizontalAlign="Left" BackColor="#2694B4" ForeColor="White" />
                            <ItemStyle  Width="250px" />
                            <ItemTemplate>
                                <asp:Label ID="lblPharmaGroup" runat="server">
                                    <%# DataBinder.Eval(Container.DataItem, "GroupName")%>
                                </asp:Label></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="Eliminar de producto">
                            <HeaderStyle Width="100px" HorizontalAlign="Center" BackColor="#2694B4" ForeColor="White" />
                            <ItemStyle HorizontalAlign="Center" Wrap="True" />
                            <ItemTemplate>
                                <asp:ImageButton ID="btnDeleteGroup" runat="server"  ImageUrl="~/Images/delete.gif" AlternateText="Eliminar Contraindicacíon" 
                                Enabled="true"  OnClick="btnDeletePharmaGroup_OnClick"/>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
       </table>
 
 </div></td></tr>
 
 <tr>
 <td>
 <div id="flip6" class="titulodiv" onclick="$('#panelSubs').slideToggle('slow',function() {if ($(this).is(':hidden')) {document.images.imgSus.src='Images/downIcon.png';} else {document.images.imgSus.src='Images/upIcon.png';} });">
 <table style="width:100%; margin-left:auto; margin-right:auto"><tr><td>Contraindicaciones por Sustancias Activas</td><td align="right"><img alt="Desplegar" id="imgSus" src="Images/downIcon.png"  width="25px" height="25px" /></td></tr></table></div><div id="panelSubs" class="contenidodiv">
 <table cellspacing="25px">
 <tr>
 <td valign="bottom"><asp:Panel ID="pnlSubs" runat="server" DefaultButton="btnSubs">
                    <asp:Label ID="Label24" runat="server" Font-Names="Trebuchet MS" Font-Size="Large" Font-Bold="true" Text="Catálogo de Contraindicaciones por Sustancia Activa" />
                    <asp:Table ID="Table7" runat="server" Width="250px">
                        <asp:TableRow>
                            <asp:TableCell HorizontalAlign="Left">
                                <asp:Label ID="Label25" runat="server" Text="Buscar Contraindicacíon" Font-Names="Trebuchet MS" Font-Size="Smaller" Font-Bold="true" />
                            </asp:TableCell><asp:TableCell HorizontalAlign="Right">
                                <asp:Label ID="Label26" runat="server" Text="Registros por página" Font-Names="Trebuchet MS" Font-Size="Smaller" Font-Bold="true" />
                            </asp:TableCell></asp:TableRow><asp:TableRow>
                            <asp:TableCell HorizontalAlign="Left">
                                <asp:TextBox ID="txtSubs" runat="server" Text="" Enabled="true" />
                                <asp:Button ID="btnSubs" runat="server" ToolTip="Consultar" Text="Consultar"  OnClick="btnSubstances_Click"/>
                            </asp:TableCell><asp:TableCell HorizontalAlign="Right">
                                <asp:DropDownList ID="ddlPageSizeSubs" runat="server" AutoPostBack="True" Font-Names="Arial" Font-Size="10pt" OnSelectedIndexChanged="ddlSubstancesPageSize_SelectedIndexChanged">
                                    <asp:ListItem Value="-1" >Ilimitado</asp:ListItem>
                                    <asp:ListItem Value="5">5</asp:ListItem>
                                    <asp:ListItem Value="15" Selected="True">15</asp:ListItem>
                                    <asp:ListItem Value="25">25</asp:ListItem>
                                    <asp:ListItem Value="50">50</asp:ListItem>
                                    <asp:ListItem Value="100">100</asp:ListItem>
                                </asp:DropDownList>
                            </asp:TableCell></asp:TableRow></asp:Table></asp:Panel></td><td valign="bottom">

                              <asp:Label ID="lblAdvertAcs" runat="server" Font-Names="Trebuchet MS" 
                Font-Size="Medium" Font-Bold="True" Font-Italic="True" Font-Strikeout="False" 
                Font-Underline="False" Visible="False"></asp:Label><asp:Label ID="lblItemAcs" runat="server" Font-Names="Trebuchet MS" 
                Font-Size="Medium" Font-Bold="True" Font-Italic="True" Font-Strikeout="False" 
                Font-Underline="False" Visible="False" ForeColor="Red"></asp:Label><asp:CheckBoxList ID="BtnListSubstancesAcs" runat="server">
                </asp:CheckBoxList>
            <asp:Button ID="btnContinuarAcs" runat="server" Text="Continuar" 
                Visible="False" onclick="btnContinuarAcs_Click" />
            <asp:Button ID="btnCancelAcs" runat="server" onclick="btnCancelAcs_Click" 
                Text="Cancelar" Visible="False" /><br />
                            
                            <table style="width:100%">
                                  <tr>
                                    <td style="text-align:left">
                                    <asp:Label ID="Label27" runat="server" Font-Names="Trebuchet MS" Font-Size="Large" Font-Bold="true" Text="Contraindicaciones Asociadas por Sustancias Activas" />
                                    </td>
                                    <td style="text-align:right">
                                    <asp:Button ID="btnDeleteAllAcs" runat="server" onclick="btnDeleteAllAcs_Click" Text="Eliminar clasificación" Visible="true" OnClientClick="return confirmDeleteAll('por Sustancias Activas')"/>
                                    </td>
                                </tr>
                            </table>

                            
                            </td>
 </tr>
        <tr>
             <td valign="top">
                <asp:GridView 
                    ID="grdSubs" runat="server" AutoGenerateColumns="False" BackColor="White" 
                    BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                    Font-Names="Trebuchet MS" Font-Size="Smaller" DataKeyNames="ActiveSubstanceId" 
                    HorizontalAlign="Left" 
                     onpageindexchanging="grdSubstances_PageIndexChanging" 
                     onrowdatabound="grdSubstances_RowDataBound" ShowHeaderWhenEmpty="true"><Columns>
                        <asp:BoundField DataField="ActiveSubstanceId" Visible="False" />
                        <asp:TemplateField HeaderText="Nombre de la Contraindicación">
                            <HeaderStyle HorizontalAlign="Left" BackColor="#2694B4" ForeColor="White" />
                            <ItemStyle  Width="250px" />
                            <ItemTemplate>
                                <asp:Label ID="lblSubstance" runat="server">
                                    <%# DataBinder.Eval(Container.DataItem, "Description")%>
                                </asp:Label></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="Agregar a producto">
                            <HeaderStyle Width="100px" HorizontalAlign="Center" BackColor="#2694B4" ForeColor="White" />
                            <ItemStyle HorizontalAlign="Center" Wrap="True" />
                            <ItemTemplate>
                                <asp:ImageButton ID="btnAddSubs" runat="server" CommandName="Add" ImageUrl="~/Images/agregar.png" AlternateText="Agregar a producto" 
                                    Enabled="true" Width="20px" OnClick="btnAddSubstances_OnClick" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </td>
            <td valign="top">
                
                <asp:GridView ID="grdProductSubst" runat="server" 
                    AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" 
                    BorderStyle="None" BorderWidth="1px" CellPadding="3"
                    Font-Names="Trebuchet MS" Font-Size="Smaller" DataKeyNames="ActiveSubstanceId" 
                    HorizontalAlign="Left" 
                     onrowdatabound="grdProductSubstance_RowDataBound" ShowHeaderWhenEmpty="true"><Columns>
                        <asp:BoundField DataField="ActiveSubstanceId" Visible="False" />
                         <asp:TemplateField HeaderText="Sustancia del producto">
                            <HeaderStyle HorizontalAlign="Left" BackColor="#2694B4" ForeColor="White" />
                            <ItemStyle  Width="250px" />
                            <ItemTemplate>
                                <asp:Label ID="lblsubstan" runat="server">
                                    <%# DataBinder.Eval(Container.DataItem, "ActiveSubstance")%>
                                </asp:Label></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="Contraindicacíon">
                            <HeaderStyle HorizontalAlign="Left" BackColor="#2694B4" ForeColor="White" />
                            <ItemStyle  Width="250px" />
                            <ItemTemplate>
                                <asp:Label ID="lblsubstanContra" runat="server">
                                    <%# DataBinder.Eval(Container.DataItem, "SubstanceContraindication")%>
                                </asp:Label></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="Eliminar de producto">
                            <HeaderStyle Width="100px" HorizontalAlign="Center" BackColor="#2694B4" ForeColor="White" />
                            <ItemStyle HorizontalAlign="Center" Wrap="True" />
                            <ItemTemplate>
                                <asp:ImageButton ID="btnDelSubs" runat="server"  ImageUrl="~/Images/delete.gif" AlternateText="Eliminar Contraindicacíon" 
                                Enabled="true"  OnClick="btnDeleteSubstances_OnClick"/>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
       </table>
 </div></td></tr>
 
 <tr>
 <td>
 <div id="flip7" class="titulodiv" onclick="$('#panelOther').slideToggle('slow',function() {if ($(this).is(':hidden')) {document.images.imgOtr.src='Images/downIcon.png';} else {document.images.imgOtr.src='Images/upIcon.png';} });">
 <table style="width:100%; margin-left:auto; margin-right:auto"><tr><td>Contraindicaciones por Otros Elementos</td><td align="right"><img alt="Desplegar" id="imgOtr" src="Images/downIcon.png"  width="25px" height="25px" /></td></tr></table></div><div id="panelOther" class="contenidodiv">

 <table cellspacing="25px">
 <tr>
 <td valign="bottom"><asp:Panel ID="pnlOther" runat="server" DefaultButton="btnOther">
                    <asp:Label ID="Label28" runat="server" Font-Names="Trebuchet MS" Font-Size="Large" Font-Bold="true" Text="Catálogo de Otros Elementos" />
                    <asp:Table ID="Table8" runat="server" Width="250px">
                        <asp:TableRow>
                            <asp:TableCell HorizontalAlign="Left">
                                <asp:Label ID="Label29" runat="server" Text="Buscar Contraindicación" Font-Names="Trebuchet MS" Font-Size="Smaller" Font-Bold="true" />
                            </asp:TableCell><asp:TableCell HorizontalAlign="Right">
                                <asp:Label ID="Label30" runat="server" Text="Registros por página" Font-Names="Trebuchet MS" Font-Size="Smaller" Font-Bold="true" />
                            </asp:TableCell></asp:TableRow><asp:TableRow>
                            <asp:TableCell HorizontalAlign="Left">
                                <asp:TextBox ID="txtOther" runat="server" Text="" Enabled="true" />
                                <asp:Button ID="btnOther" runat="server" ToolTip="Consultar" Text="Consultar"  OnClick="btnOther_Click"/>
                            </asp:TableCell><asp:TableCell HorizontalAlign="Right">
                                <asp:DropDownList ID="ddlPageSizeOther" runat="server" AutoPostBack="True" Font-Names="Arial" Font-Size="10pt" OnSelectedIndexChanged="ddlOtherPageSize_SelectedIndexChanged">
                                    <asp:ListItem Value="-1" >Ilimitado</asp:ListItem>
                                    <asp:ListItem Value="5">5</asp:ListItem>
                                    <asp:ListItem Value="15" Selected="True">15</asp:ListItem>
                                    <asp:ListItem Value="25">25</asp:ListItem>
                                    <asp:ListItem Value="50">50</asp:ListItem>
                                    <asp:ListItem Value="100">100</asp:ListItem>
                                </asp:DropDownList>
                            </asp:TableCell></asp:TableRow></asp:Table></asp:Panel></td><td valign="bottom">
                            
                             <asp:Label ID="lblAdvertOth" runat="server" Font-Names="Trebuchet MS" 
                Font-Size="Medium" Font-Bold="True" Font-Italic="True" Font-Strikeout="False" 
                Font-Underline="False" Visible="False"></asp:Label><asp:Label ID="lblItemOth" runat="server" Font-Names="Trebuchet MS" 
                Font-Size="Medium" Font-Bold="True" Font-Italic="True" Font-Strikeout="False" 
                Font-Underline="False" Visible="False" ForeColor="Red"></asp:Label><asp:CheckBoxList ID="BtnListSubstancesOth" runat="server">
                </asp:CheckBoxList>
            <asp:Button ID="btnContinuarOth" runat="server" Text="Continuar" 
                Visible="False" onclick="btnContinuarOth_Click" />
            <asp:Button ID="btnCancelOth" runat="server" onclick="btnCancelOth_Click" 
                Text="Cancelar" Visible="False" /><br />
                            
                             <table style="width:100%">
                                  <tr>
                                    <td style="text-align:left">
                                    <asp:Label ID="Label31" runat="server" Font-Names="Trebuchet MS" Font-Size="Large" Font-Bold="true" Text="Contraindicaciones Asociadas por Otros Elementos" />
                                    </td>
                                    <td style="text-align:right">
                                    <asp:Button ID="btnDeleteAllOth" runat="server" onclick="btnDeleteAllOth_Click" Text="Eliminar clasificación" Visible="true" OnClientClick="return confirmDeleteAll('por Otros Elementos')"/>
                                    </td>
                                </tr>
                            </table>

                            
                            </td>
 </tr>
        <tr>
             <td valign="top">
                <asp:GridView 
                    ID="grdOther" runat="server" AutoGenerateColumns="False" BackColor="White" 
                    BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                    Font-Names="Trebuchet MS" Font-Size="Smaller" DataKeyNames="ElementId" 
                    HorizontalAlign="Left" 
                     onpageindexchanging="grdOther_PageIndexChanging" 
                     onrowdatabound="grdOther_RowDataBound" ShowHeaderWhenEmpty="true"><Columns>
                        <asp:BoundField DataField="ElementId" Visible="False" />
                        <asp:TemplateField HeaderText="Nombre de la Contraindicación">
                            <HeaderStyle HorizontalAlign="Left" BackColor="#2694B4" ForeColor="White" />
                            <ItemStyle  Width="250px" />
                            <ItemTemplate>
                                <asp:Label ID="lblOther" runat="server">
                                    <%# DataBinder.Eval(Container.DataItem, "ElementName")%>
                                </asp:Label></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="Agregar a producto">
                            <HeaderStyle Width="100px" HorizontalAlign="Center" BackColor="#2694B4" ForeColor="White" />
                            <ItemStyle HorizontalAlign="Center" Wrap="True" />
                            <ItemTemplate>
                                <asp:ImageButton ID="btnAddOther" runat="server" CommandName="Add" ImageUrl="~/Images/agregar.png" AlternateText="Agregar a producto" 
                                    Enabled="true" Width="20px" OnClick="btnAddOther_OnClick" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </td>
            <td valign="top">
                
                <asp:GridView ID="grdProductOther" runat="server" 
                    AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" 
                    BorderStyle="None" BorderWidth="1px" CellPadding="3"
                    Font-Names="Trebuchet MS" Font-Size="Smaller" DataKeyNames="ElementId" 
                    HorizontalAlign="Left" 
                     onrowdatabound="grdProductOther_RowDataBound" ShowHeaderWhenEmpty="true"><Columns>
                        <asp:BoundField DataField="ElementId" Visible="False" />
                         <asp:TemplateField HeaderText="Sustancia del producto">
                            <HeaderStyle HorizontalAlign="Left" BackColor="#2694B4" ForeColor="White" />
                            <ItemStyle  Width="250px" />
                            <ItemTemplate>
                                <asp:Label ID="lblsubstanOth" runat="server">
                                    <%# DataBinder.Eval(Container.DataItem, "ActiveSubstance")%>
                                </asp:Label></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="Contraindicación">
                            <HeaderStyle HorizontalAlign="Left" BackColor="#2694B4" ForeColor="White" />
                            <ItemStyle  Width="250px" />
                            <ItemTemplate>
                                <asp:Label ID="lblOther" runat="server">
                                    <%# DataBinder.Eval(Container.DataItem, "ElementName")%>
                                </asp:Label></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="Eliminar de producto">
                            <HeaderStyle Width="100px" HorizontalAlign="Center" BackColor="#2694B4" ForeColor="White" />
                            <ItemStyle HorizontalAlign="Center" Wrap="True" />
                            <ItemTemplate>
                                <asp:ImageButton ID="btnDelOther" runat="server"  ImageUrl="~/Images/delete.gif" AlternateText="Eliminar Contraindicacíon" 
                                Enabled="true"  OnClick="btnDeleteOther_OnClick"/>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
       </table>

 </div></td></tr>
 <tr>
 <td>
 <div id="flip8" class="titulodiv" onclick="$('#panelCom').slideToggle('slow',function() {if ($(this).is(':hidden')) {document.images.imgCom.src='Images/downIcon.png';} else {document.images.imgCom.src='Images/upIcon.png';} });">
 <table style="width:100%; margin-left:auto; margin-right:auto"><tr><td>Contraindicaciones por Comentarios</td><td align="right"><img alt="Desplegar" id="imgCom" src="Images/downIcon.png"  width="25px" height="25px" /></td></tr></table></div><div id="panelCom" class="contenidodiv">
 <table cellspacing="25px">
 <tr>
 <td></td>
 <td>
    <asp:Label ID="lblAdvertCom" runat="server" Font-Names="Trebuchet MS" 
                Font-Size="Medium" Font-Bold="True" Font-Italic="True" Font-Strikeout="False" 
                Font-Underline="False" Visible="False"></asp:Label><asp:Label ID="lblItemCom" runat="server" Font-Names="Trebuchet MS" 
                Font-Size="Medium" Font-Bold="True" Font-Italic="True" Font-Strikeout="False" 
                Font-Underline="False" Visible="False" ForeColor="Red"></asp:Label><asp:CheckBoxList ID="BtnListSubstancesCom" runat="server">
                </asp:CheckBoxList>
            <asp:Button ID="btnContinuarCom" runat="server" Text="Continuar" 
                Visible="False" onclick="btnContinuarCom_Click" />
            <asp:Button ID="btnCancelCom" runat="server" onclick="btnCancelCom_Click" 
                Text="Cancelar" Visible="False" /></td>
 </tr>
 <tr>
 <td valign="top"><asp:Panel ID="pnlComment" runat="server" DefaultButton="btnAddComment">
                    <asp:Table ID="Table9" runat="server" Width="250px">
                        <asp:TableRow>
                            <asp:TableCell HorizontalAlign="Left">
                                <asp:Label ID="Label33" runat="server" Text="Ingrese el comentario" Font-Names="Trebuchet MS" Font-Size="Smaller" Font-Bold="true" />
                            </asp:TableCell></asp:TableRow><asp:TableRow>
                            <asp:TableCell HorizontalAlign="Left">
                                <asp:TextBox ID="txtComment" runat="server" Text="" Enabled="true" TextMode="MultiLine" MaxLength="250" Height="100px" Width="350px" style="resize:none" /><br />
                                <asp:Button ID="btnAddComment" runat="server" ToolTip="Agregar" Text="Agregar"  OnClick="btnAddComment_OnClick"/>
                            </asp:TableCell></asp:TableRow></asp:Table></asp:Panel></td><td valign="top">

                             <table style="width:100%">
                                  <tr>
                                    <td style="text-align:left">
                                    <asp:Label ID="Label35" runat="server" Font-Names="Trebuchet MS" Font-Size="Large" Font-Bold="true" Text="Contraindicaciones Asociadas por Comentarios" />
                                    </td>
                                    <td style="text-align:right">
                                    <asp:Button ID="btnDeleteAllCom" runat="server" onclick="btnDeleteAllCom_Click" Text="Eliminar clasificación" Visible="true" OnClientClick="return confirmDeleteAll('por Comentarios')"/>
                                    </td>
                                </tr>
                            </table>
                
                <asp:GridView ID="grdComments" runat="server" 
                    AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" 
                    BorderStyle="None" BorderWidth="1px" CellPadding="3"
                    Font-Names="Trebuchet MS" Font-Size="Smaller" DataKeyNames="ProductCommentId" 
                    HorizontalAlign="Left" 
                     onrowdatabound="grdProductComment_RowDataBound" ShowHeaderWhenEmpty="true"><Columns>
                        <asp:BoundField DataField="ProductCommentId" Visible="False" />
                        <asp:TemplateField HeaderText="Sustancia del producto">
                            <HeaderStyle HorizontalAlign="Left" BackColor="#2694B4" ForeColor="White" />
                            <ItemStyle  Width="250px" />
                            <ItemTemplate>
                                <asp:Label ID="lblsubstanCom" runat="server">
                                    <%# DataBinder.Eval(Container.DataItem, "ActiveSubstance")%>
                                </asp:Label></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="Contraindicacíon">
                            <HeaderStyle HorizontalAlign="Left" BackColor="#2694B4" ForeColor="White" />
                            <ItemStyle  Width="250px" />
                            <ItemTemplate>
                                <asp:Label ID="lblComment" runat="server">
                                    <%# DataBinder.Eval(Container.DataItem, "Comments")%>
                                </asp:Label></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="Eliminar de producto">
                            <HeaderStyle Width="100px" HorizontalAlign="Center" BackColor="#2694B4" ForeColor="White" />
                            <ItemStyle HorizontalAlign="Center" Wrap="True" />
                            <ItemTemplate>
                                <asp:ImageButton ID="btnDelComment" runat="server"  ImageUrl="~/Images/delete.gif" AlternateText="Eliminar Comentario" 
                                Enabled="true"  OnClick="btnDeleteComment_OnClick"/>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
       </table>
 </div></td></tr>
 </table>
 </asp:Content>