using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace CreateHTML
{
    class Program
    {
        static void Main(string[] args)
        {           
            
            SaveHTML("C:\\Documents and Settings\\bvazquez001\\Mis documentos\\Archivos Exportados HTML\\Carpeta A\\A.html");
          
        }

        public static String getName(String name)
        {
            name = name.Trim('\t');
            String val = "<p class="+"\"t-tulo"+"\">";           
            int len = val.Length;

            if (name.IndexOf(val) >= 0)
            {
                int f = name.IndexOf('<', len);
                name = name.Substring(len, f-len);               
            }
            
            return name;
        }

        public static String getPharmaForm(String pf)
        {
            pf = pf.Trim('\t');
            String val = "<p class="+"\"forma-farmac-"+"\">";
            int len = val.Length;

            if (pf.IndexOf(val) >= 0)
            {
                int f = pf.IndexOf('<', len);
                pf = pf.Substring(len, f - len);                
            }

            return pf;
        }

        /*static void ReadFromFile(string filename)
        {
            StreamReader SR;
            StreamWriter SW;            
            string S;
            String pf = null;
            String n=null;
            String n1;
            String pf1;
            
            SR = File.OpenText(filename);
            S = SR.ReadLine();
            SW = File.CreateText("c:\\" + n + "_" + pf + ".html");
            while (S != null)

            {
                                               
                if (S.IndexOf("<p class=" + "\"t-tulo" + "\">") >= 0)
                {        
                    n=getName(S);
                    n1 = S;                  
                    S = SR.ReadLine();
                    
                    if (S.IndexOf("<p class=" + "\"forma-farmac-" + "\">") >= 0)
                    {
                        pf = getPharmaForm(S);
                        pf1 = S;
                        Console.WriteLine(n + " " + pf);
                        
                        //S = SR.ReadLine(); 
                        n = n.Replace('*',' ');
                        pf = pf.Replace('*',' ');
                        Console.WriteLine(n + " " + pf);
                        Console.ReadLine();
                        SW = File.CreateText("c:\\"+n+"_"+pf+".html");
                        SW.WriteLine(n1);
                        SW.WriteLine(pf1);
                        
                           
                        while (S.IndexOf("<p class=" + "\"t-tulo" + "\">") < 0)
                        {
                            S = SR.ReadLine();
                            SW.WriteLine(S);
                        }
                    
                    }
                }
               
                //Console.WriteLine(S);                
                //Console.ReadLine();                
                S = SR.ReadLine();
                SW.WriteLine(S);
            }
            SR.Close();
            SW.Close();
        }*/


        /*static void ReadFromFile(string filename)
        {
            StreamReader SR;
            StreamWriter SW = null;
            String cont;
            String S;
            String np = null, pf = null, n = null, pf1 = null;
            //f;

            Console.WriteLine("Bety");
            Console.ReadLine();
            SR = File.OpenText(filename);
            cont = SR.ReadToEnd();
            
            
            S = SR.ReadLine();
            SW = File.CreateText("c:\\" + "_.html");
            while (S != null)
            {
                if (S.IndexOf("<p class=" + "\"t-tulo" + "\">") >= 0)
                {
                    n = S;
                    np = getName(S);

                }
                if (S.IndexOf("<p class=" + "\"forma-farmac-" + "\">") >= 0)
                {
                    pf1 = S;
                    pf = getPharmaForm(S);

                }

                if (np != null && pf != null)
                {
                    SW = File.CreateText("c:\\" + np + "_" + pf + ".html");
                    SW.WriteLine(n);
                    SW.WriteLine(pf1);
                    Console.WriteLine(n + " " + pf1);
                    Console.ReadLine();
                    while (S.IndexOf("<p class=" + "\"t-tulo" + "\">") == -1)
                    {
                        SW.WriteLine(n);
                        SW.WriteLine(pf1);
                        S = SR.ReadLine();
                        SW.WriteLine(S);
                        
                    }
                    SW.Close();
                    np = null;
                    pf = null;
                    n = null;
                    pf1 = null;
                }


                S = SR.ReadLine();
                //SW.WriteLine(S);
                Console.ReadLine();


            }

            SR.Close();
            //SW.Close();

        }*/

        


        
        static void SaveHTML(String filename)

        {
            StreamReader SR;
            StreamWriter SW =null;
            String S, aux, aux1="",cont;
            int i, f, i1, f1, faux, y, faux2, iaux;
            StringBuilder sb = new StringBuilder();            
            String auxS1, auxS2, auxS3, auxS4;
            String vaux = "<div class=" + "\"story" + "\">";
            //String title = "<p class=" + "\"t-tulo" + "\">";
            String title = "<p class="+"\"t-tulo";
            String pharma = "<p class=" + "\"forma-farmac-" + "\">";

            SR = File.OpenText(filename);
            S = SR.ReadToEnd();
            S = S.Replace("<p class="+"\"medio-de-d-prod-nuevo"+"\">INFORMACIÓN NUEVA</p>", "");
            S = S.Replace("<p class="+"\"medio-de-d-"+"\">información revisada</p>", "");
            S = quitAccents(S);            
            S = S.Replace("-<br />", "");
            S = S.Replace(" <br />", " ");

                      
            y = S.IndexOf(vaux);

            if (y >= 0)
                S = S.Substring(y+19, S.Length -(y+19));


            sb.Append("<!DOCTYPE html PUBLIC "+"\"-//W3C//DTD XHTML 1.0 Strict//EN"+"\" "+"\"http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd"+"\">"+
                    "<html xmlns="+"\"http://www.w3.org/1999/xhtml"+"\">"+
	                "<head>"+
		                "<meta http-equiv="+"\"content-type"+"\" content="+"\"text/html;charset=utf-8"+"\" />"+
		                "<title>@@@Titulo@@@</title>"+
		                "<link href="+"\"estilos.css"+"\" rel="+"\"stylesheet"+"\" type="+"\"text/css"+"\" />"+
	                "</head>"+
	                "<body>"+
		                "<div id='@@@Titulo@@@'>"+
			                "<div class="+"\"story"+"\">"+

				                "@@@Contenido@@@"+

			                "</div>"+
		                "</div>"+
	                "</body>"+
                "</html>");

            auxS1 = "<!DOCTYPE html PUBLIC " + "\"-//W3C//DTD XHTML 1.0 Strict//EN" + "\" " + "\"http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd" + "\">" +
                    "<html xmlns=" + "\"http://www.w3.org/1999/xhtml" + "\">" +
                    "<head>" +
                        "<meta http-equiv=" + "\"content-type" + "\" content=" + "\"text/html;charset=utf-8" + "\" />" +
                        "<title>";
            auxS2 = "</title>" +
                        "<link href=" + "\"estilos.css" + "\" rel=" + "\"stylesheet" + "\" type=" + "\"text/css" + "\" />" +
                    "</head>" +
                    "<body>" +
                        "<div id='";
            auxS3 = "'>"+
			                "<div class="+"\"story"+"\">";

            auxS4 = "</div>"+
		                "</div>"+
	                "</body>"+
                "</html>";           

            
            

            while(S != null)
            {
                               
                i = S.IndexOf(title);
                faux2 = S.IndexOf(">", i);                
                f = S.IndexOf("<", faux2);
                aux = S.Substring(faux2 + 1, f - (faux2 + 1));                                              
                aux = aux.Replace("*","");
                aux = aux.Replace("®", "");
                aux = aux.Replace("†", "");
                aux = aux.Replace("MR ", " ");
                aux = setAccents(aux);
                aux = aux.Trim();
                
                
                /*
                i1 = S.IndexOf(pharma, f);
                i1 = i1 + pharma.Length;
                f1 = S.IndexOf("<", i1);

                aux1 = S.Substring(i1, f1 - i1);
                aux1 = aux1.Replace("*", "");
                aux1 = aux1.Replace("®", "");
                aux1 = aux1.Replace("†", "");
                aux1 = aux1.Replace("MR ", " ");
                aux1 = setAccents(aux1);
                aux1 = aux1.Trim();
                */
                iaux = S.IndexOf(title, f);

                if (iaux >= 0)
                    cont = S.Substring(i, iaux);
                else
                {
                    faux = S.LastIndexOf("</p>");
                    cont = S.Substring(i, faux+4);
                }

                i1 = cont.IndexOf(pharma, f);
                
                if (i1 >= 0)
                {
                    i1 = i1 + pharma.Length;
                    f1 = S.IndexOf("<", i1);
                    aux1 = cont.Substring(i1, f1 - i1);
                    aux1 = aux1.Replace("*", "");
                    aux1 = aux1.Replace("®", "");
                    aux1 = aux1.Replace("†", "");
                    aux1 = aux1.Replace("MR ", " ");
                    aux1 = setAccents(aux1);
                    aux1 = aux1.Trim();
                }

                                
                SW = File.CreateText("C:\\Documents and Settings\\bvazquez001\\Mis documentos\\Archivos Exportados HTML\\Carpeta A\\"+aux+"_"+aux1+".html");
                cont = auxS1+aux+"_"+aux1+auxS2+aux+"_"+aux1+auxS3+cont+auxS4;                
                SW.Write(cont);                
                SW.Close();


                if (iaux >= 0)
                {
                    S = S.Substring(iaux, S.Length - iaux);
                }
                else S = null;
   
                
            }              
            SR.Close();           

        }


        public static StringBuilder setParameters(StringBuilder sbBody, params string[] htmlParameters)
        {
            for (int i = 0; i < htmlParameters.Length; i++)
            {
                string paramName = htmlParameters[i].Split('=')[0];
                string paramValue = htmlParameters[i].Split('=')[1];

                sbBody.Replace(paramName, paramValue);                
            } return sbBody;          
        }


        public static string quitAccents(string originalString)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder(originalString);

            sb.Replace("á", "&aacute;");
            sb.Replace("Á", "&Aacute;");

            sb.Replace("é", "&eacute;");
            sb.Replace("É", "&Eacute;");

            sb.Replace("í", "&iacute;");
            sb.Replace("Í", "&Iacute;");

            sb.Replace("ó", "&oacute;");
            sb.Replace("Ó", "&Oacute;");

            sb.Replace("ú", "&uacute;");
            sb.Replace("Ú", "&Uacute;");

            return sb.ToString();
        }

        public static string setAccents(string originalString)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder(originalString);

            sb.Replace("&aacute;","á" );
            sb.Replace("&Aacute;","Á");

            sb.Replace("&eacute;","é");
            sb.Replace("&Eacute;","É");

            sb.Replace("&iacute;","í");
            sb.Replace("&Iacute;","Í");

            sb.Replace("&oacute;","ó");
            sb.Replace("&Oacute;","Ó");

            sb.Replace("&uacute;","ú");
            sb.Replace("&Uacute;","Ú");

            return sb.ToString();
        }
        
        

        
       
    

    }

}
