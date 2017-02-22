using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Text;
using System.IO;

namespace ExportHTML
{
    public class DEACI
    {

        static void Main(string[] args)
        {
            string token = "<p class=\"t3-empresa\">";//""<p class=\"x3-nombre-producto\">""<p class=\"x14-pie-empresa\">;
            string token2 = "<p class=\"x14-pie-empresa\">";
            string s = "";

            do
            {
                Console.WriteLine("Ingresa la ruta:");

                string path = Console.ReadLine();

                DirectoryInfo dirInfo = new DirectoryInfo(path);

                FileInfo[] fileInfo = dirInfo.GetFiles("*.html");

                GetHTML filecontent = new GetHTML();

                for (int x = 0; x < fileInfo.Length; x++)
                {
                    string name = fileInfo[x].Name.Substring(0, (fileInfo[x].Name.IndexOf(".")));

                    string content = filecontent.getHtmlContent(fileInfo[x].FullName);

                    Console.WriteLine("Archivo:" + name + ".html");

                    content=filecontent.replaceIMGPath(token, content.ToString());

                    content = filecontent.replaceIMGPath(token2, content.ToString());

                    content = filecontent.changeImage(content.ToString(), name);

                    content = filecontent.findUrl(content.ToString(), name);

                    content = filecontent.findEmail(content.ToString(), name);

                    filecontent.saveHtmlFile(path, name + ".html", content.ToString());

                    Console.WriteLine("Archivo Modificado.");

                }

                Console.WriteLine("Los archivos han sido modificados.");

                Console.WriteLine("¿Deseas modificar más? S/N");

                s = Console.ReadLine();




            } while (s.ToLower() == "s");

        }


    }
}
