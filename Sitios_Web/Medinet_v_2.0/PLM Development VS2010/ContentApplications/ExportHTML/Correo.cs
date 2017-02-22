using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Text;
using System.IO;

namespace ExportHTML
{
    public class Correo
    {
        static void Main(string[] args)
        {
            string s = "";

            do
            {
                Console.WriteLine("Ingresa la ruta:");

                string path = Console.ReadLine();

                DirectoryInfo dirInfo = new DirectoryInfo(path);

                FileInfo[] fileInfo = dirInfo.GetFiles("*.html");

                GetHTML filecontent = new GetHTML();

                string content, name;

                for (int x = 0; x < fileInfo.Length; x++)
                {
                    content = filecontent.getHtmlContent(fileInfo[x].FullName);

                    name = fileInfo[x].Name;

                    content = filecontent.findUrl(content, name);

                    content = filecontent.findEmail(content, name);

                    filecontent.saveHtmlFile(path, name, content);

                }
           
                Console.WriteLine("Se agregaron links a todos los archivos de esta carpeta");

                Console.WriteLine("¿Deseas modificar más archivo? S / N");
 
                s = Console.ReadLine();

            } while (s.ToLower() == "s");

            }
    }
}
