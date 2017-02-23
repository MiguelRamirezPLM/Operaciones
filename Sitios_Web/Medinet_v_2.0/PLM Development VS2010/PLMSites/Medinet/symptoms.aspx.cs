using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class symptoms : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this._editionId = this.Session["editionId"] == null ? -1 : Convert.ToInt32(this.Session["editionId"]);
        this._divisionId = this.Session["divisionId"] == null ? -1 : Convert.ToInt32(this.Session["divisionId"]);
        this._categoryId = this.Session["categoryId"] == null ? -1 : Convert.ToInt32(this.Session["categoryId"]);
        this._productId = this.Session["productId"] == null ? -1 : Convert.ToInt32(this.Session["productId"]);
        this._pharmaFormId = this.Session["pharmaFormId"] == null ? -1 : Convert.ToInt32(this.Session["pharmaFormId"]);
        this._userId = this.Session["userId"] == null ? -1 : Convert.ToInt32(this.Session["userId"]);
        this._brand = this.Session["brand"] == null ? "" : this.Session["brand"].ToString();
        this._pharmaForm = this.Session["pharmaForm"] == null ? "" : this.Session["pharmaForm"].ToString();
        this._hashkey = System.Configuration.ConfigurationManager.AppSettings["HashKey"];
        //btnContraindication.Visible = false;
        if (!IsPostBack)
        {
            this.lblBrand.Text = this._brand;
            this.lblPharmaForm.Text = this._pharmaForm;
            this.prepareGrid(Convert.ToInt16(this.ddlPageSize.SelectedValue));
            this.getSymptomsCatalog();
            this.getSymptomsByProduct();
        }
    }

    #region Private Methods
    private void getSymptomsCatalog()
    {
        string symptomToSearch = "";
        if (txtSymptomName.Text.Trim().Length > 3)
        {
            symptomToSearch = "%" + txtSymptomName.Text + "%";
        }
        else
        {
            symptomToSearch = string.IsNullOrEmpty(this.txtSymptomName.Text.Trim()) ? "%" : this.txtSymptomName.Text.Trim() + "%";
        }
        List<MedinetBusinessEntries.SymptomInfo> listCatalog = MedinetBusinessLogicComponent.CatalogsBLC.Instance.getSymptoms(symptomToSearch);
        foreach (MedinetBusinessEntries.SymptomInfo item in MedinetBusinessLogicComponent.CatalogsBLC.Instance.getSymptomsByProduct(this._productId,this._pharmaFormId))
        {
            var symp = (from c in listCatalog
                        where c.SymptomId== item.SymptomId
                        select c);
            if (symp.Count() > 0)
            {
                listCatalog.Remove(symp.Single());
            }
        }
        this.grdSymptom.DataSource = listCatalog;
        this.grdSymptom.DataBind();
    }

    private void getSymptomsByProduct()
    {

        this.grdProductSymptoms.DataSource = MedinetBusinessLogicComponent.CatalogsBLC.Instance.getSymptomsByProduct(this._productId,this._pharmaFormId);
        this.grdProductSymptoms.DataBind();
    }

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

    private void prepareGrid(short pagesize)
    {
        if (pagesize > 0)
        {
            this.grdSymptom.AllowPaging = true;
            this.grdSymptom.PageSize = pagesize;
            this.grdSymptom.PageIndex = this.Session["CurrentPage"] == null ? 0 : Convert.ToInt32(this.Session["CurrentPage"]);
        }
        else
            this.grdSymptom.AllowPaging = false;
    }
    #endregion

    #region Events
    protected void grdSymptom_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        this.grdSymptom.PageIndex = e.NewPageIndex;
        this.Session["CurrentPage"] = Convert.ToInt32(e.NewPageIndex);
        this.getSymptomsCatalog();
    }

    protected void btnSymptom_Click(object sender, EventArgs e)
    {
        this.prepareGrid(Convert.ToInt16(this.ddlPageSize.SelectedValue));
        this.getSymptomsCatalog();
        this.txtSymptomName.Focus();
    }
    protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.Session["CurrentPage"] = 0;
        this.prepareGrid(Convert.ToInt16(this.ddlPageSize.SelectedValue));
        this.getSymptomsCatalog();
    }
    protected void grdSymptom_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            MedinetBusinessEntries.SymptomInfo currentRow = (MedinetBusinessEntries.SymptomInfo)e.Row.DataItem;
            ImageButton btnAddSSymptom = (ImageButton)e.Row.FindControl("btnAddSymptom");

            //Button Add Symptom to Product
            btnAddSSymptom.Attributes.Add("symptomId", currentRow.SymptomId.ToString());
        }
    }

    protected void btnAddSym_OnClick(object sender, EventArgs e)
    {
        int symptomId;
        ImageButton btnAddSymptom = (ImageButton)sender;

        symptomId = Convert.ToInt32(btnAddSymptom.Attributes["symptomId"]);
        MedinetBusinessEntries.ProductSymptomInfo ProdSymp = new MedinetBusinessEntries.ProductSymptomInfo();
        ProdSymp.PharmaFormId = this._pharmaFormId;
        ProdSymp.ProductId = this._productId;
        ProdSymp.SymptomId = symptomId;
        MedinetBusinessLogicComponent.ProductSymptomsBLC.Instance.addProductSymptom(ProdSymp);
        MedinetBusinessEntries.EditionSymptomInfo editSymp = new MedinetBusinessEntries.EditionSymptomInfo();
        editSymp.EditionId = this._editionId;
        editSymp.SymptomId = symptomId;
        MedinetBusinessLogicComponent.EditionSymptomBLC.Instance.addEditionSymptom(editSymp);
        this.getSymptomsByProduct();
        this.getSymptomsCatalog();
    }

    protected void btnDeleteSymptom_OnClick(object sender, EventArgs e)
    {
        int symptomId;
        ImageButton btnDeleteSymptom = (ImageButton)sender;

        symptomId = Convert.ToInt32(btnDeleteSymptom.Attributes["symptomId"]);
        MedinetBusinessEntries.ProductSymptomInfo ProdSymp = new MedinetBusinessEntries.ProductSymptomInfo();
        ProdSymp.PharmaFormId = this._pharmaFormId;
        ProdSymp.ProductId = this._productId;
        ProdSymp.SymptomId = symptomId;
        MedinetBusinessLogicComponent.ProductSymptomsBLC.Instance.deleteProductSymptom(ProdSymp);

        this.getSymptomsByProduct();
        this.getSymptomsCatalog();

    }
    protected void grdProductSymptoms_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            MedinetBusinessEntries.SymptomInfo currentRow = (MedinetBusinessEntries.SymptomInfo)e.Row.DataItem;
            ImageButton btnDeleteSymptom = (ImageButton)e.Row.FindControl("btnDeleteSymptom");

            //Button Add Substances to Product
            btnDeleteSymptom.Attributes.Add("symptomId", currentRow.SymptomId.ToString());
            btnDeleteSymptom.Attributes.Add("onClick", "javascript:return confirmDelete('" + currentRow.SymptomName + "')");

        }
    }
    
    protected void imgBtnBackProducts_Click(object sender, ImageClickEventArgs e)
    {
        this.Session["productId"] = null;
        this.Session["pharmaFormId"] = null;
        this.Response.Redirect("assignedProducts.aspx");
    }
    protected void btnTherapeuticsIndex_Click(object sender, EventArgs e)
    {
        this.Response.Redirect("therapeutics.aspx");
        
    }
    protected void btnEncyclo_Click(object sender, EventArgs e)
    {
        this.Response.Redirect("encyclopedias.aspx");
    }
    protected void btnIcd_Click(object sender, EventArgs e)
    {
        this.Response.Redirect("ICD.aspx");
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
    protected void btnSubstancesIndex_Click(object sender, EventArgs e)
    {
        this.Response.Redirect("activeSubstances.aspx");
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
  
    
}