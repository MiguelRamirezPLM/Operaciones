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

public partial class Laboratories : System.Web.UI.Page
{
    #region Events

    protected void Page_Load(object sender, EventArgs e)
    {
        int editionId;

        if (this.Request.QueryString["editionId"] != null)
        {
            editionId = Convert.ToInt32(this.Request.QueryString["editionId"]);

            DEACITableAdapters.CompaniesTableAdapter labSec = new DEACITableAdapters.CompaniesTableAdapter();
            DEACI.CompaniesDataTable labs = labSec.getCompanies(editionId);

            this.rptComp.DataSource = labs;
            this.rptComp.DataBind();
        }
    }

    #endregion

    #region Public Methods

    public string getCompany(string name, string html)
    {
        string cadena = "";

        

        if (!string.IsNullOrEmpty(name))
        {
            if (!string.IsNullOrEmpty(html))
            {
                cadena = "<b> <a href='" + html + "' target='adentro' style='text-decoration:none'>" + name + "</a></b>";
                if (this._letter != name.Substring(0, 1) && !string.IsNullOrEmpty(this._letter))
                    cadena = "<br />" + cadena;
            }
            else
            {
                cadena = "<b> " + name + "</b>";
            }
        }
        else
            cadena = "";

        this._letter = name.Substring(0, 1);
        return cadena;
    }

    #endregion

    private string _letter;

}
