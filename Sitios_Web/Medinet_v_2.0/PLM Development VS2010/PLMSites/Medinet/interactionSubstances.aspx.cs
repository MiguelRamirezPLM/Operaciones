using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class interactionSubstances : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this._editionId = this.Session["editionId"] == null ? -1 : Convert.ToInt32(this.Session["editionId"]);
        this._divisionId = this.Session["divisionId"] == null ? -1 : Convert.ToInt32(this.Session["divisionId"]);
        this._categoryId = this.Session["categoryId"] == null ? -1 : Convert.ToInt32(this.Session["categoryId"]);
        this._productId = this.Session["productId"] == null ? -1 : Convert.ToInt32(this.Session["productId"]);
        this._pharmaFormId = this.Session["pharmaFormId"] == null ? -1 : Convert.ToInt32(this.Session["pharmaFormId"]);
        this._userId = this.Session["userId"] == null ? -1 : Convert.ToInt32(this.Session["userId"]);
        this._interactionId= this.Session["interactionId"] == null ? -1 : Convert.ToInt32(this.Session["interactionId"]);
        this._brand = this.Session["brand"] == null ? "" : this.Session["brand"].ToString();
        this._pharmaForm = this.Session["pharmaForm"] == null ? "" : this.Session["pharmaForm"].ToString();
        this._itemCatalog = this.Session["itemCatalog"] == null ? "" : this.Session["itemCatalog"].ToString();
        this._bookShortName = this.Session["bookShortName"] == null ? "" : this.Session["bookShortName"].ToString();
        this._hashkey = System.Configuration.ConfigurationManager.AppSettings["HashKey"];

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
            this.setStatusIMChanges();
            this.lblBrand.Text = this._brand;
            this.lblPharmaForm.Text = this._pharmaForm;
           
            this.prepareGrid(Convert.ToInt16(this.ddlPageSize.SelectedValue));
            this.prepareGridPharmaGroup(Convert.ToInt16(this.ddlPharmaGroup.SelectedValue));
            this.prepareGridOthers(Convert.ToInt16(this.ddlOtherPages.SelectedValue));

            this.getSubstancesByProduct();
            this.getPharmacologicalByProduct();
            this.getOthersElementsByProduct();

            this.getSubstancesWithoutProduct();
            this.getPharmaGroupWithoutProductInteractions();
            this.getOtherElementsWithoutProduct();

            this.getProductSubstances();
            this.setHTMLContraindications();
        }
    }

    #region Events

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        cleanWorkGroup();
    }

    protected void imgBtnBackProducts_Click(object sender, ImageClickEventArgs e)
    {
        this.Response.Redirect("activeSubstances.aspx");

    }

    protected void btnEncyclo_Click(object sender, EventArgs e)
    {
        this.Response.Redirect("encyclopedias.aspx");
    }
    protected void btnTherapeuticsIndex_Click(object sender, EventArgs e)
    {
        this.Response.Redirect("therapeutics.aspx");
    }
    protected void btnContraindications_Click(object sender, EventArgs e)
    {
        List<MedinetBusinessEntries.ActiveSubstanceInfo> substances = MedinetBusinessLogicComponent.CatalogsBLC.Instance.getSubstancesByProduct(this._productId);
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

    #region EventsSubstances

    protected void grdProductSubstances_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            MedinetBusinessEntries.ProductSubstanceInteractionsInfo currentRow = (MedinetBusinessEntries.ProductSubstanceInteractionsInfo)e.Row.DataItem;
            ImageButton btnDeleteSubstance = (ImageButton)e.Row.FindControl("btnDeleteSubstance");

            //Button Add Substances to Product
            btnDeleteSubstance.Attributes.Add("activeSubstanceId", currentRow.ActiveSubstanceId.ToString());
            btnDeleteSubstance.Attributes.Add("SubstanceInteraId", currentRow.SubstanceInteractId.ToString());
            btnDeleteSubstance.Attributes.Add("onClick", "javascript:return confirmDelete('" + currentRow.SubstanceInteraction + "')");
            
        }
    }
    
    protected void grdSubstances_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        this.grdSubstances.PageIndex = e.NewPageIndex;
        this.Session["CurrentPage"] = Convert.ToInt32(e.NewPageIndex);
        this.getSubstancesWithoutProduct();
    }
   
    protected void grdSubstances_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            MedinetBusinessEntries.ActiveSubstanceInfo currentRow = (MedinetBusinessEntries.ActiveSubstanceInfo)e.Row.DataItem;
            ImageButton btnAddSubstance = (ImageButton)e.Row.FindControl("btnAddSubstance");

            //Button Add Substances to Product
            btnAddSubstance.Attributes.Add("activeSubstanceId", currentRow.ActiveSubstanceId.ToString());
            btnAddSubstance.Attributes.Add("activeSubstance", currentRow.Description);

        }
    }
   
    protected void btnAddSubstance_click(object sender, EventArgs e)
    {

        ImageButton btnAddSubstance = (ImageButton)sender;
        this.Session["interactionId"] = Convert.ToInt32(btnAddSubstance.Attributes["activeSubstanceId"]);
        this._interactionId = Convert.ToInt32(btnAddSubstance.Attributes["activeSubstanceId"]);
        this.Session["itemCatalog"] = btnAddSubstance.Attributes["activeSubstance"];
        this._itemCatalog = btnAddSubstance.Attributes["activeSubstance"];
        List<MedinetBusinessEntries.ActiveSubstanceInfo> data = MedinetBusinessLogicComponent.CatalogsBLC.Instance.getSubstancesByProduct(this._productId);

        foreach (MedinetBusinessEntries.ProductSubstanceInteractionsInfo item in MedinetBusinessLogicComponent.ProductSubstanceInteractionsBLC.Instance.getInteractionsSubstances(this._categoryId, this._pharmaFormId, this._productId, this._divisionId, this._interactionId))
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
            btnContinuar.Visible = true;
            lblAdvert.Text = "El actual producto contiene mas de una sustancia sin interacción, marque cuál(es) hacen interaccion con la sustancia: ";
            lblItem.Text = this._itemCatalog;
            BtnListSubstances.DataSource = data;
            BtnListSubstances.DataValueField = "ActiveSubstanceId";
            BtnListSubstances.DataTextField = "Description";
            BtnListSubstances.DataBind();
        }
        else
        {
            if (data.Count <= 0)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "messageScript", "jAlert('Todas las sustancias del producto ya tienen interacción con la sustancia: " + this._itemCatalog + "', 'Interacciones');", true);
            }
            else
            {
                if (MedinetBusinessLogicComponent.IppaProductInteractionBLC.Instance.checkProductInteractions(this._categoryId, this._pharmaFormId, this._productId, this._divisionId, data[0].ActiveSubstanceId))
                {
                    MedinetBusinessEntries.ProductSubstanceInteractionsInfo psiInfo = new MedinetBusinessEntries.ProductSubstanceInteractionsInfo();
                    psiInfo.ActiveSubstanceId = data[0].ActiveSubstanceId;
                    psiInfo.CategoryId = this._categoryId;
                    psiInfo.DivisionId = this._divisionId;
                    psiInfo.PharmaFormId = this._pharmaFormId;
                    psiInfo.ProductId = this._productId;
                    psiInfo.SubstanceInteractId = this._interactionId;
                    if (MedinetBusinessLogicComponent.ProductSubstanceInteractionsBLC.Instance.insertInteraction(psiInfo, this._userId, this._hashkey) == 0)
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "messageScript", "jAlert('La sustancia que ha seleccionado ya esta asociada a las interacciones del producto', 'Interacciones');", true);
                    }
                    this.getSubstancesByProduct();
                    this.getSubstancesWithoutProduct();
                }
            }
        }

    }
 
    protected void btnDeleteSubstance_OnClick(object sender, EventArgs e)
    {
        int activeSubstanceId, substanceinteraId;
        ImageButton btnDeleteSubstance = (ImageButton)sender;

        activeSubstanceId = Convert.ToInt32(btnDeleteSubstance.Attributes["activeSubstanceId"]);
        substanceinteraId = Convert.ToInt32(btnDeleteSubstance.Attributes["SubstanceInteraId"]);
        MedinetBusinessEntries.ProductSubstanceInteractionsInfo psiInfo = new MedinetBusinessEntries.ProductSubstanceInteractionsInfo();
        psiInfo.ActiveSubstanceId = activeSubstanceId;
        psiInfo.CategoryId = this._categoryId;
        psiInfo.DivisionId = this._divisionId;
        psiInfo.PharmaFormId = this._pharmaFormId;
        psiInfo.ProductId = this._productId;
        psiInfo.SubstanceInteractId = substanceinteraId;
        MedinetBusinessLogicComponent.ProductSubstanceInteractionsBLC.Instance.deleteInteraction(psiInfo, this._userId, this._hashkey);

        this.getSubstancesByProduct();
        this.getSubstancesWithoutProduct();
    }
  
    protected void btnContinuar_Click(object sender, EventArgs e)
    {
        this.btnContinuar.Visible = false;
        this.BtnListSubstances.Visible = false;
        this.lblAdvert.Visible = false;
        this.lblItem.Visible = false;
        this.btnCancel.Visible = false;
        for (int i = 0; i < BtnListSubstances.Items.Count; i++)
        {
            if (BtnListSubstances.Items[i].Selected)
            {
                int actsubsId = int.Parse(BtnListSubstances.Items[i].Value);
                if (MedinetBusinessLogicComponent.IppaProductInteractionBLC.Instance.checkProductInteractions(this._categoryId, this._pharmaFormId, this._productId, this._divisionId, actsubsId))
                {
                    MedinetBusinessEntries.ProductSubstanceInteractionsInfo psiInfo = new MedinetBusinessEntries.ProductSubstanceInteractionsInfo();
                    psiInfo.ActiveSubstanceId = actsubsId;
                    psiInfo.CategoryId = this._categoryId;
                    psiInfo.DivisionId = this._divisionId;
                    psiInfo.PharmaFormId = this._pharmaFormId;
                    psiInfo.ProductId = this._productId;
                    psiInfo.SubstanceInteractId = this._interactionId;
                    if (MedinetBusinessLogicComponent.ProductSubstanceInteractionsBLC.Instance.insertInteraction(psiInfo, this._userId, this._hashkey) == 0)
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "messageScript", "jAlert('Ya existe una asociacion de " + BtnListSubstances.Items[i].Text + " a las interacciones del producto', 'Interacciones');", true);
                    }
                }
            }
        }
        cleanWorkGroup();
        this.getSubstancesByProduct();
        this.getSubstancesWithoutProduct();
    }
  
    protected void btnFindSubstance_Click(object sender, EventArgs e)
    {
        this.prepareGrid(Convert.ToInt16(this.ddlPageSize.SelectedValue));
        this.getSubstancesWithoutProduct();
        this.txtSubstanceName.Focus();

    }
 
    protected void ddlPageSubstance_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.Session["CurrentPage"] = 0;
        this.prepareGrid(Convert.ToInt16(this.ddlPageSize.SelectedValue));
        this.getSubstancesWithoutProduct();
    }

    #endregion

    #region Events PharmaGroups

    protected void grdProductPharmacol_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            MedinetBusinessEntries.ProductPharmaGroupsInteractionsInfo currentRow = (MedinetBusinessEntries.ProductPharmaGroupsInteractionsInfo)e.Row.DataItem;
            ImageButton btnDeleteSubstance = (ImageButton)e.Row.FindControl("btnDeleteGroup");

            //Button Add Substances to Product
            btnDeleteSubstance.Attributes.Add("activeSubstanceId", currentRow.ActiveSubstanceId.ToString());
            btnDeleteSubstance.Attributes.Add("pharmaGroupId", currentRow.PharmaGroupId.ToString());
            btnDeleteSubstance.Attributes.Add("onClick", "javascript:return confirmDelete('" + currentRow.GroupName + "')");

        }
    }
 
    protected void grdPharmaGroup_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        this.grdPharmaGroup.PageIndex = e.NewPageIndex;
        this.Session["CurrentPagePg"] = Convert.ToInt32(e.NewPageIndex);
        this.getPharmaGroupWithoutProductInteractions();
    }
   
    protected void grdPharmaGroup_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            MedinetBusinessEntries.PharmacologicalGroupsInfo currentRow = (MedinetBusinessEntries.PharmacologicalGroupsInfo)e.Row.DataItem;
            ImageButton btnAddSubstance = (ImageButton)e.Row.FindControl("btnAddGroup");

            //Button Add Substances to Product
            btnAddSubstance.Attributes.Add("pharmaGroupId", currentRow.PharmaGroupId.ToString());
            btnAddSubstance.Attributes.Add("pharmaGroup", currentRow.GroupName);
        }
    }

    protected void btnAddGroup_click(object sender, EventArgs e)
    {

        ImageButton btnAddSubstance = (ImageButton)sender;
        this.Session["interactionId"] = Convert.ToInt32(btnAddSubstance.Attributes["pharmaGroupId"]);
        this._interactionId = Convert.ToInt32(btnAddSubstance.Attributes["pharmaGroupId"]);
        this.Session["itemCatalog"] = btnAddSubstance.Attributes["pharmaGroup"];
        this._itemCatalog = btnAddSubstance.Attributes["pharmaGroup"];
        List<MedinetBusinessEntries.ActiveSubstanceInfo> data = MedinetBusinessLogicComponent.CatalogsBLC.Instance.getSubstancesByProduct(this._productId);

        foreach (MedinetBusinessEntries.ProductPharmaGroupsInteractionsInfo item in MedinetBusinessLogicComponent.ProductPharmaGroupInteractionsBLC.Instance.getInteractionsGroups(this._categoryId, this._pharmaFormId, this._productId, this._divisionId, this._interactionId, 1))
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
            btnContinuarGroup.Visible = true;
            lblAdvert.Text = "El actual producto contiene más de una sustancia sin interaccion, marque cuál(es) hacen interacción con el grupo farmacológico: ";
            lblItem.Text = this._itemCatalog;
            BtnListSubstances.DataSource = data;
            BtnListSubstances.DataValueField = "ActiveSubstanceId";
            BtnListSubstances.DataTextField = "Description";
            BtnListSubstances.DataBind();
        }
        else
        {
            if (data.Count <= 0)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "messageScript", "jAlert('Todas las sustancias del producto ya tienen interacción con el grupo: " + this._itemCatalog + "', 'Interacciones');", true);
            }
            else
            {

                if (MedinetBusinessLogicComponent.IppaProductInteractionBLC.Instance.checkProductInteractions(this._categoryId, this._pharmaFormId, this._productId, this._divisionId, data[0].ActiveSubstanceId))
                {
                    MedinetBusinessEntries.ProductPharmaGroupsInteractionsInfo psiInfo = new MedinetBusinessEntries.ProductPharmaGroupsInteractionsInfo();
                    psiInfo.ActiveSubstanceId = data[0].ActiveSubstanceId;
                    psiInfo.CategoryId = this._categoryId;
                    psiInfo.DivisionId = this._divisionId;
                    psiInfo.PharmaFormId = this._pharmaFormId;
                    psiInfo.ProductId = this._productId;
                    psiInfo.PharmaGroupId = this._interactionId;
                    if (MedinetBusinessLogicComponent.ProductPharmaGroupInteractionsBLC.Instance.insertInteraction(psiInfo, this._userId, this._hashkey) == 0)
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "messageScript", "jAlert('El grupo farmacologico que ha seleccionado, ya esta asociado a las interacciones del producto', 'Interacciones');", true);
                    }
                    this.getPharmacologicalByProduct();
                    this.getPharmaGroupWithoutProductInteractions();

                }
            }
        }

    }
   
    protected void btnContinuarGroup_Click(object sender, EventArgs e)
    {

        for (int i = 0; i < BtnListSubstances.Items.Count; i++)
        {
            if (BtnListSubstances.Items[i].Selected)
            {
                int actsubsId = int.Parse(BtnListSubstances.Items[i].Value);
                if (MedinetBusinessLogicComponent.IppaProductInteractionBLC.Instance.checkProductInteractions(this._categoryId, this._pharmaFormId, this._productId, this._divisionId, actsubsId))
                {
                    MedinetBusinessEntries.ProductPharmaGroupsInteractionsInfo psiInfo = new MedinetBusinessEntries.ProductPharmaGroupsInteractionsInfo();
                    psiInfo.ActiveSubstanceId = actsubsId;
                    psiInfo.CategoryId = this._categoryId;
                    psiInfo.DivisionId = this._divisionId;
                    psiInfo.PharmaFormId = this._pharmaFormId;
                    psiInfo.ProductId = this._productId;
                    psiInfo.PharmaGroupId = this._interactionId;
                    if (MedinetBusinessLogicComponent.ProductPharmaGroupInteractionsBLC.Instance.insertInteraction(psiInfo, this._userId, this._hashkey) == 0)
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "messageScript", "jAlert('El grupo farmacologico que ha seleccionado, ya esta asociado a las interacciones del producto', 'Interacciones');", true);
                    }
                }
            }
        }
        cleanWorkGroup();
        this.getPharmacologicalByProduct();
        this.getPharmaGroupWithoutProductInteractions();
    }
   
    protected void btnDeleteGroup_OnClick(object sender, EventArgs e)
    {
        int activeSubstanceId, pharmaGroupinteraId;
        ImageButton btnDeleteGroup = (ImageButton)sender;

        activeSubstanceId = Convert.ToInt32(btnDeleteGroup.Attributes["activeSubstanceId"]);
        pharmaGroupinteraId = Convert.ToInt32(btnDeleteGroup.Attributes["pharmaGroupId"]);
        MedinetBusinessEntries.ProductPharmaGroupsInteractionsInfo psiInfo = new MedinetBusinessEntries.ProductPharmaGroupsInteractionsInfo();
        psiInfo.ActiveSubstanceId = activeSubstanceId;
        psiInfo.CategoryId = this._categoryId;
        psiInfo.DivisionId = this._divisionId;
        psiInfo.PharmaFormId = this._pharmaFormId;
        psiInfo.ProductId = this._productId;
        psiInfo.PharmaGroupId = pharmaGroupinteraId;
        MedinetBusinessLogicComponent.ProductPharmaGroupInteractionsBLC.Instance.deleteInteraction(psiInfo, this._userId, this._hashkey);

        this.getPharmacologicalByProduct();
        this.getPharmaGroupWithoutProductInteractions();
    }
 
    protected void btnFindGroup_Click(object sender, EventArgs e)
    {
        this.prepareGridPharmaGroup(Convert.ToInt16(this.ddlPharmaGroup.SelectedValue));
        this.getPharmaGroupWithoutProductInteractions();
        txtPharmaGroup.Focus();

    }
  
    protected void ddlPageGroup_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.Session["CurrentPagePg"] = 0;
        this.prepareGridPharmaGroup(Convert.ToInt16(this.ddlPharmaGroup.SelectedValue));
        this.getPharmaGroupWithoutProductInteractions();
    }

    #endregion

    #region events OtherElements

    protected void grdProductOthers_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            MedinetBusinessEntries.ProductOtherInteractionsInfo currentRow = (MedinetBusinessEntries.ProductOtherInteractionsInfo)e.Row.DataItem;
            ImageButton btnDeleteSubstance = (ImageButton)e.Row.FindControl("btnDeleteElement");

            //Button Add Substances to Product
            btnDeleteSubstance.Attributes.Add("activeSubstanceId", currentRow.ActiveSubstanceId.ToString());
            btnDeleteSubstance.Attributes.Add("elementId", currentRow.ElementId.ToString());
            btnDeleteSubstance.Attributes.Add("onClick", "javascript:return confirmDelete('" + currentRow.ElementName + "')");

        }
    }
   
    protected void grdOtherElements_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            MedinetBusinessEntries.OtherElementsInfo currentRow = (MedinetBusinessEntries.OtherElementsInfo)e.Row.DataItem;
            ImageButton btnAddSubstance = (ImageButton)e.Row.FindControl("btnAddElement");

            //Button Add Substances to Product
            btnAddSubstance.Attributes.Add("elementId", currentRow.ElementId.ToString());
            btnAddSubstance.Attributes.Add("element", currentRow.ElementName);
        }
    }
    
    protected void grdOtherElements_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        this.grdOtherElements.PageIndex = e.NewPageIndex;
        this.Session["CurrentPageOt"] = Convert.ToInt32(e.NewPageIndex);
        this.getOtherElementsWithoutProduct();
    }
   
    protected void btnAddElement_click(object sender, EventArgs e)
    {

        ImageButton btnAddOther = (ImageButton)sender;
        this.Session["interactionId"] = Convert.ToInt32(btnAddOther.Attributes["elementId"]);
        this._interactionId = Convert.ToInt32(btnAddOther.Attributes["elementId"]);
        this.Session["itemCatalog"] = btnAddOther.Attributes["element"];
        this._itemCatalog = btnAddOther.Attributes["element"];
        List<MedinetBusinessEntries.ActiveSubstanceInfo> data = MedinetBusinessLogicComponent.CatalogsBLC.Instance.getSubstancesByProduct(this._productId);
        foreach (MedinetBusinessEntries.ProductOtherInteractionsInfo item in MedinetBusinessLogicComponent.ProductsOtherInteractionsBLC.Instance.getInteractionsElement(this._categoryId, this._pharmaFormId, this._productId, this._divisionId, this._interactionId, 1))
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
            btnContinuarOther.Visible = true;
            lblAdvert.Text = "El actual producto contiene mas de una sustancia sin interacción, elija cuál(es) de ellas hace interaccion con el elemento: ";
            lblItem.Text = this._itemCatalog;
            BtnListSubstances.DataSource = data;
            BtnListSubstances.DataValueField = "ActiveSubstanceId";
            BtnListSubstances.DataTextField = "Description";
            BtnListSubstances.DataBind();
        }
        else
        {
            if (data.Count <= 0)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "messageScript", "jAlert('Todas las sustancias del producto ya tienen interacción con el elemento: " + this._itemCatalog + "', 'Interacciones');", true);
            }
            else
            {
                if (MedinetBusinessLogicComponent.IppaProductInteractionBLC.Instance.checkProductInteractions(this._categoryId, this._pharmaFormId, this._productId, this._divisionId, data[0].ActiveSubstanceId))
                {
                    MedinetBusinessEntries.ProductOtherInteractionsInfo psiInfo = new MedinetBusinessEntries.ProductOtherInteractionsInfo();
                    psiInfo.ActiveSubstanceId = data[0].ActiveSubstanceId;
                    psiInfo.CategoryId = this._categoryId;
                    psiInfo.DivisionId = this._divisionId;
                    psiInfo.PharmaFormId = this._pharmaFormId;
                    psiInfo.ProductId = this._productId;
                    psiInfo.ElementId = this._interactionId;
                    if (MedinetBusinessLogicComponent.ProductsOtherInteractionsBLC.Instance.insertInteraction(psiInfo, this._userId, this._hashkey) == 0)
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "messageScript", "jAlert('El elemento que ha seleccionado, ya esta asociado a las interacciones del producto', 'Interacciones');", true);
                    }

                    this.getOthersElementsByProduct();
                    this.getOtherElementsWithoutProduct();

                }
            }
        }

    }
 
    protected void btnDeleteElement_OnClick(object sender, EventArgs e)
    {
        int activeSubstanceId, elementinteraId;
        ImageButton btnDeleteElement = (ImageButton)sender;

        activeSubstanceId = Convert.ToInt32(btnDeleteElement.Attributes["activeSubstanceId"]);
        elementinteraId = Convert.ToInt32(btnDeleteElement.Attributes["elementId"]);
        MedinetBusinessEntries.ProductOtherInteractionsInfo psiInfo = new MedinetBusinessEntries.ProductOtherInteractionsInfo();
        psiInfo.ActiveSubstanceId = activeSubstanceId;
        psiInfo.CategoryId = this._categoryId;
        psiInfo.DivisionId = this._divisionId;
        psiInfo.PharmaFormId = this._pharmaFormId;
        psiInfo.ProductId = this._productId;
        psiInfo.ElementId = elementinteraId;
        MedinetBusinessLogicComponent.ProductsOtherInteractionsBLC.Instance.deleteInteraction(psiInfo, this._userId, this._hashkey);

        this.getOthersElementsByProduct();
        this.getOtherElementsWithoutProduct();

    }
    
    protected void btnContinuarOther_Click(object sender, EventArgs e)
    {

        for (int i = 0; i < BtnListSubstances.Items.Count; i++)
        {
            if (BtnListSubstances.Items[i].Selected)
            {
                int actsubsId = int.Parse(BtnListSubstances.Items[i].Value);
                if (MedinetBusinessLogicComponent.IppaProductInteractionBLC.Instance.checkProductInteractions(this._categoryId, this._pharmaFormId, this._productId, this._divisionId, actsubsId))
                {
                    MedinetBusinessEntries.ProductOtherInteractionsInfo psiInfo = new MedinetBusinessEntries.ProductOtherInteractionsInfo();
                    psiInfo.ActiveSubstanceId = actsubsId;
                    psiInfo.CategoryId = this._categoryId;
                    psiInfo.DivisionId = this._divisionId;
                    psiInfo.PharmaFormId = this._pharmaFormId;
                    psiInfo.ProductId = this._productId;
                    psiInfo.ElementId = this._interactionId;
                    if (MedinetBusinessLogicComponent.ProductsOtherInteractionsBLC.Instance.insertInteraction(psiInfo, this._userId, this._hashkey) == 0)
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "messageScript", "jAlert('El elemento que ha seleccionado, ya esta asociado a las interacciones del producto', 'Interacciones');", true);
                    }
                }
            }
        }
        cleanWorkGroup();
        this.getOthersElementsByProduct();
        this.getOtherElementsWithoutProduct();
    }
 
    protected void ddlPageOther_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.Session["CurrentPageOt"] = 0;
        this.prepareGridOthers(Convert.ToInt16(this.ddlOtherPages.SelectedValue));
        this.getOtherElementsWithoutProduct();
    }
 
    protected void btnFindElement_Click(object sender, EventArgs e)
    {
        this.prepareGridOthers(Convert.ToInt16(this.ddlOtherPages.SelectedValue));
        this.getOtherElementsWithoutProduct();
        txtotherName.Focus();
    }

    #endregion
    
    #endregion

    #region Private Methods

    #region private pharmaGroups

    private void prepareGridPharmaGroup(short pagesize)
    {
        if (pagesize > 0)
        {
            this.grdPharmaGroup.AllowPaging = true;
            this.grdPharmaGroup.PageSize = pagesize;
            this.grdPharmaGroup.PageIndex = this.Session["CurrentPagePg"] == null ? 0 : Convert.ToInt32(this.Session["CurrentPagePg"]);
        }
        else
            this.grdPharmaGroup.AllowPaging = false;
    }
    private void getPharmacologicalByProduct()
    {
        this.gdvpharmacol.DataSource = MedinetBusinessLogicComponent.ProductPharmaGroupInteractionsBLC.Instance.getInteractions(this._categoryId, this._pharmaFormId, this._productId, this._divisionId);
        this.gdvpharmacol.DataBind();
    }
    private void getPharmaGroupWithoutProductInteractions()
    {
        string groupToSearch = "";
        if (this.txtPharmaGroup.Text.Trim().Length>3)
        {
            groupToSearch = "%"+this.txtPharmaGroup.Text + "%";
        }
        else
        {
            groupToSearch = string.IsNullOrEmpty(this.txtPharmaGroup.Text.Trim()) ? null : this.txtPharmaGroup.Text.Trim() + "%";
        }
        
        this.grdPharmaGroup.DataSource = MedinetBusinessLogicComponent.CatalogsBLC.Instance.getPharmaGroupWithoutProductInteractions(this._productId, groupToSearch, this._categoryId, this._pharmaFormId, this._divisionId);
        this.grdPharmaGroup.DataBind();
    }
    
    #endregion

    #region private substances

    private void getSubstancesWithoutProduct()
    {
        string substanceToSearch = "";
        if (txtSubstanceName.Text.Trim().Length>3)
        {
            substanceToSearch = "%" + txtSubstanceName.Text + "%";
        }
        else
        {
        substanceToSearch = string.IsNullOrEmpty(this.txtSubstanceName.Text.Trim()) ? null : this.txtSubstanceName.Text.Trim() + "%";
        }
        List<MedinetBusinessEntries.ActiveSubstanceInfo> listCatalog = MedinetBusinessLogicComponent.CatalogsBLC.Instance.getSubstancesWithoutProductInteractions(this._productId, substanceToSearch, this._categoryId, this._pharmaFormId, this._divisionId);
            foreach (MedinetBusinessEntries.ActiveSubstanceInfo item in MedinetBusinessLogicComponent.CatalogsBLC.Instance.getSubstancesByProduct(this._productId))
	        {
                var sust = (from c in listCatalog
                        where c.ActiveSubstanceId == item.ActiveSubstanceId
                         select c);
                if (sust.Count()>0)
                {
                    listCatalog.Remove(sust.Single());       
                }
    	    }
        
        this.grdSubstances.DataSource = listCatalog;
        this.grdSubstances.DataBind();
    }
    private void getSubstancesByProduct()
    {
        this.grdProductSubstances.DataSource = MedinetBusinessLogicComponent.ProductSubstanceInteractionsBLC.Instance.getInteractions(this._categoryId, this._pharmaFormId, this._productId, this._divisionId);
        this.grdProductSubstances.DataBind();
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

    private void getProductSubstances()
    {

        this.grdSubstancesProduct.DataSource = MedinetBusinessLogicComponent.CatalogsBLC.Instance.getSubstancesByProduct(this._productId);
        this.grdSubstancesProduct.DataBind();

    }

    private void setProductNoInteractions() {
        List<MedinetBusinessEntries.ActiveSubstanceInfo> subs = MedinetBusinessLogicComponent.CatalogsBLC.Instance.getSubstancesByProduct(this._productId);
        MedinetBusinessEntries.IppaProductInteractionsInfo inter;
        foreach (MedinetBusinessEntries.ActiveSubstanceInfo item in subs)
        {
            inter = new MedinetBusinessEntries.IppaProductInteractionsInfo();
            inter.CategoryId = this._categoryId;
            inter.DivisionId = this._divisionId;
            inter.PharmaFormId = this._pharmaFormId;
            inter.ProductId = this._productId;
            inter.ActiveSubstanceId = item.ActiveSubstanceId;
            inter.IMStatusId = 4;
            MedinetBusinessLogicComponent.IppaProductInteractionBLC.Instance.addProductInteraction(inter);
        }
    }
    #endregion

    #region private otherElements
    private void getOthersElementsByProduct()
    {
        this.grdProductOthers.DataSource = MedinetBusinessLogicComponent.ProductsOtherInteractionsBLC.Instance.getInteractions(this._categoryId, this._pharmaFormId, this._productId, this._divisionId);
        this.grdProductOthers.DataBind();
    }

    private void getOtherElementsWithoutProduct()
    {
        string elementToSearch = "";
        if (txtotherName.Text.Trim().Length>3)
        {
            elementToSearch = "%" + txtotherName.Text + "%";
        }
        else
        {
        elementToSearch = string.IsNullOrEmpty(this.txtotherName.Text.Trim()) ? null : this.txtotherName.Text.Trim() + "%";
        }
        this.grdOtherElements.DataSource = MedinetBusinessLogicComponent.CatalogsBLC.Instance.getOtherElementsWithoutProductInteractions(this._productId, elementToSearch, this._categoryId, this._pharmaFormId, this._divisionId);
        this.grdOtherElements.DataBind();
    }
   
    private void prepareGridOthers(short pagesize)
    {
        if (pagesize > 0)
        {
            this.grdOtherElements.AllowPaging = true;
            this.grdOtherElements.PageSize = pagesize;
            this.grdOtherElements.PageIndex = this.Session["CurrentPageOt"] == null ? 0 : Convert.ToInt32(this.Session["CurrentPageOt"]);
        }
        else
            this.grdOtherElements.AllowPaging = false;
    }
    #endregion


    private void setHTMLContraindications()
    {
        List<MedinetBusinessEntries.IppaProductInteractionsInfo> prodInter =
            MedinetBusinessLogicComponent.IppaProductInteractionBLC.Instance.getProductInteractions(this._categoryId, this._pharmaFormId, this._productId, this._divisionId);
        this._htmlContent = MedinetBusinessLogicComponent.IppaProductInteractionBLC.Instance.getHtmlContenrProductInteractions(this._categoryId, this._pharmaFormId, this._productId, this._divisionId);
        if (this._htmlContent.Length < 1)
        {
            this._htmlContent = "<p class='normal'>No se cuenta aun con contenido de este producto</p>";
        }
        div.InnerHtml = this._htmlContent;

    }

    private void cleanWorkGroup()
    {
        BtnListSubstances.DataSource = null;
        BtnListSubstances.Visible = false;
        lblAdvert.Visible = false;
        lblItem.Visible = false;
        btnContinuar.Visible = false;
        btnCancel.Visible = false;
        btnContinuarGroup.Visible = false;
        btnContinuarOther.Visible = false;
    }

    private void setWorkGroup()
    {
        BtnListSubstances.Visible = true;
        lblAdvert.Visible = true;
        lblItem.Visible = true;
        btnCancel.Visible = true;

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
    private int _interactionId;
    private string _itemCatalog;
    private string _htmlContent;
    private int _statusIM;
    private string _bookShortName;




    protected void btnNoIM_Click(object sender, EventArgs e)
    {
        this.setProductNoInteractions();
        this.getSubstancesByProduct();
        this.getPharmacologicalByProduct();
        this.getOthersElementsByProduct();
        ScriptManager.RegisterStartupScript(this, typeof(Page), "messageScript", "jAlert('Producto clasificado sin interacciones', 'Interacciones');", true);
    }

    private void setStatusIMChanges()
    {
        MedinetBusinessLogicComponent.IppaProductInteractionBLC.Instance.checkStatusIMProduct(this._categoryId, this._pharmaFormId, this._productId, this._divisionId);
    }
}