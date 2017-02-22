<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Seccion_1_prods.aspx.cs" Inherits="Seccion_1_prods" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
<link href='../estilos.css' rel='stylesheet' type='text/css' />
    <title>Página sin título</title>
</head>
<body>
    <form id="form1" runat="server">
    
    <div>
    
    
        <table width="100%">
            <tr>
                <td>
                    <p align="center" class="SectionTitle">                       
                            <asp:Label ID="lblSubSectionDescription" runat="server"></asp:Label>                      
                    </p>
                </td>
            </tr>
            <tr>
                <td>
                   &nbsp; 
                </td>
            </tr>
            <tr>
                <td align="left"> 
                
                    <asp:Repeater ID="rptSubSections" runat="server">
                    
                        <ItemTemplate> 
                        
                            <p class="producto"> <a class="linksSections" href="../Productos/<%# Eval("HtmlFile") %>" style='text-decoration:none'>* <%# Eval("ProductName") %>. <i><%# Eval("Detail") %></i> <b><%# Eval("CompanyShortName") %></b></a></p>
                            
                           
                        
                        </ItemTemplate> 
                    
                    </asp:Repeater> 
                
                </td>
            </tr>
            
        </table>
    </div>
    </form>
</body>
</html>
