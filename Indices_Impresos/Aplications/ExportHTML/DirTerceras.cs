using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Text;
using System.IO;

namespace ExportHTML
{
    public class DirTerceras
    {

        static void Main(string[] args)
        {
            DirectoryInfo dirInfo = new DirectoryInfo(@"C:\DEF Ecuador\Terceras\");

            FileInfo[] fileInfo = dirInfo.GetFiles("*.html");

            string token = "<p class=\"direcci-n\">";

            string token2 = "</p>";

            string token3 = "<img src=\"LAB35-web-images/";

            int ini, med, fin, next,id;
            int ini2,med2,fin2;
            string dir, img;
            fin = fin2 = 0;

            img = "";

            dir = "";

            GetHTML filecontent = new GetHTML();
            
            for (int x = 0; x < fileInfo.Length; x++)
            {
               string name = fileInfo[x].Name.Substring(0, (fileInfo[x].Name.IndexOf(".")));

               string content = filecontent.getHtmlContent(fileInfo[x].FullName);

               next = 0;

               for (int z = 0; z < content.Length; z++)
               {
                   if (content.IndexOf(token, next) > 0)
                   {
                       do
                       {
                           ini = content.IndexOf(token, next);
                           med = content.IndexOf(">", ini);
                           fin = content.IndexOf(token2, med);

                           dir = dir + " " + content.Substring(ini, (fin - (ini)));

                           next = fin;
                       } while (content.IndexOf(token, fin) > 0);
                       
                       //dir = dir.Replace("<br />"," ");

                       for (int i = 0; i < dir.Length; i++)
                       {
                           if (dir.IndexOf(token3, i) > 0)
                           {
                               ini2 = dir.IndexOf(token3, i);
                               med2 = ini2 + token3.Length;
                               fin2 = dir.IndexOf("/>", med2);

                               if (img != "")
                                   img = img + "," + dir.Substring(med2, (dir.IndexOf("\"", med2) - med2));
                               else
                                   img = img + dir.Substring(med2, (dir.IndexOf("\"", med2) - med2));

                               string imgTxt = dir.Substring(ini2, ((fin2 + 2) - ini2));

                               dir = dir.Replace(imgTxt, " ");

                           }

                       }

                       next = fin;

                   }
               }

               id = filecontent.insertDescrip(name, dir, img);
               dir = "";
               img = "";
               if (id != 0)
                   Console.WriteLine("Se ingreso información de:" + name);
    
            }

            Console.WriteLine("Terminado...");
            Console.ReadLine();
        }



    }
}
