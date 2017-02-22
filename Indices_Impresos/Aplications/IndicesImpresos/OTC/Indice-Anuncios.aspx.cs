using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Indice_Anuncios : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this._editionId = this.Request.QueryString["Edition"] == null ? -1 : Convert.ToInt32(this.Request.QueryString["Edition"]);

        if (this._editionId > 0)
        {

            this.rptLabs.DataSource = Querys.Instance.getDivisionAds(this._editionId);
            this.rptLabs.DataBind();
        }
        this._cont = 0;

    }
    protected void rptLabs_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        Divisions div = (Divisions)e.Item.DataItem;

        _ds.Laboratory.AddLaboratoryRow(div.Description);

        Repeater rptAds = (Repeater)e.Item.FindControl("rptAds");
        rptAds.DataSource = Querys.Instance.getAdvertisements(this._editionId, div.DivisionId);
        rptAds.DataBind();

        this._cont += 1;
    }
    protected void rptAds_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        AdsByEdition ads = (AdsByEdition)e.Item.DataItem;

        _ds.Advertisement.AddAdvertisementRow(ads.AdName, ads.Page == null ? "" : ads.Page, (IndAdvertisements.LaboratoryRow)_ds.Laboratory.Rows[this._cont]);

        _ds.WriteXml("C:/OTC/XML/Anuncios.xml");


    }

    private int _cont;
    private int _editionId;
    private IndAdvertisements _ds = new IndAdvertisements();

}