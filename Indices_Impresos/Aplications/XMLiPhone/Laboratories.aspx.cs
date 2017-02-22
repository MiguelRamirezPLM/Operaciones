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

public partial class Laboratories : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.rptLab.DataSource = GetData.Instance.generateLab();
        this.rptLab.DataBind();

    }

    protected void rptLab_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        PharmaSearchEngine.DivisionByEditionInfo lab = (PharmaSearchEngine.DivisionByEditionInfo)e.Item.DataItem;

        this._ds = new Laboratory();

        _ds.root.AddrootRow(lab.Description);
        
        ID = lab.DivisionId.ToString();
        name = lab.Description.ToString();

        Repeater rptProd = (Repeater)e.Item.FindControl("rptProd");

        PharmaSearchEngine.DivisionInformationInfo divInfo = GetData.Instance.generateLabInformation(lab.DivisionId);
        
        

       if (divInfo != null)
        {
            address = divInfo.Address.ToString();
            suburb = divInfo.Suburb.ToString();
            zipcode = divInfo.ZipCode.ToString();
            city = divInfo.City.ToString();
            state = divInfo.State.ToString();

            if (address != null)
                dir += address;
            if(suburb != null)
                dir += ", " + suburb;
            if(zipcode != null)
                dir += ", " + zipcode;
            if(city != null)
                dir += ", " + city;
            if(state != null)
                dir += ", " + state;
            Image0 = divInfo.Image.ToString();
            
            if (Image0.IndexOf(",") >= 0)
            {
                Image0 = divInfo.Image.ToString();
                words = Image0.Split(',');

                Image1 = words[0].ToString();
                Image2 = words[1].ToString();

                this._ds.article.AddarticleRow(name, divInfo.Telephone, System.Configuration.ConfigurationManager.AppSettings["path"].ToString() + "images/" + Image1, "", "", dir, (Laboratory.rootRow)_ds.root.Rows[_cont]);                        

            }
            else
                this._ds.article.AddarticleRow(name, divInfo.Telephone, ConfigurationManager.AppSettings["path"].ToString() + "images/" + divInfo.Image, "", "", dir, (Laboratory.rootRow)_ds.root.Rows[_cont]);                        
            
            this._ds.WriteXml("c:\\XML\\laboratories\\" + ID + ".xml");

            suburb = "";
            zipcode = "";
            city = "";
            state = "";
            dir = "";



        }

        //rptProd.DataSource = GetData.Instance.generateLabInformation(lab.DivisionId);
        //rptProd.DataBind();
    }

    protected void rptProd_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {

        //PharmaSearchEngine.DivisionInformationInfo divInfo = (PharmaSearchEngine.DivisionInformationInfo)e.Item.DataItem;

        //_ds.article.AddarticleRow(name, divInfo.Telephone, System.Configuration.ConfigurationManager.AppSettings["path"].ToString() + "images/" + divInfo.Image, "", "", (Laboratory.rootRow)_ds.root.Rows[_cont]); 
        ////_ds.article.AddarticleRow("med", prod.Brand, System.Configuration.ConfigurationManager.AppSettings["path"].ToString() + "productos/ " + prod.ProductId.ToString() + "_" + prod.PharmaFormId.ToString() + ".xml", (Laboratory.rootRow)_ds.root.Rows[_cont]);
        //_ds.WriteXml("c:\\XMLiPhone\\Labs\\" + ID + ".xml");

    }
    private Laboratory _ds;
    private string ID = "";
    private int _cont = 0;
    private string name = "";
    private string Image0 = "";
    private string Image1 = "";
    private string Image2 = "";
    private string[] words;
    private string address = "";
    private string suburb = "";
    private string zipcode = "";
    private string city = "";
    private string state = "";
    private string dir = "";
}
