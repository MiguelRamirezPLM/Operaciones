<%@ Page Language="C#" MasterPageFile="~/medinet.master" AutoEventWireup="true" CodeFile="assignedProducts.aspx.cs" Inherits="assignedProducts" %>

<%@ Register Assembly="DevExpress.Web.ASPxGridView.v10.1, Version=10.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>

<%@ MasterType VirtualPath="~/medinet.master" %>

<%@ Register assembly="DevExpress.Web.ASPxEditors.v10.1, Version=10.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>

<%@ Register assembly="DevExpress.XtraReports.v10.1.Web, Version=10.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.XtraReports.Web" tagprefix="dx" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
<script type="text/javascript" language="javascript">
    function downFile(pathFile, fileName, contentType) {
        //window.open('downFile.aspx?p=' + pathFile + '&f=' + fileName + '&c=' + contentType, "Downloading");
        window.open('downFile.aspx?p=' + pathFile + '&f=' + fileName + '&c=' + contentType, "Downloading", 'height=200,width=1000,toolbar=no,location=no,resizable=no');
    }
   function mostrar_procesar()
{
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
                <td style="text-align:center;">Por favor espere...</td>
                </tr>
                </table>
                    
                    
                </div>
    <table cellspacing="3" cellpadding="3" border="0">
        <tr>
            <td>
                <asp:ImageButton ID="imgBtnBack" runat="server" ImageUrl="images/regresar.png" ToolTip="Elegir otra edición" OnClick="imgBtnBack_Click" />
            </td>
            <td>
                <asp:ImageButton ID="imgBtnSubst" runat="server" ImageUrl="images/sust-act.jpg" ToolTip="Catálogo de sustancias" Width="70px" Visible="false" OnClick="imgBtnSubst_Click" />
            </td>
            <td>
                <asp:ImageButton ID="imgBtnInd" runat="server" ImageUrl="images/indicaciones.jpg" ToolTip="Catálogo de indicaciones" Width="70px" Visible="false" OnClick="imgBtnInd_Click" />
            </td>
            <td>
                <asp:ImageButton ID="imgBtnATC" runat="server" ImageUrl="images/terap.jpg" ToolTip="Catálogo ATC" Visible="false" Width="70px" OnClick="imgBtnATC_Click" />
            </td>
            <td>
                <asp:ImageButton ID="imgBtnATCMed" runat="server" ImageUrl="images/atc_med.jpg" ToolTip="Ingresar productos por ATC" Width="70px" OnClick="imgBtnATCMed_Click" />
            </td>
            <td>
            <asp:ImageButton ID="imgBtnExport" runat="server" ImageUrl="images/PDF.png" 
                    ToolTip="Exportar productos participantes a PDF" Width="45px" 
                    onclick="imgBtnExport_Click" OnClientClick="mostrar_procesar();" 
                    Visible="False" />
            </td>
            <td>
            <asp:ImageButton ID="imgBtnExportxls" runat="server" ImageUrl="images/excel.png" 
                    ToolTip="Exportar productos participantes" Width="49px" 
                    onclick="imgBtnExportxls_Click"  OnClientClick="mostrar_procesar();" 
                    Height="54px" />
            </td>
            <td>
                    
                    <%--<span id='procesando_div' style='display: none' >Procesando...
                    <img src='Images/loading.gif' id='procesando' align='center' width="60px" height="60px" />
                    </span>--%>
                    
            </td>
        </tr>
            </table>
             
                

    <br />
    <br />

    <asp:DropDownList ID="ddlPageSize" runat="server" AutoPostBack="True" Font-Names="Arial" Font-Size="6pt" 
        OnSelectedIndexChanged="ddlPageSize_SelectedIndexChanged">
        <asp:ListItem Value="-1" >Ilimitado</asp:ListItem>
        <asp:ListItem Value="5">5</asp:ListItem>
        <asp:ListItem Value="10">10</asp:ListItem>
        <asp:ListItem Value="25">25</asp:ListItem>
        <asp:ListItem Value="50" Selected="True">50</asp:ListItem>
        <asp:ListItem Value="100">100</asp:ListItem>
    </asp:DropDownList>
    
    <asp:Button ID="btnSaveTop" runat="server" Height="24px" 
        onclick="btnSaveTop_Click" Text="Guardar"  />
    
    <br />
    <br />
     
    <dx:ASPxGridView ID="aspGrdProducts" runat="server" 
        ClientInstanceName="aspGrdProducts" KeyFieldName="ProductId" 
        AutoGenerateColumns="False" OnRowCommand="aspGrdProducts_RowCommand" 
        onhtmlrowcreated="aspGrdProducts_HtmlRowCreated" 
        onselectionchanged="aspGrdProducts_SelectionChanged" >
        
        <SettingsBehavior SortMode="Custom" />
        <Settings ShowFilterRow="True" ShowFilterRowMenu="true" ShowGroupPanel="True" />

        <Styles>
            <Header BackColor="#2694B4" Font-Names="Verdana" Font-Size="12px" Font-Bold="true" ForeColor="White" HorizontalAlign="Left" />
            <AlternatingRow Enabled="True" />
            <GroupRow HorizontalAlign="Left" Font-Bold="true" />
            <PagerBottomPanel Font-Names="Verdana" Font-Size="10px" ForeColor="#7cbdd0" />
            <PagerTopPanel Font-Names="Verdana" Font-Size="10px" ForeColor="#7cbdd0" />
        </Styles>
        
        <Columns>
            
            <dx:GridViewDataColumn FieldName="ProductId" Visible="False" ></dx:GridViewDataColumn>
            <dx:GridViewDataColumn FieldName="PharmaFormId" Visible="False"></dx:GridViewDataColumn>
            <dx:GridViewDataColumn FieldName="ProductTypeId" Visible="False"></dx:GridViewDataColumn>
            
            <dx:GridViewDataColumn FieldName="Brand" Caption="Producto" VisibleIndex="0">
                <CellStyle CssClass="label" HorizontalAlign="Left" />
            </dx:GridViewDataColumn>
            
            <dx:GridViewDataColumn FieldName="PharmaForm" Caption="Forma Farmacéutica" VisibleIndex="1">
                <CellStyle CssClass="label" HorizontalAlign="Left" />
            </dx:GridViewDataColumn>

            <dx:GridViewDataColumn FieldName="DivisionShortName" Caption="Laboratorio" VisibleIndex="2">
                <CellStyle CssClass="label" HorizontalAlign="Left" />
            </dx:GridViewDataColumn>
            
            <dx:GridViewDataColumn FieldName="ProductType" Caption="Tipo de producto" VisibleIndex="3" >
            
            <DataItemTemplate>
                <asp:DropDownList ID="ddlProductType" runat="server" AutoPostBack="false" >
                </asp:DropDownList>
            </DataItemTemplate>
            </dx:GridViewDataColumn>
            
           
            <dx:GridViewDataCheckColumn FieldName="ATC" Caption="Terapéutico EphMRA" VisibleIndex="4" >
                <PropertiesCheckEdit DisplayTextChecked="Clasificado" DisplayTextUnchecked="No Clasificado" />
            </dx:GridViewDataCheckColumn>

            <dx:GridViewDataCheckColumn FieldName="Substance" Caption="Sustancias" VisibleIndex="5" >
                <PropertiesCheckEdit DisplayTextChecked="Clasificado" DisplayTextUnchecked="No Clasificado" />
            </dx:GridViewDataCheckColumn>
                         
            <dx:GridViewDataCheckColumn FieldName="Indication" Caption="Indicaciones" VisibleIndex="6" >
                <PropertiesCheckEdit DisplayTextChecked="Clasificado" DisplayTextUnchecked="No Clasificado" />
            </dx:GridViewDataCheckColumn>
            <dx:GridViewDataCheckColumn FieldName="ICD" Caption="CIE-10" VisibleIndex="7">
                <PropertiesCheckEdit DisplayTextChecked="Clasificado" DisplayTextUnchecked="No Clasificado" />
            </dx:GridViewDataCheckColumn>
            <dx:GridViewDataCheckColumn FieldName="Encyclopedia" Caption="Enciclopedias" VisibleIndex="8" >
                <PropertiesCheckEdit DisplayTextChecked="Clasificado" DisplayTextUnchecked="No Clasificado" />
            </dx:GridViewDataCheckColumn>
            <%--<dx:GridViewDataCheckColumn FieldName="Interaction" Caption="Interacciones" VisibleIndex="7" >
                <PropertiesCheckEdit DisplayTextChecked="Clasificado" DisplayTextUnchecked="No Clasificado" />
            </dx:GridViewDataCheckColumn>--%>              
            <dx:GridViewDataColumn FieldName="Interaction" Caption="Interacciones" VisibleIndex="9">
                <CellStyle CssClass="label" HorizontalAlign="Left" />
            </dx:GridViewDataColumn>
            
           <dx:GridViewDataCheckColumn FieldName="Symptom"   Caption="Síntomas" VisibleIndex="10" >
                <PropertiesCheckEdit DisplayTextChecked="Clasificado" DisplayTextUnchecked="No Clasificado" />
            </dx:GridViewDataCheckColumn>
            <dx:GridViewDataColumn FieldName="Contraindication" Caption="Contraindicaciones" VisibleIndex="11" >
                <CellStyle CssClass="label" HorizontalAlign="Left" />
            </dx:GridViewDataColumn>
            <dx:GridViewDataCheckColumn FieldName="ATCOMS" Caption="Terapéutico OMS" VisibleIndex="12" >
                <PropertiesCheckEdit DisplayTextChecked="Clasificado" DisplayTextUnchecked="No Clasificado" />
            </dx:GridViewDataCheckColumn>
            <dx:GridViewDataHyperLinkColumn Caption="Editar Índices" VisibleIndex="13" >
                <CellStyle CssClass="label" HorizontalAlign="Center"/>
                <DataItemTemplate>                
                    <asp:LinkButton ID="btnEdit" runat="server"  CommandName="Edit"
                                    CommandArgument='<%# getData((int)(DataBinder.Eval(Container.DataItem, "divisionId")), (int)(DataBinder.Eval(Container.DataItem, "categoryId")),
                                    (int)DataBinder.Eval(Container.DataItem, "productId"),(int)DataBinder.Eval(Container.DataItem, "pharmaFormId")) %>'
                                    Enabled="true" OnClientClick="mostrar_procesar();">
                        <asp:Image ID="imgEdit" runat="server" ImageUrl="~/Images/editar.gif" BorderColor="InactiveBorder" />
                    </asp:LinkButton>
                </DataItemTemplate>
            </dx:GridViewDataHyperLinkColumn>           
        </Columns>

        <SettingsPager PageSize="50" Position="TopAndBottom"></SettingsPager>

    </dx:ASPxGridView>

    <br />

    <asp:Button ID="BtnSaveDown" runat="server" Text="Guardar" />

</asp:Content>

