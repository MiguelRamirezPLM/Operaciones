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

public partial class indiceProductos : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this._letra = this.Request.QueryString["letra"];
        this._editionId = this.Request.QueryString["Edition"] == null ? -1 : Convert.ToInt32(this.Request.QueryString["Edition"]);

        if (this._letra != "" && this._letra != null)
        {

            DEAQTableAdapters.ProductosTableAdapter tAProducts = new DEAQTableAdapters.ProductosTableAdapter();

            DEAQ.ProductosDataTable product = tAProducts.GetProducts(this._editionId, this._letra);

            rptLevel_0.DataSource = product;
            rptLevel_0.DataBind();
        }
    
    }
    
    protected void rptLevel_0_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {

        
    }

    private string _letra ="";
    private int _editionId; 
}


