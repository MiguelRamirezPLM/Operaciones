using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class activeSubstances : System.Web.UI.Page
{

    #region Page Events

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
            this.getSubstancesByProduct();
            this.getSubstancesWithoutProduct();
            this.getSpinoffsByProduct();
            
        }
    }

    #endregion

    #region Control Events

    protected void grdProductSubstances_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            MedinetBusinessEntries.ActiveSubstanceInfo currentRow = (MedinetBusinessEntries.ActiveSubstanceInfo)e.Row.DataItem;
            ImageButton btnDeleteSubstance = (ImageButton)e.Row.FindControl("btnDeleteSubstance");

            //Button Add Substances to Product
            btnDeleteSubstance.Attributes.Add("activeSubstanceId", currentRow.ActiveSubstanceId.ToString());
            btnDeleteSubstance.Attributes.Add("onClick", "javascript:return confirmDelete('" + currentRow.Description + "')");

            if (MedinetBusinessLogicComponent.ProductContentSubstancesBLC.Instance.getProductContentSubstances(this._productId, currentRow.ActiveSubstanceId).Count() > 0)
                btnDeleteSubstance.OnClientClick = "return confirmDeleteInteracion()";
        }
    }

    protected void grdSubstances_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            MedinetBusinessEntries.ActiveSubstanceInfo currentRow = (MedinetBusinessEntries.ActiveSubstanceInfo)e.Row.DataItem;
            ImageButton btnAddSubstance = (ImageButton)e.Row.FindControl("btnAddSubstance");

            //Button Add Substances to Product
            btnAddSubstance.Attributes.Add("activeSubstanceId", currentRow.ActiveSubstanceId.ToString());
        }
    }

    protected void grdSubstances_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        this.grdSubstances.PageIndex = e.NewPageIndex;
        this.Session["CurrentPage"] = Convert.ToInt32(e.NewPageIndex);
        this.getSubstancesWithoutProduct();
    }

    protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.Session["CurrentPage"] = 0;
        this.prepareGrid(Convert.ToInt16(this.ddlPageSize.SelectedValue));
        this.getSubstancesWithoutProduct();
    }

    protected void btnSubstance_Click(object sender, EventArgs e)
    {
        this.prepareGrid(Convert.ToInt16(this.ddlPageSize.SelectedValue));
        this.getSubstancesWithoutProduct();
        this.txtSubstanceName.Focus();
    }

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

    protected void btnOMSThera_Click(object sender, EventArgs e)
    {
        this.Response.Redirect("therapeuticoms.aspx");
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

    protected void btnDeleteSubstance_OnClick(object sender, EventArgs e)
    {
        int activeSubstanceId;
        ImageButton btnDeleteSubstance = (ImageButton)sender;

        activeSubstanceId = Convert.ToInt32(btnDeleteSubstance.Attributes["activeSubstanceId"]);

        this.removeOtherInteractions(activeSubstanceId);
        this.removeProductInteractions(activeSubstanceId);
        this.removePharmaGroupInteractions(activeSubstanceId);
        this.removeProductContentSubstances(activeSubstanceId);

        if (MedinetBusinessLogicComponent.ProductSubstancesBLC.Instance.checkSubstance(this._productId, activeSubstanceId))
            MedinetBusinessLogicComponent.ProductSubstancesBLC.Instance.removeSubstance(this.getProductSubstance(this._productId, activeSubstanceId), this._userId, this._hashkey);

        this.getSubstancesByProduct();
        this.getSubstancesWithoutProduct();
        
    }

    protected void btnAddSubstance_OnClick(object sender, EventArgs e)
    {
        int activeSubstanceId;
        ImageButton btnAddSubstance = (ImageButton)sender;

        activeSubstanceId = Convert.ToInt32(btnAddSubstance.Attributes["activeSubstanceId"]);

        if (!MedinetBusinessLogicComponent.ProductSubstancesBLC.Instance.checkSubstance(this._productId, activeSubstanceId))
            MedinetBusinessLogicComponent.ProductSubstancesBLC.Instance.addSubstance(this.getProductSubstance(this._productId, activeSubstanceId), this._userId, this._hashkey);

        this.getSubstancesByProduct();
        this.getSubstancesWithoutProduct();
    }

    protected void btnSaveSpinoffs_OnClick(object sender, EventArgs e)
    {
        int spinEditionId;
        CheckBox chkParticipant;

        for (int item = 0; item < this.dlProductSpinoffs.Items.Count; item++)
        {
            spinEditionId = Convert.ToInt32(this.dlProductSpinoffs.DataKeys[item].ToString());
            chkParticipant = (CheckBox)this.dlProductSpinoffs.Items[item].FindControl("chkParticipant");

            if (chkParticipant != null)
            {
                if (chkParticipant.Checked == true)
                {
                    MedinetBusinessEntries.ParticipantProductsInfo participantInfo =
                        MedinetBusinessLogicComponent.ParticipantProductsBLC.Instance.getParticipantProduct(this._productId, spinEditionId, this._pharmaFormId, this._divisionId, this._categoryId);

                    if (participantInfo == null)
                    {
                        participantInfo = new MedinetBusinessEntries.ParticipantProductsInfo();
                        participantInfo.EditionId = spinEditionId;
                        participantInfo.DivisionId = this._divisionId;
                        participantInfo.CategoryId = this._categoryId;
                        participantInfo.ProductId = this._productId;
                        participantInfo.PharmaFormId = this._pharmaFormId;
                        participantInfo.HTMLContent = null;
                        participantInfo.XMLContent = null;
                        participantInfo.Page = null;
                        participantInfo.ModifiedContent = false;
                        MedinetBusinessLogicComponent.ParticipantProductsBLC.Instance.addToBook(participantInfo, this._userId, this._hashkey);
                    }

                    MedinetBusinessEntries.EditionDivisionProductInfo editionDivisionInfo =
                        MedinetBusinessLogicComponent.EditionDivisionProductsBLC.Instance.getDivision(spinEditionId, this._divisionId, this._categoryId, this._productId, this._pharmaFormId);

                    if (editionDivisionInfo == null)
                    {
                        editionDivisionInfo = new MedinetBusinessEntries.EditionDivisionProductInfo();
                        editionDivisionInfo.EditionId = spinEditionId;
                        editionDivisionInfo.DivisionId = this._divisionId;
                        editionDivisionInfo.CategoryId = this._categoryId;
                        editionDivisionInfo.ProductId = this._productId;
                        editionDivisionInfo.PharmaFormId = this._pharmaFormId;
                        MedinetBusinessLogicComponent.EditionDivisionProductsBLC.Instance.addDivision(editionDivisionInfo, this._userId, this._hashkey);
                    }
                }
                else
                {
                    List<MedinetBusinessEntries.ProductContentsInfo> productContents =
                        MedinetBusinessLogicComponent.ProductContentsBLC.Instance.getProductContents(spinEditionId, this._divisionId, this._categoryId, this._productId, this._pharmaFormId);

                    if (productContents.Count > 0)
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "messageScript", "jAlert('El producto tiene contenidos asociados, no se puede eliminar de la obra.', 'Operación Inválida');", true);
                        chkParticipant.Checked = true;
                        return;
                    }

                    MedinetBusinessEntries.EditionDivisionProductInfo editionDivisionInfo =
                        MedinetBusinessLogicComponent.EditionDivisionProductsBLC.Instance.getDivision(spinEditionId, this._divisionId, this._categoryId, this._productId, this._pharmaFormId);

                    if (editionDivisionInfo != null)
                    {
                        editionDivisionInfo = new MedinetBusinessEntries.EditionDivisionProductInfo();
                        editionDivisionInfo.EditionId = spinEditionId;
                        editionDivisionInfo.DivisionId = this._divisionId;
                        editionDivisionInfo.CategoryId = this._categoryId;
                        editionDivisionInfo.ProductId = this._productId;
                        editionDivisionInfo.PharmaFormId = this._pharmaFormId;
                        MedinetBusinessLogicComponent.EditionDivisionProductsBLC.Instance.removeDivision(editionDivisionInfo, this._userId, this._hashkey);
                    }

                    MedinetBusinessEntries.ParticipantProductsInfo participantInfo =
                        MedinetBusinessLogicComponent.ParticipantProductsBLC.Instance.getParticipantProduct(this._productId, spinEditionId, this._pharmaFormId, this._divisionId, this._categoryId);

                    if (participantInfo != null)
                    {
                        participantInfo = new MedinetBusinessEntries.ParticipantProductsInfo();
                        participantInfo.EditionId = spinEditionId;
                        participantInfo.DivisionId = this._divisionId;
                        participantInfo.CategoryId = this._categoryId;
                        participantInfo.ProductId = this._productId;
                        participantInfo.PharmaFormId = this._pharmaFormId;
                        participantInfo.HTMLContent = null;
                        participantInfo.XMLContent = null;
                        participantInfo.Page = null;
                        participantInfo.ModifiedContent = false;
                        MedinetBusinessLogicComponent.ParticipantProductsBLC.Instance.removeToBook(participantInfo, this._userId, this._hashkey);
                    }
                }
            }
        }
        this.getSpinoffsByProduct();
        ScriptManager.RegisterStartupScript(this, typeof(Page), "messageScript", "jAlert('Los Spinoffs se guardaron con éxito.', 'Spinoffs guardados');", true);
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

    private void getSubstancesByProduct()
    {
        
        this.grdProductSubstances.DataSource = MedinetBusinessLogicComponent.CatalogsBLC.Instance.getSubstancesByProduct(this._productId);
        this.grdProductSubstances.DataBind();
    }

    private void getSubstancesWithoutProduct()
    {
        string substanceToSearch = string.IsNullOrEmpty(this.txtSubstanceName.Text.Trim()) ? null : this.txtSubstanceName.Text.Trim() + "%";
        this.grdSubstances.DataSource = MedinetBusinessLogicComponent.CatalogsBLC.Instance.getSubstancesWithoutProduct(this._productId, substanceToSearch);
        this.grdSubstances.DataBind();
    }

    private void prepareGrid(short pagesize)
    {
        if (pagesize > 0)
        {
            this.grdSubstances.AllowPaging = true;
            this.grdSubstances.PageSize = pagesize;
            this.grdSubstances.PageIndex = this.Session["CurrentPage"] == null ? 0 : Convert.ToInt32(this.Session["CurrentPage"]);
        }
        else
            this.grdSubstances.AllowPaging = false;
    }

    private MedinetBusinessEntries.ProductSubstanceInfo getProductSubstance(int productId, int activeSubstanceId)
    {
        MedinetBusinessEntries.ProductSubstanceInfo productSubstance = new MedinetBusinessEntries.ProductSubstanceInfo();
        productSubstance.ProductId = productId;
        productSubstance.ActiveSubstanceId = activeSubstanceId;
        return productSubstance;
    }

    private void removeOtherInteractions(int activeSubstanceId)
    {
        List<MedinetBusinessEntries.ProductOtherInteractionsInfo> otherInteractionList =
            MedinetBusinessLogicComponent.ProductsOtherInteractionsBLC.Instance.getInteractions(this._categoryId, this._pharmaFormId, this._productId, this._divisionId,activeSubstanceId);
        if (otherInteractionList.Count() > 0)
        {
            foreach (MedinetBusinessEntries.ProductOtherInteractionsInfo otherInteractionItem in otherInteractionList)
                MedinetBusinessLogicComponent.ProductsOtherInteractionsBLC.Instance.deleteInteraction(otherInteractionItem, this._userId, this._hashkey);
        }

        //List<MedinetBusinessEntries.OtherInteractionsInfo> otherInteractionList =
        //    MedinetBusinessLogicComponent.OtherInteractionsBLC.Instance.getOtherInteractions(this._productId, activeSubstanceId);

        //if (otherInteractionList.Count() > 0)
        //{
        //    foreach (MedinetBusinessEntries.OtherInteractionsInfo otherInteractionItem in otherInteractionList)
        //        MedinetBusinessLogicComponent.OtherInteractionsBLC.Instance.removeOtherInteraction(otherInteractionItem, this._userId, this._hashkey);
        //}
    }

    private void removeProductInteractions(int activeSubstanceId)
    {
        List<MedinetBusinessEntries.ProductSubstanceInteractionsInfo> productInteractionList =
            MedinetBusinessLogicComponent.ProductSubstanceInteractionsBLC.Instance.getInteractions(this._categoryId, this._pharmaFormId, this._productId, this._divisionId, activeSubstanceId);
        //List<MedinetBusinessEntries.ProductInteractionsInfo> productInteractionList =
        //    MedinetBusinessLogicComponent.ProductInteractionsBLC.Instance.getProductInteractions(this._productId, activeSubstanceId);

        if (productInteractionList.Count() > 0)
        {
            foreach (MedinetBusinessEntries.ProductSubstanceInteractionsInfo productInteractionItem in productInteractionList)
                MedinetBusinessLogicComponent.ProductSubstanceInteractionsBLC.Instance.deleteInteraction(productInteractionItem, this._userId, this._hashkey);
        }
    }

    private void removePharmaGroupInteractions(int activeSubstanceId)
    {
        List<MedinetBusinessEntries.ProductPharmaGroupsInteractionsInfo> pharmaGroupInteractionList =
            MedinetBusinessLogicComponent.ProductPharmaGroupInteractionsBLC.Instance.getInteractions(this._categoryId, this._pharmaFormId, this._productId, this._divisionId, activeSubstanceId);
        
        if (pharmaGroupInteractionList.Count() > 0)
        {
            foreach (MedinetBusinessEntries.ProductPharmaGroupsInteractionsInfo pharmaGroupInteractionItem in pharmaGroupInteractionList)
                MedinetBusinessLogicComponent.ProductPharmaGroupInteractionsBLC.Instance.deleteInteraction(pharmaGroupInteractionItem, this._userId, this._hashkey);
        }
    }

    private void removeProductContentSubstances(int activeSubstanceId)
    {
        List<MedinetBusinessEntries.IppaProductInteractionsInfo> prpductInterac =
            MedinetBusinessLogicComponent.IppaProductInteractionBLC.Instance.getProductInteractions(this._categoryId, this._pharmaFormId, this._productId, this._divisionId, activeSubstanceId);


        if (prpductInterac.Count() > 0)
        {
            foreach (MedinetBusinessEntries.IppaProductInteractionsInfo productContentSubstanceItem in prpductInterac)
                MedinetBusinessLogicComponent.IppaProductInteractionBLC.Instance.deleteProductInteraction(productContentSubstanceItem, this._userId, this._hashkey);
        }
    }

    private void getSpinoffsByProduct()
    {
        DataView dv = new DataView(Utilities.Instance.getSpinoffsByProduct(this._editionId, this._divisionId, this._categoryId, this._productId, this._pharmaFormId));

        if (dv.Count <= 0)
        {
            this.dlProductSpinoffs.Visible = false;
            this.btnSaveSpinoffs.Visible = false;
        }
        else
        {
            this.dlProductSpinoffs.DataSource = dv;
            this.dlProductSpinoffs.DataBind();
            this.dlProductSpinoffs.Visible = true;
            this.btnSaveSpinoffs.Visible = true;
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
    private string _bookShortName;
}