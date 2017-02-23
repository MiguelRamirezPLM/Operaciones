using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class therapeutics : System.Web.UI.Page
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
            this.getTherapeuticsByProduct();
            this.getSpinoffsByProduct();
            
            // Fill the ATC tree:
            this.populateATC(this.ATCTree.Nodes, null);
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
  
    
    protected void grdProductTherapeutics_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            MedinetBusinessEntries.TherapeuticInfo currentRow = (MedinetBusinessEntries.TherapeuticInfo)e.Row.DataItem;
            ImageButton btnDeleteTherapeutic = (ImageButton)e.Row.FindControl("btnDeleteTherapeutic");

            //Button Delete Therapeutics to Product
            btnDeleteTherapeutic.Attributes.Add("therapeuticId", currentRow.TherapeuticId.ToString());
            btnDeleteTherapeutic.Attributes.Add("onClick", "javascript:return confirmDelete('" + currentRow.SpanishDescription + "')");
        }
    }

    protected void ATCTree_TreeNodePopulate(object sender, TreeNodeEventArgs e)
    {
        this.populateATC(e.Node.ChildNodes, Convert.ToInt32(e.Node.Value));
    }

    protected void ATCTree_SelectedNodeChanged(object sender, EventArgs e)
    {
        TreeNode selectedNode = this.ATCTree.SelectedNode;
        int therapeuticId = Convert.ToInt32(selectedNode.Value);
        
        if (selectedNode.ChildNodes.Count == 0)
        {
            if(!MedinetBusinessLogicComponent.ProductTherapeuticsBLC.Instance.checkTherapeuticByProduct(therapeuticId, this._productId, this._pharmaFormId))
            {
                // Insert the new association:
                MedinetBusinessEntries.ProductTherapeuticInfo productTherapeuticInfo =
                    new MedinetBusinessEntries.ProductTherapeuticInfo();

                productTherapeuticInfo.TherapeuticId = therapeuticId;
                productTherapeuticInfo.ProductId = this._productId;
                productTherapeuticInfo.PharmaFormId = this._pharmaFormId;

                MedinetBusinessLogicComponent.ProductTherapeuticsBLC.Instance.addTherapeutic(productTherapeuticInfo, this._userId, this._hashkey);
            }
            else
                ScriptManager.RegisterStartupScript(this, typeof(Page), "messageScript", "jAlert('El índice ya se encuentra asignado al producto.', 'Terapéutico asignado');", true);
        }
        selectedNode.Selected = false;
        this.getTherapeuticsByProduct();
    }

    protected void imgBtnBackProducts_Click(object sender, ImageClickEventArgs e)
    {
        this.Session["productId"] = null;
        this.Session["pharmaFormId"] = null;
        this.Response.Redirect("assignedProducts.aspx");
    }

    protected void btnIndicationsIndex_Click(object sender, EventArgs e)
    {
        this.Response.Redirect("indications.aspx");
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

    protected void btnEncyclo_Click(object sender, EventArgs e)
    {
        this.Response.Redirect("encyclopedias.aspx");
    }
    protected void btnSubstancesIndex_Click(object sender, EventArgs e)
    {
        this.Response.Redirect("activeSubstances.aspx");
    }
    protected void btnSymptoms_Click(object sender, EventArgs e)
    {
        this.Response.Redirect("symptoms.aspx");
    }
    protected void btnDeleteTherapeutic_OnClick(object sender, EventArgs e)
    {
        int therapeuticId;
        ImageButton btnDeleteTherapeutic = (ImageButton)sender;

        therapeuticId = Convert.ToInt32(btnDeleteTherapeutic.Attributes["therapeuticId"]);

        if (MedinetBusinessLogicComponent.ProductTherapeuticsBLC.Instance.checkTherapeuticByProduct(therapeuticId, this._productId, this._pharmaFormId))
            MedinetBusinessLogicComponent.ProductTherapeuticsBLC.Instance.removeTherapeutic(this.getProductTherapeutic(therapeuticId, this._productId, this._pharmaFormId), this._userId, this._hashkey);

        this.getTherapeuticsByProduct();
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
        else {
            return false;
        }
    }


    private void getTherapeuticsByProduct()
    {
        this.grdProductTherapeutics.DataSource = MedinetBusinessLogicComponent.CatalogsBLC.Instance.getTherapeuticsByProduct(this._productId, this._pharmaFormId);
        this.grdProductTherapeutics.DataBind();
    }

    private void populateATC(TreeNodeCollection nodes, int? parentId)
    {
        List<MedinetBusinessEntries.TherapeuticInfo> therapeuticInfoList =
            MedinetBusinessLogicComponent.CatalogsBLC.Instance.getTherapeutics(parentId);

        List<MedinetBusinessEntries.TherapeuticInfo>.Enumerator enuTer = therapeuticInfoList.GetEnumerator();

        while (enuTer.MoveNext())
        {
            TreeNode curNode = new TreeNode();
            curNode.Text = enuTer.Current.TherapeuticKey + " - " + enuTer.Current.SpanishDescription;
            curNode.Value = enuTer.Current.TherapeuticId.ToString();

            if (enuTer.Current.TherapeuticSons > 0)
            {
                curNode.PopulateOnDemand = true;
                curNode.SelectAction = TreeNodeSelectAction.Expand;
            }
            else
            {
                curNode.PopulateOnDemand = false;
                curNode.SelectAction = TreeNodeSelectAction.Select;
            }
            nodes.Add(curNode);
        }
    }

    private MedinetBusinessEntries.ProductTherapeuticInfo getProductTherapeutic(int therapeuticId, int productId, int pharmaFormId)
    {
        MedinetBusinessEntries.ProductTherapeuticInfo productTherapeutic = new MedinetBusinessEntries.ProductTherapeuticInfo();
        productTherapeutic.TherapeuticId = therapeuticId;
        productTherapeutic.ProductId = productId;
        productTherapeutic.PharmaFormId = pharmaFormId;
        return productTherapeutic;
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