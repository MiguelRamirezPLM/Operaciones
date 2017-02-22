using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Text;
using System.IO;

namespace ExportHTML
{
    public class ReadFile
    {
        static void Main(string[] args)
        {
            string s ="";

            do
            {
                Console.WriteLine("Ingresa la ruta:");
                string path = Console.ReadLine();

                StreamReader sr = File.OpenText(path);

                string linea = "";



                linea = sr.ReadLine();
                int cont = 1;

                while (linea != null)
                {


                    if (linea.Length == 1024)
                    {
                        Console.WriteLine("La linea " + cont + " tiene el tamaño correcto: " + linea.Length + " caracteres.");
                    }
                    else
                    {
                        if (linea.Substring(0, 3).Equals("PAY"))
                            Console.WriteLine("La linea " + cont + " no tiene el tamaño correcto: " + linea.Length + " caracteres.");
                        else
                            Console.WriteLine("El TRL es de: " + linea.Length + " caracteres");
                    }
                    linea = sr.ReadLine();
                    cont++;
                }

                Console.WriteLine("¿Deseas validar otro archivo? S / N");
                s =Console.ReadLine();
            } while (s.ToUpper() == "S");
            
        }


    }
}
