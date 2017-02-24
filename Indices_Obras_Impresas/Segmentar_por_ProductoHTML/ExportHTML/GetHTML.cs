using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Text;
using System.IO;


namespace ExportHTML
{
    public class GetHTML : PLMDataAccessAdapter<object>
    {

        public GetHTML() {}

        public string getHtmlContent(string sourcePath)
        {
            if (string.IsNullOrEmpty(sourcePath))
                throw new ArgumentException("The source path cannot be null or empty.");

            StreamReader sr = new StreamReader(sourcePath, System.Text.Encoding.UTF8);
            string strHtml = sr.ReadToEnd();

            sr.Close();
            sr.Dispose();

            return strHtml;
        }

        public void saveHtmlFile(string destinationPath, string fileName, string fileContent)
        {
            if (string.IsNullOrEmpty(destinationPath))
                throw new ArgumentException("The destination path cannot be null or empty.");


            fileName = fileName.Replace("|", "");
            fileName = fileName.Replace("\r\n\t\t\t", "_");
            FileStream htmlFile = new FileStream(destinationPath + "\\" + fileName, FileMode.Create);

            StreamWriter sw = new StreamWriter(htmlFile, System.Text.Encoding.UTF8);

            fileContent = fileContent.Replace("</div>\n\t</body>\n</html>\n\r\n\r\n\t\t\t</div>\r\n\t\t</div>\r\n\t</body>\r\n</html>\r\n", "\t</div>\r\n\t\t</div>\r\n\t</body>\r\n</html>\r\n");

            sw.Write(fileContent);

            

            sw.Close();
            sw.Dispose();
        }

        public void saveLogFile(string destinationPath, string fileName, string fileContent)
        {
            if (string.IsNullOrEmpty(destinationPath))
                throw new ArgumentException("The destination path cannot be null or empty.");

            FileStream htmlFile = new FileStream(destinationPath + "\\" + fileName, FileMode.Create);
            StreamWriter sw = new StreamWriter(htmlFile, System.Text.Encoding.Unicode);
            sw.Write(fileContent);

            sw.Close();
            sw.Dispose();
        }

        public void setParameters(StringBuilder sbBody, params string[] htmlParameters)
        {
            for (int i = 0; i < htmlParameters.Length; i++)
            {
                string paramName = htmlParameters[i].Split('=')[0];
                string paramValue = htmlParameters[i].Split('=')[1];

                sbBody.Replace(paramName, paramValue);
            }
        }

        public string quitAccents(string originalString)
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

            sb.Replace("ñ", "&ntilde;");
            sb.Replace("Ñ", "&Ntilde;");

            sb.Replace("ü", "&uuml;");
            sb.Replace("Ü", "&Uuml;");

            sb.Replace(" <br />"," "); 
            sb.Replace("-<br />","");

            sb.Replace("®", "&#174;");

            sb.Replace("%2520","%20");
            //sb.Replace("™", "");
            
            
            return sb.ToString();
        }

        public string quitAccentsFileName(string originalString)
        {
            int ini, med;
            ini = 0;
            med = 0;
            string token;

            System.Text.StringBuilder sb = new System.Text.StringBuilder(originalString);

            sb.Replace("á", "a");
            sb.Replace("Á", "A");

            sb.Replace("é", "e");
            sb.Replace("É", "E");

            sb.Replace("í", "i");
            sb.Replace("Í", "I");

            sb.Replace("ó", "o");
            sb.Replace("Ó", "O");

            sb.Replace("ú", "u");
            sb.Replace("Ú", "U");

            sb.Replace("ü", "u");

            sb.Replace("ñ", "n");
            sb.Replace("&", "and");
            sb.Replace("'", "");
            sb.Replace("<br />", " ");
            sb.Replace("*", "");
            sb.Replace("®", "");
            sb.Replace(".", "");
            sb.Replace(" ", "");
            sb.Replace("/", "");
            sb.Replace("%", "");
            sb.Replace("+", "");
            sb.Replace("'", "");
            sb.Replace("&quot;", "");
            sb.Replace(",", "");
            sb.Replace("-", "_");
            sb.Replace(":", "");
            sb.Replace(";", "");

            sb.Replace("&#174;", "");
            sb.Replace("&#9;", "");
            sb.Replace("#9", "");
            sb.Replace("Ning&uacute;n-estilo-de-tabla", "Ningun-estilo-de-tabla"); 


            if (sb.ToString().IndexOf("<", 0) >= 0)
                for (int i = 0; i < sb.Length; i++)
                {
                    ini = sb.ToString().IndexOf("<", 0);
                    med = sb.ToString().IndexOf(">", 0);

                    token = sb.ToString().Substring(ini, ((med + 1) - ini));

                    sb.Replace(token, "");

                    if (sb.ToString().IndexOf("<", 0) == -1)
                        i = sb.Length;

                }

            sb = sb.Replace("</span>", "");

            return sb.ToString();

            
        }

        public string addAccents(string cadena)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder(cadena);

            sb.Replace("&aacute;","á");
            sb.Replace("&Aacute;","Á");

            sb.Replace("&eacute;","é");
            sb.Replace("&Eacute;","É");

            sb.Replace("&iacute;","í");
            sb.Replace("&Iacute;","Í");

            sb.Replace("&oacute;","ó");
            sb.Replace("&Oacute;","Ó");

            sb.Replace("&uacute;","ú");
            sb.Replace("&Uacute;","Ú");

            sb.Replace("</br>", "");

            sb.Replace("&ntilde;", "ñ");
            sb.Replace("&Ntilde;", "Ñ");

            sb.Replace("&uml;", "ü");
            sb.Replace("&Uml;", "Ü");

