
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

public partial class Indices_DEF : System.Web.UI.Page
{
    #region Page Events

    protected void Page_Load(object sender, EventArgs e)
    {
        this.txtEdition.Focus();
    }

    #endregion

    #region Controls

    protected void btnSP_Click(object sender, EventArgs e)
    {
        int edition = Convert.ToInt32(this.txtEdition.Text.Trim());
        this.lblMessage.Text = "";

        //CreateIndexes generatePC = new CreateIndexes(this.Server.MapPath("./HtmlTemplates/Indices/letterSP.htm"), "C:/MedicamentosPLM/Productos/");
        CreateIndexes generatePC = new CreateIndexes(this.Server.MapPath("./HtmlTemplates/Indices/indexproducts.htm"), "C:/Users/angel.hernandez/Desktop/Discos def/");
        //generatePC.generateHTMLS();
        generatePC.generateProductatribute(edition);

        this.lblMessage.Text = "Los Productos se crearon correctamente";

    }
    
    protected void btnNC_Click(object sender, EventArgs e)
    {
        this.Label1.Text = "";

        int edition = Convert.ToInt32(this.txtEdition.Text.Trim());

        CreateIndexes generateSust = new CreateIndexes(this.Server.MapPath("./HtmlTemplates/indsus.htm"), @"C:\Users\ulises.orihuela\Desktop\Productos_Discos\PERU\sustancias\");
        generateSust.getAlphabetSubs(edition, this.Server.MapPath("./HtmlTemplates"));

        this.Label1.Text = "Los sustancias se crearon correctamente";
    }
    
    protected void btnSub_Click(object sender, EventArgs e)
    {
        this.Label3.Text = "";

        int edition = Convert.ToInt32(this.txtEdition.Text.Trim());

        CreateIndexes generateInd = new CreateIndexes(this.Server.MapPath("./HtmlTemplates/indind.htm"), @"C:\Users\ulises.orihuela\Desktop\Productos_Discos\PERU\indicaciones\");
        generateInd.getAlphabetInds(edition, this.Server.MapPath("./HtmlTemplates"));

        this.Label3.Text = "Los indicaciones se crearon correctamente";
    }
    
    protected void Button1_Click(object sender, EventArgs e)
    {
        this.Label5.Text = "";

        int edition = Convert.ToInt32(this.txtEdition.Text.Trim());

        CreateIndexes generateSid = new CreateIndexes(this.Server.MapPath("./HtmlTemplates/indexSidef.htm"), @"C:\Users\ulises.orihuela\Desktop\Productos_Discos\PERU\sidef\");
        generateSid.generateSidef(edition, this.Server.MapPath("./HtmlTemplates"));

        this.Label5.Text = "El SIDEF se creo correctamente";

    }
    
    protected void btnLabs_Click(object sender, EventArgs e)
    {
        this.Label7.Text = "";

        int edition = Convert.ToInt32(this.txtEdition.Text.Trim());

        CreateIndexes generateLab = new CreateIndexes(this.Server.MapPath("./HtmlTemplates/indlab.htm"), @"C:\Users\ulises.orihuela\Desktop\Productos_Discos\PERU\laboratorios");
        generateLab.getAlphabetDivs(edition, this.Server.MapPath("./HtmlTemplates"));

        this.Label7.Text = "Los laboratorios se crearon Correctamente";
    }

    protected void btnNutr_Click(object sender, EventArgs e)
    {
        this.Label9.Text = "";

        int edition = Convert.ToInt32(this.txtEdition.Text.Trim());

        CreateIndexes generateProd = new CreateIndexes(this.Server.MapPath("./HtmlTemplates/indmar.htm"), @"C:\Users\ulises.orihuela\Desktop\Productos_Discos\PERU\marcas\");
        generateProd.getAlphabetProducts(edition);

        this.Label9.Text = "El índice de marcas se creó Correctamente";
    }


    protected void btnpharmaform(object sender, EventArgs e)//Genera pharmaForm
    {
        int edition = Convert.ToInt32(this.txtEdition.Text.Trim());
        this.Label10.Text = "";

        CreateIndexes generatePC = new CreateIndexes(this.Server.MapPath("./HtmlTemplates/Indices/indexproducts.htm"), @"\\195.192.2.17\Proyectos\Sitios web\medicamentosplm.com.pe\productos\");
        generatePC.generatepharmaforms(edition, this.Server.MapPath("./HtmlTemplates"));

        this.Label10.Text = "Las formas farmaceuticas se crearon correctamente";
    }

    protected void btnRepComun(object sender, EventArgs e)
    {
        int edition = Convert.ToInt32(this.txtEdition.Text.Trim());
        this.Label10.Text = "";

        CreateIndexes generatePC = new CreateIndexes(this.Server.MapPath("./HtmlTemplates/Indices/indexproducts.htm"), @"\\195.192.2.17\Proyectos\Sitios web\medicamentosplm.com.pa\RepositorioComun\");
        generatePC.generateProductMaxEditions(edition, this.Server.MapPath("./HtmlTemplates"));

        this.Label10.Text = "Los productos se crearon correctamente";
    }
    
    #endregion
}    


