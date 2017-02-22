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


public partial class IndAnunciantes : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this._editionId = this.Request.QueryString["Edition"] == null ? -1 : Convert.ToInt32(this.Request.QueryString["Edition"]);
        {
            this.rptLetters.DataSource = Catalogs.Instance.getLetterAnun(_editionId);
            this.rptLetters.DataBind();
            this._cont = 0;
        }

       this.Response.Redirect("general.aspx");
        
    }

    protected void rptLetters_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        DataRowView rowView = (DataRowView)e.Item.DataItem;
        DataRow rowLet = rowView.Row;

        _ds2.Letter.AddLetterRow(rowLet.ItemArray[0].ToString());

        Repeater rptComp = (Repeater)e.Item.FindControl("rptComp");
        rptComp.DataSource = Catalogs.Instance.getCompaniesAnun(rowLet.ItemArray[0].ToString(), _editionId);
        rptComp.DataBind();

        _cont += 1;
    }


    protected void rptComp_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        DataRowView rowView = (DataRowView)e.Item.DataItem;
        DataRow rowComp = rowView.Row;

        _ds2.Company.AddCompanyRow(rowComp.ItemArray[1].ToString(), rowComp.ItemArray[14].ToString(),
            rowComp.ItemArray[2].ToString(), rowComp.ItemArray[3].ToString(), rowComp.ItemArray[4].ToString(),
            rowComp.ItemArray[5].ToString(), rowComp.ItemArray[6].ToString(), rowComp.ItemArray[7].ToString(),
            rowComp.ItemArray[9].ToString(), rowComp.ItemArray[8].ToString(), rowComp.ItemArray[10].ToString(),
            rowComp.ItemArray[11].ToString(), rowComp.ItemArray[12].ToString(), rowComp.ItemArray[13].ToString(), 
            (IndAnun.LetterRow)_ds2.Letter.Rows[_cont]);

        _ds2.WriteXml(System.Configuration.ConfigurationManager.AppSettings["XML"] + "indAnun62.xml");

        
    }

 
    private int _cont;
    private int _editionId;
    private IndAnun _ds2 = new IndAnun();
}
