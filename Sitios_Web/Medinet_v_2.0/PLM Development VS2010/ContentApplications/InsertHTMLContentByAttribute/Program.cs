using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.IO;

using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;


namespace InsertHTMLContentByAttribute
{
    class Program4
    {
        static void Main(string[] args)
        {
            DirectoryInfo dir = new System.IO.DirectoryInfo("\\\\195.192.2.17\\Proyectos\\Obras PLM\\Ecuador\\DEF 38\\XML\\HTML\\no cargados\\mal\\");
            String nameFile = "";
            foreach (FileInfo f in dir.GetFiles("*.htm"))
            {
                nameFile = f.Name;
                nameFile = nameFile.Replace(".htm", "");
                //insertXMLContent(f.DirectoryName + "\\" + f.Name, nameFile);
                insertHTMLContent(f.DirectoryName + "\\" + f.Name, nameFile);
                //cleanHTML(f.DirectoryName + "\\" + f.Name, nameFile);
                //getAttribute(f.DirectoryName + "\\" + f.Name, f.Name);
                //Console.WriteLine(f.DirectoryName + "\\" + f.Name);                
                //Console.ReadLine();
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

        public static void insertXMLContent(String file, String nameFile)
        {
            StreamReader SR = File.OpenText(file);
            String[] split = nameFile.Split('_');
            String productId = split[0];
            String pharmaId = split[1];

            String S = SR.ReadToEnd();

            String sSQL = " Update ParticipantProducts " +
                            " Set XMLContent = '" + S + "'" +
                            " Where productid = " + productId +
                            " and pharmaformid = " + pharmaId +
                            " and editionid= 20";

            ConnectionDB.ExecuteNonQuery(CommandType.Text, sSQL);

            SR.Close();
        }


        public static void getAttribute(String F, String productId, String pharmaId)
        //public static void getAttribute(String F, String FileName)
        {
            StreamReader SR;
            String S;
            String rubro = "";            
            //String marcador = "<p class=" + "\"normal" + "\"><span class=" + "\"rubros-azules" + "\">";
            //String marcador ="<p class=\"normal\"><span class=\"letra-cyan";
            String marcador = "<p class=\"normal\"><span class=\"subt";
            String marcadorF = "</span>";
            String rubroNombre = " ";
            String rubroContenido = null;
            String rubroC = "";
            int i = 0, f = 0, iaux = 0, faux = 0;
            SR = File.OpenText(F);
            S = SR.ReadToEnd();
            S = S.Replace("\t", "");

            String sSQL = "";
            String sSQLUpdate = "";
            String attributeId = "";

            int ff=0;
                         
            while (S != null)
            {
                i = S.IndexOf(marcador);
                if (i >= 0)
                {
                    f = S.IndexOf(marcadorF, i);
                    //ff = S.IndexOf(">", i);
                    ff = S.IndexOf(">", i + marcador.Length);
                    //rubroNombre = S.Substring(i + marcador.Length, f - (i + marcador.Length));
                    rubroNombre = S.Substring(ff + 1, f-(ff + 1));
                    rubroNombre = cleanRubro(rubroNombre);
                                        
                    iaux = S.IndexOf(marcador, f);                    

                    if (iaux >= 0)
                    {                        
                        rubroContenido = S.Substring(i, iaux-i);
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


                    sSQL = "Select top 1 AttributeId from Attributes where Description = '" + rubroNombre + "'";

                    IDataReader iDReader = ConnectionDB.ExecuteReader(CommandType.Text, sSQL);

                    while (iDReader.Read())
                    {

                        attributeId = iDReader["attributeId"].ToString();

                        //sSQLUpdate = " INSERT INTO [tmpProductsContentDEF58] " +
                        //             "  ([HTMLFile] " +
                        //             "  ,[AttributeId] " +
                        //             "  ,[HTMLContent]) " +
                        //             "   VALUES " +
                        //             "  ( '" + FileName + "'" + 
                        //             "  , " + attributeId + 
                        //             "  , '" + rubroContenido + "')";


                        sSQLUpdate = "Update tmpProductContents " +
                                     " set HTMLContent = '" + rubroContenido + "'" +
                                     " where pharmaFormId = " + pharmaId +
                                     " and productId = " + productId +
                                     " and attributeId = " + attributeId +
                                     " and editionid = 58" +
                                     " and HTMLContent is null";

                        ConnectionDB.ExecuteNonQuery(CommandType.Text, sSQLUpdate);

                    } iDReader.Close();
                }
                else S = null;
            }           
            SR.Close();
            //return rubroC;
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

            
            rubroNombre = rubroNombre.Replace(":", "");
            rubroNombre = rubroNombre.Replace(".", "");
            //rubroNombre = rubroNombre.Replace(",", "");
            rubroNombre = rubroNombre.Replace(";", "");
            
                  
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
            rubroNombre = rubroNombre.Replace("&#174;", "");

            rubroNombre = rubroNombre.ToUpper();
            rubroNombre = rubroNombre.Trim();
                                              
            return rubroNombre;
        }

        public static void cleanHTML(String file, String nameFile)
        {
            StreamReader SR;
            StreamWriter SW;
            SR = File.OpenText(file);
            String S = SR.ReadToEnd();

            int i;
            int f;
            String pie="";
            String cont="";
            
            while (S != null)
            {
                i = S.IndexOf("<p class=" + "\"pie1" + "\">");
                cont = S;
                if (i >= 0)
                {
                    f = S.IndexOf("</p>", i);
                    pie = S.Substring(i, (f +4) -i);
                    S = S.Replace(pie, "");
                    cont = S;
                }
                else S = null;

            }

            i = cont.IndexOf("<img");
            if (i >= 0)
            {
                f = cont.IndexOf(" />", i);
                pie = cont.Substring(i, (f + 3) - i);
                cont = cont.Replace(pie, "");
                //cont = S;
            }
            String path ="C:\\Documents and Settings\\bvazquez001\\Mis documentos\\Discos\\PLM Móvil Ecuador\\HTML PLM Móvil Ecuador\\HTML PLM Móvil Ecuador\\prods\\";
            SW = File.CreateText(path + nameFile + ".htm");
            SW.Write(cont);
            SW.Close();
            SR.Close();
        }

        protected static Database ConnectionDB = DatabaseFactory.CreateDatabase("MedinetConnectionString");
        protected static Database ConnectionDB1 = DatabaseFactory.CreateDatabase("MedinetConnectionString");

    }
}
