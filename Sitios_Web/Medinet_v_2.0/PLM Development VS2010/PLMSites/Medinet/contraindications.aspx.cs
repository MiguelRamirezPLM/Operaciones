using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class contraindications : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this._editionId = this.Session["editionId"] == null ? -1 : Convert.ToInt32(this.Session["editionId"]);
        this._divisionId = this.Session["divisionId"] == null ? -1 : Convert.ToInt32(this.Session["divisionId"]);
        this._categoryId = this.Session["categoryId"] == null ? -1 : Convert.ToInt32(this.Session["categoryId"]);
        this._productId = this.Session["productId"] == null ? -1 : Convert.ToInt32(this.Session["productId"]);
        this._pharmaFormId = this.Session["pharmaFormId"] == null ? -1 : Convert.ToInt32(this.Session["pharmaFormId"]);
        this._userId = this.Session["userId"] == null ? -1 : Convert.ToInt32(this.Session["userId"]);
        this._contraindication = this.Session["contraindicationId"] == null ? -1 : Convert.ToInt32(this.Session["contraindicationId"]);
        this._brand = this.Session["brand"] == null ? "" : this.Session["brand"].ToString();
        this._pharmaForm = this.Session["pharmaForm"] == null ? "" : this.Session["pharmaForm"].ToString();
        this._hashkey = System.Configuration.ConfigurationManager.AppSettings["HashKey"];
        this._bookShortName = this.Session["bookShortName"] == null ? "" : this.Session["bookShortName"].ToString();
        this._itemCatalog = this.Session["itemCatalog"] == null ? "" : this.Session["itemCatalog"].ToString();
        
        if (this._bookShortName.Equals(System.Configuration.ConfigurationManager.AppSettings["Symptoms"]))
        {
            btnSymptomps.Visible = true;
        }
        else
        {
            btnSymptomps.Visible = false;
        }
        
        if (!IsPostBack)
        {
            this.setStatusCMChanges();
            this.setHTMLContraindications();            
            this.lblBrand.Text = this._brand;
            this.lblPharmaForm.Text = this._pharmaForm;
            this.prepareGrid(Convert.ToInt16(this.ddlHypersensibilityPageSize.SelectedValue));
            this.prepareGridMedical(Convert.ToInt16(this.ddlPageSizeMed.SelectedValue));
            this.prepareGridPhys(Convert.ToInt16(this.ddlPageSizeFisiologic.SelectedValue));
            this.prepareGridPharma(Convert.ToInt16(this.ddlPageSizePharma.SelectedValue));
            this.prepareGridPharmaGroup(Convert.ToInt16(this.ddlPageSizeGroups.SelectedValue));
            this.prepareGridSubstances(Convert.ToInt16(this.ddlPageSizeSubs.SelectedValue));
            this.prepareGridOther(Convert.ToInt16(this.ddlPageSizeOther.SelectedValue));
            this.getHypersensibilitiesCatalog();
            this.getHypersensibilitiesByProduct();

            this.getMedicalCatalog();
            this.getMedicalByProduct();

            this.getPhysCatalog();
            this.getPhysByProduct();

            this.getPharmaCatalog();
            this.getPharmaByProduct();

            this.getPharmaGroupCatalog();
            this.getPharmaGroupByProduct();

            this.getSubstancesCatalog();
            this.getSubstancesByProduct();

            this.getOtherCatalog();
            this.getOtherByProduct();

            this.getCommentsByProduct();

            this.getProductSubstances();
        }
    }
    #region GeneralEvents

    protected void imgBtnBackProducts_Click(object sender, ImageClickEventArgs e)
    {
        this.Session["productId"] = null;
        this.Session["pharmaFormId"] = null;
        this.Response.Redirect("assignedProducts.aspx");
    }
    protected void btnEncyclo_Click(object sender, EventArgs e)
    {
        this.Response.Redirect("encyclopedias.aspx");
    }
    protected void btnTherapeuticsIndex_Click(object sender, EventArgs e)
    {
        this.Response.Redirect("therapeutics.aspx");
    }
    protected void btnInteractions_Click(object sender, EventArgs e)
    {
        List<MedinetBusinessEntries.ActiveSubstanceInfo> substances = MedinetBusinessLogicComponent.CatalogsBLC.Instance.getSubstancesByProduct(this._productId);
        if (checkSubstancesForInteractions())
        {
            this.Response.Redirect("interactionSubstances.aspx");
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "messageScript", "jAlert('Para acceder a interacciones es necesario agregar sustancias al producto','Interacciones');", true);
        }      
        
    }

    protected void btnOMSThera_Click(object sender, EventArgs e)
    {
        this.Response.Redirect("therapeuticoms.aspx");
    }

    protected void btnIndicationsIndex_Click(object sender, EventArgs e)
    {
        this.Response.Redirect("indications.aspx");
    }
    protected void btnSubstancesIndex_Click(object sender, EventArgs e)
    {
        this.Response.Redirect("activeSubstances.aspx");
    }
    protected void btnSymptoms_Click(object sender, EventArgs e)
    {
        this.Response.Redirect("symptoms.aspx");
    }
    protected void btnIcd_Click(object sender, EventArgs e)
    {
        this.Response.Redirect("ICD.aspx");
    }
    protected void btnNoCM_Click(object sender, EventArgs e)
    {
        
        this.setProductNoContraindications();
        this.getHypersensibilitiesByProduct();
        this.getMedicalByProduct();
        this.getPhysByProduct();
        this.getPharmaByProduct();
        this.getPharmaGroupByProduct();
        this.getSubstancesByProduct();
        this.getOtherByProduct();
        this.getCommentsByProduct();
        ScriptManager.RegisterStartupScript(this, typeof(Page), "messageScript", "jAlert('Producto clasificado sin contraindicaciones', 'Contraindicaciones');", true);
    }
    #endregion

    #region General methodsPrivate
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
    private void getProductSubstances()
    {
        
        this.grdSubstancesProduct.DataSource = MedinetBusinessLogicComponent.CatalogsBLC.Instance.getSubstancesByProduct(this._productId);
        this.grdSubstancesProduct.DataBind();

    }
    private void setHTMLContraindications()
    {
        this._htmlContent = MedinetBusinessLogicComponent.IppaProductContraindicationsBLC.Instance.getHtmlContraindicationByProduct(this._productId, this._pharmaFormId, this._divisionId, this._categoryId);
        if (this._htmlContent.Length < 1)
        {
            this._htmlContent = "<p class='normal'>No se cuenta aun con contenido de este producto</p>";
        }
        div.InnerHtml = this._htmlContent;

    }
    private void setProductNoContraindications()
    {
        List<MedinetBusinessEntries.ActiveSubstanceInfo> subs = MedinetBusinessLogicComponent.CatalogsBLC.Instance.getSubstancesByProduct(this._productId);
        MedinetBusinessEntries.IppaProductContraindicationsInfo contr;
        foreach (MedinetBusinessEntries.ActiveSubstanceInfo item in subs)
        {
            contr = new MedinetBusinessEntries.IppaProductContraindicationsInfo();
            contr.CategoryId = this._categoryId;
            contr.DivisionId = this._divisionId;
            contr.PharmaFormId = this._pharmaFormId;
            contr.ProductId = this._productId;
            contr.ActiveSubstanceId = item.ActiveSubstanceId;
            contr.StatusId = 4;
            MedinetBusinessLogicComponent.IppaProductContraindicationsBLC.Instance.addProductContraindications(contr,true);
            
        }
        
    }
    private void setStatusCMChanges()
    {
        MedinetBusinessLogicComponent.IppaProductContraindicationsBLC.Instance.checkStatusCMProduct(this._categoryId, this._pharmaFormId, this._productId, this._divisionId);
    }
    private void checkContraindications() {
        if (MedinetBusinessLogicComponent.IppaProductContraindicationsBLC.Instance.getProductContraindications(this._categoryId, this._pharmaFormId, this._productId, this._divisionId).Count > 0)
            this.btnNoCM.OnClientClick = "return confirmStatus()";
        else
            this.btnNoCM.OnClientClick = "";
    }

    
    #endregion

    #region Hypersendibility Events
    protected void btnHypersensibility_Click(object sender, EventArgs e)
    {
        this.prepareGrid(Convert.ToInt16(this.ddlHypersensibilityPageSize.SelectedValue));
        this.getHypersensibilitiesCatalog();
        this.txtHypersensibilityName.Focus();
        ScriptManager.RegisterStartupScript(this, typeof(Page), "messageScript", "$(panelHyper).css({'display':'block'});", true);
    }
    protected void ddlHypersensibilityPageSize_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.Session["CurrentPage"] = 0;
        this.prepareGrid(Convert.ToInt16(this.ddlHypersensibilityPageSize.SelectedValue));
        this.getHypersensibilitiesCatalog();
        ScriptManager.RegisterStartupScript(this, typeof(Page), "messageScript", "$(panelHyper).css({'display':'block'});", true);
    }
    protected void grdHypersensibilities_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            MedinetBusinessEntries.HypersensibilitiesInfo currentRow = (MedinetBusinessEntries.HypersensibilitiesInfo)e.Row.DataItem;
            ImageButton btnAddSHyper = (ImageButton)e.Row.FindControl("btnAddHypersensibility");
            btnAddSHyper.Attributes.Add("HypersensibilityId", currentRow.HypersensibilityId.ToString());
            btnAddSHyper.Attributes.Add("HypersensibilityName", currentRow.HypersensibilityName);
        }
    }
    protected void grdHypersensibilities_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        this.grdHypersensibilities.PageIndex = e.NewPageIndex;
        this.Session["CurrentPage"] = Convert.ToInt32(e.NewPageIndex);
        this.getHypersensibilitiesCatalog();
        ScriptManager.RegisterStartupScript(this, typeof(Page), "messageScript", "$(panelHyper).css({'display':'block'});", true);
    }
    protected void btnAddHyper_OnClick(object sender, EventArgs e)
    {

        ImageButton btnAddHyp = (ImageButton)sender;
        this.Session["contraindicationId"] = Convert.ToInt32(btnAddHyp.Attributes["HypersensibilityId"]);
        this._contraindication = Convert.ToInt32(btnAddHyp.Attributes["HypersensibilityId"]);
        this.Session["itemCatalog"] = btnAddHyp.Attributes["HypersensibilityName"];
        this._itemCatalog = btnAddHyp.Attributes["HypersensibilityName"];

        List<MedinetBusinessEntries.ActiveSubstanceInfo> data = MedinetBusinessLogicComponent.CatalogsBLC.Instance.getSubstancesByProduct(this._productId);

        foreach (MedinetBusinessEntries.ProductHypersensibilitiesContraindicationInfo item in MedinetBusinessLogicComponent.ProductHypersensibilityContraindicationsBLC.Instance.getHypersensibilityContraindications(this._productId, this._categoryId, this._divisionId, this._pharmaFormId, this._contraindication))
        {
            var sust = (from c in data
                        where c.ActiveSubstanceId == item.ActiveSubstanceId
                        select c);
            if (sust.Count() > 0)
            {
                data.Remove(sust.Single());
            }
        }
        if (data.Count > 1)
        {
            cleanWorkGroupHip();
            setWorkGroupHip();
            btnContinuarHip.Visible = true;
            lblAdvertHip.Text = "El actual producto contiene más de una sustancia sin Contraindicación, marque cuál(es) están contraindicadas con : ";
            lblItemHip.Text = this._itemCatalog;
            BtnListSubstancesHip.DataSource = data;
            BtnListSubstancesHip.DataValueField = "ActiveSubstanceId";
            BtnListSubstancesHip.DataTextField = "Description";
            BtnListSubstancesHip.DataBind();
        }
        else
        {
            if (data.Count <= 0)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "messageScript", "$(panelHyper).css({'display':'block'});", true);
                ScriptManager.RegisterStartupScript(this, typeof(Page), "messageScripts", "jAlert('Todas las sustancias del producto ya tienen Contraindicación con la sustancia: " + this._itemCatalog + "', 'Contraindicaciones');", true);
            }
            else
            {
                if (MedinetBusinessLogicComponent.IppaProductContraindicationsBLC.Instance.checkProductContraindications(this._categoryId, this._pharmaFormId, this._productId, this._divisionId, data[0].ActiveSubstanceId))
                {
                    MedinetBusinessEntries.ProductHypersensibilitiesContraindicationInfo ProdHyp = new MedinetBusinessEntries.ProductHypersensibilitiesContraindicationInfo();
                    ProdHyp.ActiveSubstanceId = data[0].ActiveSubstanceId;
                    ProdHyp.CategoryId = this._categoryId;
                    ProdHyp.DivisionId = this._divisionId;
                    ProdHyp.PharmaFormId = this._pharmaFormId;
                    ProdHyp.ProductId = this._productId;
                    ProdHyp.HypersensibilityId = this._contraindication;
                    if (MedinetBusinessLogicComponent.ProductHypersensibilityContraindicationsBLC.Instance.addProductHypersensibility(ProdHyp) == 0)
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "messageScript", "jAlert('La sustancia que ha seleccionado ya esta asociada a las contraindicaciones del producto', 'Contraindicaciones');", true);
                    }
                    this.getHypersensibilitiesByProduct();
                    this.getHypersensibilitiesCatalog();
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "messageScript", "$(panelHyper).css({'display':'block'});", true);
                }
                cleanWorkGroupHip();
            }
        }
        ScriptManager.RegisterStartupScript(this, typeof(Page), "messageScript", "$(panelHyper).css({'display':'block'});", true);

    }
    protected void btnDeleteHyper_OnClick(object sender, EventArgs e)
    {
        int hypersensibilityId,activesubstanceId;
        ImageButton btnDelHyper = (ImageButton)sender;

        hypersensibilityId = Convert.ToInt32(btnDelHyper.Attributes["HypersensibilityId"]);
        activesubstanceId = Convert.ToInt32(btnDelHyper.Attributes["ActiveSubstanceId"]);
        MedinetBusinessEntries.ProductHypersensibilitiesContraindicationInfo ProdHyp = new MedinetBusinessEntries.ProductHypersensibilitiesContraindicationInfo();
        ProdHyp.HypersensibilityId = hypersensibilityId;
        ProdHyp.CategoryId = this._categoryId;
        ProdHyp.DivisionId = this._divisionId;
        ProdHyp.PharmaFormId = this._pharmaFormId;
        ProdHyp.ProductId = this._productId;
        ProdHyp.ActiveSubstanceId = activesubstanceId;
        MedinetBusinessLogicComponent.ProductHypersensibilityContraindicationsBLC.Instance.deleteProductHypersensibility(ProdHyp);
        this.getHypersensibilitiesByProduct();
        this.getHypersensibilitiesCatalog();
        ScriptManager.RegisterStartupScript(this, typeof(Page), "messageScript", "$(panelHyper).css({'display':'block'});", true);

    }
    protected void grdProductHypersensibilities_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            MedinetBusinessEntries.ProductHypersensibilitiesContraindicationInfo currentRow = (MedinetBusinessEntries.ProductHypersensibilitiesContraindicationInfo)e.Row.DataItem;
            ImageButton btnDelSHyper = (ImageButton)e.Row.FindControl("btnDeleteHypersensibility");
            btnDelSHyper.Attributes.Add("HypersensibilityId", currentRow.HypersensibilityId.ToString());
            btnDelSHyper.Attributes.Add("ActiveSubstanceId", currentRow.ActiveSubstanceId.ToString());
            btnDelSHyper.Attributes.Add("onClick", "javascript:return confirmDelete('" + currentRow.HypersensibilityName + "')");
        }
    }
    protected void btnContinuarHip_Click(object sender, EventArgs e)
    {
        this.btnContinuarHip.Visible = false;
        this.BtnListSubstancesHip.Visible = false;
        this.lblAdvertHip.Visible = false;
        this.lblItemHip.Visible = false;
        this.btnCancelHip.Visible = false;
        bool selects = false;
        for (int i = 0; i < BtnListSubstancesHip.Items.Count; i++)
        {
            if (BtnListSubstancesHip.Items[i].Selected)
            {
                selects = true;
                int actsubsId = int.Parse(BtnListSubstancesHip.Items[i].Value);
                if (MedinetBusinessLogicComponent.IppaProductContraindicationsBLC.Instance.checkProductContraindications(this._categoryId, this._pharmaFormId, this._productId, this._divisionId, actsubsId))
                {
                    MedinetBusinessEntries.ProductHypersensibilitiesContraindicationInfo phcInfo = new MedinetBusinessEntries.ProductHypersensibilitiesContraindicationInfo();
                    phcInfo.ActiveSubstanceId = actsubsId;
                    phcInfo.CategoryId = this._categoryId;
                    phcInfo.DivisionId = this._divisionId;
                    phcInfo.PharmaFormId = this._pharmaFormId;
                    phcInfo.ProductId = this._productId;
                    phcInfo.HypersensibilityId = this._contraindication;
                    if (MedinetBusinessLogicComponent.ProductHypersensibilityContraindicationsBLC.Instance.addProductHypersensibility(phcInfo) == 0)
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "messageScripta", "jAlert('Ya existe una asociacion de " + BtnListSubstancesHip.Items[i].Text + " a las contraindicaciones del producto', 'Contraindicaciones');", true);
                    }
                }
            }
        }
        if (selects)
        {
        cleanWorkGroupHip();
        this.getHypersensibilitiesByProduct();
        this.getHypersensibilitiesCatalog();
        }
        else
        {
            this.btnContinuarHip.Visible = true;
            setWorkGroupHip();
            ScriptManager.RegisterStartupScript(this, typeof(Page), "messageScriptSelect", "jAlert('Debe seleccionar al menos una sustancia del producto para clasificar', 'Contraindicaciones');", true);
        }
        ScriptManager.RegisterStartupScript(this, typeof(Page), "messageScript", "$(panelHyper).css({'display':'block'});", true);
    }
    protected void btnCancelHip_Click(object sender, EventArgs e)
    {
        cleanWorkGroupHip();
        ScriptManager.RegisterStartupScript(this, typeof(Page), "messageScript", "$(panelHyper).css({'display':'block'});", true);
    }
    protected void btnDeleteAllHyp_Click(object sender, EventArgs e)
    {
        if (grdProductHypersensibilities.Rows.Count > 0)
        {
            MedinetBusinessEntries.ProductHypersensibilitiesContraindicationInfo ProdCon = new MedinetBusinessEntries.ProductHypersensibilitiesContraindicationInfo();
            ProdCon.CategoryId = this._categoryId;
            ProdCon.DivisionId = this._divisionId;
            ProdCon.PharmaFormId = this._pharmaFormId;
            ProdCon.ProductId = this._productId;
            MedinetBusinessLogicComponent.ProductHypersensibilityContraindicationsBLC.Instance.deleteProductHypersensibilityAll(ProdCon);
            this.getHypersensibilitiesCatalog();
            this.getHypersensibilitiesByProduct();
            ScriptManager.RegisterStartupScript(this, typeof(Page), "messageScript", "$(panelHyper).css({'display':'block'});", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "messageScriptAlert", "jAlert('El producto no tiene contraindicaciones por hipersensibilidad asociadas');", true);
            ScriptManager.RegisterStartupScript(this, typeof(Page), "messageScript", "$(panelHyper).css({'display':'block'});", true);
        }


    }

    #endregion

    #region Hypersensibility Private Methods
    private void getHypersensibilitiesCatalog()
    {
        string hypersensibilityToSearch = "";
        if (txtHypersensibilityName.Text.Trim().Length > 3)
        {
            hypersensibilityToSearch = "%" + txtHypersensibilityName.Text + "%";
        }
        else
        {
            hypersensibilityToSearch = string.IsNullOrEmpty(this.txtHypersensibilityName.Text.Trim()) ? "%" : this.txtHypersensibilityName.Text.Trim() + "%";
        }
        List<MedinetBusinessEntries.HypersensibilitiesInfo> listCatalog = MedinetBusinessLogicComponent.CatalogsBLC.Instance.getHypersensibilitiesWithOutAttach(hypersensibilityToSearch, this._productId, this._pharmaFormId, this._divisionId, this._categoryId);
   
        this.grdHypersensibilities.DataSource = listCatalog;
        this.grdHypersensibilities.DataBind();
    }

    private void getHypersensibilitiesByProduct()
    {
        this.grdProductHypersensibilities.DataSource = MedinetBusinessLogicComponent.CatalogsBLC.Instance.getProductHypersensibilities(this._productId, this._pharmaFormId, this._divisionId, this._categoryId);
        this.grdProductHypersensibilities.DataBind();
        checkContraindications();
        if (grdProductHypersensibilities.Rows.Count > 0)
            btnDeleteAllHyp.Visible = true;
        else
            btnDeleteAllHyp.Visible = false;
    }

    private void prepareGrid(short pagesize)
    {
        if (pagesize > 0)
        {
            this.grdHypersensibilities.AllowPaging = true;
            this.grdHypersensibilities.PageSize = pagesize;
            this.grdHypersensibilities.PageIndex = this.Session["CurrentPage"] == null ? 0 : Convert.ToInt32(this.Session["CurrentPage"]);
        }
        else
            this.grdHypersensibilities.AllowPaging = false;
    }

    private void cleanWorkGroupHip()
    {
        BtnListSubstancesHip.DataSource = null;
        BtnListSubstancesHip.Visible = false;
        lblAdvertHip.Visible = false;
        lblItemHip.Visible = false;
        btnCancelHip.Visible = false;
        btnContinuarHip.Visible = false;
    }

    private void setWorkGroupHip()
    {
        BtnListSubstancesHip.Visible = true;
        lblAdvertHip.Visible = true;
        lblItemHip.Visible = true;
        btnCancelHip.Visible = true;
    }

    #endregion

    #region Medical Events
    protected void btnMedical_Click(object sender, EventArgs e)
    {
        this.prepareGridMedical(Convert.ToInt16(this.ddlPageSizeMed.SelectedValue));
        this.getMedicalCatalog();
        this.txtContraMed.Focus();
        ScriptManager.RegisterStartupScript(this, typeof(Page), "messageScript", "$(panelMed).css({'display':'block'});", true);
    }
    protected void ddlMedicalPageSize_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.Session["CurrentPage"] = 0;
        this.prepareGridMedical(Convert.ToInt16(this.ddlPageSizeMed.SelectedValue));
        this.getMedicalCatalog();
        ScriptManager.RegisterStartupScript(this, typeof(Page), "messageScript", "$(panelMed).css({'display':'block'});", true);
    }
    protected void grdMedical_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            MedinetBusinessEntries.MedicalContraindicationInfo currentRow = (MedinetBusinessEntries.MedicalContraindicationInfo)e.Row.DataItem;
            ImageButton btnAddMedical = (ImageButton)e.Row.FindControl("btnAddMedical");
            btnAddMedical.Attributes.Add("MedicalContraindicationId", currentRow.MedicalContraindicationId.ToString());
            btnAddMedical.Attributes.Add("MedicalContraindicationName", currentRow.MedicalContraindicationName.ToString());
        }
    }
    protected void grdMedical_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        this.grdMedical.PageIndex = e.NewPageIndex;
        this.Session["CurrentPage"] = Convert.ToInt32(e.NewPageIndex);
        this.getMedicalCatalog();
        ScriptManager.RegisterStartupScript(this, typeof(Page), "messageScript", "$(panelMed).css({'display':'block'});", true);
    }
    protected void btnAddMedical_OnClick(object sender, EventArgs e)
    {
        
        
        ImageButton btnAddMed = (ImageButton)sender;
        this.Session["contraindicationId"] = Convert.ToInt32(btnAddMed.Attributes["MedicalContraindicationId"]);
        this._contraindication = Convert.ToInt32(btnAddMed.Attributes["MedicalContraindicationId"]);
        this.Session["itemCatalog"] = btnAddMed.Attributes["MedicalContraindicationName"];
        this._itemCatalog = btnAddMed.Attributes["MedicalContraindicationName"];

        List<MedinetBusinessEntries.ActiveSubstanceInfo> data = MedinetBusinessLogicComponent.CatalogsBLC.Instance.getSubstancesByProduct(this._productId);

        foreach (MedinetBusinessEntries.ProductMedicalContraindicationInfo item in MedinetBusinessLogicComponent.ProductMedicalContraindicationBLC.Instance.getMedicalContraindications(this._productId, this._categoryId, this._divisionId, this._pharmaFormId, this._contraindication))
        {
            var sust = (from c in data
                        where c.ActiveSubstanceId == item.ActiveSubstanceId
                        select c);
            if (sust.Count() > 0)
            {
                data.Remove(sust.Single());
            }
        }
         if (data.Count > 1)
        {
            cleanWorkGroupMed();
            setWorkGroupMed();
            btnContinuarMed.Visible = true;
            lblAdvertMed.Text = "El actual producto contiene más de una sustancia sin Contraindicación, marque cuál(es) están contraindicadas con : ";
            lblItemMed.Text = this._itemCatalog;
            BtnListSubstancesMed.DataSource = data;
            BtnListSubstancesMed.DataValueField = "ActiveSubstanceId";
            BtnListSubstancesMed.DataTextField = "Description";
            BtnListSubstancesMed.DataBind();
        }
         else
        {
            if (data.Count <= 0)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "messageScript", "$(panelMed).css({'display':'block'});", true);
                ScriptManager.RegisterStartupScript(this, typeof(Page), "messageScripts", "jAlert('Todas las sustancias del producto ya tienen Contraindicación con la sustancia: " + this._itemCatalog + "', 'Contraindicaciones');", true);
            }
            else
            {
                if (MedinetBusinessLogicComponent.IppaProductContraindicationsBLC.Instance.checkProductContraindications(this._categoryId, this._pharmaFormId, this._productId, this._divisionId, data[0].ActiveSubstanceId))
                {
                    MedinetBusinessEntries.ProductMedicalContraindicationInfo ProdMed = new MedinetBusinessEntries.ProductMedicalContraindicationInfo();
                    ProdMed.ActiveSubstanceId = data[0].ActiveSubstanceId;
                    ProdMed.CategoryId = this._categoryId;
                    ProdMed.DivisionId = this._divisionId;
                    ProdMed.PharmaFormId = this._pharmaFormId;
                    ProdMed.ProductId = this._productId;
                    ProdMed.MedicalContraindicationId = this._contraindication;
                    if (MedinetBusinessLogicComponent.ProductMedicalContraindicationBLC.Instance.insertContraindication(ProdMed) == 0)
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "messageScript", "jAlert('La sustancia que ha seleccionado ya esta asociada a las contraindicaciones del producto', 'Contraindicaciones');", true);
                    }
                    this.getMedicalByProduct();
                    this.getMedicalCatalog();
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "messageScript", "$(panelMed).css({'display':'block'});", true);
                }
                cleanWorkGroupMed();
            }
        }
         ScriptManager.RegisterStartupScript(this, typeof(Page), "messageScript", "$(panelMed).css({'display':'block'});", true);
    }
    protected void btnDeleteMedical_OnClick(object sender, EventArgs e)
    {
        int medicalId,activesubstanceId;
        ImageButton btnDelMed = (ImageButton)sender;

        medicalId = Convert.ToInt32(btnDelMed.Attributes["MedicalContraindicationId"]);
        activesubstanceId = Convert.ToInt32(btnDelMed.Attributes["ActiveSubstanceId"]);

        MedinetBusinessEntries.ProductMedicalContraindicationInfo ProdMed = new MedinetBusinessEntries.ProductMedicalContraindicationInfo();
        ProdMed.MedicalContraindicationId = medicalId;
        ProdMed.CategoryId = this._categoryId;
        ProdMed.DivisionId = this._divisionId;
        ProdMed.PharmaFormId = this._pharmaFormId;
        ProdMed.ProductId = this._productId;
        ProdMed.ActiveSubstanceId = activesubstanceId;
        MedinetBusinessLogicComponent.ProductMedicalContraindicationBLC.Instance.deleteProductMedicalContraindication(ProdMed);
        this.getMedicalByProduct();
        this.getMedicalCatalog();
        ScriptManager.RegisterStartupScript(this, typeof(Page), "messageScript", "$(panelMed).css({'display':'block'});", true);

    }
    protected void btnDeleteAllMed_Click(object sender, EventArgs e)
    {
        if (grdProductMedical.Rows.Count>0)
        {
            MedinetBusinessEntries.ProductMedicalContraindicationInfo ProdMed = new MedinetBusinessEntries.ProductMedicalContraindicationInfo();
            ProdMed.CategoryId = this._categoryId;
            ProdMed.DivisionId = this._divisionId;
            ProdMed.PharmaFormId = this._pharmaFormId;
            ProdMed.ProductId = this._productId;
            MedinetBusinessLogicComponent.ProductMedicalContraindicationBLC.Instance.deleteAllProductMedicalContraindication(ProdMed);
            this.getMedicalByProduct();
            this.getMedicalCatalog();
            ScriptManager.RegisterStartupScript(this, typeof(Page), "messageScript", "$(panelMed).css({'display':'block'});", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "messageScriptAlert", "jAlert('El producto no tiene contraindicaciones médicas asociadas');", true);
            ScriptManager.RegisterStartupScript(this, typeof(Page), "messageScript", "$(panelMed).css({'display':'block'});", true);
        }
       

    }
    protected void grdProductMedical_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            MedinetBusinessEntries.ProductMedicalContraindicationInfo currentRow = (MedinetBusinessEntries.ProductMedicalContraindicationInfo)e.Row.DataItem;
            ImageButton btnDelMed = (ImageButton)e.Row.FindControl("btnDeleteMedical");
            btnDelMed.Attributes.Add("MedicalContraindicationId", currentRow.MedicalContraindicationId.ToString());
            btnDelMed.Attributes.Add("ActiveSubstanceId", currentRow.ActiveSubstanceId.ToString());
            btnDelMed.Attributes.Add("onClick", "javascript:return confirmDelete('" + currentRow.MedicalContraindicationName + "')");
        }
    }
    protected void btnContinuarMed_Click(object sender, EventArgs e)
    {
        this.btnContinuarMed.Visible = false;
        this.BtnListSubstancesMed.Visible = false;
        this.lblAdvertMed.Visible = false;
        this.lblItemMed.Visible = false;
        this.btnCancelMed.Visible = false;
        bool selects = false;
        for (int i = 0; i < BtnListSubstancesMed.Items.Count; i++)
        {
            if (BtnListSubstancesMed.Items[i].Selected)
            {
                selects = true;
                int actsubsId = int.Parse(BtnListSubstancesMed.Items[i].Value);
                if (MedinetBusinessLogicComponent.IppaProductContraindicationsBLC.Instance.checkProductContraindications(this._categoryId, this._pharmaFormId, this._productId, this._divisionId, actsubsId))
                {
                    MedinetBusinessEntries.ProductMedicalContraindicationInfo pmcInfo = new MedinetBusinessEntries.ProductMedicalContraindicationInfo();
                    pmcInfo.ActiveSubstanceId = actsubsId;
                    pmcInfo.CategoryId = this._categoryId;
                    pmcInfo.DivisionId = this._divisionId;
                    pmcInfo.PharmaFormId = this._pharmaFormId;
                    pmcInfo.ProductId = this._productId;
                    pmcInfo.MedicalContraindicationId = this._contraindication;
                    if (MedinetBusinessLogicComponent.ProductMedicalContraindicationBLC.Instance.insertContraindication(pmcInfo) == 0)
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "messageScript", "jAlert('Ya existe una asociacion de " + BtnListSubstancesMed.Items[i].Text + " a las contraindicaciones del producto', 'Contraindicaciones');", true);
                    }
                }
            }
        }
        if (selects)
        {
            cleanWorkGroupMed();
            this.getMedicalByProduct();
            this.getMedicalCatalog();
        }
        else
        {
            this.btnContinuarMed.Visible = true;
            setWorkGroupMed();
            ScriptManager.RegisterStartupScript(this, typeof(Page), "messageScriptSelect", "jAlert('Debe seleccionar al menos una sustancia del producto para clasificar', 'Contraindicaciones');", true);
        }
        
        ScriptManager.RegisterStartupScript(this, typeof(Page), "messageScript", "$(panelMed).css({'display':'block'});", true);
    }
    protected void btnCancelMed_Click(object sender, EventArgs e)
    {
        cleanWorkGroupMed();
        ScriptManager.RegisterStartupScript(this, typeof(Page), "messageScript", "$(panelMed).css({'display':'block'});", true);
    }
    #endregion

    #region Medical Private Methods

    private void getMedicalCatalog()
    {
        string medicalToSearch = "";
        if (txtContraMed.Text.Trim().Length > 3)
        {
            medicalToSearch = "%" + txtContraMed.Text + "%";
        }
        else
        {
            medicalToSearch = string.IsNullOrEmpty(this.txtContraMed.Text.Trim()) ? "%" : this.txtContraMed.Text.Trim() + "%";
        }
        List<MedinetBusinessEntries.MedicalContraindicationInfo> listCatalog = MedinetBusinessLogicComponent.CatalogsBLC.Instance.getMedicalContraindicationsWithOutAttach(medicalToSearch, this._productId, this._pharmaFormId, this._divisionId, this._categoryId);
       
        this.grdMedical.DataSource = listCatalog;
        this.grdMedical.DataBind();
    }

    private void getMedicalByProduct()
    {
        this.grdProductMedical.DataSource = MedinetBusinessLogicComponent.CatalogsBLC.Instance.getProductMedicalContraindications(this._productId, this._pharmaFormId, this._divisionId, this._categoryId);
        this.grdProductMedical.DataBind();
        checkContraindications();
        if (grdProductMedical.Rows.Count > 0)
            btnDeleteAllMed.Visible = true;
        else
            btnDeleteAllMed.Visible = false;
    }

    private void prepareGridMedical(short pagesize)
    {
        if (pagesize > 0)
        {
            this.grdMedical.AllowPaging = true;
            this.grdMedical.PageSize = pagesize;
            this.grdMedical.PageIndex = this.Session["CurrentPage"] == null ? 0 : Convert.ToInt32(this.Session["CurrentPage"]);
        }
        else
            this.grdMedical.AllowPaging = false;
    }

    private void cleanWorkGroupMed()
    {
        BtnListSubstancesMed.DataSource = null;
        BtnListSubstancesMed.Visible = false;
        lblAdvertMed.Visible = false;
        lblItemMed.Visible = false;
        btnCancelMed.Visible = false;
        btnContinuarMed.Visible = false;
    }

    private void setWorkGroupMed()
    {
        BtnListSubstancesMed.Visible = true;
        lblAdvertMed.Visible = true;
        lblItemMed.Visible = true;
        btnCancelMed.Visible = true;

    }


    #endregion

    #region Physiologic Events
    protected void btnPhys_Click(object sender, EventArgs e)
    {
        this.prepareGridPhys(Convert.ToInt16(this.ddlPageSizeFisiologic.SelectedValue));
        this.getPhysCatalog();
        this.txtfisiologic.Focus();
        ScriptManager.RegisterStartupScript(this, typeof(Page), "messageScript", "$(panelfis).css({'display':'block'});", true);
    }
    protected void ddlPhysPageSize_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.Session["CurrentPage"] = 0;
        this.prepareGridPhys(Convert.ToInt16(this.ddlPageSizeFisiologic.SelectedValue));
        this.getPhysCatalog();
        ScriptManager.RegisterStartupScript(this, typeof(Page), "messageScript", "$(panelfis).css({'display':'block'});", true);
    }
    protected void grdPhys_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            MedinetBusinessEntries.PhysiologicalContraindicationInfo currentRow = (MedinetBusinessEntries.PhysiologicalContraindicationInfo)e.Row.DataItem;
            ImageButton btnAddPhys = (ImageButton)e.Row.FindControl("btnAddFisiol");
            btnAddPhys.Attributes.Add("PhysContraindicationId", currentRow.PhysContraindicationId.ToString());
            btnAddPhys.Attributes.Add("PhysContraindicationName", currentRow.PhysContraindicationName.ToString());
        }
    }
    protected void grdPhys_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        this.grdFisiologic.PageIndex = e.NewPageIndex;
        this.Session["CurrentPage"] = Convert.ToInt32(e.NewPageIndex);
        this.getPhysCatalog();
        ScriptManager.RegisterStartupScript(this, typeof(Page), "messageScript", "$(panelfis).css({'display':'block'});", true);
    }
    protected void btnAddPhys_OnClick(object sender, EventArgs e)
    {
        
        ImageButton btnAddPhys = (ImageButton)sender;
        this.Session["contraindicationId"] = Convert.ToInt32(btnAddPhys.Attributes["PhysContraindicationId"]);
        this._contraindication = Convert.ToInt32(btnAddPhys.Attributes["PhysContraindicationId"]);
        this.Session["itemCatalog"] = btnAddPhys.Attributes["PhysContraindicationName"];
        this._itemCatalog = btnAddPhys.Attributes["PhysContraindicationName"];

        List<MedinetBusinessEntries.ActiveSubstanceInfo> data = MedinetBusinessLogicComponent.CatalogsBLC.Instance.getSubstancesByProduct(this._productId);

        foreach (MedinetBusinessEntries.ProductPhysiologicalContraindicationInfo item in MedinetBusinessLogicComponent.ProductPhysiologicalContraindicationsBLC.Instance.getPhysicologicalContraindications(this._productId, this._categoryId, this._divisionId, this._pharmaFormId, this._contraindication))
        {
            var sust = (from c in data
                        where c.ActiveSubstanceId == item.ActiveSubstanceId
                        select c);
            if (sust.Count() > 0)
            {
                data.Remove(sust.Single());
            }
        }
        if (data.Count > 1)
        {
            cleanWorkGroupPhy();
            setWorkGroupPhy();
            btnContinuarPhy.Visible = true;
            lblAdvertPhy.Text = "El actual producto contiene más de una sustancia sin Contraindicación, marque cuál(es) están contraindicadas con : ";
            lblItemPhy.Text = this._itemCatalog;
            BtnListSubstancesPhy.DataSource = data;
            BtnListSubstancesPhy.DataValueField = "ActiveSubstanceId";
            BtnListSubstancesPhy.DataTextField = "Description";
            BtnListSubstancesPhy.DataBind();
        }
        else
        {
            if (data.Count <= 0)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "messageScript", "$(panelfis).css({'display':'block'});", true);
                ScriptManager.RegisterStartupScript(this, typeof(Page), "messageScripts", "jAlert('Todas las sustancias del producto ya tienen Contraindicación con la sustancia: " + this._itemCatalog + "', 'Contraindicaciones');", true);
            }
            else
            {
                if (MedinetBusinessLogicComponent.IppaProductContraindicationsBLC.Instance.checkProductContraindications(this._categoryId, this._pharmaFormId, this._productId, this._divisionId, data[0].ActiveSubstanceId))
                {
                    MedinetBusinessEntries.ProductPhysiologicalContraindicationInfo ProdPhy = new MedinetBusinessEntries.ProductPhysiologicalContraindicationInfo();
                    ProdPhy.ActiveSubstanceId = data[0].ActiveSubstanceId;
                    ProdPhy.CategoryId = this._categoryId;
                    ProdPhy.DivisionId = this._divisionId;
                    ProdPhy.PharmaFormId = this._pharmaFormId;
                    ProdPhy.ProductId = this._productId;
                    ProdPhy.PhysContraindicationId = this._contraindication;
                    if (MedinetBusinessLogicComponent.ProductPhysiologicalContraindicationsBLC.Instance.addProductPhysContraintication(ProdPhy) == 0)
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "messageScript", "jAlert('La sustancia que ha seleccionado ya esta asociada a las contraindicaciones del producto', 'Contraindicaciones');", true);
                    }
                    this.getPhysByProduct();
                    this.getPhysCatalog();
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "messageScript", "$(panelfis).css({'display':'block'});", true);
                }
                cleanWorkGroupPhy();
            }
        }
        ScriptManager.RegisterStartupScript(this, typeof(Page), "messageScript", "$(panelfis).css({'display':'block'});", true);
    }
    protected void btnDeletePhys_OnClick(object sender, EventArgs e)
    {
        int physlId,activeSubstanceId;
        ImageButton btnDelPhys = (ImageButton)sender;

        physlId = Convert.ToInt32(btnDelPhys.Attributes["PhysContraindicationId"]);
        activeSubstanceId = Convert.ToInt32(btnDelPhys.Attributes["ActiveSubstanceId"]);

        MedinetBusinessEntries.ProductPhysiologicalContraindicationInfo ProdPhys = new MedinetBusinessEntries.ProductPhysiologicalContraindicationInfo();
        ProdPhys.PhysContraindicationId = physlId;
        ProdPhys.CategoryId = this._categoryId;
        ProdPhys.DivisionId = this._divisionId;
        ProdPhys.PharmaFormId = this._pharmaFormId;
        ProdPhys.ProductId = this._productId;
        ProdPhys.ActiveSubstanceId = activeSubstanceId;
        MedinetBusinessLogicComponent.ProductPhysiologicalContraindicationsBLC.Instance.deleteProductPhysContraindication(ProdPhys);
        this.getPhysByProduct();
        this.getPhysCatalog();
        ScriptManager.RegisterStartupScript(this, typeof(Page), "messageScript", "$(panelfis).css({'display':'block'});", true);

    }
    protected void grdProductPhys_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            MedinetBusinessEntries.ProductPhysiologicalContraindicationInfo currentRow = (MedinetBusinessEntries.ProductPhysiologicalContraindicationInfo)e.Row.DataItem;
            ImageButton btnDelPhys = (ImageButton)e.Row.FindControl("btnDeleteFisiol");
            btnDelPhys.Attributes.Add("PhysContraindicationId", currentRow.PhysContraindicationId.ToString());
            btnDelPhys.Attributes.Add("ActiveSubstanceId", currentRow.ActiveSubstanceId.ToString());
            btnDelPhys.Attributes.Add("onClick", "javascript:return confirmDelete('" + currentRow.PhysContraindicationName + "')");
        }
    }
    protected void btnContinuarPhy_Click(object sender, EventArgs e)
    {
        this.btnContinuarPhy.Visible = false;
        this.BtnListSubstancesPhy.Visible = false;
        this.lblAdvertPhy.Visible = false;
        this.lblItemPhy.Visible = false;
        this.btnCancelPhy.Visible = false;
        bool selects = false;
        for (int i = 0; i < BtnListSubstancesPhy.Items.Count; i++)
        {
            if (BtnListSubstancesPhy.Items[i].Selected)
            {
                selects = true;
                int actsubsId = int.Parse(BtnListSubstancesPhy.Items[i].Value);
                if (MedinetBusinessLogicComponent.IppaProductContraindicationsBLC.Instance.checkProductContraindications(this._categoryId, this._pharmaFormId, this._productId, this._divisionId, actsubsId))
                {
                    MedinetBusinessEntries.ProductPhysiologicalContraindicationInfo ppcInfo = new MedinetBusinessEntries.ProductPhysiologicalContraindicationInfo();
                    ppcInfo.ActiveSubstanceId = actsubsId;
                    ppcInfo.CategoryId = this._categoryId;
                    ppcInfo.DivisionId = this._divisionId;
                    ppcInfo.PharmaFormId = this._pharmaFormId;
                    ppcInfo.ProductId = this._productId;
                    ppcInfo.PhysContraindicationId = this._contraindication;
                    if (MedinetBusinessLogicComponent.ProductPhysiologicalContraindicationsBLC.Instance.addProductPhysContraintication(ppcInfo) == 0)
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "messageScript", "jAlert('Ya existe una asociacion de " + BtnListSubstancesPhy.Items[i].Text + " a las contraindicaciones del producto', 'Contraindicaciones');", true);
                    }
                }
            }
        }
      
        if (selects)
        {
            cleanWorkGroupPhy();
            this.getPhysByProduct();
            this.getPhysCatalog();
        }
        else
        {
            this.btnContinuarPhy.Visible = true;
            setWorkGroupPhy();
            ScriptManager.RegisterStartupScript(this, typeof(Page), "messageScriptSelect", "jAlert('Debe seleccionar al menos una sustancia del producto para clasificar', 'Contraindicaciones');", true);
        }
        ScriptManager.RegisterStartupScript(this, typeof(Page), "messageScript", "$(panelfis).css({'display':'block'});", true);
    }
    protected void btnCancelPhy_Click(object sender, EventArgs e)
    {
        cleanWorkGroupPhy();
        ScriptManager.RegisterStartupScript(this, typeof(Page), "messageScript", "$(panelfis).css({'display':'block'});", true);
    }
    protected void btnDeleteAllPhy_Click(object sender, EventArgs e)
    {
        if (grdProductFisi.Rows.Count > 0)
        {
            MedinetBusinessEntries.ProductPhysiologicalContraindicationInfo ProdCon = new MedinetBusinessEntries.ProductPhysiologicalContraindicationInfo();
            ProdCon.CategoryId = this._categoryId;
            ProdCon.DivisionId = this._divisionId;
            ProdCon.PharmaFormId = this._pharmaFormId;
            ProdCon.ProductId = this._productId;
            MedinetBusinessLogicComponent.ProductPhysiologicalContraindicationsBLC.Instance.deleteProductPhysContraindicationAll(ProdCon);
            this.getPhysByProduct();
            this.getPhysCatalog();
            ScriptManager.RegisterStartupScript(this, typeof(Page), "messageScript", "$(panelfis).css({'display':'block'});", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "messageScriptAlert", "jAlert('El producto no tiene contraindicaciones fisiológicas');", true);
            ScriptManager.RegisterStartupScript(this, typeof(Page), "messageScript", "$(panelfis).css({'display':'block'});", true);
        }


    }
    #endregion

    #region Physiologic Private Methods

    private void getPhysCatalog()
    {
        string physToSearch = "";
        if (txtfisiologic.Text.Trim().Length > 3)
        {
            physToSearch = "%" + txtfisiologic.Text + "%";
        }
        else
        {
            physToSearch = string.IsNullOrEmpty(this.txtfisiologic.Text.Trim()) ? "%" : this.txtfisiologic.Text.Trim() + "%";
        }
        List<MedinetBusinessEntries.PhysiologicalContraindicationInfo> listCatalog = MedinetBusinessLogicComponent.CatalogsBLC.Instance.getPhysContraindicationsWithOutAttach(physToSearch, this._productId, this._pharmaFormId, this._divisionId, this._categoryId);
        
        this.grdFisiologic.DataSource = listCatalog;
        this.grdFisiologic.DataBind();
    }

    private void getPhysByProduct()
    {
        this.grdProductFisi.DataSource = MedinetBusinessLogicComponent.CatalogsBLC.Instance.getProductPhysContraindications(this._productId, this._pharmaFormId, this._divisionId, this._categoryId);
        this.grdProductFisi.DataBind();
        checkContraindications();
        if (grdProductFisi.Rows.Count > 0)
            btnDeleteAllPhy.Visible = true;
        else
            btnDeleteAllPhy.Visible = false;
    }

    private void prepareGridPhys(short pagesize)
    {
        if (pagesize > 0)
        {
            this.grdFisiologic.AllowPaging = true;
            this.grdFisiologic.PageSize = pagesize;
            this.grdFisiologic.PageIndex = this.Session["CurrentPage"] == null ? 0 : Convert.ToInt32(this.Session["CurrentPage"]);
        }
        else
            this.grdFisiologic.AllowPaging = false;
    }

    private void cleanWorkGroupPhy()
    {
        BtnListSubstancesPhy.DataSource = null;
        BtnListSubstancesPhy.Visible = false;
        lblAdvertPhy.Visible = false;
        lblItemPhy.Visible = false;
        btnCancelPhy.Visible = false;
        btnContinuarPhy.Visible = false;
    }

    private void setWorkGroupPhy()
    {
        BtnListSubstancesPhy.Visible = true;
        lblAdvertPhy.Visible = true;
        lblItemPhy.Visible = true;
        btnCancelPhy.Visible = true;
    }

    #endregion

    #region Pharmacological Events
    protected void btnPharma_Click(object sender, EventArgs e)
    {
        this.prepareGridPharma(Convert.ToInt16(this.ddlPageSizePharma.SelectedValue));
        this.getPharmaCatalog();
        this.txtPharma.Focus();
        ScriptManager.RegisterStartupScript(this, typeof(Page), "messageScript", "$(panelPharma).css({'display':'block'});", true);
    }
    protected void ddlPharmaPageSize_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.Session["CurrentPage"] = 0;
        this.prepareGridPharma(Convert.ToInt16(this.ddlPageSizePharma.SelectedValue));
        this.getPharmaCatalog();
        ScriptManager.RegisterStartupScript(this, typeof(Page), "messageScript", "$(panelPharma).css({'display':'block'});", true);
    }
    protected void grdPharma_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            MedinetBusinessEntries.PharmacologicalContraindicationInfo currentRow = (MedinetBusinessEntries.PharmacologicalContraindicationInfo)e.Row.DataItem;
            ImageButton btnAddPharma = (ImageButton)e.Row.FindControl("btnAddPharma");
            btnAddPharma.Attributes.Add("PharmaContraindicationId", currentRow.PharmaContraindicationId.ToString());
            btnAddPharma.Attributes.Add("PharmaContraindicationName", currentRow.PharmaContraindicationName.ToString());
        }
    }
    protected void grdPharma_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        this.grdPharma.PageIndex = e.NewPageIndex;
        this.Session["CurrentPage"] = Convert.ToInt32(e.NewPageIndex);
        this.getPharmaCatalog();
        ScriptManager.RegisterStartupScript(this, typeof(Page), "messageScript", "$(panelPharma).css({'display':'block'});", true);
    }
    protected void btnAddPharma_OnClick(object sender, EventArgs e)
    {
        ImageButton btnAddPharma = (ImageButton)sender;
        this.Session["contraindicationId"] = Convert.ToInt32(btnAddPharma.Attributes["PharmaContraindicationId"]);
        this._contraindication = Convert.ToInt32(btnAddPharma.Attributes["PharmaContraindicationId"]);
        this.Session["itemCatalog"] = btnAddPharma.Attributes["PharmaContraindicationName"];
        this._itemCatalog = btnAddPharma.Attributes["PharmaContraindicationName"];

        List<MedinetBusinessEntries.ActiveSubstanceInfo> data = MedinetBusinessLogicComponent.CatalogsBLC.Instance.getSubstancesByProduct(this._productId);

        foreach (MedinetBusinessEntries.ProductPharmacologicalContraindicationInfo item in MedinetBusinessLogicComponent.ProductPharmacologicalContraindicationsBLC.Instance.getPharmacologicalContraindications(this._productId, this._categoryId, this._divisionId, this._pharmaFormId, this._contraindication))
        {
            var sust = (from c in data
                        where c.ActiveSubstanceId == item.ActiveSubstanceId
                        select c);
            if (sust.Count() > 0)
            {
                data.Remove(sust.Single());
            }
        }
        if (data.Count > 1)
        {
            cleanWorkGroupPha();
            setWorkGroupPha();
            btnContinuarPha.Visible = true;
            lblAdvertPha.Text = "El actual producto contiene más de una sustancia sin Contraindicación, marque cuál(es) están contraindicadas con : ";
            lblItemPha.Text = this._itemCatalog;
            BtnListSubstancesPha.DataSource = data;
            BtnListSubstancesPha.DataValueField = "ActiveSubstanceId";
            BtnListSubstancesPha.DataTextField = "Description";
            BtnListSubstancesPha.DataBind();
        }
        else
        {
            if (data.Count <= 0)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "messageScript", "$(panelPharma).css({'display':'block'});", true);
                ScriptManager.RegisterStartupScript(this, typeof(Page), "messageScripts", "jAlert('Todas las sustancias del producto ya tienen Contraindicación con la sustancia: " + this._itemCatalog + "', 'Contraindicaciones');", true);
            }
            else
            {
                if (MedinetBusinessLogicComponent.IppaProductContraindicationsBLC.Instance.checkProductContraindications(this._categoryId, this._pharmaFormId, this._productId, this._divisionId, data[0].ActiveSubstanceId))
                {
                    MedinetBusinessEntries.ProductPharmacologicalContraindicationInfo ProdPha = new MedinetBusinessEntries.ProductPharmacologicalContraindicationInfo();
                    ProdPha.ActiveSubstanceId = data[0].ActiveSubstanceId;
                    ProdPha.CategoryId = this._categoryId;
                    ProdPha.DivisionId = this._divisionId;
                    ProdPha.PharmaFormId = this._pharmaFormId;
                    ProdPha.ProductId = this._productId;
                    ProdPha.PharmaContraindicationId = this._contraindication;
                    if (MedinetBusinessLogicComponent.ProductPharmacologicalContraindicationsBLC.Instance.addProductPharmaContraintication(ProdPha) == 0)
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "messageScript", "jAlert('La sustancia que ha seleccionado ya esta asociada a las contraindicaciones del producto', 'Contraindicaciones');", true);
                    }
                    this.getPharmaByProduct();
                    this.getPharmaCatalog();
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "messageScript", "$(panelPharma).css({'display':'block'});", true);
                }
                cleanWorkGroupPha();
            }
        }
        ScriptManager.RegisterStartupScript(this, typeof(Page), "messageScript", "$(panelPharma).css({'display':'block'});", true);

    }
    protected void btnDeletePharma_OnClick(object sender, EventArgs e)
    {
        int pharmalId,activeSubstanceId;
        ImageButton btnDelPharma = (ImageButton)sender;

        pharmalId = Convert.ToInt32(btnDelPharma.Attributes["PharmaContraindicationId"]);
        activeSubstanceId = Convert.ToInt32(btnDelPharma.Attributes["ActiveSubstanceId"]);
        MedinetBusinessEntries.ProductPharmacologicalContraindicationInfo ProdPha = new MedinetBusinessEntries.ProductPharmacologicalContraindicationInfo();
        ProdPha.PharmaContraindicationId = pharmalId;
        ProdPha.CategoryId = this._categoryId;
        ProdPha.DivisionId = this._divisionId;
        ProdPha.PharmaFormId = this._pharmaFormId;
        ProdPha.ProductId = this._productId;
        ProdPha.ActiveSubstanceId = activeSubstanceId;
        MedinetBusinessLogicComponent.ProductPharmacologicalContraindicationsBLC.Instance.deleteProductPharmaContraindication(ProdPha);
        this.getPharmaByProduct();
        this.getPharmaCatalog();
        ScriptManager.RegisterStartupScript(this, typeof(Page), "messageScript", "$(panelPharma).css({'display':'block'});", true);

    }
    protected void grdProductPharma_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            MedinetBusinessEntries.ProductPharmacologicalContraindicationInfo currentRow = (MedinetBusinessEntries.ProductPharmacologicalContraindicationInfo)e.Row.DataItem;
            ImageButton btnDelPharma = (ImageButton)e.Row.FindControl("btnDeletePharma");
            btnDelPharma.Attributes.Add("PharmaContraindicationId", currentRow.PharmaContraindicationId.ToString());
            btnDelPharma.Attributes.Add("ActiveSubstanceId", currentRow.ActiveSubstanceId.ToString());
            btnDelPharma.Attributes.Add("onClick", "javascript:return confirmDelete('" + currentRow.PharmaContraindicationName + "')");
        }
    }
    protected void btnContinuarPha_Click(object sender, EventArgs e)
    {
        this.btnContinuarPha.Visible = false;
        this.BtnListSubstancesPha.Visible = false;
        this.lblAdvertPha.Visible = false;
        this.lblItemPha.Visible = false;
        this.btnCancelPha.Visible = false;
        bool selects = false;
        for (int i = 0; i < BtnListSubstancesPha.Items.Count; i++)
        {
            if (BtnListSubstancesPha.Items[i].Selected)
            {
                selects = true;
                int actsubsId = int.Parse(BtnListSubstancesPha.Items[i].Value);
                if (MedinetBusinessLogicComponent.IppaProductContraindicationsBLC.Instance.checkProductContraindications(this._categoryId, this._pharmaFormId, this._productId, this._divisionId, actsubsId))
                {
                    MedinetBusinessEntries.ProductPharmacologicalContraindicationInfo ppcInfo = new MedinetBusinessEntries.ProductPharmacologicalContraindicationInfo();
                    ppcInfo.ActiveSubstanceId = actsubsId;
                    ppcInfo.CategoryId = this._categoryId;
                    ppcInfo.DivisionId = this._divisionId;
                    ppcInfo.PharmaFormId = this._pharmaFormId;
                    ppcInfo.ProductId = this._productId;
                    ppcInfo.PharmaContraindicationId = this._contraindication;
                    if (MedinetBusinessLogicComponent.ProductPharmacologicalContraindicationsBLC.Instance.addProductPharmaContraintication(ppcInfo) == 0)
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "messageScript", "jAlert('Ya existe una asociacion de " + BtnListSubstancesPhy.Items[i].Text + " a las contraindicaciones del producto', 'Contraindicaciones');", true);
                    }
                }
            }
        }

        if (selects)
        {
            cleanWorkGroupPha();
            this.getPharmaByProduct();
            this.getPharmaCatalog();
        }
        else
        {
            this.btnContinuarPha.Visible = true;
            setWorkGroupPha();
            ScriptManager.RegisterStartupScript(this, typeof(Page), "messageScriptSelect", "jAlert('Debe seleccionar al menos una sustancia del producto para clasificar', 'Contraindicaciones');", true);
        }
        
        ScriptManager.RegisterStartupScript(this, typeof(Page), "messageScript", "$(panelPharma).css({'display':'block'});", true);
    }
    protected void btnCancelPha_Click(object sender, EventArgs e)
    {
        cleanWorkGroupPha();
        ScriptManager.RegisterStartupScript(this, typeof(Page), "messageScript", "$(panelPharma).css({'display':'block'});", true);
    }
    protected void btnDeleteAllPha_Click(object sender, EventArgs e)
    {
        if (grdProductPharma.Rows.Count > 0)
        {
            MedinetBusinessEntries.ProductPharmacologicalContraindicationInfo ProdCon = new MedinetBusinessEntries.ProductPharmacologicalContraindicationInfo();
            ProdCon.CategoryId = this._categoryId;
            ProdCon.DivisionId = this._divisionId;
            ProdCon.PharmaFormId = this._pharmaFormId;
            ProdCon.ProductId = this._productId;
            MedinetBusinessLogicComponent.ProductPharmacologicalContraindicationsBLC.Instance.deleteProductPharmaContraindicationAll(ProdCon);
            this.getPharmaByProduct();
            this.getPharmaCatalog();
            
            ScriptManager.RegisterStartupScript(this, typeof(Page), "messageScript", "$(panelPharma).css({'display':'block'});", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "messageScriptAlert", "jAlert('El producto no tiene contraindicaciones Farmacológicas');", true);
            ScriptManager.RegisterStartupScript(this, typeof(Page), "messageScript", "$(panelPharma).css({'display':'block'});", true);
        }


    }


    #endregion

    #region Pharmacological Private Methods

    private void getPharmaCatalog()
    {
        string pharmaToSearch = "";
        if (txtPharma.Text.Trim().Length > 3)
        {
            pharmaToSearch = "%" + txtPharma.Text + "%";
        }
        else
        {
            pharmaToSearch = string.IsNullOrEmpty(this.txtPharma.Text.Trim()) ? "%" : this.txtPharma.Text.Trim() + "%";
        }
        List<MedinetBusinessEntries.PharmacologicalContraindicationInfo> listCatalog = MedinetBusinessLogicComponent.CatalogsBLC.Instance.getPharmacologicalContraindicationsWithOutAttach(pharmaToSearch, this._productId, this._pharmaFormId, this._divisionId, this._categoryId);
        
        this.grdPharma.DataSource = listCatalog;
        this.grdPharma.DataBind();
    }

    private void getPharmaByProduct()
    {
        this.grdProductPharma.DataSource = MedinetBusinessLogicComponent.CatalogsBLC.Instance.getProductPharmacologicalContraindications(this._productId, this._pharmaFormId, this._divisionId, this._categoryId);
        this.grdProductPharma.DataBind();
        checkContraindications();
        if (grdProductPharma.Rows.Count > 0)
            btnDeleteAllPha.Visible = true;
        else
            btnDeleteAllPha.Visible = false;
    }

    private void prepareGridPharma(short pagesize)
    {
        if (pagesize > 0)
        {
            this.grdPharma.AllowPaging = true;
            this.grdPharma.PageSize = pagesize;
            this.grdPharma.PageIndex = this.Session["CurrentPage"] == null ? 0 : Convert.ToInt32(this.Session["CurrentPage"]);
        }
        else
            this.grdPharma.AllowPaging = false;
    }

    private void cleanWorkGroupPha()
    {
        BtnListSubstancesPha.DataSource = null;
        BtnListSubstancesPha.Visible = false;
        lblAdvertPha.Visible = false;
        lblItemPha.Visible = false;
        btnCancelPha.Visible = false;
        btnContinuarPha.Visible = false;
    }

    private void setWorkGroupPha()
    {
        BtnListSubstancesPha.Visible = true;
        lblAdvertPha.Visible = true;
        lblItemPha.Visible = true;
        btnCancelPha.Visible = true;
    }
    #endregion

    #region PharmaGroup Events
    protected void btnPharmaGroup_Click(object sender, EventArgs e)
    {
        this.prepareGridPharmaGroup(Convert.ToInt16(this.ddlPageSizeGroups.SelectedValue));
        this.getPharmaGroupCatalog();
        this.txtGruops.Focus();
        ScriptManager.RegisterStartupScript(this, typeof(Page), "messageScript", "$(panelGroups).css({'display':'block'});", true);
    }
    protected void ddlPharmaGroupPageSize_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.Session["CurrentPage"] = 0;
        this.prepareGridPharmaGroup(Convert.ToInt16(this.ddlPageSizeGroups.SelectedValue));
        this.getPharmaGroupCatalog();
        ScriptManager.RegisterStartupScript(this, typeof(Page), "messageScript", "$(panelGroups).css({'display':'block'});", true);
    }
    protected void grdPharmaGroups_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            MedinetBusinessEntries.PharmacologicalGroupsInfo currentRow = (MedinetBusinessEntries.PharmacologicalGroupsInfo)e.Row.DataItem;
            ImageButton btnAddGroup = (ImageButton)e.Row.FindControl("btnAddGroup");
            btnAddGroup.Attributes.Add("PharmaGroupId", currentRow.PharmaGroupId.ToString());
            btnAddGroup.Attributes.Add("GroupName", currentRow.GroupName.ToString());

        }
    }
    protected void grdPharmaGroups_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        this.grdGroups.PageIndex = e.NewPageIndex;
        this.Session["CurrentPage"] = Convert.ToInt32(e.NewPageIndex);
        this.getPharmaGroupCatalog();
        ScriptManager.RegisterStartupScript(this, typeof(Page), "messageScript", "$(panelGroups).css({'display':'block'});", true);
    }
    protected void btnAddPharmaGroup_OnClick(object sender, EventArgs e)
    {
        
        ImageButton btnAddGroup = (ImageButton)sender;
        this.Session["contraindicationId"] = Convert.ToInt32(btnAddGroup.Attributes["PharmaGroupId"]);
        this._contraindication = Convert.ToInt32(btnAddGroup.Attributes["PharmaGroupId"]);
        this.Session["itemCatalog"] = btnAddGroup.Attributes["GroupName"];
        this._itemCatalog = btnAddGroup.Attributes["GroupName"];

        List<MedinetBusinessEntries.ActiveSubstanceInfo> data = MedinetBusinessLogicComponent.CatalogsBLC.Instance.getSubstancesByProduct(this._productId);

        foreach (MedinetBusinessEntries.ProductPharmaGroupContraindicationInfo item in MedinetBusinessLogicComponent.ProductPharmaGroupContraindicationBLC.Instance.getPharmaGroupsContraindications(this._productId, this._categoryId, this._divisionId, this._pharmaFormId, this._contraindication))
        {
            var sust = (from c in data
                        where c.ActiveSubstanceId == item.ActiveSubstanceId
                        select c);
            if (sust.Count() > 0)
            {
                data.Remove(sust.Single());
            }
        }
        if (data.Count > 1)
        {
            cleanWorkGroupGro();
            setWorkGroupGro();
            btnContinuarGro.Visible = true;
            lblAdvertGro.Text = "El actual producto contiene más de una sustancia sin Contraindicación, marque cuál(es) están contraindicadas con : ";
            lblItemGro.Text = this._itemCatalog;
            BtnListSubstancesGro.DataSource = data;
            BtnListSubstancesGro.DataValueField = "ActiveSubstanceId";
            BtnListSubstancesGro.DataTextField = "Description";
            BtnListSubstancesGro.DataBind();
        }
        else
        {
            if (data.Count <= 0)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "messageScript", "$(panelGroups).css({'display':'block'});", true);
                ScriptManager.RegisterStartupScript(this, typeof(Page), "messageScripts", "jAlert('Todas las sustancias del producto ya tienen Contraindicación con la sustancia: " + this._itemCatalog + "', 'Contraindicaciones');", true);
            }
            else
            {
                if (MedinetBusinessLogicComponent.IppaProductContraindicationsBLC.Instance.checkProductContraindications(this._categoryId, this._pharmaFormId, this._productId, this._divisionId, data[0].ActiveSubstanceId))
                {
                    MedinetBusinessEntries.ProductPharmaGroupContraindicationInfo ProdGro = new MedinetBusinessEntries.ProductPharmaGroupContraindicationInfo();
                    ProdGro.ActiveSubstanceId = data[0].ActiveSubstanceId;
                    ProdGro.CategoryId = this._categoryId;
                    ProdGro.DivisionId = this._divisionId;
                    ProdGro.PharmaFormId = this._pharmaFormId;
                    ProdGro.ProductId = this._productId;
                    ProdGro.PharmaGroupId = this._contraindication;
                    if (MedinetBusinessLogicComponent.ProductPharmaGroupContraindicationBLC.Instance.addProductPharmaGroupContraintication(ProdGro) == 0)
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "messageScript", "jAlert('La sustancia que ha seleccionado ya esta asociada a las contraindicaciones del producto', 'Contraindicaciones');", true);
                    }
                    this.getPharmaGroupByProduct();
                    this.getPharmaGroupCatalog();
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "messageScript", "$(panelGroups).css({'display':'block'});", true);
                }
                cleanWorkGroupGro();
            }
        }
        ScriptManager.RegisterStartupScript(this, typeof(Page), "messageScript", "$(panelGroups).css({'display':'block'});", true);

    }
    protected void btnDeletePharmaGroup_OnClick(object sender, EventArgs e)
    {
        int grouplId,activeSubstanceId;
        ImageButton btnDelGroup = (ImageButton)sender;

        grouplId = Convert.ToInt32(btnDelGroup.Attributes["PharmaGroupId"]);
        activeSubstanceId = Convert.ToInt32(btnDelGroup.Attributes["ActiveSubstanceId"]);
        MedinetBusinessEntries.ProductPharmaGroupContraindicationInfo ProdPha = new MedinetBusinessEntries.ProductPharmaGroupContraindicationInfo();
        ProdPha.PharmaGroupId = grouplId;
        ProdPha.CategoryId = this._categoryId;
        ProdPha.DivisionId = this._divisionId;
        ProdPha.PharmaFormId = this._pharmaFormId;
        ProdPha.ProductId = this._productId;
        ProdPha.ActiveSubstanceId = activeSubstanceId;
        MedinetBusinessLogicComponent.ProductPharmaGroupContraindicationBLC.Instance.deleteProductPharmaGroupContraindication(ProdPha);
        this.getPharmaGroupByProduct();
        this.getPharmaGroupCatalog();
        ScriptManager.RegisterStartupScript(this, typeof(Page), "messageScript", "$(panelGroups).css({'display':'block'});", true);

    }
    protected void grdProductPharmaGroup_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            MedinetBusinessEntries.ProductPharmaGroupContraindicationInfo currentRow = (MedinetBusinessEntries.ProductPharmaGroupContraindicationInfo)e.Row.DataItem;
            ImageButton btnDelGroup = (ImageButton)e.Row.FindControl("btnDeleteGroup");
            btnDelGroup.Attributes.Add("PharmaGroupId", currentRow.PharmaGroupId.ToString());
            btnDelGroup.Attributes.Add("ActiveSubstanceId", currentRow.ActiveSubstanceId.ToString());
            btnDelGroup.Attributes.Add("onClick", "javascript:return confirmDelete('" + currentRow.GroupName + "')");
        }
    }
    protected void btnContinuarGro_Click(object sender, EventArgs e)
    {
        this.btnContinuarGro.Visible = false;
        this.BtnListSubstancesGro.Visible = false;
        this.lblAdvertGro.Visible = false;
        this.lblItemGro.Visible = false;
        this.btnCancelGro.Visible = false;
        bool selects = false;
        for (int i = 0; i < BtnListSubstancesGro.Items.Count; i++)
        {
            if (BtnListSubstancesGro.Items[i].Selected)
            {
                selects = true;
                int actsubsId = int.Parse(BtnListSubstancesGro.Items[i].Value);
                if (MedinetBusinessLogicComponent.IppaProductContraindicationsBLC.Instance.checkProductContraindications(this._categoryId, this._pharmaFormId, this._productId, this._divisionId, actsubsId))
                {
                    MedinetBusinessEntries.ProductPharmaGroupContraindicationInfo pgcInfo = new MedinetBusinessEntries.ProductPharmaGroupContraindicationInfo();
                    pgcInfo.ActiveSubstanceId = actsubsId;
                    pgcInfo.CategoryId = this._categoryId;
                    pgcInfo.DivisionId = this._divisionId;
                    pgcInfo.PharmaFormId = this._pharmaFormId;
                    pgcInfo.ProductId = this._productId;
                    pgcInfo.PharmaGroupId = this._contraindication;
                    if (MedinetBusinessLogicComponent.ProductPharmaGroupContraindicationBLC.Instance.addProductPharmaGroupContraintication(pgcInfo) == 0)
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "messageScript", "jAlert('Ya existe una asociacion de " + BtnListSubstancesGro.Items[i].Text + " a las contraindicaciones del producto', 'Contraindicaciones');", true);
                    }
                }
            }
        }
        if (selects)
        {
            cleanWorkGroupGro();
            this.getPharmaGroupByProduct();
            this.getPharmaGroupCatalog();
        }
        else
        {
            this.btnContinuarGro.Visible = true;
            setWorkGroupGro();
            ScriptManager.RegisterStartupScript(this, typeof(Page), "messageScriptSelect", "jAlert('Debe seleccionar al menos una sustancia del producto para clasificar', 'Contraindicaciones');", true);
        }
        
        ScriptManager.RegisterStartupScript(this, typeof(Page), "messageScript", "$(panelGroups).css({'display':'block'});", true);
    }
    protected void btnCancelGro_Click(object sender, EventArgs e)
    {
        cleanWorkGroupGro();
        ScriptManager.RegisterStartupScript(this, typeof(Page), "messageScript", "$(panelGroup).css({'display':'block'});", true);
    }
    protected void btnDeleteAllGro_Click(object sender, EventArgs e)
    {
        if (grdProductGroups.Rows.Count > 0)
        {
            MedinetBusinessEntries.ProductPharmaGroupContraindicationInfo ProdCon = new MedinetBusinessEntries.ProductPharmaGroupContraindicationInfo();
            ProdCon.CategoryId = this._categoryId;
            ProdCon.DivisionId = this._divisionId;
            ProdCon.PharmaFormId = this._pharmaFormId;
            ProdCon.ProductId = this._productId;
            MedinetBusinessLogicComponent.ProductPharmaGroupContraindicationBLC.Instance.deleteProductPharmaGroupContraindicationAll(ProdCon);
            this.getPharmaGroupByProduct();
            this.getPharmaGroupCatalog();

            ScriptManager.RegisterStartupScript(this, typeof(Page), "messageScript", "$(panelGroups).css({'display':'block'});", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "messageScriptAlert", "jAlert('El producto no tiene contraindicaciones por grupos Farmacológicos');", true);
            ScriptManager.RegisterStartupScript(this, typeof(Page), "messageScript", "$(panelGroups).css({'display':'block'});", true);
        }


    }
    #endregion

    #region Pharma Group Private Methods

    private void getPharmaGroupCatalog()
    {
        string groupToSearch = "";
        if (txtGruops.Text.Trim().Length > 3)
        {
            groupToSearch = "%" + txtGruops.Text + "%";
        }
        else
        {
            groupToSearch = string.IsNullOrEmpty(this.txtGruops.Text.Trim()) ? "%" : this.txtGruops.Text.Trim() + "%";
        }
        List<MedinetBusinessEntries.PharmacologicalGroupsInfo> listCatalog = MedinetBusinessLogicComponent.CatalogsBLC.Instance.getPharmaGroupsContraindicationsWithOutAttach(groupToSearch, this._productId, this._pharmaFormId, this._divisionId, this._categoryId);
        
        this.grdGroups.DataSource = listCatalog;
        this.grdGroups.DataBind();
    }

    private void getPharmaGroupByProduct()
    {
        this.grdProductGroups.DataSource = MedinetBusinessLogicComponent.CatalogsBLC.Instance.getProductPharmaGroupContraindications(this._productId, this._pharmaFormId, this._divisionId, this._categoryId);
        this.grdProductGroups.DataBind();
        checkContraindications();
        if (grdProductGroups.Rows.Count > 0)
            btnDeleteAllGro.Visible = true;
        else
            btnDeleteAllGro.Visible = false;
    }

    private void prepareGridPharmaGroup(short pagesize)
    {
        if (pagesize > 0)
        {
            this.grdGroups.AllowPaging = true;
            this.grdGroups.PageSize = pagesize;
            this.grdGroups.PageIndex = this.Session["CurrentPage"] == null ? 0 : Convert.ToInt32(this.Session["CurrentPage"]);
        }
        else
            this.grdGroups.AllowPaging = false;
    }

    private void cleanWorkGroupGro()
    {
        BtnListSubstancesGro.DataSource = null;
        BtnListSubstancesGro.Visible = false;
        lblAdvertGro.Visible = false;
        lblItemGro.Visible = false;
        btnCancelGro.Visible = false;
        btnContinuarGro.Visible = false;
    }

    private void setWorkGroupGro()
    {
        BtnListSubstancesGro.Visible = true;
        lblAdvertGro.Visible = true;
        lblItemGro.Visible = true;
        btnCancelGro.Visible = true;
    }
    #endregion

    #region Substances Events
    protected void btnSubstances_Click(object sender, EventArgs e)
    {
        this.prepareGridSubstances(Convert.ToInt16(this.ddlPageSizeSubs.SelectedValue));
        this.getSubstancesCatalog();
        this.txtSubs.Focus();
        ScriptManager.RegisterStartupScript(this, typeof(Page), "messageScript", "$(panelSubs).css({'display':'block'});", true);
    }
    protected void ddlSubstancesPageSize_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.Session["CurrentPage"] = 0;
        this.prepareGridSubstances(Convert.ToInt16(this.ddlPageSizeSubs.SelectedValue));
        this.getSubstancesCatalog();
        ScriptManager.RegisterStartupScript(this, typeof(Page), "messageScript", "$(panelSubs).css({'display':'block'});", true);
    }
    protected void grdSubstances_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            MedinetBusinessEntries.ActiveSubstanceInfo currentRow = (MedinetBusinessEntries.ActiveSubstanceInfo)e.Row.DataItem;
            ImageButton btnAddSubs = (ImageButton)e.Row.FindControl("btnAddSubs");
            btnAddSubs.Attributes.Add("ActiveSubstanceId", currentRow.ActiveSubstanceId.ToString());
            btnAddSubs.Attributes.Add("ActiveSubstanceName", currentRow.Description.ToString());
        }
    }
    protected void grdSubstances_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        this.grdSubs.PageIndex = e.NewPageIndex;
        this.Session["CurrentPage"] = Convert.ToInt32(e.NewPageIndex);
        this.getSubstancesCatalog();
        ScriptManager.RegisterStartupScript(this, typeof(Page), "messageScript", "$(panelSubs).css({'display':'block'});", true);
    }
    protected void btnAddSubstances_OnClick(object sender, EventArgs e)
    {
        ImageButton btnAddSubs = (ImageButton)sender;
        this.Session["contraindicationId"] = Convert.ToInt32(btnAddSubs.Attributes["ActiveSubstanceId"]);
        this._contraindication = Convert.ToInt32(btnAddSubs.Attributes["ActiveSubstanceId"]);
        this.Session["itemCatalog"] = btnAddSubs.Attributes["ActiveSubstanceName"];
        this._itemCatalog = btnAddSubs.Attributes["ActiveSubstanceName"];
        List<MedinetBusinessEntries.ActiveSubstanceInfo> data = MedinetBusinessLogicComponent.CatalogsBLC.Instance.getSubstancesByProduct(this._productId);

        foreach (MedinetBusinessEntries.ProductSubstanceContraindicationInfo item in MedinetBusinessLogicComponent.ProductSubstanceContraindicationBLC.Instance.getContraindicationSubstances(this._productId, this._categoryId, this._divisionId, this._pharmaFormId, this._contraindication))
        {
            var sust = (from c in data
                        where c.ActiveSubstanceId == item.ActiveSubstanceId
                        select c);
            if (sust.Count() > 0)
            {
                data.Remove(sust.Single());
            }
        }
        if (data.Count > 1)
        {
            cleanWorkGroup();
            setWorkGroup();
            btnContinuarAcs.Visible = true;
            lblAdvertAcs.Text = "El actual producto contiene más de una sustancia sin Contraindicación, marque cuál(es) están contraindicadas con : ";
            lblItemAcs.Text = this._itemCatalog;
            BtnListSubstancesAcs.DataSource = data;
            BtnListSubstancesAcs.DataValueField = "ActiveSubstanceId";
            BtnListSubstancesAcs.DataTextField = "Description";
            BtnListSubstancesAcs.DataBind();
        }
        else
        {
            if (data.Count <= 0)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "messageScript", "$(panelSubs).css({'display':'block'});", true);
                ScriptManager.RegisterStartupScript(this, typeof(Page), "messageScripts", "jAlert('Todas las sustancias del producto ya tienen Contraindicación con la sustancia: " + this._itemCatalog + "', 'Contraindicaciones');", true);
            }
            else
            {
                if (MedinetBusinessLogicComponent.IppaProductContraindicationsBLC.Instance.checkProductContraindications(this._categoryId, this._pharmaFormId, this._productId, this._divisionId, data[0].ActiveSubstanceId))
                {
                    MedinetBusinessEntries.ProductSubstanceContraindicationInfo pscInfo = new MedinetBusinessEntries.ProductSubstanceContraindicationInfo();
                    pscInfo.ActiveSubstanceId = data[0].ActiveSubstanceId;
                    pscInfo.CategoryId = this._categoryId;
                    pscInfo.DivisionId = this._divisionId;
                    pscInfo.PharmaFormId = this._pharmaFormId;
                    pscInfo.ProductId = this._productId;
                    pscInfo.SubstanceContraindicationId = this._contraindication;
                    if (MedinetBusinessLogicComponent.ProductSubstanceContraindicationBLC.Instance.insertContraindication(pscInfo) == 0)
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "messageScript", "jAlert('La sustancia que ha seleccionado ya esta asociada a las contraindicaciones del producto', 'Contraindicaciones');", true);
                    }
                    this.getSubstancesByProduct();
                    this.getSubstancesCatalog();
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "messageScript", "$(panelSubs).css({'display':'block'});", true);
                }
                cleanWorkGroup();
            }
        }
        ScriptManager.RegisterStartupScript(this, typeof(Page), "messageScript", "$(panelSubs).css({'display':'block'});", true);
    }
    protected void btnDeleteSubstances_OnClick(object sender, EventArgs e)
    {

        int activeSubstanceId,ContraActiveSubstanceId;
        ImageButton btnDelSubs = (ImageButton)sender;

        activeSubstanceId = Convert.ToInt32(btnDelSubs.Attributes["ActiveSubstanceId"]);
        ContraActiveSubstanceId = Convert.ToInt32(btnDelSubs.Attributes["SubstanceContraindicationId"]);
        
        MedinetBusinessEntries.ProductSubstanceContraindicationInfo ProdSub = new MedinetBusinessEntries.ProductSubstanceContraindicationInfo();
        ProdSub.SubstanceContraindicationId = ContraActiveSubstanceId;
        ProdSub.ActiveSubstanceId = activeSubstanceId;
        ProdSub.CategoryId = this._categoryId;
        ProdSub.DivisionId = this._divisionId;
        ProdSub.PharmaFormId = this._pharmaFormId;
        ProdSub.ProductId = this._productId;
        MedinetBusinessLogicComponent.ProductSubstanceContraindicationBLC.Instance.deleteProductSubstanceContraindication(ProdSub);
        this.getSubstancesByProduct();
        this.getSubstancesCatalog();
        ScriptManager.RegisterStartupScript(this, typeof(Page), "messageScript", "$(panelSubs).css({'display':'block'});", true);

    }
    protected void grdProductSubstance_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            MedinetBusinessEntries.ProductSubstanceContraindicationInfo currentRow = (MedinetBusinessEntries.ProductSubstanceContraindicationInfo)e.Row.DataItem;
            ImageButton btnDelSub = (ImageButton)e.Row.FindControl("btnDelSubs");
            btnDelSub.Attributes.Add("ActiveSubstanceId", currentRow.ActiveSubstanceId.ToString());
            btnDelSub.Attributes.Add("SubstanceContraindicationId", currentRow.SubstanceContraindicationId.ToString());
            btnDelSub.Attributes.Add("onClick", "javascript:return confirmDelete('" + currentRow.SubstanceContraindication + "')");
        }
    }
    protected void btnContinuarAcs_Click(object sender, EventArgs e)
    {
        this.btnContinuarAcs.Visible = false;
        this.BtnListSubstancesAcs.Visible = false;
        this.lblAdvertAcs.Visible = false;
        this.lblItemAcs.Visible = false;
        this.btnCancelAcs.Visible = false;
        bool selects = false;
        for (int i = 0; i < BtnListSubstancesAcs.Items.Count; i++)
        {
            if (BtnListSubstancesAcs.Items[i].Selected)
            {
                selects = true;
                int actsubsId = int.Parse(BtnListSubstancesAcs.Items[i].Value);
                if (MedinetBusinessLogicComponent.IppaProductContraindicationsBLC.Instance.checkProductContraindications(this._categoryId, this._pharmaFormId, this._productId, this._divisionId, actsubsId))
                {
                    MedinetBusinessEntries.ProductSubstanceContraindicationInfo pscInfo = new MedinetBusinessEntries.ProductSubstanceContraindicationInfo();
                    pscInfo.ActiveSubstanceId = actsubsId;
                    pscInfo.CategoryId = this._categoryId;
                    pscInfo.DivisionId = this._divisionId;
                    pscInfo.PharmaFormId = this._pharmaFormId;
                    pscInfo.ProductId = this._productId;
                    pscInfo.SubstanceContraindicationId = this._contraindication;
                    if (MedinetBusinessLogicComponent.ProductSubstanceContraindicationBLC.Instance.insertContraindication(pscInfo) == 0)
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "messageScript", "jAlert('Ya existe una asociacion de " + BtnListSubstancesAcs.Items[i].Text + " a las contraindicaciones del producto', 'Contraindicaciones');", true);
                    }
                }
            }
        }
        if (selects)
        {
            cleanWorkGroup();
            this.getSubstancesByProduct();
            this.getSubstancesCatalog();
        }
        else
        {
            this.btnContinuarAcs.Visible = true;
            setWorkGroup();
            ScriptManager.RegisterStartupScript(this, typeof(Page), "messageScriptSelect", "jAlert('Debe seleccionar al menos una sustancia del producto para clasificar', 'Contraindicaciones');", true);
        }
       
        ScriptManager.RegisterStartupScript(this, typeof(Page), "messageScript", "$(panelSubs).css({'display':'block'});", true);
    }
    protected void btnCancelAcs_Click(object sender, EventArgs e)
    {
        cleanWorkGroup();
        ScriptManager.RegisterStartupScript(this, typeof(Page), "messageScript", "$(panelSubs).css({'display':'block'});", true);
    }
    protected void btnDeleteAllAcs_Click(object sender, EventArgs e)
    {
        if (grdProductSubst.Rows.Count > 0)
        {
            MedinetBusinessEntries.ProductSubstanceContraindicationInfo ProdCon = new MedinetBusinessEntries.ProductSubstanceContraindicationInfo();
            ProdCon.CategoryId = this._categoryId;
            ProdCon.DivisionId = this._divisionId;
            ProdCon.PharmaFormId = this._pharmaFormId;
            ProdCon.ProductId = this._productId;
            MedinetBusinessLogicComponent.ProductSubstanceContraindicationBLC.Instance.deleteProductSubstanceContraindicationAll(ProdCon);
            this.getSubstancesByProduct();
            this.getSubstancesCatalog();
            
            ScriptManager.RegisterStartupScript(this, typeof(Page), "messageScript", "$(panelSubs).css({'display':'block'});", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "messageScriptAlert", "jAlert('El producto no tiene contraindicaciones por sustancias activas');", true);
            ScriptManager.RegisterStartupScript(this, typeof(Page), "messageScript", "$(panelSubs).css({'display':'block'});", true);
        }


    }
    #endregion

    #region Substances Private Methods

    private void getSubstancesCatalog()
    {
        string substToSearch = "";
        substToSearch = string.IsNullOrEmpty(this.txtSubs.Text.Trim()) ? null : this.txtSubs.Text.Trim();
        List<MedinetBusinessEntries.ActiveSubstanceInfo> listCatalog = MedinetBusinessLogicComponent.CatalogsBLC.Instance.getSubstancesContraindicationsWithOutAttach(substToSearch, this._productId, this._pharmaFormId, this._divisionId, this._categoryId);
        
        this.grdSubs.DataSource = listCatalog;
        this.grdSubs.DataBind();
    }

    private void getSubstancesByProduct()
    {
        this.grdProductSubst.DataSource = MedinetBusinessLogicComponent.CatalogsBLC.Instance.getProductSubstancesContraindications(this._productId, this._pharmaFormId, this._divisionId, this._categoryId);
        this.grdProductSubst.DataBind();
        checkContraindications();
        if (grdProductSubst.Rows.Count > 0)
            btnDeleteAllAcs.Visible = true;
        else
            btnDeleteAllAcs.Visible = false;
    }

    private void prepareGridSubstances(short pagesize)
    {
        if (pagesize > 0)
        {
            this.grdSubs.AllowPaging = true;
            this.grdSubs.PageSize = pagesize;
            this.grdSubs.PageIndex = this.Session["CurrentPage"] == null ? 0 : Convert.ToInt32(this.Session["CurrentPage"]);
        }
        else
            this.grdSubs.AllowPaging = false;
    }

    private void cleanWorkGroup()
    {
        BtnListSubstancesAcs.DataSource = null;
        BtnListSubstancesAcs.Visible = false;
        lblAdvertAcs.Visible = false;
        lblItemAcs.Visible = false;
        btnCancelAcs.Visible = false;
        btnContinuarAcs.Visible = false;
    }

    private void setWorkGroup()
    {
        BtnListSubstancesAcs.Visible = true;
        lblAdvertAcs.Visible = true;
        lblItemAcs.Visible = true;
        btnCancelAcs.Visible = true;

    }

    #endregion

    #region Others Events
    protected void btnOther_Click(object sender, EventArgs e)
    {
        this.prepareGridOther(Convert.ToInt16(this.ddlPageSizeOther.SelectedValue));
        this.getOtherCatalog();
        this.txtOther.Focus();
        ScriptManager.RegisterStartupScript(this, typeof(Page), "messageScript", "$(panelOther).css({'display':'block'});", true);
    }
    protected void ddlOtherPageSize_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.Session["CurrentPage"] = 0;
        this.prepareGridOther(Convert.ToInt16(this.ddlPageSizeOther.SelectedValue));
        this.getOtherCatalog();
        ScriptManager.RegisterStartupScript(this, typeof(Page), "messageScript", "$(panelOther).css({'display':'block'});", true);
    }
    protected void grdOther_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            MedinetBusinessEntries.OtherElementsInfo currentRow = (MedinetBusinessEntries.OtherElementsInfo)e.Row.DataItem;
            ImageButton btnAddOther = (ImageButton)e.Row.FindControl("btnAddOther");
            btnAddOther.Attributes.Add("ElementId", currentRow.ElementId.ToString());
            btnAddOther.Attributes.Add("ElementName", currentRow.ElementName.ToString());
        }
    }
    protected void grdOther_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        this.grdOther.PageIndex = e.NewPageIndex;
        this.Session["CurrentPage"] = Convert.ToInt32(e.NewPageIndex);
        this.getOtherCatalog();
        ScriptManager.RegisterStartupScript(this, typeof(Page), "messageScript", "$(panelOther).css({'display':'block'});", true);
    }
    protected void btnAddOther_OnClick(object sender, EventArgs e)
    {
        ImageButton btnAddOther = (ImageButton)sender;

        this.Session["contraindicationId"] = Convert.ToInt32(btnAddOther.Attributes["ElementId"]);
        this._contraindication = Convert.ToInt32(btnAddOther.Attributes["ElementId"]);
        this.Session["itemCatalog"] = btnAddOther.Attributes["ElementName"];
        this._itemCatalog = btnAddOther.Attributes["ElementName"];

        List<MedinetBusinessEntries.ActiveSubstanceInfo> data = MedinetBusinessLogicComponent.CatalogsBLC.Instance.getSubstancesByProduct(this._productId);

        foreach (MedinetBusinessEntries.ProductOtherContraindicationInfo item in MedinetBusinessLogicComponent.ProductOtherContraindicationsBLC.Instance.getOtherContraindications(this._productId, this._categoryId, this._divisionId, this._pharmaFormId, this._contraindication))
        {
            var sust = (from c in data
                        where c.ActiveSubstanceId == item.ActiveSubstanceId
                        select c);
            if (sust.Count() > 0)
            {
                data.Remove(sust.Single());
            }
        }
        if (data.Count > 1)
        {
            cleanWorkGroupOth();
            setWorkGroupOth();
            btnContinuarOth.Visible = true;
            lblAdvertOth.Text = "El actual producto contiene más de una sustancia sin Contraindicación, marque cuál(es) están contraindicadas con : ";
            lblItemOth.Text = this._itemCatalog;
            BtnListSubstancesOth.DataSource = data;
            BtnListSubstancesOth.DataValueField = "ActiveSubstanceId";
            BtnListSubstancesOth.DataTextField = "Description";
            BtnListSubstancesOth.DataBind();
        }
        else
        {
            if (data.Count <= 0)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "messageScript", "$(panelOther).css({'display':'block'});", true);
                ScriptManager.RegisterStartupScript(this, typeof(Page), "messageScripts", "jAlert('Todas las sustancias del producto ya tienen Contraindicación con la sustancia: " + this._itemCatalog + "', 'Contraindicaciones');", true);
            }
            else
            {
                if (MedinetBusinessLogicComponent.IppaProductContraindicationsBLC.Instance.checkProductContraindications(this._categoryId, this._pharmaFormId, this._productId, this._divisionId, data[0].ActiveSubstanceId))
                {
                    MedinetBusinessEntries.ProductOtherContraindicationInfo ProdOth = new MedinetBusinessEntries.ProductOtherContraindicationInfo();
                    ProdOth.ActiveSubstanceId = data[0].ActiveSubstanceId;
                    ProdOth.CategoryId = this._categoryId;
                    ProdOth.DivisionId = this._divisionId;
                    ProdOth.PharmaFormId = this._pharmaFormId;
                    ProdOth.ProductId = this._productId;
                    ProdOth.ElementId = this._contraindication;
                    if (MedinetBusinessLogicComponent.ProductOtherContraindicationsBLC.Instance.addProductOtherContraintication(ProdOth) == 0)
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "messageScript", "jAlert('La sustancia que ha seleccionado ya esta asociada a las contraindicaciones del producto', 'Contraindicaciones');", true);
                    }
                    this.getOtherByProduct();
                    this.getOtherCatalog();
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "messageScript", "$(panelOther).css({'display':'block'});", true);
                }
                cleanWorkGroupOth();
            }
        }
        ScriptManager.RegisterStartupScript(this, typeof(Page), "messageScript", "$(panelOther).css({'display':'block'});", true);

    }
    protected void btnDeleteOther_OnClick(object sender, EventArgs e)
    {

        int otherId,activeSubstanceId;
        ImageButton btnDelOther = (ImageButton)sender;

        otherId = Convert.ToInt32(btnDelOther.Attributes["ElementId"]);
        activeSubstanceId = Convert.ToInt32(btnDelOther.Attributes["ActiveSubstanceId"]);
        MedinetBusinessEntries.ProductOtherContraindicationInfo prodOth = new MedinetBusinessEntries.ProductOtherContraindicationInfo();
        prodOth.ElementId = otherId;
        prodOth.CategoryId = this._categoryId;
        prodOth.DivisionId = this._divisionId;
        prodOth.PharmaFormId = this._pharmaFormId;
        prodOth.ProductId = this._productId;
        prodOth.ActiveSubstanceId = activeSubstanceId;
        MedinetBusinessLogicComponent.ProductOtherContraindicationsBLC.Instance.deleteProductOtherContraindication(prodOth);
        this.getOtherByProduct();
        this.getOtherCatalog();
        ScriptManager.RegisterStartupScript(this, typeof(Page), "messageScript", "$(panelOther).css({'display':'block'});", true);

    }
    protected void grdProductOther_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            MedinetBusinessEntries.ProductOtherContraindicationInfo currentRow = (MedinetBusinessEntries.ProductOtherContraindicationInfo)e.Row.DataItem;
            ImageButton btnDelOth = (ImageButton)e.Row.FindControl("btnDelOther");
            btnDelOth.Attributes.Add("ElementId", currentRow.ElementId.ToString());
            btnDelOth.Attributes.Add("ActiveSubstanceId", currentRow.ActiveSubstanceId.ToString());
            btnDelOth.Attributes.Add("onClick", "javascript:return confirmDelete('" + currentRow.ElementName + "')");
        }
    }
    protected void btnContinuarOth_Click(object sender, EventArgs e)
    {
        this.btnContinuarOth.Visible = false;
        this.BtnListSubstancesOth.Visible = false;
        this.lblAdvertOth.Visible = false;
        this.lblItemOth.Visible = false;
        this.btnCancelOth.Visible = false;
        bool selects = false;
        for (int i = 0; i < BtnListSubstancesOth.Items.Count; i++)
        {
            if (BtnListSubstancesOth.Items[i].Selected)
            {
                selects = true;
                int actsubsId = int.Parse(BtnListSubstancesOth.Items[i].Value);
                if (MedinetBusinessLogicComponent.IppaProductContraindicationsBLC.Instance.checkProductContraindications(this._categoryId, this._pharmaFormId, this._productId, this._divisionId, actsubsId))
                {
                    MedinetBusinessEntries.ProductOtherContraindicationInfo pocInfo = new MedinetBusinessEntries.ProductOtherContraindicationInfo();
                    pocInfo.ActiveSubstanceId = actsubsId;
                    pocInfo.CategoryId = this._categoryId;
                    pocInfo.DivisionId = this._divisionId;
                    pocInfo.PharmaFormId = this._pharmaFormId;
                    pocInfo.ProductId = this._productId;
                    pocInfo.ElementId = this._contraindication;
                    if (MedinetBusinessLogicComponent.ProductOtherContraindicationsBLC.Instance.addProductOtherContraintication(pocInfo) == 0)
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "messageScript", "jAlert('Ya existe una asociacion de " + BtnListSubstancesOth.Items[i].Text + " a las contraindicaciones del producto', 'Contraindicaciones');", true);
                    }
                }
            }
        }
        if (selects)
        {
            cleanWorkGroupOth();
            this.getOtherByProduct();
            this.getOtherCatalog();
        }
        else
        {
            this.btnContinuarOth.Visible = true;
            setWorkGroupOth();
            ScriptManager.RegisterStartupScript(this, typeof(Page), "messageScriptSelect", "jAlert('Debe seleccionar al menos una sustancia del producto para clasificar', 'Contraindicaciones');", true);
        }
        
        ScriptManager.RegisterStartupScript(this, typeof(Page), "messageScript", "$(panelOther).css({'display':'block'});", true);
    }
    protected void btnCancelOth_Click(object sender, EventArgs e)
    {
        cleanWorkGroupOth();
        ScriptManager.RegisterStartupScript(this, typeof(Page), "messageScript", "$(panelOther).css({'display':'block'});", true);
    }
    protected void btnDeleteAllOth_Click(object sender, EventArgs e)
    {
        if (grdProductOther.Rows.Count > 0)
        {
            MedinetBusinessEntries.ProductOtherContraindicationInfo ProdCon = new MedinetBusinessEntries.ProductOtherContraindicationInfo();
            ProdCon.CategoryId = this._categoryId;
            ProdCon.DivisionId = this._divisionId;
            ProdCon.PharmaFormId = this._pharmaFormId;
            ProdCon.ProductId = this._productId;
            MedinetBusinessLogicComponent.ProductOtherContraindicationsBLC.Instance.deleteProductOtherContraindicationAll(ProdCon);
            this.getOtherByProduct();
            this.getOtherCatalog();

            ScriptManager.RegisterStartupScript(this, typeof(Page), "messageScript", "$(panelOther).css({'display':'block'});", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "messageScriptAlert", "jAlert('El producto no tiene contraindicaciones por otros elementos');", true);
            ScriptManager.RegisterStartupScript(this, typeof(Page), "messageScript", "$(panelOther).css({'display':'block'});", true);
        }


    }
    #endregion

    #region Other Private Methods

    private void getOtherCatalog()
    {
        string elementToSearch = "";
        if (txtOther.Text.Trim().Length > 3)
        {
            elementToSearch = "%" + txtOther.Text + "%";
        }
        else
        {
            elementToSearch = string.IsNullOrEmpty(this.txtOther.Text.Trim()) ? null : this.txtOther.Text.Trim() + "%";
        }
        List<MedinetBusinessEntries.OtherElementsInfo> listCatalog = MedinetBusinessLogicComponent.CatalogsBLC.Instance.getOtherContraindicationsWithOutAttach(elementToSearch, this._productId, this._pharmaFormId, this._divisionId, this._categoryId);
        
        this.grdOther.DataSource = listCatalog;
        this.grdOther.DataBind();
    }

    private void getOtherByProduct()
    {
        this.grdProductOther.DataSource = MedinetBusinessLogicComponent.CatalogsBLC.Instance.getProductOtherContraindications(this._productId, this._pharmaFormId, this._divisionId, this._categoryId);
        this.grdProductOther.DataBind();
        checkContraindications();
        if (grdProductOther.Rows.Count > 0)
            btnDeleteAllOth.Visible = true;
        else
            btnDeleteAllOth.Visible = false;
    }

    private void prepareGridOther(short pagesize)
    {
        if (pagesize > 0)
        {
            this.grdOther.AllowPaging = true;
            this.grdOther.PageSize = pagesize;
            this.grdOther.PageIndex = this.Session["CurrentPage"] == null ? 0 : Convert.ToInt32(this.Session["CurrentPage"]);
        }
        else
            this.grdOther.AllowPaging = false;
    }

    private void cleanWorkGroupOth()
    {
        BtnListSubstancesOth.DataSource = null;
        BtnListSubstancesOth.Visible = false;
        lblAdvertOth.Visible = false;
        lblItemOth.Visible = false;
        btnCancelOth.Visible = false;
        btnContinuarOth.Visible = false;
    }

    private void setWorkGroupOth()
    {
        BtnListSubstancesOth.Visible = true;
        lblAdvertOth.Visible = true;
        lblItemOth.Visible = true;
        btnCancelOth.Visible = true;
    }
    #endregion

    #region Comments Events

    protected void btnAddComment_OnClick(object sender, EventArgs e)
    {

        
        if (txtComment.Text.Trim().Length > 0)
        {
            this.Session["itemCatalog"] = txtComment.Text;
            this._itemCatalog = txtComment.Text;
            List<MedinetBusinessEntries.ActiveSubstanceInfo> data = MedinetBusinessLogicComponent.CatalogsBLC.Instance.getSubstancesByProduct(this._productId);
            cleanWorkGroupCom();
            if (data.Count > 1)
            {
                cleanWorkGroupCom();
                setWorkGroupCom();
                btnContinuarCom.Visible = true;
                lblAdvertCom.Text = "El actual producto contiene más de una sustancia, marque cuál(es) están contraindicadas con el comentario: ";
                lblItemCom.Text = this._itemCatalog;
                BtnListSubstancesCom.DataSource = data;
                BtnListSubstancesCom.DataValueField = "ActiveSubstanceId";
                BtnListSubstancesCom.DataTextField = "Description";
                BtnListSubstancesCom.DataBind();
            }
            else
            {
                if (data.Count <= 0)
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "messageScript", "$(panelCom).css({'display':'block'});", true);
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "messageScripts", "jAlert('Todas las sustancias del producto ya tienen Contraindicación con la sustancia: " + this._itemCatalog + "', 'Contraindicaciones');", true);
                }
                else
                {
                    if (MedinetBusinessLogicComponent.IppaProductContraindicationsBLC.Instance.checkProductContraindications(this._categoryId, this._pharmaFormId, this._productId, this._divisionId, data[0].ActiveSubstanceId))
                    {
                        MedinetBusinessEntries.ProductCommentContraindicationsInfo ProdOth = new MedinetBusinessEntries.ProductCommentContraindicationsInfo();
                        ProdOth.ActiveSubstanceId = data[0].ActiveSubstanceId;
                        ProdOth.CategoryId = this._categoryId;
                        ProdOth.DivisionId = this._divisionId;
                        ProdOth.PharmaFormId = this._pharmaFormId;
                        ProdOth.ProductId = this._productId;
                        ProdOth.Comments = this.txtComment.Text;
                        if (MedinetBusinessLogicComponent.ProductCommentContraindicationBLC.Instance.addProductCommentContraintication(ProdOth) == 0)
                        {
                            ScriptManager.RegisterStartupScript(this, typeof(Page), "messageScript", "jAlert('La sustancia que ha seleccionado ya esta asociada a las contraindicaciones del producto', 'Contraindicaciones');", true);
                        }
                        txtComment.Text = "";
                        this.getCommentsByProduct();
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "messageScript", "$(panelCom).css({'display':'block'});", true);
                    }
                }
            }
            ScriptManager.RegisterStartupScript(this, typeof(Page), "messageScript", "$(panelCom).css({'display':'block'});", true);
        }
        else {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "messageScript", "jAlert('El comentario no debe ser un texto en blanco','Comentario no valido');", true);
        }
    }
    protected void btnDeleteComment_OnClick(object sender, EventArgs e)
    {

        int ProductCommentId,activeSubstanceId;
        ImageButton btnDelComment = (ImageButton)sender;
        ProductCommentId = Convert.ToInt32(btnDelComment.Attributes["ProductCommentId"]);
        activeSubstanceId = Convert.ToInt32(btnDelComment.Attributes["activeSubstanceId"]);

        MedinetBusinessEntries.ProductCommentContraindicationsInfo ProdCom = new MedinetBusinessEntries.ProductCommentContraindicationsInfo();
        ProdCom.ProductCommentId = ProductCommentId;
        ProdCom.CategoryId = this._categoryId;
        ProdCom.DivisionId = this._divisionId;
        ProdCom.PharmaFormId = this._pharmaFormId;
        ProdCom.ProductId = this._productId;
        ProdCom.ActiveSubstanceId = activeSubstanceId;
        MedinetBusinessLogicComponent.ProductCommentContraindicationBLC.Instance.deleteProductCommentContraindication(ProdCom);
        this.getCommentsByProduct();
        ScriptManager.RegisterStartupScript(this, typeof(Page), "messageScript", "$(panelCom).css({'display':'block'});", true);

    }
    protected void grdProductComment_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            MedinetBusinessEntries.ProductCommentContraindicationsInfo currentRow = (MedinetBusinessEntries.ProductCommentContraindicationsInfo)e.Row.DataItem;
            ImageButton btnDelComment = (ImageButton)e.Row.FindControl("btnDelComment");
            btnDelComment.Attributes.Add("ProductCommentId", currentRow.ProductCommentId.ToString());
            btnDelComment.Attributes.Add("ActiveSubstanceId", currentRow.ActiveSubstanceId.ToString());
            btnDelComment.Attributes.Add("onClick", "javascript:return confirmDelete('" + currentRow.Comments + "')");
        }
    }
    protected void btnContinuarCom_Click(object sender, EventArgs e)
    {
        this.btnContinuarCom.Visible = false;
        this.BtnListSubstancesCom.Visible = false;
        this.lblAdvertCom.Visible = false;
        this.lblItemCom.Visible = false;
        this.btnCancelCom.Visible = false;
        bool selects = false;
        for (int i = 0; i < BtnListSubstancesCom.Items.Count; i++)
        {
            if (BtnListSubstancesCom.Items[i].Selected)
            {
                selects = true;
                int actsubsId = int.Parse(BtnListSubstancesCom.Items[i].Value);
                if (MedinetBusinessLogicComponent.IppaProductContraindicationsBLC.Instance.checkProductContraindications(this._categoryId, this._pharmaFormId, this._productId, this._divisionId, actsubsId))
                {
                    MedinetBusinessEntries.ProductCommentContraindicationsInfo pocInfo = new MedinetBusinessEntries.ProductCommentContraindicationsInfo();
                    pocInfo.ActiveSubstanceId = actsubsId;
                    pocInfo.CategoryId = this._categoryId;
                    pocInfo.DivisionId = this._divisionId;
                    pocInfo.PharmaFormId = this._pharmaFormId;
                    pocInfo.ProductId = this._productId;
                    pocInfo.Comments = this._itemCatalog;
                    if (MedinetBusinessLogicComponent.ProductCommentContraindicationBLC.Instance.addProductCommentContraintication(pocInfo) == 0)
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "messageScript", "jAlert('Ya existe una asociacion de " + BtnListSubstancesOth.Items[i].Text + " a las contraindicaciones del producto', 'Contraindicaciones');", true);
                    }
                }
            }
        }
        if (selects)
        {
            cleanWorkGroupCom();
            this.getCommentsByProduct();
        }
        else
        {
            this.btnContinuarCom.Visible = true;
            setWorkGroupCom();
            ScriptManager.RegisterStartupScript(this, typeof(Page), "messageScriptSelect", "jAlert('Debe seleccionar al menos una sustancia del producto para clasificar', 'Contraindicaciones');", true);
        }
        
        ScriptManager.RegisterStartupScript(this, typeof(Page), "messageScript", "$(panelCom).css({'display':'block'});", true);
    }
    protected void btnCancelCom_Click(object sender, EventArgs e)
    {
        cleanWorkGroupCom();
        ScriptManager.RegisterStartupScript(this, typeof(Page), "messageScript", "$(panelCom).css({'display':'block'});", true);
    }
    protected void btnDeleteAllCom_Click(object sender, EventArgs e)
    {
        if (grdComments.Rows.Count > 0)
        {
            MedinetBusinessEntries.ProductCommentContraindicationsInfo ProdCon = new MedinetBusinessEntries.ProductCommentContraindicationsInfo();
            ProdCon.CategoryId = this._categoryId;
            ProdCon.DivisionId = this._divisionId;
            ProdCon.PharmaFormId = this._pharmaFormId;
            ProdCon.ProductId = this._productId;
            MedinetBusinessLogicComponent.ProductCommentContraindicationBLC.Instance.deleteProductCommentContraindicationAll(ProdCon);
            this.getCommentsByProduct();
            
            ScriptManager.RegisterStartupScript(this, typeof(Page), "messageScript", "$(panelCom).css({'display':'block'});", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "messageScriptAlert", "jAlert('El producto no tiene contraindicaciones por Comentarios');", true);
            ScriptManager.RegisterStartupScript(this, typeof(Page), "messageScript", "$(panelCom).css({'display':'block'});", true);
        }


    }

    #endregion

    #region Comments Private Methods

    private void getCommentsByProduct()
    {
        this.grdComments.DataSource = MedinetBusinessLogicComponent.CatalogsBLC.Instance.getCommentsContraindications(this._productId, this._pharmaFormId, this._divisionId, this._categoryId);
        this.grdComments.DataBind();
        if (grdComments.Rows.Count > 0)
            btnDeleteAllCom.Visible = true;
        else
            btnDeleteAllCom.Visible = false;
    }

    private void cleanWorkGroupCom()
    {
        BtnListSubstancesCom.DataSource = null;
        BtnListSubstancesCom.Visible = false;
        lblAdvertCom.Visible = false;
        lblItemCom.Visible = false;
        btnCancelCom.Visible = false;
        btnContinuarCom.Visible = false;
        txtComment.Text = "";
    }

    private void setWorkGroupCom()
    {
        BtnListSubstancesCom.Visible = true;
        lblAdvertCom.Visible = true;
        lblItemCom.Visible = true;
        btnCancelCom.Visible = true;
        

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
    private string _htmlContent;
    private string _bookShortName;
    private int _contraindication;
    private string _itemCatalog;
   
    

}