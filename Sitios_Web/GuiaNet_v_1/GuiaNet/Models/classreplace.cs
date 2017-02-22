using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace GuiaNet.Models
{
    public class classreplace
    {
        public static string replace(string _description)
        {
            _description = _description.Replace(",", "");
            _description = _description.Replace("..", "");
            _description = _description.Replace("  ", " ");

            _description = _description.Replace("á", "a");
            _description = _description.Replace("Á", "A");
            _description = _description.Replace("é", "e");
            _description = _description.Replace("É", "E");
            _description = _description.Replace("í", "i");
            _description = _description.Replace("Í", "I");
            _description = _description.Replace("ó", "o");
            _description = _description.Replace("Ó", "O");
            _description = _description.Replace("Ú", "U");
            _description = _description.Replace("ú", "u");
            _description = _description.Replace("ü", "u");
            _description = _description.Replace("Ü", "U");

            _description = _description.Replace("*", "");
            _description = _description.Replace("®", "");

            _description = _description.Replace("\"A\"", "A");
            _description = _description.Replace("\"B\"", "B");
            _description = _description.Replace("\"AB\"", "AB");
            _description = _description.Replace("\"C\"", "C");
            _description = _description.Replace("\"D\"", "D");
            _description = _description.Replace("\"E\"", "E");
            _description = _description.Replace("\"F\"", "F");
            _description = _description.Replace("\"G\"", "G");
            _description = _description.Replace("\"H\"", "H");
            _description = _description.Replace("\"I\"", "I");
            _description = _description.Replace("\"J\"", "J");
            _description = _description.Replace("\"K\"", "K");
            _description = _description.Replace("\"L\"", "L");
            _description = _description.Replace("\"M\"", "M");
            _description = _description.Replace("\"N\"", "N");
            _description = _description.Replace("\"Ñ\"", "Ñ");
            _description = _description.Replace("\"O\"", "O");
            _description = _description.Replace("\"P\"", "P");
            _description = _description.Replace("\"Q\"", "Q");
            _description = _description.Replace("\"R\"", "R");
            _description = _description.Replace("\"S\"", "S");
            _description = _description.Replace("\"T\"", "T");
            _description = _description.Replace("\"U\"", "U");
            _description = _description.Replace("\"V\"", "V");
            _description = _description.Replace("\"W\"", "W");
            _description = _description.Replace("\"X\"", "X");
            _description = _description.Replace("\"Y\"", "Y");
            _description = _description.Replace("\"Z\"", "Z");

            _description = _description.Replace("A.", "A");
            _description = _description.Replace("B.", "B");
            _description = _description.Replace("AB.", "AB");
            _description = _description.Replace("C.", "C");
            _description = _description.Replace("D.", "D");
            _description = _description.Replace("E.", "E");
            _description = _description.Replace("F.", "F");
            _description = _description.Replace("G.", "G");
            _description = _description.Replace("H.", "H");
            _description = _description.Replace("I.", "I");
            _description = _description.Replace("J.", "J");
            _description = _description.Replace("K.", "K");
            _description = _description.Replace("L.", "L");
            _description = _description.Replace("M.", "M");
            _description = _description.Replace("N.", "N");
            _description = _description.Replace("Ñ.", "Ñ");
            _description = _description.Replace("O.", "O");
            _description = _description.Replace("P.", "P");
            _description = _description.Replace("Q.", "Q");
            _description = _description.Replace("R.", "R");
            _description = _description.Replace("S.", "S");
            _description = _description.Replace("T.", "T");
            _description = _description.Replace("U.", "U");
            _description = _description.Replace("V.", "V");
            _description = _description.Replace("W.", "W");
            _description = _description.Replace("X.", "X");
            _description = _description.Replace("Y.", "Y");
            _description = _description.Replace("Z.", "Z");

            _description = _description.Replace(" ¼”", "1/4");
            _description = _description.Replace("µg", "ug");
            _description = _description.Replace("- ", " ");
            _description = _description.Replace(": ", " ");
            _description = _description.Replace("; ", " ");
            _description = _description.Replace("mL", "ml");
            _description = _description.Replace("'", " ");           
            _description = _description.Replace("(", "");
            _description = _description.Replace(")", "");
            //_description = _description.Replace("/ ", "/");
            //_description = _description.Replace(" /", "/");
            //_description = _description.Replace(" / ", "/");
            _description = _description.Replace("/", " ");
            _description = _description.Replace("   ", " ");

            return _description;
        }

        public static string descriptiondatabasereplace(string descriptiondatabase)
        {
            descriptiondatabase = descriptiondatabase.Replace(",", "");
            descriptiondatabase = descriptiondatabase.Replace("..", "");
            descriptiondatabase = descriptiondatabase.Replace("  ", " ");


            descriptiondatabase = descriptiondatabase.Replace("á", "a");
            descriptiondatabase = descriptiondatabase.Replace("Á", "A");
            descriptiondatabase = descriptiondatabase.Replace("é", "e");
            descriptiondatabase = descriptiondatabase.Replace("É", "E");
            descriptiondatabase = descriptiondatabase.Replace("í", "i");
            descriptiondatabase = descriptiondatabase.Replace("Í", "I");
            descriptiondatabase = descriptiondatabase.Replace("ó", "o");
            descriptiondatabase = descriptiondatabase.Replace("Ó", "O");
            descriptiondatabase = descriptiondatabase.Replace("Ú", "U");
            descriptiondatabase = descriptiondatabase.Replace("ú", "u");
            descriptiondatabase = descriptiondatabase.Replace("ü", "u");
            descriptiondatabase = descriptiondatabase.Replace("Ü", "U");

            descriptiondatabase = descriptiondatabase.Replace("*", "");
            descriptiondatabase = descriptiondatabase.Replace("®", "");
            
            descriptiondatabase = descriptiondatabase.Replace("\"A\"", "A");
            descriptiondatabase = descriptiondatabase.Replace("\"B\"", "B");
            descriptiondatabase = descriptiondatabase.Replace("\"AB\"", "AB");
            descriptiondatabase = descriptiondatabase.Replace("\"C\"", "C");
            descriptiondatabase = descriptiondatabase.Replace("\"D\"", "D");
            descriptiondatabase = descriptiondatabase.Replace("\"E\"", "E");
            descriptiondatabase = descriptiondatabase.Replace("\"F\"", "F");
            descriptiondatabase = descriptiondatabase.Replace("\"G\"", "G");
            descriptiondatabase = descriptiondatabase.Replace("\"H\"", "H");
            descriptiondatabase = descriptiondatabase.Replace("\"I\"", "I");
            descriptiondatabase = descriptiondatabase.Replace("\"J\"", "J");
            descriptiondatabase = descriptiondatabase.Replace("\"K\"", "K");
            descriptiondatabase = descriptiondatabase.Replace("\"L\"", "L");
            descriptiondatabase = descriptiondatabase.Replace("\"M\"", "M");
            descriptiondatabase = descriptiondatabase.Replace("\"N\"", "N");
            descriptiondatabase = descriptiondatabase.Replace("\"Ñ\"", "Ñ");
            descriptiondatabase = descriptiondatabase.Replace("\"O\"", "O");
            descriptiondatabase = descriptiondatabase.Replace("\"P\"", "P");
            descriptiondatabase = descriptiondatabase.Replace("\"Q\"", "Q");
            descriptiondatabase = descriptiondatabase.Replace("\"R\"", "R");
            descriptiondatabase = descriptiondatabase.Replace("\"S\"", "S");
            descriptiondatabase = descriptiondatabase.Replace("\"T\"", "T");
            descriptiondatabase = descriptiondatabase.Replace("\"U\"", "U");
            descriptiondatabase = descriptiondatabase.Replace("\"V\"", "V");
            descriptiondatabase = descriptiondatabase.Replace("\"W\"", "W");
            descriptiondatabase = descriptiondatabase.Replace("\"X\"", "X");
            descriptiondatabase = descriptiondatabase.Replace("\"Y\"", "Y");
            descriptiondatabase = descriptiondatabase.Replace("\"Z\"", "Z");


            descriptiondatabase = descriptiondatabase.Replace("A.", "A");
            descriptiondatabase = descriptiondatabase.Replace("B.", "B");
            descriptiondatabase = descriptiondatabase.Replace("AB.", "AB");
            descriptiondatabase = descriptiondatabase.Replace("C.", "C");
            descriptiondatabase = descriptiondatabase.Replace("D.", "D");
            descriptiondatabase = descriptiondatabase.Replace("E.", "E");
            descriptiondatabase = descriptiondatabase.Replace("F.", "F");
            descriptiondatabase = descriptiondatabase.Replace("G.", "G");
            descriptiondatabase = descriptiondatabase.Replace("H.", "H");
            descriptiondatabase = descriptiondatabase.Replace("I.", "I");
            descriptiondatabase = descriptiondatabase.Replace("J.", "J");
            descriptiondatabase = descriptiondatabase.Replace("K.", "K");
            descriptiondatabase = descriptiondatabase.Replace("L.", "L");
            descriptiondatabase = descriptiondatabase.Replace("M.", "M");
            descriptiondatabase = descriptiondatabase.Replace("N.", "N");
            descriptiondatabase = descriptiondatabase.Replace("Ñ.", "Ñ");
            descriptiondatabase = descriptiondatabase.Replace("O.", "O");
            descriptiondatabase = descriptiondatabase.Replace("P.", "P");
            descriptiondatabase = descriptiondatabase.Replace("Q.", "Q");
            descriptiondatabase = descriptiondatabase.Replace("R.", "R");
            descriptiondatabase = descriptiondatabase.Replace("S.", "S");
            descriptiondatabase = descriptiondatabase.Replace("T.", "T");
            descriptiondatabase = descriptiondatabase.Replace("U.", "U");
            descriptiondatabase = descriptiondatabase.Replace("V.", "V");
            descriptiondatabase = descriptiondatabase.Replace("W.", "W");
            descriptiondatabase = descriptiondatabase.Replace("X.", "X");
            descriptiondatabase = descriptiondatabase.Replace("Y.", "Y");
            descriptiondatabase = descriptiondatabase.Replace("Z.", "Z");

            descriptiondatabase = descriptiondatabase.Replace(" ¼”", "1/4");
            descriptiondatabase = descriptiondatabase.Replace("µg", "ug");
            descriptiondatabase = descriptiondatabase.Replace("- ", " ");
            descriptiondatabase = descriptiondatabase.Replace(": ", " ");
            descriptiondatabase = descriptiondatabase.Replace("; ", " ");
            descriptiondatabase = descriptiondatabase.Replace("mL", "ml");
            descriptiondatabase = descriptiondatabase.Replace("'", " ");
            descriptiondatabase = descriptiondatabase.Replace(" / ", "/");
            descriptiondatabase = descriptiondatabase.Replace("(", "");
            descriptiondatabase = descriptiondatabase.Replace(")", "");
            descriptiondatabase = descriptiondatabase.Replace("/ ", "/");
            descriptiondatabase = descriptiondatabase.Replace(" /", "/");
            descriptiondatabase = descriptiondatabase.Replace("/", " ");
            descriptiondatabase = descriptiondatabase.Replace("   ", " ");

            return descriptiondatabase;
        }

        public static string replacepdffilename(string pdf)
        {
            pdf = pdf.Replace(" ","_");
            pdf = pdf.Replace("__", "_");
            pdf = pdf.Replace("á", "a");
            pdf = pdf.Replace("Á", "A");
            pdf = pdf.Replace("é", "e");
            pdf = pdf.Replace("É", "E");
            pdf = pdf.Replace("í", "i");
            pdf = pdf.Replace("Í", "I");
            pdf = pdf.Replace("ó", "o");
            pdf = pdf.Replace("Ó", "O");
            pdf = pdf.Replace("Ú", "U");
            pdf = pdf.Replace("ú", "u");
            pdf = pdf.Replace("ü", "u");
            pdf = pdf.Replace("Ü", "U");
            pdf = pdf.Replace(",", "_");
            pdf = pdf.Replace("-", "_");
            return pdf;
        }

        public static string replacehtmlfilename(string html)
        {
            html = html.Replace(" ", "_");
            html = html.Replace("__", "_");
            html = html.Replace("á", "a");
            html = html.Replace("Á", "A");
            html = html.Replace("é", "e");
            html = html.Replace("É", "E");
            html = html.Replace("í", "i");
            html = html.Replace("Í", "I");
            html = html.Replace("ó", "o");
            html = html.Replace("Ó", "O");
            html = html.Replace("Ú", "U");
            html = html.Replace("ú", "u");
            html = html.Replace("ü", "u");
            html = html.Replace("Ü", "U");
            html = html.Replace(",", "_");
            html = html.Replace("-", "_");
            html = html.Replace("___", "_");

            return html;
        }

        public static string replacelevel(string level)
        {

            level = level.Replace("Á", "A");
            level = level.Replace("É", "E");
            level = level.Replace("Í", "I");
            level = level.Replace("Ó", "O");
            level = level.Replace("Ú", "U");
            level = level.Replace("Ü", "U");

            level = level.Replace("á", "a");
            level = level.Replace("é", "e");
            level = level.Replace("í", "i");
            level = level.Replace("o", "o");
            level = level.Replace("ú", "u");
            level = level.Replace("ü", "u");

            level = level.Replace("  ", " ");
            level = level.Replace(".", "");
            level = level.Replace(",", "");
            level = level.Replace("-", "");

            return level;
        }

        public static string replacedatabase(string descriptiondatabase)
        {
            descriptiondatabase = descriptiondatabase.Replace("Á", "A");
            descriptiondatabase = descriptiondatabase.Replace("É", "E");
            descriptiondatabase = descriptiondatabase.Replace("Í", "I");
            descriptiondatabase = descriptiondatabase.Replace("Ó", "O");
            descriptiondatabase = descriptiondatabase.Replace("Ú", "U");
            descriptiondatabase = descriptiondatabase.Replace("Ü", "U");

            descriptiondatabase = descriptiondatabase.Replace("á", "a");
            descriptiondatabase = descriptiondatabase.Replace("é", "e");
            descriptiondatabase = descriptiondatabase.Replace("í", "i");
            descriptiondatabase = descriptiondatabase.Replace("o", "o");
            descriptiondatabase = descriptiondatabase.Replace("ú", "u");
            descriptiondatabase = descriptiondatabase.Replace("ü", "u");

            descriptiondatabase = descriptiondatabase.Replace("  ", " ");
            descriptiondatabase = descriptiondatabase.Replace(".", "");
            descriptiondatabase = descriptiondatabase.Replace(",", "");
            descriptiondatabase = descriptiondatabase.Replace("-", "");

            return descriptiondatabase;
        }

        public string quitAccentsFileName(string originalString)
        {
            int ini, med;
            ini = 0;
            med = 0;
            string token;

            System.Text.StringBuilder sb = new System.Text.StringBuilder(originalString);

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

            sb.Replace("ü", "u");

            sb.Replace("ñ", "n");
            sb.Replace("&", "and");
            sb.Replace("'", "");
            sb.Replace("<br />", " ");
            sb.Replace("*", "");
            sb.Replace("®", "");
            sb.Replace(".", "");
            sb.Replace(" ", "");
            sb.Replace("/", "");
            sb.Replace("%", "");
            sb.Replace("+", "");
            sb.Replace("'", "");
            sb.Replace("&quot;", "");
            sb.Replace(",", "");
            sb.Replace("-", "_");
            sb.Replace(":", "");
            sb.Replace(";", "");
            sb.Replace("°", "");

            sb.Replace("&#174;", "");
            sb.Replace("&#9;", "");
            sb.Replace("#9", "");
            sb.Replace("Ning&uacute;n-estilo-de-tabla", "Ningun-estilo-de-tabla");


            if (sb.ToString().IndexOf("<", 0) >= 0)
                for (int i = 0; i < sb.Length; i++)
                {
                    ini = sb.ToString().IndexOf("<", 0);
                    med = sb.ToString().IndexOf(">", 0);

                    token = sb.ToString().Substring(ini, ((med + 1) - ini));

                    sb.Replace(token, "");

                    if (sb.ToString().IndexOf("<", 0) == -1)
                        i = sb.Length;

                }

            sb = sb.Replace("</span>", "");

            return sb.ToString();


        }

        public string quitAccents(string originalString)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder(originalString);

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

            sb.Replace("ü", "&uuml;");
            sb.Replace("Ü", "&Uuml;");

            sb.Replace(" <br />", " ");

            sb.Replace("-<br />", "");

            sb.Replace("®", "&#174;");

            sb.Replace("%2520", "%20");
            sb.Replace("™", "");


            return sb.ToString();
        }

        public void setParameters(StringBuilder sbBody, params string[] htmlParameters)
        {
            for (int i = 0; i < htmlParameters.Length; i++)
            {
                string paramName = htmlParameters[i].Split('=')[0];
                string paramValue = htmlParameters[i].Split('=')[1];

                sbBody.Replace(paramName, paramValue);
            }
        }

        public string cleanName(string sb)
        {
            int ini, med;
            ini = 0;
            med = 0;
            string token;

            if (sb.IndexOf("<", 0) >= 0)
                for (int i = 0; i < sb.Length; i++)
                {
                    ini = sb.IndexOf("<", 0);
                    med = sb.IndexOf(">", 0);

                    token = sb.Substring(ini, ((med + 1) - ini));

                    sb = sb.Replace(token, "");

                    if (sb.IndexOf("<", 0) == -1)
                        i = sb.Length;

                }

            sb = sb.Replace("</span>", "");
            sb = sb.Replace("*", "");
            sb = sb.Replace("®", "");
            sb = sb.Replace("'", "");
            sb = sb.Replace("  ", " ");
            sb = sb.Replace(":", "");
            sb = sb.Replace(";", "");
            sb = sb.Replace(",", "");
            sb = sb.Replace("´", "");
            sb = sb.Replace("/", "");
            sb = sb.Replace("á", "a");
            sb = sb.Replace("Á", "A");
            sb = sb.Replace(" ", "_");
            sb = sb.Replace("é", "e");
            sb = sb.Replace("É", "E");

            sb = sb.Replace("í", "i");
            sb = sb.Replace("Í", "I");

            sb = sb.Replace("ó", "o");
            sb = sb.Replace("Ó", "O");

            sb = sb.Replace("ú", "u");
            sb = sb.Replace("Ú", "U");

            sb = sb.Replace("ü", "u");

            sb = sb.Replace("&#9;", "");
            sb = sb.Replace("#9", "");
            sb = sb.Replace("™", "");

            return sb;

        }

        public string changeImage(string cadena, string filename)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder(cadena);

            sb.Replace(".EPS", ".jpg");
            sb.Replace(".TIF", ".jpg");
            sb.Replace(".AI", ".jpg");
            sb.Replace(".PSD", ".jpg");
            sb.Replace(".psd", ".jpg");
            sb.Replace(".ai", ".jpg");
            sb.Replace(".eps", ".jpg");
            sb.Replace(".tif", ".jpg");
            sb.Replace(filename + "-web-images", "img");

            return sb.ToString();

        }

        public string findEmail(string content, string file)
        {
            StringBuilder sb = new StringBuilder(content);

            int ini, med, fin;

            ini = fin = 0;

            string cad = "";

            for (int i = 0; i < sb.Length; i++)
                if (sb.ToString().IndexOf("@", i) > 0)
                {
                    med = sb.ToString().IndexOf("@", i);

                    for (int x = med; x > 0; x--)
                    {
                        if (char.IsWhiteSpace(sb.ToString()[x]) || sb.ToString()[x] == ':' || sb.ToString()[x] == '>')
                        {
                            ini = x + 1;
                            break;
                        }
                    }

                    for (int x = med; x <= sb.ToString().IndexOf("</p>", med); x++)
                    {
                        if (char.IsWhiteSpace(sb.ToString()[x]) || sb.ToString()[x] == '<' || sb.ToString()[x] == ',' || sb.ToString()[x] == ';')
                        {
                            fin = x;
                            break;
                        }
                    }

                    cad = sb.ToString().Substring(ini, (fin - ini));

                    sb.Replace(cad, "<a href= 'mailto:" + cad + "'>" + cad + "</a>");

                    fin = sb.ToString().IndexOf("</a>", fin);

                    i = fin;

                }
                else
                    break;

            return sb.ToString();

        }

        public string findUrl(string content, string file)
        {
            StringBuilder sb = new StringBuilder(content);

            int ini, fin;

            string cad = "";

            //int inicio = sb.ToString().IndexOf("<body>", 0);
            int inicio = sb.ToString().IndexOf("<", 0);

            //int inicio = 0;

            for (int x = inicio; x < sb.Length; x++)

                if (sb.ToString().IndexOf("www.", inicio) > 0)
                {
                    ini = sb.ToString().IndexOf("www.", inicio);
                    fin = sb.ToString().IndexOf("<", ini);

                    cad = sb.ToString().Substring(ini, (fin - ini));

                    sb.Replace(cad, "<a href='http://" + cad + "' target='_blank'>" + cad + "</a>");

                    fin = sb.ToString().IndexOf("</a>", fin);

                    x = fin;

                    inicio = fin;
                }
                else
                    break;


            return sb.ToString();

        }

        public string getString(string cadena, string token)
        {
            string cad;
            if (cadena.IndexOf(token) != -1)
            {
                cad = cadena.Substring(cadena.IndexOf(token), cadena.IndexOf("<", (cadena.IndexOf(token) + token.Length)) - cadena.IndexOf(token));
                return cad;

            }
            else
                return string.Empty;

        }

        public void saveHtmlFile(string destinationPath, string fileName, string fileContent)
        {
            if (string.IsNullOrEmpty(destinationPath))
                throw new ArgumentException("The destination path cannot be null or empty.");


            fileName = fileName.Replace("|", "");
            FileStream htmlFile = new FileStream(destinationPath + "\\" + fileName, FileMode.Create);
            StreamWriter sw = new StreamWriter(htmlFile, System.Text.Encoding.UTF8);
            sw.Write(fileContent);

            sw.Close();
            sw.Dispose();
        }
    }
}