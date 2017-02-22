using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;
using System.IO;

public class Sections : GenerateHTML
{
    #region Constructors

    public Sections(string sourcePath, string destinationPath) : base(sourcePath, destinationPath){ }

    #endregion

    #region Public Methods

    public override void getHtmlFiles(int editionId)
    {
        throw new Exception("The method or operation is not implemented.");
    }

    public void getHtmlFilesSections(int editionId, int sectionId, string title)
    {
        //Get Specialities from Database:
        DEIATableAdapters.SectionsTableAdapter sectionAdp = new DEIATableAdapters.SectionsTableAdapter();
        DEIA.SectionsDataTable sectionsTable = sectionAdp.getByParentPr(editionId, sectionId);

        foreach (DataRow sectRow in sectionsTable.Rows)
        {
            DEIA.SectionsRow sectionRow = (DEIA.SectionsRow)sectRow;

            // Get the html file template to save the correspond letter html file:
            StringBuilder sbHtmlTemplate = new StringBuilder(this.getHtmlTemplate());
            sbHtmlTemplate.Replace("@@@Seccion@@@", this.replaceAccentsToHtmlCodes(title.ToUpper()));
            sbHtmlTemplate.Replace("@@@SubSeccion@@@", this.replaceAccentsToHtmlCodes(sectionRow.SectionName.ToUpper()));

            //Get Products from Database:
            DEIATableAdapters.ProductsTableAdapter prodsAdp = new DEIATableAdapters.ProductsTableAdapter();
            DEIA.ProductsDataTable prodsTable = prodsAdp.getProductsBySection(editionId, sectionRow.SectionId);

            foreach (DataRow prodRow in prodsTable.Rows)
            {
                DEIA.ProductsRow productRow = (DEIA.ProductsRow)prodRow;

                sbHtmlTemplate.Append(this.productLink(productRow));

            }

            sbHtmlTemplate.Append("\r\n</div></body></html>");

            // Save the htm file:
            this.saveHtmlFile(sectionRow.SectionId + ".htm", sbHtmlTemplate.ToString());
        
        }

    }

    public void getHtmlFilesSubSections(int editionId, int sectionId, string title)
    {
        //Get Specialities from Database:
        DEIATableAdapters.SectionsTableAdapter sectionAdp = new DEIATableAdapters.SectionsTableAdapter();
        DEIA.SectionsDataTable sectionsTable = sectionAdp.getSubSections(editionId, sectionId);

        foreach (DataRow sectRow in sectionsTable.Rows)
        {
            DEIA.SectionsRow sectionRow = (DEIA.SectionsRow)sectRow;

            // Get the html file template to save the correspond letter html file:
            StringBuilder sbHtmlTemplate = new StringBuilder(this.getHtmlTemplate());
            sbHtmlTemplate.Replace("@@@Seccion@@@", this.replaceAccentsToHtmlCodes(title.ToUpper()));
            sbHtmlTemplate.Replace("@@@SubSeccion@@@", this.replaceAccentsToHtmlCodes(sectionRow.SectionName.ToUpper()));

            //Get Products from Database:
            DEIATableAdapters.ProductsTableAdapter prodsAdp = new DEIATableAdapters.ProductsTableAdapter();
            DEIA.ProductsDataTable prodsTable = prodsAdp.getProductsBySection(editionId, sectionRow.SectionId);

            foreach (DataRow prodRow in prodsTable.Rows)
            {
                DEIA.ProductsRow productRow = (DEIA.ProductsRow)prodRow;

                sbHtmlTemplate.Append(this.productLink(productRow));

            }

            sbHtmlTemplate.Append("\r\n</div></body></html>");

            // Save the htm file:
            this.saveHtmlFile(sectionRow.SectionId + ".htm", sbHtmlTemplate.ToString());

        }
    }

    public void getHtmlFilesSectionsEE(int editionId, int sectionId, string title)
    {
        //Get Specialities from Database:
        DEIATableAdapters.SectionsTableAdapter sectionAdp = new DEIATableAdapters.SectionsTableAdapter();
        DEIA.SectionsDataTable sectionsTable = sectionAdp.getByParent(editionId, sectionId);

        foreach (DataRow sectRow in sectionsTable.Rows)
        {
            DEIA.SectionsRow sectionRow = (DEIA.SectionsRow)sectRow;

            // Get the html file template to save the correspond letter html file:
            StringBuilder sbHtmlTemplate = new StringBuilder(this.getHtmlTemplate());
            sbHtmlTemplate.Replace("@@@Seccion@@@", this.replaceAccentsToHtmlCodes(title.ToUpper()));
            sbHtmlTemplate.Replace("@@@SubSeccion@@@", this.replaceAccentsToHtmlCodes(sectionRow.SectionName.ToUpper()));

            //Get Products from Database:
            DEIATableAdapters.CompanyIndexesTableAdapter compAdp = new DEIATableAdapters.CompanyIndexesTableAdapter();
            DEIA.CompanyIndexesDataTable compTable = compAdp.getCompaniesBySection(editionId, sectionRow.SectionId);

            foreach (DataRow compRow in compTable.Rows)
            {
                DEIA.CompanyIndexesRow companyRow = (DEIA.CompanyIndexesRow)compRow;

                sbHtmlTemplate.Append(this.companyLink(companyRow));

            }

            sbHtmlTemplate.Append("\r\n</div></body></html>");

            // Save the htm file:
            this.saveHtmlFile(sectionRow.SectionId + ".htm", sbHtmlTemplate.ToString());

        }

    }

    #endregion

    #region Protected Methods

    protected override string getLink(string href, string label)
    {
        throw new Exception("The method or operation is not implemented.");
    }

    protected string productLink(DEIA.ProductsRow prodRow)
    {
        StringBuilder sb = new StringBuilder();

        if (!string.IsNullOrEmpty(prodRow.HtmlFile))
        {
            sb.Append("\r\n<p class=\"prodLink\"><a class=\"linksSections\" href=\"../productos/" + prodRow.HtmlFile + "\" style='text-decoration:none'><b>* " +
                this.replaceAccentsToHtmlCodes(prodRow.ProductName) + "</b>. " +
                this.replaceAccentsToHtmlCodes(prodRow.Description) + " <b>" +
                this.replaceAccentsToHtmlCodes(prodRow.CompanyShortName) + ".</b></a></p><br />");
        }
        else
        {
            sb.Append("\r\n<p class=\"prodNoLink\"><b>" + this.replaceAccentsToHtmlCodes(prodRow.ProductName) + "</b>. " +
                this.replaceAccentsToHtmlCodes(prodRow.Description) + " <b>" + prodRow.CompanyShortName + ".</b></p><br />");
        }

        return sb.ToString();
    }

    protected string companyLink(DEIA.CompanyIndexesRow companyRow)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("\r\n<p class=\"Companies\"><a href=\"../labs/" + companyRow.CompanyId + ".htm\" style='text-decoration:none'><b>* " +
            this.replaceAccentsToHtmlCodes(companyRow.CompanyName) + ".</b></a></p><br />");


        return sb.ToString();
    }

    #endregion

}
