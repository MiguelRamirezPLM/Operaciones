using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Text;
using System.IO;


namespace ExportHTML
{
    public class RemplazoCad
    {
        static void Main(string[] args)
        {
            string s = "";

            do
            {
                //Console.WriteLine("Ingresa la ruta:");

                string path = @"\\framirez001\CD\Src\PEV29\CD\source\src\prods\";// Console.ReadLine();

                //string path = @"C:\PEV\sub\prods\";// Console.ReadLine();
                //Console.WriteLine("Ingresa la etiqueta inicial a reemplazar:");

                string eini = "<p class=\"titulo\">";//Console.ReadLine();

                string file = "";

                //Console.WriteLine("Ingresa la etiqueta final a reemplazar:");

                string efin = "<p class=\"uso-en\">";// Console.ReadLine();

                //Console.WriteLine("Ingresa la etiqueta inicial de reemplazo:");

                //string esini = Console.ReadLine();

                //Console.WriteLine("Ingresa la etiqueta final de reemplazo:");

                //string esfin = Console.ReadLine();

                DirectoryInfo dirInfo = new DirectoryInfo(path);

                FileInfo[] fileInfo = dirInfo.GetFiles("*.htm");

                GetHTML filecontent = new GetHTML();

                string content, name;

                for (int x = 0; x < fileInfo.Length; x++)
                {
                    content = filecontent.getHtmlContent(fileInfo[x].FullName);
                    
                    name = fileInfo[x].Name;
                    
                    if (content.IndexOf(efin) > 0)
                    {
                        
                        //content = filecontent.replaceString(eini,efin,esini,esfin,content);

                        content = filecontent.replaceString3(eini, efin, content);

                        //content = filecontent.setDotted(eini, content, "<br />", efin);

                        filecontent.saveHtmlFile(path, name, content);
                    }
                    else
                        file = file + name + Environment.NewLine;

                }

                filecontent.saveHtmlFile(@"C:\PEV\sub\prods\", "sinUso.txt", file);

                Console.WriteLine("Se reemplazaron las etiquetas.");

                Console.WriteLine("¿Deseas modificar más archivos? S / N");

                s = Console.ReadLine();

            } while (s.ToLower() == "s");

        }
    }
}
