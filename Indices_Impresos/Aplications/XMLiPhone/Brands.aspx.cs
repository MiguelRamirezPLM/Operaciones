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

public partial class Brands : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.rptBrand.DataSource = GetData.Instance.generateProduct();
        this.rptBrand.DataBind();

    }

    protected void rptBrand_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {

        PharmaSearchEngine.ProductByEditionInfo prod = (PharmaSearchEngine.ProductByEditionInfo)e.Item.DataItem;
        
        ID = prod.ProductId.ToString() + "_" + prod.PharmaFormId.ToString();
        productId = prod.ProductId;

        this._ds = new Brand();        

        BrandName = prod.Brand.ToString() + " " + prod.PharmaForm;

        Subs = GetData.Instance.generateSubstanceByProduct(prod.ProductId);               

        Repeater rptProd = (Repeater)e.Item.FindControl("rptProd");
        rptProd.DataSource = GetData.Instance.generateAttributesByProduct(prod.ProductId, prod.DivisionId, prod.CategotyId, prod.PharmaFormId);
        rptProd.DataBind();

        

        createxml();

    }

    protected void rptProd_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {

        PharmaSearchEngine.AttributeByProductInfo attr = (PharmaSearchEngine.AttributeByProductInfo)e.Item.DataItem;

        if(attr.AttributeId == 3420 || attr.AttributeId ==3709 && dosis == "")
            dosis = System.Configuration.ConfigurationManager.AppSettings["path"].ToString() + "attributes/" + ID + "_" + attr.AttributeId.ToString() + ".xml";

        if (attr.AttributeId == 3767 || attr.AttributeId == 3406 || attr.AttributeId == 3585 && formu == "")
            formu = System.Configuration.ConfigurationManager.AppSettings["path"].ToString() + "attributes/" + ID + "_" + attr.AttributeId.ToString() + ".xml";
        
        if (attr.AttributeId == 3437 || attr.AttributeId == 3438 || attr.AttributeId == 3475 || attr.AttributeId == 3720 || attr.AttributeId == 3793 && indic == "")
            indic = System.Configuration.ConfigurationManager.AppSettings["path"].ToString() + "attributes/" + ID + "_" + attr.AttributeId.ToString() + ".xml";
        
        if (attr.AttributeId == 3407 || attr.AttributeId == 3447 || attr.AttributeId == 3479 && contra == "")
            contra = System.Configuration.ConfigurationManager.AppSettings["path"].ToString() + "attributes/" + ID + "_" + attr.AttributeId.ToString() + ".xml";

        if (attr.AttributeId == 3473 || attr.AttributeId == 3447 || attr.AttributeId == 3479 && presen == "")
            presen = System.Configuration.ConfigurationManager.AppSettings["path"].ToString() + "attributes/" + ID + "_" + attr.AttributeId.ToString() + ".xml";

        if (attr.AttributeId == 3460 || attr.AttributeId == 3464 || attr.AttributeId == 3517 && preca == "")
            preca = System.Configuration.ConfigurationManager.AppSettings["path"].ToString() + "attributes/" + ID + "_" + attr.AttributeId.ToString() + ".xml";

        lab = attr.DivisionId.ToString();
        

        


        
    }

    public void createxml()
    {
        //Subs = GetData.Instance.generateSubstanceByProduct(productId);
        _ds.root.AddrootRow(BrandName, Subs, "", "", "", dosis, formu, indic, contra, presen, System.Configuration.ConfigurationManager.AppSettings["path"].ToString() + "laboratories/" + lab + ".xml", preca);
        _ds.WriteXml("c:\\XML\\products\\" + ID + ".xml");

        dosis = "";
        formu = "";
        indic = "";
        contra = "";
        presen = "";
        preca = "";
        lab = "";
        Subs = "";
        
        
    }

    private Brand _ds;
    private String ID = "";
    private int _cont = 0;
    private string BrandName = "";
    private string Subs = "";
    private string dosis = "";
    private string formu = "";
    private string indic = "";
    private string contra = "";
    private string presen = "";
    private string preca = "";
    private string lab = "";
    private int productId;


}
