<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>DEF 56</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Size="X-Large" Font-Strikeout="False"
            ForeColor="#0000C0" Text="ÍNDICE DE MARCAS GUÍA 56"></asp:Label><br />
        <br />
        <asp:Label ID="Label1" runat="server" Text="Selecciona la Letra:" CssClass="labelTitle"></asp:Label>
        <asp:DropDownList ID="ddlLetra" runat="server" Width="72px"  AutoPostBack="True">
            <asp:ListItem Value="-----">-----</asp:ListItem>
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
            <asp:ListItem Value="%">Todas</asp:ListItem>
        </asp:DropDownList>
        <br />
        <br />
        <table>
        <tr>
        <td align="left" style="width:178%">
        <asp:Repeater runat="server" ID="rptLetter" OnItemDataBound="rptLetter_ItemDataBound">
         <ItemTemplate>
            <img alt="" src='\\195.192.2.180\Imagenes\GUIA 56\Logos\<%# DataBinder.Eval(Container.DataItem, "Archivo") %>'style="border:0; " width="25%" />  
            <asp:Repeater runat="server" ID="rptMarcas" OnItemDataBound="rptMarcas_ItemDataBound">
                <ItemTemplate>
                    <table>
                    <tr><td style="width:600px">
                     <img alt="" src='\\mmedina001\MARCAS - GUIA 56\Logos\<%# DataBinder.Eval(Container.DataItem, "logo") %>'style="border:0; " width="25%" />    
                    </td></tr><tr><br /><td><asp:Label runat="server" ID="lblMarca" CssClass="labelSubTitle"><%# DataBinder.Eval(Container.DataItem,"brandname") %></asp:Label></td></tr><br />
                     <tr><td>
                        <asp:Repeater runat="server" ID="rptEmpresas" OnItemDataBound="rptEmpresas_ItemDataBound">
                            <ItemTemplate>                   
                                <table>
                                <tr>
                                <td ><asp:Label runat="server" ID="lblCarta" CssClass="label"><%# DataBinder.Eval(Container.DataItem, "clientbrandtypeId")%></asp:Label></td>
                                <td><asp:Label runat="server" ID="lblEmpresa" CssClass="label"><%# DataBinder.Eval(Container.DataItem, "companyname")%></asp:Label></td>
                                <br />    
                                </tr></table>
                            </ItemTemplate>
                        </asp:Repeater>
                        </td></tr></table>          
                </ItemTemplate>
            </asp:Repeater>
        </ItemTemplate>
        </asp:Repeater>
        
        </td>
        </tr>
        </table>
        
    </div>
    </form>
</body>
</html>
