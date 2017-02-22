<%@ Page Language="C#" AutoEventWireup="true" CodeFile="IndTheraPEV.aspx.cs" Inherits="PEV_IndTheraPEV" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Página sin título</title>
</head>
<body>
    <form id="form1" runat="server">
<div>
    <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Size="X-Large" Font-Strikeout="False" ForeColor="#0000C0" Text="ÍNDICE Terapéutico PEV"></asp:Label><br />
        <br />
        <asp:Label ID="Label1" runat="server" Text="Selecciona la Letra:" CssClass="labelTitle"></asp:Label>
        <br />
        <table>
        <tr>
        <td align="left" style="width:178%">
        <asp:Repeater runat="server" ID="rptTerN0" OnItemDataBound="rptTerN0_ItemDataBound" >
            <ItemTemplate>
                <table>
                    <tr>
                        <td style="width:600px"></td>
                    </tr>
                    <tr>
                        <td><asp:Label runat="server" ID="lblTerN0" CssClass="labelSubTitle"><%# DataBinder.Eval(Container.DataItem, "Description")%></asp:Label></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Repeater runat="server" ID="rptProdN0" OnItemDataBound="rptProdN0_ItemDataBound">
                                <ItemTemplate>
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:Label runat="server" ID="lblBrandN0" CssClass="label"><%# DataBinder.Eval(Container.DataItem, "ProductName")%>.</asp:Label>
                                                <asp:Label runat="server" ID="lblActSubsN0" CssClass="label"><%# DataBinder.Eval(Container.DataItem, "ACTIVESUBSTANCES")%>.</asp:Label>
                                                <asp:Label runat="server" ID="lblPharmaFN0" CssClass="label"><%# DataBinder.Eval(Container.DataItem, "PHARMAFORMS")%>.</asp:Label>
                                                <asp:Label runat="server" ID="lblLabN0" CssClass="label"><%# DataBinder.Eval(Container.DataItem, "LABORATORYNAME")%>.</asp:Label>
                                                <asp:Label runat="server" ID="lblPage0" CssClass="label"><%# DataBinder.Eval(Container.DataItem, "PAGE")%>.</asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                            </asp:Repeater>   
                                   
                            <asp:Repeater runat="server" ID="rptTerN1" OnItemDataBound="rptTerN1_ItemDataBound" >
                                <ItemTemplate>                   
                                    <table>
                                        <tr>
                                            <td ><asp:Label runat="server" ID="lblTerN1" CssClass="label"><%# DataBinder.Eval(Container.DataItem, "Description")%></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td>
                                               <asp:Repeater runat="server" ID="rptProdN1" OnItemDataBound="rptProdN1_ItemDataBound">
                                                    <ItemTemplate>
                                                        <table>
                                                        <tr>
                                                        <td >
                                                            <asp:Label runat="server" ID="lblBrandN1" CssClass="label"><%# DataBinder.Eval(Container.DataItem, "ProductName")%>.</asp:Label>
                                                            <asp:Label runat="server" ID="lblActSubsN1" CssClass="label"><%# DataBinder.Eval(Container.DataItem, "ACTIVESUBSTANCES")%>.</asp:Label>
                                                            <asp:Label runat="server" ID="lblPharmaFN1" CssClass="label"><%# DataBinder.Eval(Container.DataItem, "PHARMAFORMS")%>.</asp:Label>
                                                            <asp:Label runat="server" ID="lblLabN1" CssClass="label"><%# DataBinder.Eval(Container.DataItem, "LABORATORYNAME")%>.</asp:Label>
                                                            <asp:Label runat="server" ID="lblPage1" CssClass="label"><%# DataBinder.Eval(Container.DataItem, "PAGE")%>.</asp:Label>
                                                       </td>
                                                        </td>
                                                        </tr>
                                                        </table>    
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Repeater runat="server" ID="rptTerN2" OnItemDataBound="rptTerN2_ItemDataBound">
                                                    <ItemTemplate>
                                                        <table>
                                                        <tr>
                                                            <td>
                                                                <asp:Label runat="server" ID="lblTerN2" CssClass="label"><%# DataBinder.Eval(Container.DataItem, "Description")%></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Repeater runat="server" ID="rptProdN2" OnItemDataBound="rptProdN2_ItemDataBound">
                                                                    <ItemTemplate>
                                                                        <table>
                                                                            <tr>
                                                                                <td >
                                                                                    <asp:Label runat="server" ID="lblBrandN2" CssClass="label"><%# DataBinder.Eval(Container.DataItem, "ProductName")%>.</asp:Label>
                                                                                    <asp:Label runat="server" ID="lblActSubsN2" CssClass="label"><%# DataBinder.Eval(Container.DataItem, "ACTIVESUBSTANCES")%>.</asp:Label>
                                                                                    <asp:Label runat="server" ID="lblPharmaFN2" CssClass="label"><%# DataBinder.Eval(Container.DataItem, "PHARMAFORMS")%>.</asp:Label>
                                                                                    <asp:Label runat="server" ID="lblLabN2" CssClass="label"><%# DataBinder.Eval(Container.DataItem, "LABORATORYNAME")%>.</asp:Label>
                                                                                    <asp:Label runat="server" ID="lblPage2" CssClass="label"><%# DataBinder.Eval(Container.DataItem, "PAGE")%>.</asp:Label>
                                                                                </td>
                                                                                </td>
                                                                            </tr>
                                                                        </table>    
                                                                    </ItemTemplate>
                                                               </asp:Repeater>
                                                            </td>
                                                        </tr> 
                                                         <tr>
                                            <td>
                                                <asp:Repeater runat="server" ID="rptTerN3" OnItemDataBound="rptTerN3_ItemDataBound">
                                                    <ItemTemplate>
                                                        <table>
                                                        <tr>
                                                            <td>
                                                                <asp:Label runat="server" ID="lblTerN3" CssClass="label"><%# DataBinder.Eval(Container.DataItem, "TherapeuticName")%></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Repeater runat="server" ID="rptProdN3" OnItemDataBound="rptProdN3_ItemDataBound">
                                                                    <ItemTemplate>
                                                                        <table>
                                                                            <tr>
                                                                                <td >
                                                                                    <asp:Label runat="server" ID="lblBrandN2" CssClass="label"><%# DataBinder.Eval(Container.DataItem, "ProductName")%>.</asp:Label>
                                                                                    <asp:Label runat="server" ID="lblActSubsN2" CssClass="label"><%# DataBinder.Eval(Container.DataItem, "ACTIVESUBSTANCES")%>.</asp:Label>
                                                                                    <asp:Label runat="server" ID="lblPharmaFN2" CssClass="label"><%# DataBinder.Eval(Container.DataItem, "PHARMAFORMS")%>.</asp:Label>
                                                                                    <asp:Label runat="server" ID="lblLabN2" CssClass="label"><%# DataBinder.Eval(Container.DataItem, "LABORATORYNAME")%>.</asp:Label>
                                                                                    <asp:Label runat="server" ID="lblPage3" CssClass="label"><%# DataBinder.Eval(Container.DataItem, "PAGE")%>.</asp:Label>
                                                                                </td>
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
</div>
</form>
</body>
</html>
