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

public partial class PEV_IndTheraPEV : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this._editionId = this.Request.QueryString["Edition"] == null ? -1 : Convert.ToInt32(this.Request.QueryString["Edition"]);

        if (this._editionId > 0)
        {
            this.rptTerN0.DataSource = GetPEV.Instance.getAlphabet();
            this.rptTerN0.DataBind();
            _cont = 0;
            _cont2 = 0;
            _cont3 = 0;
            _cont4 = 0;
        }
    }

    protected void rptTerN0_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        DataRowView rowView = (DataRowView)e.Item.DataItem;
        DataRow terN0Row = rowView.Row;


        _ds.TherapeuticsN0.AddTherapeuticsN0Row(terN0Row.ItemArray[1].ToString());

        Repeater rptProdN0 = (Repeater)e.Item.FindControl("rptProdN0");
        rptProdN0.DataSource = GetPEV.Instance.getInformation(Convert.ToInt32(terN0Row.ItemArray[0].ToString()), this._editionId);
        rptProdN0.DataBind();

        Repeater rptTherN1 = (Repeater)e.Item.FindControl("rptTerN1");
        rptTherN1.DataSource = GetPEV.Instance.getTherapeuticsByParentId(Convert.ToInt32(terN0Row.ItemArray[0].ToString()));
        rptTherN1.DataBind();
        _cont += 1;

    }

    protected void rptProdN0_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        DataRowView rowView = (DataRowView)e.Item.DataItem;
        DataRow ProN0Row = rowView.Row;

        _ds.ProductsN0.AddProductsN0Row(ProN0Row.ItemArray[1].ToString(), setToken(ProN0Row.ItemArray[2].ToString()), ProN0Row.ItemArray[3].ToString(),
            ProN0Row.ItemArray[4].ToString(),ProN0Row.ItemArray[5].ToString(), (XMLTheraPEV.TherapeuticsN0Row)_ds.TherapeuticsN0.Rows[_cont]);

        _ds.WriteXml(System.Configuration.ConfigurationManager.AppSettings["General"] + "Therapeutics.xml");
    }

    protected void rptTerN1_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        DataRowView rowView = (DataRowView)e.Item.DataItem;
        DataRow terN1Row = rowView.Row;


        _ds.TherapeuticsN1.AddTherapeuticsN1Row(terN1Row.ItemArray[1].ToString(), (XMLTheraPEV.TherapeuticsN0Row)_ds.TherapeuticsN0.Rows[_cont]);


        Repeater rptProdN1 = (Repeater)e.Item.FindControl("rptProdN1");
        rptProdN1.DataSource = GetPEV.Instance.getInformation(Convert.ToInt32(terN1Row.ItemArray[0].ToString()), this._editionId);
        rptProdN1.DataBind();


        Repeater rptTerN2 = (Repeater)e.Item.FindControl("rptTerN2");
        rptTerN2.DataSource = GetPEV.Instance.getTherapeuticsByParentId(Convert.ToInt32(terN1Row.ItemArray[0].ToString()));
        rptTerN2.DataBind();
        _cont2 += 1;

    }

    protected void rptProdN1_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        DataRowView rowView = (DataRowView)e.Item.DataItem;
        DataRow ProN1Row = rowView.Row;

        _ds.ProductsN1.AddProductsN1Row(ProN1Row.ItemArray[1].ToString(), setToken(ProN1Row.ItemArray[2].ToString()), ProN1Row.ItemArray[3].ToString(),
            ProN1Row.ItemArray[4].ToString(), ProN1Row.ItemArray[5].ToString(), (XMLTheraPEV.TherapeuticsN1Row)_ds.TherapeuticsN1.Rows[_cont2]);

        _ds.WriteXml(System.Configuration.ConfigurationManager.AppSettings["General"] + "Therapeutics.xml");
    }

    protected void rptTerN2_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        DataRowView rowView = (DataRowView)e.Item.DataItem;
        DataRow terN2Row = rowView.Row;


        _ds.TherapeuticsN2.AddTherapeuticsN2Row(terN2Row.ItemArray[1].ToString(), (XMLTheraPEV.TherapeuticsN1Row)_ds.TherapeuticsN1.Rows[_cont2]);

        Repeater rptProdN2 = (Repeater)e.Item.FindControl("rptProdN2");
        rptProdN2.DataSource = GetPEV.Instance.getInformation(Convert.ToInt32(terN2Row.ItemArray[0].ToString()), this._editionId);
        rptProdN2.DataBind();

        Repeater rptTerN3 = (Repeater)e.Item.FindControl("rptTerN3");
        rptTerN3.DataSource = GetPEV.Instance.getTherapeuticsByParentId(Convert.ToInt32(terN2Row.ItemArray[0].ToString()));
        rptTerN3.DataBind();
        _cont3 += 1;

       
    }

    protected void rptProdN2_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        DataRowView rowView = (DataRowView)e.Item.DataItem;
        DataRow prodN2Row = rowView.Row;

        _ds.ProductsN2.AddProductsN2Row(prodN2Row.ItemArray[1].ToString(), setToken(prodN2Row.ItemArray[2].ToString()),
            prodN2Row.ItemArray[3].ToString(), prodN2Row.ItemArray[4].ToString(), prodN2Row.ItemArray[5].ToString(), (XMLTheraPEV.TherapeuticsN2Row)_ds.TherapeuticsN2.Rows[_cont3]);

        _ds.WriteXml(System.Configuration.ConfigurationManager.AppSettings["General"] + "Therapeutics.xml");
    }

    protected void rptTerN3_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        DataRowView rowView = (DataRowView)e.Item.DataItem;
        DataRow terN3Row = rowView.Row;


        _ds.TherapeuticsN3.AddTherapeuticsN3Row(terN3Row.ItemArray[1].ToString(), (XMLTheraPEV.TherapeuticsN2Row)_ds.TherapeuticsN2.Rows[_cont3]);


        Repeater rptProdN3 = (Repeater)e.Item.FindControl("rptProdN3");
        rptProdN3.DataSource = GetPEV.Instance.getInformation(Convert.ToInt32(terN3Row.ItemArray[0].ToString()), this._editionId);
        rptProdN3.DataBind();
        //Agrege este cont
        _cont4 += 1;

    }

    protected void rptProdN3_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        DataRowView rowView = (DataRowView)e.Item.DataItem;
        DataRow prodN3Row = rowView.Row;

        _ds.ProductsN3.AddProductsN3Row(prodN3Row.ItemArray[1].ToString(), setToken(prodN3Row.ItemArray[2].ToString()),
            prodN3Row.ItemArray[3].ToString(), prodN3Row.ItemArray[4].ToString(), prodN3Row.ItemArray[5].ToString(), (XMLTheraPEV.TherapeuticsN3Row)_ds.TherapeuticsN3.Rows[_cont4]);

        _ds.WriteXml(System.Configuration.ConfigurationManager.AppSettings["General"] + "Therapeutics.xml");
    }
    
    private string setToken(string substances)
    {


        //if (substances.IndexOf("Vitamina ") != -1)
        //{
        //    substances = substances.Replace("Vitamina a", "Vitamina A");
        //    substances = substances.Replace("Vitamina b-", "Vitamina B-");
        //    substances = substances.Replace("Vitamina b1 ", "Vitamina B#1 ");
        //    substances = substances.Replace("Vitamina b12 ", "Vitamina B#12 ");
        //    substances = substances.Replace("Vitamina b2 ", "Vitamina B#2 ");
        //    substances = substances.Replace("Vitamina b3 ", "Vitamina B#3 ");
        //    substances = substances.Replace("Vitamina b5 ", "Vitamina B#5 ");
        //    substances = substances.Replace("Vitamina b6 ", "Vitamina B#6 ");
        //    substances = substances.Replace("Vitamina d ", "Vitamina D ");
        //    substances = substances.Replace("Vitamina d2 ", "Vitamina D#2 ");
        //    substances = substances.Replace("Vitamina d3 ", "Vitamina D#3 ");
        //    substances = substances.Replace("Vitamina e ", "Vitamina E ");
        //    substances = substances.Replace("Vitamina k ", "Vitamina K ");
        //    substances = substances.Replace("Vitamina k1 ", "Vitamina K#1 ");
        //    substances = substances.Replace("Vitamina c ", "Vitamina C ");
        //    substances = substances.Replace("Vitamina b15 ", "Vitamina B#15 ");
        //    substances = substances.Replace("complejo (b1, b6, b12)", "complejo (B#1, B#6, B#12)");
        //    substances = substances.Replace("(vitaminas b1, b6 y b12)", "(vitaminas b#1, b#6 y b#12)");
        //    substances = substances.Replace("Vitaminas b1, b6, b12 ", "Vitaminas B#1, B#6, B#12 ");
        //    substances = substances.Replace("Amfotericina b ", "Amfotericina B ");
        //    substances = substances.Replace("Polomixina b ", "Polomixina B ");
        //    substances = substances.Replace("(véase vitamina b12)", "(véase Vitamina B#12)");
        //    substances = substances.Replace("Edta", "EDTA");
        //    substances = substances.Replace("(gabob)", "(GABOB)");
        //    substances = substances.Replace("(gaba)", "(GABA)");
        //    substances = substances.Replace("Liposomas de q10", "Liposomas de Q#10");
        //    substances = substances.Replace("(ver vitamina b2)", "(ver Vitamina B#2)");
        //}
        return substances;

    }


    private int _cont, _cont2, _cont3, _cont4;
    private XMLTheraPEV _ds = new XMLTheraPEV();
    private int _editionId;

}
