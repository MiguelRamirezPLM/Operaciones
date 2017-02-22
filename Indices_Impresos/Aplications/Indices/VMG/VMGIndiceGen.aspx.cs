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

public partial class VMG_VMGIndiceGen : System.Web.UI.Page
{
    
        protected void Page_Load(object sender, EventArgs e)
    {
        this._editionId = this.Request.QueryString["Edition"] == null ? -1 : Convert.ToInt32(this.Request.QueryString["Edition"]);

        if (this._editionId > 0)
        {
            this.rptLet.DataSource = VMGenIndice.Instance.getAlphabet(this._editionId);
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
        rptProd.DataSource = VMGenIndice.Instance.getProducts(this._editionId, letraRow.ItemArray[0].ToString());
        rptProd.DataBind();
        _cont += 1;

    }

    protected void rptProd_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        DataRowView rowView = (DataRowView)e.Item.DataItem;
        DataRow prodRow = rowView.Row;

        _ds.Producto.AddProductoRow(prodRow.ItemArray[0].ToString(), prodRow.ItemArray[1].ToString(),
            prodRow.ItemArray[2].ToString(),
            (VMGGeneral.LetraRow)_ds.Letra.Rows[_cont]);


        _ds.WriteXml(System.Configuration.ConfigurationManager.AppSettings["VMGGeneral"] + "VMGGeneral.xml");

    }
    private int _cont;
    private VMGGeneral _ds = new VMGGeneral();
    private int _editionId;
}
