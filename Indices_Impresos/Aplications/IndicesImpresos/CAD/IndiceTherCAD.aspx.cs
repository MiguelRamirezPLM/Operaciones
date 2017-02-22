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
public partial class CAD_IndiceTherCAD : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this._editionId = this.Request.QueryString["Edition"] == null ? -1 : Convert.ToInt32(this.Request.QueryString["Edition"]);

        if (this._editionId > 0)
        {
            this.rptTerN0.DataSource = TheraCAD.Instance.getAlphabet();
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
        rptTherN1.DataSource = TheraCAD.Instance.getTherapeuticsByParentId(Convert.ToInt32(terN0Row.ItemArray[2].ToString()));
        rptTherN1.DataBind();
        _cont += 1;

    }

    protected void rptTerN1_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        DataRowView rowView = (DataRowView)e.Item.DataItem;
        DataRow terN1Row = rowView.Row;

        _ds.TherapeuticN1.AddTherapeuticN1Row(terN1Row.ItemArray[1].ToString().ToUpper() + " - " + terN1Row.ItemArray[0].ToString().ToUpper(),
            (TherapeuticCAD.TherapeuticN0Row)_ds.TherapeuticN0.Rows[_cont]);

        Repeater rptTherN2 = (Repeater)e.Item.FindControl("rptTerN2");
        rptTherN2.DataSource = TheraCAD.Instance.getTherapeuticsByParentId(Convert.ToInt32(terN1Row.ItemArray[2].ToString()));
        rptTherN2.DataBind();
        _cont2 += 1;

    }

    protected void rptTerN2_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        DataRowView rowView = (DataRowView)e.Item.DataItem;
        DataRow terN2Row = rowView.Row;

        _ds.TherapeuticN2.AddTherapeuticN2Row(terN2Row.ItemArray[1].ToString() + " - " + terN2Row.ItemArray[0].ToString(),
            (TherapeuticCAD.TherapeuticN1Row)_ds.TherapeuticN1.Rows[_cont2]);

        Repeater rptProdN2 = (Repeater)e.Item.FindControl("rptProdN2");
        rptProdN2.DataSource = TheraCAD.Instance.getInformation(Convert.ToInt32(terN2Row.ItemArray[2].ToString()), this._editionId);
        rptProdN2.DataBind();

        Repeater rptTerN3 = (Repeater)e.Item.FindControl("rptTerN3");
        rptTerN3.DataSource = TheraCAD.Instance.getTherapeuticsByParentId(Convert.ToInt32(terN2Row.ItemArray[2].ToString()));
        rptTerN3.DataBind();
        _cont3 += 1;

    }

    protected void rptTerN3_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        DataRowView rowView = (DataRowView)e.Item.DataItem;
        DataRow terN3Row = rowView.Row;

        _ds.TherapeuticN3.AddTherapeuticN3Row(terN3Row.ItemArray[1].ToString() + " - " + terN3Row.ItemArray[0].ToString(),
            (TherapeuticCAD.TherapeuticN2Row)_ds.TherapeuticN2.Rows[_cont3]);

        Repeater rptProdN3 = (Repeater)e.Item.FindControl("rptProdN3");
        rptProdN3.DataSource = TheraCAD.Instance.getInformation(Convert.ToInt32(terN3Row.ItemArray[2].ToString()), this._editionId);
        rptProdN3.DataBind();
        _cont4 += 1;
    }

    protected void rptProdN2_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        DataRowView rowView = (DataRowView)e.Item.DataItem;
        DataRow prodN2Row = rowView.Row;

        string substances = setToken(prodN2Row.ItemArray[2].ToString());

        _ds.ProductsN2.AddProductsN2Row(prodN2Row.ItemArray[1].ToString(),prodN2Row.ItemArray[6].ToString(), 
            //substances,
            prodN2Row.ItemArray[2].ToString(),
            prodN2Row.ItemArray[3].ToString(), prodN2Row.ItemArray[4].ToString(), prodN2Row.ItemArray[5].ToString(), (TherapeuticCAD.TherapeuticN2Row)_ds.TherapeuticN2.Rows[_cont3]);

        _ds.WriteXml(System.Configuration.ConfigurationManager.AppSettings["TherapeuticCAD"] + "Therapeutics.xml");
    }

    protected void rptProdN3_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        DataRowView rowView = (DataRowView)e.Item.DataItem;
        DataRow prodN3Row = rowView.Row;

        string substances = setToken(prodN3Row.ItemArray[2].ToString());

        _ds.ProductsN3.AddProductsN3Row(prodN3Row.ItemArray[1].ToString(),prodN3Row.ItemArray[6].ToString(), 
            //substances,
            prodN3Row.ItemArray[2].ToString(),
            prodN3Row.ItemArray[3].ToString(), prodN3Row.ItemArray[4].ToString(), prodN3Row.ItemArray[5].ToString(), (TherapeuticCAD.TherapeuticN3Row)_ds.TherapeuticN3.Rows[_cont4]);

        _ds.WriteXml(System.Configuration.ConfigurationManager.AppSettings["TherapeuticCAD"] + "Therapeutics.xml");
    }

    private string setToken(string substances)
    {
        if (substances.IndexOf("Vitamina ") != -1)
        {
            substances = substances.Replace("Vitamina a", "Vitamina A");
            substances = substances.Replace("Vitamina b-", "Vitamina B-");
            substances = substances.Replace("Vitamina b1 ", "Vitamina B#1 ");
            substances = substances.Replace("Vitamina b12 ", "Vitamina B#12 ");
            substances = substances.Replace("Vitamina b2 ", "Vitamina B#2 ");
            substances = substances.Replace("Vitamina b3 ", "Vitamina B#3 ");
            substances = substances.Replace("Vitamina b5 ", "Vitamina B#5 ");
            substances = substances.Replace("Vitamina b6 ", "Vitamina B#6 ");
            substances = substances.Replace("Vitamina d ", "Vitamina D ");
            substances = substances.Replace("Vitamina d2 ", "Vitamina D#2 ");
            substances = substances.Replace("Vitamina d3 ", "Vitamina D#3 ");
            substances = substances.Replace("Vitamina e ", "Vitamina E ");
            substances = substances.Replace("Vitamina k ", "Vitamina K ");
            substances = substances.Replace("Vitamina k1 ", "Vitamina K#1 ");
            substances = substances.Replace("Vitamina c ", "Vitamina C ");
            substances = substances.Replace("Vitamina b15 ", "Vitamina B#15 ");
            substances = substances.Replace("complejo (b1, b6, b12)", "complejo (B#1, B#6, B#12)");
            substances = substances.Replace("Vitaminas b1, b6, b12 ", "Vitaminas B#1, B#6, B#12 ");
            substances = substances.Replace("Amfotericina b ", "Amfotericina B ");
            substances = substances.Replace("Polomixina b ", "Polomixina B ");
            substances = substances.Replace("(véase vitamina b12)", "(véase Vitamina B#12)");
            substances = substances.Replace("Edta", "EDTA");
            substances = substances.Replace("(gabob)", "(GABOB)");
            substances = substances.Replace("(gaba)", "(GABA)");
            substances = substances.Replace("Liposomas de q10", "Liposomas de Q#10");
            substances = substances.Replace("(ver vitamina b2)", "(ver Vitamina B#2)");
        }
        return substances;

    }


    private int _cont, _cont2, _cont3, _cont4;
    private TherapeuticCAD _ds = new TherapeuticCAD();
    private int _editionId;
}
