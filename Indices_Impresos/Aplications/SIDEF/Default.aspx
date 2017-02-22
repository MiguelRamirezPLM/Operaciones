<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.1//EN" "http://www.w3.org/TR/xhtml11/DTD/xhtml11.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>SIDEF</title>
</head>
<body>
    <form id="form1" runat="server">
        <ajaxToolkit:ToolkitScriptManager ID="ScriptManager1" runat="server" />
        <div style="text-align:center;">
            <%--<dxdv:ASPxDataView ID="ASPxDataView1" runat="server" ColumnCount="2" RowPerPage="15">
                <ItemTemplate>
                    <a href='../productos/<%# Eval("ProductId")%>_<%# Eval("PharmaFormId")%>.htm' target="_blank">
                    <img id="ASPxImage1" src='images/<%# Eval("ImageName")%>' alt="imagen"/></a>
                </ItemTemplate>
                <PagerSettings SEOFriendly="Enabled">
                    
                </PagerSettings>
             
               
            </dxdv:ASPxDataView>--%>
             <asp:Label ID="lblLab" runat="server" Font-Names="verdana" Font-Bold="true" Font-Size="14px"></asp:Label> 
              <br />
              <br />
             <asp:DataList ID= "DataList1" runat = "server" RepeatColumns="4" RepeatDirection="Horizontal" CellPadding="2" CellSpacing="40" BorderWidth="2"
              HorizontalAlign="Center" BorderStyle="Solid" GridLines="Both" CssClass="Tabla" ItemStyle-Height="250px" ItemStyle-Width="250px"> 
             <ItemTemplate>
               <a href='../productos/<%# Eval("ProductId")%>_<%# Eval("PharmaFormId")%>.htm' style="text-decoration:none;" >
                
                  
                  
                    <center><img id="ASPxImage1"  src='images/<%# Eval("ProductShot")%>' alt="imagen" /></a></center>
                    
                 
             </ItemTemplate>
             
             
             
             </asp:DataList> 
               
               
               
        </div>
    </form>
</body>
</html>