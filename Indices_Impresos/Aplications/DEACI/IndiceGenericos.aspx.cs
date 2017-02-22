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

public partial class IndiceGenericos : System.Web.UI.Page
{
    #region Events

    protected void Page_Load(object sender, EventArgs e)
    {
        this._letter = this.Request.QueryString["letra"] == null ? "" :
            this.Request.QueryString["letra"].ToString();

        this.lblLetter.Text = "-" + this._letter.ToString().ToUpper() + "-";

        DEACITableAdapters.GenericsTableAdapter sect = new DEACITableAdapters.GenericsTableAdapter();

        DEACI.GenericsDataTable brandSect = sect.getSectionsByLetter(this._letter, this._editionId);

        this.rptGener.DataSource = brandSect;
        this.rptGener.DataBind();

    }

    #endregion

    #region Control Events

    protected void rptGener_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        DataRowView rowview = (DataRowView)e.Item.DataItem;
        DataRow row = rowview.Row;

        DEACITableAdapters.ProductGenericsTableAdapter sect = new DEACITableAdapters.ProductGenericsTableAdapter();
        DEACI.ProductGenericsDataTable sectBrand = sect.getProductsBySection(Convert.ToInt32(row[0].ToString()), this._editionId);

        Repeater rpt = (Repeater)e.Item.FindControl("rptProd");
        rpt.DataSource = sectBrand;
        rpt.DataBind();

    }

    #endregion

    public string getLink(string htmlFile, string productName, string companyShortName)
    {
        string cadenahtml = "";

        if (htmlFile.Equals(""))
        {
            cadenahtml = " <p class='productoNoLink'>" +
                      "<b> " + productName + ".</b> &nbsp;" + companyShortName + "</p>";
        }
        else
        {
            cadenahtml = "<p class='producto'><a class='links' href='../../Productos/" + htmlFile + "'><b> " +
                       productName + ".</b>&nbsp;" + companyShortName + "</a></p>";
        }


        return cadenahtml;
    }

    private string _letter;
    private int _editionId = 4;
}
