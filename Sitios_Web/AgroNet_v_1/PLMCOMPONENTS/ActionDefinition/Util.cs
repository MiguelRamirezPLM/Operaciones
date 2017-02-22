using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Specialized;
using System.Collections;
using System.Text;

/// <summary>
/// Descripción breve de Util
/// </summary>
public class Util
{
    #region Constructors

    private Util() { }

    #endregion

    #region Public methods

    public void showCurrentAction(Label title, Actions enuAction)
    {
        if (enuAction == Actions.Add)
            title.Text = "Agregar - " + title.Text;

        else if (enuAction == Actions.Edit)
            title.Text = "Editar - " + title.Text;

        else if (enuAction == Actions.View)
            title.Text = "Ver - " + title.Text;

    }

    public string setInLineStyles(string htmlContent)
    {
        htmlContent = this.removeStylesForFirstDiv(htmlContent);

        int startIndex = 0;
        int indexClass = htmlContent.IndexOf("class", startIndex);

        HybridDictionary hdStyles = new System.Collections.Specialized.HybridDictionary();

        // The token class was found:
        while (indexClass >= 0)
        {
            int nextChar = indexClass + 5;

            string className = string.Empty;

            // It is a class attribute applied to a html tag:
            if (nextChar < htmlContent.Length && htmlContent[nextChar] == '=')
            { 
                // Get the class name:
                for (int i = nextChar + 1; htmlContent[i] != ' ' && htmlContent[i] != '>'; i++)
                    className += htmlContent[i];
            }

            if (!string.IsNullOrEmpty(className) && !hdStyles.Contains(className))
            {
                string classDef = this.getClassDefinition(htmlContent, className);

                if (!string.IsNullOrEmpty(classDef) && !hdStyles.Contains(className))
                    hdStyles.Add(className, classDef);
            }

            startIndex = indexClass + 5;
            indexClass = htmlContent.IndexOf("class", startIndex);
        }

        string htmlContentChanged = string.Empty;

        // We finished to collect all the class elements and its definitions:
        if (hdStyles.Count > 0)
        {
            StringBuilder sbClasses = new StringBuilder(htmlContent);

            // We are ready to replace class entries to inline styles:
            IDictionaryEnumerator e = hdStyles.GetEnumerator();

            while (e.MoveNext())
            {
                string classToReplace = "class=" + e.Key.ToString();
                string inlineStyle = "style=\"" + e.Value.ToString() + "\"";

                // First option:
                sbClasses.Replace(classToReplace, inlineStyle);

                // Second option:
                classToReplace = "class=\"" + e.Key.ToString() + "\"";
                sbClasses.Replace(classToReplace, inlineStyle);
            }

            htmlContentChanged = sbClasses.ToString();
        }
        else
            htmlContentChanged = htmlContent;

        return htmlContentChanged;
    }

    #endregion

    #region Private methods

    private string getClassDefinition(string htmlContent, string className)
    { 
        // Look for the class name inside the html content:
        int classIndex = htmlContent.IndexOf(className);

        string classDefinition = string.Empty;

        // The class name was found:
        if (classIndex >= 0)
        {
            int startStyleDefinition = htmlContent.IndexOf("{", classIndex);
            int endStyleDefinition = htmlContent.IndexOf("}", startStyleDefinition);

            if (startStyleDefinition >= 0 && endStyleDefinition >= 0)
                classDefinition = htmlContent.Substring(startStyleDefinition + 1, endStyleDefinition - startStyleDefinition - 1);

            classDefinition = classDefinition.Replace("\"", "").Replace("\r\n", "").ToLower();

            // Remove the line-height style:
            int lineHeightIndex = classDefinition.IndexOf("line-height");

            if (lineHeightIndex >= 0)
            {
                int commaIndex = classDefinition.IndexOf(";", lineHeightIndex);

                if (commaIndex >= 0)
                    classDefinition = classDefinition.Remove(lineHeightIndex, commaIndex - lineHeightIndex + 1);
            }
        }

        return classDefinition;
    }

    private string removeStylesForFirstDiv(string htmlContent)
    {
        int bodyIndex = htmlContent.ToLower().IndexOf("body");
        int divIndex = htmlContent.ToLower().IndexOf("div", bodyIndex);

        string newHtml = htmlContent;

        // There is a div tag inside body tag:
        if (divIndex >= 0)
        {
            int classDivIndex = htmlContent.ToLower().IndexOf("class", divIndex);

            if (classDivIndex >= 0)
            {
                string classTag = string.Empty;

                for (int i = classDivIndex; htmlContent[i] != ' ' && htmlContent[i] != '>'; i++)
                    classTag += htmlContent[i];

                string styleTag = "style='margin:5pt;'";

                newHtml = newHtml.Remove(classDivIndex, classTag.Length);
                newHtml = newHtml.Insert(classDivIndex, styleTag);
            }
        }

        return newHtml;
    }

    #endregion

    #region Enums

    public enum Applications : byte
    { 
        Pegaso = 1,
        Eplm = 2
    }

    public enum Actions : byte
    {
        Add = 1,
        View = 2,
        Edit = 3,
        Delete = 4
    }

    public enum UserTypes : byte
    { 
        Administrator = 1,
        Editor = 2,
        Standard = 3
    }

    public enum EditProduct : byte
    { 
        GeneralInfo = 0,
        Presentations = 1,
        Indexes = 2
    }

    #endregion

    public static readonly Util Instance = new Util();
}
