<%@ Page Language="C#" AutoEventWireup="true" CodeFile="indiceSemillas.aspx.cs" Inherits="indiceSemillas" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Página sin título</title>
     <link href='../estilos.css' rel='stylesheet' type='text/css' />
</head>
<body>
    <asp:Repeater ID="rptLevel_0" runat="server" OnItemDataBound="rptLevel_0_ItemDataBound" >
 <ItemTemplate> 
  <table borderColor='#999999' cellSpacing='0' cellPadding='7' width='100%' border='0' id='table1'> 
    <tr>     
        <td>
            <table id='table2'>
                <tr>
                    <td width='600'>
                        <div class='NomLab'> <%# getImage((string)Eval("Image")) %><br /><%# Eval("DivisionName")%>
                        </div>                          
                            
                            <table>
                                <tr>
                                    <td width='600'>                           
                                       
                                        <%#getDatos((string)Eval("Address"), (string) Eval("Suburb"),(string)Eval("Location"), (string)Eval("ZipCode"), (string) Eval("Telephone"), (string) Eval("Lada"), (string)Eval("Fax"), (string)Eval("Web"), (string)Eval("City"), (string)Eval("State"), (string)Eval("Email")) %>                                      
                                    </td>
                                    
                                </tr>
                                
                            </table>
                          
                    </td>                                    
                    
                    
                </tr>                
            </table>
        <hr>
        </td>
      
    </tr> 
    
    &nbsp;    
    
     <asp:Repeater ID="rptLevel_00" runat="server" OnItemDataBound="rptLevel_00_ItemDataBound"  >
       <ItemTemplate>
            <tr>
                <td>
                    <p class='semilla'><%# Eval("SeedName") %></p>
                </td>
            </tr>
            
            <!--Producto con contenido --> 
            <asp:Repeater ID="rptLevel_1" runat="server"  >
              <ItemTemplate>
                 
               <tr>     
                 
                 <td>                 
                    <p class="productoSemilla"><a href="<%# Eval("ProductId")+"_"+Eval("PharmaFormID")+".htm"%>" style='text-decoration:none' class=Producto">* <%# Eval("Producto")%></a></p>
                    
                 </td>      
               </tr>   
              </ItemTemplate>
            </asp:Repeater>   
            
       </ItemTemplate>
     </asp:Repeater> 
    
  </table>
  
 </ItemTemplate>
</asp:Repeater>
</body>
</html>
