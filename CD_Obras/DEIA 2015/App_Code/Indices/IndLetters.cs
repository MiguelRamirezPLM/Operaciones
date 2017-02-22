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

public class IndLetters : GenerateHTML
{
    #region Constructors

    public IndLetters(string sourcePath, string destinationPath) : base(sourcePath, destinationPath) { }

    #endregion

    #region Public Methods

    public override void getHtmlFiles(int editionId)
    {
        throw new Exception("The method or operation is not implemented.");
    }

    public void getHtmlFilesInd(int editionId, int sectionId, string title)
    {
        //Get Specialities from Database:
        DEIATableAdapters.SectionsTableAdapter sectionAdp = new DEIATableAdapters.SectionsTableAdapter();
        DEIA.SectionsDataTable sectionsTable = sectionAdp.getByParentPr(editionId, sectionId);

        // Get the html file template to save the correspond letter html file:
        StringBuilder sbHtmlTemplate = new StringBuilder(this.getHtmlTemplate());
        sbHtmlTemplate.Replace("@@@TituloIndice@@@", this.replaceAccentsToHtmlCodes(title.ToUpper()));

        foreach (DataRow sectRow in sectionsTable.Rows)
        {
            DEIA.SectionsRow sectionRow = (DEIA.SectionsRow)sectRow;

            sbHtmlTemplate.Append(this.sectionLink(this.replaceAccentsToHtmlCodes(sectionRow.SectionName.ToUpper())));
            
            //Get Products from Database:
            DEIATableAdapters.ProductsTableAdapter prodsAdp = new DEIATableAdapters.ProductsTableAdapter();
            DEIA.ProductsDataTable prodsTable = prodsAdp.getProductsBySection(editionId, sectionRow.SectionId);

            foreach (DataRow prodRow in prodsTable.Rows)
            {
                DEIA.ProductsRow productRow = (DEIA.ProductsRow)prodRow;

                sbHtmlTemplate.Append(this.productLink(productRow));
                
            }

            sbHtmlTemplate.Append("\r\n</blockquote>");
        
        }

        sbHtmlTemplate.Append("\r\n</td></tr></table>");

        sbHtmlTemplate.Append("\r\n</div></body></html>");

        // Save the htm file:
        this.saveHtmlFile("indice_" + this.quitAccents(title.ToLower().Replace(" ","")) + ".htm", sbHtmlTemplate.ToString());
    }

    public void getHtmlFilesEE(int editionId, int sectionId, string title)
    {
        //Get Specialities from Database:
        DEIATableAdapters.SectionsTableAdapter sectionAdp = new DEIATableAdapters.SectionsTableAdapter();
        DEIA.SectionsDataTable sectionsTable = sectionAdp.getByParent(editionId, sectionId);

        // Get the html file template to save the correspond letter html file:
        StringBuilder sbHtmlTemplate = new StringBuilder(this.getHtmlTemplate());
        sbHtmlTemplate.Replace("@@@TituloIndice@@@", this.replaceAccentsToHtmlCodes(title.ToUpper()));

        foreach (DataRow sectRow in sectionsTable.Rows)
        {
            DEIA.SectionsRow sectionRow = (DEIA.SectionsRow)sectRow;

            sbHtmlTemplate.Append(this.sectionLink(this.replaceAccentsToHtmlCodes(sectionRow.SectionName.ToUpper())));

            //Get Products from Database:
            DEIATableAdapters.CompanyIndexesTableAdapter compAdp = new DEIATableAdapters.CompanyIndexesTableAdapter();
            DEIA.CompanyIndexesDataTable compTable = compAdp.getCompaniesBySection(editionId, sectionRow.SectionId);

            foreach (DataRow compRow in compTable.Rows)
            {
                DEIA.CompanyIndexesRow companyRow = (DEIA.CompanyIndexesRow)compRow;

                sbHtmlTemplate.Append(this.companyLink(companyRow));

            }

            sbHtmlTemplate.Append("\r\n</blockquote>");

        }

        sbHtmlTemplate.Append("\r\n</td></tr></table>");

        sbHtmlTemplate.Append("\r\n</div></body></html>");

        // Save the htm file:
        this.saveHtmlFile("indice_" + this.quitAccents(title.ToLower().Replace(" ", "")) + ".htm", sbHtmlTemplate.ToString());
    }

