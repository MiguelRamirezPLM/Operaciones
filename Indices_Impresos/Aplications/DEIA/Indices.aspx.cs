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

public partial class Indices : System.Web.UI.Page
{
    #region Page Events

    protected void Page_Load(object sender, EventArgs e)
    {
        this.txtEdition.Focus();
    }

    #endregion

    #region Control Events

    protected void btnNC_Click(object sender, EventArgs e)
    {
        this.lblMessage.Text = "";

        int edition = Convert.ToInt32(txtEdition.Text);

        IndLetters generateNC = new IndLetters(this.Server.MapPath("./HtmlTemplates/Indices/lettersNC.htm"), this.Server.MapPath("./Indices"));
        generateNC.getHtmlFilesINC(edition, (int)Indexes.Nombre_Comercial, "ÍNDICE GENERAL POR NOMBRE COMERCIAL");

        this.lblMessage.Text = "El índice general se generó correctamente";

    }

    protected void btnMp_Click(object sender, EventArgs e)
    {
        this.lblMessage.Text = "";

        int edition = Convert.ToInt32(txtEdition.Text);

        IndLetters generateMP = new IndLetters(this.Server.MapPath("./HtmlTemplates/Indices/lettersMP.htm"), this.Server.MapPath("./Indices"));
        generateMP.getHtmlFilesMP(edition, (int)Sections.Materias_Primas, "MATERIAS PRIMAS");

        this.lblMessage.Text = "El índice de Materias Primas se generó correctamente";
    }

    protected void btnMaq_Click(object sender, EventArgs e)
    {
        this.lblMessage.Text = "";

        int edition = Convert.ToInt32(txtEdition.Text);

        IndLetters generateMaq = new IndLetters(this.Server.MapPath("./HtmlTemplates/Indices/Indice.htm"), this.Server.MapPath("./Indices"));
        generateMaq.getHtmlFilesInd(edition, (int)Sections.Maquinaria, "MAQUINARIA");

        this.lblMessage.Text = "El índice de Maquinaria se generó correctamente";
    }

    protected void btnCC_Click(object sender, EventArgs e)
    {
        this.lblMessage.Text = "";

        int edition = Convert.ToInt32(txtEdition.Text);

        IndLetters generateCC = new IndLetters(this.Server.MapPath("./HtmlTemplates/Indices/Indice.htm"), this.Server.MapPath("./Indices"));
        generateCC.getHtmlFilesCC(edition, (int)Sections.Control_Calidad, "CONTROL DE CALIDAD");

        this.lblMessage.Text = "El índice de Control de Calidad se generó correctamente";
    }

    protected void btnEE_Click(object sender, EventArgs e)
    {
        this.lblMessage.Text = "";

        int edition = Convert.ToInt32(txtEdition.Text);

        IndLetters generateEE = new IndLetters(this.Server.MapPath("./HtmlTemplates/Indices/Indice.htm"), this.Server.MapPath("./Indices"));
        generateEE.getHtmlFilesEE(edition, (int)Sections.Envases_Embalajes, "ENVASES Y EMBALAJES");

        CompaniesEE generateCEE = new CompaniesEE(this.Server.MapPath("./HtmlTemplates/Indices/CompaniesEE.htm"), this.Server.MapPath("./labs"));
        generateCEE.getCompaniesFiles(edition, (int)Sections.Envases_Embalajes);

        this.lblMessage.Text = "El índice de Envases y Embalajes se generó correctamente";
    }

    protected void btnServ_Click(object sender, EventArgs e)
    {
        this.lblMessage.Text = "";

        int edition = Convert.ToInt32(txtEdition.Text);

        IndLetters generateServ = new IndLetters(this.Server.MapPath("./HtmlTemplates/Indices/Indice.htm"), this.Server.MapPath("./Indices"));
        generateServ.getHtmlFilesInd(edition, (int)Sections.Servicios, "SERVICIOS");

        this.lblMessage.Text = "El índice de Servicios se generó correctamente";
    }

    protected void btnTerc_Click(object sender, EventArgs e)
    {
        this.lblMessage.Text = "";

        int edition = Convert.ToInt32(txtEdition.Text);

        IndLetters generateDPP = new IndLetters(this.Server.MapPath("./HtmlTemplates/Indices/Terceras.htm"), this.Server.MapPath("./Indices"));
        generateDPP.getHtmlFilesDPP(edition, (int)Sections.Directorio_Proveedores, "DIRECTORIO DE PROVEEDORES Y PRODUCTOS");

        this.lblMessage.Text = "El directorio de proveedores se generó correctamente";

    }


    #endregion

    #region Enums

    private enum Sections : int
    {
        Nombre_Comercial = 5,
        Directorio_Proveedores = 4,
        Materias_Primas = 3,
        Maquinaria = 8,
        Envases_Embalajes = 9,
        Control_Calidad = 10,
        Servicios = 23
    }

    private enum Indexes : int
    {
        Nombre_Comercial = 1,
        Materias_Primas = 2,
        Maquinaria = 3,
        Envases_Embalajes = 5,
        Control_Calidad = 4,
        Servicios = 6
    }

    #endregion

}
