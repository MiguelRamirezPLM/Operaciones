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

public class Companies : GenerateHTML
{
    #region Constructors

    public Companies(string sourcePath, string destinationPath) : base(sourcePath, destinationPath){}

    #endregion

    #region Public Methods

    public override void getHtmlFiles(int editionId)
    {
        //Get all letter from database:
        DEIATableAdapters.AlphabetTableAdapter alphabetAdp = new DEIATableAdapters.AlphabetTableAdapter();
        DEIA.AlphabetDataTable alphabetTable = alphabetAdp.getAlphabet();

        foreach (DataRow aRow in alphabetTable.Rows)
        {
            DEIA.AlphabetRow alphabetRow = (DEIA.AlphabetRow)aRow;

            //Get Specialities from Database:
            DEIATableAdapters.SectionsTableAdapter sectionsAdp = new DEIATableAdapters.SectionsTableAdapter();
            DEIA.SectionsDataTable sectionsTable = sectionsAdp.getByParentByLetter(editionId, _section, alphabetRow.Letter);

            foreach (DataRow sRow in sectionsTable.Rows)
            {
                DEIA.SectionsRow sectionRow = (DEIA.SectionsRow)sRow;

                // Get the states which correspond to each speciality:
                DEIATableAdapters.StatesTableAdapter statesAdp = new DEIATableAdapters.StatesTableAdapter();
                DEIA.StatesDataTable statesTable = statesAdp.getStatesBySection(editionId, sectionRow.SectionId);

                byte rowNumber = 1;

                foreach (DataRow stRow in statesTable.Rows)
                {
                    DEIA.StatesRow stateRow = (DEIA.StatesRow)stRow;

                    //Get the cities which correspond to each state:
                    DEIATableAdapters.CitiesTableAdapter citiesAdp = new DEIATableAdapters.CitiesTableAdapter();
                    DEIA.CitiesDataTable citiesTable = citiesAdp.getCitiesByState(editionId, sectionRow.SectionId, stateRow.StateId);

                    // Get the html file template to save the correspond section html file:
                    StringBuilder sbHtmlTemplate = new StringBuilder(this.getHtmlTemplate());
                    sbHtmlTemplate.Replace("@@@EstadoTitulo@@@", this.replaceAccentsToHtmlCodes(stateRow.StateName.ToUpper()));

                    //bool firstCity = true;

                    foreach (DataRow cRow in citiesTable)
                    {
                        DEIA.CitiesRow cityRow = (DEIA.CitiesRow)cRow;

                        //if (!firstCity)
                        //    sbHtmlTemplate.Append("\r\n<br><br><br>");

                        // Add the city title:
                        sbHtmlTemplate.Append(this.getCityTitle(this.replaceAccentsToHtmlCodes(cityRow.CityName)));

                        //Get the companies correspond to each city:
                        DEIATableAdapters.GuiaFabricantesTableAdapter compAdp = new DEIATableAdapters.GuiaFabricantesTableAdapter();
                        DEIA.GuiaFabricantesDataTable compTable = compAdp.getCompaniesByCity(editionId, sectionRow.SectionId, cityRow.CityId);

                        //bool firstCompany = true;

                        foreach (DataRow cpRow in compTable.Rows)
                        {
                            DEIA.GuiaFabricantesRow compRow = (DEIA.GuiaFabricantesRow)cpRow;

                            // Add a company:
                            sbHtmlTemplate.Append(this.getCompanyData(compRow));
                            //firstCompany = false;
                            
                            sbHtmlTemplate.Append("<br /><br />");
                        }
                        //firstCity = false;
                    }

                    sbHtmlTemplate.Append("\r\n</body></html>");

                    // Save the htm file:
                    this.saveHtmlFile(this.quitAccents(sectionRow.SectionName).Replace(" ", "").ToLower() + Convert.ToString(rowNumber++) + ".htm", sbHtmlTemplate.ToString());
                                        
                }
                
            }
        }
    }

    #endregion

    #region Protected Methods

    protected override string getLink(string href, string label)
    {
        throw new Exception("The method or operation is not implemented.");
    }

    protected string getCityTitle(string city)
    {
        StringBuilder sb = new StringBuilder();

        sb.Append("\r\n<p class=\"cityG\"><u>@@@Ciudad@@@</u></p> ");
        sb.Replace("@@@Ciudad@@@", city);

        return sb.ToString();
    }

    protected string getCompanyData(DEIA.GuiaFabricantesRow compRow)
    {
        StringBuilder sb = new StringBuilder();

        sb.Append("\r\n<p class=\"addressG\"><strong>@@@Empresa@@@</strong><br />");
        sb.Append("\r\n<i>");
        sb.Append("\r\n@@@Direccion@@@<br />");
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

        sb.Append("\r\n</i></p>");

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

    private const int _section = 14;
}
