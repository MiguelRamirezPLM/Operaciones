using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Text;
using System.IO;

namespace ExportHTML
{
    class pediatrico
    {
        static void Main(string[] args)
        {
            string path = @"C:\angel.parra\HTMLs\";
            GetHTML filecontent = new GetHTML();
            string valid = "";
            do
            {
                //Console.WriteLine("Ingresa la ruta:");

                //string path = Console.ReadLine();

                //string filename = path.Substring(path.LastIndexOf("\\") + 1,
                //    path.LastIndexOf(".") - (path.LastIndexOf("\\") + 1));


                //string allContent = filecontent.getHtmlContent(path);
                //allContent = filecontent.quitAccents(allContent);
                //allContent = filecontent.changeImage(allContent, filename);
                //allContent = filecontent.findEmail(allContent, filename);
                //allContent = filecontent.findUrl(allContent, filename);

                //filecontent.saveHtmlFile("C:/angel.parra/PEDIATRICO_HTML", filename + ".htm", allContent);

                ////////////////////////////////////////////////////////////// 

                GetHTML fileContent = new GetHTML();

                Console.WriteLine("Escribe el archivo a cargar:");

                string file = Console.ReadLine();

                StringBuilder content = new StringBuilder(fileContent.getHtmlContent(path + file));
                Console.WriteLine("Ingresa el productId:");

                string proId = Console.ReadLine();

                Console.WriteLine("Ingresa el pharmaformId:");

                string pharmaId = Console.ReadLine();

               // Console.WriteLine("Ingresa la pagina:");

                //string page = Console.ReadLine();
                content.Replace("'", "\"");

                //fileContent.loadHTML2(proId, pharmaId, content.ToString());





                //////////////////////////////////////////////////////////////////////


                Console.WriteLine("¿Deseas replasar  otro archivo? S/N");

                valid = Console.ReadLine();

            } while (valid.ToLower().Equals("s"));

        }
    }
}
