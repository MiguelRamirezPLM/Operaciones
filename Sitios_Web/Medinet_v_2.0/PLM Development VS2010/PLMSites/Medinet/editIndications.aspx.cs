using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class editIndications : System.Web.UI.Page
{

    #region Page Events

    protected void Page_Load(object sender, EventArgs e)
    {
        this._editionId = this.Session["editionId"] == null ? -1 : Convert.ToInt32(this.Session["editionId"]);
        this._countryId = this.Session["countryId"] == null ? -1 : Convert.ToInt32(this.Session["countryId"]);
        this._userId = this.Session["userId"] == null ? -1 : Convert.ToInt32(this.Session["userId"]);
        this._userCountry = this.Session["userCountry"] == null ? 0 : Convert.ToInt32(this.Session["userCountry"]);
        this._hashkey = System.Configuration.ConfigurationManager.AppSettings["HashKey"];
        this._gridStatus = this.Session["gridStatus"] == null ? "" : this.Session["gridStatus"].ToString();

        if (!IsPostBack)
        {   
            this.prepareGrid(Convert.ToInt16(this.ddlPageSize.SelectedValue));
            this.getIndicationCatalog();
        }
    }

    #endregion

    #region Control Events

    protected void grdIndications_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            MedinetBusinessEntries.IndicationInfo currentRow = (MedinetBusinessEntries.IndicationInfo)e.Row.DataItem;
            ImageButton btnDeleteIndication = (ImageButton)e.Row.FindControl("btnDeleteIndication");

            //Button Delete Indication
            //btnDeleteIndication.Attributes.Add("indicationId", currentRow.IndicationId.ToString());
            //btnDeleteIndication.Attributes.Add("onClick", "javascript:return confirmDelete('" + currentRow.Description + "')");
        }
    }

    protected void grdIndications_RowEditing(object sender, GridViewEditEventArgs e)
    {
        this.grdIndications.EditIndex = e.NewEditIndex;
        this.getIndicationCatalog();
    }

    protected void grdIndications_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        int indicationId = Convert.ToInt32(this.grdIndications.DataKeys[e.RowIndex].Values[0].ToString());

        TextBox txtIndicationName = (TextBox)this.grdIndications.Rows[e.RowIndex].FindControl("txtIndicationName");

        //Valid IndicationName
        if (txtIndicationName.Text.Trim() == "")
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "messageScript", "jAlert('Debe ingresar un nombre para la Indicación.', 'Indicación inválida');", true);
            return;
        }

        MedinetBusinessEntries.IndicationInfo indicationInfo = MedinetBusinessLogicComponent.IndicationsBLC.Instance.getIndication(indicationId);
        indicationInfo.Description = txtIndicationName.Text.Trim().ToUpper();
        indicationInfo.Active = true;
        MedinetBusinessLogicComponent.IndicationsBLC.Instance.updateIndication(indicationInfo, this._userId, this._hashkey);

        this.grdIndications.EditIndex = -1;
        this.getIndicationCatalog();
        ScriptManager.RegisterStartupScript(this, typeof(Page), "messageScript", "jAlert('Se actualizó correctamente la indicación.', 'Indicación actualizada');", true);
    }

    protected void grdIndications_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        this.grdIndications.PageIndex = e.NewPageIndex;
        this.Session["CurrentPage"] = Convert.ToInt32(e.NewPageIndex);
        this.getIndicationCatalog();
    }

    protected void grdSubstituteIndications_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            MedinetBusinessEntries.IndicationInfo currentRow = (MedinetBusinessEntries.IndicationInfo)e.Row.DataItem;
            ImageButton btnReplaceIndication = (ImageButton)e.Row.FindControl("btnReplaceIndication");

            //Button
            btnReplaceIndication.Attributes.Add("indicationId", currentRow.IndicationId.ToString());
            btnReplaceIndication.Attributes.Add("onClick", "javascript:return confirmSubstitute('" + currentRow.Description + "')");
        }
    }

    protected void grdSubstituteIndications_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        this.grdSubstituteIndications.PageIndex = e.NewPageIndex;
        this.Session["CurrentSubstitutePage"] = Convert.ToInt32(e.NewPageIndex);
        this.displayConfirmSubstitute(Convert.ToInt32(this.Session["oldIndicationId"]));
    }

    protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.Session["CurrentPage"] = 0;
        this.prepareGrid(Convert.ToInt16(this.ddlPageSize.SelectedValue));
        this.getIndicationCatalog();
    }

    protected void ddlPageSubstitute_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.Session["CurrentSubstitutePage"] = 0;
        this.prepareSubstituteGrid(Convert.ToInt16(this.ddlPageSubstitute.SelectedValue));
        this.displayConfirmSubstitute(Convert.ToInt32(this.Session["oldIndicationId"]));
    }

    protected void btnSearchIndication_Click(object sender, EventArgs e)
    {
        this.prepareGrid(Convert.ToInt16(this.ddlPageSize.SelectedValue));
        this.getIndicationCatalog();
        this.txtSearchIndication.Focus();
    }

    protected void imgBtnBackProducts_Click(object sender, ImageClickEventArgs e)
    {
        this.Response.Redirect("assignedProducts.aspx");
    }

    protected void btnDeleteIndication_OnClick(object sender, ImageClickEventArgs e)
    {
        int indicationId;
        ImageButton btnDeleteIndication = (ImageButton)sender;

        indicationId = Convert.ToInt32(btnDeleteIndication.Attributes["indicationId"]);

        List<MedinetBusinessEntries.ProductIndicationInfo> productIndicationList =
            MedinetBusinessLogicComponent.ProductIndicationsBLC.Instance.getProductIndications(indicationId);

        if (productIndicationList.Count <= 0)
        {
            MedinetBusinessEntries.IndicationInfo indicationInfo = MedinetBusinessLogicComponent.IndicationsBLC.Instance.getIndication(indicationId);
            indicationInfo.Active = false;

            MedinetBusinessLogicComponent.IndicationsBLC.Instance.updateIndication(indicationInfo, this._userId, this._hashkey);
            this.getIndicationCatalog();
            this.tdConfirmSubstitute.Visible = false;
            ScriptManager.RegisterStartupScript(this, typeof(Page), "messageScript", "jAlert('La indicación se eliminó con éxito.', 'Indicación eliminada');", true);
        }
        else
        {
            this.displayConfirmSubstitute(indicationId);
        }

    }

    protected void btnAddNewIndication_Click(object sender, EventArgs e)
    {
        string indicationName = this.txtNewIndicationName.Text.ToUpper().Trim();

        //Valid SubstanceName
        if (indicationName.Trim() == "")
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "messageScript", "jAlert('Debe ingresar un nombre.', 'Indicación inválida');", true);
            return;
        }

        List<MedinetBusinessEntries.IndicationInfo> indicationList = MedinetBusinessLogicComponent.CatalogsBLC.Instance.getAllIndicationsByFilter(indicationName);

        if (indicationList.Count <= 0)
        {
            MedinetBusinessEntries.IndicationInfo indicationInfo = new MedinetBusinessEntries.IndicationInfo();
            indicationInfo.ParentId = null;
            indicationInfo.Description = indicationName;
            indicationInfo.Active = true;
            MedinetBusinessLogicComponent.IndicationsBLC.Instance.addIndication(indicationInfo, this._userId, this._hashkey);

            this.getIndicationCatalog();
            this.tdExistsIndications.Visible = false;
            this.txtNewIndicationName.Text = "";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "messageScript", "jAlert('La indicación se agregó con éxito.', 'Indicación agregada');", true);
        }
        else
        {
            this.grdExistsIndications.DataSource = indicationList;
            this.grdExistsIndications.DataBind();
            this.tdExistsIndications.Visible = true;
            this.txtNewIndicationName.Enabled = false;
        }
    }

    protected void btnConfirmIndication_Click(object sender, EventArgs e)
    {
        string indicationName = this.txtNewIndicationName.Text.ToUpper().Trim();

        //Valid SubstanceName
        if (indicationName.Trim() == "")
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "messageScript", "jAlert('Debe ingresar un nombre.', 'Indicación inválida');", true);
            return;
        }

        MedinetBusinessEntries.IndicationInfo indicationInfo = new MedinetBusinessEntries.IndicationInfo();
        indicationInfo.ParentId = null;
        indicationInfo.Description = indicationName;
        indicationInfo.Active = true;
        MedinetBusinessLogicComponent.IndicationsBLC.Instance.addIndication(indicationInfo, this._userId, this._hashkey);

        this.tdExistsIndications.Visible = false;
        this.txtNewIndicationName.Text = "";
        this.txtNewIndicationName.Enabled = true;
        this.getIndicationCatalog();
        ScriptManager.RegisterStartupScript(this, typeof(Page), "messageScript", "jAlert('La indicación se agregó con éxito.', 'Indicación agregada');", true);
    }

    protected void btnCancelIndication_Click(object sender, EventArgs e)
    {
        this.tdExistsIndications.Visible = false;
        this.txtNewIndicationName.Text = "";
        this.txtNewIndicationName.Enabled = true;
    }

    protected void btnConfirmDelete_OnClick(object sender, EventArgs e)
    {
        int indicationId;
        indicationId = Convert.ToInt32(this.Session["oldIndicationId"]);

        this.removeProductIndications(indicationId);

        MedinetBusinessEntries.IndicationInfo indicationInfo =
            MedinetBusinessLogicComponent.IndicationsBLC.Instance.getIndication(indicationId);
        
        indicationInfo.Active = false;
        MedinetBusinessLogicComponent.IndicationsBLC.Instance.updateIndication(indicationInfo, this._userId, this._hashkey);

        this.getIndicationCatalog();
        this.tdConfirmSubstitute.Visible = false;
        ScriptManager.RegisterStartupScript(this, typeof(Page), "messageScript", "jAlert('La Indicación se eliminó con éxito.', 'Indicación eliminada');", true);
    }

    protected void btnCancelDelete_OnClick(object sender, EventArgs e)
    {
        this.Session["oldIndicationId"] = null;
        this.tdConfirmSubstitute.Visible = false;
    }

    protected void btnSearchSubstitute_Click(object sender, EventArgs e)
    {
        this.displayConfirmSubstitute(Convert.ToInt32(this.Session["oldIndicationId"]));
    }

    protected void btnReplaceIndication_OnClick(object sender, ImageClickEventArgs e)
    {
        int substituteIndicationId;
        int oldIndicationId = Convert.ToInt32(this.Session["oldIndicationId"]);
        ImageButton btnReplaceIndication = (ImageButton)sender;
        MedinetBusinessEntries.ProductIndicationInfo productIndicationToAdd;

        substituteIndicationId = Convert.ToInt32(btnReplaceIndication.Attributes["indicationId"]);

        List<MedinetBusinessEntries.ProductIndicationInfo> productIndications =
            MedinetBusinessLogicComponent.ProductIndicationsBLC.Instance.getProductIndications(oldIndicationId);

        foreach (MedinetBusinessEntries.ProductIndicationInfo productIndicationItem in productIndications)
        {
            productIndicationToAdd = productIndicationItem;
            productIndicationToAdd.IndicationId = substituteIndicationId;

            //Add new record in ProductIndication with the substitute indication
            MedinetBusinessLogicComponent.ProductIndicationsBLC.Instance.addIndication(productIndicationToAdd, this._userId, this._hashkey);

            //Delete old record in ProductIndication with the old indication
            MedinetBusinessLogicComponent.ProductIndicationsBLC.Instance.removeIndication(productIndicationItem, this._userId, this._hashkey);
        }
        this.getIndicationCatalog();
        this.tdConfirmSubstitute.Visible = false;
        ScriptManager.RegisterStartupScript(this, typeof(Page), "messageScript", "jAlert('La indicación se sustituyó con éxito.', 'Indicación sustituida');", true);
    }

    #endregion

    #region Private Methods

    private void getIndicationCatalog()
    {
        string indicationToSearch = string.IsNullOrEmpty(this.txtSearchIndication.Text.Trim()) ? "" : this.txtSearchIndication.Text.Trim();
        this.grdIndications.DataSource = MedinetBusinessLogicComponent.CatalogsBLC.Instance.getAllIndicationsByFilter(indicationToSearch);
        this.grdIndications.DataBind();
    }

    private void prepareGrid(short pagesize)
    {
        if (pagesize > 0)
        {
            this.grdIndications.AllowPaging = true;
            this.grdIndications.PageSize = pagesize;
            this.grdIndications.PageIndex = this.Session["CurrentPage"] == null ? 0 : Convert.ToInt32(this.Session["CurrentPage"]);
        }
        else
            this.grdIndications.AllowPaging = false;
    }

    private void prepareSubstituteGrid(short pagesize)
    {
        if (pagesize > 0)
        {
            this.grdSubstituteIndications.AllowPaging = true;
            this.grdSubstituteIndications.PageSize = pagesize;
            this.grdSubstituteIndications.PageIndex = this.Session["CurrentSubstitutePage"] == null ? 0 : Convert.ToInt32(this.Session["CurrentSubstitutePage"]);
        }
        else
            this.grdSubstituteIndications.AllowPaging = false;
    }

    protected string displayIndicationName(string indicationName)
    {
        return string.IsNullOrEmpty(indicationName) ? "" : indicationName;
    }

    protected void displayConfirmSubstitute(int indicationId)
    {
        this.Session["oldIndicationId"] = indicationId;
        this.prepareSubstituteGrid(Convert.ToInt16(this.ddlPageSubstitute.SelectedValue));

        if (this.txtSubstituteIndication.Text.Trim() != "")
        {
            this.grdSubstituteIndications.DataSource = MedinetBusinessLogicComponent.IndicationsBLC.Instance.getAllIndicationsByFilter(indicationId, this.txtSubstituteIndication.Text.Trim());
            this.grdSubstituteIndications.DataBind();
        }
        else
        {
            this.grdSubstituteIndications.DataSource = MedinetBusinessLogicComponent.IndicationsBLC.Instance.getAllIndicationsByFilter(indicationId, "");
            this.grdSubstituteIndications.DataBind();
        }
        this.tdConfirmSubstitute.Visible = true;
    }

    private void removeProductIndications(int indicationId)
    {
        List<MedinetBusinessEntries.ProductIndicationInfo> productIndications =
            MedinetBusinessLogicComponent.ProductIndicationsBLC.Instance.getProductIndications(indicationId);

        if (productIndications.Count > 0)
        {
            foreach (MedinetBusinessEntries.ProductIndicationInfo productIndicationItem in productIndications)
                MedinetBusinessLogicComponent.ProductIndicationsBLC.Instance.removeIndication(productIndicationItem, this._userId, this._hashkey);
        }
    }

    #endregion

    private int _editionId;
    private int _countryId;
    private int _userId;
    private int _userCountry;
    private string _hashkey;
    private string _gridStatus;
}