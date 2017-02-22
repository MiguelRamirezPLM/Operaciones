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
using System.IO;
using System.Collections;

 
public class IndLetter : GenerateHTML
{
    #region Constructors

    public IndLetter(string sourcePath, string destinationPath) : base(sourcePath, destinationPath) { }

    #endregion

    #region Public Methods

    public override void getHtmlFiles(int editionId)
    {
        throw new Exception("The method or operation is not implemented.");
    }

    public void getCompanies(int editionId)
    {

        //Get species from database

        DEACITableAdapters.CompaniesTableAdapter companyAdapter = new DEACITableAdapters.CompaniesTableAdapter();
        DEACI.CompaniesDataTable companTable = companyAdapter.getCompanies(editionId);
 
        // Get the html file template to save the correspond letter html file:

        StringBuilder sbHtmlTemplate = new StringBuilder(this.getHtmlTemplate());
       // sbHtmlTemplate.Replace("@@@TituloIndice@@@", this.replaceAccentsToHtmlCodes(CompanyName.ToUpper()));


        
        foreach (DataRow cRow in companTable.Rows)
        {
            DEACI.CompaniesRow companyRow = (DEACI.CompaniesRow)cRow;
 
            sbHtmlTemplate.Append(this.companyLink(companyRow));
             
        }
        

        sbHtmlTemplate.Append("\r\n</div></body></html>");

        this.saveHtmlFile("clientes.htm", sbHtmlTemplate.ToString());

    }


    public void getHtmlFilesIndGenericos(int editonId, string title)
    {

        //get Alphabet from Database:

        DEACITableAdapters.AlphabetTableAdapter alphabetAdp = new DEACITableAdapters.AlphabetTableAdapter();
        DEACI.AlphabetDataTable alphabetTable = alphabetAdp.GetAlphabetGenericos(editonId);

        foreach (DataRow aRow in alphabetTable.Rows)
        {

            DEACI.AlphabetRow alphabetRows = (DEACI.AlphabetRow)aRow;
           
            //Get Substances from Database:

            DEACITableAdapters.GenericsTableAdapter genAdp = new DEACITableAdapters.GenericsTableAdapter();
            DEACI.GenericsDataTable genTable = genAdp.getSectionsByLetter(alphabetRows.Letter, editonId);
            
            // Get the html file template to save the correspond letter html file:

            StringBuilder sbHtmlTempale = new StringBuilder(this.getHtmlTemplate());
            sbHtmlTempale.Replace("@@@TituloIndice@@@", this.replaceAccentsToHtmlCodes(title.ToUpper()));

            sbHtmlTempale.Append("<p align='center' class='titulo'><span id=\"lblLetter\">-" + alphabetRows.Letter + "-</span></p>");

            foreach (DataRow prodGenRow in genTable.Rows)
            {
                DEACI.GenericsRow genRow = (DEACI.GenericsRow)prodGenRow;


                sbHtmlTempale.Append(this.genericLink(genRow));


                //Get productos from database
                //sbHtmlTempale.Append("\r\n<blockquote>");

                sbHtmlTempale.Append("\r\n<ul>");

                //DEACITableAdapters.ProductSectionsTableAdapter producGen = new DEACITableAdapters.ProductSectionsTableAdapter();
                //DEACI.ProductSectionsDataTable prodTable = producGen.getProductSections1(editonId, genRow.SectionId);

                DEACITableAdapters.plm_spGetProductBySectionTableAdapter producGen = new DEACITableAdapters.plm_spGetProductBySectionTableAdapter();
                DEACI.plm_spGetProductBySectionDataTable prodTable = producGen.getSectionByProducts(genRow.SectionId, editonId);


                  foreach (DataRow prdGenericRow in prodTable.Rows)
                    {


                        DEACI.plm_spGetProductBySectionRow prdgenRow = (DEACI.plm_spGetProductBySectionRow)prdGenericRow;
                       
                        sbHtmlTempale.Append(this.prdGenericLink(prdgenRow));


                    }
                 
                  sbHtmlTempale.Append("</ul><br>");
                  
            }


            sbHtmlTempale.Append("\r\n</body></html>");

            // Save the htm file:
            sbHtmlTempale.Replace("@@@letters@@@", constraintLettersGeneric(editonId));
            this.saveHtmlFile(alphabetRows.Letter + "-gen.htm", sbHtmlTempale.ToString());

        }
        
    }


