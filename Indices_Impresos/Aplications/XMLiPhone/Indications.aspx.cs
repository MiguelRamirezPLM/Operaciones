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

public partial class Indications : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.rptInd.DataSource = GetData.Instance.generateIndication();
        this.rptInd.DataBind();

    }

    protected void rptInd_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {

        PharmaSearchEngine.IndicationInfo ind = (PharmaSearchEngine.IndicationInfo)e.Item.DataItem;

        this._ds = new Indication();

        _ds.root.AddrootRow(ind.Description);

        ID = ind.IndicationId.ToString();

        Repeater rptProd = (Repeater)e.Item.FindControl("rptProd");
        rptProd.DataSource = GetData.Instance.getProductsByIndication(ind.IndicationId);
        rptProd.DataBind();

    }

    protected void rptProd_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {

        PharmaSearchEngine.ProductByEditionInfo prod = (PharmaSearchEngine.ProductByEditionInfo)e.Item.DataItem;

        _ds.article.AddarticleRow("med", prod.Brand + " " + prod.PharmaForm, System.Configuration.ConfigurationManager.AppSettings["path"].ToString() + "products/" + prod.ProductId.ToString() + "_" + prod.PharmaFormId.ToString() + ".xml", (Indication.rootRow)_ds.root.Rows[_cont]);
        _ds.WriteXml("c:\\XML\\indications\\" + ID + ".xml");

    }

    private Indication _ds;
    private String ID = "";
    private int _cont = 0;

}