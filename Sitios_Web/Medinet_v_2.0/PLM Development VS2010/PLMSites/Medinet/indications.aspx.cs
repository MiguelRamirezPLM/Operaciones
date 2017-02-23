using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class indications : System.Web.UI.Page
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
            this.getIndicationsByProduct();
            this.getIndicationsWithoutProduct();
            this.getSpinoffsByProduct();
            
        }
    }

    #endregion

    #region Control Events
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

    protected void btnOMSThera_Click(object sender, EventArgs e)
    {
        this.Response.Redirect("therapeuticoms.aspx");
    }

    protected void grdProductIndications_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            MedinetBusinessEntries.IndicationInfo currentRow = (MedinetBusinessEntries.IndicationInfo)e.Row.DataItem;
            ImageButton btnDeleteIndication = (ImageButton)e.Row.FindControl("btnDeleteIndication");

            //Button Add Substances to Product
            btnDeleteIndication.Attributes.Add("indicationId", currentRow.IndicationId.ToString());
            btnDeleteIndication.Attributes.Add("onClick", "javascript:return confirmDelete('" + currentRow.Description + "')");
        }
    }

    protected void grdIndications_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            MedinetBusinessEntries.IndicationInfo currentRow = (MedinetBusinessEntries.IndicationInfo)e.Row.DataItem;
            ImageButton btnAddIndication = (ImageButton)e.Row.FindControl("btnAddIndication");

            //Button Add Indications to Product
            btnAddIndication.Attributes.Add("indicationId", currentRow.IndicationId.ToString());
        }
    }

    protected void grdIndications_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        this.grdIndications.PageIndex = e.NewPageIndex;
        this.Session["CurrentPage"] = Convert.ToInt32(e.NewPageIndex);
        this.getIndicationsWithoutProduct();
    }

    protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.Session["CurrentPage"] = 0;
        this.prepareGrid(Convert.ToInt16(this.ddlPageSize.SelectedValue));
        this.getIndicationsWithoutProduct();
    }

    protected void btnIndication_Click(object sender, EventArgs e)
    {
        this.prepareGrid(Convert.ToInt16(this.ddlPageSize.SelectedValue));
        this.getIndicationsWithoutProduct();
        this.txtIndicationName.Focus();
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
    protected void btnIcd_Click(object sender, EventArgs e)
    {
        this.Response.Redirect("ICD.aspx");
    }

    
    protected void btnSymptoms_Click(object sender, EventArgs e)
    {
        this.Response.Redirect("symptoms.aspx");
    }

    protected void btnDeleteIndication_OnClick(object sender, EventArgs e)
    {
        int indicationId;
        ImageButton btnDeleteIndication = (ImageButton)sender;

        indicationId = Convert.ToInt32(btnDeleteIndication.Attributes["indicationId"]);

        if (!MedinetBusinessLogicComponent.ProductIndicationsBLC.Instance.check(this.getProductIndication(this._productId, indicationId)))
            MedinetBusinessLogicComponent.ProductIndicationsBLC.Instance.removeIndication(this.getProductIndication(this._productId, indicationId), this._userId, this._hashkey);

        this.getIndicationsByProduct();
        this.getIndicationsWithoutProduct();
    }

    protected void btnAddIndication_OnClick(object sender, EventArgs e)
    {
        int indicationId;
        ImageButton btnAddIndication = (ImageButton)sender;

        indicationId = Convert.ToInt32(btnAddIndication.Attributes["indicationId"]);

        if(MedinetBusinessLogicComponent.ProductIndicationsBLC.Instance.check(this.getProductIndication(this._productId, indicationId)))
            MedinetBusinessLogicComponent.ProductIndicationsBLC.Instance.addIndication(this.getProductIndication(this._productId, indicationId), this._userId, this._hashkey);

        this.getIndicationsByProduct();
        this.getIndicationsWithoutProduct();
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
    protected bool displayParticipate(int participate)
    {
        if (participate == 0)
            return false;
        else
            return true;
    }

    #endregion

    #region Private Methods
    


    private void getIndicationsByProduct()
    {
        this.grdProductIndications.DataSource = MedinetBusinessLogicComponent.CatalogsBLC.Instance.getIndicationsByProduct(this._productId);
        this.grdProductIndications.DataBind();
    }

    private void getIndicationsWithoutProduct()
    {
        string indicationToSearch = string.IsNullOrEmpty(this.txtIndicationName.Text.Trim()) ? null : this.txtIndicationName.Text.Trim() + "%";
        this.grdIndications.DataSource = MedinetBusinessLogicComponent.CatalogsBLC.Instance.getIndicationsWithoutProduct(this._productId, indicationToSearch);
        this.grdIndications.DataBind();
    }

    private void prepareGrid(short pagesize)
    {
        if (pagesize > 0)
        {
            this.grdIndications.AllowPaging = true;
            this.grdIndications.PageSize = pagesize;
            this.grdIndications.PageIndex = this.Session["CurrentPage"] == null ? 0 : Convert.ToInt32(this.Session["CurrentPage"]);
        }
        else
            this.grdIndications.AllowPaging = false;
    }

    private MedinetBusinessEntries.ProductIndicationInfo getProductIndication(int productId, int indicationId)
    {
        MedinetBusinessEntries.ProductIndicationInfo productIndication = new MedinetBusinessEntries.ProductIndicationInfo();
        productIndication.ProductId = productId;
        productIndication.IndicationId = indicationId;
        return productIndication;
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