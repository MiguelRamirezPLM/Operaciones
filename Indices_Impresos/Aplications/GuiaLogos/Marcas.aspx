<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Marcas.aspx.cs" Inherits="Marcas" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Página sin título</title>
    <link href="Styles.css" type="text/css" rel="Stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
    <div style="text-align:left; width:100%;">
    <table width="100%" >
        <tr>
            <td rowspan="7" style="width:50px;"></td>
            <td align="left" valign="top">
                <asp:Label ID="lblPageTitle" runat="server" Text="Marcas" CssClass="labelTitle"></asp:Label>
            </td>    
        </tr>
        <tr>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td rowspan="7" style="width:50px;"></td>
            <td align="left" valign="top">
                <a id="lknback" href="javascript:history.back();">REGRESAR</a>
            </td>    
        </tr>
        <tr>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td align="center">
               <table width="90%" >
                    <tr>
                        <td style="text-align:left;">
                            <asp:Label ID="lblShortCompanyName" runat="server" Text="Label" CssClass="labelSubTitle"></asp:Label>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td style="text-align:left;">
                            <asp:Label ID="lblTotalBrands" runat="server" Text="Label" CssClass="labelSubTitle"></asp:Label>
                        </td>
                    </tr>
               </table>
            </td>
        </tr>
        <tr>
            <td>&nbsp;
            </td>
        </tr>
        <tr>
            <td align="center">
            
                <table width="90%" >
            
                    <tr>
                        <td align="left" class="labelSubTitle" style="width:50%;">
                            MARCA
                        </td>
                        <td style="width:5%;">
                            &nbsp;
                        </td>
                        <td align="center" class="labelSubTitle" style="width:5%;">
                            CARTA
                        </td>
                        <td style="width:5%;">
                            &nbsp;
                        </td>
                        <td align="center" class="labelSubTitle" style="width: 150px">
                           LOGO
                        </td>
                    </tr>            
    
                <asp:Repeater ID="rptBrands" runat="server" OnItemDataBound="rptBrands_ItemDataBound">
                
                    
                    <ItemTemplate>
                    
                               
                            <tr>
                                <td align="left" style="width:50%">
                                    <%# DataBinder.Eval(Container.DataItem, "MARCA") %>
                                </td>
                                <td style="border:1; width:5%;">
                                    &nbsp;
                                </td>
                                <td align="center" style="width:5%;">
                                    <%# DataBinder.Eval(Container.DataItem, "CARTA") %>
                                </td>
                                <td style="border:1; width:10%;">
                                    &nbsp;
                                </td>
                                <td align="left" style="width:40%; height:40%;" >
                                    <asp:Image ID="idimg" runat="server" Width="80%" Height="80%"  Visible="false" />                                
                                    <asp:Image ID="idimg4" runat="server" Width="50%" Height="50%"  Visible="false" />                                
                                </td>
                            </tr>        
  
                    </ItemTemplate>
                
                </asp:Repeater>
            
                </table>
                
            </td>
        </tr>
    </table>
</div>
    </form>
</body>
</html>
