using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace ExportHTML
{
    public class PEV
    {

        //static void Main(string[] args)
        //{
        //    string valid = "";
        //    do
        //    {
        //        Console.WriteLine("Ingresa la ruta del archivo a segmentar:");
                
        //        string path = Console.ReadLine();

        //        string filename = path.Substring(path.LastIndexOf("\\") + 1,
        //            path.IndexOf(".") - (path.LastIndexOf("\\") + 1));

        //        string temp = "../../IndesignTemplate.htm";
        //        string token = "<p class=\"titulo\">";
        //        string token2 = "<p class=\"forma-farmaceutica\"><span class=\"boldcursiva\">";
        //        string archivo = "Producto,Archivo" + Environment.NewLine;

        //        GetHTML filecontent = new GetHTML();

        //        string allcontent = filecontent.getHtmlContent(path);
        //        string template = filecontent.getHtmlContent((temp));
        //        string name, pf,name2 ;

                

        //        int ini, ini2, next;
        //        int fin, fin2,fin3;
        //        int len, len2,id;

        //        len = token.Length;
        //        len2 = token2.Length;


        //        next = 0;
            
            
        //        for (int i = 0; i < allcontent.Length; i++)
        //        {

        //            if (allcontent.IndexOf(token, next) != -1)
        //            {
        //                ini = allcontent.IndexOf(token, next);

        //                fin = allcontent.IndexOf("</p>", ini + len);

        //                fin3 = allcontent.IndexOf("</p>", ini + len);

        //                name = allcontent.Substring(ini + len, (fin - (ini + len)));

        //                name2 = allcontent.Substring(ini + len, (fin - (ini + len)));

        //                name = filecontent.quitAccentsFileName(name);

        //                name = filecontent.cleanName(name);

        //                name2 = filecontent.cleanName(name2);

        //                ini2 = allcontent.IndexOf(token2, next);
        //                fin2 = allcontent.IndexOf("<", ini2 + len2);

        //                pf = allcontent.Substring(ini2 + len2, (fin2 - (ini2 + len2)));

        //                pf = filecontent.quitAccentsFileName(pf);

        //                next = allcontent.IndexOf(token, fin);

        //                System.Text.StringBuilder sb = new System.Text.StringBuilder(template);

        //                if (next > 0)
        //                {
        //                    string file = allcontent.Substring(ini, (next - ini));

        //                    file = filecontent.quitAccents(file);
        //                    file = filecontent.changeImage(file, filename);
        //                    filecontent.setParameters(sb, "@@@Titulo@@@=" + (name.Trim() + "_" + pf.Trim()));
        //                    sb.Replace("@@@Contenido@@@", file);
        //                }
        //                else
        //                {
        //                    string file = allcontent.Substring(ini);

        //                    file = filecontent.quitAccents(file);

        //                    filecontent.setParameters(sb, "@@@Titulo@@@=" + (name.Trim() + "_" + pf.Trim()));//, "@@@Contenido@@@=" + file); 
        //                    sb.Replace("@@@Contenido@@@", file);

        //                    i = allcontent.Length;
        //                }

        //                id = filecontent.getProIDDEAQ(name2,pf);

        //                if (id == 0)
        //                {

        //                    if (File.Exists("C:/Documents and Settings/jramirez001/Escritorio/PEV/PROD-NUT/" + filename + "/" +
        //                            name.Trim().ToLower() + "_" + pf.Trim().ToLower() + ".htm"))
        //                    {
        //                        filecontent.saveHtmlFile("C:/Documents and Settings/jramirez001/Escritorio/PEV/PROD-NUT/" + filename + "/",
        //                           name.Trim().ToLower() + "_" + pf.Trim().ToLower() + i.ToString() + ".htm", sb.ToString());

        //                        archivo = archivo + name + "," + name.Trim().ToLower() + "_" + pf.Trim().ToLower() + i.ToString() + ".htm" + Environment.NewLine;
        //                    }
        //                    else
        //                    {

        //                        filecontent.saveHtmlFile("C:/Documents and Settings/jramirez001/Escritorio/PEV/PROD-NUT/" + filename + "/",
        //                        name.Trim().ToLower() + "_" + pf.Trim().ToLower() + ".htm", sb.ToString());

        //                        archivo = archivo + name + "," + name.Trim().ToLower() + "_" + pf.Trim().ToLower() + ".htm" + Environment.NewLine;
        //                    }
        //                }
        //                else
        //                {
        //                    if (File.Exists("C:/Documents and Settings/jramirez001/Escritorio/PEV/PROD-NUT/" + filename + "/" + id.ToString() + ".htm"))
        //                    {
        //                        filecontent.saveHtmlFile("C:/Documents and Settings/jramirez001/Escritorio/PEV/PROD-NUT/" + filename + "/",
        //                           id.ToString() + "_" + i.ToString() + ".htm", sb.ToString());

        //                        archivo = archivo + name + "," + id.ToString() + "_" + i.ToString() + ".htm" + Environment.NewLine;
        //                    }
        //                    else
        //                    {
        //                        filecontent.saveHtmlFile("C:/Documents and Settings/jramirez001/Escritorio/PEV/PROD-NUT/" + filename + "/",
        //                        id.ToString() + ".htm", sb.ToString());

        //                    }
        //                }


        //            }
                        
        //        }

        //        filecontent.saveHtmlFile("C:/Documents and Settings/jramirez001/Escritorio/PEV/PROD-NUT/" + filename + "/", "Archivos_" + filename + ".csv", archivo);
                            
        //        Console.WriteLine("El archivo ha sido segmentado.");

        //        Console.WriteLine("¿Deseas segmentar otro archivo? S/N");

        //        valid = Console.ReadLine();


        //    }while(valid.ToLower().Equals("s"));

        //}

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