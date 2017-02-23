<%@ Page Language="C#" MasterPageFile="~/deaq.master" AutoEventWireup="true" CodeFile="Products2.aspx.cs" Inherits="Products2" %>
<%@ MasterType VirtualPath="~/deaq.master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="DevExpress.Web.v10.1, Version=10.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxCallbackPanel" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.v10.1, Version=10.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGlobalEvents" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v10.1, Version=10.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v10.1, Version=10.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPopupControl" TagPrefix="dxpc" %>
<%@ Register Assembly="DevExpress.Web.v10.1, Version=10.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dxp" %>
<%@ Register Assembly="DevExpress.Web.v10.1, Version=10.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxRoundPanel" TagPrefix="dxrp" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v10.1, Version=10.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dxwgv" %>

<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
<script type="text/javascript">

    function checkMutuallyExclusive(target1, target2) {
        var participate, mentionated, participateid, mentionatedid, newp, newpid;
        var row = target1.substring(target1.lastIndexOf("l") + 1, target1.lastIndexOf("l") + 2);
        
        document.getElementById(target1).checked = false;
        alert(row);
        grdProducts.GetRowValues(row, 'Participate');
        participateid = "ctl00_ContentPlaceHolder1_grdProducts_cell" + target1.substring(target1.lastIndexOf("l") + 1, target1.lastIndexOf("l") + 2) + "_11_Participate";
        alert(participateid);
        mentionatedid = "ctl00_ContentPlaceHolder1_grdProducts_cell" + target1.substring(target1.lastIndexOf("l") + 1, target1.lastIndexOf("l") + 2) + "_11_Mentionated";;
        newpid = "ctl00_ContentPlaceHolder1_grdProducts_cell" + target1.substring(target1.lastIndexOf("l") + 1, target1.lastIndexOf("l") + 2) + "_11_NewProduct";
        participate = $("#Participate")
        mentionated = grdProducts.GetRowValues(row, 'Mentionated');
        newp = grdProducts.GetRowValues(row, 'NewProduct');
        alert(participate);
        if (target2 != null) {
            // alert(target1);
            if (participate.checked == false) {
                document.getElementById(target2).checked = false;
                document.getElementById(target2).disabled = true;
            }
            else if (participate.checked == true && newp.checked == false)
                document.getElementById(target2).disabled = false;

        }
    }
    function checkNotMutuallyExclusive(target1, value) {
        document.getElementById(target1).checked = value;
    }

    function openReport(url) {
        window.open(url);
    }

