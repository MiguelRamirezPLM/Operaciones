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
    
    protected void Page_Load(object sender, EventArgs e)
    {
        this.txtEdition.Focus();
    }
    #region Control Events

    protected void btnSP_Click(object sender, EventArgs e)
    {
        this.lblMessage.Text = "";

        int edition = Convert.ToInt32(txtEdition.Text);
              

        IndLetter generateSP = new IndLetter(this.Server.MapPath("./HtmlTemplates/Indices/letterSP.htm"), @"C:\PEV\Disco\especies");
        generateSP.getHtmlFilesInd(edition,  "INDICE DE ESPECIES");     

        this.lblMessage.Text = "El índice de especies se generó correctamente";
    }

    protected void btnIndGeneral_Click(object sender, EventArgs e)
    {

        this.lblMessage.Text = "";

        int edition = Convert.ToInt32(txtEdition.Text);

        IndLetter IndGeneral = new IndLetter(this.Server.MapPath("./HtmlTemplates/Indices/lettersIndGen.htm"), @"C:\PEV\Disco\productos");
        IndGeneral.getHtmlFilesIndPro(edition, "INDICE DE PRODUCTOS");
      
       
        this.Label1.Text = "El índice de productos se generó correctamente";
    
    }

    protected void btnIndSubs_Click(object sender, EventArgs e)
    {
        this.lblMessage.Text = "";

       int edition = Convert.ToInt32(txtEdition.Text);

       IndLetter generateSubs = new IndLetter(this.Server.MapPath("./HtmlTemplates/Indices/letterSubs.htm"), @"C:\PEV\Disco\sustancias");
       generateSubs.getHtmlFilesIndSubs(edition, "INDICE DE INGREDIENTES ACTIVOS");

       this.Label3.Text = "El índice de sustancias activas se generó correctamente";

    }

    protected void btnIndThera_Click(object sender, EventArgs e)
    {

        this.lblMessage.Text = "";

        int edition = Convert.ToInt32(txtEdition.Text);

        IndLetter generateThera = new IndLetter(this.Server.MapPath("./HtmlTemplates/Indices/letterThera.htm"), @"C:\PEV\Disco\atc");
        generateThera.getHtmlFilesIndThera(edition, "ÍNDICE TERAPÉUTICO");

        this.Label5.Text = "El índice terapéutico se generó correctamente";


    }

    //protected void btnNutr_Click(object sender, EventArgs e)
    //{
    //    this.lblMessage.Text = "";

    //    int edition = Convert.ToInt32(txtEdition.Text);

    //    IndLetter IndNutricional = new IndLetter(this.Server.MapPath("./HtmlTemplates/Indices/lettersIndNutr.htm"), @"C:\PEV\Disco\nutricionales");
    //    IndNutricional.getHtmlFilesIndNutrPro(edition, "INDICE DE PRODUCTOS NUTRICIONALES");


    //    this.Label9.Text = "El índice de productos nutricionales se generó correctamente";
    //}

    protected void btnNutr_Click(object sender, EventArgs e)
    {
        this.lblMessage.Text = "";

        int edition = Convert.ToInt32(txtEdition.Text);

        IndLetter IndNutricional = new IndLetter(this.Server.MapPath("./HtmlTemplates/Indices/letterNutricionAnimalesCompania.htm"), @"C:\PEV\Disco\nutricionales");
        IndNutricional.getHtmlFilesIndNutrPro(edition, "INDICE DE PRODUCTOS NUTRICIONALES");


        this.Label9.Text = "El índice de productos nutricionales se generó correctamente";
    }


     

    protected void btnLabs_Click(object sender, EventArgs e)
    {
        this.lblMessage.Text = "";

        int edition = Convert.ToInt32(txtEdition.Text);

        IndLetter IndLabs = new IndLetter(this.Server.MapPath("./HtmlTemplates/Indices/letterLabs.htm"), @"C:\PEV\Disco\labs");
        IndLabs.getHtmlFilesIndLabs(edition, "INDICE DE EMPRESAS", this.Server.MapPath("./HtmlTemplates/Indices/Laboratories.htm"));

        this.Label7.Text = "El índice de laboratorios se generó correctamente";
    }

    #endregion


    protected void btnVacunacion_Click(object sender, EventArgs e)
    {
        this.lblMessage.Text = "";

        int edition = Convert.ToInt32(txtEdition.Text);
        IndLetter IndVacu = new IndLetter(this.Server.MapPath("./HtmlTemplates/Indices/vacunacion.htm"), @"C:\PEV\Disco\vacunacion");
        IndVacu.getHtmlFilesIndVacun(edition, "INDICE DE DE VACUNACIÓN" );
    }
  
    protected void btnLabsNew_Click(object sender, EventArgs e)
    {
        this.lblMessage.Text = "";

        int edition = Convert.ToInt32(txtEdition.Text);

        IndLetter IndLabs = new IndLetter(this.Server.MapPath("./HtmlTemplates/Indices/letterLabsN.htm"), @"C:\PEV\Disco\labs");
        IndLabs.getHtmlFilesIndLabsN(edition, "INDICE DE EMPRESAS", this.Server.MapPath("./HtmlTemplates/Indices/LaboratoriesN.htm"));

        this.Label13.Text = "El índice de laboratorios se generó correctamente";
    }

    protected void btnIndGeneralNew_Click(object sender, EventArgs e)
    {
        this.lblMessage.Text = "";

        int edition = Convert.ToInt32(txtEdition.Text);

 

        IndLetter IndGeneral = new IndLetter(this.Server.MapPath("./HtmlTemplates/Indices/lettersIndGenN.htm"), @"C:\PEV\Disco\prods");
        IndGeneral.getHtmlFilesIndProN (edition, "INDICE DE PRODUCTOS");

        this.Label15.Text = "El índice general se generó correctamente";
    }
    protected void btnIndGeneralDivPecNew_Click(object sender, EventArgs e)
    {

        this.lblMessage.Text = "";

        int edition = Convert.ToInt32(txtEdition.Text);

        IndLetter IndGeneral = new IndLetter(this.Server.MapPath("./HtmlTemplates/Indices/lettersIndGenDivPec.htm"), @"C:\PEV\Disco\prodsDivPec");
        IndGeneral.getHtmlFilesIndProNDP(edition, "INDICE DE PRODUCTOS DIVISION PECUARIA");

        this.Label17.Text = "El índice general Division Pecuaria se generó correctamente";

    }


    protected void btnVacunacionN_Click(object sender, EventArgs e)
    {
        this.lblMessage.Text = "";

        int edition = Convert.ToInt32(txtEdition.Text);
        IndLetter IndVacu = new IndLetter(this.Server.MapPath("./HtmlTemplates/Indices/vacunacionNew.htm"), @"C:\PEV\Disco\vacunacion");
        IndVacu.getHtmlFilesIndVacunNew(edition, "INDICE DE DE VACUNACIÓN");

        this.Label19.Text = "El índice  DE VACUNACION se generó correctamente";
    }

    protected void btnIndSubsNew_Click(object sender, EventArgs e)
    {
        this.lblMessage.Text = "";

        int edition = Convert.ToInt32(txtEdition.Text);

        IndLetter generateSubs = new IndLetter(this.Server.MapPath("./HtmlTemplates/Indices/letterSubsN.htm"), @"C:\PEV\Disco\sustancias");
        generateSubs.getHtmlFilesIndSubsN(edition, "INDICE DE INGREDIENTES ACTIVOS");

        this.Label21.Text = "El índice de sustancias activas se generó correctamente";
    }

    protected void btnIndTheraN_Click(object sender, EventArgs e)
    {
        this.lblMessage.Text = "";

        int edition = Convert.ToInt32(txtEdition.Text);

        IndLetter generateThera = new IndLetter(this.Server.MapPath("./HtmlTemplates/Indices/letterTheraN.htm"), @"C:\PEV\Disco\atc");
        generateThera.getHtmlFilesIndTheraN (edition, "ÍNDICE TERAPÉUTICO");

        this.Label23.Text = "El índice terapéutico se generó correctamente";
    }
}
