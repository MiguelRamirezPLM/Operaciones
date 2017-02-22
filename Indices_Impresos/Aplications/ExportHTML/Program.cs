using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Text;
using System.IO;

namespace ExportHTML
{
    public class Program
    {
        static void Main(string[] args)
        {

            
            string letra = Console.ReadLine();
            //string path = "C:/HTMLS/" + letra + "/" + letra + ".html";
            string path = "C:/HTMLS/Genoma/" +letra + ".html";
            string temp = "../../IndesignTemplate.htm";
            string token = "<p class=\"t-tulo";
            string token2 = "<p class=\"forma-farmac-\">";
            string token3 = "<p class=\"medio-de-d-";
           
            GetHTML filecontent = new GetHTML();
            
            string allcontent = filecontent.getHtmlContent(path);
            string template = filecontent.getHtmlContent((temp));
            string name,pf,inf;
            
            int ini,ini2,ini3,next;
            int fin,fin2,med,med2;
            int len,len2,len3;
            
            len = token.Length;
            len2 = token2.Length;
            len3 = token3.Length;
            
            next = 0;
            inf = "";
            ini3 = 0;
            if (allcontent.IndexOf(token3, next) != -1)
            {
                ini3 = allcontent.IndexOf(token3, next);
                inf = allcontent.Substring(ini3, (allcontent.IndexOf("<", (ini3 + len3)) - ini3));
            }
            
            for (int i=0; i < allcontent.Length; i++)
            {
                
                if (allcontent.IndexOf(token,next) != -1)
                {   
                    ini = allcontent.IndexOf(token,next);
                    med = allcontent.IndexOf(">", ini);
                    fin = allcontent.IndexOf("<",med);
                    
                    if (ini < ini3)
                        inf = "";

                    name = allcontent.Substring(med + 1,(fin - med) - 1);
                    name = filecontent.quitAccentsFileName(name);

                    ini2 = allcontent.IndexOf(token2,next);
                    if (ini2 > 0)
                    {
                        med2 = allcontent.IndexOf(">", ini2);
                        fin2 = allcontent.IndexOf("<", med2);
                        pf = allcontent.Substring(med2 + 1, (fin2 - med2) - 1);
                        pf = filecontent.quitAccentsFileName(pf);
                    }
                    else
                        pf = string.Empty;

                    next = allcontent.IndexOf(token, fin);

                    System.Text.StringBuilder sb = new System.Text.StringBuilder(template);

                    if (next > 0){
                        string file = allcontent.Substring(ini, (next - ini));
                        
                        file = filecontent.quitAccents(file);
                        inf = filecontent.quitAccents(inf);

                        file = file.Replace("<p class=\"medio-de-d-prod-nuevo\">INFORMACI&Oacute;N REVISADA</p>", "");
                        file = file.Replace("<p class=\"medio-de-d-prod-nuevo\">INFORMACI&Oacute;N NUEVA</p>", "");

                        file = file.Replace("<p class=\"medio-de-d-prod-nuevo\">informaci&oacute;n revisada</p>", "");
                        file = file.Replace("<p class=\"medio-de-d-prod-nuevo\">informaci&oacute;n nueva</p>", "");

                        file = file.Replace("<p class=\"medio-de-d-prod-nuevo\">informaci&oacute;n REVISADA</p>", "");
                        file = file.Replace("<p class=\"medio-de-d-prod-nuevo\">informaci&oacute;n NUEVA</p>", "");

                        filecontent.setParameters(sb, "@@@Titulo@@@=" + (name.Trim() + "_" + pf.Trim()));
                        sb.Replace("@@@Contenido@@@",inf + file);
                           
                        inf = filecontent.getString(file, token3);

                    }
                    else
                    {
                        string file = allcontent.Substring(ini);
                                                
                        file = filecontent.quitAccents(file);
                        filecontent.setParameters(sb, "@@@Titulo@@@=" + (name.Trim() + "_" + pf.Trim()));//, "@@@Contenido@@@=" + file); 
                        sb.Replace("@@@Contenido@@@",inf + file);

                        i = allcontent.Length;
                    }



                    //if (File.Exists("C:/HTMLS/" + letra + "/" + name.Trim() + "_" + pf.Trim() + ".html"))
                    //    filecontent.saveHtmlFile("C:/HTMLS/" + letra + "/",
                    //    name.Trim() + "_" + pf.Trim() + i.ToString() + ".html", sb.ToString());

                    //else
                    //    filecontent.saveHtmlFile("C:/HTMLS/" + letra + "/",
                    //    name.Trim() + "_" + pf.Trim() + ".html", sb.ToString());


                    if (File.Exists("C:/HTMLS/Genoma/" + name.Trim() + "_" + pf.Trim() + ".html"))
                        filecontent.saveHtmlFile("C:/HTMLS/Genoma/",
                        name.Trim() + "_" + pf.Trim() + "_" + i.ToString() + ".html", sb.ToString());

                    else
                        filecontent.saveHtmlFile("C:/HTMLS/Genoma/",
                        name.Trim() + "_" + pf.Trim() + ".html", sb.ToString());



                    name = "";
                    pf = "";
                }
                
            }

            Console.WriteLine("Terminado....");
            Console.ReadLine();
        }

       

    }
}
