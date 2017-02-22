using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Text;
using System.IO;

namespace ExportHTML
{
    public class UpdateContent
    {
        static void Main(string[] args)
        {
            GetHTML load = new GetHTML();

            load.getContent();

            //load.getUsers();


            Console.WriteLine("Terminado...");
            Console.ReadLine();

        }

    }
}
