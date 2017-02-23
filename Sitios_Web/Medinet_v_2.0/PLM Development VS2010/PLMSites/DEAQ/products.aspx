<%@ Page Language="C#" MasterPageFile="~/deaq.master" AutoEventWireup="true" CodeFile="products.aspx.cs" Inherits="products" %>
<%@ MasterType VirtualPath="~/deaq.master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
<script type="text/javascript">

    function checkMutuallyExclusive(target1, target2) {
        var participate, mentionated,participateid,mentionatedid,newp,newpid;
        
        document.getElementById(target1).checked = false;
        participateid = "ContentPlaceHolder1_grdProducts_Participate_" + target1.substring(target1.lastIndexOf("_") + 1);
        mentionatedid = "ContentPlaceHolder1_grdProducts_Mentionated_" + target1.substring(target1.lastIndexOf("_") + 1);
        newpid = "ContentPlaceHolder1_grdProducts_NewProduct_" + target1.substring(target1.lastIndexOf("_") + 1);
        participate = document.getElementById(participateid);
        mentionated = document.getElementById(mentionatedid);
        newp = document.getElementById(newpid);
        if (target2 != null) {
           // alert(target1);
            if (participate.checked == false) {
                document.getElementById(target2).checked = false;
                document.getElementById(target2).disabled = true;
            }
            else if (participate.checked == true && newp.checked==false)
                document.getElementById(target2).disabled = false;

        }
    }
    function checkNotMutuallyExclusive(target1,value) {
        document.getElementById(target1).checked = value;
    }

    function openReport(url) {
        window.open(url);
    }
    //$(document).ready(function () {
    //    $("#btnEdit").click(function () {
    //        var row = $(this).closest("tr");
    //        //Getting the ID of the record and the data from textbox to update
    //        var pID = $(this).attr("id");
    //        var update_data = $(".stock", row).val(); //stock is the CssClass name of the textbox.
    //        // If you are updating  more than one column, retrieve the value of all the
    //        //textboxes similarly and then add them in single variable
    //        //(see below)
    //        update_data = pID + "," + update_data; //adding both the value in single variable
    //        alert(update_data);
    //        //with ',' separator                                
    //        // Confirming the operation from the user
    //        if (confirm("Do you want to update this record?")) {
    //            $.ajax({
    //                type: "POST",
    //                //UpdateRecordInGridViewUsingJQuery.aspx is the page name and UpdateProduct 
    //                // is the server side web method which actually does the updation
    //                url: "UpdateRecordInGridViewUsingJQuery.aspx/UpdateProduct",
    //                //Passing the record id and data to be updated which is in the variable record_id
    //                data: "{'args': '" + update_data + "'}",
    //                contentType: "application/json; charset=utf-8",
    //                dataType: "json",
    //                //Giving message to user on successful updation
    //                success: function () {
    //                    alert("Record successfully updated!!!");
    //                    //location.reload();
    //                }
    //            });
    //        }
    //        return false;
    //    });
    //});

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

    <asp:Table runat="server" >
        <asp:TableRow BorderWidth="1px" Font-Names="Trebuchet MS" Font-Size="Smaller" Font-Bold="true">
            <asp:TableCell>
                <asp:Label ID="lblTitle" runat="server" Text="Ingresar producto nuevo" />
            </asp:TableCell></asp:TableRow><asp:TableRow BorderWidth="1px" Font-Names="Trebuchet MS" Font-Size="Smaller" Font-Bold="true">
            <asp:TableCell BorderWidth="1px">
                <asp:Label ID="lblBrandToAdd" runat="server" Text="Nombre" />
            </asp:TableCell>
                <asp:TableCell BorderWidth="1px">
                <asp:Label ID="Label2" runat="server" Text="Descripción" />
            </asp:TableCell>
                <asp:TableCell BorderWidth="1px">
                <asp:Label ID="Label10" runat="server" Text="Código de Registro" />
            </asp:TableCell><asp:TableCell BorderWidth="1px">
                <asp:Label ID="lblPharmaFormToAdd" runat="server" Text="Forma Farmacéuitca" />
            </asp:TableCell><asp:TableCell BorderWidth="1px">
                <asp:Label ID="lblGuardar" runat="server" Text="Agregar producto" />
            </asp:TableCell></asp:TableRow><asp:TableRow BorderWidth="1px">
            <asp:TableCell BorderWidth="1px">
                <asp:TextBox ID="txtBrandToAdd" runat="server" Text="" onkeydown="return (event.keyCode!=13);" />
            </asp:TableCell>
                <asp:TableCell BorderWidth="1px">
                <asp:TextBox ID="txtDescriptionToAdd" runat="server" Text="" onkeydown="return (event.keyCode!=13);"/>
            </asp:TableCell>
                <asp:TableCell BorderWidth="1px">
                <asp:TextBox ID="txtRegisterToAdd" runat="server" Text="" onkeydown="return (event.keyCode!=13);"/>
            </asp:TableCell><asp:TableCell BorderWidth="1px">
                <asp:DropDownList ID="ddlPharmaFormToAdd" runat="server">
                </asp:DropDownList>
            </asp:TableCell><asp:TableCell BorderWidth="1px">
                <asp:Button ID="btnAddProduct" runat="server" Text="Agregar producto" OnClick="btnAddProduct_Click" />
            </asp:TableCell></asp:TableRow></asp:Table><br /><br />

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

    

    <asp:Panel ID="pnlSearch" runat="server" DefaultButton="btnConsult">
        <asp:Table runat="server" Width="850px">
            <asp:TableRow>
                <asp:TableCell HorizontalAlign="Left">
                    <asp:Label ID="Label7" runat="server" Text="Buscar un producto" Font-Names="Trebuchet MS" Font-Size="Smaller" Font-Bold="true" />
                </asp:TableCell>
                <asp:TableCell HorizontalAlign="Right">
                    <asp:Label ID="Label3" runat="server" Text="Registros por página" Font-Names="Trebuchet MS" Font-Size="Smaller" Font-Bold="true" />
                </asp:TableCell><asp:TableCell HorizontalAlign="center"  ColumnSpan ="2">
                    <asp:Label ID="Label9" runat="server" Text="Exportar productos" Font-Names="Trebuchet MS" Font-Size="Smaller" Font-Bold="true" />
                </asp:TableCell></asp:TableRow><asp:TableRow>
                      <asp:TableCell HorizontalAlign="Left">
                    <asp:TextBox ID="txtProductName" runat="server" Text="" Enabled="true"  />
                    <asp:Button ID="btnConsult" runat="server" ToolTip="Consultar" Text="Consultar" OnClick="btnConsult_Click" />
                </asp:TableCell>
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
                </asp:TableCell></asp:TableRow></asp:Table></asp:Panel><bqdwr />


    <asp:UpdatePanel ID="up" runat="server" UpdateMode="Always" ChildrenAsTriggers="true">
                    <ContentTemplate>
    <asp:GridView ID="grdProducts" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3"
        Font-Names="Trebuchet MS" Font-Size="Smaller" OnRowDataBound="grdProducts_RowDataBound" OnPageIndexChanging="grdProducts_PageIndexChanging" OnRowEditing="grdProducts_RowEditing"
        OnRowUpdating="grdProducts_RowUpdating" DataKeyNames="ProductId,PharmaFormId,DivisionId,CategoryId">
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
                    </asp:Label></ItemTemplate><EditItemTemplate>
                    <asp:TextBox ID="txtBrand" runat="server" CssClass="stock" Text='<%# displayBrand((string)DataBinder.Eval(Container.DataItem, "Brand")) %>'
                        Width="150px" />
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Descripción">
                <HeaderStyle HorizontalAlign="Left" BackColor="#2694B4" ForeColor="White"/>
                <ItemStyle  Width="100px" />
                <ItemTemplate>
                    <asp:Label ID="lblProdDescription" runat="server">
                        <%# DataBinder.Eval(Container.DataItem, "ProductDescription") %>
                    </asp:Label></ItemTemplate><EditItemTemplate>
                    <asp:TextBox ID="txtDescription" runat="server" CssClass="label" Text='<%# displayBrand((string)DataBinder.Eval(Container.DataItem, "ProductDescription")) %>'
                        Width="150px" />
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Forma Farmacéutica">
                <HeaderStyle HorizontalAlign="Left" BackColor="#2694B4" ForeColor="White"/>
                <ItemStyle  Width="100px" />
                <ItemTemplate>
                    <asp:Label ID="lblPharmaForm" runat="server">
                        <%# DataBinder.Eval(Container.DataItem, "PharmaForm")%>
                    </asp:Label></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="Código de Registro">
                <HeaderStyle HorizontalAlign="Left" BackColor="#2694B4" ForeColor="White" />
                <ItemStyle  Width="100px" />
                        <ItemTemplate>
                    <asp:Label ID="lblRegister" runat="server">
                        <%# DataBinder.Eval(Container.DataItem, "Register") %>
                    </asp:Label></ItemTemplate><EditItemTemplate>
                    <asp:TextBox ID="txtRegister" runat="server" CssClass="label" Text='<%# displayBrand((string)DataBinder.Eval(Container.DataItem, "Register")) %>'
                        Width="150px" />
                </EditItemTemplate>


                </asp:TemplateField><asp:TemplateField HeaderText="Ediciones">
                <HeaderStyle HorizontalAlign="Left" BackColor="#2694B4" ForeColor="White" />
                <ItemStyle  Width="100px" />
                <ItemTemplate>
                    <asp:Label ID="lblEditions" runat="server">
                        <%# DataBinder.Eval(Container.DataItem, "Editions")%>
                    </asp:Label></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="Participante">
                <HeaderStyle Width="30px" HorizontalAlign="Center" BackColor="#2694B4" ForeColor="White" />
                <ItemStyle HorizontalAlign="Center" Wrap="True" Width="50px" />
                <ItemTemplate>
                    <asp:CheckBox ID="Participate" runat="server" AutoPostBack="false" Enabled="false" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Mencionado">
                <HeaderStyle Width="30px" HorizontalAlign="Center" BackColor="#2694B4" ForeColor="White" />
                <ItemStyle HorizontalAlign="Center" Wrap="True" Width="50px" />
                <ItemTemplate>
                    <asp:CheckBox ID="Mentionated" runat="server" AutoPostBack="false" Enabled="false" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Nuevo">
                <HeaderStyle Width="30px" HorizontalAlign="Center" BackColor="#2694B4" ForeColor="White"/>
                <ItemStyle HorizontalAlign="Center" Wrap="True" Width="50px" />
                <ItemTemplate>
                    <asp:CheckBox ID="NewProduct" runat="server" AutoPostBack="false" Enabled="false" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Con Cambios" >
                <HeaderStyle Width="30px" HorizontalAlign="Center" BackColor="#2694B4" ForeColor="White"/>
                <ItemStyle HorizontalAlign="Center" Wrap="True" Width="50px" />
                <ItemTemplate>
                    <asp:CheckBox ID="ModifiedContent" runat="server" AutoPostBack="false"  Enabled="false" />
                </ItemTemplate>
            </asp:TemplateField>
                 
            <asp:TemplateField HeaderText="Editar">
                <HeaderStyle Width="45px" HorizontalAlign="Center" BackColor="#2694B4" ForeColor="White"/>
                <ItemStyle HorizontalAlign="Center" Wrap="True" />
               

                <ItemTemplate>
                    
                    <asp:ImageButton ID="btnEdit" ClientIDMode="Static" runat="server" CommandName="Edit" ImageUrl="~/Images/editar.gif" AlternateText="Editar producto" Enabled="true" />
                        
                </ItemTemplate>
                <EditItemTemplate >
                    <asp:ImageButton ID="btnSave" runat="server" CommandName="Update" ImageUrl="~/Images/salvar.jpg" AlternateText="Guardar producto" ValidationGroup="ProdInfo"  />
                </EditItemTemplate>
                        
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
                      </ContentTemplate>
                    </asp:UpdatePanel>

    <br />
    
    <asp:Button ID="btnSaveParticipant" runat="server" ToolTip="Guardar" Text="Guardar" OnClick="btnSaveParticipant_Click" />

    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />

</asp:Content>