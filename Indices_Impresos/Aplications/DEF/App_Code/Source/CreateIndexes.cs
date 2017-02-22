using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Descripción breve de CreateIndexes
/// </summary>
public class CreateIndexes : GenerateHTML
{
    #region Constructors

    public CreateIndexes(string sourcePath, string destinationPath) : base(sourcePath, destinationPath) { }

    #endregion

    public override void getHtmlFiles(int editionId)
    {
        throw new Exception("The method or operation is not implemented.");
    }

    #region Products

    public void generateProductatribute(int editionId)
    {
        try{
        DEFTableAdapters.ProductsbyEditionTableAdapter productContentAdp = new DEFTableAdapters.ProductsbyEditionTableAdapter();
        DEF.ProductsbyEditionDataTable productContentTable = productContentAdp.GetProductByEdtion(editionId);

        foreach (DataRow pcRow in productContentTable.Rows)
        {
            DEF.ProductContentsRow productContentRow = (DEF.ProductContentsRow)pcRow;

            string fileName = this.cleanFileName(productContentRow.Brand);// + "_" + this.cleanFileName(productContentRow.PharmaForm).Trim();

            StringBuilder sb = new StringBuilder(this.getHtmlTemplate());

            string brand = productContentRow.Brand;

            sb.Replace("@@titulo@@", brand);

            this.saveHtmlFile(fileName + ".htm", sb.ToString());
        }
        } catch(Exception ee){
            Console.WriteLine(ee.Message);
            Console.ReadLine();
        }         
    }

    public void generatepharmaforms(int editionId, string path)
    {

      try{  
        DEFTableAdapters.PharmaByEditionTableAdapter pharmacontent = new DEFTableAdapters.PharmaByEditionTableAdapter();
        DEF.PharmaByEditionDataTable pharmacontenttable = pharmacontent.GetPharmaFormDef(editionId);
        
        foreach (DataRow pcrow in pharmacontenttable.Rows)
        {
            DEF.PharmaByEditionRow pharmacontentrow = (DEF.PharmaByEditionRow)pcrow;
            StringBuilder sb = new StringBuilder(this.getHtmlTemplate(path + "/indexproducts.htm"));
            StringBuilder cadenauno = new StringBuilder(this.getHtmlTemplate(path + "/plantillatop.htm"));
            StringBuilder cadenados = new StringBuilder(this.getHtmlTemplate(path + "/plantillaButtom.htm"));

            int numberEdition = pharmacontentrow.NumberEdition;
            string IDCountry = pharmacontentrow.ID;
            string pharma = pharmacontentrow.pharmaform;
            string Brand = pharmacontentrow.Brand;
            string image = pharmacontentrow.productshot;
            string subs = pharmacontentrow.Description;
            string sublink = this.cleanFileName(pharmacontentrow.Description);
            string tera = pharmacontentrow.TherapeuticKey;
            int ProductId = pharmacontentrow.ProductId;
            string Descriptiontera = pharmacontentrow.Description1;
            int pharmaformId = pharmacontentrow.pharmaformId;
            int divisionId = pharmacontentrow.DivisionId;
            int categotyId = pharmacontentrow.CategoryId;
            string fileName = this.cleanFileName(pharmacontentrow.Brand) + "_" + this.cleanFileName(pharmacontentrow.pharmaform).Trim();
            
            bool attributeValidator = true;
            string stringcomscore = getComscore(numberEdition, "Medicamentos", IDCountry, Brand, pharma);
            string comscorecomplete = this.comscore.Replace("area1.area2",stringcomscore);
            string codeAnalytics = this.getGoogleAnalytics(IDCountry);

            sb.Replace("@@@google@@@", codeAnalytics);
            sb.Replace("@@nedition@@", numberEdition.ToString());
            sb.Replace("@@titulo@@", Brand);
            sb.Replace("@@formaFarma@@", pharma);
            sb.Replace("@@imagen@@", image);
            sb.Replace("@@ingrediente@@", "<a href=\"../sustancias/" + sublink + ".htm \" >" + subs + "</a>");
            sb.Replace("@@AA@@", Descriptiontera+" ("+tera+")");
            sb.Replace("@@@comscomre@@@", comscorecomplete);
         
                DEFTableAdapters.GetattributeTableAdapter attributecontent = new DEFTableAdapters.GetattributeTableAdapter();
                DEF.GetattributeDataTable attributecontenttable = attributecontent.GetAttributeGroup(editionId, ProductId, pharmaformId, divisionId, categotyId);

                foreach (DataRow rowatt in attributecontenttable.Rows)
                {
                    DEF.GetattributeRow attributerow = (DEF.GetattributeRow)rowatt;
                    string htmlcontent = attributerow.HtmlContent;
                    int attributeid = attributerow.attributeId;
                    int pharmaformattributes = attributerow.PharmaFormId;
                    int productId = attributerow.ProductId;
                    string attributegroupname= attributerow.AttributeGroupName;
                    int attributegroupid = attributerow.attributegroupid;
                    
                    DEFTableAdapters.CountattributesTableAdapter countcontent = new DEFTableAdapters.CountattributesTableAdapter();
                    DEF.CountattributesDataTable counttable = countcontent.Getcountattributes(editionId, ProductId, pharmaformId, divisionId, categotyId,attributegroupid);
                    int numReg = counttable.Count;
                    if (numReg != 1)
                    {
                        string htmlcompleteattribute = " ", attributegroupnamecomplete = " ";
                        foreach (DataRow rowcount in counttable.Rows)
                        {
                            DEF.CountattributesRow countrow = (DEF.CountattributesRow)rowcount;
                            htmlcontent = countrow.HTMLContent; ;
                            htmlcompleteattribute = htmlcompleteattribute + htmlcontent;
                            attributegroupnamecomplete = attributegroupname;
                        }
                    
                        if (attributeValidator == true)
                        {
                            cadenauno.Replace("@@rubro@@", "<a class = \"inicios\" href=\"#" + attributeid + "\">" + attributegroupnamecomplete + "</a><br />" + "@@rubro@@ ");
                            cadenados.Replace("@@rubro@@", "<a class = \"inicios\" name=\"" + attributeid + "\"></a><br />" + htmlcompleteattribute + "<br /> <a class = \"inicios\" href=\"#inicio\"><img  src=\"img/returnhome.jpg\" height=\"20px\"/></a><br />@@rubro@@");
                            attributeValidator = false;
                        }
                        else
                        {
                            attributeValidator = true;
                        }
                        
                    }
                    else
                    {
                        cadenauno.Replace("@@rubro@@", "<a class = \"inicios\" href=\"#" + attributeid + "\">" + attributegroupname + "</a><br />" + "@@rubro@@ ");
                        cadenados.Replace("@@rubro@@", "<a class = \"inicios\" name=\"" + attributeid + "\"></a><br />" + htmlcontent + "<br /> <a class = \"inicios\" href=\"#inicio\"><img  src=\"img/returnhome.jpg\" height=\"20px\"/></a><br />@@rubro@@");
                    }
                   
                }
               
            cadenauno.Replace("@@rubro@@", " ");
            cadenados.Replace("@@rubro@@", " ");

            sb.Replace("@@completotop@@", cadenauno.ToString()); 
            sb.Replace("@@content@@", cadenados.ToString());
            this.saveHtmlFile(fileName+".htm", sb.ToString());
        }
       } 
      catch(Exception ee)
      {
            Console.WriteLine(ee.Message);
            Console.ReadLine();
      }              
   }

