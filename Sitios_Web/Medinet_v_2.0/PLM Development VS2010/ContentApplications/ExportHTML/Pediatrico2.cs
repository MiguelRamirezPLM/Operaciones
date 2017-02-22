using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Text;
using System.IO;

namespace ExportHTML
{
    class Pediatrico2
    {
        static void Main(string[] args)
        {
            GetHTML load = new GetHTML();

            load.getFile2();

            Console.WriteLine("Terminado...");
            Console.ReadLine();
        }
    }
}
