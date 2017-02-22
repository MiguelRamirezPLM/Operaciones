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

public class Letters : GenerateHTML
{
    #region Constructors

    public Letters(string sourcePath, string destinationPath) : base(sourcePath, destinationPath){ }

    #endregion

    #region Public Methods

    public override void  getHtmlFiles(int editionId)
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

            // Get the html file template to save the correspond letter html file:
            StringBuilder sbHtmlTemplate = new StringBuilder(this.getHtmlTemplate());

            foreach (DataRow sRow in sectionsTable.Rows)
            {
                DEIA.SectionsRow sectionRow = (DEIA.SectionsRow)sRow;
                sbHtmlTemplate.Append(this.getLink(this.quitAccents(sectionRow.SectionName).Replace(" ", "").ToLower(),
                    this.replaceAccentsToHtmlCodes(sectionRow.SectionName.ToUpper())));
                
            }

            sbHtmlTemplate.Append("\r\n</div></body></html>");

            // Save the htm file:
            this.saveHtmlFile(alphabetRow.Letter + ".htm", sbHtmlTemplate.ToString());
        }


    }

    #endregion

    #region Protected Methods

    protected override string getLink(string href, string label)
    {
        StringBuilder sb = new StringBuilder();

        sb.Append("\r\n<p class=\"sectionG\" align=\"left\">");
        sb.Append("\r\n<span lang=\"ES-TRAD\"><a style=\"text-decoration: none\" href=\"@@@SubSection@@@.htm\">*@@@SectionName@@@</a></span>");
        sb.Append("\r\n</p>");

        sb.Replace("@@@SubSection@@@", href);
        sb.Replace("@@@SectionName@@@", label);

        return sb.ToString();
    }

    #endregion


    private const int _section = 14;

}
