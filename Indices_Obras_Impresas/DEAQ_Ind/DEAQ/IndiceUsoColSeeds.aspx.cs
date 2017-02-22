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

public partial class IndiceUsoCol : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this._editionId = this.Request.QueryString["Edition"] == null ? -1 : Convert.ToInt32(this.Request.QueryString["Edition"]);

        if (this._editionId > 0)
        {
            this.rptLet.DataSource = IndiceUsosCol.Instance.getAlphabetSeeds(this._editionId);
            this.rptLet.DataBind();
            _cont = 0;
            _cont2 = 0;
        }

        this.Response.Redirect("Default.aspx"); 

    }

    protected void rptLet_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        DataRowView rowView = (DataRowView)e.Item.DataItem;
        DataRow cultRow = rowView.Row;

        Repeater rptUso = (Repeater)e.Item.FindControl("rptUso");
        rptUso.DataSource = IndiceUsosCol.Instance.getUsosSeeds(cultRow.ItemArray[0].ToString(), this._editionId);
        rptUso.DataBind();
        

    }
    
    protected void rptUso_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        DataRowView rowView = (DataRowView)e.Item.DataItem;
        DataRow usoRow = rowView.Row;

        _ds.Usos.AddUsosRow(usoRow.ItemArray[1].ToString());

        Repeater rptProd = (Repeater)e.Item.FindControl("rptProds");
        rptProd.DataSource = IndiceUsosCol.Instance.getProductsSeeds(Convert.ToInt32(usoRow.ItemArray[0].ToString()), this._editionId);
        rptProd.DataBind();
        
        Repeater rptSubUso = (Repeater)e.Item.FindControl("rptSubUso");
        rptSubUso.DataSource = IndiceUsosCol.Instance.getSubUsosSeeds(Convert.ToInt32(usoRow.ItemArray[0].ToString()), this._editionId);
        rptSubUso.DataBind();
        _cont += 1;

    }

    protected void rptSubUso_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        DataRowView rowView = (DataRowView)e.Item.DataItem;
        DataRow subUsoRow = rowView.Row;

        _ds.SubUso.AddSubUsoRow(subUsoRow.ItemArray[1].ToString(), (IndUsoCol.UsosRow)_ds.Usos.Rows[_cont]);

        Repeater rptProdN2 = (Repeater)e.Item.FindControl("rptProdsN2");
        rptProdN2.DataSource = IndiceUsosCol.Instance.getProductsSeeds(Convert.ToInt32(subUsoRow.ItemArray[0].ToString()), this._editionId);
        rptProdN2.DataBind();
        _cont2 += 1;

    }

    protected void rptProdsN2_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        DataRowView rowView = (DataRowView)e.Item.DataItem;
        DataRow prodN2Row = rowView.Row;

        _ds.ProductoN2.AddProductoN2Row(prodN2Row.ItemArray[0].ToString(), prodN2Row.ItemArray[3].ToString(), prodN2Row.ItemArray[4].ToString(), (IndUsoCol.SubUsoRow)_ds.SubUso.Rows[_cont2]);

        _ds.WriteXml(System.Configuration.ConfigurationManager.AppSettings["DEAQGen"] + "UsosDEAQSemillas.xml");

    }

    protected void rptProds_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        DataRowView rowView = (DataRowView)e.Item.DataItem;
        DataRow infRow = rowView.Row;

        _ds.Producto.AddProductoRow(infRow.ItemArray[0].ToString(), infRow.ItemArray[3].ToString(), infRow.ItemArray[4].ToString(), (IndUsoCol.UsosRow)_ds.Usos.Rows[_cont]);

        _ds.WriteXml(System.Configuration.ConfigurationManager.AppSettings["DEAQGen"] + "UsosDEAQSemillas.xml");

    }


    private int _cont, _cont2, _editionId;
    private IndUsoCol _ds = new IndUsoCol();
}
