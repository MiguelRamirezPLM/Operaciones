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
            
            this.rptLet.DataSource = GetPEV.Instance.getAlphabetBySub(this._editionId);
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
        rptSubs.DataSource = GetPEV.Instance.getSubstances(_editionId,letra.ItemArray[0].ToString());
        rptSubs.DataBind();
        _cont+=1;

    }
    protected void rptSubs_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        DataRowView rowView = (DataRowView)e.Item.DataItem;
        DataRow subs = rowView.Row;
        _ds.Substances.AddSubstancesRow(subs.ItemArray[0].ToString(),(XMLSubsPEV.LetterRow)_ds.Letter.Rows[_cont]);
        Repeater rptProds= (Repeater)e.Item.FindControl("rptProds");
        rptProds.DataSource = GetPEV.Instance.getInfor(_editionId , subs.ItemArray[1].ToString());
        rptProds.DataBind();
        _cont2+=1;
    }
    protected void rptProds_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        DataRowView rowView = (DataRowView)e.Item.DataItem;
        DataRow Prods = rowView.Row;

        if(Prods.ItemArray[2]== "")
            _ds.Solo.AddSoloRow(Prods.ItemArray[1].ToString(),Prods.ItemArray[3].ToString(),Prods.ItemArray[4].ToString(),
                Prods.ItemArray[5].ToString(),(XMLSubsPEV.SubstancesRow)_ds.Substances.Rows[_cont2]);
        else
            _ds.Combinado.AddCombinadoRow(Prods.ItemArray[1].ToString(),setToken( Prods.ItemArray[2].ToString()),Prods.ItemArray[3].ToString(),Prods.ItemArray[4].ToString(),
                Prods.ItemArray[5].ToString(),(XMLSubsPEV.SubstancesRow)_ds.Substances.Rows[_cont2]);

        _ds.WriteXml(System.Configuration.ConfigurationManager.AppSettings["General"] + "SubsPEVPRU.xml");
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
            substances = substances.Replace("(vitaminas b1, b6 y b12)", "(vitaminas b#1, b#6 y b#12)");
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

    private int _cont, _cont2;
    private XMLSubsPEV _ds = new XMLSubsPEV();
    private int _editionId;
}
