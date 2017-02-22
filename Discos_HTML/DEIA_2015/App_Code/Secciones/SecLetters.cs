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


public class SecLetters : GenerateHTML
{
    #region Constructors

    public SecLetters(string sourcePath, string destinationPath) : base(sourcePath, destinationPath){}

    #endregion

    #region Public Methods

    public override void getHtmlFiles(int editionId)
    {
        throw new Exception("The method or operation is not implemented.");
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
            sbHtmlTemplate.Replace("@@@TituloSeccion@@@", this.replaceAccentsToHtmlCodes(title.ToUpper()));

            foreach (DataRow sectRow in sectionsTable.Rows)
            {
                DEIA.SectionsRow sectionRow = (DEIA.SectionsRow)sectRow;

                DEIATableAdapters.SectionsTableAdapter subSections = new DEIATableAdapters.SectionsTableAdapter();
                DEIA.SectionsDataTable subSectTable = subSections.getByParentPr(editionId, sectionRow.SectionId);

                if (subSectTable.Rows.Count == 0)
                    sbHtmlTemplate.Append(this.sectionLink(sectionRow));
                else
                {
                    sbHtmlTemplate.Append("\r\n<tr><td>");
                    sbHtmlTemplate.Append("\r\n<p class=\"SubSeccion\"><b>" + this.replaceAccentsToHtmlCodes(sectionRow.SectionName.ToUpper()) + "</b></p>");
                    sbHtmlTemplate.Append("\r\n<blockquote>");

                    foreach (DataRow subSectRow in subSectTable.Rows)
                    {
                        DEIA.SectionsRow subSectionRow = (DEIA.SectionsRow)subSectRow;

                        sbHtmlTemplate.Append(this.subSectionLink(subSectionRow));

                    }

                    sbHtmlTemplate.Append("\r\n</blockquote>");
                    sbHtmlTemplate.Append("\r\n</td></tr>");

                }
            }
            sbHtmlTemplate.Append("\r\n</table>");

            sbHtmlTemplate.Append("\r\n</div></body></html>");

            // Save the htm file:
            this.saveHtmlFile(alphabetRow.Letter + "-mp.htm", sbHtmlTemplate.ToString());
        }
    }

    public void getHtmlFilesSections(int editionId, int sectionId, string title)
    {
        //Get Specialities from Database:
        DEIATableAdapters.SectionsTableAdapter sectionAdp = new DEIATableAdapters.SectionsTableAdapter();
        DEIA.SectionsDataTable sectionsTable = sectionAdp.getByParentPr(editionId, sectionId);

        // Get the html file template to save the correspond letter html file:
        StringBuilder sbHtmlTemplate = new StringBuilder(this.getHtmlTemplate());
        sbHtmlTemplate.Replace("@@@Seccion@@@", this.replaceAccentsToHtmlCodes(title.ToUpper()));
        
        foreach (DataRow sectRow in sectionsTable.Rows)
        {
            DEIA.SectionsRow sectionRow = (DEIA.SectionsRow)sectRow;
                  
            sbHtmlTemplate.Append(this.sectionLink(sectionRow));

        }
        
        sbHtmlTemplate.Append("\r\n</table>");

        sbHtmlTemplate.Append("\r\n</div></body></html>");
        
        // Save the htm file:
        this.saveHtmlFile(this.quitAccents(title.ToLower()) + ".htm", sbHtmlTemplate.ToString());
       
    }

    public void getHtmlFilesSectionsCC(int editionId, int sectionId, string title)
    {
        //Get Specialities from Database:
        DEIATableAdapters.SectionsTableAdapter sectionAdp = new DEIATableAdapters.SectionsTableAdapter();
        DEIA.SectionsDataTable sectionsTable = sectionAdp.getBySubSection(editionId, sectionId);

        // Get the html file template to save the correspond letter html file:
        StringBuilder sbHtmlTemplate = new StringBuilder(this.getHtmlTemplate());
        sbHtmlTemplate.Replace("@@@Seccion@@@", this.replaceAccentsToHtmlCodes(title.ToUpper()));

        foreach (DataRow sectRow in sectionsTable.Rows)
        {
            DEIA.SectionsRow sectionRow = (DEIA.SectionsRow)sectRow;

            sbHtmlTemplate.Append(this.sectionLink(sectionRow));

            this.getHtmlFilesSubSectionsCC(editionId, sectionRow.SectionId, sectionRow.SectionName);

        }

        sbHtmlTemplate.Append("\r\n</table>");

        sbHtmlTemplate.Append("\r\n</div></body></html>");

        // Save the htm file:
        this.saveHtmlFile(this.quitAccents(title.ToLower()) + ".htm", sbHtmlTemplate.ToString());

    }

    public void getHtmlFilesSubSectionsCC(int editionId, int sectionId, string title)
    {
        //Get Specialities from Database:
        DEIATableAdapters.SectionsTableAdapter sectionAdp = new DEIATableAdapters.SectionsTableAdapter();
        DEIA.SectionsDataTable sectionsTable = sectionAdp.getByParentPr(editionId, sectionId);

        // Get the html file template to save the correspond letter html file:
        StringBuilder sbHtmlTemplate = new StringBuilder(this.getHtmlTemplate());
        sbHtmlTemplate.Replace("@@@Seccion@@@", this.replaceAccentsToHtmlCodes(title.ToUpper()));

        foreach (DataRow sectRow in sectionsTable.Rows)
        {
            DEIA.SectionsRow sectionRow = (DEIA.SectionsRow)sectRow;

            sbHtmlTemplate.Append(this.sectionLink(sectionRow));

            this.getHtmlFilesSections(editionId, sectionRow.SectionId, sectionRow.SectionId.ToString());

        }

        sbHtmlTemplate.Append("\r\n</table>");

        sbHtmlTemplate.Append("\r\n</div></body></html>");

        // Save the htm file:
        this.saveHtmlFile(sectionId + ".htm", sbHtmlTemplate.ToString());

    }
    
    public void getHtmlFilesSectionsEE(int editionId, int sectionId, string title)
    {
        //Get Specialities from Database:
        DEIATableAdapters.SectionsTableAdapter sectionAdp = new DEIATableAdapters.SectionsTableAdapter();
        DEIA.SectionsDataTable sectionsTable = sectionAdp.getByParent(editionId, sectionId);

        // Get the html file template to save the correspond letter html file:
        StringBuilder sbHtmlTemplate = new StringBuilder(this.getHtmlTemplate());
        sbHtmlTemplate.Replace("@@@Seccion@@@", this.replaceAccentsToHtmlCodes(title.ToUpper()));

        foreach (DataRow sectRow in sectionsTable.Rows)
        {
            DEIA.SectionsRow sectionRow = (DEIA.SectionsRow)sectRow;

            sbHtmlTemplate.Append(this.sectionLink(sectionRow));

        }

        sbHtmlTemplate.Append("\r\n</table>");

        sbHtmlTemplate.Append("\r\n</div></body></html>");

        // Save the htm file:
        this.saveHtmlFile(this.quitAccents(title.ToLower()) + ".htm", sbHtmlTemplate.ToString());

    }

    #endregion

    #region Protected Methods

    protected override string getLink(string href, string label)
    {
        throw new Exception("The method or operation is not implemented.");
    }

    protected string sectionLink(DEIA.SectionsRow section)
    {
        StringBuilder sb = new StringBuilder();

        sb.Append("\r\n<tr><td>");
        sb.Append("\r\n<p class=\"SubSeccion\"><b><a href=\"" + section.SectionId + ".htm\" style='text-decoration:none'><b>" + 
            this.replaceAccentsToHtmlCodes(section.SectionName.ToUpper()) + "</b></a></p>");
        sb.Append("\r\n</td></tr>");

        return sb.ToString();
    }

    protected string subSectionLink(DEIA.SectionsRow section)
    {
        StringBuilder sb = new StringBuilder();

        sb.Append("\r\n<p class=\"SubSeccion\"><b><a href=\"" + section.SectionId + ".htm\" style='text-decoration:none'><b>" +
            this.replaceAccentsToHtmlCodes(section.SectionName.ToUpper()) + "</b></a></p>");
        
        return sb.ToString();
    }

    #endregion

}
