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

public partial class IndConsultaR : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.rptLetter.DataSource = Catalogs.Instance.getLetter();
        this.rptLetter.DataBind();
        _cont = 0;
    }


    protected void rptLetter_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        DataRowView rowView = (DataRowView)e.Item.DataItem;
        DataRow letRow = rowView.Row;

        _ds2.Letter.AddLetterRow(letRow.ItemArray[1].ToString());
        _ds2.ImgLetter.AddImgLetterRow((System.Configuration.ConfigurationManager.AppSettings["letras"]+letRow.ItemArray[2].ToString() + ".tif"), (IndCRProds.LetterRow)_ds2.Letter.Rows[_cont]);

        Repeater rptProds = (Repeater)e.Item.FindControl("rptProds");
        rptProds.DataSource = Catalogs.Instance.getProducts(letRow.ItemArray[1].ToString());
        rptProds.DataBind();
        _cont += 1;

    }

    protected void rptProds_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        DataRowView rowView = (DataRowView)e.Item.DataItem;
        DataRow prodRow = rowView.Row;

        //_ds2.Product.AddProductRow(prodRow.ItemArray[0].ToString(),,(IndCRProds.LetterRow)_ds2.Letter.Rows[_cont]);

        _ds2.WriteXml(System.Configuration.ConfigurationManager.AppSettings["XML"] + "indProdCR.xml");
        

    }

    private IndCRProds _ds2 = new IndCRProds();
    private int _cont;
}
