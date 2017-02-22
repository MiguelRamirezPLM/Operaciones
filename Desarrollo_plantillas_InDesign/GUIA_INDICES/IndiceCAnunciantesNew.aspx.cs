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

public partial class IndiceCAnunciantesNew : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this._editionId = this.Request.QueryString["Edition"] == null ? -1 : Convert.ToInt32(this.Request.QueryString["Edition"]);
        {
            this.rptLetters.DataSource = Catalogs.Instance.getLetterAnunNew(_editionId);
            this.rptLetters.DataBind();
            this._cont = 0;
        }

        //this.Response.Redirect("general.aspx");
    }

    protected void rptLetters_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        DataRowView rowView = (DataRowView)e.Item.DataItem;
        DataRow rowLet = rowView.Row;

        //_ds2.Letter.AddLetterRow(rowLet.ItemArray[0].ToString());

        Repeater rptClientN = (Repeater)e.Item.FindControl("rptClientN");
        rptClientN.DataSource = Catalogs.Instance.getCompaniesAnunN(rowLet.ItemArray[0].ToString(), _editionId);
        rptClientN.DataBind();

        _cont += 1;
    }

    protected void rptClientN_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        DataRowView rowView = (DataRowView)e.Item.DataItem;
        DataRow rowClientN = rowView.Row;

        //_ds2.Letter.AddLetterRow(rowLet.ItemArray[0].ToString());

        Repeater rptDirClientN = (Repeater)e.Item.FindControl("rptDirClientN");
        rptDirClientN.DataSource = Catalogs.Instance.getCompaniesAnunDir(Convert.ToInt32(rowClientN.ItemArray[0]), _editionId);
        rptDirClientN.DataBind();

        _cont1 += 1;
    }

    protected void rptDirClientN_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        DataRowView rowView = (DataRowView)e.Item.DataItem;
        DataRow rowClientN = rowView.Row;

        //_ds2.Letter.AddLetterRow(rowLet.ItemArray[0].ToString());

        //Repeater rptDirClientN = (Repeater)e.Item.FindControl("rptDirClientN");
        //rptDirClientN.DataSource = Catalogs.Instance.getCompaniesAnunDir(Convert.ToInt32(rowClientN.ItemArray[0]), _editionId);
        //rptDirClientN.DataBind();

        _cont2 += 1;
    }


    private int _cont;
    private int _cont1;
    private int _cont2;
    private int _editionId;

}