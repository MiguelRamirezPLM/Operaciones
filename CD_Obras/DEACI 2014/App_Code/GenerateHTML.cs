using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Text;
 
public abstract class GenerateHTML
{
    #region Constructor

    public GenerateHTML(string sourcePath, string destinationPath)
	{
        this._destinationPath = destinationPath;
        this._sourcePath = sourcePath;
    }
    #endregion

    #region Public Methods

    public string getHtmlTemplate()
    {
        if (string.IsNullOrEmpty(this._sourcePath))
            throw new ArgumentException("The source path cannot be null or empty.");

        StreamReader sr = new StreamReader(this._sourcePath, Encoding.Default);
        string strHtml = sr.ReadToEnd();

        sr.Close();
        sr.Dispose();

        return strHtml;

    }

    public string getHtmlTemplate(string path)
    {
        if (string.IsNullOrEmpty(path))
            throw new ArgumentException("The source path cannot be null or empty.");

        StreamReader sr = new StreamReader(path, Encoding.Default);
        string strHtml = sr.ReadToEnd();

        sr.Close();
        sr.Dispose();

        return strHtml;

    }

    public void saveHtmlFile(string fileName, string fileContent)
    {

        if (string.IsNullOrEmpty(this._destinationPath))
            throw new ArgumentException("The destination path cannot be null or empty.");

        FileStream htmlFile = new FileStream(this._destinationPath + "\\" + fileName, FileMode.Create);
        StreamWriter sw = new StreamWriter(htmlFile);

        sw.Write(fileContent);

        sw.Close();
        sw.Dispose();

    }


    public string quitAccents(string originalString)
    {
        StringBuilder sb = new StringBuilder(originalString);

        sb.Replace("á", "a");
        sb.Replace("Á", "A");

        sb.Replace("é", "e");
        sb.Replace("É", "E");

        sb.Replace("í", "i");
        sb.Replace("Í", "I");

        sb.Replace("ó", "o");
        sb.Replace("Ó", "O");

        sb.Replace("ú", "u");
        sb.Replace("Ú", "U");


        return sb.ToString();
    }


    public string replaceAccentsToHtmlCodes(string originalString)
    {
        StringBuilder sb = new StringBuilder(originalString);

        sb.Replace("á", "&aacute;");
        sb.Replace("Á", "&Aacute;");

        sb.Replace("é", "&eacute;");
        sb.Replace("É", "&Eacute;");

        sb.Replace("í", "&iacute;");
        sb.Replace("Í", "&Iacute;");

        sb.Replace("ó", "&oacute;");
        sb.Replace("Ó", "&Oacute;");

        sb.Replace("ú", "&uacute;");
        sb.Replace("Ú", "&Uacute;");

        sb.Replace("ñ", "&ntilde;");
        sb.Replace("Ñ", "&Ntilde;");

        sb.Replace("´", "'");
        sb.Replace("ö", "&ouml;");
        sb.Replace("Ö", "&Ouml;");
        sb.Replace("ü", "&uuml;");
        sb.Replace("Ü", "&Uuml;");

        sb.Replace("&AACUTE;", "&Aacute;");
        sb.Replace("&EACUTE;", "&Eacute;");
        sb.Replace("&IACUTE;", "&Iacute;");
        sb.Replace("&OACUTE;", "&Oacute;");
        sb.Replace("&UACUTE;", "&Uacute;");

        sb.Replace("®", "<sup>&#174;</sup>");
        sb.Replace("™", "<sup>&#8482;</sup>");
        sb.Replace("’", "'");
        sb.Replace("Âº", " ");
        sb.Replace("•", " <b>&#8226;</b> ");
        sb.Replace(".", "");
        sb.Replace("“O”", "&#34;O&#34;");
        sb.Replace("™", "<sup>&#8482;</sup>");
        sb.Replace("“", "&#34;");
   
        return sb.ToString();
    }

      public string replacePhone(string originalString)
    {
        StringBuilder sb = new StringBuilder(originalString);

        sb.Replace(", F", "<br>F");
        sb.Replace(", L", "<br>L");
        sb.Replace(", T", "<br>T");
        sb.Replace("á", "&aacute;");
        sb.Replace("Á", "&Aacute;");

        sb.Replace("é", "&eacute;");
        sb.Replace("É", "&Eacute;");

        sb.Replace("í", "&iacute;");
        sb.Replace("Í", "&Iacute;");

        sb.Replace("ó", "&oacute;");
        sb.Replace("Ó", "&Oacute;");

        sb.Replace("ú", "&uacute;");
        sb.Replace("Ú", "&Uacute;");
        return sb.ToString();
    }

    public abstract void getHtmlFiles(int editionId);


    #endregion

    private string _sourcePath;
    private string _destinationPath;
}