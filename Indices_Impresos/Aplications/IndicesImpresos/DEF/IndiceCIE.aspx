<%@ Page Language="C#" AutoEventWireup="true" CodeFile="IndiceCIE.aspx.cs" Inherits="DEF_CIE" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Size="X-Large" Font-Strikeout="False"
            ForeColor="#0000C0" Text="ÍNDICE CIE DEF 60"></asp:Label><br />
        <br />
       <%-- <asp:Label ID="Label1" runat="server" Text="Selecciona la Letra:" CssClass="labelTitle"></asp:Label>
        <asp:DropDownList ID="ddlLetra" runat="server" Width="72px" OnSelectedIndexChanged="ddlLetra_SelectedIndexChanged" AutoPostBack="True">
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
        </asp:DropDownList>--%>
        <br />
        <br />
        <table>
        <tr>
        <td align="left" style="width:178%">
        <asp:Repeater runat="server" ID="rptCIEN0" OnItemDataBound="rptCIEN0_ItemDataBound" >
            <ItemTemplate>
                <table>
                <tr><td style="width:600px">
                 </td></tr><tr><br /><td><b>CAPITULO</b><asp:Label runat="server" ID="lblCIEN0" CssClass="labelSubTitle"><%# DataBinder.Eval(Container.DataItem, "ICDKey") + " - " + DataBinder.Eval(Container.DataItem, "SPANISHDESCRIPTION")%></asp:Label></td></tr><br />
                 <tr><td>
                       <asp:Repeater runat="server" ID="rptCIEN1" OnItemDataBound="rptCIEN1_ItemDataBound" >
                        <ItemTemplate>                   
                            <table>
                            <tr>
                            <td ><b>NIVEL 1</b><asp:Label runat="server" ID="lblCIEN1" CssClass="label"><%# DataBinder.Eval(Container.DataItem, "ICDKey") + " - " + DataBinder.Eval(Container.DataItem, "SPANISHDESCRIPTION")%></asp:Label>
                            </td><br />    
                            </tr>
                                <tr>
                                <td>
                                    <asp:Repeater runat="server" ID="rptProdN1" OnItemDataBound="rptProdN1_ItemDataBound">
                                    <ItemTemplate>
                                    <table>
                                    <tr>
                                    <td >
                                        <asp:Label runat="server" ID="lblBrandN1" CssClass="label"><%# DataBinder.Eval(Container.DataItem, "BRAND")%>.</asp:Label>
                                        <asp:Label runat="server" ID="lblActSubsN1" CssClass="label"><%# DataBinder.Eval(Container.DataItem, "ACTIVESUBSTANCES")%>.</asp:Label>
                                        <asp:Label runat="server" ID="lblPharmaFN1" CssClass="label"><%# DataBinder.Eval(Container.DataItem, "PHARMAFORMS")%>.</asp:Label>
                                        <asp:Label runat="server" ID="lblLabN1" CssClass="label"><%# DataBinder.Eval(Container.DataItem, "LABORATORYNAME")%>.</asp:Label>
                                        <asp:Label runat="server" ID="lblPage" CssClass="label"><%# DataBinder.Eval(Container.DataItem, "PAGE")%>.</asp:Label>
                                    </td>
                                    </tr>
                                    </table>    
                                    </ItemTemplate>
                                    </asp:Repeater>
                                 </td></tr>
                                 <tr><td>
                            <asp:Repeater runat="server" ID="rptCIEN2" OnItemDataBound="rptCIEN2_ItemDataBound">
                                    <ItemTemplate>
                                    <table>
                                    <tr>
                                    <td ><b>NIVEL 2</b>
                                        <asp:Label runat="server" ID="lblCIEN2" CssClass="label"><%# DataBinder.Eval(Container.DataItem, "ICDKey") + " - " +  DataBinder.Eval(Container.DataItem, "SPANISHDESCRIPTION")%></asp:Label>
                                    </td><br />
                                    </tr>
                                  
                                    <tr>
                                    <td>
                                        <asp:Repeater runat="server" ID="rptProdN2" OnItemDataBound="rptProdN2_ItemDataBound">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblBrandN2" CssClass="label"><%# DataBinder.Eval(Container.DataItem, "BRAND")%>.</asp:Label>
                                            <asp:Label runat="server" ID="lblActSubsN2" CssClass="label"><%# DataBinder.Eval(Container.DataItem, "ACTIVESUBSTANCES")%>.</asp:Label>
                                            <asp:Label runat="server" ID="lblPharmaFN2" CssClass="label"><%# DataBinder.Eval(Container.DataItem, "PHARMAFORMS")%>.</asp:Label>
                                            <asp:Label runat="server" ID="lblLabN2" CssClass="label"><%# DataBinder.Eval(Container.DataItem, "LABORATORYNAME")%>.</asp:Label>
                                            <asp:Label runat="server" ID="lblPage2" CssClass="label"><%# DataBinder.Eval(Container.DataItem, "PAGE")%>.</asp:Label>
                                         </ItemTemplate>
                                        </asp:Repeater>
                                    </td>
                                    </tr>
                                    </table>    
                                    </ItemTemplate>
                                    </asp:Repeater>
                                </td>
                                </tr>
                                </table>
                                </ItemTemplate>
                                </asp:Repeater>
                            </td>
                            </tr>
                            </table>
                        </ItemTemplate>
                    </asp:Repeater>
                    </td></tr></table>          
           
    </div>
    </form>
</body>
</html>
