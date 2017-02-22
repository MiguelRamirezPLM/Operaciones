using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Text;
using System.IO;


namespace ExportHTML
{
    public class DEF
    {

        static void Main(string[] args)
        {
            string tokenAux = "<p class=\"t-tulo\""; 
            //string token = "<p class=\"normal\"><img";
            string token = "<p class=\"t-tulo";           
            string token2 = "<p class=\"forma-farmac-"; 
            
            //string token = "<p class=\"t-tulo\"><span class=\"boldcursiva\">";
            //string token2 = "<p class=\"forma-farmac-\"><span class=\"cursiva\"";

            ///PEV 
            //string token ="<p class=\"titulo\">";
            //string token2 = "<p class=\"forma-farmaceutica\"><span class=\"boldcursiva\">";

            ///PEV COLOMBIA
            //string token = "<p class=\"x0-nombre";
            //string token2 = "<p class=\"x1-presenta";

            string temp = "../../IndesignTemplate.htm";
            string token3 = "<p class=\"medio-de-d-prod-nuevo\">";//"<p class=\"medio-de-d-prod-nuevo\">";                             
            string s = "";
            do
            {
                Console.WriteLine("Ingresa la ruta bbb:");
                
                string path = Console.ReadLine();
                DirectoryInfo dir = new System.IO.DirectoryInfo(path);
                foreach (FileInfo f in dir.GetFiles("*.html"))
                {
                    string filename = "";
                
                //////string filename = path.Substring(path.LastIndexOf("\\") + 1,
                //////    path.LastIndexOf(".") - (path.LastIndexOf("\\") + 1));
                    filename = f.FullName.Substring(f.FullName.LastIndexOf("\\") + 1,
                    f.FullName.LastIndexOf(".") - (f.FullName.LastIndexOf("\\") + 1));
                //string path2 = path.Substring(0, path.LastIndexOf("\\")+ 1) + filename;
                    string path2 = @"\\195.192.2.17\Proyectos\Obras PLM\Ecuador\DEF 39\Formación\Roemmer - Roewe entregado 20131023\ProductosSegmentados\";
                
                GetHTML filecontent = new GetHTML();

                //string allcontent = filecontent.getHtmlContent(path);
                string allcontent = filecontent.getHtmlContent(f.FullName);
                string template = filecontent.getHtmlContent((temp));
                string name, pf, inf;

                int ini, ini2, ini3, next;
                int fin, fin2, med, med2;
                int len, len2, len3;

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

                for (int i = 0; i < allcontent.Length; i++)
                {

                    if (allcontent.IndexOf(token, next) != -1)
                    {
                        ini = allcontent.IndexOf(token, next);
                        int init = allcontent.IndexOf(tokenAux,next);
                        //med = allcontent.IndexOf(">", (ini + 18));                        
                        med = allcontent.IndexOf(">", init);
                        //PEV 30
                        //med = ini + token.Length - 1;
                        fin = allcontent.IndexOf("<", med);

                        name = allcontent.Substring(med + 1, (fin - med) - 1);
                        name = filecontent.quitAccentsFileName(name);
                        name = filecontent.cleanName(name);

                        ini2 = allcontent.IndexOf(token2, next);
                        if (ini2 > 0 && ini2 < allcontent.IndexOf(token, fin))
                        {
                            med2 = allcontent.IndexOf(">", ini2);
                            //med2 = allcontent.IndexOf(">", ini2+25);
                            //PEV 30
                            //med2 = ini2 + token2.Length -1;
                            fin2 = allcontent.IndexOf("<", med2);
                            pf = allcontent.Substring(med2 + 1, (fin2 - med2) - 1);
                            pf = filecontent.quitAccentsFileName(pf);
                            pf = filecontent.cleanName(pf);
                        }
                        else
                            pf = string.Empty;

                        next = allcontent.IndexOf(token, fin);

                        System.Text.StringBuilder sb = new System.Text.StringBuilder(template);

                        if (next > 0)
                        {
                            string file = allcontent.Substring(ini, (next - ini));

                            file = filecontent.quitAccents(file);
                            file = filecontent.changeImage(file, filename);
                            file = filecontent.findEmail(file, filename);
                            file = filecontent.findUrl(file, filename);

                            inf = filecontent.quitAccents(inf);

                            //file = file.Replace("<p class=\"medio-de-d-prod-nuevo\">producto nuevo</p>", "");
                            file = file.Replace("<p class=\"medio-de-d-prod-nuevo\">INFORMACIÓN NUEVA</p>", "");

                            //file = file.Replace("<p class=\"medio-de-d-prod-nuevo\">Producto Nuevo</p>", "");
                            //file = file.Replace("<p class=\"medio-de-d-prod-nuevo\">Producto nuevo</p>", "");
                            //file = file.Replace("<p class=\"medio-de-d-prod-nuevo\">informaci&oacute;n nueva</p>", "");

                            //file = file.Replace("<p class=\"antetitulo\">PRODUCTO NUEVO</p>", "");
                            //file = file.Replace("<p class=\"antetitulo\">PRODUCTO Nuevo</p>", "");

                            //file = file.Replace("<p class=\"antetitulo\">Producto Nuevo</p>", "");
                            //file = file.Replace("<p class=\"antetitulo\">Producto nuevo</p>", "");
                            //file = file.Replace("<p class=\"antetitulo\">informaci&oacute;n nueva</p>", "");


                            filecontent.setParameters(sb, "@@@Titulo@@@=" + (name.Trim() + "_" + pf.Trim()));
                            sb.Replace("@@@Contenido@@@", inf + file);

                            inf = filecontent.getString(file, token3);
                        }
                        else
                        {
                            string file = allcontent.Substring(ini);

                            file = filecontent.quitAccents(file);
                            filecontent.setParameters(sb, "@@@Titulo@@@=" + (name.Trim() + "_" + pf.Trim()));//, "@@@Contenido@@@=" + file); 
                            sb.Replace("@@@Contenido@@@", inf + file);

                            i = allcontent.Length;
                        }

                        if (File.Exists(path2 + "/" + name.Trim() + "_" + pf.Trim() + ".htm"))
                            filecontent.saveHtmlFile(path2,
                            name.Trim() + "_" + pf.Trim() + "_" + i.ToString() + ".htm", sb.ToString());

                        else
                            filecontent.saveHtmlFile(path2,
                            name.Trim() + "_" + pf.Trim() + ".htm", sb.ToString());



                        name = "";
                        pf = "";
                    }



                }
                }
                Console.WriteLine("El archivo ha sido segmentado.");

                Console.WriteLine("¿Deseas segmentar otro archivo? S/N");

                s = Console.ReadLine();

            } while (s.ToLower() == "s");


        }

    }
}
