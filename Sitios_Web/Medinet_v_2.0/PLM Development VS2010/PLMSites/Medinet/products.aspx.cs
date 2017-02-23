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
public partial class products : System.Web.UI.Page
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
        if (this._countryId!=3)
        {
            this.grdProducts.Columns[15].Visible = false;
        }
        if (!IsPostBack)
        {
            string urlByDay = "http://www.plmconnection.com/MedinetViewer/VendorReport.aspx?v=" + this._cryptography.encrypt(this._userId.ToString()) + "&e="
                + this._cryptography.encrypt(this._editionId.ToString()) + "&d=" + this._cryptography.encrypt(this._divisionId.ToString())
                + "&m=" + this._cryptography.encrypt(Convert.ToByte(Utilities.Module.Ventas).ToString());

            string urlGlobal = "http://www.plmconnection.com/MedinetViewer/VendorReport.aspx?v=" + this._cryptography.encrypt(this._userId.ToString()) + "&e="
                + this._cryptography.encrypt(this._editionId.ToString()) + "&d=" + this._cryptography.encrypt(this._divisionId.ToString())
                + "&m=" + this._cryptography.encrypt(Convert.ToByte(Utilities.Module.Ventas).ToString()) + "&t=" + this._cryptography.encrypt(Convert.ToByte(Utilities.ReportVendorType.Global).ToString());

            this.btnViewReport.Attributes.Add("onClick", "javascript:openReport('" + urlByDay + "')");
            this.btnGlobalReport.Attributes.Add("onClick", "javascript:openReport('" + urlGlobal + "')");

            this.prepareGrid(Convert.ToInt16(this.ddlPageSize.SelectedValue));
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

    protected void grdProducts_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            MedinetBusinessEntries.ProductsToEditInfo currentRow = (MedinetBusinessEntries.ProductsToEditInfo)e.Row.DataItem;
            

            //Checkboxs
            CheckBox chk1 = (CheckBox)e.Row.FindControl("Participate");
            CheckBox chk2 = (CheckBox)e.Row.FindControl("NewProduct");
            CheckBox chk3 = (CheckBox)e.Row.FindControl("ModifiedContent");
            CheckBox chk4 = (CheckBox)e.Row.FindControl("Sidef");

            DropDownList ddlFamilyProducts = (DropDownList)e.Row.FindControl("ddlFamilyProducts");
            DropDownList ddlFamilyProductShots = (DropDownList)e.Row.FindControl("ddlFamilyProductShots");

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

                //if (PLMUsersBusinessLogicComponent.ApplicationUsersBLC.Instance.getRole(this._userId, this._hashkey).RoleId != (int)Utilities.Roles.Administrador && (chk1.Checked == true || chk2.Checked == true))
                //{
                //    chk1.Enabled = false;
                //    chk2.Enabled = false;
                //    chk3.Enabled = false;
                //    chk4.Enabled = false;
                //}

                //New Product
                chk2.Attributes.Add("productId", currentRow.ProductId.ToString());
                chk2.Attributes.Add("pharmaFormId", currentRow.PharmaFormId.ToString());
                chk2.Checked = currentRow.NewProduct;
                chk2.Attributes.Add("onclick", "javascript:checkMutuallyExclusive('" + chk3.ClientID + "')");

                //Modified content
                chk3.Attributes.Add("productId", currentRow.ProductId.ToString());
                chk3.Attributes.Add("pharmaFormId", currentRow.PharmaFormId.ToString());
                chk3.Checked = currentRow.ModifiedContent;
                chk3.Attributes.Add("onclick", "javascript:checkMutuallyExclusive('" + chk2.ClientID + "')");

                //SIDEF
                chk4.Attributes.Add("productId", currentRow.ProductId.ToString());
                chk4.Attributes.Add("pharmaFormId", currentRow.PharmaFormId.ToString());
                chk4.Checked = currentRow.Sidef;
            }

            //ContentFamilyString
            this.displayContentFamily(e, currentRow.DivisionId, currentRow.ContentFamilyId);

            //PSFamilyString
            this.displayPSFamily(e, currentRow.DivisionId, currentRow.PSFamilyId);

            //Countries
            if (this._countryId==3)
            {
                this.displayCountriesToEdit(e, this._countryId);    
            }
            
        }
    }

    
    protected void grdProducts_RowEditing(object sender, GridViewEditEventArgs e)
    {
        this.grdProducts.EditIndex = e.NewEditIndex;

        this.getEditableProducts();
    }

    protected void grdProducts_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        CheckBox chkParticipant;
        CheckBox chkNewProduct;
        CheckBox chkModified;
        CheckBox chkSidef;

        MedinetBusinessEntries.ParticipantProductsInfo currentParticipant;
        MedinetBusinessEntries.EditionProductShotsInfo productShot;

        int productId = Convert.ToInt32(this.grdProducts.DataKeys[e.RowIndex].Values[0].ToString());
        int pharmaFormId = Convert.ToInt32(this.grdProducts.DataKeys[e.RowIndex].Values[1].ToString());
        int divisionId = Convert.ToInt32(this.grdProducts.DataKeys[e.RowIndex].Values[2].ToString());
        int categoryId = Convert.ToInt32(this.grdProducts.DataKeys[e.RowIndex].Values[3].ToString());

        DropDownList ddlFamilyProduct = (DropDownList)this.grdProducts.Rows[e.RowIndex].FindControl("ddlContentFamilyString");
        DropDownList ddlFamilyProductShot = (DropDownList)this.grdProducts.Rows[e.RowIndex].FindControl("ddlPSFamilyString");
        TextBox txtBrand = (TextBox)this.grdProducts.Rows[e.RowIndex].FindControl("txtBrand");
        CheckBoxList chkCountries = (CheckBoxList)this.grdProducts.Rows[e.RowIndex].FindControl("chkCountries");
        //Countries
        if (this._countryId == 3)
        {
            string countriesList = chkCountries.Attributes["countries"];
            foreach (ListItem val in chkCountries.Items)
            {
                if (val.Selected)
                {
                    if (!(countriesList.Contains(val.Value)))
                    {
                        MedinetBusinessEntries.CountryProductInfo cc = new MedinetBusinessEntries.CountryProductInfo();
                        cc.CountryCodeId = Convert.ToByte(val.Value);
                        cc.PharmaFormId = pharmaFormId;
                        cc.ProductId = productId;
                        MedinetBusinessLogicComponent.CountryProductsBLC.Instance.addToCountry(cc, this._userId, this._hashkey);
                    }
                }
                else
                {
                    if (countriesList.Contains(val.Value))
                    {
                        MedinetBusinessEntries.CountryProductInfo cc = new MedinetBusinessEntries.CountryProductInfo();
                        cc.CountryCodeId = Convert.ToByte(val.Value);
                        cc.PharmaFormId = pharmaFormId;
                        cc.ProductId = productId;
                        MedinetBusinessLogicComponent.CountryProductsBLC.Instance.removeToCountry(cc, this._userId, this._hashkey);
                    }
                }
            }
        }
        //Brand
        MedinetBusinessEntries.ProductsInfo product = MedinetBusinessLogicComponent.ProductsBLC.Instance.getProductById(productId);
        if (txtBrand.Text.Trim() == "")
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "messageScript", "jAlert('Debe ingresar una marca.', 'Marca Inválida');", true);
            return;
        }
        product.Brand = txtBrand.Text.Trim().ToUpper();
        MedinetBusinessLogicComponent.ProductsBLC.Instance.updateProduct(product, this._userId, this._hashkey);

        //PharmaceuticalForms
        MedinetBusinessEntries.ProductPharmaFormInfo productPharmaForm = MedinetBusinessLogicComponent.ProductPharmaFormsBLC.Instance.getProductPF(productId, pharmaFormId);
        if (productPharmaForm == null)
        {
            productPharmaForm = new MedinetBusinessEntries.ProductPharmaFormInfo();
            productPharmaForm.ProductId = productId;
            productPharmaForm.PharmaFormId = pharmaFormId;
            productPharmaForm.Active = true;
            MedinetBusinessLogicComponent.ProductPharmaFormsBLC.Instance.addPharmaForm(productPharmaForm, this._userId, this._hashkey);
        }

        //ProductCategories
        MedinetBusinessEntries.ProductCategoriesInfo productCategory = MedinetBusinessLogicComponent.ProductCategoriesBLC.Instance.getCategory(productId,
             pharmaFormId, divisionId, categoryId);
        if (productCategory == null)
        {   
            productCategory = new MedinetBusinessEntries.ProductCategoriesInfo();
            productCategory.ProductId = productId;
            productCategory.PharmaFormId = pharmaFormId;
            productCategory.DivisionId = divisionId;
            productCategory.CategoryId = categoryId;
            MedinetBusinessLogicComponent.ProductCategoriesBLC.Instance.addProductToCategory(productCategory, this._userId, this._hashkey);
        }
        
        //Participant Product
        chkParticipant = (CheckBox)this.grdProducts.Rows[e.RowIndex].FindControl("Participate");

        //New Product
        chkNewProduct = (CheckBox)this.grdProducts.Rows[e.RowIndex].FindControl("NewProduct");

        //Modified Product
        chkModified = (CheckBox)this.grdProducts.Rows[e.RowIndex].FindControl("ModifiedContent");

        //SIDEF
        chkSidef = (CheckBox)this.grdProducts.Rows[e.RowIndex].FindControl("Sidef");

        if (divisionId > 0 && categoryId > 0)
        {
            currentParticipant = MedinetBusinessLogicComponent.ParticipantProductsBLC.Instance.getParticipantProduct(productId,
                this._editionId, pharmaFormId, divisionId, categoryId);

            if (currentParticipant == null)
            {
                if (chkParticipant.Checked == true)
                {
                    //Add ParticipantProduct
                    currentParticipant = this.getParticipantProduct(this._editionId, divisionId, categoryId, productId, pharmaFormId);
                    MedinetBusinessLogicComponent.ParticipantProductsBLC.Instance.addToBook(currentParticipant, this._userId, this._hashkey);

                    //Update field ModifiedContent
                    currentParticipant.ModifiedContent = chkModified.Checked;
                    MedinetBusinessLogicComponent.ParticipantProductsBLC.Instance.modifiedContent(currentParticipant, this._userId, this._hashkey);

                    //Verifies if the product is new or is not
                    this.addOrRemoveNewProduct(productId, pharmaFormId, divisionId, categoryId, chkNewProduct, chkParticipant);

                    //SIDEF
                    if (chkSidef.Checked == true)
                    {
                        productShot = this.getEditionProductShot(this._editionId, divisionId, categoryId, productId, pharmaFormId);
                        MedinetBusinessLogicComponent.EditionProductShotsBLC.Instance.addProductShot(productShot, this._userId, this._hashkey);
                    }

                    //FamilyProduct
                    if (ddlFamilyProduct.SelectedValue != "-1")
                    {
                        MedinetBusinessEntries.FamilyProductInfo familyProduct =
                            MedinetBusinessLogicComponent.FamilyProductsBLC.Instance.getFamilyProduct(this._editionId, divisionId, categoryId, productId, pharmaFormId);

                        if (familyProduct != null)
                            MedinetBusinessLogicComponent.FamilyProductsBLC.Instance.removeFamilyToProduct(familyProduct, this._userId, this._hashkey);

                        familyProduct =
                            this.getFamilyProduct(this._editionId, Convert.ToInt32(ddlFamilyProduct.SelectedValue), divisionId, categoryId, productId, pharmaFormId);
                        MedinetBusinessLogicComponent.FamilyProductsBLC.Instance.addFamilyToProduct(familyProduct, this._userId, this._hashkey);
                    }
                    else
                    {
                        MedinetBusinessEntries.FamilyProductInfo familyProduct =
                            MedinetBusinessLogicComponent.FamilyProductsBLC.Instance.getFamilyProduct(this._editionId, divisionId, categoryId, productId, pharmaFormId);

                        if (familyProduct != null)
                            MedinetBusinessLogicComponent.FamilyProductsBLC.Instance.removeFamilyToProduct(familyProduct, this._userId, this._hashkey);
                    }

                    //FamilyProductShot
                    if (ddlFamilyProductShot.SelectedValue != "-1")
                    {
                        MedinetBusinessEntries.FamilyProductShotInfo familyProductShot =
                            MedinetBusinessLogicComponent.FamilyProductShotsBLC.Instance.getFamilyProductShot(this._editionId, divisionId, categoryId, productId, pharmaFormId);

                        if (familyProductShot != null)
                        {
                            //Delete the previous record
                            MedinetBusinessLogicComponent.FamilyProductShotsBLC.Instance.removeFamilyToProductShot(familyProductShot, this._userId, this._hashkey);

                            //Insert the new FamilyProductShot
                            familyProductShot.FamilyId = Convert.ToInt32(ddlFamilyProductShot.SelectedValue);
                            MedinetBusinessLogicComponent.FamilyProductShotsBLC.Instance.addFamilyToProductShot(familyProductShot, this._userId, this._hashkey);
                        }
                        else
                        {
                            familyProductShot = new MedinetBusinessEntries.FamilyProductShotInfo();
                            List<MedinetBusinessEntries.EditionProductShotsInfo> editionProductShot =
                                MedinetBusinessLogicComponent.EditionProductShotsBLC.Instance.getProductShots(this._editionId, divisionId, categoryId, productId, pharmaFormId);

                            if (editionProductShot.Count() > 0)
                            {
                                familyProductShot.EditionProductShotId = editionProductShot[0].EditionProductShotId;
                                familyProductShot.FamilyId = Convert.ToInt32(ddlFamilyProductShot.SelectedValue);
                                MedinetBusinessLogicComponent.FamilyProductShotsBLC.Instance.addFamilyToProductShot(familyProductShot, this._userId, this._hashkey);
                            }
                            else
                            {
                                productShot = this.getEditionProductShot(this._editionId, divisionId, categoryId, productId, pharmaFormId);
                                int editionProductShotId = MedinetBusinessLogicComponent.EditionProductShotsBLC.Instance.addProductShot(productShot, this._userId, this._hashkey);

                                familyProductShot.EditionProductShotId = editionProductShotId;
                                familyProductShot.FamilyId = Convert.ToInt32(ddlFamilyProductShot.SelectedValue);
                                MedinetBusinessLogicComponent.FamilyProductShotsBLC.Instance.addFamilyToProductShot(familyProductShot, this._userId, this._hashkey);
                            }
                        }
                    }
                    else
                    {
                        MedinetBusinessEntries.FamilyProductShotInfo familyProductShot =
                            MedinetBusinessLogicComponent.FamilyProductShotsBLC.Instance.getFamilyProductShot(this._editionId, divisionId, categoryId, productId, pharmaFormId);

                        if (familyProductShot != null)
                            MedinetBusinessLogicComponent.FamilyProductShotsBLC.Instance.removeFamilyToProductShot(familyProductShot, this._userId, this._hashkey);
                    }
                }
                else
                {
                    MedinetBusinessLogicComponent.EditionProductShotsBLC.Instance.removeProductShotsByProduct(this._editionId, divisionId,
                            categoryId, productId, pharmaFormId, this._userId, this._hashkey);
                    this.addOrRemoveNewProduct(productId, pharmaFormId, divisionId, categoryId, chkNewProduct, chkParticipant);
                }
            }
            else
            {
                if (chkParticipant.Checked == true)
                {
                    //Update field ModifiedContent
                    currentParticipant.ModifiedContent = chkModified.Checked;
                    MedinetBusinessLogicComponent.ParticipantProductsBLC.Instance.modifiedContent(currentParticipant, this._userId, this._hashkey);

                    //Erase the modified Attribute group
                    if (chkModified.Checked == false)
                    {
                        List<MedinetBusinessEntries.ModifiedAttributeGroupsInfo> modifiedAttributeGroups =
                            MedinetBusinessLogicComponent.ModifiedAttributeGroupsBLC.Instance.getModifiedAttributeGroupsByProduct(this._editionId, divisionId, categoryId, pharmaFormId, productId);
                        
                        if (modifiedAttributeGroups.Count > 0)
                        {
                            foreach (MedinetBusinessEntries.ModifiedAttributeGroupsInfo attributeGroupItem in modifiedAttributeGroups)
                                MedinetBusinessLogicComponent.ModifiedAttributeGroupsBLC.Instance.removeProductToAttributeGroup(attributeGroupItem, this._userId, this._hashkey);
                        }
                    }

                    //Verifies if the product is new or not
                    this.addOrRemoveNewProduct(productId, pharmaFormId, divisionId, categoryId, chkNewProduct, chkParticipant);

                    if (chkSidef.Checked == true)
                    {
                        if (MedinetBusinessLogicComponent.EditionProductShotsBLC.Instance.getProductShots(this._editionId, divisionId, categoryId, productId, pharmaFormId).Count() < 1)
                        {
                            productShot = this.getEditionProductShot(this._editionId, divisionId, categoryId, productId, pharmaFormId);
                            MedinetBusinessLogicComponent.EditionProductShotsBLC.Instance.addProductShot(productShot, this._userId, this._hashkey);
                        }
                    }
                    else
                    {
                        MedinetBusinessLogicComponent.EditionProductShotsBLC.Instance.removeProductShotsByProduct(this._editionId, divisionId,
                            categoryId, productId, pharmaFormId, this._userId, this._hashkey);
                    }

                    //FamilyProduct
                    if (ddlFamilyProduct.SelectedValue != "-1")
                    {
                        MedinetBusinessEntries.FamilyProductInfo familyProduct =
                            MedinetBusinessLogicComponent.FamilyProductsBLC.Instance.getFamilyProduct(this._editionId, divisionId, categoryId, productId, pharmaFormId);

                        if (familyProduct != null)
                            MedinetBusinessLogicComponent.FamilyProductsBLC.Instance.removeFamilyToProduct(familyProduct, this._userId, this._hashkey);

                        familyProduct =
                            this.getFamilyProduct(this._editionId, Convert.ToInt32(ddlFamilyProduct.SelectedValue), divisionId, categoryId, productId, pharmaFormId);
                        MedinetBusinessLogicComponent.FamilyProductsBLC.Instance.addFamilyToProduct(familyProduct, this._userId, this._hashkey);
                    }
                    else
                    {
                        MedinetBusinessEntries.FamilyProductInfo familyProduct =
                            MedinetBusinessLogicComponent.FamilyProductsBLC.Instance.getFamilyProduct(this._editionId, divisionId, categoryId, productId, pharmaFormId);

                        if (familyProduct != null)
                            MedinetBusinessLogicComponent.FamilyProductsBLC.Instance.removeFamilyToProduct(familyProduct, this._userId, this._hashkey);
                    }

                    //FamilyProductShot
                    if (ddlFamilyProductShot.SelectedValue != "-1")
                    {
                        MedinetBusinessEntries.FamilyProductShotInfo familyProductShot =
                            MedinetBusinessLogicComponent.FamilyProductShotsBLC.Instance.getFamilyProductShot(this._editionId, divisionId, categoryId, productId, pharmaFormId);

                        if (familyProductShot != null)
                        {
                            //Delete the previous record
                            MedinetBusinessLogicComponent.FamilyProductShotsBLC.Instance.removeFamilyToProductShot(familyProductShot, this._userId, this._hashkey);

                            //Insert the new FamilyProductShot
                            familyProductShot.FamilyId = Convert.ToInt32(ddlFamilyProductShot.SelectedValue);
                            MedinetBusinessLogicComponent.FamilyProductShotsBLC.Instance.addFamilyToProductShot(familyProductShot, this._userId, this._hashkey);
                        }
                        else
                        {
                            familyProductShot = new MedinetBusinessEntries.FamilyProductShotInfo();
                            List<MedinetBusinessEntries.EditionProductShotsInfo> editionProductShot =
                                MedinetBusinessLogicComponent.EditionProductShotsBLC.Instance.getProductShots(this._editionId, divisionId, categoryId, productId, pharmaFormId);

                            if (editionProductShot.Count() > 0)
                            {
                                familyProductShot.EditionProductShotId = editionProductShot[0].EditionProductShotId;
                                familyProductShot.FamilyId = Convert.ToInt32(ddlFamilyProductShot.SelectedValue);
                                MedinetBusinessLogicComponent.FamilyProductShotsBLC.Instance.addFamilyToProductShot(familyProductShot, this._userId, this._hashkey);
                            }
                            else
                            {
                                productShot = this.getEditionProductShot(this._editionId, divisionId, categoryId, productId, pharmaFormId);
                                int editionProductShotId = MedinetBusinessLogicComponent.EditionProductShotsBLC.Instance.addProductShot(productShot, this._userId, this._hashkey);

                                familyProductShot.EditionProductShotId = editionProductShotId;
                                familyProductShot.FamilyId = Convert.ToInt32(ddlFamilyProductShot.SelectedValue);
                                MedinetBusinessLogicComponent.FamilyProductShotsBLC.Instance.addFamilyToProductShot(familyProductShot, this._userId, this._hashkey);
                            }
                        }
                    }
                    else
                    {
                        MedinetBusinessEntries.FamilyProductShotInfo familyProductShot =
                            MedinetBusinessLogicComponent.FamilyProductShotsBLC.Instance.getFamilyProductShot(this._editionId, divisionId, categoryId, productId, pharmaFormId);

                        if (familyProductShot != null)
                            MedinetBusinessLogicComponent.FamilyProductShotsBLC.Instance.removeFamilyToProductShot(familyProductShot, this._userId, this._hashkey);
                    }
                }
                else
                {
                    //FamilyProduct
                    MedinetBusinessEntries.FamilyProductInfo eraseFamilyProduct =
                        MedinetBusinessLogicComponent.FamilyProductsBLC.Instance.getFamilyProduct(this._editionId, divisionId, categoryId, productId, pharmaFormId);
                    if (eraseFamilyProduct != null)
                        MedinetBusinessLogicComponent.FamilyProductsBLC.Instance.removeFamilyToProduct(eraseFamilyProduct, this._userId, this._hashkey);

                    MedinetBusinessLogicComponent.EditionProductShotsBLC.Instance.removeProductShotsByProduct(this._editionId, divisionId,
                            categoryId, productId, pharmaFormId, this._userId, this._hashkey);

                    //Erase the modified Attribute group
                    List<MedinetBusinessEntries.ModifiedAttributeGroupsInfo> modifiedAttributeGroups =
                            MedinetBusinessLogicComponent.ModifiedAttributeGroupsBLC.Instance.getModifiedAttributeGroupsByProduct(this._editionId, divisionId, categoryId, pharmaFormId, productId);

                    if (modifiedAttributeGroups.Count > 0)
                    {
                        foreach (MedinetBusinessEntries.ModifiedAttributeGroupsInfo attributeGroupItem in modifiedAttributeGroups)
                            MedinetBusinessLogicComponent.ModifiedAttributeGroupsBLC.Instance.removeProductToAttributeGroup(attributeGroupItem, this._userId, this._hashkey);
                    }

                    MedinetBusinessLogicComponent.ParticipantProductsBLC.Instance.removeToBook(currentParticipant, this._userId, this._hashkey);
                    this.addOrRemoveNewProduct(productId, pharmaFormId, divisionId, categoryId, chkNewProduct, chkParticipant);
                }
            }
            this.addOrRemoveEdition(productId, pharmaFormId, divisionId, categoryId, chkParticipant);
        }
        this.grdProducts.EditIndex = -1;
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
        int productId = Convert.ToInt32(this.grdProducts.DataKeys[this.grdProducts.EditIndex].Values[0].ToString());
        int pharmaFormId = Convert.ToInt32(this.grdProducts.DataKeys[this.grdProducts.EditIndex].Values[1].ToString());
        int divisionId = Convert.ToInt32(this.grdProducts.DataKeys[this.grdProducts.EditIndex].Values[2].ToString());
        int categoryId = Convert.ToInt32(this.grdProducts.DataKeys[this.grdProducts.EditIndex].Values[3].ToString());
       
        DropDownList ddlContentFamily = (DropDownList)sender;

        if (ddlContentFamily.SelectedValue == "-2")
        {
            //FamilyPrefix
            MedinetBusinessEntries.FamilyPrefixInfo familyPrefixInfo = MedinetBusinessLogicComponent.FamilyPrefixesBLC.Instance.getFamilyPrefixByEdition(this._editionId,
                (byte)PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.PrefixTypes.Contenido);
            
            if (familyPrefixInfo != null)
            {
                //ParticipantProducts
                MedinetBusinessEntries.ParticipantProductsInfo currentParticipant = MedinetBusinessLogicComponent.ParticipantProductsBLC.Instance.getParticipantProduct(productId,
                    this._editionId, pharmaFormId, divisionId, categoryId);
                
                if (currentParticipant == null)
                {
                    currentParticipant = this.getParticipantProduct(this._editionId, divisionId, categoryId, productId, pharmaFormId);
                    MedinetBusinessLogicComponent.ParticipantProductsBLC.Instance.addToBook(currentParticipant, this._userId, this._hashkey);
                }

                //EditionDivisionProducts
                MedinetBusinessEntries.EditionDivisionProductInfo currentEdProduct = MedinetBusinessLogicComponent.EditionDivisionProductsBLC.Instance.getDivision(this._editionId, divisionId,
                    categoryId, productId, pharmaFormId);
                if (currentEdProduct == null)
                {
                    currentEdProduct = new MedinetBusinessEntries.EditionDivisionProductInfo();
                    currentEdProduct.EditionId = this._editionId;
                    currentEdProduct.DivisionId = divisionId;
                    currentEdProduct.CategoryId = categoryId;
                    currentEdProduct.PharmaFormId = pharmaFormId;
                    currentEdProduct.ProductId = productId;
                    MedinetBusinessLogicComponent.EditionDivisionProductsBLC.Instance.addDivision(currentEdProduct, this._userId, this._hashkey);
                }
                
                familyPrefixInfo.CurrentValue = familyPrefixInfo.CurrentValue + 1;

                //Families
                MedinetBusinessEntries.FamilyInfo familyInfo = new MedinetBusinessEntries.FamilyInfo();
                familyInfo.PrefixId = familyPrefixInfo.PrefixId;
                familyInfo.FamilyString = "[" + familyPrefixInfo.PrefixName + "]" + familyPrefixInfo.CurrentValue;
                familyInfo.Active = true;
                int familyId = MedinetBusinessLogicComponent.FamiliesBLC.Instance.addFamily(familyInfo, this._userId, this._hashkey);

                //Update FamilyPrefix
                MedinetBusinessLogicComponent.FamilyPrefixesBLC.Instance.updateFamilyPrefix(familyPrefixInfo, this._userId, this._hashkey);

                //FamilyProducts
                MedinetBusinessEntries.FamilyProductInfo familyProductInfo = this.getFamilyProduct(this._editionId, familyId, divisionId, categoryId, productId, pharmaFormId);

                //First Delete
                MedinetBusinessLogicComponent.FamilyProductsBLC.Instance.removeFamilyToProduct(familyProductInfo, this._userId, this._hashkey);
                //Then Insert
                MedinetBusinessLogicComponent.FamilyProductsBLC.Instance.addFamilyToProduct(familyProductInfo, this._userId, this._hashkey);

                this.grdProducts.EditIndex = -1;
                this.getEditableProducts();
                ScriptManager.RegisterStartupScript(this, typeof(Page), "messageScript", "jAlert('La familia de contenido se agregó con éxito.', 'Familia de contenido');", true);
            }
        }
    }

    protected void ddlPSFamilyString_SelectedIndexChanged(object sender, EventArgs e)
    {
        int editionProductShotId = 0;
        int productId = Convert.ToInt32(this.grdProducts.DataKeys[this.grdProducts.EditIndex].Values[0].ToString());
        int pharmaFormId = Convert.ToInt32(this.grdProducts.DataKeys[this.grdProducts.EditIndex].Values[1].ToString());
        int divisionId = Convert.ToInt32(this.grdProducts.DataKeys[this.grdProducts.EditIndex].Values[2].ToString());
        int categoryId = Convert.ToInt32(this.grdProducts.DataKeys[this.grdProducts.EditIndex].Values[3].ToString());

        DropDownList ddlPSFamily = (DropDownList)sender;

        if (ddlPSFamily.SelectedValue == "-2")
        {
            //FamilyPrefix
            MedinetBusinessEntries.FamilyPrefixInfo familyPrefixInfo = MedinetBusinessLogicComponent.FamilyPrefixesBLC.Instance.getFamilyPrefixByEdition(this._editionId,
                (byte)PLMUsersBusinessLogicComponent.PLMUsersBusinessLogic.PrefixTypes.ProductShot);

            if (familyPrefixInfo != null)
            {
                //ParticipantProducts
                MedinetBusinessEntries.ParticipantProductsInfo currentParticipant = MedinetBusinessLogicComponent.ParticipantProductsBLC.Instance.getParticipantProduct(productId,
                    this._editionId, pharmaFormId, divisionId, categoryId);

                if (currentParticipant == null)
                {
                    currentParticipant = this.getParticipantProduct(this._editionId, divisionId, categoryId, productId, pharmaFormId);
                    MedinetBusinessLogicComponent.ParticipantProductsBLC.Instance.addToBook(currentParticipant, this._userId, this._hashkey);
                }

                //EditionDivisionProducts
                MedinetBusinessEntries.EditionDivisionProductInfo currentEdProduct = MedinetBusinessLogicComponent.EditionDivisionProductsBLC.Instance.getDivision(this._editionId, divisionId,
                    categoryId, productId, pharmaFormId);
                if (currentEdProduct == null)
                {
                    currentEdProduct = new MedinetBusinessEntries.EditionDivisionProductInfo();
                    currentEdProduct.EditionId = this._editionId;
                    currentEdProduct.DivisionId = divisionId;
                    currentEdProduct.CategoryId = categoryId;
                    currentEdProduct.PharmaFormId = pharmaFormId;
                    currentEdProduct.ProductId = productId;
                    MedinetBusinessLogicComponent.EditionDivisionProductsBLC.Instance.addDivision(currentEdProduct, this._userId, this._hashkey);
                }

                //EditionProductShots
                List<MedinetBusinessEntries.EditionProductShotsInfo> productShots = 
                    MedinetBusinessLogicComponent.EditionProductShotsBLC.Instance.getProductShots(this._editionId, divisionId, categoryId, productId, pharmaFormId);
                if (productShots.Count() < 1)
                {
                    MedinetBusinessEntries.EditionProductShotsInfo productShot = this.getEditionProductShot(this._editionId, divisionId, categoryId, productId, pharmaFormId);
                    editionProductShotId = MedinetBusinessLogicComponent.EditionProductShotsBLC.Instance.addProductShot(productShot, this._userId, this._hashkey);
                }
                else
                    editionProductShotId = productShots[0].EditionProductShotId;

                familyPrefixInfo.CurrentValue = familyPrefixInfo.CurrentValue + 1;

                //Families
                MedinetBusinessEntries.FamilyInfo familyInfo = new MedinetBusinessEntries.FamilyInfo();
                familyInfo.PrefixId = familyPrefixInfo.PrefixId;
                familyInfo.FamilyString = "[" + familyPrefixInfo.PrefixName + "]" + familyPrefixInfo.CurrentValue;
                familyInfo.Active = true;
                int familyId = MedinetBusinessLogicComponent.FamiliesBLC.Instance.addFamily(familyInfo, this._userId, this._hashkey);

                //Update FamilyPrefix
                MedinetBusinessLogicComponent.FamilyPrefixesBLC.Instance.updateFamilyPrefix(familyPrefixInfo, this._userId, this._hashkey);

                //FamilyProductShots
                MedinetBusinessEntries.FamilyProductShotInfo familyProductShotInfo = new MedinetBusinessEntries.FamilyProductShotInfo();
                familyProductShotInfo.FamilyId = familyId;
                familyProductShotInfo.EditionProductShotId = editionProductShotId;

                //First Delete
                MedinetBusinessLogicComponent.FamilyProductShotsBLC.Instance.removeFamilyToProductShot(familyProductShotInfo, this._userId, this._hashkey);
                //Then Insert
                MedinetBusinessLogicComponent.FamilyProductShotsBLC.Instance.addFamilyToProductShot(familyProductShotInfo, this._userId, this._hashkey);

                this.grdProducts.EditIndex = -1;
                this.getEditableProducts();
                ScriptManager.RegisterStartupScript(this, typeof(Page), "messageScript", "jAlert('La familia de SIDEF se agregó con éxito.', 'Familia de SIDEF');", true);
            }
        }
    }

    protected void ddlProductToAdd_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (this.ddlProductToAdd.SelectedItem.ToString() != "SELECCIONAR")
        {
            int productId = Convert.ToInt32(this.ddlProductToAdd.SelectedValue);
            this.ddlNewPharmaFormToAdd.Enabled = true;
            this.ddlNewPharmaFormToAdd.DataSource = MedinetBusinessLogicComponent.CatalogsBLC.Instance.getPharmaFormsWithoutProduct(productId);
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
        this.txtProductName.Focus();
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

    protected void btnEditLaboratory_Click(object sender, EventArgs e)
    {
        this.Response.Redirect("laboratoryInformation.aspx");
    }
    protected void btnChangeDivision_Click(object sender, EventArgs e)
    {
        this.Response.Redirect("productDivisions.aspx");
    }

    protected void btnSaveParticipant_Click(object sender, EventArgs e)
    {
        int productId, pharmaFormId, divisionId, categoryId;

        CheckBox chkParticipant, chkNewProduct, chkModified, chkSidef;

        MedinetBusinessEntries.ParticipantProductsInfo currentParticipant;
        MedinetBusinessEntries.EditionProductShotsInfo productShot;

        for (int gridRow = 0; gridRow < this.grdProducts.Rows.Count; gridRow++)
        {
            productId = Convert.ToInt32(this.grdProducts.DataKeys[gridRow].Values[0].ToString());
            pharmaFormId = Convert.ToInt32(this.grdProducts.DataKeys[gridRow].Values[1].ToString());
            divisionId = this.grdProducts.DataKeys[gridRow].Values[2] == null ? -1 : Convert.ToInt32(this.grdProducts.DataKeys[gridRow].Values[2].ToString());
            categoryId = this.grdProducts.DataKeys[gridRow].Values[3] == null ? -1 : Convert.ToInt32(this.grdProducts.DataKeys[gridRow].Values[3].ToString());

            //Participant Product
            chkParticipant = (CheckBox)this.grdProducts.Rows[gridRow].FindControl("Participate");
            //Modified Content
            chkModified = (CheckBox)this.grdProducts.Rows[gridRow].FindControl("ModifiedContent");
            //New Product
            chkNewProduct = (CheckBox)this.grdProducts.Rows[gridRow].FindControl("NewProduct");
            //SIDEF
            chkSidef = (CheckBox)this.grdProducts.Rows[gridRow].FindControl("Sidef");

            if (divisionId > 0 && categoryId > 0)
            {
                currentParticipant = MedinetBusinessLogicComponent.ParticipantProductsBLC.Instance.getParticipantProduct(productId,
                    this._editionId, pharmaFormId, divisionId, categoryId);

                if (currentParticipant == null)
                {
                    if (chkParticipant.Checked == true)
                    {
                        currentParticipant = this.getParticipantProduct(this._editionId, divisionId, categoryId, productId, pharmaFormId);
                        MedinetBusinessLogicComponent.ParticipantProductsBLC.Instance.addToBook(currentParticipant, this._userId, this._hashkey);
                        this.addEditionSymptom(this._editionId, productId, pharmaFormId);
                        currentParticipant.ModifiedContent = chkModified.Checked;
                        MedinetBusinessLogicComponent.ParticipantProductsBLC.Instance.modifiedContent(currentParticipant, this._userId, this._hashkey);
                        this.addOrRemoveNewProduct(productId, pharmaFormId, divisionId, categoryId, chkNewProduct, chkParticipant);

                        if (chkSidef.Checked == true)
                        {
                            productShot = this.getEditionProductShot(this._editionId, divisionId, categoryId, productId, pharmaFormId);
                            MedinetBusinessLogicComponent.EditionProductShotsBLC.Instance.addProductShot(productShot, this._userId, this._hashkey);
                        }
                    }
                    else
                    {
                        MedinetBusinessLogicComponent.EditionProductShotsBLC.Instance.removeProductShotsByProduct(this._editionId, divisionId,
                                categoryId, productId, pharmaFormId, this._userId, this._hashkey);
                        this.addOrRemoveNewProduct(productId, pharmaFormId, divisionId, categoryId, chkNewProduct, chkParticipant);
                    }
                }
                else
                {
                    if (chkParticipant.Checked == true)
                    {
                        currentParticipant.ModifiedContent = chkModified.Checked;
                        MedinetBusinessLogicComponent.ParticipantProductsBLC.Instance.modifiedContent(currentParticipant, this._userId, this._hashkey);

                        //Erase the modified Attribute group
                        if (chkModified.Checked == false)
                        {
                            List<MedinetBusinessEntries.ModifiedAttributeGroupsInfo> modifiedAttributeGroups =
                                MedinetBusinessLogicComponent.ModifiedAttributeGroupsBLC.Instance.getModifiedAttributeGroupsByProduct(this._editionId, divisionId, categoryId, pharmaFormId, productId);

                            if (modifiedAttributeGroups.Count > 0)
                            {
                                foreach (MedinetBusinessEntries.ModifiedAttributeGroupsInfo attributeGroupItem in modifiedAttributeGroups)
                                    MedinetBusinessLogicComponent.ModifiedAttributeGroupsBLC.Instance.removeProductToAttributeGroup(attributeGroupItem, this._userId, this._hashkey);
                            }
                        }

                        this.addOrRemoveNewProduct(productId, pharmaFormId, divisionId, categoryId, chkNewProduct, chkParticipant);

                        if (chkSidef.Checked == true)
                        {
                            if (MedinetBusinessLogicComponent.EditionProductShotsBLC.Instance.getProductShots(this._editionId, divisionId, categoryId, productId, pharmaFormId).Count() < 1)
                            {
                                productShot = this.getEditionProductShot(this._editionId, divisionId, categoryId, productId, pharmaFormId);
                                MedinetBusinessLogicComponent.EditionProductShotsBLC.Instance.addProductShot(productShot, this._userId, this._hashkey);
                            }
                        }
                        else
                        {
                            MedinetBusinessLogicComponent.EditionProductShotsBLC.Instance.removeProductShotsByProduct(this._editionId, divisionId,
                                categoryId, productId, pharmaFormId, this._userId, this._hashkey);
                        }
                    }
                    else
                    {
                        //FamilyContent
                        MedinetBusinessEntries.FamilyProductInfo familyProduct =
                            MedinetBusinessLogicComponent.FamilyProductsBLC.Instance.getFamilyProduct(this._editionId, divisionId, categoryId, productId, pharmaFormId);
                        if (familyProduct != null)
                            MedinetBusinessLogicComponent.FamilyProductsBLC.Instance.removeFamilyToProduct(familyProduct, this._userId, this._hashkey);

                        MedinetBusinessLogicComponent.EditionProductShotsBLC.Instance.removeProductShotsByProduct(this._editionId, divisionId,
                                categoryId, productId, pharmaFormId, this._userId, this._hashkey);

                        //Erase the modified Attribute group
                        List<MedinetBusinessEntries.ModifiedAttributeGroupsInfo> modifiedAttributeGroups =
                                MedinetBusinessLogicComponent.ModifiedAttributeGroupsBLC.Instance.getModifiedAttributeGroupsByProduct(this._editionId, divisionId, categoryId, pharmaFormId, productId);

                        if (modifiedAttributeGroups.Count > 0)
                        {
                            foreach (MedinetBusinessEntries.ModifiedAttributeGroupsInfo attributeGroupItem in modifiedAttributeGroups)
                                MedinetBusinessLogicComponent.ModifiedAttributeGroupsBLC.Instance.removeProductToAttributeGroup(attributeGroupItem, this._userId, this._hashkey);
                        }
                        this.removeEditionSymptom(this._editionId, productId, pharmaFormId);
                        MedinetBusinessLogicComponent.ParticipantProductsBLC.Instance.removeToBook(currentParticipant, this._userId, this._hashkey);
                        this.addOrRemoveNewProduct(productId, pharmaFormId, divisionId, categoryId, chkNewProduct, chkParticipant);
                    }
                }

                this.addOrRemoveEdition(productId, pharmaFormId, divisionId, categoryId, chkParticipant);
            }
        }
        this.ddlProductToAdd.SelectedValue = "-1";
        this.ddlNewPharmaFormToAdd.SelectedValue = "-1";
        this.getEditableProducts();
        ScriptManager.RegisterStartupScript(this, typeof(Page), "messageScript", "jAlert('Los registros se guardaron con éxito.', 'Registros guardados');", true);
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
    private void addEditionSymptom(int editionId,int productId,int pharmaFormId) {
        if (this._bookShortName.Equals(System.Configuration.ConfigurationManager.AppSettings["Symptoms"]))
        {
            List<MedinetBusinessEntries.ProductSymptomInfo> lisPs = MedinetBusinessLogicComponent.ProductSymptomsBLC.Instance.getSymptomsByParticipantProducts(editionId, productId, pharmaFormId);
            MedinetBusinessEntries.EditionSymptomInfo edSymp;
            foreach (MedinetBusinessEntries.ProductSymptomInfo item in lisPs)
            {
                if (item.SymptomId != -1)
                {
                    edSymp = new MedinetBusinessEntries.EditionSymptomInfo();
                    edSymp.EditionId = editionId;
                    edSymp.SymptomId = item.SymptomId;
                    MedinetBusinessLogicComponent.EditionSymptomBLC.Instance.addEditionSymptom(edSymp);
                }
            }    
        }
     }
    private void removeEditionSymptom(int editionId, int productId, int pharmaFormId) {
        List<MedinetBusinessEntries.ProductSymptomInfo> lisPs = MedinetBusinessLogicComponent.ProductSymptomsBLC.Instance.getSymptomsByParticipantProducts(editionId, productId, pharmaFormId);
        MedinetBusinessEntries.EditionSymptomInfo edSymp;
        foreach (MedinetBusinessEntries.ProductSymptomInfo item in lisPs)
        {
            if (item.SymptomId != -1)
            {
                edSymp = new MedinetBusinessEntries.EditionSymptomInfo();
                edSymp.EditionId = editionId;
                edSymp.SymptomId = item.SymptomId;
                MedinetBusinessLogicComponent.EditionSymptomBLC.Instance.deleteEditionSymptom(edSymp);
            }
        }
    }
    private void getProductsData()
    {
        this._ProductsInfo = new MedinetBusinessEntries.ProductsInfo();

        this._ProductsInfo.Brand = txtBrandToAdd.Text.Trim().ToUpper(); 
        this._ProductsInfo.CountryId = this._countryId;
        this._ProductsInfo.Description = null;
        this._ProductsInfo.SanitaryRegistry = null;
        this._ProductsInfo.Active = true;

        //AlphabetInformation
        MedinetBusinessEntries.AlphabetInfo alphabetInfo = MedinetBusinessLogicComponent.CatalogsBLC.Instance.getLetter(this.txtBrandToAdd.Text.Trim()[0]);
        if (alphabetInfo == null)
            this._ProductsInfo.AlphabetId = MedinetBusinessLogicComponent.CatalogsBLC.Instance.getLetter('A').AlphabetId;
        else
            this._ProductsInfo.AlphabetId = alphabetInfo.AlphabetId;

        //LaboratoryInformation
        MedinetBusinessEntries.DivisionsInfo divisionInfo = MedinetBusinessLogicComponent.CatalogsBLC.Instance.getDivision(this._divisionId);
        this._ProductsInfo.LaboratoryId = divisionInfo.LaboratoryId;
    }

    private void getEditableProducts()
    {
        string proNameToSearch = string.IsNullOrEmpty(this.txtProductName.Text.Trim()) ? null : this.txtProductName.Text.Trim() + "%";
        this.grdProducts.DataSource = MedinetBusinessLogicComponent.ProductsBLC.Instance.getEditableProductsByDivision(this._divisionId, this._countryId, this._editionId, this._bookId, proNameToSearch);
        this.grdProducts.DataBind();
        
    }
    
    private MedinetBusinessEntries.FamilyProductInfo getFamilyProduct(int editionId, int familyId, int divisionId, int categoryId, int productId, int pharmaFormId)
    {
        MedinetBusinessEntries.FamilyProductInfo familyProduct = new MedinetBusinessEntries.FamilyProductInfo();
        familyProduct.EditionId = editionId;
        familyProduct.FamilyId = familyId;
        familyProduct.DivisionId = divisionId;
        familyProduct.CategoryId = categoryId;
        familyProduct.ProductId = productId;
        familyProduct.PharmaFormId = pharmaFormId;

        return familyProduct;
    }

    private MedinetBusinessEntries.EditionProductShotsInfo getEditionProductShot(int editionId, int divisionId, int categoryId, int productId, int pharmaFormId)
    {
        MedinetBusinessEntries.EditionProductShotsInfo editionProductShot = new MedinetBusinessEntries.EditionProductShotsInfo();
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

    private MedinetBusinessEntries.ParticipantProductsInfo getParticipantProduct(int editionId, int divisionId, int categoryId, int productId, int pharmaFormId)
    {
        MedinetBusinessEntries.ParticipantProductsInfo participantProduct = new MedinetBusinessEntries.ParticipantProductsInfo();
        participantProduct.EditionId = editionId;
        participantProduct.DivisionId = divisionId;
        participantProduct.CategoryId = categoryId;
        participantProduct.ProductId = productId;
        participantProduct.PharmaFormId = pharmaFormId;

        return participantProduct;
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

    private void preparedTableToAddProducts()
    {
        this.ddlPharmaFormToAdd.DataSource = MedinetBusinessLogicComponent.CatalogsBLC.Instance.getPharmaForms("%");
        this.ddlPharmaFormToAdd.DataTextField = "Description";
        this.ddlPharmaFormToAdd.DataValueField = "PharmaFormId";
        this.ddlPharmaFormToAdd.DataBind();

        ListItem initialLine = new ListItem("SELECCIONAR", "-1", true);
        initialLine.Selected = true;
        this.ddlPharmaFormToAdd.Items.Insert(0, initialLine);
    }

    private void preparedTableToAddPharmaForms()
    {
        this.ddlProductToAdd.DataSource = MedinetBusinessLogicComponent.ProductsBLC.Instance.getProductsByDivision(this._divisionId, this._countryId, this._bookId);
        this.ddlProductToAdd.DataTextField = "Brand";
        this.ddlProductToAdd.DataValueField = "ProductId";
        this.ddlProductToAdd.DataBind();

        ListItem initialLine = new ListItem("SELECCIONAR", "-1", true);
        initialLine.Selected = true;
        this.ddlProductToAdd.Items.Insert(0, initialLine);
    }

    private void addProduct()
    {
        int pharmaFormId = Convert.ToInt32(this.ddlPharmaFormToAdd.SelectedValue);
        this.getProductsData();

        //Insert Product
        int productId = MedinetBusinessLogicComponent.ProductsBLC.Instance.addProduct(this._ProductsInfo, this._userId, this._hashkey);

        //Insert ProductPharmaForm
        MedinetBusinessEntries.ProductPharmaFormInfo productPharmaForm = new MedinetBusinessEntries.ProductPharmaFormInfo();
        productPharmaForm.ProductId = productId;
        productPharmaForm.PharmaFormId = pharmaFormId;
        productPharmaForm.Active = true;
        MedinetBusinessLogicComponent.ProductPharmaFormsBLC.Instance.addPharmaForm(productPharmaForm, this._userId, this._hashkey);

        //Insert DivisionCategories
        MedinetBusinessEntries.DivisionCategoryInfo divisionCategory =
            MedinetBusinessLogicComponent.DivisionCategoriesBLC.Instance.getDivisionCategory(this._divisionId, Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["DefaultCategory"]));
        if (divisionCategory == null)
        {
            divisionCategory = new MedinetBusinessEntries.DivisionCategoryInfo();
            divisionCategory.DivisionId = this._divisionId;
            divisionCategory.CategoryId = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["DefaultCategory"]);
            MedinetBusinessLogicComponent.DivisionCategoriesBLC.Instance.addDivisionCategory(divisionCategory, this._userId, this._hashkey);
        }

        //Insert ProductCategories
        MedinetBusinessEntries.ProductCategoriesInfo productCategory = new MedinetBusinessEntries.ProductCategoriesInfo();
        productCategory.DivisionId = this._divisionId;
        productCategory.CategoryId = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["DefaultCategory"]);
        productCategory.ProductId = productId;
        productCategory.PharmaFormId = pharmaFormId;
        productCategory.ProductDescription = null;
        productCategory.TechnicalSheet = false;
        MedinetBusinessLogicComponent.ProductCategoriesBLC.Instance.addProductToCategory(productCategory, this._userId, this._hashkey);

        //Insert ParticipantProducts
        MedinetBusinessEntries.ParticipantProductsInfo participantProduct = new MedinetBusinessEntries.ParticipantProductsInfo();
        participantProduct.EditionId = this._editionId;
        participantProduct.DivisionId = this._divisionId;
        participantProduct.CategoryId = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["DefaultCategory"]);
        participantProduct.ProductId = productId;
        participantProduct.PharmaFormId = pharmaFormId;
        participantProduct.HTMLContent = null;
        participantProduct.XMLContent = null;
        participantProduct.Page = null;
        participantProduct.ModifiedContent = null;
        MedinetBusinessLogicComponent.ParticipantProductsBLC.Instance.addToBook(participantProduct, this._userId, this._hashkey);

        //Insert NewProducts
        MedinetBusinessEntries.NewProductInfo newProduct = new MedinetBusinessEntries.NewProductInfo();
        newProduct.EditionId = this._editionId;
        newProduct.DivisionId = this._divisionId;
        newProduct.CategoryId = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["DefaultCategory"]);
        newProduct.ProductId = productId;
        newProduct.PharmaFormId = pharmaFormId;
        MedinetBusinessLogicComponent.NewProductsBLC.Instance.addNew(newProduct, this._userId, this._hashkey);

        //Insert EditionDivisionProducts
        MedinetBusinessEntries.EditionDivisionProductInfo editionDivisionProduct = new MedinetBusinessEntries.EditionDivisionProductInfo();
        editionDivisionProduct.EditionId = this._editionId;
        editionDivisionProduct.DivisionId = this._divisionId;
        editionDivisionProduct.CategoryId = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["DefaultCategory"]);
        editionDivisionProduct.ProductId = productId;
        editionDivisionProduct.PharmaFormId = pharmaFormId;
        MedinetBusinessLogicComponent.EditionDivisionProductsBLC.Instance.addDivision(editionDivisionProduct, this._userId, this._hashkey);
    }

    private void addPharmaFormToProduct()
    {
        int productId = Convert.ToInt32(this.ddlProductToAdd.SelectedValue);
        int pharmaFormId = Convert.ToInt32(this.ddlNewPharmaFormToAdd.SelectedValue);

        //Insert ProductPharmaForm
        MedinetBusinessEntries.ProductPharmaFormInfo productPharmaForm = MedinetBusinessLogicComponent.ProductPharmaFormsBLC.Instance.getProductPF(productId, pharmaFormId);
        if (productPharmaForm == null)
        {
            productPharmaForm = new MedinetBusinessEntries.ProductPharmaFormInfo();
            productPharmaForm.ProductId = productId;
            productPharmaForm.PharmaFormId = pharmaFormId;
            productPharmaForm.Active = true;
            MedinetBusinessLogicComponent.ProductPharmaFormsBLC.Instance.addPharmaForm(productPharmaForm, this._userId, this._hashkey);
        }

        //Insert ProductCategories
        MedinetBusinessEntries.ProductCategoriesInfo productCategory =
            MedinetBusinessLogicComponent.ProductCategoriesBLC.Instance.getCategory(productId, pharmaFormId, this._divisionId, Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["DefaultCategory"]));
        if (productCategory == null)
        {
            productCategory = new MedinetBusinessEntries.ProductCategoriesInfo();
            productCategory.DivisionId = this._divisionId;
            productCategory.CategoryId = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["DefaultCategory"]);
            productCategory.ProductId = productId;
            productCategory.PharmaFormId = pharmaFormId;
            productCategory.ProductDescription = null;
            productCategory.TechnicalSheet = false;
            MedinetBusinessLogicComponent.ProductCategoriesBLC.Instance.addProductToCategory(productCategory, this._userId, this._hashkey);
        }

        //Insert ParticipantProducts
        MedinetBusinessEntries.ParticipantProductsInfo participantProduct =
            MedinetBusinessLogicComponent.ParticipantProductsBLC.Instance.getParticipantProduct(productId, this._editionId, pharmaFormId, this._divisionId, Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["DefaultCategory"]));
        if(participantProduct == null)
        {
            participantProduct = new MedinetBusinessEntries.ParticipantProductsInfo();
            participantProduct.EditionId = this._editionId;
            participantProduct.DivisionId = this._divisionId;
            participantProduct.CategoryId = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["DefaultCategory"]);
            participantProduct.ProductId = productId;
            participantProduct.PharmaFormId = pharmaFormId;
            participantProduct.HTMLContent = null;
            participantProduct.XMLContent = null;
            participantProduct.Page = null;
            participantProduct.ModifiedContent = null;
            MedinetBusinessLogicComponent.ParticipantProductsBLC.Instance.addToBook(participantProduct, this._userId, this._hashkey);
        }

        //Insert NewProducts
        MedinetBusinessEntries.NewProductInfo newProduct = 
            MedinetBusinessLogicComponent.NewProductsBLC.Instance.getParticipantProduct(productId, this._editionId, pharmaFormId, this._divisionId, Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["DefaultCategory"]));
        if (newProduct == null)
        {
            newProduct = new MedinetBusinessEntries.NewProductInfo();
            newProduct.EditionId = this._editionId;
            newProduct.DivisionId = this._divisionId;
            newProduct.CategoryId = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["DefaultCategory"]);
            newProduct.ProductId = productId;
            newProduct.PharmaFormId = pharmaFormId;
            MedinetBusinessLogicComponent.NewProductsBLC.Instance.addNew(newProduct, this._userId, this._hashkey);
        }

        //Insert EditionDivisionProducts
        MedinetBusinessEntries.EditionDivisionProductInfo editionDivisionProduct =
            MedinetBusinessLogicComponent.EditionDivisionProductsBLC.Instance.getDivision(this._editionId, this._divisionId, Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["DefaultCategory"]), productId, pharmaFormId);
        if (editionDivisionProduct == null)
        {
            editionDivisionProduct = new MedinetBusinessEntries.EditionDivisionProductInfo();
            editionDivisionProduct.EditionId = this._editionId;
            editionDivisionProduct.DivisionId = this._divisionId;
            editionDivisionProduct.CategoryId = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["DefaultCategory"]);
            editionDivisionProduct.ProductId = productId;
            editionDivisionProduct.PharmaFormId = pharmaFormId;
            MedinetBusinessLogicComponent.EditionDivisionProductsBLC.Instance.addDivision(editionDivisionProduct, this._userId, this._hashkey);
        }
    }

    private void addOrRemoveEdition(int productId, int pharmaFormId, int divisionId, int categoryId, CheckBox chkParticipant)
    {
        MedinetBusinessEntries.EditionDivisionProductInfo currentEdProduct =
            MedinetBusinessLogicComponent.EditionDivisionProductsBLC.Instance.getDivision(this._editionId, divisionId, categoryId, productId, pharmaFormId);

        if (currentEdProduct == null)
        {
            if (chkParticipant.Checked == true)
            {
                currentEdProduct = new MedinetBusinessEntries.EditionDivisionProductInfo();
                currentEdProduct.EditionId = this._editionId;
                currentEdProduct.DivisionId = divisionId;
                currentEdProduct.CategoryId = categoryId;
                currentEdProduct.PharmaFormId = pharmaFormId;
                currentEdProduct.ProductId = productId;
                MedinetBusinessLogicComponent.EditionDivisionProductsBLC.Instance.addDivision(currentEdProduct, this._userId, this._hashkey);
            }
        }
        else
            if (chkParticipant.Checked == false)
                MedinetBusinessLogicComponent.EditionDivisionProductsBLC.Instance.removeDivision(currentEdProduct, this._userId, this._hashkey);
    }

    private void addOrRemoveNewProduct(int productId, int pharmaFormId, int divisionId, int categoryId, CheckBox chkNewProduct, CheckBox chkParticipant)
    {
        MedinetBusinessEntries.NewProductInfo currentNewProduct =
            MedinetBusinessLogicComponent.NewProductsBLC.Instance.getParticipantProduct(productId, this._editionId, pharmaFormId, divisionId, categoryId);

        if (currentNewProduct == null)
        {
            if (chkParticipant.Checked == true && chkNewProduct.Checked == true)
            {
                currentNewProduct = new MedinetBusinessEntries.NewProductInfo();
                currentNewProduct.EditionId = this._editionId;
                currentNewProduct.DivisionId = divisionId;
                currentNewProduct.CategoryId = categoryId;
                currentNewProduct.PharmaFormId = pharmaFormId;
                currentNewProduct.ProductId = productId;
                MedinetBusinessLogicComponent.NewProductsBLC.Instance.addNew(currentNewProduct, this._userId, this._hashkey);
            }
        }
        else
            if (chkParticipant.Checked == false || chkNewProduct.Checked == false)
                MedinetBusinessLogicComponent.NewProductsBLC.Instance.removeNew(currentNewProduct, this._userId, this._hashkey);
    }

    
 
    private void exportPdf()
    { 
        
        DevExpress.XtraPrinting.PdfExportOptions pdfExport = new DevExpress.XtraPrinting.PdfExportOptions();
        pdfExport.DocumentOptions.Title = "Participant Products";
        pdfExport.DocumentOptions.Author = "Medinet 2.0";
        _date = DateTime.Now.ToString();
        List<string> titles = new List<string>();
        titles.Add("Listado de Productos Participantes");
        titles.Add("Obra : " + _bookName);
        titles.Add("Edición : " + _editionName);
        titles.Add("Laboratorio: " + _divisionName);
        titles.Add("Usuario: " + MedinetBusinessLogicComponent.FilesBLC.Instance.getUserName(_userId));
        titles.Add("Fecha: " + _date);

        System.Data.DataTable dtProds = new System.Data.DataTable();
        dtProds.Columns.Add("Categoria");
        dtProds.Columns.Add("Producto");
        dtProds.Columns.Add("Forma farmaceutica");
        dtProds.Columns.Add("Descripción");
        string proNameToSearch = string.IsNullOrEmpty(this.txtProductName.Text.Trim()) ? null : this.txtProductName.Text.Trim() + "%";
        List<MedinetBusinessEntries.ProductsToEditInfo> products = MedinetBusinessLogicComponent.ProductsBLC.Instance.getEditableProductsByDivision(this._divisionId, this._countryId, this._editionId, this._bookId, proNameToSearch);
        foreach (MedinetBusinessEntries.ProductsToEditInfo prod in products)
        {
            if (prod.Participant)
            {
                dtProds.Rows.Add(prod.CategoryName, prod.Brand, prod.PharmaForm, prod.ProductDescription);
            }
        }
        MedinetBusinessEntries.VersionFileInfo VersionFile = MedinetBusinessLogicComponent.FilesBLC.Instance.getVersionFile(getPrefixApplication(), _editionId);
        string route = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["BaseURL"]);
        string fileName = "ProductosParticipantes_" + getPrefixApplication() + "_" + VersionFile.CurrentValue + ".pdf";
        string filePath = route + @"\" + fileName;
        createReport(dtProds, titles).ExportToPdf(filePath, pdfExport);
        MedinetBusinessEntries.FileInfo fileInfo = new MedinetBusinessEntries.FileInfo();
        fileInfo.BaseUrl = route;
        fileInfo.FileDate = DateTime.Parse(_date);
        fileInfo.FileName = fileName;
        fileInfo.FormatFileId = MedinetBusinessLogicComponent.FilesBLC.Instance.getFormatFileId("pdf");
        fileInfo.NickName = _user;
        fileInfo.Version = getPrefixApplication() + "_" + (VersionFile.CurrentValue.ToString());
        fileInfo.VersionFileId = VersionFile.VersionFileId;
        MedinetBusinessLogicComponent.FilesBLC.Instance.addFile(fileInfo, getPrefixApplication());
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
        titles.Add("Usuario: " + MedinetBusinessLogicComponent.FilesBLC.Instance.getUserName(_userId));
        titles.Add("Fecha: " + _date);
        
        System.Data.DataTable dtProds = new System.Data.DataTable();
        dtProds.Columns.Add("Categoria");
        dtProds.Columns.Add("Producto");
        dtProds.Columns.Add("Forma farmaceutica");
        dtProds.Columns.Add("Tipo de producto");
        dtProds.Columns.Add("Descripción");
        dtProds.Columns.Add("Nuevo");
        dtProds.Columns.Add("Con Cambios");
        dtProds.Columns.Add("ProductShot");
        dtProds.Columns.Add("Ejecutivo(s)");
        string proNameToSearch = string.IsNullOrEmpty(this.txtProductName.Text.Trim()) ? null : this.txtProductName.Text.Trim() + "%";
        List<MedinetBusinessEntries.ProductsToEditInfo> products = MedinetBusinessLogicComponent.ProductsBLC.Instance.getEditableProductsByDivision(this._divisionId, this._countryId, this._editionId, this._bookId, proNameToSearch);
        foreach (MedinetBusinessEntries.ProductsToEditInfo prod in products)
        {
            if (prod.Participant)
            {
                dtProds.Rows.Add(prod.CategoryName, prod.Brand, prod.PharmaForm, prod.ProductType, prod.ProductDescription, prod.NewProduct ? "Si" : "No", prod.ModifiedContent ? "Si" : "No", prod.Sidef ? "Si" : "No", MedinetBusinessLogicComponent.FilesBLC.Instance.getUserAgentByDivisionByEditionByAction(this._editionId, Convert.ToInt32(prod.DivisionId), Convert.ToInt32(prod.CategoryId), Convert.ToInt32(prod.PharmaFormId), prod.ProductId));
            }
        }
        MedinetBusinessEntries.VersionFileInfo VersionFile = MedinetBusinessLogicComponent.FilesBLC.Instance.getVersionFile(getPrefixApplication(), _editionId);
        string route = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["BaseURL"]);
        string fileName = "ProductosParticipantes_" + getPrefixApplication() + "_" + VersionFile.CurrentValue + ".xls";
        string filePath = route + @"\" + fileName;
        MemoryStream ms = new MemoryStream();
        this.createReport(dtProds, titles).ExportToXls(ms);
        byte[] buffer = new byte[ms.Length];
        ms.Position = 0;
        ms.Read(buffer, 0, (int)ms.Length);
        ms.Close();
        this.Session["doc"] = buffer;
        //.ExportToXls(filePath,xlsExport);
        MedinetBusinessEntries.FileInfo fileInfo = new MedinetBusinessEntries.FileInfo();
        fileInfo.BaseUrl = route;
        fileInfo.FileDate = DateTime.Parse(_date);
        fileInfo.FileName = fileName;
        fileInfo.FormatFileId = MedinetBusinessLogicComponent.FilesBLC.Instance.getFormatFileId("xls");
        fileInfo.NickName = _user;
        fileInfo.Version = getPrefixApplication() + "_" + (VersionFile.CurrentValue.ToString());
        fileInfo.VersionFileId = VersionFile.VersionFileId;
        MedinetBusinessLogicComponent.FilesBLC.Instance.addFile(fileInfo, getPrefixApplication());
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
        if (e.Row.RowIndex == this.grdProducts.EditIndex)
        {
            DropDownList ddlPharmaFormsToAdd = (DropDownList)e.Row.FindControl("ddlPharmaFormsToAdd");

            ddlPharmaFormsToAdd.DataSource = MedinetBusinessLogicComponent.CatalogsBLC.Instance.getPharmaFormsWithoutProduct(productId);
            ddlPharmaFormsToAdd.DataTextField = "Description";
            ddlPharmaFormsToAdd.DataValueField = "PharmaFormId";
            ddlPharmaFormsToAdd.DataBind();

            // Add the initial value:
            ListItem initialLine = new ListItem("SELECCIONAR", "-1", true);
            initialLine.Selected = true;
            ddlPharmaFormsToAdd.Items.Insert(0, initialLine);
            ddlPharmaFormsToAdd.SelectedValue = "-1";
            ddlPharmaFormsToAdd.Enabled = true;
        }
    }

    protected void displayContentFamily(GridViewRowEventArgs e, int? divisionId, int? contentFamilyId)
    {
        if (divisionId != null)
        {
            if (e.Row.RowIndex == this.grdProducts.EditIndex)
            {
                ListItem initialLine;

                DropDownList ddlContentFamilies = (DropDownList)e.Row.FindControl("ddlContentFamilyString");

                ddlContentFamilies.DataSource = MedinetBusinessLogicComponent.FamiliesBLC.Instance.getContentFamiliesByDivision(this._editionId, (int)divisionId);
                ddlContentFamilies.DataTextField = "FamilyString";
                ddlContentFamilies.DataValueField = "FamilyId";
                ddlContentFamilies.DataBind();

                // Add the initial value:
                initialLine = new ListItem("SELECCIONAR", "-1", true);
                initialLine.Selected = true;
                ddlContentFamilies.Items.Insert(0, initialLine);

                // Add the second value:
                initialLine = new ListItem("AGREGAR NUEVA", "-2", true);
                initialLine.Selected = true;
                ddlContentFamilies.Items.Insert(1, initialLine);

                ddlContentFamilies.SelectedValue = contentFamilyId == null ? "-1" : contentFamilyId.ToString();
                ddlContentFamilies.Enabled = true;
            }
            else
            {
                Label lblPF = (Label)e.Row.FindControl("lblContentFamilyString");
                lblPF.Text = contentFamilyId == null ? "" : MedinetBusinessLogicComponent.FamiliesBLC.Instance.getFamily((int)contentFamilyId).FamilyString;
            }
        }
    }

    protected void displayPSFamily(GridViewRowEventArgs e, int? divisionId, int? psFamilyId)
    {
        if (divisionId != null)
        {
            if (e.Row.RowIndex == this.grdProducts.EditIndex)
            {
                ListItem initialLine;
                DropDownList ddlPSFamilies = (DropDownList)e.Row.FindControl("ddlPSFamilyString");

                ddlPSFamilies.DataSource = MedinetBusinessLogicComponent.FamiliesBLC.Instance.getPSFamiliesByDivision(this._editionId, (int)divisionId);
                ddlPSFamilies.DataTextField = "FamilyString";
                ddlPSFamilies.DataValueField = "FamilyId";
                ddlPSFamilies.DataBind();

                // Add the initial value:
                initialLine = new ListItem("SELECCIONAR", "-1", true);
                initialLine.Selected = true;
                ddlPSFamilies.Items.Insert(0, initialLine);

                // Add the second value:
                initialLine = new ListItem("AGREGAR NUEVA", "-2", true);
                initialLine.Selected = true;
                ddlPSFamilies.Items.Insert(1, initialLine);

                ddlPSFamilies.SelectedValue = psFamilyId == null ? "-1" : psFamilyId.ToString();
                ddlPSFamilies.Enabled = true;
            }
            else
            {
                Label lblPF = (Label)e.Row.FindControl("lblPSFamilyString");
                lblPF.Text = psFamilyId == null ? "" : MedinetBusinessLogicComponent.FamiliesBLC.Instance.getFamily((int)psFamilyId).FamilyString;
            }
        }
    }

    protected void displayCountriesToEdit(GridViewRowEventArgs e, int countryId)
    {
        if (e.Row.RowIndex == this.grdProducts.EditIndex)
        {
            CheckBoxList chkList = (CheckBoxList)e.Row.FindControl("chkCountries");
            MedinetBusinessEntries.ProductsToEditInfo currentRow = (MedinetBusinessEntries.ProductsToEditInfo)e.Row.DataItem;
            chkList.Attributes.Add("countries",currentRow.Countries);
            chkList.DataSource = MedinetBusinessLogicComponent.CatalogsBLC.Instance.getCountryCodes(countryId);
            chkList.DataTextField = "CountryName";
            chkList.DataValueField = "CountryCodeId";
            chkList.DataBind();

            string [] countriesList = currentRow.Countries.Split(',');
            foreach (ListItem item in chkList.Items)
            {
                foreach (string val in countriesList)
                {
                    if (val.Equals(item.Value))
                    {
                        item.Selected = true;
                    }
                }
            }
            
        }
    }

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
    private MedinetBusinessEntries.ProductsInfo _ProductsInfo;
    private PLMCryptographyComponent.CryptographyComponent _cryptography = new PLMCryptographyComponent.CryptographyComponent();

}