    public void getHtmlFilesIndGenericosNComercial(int editonId, string title)
    {

        //get Alphabet from Database:

        DEACITableAdapters.AlphabetTableAdapter alphabetAdp = new DEACITableAdapters.AlphabetTableAdapter();
        DEACI.AlphabetDataTable alphabetTable = alphabetAdp.GetAlphabetNComercial(editonId);
        foreach (DataRow aRow in alphabetTable.Rows)
        {

            DEACI.AlphabetRow alphabetRows = (DEACI.AlphabetRow)aRow;

            //Get Substances from Database:

            DEACITableAdapters.ProductsNCTableAdapter genNCAdp = new DEACITableAdapters.ProductsNCTableAdapter();

            DEACI.ProductsNCDataTable genNCTable = genNCAdp.getProducts1(editonId, alphabetRows.Letter);

            // Get the html file template to save the correspond letter html file:

            StringBuilder sbHtmlTempale = new StringBuilder(this.getHtmlTemplate());
            sbHtmlTempale.Replace("@@@TituloIndice@@@", this.replaceAccentsToHtmlCodes(title.ToUpper()));

            sbHtmlTempale.Append("<p align='center' class='titulo'><span id=\"lblLetter\">-" + alphabetRows.Letter + "-</span></p>");

            foreach (DataRow prodGenNCRow in genNCTable.Rows)
            {
                DEACI.ProductsNCRow genNCRow = (DEACI.ProductsNCRow)prodGenNCRow;

                sbHtmlTempale.Append(this.genericNComercialLink(genNCRow));
                 
                
            }


            sbHtmlTempale.Append("\r\n</body></html>");

            // Save the htm file:
            sbHtmlTempale.Replace("@@@letters@@@", constraintLettersGenericNComercial(editonId));
            this.saveHtmlFile(alphabetRows.Letter + "-gral.htm", sbHtmlTempale.ToString());

        }

    }


    public void getHtmlFilesIndGeneralImagenologia(int editionId, string title)
    {

        //Get species from database

        DEACITableAdapters.GenericsTableAdapter sectAdapter = new DEACITableAdapters.GenericsTableAdapter();
        DEACI.GenericsDataTable sectTable = sectAdapter.getSectionImag(editionId);

        // Get the html file template to save the correspond letter html file:

        StringBuilder sbHtmlTemplate = new StringBuilder(this.getHtmlTemplate());
        // sbHtmlTemplate.Replace("@@@TituloIndice@@@", this.replaceAccentsToHtmlCodes(CompanyName.ToUpper()));



        foreach (DataRow  sRow in sectTable.Rows)
        {

            DEACI.GenericsRow sectRow = (DEACI.GenericsRow)sRow;
             sbHtmlTemplate.Append(this.sectionLink(sectRow));

             sbHtmlTemplate.Append("\r\n<ul>");

             
             DEACITableAdapters.plm_spGetProductBySectionTableAdapter producGen = new DEACITableAdapters.plm_spGetProductBySectionTableAdapter();
             DEACI.plm_spGetProductBySectionDataTable prodTable = producGen.getSectionByProducts(sectRow.SectionId, editionId);


             foreach (DataRow prdGenericRow in prodTable.Rows)
             {


                 DEACI.plm_spGetProductBySectionRow prdgenRow = (DEACI.plm_spGetProductBySectionRow)prdGenericRow;

                 sbHtmlTemplate.Append(this.prdGenericLink(prdgenRow));


             }

             sbHtmlTemplate.Append("</ul><br>");

        }


        sbHtmlTemplate.Append("\r\n</body></html>");

        this.saveHtmlFile("gral.htm", sbHtmlTemplate.ToString());

    }


    public void getHtmlFilesIndMarcas(int editionId, string title)
    {

        //get Alphabet from Database:

        DEACITableAdapters.AlphabetTableAdapter alphabetAdp = new DEACITableAdapters.AlphabetTableAdapter();
        DEACI.AlphabetDataTable alphabetTable = alphabetAdp.GetAlphabetBrands(editionId);

        foreach (DataRow aRow in alphabetTable.Rows)
        {

            DEACI.AlphabetRow alphabetRows = (DEACI.AlphabetRow)aRow;

            //Get Substances from Database:

            DEACITableAdapters.BrandsTableAdapter brandAdp = new DEACITableAdapters.BrandsTableAdapter();
            DEACI.BrandsDataTable brandTable = brandAdp.getBrands(editionId, alphabetRows.Letter);

            // Get the html file template to save the correspond letter html file:

            StringBuilder sbHtmlTemplate = new StringBuilder(this.getHtmlTemplate());
            sbHtmlTemplate.Replace("@@@TituloIndice@@@", this.replaceAccentsToHtmlCodes(title.ToUpper()));

            sbHtmlTemplate.Append("<p align='center' class='titulo'><span id=\"lblLetter\">-" + alphabetRows.Letter + "-</span></p>");

            foreach (DataRow BrandRow in brandTable.Rows)
            {
                DEACI.BrandsRow brandRow = (DEACI.BrandsRow)BrandRow;
                sbHtmlTemplate.Append(this.brandLink(brandRow));


                //Get productos from database 

                sbHtmlTemplate.Append("\r\n<ul>");

                DEACITableAdapters.CompanyBrandsTableAdapter brandComp = new DEACITableAdapters.CompanyBrandsTableAdapter();
                DEACI.CompanyBrandsDataTable brandCompTable = brandComp.getComp(editionId, brandRow.BRANDID);


                foreach (DataRow compBrandRow in brandCompTable.Rows)
                {


                    DEACI.CompanyBrandsRow cBrandRow = (DEACI.CompanyBrandsRow)compBrandRow;

                    sbHtmlTemplate.Append(this.companyBrandLink(cBrandRow));
                    
                }

                sbHtmlTemplate.Append("</ul><br>");

            }


            sbHtmlTemplate.Append("\r\n</body></html>");

            // Save the htm file:
            sbHtmlTemplate.Replace("@@@letters@@@", constraintLettersBrands(editionId));
            this.saveHtmlFile(alphabetRows.Letter + "-marca.htm", sbHtmlTemplate.ToString());

        }

    }

