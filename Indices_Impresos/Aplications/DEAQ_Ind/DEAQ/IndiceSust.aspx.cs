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

public partial class DEAQ_IndiceSust : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.Request.QueryString["Edition"] != null)
        {
            this._editionId = Convert.ToInt32(this.Request.QueryString["Edition"]);

            this.rptLet.DataSource = IndiceSustancias.Instance.getAlphabet(this._editionId);
            this.rptLet.DataBind();
            _cont = 0;
            _cont2 = 0;
        }

        this.Response.Redirect("Default.aspx"); 
    }

    protected void ddlLetra_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void rptLet_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        DataRowView rowView = (DataRowView)e.Item.DataItem;
        DataRow subsRow = rowView.Row;

        Repeater rptSubs = (Repeater)e.Item.FindControl("rptSubs");
        rptSubs.DataSource = IndiceSustancias.Instance.getSubstances(subsRow.ItemArray[0].ToString(),this._editionId);
        rptSubs.DataBind();
        _cont += 1;

    }

    protected void rptSubs_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        DataRowView rowView = (DataRowView)e.Item.DataItem;
        DataRow subsRow = rowView.Row;

        _ds.Sustancias.AddSustanciasRow(subsRow.ItemArray[1].ToString());

        Repeater rptProd = (Repeater)e.Item.FindControl("rptProds");
        rptProd.DataSource = IndiceSustancias.Instance.getProducts(Convert.ToInt32(subsRow.ItemArray[0].ToString()), this._editionId);
        rptProd.DataBind();
        _cont2 += 1;
    }

    protected void rptProds_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        DataRowView rowView = (DataRowView)e.Item.DataItem;
        DataRow infoRow = rowView.Row;

        //if (infoRow.ItemArray[1].ToString() == "")
        //    _ds.Solo.AddSoloRow(infoRow.ItemArray[0].ToString(), infoRow.ItemArray[2].ToString(),
        //        infoRow.ItemArray[3].ToString(), infoRow.ItemArray[4].ToString(), (IndSustancias.SustanciasRow)_ds.Sustancias.Rows[_cont2]);

        //else
        //    _ds.Combinado.AddCombinadoRow(infoRow.ItemArray[0].ToString(), infoRow.ItemArray[1].ToString(),
        //        infoRow.ItemArray[2].ToString(), infoRow.ItemArray[3].ToString(), infoRow.ItemArray[4].ToString(), (IndSustancias.SustanciasRow)_ds.Sustancias.Rows[_cont2]);


        //peru
        if (infoRow.ItemArray[1].ToString() == "")
            _ds.Solo.AddSoloRow(infoRow.ItemArray[0].ToString(), infoRow.ItemArray[2].ToString(),
                infoRow.ItemArray[3].ToString(), infoRow.ItemArray[4].ToString(), (IndSustancias.SustanciasRow)_ds.Sustancias.Rows[_cont2]);

        else
            _ds.Combinado.AddCombinadoRow(infoRow.ItemArray[0].ToString(), infoRow.ItemArray[1].ToString(),
                infoRow.ItemArray[2].ToString(), infoRow.ItemArray[3].ToString(), infoRow.ItemArray[4].ToString(), (IndSustancias.SustanciasRow)_ds.Sustancias.Rows[_cont2]);


        _ds.WriteXml(System.Configuration.ConfigurationManager.AppSettings["DEAQGen"] + "IngActivosDIPO.xml");
        //PERU


    }


    private int _cont, _cont2;
    private int _editionId;
    private IndSustancias _ds = new IndSustancias();
}