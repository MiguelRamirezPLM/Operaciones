<%@ Page Language="C#" MasterPageFile="~/medinet.master" AutoEventWireup="true" CodeFile="laboratories.aspx.cs" Inherits="laboratories" %>
<%@ MasterType VirtualPath="~/medinet.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
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
     <%--<asp:ImageButton ID="imgBtnBackProducts" runat="server" ImageUrl="images/regresar.png" ToolTip="Regresar a productos" OnClick="imgBtnBackProducts_Click" />--%>
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
    <table>
        <tr>
            <td><asp:Label ID="lblCountry" runat="server" Text="Label">País:</asp:Label></td>
            <td>
                <asp:DropDownList ID="ddlCountries" runat="server" Width="150px" CssClass="label" AutoPostBack="True" OnSelectedIndexChanged="ddlCountries_SelectedIndexChanged" />
            </td>
        </tr>
        <tr>
            <td><asp:Label ID="lblBook" runat="server" Text="Label">Libro:</asp:Label></td>
            <td>
                <asp:DropDownList ID="ddlBooks" runat="server" Width="400px" CssClass="label" Enabled="False" AutoPostBack="True" OnSelectedIndexChanged="ddlBooks_SelectedIndexChanged" />
            </td>
        </tr>
        <tr>
            <td><asp:Label ID="lblEdition" runat="server" Text="Label">Edición:</asp:Label></td>
            <td>
                <asp:DropDownList ID="ddlEditions" runat="server" Width="400px" CssClass="label" Enabled="False" AutoPostBack="True" OnSelectedIndexChanged="ddlEditions_SelectedIndexChanged" />
            </td>
            <td>
                <asp:Button ID="btnPagination" runat="server" Text="Paginar ésta obra." Visible="false" OnClick="btnPagination_OnClick" />
            </td>
            <td>
                <asp:Button ID="btnPaginationSymptoms" runat="server" Text="Paginar Síntomas" 
                    Visible="False" onclick="btnPaginationSymptoms_Click"  />
            </td>
            <td> 
             <asp:ImageButton ID="imgBtnExport" runat="server" ImageUrl="images/PDF.png" 
                    ToolTip="Exportar productos participantes a PDF" Width="45px" 
                    onclick="imgBtnExport_Click" Visible="False" OnClientClick="mostrar_procesar();" />
                
                <asp:ImageButton ID="imgBtnExportxls" runat="server" ImageUrl="images/excel.png" 
                    ToolTip="Exportar productos participantes a excel" Width="45px" 
                    onclick="imgBtnExportxls_Click" Visible="False" OnClientClick="mostrar_procesar();"/>
                
            </td>   
            <%--<td>
                    <span id='procesando_div' style='display: none'>Procesando...
                    <img src='Images/loading.gif' id='procesando' align='center' width="60px" height="60px" />
                    </span>
            </td>--%>
        </tr>
        <tr>
            <td><asp:Label ID="lblLaboratory" runat="server" Text="Laboratorio:" /></td>
            <td>
                <asp:DropDownList ID="ddlDivisions" runat="server" Width="400px" CssClass="label" Enabled="True" AutoPostBack="True" OnSelectedIndexChanged="ddlDivisions_SelectedIndexChanged" />
            </td>
        </tr>
    </table>
</asp:Content>