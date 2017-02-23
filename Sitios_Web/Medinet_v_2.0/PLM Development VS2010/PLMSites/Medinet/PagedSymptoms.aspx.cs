using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PagedSymptoms : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this._editionId = this.Session["editionId"] == null ? -1 : Convert.ToInt32(this.Session["editionId"]);
        this._bookId = this.Session["bookId"] == null ? -1 : Convert.ToInt32(this.Session["bookId"]);
        this._countryId = this.Session["countryId"] == null ? -1 : Convert.ToInt32(this.Session["countryId"]);
        this._divisionId = this.Session["divisionId"] == null ? -1 : Convert.ToInt32(this.Session["divisionId"]);
        this._userId = this.Session["userId"] == null ? -1 : Convert.ToInt32(this.Session["userId"]);
        this._userCountry = this.Session["userCountry"] == null ? 0 : Convert.ToInt32(this.Session["userCountry"]);
        this._hashkey = System.Configuration.ConfigurationManager.AppSettings["HashKey"];
        btnSaveDown.Click += new EventHandler(btnSavePages_OnClick);
        if (this._editionId != -1 && this._countryId != -1)
        {
            this.getPagedProducts();
        }
    }
    protected void aspGrdSymptoms_HtmlRowCreated(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewTableRowEventArgs e)
    {

        if (e.RowType == DevExpress.Web.ASPxGridView.GridViewRowType.Data)
        {
            TextBox txtPag = aspGrdSymptoms.FindRowCellTemplateControl(e.VisibleIndex, null, "txtPage") as TextBox;
            string textPage = (string)aspGrdSymptoms.GetRowValues(e.VisibleIndex, "Page");
            int textSymptom = (int)aspGrdSymptoms.GetRowValues(e.VisibleIndex, "SymptomId");
            txtPag.Text = textPage;
            txtPag.Attributes.Add("SymptomId", textSymptom.ToString());
        }
    }

    private void getPagedProducts()
    {
        string letterToSearch = this.ddlAlphabet.SelectedValue == "-1" ? "%" : this.ddlAlphabet.SelectedValue + "%";

        if (this._editionId > 0)
        {
            this.aspGrdSymptoms.DataSource = MedinetBusinessLogicComponent.EditionSymptomBLC.Instance.getEditionSymptoms(this._editionId, letterToSearch);
            this.aspGrdSymptoms.DataBind();
        }
    }
    protected void ddlAlphabet_SelectedIndexChanged(object sender, EventArgs e)
    {
        string letterToSearch = this.ddlAlphabet.SelectedValue == "-1" ? "%" : this.ddlAlphabet.SelectedValue + "%";

        if (this._editionId > 0)
        {
            this.aspGrdSymptoms.DataSource = MedinetBusinessLogicComponent.EditionSymptomBLC.Instance.getEditionSymptoms(this._editionId, letterToSearch);
            this.aspGrdSymptoms.DataBind();
        }
    }

    protected void imgBtnBackLabs_Click(object sender, ImageClickEventArgs e)
    {
        this.Response.Redirect("laboratories.aspx");
    }

    protected void btnSavePages_OnClick(object sender, EventArgs e)
    {
        MedinetBusinessEntries.EditionSymptomInfo editionSymptom;
        TextBox txtPage;
        for (int gridRow = 0; gridRow < this.aspGrdSymptoms.VisibleRowCount; gridRow++)
        {
            int symptomId = (int)aspGrdSymptoms.GetRowValues(gridRow, "SymptomId");
            editionSymptom = new MedinetBusinessEntries.EditionSymptomInfo();
            txtPage = aspGrdSymptoms.FindRowCellTemplateControl(gridRow, null, "txtPage") as TextBox;
            if (txtPage != null)
            {
                editionSymptom.Page = txtPage.Text.Trim() == "" ? null : txtPage.Text.Trim();
                editionSymptom.SymptomId = symptomId;
                editionSymptom.EditionId = this._editionId;
                MedinetBusinessLogicComponent.EditionSymptomBLC.Instance.updateEditionSymptom(editionSymptom);
            }
        }
        this.getPagedProducts();
        ScriptManager.RegisterStartupScript(this, typeof(Page), "messageScript", "jAlert('Las paginas se guardaron con éxito', 'Registros guardados');", true);
    }

    protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.Session["CurrentPage"] = 0;
        if (this.ddlPageSize.SelectedValue != "-1")
        {
            this.aspGrdSymptoms.SettingsPager.Mode = DevExpress.Web.ASPxGridView.GridViewPagerMode.ShowPager;
            this.aspGrdSymptoms.SettingsPager.PageSize = Convert.ToInt32(this.ddlPageSize.SelectedValue);
        }
        else
        {
            this.aspGrdSymptoms.SettingsPager.Mode = DevExpress.Web.ASPxGridView.GridViewPagerMode.ShowAllRecords;
        }
        this.getPagedProducts();
    }

    private int _editionId;
    private int _bookId;
    private int _countryId;
    private int _divisionId;
    private int _userId;
    private int _userCountry;
    private string _hashkey;
}