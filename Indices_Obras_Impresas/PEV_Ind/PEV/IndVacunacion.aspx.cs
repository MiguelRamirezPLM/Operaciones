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

public partial class PEV_IndSubsPEV : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this._editionId = this.Request.QueryString["Edition"] == null ? -1 : Convert.ToInt32(this.Request.QueryString["Edition"]);

        if (this._editionId > 0)
        {
            
            this.rptLet.DataSource = GetPEV.Instance.getAlphabetVac(this._editionId);
            this.rptLet.DataBind();
            _cont = 0;
            _cont2 = 0;
        }

        this.Response.Redirect("Default.aspx"); 
    }

    protected void rptLet_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        DataRowView rowView = (DataRowView)e.Item.DataItem;
        DataRow letra = rowView.Row;

        _ds.Letter.AddLetterRow(letra.ItemArray[0].ToString());

        Repeater rptSubs = (Repeater)e.Item.FindControl("rptSubs");
        rptSubs.DataSource = GetPEV.Instance.getSubstancesVac(_editionId,letra.ItemArray[0].ToString());
        rptSubs.DataBind();
        _cont+=1;

    }
    protected void rptSubs_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        DataRowView rowView = (DataRowView)e.Item.DataItem;
        DataRow subs = rowView.Row;
        _ds.Substance.AddSubstanceRow(subs.ItemArray[0].ToString(), (IndVac.LetterRow)_ds.Letter.Rows[_cont]);
        Repeater rptProds= (Repeater)e.Item.FindControl("rptProds");
        rptProds.DataSource = GetPEV.Instance.getInforVac(_editionId , subs.ItemArray[1].ToString());
        rptProds.DataBind();
        _cont2+=1;
    }
    protected void rptProds_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        DataRowView rowView = (DataRowView)e.Item.DataItem;
        DataRow Prods = rowView.Row;

        _ds.Product.AddProductRow(Prods.ItemArray[0].ToString(),Prods.ItemArray[1].ToString(),Prods.ItemArray[2].ToString(),Prods.ItemArray[3].ToString(),(IndVac.SubstanceRow)_ds.Substance.Rows[_cont2]);

        _ds.WriteXml(System.Configuration.ConfigurationManager.AppSettings["General"] + "SubsVacunacionPEV.xml");
    }


    private int _cont, _cont2;
    private IndVac _ds = new IndVac();
    private int _editionId;
}
