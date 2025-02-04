﻿using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DevExpress.XtraReports.UI;
using DevExpress.XtraReports.Parameters;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Web.UI.HtmlControls;
using System;
using System.Collections.Generic;
using DevExpress.Web.ASPxGridView;
public partial class LaboratoriesLogos : System.Web.UI.Page
{
    public static string[] MyValue = { "~/palomita.gif", "~/Images/tache.gif" };
    public static int cont = 0;
    static String[] archivos = { "105x330", "125x390", "320x384", "384x512", "400x400" };
    #region Page Events

    protected void Page_Load(object sender, EventArgs e)
    {
        this.Session["divisionName"] = null;
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
       

       
    }

    #endregion

    #region Control Events

    protected void grdProducts_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        this.grdProducts.PageIndex = e.NewPageIndex;
        this.Session["CurrentPage"] = Convert.ToInt32(e.NewPageIndex);
        this.getEditableProducts();
    }

    

    protected void grdProducts_RowEditing(object sender, GridViewEditEventArgs e)
    {
        //this.grdProducts.EditIndex = e.NewEditIndex;
        //this.getEditableProducts();
    }

    protected void grdProducts_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        //int attributeGroupId;

        //MedinetBusinessEntries.ParticipantProductsInfo currentParticipant;

        //CheckBox chkNewProduct;
        //CheckBox chkModified;

        //CheckBoxList checkBoxList;

        //int productId = Convert.ToInt32(this.grdProducts.DataKeys[e.RowIndex].Values[0].ToString());
        //int pharmaFormId = Convert.ToInt32(this.grdProducts.DataKeys[e.RowIndex].Values[1].ToString());
        //int divisionId = Convert.ToInt32(this.grdProducts.DataKeys[e.RowIndex].Values[2].ToString());
        //int categoryId = Convert.ToInt32(this.grdProducts.DataKeys[e.RowIndex].Values[3].ToString());

        //chkNewProduct = (CheckBox)this.grdProducts.Rows[e.RowIndex].FindControl("NewProduct");
        //chkModified = (CheckBox)this.grdProducts.Rows[e.RowIndex].FindControl("ModifiedContent");
        //checkBoxList = (CheckBoxList)this.grdProducts.Rows[e.RowIndex].FindControl("cblModifiedAttributeGroups");

        //if (chkNewProduct.Checked == true || chkModified.Checked == false)
        //{
        //    currentParticipant = this.getParticipantProduct(this._editionId, divisionId, categoryId, productId, pharmaFormId);
        //    currentParticipant.ModifiedContent = chkModified.Checked;
        //    MedinetBusinessLogicComponent.ParticipantProductsBLC.Instance.modifiedContent(currentParticipant, this._userId, this._hashkey);
        //    this.addOrRemoveNewProduct(productId, pharmaFormId, divisionId, categoryId, chkNewProduct);

        //    List<MedinetBusinessEntries.ModifiedAttributeGroupsInfo> modifiedAttributeGroups =
        //        MedinetBusinessLogicComponent.ModifiedAttributeGroupsBLC.Instance.getModifiedAttributeGroupsByProduct(this._editionId, divisionId, categoryId, pharmaFormId, productId);

        //    if (modifiedAttributeGroups != null)
        //    {
        //        foreach (MedinetBusinessEntries.ModifiedAttributeGroupsInfo attributeGroupItem in modifiedAttributeGroups)
        //            MedinetBusinessLogicComponent.ModifiedAttributeGroupsBLC.Instance.removeProductToAttributeGroup(attributeGroupItem, this._userId, this._hashkey);
        //    }
        //}
        //else
        //{
        //    if (chkModified.Checked == true)
        //    {
        //        currentParticipant = this.getParticipantProduct(this._editionId, divisionId, categoryId, productId, pharmaFormId);
        //        currentParticipant.ModifiedContent = chkModified.Checked;
        //        MedinetBusinessLogicComponent.ParticipantProductsBLC.Instance.modifiedContent(currentParticipant, this._userId, this._hashkey);
        //        this.addOrRemoveNewProduct(productId, pharmaFormId, divisionId, categoryId, chkNewProduct);

        //        foreach (ListItem item in checkBoxList.Items)
        //        {
        //            attributeGroupId = Convert.ToInt32(item.Value);
        //            if (item.Selected)
        //            {
        //                MedinetBusinessEntries.ModifiedAttributeGroupsInfo modifiedAttributeGroup =
        //                    MedinetBusinessLogicComponent.ModifiedAttributeGroupsBLC.Instance.getModifiedAttributeGroup(
        //                        this.getModifiedAttributeGroup(this._editionId, divisionId, categoryId, productId, pharmaFormId, attributeGroupId));

        //                if (modifiedAttributeGroup == null)
        //                    MedinetBusinessLogicComponent.ModifiedAttributeGroupsBLC.Instance.addProductToAttributeGroup(
        //                        this.getModifiedAttributeGroup(this._editionId, divisionId, categoryId, productId, pharmaFormId, attributeGroupId), this._userId, this._hashkey);
        //            }
        //            else
        //            {
        //                MedinetBusinessEntries.ModifiedAttributeGroupsInfo modifiedAttributeGroup =
        //                    MedinetBusinessLogicComponent.ModifiedAttributeGroupsBLC.Instance.getModifiedAttributeGroup(
        //                        this.getModifiedAttributeGroup(this._editionId, divisionId, categoryId, productId, pharmaFormId, attributeGroupId));

        //                if (modifiedAttributeGroup != null)
        //                    MedinetBusinessLogicComponent.ModifiedAttributeGroupsBLC.Instance.removeProductToAttributeGroup(
        //                        this.getModifiedAttributeGroup(this._editionId, divisionId, categoryId, productId, pharmaFormId, attributeGroupId), this._userId, this._hashkey);
        //            }
        //        }
        //    }
        //}
        //this.grdProducts.EditIndex = -1;
        //this.getEditableProducts();
        //ScriptManager.RegisterStartupScript(this, typeof(Page), "messageScript", "jAlert('El registro se actualizó con éxito.', 'Modificación exitosa');", true);
    }
    /*-------------------------------------------------------------------------------------------------------------------------------------------*/
    /*-------------------------------------------------------------------------------------------------------------------------------------------*/
    protected void btnAddImage_Click(object sender, EventArgs e)
    {

    }










    /*-----------------------------------------------------------------------------------------------------------------------------------------------*/
    /*-----------------------------------------------------------------------------------------------------------------------------------------------*/
    protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.Session["CurrentPage"] = 0;
        if (this.ddlPageSize.SelectedValue != "-1")
        {
            this.grdProducts.SettingsPager.Mode = DevExpress.Web.ASPxGridView.GridViewPagerMode.ShowPager;
            this.grdProducts.SettingsPager.PageSize = Convert.ToInt32(this.ddlPageSize.SelectedValue);
        }
        else
        {
            this.grdProducts.SettingsPager.Mode = DevExpress.Web.ASPxGridView.GridViewPagerMode.ShowAllRecords;
        }
        
        this.getEditableProducts();
    }
    
    protected void imgBtnBackLabs_Click(object sender, ImageClickEventArgs e)
    {
        this.Session["divisionId"] = -1;
        Session["divisionName"] = this._divisionName;
        this.Response.Redirect("laboratories.aspx");
    }

    protected void btnEditDivsion_Click(object sender, EventArgs e)
    {
        ImageButton btn = (ImageButton)sender;
        GridViewDataRowTemplateContainer Row = (GridViewDataRowTemplateContainer)btn.NamingContainer;
        Session["divisionId"] = (int)grdProducts.GetRowValues(Row.VisibleIndex, "DivisionId");
        Session["divisionName"] =( (Label)grdProducts.FindRowCellTemplateControl(Row.VisibleIndex,null,"lblPharmaForm")).Text;
        Session["description"] = ((Label)grdProducts.FindRowCellTemplateControl(Row.VisibleIndex,null,"lblPharmaForm")).Text;
        //string jh = ((Label)row.FindControl("lblPharmaForm")).Text;
        this.Response.Redirect("divisionImages.aspx");
    }
 
    protected void btnSaveParticipant_Click(object sender, EventArgs e)
    {
        //int productId, pharmaFormId, divisionId, categoryId;

        //CheckBox chkNewProduct, chkModified;

        //MedinetBusinessEntries.ParticipantProductsInfo currentParticipant;

        //for (int gridRow = 0; gridRow < this.grdProducts.Rows.Count; gridRow++)
        //{
        //    productId = Convert.ToInt32(this.grdProducts.DataKeys[gridRow].Values[0].ToString());
        //    pharmaFormId = Convert.ToInt32(this.grdProducts.DataKeys[gridRow].Values[1].ToString());
        //    divisionId = Convert.ToInt32(this.grdProducts.DataKeys[gridRow].Values[2].ToString());
        //    categoryId = Convert.ToInt32(this.grdProducts.DataKeys[gridRow].Values[3].ToString());

        //    //Modified Content
        //    chkModified = (CheckBox)this.grdProducts.Rows[gridRow].FindControl("ModifiedContent");
        //    //New Product
        //    chkNewProduct = (CheckBox)this.grdProducts.Rows[gridRow].FindControl("NewProduct");

        //    if (divisionId > 0 && categoryId > 0)
        //    {
        //        currentParticipant = this.getParticipantProduct(this._editionId, divisionId, categoryId, productId, pharmaFormId);
        //        currentParticipant.ModifiedContent = chkModified.Checked;
        //        MedinetBusinessLogicComponent.ParticipantProductsBLC.Instance.modifiedContent(currentParticipant, this._userId, this._hashkey);
        //        this.addOrRemoveNewProduct(productId, pharmaFormId, divisionId, categoryId, chkNewProduct);

        //        if (chkNewProduct.Checked || !chkModified.Checked)
        //        {
        //            List<MedinetBusinessEntries.ModifiedAttributeGroupsInfo> modifiedAttributeGroups =
        //                MedinetBusinessLogicComponent.ModifiedAttributeGroupsBLC.Instance.getModifiedAttributeGroupsByProduct(this._editionId, divisionId, categoryId, pharmaFormId, productId);

        //            if (modifiedAttributeGroups != null)
        //            {
        //                foreach (MedinetBusinessEntries.ModifiedAttributeGroupsInfo attributeGroupItem in modifiedAttributeGroups)
        //                    MedinetBusinessLogicComponent.ModifiedAttributeGroupsBLC.Instance.removeProductToAttributeGroup(attributeGroupItem, this._userId, this._hashkey);
        //            }
        //        }
        //    }
        //}
        //this.getEditableProducts();
        //ScriptManager.RegisterStartupScript(this, typeof(Page), "messageScript", "jAlert('Los registros se guardaron con éxito.', 'Registros guardados');", true);
    }

    protected void imgBtnExport_Click(object sender, ImageClickEventArgs e)
    {
        exportPdf();
    }
    protected void imgBtnExportxls_Click(object sender, ImageClickEventArgs e)
    {

        exportXls();
    }
    #endregion

    #region Private Methods

    private void prepareGrid(short pagesize)
    {
        //if (pagesize > 0)
        //{
        //    this.grdProducts.AllowPaging = true;
        //    this.grdProducts.PageSize = pagesize;
        //    this.grdProducts.PageIndex = this.Session["CurrentPage"] == null ? 0 : Convert.ToInt32(this.Session["CurrentPage"]);
        //}
        //else
        //    this.grdProducts.AllowPaging = false;
    }

    private void displayCheckBoxList(GridViewRowEventArgs e, CheckBoxList cblModifiedAttributeGroups, int divisionId, int categoryId, int pharmaFormId, int productId)
    {
        //if (e.Row.RowIndex == this.grdProducts.EditIndex)
        //{
        //    MedinetBusinessEntries.ProductsToEditInfo currentProduct = (MedinetBusinessEntries.ProductsToEditInfo)e.Row.DataItem;

        //    if (currentProduct.ModifiedContent)
        //    {
        //        List<MedinetBusinessEntries.AttributeGroupDetailInfo> modifiedAttribute =
        //            MedinetBusinessLogicComponent.ModifiedAttributeGroupsBLC.Instance.getAttributeGroupsByProduct(this._bookId, this._countryId, this._editionId, divisionId, categoryId, pharmaFormId, productId);

        //        cblModifiedAttributeGroups.DataSource = MedinetBusinessLogicComponent.ModifiedAttributeGroupsBLC.Instance.getAttributeGroupsByProduct(this._bookId, this._countryId,
        //            this._editionId, divisionId, categoryId, pharmaFormId, productId);
        //        cblModifiedAttributeGroups.DataTextField = "AttributeGroupName";
        //        cblModifiedAttributeGroups.DataValueField = "AttributeGroupId";
        //        cblModifiedAttributeGroups.DataBind();

        //        foreach (MedinetBusinessEntries.AttributeGroupDetailInfo record in modifiedAttribute)
        //        {
        //            if (record.ModifiedAttributeGroup)
        //            {
        //                ListItem item = cblModifiedAttributeGroups.Items.FindByText(record.AttributeGroupName);

        //                if (cblModifiedAttributeGroups != null)
        //                    item.Selected = record.ModifiedAttributeGroup;
        //            }
        //        }
        //    }
        //}
    }

    private void getEditableProducts()
    {
        this.grdProducts.DataSource = MedinetBusinessLogicComponent.DivisionsBLC.Instance.getParticipantDivisions(this._countryId,this._editionId);
        this.grdProducts.DataBind();
       // getProductImages();b
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
        this._user = this.Session["user"] == null ? "" : this.Session["user"].ToString();
        this._roleId = this.Session["roleId"] == null ? -1 : Convert.ToInt32(this.Session["roleId"]);
        this._bookName = this.Session["bookName"] == null ? "" : this.Session["bookName"].ToString();
        this._editionName = this.Session["edition"] == null ? "" : (this.Session["edition"]).ToString();
        this._divisionName = this.Session["divisionName"] == null ? "" : this.Session["divisionName"].ToString();
        this._bookShortName = this.Session["bookShortName"] == null ? "" : this.Session["bookShortName"].ToString();
      
        getEditableProducts();
    }
    private MedinetBusinessEntries.ParticipantProductsInfo getParticipantProduct(int editionId, int divisionId, int categoryId, int productId, int pharmaFormId)
    {
        MedinetBusinessEntries.ParticipantProductsInfo participantProduct = new MedinetBusinessEntries.ParticipantProductsInfo();
        participantProduct.EditionId = editionId;
        participantProduct.DivisionId = divisionId;
        participantProduct.CategoryId = categoryId;
        participantProduct.ProductId = productId;
        participantProduct.PharmaFormId = pharmaFormId;

        return participantProduct;
    }

    private MedinetBusinessEntries.ModifiedAttributeGroupsInfo getModifiedAttributeGroup(int editionId, int divisionId, int categoryId, int productId, int pharmaFormId, int attributeGroupId)
    {
        MedinetBusinessEntries.ModifiedAttributeGroupsInfo modifiedAttributeGroup = new MedinetBusinessEntries.ModifiedAttributeGroupsInfo();
        modifiedAttributeGroup.EditionId = editionId;
        modifiedAttributeGroup.DivisionId = divisionId;
        modifiedAttributeGroup.CategoryId = categoryId;
        modifiedAttributeGroup.ProductId = productId;
        modifiedAttributeGroup.PharmaFormId = pharmaFormId;
        modifiedAttributeGroup.AttributeGroupId = attributeGroupId;

        return modifiedAttributeGroup;
    }

    private void addOrRemoveNewProduct(int productId, int pharmaFormId, int divisionId, int categoryId, CheckBox chkNewProduct)
    {
        MedinetBusinessEntries.NewProductInfo currentNewProduct =
            MedinetBusinessLogicComponent.NewProductsBLC.Instance.getParticipantProduct(productId, this._editionId, pharmaFormId, divisionId, categoryId);

        if (currentNewProduct == null)
        {
            if (chkNewProduct.Checked == true)
            {
                currentNewProduct = new MedinetBusinessEntries.NewProductInfo();
                currentNewProduct.EditionId = this._editionId;
                currentNewProduct.DivisionId = divisionId;
                currentNewProduct.CategoryId = categoryId;
                currentNewProduct.PharmaFormId = pharmaFormId;
                currentNewProduct.ProductId = productId;
                MedinetBusinessLogicComponent.NewProductsBLC.Instance.addNew(currentNewProduct, this._userId, this._hashkey);
            }
        }
        else
            if (chkNewProduct.Checked == false)
                MedinetBusinessLogicComponent.NewProductsBLC.Instance.removeNew(currentNewProduct, this._userId, this._hashkey);
    }

    private List<MedinetBusinessEntries.PresentationDetailInfo> showPresentations(int editionId, int divisionId, int categoryId, int productId, int pharmaFormId)
    {

        List<MedinetBusinessEntries.PresentationDetailInfo> finalList =
                MedinetBusinessLogicComponent.PresentationsBLC.Instance.getPresentationsByProduct(editionId, divisionId, categoryId, productId, pharmaFormId);
        List<MedinetBusinessEntries.PresentationDetailInfo> presentationList =
           MedinetBusinessLogicComponent.PresentationsBLC.Instance.getPresentationsByEditionByProduct(editionId, divisionId, categoryId, productId, pharmaFormId);

        finalList.AddRange(presentationList);
        foreach (MedinetBusinessEntries.PresentationDetailInfo item in finalList)
        {
            if (item.Editions.Length < 1)
            {
                item.Editions = "Sin asociar";
            }
        }

        return finalList;
    }

    private void exportPdf()
    {
        DevExpress.XtraPrinting.PdfExportOptions pdfExport = new DevExpress.XtraPrinting.PdfExportOptions();
        pdfExport.DocumentOptions.Title = "Participant Products";
        pdfExport.DocumentOptions.Author = "Medinet 2.0";
        _date = DateTime.Now.ToString();
        List<string> titles = new List<string>();
        titles.Add("Listado de Productos Participantes");
        titles.Add("Obra : " + _bookName);
        titles.Add("Edición : " + _editionName);
        titles.Add("Laboratorio: " + _divisionName);
        titles.Add("Usuario: " + MedinetBusinessLogicComponent.FilesBLC.Instance.getUserName(_userId));
        titles.Add("Fecha: " + _date);

        System.Data.DataTable dtProds = new System.Data.DataTable();
        dtProds.Columns.Add("Categoria");
        dtProds.Columns.Add("Producto");
        dtProds.Columns.Add("Forma farmaceutica");
        dtProds.Columns.Add("Descripción");
        List<MedinetBusinessEntries.ProductsToEditInfo> products = MedinetBusinessLogicComponent.ProductsBLC.Instance.getParticipantProductsByDivision(this._divisionId, this._countryId, this._editionId, this._bookId);
        foreach (MedinetBusinessEntries.ProductsToEditInfo prod in products)
        {
            dtProds.Rows.Add(prod.CategoryName, prod.Brand, prod.PharmaForm, prod.ProductDescription);
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
        titles.Add("Laboratorio: " + _divisionName);
        titles.Add("Usuario: " + MedinetBusinessLogicComponent.FilesBLC.Instance.getUserName(_userId));
        titles.Add("Fecha: " + _date);


        System.Data.DataTable dtProds = new System.Data.DataTable();
        dtProds.Columns.Add("Categoria");
        dtProds.Columns.Add("Producto");
        dtProds.Columns.Add("Forma farmaceutica");
        dtProds.Columns.Add("Tipo de producto");
        List<MedinetBusinessEntries.ProductsToEditInfo> products;
        if (this._bookShortName.Equals(System.Configuration.ConfigurationManager.AppSettings["Symptoms"]))
        {
            dtProds.Columns.Add("Síntoma del producto");
            products = MedinetBusinessLogicComponent.ProductsBLC.Instance.getParticipantProductsByDivisionSymptoms(this._divisionId, this._countryId, this._editionId, this._bookId);
        }
        else
        {
            products = MedinetBusinessLogicComponent.ProductsBLC.Instance.getParticipantProductsByDivision(this._divisionId, this._countryId, this._editionId, this._bookId);
        }
        dtProds.Columns.Add("Descripción");
        dtProds.Columns.Add("Nuevo");
        dtProds.Columns.Add("Con Cambios");
        dtProds.Columns.Add("ProductShot");
        dtProds.Columns.Add("Ejecutivo(s)");
        foreach (MedinetBusinessEntries.ProductsToEditInfo prod in products)
        {
            if (this._bookShortName.Equals(System.Configuration.ConfigurationManager.AppSettings["Symptoms"]))
            {
                dtProds.Rows.Add(prod.CategoryName, prod.Brand, prod.PharmaForm, prod.ProductType, prod.Symptoms, prod.ProductDescription, prod.NewProduct ? "Si" : "No", prod.ModifiedContent ? "Si" : "No", prod.Sidef ? "Si" : "No", MedinetBusinessLogicComponent.FilesBLC.Instance.getUserAgentByDivisionByEditionByAction(this._editionId, Convert.ToInt32(prod.DivisionId), Convert.ToInt32(prod.CategoryId), Convert.ToInt32(prod.PharmaFormId), prod.ProductId));
            }
            else
            {
                dtProds.Rows.Add(prod.CategoryName, prod.Brand, prod.PharmaForm, prod.ProductType, prod.ProductDescription, prod.NewProduct ? "Si" : "No", prod.ModifiedContent ? "Si" : "No", prod.Sidef ? "Si" : "No", MedinetBusinessLogicComponent.FilesBLC.Instance.getUserAgentByDivisionByEditionByAction(this._editionId, Convert.ToInt32(prod.DivisionId), Convert.ToInt32(prod.CategoryId), Convert.ToInt32(prod.PharmaFormId), prod.ProductId));
            }

        }
        MedinetBusinessEntries.VersionFileInfo VersionFile = MedinetBusinessLogicComponent.FilesBLC.Instance.getVersionFile(getPrefixApplication(), _editionId);
        string route = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["BaseURL"]);
        string fileName = "ProductosParticipantes_" + getPrefixApplication() + "_" + VersionFile.CurrentValue + ".xls";
        string filePath = route + @"\" + fileName;
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
    private string getPrefixApplication()
    {
        switch (this._roleId)
        {
            case (int)Utilities.Roles.Diagramador:
                return "MPL";
            case (int)Utilities.Roles.Administrador:
                return "MA";

            default:
                return "MA";
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
    private string _user;
    private int _roleId;
    private string _bookName;
    private string _editionName;
    private string _divisionName;
    private string _date;
    private string _bookShortName;
    private PLMCryptographyComponent.CryptographyComponent _cryptography = new PLMCryptographyComponent.CryptographyComponent();
}