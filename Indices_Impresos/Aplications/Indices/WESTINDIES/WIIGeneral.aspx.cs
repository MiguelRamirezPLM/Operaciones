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

public partial class WESTINDIES_WIIGeneral : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.Request.QueryString["Edition"] != null)
        {
            this._editionId = Convert.ToInt32(this.Request.QueryString["Edition"]);

            this.rptLet.DataSource = WIGen.Instance.getAlphabet(this._editionId);
            this.rptLet.DataBind();
            _cont = 0;
        }
    }

    protected void rptLet_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        DataRowView rowView = (DataRowView)e.Item.DataItem;
        DataRow letraRow = rowView.Row;

        _ds.Letra.AddLetraRow(letraRow.ItemArray[0].ToString());

        
        Repeater rptProd = (Repeater)e.Item.FindControl("rptProd");
        rptProd.DataSource = WIGen.Instance.getProducts(this._editionId, letraRow.ItemArray[0].ToString());
        rptProd.DataBind();
        _cont += 1;

    }

    protected void rptProd_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        DataRowView rowView = (DataRowView)e.Item.DataItem;
        DataRow prodRow = rowView.Row;

        
        _ds.Product.AddProductRow(prodRow.ItemArray[1].ToString(), prodRow.ItemArray[7].ToString(), prodRow.ItemArray[2].ToString(), prodRow.ItemArray[3].ToString(),
                prodRow.ItemArray[4].ToString(), prodRow.ItemArray[5].ToString(), prodRow.ItemArray[6].ToString(), (WIGeneral.LetraRow)_ds.Letra.Rows[_cont]);

        _ds.WriteXml(System.Configuration.ConfigurationManager.AppSettings["GeneralWI"] + "General.xml");

    }

    private int _cont;
    private WIGeneral _ds = new WIGeneral();
    private int _editionId;
}
