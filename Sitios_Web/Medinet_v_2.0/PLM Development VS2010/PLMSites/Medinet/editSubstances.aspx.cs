using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.IO;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;

public partial class editSubstances : System.Web.UI.Page
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
            this.generateSubstanceXML();
            this.prepareGrid(Convert.ToInt16(this.ddlPageSize.SelectedValue));
            this.getSubstanceCatalog();
        }
    }

    #endregion

    #region Control Events

    //Grid Substance Catalog Events
    protected void grdSubstances_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            MedinetBusinessEntries.ActiveSubstanceInfo currentRow = (MedinetBusinessEntries.ActiveSubstanceInfo)e.Row.DataItem;
            ImageButton btnDeleteSubstance = (ImageButton)e.Row.FindControl("btnDeleteSubstance");

            //Button Delete Substance
            //btnDeleteSubstance.Attributes.Add("activeSubstanceId", currentRow.ActiveSubstanceId.ToString());
            //btnDeleteSubstance.Attributes.Add("onClick", "javascript:return confirmDelete('" + currentRow.Description + "')");
        }
    }

    protected void grdSubstances_RowEditing(object sender, GridViewEditEventArgs e)
    {
        this.grdSubstances.EditIndex = e.NewEditIndex;
        this.getSubstanceCatalog();
    }

    protected void grdSubstances_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        int activeSubstanceId = Convert.ToInt32(this.grdSubstances.DataKeys[e.RowIndex].Values[0].ToString());

        TextBox txtSubstanceName = (TextBox)this.grdSubstances.Rows[e.RowIndex].FindControl("txtSubstanceName");
        TextBox txtEnglishDescription = (TextBox)this.grdSubstances.Rows[e.RowIndex].FindControl("txtSubstanceEnglishName");

        //Valid SubstanceName
        if (txtSubstanceName.Text.Trim() == "")
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "messageScript", "jAlert('Debe ingresar un nombre.', 'Sustancia inválida');", true);
            return;
        }

        MedinetBusinessEntries.ActiveSubstanceInfo substanceInfo = new MedinetBusinessEntries.ActiveSubstanceInfo();
        substanceInfo.ActiveSubstanceId = activeSubstanceId;
        substanceInfo.Description = txtSubstanceName.Text.Trim().ToUpper();
        substanceInfo.EnglishDescription = txtEnglishDescription.Text.Trim() == "" ? null : txtEnglishDescription.Text.Trim().ToUpper();
        substanceInfo.Enunciative = false;
        substanceInfo.Active = true;

        MedinetBusinessLogicComponent.ActiveSubstancesBLC.Instance.updateActiveSubstance(substanceInfo, this._userId, this._hashkey);

        this.grdSubstances.EditIndex = -1;
        this.generateSubstanceXML();
        this.getSubstanceCatalog();
        ScriptManager.RegisterStartupScript(this, typeof(Page), "messageScript", "jAlert('Se actualizó correctamente la sustancia.', 'Sustancia actualizada');", true);
    }

    protected void grdSubstances_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        this.grdSubstances.PageIndex = e.NewPageIndex;
        this.Session["CurrentPage"] = Convert.ToInt32(e.NewPageIndex);
        this.getSubstanceCatalog();
    }

    protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.Session["CurrentPage"] = 0;
        this.prepareGrid(Convert.ToInt16(this.ddlPageSize.SelectedValue));
        this.getSubstanceCatalog();
    }

    //Grid Substitute Substances Events
    protected void grdSubstituteSubstances_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            MedinetBusinessEntries.ActiveSubstanceInfo currentRow = (MedinetBusinessEntries.ActiveSubstanceInfo)e.Row.DataItem;
            ImageButton btnReplaceSubstance = (ImageButton)e.Row.FindControl("btnReplaceSubstance");

            //Button
            btnReplaceSubstance.Attributes.Add("activeSubstanceId", currentRow.ActiveSubstanceId.ToString());
            btnReplaceSubstance.Attributes.Add("onClick", "javascript:return confirmSubstitute('" + currentRow.Description + "')");
        }
    }

    protected void grdSubstituteSubstances_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        this.grdSubstituteSubstances.PageIndex = e.NewPageIndex;
        this.Session["CurrentSubstitutePage"] = Convert.ToInt32(e.NewPageIndex);
        this.displayConfirmation(Convert.ToInt32(this.Session["oldSubstanceId"]));
    }

    protected void ddlPageSubstitute_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.Session["CurrentSubstitutePage"] = 0;
        this.prepareSubstituteGrid(Convert.ToInt16(this.ddlPageSubstitute.SelectedValue));
        this.displayConfirmation(Convert.ToInt32(this.Session["oldSubstanceId"]));
    }

    //Search Buttons Events
    protected void btnSearchSubstance_Click(object sender, EventArgs e)
    {
        this.prepareGrid(Convert.ToInt16(this.ddlPageSize.SelectedValue));
        this.getSubstanceCatalog();
        this.txtSearchSubstance.Focus();
    }

    protected void btnSearchSubstitute_Click(object sender, EventArgs e)
    {
        this.displayConfirmation(Convert.ToInt32(this.Session["oldSubstanceId"]));
    }

    //Back Button Event
    protected void imgBtnBackProducts_Click(object sender, ImageClickEventArgs e)
    {
        this.Response.Redirect("assignedProducts.aspx");
    }

    //Delete or Replace or Add Substance Buttons
    protected void btnAddNewSubstance_Click(object sender, EventArgs e)
    {
        string substanceName = this.txtNewSubstanceName.Text.Trim().ToUpper();
        string englishSubstance = this.txtNewEnglishDescription.Text.Trim().ToUpper();

        //Valid SubstanceName
        if (substanceName.Trim() == "")
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "messageScript", "jAlert('Debe ingresar un nombre.', 'Sustancia inválida');", true);
            return;
        }

        //Valid English Description
        if (englishSubstance.Trim() == "")
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "messageScript", "jAlert('Debe ingresar un nombre en inglés.', 'Nombre en inglés inválido');", true);
            return;
        }

        MedinetBusinessEntries.ActiveSubstanceInfo substanceInfo = MedinetBusinessLogicComponent.ActiveSubstancesBLC.Instance.checkSubstanceByName(substanceName);

        if (substanceInfo == null)
        {
            substanceInfo = new MedinetBusinessEntries.ActiveSubstanceInfo();
            substanceInfo.Description = substanceName.Trim().ToUpper();
            substanceInfo.EnglishDescription = englishSubstance.Trim().ToUpper();
            substanceInfo.Enunciative = false;
            substanceInfo.Active = true;
            MedinetBusinessLogicComponent.ActiveSubstancesBLC.Instance.addActiveSubstance(substanceInfo, this._userId, this._hashkey);

            this.generateSubstanceXML();
            this.getSubstanceCatalog();
            ScriptManager.RegisterStartupScript(this, typeof(Page), "messageScript", "jAlert('La sustancia se agregó con éxito.', 'Sustancia agregada');", true);
        }
        else
        {
            if (substanceInfo.Active == false)
            {
                substanceInfo.Active = true;
                MedinetBusinessLogicComponent.ActiveSubstancesBLC.Instance.updateActiveSubstance(substanceInfo, this._userId, this._hashkey);
                this.generateSubstanceXML();
                this.getSubstanceCatalog();
                ScriptManager.RegisterStartupScript(this, typeof(Page), "messageScript", "jAlert('La sustancia se agregó con éxito.', 'Sustancia agregada');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "messageScript", "jAlert('La sustancia ya existe en el catálogo.', 'Sustancia existente');", true);
                return;
            }
        }
        
        this.txtNewSubstanceName.Text = "";
        this.txtNewEnglishDescription.Text = "";
    }

    protected void btnDeleteSubstance_OnClick(object sender, ImageClickEventArgs e)
    {
        int activeSubstanceId;
        ImageButton btnDeleteSubstance = (ImageButton)sender;

        activeSubstanceId = Convert.ToInt32(btnDeleteSubstance.Attributes["activeSubstanceId"]);

        List<MedinetBusinessEntries.ProductContentSubstancesInfo> productContentSubstances =
                MedinetBusinessLogicComponent.ProductContentSubstancesBLC.Instance.getProductContentSubstances(activeSubstanceId);

        if (productContentSubstances.Count() == 0 && MedinetBusinessLogicComponent.ProductSubstancesBLC.Instance.checkSubstance(activeSubstanceId) == false)
        {
            MedinetBusinessEntries.ActiveSubstanceInfo activeSubstance = MedinetBusinessLogicComponent.ActiveSubstancesBLC.Instance.getActiveSubstance(activeSubstanceId);
            activeSubstance.Active = false;

            MedinetBusinessLogicComponent.ActiveSubstancesBLC.Instance.updateActiveSubstance(activeSubstance, this._userId, this._hashkey);
            this.getSubstanceCatalog();
            this.tdConfirmDelete.Visible = false;
            ScriptManager.RegisterStartupScript(this, typeof(Page), "messageScript", "jAlert('La sustancia se eliminó con éxito.', 'Sustancia eliminada');", true);
        }
        else
        {
            this.displayConfirmation(activeSubstanceId);
        }
    }

    protected void btnConfirmDelete_OnClick(object sender, EventArgs e)
    {
        int substanceId;
        substanceId = Convert.ToInt32(this.Session["oldSubstanceId"]);

        this.removeOtherInteractions(substanceId);
        this.removeProductInteractions(substanceId);
        this.removePharmaGroupInteractions(substanceId);
        this.removeProductContentSubstances(substanceId);
        this.removeProductSubstances(substanceId);

        MedinetBusinessEntries.ActiveSubstanceInfo substanceInfo =
            MedinetBusinessLogicComponent.ActiveSubstancesBLC.Instance.getActiveSubstance(substanceId);

        substanceInfo.Active = false;

        MedinetBusinessLogicComponent.ActiveSubstancesBLC.Instance.updateActiveSubstance(substanceInfo, this._userId, this._hashkey);

        this.getSubstanceCatalog();
        this.tdConfirmDelete.Visible = false;
        ScriptManager.RegisterStartupScript(this, typeof(Page), "messageScript", "jAlert('La sustancia se eliminó con éxito.', 'Sustancia eliminada');", true);
    }

    protected void btnCancelDelete_OnClick(object sender, EventArgs e)
    {
        this.Session["oldSubstanceId"] = null;
        this.tdConfirmDelete.Visible = false;
    }

    protected void btnReplaceSubstance_OnClick(object sender, ImageClickEventArgs e)
    {
        int substituteSubstanceId;
        int oldSubstanceId = Convert.ToInt32(this.Session["oldSubstanceId"]);
        ImageButton btnReplaceSubstance = (ImageButton)sender;
        MedinetBusinessEntries.ProductSubstanceInfo productSubstanceToAdd;
        MedinetBusinessEntries.ProductContentSubstancesInfo productContentSubstanceToAdd;

        substituteSubstanceId = Convert.ToInt32(btnReplaceSubstance.Attributes["activeSubstanceId"]);

        //Add new record in ProductSubstances
        List<MedinetBusinessEntries.ProductSubstanceInfo> productSubstances =
            MedinetBusinessLogicComponent.ProductSubstancesBLC.Instance.getProductSubstances(oldSubstanceId);

        foreach (MedinetBusinessEntries.ProductSubstanceInfo productSubstanceItem in productSubstances)
        {
            productSubstanceToAdd = productSubstanceItem;
            productSubstanceToAdd.ActiveSubstanceId = substituteSubstanceId;

            MedinetBusinessLogicComponent.ProductSubstancesBLC.Instance.addSubstance(productSubstanceToAdd, this._userId, this._hashkey);
        }

        //Add new record in ProductContentSubstances
        List<MedinetBusinessEntries.ProductContentSubstancesInfo> productContentSubstances =
            MedinetBusinessLogicComponent.ProductContentSubstancesBLC.Instance.getProductContentSubstances(oldSubstanceId);

        foreach (MedinetBusinessEntries.ProductContentSubstancesInfo productContentItem in productContentSubstances)
        {
            productContentSubstanceToAdd = productContentItem;
            productContentSubstanceToAdd.ActiveSubstanceId = substituteSubstanceId;

            MedinetBusinessLogicComponent.ProductContentSubstancesBLC.Instance.addProductContentSubstance(productContentSubstanceToAdd, this._userId, this._hashkey);
        }

        //Substitute OtherInteractions
        List<MedinetBusinessEntries.OtherInteractionsInfo> otherInteractions =
            MedinetBusinessLogicComponent.OtherInteractionsBLC.Instance.getOtherInteractions(oldSubstanceId);

        foreach (MedinetBusinessEntries.OtherInteractionsInfo otherInteractionItem in otherInteractions)
        {
            //Delete Old Interact Substance
            MedinetBusinessLogicComponent.OtherInteractionsBLC.Instance.removeOtherInteraction(otherInteractionItem, this._userId, this._hashkey);

            otherInteractionItem.ActiveSubstanceId = substituteSubstanceId;
            
            //Add Substitute Substance
            MedinetBusinessLogicComponent.OtherInteractionsBLC.Instance.addOtherInteraction(otherInteractionItem, this._userId, this._hashkey);
        }

        //Substitute PharmaGroupInteractions
        List<MedinetBusinessEntries.PharmaGroupInteractionsInfo> pharmaGroupInteractions =
            MedinetBusinessLogicComponent.PharmaGroupInteractionsBLC.Instance.getPharmaGroupInteractions(oldSubstanceId);

        foreach (MedinetBusinessEntries.PharmaGroupInteractionsInfo pharmaGorupItem in pharmaGroupInteractions)
        {
            //Delete Old Interact Substance
            MedinetBusinessLogicComponent.PharmaGroupInteractionsBLC.Instance.removePharmaGroupInteraction(pharmaGorupItem, this._userId, this._hashkey);

            pharmaGorupItem.ActiveSubstanceId = substituteSubstanceId;

            //Add Substitute Substance
            MedinetBusinessLogicComponent.PharmaGroupInteractionsBLC.Instance.addPharmaGroupInteraction(pharmaGorupItem, this._userId, this._hashkey);
        }

        //Substitute ProductInteractions
        List<MedinetBusinessEntries.ProductInteractionsInfo> productInteractions =
            MedinetBusinessLogicComponent.ProductInteractionsBLC.Instance.getProductInteractions(oldSubstanceId);

        foreach (MedinetBusinessEntries.ProductInteractionsInfo productInteractionItem in productInteractions)
        {
            //Delete Old ProductInteraction
            MedinetBusinessLogicComponent.ProductInteractionsBLC.Instance.removeProductInteraction(productInteractionItem, this._userId, this._hashkey);

            productInteractionItem.ActiveSubstanceId = substituteSubstanceId;

            //Add SubstituteSubstance
            MedinetBusinessLogicComponent.ProductInteractionsBLC.Instance.addProductInteraction(productInteractionItem, this._userId, this._hashkey);
        }

        //To finish, delete the exist records in ProductContentSubstances
        foreach (MedinetBusinessEntries.ProductContentSubstancesInfo productContentItem in productContentSubstances)
        {
            MedinetBusinessLogicComponent.ProductContentSubstancesBLC.Instance.removeProductContentSubstance(productContentItem, this._userId, this._hashkey);
        }

        //To finish, delete the exist records in ProductSubstances
        foreach (MedinetBusinessEntries.ProductSubstanceInfo productSubstanceItem in productSubstances)
        {
            MedinetBusinessLogicComponent.ProductSubstancesBLC.Instance.removeSubstance(productSubstanceItem, this._userId, this._hashkey);
        }

        //To finish, inactivate the Active Substance
        MedinetBusinessEntries.ActiveSubstanceInfo activeSubstance = MedinetBusinessLogicComponent.ActiveSubstancesBLC.Instance.getActiveSubstance(oldSubstanceId);
        activeSubstance.Active = false;
        MedinetBusinessLogicComponent.ActiveSubstancesBLC.Instance.updateActiveSubstance(activeSubstance, this._userId, this._hashkey);

        this.getSubstanceCatalog();
        this.tdConfirmDelete.Visible = false;
        ScriptManager.RegisterStartupScript(this, typeof(Page), "messageScript", "jAlert('La sustancia se sustituyó con éxito.', 'Sustancia sustituida');", true);
    }

    #endregion

    #region Service Methods

    [System.Web.Services.WebMethod(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static List<string> getSubstanceList(string prefixText, int count, string contextKey)
    {
        List<string> substanceList;

        DataSet substanceSet = new DataSet();

        byte[] byteArray = Encoding.UTF8.GetBytes(xmlSubstances.InnerXml);
        MemoryStream stream = new MemoryStream(byteArray);

        substanceSet.ReadXml(stream);

        var result = from substance in substanceSet.Tables["Substance"].AsEnumerable()
                     where substance.Field<string>("CleanSubstanceName").ToString().Contains(prefixText)
                     orderby substance.Field<string>("SubstanceName")
                     select substance.Field<string>("SubstanceName").ToString();

        IEnumerable<string> collect = result.Take(Convert.ToInt32(count));
        substanceList = collect.ToList();
        return substanceList;
    }

    #endregion

    #region Private Methods

    private void getSubstanceCatalog()
    {
        string substanceToSearch = string.IsNullOrEmpty(this.txtSearchSubstance.Text.Trim()) ? "%" : this.txtSearchSubstance.Text.Trim();
        this.grdSubstances.DataSource = MedinetBusinessLogicComponent.CatalogsBLC.Instance.getAllSubstancesByFilter(substanceToSearch);
        this.grdSubstances.DataBind();
    }

    protected string displaySubstanceName(string substanceName)
    {
        return string.IsNullOrEmpty(substanceName) ? "" : substanceName;
    }

    protected string displaySubstanceEnglishName(string substanceEnglishName)
    {
        return string.IsNullOrEmpty(substanceEnglishName) ? "" : substanceEnglishName;
    }

    private void displayConfirmation(int substanceId)
    {
        this.Session["oldSubstanceId"] = substanceId;
        this.prepareSubstituteGrid(Convert.ToInt16(this.ddlPageSubstitute.SelectedValue));
        this.lblSubstanceToDelete.Text = MedinetBusinessLogicComponent.ActiveSubstancesBLC.Instance.getActiveSubstance(substanceId).Description;

        if (this.txtSubstituteSubstance.Text.Trim() != "")
        {
            this.grdSubstituteSubstances.DataSource = MedinetBusinessLogicComponent.ActiveSubstancesBLC.Instance.getAllSubstancesByFilter(substanceId, this.txtSubstituteSubstance.Text.Trim() + '%');
            this.grdSubstituteSubstances.DataBind();
        }
        else
        {
            this.grdSubstituteSubstances.DataSource = MedinetBusinessLogicComponent.ActiveSubstancesBLC.Instance.getAllSubstancesByFilter(substanceId);
            this.grdSubstituteSubstances.DataBind();
        }

        this.tdConfirmDelete.Visible = true;
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

    private void prepareSubstituteGrid(short pagesize)
    {
        if (pagesize > 0)
        {
            this.grdSubstituteSubstances.AllowPaging = true;
            this.grdSubstituteSubstances.PageSize = pagesize;
            this.grdSubstituteSubstances.PageIndex = this.Session["CurrentSubstitutePage"] == null ? 0 : Convert.ToInt32(this.Session["CurrentSubstitutePage"]);
        }
        else
            this.grdSubstituteSubstances.AllowPaging = false;
    }

    private void generateSubstanceXML()
    {
        string cleanSubstanceName;

        List<MedinetBusinessEntries.ActiveSubstanceInfo> activeSubstances =
            activeSubstances = MedinetBusinessLogicComponent.CatalogsBLC.Instance.getAllSubstancesByFilter("%");

        DataTable dataTable = new DataTable("Substance");
        dataTable.Columns.Add("ActiveSubstanceId");
        dataTable.Columns.Add("CleanSubstanceName");
        dataTable.Columns.Add("SubstanceName");

        foreach (MedinetBusinessEntries.ActiveSubstanceInfo item in activeSubstances)
        {
            cleanSubstanceName = this.cleanSubstanceName(item.Description);
            item.Description.Replace("&", "&amp;");
            item.Description.Replace("™", "");

            dataTable.Rows.Add(item.ActiveSubstanceId, cleanSubstanceName, item.Description);
        }

        dataSetSubstances = new DataSet("Root");
        dataSetSubstances.Tables.Add(dataTable);

        xmlSubstances = new XmlDocument();
        xmlSubstances.InnerXml = dataSetSubstances.GetXml();
    }

    private string cleanSubstanceName(string originalString)
    {
        StringBuilder sb = new StringBuilder(originalString.ToUpper());

        sb.Replace("Á", "A");
        sb.Replace("É", "E");
        sb.Replace("Í", "I");
        sb.Replace("Ó", "O");
        sb.Replace("Ú", "U");
        sb.Replace("Ñ", "N");
        sb.Replace("Ü", "U");
        sb.Replace("&", "");
        sb.Replace("/", "");
        sb.Replace("™", "");
        sb.Replace(":", "");
        sb.Replace(".", "");
        sb.Replace(";", "");
        sb.Replace(",", "");
        sb.Replace("'", "");
        sb.Replace("\"", "");
        sb.Replace("+", "");
        sb.Replace("*", "");
        sb.Replace("(", "");
        sb.Replace(")", "");
        sb.Replace(">", "");
        sb.Replace("<", "");
        sb.Replace("´", "");
        sb.Replace("[", "");
        sb.Replace("]", "");
        sb.Replace("{", "");
        sb.Replace("}", "");
        sb.Replace("@", "");
        sb.Replace("?", "");
        sb.Replace("%", "");
        sb.Replace("Â", "A");
        sb.Replace("Ê", "E");
        sb.Replace("Î", "I");
        sb.Replace("Ô", "O");
        sb.Replace("Û", "U");
        //sb.Replace(" ", "_");
        //sb.Replace("__", "_");
        //sb.Replace("__", "_");

        return sb.ToString().ToLower();
    }

    private void removeOtherInteractions(int activeSubstanceId)
    {
        List<MedinetBusinessEntries.OtherInteractionsInfo> otherInteractionList =
            MedinetBusinessLogicComponent.OtherInteractionsBLC.Instance.getOtherInteractions(activeSubstanceId);

        if (otherInteractionList.Count() > 0)
        {
            foreach (MedinetBusinessEntries.OtherInteractionsInfo otherInteractionItem in otherInteractionList)
                MedinetBusinessLogicComponent.OtherInteractionsBLC.Instance.removeOtherInteraction(otherInteractionItem, this._userId, this._hashkey);
        }
    }

    private void removeProductInteractions(int activeSubstanceId)
    {
        List<MedinetBusinessEntries.ProductInteractionsInfo> productInteractionList =
            MedinetBusinessLogicComponent.ProductInteractionsBLC.Instance.getProductInteractions(activeSubstanceId);

        if (productInteractionList.Count() > 0)
        {
            foreach (MedinetBusinessEntries.ProductInteractionsInfo productInteractionItem in productInteractionList)
                MedinetBusinessLogicComponent.ProductInteractionsBLC.Instance.removeProductInteraction(productInteractionItem, this._userId, this._hashkey);
        }
    }

    private void removePharmaGroupInteractions(int activeSubstanceId)
    {
        List<MedinetBusinessEntries.PharmaGroupInteractionsInfo> pharmaGroupInteractionList =
            MedinetBusinessLogicComponent.PharmaGroupInteractionsBLC.Instance.getPharmaGroupInteractions(activeSubstanceId);

        if (pharmaGroupInteractionList.Count() > 0)
        {
            foreach (MedinetBusinessEntries.PharmaGroupInteractionsInfo pharmaGroupInteractionItem in pharmaGroupInteractionList)
                MedinetBusinessLogicComponent.PharmaGroupInteractionsBLC.Instance.removePharmaGroupInteraction(pharmaGroupInteractionItem, this._userId, this._hashkey);
        }
    }

    private void removeProductContentSubstances(int activeSubstanceId)
    {
        List<MedinetBusinessEntries.ProductContentSubstancesInfo> productContentSubstanceList =
            MedinetBusinessLogicComponent.ProductContentSubstancesBLC.Instance.getProductContentSubstances(activeSubstanceId);

        if (productContentSubstanceList.Count() > 0)
        {
            foreach (MedinetBusinessEntries.ProductContentSubstancesInfo productContentSubstanceItem in productContentSubstanceList)
                MedinetBusinessLogicComponent.ProductContentSubstancesBLC.Instance.removeProductContentSubstance(productContentSubstanceItem, this._userId, this._hashkey);
        }
    }

    private void removeProductSubstances(int activeSubstanceId)
    {
        List<MedinetBusinessEntries.ProductSubstanceInfo> productSubstanceList =
            MedinetBusinessLogicComponent.ProductSubstancesBLC.Instance.getProductSubstances(activeSubstanceId);

        if (productSubstanceList.Count() > 0)
        {
            foreach (MedinetBusinessEntries.ProductSubstanceInfo productSubstanceItem in productSubstanceList)
                MedinetBusinessLogicComponent.ProductSubstancesBLC.Instance.removeSubstance(productSubstanceItem, this._userId, this._hashkey);
        }
    }

    #endregion

    private int _editionId;
    private int _countryId;
    private int _userId;
    private int _userCountry;
    private string _hashkey;
    private string _gridStatus;
    public static DataSet dataSetSubstances;
    public static XmlDocument xmlSubstances;

}