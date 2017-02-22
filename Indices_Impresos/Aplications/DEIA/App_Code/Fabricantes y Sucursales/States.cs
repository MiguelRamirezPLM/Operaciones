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


public class States : GenerateHTML
{
    #region Constructors

    public States(string sourcePath, string destinationPath) : base(sourcePath, destinationPath) { }
 
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

                // Get the html file template to save the correspond section html file:
                StringBuilder sbHtmlTemplate = new StringBuilder(this.getHtmlTemplate());
                sbHtmlTemplate.Replace("@@@SeccionTitulo@@@", this.replaceAccentsToHtmlCodes(sectionRow.SectionName.ToUpper()));

                byte rowNumber = 1;

                foreach (DataRow stRow in statesTable.Rows)
                {
                    DEIA.StatesRow stateRow = (DEIA.StatesRow)stRow;

                    sbHtmlTemplate.Append(this.getLink(this.quitAccents(sectionRow.SectionName).Replace(" ","").ToLower() + Convert.ToString(rowNumber++),
                        this.replaceAccentsToHtmlCodes(stateRow.StateName.ToUpper())));
                    
                }

                sbHtmlTemplate.Append("</p>\r\n</body></html>");

                // Save the htm file:
                this.saveHtmlFile(this.quitAccents(sectionRow.SectionName).Replace(" ", "").ToLower() + ".htm", sbHtmlTemplate.ToString());
            }

        }

    }

    public void getHtmlFiles(int editionId, int companyId, string title)
    {
        // Get the states which correspond to each speciality:
        DEIATableAdapters.StatesTableAdapter statesAdp = new DEIATableAdapters.StatesTableAdapter();
        DEIA.StatesDataTable statesTable = statesAdp.getStatesByCompany(editionId, _sectionSuc, companyId);

        // Get the html file template to save the correspond section html file:
        StringBuilder sbHtmlTemplate = new StringBuilder(this.getHtmlTemplate());
        sbHtmlTemplate.Replace("@@@SeccionTitulo@@@", "Sucursales de " + this.replaceAccentsToHtmlCodes(title));
              

        foreach (DataRow stRow in statesTable.Rows)
        {
            DEIA.StatesRow stateRow = (DEIA.StatesRow)stRow;

            sbHtmlTemplate.Append(this.getLinkBr(title.ToLower() + "_" + this.quitAccents(stateRow.StateName).Replace(" ", "").ToLower(),
                this.replaceAccentsToHtmlCodes(stateRow.StateName.ToUpper())));

        }

        sbHtmlTemplate.Append("</p>\r\n</body></html>");

        // Save the htm file:
        this.saveHtmlFile(this.quitAccents(title).Replace(" ", "").ToLower() + ".htm", sbHtmlTemplate.ToString());
    }

    #endregion

    #region Protected Methods

    protected override string getLink(string href, string label)
    {
        StringBuilder sb = new StringBuilder();

        sb.Append("\r\n<a class=\"stateG\" href='fab/@@@EstadoRef@@@.htm'>*@@@EstadoRotulo@@@</a><br>");

        sb.Replace("@@@EstadoRef@@@", href);
        sb.Replace("@@@EstadoRotulo@@@", label);

        return sb.ToString();
    }

    protected string getLinkBr(string href, string label)
    {
        StringBuilder sb = new StringBuilder();

        sb.Append("\r\n<a class=\"stateG\" href='suc/@@@EstadoRef@@@.htm'>* @@@EstadoRotulo@@@</a><br>");

        sb.Replace("@@@EstadoRef@@@", href);
        sb.Replace("@@@EstadoRotulo@@@", label);

        return sb.ToString();
    }

    
    #endregion

    private const int _section = 14;
    private const int _sectionSuc = 4;

}