            return sb.ToString();
        }

        public string getString(string cadena, string token)
        {
            string cad;
            if (cadena.IndexOf(token) != -1)
            {
                cad = cadena.Substring(cadena.IndexOf(token), cadena.IndexOf("<", (cadena.IndexOf(token) + token.Length)) - cadena.IndexOf(token));
                return cad;
                
            }
            else
                return string.Empty;

        }

        //public DataTable checkName(string prodName)
        //{
        //    StringBuilder sb = new StringBuilder();

        //    sb.Append("SELECT DISTINCT P.ProductID,P.Brand ");
        //    sb.Append("\nFROM Products P");
        //    sb.Append("\nINNER JOIN ProductVersionPages PV ON(P.ProductId = PV.ProductId) ");
        //    sb.Append("\nWHERE PV.VersionId = 10 AND P.COUNTRYID = 14 AND P.ACTIVE = 1 AND P.BRAND = '" + prodName + "'");
        //    sb.Append("\nORDER BY 1");

        //    DataSet ds = GetHTML.HTMLDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());


        //    if (ds.Tables[0].Rows.Count == 0)
        //    {
        //        DataSet ds2 = new DataSet();

        //        for (int x = 0, y = prodName.Length - 1; x < 2; x++, y--)
        //        {
        //            StringBuilder sb2 = new StringBuilder();

        //            sb2.Append("SELECT DISTINCT P.ProductID,P.Brand ");
        //            sb2.Append("\nFROM Products P");
        //            sb2.Append("\nINNER JOIN ProductVersionPages PV ON(P.ProductId = PV.ProductId) ");
        //            sb2.Append("\nWHERE PV.VersionId = 10 AND P.COUNTRYID = 14 AND P.ACTIVE = 1 AND P.BRAND COLLATE SQL_Latin1_General_CP1_CI_AI LIKE ('" + prodName.Substring(0, y) + "%') ");
        //            sb2.Append("\nORDER BY 1");

        //            ds2 = GetHTML.HTMLDatabase.ExecuteDataSet(CommandType.Text, sb2.ToString());

        //            if (ds2.Tables[0].Rows.Count == 1)
        //            {
        //                //this.ProductId = Convert.ToInt32(ds2.Tables[0].Rows[0]["ProductId"].ToString());
        //                x = 2;
        //            }
        //        }
        //        return ds2.Tables[0];
        //    }
        //    else
        //        return ds.Tables[0];
            
        //}

        //public DataTable checkNameMentionated(string prodName)
        //{
        //    StringBuilder sb = new StringBuilder();

        //    sb.Append("SELECT DISTINCT P.ProductID,P.Brand ");
        //    sb.Append("\nFROM Products P");
        //    sb.Append("\nINNER JOIN MentionatedProducts PV ON(P.ProductId = PV.ProductId) ");
        //    sb.Append("\nWHERE PV.EditionId = 3 AND P.COUNTRYID = 14 AND P.ACTIVE = 1 AND P.BRAND = '" + prodName + "'");
        //    sb.Append("\nORDER BY 1");

        //    DataSet ds = GetHTML.HTMLDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());


        //    if (ds.Tables[0].Rows.Count == 0)
        //    {
        //        DataSet ds2 = new DataSet();

        //        for (int x = 0, y = prodName.Length - 1; x < 2; x++, y--)
        //        {
        //            StringBuilder sb2 = new StringBuilder();

        //            sb2.Append("SELECT DISTINCT P.ProductID,P.Brand ");
        //            sb2.Append("\nFROM Products P");
        //            sb2.Append("\nINNER JOIN MentionatedProducts PV ON(P.ProductId = PV.ProductId) ");
        //            sb2.Append("\nWHERE PV.EditionId = 3 AND P.COUNTRYID = 14 AND P.ACTIVE = 1 AND P.BRAND COLLATE SQL_Latin1_General_CP1_CI_AI LIKE ('" + prodName.Substring(0, y) + "%') ");
        //            sb2.Append("\nORDER BY 1");

        //            ds2 = GetHTML.HTMLDatabase.ExecuteDataSet(CommandType.Text, sb2.ToString());

        //            if (ds2.Tables[0].Rows.Count == 1)
        //            {
        //                //this.ProductId = Convert.ToInt32(ds2.Tables[0].Rows[0]["ProductId"].ToString());
        //                x = 2;
        //            }
        //        }
        //        return ds2.Tables[0];
        //    }
        //    else
        //        return ds.Tables[0];

        //}

        //public string getPharmaForms(int productId)
        //{
        //    string pharmaForms="";

        //    StringBuilder sb = new StringBuilder();

        //    sb.Append("SELECT PharmaFormId,Page");
        //    sb.Append("\nFROM ProductVersionPages ");
        //    sb.Append("\nWHERE VersionId = 10 AND ProductId = " + productId);
        //    sb.Append("\nORDER BY 1");

        //    DataSet ds = GetHTML.HTMLDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());

        //    if (ds.Tables[0].Rows.Count > 0)
        //    {
        //        string page = "";
        //        for (int x = 0; x < ds.Tables[0].Rows.Count; x++)
        //        {

        //            if (x == 0)
        //            {
        //                pharmaForms = ds.Tables[0].Rows[x][0].ToString();
        //                page = ds.Tables[0].Rows[x][1].ToString();
        //            }
        //            else
        //            {
        //                if(page.Equals(ds.Tables[0].Rows[x][1].ToString()))
        //                    pharmaForms = pharmaForms + "," + ds.Tables[0].Rows[x][0].ToString();

        //            }
        //        }
        //    }

        //    return pharmaForms;
        //}

        //public int insertDescrip(string divName, string dir, string img)
        //{
        //    StringBuilder sb = new StringBuilder();

        //    sb.Append("SELECT * ");
        //    sb.Append("FROM TMPDIVISIONES ");
        //    sb.Append("WHERE HTMLFILE LIKE '" + divName + "%' ");
        //    sb.Append("ORDER BY 1");
                   
        //    DataSet ds = GetHTML.HTMLDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());

        //    if (ds.Tables[0].Rows.Count != 0)
        //    {
        //        StringBuilder sb2 = new StringBuilder();

        //        img = img.Replace(".eps", ".jpg");
        //        img = img.Replace(".tif", ".jpg");

        //        sb2.Append("INSERT INTO [dbo].[DivisionInformation]([DivisionId],[Address],[Image],[Active]) ");
        //        sb2.Append("VALUES (" + ds.Tables[0].Rows[0].ItemArray[0].ToString() + ",");
        //        sb2.Append("'" + dir + "','" + img + "',1)");



        //        return GetHTML.HTMLDatabase.ExecuteNonQuery(CommandType.Text, sb2.ToString());
        //    }
        //    else
        //        return 0;

        //}

        //public void loadHTML(string productId, string pharmaFormId, string contenido)
        //{
        //    StringBuilder sb2 = new StringBuilder();

        //    sb2.Append("INSERT INTO [dbo].[EditionProductContents]([EditionId],[PharmaFormId],[ProductId],[HTMLContent],[XMLContent],[VersionId]) ");
        //    sb2.Append("VALUES (5," + pharmaFormId + ",");
        //    sb2.Append(productId + ",'" + contenido + "','',11) ");

        //    GetHTML.HTMLDatabase.ExecuteNonQuery(CommandType.Text, sb2.ToString());

        //}
        
        //public void loadHTML2(string productId, string pharmaFormId, string categoryid, string divisionid, string contenido)
        //{
        //    StringBuilder sb2 = new StringBuilder();

        //    sb2.Append("update ParticipantProducts set  [HTMLContent] ='" + contenido + "' where productId=" + productId + " and " );
        //    sb2.Append("pharmaformid="+pharmaFormId + " and divisionid = " + divisionid + " and categoryid = " + categoryid + " and editionid=6 ");

        //    GetHTML.HTMLDatabase.ExecuteNonQuery(CommandType.Text, sb2.ToString());

        //}
        
        //public void replaceHTMLContent()
        //{
        //    string contenido = "";
        //    StringBuilder sb2 = new StringBuilder();

        //    sb2.Append("select productId,PharmaformId,HtmlContent from ParticipantProducts ");
        //    sb2.Append("where  editionid=18 ");

        //    DataSet ds = GetHTML.HTMLDatabase.ExecuteDataSet(CommandType.Text, sb2.ToString());

        //    for (int x = 0; x < ds.Tables[0].Rows.Count; x++)
        //        {
        //            contenido=ds.Tables[0].Rows[x][2].ToString();
        //               contenido=contenido.Replace("<!--<link href=\"estilos.css\" rel=\"stylesheet\" type=\"text/css\" />-->","<link href=\"estilos.css\" rel=\"stylesheet\" type=\"text/css\" />");
                       
        //        //loadHTML2(ds.Tables[0].Rows[x][0].ToString(), ds.Tables[0].Rows[x][1].ToString(), contenido);
        //        }

        //}
        
        ////public void updatehtmlContent(string productId, string pharmaFormId,string editionid,string divisionId, string categoryId ,string contenido)
        ////{
        //  //  StringBuilder sb2 = new StringBuilder();

        // //   sb2.Append("update participantproducts set htmlcontent= '"+contenido + "'");
        // //   sb2.Append("\nwhere productid=" + productId + " and editionid=" + editionid + " and pharmaformid=" + pharmaFormId);
        // //   sb2.Append("\n and categoryId = " + categoryId + " and divisionId = " + divisionId);
        ////
        //  //  GetHTML.PEV30Database.ExecuteNonQuery(CommandType.Text, sb2.ToString());

        //// }

        //public void updatehtmlContent(string edProdId, string editionId, string sectionId, string productId, string contenido)
        //{
        //    StringBuilder sb2 = new StringBuilder();

        //    sb2.Append("update EditionProductSections set HtmlContent = '" + contenido + "'");
        //    sb2.Append("\nwhere edProdId = " + edProdId + " and editionId = " + editionId + " and sectionId = " + sectionId);
        //    sb2.Append("\n and productId = " + productId);

        //    GetHTML.PEV30Database.ExecuteNonQuery(CommandType.Text, sb2.ToString());
        //}

        //public void loadVBM(int editionid,int divisionid, int genericid, int pharmaformid, string content, string page)
        //{
        //    StringBuilder sb2 = new StringBuilder();

        //    sb2.Append("INSERT INTO [dbo].[ParticipantGenerics]([EditionId],[DivisionId],[GenericId],[PharmaFormId],[Page],[HTMLContent],[XMLContent],[NewContent]) ");
        //    sb2.Append("VALUES (" + editionid +"," + divisionid + ",");
        //    sb2.Append(genericid + "," + pharmaformid + ",'" + page + "','" + content + "','',0)");

        //    GetHTML.VMBDatabase.ExecuteNonQuery(CommandType.Text, sb2.ToString());
        //}

        //public void getHTMLContentWI()
        //{
        //    StringBuilder sb = new StringBuilder();

        //    //sb.Append("Select ProductId,PharmaFormId,HTMLContent From ParticipantProducts Where EditionId = 16");
        //    sb.Append("Select GenericId,DivisionId,HTMLContent From DivisionGenerics Where EditionId = 1 ");


        //    DataSet ds = GetHTML.HTMLDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());

        //    for (int x = 0; x < ds.Tables[0].Rows.Count; x++)
        //    {

        //        //saveHtmlFile("C:/WestIndies/CD/prods/", ds.Tables[0].Rows[x]["ProductId"].ToString() + "_"
        //        //    + ds.Tables[0].Rows[x]["PharmaFormId"].ToString() + ".htm", ds.Tables[0].Rows[x]["HTMLContent"].ToString());

        //        saveHtmlFile("C:/VMB/CD/prods/", ds.Tables[0].Rows[x]["GenericId"].ToString() + ".htm", ds.Tables[0].Rows[x]["HTMLContent"].ToString());

        //    }
        //}

        //public void getFile()
        //{
        //    string contenido;
            
        //    string falt="";

        //    StringBuilder sb = new StringBuilder();

        //    //sb.Append("SELECT T.PRODUCTID,T.PHARMAFORMID,T.BRAND,T.FORMAFARMACEUTICA,T.HTMLFILE  ");
        //    //sb.Append("FROM TMPLISTADO T ");
        //    //sb.Append("INNER JOIN (SELECT COUNT(*) AS NUM, PRODUCTID,PHARMAFORMID FROM TMPLISTADO WHERE BRAND IS NOT NULL ");
        //    //sb.Append("GROUP BY PRODUCTID,PHARMAFORMID ");
        //    //sb.Append("HAVING COUNT(*) = 1) TMP ");
        //    //sb.Append("ON(T.PRODUCTID = TMP.PRODUCTID AND T.PHARMAFORMID = TMP.PHARMAFORMID) ");
        //    //sb.Append("INNER JOIN dbo.plm_vwPagedProductsMx PP ON(T.PRODUCTID = PP.PRODUCTID AND T.PHARMAFORMID = PP.PHARMAFORMID) ");
        //    //sb.Append("ORDER BY 2");

        //    //sb.Append("SELECT T.PRODUCTID,T.PHARMAFORMID,T.BRAND,T.FORMAFARMACEUTICA,T.HTMLFILE ");
        //    //sb.Append("FROM TMPLISTADO T ");
        //    //sb.Append("INNER JOIN dbo.plm_vwPagedProductsMx V ON (T.PRODUCTID = V.PRODUCTID AND T.PHARMAFORMID = V.PHARMAFORMID) ");
        //    //sb.Append("LEFT JOIN EDITIONPRODUCTCONTENTSMX E ON (T.PRODUCTID = E.PRODUCTID AND T.PHARMAFORMID = E.PHARMAFORMID) ");
        //    //sb.Append("WHERE HTMLCONTENT IS NULL AND T.PRODUCTID NOT IN (7977,8850,7964,10561,6756) ");

        //    //sb.Append("SELECT T.PRODUCTID,T.PHARMAFORMID,T.BRAND,T.FORMAFARMACEUTICA,T.HTMLFILE ");
        //    //sb.Append("FROM TMPMODIFICADOS T ");
        //    //sb.Append("WHERE T.HTMLFILE IS NOT NULL ");

        //    //sb.Append("SELECT DISTINCT PRODUCID,PHARMAFORMID,HTML ");
        //    //sb.Append("FROM TMPJMROREFERMED ");
        //    //sb.Append("WHERE PRODUCID IS NOT NULL AND PHARMAFORMID IS NOT NULL AND HTML IS NOT NULL ");

        //    //sb.Append("Select v.ProductId,v.FF1,v.HTML From plm_vwProductContenCol v");
        //    //sb.Append("\nleft join tmpdup t on(v.ProductId = t.ProductId and v.FF1 = t.FF1) ");
        //    //sb.Append("\nwhere numero is  null");

        //    //sb.Append("select t.ProductId,t.PharmaFormId,t.Archivo ");
        //    //sb.Append("\nfrom tmpfaltantes t ");
        //    //sb.Append("\ninner join participantproducts p on(t.productid = p.productid and t.pharmaformid = p.pharmaformid)");

        //    //sb.Append("select distinct p.productid,p.pharmaformid,e.html as Archivo ");
        //    //sb.Append("\nfrom tmpFaltantesCol p ");
        //    //sb.Append("\nleft join tmpprodhtmls e on(p.productid = e.productid) where e.productid is not null");

        //    //sb.Append("select distinct p.productid,p.pharmaformid,p.html as Archivo ");
        //    //sb.Append("\nfrom tmpPeruHTMLS p ");
        //    //sb.Append("\nleft join productversionpages pv on(p.productid = pv.productid and p.pharmaformid = pv.pharmaformid) ");
        //    //sb.Append("\nwhere pv.productid is not null");

        //    //sb.Append("SELECT DISTINCT T.PRODUCTID,T.PHARMAFORMID,T.HTMLNAME AS Archivo ");
        //    //sb.Append("\nFROM (SELECT *");
        //    //sb.Append("\nFROM TMPHTMLSCAD t ");
        //    //sb.Append("\nLEFT JOIN TMPPRODS2 t2 on(t.HTMLNAME = t2.HTML) ");
        //    //sb.Append("\nWHERE t2.NOMBREHTML IS NOT NULL AND HTMLNAME IS NOT NULL) T ");
        //    //sb.Append("\nINNER JOIN PRODUCTVERSIONPAGES PV ON(T.PRODUCTID = PV.PRODUCTID AND T.PHARMAFORMID = PV.PHARMAFORMID) ");
        //    //sb.Append("\nWHERE PV.PRODUCTID IS NOT NULL ");
        //    //sb.Append("\nUNION ");
        //    //sb.Append("SELECT DISTINCT T.PRODUCTID,T.PHARMAFORMID,T.HTMLNEW AS Archivo  ");
        //    //sb.Append("\nFROM TMPFALTCAD T ");
        //    //sb.Append("\nINNER JOIN PRODUCTVERSIONPAGES PV ON(T.PRODUCTID = PV.PRODUCTID AND T.PHARMAFORMID = PV.PHARMAFORMID) ");
        //    //sb.Append("\nWHERE PV.PRODUCTID IS NOT NULL ");

        //    //sb.Append("\nSelect Distinct ProductId,FormaId, HTML,HTMLSeg ");
        //    //sb.Append("\nFrom tmphtmlsbreves ");
        //    //sb.Append("\nOrder by 1 ");

        //    //sb.Append("\nSelect Distinct p.ProductId,p.PharmaFormId ");
        //    //sb.Append("\nFrom ProductVersionPages p");
        //    //sb.Append("\nLeft Join BriefProducts b on(p.Productid = b.ProductId and p.PharmaFormId = b.PharmaFormId) ");
        //    //sb.Append("\nWhere VersionId = 11 and b.ProductId is null ");
        //    //sb.Append("\nOrder by 1");

        //    sb.Append("\n SELECT DISTINCT DIVISIONID,GENERICID,PHARMAFORMID,[FILE],[Page] ");
        //    sb.Append("\n FROM TMPPRODUCTS");



        //    DataSet ds = GetHTML.VMBDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());

        //    for (int x = 0; x < ds.Tables[0].Rows.Count; x++)
        //    {

        //        //if (ds.Tables[0].Rows[x].ItemArray[2].ToString().Contains(".htm#"))
        //        //{

        //            if (File.Exists(@"C:\VMB\HTMLS\" + ds.Tables[0].Rows[x].ItemArray[3].ToString()))
        //            {
        //                //contenido = getHtmlContent(@"\\195.192.2.17\Proyectos\Discos\CAD 40\Productos Breves\" + ds.Tables[0].Rows[x].ItemArray[3].ToString());

        //                contenido = getHtmlContent(@"C:\VMB\HTMLS\" + ds.Tables[0].Rows[x].ItemArray[3].ToString());
                        
        //                contenido = contenido.Replace("'", "\"");

        //                loadVBM(1, Convert.ToInt32(ds.Tables[0].Rows[x].ItemArray[0]), Convert.ToInt32(ds.Tables[0].Rows[x].ItemArray[1]), Convert.ToInt32(ds.Tables[0].Rows[x].ItemArray[2]), contenido, ds.Tables[0].Rows[x].ItemArray[4].ToString());
        //            }
        //            else
        //            {
        //                if (falt == "")
        //                //    falt = ds.Tables[0].Rows[x].ItemArray[3].ToString() + Environment.NewLine;
        //                      falt = ds.Tables[0].Rows[x].ItemArray[3].ToString() + Environment.NewLine;
        //                else
        //                    //falt = falt + ";" + ds.Tables[0].Rows[x].ItemArray[3].ToString() + Environment.NewLine;
        //                    falt = falt + ";" + ds.Tables[0].Rows[x].ItemArray[3].ToString() + Environment.NewLine;
        //            }

        //    //    }
        //    //    else
        //    //    {
        //    //        if (File.Exists(@"\\195.192.2.17\Proyectos\Discos\CAD 40\Productos Breves\" + ds.Tables[0].Rows[x].ItemArray[2].ToString()))
        //    //        {
        //    //            contenido = getHtmlContent(@"\\195.192.2.17\Proyectos\Discos\CAD 40\Productos Breves\" + ds.Tables[0].Rows[x].ItemArray[2].ToString());

        //    //            //contenido = getHtmlContent(@"\\195.192.2.17\Proyectos\Discos\CAD 40\CD\source\src\prods" + ds.Tables[0].Rows[x].ItemArray[0].ToString() + "_" + ds.Tables[0].Rows[x].ItemArray[1].ToString() + ".htm");

        //    //            contenido = contenido.Replace("'", "\"");

        //    //            loadHTML(ds.Tables[0].Rows[x].ItemArray[0].ToString(), ds.Tables[0].Rows[x].ItemArray[1].ToString(), contenido);
        //    //        }
        //    //        else
        //    //        {
        //    //            if (falt == "")
        //    //                falt = ds.Tables[0].Rows[x].ItemArray[3].ToString() + Environment.NewLine;
        //    //            //falt = ds.Tables[0].Rows[x].ItemArray[0].ToString() + "_" + ds.Tables[0].Rows[x].ItemArray[1].ToString() + ".htm" + Environment.NewLine;
        //    //            else
        //    //                falt = falt + ";" + ds.Tables[0].Rows[x].ItemArray[3].ToString() + Environment.NewLine;
        //    //            //falt = falt + ";" + ds.Tables[0].Rows[x].ItemArray[0].ToString() + "_" + ds.Tables[0].Rows[x].ItemArray[1].ToString() + ".htm" + Environment.NewLine;
        //    //        }
        //    //    }
                
        //    }
        //    saveHtmlFile(@"C:\VMB\", "faltantes.txt", falt);

        //}
        //public void getFile2()
        //{
        //    string contenido;

        //    string falt = "";

        //    StringBuilder sb = new StringBuilder();


        //    sb.Append("Select EditionId,ProductId,PharmaFormId,CategoryId,DivisionId From ParticipantProducts Where EditionId = 34");
        //    //sb.Append("\n SELECT ProductId,PharmaFormId,CategoryId,DivisionId,FileName FROM TMPCONTENTDEF57 WHERE [FILENAME] IS NOT NULL And DivisionId is not null ");
        //    //sb.Append("\n SELECT ProductId,PharmaFormId,CategoryId,DivisionId,FileName FROM tmpDEF37_Contents_02 WHERE [FILENAME] IS NOT NULL ");



        //    DataSet ds = GetHTML.HTMLDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());

        //    for (int x = 0; x < ds.Tables[0].Rows.Count; x++)
        //    {


        //        if (File.Exists(@"\\195.192.2.17\Proyectos\Discos\Ecuador\DEF 37\CD\source\src\productos\" + ds.Tables[0].Rows[x].ItemArray[1].ToString() + "_" + ds.Tables[0].Rows[x].ItemArray[2].ToString() + ".htm"))
        //        {

        //            contenido = getHtmlContent(@"\\195.192.2.17\Proyectos\Discos\Ecuador\DEF 37\CD\source\src\productos\" + ds.Tables[0].Rows[x].ItemArray[1].ToString() + "_" + ds.Tables[0].Rows[x].ItemArray[2].ToString() + ".htm");

        //            contenido = contenido.Replace("'", "\"");

        //            loadHTML2(ds.Tables[0].Rows[x].ItemArray[1].ToString(), ds.Tables[0].Rows[x].ItemArray[2].ToString(), ds.Tables[0].Rows[x].ItemArray[3].ToString(), ds.Tables[0].Rows[x].ItemArray[4].ToString(), contenido.ToString());
        //        }
        //        else
        //        {
        //            if (falt == "")
        //                //    falt = ds.Tables[0].Rows[x].ItemArray[3].ToString() + Environment.NewLine;
        //                falt = ds.Tables[0].Rows[x].ItemArray[1].ToString() + "_" + ds.Tables[0].Rows[x].ItemArray[2].ToString() + Environment.NewLine;
        //            else
        //                //falt = falt + ";" + ds.Tables[0].Rows[x].ItemArray[3].ToString() + Environment.NewLine;
        //                falt = falt + ";" + ds.Tables[0].Rows[x].ItemArray[1].ToString() + "_" + ds.Tables[0].Rows[x].ItemArray[2].ToString() + Environment.NewLine;
        //        }

               

        //    }
        //    saveLogFile(@"C:\Productos", "faltantes.txt", falt);

        //}

        public string changeImage(string cadena, string filename)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder(cadena);

            sb.Replace(".EPS", ".jpg");
            sb.Replace(".TIF", ".jpg");
            sb.Replace(".AI",".jpg");
            sb.Replace(".PSD", ".jpg");
            sb.Replace(".psd", ".jpg");
            sb.Replace(".ai",".jpg");
            sb.Replace(".eps", ".jpg");
            sb.Replace(".tif", ".jpg");
            sb.Replace(filename + "-web-images","img");

            return sb.ToString();

        }

        public string cleanName(string sb)
        {
            int ini, med;
            ini = 0;
            med = 0;
            string token;
            //System.Text.StringBuilder sb = new System.Text.StringBuilder(cadena);

            //if (sb.IndexOf("<span class=\"superindice-normal\">tm</span>") != -1 || sb.IndexOf("<span class=\"superindice-normal\">mr</span>") != -1)
            //{
            //    sb = sb.Replace("<span class=\"superindice-normal\">mr</span>", "");
            //    sb = sb.Replace("<span class=\"superindice-normal\">tm</span>", "");
            //    sb = sb.Replace("<span class=\"superindice-normal\">MR</span>", "");
            //    sb = sb.Replace("<span class=\"superindice-normal\">TM</span>", "");

            //}
            //if (sb.IndexOf("<span class=\"superindice-titulo\">tm</span>") != -1 || sb.IndexOf("<span class=\"superindice-titulo\">mr</span>") != -1)
            //{
            //    sb = sb.Replace("<span class=\"superindice-titulo\">mr</span>", "");
            //    sb = sb.Replace("<span class=\"superindice-titulo\">tm</span>", "");
            //    sb = sb.Replace("<span class=\"superindice-titulo\">MR</span>", "");
            //    sb = sb.Replace("<span class=\"superindice-titulo\">TM</span>", "");

            //}

            //if (sb.IndexOf("<span class=\"superindicetit\">tm</span>") != -1 || sb.IndexOf("<span class=\"superindicetit\">mr</span>") != -1)
            //{
            //    sb = sb.Replace("<span class=\"superindicetit\">mr</span>", "");
            //    sb = sb.Replace("<span class=\"superindicetit\">tm</span>", "");
            //    sb = sb.Replace("<span class=\"superindicetit\">MR</span>", "");
            //    sb = sb.Replace("<span class=\"superindicetit\">TM</span>", "");

            //}
            

            if (sb.IndexOf("<", 0) >= 0)
                for (int i = 0; i < sb.Length; i++)
                {
                    ini = sb.IndexOf("<", 0);
                    med = sb.IndexOf(">", 0);

                    token = sb.Substring(ini, ((med + 1) - ini));

                    sb = sb.Replace(token, "");

                    if (sb.IndexOf("<", 0) == -1)
                        i = sb.Length;

                }

            sb=sb.Replace("</span>", "");
            sb = sb.Replace("*", "");
            sb = sb.Replace("®", "");
            //sb = sb.Replace(".", "");
            sb = sb.Replace("'", "");
            sb = sb.Replace("  ", " ");
            sb = sb.Replace(":", "");
            sb = sb.Replace(";", "");
            sb = sb.Replace(",", "");
            sb = sb.Replace("´", "");
            sb = sb.Replace("/", "");
            sb = sb.Replace("á", "a");
            sb = sb.Replace("Á", "A");
            sb = sb.Replace(" ", "_");
            sb = sb.Replace("é", "e");
            sb = sb.Replace("É", "E");

            sb = sb.Replace("í", "i");
            sb = sb.Replace("Í", "I");

            sb = sb.Replace("ó", "o");
            sb = sb.Replace("Ó", "O");

            sb = sb.Replace("ú", "u");
            sb = sb.Replace("Ú", "U");

            sb = sb.Replace("ü", "u");

            sb = sb.Replace("&#9;", "");
            sb = sb.Replace("#9", "");
            //sb = sb.Replace("™", ""); 

            return sb;

        }

        public string cleanName2(string sb)
        {
            int ini, med;
            ini = 0;
            med = 0;
            string token;
            //System.Text.StringBuilder sb = new System.Text.StringBuilder(cadena);

            //if (sb.IndexOf("<span class=\"superindice-normal\">tm</span>") != -1 || sb.IndexOf("<span class=\"superindice-normal\">mr</span>") != -1)
            //{
            //    sb = sb.Replace("<span class=\"superindice-normal\">mr</span>", "");
            //    sb = sb.Replace("<span class=\"superindice-normal\">tm</span>", "");
            //    sb = sb.Replace("<span class=\"superindice-normal\">MR</span>", "");
            //    sb = sb.Replace("<span class=\"superindice-normal\">TM</span>", "");

            //}
            //if (sb.IndexOf("<span class=\"superindice-titulo\">tm</span>") != -1 || sb.IndexOf("<span class=\"superindice-titulo\">mr</span>") != -1)
            //{
            //    sb = sb.Replace("<span class=\"superindice-titulo\">mr</span>", "");
            //    sb = sb.Replace("<span class=\"superindice-titulo\">tm</span>", "");
            //    sb = sb.Replace("<span class=\"superindice-titulo\">MR</span>", "");
            //    sb = sb.Replace("<span class=\"superindice-titulo\">TM</span>", "");

            //}

            if (sb.IndexOf("<span class=\"superindice\">") != -1 )
            {
                sb = sb.Replace("<span class=\"superindice\">mr</span>", "");
                sb = sb.Replace("<span class=\"superindice\">tm</span>", "");
                sb = sb.Replace("<span class=\"superindice\">MR</span>", "");
                sb = sb.Replace("<span class=\"superindice\">TM</span>", "");

            }


            if (sb.IndexOf("<", 0) >= 0)
                for (int i = 0; i < sb.Length; i++)
                {
                    ini = sb.IndexOf("<", 0);
                    med = sb.IndexOf(">", 0);

                    token = sb.Substring(ini, ((med + 1) - ini));

                    sb = sb.Replace(token, "");

                    if (sb.IndexOf("<", 0) == -1)
                        i = sb.Length;

                }

            sb = sb.Replace("</span>", "");
            sb = sb.Replace("* ", "");
            sb = sb.Replace("®", "");
            //sb = sb.Replace(".", "");
            sb = sb.Replace("'", "");
            sb = sb.Replace("  ", " ");
            sb = sb.Replace(":", "");
            sb = sb.Replace(";", "");
            sb = sb.Replace(",", "");
            sb = sb.Replace("´", "");
            sb = sb.Replace("/", "");
            sb = sb.Replace("á", "a");
            sb = sb.Replace("Á", "A");

            sb = sb.Replace("é", "e");
            sb = sb.Replace("É", "E");

            sb = sb.Replace("í", "i");
            sb = sb.Replace("Í", "I");

            sb = sb.Replace("ó", "o");
            sb = sb.Replace("Ó", "O");

            sb = sb.Replace("ú", "u");
            sb = sb.Replace("Ú", "U");

            sb = sb.Replace("ü", "u");
            sb = sb.Replace("&#174", "");

            return sb;

        }

        //public int getProIDDEAQ(string producto,string lab)
        //{
        //    StringBuilder sb = new StringBuilder();

        //    //sb.Append("SELECT P.IDProducto ");
        //    //sb.Append("FROM Productos P ");
        //    //sb.Append("INNER JOIN RelacionFormasAplicacion R ");
        //    //sb.Append("ON (P.IDProducto = R.IDProducto) ");
        //    //sb.Append("INNER JOIN FormasAplicacion F ");
        //    //sb.Append("ON (R.IDFormasAplicacion = F.IDFormaAplicacion) ");
        //    //sb.Append("WHERE P.ACTIVO <> 0 AND P.NOMBRE LIKE '" + producto.Trim() + "%' ");
        //    ////sb.Append("AND FORMAAPLICACION LIKE '" + forma.Trim() + "%' ");
        //    //sb.Append("ORDER BY 1");

        //    sb.Append("SELECT IDProd ");
        //    sb.Append("\nFROM Producto ");
        //    sb.Append("\nWHERE LABORATORIO LIKE '%" + lab + "%' AND ");
        //    sb.Append("\nDEDICION = 1 AND NOMBRE LIKE '%" + producto + "%'");
            
        //    DataSet ds = GetHTML.DEAQDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());

        //    if (ds.Tables[0].Rows.Count > 0)
        //        return Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());
        //    else
        //        return 0;
        //}
        
        public string findUrl(string content,string file)
        {
            StringBuilder sb = new StringBuilder(content);
            
            int ini,fin;

            string cad = ""; 

            //int inicio = sb.ToString().IndexOf("<body>", 0);
            int inicio = sb.ToString().IndexOf("<", 0);

            //int inicio = 0;

            for (int x = inicio; x < sb.Length; x++)

                if (sb.ToString().IndexOf("www.", inicio) > 0)
                {
                    ini = sb.ToString().IndexOf("www.", inicio);
                    fin = sb.ToString().IndexOf("<", ini);

                    cad = sb.ToString().Substring(ini, (fin - ini));

                    sb.Replace(cad, "<a href='http://" + cad + "' target='_blank'>" + cad + "</a>");

                    fin = sb.ToString().IndexOf("</a>", fin);

                    x = fin;

                    inicio = fin;
                }
                else
                    break;


            return sb.ToString();

        }

        public string findEmail(string content, string file)
        {
            StringBuilder sb = new StringBuilder(content);

            int ini,med, fin;

            ini = fin = 0;

            string cad = "";

            for (int i = 0; i < sb.Length; i++)
                if (sb.ToString().IndexOf("@", i) > 0)
                {
                    med = sb.ToString().IndexOf("@", i);

                    for (int x = med; x > 0; x--)
                    {
                        if (char.IsWhiteSpace(sb.ToString()[x]) || sb.ToString()[x] == ':' || sb.ToString()[x] == '>')
                        {
                            ini = x + 1;
                            break;
                        }
                    }

                    for (int x = med; x <= sb.ToString().IndexOf("</p>", med); x++)
                    {
                        if (char.IsWhiteSpace(sb.ToString()[x]) || sb.ToString()[x] == '<' || sb.ToString()[x] == ',' || sb.ToString()[x] == ';')
                        {
                            fin = x;
                            break;
                        }
                    }

                    cad = sb.ToString().Substring(ini, (fin - ini));

                    sb.Replace(cad, "<a href= 'mailto:" + cad + "'>" + cad + "</a>");

                    fin = sb.ToString().IndexOf("</a>", fin);

                    i = fin;

                }
                else
                    break;

            return sb.ToString();

        }

        public string replaceString(string eini, string efin, string esini, string esfin, string content)
        {
            int ini, med, fin;

            ini = med = fin = 0;

            string cad;
            for (int x = 0; x < content.Length; x++)
            {
                if (content.IndexOf(eini, x) > 0)
                {
                    ini = content.IndexOf(eini, x);
                    med = content.IndexOf(">", ini);
                    fin = content.IndexOf(efin, med);

                    cad = content.Substring(med + 1, fin - (med + 1));

                    content = content.Replace(eini + cad + efin, esini + cad + esfin);

                    x = content.IndexOf(esfin, x);

                }
                else
                    x = content.Length;
            }

            return content;

        }

        public string replaceString2(string eini, string efin, string content)
        {
            int ini, med, fin;

            ini = med = fin = 0;

            string cad, cad2;

            ini = content.IndexOf(eini, 0);
            med = content.IndexOf(efin);
            fin = content.IndexOf(">",med);

            cad = content.Substring(ini, fin - ini);

            cad2 = cad.Replace("<span class=\"cursiva\">", "");

            cad2 = cad2.Replace("</span>", "");

            cad2 = cad2.Replace("<img", "&nbsp;&nbsp;&nbsp;&nbsp;<img");

            content = content.Replace(cad, cad2);

            return content;
        }

        public string replaceString3(string eini, string efin, string content)
        {
            StringBuilder sb = new StringBuilder(content);

            int ini, fin;

            ini = fin = 0;

            ini = sb.ToString().IndexOf(eini, 0);
            fin = sb.ToString().IndexOf(efin, ini);

            string cad = sb.ToString().Substring(ini, fin - ini);
            string cad2 = sb.ToString().Substring(ini, fin - ini);

            for (int i = 0; i < cad.Length; i++)
            {
                int ini2, fin2;

                ini2 = fin2 = 0;

                if (cad.IndexOf("_fmt", i) > 0)
                {
                    ini2 = cad.IndexOf("_fmt", i);
                    fin2 = cad.IndexOf(".", ini2);
                    string cad3 = cad.Substring(ini2, fin2 - ini2);

                    cad = cad.Replace(cad3, "");

                    i = fin2;
                }
                else
                    i = cad.Length;
            }

            sb.Replace(cad2, cad);

            return sb.ToString();

        }

        //public string replaceIMGPath(string token,string content)
        //{
        //    string cad,cad2,cad3,cad4;
        //    int ini, fin,next;
        //    ini = fin = 0;

        //    cad3 = "";

        //    for (int x = 0; x < content.ToString().Length; x++)
        //    {
        //        if (content.IndexOf(token, fin) > 0)
        //        {
        //            ini = content.IndexOf(token, fin);
        //            fin = content.IndexOf("</p>", ini);

        //            cad = content.Substring(ini, (fin - ini));
        //            cad4 = content.Substring(ini, (fin - ini));
        //            int ini2, fin2;
        //            ini2 = fin2 = 0;
        //            for (int z = 0; z < cad.Length; z++)
        //            {
        //                if (cad.IndexOf("<img") > 0)
        //                {
        //                    cad2 = cad.Substring(cad.IndexOf("src=\""), (cad.IndexOf("\"", cad.IndexOf("src=\"") + 5) - cad.IndexOf("src=\"")));
        //                    ini2 = cad2.Length - 1;

        //                    for (int y = ini2; y > 0; y--)
        //                    {
        //                        if (cad2[y].Equals('"') || cad2[y].Equals('/'))
        //                        {
        //                            cad3 = cad2.Substring(y, (ini2 - (y - 1)));
        //                            y = 0;
        //                        }
        //                    }
        //                    cad3 = cad3.Replace("\"","");

        //                    cad = cad.Replace(cad2, "src=\"logos/" + cad3 + "\"");

        //                    z = cad.Length;
        //                }
        //            }

        //            content = content.Replace(cad4, cad);

        //            fin = content.IndexOf("</p>", ini);

        //            x = fin;

        //        }
        //        else
        //            x = content.Length;
        //    }

        //    return content;
        //}

        //public string setDotted(string eini, string content,params string[] efin)
        //{
        //    int ini, med, fin;

        //    ini = med = fin = 0;

        //    string cad;

        //    for (int x = 0; x < content.Length; x++)
        //    {
        //        if (content.IndexOf(eini, x) > 0)
        //        {
        //            ini = content.IndexOf(eini, x);
        //            med = content.IndexOf(">", ini);
        //            fin = content.IndexOf("</p>", med);

        //            cad = content.Substring(med + 1, fin - (med + 1));

        //            if (cad.IndexOf(efin[0]) > 0)
        //            {
        //                if (cad.IndexOf("Veh&iacute;culo", 0) < 0 && cad.IndexOf("Excipiente", 0) < 0 && cad.IndexOf("FORMULA", 0) < 0)
        //                {
        //                    StringBuilder cad2 = new StringBuilder();
        //                    int cini, cfin;
        //                    cini = cfin = 0;
        //                    for (int z = 0; z < cad.Length; z++)
        //                    {
        //                        cfin = cad.IndexOf(efin[0], cini);

        //                        if (cfin > 0)
        //                        {
        //                            string cad3 = cad.Substring(cini, cfin - cini);

        //                            cad2.Append("<div class=\"dots\"style=\"width:40%\"><div class=\"left\">" + cad3.Substring(0, cad3.LastIndexOf(" ") - 1 <= 0 ? cad3.Length - 1 : cad3.LastIndexOf(" ") - 1));
        //                            cad2.Append(" </div><div class=\"right\">" + cad3.Substring(cad3.LastIndexOf(" ") <= 0 ? cad3.Length - 1 : cad3.LastIndexOf(" ")) + "</div></div><br />");

        //                            //cini = cad.IndexOf(efin[0], cfin + efin[0].Length);
        //                            cini = cfin + efin[0].Length;

        //                            z = cini == -1 ? cad.Length : cini - 1;
        //                        }
        //                        else
        //                            z = cad.Length;
        //                    }
        //                    for (int i = cad.LastIndexOf(" ") - 1; i > 0; --i)
        //                        if (char.IsWhiteSpace(cad[i]) && i > (cad.LastIndexOf(efin[0]) + efin[0].Length))
        //                        {
        //                            cad2.Append("<div class=\"dots\"style=\"width:40%\"><div class=\"left\">" + cad.Substring(cad.LastIndexOf(efin[0]) + efin[0].Length, (i - (cad.LastIndexOf(efin[0]) + efin[0].Length))));
        //                            cad2.Append(" </div><div class=\"right\">" + cad.Substring(i) + "</div></div>");

        //                            i = 0;

        //                        }
                                                                   
        //                    content = content.Replace(cad, cad2.ToString());

        //                    fin = content.LastIndexOf("</div></div>",fin);
        //                }

        //                x = fin;
        //            }
        //            else
        //            {
        //                if (cad.IndexOf("Veh&iacute;culo", 0) < 0 && cad.IndexOf("Excipiente", 0) < 0 && cad.IndexOf("FORMULA", 0) < 0)
        //                {
        //                    bool band = false;
        //                    for (int i = cad.LastIndexOf(" ") - 1; i > 0; --i)
        //                        if (char.IsWhiteSpace(cad[i]))
        //                        {
        //                            StringBuilder cad2 = new StringBuilder(cad.Substring(0, i));

        //                            cad2.Append(" </div><div class=\"right\">" + cad.Substring(i));

        //                            content = content.Replace(cad, "<div class=\"dots\"style=\"width:40%\"><div class=\"left\">" + cad2.ToString() + "</div></div>");
        //                            i = 0;
        //                            fin = content.IndexOf("</div></div>", fin);
        //                        }
        //                    if (band == false && cad.IndexOf(" ") > 0)
        //                    {
        //                        StringBuilder cad2 = new StringBuilder(cad.Substring(0, cad.IndexOf(" ")));

        //                        cad2.Append(" </div><div class=\"right\">" + cad.Substring(cad.IndexOf(" ")));
        //                        band = true;
        //                        content = content.Replace(cad, "<div class=\"dots\"style=\"width:40%\"><div class=\"left\">" + cad2.ToString() + "</div></div>");

        //                    }
        //                    x = fin;
        //                }
        //                else
        //                    x = fin;
        //            }
        //        }
        //        else
        //            x = content.Length;

        //    }

        //    return content;
        //}

        //public void getContent()
        //{
        //    /*StringBuilder sb2 = new StringBuilder();

        //    sb2.Append("SELECT PRODUCTID,PHARMAFORMID,HTMLCONTENT,XMLCONTENT,EDITIONID,VERSIONID  ");
        //    sb2.Append("FROM EDITIONPRODUCTCONTENTS ");
        //    //sb2.Append("where htmlcontent like '%.TIF%'OR XMLCONTENT LIKE '%.TIF%' ");
        //    sb2.Append("where editionid = 1 and versionid=1 and pharmaformid=210 and productid = 9040");
            
        //    DataSet ds = GetHTML.HTMLDatabase.ExecuteDataSet(CommandType.Text, sb2.ToString());

        //    //string htmlcontent = "";
        //    string xmlcontent = "";
        //    //int prodid, pharmaid, editid, verid;

        //    //prodid= pharmaid= editid= verid = 0;

        //    //for (int x = 0; x < ds.Tables[0].Rows.Count; x++)
        //    //{
        //    //    prodid = Convert.ToInt32(ds.Tables[0].Rows[x]["PRODUCTID"].ToString());
        //    //    pharmaid = Convert.ToInt32(ds.Tables[0].Rows[x]["PHARMAFORMID"].ToString());
        //    //    editid = Convert.ToInt32(ds.Tables[0].Rows[x]["EDITIONID"].ToString());
        //    //    verid = Convert.ToInt32(ds.Tables[0].Rows[x]["VERSIONID"].ToString());
        //    //    htmlcontent = ds.Tables[0].Rows[x]["HTMLCONTENT"].ToString();
        //        xmlcontent = ds.Tables[0].Rows[0]["XMLCONTENT"].ToString();

        //    //    htmlcontent = htmlcontent.Replace(".EPS", ".jpg");
        //    //    htmlcontent = htmlcontent.Replace(".TIF", ".jpg");
        //    //    htmlcontent = htmlcontent.Replace(".eps", ".jpg");
        //    //    htmlcontent = htmlcontent.Replace(".tif", ".jpg");

        //    //    //htmlcontent = htmlcontent.Replace("A1-web-images", "img");
        //    //    //htmlcontent = htmlcontent.Replace("F-web-images", "img");
        //    //    //htmlcontent = htmlcontent.Replace("H-web-images", "img");
        //    //    //htmlcontent = htmlcontent.Replace("E-web-images", "img");
        //    //    //htmlcontent = htmlcontent.Replace("O-web-images", "img");
        //    //    //htmlcontent = htmlcontent.Replace("XY-web-images", "img");


        //    //    xmlcontent = xmlcontent.Replace(".EPS", ".jpg");
        //    //    xmlcontent = xmlcontent.Replace(".TIF", ".jpg");
        //    //    xmlcontent = xmlcontent.Replace(".eps", ".jpg");
        //    //    xmlcontent = xmlcontent.Replace(".tif", ".jpg");

        //    //    //xmlcontent = xmlcontent.Replace("A1-web-images", "img");
        //    //    //xmlcontent = xmlcontent.Replace("F-web-images", ".img");
        //    //    //xmlcontent = xmlcontent.Replace("E-web-images", "img");
        //    //    //xmlcontent = xmlcontent.Replace("O-web-images", "img");
        //    //    //xmlcontent = xmlcontent.Replace("XY-web-images", "img");
        //    //    //xmlcontent = xmlcontent.Replace("H-web-images", "img");

        //    //    StringBuilder sb3 = new StringBuilder();

        //    //    sb3.Append("UPDATE EDITIONPRODUCTCONTENTS  ");
        //    //    sb3.Append("SET HTMLCONTENT = '" + htmlcontent + "',");
        //    //    sb3.Append(" XMLCONTENT = '" + xmlcontent + "' ");
        //    //    sb3.Append("WHERE PRODUCTID = " + prodid + " AND PHARMAFORMID = " + pharmaid + " AND ");
        //    //    sb3.Append("EDITIONID = " + editid + " AND VERSIONID = " + verid);

        //    //    GetHTML.HTMLDatabase.ExecuteDataSet(CommandType.Text, sb3.ToString());

        //    //    prodid = pharmaid = editid = verid = 0;

        //    //    htmlcontent = "";
        //    //    xmlcontent = "";

        //    //}

        //   saveHtmlFile("C:/","prodnorispez.txt", xmlcontent);*/


        //}

        //public void insertCode(int counter)
        //{
        //    StringBuilder sb = new StringBuilder();

        //    sb.Append("INSERT INTO [dbo].[Codes]([CodeString],[Prefix],[CurrentNumber],[Assign]) ");
        //    sb.Append("VALUES('" + cryp.addHash("9327483" + counter.ToString()) + "','9327483'," + counter + ",0)");

        //    GetHTML.WebSiteDatabase.ExecuteNonQuery(CommandType.Text, sb.ToString());
            
        //}

        //public DataTable getHTML(string lab)
        //{
        //    StringBuilder sb = new StringBuilder();

        //    //sb.Append("Select Distinct nArchivo From tmpHTMLS Order by 1");
        //    sb.Append("\nselect NOMBRE, COALESCE(COALESCE(p.NARCHIVO,NULL,t.NARCHIVO),null,t2.Archivo) AS ARCHIVO ");
        //    sb.Append("\nfrom producto h ");
        //    sb.Append("\nleft join htmls p on(h.idprod=p.idprod) ");
        //    sb.Append("\nleft join tmphtmls t on(h.idprod=t.idprod) ");
        //    sb.Append("\nleft join tmphtmls2 t2 on(h.idprod = t2.idprod) ");
        //    sb.Append("\nwhere laboratorio like '%" + lab + "%' and dedicion = 1");
        //    sb.Append("\norder by 2");

        //    return GetHTML.DEAQDatabase.ExecuteDataSet(CommandType.Text, sb.ToString()).Tables[0];
            
        //}

        //public string getAddress(string lab)
        //{
        //    string file = getHtmlContent("C:/CAD 40/Terceras/" + lab + ".htm");

        //    int ini, med, fin;

        //    ini = file.IndexOf("<div");
        //    med = file.IndexOf(">", ini);
        //    fin = file.IndexOf("<hr />", med);

        //    return file.Substring(med + 1,fin - (med + 1));

        //}

        //public string getProds(string division)
        //{
        //    string producto = "";

        //    StringBuilder sb = new StringBuilder();

        //    sb.Append("SELECT DISTINCT P.PRODUCTID,UPPER(P.BRAND) AS BRAND,dbo.plm_dfGetCountriesByProduct (P.PRODUCTID,PF.PHARMAFORMID) as Countries, ");
        //    sb.Append("\nUPPER(SUBSTRING(PD.[PRODUCTDESCRIPTION],1,1)) + LOWER(SUBSTRING(PD.[PRODUCTDESCRIPTION],2,LEN(PD.[PRODUCTDESCRIPTION])))AS [PRODUCTDESCRIPTION], ");
        //    sb.Append("\ndbo.plm_dfGetPharmaFormsByDiffPagedProductCAD (P.ProductId,PPV.VERSIONID,dbo.plm_dfGetCountriesByProduct (P.PRODUCTID,PF.PHARMAFORMID),PPV.PAGE) AS PHARMAFORMS, ");
        //    sb.Append("\ndbo.plm_dfGetPharmaFormsByProduct(MP.ProductId) AS PHARMAFORMS2, ");
        //    sb.Append("\nUPPER(D.SHORTNAME) AS LABORATORYNAME,PPV.PAGE,MP.PRODUCTID AS MENTIONATED, ");
        //    sb.Append("\n(SELECT TOP 1 CONVERT(VARCHAR,PHARMAFORMID) AS PHARMAFORMID FROM PRODUCTVERSIONPAGES WHERE PRODUCTID = P.PRODUCTID AND PAGE = PPV.PAGE) as Pharma ");
        //    sb.Append("\nFROM PRODUCTS P  ");
        //    sb.Append("\nINNER JOIN PRODUCTPHARMAFORMS PF ON(P.PRODUCTID = PF.PRODUCTID) ");
        //    sb.Append("\nLEFT JOIN (SELECT DISTINCT PRODUCTID,PHARMAFORMID FROM PARTICIPANTPRODUCTS WHERE EDITIONID = 5) PP ON(PF.PRODUCTID = PP.PRODUCTID AND PF.PHARMAFORMID = PP.PHARMAFORMID) ");
        //    sb.Append("\nLEFT JOIN (SELECT DISTINCT PRODUCTID,PHARMAFORMID FROM MENTIONATEDPRODUCTS WHERE EDITIONID = 5) MP ON(PF.PRODUCTID = MP.PRODUCTID AND PF.PHARMAFORMID = MP.PHARMAFORMID) ");
        //    sb.Append("\nLEFT JOIN PRODUCTDESCRIPTIONS PD ON(PF.PRODUCTID = PD.PRODUCTID AND PF.PHARMAFORMID = PD.PHARMAFORMID) ");
        //    sb.Append("\nLEFT JOIN PRODUCTVERSIONPAGES PPV ON(PF.PRODUCTID = PPV.PRODUCTID AND PF.PHARMAFORMID = PPV.PHARMAFORMID) ");
        //    sb.Append("\nLEFT JOIN TMPHTMLSBREVES BP ON(PF.PRODUCTID = BP.PRODUCTID AND PF.PHARMAFORMID = BP.FORMAID) ");
        //    sb.Append("\nINNER JOIN DIVISIONPRODUCTS DP ON(P.PRODUCTID = DP.PRODUCTID) ");
        //    sb.Append("\nINNER JOIN DIVISIONS D ON(DP.DIVISIONID = D.DIVISIONID) ");
        //    sb.Append("\nWHERE P.COUNTRYID = 3 AND D.COUNTRYID = 3 AND P.ACTIVE = 1 AND PF.ACTIVE = 1 AND BP.PRODUCTID IS NULL AND D.DIVISIONID = " + division);//D.SHORTNAME COLLATE SQL_Latin1_General_CP1_CI_AI LIKE ('" + division + "') ");
        //    sb.Append("\nUNION ");
        //    sb.Append("SELECT DISTINCT P.PRODUCTID,UPPER(P.BRAND) AS BRAND,dbo.plm_dfGetCountriesByProduct (P.PRODUCTID,PF.PHARMAFORMID) as Countries, ");
        //    sb.Append("\nUPPER(SUBSTRING(PD.[PRODUCTDESCRIPTION],1,1)) + LOWER(SUBSTRING(PD.[PRODUCTDESCRIPTION],2,LEN(PD.[PRODUCTDESCRIPTION])))AS [PRODUCTDESCRIPTION], ");
        //    sb.Append("\ndbo.plm_dfGetPharmaFormsByPagedProductBP (P.ProductId,BP.HTML,dbo.plm_dfGetCountriesByProduct (P.PRODUCTID,PF.PHARMAFORMID)) AS PHARMAFORMS, ");
        //    sb.Append("\ndbo.plm_dfGetPharmaFormsByProduct(MP.ProductId) AS PHARMAFORMS2, ");
        //    sb.Append("\nUPPER(D.SHORTNAME) AS LABORATORYNAME,PPV.PAGE,MP.PRODUCTID AS MENTIONATED, ");
        //    sb.Append("\nBP.HTML as Pharma ");
        //    sb.Append("\nFROM PRODUCTS P  ");
        //    sb.Append("\nINNER JOIN PRODUCTPHARMAFORMS PF ON(P.PRODUCTID = PF.PRODUCTID) ");
        //    sb.Append("\nLEFT JOIN (SELECT DISTINCT PRODUCTID,PHARMAFORMID FROM PARTICIPANTPRODUCTS WHERE EDITIONID = 5) PP ON(PF.PRODUCTID = PP.PRODUCTID AND PF.PHARMAFORMID = PP.PHARMAFORMID) ");
        //    sb.Append("\nLEFT JOIN (SELECT DISTINCT PRODUCTID,PHARMAFORMID FROM MENTIONATEDPRODUCTS WHERE EDITIONID = 5) MP ON(PF.PRODUCTID = MP.PRODUCTID AND PF.PHARMAFORMID = MP.PHARMAFORMID) ");
        //    sb.Append("\nLEFT JOIN PRODUCTDESCRIPTIONS PD ON(PF.PRODUCTID = PD.PRODUCTID AND PF.PHARMAFORMID = PD.PHARMAFORMID) ");
        //    sb.Append("\nLEFT JOIN PRODUCTVERSIONPAGES PPV ON(PF.PRODUCTID = PPV.PRODUCTID AND PF.PHARMAFORMID = PPV.PHARMAFORMID) ");
        //    sb.Append("\nLEFT JOIN TMPHTMLSBREVES BP ON(PF.PRODUCTID = BP.PRODUCTID AND PF.PHARMAFORMID = BP.FORMAID) ");
        //    sb.Append("\nINNER JOIN DIVISIONPRODUCTS DP ON(P.PRODUCTID = DP.PRODUCTID) ");
        //    sb.Append("\nINNER JOIN DIVISIONS D ON(DP.DIVISIONID = D.DIVISIONID) ");
        //    sb.Append("\nWHERE P.COUNTRYID = 3 AND D.COUNTRYID = 3 AND P.ACTIVE = 1 AND PF.ACTIVE = 1 AND BP.HTML IS NOT NULL AND BP.PRODUCTID IS NOT NULL AND D.DIVISIONID = " + division);//D.SHORTNAME COLLATE SQL_Latin1_General_CP1_CI_AI LIKE ('" + division + "') ");
        //    sb.Append("\nORDER BY 2");


        //    DataTable dt = GetHTML.HTMLDatabase.ExecuteDataSet(CommandType.Text, sb.ToString()).Tables[0];

        //    if (dt.Rows.Count == 0)
        //        return "";
        //    else
        //    {
        //        for (int x = 0; x < dt.Rows.Count; x++)
        //        {
        //            producto += "<tr>" + Environment.NewLine;

        //            if (dt.Rows[x]["Pharma"].ToString().Contains(".htm"))
        //            {

        //                producto += "<td>" + Environment.NewLine + "<p class='Producto'><b><a href='../prods/" + dt.Rows[x]["Pharma"].ToString() + "' >&#9633; " + dt.Rows[x]["Brand"].ToString() + " <sub>" + dt.Rows[x]["Countries"].ToString() + "</sub></a></b></p>" + Environment.NewLine + "</td>" + Environment.NewLine;

        //                producto += "<td>" + Environment.NewLine + "<p class='Producto'><i>" ;

        //                producto +=    dt.Rows[x]["PRODUCTDESCRIPTION"].ToString() == string.Empty ? "&nbsp;" : dt.Rows[x]["PRODUCTDESCRIPTION"].ToString() +
        //                    "</i></p>" + Environment.NewLine + "</td>" + Environment.NewLine;

        //                producto += "<td>" + Environment.NewLine + "<p class='Producto'>";

        //                producto += dt.Rows[x]["PHARMAFORMS"].ToString() == string.Empty ? dt.Rows[x]["PHARMAFORMS2"].ToString() == string.Empty ? "&nbps;" : dt.Rows[x]["PHARMAFORMS2"].ToString() : dt.Rows[x]["PHARMAFORMS"].ToString() +
        //                    "</p>" + Environment.NewLine + "</td>" + Environment.NewLine;
                                                

        //            }
        //            else
        //            {
        //                if(dt.Rows[x]["Page"] != System.DBNull.Value)
        //                {
        //                    producto += "<td>" + Environment.NewLine + "<p class='Producto'><b><a href='../prods/" + dt.Rows[x]["ProductId"].ToString() + "_" + dt.Rows[x]["Pharma"].ToString() + ".htm' >* " + dt.Rows[x]["Brand"].ToString() + " <sub>" + dt.Rows[x]["Countries"].ToString() +"</sub></a></b></p>" + Environment.NewLine + "</td>" + Environment.NewLine;

        //                    producto += "<td>" + Environment.NewLine + "<p class='Producto'><i>";
                            
        //                    producto +=  dt.Rows[x]["PRODUCTDESCRIPTION"].ToString() == string.Empty ? "&nbsp;" : dt.Rows[x]["PRODUCTDESCRIPTION"].ToString() +
        //                        "</i></p>" + Environment.NewLine + "</td>" + Environment.NewLine;

        //                    producto += "<td>" + Environment.NewLine + "<p class='Producto'>";

        //                    producto += dt.Rows[x]["PHARMAFORMS"].ToString() == string.Empty ? dt.Rows[x]["PHARMAFORMS2"].ToString() == string.Empty ? "&nbps;" : dt.Rows[x]["PHARMAFORMS2"].ToString() : dt.Rows[x]["PHARMAFORMS"].ToString() +
        //                    "</p>" + Environment.NewLine + "</td>" + Environment.NewLine;
        //                }
        //                else
        //                {
        //                    producto += "<td>" + Environment.NewLine + "<p class='Producto'><b>" + dt.Rows[x]["Brand"].ToString() + " <sub>" + dt.Rows[x]["Countries"].ToString() + "</sub></b>.</p>" + Environment.NewLine + "</td>" + Environment.NewLine;

        //                    producto += "<td>" + Environment.NewLine + "<p class='Producto'><i>";

        //                    producto +=   dt.Rows[x]["PRODUCTDESCRIPTION"].ToString() == string.Empty ? "&nbsp;" : dt.Rows[x]["PRODUCTDESCRIPTION"].ToString() +
        //                        "</i></p>" + Environment.NewLine + "</td>" + Environment.NewLine;

        //                    producto += "<td>" + Environment.NewLine + "<p class='Producto'>";

        //                    producto += dt.Rows[x]["PHARMAFORMS"].ToString() == string.Empty ? dt.Rows[x]["PHARMAFORMS2"].ToString() == string.Empty ? "&nbps;" : dt.Rows[x]["PHARMAFORMS2"].ToString() : dt.Rows[x]["PHARMAFORMS"].ToString() +
        //                    "</p>" + Environment.NewLine + "</td>" + Environment.NewLine;
        //                }
        //            }

        //            producto += "</tr>" + Environment.NewLine;
        //        }

        //        this.Laboratory = dt.Rows[0]["LABORATORYNAME"].ToString();

        //        return producto;
        //    }

        //}

        //public string getProdsAgro(string lab)
        //{
        //    lab = lab.Replace("_", " ");

        //    string producto = "";

        //    string desc;

        //    StringBuilder sb = new StringBuilder();

        //    sb.Append("\nselect h.Nombre,Coalesce(f.FFarmaceutica,'') as FFarmaceutica,COALESCE(COALESCE(p.NARCHIVO,NULL,t.NARCHIVO),null,t2.Archivo) AS ARCHIVO, h.Laboratorio ");
        //    sb.Append("\nfrom producto h ");
        //    sb.Append("\nleft join RelFFarmaceutica r on(h.idprod = r.IdProducto) ");
        //    sb.Append("\nleft join FFarmaceutica f on(r.IDFFarmaceutica = f.IDFF) ");
        //    sb.Append("\ninner join tmpprods tp on(h.idprod = tp.idprod)");
        //    sb.Append("\nleft join htmls p on(h.idprod=p.idprod) ");
        //    sb.Append("\nleft join tmphtmls t on(h.idprod=t.idprod) ");
        //    sb.Append("\nleft join tmphtmls2 t2 on(h.idprod = t2.idprod) ");
        //    sb.Append("\nwhere [Orgánico] is null and h.laboratorio like '%" + lab + "%' ");
        //    sb.Append("\norder by 1");

        //    DataTable dt = GetHTML.DEAQDatabase.ExecuteDataSet(CommandType.Text, sb.ToString()).Tables[0];

        //    if (dt.Rows.Count == 0)
        //        return "";
        //    else
        //    {

        //        for (int x = 0; x < dt.Rows.Count; x++)
        //        {

        //            if (dt.Rows[x]["ARCHIVO"] == System.DBNull.Value)
        //            {
        //                producto = producto + "<p class='NoLinks'><b>" + dt.Rows[x]["Nombre"].ToString() + "</b>.";

        //                producto = producto + "<i>" + dt.Rows[x]["FFarmaceutica"].ToString() + "</i>.</p>" + Environment.NewLine;

        //            }
        //            else
        //            {
        //                producto = producto + "<p class='Links'><a href='../prods/" + dt.Rows[x]["ARCHIVO"].ToString() +
        //                    "' ><b>" + dt.Rows[x]["Nombre"].ToString() + "</b>. ";

        //                desc = getDescription(dt.Rows[x]["ARCHIVO"].ToString());

        //                producto = producto + desc + ". <i>" + dt.Rows[x]["FFarmaceutica"].ToString() + "</i>.</a></p>" + Environment.NewLine;

        //            }
        //        }

        //        this.Laboratory = dt.Rows[0]["Laboratorio"].ToString();

        //        return producto;
        //    }
        //}

        //public string getProdsOrg(string lab)
        //{
        //    lab = lab.Replace("_", " ");

        //    string producto = "<br />\n<p class=\"Subtitulo\">Orgánicos:</p><br />\n";

        //    string desc;

        //    StringBuilder sb = new StringBuilder();

        //    sb.Append("\nselect h.Nombre,Coalesce(f.FFarmaceutica,'') as FFarmaceutica,COALESCE(COALESCE(p.NARCHIVO,NULL,t.NARCHIVO),null,t2.Archivo) AS ARCHIVO, h.Laboratorio ");
        //    sb.Append("\nfrom producto h ");
        //    sb.Append("\nleft join RelFFarmaceutica r on(h.idprod = r.IdProducto) ");
        //    sb.Append("\nleft join FFarmaceutica f on(r.IDFFarmaceutica = f.IDFF) ");
        //    sb.Append("\ninner join tmpprods tp on(h.idprod = tp.idprod)");
        //    sb.Append("\nleft join htmls p on(h.idprod=p.idprod) ");
        //    sb.Append("\nleft join tmphtmls t on(h.idprod=t.idprod) ");
        //    sb.Append("\nleft join tmphtmls2 t2 on(h.idprod = t2.idprod) ");
        //    sb.Append("\nwhere [Orgánico] is not null and h.laboratorio like '%" + lab + "%' ");
        //    sb.Append("\norder by 1");

        //    DataTable dt = GetHTML.DEAQDatabase.ExecuteDataSet(CommandType.Text, sb.ToString()).Tables[0];

        //    if (dt.Rows.Count == 0)
        //        return "";
        //    else
        //    {
        //        for (int x = 0; x < dt.Rows.Count; x++)
        //        {

        //            if (dt.Rows[x]["ARCHIVO"] == System.DBNull.Value)
        //            {
        //                producto = producto + "<p class='NoLinks'><b>" + dt.Rows[x]["Nombre"].ToString() + "</b>.";

        //                producto = producto + "<i>" + dt.Rows[x]["FFarmaceutica"].ToString() + "</i>.</p>" + Environment.NewLine;

        //            }
        //            else
        //            {
        //                producto = producto + "<p class='Links'><a href='../prods/" + dt.Rows[x]["ARCHIVO"].ToString() +
        //                    "' ><b>" + dt.Rows[x]["Nombre"].ToString() + "</b>. ";

        //                desc = getDescription(dt.Rows[x]["ARCHIVO"].ToString());

        //                producto = producto + desc + ". <i>" + dt.Rows[x]["FFarmaceutica"].ToString() + "</i>.</a></p>" + Environment.NewLine;

        //            }
        //        }

        //        this.Laboratory = dt.Rows[0]["Laboratorio"].ToString();

        //        return producto;
        //    }
        //}

        //public string getDescription(string file)
        //{
        //    string token;
        //    string desc = getHtmlContent("C:/DEAQ 2010/src/prods/" + file);

        //    int ini, med, fin;

        //    if (desc.IndexOf("<p class=\"base-de-prop-\">") != -1)
        //    {
        //        ini = desc.IndexOf("<p class=\"base-de-prop-\">");
        //        med = desc.IndexOf(">", ini);
        //        fin = desc.IndexOf("</p>", med);

        //        string desc2 = desc.Substring(med + 1, fin - (med + 1));

        //        if (desc2.IndexOf("<", 0) >= 0)
        //            for (int i = 0; i < desc2.Length; i++)
        //            {
        //                ini = desc2.IndexOf("<", 0);
        //                med = desc2.IndexOf(">", 0);

        //                token = desc2.Substring(ini, ((med + 1) - ini));

        //                desc2 = desc2.Replace(token, "");

        //                if (desc2.IndexOf("<", 0) == -1)
        //                    i = desc2.Length;

        //            }

        //        desc2 = desc2.Replace("</span>", "");

        //        return desc2;
        //    }
        //    else
        //        return "";
        //}

        //public string Laboratory
        //{
        //    get { return this._lab; }
        //    set { this._lab = value; }
        //}

        //public int ProductId
        //{
        //    get{return this._prodId;}
        //    set{this._prodId = value;}
        //}

        //public string checkProduct(string lab, string product)
        //{
        //    StringBuilder sb = new StringBuilder();

        //    sb.Append("SELECT DISTINCT PP.PRODUCTID,PP.PHARMAFORMID,ED.DIVISIONID ");
        //    sb.Append("\nFROM PARTICIPANTPRODUCTS PP ");
        //    sb.Append("\nINNER JOIN EDITIONDIVISIONPRODUCTS ED ");
        //    sb.Append("\nON(PP.PRODUCTID = ED.PRODUCTID AND PP.PHARMAFORMID = ED.PHARMAFORMID) ");
        //    sb.Append("\nINNER JOIN PRODUCTS P ON (PP.PRODUCTID = P.PRODUCTID) ");
        //    sb.Append("\nWHERE PP.EDITIONID = 1 AND ED.DIVISIONID = " + lab + "AND P.PRODUCTNAME = '" + product.Trim() + "' ");

        //    DataSet ds = GetHTML.PEV30Database.ExecuteDataSet(CommandType.Text, sb.ToString());

        //    this.Laboratory = lab;

        //    if (ds.Tables[0].Rows.Count == 1)
        //    {
        //        //this.Laboratory = ds.Tables[0].Rows[0]["DivisionId"].ToString();
        //        return "<p class=\"normal-1\"><a href=\"../productos/" + ds.Tables[0].Rows[0]["ProductId"].ToString() + "_" + ds.Tables[0].Rows[0]["PharmaFormId"].ToString() + ".htm \">";
        //    }
            

        //    return string.Empty;

        //}

        //public string getProductId(string file)
        //{
        //    StringBuilder sb = new StringBuilder();

        //    sb.Append("select t.idprod,p.nombre,t.narchivo,p.laboratorio ");
        //    sb.Append("\nfrom tmpHTMLSF t ");
        //    sb.Append("\nleft join producto p on(t.idprod = p.idprod) ");
        //    sb.Append("\nwhere t.narchivo like '%" + file + "%' ");

        //    DataSet ds = GetHTML.DEAQDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());

        //    if (ds.Tables[0].Rows.Count == 1)
        //    {
        //        int prodId = Convert.ToInt32(ds.Tables[0].Rows[0]["idprod"]);

        //        string cultivos = getCultivos(prodId);

        //        string ingredientes = getIngredientes(prodId);

        //        string usos = getUsos(prodId);

        //        return cultivos + ingredientes + usos;
        //    }


        //    return string.Empty;
        //}

        //public string getCultivos(int prodId)
        //{
        //    StringBuilder sb = new StringBuilder();

        //    sb.Append("select c.IDCul,c.Cultivo ");
        //    sb.Append("\nfrom Cultivos c ");
        //    sb.Append("\ninner join RelCultivo r on(c.IDCul = r.IDCultivo) ");
        //    sb.Append("\nwhere r.IDProducto = " + prodId);

        //    DataSet ds = GetHTML.DEAQDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());

        //    if (ds.Tables[0].Rows.Count > 0)
        //    {
        //        string link = "";

        //        for (int x = 0; x < ds.Tables[0].Rows.Count; x++)
        //        {
        //            link = link + "<a href='../cult/subcult/" + ds.Tables[0].Rows[x]["IDCul"].ToString() + ".htm' >" +
        //                ds.Tables[0].Rows[x]["Cultivo"].ToString().ToUpper() + "</a><br />" + Environment.NewLine;

        //        }

        //        return "<div style='text-align:left;'>" + Environment.NewLine + "<b>Cultivos: </b><br />" + Environment.NewLine +
        //            link + Environment.NewLine + "</div><br />";

        //    }
        //    return string.Empty;

        //}

        //public string getIngredientes(int prodId)
        //{
        //    StringBuilder sb = new StringBuilder();

        //    sb.Append("select c.IDIActivo,c.IActivo ");
        //    sb.Append("\nfrom IngreAct c ");
        //    sb.Append("\ninner join RelIngreAct r on(c.IDIActivo = r.IDIngreAct) ");
        //    sb.Append("\nwhere r.IDProducto = " + prodId);

        //    DataSet ds = GetHTML.DEAQDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());

        //    if (ds.Tables[0].Rows.Count > 0)
        //    {
        //        string link = "";

        //        for (int x = 0; x < ds.Tables[0].Rows.Count; x++)
        //        {
        //            link = link + "<a href='../ingact/subingact/" + ds.Tables[0].Rows[x]["IDIActivo"].ToString() + ".htm' >" +
        //                ds.Tables[0].Rows[x]["IActivo"].ToString().ToUpper() + "</a><br />" + Environment.NewLine;

        //        }

        //        return "<div style='text-align:left;'>" + Environment.NewLine + "<b>Ingredientes Activos: </b><br />" + Environment.NewLine +
        //            link + Environment.NewLine + "</div><br />";

        //    }
        //    return string.Empty;
        //}

        //public string getUsos(int prodId)
        //{
        //    StringBuilder sb = new StringBuilder();

        //    sb.Append("select c.IDUsos,c.Usos ");
        //    sb.Append("\nfrom Usos c ");
        //    sb.Append("\ninner join RelUsos r on(c.IDUsos = r.IDUso) ");
        //    sb.Append("\nwhere r.IDProducto = " + prodId);

        //    DataSet ds = GetHTML.DEAQDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());

        //    if (ds.Tables[0].Rows.Count > 0)
        //    {
        //        string link = "";

        //        for (int x = 0; x < ds.Tables[0].Rows.Count; x++)
        //        {
        //            link = link + "<a href='../usos/subusos/" + ds.Tables[0].Rows[x]["IDUsos"].ToString() + ".htm' >" +
        //                ds.Tables[0].Rows[x]["Usos"].ToString().ToUpper() + "</a>" + Environment.NewLine;

        //        }

        //        return "<div style='text-align:left;'>" + Environment.NewLine + "<b>Usos Agroqu&iacute;micos: </b><br />" + Environment.NewLine +
        //            link + Environment.NewLine + "</div><br />";

        //    }
        //    return string.Empty;
        //}

        #region Mentionated Functions

        public string cleanText(string cadena)
        {
            int ini, med;
            ini = 0;
            med = 0;
            string token;

            if (cadena.IndexOf("<", 0) >= 0)
                for (int i = 0; i < cadena.Length; i++)
                {
                    ini = cadena.IndexOf("<", 0);
                    med = cadena.IndexOf(">", 0);

                    token = cadena.Substring(ini, ((med + 1) - ini));

                    cadena = cadena.Replace(token, "");

                    if (cadena.IndexOf("<", 0) == -1)
                        i = cadena.Length;

                }

            cadena = cadena.Replace("</span>", "");
            cadena = cadena.Replace("</p>","");
            cadena = cadena.Replace("<br />","");

            return cadena;

        }

        public string getImage(string cadena)
        {
            int ini, med;
            ini = 0;
            med = 0;
            string token;

            string cad2, cad3;

            cad2 = cad3 = "";

            int ini2,fin2;
            ini2 = fin2 = 0;

            for (int z = 0; z < cadena.Length; z++)
            {
                if (cadena.IndexOf("<img", fin2 + 1) > 0)
                {
                    fin2 = cadena.IndexOf("<img", fin2 + 1);
                    cad2 = cadena.Substring(cadena.IndexOf("src=\"", fin2), (cadena.IndexOf("\"", cadena.IndexOf("src=\"", fin2) + 5) - cadena.IndexOf("src=\"", fin2)));
                    ini2 = cad2.Length - 1;

                    for (int y = ini2; y > 0; y--)
                    {
                        if (cad2[y].Equals('"') || cad2[y].Equals('/'))
                        {
                            cad2 = cad2.Substring(y + 1, (ini2 - y));
                            y = 0;
                            if (!string.IsNullOrEmpty(cad3))
                                cad3 = cad3 + "," + cad2;
                            else
                                cad3 = cad2;
                        }

                    }

                }
                else
                    z = cadena.Length;
            }

            this.Image = cad3;


            if (cadena.IndexOf("<", 0) >= 0)
                for (int i = 0; i < cadena.Length; i++)
                {
                    ini = cadena.IndexOf("<", 0);
                    med = cadena.IndexOf(">", 0);

                    token = cadena.Substring(ini, ((med + 1) - ini));

                    cadena = cadena.Replace(token, "");

                    if (cadena.IndexOf("<", 0) == -1)
                        i = cadena.Length;

                }
            cadena = cadena.Replace("</p>","");
            
            

            return cadena;

        }

        public string Image
        {
            get { return this._img; }
            set { this._img = value; }
        }


        #endregion

        //#region EncryptPass

        //public void getUsers()
        //{
        //    StringBuilder sb = new StringBuilder();

        //    sb.Append("\nSelect UserId,NickName,[Password] ");
        //    sb.Append("\nFrom Users  ");
        //    sb.Append("\nWhere NickPrefixId = 50 ");

        //    DataSet ds = GetHTML.PLMClientsDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());

        //    for (int x = 0; x < ds.Tables[0].Rows.Count; x++)
        //    {
        //        this.updateCode(ds.Tables[0].Rows[x]["UserId"].ToString(), ds.Tables[0].Rows[x]["Password"].ToString());

        //    }
            

        //}

        //public void updateCode(string userId, string pass)
        //{
        //    StringBuilder sb = new StringBuilder();

        //    sb.Append("\nUpdate Users ");
        //    sb.Append("\nSet Password = '" + cryp.encrypt(pass) + "' ");
        //    sb.Append("\nWhere UserId = " + userId);

        //    GetHTML.PLMClientsDatabase.ExecuteNonQuery(CommandType.Text, sb.ToString());

        //}
            

        //#endregion

       // #region DED

       // //public void updateContent(string path)
       // //{
       // //    StringBuilder sb = new StringBuilder();

       // //    sb.Append("select ProductId,PharmaFormId,CategoryId,DivisionId,html ");
       // //    sb.Append("\nfrom TMPPRODSPEVCOL Where html is not null");
           
       // //    DataSet ds = GetHTML.PEV30Database.ExecuteDataSet(CommandType.Text, sb.ToString());

       // //    if (ds.Tables[0].Rows.Count > 0)
       // //    {
       // //        for (int x = 0; x < ds.Tables[0].Rows.Count; x++)
       // //        {
       // //           string file = this.getHtmlContent(path + "/" + ds.Tables[0].Rows[x]["html"].ToString() + ".htm");

       // //            this.updatehtmlContent(ds.Tables[0].Rows[x]["ProductId"].ToString(),
       // //                ds.Tables[0].Rows[x]["PharmaFormId"].ToString(),
       // //                "4",
       // //                ds.Tables[0].Rows[x]["DivisionId"].ToString(),
       // //                ds.Tables[0].Rows[x]["CategoryId"].ToString(), file.Replace("'","\""));




       // //        }

       // //    }


       // //}


       // public void updateContent(string path)
       // {
       //     StringBuilder sb = new StringBuilder();

       //      sb.Append("select edProdId,editionId,sectionId,productId,htmlFile ");
       //      sb.Append("\nfrom EDITIONPRODUCTSECTIONS where htmlfile is not null and htmlcontent like '%No se encuentra el archivo%' and edProdId <> 2913");

       //     DataSet ds = GetHTML.PEV30Database.ExecuteDataSet(CommandType.Text, sb.ToString());

       //     if (ds.Tables[0].Rows.Count > 0)
       //     {
       //         for (int x = 0; x < ds.Tables[0].Rows.Count; x++)
       //         {
       //             string file;

       //             if (File.Exists(path + ds.Tables[0].Rows[x]["htmlFile"].ToString()))
       //                 file = this.getHtmlContent(path + ds.Tables[0].Rows[x]["htmlFile"].ToString());
       //             else
       //                 file = "No se encuentra el archivo";


       //             this.updatehtmlContent(ds.Tables[0].Rows[x]["edProdId"].ToString(),
       //                 "3",
       //                 ds.Tables[0].Rows[x]["sectionId"].ToString(),
       //                 ds.Tables[0].Rows[x]["productId"].ToString(),
       //                 file.Replace("'","\""));
       //     }

       //    }


       //}






       // public void createProducts(string path)
       // {
       //     StringBuilder sb = new StringBuilder();

       //     StringBuilder sb2 = new StringBuilder();

       //     StringBuilder sb3 = new StringBuilder();

       //     sb.Append("select EditionId,DivisionId,CategoryId,ProductId,PharmaFormId,HtmlContent ");
       //     sb.Append("\nfrom ParticipantProducts Where EditionId = 4 and HtmlContent is not null");

       //     DataSet ds = GetHTML.PEV30Database.ExecuteDataSet(CommandType.Text, sb.ToString());

       //     sb2.Append(getHtmlContent(@"C:\PLM Development\Aplications\ExportHTML\IndesignTemplate.htm"));

       //     if (ds.Tables[0].Rows.Count > 0)
       //     {
       //         for (int x = 0; x < ds.Tables[0].Rows.Count; x++)
       //         {
       //             string file = ds.Tables[0].Rows[x]["HtmlContent"].ToString();

       //             this.saveHtmlFile(path, ds.Tables[0].Rows[x]["ProductId"].ToString() + "_"
       //                 + ds.Tables[0].Rows[x]["PharmaFormId"].ToString() + ".htm", file);


       //             //sb3.Append("<a href='"+ ds.Tables[0].Rows[x]["ProductId"].ToString() + "_"
       //             //    + ds.Tables[0].Rows[x]["PharmaFormId"].ToString() + ".htm' >" + ds.Tables[0].Rows[x]["ProductId"].ToString() + "_"
       //             //    + ds.Tables[0].Rows[x]["PharmaFormId"].ToString() + ".htm</a><br />");


       //         }

       //         //sb2.Replace("@@@Contenido@@@",sb3.ToString());

       //         //this.saveHtmlFile(path,"index.htm",sb2.ToString());

       //     }
       // }


       // #endregion

        private PLMCryptographyComponent.CryptographyComponent cryp = new PLMCryptographyComponent.CryptographyComponent();
        public static readonly GetHTML Instance = new GetHTML();

        //private string _lab;
        //private int _prodId;
        private string _img;
    }
}