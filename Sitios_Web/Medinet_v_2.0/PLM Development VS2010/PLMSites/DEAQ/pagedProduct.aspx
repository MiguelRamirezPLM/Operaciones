<%@ Page Language="C#" MasterPageFile="~/deaq.master" AutoEventWireup="true" CodeFile="pagedProduct.aspx.cs" Inherits="pagedProduct" %>
<%@ MasterType VirtualPath="~/deaq.master" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v10.1, Version=10.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxEditors.v10.1, Version=10.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>

<%@ Register assembly="DevExpress.XtraReports.v10.1.Web, Version=10.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.XtraReports.Web" tagprefix="dx" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">

    <asp:Label ID="Label1" runat="server" Font-Names="Trebuchet MS" Font-Size="X-Large" Font-Bold="true" Text="Paginación de productos" />
    <br />
    <br />

    <asp:Table ID="tblOptions" runat="server">
        <asp:TableRow BorderWidth="1px" Font-Names="Trebuchet MS" Font-Size="Smaller" Font-Bold="true">
            <asp:TableCell BorderWidth="1px" Width="220px" HorizontalAlign="Left">
                <asp:Label ID="Label2" runat="server" Text="Regresar..." />
            </asp:TableCell><asp:TableCell BorderWidth="1px" Width="220px" HorizontalAlign="Left">
                <asp:Label ID="lblChooseOther" runat="server" Text="Elegir letra del alfabeto: " />
            </asp:TableCell><asp:TableCell BorderWidth="1px" Width="220px" HorizontalAlign="Right">
                <asp:Label ID="lblQtyRecords" runat="server" Text="Registros por página" />
            </asp:TableCell></asp:TableRow><asp:TableRow BorderWidth="1px">
            <asp:TableCell BorderWidth="1px" Width="220px" HorizontalAlign="Left">
                <asp:ImageButton ID="imgBtnBackLabs" runat="server" ImageUrl="images/regresar.png" ToolTip="Elegir otro laboratorio" OnClick="imgBtnBackLabs_Click" />
            </asp:TableCell><asp:TableCell BorderWidth="1px" Width="220px" HorizontalAlign="Left">
                <asp:DropDownList ID="ddlAlphabet" runat="server" AutoPostBack="True" Font-Names="Arial" Font-Size="10pt" OnSelectedIndexChanged="ddlAlphabet_SelectedIndexChanged">
                    <asp:ListItem Value="-1" Selected="True" >Seleccionar letra</asp:ListItem>
                    <asp:ListItem Value="A">A</asp:ListItem>
                    <asp:ListItem Value="B">B</asp:ListItem>
                    <asp:ListItem Value="C">C</asp:ListItem>
                    <asp:ListItem Value="D">D</asp:ListItem>
                    <asp:ListItem Value="E">E</asp:ListItem>
                    <asp:ListItem Value="F">F</asp:ListItem>
                    <asp:ListItem Value="G">G</asp:ListItem>
                    <asp:ListItem Value="H">H</asp:ListItem>
                    <asp:ListItem Value="I">I</asp:ListItem>
                    <asp:ListItem Value="J">J</asp:ListItem>
                    <asp:ListItem Value="K">K</asp:ListItem>
                    <asp:ListItem Value="L">L</asp:ListItem>
                    <asp:ListItem Value="M">M</asp:ListItem>
                    <asp:ListItem Value="N">N</asp:ListItem>
                    <asp:ListItem Value="O">O</asp:ListItem>
                    <asp:ListItem Value="P">P</asp:ListItem>
                    <asp:ListItem Value="Q">Q</asp:ListItem>
                    <asp:ListItem Value="R">R</asp:ListItem>
                    <asp:ListItem Value="S">S</asp:ListItem>
                    <asp:ListItem Value="T">T</asp:ListItem>
                    <asp:ListItem Value="U">U</asp:ListItem>
                    <asp:ListItem Value="V">V</asp:ListItem>
                    <asp:ListItem Value="W">W</asp:ListItem>
                    <asp:ListItem Value="X">X</asp:ListItem>
                    <asp:ListItem Value="Y">Y</asp:ListItem>
                    <asp:ListItem Value="Z">Z</asp:ListItem>
                </asp:DropDownList>
            </asp:TableCell><asp:TableCell BorderWidth="1px" Width="220px" HorizontalAlign="Right">
                <asp:DropDownList ID="ddlPageSize" runat="server" AutoPostBack="True" Font-Names="Arial" Font-Size="10pt" OnSelectedIndexChanged="ddlPageSize_SelectedIndexChanged">
                    <asp:ListItem Value="-1" >Ilimitado</asp:ListItem>
                    <asp:ListItem Value="5">5</asp:ListItem>
                    <asp:ListItem Value="15" Selected="True">15</asp:ListItem>
                    <asp:ListItem Value="25">25</asp:ListItem>
                    <asp:ListItem Value="50">50</asp:ListItem>
                    <asp:ListItem Value="100">100</asp:ListItem>
                </asp:DropDownList>
            </asp:TableCell></asp:TableRow></asp:Table><br />
    <asp:Button 
        ID="BtnSaveTop" runat="server" Text="Guardar" /><br /><br />

    <asp:GridView ID="grdProducts" runat="server" AutoGenerateColumns="False" 
        BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3"
        Font-Names="Trebuchet MS" Font-Size="Smaller" 
        DataKeyNames="ProductId,PharmaFormId,DivisionId,CategoryId" OnRowDataBound="grdProducts_RowDataBound"
        OnPageIndexChanging="grdProducts_PageIndexChanging" Visible="False" ><Columns>
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
                    </asp:Label></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="Tipo de producto">
                <HeaderStyle HorizontalAlign="Left" BackColor="#2694B4" ForeColor="White" />
                <ItemStyle  Width="100px" />
                    <ItemTemplate>
                        <asp:Label ID="lblTipo" runat="server">
                        <%# DataBinder.Eval(Container.DataItem, "ProductType")%>
                    </asp:Label></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="Página">
                <HeaderStyle HorizontalAlign="Left" BackColor="#2694B4" ForeColor="White" />
                <ItemStyle  Width="100px" />
                    <ItemTemplate>
                        <asp:TextBox runat="server" ID="txtPage" Width="81px" />
                    </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
  
     <dx:ASPxGridView ID="aspGrdProducts" runat="server" 
        ClientInstanceName="aspGrdProducts"   
        KeyFieldName="ProductId,PharmaFormId,DivisionId,CategoryId" 
        AutoGenerateColumns="False" ClientIDMode="AutoID" 
         EnableRowsCache="False" 
        onhtmlrowcreated="aspGrdProducts_HtmlRowCreated" 
         ><SettingsBehavior SortMode="Custom" />
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
            <dx:GridViewDataColumn FieldName="DivisionId" Visible="False"></dx:GridViewDataColumn>
            <dx:GridViewDataColumn FieldName="CategoryId" Visible="False"></dx:GridViewDataColumn>
            <dx:GridViewDataColumn FieldName="Page"  Visible="False"></dx:GridViewDataColumn>
            
            <dx:GridViewDataColumn FieldName="Brand" Caption="Nombre" VisibleIndex="0">
                <CellStyle CssClass="label" HorizontalAlign="Left" />
            </dx:GridViewDataColumn>
            
            <dx:GridViewDataColumn FieldName="PharmaForm" Caption="Forma Farmacéutica" VisibleIndex="1">
                <CellStyle CssClass="label" HorizontalAlign="Left" />
            </dx:GridViewDataColumn>

            <dx:GridViewDataColumn FieldName="DivisionShortName" Caption="Laboratorio" VisibleIndex="2">
                <CellStyle CssClass="label" HorizontalAlign="Left" />
            </dx:GridViewDataColumn>

            <dx:GridViewDataColumn FieldName="ProductType" Caption="Tipo de producto" VisibleIndex="3" >
                <CellStyle CssClass="label" HorizontalAlign="Left" />
            </dx:GridViewDataColumn>
            <dx:GridViewDataColumn Caption="Síntomas" VisibleIndex="3" ReadOnly="true">
            <Settings AllowDragDrop="False"  />
            <EditFormSettings  Visible="False"/>
            <DataItemTemplate>
                <asp:GridView ID="grdSymptoms" runat="server" AutoGenerateColumns="false">
                    <Columns>
            <asp:TemplateField HeaderText="Nombre">
                <HeaderStyle BackColor="#2694B4" ForeColor="White" HorizontalAlign="Center"/>
                <ItemStyle  Width="150px" HorizontalAlign="Center" />
                <ItemTemplate>
                    <asp:Label ID="lblEditions" runat="server">
                        <%# DataBinder.Eval(Container.DataItem, "SymptomName") %>
                    </asp:Label></ItemTemplate></asp:TemplateField>
                    </Columns></asp:GridView>
            </DataItemTemplate>
            </dx:GridViewDataColumn>

            
            
           
          <dx:GridViewDataColumn FieldName="Page" Caption="Página" 
                VisibleIndex="4">
                <DataItemTemplate>
            <asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Page") %>' ID="txtPage" Width="81px" />
            </DataItemTemplate>
            </dx:GridViewDataColumn></Columns>

        <SettingsPager PageSize="50" Position="TopAndBottom"></SettingsPager>

    </dx:ASPxGridView>
    
    <br />

    <asp:Button ID="btnSaveProducts" runat="server" Text="Guardar" OnClick="btnSaveProducts_OnClick" />

</asp:Content>