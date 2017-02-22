<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Indices.aspx.cs" Inherits="Indices" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Índices</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" RenderMode="Block"> 
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
                    <asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="0" DynamicLayout="true" >
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
                    <asp:Label ID="lblSP" runat="server"  Font-Names="Verdana" Font-Size="12px" Width="150px" Font-Bold="False">Índice de Especies</asp:Label>
                </td>
                <td>
                    &nbsp;
                </td>
                <td align="center" style="width: 300px">
                    <asp:Button ID="btnSP" runat="server" Text="Generar" ValidationGroup="Indexes" OnClick="btnSP_Click" />
                   
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
                    <asp:Label ID="lblNC" runat="server"  Font-Names="Verdana" Font-Size="12px" Width="150px" Font-Bold="False">Índice General</asp:Label>
                </td>
                <td>
                    &nbsp;
                </td>
                <td align="center" style="width: 300px">
                    <asp:Button ID="btnIndGeneral" runat="server" onclick="btnIndGeneral_Click" 
                        Text="Button" />
                    |</td>
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
                    <asp:Label ID="Label2" runat="server"  Font-Names="Verdana" Font-Size="12px" Width="150px" Font-Bold="False">Índice de Sustancias</asp:Label>
                </td>
                <td>
                    &nbsp;
                </td>
                <td align="center" style="width: 300px">
                    <asp:Button ID="btnSub" runat="server" Text="Generar" ValidationGroup="Indexes" OnClick="btnIndSubs_Click" />
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
           
            <caption>
                ----
                <tr>
                    <td>
                        <asp:Label ID="Label4" runat="server" Font-Bold="False" Font-Names="Verdana" 
                            Font-Size="12px" Width="150px">Índice Terapeútico</asp:Label>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td align="center" style="width: 300px">
                        <asp:Button ID="Button1" runat="server" OnClick="btnIndThera_Click" 
                            Text="Generar" ValidationGroup="Indexes" />
                    </td>
                </tr>
                <tr>
                    <td colspan="4" style="height:15px;">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td align="center" colspan="3">
                        <asp:Label ID="Label5" runat="server" Font-Names="verdana" Font-Size="14px" 
                            ForeColor="red"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label6" runat="server" Font-Bold="False" Font-Names="Verdana" 
                            Font-Size="12px" Width="150px">Índice Laboratorios</asp:Label>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td align="center" style="width: 300px">
                        <asp:Button ID="btnLabs" runat="server" OnClick="btnLabs_Click" Text="Generar" 
                            ValidationGroup="Indexes" />
                    </td>
                </tr>
                <tr>
                    <td colspan="4" style="height:15px;">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td align="center" colspan="3">
                        <asp:Label ID="Label7" runat="server" Font-Names="verdana" Font-Size="14px" 
                            ForeColor="red"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="4" style="height:15px;">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label8" runat="server" Font-Bold="False" Font-Names="Verdana" 
                            Font-Size="12px" Width="150px">Índice Nutricional</asp:Label>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td align="center" style="width: 300px">
                        <asp:Button ID="btnNutr" runat="server" OnClick="btnNutr_Click" Text="Generar" 
                            ValidationGroup="Indexes" />
                    </td>
                </tr>
                <tr>
                    <td colspan="4" style="height:15px;">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td align="center" colspan="3">
                        <asp:Label ID="Label9" runat="server" Font-Names="verdana" Font-Size="14px" 
                            ForeColor="red"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="left" colspan="1">
                       <asp:Label ID="Label10" runat="server" Font-Bold="False" Font-Names="Verdana" 
                            Font-Size="12px" Width="150px">Índice Vacunacion</asp:Label>
                    
                    </td>
                    <td  align="center" colspan="2">
                        <asp:Button ID="btnVacunacion" runat="server" Text="Generar" 
                            onclick="btnVacunacion_Click" />
                    </td>
                </tr>
                <tr>
                    <td align="center" colspan="3">
                        <asp:Label ID="Label11" runat="server" Font-Names="verdana" Font-Size="14px" 
                            ForeColor="red"></asp:Label>
                    </td>
                </tr>
         <tr>
                    <td colspan="4">
                        <asp:Label runat="server" Font-Names="verdana" ForeColor="#9933ff" Font-Italic="true" Font-Size="25px" Font-Bold="true">INDICES PLANTILLA NUEVA</asp:Label> 
                    </td>
                </tr>
                <caption>
                    <br />
                    <tr>
                        <td align="left" colspan="1">
                            <asp:Label ID="Label12" runat="server" Font-Bold="False" Font-Names="Verdana" Font-Size="12px" Width="150px">Índice Laboratorios </asp:Label>
                        </td>
                        <td align="center" colspan="2">
                            <asp:Button ID="btnLabsNew" runat="server" onclick="btnLabsNew_Click" Text="Generar" />
                        </td>
                    </tr>
                    <tr>
                        <td align="center" colspan="3">
                            <asp:Label ID="Label13" runat="server" Font-Names="verdana" Font-Size="14px" ForeColor="red"></asp:Label>
                        </td>
                    </tr>

                     <tr>
                        <td align="left" colspan="1">
                            <asp:Label ID="Label14" runat="server" Font-Bold="False" Font-Names="Verdana" Font-Size="12px" Width="150px">Índice General </asp:Label>
                        </td>
                        <td align="center" colspan="2">
                            <asp:Button ID="btnIndGeneralNew" runat="server" onclick="btnIndGeneralNew_Click" Text="Generar" style="height: 26px" />
                        </td>
                    </tr>
                    <tr>
                        <td align="center" colspan="3">
                            <asp:Label ID="Label15" runat="server" Font-Names="verdana" Font-Size="14px" ForeColor="red"></asp:Label>
                        </td>
                    </tr>

                    <tr>
                        <td align="left" colspan="1">
                            <asp:Label ID="Label16" runat="server" Font-Bold="False" Font-Names="Verdana" Font-Size="12px" Width="150px">Índice Division Pecuaria </asp:Label>
                        </td>
                        <td align="center" colspan="2">
                            <asp:Button ID="Button2" runat="server" onclick="btnIndGeneralDivPecNew_Click" Text="Generar" style="height: 26px" />
                        </td>
                    </tr>
                    <tr>
                        <td align="center" colspan="3">
                            <asp:Label ID="Label17" runat="server" Font-Names="verdana" Font-Size="14px" ForeColor="red"></asp:Label>
                        </td>
                    </tr>
                     <tr>
                    <td align="left" colspan="1">
                       <asp:Label ID="Label18" runat="server" Font-Bold="False" Font-Names="Verdana" 
                            Font-Size="12px" Width="150px">Índice Vacunacion</asp:Label>
                    
                    </td>
                    <td  align="center" colspan="2">
                        <asp:Button ID="Button3" runat="server" Text="Generar" 
                            onclick="btnVacunacionN_Click" />
                    </td>
                </tr>
                <tr>
                    <td align="center" colspan="3">
                        <asp:Label ID="Label19" runat="server" Font-Names="verdana" Font-Size="14px" 
                            ForeColor="red"></asp:Label>
                    </td>
                </tr>

            <tr>
                <td>
                    <asp:Label ID="Label20" runat="server"  Font-Names="Verdana" Font-Size="12px" Width="150px" Font-Bold="False">Índice de Sustancias</asp:Label>
                </td>
                <td>
                    &nbsp;
                </td>
                <td align="center" style="width: 300px">
                    <asp:Button ID="Button4" runat="server" Text="Generar" ValidationGroup="Indexes" OnClick="btnIndSubsNew_Click" />
                </td>
            </tr>
             <tr>
                <td colspan="3" align="center">
                    <asp:Label ID="Label21" runat="server" Font-Names="verdana" Font-Size="14px" ForeColor="red"></asp:Label>
                </td>
            </tr>
<tr>
                    <td>
                        <asp:Label ID="Label22" runat="server" Font-Bold="False" Font-Names="Verdana" 
                            Font-Size="12px" Width="150px">Índice Terapeútico</asp:Label>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td align="center" style="width: 300px">
                        <asp:Button ID="Button5" runat="server" OnClick="btnIndTheraN_Click" 
                            Text="Generar" ValidationGroup="Indexes" style="height: 26px" />
                    </td>
                </tr>
                <tr>
                  <td colspan="3" align="center">
                    <asp:Label ID="Label23" runat="server" Font-Names="verdana" Font-Size="14px" ForeColor="red"></asp:Label>
                </td>
                </tr>

                </caption>

            </caption>
            
        </table>  
    </ContentTemplate>
    </asp:UpdatePanel>   
    </div>
    </form>
</body>
</html>
