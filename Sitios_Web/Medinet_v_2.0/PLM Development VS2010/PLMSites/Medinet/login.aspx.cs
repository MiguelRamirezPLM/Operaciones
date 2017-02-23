using System;
using System.Data;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using PLMUsersDataAccessComponent;

public partial class login : System.Web.UI.Page
{
     
    protected void Page_Load(object sender, EventArgs e)
    {
        this.txtUserName.Focus();
        //Version
        this.lblVersion.Text = System.Configuration.ConfigurationManager.AppSettings["Version"]; 
    }

    protected void btnAceptar_Click(object sender, EventArgs e)
    {
        this.Session.Clear();
       // string[] modulos;
        List<PLMUserBusinessEntries.MenuByWebSection> li = new List<PLMUserBusinessEntries.MenuByWebSection>();
        PLMUserBusinessEntries.CountriesByUserInfo userCountries = PLMUsersBusinessLogicComponent.UsersBLC.Instance.getUserC(this.txtUserName.Text.Trim(), 
            this._cryptography.encrypt(this.txtUserPsw.Text.Trim()));

        if (userCountries != null)
        {
            if (userCountries.Active == true)
            {
                PLMUserBusinessEntries.ApplicationInfo applicationInf = 
                    PLMUsersBusinessLogicComponent.ApplicationUsersBLC.Instance.getApplication(System.Configuration.ConfigurationManager.AppSettings["hashkey"]);

                if(applicationInf != null)
                {
                    if (applicationInf.Active == true)
                    {
                        PLMUserBusinessEntries.ApplicationUserInfo applicationUser = PLMUsersBusinessLogicComponent.ApplicationUsersBLC.Instance.getUserAppl(applicationInf.ApplicationId, userCountries.UserId);
                        if (applicationUser != null)
                        {
                            this.Session["userId"] = userCountries.UserId;
                            this.Session["user"] = userCountries.NickName;
                            this._activitySession.ApplicationId = applicationInf.ApplicationId;
                            this._activitySession.UserId = userCountries.UserId;
                            this.Session["Countries"] = userCountries.Countries;
                            this.Session["userCountry"] = PLMUsersBusinessLogicComponent.ActivitySessionsBLC.Instance.getCountry(userCountries.CountryId);
                            PLMUsersBusinessLogicComponent.ActivitySessionsBLC.Instance.addActivity(this._activitySession);
                            PLMUserBusinessEntries.RoleInfo Role = PLMUsersBusinessLogicComponent.ApplicationUsersBLC.Instance.getRole(userCountries.UserId,
                                System.Configuration.ConfigurationManager.AppSettings["HashKey"]);
                            this._roleId = Role.RoleId;
                            this.Session["roleId"] = this._roleId;
                            this.Session["roleDescription"] = Role.Description;
                            li = PLMUsersBusinessLogicComponent.MenuesBLC.Instance.getMenues((int)Utilities.WebPages.MasterPage, (byte)Utilities.WebSections.MIDDLE, (int)Utilities.Roles.Diseñador);
                            
                                Session["moduleList"] = li;
                                if (Role.Description == "Administrador")
                                {
                                 
                                        
                                        
                                    this.Response.Redirect("chooseModule.aspx");
                                }
                                else if (Role.Description == "Diseñador")
                                {
                                    this.Response.Redirect("chooseMenu.aspx");
                                }
                                else
                                    this.Response.Redirect("laboratories.aspx");
                       
                        }
                        else
                            ScriptManager.RegisterStartupScript(this, typeof(Page), "messageScript", "jAlert('No tiene privilegios para accesar a esta aplicación.', 'Login');", true);
                    }
                    else
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "messageScript", "jAlert('La aplicación se encuentra inactiva por el momento.', 'Login');", true);
                }
                else
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "messageScript", "jAlert('La aplicación no esta disponible.', 'Login');", true);
            }
        }
        else
            ScriptManager.RegisterStartupScript(this, typeof(Page), "messageScript", "jAlert('Usuario / Contraseña incorrecta.', 'Login');", true);
    }

    private int _roleId;
    private PLMCryptographyComponent.CryptographyComponent _cryptography = new PLMCryptographyComponent.CryptographyComponent();
    private PLMUserBusinessEntries.ActivitySessionInfo _activitySession = new PLMUserBusinessEntries.ActivitySessionInfo();

}