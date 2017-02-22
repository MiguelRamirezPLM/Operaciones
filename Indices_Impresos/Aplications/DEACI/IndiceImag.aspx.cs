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

public partial class IndiceImag : System.Web.UI.Page
{
    #region Events

    protected void Page_Load(object sender, EventArgs e)
    {
        DEACITableAdapters.GenericsTableAdapter sect = new DEACITableAdapters.GenericsTableAdapter();

        DEACI.GenericsDataTable brandSect = sect.getSectionImag();

        this.rptImag.DataSource = brandSect;
        this.rptImag.DataBind();

    }

    #endregion

    #region Control Events

    protected void rptImag_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        DataRowView rowview = (DataRowView)e.Item.DataItem;
        DataRow row = rowview.Row;

        DEACITableAdapters.ProductGenericsTableAdapter sect = new DEACITableAdapters.ProductGenericsTableAdapter();
        DEACI.ProductGenericsDataTable sectBrand = sect.getProductsBySection(Convert.ToInt32(row[0].ToString()), this._editionId);

        Repeater rpt = (Repeater)e.Item.FindControl("rptProd");
        rpt.DataSource = sectBrand;
        rpt.DataBind();

    }

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
            cadenahtml = "<p class='producto'><a class='links' href='../../Productos/" + htmlFile + "'> <b>" +
                       productName + ".</b>&nbsp;" + companyShortName + "</a></p>";
        }


        return cadenahtml;
    }

    #endregion
    private int _editionId = 4;
}
