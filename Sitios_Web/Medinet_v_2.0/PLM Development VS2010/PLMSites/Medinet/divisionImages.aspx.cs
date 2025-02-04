﻿using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;
using DevExpress.XtraReports.UI;
using DevExpress.XtraReports.Parameters;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxEditors;
using System.Web.UI.HtmlControls;
using System.Configuration;

public partial class divisionImages : System.Web.UI.Page
{

    static FileUpload FiletoUpload = null;
    public static string operationType = "addImageSize";
    static String[] archivos = { "105x330", "125x390", "320x384", "384x512", "400x400" };
    public static double anchoMayor, largoMayor;
    public static string position = "",firstImage;
    public static DataTable SizesIm = null;
    
    #region Page Events

    protected override void OnInit(EventArgs e)
    {
        this._editionId = this.Session["editionId"] == null ? -1 : Convert.ToInt32(this.Session["editionId"]);
        this._bookId = this.Session["bookId"] == null ? -1 : Convert.ToInt32(this.Session["bookId"]);
        this._countryId = this.Session["countryId"] == null ? -1 : Convert.ToInt32(this.Session["countryId"]);
        this._divisionId = this.Session["divisionId"] == null ? -1 : Convert.ToInt32(this.Session["divisionId"]);
        this._userId = this.Session["userId"] == null ? -1 : Convert.ToInt32(this.Session["userId"]);
        this._userCountry = this.Session["userCountry"] == null ? 0 : Convert.ToInt32(this.Session["userCountry"]);
        this._hashkey = System.Configuration.ConfigurationManager.AppSettings["HashKey"];
        this._categoryId = this.Session["categoryId"] == null ? -1 : Convert.ToInt32(this.Session["categoryId"]);
        this._productId = this.Session["productId"] == null ? -1 : Convert.ToInt32(this.Session["productId"]);
        this._pharmaFormId = this.Session["pharmaFormId"] == null ? -1 : Convert.ToInt32(this.Session["pharmaFormId"]);
        this._brand = this.Session["brand"] == null ? "" : this.Session["brand"].ToString();
        this._pharmaForm = this.Session["pharmaForm"] == null ? "" : this.Session["pharmaForm"].ToString();

        SizesIm = new DataTable();
        SizesIm.Columns.Add("Width");
        SizesIm.Columns.Add("Height");
        getPresentationCatalog();
        base.OnInit(e);

    }


    protected void Page_Load(object sender, EventArgs e)
    {
        this._editionId = this.Session["editionId"] == null ? -1 : Convert.ToInt32(this.Session["editionId"]);
        this._bookId = this.Session["bookId"] == null ? -1 : Convert.ToInt32(this.Session["bookId"]);
        this._countryId = this.Session["countryId"] == null ? -1 : Convert.ToInt32(this.Session["countryId"]);
        this._divisionId = this.Session["divisionId"] == null ? -1 : Convert.ToInt32(this.Session["divisionId"]);
        this._userId = this.Session["userId"] == null ? -1 : Convert.ToInt32(this.Session["userId"]);
        this._userCountry = this.Session["userCountry"] == null ? 0 : Convert.ToInt32(this.Session["userCountry"]);
        this._hashkey = System.Configuration.ConfigurationManager.AppSettings["HashKey"];
        this._categoryId = this.Session["categoryId"] == null ? -1 : Convert.ToInt32(this.Session["categoryId"]);
        this._productId = this.Session["productId"] == null ? -1 : Convert.ToInt32(this.Session["productId"]);
        this._pharmaFormId = this.Session["pharmaFormId"] == null ? -1 : Convert.ToInt32(this.Session["pharmaFormId"]);
        this._brand = this.Session["brand"] == null ? "" : this.Session["brand"].ToString();
        this._pharmaForm = this.Session["pharmaForm"] == null ? "" : this.Session["pharmaForm"].ToString();

        if (!IsPostBack)
        {

            //this.getPresentationCatalog();
            //this.getPresentations();
        }

    }

    #endregion

    #region Control Events



    protected void imgBtnBackProducts_Click(object sender, ImageClickEventArgs e)
    {

        this.Response.Redirect("classifiedLaboratoriesLogos.aspx");
    }
    #endregion