    public void generateProductMaxEditions(int countryId, string path)
    {
        try
        {
            DEFTableAdapters.ProductMaxEditionTableAdapter MaxEditions = new DEFTableAdapters.ProductMaxEditionTableAdapter();
            DEF.ProductMaxEditionDataTable MaxEditionsTable = MaxEditions.GetProductByMaxEdition(countryId);

            foreach (DataRow MaxRow in MaxEditionsTable.Rows)
            {
                DEF.ProductMaxEditionRow MaxEditionsRow = (DEF.ProductMaxEditionRow)MaxRow;

                int ProductId = MaxEditionsRow.productid;
                int PharmaFormId = MaxEditionsRow.pharmaformid;
                int DivisionId = MaxEditionsRow.divisionid;
                int CategoryId = MaxEditionsRow.categoryid;
                int EditionId = MaxEditionsRow.editionid;
                
                DEFTableAdapters.ProductByMaxEditionTableAdapter ProductsMax = new DEFTableAdapters.ProductByMaxEditionTableAdapter();
                DEF.ProductByMaxEditionDataTable ProductsMaxTable = ProductsMax.GetProducts(EditionId, ProductId, PharmaFormId, DivisionId, CategoryId);

                foreach (DataRow ProdRow in ProductsMaxTable.Rows)
                    {
                        DEF.ProductByMaxEditionRow ProductEditionsRow = (DEF.ProductByMaxEditionRow)ProdRow;

                        StringBuilder sb = new StringBuilder(this.getHtmlTemplate(path + "/indexproducts.htm"));
                        StringBuilder cadenauno = new StringBuilder(this.getHtmlTemplate(path + "/plantillatop.htm"));
                        StringBuilder cadenados = new StringBuilder(this.getHtmlTemplate(path + "/plantillaButtom.htm"));

                        int numberEdition = ProductEditionsRow.NumberEdition;
                        string IDCountry = ProductEditionsRow.ID;
                        string pharma = ProductEditionsRow.pharmaform;
                        string Brand = ProductEditionsRow.Brand;
                        string image = ProductEditionsRow.productshot;
                        string subs = ProductEditionsRow.Description;
                        string subslink = this.cleanFileName(ProductEditionsRow.Description);
                        string tera = ProductEditionsRow.TherapeuticKey;                     
                        string Descriptiontera = ProductEditionsRow.Description1;
                        string fileName = this.cleanFileName(ProductEditionsRow.Brand) + "_" + this.cleanFileName(ProductEditionsRow.pharmaform).Trim();
                        bool attributeValidator = true;
                        string stringcomscore = getComscore(numberEdition, "Medicamentos", IDCountry, Brand, pharma);
                        string comscorecomplete = this.comscore.Replace("area1.area2", stringcomscore);
                        string countrycodes = ProductEditionsRow.countrycodes;
                       
                        sb.Replace("@@nedition@@", numberEdition.ToString());
                        if (countrycodes != "0")
                        {
                            sb.Replace("@@titulo@@", Brand + " <label class=\"countrycode\">" + countrycodes +"</label>");
                        }
                        else
                        {
                            sb.Replace("@@titulo@@", Brand);
                        }
                        sb.Replace("@@formaFarma@@", pharma);
                        sb.Replace("@@imagen@@", image);
                        sb.Replace("@@ingrediente@@", "<a href=\"../sustancias/" + subslink + ".htm \" >" + subs + "</a>");
                        sb.Replace("@@AA@@", Descriptiontera + " (" + tera + ")");
                        sb.Replace("@@@comscomre@@@", comscorecomplete);

                        DEFTableAdapters.GetattributeTableAdapter attributecontent = new DEFTableAdapters.GetattributeTableAdapter();
                        DEF.GetattributeDataTable attributecontenttable = attributecontent.GetAttributeGroup(EditionId, ProductId, PharmaFormId, DivisionId, CategoryId);

                        if (attributecontenttable.Rows.Count != 0)
                        {
                            foreach (DataRow rowatt in attributecontenttable.Rows)
                            {
                                DEF.GetattributeRow attributerow = (DEF.GetattributeRow)rowatt;
                                string htmlcontent = attributerow.HtmlContent;
                                int attributeid = attributerow.attributeId;
                                int pharmaformattributes = attributerow.PharmaFormId;
                                int productId = attributerow.ProductId;
                                string attributegroupname = attributerow.AttributeGroupName;
                                int attributegroupid = attributerow.attributegroupid;

                                DEFTableAdapters.CountattributesTableAdapter countcontent = new DEFTableAdapters.CountattributesTableAdapter();
                                DEF.CountattributesDataTable counttable = countcontent.Getcountattributes(EditionId, ProductId, PharmaFormId, DivisionId, CategoryId, attributegroupid);
                                int numReg = counttable.Count;
                                if (numReg != 1)
                                {
                                    string htmlcompleteattribute = " ", attributegroupnamecomplete = " ";
                                    foreach (DataRow rowcount in counttable.Rows)
                                    {
                                        DEF.CountattributesRow countrow = (DEF.CountattributesRow)rowcount;
                                        htmlcontent = countrow.HTMLContent; ;
                                        htmlcompleteattribute = htmlcompleteattribute + htmlcontent;
                                        attributegroupnamecomplete = attributegroupname;
                                    }

                                    if (attributeValidator == true)
                                    {
                                        cadenauno.Replace("@@rubro@@", "<a class = \"inicios\" href=\"#" + attributeid + "\">" + attributegroupnamecomplete + "</a><br />" + "@@rubro@@ ");
                                        cadenados.Replace("@@rubro@@", "<a class = \"inicios\" name=\"" + attributeid + "\"></a><br />" + htmlcompleteattribute + "<br /> <a class = \"inicios\" href=\"#inicio\"><img  src=\"img/returnhome.jpg\" height=\"20px\"/></a><br />@@rubro@@");
                                        attributeValidator = false;
                                    }
                                    else
                                    {
                                        attributeValidator = true;
                                    }
                                }
                                else
                                {
                                    cadenauno.Replace("@@rubro@@", "<a class = \"inicios\" href=\"#" + attributeid + "\">" + attributegroupname + "</a><br />" + "@@rubro@@ ");
                                    cadenados.Replace("@@rubro@@", "<a class = \"inicios\" name=\"" + attributeid + "\"></a><br />" + htmlcontent + "<br /> <a class = \"inicios\" href=\"#inicio\"><img  src=\"img/returnhome.jpg\" height=\"20px\"/></a><br />@@rubro@@");
                                }
                            }

                            cadenauno.Replace("@@rubro@@", " ");
                            cadenados.Replace("@@rubro@@", " ");
                        }
                        else
                        {
                            DEFTableAdapters.HtmlContentTableAdapter htmltable = new DEFTableAdapters.HtmlContentTableAdapter();
                            DEF.HtmlContentDataTable htmlcontent = htmltable.GetHtmlContent(EditionId, ProductId, PharmaFormId, DivisionId, CategoryId);

                            if (htmlcontent.Rows.Count != 0)
                            {
                                foreach (DataRow row in htmlcontent.Rows)
                                {
                                    DEF.HtmlContentRow rows = (DEF.HtmlContentRow)row;
                                    cadenados.Replace("@@rubro@@", rows.HTMLContent);
                                }
                            }
                        }
                        cadenauno.Replace("@@rubro@@", "");
                        cadenados.Replace("@@rubro@@", "");

                        sb.Replace("@@completotop@@", cadenauno.ToString());
                        sb.Replace("@@content@@", cadenados.ToString());
                        this.saveHtmlFile(fileName + ".htm", sb.ToString());

                        DEFTableAdapters.ProductCategoriesTableAdapter categories = new DEFTableAdapters.ProductCategoriesTableAdapter();
                        categories.UpdateProductCategories(fileName+".htm", ProductId, PharmaFormId, DivisionId, CategoryId);
                    }

            } 
        }
        catch (Exception ee)
        {
            Console.WriteLine(ee.Message);          
        }
    } 

