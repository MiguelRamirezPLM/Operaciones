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
        this.btnCdivision.Visible = false;
        this.btnCaddress.Visible = false;
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
        this.btnCdivision.Visible = true;
    }

    protected void grdDivision_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        TextBox divisionDescription = (TextBox)this.grdDivision.Rows[e.RowIndex].FindControl("txtDivisionDescription");
        TextBox divisionShortName = (TextBox)this.grdDivision.Rows[e.RowIndex].FindControl("txtDivisionShortName");
        
        AgroBusinessEntries.DivisionAddressInfo divisionInfo = AgroBusinessLogicComponent.LaboratoryDivisionBLC.Instance.getDivision(this._divisionId);

        if (!string.IsNullOrEmpty(divisionDescription.Text.Trim()))
        {
            if (!string.IsNullOrEmpty(divisionShortName.Text.Trim()))
            {
                divisionInfo.DivisionName = divisionDescription.Text.Trim().ToUpper();
                divisionInfo.ShortName = divisionShortName.Text.Trim().ToUpper();

                AgroBusinessLogicComponent.LaboratoryDivisionBLC.Instance.updateDivision(divisionInfo, this._userId, this._hashkey);
               
                this.grdDivision.EditIndex = -1;
                this.getDivision(); 
                ScriptManager.RegisterStartupScript(this, typeof(Page), "messageScript", "jAlert('El laboratorio fue actualizado', 'Laboratorio Actualizado');", true);

            }
            else
                ScriptManager.RegisterStartupScript(this, typeof(Page), "messageScript", "jAlert(Debe escribir un nombre corto', 'Nombre corto');", true);
               
        }
        else
            ScriptManager.RegisterStartupScript(this, typeof(Page), "messageScript", "jAlert('Debe escribir una razón social', 'Razón social');", true);
    }

    protected void grdDivisionInformation_RowEditing(object sender, GridViewEditEventArgs e)
    {
        this.grdDivisionInformation.EditIndex = e.NewEditIndex;
        this.getDivisionInformation();

        this.btnCaddress.Visible = true;

    }


    protected void grdDivisionInformation_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        AgroBusinessEntries.DivisionAddressInfo divisionInformation = new AgroBusinessEntries.DivisionAddressInfo();
        divisionInformation = AgroBusinessLogicComponent.LaboratoryDivisionBLC.Instance.getInformationByDivision(this._divisionId);

        TextBox divisionAddress = (TextBox)this.grdDivisionInformation.Rows[e.RowIndex].FindControl("txtDivisionAddress");
        TextBox divisionSuburb = (TextBox)this.grdDivisionInformation.Rows[e.RowIndex].FindControl("txtDivisionSuburb");
        TextBox divisionZipCode = (TextBox)this.grdDivisionInformation.Rows[e.RowIndex].FindControl("txtDivisionZipCode");
        TextBox divisionTelephone = (TextBox)this.grdDivisionInformation.Rows[e.RowIndex].FindControl("txtDivisionTelephone");
        TextBox divisionFax = (TextBox)this.grdDivisionInformation.Rows[e.RowIndex].FindControl("txtDivisionFax");
        TextBox divisionWeb = (TextBox)this.grdDivisionInformation.Rows[e.RowIndex].FindControl("txtDivisionWeb");
        TextBox divisionEmail = (TextBox)this.grdDivisionInformation.Rows[e.RowIndex].FindControl("txtDivisionEmail");
        TextBox divisionCity = (TextBox)this.grdDivisionInformation.Rows[e.RowIndex].FindControl("txtDivisionCity");
        TextBox divisionState = (TextBox)this.grdDivisionInformation.Rows[e.RowIndex].FindControl("txtDivisionState");
        TextBox divisionLada = (TextBox)this.grdDivisionInformation.Rows[e.RowIndex].FindControl("txtDivisionLada");

        ImageButton ButtonAdd = (ImageButton)this.grdDivisionInformation.Rows[e.RowIndex].FindControl("btnSaveDivInf");
    divisionInformation.DivisionInformationId = int.Parse(ButtonAdd.Attributes["DivisionInformationId"]);

        divisionInformation.Address = string.IsNullOrEmpty(divisionAddress.Text.Trim()) ? null : divisionAddress.Text.Trim();
        divisionInformation.Suburb = string.IsNullOrEmpty(divisionSuburb.Text.Trim()) ? null : divisionSuburb.Text.Trim();
        divisionInformation.ZipCode = string.IsNullOrEmpty(divisionZipCode.Text.Trim()) ? null : divisionZipCode.Text.Trim();
        divisionInformation.Telephone = string.IsNullOrEmpty(divisionTelephone.Text.Trim()) ? null : divisionTelephone.Text.Trim();
        divisionInformation.Fax = string.IsNullOrEmpty(divisionFax.Text.Trim()) ? null : divisionFax.Text.Trim();
        divisionInformation.Web = string.IsNullOrEmpty(divisionWeb.Text.Trim()) ? null : divisionWeb.Text.Trim();
        divisionInformation.Email = string.IsNullOrEmpty(divisionEmail.Text.Trim()) ? null : divisionEmail.Text.Trim();
        divisionInformation.City = string.IsNullOrEmpty(divisionCity.Text.Trim()) ? null : divisionCity.Text.Trim();
        divisionInformation.State = string.IsNullOrEmpty(divisionState.Text.Trim()) ? null : divisionState.Text.Trim();
        divisionInformation.Lada = string.IsNullOrEmpty(divisionLada.Text.Trim()) ? null : divisionLada.Text.Trim();


        AgroBusinessLogicComponent.LaboratoryDivisionBLC.Instance.updateDivisionInformation(divisionInformation, this._userId, this._hashkey);
        this.grdDivisionInformation.EditIndex = -1;
        this.getDivisionInformation();

        ScriptManager.RegisterStartupScript(this, typeof(Page), "messageScript", "jAlert('La dirección fue actualizada', 'Dirección Actualizada');", true);
 
    }

   
    protected void btnSaveAddress_Click(object sender, EventArgs e)
    {
        this.pnlAddress.Visible = false;
        this.addDataDivisionInformation();
        this.getDivisionInformation();

        ScriptManager.RegisterStartupScript(this, typeof(Page), "messageScript", "jAlert('La dirección se agrego correctamente', 'Dirección correcta');", true); 


        this.clearProperties();
    }

   protected void AddAddress_Click(object sender, EventArgs e)
    {
        pnlAddress.Visible = true;
    }

    #endregion

    #region Private Methods

    private void getDataDivisionInformation()
    {
        this._divInfo = new AgroBusinessEntries.DivisionAddressInfo();

        this._divInfo.Address = txtAddress.Text;
        this._divInfo.City = txtCity.Text;
        this._divInfo.Lada = txtLada.Text;
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

        AgroBusinessLogicComponent.LaboratoryDivisionBLC.Instance.addDivisionInformation(this._divInfo, this._userId, this._hashkey);
    }
    private void getDivision()
    {
        AgroBusinessEntries.DivisionAddressInfo divisionInfo = AgroBusinessLogicComponent.LaboratoryDivisionBLC.Instance.getDivision(this._divisionId);
        List<AgroBusinessEntries.DivisionAddressInfo> divisions = new List<AgroBusinessEntries.DivisionAddressInfo>();
        divisions.Add(divisionInfo);
 
        this.grdDivision.DataSource = divisions;
        this.grdDivision.DataBind();
    }

    private void getDivisionInformation()
    {
       
        List<AgroBusinessEntries.DivisionAddressInfo> divisionInformation = AgroBusinessLogicComponent.LaboratoryDivisionBLC.Instance.getListInformationByDivision(this._divisionId);
        
        if (divisionInformation != null)
        {
           
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

    protected string displayLada(string lada)
    {
        return string.IsNullOrEmpty(lada) ? "" : lada;
    }

    #endregion

    private int _divisionId;
    private int _divisionInfId;
    private int _userId;
    private string _hashkey;
    private AgroBusinessEntries.DivisionAddressInfo _divInfo;

    protected void grdDivisionInformation_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            
            AgroBusinessEntries.DivisionAddressInfo currentRow = (AgroBusinessEntries.DivisionAddressInfo)e.Row.DataItem;

            ImageButton ButtonAdd = (ImageButton)e.Row.FindControl("btnSaveDivInf");
            ImageButton ButtonDel = (ImageButton)e.Row.FindControl("btnElimDivInf");
            if (ButtonAdd!=null)
            {
                ButtonAdd.Attributes.Add("DivisionInformationId", currentRow.DivisionInformationId.ToString());    
            }
            if (ButtonDel != null)
            {
                ButtonDel.Attributes.Add("DivisionInformationId", currentRow.DivisionInformationId.ToString());
            }

        }
    }
    protected void grdDivisionInformation_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        AgroBusinessEntries.DivisionAddressInfo divisionInformation = new AgroBusinessEntries.DivisionAddressInfo();


        ImageButton ButtonDel = (ImageButton)this.grdDivisionInformation.Rows[e.RowIndex].FindControl("btnElimDivInf");
        divisionInformation.DivisionInformationId = int.Parse(ButtonDel.Attributes["DivisionInformationId"]);

     
        AgroBusinessLogicComponent.LaboratoryDivisionBLC.Instance.deleteDivisionInformation(divisionInformation, this._userId, this._hashkey);
        this.grdDivisionInformation.EditIndex = -1;
        this.getDivisionInformation();
 

        ScriptManager.RegisterStartupScript(this, typeof(Page), "messageScript", "jAlert('La dirección fue eliminada', 'Dirección Eliminada');", true); 

    }

    protected void btnLabReturn_Click(object sender, ImageClickEventArgs e)
    {
        this.Response.Redirect("products.aspx");
    }

    private void clearProperties()
    {
        txtAddress.Text = "";
        txtCity.Text = "";
        txtEmail.Text = "";
        txtFax.Text = "";
        txtLada.Text = "";
        txtPhone.Text = "";
        txtPostalCode.Text = "";
        txtState.Text = "";
        txtSuburb.Text = "";
        txtWeb.Text = "";
        
    }


   // public AgroBusinessEntries.DivisionAddressInfo _divInfo { get; set; }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        this.pnlAddress.Visible = false;
        this.clearProperties();
    }
    protected void btnCdivision_Click(object sender, EventArgs e)
    {
        this.grdDivision.EditIndex = -1;
        this.getDivision(); 
    }
    protected void btnCaddress_Click(object sender, EventArgs e)
    {
        this.grdDivisionInformation.EditIndex = -1;
        this.getDivisionInformation(); 
    }
}