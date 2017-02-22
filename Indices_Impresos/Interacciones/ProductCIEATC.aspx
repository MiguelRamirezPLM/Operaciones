<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProductCIEATC.aspx.cs" Inherits="ProductCIEATC" %>

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
            ForeColor="#0000C0" Text="PRODUCTOS ICD-ATC"></asp:Label><br />
        <br />
        <br />
        <asp:Repeater runat="server" ID="rptLet" OnItemDataBound="rptLet_ItemDataBound">
            <ItemTemplate>

            <asp:Repeater runat="server" ID="rptProd" OnItemDataBound="rptProd_ItemDataBound">
                <ItemTemplate>
                    Producto: <asp:Label runat="server"  Font-Italic="true" Font-Names="verdana" ID="lblProd" CssClass="label"><%# DataBinder.Eval(Container.DataItem, "Brand")%></asp:Label><br>
                    Forma:<asp:Label runat="server" ForeColor="Violet"  Font-Names="verdana"  ID="Label5" CssClass="label"><%# DataBinder.Eval(Container.DataItem, "PharmaForm")%></asp:Label><br />
                  <%--  Descripcion: <asp:Label runat="server" ForeColor="Violet"  Font-Names="verdana"  ID="Label1" CssClass="label"><%# DataBinder.Eval(Container.DataItem, "ProductDescription")%></asp:Label><br />
                    Division: <asp:Label runat="server" ForeColor="Violet"  Font-Names="verdana"  ID="Label3" CssClass="label"><%# DataBinder.Eval(Container.DataItem, "DivisionName")%></asp:Label><br />
                    Sustancia: <asp:Label runat="server" ForeColor="Violet"  Font-Names="verdana"  ID="Label4" CssClass="label"><%# DataBinder.Eval(Container.DataItem, "ActiveSubstance")%></asp:Label><br />
                    ATC: <asp:Label runat="server" ForeColor="Violet"  Font-Names="verdana"  ID="Label6" CssClass="label"><%# DataBinder.Eval(Container.DataItem, "atc")%></asp:Label><br />
                    ICD: <asp:Label runat="server" ForeColor="Violet"  Font-Names="verdana"  ID="Label7" CssClass="label"><%# DataBinder.Eval(Container.DataItem, "icd")%></asp:Label><br /><br />--%>
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