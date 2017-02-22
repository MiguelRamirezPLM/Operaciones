using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Text;
using System.IO;

namespace ExportHTML
{
    public class generateTXT
    {
        
        public static void Main(string[] args)
        {
            string s,s1 ;
            
            do
            {
                Console.WriteLine("Ingresa el nombre del Laboratorio:");
                s1 = Console.ReadLine();

                s1 = s1.Replace("_", " ");
                
                GetHTML filecontent = new GetHTML();
                

                //string token = "<img src='../awmmenupath.gif' alt='' name='awmMenuPathImg-tecnatint01' id='awmMenuPathImg-tecnatint01'>" +
                //    "<script type='text/javascript'>var MenuLinkedBy='AllWebMenus [2]', awmBN='524'; awmAltUrl='';</script>" +
                //    "<script src='@@@file@@@.js' language='JavaScript1.2' type='text/javascript'></script>" +
                //    "<script type='text/javascript'>awmBuildMenu();</script><br><br><br><br>";

                DataTable dt = filecontent.getHTML(s1);

                for (int x = 0; x < dt.Rows.Count; x++)
                {
                    string file = filecontent.getHtmlContent("C:/DEAQ 2010/src/prods/" + dt.Rows[x]["ARCHIVO"].ToString());

                    string name = dt.Rows[x]["ARCHIVO"].ToString();

                    name = name.Replace(".htm", "");

                    file = file.Replace("src=\"img/", "src=\"img/" + s1 + "/");

                    //file = token + file;

                    filecontent.saveHtmlFile("C:/DEAQ 2010/src/prods/", name + ".htm", file);

                }

                Console.WriteLine("¿Deseas cambiar otro lab? S/N");
                s = Console.ReadLine();

            }while(s.ToLower().Equals("s"));
            //Console.WriteLine("Archivos creados....");
            //Console.ReadLine();
        }
    }

            
}
