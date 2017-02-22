using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Text;
using System.IO;

namespace ExportHTML
{
    public class Terceras
    {
    //    static void Main(string[] args)
    //    {
    //        Console.WriteLine("Ruta del Archivo:");

    //        string path = Console.ReadLine();
    //        string temp = "../../IndesignTemplate.htm";
    //        string token = "<p class=\"t-tulo\">";

    //        GetHTML filecontent = new GetHTML();

    //        string allcontent = filecontent.getHtmlContent(path);
    //        string template = filecontent.getHtmlContent((temp));

    //        int ini, fin, len, med, next;
    //        string name;

    //        next = 0;

    //        len = token.Length;

    //        for (int i = 0; i < allcontent.Length; i++)
    //        {

    //            if (allcontent.IndexOf(token, next) != -1)
    //            {
    //                ini = allcontent.IndexOf(token, next);
    //                med = allcontent.IndexOf(">", ini);
    //                fin = allcontent.IndexOf("</p>", med);

    //                name = allcontent.Substring(med + 1, (allcontent.IndexOf("</p>", med) - (med+1)));
    //                name = filecontent.quitAccentsFileName(name);
    //                name = filecontent.cleanName(name);

    //                next = allcontent.IndexOf(token, fin);

    //                System.Text.StringBuilder sb = new System.Text.StringBuilder(template);

    //                if (next > 0)
    //                {
    //                    string file = allcontent.Substring(ini, (next - ini));

    //                    file = filecontent.quitAccents(file);

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


    //                if (File.Exists("C:/DEF Ecuador/Terceras/" + name.Trim() + ".html"))
    //                    filecontent.saveHtmlFile("C:/DEF Ecuador/Terceras/",
    //                    name.Trim() + "_" + i.ToString() + ".html", sb.ToString());

    //                else
    //                    filecontent.saveHtmlFile("C:/DEF Ecuador/Terceras/",
    //                    name.Trim() + ".html", sb.ToString());



    //                name = "";
    //            }

    //        }

    //        Console.WriteLine("Terminado....");
    //        Console.ReadLine();
    //    }

        static void Main(string[] args)
        {
            GetHTML load = new GetHTML();

            load.getFile();

            Console.WriteLine("Terminado...");
            Console.ReadLine();

        }

        }

      
}
