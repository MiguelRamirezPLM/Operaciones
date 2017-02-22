<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="Empresas.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Página sin título</title>
    <link href="Styles.css" type="text/css" rel="Stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
    <div style="text-align:left; width:100%;">
    <table width="100%">
        <tr>
            <td rowspan="7" style="width:50px;"></td>
            <td align="left" valign="top">
                <asp:Label ID="lblPageTitle" runat="server" Text="Empresas - GUÍA 58" CssClass="labelTitle"></asp:Label>
            </td>    
        </tr>
        <tr>
            <td>&nbsp;</td>
        </tr>
        
        <tr>
            <td>&nbsp;
            </td>
        </tr>
        <tr>
            <td align="center">
                 <table width="90%">
                    <tr>
                        <td style="width:70%;" align="left">
                            &nbsp;</td>
                        <td style="width:30%;" align="right">
                             <asp:Label ID="lblRegPag" runat="server" CssClass="label" Text="Registros por página"></asp:Label>
                        </td>
                         <td align="left" valign="top">
                            <asp:DropDownList ID="ddlPageSize" runat="server" AutoPostBack="True" Font-Names="Arial" Font-Size="6pt" OnSelectedIndexChanged="ddlPageSize_SelectedIndexChanged">
                                <asp:ListItem Value="-1">Ilimitado</asp:ListItem>
                                <asp:ListItem Value="5">5</asp:ListItem>
                                <asp:ListItem Value="10">10</asp:ListItem>
                                <asp:ListItem Value="25" Selected="True">25</asp:ListItem>
                                <asp:ListItem Value="50">50</asp:ListItem>
                                <asp:ListItem Value="100">100</asp:ListItem>
                            </asp:DropDownList>   
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <asp:DataGrid ID="grdCompanies" runat="server" AllowSorting="True"
                            AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="None"
                            BorderWidth="1px" CellPadding="3" Font-Names="Trebuchet MS" Font-Size="Smaller"
                            GridLines="Vertical" Width="100%" AllowPaging="True" PageSize="25" OnPageIndexChanged="grdCompanies_PageIndexChanged">
                            
                        <FooterStyle BackColor="#CCCCCC" CssClass="gridfooter" ForeColor="Black" />
                        <EditItemStyle CssClass="gridedititem" HorizontalAlign="Left" />
                        <SelectedItemStyle BackColor="#008A8C" CssClass="gridselecteditem" Font-Bold="True"
                            ForeColor="White" />
                        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" Mode="NumericPages"
                            PageButtonCount="25" />
                        <AlternatingItemStyle BackColor="Gainsboro" CssClass="gridalternateitem" HorizontalAlign="Left" />
                        <ItemStyle BackColor="#EEEEEE" CssClass="griditem" ForeColor="Black" HorizontalAlign="Left" />
                        <HeaderStyle BackColor="#00397A" CssClass="gridheader" Font-Bold="True" ForeColor="White" />
                    
                        <Columns>
                        
                            <asp:TemplateColumn HeaderText="Nombre largo">
                                 <ItemTemplate>
                                    <a href='Marcas.aspx?EmpresaId=<%# DataBinder.Eval(Container.DataItem, "ID") %>' style="border=0;" >
                                        <%# DataBinder.Eval(Container.DataItem, "Nombre LARGO - GUIA 53")%>
                                    </a>
                                 </ItemTemplate>
                                 <ItemStyle Width="250px" Wrap="True" />
                            </asp:TemplateColumn>
                            
                            <asp:BoundColumn DataField="Nombre CORTO de la Empresa - GUIA 53" HeaderText="Nombre corto">
                                <ItemStyle Width="150px" Wrap="True" />
                            </asp:BoundColumn>
                            
                            <asp:BoundColumn DataField="N&#250;m de MARCAS" HeaderText="Marcas">
                                <ItemStyle Width="30px" Wrap="True" />
                            </asp:BoundColumn>

                            </Columns>
                         </asp:DataGrid>
                         
                    </td>
                </tr>
              </table>
            </td>
        </tr>
    </table>
</div>
    </form>
</body>
</html>
