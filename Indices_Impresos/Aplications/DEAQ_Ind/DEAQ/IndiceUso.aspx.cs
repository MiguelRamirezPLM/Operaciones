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

public partial class DEAQ_IndiceUso : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this._editionId = this.Request.QueryString["Edition"] == null ? -1 : Convert.ToInt32(this.Request.QueryString["Edition"]);

        if (this._editionId > 0)
        {
            this.rptLet.DataSource = IndiceUsos.Instance.getAlphabet(this._editionId);
            this.rptLet.DataBind();
            _cont = 0;
            _cont2 = 0;
        }

        this.Response.Redirect("Default.aspx"); 
    }

      //this._editionId = this.Request.QueryString["Edition"] == null ? -1 : Convert.ToInt32(this.Request.QueryString["Edition"]);

      //  if (this._editionId > 0)
      //  {
      //      this.rptLet.DataSource = IndiceGeneral.Instance.getAlphabet(this._editionId);
      //      this.rptLet.DataBind();
      //      _cont = 0;
      //  }


    protected void ddlLetra_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void rptLet_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        DataRowView rowView = (DataRowView)e.Item.DataItem;
        DataRow cultRow = rowView.Row;

        Repeater rptInd = (Repeater)e.Item.FindControl("rptUso");
        rptInd.DataSource = IndiceUsos.Instance.getUsos(cultRow.ItemArray[0].ToString(), this._editionId);
        rptInd.DataBind();
        _cont += 1;

    }




    protected void rptUso_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        DataRowView rowView = (DataRowView)e.Item.DataItem;
        DataRow usoRow = rowView.Row;

        _ds.Usos.AddUsosRow(usoRow.ItemArray[1].ToString());

        Repeater rptProd = (Repeater)e.Item.FindControl("rptProds");
        rptProd.DataSource = IndiceUsos.Instance.getProducts(Convert.ToInt32(usoRow.ItemArray[0].ToString()), this._editionId);
        rptProd.DataBind();
        _cont2 += 1;

    }

    protected void rptProds_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        DataRowView rowView = (DataRowView)e.Item.DataItem;
        DataRow infRow = rowView.Row;

        _ds.Producto.AddProductoRow(infRow.ItemArray[0].ToString(), infRow.ItemArray[1].ToString(), infRow.ItemArray[2].ToString(),
            infRow.ItemArray[3].ToString(), infRow.ItemArray[4].ToString(), (IndUsos.UsosRow)_ds.Usos.Rows[_cont2]);

        _ds.WriteXml(System.Configuration.ConfigurationManager.AppSettings["DEAQGen"] + "UsosDIPO.xml");

    }

    private int _cont, _cont2;
    private IndUsos _ds = new IndUsos();
    private int _editionId;
}
