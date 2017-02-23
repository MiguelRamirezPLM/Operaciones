<%@ Page Language="C#" MasterPageFile="~/medinet.master" AutoEventWireup="true" CodeFile="encyclopediaImages1.aspx.cs" Inherits="encyclopediaImages1" %>
<%@ MasterType VirtualPath="~/medinet.master" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v10.1, Version=10.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxEditors.v10.1, Version=10.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxTreeList.v10.1, Version=10.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxTreeList" tagprefix="dx" %>
<%@ Register assembly="DevExpress.XtraReports.v10.1.Web, Version=10.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.XtraReports.Web" tagprefix="dx" %>
  

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
    function hh(lnk) {
        var row = lnk.parentNode.parentNode;
        var rowIndex = row.rowIndex - 1;


        $.ajax({
            type: "POST",
            url: "ProductPresentationsI.aspx/siguiente",
            data: "{rowIndex:" + rowIndex + "}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",

            error: function (result) {
                alert('ERROR ');
            }
        });
        return false;
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
            </asp:TableCell></asp:TableRow><asp:TableRow BorderWidth="1px">
            <asp:TableCell BorderWidth="1px" Width="220px" HorizontalAlign="Left">
                <asp:ImageButton ID="imgBtnBackLabs" runat="server" ImageUrl="images/regresar.png" ToolTip="Elegir otro laboratorio" OnClick="imgBtnBackLabs_Click" />
            </asp:TableCell><asp:TableCell BorderWidth="1px" Width="220px" HorizontalAlign="Right">
                <asp:DropDownList ID="ddlPageSize" runat="server" AutoPostBack="True" Font-Names="Arial" Font-Size="10pt" OnSelectedIndexChanged="ddlPageSize_SelectedIndexChanged">
                    <asp:ListItem Value="-1" >Ilimitado</asp:ListItem>
                    <asp:ListItem Value="5">5</asp:ListItem>
                    <asp:ListItem Value="15" Selected="True">15</asp:ListItem>
                    <asp:ListItem Value="25">25</asp:ListItem>
                    <asp:ListItem Value="50" >50</asp:ListItem>
                    <asp:ListItem Value="100">100</asp:ListItem>
                </asp:DropDownList>
            </asp:TableCell><%--<asp:TableCell>
                    <span id='procesando_div' style='display: none'>Procesando...
                    <img src='Images/loading.gif' id='procesando' align='center' width="60px" height="60px" />
                    </span>
                </asp:TableCell>--%></asp:TableRow></asp:Table><br /><br />

 

    <dx:ASPxGridView ID="grdProducts" runat="server" AutoGenerateColumns="False"
        DataKeyNames="EncyclopediaId,EncyclopediaTypeId" KeyFieldName="EncyclopediaId,EncyclopediaTypeId" OnRowDataBound="grdProducts_RowDataBound"
         OnRowEditing="grdProducts_RowEditing" onhtmlrowcreated="aspGrdProducts_HtmlRowCreated" SettingsPager-AlwaysShowPager="true"  >
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
            <dx:GridViewDataColumn FieldName="EncyclopediaId" Visible="False" />
            <dx:GridViewDataColumn FieldName="EncyclopediaTypeId" Visible="False" />
            
            <dx:GridViewDataColumn Caption="Nombre" FieldName="EncyclopediaName">
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" BackColor="#2694B4" ForeColor="White" />
                <CellStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                <DataItemTemplate>
                    <asp:Label ID="lblEncyName" runat="server">
                        <%# DataBinder.Eval(Container.DataItem, "EncyclopediaName") %>
                    </asp:Label></DataItemTemplate></dx:GridViewDataColumn><dx:GridViewDataColumn Caption="Tipo de Enciclopedia" FieldName="EncyclopediaType">
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" BackColor="#2694B4" ForeColor="White" />
                <CellStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                <DataItemTemplate>
                    <asp:Label ID="lblType" runat="server">
                        <%# DataBinder.Eval(Container.DataItem, "EncyclopediaType")%>
                    </asp:Label></DataItemTemplate></dx:GridViewDataColumn><dx:GridViewDataColumn Caption="ICD" FieldName="ICD">
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" BackColor="#2694B4" ForeColor="White" />
                <CellStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                <DataItemTemplate>
                    <asp:Label ID="lblICD" runat="server">
                        <%# DataBinder.Eval(Container.DataItem, "ICD")%>
                    </asp:Label></DataItemTemplate></dx:GridViewDataColumn>
             <dx:GridViewDataColumn Caption="Editar Imágenes">
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" BackColor="#2694B4" ForeColor="White" />
                <CellStyle HorizontalAlign="Center" Wrap="True" />
                <DataItemTemplate>
                    <asp:ImageButton ID="btnEditEncyclopedia" runat="server" ImageUrl="~/Images/editar.gif" Text="Editar" Visible="true" OnClick="btnEditEncyclopedia_Click" />
                </DataItemTemplate>
            </dx:GridViewDataColumn>
        </Columns>
        <SettingsPager PageSize="15" Position="TopAndBottom"></SettingsPager>
    </dx:ASPxGridView><br />
    

</asp:Content>