    public void getHtmlFilesIndGuia(int editionId)
    {

        //Get species from database

        DEACITableAdapters.StatesTableAdapter stateAdapter = new DEACITableAdapters.StatesTableAdapter();
        DEACI.StatesDataTable statesTable = stateAdapter.getStates(editionId);

        // Get the html file template to save the correspond letter html file:

        StringBuilder sbHtmlTemplate = new StringBuilder(this.getHtmlTemplate());
        // sbHtmlTemplate.Replace("@@@TituloIndice@@@", this.replaceAccentsToHtmlCodes(CompanyName.ToUpper()));


        foreach (DataRow stRow in statesTable.Rows)
        {
            DEACI.StatesRow statesRow = (DEACI.StatesRow)stRow;

            sbHtmlTemplate.Append(this.statesLink(statesRow));

        }
        
        sbHtmlTemplate.Append("\r\n</body></html>");

        this.saveHtmlFile("guia.htm", sbHtmlTemplate.ToString());

        getHtmlFilesIndInfoFabricantes(editionId);

    }

    public void getHtmlFilesIndSeccion8(int editionId)
    {

        //Get species from database

        DEACITableAdapters.StatesTableAdapter stateAdapter = new DEACITableAdapters.StatesTableAdapter();
        DEACI.StatesDataTable statesTable = stateAdapter.getLabStates(editionId);

        // Get the html file template to save the correspond letter html file:

        StringBuilder sbHtmlTemplate = new StringBuilder(this.getHtmlTemplate());
        // sbHtmlTemplate.Replace("@@@TituloIndice@@@", this.replaceAccentsToHtmlCodes(CompanyName.ToUpper()));


        foreach (DataRow stRow in statesTable.Rows)
        {
            DEACI.StatesRow statesRow = (DEACI.StatesRow)stRow;

            sbHtmlTemplate.Append(this.statesLink(statesRow));

        }

        sbHtmlTemplate.Append("\r\n</body></html>");

        this.saveHtmlFile("laboratorios.htm", sbHtmlTemplate.ToString());

        getHtmlFilesIndInfoLabs(editionId);

    }

    public void getHtmlFilesIndInfoFabricantes(int editionId)
    {

        //get states from Database:

        DEACITableAdapters.StatesTableAdapter stateAdapter = new DEACITableAdapters.StatesTableAdapter();
        DEACI.StatesDataTable statesTable = stateAdapter.getStates(editionId);

        foreach (DataRow stRow in statesTable.Rows)
        {

            DEACI.StatesRow stateRows = (DEACI.StatesRow)stRow;

            //Get Substances from Database:

            DEACITableAdapters.CitiesTableAdapter citiesAdp = new DEACITableAdapters.CitiesTableAdapter();
            DEACI.CitiesDataTable citiesTable = citiesAdp.getCities(stateRows.StateId, editionId);

            // Get the html file template to save the correspond letter html file:

            StringBuilder sbHtmlTemplate = new StringBuilder(this.getHtmlTemplate());
           
            sbHtmlTemplate.Append("<p align='center' class='titulo'><span id=\"lblLetter\">-" + stateRows.Name + "-</span></p>");

            foreach (DataRow citiesRow in citiesTable.Rows)
            {
                DEACI.CitiesRow cityRow = (DEACI.CitiesRow)citiesRow;
                sbHtmlTemplate.Append(this.cityLink(cityRow));


                //Get productos from database 


                DEACITableAdapters.CompaniesByCityTableAdapter cityFab = new DEACITableAdapters.CompaniesByCityTableAdapter();
                DEACI.CompaniesByCityDataTable cityFabTable = cityFab.getInfo(editionId, cityRow.CityId);

                    sbHtmlTemplate.Append("\r\n<ul>");

                    foreach (DataRow infofabRow in cityFabTable.Rows)
                {


                    DEACI.CompaniesByCityRow infoFRow = (DEACI.CompaniesByCityRow)infofabRow;
                    sbHtmlTemplate.Append(this.infoFabricanteLink(infoFRow));

                    DEACITableAdapters.PhonesTableAdapter phone = new DEACITableAdapters.PhonesTableAdapter();
                    DEACI.PhonesDataTable phoneTable = phone.getPhones(infoFRow.CompanyId);

                    
                    sbHtmlTemplate.Append("<br> ");
                }

                    sbHtmlTemplate.Append("</ul><br> ");

            }


            sbHtmlTemplate.Append("\r\n</body></html>");

            // Save the htm file:
           // sbHtmlTemplate.Replace("@@@letters@@@", constraintLettersBrands(editonId));
            this.saveHtmlFile(stateRows.StateId + ".htm", sbHtmlTemplate.ToString());

        }
    }

