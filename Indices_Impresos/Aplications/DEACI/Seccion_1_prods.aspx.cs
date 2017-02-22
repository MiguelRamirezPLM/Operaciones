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

public partial class Seccion_1_prods : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this._sectionId = this.Request.QueryString["SectionId"] == null ? -1 :
            Convert.ToInt32(this.Request.QueryString["SectionId"]);
        if (this._sectionId != -1)
        {

            DEACITableAdapters.DSectionsTableAdapter secAdp = new DEACITableAdapters.DSectionsTableAdapter();
            DEACI.DSectionsDataTable secTable = secAdp.getProductsBySection(this._editionId, this._sectionId);

            this.lblSubSectionDescription.Text = secTable.Rows[0]["Section"].ToString();

            //DEACITableAdapters.ProductSectionsTableAdapter prodsSecAdp = new DEACITableAdapters.ProductSectionsTableAdapter();
            //DEACI.ProductSectionsDataTable prodsSecTable = prodsSecAdp.getProductSections(this._sectionId);

            this.rptSubSections.DataSource = secTable;
            this.rptSubSections.DataBind();
        }
    }

    private int _sectionId;
    private int _editionId = 4;
}
