using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class Fabricantes : System.Web.UI.Page
{
    #region Page Events

    protected void Page_Load(object sender, EventArgs e)
    {
        this.txtEdition.Focus();
    }

    #endregion

    #region Control Events
    
    protected void btnGenerate_Click(object sender, EventArgs e)
    {
        int edition = Convert.ToInt32(txtEdition.Text);

        Letters generateLetterFiles = new Letters(this.Server.MapPath("./HtmlTemplates/Fabricantes y Sucursales/letters.htm"),
                this.Server.MapPath("./Guia de Fabricantes"));

        generateLetterFiles.getHtmlFiles(edition);


        States generateStatesFiles = new States(this.Server.MapPath("./HtmlTemplates/Fabricantes y Sucursales/states.htm"),
            this.Server.MapPath("./Guia de Fabricantes"));

        generateStatesFiles.getHtmlFiles(edition);


        Companies generateCompaniesFiles = new Companies(this.Server.MapPath("./HtmlTemplates/Fabricantes y Sucursales/company.htm"),
            this.Server.MapPath("./Guia de Fabricantes/fab"));

        generateCompaniesFiles.getHtmlFiles(edition);

        this.lblMessage.Text = "Los archivos se generaron correctamente";

    }

    #endregion
}
