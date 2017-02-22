using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Text;
using System.IO;

namespace ExportHTML
{
    public class Participantes
    {
        static void Main(string[] args)
        {
            DirectoryInfo dirInfo = new DirectoryInfo(@"C:\Terceras\");

            FileInfo[] fileInfo = dirInfo.GetFiles("*.html");

            string token = "<p class=\"normal\">";

            string token2 = "<br />";

            string file = "";

            int ini,med,fin,next;
            int ini2,ini3, cont,num;
            string prod,prod2,ff;
            ini3 = 0;
            prod2 = "";
            num = 0;
            ff = "";

            GetHTML filecontent = new GetHTML();

            for (int x = 0; x < fileInfo.Length; x++)
            {
                string name = fileInfo[x].Name.Substring(0, (fileInfo[x].Name.IndexOf(".")));

                string content = filecontent.getHtmlContent(fileInfo[x].FullName);

                next = 0;

                for (int i = 0; i < content.Length; i++)
                {
                    if (content.IndexOf(token, next) > 0)
                    {
                        ini = content.IndexOf(token, next);
                        med = content.IndexOf(">", ini);
                        fin = content.IndexOf("</p>", med);

                        if (!content[med + 1].Equals('*'))
                        {

                            prod = content.Substring(med + 1, (content.IndexOf(" ", med + 2) - (med + 1))) == " " ?
                                content.Substring(med + 2, (content.IndexOf(" ", med + 3) - (med + 1))) : content.Substring(med + 1, (content.IndexOf(" ", med + 2) - (med + 1)));

                            ini2 = content.IndexOf(" ", med + 2);

                            prod = prod.Trim();

                            prod = filecontent.addAccents(prod);

                            do
                            {
                                cont = 0;//filecontent.checkName(prod);

                                if (cont > 0)
                                {
                                    prod2 = prod;
                                    prod = prod + content.Substring(ini2, (content.IndexOf(" ", ini2 + 1) - (ini2)));
                                    ini2 = content.IndexOf(" ", ini2);
                                }
                                else
                                {
                                    ini3 = content.IndexOf(token2, ini2);
                                    ff = content.Substring(ini3 + token2.Length, (content.IndexOf(".", ini3) - (ini3 + token2.Length)));
                                    ff = filecontent.addAccents(ff);

                                }

                            } while (cont > 0);
                            next = ini3;
                            file = file + name + ","+ prod2 + "," + ff + ";" + Environment.NewLine;
                            prod2 = "";
                            ff = "";
                            num++;
                        }
                        //else
                        //{
                        //    prod = content.Substring(med + 3, (content.IndexOf(" ", med + 3) - (med + 2)));

                        //    ini2 = content.IndexOf(" ", med + 3);

                        //    prod = prod.Trim();

                        //    do
                        //    {
                        //        cont = filecontent.checkName(prod);

                        //        if (cont > 0)
                        //        {
                        //            prod2 = prod;
                        //            prod = prod + content.Substring(ini2, (content.IndexOf(" ", ini2 + 1) - (ini2)));
                        //            ini2 = content.IndexOf(" ", ini2);
                        //        }
                        //        else
                        //        {
                        //            ini3 = content.IndexOf(token2, ini2);
                        //            ff = content.Substring(ini3 + token2.Length, (content.IndexOf(".", ini3) - (ini3 + token2.Length)));
                        //            ff = filecontent.addAccents(ff);

                        //        }

                        //    } while (cont > 0);
                        //    next = ini3;
                        //    file = file + "* " + prod2 + "," + ff + ";" + Environment.NewLine;
                        //    prod2 = "";
                        //    ff = "";
                        //}

                        else
                            next = fin;
                    }
                    
                    

                }
                //filecontent.saveHtmlFile("C:/Terceras/Respaldo/", name + ".csv", file);
                //file = "";
            }
            filecontent.saveHtmlFile("C:/Terceras/Respaldo/","Mencionados.csv", file);
            
            Console.WriteLine("Terminado....Productos" + num.ToString());
            Console.ReadLine();

        }

    }
}