    public void getHtmlFilesINC(int editionId, byte indexId, string title)
    {
        //Get all letter from database:
        DEIATableAdapters.AlphabetTableAdapter alphabetAdp = new DEIATableAdapters.AlphabetTableAdapter();
        DEIA.AlphabetDataTable alphabetTable = alphabetAdp.getAlphabet();

        foreach (DataRow aRow in alphabetTable.Rows)
        {
            DEIA.AlphabetRow alphabetRow = (DEIA.AlphabetRow)aRow;

            //Get Products from Database:
            DEIATableAdapters.ProductsTableAdapter prodsAdp = new DEIATableAdapters.ProductsTableAdapter();

            DEIA.ProductsDataTable prodsTable = prodsAdp.getProductsByName(editionId, indexId, alphabetRow.Letter);

            // Get the html file template to save the correspond letter html file:
            StringBuilder sbHtmlTemplate = new StringBuilder(this.getHtmlTemplate());
            sbHtmlTemplate.Replace("@@@TituloIndice@@@", this.replaceAccentsToHtmlCodes(title.ToUpper()));

            foreach (DataRow prodRow in prodsTable.Rows)
            {
                DEIA.ProductsRow productRow = (DEIA.ProductsRow)prodRow;

                sbHtmlTemplate.Append(this.productLink(productRow));
                
            }

            sbHtmlTemplate.Append("\r\n</td></tr></table>");

            sbHtmlTemplate.Append("\r\n</div></body></html>");

            // Save the htm file:
            this.saveHtmlFile(alphabetRow.Letter + "-gral.htm", sbHtmlTemplate.ToString());
        }

    }

    public void getHtmlFilesMP(int editionId, int sectionId, string title)
    {
        //Get all letter from database:
        DEIATableAdapters.AlphabetTableAdapter alphabetAdp = new DEIATableAdapters.AlphabetTableAdapter();
        DEIA.AlphabetDataTable alphabetTable = alphabetAdp.getAlphabet();

        foreach (DataRow aRow in alphabetTable.Rows)
        {
            DEIA.AlphabetRow alphabetRow = (DEIA.AlphabetRow)aRow;

            //Get Specialities from Database:
            DEIATableAdapters.SectionsTableAdapter sectionAdp = new DEIATableAdapters.SectionsTableAdapter();
            DEIA.SectionsDataTable sectionsTable = sectionAdp.getByParentByLetterPr(editionId, sectionId, alphabetRow.Letter);

            // Get the html file template to save the correspond letter html file:
            StringBuilder sbHtmlTemplate = new StringBuilder(this.getHtmlTemplate());
            sbHtmlTemplate.Replace("@@@TituloIndice@@@", this.replaceAccentsToHtmlCodes(title.ToUpper()));

            foreach (DataRow sectRow in sectionsTable.Rows)
            {
                DEIA.SectionsRow sectionRow = (DEIA.SectionsRow)sectRow;

                DEIATableAdapters.SectionsTableAdapter subSections = new DEIATableAdapters.SectionsTableAdapter();
                DEIA.SectionsDataTable subSectTable = subSections.getByParentPr(editionId, sectionRow.SectionId);

                if (subSectTable.Rows.Count == 0)
                {
                    sbHtmlTemplate.Append(this.sectionLink(this.replaceAccentsToHtmlCodes(sectionRow.SectionName.ToUpper())));

                    //Get Products from Database:
                    DEIATableAdapters.ProductsTableAdapter prodsAdp = new DEIATableAdapters.ProductsTableAdapter();
                    DEIA.ProductsDataTable prodsTable = prodsAdp.getProductsBySection(editionId, sectionRow.SectionId);

                    foreach (DataRow prodRow in prodsTable.Rows)
                    {
                        DEIA.ProductsRow productRow = (DEIA.ProductsRow)prodRow;

                        sbHtmlTemplate.Append(this.productLink(productRow));
                       
                    }

                    sbHtmlTemplate.Append("\r\n</blockquote>");
                }
                else
                {
                    foreach (DataRow subSectRow in subSectTable.Rows)
                    {
                        DEIA.SectionsRow subSectionRow = (DEIA.SectionsRow)subSectRow;

                        sbHtmlTemplate.Append(this.sectionLink(this.replaceAccentsToHtmlCodes(sectionRow.SectionName.ToUpper() + " - " + subSectionRow.SectionName.ToUpper())));

                        //Get Products from Database:
                        DEIATableAdapters.ProductsTableAdapter prodsAdp = new DEIATableAdapters.ProductsTableAdapter();
                        DEIA.ProductsDataTable prodsTable = prodsAdp.getProductsBySection(editionId, subSectionRow.SectionId);

                        foreach (DataRow prodRow in prodsTable.Rows)
                        {
                            DEIA.ProductsRow productRow = (DEIA.ProductsRow)prodRow;

                            sbHtmlTemplate.Append(this.productLink(productRow));

                        }

                        sbHtmlTemplate.Append("\r\n</blockquote>");

                    }
                }

            }

            sbHtmlTemplate.Append("\r\n</td></tr></table>");

            sbHtmlTemplate.Append("\r\n</div></body></html>");

            // Save the htm file:
            this.saveHtmlFile(alphabetRow.Letter + "-mp.htm", sbHtmlTemplate.ToString());

        }

    }

