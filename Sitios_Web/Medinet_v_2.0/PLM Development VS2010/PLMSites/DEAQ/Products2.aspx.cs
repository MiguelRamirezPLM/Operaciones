using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.XtraReports.UI;
using DevExpress.XtraReports.Parameters;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using DevExpress.Web.Data;
using DevExpress.Web.ASPxGridView;
public partial class Products2 : System.Web.UI.Page
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
        this._user = this.Session["user"] == null ? "" : this.Session["user"].ToString();
        this._roleId = this.Session["roleId"] == null ? -1 : Convert.ToInt32(this.Session["roleId"]);
        this._bookName = this.Session["bookName"] == null ? "" : this.Session["bookName"].ToString();
        this._editionName = this.Session["edition"] == null ? "" : (this.Session["edition"]).ToString();
        this._divisionName = this.Session["divisionName"] == null ? "" : this.Session["divisionName"].ToString();
        this._bookShortName = this.Session["bookShortName"] == null ? "" : this.Session["bookShortName"].ToString();

        if (!IsPostBack)
        {

            this.prepareGrid(Convert.ToInt16(this.ddlPageSize.SelectedValue));
            this.loadLaboratories();
            loadProducts();
            this.getEditableProducts();
            this.preparedTableToAddProducts();
            this.preparedTableToAddPharmaForms();
        }
    }

    #endregion

    #region ControlEvents

    //Grid's Events
    protected void grdProducts_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        this.grdProducts.PageIndex = e.NewPageIndex;
        this.Session["CurrentPage"] = Convert.ToInt32(e.NewPageIndex);
        this.getEditableProducts();
    }


    protected void aspGrdProducts_HtmlRowCreated(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType == DevExpress.Web.ASPxGridView.GridViewRowType.Data)
        {
            AgroBusinessEntries.ProductsToEditInfo currentRow = new AgroBusinessEntries.ProductsToEditInfo() ;

            //Checkboxs
            CheckBox chk1 = (CheckBox)grdProducts.FindRowCellTemplateControl(e.VisibleIndex, null, "Participate");
            CheckBox chk2 = (CheckBox)grdProducts.FindRowCellTemplateControl(e.VisibleIndex, null, "NewProduct");
            CheckBox chk3 = (CheckBox)grdProducts.FindRowCellTemplateControl(e.VisibleIndex, null, "ModifiedContent");
            CheckBox chk4 = (CheckBox)grdProducts.FindRowCellTemplateControl(e.VisibleIndex, null, "Mentionated");
            Boolean newProduct = false;
            Boolean withChanges = false;
            Boolean withOutChanges = false;
            currentRow.ProductId = (int)grdProducts.GetRowValues(e.VisibleIndex, "ProductId");
            currentRow.PharmaFormId = (int)grdProducts.GetRowValues(e.VisibleIndex, "PharmaFormId");
            currentRow.DivisionId = (int)grdProducts.GetRowValues(e.VisibleIndex, "DivisionId");
            if (currentRow.DivisionId != null)
            {
                //Enable checkbox
                chk1.Enabled = true;
                chk2.Enabled = false;
                // chk3.Enabled = true;
                chk4.Enabled = true;


                //Participantss
                chk1.Attributes.Add("productId", currentRow.ProductId.ToString());
                chk1.Attributes.Add("pharmaFormId", currentRow.PharmaFormId.ToString());
                chk1.Checked = currentRow.Participant;
                chk1.Attributes.Add("onclick", "javascript:checkMutuallyExclusive('" + chk4.ClientID + "'," + "'" + chk3.ClientID + "'" + ")");


                chk4.Attributes.Add("productId", currentRow.ProductId.ToString());
                chk4.Attributes.Add("pharmaFormId", currentRow.PharmaFormId.ToString());
                chk4.Checked = currentRow.Mentionated;
                //  chk4.Attributes.Add("onclick", "javascript:checkMutuallyExclusive('" + chk3.ClientID + "')");
                chk4.Attributes.Add("onclick", "javascript:checkMutuallyExclusive('" + chk1.ClientID + "'," + "'" + chk3.ClientID + "'" + ")");



                //New Product
                chk2.Attributes.Add("productId", currentRow.ProductId.ToString());
                chk2.Attributes.Add("pharmaFormId", currentRow.PharmaFormId.ToString());
                chk2.Checked = currentRow.NewProduct;
                // chk2.Attributes.Add("onclick", "javascript:checkMutuallyExclusive('" + chk3.ClientID + "')");

                //Modified content
                chk3.Attributes.Add("productId", currentRow.ProductId.ToString());
                chk3.Attributes.Add("pharmaFormId", currentRow.PharmaFormId.ToString());
                chk3.Checked = currentRow.ModifiedContent;
                //chk3.Attributes.Add("onclick", "javascript:checkMutuallyExclusive('" + chk2.ClientID + "')");
                //chk3.Attributes.Add("onclick", "javascript:checkMutuallyExclusive('" + chk2.ClientID + "'," + "'" + chk4.ClientID + "'" + ")");

                if (chk2.Checked == true)
                    chk3.Enabled = false;
                else if (chk1.Checked == true)
                    chk3.Enabled = true;


            }
        }
    }

    protected void grdProducts_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            AgroBusinessEntries.ProductsToEditInfo currentRow = (AgroBusinessEntries.ProductsToEditInfo)e.Row.DataItem;


            //Checkboxs
            CheckBox chk1 = (CheckBox)e.Row.FindControl("Participate");
            CheckBox chk2 = (CheckBox)e.Row.FindControl("NewProduct");
            CheckBox chk3 = (CheckBox)e.Row.FindControl("ModifiedContent");
            CheckBox chk4 = (CheckBox)e.Row.FindControl("Mentionated");
            Boolean newProduct = false;
            Boolean withChanges = false;
            Boolean withOutChanges = false;

            if (currentRow.DivisionId != null)
            {
                //Enable checkbox
                chk1.Enabled = true;
                chk2.Enabled = true;
                chk3.Enabled = true;
                chk4.Enabled = true;

                //Participant
                chk1.Attributes.Add("productId", currentRow.ProductId.ToString());
                chk1.Attributes.Add("pharmaFormId", currentRow.PharmaFormId.ToString());
                chk1.Checked = currentRow.Participant;
                chk1.Attributes.Add("onclick", "javascript:checkMutuallyExclusive('" + chk4.ClientID + "')");


                chk4.Attributes.Add("productId", currentRow.ProductId.ToString());
                chk4.Attributes.Add("pharmaFormId", currentRow.PharmaFormId.ToString());
                chk4.Checked = currentRow.Mentionated;
                //  chk4.Attributes.Add("onclick", "javascript:checkMutuallyExclusive('" + chk3.ClientID + "')");
                chk4.Attributes.Add("onclick", "javascript:checkMutuallyExclusive('" + chk1.ClientID + "'," + "'" + chk3.ClientID + "'" + ")");

                //if (PLMUsersBusinessLogicComponent.ApplicationUsersBLC.Instance.getRole(this._userId, this._hashkey).RoleId != (int)Utilities.Roles.Administrador && (chk1.Checked == true || chk2.Checked == true))
                //{
                //    chk1.Enabled = false;
                //    chk2.Enabled = false;
                //    chk3.Enabled = false;
                //    chk4.Enabled = false;
                //}

                if (currentRow.ModifiedContent == null)
                {
                    newProduct = true;
                    withChanges = false;
                    withOutChanges = false;
                }
                else if (currentRow.ModifiedContent == true)
                {
                    newProduct = false;
                    withChanges = true;

                }
                else if (currentRow.ModifiedContent == false)
                {
                    newProduct = false;
                    withChanges = false;

                }

                //New Product
                chk2.Attributes.Add("productId", currentRow.ProductId.ToString());
                chk2.Attributes.Add("pharmaFormId", currentRow.PharmaFormId.ToString());
                chk2.Checked = newProduct;
                chk2.Attributes.Add("onclick", "javascript:checkMutuallyExclusive('" + chk3.ClientID + "')");

                //Modified content
                chk3.Attributes.Add("productId", currentRow.ProductId.ToString());
                chk3.Attributes.Add("pharmaFormId", currentRow.PharmaFormId.ToString());
                chk3.Checked = withChanges;
                //chk3.Attributes.Add("onclick", "javascript:checkMutuallyExclusive('" + chk2.ClientID + "')");
                chk3.Attributes.Add("onclick", "javascript:checkMutuallyExclusive('" + chk2.ClientID + "'," + "'" + chk4.ClientID + "'" + ")");




            }





        }
    }


    protected void grdProducts_RowEditing(object sender, ASPxDataUpdatingEventArgs e)
    {
        //this.grdProducts.EditIndex = e.NewEditIndex;
        //grdProducts.EditingRowVisibleIndex = 2;
        //this.getEditableProducts();
    }

    protected void grdProducts_RowUpdating(object sender, ASPxDataUpdatingEventArgs e)
    {
        CheckBox chkParticipant;
        CheckBox chkNewProduct;
        CheckBox chkModified;
        CheckBox chkMentionated;
        ImageButton btn = (ImageButton)sender;
        GridViewDataRowTemplateContainer Row = (GridViewDataRowTemplateContainer)btn.NamingContainer;
        AgroBusinessEntries.ParticipantProductsInfo currentParticipant;
        AgroBusinessEntries.EditionProductShotsInfo productShot;

        int productId = (int)e.Keys["ProductId"];
        int pharmaFormId = (int)e.Keys["PharmaFormId"];
        int divisionId = (int)e.Keys["DivisionId"];
        int categoryId = (int)e.Keys["CategoryId"];

        DropDownList ddlFamilyProduct = (DropDownList)grdProducts.FindRowCellTemplateControl(Row.VisibleIndex, null, "ddlContentFamilyString");
        DropDownList ddlFamilyProductShot = (DropDownList)grdProducts.FindRowCellTemplateControl(Row.VisibleIndex, null, "ddlPSFamilyString");
        TextBox txtBrand = (TextBox)grdProducts.FindRowCellTemplateControl(Row.VisibleIndex, null, "txtBrand");
        TextBox txtDescription = (TextBox)grdProducts.FindRowCellTemplateControl(Row.VisibleIndex, null, "txtDescription");
        TextBox txtRegister = (TextBox)grdProducts.FindRowCellTemplateControl(Row.VisibleIndex, null, "txtRegister");
        CheckBoxList chkCountries = (CheckBoxList)grdProducts.FindRowCellTemplateControl(Row.VisibleIndex, null, "chkCountries");
        //Countries

        //Brand
        AgroBusinessEntries.ProductsInfo product = AgroBusinessLogicComponent.ProductsBLC.Instance.getProductById(productId);
        if (txtBrand.Text.Trim() == "")
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "messageScript", "jAlert('Debe ingresar una marca.', 'Marca Inválida');", true);
            return;
        }
        product.ProductName = txtBrand.Text.Trim().ToUpper();
        product.Description = txtDescription.Text.Trim().ToUpper();
        product.Register = txtRegister.Text.Trim().ToUpper();
        AgroBusinessLogicComponent.ProductsBLC.Instance.updateProduct(product, this._userId, this._hashkey);

        //PharmaceuticalForms
        AgroBusinessEntries.ProductPharmaFormInfo productPharmaForm = AgroBusinessLogicComponent.ProductPharmaFormsBLC.Instance.getProductPF(productId, pharmaFormId);
        if (productPharmaForm == null)
        {
            productPharmaForm = new AgroBusinessEntries.ProductPharmaFormInfo();
            productPharmaForm.ProductId = productId;
            productPharmaForm.PharmaFormId = pharmaFormId;
            productPharmaForm.Active = true;
            AgroBusinessLogicComponent.ProductPharmaFormsBLC.Instance.addPharmaForm(productPharmaForm, this._userId, this._hashkey);
        }

        //ProductCategories
        AgroBusinessEntries.ProductCategoriesInfo productCategory = AgroBusinessLogicComponent.ProductCategoriesBLC.Instance.getCategory(productId,
             pharmaFormId, divisionId, categoryId);
        if (productCategory == null)
        {
            productCategory = new AgroBusinessEntries.ProductCategoriesInfo();
            productCategory.ProductId = productId;
            productCategory.PharmaFormId = pharmaFormId;
            productCategory.DivisionId = divisionId;
            productCategory.CategoryId = categoryId;
            AgroBusinessLogicComponent.ProductCategoriesBLC.Instance.addProductToCategory(productCategory, this._userId, this._hashkey);
        }

        //Participant Product
        chkParticipant = (CheckBox)grdProducts.FindRowCellTemplateControl(Row.VisibleIndex, null, "Participate");

        //New Product
        chkNewProduct = (CheckBox)grdProducts.FindRowCellTemplateControl(Row.VisibleIndex, null,"NewProduct");

        //Modified Product
        chkModified = (CheckBox)grdProducts.FindRowCellTemplateControl(Row.VisibleIndex, null, "ModifiedContent");

        chkMentionated = (CheckBox)grdProducts.FindRowCellTemplateControl(Row.VisibleIndex, null, "Mentionated");
        if (divisionId > 0 && categoryId > 0)
        {
            currentParticipant = AgroBusinessLogicComponent.ParticipantProductsBLC.Instance.getParticipantProduct(productId,
                this._editionId, pharmaFormId, divisionId, categoryId);

            if (currentParticipant == null)
            {
                if (chkParticipant.Checked == true)
                {
                    //Add ParticipantProduct
                    currentParticipant = this.getParticipantProduct(this._editionId, divisionId, categoryId, productId, pharmaFormId);
                    AgroBusinessLogicComponent.ParticipantProductsBLC.Instance.addToBook(currentParticipant, this._userId, this._hashkey);

                    //Update field ModifiedContent
                    currentParticipant.ModifiedContent = chkModified.Checked;
                    AgroBusinessLogicComponent.ParticipantProductsBLC.Instance.modifiedContent(currentParticipant, this._userId, this._hashkey);

                    //Verifies if the product is new or is not
                    this.addOrRemoveNewProduct(productId, pharmaFormId, divisionId, categoryId, chkNewProduct, chkParticipant, chkMentionated);


                }


            }
            else
            {
                if (chkParticipant.Checked == true)
                {
                    //Update field ModifiedContent
                    currentParticipant.ModifiedContent = chkModified.Checked;
                    AgroBusinessLogicComponent.ParticipantProductsBLC.Instance.modifiedContent(currentParticipant, this._userId, this._hashkey);

                    //Erase the modified Attribute group
                    if (chkModified.Checked == false)
                    {
                        List<AgroBusinessEntries.ModifiedAttributeGroupsInfo> modifiedAttributeGroups =
                            AgroBusinessLogicComponent.ModifiedAttributeGroupsBLC.Instance.getModifiedAttributeGroupsByProduct(this._editionId, divisionId, categoryId, pharmaFormId, productId);

                        if (modifiedAttributeGroups.Count > 0)
                        {
                            foreach (AgroBusinessEntries.ModifiedAttributeGroupsInfo attributeGroupItem in modifiedAttributeGroups)
                                AgroBusinessLogicComponent.ModifiedAttributeGroupsBLC.Instance.removeProductToAttributeGroup(attributeGroupItem, this._userId, this._hashkey);
                        }
                    }

                    //Verifies if the product is new or not
                    this.addOrRemoveNewProduct(productId, pharmaFormId, divisionId, categoryId, chkNewProduct, chkParticipant, chkMentionated);

                }
                else
                {

                    //Erase the modified Attribute group
                    List<AgroBusinessEntries.ModifiedAttributeGroupsInfo> modifiedAttributeGroups =
                            AgroBusinessLogicComponent.ModifiedAttributeGroupsBLC.Instance.getModifiedAttributeGroupsByProduct(this._editionId, divisionId, categoryId, pharmaFormId, productId);

                    if (modifiedAttributeGroups.Count > 0)
                    {
                        foreach (AgroBusinessEntries.ModifiedAttributeGroupsInfo attributeGroupItem in modifiedAttributeGroups)
                            AgroBusinessLogicComponent.ModifiedAttributeGroupsBLC.Instance.removeProductToAttributeGroup(attributeGroupItem, this._userId, this._hashkey);
                    }

                    AgroBusinessLogicComponent.ParticipantProductsBLC.Instance.removeToBook(currentParticipant, this._userId, this._hashkey);
                    this.addOrRemoveNewProduct(productId, pharmaFormId, divisionId, categoryId, chkNewProduct, chkParticipant, chkMentionated);
                }
            }
            this.addOrRemoveEdition(productId, pharmaFormId, divisionId, categoryId, chkParticipant);
        }
       // this.grdProducts.EditIndex = -1;
        this.getEditableProducts();
        ScriptManager.RegisterStartupScript(this, typeof(Page), "messageScript", "jAlert('El registro se actualizó con éxito.', 'Modificación exitosa');", true);
    }

    //DropDownList's Events
    protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.Session["CurrentPage"] = 0;
        this.prepareGrid(Convert.ToInt16(this.ddlPageSize.SelectedValue));
        this.getEditableProducts();
    }

    protected void ddlContentFamilyString_SelectedIndexChanged(object sender, EventArgs e)
    {
        //int productId = Convert.ToInt32(this.grdProducts.DataKeys[this.grdProducts.EditIndex].Values[0].ToString());
        //int pharmaFormId = Convert.ToInt32(this.grdProducts.DataKeys[this.grdProducts.EditIndex].Values[1].ToString());
        //int divisionId = Convert.ToInt32(this.grdProducts.DataKeys[this.grdProducts.EditIndex].Values[2].ToString());
        //int categoryId = Convert.ToInt32(this.grdProducts.DataKeys[this.grdProducts.EditIndex].Values[3].ToString());

        //DropDownList ddlContentFamily = (DropDownList)sender;

        //if (ddlContentFamily.SelectedValue == "-2")
        //{



        //    //ParticipantProducts
        //    AgroBusinessEntries.ParticipantProductsInfo currentParticipant = AgroBusinessLogicComponent.ParticipantProductsBLC.Instance.getParticipantProduct(productId,
        //        this._editionId, pharmaFormId, divisionId, categoryId);

        //    if (currentParticipant == null)
        //    {
        //        currentParticipant = this.getParticipantProduct(this._editionId, divisionId, categoryId, productId, pharmaFormId);
        //        AgroBusinessLogicComponent.ParticipantProductsBLC.Instance.addToBook(currentParticipant, this._userId, this._hashkey);
        //    }

        //    //EditionDivisionProducts
        //    AgroBusinessEntries.EditionDivisionProductInfo currentEdProduct = AgroBusinessLogicComponent.EditionDivisionProductsBLC.Instance.getDivision(this._editionId, divisionId,
        //        categoryId, productId, pharmaFormId);
        //    if (currentEdProduct == null)
        //    {
        //        currentEdProduct = new AgroBusinessEntries.EditionDivisionProductInfo();
        //        currentEdProduct.EditionId = this._editionId;
        //        currentEdProduct.DivisionId = divisionId;
        //        currentEdProduct.CategoryId = categoryId;
        //        currentEdProduct.PharmaFormId = pharmaFormId;
        //        currentEdProduct.ProductId = productId;
        //        AgroBusinessLogicComponent.EditionDivisionProductsBLC.Instance.addDivision(currentEdProduct, this._userId, this._hashkey);
        //    }


        //    this.grdProducts.EditIndex = -1;
        //    this.getEditableProducts();
        //    //ScriptManager.RegisterStartupScript(this, typeof(Page), "messageScript", "jAlert('La familia de contenido se agregó con éxito.', 'Familia de contenido');", true);

        //}
    }

    protected void ddlPSFamilyString_SelectedIndexChanged(object sender, EventArgs e)
    {
        //int editionProductShotId = 0;
        //int productId = Convert.ToInt32(this.grdProducts.DataKeys[this.grdProducts.EditIndex].Values[0].ToString());
        //int pharmaFormId = Convert.ToInt32(this.grdProducts.DataKeys[this.grdProducts.EditIndex].Values[1].ToString());
        //int divisionId = Convert.ToInt32(this.grdProducts.DataKeys[this.grdProducts.EditIndex].Values[2].ToString());
        //int categoryId = Convert.ToInt32(this.grdProducts.DataKeys[this.grdProducts.EditIndex].Values[3].ToString());

        //DropDownList ddlPSFamily = (DropDownList)sender;

        //if (ddlPSFamily.SelectedValue == "-2")
        //{


        //    //ParticipantProducts
        //    AgroBusinessEntries.ParticipantProductsInfo currentParticipant = AgroBusinessLogicComponent.ParticipantProductsBLC.Instance.getParticipantProduct(productId,
        //        this._editionId, pharmaFormId, divisionId, categoryId);

        //    if (currentParticipant == null)
        //    {
        //        currentParticipant = this.getParticipantProduct(this._editionId, divisionId, categoryId, productId, pharmaFormId);
        //        AgroBusinessLogicComponent.ParticipantProductsBLC.Instance.addToBook(currentParticipant, this._userId, this._hashkey);
        //    }

        //    //EditionDivisionProducts
        //    AgroBusinessEntries.EditionDivisionProductInfo currentEdProduct = AgroBusinessLogicComponent.EditionDivisionProductsBLC.Instance.getDivision(this._editionId, divisionId,
        //        categoryId, productId, pharmaFormId);
        //    if (currentEdProduct == null)
        //    {
        //        currentEdProduct = new AgroBusinessEntries.EditionDivisionProductInfo();
        //        currentEdProduct.EditionId = this._editionId;
        //        currentEdProduct.DivisionId = divisionId;
        //        currentEdProduct.CategoryId = categoryId;
        //        currentEdProduct.PharmaFormId = pharmaFormId;
        //        currentEdProduct.ProductId = productId;
        //        AgroBusinessLogicComponent.EditionDivisionProductsBLC.Instance.addDivision(currentEdProduct, this._userId, this._hashkey);
        //    }


        //    this.grdProducts.EditIndex = -1;
        //    this.getEditableProducts();
        //    // ScriptManager.RegisterStartupScript(this, typeof(Page), "messageScript", "jAlert('La familia de SIDEF se agregó con éxito.', 'Familia de SIDEF');", true);

        //}
    }

    protected void ddlProductToAdd_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (this.ddlProductToAdd.SelectedItem.ToString() != "SELECCIONAR")
        {
            int productId = Convert.ToInt32(this.ddlProductToAdd.SelectedValue);
            this.ddlNewPharmaFormToAdd.Enabled = true;
            this.ddlNewPharmaFormToAdd.DataSource = AgroBusinessLogicComponent.CatalogsBLC.Instance.getPharmaFormsWithoutProduct(productId);
            this.ddlNewPharmaFormToAdd.DataTextField = "Description";
            this.ddlNewPharmaFormToAdd.DataValueField = "PharmaFormId";
            this.ddlNewPharmaFormToAdd.DataBind();

            ListItem initialLine = new ListItem("SELECCIONAR", "-1", true);
            initialLine.Selected = true;
            this.ddlNewPharmaFormToAdd.Items.Insert(0, initialLine);
        }
        else
        {
            this.ddlNewPharmaFormToAdd.SelectedIndex = -1;
            this.ddlNewPharmaFormToAdd.Enabled = false;
        }
    }

    //Button's Events
    protected void btnConsult_Click(object sender, EventArgs e)
    {
        this.prepareGrid(Convert.ToInt16(this.ddlPageSize.SelectedValue));
        this.getEditableProducts();

    }

    protected void btnAddProduct_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(this.txtBrandToAdd.Text.Trim()))
        {
            if (Convert.ToInt32(this.ddlPharmaFormToAdd.SelectedValue) != -1)
            {
                this.addProduct();

                this.txtBrandToAdd.Text = "";
                this.ddlPharmaFormToAdd.SelectedValue = "-1";
                this.preparedTableToAddPharmaForms();
                this.getEditableProducts();
                 ScriptManager.RegisterStartupScript(this, typeof(Page), "messageScript", "jAlert('El producto se agregó con éxito.', 'Producto guardado');", true);
            }
            else
                ScriptManager.RegisterStartupScript(this, typeof(Page), "messageScript", "jAlert('Debe elegir una Forma Farmacéutica', 'Forma Farmacéutica');", true);
        }
        else
            ScriptManager.RegisterStartupScript(this, typeof(Page), "messageScript", "jAlert('Debe ingresar una marca', 'Marca');", true);
    }

    protected void btnAddPharmaForm_Click(object sender, EventArgs e)
    {
        if (Convert.ToInt32(this.ddlProductToAdd.SelectedValue) != -1)
        {
            if (Convert.ToInt32(this.ddlNewPharmaFormToAdd.SelectedValue) != -1)
            {
                this.addPharmaFormToProduct();

                this.ddlProductToAdd.SelectedValue = "-1";
                this.ddlNewPharmaFormToAdd.SelectedValue = "-1";
                this.getEditableProducts();
                ScriptManager.RegisterStartupScript(this, typeof(Page), "messageScript", "jAlert('La forma se agregó con éxito.', 'Forma Farmacéutica guardada');", true);
            }
            else
                ScriptManager.RegisterStartupScript(this, typeof(Page), "messageScript", "jAlert('Debe elegir una Forma Farmacéutica', 'Forma Farmacéutica');", true);
        }
        else
            ScriptManager.RegisterStartupScript(this, typeof(Page), "messageScript", "jAlert('Debe elegir un producto', 'Producto inválido');", true);

    }

    protected void btnLabs_Click(object sender, EventArgs e)
    {
        this.Session["divisionId"] = -1;
        this.Response.Redirect("laboratories.aspx");
    }

    protected void btnAddLaboratory_Click(object sender, EventArgs e)
    {




    }
    protected void btnEditLaboratory_Click(object sender, EventArgs e)
    {
        this.Response.Redirect("laboratoryInformation.aspx");
    }

    protected void btnSaveParticipant_Click(object sender, EventArgs e)
    {
        //int productId, pharmaFormId, divisionId, categoryId;

        //CheckBox chkParticipant, chkNewProduct, chkModified, chkMentionated;

        //AgroBusinessEntries.ParticipantProductsInfo currentParticipant;
        //AgroBusinessEntries.MentionatedProductInfo currentParticipant2;
        //AgroBusinessEntries.EditionProductShotsInfo productShot;

        //for (int gridRow = 0; gridRow < this.grdProducts.Rows.Count; gridRow++)
        //{
        //    productId = Convert.ToInt32(this.grdProducts.DataKeys[gridRow].Values[0].ToString());
        //    pharmaFormId = Convert.ToInt32(this.grdProducts.DataKeys[gridRow].Values[1].ToString());
        //    divisionId = this.grdProducts.DataKeys[gridRow].Values[2] == null ? -1 : Convert.ToInt32(this.grdProducts.DataKeys[gridRow].Values[2].ToString());
        //    categoryId = this.grdProducts.DataKeys[gridRow].Values[3] == null ? -1 : Convert.ToInt32(this.grdProducts.DataKeys[gridRow].Values[3].ToString());

        //    //Participant Product
        //    chkParticipant = (CheckBox)this.grdProducts.Rows[gridRow].FindControl("Participate");
        //    //Modified Content
        //    chkModified = (CheckBox)this.grdProducts.Rows[gridRow].FindControl("ModifiedContent");
        //    //New Product
        //    chkNewProduct = (CheckBox)this.grdProducts.Rows[gridRow].FindControl("NewProduct");
        //    chkMentionated = (CheckBox)this.grdProducts.Rows[gridRow].FindControl("Mentionated");

        //    if (divisionId > 0 && categoryId > 0)
        //    {
        //        currentParticipant = AgroBusinessLogicComponent.ParticipantProductsBLC.Instance.getParticipantProduct(productId,
        //            this._editionId, pharmaFormId, divisionId, categoryId);

        //        if (currentParticipant == null)
        //        {
        //            if (chkParticipant.Checked == true)
        //            {
        //                currentParticipant = this.getParticipantProduct(this._editionId, divisionId, categoryId, productId, pharmaFormId);
        //                AgroBusinessLogicComponent.ParticipantProductsBLC.Instance.addToBook(currentParticipant, this._userId, this._hashkey);

        //                if (chkModified.Checked == false && chkNewProduct.Checked == false)
        //                {
        //                    currentParticipant.ModifiedContent = false;
        //                }
        //                else if (chkNewProduct.Checked == true)
        //                {
        //                    currentParticipant.ModifiedContent = null;

        //                }
        //                else if (chkModified.Checked == true)
        //                {
        //                    currentParticipant.ModifiedContent = true;

        //                }
        //                AgroBusinessLogicComponent.ParticipantProductsBLC.Instance.modifiedContent(currentParticipant, this._userId, this._hashkey);
        //                this.addOrRemoveNewProduct(productId, pharmaFormId, divisionId, categoryId, chkNewProduct, chkParticipant, chkMentionated);


        //            }

        //            else
        //            {

        //                this.addOrRemoveNewProduct(productId, pharmaFormId, divisionId, categoryId, chkNewProduct, chkParticipant, chkMentionated);
        //            }
        //        }
        //        else
        //        {
        //            if (chkParticipant.Checked == true)
        //            {
        //                if (chkModified.Checked == false && chkNewProduct.Checked == false)
        //                {
        //                    currentParticipant.ModifiedContent = false;
        //                }
        //                else if (chkNewProduct.Checked == true)
        //                {
        //                    currentParticipant.ModifiedContent = null;

        //                }
        //                else if (chkModified.Checked == true)
        //                {
        //                    currentParticipant.ModifiedContent = true;

        //                }
        //                // currentParticipant.ModifiedContent = chkModified.Checked;
        //                AgroBusinessLogicComponent.ParticipantProductsBLC.Instance.modifiedContent(currentParticipant, this._userId, this._hashkey);

        //                //Erase the modified Attribute group
        //                if (chkModified.Checked == false)
        //                {
        //                    List<AgroBusinessEntries.ModifiedAttributeGroupsInfo> modifiedAttributeGroups =
        //                        AgroBusinessLogicComponent.ModifiedAttributeGroupsBLC.Instance.getModifiedAttributeGroupsByProduct(this._editionId, divisionId, categoryId, pharmaFormId, productId);

        //                    if (modifiedAttributeGroups.Count > 0)
        //                    {
        //                        foreach (AgroBusinessEntries.ModifiedAttributeGroupsInfo attributeGroupItem in modifiedAttributeGroups)
        //                            AgroBusinessLogicComponent.ModifiedAttributeGroupsBLC.Instance.removeProductToAttributeGroup(attributeGroupItem, this._userId, this._hashkey);
        //                    }
        //                }

        //                this.addOrRemoveNewProduct(productId, pharmaFormId, divisionId, categoryId, chkNewProduct, chkParticipant, chkMentionated);


        //            }
        //            else
        //            {


        //                //Erase the modified Attribute group
        //                List<AgroBusinessEntries.ModifiedAttributeGroupsInfo> modifiedAttributeGroups =
        //                        AgroBusinessLogicComponent.ModifiedAttributeGroupsBLC.Instance.getModifiedAttributeGroupsByProduct(this._editionId, divisionId, categoryId, pharmaFormId, productId);

        //                if (modifiedAttributeGroups.Count > 0)
        //                {
        //                    foreach (AgroBusinessEntries.ModifiedAttributeGroupsInfo attributeGroupItem in modifiedAttributeGroups)
        //                        AgroBusinessLogicComponent.ModifiedAttributeGroupsBLC.Instance.removeProductToAttributeGroup(attributeGroupItem, this._userId, this._hashkey);
        //                }
        //                AgroBusinessLogicComponent.ParticipantProductsBLC.Instance.removeToBook(currentParticipant, this._userId, this._hashkey);
        //                this.addOrRemoveNewProduct(productId, pharmaFormId, divisionId, categoryId, chkNewProduct, chkParticipant, chkMentionated);
        //            }
        //        }

        //        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //        currentParticipant2 = AgroBusinessLogicComponent.MentionatedProductsBLC.Instance.getParticipantProduct(productId,
        //           this._editionId, pharmaFormId, divisionId, categoryId);
        //        if (currentParticipant2 == null)
        //        {
        //            if (chkMentionated.Checked == true)
        //            {
        //                currentParticipant2 = this.getMentionatedProduct(this._editionId, divisionId, categoryId, productId, pharmaFormId);
        //                AgroBusinessLogicComponent.MentionatedProductsBLC.Instance.addToBook(currentParticipant2, this._userId, this._hashkey);


        //                this.addOrRemoveNewProduct(productId, pharmaFormId, divisionId, categoryId, chkNewProduct, chkParticipant, chkMentionated);


        //            }

        //            else
        //            {

        //                this.addOrRemoveNewProduct(productId, pharmaFormId, divisionId, categoryId, chkNewProduct, chkParticipant, chkMentionated);
        //            }
        //        }
        //        else
        //        {
        //            if (chkMentionated.Checked == true)
        //            {


        //                this.addOrRemoveNewProduct(productId, pharmaFormId, divisionId, categoryId, chkNewProduct, chkParticipant, chkMentionated);


        //            }
        //            else
        //            {



        //                AgroBusinessLogicComponent.MentionatedProductsBLC.Instance.removeToBook(currentParticipant2, this._userId, this._hashkey);
        //                this.addOrRemoveNewProduct(productId, pharmaFormId, divisionId, categoryId, chkNewProduct, chkParticipant, chkMentionated);
        //            }
        //        }
        //        //this.addOrRemoveEdition(productId, pharmaFormId, divisionId, categoryId, chkParticipant);
        //    }
        //}
        //this.ddlProductToAdd.SelectedValue = "-1";
        //this.ddlNewPharmaFormToAdd.SelectedValue = "-1";
        //this.getEditableProducts();
        // ScriptManager.RegisterStartupScript(this, typeof(Page), "messageScript", "jAlert('El producto se agregó con éxito.', 'Producto guardado');", true);
        // ScriptManager.RegisterStartupScript(this, typeof(Page), "messageScript", "jAlert('Los registros se guardaron con éxito.', 'Registros guardados');", true);
    }

    protected void btnChangeDivision_Click(object sender, EventArgs e)
    {
        this.Response.Redirect("productDivisions.aspx");
    }



    protected void imgBtnExport_Click(object sender, ImageClickEventArgs e)
    {
        exportPdf();
    }
    protected void imgBtnExportxls_Click(object sender, ImageClickEventArgs e)
    {

        exportXls();
    }
    #endregion

    #region Private Methods


    private void getProductsData()
    {
        this._ProductsInfo = new AgroBusinessEntries.ProductsInfo();

        this._ProductsInfo.ProductName = txtBrandToAdd.Text.Trim().ToUpper();
        this._ProductsInfo.CountryId = this._countryId;
        this._ProductsInfo.Description = null;
        this._ProductsInfo.SanitaryRegistry = null;
        this._ProductsInfo.Register = txtRegisterToAdd.Text.Trim().ToUpper();
        this._ProductsInfo.Description = txtDescriptionToAdd.Text.Trim().ToUpper();
        this._ProductsInfo.Active = true;



        //LaboratoryInformation
        AgroBusinessEntries.DivisionsInfo divisionInfo = AgroBusinessLogicComponent.CatalogsBLC.Instance.getDivision(this._divisionId);
        this._ProductsInfo.LaboratoryId = divisionInfo.LaboratoryId;
    }

    private void getEditableProducts()
    {
        string proNameToSearch = null;
        this.grdProducts.DataSource = AgroBusinessLogicComponent.ProductsBLC.Instance.getEditableProductsByDivision(this._divisionId, this._countryId, this._editionId, this._bookId, proNameToSearch);
        this.grdProducts.DataBind();

    }



    private AgroBusinessEntries.EditionProductShotsInfo getEditionProductShot(int editionId, int divisionId, int categoryId, int productId, int pharmaFormId)
    {
        AgroBusinessEntries.EditionProductShotsInfo editionProductShot = new AgroBusinessEntries.EditionProductShotsInfo();
        editionProductShot.EditionId = editionId;
        editionProductShot.DivisionId = divisionId;
        editionProductShot.CategoryId = categoryId;
        editionProductShot.PharmaFormId = pharmaFormId;
        editionProductShot.ProductId = productId;
        editionProductShot.PSTypeId = 1;
        editionProductShot.ProductShot = null;
        editionProductShot.QtyCells = null;
        editionProductShot.Active = true;

        return editionProductShot;
    }

    private AgroBusinessEntries.ParticipantProductsInfo getParticipantProduct(int editionId, int divisionId, int categoryId, int productId, int pharmaFormId)
    {
        AgroBusinessEntries.ParticipantProductsInfo participantProduct = new AgroBusinessEntries.ParticipantProductsInfo();
        participantProduct.EditionId = editionId;
        participantProduct.DivisionId = divisionId;
        participantProduct.CategoryId = categoryId;
        participantProduct.ProductId = productId;
        participantProduct.PharmaFormId = pharmaFormId;

        return participantProduct;
    }

    private AgroBusinessEntries.MentionatedProductInfo getMentionatedProduct(int editionId, int divisionId, int categoryId, int productId, int pharmaFormId)
    {
        AgroBusinessEntries.MentionatedProductInfo participantProduct = new AgroBusinessEntries.MentionatedProductInfo();
        participantProduct.EditionId = editionId;
        participantProduct.DivisionId = divisionId;
        participantProduct.CategoryId = categoryId;
        participantProduct.ProductId = productId;
        participantProduct.PharmaFormId = pharmaFormId;

        return participantProduct;
    }

    private void prepareGrid(short pagesize)
    {
        //if (pagesize > 0)
        //{
        //    this.grdProducts.AllowPaging = true;
        //    this.grdProducts.PageSize = pagesize;
        //    this.grdProducts.PageIndex = this.Session["CurrentPage"] == null ? 0 : Convert.ToInt32(this.Session["CurrentPage"]);
        //}
        //else
        //    this.grdProducts.AllowPaging = false;
    }

    private void preparedTableToAddProducts()
    {
        this.ddlPharmaFormToAdd.DataSource = AgroBusinessLogicComponent.CatalogsBLC.Instance.getPharmaForms("%");
        this.ddlPharmaFormToAdd.DataTextField = "Description";
        this.ddlPharmaFormToAdd.DataValueField = "PharmaFormId";
        this.ddlPharmaFormToAdd.DataBind();

        ListItem initialLine = new ListItem("SELECCIONAR", "-1", true);
        initialLine.Selected = true;
        this.ddlPharmaFormToAdd.Items.Insert(0, initialLine);
    }

    private void preparedTableToAddPharmaForms()
    {
        this.ddlProductToAdd.DataSource = AgroBusinessLogicComponent.ProductsBLC.Instance.getProductsByDivision(this._divisionId, this._countryId, this._bookId);
        this.ddlProductToAdd.DataTextField = "ProductName";
        this.ddlProductToAdd.DataValueField = "ProductId";
        this.ddlProductToAdd.DataBind();

        ListItem initialLine = new ListItem("SELECCIONAR", "-1", true);
        initialLine.Selected = true;
        this.ddlProductToAdd.Items.Insert(0, initialLine);
    }

    private void loadProducts()
    {

    }

    private void addProduct()
    {
        int pharmaFormId = Convert.ToInt32(this.ddlPharmaFormToAdd.SelectedValue);
        this.getProductsData();

        //Insert Product
        int productId = AgroBusinessLogicComponent.ProductsBLC.Instance.addProduct(this._ProductsInfo, this._userId, this._hashkey);

        //Insert ProductPharmaForm
        AgroBusinessEntries.ProductPharmaFormInfo productPharmaForm = new AgroBusinessEntries.ProductPharmaFormInfo();
        productPharmaForm.ProductId = productId;
        productPharmaForm.PharmaFormId = pharmaFormId;
        productPharmaForm.Active = true;
        AgroBusinessLogicComponent.ProductPharmaFormsBLC.Instance.addPharmaForm(productPharmaForm, this._userId, this._hashkey);

        //Insert DivisionCategories
        AgroBusinessEntries.DivisionCategoryInfo divisionCategory =
            AgroBusinessLogicComponent.DivisionCategoriesBLC.Instance.getDivisionCategory(this._divisionId, Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["DefaultCategory"]));
        if (divisionCategory == null)
        {
            divisionCategory = new AgroBusinessEntries.DivisionCategoryInfo();
            divisionCategory.DivisionId = this._divisionId;
            divisionCategory.CategoryId = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["DefaultCategory"]);
            AgroBusinessLogicComponent.DivisionCategoriesBLC.Instance.addDivisionCategory(divisionCategory, this._userId, this._hashkey);
        }

        //Insert ProductCategories
        AgroBusinessEntries.ProductCategoriesInfo productCategory = new AgroBusinessEntries.ProductCategoriesInfo();
        productCategory.DivisionId = this._divisionId;
        productCategory.CategoryId = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["DefaultCategory"]);
        productCategory.ProductId = productId;
        productCategory.PharmaFormId = pharmaFormId;
        productCategory.ProductDescription = null;
        productCategory.TechnicalSheet = false;
        AgroBusinessLogicComponent.ProductCategoriesBLC.Instance.addProductToCategory(productCategory, this._userId, this._hashkey);

        //Insert ParticipantProducts
        AgroBusinessEntries.ParticipantProductsInfo participantProduct = new AgroBusinessEntries.ParticipantProductsInfo();
        participantProduct.EditionId = this._editionId;
        participantProduct.DivisionId = this._divisionId;
        participantProduct.CategoryId = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["DefaultCategory"]);
        participantProduct.ProductId = productId;
        participantProduct.PharmaFormId = pharmaFormId;
        participantProduct.HTMLContent = null;
        participantProduct.XMLContent = null;
        participantProduct.Page = null;
        participantProduct.ModifiedContent = null;
        AgroBusinessLogicComponent.ParticipantProductsBLC.Instance.addToBook(participantProduct, this._userId, this._hashkey);

        //Insert NewProducts
        AgroBusinessEntries.NewProductInfo newProduct = new AgroBusinessEntries.NewProductInfo();
        newProduct.EditionId = this._editionId;
        newProduct.DivisionId = this._divisionId;
        newProduct.CategoryId = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["DefaultCategory"]);
        newProduct.ProductId = productId;
        newProduct.PharmaFormId = pharmaFormId;
        AgroBusinessLogicComponent.NewProductsBLC.Instance.addNew(newProduct, this._userId, this._hashkey);

        //Insert EditionDivisionProducts
        AgroBusinessEntries.EditionDivisionProductInfo editionDivisionProduct = new AgroBusinessEntries.EditionDivisionProductInfo();
        editionDivisionProduct.EditionId = this._editionId;
        editionDivisionProduct.DivisionId = this._divisionId;
        editionDivisionProduct.CategoryId = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["DefaultCategory"]);
        editionDivisionProduct.ProductId = productId;
        editionDivisionProduct.PharmaFormId = pharmaFormId;
        AgroBusinessLogicComponent.EditionDivisionProductsBLC.Instance.addDivision(editionDivisionProduct, this._userId, this._hashkey);
    }

    private void loadLaboratories()
    {
        //    this.ddlDivisions.DataSource = AgroBusinessLogicComponent.CatalogsBLC.Instance.getDivisionsByCountry(this._countryId);
        //    this.ddlDivisions.DataTextField = "ShortName";
        //    this.ddlDivisions.DataValueField = "DivisionId";
        //    this.ddlDivisions.DataBind();

        //    // Add the initial value:
        //    ListItem initialLine = new ListItem("SELECCIONAR", "-1", true);
        //    initialLine.Selected = true;
        //    this.ddlDivisions.Items.Insert(0, initialLine);

        //    //this.ddlDivisions.SelectedValue = this._divisionId.ToString();
        //    this.Session["divisionName"] = this._divisionId == -1 ? null : this.ddlDivisions.SelectedItem.Text;
    }
    private void addPharmaFormToProduct()
    {
        int productId = Convert.ToInt32(this.ddlProductToAdd.SelectedValue);
        int pharmaFormId = Convert.ToInt32(this.ddlNewPharmaFormToAdd.SelectedValue);

        //Insert ProductPharmaForm
        AgroBusinessEntries.ProductPharmaFormInfo productPharmaForm = AgroBusinessLogicComponent.ProductPharmaFormsBLC.Instance.getProductPF(productId, pharmaFormId);
        if (productPharmaForm == null)
        {
            productPharmaForm = new AgroBusinessEntries.ProductPharmaFormInfo();
            productPharmaForm.ProductId = productId;
            productPharmaForm.PharmaFormId = pharmaFormId;
            productPharmaForm.Active = true;
            AgroBusinessLogicComponent.ProductPharmaFormsBLC.Instance.addPharmaForm(productPharmaForm, this._userId, this._hashkey);
        }

        //Insert ProductCategories
        AgroBusinessEntries.ProductCategoriesInfo productCategory =
            AgroBusinessLogicComponent.ProductCategoriesBLC.Instance.getCategory(productId, pharmaFormId, this._divisionId, Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["DefaultCategory"]));
        if (productCategory == null)
        {
            productCategory = new AgroBusinessEntries.ProductCategoriesInfo();
            productCategory.DivisionId = this._divisionId;
            productCategory.CategoryId = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["DefaultCategory"]);
            productCategory.ProductId = productId;
            productCategory.PharmaFormId = pharmaFormId;
            productCategory.ProductDescription = null;
            productCategory.TechnicalSheet = false;
            AgroBusinessLogicComponent.ProductCategoriesBLC.Instance.addProductToCategory(productCategory, this._userId, this._hashkey);
        }

        //Insert ParticipantProducts
        AgroBusinessEntries.ParticipantProductsInfo participantProduct =
            AgroBusinessLogicComponent.ParticipantProductsBLC.Instance.getParticipantProduct(productId, this._editionId, pharmaFormId, this._divisionId, Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["DefaultCategory"]));
        if (participantProduct == null)
        {
            participantProduct = new AgroBusinessEntries.ParticipantProductsInfo();
            participantProduct.EditionId = this._editionId;
            participantProduct.DivisionId = this._divisionId;
            participantProduct.CategoryId = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["DefaultCategory"]);
            participantProduct.ProductId = productId;
            participantProduct.PharmaFormId = pharmaFormId;
            participantProduct.HTMLContent = null;
            participantProduct.XMLContent = null;
            participantProduct.Page = null;
            participantProduct.ModifiedContent = null;
            AgroBusinessLogicComponent.ParticipantProductsBLC.Instance.addToBook(participantProduct, this._userId, this._hashkey);
        }

        //Insert NewProducts
        AgroBusinessEntries.NewProductInfo newProduct =
            AgroBusinessLogicComponent.NewProductsBLC.Instance.getParticipantProduct(productId, this._editionId, pharmaFormId, this._divisionId, Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["DefaultCategory"]));
        if (newProduct == null)
        {
            newProduct = new AgroBusinessEntries.NewProductInfo();
            newProduct.EditionId = this._editionId;
            newProduct.DivisionId = this._divisionId;
            newProduct.CategoryId = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["DefaultCategory"]);
            newProduct.ProductId = productId;
            newProduct.PharmaFormId = pharmaFormId;
            AgroBusinessLogicComponent.NewProductsBLC.Instance.addNew(newProduct, this._userId, this._hashkey);
        }

        //Insert EditionDivisionProducts
        AgroBusinessEntries.EditionDivisionProductInfo editionDivisionProduct =
            AgroBusinessLogicComponent.EditionDivisionProductsBLC.Instance.getDivision(this._editionId, this._divisionId, Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["DefaultCategory"]), productId, pharmaFormId);
        if (editionDivisionProduct == null)
        {
            editionDivisionProduct = new AgroBusinessEntries.EditionDivisionProductInfo();
            editionDivisionProduct.EditionId = this._editionId;
            editionDivisionProduct.DivisionId = this._divisionId;
            editionDivisionProduct.CategoryId = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["DefaultCategory"]);
            editionDivisionProduct.ProductId = productId;
            editionDivisionProduct.PharmaFormId = pharmaFormId;
            AgroBusinessLogicComponent.EditionDivisionProductsBLC.Instance.addDivision(editionDivisionProduct, this._userId, this._hashkey);
        }
    }

    private void addOrRemoveEdition(int productId, int pharmaFormId, int divisionId, int categoryId, CheckBox chkParticipant)
    {
        AgroBusinessEntries.EditionDivisionProductInfo currentEdProduct =
            AgroBusinessLogicComponent.EditionDivisionProductsBLC.Instance.getDivision(this._editionId, divisionId, categoryId, productId, pharmaFormId);

        if (currentEdProduct == null)
        {
            if (chkParticipant.Checked == true)
            {
                currentEdProduct = new AgroBusinessEntries.EditionDivisionProductInfo();
                currentEdProduct.EditionId = this._editionId;
                currentEdProduct.DivisionId = divisionId;
                currentEdProduct.CategoryId = categoryId;
                currentEdProduct.PharmaFormId = pharmaFormId;
                currentEdProduct.ProductId = productId;
                AgroBusinessLogicComponent.EditionDivisionProductsBLC.Instance.addDivision(currentEdProduct, this._userId, this._hashkey);
            }
        }
        else
            if (chkParticipant.Checked == false)
                AgroBusinessLogicComponent.EditionDivisionProductsBLC.Instance.removeDivision(currentEdProduct, this._userId, this._hashkey);
    }

    private void addOrRemoveNewProduct(int productId, int pharmaFormId, int divisionId, int categoryId, CheckBox chkNewProduct, CheckBox chkParticipant, CheckBox chkMentionated)
    {
        AgroBusinessEntries.NewProductInfo currentNewProduct =
            AgroBusinessLogicComponent.NewProductsBLC.Instance.getParticipantProduct(productId, this._editionId, pharmaFormId, divisionId, categoryId);

        if (currentNewProduct == null)
        {
            if ((chkParticipant.Checked == true || chkMentionated.Checked == true) && chkNewProduct.Checked == true)
            {
                currentNewProduct = new AgroBusinessEntries.NewProductInfo();
                currentNewProduct.EditionId = this._editionId;
                currentNewProduct.DivisionId = divisionId;
                currentNewProduct.CategoryId = categoryId;
                currentNewProduct.PharmaFormId = pharmaFormId;
                currentNewProduct.ProductId = productId;
                AgroBusinessLogicComponent.NewProductsBLC.Instance.addNew(currentNewProduct, this._userId, this._hashkey);
            }
        }
        else
            if ((chkParticipant.Checked == false || chkMentionated.Checked == false) || chkNewProduct.Checked == false)
                AgroBusinessLogicComponent.NewProductsBLC.Instance.removeNew(currentNewProduct, this._userId, this._hashkey);
    }



    private void exportPdf()
    {

        DevExpress.XtraPrinting.PdfExportOptions pdfExport = new DevExpress.XtraPrinting.PdfExportOptions();
        pdfExport.DocumentOptions.Title = "Participant Products";
        pdfExport.DocumentOptions.Author = "Agro 2.0";
        _date = DateTime.Now.ToString();
        List<string> titles = new List<string>();
        titles.Add("Listado de Productos Participantes");
        titles.Add("Obra : " + _bookName);
        titles.Add("Edición : " + _editionName);
        titles.Add("Laboratorio: " + _divisionName);
        titles.Add("Usuario: " + AgroBusinessLogicComponent.FilesBLC.Instance.getUserName(_userId));
        titles.Add("Fecha: " + _date);

        System.Data.DataTable dtProds = new System.Data.DataTable();
        dtProds.Columns.Add("Categoria");
        dtProds.Columns.Add("Producto");
        dtProds.Columns.Add("Forma farmaceutica");
        dtProds.Columns.Add("Descripción");
        string proNameToSearch = null;
        List<AgroBusinessEntries.ProductsToEditInfo> products = AgroBusinessLogicComponent.ProductsBLC.Instance.getEditableProductsByDivision(this._divisionId, this._countryId, this._editionId, this._bookId, proNameToSearch);
        foreach (AgroBusinessEntries.ProductsToEditInfo prod in products)
        {
            if (prod.Participant)
            {
                dtProds.Rows.Add(prod.CategoryName, prod.Brand, prod.PharmaForm, prod.ProductDescription);
            }
        }
        AgroBusinessEntries.VersionFileInfo VersionFile = AgroBusinessLogicComponent.FilesBLC.Instance.getVersionFile(getPrefixApplication(), _editionId);
        string route = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["BaseURL"]);
        string fileName = "ProductosParticipantes_" + getPrefixApplication() + "_" + VersionFile.CurrentValue + ".pdf";
        string filePath = route + @"\" + fileName;
        createReport(dtProds, titles).ExportToPdf(filePath, pdfExport);
        AgroBusinessEntries.FileInfo fileInfo = new AgroBusinessEntries.FileInfo();
        fileInfo.BaseUrl = route;
        fileInfo.FileDate = DateTime.Parse(_date);
        fileInfo.FileName = fileName;
        fileInfo.FormatFileId = AgroBusinessLogicComponent.FilesBLC.Instance.getFormatFileId("pdf");
        fileInfo.NickName = _user;
        fileInfo.Version = getPrefixApplication() + "_" + (VersionFile.CurrentValue.ToString());
        fileInfo.VersionFileId = VersionFile.VersionFileId;
        AgroBusinessLogicComponent.FilesBLC.Instance.addFile(fileInfo, getPrefixApplication());
        filePath = filePath.Replace(@"\", "/");
        ClientScript.RegisterStartupScript(this.GetType(), "down", "downFile('" + filePath + "','" + fileInfo.FileName + "','application/vnd.ms-excel');", true);


    }
    private void exportXls()
    {

        DevExpress.XtraPrinting.XlsExportOptions xlsExport = new DevExpress.XtraPrinting.XlsExportOptions();
        xlsExport.SheetName = "Productos Particiapantes";
        _date = DateTime.Now.ToString();
        List<string> titles = new List<string>();
        titles.Add("Listado de Productos Participantes");
        titles.Add("Obra : " + _bookName);
        titles.Add("Edición : " + _editionName);
        titles.Add("Laboratorio: " + _divisionName);
        titles.Add("Usuario: " + AgroBusinessLogicComponent.FilesBLC.Instance.getUserName(_userId));
        titles.Add("Fecha: " + _date);
        bool modifiedcontent = false;
        System.Data.DataTable dtProds = new System.Data.DataTable();
        dtProds.Columns.Add("Categoria");
        dtProds.Columns.Add("Producto");
        dtProds.Columns.Add("Forma farmaceutica");
        dtProds.Columns.Add("Codigo de registro");
        dtProds.Columns.Add("Descripción");
        dtProds.Columns.Add("Nuevo");
        dtProds.Columns.Add("Con Cambios");
        dtProds.Columns.Add("ProductShot");

        string proNameToSearch = null;
        List<AgroBusinessEntries.ProductsToEditInfo> products = AgroBusinessLogicComponent.ProductsBLC.Instance.getEditableProductsByDivision(this._divisionId, this._countryId, this._editionId, this._bookId, proNameToSearch);

        foreach (AgroBusinessEntries.ProductsToEditInfo prod in products)
        {
            if (prod.Participant)
            {
                if (prod.ModifiedContent != null)
                    modifiedcontent = Convert.ToBoolean(prod.ModifiedContent);
                dtProds.Rows.Add(prod.CategoryName, prod.Brand, prod.PharmaForm, prod.ProductType, prod.ProductDescription, prod.ModifiedContent == null ? "Si" : "No", modifiedcontent ? "Si" : "No", prod.Sidef ? "Si" : "No");
            }
        }
        //AgroBusinessEntries.VersionFileInfo VersionFile = AgroBusinessLogicComponent.FilesBLC.Instance.getVersionFile(getPrefixApplication(), _editionId);
        string route = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["BaseURL"]);
        string fileName = "ProductosParticipantes_" + getPrefixApplication() + ".xls";
        string filePath = route + @"\" + fileName;
        MemoryStream ms = new MemoryStream();
        this.createReport(dtProds, titles).ExportToXls(ms);
        byte[] buffer = new byte[ms.Length];
        ms.Position = 0;
        ms.Read(buffer, 0, (int)ms.Length);
        ms.Close();
        this.Session["doc"] = buffer;
        //.ExportToXls(filePath,xlsExport);
        AgroBusinessEntries.FileInfo fileInfo = new AgroBusinessEntries.FileInfo();
        fileInfo.BaseUrl = route;
        fileInfo.FileDate = DateTime.Parse(_date);
        fileInfo.FileName = fileName;
        //  fileInfo.FormatFileId = AgroBusinessLogicComponent.FilesBLC.Instance.getFormatFileId("xls");
        fileInfo.NickName = _user;
        fileInfo.Version = getPrefixApplication();
        //fileInfo.VersionFileId = VersionFile.VersionFileId;
        //AgroBusinessLogicComponent.FilesBLC.Instance.addFile(fileInfo, getPrefixApplication());
        filePath = filePath.Replace(@"\", "/");
        ClientScript.RegisterStartupScript(this.GetType(), "down", "downFile('" + filePath + "','" + fileInfo.FileName + "','application/vnd.ms-excel');", true);

    }
    private DevExpress.XtraReports.UI.XtraReport createReport(DataTable data, List<string> titles)
    {
        System.Data.DataTable dataContent = data;
        Report report = new Report();
        ReportHeaderBand header = new ReportHeaderBand();
        XRTable tableHeader = getReportHeader(titles);
        header.Controls.Add(tableHeader);
        DetailReportBand rband = new DetailReportBand();
        DetailBand band = new DetailBand();
        XRTable table = new XRTable();
        table.Borders = DevExpress.XtraPrinting.BorderSide.All;
        table.SizeF = new System.Drawing.SizeF(650f, 10f);
        band.Controls.Add(table);
        XRTableRow tRow = new DevExpress.XtraReports.UI.XRTableRow();
        XRTableCell col = new DevExpress.XtraReports.UI.XRTableCell();
        foreach (System.Data.DataColumn colu in dataContent.Columns)
        {
            col = new DevExpress.XtraReports.UI.XRTableCell();
            col.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(8)))), ((int)(((byte)(1)))), ((int)(((byte)(41)))));
            col.ForeColor = Color.White;
            col.SizeF = new System.Drawing.SizeF(50F, 45F);
            col.Text = colu.ColumnName;
            col.Font = new Font("Tahoma", 10, FontStyle.Bold);
            tRow.Cells.Add(col);
        }
        table.Rows.Add(tRow);
        List<XRTableRow> ROWS = new List<XRTableRow>();
        XRTableRow tContentRow = new DevExpress.XtraReports.UI.XRTableRow();
        XRTableCell column = new DevExpress.XtraReports.UI.XRTableCell();
        column.Font = new Font("tahoma", 10, FontStyle.Regular);
        column.SizeF = new System.Drawing.SizeF(50F, 45F);
        for (int r = 0; r < dataContent.Rows.Count; r++)
        {
            tContentRow = new DevExpress.XtraReports.UI.XRTableRow();
            for (int c = 0; c < dataContent.Columns.Count; c++)
            {
                column = new DevExpress.XtraReports.UI.XRTableCell();
                column.Text = dataContent.Rows[r][c].ToString();
                tContentRow.Cells.Add(column);
            }
            ROWS.Add(tContentRow);

        }
        XRTableRow[] arr = new XRTableRow[ROWS.Count];
        for (int i = 0; i < ROWS.Count; i++)
        {
            arr[i] = ROWS[i];
        }
        table.Rows.AddRange(arr);
        rband.Bands.Add(band);
        report.Bands.Add(header);
        report.Bands.Add(rband);
        return report;
    }
    public XRTable getReportHeader(List<string> titles)
    {
        XRTable tableHeader = new XRTable();

        tableHeader.SizeF = new System.Drawing.SizeF(850f, 10f);

        foreach (string val in titles)
        {
            XRTableCell title = new DevExpress.XtraReports.UI.XRTableCell();
            title.Text = val;
            title.Font = new Font("Tahoma", 14, FontStyle.Bold);
            XRTableRow tRowHeader = new DevExpress.XtraReports.UI.XRTableRow();
            tRowHeader.Cells.AddRange(new XRTableCell[] { title });
            tableHeader.Rows.Add(tRowHeader);
        }
        return tableHeader;
    }
    private string getPrefixApplication()
    {
        switch (this._roleId)
        {
            case (int)Utilities.Roles.Vendedor:
                return "MV";
            case (int)Utilities.Roles.Administrador:
                return "MA";

            default:
                return "MA";
        }
    }
    #endregion

    #region Protected Methods

    protected string displayBrand(string brand)
    {
        return string.IsNullOrEmpty(brand) ? "" : brand;
    }

    protected void displayPharmaFormsToAdd(GridViewRowEventArgs e, int productId)
    {
        //if (e.Row.RowIndex == this.grdProducts.EditIndex)
        //{
        //    DropDownList ddlPharmaFormsToAdd = (DropDownList)e.Row.FindControl("ddlPharmaFormsToAdd");

        //    ddlPharmaFormsToAdd.DataSource = AgroBusinessLogicComponent.CatalogsBLC.Instance.getPharmaFormsWithoutProduct(productId);
        //    ddlPharmaFormsToAdd.DataTextField = "Description";
        //    ddlPharmaFormsToAdd.DataValueField = "PharmaFormId";
        //    ddlPharmaFormsToAdd.DataBind();

        //    // Add the initial value:
        //    ListItem initialLine = new ListItem("SELECCIONAR", "-1", true);
        //    initialLine.Selected = true;
        //    ddlPharmaFormsToAdd.Items.Insert(0, initialLine);
        //    ddlPharmaFormsToAdd.SelectedValue = "-1";
        //    ddlPharmaFormsToAdd.Enabled = true;
        //}
    }





    //protected void displayCountriesToEdit(GridViewRowEventArgs e, int countryId)
    //{
    //    if (e.Row.RowIndex == this.grdProducts.EditIndex)
    //    {
    //        CheckBoxList chkList = (CheckBoxList)e.Row.FindControl("chkCountries");
    //        AgroBusinessEntries.ProductsToEditInfo currentRow = (AgroBusinessEntries.ProductsToEditInfo)e.Row.DataItem;
    //        chkList.Attributes.Add("countries", currentRow.Countries);
    //        chkList.DataSource = AgroBusinessLogicComponent.CatalogsBLC.Instance.getCountryCodes(countryId);
    //        chkList.DataTextField = "CountryName";
    //        chkList.DataValueField = "CountryCodeId";
    //        chkList.DataBind();

    //        string[] countriesList = currentRow.Countries.Split(',');
    //        foreach (ListItem item in chkList.Items)
    //        {
    //            foreach (string val in countriesList)
    //            {
    //                if (val.Equals(item.Value))
    //                {
    //                    item.Selected = true;
    //                }
    //            }
    //        }

    //    }
    //}

    #endregion

    private int _editionId;
    private int _bookId;
    private int _countryId;
    private int _divisionId;
    private int _userId;
    private int _userCountry;
    private string _hashkey;
    private string _user;
    private int _roleId;
    private string _bookName;
    private string _editionName;
    private string _divisionName;
    private string _date;
    private string _bookShortName;
    private AgroBusinessEntries.ProductsInfo _ProductsInfo;
    private PLMCryptographyComponent.CryptographyComponent _cryptography = new PLMCryptographyComponent.CryptographyComponent();

}