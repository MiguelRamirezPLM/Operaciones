<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FarmaPrecios.aspx.cs" Inherits="FarmaPrecios" %>

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
            ForeColor="#0000C0" Text="FARMAPRECIOS"></asp:Label><br />
        <br />
        <br />
         
           <asp:Repeater runat="server" ID="rptLet" OnItemDataBound="rptLet_ItemDataBound">
            <ItemTemplate>
                
                  <asp:Label runat="server" ID="lblLet" CssClass="label" ForeColor="#cc0099" Font-Size="20px" Font-Bold="true"><%# DataBinder.Eval(Container.DataItem, "Letter")%></asp:Label><br /><br />
                 
                <asp:Repeater runat="server" ID="rptProd" OnItemDataBound="rptProd_ItemDataBound" >
                    <ItemTemplate>
                        
                        <asp:Label runat="server" ID="Label3" CssClass="label" ForeColor="#0000C0" Font-Italic="true"><%# DataBinder.Eval(Container.DataItem, "Brand")%></asp:Label>                            
                        <asp:Label runat="server" ID="Label1" CssClass="label"><%#DataBinder.Eval(Container.DataItem, "DivisionShortName")%></asp:Label><br />
                        <asp:Label runat="server" ID="Label4" CssClass="label" Font-Italic="true" ><%# DataBinder.Eval(Container.DataItem, "ActiveSubstances")%></asp:Label><br />
                        <asp:Label runat="server" ID="Label5" CssClass="label"><%#DataBinder.Eval(Container.DataItem, "ProductDescription")%></asp:Label><br />

                        <asp:Repeater runat="server" ID="rptProdPresentation" OnItemDataBound="rptProdPresentation_ItemDataBound" >                      
                        <ItemTemplate>
                            <asp:Label runat="server" ID="Label6" CssClass="label" Font-Bold="true"><%#DataBinder.Eval(Container.DataItem, "Presentation")%></asp:Label>   
                           <%-- <asp:Label runat="server" ID="Label7" CssClass="label" Font-Italic="true"><%#DataBinder.Eval(Container.DataItem, "Price")%></asp:Label> --%> 
                            <asp:Label runat="server" ID="Label8" CssClass="label" Font-Italic="true"><%#DataBinder.Eval(Container.DataItem, "AveragePrice")%></asp:Label>   
                       <br>
                        </ItemTemplate>
                        </asp:Repeater> 


                    </ItemTemplate>
                </asp:Repeater> <br>
                
               
            </ItemTemplate>
           </asp:Repeater>   
         
    </div>
    </form>
</body>
</html>
