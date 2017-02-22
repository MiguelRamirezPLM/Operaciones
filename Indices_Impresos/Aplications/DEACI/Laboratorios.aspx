<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Laboratorios.aspx.cs" Inherits="Laboratories" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
<link href="../../estilos.css" rel="stylesheet" type="text/css" />
    <title>LABORATORIOS Y COMPAÑÍAS PARTICIPANTES</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <p align='center' class="titulo">LABORATORIOS Y COMPAÑÍAS PARTICIPANTES</p>
        
        <asp:Repeater ID="rptComp" runat="server" >
        <ItemTemplate>
            <span class="lab"> <%# getCompany(DataBinder.Eval(Container.DataItem, "CompanyName").ToString(), DataBinder.Eval(Container.DataItem, "HtmlFile").ToString()) %></span><br>
        </ItemTemplate>
        </asp:Repeater>
            
    </div>
    </form>
</body>
</html>
