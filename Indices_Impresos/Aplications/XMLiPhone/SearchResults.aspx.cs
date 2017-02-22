using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class _Default : System.Web.UI.Page 
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.Request.QueryString["x"] != null && this.Request.QueryString["y"] != null && this.Request.QueryString["y"] != null)
        {

            this._countryId = Convert.ToByte(this.Request.QueryString["x"].ToString());
            this._editionId = Convert.ToInt32(this.Request.QueryString["y"].ToString());
            this._prefix = this.Request.QueryString["z"].ToString();

            PharmaSearchEngine.SearchResultsInfo bentity = this._service.getResults("", this._countryId, true, this._editionId, true, _prefix);

            this._ds.root.AddrootRow(_prefix);

            this.generateMed(bentity);
            this.generateSust(bentity);
            this.generateInd(bentity);
            this.generateLabs(bentity);

            this.Response.Clear();
            this.Response.Write(this._ds.GetXml());
            this.Response.End();
        }

    }

    public void generateMed(PharmaSearchEngine.SearchResultsInfo bentity)
    {
        this._ds.group.AddgroupRow("Medicamentos", "1", (XMLResults.rootRow)this._ds.root.Rows[0]);

        foreach (PharmaSearchEngine.ProductByEditionInfo prod in bentity.Products)
        {
            this._ds.article.AddarticleRow("med", prod.Brand + " " + prod.PharmaForm, 
                ConfigurationManager.AppSettings["path"].ToString() + "/products/" + prod.ProductId.ToString() + "_" + prod.PharmaFormId.ToString() + ".xml"
                , (XMLResults.groupRow)this._ds.group.Rows[0]);

        }
        
    }

    public void generateSust(PharmaSearchEngine.SearchResultsInfo bentity)
    {
        this._ds.group.AddgroupRow("Sustancias", "2", (XMLResults.rootRow)this._ds.root.Rows[0]);

        foreach (PharmaSearchEngine.ActiveSubstanceInfo subs in bentity.Substances)
        {
            this._ds.article.AddarticleRow("substance", subs.Description,
                ConfigurationManager.AppSettings["path"].ToString() + "/substances/" + subs.ActiveSubstanceId.ToString() + ".xml"
                , (XMLResults.groupRow)this._ds.group.Rows[1]);

        }
    }

    public void generateInd(PharmaSearchEngine.SearchResultsInfo bentity)
    {
        this._ds.group.AddgroupRow("Indicaciones", "3", (XMLResults.rootRow)this._ds.root.Rows[0]);

        foreach (PharmaSearchEngine.IndicationInfo indication in bentity.Indications)
        {
            this._ds.article.AddarticleRow("indication", indication.Description,
                ConfigurationManager.AppSettings["path"].ToString() + "/indications/" + indication.IndicationId.ToString() + ".xml"
                , (XMLResults.groupRow)this._ds.group.Rows[2]);

        }
    }

    public void generateLabs(PharmaSearchEngine.SearchResultsInfo bentity)
    {
        this._ds.group.AddgroupRow("Laboratorios", "4", (XMLResults.rootRow)this._ds.root.Rows[0]);

        foreach (PharmaSearchEngine.DivisionByEditionInfo lab in bentity.Labs)
        {
            this._ds.article.AddarticleRow("lab", lab.Description,
                ConfigurationManager.AppSettings["path"].ToString() + "/laboratories/" + lab.DivisionId.ToString() + ".xml"
                , (XMLResults.groupRow)this._ds.group.Rows[3]);

        }
       

    }

    private string _prefix;
    private byte _countryId;
    private int _editionId;
    private XMLResults _ds = new XMLResults();
    private PharmaSearchEngine.iOSPharmaSearchEngine _service = new PharmaSearchEngine.iOSPharmaSearchEngine();
}
