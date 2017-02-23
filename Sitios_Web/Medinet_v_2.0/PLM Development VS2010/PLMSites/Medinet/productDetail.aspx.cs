using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.IO;

public partial class productDetail : System.Web.UI.Page
{

    #region Page Events

    protected void Page_Load(object sender, EventArgs e)
    {
        this._countryId = this.Session["countryId"] == null ? -1 : Convert.ToInt32(this.Session["countryId"]);
        this._editionId = Convert.ToInt32(this.Request.QueryString["ed"]);
        this._divisionId = Convert.ToInt32(this.Request.QueryString["di"]);
        this._categoryId = Convert.ToInt32(this.Request.QueryString["ca"]);
        this._productId = Convert.ToInt32(this.Request.QueryString["pr"]);
        this._pharmaFormId = Convert.ToInt32(this.Request.QueryString["pf"]);
        
        if (this._editionId != null
            && this._divisionId != null
            && this._categoryId != null
            && this._productId != null
            && this._pharmaFormId != null)
        {
            this.lblBrand.Text = MedinetBusinessLogicComponent.ProductsBLC.Instance.getProductById((int)this._productId).Brand;
            this.lblPharmaForm.Text = MedinetBusinessLogicComponent.CatalogsBLC.Instance.getPharmaForm((int)this._pharmaFormId).Description;

            this.getSubstances();
            this.getIndications();
            this.getProductContent();
        }
    }

    #endregion

    #region Private Methods

    private void getSubstances() 
    {
        List<MedinetBusinessEntries.ActiveSubstanceInfo> substanceList = MedinetBusinessLogicComponent.CatalogsBLC.Instance.getSubstancesByProduct((int)this._productId);

        if (substanceList.Count > 0)
        {
            this.lblNotSubstances.Visible = false;
            this.grdProductSubstances.DataSource = substanceList;
            this.grdProductSubstances.DataBind();
            this.grdProductSubstances.Visible = true;
        }
        else
        {
            this.lblNotSubstances.Visible = true;
            this.grdProductSubstances.Visible = false;
        }
    }

    private void getIndications() 
    {
        List<MedinetBusinessEntries.IndicationInfo> indicationList = MedinetBusinessLogicComponent.CatalogsBLC.Instance.getIndicationsByProduct((int)this._productId);

        if (indicationList.Count > 0)
        {
            this.lblNotIndications.Visible = false;
            this.grdProductIndications.DataSource = MedinetBusinessLogicComponent.CatalogsBLC.Instance.getIndicationsByProduct((int)this._productId);
            this.grdProductIndications.DataBind();
            this.grdProductIndications.Visible = true;
        }
        else
        {
            this.lblNotIndications.Visible = true;
            this.grdProductIndications.Visible = false;
        }
    }

    private void getProductContent()
    {
        string content = 
            MedinetBusinessLogicComponent.ParticipantProductsBLC.Instance.getParticipantProduct(
            (int)this._productId, (int)this._editionId, (int)this._pharmaFormId, (int)this._divisionId, (int)this._categoryId).HTMLContent;

        if (string.IsNullOrEmpty(content))
            this.lblProductContent.Text = "No hay contenido para este producto";
        else
        {
            content = this.clearContent(content);
            this.lblProductContent.Text = content;
        }
    }

    private string clearContent(string texto)
    {
        StringBuilder sb = new StringBuilder(texto);

        sb.Replace(">INFORMACI&Oacute;N REVISADA", ">");
        sb.Replace(">INFORMACI&Oacute;N NUEVA", ">");

        sb.Replace(">informaci&oacute;n revisada", ">");
        sb.Replace(">informaci&oacute;n nueva", ">");

        sb.Replace(">informaci&oacute;n REVISADA", ">");
        sb.Replace(">informaci&oacute;n NUEVA", ">");

        sb.Replace(">Informaci&oacute;n revisada", ">");
        sb.Replace(">Informaci&oacute;n nueva", ">");

        sb.Replace(">Informaci&oacute;n REVISADA", ">");
        sb.Replace(">Informaci&oacute;n NUEVA", ">");

        sb.Replace("&#174;", "<sup>&#174;</sup>");
        sb.Replace("<p class=\"pie1\">?", "<p class=\"pie1\">");
        sb.Replace("<table>", "<table border='1px'>");

        String s = texto;

        while (s != null)
        {
            a = s.IndexOf("<img");

            if (a >= 0)
            {
                a = s.IndexOf("<img");
                b = s.IndexOf("/>", (a + 1));
                img = s.Substring(a, b - (a - 2));

                s = s.Replace(img, "");
                sb = sb.Replace(img, "");

                img = "";
                a = -1;
            }
            else
                s = null;
        }

        string countryName = MedinetBusinessLogicComponent.CatalogsBLC.Instance.getCountry(this._countryId).CountryName;

        sb = sb.Replace("<link href=\"estilos.css\"", "<link href=\"" + countryName + "/estilos.css\"");

        return sb.ToString();
    }

    #endregion

    private int _countryId;
    private int? _editionId, _divisionId, _categoryId, _productId, _pharmaFormId;
    int a, b;
    string img;
    
}