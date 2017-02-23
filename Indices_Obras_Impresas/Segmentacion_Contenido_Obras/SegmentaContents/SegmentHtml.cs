using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Xml;
namespace SegmentaContents
{
    public class SegmentHtml
    {
        //public SegmentHtml() { }
        
        public void SegmentaHtml(string Token)
        {
            string tokenAux = "<p class=\"ttulo\"";
            string token = "<p class=\"normal\"><img";
            //string token = "<p class=\"t-tulo";           
            string token2 = "<p class=\"forma-farmaceutica";

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
                    string path2 = @"\\195.192.2.17\Proyectos\Obras PLM\West Indies\West Indies 8\Elementos para CD\HTML\Productos\";

                    //GetHTML filecontent = new GetHTML();

                    //string allcontent = filecontent.getHtmlContent(path);
                    string allcontent = "";//filecontent.getHtmlContent(f.FullName);
                    string template = "";//filecontent.getHtmlContent((temp));
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
                            int init = allcontent.IndexOf(tokenAux, next);
                            //med = allcontent.IndexOf(">", (ini + 18));                        
                            med = allcontent.IndexOf(">", init + 18);
                            //PEV 30
                            //med = ini + token.Length - 1;
                            fin = allcontent.IndexOf("<", med);

                            name = allcontent.Substring(med + 1, (fin - med) - 1);
                            name = "";// filecontent.quitAccentsFileName(name);
                            name = "";// filecontent.cleanName(name);

                            ini2 = allcontent.IndexOf(token2, next);
                            if (ini2 > 0 && ini2 < allcontent.IndexOf(token, fin))
                            {
                                //med2 = allcontent.IndexOf(">", ini2);
                                med2 = allcontent.IndexOf(">", ini2 + 25);
                                //PEV 30
                                //med2 = ini2 + token2.Length -1;
                                fin2 = allcontent.IndexOf("<", med2);
                                pf = allcontent.Substring(med2 + 1, (fin2 - med2) - 1);
                                pf = "";// filecontent.quitAccentsFileName(pf);
                                pf = "";// filecontent.cleanName(pf);
                            }
                            else
                                pf = string.Empty;

                            next = allcontent.IndexOf(token, fin);

                            System.Text.StringBuilder sb = new System.Text.StringBuilder(template);

                            if (next > 0)
                            {
                                string file = allcontent.Substring(ini, (next - ini));

                                file = "";// filecontent.quitAccents(file);
                                file = ""; //filecontent.changeImage(file, filename);
                                file = ""; //filecontent.findEmail(file, filename);
                                file = ""; //filecontent.findUrl(file, filename);

                                inf = "";// filecontent.quitAccents(inf);

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


                                //filecontent.setParameters(sb, "@@@Titulo@@@=" + (name.Trim() + "_" + pf.Trim()));
                                sb.Replace("@@@Contenido@@@", inf + file);

                                inf = "";// filecontent.getString(file, token3);
                            }
                            else
                            {
                                string file = allcontent.Substring(ini);

                                file = "";// filecontent.quitAccents(file);
                                //filecontent.setParameters(sb, "@@@Titulo@@@=" + (name.Trim() + "_" + pf.Trim()));//, "@@@Contenido@@@=" + file); 
                                sb.Replace("@@@Contenido@@@", inf + file);

                                i = allcontent.Length;
                            }

                            if (File.Exists(path2 + "/" + name.Trim() + "_" + pf.Trim() + ".htm"))
                                //filecontent.saveHtmlFile(path2,
                                //name.Trim() + "_" + pf.Trim() + "_" + i.ToString() + ".htm", sb.ToString());

                            //else
                                //filecontent.saveHtmlFile(path2,
                                //name.Trim() + "_" + pf.Trim() + ".htm", sb.ToString());

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

        /// <summary>
        /// Metodo que lee archivo html y lo actualiza en tmpproductContents
        /// </summary>
        /// <param name="NameTableTmp">Nombre de la tabla temporal donde esta el empate de los productos con su HTML</param>
        /// <param name="UrlPath">Ubicacion de los archvivos HTML a insertar</param>
        /// <param name="EditionId">Identificador de la edicion</param>
        public Boolean Step1InsertHTMLfromFile(string NameTableTmp, string UrlPath, int EditionId)
        {
            Boolean Correcto = true;
            int Actualizados = 0, NoActualizados = 0;
            String sSQL = "Select ProductId, PharmaFormId, DivisionId, CategoryId, HTML from " + NameTableTmp;

            IDataReader iDReader = ConnectionDB.ExecuteReader(CommandType.Text, sSQL);

            String pharmaformId, productid, categoryId, divisionId;
            StreamReader SR;
            String text;
            String htmlfile;

            while (iDReader.Read())
            {

                pharmaformId = iDReader["PharmaFormId"].ToString();
                productid = iDReader["ProductId"].ToString();
                htmlfile = iDReader["HTML"].ToString();
                categoryId = iDReader["CategoryId"].ToString();
                divisionId = iDReader["DivisionId"].ToString();

                htmlfile = htmlfile.Trim();

                if (File.Exists(UrlPath + @"\" + htmlfile))
                {
                    SR = File.OpenText(UrlPath + @"\" + htmlfile);
                    text = SR.ReadToEnd();
                    text = text.Replace("'", "\"");

                    String sSQLUpdate = " Update  tmpParticipantProducts " +
                                    " Set HTMLContent = '" + text + "'" +
                                    " Where PharmaFormId = " + pharmaformId +
                                    " And ProductId = " + productid +
                                    " And CategoryId = " + categoryId +
                                    " And DivisionId = " + divisionId +
                                    " And EditionId = " + EditionId +
                                    "  ";
                    Actualizados += ConnectionDB.ExecuteNonQuery(CommandType.Text, sSQLUpdate);
                    Console.WriteLine("Insertado: " + htmlfile);
                }
                else
                {
                    Console.WriteLine("No existe el archivo: -" + htmlfile + "- para ProductId:" + productid + " ,PharmaFormId:" + pharmaformId);
                    Correcto = false;
                    NoActualizados += 1;
                }
                text = "";
                pharmaformId = "";
                productid = "";
                htmlfile = "";
                categoryId = "";
                divisionId = "";

            }
             Console.WriteLine("Se insertaron correctamente -" + Actualizados + "- No se encontraron " + NoActualizados + " Archivos");
            return Correcto;
        }

        /// <summary>
        /// Metodo que extrae de la BD el contenido html y lo crea en una carpeta
        /// </summary>
        /// <param name="EditionId">Identificador de la edicion</param>
        /// <param name="UrlPathDestination">Ruta donde se guardaran los archivos creados</param>
        /// <returns></returns>
        public  Boolean Step2ExportHTMLfromDB(int EditionId, string UrlPathDestination)
        {
            int i = 0;
            Boolean correcto = true;
            try
            {
                String sSQL = "Select * from tmpParticipantProducts  where editionid = " + EditionId + "  ";
                String cadena = "";
                IDataReader iDReader = ConnectionDB.ExecuteReader(CommandType.Text, sSQL);


                while (iDReader.Read())
                {
                    StreamWriter SW;
                    //if (File.Exists(UrlPathDestination + @"\" +  iDReader["ProductId"] + "_" + iDReader["PharmaFormId"] + ".htm"))
                    //{
                    //    SW = File.CreateText(UrlPathDestination + @"\" + iDReader["ProductId"] + "_" + iDReader["PharmaFormId"] + ".html");
                    //    cadena += iDReader["HTMLContent"];
                    //    SW.Write(cadena);
                    //    SW.Close();
                    //    //System.Console.Write("Existe" + iDReader["ProductId"] + "_" + iDReader["PharmaFormId"] + ".htm");
                    //    //System.Console.ReadLine();
                    //}

                    //****** Esta linea es para el vademecum, para que genere los htm completos
               //    SW = File.CreateText(UrlPathDestination + @"\" + iDReader["ProductId"] + "_" + iDReader["PharmaFormId"] + "_" + iDReader["DivisionId"] + ".htm");
                    SW = File.CreateText(UrlPathDestination + @"\" + iDReader["ProductId"] + "_" + iDReader["PharmaFormId"] + ".htm");
                    cadena += iDReader["HTMLContent"];
                    SW.Write(cadena);
                    SW.Close();
                    cadena = "";
                    i++;

                }
                iDReader.Close();
            }
            catch (Exception e )
            {
                Console.WriteLine("Error: " + e.Message);
              
                correcto = false;
            }
            Console.WriteLine("Se crearon correctamente: " + i + " archivos");
            return correcto;
        }
        /// <summary>
        /// Metodo que lee los archivos html y crea los XML correspondientes
        /// </summary>
        /// <param name="UrlPath">Ruta de donde tomara los archivos HTML</param>
        /// <param name="UrlPathDestination">Ruta de donde tomara los archivos HTML</param>
        public  void Step3GenerateXMLfromHTMLDB(string UrlPath, string UrlPathDestination)
        {
            DirectoryInfo dir = new System.IO.DirectoryInfo(UrlPath + @"\");
            String nameFile = "";
            int x = 0;
            foreach (FileInfo f in dir.GetFiles("*.htm"))
            {
                x++;
                nameFile = f.Name;
                nameFile = nameFile.Replace(".htm", "");
                setXMLFile(f.DirectoryName + "\\" + f.Name, nameFile, UrlPathDestination);
                Console.WriteLine(x + ".- " + f.Name + " Procesado");
            }

        
        }
        /// <summary>
        /// Metodo para verificar consistencia de archivos
        /// </summary>
        /// <param name="urlPath">Ruta donde se encuentran los archivos xml</param>
        /// <returns></returns>
        public  Boolean Step4VerifyXML(string urlPath)
        {
            Boolean correcto = true;
            DirectoryInfo dir = new System.IO.DirectoryInfo(urlPath + @"\");
            foreach (FileInfo f in dir.GetFiles("*.xml"))
            {
                try
                {
                    XmlDocument cdoc = new XmlDocument();
                    cdoc.Load(f.FullName);
                }
                catch (Exception)
                {
                    correcto = false;
                    Console.WriteLine("Error en el archivo:   " + f.Name);
                }

            }
            return correcto;
        }
        /// <summary>
        /// Metodo para insertar los contenidos XML
        /// </summary>
        /// <param name="urlPath">Ruta de donde se tomaran los xml a insertar</param>
        /// <param name="editionId">Identificador de la edicion</param>
        /// <returns></returns>
        public  Boolean Step5InsertXMLinDB(string urlPath, int editionId)
        {
            DirectoryInfo dir = new System.IO.DirectoryInfo(urlPath);
            String nameFile = "";
            Boolean correcto = true;
            int inserted = 0;
            foreach (FileInfo f in dir.GetFiles("*.xml"))
            {
                nameFile = f.Name;
                nameFile = nameFile.Replace(".xml", "");
                try
                {
                    inserted += getDataFromFile(f.DirectoryName + "\\" + f.Name, nameFile, editionId);
                }
                catch (Exception)
                {
                    correcto = false;
                }
            }
            Console.WriteLine("Se insertaron " + inserted + " Xml's");
            return correcto;
        }

        public DataSet Step6VerificaRubros(int edition)
       //  public DataSet Step6VerificaRubros(int edition)
        {

            String titulo = "<Titulo>";
            String tmp = "";
            String productId = "";
            String pharmaId = "";
            String editionId = "";
            String divisionId = "";
            String categoryId = "";
            
            int i, fin;

            String S = "";
            String attributeDescription = "";
            string sSQL = "Select EditionId, PharmaFormId, ProductId, DivisionId, CategoryId, XMLContent from tmpParticipantProducts  where editionId= " + edition + "  ";

            IDataReader Reader = ConnectionDB.ExecuteReader(CommandType.Text, sSQL);
            int count = 0;
            //sSQL = "delete from tmpAttribute";
            sSQL = "delete from tmpAttributes";
            ConnectionDB.ExecuteNonQuery(CommandType.Text, sSQL);
            while (Reader.Read())
            {
                count++;
                S = Reader["XMLContent"].ToString();
                productId = Reader["ProductId"].ToString();
                pharmaId = Reader["PharmaFormId"].ToString();
                editionId = Reader["EditionId"].ToString();
                divisionId = Reader["DivisionId"].ToString();
                categoryId = Reader["CategoryId"].ToString();
                tmp = S;
                while (S != null)
                {

                    i = S.IndexOf("<Titulo>");

                    if (i >= 0)
                    {
                        fin = S.IndexOf("</Titulo>", i);
                        attributeDescription = S.Substring(i + titulo.Length, fin - (i + titulo.Length));
                        S = S.Substring(fin);
                        if (attributeDescription != "")
                        {
                            //sSQL = "INSERT INTO tmpAttribute " +
                            sSQL = "INSERT INTO tmpAttributes " +
                                       "(Description,info) " +
                                      
                                       " VALUES " +
                            "('" + attributeDescription.Replace(":", "").Trim().Replace(";", "").Trim() + "','" + productId + "_" + pharmaId + "')";
                          //  "('" + attributeDescription.Replace(":", "").Trim().Replace(";", "").Trim().Replace(".", "") + "','" + productId + "_" + pharmaId + "')";
                            ConnectionDB.ExecuteNonQuery(CommandType.Text, sSQL);
                        }
                    }
                    else S = null;
                }
            }
            //sSQL = "select distinct  t.* from tmpAttribute t " +
            sSQL = "select distinct  t.* from tmpAttributes t " +
                    "left join Attributes at on t.Description=at.Description " +
                    "where at.Description is null";
            DataSet ds = new DataSet();
            ds = ConnectionDB.ExecuteDataSet(CommandType.Text, sSQL);
          
            return ds;
        }


        public void Step7LlenaContenidos(int edition)
       // public void Step7LlenaContenidos(int edition)
        {

            String titulo = "<Titulo>";
            String contenido = "<Contenido>";
            String tmp = "";
            String productId = "";
            String pharmaId = "";
            String editionId = "";
            String divisionId = "";
            String categoryId = "";
            String plainContent = "";
            String content = "";
            int i, fin, iContent, fContent;

            String S = "";
            String attributeDescription = "";
            String attributeId = "";

            string sSQL = "Select EditionId, PharmaFormId, ProductId, DivisionId, CategoryId, XMLContent from  tmpParticipantProducts where editionId= " + edition + "  ";

            IDataReader Reader = ConnectionDB.ExecuteReader(CommandType.Text, sSQL);
            int count = 0;
           // sSQL = "delete from tmpproductContentsVMG10";
            sSQL = "delete from dbo.tmpProductContents";
            ConnectionDB.ExecuteNonQuery(CommandType.Text, sSQL);
            while (Reader.Read())
            {
                count++;
                S = Reader["XMLContent"].ToString();
                productId = Reader["ProductId"].ToString();
                pharmaId = Reader["PharmaFormId"].ToString();
                editionId = Reader["EditionId"].ToString();
                divisionId = Reader["DivisionId"].ToString();
                categoryId = Reader["CategoryId"].ToString();
                tmp = S;
                while (S != null)
                {

                    i = S.IndexOf("<Titulo>");

                    if (i >= 0)
                    {
                        fin = S.IndexOf("</Titulo>", i);
                        attributeDescription = S.Substring(i + titulo.Length, fin - (i + titulo.Length));
                        S = S.Substring(fin);
                        if (attributeDescription != "")
                        {
                            
                           // sSQL = "Select top 1 AttributeId from Attributes where Description = '" + attributeDescription.Replace(":", "").Replace(";", "").Replace(".","").Trim() + "'";
                            sSQL = "Select top 1 AttributeId from Attributes where Description = '" + attributeDescription.Replace(":", "").Replace(";", "").Trim() + "'";
                            IDataReader Reader1 = ConnectionDB.ExecuteReader(CommandType.Text, sSQL);

                            while (Reader1.Read())
                            {
                                attributeId = Reader1["AttributeId"].ToString();
                            }
                            Reader1.Close();
                            attributeDescription = "";

                        }

                        iContent = S.IndexOf("<Contenido>", 0);
                        if (iContent >= 0)
                        {
                            fContent = S.IndexOf("</Contenido>", iContent);
                            content = S.Substring(iContent + contenido.Length, fContent - (iContent + contenido.Length));
                            content = content.Trim();

                            S = S.Substring(iContent, S.Length - iContent);
                        }
                        else S = null;

                        if (attributeId != "" && content != "")
                        {

                            plainContent = content;
                            plainContent = cleanImage(plainContent);
                            plainContent = cleanTable(plainContent);


                           //    sSQL = "INSERT INTO [tmpproductContentsVMG10] " +
                            sSQL = "INSERT INTO dbo.tmpProductContents " +
                                    "       ([ProductId] " +
                                    "       ,[AttributeId] " +
                                    "       ,[Content] " +
                                    "       ,[EditionId] " +
                                    "       ,[PharmaFormId] " +
                                    "       ,[PlainContent] " +
                                    "       ,[DivisionId] " +
                                    "       ,[CategoryId])" +
                                    " VALUES " +
                                    "       (" + productId +
                                    "       ," + attributeId +
                                    "       ,'" + content + "'" +
                                    "       ," + editionId +
                                    "       ," + pharmaId +
                                    "       ,'" + plainContent + "'" +
                                    "       ," + divisionId +
                                    "       ," + categoryId + ")";

                            ConnectionDB.ExecuteNonQuery(CommandType.Text, sSQL);

                            //productId = "";
                            attributeId = "";
                            content = "";
                            //editionId = "";
                            //pharmaId = "";
                            plainContent = "";
                            //divisionId = "";
                            //categoryId = "";

                        }
                        else
                        {
                            Console.WriteLine("________" + attributeId + "_______" + productId + "____" + pharmaId + "___");
                        }
                    }
                    else S = null;
                }
            }
           
           // return ds;
        }

        public void Step8LlenaHTMLContenidos(string urlHTML) {

            DirectoryInfo dir = new System.IO.DirectoryInfo(urlHTML);
            String nameFile = "";
            foreach (FileInfo f in dir.GetFiles("*.htm"))
            {
                nameFile = f.Name;
                nameFile = nameFile.Replace(".htm", "");
                
                insertHTMLContent(f.DirectoryName + "\\" + f.Name, nameFile);
            }
        
        }
        public static void insertHTMLContent(String file, String nameFile)
        {

            String[] split = nameFile.Split('_');
            String productId = split[0];
            String pharmaId = split[1];

            //String rubro = getAttribute(file, productId, pharmaId);
            getAttribute(file, productId, pharmaId);
        }
        public static void getAttribute(String F, String productId, String pharmaId)
        {
            StreamReader SR;
            String S;
            String rubro = "";
           //String marcador = "<p class=\"normal-normal\"><span class=\"normal-normal-color\"";
            //String marcador = "<p class=" + "\"normal" + "\"><span class=" + "\"rubros-azules" + "\">";
            //String marcador = "<span class=" + "\"rubros-azules" + "\"";
           // String marcador ="<p class=\"normal\"><span class=\"letra-cyan";
            //String marcador = "<p class=\"normal\"><span class=\"bold-entrada\"";
            String marcador = "<p class=\"Normal\"><span class=\"Rubros-azules\""; 

          // String marcador = "<p class=\"texto\"><span class=\"indice\""; //***OTC***


            //String marcador = "<p class=\"normal\"><span class=\"subt-tulo";                               
            //String marcador = "<span class=\"x1b-atributo";
            //String marcador = "<p class=\"x5a-texto-normal\"><span class=\"x1b-atributo\">";
            //String marcador = "<p class=\"normal\"><span class=\"subt-tulo\"";
            //String marcador = "<p class=\"normal\"><span class=\"rubros-azules\"";
            String marcadorF = "</span>";
            String rubroNombre = " ";
            String rubroContenido = null;
            String rubroC = "";
            int i = 0, f = 0, iaux = 0, faux = 0;
            SR = File.OpenText(F);
            S = SR.ReadToEnd();
            //S = S.Replace("\t", "");

            String sSQL = "";
            String sSQLUpdate = "";
            String attributeId = "";

            int ff = 0;

            while (S != null)
            {
                i = S.IndexOf(marcador);
                if (i >= 0)
                {
                    f = S.IndexOf(marcadorF, i);
                    //ff = S.IndexOf(">", i);
                    ff = S.IndexOf(">", i + marcador.Length);
                    //rubroNombre = S.Substring(i + marcador.Length, f - (i + marcador.Length));
                    rubroNombre = S.Substring(ff + 1, f - (ff + 1));
                    rubroNombre = cleanRubro(rubroNombre);

                    iaux = S.IndexOf(marcador, f);

                    if (iaux >= 0)
                    {
                        rubroContenido = S.Substring(i, iaux - i);
                        S = S.Substring(iaux, S.Length - iaux);
                    }
                    else
                    {
                        rubroContenido = S.Substring(i, S.Length - i);
                        S = null;
                    }

                    rubroContenido = rubroContenido.Replace("</div>", "");
                    rubroContenido = rubroContenido.Replace("</body>", "");
                    rubroContenido = rubroContenido.Replace("</html>", "");
                    rubroContenido = rubroContenido.Replace("'", "\"");


                    sSQL = "Select top 1 AttributeId from Attributes where Description = '" + rubroNombre.Replace(":", "").Replace(";", "").Trim() + "'";

                    IDataReader iDReader = ConnectionDB.ExecuteReader(CommandType.Text, sSQL);

                    while (iDReader.Read())
                    {

                        attributeId = iDReader["attributeId"].ToString();


                        //  sSQLUpdate = "Update tmpproductContentsVMG10 " +
                        sSQLUpdate = "Update dbo.tmpProductContents " +
                                     " set HTMLContent = '" + rubroContenido + "'" +
                                     " where pharmaFormId = " + pharmaId +
                                     " and productId = " + productId +
                                     " and attributeId = " + attributeId +
                                     "  ";

                        ConnectionDB.ExecuteNonQuery(CommandType.Text, sSQLUpdate);

                    } iDReader.Close();
                }
                else S = null;
            }
            SR.Close();
            
        }

        public static String cleanRubro(String rubroNombre)
        {
            rubroNombre = rubroNombre.Replace("&aacute;", "á");
            rubroNombre = rubroNombre.Replace("&eacute;", "é");
            rubroNombre = rubroNombre.Replace("&iacute;", "í");
            rubroNombre = rubroNombre.Replace("&oacute;", "ó");
            rubroNombre = rubroNombre.Replace("&uacute;", "ú");

            rubroNombre = rubroNombre.Replace("&Aacute;", "Á");
            rubroNombre = rubroNombre.Replace("&Eacute;", "É");
            rubroNombre = rubroNombre.Replace("&Iacute;", "Í");
            rubroNombre = rubroNombre.Replace("&Oacute;", "Ó");
            rubroNombre = rubroNombre.Replace("&Uacute;", "Ú");

            rubroNombre = rubroNombre.Replace("&Ntilde;", "Ñ");
            rubroNombre = rubroNombre.Replace("&ntilde;", "ñ");
            rubroNombre = rubroNombre.Replace("&NTILDE;", "Ñ");

            rubroNombre = rubroNombre.Replace("&uml;", "ü");
            rubroNombre = rubroNombre.Replace("&Uml;", "Ü");
            rubroNombre = rubroNombre.Replace("&uuml;", "ü");
            rubroNombre = rubroNombre.Replace("&Uuml;", "Ü");

            rubroNombre = rubroNombre.Replace("<BR />", " ");
            rubroNombre = rubroNombre.Replace("&nbsp;", " ");
            rubroNombre = rubroNombre.Replace("&NBSP;", " ");
            rubroNombre = rubroNombre.Replace("<br />", " ");
            rubroNombre = rubroNombre.Replace("<br />", " ");
            rubroNombre = rubroNombre.Replace("&#174;", "®");
            rubroNombre = rubroNombre.Replace("¡", "");
            rubroNombre = rubroNombre.Replace("!", "");

            rubroNombre = rubroNombre.Replace(":", "");
            //rubroNombre = rubroNombre.Replace(".", "");
            //rubroNombre = rubroNombre.Replace(",", "");
            //rubroNombre = rubroNombre.Replace(";", "");

            rubroNombre = rubroNombre.ToUpper();
            rubroNombre = rubroNombre.Trim();

            return rubroNombre;
        }
        public static void setXMLFile(String file, String nameFile, string UrlPath)
        {
            String xml = "<?xml version=" + "\"1.0" + "\" encoding=" + "\"UTF-8" + "\" standalone=" + "\"yes" + "\"?>";
            String rootIni = "<Root>";
            String rootFin = "</Root>";
            String f = getProduct(file);
            String reg = getReg(file);
            StreamWriter SW;
            String forma = getPharmaForm(file);
            String subs = getSubstance(file);
            String rubro = getAttribute(file);
            String info = getTypeInformation(file);
            String prop = getPropaganda(file);
            String lab = getLaboratory(file);

            f = f.Trim();
            forma = forma.Trim();
            f = f.Replace("+", "");

            f = f.Replace("*", "");
            forma = forma.Replace("+", "");
            forma = forma.Replace("*", "");
            //cleanAttribute(rubro);            

            SW = File.CreateText(UrlPath + @"\" + nameFile + ".xml");
            SW.Write(xml);
            SW.Write(rootIni);
            SW.Write("<Producto>");

            if (info != "")
            {
                SW.Write(info);
            }
            if (f != "")
            { SW.Write(f); }
            if (reg != "")
            { SW.Write(reg); }
            if (forma != "")
            { SW.Write(forma); }
            if (subs != "")
            { SW.Write(subs); }
            if (prop != "")
            { SW.Write(prop); }
            if (rubro != "")
            { SW.Write(rubro); }
            if (lab != "")
            { SW.Write(lab); }
            SW.Write("</Producto>");
            SW.Write(rootFin);
            SW.Close();
        }
        #region ReturnXML

        public static String getAttribute(String F)
        {
            StreamReader SR;
            String S;
            String rubro = "";
            String info = "";
            String marcador = "<p class=\"Normal\"><span class=\"Rubros-azules\"";

           // String marcador = "<p class=\"texto\"><span class=\"indice\""; //***OTC***

                //String marcador = "<p class=\"normal\"><span class=\"rubros-azules\"";
           // String marcador = "<p class=\"normal\"><span class=\"bold-entrada\"";
            //String marcador = "<p class=\"normal-normal\"><span class=\"normal-normal-color\"";
            //String marcador = "<span class=\"x1b-atributo";
            //String marcador = "<p class=\"normal\"><span class=\"subt";
            //String marcador = "<p class=\"normal\"><span class=\"bold";
            //String marcador = "<p class=\"normal\"><span class=\"negro-normal\"";                 
            //String marcador = "<p class=\"normal-normal\"><span class=\"normal-normal-color\"";
            //String marcador = "<p class=\"normal\"><span class=\"rubros-azules\"";                              
            //String marcador = "<p class=\"normal\">
            //String marcador = "<p class=\"normal\"><span class=\"letra-cyan\"";
            //String marcador = "<p class=\"normal\"><span class=\"letra-cyan-2\"";                   
            //String marcador = "<p class=\"normal-normal\"><span class=\"normal-normal-color\"";
            //String marcador = "<p class=\"normal\"><span class=\"subtitulo\"";
            //String marcador = "<p class=\"normal\"><span class=\"bold-entrada\"";
            //String marcador = "<p class=\"x5a-texto-normal\"><span class=\"x1b-atributo\"";
            //String marcador = "<p class=\"x4-body-text\"><span class=\"subt-tulo\"";
            //String marcador=  "<p class=\"normal\"><span class=\"subt-tulo\"";

            String marcadorF = "</span>";
            String rubroNombre = " ";
            String rubroContenido = null; ;
            String rubroC = "";
            int i = 0, f = 0, iaux = 0, faux = 0;
            SR = File.OpenText(F);
            S = SR.ReadToEnd();
            S = S.Replace("\t", "");
            int pie = 0;
            String marcadorPie = "<p class=\"Pie1";
            //String marcadorPie = "<p class=\"x9a-pie-empresa";
            //String marcadorPie = "<p class=\"pie-2";
            while (S != null)
            {
                i = S.IndexOf(marcador);
                //if (i == -1)
                //{
                //    String marcador = "<p class=\"normal\"><span class=\"normal-normal-color\"";
                //}
                if (i >= 0)
                {
                    f = S.IndexOf(marcadorF, i);
                    faux = S.IndexOf(">", i + marcador.Length);
                    info = S.Substring(faux + 1, f - (faux + 1));
                    rubroNombre = info;//S.Substring(i + marcador.Length, f - (i + marcador.Length));

                    iaux = S.IndexOf(marcador, f);

                    if (iaux >= 0)
                    {
                        rubroContenido = S.Substring(f + marcadorF.Length, iaux - (f + marcadorF.Length));
                        S = S.Substring(iaux, S.Length - iaux);
                    }
                    else
                    {
                        pie = S.IndexOf(marcadorPie, f);
                        if (pie >= 0)
                            rubroContenido = S.Substring(f, pie - f);
                        else
                            rubroContenido = S.Substring(f, S.Length - f);
                        S = null;
                    }
                    if (rubroNombre != " ")
                    {
                        rubroC += "<Rubro>";
                        rubroC += "<Titulo>" + rubroNombre + "</Titulo>";
                        rubroC += "<Contenido>" + rubroContenido + "</Contenido></Rubro>";
                    }
                }
                else S = null;

            }
            if (rubroC != "")
            {
                rubroC = getTableByAttribute(rubroC);
                rubroC = cleanFileHTML(rubroC);
            }

            SR.Close();

            return rubroC;
        }
        public static String getLaboratory(String file)
        {
            StreamReader SR;

            int i, f;
            int faux;

            String S;
            String imagen = "";
            String imaFin = " />";
            String lab = "";
            int iIma, fIma;
            String imag = "";
            String marcadorLab = "<p class=\"Pie\"";
            String marcadorLab2 = "<p class=\"Pie1\"";
            //String marcadorLab = "<p class=\"x9a-pie-empresa";
            //String marcadorLab = "<p class=\"final-pagina\"><span class=\"boldcursiva\">";
            //String marcadorLab = "<p class=\"x6-pie-2\"";

            String laboratorio = "";

            SR = File.OpenText(file);
            S = SR.ReadToEnd();

            while (S != null)
            {
                i = S.IndexOf(marcadorLab2);
                if (i >= 0)
                {
                    f = S.IndexOf("</p>", i);
                    faux = S.IndexOf(">", i);
                    //lab = S.Substring(faux + 1, f - (faux + 1));
                    lab = S.Substring(faux + 1, S.Length - (faux + 1));
                    laboratorio = lab;
                    while (lab != null)
                    {
                        iIma = lab.IndexOf("<img");
                        if (iIma >= 0)
                        {
                            fIma = lab.IndexOf(" />", iIma);
                            imagen = lab.Substring(iIma, (fIma + imaFin.Length) - iIma);
                            lab = lab.Replace(imagen, "");
                            laboratorio = lab;
                            imag += imagen;

                        }
                        else lab = null;

                    }
                    S = null;
                }
                else
                {
                    i = S.IndexOf(marcadorLab);
                    if (i >= 0)
                    {
                        f = S.IndexOf("</p>", i);
                        faux = S.IndexOf(">", i);
                        //lab = S.Substring(faux + 1, f - (faux + 1));
                        lab = S.Substring(faux + 1, S.Length - (faux + 1));
                        laboratorio = lab;
                        while (lab != null)
                        {
                            iIma = lab.IndexOf("<img");
                            if (iIma >= 0)
                            {
                                fIma = lab.IndexOf(" />", iIma);
                                imagen = lab.Substring(iIma, (fIma + imaFin.Length) - iIma);
                                lab = lab.Replace(imagen, "");
                                laboratorio = lab;
                                imag += imagen;

                            }
                            else lab = null;

                        }
                        S = null;
                    }
                    else S = null;
                }

            }
            SR.Close();
            if (laboratorio != "")
            {
                laboratorio = "<Laboratorio>" + laboratorio + "</Laboratorio>";

                if (imag != "")
                {
                    laboratorio += imag;
                }
                laboratorio = getTableByAttribute(laboratorio);
                laboratorio = cleanFileHTML(laboratorio);
            }


            return laboratorio;

        }
        public static String getProduct(String file)
        {
            StreamReader SR;

            int i, f;
            int faux;

            String S;
            String imagen = "";
            String imaFin = " />";
            String nombre = "";
            int iIma, fIma;
            String imag = "";
            String marcadorName = "<p class=\"T-tulo";
            //String marcadorName = "<p class=\"t-tulo-prod-nuevo";

            //String marcadorName = "<p class=\"nombre-del-producto"; //***OTC***

           // String marcadorName = "<p class=\"t-tulo";
            //String marcadorName = "<p class=\"x1a-nombre-prod\"><span class=\"x1a-bold-titulos\"";
            //String marcadorName = "<p class=\"titulo";
            //String marcadorName = "<p class=\"titulo\"><span class=\"boldcursiva";
            //String marcadorName = "<p class=\"x1-nombre-producto";
            String nombreProducto = "";

            SR = File.OpenText(file);
            S = SR.ReadToEnd();

            while (S != null)
            {
                i = S.IndexOf(marcadorName);
                if (i >= 0)
                {
                    f = S.IndexOf("</p>", i);
                    faux = S.IndexOf(">", i);
                    nombre = S.Substring(faux + 1, f - (faux + 1));
                    nombreProducto = nombre;
                    while (nombre != null)
                    {
                        iIma = nombre.IndexOf("<img");
                        if (iIma >= 0)
                        {
                            fIma = nombre.IndexOf(" />", iIma);
                            imagen = nombre.Substring(iIma, (fIma + imaFin.Length) - iIma);
                            nombre = nombre.Replace(imagen, "");
                            nombre = nombre.Replace("<span class=\"bold\">", "");
                            nombreProducto = nombre;
                            imag += imagen;

                        }
                        else nombre = null;

                    }
                    S = null;
                }
                else S = null;
            }
            SR.Close();
            if (nombreProducto != "")
            {
                nombreProducto = "<Nombre>" + nombreProducto + "</Nombre>";
                if (imag != "")
                {
                    nombreProducto += imag;
                }
                nombreProducto = cleanFileHTML(nombreProducto);
            }

            return nombreProducto;
        }
        public static String getPharmaForm(String file)
        {
            StreamReader SR;

            String S;
            String imagen = "";
            String imaFin = " />";
            String pharma = "";
            String marcadorPharma = "<p class=" + "\"Forma-farmac-" + "\">";

            //String marcadorPharma = "<p class=" + "\"forma-farmaceutica" + "\">"; //***OTC***

            //String marcadorPharma = "<p class=\"x2a-forma-farmaceutica\">";
            //String marcadorPharma = "<p class=\"base-de-propaganda-integrado\"><span class=\"forma-farmace-tica\">";
            String marcadorPharmaA = "<p class=\"forma-farmaceutica\"><span class=\"boldcursiva\">";
            //String marcadorPharma = "<p class=\"forma-farmaceutica\"><span class=\"forma-cursiva\">";  
            String marcadorPharmaB = "<p class=\"forma-farmaceutica\"><span class=\"base-boldcursivo\">";
            //   "<p class=\"forma-farmaceutica\"><span class=\"base-boldcursivo\">"
            //String marcadorPharma = "<p class=\"x2-presenta\">";
            String imag = "";
            String pharmaForm = "";

            int i, f, iIma, fIma;

            SR = File.OpenText(file);
            S = SR.ReadToEnd();
            S = S.Replace("<span class=" + "\"cursivo-normal" + "\">", "");
            S = S.Replace("<span class=\"x2b-cursivas\">", "");
            S = S.Replace("</span>", "");


            while (S != null)
            {
                i = S.IndexOf(marcadorPharma);

                if (i >= 0)
                {
                    f = S.IndexOf("</p>", i + marcadorPharma.Length);
                    pharma = S.Substring(i + marcadorPharma.Length, f - (i + marcadorPharma.Length));
                    pharmaForm = pharma;
                    while (pharma != null)
                    {
                        iIma = pharma.IndexOf("<img");
                        if (iIma >= 0)
                        {
                            fIma = pharma.IndexOf(" />", iIma);
                            imagen = pharma.Substring(iIma, (fIma + imaFin.Length) - iIma);
                            pharma = pharma.Replace(imagen, "");
                            pharmaForm = pharma;
                            imag += imagen;
                        }
                        else pharma = null;

                    }
                    S = null;
                }
                //Codigo auxiliar
                else
                {
                    i = S.IndexOf(marcadorPharmaA);

                    if (i >= 0)
                    {
                        f = S.IndexOf("</p>", i + marcadorPharmaA.Length);
                        pharma = S.Substring(i + marcadorPharmaA.Length, f - (i + marcadorPharmaA.Length));
                        pharmaForm = pharma;
                        while (pharma != null)
                        {
                            iIma = pharma.IndexOf("<img");
                            if (iIma >= 0)
                            {
                                fIma = pharma.IndexOf(" />", iIma);
                                imagen = pharma.Substring(iIma, (fIma + imaFin.Length) - iIma);
                                pharma = pharma.Replace(imagen, "");
                                pharmaForm = pharma;
                                imag += imagen;
                            }
                            else pharma = null;

                        }
                        S = null;
                    }
                    else
                    {
                        i = S.IndexOf(marcadorPharmaB);

                        if (i >= 0)
                        {
                            f = S.IndexOf("</p>", i + marcadorPharmaB.Length);
                            pharma = S.Substring(i + marcadorPharmaB.Length, f - (i + marcadorPharmaB.Length));
                            pharmaForm = pharma;
                            while (pharma != null)
                            {
                                iIma = pharma.IndexOf("<img");
                                if (iIma >= 0)
                                {
                                    fIma = pharma.IndexOf(" />", iIma);
                                    imagen = pharma.Substring(iIma, (fIma + imaFin.Length) - iIma);
                                    pharma = pharma.Replace(imagen, "");
                                    pharmaForm = pharma;
                                    imag += imagen;
                                }
                                else pharma = null;
                            }
                            S = null;
                        }
                        else S = null;
                    }

                }
            }
            SR.Close();

            if (pharmaForm != "")
            {
                pharmaForm = "<FormaFarmaceutica>" + pharmaForm + "</FormaFarmaceutica>";
                if (imag != "")
                {
                    pharmaForm += imag;
                }
                pharmaForm = cleanFileHTML(pharmaForm);
            }

            return pharmaForm;
        }
        public static String getSubstance(String file)
        {
            StreamReader SR;

            String S;
            String imagen = "";
            String imaFin = " />";
            String subs = "";
            //String marcadorSubs = "<p class="+"\"ingrediente"+"\">";
            String marcadorSubs = "<p class=\"Ingrediente\">";

            //String marcadorSubs = "<p class=\"sustancia-del-producto\">"; //***OTC***


            //String marcadorSubs = "<p class=\"x3a-ingrediente\">";
            //String marcadorSubs = "<p class=\"x3-indic-o-sust-\">";

            String substance = "";
            String imag = "";

            int i, f, iIma, fIma;

            SR = File.OpenText(file);
            S = SR.ReadToEnd();

            while (S != null)
            {
                i = S.IndexOf(marcadorSubs);

                if (i >= 0)
                {
                    f = S.IndexOf("</p>", i + marcadorSubs.Length);
                    subs = S.Substring(i + marcadorSubs.Length, f - (i + marcadorSubs.Length));
                    substance = subs;
                    while (subs != null)
                    {
                        iIma = subs.IndexOf("<img");
                        if (iIma >= 0)
                        {
                            fIma = subs.IndexOf(" />", iIma);
                            imagen = subs.Substring(iIma, (fIma + imaFin.Length) - iIma);
                            subs = subs.Replace(imagen, "");
                            substance = subs;
                            imag += imagen;
                        }
                        else subs = null;

                    }
                    S = null;
                }
                else S = null;
            }

            SR.Close();

            if (substance != "")
            {
                substance = "<SustanciaActiva>" + substance + "</SustanciaActiva>";
                if (imag != "")
                {
                    substance += imag;
                }
                substance = cleanFileHTML(substance);
            }

            return substance;
        }
        public static String getTypeInformation(String file)
        {

            StreamReader SR;

            String S;
            String info = "";
            //String marcador = "<p class=" + "\"medio-de-d-";
            String marcador = "<p class=\"antetitulo\"";
            String imagen = "";
            String imaFin = " />";
            String tipoInfo = "";
            String imag = "";

            int i, f, iIma, fIma, faux;

            SR = File.OpenText(file);
            S = SR.ReadToEnd();

            while (S != null)
            {
                i = S.IndexOf(marcador);
                if (i >= 0)
                {
                    f = S.IndexOf("<", i + marcador.Length);
                    faux = S.IndexOf(">", i + marcador.Length);
                    info = S.Substring(faux + 1, f - (faux + 1));
                    tipoInfo = info;
                    while (info != null)
                    {
                        iIma = info.IndexOf("<img");
                        if (iIma >= 0)
                        {
                            fIma = info.IndexOf(" />", iIma);
                            imagen = info.Substring(iIma, (fIma + imaFin.Length) - iIma);
                            info = info.Replace(imagen, "");
                            tipoInfo = info;
                            imag += imagen;
                        }
                        else info = null;

                    }
                    S = null;
                }
                else S = null;
            }
            SR.Close();

            if (tipoInfo != "")
            {
                tipoInfo = "<TipoInformacion>" + tipoInfo + "</TipoInformacion>";
                if (imag != "")
                {
                    tipoInfo += imag;
                }
                tipoInfo = cleanFileHTML(tipoInfo);
            }

            return tipoInfo;
        }
        public static String getPropaganda(String file)
        {
            StreamReader SR;

            String S;
            String propaganda = "";
            String imagen;
            //String marcadorProp = "<p class="+"\"base-de-prop-"+"\">";
            String marcadorProp = "<p class=\"base-de-prop-\">";

           // String marcadorProp = "<span class=\"padecimiento\">"; //***OTC***


            //String marcadorProp = "<p class=\"x4-base-de-prop\">";
            String marcadorPropB = "<p class=\"base-de-propaganda\"><span class=\"cursiva\">";
            //String marcadorProp = "<p class=\"base-de-propaganda\"><span class=\"base-boldcursivo\">";

            String imaIni = "<img";
            String imaFin = " />";
            String baseprop = "";
            String imag = "";

            int i, f, iIma, fIma;

            SR = File.OpenText(file);
            S = SR.ReadToEnd();

            while (S != null)
            {

                i = S.IndexOf(marcadorProp);

                if (i >= 0)
                {
                    f = S.IndexOf("</p>", i + marcadorProp.Length);
                    propaganda = S.Substring(i + marcadorProp.Length, f - (i + marcadorProp.Length));
                    baseprop = propaganda;
                    while (propaganda != null)
                    {
                        iIma = propaganda.IndexOf("<img");
                        if (iIma >= 0)
                        {
                            fIma = propaganda.IndexOf(" />", iIma);
                            imagen = propaganda.Substring(iIma, (fIma + imaFin.Length) - iIma);
                            propaganda = propaganda.Replace(imagen, "");
                            baseprop = propaganda;
                            imag += imagen;
                        }
                        else propaganda = null;

                    }
                    S = null;
                }
                else
                {
                    i = S.IndexOf(marcadorPropB);

                    if (i >= 0)
                    {
                        f = S.IndexOf("</p>", i + marcadorPropB.Length);
                        propaganda = S.Substring(i + marcadorPropB.Length, f - (i + marcadorPropB.Length));
                        baseprop = propaganda;
                        while (propaganda != null)
                        {
                            iIma = propaganda.IndexOf("<img");
                            if (iIma >= 0)
                            {
                                fIma = propaganda.IndexOf(" />", iIma);
                                imagen = propaganda.Substring(iIma, (fIma + imaFin.Length) - iIma);
                                propaganda = propaganda.Replace(imagen, "");
                                baseprop = propaganda;
                                imag += imagen;
                            }
                            else propaganda = null;

                        }
                        S = null;
                    }
                    else S = null;
                }

            }
            SR.Close();

            if (baseprop != "")
            {
                baseprop = "<BasePropaganda>" + baseprop + "</BasePropaganda>";
                if (imag != "")
                {
                    baseprop += imag;
                }
                baseprop = cleanFileHTML(baseprop);
            }

            return baseprop;

        }
        public static String getReg(String file)
        {
            StreamReader SR;

            String S;
            String registro = "";
            String marcadorReg = "<p class=\"reg-sarh\">";
            String baseReg = "";
            int i, f;

            SR = File.OpenText(file);
            S = SR.ReadToEnd();

            while (S != null)
            {
                i = S.IndexOf(marcadorReg);

                if (i >= 0)
                {
                    f = S.IndexOf("</p>", i + marcadorReg.Length);
                    registro = S.Substring(i + marcadorReg.Length, f - (i + marcadorReg.Length));
                    baseReg = registro;
                    S = null;
                }
                else S = null;
            }
            SR.Close();

            if (baseReg != "")
            {
                baseReg = "<Registro>" + baseReg + "</Registro>";

                baseReg = cleanFileHTML(baseReg);
            }

            return baseReg;

        }
        public static String cleanFileHTML (String cadena)
        {
            cadena = cadena.Replace("&aacute;", "á");
            cadena = cadena.Replace("&eacute;", "é");
            cadena = cadena.Replace("&iacute;", "í");
            cadena = cadena.Replace("&oacute;", "ó");
            cadena = cadena.Replace("&uacute;", "ú");

            cadena = cadena.Replace("&Aacute;", "Á");
            cadena = cadena.Replace("&Eacute;", "É");
            cadena = cadena.Replace("&Iacute;", "Í");
            cadena = cadena.Replace("&Oacute;", "Ó");
            cadena = cadena.Replace("&Uacute;", "Ú");

            cadena = cadena.Replace("&#174;", "® ");
            cadena = cadena.Replace("<p class=" + "\"medio-de-d-prod-nuevo" + "\">", "\n");
            cadena = cadena.Replace("&lt;", "");
            cadena = cadena.Replace("<p class=" + "\"medio-de-d-" + "\">", "");
            cadena = cadena.Replace("&gt;", "");
            cadena = cadena.Replace("<br />", " ");
            cadena = cadena.Replace("<span class=" + "\"tabla" + "\">", "");
            cadena = cadena.Replace("<div class=" + "\"story" + "\">", "");
            cadena = cadena.Replace("<p class=" + "\"corniza-derecha" + "\">", "");
            cadena = cadena.Replace("<p class=" + "\"corniza-izquierda" + "\">", "");
            cadena = cadena.Replace("<div class=" + "\"group" + "\">", "");
            cadena = cadena.Replace("<p>", "");
            cadena = cadena.Replace("<span class=" + "\"rubros-azules" + "\">", "");
            cadena = cadena.Replace("<span class=" + "\"cursivas-normal" + "\">", "");
            cadena = cadena.Replace("<span class=" + "\"resaltado" + "\">", "");
            cadena = cadena.Replace("-<br />", " ");
            cadena = cadena.Replace(" <br />", " ");

            cadena = cadena.Replace("<p class=\"capitular1\">", "");
            cadena = cadena.Replace("<span class=\"indicacion\">", "");
            cadena = cadena.Replace("<p class=\"www\">", "");
            cadena = cadena.Replace("</div>", "");
            cadena = cadena.Replace("</html>", "");
            cadena = cadena.Replace("</body>", "");
            cadena = cadena.Replace("<span class=" + "\"cursivo-normal" + "\">", " ");
            cadena = cadena.Replace("<span class=" + "\"symbolprop-bt" + "\">", "");
            cadena = cadena.Replace("</span>", "");
            cadena = cadena.Replace("&nbsp", "");

            cadena = cadena.Replace("</p>", " ");
            cadena = cadena.Replace("<p class=" + "\"texto-de-leyendas-de-prot-" + "\">", "");
            cadena = cadena.Replace("<p class=" + "\"pie1" + "\">", "");
            cadena = cadena.Replace("<p class=" + "\"pie" + "\">", "");
            cadena = cadena.Replace("<p class=" + "\"bandos" + "\">", "");
            cadena = cadena.Replace("<p class=" + "\"biblio-refe" + "\">", "");
            cadena = cadena.Replace("<span class=" + "\"symbol" + "\">", "");
            cadena = cadena.Replace("<p class=" + "\"pie-de-columna" + "\">", "");
            cadena = cadena.Replace("<p class=" + "\"sin-estilo-normal" + "\">", "");
            cadena = cadena.Replace("<p class=" + "\"bandos1" + "\">", "");


            cadena = cadena.Replace("<p class=" + "\"tabla" + "\">", "\n");
            cadena = cadena.Replace("<p class=" + "\"pie-de-tabla" + "\">", "\n");
            cadena = cadena.Replace("<p class=" + "\"pie-de-tabla-2" + "\">", "\n");

            cadena = cadena.Replace("<p class=" + "\"normal" + "\">", "");
            cadena = cadena.Replace("<span class=" + "\"rubros-az", "");

            cadena = cadena.Replace("<img src=" + "\"", "<Imagen href=" + "\"file://");
            cadena = cadena.Replace("<span class=\"superindice-normal\">", "");
            cadena = cadena.Replace("<span class=\"subindice-normal\">", "");
            cadena = cadena.Replace("<span class=\"superindice-titulo\">", "");
            cadena = cadena.Replace("<span class=\"letra-cyan\">", "");
            cadena = cadena.Replace("&ntilde;", "ñ");
            cadena = cadena.Replace("<p class=\"logo-pie\">", "");
            cadena = cadena.Replace("<span class=\"subrayado\">", "");
            cadena = cadena.Replace("<span class=\"alfabeto-griego\">", "");
            cadena = cadena.Replace("<span class=\"mg-titulo\">", "");
            cadena = cadena.Replace("<span class=\"subtirulito\">", "");
            cadena = cadena.Replace("<span class=\"subtitulo-3\">", "");
            cadena = cadena.Replace("<span class=\"ref-de-comentario\">", "");
            cadena = cadena.Replace("<span class=\"texto-en-negrita\">", "");
            cadena = cadena.Replace("<span class=\"apple-style-span\">", "");
            cadena = cadena.Replace("<span class=\"hiperv-nculo\">", "");
            cadena = cadena.Replace("<span class=\"numero-titulo\">", "");
            cadena = cadena.Replace("<span class=\"hyperlink\">", "");
            cadena = cadena.Replace("<span class=\"prddispsmbk1\">", "");
            cadena = cadena.Replace("<span class=\"subtitulos1\">", "");
            cadena = cadena.Replace("<p class=\"tabla-titulo\">", "");
            cadena = cadena.Replace("<p class=\"imagen\">", "");
            cadena = cadena.Replace("<p class=\"logo\">", "");
            cadena = cadena.Replace("<p class=\"tabla-celda\">", "");
            cadena = cadena.Replace("<p class=\"zletra\">", "");
            cadena = cadena.Replace("&Ntilde;", "Ñ");

            cadena = cadena.Replace("<span class=\"superindice\">", " ");
            cadena = cadena.Replace("<span class=\"negro-normal\">", " ");

            cadena = cadena.Replace("<span class=\"puntos-formulaci-n\">", " ");
            cadena = cadena.Replace("<span class=\"subindice\">", " ");
            cadena = cadena.Replace("<span class=\"tabla-negro\">", "");
            cadena = cadena.Replace("<span class=\"aa\">", " ");

            cadena = cadena.Replace("&uml;", "ü");
            cadena = cadena.Replace("&Uml;", "Ü");
            cadena = cadena.Replace("&uuml;", "ü");
            cadena = cadena.Replace("&Uuml;", "Ü");
            cadena = cadena.Replace("<strong>", "");
            cadena = cadena.Replace("</strong>", "");
            //cadena = cadena.Replace("<span class="letra-cyan-copia">");
            cadena = cadena.Replace("<p class=\"logo-pie\" align=\"center\">", "");
            cadena = cadena.Replace("<span class=\"logo-pie\">", "");
            cadena = cadena.Replace("<p class=\"logo-pie\" align=\"center\">;", "");
            cadena = cadena.Replace("<span class=\"logo\">", "");
            cadena = cadena.Replace("<p class=\"tabla-encabezado-columna-\">", "");
            cadena = cadena.Replace("<span class=\"strong\">", "");
            cadena = cadena.Replace("<p class=\"tabla-pie\">", "");
            cadena = cadena.Replace("<span class=\"s93\">", "");
            cadena = cadena.Replace("<span class=\"s161\">", "");
            cadena = cadena.Replace("<p class=\"tabla-2\">", "");
            cadena = cadena.Replace("<span class=\"sub-indice-titulo\">", " ");
            cadena = cadena.Replace("<span class=\"superindice-titulo-nuevo\">", " ");
            cadena = cadena.Replace("<span class=\"page-number\">", "");
            cadena = cadena.Replace("<span class=\"ref-de-nota-al-final\">", " ");
            cadena = cadena.Replace("<span class=\"alfabeto-griego-2\">", "");
            cadena = cadena.Replace("<span class=\"strong-car-car\">", "");
            cadena = cadena.Replace("<span class=\"ref-de-nota-al-final\">", "");
            cadena = cadena.Replace("<span class=\"emphasis\">", "");
            cadena = cadena.Replace("<span class=\"a0\">", "");
            cadena = cadena.Replace("<span class=\"short-text\">", "");
            cadena = cadena.Replace("<p class=\"logo\" align=\"center\">", "");
            cadena = cadena.Replace("<p class=\"logo\" align=\"right\">", "");
            cadena = cadena.Replace("<span class=\"tit-nuevo-distribuidores\">", "");
            cadena = cadena.Replace("<span class=\"forma-farma-distribuidores\">", "");
            cadena = cadena.Replace("<span class=\"azul-titulo-distribuidores\">", "");
            cadena = cadena.Replace("<span class=\"normal-vitamina\">", "");
            cadena = cadena.Replace("<p class=\"fin-de-producto\">", "");
            cadena = cadena.Replace("<p class=\"tabla-titulos\">", "");
            cadena = cadena.Replace("<p class=\"bandos-2\">", "");
            cadena = cadena.Replace("<p class=\"bandos-3\">", "");
            cadena = cadena.Replace("<span class=\"titulo-distribuidores\">", "");
            cadena = cadena.Replace("<span class=\"endnote-reference\">", "");
            cadena = cadena.Replace("<span class=\"bandos\">", "");
            cadena = cadena.Replace("<p class=\"bandos2\">", "");

            cadena = cadena.Replace("<span class=\"titulo-super-ndice\">", "");
            cadena = cadena.Replace("<span class=\"normal-normal-super-ndice\">", "");
            cadena = cadena.Replace("<span class=\"normal-normal-negrita\">", "");
            cadena = cadena.Replace("<p class=\"normal-normal\">", "");
            cadena = cadena.Replace("<p class=\"bandos-bandos\">", "");
            cadena = cadena.Replace("<p class=\"bandos-bandos-sin-vi-eta\">", "");
            cadena = cadena.Replace("<span class=\"bandos-bandos-cursiva\">", "");
            cadena = cadena.Replace("<span class=\"bandos-bandos-normal-superindice\">", "");
            cadena = cadena.Replace("<span class=\"normal-normal-negrita-cursiva\">", "");
            cadena = cadena.Replace("<span class=\"normal-normal-cursiva\">", "");
            cadena = cadena.Replace("<span class=\"titulo-numeros\">", "");
            cadena = cadena.Replace("<p class=\"bandos-bandos-1\">", "");
            cadena = cadena.Replace("<p class=\"bandos-bandos-2\">", "");
            cadena = cadena.Replace("<p class=\"bandos-bandos-para-numeros\">", "");
            cadena = cadena.Replace("<span class=\"bandos-bandos-negrita\">", "");
            cadena = cadena.Replace("<span class=\"bandos-bandos-normal-subindice\">", "");
            cadena = cadena.Replace("<p class=\"bandos-bandos-1-sin-vi-eta\">", "");
            cadena = cadena.Replace("<span class=\"tablas-tabla-normal-superindice\">", "");
            cadena = cadena.Replace("<span class=\"normal-normal-negra-sub-ndice\">", "");
            cadena = cadena.Replace("<span class=\"normal-symbol\">", "");
            cadena = cadena.Replace("<p class=\"tablas-titulo-tabla\">", "");
            cadena = cadena.Replace("<span class=\"tablas-tabla-bold\">", "");
            cadena = cadena.Replace("<span class=\"normal-normal-color-sin-bold-\">", "");
            cadena = cadena.Replace("<span class=\"normal-normal-color\">", "");
            cadena = cadena.Replace("<span class=\"pie-1-super-ndice\">", "");
            cadena = cadena.Replace("<span class=\"titulo-producto-nuevo-numeros\">", "");
            cadena = cadena.Replace("<span class=\"normal-normal-sub-ndice\">", "");
            cadena = cadena.Replace("<div class=\"image\">", "");
            cadena = cadena.Replace("<p class=\"normal-normal-graficos-centrado\">", "");
            cadena = cadena.Replace("<span class=\"tablas-tabla-bold-superindice\">", " ");
            cadena = cadena.Replace("<span class=\"tablas-titulo-tabla-normal\">", "");
            cadena = cadena.Replace("<span class=\"tablas-titulo-tabla-normal-cursiva\">", "");
            cadena = cadena.Replace("<span class=\"tablas-titulo-tabla-bold-superindice\">", "");
            cadena = cadena.Replace("<span class=\"tablas-titulo-tabla-normal-superindice\">", "");
            cadena = cadena.Replace("<span class=\"normal-normal-negra-super-ndice\">", " ");
            cadena = cadena.Replace("<span class=\"normal-normal-color-sin-bold-superindice\">", "");
            cadena = cadena.Replace("<span class=\"tablas-tabla-bold-subindice\">", " ");
            cadena = cadena.Replace("<span class=\"tablas-tabla-cursiva\">", "");
            cadena = cadena.Replace("<span class=\"normal-titulo-super-ndice\">", "");
            cadena = cadena.Replace("<span class=\"normal-normal-color-sin-bold-superindice\">", " ");
            cadena = cadena.Replace("<p class=\"normal-normal-tabla-izq\">", "");
            cadena = cadena.Replace("<span class=\"pie-1-negrita-cursiva\">", "");
            cadena = cadena.Replace("<span class=\"tablas-tabla-bold-cursiva\">", "");
            cadena = cadena.Replace("<p class=\"tablas-tabla-izquierda-con-tabulado\">", "");
            cadena = cadena.Replace("<span class=\"tablas-tabla-symbol\">", "");
            cadena = cadena.Replace("<span class=\"tablas-tabla-normal-supbindice\">", " ");
            cadena = cadena.Replace("<span class=\"bandos-bandos-symbol\">", " ");
            cadena = cadena.Replace("<span class=\"base-de-producto-sub-indice\">", "");
            cadena = cadena.Replace("<span class=\"ingrediente-sub-ndice\">", "");
            cadena = cadena.Replace("<p class=\"tablas-pie-tabla\">", "");
            cadena = cadena.Replace("<span class=\"base-de-producto-subindice\">", "");
            cadena = cadena.Replace("<span class=\"tablas-tabla-symbolpro\">", "");
            cadena = cadena.Replace("<span class=\"tablas-titulo-tabla-normal-subindice\">", "");
            cadena = cadena.Replace("<span class=\"titulo-sub-ndice\">", "");
            cadena = cadena.Replace("<span class=\"titulo-producto-nuevo-super-ndice\">", "");
            cadena = cadena.Replace("<span class=\"normal-normal-underline\">", "");
            cadena = cadena.Replace("<span class=\"normal-symbol-winding\">", "");
            cadena = cadena.Replace("<p class=\"tablas-tabla-centrado\">", "");
            cadena = cadena.Replace("<p class=\"tablas-tabla-izquierda\">", "");
            cadena = cadena.Replace("<span class=\"tablas-tabla-windings-3\">", "");
            cadena = cadena.Replace("<span class=\"tablas-symbol\">", "");
            cadena = cadena.Replace("<span class=\"tablas-titulo-tabla-bold-cursiva\">", "");
            cadena = cadena.Replace("<span class=\"bandos-bandos-symbol-subindice\">", " ");
            cadena = cadena.Replace("<span class=\"normal-symbol-sub-ndice\">", " ");
            cadena = cadena.Replace("<span class=\"normal-symbol-pro-\">", "");
            cadena = cadena.Replace("<p class=\"normal-normal-tabla-cen\">", "");
            cadena = cadena.Replace("<span class=\"tablas-titulo-tabla-bold-subindice\">", " ");
            cadena = cadena.Replace("<span class=\"tablas-tabla-windings-3-subindice\">", " ");
            cadena = cadena.Replace("<span class=\"normal-symbol-winding-subindice\">", " ");
            cadena = cadena.Replace("<span class=\"tablas-tabla-symbol-subindice\">", "");
            cadena = cadena.Replace("<br>", " ");
            cadena = cadena.Replace("<p class=\"cuadro\">", "");
            cadena = cadena.Replace("<p class=\"antetitulo\">", "");
            cadena = cadena.Replace("<p class=\"bandos-copy\">", "");
            cadena = cadena.Replace("<span class=\"superindice-tit\">", "");
            cadena = cadena.Replace("<span class=\"negro-superindice\">", "");
            cadena = cadena.Replace("<span class=\"negro-cursivo\">", "");
            cadena = cadena.Replace("<span class=\"negro-subindice\">", "");
            cadena = cadena.Replace("<span class=\"symbolprop-bt-super-ndice\">", "");
            cadena = cadena.Replace("<span class=\"symbolprop-bt-sub-ndice\">", "");
            cadena = cadena.Replace("<span class=\"symbol-subindice\">", "");
            cadena = cadena.Replace("<span class=\"azul-titulo-reg\">", "");
            cadena = cadena.Replace("<p class=\"pie-2\">", "");
            cadena = cadena.Replace("<span class=\"normal-reg\">", "");
            cadena = cadena.Replace("<span class=\"tit-nuevo-reg-\">", "");
            cadena = cadena.Replace("<span class=\"tit-nuevo-distribuidores-copia\">", "");
            cadena = cadena.Replace("<p class=\"pie-3\">", "");
            cadena = cadena.Replace("<p class=\"pie-4-logo\">", "");
            cadena = cadena.Replace("<p class=\"pie-laboratorio\">", "");
            cadena = cadena.Replace("<p class=\"antetitulo-2\">", "");
            cadena = cadena.Replace("<p class=\"pie-4-logo\">", "");
            cadena = cadena.Replace("<p class=\"antetitulo-sin-separacion\">", "");
            cadena = cadena.Replace("<p class=\"tabla-pie-2\">", "");
            cadena = cadena.Replace("<p class=\"bandos-1\">", "");
            cadena = cadena.Replace("<span class=\"bandos-negrita-cursiva\">", "");
            cadena = cadena.Replace("<span class=\"tablas-tabla-symbolpro-subindice\">", "");
            cadena = cadena.Replace("<span class=\"titulo-producto-nuevo-numeros-2\">", "");

            //DEAQ
            cadena = cadena.Replace("<span class=\"boldcursiva\">", " ");
            cadena = cadena.Replace("<span class=\"cursiva\">", " ");
            cadena = cadena.Replace("<p class=\"cuadro-arriba\">", " ");
            cadena = cadena.Replace("<p class=\"cuadro-fin\">", " ");
            cadena = cadena.Replace("<p class=\"final-pagina\">", " ");
            cadena = cadena.Replace("<p class=\"final-pagina1\">", " ");
            cadena = cadena.Replace("<p class=\"texto-centro1\">", " ");
            cadena = cadena.Replace("<span class=\"min-titulo\">", " ");
            cadena = cadena.Replace("<span class=\"superindicetit\">", " ");
            cadena = cadena.Replace("<span class=\"boldpantalla\">", " ");
            cadena = cadena.Replace("<p class=\"final-p-gina-1\">", " ");
            cadena = cadena.Replace("<p class=\"texto-centro\">", " ");
            cadena = cadena.Replace("<p class=\"final-p-gina-1\">", " ");
            cadena = cadena.Replace("<p class=\"cuadro1\">", "");
            cadena = cadena.Replace("<p class=\"composi\">", " ");
            cadena = cadena.Replace("<p class=\"forma-farmac-\">", "");
            cadena = cadena.Replace("<p class=\"reg-sarh\">", "");
            cadena = cadena.Replace("<p class=\"cuadro2\">", "");
            cadena = cadena.Replace("<p class=\"t-tulosub\">", "");
            cadena = cadena.Replace("<p class=\"composici-n\">", "");
            cadena = cadena.Replace("<p class=\"cuadro3\">", "");
            cadena = cadena.Replace("<span class=\"bold\">", "");
            cadena = cadena.Replace("<p class=\"pie-1\">", "");
            cadena = cadena.Replace("<p class=\"t-tulo-prod-nuevo\">", "");

            cadena = cadena.Replace("<p class=\"tabla-prod\">", " ");
            cadena = cadena.Replace("<span class=\"negro\">", " ");
            cadena = cadena.Replace("<span class=\"symbolprop-bt-superindice\">", " ");
            cadena = cadena.Replace("<td class=\"normal-tabla-prod\">", "");
            cadena = cadena.Replace("<p class=\"productos\">", "");
            cadena = cadena.Replace("class=\"normal-tabla-prod\"", " ");
            cadena = cadena.Replace("<p class=\"x-ning-n-estilo-de-p-rrafo-\">", " ");
            cadena = cadena.Replace("<p class=\"style-group-1-bandos\">", "");
            cadena = cadena.Replace("<span class=\"boldcursiva-texto\">", " ");
            cadena = cadena.Replace("<span class=\"symbolos\">", " ");
            cadena = cadena.Replace("<p class=\"subt-tulo\">", " ");
            cadena = cadena.Replace("<span class=\"superindice-bold\">", " ");
            cadena = cadena.Replace("<span class=\"subindice-bold\">", " ");
            //OTC

            cadena = cadena.Replace("<span class=\"x1a-bold-titulos\">", "");
            cadena = cadena.Replace("<span class=\"x1c-bold-cursiva-tit\">", "");
            cadena = cadena.Replace("<p class=\"x6-f-rmula\">", "");
            cadena = cadena.Replace("<p class=\"x5a-texto-normal\">", "");
            cadena = cadena.Replace("<p class=\"x5b-texto-sin-espacios\">", "");
            cadena = cadena.Replace("<span class=\"x3a-superindice\">", " ");
            cadena = cadena.Replace("<span class=\"x3b-superindice-tit\">", " ");
            cadena = cadena.Replace("<span class=\"x1b-bold-texto\">", "");
            cadena = cadena.Replace("<p class=\"x10-tabla-espacio\">", "");
            cadena = cadena.Replace("<p class=\"x7-bandos\">", " ");
            cadena = cadena.Replace("<span class=\"x5-puntitos\">", " ");
            cadena = cadena.Replace("<p class=\"x8-leyendas-protecci-n\">", " ");
            cadena = cadena.Replace("<span class=\"x1b-atributo\">", " ");
            cadena = cadena.Replace("<span class=\"x2a-cursivo-normal\">", " ");
            cadena = cadena.Replace("<span class=\"x4-subindice\">", "");
            cadena = cadena.Replace("<span class=\"x2b-cursivas\">", "");
            cadena = cadena.Replace("<span class=\"x4b-subindice-tit\">", "");
            cadena = cadena.Replace("<p class=\"espacio\">", " ");
            cadena = cadena.Replace("<p class=\"embarazo\">", "");

            cadena = cadena.Replace("<p class=\"basic-paragraph\">", "-");
            cadena = cadena.Replace("<span class=\"subindice-normal-copia\">", "");
            cadena = cadena.Replace("<span class=\"mag\">", "");
            cadena = cadena.Replace("<span class=\"magenta\">", "");

            cadena = cadena.Replace("<p class=\"leyendas\">", "");

            cadena = cadena.Replace("<span class=\"clave\">", "");
            cadena = cadena.Replace("<span class=\"descripci-n\">", "");
            cadena = cadena.Replace("<span class=\"producto\">", "");
            cadena = cadena.Replace("<p class=\"tabla-normal\">", "");
            cadena = cadena.Replace("<span class=\"simbolos\">", "");
            cadena = cadena.Replace("<p class=\"bullets\">", "");
            cadena = cadena.Replace("<p class=\"cabeza-tabla\">", "");
            cadena = cadena.Replace("<p class=\"fuente\">", "");
            cadena = cadena.Replace("<span class=\"bold-cursiva\">", "");

            cadena = cadena.Replace("<span class=\"superindice-normal-2\">", " ");
            cadena = cadena.Replace("<span class=\"mayusculas\">", " ");


            cadena = cadena.Replace("<p class=\"x4-body-text\">", "");
            cadena = cadena.Replace("<span class=\"tabla-titulo\">", "");
            cadena = cadena.Replace("<p class=\"x7-mota\">", "");

            cadena = cadena.Replace("<p class=\"f-rmula\">", "");
            cadena = cadena.Replace("<span class=\"bold-entrada\">", "");
            cadena = cadena.Replace("<span class=\"base-boldcursivo\">", "");
            cadena = cadena.Replace("<p class=\"uso-en1\">", "");
            cadena = cadena.Replace("<span class=\"forma-cursiva\">", "");
            cadena = cadena.Replace("<p class=\"pie-0\">", "");
            cadena = cadena.Replace("<p class=\"uso-en\">", "");
            cadena = cadena.Replace("<p class=\"uso\">", "");
            cadena = cadena.Replace("<td class=\"tabla\">", "");
            cadena = cadena.Replace("<td class=\"tabla\"  colspan=\"2\">", "");
            cadena = cadena.Replace("<td class=\"tabla\">", "");
            cadena = cadena.Replace("<td>", "");
            cadena = cadena.Replace("</td>", "");
            cadena = cadena.Replace("<tr>", "");
            cadena = cadena.Replace("</tr>", "");
            cadena = cadena.Replace("</tbody>", "");
            cadena = cadena.Replace("</table>", "");

            cadena = cadena.Replace("<span class=\"symbolprop-subindice\">", "");
            cadena = cadena.Replace("<span class=\"bayer-table-row-headings-zchn\">", "");
            cadena = cadena.Replace("<span class=\"bms-table-note\">", "");
            cadena = cadena.Replace("<span class=\"x-nfasis\">", "");
            cadena = cadena.Replace("<span class=\"paginacion\">", "");
            cadena = cadena.Replace("<span class=\"bms-table-note\">", "");
            cadena = cadena.Replace("<span class=\"azul-superindice\">", "");
            cadena = cadena.Replace("<p class=\"body-text-2\">", "");


            ///////////
            cadena = cadena.Replace("<span class=\"subt-tulo\">", "");
            cadena = cadena.Replace("<p class=\"t-tulo-copia-2-2\">", "");
            cadena = cadena.Replace("<p class=\"ingrediente\">", "");
            cadena = cadena.Replace("<p class=\"base-de-prop-\">", "");
            cadena = cadena.Replace("<td  colspan=\"2\">", "");
            cadena = cadena.Replace("<p class=\"t-tulo-copia-3\">", "");
            cadena = cadena.Replace("<p class=\"t-tulo-copia\">", "");
            cadena = cadena.Replace("<p class=\"t-tulo-copy\">", "");
            cadena = cadena.Replace("<p class=\"t-tulo-copia-copia\">", "");
            cadena = cadena.Replace("<br", "");
            cadena = cadena.Replace("<span class=\"normal\">", "");
            cadena = cadena.Replace("<span class=\"bandos-bandos-con-bullet-cursiva\">", "");
            cadena = cadena.Replace("<span class=\"normal-normal-bold\">", "");
            cadena = cadena.Replace("<span class=\"normal-normal-super-indice\">", "");
            cadena = cadena.Replace("<p class=\"bandos-bandos-con-bullet-2\">", "");
            cadena = cadena.Replace("<p class=\"bandos-bandos-sin-bullet-2\">", "");
            cadena = cadena.Replace("<p class=\"bandos-bandos-con-bullet-1\">", "");
            cadena = cadena.Replace("<span class=\"bandos-bandos-con-bullet-bold\">", "");
            cadena = cadena.Replace("<span class=\"bandos-bandos-super-indices\">", "");
            cadena = cadena.Replace("<p class=\"normal-ingrediente-punteado\">", "");
            cadena = cadena.Replace("<p class=\"bandos-bandos-con-bullet-1-b\">", "");
            cadena = cadena.Replace("<span class=\"normal-normal-sub-indice\">", "");
            cadena = cadena.Replace("<p class=\"normal-normal-grafico\">", "");
            cadena = cadena.Replace("<p class=\"tablas-tabla-titulo\">", "");
            cadena = cadena.Replace("<span class=\"bandos-bandos-bold\">", "");
            cadena = cadena.Replace("<p class=\"normal-info-de-laboratorio\">", "");
            cadena = cadena.Replace("<span class=\"tablas-tabla-super-indice\">", "");
            cadena = cadena.Replace("<span class=\"tablas-tabla-super-indice-n\">", "");
            cadena = cadena.Replace("<p class=\"tablas-bandos-bandos\">", "");
            cadena = cadena.Replace("<span class=\"normal-normal-color-bold-superindice\">", "");
            cadena = cadena.Replace("<p class=\"tablas-bandos-bandos-sin-vi-eta\">", "");
            cadena = cadena.Replace("<p class=\"tablas-bandos-bandos-para-numeros\">", "");
            cadena = cadena.Replace("<p class=\"tablas-bandos-bandos-1\">", "");
            cadena = cadena.Replace("<p class=\"bandos-bandos-2-sin-vi-eta\">", "");
            cadena = cadena.Replace("<p class=\"tablas-bandos-bandos-1-sin-vi-eta\">", "");
            cadena = cadena.Replace("<p class=\"tablas-bandos-bandos-2\">", "");
            cadena = cadena.Replace("<p class=\"bandos-bandos-con-buellet-3\">", "");
            cadena = cadena.Replace("<p class=\"t-tulo\">", "");
            cadena = cadena.Replace("<div id=\"Div1\">", "");
            cadena = cadena.Replace("<p class=\"bandos-bandos-3\">", "");
            cadena = cadena.Replace("<span class=\"normal-normal-color-superindice\">", "");
            cadena = cadena.Replace("<p class=\"bandos-bandos-2-sin-vi-etas\">", "");
            cadena = cadena.Replace("<span>", "");
            cadena = cadena.Replace("<span >", "");
            cadena = cadena.Replace("<span class=\"tablas-tabla-symbol-super-ndice\">", "");
            cadena = cadena.Replace("<p class=\"presentacion\">", "");
            cadena = cadena.Replace("<p class=\"hecho\">", "");
            cadena = cadena.Replace("<p class=\"registro\">", "");
            cadena = cadena.Replace("<p class=\"direcciion\">", "");
            cadena = cadena.Replace("<p class=\"clave\">", "");
            cadena = cadena.Replace("<p class=\"marca-reg\">", "");
            cadena = cadena.Replace("<p class=\"pie-sub\">", "");

            cadena = cadena.Replace("<span class=\"SuperIndice_Tit _idGenCharOverride-1\">", "");
            cadena = cadena.Replace("<span class=\"Negro-Normal\">", "");
            cadena = cadena.Replace("<p class=\"Normal\">", "");
            cadena = cadena.Replace("<p class=\"Normal ParaOverride-1\">", "");
            cadena = cadena.Replace("<span class=\"SuperIndice _idGenCharOverride-1\">", "");
            cadena = cadena.Replace("<span class=\"Cursivo-Normal\">", "");
            cadena = cadena.Replace("<p class=\"Texto-de-leyendas-de-prot-\">", "");
            cadena = cadena.Replace("<p class=\"Pie\">", "");
            cadena = cadena.Replace("<p class=\"Pie1\">", "");
            cadena = cadena.Replace("<p class=\"Bandos\">", "");
            cadena = cadena.Replace("<span class=\"SuperIndice CharOverride-1\">", "");
            cadena = cadena.Replace("<p class=\"Pie-de-columna\">", "");
            cadena = cadena.Replace("<p class=\"Pie-de-columna ParaOverride-6\">", "");
            cadena = cadena.Replace("<p class=\"Normal ParaOverride-10\">", "");
            cadena = cadena.Replace("<p class=\"Pie1 ParaOverride-1\">", "");
            cadena = cadena.Replace("<span class=\"SubIndice _idGenCharOverride-1\">", "");
            cadena = cadena.Replace("<p class=\"Normal ParaOverride-2\">", "");
            cadena = cadena.Replace("<span class=\"SymbolProp-BT\">", "");
            cadena = cadena.Replace("<p class=\"Pie-de-tabla ParaOverride-4\">", "");
            cadena = cadena.Replace("<p class=\"Pie-de-tabla ParaOverride-5\">", "");
            cadena = cadena.Replace("<p class=\"Normal ParaOverride-7\">", "");
            cadena = cadena.Replace("<p class=\"Pie-de-tabla ParaOverride-6\">", "");

            cadena = cadena.Replace("<span class=\"padecimiento\">", "");
            cadena = cadena.Replace("<p class=\"texto\">", "");
            cadena = cadena.Replace("<p class=\"dosis\">","");
            cadena = cadena.Replace("<span class=\"indice\">","");

            //DEF 60
            cadena = cadena.Replace("<span class=\"heading-4-char\">","");
            cadena = cadena.Replace("<span class=\"symbolprop-bt-subindice\">","");
            cadena = cadena.Replace("<span class=\"symbolprop-subindic\">","");
            cadena = cadena.Replace("<span class=\"subindice-tit\">", "");
            cadena = cadena.Replace("<span class=\"hps\">", "");
            cadena = cadena.Replace("<span class=\"long-text1\">", "");
            cadena = cadena.Replace("<span class=\"apple-converted-space\">", "");
            cadena = cadena.Replace("<span class=\"symbolprop-bt-2\">", "");

            cadena = cadena.Replace("<span class=\"SuperIndice\">", "");
            cadena = cadena.Replace("<span class=\"Rubros-azules\">", "");
            cadena = cadena.Replace("<p class=\"Normal\">", "");
            cadena = cadena.Replace("<span class=\"Negro-Normal\">", "");
            cadena = cadena.Replace("<p class=\"Texto-de-leyendas-de-prot-\">", "");
            
            cadena = cadena.Replace("<span class=\"SuperIndice_Tit\">", "");
            cadena = cadena.Replace("<span class=\"Negro-Cursivo\">", "");
            cadena = cadena.Replace("<span class=\"SubIndice\">", "");
            cadena = cadena.Replace("<p class=\"Pie-de-tabla\">", "");
            cadena = cadena.Replace("<p class=\"Medio-de-D--prod--nuevo\">", "");
            cadena = cadena.Replace("INFORMACIÓN NUEVA", "");
            cadena = cadena.Replace("<span class=\"Negro-superIndice\">", "");
            cadena = cadena.Replace("<span class=\"Symbol\">", "");
            
            cadena = cadena.Replace("<span class=\"Paginacion\">", "");
            cadena = cadena.Replace("<span lang=\"ar-SA\">", "");
           cadena = cadena.Replace("<span class=\"cursivo-normal-2\">", "");
            cadena = cadena.Replace("<span class=\"atn\">", "");
            cadena = cadena.Replace("<span class=\"Puntos-formulación\">", "");
            cadena = cadena.Replace("<p class=\"Tabla_prod\">", "");
            cadena = cadena.Replace("<span class=\"Negro-Normal\" lang=\"en-GB\">", "");
            cadena = cadena.Replace("<span lang=\"en-GB\">", "");
           cadena = cadena.Replace("<p class=\"Pie-de-tabla-2\">", "");
            cadena = cadena.Replace("<span class=\"Negro-subIndice\">", "");
            cadena = cadena.Replace("<span class=\"SymbolProp-BT_super-ndice\">", "");
            cadena = cadena.Replace("<span class=\"SymbolProp-BT_sub-ndice\">", "");
            cadena = cadena.Replace("<p class=\"Bandos1\">", "");
            cadena = cadena.Replace("<p class=\"BIBLIO-REFE\">", "");
            cadena = cadena.Replace("<span class=\"SymbolProp-BT-super-ndice\">", "");
            cadena = cadena.Replace("<span lang=\"fr-FR\">", "");
            cadena = cadena.Replace("<p class=\"PRESENTACION\">", "");

            cadena = cadena.Replace("<p class=\"Direcciion\">", "");
            cadena = cadena.Replace("<p class=\"Registro\">", "");
            cadena = cadena.Replace("<p class=\"Hecho\">", "");
            cadena = cadena.Replace("<p class=\"Marca-reg\">", "");

            cadena = cadena.Replace("<span class=\"Bold\">", "");
            cadena = cadena.Replace("<span class=\"Cursiva\">", "");
           cadena = cadena.Replace("<span class=\"SIMBOLOS\">", "");
            cadena = cadena.Replace("<span class=\"Symbol_subIndice\">", "");
            cadena = cadena.Replace("<p class=\"Pie-sub\">", "");
            cadena = cadena.Replace("<span lang=\"pt-PT\">", "");
            cadena = cadena.Replace("<span class=\"Tabla-negro\">", "");
            cadena = cadena.Replace("<span class=\"SuperIndice_Tit\" lang=\"ar-SA\">", "");
            cadena = cadena.Replace("<span class=\"SuperIndice\" lang=\"ar-SA\">", "");
            cadena = cadena.Replace("<span class=\"Cursivas-Normal\">", "");
            cadena = cadena.Replace("<span class=\"SubIndice_Tit\">", "");
            cadena = cadena.Replace("<span lang=\"pt-BR\">", "");
            cadena = cadena.Replace("<span class=\"Endnote-Reference\">", "");
            cadena = cadena.Replace(" lang=\"pt-BR\"", "");
            cadena = cadena.Replace("<span class=\"_nfasis\">", "");
            cadena = cadena.Replace("<p class=\"Fin-de-producto\">", "");
            cadena = cadena.Replace("<span class=\"SymbolProp-BT-subIndice\">", "");
            cadena = cadena.Replace("<span class=\"BMS-Table-Note\">", "");
            cadena = cadena.Replace("<span class=\"Negro-Normal\">", ""); 
            cadena = cadena.Replace("<span>", "");
            cadena = cadena.Replace(" lang=\"ar-SA\"", "");
            cadena = cadena.Replace("<span class=\"SymbolProp-BT_superIndice\">", "");
            cadena = cadena.Replace("<span class=\"SubIndice-tit\">", "");
            cadena = cadena.Replace(" lang=\"de-DE\"", "");
            cadena = cadena.Replace("<a href=\"\">", "");
            cadena = cadena.Replace("<span class=\"lucida-sans-unicode_fuente\">", ""); 
            cadena = cadena.Replace("<span class=\"Tabla\">", "");
            cadena = cadena.Replace("<span lang=\"it-IT\">", "");
            cadena = cadena.Replace("<span class=\"short_text\">", "");
            cadena = cadena.Replace("<span class=\"Puntos-formulacion\">", "");
            cadena = cadena.Replace("<a href=\"\">", "");
            cadena = cadena.Replace("&#9;", " ");
            cadena = cadena.Replace("<span class=\"Logo\">", "");
            cadena = cadena.Replace("&#160;", " ");
            cadena = cadena.Replace("&#173;", "-");
            cadena = cadena.Replace("&#174;", "®");
            cadena = cadena.Replace("&apos;", "'");
            cadena = cadena.Replace("<span class=\"SymbolProp-BT_subIndice\">", "");

            cadena = cadena.Replace("&#8805;", "≥");
            cadena = cadena.Replace("&#8804;", "≤");
            cadena = cadena.Replace("&#8734;", "∞");



            return cadena;
        }
        //Tabla
        public static void getRows(String S)
        {

            int i = 0;
            int renglonIni, renglonFin;
            String renglon = "";
            int columnas = 0;

            while (S != null)
            {
                renglonIni = S.IndexOf("<tr");

                if (renglonIni > 0)
                {
                    renglonFin = S.IndexOf("</tr>", renglonIni);
                    renglon = S.Substring(renglonIni, renglonFin - renglonIni + 5);
                    columnas = getColumns(renglon);
                    rows[i] = columnas;
                    S = S.Substring(renglonFin + 5, S.Length - (renglonFin + 5));
                    i++;
                }
                else S = null;
            }

            numrenglones = i;
        }
        public static int getColumns(String renglon)
        {

            int i = 0;
            int columnaIni, columnaFin;
            String columna = "", contenido = "";
            String rowS = "rowspan";
            String colS = "colspan";
            String colFin = "</td>";
            int rSini, rSFin, cSIni, cSFin, rSpan = 0, cSpan = 0;
            String valueRowS = "1", valueColS = "1";

            while (renglon != null)
            {
                columnaIni = renglon.IndexOf("<td");

                if (columnaIni > 0)
                {
                    columnaFin = renglon.IndexOf(colFin, columnaIni);
                    columna = renglon.Substring(columnaIni, (columnaFin - columnaIni) + colFin.Length);
                    valueRowS = "1";
                    valueColS = "1";
                    rSpan = columna.IndexOf("rowspan");
                    cSpan = columna.IndexOf("colspan");


                    if (rSpan > 0)
                    {
                        rSini = columna.IndexOf("\"", rSpan);

                        if (rSini > 0)
                        {
                            rSFin = columna.IndexOf("\"", rSini + 1);
                            valueRowS = columna.Substring(rSini + 1, rSFin - (rSini + 1));
                        }
                    }

                    if (cSpan > 0)
                    {
                        cSIni = columna.IndexOf("\"", cSpan);

                        if (cSIni > 0)
                        {
                            cSFin = columna.IndexOf("\"", cSIni + 1);
                            valueColS = columna.Substring(cSIni + 1, cSFin - (cSIni + 1));
                        }
                    }

                    renglon = renglon.Substring(columnaFin + colFin.Length, renglon.Length - (columnaFin + colFin.Length));
                    contenido = getContentColumn(columna, j, valueRowS, valueColS);

                    i++;
                    j++;

                }
                else renglon = null;
                numcolumnas = i;

            }
            return i;
        }
        public static String getContentColumn(String columna, int cont, String rowSpan, String colSpan)
        {
            int i = 0, f, faux, fini;
            String contenido = "";
            String marcadorFin = "</p>";
            //columna = columna.Replace("<p class=" + "\"tabla" + "\">", "");
            //columna = columna.Replace("</p>", " ");
            String contenido1 = "";

            while (columna != null)
            {
                f = columna.IndexOf("<p");

                if (f > 0)
                {
                    fini = columna.IndexOf(">", f);
                    faux = columna.IndexOf(marcadorFin, f);
                    contenido = columna.Substring(fini + 1, faux - (fini + 1));
                    contenido1 = contenido1 + " " + contenido;
                    contenidoColumna[cont] = contenido1;
                    rowS[cont] = rowSpan;
                    colS[cont] = colSpan;
                    columna = columna.Substring(faux + marcadorFin.Length, columna.Length - (faux + marcadorFin.Length));

                }
                else
                {
                    columna = null;
                }
            }
            if (contenido1.Equals(""))
            {
                contenidoColumna[cont] = "";
                rowS[cont] = "1";
                colS[cont] = "1";
            }

            return contenido1;
        }
        public static String createTableXML(String S)
        {

            getRows(S);
            int colmax = 0;

            for (int i = 0; i <= numrenglones; i++)
            {
                if (i == 0)
                    colmax = rows[i];

                if (rows[i] > colmax)
                    colmax = rows[i];
            }

            tabla = "<Tabla xmlns:aid=" + "\"http://ns.adobe.com/AdobeInDesign/4.0/" + "\" aid:table=" + "\"table" + "\" aid:trows=" + "\"" + numrenglones + "\" aid:tcols=" + "\"" + colmax.ToString() + "\">";
            int celdas = numcolumnas * numrenglones;
            for (int i = 0; i < j; i++)
            {
                tabla += "<Celda aid:table=" + "\"cell" + "\" aid:crows=" + "\"" + rowS[i] + "" + "\" aid:ccols=" + "\"" + colS[i] + "" + "\">" + contenidoColumna[i] + "</Celda>";
            }
            j = 0;
            tabla += "</Tabla>";
            rowS = new String[8000];
            colS = new String[8000];
            contenidoColumna = new String[8000];
            rows = new int[800];

            return tabla;


        }
        public static String getTableByAttribute(String rubro)
        {
            String tablaFin = "</table>";
            String tabla = "";
            int tableIni;
            int tableFin;
            String tablaXML = "";
            String contenidoColumna = rubro;
            int i = 0;

            while (rubro != "")
            {
                tableIni = rubro.IndexOf("<table");

                if (tableIni >= 0)
                {
                    tableFin = rubro.IndexOf("</table>", tableIni);
                    tabla = rubro.Substring(tableIni, (tableFin - tableIni) + tablaFin.Length);
                    tablaXML = createTableXML(tabla);


                    rubro = rubro.Replace(tabla, tablaXML);
                    contenidoColumna = rubro;
                }
                else rubro = "";
                i++;
            }

            return contenidoColumna;

        }
        #endregion

        public static int getDataFromFile(String file, String namefile, int editionId)
        {
            StreamReader SR;
            SR = File.OpenText(file);
            String S = SR.ReadToEnd();
            String pharmaFormId;
            String productId;
            String[] name = namefile.Split('_');

            productId = name[0];
            pharmaFormId = name[1];
            SR.Close();
            return insertXMLToDB(S, productId, pharmaFormId, editionId);


        }

        public static int insertXMLToDB(String contenido, String productId, String pharmaFormId, int editionId)
        {
            String sSQL = "";
            int inserted = 0;
            sSQL = "Update  tmpParticipantProducts   " +
                          " set XMLContent = '" + contenido + "' " +
                          " where editionid = " + editionId + " and productId =" + productId +
                          " and PharmaFormId =" + pharmaFormId +
                          " ";
            inserted += ConnectionDB.ExecuteNonQuery(CommandType.Text, sSQL);
            return inserted;
        }

        public static String cleanHTMLcode(String cadena)
        {
            cadena = cadena.Replace("&aacute;", "á");
            cadena = cadena.Replace("&eacute;", "é");
            cadena = cadena.Replace("&iacute;", "í");
            cadena = cadena.Replace("&oacute;", "ó");
            cadena = cadena.Replace("&uacute;", "ú");

            cadena = cadena.Replace("&Aacute;", "Á");
            cadena = cadena.Replace("&Eacute;", "É");
            cadena = cadena.Replace("&Iacute;", "Í");
            cadena = cadena.Replace("&Oacute;", "Ó");
            cadena = cadena.Replace("&Uacute;", "Ú");

            cadena = cadena.Replace("&Auml;", "Ä");
            cadena = cadena.Replace("&Euml;", "Ë");
            cadena = cadena.Replace("&Iuml;", "Ï");
            cadena = cadena.Replace("&Ouml;", "Ö");
            cadena = cadena.Replace("&Uuml;", "Ü");

            cadena = cadena.Replace("&auml;", "ä");
            cadena = cadena.Replace("&euml;", "ë");
            cadena = cadena.Replace("&iuml;", "ï");
            cadena = cadena.Replace("&ouml;", "ö");
            cadena = cadena.Replace("&uuml;", "ü");

            cadena = cadena.Replace("&ntilde;", "ñ");
            cadena = cadena.Replace("&Ntilde;", "Ñ");

            //cadena = cadena.Replace("&ntilde;", "�");
            cadena = cadena.Replace("file://", "");
            cadena = cadena.Replace("&nbsp;", " ");

            return cadena;
        }

        public static String cleanImage(String S)
        {
            String imagen;
            int i, f;
            String content = S;

            while (S != null)
            {
                i = S.IndexOf("<Imagen ");
                if (i >= 0)
                {
                    f = S.IndexOf("/>", i);
                    imagen = S.Substring(i, (f + 2) - i);
                    S = S.Replace(imagen, " ");
                    content = S;
                }
                else S = null;
            }
            return content;
        }


        public static String cleanTable(String S)
        {
            String table;
            int i, f;
            String content = S;

            while (S != null)
            {
                i = S.IndexOf("<Tabla ");
                if (i >= 0)
                {
                    f = S.IndexOf("</Tabla>", i);
                    table = S.Substring(i, (f + 8) - i);
                    S = S.Replace(table, " ");
                    content = S;
                }
                else S = null;
            }
            return content;


        }
        public static String tabla;
        public static int numrenglones;
        public static int j = 0;
        public static int numcolumnas;
        public static String[] contenidoColumna = new String[8000];
        public static String[] rowS = new String[8000];
        public static String[] colS = new String[8000];
        public static int[] rows = new int[800];
        public static List<string> lis = new List<string>();






    //    public static readonly SegmentHtml instance = new SegmentHtml();
        protected static Database ConnectionDB = DatabaseFactory.CreateDatabase("MedinetConnectionString");
    }
}