    public void generateHTMLS()
    {
        //Get products from database

        DEFTableAdapters.ProductContentsTableAdapter productContentAdp = new DEFTableAdapters.ProductContentsTableAdapter();
        DEF.ProductContentsDataTable productContentTable = productContentAdp.getProductContent();

        foreach (DataRow pcRow in productContentTable.Rows)
        {
            DEF.ProductContentsRow productContentRow = (DEF.ProductContentsRow)pcRow;

            string fileName = this.cleanFileName(productContentRow.Brand) + "_" + this.cleanFileName(productContentRow.PharmaForm).Trim();

            StringBuilder sb = new StringBuilder(productContentRow.HtmlContent);

            sb.Replace("</head>", this.googleAnalytics + Environment.NewLine + "</head>" + Environment.NewLine);

            sb.Replace("<body>", "<body>" + Environment.NewLine + this.comscore.Replace("area1.area2", "medicamentos." + fileName) + Environment.NewLine);

            this.saveHtmlFile(fileName + ".htm",sb.ToString());
            
        }
    }
    
    #endregion

    #region Substances



    public void getAlphabetSubs(int editionId, string path)
    {
        DEFTableAdapters.AlphabetTableAdapter alphabetAdp = new DEFTableAdapters.AlphabetTableAdapter();
        DEF.AlphabetDataTable alphabetTable = alphabetAdp.getAlphabetSubs(editionId);

        foreach (DataRow asRow in alphabetTable.Rows)
        {
            DEF.AlphabetRow letterSustRow = (DEF.AlphabetRow)asRow;

            DEFTableAdapters.ActiveSubstancesTableAdapter substanceAdp = new DEFTableAdapters.ActiveSubstancesTableAdapter();
            DEF.ActiveSubstancesDataTable substanceTable = substanceAdp.getSubstance(editionId, letterSustRow.Letter);

            StringBuilder sbHtmlTemplete = new StringBuilder(this.getHtmlTemplate());

            //sbHtmlTemplete.Replace("@@@comscore@@@", this.comscore.Replace("area1.area2", "sustancias." + letterSustRow.Letter.ToLower()) + Environment.NewLine);

            sbHtmlTemplete.Append("<table width=\"100%\">\r\n");
            

            foreach (DataRow sustRow in substanceTable.Rows)
            {
                DEF.ActiveSubstancesRow substance = (DEF.ActiveSubstancesRow)sustRow;
                sbHtmlTemplete.Append("<tr><td>\r\n");

                sbHtmlTemplete.Append("<p class=\"sustancia\"><a href=\"" + (this.cleanFileName(substance.SUBSTANCE).Length > 50 ? this.cleanFileName(substance.SUBSTANCE).Substring(0, 50) : this.cleanFileName(substance.SUBSTANCE)) + ".htm\" >" +
                    this.replaceAccentsToHtmlCodes(substance.SUBSTANCE.ToUpper()) + "</a></p>\r\n");
                sbHtmlTemplete.Append("</td></tr>\r\n");
                
                string ID = substance.ID;
                int nEdition = substance.NumberEdition;
                string scomscore = getComscore(nEdition, "Sustancias", ID, substance.SUBSTANCE, null);

                DEFTableAdapters.ProductsBySubstanceTableAdapter prodSust = new DEFTableAdapters.ProductsBySubstanceTableAdapter();
                DEF.ProductsBySubstanceDataTable prodSustTable = prodSust.getAloneProductsBySubstance(editionId, substance.ACTIVESUBSTANCEID);

                StringBuilder sbHtmlTemplate = new StringBuilder(this.getHtmlTemplate(path + "/sustancia.htm"));

                sbHtmlTemplate.Replace("@@@Sustancia@@@", this.replaceAccentsToHtmlCodes(substance.SUBSTANCE.ToUpper()));

                sbHtmlTemplate.Replace("<body>", "<body>" + Environment.NewLine + this.comscore.Replace("area1.area2", scomscore));

                sbHtmlTemplate.Append("<table width=\"100%\">\r\n");

                if (prodSustTable.Rows.Count > 0)
                {
                    sbHtmlTemplate.Append("<tr><td>\r\n");
                    sbHtmlTemplate.Append("<span class=\"solos\" >SOLOS</span>\r\n");
                    sbHtmlTemplate.Append("</td></tr>\r\n");

                    foreach (DataRow prodSRow in prodSustTable)
                    {
                        DEF.ProductsBySubstanceRow prodSustRow = (DEF.ProductsBySubstanceRow)prodSRow;
                        sbHtmlTemplate.Append("<tr><td>\r\n");
  
                        sbHtmlTemplate.Append("<p class=\"Marca_productoLink\" ><a href=\"../productos/" + this.cleanFileName(prodSustRow.BRAND) + "_" + this.cleanFileName(prodSustRow.PHARMAFORM).Trim() + ".htm\" >* ");
                        sbHtmlTemplate.Append(prodSustRow.BRAND + ". " + (prodSustRow.IsPRODUCTDESCRIPTIONNull() == true ? prodSustRow.IsLABDESCRIPTIONNull() == true ? "" : (prodSustRow.LABDESCRIPTION + ". ") : (prodSustRow.PRODUCTDESCRIPTION + ". ")));
                        sbHtmlTemplate.Append(prodSustRow.PHARMAFORM == "Sin asignar" ? "" : ("<i>" + prodSustRow.PHARMAFORM + ".</i> "));
                        sbHtmlTemplate.Append(prodSustRow.DIVISIONSHORTNAME + ".</a></p>");
                        
                        sbHtmlTemplate.Append("</td></tr>\r\n");

                    }
                    
                    
                    sbHtmlTemplate.Append("<tr><td>\r\n");
                    sbHtmlTemplate.Append("&nbsp;\r\n");
                    sbHtmlTemplate.Append("</td></tr>\r\n");
                }

                DEF.ProductsBySubstanceDataTable prodCombSustTable = prodSust.getProductsBySubstance(editionId, substance.ACTIVESUBSTANCEID);

                if (prodCombSustTable.Rows.Count > 0)
                {
                    sbHtmlTemplate.Append("<tr><td>\r\n");
                    sbHtmlTemplate.Append("<span class=\"solos\" >COMBINADOS</span>\r\n");
                    sbHtmlTemplate.Append("</td></tr>\r\n");

                    foreach (DataRow prodSRow in prodCombSustTable)
                    {
                        DEF.ProductsBySubstanceRow prodSustRow = (DEF.ProductsBySubstanceRow)prodSRow;
                        sbHtmlTemplate.Append("<tr><td>\r\n");

                        sbHtmlTemplate.Append("<p class=\"Marca_productoLink\" ><a href=\"../productos/" + this.cleanFileName(prodSustRow.BRAND) + "_" + this.cleanFileName(prodSustRow.PHARMAFORM).Trim() + ".htm\" >* "); 
                        sbHtmlTemplate.Append(prodSustRow.BRAND + ". <i>" + prodSustRow.Substances + ".</i> ");
                        sbHtmlTemplate.Append((prodSustRow.IsPRODUCTDESCRIPTIONNull() == true ? prodSustRow.IsLABDESCRIPTIONNull() == true ? "" : (prodSustRow.LABDESCRIPTION + ". ") : (prodSustRow.PRODUCTDESCRIPTION + ". ")));
                        sbHtmlTemplate.Append(prodSustRow.PHARMAFORM == "Sin asignar" ? "" : ("<i>" + prodSustRow.PHARMAFORM + ".</i> "));
                        sbHtmlTemplate.Append(prodSustRow.DIVISIONSHORTNAME.Trim() + ".</a></p>");

                        sbHtmlTemplate.Append("</td></tr>\r\n");

                    }
                    sbHtmlTemplate.Append("<tr><td>\r\n");
                    sbHtmlTemplate.Append("&nbsp;\r\n");
                    sbHtmlTemplate.Append("</td></tr>\r\n");
                }

                sbHtmlTemplate.Append("</table>\r\n");
                sbHtmlTemplate.Append("</div></body></html>");

                this.saveHtmlFile((this.cleanFileName(substance.SUBSTANCE).Length > 50 ? this.cleanFileName(substance.SUBSTANCE).Substring(0, 50) : this.cleanFileName(substance.SUBSTANCE)) + ".htm", this.replaceAccentsToHtmlCodes(sbHtmlTemplate.ToString()));
                
            
            }
            sbHtmlTemplete.Append("</table>\r\n");

            sbHtmlTemplete.Append("</div></body></html>");

            this.saveHtmlFile("indsus" + letterSustRow.Letter.ToLower() + ".htm", this.replaceAccentsToHtmlCodes(sbHtmlTemplete.ToString()));



        }

        this.generateNumberSubstances(editionId, 1, "4-METILBENCILIDENO", path);
        this.generateNumberSubstances(editionId, 3429, "ß-CAROTENO", path);
        

    }

