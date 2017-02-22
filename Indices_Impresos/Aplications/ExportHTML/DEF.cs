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

            ///PEV  34
            //string token = "<p class=\"titulo";
            //string token2 = "<p class=\"forma-farmaceutica";

            //otros
            //string token = "<p class=\"Sintoma\">";

            //string token2 = "<p class=\"forma-farmaceutica";
             
             //VMG
             //string token = "<p class=\"t-tulo\"";
             //string token2 = "<p class=\"forma-farmac-\"><span class=\"cursivo-normal\"";


            ////DEF 60
            string token = "<p class=\"T-tulo";
            string token2 = "<p class=\"Forma-farmac-";


              //Guia Salud
            //string token = "<p class=\"GS_nombre-del-producto";
            //string token2 = "<p class=\"GS_forma-farmaceutica";

            //Guia Salud ECUADOR
            //string token = "<p class=\"Nombredelproducto";
            //string token2 = "<p class=\"Formafarmaceutica";


            //DEF colombia
            //string token = "<p class=\"T-tulo\">";
            //string token2 = "<p class=\"Forma-farmac-\">";

            //GUIA 60
            //string token = "<p class=\"pf_titulo_producto";
            //string token2 = "<p class=\"pf_base_propaganda";

            //DEACI
            //string token = "<p class=\"pf_titulo_producto\"";
            //string token2 = "<p class=\"pf_foto_centrada";


            ////DEF colombia
            //string token = "<p class=\"T-tulo\">";
            //string token2 = "<p class=\"Logo";

            //DEAQ
            //string token = "<p class=\"T-tulo\"><span class=\"boldcursiva\">";
            ////string token2 = "<p class=\"Base-de-prop-\"><span class=\"bold\">";
            //string token2 = "<p class=\"Forma-farmac-\">";

            //string token = "<p class=\"Nombre-del-Producto\">";
            //string token2 = "<p class=\"_-BODY-TEXT-4\"><span class=\"subtitulo-texto\">"; 
             

            ////DEAQ Colombia
           //// string token = "<p class=\"Titulo\"";
           //// string token2 = "<p class=\"Presentacion\">";
           ////// string token2 = "<p class=\"_-presentacion\""; 
           
            /////PEV 
            //string token = "<p class=\"TITULO\"";
            //string token2 = "<p class=\"logo\"";

            ///PEV COLOMBIA
            //string token = "<p class=\"NOMBRE";
            //string token2 = "<p class=\"PRESENTA";


            string temp = "../../IndesignTemplate.htm";
           // string token3 = "<p class='T-tulo-prod--nuevo'>";//"<p class=\"medio-de-d-prod-nuevo\">";      
            string token3 = "<p class=\"dga\">";
            string s = "";
            int cont = 0;
            do
            {

                Console.WriteLine("Ingresa la ruta bbb:");

                string path = Console.ReadLine();
                string pathD = path.Substring(0, path.LastIndexOf("\\") + 1);

                
                DirectoryInfo directory = new DirectoryInfo(path);
                
                FileInfo[] files = directory.GetFiles("*.html");
                foreach (FileInfo item in files)
                {
                    cont++;
                    string filename = item.Name.Substring(0, item.Name.IndexOf("."));
                    //path.Substring(path.LastIndexOf("\\") + 1, path.LastIndexOf(".") - (path.LastIndexOf("\\") + 1));

                    //string path2 = path.Substring(0, path.LastIndexOf("\\")+ 1) + filename;
                    //string path2 = @"\\\\195.192.2.17\Proyectos\Obras PLM\Mexico\PEV 33\Formación\ProductSegmentados\DNA\";
                    //string path2 = @"O:\Peru\DEF 26\DEF - 2014\LABORATORIOS CD\LABORATORIOS CD\Formacion Segmentada\";

                   //string path2 = @"C:\def\def 62\ve\ProductSegmentados\";

                   string path2 = @"C:\def\def 29P\ProductSegmentados\";

                   //  string path2 = @"C:\vmg\ProductSegmentados\";
               
                    GetHTML filecontent = new GetHTML();

                    string allcontent = filecontent.getHtmlContent(item.FullName);
                    string template = filecontent.getHtmlContent((temp));
                    string name, pf, inf;

                    int ini, ini2, ini3, next;
                    int fin, fin2, med, med2;
                    int len, len2, len3;

                   

                    len = token.Length;
                    //len2 = token2.Length;
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
                            //med = allcontent.IndexOf(">", (ini + 18));                        
                            med = allcontent.IndexOf(">", ini);
                            //PEV 30
                            //med = ini + token.Length - 1;
                            fin = allcontent.IndexOf("</", med);

                            name = allcontent.Substring(med + 1, (fin - med) - 1);
                            name = filecontent.quitAccentsFileName(name);
                            name = filecontent.cleanName(name);
                            int xx = allcontent.IndexOf(token, fin);
                            ini2 = allcontent.IndexOf(token2, next);

                            if (ini2 > 0 && ini2 < allcontent.IndexOf(token, fin))
                            {
                                med2 = allcontent.IndexOf(">", ini2);
                                //PEV 30
                                //med2 = ini2 + token2.Length - 1;
                                fin2 = allcontent.IndexOf("</", med2);
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
                          //  file = file.Replace("<p class=\"ANTETITULO\">PRODUCTO NUEVO</p>", ""); -- DEF Colombia

                            file = file.Replace("xml:lang='es-ES'>", ""); 
                            file = file.Replace("<p class=\"Antetitulo\">Información nueva</p>", "");
                            file = file.Replace("lang=\"en-GB\"", "");

                            file = file.Replace("<span class=\"SymbolProp-BT\">³</span>", "&#8805;");
                            file = file.Replace("<span class=\"SymbolProp-BT\">£</span>", "&#8804;");
                            file = file.Replace("<span class=\"SymbolProp-BT_subIndice\">¥</span>", "<span class=\"SubIndice\">&#8734;</span>");
                            file = file.Replace("<span class=\"SymbolProp-BT\">¥</span>", "&#8734;");
                            file = file.Replace("<span class=\"SymbolProp-BT\"> </span>", "&#149;");

                           
                            file = file.Replace("				<colgroup>", "");
                            file = file.Replace("					<col />", "");
                            file = file.Replace("				</colgroup>", "");

                            file = file.Replace("				<colgroup>", "");
                            file = file.Replace("					<col />", "");
                            file = file.Replace("					<col />", "");
                            file = file.Replace("				</colgroup>", "");

                            file = file.Replace("				<colgroup>", "");
                            file = file.Replace("					<col />", "");
                            file = file.Replace("					<col />", "");
                            file = file.Replace("					<col />", "");
                            file = file.Replace("				</colgroup>", "");

                            file = file.Replace("				<colgroup>", "");
                            file = file.Replace("					<col />", "");
                            file = file.Replace("					<col />", "");
                            file = file.Replace("					<col />", "");
                            file = file.Replace("					<col />", "");
                            file = file.Replace("				</colgroup>", "");

                            file = file.Replace("<p class=\"Normal\"><span class=\"Producto-Nuevo\">producto nuevo</span></p>", "");
 
                            //file = file.Replace("class=\"Ningún-estilo-de-tabla\"", ""); 
                            //file = file.Replace("class=\"Ning&uacute;n-estilo-de-tabla\"", "");
                           file = file.Replace("Ning&uacute;n-estilo-de-tabla","Ningun-estilo-de-tabla");
                            file = file.Replace(" lang=\"en-US\"", "");
                            file = file.Replace("<p class=\"Medio-de-D--prod--nuevo\">INFORMACIÓN NUEVA</p>", "");
                            file = file.Replace("<p class=\"Medio-de-D--prod--nuevo\">INFORMACI&Oacute;N NUEVA</p>", "");
                            file = file.Replace(" lang=\"es-ES\"", "");
                            file = file.Replace("<td class=\"Tabla-b-sica Tabla\" />", "<td class=\"Tabla-b-sica Tabla\"> </td>");
                            file = file.Replace("                                      </p>", "</p>");
                            file = file.Replace("\n\n\n", "");
                            file = file.Replace("\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\t\t\t", "");
                            file = file.Replace("\r\n\r\n\r\n\r\n\r\n\r\n\r\n\t\t\t", "");
                            file = file.Replace("\r\n\r\n\r\n\r\n\r\n\t\t\t", "");

                            file = file.Replace("\r\n\r\n\r\n\r\n\t\t", "");

                            file = file.Replace("</span><span class=\"bold-entrada\">", "");
                            file = file.Replace("<td class=\"tb_transparente cl_celdas_sin_filo cl_celdas_sin_filo\" />", "<td class=\"tb_transparente cl_celdas_sin_filo cl_celdas_sin_filo\"> </td>");
                            file = file.Replace("<p class=\"pf_especial_titulo_prod\">", "<p class=\"pf_normal\">");
                            file = file.Replace("T-tulo-prod--nuevo", "T-tulo");
                            file = file.Replace("<td class=\"Tabla-b-sica Tabla Tabla\" />", "<td class=\"Tabla-b-sica Tabla Tabla\" > </td>");
 
                            //file = file.Replace("<p class=\"medio-de-d-prod-nuevo\">Producto Nuevo</p>", "");
                            //file = file.Replace("<p class=\"medio-de-d-prod-nuevo\">Producto nuevo</p>", "");
                            //file = file.Replace("<p class=\"medio-de-d-prod-nuevo\">informaci&oacute;n nueva</p>", "");

                            //file = file.Replace("<p class=\"antetitulo\">PRODUCTO NUEVO</p>", "");
                            //file = file.Replace("<p class=\"antetitulo\">PRODUCTO Nuevo</p>", "");

                            //file = file.Replace("<p class=\"antetitulo\">Producto Nuevo</p>", "");
                            //file = file.Replace("<p class=\"antetitulo\">Producto nuevo</p>", "");
                            //file = file.Replace("<p class=\"antetitulo\">informaci&oacute;n nueva</p>", "");

                            pf = "_";
                            filecontent.setParameters(sb, "@@@Titulo@@@=" + (name.Trim() + "_" + pf.Trim()));
                            sb.Replace("@@@Contenido@@@", inf + file);

                            inf = filecontent.getString(file, token3);
                        }
                        else
                        {
                            string file = allcontent.Substring(ini);

                            file = file.Replace("Ning&uacute;n-estilo-de-tabla", "Ningun-estilo-de-tabla");
                            file = file.Replace("Ningún-estilo-de-tabla", "Ningun-estilo-de-tabla");

                            file = file.Replace("<span class=\"SymbolProp-BT\">³</span>", "&#8805;");
                            file = file.Replace("<span class=\"SymbolProp-BT\">£</span>", "&#8804;");
                            file = file.Replace("<span class=\"SymbolProp-BT_subIndice\">¥</span>", "<span class=\"SubIndice\">&#8734;</span>");
                            file = file.Replace("<span class=\"SymbolProp-BT\">¥</span>", "&#8734;");
                            file = file.Replace("<span class=\"SymbolProp-BT\"> </span>", "&#149;");

                            file = file.Replace("				<colgroup>", "");
                            file = file.Replace("					<col />", "");
                            file = file.Replace("				</colgroup>", "");

                            file = file.Replace("				<colgroup>", "");
                            file = file.Replace("					<col />", "");
                            file = file.Replace("					<col />", "");
                            file = file.Replace("				</colgroup>", "");

                            file = file.Replace("				<colgroup>", "");
                            file = file.Replace("					<col />", "");
                            file = file.Replace("					<col />", "");
                            file = file.Replace("					<col />", "");
                            file = file.Replace("				</colgroup>", "");

                            file = file.Replace("				<colgroup>", "");
                            file = file.Replace("					<col />", "");
                            file = file.Replace("					<col />", "");
                            file = file.Replace("					<col />", "");
                            file = file.Replace("					<col />", "");
                            file = file.Replace("				</colgroup>", "");

                            file = file.Replace("<td class=\"Tabla-b-sica Tabla\" />", "<td class=\"Tabla-b-sica Tabla\"> </td>");
                            file = file.Replace("                                      </p>", "</p>");
                            file = file.Replace("\n\n\n", "");
                            file = file.Replace("\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\t\t\t", "");
                            file = file.Replace("\r\n\r\n\r\n\r\n\r\n\r\n\r\n\t\t\t", "");
                            file = file.Replace("\r\n\r\n\r\n\r\n\r\n\t\t\t", "");
                            file = file.Replace("\r\n\r\n\r\n\r\n\t\t", "");
                            
                            file = file.Replace("<td class=\"Tabla-b-sica Tabla Tabla\" />", "<td class=\"Tabla-b-sica Tabla Tabla\" > </td>");
                            
                            file = filecontent.quitAccents(file);
                            file = filecontent.findEmail(file, filename);
                            file = filecontent.findUrl(file, filename);

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
                Console.WriteLine(cont + ".-El archivo " + item.Name + " ha sido segmentado Continuar?");
                string w = Console.ReadLine();
                }
                Console.WriteLine("¿Deseas segmentar otro directorio de archivos archivo? S/N");

                s = Console.ReadLine();
            
            } while (s.ToLower() == "s");


        }

    }
}
