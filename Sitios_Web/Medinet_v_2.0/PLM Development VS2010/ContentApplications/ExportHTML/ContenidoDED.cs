using System;
using System.Collections.Generic;
using System.Text;

namespace ExportHTML
{
    public class ContenidoDED
    {

        static void Main(string[] args)
        {
            string path;

            System.Console.WriteLine("Escribe la ruta de los archivos:");
            path = System.Console.ReadLine();

            //GetHTML.Instance.updateContent(path);
            GetHTML.Instance.createProducts(path);

            System.Console.WriteLine("Terminado...");
            System.Console.ReadLine();





        }

    }
}
