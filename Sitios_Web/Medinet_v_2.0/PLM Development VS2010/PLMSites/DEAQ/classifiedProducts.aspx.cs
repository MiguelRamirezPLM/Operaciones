using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using DevExpress.Web.ASPxGridView;
using System.Collections;

public partial class classifiedProducts : System.Web.UI.Page
{
    

  
    protected void Page_Load(object sender, EventArgs e) {
        
        this.lblProdName.Text = Session["Brand"].ToString() ;
        this.lblPharmaName.Text = Session["PharmaName"].ToString();
        this._editionId = this.Session["editionId"] == null ? -1 : Convert.ToInt32(this.Session["editionId"]);
        this._countryId = this.Session["countryId"] == null ? -1 : Convert.ToInt32(this.Session["countryId"]);
        this._userId = this.Session["userId"] == null ? -1 : Convert.ToInt32(this.Session["userId"]);
        this._hashkey = System.Configuration.ConfigurationManager.AppSettings["HashKey"];
        this._productId = this.Session["productId"] == null ? -1 : Convert.ToInt32(this.Session["productId"]);
        this._bookId = this.Session["bookId"] == null ? -1 : Convert.ToInt32(this.Session["bookId"]);
        this._divisionId = Convert.ToInt32(Session["divisionId"]);
        
        this._pharmaFormId = Convert.ToInt32(Session["pharmaFormId"]);

        if (!IsPostBack)
        {
            this.prepareGrids();
            loadAll();
            
        }
      
     
      
           
        
    }
    protected void ddlPageSizeFrom_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ac1.Checked == true)
        {
            this.Session["CurrentPage"] = 0;
            this.prepareGrid(Convert.ToInt16(this.ddlAgroPageSizeFrom.SelectedValue));
           
            getAgrochemicalUses();
            getAgrochemicalUsesForProduct();
        }
        else if (ac2.Checked == true)
        {
            this.Session["CurrentPage"] = 0;
            this.prepareGrid(Convert.ToInt16(this.ddlCropPageSizeFrom.SelectedValue));

            getCrops();
            getCropsForProduct();
           
        }
        else if (ac3.Checked == true)
        {
            this.Session["CurrentPage"] = 0;
            this.prepareGrid(Convert.ToInt16(this.ddlSeedPageSizeFrom.SelectedValue));

            getSeeds();
            getSeedsForProduct();
        }
        else if (ac4.Checked == true)
        {
            this.Session["CurrentPage"] = 0;
            this.prepareGrid(Convert.ToInt16(this.ddlSubsPageSizeFrom.SelectedValue));

            getActiveSubstances();
            getActiveSubstancesForProduct();
        }
        
    }
    protected void grdProducts_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        if (ac1.Checked == true)
        {
            this.agrochemicalGridFrom.PageIndex = e.NewPageIndex;
            this.Session["CurrentPage"] = Convert.ToInt32(e.NewPageIndex);
           
            getAgrochemicalUses();
            getAgrochemicalUsesForProduct();
        }
        else if (ac2.Checked == true)
        {
            this.cropGridFrom.PageIndex = e.NewPageIndex;
            this.Session["CurrentPage"] = Convert.ToInt32(e.NewPageIndex);

            getCrops();
            getCropsForProduct();
        }
        else if (ac3.Checked == true)
        {
            this.seedGridFrom.PageIndex = e.NewPageIndex;
            this.Session["CurrentPage"] = Convert.ToInt32(e.NewPageIndex);

            getSeeds();
            getSeedsForProduct();
        }
        else if (ac4.Checked == true)
        {
            this.substanceGridFrom.PageIndex = e.NewPageIndex;
            this.Session["CurrentPage"] = Convert.ToInt32(e.NewPageIndex);

            getActiveSubstances();
            getActiveSubstancesForProduct();
        }
       
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        ImageButton btn = (ImageButton)sender;
        GridViewRow row =(GridViewRow) btn.NamingContainer;
        int rowKey = 0;
            
        if (ac1.Checked == true)
        {
            rowKey=Convert.ToInt32(agrochemicalGridFrom.DataKeys[row.RowIndex].Values[0].ToString());
            AgroBusinessLogicComponent.AgrochemicalUsesBLC.Instance.addAgrochemicalUsesForProduct(this._productId, Convert.ToInt32(rowKey), this._userId, this._hashkey);
            getAgrochemicalUses();
            getAgrochemicalUsesForProduct();
        }
        else if (ac2.Checked == true)
        {
            rowKey = Convert.ToInt32(cropGridFrom.DataKeys[row.RowIndex].Values[0].ToString());
            AgroBusinessLogicComponent.CropsBLC.Instance.addCropsForProduct(this._productId, Convert.ToInt32(rowKey), this._userId, this._hashkey);
            getCrops();
            getCropsForProduct();
        }
        else if (ac3.Checked == true)
        {
            rowKey = Convert.ToInt32(seedGridFrom.DataKeys[row.RowIndex].Values[0].ToString());
            AgroBusinessLogicComponent.SeedsBLC.Instance.addSeedsForProduct(this._productId, Convert.ToInt32(rowKey), this._userId, this._hashkey);
            getSeeds();
            getSeedsForProduct();
        }
        else if (ac4.Checked == true)
        {
            rowKey = Convert.ToInt32(substanceGridFrom.DataKeys[row.RowIndex].Values[0].ToString());
            AgroBusinessLogicComponent.ActiveSubstancesBLC.Instance.addActiveSubstancesForProduct(this._productId, Convert.ToInt32(rowKey), this._userId, this._hashkey);
            getActiveSubstances();
            getActiveSubstancesForProduct();
        }
    }
    

   
    protected void imgBtnBackLabs_Click(object sender, ImageClickEventArgs e)
    {
        this.Session["divisionId"] = -1;
        
        this.Response.Redirect("assignedProducts.aspx");
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        ImageButton btn = (ImageButton)sender;
        GridViewRow row = (GridViewRow)btn.NamingContainer;
        if (ac1.Checked == true)
        {
            int productId=Convert.ToInt32(agrochemicalGridTo.DataKeys[row.RowIndex].Values[0].ToString());
            int agroId = Convert.ToInt32(agrochemicalGridTo.DataKeys[row.RowIndex].Values[1].ToString());

            AgroBusinessLogicComponent.AgrochemicalUsesBLC.Instance.deleteAgrochemicalUsesForProduct(this._productId, agroId, this._userId, this._hashkey);
            getAgrochemicalUsesForProduct();
            getAgrochemicalUses();
        }
        else if (ac2.Checked == true)
        {
            int productId = Convert.ToInt32(cropGridTo.DataKeys[row.RowIndex].Values[0].ToString());
            int cropId = Convert.ToInt32(cropGridTo.DataKeys[row.RowIndex].Values[1].ToString());

            AgroBusinessLogicComponent.CropsBLC.Instance.deleteCropsForProduct(this._productId, cropId, this._userId, this._hashkey);
            getCropsForProduct();
            getCrops();
        }
        else if (ac3.Checked == true)
        {
            int productId = Convert.ToInt32(seedGridTo.DataKeys[row.RowIndex].Values[0].ToString());
            int seedId = Convert.ToInt32(seedGridTo.DataKeys[row.RowIndex].Values[1].ToString());

            AgroBusinessLogicComponent.SeedsBLC.Instance.deleteSeedsForProduct(this._productId, seedId, this._userId, this._hashkey);
            getSeedsForProduct();
            getSeeds();
        }
        else if (ac4.Checked == true)
        {
            int productId = Convert.ToInt32(substanceGridTo.DataKeys[row.RowIndex].Values[0].ToString());
            int subsId = Convert.ToInt32(substanceGridTo.DataKeys[row.RowIndex].Values[1].ToString());

            AgroBusinessLogicComponent.ActiveSubstancesBLC.Instance.deleteActiveSubstancesForProduct(this._productId, subsId, this._userId, this._hashkey);
            getActiveSubstancesForProduct();
            getActiveSubstances();
        }
       
    }
 

    private void prepareGrids()
    {
        this.agrochemicalGridFrom.AllowPaging = true;
        this.agrochemicalGridFrom.PageSize = Convert.ToInt16(ddlAgroPageSizeFrom.SelectedValue);
        this.agrochemicalGridFrom.PageIndex = this.Session["CurrentPage"] == null ? 0 : Convert.ToInt32(this.Session["CurrentPage"]);


        this.cropGridFrom.AllowPaging = true;
        this.cropGridFrom.PageSize = Convert.ToInt16(ddlAgroPageSizeFrom.SelectedValue);
        this.cropGridFrom.PageIndex = this.Session["CurrentPage"] == null ? 0 : Convert.ToInt32(this.Session["CurrentPage"]);


        this.seedGridFrom.AllowPaging = true;
        this.seedGridFrom.PageSize = Convert.ToInt16(ddlAgroPageSizeFrom.SelectedValue);
        this.seedGridFrom.PageIndex = this.Session["CurrentPage"] == null ? 0 : Convert.ToInt32(this.Session["CurrentPage"]);


        this.substanceGridFrom.AllowPaging = true;
        this.substanceGridFrom.PageSize = Convert.ToInt16(ddlAgroPageSizeFrom.SelectedValue);
        this.substanceGridFrom.PageIndex = this.Session["CurrentPage"] == null ? 0 : Convert.ToInt32(this.Session["CurrentPage"]);
    }

    private void prepareGrid(short pagesize)
    {

        if (ac1.Checked == true)
        {
            if (pagesize > 0)
            {
                this.agrochemicalGridFrom.AllowPaging = true;
                this.agrochemicalGridFrom.PageSize = pagesize;
                this.agrochemicalGridFrom.PageIndex = this.Session["CurrentPage"] == null ? 0 : Convert.ToInt32(this.Session["CurrentPage"]);
            }
            else
                this.agrochemicalGridFrom.AllowPaging = false;

        }
        else if (ac2.Checked == true)
        {
            if (pagesize > 0)
            {
                this.cropGridFrom.AllowPaging = true;
                this.cropGridFrom.PageSize = pagesize;
                this.cropGridFrom.PageIndex = this.Session["CurrentPage"] == null ? 0 : Convert.ToInt32(this.Session["CurrentPage"]);
            }
            else
                this.cropGridFrom.AllowPaging = false;
        }
        else if (ac3.Checked == true)
        {
            if (pagesize > 0)
            {
                this.seedGridFrom.AllowPaging = true;
                this.seedGridFrom.PageSize = pagesize;
                this.seedGridFrom.PageIndex = this.Session["CurrentPage"] == null ? 0 : Convert.ToInt32(this.Session["CurrentPage"]);
            }
            else
                this.seedGridFrom.AllowPaging = false;
        }
        else if (ac4.Checked == true)
        {
            if (pagesize > 0)
            {
                this.substanceGridFrom.AllowPaging = true;
                this.substanceGridFrom.PageSize = pagesize;
                this.substanceGridFrom.PageIndex = this.Session["CurrentPage"] == null ? 0 : Convert.ToInt32(this.Session["CurrentPage"]);
            }
            else
                this.substanceGridFrom.AllowPaging = false;
        }
       
        
    }
    protected void loadAll()
    {

        this.getAgrochemicalUses();
        this.getAgrochemicalUsesForProduct();


        this.getCrops();
        this.getCropsForProduct();


        this.getSeeds();
        this.getSeedsForProduct();


        this.getActiveSubstances();
        this.getActiveSubstancesForProduct();
    }

    protected void btnConsult_Click(object sender, EventArgs e)
    {

        if (ac1.Checked == true)
        {
            this.getAgrochemicalUses();
            this.txtAgroName.Focus();

        }
        else if (ac2.Checked == true)
        {
            this.getCrops();
            this.txtCropName.Focus();
        }
        else if (ac3.Checked == true)
        {
            this.getSeeds();
            this.txtSeedName.Focus();
        }
        else if (ac4.Checked == true)
        {
            this.getActiveSubstances();
            this.txtSubsName.Focus();
        }
       
        
       
    }


    protected void getAgrochemicalUses()
    {
        string proNameToSearch = string.IsNullOrEmpty(this.txtAgroName.Text.Trim()) ? null : this.txtAgroName.Text.Trim() + "%";
        agrochemicalGridFrom.DataSource = AgroBusinessLogicComponent.AgrochemicalUsesBLC.Instance.getAgrochemicalUsesNotAssociated(this._editionId, this._divisionId, this._bookId, this._productId, this._pharmaFormId,proNameToSearch);
        agrochemicalGridFrom.DataBind();
    }
    protected void getAgrochemicalUsesForProduct()
    {
        agrochemicalGridTo.DataSource = AgroBusinessLogicComponent.AgrochemicalUsesBLC.Instance.getAgrochemicalUsesForProduct(this._editionId, this._divisionId, this._bookId, this._productId, this._pharmaFormId);
        agrochemicalGridTo.DataBind();
    }


    protected void getActiveSubstances()
    {
        string proNameToSearch = string.IsNullOrEmpty(this.txtSubsName.Text.Trim()) ? null : this.txtSubsName.Text.Trim() + "%";
        substanceGridFrom.DataSource = AgroBusinessLogicComponent.ActiveSubstancesBLC.Instance.getActiveSubstancesNotAssociated(this._editionId, this._divisionId, this._bookId, this._productId, this._pharmaFormId, proNameToSearch);
        substanceGridFrom.DataBind();
       
    }
    protected void getActiveSubstancesForProduct()
    {
        substanceGridTo.DataSource = AgroBusinessLogicComponent.ActiveSubstancesBLC.Instance.getActiveSubstancesForProduct(this._editionId, this._divisionId, this._bookId, this._productId, this._pharmaFormId);
        substanceGridTo.DataBind();
    }


    protected void getCrops()
    {
        string proNameToSearch = string.IsNullOrEmpty(this.txtCropName.Text.Trim()) ? null : this.txtCropName.Text.Trim() + "%";
        cropGridFrom.DataSource = AgroBusinessLogicComponent.CropsBLC.Instance.getCropsNotAssociated(this._editionId, this._divisionId, this._bookId, this._productId, this._pharmaFormId, proNameToSearch);
        cropGridFrom.DataBind();
    }
    protected void getCropsForProduct()
    {
        cropGridTo.DataSource = AgroBusinessLogicComponent.CropsBLC.Instance.getCropsForProduct(this._editionId, this._divisionId, this._bookId, this._productId, this._pharmaFormId);
        cropGridTo.DataBind();
    }


    protected void getSeeds()
    {
        string proNameToSearch = string.IsNullOrEmpty(this.txtSeedName.Text.Trim()) ? null : this.txtSeedName.Text.Trim() + "%";
        seedGridFrom.DataSource = AgroBusinessLogicComponent.SeedsBLC.Instance.getSeedsNotAssociated(this._editionId, this._divisionId, this._bookId, this._productId, this._pharmaFormId, proNameToSearch);
        seedGridFrom.DataBind();
     
    }
    protected void getSeedsForProduct()
    {
        seedGridTo.DataSource = AgroBusinessLogicComponent.SeedsBLC.Instance.getSeedsForProduct(this._editionId, this._divisionId, this._bookId, this._productId, this._pharmaFormId);
        seedGridTo.DataBind();
    }
    

    private int _editionId;
    
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
    private int _productId;
    private int _bookId;
    private int _pharmaFormId;
    private int bandera;
}