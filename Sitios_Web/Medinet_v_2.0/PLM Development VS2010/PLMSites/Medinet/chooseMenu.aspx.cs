using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class chooseMenu : System.Web.UI.Page
{
    string roleDescription;
    public static int subMenu = 0;

   
    protected void Page_Load(object sender, EventArgs e)
    {
        this.Session["countryId"] = null;
        this.Session["bookId"] = null;
        this.Session["editionId"] = null;
        this.Session["divisionName"] = null;
        roleDescription=Session["roleDescription"].ToString();
        List<PLMUserBusinessEntries.MenuByWebSection> li=(List<PLMUserBusinessEntries.MenuByWebSection>)Session["moduleList"];

       
            getDesignMenu();
           // getDesignMenu();
            if (roleDescription == "Diseñador")
                imgBtnBackLabs.Visible = false;

         
        
        
    }
    

    protected void getDesignMenu()
    {
        imgBtnBackLabs.Visible = true;
       List<PLMUserBusinessEntries.MenuByWebSection> li=(List<PLMUserBusinessEntries.MenuByWebSection>)Session["moduleList"];
        titulo.InnerText = "Diseño";
            for (int i = 0; i < li.Count; i++)
            {
                HtmlGenericControl lis = new HtmlGenericControl("li");
               
                    hhh.Controls.Add(lis);
                
                HtmlAnchor anchor = new HtmlAnchor();
                anchor.Attributes.Add("href", "#");
                anchor.Attributes.Add("runat", "server");
                anchor.Attributes.Add("Name", li[i].Url);
                //anchor.Attributes.Add("onserverclick", "chooseModule");
                anchor.ServerClick += new EventHandler(chooseModule);
                anchor.InnerText = li[i].MenuName;

                lis.Controls.Add(anchor);
            }
        
    }
   
    protected void imgBtnBackLabs_Click(object sender, ImageClickEventArgs e)
    {
        hhh.Controls.Clear();
        Response.Redirect("chooseModule.aspx");
    }
    protected void chooseModule(object sender, EventArgs e)
    {

        HtmlAnchor an = (HtmlAnchor)sender;
        Session["itemMenu"] = an.InnerText;
        Session["modulo"] = an.Name;
     
        Response.Redirect("laboratories.aspx");

    }
 
}