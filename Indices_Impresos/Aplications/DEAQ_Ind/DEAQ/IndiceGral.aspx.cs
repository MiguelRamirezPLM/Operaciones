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

public partial class DEAQ_IndiceGral : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this._editionId = this.Request.QueryString["Edition"] == null ? -1 : Convert.ToInt32(this.Request.QueryString["Edition"]);

        if (this._editionId > 0)
        {
            this.rptLet.DataSource = IndiceGeneral.Instance.getAlphabet(this._editionId);
            this.rptLet.DataBind();
            _cont = 0;
        }

        this.Response.Redirect("Default.aspx"); 
    }

    protected void rptLet_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        DataRowView rowView = (DataRowView)e.Item.DataItem;
        DataRow letraRow = rowView.Row;

       
        _ds.General.AddGeneralRow(letraRow.ItemArray[0].ToString());

        _ds.Image.AddImageRow("file:///C:/Índices/Capitulares/" + letraRow.ItemArray[0].ToString().ToLower() + ".tif", (IndGral.GeneralRow)_ds.General.Rows[_cont]);
        
        Repeater rptProd = (Repeater)e.Item.FindControl("rptProd");

        if (letraRow[1].ToString() == "11")
        {
            rptProd.DataSource = IndiceGeneral.Instance.getProductsM(letraRow.ItemArray[0].ToString(), this._editionId);
        }
        else
        {
            rptProd.DataSource = IndiceGeneral.Instance.getProducts(letraRow.ItemArray[0].ToString(), this._editionId);
        }


        
        rptProd.DataBind();
        _cont += 1;

    }

    protected void rptProd_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        DataRowView rowView = (DataRowView)e.Item.DataItem;
        DataRow prodRow = rowView.Row;


        if(prodRow.ItemArray[5].ToString().Equals(""))
            _ds.Producto.AddProductoRow("", prodRow.ItemArray[0].ToString(), prodRow.ItemArray[1].ToString(),
                prodRow.ItemArray[2].ToString(), prodRow.ItemArray[3].ToString(), prodRow.ItemArray[4].ToString(),"", (IndGral.GeneralRow)_ds.General.Rows[_cont]);
        else
            _ds.Producto.AddProductoRow("*", prodRow.ItemArray[0].ToString(), prodRow.ItemArray[1].ToString(),
                prodRow.ItemArray[2].ToString(), prodRow.ItemArray[3].ToString(), prodRow.ItemArray[4].ToString(), prodRow.ItemArray[5].ToString(), (IndGral.GeneralRow)_ds.General.Rows[_cont]);

        _ds.WriteXml(System.Configuration.ConfigurationManager.AppSettings["DEAQGen"] + "GeneralDIPO.xml");

    }

    private int _cont;
    private IndGral _ds = new IndGral();
    private int _editionId;

}
