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
using System.Xml;
using System.IO;


public partial class ProductCIEATC : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this._editionId = this.Request.QueryString["Edition"] == null ? -1 : Convert.ToInt32(this.Request.QueryString["Edition"]);

        if (this._editionId > 0)
        { 

            this.rptLet.DataSource = GenIndice.Instance.getAlpha(this._editionId);
            this.rptLet.DataBind();
             
            _cont = 0;
        }

        btnBack.Attributes.Add("onclick", "history.back(); return false;");
    }

    protected void rptLet_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        DataRowView rowView = (DataRowView)e.Item.DataItem;
        DataRow letraRow = rowView.Row;

       _ds.Letras.AddLetrasRow(letraRow.ItemArray[0].ToString());
          

        Repeater rptProd = (Repeater)e.Item.FindControl("rptProd");
        rptProd.DataSource = GenIndice.Instance.getProductATCICD(this._editionId, letraRow.ItemArray[0].ToString());
        rptProd.DataBind();
        _cont += 1;


    }

    protected void rptProd_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        DataRowView rowView = (DataRowView)e.Item.DataItem;
        DataRow ProdRow = rowView.Row;

        _ds.Brand.AddBrandRow(ProdRow.ItemArray[1].ToString(),ProdRow.ItemArray[3].ToString(),ProdRow.ItemArray[4].ToString(), ProdRow.ItemArray[5].ToString(), ProdRow.ItemArray[7].ToString(),
            ProdRow.ItemArray[8].ToString(), ProdRow.ItemArray[6].ToString(), (ProdCIEATC.LetrasRow)_ds.Letras.Rows[_cont]);

        //_ds.Productos.AddProductoRow(ProdRow.ItemArray[1].ToString(), ProdRow.ItemArray[2].ToString(),
        //   setToken(prodRow.ItemArray[3].ToString()),
        //    //prodRow.ItemArray[3].ToString(),
        //   prodRow.ItemArray[4].ToString(), prodRow.ItemArray[5].ToString(), ProdRow.ItemArray[6].ToString(), (GenIndices.LetraRow)_ds.Letra.Rows[_cont]);

        _ds.WriteXml(System.Configuration.ConfigurationManager.AppSettings["General"] + "PoductocATCICD.xml");



            // _ds.Producto.AddProductoRow(prodRow.ItemArray[0].ToString(), prodRow.ItemArray[1].ToString(),

        Repeater rptProd = (Repeater)e.Item.FindControl("rptProd");
        //rptProd.DataSource = GenIndice.Instance.getProductContraindications(this._editionId, letraRow.ItemArray[0].ToString());
        //rptProd.DataBind();
        //_cont += 1;
    }

    private int _cont;

    private ProdCIEATC _ds = new ProdCIEATC(); 

    private int _editionId;

}