    #region Private Methods

    
    private void getPresentationCatalog()
    {
       

        List<MedinetBusinessEntries.DivisionsInfo> presentationCatalog =
          MedinetBusinessLogicComponent.DivisionsBLC.Instance.getDivisionLogos(this._editionId, this._divisionId, this._countryId, this._bookId);
        this.grdPresentationCatalog.DataSource = presentationCatalog;
        if (presentationCatalog.Count > 0)
        {

            this.Sizes = presentationCatalog[0].Sizes;
            if (SizesIm.Rows.Count == 0)
                getImageSizes();
        }
        else
        {
            this.Sizes = MedinetBusinessLogicComponent.DivisionsBLC.Instance.getSizes();
            if (SizesIm.Rows.Count == 0)
                getImageSizes();
        }
        this.grdPresentationCatalog.DataBind();

    }
    protected void getImageSizes()
    {
        string[] Tamaños = this.Sizes.Split(';');
        double tempH = 0, tempW = 0;
        for (int i = 0; i < Tamaños.Length; i++)
        {
            if (Convert.ToInt32(Tamaños[i].Split('x')[0]) > anchoMayor)
                anchoMayor = Convert.ToInt32(Tamaños[i].Split('x')[0]);
            if (Convert.ToInt32(Tamaños[i].Split('x')[1]) > largoMayor)
                largoMayor = Convert.ToInt32(Tamaños[i].Split('x')[1]);
            if (i == Tamaños.Length - 1)
            {
                firstImage = (Tamaños[i].Split('x')[0]) +"x"+ (Tamaños[i].Split('x')[1]);
            }
        }
        for (int i = 0; i < Tamaños.Length; i++)
        {
            tempW = (Convert.ToDouble(Tamaños[i].Split('x')[0]) * 100) / anchoMayor;
            tempH = (Convert.ToDouble(Tamaños[i].Split('x')[1]) * 100) / largoMayor;
            SizesIm.Rows.Add(tempW, tempH);
        }

        Label2.Text ="Selecciona la imagen de " +firstImage;
    }
    protected void aspGrdProducts_RowCommand(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewRowCommandEventArgs e)
    {
        switch (e.CommandArgs.CommandName)
        {
            case "Edit":


                this.Session["divisionId"] = Convert.ToInt32(e.CommandArgs.CommandArgument.ToString().Split('-')[0]);
                this.Session["categoryId"] = Convert.ToInt32(e.CommandArgs.CommandArgument.ToString().Split('-')[1]);
                this.Session["productId"] = Convert.ToInt32(e.CommandArgs.CommandArgument.ToString().Split('-')[2]);
                this._productId = Convert.ToInt32(this.Session["productId"]);
                this.Session["pharmaFormId"] = Convert.ToInt32(e.CommandArgs.CommandArgument.ToString().Split('-')[3]);
                this._pharmaFormId = Convert.ToInt32(this.Session["pharmaFormId"]);

                this.Response.Redirect("activeSubstances.aspx");

                break;

            default:
                return;
        }
    }


    //private void getPresentations()
    //{
    //    this.ddlPresentations.DataSource = MedinetBusinessLogicComponent.PresentationsBLC.Instance.getPresentationsByProduct(this._editionId, this._divisionId, this._categoryId, this._productId, this._pharmaFormId);
    //    this.ddlPresentations.DataTextField = "Presentation";
    //    this.ddlPresentations.DataValueField = "PresentationId";
    //    this.ddlPresentations.DataBind();
    //    ListItem initialLine = new ListItem("SELECCIONAR", "-1", true);
    //    initialLine.Selected = true;
    //    this.ddlPresentations.Items.Insert(0, initialLine);
    //}


    