    public void getHtmlFilesIndInfoLabs(int editionId)
    {

        //get states from Database:

        DEACITableAdapters.StatesTableAdapter stateAdapter = new DEACITableAdapters.StatesTableAdapter();
        DEACI.StatesDataTable statesTable = stateAdapter.getLabStates(editionId);

        foreach (DataRow stRow in statesTable.Rows)
        {

            DEACI.StatesRow stateRows = (DEACI.StatesRow)stRow;

            //Get Substances from Database:

            DEACITableAdapters.CitiesTableAdapter citiesAdp = new DEACITableAdapters.CitiesTableAdapter();
            DEACI.CitiesDataTable citiesTable = citiesAdp.getLabCities(stateRows.StateId, editionId);

            // Get the html file template to save the correspond letter html file:

            StringBuilder sbHtmlTemplate = new StringBuilder(this.getHtmlTemplate());

            sbHtmlTemplate.Append("<p align='center' class='titulo'><span id=\"lblLetter\">-" + stateRows.Name + "-</span></p>");

            foreach (DataRow citiesRow in citiesTable.Rows)
            {
                DEACI.CitiesRow cityRow = (DEACI.CitiesRow)citiesRow;
                sbHtmlTemplate.Append(this.cityLink(cityRow));


                //Get productos from database 


                DEACITableAdapters.CompaniesByCityTableAdapter cityLab = new DEACITableAdapters.CompaniesByCityTableAdapter();
                DEACI.CompaniesByCityDataTable cityLabTable = cityLab.getLabInfo(editionId, cityRow.CityId);

                sbHtmlTemplate.Append("\r\n<ul>");

                foreach (DataRow infolabRow in cityLabTable.Rows)
                {


                    DEACI.CompaniesByCityRow infoFRow = (DEACI.CompaniesByCityRow)infolabRow;

                    sbHtmlTemplate.Append(this.infoFabricanteLink(infoFRow));

                    sbHtmlTemplate.Append("<br> ");

                }

                sbHtmlTemplate.Append("</ul><br> ");

            }


            sbHtmlTemplate.Append("\r\n</body></html>");

            // Save the htm file:
            // sbHtmlTemplate.Replace("@@@letters@@@", constraintLettersBrands(editonId));
            this.saveHtmlFile(stateRows.StateId + ".htm", sbHtmlTemplate.ToString());

        }
    }


    public void getHtmlFilesIndSeccion(int editionId, int parentid, string titulo, string nhtm)
    {

        //Get species from database

        DEACITableAdapters.plm_spGetSectionParentTableAdapter sec1Adapter = new DEACITableAdapters.plm_spGetSectionParentTableAdapter();
        DEACI.plm_spGetSectionParentDataTable sec1Table = sec1Adapter.getSectionParent(editionId, parentid);

        // Get the html file template to save the correspond letter html file:

        StringBuilder sbHtmlTemplate = new StringBuilder(this.getHtmlTemplate());
        sbHtmlTemplate.Replace("@@@titulo@@@", this.replaceAccentsToHtmlCodes(titulo.ToUpper()));



        foreach (DataRow s1Row in sec1Table.Rows)

        {
            
            //if (s1Row[4].ToString() == "0")
            //{
                

            //    DEACI.plm_spGetSectionParentRow sec1Row = (DEACI.plm_spGetSectionParentRow)s1Row;

            //    //sbHtmlTemplate.Append(this.section1Link(sec1Row));

            //    getHtmlFilesIndProductSec(editionId, Convert.ToInt32(s1Row[0]), s1Row[1].ToString());

            //}
            //else
            //{
                DEACI.plm_spGetSectionParentRow secRow = (DEACI.plm_spGetSectionParentRow)s1Row;
               
                sbHtmlTemplate.Append(this.section1Link(secRow));

                DEACITableAdapters.plm_spGetSectionParentTableAdapter sec2Adapter = new DEACITableAdapters.plm_spGetSectionParentTableAdapter();
                DEACI.plm_spGetSectionParentDataTable sec2Table = sec2Adapter.getSectionParent(editionId, secRow.SectionId);

                sbHtmlTemplate.Append("<blockquote> ");

                foreach (DataRow s2Row in sec2Table.Rows)
                {

                    DEACI.plm_spGetSectionParentRow sec2Row = (DEACI.plm_spGetSectionParentRow)s2Row;

                    sbHtmlTemplate.Append(this.section1Link(sec2Row));
                    
                    getHtmlFilesIndProductSec(editionId, Convert.ToInt32(s2Row[0]), s2Row[1].ToString());
                }

                sbHtmlTemplate.Append("</blockquote> ");

                
            //}
 
            
        }


        sbHtmlTemplate.Append("\r\n</div></body></html>");

        this.saveHtmlFile(nhtm, sbHtmlTemplate.ToString());

    }

