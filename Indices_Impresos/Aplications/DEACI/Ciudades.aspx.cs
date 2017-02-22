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

public partial class Cities : System.Web.UI.Page
{
    #region Events

    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.Request.QueryString["StateId"] != null && this.Request.QueryString["Index"] != null)
        {
            this._index = Convert.ToInt32(this.Request.QueryString["Index"].ToString());


            this._stateId = Convert.ToInt32(this.Request.QueryString["StateId"].ToString());
            
            switch(this._index)
            {
                case 1:
                    if (this._stateId != -1)
                    {
                        this.lblTitle.InnerText = "GUÍA NACIONAL DE FABRICANTES Y DISTRIBUIDORES";

                        DEACITableAdapters.StatesTableAdapter state = new DEACITableAdapters.StatesTableAdapter();
                        DEACI.StatesDataTable stateTab = state.getOne(this._stateId);


                        this.lblState.Text = "-" + stateTab.Rows[0]["Name"].ToString() + "-";

                        DEACITableAdapters.CitiesTableAdapter city = new DEACITableAdapters.CitiesTableAdapter();

                        DEACI.CitiesDataTable citiyComp = city.getCities(this._stateId, this._editionId);

                        this.rptCities.DataSource = citiyComp;
                        this.rptCities.DataBind();
                    }
                    break;
                case 2:
                    if (this._stateId != -1)
                    {
                        this.lblTitle.InnerText = "LABORATORIOS Y EMPRESAS DE DIAGNÓSTICOS POR IMAGEN";

                        DEACITableAdapters.StatesTableAdapter state = new DEACITableAdapters.StatesTableAdapter();
                        DEACI.StatesDataTable stateTab = state.getOne(this._stateId);


                        this.lblState.Text = "-" + stateTab.Rows[0]["Name"].ToString() + "-";

                        DEACITableAdapters.CitiesTableAdapter city = new DEACITableAdapters.CitiesTableAdapter();

                        DEACI.CitiesDataTable citiyComp = city.getLabCities(this._stateId, this._editionId);

                        this.rptCities.DataSource = citiyComp;
                        this.rptCities.DataBind();
                    }
                    break;
            }
        }
    }
    #endregion

    #region Control Events

    protected void rptCities_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        DataRowView rowview = (DataRowView)e.Item.DataItem;
        DataRow row = rowview.Row;

        DEACITableAdapters.CompaniesByCityTableAdapter city = new DEACITableAdapters.CompaniesByCityTableAdapter();
        DEACI.CompaniesByCityDataTable cityComp;

        switch(this._index)
        {
            case 1:
                cityComp = city.getInfo(Convert.ToInt32(row[0].ToString()), this._editionId);
                break;
            case 2:
                cityComp = city.getLabInfo(Convert.ToInt32(row[0].ToString()), this._editionId);
                break;
            default:
                cityComp = city.getInfo(Convert.ToInt32(row[0].ToString()), this._editionId);
                break;
        }
        
        Repeater rpt = (Repeater)e.Item.FindControl("rptComp");
        rpt.DataSource = cityComp;
        rpt.DataBind();

    }

    protected void rptComp_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        DataRowView rowview = (DataRowView)e.Item.DataItem;
        DataRow row = rowview.Row;

        DEACITableAdapters.PhonesTableAdapter phone = new DEACITableAdapters.PhonesTableAdapter();
        DEACI.PhonesDataTable phoneTab = phone.getPhones(Convert.ToInt32(row[0].ToString()));

        Repeater rpt = (Repeater)e.Item.FindControl("rptPhones");
        rpt.DataSource = phoneTab;
        rpt.DataBind();
    }

    #endregion

    #region Public Methods

    public string getCompany(string name, string html)
    {
        string cadena = "";

        if (!string.IsNullOrEmpty(name))
        {
            if (!string.IsNullOrEmpty(html))
            {
                cadena = "<b> <a class='linksLab' href='..\\terceras\\" + html + "' target='adentro' style='text-decoration:none'>" + name + "</a></b>";
            }
            else
            {
                cadena = "<b> " + name + "</b>";
            }
        }
        else
            cadena = "";

        return cadena;
    }

    public string getText(string data, int type)
    {
        string cadena = "";

        switch(type)
        {
            case 1:
                if (!string.IsNullOrEmpty(data))
                {
                    cadena = data + " <br />";
                }
                else
                    cadena = "";

                break;
            case 2:
                if (!string.IsNullOrEmpty(data))
                {
                    cadena = "C.P. " + data + " <br />";
                }
                else
                    cadena = "";

                break;
            case 3:
                if (!string.IsNullOrEmpty(data))
                {
                    cadena = "Email: <a href='mailto:" + data + "' >" + data + "</a> <br />";
                }
                else
                    cadena = "";

                break;
            case 4:
                if (!string.IsNullOrEmpty(data))
                {
                    cadena = "<a href='http://" + data + "' target='_blank' >" + data + "</a> <br />";
                }
                else
                    cadena = "";

                break;
            case 5:
                if (!string.IsNullOrEmpty(data))
                {
                    cadena = "<b>Contacto: </b>" + data + " <br />";
                }
                else
                    cadena = "";

                break;
        }
        return cadena;
    }

    public string getPhone(string type, string data)
    {
        string cadena = "";

        if (!string.IsNullOrEmpty(type) && !string.IsNullOrEmpty(data))
        {
            cadena = type + " " + data + " <br />";
        }
        else
            cadena = "";
            
        return cadena;
    }

    #endregion

    private int _stateId;
    private int _editionId = 4;
    private int _index;
}
