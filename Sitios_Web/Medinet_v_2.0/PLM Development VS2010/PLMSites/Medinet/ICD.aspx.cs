using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ICD : System.Web.UI.Page
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
            this.getICDByProduct();
            this.prepareGrid(Convert.ToInt16(this.ddlPageSize.SelectedValue));
            //this.getSpinoffsByProduct();

            // Fill the ATC tree:
            this.populateICD(this.ICDTree.Nodes, null);
            this.grdResults.Visible = false;
            this.BtnShowTree.Visible = false;
            this.ddlPageSize.Visible = false;
            this.lblPage.Visible = false;
        }
}
    #region Control Events

    protected void ddlPage_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.Session["CurrentPage"] = 0;
        this.prepareGrid(Convert.ToInt16(this.ddlPageSize.SelectedValue));
        this.SearchByText();
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

    protected void btnOMSThera_Click(object sender, EventArgs e)
    {
        this.Response.Redirect("therapeuticoms.aspx");
    }

    protected void grdProductICD_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            MedinetBusinessEntries.ICDInfo currentRow = (MedinetBusinessEntries.ICDInfo)e.Row.DataItem;
            ImageButton btnDeleteICD = (ImageButton)e.Row.FindControl("btnDeleteICD");

            //Button Delete Therapeutics to Product
            btnDeleteICD.Attributes.Add("ICDId", currentRow.ICDId.ToString());
            btnDeleteICD.Attributes.Add("onClick", "javascript:return confirmDelete('" + currentRow.SpanishDescription + "')");
        }
    }

    protected void grdResults_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            MedinetBusinessEntries.ICDInfo currentRow = (MedinetBusinessEntries.ICDInfo)e.Row.DataItem;
            ImageButton btnAddICD = (ImageButton)e.Row.FindControl("btnAddICD");

            //Button Delete Therapeutics to Product
            btnAddICD.Attributes.Add("ICDId", currentRow.ICDId.ToString());
        }
    }

    protected void ICDTree_TreeNodePopulate(object sender, TreeNodeEventArgs e)
    {

        this.populateICD(e.Node.ChildNodes, Convert.ToInt32(e.Node.Value));
    }

    protected void btnEncyclo_Click(object sender, EventArgs e)
    {
        this.Response.Redirect("encyclopedias.aspx");
    }
    protected void ICDTree_SelectedNodeChanged(object sender, EventArgs e)
    {
        TreeNode selectedNode = this.ICDTree.SelectedNode;
        int ICDId = Convert.ToInt32(selectedNode.Value);

        if (selectedNode.ChildNodes.Count == 0)
        {
            if (!MedinetBusinessLogicComponent.ProductIcdBLC.Instance.checkICDByProduct(ICDId, this._productId, this._pharmaFormId))
            {
                // Insert the new association:
                MedinetBusinessEntries.ProductICDInfo productICDInfo =
                    new MedinetBusinessEntries.ProductICDInfo();

                productICDInfo.ICDId = ICDId;
                productICDInfo.ProductId = this._productId;
                productICDInfo.PharmaFormId = this._pharmaFormId;
                MedinetBusinessLogicComponent.ProductIcdBLC.Instance.addICD(productICDInfo);
            }
            else
                ScriptManager.RegisterStartupScript(this, typeof(Page), "messageScript", "jAlert('El índice ya se encuentra asignado al producto.', 'CIE-10 asignado');", true);
        }
        selectedNode.Selected = false;
        this.getICDByProduct();
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

    protected void btnTherapeuticsIndex_Click(object sender, EventArgs e)
    {
        this.Response.Redirect("therapeutics.aspx");
    }

    protected void btnSubstancesIndex_Click(object sender, EventArgs e)
    {
        this.Response.Redirect("activeSubstances.aspx");
    }
    protected void btnSymptoms_Click(object sender, EventArgs e)
    {
        this.Response.Redirect("symptoms.aspx");
    }
    protected void btnDeleteICD_OnClick(object sender, EventArgs e)
    {
        int icdId;
        ImageButton btnDeleteICD = (ImageButton)sender;

        icdId = Convert.ToInt32(btnDeleteICD.Attributes["ICDId"]);

        //if (MedinetBusinessLogicComponent.ProductIcdBLC.Instance.checkICDByProduct(icdId, this._productId))
            MedinetBusinessLogicComponent.ProductIcdBLC.Instance.removeICD(this.getProductIcd( icdId, this._productId, this._pharmaFormId));

        this.getICDByProduct();
        if (txtSearch.Text.Length>=3)
        {
            this.getSearchICDByProduct(this._productId, txtSearch.Text, this._pharmaFormId);
        }
        this.ICDTree.Nodes.Clear();
        this.populateICD(this.ICDTree.Nodes, null);
    }

    protected void btnAddICD_OnClick(object sender, EventArgs e)
    {
        int icdId;
        ImageButton btnAddICD = (ImageButton)sender;

        icdId = Convert.ToInt32(btnAddICD.Attributes["ICDId"]);

        //if (MedinetBusinessLogicComponent.ProductIcdBLC.Instance.checkICDByProduct(icdId, this._productId))
            MedinetBusinessLogicComponent.ProductIcdBLC.Instance.addICD(this.getProductIcd(icdId, this._productId, this._pharmaFormId));

        //this.getICDByProduct();
        //this.ICDTree.Nodes.Clear();
        //this.populateICD(this.ICDTree.Nodes, null);
            this.getSearchICDByProduct(this._productId, txtSearch.Text, this._pharmaFormId);
            this.getICDByProduct();
            this.ICDTree.Visible = false;
    }

    //protected void btnSaveSpinoffs_OnClick(object sender, EventArgs e)
    //{
    //    int spinEditionId;
    //    CheckBox chkParticipant;

    //    for (int item = 0; item < this.dlProductSpinoffs.Items.Count; item++)
    //    {
    //        spinEditionId = Convert.ToInt32(this.dlProductSpinoffs.DataKeys[item].ToString());
    //        chkParticipant = (CheckBox)this.dlProductSpinoffs.Items[item].FindControl("chkParticipant");

    //        if (chkParticipant != null)
    //        {
    //            if (chkParticipant.Checked == true)
    //            {
    //                MedinetBusinessEntries.ParticipantProductsInfo participantInfo =
    //                    MedinetBusinessLogicComponent.ParticipantProductsBLC.Instance.getParticipantProduct(this._productId, spinEditionId, this._pharmaFormId, this._divisionId, this._categoryId);

    //                if (participantInfo == null)
    //                {
    //                    participantInfo = new MedinetBusinessEntries.ParticipantProductsInfo();
    //                    participantInfo.EditionId = spinEditionId;
    //                    participantInfo.DivisionId = this._divisionId;
    //                    participantInfo.CategoryId = this._categoryId;
    //                    participantInfo.ProductId = this._productId;
    //                    participantInfo.PharmaFormId = this._pharmaFormId;
    //                    participantInfo.HTMLContent = null;
    //                    participantInfo.XMLContent = null;
    //                    participantInfo.Page = null;
    //                    participantInfo.ModifiedContent = false;
    //                    MedinetBusinessLogicComponent.ParticipantProductsBLC.Instance.addToBook(participantInfo, this._userId, this._hashkey);
    //                }

    //                MedinetBusinessEntries.EditionDivisionProductInfo editionDivisionInfo =
    //                    MedinetBusinessLogicComponent.EditionDivisionProductsBLC.Instance.getDivision(spinEditionId, this._divisionId, this._categoryId, this._productId, this._pharmaFormId);

    //                if (editionDivisionInfo == null)
    //                {
    //                    editionDivisionInfo = new MedinetBusinessEntries.EditionDivisionProductInfo();
    //                    editionDivisionInfo.EditionId = spinEditionId;
    //                    editionDivisionInfo.DivisionId = this._divisionId;
    //                    editionDivisionInfo.CategoryId = this._categoryId;
    //                    editionDivisionInfo.ProductId = this._productId;
    //                    editionDivisionInfo.PharmaFormId = this._pharmaFormId;
    //                    MedinetBusinessLogicComponent.EditionDivisionProductsBLC.Instance.addDivision(editionDivisionInfo, this._userId, this._hashkey);
    //                }
    //            }
    //            else
    //            {
    //                List<MedinetBusinessEntries.ProductContentsInfo> productContents =
    //                    MedinetBusinessLogicComponent.ProductContentsBLC.Instance.getProductContents(spinEditionId, this._divisionId, this._categoryId, this._productId, this._pharmaFormId);

    //                if (productContents.Count > 0)
    //                {
    //                    ScriptManager.RegisterStartupScript(this, typeof(Page), "messageScript", "jAlert('El producto tiene contenidos asociados, no se puede eliminar de la obra.', 'Operación Inválida');", true);
    //                    chkParticipant.Checked = true;
    //                    return;
    //                }

    //                MedinetBusinessEntries.EditionDivisionProductInfo editionDivisionInfo =
    //                    MedinetBusinessLogicComponent.EditionDivisionProductsBLC.Instance.getDivision(spinEditionId, this._divisionId, this._categoryId, this._productId, this._pharmaFormId);

    //                if (editionDivisionInfo != null)
    //                {
    //                    editionDivisionInfo = new MedinetBusinessEntries.EditionDivisionProductInfo();
    //                    editionDivisionInfo.EditionId = spinEditionId;
    //                    editionDivisionInfo.DivisionId = this._divisionId;
    //                    editionDivisionInfo.CategoryId = this._categoryId;
    //                    editionDivisionInfo.ProductId = this._productId;
    //                    editionDivisionInfo.PharmaFormId = this._pharmaFormId;
    //                    MedinetBusinessLogicComponent.EditionDivisionProductsBLC.Instance.removeDivision(editionDivisionInfo, this._userId, this._hashkey);
    //                }

    //                MedinetBusinessEntries.ParticipantProductsInfo participantInfo =
    //                    MedinetBusinessLogicComponent.ParticipantProductsBLC.Instance.getParticipantProduct(this._productId, spinEditionId, this._pharmaFormId, this._divisionId, this._categoryId);

    //                if (participantInfo != null)
    //                {
    //                    participantInfo = new MedinetBusinessEntries.ParticipantProductsInfo();
    //                    participantInfo.EditionId = spinEditionId;
    //                    participantInfo.DivisionId = this._divisionId;
    //                    participantInfo.CategoryId = this._categoryId;
    //                    participantInfo.ProductId = this._productId;
    //                    participantInfo.PharmaFormId = this._pharmaFormId;
    //                    participantInfo.HTMLContent = null;
    //                    participantInfo.XMLContent = null;
    //                    participantInfo.Page = null;
    //                    participantInfo.ModifiedContent = false;
    //                    MedinetBusinessLogicComponent.ParticipantProductsBLC.Instance.removeToBook(participantInfo, this._userId, this._hashkey);
    //                }
    //            }
    //        }
    //    }
    //    this.getSpinoffsByProduct();
    //    ScriptManager.RegisterStartupScript(this, typeof(Page), "messageScript", "jAlert('Los Spinoffs se guardaron con éxito.', 'Spinoffs guardados');", true);
    //}

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


    private void getICDByProduct()
    {

        this.grdProductICD.DataSource = MedinetBusinessLogicComponent.ICDBLC.Instance.getICDByDrugs(this._editionId, this._productId, this._pharmaFormId);
        this.grdProductICD.DataBind();
    }
    private int getSearchICDByProduct(int productId,string search, int pharmaFormId)
    {
        List<MedinetBusinessEntries.ICDInfo> list =MedinetBusinessLogicComponent.ICDBLC.Instance.getICDWithOutProduct(productId, search, pharmaFormId);
        this.grdResults.DataSource = list;
        this.grdResults.DataBind();
        return list.Count;
    }

    private void populateICD(TreeNodeCollection nodes, int? parentId)
    {
        List<MedinetBusinessEntries.ICDInfo> IcdInfoList =
            MedinetBusinessLogicComponent.ICDBLC.Instance.getAllICD(parentId);
        this._editionId = this.Session["editionId"] == null ? -1 : Convert.ToInt32(this.Session["editionId"]);
        this._productId = this.Session["productId"] == null ? -1 : Convert.ToInt32(this.Session["productId"]);
        
        foreach (MedinetBusinessEntries.ICDInfo item in MedinetBusinessLogicComponent.ICDBLC.Instance.getICDByDrugs(this._editionId,this._productId, this._pharmaFormId))
        {
            var icds = (from c in IcdInfoList
                        where c.ICDKey == item.ICDKey && c.ICDSons==0
                        select c);
            if (icds.Count() > 0)
            {
                IcdInfoList.Remove(icds.Single());
            }

            var icds2 = (from c in IcdInfoList
                        where c.ICDKey == item.ICDKey && 0 == MedinetBusinessLogicComponent.ICDBLC.Instance.getDifferenceICD(this._productId,item.ICDId)
                        select c);
            if (icds2.Count() > 0)
            {
                IcdInfoList.Remove(icds2.Single());
            }

        }
        

        List<MedinetBusinessEntries.ICDInfo>.Enumerator enuTer = IcdInfoList.GetEnumerator();

        while (enuTer.MoveNext())
        {
            TreeNode curNode = new TreeNode();
            curNode.Text = enuTer.Current.ICDKey + " - " + enuTer.Current.SpanishDescription;
            curNode.Value = enuTer.Current.ICDId.ToString();
            if (enuTer.Current.ICDKey.Length > 2) {
                curNode.ShowCheckBox = true;
            }
            if (enuTer.Current.ICDSons > 0)
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

    private MedinetBusinessEntries.ProductICDInfo getProductIcd(int icdId, int productId, int pharmaFormId)
    {
        MedinetBusinessEntries.ProductICDInfo productIcd = new MedinetBusinessEntries.ProductICDInfo();
        productIcd.ICDId = icdId;
        productIcd.ProductId = productId;
        productIcd.PharmaFormId = pharmaFormId;
        return productIcd;
    }

    //private void getSpinoffsByProduct()
    //{
    //    DataView dv = new DataView(Utilities.Instance.getSpinoffsByProduct(this._editionId, this._divisionId, this._categoryId, this._productId, this._pharmaFormId));

    //    if (dv.Count <= 0)
    //    {
    //        this.dlProductSpinoffs.Visible = false;
    //        this.btnSaveSpinoffs.Visible = false;
    //    }
    //    else
    //    {
    //        this.dlProductSpinoffs.DataSource = dv;
    //        this.dlProductSpinoffs.DataBind();
    //        this.dlProductSpinoffs.Visible = true;
    //        this.btnSaveSpinoffs.Visible = true;
    //    }
    //}

    protected void btnSaveCieAsociated_OnClick(object sender, EventArgs e)
    {
        List<MedinetBusinessEntries.ProductICDInfo> inserts = new List<MedinetBusinessEntries.ProductICDInfo>();
        List<TreeNode> Remove = new List<TreeNode>();
        foreach (TreeNode nodeItem_level0 in this.ICDTree.Nodes)
        {
            foreach (TreeNode nodeItem_level1 in nodeItem_level0.ChildNodes)
            {
                foreach (TreeNode nodeItem_FinalLevel in nodeItem_level1.ChildNodes)
                        {
                            if (nodeItem_FinalLevel.Checked)
                            {
                                    int ICD = Convert.ToInt32(nodeItem_FinalLevel.Value);
                                    MedinetBusinessEntries.ProductICDInfo productICDInfo = new MedinetBusinessEntries.ProductICDInfo();
                                    productICDInfo.ICDId = ICD;
                                    productICDInfo.ProductId = this._productId;
                                    productICDInfo.PharmaFormId = this._pharmaFormId;
                                    inserts.Add(productICDInfo);
                                    Remove.Add(nodeItem_FinalLevel);
                            }
                        }
                if (nodeItem_level1.Checked)
                {
                        int ICD = Convert.ToInt32(nodeItem_level1.Value);
                        MedinetBusinessEntries.ProductICDInfo productICDInfo = new MedinetBusinessEntries.ProductICDInfo();
                        productICDInfo.ICDId = ICD;
                        productICDInfo.ProductId = this._productId;
                        productICDInfo.PharmaFormId = this._pharmaFormId;
                        inserts.Add(productICDInfo);
                }
                    
            }
        }
        if (inserts.Count > 0)
        {
            MedinetBusinessLogicComponent.ProductIcdBLC.Instance.InsertICD(inserts);

            this.getICDByProduct();
            this.ICDTree.Nodes.Clear();
            this.populateICD(this.ICDTree.Nodes, null);
            ScriptManager.RegisterStartupScript(this, typeof(Page), "messageScript", "jAlert('Los ICE-10 seleccionados se agregaron al Producto.', 'Asociados Correctamente');", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "messageScript", "jAlert('No selecciono ningun elemento para asociar', 'CIE-10');", true);
        }
        
    }

    protected void btnSearch_OnClick(object sender, EventArgs e)
    {
        this.SearchByText();
        
    }
    protected void btnShowTree_OnClick(object sender, EventArgs e)
    {
        this.lblHeadCIE.Text = "Catálogo CIE-10";
        this.ICDTree.Nodes.Clear();
        this.ICDTree.Visible = true;
        this.grdResults.Visible = false;
        this.BtnShowTree.Visible = false;
        this.btnSaveCieAsociated.Visible = true;
        this.lblPage.Visible = false;
        this.ddlPageSize.Visible = false;
        this.txtSearch.Text = "";
        this.populateICD(this.ICDTree.Nodes, null);
        
    }

    protected void grdResults_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        this.grdResults.PageIndex = e.NewPageIndex;
        this.Session["CurrentPage"] = Convert.ToInt32(e.NewPageIndex);
        this.getSearchICDByProduct(this._productId, txtSearch.Text, this._pharmaFormId);
    }

    private void prepareGrid(short pagesize)
    {
        if (pagesize > 0)
        {
            this.grdResults.AllowPaging = true;
            this.grdResults.PageSize = pagesize;
            this.grdResults.PageIndex = this.Session["CurrentPage"] == null ? 0 : Convert.ToInt32(this.Session["CurrentPage"]);
        }
        else
            this.grdResults.AllowPaging = false;
    }

    private void SearchByText() {
        if (txtSearch.Text.Trim().Length >= 3)
        {
            this.ICDTree.Visible = false;
            this.btnSaveCieAsociated.Visible = false;
            this.BtnShowTree.Visible = true;
            this.grdResults.Visible = true;
            this.ddlPageSize.Visible = true;
            this.lblPage.Visible = true;
            if (this.getSearchICDByProduct(this._productId, txtSearch.Text, this._pharmaFormId) > 0)
            {
                this.lblHeadCIE.Text = "Resultados:";
            }
            else
            {
                this.lblHeadCIE.Text = "No se encontraron resultados.";
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "messageScript", "jAlert('El texto de busqueda debe tener al menos 3 caracteres', 'Busqueda');", true);
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