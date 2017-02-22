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

public partial class indiceLabs : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this._letter = this.Request.QueryString["letra"] == null ? "" :
            this.Request.QueryString["letra"].ToString();

        

        this.lblLetter.Text = "-" + this._letter.ToString().ToUpper() + "-";

        DEAQTableAdapters.EmpresasTableAdapter labs = new DEAQTableAdapters.EmpresasTableAdapter();
        DEAQ.EmpresasDataTable empresa = labs.getLabs(this._letter);

        this.rptLabs.DataSource = empresa;
        this.rptLabs.DataBind();
    }


    private string _letter;

}