    public void getAlphabetInds(int editionId, string path)
    {
        DEFTableAdapters.AlphabetTableAdapter alphabetAdp = new DEFTableAdapters.AlphabetTableAdapter();
        DEF.AlphabetDataTable alphabetTable = alphabetAdp.getAlphabetInds(editionId);

        foreach (DataRow alIndRow in alphabetTable)
        {
            DEF.AlphabetRow letterIndRow = (DEF.AlphabetRow)alIndRow;

            DEFTableAdapters.IndicationsTableAdapter indicationAdp = new DEFTableAdapters.IndicationsTableAdapter();
            DEF.IndicationsDataTable indicationTable = indicationAdp.getIndication(editionId, letterIndRow.Letter);

            StringBuilder sbHtmlTemplete = new StringBuilder(this.getHtmlTemplate());

            sbHtmlTemplete.Replace("@@@comscore@@@", this.comscore.Replace("area1.area2", "indicaciones." + letterIndRow.Letter.ToLower()) + Environment.NewLine);

            sbHtmlTemplete.Append("<table width=\"100%\">\r\n");

            foreach (DataRow indRow in indicationTable)
            {
                DEF.IndicationsRow indication = (DEF.IndicationsRow)indRow;
                sbHtmlTemplete.Append("<tr><td>\r\n");
                
                sbHtmlTemplete.Append("<p class=\"sustancia\"><a href=\"" + (this.cleanFileName(indication.INDICATION).Length > 100 ? this.cleanFileName(indication.INDICATION).Substring(0, 100) : this.cleanFileName(indication.INDICATION)) + ".htm\" >" +
                    this.replaceAccentsToHtmlCodes(indication.INDICATION.ToUpper()) + "</a></p>\r\n");
                sbHtmlTemplete.Append("</td></tr>\r\n");


                DEFTableAdapters.ProductsTableAdapter prodInd = new DEFTableAdapters.ProductsTableAdapter();
                DEF.ProductsDataTable prodIndTable = prodInd.getProductsByIndication(editionId, indication.INDICATIONID);

                StringBuilder sbHtmlTemplate = new StringBuilder(this.getHtmlTemplate(path + "/indicacion.htm"));

                sbHtmlTemplate.Replace("@@@Indicacion@@@", this.replaceAccentsToHtmlCodes(indication.INDICATION.ToUpper()));

                int nEdition = indication.NumberEdition;

                string ID = indication.ID;

                string comscomreC = getComscore(nEdition,"Indicaciones",ID,indication.INDICATION,null);

                sbHtmlTemplate.Replace("<body>", "<body>" + Environment.NewLine + this.comscore.Replace("area1.area2", comscomreC) + Environment.NewLine);

                sbHtmlTemplate.Append("<table width=\"100%\">\r\n");

                foreach (DataRow prodRow in prodIndTable)
                {
                    DEF.ProductsRow prodIndRow = (DEF.ProductsRow)prodRow;
                    sbHtmlTemplate.Append("<tr><td>\r\n");

                    sbHtmlTemplate.Append("<p class=\"Marca_productoLink\" ><a href=\"../productos/" + this.cleanFileName(prodIndRow.BRAND) + "_" + this.cleanFileName(prodIndRow.PHARMAFORM).Trim() + ".htm\" >* ");
                    sbHtmlTemplate.Append(prodIndRow.BRAND + ". " + (prodIndRow.IsPRODUCTDESCRIPTIONNull() == true ? prodIndRow.IsLABDESCRIPTIONNull() == true ? "" : (prodIndRow.LABDESCRIPTION + ". ") : (prodIndRow.PRODUCTDESCRIPTION + ". ")));
                    sbHtmlTemplate.Append(prodIndRow.PHARMAFORM == "Sin asignar" ? "" : ("<i>" + prodIndRow.PHARMAFORM + ".</i> "));
                    sbHtmlTemplate.Append(prodIndRow.DIVISIONSHORTNAME + ".</a></p>");

                    sbHtmlTemplate.Append("</td></tr>\r\n");

                }
                sbHtmlTemplate.Append("</table>\r\n");

                sbHtmlTemplate.Append("</div></body></html>");

                this.saveHtmlFile((this.cleanFileName(indication.INDICATION).Length > 100 ? this.cleanFileName(indication.INDICATION).Substring(0, 100) : this.cleanFileName(indication.INDICATION)) + ".htm", this.replaceAccentsToHtmlCodes(sbHtmlTemplate.ToString()));

            }

            sbHtmlTemplete.Append("</table>\r\n");

            sbHtmlTemplete.Append("</div></body></html>");

            this.saveHtmlFile("indind" + cleanFileName(letterIndRow.Letter.ToLower()) + ".htm", this.replaceAccentsToHtmlCodes(sbHtmlTemplete.ToString()));


        }


    }