    public void getHtmlFilesCC(int editionId, int sectionId, string title)
    {
        //Get Specialities from Database:
        DEIATableAdapters.SectionsTableAdapter sectionAdp = new DEIATableAdapters.SectionsTableAdapter();
        DEIA.SectionsDataTable sectionsTable = sectionAdp.getSubSections(editionId, sectionId);

        // Get the html file template to save the correspond letter html file:
        StringBuilder sbHtmlTemplate = new StringBuilder(this.getHtmlTemplate());
        sbHtmlTemplate.Replace("@@@TituloIndice@@@", this.replaceAccentsToHtmlCodes(title.ToUpper()));

        foreach (DataRow sectRow in sectionsTable.Rows)
        {
            DEIA.SectionsRow sectionRow = (DEIA.SectionsRow)sectRow;

            sbHtmlTemplate.Append(this.sectionLink(this.replaceAccentsToHtmlCodes(sectionRow.SectionName.ToUpper())));

            //Get Products from Database:
            DEIATableAdapters.ProductsTableAdapter prodsAdp = new DEIATableAdapters.ProductsTableAdapter();
            DEIA.ProductsDataTable prodsTable = prodsAdp.getProductsBySection(editionId, sectionRow.SectionId);

            foreach (DataRow prodRow in prodsTable.Rows)
            {
                DEIA.ProductsRow productRow = (DEIA.ProductsRow)prodRow;

                sbHtmlTemplate.Append(this.productLink(productRow));

            }

            sbHtmlTemplate.Append("\r\n</blockquote>");

        }

        sbHtmlTemplate.Append("\r\n</td></tr></table>");

        sbHtmlTemplate.Append("\r\n</div></body></html>");

        // Save the htm file:
        this.saveHtmlFile("indice_" + this.quitAccents(title.ToLower().Replace(" ", "")) + ".htm", sbHtmlTemplate.ToString());
    }

    public void getHtmlFilesDPP(int editionId, int sectionId, string title)
    {
        //Get all letter from database:
       // DEIATableAdapters.AlphabetTableAdapter alphabetAdp = new DEIATableAdapters.AlphabetTableAdapter();
       // DEIA.AlphabetDataTable alphabetTable = alphabetAdp.getAlphabet();
        DEIATableAdapters.AlphabetCTableAdapter alphabetAdp  = new DEIATableAdapters.AlphabetCTableAdapter();
        DEIA.AlphabetCDataTable alphabetTable = alphabetAdp.getAlphabetCompanies(editionId);

        foreach (DataRow aRow in alphabetTable.Rows)
        {
            //DEIA.AlphabetRow alphabetRow = (DEIA.AlphabetRow)aRow;
            DEIA.AlphabetCRow alphabetRow = (DEIA.AlphabetCRow)aRow;

            DEIATableAdapters.CompanyIndexesTableAdapter compAdp = new DEIATableAdapters.CompanyIndexesTableAdapter();
           // DEIA.CompanyIndexesDataTable compTable = compAdp.getCompaniesByLetter(editionId, sectionId, alphabetRow.Letter);
            DEIA.CompanyIndexesDataTable compTable = compAdp.getCompaniesByLetter(editionId, alphabetRow.Letter);

            // Get the html file template to save the correspond letter html file:
            StringBuilder sbHtmlTemplate = new StringBuilder(this.getHtmlTemplate());
            sbHtmlTemplate.Replace("@@@TituloIndice@@@", this.replaceAccentsToHtmlCodes(title.ToUpper()));

            foreach (DataRow compRow in compTable.Rows)
            {
                DEIA.CompanyIndexesRow companyRow = (DEIA.CompanyIndexesRow)compRow;

                sbHtmlTemplate.Append(this.tercerasLink(companyRow));

            }

            sbHtmlTemplate.Append("\r\n</table>");

            sbHtmlTemplate.Append("</div></body></html>");

            // Save the htm file:
            this.saveHtmlFile(alphabetRow.Letter + "-terc.htm", sbHtmlTemplate.ToString());

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
                this.replaceAccentsToHtmlCodes(prodRow.CompanyShortName) + ".</b></a></p><br/>");
        }
        else
        {
            sb.Append("\r\n<p class=\"prodNoLink\"><b>" + this.replaceAccentsToHtmlCodes(prodRow.ProductName) + "</b>. " + 
                this.replaceAccentsToHtmlCodes(prodRow.Description) + " <b>" + this.replaceAccentsToHtmlCodes(prodRow.CompanyShortName) + ".</b></p><br />");
        }