</script>
<script type="text/javascript" language="javascript">
  
    $(document).ready(function () {
        $("#main div").hide(); // Initially hide all content
        $("#tabs li:first").attr("id", "current"); // Activate first tab
        $("#main div:first").fadeIn(); // Show first tab content


        $("#menu ul li a").click(function (e) {
            e.preventDefault();

            $("#menu ul li a").each(function () {
                $(this).removeClass("active");
            });

            $(this).addClass("active");

            $("h3").html($(this).attr("title"));

        });
        

        $('#tabs a').click(function (e) {
            e.preventDefault();
            $("#main div").hide(); //Hide all content
            $("#tabs li").attr("id", ""); //Reset id's
            $(this).parent().attr("id", "current"); // Activate this
            $('#' + $(this).attr('title')).fadeIn(); // Show content for current tab
        });
    })();

   
    //google.load("jquery", "1.3.1");

    //google.setOnLoadCallback(function () {
    //    // Just for demonstration purposes, change the contents/active state using jQuery
    //    $("#menu ul li a").click(function (e) {
    //        e.preventDefault();

    //        $("#menu ul li a").each(function () {
    //            $(this).removeClass("active");
    //        });

    //        $(this).addClass("active");

    //        $("h3").html($(this).attr("title"));
    //    });
    //});



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


   



      <asp:ImageButton ID="imgBtnBackLabs" runat="server" ImageUrl="images/regresar.png" ToolTip="Elegir otro laboratorio" OnClick="btnLabs_Click" />
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

     <div id="content">
		<div id="menu">
			<ul id="tabs">
				<li><a href="#" title="tab1" class="active">Productos</a></li>
				<li><a href="#" title="tab2" >Agregar Producto</a></li>
				<li><a href="#" title="tab3">Agregar Forma</a></li>
				<li><a href="#" title="tab4">Cambiar Producto</a></li>
				
			</ul>
		</div>
		<div id="main">
            <div id="tab1" style="float:left; ">
    <asp:Table ID="tblDivisionOptions" runat="server">
        <asp:TableRow BorderWidth="1px" Font-Names="Trebuchet MS" Font-Size="Smaller" Font-Bold="true">
            <asp:TableCell BorderWidth="1px" Width="220px" HorizontalAlign="Right">
                <asp:Label ID="lblEditLaboratory" runat="server" Text="Editar la información de éste laboratorio" />
            </asp:TableCell><asp:TableCell BorderWidth="1px" Width="220px" HorizontalAlign="Right">
                <asp:Label ID="Label6" runat="server" Text="Cambiar productos de laboratorio" />
            </asp:TableCell>

        </asp:TableRow><asp:TableRow BorderWidth="1px">
            <asp:TableCell BorderWidth="1px" Width="220px" HorizontalAlign="Right">
                <asp:Button ID="btnEditLaboratory" runat="server" ToolTip="Editar laboratorio" Text="Editar laboratorio" OnClick="btnEditLaboratory_Click" />
            </asp:TableCell><asp:TableCell BorderWidth="1px" Width="220px" HorizontalAlign="Right">
                <asp:Button ID="btnChangeDivision" runat="server" ToolTip="Cambiar productos de laboratorio" Text="Cambiar productos" OnClick="btnChangeDivision_Click" />
            </asp:TableCell></asp:TableRow></asp:Table><br />
    <br />
      </div>


            <div id="tab2" style="float:left; ">
    <asp:Table runat="server" >
        <asp:TableRow BorderWidth="1px" Font-Names="Trebuchet MS" Font-Size="Smaller" Font-Bold="true">
            <asp:TableCell>
                <asp:Label ID="lblTitle" runat="server" Text="Ingresar producto nuevo" />
            </asp:TableCell></asp:TableRow><asp:TableRow BorderWidth="1px" Font-Names="Trebuchet MS" Font-Size="Smaller" Font-Bold="true">
            <asp:TableCell BorderWidth="1px">
                <asp:Label ID="lblBrandToAdd" runat="server" Text="Nombre" />
            </asp:TableCell>
                <asp:TableCell BorderWidth="1px">
                <asp:Label ID="Label2" runat="server" Text="Descripcion" />
            </asp:TableCell>
                <asp:TableCell BorderWidth="1px">
                <asp:Label ID="Label10" runat="server" Text="Codigo de Registro" />
            </asp:TableCell><asp:TableCell BorderWidth="1px">
                <asp:Label ID="lblPharmaFormToAdd" runat="server" Text="Forma Farmacéuitca" />
            </asp:TableCell><asp:TableCell BorderWidth="1px">
                <asp:Label ID="lblGuardar" runat="server" Text="Agregar producto" />
            </asp:TableCell></asp:TableRow><asp:TableRow BorderWidth="1px">
            <asp:TableCell BorderWidth="1px">
                <asp:TextBox ID="txtBrandToAdd" runat="server" Text="" />
            </asp:TableCell>
                <asp:TableCell BorderWidth="1px">
                <asp:TextBox ID="txtDescriptionToAdd" runat="server" Text="" />
            </asp:TableCell>
                <asp:TableCell BorderWidth="1px">
                <asp:TextBox ID="txtRegisterToAdd" runat="server" Text="" />
            </asp:TableCell><asp:TableCell BorderWidth="1px">
                <asp:DropDownList ID="ddlPharmaFormToAdd" runat="server">
                </asp:DropDownList>
            </asp:TableCell><asp:TableCell BorderWidth="1px">
                <asp:Button ID="btnAddProduct" runat="server" Text="Agregar producto" OnClick="btnAddProduct_Click" />
            </asp:TableCell></asp:TableRow></asp:Table><br /><br />
      </div>
            <div id="tab3" style="float:left; ">

    <asp:Table runat="server">
        <asp:TableRow BorderWidth="1px" Font-Names="Trebuchet MS" Font-Size="Smaller" Font-Bold="true">
            <asp:TableCell>
                <asp:Label ID="lblTitle2" runat="server" Text="Agregar nueva forma farmacéutica" />
            </asp:TableCell></asp:TableRow><asp:TableRow BorderWidth="1px" Font-Names="Trebuchet MS" Font-Size="Smaller" Font-Bold="true">
            <asp:TableCell BorderWidth="1px">
                <asp:Label ID="Label1" runat="server" Width="300px" Text="Producto" />
            </asp:TableCell><asp:TableCell BorderWidth="1px">
                <asp:Label ID="Label4" runat="server" Width="300px" Text="Nueva Forma Farmacéuitca" />
            </asp:TableCell><asp:TableCell BorderWidth="1px">
                <asp:Label ID="Label5" runat="server" Width="140px" Text="Agregar nueva forma farmacéutica" />
            </asp:TableCell></asp:TableRow><asp:TableRow>
            <asp:TableCell BorderWidth="1px">
                <asp:DropDownList ID="ddlProductToAdd" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlProductToAdd_SelectedIndexChanged" />
            </asp:TableCell><asp:TableCell BorderWidth="1px">
                <asp:DropDownList ID="ddlNewPharmaFormToAdd" runat="server" Enabled="false" />
            </asp:TableCell><asp:TableCell BorderWidth="1px">
                <asp:Button ID="btnAddPharmaForm" runat="server" Text="Agregar Forma" OnClick="btnAddPharmaForm_Click" />
            </asp:TableCell></asp:TableRow></asp:Table><br /><br />
                </div>


                <div id="tab4" style="float:left; ">

    
        <asp:Table runat="server" Width="850px" >
            <asp:TableRow>
                <asp:TableCell HorizontalAlign="Right">
                    <asp:Label ID="Label3" runat="server" Text="Registros por página" Font-Names="Trebuchet MS" Font-Size="Smaller" Font-Bold="true" />
                </asp:TableCell><asp:TableCell HorizontalAlign="center"  ColumnSpan ="2">
                    <asp:Label ID="Label9" runat="server" Text="Exportar productos" Font-Names="Trebuchet MS" Font-Size="Smaller" Font-Bold="true" />
                </asp:TableCell></asp:TableRow><asp:TableRow>
               <asp:TableCell HorizontalAlign="Right">
                    <asp:DropDownList ID="ddlPageSize" runat="server" AutoPostBack="True" Font-Names="Arial" Font-Size="10pt" OnSelectedIndexChanged="ddlPageSize_SelectedIndexChanged">
                        <asp:ListItem Value="-1" >Ilimitado</asp:ListItem>
                        <asp:ListItem Value="5">5</asp:ListItem>
                        <asp:ListItem Value="15" Selected="True">15</asp:ListItem>
                        <asp:ListItem Value="25">25</asp:ListItem>
                        <asp:ListItem Value="50">50</asp:ListItem>
                        <asp:ListItem Value="100">100</asp:ListItem>
                    </asp:DropDownList>
                </asp:TableCell><asp:TableCell>
                <asp:ImageButton ID="imgBtnExportxls" runat="server" ImageUrl="images/excel.png" 
                    ToolTip="Exportar productos participantes a excel" Width="45px" onclick="imgBtnExportxls_Click" OnClientClick="mostrar_procesar();" style="position:relative; left:180px;" />
                </asp:TableCell></asp:TableRow></asp:Table>

     <dxwgv:ASPxGridView ID="grdProducts" runat="server" AutoGenerateColumns="False"  OnRowDataBound="grdProducts_RowDataBound" OnPageIndexChanging="grdProducts_PageIndexChanging" OnRowEditing="grdProducts_RowEditing"
        OnRowUpdating="grdProducts_RowUpdating" KeyFieldName="ProductId;PharmaFormId;DivisionId;CategoryId" DataKeyNames="ProductId,PharmaFormId,DivisionId,CategoryId" OnHtmlRowCreated="aspGrdProducts_HtmlRowCreated" ClientInstanceName="grdProducts">
        <Columns>
           <dxwgv:GridViewCommandColumn Caption="New" VisibleIndex="1">
                                       
                    <EditButton Visible="true" />
                </dxwgv:GridViewCommandColumn>
            <dxwgv:GridViewDataColumn FieldName="ProductId" Visible="False" />
            <dxwgv:GridViewDataColumn FieldName="PharmaFormId" Visible="False" />
           <dxwgv:GridViewDataColumn FieldName="DivisionId" Visible="False" />
            <dxwgv:GridViewDataColumn FieldName="CategoryId" Visible="False" />
            <dxwgv:GridViewDataColumn FieldName="Nombre">
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" BackColor="#2694B4" ForeColor="White" />
                <CellStyle HorizontalAlign="Center" VerticalAlign="Middle"  />
                <DataItemTemplate>
                    <asp:Label ID="lblProdName" runat="server">
                        <%# DataBinder.Eval(Container.DataItem, "Brand") %>
                    </asp:Label></DataItemTemplate><EditItemTemplate>
                    <asp:TextBox ID="txtBrand" runat="server" CssClass="label" Text='<%# displayBrand((string)DataBinder.Eval(Container.DataItem, "Brand")) %>'
                        Width="150px" />
                </EditItemTemplate>
            </dxwgv:GridViewDataColumn>
            <dxwgv:GridViewDataColumn FieldName="Descripcion">
               <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" BackColor="#2694B4" ForeColor="White" />
                <CellStyle HorizontalAlign="Center" VerticalAlign="Middle"  />
                <DataItemTemplate>
                    <asp:Label ID="lblProdDescription" runat="server">
                        <%# DataBinder.Eval(Container.DataItem, "ProductDescription") %>
                    </asp:Label></DataItemTemplate><EditItemTemplate>
                    <asp:TextBox ID="txtDescription" runat="server" CssClass="label" Text='<%# displayBrand((string)DataBinder.Eval(Container.DataItem, "ProductDescription")) %>'
                        Width="150px" />
                </EditItemTemplate>
            </dxwgv:GridViewDataColumn>
            <dxwgv:GridViewDataColumn FieldName="Forma Farmacéutica" EditFormSettings-Visible="false" ReadOnly="true">
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" BackColor="#2694B4" ForeColor="White" />
                <CellStyle HorizontalAlign="Center" VerticalAlign="Middle"  />
                <DataItemTemplate>
                    <asp:Label ID="lblPharmaForm" runat="server">
                        <%# DataBinder.Eval(Container.DataItem, "PharmaForm")%>
                    </asp:Label></DataItemTemplate></dxwgv:GridViewDataColumn>
            <dxwgv:GridViewDataColumn FieldName="Codigo de Registro">
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" BackColor="#2694B4" ForeColor="White" />
                <CellStyle HorizontalAlign="Center" VerticalAlign="Middle"  />
                        <DataItemTemplate>
                    <asp:Label ID="lblRegister" runat="server">
                        <%# DataBinder.Eval(Container.DataItem, "Register") %>
                    </asp:Label></DataItemTemplate><EditItemTemplate>
                    <asp:TextBox ID="txtRegister" runat="server" CssClass="label" Text='<%# displayBrand((string)DataBinder.Eval(Container.DataItem, "Register")) %>'
                        Width="150px" />
                </EditItemTemplate>


                </dxwgv:GridViewDataColumn><dxwgv:GridViewDataColumn FieldName="Ediciones" EditFormSettings-Visible="false">
                 <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" BackColor="#2694B4" ForeColor="White" />
                <CellStyle HorizontalAlign="Center" VerticalAlign="Middle"  />
                <DataItemTemplate>
                    <asp:Label ID="lblEditions" runat="server">
                        <%# DataBinder.Eval(Container.DataItem, "Editions")%>
                    </asp:Label></DataItemTemplate></dxwgv:GridViewDataColumn>
            <dxwgv:GridViewDataColumn FieldName="Participante" EditFormSettings-Visible="false">
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" BackColor="#2694B4" ForeColor="White" />
                <CellStyle HorizontalAlign="Center" VerticalAlign="Middle"  />
                <DataItemTemplate>
                    <asp:CheckBox ID="Participate" runat="server" AutoPostBack="false" Enabled="false" ClientIDMode="Static" />
                </DataItemTemplate>
            </dxwgv:GridViewDataColumn>
            <dxwgv:GridViewDataColumn FieldName="Mencionado" EditFormSettings-Visible="false">
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" BackColor="#2694B4" ForeColor="White" />
                <CellStyle HorizontalAlign="Center" VerticalAlign="Middle"  />
                <DataItemTemplate>
                    <asp:CheckBox ID="Mentionated" runat="server" AutoPostBack="false" Enabled="false" />
                </DataItemTemplate>
            </dxwgv:GridViewDataColumn>
            <dxwgv:GridViewDataColumn FieldName="Nuevo" EditFormSettings-Visible="false">
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" BackColor="#2694B4" ForeColor="White" />
                <CellStyle HorizontalAlign="Center" VerticalAlign="Middle"  />
                <DataItemTemplate>
                    <asp:CheckBox ID="NewProduct" runat="server" AutoPostBack="false" Enabled="false" />
                </DataItemTemplate>
            </dxwgv:GridViewDataColumn>
            <dxwgv:GridViewDataColumn FieldName="Con Cambios" EditFormSettings-Visible="false">
               <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" BackColor="#2694B4" ForeColor="White" />
                <CellStyle HorizontalAlign="Center" VerticalAlign="Middle"  />
                <DataItemTemplate>
                    <asp:CheckBox ID="ModifiedContent" runat="server" AutoPostBack="false"  Enabled="false" />
                </DataItemTemplate>
            </dxwgv:GridViewDataColumn>
                 
            <dxwgv:GridViewDataColumn FieldName="Editar">
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" BackColor="#2694B4" ForeColor="White" />
                <CellStyle HorizontalAlign="Center" VerticalAlign="Middle"  />
                <DataItemTemplate>
                    <asp:ImageButton ID="btnEdit" runat="server" CommandName="Edit" ImageUrl="~/Images/editar.gif" AlternateText="Editar producto" Enabled="true" />
                </DataItemTemplate>
                <EditItemTemplate >
                    <asp:ImageButton ID="btnSave" runat="server" CommandName="Update" ImageUrl="~/Images/salvar.jpg" AlternateText="Guardar producto" ValidationGroup="ProdInfo" />
                </EditItemTemplate>
            </dxwgv:GridViewDataColumn>
        </Columns>
          <SettingsEditing Mode="Inline" />
    </dxwgv:ASPxGridView>


    <br />
    
    <asp:Button ID="btnSaveParticipant" runat="server" ToolTip="Guardar" Text="Guardar" OnClick="btnSaveParticipant_Click" />

   
                    </div>
</div>
    </div>
     <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
</asp:Content>