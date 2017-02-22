using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class CAD_IndiceASCAD : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this._editionId = this.Request.QueryString["Edition"] == null ? -1 : Convert.ToInt32(this.Request.QueryString["Edition"]);

        if (this._editionId > 0)
        {
            this.rptLet.DataSource = ASubstancesCAD.Instance.getAlphabet(this._editionId);
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

        _ds.Image.AddImageRow("file:///C:/Capitulares/" + subsRow.ItemArray[0].ToString().ToLower() + ".tif", (ActSubstancesCAD.LetraRow)_ds.Letra.Rows[_cont]);
        //_ds.Image.AddImageRow("", (ActiveSubstances.LetraRow)_ds.Letra.Rows[_cont]);

        Repeater rptSubs = (Repeater)e.Item.FindControl("rptSubs");
        rptSubs.DataSource = ASubstancesCAD.Instance.getActiveSubs(subsRow.ItemArray[0].ToString(), this._editionId);
        rptSubs.DataBind();
        _cont += 1;

    }

    protected void rptSubs_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        DataRowView rowView = (DataRowView)e.Item.DataItem;
        DataRow subsRow = rowView.Row;

        _ds.Substances.AddSubstancesRow(subsRow.ItemArray[0].ToString(), (ActSubstancesCAD.LetraRow)_ds.Letra.Rows[_cont]);

        Repeater rptProd = (Repeater)e.Item.FindControl("rptProds");
        rptProd.DataSource = ASubstancesCAD.Instance.getInfor(Convert.ToInt32(subsRow.ItemArray[1].ToString()), this._editionId);
        rptProd.DataBind();
        _cont2 += 1;
    }

    protected void rptProds_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        DataRowView rowView = (DataRowView)e.Item.DataItem;
        DataRow infoRow = rowView.Row;

        if (infoRow.ItemArray[3].ToString() == "")
            _ds.Solo.AddSoloRow(infoRow.ItemArray[1].ToString(), infoRow.ItemArray[7].ToString(), infoRow.ItemArray[4].ToString(),
                infoRow.ItemArray[5].ToString(), infoRow.ItemArray[6].ToString(), (ActSubstancesCAD.SubstancesRow)_ds.Substances.Rows[_cont2]);

        else
        {
            string substances = setToken(infoRow.ItemArray[3].ToString());

            _ds.Combinado.AddCombinadoRow(infoRow.ItemArray[1].ToString(), infoRow.ItemArray[7].ToString(), 
                //substances,
                infoRow.ItemArray[3].ToString(),
                infoRow.ItemArray[4].ToString(), infoRow.ItemArray[5].ToString(), infoRow.ItemArray[6].ToString(), (ActSubstancesCAD.SubstancesRow)_ds.Substances.Rows[_cont2]);

        }
        _ds.WriteXml(System.Configuration.ConfigurationManager.AppSettings["ActiveSubstCAD"] + "ActiveSubs.xml");



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

    private int _cont, _cont2;
    private ActSubstancesCAD _ds = new ActSubstancesCAD();
    private int _editionId;
}
