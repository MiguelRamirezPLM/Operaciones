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
using System.Data.SqlClient;

public partial class _Default : System.Web.UI.Page 
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this._letter = this.Request.QueryString["letra"] == null ? "" :
            this.Request.QueryString["letra"].ToString();

        this.lblLetter.Text = "-" + this._letter.ToString().ToUpper() + "-";

        DEAQTableAdapters.TIPOSUSOSTableAdapter tusos = new DEAQTableAdapters.TIPOSUSOSTableAdapter();
        DEAQ.TIPOSUSOSDataTable prod = tusos.getUsos(this._letter);

        this.rptUsos.DataSource = prod;
        this.rptUsos.DataBind();
    }

    protected void rptUsos_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        HtmlControl divControl = (HtmlControl)e.Item.FindControl("s_divLevelChildren_0");
        HtmlControl aControl = (HtmlControl)e.Item.FindControl("displayChildren_0");

        aControl.Attributes.Add("onClick", "document.getElementById('" + divControl.ClientID + "').style.display='block'; guardSearch('" + divControl.ClientID + "', 1); return false;");
        aControl.Attributes.Add("onDblClick", "document.getElementById('" + divControl.ClientID + "').style.display='none'; guardSearch('" + divControl.ClientID + "', 0); return false");

        DataRowView rowview = (DataRowView)e.Item.DataItem;
        DataRow row = rowview.Row;

        DEAQTableAdapters.USOSTableAdapter uprod = new DEAQTableAdapters.USOSTableAdapter();
        DEAQ.USOSDataTable prods = uprod.getProducts(row[0].ToString());

        Repeater rpt = (Repeater)e.Item.FindControl("rptProd");
        rpt.DataSource = prods;
        rpt.DataBind();
    }

        
    private string _letter;
   


}