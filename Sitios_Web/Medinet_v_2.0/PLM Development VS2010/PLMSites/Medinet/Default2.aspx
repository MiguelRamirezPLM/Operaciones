<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/medinet.master" CodeFile="Default2.aspx.cs" Inherits="Default2" %>

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

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">

 
  <script type="text/javascript" src="//code.jquery.com/jquery-1.10.2.js"></script>
  <script type="text/javascript" src="//code.jquery.com/ui/1.11.0/jquery-ui.js"></script>


    <script language="javascript" type="text/javascript">

       

        var rowKey = -1;
        var rowValue = "";
        var otherGrid;
        function OnControlsInitialized(s, e) {
            var rowKey = -1; $('.draggableRow').draggable({ //http://api.jqueryui.com/draggable/
                helper: 'clone',
                start: function (ev, ui) {
                    var $sourceElement = $(ui.helper.context);
                    var $draggingElement = $(ui.helper);
                    var sourceGrid = ASPxClientGridView.Cast($draggingElement.hasClass("left") ? "gridFrom" : "gridTo");
                    otherGrid = ASPxClientGridView.Cast("gridFrom");
                    //style elements
                    $sourceElement.addClass("draggingStyle");
                    $draggingElement.addClass("draggingStyle");

                    //find key
                    rowKey = sourceGrid.GetRowKey($sourceElement.index() - 1);
                    //rowValue = otherGrid.GetRowValues(1, 'CategoryID');
                    $draggingElement.width(sourceGrid.GetWidth());
                    //alert(rowValue);
                },
                stop: function (e, ui) {
                    alert("ssddf");
                    rowValue = otherGrid.GetRowValues(1, 'AgrochemicalUseId');
                    alert(rowValue);
                    $(".draggingStyle").removeClass("draggingStyle");
                }
            });

            var settings = function (className) {
                return {
                    tolerance: "intersect",
                    accept: className,
                    drop: function (ev, ui) {
                        $(".targetGrid").removeClass("targetGrid");
                        var leftToRight = ui.helper.hasClass("left");
                        cbPanel.PerformCallback(rowKey + "|" + leftToRight);
                    },
                    over: function (ev, ui) {
                        $(this).addClass("targetGrid");
                    },
                    out: function (ev, ui) {
                        $(".targetGrid").removeClass("targetGrid");
                    }
                };
            };

            //http://api.jqueryui.com/droppable/
            $(".droppableLeft").droppable(settings(".right"));
            $(".droppableRight").droppable(settings(".left"));

        }
    
    </script>



   <dx:ASPxCallbackPanel ID="ASPxCallbackPanel3" runat="server" ClientInstanceName="cbPanel" OnCallback="cbPanel_Callback">
                
                      
                        <PanelCollection>
                            <dxp:PanelContent runat="server">
                               
                                                       
                                <dxwgv:ASPxGridView ID="ASPxGridView6" EnableCallBacks="false" ClientInstanceName="gridFrom"
                                        EnableCallbackCompression="true" DataSourceID="AccessDataSource3" EnableRowsCache="true" Width="100px"
                                        runat="server" KeyFieldName="CategoryID" AutoGenerateColumns="False" style="float:left;">
                                    <SettingsBehavior SortMode="Custom" />
                    <Settings ShowFilterRow="True" ShowFilterRowMenu="true" />
                    
                                        <Columns>
                                            <dxwgv:GridViewDataColumn FieldName="CategoryName" Caption="ClassificationName">
                                                <Settings AutoFilterCondition="Contains" />
                                            </dxwgv:GridViewDataColumn>
                                            
                                        </Columns>
                                         <Styles>
                                             <Header BackColor="#2694B4" Font-Names="Verdana" Font-Size="12px" Font-Bold="true" ForeColor="White" HorizontalAlign="Left" />
                            <AlternatingRow Enabled="True" />
                            <GroupRow HorizontalAlign="Left" Font-Bold="true" />
                            <PagerBottomPanel Font-Names="Verdana" Font-Size="10px" ForeColor="#7cbdd0" />
                            <PagerTopPanel Font-Names="Verdana" Font-Size="10px" ForeColor="#7cbdd0" />
                                            <Table CssClass="droppableRight"></Table>
                                            <Row CssClass="draggableRow right"></Row>
                                        </Styles>
                                </dxwgv:ASPxGridView>
                                <dxwgv:ASPxGridView ID="ASPxGridView7" ClientInstanceName="gridTo"
                                    Width="20%" runat="server" KeyFieldName="ID" AutoGenerateColumns="False" OnRowInserting="ASPxGridView1_RowInserting" style="float:left; ">
                                    <SettingsBehavior SortMode="Custom" />
                    <Settings ShowFilterRow="True" ShowFilterRowMenu="true" />
                                    <Columns>
                                       
                                        <dxwgv:GridViewDataColumn FieldName="Producto" VisibleIndex="1" Settings-AllowAutoFilter="False">
                                            <EditFormSettings Visible="True" />
                                        </dxwgv:GridViewDataColumn>
                                        <dxwgv:GridViewDataColumn FieldName="ClassificationName" VisibleIndex="2">
                                        </dxwgv:GridViewDataColumn>
                                    </Columns>
                             <Styles>
                                  <Header BackColor="#2694B4" Font-Names="Verdana" Font-Size="12px" Font-Bold="true" ForeColor="White" HorizontalAlign="Left" />
                            <AlternatingRow Enabled="True" />
                            <GroupRow HorizontalAlign="Left" Font-Bold="true" />
                            <PagerBottomPanel Font-Names="Verdana" Font-Size="10px" ForeColor="#7cbdd0" />
                            <PagerTopPanel Font-Names="Verdana" Font-Size="10px" ForeColor="#7cbdd0" />
                                            <Table CssClass="droppableLeft"></Table>
                                            <Row CssClass="draggableRow left"></Row>
                             </Styles>
                                  
                                </dxwgv:ASPxGridView>
                                    <asp:AccessDataSource ID="AccessDataSource3" runat="server" DataFile="~/Nwind.mdb" SelectCommand="SELECT [CategoryID], [CategoryName], [Description] FROM [Categories]">
                                    </asp:AccessDataSource>
                                    <asp:XmlDataSource ID="XmlDataSource4" runat="server" DataFile="~/XMLFile.xml"></asp:XmlDataSource>
                                
                            </dxp:PanelContent>
                        </PanelCollection>
                    </dx:ASPxCallbackPanel>
                   
                    <dx:ASPxGlobalEvents ID="ge" runat="server">
                <ClientSideEvents ControlsInitialized="OnControlsInitialized" EndCallback="OnControlsInitialized" />
            </dx:ASPxGlobalEvents>
<%--                </div>
            </div>
        </div>--%>


</asp:Content>




