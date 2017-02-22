using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Indices : System.Web.UI.Page
{
    IndLetter generateSP;
    protected void Page_Load(object sender, EventArgs e)
    {
         
    }

    protected void btnGeneraDisco_Click(object sender, EventArgs e)
    {
        int edition = Convert.ToInt32(txtEdicion.Text);
        int tipo = 2;
        int tipoInt = 4;

        ////////////////////////////////// ANUNCIANTES //////////////////////////////////

        //generateSP = new IndLetter(this.Server.MapPath("./HtmlTemplates/Indices/Anunciantes.htm"), @"C:\GUIA61\Disco\Anunciantes");
        //generateSP.getHtmlFilesInd(edition, tipo);
        //this.lblAnunciante.Text = "índice de Anunciantes se generó correctamente";

        //generateSP = new IndLetter(this.Server.MapPath("./HtmlTemplates/Indices/DatosAnunciant.htm"), @"C:\GUIA61\Disco\Anunciantes");
        //generateSP.getHtmlFilesDataClient(edition, tipo);
        //this.lblDatosAnunciante.Text = "datos de anunciantes se generaron correctamente";

        //generateSP = new IndLetter(this.Server.MapPath("./HtmlTemplates/Indices/MarcasAnunciantes.htm"), @"C:\GUIA61\Disco\Marcas");
        //generateSP.getHtmlMarcasClient(edition, tipo);
        //this.lblMarcasAnunciantes.Text = "Marcas de anunciantes se generó correctamente";

        //generateSP = new IndLetter(this.Server.MapPath("./HtmlTemplates/Indices/AnunciantesMarcas.htm"), @"C:\GUIA61\Disco\Marcas\marcas");
        //generateSP.getHtmlClientMarcas(edition, tipo);
        //this.lblAnunciantesMarcas.Text = "Anunciantes por marca se generó correctamente";

        generateSP = new IndLetter(this.Server.MapPath("./HtmlTemplates/Indices/ProductosAnunciantes.htm"), @"C:\GUIA61\Disco\Productos");
        generateSP.getHtmlProductsClients(edition, tipo);
        this.lblProductosAnunciantes.Text = "Productos de anunciantes se generó correctamente";

        ////generateSP = new IndLetter(this.Server.MapPath("./HtmlTemplates/Indices/ProductosHospitalarios.htm"), @"C:\GUIA61\Disco\Productos");
        ////generateSP.getHtmlProductosHospitalarios(edition);
        ////this.prodHosp.Text = "Productos Hospitalarios se generó correctamente";

        //generateSP = new IndLetter(this.Server.MapPath("./HtmlTemplates/Indices/SucursalesAnunciantes.htm"), @"C:\GUIA61\Disco\Anunciantes\sucursal");
        //generateSP.getHtmlSucursalesAnunciantes(edition);
        //this.lblSucursalesAnunciantes.Text = "Sucursales de anunciantes se generó correctamente";


        ////////////////////////////////// ANUNCIANTES INTERNACIONALES //////////////////////////////////


        //generateSP = new IndLetter(this.Server.MapPath("./HtmlTemplates/Indices/Internacional.htm"), @"C:\GUIA61\Disco\internacional");
        //generateSP.getHtmlClientsInternacional(edition, tipoInt);
        //this.lblInternacional.Text = "directorio internacional se generó correctamente";

        //generateSP = new IndLetter(this.Server.MapPath("./HtmlTemplates/Indices/DatosInternacional.htm"), @"C:\GUIA61\Disco\internacional");
        //generateSP.getHtmlDataClientInternacional(edition, tipoInt);
        //this.lblDatosInternacional.Text = "Datos de Clientes internacionales se generó correctamente";


        ////////////////////////////////// ESPECIALIDADES //////////////////////////////////

        //generateSP = new IndLetter(this.Server.MapPath("./HtmlTemplates/Indices/Especialidades.htm"), @"C:\GUIA61\Disco\Especialidades");
        //generateSP.getHtmlSpecialities(edition);
        //this.lblEspecialidades.Text = "El índice de especialidades se generó correctamente";

        //generateSP = new IndLetter(this.Server.MapPath("./HtmlTemplates/Indices/EstadosEspecialidades.htm"), @"C:\GUIA61\Disco\Especialidades");
        //generateSP.getHtmlStatesSpeciality(edition);
        //this.lblEstadoEspecialidades.Text = "Estados por especialidad se generó correctamente";

        //generateSP = new IndLetter(this.Server.MapPath("./HtmlTemplates/Indices/ClientesEspecialidadEstado.htm"), @"C:\GUIA61\Disco\Especialidades");
        //generateSP.getHtmlClientStateSpeciality(edition);//
        //this.lblClientesEstado.Text = "clientes del estado se generó correctamente";

        //generateSP = new IndLetter(this.Server.MapPath("./HtmlTemplates/Indices/ClientesEstadoDatos.htm"), @"C:\GUIA61\Disco\sucursal");
        //generateSP.getHtmlClientByState(edition);////
        //this.lblDatosClientesEstado.Text = "Datos de clientes del estado se generó correctamente";


        //////////////////////////////////  PRODUCTOS Y SERVICIOS  //////////////////////////////////  //

        generateSP = new IndLetter(this.Server.MapPath("./HtmlTemplates/Indices/ProductosServicios.htm"), @"C:\GUIA61\Disco\ProductosServicios");
        generateSP.getHtmlproductServicios(edition);
        this.lblProdsServicios.Text = "Indice de productos y servicios se generó correctamente";

        generateSP = new IndLetter(this.Server.MapPath("./HtmlTemplates/Indices/ProductosServiciosDe.htm"), @"C:\GUIA61\Disco\ProductosServicios\Prods");
        generateSP.getHtmlproductServiciosSubProductos(edition);
        this.lblSubproductosClientes.Text = "Subproductos Clientes se generó correctamente";


        //////////////////////////////////  MARCAS  //////////////////////////////////

        //generateSP = new IndLetter(this.Server.MapPath("./HtmlTemplates/Indices/IndiceMarcas.htm"), @"C:\GUIA61\Disco\marcas");
        //generateSP.getHtmlIndiceMarcas(edition);//
        //this.lblIndiceMArcas.Text = "El índice de marcas se generó correctamente";


        //////////////////////////////////  ZONA GEOGRAFICA  //////////////////////////////////


        //generateSP = new IndLetter(this.Server.MapPath("./HtmlTemplates/Indices/estadosAnun.htm"), @"C:\GUIA61\Disco\Anunciantes\xestado");
        //generateSP.getHtmlIndiceGeografic(edition);
        //this.lblEStadosAnunciantes.Text = "Estados de anunciantes se generó correctamente";

        //generateSP = new IndLetter(this.Server.MapPath("./HtmlTemplates/Indices/AnunciantesXestado.htm"), @"C:\GUIA61\Disco\Anunciantes\xestado");
        //generateSP.getHtmlIndiceGeograficXestado(edition);
        //this.lblAnunciantesEstado.Text = "Anunciantes por estado se genero correctamente";
        ////this.lblfinish.Text = "Todo se genero correctamente";


    }

}