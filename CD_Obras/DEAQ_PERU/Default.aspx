<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table>
     <tr>
      <td>
       Edicion: 
      </td>
      <td>
       <asp:TextBox runat="server" ID="txtEdition"></asp:TextBox>
      </td>
     </tr>
     <tr>
      <td>Generar Indice de Cultivos</td>
      <td><asp:Button runat="server" ID="btnclick" OnClick="generateCrops"  Text="Generar" /></td>
     </tr>
     <tr>
      <td>Generar Indice de Laboratorios</td>
      <td><asp:Button runat="server" ID="Button1" OnClick="generateLabs"  Text="Generar" /></td>
     </tr>
     <tr>
      <td>Generar Indice de Productos</td>
      <td><asp:Button runat="server" ID="Button2" OnClick="generateProducts"  Text="Generar" /></td>
     </tr>
     <tr>
      <td>Generar Indice de Semillas</td>
      <td><asp:Button runat="server" ID="Button3" OnClick="generateSeeds"  Text="Generar" /></td>
     </tr>
     <tr>
      <td>Generar Indice de Sustancias</td>
      <td><asp:Button runat="server" ID="Button4" OnClick="generateSubstances"  
              Text="Generar" style="height: 26px" /></td>
     </tr>
     <tr>
      <td>Generar Indice de Terceras</td>
      <td><asp:Button runat="server" ID="Button5" OnClick="generateThird"  Text="Generar" /></td>
     </tr>
     <tr>
      <td>Generar Indice de Usos</td>
      <td><asp:Button runat="server" ID="Button6" OnClick="generatesUses"  Text="Generar" /></td>
     </tr>
    </table>    
    
    </div>
    </form>
</body>
</html>
