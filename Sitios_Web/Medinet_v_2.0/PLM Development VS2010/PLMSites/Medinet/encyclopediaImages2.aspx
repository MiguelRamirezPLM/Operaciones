<%@ Page Language="C#" MasterPageFile="~/medinet.master" AutoEventWireup="true" CodeFile="encyclopediaImages2.aspx.cs" Inherits="encyclopediaImages2" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v10.1, Version=10.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ MasterType VirtualPath="~/medinet.master" %>
<%@ Register assembly="DevExpress.Web.ASPxEditors.v10.1, Version=10.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxTreeList.v10.1, Version=10.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxTreeList" tagprefix="dx" %>
<%@ Register assembly="DevExpress.XtraReports.v10.1.Web, Version=10.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.XtraReports.Web" tagprefix="dx" %>
   
  
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">

    <script type="text/javascript">
        var index;
        var arch;

        function showPopUpReplace(button) {
            //alert("hh");
            document.getElementById("askR").click();

        }
        function showPopUpSizes(button) {
            //alert("hh");
            var hh;

            document.getElementById("askS").click();

        }
        function upImage(but) {
            // $("#lblSize1").val = "hhh;"
            var d = but.id;
            var tam = d.substring(1);
            //alert(tam);
            //alert(but.parentNode.parentNode.parentNode.getElementById("b125X390"));
            var id = '#b' + tam;
            // alert(id);
            var fin = $(id);
            // alert(fin);
            $(id).click();

        }


        this.imagePreview = function () {
            xOffset = 180;
            yOffset = 30;
            $(".preview").hover(function (e) {

                this.t = this.title;
                this.title = "";
                var c = (this.t != "") ? "<br/>" + this.t : "";

                $("body").append("<p id='preview' style='background-color:white;'><img src='" + this.src + "' alt='Image preview' />" + c + "</p>");

                $("#preview")
                    .css("top", (e.pageY - xOffset) + "px")
                    .css("left", (e.pageX + yOffset) + "px")
                    .css("z-index", "10")
                    .fadeIn("fast");
            },
            function () {
                this.title = this.t;
                $("#preview").remove();
            });
            $(".preview").mousemove(function (e) {
                $("#preview")
                    .css("top", (e.pageY - xOffset) + "px")
                    .css("left", (e.pageX + yOffset) + "px");
            });
        };


        // starting the script on page load
        $(document).ready(function () {
            imagePreview();
        });
        function upNewImage(but) {
            //alert("hhh");
            $('#Button1').click();
        }


    </script>
    <asp:ImageButton ID="imgBtnBackProducts" runat="server" ImageUrl="images/regresar.png" ToolTip="Regresar a productos" OnClick="imgBtnBackProducts_Click" />
    <br />
    <br />
    <br />
    <asp:Label ID="lblTitle" runat="server" Text="Agregar imagen a la enciclopedia" Font-Names="Trebuchet MS" Font-Size="Medium" Font-Bold="true" />
    <br />
 
    <div class="classname1"  >
            <asp:Label ID="Label1" runat="server" Text="Agregar Imagen" style="position:absolute; left:15px; color:white; font-weight:bold; "></asp:Label>
            <asp:FileUpload ID="File1" name="classname1" onchange="upNewImage(this);" runat="server"  />
    </div>
    <asp:Label ID="Label2" runat="server" Text="Selecciona la imagen de 400x400" Visible="true"></asp:Label>
    <asp:Button ID="Button1" runat="server" Text="Upload" onClick="addNewImage" ClientIDMode="Static" style="visibility:hidden;"/>
    <br />
    <br />
    <dx:ASPxGridView ID="grdPresentationCatalog" runat="server" AutoGenerateColumns="False" 
                    ClientInstanceName="grdPresentationCatalog"
                    OnRowDeleting="grdDeletingRow_click"
                    EnableRowsCache="false"         
                    onhtmlrowcreated="aspGrdProducts_HtmlRowCreated" 
                    KeyFieldName="EncyclopediaId;EncyclopediaTypeId"   >
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
                            <dx:GridViewDataColumn  FieldName="EncyclopediaId" Visible="False" VisibleIndex="-10"/>
                            <dx:GridViewDataColumn  FieldName="EncyclopediaTypeId" Visible="False" VisibleIndex="-9" />
                            <dx:GridViewDataColumn FieldName="ImageId" Visible="False"  VisibleIndex="-8"/>
                           
                            <dx:GridViewDataColumn FieldName="EncyclopediaName" Caption="Nombre" VisibleIndex="0" Settings-AllowDragDrop="True">
                                    <CellStyle CssClass="label" HorizontalAlign="Center" VerticalAlign="Middle" />
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataColumn FieldName="EncyclopediaType" Caption="Tipo" VisibleIndex="1">
                                    <CellStyle CssClass="label" HorizontalAlign="Center" VerticalAlign="Middle" />
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataColumn FieldName="ICD" Caption="ICD" VisibleIndex="2">
                                    <CellStyle CssClass="label" HorizontalAlign="Center" VerticalAlign="Middle" />
                            </dx:GridViewDataColumn>
                                     <dx:GridViewDataColumn Caption="Imágenes" VisibleIndex="4" Settings-AllowDragDrop="False">
                                    <CellStyle CssClass="label" HorizontalAlign="Left"  />
                                    <DataItemTemplate>
                                   
                                            <div style="width:100%; padding-left:30px;">                               
                                                    <asp:Repeater ID="RepeaterImages" runat="server" OnItemDataBound="rpImages_ItemBound" EnableViewState="true" ViewStateMode="Disabled" >
                                                           <ItemTemplate>
                                                                    <div id="repeaterItem" runat="server" style=" display:inline-block; position:relative; width:190px; height:105px; ">
                                                                        <div id="ful" runat="server" class="upload">
                                                                                    <asp:FileUpload ID="file"  runat="server" name="upload" onchange="upImage(this);" ClientIDMode="Static"/>
                                                                            </div>
                                                                            <asp:Image ID="Image" runat="server" ImageUrl='<%# DataBinder.Eval(Container.DataItem,"Image") %>' CssClass='<%# DataBinder.Eval(Container.DataItem,"class") %>' style="position:absolute; bottom:0; " />
                                                                            <asp:Label ID="lblImage" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Size") %>' style="position:absolute; bottom:-20px;  "  ></asp:Label>
                                                                            
                                                                         <asp:Button runat="server" ID="b" Text="f" OnClick="saveImage" ClientIDMode="Static" style="visibility:hidden"></asp:Button >
                                                                    </div>
                                                           </ItemTemplate>
                                                    </asp:Repeater>
                                            </div>
                 
                                            <br />
                                        <br />
                                            <dx:ASPxTextBox ID="lblSizes" runat="server" Text=' <%# DataBinder.Eval(Container.DataItem, "ImageSizes")%>' style="display:none;" Visible="false">
                                            </dx:ASPxTextBox>
                                            <dx:ASPxTextBox ID="txtLB" runat="server" Text=' <%# DataBinder.Eval(Container.DataItem, "Imagen")%>' Width="60px" style="display:none;" Visible="false">
                                            </dx:ASPxTextBox>
                                        
                                    </DataItemTemplate>
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataColumn Caption="Eliminar" VisibleIndex="5" Settings-AllowDragDrop="False">
                                <CellStyle CssClass="label" HorizontalAlign="Center" VerticalAlign="Middle" />
                                <DataItemTemplate>
                                        <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/Images/delete.gif" AlternateText="Eliminar" OnClick="askDelete" ClientIDMode="Static"/>
                                </DataItemTemplate>
                            </dx:GridViewDataColumn>
                    </Columns>
                    <SettingsPager PageSize="50" Position="TopAndBottom"></SettingsPager>
     </dx:ASPxGridView>
     <input id="Hidden1" type="hidden" runat="server" value="addImageSize"/>
</asp:Content>