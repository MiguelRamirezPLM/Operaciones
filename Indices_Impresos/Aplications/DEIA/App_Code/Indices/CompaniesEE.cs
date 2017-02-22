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

public class CompaniesEE : GenerateHTML
{
    #region Constructors

    public CompaniesEE(string sourcePath, string destinationPath) : base(sourcePath, destinationPath){}

    #endregion

    #region Public Methods

    public override void getHtmlFiles(int editionId)
    {
        throw new Exception("The method or operation is not implemented.");
    }

    public void getCompaniesFiles(int editionId, int sectionId)
    {
        //Get Specialities from Database:
        DEIATableAdapters.SectionsTableAdapter sectionAdp = new DEIATableAdapters.SectionsTableAdapter();
        DEIA.SectionsDataTable sectionsTable = sectionAdp.getByParent(editionId, sectionId);

        foreach (DataRow sectRow in sectionsTable.Rows)
        {
            DEIA.SectionsRow sectionRow = (DEIA.SectionsRow)sectRow;

            //Get Companies from Database:
            DEIATableAdapters.CompanyIndexesTableAdapter compIndAdp = new DEIATableAdapters.CompanyIndexesTableAdapter();
            DEIA.CompanyIndexesDataTable compIndTable = compIndAdp.getCompaniesBySection(editionId, sectionRow.SectionId);

            foreach (DataRow compIndRow in compIndTable.Rows)
            {
                DEIA.CompanyIndexesRow companyIndRow = (DEIA.CompanyIndexesRow)compIndRow;

                //Get Company from Database:
                DEIATableAdapters.CompaniesTableAdapter compAdp = new DEIATableAdapters.CompaniesTableAdapter();
                DEIA.CompaniesDataTable compTable = compAdp.getCompany(editionId, sectionRow.SectionId, companyIndRow.CompanyId);

                // Get the html file template to save the correspond letter html file:
                StringBuilder sbHtmlTemplate = new StringBuilder(this.getHtmlTemplate());

                foreach (DataRow compRow in compTable.Rows)
                {
                    DEIA.CompaniesRow companyRow = (DEIA.CompaniesRow)compRow;

                    sbHtmlTemplate.Append(this.getCompanyData(companyRow));

                    sbHtmlTemplate.Append("\r\n</td></tr></table>");

                    sbHtmlTemplate.Append("\r\n</div></body></html>");

                }

                // Save the htm file:
                this.saveHtmlFile(companyIndRow.CompanyId.ToString() + ".htm", sbHtmlTemplate.ToString());
            }
        }

    }

    #endregion

    #region Protected Methods

    protected override string getLink(string href, string label)
    {
        throw new Exception("The method or operation is not implemented.");
    }

    protected string getCompanyData(DEIA.CompaniesRow compRow)
    {
        StringBuilder sb = new StringBuilder();

        sb.Append("\r\n<p class=\"Company\">@@@Empresa@@@</p>");
        sb.Append("\r\n<p class=\"Direccion\">@@@Direccion@@@<br />");
        sb.Append("\r\n@@@Colonia@@@<br />");
        sb.Append("\r\nCP. @@@CP@@@<br />");

        sb.Replace("@@@Empresa@@@", this.replaceAccentsToHtmlCodes(compRow.CompanyName));
        sb.Replace("@@@Direccion@@@", this.replaceAccentsToHtmlCodes(compRow.Address));
        sb.Replace("@@@Colonia@@@", this.replaceAccentsToHtmlCodes(compRow.Suburb));
        sb.Replace("@@@CP@@@", compRow.ZIPCODE.ToString());

        if (!string.IsNullOrEmpty(compRow.UBICATION))
        {
            sb.Append("\r\n@@@Ubicacion@@@<br>");

            sb.Replace("@@@Ubicacion@@@", this.replaceAccentsToHtmlCodes(compRow.UBICATION));
        }

        if (!string.IsNullOrEmpty(compRow.PHONE))
        {
            sb.Append("\r\n@@@Telefono@@@<br>");

            sb.Replace("@@@Telefono@@@", this.replaceAccentsToHtmlCodes(compRow.PHONE));
        }

        if (!string.IsNullOrEmpty(compRow.FAX))
        {
            sb.Append("\r\n@@@Fax@@@<br>");

            sb.Replace("@@@Fax@@@", this.replaceAccentsToHtmlCodes(compRow.FAX));
        }

        if (!string.IsNullOrEmpty(compRow.OXIDOM))
        {
            sb.Append("\r\n@@@Oxidom@@@<br>");

            sb.Replace("@@@Oxidom@@@", this.replaceAccentsToHtmlCodes(compRow.OXIDOM));
        }

        if (!string.IsNullOrEmpty(compRow.LADA))
        {
            sb.Append("\r\n@@@Lada@@@<br>");

            sb.Replace("@@@Lada@@@", this.replaceAccentsToHtmlCodes(compRow.LADA));
        }

        if (!string.IsNullOrEmpty(compRow.EMAIL))
        {
            if (!compRow.EMAIL.Contains(";") && !compRow.EMAIL.Contains(",") && !compRow.EMAIL.Contains(":"))
            {
                sb.Append("\r\nemail: <a href=\"mailto:@@@email@@@\">@@@email@@@</a><br>");

                sb.Replace("@@@email@@@", compRow.EMAIL);
            }
            else
            {
                sb.Append("\r\nemail: " + setEmail(compRow.EMAIL.ToString()) + "<br>");
            }
        }

        if (!string.IsNullOrEmpty(compRow.WEB))
        {
            if (!compRow.WEB.Contains(";") && !compRow.WEB.Contains(",") && !compRow.WEB.Contains(":"))
            {
                sb.Append("\r\n<a href=\"http://@@@PaginaWeb@@@\" target=\"_blank\">@@@PaginaWeb@@@</a><br>");

                sb.Replace("@@@PaginaWeb@@@", compRow.WEB);
            }
            else
            {
                sb.Append("\r\n" + setWeb(compRow.WEB.ToString()) + "<br>");
            }
        }

        sb.Append("\r\n</p>");

        if (!string.IsNullOrEmpty(compRow.GIRO))
        {
            sb.Append("\r\n<p class=\"Giro\"><b>Giro Comercial</b></p>");

            sb.Append("\r\n<p class=\"DescripcionGiro\">" + this.replaceAccentsToHtmlCodes(compRow.GIRO) + "</p>");
                       
        }
        
        return sb.ToString();
    }

    #endregion

    #region Private Methods

    private string setEmail(string cadena)
    {
        string[] arr = cadena.Split(',', ';', ':');

        for (int x = 0; x < arr.Length; x++)
        {
            if (arr[x].Contains("@"))
            {
                cadena = cadena.Replace(arr[x], "<a href=\"mailto:" + arr[x].Trim() + "\">" + arr[x].Trim() + "</a>");
            }

        }

        return cadena;
    }

    private string setWeb(string cadena)
    {
        string[] arr = cadena.Split(',', ';', ':');

        for (int x = 0; x < arr.Length; x++)
        {
            if (arr[x].Contains("www."))
            {
                cadena = cadena.Replace(arr[x], "<a href=\"http://:" + arr[x].Trim() + "\" target=\"_blank\" >" + arr[x].Trim() + "</a>");
            }

        }

        return cadena;

    }

    #endregion
}
