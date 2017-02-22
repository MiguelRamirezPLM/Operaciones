using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;
using System.Data;
using System.Data.SqlClient;

using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace XMLInsertRubros
{
    class Program
    {
        static void Main(string[] args)
        {
            insertContent();            
        }

        public static void insertContent()
        {
            String connect = "Data Source=195.192.2.17;Initial Catalog=Medinet 20120625;User ID=sa;Password=t0m$0nl@t1n@";
            //String connect = "Data Source=localhost;Initial Catalog=Medinet 20110623;Integrated Security=True";
            String sSQL = "";
            String sSQL1 = "";
            String sSQL2 = "";
            String sSQL3 = "";
            String titulo = "<Titulo>";
            String contenido = "<Contenido>";

            SqlConnection connection = new SqlConnection(connect);
            connection.Open();         

            String productId = "";
            String pharmaId = "";
            String editionId = "";
            String divisionId = "";
            String categoryId = "";
            String plainContent = "";
            String content = "";
            int i, fin, iContent, fContent;

            String S ="";
            String attributeDescription = "";
            String attributeId = "";

            sSQL = "Select EditionId, PharmaFormId, ProductId, DivisionId, CategoryId, XMLContent from ParticipantProducts " +
                    "where Editionid=58 ";
            //sSQL = "Select * from tmpFaltantesRoche";

            IDataReader Reader = ConnectionDB.ExecuteReader(CommandType.Text, sSQL);
             
            int count = 0;

            while (Reader.Read())
            {
                count++;
                S = Reader["XMLContent"].ToString();
                productId = Reader["ProductId"].ToString();
                pharmaId = Reader["PharmaFormId"].ToString();
                editionId = Reader["EditionId"].ToString();
                divisionId = Reader["DivisionId"].ToString();
                categoryId = Reader["CategoryId"].ToString();

                while (S != null)
                {
                    i = S.IndexOf("<Titulo>");

                    if (i >= 0)
                    {
                        fin = S.IndexOf("</Titulo>", i);
                        attributeDescription = S.Substring(i + titulo.Length, fin - (i + titulo.Length));
                        if (attributeDescription != "")
                        {
                            attributeDescription = cleanText(attributeDescription);

                            //sSQL1 = "Select top 1 AttributeId, Description from Attributes where Description COLLATE SQL_LATIN1_GENERAL_CP1_CI_AI LIKE '%" + attributeDescription + "%' and Active = 1";
                            sSQL1 = "Select top 1 AttributeId from Attributes where Description = '" + attributeDescription + "'";

                            IDataReader Reader1 = ConnectionDB1.ExecuteReader(CommandType.Text, sSQL1);

                            while (Reader1.Read())
                            {
                                attributeId = Reader1["AttributeId"].ToString();

                            }
                            Reader1.Close();

                            //if (attributeId.Equals(""))
                            //{
                            //    sSQL3 = "INSERT INTO tmpAttributes " +
                            //                "(Description " +
                            //                ",TechnicalSheet " +
                            //                ",Active) " +
                            //            " VALUES " +
                            //                "('" + attributeDescription + "' " +
                            //                ",0 " +
                            //                ",1) ";

                            //    ConnectionDB1.ExecuteNonQuery(CommandType.Text, sSQL3);
                                

                            //    sSQL1 = "Select top 1 AttributeId from tmpAttributes where Description = '" + attributeDescription + "'";

                            //    IDataReader Reader1 = ConnectionDB1.ExecuteReader(CommandType.Text, sSQL1);

                            //    while (Reader1.Read())
                            //    {
                            //        attributeId = Reader1["AttributeId"].ToString();

                            //    }
                            //    Reader1.Close();
                            //    attributeDescription = "";
                            //}

                        }

                        iContent = S.IndexOf("<Contenido>", fin);
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


                            sSQL2 = "INSERT INTO [tmpProductContents] " +
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

                            ConnectionDB1.ExecuteNonQuery(CommandType.Text, sSQL2);

                            //productId = "";
                            attributeId = "";
                            content = "";
                            //editionId = "";
                            //pharmaId = "";
                            plainContent = "";
                            //divisionId = "";
                            //categoryId = "";


                        }
                    }
                    else S = null;               
            }          
        }
        }

        public static String cleanText(String cadena)
        {
            cadena = cadena.Replace(":", "");
            cadena = cadena.Replace(";", "");
            cadena = cadena.Replace(",", "");
            cadena = cadena.Trim();

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

            cadena = cadena.Replace("&Ntilde;", "Ñ");
            cadena = cadena.Replace("&ntilde;", "ñ");
            //cadena = cadena.Replace(".", "");
           
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

        protected static Database ConnectionDB = DatabaseFactory.CreateDatabase("MedinetConnectionString");
        protected static Database ConnectionDB1 = DatabaseFactory.CreateDatabase("MedinetConnectionString");
       

    }
 }

