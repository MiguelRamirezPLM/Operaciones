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


public partial class DEF_CIE : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e) 
    {
        this._editionId = this.Request.QueryString["Edition"] == null ? -1 : Convert.ToInt32(this.Request.QueryString["Edition"]);

       // _editionId = 82;
        if (this._editionId > 0)
        {
            CIE.Instance.generateCIETmp(this._editionId);
            this.rptCIEN0.DataSource = CIE.Instance.getAlphabetCIE(this._editionId);
            this.rptCIEN0.DataBind();
            _cont = 0;
            _cont2 = 0;
            _cont3 = 0;
        }
        
    }

    protected void rptCIEN0_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        DataRowView rowView = (DataRowView)e.Item.DataItem;
        DataRow cieN0Row = rowView.Row;

       //_ds.CIEN0.AddCIEN0Row(cieN0Row.ItemArray[1].ToString().ToUpper() + " - " + cieN0Row.ItemArray[0].ToString().ToUpper());

        _ds.CIEN0.AddCIEN0Row(cieN0Row.ItemArray[3].ToString().ToUpper());

        Repeater rptCIEN1 = (Repeater)e.Item.FindControl("rptCIEN1");
        rptCIEN1.DataSource = CIEIndice.Instance.getCIEByParentId(Convert.ToInt32(cieN0Row.ItemArray[2].ToString()), this._editionId);
        rptCIEN1.DataBind();
        _cont += 1;

    }

    protected void rptCIEN1_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        DataRowView rowView = (DataRowView)e.Item.DataItem;
        DataRow cieN1Row = rowView.Row;

        //_ds.CIEN1.AddCIEN1Row(cieN1Row.ItemArray[1].ToString().ToUpper() + " - " + cieN1Row.ItemArray[0].ToString().ToUpper(),
        //    (CIE.CIEN0Row)_ds.CIEN0.Rows[_cont]);

        _ds.CIEN1.AddCIEN1Row(cieN1Row.ItemArray[3].ToString().ToUpper(),
            (CIE.CIEN0Row)_ds.CIEN0.Rows[_cont]);

        Repeater rptProdN1 = (Repeater)e.Item.FindControl("rptProdN1");
        rptProdN1.DataSource = CIEIndice.Instance.getInformation(Convert.ToInt32(cieN1Row.ItemArray[2].ToString()), this._editionId);
        rptProdN1.DataBind();

        Repeater rptCIEN2 = (Repeater)e.Item.FindControl("rptCIEN2");
        rptCIEN2.DataSource = CIEIndice.Instance.getCIEByParentId(Convert.ToInt32(cieN1Row.ItemArray[2].ToString()), this._editionId);
        rptCIEN2.DataBind();
        _cont2 += 1;

    }

    protected void rptCIEN2_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        DataRowView rowView = (DataRowView)e.Item.DataItem;
        DataRow cieN2Row = rowView.Row;

        //_ds.CIEN2.AddCIEN2Row(cieN2Row.ItemArray[1].ToString().ToUpper() + " - " + cieN2Row.ItemArray[0].ToString().ToUpper(),
        //  (CIE.CIEN1Row)_ds.CIEN1.Rows[_cont2]);


        _ds.CIEN2.AddCIEN2Row(cieN2Row.ItemArray[3].ToString().ToUpper(),
          (CIE.CIEN1Row)_ds.CIEN1.Rows[_cont2]);


        Repeater rptProdN2 = (Repeater)e.Item.FindControl("rptProdN2");
        rptProdN2.DataSource = CIEIndice.Instance.getInformation(Convert.ToInt32(cieN2Row.ItemArray[2].ToString()), this._editionId);
        rptProdN2.DataBind();
        _cont3 += 1;

    }

    protected void rptProdN1_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        DataRowView rowView = (DataRowView)e.Item.DataItem;
        DataRow prodN1Row = rowView.Row;

        _ds.ProductsN1.AddProductsN1Row(prodN1Row.ItemArray[0].ToString(),
            prodN1Row.ItemArray[1].ToString(), prodN1Row.ItemArray[2].ToString(),
            prodN1Row.ItemArray[3].ToString(), prodN1Row.ItemArray[4].ToString(), prodN1Row.ItemArray[5].ToString(), (CIE.CIEN1Row)_ds.CIEN1.Rows[_cont2]);

        _ds.WriteXml(System.Configuration.ConfigurationManager.AppSettings["CIE"] + "CIEP.xml");
    }

    protected void rptProdN2_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        DataRowView rowView = (DataRowView)e.Item.DataItem;
        DataRow prodN2Row = rowView.Row;

        _ds.ProductsN2.AddProductsN2Row(prodN2Row.ItemArray[0].ToString(),
            prodN2Row.ItemArray[1].ToString(), prodN2Row.ItemArray[2].ToString(),
            prodN2Row.ItemArray[3].ToString(), prodN2Row.ItemArray[4].ToString(), prodN2Row.ItemArray[5].ToString(), (CIE.CIEN2Row)_ds.CIEN2.Rows[_cont3]);

        _ds.WriteXml(System.Configuration.ConfigurationManager.AppSettings["CIE"] + "CIEP.xml");

        CIEIndice.Instance.cleanCIETmp();
        
    }

    

    private int _cont, _cont2, _cont3;
    private CIE _ds = new CIE ();
    private int _editionId;   
}