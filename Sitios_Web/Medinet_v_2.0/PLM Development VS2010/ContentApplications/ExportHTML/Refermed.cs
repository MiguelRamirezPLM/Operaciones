using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Text;
using System.IO;

namespace ExportHTML
{
    public class Refermed
    {
        //static void Main(string[] args)
        //{
        //    string token = "<p class=\"t-tulo";
        //    string token2 = "<p class=\"forma-farmac-\">";
        //    string temp = "../../IndesignTemplate.htm";
        //    string archivo = "Producto,Archivo" + Environment.NewLine;
        //    string s = "";
        //    do
        //    {
        //        Console.WriteLine("Ingresa la ruta:");

        //        string path = Console.ReadLine();

        //        Console.WriteLine("Carpeta donde deseas guardar los archivos");

        //        string carp = Console.ReadLine();

        //        string filename = path.Substring(path.LastIndexOf("\\") + 1,
        //            path.IndexOf(".") - (path.LastIndexOf("\\") + 1));

        //        string path2 = path.Substring(0, path.LastIndexOf("\\") + 1);

        //        GetHTML filecontent = new GetHTML();

        //        string allcontent = filecontent.getHtmlContent(path);
        //        string template = filecontent.getHtmlContent((temp));

        //        string name, name2;

        //        int ini, next;
        //        int fin, med;
        //        int len;

        //        len = token.Length;

        //        next = 0;

        //        for (int i = 0; i < allcontent.Length; i++)
        //        {

        //            if (allcontent.IndexOf(token, next) != -1)
        //            {
        //                ini = allcontent.IndexOf(token, next);
        //                med = allcontent.IndexOf(">", ini);
        //                fin = allcontent.IndexOf("</p>", med);

        //                name2 = name = allcontent.Substring(med + 1, (fin - med) - 1);
        //                name = filecontent.quitAccentsFileName(name);
        //                name = filecontent.cleanName(name);

        //                next = allcontent.IndexOf(token, fin);

        //                System.Text.StringBuilder sb = new System.Text.StringBuilder(template);

        //                if (next > 0)
        //                {
        //                    string file = allcontent.Substring(ini, (next - ini));

        //                    file = filecontent.quitAccents(file);
        //                    file = filecontent.changeImage(file, filename);
        //                    file = filecontent.findEmail(file, filename);
        //                    file = filecontent.findUrl(file, filename);

        //                    file = file.Replace("<p class=\"medio-de-d-prod-nuevo\">producto nuevo</p>", "");
        //                    file = file.Replace("<p class=\"medio-de-d-prod-nuevo\">PRODUCTO NUEVO</p>", "");

        //                    file = file.Replace("<p class=\"medio-de-d-prod-nuevo\">Producto Nuevo</p>", "");
        //                    file = file.Replace("<p class=\"medio-de-d-prod-nuevo\">Producto nuevo</p>", "");

        //                    filecontent.setParameters(sb, "@@@Titulo@@@=" + name.Trim());
        //                    sb.Replace("@@@Contenido@@@", file);

        //                }
        //                else
        //                {
        //                    string file = allcontent.Substring(ini);

        //                    file = filecontent.quitAccents(file);
        //                    filecontent.setParameters(sb, "@@@Titulo@@@=" + name.Trim());//, "@@@Contenido@@@=" + file); 
        //                    sb.Replace("@@@Contenido@@@", file);

        //                    i = allcontent.Length;
        //                }

        //                if (File.Exists("C:/Refermed/Archivos/" + carp + "/" + name.Trim() + ".htm"))
        //                {
        //                    filecontent.saveHtmlFile("C:/Refermed/Archivos/" + carp + "/",
        //                   name.Trim() + "_" + i.ToString() + ".htm", sb.ToString());
        //                    archivo = archivo + name2 + "," + name.Trim() + "_" + i.ToString() + ".htm" + Environment.NewLine;
        //                }
        //                else
        //                {
        //                    filecontent.saveHtmlFile("C:/Refermed/Archivos/" + carp + "/",
        //                    name.Trim() + ".htm", sb.ToString());

        //                    archivo = archivo + name2 + "," + name.Trim() + ".htm" + Environment.NewLine;
        //                }

        //                name = "";

        //            }

        //        }



        //        filecontent.saveHtmlFile("C:/Refermed/Archivos/" + carp + "/", "Archivos_" + carp + ".csv", archivo);

        //        archivo = "Producto,Archivo" + Environment.NewLine;

        //        Console.WriteLine("El archivo ha sido segmentado.");

        //        Console.WriteLine("¿Deseas segmentar otro archivo? S/N");

        //        s = Console.ReadLine();

        //    } while (s.ToLower() == "s");

        //}

        static void Main(string[] args)
        {
            GetHTML load = new GetHTML();

            load.getFile2();
            //load.getHTMLContentWI();


            Console.WriteLine("Terminado...");
            Console.ReadLine();

        }
    }
}
