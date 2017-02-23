using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.IO;
using System.Data;
using DevExpress.XtraReports.UI;
using DevExpress.XtraReports.Parameters;
using System.Drawing;
using System.Drawing.Printing;
public partial class assignedProducts : System.Web.UI.Page
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
        this._user = this.Session["user"] == null ? "" : this.Session["user"].ToString();
        this._roleId = this.Session["roleId"] == null ? -1 : Convert.ToInt32(this.Session["roleId"]);
        this._bookName = this.Session["bookName"] == null ? "" : this.Session["bookName"].ToString();
        this._editionName = this.Session["edition"] == null ? "" : (this.Session["edition"]).ToString();
        this._divisionName = this.Session["divisionName"] == null ? "" : this.Session["divisionName"].ToString();
        this._bookShortName = this.Session["bookShortName"] == null ? "" : this.Session["bookShortName"].ToString();
        MedinetBusinessEntries.EditionsInfo editionInfo = MedinetBusinessLogicComponent.CatalogsBLC.Instance.getEdition(this._editionId);
        
        if (this._bookShortName.Equals(System.Configuration.ConfigurationManager.AppSettings["Symptoms"]))
        {
            aspGrdProducts.Columns["Síntomas"].Visible = true;
        }
        else {
            aspGrdProducts.Columns["Síntomas"].Visible = false;
        }
        //aspGrdProducts.Columns["Contraindicaciones"].Visible = false;
        
        if (editionInfo.ParentId == null)
            this.imgBtnATCMed.Visible = false;

        //User autorized
        if (this.Session["user"].ToString() == System.Configuration.ConfigurationManager.AppSettings["NickUser"])
        {
            this.imgBtnSubst.Visible = true;
            this.imgBtnInd.Visible = true;
        }

        if (this._editionId != -1 && this._countryId != -1) {
            this.saveProductType();
            this._productIds = this.Session["products"] == null ? "" : this.Session["products"].ToString();
            this._productTypeIds = this.Session["prodType"] == null ? "" : this.Session["prodType"].ToString();
           this.getProducts();
           
        }
        
        if (!IsPostBack)
        {
        
            this.aspGrdProducts.FilterExpression = this._gridStatus;
        }
        else
        {
           
        }
        this.BtnSaveDown.Click += new EventHandler(btnSaveTop_Click);
        
    }

    #endregion

    #region Control Events

    protected void aspGrdProducts_RowCommand(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewRowCommandEventArgs e)
    {
        switch (e.CommandArgs.CommandName)
        {
            case "Edit":
                
                this.saveProductsTypeSelected();
                this.saveProductType();
                this.saveGridStatus();
                this.Session["divisionId"] = Convert.ToInt32(e.CommandArgs.CommandArgument.ToString().Split('-')[0]);
                this.Session["categoryId"] = Convert.ToInt32(e.CommandArgs.CommandArgument.ToString().Split('-')[1]);
                this.Session["productId"] = Convert.ToInt32(e.CommandArgs.CommandArgument.ToString().Split('-')[2]);
                this._productId = Convert.ToInt32(this.Session["productId"]);
                this.Session["pharmaFormId"] = Convert.ToInt32(e.CommandArgs.CommandArgument.ToString().Split('-')[3]);
                this._pharmaFormId = Convert.ToInt32(this.Session["pharmaFormId"]);
                this.getProductName();
                this.getPharmaForm();
                this.Response.Redirect("activeSubstances.aspx");

                break;

            default:
                return;
        }
    }

    protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.Session["CurrentPage"] = 0;
        if (this.ddlPageSize.SelectedValue != "-1")
        {
            this.aspGrdProducts.SettingsPager.Mode = DevExpress.Web.ASPxGridView.GridViewPagerMode.ShowPager;
            this.aspGrdProducts.SettingsPager.PageSize = Convert.ToInt32(this.ddlPageSize.SelectedValue);
        }
        else
        {
            this.aspGrdProducts.SettingsPager.Mode = DevExpress.Web.ASPxGridView.GridViewPagerMode.ShowAllRecords;
        }
        this.getProducts();
    }

    protected void imgBtnBack_Click(object sender, ImageClickEventArgs e)
    {
        this.Session["productId"] = null;
        this.Session["pharmaFormId"] = null;
        this.Session["editionId"] = null;
        this.Response.Redirect("laboratories.aspx");
    }

    protected void imgBtnSubst_Click(object sender, ImageClickEventArgs e)
    {
        this.Response.Redirect("editSubstances.aspx");
    }

    protected void imgBtnInd_Click(object sender, ImageClickEventArgs e)
    {
        this.Response.Redirect("editIndications.aspx");
    }

    protected void imgBtnATC_Click(object sender, ImageClickEventArgs e)
    { }

    protected void imgBtnATCMed_Click(object sender, ImageClickEventArgs e)
    {
        this.Response.Redirect("addProductsByTherapeutic.aspx");
    }

    #endregion

    #region Protected Methods

    protected string getData(int divisionId, int categoryId, int productId, int pharmaFormId)
    {
        string pharmaForm;
        pharmaForm = string.IsNullOrEmpty(pharmaFormId.ToString()) ? "" : pharmaFormId.ToString();
        return divisionId + "-" + categoryId + "-" + productId + "-" + pharmaForm;
    }

    #endregion

    #region Private Methods



    
    #region ExportData
    private void exportPdf()
    {
        DevExpress.XtraPrinting.PdfExportOptions pdfExport = new DevExpress.XtraPrinting.PdfExportOptions();
        pdfExport.DocumentOptions.Title = "Participant Products";
        pdfExport.DocumentOptions.Author = "Medinet 2.0";
        pdfExport.Compressed = true;
        _date = DateTime.Now.ToString();
        List<string> titles = new List<string>();
        titles.Add("Listado de Productos Participantes");
        titles.Add("Obra : " + _bookName);
        titles.Add("Edición : " + _editionName);
        titles.Add("Usuario: " + MedinetBusinessLogicComponent.FilesBLC.Instance.getUserName(_userId));
        titles.Add("Fecha: " + _date);

        System.Data.DataTable dtProds = new System.Data.DataTable();
        dtProds.Columns.Add("Laboratorio");
        dtProds.Columns.Add("Categoria");
        dtProds.Columns.Add("Producto");
        dtProds.Columns.Add("Forma farmaceutica");
        dtProds.Columns.Add("Tipo de producto");
        dtProds.Columns.Add("Descripción");
        List<MedinetBusinessEntries.AssignedProduct> products = MedinetBusinessLogicComponent.CatalogsBLC.Instance.getNoAssigned(this._editionId);
        foreach (MedinetBusinessEntries.AssignedProduct prod in products)
        {
            dtProds.Rows.Add(prod.DivisionShortName, prod.CategoryName, prod.Brand, prod.PharmaForm,prod.ProductType, prod.ProductDescription);
        }
        MedinetBusinessEntries.VersionFileInfo VersionFile = MedinetBusinessLogicComponent.FilesBLC.Instance.getVersionFile(getPrefixApplication(), _editionId);
        string route = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["BaseURL"]);
        string fileName = "ProductosParticipantes_" + getPrefixApplication() + "_" + VersionFile.CurrentValue + ".pdf";
        string filePath = route + @"\" + fileName;
        createReport(dtProds, titles).ExportToPdf(filePath, pdfExport);
        MedinetBusinessEntries.FileInfo fileInfo = new MedinetBusinessEntries.FileInfo();
        fileInfo.BaseUrl = route;
        fileInfo.FileDate = DateTime.Parse(_date);
        fileInfo.FileName = fileName;
        fileInfo.FormatFileId = MedinetBusinessLogicComponent.FilesBLC.Instance.getFormatFileId("pdf");
        fileInfo.NickName = _user;
        fileInfo.Version = getPrefixApplication() + "_" + (VersionFile.CurrentValue.ToString());
        fileInfo.VersionFileId = VersionFile.VersionFileId;
        MedinetBusinessLogicComponent.FilesBLC.Instance.addFile(fileInfo, getPrefixApplication());
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
        titles.Add("Usuario: " + MedinetBusinessLogicComponent.FilesBLC.Instance.getUserName(_userId));
        titles.Add("Fecha: " + _date);

        System.Data.DataTable dtProds = new System.Data.DataTable();
        dtProds.Columns.Add("Laboratorio");
        dtProds.Columns.Add("Categoria");
        dtProds.Columns.Add("Producto");
        dtProds.Columns.Add("Forma farmaceutica");
        dtProds.Columns.Add("Tipo de producto");
        dtProds.Columns.Add("Descripción");
        dtProds.Columns.Add("Nuevo");
        dtProds.Columns.Add("Con Cambios");
        dtProds.Columns.Add("ProductShot");
        dtProds.Columns.Add("Ejecutivo(s)");
        List<MedinetBusinessEntries.AssignedProduct> products = MedinetBusinessLogicComponent.CatalogsBLC.Instance.getNoAssigned(this._editionId);
        foreach (MedinetBusinessEntries.AssignedProduct prod in products)
        {
            dtProds.Rows.Add(prod.DivisionShortName, prod.CategoryName, prod.Brand, prod.PharmaForm,prod.ProductType, prod.ProductDescription, prod.NewProduct ? "Si" : "No", prod.ModifiedContent ? "Si" : "No", prod.Sidef ? "Si" : "No", MedinetBusinessLogicComponent.FilesBLC.Instance.getUserAgentByDivisionByEditionByAction(this._editionId, Convert.ToInt32(prod.DivisionId), Convert.ToInt32(prod.CategoryId), Convert.ToInt32(prod.PharmaFormId), prod.ProductId));
        }
        MedinetBusinessEntries.VersionFileInfo VersionFile = MedinetBusinessLogicComponent.FilesBLC.Instance.getVersionFile(getPrefixApplication(), _editionId);
        string route = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["BaseURL"]);
        string fileName = "ProductosParticipantes_" + getPrefixApplication() + "_" + VersionFile.CurrentValue + ".xls";
        string filePath = route + @"\" + fileName;
        DevExpress.XtraReports.Web.ReportViewer rv = new DevExpress.XtraReports.Web.ReportViewer();
        //this.Session["file"]=createReport(dtProds, titles);
        MemoryStream ms = new MemoryStream();
        this.createReport(dtProds, titles).ExportToXls(ms);
        byte[] buffer = new byte[ms.Length];
        ms.Position = 0;
        ms.Read(buffer, 0, (int)ms.Length);
        ms.Close();
        this.Session["doc"] = buffer;
        //.ExportToXls(filePath,xlsExport);
        MedinetBusinessEntries.FileInfo fileInfo = new MedinetBusinessEntries.FileInfo();
        fileInfo.BaseUrl = route;
        fileInfo.FileDate = DateTime.Parse(_date);
        fileInfo.FileName = fileName;
        fileInfo.FormatFileId = MedinetBusinessLogicComponent.FilesBLC.Instance.getFormatFileId("xls");
        fileInfo.NickName = _user;
        fileInfo.Version = getPrefixApplication() + "_" + (VersionFile.CurrentValue.ToString());
        fileInfo.VersionFileId = VersionFile.VersionFileId;
        MedinetBusinessLogicComponent.FilesBLC.Instance.addFile(fileInfo, getPrefixApplication());
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
    private string getPrefixApplication() {
        switch (this._roleId)
        {
            case (int)Utilities.Roles.Administrador:
                return "MA";
            case (int)Utilities.Roles.Medico:
                return "MM";
            
            default:
                return "MA";
        }
    }
    #endregion

    private void getProducts()
    {
        this.aspGrdProducts.DataSource = MedinetBusinessLogicComponent.CatalogsBLC.Instance.getNoAssigned(this._editionId);
        this.aspGrdProducts.DataBind();
    }

    private void getProductName()
    {
        MedinetBusinessEntries.ProductsInfo prodInfo = MedinetBusinessLogicComponent.ProductsBLC.Instance.getProductById(this._productId);
        this.Session["brand"] = prodInfo == null ? "Producto Nuevo" : prodInfo.Brand;
    }

    private void getPharmaForm()
    {
        MedinetBusinessEntries.PharmaceuticalFormInfo pharmaInfo = MedinetBusinessLogicComponent.CatalogsBLC.Instance.getPharmaForm(this._pharmaFormId);
        this.Session["pharmaForm"] = pharmaInfo == null ? "" : pharmaInfo.Description;
    }

    private void saveGridStatus()
    {
        DevExpress.Web.ASPxGridView.GridViewDataCheckColumn checkATC = (DevExpress.Web.ASPxGridView.GridViewDataCheckColumn)this.aspGrdProducts.Columns[7];
        DevExpress.Web.ASPxGridView.GridViewDataCheckColumn checkSubstances = (DevExpress.Web.ASPxGridView.GridViewDataCheckColumn)this.aspGrdProducts.Columns[8];
        DevExpress.Web.ASPxGridView.GridViewDataCheckColumn checkIndications = (DevExpress.Web.ASPxGridView.GridViewDataCheckColumn)this.aspGrdProducts.Columns[9];
        //DevExpress.Web.ASPxGridView.GridViewDataCheckColumn checkInteractions = (DevExpress.Web.ASPxGridView.GridViewDataCheckColumn)this.aspGrdProducts.Columns[10];
        
        this._gridStatus = checkATC.FilterExpression == "" ? "" : checkATC.FilterExpression;
        
        if (this._gridStatus != "")
        {
            if (checkSubstances.FilterExpression != "")
                this._gridStatus = this._gridStatus + " AND " + checkSubstances.FilterExpression;
        }
        else
            if (checkSubstances.FilterExpression != "")
                this._gridStatus = checkSubstances.FilterExpression;


        if (this._gridStatus != "")
        {
            if (checkIndications.FilterExpression != "")
                this._gridStatus = this._gridStatus + " AND " + checkIndications.FilterExpression;
        }
        else
            if (checkIndications.FilterExpression != "")
                this._gridStatus = checkIndications.FilterExpression;

        //if (this._gridStatus != "")
        //{
        //    if (checkInteractions.FilterExpression != "")
        //        this._gridStatus = this._gridStatus + " AND " + checkInteractions.FilterExpression;
        //}
        //else
        //    if (checkInteractions.FilterExpression != "")
        //        this._gridStatus = checkInteractions.FilterExpression;

        this.Session["gridStatus"] = this._gridStatus;
    }

    
    #endregion

    private int _editionId;
    private int _countryId;
    private int _userId;
    private int _userCountry;
    private int _productId;
    private int _pharmaFormId;
    private string _hashkey;
    private string _gridStatus;
    private string _user;
    private int _roleId;
    private string _bookName;
    private string _editionName;
    private string _divisionName; 
    private string _date;
    private string _bookShortName;
    private string _productIds;
    private string _productTypeIds;
    protected void imgBtnExport_Click(object sender, ImageClickEventArgs e)
    {
        exportPdf();
    }
    protected void imgBtnExportxls_Click(object sender, ImageClickEventArgs e)
    {
        exportXls();
    }



    protected void aspGrdProducts_HtmlRowCreated(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType == DevExpress.Web.ASPxGridView.GridViewRowType.Data)
        {

            
            DropDownList ddlProductType = aspGrdProducts.FindRowCellTemplateControl(e.VisibleIndex, null, "ddlProductType") as DropDownList;
            
            int textPro = (int)aspGrdProducts.GetRowValues(e.VisibleIndex, "ProductId");
            string prodTyp = (string)aspGrdProducts.GetRowValues(e.VisibleIndex, "ProductType");
            int prodTypId = (int)aspGrdProducts.GetRowValues(e.VisibleIndex, "ProductTypeId");

            if (ddlProductType!=null)
            {
                List<MedinetBusinessEntries.ProductTypeInfo> ptList = MedinetBusinessLogicComponent.CatalogsBLC.Instance.getProductTypes();
               /* if (prodTypId==0)
                {
                    ptList.Add(new MedinetBusinessEntries.ProductTypeInfo { ProductTypeId = 0, TypeName = "Sin asignar" });    
                }*/
                
                ddlProductType.DataSource = ptList;
                ddlProductType.DataTextField = "Typename";
                ddlProductType.DataValueField = "ProductTypeId";
                ddlProductType.DataBind();
                ddlProductType.SelectedValue = prodTypId.ToString();
                ddlProductType.Attributes.Add("productId", textPro.ToString());
                
            }
            
        }
       
    }

    protected void ddlProductType_SelectedIndexChanged(object sender, EventArgs e)
    {
        
    }

    protected void aspGrdProducts_SelectionChanged(object sender, EventArgs e)
    {

    }
    
    private void saveProductType() {
        DropDownList ddlType;
        string productTy = "",productIds="";

        for (int gridRow = 0; gridRow < this.aspGrdProducts.VisibleRowCount; gridRow++)
        {
            ddlType = aspGrdProducts.FindRowCellTemplateControl(gridRow, null, "ddlProductType") as DropDownList;
            if (ddlType!=null)
            {
                    if (productIds.Length!=0)
                    {
                        productIds += "-" + aspGrdProducts.GetRowValues(gridRow, "ProductId").ToString();
                    }
                    else
                    {
                        productIds += aspGrdProducts.GetRowValues(gridRow, "ProductId").ToString();
                    }

                    if (productTy.Length != 0)
                    {
                        productTy += "-" + ddlType.SelectedValue;
                    }
                    else 
                    {
                    productTy += ddlType.SelectedValue;
                    }
            }
        }
        
        this.Session["products"] = productIds;
        this.Session["prodType"] = productTy;
        
    }

    private void saveProductsTypeSelected() {

        string[] idsProducts = this._productIds.Split('-');
        string[] idsProdType = this._productTypeIds.Split('-');
        for (int i = 0; i < idsProducts.Length; i++)
        {
            MedinetBusinessEntries.ProductsInfo product = MedinetBusinessLogicComponent.ProductsBLC.Instance.getProductById(int.Parse(idsProducts[i]));
            product.ProductTypeId = int.Parse(idsProdType[i]);
            MedinetBusinessLogicComponent.ProductsBLC.Instance.updateProduct(product, this._userId, this._hashkey);
        }
    
    }
    protected void btnSaveTop_Click(object sender, EventArgs e)
    {
        
        if (this._productIds.Length>0)
        {
            this.saveProductsTypeSelected();
           // this.getProducts();
            ScriptManager.RegisterStartupScript(this, typeof(Page), "messageScript", "jAlert('Cambios guardados con exito', 'Tipo de producto');", true);
            
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "messageScript", "jAlert('Ningun cambio realizado cambie al menos un producto', 'Tipo de producto');", true);
        }
       // this.Response.Redirect("assignedProducts.aspx");
    }
}
