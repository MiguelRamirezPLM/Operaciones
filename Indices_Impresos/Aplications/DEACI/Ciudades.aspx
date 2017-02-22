<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Ciudades.aspx.cs" Inherits="Cities" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
<link href='../estilos.css' rel='stylesheet' type='text/css' />
    <title>Guía de Fabricantes</title>
</head>
<body>
    <form id="form1" runat="server">
    <p id="lblTitle" runat="server" align='center' class='titulo'></p>
    <p align='center' class="titulo"><asp:Label ID="lblState" runat="server"></asp:Label></p>
    <%--<div style='overflow:auto; width:728px; height:500px;'>--%>
    <div style='overflow:auto;'>
        <asp:Repeater ID="rptCities" runat="server" OnItemDataBound="rptCities_ItemDataBound">
            <ItemTemplate>
                <p class='subsectionEstado'><b><%# DataBinder.Eval(Container.DataItem, "Name").ToString().ToUpper() %></b></p>
                <ul>
                    <asp:Repeater ID="rptComp" runat="server" OnItemDataBound="rptComp_ItemDataBound" >
                        <ItemTemplate>
                            <span class='company'><%# getCompany(DataBinder.Eval(Container.DataItem, "CompanyName").ToString(), DataBinder.Eval(Container.DataItem, "HtmlFile").ToString()) %></span><br>
                            <span class="direccion"><%# getText(DataBinder.Eval(Container.DataItem, "Address").ToString(),(int)1) %>
                            <%# getText(DataBinder.Eval(Container.DataItem, "Suburb").ToString(), (int)1) %>
                            <%# getText(DataBinder.Eval(Container.DataItem, "PostalCode").ToString(),(int)2) %>
                            <asp:Repeater ID="rptPhones" runat="server">
                                <ItemTemplate>
                                    <%# getPhone(DataBinder.Eval(Container.DataItem,"Description").ToString(), DataBinder.Eval(Container.DataItem, "PhoneValue").ToString())%> 
                                </ItemTemplate>
                            </asp:Repeater>
                             <%# getText(DataBinder.Eval(Container.DataItem, "Email").ToString(),(int)3) %>
                             <%# getText(DataBinder.Eval(Container.DataItem, "Web").ToString(), (int)4)%>
                             <%# getText(DataBinder.Eval(Container.DataItem, "Contact").ToString(), (int)5)%></span>  
                            <br>
                        </ItemTemplate>
                    </asp:Repeater>
                </ul>
            </ItemTemplate>    
        </asp:Repeater>
    </div>
    </form>
</body>
</html>
