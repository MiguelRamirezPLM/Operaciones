<%@ Page Language="C#" MasterPageFile="~/medinet.master" AutoEventWireup="true" CodeFile="classifiedProducts.aspx.cs" Inherits="classifiedProducts" %>
<%@ MasterType VirtualPath="~/medinet.master" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">

<script type="text/javascript">
    function checkMutuallyExclusive(target1) {
        document.getElementById(target1).checked = false;
    }

    function openReport(url) {
        window.open(url);
    }
     
</script>
<script type="text/javascript" language="javascript">
    function downFile(pathFile, fileName, contentType) {

        window.open('downFile.aspx?p=' + pathFile + '&f=' + fileName + '&c=' + contentType, "Downloading", 'height=200,width=1000,toolbar=no,location=no,resizable=no');
    }
    function mostrar_procesar() {
        //document.getElementById('procesando_div').style.display = '';

        if (document.getElementById) {
            document.getElementById('progressMessage').style.display = '';
            document.getElementById('screenLock').style.display = '';
            var progressMessage = document.getElementById('progressMessage');
            var screenLock = document.getElementById('screenLock');


            progressMessage.style.backgroundColor = 'white';
            progressMessage.style.position = 'relative';
            screenLock.style.width = '100%';
            screenLock.style.backgroundColor = '#F2F2F2';
            screenLock.style.height = '200%';
            screenLock.style.position = 'absolute';
            progressMessage.style.width = '400px';
            progressMessage.style.height = '80px';
            progressMessage.style.top = document.documentElement.clientHeight / 3 - progressMessage.style.height.replace('px', '') / 2 + 'px';
            progressMessage.style.left = document.body.offsetWidth / 2 - progressMessage.style.width.replace('px', '') / 2 + 'px';

        }
    }
    
    </script>
        <div id="screenLock" style='display: none;filter:alpha(opacity=50); opacity:0.5;'  >
                    </div>
                <div id="progressMessage" style='display: none;border: solid 1px gray;'>
                <table style="text-align:center;">
                <tr >
                <td style="text-align:center;"><img class="style1" src="images/loading.gif" style="height: 65px; width: 85px"/></td>
                <td style="text-align:center;">Generando archivo, por favor espere...</td>
                </tr>
                </table>
                    
                    
                </div>
    <asp:Table ID="tblOptions" runat="server">
        <asp:TableRow BorderWidth="1px" Font-Names="Trebuchet MS" Font-Size="Smaller" Font-Bold="true">
            <asp:TableCell BorderWidth="1px" Width="220px" HorizontalAlign="Left">
                <asp:Label ID="lblChooseOther" runat="server" Text="Elegir otro laboratorio" />
            </asp:TableCell><asp:TableCell BorderWidth="1px" Width="220px" HorizontalAlign="Right">
                <asp:Label ID="lblQtyRecords" runat="server" Text="Registros por página" />
            </asp:TableCell><asp:TableCell BorderWidth="1px" Width="220px" Visible="true" HorizontalAlign="Right">
                <asp:Label ID="Label7" runat="server" Text="Actividad del Día" />
            </asp:TableCell><asp:TableCell BorderWidth="1px" Width="220px" Visible="true" HorizontalAlign="Right">
                <asp:Label ID="Label1" runat="server" Text="Actividad Global" />
            </asp:TableCell><asp:TableCell HorizontalAlign="center"  ColumnSpan ="2" BorderWidth="1px">
                    <asp:Label ID="Label9" runat="server" Text="Exportar productos" />
                </asp:TableCell></asp:TableRow><asp:TableRow BorderWidth="1px">
            <asp:TableCell BorderWidth="1px" Width="220px" HorizontalAlign="Left">
                <asp:ImageButton ID="imgBtnBackLabs" runat="server" ImageUrl="images/regresar.png" ToolTip="Elegir otro laboratorio" OnClick="imgBtnBackLabs_Click" />
            </asp:TableCell><asp:TableCell BorderWidth="1px" Width="220px" HorizontalAlign="Right">
                <asp:DropDownList ID="ddlPageSize" runat="server" AutoPostBack="True" Font-Names="Arial" Font-Size="10pt" OnSelectedIndexChanged="ddlPageSize_SelectedIndexChanged">
                    <asp:ListItem Value="-1" >Ilimitado</asp:ListItem>
                    <asp:ListItem Value="5">5</asp:ListItem>
                    <asp:ListItem Value="15" Selected="True">15</asp:ListItem>
                    <asp:ListItem Value="25">25</asp:ListItem>
                    <asp:ListItem Value="50">50</asp:ListItem>
                    <asp:ListItem Value="100">100</asp:ListItem>
                </asp:DropDownList>
            </asp:TableCell><asp:TableCell BorderWidth="1px" Width="220px" HorizontalAlign="Right">
                <asp:Button ID="btnViewReport" runat="server" ToolTip="Ver reporte" Visible="true" Text="Ver reporte" />
            </asp:TableCell><asp:TableCell BorderWidth="1px" Width="220px" HorizontalAlign="Right">
                <asp:Button ID="btnGlobalReport" runat="server" ToolTip="Ver reporte" Text="Ver reporte global" />
            </asp:TableCell><asp:TableCell  HorizontalAlign="center" BorderWidth="1px">
                <asp:ImageButton ID="imgBtnExport" runat="server" ImageUrl="images/PDF.png" 
                    ToolTip="Exportar productos participantes a PDF" Width="45px" 
                    onclick="imgBtnExport_Click"  OnClientClick="mostrar_procesar();" Visible="false"/>
                </asp:TableCell><asp:TableCell BorderWidth="1px" HorizontalAlign="center"> 
                <asp:ImageButton ID="imgBtnExportxls" runat="server" ImageUrl="images/excel.png" 
                    ToolTip="Exportar productos participantes a excel" Width="45px" onclick="imgBtnExportxls_Click" OnClientClick="mostrar_procesar();"/>
                </asp:TableCell><%--<asp:TableCell>
                    <span id='procesando_div' style='display: none'>Procesando...
                    <img src='Images/loading.gif' id='procesando' align='center' width="60px" height="60px" />
                    </span>
                </asp:TableCell>--%></asp:TableRow></asp:Table><br />

    <asp:Button 
        ID="btnSaveParticipantTop" runat="server" ToolTip="Guardar" Text="Guardar" 
        OnClick="btnSaveParticipant_Click" /><br />

    <asp:GridView ID="grdProducts" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3"
        Font-Names="Trebuchet MS" Font-Size="Smaller" DataKeyNames="ProductId,PharmaFormId,DivisionId,CategoryId" OnRowDataBound="grdProducts_RowDataBound"
        OnPageIndexChanging="grdProducts_PageIndexChanging" OnRowEditing="grdProducts_RowEditing" OnRowUpdating="grdProducts_RowUpdating" >
        <Columns>
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
                    </asp:Label></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="Tipo de Producto">
                <HeaderStyle HorizontalAlign="Left" BackColor="#2694B4" ForeColor="White" />
                <ItemStyle  Width="100px" />
                <ItemTemplate>
                    <asp:Label ID="lblProductType" runat="server">
                        <%# DataBinder.Eval(Container.DataItem, "ProductType")%>
                    </asp:Label></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="Síntomas del producto">
                <HeaderStyle Width="150px" HorizontalAlign="Center" BackColor="#2694B4" ForeColor="White" />
                <ItemStyle  Wrap="true" Width="150px" />
                <ItemTemplate>
                    <asp:GridView ID="grdSymptoms" runat="server" AutoGenerateColumns="false">
                    <Columns>
            <asp:TemplateField HeaderText="Nombre">
                <HeaderStyle BackColor="#2694B4" ForeColor="White" HorizontalAlign="Center"/>
                <ItemStyle  Width="150px" HorizontalAlign="Center" />
                <ItemTemplate>
                    <asp:Label ID="lblEditions" runat="server">
                        <%# DataBinder.Eval(Container.DataItem, "SymptomName") %>
                    </asp:Label></ItemTemplate></asp:TemplateField></Columns></asp:GridView></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="Ediciones">
                <HeaderStyle HorizontalAlign="Left" BackColor="#2694B4" ForeColor="White" />
                <ItemStyle  Width="100px" />
                <ItemTemplate>
                    <asp:Label ID="lblEditions" runat="server">
                        <%# DataBinder.Eval(Container.DataItem, "Editions")%>
                    </asp:Label></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="Nuevo">
                <HeaderStyle Width="30px" HorizontalAlign="Center" BackColor="#2694B4" ForeColor="White" />
                <ItemStyle HorizontalAlign="Center" Wrap="True" Width="50px" />
                <ItemTemplate>
                    <asp:CheckBox ID="NewProduct" runat="server" AutoPostBack="false" Enabled="false" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Con Cambios">
                <HeaderStyle Width="30px" HorizontalAlign="Center" BackColor="#2694B4" ForeColor="White" />
                <ItemStyle HorizontalAlign="Center" Wrap="True" Width="50px" />
                <ItemTemplate>
                    <asp:CheckBox ID="ModifiedContent" runat="server" AutoPostBack="false"  Enabled="false" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Rubros con cambios">
                <HeaderStyle HorizontalAlign="Center" BackColor="#2694B4" ForeColor="White" />
                <ItemStyle HorizontalAlign="Left" Wrap="True" Width="50px" />
                <ItemTemplate>
                    <asp:BulletedList ID="bulletsModifiedAttributes" runat="server" Width="200px" />
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:CheckBoxList ID="cblModifiedAttributeGroups" runat="server" Enabled="true" Width="500px"  />
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Editar Rubros">
                <HeaderStyle Width="45px" HorizontalAlign="Center" BackColor="#2694B4" ForeColor="White" />
                <ItemStyle HorizontalAlign="Center" Wrap="True" />
                <ItemTemplate>
                    <asp:ImageButton ID="btnEdit" runat="server" CommandName="Edit" ImageUrl="~/Images/editar.gif" AlternateText="Editar Rubros" Visible="false" />
                </ItemTemplate>
                <EditItemTemplate >
                    <asp:ImageButton ID="btnSave" runat="server" CommandName="Update" ImageUrl="~/Images/salvar.jpg" AlternateText="Guardar Rubros" />
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Editar presentaciones">
                <HeaderStyle Width="45px" HorizontalAlign="Center" BackColor="#2694B4" ForeColor="White" />
                <ItemStyle HorizontalAlign="Left" Wrap="True" Width="50px" />
                <ItemTemplate>
                    <asp:Button ID="btnEditPresentations" runat="server" Text="Editar presentaciones" Visible="true" OnClick="btnEditPresentations_Click" />
                </ItemTemplate>
            </asp:TemplateField>
            
             <asp:TemplateField HeaderText="Presentaciones">
                <HeaderStyle Width="500px" HorizontalAlign="Center" BackColor="#2694B4" ForeColor="White" />
                <ItemStyle  Wrap="true" Width="500px" />
                <ItemTemplate>
                    <asp:GridView ID="grdPresent" runat="server" AutoGenerateColumns="false">
                    <Columns>
            <asp:TemplateField HeaderText="Ediciones">
                <HeaderStyle HorizontalAlign="Left" BackColor="#2694B4" ForeColor="White" />
                <ItemStyle  Width="300px" />
                <ItemTemplate>
                    <asp:Label ID="lblEditions" runat="server">
                        <%# DataBinder.Eval(Container.DataItem, "Editions") %>
                    </asp:Label></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="Presentación">
                <HeaderStyle HorizontalAlign="Left" BackColor="#2694B4" ForeColor="White" />
                <ItemStyle  Width="300px" />
                <ItemTemplate>
                    <asp:Label ID="lblPresentation" runat="server">
                        <%# DataBinder.Eval(Container.DataItem, "Presentation")%>
                    </asp:Label></ItemTemplate></asp:TemplateField></Columns></asp:GridView></ItemTemplate></asp:TemplateField></Columns></asp:GridView><br />
    <asp:Button ID="btnSaveParticipant" runat="server" ToolTip="Guardar" Text="Guardar" OnClick="btnSaveParticipant_Click" />
</asp:Content>