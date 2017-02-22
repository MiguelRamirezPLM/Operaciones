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

public partial class PEV_IndRec : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this._editionId = this.Request.QueryString["EditionId"] == null ? -1 : Convert.ToInt32(this.Request.QueryString["EditionId"]);

        if (_editionId > 0)
        {
            this.rptProd.DataSource = GetPEV.Instance.getNewProducts(_editionId);
            this.rptProd.DataBind();
        }
    }


    protected void rptProd_OnItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        DataRowView rowview = (DataRowView)e.Item.DataItem;
        DataRow Prod = rowview.Row;

        _ds.Products.AddProductsRow(Prod.ItemArray[1].ToString(), Prod.ItemArray[2].ToString(), setToken(Prod.ItemArray[3].ToString()), Prod.ItemArray[4].ToString(), Prod.ItemArray[5].ToString());

        _ds.WriteXml(System.Configuration.ConfigurationManager.AppSettings["General"] + "RecPEV.xml");
    }

    private string setToken(string substances)
    {


        if (substances.IndexOf("Vitamina ") != -1)
        {
            substances = substances.Replace("Vitamina a", "Vitamina A");
            substances = substances.Replace("Vitamina b-", "Vitamina B-");
            substances = substances.Replace("Vitamina b1, ", "Vitamina B#1, ");
            substances = substances.Replace("Vitamina b12, ", "Vitamina B#12, ");
            substances = substances.Replace("Vitamina b12", "Vitamina B#12, ");
            substances = substances.Replace("Vitamina b2, ", "Vitamina B#2, ");
            substances = substances.Replace("Vitamina b3, ", "Vitamina B#3, ");
            substances = substances.Replace("Vitamina b5, ", "Vitamina B#5, ");
            substances = substances.Replace("Vitamina b6, ", "Vitamina B#6, ");
            substances = substances.Replace("Vitamina d, ", "Vitamina D, ");
            substances = substances.Replace("Vitamina d2, ", "Vitamina D#2, ");
            substances = substances.Replace("Vitamina d3, ", "Vitamina D#3, ");
            substances = substances.Replace("Vitamina e, ", "Vitamina E, ");
            substances = substances.Replace("Vitamina k, ", "Vitamina K, ");
            substances = substances.Replace("Vitamina k1, ", "Vitamina K#1, ");
            substances = substances.Replace("Vitamina c, ", "Vitamina C, ");
            substances = substances.Replace("Vitamina b15, ", "Vitamina B#15, ");
            substances = substances.Replace("(vitaminas b1, b6 y b12), ", "(vitaminas b#1, b#6 y b#12), ");
            substances = substances.Replace("complejo (b1, b6, b12), ", "complejo (B#1, B#6, B#12), ");
            substances = substances.Replace("Vitaminas b1, b6, b12, ", "Vitaminas B#1, B#6, B#12,  ");
            substances = substances.Replace("Amfotericina b, ", "Amfotericina B, ");
            substances = substances.Replace("Polomixina b, ", "Polomixina B, ");
            substances = substances.Replace("(véase vitamina b12),", "(véase Vitamina B#12),");
            substances = substances.Replace("Edta", "EDTA");
            substances = substances.Replace("(gabob), ", "(GABOB)");
            substances = substances.Replace("(gaba), ", "(GABA)");
            substances = substances.Replace("Liposomas de q10, ", "Liposomas de Q#10");
            substances = substances.Replace("(ver vitamina b2),", "(ver Vitamina B#2)");
        }
        return substances;

    }


    private XMLRecPEV _ds = new XMLRecPEV();
    private int _editionId;
}

