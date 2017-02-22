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

public partial class Sucursales : System.Web.UI.Page
{
    #region Page Events

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    #endregion

    #region Control Events

    protected void btnGenerate_Click(object sender, EventArgs e)
    {
        int edition = Convert.ToInt32(txtEdition.Text);
        string company = txtCompany.Text.Trim().ToUpper();
        int companyId = Convert.ToInt32(txtCompanyId.Text);
        
        States generateStates = new States(this.Server.MapPath("./HtmlTemplates/Fabricantes y Sucursales/statesBr.htm"), this.Server.MapPath("./Sucursales"));
        generateStates.getHtmlFiles(edition, companyId, company);

        Branches generateBranches = new Branches(this.Server.MapPath("./HtmlTemplates/Fabricantes y Sucursales/companyBr.htm"), this.Server.MapPath("./Sucursales/suc"));
        generateBranches.getHtmlFiles(edition, companyId, company);

        this.lblMessage.Text = "Los archivos se generaron correctamente";

    }

    #endregion
    
    
}
