using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class chooseModule : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.Session["countryId"] =null;
            this.Session["bookId"] = null;
                this.Session["editionId"] = null;
                    this.Session["divisionName"] = null;
                  
        getAdminMenu();
    }
    protected void getAdminMenu()
    {
        string roleDescription = Session["roleDescription"].ToString();
        List<PLMUserBusinessEntries.MenuByWebSection> li = (List<PLMUserBusinessEntries.MenuByWebSection>)Session["moduleList"];
        titulo.InnerText = "Módulos";

        HtmlGenericControl ventasLi = new HtmlGenericControl("li");
        hhh.Controls.Add(ventasLi);
        HtmlGenericControl medicoLi = new HtmlGenericControl("li");
        hhh.Controls.Add(medicoLi);
        HtmlGenericControl produccionLi = new HtmlGenericControl("li");
        hhh.Controls.Add(produccionLi);
        HtmlGenericControl diseñoLi = new HtmlGenericControl("li");
        hhh.Controls.Add(diseñoLi);

        HtmlAnchor ventas = new HtmlAnchor();

        ventas.Attributes.Add("href", "#");
        ventas.Attributes.Add("runat", "server");
        ventas.Attributes.Add("Name", "products.aspx");
        //anchor.Attributes.Add("onserverclick", "chooseModule");
        ventas.ServerClick += new EventHandler(chooseModules);
        ventas.InnerText = "Ventas";

        HtmlAnchor medico = new HtmlAnchor();
        medico.Attributes.Add("href", "#");
        medico.Attributes.Add("runat", "server");
        medico.Attributes.Add("Name", "assignedProducts.aspx");
        //anchor.Attributes.Add("onserverclick", "chooseModule");
        medico.ServerClick += new EventHandler(chooseModules);
        medico.InnerText = "Agroquímicos";

        HtmlAnchor produccion = new HtmlAnchor();
        produccion.Attributes.Add("href", "#");
        produccion.Attributes.Add("runat", "server");
        produccion.Attributes.Add("Name", "classifiedProducts.aspx");
        //anchor.Attributes.Add("onserverclick", "chooseModule");
        produccion.ServerClick += new EventHandler(chooseModules);
        produccion.InnerText = "Producción";

        HtmlAnchor diseño = new HtmlAnchor();
        diseño.Attributes.Add("href", "#");
        diseño.Attributes.Add("runat", "server");
        diseño.Attributes.Add("Name", "Diseño");
        //anchor.Attributes.Add("onserverclick", "chooseModule");
        diseño.ServerClick += new EventHandler(chooseModules);
        diseño.InnerText = "Diseño";


        ventasLi.Controls.Add(ventas);
        medicoLi.Controls.Add(medico);
        produccionLi.Controls.Add(produccion);
        diseñoLi.Controls.Add(diseño);





    }

    protected void chooseModules(object sender, EventArgs e)
    {

        HtmlAnchor an = (HtmlAnchor)sender;
       Session["UserType"]= an.InnerText;
        Session["modulo"] = an.Name;
        if (an.Name == "Diseño")
        {
            hhh.Controls.Clear();
            Response.Redirect("chooseMenu.aspx");
        }
        else
        {
            SiteMapPath aa = (SiteMapPath)Master.FindControl("SiteMap1");
            //aa.SiteMapProvider = "WebNormal";
            Response.Redirect("laboratories.aspx");
            
        }

    }
}