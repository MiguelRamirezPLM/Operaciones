<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Interactions.aspx.cs" Inherits="Interactions" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Página sin título</title>
    <script language="javascript" type="text/javascript">
</script>
</head>
<body>
    <form id="form1" runat="server">   
    <div >    
        <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Size="X-Large" Font-Strikeout="False" style ="text-align: center "
            ForeColor="#0000C0" Text="INTERACCIONES"></asp:Label><br />
        <br />
        <br />
        
            <asp:Repeater runat="server" ID="rptLet" OnItemDataBound="rptLet_ItemDataBound">
            <ItemTemplate>
                
                <asp:Repeater runat="server" ID="rptProd" OnItemDataBound="rptProd_ItemDataBound" >
                    <ItemTemplate>
                        
                    </ItemTemplate>
                </asp:Repeater>
                                
            </ItemTemplate>
            </asp:Repeater>
         <br /><br />
   <div style="text-align:center; font-family:Verdana; font-size: 20px; color:red;">  
        <asp:Label Text="Incice Generado" runat="server" ></asp:Label> 
       </div>
        <br />
        <br />

     <div>   <asp:button id="btnBack" runat="server"  Text="Regresar"  ></asp:button>
       </div>
    </div>
    </form>   
</body>
</html>