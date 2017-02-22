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

public partial class NewProducts : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.rptProd.DataSource = NewIndex.Instance.getNewProducts();
        this.rptProd.DataBind();
    }
    
    protected void rptProd_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        DataRowView rowView = (DataRowView)e.Item.DataItem;
        DataRow prodRow = rowView.Row;

        _ds.Products.AddProductsRow(prodRow.ItemArray[1].ToString(),prodRow.ItemArray[4].ToString(),prodRow.ItemArray[2].ToString(),
            prodRow.ItemArray[3].ToString(),prodRow.ItemArray[5].ToString(),prodRow.ItemArray[6].ToString());

        _ds.WriteXml(System.Configuration.ConfigurationManager.AppSettings["General"] + "News.xml");

    }


    private News _ds = new News();

}
