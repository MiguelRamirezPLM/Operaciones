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

public partial class WESTINDIES_WITherap : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.Request.QueryString["Edition"] != null)
        {
            this._editionId = Convert.ToInt32(this.Request.QueryString["Edition"]);

            this.rptTerN0.DataSource = WITherapeutics.Instance.getAlphabet();
            this.rptTerN0.DataBind();
            _cont = 0;
            _cont2 = 0;
            _cont3 = 0;
            _cont4 = 0;
        }
    }

    protected void ddlLetra_SelectedIndexChanged(object sender, EventArgs e)
    {
        //this.rptTerN0.DataSource = TherIndice.Instance.getTherapeutics(this.ddlLetra.SelectedValue);
        //this.rptTerN0.DataBind();
        //_cont = 0;
        //_cont2 = 0;
        //_cont3 = 0;
        //_cont4 = 0;
    }

    protected void rptTerN0_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        DataRowView rowView = (DataRowView)e.Item.DataItem;
        DataRow terN0Row = rowView.Row;

        _ds.TherapeuticN0.AddTherapeuticN0Row(terN0Row.ItemArray[1].ToString().ToUpper() + " - " + terN0Row.ItemArray[0].ToString().ToUpper());

        Repeater rptTherN1 = (Repeater)e.Item.FindControl("rptTerN1");
        rptTherN1.DataSource = WITherapeutics.Instance.getTherapeuticsByParentId(Convert.ToInt32(terN0Row.ItemArray[2].ToString()));
        rptTherN1.DataBind();
        _cont += 1;

    }

    protected void rptTerN1_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        DataRowView rowView = (DataRowView)e.Item.DataItem;
        DataRow terN1Row = rowView.Row;

        _ds.TherapeuticN1.AddTherapeuticN1Row(terN1Row.ItemArray[1].ToString().ToUpper() + " - " + terN1Row.ItemArray[0].ToString().ToUpper(),
            (WITherapeutic.TherapeuticN0Row)_ds.TherapeuticN0.Rows[_cont]);

        Repeater rptTherN2 = (Repeater)e.Item.FindControl("rptTerN2");
        rptTherN2.DataSource = WITherapeutics.Instance.getTherapeuticsByParentId(Convert.ToInt32(terN1Row.ItemArray[2].ToString()));
        rptTherN2.DataBind();
        _cont2 += 1;

    }

    protected void rptTerN2_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        DataRowView rowView = (DataRowView)e.Item.DataItem;
        DataRow terN2Row = rowView.Row;

        _ds.TherapeuticN2.AddTherapeuticN2Row(terN2Row.ItemArray[1].ToString() + " - " + terN2Row.ItemArray[0].ToString(),
            (WITherapeutic.TherapeuticN1Row)_ds.TherapeuticN1.Rows[_cont2]);

        Repeater rptProdN2 = (Repeater)e.Item.FindControl("rptProdN2");
        rptProdN2.DataSource = WITherapeutics.Instance.getInformation(this._editionId, Convert.ToInt32(terN2Row.ItemArray[2].ToString()));
        rptProdN2.DataBind();

        Repeater rptTerN3 = (Repeater)e.Item.FindControl("rptTerN3");
        rptTerN3.DataSource = WITherapeutics.Instance.getTherapeuticsByParentId(Convert.ToInt32(terN2Row.ItemArray[2].ToString()));
        rptTerN3.DataBind();
        _cont3 += 1;

    }

    protected void rptTerN3_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        DataRowView rowView = (DataRowView)e.Item.DataItem;
        DataRow terN3Row = rowView.Row;

        _ds.TherapeuticN3.AddTherapeuticN3Row(terN3Row.ItemArray[1].ToString() + " - " + terN3Row.ItemArray[0].ToString(),
            (WITherapeutic.TherapeuticN2Row)_ds.TherapeuticN2.Rows[_cont3]);

        Repeater rptProdN3 = (Repeater)e.Item.FindControl("rptProdN3");
        rptProdN3.DataSource = WITherapeutics.Instance.getInformation(this._editionId, Convert.ToInt32(terN3Row.ItemArray[2].ToString()));
        rptProdN3.DataBind();
        _cont4 += 1;
    }

    protected void rptProdN2_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        DataRowView rowView = (DataRowView)e.Item.DataItem;
        DataRow prodN2Row = rowView.Row;

        _ds.ProductsN2.AddProductsN2Row(prodN2Row.ItemArray[1].ToString(), prodN2Row.ItemArray[6].ToString(), prodN2Row.ItemArray[2].ToString(),
            prodN2Row.ItemArray[3].ToString(), prodN2Row.ItemArray[4].ToString(), prodN2Row.ItemArray[5].ToString(), (WITherapeutic.TherapeuticN2Row)_ds.TherapeuticN2.Rows[_cont3]);

        _ds.WriteXml(System.Configuration.ConfigurationManager.AppSettings["TherapeuticWI"] + "Therapeutics.xml");
    }

    protected void rptProdN3_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        DataRowView rowView = (DataRowView)e.Item.DataItem;
        DataRow prodN3Row = rowView.Row;

        _ds.ProductsN3.AddProductsN3Row(prodN3Row.ItemArray[1].ToString(), prodN3Row.ItemArray[6].ToString(), prodN3Row.ItemArray[2].ToString(),
            prodN3Row.ItemArray[3].ToString(), prodN3Row.ItemArray[4].ToString(), prodN3Row.ItemArray[5].ToString(), (WITherapeutic.TherapeuticN3Row)_ds.TherapeuticN3.Rows[_cont4]);

        _ds.WriteXml(System.Configuration.ConfigurationManager.AppSettings["TherapeuticWI"] + "Therapeutics.xml");
    }


    private int _cont, _cont2, _cont3, _cont4;
    private WITherapeutic _ds = new WITherapeutic();
    private int _editionId;
}
