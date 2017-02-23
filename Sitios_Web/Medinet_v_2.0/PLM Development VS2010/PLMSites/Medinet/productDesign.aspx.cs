using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class productDesign : System.Web.UI.Page
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
        this._user = this.Session["user"] == null ? "" : this.Session["user"].ToString();
        this._roleId = this.Session["roleId"] == null ? -1 : Convert.ToInt32(this.Session["roleId"]);
        this._bookName = this.Session["bookName"] == null ? "" : this.Session["bookName"].ToString();
        this._editionName = this.Session["edition"] == null ? "" : (this.Session["edition"]).ToString();
        this._divisionName = this.Session["divisionName"] == null ? "" : this.Session["divisionName"].ToString();
        this._bookShortName = this.Session["bookShortName"] == null ? "" : this.Session["bookShortName"].ToString();
        if (!IsPostBack)
        {
            getEditableProducts();
        }
    }

    private void getEditableProducts()
    {
        this.grdProducts.DataSource = MedinetBusinessLogicComponent.ProductsBLC.Instance.getParticipantProductsByDivision(this._divisionId, this._countryId, this._editionId, this._bookId);
        this.grdProducts.DataBind();
    }

    protected void grdProducts_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {


            MedinetBusinessEntries.ProductsToEditInfo prod = (MedinetBusinessEntries.ProductsToEditInfo)e.Row.DataItem;
            DataList dl = (DataList)e.Row.FindControl("dlSidef");
            Image edit = (Image)e.Row.FindControl("btnEdit");
            if (dl!=null)
            {
                dl.DataSource = MedinetBusinessLogicComponent.EditionProductShotsBLC.Instance.getProductShots(this._editionId, (int)prod.DivisionId, (int)prod.CategoryId, prod.ProductId, Convert.ToInt32(prod.PharmaFormId));
                dl.DataBind();    
            }
            if (edit != null) 
            {
                edit.Attributes.Add("editionId", this._editionId.ToString());
                edit.Attributes.Add("productId",prod.ProductId.ToString());
                edit.Attributes.Add("pharmaFormId", prod.PharmaFormId.ToString());
                edit.Attributes.Add("categoryId", prod.CategoryId.ToString());
                edit.Attributes.Add("divisionId", prod.DivisionId.ToString());
            }
            
        }
    }

    protected void dlSidef_DataBound(object sender, DataListItemEventArgs e)
    {


        Image img = (Image)e.Item.FindControl("imgSidef");
        if (img!=null)
        {
            img.ImageUrl = @"images\pdf.png";     
        }
       

        
    }
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
    private PLMCryptographyComponent.CryptographyComponent _cryptography = new PLMCryptographyComponent.CryptographyComponent();
    protected void editShot_click(object sender, ImageClickEventArgs e)
    {
        Image edit = (Image)sender;
        this.Session["editionId"]=edit.Attributes["editionId"];
        this.Session["divisionId"] = edit.Attributes["divisionId"];
        this.Session["productId"] = edit.Attributes["productId"];
        this.Session["categoryId"] = edit.Attributes["categoryId"];
        this.Session["pharmaFormId"] = edit.Attributes["pharmaFormId"];
        this.Response.Redirect("productShots.aspx");
    }
}