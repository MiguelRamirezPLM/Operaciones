using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class productPresentations : System.Web.UI.Page
{

    #region Page Events

    protected void Page_Load(object sender, EventArgs e)
    {
        this._editionId = this.Session["editionId"] == null ? -1 : Convert.ToInt32(this.Session["editionId"]);
        this._bookId = this.Session["bookId"] == null ? -1 : Convert.ToInt32(this.Session["bookId"]);
        this._countryId = this.Session["countryId"] == null ? -1 : Convert.ToInt32(this.Session["countryId"]);
        this._divisionId = this.Session["divisionId"] == null ? -1 : Convert.ToInt32(this.Session["divisionId"]);
        this._userId = this.Session["userId"] == null ? -1 : Convert.ToInt32(this.Session["userId"]);
        this._userCountry = this.Session["userCountry"] == null ? 0 : Convert.ToInt32(this.Session["userCountry"]);
        this._hashkey = System.Configuration.ConfigurationManager.AppSettings["HashKey"];
        this._categoryId = this.Session["categoryId"] == null ? -1 : Convert.ToInt32(this.Session["categoryId"]);
        this._productId = this.Session["productId"] == null ? -1 : Convert.ToInt32(this.Session["productId"]);
        this._pharmaFormId = this.Session["pharmaFormId"] == null ? -1 : Convert.ToInt32(this.Session["pharmaFormId"]);
        this._brand = this.Session["brand"] == null ? "" : this.Session["brand"].ToString();
        this._pharmaForm = this.Session["pharmaForm"] == null ? "" : this.Session["pharmaForm"].ToString();

        if (!IsPostBack)
        {
            this.lblBrand.Text = this._brand;
            this.lblPharmaForm.Text = this._pharmaForm;
            this.getPresentationCatalog();
            this.getProductPresentations();
            this.getExternalPacks();
            this.getInternalPacks();
            this.getContentUnits();
            this.getWeightUnits();
        }
    }

    #endregion

    #region Control Events

    protected void grdProductPresentations_RowEditing(object sender, GridViewEditEventArgs e)
    {
        this.grdProductPresentations.EditIndex = e.NewEditIndex;
        this.getProductPresentations();
    }

    protected void grdProductPresentations_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        int? qtyExternalPack, externalPackId, qtyInternalPack, internalPackId, contentUnitId, weightUnitId;

        string qtyContentUnit, qtyWeightUnit;

        int presentationId = Convert.ToInt32(this.grdProductPresentations.DataKeys[e.RowIndex].Values[0].ToString());
        int productId = Convert.ToInt32(this.grdProductPresentations.DataKeys[e.RowIndex].Values[1].ToString());
        int pharmaFormId = Convert.ToInt32(this.grdProductPresentations.DataKeys[e.RowIndex].Values[2].ToString());
        int divisionId = Convert.ToInt32(this.grdProductPresentations.DataKeys[e.RowIndex].Values[3].ToString());
        int categoryId = Convert.ToInt32(this.grdProductPresentations.DataKeys[e.RowIndex].Values[4].ToString());
        
        TextBox txtQtyExternalPack = (TextBox)this.grdProductPresentations.Rows[e.RowIndex].FindControl("txtQtyExternalPack");
        TextBox txtQtyInternalPack = (TextBox)this.grdProductPresentations.Rows[e.RowIndex].FindControl("txtQtyInternalPack");
        TextBox txtQtyContentUnit = (TextBox)this.grdProductPresentations.Rows[e.RowIndex].FindControl("txtQtyContentUnit");
        TextBox txtQtyWeightUnit = (TextBox)this.grdProductPresentations.Rows[e.RowIndex].FindControl("txtQtyWeightUnit");

        DropDownList ddlExternalPack = (DropDownList)this.grdProductPresentations.Rows[e.RowIndex].FindControl("ddlExternalPack");
        DropDownList ddlInternalPack = (DropDownList)this.grdProductPresentations.Rows[e.RowIndex].FindControl("ddlInternalPack");
        DropDownList ddlContentUnitName = (DropDownList)this.grdProductPresentations.Rows[e.RowIndex].FindControl("ddlContentUnitName");
        DropDownList ddlWeightUnitName = (DropDownList)this.grdProductPresentations.Rows[e.RowIndex].FindControl("ddlWeightUnitName");

        //Valid QtyExternalPack
        if (txtQtyExternalPack.Text.Trim() != "")
        {
            if (this.validValue(txtQtyExternalPack.Text.Trim()))
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "messageScript", "jAlert('Debe íngresar sólo números.', 'Presentación inválida');", true);
                return;
            }
            else
                qtyExternalPack = Convert.ToInt32(txtQtyExternalPack.Text.Trim());
        }
        else
            qtyExternalPack = -1;

        //Valid QtyInternalPack
        if (txtQtyInternalPack.Text.Trim() != "")
        {
            if (this.validValue(txtQtyInternalPack.Text.Trim()))
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "messageScript", "jAlert('Debe íngresar sólo números.', 'Presentación inválida');", true);
                return;
            }
            else
                qtyInternalPack = Convert.ToInt32(txtQtyInternalPack.Text.Trim());
        }
        else
            qtyInternalPack = -1;

        //Valid QtyContentUnit
        if (txtQtyContentUnit.Text.Trim() != "")
        {
            qtyContentUnit = txtQtyContentUnit.Text.Trim();
        }
        else
            qtyContentUnit = "-1";

        //Valid QtyWeightUnit
        if (txtQtyWeightUnit.Text.Trim() != "")
        {
            qtyWeightUnit = txtQtyWeightUnit.Text.Trim();
        }
        else
            qtyWeightUnit = "-1";
        
        internalPackId = ddlInternalPack.SelectedValue == "-1" ? -1 : Convert.ToInt32(ddlInternalPack.SelectedValue);
        externalPackId = ddlExternalPack.SelectedValue == "-1" ? -1 : Convert.ToInt32(ddlExternalPack.SelectedValue);
        contentUnitId = ddlContentUnitName.SelectedValue == "-1" ? -1 : Convert.ToInt32(ddlContentUnitName.SelectedValue);
        weightUnitId = ddlWeightUnitName.SelectedValue == "-1" ? -1 : Convert.ToInt32(ddlWeightUnitName.SelectedValue);

        if ((qtyExternalPack == -1 && externalPackId == -1) &&
            (qtyInternalPack == -1 && internalPackId == -1) &&
            (qtyContentUnit == "-1" && contentUnitId == -1) &&
            (qtyWeightUnit == "-1" && weightUnitId == -1))
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "messageScript", "jAlert('Debe elegir por lo menos una opción para actualizar la presentación.', 'Presentación inválida');", true);
            return;
        }

        MedinetBusinessEntries.PresentationsInfo presentationProduct = this.getPresentation(divisionId, categoryId, productId, pharmaFormId, qtyExternalPack,
            externalPackId, qtyInternalPack, internalPackId, qtyContentUnit, contentUnitId, qtyWeightUnit, weightUnitId);

        presentationProduct.PresentationId = presentationId;
        presentationProduct.Presentation = null;
        presentationProduct.Active = true;

        MedinetBusinessLogicComponent.PresentationsBLC.Instance.updatePresentationToProduct(presentationProduct, this._userId, this._hashkey);

        this.grdProductPresentations.EditIndex = -1;
        this.getPresentationCatalog();
        this.getProductPresentations();
        ScriptManager.RegisterStartupScript(this, typeof(Page), "messageScript", "jAlert('El registro se actualizó con éxito.', 'Modificación exitosa');", true);
    }

    protected void grdProductPresentations_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ImageButton btnEditPresentations = (ImageButton)e.Row.FindControl("btnEditPresentations");

            MedinetBusinessEntries.PresentationDetailInfo currentRow = (MedinetBusinessEntries.PresentationDetailInfo)e.Row.DataItem;
            ImageButton btnDeletePresentation = (ImageButton)e.Row.FindControl("btnDeletePresentation");

            //Button Delete Presentations to Edition
            btnDeletePresentation.Attributes.Add("presentationId", currentRow.PresentationId.ToString());

            //Button Edit Presentations
            if (MedinetBusinessLogicComponent.EditionPresentationsBLC.Instance.checkEditionPresentation(this._editionId, currentRow.PresentationId) == true)
                btnEditPresentations.Visible = false;

            //Display ExternalPack
            this.displayExternalPackName(e, currentRow.ExternalPackId, currentRow.ExternalPackName);

            //Display InternalPack
            this.displayInternalPackName(e, currentRow.InternalPackId, currentRow.InternalPackName);

            //Display ContentUnit
            this.displayContentUnitName(e, currentRow.ContentUnitId, currentRow.ContentUnitName);

            //Display WeightUnit
            this.displayWeightUnitName(e, currentRow.WeightUnitId, currentRow.WeightUnitName);

        }
    }

    protected void grdPresentationCatalog_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ImageButton btnEditPresentations = (ImageButton)e.Row.FindControl("btnEditPresentations");

            MedinetBusinessEntries.PresentationDetailInfo currentRow = (MedinetBusinessEntries.PresentationDetailInfo)e.Row.DataItem;
            ImageButton btnAddPresentationToEdition = (ImageButton)e.Row.FindControl("btnAddPresentationToEdition");

            //Button Add Presentations to Edition
            btnAddPresentationToEdition.Attributes.Add("presentationId", currentRow.PresentationId.ToString());

            //Button Edit Presentations
            if (MedinetBusinessLogicComponent.EditionPresentationsBLC.Instance.checkEditionPresentation(this._editionId, currentRow.PresentationId) == true)
                btnEditPresentations.Visible = false;

            //Display ExternalPack
            this.displayExternalPackNameProdPres(e, currentRow.ExternalPackId, currentRow.ExternalPackName);

            //Display InternalPack
            this.displayInternalPackNameProdPres(e, currentRow.InternalPackId, currentRow.InternalPackName);

            //Display ContentUnit
            this.displayContentUnitNameProdPres(e, currentRow.ContentUnitId, currentRow.ContentUnitName);

            //Display WeightUnit
            this.displayWeightUnitNameProdPres(e, currentRow.WeightUnitId, currentRow.WeightUnitName);
        }
    }

    protected void imgBtnBackProducts_Click(object sender, ImageClickEventArgs e)
    {
        this.Session.Remove("categoryId");
        this.Session.Remove("productId");
        this.Session.Remove("pharmaFormId");
        this.Session.Remove("brand");
        this.Session.Remove("pharmaForm");
        this.Response.Redirect("classifiedProducts.aspx");
    }

    protected void btnAddNewPresentation_Click(object sender, EventArgs e)
    {
        int presentationId;
        int? qtyExternalPack, externalPackId, qtyInternalPack, internalPackId, contentUnitId, weightUnitId;
        string qtyContentUnit, qtyWeightUnit;

        //Valid QtyExternalPack
        if (this.txtQtyExtPack.Text.Trim() != "")
        {
            if (this.validValue(this.txtQtyExtPack.Text.Trim()))
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "messageScript", "jAlert('Debe íngresar sólo números.', 'Presentación inválida');", true);
                return;
            }
            else
                qtyExternalPack = Convert.ToInt32(this.txtQtyExtPack.Text.Trim());
        }
        else
            qtyExternalPack = -1;

        //Valid QtyInternalPack
        if (this.txtQtyIntPack.Text.Trim() != "")
        {
            if (this.validValue(this.txtQtyIntPack.Text.Trim()))
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "messageScript", "jAlert('Debe íngresar sólo números.', 'Presentación inválida');", true);
                return;
            }
            else
                qtyInternalPack = Convert.ToInt32(this.txtQtyIntPack.Text.Trim());
        }
        else
            qtyInternalPack = -1;

        //Valid QtyContentUnit
        if (this.txtQtyContentUnit.Text.Trim() != "")
        {
            qtyContentUnit = this.txtQtyContentUnit.Text.Trim();
        }
        else
            qtyContentUnit = "-1";

        //Valid QtyWeightUnit
        if (this.txtQtyWeightUnit.Text.Trim() != "")
        {
            qtyWeightUnit = this.txtQtyWeightUnit.Text.Trim();
        }
        else
            qtyWeightUnit = "-1";

        externalPackId = this.ddlExternalPacks.SelectedValue == "-1" ? -1 : Convert.ToInt32(this.ddlExternalPacks.SelectedValue);
        internalPackId = this.ddlInternalPacks.SelectedValue == "-1" ? -1 : Convert.ToInt32(this.ddlInternalPacks.SelectedValue);
        contentUnitId = this.ddlContentUnits.SelectedValue == "-1" ? -1 : Convert.ToInt32(this.ddlContentUnits.SelectedValue);
        weightUnitId = this.ddlWeightUnits.SelectedValue == "-1" ? -1 : Convert.ToInt32(this.ddlWeightUnits.SelectedValue);

        if ((qtyExternalPack == -1 && externalPackId == -1) && 
            (qtyInternalPack == -1 && internalPackId == -1) &&
            (qtyContentUnit == "-1" && contentUnitId == -1) &&
            (qtyWeightUnit == "-1" && weightUnitId == -1))
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "messageScript", "jAlert('Debe elegir por lo menos una opción para la nueva presentación.', 'Presentación inválida');", true);
            return;
        }

        MedinetBusinessEntries.PresentationsInfo presentationProduct = this.getPresentation(this._divisionId, this._categoryId, this._productId, this._pharmaFormId, qtyExternalPack, 
            externalPackId, qtyInternalPack, internalPackId, qtyContentUnit, contentUnitId, qtyWeightUnit, weightUnitId);

        //Add the New Presentation to Data Base
        presentationId = MedinetBusinessLogicComponent.PresentationsBLC.Instance.addPresentationToProduct(presentationProduct, this._userId, this._hashkey);

        //Add the New Presentation to Edition
        MedinetBusinessEntries.EditionPresentationsInfo editionPresentation = new MedinetBusinessEntries.EditionPresentationsInfo();
        editionPresentation.EditionId = this._editionId;
        editionPresentation.PresentationId = presentationId;
        MedinetBusinessLogicComponent.EditionPresentationsBLC.Instance.addEditionPresentation(editionPresentation, this._userId, this._hashkey);

        this.clearProperties();
        this.getPresentationCatalog();
        this.getProductPresentations();
        ScriptManager.RegisterStartupScript(this, typeof(Page), "messageScript", "jAlert('La presentación se agregó con éxito.', 'Presentación guardada');", true);
    }

    protected void btnDeletePresentation_OnClick(object sender, EventArgs e)
    {
        int presentationId;
        ImageButton btnDeletePresentation = (ImageButton)sender;

        presentationId = Convert.ToInt32(btnDeletePresentation.Attributes["presentationId"]);

        MedinetBusinessEntries.EditionPresentationsInfo editionPresentation = new MedinetBusinessEntries.EditionPresentationsInfo();
        editionPresentation.EditionId = this._editionId;
        editionPresentation.PresentationId = presentationId;

        MedinetBusinessLogicComponent.EditionPresentationsBLC.Instance.removeEditionPresentation(editionPresentation, this._userId, this._hashkey);

        this.getPresentationCatalog();
        this.getProductPresentations();
    }

    protected void btnAddPresentationToEdition_OnClick(object sender, EventArgs e)
    {
        int presentationId;
        ImageButton btnAddPresentationToEdition = (ImageButton)sender;

        presentationId = Convert.ToInt32(btnAddPresentationToEdition.Attributes["presentationId"]);

        MedinetBusinessEntries.EditionPresentationsInfo editionPresentation = new MedinetBusinessEntries.EditionPresentationsInfo();
        editionPresentation.EditionId = this._editionId;
        editionPresentation.PresentationId = presentationId;

        MedinetBusinessLogicComponent.EditionPresentationsBLC.Instance.addEditionPresentation(editionPresentation, this._userId, this._hashkey);

        this.getPresentationCatalog();
        this.getProductPresentations();
    }

    #endregion

    #region Private Methods

    private void getPresentationCatalog()
    {
        List<MedinetBusinessEntries.PresentationDetailInfo> presentationCatalog =
            MedinetBusinessLogicComponent.PresentationsBLC.Instance.getPresentationsByProduct(this._editionId, this._divisionId, this._categoryId, this._productId, this._pharmaFormId);

        if (presentationCatalog.Count() > 0)
            this.lblPresentations.Visible = true;
        else
            this.lblPresentations.Visible = false;

        this.grdPresentationCatalog.DataSource = presentationCatalog;
        this.grdPresentationCatalog.DataBind();
    }

    private void getProductPresentations()
    {
        List<MedinetBusinessEntries.PresentationDetailInfo> presentationList =
            MedinetBusinessLogicComponent.PresentationsBLC.Instance.getPresentationsByEditionByProduct(this._editionId, this._divisionId, this._categoryId, this._productId, this._pharmaFormId);

        if (presentationList.Count() > 0)
            this.lblProductPresentations.Visible = true;
        else
            this.lblProductPresentations.Visible = false;

        this.grdProductPresentations.DataSource = presentationList;
        this.grdProductPresentations.DataBind();
    }

    private void getExternalPacks()
    {
        this.ddlExternalPacks.DataSource =
            MedinetBusinessLogicComponent.ExternalPacksBLC.Instance.getExternalPacks();

        this.ddlExternalPacks.DataTextField = "ExternalPackName";
        this.ddlExternalPacks.DataValueField = "ExternalPackId";
        this.ddlExternalPacks.DataBind();

        // Add the initial value:
        ListItem initialLine = new ListItem("SELECCIONAR", "-1", true);
        initialLine.Selected = true;
        this.ddlExternalPacks.Items.Insert(0, initialLine);
    }

    private void getInternalPacks()
    {
        this.ddlInternalPacks.DataSource =
            MedinetBusinessLogicComponent.InternalPacksBLC.Instance.getInternalPacks();

        this.ddlInternalPacks.DataTextField = "InternalPackName";
        this.ddlInternalPacks.DataValueField = "InternalPackId";
        this.ddlInternalPacks.DataBind();

        // Add the initial value:
        ListItem initialLine = new ListItem("SELECCIONAR", "-1", true);
        initialLine.Selected = true;
        this.ddlInternalPacks.Items.Insert(0, initialLine);
    }

    private void getContentUnits()
    {
        this.ddlContentUnits.DataSource =
            MedinetBusinessLogicComponent.ContentUnitsBLC.Instance.getContentUnits();

        this.ddlContentUnits.DataTextField = "UnitName";
        this.ddlContentUnits.DataValueField = "ContentUnitId";
        this.ddlContentUnits.DataBind();

        // Add the initial value:
        ListItem initialLine = new ListItem("SELECCIONAR", "-1", true);
        initialLine.Selected = true;
        this.ddlContentUnits.Items.Insert(0, initialLine);
    }

    private void getWeightUnits()
    {
        this.ddlWeightUnits.DataSource =
            MedinetBusinessLogicComponent.WeightUnitsBLC.Instance.getWeightUnits();

        this.ddlWeightUnits.DataTextField = "ShortName";
        this.ddlWeightUnits.DataValueField = "WeightUnitId";
        this.ddlWeightUnits.DataBind();

        // Add the initial value:
        ListItem initialLine = new ListItem("SELECCIONAR", "-1", true);
        initialLine.Selected = true;
        this.ddlWeightUnits.Items.Insert(0, initialLine);
    }

    private void clearProperties()
    {
        this.ddlExternalPacks.SelectedValue = "-1";
        this.ddlInternalPacks.SelectedValue = "-1";
        this.ddlContentUnits.SelectedValue = "-1";
        this.ddlWeightUnits.SelectedValue = "-1";

        this.txtQtyExtPack.Text = "";
        this.txtQtyIntPack.Text = "";
        this.txtQtyContentUnit.Text = "";
        this.txtQtyWeightUnit.Text = "";
    }

    protected string displayQtyExternalPack(string qtyExternalPack)
    {
        return string.IsNullOrEmpty(qtyExternalPack) ? "" : qtyExternalPack;
    }

    protected void displayExternalPackName(GridViewRowEventArgs e, int? externalPackId, string externalPackName)
    {
        if (e.Row.RowIndex == this.grdProductPresentations.EditIndex)
        {
            ListItem initialLine;

            DropDownList ddlExternalPack = (DropDownList)e.Row.FindControl("ddlExternalPack");

            ddlExternalPack.DataSource = MedinetBusinessLogicComponent.ExternalPacksBLC.Instance.getExternalPacks();
            ddlExternalPack.DataTextField = "ExternalPackName";
            ddlExternalPack.DataValueField = "ExternalPackId";
            ddlExternalPack.DataBind();

            // Add the initial value:
            initialLine = new ListItem("SELECCIONAR", "-1", true);
            initialLine.Selected = true;
            ddlExternalPack.Items.Insert(0, initialLine);

            ddlExternalPack.SelectedValue = externalPackId == null ? "-1" : externalPackId.ToString();
            ddlExternalPack.Enabled = true;
        }
        else
        {
            Label lblExternalPackName = (Label)e.Row.FindControl("lblExternalPackName");
            lblExternalPackName.Text = externalPackName == null ? "" : externalPackName;
        }
    }

    protected void displayExternalPackNameProdPres(GridViewRowEventArgs e, int? externalPackId, string externalPackName)
    {
        if (e.Row.RowIndex == this.grdPresentationCatalog.EditIndex)
        {
            ListItem initialLine;

            DropDownList ddlExternalPack = (DropDownList)e.Row.FindControl("ddlExternalPack");

            ddlExternalPack.DataSource = MedinetBusinessLogicComponent.ExternalPacksBLC.Instance.getExternalPacks();
            ddlExternalPack.DataTextField = "ExternalPackName";
            ddlExternalPack.DataValueField = "ExternalPackId";
            ddlExternalPack.DataBind();

            // Add the initial value:
            initialLine = new ListItem("SELECCIONAR", "-1", true);
            initialLine.Selected = true;
            ddlExternalPack.Items.Insert(0, initialLine);

            ddlExternalPack.SelectedValue = externalPackId == null ? "-1" : externalPackId.ToString();
            ddlExternalPack.Enabled = true;
        }
        else
        {
            Label lblExternalPackName = (Label)e.Row.FindControl("lblExternalPackName");
            lblExternalPackName.Text = externalPackName == null ? "" : externalPackName;
        }
    }

    protected string displayQtyInternalPack(string qtyInternalPack)
    {
        return string.IsNullOrEmpty(qtyInternalPack) ? "" : qtyInternalPack;
    }

    protected void displayInternalPackName(GridViewRowEventArgs e, int? internalPackId, string internalPackName)
    {
        if (e.Row.RowIndex == this.grdProductPresentations.EditIndex)
        {
            ListItem initialLine;

            DropDownList ddlInternalPack = (DropDownList)e.Row.FindControl("ddlInternalPack");

            ddlInternalPack.DataSource = MedinetBusinessLogicComponent.InternalPacksBLC.Instance.getInternalPacks();
            ddlInternalPack.DataTextField = "InternalPackName";
            ddlInternalPack.DataValueField = "InternalPackId";
            ddlInternalPack.DataBind();

            // Add the initial value:
            initialLine = new ListItem("SELECCIONAR", "-1", true);
            initialLine.Selected = true;
            ddlInternalPack.Items.Insert(0, initialLine);

            ddlInternalPack.SelectedValue = internalPackId == null ? "-1" : internalPackId.ToString();
            ddlInternalPack.Enabled = true;
        }
        else
        {
            Label lblInternalPackName = (Label)e.Row.FindControl("lblInternalPackName");
            lblInternalPackName.Text = internalPackName == null ? "" : internalPackName;
        }
    }

    protected void displayInternalPackNameProdPres(GridViewRowEventArgs e, int? internalPackId, string internalPackName)
    {
        if (e.Row.RowIndex == this.grdPresentationCatalog.EditIndex)
        {
            ListItem initialLine;

            DropDownList ddlInternalPack = (DropDownList)e.Row.FindControl("ddlInternalPack");

            ddlInternalPack.DataSource = MedinetBusinessLogicComponent.InternalPacksBLC.Instance.getInternalPacks();
            ddlInternalPack.DataTextField = "InternalPackName";
            ddlInternalPack.DataValueField = "InternalPackId";
            ddlInternalPack.DataBind();

            // Add the initial value:
            initialLine = new ListItem("SELECCIONAR", "-1", true);
            initialLine.Selected = true;
            ddlInternalPack.Items.Insert(0, initialLine);

            ddlInternalPack.SelectedValue = internalPackId == null ? "-1" : internalPackId.ToString();
            ddlInternalPack.Enabled = true;
        }
        else
        {
            Label lblInternalPackName = (Label)e.Row.FindControl("lblInternalPackName");
            lblInternalPackName.Text = internalPackName == null ? "" : internalPackName;
        }
    }

    protected string displayQtyContentUnit(string qtyContentUnit)
    {
        return string.IsNullOrEmpty(qtyContentUnit) ? "" : qtyContentUnit;
    }

    protected void displayContentUnitName(GridViewRowEventArgs e, int? contentUnitId, string contentUnitName)
    {
        if (e.Row.RowIndex == this.grdProductPresentations.EditIndex)
        {
            ListItem initialLine;

            DropDownList ddlContentUnitName = (DropDownList)e.Row.FindControl("ddlContentUnitName");

            ddlContentUnitName.DataSource = MedinetBusinessLogicComponent.ContentUnitsBLC.Instance.getContentUnits();
            ddlContentUnitName.DataTextField = "UnitName";
            ddlContentUnitName.DataValueField = "ContentUnitId";
            ddlContentUnitName.DataBind();

            // Add the initial value:
            initialLine = new ListItem("SELECCIONAR", "-1", true);
            initialLine.Selected = true;
            ddlContentUnitName.Items.Insert(0, initialLine);

            ddlContentUnitName.SelectedValue = contentUnitId == null ? "-1" : contentUnitId.ToString();
            ddlContentUnitName.Enabled = true;
        }
        else
        {
            Label lblContentUnitName = (Label)e.Row.FindControl("lblContentUnitName");
            lblContentUnitName.Text = contentUnitName == null ? "" : contentUnitName;
        }
    }

    protected void displayContentUnitNameProdPres(GridViewRowEventArgs e, int? contentUnitId, string contentUnitName)
    {
        if (e.Row.RowIndex == this.grdPresentationCatalog.EditIndex)
        {
            ListItem initialLine;

            DropDownList ddlContentUnitName = (DropDownList)e.Row.FindControl("ddlContentUnitName");

            ddlContentUnitName.DataSource = MedinetBusinessLogicComponent.ContentUnitsBLC.Instance.getContentUnits();
            ddlContentUnitName.DataTextField = "UnitName";
            ddlContentUnitName.DataValueField = "ContentUnitId";
            ddlContentUnitName.DataBind();

            // Add the initial value:
            initialLine = new ListItem("SELECCIONAR", "-1", true);
            initialLine.Selected = true;
            ddlContentUnitName.Items.Insert(0, initialLine);

            ddlContentUnitName.SelectedValue = contentUnitId == null ? "-1" : contentUnitId.ToString();
            ddlContentUnitName.Enabled = true;
        }
        else
        {
            Label lblContentUnitName = (Label)e.Row.FindControl("lblContentUnitName");
            lblContentUnitName.Text = contentUnitName == null ? "" : contentUnitName;
        }
    }

    protected string displayQtyWeightUnit(string qtyWeightUnit)
    {
        return string.IsNullOrEmpty(qtyWeightUnit) ? "" : qtyWeightUnit;
    }

    protected void displayWeightUnitName(GridViewRowEventArgs e, int? weightUnitId, string weightUnitName)
    {
        if (e.Row.RowIndex == this.grdProductPresentations.EditIndex)
        {
            ListItem initialLine;

            DropDownList ddlWeightUnitName = (DropDownList)e.Row.FindControl("ddlWeightUnitName");

            ddlWeightUnitName.DataSource = MedinetBusinessLogicComponent.WeightUnitsBLC.Instance.getWeightUnits();
            ddlWeightUnitName.DataTextField = "UnitName";
            ddlWeightUnitName.DataValueField = "WeightUnitId";
            ddlWeightUnitName.DataBind();

            // Add the initial value:
            initialLine = new ListItem("SELECCIONAR", "-1", true);
            initialLine.Selected = true;
            ddlWeightUnitName.Items.Insert(0, initialLine);

            ddlWeightUnitName.SelectedValue = weightUnitId == null ? "-1" : weightUnitId.ToString();
            ddlWeightUnitName.Enabled = true;
        }
        else
        {
            Label lblWeightUnitName = (Label)e.Row.FindControl("lblWeightUnitName");
            lblWeightUnitName.Text = weightUnitName == null ? "" : weightUnitName;
        }
    }

    protected void displayWeightUnitNameProdPres(GridViewRowEventArgs e, int? weightUnitId, string weightUnitName)
    {
        if (e.Row.RowIndex == this.grdPresentationCatalog.EditIndex)
        {
            ListItem initialLine;

            DropDownList ddlWeightUnitName = (DropDownList)e.Row.FindControl("ddlWeightUnitName");

            ddlWeightUnitName.DataSource = MedinetBusinessLogicComponent.WeightUnitsBLC.Instance.getWeightUnits();
            ddlWeightUnitName.DataTextField = "UnitName";
            ddlWeightUnitName.DataValueField = "WeightUnitId";
            ddlWeightUnitName.DataBind();

            // Add the initial value:
            initialLine = new ListItem("SELECCIONAR", "-1", true);
            initialLine.Selected = true;
            ddlWeightUnitName.Items.Insert(0, initialLine);

            ddlWeightUnitName.SelectedValue = weightUnitId == null ? "-1" : weightUnitId.ToString();
            ddlWeightUnitName.Enabled = true;
        }
        else
        {
            Label lblWeightUnitName = (Label)e.Row.FindControl("lblWeightUnitName");
            lblWeightUnitName.Text = weightUnitName == null ? "" : weightUnitName;
        }
    }

    private MedinetBusinessEntries.PresentationsInfo getPresentation(int divisionId, int categoryId, int productId, int pharmaFormId, int? qtyExternalPack, int? externalPackId,
        int? qtyInternalPack, int? internalPackId, string qtyContentUnit, int? contentUnitId, string qtyWeightUnit, int? weightUnitId)
    {
        MedinetBusinessEntries.PresentationsInfo presentationInfo = new MedinetBusinessEntries.PresentationsInfo();

        presentationInfo.DivisionId = divisionId;
        presentationInfo.CategoryId = categoryId;
        presentationInfo.ProductId = productId;
        presentationInfo.PharmaFormId = pharmaFormId;

        //Quantity ExternalPack
        if (qtyExternalPack > 0)
            presentationInfo.QtyExternalPack = qtyExternalPack;
        else
            presentationInfo.QtyExternalPack = null;

        //ExternalPackId
        if (externalPackId > 0)
            presentationInfo.ExternalPackId = externalPackId;
        else
            presentationInfo.ExternalPackId = null;

        //Quantity InternalPack
        if (qtyInternalPack > 0)
            presentationInfo.QtyInternalPack = qtyInternalPack;
        else
            presentationInfo.QtyInternalPack = null;

        //InternalPackId
        if (internalPackId > 0)
            presentationInfo.InternalPackId = internalPackId;
        else
            presentationInfo.InternalPackId = null;

        //Quantity ContentUnit
        if (qtyContentUnit != "-1")
            presentationInfo.QtyContentUnit = qtyContentUnit;
        else
            presentationInfo.QtyContentUnit = null;
        
        //ContentUnitId
        if (contentUnitId > 0)
            presentationInfo.ContentUnitId = contentUnitId;
        else
            presentationInfo.ContentUnitId = null;
        
        //Quantity WeightUnit
        if (qtyWeightUnit != "-1")
            presentationInfo.QtyWeightUnit = qtyWeightUnit;
        else
            presentationInfo.QtyWeightUnit = null;
        
        //WeightUnitId
        if (weightUnitId > 0)
            presentationInfo.WeightUnitId = weightUnitId;
        else
            presentationInfo.WeightUnitId = null;

        presentationInfo.Presentation = null;
        presentationInfo.Active = true;

        return presentationInfo;
    }

    private bool validValue(string textBoxValue)
    {
        string sPattern = "^(\\d|-)?(\\d|,)*\\.?\\d*$";

        if (!System.Text.RegularExpressions.Regex.IsMatch(textBoxValue, sPattern))
            return true;
        else
            return false;
    }

    #endregion

    private int _editionId;
    private int _bookId;
    private int _countryId;
    private int _divisionId;
    private int _userId;
    private int _userCountry;
    private string _hashkey;
    private int _categoryId;
    private int _productId;
    private int _pharmaFormId;
    private string _brand;
    private string _pharmaForm;

    protected void grdPresentationCatalog_RowEditing(object sender, GridViewEditEventArgs e)
    {
        this.grdPresentationCatalog.EditIndex = e.NewEditIndex;
        this.getPresentationCatalog();
    }
    protected void grdPresentationCatalog_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        int? qtyExternalPack, externalPackId, qtyInternalPack, internalPackId, contentUnitId, weightUnitId;

        string qtyContentUnit, qtyWeightUnit;

        int presentationId = Convert.ToInt32(this.grdPresentationCatalog.DataKeys[e.RowIndex].Values[0].ToString());
        int productId = Convert.ToInt32(this.grdPresentationCatalog.DataKeys[e.RowIndex].Values[1].ToString());
        int pharmaFormId = Convert.ToInt32(this.grdPresentationCatalog.DataKeys[e.RowIndex].Values[2].ToString());
        int divisionId = Convert.ToInt32(this.grdPresentationCatalog.DataKeys[e.RowIndex].Values[3].ToString());
        int categoryId = Convert.ToInt32(this.grdPresentationCatalog.DataKeys[e.RowIndex].Values[4].ToString());

        TextBox txtQtyExternalPack = (TextBox)this.grdPresentationCatalog.Rows[e.RowIndex].FindControl("txtQtyExternalPack");
        TextBox txtQtyInternalPack = (TextBox)this.grdPresentationCatalog.Rows[e.RowIndex].FindControl("txtQtyInternalPack");
        TextBox txtQtyContentUnit = (TextBox)this.grdPresentationCatalog.Rows[e.RowIndex].FindControl("txtQtyContentUnit");
        TextBox txtQtyWeightUnit = (TextBox)this.grdPresentationCatalog.Rows[e.RowIndex].FindControl("txtQtyWeightUnit");

        DropDownList ddlExternalPack = (DropDownList)this.grdPresentationCatalog.Rows[e.RowIndex].FindControl("ddlExternalPack");
        DropDownList ddlInternalPack = (DropDownList)this.grdPresentationCatalog.Rows[e.RowIndex].FindControl("ddlInternalPack");
        DropDownList ddlContentUnitName = (DropDownList)this.grdPresentationCatalog.Rows[e.RowIndex].FindControl("ddlContentUnitName");
        DropDownList ddlWeightUnitName = (DropDownList)this.grdPresentationCatalog.Rows[e.RowIndex].FindControl("ddlWeightUnitName");

        //Valid QtyExternalPack
        if (txtQtyExternalPack.Text.Trim() != "")
        {
            if (this.validValue(txtQtyExternalPack.Text.Trim()))
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "messageScript", "jAlert('Debe íngresar sólo números.', 'Presentación inválida');", true);
                return;
            }
            else
                qtyExternalPack = Convert.ToInt32(txtQtyExternalPack.Text.Trim());
        }
        else
            qtyExternalPack = -1;

        //Valid QtyInternalPack
        if (txtQtyInternalPack.Text.Trim() != "")
        {
            if (this.validValue(txtQtyInternalPack.Text.Trim()))
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "messageScript", "jAlert('Debe íngresar sólo números.', 'Presentación inválida');", true);
                return;
            }
            else
                qtyInternalPack = Convert.ToInt32(txtQtyInternalPack.Text.Trim());
        }
        else
            qtyInternalPack = -1;

        //Valid QtyContentUnit
        if (txtQtyContentUnit.Text.Trim() != "")
        {
            qtyContentUnit = txtQtyContentUnit.Text.Trim();
        }
        else
            qtyContentUnit = "-1";

        //Valid QtyWeightUnit
        if (txtQtyWeightUnit.Text.Trim() != "")
        {
            qtyWeightUnit = txtQtyWeightUnit.Text.Trim();
        }
        else
            qtyWeightUnit = "-1";

        internalPackId = ddlInternalPack.SelectedValue == "-1" ? -1 : Convert.ToInt32(ddlInternalPack.SelectedValue);
        externalPackId = ddlExternalPack.SelectedValue == "-1" ? -1 : Convert.ToInt32(ddlExternalPack.SelectedValue);
        contentUnitId = ddlContentUnitName.SelectedValue == "-1" ? -1 : Convert.ToInt32(ddlContentUnitName.SelectedValue);
        weightUnitId = ddlWeightUnitName.SelectedValue == "-1" ? -1 : Convert.ToInt32(ddlWeightUnitName.SelectedValue);

        if ((qtyExternalPack == -1 && externalPackId == -1) &&
            (qtyInternalPack == -1 && internalPackId == -1) &&
            (qtyContentUnit == "-1" && contentUnitId == -1) &&
            (qtyWeightUnit == "-1" && weightUnitId == -1))
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "messageScript", "jAlert('Debe elegir por lo menos una opción para actualizar la presentación.', 'Presentación inválida');", true);
            return;
        }

        MedinetBusinessEntries.PresentationsInfo presentationProduct = this.getPresentation(divisionId, categoryId, productId, pharmaFormId, qtyExternalPack,
            externalPackId, qtyInternalPack, internalPackId, qtyContentUnit, contentUnitId, qtyWeightUnit, weightUnitId);

        presentationProduct.PresentationId = presentationId;
        presentationProduct.Presentation = null;
        presentationProduct.Active = true;

        MedinetBusinessLogicComponent.PresentationsBLC.Instance.updatePresentationToProduct(presentationProduct, this._userId, this._hashkey);

        this.grdPresentationCatalog.EditIndex = -1;
        this.getPresentationCatalog();
        this.getProductPresentations();
        ScriptManager.RegisterStartupScript(this, typeof(Page), "messageScript", "jAlert('El registro se actualizó con éxito.', 'Modificación exitosa');", true);
    }
}