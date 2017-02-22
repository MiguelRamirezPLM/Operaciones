using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Indices : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnLabs_Click(object sender, EventArgs e)
    {
        

        int editionId = Convert.ToInt32(txtEdition.Text);

        IndLetter IndLabs = new IndLetter(this.Server.MapPath("./HtmlTemplates/Indices/clientes.htm"), this.Server.MapPath("./Indices/terceras/"));
        IndLabs.getCompanies(editionId);

        this.Label1.Text = "El índice de laboratorios se generó correctamente";

    }

    protected void btnGenerico_Click(object sender, EventArgs e)
    {

        int editionId = Convert.ToInt32(txtEdition.Text);

        IndLetter IndLabs = new IndLetter(this.Server.MapPath("./HtmlTemplates/Indices/IndiceGenericos.htm"), this.Server.MapPath("./Indices/indices/genericos"));
        IndLabs.getHtmlFilesIndGenericos(editionId,"INDICE DE GENERICOS");

        this.Label2.Text = "El índice de genericos se generó correctamente";

    }
    protected void btnPnombre_Click(object sender, EventArgs e)
    {
        int editionId = Convert.ToInt32(txtEdition.Text);

        IndLetter IndLabs = new IndLetter(this.Server.MapPath("./HtmlTemplates/Indices/IndiceNombreComercial.htm"), this.Server.MapPath("./Indices/indices/nombre_comercial"));
        IndLabs.getHtmlFilesIndGenericosNComercial(editionId, "INDICE DE GENERICOS POR NOMBRE COMERCIAL");

        this.Label3.Text = "El índice de genericos por nombre comercial se generó correctamente";
    }
    protected void btnGeneralimg_Click(object sender, EventArgs e)
    {
        int editionId = Convert.ToInt32(txtEdition.Text);

        IndLetter IndLabs = new IndLetter(this.Server.MapPath("./HtmlTemplates/Indices/IndiceImagenologia.htm"), this.Server.MapPath("./Indices/indices/imagenologia"));
        IndLabs.getHtmlFilesIndGeneralImagenologia(editionId, "INDICE General imagenologia");

        this.Label5.Text = "El índice general imagenologia";

    }
    protected void btnMarcas_Click(object sender, EventArgs e)
    {
        int editionId = Convert.ToInt32(txtEdition.Text);

        IndLetter IndLabs = new IndLetter(this.Server.MapPath("./HtmlTemplates/Indices/IndiceMarcas.htm"), this.Server.MapPath("./Indices/indices/marcas"));
        IndLabs.getHtmlFilesIndMarcas(editionId, "INDICE de marcas");

        this.Label7.Text = "El índice de marcas";
    }
    protected void btnGuia_Click(object sender, EventArgs e)
    {
        int editionId = Convert.ToInt32(txtEdition.Text);

        IndLetter IndLabs = new IndLetter(this.Server.MapPath("./HtmlTemplates/Indices/guia.htm"), this.Server.MapPath("./Indices/guia"));
        IndLabs.getHtmlFilesIndGuia(editionId);

        this.Label9.Text = "El índice de guia";

    }
    protected void btnsec8_Click(object sender, EventArgs e)
    {
        int editionId = Convert.ToInt32(txtEdition.Text);

        IndLetter IndLabs = new IndLetter(this.Server.MapPath("./HtmlTemplates/Indices/seccion_8.htm"), this.Server.MapPath("./Indices/seccion_8"));
        IndLabs.getHtmlFilesIndSeccion8(editionId);

        this.Label11.Text = "El índice de seccion 8 ";
    }

    protected void btnsec1_Click(object sender, EventArgs e)
    {
        int editionId = Convert.ToInt32(txtEdition.Text);

        IndLetter IndLabs = new IndLetter(this.Server.MapPath("./HtmlTemplates/Indices/Secciones.htm"), this.Server.MapPath("./Indices/secciones"));
        IndLabs.getHtmlFilesIndSeccion(editionId, 7, "SECCI&Oacute;N 1 - APARATOS Y EQUIPOS AUTOMATIZADOS, ACCESORIOS, MOBILIARIO Y MATERIALES", "equipos.htm");

        this.Label12.Text = "El índice de seccion 1 ";
    }

    protected void btnsec2_Click(object sender, EventArgs e)
    {
        int editionId = Convert.ToInt32(txtEdition.Text);

        IndLetter IndLabs = new IndLetter(this.Server.MapPath("./HtmlTemplates/Indices/Secciones.htm"), this.Server.MapPath("./Indices/secciones"));
        IndLabs.getHtmlFilesIndSeccion(editionId, 8, "SECCI&Oacute;N 2 - KITS - REACTIVOS", "kitsR.htm");

        this.Label13.Text = "El índice de seccion 2 ";
    }

    protected void btnsec3_Click(object sender, EventArgs e)
    {
        int editionId = Convert.ToInt32(txtEdition.Text);

        IndLetter IndLabs = new IndLetter(this.Server.MapPath("./HtmlTemplates/Indices/Secciones.htm"), this.Server.MapPath("./Indices/secciones"));
        IndLabs.getHtmlFilesIndSeccion(editionId, 9, "SECCI&Oacute;N 3 - BIOLOG&Iacute;A MOLECULAR", "biologia.htm");

        this.Label14.Text = "El índice de seccion 3 ";
    }
    protected void btnsec4_Click(object sender, EventArgs e)
    {
        int editionId = Convert.ToInt32(txtEdition.Text);

        IndLetter IndLabs = new IndLetter(this.Server.MapPath("./HtmlTemplates/Indices/Secciones.htm"), this.Server.MapPath("./Indices/secciones"));
        IndLabs.getHtmlFilesIndSeccion(editionId, 10, "SECCI&Oacute;N 4 - MICROBIOLOG&Iacute;A", "microbiologia.htm");

        this.Label15.Text = "El índice de seccion 4 ";
    }
    protected void btnsec5_Click(object sender, EventArgs e)
    {
        int editionId = Convert.ToInt32(txtEdition.Text);

        IndLetter IndLabs = new IndLetter(this.Server.MapPath("./HtmlTemplates/Indices/Secciones.htm"), this.Server.MapPath("./Indices/secciones"));
        IndLabs.getHtmlFilesIndSeccion(editionId, 11, "SECCI&Oacute;N 5 - TIRAS Y TABLETAS REACTIVAS", "tiras.htm");

        this.Label16.Text = "El índice de seccion 5 ";
    }
    protected void btnsec6_Click(object sender, EventArgs e)
    {
        int editionId = Convert.ToInt32(txtEdition.Text);

        IndLetter IndLabs = new IndLetter(this.Server.MapPath("./HtmlTemplates/Indices/Secciones.htm"), this.Server.MapPath("./Indices/secciones"));
        IndLabs.getHtmlFilesIndSeccion(editionId, 12, "SECCI&Oacute;N 6 - INFORM&Aacute;TICA, ASESOR&Iacute;A, CONSULTOR&Iacute;A Y CONTROL DE CALIDAD", "informatica.htm");

        this.Label17.Text = "El índice de seccion 6 ";

    }
    protected void btnsec7_Click(object sender, EventArgs e)
    {

        int editionId = Convert.ToInt32(txtEdition.Text);

        IndLetter IndLabs = new IndLetter(this.Server.MapPath("./HtmlTemplates/Indices/Secciones.htm"), this.Server.MapPath("./Indices/secciones"));
        IndLabs.getHtmlFilesIndSeccion(editionId, 13, "SECCI&Oacute;N 7 - IMAGENOLOG&Iacute;A-RADIODIAGN&Oacute;STICO", "imagenologia.htm");

        this.Label18.Text = "El índice de seccion 7 ";
    }
    protected void txtEdition_TextChanged(object sender, EventArgs e)
    {

    }
}