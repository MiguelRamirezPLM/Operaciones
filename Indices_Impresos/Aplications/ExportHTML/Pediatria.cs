using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Text;
using System.IO;

namespace ExportHTML
{
    public class Pediatria
    {
        static void Main(string[] args)
        {
            string valid = "";
            do
            {
                Console.WriteLine("Ingresa la ruta del archivo a segmentar:");

                string path = Console.ReadLine();

                string filename = path.Substring(path.LastIndexOf("\\") + 1,
                    path.LastIndexOf(".") - (path.LastIndexOf("\\") + 1));

              

                string fileToken = "<p class=\"t-tulo\">";

                string pharmaForm = "<p class=\"forma-farmac-\">";

                string archivo = "Producto,Forma,Archivo" + Environment.NewLine;

                string temp = "../../IndesignTemplate.htm";

                GetHTML filecontent = new GetHTML();

                string allContent = filecontent.getHtmlContent(path);
                string template = filecontent.getHtmlContent((temp));


                string product, bprop, fileName, data;

                int ini, ini2, med, med2, fin, fin2, next;
               
                next = 0;

                for (int i = 0; i < allContent.Length; i++)
                {
                    if (allContent.IndexOf(fileToken, next) != -1)
                    {
                        ini = allContent.IndexOf(fileToken, next);
                        med = allContent.IndexOf(">", ini);
                        fin = allContent.IndexOf("</p>", med);

                        product = allContent.Substring(med + 1, fin - (med + 1));

                        fileName = filecontent.quitAccentsFileName(product);

                        ini2 = allContent.IndexOf(pharmaForm, next);

                        med2 = allContent.IndexOf(">", ini2);
                        fin2 = allContent.IndexOf("</p>", med2);

                        if (ini2 < fin2)
                        {
                            bprop = allContent.Substring(med2 + 1, fin2 - (med2 + 1));

                            bprop = bprop.Replace(",", "_");
                        }
                        else
                            bprop = "";

                        next = allContent.IndexOf(fileToken, fin);

                        StringBuilder sb = new StringBuilder(template);

                        string file;

                        if (next != -1)
                            file = allContent.Substring(ini, (next - ini));

                        else
                        {
                            file = allContent.Substring(ini);
                            i = allContent.Length;
                        }
                        file = filecontent.quitAccents(file);
                        file = filecontent.changeImage(file, filename);
                        file = filecontent.findEmail(file, filename);
                        file = filecontent.findUrl(file, filename);
                        
                        
                        filecontent.setParameters(sb, "@@@Titulo@@@=" + fileName);
                        sb.Replace("@@@Contenido@@@", file);

                        if (File.Exists("C:/angel.parra/WIabbott/HTMLs/" + fileName + "_" + filecontent.quitAccentsFileName(bprop) + ".htm"))
                        {
                            filecontent.saveHtmlFile("C:/angel.parra/WIabbott/HTMLs/", fileName + "_" + filecontent.quitAccentsFileName(bprop) + "_" + i + ".htm", sb.ToString());

                            archivo = archivo + product + "," + bprop + "," + (fileName + "_" + filecontent.quitAccentsFileName(bprop) + "_" + i + ".htm");
                        }
                        else
                        {
                            filecontent.saveHtmlFile("C:/angel.parra/WIabbott/HTMLs/", fileName + "_" + filecontent.quitAccentsFileName(bprop) + ".htm", sb.ToString());

                            archivo = archivo + product + "," + bprop + "," + (fileName + "_" + filecontent.quitAccentsFileName(bprop) + ".htm");
                        }
                        i = next == -1 ? i : next;
    
                        
                        product = "";
                        bprop = "";

                    }
                    else
                        i = allContent.Length;

                }

                filecontent.saveLogFile("C:/angel.parra/WIabbott/HTMLs/", "EmpateProds" + filename.Substring(0, 3) + ".xls", archivo);

                Console.WriteLine("El archivo ha sido segmentado.");

                Console.WriteLine("¿Deseas segmentar otro archivo? S/N");

                valid = Console.ReadLine();

            } while (valid.ToLower().Equals("s"));


        }
    }
}
