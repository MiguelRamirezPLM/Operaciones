using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Indice_Laboratorios : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this._editionId = this.Request.QueryString["Edition"] == null ? -1 : Convert.ToInt32(this.Request.QueryString["Edition"]);

        if (this._editionId > 0)
        {
            this.rptLabs.DataSource = Querys.Instance.getLaboratories(this._editionId);
            this.rptLabs.DataBind();
        }

        this._cont = 0;

    }
    protected void rptLabs_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        Divisions div = (Divisions)e.Item.DataItem;
        
        _ds.Company.AddCompanyRow(div.Description);

        //DivisionInformation divInfo = Querys.Instance.getLabInformation(div.DivisionId);

        //_ds.Address.AddAddressRow(divInfo.Address == null ? "" : divInfo.Address, 
        //    divInfo.Suburb == null ? "" : divInfo.Suburb, 
        //    divInfo.ZipCode== null ? "" : divInfo.ZipCode, 
        //    divInfo.City== null ? "" : divInfo.City, 
        //    divInfo.Telephone== null ? "" : divInfo.Telephone, 
        //    divInfo.Fax == null ? "" : divInfo.Fax, 
        //    divInfo.Email== null ? "" : divInfo.Email,
        //    divInfo.Web == null ? "" : divInfo.Web, 
        //    (Companies.CompanyRow)_ds.Company.Rows[this._cont]);

        Repeater rptProducts = (Repeater)e.Item.FindControl("rptProducts");
        rptProducts.DataSource = Querys.Instance.getProductsByLab(this._editionId, div.DivisionId);
        rptProducts.DataBind();

        this._cont += 1;
    }

    protected void rptProducts_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        ProductByEdition product = (ProductByEdition)e.Item.DataItem;

        _ds.Products.AddProductsRow(product.Brand, (Companies.CompanyRow)_ds.Company.Rows[this._cont]);


        if (!string.IsNullOrEmpty(product.Pages))
        {
            _ds.PProduct.AddPProductRow(product.Brand, product.Description == null ? "" : product.Description, product.PharmaForm, product.Pages, (Companies.ProductsRow)_ds.Products.Rows[_ds.Products.Rows.Count - 1]);
        }
        else
        {
            _ds.MProduct.AddMProductRow(product.Brand, product.Description == null ? "" : product.Description, product.PharmaForm, (Companies.ProductsRow)_ds.Products.Rows[_ds.Products.Rows.Count - 1]);
        }

        _ds.WriteXml("C:/OTC/XML/Laboratorios.xml");

    }

    private int _cont;
    private int _editionId;
    private Companies _ds = new Companies();

}