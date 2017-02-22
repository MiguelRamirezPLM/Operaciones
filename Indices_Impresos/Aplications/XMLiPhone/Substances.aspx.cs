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

public partial class Substances : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.rptSust.DataSource = GetData.Instance.generateSubstance();
        this.rptSust.DataBind();
               
    }

    protected void rptSust_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {

        PharmaSearchEngine.ActiveSubstanceInfo sust = (PharmaSearchEngine.ActiveSubstanceInfo)e.Item.DataItem;

        this._ds = new Substance();

        _ds.root.AddrootRow(sust.Description);

        ID = sust.ActiveSubstanceId.ToString();

        Repeater rptProd = (Repeater)e.Item.FindControl("rptProd");
        rptProd.DataSource = GetData.Instance.generateProductsBySubstance(sust.ActiveSubstanceId);
        rptProd.DataBind();
        
    }

    protected void rptProd_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {

        PharmaSearchEngine.ProductByEditionInfo prod = (PharmaSearchEngine.ProductByEditionInfo)e.Item.DataItem;

        _ds.article.AddarticleRow("med", prod.Brand + " " + prod.PharmaForm, System.Configuration.ConfigurationManager.AppSettings["path"].ToString() + "products/" + prod.ProductId.ToString() + "_" + prod.PharmaFormId.ToString() + ".xml", (Substance.rootRow)_ds.root.Rows[_cont]);
        _ds.WriteXml("c:\\XML\\substances\\" + ID + ".xml");
               
    }

    private Substance _ds;    
    private String ID = "";
    private int _cont=0;
    
}
