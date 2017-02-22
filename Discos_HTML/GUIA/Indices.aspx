<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Indices.aspx.cs" Inherits="Indices" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .style1
        {
            width: 165px;
        }
        .style2
        {
            width: 469px;
            font-family:Verdana;
            font-size:12px;
        }

        .etiqueta
        {
            font-family:Verdana;
            font-size:12px;
            color:red;
        }
        .auto-style1 {
            width: 469px;
            font-family: Verdana;
            font-size: 12px;
            height: 69px;
        }
        .auto-style2 {
            height: 69px;
            width: 831px;
        }
        .auto-style3 {
            width: 831px;
        }
        .centrar
        {
            align-content:center;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div style="text-align:center;">
    <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Names="Verdana" Font-Size="X-Large" Font-Strikeout="False" style ="text-align:center "
            ForeColor="#194760">INDICES DISCO GUIA DE PROVEEDORES</asp:Label>   
    </div>
        <br />
        <div  class="centrar"   style=" width:100%;">
            
    <table style="width:80%; align-content:center; " >
        <tr>
            <td class="auto-style1" align="center">
               <b>Edicion:</b>&nbsp;&nbsp;&nbsp;&nbsp; <asp:TextBox ID="txtEdicion" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" Display="Dynamic" 
                    ForeColor="red" ValidationGroup="Indexes" ControlToValidate="txtEdicion" SetFocusOnError="true" ></asp:RequiredFieldValidator>
               </td>
            <td class="auto-style2">
                <asp:Button ID="btnGeneraDisco" runat="server" onclick="btnGeneraDisco_Click" 
                    Text="Generar Indices de Disco de Guia" ValidationGroup="Indexes" />
               
             </td>
        </tr>
        <tr>
           <td class="style2">
                <b>LISTADO DE CLIENTES ANUNCIANTES</b>
            </td>
            <td class="auto-style3">
                <asp:Label ID="lblAnunciante" runat="server" CssClass="etiqueta"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="style2">
                 &nbsp;&nbsp;- Datos de Anunciante
            </td>
            <td class="auto-style3">
                <asp:Label ID="lblDatosAnunciante" runat="server" CssClass="etiqueta"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="style2">
                &nbsp;&nbsp;- Lista de marcas del Anunciante
            </td>
            <td class="auto-style3">
                <asp:Label ID="lblMarcasAnunciantes" runat="server" CssClass="etiqueta"></asp:Label>
            </td>
        </tr>
           <tr>
            <td class="style2">
                &nbsp;&nbsp;- Detalle de las marcas del Anunciante
            </td>
            <td class="auto-style3">
                <asp:Label ID="lblAnunciantesMarcas" runat="server" CssClass="etiqueta"></asp:Label>
               </td>
        </tr>
           <tr>
            <td class="style2">
                &nbsp;&nbsp;- Listado de Productos del Anunciante
            </td>
            <td class="auto-style3">
                <asp:Label ID="lblProductosAnunciantes" runat="server" CssClass="etiqueta"></asp:Label>
               </td>
        </tr>
           <tr>
            <td class="style2">
                &nbsp;&nbsp;- Detalle de los Productos Hospitalarios de los Anunciantes
            </td>
            <td class="auto-style3">
                <asp:Label ID="prodHosp" runat="server" CssClass="etiqueta"></asp:Label>
               </td>
        </tr>  
        
        <tr>
            <td class="style2">
                &nbsp;&nbsp;- Detalle de las Sucursales de los  Anunciante
            </td>
            <td class="auto-style3">
                <asp:Label ID="lblSucursalesAnunciantes" runat="server" CssClass="etiqueta"></asp:Label>
             </td>
        </tr>
         <tr>
            <td class="style2">
               <b>LISTADO DE CLIENTES INTERNACIONALES</b>
            </td>
            <td class="auto-style3">
                <asp:Label ID="lblInternacional" runat="server" CssClass="etiqueta"></asp:Label>
            </td>
        </tr>   
        <tr>
            <td class="style2">
                &nbsp;&nbsp;- Detalle de los Clientes Internacionales
            </td>
            <td class="auto-style3">
                <asp:Label ID="lblDatosInternacional" runat="server" CssClass="etiqueta"></asp:Label>
            </td>
        </tr>   
        <tr>            
            <td class="style2">
                <b>LISTADO DE ESPECIALIDADES</b>
            </td>
            <td class="auto-style3">
                <asp:Label ID="lblEspecialidades" runat="server" CssClass="etiqueta"></asp:Label>
            </td>
        </tr>   
        <tr>
            <td class="style2">
                &nbsp;&nbsp;- Lista de estados por Especialidad
            </td>
            <td class="auto-style3">
                <asp:Label ID="lblEstadoEspecialidades" runat="server" CssClass="etiqueta"></asp:Label>
            </td>
        </tr>   
        <tr>
            <td class="style2">
               &nbsp;&nbsp;- Lista de Clientes por estado de la Especialidad
            </td>
            <td class="auto-style3">
                <asp:Label ID="lblClientesEstado" runat="server" CssClass="etiqueta"></asp:Label>
            </td>
        </tr>   
        <tr>
            <td class="style2">
                &nbsp;&nbsp;- Detalle  de Clientes por estado de la Especialidad
            </td>
            <td class="auto-style3">
                <asp:Label ID="lblDatosClientesEstado" runat="server" CssClass="etiqueta"></asp:Label>
            </td>
        </tr>   
        <tr>
            <td class="style2">
               <b>LISTADO DE PRODUCTOS Y SERVICOS</b>
            </td>
            <td class="auto-style3">
                <asp:Label ID="lblProdsServicios" runat="server" CssClass="etiqueta"></asp:Label>
            </td>
        </tr>
         
         <tr>
            <td class="style2">
                &nbsp;&nbsp;- Lista de SubProductos con sus clientes
            </td>
            <td class="auto-style3">
                <asp:Label ID="lblSubproductosClientes" runat="server" CssClass="etiqueta"></asp:Label>
             </td>
        </tr>
         <tr>
            <td class="style2">
               <b>LISTADO DE MARCAS</b>
            </td>
            <td class="auto-style3">
                <asp:Label ID="lblIndiceMArcas" runat="server" CssClass="etiqueta"></asp:Label>
             </td>
        </tr>
         <tr>
            <td class="style2">
                <b>LISTADO DE ESTADOS DE ANUNCIANTES</b>
            </td>
            <td class="auto-style3">
                <asp:Label ID="lblEStadosAnunciantes" runat="server" CssClass="etiqueta"></asp:Label>
             </td>
        </tr>
         <tr>
            <td class="style2">
                &nbsp;&nbsp;- Lista de anunciantes por Estado
            </td>
            <td class="auto-style3">
                <asp:Label ID="lblAnunciantesEstado" runat="server" CssClass="etiqueta"></asp:Label>
             </td>
        </tr>
         
    </table>
        </center>    </div>
    </form>
</body>
</html>
