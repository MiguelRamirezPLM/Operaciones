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

public partial class indiceSustancias : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        this._letra = this.Request.QueryString["letra"];
        if (this._letra != null && this._letra != "")
        {
            DEAQTableAdapters.ProdSustTableAdapter tASust = new DEAQTableAdapters.ProdSustTableAdapter();
            DEAQ.ProdSustDataTable sustancia = tASust.GetSust(this._letra);

            this.lblLetter.Text = "-" + this._letra.ToString().ToUpper() + "-";
            rptLevel_0.DataSource = sustancia;
            rptLevel_0.DataBind();
        }
    }

    protected void rptLevel_0_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        HtmlControl divControl = (HtmlControl)e.Item.FindControl("z_divLevelChildren_0");
        HtmlControl aControl = (HtmlControl)e.Item.FindControl("displayChildren_0");


        aControl.Attributes.Add("onClick", "document.getElementById('" + divControl.ClientID + "').style.display='block'; guardSearch('" + divControl.ClientID + "', 1); return false;");
        aControl.Attributes.Add("onDblClick", "document.getElementById('" + divControl.ClientID + "').style.display='none'; guardSearch('" + divControl.ClientID + "', 0); return false");
        
        
        Repeater rpt = (Repeater)e.Item.FindControl("rptLevel_1");

        DataRowView rowview = (DataRowView)e.Item.DataItem;
        DataRow row = rowview.Row;

        DEAQTableAdapters.ProdSust1TableAdapter tASustaSolos = new DEAQTableAdapters.ProdSust1TableAdapter();
        DEAQ.ProdSust1DataTable sustSolos = tASustaSolos.GetSustanciaSola(row[0].ToString());

        rpt.DataSource = sustSolos; 
        rpt.DataBind();

        Label solos = (Label)e.Item.FindControl("solos");
        if (rpt.Items.Count != 0)
            solos.Visible = true;
        else solos.Visible = false;

        Repeater rpt1 = (Repeater)e.Item.FindControl("rptLevel_2");

        DEAQTableAdapters.ProdSust2TableAdapter tASustCombinado = new DEAQTableAdapters.ProdSust2TableAdapter();
        DEAQ.ProdSust2DataTable sustCombinado = tASustCombinado.GetSustanciaCombinada(row[0].ToString());

        rpt1.DataSource = sustCombinado;
        rpt1.DataBind();

        Label combinados = (Label)e.Item.FindControl("combinados");
        if (rpt1.Items.Count != 0)
            combinados.Visible = true;
        else combinados.Visible = false;

    }

    private string _letra;
}