    public void getHtmlFilesIndSeccion2(int editionId, int parentid, string titulo)
    {

        //Get species from database

        DEACITableAdapters.plm_spGetSectionParentTableAdapter secAdapter = new DEACITableAdapters.plm_spGetSectionParentTableAdapter();
        DEACI.plm_spGetSectionParentDataTable secTable = secAdapter.getSectionParent(editionId, parentid);

        // Get the html file template to save the correspond letter html file:

        StringBuilder sbHtmlTemplate = new StringBuilder(this.getHtmlTemplate());
        sbHtmlTemplate.Replace("@@@titulo@@@", this.replaceAccentsToHtmlCodes(titulo.ToUpper()));
 
        foreach (DataRow s1Row in secTable.Rows)
        {
            DEACI.plm_spGetSectionParentRow secRow = (DEACI.plm_spGetSectionParentRow)s1Row;
            sbHtmlTemplate.Append(this.section2Link(secRow));



            DEACITableAdapters.plm_spGetSectionParentTableAdapter sec2Adapter = new DEACITableAdapters.plm_spGetSectionParentTableAdapter();
            DEACI.plm_spGetSectionParentDataTable sec2Table = sec2Adapter.getSectionParent(editionId, secRow.SectionId);

     sbHtmlTemplate.Append("<blockquote> "); 

            foreach (DataRow s2Row in sec2Table.Rows)
            {

                DEACI.plm_spGetSectionParentRow sec1Row = (DEACI.plm_spGetSectionParentRow)s2Row;
                sbHtmlTemplate.Append(this.section1Link(sec1Row));

                getHtmlFilesIndProductSec(editionId, Convert.ToInt32(s2Row[0]), s2Row[1].ToString());
 
            }

            sbHtmlTemplate.Append("</blockquote> ");

            

        }
         //  getHtmlFilesIndProductSec(editionId, Convert.ToInt32(s1Row[0]), s1Row[1].ToString());

        sbHtmlTemplate.Append("\r\n</body></html>");

        this.saveHtmlFile("kits.htm", sbHtmlTemplate.ToString());

        //getHtmlFilesIndProductSec(editionId, );

    }


    public void getHtmlFilesIndProductSec(int editionId, int SectionId, string section)
    {


        //Get species from database

        DEACITableAdapters.DSectionsTableAdapter productsecAdapter = new DEACITableAdapters.DSectionsTableAdapter();
        DEACI.DSectionsDataTable productsecTable = productsecAdapter.getProductsBySection(editionId, SectionId);

        StringBuilder sbHtmlTemplate = new StringBuilder(this.getHtmlTemplate());
        sbHtmlTemplate.Replace("@@@titulo@@@", this.replaceAccentsToHtmlCodes(section.ToUpper()));

        foreach (DataRow psRow in productsecTable.Rows)
        {
            DEACI.DSectionsRow productsecRow = (DEACI.DSectionsRow)psRow;

            sbHtmlTemplate.Append(this.prodsecLink(productsecRow));

        }


        sbHtmlTemplate.Append("\r\n</div></body></html>");
        
        this.saveHtmlFile(SectionId + ".htm", sbHtmlTemplate.ToString());



    }

    protected string companyLink(DEACI.CompaniesRow companiesRow)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("<span class=\"lab\"> <b><a href=\"" + companiesRow.HtmlFile + "\" target='adentro' style='text-decoration:none'>"
         + this.replaceAccentsToHtmlCodes(companiesRow.CompanyName) + "</b></a><br/>" + System.Environment.NewLine);


