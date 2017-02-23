using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class medinet : System.Web.UI.MasterPage
{
    
    #region Page Events
  
    protected void Page_Load(object sender, EventArgs e)
    {
       
        ss.ItemCreating += new SiteMapNodeItemEventHandler(SiteMapPathContentMap_ItemCreating);
            
        if (!IsPostBack) 
        {
            this.lblUserName.Text = Convert.ToString(this.Session["user"]) == null ? "" : Convert.ToString(this.Session["user"]);
            //Country Information
            if (this.Session["countryId"] == null)
                this.lblCountrylabel.Visible = false;
            else
                lblCountrylabel.Visible = true;
            this.lblCountryName.Text = this.Session["countryId"] == null ? "" : 
                MedinetBusinessLogicComponent.CatalogsBLC.Instance.getCountry(Convert.ToInt32(this.Session["countryId"])).CountryName;

            //Book Information
            if (this.Session["bookId"] == null)
                this.lblBookLabel.Visible = false;
            else
                lblBookLabel.Visible = true;
            this.lblBookShortName.Text = this.Session["bookId"] == null ? "" :
                MedinetBusinessLogicComponent.CatalogsBLC.Instance.getBook(Convert.ToInt32(this.Session["bookId"])).ShortName;
                
            //Edition Information
            if (this.Session["editionId"] == null)
                this.lblEditionLabel.Visible = false;
            else
                lblEditionLabel.Visible = true;
            this.lblNumberEdition.Text = this.Session["editionId"] == null ? "" :  
                MedinetBusinessLogicComponent.CatalogsBLC.Instance.getEdition(Convert.ToInt32(this.Session["editionId"])).NumberEdition.ToString();

            //DivisionInformation
            if (this.Session["divisionName"] == null)
                this.lblDivisionLabel.Visible = false;
            else
                this.lblDivisionLabel.Visible = true;

            this.lblDivisionName.Text = Convert.ToString(this.Session["divisionName"]) == null ? "" : Convert.ToString(this.Session["divisionName"]);

            //Version
            this.lblVersion.Text = System.Configuration.ConfigurationManager.AppSettings["Version"]; 
        }
                
      
    }

    #endregion

    #region Control Events

    protected void btnCloseSession_OnClick(object sender, EventArgs e)
    {
        this.Session.Abandon();
        this.Response.Redirect("login.aspx");
    }



    void SiteMapPathContentMap_ItemCreating(object sender, SiteMapNodeItemEventArgs e)
    {
        string InformationId = "";
        if (e.Item.ItemType == SiteMapNodeItemType.PathSeparator)
            return;
        //if (Session["roleDescription"].ToString() == "Administrador" && Session["UserType"]!=null)
        //     InformationId = Session["UserType"].ToString();
        //if (Session["itemMenu"]!=null)
        if (Session["UserType"]!=null)
        {
            if (Session["UserType"].ToString() == "Diseño" && Session["itemMenu"]!=null)
            InformationId = Session["itemMenu"].ToString();
            else
                InformationId = Session["UserType"].ToString();
            
        }
        else if(Session["itemMenu"]!=null)
            InformationId = Session["itemMenu"].ToString();
        else
        {
            switch (Session["roleDescription"].ToString())
            {
                case "Vendedor":
                    InformationId = "Ventas";
                    break;
                case "Médico":
                    InformationId = "Médicos";
                    break;
                case "Diagramador":
                    InformationId = "Producción";
                    break;
                
                    
            }
        }

            if (e.Item.SiteMapNode.Title.IndexOf("Información") != -1)
            {
                bool originalReadOnly = e.Item.SiteMapNode.ReadOnly;
                e.Item.SiteMapNode.ReadOnly = false;
                //string p = Session["UserType"].ToString(); ;
                e.Item.SiteMapNode.Title = "Información de " + InformationId;
                e.Item.SiteMapNode.ReadOnly = originalReadOnly;
            }
        

    }


    protected void SiteMapPath_ItemCreated(object sender, SiteMapNodeItemEventArgs e)
    {

        /*if (e.Item.ItemType == SiteMapNodeItemType.Root || (e.Item.ItemType == SiteMapNodeItemType.PathSeparator && e.Item.ItemIndex == 1))
        {
            e.Item.Visible = false;
        }*/
        if (e.Item.SiteMapNode != null)
        {

            if (Session["UserType"] != null)
            {
                string p = Session["UserType"].ToString();
                if (Session["UserType"].ToString() != "Diseño" && e.Item.SiteMapNode.Title == "Menú de Diseño")
                {
                    Session["TITLE"] = e.Item.ItemIndex;
                    e.Item.Visible = false;
                }

            }
            if (Session["roleDescription"].ToString() != "Administrador")
            {
                if (e.Item.SiteMapNode.Title == "Selección de Módulo" )
                {
                    Session["TITLE"] = e.Item.ItemIndex;
                    e.Item.Visible = false;
                }
                if(Session["roleDescription"].ToString() != "Diseñador" && e.Item.SiteMapNode.Title == "Menú de Diseño" )
                {
                    Session["TITLE"] = e.Item.ItemIndex;
                    e.Item.Visible = false;
                }
            }

        }
        if (Session["TITLE"] != null)
        {
            if (e.Item.ItemType == SiteMapNodeItemType.PathSeparator && e.Item.ItemIndex == Convert.ToInt32(Session["TITLE"]) + 1)
            {
                e.Item.Visible = false;
                Session["TITLE"] = null;
            }

        }

    }
  

    #endregion

}
