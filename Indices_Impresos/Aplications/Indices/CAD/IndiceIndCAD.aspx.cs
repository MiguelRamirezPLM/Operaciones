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

public partial class CAD_IndiceIndCAD : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this._editionId = this.Request.QueryString["Edition"] == null ? -1 : Convert.ToInt32(this.Request.QueryString["Edition"]);

        if (this._editionId > 0)
        {
            this.rptLet.DataSource = IndiCAD.Instance.getAlphabet(this._editionId);
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
        DataRow indRow = rowView.Row;

        _ds.Letra.AddLetraRow(indRow.ItemArray[1].ToString());

        _ds.Image.AddImageRow("file:///C:/Letras2/Capitulares/" + indRow.ItemArray[0].ToString().ToLower() + ".tif", (IndicationsCAD.LetraRow)_ds.Letra.Rows[_cont]);

        Repeater rptInd = (Repeater)e.Item.FindControl("rptInd");
        rptInd.DataSource = IndiCAD.Instance.getIndications(indRow.ItemArray[0].ToString(), this._editionId);
        rptInd.DataBind();
        _cont += 1;

    }




    protected void rptInd_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        DataRowView rowView = (DataRowView)e.Item.DataItem;
        DataRow indRow = rowView.Row;

        _ds.Indication.AddIndicationRow(indRow.ItemArray[0].ToString(), (IndicationsCAD.LetraRow)_ds.Letra.Rows[_cont]);

        Repeater rptProd = (Repeater)e.Item.FindControl("rptProds");
        rptProd.DataSource = IndiCAD.Instance.getInformation(Convert.ToInt32(indRow.ItemArray[1].ToString()), this._editionId);
        rptProd.DataBind();
        _cont2 += 1;

    }

    protected void rptProds_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        DataRowView rowView = (DataRowView)e.Item.DataItem;
        DataRow infRow = rowView.Row;

        _ds.Product.AddProductRow(infRow.ItemArray[1].ToString(),infRow.ItemArray[4].ToString(), infRow.ItemArray[2].ToString(), infRow.ItemArray[3].ToString(),
            (IndicationsCAD.IndicationRow)_ds.Indication.Rows[_cont2]);

        _ds.WriteXml(System.Configuration.ConfigurationManager.AppSettings["IndicationsCAD"] + "Indicaciones.xml");

    }

    private int _cont, _cont2;
    private IndicationsCAD _ds = new IndicationsCAD();
    private int _editionId;    
}