    public void generateSidef(int editionId, string path)
    {
        DEFTableAdapters.DivisionsTableAdapter divAdp = new DEFTableAdapters.DivisionsTableAdapter();
        DEF.DivisionsDataTable divTable = divAdp.getDivisionProductShot(editionId);
        
        StringBuilder sbHtmlTemplete = new StringBuilder(this.getHtmlTemplate());

        sbHtmlTemplete.Replace("<body>", "<body>\r\n" + this.comscore.Replace("area1.area2", "sidef") + Environment.NewLine);

        sbHtmlTemplete.Append("<table width=\"100%\">\r\n");

        foreach (DataRow divRow in divTable)
        {
            DEF.DivisionsRow divPsRow = (DEF.DivisionsRow)divRow;

            DEFTableAdapters.ProductShotsTableAdapter psAdp = new DEFTableAdapters.ProductShotsTableAdapter();
            DEF.ProductShotsDataTable psTable = psAdp.getProductShotsByDivision(editionId, divPsRow.DivisionId);

            sbHtmlTemplete.Append("<tr><td>\r\n");

            sbHtmlTemplete.Append("<p class=\"sidef\"><a href=\"" + this.cleanFileName(divPsRow.DivisionShortName) + ".htm\" >" +
                this.replaceAccentsToHtmlCodes(divPsRow.DivisionShortName.ToUpper()) + "</a></p>\r\n");
            sbHtmlTemplete.Append("</td></tr>\r\n");

            StringBuilder sbHtmlSidef = new StringBuilder(this.getHtmlTemplate(path + "/sidef.htm"));

            sbHtmlSidef.Replace("<body>", "<body>\r\n" + this.comscore.Replace("area1.area2", "sidef." + this.cleanFileName(divPsRow.DivisionShortName)) + Environment.NewLine);

            sbHtmlSidef.Replace("@@@LABORATORIO@@@", divPsRow.DivisionShortName);

            sbHtmlSidef.Append("<table width=\"100%\" align=\"Center\" rules=\"all\" >\r\n");

            for (int x = 0; x < psTable.Rows.Count; x++)
            {
                sbHtmlSidef.Append("<tr>\r\n");
                if (x == (psTable.Rows.Count - 1))
                {
                    sbHtmlSidef.Append("<td style=\"height:250px;width:250px;\" colspan=\"2\">\r\n");
                    if(psTable.Rows[x]["TypeInEdition"].ToString().Equals("P"))
                        sbHtmlSidef.Append("<a href=\"../productos/" + this.cleanFileName(psTable.Rows[x]["Brand"].ToString()) + "_" + this.cleanFileName(psTable.Rows[x]["PharmaForm"].ToString()).Trim() + ".htm\" style=\"text-decoration:none;\" >\r\n");
                    
                    sbHtmlSidef.Append("<center><img id=\"" + x.ToString() + "\"  src=\"images/" + psTable.Rows[x]["ProductShot"].ToString() + "\" alt=\"imagen\" /></center>\r\n");
                    if (psTable.Rows[x]["TypeInEdition"].ToString().Equals("P"))
                        sbHtmlSidef.Append("</a>");
                    
                    sbHtmlSidef.Append("</td>\r\n");
                }
                else
                {
                    sbHtmlSidef.Append("<td style=\"height:250px;width:250px;\">\r\n");
                    if (psTable.Rows[x]["TypeInEdition"].ToString().Equals("P"))
                        sbHtmlSidef.Append("<a href=\"../productos/" + this.cleanFileName(psTable.Rows[x]["Brand"].ToString()) + "_" + this.cleanFileName(psTable.Rows[x]["PharmaForm"].ToString()).Trim() + ".htm\" style=\"text-decoration:none;\" >\r\n");
                    sbHtmlSidef.Append("<center><img id=\"" + x.ToString() + "\"  src=\"images/" + psTable.Rows[x]["ProductShot"].ToString() + "\" alt=\"imagen\" /></center>\r\n");
                    if (psTable.Rows[x]["TypeInEdition"].ToString().Equals("P"))
                        sbHtmlSidef.Append("</a>");

                    sbHtmlSidef.Append("</td>\r\n");

                    sbHtmlSidef.Append("<td style=\"height:250px;width:250px;\">\r\n");
                    if (psTable.Rows[x + 1]["TypeInEdition"].ToString().Equals("P"))
                        sbHtmlSidef.Append("<a href=\"../productos/" + this.cleanFileName(psTable.Rows[x + 1]["Brand"].ToString()) + "_" + this.cleanFileName(psTable.Rows[x + 1]["PharmaForm"].ToString()).Trim() + ".htm\" style=\"text-decoration:none;\" >\r\n");
                    
                    sbHtmlSidef.Append("<center><img id=\"" + (x + 1).ToString() + "\"  src=\"images/" + psTable.Rows[x + 1]["ProductShot"].ToString() + "\" alt=\"imagen\" /></center>\r\n");
                    if (psTable.Rows[x]["TypeInEdition"].ToString().Equals("P"))
                        sbHtmlSidef.Append("</a>");

                    sbHtmlSidef.Append("</td>\r\n");

                    x++;

                }
                sbHtmlSidef.Append("</tr>\r\n");
            }

            sbHtmlSidef.Append("</table>\r\n");

            sbHtmlSidef.Append("</div></body></html>");

            this.saveHtmlFile(this.cleanFileName(divPsRow.DivisionShortName) + ".htm", sbHtmlSidef.ToString());

        }
        sbHtmlTemplete.Append("</table>\r\n");

        sbHtmlTemplete.Append("</div></body></html>");

        this.saveHtmlFile("indexSidef.htm", sbHtmlTemplete.ToString());

    }

