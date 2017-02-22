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

public partial class IndiceMarca : System.Web.UI.Page
{
    #region Events

    protected void Page_Load(object sender, EventArgs e)
    {
        this._letter = this.Request.QueryString["letra"] == null ? "" :
            this.Request.QueryString["letra"].ToString();

        if (this._letter != "")
        {
            this.lblLetter.Text = "-" + this._letter.ToString().ToUpper() + "-";

            DEACITableAdapters.BrandsTableAdapter brand = new DEACITableAdapters.BrandsTableAdapter();

            DEACI.BrandsDataTable brandTab = brand.getBrands(this._editionId, this._letter);

            this.rptMarcas.DataSource = brandTab;
            this.rptMarcas.DataBind();
        }

    }

    #endregion

    #region Control Events

    protected void rptMarcas_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        DataRowView rowview = (DataRowView)e.Item.DataItem;
        DataRow row = rowview.Row;

        DEACITableAdapters.CompanyBrandsTableAdapter comp = new DEACITableAdapters.CompanyBrandsTableAdapter();
        DEACI.CompanyBrandsDataTable compBrand = comp.getComp(this._editionId, Convert.ToInt32(row[0].ToString()));

        Repeater rpt = (Repeater)e.Item.FindControl("rptComp");
        rpt.DataSource = compBrand;
        rpt.DataBind();
    }

    #endregion

    private string _letter;
    private int _editionId = 4;
}
