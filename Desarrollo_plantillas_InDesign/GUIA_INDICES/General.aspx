<%@ Page Language="C#" AutoEventWireup="true" CodeFile="General.aspx.cs" Inherits="General" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            height: 53px;
        }
        .auto-style2 {
            height: 53px;
            width: 237px;
             font-family:Verdana;
            font-size:12px;
            font-weight:bold;
        }
        .auto-style4 {
            width: 237px;
            height: 35px; 
            font-family:Verdana;
            font-size:12px;
        }
        .auto-style5 {
            height: 35px;
        }
        .auto-style9 {
            width: 260px;
             font-family:Verdana;
            font-size:12px;
        }
        .auto-style11 {
            width: 260px;
            height: 30px;
            font-family:Verdana;
            font-size:12px;
        }
        .auto-style12 {
            height: 30px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div style="text-align:center;">
            <br />
       <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Names="Verdana" Font-Size="X-Large" Font-Strikeout="False" style ="text-align:center "
            ForeColor="#194760">INDICES GUIA DE PROVEEDORES</asp:Label> 
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

      </td>  
 
     </tr>
 <tr>
     <td colspan="2" style="text-align:center">
                    <asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="0" DynamicLayout="true" >
                    <ProgressTemplate>
                        <div style="align-content:center; text-align: center;">
                            <img src="img/progress1.gif" alt=""  width="30%" height ="30%"  />
                            <br /><asp:Label ID="lblProgress" runat="server" Font-Names="Verdana" Font-Size="14" Font-Bold="true">Generando...</asp:Label>
                        </div>
                    </ProgressTemplate>  
                    </asp:UpdateProgress>
                   

     </td>
 </tr>
     <tr>
      <td class="auto-style9">
           Indice de Anunciantes </td>
      <td><asp:Button runat="server" ID="btnclick" OnClick="generateAnunciantes"  Text="Generar" ValidationGroup="Indexes" /></td>
     </tr>
    <tr>
      <td class="auto-style11">Indice de Marcas</td>
      <td class="auto-style12"><asp:Button runat="server" ID="btnMarcas" OnClick="generateMarcas"  Text="Generar" ValidationGroup="Indexes" /></td>
     </tr>
       <tr>
      <td class="auto-style9">Indice de Productos y Servicios </td>
      <td><asp:Button runat="server" ID="btnPyS" OnClick="generatePyS"  Text="Generar" ValidationGroup="Indexes" /></td>
     </tr>
      <tr>
      <td class="auto-style4">Indice de Especialidades</td>
      <td class="auto-style5"><asp:dropdownlist id ="Speciality" runat ="server" Font-Size="11px">
                  <asp:listitem value ="8">ALIMENTACIÓN </asp:listitem >
                    <asp:listitem value ="9">AMBULANCIAS, Equipos, Servicios </asp:listitem >
                    <asp:listitem value ="40">ANÁLISIS y Estudios Clínicos, Laboratorios </asp:listitem >
                    <asp:listitem value ="10">ANESTESIOLOGÍA, Aparatos y Equipos </asp:listitem >
                    <asp:listitem value ="42">APARATOS Y EQUIPOS MÉDICOS PARA CONSULTORIOS, VENTA O ALQUILER A CLÍNICAS, HOSPITALES, LABORATORIOS Y  DOMICILIARIO </asp:listitem >
                    <asp:listitem value ="11">APARATOS, EQUIPOS Y MOBILIARIO para Consultorios, Clínicas, Hospitales y Laboratorios </asp:listitem >
                    <asp:listitem value ="43">ASESORÍAS LEGALES </asp:listitem >
                    <asp:listitem value ="12">ASOCIACIONES, Colegios, Consejos, Federaciones y Sociedades Médicas </asp:listitem >
                    <asp:listitem value ="44">ASOCIACIONES, COLEGIOS, FEDERACIONES Y SOCIEDADES MÉDICAS, INSTITUCIONES Y FUNDACIONES </asp:listitem >
                    <asp:listitem value ="13">COMPUTACIÓN, Equipos, Programas Médicos y Servicios </asp:listitem >
                    <asp:listitem value ="14">CURACIÓN, Materiales </asp:listitem >
                    <asp:listitem value ="45">CURACIÓN, MATERIALES Y SUMINISTROS MÉDICO -QUIRÚRGICOS </asp:listitem >
                    <asp:listitem value ="15">DESINFECCIÓN Y ESTERILIZACIÓN, Aparatos, Equipos y Materiales </asp:listitem >
                    <asp:listitem value ="16">DIAGNÓSTICO POR IMAGEN, APARATOS, Equipos y Materiales </asp:listitem >
                    <asp:listitem value ="39">DIAGNÓSTICO POR IMAGEN, SERVICIOS </asp:listitem >
                    <asp:listitem value ="17">EMPRESAS DE SERVICIOS ESPECIALIZADOS </asp:listitem >
                    <asp:listitem value ="46">EMPRESAS DE SERVICIOS ESPECIALIZADOS ASEO, COMUNICACIÓN, SERVICIOS GENERALES Y MANTENIMIENTO </asp:listitem >
                    <asp:listitem value ="18">ENFERMERÍA, Servicios </asp:listitem >
                    <asp:listitem value ="19">ENSEÑANZA MÉDICA (Audiovisuales, Material, Equipo, Fotografías, Películas, Videos) </asp:listitem >
                    <asp:listitem value ="20">EQUIPOS, Kits, Reactivos, Agentes para Diagnóstico y Materiales para Laboratorio </asp:listitem >
                    <asp:listitem value ="21">FOTOGRÁFICO, Materiales </asp:listitem >
                    <asp:listitem value ="47">GASES MEDICINALES, EQUIPOS, INSTALACIONES Y SERVICIOS DE OXÍGENO </asp:listitem >
                    <asp:listitem value ="22">GASES MEDICINALES, Instalaciones y Servicios de Oxígeno </asp:listitem >
                    <asp:listitem value ="23">HOSPITALES, CLÍNICAS Y SANATORIOS </asp:listitem >
                    <asp:listitem value ="24">INHALOTERAPIA, Aparatos y Servicios </asp:listitem >
                    <asp:listitem value ="25">INSTRUMENTAL y Material Quirúrgico </asp:listitem >
                    <asp:listitem value ="26">INTERCOMUNICACIÓN, Aparatos y Servicios (Fax, Teléfono, Celular, Vip) </asp:listitem >
                    <asp:listitem value ="37">LIBROS Y REVISTAS MÉDICAS (Librerías, Editoriales, Distribuidores) </asp:listitem >
                    <asp:listitem value ="48">MAQUILAS </asp:listitem >
                    <asp:listitem value ="27">MAQUINARIA, Envases, Empaques y Materiales para la Industria Químico-Farmacéutica </asp:listitem >
                    <asp:listitem value ="38">MATERIAS PRIMAS Químico-Farmacéuticas </asp:listitem >
                    <asp:listitem value ="28">MEDICAMENTOS Hospitalarios </asp:listitem >
                    <asp:listitem value ="29">MEDICAMENTOS, Distribución </asp:listitem >
                    <asp:listitem value ="49">MEDICINA ALTERNATIVA </asp:listitem >
                    <asp:listitem value ="50">MOBILIARIO HOSPITALARIO </asp:listitem >
                    <asp:listitem value ="30">NEFROLOGÍA Y UROLOGÍA, Aparatos y Equipos </asp:listitem >
                    <asp:listitem value ="31">ODONTOLOGÍA, Aparatos y Equipos </asp:listitem >
                    <asp:listitem value ="51">ODONTOLOGÍA, SUMINITROS, EQUIPOS Y MANTENIMIENTO </asp:listitem >
                    <asp:listitem value ="32">OFTALMOLOGÍA, Aparatos y Equipos </asp:listitem >
                    <asp:listitem value ="33">ORTOPEDIA, Aparatos, Equipos, Accesorios, Rehabilitación </asp:listitem >
                    <asp:listitem value ="52">OTRAS ESPECIALIDADES MÉDICAS </asp:listitem >
                    <asp:listitem value ="53">PRINCIPIOS ACTIVOS FARMACÉUTICOS </asp:listitem >
                    <asp:listitem value ="34">REPARACIONES, Mantenimiento, Accesorios y Refacciones para Equipo Médico </asp:listitem >
                    <asp:listitem value ="54">ROPA CLÍNICA, VENTA, ALQUILER, DESECHABLES, LAVADO </asp:listitem >
                    <asp:listitem value ="35">SANGRE, Equipos </asp:listitem >
                    <asp:listitem value ="55">SANGRE, EQUIPOS E INSUMOS </asp:listitem >
                    <asp:listitem value ="36">VESTIMENTA CLÍNICA, Venta, Alquiler, Desechables, Lavado </asp:listitem >
                  </asp:dropdownlist >
         
           <asp:Button runat="server" ID="btnEspecialidades" OnClick="generateEspecialidades"  Text="Generar" ValidationGroup="Indexes"  /></td>
     </tr>
    <tr>
      <td class="auto-style9">Indice de Consulta Rapida</td>
      <td><asp:Button runat="server" ID="btnCR" OnClick="generateCR"  Text="Generar" ValidationGroup="Indexes" /></td>
     </tr>
    <tr>
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
     </tr>

<tr>
      <td colspan="2" align="center"> <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Names="Verdana" Font-Size="X-Large" Font-Strikeout="False" style ="text-align:center "
            ForeColor="#194760">INDICES GUIA DE PROVEEDORES</asp:Label> </td>
      
     </tr>

      <tr>
      <td class="auto-style9">Indice Secciones de Productos y Servicios </td>
      <td><asp:Button runat="server" ID="btnSPYS" OnClick="generateSPyS"  Text="Generar" ValidationGroup="Indexes" /></td>
     </tr>

     <tr>
      <td class="auto-style9">Indice Clientes ANUNCIANTES </td>
      <td><asp:Button runat="server" ID="btnCAnunciantesN" OnClick="generateNClientAnun"  Text="Generar" ValidationGroup="Indexes" /></td>
     </tr>

    </table>    
    </ContentTemplate>
    </asp:UpdatePanel>   

    </form>
</body>
</html>

