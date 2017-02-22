using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Text;
using System.IO;


namespace ExportHTML
{
    public class mentionatedDEF
    {

        static void Main(string[] args)
        {
            string s;
            string path, filename, tabProds,catContent;
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
            lab = cat = address = prod = pharma= desc = "";

            int ini, med, fin, next;
            
            do{
                Console.WriteLine("Ingresa la ruta del archivo a revisar:");
                path = Console.ReadLine();

                filename = path.Substring(path.LastIndexOf("\\") + 1,
                    path.LastIndexOf(".") - (path.LastIndexOf("\\") + 1));

                GetHTML content = new GetHTML();

                string allcontent = content.getHtmlContent(path);

                ini = med = fin = next = 0;

                for (int z = 0; z < allcontent.Length; z++)
                {
                    if (allcontent.IndexOf(labToken, next) != -1)
                    {
                        ini = allcontent.IndexOf(labToken, next);
                        fin = allcontent.IndexOf(labToken, ini + 1);
                       

                        lab = allcontent.Substring(allcontent.IndexOf(labToken, next), (allcontent.IndexOf("</p>", allcontent.IndexOf(labToken, next)) - allcontent.IndexOf(labToken, next)));

                        lab = content.cleanText(lab);

                        if(allcontent.IndexOf(dirToken, next) != -1 && allcontent.IndexOf(dirToken, next) < fin)
                        {   
                            address = allcontent.Substring(allcontent.IndexOf(dirToken, next), (allcontent.IndexOf("</p>", allcontent.IndexOf(dirToken, next)) - allcontent.IndexOf(dirToken, next)));

                            address = content.getImage(address);
                        }

                        catContent = fin == -1 ? allcontent.Substring(ini) : allcontent.Substring(ini, (fin - ini));
                        int ini1, fin1, next1;
                        ini1 = fin1 = next1 = 0;

                        for (int x = 0; x < catContent.Length; x++)
                        {

                            if (catContent.IndexOf(catToken, next1) != -1 && catContent.IndexOf(catToken, next1) < fin)
                            {
                                cat = catContent.Substring(catContent.IndexOf(catToken, next1), (catContent.IndexOf("</p>", catContent.IndexOf(catToken, next1)) - catContent.IndexOf(catToken, next1)));

                                cat = content.cleanText(cat);
                            }
                            //products

                            ini1 = catContent.IndexOf(catToken, next1);
                            fin1 = catContent.IndexOf(catToken, ini1 + 1);

                            tabProds = fin1 == -1 ? catContent.Substring(ini1) : catContent.Substring(ini1, (fin1 - ini1));
                              

                            int ini2, fin2, next2;
                            int ini3, fin3, next3;
                            int ini4, fin4, next4;

                            ini2 = fin2 = next2 = 0;
                            ini3 = fin3 = next3 = 0;
                            ini4 = fin4 = next4 = 0;

                            for (int y = 0; y < tabProds.Length; y++)
                            {
                                if (tabProds.IndexOf(proToken, next2) != -1)
                                {
                                    ini2 = tabProds.IndexOf(proToken, next2);
                                    fin2 = tabProds.IndexOf("</span>", ini2 + 1);

                                    prod = tabProds.Substring(ini2, (fin2 - ini2));
                                    prod = content.cleanText(prod);

                                }
                                next2 = fin2;
                                ///
                                if (tabProds.IndexOf(pharmaToken, next3) != -1)
                                {
                                    ini3 = tabProds.IndexOf(pharmaToken, next3);
                                    fin3 = tabProds.IndexOf("</span>", ini3 + 1);

                                    pharma = tabProds.Substring(ini3, (fin3 - ini3));
                                    pharma = content.cleanText(pharma);
                                }
                                next3 = fin3;
                                //////
                                if (tabProds.IndexOf(desToken, next4) != -1)
                                {
                                    ini4 = tabProds.IndexOf(desToken, next4);
                                    fin4 = tabProds.IndexOf("</td>", ini4 + 1);

                                    desc = tabProds.Substring(ini4, (fin4 - ini4));
                                    desc = content.cleanText(desc);
                                    desc.Replace("\n", "");
                                    desc.Replace("\t", "");
                                    desc.Replace("\r", "");
                                }
                                next4 = fin4;

                                archivo = archivo + prod + "&" + pharma + "&" + desc + "&" + lab + "&" + cat + Environment.NewLine;

                            }
                        
                        }

                        next1 =  fin1;



                        //if(allcontent.IndexOf(catToken, next) != -1 && allcontent.IndexOf(catToken, next)< fin)
                        //{ 
                        //    cat = allcontent.Substring(allcontent.IndexOf(catToken, next), (allcontent.IndexOf("</p>", allcontent.IndexOf(catToken, next)) - allcontent.IndexOf(catToken, next)));

                        //    cat = content.cleanText(cat);
                        //}
                        
                        //Products

                        //tabProds = allcontent.Substring(ini, (fin - ini));

                        //int ini2, fin2,next2;
                        //int ini3, fin3, next3;
                        //int ini4, fin4, next4;

                        //ini2 = fin2 = next2 = 0;
                        //ini3 = fin3 = next3 = 0;
                        //ini4 = fin4 = next4 = 0;

                        //for (int y = 0; y < tabProds.Length; y++)
                        //{
                        //    if (tabProds.IndexOf(proToken, next2) != -1)
                        //    {
                        //        ini2 = tabProds.IndexOf(proToken, next2);
                        //        fin2 = tabProds.IndexOf("</span>", ini2 + 1);

                        //        prod=tabProds.Substring(ini2, (fin2 - ini2));


                        //    }
                        //    next2 = tabProds.IndexOf(proToken, fin2);


                        //    if (tabProds.IndexOf(pharmaToken, next3) != -1)
                        //    {
                        //        ini3 = tabProds.IndexOf(pharmaToken, next3);
                        //        fin3 = tabProds.IndexOf("</span>", ini3 + 1);

                        //        pharma = tabProds.Substring(ini3, (fin3 - ini3));

                        //    }
                        //    next3 = tabProds.IndexOf(pharmaToken, fin3);

                        //    if (tabProds.IndexOf(desToken, next4) != -1)
                        //    {
                        //        ini4 = tabProds.IndexOf(desToken, next4);
                        //        fin4 = tabProds.IndexOf("</td>", ini4 + 1);

                        //        desc = tabProds.Substring(ini4, (fin4 - ini4));

                        //    }
                        //    next4 = tabProds.IndexOf(desToken, fin4);


                        //}
                        
                        
                        
                        next = fin;

                        z = fin;
                    }
                    else
                        z = allcontent.Length;

                    

                }

                content.saveLogFile (@"C:\Documents and Settings\angel.parra\Escritorio", "mentionados.xls", archivo);

                s = "s";

            }while(s.ToLower().Equals("s"));
        }

    }
}
