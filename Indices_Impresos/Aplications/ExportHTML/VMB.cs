using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Text;
using System.IO;

namespace ExportHTML
{
    public class VMB
    {
        //static void Main(string[] args)
        //{
        //    string valid = "";
        //    do
        //    {
        //        Console.WriteLine("Ingresa la ruta del archivo a segmentar:");

        //        string path = Console.ReadLine();

        //        string filename = path.Substring(path.LastIndexOf("\\") + 1,
        //            path.IndexOf(".") - (path.LastIndexOf("\\") + 1));

        //        string fileToken = "<p class=\"t-tulo";

        //        string baseprop = "<p class=\"base-de-propaganda\">";

        //        string dataToken = "<p class=\"tabla-presentaciones\">";

        //        string archivo = "Substance,Base Prop,Lab,PharmaForm,Presentation,File" + Environment.NewLine;

        //        string temp = "../../IndesignTemplate.htm";

        //        GetHTML filecontent = new GetHTML();

        //        string allContent = filecontent.getHtmlContent(path);
        //        string template = filecontent.getHtmlContent((temp));

        //        string substance, bprop, fileName,data;

        //        int ini, ini2,ini3, med, med2,med3, fin, fin2,fin3, next;
        //        int len, len2;

        //        len = fileToken.Length;
        //        len2 = dataToken.Length;

        //        next = 0;

        //        for (int i = 0; i < allContent.Length; i++)
        //        {
        //            if (allContent.IndexOf(fileToken, next) != -1)
        //            {
        //                ini = allContent.IndexOf(fileToken, next);
        //                med = allContent.IndexOf(">", ini);
        //                fin = allContent.IndexOf("</p>", med);

        //                substance = allContent.Substring(med + 1, fin - (med + 1));

        //                fileName = filecontent.quitAccentsFileName(substance);

        //                ini2 = allContent.IndexOf(baseprop, next);

        //                med2 = allContent.IndexOf(">", ini2);
        //                fin2 = allContent.IndexOf("</p>", med2);

        //                if (ini2 < fin2)
        //                {
        //                    bprop = allContent.Substring(med2 + 1, fin2 - (med2 + 1));
                            
        //                    bprop = bprop.Replace(",", "_");
        //                }
        //                else
        //                    bprop = "";

        //                next = allContent.IndexOf(fileToken, fin);

        //                StringBuilder sb = new StringBuilder(template);

        //                string file;

        //                if (next != -1)
        //                    file = allContent.Substring(ini, (next - ini));

        //                else
        //                {
        //                    file = allContent.Substring(ini);
        //                    i = allContent.Length;
        //                }
        //                file = filecontent.quitAccents(file);
        //                file = filecontent.changeImage(file, filename);
        //                file = filecontent.findEmail(file, filename);
        //                file = filecontent.findUrl(file, filename);

        //                fin3 = 0;
        //                for (int x = 0, y = 0; x < file.Length; x++)
        //                {
        //                    if (file.IndexOf(dataToken, fin3) != -1)
        //                    {
        //                        ini3 = file.IndexOf(dataToken, fin3);
        //                        med3 = file.IndexOf(">", ini3);
        //                        fin3 = file.IndexOf("</p>", med3);

        //                        data = file.Substring(med3 + 1, fin3 - (med3 + 1));

        //                        if (!data.Equals("Laboratorio") && !data.Equals("Forma farmac&eacute;utica") && !data.Equals("Presentaci&oacute;n"))
        //                        {
        //                            switch (y)
        //                            {
        //                                case 0:
        //                                    archivo = archivo + substance + "," + bprop + "," + filecontent.addAccents(data);
        //                                    y++;
        //                                    break;
        //                                case 1:
        //                                    archivo = archivo + "," + filecontent.addAccents(data);
        //                                    y++;
        //                                    break;
        //                                case 2:
        //                                    archivo = archivo + "," + filecontent.addAccents(data) + "," + fileName + ".htm" + Environment.NewLine;
        //                                    y = 0;
        //                                    break;
        //                            }
        //                        }
        //                    }
        //                    else
        //                        fin3 = file.Length;
        //                }

        //                filecontent.setParameters(sb, "@@@Titulo@@@=" + fileName);
        //                sb.Replace("@@@Contenido@@@", file);

        //                if (File.Exists("C:/VMB/HTMLS/" + substance + ".htm"))
        //                    filecontent.saveHtmlFile("C:/VMB/HTMLS/", fileName + "_" + i + ".htm", sb.ToString());
        //                else
        //                    filecontent.saveHtmlFile("C:/VMB/HTMLS/", fileName + ".htm", sb.ToString());

        //                i = next == -1 ? i : next;

        //                substance = "";
        //                bprop = "";

        //            }
        //            else
        //                i = allContent.Length;

        //        }

        //        filecontent.saveLogFile("C:/VMB/", "EmpateProds.csv", archivo);

        //        Console.WriteLine("El archivo ha sido segmentado.");

        //        Console.WriteLine("¿Deseas segmentar otro archivo? S/N");

        //        valid = Console.ReadLine();

        //    } while (valid.ToLower().Equals("s"));


        //}

        static void Main(string[] args)
        {
            GetHTML load = new GetHTML();

            load.getFile();

            Console.WriteLine("Terminado...");
            Console.ReadLine();
        }


    }
}
