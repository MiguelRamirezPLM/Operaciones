using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class indAlimentos : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.rptStates.DataSource = IndiceGuia.Instance.getState();
        this.rptStates.DataBind();
    }
    protected void rptStates_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        DataRowView rowView = (DataRowView)e.Item.DataItem;
        DataRow staRow = rowView.Row;

        _ds2.State.AddStateRow(staRow.ItemArray[2].ToString());

        Repeater rptCities = (Repeater)e.Item.FindControl("rptCities");
        rptCities.DataSource = IndiceGuia.Instance.getCities(Convert.ToInt32(staRow.ItemArray[0]), value);
        rptCities.DataBind();
        _cont += 1;

    }
    protected void rptCities_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        DataRowView rowView = (DataRowView)e.Item.DataItem;
        DataRow CitRow = rowView.Row;
        _ds2.City.AddCityRow(CitRow.ItemArray[0].ToString(), (XMLAmbulancias.StateRow)_ds2.State.Rows[_cont]);
        
        Repeater rptCompany = (Repeater)e.Item.FindControl("rptCompany");
        rptCompany.DataSource = IndiceGuia.Instance.getCompany(Convert.ToInt32(CitRow.ItemArray[1]), value, CitRow.ItemArray[0].ToString());
        rptCompany.DataBind();
        _cont2 += 1;

    }

    protected void rptCompany_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        DataRowView rowView = (DataRowView)e.Item.DataItem;
        DataRow ComRow = rowView.Row;
        Repeater rptPhone = (Repeater)e.Item.FindControl("rptPhone");
        switch(ComRow.ItemArray[6].ToString())
        {
            case "3":
                _ds2.CompanyC.AddCompanyCRow("", "", "", "", "", "", "", "", "","", "", "", ComRow.ItemArray[2].ToString(), "#siglas", ComRow.ItemArray[3].ToString(), ComRow.ItemArray[4].ToString(),
                    "CP "+ComRow.ItemArray[5].ToString(), ComRow.ItemArray[7].ToString(), ComRow.ItemArray[8].ToString(), ComRow.ItemArray[9].ToString(),
                    ComRow.ItemArray[10].ToString(), ComRow.ItemArray[13].ToString(), ComRow.ItemArray[11].ToString() == "" ? "" : "Email: " + ComRow.ItemArray[11].ToString(), ComRow.ItemArray[12].ToString(),
                    (XMLAmbulancias.CityRow)_ds2.City.Rows[_cont2]);
                
               
                break;
            default:
                _ds2.CompanyC.AddCompanyCRow(ComRow.ItemArray[2].ToString(), "#siglas", ComRow.ItemArray[3].ToString(), ComRow.ItemArray[4].ToString(),
                    "CP "+ComRow.ItemArray[5].ToString(), ComRow.ItemArray[7].ToString(), ComRow.ItemArray[8].ToString(), ComRow.ItemArray[9].ToString(),
                   ComRow.ItemArray[10].ToString(), ComRow.ItemArray[13].ToString(), ComRow.ItemArray[11].ToString() == "" ? "" : "Email: " + ComRow.ItemArray[11].ToString(), ComRow.ItemArray[12].ToString(), "", "", "", "", "", "", "", "", "", "", "", "",
                    (XMLAmbulancias.CityRow)_ds2.City.Rows[_cont2]);
                
        

                break;

              
        }
        
        _ds2.WriteXml(System.Configuration.ConfigurationManager.AppSettings["XML"] + "EmpresasServiciosFinal.xml");
    }

    


   
    private int _cont;
    private int _cont2;
    private int _cont3;
    private int _cont4;
    private int value = 23;
    //private XmlAlimentos _ds2 = new XmlAlimentos();
    private XMLAmbulancias _ds2 = new XMLAmbulancias();
}
