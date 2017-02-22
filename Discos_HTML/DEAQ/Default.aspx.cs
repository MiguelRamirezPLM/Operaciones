using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void generateCrops(object sender, EventArgs e)
    {
        int editionId = Convert.ToInt32(txtEdition.Text);

        string path = "C:/Users/ulises.orihuela/Desktop/Productos_Discos/deaq/";

        Session["editionId"] = editionId;

        Response.Redirect("~/indiceCultivos.aspx");
    }
    protected void generateLabs(object sender, EventArgs e)
    {

    }
    protected void generateProducts(object sender, EventArgs e)
    {

    }
    protected void generateSeeds(object sender, EventArgs e)
    {

    }
    protected void generateSubstances(object sender, EventArgs e)
    {

    }
    protected void generateThird(object sender, EventArgs e)
    {

    }
    protected void generatesUses(object sender, EventArgs e)
    {

    }
}