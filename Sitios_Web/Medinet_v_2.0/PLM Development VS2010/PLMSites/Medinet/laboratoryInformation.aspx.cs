using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

public partial class laboratoryInformation : System.Web.UI.Page
{

    #region Page Events

    protected void Page_Load(object sender, EventArgs e)
    {
        this._divisionId = this.Session["divisionId"] == null ? -1 : Convert.ToInt32(this.Session["divisionId"]);
        this._userId = this.Session["userId"] == null ? -1 : Convert.ToInt32(this.Session["userId"]);
        this._hashkey = System.Configuration.ConfigurationManager.AppSettings["HashKey"];

        if (!IsPostBack)
        {
            this.getDivision();
            this.getDivisionInformation();
        }
    }

    #endregion

    #region ControlEvents

    protected void grdDivision_RowEditing(object sender, GridViewEditEventArgs e)
    {
        this.grdDivision.EditIndex = e.NewEditIndex;
        this.getDivision();
    }

    protected void grdDivision_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        TextBox divisionDescription = (TextBox)this.grdDivision.Rows[e.RowIndex].FindControl("txtDivisionDescription");
        TextBox divisionShortName = (TextBox)this.grdDivision.Rows[e.RowIndex].FindControl("txtDivisionShortName");
        MedinetBusinessEntries.DivisionsInfo divisionInfo = MedinetBusinessLogicComponent.DivisionsBLC.Instance.getDivision(this._divisionId);

