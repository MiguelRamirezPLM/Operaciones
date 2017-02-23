using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DevExpress.XtraReports.UI;
using DevExpress.XtraReports.Parameters;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
public partial class laboratories : System.Web.UI.Page
{

    #region Page Events

    protected void Page_Load(object sender, EventArgs e)
    {

        this._modulo = (string)this.Session["modulo"];
        //this._pagina = (int)Session["pagina"];
        this._countryId = this.Session["countryId"] == null ? -1 : Convert.ToInt32(this.Session["countryId"]);
        this._bookId = this.Session["bookId"] == null ? -1 : Convert.ToInt32(this.Session["bookId"]);
        this._divisionId = this.Session["divisionId"] == null ? -1 : Convert.ToInt32(this.Session["divisionId"]);
        this._editionId = this.Session["editionId"] == null ? -1 : Convert.ToInt32(this.Session["editionId"]);
        this._roleId = this.Session["roleId"] == null ? -1 : Convert.ToInt32(this.Session["roleId"]);
        this._userCountry = this.Session["userCountry"] == null ? 0 : Convert.ToInt32(this.Session["userCountry"]);
        this._countries = this.Session["Countries"] == null ? string.Empty : this.Session["Countries"].ToString();
        this._user = this.Session["user"] == null ? "" : this.Session["user"].ToString();
        this._bookShortName = this.Session["bookShortName"] == null ? "" : this.Session["bookShortName"].ToString();
        this._userId = this.Session["userId"] == null ? -1 : Convert.ToInt32(this.Session["userId"]);
        this._bookName = this.Session["bookName"] == null ? "" : this.Session["bookName"].ToString();
        this._editionName = this.Session["edition"] == null ? "" : (this.Session["edition"]).ToString();
        this._divisionName = this.Session["divisionName"] == null ? "" : this.Session["divisionName"].ToString();
        this._roleDescription = this.Session["roleDescription"].ToString();
        Session["SM_DataTypes"] = Session["roleDescription"];
        this.setBlock();
        getControls();

        if (!IsPostBack)
        {
            this.getCountries();

            if (this.Session["editionId"] != null && this.Session["countryId"] != null && this.Session["bookId"] != null)
            {
                this.getBooks();
                this.getEditions();
                this.getDivisions(Convert.ToInt32(this.Session["countryId"]));
                if (this.Session["editionId"] != null && this._roleId == (int)Utilities.Roles.Diagramador)
                {
                    this.btnPagination.Visible = true;
                    //this.imgBtnExport.Visible = true;
                    this.imgBtnExportxls.Visible = true;
                    if (this._bookShortName.Equals(System.Configuration.ConfigurationManager.AppSettings["Symptoms"]))
                    {
                        this.btnPaginationSymptoms.Visible = true;
                    }
                }
                else if (this.Session["UserType"] != null && this._roleId == (int)Utilities.Roles.Administrador)
                {
                    if (this.Session["UserType"].ToString() == "Producción")
                    {
                        this.btnPagination.Visible = true;
                        //this.imgBtnExport.Visible = true;
                        this.imgBtnExportxls.Visible = true;
                        if (this._bookShortName.Equals(System.Configuration.ConfigurationManager.AppSettings["Symptoms"]))
                        {
                            this.btnPaginationSymptoms.Visible = true;
                        }
                    }
                }


            }
            else if (this.Session["countryId"] != null)
                this.getDivisions(Convert.ToInt32(this.Session["countryId"]));
            this.displayControls();
            this.clearProperties();
        }
    }

    #endregion

    #region Control Events

