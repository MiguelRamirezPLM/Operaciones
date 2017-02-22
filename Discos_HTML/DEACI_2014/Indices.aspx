<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Indices.aspx.cs" Inherits="Indices" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Índices</title>
    <style type="text/css">
        .auto-style1 {
            width: 243px;
        }
        .auto-style3 {
            width: 355px;
        }
        .auto-style4 {
            width: 170px;
        }
    </style>
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
               <td class="auto-style1">
                    <asp:Label ID="lblEdition" runat="server" Font-Names="Verdana" Font-Size="12px" Width="150px" Font-Bold="False">Edición</asp:Label>
               </td>
              
               <td align="center"  colspan="2" class="auto-style4">
                    <asp:TextBox ID="txtEdition" runat="server" Width="150px" OnTextChanged="txtEdition_TextChanged"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" Display="Dynamic" 
                    ForeColor="red" ValidationGroup="Indexes" ControlToValidate="txtEdition" SetFocusOnError="true" ></asp:RequiredFieldValidator>
                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers" TargetControlID="txtEdition" >
                    </cc1:FilteredTextBoxExtender>
               </td>
               <td rowspan="15" style="width:40%">
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
            
            
            <caption style="margin-top: 19px">
                ----
               
               
                <tr>
                    <td class="auto-style1">
                        <asp:Label ID="Label6" runat="server" Font-Bold="False" Font-Names="Verdana" 
                            Font-Size="12px" Width="150px">Índice Laboratorios</asp:Label>
                    </td>
                    
                    <td align="center" colspan="2" class="auto-style4" >
                        <asp:Button ID="btnLabs" runat="server" OnClick="btnLabs_Click" Text="Generar" 
                            ValidationGroup="Indexes" />
                    </td>
                </tr>
                <tr>
                    <td align="center" colspan="3">
                        <asp:Label ID="Label1" runat="server" Font-Names="verdana" Font-Size="14px" 
                            ForeColor="red"></asp:Label>
                    </td>
                </tr>
               
                <tr>
                    <td class="auto-style1">
                        <asp:Label ID="Label8" runat="server" Font-Bold="False" Font-Names="Verdana" 
                            Font-Size="12px" Width="150px">Índice GENERICO</asp:Label>
                    </td>
                    
                    <td align="center" colspan="2" class="auto-style4">
                        <asp:Button ID="btnGenerico" runat="server" OnClick="btnGenerico_Click" Text="Generar" 
                            ValidationGroup="Indexes" />
                    </td>
                </tr>
                
                <tr>
                    <td align="center" colspan="3">
                        <asp:Label ID="Label2" runat="server" Font-Names="verdana" Font-Size="14px" 
                            ForeColor="red"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="left" colspan="1" class="auto-style1">
                       <asp:Label ID="Label10" runat="server" Font-Bold="False" Font-Names="Verdana" 
                            Font-Size="12px" Width="150px">Índice POR NOMBRE</asp:Label>
                    
                    </td>
                    <td  align="center" colspan="2" class="auto-style4">
                        <asp:Button ID="btnPnombre" runat="server" Text="Generar" 
                            onclick="btnPnombre_Click" />
                    </td>
                </tr>
               
                <tr>
                    <td align="center" colspan="3">
                        <asp:Label ID="Label3" runat="server" Font-Names="verdana" Font-Size="14px" 
                            ForeColor="red"></asp:Label>
                    </td>
                </tr>

                 <tr>
                    <td align="left" colspan="1" class="auto-style1">
                       <asp:Label ID="Label4" runat="server" Font-Bold="False" Font-Names="Verdana" 
                            Font-Size="12px" Width="214px">Índice GENERAL IMAGENOLOGÍA</asp:Label>
                    
                    </td>
                    <td  align="center" colspan="2" class="auto-style4">
                        <asp:Button ID="btnGeneralimg" runat="server" Text="Generar" 
                            onclick="btnGeneralimg_Click" />
                    </td>
                </tr>
                 
                <tr>
                    <td align="center" colspan="3">
                        <asp:Label ID="Label5" runat="server" Font-Names="verdana" Font-Size="14px" 
                            ForeColor="red"></asp:Label>
                    </td>
                </tr>
              
                 <tr>
                    <td align="left" colspan="1" class="auto-style1">
                       <asp:Label ID="lblmarcas" runat="server" Font-Bold="False" Font-Names="Verdana" 
                            Font-Size="12px" Width="214px">Índice de marcas</asp:Label>
                    
                    </td>
                    <td  align="center" colspan="2" class="auto-style4">
                        <asp:Button ID="btnMarcas" runat="server" Text="Generar" 
                            onclick="btnMarcas_Click" />
                    </td>
                </tr>
                  
                <tr>
                    <td align="center" colspan="3">
                        <asp:Label ID="Label7" runat="server" Font-Names="verdana" Font-Size="14px" 
                            ForeColor="red"></asp:Label>
                    </td>
                </tr>
            
                <tr>
                    <td align="left" colspan="1" class="auto-style1">
                       <asp:Label ID="lblguia" runat="server" Font-Bold="False" Font-Names="Verdana" 
                            Font-Size="12px" Width="214px">Índice de guia </asp:Label>
                    
                    </td>
                    <td  align="center" colspan="2" class="auto-style4">
                        <asp:Button ID="btnGuia" runat="server" Text="Generar" 
                            onclick="btnGuia_Click" />
                    </td>
                </tr>
                  
                <tr>
                    <td align="center" colspan="3">
                        <asp:Label ID="Label9" runat="server" Font-Names="verdana" Font-Size="14px" 
                            ForeColor="red"></asp:Label>
                    </td>
                </tr>

                <tr>
                    <td align="left" colspan="1" class="auto-style1">
                       <asp:Label ID="lbllabs" runat="server" Font-Bold="False" Font-Names="Verdana" 
                            Font-Size="12px" Width="263px">Índice de laboratorios y empresas </asp:Label>
                    
                    </td>
                    <td  align="center" colspan="2" class="auto-style4">
                        <asp:Button ID="btnsec8" runat="server" Text="Generar" 
                            onclick="btnsec8_Click" />
                    </td>
                </tr>
                   
                <tr>
                    <td align="center" colspan="3">
                        <asp:Label ID="Label11" runat="server" Font-Names="verdana" Font-Size="14px" 
                            ForeColor="red"></asp:Label>
                    </td>
                </tr>

                <tr>
                    <td align="left" colspan="1" class="auto-style1">
                       <asp:Label ID="lblSec1" runat="server" Font-Bold="False" Font-Names="Verdana" 
                            Font-Size="12px" Width="263px">Índice de seccion 1 (APARATOS Y EQUIPOS AUTOMATIZADOS) </asp:Label>
                    
                    </td>
                    <td  align="center" colspan="2" class="auto-style4">
                        <asp:Button ID="btnsec1" runat="server" Text="Generar" 
                            onclick="btnsec1_Click" />
                    </td>
                </tr>
                   
                <tr>
                    <td align="center" colspan="3">
                        <asp:Label ID="Label12" runat="server" Font-Names="verdana" Font-Size="14px" 
                            ForeColor="red"></asp:Label>
                    </td>
                </tr>

                 <tr>
                    <td align="left" colspan="1" class="auto-style1">
                       <asp:Label ID="lblsec2" runat="server" Font-Bold="False" Font-Names="Verdana" 
                            Font-Size="12px" Width="263px">Índice de seccion 2 (KITS - REACTIVOS) </asp:Label>
                    
                    </td>
                    <td  align="center" colspan="2" class="auto-style4">
                        <asp:Button ID="btnsec2" runat="server" Text="Generar" 
                            onclick="btnsec2_Click" />
                    </td>
                </tr>
                 
                </tr>
                <tr>
                    <td align="center" colspan="3">
                        <asp:Label ID="Label13" runat="server" Font-Names="verdana" Font-Size="14px" 
                            ForeColor="red"></asp:Label>
                    </td>
                </tr>

                <tr>
                    <td align="left" colspan="1" class="auto-style1">
                       <asp:Label ID="lblsec3" runat="server" Font-Bold="False" Font-Names="Verdana" 
                            Font-Size="12px" Width="263px">Índice de seccion 3 (BIOLOGÍA MOLECULAR) </asp:Label>
                    
                    </td>
                    <td  align="center" colspan="2" class="auto-style4">
                        <asp:Button ID="btnsec3" runat="server" Text="Generar" 
                            onclick="btnsec3_Click" style="height: 26px" />
                    </td>
                </tr>
                  
                <tr>
                    <td align="center" colspan="3">
                        <asp:Label ID="Label14" runat="server" Font-Names="verdana" Font-Size="14px" 
                            ForeColor="red"></asp:Label>
                    </td>
                </tr>

                 <tr>
                    <td align="left" colspan="1" class="auto-style1">
                       <asp:Label ID="lblmb" runat="server" Font-Bold="False" Font-Names="Verdana" 
                            Font-Size="12px" Width="263px">Índice de seccion 4 (MICROBIOLOGÍA) </asp:Label>
                    
                    </td>
                    <td  align="center" colspan="2" class="auto-style4">
                        <asp:Button ID="btnsec4" runat="server" Text="Generar" 
                            onclick="btnsec4_Click" style="height: 26px" />
                    </td>
                </tr>
                 
                <tr>
                    <td align="center" colspan="3">
                        <asp:Label ID="Label15" runat="server" Font-Names="verdana" Font-Size="14px" 
                            ForeColor="red"></asp:Label>
                    </td>
                </tr>

            
                 <tr>
                    <td align="left" colspan="1" class="auto-style1">
                       <asp:Label ID="lbltiras" runat="server" Font-Bold="False" Font-Names="Verdana" 
                            Font-Size="12px" Width="263px">Índice de seccion 5 (TIRAS Y TABLETAS REACTIVAS) </asp:Label>
                    
                    </td>
                    <td  align="center" colspan="2" class="auto-style4">
                        <asp:Button ID="btnsec5" runat="server" Text="Generar" 
                            onclick="btnsec5_Click" style="height: 26px" />
                    </td>
                </tr>
                   
                <tr>
                    <td align="center" colspan="3">
                        <asp:Label ID="Label16" runat="server" Font-Names="verdana" Font-Size="14px" 
                            ForeColor="red"></asp:Label>
                    </td>
                </tr>


                <tr>
                    <td align="left" colspan="1" class="auto-style1">
                       <asp:Label ID="lblinformatica" runat="server" Font-Bold="False" Font-Names="Verdana" 
                            Font-Size="12px" Width="263px">Índice de seccion 6 (INFORMÁTICA, ASESORÍA) </asp:Label>
                    
                    </td>
                    <td  align="center" colspan="2" class="auto-style4">
                        <asp:Button ID="btnsec6" runat="server" Text="Generar" 
                            onclick="btnsec6_Click" style="height: 26px" />
                    </td>
                </tr>
                   
                <tr>
                    <td align="center" colspan="3">
                        <asp:Label ID="Label17" runat="server" Font-Names="verdana" Font-Size="14px" 
                            ForeColor="red"></asp:Label>
                    </td>
                </tr>

                <tr>
                    <td align="left" colspan="1" class="auto-style1">
                       <asp:Label ID="lblimg" runat="server" Font-Bold="False" Font-Names="Verdana" 
                            Font-Size="12px" Width="263px">Índice de seccion 7 (IMAGENOLOGÍA) </asp:Label>
                    
                    </td>
                    <td  align="center" colspan="2" class="auto-style4">
                        <asp:Button ID="btnsec7" runat="server" Text="Generar" 
                            onclick="btnsec7_Click" style="height: 26px" />
                    </td>
                </tr>
                  
                <tr>
                    <td align="center" colspan="3">
                        <asp:Label ID="Label18" runat="server" Font-Names="verdana" Font-Size="14px" 
                            ForeColor="red"></asp:Label>
                    </td>
                </tr>

            </caption>
            
        </table>  
    </ContentTemplate>
    </asp:UpdatePanel>   
    </div>
    </form>
</body>
</html>