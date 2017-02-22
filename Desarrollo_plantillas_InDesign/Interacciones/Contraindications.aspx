<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Contraindications.aspx.cs" Inherits="Contraindications" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
 <form id="form1" runat="server">   
    <div>    
        <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Size="X-Large" Font-Strikeout="False"
            ForeColor="#0000C0" Text="CONTRAINDICACIONES"></asp:Label><br />
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