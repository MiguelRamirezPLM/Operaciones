using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class Secciones : System.Web.UI.Page
{
    #region Page Events

    protected void Page_Load(object sender, EventArgs e)
    {
        this.txtEdition.Focus();
    }

    #endregion

    #region Control Events

    protected void btnMP_Click(object sender, EventArgs e)
    {
        this.lblMessage.Text = "";

        int edition = Convert.ToInt32(txtEdition.Text);

        SecLetters generateMP = new SecLetters(this.Server.MapPath("./HtmlTemplates/Secciones/lettersMP.htm"), this.Server.MapPath("./Indices/seccion_MP"));
        generateMP.getHtmlFilesMP(edition, (int)Section.Materias_Primas, "MATERIAS PRIMAS");

        Sections generateSMP = new Sections(this.Server.MapPath("./HtmlTemplates/Secciones/Subseccion.htm"), this.Server.MapPath("./Indices/seccion_MP"));
        generateSMP.getHtmlFilesSections(edition, (int)Section.Materias_Primas, "MATERIAS PRIMAS");

        Sections generateSSMP = new Sections(this.Server.MapPath("./HtmlTemplates/Secciones/Subseccion.htm"), this.Server.MapPath("./Indices/seccion_MP"));
        generateSSMP.getHtmlFilesSubSections(edition, (int)Section.Materias_Primas, "MATERIAS PRIMAS");

        this.lblMessage.Text = "La sección de Materias Primas se generó correctamente";
    }

    protected void btnMaq_Click(object sender, EventArgs e)
    {
        this.lblMessage.Text = "";

        int edition = Convert.ToInt32(txtEdition.Text);

        SecLetters generateMaq = new SecLetters(this.Server.MapPath("./HtmlTemplates/Secciones/Subseccion.htm"), this.Server.MapPath("./Indices/Seccion_Maq"));
        generateMaq.getHtmlFilesSections(edition, (int)Section.Maquinaria, "MAQUINARIA");

        Sections generateSMaq = new Sections(this.Server.MapPath("./HtmlTemplates/Secciones/Subseccion.htm"), this.Server.MapPath("./Indices/Seccion_Maq"));
        generateSMaq.getHtmlFilesSections(edition, (int)Section.Maquinaria, "MAQUINARIA");
        
        this.lblMessage.Text = "La sección de Maquinaria se generó correctamente";
    }

    protected void btnCC_Click(object sender, EventArgs e)
    {
        this.lblMessage.Text = "";

        int edition = Convert.ToInt32(txtEdition.Text);

        SecLetters generateCC = new SecLetters(this.Server.MapPath("./HtmlTemplates/Secciones/Subseccion.htm"), this.Server.MapPath("./Indices/Seccion_CC"));
        generateCC.getHtmlFilesSectionsCC(edition, (int)Section.Control_Calidad, "CONTROL DE CALIDAD");

        Sections generateSCC = new Sections(this.Server.MapPath("./HtmlTemplates/Secciones/Subseccion.htm"), this.Server.MapPath("./Indices/Seccion_CC"));
        generateSCC.getHtmlFilesSubSections(edition, (int)Section.Control_Calidad, "CONTROL DE CALIDAD");

        this.lblMessage.Text = "La sección de Control de Calidad se generó correctamente";
    }

    protected void btnEE_Click(object sender, EventArgs e)
    {
        this.lblMessage.Text = "";

        int edition = Convert.ToInt32(txtEdition.Text);

        SecLetters generateEE = new SecLetters(this.Server.MapPath("./HtmlTemplates/Secciones/Subseccion.htm"), this.Server.MapPath("./Indices/Seccion_EE"));
        generateEE.getHtmlFilesSectionsEE(edition, (int)Section.Envases_Embalajes, "ENVASES Y EMBALAJES");

        Sections generateSEE = new Sections(this.Server.MapPath("./HtmlTemplates/Secciones/Subseccion.htm"), this.Server.MapPath("./Indices/Seccion_EE"));
        generateSEE.getHtmlFilesSectionsEE(edition, (int)Section.Envases_Embalajes, "ENVASES Y EMBALAJES");

        this.lblMessage.Text = "La sección de Envases y Embalajes se generó correctamente";
    }

    protected void btnServ_Click(object sender, EventArgs e)
    {
        this.lblMessage.Text = "";

        int edition = Convert.ToInt32(txtEdition.Text);

        SecLetters generateServ = new SecLetters(this.Server.MapPath("./HtmlTemplates/Secciones/Subseccion.htm"), this.Server.MapPath("./Indices/Seccion_Serv"));
        generateServ.getHtmlFilesSections(edition, (int)Section.Servicios, "SERVICIOS");

        Sections generateSServ = new Sections(this.Server.MapPath("./HtmlTemplates/Secciones/Subseccion.htm"), this.Server.MapPath("./Indices/Seccion_Serv"));
        generateSServ.getHtmlFilesSections(edition, (int)Section.Servicios, "SERVICIOS");

        this.lblMessage.Text = "La sección de Servicios se generó correctamente";
    }

    #endregion

    #region Enums

    private enum Section : int
    {
        Nombre_Comercial = 5,
        Directorio_Proveedores = 4,
        Materias_Primas = 3,
        Maquinaria = 8,
        Envases_Embalajes = 9,
        Control_Calidad = 10,
        Servicios = 23
    }

    #endregion

}
