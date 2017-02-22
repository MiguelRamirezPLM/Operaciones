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


public partial class indProductosYServicios : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this._editionId = this.Request.QueryString["Edition"] == null ? -1 : Convert.ToInt32(this.Request.QueryString["Edition"]);
        {
            this.rptLetterPyS.DataSource = Catalogs.Instance.getLettersPyS(_editionId);
            this.rptLetterPyS.DataBind();
            this._cont = 0;
        }

        this.Response.Redirect("general.aspx");
    }

    protected void rptLetterPyS_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        DataRowView rowView = (DataRowView)e.Item.DataItem;
        DataRow rowLet = rowView.Row;
         
        _ds.ProdCat.AddProdCatRow(rowLet.ItemArray[0].ToString());
         
        Repeater rptCatN3 = (Repeater)e.Item.FindControl("rptCatN3");
        rptCatN3.DataSource = Catalogs.Instance.getCatN3(rowLet.ItemArray[0].ToString(), _editionId);
        rptCatN3.DataBind();

        _cont += 1;
         
    }


    protected void rptCatN3_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        DataRowView rowView = (DataRowView)e.Item.DataItem;
        DataRow rowCatN3 = rowView.Row; 

        _ds.CategoryP.AddCategoryPRow(rowCatN3.ItemArray[1].ToString(), (IndProductCat.ProdCatRow)_ds.ProdCat.Rows[_cont]);

        Repeater rptCatN4 = (Repeater)e.Item.FindControl("rptCatN4");
        rptCatN4.DataSource = Catalogs.Instance.getCatN4( Convert.ToInt32(rowCatN3.ItemArray[0]), _editionId);
        rptCatN4.DataBind();

        _cont1 += 1;
    }

    protected void rptCatN4_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        DataRowView rowView = (DataRowView)e.Item.DataItem;
        DataRow rowCatN4 = rowView.Row;

        _ds.CategoryPN4.AddCategoryPN4Row(rowCatN4.ItemArray[1].ToString(),(IndProductCat.CategoryPRow)_ds.CategoryP.Rows[_cont1]);

        Repeater rptClientC = (Repeater)e.Item.FindControl("rptClientC");
        rptClientC.DataSource = Catalogs.Instance.getClientC(Convert.ToInt32(rowCatN4.ItemArray[0]), _editionId);
        rptClientC.DataBind();

        _cont2 += 1;
    }

    protected void rptClientC_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        DataRowView rowView = (DataRowView)e.Item.DataItem;
        DataRow rowClientC = rowView.Row;

        _ds.ClientCategory.AddClientCategoryRow(rowClientC.ItemArray[1].ToString(), rowClientC.ItemArray[2].ToString(), (IndProductCat.CategoryPN4Row)_ds.CategoryPN4.Rows[_cont2]);

        Repeater rptProducto = (Repeater)e.Item.FindControl("rptProducto");
        rptProducto.DataSource = Catalogs.Instance.getProductoC(Convert.ToInt32(rowClientC.ItemArray[0]), _editionId, Convert.ToInt32(rowClientC.ItemArray[3]));
        rptProducto.DataBind();

        _cont3 += 1;
    }

    protected void rptProducto_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        DataRowView rowView = (DataRowView)e.Item.DataItem;
        DataRow rowProdC = rowView.Row;

        _ds.ProdCategory.AddProdCategoryRow(rowProdC.ItemArray[1].ToString(), rowProdC.ItemArray[2].ToString(), (IndProductCat.ClientCategoryRow)_ds.ClientCategory.Rows[_cont3]);

        _ds.WriteXml(System.Configuration.ConfigurationManager.AppSettings["XML"] + "ProductosCatego.xml");

        //Repeater rptProducto = (Repeater)e.Item.FindControl("rptProducto");
        //rptProducto.DataSource = Catalogs.Instance.getProductoC(Convert.ToInt32(rowClientC.ItemArray[0]), _editionId, Convert.ToInt32(rowClientC.ItemArray[3]));
        //rptProducto.DataBind();

      //  _cont3 += 1;
    }


    private int _editionId;
    private int _cont;
    private int _cont1;
    private int _cont2;
    private int _cont3;

    private VentasC _ds3 = new VentasC();

    private IndProductCat _ds = new IndProductCat();
}