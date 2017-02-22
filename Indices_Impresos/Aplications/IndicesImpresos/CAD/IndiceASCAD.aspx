<%@ Page Language="C#" AutoEventWireup="true" CodeFile="IndiceASCAD.aspx.cs" Inherits="CAD_IndiceASCAD" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Página sin título</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Size="X-Large" Font-Strikeout="False"
            ForeColor="#0000C0" Text="ÍNDICE DE SUSTANCIAS ACTIVAS"></asp:Label><br />
        <br />
        <asp:Label ID="Label1" runat="server" Text="Selecciona la Letra:" CssClass="labelTitle"></asp:Label>
        <asp:DropDownList ID="ddlLetra" runat="server" Width="72px" OnSelectedIndexChanged="ddlLetra_SelectedIndexChanged" AutoPostBack="True">
            <asp:ListItem>A</asp:ListItem>
            <asp:ListItem>B</asp:ListItem>
            <asp:ListItem>C</asp:ListItem>
            <asp:ListItem>D</asp:ListItem>
            <asp:ListItem>E</asp:ListItem>
            <asp:ListItem>F</asp:ListItem>
            <asp:ListItem>G</asp:ListItem>
            <asp:ListItem>H</asp:ListItem>
            <asp:ListItem>I</asp:ListItem>
            <asp:ListItem>J</asp:ListItem>
            <asp:ListItem>K</asp:ListItem>
            <asp:ListItem>L</asp:ListItem>
            <asp:ListItem>M</asp:ListItem>
            <asp:ListItem>N</asp:ListItem>
            <asp:ListItem>O</asp:ListItem>
            <asp:ListItem>P</asp:ListItem>
            <asp:ListItem>Q</asp:ListItem>
            <asp:ListItem>R</asp:ListItem>
            <asp:ListItem>S</asp:ListItem>
            <asp:ListItem>T</asp:ListItem>
            <asp:ListItem>U</asp:ListItem>
            <asp:ListItem>V</asp:ListItem>
            <asp:ListItem>W</asp:ListItem>
            <asp:ListItem>X</asp:ListItem>
            <asp:ListItem>Y</asp:ListItem>
            <asp:ListItem>Z</asp:ListItem>
        </asp:DropDownList>
        <br />
        <br />
        <table>
        <tr>
        <td align="left" style="width:178%">
            <asp:Repeater runat="server" ID="rptLet" OnItemDataBound="rptLet_ItemDataBound">
            <ItemTemplate>
                <table>
                <tr><td style="width:600px">
                </td></tr>
                <tr><br />
                <td>
                   <%--<img alt="" src='\\mmedina001\MARCAS - GUIA 55\Logos\<%# DataBinder.Eval(Container.DataItem, "Nombre del Archivo") %>'style="border:0; "/> --%>
                </td></tr><br />
                <tr><td>   
                <asp:Repeater runat="server" ID="rptSubs" OnItemDataBound="rptSubs_ItemDataBound" >
                    <ItemTemplate>

                        <table>
                        <tr><td style="width:600px">
                         </td></tr><tr><br /><td><asp:Label runat="server" ID="lblASubs" CssClass="labelSubTitle"><%# DataBinder.Eval(Container.DataItem, "ACTIVESUBSTANCE")%></asp:Label></td></tr><br />
                         <tr><td>
                            <asp:Repeater runat="server" ID="rptProds" OnItemDataBound="rptProds_ItemDataBound" >
                                <ItemTemplate>                   
                                    <table>
                                    <tr>
                                    <td ><asp:Label runat="server" ID="lblProd" CssClass="label"><%# DataBinder.Eval(Container.DataItem, "Brand")%>.</asp:Label>
                                    <%--<asp:Label runat="server" ID="lblDesc" CssClass="label"><%# DataBinder.Eval(Container.DataItem, "Description")%>.</asp:Label>--%>
                                    <asp:Label runat="server" ID="lblActive" CssClass="label"><%# DataBinder.Eval(Container.DataItem, "ACTIVESUBSTANCES")%>.</asp:Label>
                                    <asp:Label runat="server" ID="lblPharma" CssClass="label"><%# DataBinder.Eval(Container.DataItem, "PHARMAFORMS")%>.</asp:Label>
                                    <asp:Label runat="server" ID="Label3" CssClass="label"><%# DataBinder.Eval(Container.DataItem, "LABORATORYNAME")%>.</asp:Label>                            
                                    </td><br />    
                                    </tr></table>
                                </ItemTemplate>
                            </asp:Repeater>
                            </td></tr></table>          
                    </ItemTemplate>
                </asp:Repeater>
                
                </td><br />    
               </tr></table>
            </ItemTemplate>
            </asp:Repeater>
        </td>
        </tr>
        </table>
    </div>
    </form>
</body>
</html>
