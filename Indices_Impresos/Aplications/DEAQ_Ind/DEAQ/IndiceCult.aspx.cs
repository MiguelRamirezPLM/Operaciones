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

public partial class DEAQ_IndiceCult : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this._editionId = this.Request.QueryString["Edition"] == null ? -1 : Convert.ToInt32(this.Request.QueryString["Edition"]);

        if (this._editionId > 0)
        {
            this.rptLet.DataSource = IndiceCultivos.Instance.getAlphabet(this._editionId);
            this.rptLet.DataBind();
            _cont = 0;
            _cont2 = 0;
        }
        
        this.Response.Redirect("Default.aspx"); 

    }

    protected void ddlLetra_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void rptLet_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        DataRowView rowView = (DataRowView)e.Item.DataItem;
        DataRow cultRow = rowView.Row;

        Repeater rptInd = (Repeater)e.Item.FindControl("rptCult");

        if (cultRow.ItemArray[1].ToString() == "11")
        {
            rptInd.DataSource = IndiceCultivos.Instance.getCultivo(cultRow.ItemArray[0].ToString(), this._editionId);
        }

        else
        {
            rptInd.DataSource = IndiceCultivos.Instance.getCultivoCol(cultRow.ItemArray[0].ToString(), this._editionId);
        }
          
        rptInd.DataBind();
        _cont += 1;

    }
     

    protected void rptCult_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        DataRowView rowView = (DataRowView)e.Item.DataItem;
        DataRow cultRow = rowView.Row;

        _ds.Cultivo.AddCultivoRow(cultRow.ItemArray[1].ToString());

        //_dsC.Cultivo.AddCultivoRow(cultRow.ItemArray[1].ToString());

        Repeater rptProd = (Repeater)e.Item.FindControl("rptProds");

        if (cultRow.ItemArray[2].ToString() == "11")
        {
            rptProd.DataSource = IndiceCultivos.Instance.getProducts(Convert.ToInt32(cultRow.ItemArray[0].ToString()), this._editionId);
        }
        
        else
        {
            rptProd.DataSource = IndiceCultivos.Instance.getProductsCol(Convert.ToInt32(cultRow.ItemArray[0].ToString()), this._editionId);
        }

        rptProd.DataBind();
        _cont2 += 1;

    }

    protected void rptProds_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        DataRowView rowView = (DataRowView)e.Item.DataItem;
        DataRow infRow = rowView.Row;

        _ds.Producto.AddProductoRow(infRow.ItemArray[0].ToString(), infRow.ItemArray[1].ToString(), infRow.ItemArray[2].ToString(),
            infRow.ItemArray[3].ToString(), infRow.ItemArray[4].ToString(), (IndCultivos.CultivoRow)_ds.Cultivo.Rows[_cont2]);

        //Peru
       // _dsC.Producto.AddProductoRow(infRow.ItemArray[0].ToString(), infRow.ItemArray[1].ToString(),
         //   infRow.ItemArray[2].ToString(), (IndCultivosCol.CultivoRow)_dsC.Cultivo.Rows[_cont2]);
        //Peru

        //_dsC.Producto.AddProductoRow(infRow.ItemArray[0].ToString(), infRow.ItemArray[1].ToString(),
        //  infRow.ItemArray[2].ToString(), (IndCultivosCol.CultivoRow)_dsC.Cultivo.Rows[_cont2]);

        _ds.WriteXml(System.Configuration.ConfigurationManager.AppSettings["DEAQGen"] + "CultivosDIPO.xml");

       // _dsC.WriteXml(System.Configuration.ConfigurationManager.AppSettings["Cultivos"] + "CultivosSemilas.xml");

    }

    private int _cont, _cont2;
    private IndCultivos _ds = new IndCultivos();
    private IndCultivosCol _dsC = new IndCultivosCol();
    private int _editionId;
}
