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

/// <summary>
/// Descripción breve de GenerateHTML
/// </summary>
public abstract class GenerateHTML
{
    #region Constructors

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

    public string cleanFileName(string originalString)
    {
        StringBuilder sb = new StringBuilder(originalString.ToUpper());

        sb.Replace("Á", "A");
        sb.Replace("É", "E");
        sb.Replace("Í", "I");
        sb.Replace("Ó", "O");
        sb.Replace("Ú", "U");
        sb.Replace("Ñ", "N");
        sb.Replace("Ü", "U");
        sb.Replace("&", "");
        sb.Replace("/", "");
        sb.Replace("-", "");
        sb.Replace(":", "");
        sb.Replace(".", "");
        sb.Replace(";", "");
        sb.Replace(",", "");
        sb.Replace("'", "");
        sb.Replace("\"", "");
        sb.Replace("+", "");
        sb.Replace("*", "");
        sb.Replace("(", "");
        sb.Replace(")", "");
        sb.Replace(">", "");
        sb.Replace("<", "");
        sb.Replace("´", "");
        sb.Replace("[", "");
        sb.Replace("]", "");
        sb.Replace("{", "");
        sb.Replace("}", "");
        sb.Replace("@", "");
        sb.Replace("?", "");
        sb.Replace("%", "");
        sb.Replace("Â", "A");
        sb.Replace("Ê", "E");
        sb.Replace("Î", "I");
        sb.Replace("Ô", "O");
        sb.Replace("Û", "U");
        sb.Replace("\t", "");
        sb.Replace(":", "");
        sb.Replace(" ", "_");
        sb.Replace("__", "_");
        sb.Replace("__", "_");


        return sb.ToString().ToLower();
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

        sb.Replace("®", "<sup>&#174;</sup>");
        sb.Replace("™", "<sup>&#8482;</sup>");


        return sb.ToString();
    }

    public abstract void getHtmlFiles(int editionId);

    public string getComscore(int numberEdition, string category, string country, string brand, string pharma)
    {
        string comscore;
        country = country.ToUpper();

        brand = cleanFileName(brand);
        brand = brand.ToLower();
       
        string letterBrand = letterOnly(brand);
        if (pharma != null)
        {
            pharma = cleanFileName(pharma);
            pharma = pharma.ToLower();
            string letterPharma = letterOnly(pharma);
            comscore = country + "." + "DEF." + numberEdition + "." + category + "." + letterBrand + "_" + letterPharma;
        }
        else
        {
            comscore = country + "." + "DEF." + numberEdition + "." + category + "." + letterBrand;
        }
        return comscore;
    }

    public string getGoogleAnalytics(string country)
    {
        string codeGoogle = "";
        if (country == "COL")
        {
            codeGoogle = "<script type=\"text/javascript\">" +
                          "var _gaq = _gaq || [];" +
                          "_gaq.push(['_setAccount', 'UA-22228735-10']);" +
                          "_gaq.push(['_trackPageview']);" +
                          "(function() {" +
                          "  var ga = document.createElement('script'); ga.type = 'text/javascript'; ga.async = true;" +
                          "  ga.src = ('https:' == document.location.protocol ? 'https://ssl' : 'http://www') + '.google-analytics.com/ga.js';" +
                          "  var s = document.getElementsByTagName('script')[0]; s.parentNode.insertBefore(ga, s);" +
                          "})();" +
                        "</script>";
        }
        if (country == "ECU")
        {
            codeGoogle = "<script type=\"text/javascript\">" +
                          "var _gaq = _gaq || [];" +
                          "_gaq.push(['_setAccount', 'UA-22228735-12']);" +
                          "_gaq.push(['_trackPageview']);" +
                          "(function() {" +
                          "  var ga = document.createElement('script'); ga.type = 'text/javascript'; ga.async = true;" +
                          "  ga.src = ('https:' == document.location.protocol ? 'https://ssl' : 'http://www') + '.google-analytics.com/ga.js';" +
                          "  var s = document.getElementsByTagName('script')[0]; s.parentNode.insertBefore(ga, s);" +
                          "})();" +
                        "</script>";
        }
        if (country == "MEX")
        {
            codeGoogle = "<script type=\"text/javascript\">" +
                          "var _gaq = _gaq || [];" +
                          "_gaq.push(['_setAccount', 'UA-22228735-6']);" +
                          "_gaq.push(['_trackPageview']);" +
                          "(function() {" +
                          "  var ga = document.createElement('script'); ga.type = 'text/javascript'; ga.async = true;" +
                          "  ga.src = ('https:' == document.location.protocol ? 'https://ssl' : 'http://www') + '.google-analytics.com/ga.js';" +
                          "  var s = document.getElementsByTagName('script')[0]; s.parentNode.insertBefore(ga, s);" +
                          "})();" +
                        "</script>";
        }
        if (country == "CAD")
        {
            codeGoogle = "<script type=\"text/javascript\">" +
                          "var _gaq = _gaq || [];" +
                          "_gaq.push(['_setAccount', 'UA-22228735-13']);" +
                          "_gaq.push(['_trackPageview']);" +
                          "(function() {" +
                          "  var ga = document.createElement('script'); ga.type = 'text/javascript'; ga.async = true;" +
                          "  ga.src = ('https:' == document.location.protocol ? 'https://ssl' : 'http://www') + '.google-analytics.com/ga.js';" +
                          "  var s = document.getElementsByTagName('script')[0]; s.parentNode.insertBefore(ga, s);" +
                          "})();" +
                        "</script>";
        }
        if (country == "PER")
        {
            codeGoogle = "<script type=\"text/javascript\">" +
                          "var _gaq = _gaq || [];" +
                          "_gaq.push(['_setAccount', 'UA-22228735-11']);" +
                          "_gaq.push(['_trackPageview']);" +
                          "(function() {" +
                          "  var ga = document.createElement('script'); ga.type = 'text/javascript'; ga.async = true;" +
                          "  ga.src = ('https:' == document.location.protocol ? 'https://ssl' : 'http://www') + '.google-analytics.com/ga.js';" +
                          "  var s = document.getElementsByTagName('script')[0]; s.parentNode.insertBefore(ga, s);" +
                          "})();" +
                        "</script>";
        }
        return codeGoogle;
    }

    public string letterOnly(string texto)
    {
        return texto.Substring(0, 1).ToUpper() + texto.Substring(1);
    }
    #endregion


    private string _sourcePath;
    private string _destinationPath;

    
}