    public void getAlphabetDivs(int editionId, string path)
    {
        DEFTableAdapters.AlphabetTableAdapter alphabetAdp = new DEFTableAdapters.AlphabetTableAdapter();
        DEF.AlphabetDataTable alphabetTable = alphabetAdp.getAlphabetLabs(editionId);

        foreach (DataRow alDr in alphabetTable)
        {
            DEF.AlphabetRow letterLabRow = (DEF.AlphabetRow)alDr;

            DEFTableAdapters.DivisionsTableAdapter laboratoryAdp = new DEFTableAdapters.DivisionsTableAdapter();
            DEF.DivisionsDataTable laboratoryTable = laboratoryAdp.getDivisionByLetter(editionId, letterLabRow.Letter);

            StringBuilder sbHtmlTemplete = new StringBuilder(this.getHtmlTemplate());

           

            //sbHtmlTemplete.Replace("@@@comscore@@@", this.comscore.Replace("area1.area2", "laboratorios." + letterLabRow.Letter.ToLower()) + Environment.NewLine);

            sbHtmlTemplete.Append("<table width=\"100%\">\r\n");

            foreach (DataRow labDr in laboratoryTable)
            {
                DEF.DivisionsRow laboratory = (DEF.DivisionsRow)labDr;
                sbHtmlTemplete.Append("<tr><td>\r\n");

                sbHtmlTemplete.Append("<p class=\"Lab_laboratorio\"><a href=\"" + this.cleanFileName(laboratory.DivisionName) + ".htm\" >" +
                    this.replaceAccentsToHtmlCodes(laboratory.DivisionName.ToUpper()) + "</a></p>\r\n");
                sbHtmlTemplete.Append("</td></tr>\r\n");

                StringBuilder sbLabTemplate = new StringBuilder(this.getHtmlTemplate(path + "/laboratorio.htm"));

                string ID = laboratory.ID;

                int nEdition = laboratory.NumberEdition;

                string comscomreC = getComscore(nEdition,"Laboratorios",ID,laboratory.DivisionName,null);

                sbLabTemplate.Replace("@@@comscore@@@", this.comscore.Replace("area1.area2", comscomreC) + Environment.NewLine);

                sbLabTemplate.Replace("@@@Laboratorio@@@", this.replaceAccentsToHtmlCodes(laboratory.DivisionName.ToUpper()));

                DEFTableAdapters.DivisionInformationTableAdapter divInf = new DEFTableAdapters.DivisionInformationTableAdapter();
                DEF.DivisionInformationDataTable divInfTable = divInf.getDivInformation(laboratory.DivisionId);

                string address = "";

                foreach (DataRow divInfDr in divInfTable)
                {
                    DEF.DivisionInformationRow divInformation = (DEF.DivisionInformationRow)divInfDr;

                    address = address + Environment.NewLine + this.setInformation(divInformation);
                                      

                }

                sbLabTemplate.Replace("@@@Direccion@@@", address);

                DEFTableAdapters.DivisionImagesTableAdapter divImgAdp = new DEFTableAdapters.DivisionImagesTableAdapter();
                DEF.DivisionImagesDataTable divImgTable = divImgAdp.getImageByDivision(laboratory.DivisionId);

                string img = "";

                foreach (DataRow divImgDr in divImgTable)
                {
                    DEF.DivisionImagesRow divImage = (DEF.DivisionImagesRow)divImgDr;

                    img = "<img src=\"labContenido/img/" + divImage.ImageName + "\" /> \r\n";

                }

                sbLabTemplate.Replace("@@@Logo@@@", img);

                DEFTableAdapters.CategoriesTableAdapter catAdp = new DEFTableAdapters.CategoriesTableAdapter();
                DEF.CategoriesDataTable catTable = catAdp.getCategoriesByDivision(editionId, laboratory.DivisionId);

                foreach (DataRow catDr in catTable)
                {
                    DEF.CategoriesRow category = (DEF.CategoriesRow)catDr;

                    sbLabTemplate.Append("<tr><td>\r\n");

                    sbLabTemplate.Append("<p class='Lab_division'><center><b>" + this.replaceAccentsToHtmlCodes(category.CATEGORYNAME.ToUpper()) + "</b></center></p>");

                    sbLabTemplate.Append("</td></tr>\r\n");

                    DEFTableAdapters.ProductsTableAdapter prodLabAdp = new DEFTableAdapters.ProductsTableAdapter();
                    DEF.ProductsDataTable prodLabTable = prodLabAdp.getProductsByDivision(editionId, laboratory.DivisionId, category.CATEGORYID);

                    foreach (DataRow prodDr in prodLabTable)
                    {
                        DEF.ProductsRow product = (DEF.ProductsRow)prodDr;

                        sbLabTemplate.Append("<tr><td>\r\n");

                        if (product.TYPEINEDITION == "P" && product.Page != "Mencionado")
                        {
                            sbLabTemplate.Append("<p class=\"Marca_productoLink\" ><a href=\"../productos/" + this.cleanFileName(product.BRAND) + "_" + this.cleanFileName(product.PHARMAFORM).Trim() + ".htm\" >* ");
                            sbLabTemplate.Append(product.BRAND + ". " + (product.IsPRODUCTDESCRIPTIONNull() == true ? product.IsLABDESCRIPTIONNull() == true ? "" : (product.LABDESCRIPTION + ". ") : (product.PRODUCTDESCRIPTION + ". ")));
                            sbLabTemplate.Append(product.PHARMAFORM == "Sin asignar" ? "" : ("<i>" + product.PHARMAFORM + ".</i>"));
                            sbLabTemplate.Append("</a></p>");
                        }
                        else
                        {
                            sbLabTemplate.Append("<p class=\"Marca_productoLink\" >");
                            sbLabTemplate.Append(product.BRAND + ". " + (product.IsPRODUCTDESCRIPTIONNull() == true ? product.IsLABDESCRIPTIONNull() == true ? "" : (product.LABDESCRIPTION + ". ") : (product.PRODUCTDESCRIPTION + ". ")));
                            sbLabTemplate.Append(product.PHARMAFORM == "Sin asignar" ? "" : ("<i>" + product.PHARMAFORM + ".</i>"));
                            sbLabTemplate.Append("</p>");
                        }

                        sbLabTemplate.Append("</td></tr>\r\n");


                    }
                    sbLabTemplate.Append("<br /><br />\r\n");
                }

                sbLabTemplate.Append("</table>\r\n");

                sbLabTemplate.Append("</div></body></html>");
                
                this.saveHtmlFile(this.cleanFileName(laboratory.DivisionName) + ".htm", this.replaceAccentsToHtmlCodes(sbLabTemplate.ToString()));


            }

            sbHtmlTemplete.Append("</table>\r\n");
            sbHtmlTemplete.Replace("@@@comscore@@@", "");
            sbHtmlTemplete.Append("</div></body></html>");

            this.saveHtmlFile("indlab" + letterLabRow.Letter.ToLower() + ".htm", this.replaceAccentsToHtmlCodes(sbHtmlTemplete.ToString()));

        }
        
    }