    protected void addNewImage(object sender, EventArgs e)
    {
        string[] validFileTypes = { "png", "jpg" };
        string ext = System.IO.Path.GetExtension(File1.PostedFile.FileName);
        bool isValidFile = false;
        for (int i = 0; i < validFileTypes.Length; i++)
        {
            if (ext == "." + validFileTypes[i])
            {
                isValidFile = true;
                break;
            }
        }
        if (!isValidFile)
        {
            Label2.ForeColor = System.Drawing.Color.Red;
            Label2.Text = "Invalid File. Please upload a File with extension " +
                           string.Join(",", validFileTypes);
        }
        else
        {
            Label2.Text = "Selecciona la imagen de " + firstImage;
            for (int i = 0; i < grdPresentationCatalog.VisibleRowCount; i++)
            {
                ASPxTextBox ImageName = (ASPxTextBox)grdPresentationCatalog.FindRowCellTemplateControl(i, (GridViewDataColumn)grdPresentationCatalog.Columns["Image"], "txtLB");
                if (ImageName.Text == File1.FileName)
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "messageScript", "jAlert('Esta imagen ya esta asociada a la presentacion', 'Escoja otra imagen');", true);
                    break;
                }
            }
            int presentationId = 0;
            FileUpload File = new FileUpload();
            int imageId = 0;
            string selectedSize = "";
            string Path = ConfigurationManager.AppSettings["RouteImages"];
            int rowIndexx = grdPresentationCatalog.VisibleRowCount + 1;
            
