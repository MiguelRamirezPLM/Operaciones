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

public partial class IndInternacional : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.rptLetter.DataSource = Catalogs.Instance.getLetter();
        this.rptLetter.DataBind();
        _cont = 0;
        _cont2 = 0;
        _cont3 = 0;
        _cont4 = 0;
        _cont5 = 0;
    }

    protected void rptLetter_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        DataRowView rowView = (DataRowView)e.Item.DataItem;
        DataRow letRow = rowView.Row;

        _ds2.Letter.AddLetterRow(letRow.ItemArray[0].ToString());

        _ds2.ImgLetter.AddImgLetterRow("C:/angel.parra/Capitulares/" + letRow.ItemArray[1].ToString() + ".tif",
            (DirectInter.LetterRow)_ds2.Letter.Rows[_cont]);

        Repeater rptCompany = (Repeater)e.Item.FindControl("rptCompany");
        rptCompany.DataSource = Catalogs.Instance.getCompaniesInt(letRow.ItemArray[1].ToString());
        rptCompany.DataBind();
        _cont += 1;
    }

    protected void rptCompany_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        DataRowView rowView = (DataRowView)e.Item.DataItem;
        DataRow compRow = rowView.Row;

        _ds2.Company.AddCompanyRow(compRow.ItemArray[1].ToString(),compRow.ItemArray[2].ToString(),compRow.ItemArray[3].ToString(),
            compRow.ItemArray[4].ToString(),compRow.ItemArray[5].ToString(),compRow.ItemArray[6].ToString(),"tel:"+compRow.ItemArray[7].ToString(),
            compRow.ItemArray[8].ToString(),compRow.ItemArray[9].ToString(),compRow.ItemArray[10].ToString(),"Email:"+compRow.ItemArray[11].ToString(),
            "Web:"+compRow.ItemArray[12].ToString(),(DirectInter.LetterRow)_ds2.Letter.Rows[_cont]);

        this._clientId = Convert.ToInt32(compRow.ItemArray[0]);

        Repeater rptSCompany = (Repeater)e.Item.FindControl("rptSCompany");
        rptSCompany.DataSource = Catalogs.Instance.getCompaniesByParent(this._clientId);
        rptSCompany.DataBind();

        Repeater rptProductN0 = (Repeater)e.Item.FindControl("rptProductN0");
        rptProductN0.DataSource = Catalogs.Instance.getProductByClient(this._clientId);
        rptProductN0.DataBind();
                
        _cont2 += 1;
    }

    protected void rptSCompany_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        DataRowView rowView = (DataRowView)e.Item.DataItem;
        DataRow compRow = rowView.Row;

        //_ds2.SCompany.AddSCompanyRow(compRow.ItemArray[2].ToString(), compRow.ItemArray[3].ToString(),
        //    compRow.ItemArray[4].ToString(), compRow.ItemArray[5].ToString(), compRow.ItemArray[6].ToString(), compRow.ItemArray[7].ToString(),
        //    compRow.ItemArray[8].ToString(), compRow.ItemArray[9].ToString(), compRow.ItemArray[10].ToString(), compRow.ItemArray[11].ToString(),
        //    compRow.ItemArray[12].ToString(), (DirectInter.CompanyRow)_ds2.Company.Rows[_cont2]);
    
    }

    protected void rptProductN0_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        DataRowView rowView = (DataRowView)e.Item.DataItem;
        DataRow prodRow = rowView.Row;

        _ds2.ProdN0.AddProdN0Row(prodRow.ItemArray[1].ToString(), (DirectInter.CompanyRow)_ds2.Company.Rows[_cont2]);

        Repeater rptProductN1 = (Repeater)e.Item.FindControl("rptProductN1");
        rptProductN1.DataSource = Catalogs.Instance.getProductByIdByClientId(Convert.ToInt32(prodRow.ItemArray[0]), this._clientId);
        rptProductN1.DataBind();
        _cont3 += 1;
    }

    protected void rptProductN1_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        DataRowView rowView = (DataRowView)e.Item.DataItem;
        DataRow prodRow = rowView.Row;

        _ds2.ProdN1.AddProdN1Row(prodRow.ItemArray[1].ToString(), (DirectInter.ProdN0Row)_ds2.ProdN0.Rows[_cont3]);

        Repeater rptProductN2 = (Repeater)e.Item.FindControl("rptProductN2");
        rptProductN2.DataSource = Catalogs.Instance.getProductByIdByClientId(Convert.ToInt32(prodRow.ItemArray[0]), this._clientId);
        rptProductN2.DataBind();

        _ds2.WriteXml(System.Configuration.ConfigurationManager.AppSettings["XML"] + "indInterDI.xml");
        _cont4 += 1;
    }

    protected void rptProductN2_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        DataRowView rowView = (DataRowView)e.Item.DataItem;
        DataRow prodRow = rowView.Row;

        _ds2.ProdN2.AddProdN2Row(prodRow.ItemArray[1].ToString(), (DirectInter.ProdN1Row)_ds2.ProdN1.Rows[_cont4]);

        Repeater rptProductN3 = (Repeater)e.Item.FindControl("rptProductN3");
        rptProductN3.DataSource = Catalogs.Instance.getProductByIdByClientId(Convert.ToInt32(prodRow.ItemArray[0]), this._clientId);
        rptProductN3.DataBind();

        _ds2.WriteXml(System.Configuration.ConfigurationManager.AppSettings["XML"] + "indInterDI.xml");
        _cont5 += 1;

    }

    protected void rptProductN3_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        DataRowView rowView = (DataRowView)e.Item.DataItem;
        DataRow prodRow = rowView.Row;

        _ds2.ProdN3.AddProdN3Row(prodRow.ItemArray[1].ToString(), (DirectInter.ProdN2Row)_ds2.ProdN2.Rows[_cont5]);

        _ds2.WriteXml(System.Configuration.ConfigurationManager.AppSettings["XML"] + "indInterDI.xml");

    }

    private DirectInter _ds2 = new DirectInter();
    private int _cont, _cont2, _cont3, _cont4,_cont5;
    private int _clientId;
}
