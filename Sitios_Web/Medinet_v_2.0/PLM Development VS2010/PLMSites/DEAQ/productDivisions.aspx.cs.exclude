using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class productDivisions : System.Web.UI.Page
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
        this.Session["divisionName"] = null;
        this._hashkey = System.Configuration.ConfigurationManager.AppSettings["HashKey"];

        if (!IsPostBack)
        {
            this.prepareGrid(Convert.ToInt16(this.ddlPageSize.SelectedValue));
            this.getDivisions();
            this.getDivisionProducts();
        }
    }

    #endregion

    #region Control Events

    protected void grdDivisionProducts_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        this.grdDivisionProducts.PageIndex = e.NewPageIndex;
        this.Session["CurrentPage"] = Convert.ToInt32(e.NewPageIndex);
        this.getDivisionProducts();
    }

    protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.Session["CurrentPage"] = 0;
        this.prepareGrid(Convert.ToInt16(this.ddlPageSize.SelectedValue));
        this.getDivisionProducts();
    }

    protected void imgBtnBackLaboratory_Click(object sender, ImageClickEventArgs e)
    {
        this.Session["divisionName"] = MedinetBusinessLogicComponent.DivisionsBLC.Instance.getDivision(this._divisionId).ShortName;
        this.Response.Redirect("products.aspx");
    }

    protected void btnConsult_Click(object sender, EventArgs e)
    {
        this.prepareGrid(Convert.ToInt16(this.ddlPageSize.SelectedValue));
        this.getDivisionProducts();
        this.txtProductName.Focus();
    }

    protected void btnSaveProducts_Click(object sender, EventArgs e)
    {
        int productId, pharmaFormId;
        CheckBox chkChangeDivision;

        for (int gridRow = 0; gridRow < this.grdDivisionProducts.Rows.Count; gridRow++)
        {
            productId = Convert.ToInt32(this.grdDivisionProducts.DataKeys[gridRow].Values[1].ToString());
            pharmaFormId = Convert.ToInt32(this.grdDivisionProducts.DataKeys[gridRow].Values[2].ToString());

            chkChangeDivision = (CheckBox)this.grdDivisionProducts.Rows[gridRow].FindControl("chkChangeDivision");

            if (chkChangeDivision.Checked == true)
            {
                MedinetBusinessEntries.ProductCategoriesInfo productCategory = MedinetBusinessLogicComponent.ProductCategoriesBLC.Instance.getCategory(productId, pharmaFormId,
                    this._divisionId, Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["DefaultCategory"]));

                if (productCategory == null)
                {
                    productCategory = new MedinetBusinessEntries.ProductCategoriesInfo();
                    productCategory.DivisionId = this._divisionId;
                    productCategory.CategoryId = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["DefaultCategory"]);
                    productCategory.ProductId = productId;
                    productCategory.PharmaFormId = pharmaFormId;
                    productCategory.TechnicalSheet = false;
                    productCategory.ProductDescription = null;
                    MedinetBusinessLogicComponent.ProductCategoriesBLC.Instance.addProductToCategory(productCategory, this._userId, this._hashkey);
                }

                MedinetBusinessEntries.ParticipantProductsInfo participantProduct = MedinetBusinessLogicComponent.ParticipantProductsBLC.Instance.getParticipantProduct(productId,
                    this._editionId, pharmaFormId, this._divisionId, Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["DefaultCategory"]));
                if (participantProduct == null)
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
                    participantProduct.ModifiedContent = false;
                    MedinetBusinessLogicComponent.ParticipantProductsBLC.Instance.addToBook(participantProduct, this._userId, this._hashkey);
                }

                MedinetBusinessEntries.EditionDivisionProductInfo editionProduct = MedinetBusinessLogicComponent.EditionDivisionProductsBLC.Instance.getDivision(this._editionId, this._divisionId,
                    Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["DefaultCategory"]), productId, pharmaFormId);
                if (editionProduct == null)
                {
                    editionProduct = new MedinetBusinessEntries.EditionDivisionProductInfo();
                    editionProduct.EditionId = this._editionId;
                    editionProduct.DivisionId = this._divisionId;
                    editionProduct.CategoryId = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["DefaultCategory"]);
                    editionProduct.ProductId = productId;
                    editionProduct.PharmaFormId = pharmaFormId;
                    MedinetBusinessLogicComponent.EditionDivisionProductsBLC.Instance.addDivision(editionProduct, this._userId, this._hashkey);
                }
            }
        }
        this.getDivisionProducts();
        ScriptManager.RegisterStartupScript(this, typeof(Page), "messageScript", "jAlert('Los registros se guardaron con éxito.', 'Registros guardados');", true);
    }
    
    #endregion

    #region Private Methods

    private void getDivisionProducts()
    {
        string proNameToSearch = string.IsNullOrEmpty(this.txtProductName.Text.Trim()) ? null : this.txtProductName.Text.Trim() + "%";
        this.grdDivisionProducts.DataSource = MedinetBusinessLogicComponent.ProductsBLC.Instance.getEditableDivisionProducts(this._divisionId, this._countryId,
            this.ddlOtherDivisions.SelectedValue == "-1" ? null : (int?)Convert.ToInt32(this.ddlOtherDivisions.SelectedValue), proNameToSearch);
        this.grdDivisionProducts.DataBind();
    }

    private void getDivisions()
    {
        ListItem itemToDelete = null;

        this.ddlOtherDivisions.DataSource = MedinetBusinessLogicComponent.CatalogsBLC.Instance.getDivisionsByCountry(this._countryId);
        this.ddlOtherDivisions.DataTextField = "ShortName";
        this.ddlOtherDivisions.DataValueField = "DivisionId";
        this.ddlOtherDivisions.DataBind();

        foreach(ListItem item in this.ddlOtherDivisions.Items)
        {
            if (Convert.ToInt32(item.Value) == this._divisionId)
                itemToDelete = item;
        }

        ListItem initialLine = new ListItem("SELECCIONAR", "-1", true);
        initialLine.Selected = true;
        this.ddlOtherDivisions.Items.Insert(0, initialLine);
        this.ddlOtherDivisions.Items.Remove(itemToDelete);
    }

    private void prepareGrid(short pagesize)
    {
        if (pagesize > 0)
        {
            this.grdDivisionProducts.AllowPaging = true;
            this.grdDivisionProducts.PageSize = pagesize;
            this.grdDivisionProducts.PageIndex = this.Session["CurrentPage"] == null ? 0 : Convert.ToInt32(this.Session["CurrentPage"]);
        }
        else
            this.grdDivisionProducts.AllowPaging = false;
    }

    #endregion

    private int _editionId;
    private int _bookId;
    private int _countryId;
    private int _divisionId;
    private int _userId;
    private int _userCountry;
    private string _hashkey;

}