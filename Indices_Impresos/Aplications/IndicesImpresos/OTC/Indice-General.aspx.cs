using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Indice_General : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this._editionId = this.Request.QueryString["Edition"] == null ? -1 : Convert.ToInt32(this.Request.QueryString["Edition"]);

        if (this._editionId > 0)
        {
            this.rptAlphabet.DataSource = Querys.Instance.getAlphabet(this._editionId);
            this.rptAlphabet.DataBind();
            this._cont = 0;
        }


    }




    protected void rptAlphabet_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        string letter = e.Item.DataItem.ToString();

        Label let = (Label)e.Item.FindControl("letter");

        let.Text = letter;

        _ds.Letter.AddLetterRow(letter);

        Repeater rptProds = (Repeater)e.Item.FindControl("rptProducts");

        rptProds.DataSource = Querys.Instance.getProducts(this._editionId, letter);
        rptProds.DataBind();


        this._cont += 1;
    }

    protected void rptProducts_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        ProductByEdition product = (ProductByEdition)e.Item.DataItem;

        _ds.Product.AddProductRow(product.Brand, product.Substances, product.Description == null ? "" : product.Description, product.PharmaForm, product.Division,
            product.Pages == null ? "" : product.Pages, (General.LetterRow)_ds.Letter.Rows[this._cont]);

        _ds.WriteXml(System.Configuration.ConfigurationManager.AppSettings["General"] + "General.xml");

    }
 
    private int _editionId;
    private General _ds = new General();
    private int _cont;
}