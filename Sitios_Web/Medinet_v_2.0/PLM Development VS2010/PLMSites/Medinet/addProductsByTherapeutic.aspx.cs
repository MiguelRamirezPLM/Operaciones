using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class addProductsByTherapeutic : System.Web.UI.Page
{

    #region Page Events

    protected void Page_Load(object sender, EventArgs e)
    {
        this._editionId = this.Session["editionId"] == null ? -1 : Convert.ToInt32(this.Session["editionId"]);
        this._bookId = this.Session["bookId"] == null ? -1 : Convert.ToInt32(this.Session["bookId"]);
        this._countryId = this.Session["countryId"] == null ? -1 : Convert.ToInt32(this.Session["countryId"]);
        this._userId = this.Session["userId"] == null ? -1 : Convert.ToInt32(this.Session["userId"]);
        this._userCountry = this.Session["userCountry"] == null ? 0 : Convert.ToInt32(this.Session["userCountry"]);
        this._hashkey = System.Configuration.ConfigurationManager.AppSettings["HashKey"];
        
        if (!IsPostBack)
        {
            this.prepareGrid(Convert.ToInt16(this.ddlPageSize.SelectedValue));
            this.getParticipantProducts();

            // Fill the ATC tree:
            this.populateATC(this.ATCTree.Nodes, null);
        }
    }

    #endregion

    #region Control Events

    protected void grdProducts_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        this.grdProducts.PageIndex = e.NewPageIndex;
        this.Session["CurrentPage"] = Convert.ToInt32(e.NewPageIndex);
        this.getParticipantProducts();
    }

    protected void grdProducts_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            MedinetBusinessEntries.ProductsToEditInfo currentRow = (MedinetBusinessEntries.ProductsToEditInfo)e.Row.DataItem;
            ImageButton btnDeleteProductSpinoff = (ImageButton)e.Row.FindControl("btnDeleteProductSpinoff");

            //Button Delete Product to spinoff
            btnDeleteProductSpinoff.Attributes.Add("divisionId", currentRow.DivisionId.ToString());
            btnDeleteProductSpinoff.Attributes.Add("categoryId", currentRow.CategoryId.ToString());
            btnDeleteProductSpinoff.Attributes.Add("productId", currentRow.ProductId.ToString());
            btnDeleteProductSpinoff.Attributes.Add("pharmaFormId", currentRow.PharmaFormId.ToString());
            btnDeleteProductSpinoff.Attributes.Add("onClick", "javascript:return confirmDelete('" + currentRow.Brand + "')");
        }
    }

    protected void ATCTree_TreeNodePopulate(object sender, TreeNodeEventArgs e)
    {
        this.populateATC(e.Node.ChildNodes, Convert.ToInt32(e.Node.Value));
    }

    protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.Session["CurrentPage"] = 0;
        this.prepareGrid(Convert.ToInt16(this.ddlPageSize.SelectedValue));
        this.getParticipantProducts();
    }

    protected void imgBtnBackProducts_Click(object sender, ImageClickEventArgs e)
    {
        this.Response.Redirect("assignedProducts.aspx");
    }

    protected void btnConsult_Click(object sender, EventArgs e)
    {
        this.getParticipantProducts();
        this.txtProductName.Focus();
    }

    protected void btnDeleteProductSpinoff_OnClick(object sender, ImageClickEventArgs e)
    {
        int divisionId, categoryId, productId, pharmaFormId;

        ImageButton btnDeleteProductSpinoff = (ImageButton)sender;

        divisionId = Convert.ToInt32(btnDeleteProductSpinoff.Attributes["divisionId"]);
        categoryId = Convert.ToInt32(btnDeleteProductSpinoff.Attributes["categoryId"]);
        productId = Convert.ToInt32(btnDeleteProductSpinoff.Attributes["productId"]);
        pharmaFormId = Convert.ToInt32(btnDeleteProductSpinoff.Attributes["pharmaFormId"]);

        List<MedinetBusinessEntries.ProductContentsInfo> productContents =
            MedinetBusinessLogicComponent.ProductContentsBLC.Instance.getProductContents(this._editionId, divisionId, categoryId, productId, pharmaFormId);

        if (productContents.Count > 0)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "messageScript", "jAlert('El producto tiene contenidos asociados, no se puede eliminar de la obra.', 'Operación Inválida');", true);
            return;
        }

        //Delete on EditionDivisionProducts
        MedinetBusinessEntries.EditionDivisionProductInfo editionDivisionInfo =
            MedinetBusinessLogicComponent.EditionDivisionProductsBLC.Instance.getDivision(this._editionId, divisionId, categoryId, productId, pharmaFormId);
        if (editionDivisionInfo != null)
            MedinetBusinessLogicComponent.EditionDivisionProductsBLC.Instance.removeDivision(editionDivisionInfo, this._userId, this._hashkey);

        //Delete on ParticipantProducts
        MedinetBusinessEntries.ParticipantProductsInfo participantInfo =
            MedinetBusinessLogicComponent.ParticipantProductsBLC.Instance.getParticipantProduct(productId, this._editionId, pharmaFormId, divisionId, categoryId);
        if (participantInfo != null)
            MedinetBusinessLogicComponent.ParticipantProductsBLC.Instance.removeToBook(participantInfo, this._userId, this._hashkey);

        // Fill the ATC tree:
        this.ATCTree.Nodes.Clear();
        this.populateATC(this.ATCTree.Nodes, null);

        this.getParticipantProducts();
        ScriptManager.RegisterStartupScript(this, typeof(Page), "messageScript", "jAlert('El producto se eliminó del Spinoff.', 'Registro Guardado');", true);
    }

    protected void btnSaveATCProducts_OnClick(object sender, EventArgs e)
    {
        int divisionId, categoryId, productId, pharmaFormId;
        bool affectedRecords = false;

        foreach (TreeNode nodeItem_level0 in this.ATCTree.Nodes)
        {
            foreach (TreeNode nodeItem_level1 in nodeItem_level0.ChildNodes)
            {
                foreach (TreeNode nodeItem_level2 in nodeItem_level1.ChildNodes)
                {
                    foreach (TreeNode nodeItem_level3 in nodeItem_level2.ChildNodes)
                    {
                        foreach (TreeNode nodeItem_FinalLevel in nodeItem_level3.ChildNodes)
                        {
                            if (nodeItem_FinalLevel.Checked)
                            {
                                string[] values = nodeItem_FinalLevel.Value.Split('_');
                                divisionId = Convert.ToInt32(values[0]);
                                categoryId = Convert.ToInt32(values[1]);
                                productId = Convert.ToInt32(values[2]);
                                pharmaFormId = Convert.ToInt32(values[3]);

                                //Insert ParticipantProducts
                                MedinetBusinessEntries.ParticipantProductsInfo participantProduct =
                                    MedinetBusinessLogicComponent.ParticipantProductsBLC.Instance.getParticipantProduct(productId, this._editionId, pharmaFormId, divisionId, categoryId);
                                if (participantProduct == null)
                                {
                                    participantProduct = new MedinetBusinessEntries.ParticipantProductsInfo();
                                    participantProduct.EditionId = this._editionId;
                                    participantProduct.DivisionId = divisionId;
                                    participantProduct.CategoryId = categoryId;
                                    participantProduct.ProductId = productId;
                                    participantProduct.PharmaFormId = pharmaFormId;
                                    participantProduct.HTMLContent = null;
                                    participantProduct.XMLContent = null;
                                    participantProduct.Page = null;
                                    participantProduct.ModifiedContent = null;
                                    MedinetBusinessLogicComponent.ParticipantProductsBLC.Instance.addToBook(participantProduct, this._userId, this._hashkey);
                                }

                                //Insert EditionDivisionProducts
                                MedinetBusinessEntries.EditionDivisionProductInfo editionDivisionProduct =
                                    MedinetBusinessLogicComponent.EditionDivisionProductsBLC.Instance.getDivision(this._editionId, divisionId, categoryId, productId, pharmaFormId);
                                if (editionDivisionProduct == null)
                                {
                                    editionDivisionProduct = new MedinetBusinessEntries.EditionDivisionProductInfo();
                                    editionDivisionProduct.EditionId = this._editionId;
                                    editionDivisionProduct.DivisionId = divisionId;
                                    editionDivisionProduct.CategoryId = categoryId;
                                    editionDivisionProduct.ProductId = productId;
                                    editionDivisionProduct.PharmaFormId = pharmaFormId;
                                    MedinetBusinessLogicComponent.EditionDivisionProductsBLC.Instance.addDivision(editionDivisionProduct, this._userId, this._hashkey);
                                }
                                affectedRecords = true;
                            }
                        }
                        if (nodeItem_level3.Checked)
                        {
                            string[] values = nodeItem_level3.Value.Split('_');
                            divisionId = Convert.ToInt32(values[0]);
                            categoryId = Convert.ToInt32(values[1]);
                            productId = Convert.ToInt32(values[2]);
                            pharmaFormId = Convert.ToInt32(values[3]);

                            //Insert ParticipantProducts
                            MedinetBusinessEntries.ParticipantProductsInfo participantProduct =
                                MedinetBusinessLogicComponent.ParticipantProductsBLC.Instance.getParticipantProduct(productId, this._editionId, pharmaFormId, divisionId, categoryId);
                            if (participantProduct == null)
                            {
                                participantProduct = new MedinetBusinessEntries.ParticipantProductsInfo();
                                participantProduct.EditionId = this._editionId;
                                participantProduct.DivisionId = divisionId;
                                participantProduct.CategoryId = categoryId;
                                participantProduct.ProductId = productId;
                                participantProduct.PharmaFormId = pharmaFormId;
                                participantProduct.HTMLContent = null;
                                participantProduct.XMLContent = null;
                                participantProduct.Page = null;
                                participantProduct.ModifiedContent = null;
                                MedinetBusinessLogicComponent.ParticipantProductsBLC.Instance.addToBook(participantProduct, this._userId, this._hashkey);
                            }

                            //Insert EditionDivisionProducts
                            MedinetBusinessEntries.EditionDivisionProductInfo editionDivisionProduct =
                                MedinetBusinessLogicComponent.EditionDivisionProductsBLC.Instance.getDivision(this._editionId, divisionId, categoryId, productId, pharmaFormId);
                            if (editionDivisionProduct == null)
                            {
                                editionDivisionProduct = new MedinetBusinessEntries.EditionDivisionProductInfo();
                                editionDivisionProduct.EditionId = this._editionId;
                                editionDivisionProduct.DivisionId = divisionId;
                                editionDivisionProduct.CategoryId = categoryId;
                                editionDivisionProduct.ProductId = productId;
                                editionDivisionProduct.PharmaFormId = pharmaFormId;
                                MedinetBusinessLogicComponent.EditionDivisionProductsBLC.Instance.addDivision(editionDivisionProduct, this._userId, this._hashkey);
                            }
                            affectedRecords = true;
                        }
                    }
                }
            }
        }

        // Fill the ATC tree:
        this.ATCTree.Nodes.Clear();
        this.populateATC(this.ATCTree.Nodes, null);
        this.getParticipantProducts();

        if(affectedRecords == true)
            ScriptManager.RegisterStartupScript(this, typeof(Page), "messageScript", "jAlert('Los productos se agregaron al Spinoff.', 'Productos Guardados');", true);
    }

    #endregion

    #region Private Methods

    private void getParticipantProducts()
    {
        string brandToSearch = this.txtProductName.Text.Trim() == "" ? "%" : this.txtProductName.Text.Trim() + "%";
        this.grdProducts.DataSource = MedinetBusinessLogicComponent.ProductsBLC.Instance.getParticipantProductsByEdition(this._countryId, this._editionId, this._bookId, brandToSearch);
        this.grdProducts.DataBind();
    }

    private void prepareGrid(short pagesize)
    {
        if (pagesize > 0)
        {
            this.grdProducts.AllowPaging = true;
            this.grdProducts.PageSize = pagesize;
            this.grdProducts.PageIndex = this.Session["CurrentPage"] == null ? 0 : Convert.ToInt32(this.Session["CurrentPage"]);
        }
        else
            this.grdProducts.AllowPaging = false;
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
                nodes.Add(curNode);
            }
            else
            {
                curNode.PopulateOnDemand = false;
                curNode.SelectAction = TreeNodeSelectAction.None;
                curNode.Expand();

                int parentEditionId = (int)MedinetBusinessLogicComponent.CatalogsBLC.Instance.getEdition(Convert.ToInt32(this.Session["editionId"].ToString())).ParentId;
                DataView dv = new DataView(Utilities.Instance.getProductsByEditionByATC(parentEditionId, (Convert.ToInt32(this.Session["editionId"].ToString())), enuTer.Current.TherapeuticId));
                TreeNode productNode;

                if (dv.Table.Rows.Count > 0)
                {
                    for (int i = 0; i < dv.Table.Rows.Count; i++)
                    {
                        Button btnViewSubstances = new Button();
                        Button btnViewIndications = new Button();
                        Button btnViewIpp = new Button();

                        productNode = new TreeNode();

                        productNode.Value = dv.Table.Rows[i]["DivisionId"].ToString() + "_" 
                            + dv.Table.Rows[i]["CategoryId"].ToString() + "_"
                            + dv.Table.Rows[i]["ProductId"].ToString() + "_" 
                            + dv.Table.Rows[i]["PharmaFormId"].ToString();
                        productNode.Text = dv.Table.Rows[i]["Brand"].ToString() + ", " + dv.Table.Rows[i]["PharmaForm"].ToString() + ", " + dv.Table.Rows[i]["DivisionShortName"].ToString();
                        productNode.PopulateOnDemand = false;
                        productNode.SelectAction = TreeNodeSelectAction.Select;
                        productNode.ShowCheckBox = true;
                        productNode.NavigateUrl = "productDetail.aspx?ed=" + parentEditionId.ToString() +
                            "&di=" + dv.Table.Rows[i]["DivisionId"].ToString() +
                            "&ca=" + dv.Table.Rows[i]["CategoryId"].ToString() +
                            "&pr=" + dv.Table.Rows[i]["ProductId"].ToString() +
                            "&pf=" + dv.Table.Rows[i]["PharmaFormId"].ToString();
                        productNode.Target = "_blank";
                        curNode.ChildNodes.Add(productNode);
                        ATCTree.Nodes.Remove(curNode);
                    }
                    nodes.Add(curNode);
                }
                
                
            }
            
        }
        
    }

    #endregion

    private int _editionId;
    private int _bookId;
    private int _countryId;
    private int _userId;
    private int _userCountry;
    private string _hashkey;

}