            string divisionName = Session["divisionName"].ToString();
            string NombreImagen = rowIndexx + divisionName + "_" + File1.FileName;
            MedinetBusinessEntries.LogoInfo Logo = new MedinetBusinessEntries.LogoInfo();
            imageId = 1;
            Logo.divisionShot = NombreImagen;
            Logo.Active = 1;
            Logo.DivisionId = this._divisionId;
            Logo.LogoId = imageId;
            Logo.LogoSize= firstImage;
            MedinetBusinessLogicComponent.DivisionsBLC.Instance.addLogoToDivision(Logo, this._userId, this._hashkey);
            if (!System.IO.Directory.Exists(Path + "\\" + firstImage))
                System.IO.Directory.CreateDirectory(Path + "\\" + firstImage);
            File1.SaveAs(Path+"\\" + firstImage + "\\" + NombreImagen);
            getPresentationCatalog();
        }

    }


    protected void saveImage(object sender, EventArgs e)
    {
        string firstImage = anchoMayor + "x" + largoMayor;
        int presentationId;
        Button btn = (Button)sender;
        GridViewDataRowTemplateContainer Row = (GridViewDataRowTemplateContainer)btn.NamingContainer.NamingContainer.NamingContainer;
        int rowIndex = Row.VisibleIndex;
        FileUpload File = new FileUpload();
        Label brand = new Label();
        Label laboratory = new Label();
        FileUpload fuControl = new FileUpload();
        string Path = ConfigurationManager.AppSettings["RouteImages"];

        for (int i = 0; i < grdPresentationCatalog.VisibleRowCount; i++)
        {
            Repeater rptImages = grdPresentationCatalog.FindRowCellTemplateControl(i, null, "RepeaterImages") as Repeater;

            foreach (RepeaterItem rptItem in rptImages.Items)
            {

                File = (FileUpload)rptItem.FindControl(btn.Text);
                if (File != null)
                {
                    if (File.HasFile == true)
                    {
                        rowIndex = i;
                        break;
                    }
                }
            }
            if (File != null && File.HasFile == true)
                break;

        }
        MedinetBusinessEntries.LogoInfo Image = new MedinetBusinessEntries.LogoInfo();
        string fuControlId = btn.Text;
        ASPxTextBox lblNames = grdPresentationCatalog.FindRowCellTemplateControl(rowIndex, null, "txtLB") as ASPxTextBox;
        presentationId = (int)grdPresentationCatalog.GetRowValues(rowIndex, "DivisionId");
        string divisionName = Session["divisionName"].ToString();
        rowIndex = rowIndex + 1;
        string NombreImagen = lblNames.Text;
        if (!System.IO.Directory.Exists(Path + "\\" + firstImage))
            System.IO.Directory.CreateDirectory(Path + "\\" + firstImage);
        File.SaveAs(Path + fuControlId.Substring(1) + "\\" + NombreImagen);
        Image.divisionShot = NombreImagen;
        Image.Active = 1;
        Image.DivisionId= presentationId;
        Image.LogoSize = fuControlId.Substring(1);
       
        try
        {

            MedinetBusinessLogicComponent.DivisionsBLC.Instance.addLogoToDivision(Image, this._userId, this._hashkey);


        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "messageScript", "jAlert('Ya has ingresado la imagen en este tamaño', 'Tamaño ya ingresado');", true);
        }

        getPresentationCatalog();

    }

    //This function is call after we clic on the delete button
    protected void askDelete(object sender, EventArgs e)
    {
        ImageButton btn = (ImageButton)sender;
        GridViewDataItemTemplateContainer Row = (GridViewDataItemTemplateContainer)btn.NamingContainer;

        MedinetBusinessEntries.PresentationImages row = new MedinetBusinessEntries.PresentationImages();
        Session["rowIndex"] = Row.VisibleIndex;
        deleteModal_clic();

    }
    protected void aspGrdProducts_SelectionChanged(object sender, EventArgs e)
    {

    }

    protected void aspGrdProducts_HtmlRowPrepared(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewTableRowEventArgs e)
    {


    }
    protected void aspGrdProducts_HtmlRowCreated(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewTableRowEventArgs e)
    {
        this._editionId = this.Session["editionId"] == null ? -1 : Convert.ToInt32(this.Session["editionId"]);
        this._bookId = this.Session["bookId"] == null ? -1 : Convert.ToInt32(this.Session["bookId"]);
        this._countryId = this.Session["countryId"] == null ? -1 : Convert.ToInt32(this.Session["countryId"]);
        this._divisionId = this.Session["divisionId"] == null ? -1 : Convert.ToInt32(this.Session["divisionId"]);
        this._userId = this.Session["userId"] == null ? -1 : Convert.ToInt32(this.Session["userId"]);
        this._userCountry = this.Session["userCountry"] == null ? 0 : Convert.ToInt32(this.Session["userCountry"]);
        this._hashkey = System.Configuration.ConfigurationManager.AppSettings["HashKey"];
        this._categoryId = this.Session["categoryId"] == null ? -1 : Convert.ToInt32(this.Session["categoryId"]);
        this._productId = this.Session["productId"] == null ? -1 : Convert.ToInt32(this.Session["productId"]);
        this._pharmaFormId = this.Session["pharmaFormId"] == null ? -1 : Convert.ToInt32(this.Session["pharmaFormId"]);
        this._brand = this.Session["brand"] == null ? "" : this.Session["brand"].ToString();
        this._pharmaForm = this.Session["pharmaForm"] == null ? "" : this.Session["pharmaForm"].ToString();

        string imageName = "";
        string[] imageSizes = null;
        string[] sizes = null;

        if (e.RowType == DevExpress.Web.ASPxGridView.GridViewRowType.Data)
        {
            List<Image> li = new List<Image>();
            ASPxTextBox lblNames = grdPresentationCatalog.FindRowCellTemplateControl(e.VisibleIndex, null, "txtLB") as ASPxTextBox;
            ASPxTextBox lblSizes = grdPresentationCatalog.FindRowCellTemplateControl(e.VisibleIndex, null, "lblSizes") as ASPxTextBox;
            Repeater rpImage = grdPresentationCatalog.FindRowCellTemplateControl(e.VisibleIndex, null, "RepeaterImages") as Repeater;
            Session["rowIndex"] = e.VisibleIndex;
            if (lblNames != null && this.Sizes != null)
            {
                imageName = lblNames.Text;
                sizes = this.Sizes.Split(';');
                imageSizes = lblSizes.Text.Split(';');
                DataTable images = new DataTable();
                images.Columns.Add("Size");
                images.Columns.Add("Image");
                images.Columns.Add("ID");
                images.Columns.Add("class");
                for (int i = 0; i < sizes.Length; i++)
                {
                    //string[] specificSize = sizes[i].Split('x')[1]
                    double Height = Convert.ToInt32(sizes[i].Split('x')[1]) * .05 + 50;
                    string hh = "40px";
                    images.Rows.Add(new[] { sizes[i], "~/Images/not.jpg", "f" + sizes[i], "" });
                    Image im = grdPresentationCatalog.FindRowCellTemplateControl(e.VisibleIndex, null, "i" + sizes[i]) as Image;
                    for (int j = 0; j < imageSizes.Length; j++)
                    {

                        if (sizes[i] == imageSizes[j])
                        {
                            images.Rows[i].SetField(1, "~/" + imageSizes[j] + "/" + imageName + "?" + DateTime.Now.Ticks.ToString());
                            images.Rows[i].SetField(3, "preview");
                            break;

                        }

                    }
                }
                rpImage.DataSource = images;
                rpImage.DataBind();
                getPresentationCatalog();
            }
        }
    }
    protected void rpImages_ItemBound(Object sender, RepeaterItemEventArgs e)
    {
        string[] Sizes = this.Sizes.Split(';');

        FileUpload file = (FileUpload)e.Item.FindControl("file");
        Button button = (Button)e.Item.FindControl("b");
        Image image = (Image)e.Item.FindControl("Image");
        Label lblimage = (Label)e.Item.FindControl("lblImage");
        HtmlGenericControl fu = (HtmlGenericControl)e.Item.FindControl("ful");
        HtmlGenericControl repeaterItem = (HtmlGenericControl)e.Item.FindControl("repeaterItem");
        if (e.Item.ItemIndex == 0)
            position = "160";
        else
            position = (Convert.ToInt32(position) + 94).ToString();
        string height = lblimage.Text.Split('x')[1];
        double heightSize = Convert.ToDouble(SizesIm.Rows[e.Item.ItemIndex]["Height"]);
        double widthSize = Convert.ToDouble(SizesIm.Rows[e.Item.ItemIndex]["Width"]);
        int percentHeight = Convert.ToInt32(Convert.ToDouble(height) / 3);
        image.Height = Convert.ToInt32(heightSize);
        image.Width = Convert.ToInt32(widthSize);
        file.ID = "f" + Sizes[e.Item.ItemIndex];
        string divHeigth = ((96) - Convert.ToInt32(heightSize)).ToString();
        string divWidth = (Convert.ToInt32(widthSize)).ToString();
        button.ID = "b" + Sizes[e.Item.ItemIndex];
        fu.ID = "ful" + Sizes[e.Item.ItemIndex]; ;
        button.Text = "f" + Sizes[e.Item.ItemIndex];
        fu.Style.Add("position", "relative");
        fu.Style.Add("top", divHeigth + "px");
        fu.Style.Add("left", divWidth + "px");
        repeaterItem.Style.Add("width", (Convert.ToInt32(widthSize) + 55).ToString() + "px");


    }
    protected void deleteModal_clic()
    {
        int rowIndex = (int)Session["rowIndex"];
        grdPresentationCatalog.DeleteRow(rowIndex);
    }
    protected void grdDeletingRow_click(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        try
        {
            string[] Sizes = this.Sizes.Split(';');
            e.Cancel = true;
            string Path = ConfigurationManager.AppSettings["RouteImages"];
            int rowIndex = (int)Session["rowIndex"];
            int presentationId = (int)grdPresentationCatalog.GetRowValues(rowIndex, "DivisionId");
            int imageId = (int)grdPresentationCatalog.GetRowValues(rowIndex, "LogoId");
            ASPxTextBox lblNames = grdPresentationCatalog.FindRowCellTemplateControl(rowIndex, null, "txtLB") as ASPxTextBox;
            ASPxTextBox lblSizes = grdPresentationCatalog.FindRowCellTemplateControl(rowIndex, null, "lblSizes") as ASPxTextBox;

            MedinetBusinessEntries.LogoInfo row = new MedinetBusinessEntries.LogoInfo();
            row.Active = 1;
            row.DivisionId = presentationId;
            row.divisionShot = lblNames.Text;
            row.LogoId= imageId;
            row.LogoSize = "";

           
                MedinetBusinessLogicComponent.DivisionsBLC.Instance.deleteLogoToDivision(row, this._userId, this._hashkey);

            getPresentationCatalog();
            for (int i = 0; i < Sizes.Length; i++)
            {
                if(System.IO.File.Exists(Path+Sizes[i]+"\\"+row.divisionShot))
                    System.IO.File.Delete(Path + Sizes[i] + "\\" + row.divisionShot);
            }
        }
        catch (Exception exc)
        {

        }
    }


    #endregion

    private int _editionId;
    private int _bookId;
    private int _countryId;
    private int _divisionId;
    private int _userId;
    private int _userCountry;
    private string _hashkey;
    private int _categoryId;
    private int _productId;
    private int _pharmaFormId;
    private string _brand;
    private string _pharmaForm;
    private string Sizes;


}