        if (!string.IsNullOrEmpty(divisionDescription.Text.Trim()))
        {
            if (!string.IsNullOrEmpty(divisionShortName.Text.Trim()))
            {
                divisionInfo.Description = divisionDescription.Text.Trim().ToUpper();
                divisionInfo.ShortName = divisionShortName.Text.Trim().ToUpper();

                MedinetBusinessLogicComponent.DivisionsBLC.Instance.updateDivision(divisionInfo, this._userId, this._hashkey);

                this.grdDivision.EditIndex = -1;
                this.getDivision();
                ScriptManager.RegisterStartupScript(this, typeof(Page), "messageScript", "jAlert('El laboratorio fue actualizado', 'Laboratorio Actualizado');", true);
            }
            else
                ScriptManager.RegisterStartupScript(this, typeof(Page), "messageScript", "jAlert('Debe escribir un nombre corto', 'Nombre corto');", true);
        }
        else
            ScriptManager.RegisterStartupScript(this, typeof(Page), "messageScript", "jAlert('Debe escribir una razón social', 'Razón social');", true);
    }

    protected void grdDivisionInformation_RowEditing(object sender, GridViewEditEventArgs e)
    {
        this.grdDivisionInformation.EditIndex = e.NewEditIndex;
        this.getDivisionInformation();
    }

    protected void grdDivisionInformation_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        MedinetBusinessEntries.DivisionInformationInfo divisionInformation = new MedinetBusinessEntries.DivisionInformationInfo();
        divisionInformation = MedinetBusinessLogicComponent.DivisionInformationBLC.Instance.getInformationByDivision(this._divisionId);

        TextBox divisionAddress = (TextBox)this.grdDivisionInformation.Rows[e.RowIndex].FindControl("txtDivisionAddress");
        TextBox divisionSuburb = (TextBox)this.grdDivisionInformation.Rows[e.RowIndex].FindControl("txtDivisionSuburb");
        TextBox divisionZipCode = (TextBox)this.grdDivisionInformation.Rows[e.RowIndex].FindControl("txtDivisionZipCode");
        TextBox divisionTelephone = (TextBox)this.grdDivisionInformation.Rows[e.RowIndex].FindControl("txtDivisionTelephone");
        TextBox divisionFax = (TextBox)this.grdDivisionInformation.Rows[e.RowIndex].FindControl("txtDivisionFax");
        TextBox divisionWeb = (TextBox)this.grdDivisionInformation.Rows[e.RowIndex].FindControl("txtDivisionWeb");
        TextBox divisionEmail = (TextBox)this.grdDivisionInformation.Rows[e.RowIndex].FindControl("txtDivisionEmail");
        TextBox divisionCity = (TextBox)this.grdDivisionInformation.Rows[e.RowIndex].FindControl("txtDivisionCity");
        TextBox divisionState = (TextBox)this.grdDivisionInformation.Rows[e.RowIndex].FindControl("txtDivisionState");
        TextBox divisionContact = (TextBox)this.grdDivisionInformation.Rows[e.RowIndex].FindControl("txtDivisionContact");
        ImageButton ButtonAdd = (ImageButton)this.grdDivisionInformation.Rows[e.RowIndex].FindControl("btnSaveDivInf");
        divisionInformation.DivisionInfId = int.Parse(ButtonAdd.Attributes["DivisionInfId"]);
        
        divisionInformation.Address = string.IsNullOrEmpty(divisionAddress.Text.Trim()) ? null : divisionAddress.Text.Trim();
        divisionInformation.Suburb = string.IsNullOrEmpty(divisionSuburb.Text.Trim()) ? null : divisionSuburb.Text.Trim();
        divisionInformation.ZipCode = string.IsNullOrEmpty(divisionZipCode.Text.Trim()) ? null : divisionZipCode.Text.Trim();
        divisionInformation.Telephone = string.IsNullOrEmpty(divisionTelephone.Text.Trim()) ? null : divisionTelephone.Text.Trim();
        divisionInformation.Fax = string.IsNullOrEmpty(divisionFax.Text.Trim()) ? null : divisionFax.Text.Trim();
        divisionInformation.Web = string.IsNullOrEmpty(divisionWeb.Text.Trim()) ? null : divisionWeb.Text.Trim();
        divisionInformation.Email = string.IsNullOrEmpty(divisionEmail.Text.Trim()) ? null : divisionEmail.Text.Trim();
        divisionInformation.City = string.IsNullOrEmpty(divisionCity.Text.Trim()) ? null : divisionCity.Text.Trim();
        divisionInformation.State = string.IsNullOrEmpty(divisionState.Text.Trim()) ? null : divisionState.Text.Trim();
        divisionInformation.Contact = string.IsNullOrEmpty(divisionContact.Text.Trim()) ? null : divisionContact.Text.Trim();
        divisionInformation.Image = null;
        divisionInformation.Active = true;

        MedinetBusinessLogicComponent.DivisionInformationBLC.Instance.updateDivisionInformation(divisionInformation, this._userId, this._hashkey);

        this.grdDivisionInformation.EditIndex = -1;
        this.getDivisionInformation();
        ScriptManager.RegisterStartupScript(this, typeof(Page), "messageScript", "jAlert('La dirección fue actualizada', 'Dirección Actualizada');", true);
    }

    protected void btnLabReturn_Click(object sender, EventArgs e)
    {
        this.Response.Redirect("products.aspx");
    }

    protected void btnSaveAddress_Click(object sender, EventArgs e)
    {
        this.pnlAddress.Visible = false;
        this.addDataDivisionInformation();
        this.getDivisionInformation();
        ScriptManager.RegisterStartupScript(this, typeof(Page), "messageScript", "jAlert('La dirección fue Registrada ', 'Dirección Registrada');", true);
    }

   protected void AddAddress_Click(object sender, EventArgs e)
    {
        pnlAddress.Visible = true;
    }

    #endregion

    #region Private Methods
    private void getDataDivisionInformation()
    {
        this._divInfo = new MedinetBusinessEntries.DivisionInformationInfo();
        this._divInfo.Active = true;
        this._divInfo.Address = txtAddress.Text;
        this._divInfo.City = txtCity.Text;
        this._divInfo.Contact = txtContact.Text;
        this._divInfo.DivisionId = this._divisionId;
        this._divInfo.Email = txtEmail.Text;
        this._divInfo.Fax = txtFax.Text;
        this._divInfo.State = txtState.Text;
        this._divInfo.Suburb = txtSuburb.Text;
        this._divInfo.Telephone = txtPhone.Text;
        this._divInfo.Web = txtWeb.Text;
        this._divInfo.ZipCode = txtPostalCode.Text;
    }

    private void addDataDivisionInformation()
    {
        this.getDataDivisionInformation();
        MedinetBusinessLogicComponent.DivisionInformationBLC.Instance.addDivisionInformation(this._divInfo, this._userId, this._hashkey);

    }
    private void getDivision()
    {
        MedinetBusinessEntries.DivisionsInfo divisionInfo = MedinetBusinessLogicComponent.DivisionsBLC.Instance.getDivision(this._divisionId);
        List<MedinetBusinessEntries.DivisionsInfo> divisions = new List<MedinetBusinessEntries.DivisionsInfo>();
        divisions.Add(divisionInfo);

        this.grdDivision.DataSource = divisions;
        this.grdDivision.DataBind();
    }

    private void getDivisionInformation()
    {
        List<MedinetBusinessEntries.DivisionInformationInfo> divisionInformation =
            MedinetBusinessLogicComponent.DivisionInformationBLC.Instance.getListInformationByDivision(this._divisionId);
        if (divisionInformation != null)
        {
            //this._divisionInfId = divisionInformation.DivisionInfId;
            //List<MedinetBusinessEntries.DivisionInformationInfo> divisionAddress = new List<MedinetBusinessEntries.DivisionInformationInfo>();
            //divisionAddress.Add(divisionInformation);

            this.grdDivisionInformation.DataSource = divisionInformation;
            this.grdDivisionInformation.DataBind();
        }
    }

    protected string displayDivisionDescription(string divisionDescription)
    {
        return string.IsNullOrEmpty(divisionDescription) ? "" : divisionDescription;
    }

    protected string displayShortName(string shortName)
    {
        return string.IsNullOrEmpty(shortName) ? "" : shortName;
    }

    protected string displayAddress(string address)
    {
        return string.IsNullOrEmpty(address) ? "" : address;
    }

    protected string displaySuburb(string suburb)
    {
        return string.IsNullOrEmpty(suburb) ? "" : suburb;
    }

    protected string displayZipCode(string zipCode)
    {
        return string.IsNullOrEmpty(zipCode) ? "" : zipCode;
    }

    protected string displayTelephone(string telephone)
    {
        return string.IsNullOrEmpty(telephone) ? "" : telephone;
    }

    protected string displayFax(string fax)
    {
        return string.IsNullOrEmpty(fax) ? "" : fax;
    }

    protected string displayWeb(string web)
    {
        return string.IsNullOrEmpty(web) ? "" : web;
    }

    protected string displayEmail(string email)
    {
        return string.IsNullOrEmpty(email) ? "" : email;
    }

    protected string displayCity(string city)
    {
        return string.IsNullOrEmpty(city) ? "" : city;
    }

    protected string displayState(string state)
    {
        return string.IsNullOrEmpty(state) ? "" : state;
    }

    protected string displayContact(string contact)
    {
        return string.IsNullOrEmpty(contact) ? "" : contact;
    }

    #endregion

    private int _divisionId;
    private int _divisionInfId;
    private int _userId;
    private string _hashkey;
    private MedinetBusinessEntries.DivisionInformationInfo _divInfo;

    protected void grdDivisionInformation_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            MedinetBusinessEntries.DivisionInformationInfo currentRow = (MedinetBusinessEntries.DivisionInformationInfo)e.Row.DataItem;

            ImageButton ButtonAdd = (ImageButton)e.Row.FindControl("btnSaveDivInf");
            ImageButton ButtonDel = (ImageButton)e.Row.FindControl("btnElimDivInf");
            if (ButtonAdd!=null)
            {
                ButtonAdd.Attributes.Add("DivisionInfId", currentRow.DivisionInfId.ToString());    
            }
            if (ButtonDel != null)
            {
                ButtonDel.Attributes.Add("DivisionInfId", currentRow.DivisionInfId.ToString());
            }

            



        }
    }
    protected void grdDivisionInformation_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        MedinetBusinessEntries.DivisionInformationInfo divisionInformation = new MedinetBusinessEntries.DivisionInformationInfo();
        ImageButton ButtonDel = (ImageButton)this.grdDivisionInformation.Rows[e.RowIndex].FindControl("btnElimDivInf");
        divisionInformation.DivisionInfId = int.Parse(ButtonDel.Attributes["DivisionInfId"]);
        MedinetBusinessLogicComponent.DivisionInformationBLC.Instance.deleteDivisionInformation(divisionInformation, this._userId, this._hashkey);
        this.grdDivisionInformation.EditIndex = -1;
        this.getDivisionInformation();
        ScriptManager.RegisterStartupScript(this, typeof(Page), "messageScript", "jAlert('La dirección fue eliminada', 'Dirección Actualizada');", true);
    }
}