        return sb.ToString();
    }

    protected string section1Link(DEACI.plm_spGetSectionParentRow sec1Row)
    {
        StringBuilder sb = new StringBuilder();

        sb.Append("<span > <a  class=\"linksSecciones\" href=\"" + sec1Row.SectionId + ".htm\" >"
         + this.replaceAccentsToHtmlCodes(sec1Row.Description) + "</a><br/><br/>" + System.Environment.NewLine);


        return sb.ToString();
    }

    protected string section2Link(DEACI.plm_spGetSectionParentRow sec1Row)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("<span  class=\"linksSecciones\" >" + this.replaceAccentsToHtmlCodes(sec1Row.Description) + "</span><br/>" + System.Environment.NewLine);


        return sb.ToString();
    }

    protected string sectionLink(DEACI.GenericsRow sectRow)
    {
        StringBuilder sb = new StringBuilder();

        sb.Append("<p class='subsection' > " + this.replaceAccentsToHtmlCodes(sectRow.Description.ToUpper()) + "</a></p>\r\n");

        return sb.ToString();

    }

    protected string brandLink(DEACI.BrandsRow brandRow)
    {
        StringBuilder sb = new StringBuilder();

        sb.Append("<p class='subsection' > " + this.replaceAccentsToHtmlCodes(brandRow.BRANDNAME.ToUpper()) + "</p>\r\n");

        return sb.ToString();

    }

    protected string cityLink(DEACI.CitiesRow cityRow)
    {
        StringBuilder sb = new StringBuilder();

        sb.Append("<p class='subsectionEstado' > " + this.replaceAccentsToHtmlCodes(cityRow.Name.ToUpper()) + "</p>\r\n");

        return sb.ToString();

    }

    protected string infoFabricanteLink(DEACI.CompaniesByCityRow infoFRow)
    {
        StringBuilder sb = new StringBuilder();

        
        sb.Append("<span  class='company' > <b>" + this.replaceAccentsToHtmlCodes(infoFRow.CompanyName.ToUpper()) + "</b></span ><br>\r\n");

        if (!infoFRow.IsAddressNull())
            sb.Append("<span  class='direccion' >" + this.replaceAccentsToHtmlCodes(infoFRow.Address) + "<br>\r\n");
        if (!infoFRow.IsSuburbNull())
            sb.Append( this.replaceAccentsToHtmlCodes(infoFRow.Suburb) + "<br>\r\n");

        if (!infoFRow.IsPostalCodeNull())
            sb.Append("C.P. " + this.replaceAccentsToHtmlCodes(infoFRow.PostalCode) + "<br>\r\n");

        if (!infoFRow.IsphoneNull())

            sb.Append(this.replacePhone(infoFRow.phone) + "<br>\r\n");

        if (!infoFRow.IsEmailNull())
            sb.Append("Email: <a href='mailto:" + this.replaceAccentsToHtmlCodes(infoFRow.Email) + "'  >" + this.replaceAccentsToHtmlCodes(infoFRow.Email)+ "</a><br>\r\n");

        if (!infoFRow.IsWebNull())
            sb.Append("<a href='http://" + this.replaceAccentsToHtmlCodes(infoFRow.Web) + "' target='_blank' >" + this.replaceAccentsToHtmlCodes(infoFRow.Web) + "</a><br>\r\n");

       

        if (!infoFRow.IsContactNull())
            sb.Append("<b>Contacto: </b>" + this.replaceAccentsToHtmlCodes(infoFRow.Contact) + "<br>\r\n");

        
        return sb.ToString();

    }


    protected string companyBrandLink(DEACI.CompanyBrandsRow cBrandRow)
    {
        StringBuilder sb = new StringBuilder();

        if (!cBrandRow.IsCOMPANYSHORTNAMENull())
        {
            sb.Append("<p class='laboratorio' > <a  class=\"links\" href=\"../../terceras/" + cBrandRow.HTMLFILE + "\"  style='text-decoration:none'>  " +
            this.replaceAccentsToHtmlCodes(cBrandRow.COMPANYSHORTNAME.ToUpper()) + "</a></p>\r\n");
        }

        else
        {
            sb.Append("<p class='laboratorio' > <a  class=\"links\" href=\"../../terceras/" + cBrandRow.HTMLFILE + "\"  style='text-decoration:none'>  " +
            this.replaceAccentsToHtmlCodes(cBrandRow.CompanyName.ToUpper()) + "</a></p>\r\n");
        }
        

        return sb.ToString();

    }


    protected string statesLink(DEACI.StatesRow statesRow)
    {
        StringBuilder sb = new StringBuilder();


        sb.Append("<p class='estado' > <a  class=\"linksEstado\" href=\"" + statesRow.StateId + ".htm\"  style='text-decoration:none'>  " +
            this.replaceAccentsToHtmlCodes(statesRow.Name) + "</a></p>\r\n");
        

        return sb.ToString();

    }

    protected string genericLink(DEACI.GenericsRow genRow)
    {
        StringBuilder sb = new StringBuilder();

        sb.Append("<p class='subsection' > " + this.replaceAccentsToHtmlCodes(genRow.Description.ToUpper()) + "</a></p>\r\n");

        return sb.ToString();

    }

    protected string genericNComercialLink(DEACI.ProductsNCRow genNCRow)
    {
        StringBuilder sb = new StringBuilder();

     

        if (!genNCRow.IsHtmlFileNull())
        {
            if (!genNCRow.IsDescriptionNull())
            {
                sb.Append("<p class=\"producto\"> <b><a  class=\"links\" href=\"../../productos/" + genNCRow.HtmlFile + "\"  style='text-decoration:none'> * "
                + this.replaceAccentsToHtmlCodes(genNCRow.ProductName) + ".</b> " + this.replaceAccentsToHtmlCodes(genNCRow.Description) + "<b > " + this.replaceAccentsToHtmlCodes(genNCRow.CompanyShortName) + ".</b></a></p>" + System.Environment.NewLine);
            
            }
            else 
            { 
                    sb.Append("<p class=\"producto\"> <b><a  class=\"links\" href=\"../../productos/" + genNCRow.HtmlFile + "\"  style='text-decoration:none'> * "
                 + this.replaceAccentsToHtmlCodes(genNCRow.ProductName) + ".</b> "  + "<b>. " + this.replaceAccentsToHtmlCodes(genNCRow.CompanyShortName) + ".</b></a></p>" + System.Environment.NewLine);
            }
        }
        else
        {
            if (!genNCRow.IsDescriptionNull())
            {
                sb.Append("<p class='producto' > <span> <b>&nbsp&nbsp " + this.replaceAccentsToHtmlCodes(genNCRow.ProductName) + "</b>" + this.replaceAccentsToHtmlCodes(genNCRow.Description)
                + "<b> " + this.replaceAccentsToHtmlCodes(genNCRow.CompanyShortName) + "</b></span></p>\r\n");
            }
            else
            {

                sb.Append("<p class='producto' > <span> <b>&nbsp&nbsp " + this.replaceAccentsToHtmlCodes(genNCRow.ProductName) + "</b>.<b>" + this.replaceAccentsToHtmlCodes(genNCRow.CompanyShortName) + "</b></span></p>\r\n");
            }
       }
        return sb.ToString();

    }

    protected string prodsecLink(DEACI.DSectionsRow  productsecRow )
    {
        StringBuilder sb = new StringBuilder();


        if (!productsecRow.IsHtmlFileNull())
        {
            if (!productsecRow.IsDETAILNull())
            {
                sb.Append("<p class=\"producto\"> <a  class=\"linksSections\" href=\"../../productos/" + productsecRow.HtmlFile + "\"  style='text-decoration:none'> * "
                + this.replaceAccentsToHtmlCodes(productsecRow.ProductName) + ".<i> " + this.replaceAccentsToHtmlCodes(productsecRow.DETAIL) + ".</i><b > "
                + this.replaceAccentsToHtmlCodes(productsecRow.CompanyShortName) + ".</b></a></p>" + System.Environment.NewLine);

            }
            else
            {
               sb.Append("<p class=\"producto\"> <a  class=\"linksSections\" href=\"../../productos/" + productsecRow.HtmlFile + "\"  style='text-decoration:none'> * "
             + this.replaceAccentsToHtmlCodes(productsecRow.ProductName)+ "<b>. " + this.replaceAccentsToHtmlCodes(productsecRow.CompanyShortName) 
             + ".</b></a></p>" + System.Environment.NewLine);
            }
        }
        else
        {
            if (!productsecRow.IsDETAILNull())
            {
                sb.Append("<p class='producto' > <span> &nbsp&nbsp " + this.replaceAccentsToHtmlCodes(productsecRow.ProductName) + ". <i>"
                    + this.replaceAccentsToHtmlCodes(productsecRow.DETAIL)
                + "</i>. <b> " + this.replaceAccentsToHtmlCodes(productsecRow.CompanyShortName) + "</b></span></p>\r\n");
            }
            else
            {

                sb.Append("<p class='producto' > <span> &nbsp&nbsp " + this.replaceAccentsToHtmlCodes(productsecRow.ProductName) + ".<b> "
                    + this.replaceAccentsToHtmlCodes(productsecRow.CompanyShortName) + "</b></span></p>\r\n");
            }
        }

            

                

        return sb.ToString();

    }



    protected string prdGenericLink(DEACI.plm_spGetProductBySectionRow prdgenRow)
    {

        StringBuilder sb = new StringBuilder();


        sb.Append("<p class=\"producto\"> <a class=\"links\" href=\"../../Productos/" + prdgenRow.HtmlFile + "\"style='text-decoration:none'> <b>"
        + this.replaceAccentsToHtmlCodes(prdgenRow.ProductName) + ".</b> " + this.replaceAccentsToHtmlCodes(prdgenRow.CompanyShortName) + "</a></p>" + System.Environment.NewLine);


        return sb.ToString();

    }
    private string constraintLettersGeneric(int edition)
    {
        string letras = "";
        string[] lets = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
        int cont = 0;
        foreach (string aRow in lets)
        {
            cont++;
            //Get Products from Database:
            DEACITableAdapters.GenericsTableAdapter genAdp = new DEACITableAdapters.GenericsTableAdapter();
            DEACI.GenericsDataTable genTable = genAdp.getSectionsByLetter(aRow, edition);

            if (genTable.Rows.Count > 0)
            {
                letras = letras + "\n" + "<th width='20' height='20' scope='col'><a href='" + aRow + "-gen.htm' title='" + aRow + "' target='adentro'><img src='../letras/" + aRow + "1.gif' name='Image1" + cont + "' border='0' onMouseOver='MM_swapImage('Image1" + cont + "','','../letras/" + aRow + "2.gif',1)' onMouseOut='MM_swapImgRestore()'></a></th>";

            }
            else
            {
                letras = letras + "\n" + "<th width='20' height='20' scope='col'><a href='faltante.htm' title='" + aRow + "' target='adentro'><img src='../letras/" + aRow + "1.gif' name='Image1" + cont + "' border='0' onMouseOver='MM_swapImage('Image1" + cont + "','','../letras/" + aRow + "2.gif',1)' onMouseOut='MM_swapImgRestore()'></a></th>"; ;
            }

        }

        return letras;
    }

    private string constraintLettersGenericNComercial(int edition)
    {
        string letras = "";
        string[] lets = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
        int cont = 0;
        foreach (string aRow in lets)
        {
            cont++;
            //Get Products from Database:
            DEACITableAdapters.ProductsNCTableAdapter genAdp = new DEACITableAdapters.ProductsNCTableAdapter();
            DEACI.ProductsNCDataTable genTable = genAdp.getProducts1(edition, aRow);

            if (genTable.Rows.Count > 0)
            {
                letras = letras + "\n" + "<th width='20' height='20' scope='col'><a href='" + aRow + "-gral.htm' title='" + aRow + "' target='adentro'><img src='../letras/" + aRow + "1.gif' name='Image1" + cont + "' border='0' onMouseOver='MM_swapImage('Image1" + cont + "','','../letras/" + aRow + "2.gif',1)' onMouseOut='MM_swapImgRestore()'></a></th>";

            }
            else
            {
                letras = letras + "\n" + "<th width='20' height='20' scope='col'><a href='faltante.htm' title='" + aRow + "' target='adentro'><img src='../letras/" + aRow + "1.gif' name='Image1" + cont + "' border='0' onMouseOver='MM_swapImage('Image1" + cont + "','','../letras/" + aRow + "2.gif',1)' onMouseOut='MM_swapImgRestore()'></a></th>"; ;
            }

        }

        return letras;
    }

    private string constraintLettersBrands(int edition)
    {
        string letras = "";
        string[] lets = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
        int cont = 0;
        foreach (string aRow in lets)
        {
            cont++;
            //Get Products from Database:
            DEACITableAdapters.BrandsTableAdapter branAdp = new DEACITableAdapters.BrandsTableAdapter();
            DEACI.BrandsDataTable branTable = branAdp.getBrands(edition, aRow);

            if (branTable.Rows.Count > 0)
            {
                letras = letras + "\n" + "<th width='20' height='20' scope='col'><a href='" + aRow + "-marca.htm' title='" + aRow + "' target='adentro'><img src='../letras/" + aRow + "1.gif' name='Image1" + cont + "' border='0' onMouseOver='MM_swapImage('Image1" + cont + "','','../letras/" + aRow + "2.gif',1)' onMouseOut='MM_swapImgRestore()'></a></th>";

            }
            else
            {
                letras = letras + "\n" + "<th width='20' height='20' scope='col'><a href='faltante.htm' title='" + aRow + "' target='adentro'><img src='../letras/" + aRow + "1.gif' name='Image1" + cont + "' border='0' onMouseOver='MM_swapImage('Image1" + cont + "','','../letras/" + aRow + "2.gif',1)' onMouseOut='MM_swapImgRestore()'></a></th>"; ;
            }

        }

        return letras;
    }



 

    #endregion

}