    protected void imgBtnBackProducts_Click(object sender, ImageClickEventArgs e)
    {

        this.Response.Redirect("chooseMenu.aspx");
    }
    protected void ddlCountries_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (this.ddlCountries.SelectedItem.ToString() != "SELECCIONAR")
        {
            if (this._modulo == "classifiedProductShots.aspx")
            {
                this._countryId = Convert.ToInt32(ddlCountries.SelectedValue);
                this.getDivisions(this._countryId);
                this.Session["countryId"] = this._countryId;
                this.Session["countryName"] = this.ddlCountries.SelectedItem.ToString();
                ddlDivisions.Enabled = true;

            }

            else
            {
                this.ddlBooks.Enabled = true;
                //Convert
                this._countryId = Convert.ToInt32(ddlCountries.SelectedValue);
                this._bookId = -1;
                this.getBooks();
                this.Session["countryId"] = this._countryId;
                this.Session["countryName"] = this.ddlCountries.SelectedItem.ToString();
                this.ddlEditions.Enabled = false;
                this.ddlDivisions.Enabled = false;
                this.ddlEditions.SelectedIndex = -1;

                this.ddlDivisions.SelectedIndex = -1;
            }

        }
        else
        {
            this.ddlBooks.Enabled = false;
            this.ddlBooks.SelectedIndex = -1;
            this.ddlEditions.Enabled = false;
            this.ddlEditions.SelectedIndex = -1;
            this.ddlDivisions.Enabled = false;
            this.ddlDivisions.SelectedIndex = -1;
        }
        this.btnPagination.Visible = false;
        this.imgBtnExportxls.Visible = false;
        this.btnPaginationSymptoms.Visible = false;

    }

    protected void ddlBooks_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (this.ddlBooks.SelectedItem.ToString() != "SELECCIONAR")
        {
            this._bookId = Convert.ToInt32(this.ddlBooks.SelectedValue);
            this._countryId = Convert.ToInt32(this.ddlCountries.SelectedValue);
            this.ddlEditions.Enabled = true;
            this._editionId = -1;
            this.getEditions();
            this.Session["bookId"] = Convert.ToInt32(this.ddlBooks.SelectedValue == null ? -1 : Convert.ToInt32(this.ddlBooks.SelectedValue));
            this.Session["bookName"] = this.ddlBooks.SelectedItem.ToString();
            this.ddlDivisions.Enabled = false;
            this.ddlDivisions.SelectedIndex = -1;
            this.Session["bookShortName"] = this.Session["bookId"] == null ? "" :
                AgroBusinessLogicComponent.CatalogsBLC.Instance.getBook(Convert.ToInt32(this.Session["bookId"])).ShortName;

        }
        else
        {
            this.ddlEditions.Enabled = false;
            this.ddlEditions.SelectedIndex = -1;
            this.ddlDivisions.Enabled = false;
            this.ddlDivisions.SelectedIndex = -1;
        }
        this.btnPagination.Visible = false;
        this.imgBtnExportxls.Visible = false;
        this.btnPaginationSymptoms.Visible = false;

    }

    protected void ddlEditions_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (this.ddlEditions.SelectedItem.ToString() != "SELECCIONAR")
        {
            this.Session["countryId"] = Convert.ToInt32(this.ddlCountries.SelectedValue);
            this.Session["editionId"] = Convert.ToInt32(this.ddlEditions.SelectedValue);
            this.Session["bookId"] = Convert.ToInt32(this.ddlBooks.SelectedValue);
            if (this._modulo == "classifiedLaboratoriesLogos.aspx" || this._modulo == "encyclopediaImages1.aspx")
            {
                this._editionId = Convert.ToInt32(this.ddlEditions.SelectedValue);
                this.Session["editionId"] = Convert.ToInt32(this.ddlEditions.SelectedValue == null ? -1 : Convert.ToInt32(this.ddlEditions.SelectedValue));
                this.Session["edition"] = this.ddlEditions.SelectedItem.ToString();
                this.Response.Redirect(this._modulo);
            }


            this._editionId = Convert.ToInt32(this.ddlEditions.SelectedValue);
            this.Session["editionId"] = Convert.ToInt32(this.ddlEditions.SelectedValue == null ? -1 : Convert.ToInt32(this.ddlEditions.SelectedValue));
            this.Session["edition"] = this.ddlEditions.SelectedItem.ToString();

            if (this._roleId == (int)Utilities.Roles.Vendedor || this._roleId == (int)Utilities.Roles.Diagramador || this._roleId == (int)Utilities.Roles.Administrador || this._roleId == (int)Utilities.Roles.Diseñador)
            {
                this.ddlDivisions.Enabled = true;
                this._divisionId = -1;
                this.getDivisions(Convert.ToInt32(this.Session["countryId"]));

                if (this._roleId == (int)Utilities.Roles.Diagramador)
                {
                    this.btnPagination.Visible = true;
                    //this.imgBtnExport.Visible = true;
                    this.imgBtnExportxls.Visible = true;
                    if (this._bookShortName.Equals(System.Configuration.ConfigurationManager.AppSettings["Symptoms"]))
                    {
                        this.btnPaginationSymptoms.Visible = true;
                    }
                }
                else if (this.Session["UserType"] != null && this._roleId == (int)Utilities.Roles.Administrador)
                {
                    if (this.Session["UserType"].ToString() == "Producción")
                    {
                        this.btnPagination.Visible = true;
                        //this.imgBtnExport.Visible = true;
                        this.imgBtnExportxls.Visible = true;
                        if (this._bookShortName.Equals(System.Configuration.ConfigurationManager.AppSettings["Symptoms"]))
                        {
                            this.btnPaginationSymptoms.Visible = true;
                        }
                    }
                    if (this.Session["UserType"].ToString() == "Agroquímicos")
                    {
                        this.Response.Redirect(this._modulo);
                    }
                }
                /////////////
            }
            else
                this.Response.Redirect(this.getPage());





        }
        else
        {
            this.ddlDivisions.Enabled = false;
            this.ddlDivisions.SelectedIndex = -1;
        }
    }

    protected void ddlDivisions_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (this.ddlDivisions.SelectedItem.ToString() != "SELECCIONAR")
        {
            this._divisionId = Convert.ToInt32(this.ddlDivisions.SelectedValue == null ? -1 : Convert.ToInt32(this.ddlDivisions.SelectedValue));
            this.Session["divisionId"] = this._divisionId.ToString();
            this.Session["divisionName"] = this.ddlDivisions.SelectedItem.ToString();
            this.Session["countryId"] = Convert.ToInt32(this.ddlCountries.SelectedValue);
            this.Session["editionId"] = Convert.ToInt32(this.ddlEditions.SelectedValue);
            this.Session["bookId"] = Convert.ToInt32(this.ddlBooks.SelectedValue);
            if (this._roleDescription == "Administrador" || this._roleDescription == "Diseñador")
                this.Response.Redirect(this._modulo);
            else
                this.Response.Redirect(this.getPage());
        }
    }

    protected void btnPagination_OnClick(object sender, EventArgs e)
    {
        this.Session["countryId"] = Convert.ToInt32(this.ddlCountries.SelectedValue);
        this._countryId = Convert.ToInt32(this.Session["countryId"]);
        this.Session["editionId"] = Convert.ToInt32(this.ddlEditions.SelectedValue);
        this._editionId = Convert.ToInt32(this.Session["editionId"]);
        this.Session["bookId"] = Convert.ToInt32(this.ddlBooks.SelectedValue);
        this._bookId = Convert.ToInt32(this.Session["bookId"]);
        this.Session["divisionName"] = this._divisionName.ToString();
        this.Response.Redirect("pagedProduct.aspx");
    }

    #endregion

    #region Private Methods

    private void getCountries()
    {
        if (string.IsNullOrEmpty(this._countries))
            this.ddlCountries.DataSource = AgroBusinessLogicComponent.CatalogsBLC.Instance.getCountry(this._userCountry);
        else
            this.ddlCountries.DataSource = AgroBusinessLogicComponent.CatalogsBLC.Instance.getCountries(this._countries);

        this.ddlCountries.DataTextField = "CountryName";
        this.ddlCountries.DataValueField = "CountryId";
        this.ddlCountries.DataBind();

        // Add the initial value:
        ListItem initialLine = new ListItem("SELECCIONAR", "-1", true);
        initialLine.Selected = true;
        this.ddlCountries.Items.Insert(0, initialLine);

        if (this._countryId != -1)
        {
            this.getBooks();
            this.ddlBooks.Enabled = true;
        }
        this.ddlCountries.SelectedValue = this._countryId.ToString();
    }

    private void getBooks()
    {
        if (this._modulo == "encyclopediaImages1.aspx")
            this.ddlBooks.DataSource = AgroBusinessLogicComponent.CatalogsBLC.Instance.getEncyclopediaBooksByCountry(this._countryId);
        else
            this.ddlBooks.DataSource = AgroBusinessLogicComponent.CatalogsBLC.Instance.getBooksByCountry(this._countryId);
        this.ddlBooks.DataTextField = "Description";
        this.ddlBooks.DataValueField = "BookId";
        this.ddlBooks.DataBind();

        // Add the initial value:
        ListItem initialLine = new ListItem("SELECCIONAR", "-1", true);
        initialLine.Selected = true;
        this.ddlBooks.Items.Insert(0, initialLine);

        if (this._bookId != -1)
        {
            this.getEditions();
            this.ddlEditions.Enabled = true;
        }
        this.ddlBooks.SelectedValue = this._bookId.ToString();
    }

    private void getEditions()
    {
        if (this._modulo == "encyclopediaImages1.aspx")
            this.ddlEditions.DataSource = AgroBusinessLogicComponent.CatalogsBLC.Instance.getEncyclopediaEditionsByBook(this._bookId, _countryId);
        else
            this.ddlEditions.DataSource = AgroBusinessLogicComponent.CatalogsBLC.Instance.getEditionsByBook(this._bookId, _countryId);
        this.ddlEditions.DataTextField = "NumberEdition";
        this.ddlEditions.DataValueField = "EditionId";
        this.ddlEditions.DataBind();

        // Add the initial value:
        ListItem initialLine = new ListItem("SELECCIONAR", "-1", true);
        initialLine.Selected = true;
        this.ddlEditions.Items.Insert(0, initialLine);
        this.ddlEditions.SelectedValue = this._editionId.ToString();
    }

    private void getDivisions(int countryId)
    {
        this.ddlDivisions.DataSource = AgroBusinessLogicComponent.CatalogsBLC.Instance.getDivisionsByCountry(countryId);
        this.ddlDivisions.DataTextField = "ShortName";
        this.ddlDivisions.DataValueField = "DivisionId";
        this.ddlDivisions.DataBind();

        // Add the initial value:
        ListItem initialLine = new ListItem("SELECCIONAR", "-1", true);
        initialLine.Selected = true;
        this.ddlDivisions.Items.Insert(0, initialLine);

        //this.ddlDivisions.SelectedValue = this._divisionId.ToString();
        this.Session["divisionName"] = this._divisionId == -1 ? null : this.ddlDivisions.SelectedItem.Text;
    }

    private void clearProperties()
    {
        if (this.ddlDivisions.Visible == true)
        {

            ddlDivisions.ClearSelection();


        }
        else if (ddlEditions.Visible == true)
        {
            ddlEditions.ClearSelection();

        }
        else if (ddlBooks.Visible == true)
        {
            ddlBooks.ClearSelection();

        }

        this.Session["countryId"] = null;
        this.Session["editionId"] = null;
        this.Session["bookId"] = null;




        this.Session["divisionName"] = null;
    }

    private void displayControls()
    {

        if (this._roleId != (int)Utilities.Roles.Vendedor && this._roleId != (int)Utilities.Roles.Diagramador && this._roleId != (int)Utilities.Roles.Administrador && this._roleId != (int)Utilities.Roles.Diseñador)
        {
            this.lblLaboratory.Visible = false;
            this.ddlDivisions.Visible = false;

        }
        else if (Session["UserType"] != null)
            if (Session["UserType"].ToString() == "Agroquímicos")
            {
                this.lblLaboratory.Visible = false;
                this.ddlDivisions.Visible = false;
            }

    }
    private void getControls()
    {
        if (this._modulo == "encyclopediaImages1.aspx")
        {
            this.ddlDivisions.Visible = false;
            this.lblLaboratory.Visible = false;


        }
        if (this._modulo == "classifiedLaboratoriesLogos.aspx")
        {

            this.lblLaboratory.Visible = false;
            this.ddlDivisions.Visible = false;

        }
        if (this._modulo == "classifiedProductShots.aspx")
        {

            this.lblBook.Visible = false;
            this.lblEdition.Visible = false;

            this.ddlBooks.Visible = false;
            this.ddlEditions.Visible = false;

        }
    }

    private string getPage()
    {
        switch (this._roleId)
        {
            case (int)Utilities.Roles.Vendedor:
                return "products.aspx";
            case (int)Utilities.Roles.Agroquimico:
                return "assignedProducts.aspx";
            case (int)Utilities.Roles.Diagramador:
                return "classifiedProducts.aspx";
            //return "LaboratoriesLogos.aspx";

            default:
                return "products.aspx";
        }
    }

    private void exportPdf()
    {

        DevExpress.XtraPrinting.PdfExportOptions pdfExport = new DevExpress.XtraPrinting.PdfExportOptions();
        pdfExport.DocumentOptions.Title = "Participant Products";
        pdfExport.DocumentOptions.Author = "Agro 2.0";
        _date = DateTime.Now.ToString();
        List<string> titles = new List<string>();
        titles.Add("Listado de Productos Participantes");
        titles.Add("Obra : " + _bookName);
        titles.Add("Edición : " + _editionName);
        titles.Add("Usuario: " + AgroBusinessLogicComponent.FilesBLC.Instance.getUserName(_userId));
        titles.Add("Fecha: " + _date);

        System.Data.DataTable dtProds = new System.Data.DataTable();
        dtProds.Columns.Add("Laboratorio");
        dtProds.Columns.Add("Categoria");
        dtProds.Columns.Add("Producto");
        dtProds.Columns.Add("Forma farmaceutica");
        dtProds.Columns.Add("Tipo de Producto");
        dtProds.Columns.Add("Descripción");
        string letterToSearch = "%";
        List<AgroBusinessEntries.PagedProductInfo> products =
        AgroBusinessLogicComponent.ParticipantProductsBLC.Instance.getProductPaged(this._editionId, letterToSearch);

        foreach (AgroBusinessEntries.PagedProductInfo prod in products)
        {
            dtProds.Rows.Add(prod.DivisionShortName, prod.CategoryName, prod.Brand, prod.PharmaForm, prod.ProductType, prod.ProductDescription);
        }
        //AgroBusinessEntries.VersionFileInfo VersionFile = AgroBusinessLogicComponent.FilesBLC.Instance.getVersionFile(getPrefixApplication(), _editionId);
        string route = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["BaseURL"]);
        string fileName = "ProductosParticipantes_" + getPrefixApplication() + ".pdf";
        string filePath = route + @"\" + fileName;
        createReport(dtProds, titles).ExportToPdf(filePath, pdfExport);
        AgroBusinessEntries.FileInfo fileInfo = new AgroBusinessEntries.FileInfo();
        fileInfo.BaseUrl = route;
        fileInfo.FileDate = DateTime.Parse(_date);
        fileInfo.FileName = fileName;
       // fileInfo.FormatFileId = AgroBusinessLogicComponent.FilesBLC.Instance.getFormatFileId("pdf");
        fileInfo.NickName = _user;
        fileInfo.Version = getPrefixApplication();
       // fileInfo.VersionFileId = VersionFile.VersionFileId;
       // AgroBusinessLogicComponent.FilesBLC.Instance.addFile(fileInfo, getPrefixApplication());
        filePath = filePath.Replace(@"\", "/");
        ClientScript.RegisterStartupScript(this.GetType(), "down", "downFile('" + filePath + "','" + fileInfo.FileName + "','application/vnd.ms-excel');", true);


    }
    private void exportXls()
    {
        DevExpress.XtraPrinting.XlsExportOptions xlsExport = new DevExpress.XtraPrinting.XlsExportOptions();
        xlsExport.SheetName = "Productos Particiapantes";
        _date = DateTime.Now.ToString();
        List<string> titles = new List<string>();
        titles.Add("Listado de Productos Participantes");
        titles.Add("Obra : " + _bookName);
        titles.Add("Edición : " + _editionName);
        titles.Add("Usuario: " + AgroBusinessLogicComponent.FilesBLC.Instance.getUserName(_userId));
        titles.Add("Fecha: " + _date);

        System.Data.DataTable dtProds = new System.Data.DataTable();
        dtProds.Columns.Add("Laboratorio");
        dtProds.Columns.Add("Categoria");
        dtProds.Columns.Add("Producto");
        dtProds.Columns.Add("Forma farmaceutica");
        dtProds.Columns.Add("Tipo de Producto");
        List<AgroBusinessEntries.PagedProductInfo> products;
        string letterToSearch = "%";
        if (this._bookShortName.Equals(System.Configuration.ConfigurationManager.AppSettings["Symptoms"]))
        {
            dtProds.Columns.Add("Síntoma del producto");
            products =
        AgroBusinessLogicComponent.ParticipantProductsBLC.Instance.getProductPagedSymptom(this._editionId, letterToSearch);
        }
        else
        {
            products =
        AgroBusinessLogicComponent.ParticipantProductsBLC.Instance.getProductPaged(this._editionId, letterToSearch);
        }
        dtProds.Columns.Add("Descripción");
        dtProds.Columns.Add("Nuevo");
        dtProds.Columns.Add("Con Cambios");
        dtProds.Columns.Add("ProductShot");
  

        foreach (AgroBusinessEntries.PagedProductInfo prod in products)
        {
            if (this._bookShortName.Equals(System.Configuration.ConfigurationManager.AppSettings["Symptoms"]))
            {
                dtProds.Rows.Add(prod.DivisionShortName, prod.CategoryName, prod.Brand, prod.PharmaForm, prod.ProductType, prod.Symptoms, prod.ProductDescription, prod.NewProduct ? "Si" : "No", prod.ModifiedContent ? "Si" : "No", prod.Sidef ? "Si" : "No");
            }
            else
            {
                dtProds.Rows.Add(prod.DivisionShortName, prod.CategoryName, prod.Brand, prod.PharmaForm, prod.ProductType, prod.ProductDescription, prod.NewProduct ? "Si" : "No", prod.ModifiedContent ? "Si" : "No", prod.Sidef ? "Si" : "No");
            }

        }
        //AgroBusinessEntries.VersionFileInfo VersionFile = AgroBusinessLogicComponent.FilesBLC.Instance.getVersionFile(getPrefixApplication(), _editionId);
        string route = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["BaseURL"]);
        string fileName = "ProductosParticipantes_" + getPrefixApplication() +  ".xls";
        string filePath = route + @"\" + fileName;

        MemoryStream ms = new MemoryStream();
        this.createReport(dtProds, titles).ExportToXls(ms);
        byte[] buffer = new byte[ms.Length];
        ms.Position = 0;
        ms.Read(buffer, 0, (int)ms.Length);
        ms.Close();
        this.Session["doc"] = buffer;
        //.ExportToXls(filePath,xlsExport);
        AgroBusinessEntries.FileInfo fileInfo = new AgroBusinessEntries.FileInfo();
        fileInfo.BaseUrl = route;
        fileInfo.FileDate = DateTime.Parse(_date);
        fileInfo.FileName = fileName;
        //fileInfo.FormatFileId = AgroBusinessLogicComponent.FilesBLC.Instance.getFormatFileId("xls");
        fileInfo.NickName = _user;
        fileInfo.Version = getPrefixApplication() ;
        //fileInfo.VersionFileId = VersionFile.VersionFileId;
      //  AgroBusinessLogicComponent.FilesBLC.Instance.addFile(fileInfo, getPrefixApplication());
        filePath = filePath.Replace(@"\", "/");
        ClientScript.RegisterStartupScript(this.GetType(), "down", "downFile('" + filePath + "','" + fileInfo.FileName + "','application/vnd.ms-excel');", true);

    }
    private DevExpress.XtraReports.UI.XtraReport createReport(DataTable data, List<string> titles)
    {
        System.Data.DataTable dataContent = data;
        Report report = new Report();
        ReportHeaderBand header = new ReportHeaderBand();
        XRTable tableHeader = getReportHeader(titles);
        header.Controls.Add(tableHeader);
        DetailReportBand rband = new DetailReportBand();
        DetailBand band = new DetailBand();
        XRTable table = new XRTable();
        table.Borders = DevExpress.XtraPrinting.BorderSide.All;
        table.SizeF = new System.Drawing.SizeF(650f, 10f);
        band.Controls.Add(table);
        XRTableRow tRow = new DevExpress.XtraReports.UI.XRTableRow();
        XRTableCell col = new DevExpress.XtraReports.UI.XRTableCell();
        foreach (System.Data.DataColumn colu in dataContent.Columns)
        {
            col = new DevExpress.XtraReports.UI.XRTableCell();
            col.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(8)))), ((int)(((byte)(1)))), ((int)(((byte)(41)))));
            col.ForeColor = Color.White;
            col.SizeF = new System.Drawing.SizeF(50F, 45F);
            col.Text = colu.ColumnName;
            col.Font = new Font("Tahoma", 10, FontStyle.Bold);
            tRow.Cells.Add(col);
        }
        table.Rows.Add(tRow);
        List<XRTableRow> ROWS = new List<XRTableRow>();
        XRTableRow tContentRow = new DevExpress.XtraReports.UI.XRTableRow();
        XRTableCell column = new DevExpress.XtraReports.UI.XRTableCell();
        column.Font = new Font("tahoma", 10, FontStyle.Regular);
        column.SizeF = new System.Drawing.SizeF(50F, 45F);
        for (int r = 0; r < dataContent.Rows.Count; r++)
        {
            tContentRow = new DevExpress.XtraReports.UI.XRTableRow();
            for (int c = 0; c < dataContent.Columns.Count; c++)
            {
                column = new DevExpress.XtraReports.UI.XRTableCell();
                column.Text = dataContent.Rows[r][c].ToString();
                tContentRow.Cells.Add(column);
            }
            ROWS.Add(tContentRow);

        }
        XRTableRow[] arr = new XRTableRow[ROWS.Count];
        for (int i = 0; i < ROWS.Count; i++)
        {
            arr[i] = ROWS[i];
        }
        table.Rows.AddRange(arr);
        rband.Bands.Add(band);
        report.Bands.Add(header);
        report.Bands.Add(rband);
        return report;
    }
    public XRTable getReportHeader(List<string> titles)
    {
        XRTable tableHeader = new XRTable();

        tableHeader.SizeF = new System.Drawing.SizeF(850f, 10f);

        foreach (string val in titles)
        {
            XRTableCell title = new DevExpress.XtraReports.UI.XRTableCell();
            title.Text = val;
            title.Font = new Font("Tahoma", 14, FontStyle.Bold);
            XRTableRow tRowHeader = new DevExpress.XtraReports.UI.XRTableRow();
            tRowHeader.Cells.AddRange(new XRTableCell[] { title });
            tableHeader.Rows.Add(tRowHeader);
        }
        return tableHeader;
    }
    private string getPrefixApplication()
    {
        switch (this._roleId)
        {
            case (int)Utilities.Roles.Administrador:
                return "MA";
            case (int)Utilities.Roles.Agroquimico:
                return "MM";
            case (int)Utilities.Roles.Diagramador:
                return "MPO";
            case (int)Utilities.Roles.Vendedor:
                return "MV";
            default:
                return "MA";
        }
    }
    #endregion

    private int _editionId;
    private string _modulo;
    private int _pagina;
    private int _countryId;
    private int _divisionId;
    private int _bookId;
    private int _userCountry;
    private int _roleId;
    private string _countries;
    private string _user;
    private string _bookName;
    private string _editionName;
    private string _divisionName;
    private int _userId;
    private string _date;
    private string _bookShortName;
    private string _roleDescription;
    protected void imgBtnExport_Click(object sender, ImageClickEventArgs e)
    {
        this.Session["countryId"] = Convert.ToInt32(this.ddlCountries.SelectedValue);
        this._countryId = Convert.ToInt32(this.Session["countryId"]);
        this.Session["editionId"] = Convert.ToInt32(this.ddlEditions.SelectedValue);
        this._editionId = Convert.ToInt32(this.Session["editionId"]);
        this.Session["bookId"] = Convert.ToInt32(this.ddlBooks.SelectedValue);
        this._bookId = Convert.ToInt32(this.Session["bookId"]);
        this.Session["divisionName"] = this._divisionName.ToString();
        exportPdf();
    }
    protected void imgBtnExportxls_Click(object sender, ImageClickEventArgs e)
    {
        this.Session["countryId"] = Convert.ToInt32(this.ddlCountries.SelectedValue);
        this._countryId = Convert.ToInt32(this.Session["countryId"]);
        this.Session["editionId"] = Convert.ToInt32(this.ddlEditions.SelectedValue);
        this._editionId = Convert.ToInt32(this.Session["editionId"]);
        this.Session["bookId"] = Convert.ToInt32(this.ddlBooks.SelectedValue);
        this._bookId = Convert.ToInt32(this.Session["bookId"]);
        this.Session["divisionName"] = this._divisionName.ToString();
        exportXls();
    }
    protected void btnPaginationSymptoms_Click(object sender, EventArgs e)
    {
        this.Response.Redirect("pagedSymptoms.aspx");
    }

    protected void setBlock()
    {
        if (!(this._roleId == (int)Utilities.Roles.Vendedor || this._roleId == (int)Utilities.Roles.Diagramador || this._roleId == (int)Utilities.Roles.Administrador || this._roleId == (int)Utilities.Roles.Diseñador))
        {
            ddlEditions.Attributes.Add("onchange", "mostrar_procesar();");
        }
        else
        {

            ddlDivisions.Attributes.Add("onchange", "mostrar_procesar();");
        }
    }
}