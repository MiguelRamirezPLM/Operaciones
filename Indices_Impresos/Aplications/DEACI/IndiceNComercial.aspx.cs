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

public partial class IndiceNComercial : System.Web.UI.Page
{
    #region Events

    protected void Page_Load(object sender, EventArgs e)
    {
        this._letter = this.Request.QueryString["letra"] == null ? "" :
            this.Request.QueryString["letra"].ToString();

        if (this._letter != "")
        {

            DEACITableAdapters.ProductsNCTableAdapter prodNc = new DEACITableAdapters.ProductsNCTableAdapter();

            this.lblLetter.Text = "-" + this._letter.ToString().ToUpper() + "-";

            DEACI.ProductsNCDataTable prods = prodNc.getProducts1(this._editionId, this._letter);

            this.rptLink.DataSource = prods;
            this.rptLink.DataBind();
        }

    }

    #endregion

    #region Control Events

    protected void rptLink_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        DataRowView view = (DataRowView)e.Item.DataItem;
        DataRow current = view.Row;

        if (!string.IsNullOrEmpty(current[3].ToString()))
        {
            HtmlGenericControl div = (HtmlGenericControl)e.Item.FindControl("Links");
            div.Visible = true;

        }
        else
        {
            HtmlGenericControl div = (HtmlGenericControl)e.Item.FindControl("NoLinks");
            div.Visible = true;
        }

    }

    #endregion

    private string _letter;
    private int _editionId = 4;
}
