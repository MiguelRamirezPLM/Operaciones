using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class pagedProduct : System.Web.UI.Page
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
        BtnSaveTop.Click += new EventHandler(btnSaveProducts_OnClick);
        this._bookShortName = this.Session["bookShortName"] == null ? "" : this.Session["bookShortName"].ToString();
        
        if (this._bookShortName.Equals(System.Configuration.ConfigurationManager.AppSettings["Symptoms"]))
        {
            aspGrdProducts.Columns["Síntomas"].Visible = true;
        }
        else
        {
            aspGrdProducts.Columns["Síntomas"].Visible = false;
        }
        if (!IsPostBack)
        {
            //this.prepareGrid(Convert.ToInt16(this.ddlPageSize.SelectedValue));
            //this.getPagedProducts();
        }
        if (this._editionId != -1 && this._countryId != -1)
        {
            this.getPagedProducts();
        }
        
    }

    #endregion

    #region Control Events

    protected void grdProducts_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            MedinetBusinessEntries.PagedProductInfo currentRow = (MedinetBusinessEntries.PagedProductInfo)e.Row.DataItem;

            TextBox txtPage = (TextBox)e.Row.FindControl("txtPage");

            txtPage.Text = currentRow.Page == null ? "" : currentRow.Page;
            txtPage.Attributes.Add("divisionId", currentRow.DivisionId.ToString());
            txtPage.Attributes.Add("categoryId", currentRow.CategoryId.ToString());
            txtPage.Attributes.Add("productId", currentRow.ProductId.ToString());
            txtPage.Attributes.Add("pharmaFormId", currentRow.PharmaFormId.ToString());
        }
    }

    protected void grdProducts_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        this.grdProducts.PageIndex = e.NewPageIndex;
        this.Session["CurrentPage"] = Convert.ToInt32(e.NewPageIndex);
        this.getPagedProducts();
    }

    protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
    {
        //this.Session["CurrentPage"] = 0;
        //this.prepareGrid(Convert.ToInt16(this.ddlPageSize.SelectedValue));
        //this.getPagedProducts();

        this.Session["CurrentPage"] = 0;
        if (this.ddlPageSize.SelectedValue != "-1")
        {
            this.aspGrdProducts.SettingsPager.Mode = DevExpress.Web.ASPxGridView.GridViewPagerMode.ShowPager;
            this.aspGrdProducts.SettingsPager.PageSize = Convert.ToInt32(this.ddlPageSize.SelectedValue);
        }
        else
        {
            this.aspGrdProducts.SettingsPager.Mode = DevExpress.Web.ASPxGridView.GridViewPagerMode.ShowAllRecords;
        }
        this.getPagedProducts();
    }

    protected void ddlAlphabet_SelectedIndexChanged(object sender, EventArgs e)
    {
        string letterToSearch = this.ddlAlphabet.SelectedValue == "-1" ? "%" : this.ddlAlphabet.SelectedValue + "%";

        if (this._editionId > 0)
        {
            //this.grdProducts.DataSource = MedinetBusinessLogicComponent.ParticipantProductsBLC.Instance.getProductPaged(this._editionId, letterToSearch);
            //this.grdProducts.DataBind();
            this.aspGrdProducts.DataSource = MedinetBusinessLogicComponent.ParticipantProductsBLC.Instance.getProductPaged(this._editionId, letterToSearch);
            this.aspGrdProducts.DataBind();
        }
    }

    protected void imgBtnBackLabs_Click(object sender, ImageClickEventArgs e)
    {
        this.Response.Redirect("laboratories.aspx");
    }

    protected void btnSaveProducts_OnClick(object sender, EventArgs e)
    {
        //int productId, pharmaFormId, divisionId, categoryId;
        MedinetBusinessEntries.ParticipantProductsInfo participantProduct;
        TextBox txtPage;
        for (int gridRow = 0; gridRow < this.aspGrdProducts.VisibleRowCount; gridRow++)
        {
            
            int prodId = (int)aspGrdProducts.GetRowValues(gridRow, "ProductId");
            int pharFo = (int)aspGrdProducts.GetRowValues(gridRow, "PharmaFormId");
            int divId = (int)aspGrdProducts.GetRowValues(gridRow, "DivisionId");
            int catId = (int)aspGrdProducts.GetRowValues(gridRow, "CategoryId");
            participantProduct = new MedinetBusinessEntries.ParticipantProductsInfo();

            txtPage = aspGrdProducts.FindRowCellTemplateControl(gridRow,null, "txtPage") as TextBox;
            if (txtPage!=null)
            {
            
                participantProduct.ProductId = prodId;
                participantProduct.PharmaFormId = pharFo;
                participantProduct.DivisionId = divId;
                participantProduct.CategoryId = catId;
                participantProduct.EditionId = this._editionId;
                participantProduct.Page = txtPage.Text.Trim() == "" ? null : txtPage.Text.Trim();
                MedinetBusinessLogicComponent.ParticipantProductsBLC.Instance.addPage(participantProduct, this._userId, this._hashkey);
            }
        }

        //for (int gridRow = 0; gridRow < this.grdProducts.Rows.Count; gridRow++)
        //{
        //    productId = Convert.ToInt32(this.grdProducts.DataKeys[gridRow].Values[0].ToString());
        //    pharmaFormId = Convert.ToInt32(this.grdProducts.DataKeys[gridRow].Values[1].ToString());
        //    divisionId = this.grdProducts.DataKeys[gridRow].Values[2] == null ? -1 : Convert.ToInt32(this.grdProducts.DataKeys[gridRow].Values[2].ToString());
        //    categoryId = this.grdProducts.DataKeys[gridRow].Values[3] == null ? -1 : Convert.ToInt32(this.grdProducts.DataKeys[gridRow].Values[3].ToString());

        //    participantProduct = new MedinetBusinessEntries.ParticipantProductsInfo();
        //    txtPage = (TextBox)this.grdProducts.Rows[gridRow].FindControl("txtPage");

        //    participantProduct.ProductId = productId;
        //    participantProduct.PharmaFormId = pharmaFormId;
        //    participantProduct.DivisionId = divisionId;
        //    participantProduct.CategoryId = categoryId;
        //    participantProduct.EditionId = this._editionId;
        //    participantProduct.Page = txtPage.Text.Trim() == "" ? null : txtPage.Text.Trim();

        //    //participantProduct.ProductId = productId;

        //    MedinetBusinessLogicComponent.ParticipantProductsBLC.Instance.addPage(participantProduct, this._userId, this._hashkey);
        //}
        this.getPagedProducts();
        ScriptManager.RegisterStartupScript(this, typeof(Page), "messageScript", "jAlert('Los registros se guardaron con éxito.', 'Registros guardados');", true);
    }

    #endregion

    #region Private Methods

    private void getPagedProducts() 
    {
        string letterToSearch = this.ddlAlphabet.SelectedValue == "-1" ? "%" : this.ddlAlphabet.SelectedValue + "%";

        if (this._editionId > 0)
        {
            //this.grdProducts.DataSource = MedinetBusinessLogicComponent.ParticipantProductsBLC.Instance.getProductPaged(this._editionId, letterToSearch);
            //this.grdProducts.DataBind();
            this.aspGrdProducts.DataSource = MedinetBusinessLogicComponent.ParticipantProductsBLC.Instance.getProductPaged(this._editionId, letterToSearch);
            this.aspGrdProducts.DataBind();
        }
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

    #endregion

    private int _editionId;
    private int _bookId;
    private int _countryId;
    private int _divisionId;
    private int _userId;
    private int _userCountry;
    private string _hashkey;
    private string _bookShortName;

   
   
    protected void aspGrdProducts_HtmlRowCreated(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewTableRowEventArgs e)
    {

        if (e.RowType == DevExpress.Web.ASPxGridView.GridViewRowType.Data)
        {
            GridView gv = aspGrdProducts.FindRowCellTemplateControl(e.VisibleIndex, null, "grdSymptoms") as GridView;
            
            TextBox txtPag = aspGrdProducts.FindRowCellTemplateControl(e.VisibleIndex, null, "txtPage") as TextBox;
            string textPage = (string)aspGrdProducts.GetRowValues(e.VisibleIndex, "Page");
            int textDiv = (int)aspGrdProducts.GetRowValues(e.VisibleIndex, "DivisionId");
            int textCat = (int)aspGrdProducts.GetRowValues(e.VisibleIndex, "CategoryId");
            int textPro = (int)aspGrdProducts.GetRowValues(e.VisibleIndex, "ProductId");
            int textPha = (int)aspGrdProducts.GetRowValues(e.VisibleIndex, "PharmaFormId");
            if (gv != null)
            {
                gv.DataSource = MedinetBusinessLogicComponent.CatalogsBLC.Instance.getSymptomsByProduct(textPro, textPha);
                gv.DataBind();
            }
            txtPag.Text = textPage;

            txtPag.Attributes.Add("divisionId", textDiv.ToString());
            txtPag.Attributes.Add("categoryId", textCat.ToString());
            txtPag.Attributes.Add("productId", textPro.ToString());
            txtPag.Attributes.Add("pharmaFormId", textPha.ToString());
        }
    }
   
  
}