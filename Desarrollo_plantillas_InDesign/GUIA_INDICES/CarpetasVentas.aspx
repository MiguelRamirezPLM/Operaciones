<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CarpetasVentas.aspx.cs" Inherits="CarpetasVentas" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:Repeater runat="server" ID="rptCliente" OnItemDataBound="rptCliente_ItemDataBound">
         <ItemTemplate>
            <asp:Label ID="lblNombreC" runat="server" Font-Size="Larger" Font-Bold="true" Text='<%# DataBinder.Eval(Container.DataItem, "CompanyName") %>'></asp:Label><br />
             <asp:Label runat="server" ID="Label1" CssClass="labelSubTitle"><%# DataBinder.Eval(Container.DataItem, "ShortName")%></asp:Label><br />
            <asp:Label runat="server" ID="lblNombreLargoC" CssClass="labelSubTitle"><%# DataBinder.Eval(Container.DataItem, "Address")%></asp:Label><br />
            <asp:Label runat="server" ID="Label2" CssClass="labelSubTitle"><%# DataBinder.Eval(Container.DataItem, "Suburb")%></asp:Label><br />
             <asp:Label runat="server" ID="Label3" CssClass="labelSubTitle"><%# DataBinder.Eval(Container.DataItem, "ZipCode")%></asp:Label><br />
             <asp:Label runat="server" ID="Label4" CssClass="labelSubTitle"><%# DataBinder.Eval(Container.DataItem, "StateName")%></asp:Label><br />
             <asp:Label runat="server" ID="Label5" CssClass="labelSubTitle"><%# DataBinder.Eval(Container.DataItem, "PHONE")%></asp:Label><br />
             <asp:Label runat="server" ID="Label6" CssClass="labelSubTitle"><%# DataBinder.Eval(Container.DataItem, "PHONEFAX")%></asp:Label><br />
             <asp:Label runat="server" ID="Label7" CssClass="labelSubTitle"><%# DataBinder.Eval(Container.DataItem, "FAX")%></asp:Label><br />
             <asp:Label runat="server" ID="Label8" CssClass="labelSubTitle"><%# DataBinder.Eval(Container.DataItem, "LADA")%></asp:Label><br />
             <asp:Label runat="server" ID="Label9" CssClass="labelSubTitle"><%# DataBinder.Eval(Container.DataItem, "Lads")%></asp:Label><br />
             <asp:Label runat="server" ID="Label10" CssClass="labelSubTitle"><%# DataBinder.Eval(Container.DataItem, "Email")%></asp:Label><br />
             <asp:Label runat="server" ID="Label11" CssClass="labelSubTitle"><%# DataBinder.Eval(Container.DataItem, "Web")%></asp:Label><br />
             <asp:Label runat="server" ID="Label12" CssClass="labelSubTitle"><%# DataBinder.Eval(Container.DataItem, "TypeName")%></asp:Label><br />
             <asp:Label runat="server" ID="Label13" CssClass="labelSubTitle" ForeColor="Tomato"><b>Productos y Servicios: </b><%# DataBinder.Eval(Container.DataItem, "NumPyS")%></asp:Label><br />
             <asp:Label runat="server" ID="Label14" CssClass="labelSubTitle" ForeColor="SlateBlue"><b>Marcas:</b> <%# DataBinder.Eval(Container.DataItem, "Brands")%></asp:Label><br />
             <asp:Label runat="server" ID="Label15" CssClass="labelSubTitle" ForeColor="Sienna"><b>Anuncios:</b> <%# DataBinder.Eval(Container.DataItem, "Advers")%></asp:Label><br />
             <asp:Label runat="server" ID="Label16" CssClass="labelSubTitle" ForeColor="PaleVioletRed"><b>Sucursales:</b> <%# DataBinder.Eval(Container.DataItem, "Sucursales")%></asp:Label><br /> 
            <asp:Label runat="server" ID="Label17" CssClass="labelSubTitle" ForeColor="SteelBlue"><b>Especialidades:</b> <%# DataBinder.Eval(Container.DataItem, "Specialities")%></asp:Label><br />
             <asp:Label runat="server" ID="Label18" CssClass="labelSubTitle" ForeColor="Chocolate"><b>Textos:</b> <%# DataBinder.Eval(Container.DataItem, "Textos")%></asp:Label><br />
              <br /> ESPECIALIDADES <br />
                <asp:Repeater runat="server" ID="rptSpecialities" OnItemDataBound="rptSpecialities_ItemDataBound">
                <ItemTemplate>
                    <asp:Label ID="lblNombreC" runat="server"  Font-Bold="true" ForeColor="HotPink" Text='<%# DataBinder.Eval(Container.DataItem, "Description") %>'></asp:Label>  <b>/</b>
                    <asp:Label ID="Label19" runat="server"  Font-Bold="true" ForeColor="HotPink" Text='<%# DataBinder.Eval(Container.DataItem, "AdversDescription") %>'></asp:Label>  <b>/</b>
                    <asp:Label ID="Label20" runat="server"  Font-Bold="true" ForeColor="HotPink" Text='<%# DataBinder.Eval(Container.DataItem, "Quantity") %>'></asp:Label><br />  
                    <br />
                 </ItemTemplate>
                 </asp:Repeater>
             <br /> SUCURSALES <br />
              <asp:Repeater runat="server" ID="rptSucursalesD" OnItemDataBound="rptSucursalesD_ItemDataBound">
                <ItemTemplate>
                     <asp:Label ID="lblNombreC" runat="server"  Font-Bold="true" ForeColor="GREEN" Text='<%# DataBinder.Eval(Container.DataItem, "CompanyName") %>'></asp:Label>  <b>/</b>
                 <%--    <asp:Label ID="Label19" runat="server"  Font-Bold="true" ForeColor="GREEN" Text='<%# DataBinder.Eval(Container.DataItem, "Address") %>'></asp:Label>  <b>/</b>
                     <asp:Label ID="Label20" runat="server"  Font-Bold="true" ForeColor="GREEN" Text='<%# DataBinder.Eval(Container.DataItem, "Suburb") %>'></asp:Label>  <b>/</b> 
                     <asp:Label ID="Label21" runat="server"  Font-Bold="true" ForeColor="GREEN" Text='<%# DataBinder.Eval(Container.DataItem, "ZipCode")%>'></asp:Label>  <b>/</b>
                     <asp:Label ID="Label22" runat="server"  Font-Bold="true" ForeColor="GREEN" Text='<%# DataBinder.Eval(Container.DataItem, "StateName")%>'></asp:Label>  <b>/</b>
                     <asp:Label ID="Label23" runat="server"  Font-Bold="true" ForeColor="GREEN" Text='<%# DataBinder.Eval(Container.DataItem, "PHONE")%>'></asp:Label>  <b>/</b>
                     <asp:Label ID="Label24" runat="server"  Font-Bold="true" ForeColor="GREEN" Text='<%# DataBinder.Eval(Container.DataItem, "PHONEFAX")%>'></asp:Label>  <b>/</b>
                     <asp:Label ID="Label25" runat="server"  Font-Bold="true" ForeColor="GREEN" Text='<%# DataBinder.Eval(Container.DataItem, "FAX")%>'></asp:Label>  <b>/</b>
                     <asp:Label ID="Label26" runat="server"  Font-Bold="true" ForeColor="GREEN" Text='<%# DataBinder.Eval(Container.DataItem, "LADA")%>'></asp:Label>  <b>/</b>
                     <asp:Label ID="Label27" runat="server"  Font-Bold="true" ForeColor="GREEN" Text='<%# DataBinder.Eval(Container.DataItem, "Lads")%>'></asp:Label>  <b>/</b>
                     <asp:Label ID="Label28" runat="server"  Font-Bold="true" ForeColor="GREEN" Text='<%# DataBinder.Eval(Container.DataItem, "Email")%>'></asp:Label>  <b>/</b>
                     <asp:Label ID="Label29" runat="server"  Font-Bold="true" ForeColor="GREEN" Text='<%# DataBinder.Eval(Container.DataItem, "Web")%>'></asp:Label>  <b>/</b><br />--%>
                 </ItemTemplate>
                 </asp:Repeater>

             <br /> Productos <br />

             <asp:Repeater runat="server" ID="rptPySClients" OnItemDataBound="rptPySClients_ItemDataBound">
                <ItemTemplate>
                    <asp:Label ID="Label29" runat="server"   Font-Bold="true" ForeColor="Pink" Text='<%# DataBinder.Eval(Container.DataItem, "CompanyName")%>'></asp:Label>  <b>/</b>
                    <asp:Label ID="Label34" runat="server"   Font-Bold="true" ForeColor="Pink" Text='<%# DataBinder.Eval(Container.DataItem, "ShortName")%>'></asp:Label>  <b>/</b>
                    <asp:Label ID="Label30" runat="server"   Font-Bold="true" ForeColor="Pink" Text='<%# DataBinder.Eval(Container.DataItem, "NumPyS")%>'></asp:Label> <br />
                                                
                        <asp:Repeater runat="server" ID="rptPySRaiz" OnItemDataBound="rptPySRaiz_ItemDataBound">
                            <ItemTemplate>
                                         <asp:Label ID="Label29" runat="server"  Font-Bold="true" ForeColor="OliveDrab" Text='<%# DataBinder.Eval(Container.DataItem, "Description")%>'></asp:Label>  <b>/</b><br />
                                                 <asp:Repeater runat="server" ID="rptPySRaizH" OnItemDataBound="rptPySRaizH_ItemDataBound">
                                                 <ItemTemplate>
                                                    <asp:Label ID="Label29" runat="server"   Font-Bold="true" ForeColor="RED" Text='<%# DataBinder.Eval(Container.DataItem, "Description")%>'></asp:Label>  <b>/</b><br />
                                                </ItemTemplate>
                                                </asp:Repeater>
                        </ItemTemplate>
                            </asp:Repeater>
                 </ItemTemplate>
              </asp:Repeater>

              
             <br /> MARCAS<br />
             <asp:Repeater runat="server" ID="rptClienteBrand" OnItemDataBound="rptClienteBrand_ItemDataBound">
                <ItemTemplate>
                    <asp:Label ID="Label29" runat="server"   Font-Bold="true" ForeColor="RED" Text='<%# DataBinder.Eval(Container.DataItem, "CompanyName")%>'></asp:Label>  <b>/</b>
                    <asp:Label ID="Label30" runat="server"   Font-Bold="true" ForeColor="RED" Text='<%# DataBinder.Eval(Container.DataItem, "Brands")%>'></asp:Label> <br />
                        
                        <asp:Repeater runat="server" ID="rptClienteBrandDetail" OnItemDataBound="rptClienteBrandDetail_ItemDataBound">
                            <ItemTemplate>
                                <asp:Label ID="Label29" runat="server"   Font-Bold="true" ForeColor="Green" Text='<%# DataBinder.Eval(Container.DataItem, "ClientBrandTypeId")%>'></asp:Label>  <b>/</b>
                              <%--  <asp:Label ID="Label30" runat="server"   Font-Bold="true" ForeColor="Green" Text='<%# DataBinder.Eval(Container.DataItem, "BrandName")%>'></asp:Label><b>/</b>
                                <asp:Label ID="Label31" runat="server"   Font-Bold="true" ForeColor="Green" Text='<%# DataBinder.Eval(Container.DataItem, "ExpireDescription")%>'></asp:Label><b>/</b>
                                <asp:Label ID="Label32" runat="server"   Font-Bold="true" ForeColor="Green" Text='<%# DataBinder.Eval(Container.DataItem, "TypeBrand")%>'></asp:Label><b>/</b>
                                <asp:Label ID="Label33" runat="server"   Font-Bold="true" ForeColor="Green" Text='<%# DataBinder.Eval(Container.DataItem, "Logo")%>'></asp:Label><b>/</b>--%>
                                 <br />
                            </ItemTemplate>
                        </asp:Repeater>

                 </ItemTemplate>
              </asp:Repeater>
             <br />
       </ItemTemplate>
        </asp:Repeater>


    </div>
    </form>
</body>
</html>
