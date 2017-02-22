using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Text;
using System.IO;

namespace ExportHTML
{
    public class TercerasCAD
    {
        public static void Main(string[] args)
        {
            string s, s1, file;
            s = "s";

            do
            {
                Console.WriteLine("Ingresa el nombre del Laboratorio:");
                s1 = Console.ReadLine();

                string temp = "../../TercerasCAD.htm";

                GetHTML filecontent = new GetHTML();

                string template = filecontent.getHtmlContent((temp));

                string empresa = filecontent.getAddress(s1);

                //empresa = filecontent.findEmail(empresa, s1);
                //empresa = filecontent.findUrl(empresa, s1);
                //empresa = filecontent.changeImage(empresa, s1);


                string prods = filecontent.getProds(s1);

                System.Text.StringBuilder sb = new System.Text.StringBuilder(template);

                sb.Replace("@@@Empresa@@@", empresa);

                sb.Replace("@@@Productos@@@", prods);

                file = filecontent.Laboratory;

                file = filecontent.quitAccentsFileName(file);

                file = filecontent.cleanName(file);

                filecontent.saveHtmlFile("C:/CAD 40/Terceras/HTMLS/", file.ToLower() + ".htm", sb.ToString());

                Console.WriteLine("El archivo ha sido guardado.");

                //Console.WriteLine("¿Deseas segmentar otro archivo? S/N");

                //s = Console.ReadLine();

            } while (s.ToLower().Equals("s"));



        }
    }
}
