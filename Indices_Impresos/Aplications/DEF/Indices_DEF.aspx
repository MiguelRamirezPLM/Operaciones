<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Indices_DEF.aspx.cs" Inherits="Indices_DEF" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<script runat="server">

</script>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Página sin título</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
         <table width="100%" cellpadding="1" cellspacing="1">
            <thead>
                <tr>
                    <td colspan="4">
                        <asp:Label ID="lblIndexes" runat="server" Font-Names="verdana" Font-Size="18px" >Sección de Índices</asp:Label>
                    </td>
                </tr>
            </thead>
            <tr>
                <td colspan="4" style="height:15px;">
                    &nbsp;
                </td>
            </tr>
            
             <tr>
               <td style="width:300px;">
                    <asp:Label ID="lblEdition" runat="server" Font-Names="Verdana" Font-Size="12px" Width="150px" Font-Bold="False">Edición</asp:Label>
               </td>
               <td>
                   &nbsp;
               </td>
               <td align="center" style="width: 300px">
                    <asp:TextBox ID="txtEdition" runat="server" Width="150px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" Display="Dynamic" 
                    ForeColor="red" ValidationGroup="Indexes" ControlToValidate="txtEdition" SetFocusOnError="true" ></asp:RequiredFieldValidator>
                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers" TargetControlID="txtEdition" >
                    </cc1:FilteredTextBoxExtender>
                   
               </td>
               <td rowspan="10" style="width:60%">
                    <asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="0" DynamicLayout="true" AssociatedUpdatePanelID="UpdatePanel1" Visible="true">
                    <ProgressTemplate>
                        <div>
                            <img src="img/progress1.gif" alt="" />
                            <asp:Label ID="lblProgress" runat="server" Font-Names="Verdana" Font-Size="14" Font-Bold="true">Generando...</asp:Label>
                        </div>
                    </ProgressTemplate>  
                    </asp:UpdateProgress>
               </td>       
            </tr>
            
             <tr>
                <td colspan="4" style="height:15px;">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblSP" runat="server"  Font-Names="Verdana" Font-Size="12px" Width="150px" Font-Bold="False">Generar Archivos de Productos</asp:Label>
                </td>
                <td>
                    &nbsp;
                </td>
                <td align="center" style="width: 300px">
                    <asp:Button ID="btnSP" runat="server" Text="Generar" ValidationGroup="Indexes" OnClick="btnSP_Click"  />
                   
                </td>
            </tr>
           
           <tr>
                <td colspan="4" style="height:15px;">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="3" align="center">
                    <asp:Label ID="lblMessage" runat="server" Font-Names="verdana" Font-Size="14px" ForeColor="red"></asp:Label>
                </td>
            </tr>
           
              <tr>
                <td>
                    <asp:Label ID="lblNC" runat="server"  Font-Names="Verdana" Font-Size="12px" Width="150px" Font-Bold="False">Índice de Sustancias</asp:Label>
                </td>
                <td>
                    &nbsp;
                </td>
                <td align="center" style="width: 300px">
                    <asp:Button ID="btnNC" runat="server" Text="Generar" ValidationGroup="Indexes" OnClick="btnNC_Click" />
                </td>
            </tr>
            <tr>
                <td colspan="4" style="height:15px;">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="3" align="center">
                    <asp:Label ID="Label1" runat="server" Font-Names="verdana" Font-Size="14px" ForeColor="red"></asp:Label>
                </td>
            </tr>
           <tr>
                <td>
                    <asp:Label ID="Label2" runat="server"  Font-Names="Verdana" Font-Size="12px" Width="150px" Font-Bold="False">Índice de Indicaciones</asp:Label>
                </td>
                <td>
                    &nbsp;
                </td>
                <td align="center" style="width: 300px">
                    <asp:Button ID="btnSub" runat="server" Text="Generar" ValidationGroup="Indexes" OnClick="btnSub_Click"  />
                </td>
            </tr>
           <tr>
                <td colspan="4" style="height:15px;">
                    &nbsp;
                </td>
            </tr>
             <tr>
                <td colspan="3" align="center">
                    <asp:Label ID="Label3" runat="server" Font-Names="verdana" Font-Size="14px" ForeColor="red"></asp:Label>
                </td>
            </tr>
           

           <tr>
                <td>
                    <asp:Label ID="Label4" runat="server"  Font-Names="Verdana" Font-Size="12px" Width="150px" Font-Bold="False">SIDEF</asp:Label>
                </td>
                <td>
                    &nbsp;
                </td>
                <td align="center" style="width: 300px">
                    <asp:Button ID="Button1" runat="server" Text="Generar" ValidationGroup="Indexes" OnClick="Button1_Click"  />
                </td>
            </tr>
           <tr>
                <td colspan="4" style="height:15px;">
                    &nbsp;
                </td>
            </tr>
             <tr>
                <td colspan="3" align="center">
                    <asp:Label ID="Label5" runat="server" Font-Names="verdana" Font-Size="14px" ForeColor="red"></asp:Label>
                </td>
            </tr>
           
           <tr>
                <td>
                    <asp:Label ID="Label6" runat="server"  Font-Names="Verdana" Font-Size="12px" Width="150px" Font-Bold="False">Índice Laboratorios</asp:Label>
                </td>
                <td>
                    &nbsp;
                </td>
                <td align="center" style="width: 300px">
                    <asp:Button ID="btnLabs" runat="server" Text="Generar" ValidationGroup="Indexes" OnClick="btnLabs_Click" />
                </td>
            </tr>
           <tr>
                <td colspan="4" style="height:15px;">
                    &nbsp;
                </td>
            </tr>
             <tr>
                <td colspan="3" align="center">
                    <asp:Label ID="Label7" runat="server" Font-Names="verdana" Font-Size="14px" ForeColor="red"></asp:Label>
                </td>
            </tr>

           <tr>
                <td>
                    <asp:Label ID="Label8" runat="server"  Font-Names="Verdana" Font-Size="12px" Width="150px" Font-Bold="False">Índice Marcas</asp:Label>
                </td>
                <td>
                    &nbsp;
                </td>
                <td align="center" style="width: 300px">
                    <asp:Button ID="btnNutr" runat="server" Text="Generar" ValidationGroup="Indexes" OnClick="btnNutr_Click" />
                </td>
            </tr>
           <tr>
                <td colspan="4" style="height:15px;">
                    &nbsp;
                </td>
            </tr>
             <tr>
                <td colspan="3" align="center">
                    <asp:Label ID="Label9" runat="server" Font-Names="verdana" Font-Size="14px" ForeColor="red"></asp:Label>
                </td>
            </tr> 
              <tr>
                <td >Productos por Rubros <br />
                  
                </td>
                <td>
                    &nbsp;
                </td>
                <td align="center" style="width: 300px">
                    <asp:Button ID="Button2" runat="server" Text="Generar" ValidationGroup="Indexes" OnClick="btnpharmaform" />
                </td>
            </tr>
             <tr>
                <td colspan="3" align="center"> 
               <asp:Label ID="Label10" runat="server" Font-Names="verdana" Font-Size="14px" ForeColor="red"></asp:Label>
                </td>
            </tr>
            <tr>
            <td>
            &nbsp;
            </td>
            </tr>
             <tr>
                <td >  Repositorio Común <br />
                
                                </td>
                <td>
                    &nbsp;
                </td>
                <td align="center" style="width: 300px">
                    <asp:Button ID="Button3" runat="server" Text="Generar" ValidationGroup="Indexes" OnClick="btnRepComun" />
                </td>
            </tr>
            <tr>
                <td colspan="3" align="center">  
                <asp:Label ID="Label11" runat="server" Font-Names="verdana" Font-Size="14px" ForeColor="red"></asp:Label>
                </td>
            </tr>
                     
        </table>  
        </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
