using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Text;
using System.IO;

namespace ExportHTML
{
    public class LinksDEAQ
    {
        public static void Main(string[] args)
        {
            string s;

            GetHTML filecontent = new GetHTML();

            do
            {
                Console.WriteLine("Ingresa la ruta de los archivos:");

                string path = Console.ReadLine();

                DirectoryInfo dirInfo = new DirectoryInfo(path);

                FileInfo[] fileInfo = dirInfo.GetFiles("*.htm");

                for (int y = 0; y < fileInfo.Length; y++)
                {
                    string name = fileInfo[y].Name;

                    string file = filecontent.getHtmlContent(fileInfo[y].FullName);

                    file = file.Substring(file.IndexOf("<!DOCTYPE"));

                    int fin = file.LastIndexOf("</body>") - 1;

                    string link = filecontent.getProductId(name);

                    file = file.Insert(fin, link);
                                       

                    filecontent.saveHtmlFile(path, name, file);
                    
                }
                    

                Console.WriteLine("Se han modificado los archivos.");
                Console.WriteLine("Deseas modificar mas? S/N");
                s=Console.ReadLine();

            } while (s.ToLower().Equals("s"));
            
        }
        
    }
}
