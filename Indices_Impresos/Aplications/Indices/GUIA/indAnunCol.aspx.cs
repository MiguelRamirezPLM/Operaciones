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

public partial class GUIA_indAnunCol : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this._editionId = this.Request.QueryString["Edition"] == null ? -1 : Convert.ToInt32(this.Request.QueryString["Edition"]);

        if (this._editionId > 0)
        {
            this.rptSpeciality.DataSource = Catalogs.Instance.getSpecialitiesByEdition(this._editionId);
            this.rptSpeciality.DataBind();
            this._cont = 0;
            this._cont2 = 0;
        }
    }

    protected void rptSpeciality_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        DataRowView rowView = (DataRowView)e.Item.DataItem;
        DataRow rowEsp = rowView.Row;

        _ds.Speciality.AddSpecialityRow(rowEsp.ItemArray[1].ToString());

        Repeater rptComp = (Repeater)e.Item.FindControl("rptComp");
        rptComp.DataSource = Catalogs.Instance.getClientsBySpeciality(this._editionId, Convert.ToInt32(rowEsp.ItemArray[0].ToString()));
        rptComp.DataBind();

        _cont += 1;
    }

    protected void rptComp_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        DataRowView rowView = (DataRowView)e.Item.DataItem;
        DataRow rowComp = rowView.Row;

        if (!(string.IsNullOrEmpty(rowComp.ItemArray[1].ToString())))
        {

        _ds.Company.AddCompanyRow(rowComp.ItemArray[1].ToString(), rowComp.ItemArray[2].ToString(), rowComp.ItemArray[3].ToString(), (DirectAnunCol.SpecialityRow)_ds.Speciality.Rows[_cont]);

        Repeater rptPhone = (Repeater)e.Item.FindControl("rptPhone");
        rptPhone.DataSource = Catalogs.Instance.getPhonesByClient(this._editionId, Convert.ToInt32(rowComp.ItemArray[0].ToString()));
        rptPhone.DataBind();
        _cont2 += 1;
        }

        //_ds.WriteXml(System.Configuration.ConfigurationManager.AppSettings["XML"] + "indAnun.xml");

    }

    protected void rptPhone_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        DataRowView rowView = (DataRowView)e.Item.DataItem;
        DataRow rowPhone = rowView.Row;

        _ds.Phone.AddPhoneRow(rowPhone.ItemArray[0].ToString(), (DirectAnunCol.CompanyRow)_ds.Company.Rows[_cont2]);

        _ds.WriteXml(System.Configuration.ConfigurationManager.AppSettings["XML"] + "indAnunCol.xml");

    }

    private int _cont, _cont2;
    private int _editionId;
    private DirectAnunCol _ds = new DirectAnunCol();
}
