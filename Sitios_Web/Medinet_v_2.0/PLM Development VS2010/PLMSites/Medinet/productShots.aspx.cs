using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class productShots : System.Web.UI.Page
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
        this._categoryId = this.Session["categoryId"] == null ? -1 : Convert.ToInt32(this.Session["categoryId"]);
        this._productId = this.Session["productId"] == null ? -1 : Convert.ToInt32(this.Session["productId"]);
        this._pharmaFormId = this.Session["pharmaFormId"] == null ? -1 : Convert.ToInt32(this.Session["pharmaFormId"]);
        this._savePath  = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["BaseUrl"]);
        if (!IsPostBack)
        {
            this.getProductShotsGrid();    
        }
    }

    private void getProductShotsGrid() {
        this.grdShots.DataSource = MedinetBusinessLogicComponent.EditionProductShotsBLC.Instance.getProductShots(this._editionId, this._divisionId, this._categoryId, this._productId, this._pharmaFormId);
        this.grdShots.DataBind();    
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
    private int _categoryId;
    private int _productId;
    private int _pharmaFormId;
    private string _savePath;
    protected void grdShots_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType==DataControlRowType.DataRow)
        {
            MedinetBusinessEntries.EditionProductShotsInfo eps = (MedinetBusinessEntries.EditionProductShotsInfo)e.Row.DataItem;
            Image img = (Image)e.Row.FindControl("imgSidef");
        if (img!=null)
        {
            img.ImageUrl = @"images\"+eps.ProductShot;     
        }
        }
    }
    protected void grdShots_RowEditing(object sender, GridViewEditEventArgs e)
    {
        this.grdShots.EditIndex = e.NewEditIndex;
        this.getProductShotsGrid();
    }
    protected void btnAddShot(object sender, EventArgs e)
    {
        
        System.Drawing.Bitmap bm = new System.Drawing.Bitmap(fUp320.PostedFile.InputStream);
        string resolution1 = _savePath + @"320\" + fUp320.FileName;
        string resolution2 = _savePath + @"\384\" + fUp384.FileName;
        string resolution3 = _savePath + @"\400\" + fUp400.FileName;

        fUp320.SaveAs(resolution1);
        fUp384.SaveAs(resolution2);
        fUp400.SaveAs(resolution3);
        MedinetBusinessEntries.EditionProductShotsInfo eps = new MedinetBusinessEntries.EditionProductShotsInfo();
        eps.Active=true;
        eps.CategoryId=this._categoryId;
        eps.DivisionId=this._divisionId;
        eps.EditionId=this._editionId;
        eps.PharmaFormId=this._pharmaFormId;
        eps.ProductId=this._productId;
        eps.ProductShot=fUp320.FileName;
        eps.PSTypeId=1;
        eps.QtyCells=1;
        //MedinetBusinessLogicComponent.EditionProductShotsBLC.Instance.addProductShot(eps, this._userId, this._hashkey);
        this.getProductShotsGrid();
        
    }


    //protected void grdShots_RowUpdating(object sender, GridViewUpdateEventArgs e)
    //{
    //    int editionProductShot = Convert.ToInt32(this.grdShots.DataKeys[e.RowIndex].Values[0].ToString());

    //    TextBox txtSubstanceName = (TextBox)this.grdSubstances.Rows[e.RowIndex].FindControl("txtSubstanceName");
    //    TextBox txtEnglishDescription = (TextBox)this.grdSubstances.Rows[e.RowIndex].FindControl("txtSubstanceEnglishName");
    //}
}