using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class DEAQ_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void generateGeneral(object sender, EventArgs e)
    {
        int edition = Convert.ToInt32(this.txtEdition.Text.Trim());

        this.Response.Redirect("IndiceGral.aspx?Edition=" + edition);   
    }
    protected void generateGeneralSemillas(object sender, EventArgs e)
    {
        int edition = Convert.ToInt32(this.txtEdition.Text.Trim());

        this.Response.Redirect("IndiceGralSemillas.aspx?Edition=" + edition);  
    }
    protected void generateCultivos(object sender, EventArgs e)
    {
        int edition = Convert.ToInt32(this.txtEdition.Text.Trim());

        this.Response.Redirect("IndiceCult.aspx?Edition=" + edition); 
    }
    protected void generateCultivoSemillas(object sender, EventArgs e)
    {
        int edition = Convert.ToInt32(this.txtEdition.Text.Trim());

        this.Response.Redirect("IndiceCultSemillas.aspx?Edition=" + edition); 
    }
   
    protected void generatebtnSusA(object sender, EventArgs e)
    {
        int edition = Convert.ToInt32(this.txtEdition.Text.Trim());

        this.Response.Redirect("IndiceSust.aspx?Edition=" + edition); 
    }
    protected void generateUsos(object sender, EventArgs e)
    {
        int edition = Convert.ToInt32(this.txtEdition.Text.Trim());

        this.Response.Redirect("IndiceUso.aspx?Edition=" + edition); 
    }
    protected void generateUsosC(object sender, EventArgs e)
    {
        int edition = Convert.ToInt32(this.txtEdition.Text.Trim());

        this.Response.Redirect("IndiceUsoCol.aspx?Edition=" + edition); 
    }
    protected void generateUsosCSeeds(object sender, EventArgs e)
    {
        int edition = Convert.ToInt32(this.txtEdition.Text.Trim());

        this.Response.Redirect("IndiceUsoColSeeds.aspx?Edition=" + edition); 
    }
}