    public void getAlphabetProducts(int editionId)
    {
        DEFTableAdapters.AlphabetTableAdapter alpAdp = new DEFTableAdapters.AlphabetTableAdapter();
        DEF.AlphabetDataTable alpTable = alpAdp.getAlphabetProds(editionId);

        foreach(DataRow alpDr in alpTable)
        {
            DEF.AlphabetRow alphabet = (DEF.AlphabetRow)alpDr;

            DEFTableAdapters.ProductTableAdapter productsAdp = new DEFTableAdapters.ProductTableAdapter();
            DEF.ProductDataTable productsTable = productsAdp.GetProductsByLetter(editionId, alphabet.Letter); 

            StringBuilder sbHtmlTemplete = new StringBuilder(this.getHtmlTemplate());

            sbHtmlTemplete.Replace("@@@comscore@@@", this.comscore.Replace("area1.area2", "medicamentos." + alphabet.Letter.ToLower()) + Environment.NewLine);

            sbHtmlTemplete.Append("<table width=\"100%\">\r\n");

            foreach (DataRow prodDr in productsTable)
            {
                DEF.ProductRow product = (DEF.ProductRow)prodDr;

                sbHtmlTemplete.Append("<tr><td>\r\n");

                sbHtmlTemplete.Append("<p class=\"Marca_productoLink\" ><a href=\"../productos/" + this.cleanFileName(product.BRAND) + "_" + this.cleanFileName(product.PHARMAFORM).Trim() + ".htm\" >* ");
                sbHtmlTemplete.Append(product.BRAND + ". ");
                //sbHtmlTemplete.Append((product.IsPRODUCTDESCRIPTIONNull() == true ? product.IsLABDESCRIPTIONNull() == true ? "" : (product.LABDESCRIPTION + ". ") : (product.PRODUCTDESCRIPTION + ". ")));
                sbHtmlTemplete.Append(product.PHARMAFORM == "Sin asignar" ? "" : ("<i>" + product.PHARMAFORM + ".</i> "));
                sbHtmlTemplete.Append(product.DIVISIONSHORTNAME.Trim() + ".</a></p>");

                sbHtmlTemplete.Append("<tr><td>\r\n");

            }
            sbHtmlTemplete.Append("</table>\r\n");

            sbHtmlTemplete.Append("</div></body></html>");

            this.saveHtmlFile("indmar" + alphabet.Letter.ToLower() + ".htm", this.replaceAccentsToHtmlCodes(sbHtmlTemplete.ToString()));


        }


    }

    #endregion

    #region Private Methods

    //private string validateSubstanceLetter(string letter)
    //{
    //    StringBuilder substance = new StringBuilder();

    //    switch (letter)
    //    {
    //        case "A":
    //            DataRow dr = sustTable.NewRow();
                
    //            dr["ActiveSubstanceId"] = 2944;
    //            dr["Substance"] = "2-AMINO";

    //            DEF.ActiveSubstancesRow sustRow = (DEF.ActiveSubstancesRow)dr;
    //            sustTable.AddActiveSubstancesRow(sustRow);

    //            break;
    //        case "P":
    //            DataRow dr2 = sustTable.NewRow();
    //            dr2["ActiveSubstanceId"] = 2943;
    //            dr2["Substance"] = "1-PROPANOL";

    //            DEF.ActiveSubstancesRow sustRow2 = (DEF.ActiveSubstancesRow)dr2;
    //            sustTable.AddActiveSubstancesRow(sustRow2);
    //            break;
    //        case "M":
    //            DataRow dr3 = sustTable.NewRow();
    //            dr3["ActiveSubstanceId"] = 1;
    //            dr3["Substance"] = "4-METILBENCILIDENO";

    //            DEF.ActiveSubstancesRow sustRow3 = (DEF.ActiveSubstancesRow)dr3;
    //            sustTable.AddActiveSubstancesRow(sustRow3);

    //            DataRow dr4 = sustTable.NewRow();
    //            dr4["ActiveSubstanceId"] = 2945;
    //            dr4["Substance"] = "2-METIL";

    //            DEF.ActiveSubstancesRow sustRow4 = (DEF.ActiveSubstancesRow)dr4;
    //            sustTable.AddActiveSubstancesRow(sustRow4);
    //            break;
    //        case "C":
    //            DataRow dr5 = sustTable.NewRow();
    //            dr5["ActiveSubstanceId"] = 3429;
    //            dr5["Substance"] = "ß-CAROTENO";

    //            DEF.ActiveSubstancesRow sustRow5 = (DEF.ActiveSubstancesRow)dr5;
    //            sustTable.AddActiveSubstancesRow(sustRow5);
    //            break;
    //    }

    //}

    private void generateNumberSubstances(int editionId, int substanceId, string substance, string path)
    {
        DEFTableAdapters.ProductsBySubstanceTableAdapter prodSust = new DEFTableAdapters.ProductsBySubstanceTableAdapter();
        DEF.ProductsBySubstanceDataTable prodSustTable = prodSust.getAloneProductsBySubstance(editionId, substanceId);

        StringBuilder sbHtmlTemplate = new StringBuilder(this.getHtmlTemplate(path + "/sustancia.htm"));

        sbHtmlTemplate.Replace("@@@Sustancia@@@", substance.ToUpper());

        sbHtmlTemplate.Replace("<body>", "<body>" + Environment.NewLine + this.comscore.Replace("area1.area2", "sustancias." + this.cleanFileName(substance.ToUpper())) + Environment.NewLine);

        sbHtmlTemplate.Append("<table width=\"100%\">\r\n");

        if (prodSustTable.Rows.Count > 0)
        {
            sbHtmlTemplate.Append("<tr><td>\r\n");
            sbHtmlTemplate.Append("<span class=\"solos\" >SOLOS</span>\r\n");
            sbHtmlTemplate.Append("</td></tr>\r\n");

            foreach (DataRow prodSRow in prodSustTable)
            {
                DEF.ProductsBySubstanceRow prodSustRow = (DEF.ProductsBySubstanceRow)prodSRow;
                sbHtmlTemplate.Append("<tr><td>\r\n");

                sbHtmlTemplate.Append("<p class=\"Marca_productoLink\" ><a href=\"../productos/" + this.cleanFileName(prodSustRow.BRAND) + "_" + this.cleanFileName(prodSustRow.PHARMAFORM).Trim() + ".htm\" >* ");
                sbHtmlTemplate.Append(prodSustRow.BRAND + ". " + (prodSustRow.IsPRODUCTDESCRIPTIONNull() == true ? prodSustRow.IsLABDESCRIPTIONNull() == true ? "" : (prodSustRow.LABDESCRIPTION + ". ") : (prodSustRow.PRODUCTDESCRIPTION + ". ")));
                sbHtmlTemplate.Append(prodSustRow.PHARMAFORM == "Sin asignar" ? "" : ("<i>" + prodSustRow.PHARMAFORM + ".</i> "));
                sbHtmlTemplate.Append(prodSustRow.DIVISIONSHORTNAME + ".</a></p>");

                sbHtmlTemplate.Append("</td></tr>\r\n");

            }
            sbHtmlTemplate.Append("<tr><td>\r\n");
            sbHtmlTemplate.Append("&nbsp;\r\n");
            sbHtmlTemplate.Append("</td></tr>\r\n");
        }

        DEF.ProductsBySubstanceDataTable prodCombSustTable = prodSust.getProductsBySubstance(editionId, substanceId);

        if (prodCombSustTable.Rows.Count > 0)
        {
            sbHtmlTemplate.Append("<tr><td>\r\n");
            sbHtmlTemplate.Append("<span class=\"solos\" >COMBINADOS</span>\r\n");
            sbHtmlTemplate.Append("</td></tr>\r\n");

            foreach (DataRow prodSRow in prodCombSustTable)
            {
                DEF.ProductsBySubstanceRow prodSustRow = (DEF.ProductsBySubstanceRow)prodSRow;
                sbHtmlTemplate.Append("<tr><td>\r\n");

                sbHtmlTemplate.Append("<p class=\"Marca_productoLink\" ><a href=\"../productos/" + this.cleanFileName(prodSustRow.BRAND) + "_" + this.cleanFileName(prodSustRow.PHARMAFORM).Trim() + ".htm\" >* ");
                sbHtmlTemplate.Append(prodSustRow.BRAND + ". <i>" + prodSustRow.Substances + ".</i> ");
                sbHtmlTemplate.Append((prodSustRow.IsPRODUCTDESCRIPTIONNull() == true ? prodSustRow.IsLABDESCRIPTIONNull() == true ? "" : (prodSustRow.LABDESCRIPTION + ". ") : (prodSustRow.PRODUCTDESCRIPTION + ". ")));
                sbHtmlTemplate.Append(prodSustRow.PHARMAFORM == "Sin asignar" ? "" : ("<i>" + prodSustRow.PHARMAFORM + ".</i> "));
                sbHtmlTemplate.Append(prodSustRow.DIVISIONSHORTNAME.Trim() + ".</a></p>");

                sbHtmlTemplate.Append("</td></tr>\r\n");

            }
            sbHtmlTemplate.Append("<tr><td>\r\n");
            sbHtmlTemplate.Append("&nbsp;\r\n");
            sbHtmlTemplate.Append("</td></tr>\r\n");
        }
        sbHtmlTemplate.Append("</table>\r\n");

        sbHtmlTemplate.Append("</div></body></html>");

        this.saveHtmlFile(this.cleanFileName(substance.ToUpper()) + ".htm", this.replaceAccentsToHtmlCodes(sbHtmlTemplate.ToString()));


    }

