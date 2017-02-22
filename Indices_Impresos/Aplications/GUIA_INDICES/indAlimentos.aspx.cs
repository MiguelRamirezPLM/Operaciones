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

        this._editionId = this.Request.QueryString["Edition"] == null ? -1 : Convert.ToInt32(this.Request.QueryString["Edition"]);
        this.value = this.Request.QueryString["Speciality"] == null ? -1 : Convert.ToInt32(this.Request.QueryString["Speciality"]);
        {
            this.rptStates.DataSource = Catalogs.Instance.getState(_editionId, value);
            this.rptStates.DataBind();
            _cont = 0;
        }

        this.Response.Redirect("general.aspx"); 

 
    }

    protected void rptStates_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        DataRowView rowView = (DataRowView)e.Item.DataItem;
        DataRow staRow = rowView.Row;

       

        _ds2.State.AddStateRow(staRow.ItemArray[1].ToString());

        Repeater rptCities = (Repeater)e.Item.FindControl("rptCities");
        rptCities.DataSource = Catalogs.Instance.getCities(Convert.ToInt32(staRow.ItemArray[0]), value, _editionId);
        rptCities.DataBind(); 

        _cont += 1;

    }
    protected void rptCities_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {

       DataRowView rowView = (DataRowView)e.Item.DataItem;
        DataRow CitRow = rowView.Row;

        _speciality = CitRow[3].ToString();

       _ds2.City.AddCityRow(CitRow.ItemArray[0].ToString() , (XmlAlimentos.StateRow)_ds2.State.Rows[_cont] );

         Repeater rptCompany = (Repeater)e.Item.FindControl("rptCompany");

         rptCompany.DataSource = Catalogs.Instance.getCompany(Convert.ToInt32(CitRow.ItemArray[1]), value, CitRow.ItemArray[0].ToString(),_editionId);
        rptCompany.DataBind();

        _cont2 += 1;

    }


    protected void rptCompany_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        
        DataRowView rowView = (DataRowView)e.Item.DataItem;
        DataRow ComRow = rowView.Row;
       
        _ds2.Companies.AddCompaniesRow((XmlAlimentos.CityRow)_ds2.City.Rows[_cont2] ); 
        Repeater rptPhone = (Repeater)e.Item.FindControl("rptPhone");

        if ((ComRow.ItemArray[6].ToString().Equals("1") && ComRow.ItemArray[14].ToString().Equals("3")) || (ComRow.ItemArray[6].ToString().Equals("3") && ComRow.ItemArray[14].ToString().Equals("3")))
        {
              _ds2.CompanyC.AddCompanyCRow(ComRow.ItemArray[2].ToString(), "#siglas", ComRow.ItemArray[3].ToString(), ComRow.ItemArray[4].ToString(),
                   "CP " + ComRow.ItemArray[5].ToString(), ComRow.ItemArray[7].ToString(), ComRow.ItemArray[8].ToString(), ComRow.ItemArray[9].ToString(),
                   ComRow.ItemArray[10].ToString(),
                   ComRow.ItemArray[13].ToString(),
                   ComRow.ItemArray[11].ToString() == "" ? "" : " " + ComRow.ItemArray[11].ToString(),
                   ComRow.ItemArray[12].ToString(),
                  //(XmlAlimentos.CompaniesRow)_ds2.Companies.Rows[_cont2]);
                   (XmlAlimentos.CompaniesRow)_ds2.Companies.Rows[_ds2.Companies.Rows.Count - 1]);
        }
        else if (ComRow.ItemArray[6].ToString().Equals("3") && ComRow.ItemArray[14].ToString().Equals(""))
        {
            _ds2.CompanyC.AddCompanyCRow(ComRow.ItemArray[2].ToString(), "#siglas", ComRow.ItemArray[3].ToString(), ComRow.ItemArray[4].ToString(),
                 "CP " + ComRow.ItemArray[5].ToString(), ComRow.ItemArray[7].ToString(), ComRow.ItemArray[8].ToString(), ComRow.ItemArray[9].ToString(),
                 ComRow.ItemArray[10].ToString(),
                 ComRow.ItemArray[13].ToString(),
                 ComRow.ItemArray[11].ToString() == "" ? "" : " " + ComRow.ItemArray[11].ToString(),
                 ComRow.ItemArray[12].ToString(),
                //(XmlAlimentos.CompaniesRow)_ds2.Companies.Rows[_cont2]);
                 (XmlAlimentos.CompaniesRow)_ds2.Companies.Rows[_ds2.Companies.Rows.Count - 1]);
        }
        else
        {
              _ds2.Company.AddCompanyRow(ComRow.ItemArray[2].ToString(), "#siglas", ComRow.ItemArray[3].ToString(), ComRow.ItemArray[4].ToString(),
                    "CP " + ComRow.ItemArray[5].ToString(), ComRow.ItemArray[7].ToString(), ComRow.ItemArray[8].ToString(), ComRow.ItemArray[9].ToString(),
                   ComRow.ItemArray[10].ToString(),
                   ComRow.ItemArray[13].ToString(),
                   ComRow.ItemArray[11].ToString() == "" ? "" : " " + ComRow.ItemArray[11].ToString(),
                   ComRow.ItemArray[12].ToString(),
                  //(XmlAlimentos.CompaniesRow)_ds2.Companies.Rows[_cont2]);
                (XmlAlimentos.CompaniesRow)_ds2.Companies.Rows[_ds2.Companies.Rows.Count - 1]);
        }
        //switch(ComRow.ItemArray[6].ToString())
        //{
        //    case "3":
        //        //_ds2.CompanyC.AddCompanyCRow("", "", "", "", "", "", "", "", "","", "", "", ComRow.ItemArray[2].ToString(), "#siglas", ComRow.ItemArray[3].ToString(), ComRow.ItemArray[4].ToString(),
        //        //    "CP "+ComRow.ItemArray[5].ToString(), ComRow.ItemArray[7].ToString(), ComRow.ItemArray[8].ToString(), ComRow.ItemArray[9].ToString(),
        //        //    ComRow.ItemArray[10].ToString(), ComRow.ItemArray[13].ToString(), ComRow.ItemArray[11].ToString() == "" ? "" : "Email: " + ComRow.ItemArray[11].ToString(), ComRow.ItemArray[12].ToString(),
        //        //    (XmlAlimentos.CityRow)_ds2.City.Rows[_cont2]);
                
                
        //        _ds2.CompanyC.AddCompanyCRow(ComRow.ItemArray[2].ToString(), "#siglas", ComRow.ItemArray[3].ToString(), ComRow.ItemArray[4].ToString(),
        //           "CP " + ComRow.ItemArray[5].ToString(), ComRow.ItemArray[7].ToString(), ComRow.ItemArray[8].ToString(), ComRow.ItemArray[9].ToString(),
        //           ComRow.ItemArray[10].ToString(),
        //           ComRow.ItemArray[13].ToString(),
        //           ComRow.ItemArray[11].ToString() == "" ? "" : " " + ComRow.ItemArray[11].ToString(),
        //           ComRow.ItemArray[12].ToString(),
        //           //(XmlAlimentos.CompaniesRow)_ds2.Companies.Rows[_cont2]);
        //           (XmlAlimentos.CompaniesRow)_ds2.Companies.Rows[_ds2.Companies.Rows.Count-1]);
                
               
        //        break;
        //    default:
        //        _ds2.Company.AddCompanyRow(ComRow.ItemArray[2].ToString(), "#siglas", ComRow.ItemArray[3].ToString(), ComRow.ItemArray[4].ToString(),
        //            "CP "+ComRow.ItemArray[5].ToString(), ComRow.ItemArray[7].ToString(), ComRow.ItemArray[8].ToString(), ComRow.ItemArray[9].ToString(),
        //           ComRow.ItemArray[10].ToString(),
        //           ComRow.ItemArray[13].ToString(),
        //           ComRow.ItemArray[11].ToString() == "" ? "" : " " + ComRow.ItemArray[11].ToString(), 
        //           ComRow.ItemArray[12].ToString(),
        //            //(XmlAlimentos.CompaniesRow)_ds2.Companies.Rows[_cont2]);
        //        (XmlAlimentos.CompaniesRow)_ds2.Companies.Rows[_ds2.Companies.Rows.Count-1]);
                

        //        break;

              
        //}
        
       //_ds2.WriteXml(System.Configuration.ConfigurationManager.AppSettings["XML"] + "EmpresasServiciosFinal.xml");
        _ds2.WriteXml(System.Configuration.ConfigurationManager.AppSettings["XMLEspecialidades"] + _speciality + ".xml");
    }




    private int _editionId;
    public string _speciality;
    private int _cont;
    private int _cont2;
    //private int _cont3;
    //private int _cont4;
   // private int value = 23;
    private int value;
   private XmlAlimentos _ds2 = new XmlAlimentos();
   // private XMLAmbulancias _ds2 = new XMLAmbulancias();
}
