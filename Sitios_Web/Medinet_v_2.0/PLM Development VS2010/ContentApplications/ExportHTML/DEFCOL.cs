using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Text;
using System.IO;

namespace ExportHTML
{
    public class DEFCOL
    {
        static void Main(string[] args)
        {
            string valid = "";
            do
            {
                Console.WriteLine("Ingresa la ruta del archivo a segmentar:");

                string path = Console.ReadLine();

                string filename = path.Substring(path.LastIndexOf("\\") + 1,
                    path.IndexOf(".") - (path.LastIndexOf("\\") + 1));

                string temp = "../../IndesignTemplate.htm";
                string token = "<p class=\"t-tulo";
                string nuevo = "<p class=\"t-tuloprodnuevo\"";
                string nuevo2 = "<p class=\"t-tuloprodnuevo2\"";
                string token2 = "<p class=\"forma-farmac-\">";
                string token3 = "<p class=\"antetitulo\">PRODUCTO NUEVO</p>";

                string archivo = "ProdId,NombreBD,NombreHTML,HTML,FF" + Environment.NewLine;

                GetHTML filecontent = new GetHTML();

                string allcontent = filecontent.getHtmlContent(path);
                string template = filecontent.getHtmlContent((temp));
                string name, name2,pf,pn,pf2,namebd,pharmaforms;

                int ini, ini2, next;
                int fin, fin2,med,med2;
                int len, len2,id;

                len = token.Length;
                len2 = token2.Length;

                
                next = 0;

                for (int i = 0; i < allcontent.Length; i++)
                {
                    if (allcontent.IndexOf(token, next) != -1)
                    {
                        ini = allcontent.IndexOf(token, next);

                        med = allcontent.IndexOf(">", (ini));

                        pn = allcontent.Substring(ini, (med - ini));

                        fin = allcontent.IndexOf("</p>", ini + len);

                        name = allcontent.Substring(med + 1, (fin - (med + 1)));

                        name2 = allcontent.Substring(med + 1, (fin - (med +1)));

                        name = filecontent.quitAccentsFileName(name);

                        name = filecontent.cleanName(name);

                        if (name.Length > 20)
                            name = name.Substring(0, 20);

                        name2 = filecontent.cleanName(name2);

                        DataTable dt = filecontent.checkName(name2.Trim());

                        if (dt.Rows.Count == 0)
                        {
                            id = 0;
                            namebd = "";
                        }
                        else
                        {
                            id = Convert.ToInt32(dt.Rows[0]["ProductId"]);
                            namebd = dt.Rows[0]["Brand"].ToString();
                        }
                        pharmaforms = filecontent.getPharmaForms(id);

                        ini2 = allcontent.IndexOf(token2, next);
                        if (ini2 > 0 && ini2 < allcontent.IndexOf(token, fin))
                        {
                            med2 = allcontent.IndexOf(">", ini2);
                            fin2 = allcontent.IndexOf("<", med2);
                            pf = allcontent.Substring(med2 + 1, (fin2 - med2) - 1);
                            pf2 = pf;
                            pf = filecontent.quitAccentsFileName(pf);
                            pf = filecontent.cleanName(pf);
                            pf2 = filecontent.cleanName(pf2);
                        }
                        else
                        {
                            pf = string.Empty;
                            pf2 = pf;
                        }
                        next = allcontent.IndexOf(token, fin);

                        System.Text.StringBuilder sb = new System.Text.StringBuilder(template);

                        if (next > 0)
                        {
                            string file = allcontent.Substring(ini, (next - ini));

                            file = filecontent.quitAccents(file);
                            file = filecontent.changeImage(file, filename);
                            file = filecontent.findEmail(file, filename);
                            file = filecontent.findUrl(file, filename);
                            filecontent.setParameters(sb, "@@@Titulo@@@=" + (name.Trim()));

                            if (pn == nuevo || pn == nuevo2)
                            {
                                file = file.Replace("<p class=\"antetitulo\">PRODUCTO NUEVO</p>", "");
                                file = file.Replace("<p class=\"antetitulo\">producto nuevo</p>", "");
                                sb.Replace("@@@Contenido@@@", token3 + file);
                            }
                            else
                            {
                                file = file.Replace("<p class=\"antetitulo\">PRODUCTO NUEVO</p>", "");
                                file = file.Replace("<p class=\"antetitulo\">producto nuevo</p>", "");
                                sb.Replace("@@@Contenido@@@", file);
                            }
                        }
                        else
                        {
                            string file = allcontent.Substring(ini);

                            file = filecontent.quitAccents(file);
                            file = filecontent.changeImage(file, filename);
                            file = filecontent.findEmail(file, filename);
                            file = filecontent.findUrl(file, filename);
                            filecontent.setParameters(sb, "@@@Titulo@@@=" + (name.Trim()));
                            if (pn == nuevo || pn == nuevo2)
                            {
                                file = file.Replace("<p class=\"antetitulo\">PRODUCTO NUEVO</p>","");
                                file = file.Replace("<p class=\"antetitulo\">producto nuevo</p>", "");
                                sb.Replace("@@@Contenido@@@", token3 + file);
                            }
                            else
                            {
                                file = file.Replace("<p class=\"antetitulo\">PRODUCTO NUEVO</p>", "");
                                file = file.Replace("<p class=\"antetitulo\">producto nuevo</p>", "");
                                sb.Replace("@@@Contenido@@@", file);
                            }
                            i = allcontent.Length;
                        }

                        if (File.Exists("C:/Colombia/HTMLS/" + filename + "/" +
                                    name.Trim().ToLower() + "_" + pf.Trim().ToLower() + ".htm"))
                        {
                            filecontent.saveHtmlFile("C:/Colombia/HTMLS/" + filename + "/",
                               name.Trim().ToLower() + "_" + pf.Trim().ToLower() + "_" + i.ToString() + ".htm", sb.ToString());

                            //archivo = archivo + name2 + "," + pf2 + "," + name.Trim().ToLower() + "_" + pf.Trim().ToLower() + "_" + i.ToString() + ".htm" + Environment.NewLine;
                            //string archivo = "ProdId,NombreBD,NombreHTML,HTML,FF" + Environment.NewLine;
                            archivo = archivo + id + ",[" + namebd.Trim() + "]," + name2
                                + "," + (name.Trim().ToLower() + "_" + pf.Trim().ToLower() + "_" + i.ToString() + ".htm") + "," + pharmaforms.Trim() + Environment.NewLine;
                        }
                        else
                        {

                            filecontent.saveHtmlFile("C:/Colombia/HTMLS/" + filename + "/",
                            name.Trim().ToLower() + "_" + pf.Trim().ToLower() + ".htm", sb.ToString());

                            //archivo = archivo + name2 + "," + pf2 + "," + name.Trim().ToLower() + "_" + pf.Trim().ToLower() + ".htm" + Environment.NewLine;

                            archivo = archivo + id + ",[" + namebd.Trim() + "]," + name2
                                + "," + (name.Trim().ToLower() + "_" + pf.Trim().ToLower() + ".htm") + "," + pharmaforms.Trim() + Environment.NewLine;
                        }
                        pf = "";
                        pf2 = "";
                        name = "";
                        name2 = "";
                        pn = "";
                    }
                }

                filecontent.saveHtmlFile("C:/Colombia/CSV/", "Archivos_" + filename + ".csv", archivo);

                Console.WriteLine("El archivo ha sido segmentado.");

                Console.WriteLine("¿Deseas segmentar otro archivo? S/N");

                valid = Console.ReadLine();

            } while (valid.ToLower().Equals("s"));


        }
        
    }
}
