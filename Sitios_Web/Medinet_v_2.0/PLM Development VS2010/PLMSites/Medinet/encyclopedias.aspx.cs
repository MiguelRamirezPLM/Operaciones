using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class encyclopedias : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this._divisionId = this.Session["divisionId"] == null ? -1 : Convert.ToInt32(this.Session["divisionId"]);
        this._categoryId = this.Session["categoryId"] == null ? -1 : Convert.ToInt32(this.Session["categoryId"]);
        this._productId = this.Session["productId"] == null ? -1 : Convert.ToInt32(this.Session["productId"]);
        this._pharmaFormId = this.Session["pharmaFormId"] == null ? -1 : Convert.ToInt32(this.Session["pharmaFormId"]);
        this._userId = this.Session["userId"] == null ? -1 : Convert.ToInt32(this.Session["userId"]);
        this._brand = this.Session["brand"] == null ? "" : this.Session["brand"].ToString();
        this._pharmaForm = this.Session["pharmaForm"] == null ? "" : this.Session["pharmaForm"].ToString();
        this._hashkey = System.Configuration.ConfigurationManager.AppSettings["HashKey"];
        this._bookShortName = this.Session["bookShortName"] == null ? "" : this.Session["bookShortName"].ToString();
        if (this._bookShortName.Equals(System.Configuration.ConfigurationManager.AppSettings["Symptoms"]))
        {
            btnSymptomps.Visible = true;
        }
        else
        {
            btnSymptomps.Visible = false;
        }
        //btnContraindication.Visible = false;
        if (!IsPostBack)
        {
            this.lblBrand.Text = this._brand;
            this.lblPharmaForm.Text = this._pharmaForm;
            this.prepareGrid(Convert.ToInt16(this.ddlPageSize.SelectedValue));
            this.getEncyclopediasByProduct();
            this.getEncyclopediasWithoutProduct();
            

        }
    }
    #region Control Events

    protected void grdProductEncyclopedias_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            MedinetBusinessEntries.EncyclopediasInfo currentRow = (MedinetBusinessEntries.EncyclopediasInfo)e.Row.DataItem;
            ImageButton btnDeleteEncyclo = (ImageButton)e.Row.FindControl("btnDeleteEncyclo");

            //Button Add Substances to Product
            btnDeleteEncyclo.Attributes.Add("EncyclopediaId", currentRow.EncyclopediaId.ToString());
            btnDeleteEncyclo.Attributes.Add("onClick", "javascript:return confirmDelete('" + currentRow.EncyclopediaName + "')");
        }
    }

    protected void grdEncyclopedias_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            MedinetBusinessEntries.EncyclopediasInfo currentRow = (MedinetBusinessEntries.EncyclopediasInfo)e.Row.DataItem;
            ImageButton btnAddEncyclopedia = (ImageButton)e.Row.FindControl("btnAddEncyclopedia");

            //Button Add Substances to Product
            btnAddEncyclopedia.Attributes.Add("EncyclopediaId", currentRow.EncyclopediaId.ToString());
        }
    }

    protected void grdEncyclopedia_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        this.grdEncyclopedias.PageIndex = e.NewPageIndex;
        this.Session["CurrentPage"] = Convert.ToInt32(e.NewPageIndex);
        this.getEncyclopediasWithoutProduct();
    }

    protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.Session["CurrentPage"] = 0;
        this.prepareGrid(Convert.ToInt16(this.ddlPageSize.SelectedValue));
        this.getEncyclopediasWithoutProduct();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        this.prepareGrid(Convert.ToInt16(this.ddlPageSize.SelectedValue));
        this.getEncyclopediasWithoutProduct();
        this.txtEncyclopediaSearch.Focus();
    }

    protected void imgBtnBackProducts_Click(object sender, ImageClickEventArgs e)
    {
        this.Session["productId"] = null;
        this.Session["pharmaFormId"] = null;
        this.Response.Redirect("assignedProducts.aspx");
    }

    protected void btnSubstancesIndex_Click(object sender, EventArgs e)
    {
        this.Response.Redirect("activeSubstances.aspx");
    }
    protected void btnTherapeuticsIndex_Click(object sender, EventArgs e)
    {
        this.Response.Redirect("therapeutics.aspx");
    }
    protected void btnIcd_Click(object sender, EventArgs e)
    {
        this.Response.Redirect("ICD.aspx");
    }
    protected void btnContraindications_Click(object sender, EventArgs e)
    {
        if (checkSubstancesForInteractions())
        {
            this.Response.Redirect("contraindications.aspx");
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "messageScript", "jAlert('Para acceder a contraindicaciones es necesario agregar sustancias al producto','Contraindicaciones');", true);
        }
    }

    protected void btnSymptoms_Click(object sender, EventArgs e)
    {
        this.Response.Redirect("symptoms.aspx");

    }
    protected void btnInteractions_Click(object sender, EventArgs e)
    {
        if (checkSubstancesForInteractions())
        {
            this.Response.Redirect("interactionSubstances.aspx");
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "messageScript", "jAlert('Para acceder a interacciones es necesario agregar sustancias al producto','Interacciones');", true);
        }

    }

    protected void btnIndicationsIndex_Click(object sender, EventArgs e)
    {
        this.Response.Redirect("indications.aspx");
    }

    protected void btnOMSThera_Click(object sender, EventArgs e)
    {
        this.Response.Redirect("therapeuticoms.aspx");
    }

    protected void btnDeleteEncyclo_OnClick(object sender, EventArgs e)
    {
        int encyclopediaId;
        ImageButton btnDeleteEncyclo = (ImageButton)sender;

        encyclopediaId = Convert.ToInt32(btnDeleteEncyclo.Attributes["EncyclopediaId"]);

            MedinetBusinessLogicComponent.ProductPharmaFormEncyclopediasBLC.Instance.deleteProductPharmaFormEncyclopedia(this.getProductEncyclopedia(this._productId,encyclopediaId,this._pharmaFormId));

            this.getEncyclopediasByProduct();
            this.getEncyclopediasWithoutProduct();

    }

    protected void btnAddEncyclopedia_OnClick(object sender, EventArgs e)
    {
        int encyclopediaId;
        ImageButton btnAddEncyclo = (ImageButton)sender;

        encyclopediaId = Convert.ToInt32(btnAddEncyclo.Attributes["EncyclopediaId"]);

        MedinetBusinessLogicComponent.ProductPharmaFormEncyclopediasBLC.Instance.addProductPharmaFormEncyclopedia(this.getProductEncyclopedia(this._productId, encyclopediaId, this._pharmaFormId));
        this.txtEncyclopediaSearch.Text = "";
        this.getEncyclopediasByProduct();
        this.getEncyclopediasWithoutProduct();

    }

   

    protected bool displayParticipate(int participate)
    {
        if (participate == 0)
            return false;
        else
            return true;
    }

    #endregion

    #region Private Methods

    private bool checkSubstancesForInteractions()
    {
        List<MedinetBusinessEntries.ActiveSubstanceInfo> substances = MedinetBusinessLogicComponent.CatalogsBLC.Instance.getSubstancesByProduct(this._productId);
        if (substances.Count > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    private void getEncyclopediasByProduct()
    {
        List<MedinetBusinessEntries.EncyclopediasInfo> list = MedinetBusinessLogicComponent.CatalogsBLC.Instance.getEncyclopediasByProductByPharmaForm(this._productId, this._pharmaFormId);
        this.grdProductEncyclopedias.DataSource = list;
        this.grdProductEncyclopedias.DataBind();
    }

    private void getEncyclopediasWithoutProduct()
    {
        string encyclopediaToSearch = null;
        if (txtEncyclopediaSearch.Text.Length<3)
        {
           encyclopediaToSearch= this.txtEncyclopediaSearch.Text.Trim() + "%";
        }
        else
        {
            encyclopediaToSearch = "%"+this.txtEncyclopediaSearch.Text.Trim() + "%";
        }
        this.grdEncyclopedias.DataSource = MedinetBusinessLogicComponent.CatalogsBLC.Instance.getAllEncyclopediasWithOutProduct(this._productId, this._pharmaFormId, encyclopediaToSearch);
        this.grdEncyclopedias.DataBind();
    }

    private void prepareGrid(short pagesize)
    {
        if (pagesize > 0)
        {
            this.grdEncyclopedias.AllowPaging = true;
            this.grdEncyclopedias.PageSize = pagesize;
            this.grdEncyclopedias.PageIndex = this.Session["CurrentPage"] == null ? 0 : Convert.ToInt32(this.Session["CurrentPage"]);
        }
        else
            this.grdEncyclopedias.AllowPaging = false;
    }

    private MedinetBusinessEntries.ProductPharmaFormEncyclopediaInfo getProductEncyclopedia(int productId, int encyclopediaId, int pharmaFormId)
    {
        MedinetBusinessEntries.ProductPharmaFormEncyclopediaInfo productEncyclo = new MedinetBusinessEntries.ProductPharmaFormEncyclopediaInfo();
        productEncyclo.ProductId = productId;
        productEncyclo.EncyclopediaId = encyclopediaId;
        productEncyclo.PharmaFormId = pharmaFormId;
        return productEncyclo;
    }
   

    #endregion

    private int _editionId;
    private int _divisionId;
    private int _categoryId;
    private int _productId;
    private int _pharmaFormId;
    private int _userId;
    private string _brand;
    private string _pharmaForm;
    private string _hashkey;
    private string _bookShortName;
}