using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Text;
using System.IO;


namespace ExportHTML
{
    public class mentionatedDEF2
    {

        static void Main(string[] args)
        {
            string s;
            string path, filename, tabProds, catContent;
            string archivo = "Producto&pharmaform&descripcion&laboratorio&categoria" + Environment.NewLine;

            //Tokens
            string labToken = "<p class=\"t-tulo\">";
            string dirToken = "<p class=\"direcci-n\">";
            string catToken = "<p class=\"productos\">";
            string proToken = "<p class=\"normal-tabla\"><span class=\"producto\">";
            string pharmaToken = "<p class=\"normal-tabla\"><span class=\"forma-farmaceutica\">";
            string desToken = "<p class=\"normal-tabla\"><span class=\"indicacion\">";

            //Variables
            string lab, cat, address, prod, pharma, desc;
            lab = cat = address = prod = pharma = desc = "";

            int ini, med, fin, next;
            int ini2, med2, fin2, next2;


            do
            {
                Console.WriteLine("Ingresa la ruta del archivo a revisar:");
                path = Console.ReadLine();

                filename = path.Substring(path.LastIndexOf("\\") + 1,
                    path.LastIndexOf(".") - (path.LastIndexOf("\\") + 1));

                GetHTML content = new GetHTML();

                string allcontent = content.getHtmlContent(path);

                ini = med = fin = next = 0;

                for (int x = 0; x < allcontent.Length; x++)
                {
                    if (allcontent.IndexOf(labToken, next) != -1)
                    {
                        ini = allcontent.IndexOf(labToken, next);
                        fin = allcontent.IndexOf(labToken, ini + 1);

                        lab = allcontent.Substring(allcontent.IndexOf(labToken, next), (allcontent.IndexOf("</p>", allcontent.IndexOf(labToken, next)) - allcontent.IndexOf(labToken, next)));

                        lab = content.cleanText(lab);
                        
                        if (allcontent.IndexOf(dirToken, next) != -1 && allcontent.IndexOf(dirToken, next) < fin)
                        {
                            address = allcontent.Substring(allcontent.IndexOf(dirToken, next), (allcontent.IndexOf("</p>", allcontent.IndexOf(dirToken, next)) - allcontent.IndexOf(dirToken, next)));

                            address = content.getImage(address);
                        }
                        //int ini22, fin22, next22;
                        ini2 = fin2 = next2 = 0;

                        catContent = fin == -1 ? allcontent.Substring(ini) : allcontent.Substring(ini, (fin - ini));

                        for (int y = 0; y < catContent.Length; y++)
                        {
                            if (catContent.IndexOf(proToken, next2) != -1)
                            {
                                 
                                 ini2 = catContent.IndexOf(catToken, next2);
                                 fin2 = catContent.IndexOf(catToken, ini2 + 1);
                                 tabProds = fin2 == -1 ? allcontent.Substring(ini2) : catContent.Substring(ini2, (fin2 - ini2));
                                 
                                
                                //tabProds = catContent.Substring(ini2, (fin2 - ini2));
                                
                                int ini3, fin3, next3;
                                int ini4, fin4, next4;
                                int ini5, fin5, next5;

                                ini3 = fin3 = next3 = 0;
                                ini4 = fin4 = next4 = 0;
                                ini5 = fin5 = next5 = 0;

                                for (int z = 0; z < tabProds.Length; z++)
                                {
                                    if (tabProds.IndexOf(proToken, next3) != -1)
                                    {
                                        ini3 = tabProds.IndexOf(proToken, next3);
                                        fin3 = tabProds.IndexOf("</span>", ini3 + 1);

                                        prod = tabProds.Substring(ini3, (fin3 - ini3));
                                        prod = content.cleanText(prod);
                                        next3 = fin3;

                                        if (tabProds.IndexOf(pharmaToken, next4) != -1)
                                        {
                                            ini4 = tabProds.IndexOf(pharmaToken, next4);
                                            fin4 = tabProds.IndexOf("</span>", ini4 + 1);

                                            pharma = tabProds.Substring(ini4, (fin4 - ini4));
                                            pharma = content.cleanText(pharma);
                                        }
                                        next4 = fin4;
                                        if (tabProds.IndexOf(desToken, next5) != -1)
                                        {
                                            ini5 = tabProds.IndexOf(desToken, next5);
                                            fin5 = tabProds.IndexOf("</td>", ini5 + 1);

                                            desc = tabProds.Substring(ini5, (fin5 - ini5));
                                            desc = content.cleanText(desc);
                                            desc = desc.Replace("\n", "");
                                            desc = desc.Replace("\t", "");
                                            desc = desc.Replace("\r", "");
                                        }
                                        next5 = fin5;

                                        archivo = archivo + prod + "&" + pharma + "&" + desc + "&" + cat +"&" + lab + Environment.NewLine;

                                    }
                                    else
                                        z = tabProds.Length;
                                }

                            }
                            else
                                y = catContent.Length;

                            next2 = fin2;
                        }

                    }
                    else
                        x = allcontent.Length;
                    next = fin;
                }



                
                content.saveLogFile(@"C:\Documents and Settings\angel.parra\Escritorio", "mentionados.xls", archivo);

                s = "s";

            } while (s.ToLower().Equals("s"));
        }

    }
}

