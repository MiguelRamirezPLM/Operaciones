<%@ Page Title="" Language="C#" MasterPageFile="~/medinet.master" AutoEventWireup="true" CodeFile="PagedSymptoms.aspx.cs" Inherits="PagedSymptoms" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v10.1, Version=10.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxEditors.v10.1, Version=10.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>

<%@ Register assembly="DevExpress.XtraReports.v10.1.Web, Version=10.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.XtraReports.Web" tagprefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
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
                <asp:DropDownList ID="ddlPageSize" runat="server" AutoPostBack="True" Font-Names="Arial" Font-Size="10pt"  OnSelectedIndexChanged="ddlPageSize_SelectedIndexChanged">
                    <asp:ListItem Value="-1" >Ilimitado</asp:ListItem>
                    <asp:ListItem Value="5">5</asp:ListItem>
                    <asp:ListItem Value="15" Selected="True">15</asp:ListItem>
                    <asp:ListItem Value="25">25</asp:ListItem>
                    <asp:ListItem Value="50">50</asp:ListItem>
                    <asp:ListItem Value="100">100</asp:ListItem>
                </asp:DropDownList>
            </asp:TableCell></asp:TableRow></asp:Table><br />
    <asp:Button 
        ID="BtnSaveTop" runat="server" Text="Guardar"  OnClick="btnSavePages_OnClick"/><br /><br />
<dx:ASPxGridView ID="aspGrdSymptoms" runat="server" 
        ClientInstanceName="aspGrdSymptoms"   
        KeyFieldName="SymptomId" 
        AutoGenerateColumns="False" ClientIDMode="AutoID" 
        EnableRowsCache="False" 
        onhtmlrowcreated="aspGrdSymptoms_HtmlRowCreated"><SettingsBehavior SortMode="Custom" />
        <Settings ShowFilterRow="True" ShowFilterRowMenu="true" ShowGroupPanel="True" />

        <Styles>
            <Header BackColor="#2694B4" Font-Names="Verdana" Font-Size="12px" Font-Bold="true" ForeColor="White" HorizontalAlign="Left" />
            <AlternatingRow Enabled="True" />
            <GroupRow HorizontalAlign="Left" Font-Bold="true" />
            <PagerBottomPanel Font-Names="Verdana" Font-Size="10px" ForeColor="#7cbdd0" />
            <PagerTopPanel Font-Names="Verdana" Font-Size="10px" ForeColor="#7cbdd0" />
        </Styles>
        
        <Columns>
            
            <dx:GridViewDataColumn FieldName="SymptomId" Visible="False" ></dx:GridViewDataColumn>
            <dx:GridViewDataColumn FieldName="SymptomName" Caption="Sintoma" VisibleIndex="0">
                <CellStyle CssClass="label" HorizontalAlign="Left" />
            </dx:GridViewDataColumn>
            <dx:GridViewDataColumn FieldName="Page" Caption="Página" 
                VisibleIndex="1">
                <DataItemTemplate>
            <asp:TextBox runat="server" ID="txtPage" Width="81px" />
            </DataItemTemplate>
            </dx:GridViewDataColumn></Columns>

        <SettingsPager PageSize="50" Position="TopAndBottom"></SettingsPager>

    </dx:ASPxGridView><br />
    <asp:Button 
        ID="btnSaveDown" runat="server" Text="Guardar" />
</asp:Content>

