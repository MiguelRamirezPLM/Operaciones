using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class General : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
      

        // btnBack.Attributes.Add("onclick", "history.back(); return false;");

    }


    protected void generateAnunciantes(object sender, EventArgs e)
    {
        int edition = Convert.ToInt32(this.txtEdition.Text.Trim());

        this.Response.Redirect("IndAnunciantes.aspx?Edition=" + edition);  
    }


    protected void generateMarcas(object sender, EventArgs e)
    {
        int edition = Convert.ToInt32(this.txtEdition.Text.Trim());

        this.Response.Redirect("Default.aspx?Edition=" + edition);
    }

    protected void generatePyS(object sender, EventArgs e)
    {

        int edition = Convert.ToInt32(this.txtEdition.Text.Trim());

        this.Response.Redirect("IndProducts.aspx?Edition=" + edition);

    }
    protected void generateEspecialidades(object sender, EventArgs e)
    {
        int edition = Convert.ToInt32(this.txtEdition.Text.Trim());
        int speciality = Convert.ToInt32(this.Speciality.Text.Trim());

        this.Response.Redirect("indAlimentos.aspx?Edition=" + edition + "&Speciality=" + speciality);
    }
    protected void generateCR(object sender, EventArgs e)
    {

        int edition = Convert.ToInt32(this.txtEdition.Text.Trim());
        this.Response.Redirect("IndConsultaR.aspx?Edition=" + edition);

    }
    protected void generateMH(object sender, EventArgs e)
    {
        int edition = Convert.ToInt32(this.txtEdition.Text.Trim());
        this.Response.Redirect("IndMedHosp.aspx?Edition=" + edition);
    }
    protected void generateIter(object sender, EventArgs e)
    {
        int edition = Convert.ToInt32(this.txtEdition.Text.Trim());
        this.Response.Redirect("IndInternacional.aspx?Edition=" + edition);
    }
    protected void generateCarpetas(object sender, EventArgs e)
    {
        int edition = Convert.ToInt32(this.txtEdition.Text.Trim());
        this.Response.Redirect("Carpetas.aspx?Edition=" + edition);
    }

    protected void generateCarpetasV(object sender, EventArgs e)
    {
        int edition = Convert.ToInt32(this.txtEdition.Text.Trim());
        this.Response.Redirect("CarpetasVentas.aspx?Edition=" + edition);
    }

    protected void generateSPyS(object sender, EventArgs e)
    {
        int edition = Convert.ToInt32(this.txtEdition.Text.Trim());
        this.Response.Redirect("indProductosYServicios.aspx?Edition=" + edition);
    }

    protected void generateNClientAnun(object sender, EventArgs e)
    {
        int edition = Convert.ToInt32(this.txtEdition.Text.Trim());
        this.Response.Redirect("IndiceCAnunciantesNew.aspx?Edition=" + edition);
    }



}
