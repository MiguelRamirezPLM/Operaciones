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

public partial class Index : System.Web.UI.Page
{
    #region Page Events

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    #endregion

    #region Control Events

    protected void btnIndex_Click(object sender, EventArgs e)
    {
        this.Response.Redirect("Indices.aspx");
    }

    protected void btnSections_Click(object sender, EventArgs e)
    {
        this.Response.Redirect("Secciones.aspx");
    }

    protected void btnFab_Click(object sender, EventArgs e)
    {
        this.Response.Redirect("Fabricantes.aspx");
    }

    protected void btnSuc_Click(object sender, EventArgs e)
    {
        this.Response.Redirect("Sucursales.aspx");
    }

    #endregion

}
