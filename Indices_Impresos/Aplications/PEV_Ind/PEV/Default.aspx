<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="PEV_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
<form id="form1" runat="server">
        <div style="text-align:center;">
            <br />
       <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Names="Verdana" Font-Size="X-Large" Font-Strikeout="False" style ="text-align:center "
            ForeColor="#194760">INDICES PEV</asp:Label> 
        <br /><br />
    </div>
            <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
 
        <asp:UpdatePanel ID="UpdatePanel1"  runat="server" UpdateMode="Conditional" RenderMode="Block"> 
        <ContentTemplate >
      <table >
     <tr>
      <td class="auto-style2" >
       Edicion: 
      </td>
      <td class="auto-style1">
       <asp:TextBox runat="server" ID="txtEdition"></asp:TextBox>
          <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" Display="Dynamic" 
                    ForeColor="red" ValidationGroup="Indexes" ControlToValidate="txtEdition" SetFocusOnError="true" ></asp:RequiredFieldValidator>

           <asp:TextBox runat="server" ID="txtTablaThera"></asp:TextBox>
          <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*" Display="Dynamic" 
                    ForeColor="red" ValidationGroup="IndexesATC" ControlToValidate="txtEdition" SetFocusOnError="true" ></asp:RequiredFieldValidator>

      </td>  
 
     </tr>
 <tr>
     <td colspan="2" style="text-align:center">
                    <asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="0" DynamicLayout="true" >
                    <ProgressTemplate>
                        <div   ">
                            <img src="../img/progress.gif" alt=""  width="10%" height ="10%"  />
                            <br /><asp:Label ID="lblProgress" runat="server" Font-Names="Verdana" Font-Size="14" Font-Bold="true">Generando...</asp:Label>
                        </div>
                    </ProgressTemplate>  
                    </asp:UpdateProgress>
                   

     </td>
 </tr>
     <tr>
      <td class="auto-style9">
           Indice de General </td>
      <td><asp:Button runat="server" ID="btnclick" OnClick="generateGenProds"  Text="Generar" ValidationGroup="Indexes" /></td>
     </tr>
  <tr>
      <td class="auto-style11">Indice de Sustancias</td>
      <td class="auto-style12"><asp:Button runat="server" ID="btnActivSubs" OnClick="generateActiveSubstances"  Text="Generar" ValidationGroup="Indexes" /></td>
     </tr>
       <tr>
      <td class="auto-style9">Indice Terapeutico </td>
      <td><asp:Button runat="server" ID="btnATC" OnClick="generateATC"  Text="Generar" ValidationGroup="IndexesATC" /></td>
     </tr> 
      <tr>
      <td class="auto-style9">Indice de Vacunacion</td>
      <td><asp:Button runat="server" ID="btnVac" OnClick="generateVac"  Text="Generar" ValidationGroup="Indexes" /></td>
     </tr>

           <tr>
      <td class="auto-style9">Indice ATC Nutricional </td>
      <td><asp:Button runat="server" ID="btnATCN" OnClick="generateATCN"  Text="Generar ATC" ValidationGroup="IndexesATC" /></td>

     </tr>

          
           <tr>
      <td class="auto-style9">Indice ATC Centroamerica </td>
      <td><asp:Button runat="server" ID="btnATCC" OnClick="generateATCC"  Text="Generar ATC" ValidationGroup="IndexesATC" /></td>

     </tr>


 <%--   <tr>
      <td class="auto-style9">Indice Medicamentos Hospitalarios</td>
      <td><asp:Button runat="server" ID="btnMH" OnClick="generateMH"  Text="Generar" ValidationGroup="Indexes" /></td>
     </tr>
    <tr>
      <td class="auto-style9">Indice Anunciantes Internacionales</td>
      <td><asp:Button runat="server" ID="btnInter" OnClick="generateIter"  Text="Generar" ValidationGroup="Indexes" /></td>
     </tr>

        <tr>
      <td class="auto-style9">Indice Carpetas </td>
      <td><asp:Button runat="server" ID="btnCarpetas" OnClick="generateCarpetas"  Text="Generar" ValidationGroup="Indexes" /></td>
     </tr>

          
        <tr>
      <td class="auto-style9">Indice Carpetas Ventas </td>
      <td><asp:Button runat="server" ID="btnCarpetasV" OnClick="generateCarpetasV"  Text="Generar" ValidationGroup="Indexes" /></td>
     </tr>--%>
      
    </table>    
    </ContentTemplate>
    </asp:UpdatePanel>   

    </form>
</body>
</html>
