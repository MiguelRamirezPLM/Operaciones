using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Text;
using System.IO;

namespace ExportHTML
{
    public class TercerasDEAQ
    {
        //public static void Main(string[] args)
        //{
        //    string s,s1,file;

        //    do
        //    {
        //        Console.WriteLine("Ingresa el nombre del Laboratorio:");
        //        s1 = Console.ReadLine();

        //        string temp = "../../TercerasDEAQ.htm";
                
        //        GetHTML filecontent = new GetHTML();

        //        string template = filecontent.getHtmlContent((temp));

        //        string empresa = filecontent.getAddress(s1);

        //        empresa = filecontent.findEmail(empresa,s1);
        //        empresa = filecontent.findUrl(empresa, s1);

        //        string agro = filecontent.getProdsAgro(s1);

        //        string orga = filecontent.getProdsOrg(s1);

        //        System.Text.StringBuilder sb = new System.Text.StringBuilder(template);

        //        sb.Replace("@@@Empresa@@@", empresa);

        //        sb.Replace("@@@Productos@@@", agro + orga);

        //        file = filecontent.Laboratory;

        //        file = filecontent.quitAccentsFileName(file);

        //        file = filecontent.cleanName(file);

        //        filecontent.saveHtmlFile("C:/DEAQ 2010/src/labs/",file.ToLower() + ".htm", sb.ToString());

        //        Console.WriteLine("El archivo ha sido guardado.");

        //        Console.WriteLine("¿Deseas segmentar otro archivo? S/N");

        //        s = Console.ReadLine();
                                                
        //    } while (s.ToLower().Equals("s"));



        //}

        public static void Main(string[] args)
        {
            string s,file;
            string token = "<p class=\"normal-1\"><span class=\"bold\">";
            s = "s";
            do
            {
                Console.WriteLine("Ingresa la ruta del archivo:");
                
                string path = Console.ReadLine();

                //string path2 = @"\\195.192.2.17\Proyectos\Obras PLM\PEV 30 MEX\Terceras\Carpeta tercera1\divisions\";

                string path2 = @"C:\PEV30\divisions2\";

                string filename = path; //path.Substring(path.LastIndexOf("\\") + 1,
                //    path.IndexOf(".") - (path.LastIndexOf("\\") + 1));

                filename = filename.Replace("_", " ");

                GetHTML filecontent = new GetHTML();

                string allcontent = filecontent.getHtmlContent(path2 + filename);

                



                for (int x = 0; x < allcontent.Length; x++)
                {
                    if (allcontent.IndexOf(token, x) != -1)
                    {
                        int ini, fin;
                        ini = allcontent.IndexOf(token, x);
                        fin = allcontent.IndexOf("</p>", ini);
                        string name = allcontent.Substring(ini, (fin - ini));
                        string token2 = name;
                        string name2 = filecontent.cleanName2(name);

                        if (name.IndexOf("*") != -1)
                        {
                            name2 = filecontent.checkProduct(filename.Replace(".htm",""), name2.Trim());

                            if (name2 != string.Empty)
                            {
                                name = name.Replace("<p class=\"normal-1\">", name2);
                                //name = name.Replace("</p>", "</a></p>");

                                allcontent = allcontent.Replace(token2, name + "</a>");
                            }
                        }
                        x = fin;
                    }
                    else
                        x = allcontent.Length;
                }

                file = filecontent.Laboratory == null ? filename.Replace(".htm", "") : filecontent.Laboratory;

                file = filecontent.quitAccentsFileName(file);

                file = filecontent.cleanName(file);

                allcontent = filecontent.findEmail(allcontent, file);

                allcontent = filecontent.findUrl(allcontent, file);

                allcontent = filecontent.quitAccents(allcontent);

                //allcontent = allcontent.Replace("</title>", "</title><link href=\"../Estilos.css\" rel=\"stylesheet\" type=\"text/css\" />");

                filecontent.saveHtmlFile("C:/PEV30/Vinculados/", file.ToLower() + ".htm", allcontent);

                Console.WriteLine("El archivo ha sido guardado.");

                //Console.WriteLine("¿Deseas segmentar otro archivo? S/N");

                //s = Console.ReadLine();

            } while (s.ToLower().Equals("s"));



        }
    }
}
