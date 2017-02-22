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

public partial class WESTINDIES_WISubstances : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.Request.QueryString["Edition"] != null)
        {
            this._editionId = Convert.ToInt32(this.Request.QueryString["Edition"]);

            this.rptLet.DataSource = WIActiveSubs.Instance.getAlphabet(this._editionId);
            this.rptLet.DataBind();
            _cont = 0;
            _cont2 = 0;
        }   
    }

    protected void ddlLetra_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void rptLet_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        DataRowView rowView = (DataRowView)e.Item.DataItem;
        DataRow subsRow = rowView.Row;

        _ds.Letra.AddLetraRow(subsRow.ItemArray[0].ToString());

        Repeater rptSubs = (Repeater)e.Item.FindControl("rptSubs");
        rptSubs.DataSource = WIActiveSubs.Instance.getSubstances(this._editionId, subsRow.ItemArray[0].ToString());
        rptSubs.DataBind();
        _cont += 1;

    }

    protected void rptSubs_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        DataRowView rowView = (DataRowView)e.Item.DataItem;
        DataRow subsRow = rowView.Row;

        _ds.Substance.AddSubstanceRow(subsRow.ItemArray[0].ToString(), (WISubstances.LetraRow)_ds.Letra.Rows[_cont]);

        Repeater rptProd = (Repeater)e.Item.FindControl("rptProds");
        rptProd.DataSource = WIActiveSubs.Instance.getProducts(this._editionId, Convert.ToInt32(subsRow.ItemArray[1].ToString()));
        rptProd.DataBind();
        _cont2 += 1;
    }

    protected void rptProds_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        DataRowView rowView = (DataRowView)e.Item.DataItem;
        DataRow infoRow = rowView.Row;

        if (infoRow.ItemArray[3].ToString() == "")
            _ds.Single.AddSingleRow(infoRow.ItemArray[1].ToString(), infoRow.ItemArray[7].ToString(), infoRow.ItemArray[4].ToString(),
                infoRow.ItemArray[5].ToString(), infoRow.ItemArray[6].ToString(), (WISubstances.SubstanceRow)_ds.Substance.Rows[_cont2]);

        else
        {
            _ds.InCombination.AddInCombinationRow(infoRow.ItemArray[1].ToString(), infoRow.ItemArray[7].ToString(), infoRow.ItemArray[3].ToString(),
                infoRow.ItemArray[4].ToString(), infoRow.ItemArray[5].ToString(), infoRow.ItemArray[6].ToString(), (WISubstances.SubstanceRow)_ds.Substance.Rows[_cont2]);

        }
        _ds.WriteXml(System.Configuration.ConfigurationManager.AppSettings["ActiveSubstWI"] + "ActiveSubs.xml");

    }



    private int _cont, _cont2;
    private WISubstances _ds = new WISubstances();
    private int _editionId;
}
