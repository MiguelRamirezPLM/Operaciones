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

public partial class DEAQ_IndiceSem : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this._editionId = this.Request.QueryString["Edition"] == null ? -1 : Convert.ToInt32(this.Request.QueryString["Edition"]);

        if (this._editionId > 0)
        {
            this.rptSem.DataSource = IndiceSemillas.Instance.getSeeds(this._editionId);
            this.rptSem.DataBind();
            _cont = 0;
        }
    }

    protected void rptSem_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        DataRowView rowView = (DataRowView)e.Item.DataItem;
        DataRow seedRow = rowView.Row;

        _ds.Seed.AddSeedRow(seedRow.ItemArray[1].ToString());

        Repeater rptProd = (Repeater)e.Item.FindControl("rptProd");
        rptProd.DataSource = IndiceSemillas.Instance.getProducts(Convert.ToInt32(seedRow.ItemArray[0].ToString()), this._editionId);
        rptProd.DataBind();
        _cont += 1;

    }

    protected void rptProd_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        DataRowView rowView = (DataRowView)e.Item.DataItem;
        DataRow prodRow = rowView.Row;

        _ds.Product.AddProductRow(prodRow.ItemArray[1].ToString(), prodRow.ItemArray[2].ToString(),
                prodRow.ItemArray[3].ToString(),(IndSemillas.SeedRow)_ds.Seed.Rows[_cont]);

        _ds.WriteXml(System.Configuration.ConfigurationManager.AppSettings["Semillas"] + "Semillas.xml");

    }

    private int _cont;
    private IndSemillas _ds = new IndSemillas();
    private int _editionId;
}
