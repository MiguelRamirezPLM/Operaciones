using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PEV_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
       
    }

    protected void generateGenProds(object sender, EventArgs e)
    {
        int edition = Convert.ToInt32(this.txtEdition.Text.Trim());

        this.Response.Redirect("IndGenPEV.aspx?Edition=" + edition);  

    }

    protected void generateActiveSubstances(object sender, EventArgs e)
    {

        int edition = Convert.ToInt32(this.txtEdition.Text.Trim());

        this.Response.Redirect("IndSubsPEV.aspx?Edition=" + edition);

    }

    protected void generateATC(object sender, EventArgs e)
    {

        int edition = Convert.ToInt32(this.txtEdition.Text.Trim());
        string tt = this.txtTablaThera.Text.Trim();
        string type = "";

        this.Response.Redirect("IndTheraPEV.aspx?Edition=" + edition + "&TableT=" + tt + "&TypeT=" + type);

    }
    protected void generateVac(object sender, EventArgs e)
    {
        int edition = Convert.ToInt32(this.txtEdition.Text.Trim());

        this.Response.Redirect("IndVacunacion.aspx?Edition=" + edition);
    }

    protected void generateATCN(object sender, EventArgs e)
    {

      
        int edition = Convert.ToInt32(this.txtEdition.Text.Trim());
        string tt = this.txtTablaThera.Text.Trim();

        //if((string.IsNullOrEmpty(tt))&&((edition == 0)||(edition == null)))
        //{

        //}
        //else
        //{
            string type = "ATCNut";

            this.Response.Redirect("IndTheraPEV.aspx?Edition=" + edition + "&TableT=" + tt + "&TypeT=" + type);
        //}
    }
    protected void generateATCC(object sender, EventArgs e)
    {
        int edition = Convert.ToInt32(this.txtEdition.Text.Trim());
        string tt = this.txtTablaThera.Text.Trim();
        string type = "ATCC";

        this.Response.Redirect("IndTheraPEV.aspx?Edition=" + edition + "&TableT=" + tt + "&TypeT=" + type);
    }
}