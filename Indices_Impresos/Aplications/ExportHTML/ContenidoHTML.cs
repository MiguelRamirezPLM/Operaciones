using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Text;
using System.IO;

namespace ExportHTML
{
    public class ContenidoHTML
    {
        static void Main(string[] args)
        {
            //string val = "s";
            //string path = @"C:\Peru\HTMLS\Files\";
            string path = "\\\\195.192.2.17\\Proyectos\\Discos\\México\\DEIA 22\\source\\src\\PRODUCTOS\\";
            //string path = "C:\\html";
            GetHTML fileContent = new GetHTML();

            GetHTML.Instance.updateContent(path);

          


          //  do
            //{
            //    Console.WriteLine("Escribe el archivo a cargar:");

            //    string file = Console.ReadLine();

            //    StringBuilder content = new StringBuilder(fileContent.getHtmlContent(path + file));

            //    //content.Replace("<p class=\"medio-de-d-prod-nuevo\">INFORMACI&Oacute;N REVISADA</p>", "");
            //    //content.Replace("<p class=\"medio-de-d-prod-nuevo\">INFORMACI&Oacute;N NUEVA</p>", "");

            //    //content.Replace("<p class=\"medio-de-d-prod-nuevo\">informaci&oacute;n revisada</p>", "");
            //    //content.Replace("<p class=\"medio-de-d-prod-nuevo\">informaci&oacute;n nueva</p>", "");

            //    //content.Replace("<p class=\"medio-de-d-prod-nuevo\">informaci&oacute;n REVISADA</p>", "");
            //    //content.Replace("<p class=\"medio-de-d-prod-nuevo\">informaci&oacute;n NUEVA</p>", "");
                
            //    //Console.WriteLine("Ingresa el productId:");

            //    //string proId = Console.ReadLine();

            //    //Console.WriteLine("Ingresa el pharmaformId:");

            //    //string pharmaId = Console.ReadLine();

            //    Console.WriteLine("Ingresa el edProdId:");

            //    string edProdId = Console.ReadLine();

            //    Console.WriteLine("Ingresa el editionId:");

            //    string editionId= Console.ReadLine();

            //    Console.WriteLine("Ingresa el sectionId:");

            //    string sectionId = Console.ReadLine();

                
            //    Console.WriteLine("Ingresa el productId:");

            //    string productId = Console.ReadLine();

               



            //    content.Replace("'", "\"");

            //   // fileContent.loadHTML(proId, pharmaId, content.ToString());
            //    fileContent.updatehtmlContent(edProdId, edProdId, sectionId, productId, content.ToString());

            //    Console.WriteLine("¿Deseas cargar otro archivo? S / N");
            //    val = Console.ReadLine();
            //} while (val.ToLower() == "s");



        }
    }
}