    private string setInformation(DEF.DivisionInformationRow divRow)
    {
        StringBuilder sb = new StringBuilder();

        sb.Append("<table>" + Environment.NewLine + "<tr>" + Environment.NewLine + "<td>" + Environment.NewLine);

        sb.Append("<p class='Lab_Datos'>" + divRow.Address + "<br />" + Environment.NewLine);

        if (!divRow.IsSuburbNull())
            if(!divRow.Suburb.Equals(""))
                sb.Append(divRow.Suburb + "<br />" + Environment.NewLine);

        if (!divRow.IsZipCodeNull())
            if(!divRow.ZipCode.Equals(""))
                sb.Append(divRow.ZipCode + " ");

        if (!divRow.IsCityNull())
            if(!divRow.City.Equals(""))
                sb.Append(divRow.City + (divRow.IsStateNull() ? "" : ", " + divRow.State) + "<br />" + Environment.NewLine);

        if (!divRow.IsTelephoneNull())
            if(!divRow.Telephone.Equals(""))
                sb.Append(divRow.Telephone + "<br />" + Environment.NewLine);

        if (!divRow.IsFaxNull())
            if(!divRow.Fax.Equals(""))
                sb.Append(divRow.Fax + "<br />" + Environment.NewLine);

        if (!divRow.IsEmailNull())
            if(!divRow.Email.Equals(""))
                sb.Append(this.setEmail(divRow.Email) + "<br />" + Environment.NewLine);

        if (!divRow.IsWebNull())
            if(!divRow.Web.Equals(""))
            {
                string[] web = divRow.Web.Split(';');

                for (int x = 0; x < web.Length; x++)
                {
                    sb.Append("<a href='http://" + web[x] + "' target='_blank' >" + web[x] + "</a>" + Environment.NewLine);
                }
            }
        sb.Append("</p>" + Environment.NewLine + "</td>" + Environment.NewLine + "</tr>" + Environment.NewLine + "</table>" + Environment.NewLine);

        return sb.ToString();
    }

    private string setEmail(string email)
    {
        string cadena = "";

        if (email.Contains("/"))
        {
            string[] emails = email.Split('/');

            foreach (string mail in emails)
            {
                cadena = "<a href='mailto:" + mail.Trim() + "' >" + mail.Trim() + "</a> / " + Environment.NewLine;
            }


        }
        else
            cadena = "<a href='mailto:" + email.Trim() + "' >" + email.Trim() + "</a> " + Environment.NewLine;


        return cadena;

    }

    #endregion

    public string comscore = "<!-- Begin comScore Inline Tag 1.1111.15 -->" + Environment.NewLine +
                                 "<script type=\"text/javascript\">" + Environment.NewLine +
                                 "// <![CDATA[" + Environment.NewLine +
                                 "function udm_(a){var b=\"comScore=\",c=document,d=c.cookie,e=\"\",f=\"indexOf\",g=\"substring\",h=\"length\",i=2048,j,k=\"&ns_\",l=\"&\",m,n,o,p,q=window,r=q.encodeURIComponent||escape;if(d[f](b)+1)for(o=0,n=d.split(\";\"),p=n[h];o<p;o++)m=n[o][f](b),m+1&&(e=l+unescape(n[o][g](m+b[h])));a+=k+\"_t=\"+ +(new Date)+k+\"c=\"+(c.characterSet||c.defaultCharset||\"\")+\"&c8=\"+r(c.title)+e+\"&c7=\"+r(c.URL)+\"&c9=\"+r(c.referrer),a[h]>i&&a[f](l)>0&&(j=a[g](0,i-8).lastIndexOf(l),a=(a[g](0,j)+k+\"cut=\"+r(a[g](j+1)))[g](0,i)),c.images?(m=new Image,q.ns_p||(ns_p=m),m.src=a):c.write(\"<\",\"p\",\"><\",'img src=\"',a,'\" height=\"1\" width=\"1\" alt=\"*\"',\"><\",\"/p\",\">\")}" + Environment.NewLine +
                                 "udm_('http'+(document.location.href.charAt(4)=='s'?'s://sb':'://b')+'.scorecardresearch.com/b?c1=2&c2=13152961&ns_site=medicamentos-mx&name=area1.area2');" + Environment.NewLine +
                                 "// ]]>" + Environment.NewLine +
                                 "</script>" + Environment.NewLine +
                                 "<noscript><p><img src=\"http://b.scorecardresearch.com/p?c1=2&amp;c2=13152961&amp;ns_site=medicamentos-mx&amp;name=area1.area2\" height=\"1\" width=\"1\" alt=\"*\"></p></noscript>" + Environment.NewLine +
                                 "<!-- End comScore Inline Tag -->" + Environment.NewLine;

    private string googleAnalytics = "<!--  Google Analytics -->" + Environment.NewLine +
                                     "<script type=\"text/javascript\">" + Environment.NewLine + 
                                     "var _gaq = _gaq || [];" + Environment.NewLine + 
                                     "_gaq.push(['_setAccount', 'UA-22228735-6']);" + Environment.NewLine + 
                                     "_gaq.push(['_trackPageview']);" + Environment.NewLine + 
                                     "(function() {" + Environment.NewLine + 
                                     "var ga = document.createElement('script'); ga.type = 'text/javascript'; ga.async = true;" + Environment.NewLine + 
                                     "ga.src = ('https:' == document.location.protocol ? 'https://ssl' : 'http://www') + '.google-analytics.com/ga.js';"+ Environment.NewLine + 
                                     "var s = document.getElementsByTagName('script')[0]; s.parentNode.insertBefore(ga, s);" + Environment.NewLine + 
                                     "})();" + Environment.NewLine + 
                                     "</script>" + Environment.NewLine + 
                                     "<!--  Google Analytics -->" + Environment.NewLine;


}
