using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Text;
using System.IO;

namespace ExportHTML
{
    public class Interacciones
    {
        public static void Main(string[] args)
        {
            string s = "s";

            do{

                Console.WriteLine("Ingresa la ruta del archivo a segmentar:");

                string path = Console.ReadLine();

                string filename = path.Substring(path.LastIndexOf("\\") + 1,
                    path.IndexOf(".") - (path.LastIndexOf("\\") + 1));

                string token = "<p class=\"titulos-sales\">";
                
                string archivo = "Sustancia;Nombre" + Environment.NewLine;

                GetHTML filecontent = new GetHTML();

                string allcontent = filecontent.getHtmlContent(path);

                string text,sustancia;
                int ini,med,fin;

                ini = med = fin = 0;

                for (int i = 0; i < allcontent.Length; i++)
                {
                    if(allcontent.IndexOf(token,fin) > -1)
                    {
                        ini = allcontent.IndexOf(token, fin);
                        med = allcontent.IndexOf(">", ini);
                        fin = allcontent.IndexOf("</p>", med);

                        text = allcontent.Substring(ini, (fin - ini));
                        sustancia = allcontent.Substring(med + 1, (fin - (med+1)));



                        allcontent = allcontent.Replace(text, token + "<a name='" + filecontent.cleanName(sustancia.Trim()) + "' >" + sustancia + "</a>");

                        archivo = archivo + filecontent.cleanName(sustancia.Trim()) + ";" + sustancia.Trim() + Environment.NewLine;

                        i = fin;
                    }
                }

                filecontent.saveHtmlFile("C:/CAD/",
                               "Interacciones.htm", allcontent);

                filecontent.saveHtmlFile("C:/CAD/", "Interacciones" + ".txt", archivo);

                Console.WriteLine("El archivo ha sido segmentado.");

                Console.WriteLine("¿Deseas segmentar otro archivo? S/N");

                s= Console.ReadLine();


            }while(s.ToLower().Equals("s"));

        }
    }
}
