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

public partial class IndProducts : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        this._editionId = this.Request.QueryString["Edition"] == null ? -1 : Convert.ToInt32(this.Request.QueryString["Edition"]);
        {
            this.rptLetter.DataSource = Catalogs.Instance.getLetterPyS(_editionId);
            this.rptLetter.DataBind();
            _cont = 0;
            _cont2 = 0;
            _cont3 = 0;
        }

              this.Response.Redirect("general.aspx"); 
    
    }

    protected void rptLetter_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        DataRowView rowView = (DataRowView)e.Item.DataItem;
        DataRow letRow = rowView.Row;

        _ds2.Letter.AddLetterRow(letRow.ItemArray[0].ToString());
        
        Repeater rptProds = (Repeater)e.Item.FindControl("rptProds");
        rptProds.DataSource = Catalogs.Instance.getProductsPyS(letRow.ItemArray[0].ToString(), _editionId);
        rptProds.DataBind();
        _cont += 1;
    }

    protected void rptProds_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        DataRowView rowView = (DataRowView)e.Item.DataItem;
        DataRow prodRow = rowView.Row;

    
        _ds2.Product.AddProductRow(prodRow.ItemArray[0].ToString(), (IndProds.LetterRow)_ds2.Letter.Rows[_cont]);

 
        Repeater rptComp = (Repeater)e.Item.FindControl("rptCompany");
        rptComp.DataSource = Catalogs.Instance.getBrandsByProdId(Convert.ToInt32(prodRow.ItemArray[1]), _editionId);
        rptComp.DataBind();

        Repeater rptSubProds = (Repeater)e.Item.FindControl("rptSubProds");
        rptSubProds.DataSource = Catalogs.Instance.getSubProducts(prodRow.ItemArray[1].ToString(), _editionId);
        rptSubProds.DataBind();

       // _ds2.WriteXml(System.Configuration.ConfigurationManager.AppSettings["IndProdCR"] + "indProdCR.xml");
        _cont2 += 1;
        
    }

    protected void rptCompa_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        DataRowView rowView = (DataRowView)e.Item.DataItem;
        DataRow compRow = rowView.Row;
         
        _ds2.CompanyProd.AddCompanyProdRow(compRow.ItemArray[0].ToString(), compRow.ItemArray[1].ToString(), (IndProds.ProductRow)_ds2.Product.Rows[_cont2]);


        _ds2.WriteXml(System.Configuration.ConfigurationManager.AppSettings["XML"] + "indProdyS62.xml");
        
    }
    
    protected void rptSubProds_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        DataRowView rowView = (DataRowView)e.Item.DataItem;
        DataRow rowSubProds = rowView.Row;

 
        _ds2.SubProduct.AddSubProductRow(rowSubProds.ItemArray[0].ToString(), (IndProds.ProductRow)_ds2.Product.Rows[_cont2]);

        Repeater rptCompSubP = (Repeater)e.Item.FindControl("rptCompanySP");
        rptCompSubP.DataSource = Catalogs.Instance.getSBrandsByProdId(Convert.ToInt32(rowSubProds.ItemArray[1]), _editionId);
        rptCompSubP.DataBind();
        _cont3 += 1;
    }

    protected void rptCompaSP_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        DataRowView rowView = (DataRowView)e.Item.DataItem;
        DataRow rowComp = rowView.Row;

          _ds2.CompanySubProd.AddCompanySubProdRow(rowComp.ItemArray[0].ToString(), rowComp.ItemArray[1].ToString(), (IndProds.SubProductRow)_ds2.SubProduct.Rows[_cont3]);

         _ds2.WriteXml(System.Configuration.ConfigurationManager.AppSettings["XML"] + "indProdyS62.xml");
    }

    private IndProds _ds2 = new IndProds();
    private int _editionId;
    private int _cont;
    private int _cont2;
    private int _cont3;
}
