using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace ExportHTML
{
    public class DEAQ
    {
        static void Main(string[] args)
        {
            string valid = "";
            do
            {
                Console.WriteLine("Ingresa la ruta del archivo a segmentar:");

                string path = Console.ReadLine();

                string filename = path.Substring(path.LastIndexOf("\\") + 1,
                    path.LastIndexOf(".") - (path.LastIndexOf("\\") + 1));

                filename = filename.Replace("_", "");


                string path2 = path.Substring(0, path.LastIndexOf("\\") + 1);

                string temp = "../../TemplateDEAQ.htm";
                //string token = "<p class=\"t-tulo\">";
                //string token2 = "<p class=\"forma-farmac-\"><span class=\"cursiva\">";

                string token = "<p class=\"t-tulo\"><span class=\"boldcursiva\">";
                string token2 = "<p class=\"forma-farmac-\"><span class=\"cursiva\">";  

                string archivo = "Producto,Archivo" + Environment.NewLine;

                GetHTML filecontent = new GetHTML();

                string allcontent = filecontent.getHtmlContent(path);
                string template = filecontent.getHtmlContent((temp));
                string name, name2,pf,pf2;

                int ini, ini2, next,id;
                int fin, fin2,med;
                int len, len2;

                len = token.Length;
                len2 = token2.Length;


                next = 0;

                for (int i = 0; i < allcontent.Length; i++)
                {

                    if (allcontent.IndexOf(token, next) != -1)
                    {
                        ini = allcontent.IndexOf(token, next);

                        med = allcontent.IndexOf(">", (ini));

                        fin = allcontent.IndexOf("</p>", ini + len);

                        name = allcontent.Substring(med + 1, (fin - (med + 1)));

                        name2 = allcontent.Substring(med + 1, (fin - (med +1)));

                        name = filecontent.quitAccentsFileName(name);

                        name = filecontent.cleanName(name);

                        name2 = filecontent.cleanName(name2);

                        ini2 = allcontent.IndexOf(token2, next);
                        fin2 = allcontent.IndexOf("<", ini2 + len2);

                        pf = allcontent.Substring(ini2 + len2, (fin2 - (ini2 + len2)));

                        pf = filecontent.quitAccentsFileName(pf);

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
                            sb.Replace("@@@Contenido@@@", file);
                        }
                        else
                        {
                            string file = allcontent.Substring(ini);

                            file = filecontent.quitAccents(file);
                            file = filecontent.changeImage(file, filename);
                            file = filecontent.findEmail(file, filename);
                            file = filecontent.findUrl(file, filename);
                            filecontent.setParameters(sb, "@@@Titulo@@@=" + (name.Trim()));
                            sb.Replace("@@@Contenido@@@", file);

                            i = allcontent.Length;
                        }
                        id = filecontent.getProIDDEAQ(name2.TrimStart().TrimEnd(),filename);

                        if (id == 0)
                        {
                            if (File.Exists("C:/DEAQ 20/HTMLS/semillas/" +
                                       name.Trim().ToLower() + ".htm"))
                            {
                                sb.Replace("@@@file@@@", name.Trim().ToLower() + "_" + i.ToString());
                                
                                filecontent.saveHtmlFile("C:/DEAQ 20/HTMLS/semillas/",
                                   name.Trim().ToLower() + "_" + i.ToString() + ".htm", sb.ToString());

                                archivo = archivo + name2 + "," + name.Trim().ToLower() + "_" + i.ToString() + ".htm" + Environment.NewLine;
                            }
                            else
                            {
                                sb.Replace("@@@file@@@", name.Trim().ToLower());

                                filecontent.saveHtmlFile("C:/DEAQ 20/HTMLS/semillas/",
                                name.Trim().ToLower() + ".htm", sb.ToString());

                                archivo = archivo + name2 + "," + name.Trim().ToLower() + ".htm" + Environment.NewLine;
                            }
                        }
                        else
                        {
                            if (File.Exists("C:/DEAQ 20/HTMLS/semillas/" +
                                       name.Trim().ToLower() + ".htm"))
                            {
                                sb.Replace("@@@file@@@", name.Trim().ToLower() + "_" + i.ToString());
                                
                                filecontent.saveHtmlFile("C:/DEAQ 20/HTMLS/semillas/",
                                   name.Trim().ToLower() + "_" + i.ToString() + ".htm", sb.ToString());

                                archivo = archivo + name2 + "," + name.Trim().ToLower() + "_" + i.ToString() + ".htm" + "," + id  + Environment.NewLine;
                            }
                            else
                            {
                                sb.Replace("@@@file@@@", name.Trim().ToLower());

                                filecontent.saveHtmlFile("C:/DEAQ 20/HTMLS/semillas/",
                                name.Trim().ToLower() + ".htm", sb.ToString());

                                archivo = archivo + name2 + "," + name.Trim().ToLower() + ".htm" + "," + id + Environment.NewLine;
                            }
                        }

                        pf = "";
                        name = "";
                        name2 = "";
                    }
                }

                filecontent.saveHtmlFile("C:/DEAQ 20/CSV/", "ArchivosSE_" + filename + ".csv", archivo);

                Console.WriteLine("El archivo ha sido segmentado.");

                Console.WriteLine("¿Deseas segmentar otro archivo? S/N");

                valid = Console.ReadLine();

            } while (valid.ToLower().Equals("s"));


        }


    }
}
