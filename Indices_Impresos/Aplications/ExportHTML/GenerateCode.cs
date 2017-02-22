using System;
using System.Collections.Generic;
using System.Text;

namespace ExportHTML
{
    public class GenerateCode
    {

        public static void Main(string[] args)
        {
            int counter = 100;

            GetHTML funct = new GetHTML();

            for (int x = 0; x < 500000; x++)
            {
                funct.insertCode(counter);
                counter++;
            }

            Console.WriteLine("Terminado");
            Console.ReadLine();

        }
        
    }
}
