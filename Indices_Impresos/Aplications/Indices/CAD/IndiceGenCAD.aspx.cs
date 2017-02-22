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
using Accessibility;

public partial class CAD_IndiceGenCAD : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this._editionId = this.Request.QueryString["Edition"] == null ? -1 : Convert.ToInt32(this.Request.QueryString["Edition"]);

        if (this._editionId > 0)
        {
            this.rptLet.DataSource = GenCAD.Instance.getAlphabet(this._editionId);
            this.rptLet.DataBind();
            _cont = 0;
        }
    }

    protected void rptLet_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        DataRowView rowView = (DataRowView)e.Item.DataItem;
        DataRow letraRow = rowView.Row;

        _ds.Letra.AddLetraRow(letraRow.ItemArray[0].ToString());

        _ds.Image.AddImageRow("file:///C:/Capitulares/" + letraRow.ItemArray[0].ToString().ToLower() + ".tif", (GeneralCAD.LetraRow)_ds.Letra.Rows[_cont]);
        //_ds.Image.AddImageRow("", (GenIndices.LetraRow)_ds.Letra.Rows[_cont]);

        Repeater rptProd = (Repeater)e.Item.FindControl("rptProd");
        rptProd.DataSource = GenCAD.Instance.getProducts(letraRow.ItemArray[0].ToString(), this._editionId);
        rptProd.DataBind();
        _cont += 1;

    }

    protected void rptProd_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        DataRowView rowView = (DataRowView)e.Item.DataItem;
        DataRow prodRow = rowView.Row;

        string substances = setToken(prodRow.ItemArray[3].ToString());
        

        _ds.Product.AddProductRow(prodRow.ItemArray[1].ToString(),prodRow.ItemArray[7].ToString(), prodRow.ItemArray[2].ToString(), 
            //substances,
            prodRow.ItemArray[3].ToString(),
            prodRow.ItemArray[4].ToString(), prodRow.ItemArray[5].ToString(), prodRow.ItemArray[6].ToString(), (GeneralCAD.LetraRow)_ds.Letra.Rows[_cont]);

        _ds.WriteXml(System.Configuration.ConfigurationManager.AppSettings["GeneralCAD"] + "General.xml");

    }

    private string setToken(string substances)
    {
        

        if (substances.IndexOf("Vitamina ") != -1)
        {
            substances = substances.Replace("Vitamina a","Vitamina A");
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

    private int _cont;
    private GeneralCAD _ds = new GeneralCAD();
    private int _editionId;
}
