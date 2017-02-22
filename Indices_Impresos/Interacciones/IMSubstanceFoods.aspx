<%@ Page Language="C#" AutoEventWireup="true" CodeFile="IMSubstanceFoods.aspx.cs" Inherits="IMSubstanceFoods" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">   
    <div >    
        <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Size="X-Large" Font-Strikeout="False" style ="text-align: center "
            ForeColor="#0000C0" Text="INDICE DE IMSubstancesFoods"></asp:Label><br />
        <br />
        <br />
        
           <asp:Repeater runat="server" ID="rptSub" OnItemDataBound="rptSub_ItemDataBound">
            <ItemTemplate>
               <%--<br> <asp:Label runat="server" BorderStyle="Groove"  ID="lblProd" CssClass="label"><%# DataBinder.Eval(Container.DataItem, "Description")%></asp:Label><br>
                <asp:Label runat="server" BorderStyle="Groove"  ID="Label5" CssClass="label"><%# DataBinder.Eval(Container.DataItem, "ATCFG")%></asp:Label><br />--%>
                
                <asp:Repeater runat="server" ID="rptIMSFoods" OnItemDataBound="rptIMSFoods_ItemDataBound" >
                    <ItemTemplate>
                     <%--    <asp:Label runat="server" BorderStyle="Ridge" BackColor="SkyBlue" ID="lblrptIMSFoods" CssClass="label"><%# DataBinder.Eval(Container.DataItem, "Description")%>.</asp:Label><br>
                        <asp:Label runat="server" ID="Label1" CssClass="label"><%# DataBinder.Eval(Container.DataItem, "ClinicalSummary")%>.</asp:Label><br>
                         <asp:Label runat="server" ID="Label3" CssClass="label"><%# DataBinder.Eval(Container.DataItem, "IMASeverity")%>.</asp:Label><br>
                        <asp:Label runat="server" ID="Label6"  CssClass="label"><%# DataBinder.Eval(Container.DataItem, "Reference")%>.</asp:Label><br>--%>
                    </ItemTemplate>
                </asp:Repeater>

                 <asp:Repeater runat="server" ID="rptIMSReference" OnItemDataBound="rptIMSReference_ItemDataBound" >
                    <ItemTemplate>
                        
                        <%--<asp:Label runat="server"   BackColor="Thistle" ID="Label4" CssClass="label"><%# DataBinder.Eval(Container.DataItem, "num")%></asp:Label> 
                         <asp:Label runat="server"   BackColor="Thistle" ID="lblrptIMSFoods" CssClass="label"><%# DataBinder.Eval(Container.DataItem, "ClinicalReference")%></asp:Label><br>
 --%>

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

     <div>  <%-- <asp:button id="btnBack" runat="server"  Text="Regresar" OnClick="btnBack_Click"  ></asp:button>--%>
       </div>
    </div>
    </form>   
</body>
</html>