        return sb.ToString();
    }

    protected string sectionLink(string section)
    {
        StringBuilder sb = new StringBuilder();

        sb.Append("\r\n<p class=\"Materia\">" + this.replaceAccentsToHtmlCodes(section) + "</p>");
        sb.Append("\r\n<blockquote>");

        return sb.ToString();
    }

    protected string companyLink(DEIA.CompanyIndexesRow companyRow)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("\r\n<p class=\"Companies\"><a href=\"../labs/" + companyRow.CompanyId + ".htm\" style='text-decoration:none'><b>* " +
            this.replaceAccentsToHtmlCodes(companyRow.CompanyName) + ".</b></a></p><br />");
    

        return sb.ToString();
    }

    protected string tercerasLink(DEIA.CompanyIndexesRow companyRow)
    {
        StringBuilder sb = new StringBuilder();

        if (companyRow.IsHtmlFileNull())
        {
            sb.Append("\r\n<tr><td>");
            sb.Append("\r\n<a href=\"" + companyRow.CompanyId.ToString() + ".htm\" style='text-decoration:none;'><b>" +
                this.replaceAccentsToHtmlCodes(companyRow.CompanyName) + "</b></a>");
            sb.Append("\r\n</td></tr>");

            this.antClient(4, companyRow);
        }
        else
        {
            sb.Append("\r\n<tr><td>");
            sb.Append("\r\n<a href=\"" + companyRow.HtmlFile + "\" style='text-decoration:none;'><b>" +
                this.replaceAccentsToHtmlCodes(companyRow.CompanyName) + "</b></a>");
            sb.Append("\r\n</td></tr>");
                       

        }
        return sb.ToString();
    }

    protected void antClient(int editionId, DEIA.CompanyIndexesRow companyRow)
    {
        StringBuilder sbAntTemp = new StringBuilder(this.getHtmlTemplate(@"C:\PLM Development VS2010\HTMLDiscs\DEIA\HtmlTemplates\Indices\ClienteAnterior.htm"));

        sbAntTemp.Replace("@@@ShortName@@@", companyRow.CompanyId.ToString());

        sbAntTemp.Replace("@@@CompanyName@@@", companyRow.CompanyName.ToString());

        DEIATableAdapters.CompaniesTableAdapter compAdp = new DEIATableAdapters.CompaniesTableAdapter();
        
        DEIA.CompaniesDataTable compTable = compAdp.getCompanyById(editionId, companyRow.CompanyId);

        foreach (DataRow compRow in compTable.Rows)
        {
            DEIA.CompaniesRow cRow = (DEIA.CompaniesRow)compRow;

            sbAntTemp.Replace("@@@Direccion@@@", this.getCompanyData(cRow));
                        
        }

        this.saveHtmlFile(companyRow.CompanyId.ToString() + ".htm", sbAntTemp.ToString());


    }

    protected string getCompanyData(DEIA.CompaniesRow compRow)
    {
        StringBuilder sb = new StringBuilder();

        sb.Append("\r\n@@@Direccion@@@<br />");
        sb.Append("\r\n@@@Colonia@@@<br />");
        sb.Append("\r\nCP. @@@CP@@@<br />");

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

        sb.Append("\r\n");

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
