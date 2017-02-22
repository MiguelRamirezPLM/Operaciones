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
/// <summary>
/// Descripción breve de IndLetter
/// </summary>
public class IndLetter : GenerateHTML
{ 
	#region Constructors
 
		public IndLetter(string sourcePath, string destinationPath) : base(sourcePath, destinationPath) { }

    #endregion

        #region Methods

    public override void getHtmlFiles(int editionId)
    {
        throw new Exception("The method or operation is not implemented.");
    }

   
    public void getHtmlFilesInd(int editionId, string title)
    { 
    
        //get Alphabet from Database:
        
       PEVTableAdapters.AlphabetTableAdapter alphabetAdp = new PEVTableAdapters.AlphabetTableAdapter();
       PEV.AlphabetDataTable alphabetTable = alphabetAdp.getSpecieAlphabet(editionId);


       foreach (DataRow aRow in alphabetTable.Rows)
       {

           PEV.AlphabetRow alphabetRow = (PEV.AlphabetRow)aRow;


           //Get Products from Database:
           PEVTableAdapters.SpeciesTableAdapter spAdp = new PEVTableAdapters.SpeciesTableAdapter();
           PEV.SpeciesDataTable spTable = spAdp.getSpecies(editionId, alphabetRow[0].ToString());
           //Get the html file template to save the correspond letter html file

           StringBuilder sbHtmlTemplete = new StringBuilder(this.getHtmlTemplate());
           sbHtmlTemplete.Replace("@@TituloIndice@@", this.replaceAccentsToHtmlCodes(title.ToUpper()));

           foreach (DataRow spRow in spTable.Rows)
           {
               PEV.SpeciesRow speciesRow = (PEV.SpeciesRow)spRow;

               sbHtmlTemplete.Append(this.speciesLink(speciesRow));

               this.getProdSpecies(Convert.ToInt32(speciesRow["specieId"]), editionId, speciesRow["specieName"].ToString());
           }


           sbHtmlTemplete.Append("</td></tr></table>");

           sbHtmlTemplete.Replace(".. <i>", ". <i>");
           sbHtmlTemplete.Replace(". <i>Sin Asignar</i>", "");
           sbHtmlTemplete.Replace("..</b>", ".</b>");
           sbHtmlTemplete.Replace(" .</b>", ".</b>");
           sbHtmlTemplete.Replace(".</b> . <i>", ".</b>  <i>");


           sbHtmlTemplete.Append("</div></body></html>");
           sbHtmlTemplete.Replace("@@@specie@@@", "");
           sbHtmlTemplete.Replace("@@@letters@@@",constraintLettersSpec(editionId));
           this.saveHtmlFile(alphabetRow.LETTER + "-specie.htm", sbHtmlTemplete.ToString());
        

       }

       }

    public void getProdSpecies(int specieId, int editionId, string title)
    { 
    
        //Get species from database

        PEVTableAdapters.ProductBySpecieTableAdapter prodSpeAdapter = new PEVTableAdapters.ProductBySpecieTableAdapter();
        PEV.ProductBySpecieDataTable prodSpecTable;


        if (title == "INCUBADORAS" || title == "INSTALACIONES PECUARIAS" || title == "INSTALACIONES Y EQUIPO")
        {
            prodSpecTable = prodSpeAdapter.getProductsByEquipment((byte)specieId, editionId);
            
        }
        else
        {
            prodSpecTable = prodSpeAdapter.getProductsBySpecie((byte)specieId, editionId);
        }
            

        // Get the html file template to save the correspond letter html file:

        StringBuilder sbHtmlTemplate = new StringBuilder(this.getHtmlTemplate());
        sbHtmlTemplate.Replace("@@@TituloIndice@@@", this.replaceAccentsToHtmlCodes(title.ToUpper()));

        foreach (DataRow prdSpRow in prodSpecTable.Rows)
        {

            PEV.ProductBySpecieRow prodSpeRow = (PEV.ProductBySpecieRow)prdSpRow;

            sbHtmlTemplate.Append(this.productSpecLink(prodSpeRow));
        
        }
        sbHtmlTemplate.Append("\r\n</table>");

        sbHtmlTemplate.Replace(".. <i>", ". <i>");
        sbHtmlTemplate.Replace(". <i>Sin Asignar</i>", "");
        sbHtmlTemplate.Replace("..</b>", ".</b>");
        sbHtmlTemplate.Replace(" .</b>", ".</b>");
        sbHtmlTemplate.Replace(".</b> . <i>", ".</b>  <i>");


        sbHtmlTemplate.Append("\r\n</div></body></html>");
        sbHtmlTemplate.Replace("@@@specie@@@", title);
        sbHtmlTemplate.Replace("@@@letters@@@", constraintLettersSpec(editionId));
        // Save the htm file:

        if (title == "INCUBADORAS" || title == "INSTALACIONES PECUARIAS" || title == "INSTALACIONES Y EQUIPO")
        {
            this.saveHtmlFile(specieId + "_e.htm", sbHtmlTemplate.ToString());

        }
        else
        {
            this.saveHtmlFile(specieId + ".htm", sbHtmlTemplate.ToString());
        }

        
    }

    public void getHtmlFilesIndPro(int editionId, string title)
    { 
    
        //get Alphabet from Database:

        PEVTableAdapters.AlphabetTableAdapter alphabetAdp = new PEVTableAdapters.AlphabetTableAdapter();
        PEV.AlphabetDataTable alphabetTable = alphabetAdp.getAlphabet(editionId);


        foreach (DataRow aRow in alphabetTable.Rows)
        {

            PEV.AlphabetRow alphabetRow = (PEV.AlphabetRow)aRow;

            //Get Products from Database:
            PEVTableAdapters.ProductsTableAdapter prodsAdp = new PEVTableAdapters.ProductsTableAdapter();

            PEV.ProductsDataTable prodTable = prodsAdp.getProductsByLetter(editionId, alphabetRow.LETTER);

           // Get the html file template to save the correspond letter html file:

            StringBuilder sbHtmlTempale = new StringBuilder(this.getHtmlTemplate());
            sbHtmlTempale.Replace("@@@TituloIndice@@@", this.replaceAccentsToHtmlCodes(title.ToUpper()));

            foreach (DataRow prodRow in prodTable.Rows)
            {

                PEV.ProductsRow productRow = (PEV.ProductsRow)prodRow;

                sbHtmlTempale.Append(this.productLink(productRow));
            }


            sbHtmlTempale.Replace(".. <i>", ". <i>");
            sbHtmlTempale.Replace(". <i>Sin Asignar</i>", "");
            sbHtmlTempale.Replace("..</b>", ".</b>");
            sbHtmlTempale.Replace(" .</b>", ".</b>");
            sbHtmlTempale.Replace(".</b> . <i>", ".</b>  <i>");

            sbHtmlTempale.Append("\r\n</td></tr></table>");

            sbHtmlTempale.Append("\r\n</div></body></html>");

            // Save the htm file:
            sbHtmlTempale.Replace("@@@letters@@@", constraintLettersProds(editionId));
            this.saveHtmlFile(alphabetRow.LETTER + "-gral.htm", sbHtmlTempale.ToString());

        }

      
    }

    public void getHtmlFilesIndNutrPro(int editionId, string title)
    {
        //get Alphabet from Database:

        PEVTableAdapters.AlphabetTableAdapter alphabetAdp = new PEVTableAdapters.AlphabetTableAdapter();
        PEV.AlphabetDataTable alphabetTable = alphabetAdp.getAlphabetNutr(editionId);


        foreach (DataRow aRow in alphabetTable.Rows)
        {

            PEV.AlphabetRow alphabetRow = (PEV.AlphabetRow)aRow;

            //Get Products from Database:
            PEVTableAdapters.ProductsTableAdapter prodsAdp = new PEVTableAdapters.ProductsTableAdapter();

            PEV.ProductsDataTable prodTable = prodsAdp.getNutrProdsByLetter(editionId, alphabetRow.LETTER);

            // Get the html file template to save the correspond letter html file:

            StringBuilder sbHtmlTempale = new StringBuilder(this.getHtmlTemplate());
            sbHtmlTempale.Replace("@@@TituloIndice@@@", this.replaceAccentsToHtmlCodes(title.ToUpper()));

            foreach (DataRow prodRow in prodTable.Rows)
            {

                PEV.ProductsRow productRow = (PEV.ProductsRow)prodRow;

                sbHtmlTempale.Append(this.productLink(productRow));
            }

            sbHtmlTempale.Append("\r\n</td></tr></table>");

            sbHtmlTempale.Replace(".. <i>", ". <i>");
            sbHtmlTempale.Replace(". <i>Sin Asignar</i>", "");
            sbHtmlTempale.Replace("..</b>", ".</b>");
            sbHtmlTempale.Replace(" .</b>", ".</b>");
            sbHtmlTempale.Replace(".</b> . <i>", ".</b>  <i>");


            sbHtmlTempale.Append("\r\n</div></body></html>");

            sbHtmlTempale.Replace("@@@letters@@@", this.replaceAccentsToHtmlCodes(constraintLettersNutr(editionId)));
            // Save the htm file:
            this.saveHtmlFile(alphabetRow.LETTER + "-nutr.htm", sbHtmlTempale.ToString());

        }
    }
    public void getHtmlFilesIndVacun(int editionId, string title) {

        PEVTableAdapters.AlphabetTableAdapter alphabetAdp = new PEVTableAdapters.AlphabetTableAdapter();
        PEV.AlphabetDataTable alphabetTable = alphabetAdp.GetVacunAlphabet(editionId);
        StringBuilder sbHtmlTempale = new StringBuilder(this.getHtmlTemplate());
        sbHtmlTempale.Replace("@@@TituloIndice@@@", this.replaceAccentsToHtmlCodes(title.ToUpper()));
        foreach (DataRow aRow in alphabetTable.Rows)
        {
            PEV.AlphabetRow alphabetRow = (PEV.AlphabetRow)aRow;
            PEVTableAdapters.ActiveSubstancesTableAdapter actsubs = new PEVTableAdapters.ActiveSubstancesTableAdapter();
            PEV.ActiveSubstancesDataTable acts = actsubs.getSubstancesVacunation(editionId, alphabetRow.LETTER);
            foreach (DataRow sub in acts)
            {
            PEV.ActiveSubstancesRow SubsRow = (PEV.ActiveSubstancesRow)sub;
            sbHtmlTempale.Append("<p class=\"vacname\">"+SubsRow.ACTIVESUBSTANCE.ToUpper()+"</p>");
            
            PEVTableAdapters.ProductsTableAdapter prodsAdp = new PEVTableAdapters.ProductsTableAdapter();
            PEV.ProductsDataTable prodTable = prodsAdp.getVacunProducts(editionId, SubsRow.ID);
            foreach (DataRow prodRow in prodTable.Rows)
            {

                PEV.ProductsRow productRow = (PEV.ProductsRow)prodRow;
                sbHtmlTempale.Append("<div id=" + productRow.ProductId + " style=\"text-align:left; display:block;\" class=\"VacProd\">");
                sbHtmlTempale.Append("<blockquote>");
                sbHtmlTempale.Append(this.productLink(productRow));
                sbHtmlTempale.Append("</blockquote>");
                sbHtmlTempale.Append("</div>");
            }
            }
            //Get Products from Database:
            

            

            // Get the html file template to save the correspond letter html file:
            

            // Save the htm file:
            }
        sbHtmlTempale.Append("\r\n</div></body></html>");
        this.saveHtmlFile("indVacun.htm", sbHtmlTempale.ToString());


    }

    public void getHtmlFilesIndSubs(int editonId, string title)
    {

        //get Alphabet from Database:

        PEVTableAdapters.AlphabetTableAdapter alphabetAdp = new PEVTableAdapters.AlphabetTableAdapter();
        PEV.AlphabetDataTable alphabetTable = alphabetAdp.getSubAlphabet(editonId);

        foreach (DataRow aRow in alphabetTable.Rows)
        {

            PEV.AlphabetRow alphabetRows = (PEV.AlphabetRow)aRow;

            //Get Substances from Database:
            PEVTableAdapters.ActiveSubstancesTableAdapter subsAdp = new PEVTableAdapters.ActiveSubstancesTableAdapter();
            PEV.ActiveSubstancesDataTable subsTable = subsAdp.getSubstance(editonId, alphabetRows.LETTER);

            // Get the html file template to save the correspond letter html file:

            StringBuilder sbHtmlTempale = new StringBuilder(this.getHtmlTemplate());
            sbHtmlTempale.Replace("@@@TituloIndice@@@", this.replaceAccentsToHtmlCodes(title.ToUpper()));
            
            foreach (DataRow prodSusRow in subsTable.Rows)
            {
                PEV.ActiveSubstancesRow actSubRow = (PEV.ActiveSubstancesRow)prodSusRow;

                sbHtmlTempale.Append(this.sustanciasLink(actSubRow));


                //Get productos from database
                //sbHtmlTempale.Append("\r\n<blockquote>");

                sbHtmlTempale.Append("\r\n<table>");

                
                
                PEVTableAdapters.ProdSus1TableAdapter prodSoloSus = new PEVTableAdapters.ProdSus1TableAdapter();
                PEV.ProdSus1DataTable prodSoloSusTable = prodSoloSus.getProdSusSolo(editonId, actSubRow.ACTIVESUBSTANCE.ToString());


                //// Get the html file template to save the correspond letter html file:

                if (prodSoloSusTable.Rows.Count > 0) 
                {
                    sbHtmlTempale.Append("\r\n<tr><td><span id=\"rptLevel_0_ctl00_solos\" class=\"solos\">SOLOS</span></td></tr>");
               
                    foreach (DataRow prdSoloSusRow in prodSoloSusTable.Rows)
                    {


                        PEV.ProdSus1Row prdSusSoloRow = (PEV.ProdSus1Row)prdSoloSusRow;

                        sbHtmlTempale.Append(this.prdActSubsLink(prdSusSoloRow));
                    }
                }

                PEVTableAdapters.ProdSus2TableAdapter prosCombSusAct = new PEVTableAdapters.ProdSus2TableAdapter();
                PEV.ProdSus2DataTable prodCombSusTable = prosCombSusAct.getProdComb(editonId, actSubRow.ACTIVESUBSTANCE.ToString());

                if (prodCombSusTable.Rows.Count > 0)
                {
                    sbHtmlTempale.Append("\r\n<tr><td><span id=\"rptLevel_0_ctl00_combinados\" class=\"solos\">COMBINADOS</span></td></tr>");

                    foreach (DataRow prdCombSusRow in prodCombSusTable.Rows)
                    {


                        PEV.ProdSus2Row prdSusCombRow = (PEV.ProdSus2Row)prdCombSusRow;

                        sbHtmlTempale.Append(this.prdSusComLink(prdSusCombRow));


                    }
                }

                sbHtmlTempale.Append("\r\n</table>");
                sbHtmlTempale.Append("</div>");
                sbHtmlTempale.Append("\r\n</blockquote>");
            }


            sbHtmlTempale.Replace(".. <i>", ". <i>");
            sbHtmlTempale.Replace(". <i>Sin Asignar</i>", "");
            sbHtmlTempale.Replace("..</b>", ".</b>");
            sbHtmlTempale.Replace(" .</b>", ".</b>");
            sbHtmlTempale.Replace(".</b> . <i>", ".</b>  <i>");

            
            sbHtmlTempale.Append("\r\n</div></body></html>");
            
            // Save the htm file:
            sbHtmlTempale.Replace("@@@letters@@@", constraintLettersSubs(editonId));
            this.saveHtmlFile(alphabetRows.LETTER + "-indsus.htm", sbHtmlTempale.ToString());



        }

    }

    public void getHtmlFilesIndThera(int editionId, string title)
    { 
    
        //get Alphabet from Database:

        PEVTableAdapters.AlphabetTableAdapter alphabetAdp = new PEVTableAdapters.AlphabetTableAdapter();
        PEV.AlphabetDataTable alphabetTable = alphabetAdp.getTheraAlphabet(editionId);

        foreach (DataRow aRow in alphabetTable.Rows)
        {

            PEV.AlphabetRow alphabetRows = (PEV.AlphabetRow)aRow;

            //Get Therapeutic from Database:
         
            PEVTableAdapters.TMPTHERATableAdapter teraAdp = new PEVTableAdapters.TMPTHERATableAdapter();
            PEV.TMPTHERADataTable theraTable = teraAdp.getTheraByLetter(alphabetRows.LETTER);


            // Get the html file template to save the correspond letter html file:

            StringBuilder sbHtmlTempale = new StringBuilder(this.getHtmlTemplate());
            sbHtmlTempale.Replace("@@@TituloIndice@@@", this.replaceAccentsToHtmlCodes(title.ToUpper()));

            foreach (DataRow teraRow in theraTable.Rows)
            {

                PEV.TMPTHERARow theraRow = (PEV.TMPTHERARow)teraRow;

                sbHtmlTempale.Append(this.teraLink(theraRow, editionId));

                
            }


            sbHtmlTempale.Replace(".. <i>", ". <i>");
            sbHtmlTempale.Replace(". <i>Sin Asignar</i>", "");
            sbHtmlTempale.Replace("..</b>", ".</b>");
            sbHtmlTempale.Replace(" .</b>", ".</b>");
            sbHtmlTempale.Replace(".</b> . <i>", ".</b>  <i>");


            sbHtmlTempale.Append("\r\n</td></tr></table>");
            sbHtmlTempale.Append("\r\n</div></body></html>");

            // Save the htm file:
            sbHtmlTempale.Replace("@@@letters@@@", constraintLettersTher(editionId));
            this.saveHtmlFile(alphabetRows.LETTER + "-indatc.htm", sbHtmlTempale.ToString());
        }
    }

    public void getHtmlFilesIndLabs(int editionId, string title, string path)
    {
        //get Alphabet from Database:

        PEVTableAdapters.AlphabetTableAdapter alphabetAdp = new PEVTableAdapters.AlphabetTableAdapter();
        PEV.AlphabetDataTable alphabetTable = alphabetAdp.getLabsAlphabet(editionId);
        foreach (DataRow aRow in alphabetTable.Rows)
        {
            PEV.AlphabetRow alphabetRow = (PEV.AlphabetRow)aRow;

            //Get Products from Database:
            PEVTableAdapters.LaboratoriesTableAdapter labsAdp = new PEVTableAdapters.LaboratoriesTableAdapter();

            PEV.LaboratoriesDataTable labsTable = labsAdp.getLabsByLetter(editionId, alphabetRow.LETTER);

            // Get the html file template to save the correspond letter html file:

            StringBuilder sbHtmlTempale = new StringBuilder(this.getHtmlTemplate());
            sbHtmlTempale.Replace("@@@TituloIndice@@@", this.replaceAccentsToHtmlCodes(title.ToUpper()));


            foreach (DataRow labRow in labsTable.Rows)
            {
                
                PEV.LaboratoriesRow laboratoryRow = (PEV.LaboratoriesRow)labRow;
                
                sbHtmlTempale.Append(this.labsLink(laboratoryRow));

                this.getHtmlLabs(editionId, laboratoryRow, path);



            }

            sbHtmlTempale.Append("\r\n</td></tr></table>");

            sbHtmlTempale.Append("\r\n</div></body></html>");

            // Save the htm file:
            sbHtmlTempale.Replace("@@@letters@@@",constraintLettersLabs(editionId));
            this.saveHtmlFile("indlab" + alphabetRow.LETTER.ToLower() + ".htm", sbHtmlTempale.ToString());


        }


    }

    private string constraintLettersLabs(int edition) {
        string letras ="";
        string[] lets = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
        int cont = 0;
        foreach (string aRow in lets)
        {
            cont++;
            //Get Products from Database:
            PEVTableAdapters.LaboratoriesTableAdapter labsAdp = new PEVTableAdapters.LaboratoriesTableAdapter();

            PEV.LaboratoriesDataTable labsTable = labsAdp.getLabsByLetter(edition, aRow);

            if (labsTable.Rows.Count > 0)
	         {
		        letras=letras+"\n"+"<th width='20' height='20' scope='col'><a href='indlab"+aRow.ToLower()+".htm' title="+aRow+" target='main'><img src='../letras/"+aRow+"1.gif' name='Image1"+cont+"' border='0' onMouseOver='MM_swapImage('Image1"+cont+"','','../letras/"+aRow.ToLower()+"2.gif',1)' onMouseOut='MM_swapImgRestore()'></a></th>";
	         }else
	        {
                letras = letras + "\n" + "<th width='20' height='20' scope='col'><a href='indlabno.htm' title=" + aRow + " target='main'><img src='../letras/" + aRow + "1.gif' name='Image1"+cont+"' border='0' onMouseOver='MM_swapImage('Image1"+cont+"','','../letras/"+aRow.ToLower()+"2.gif',1)' onMouseOut='MM_swapImgRestore()'></a></th>";
	        }
            
        }
        
        return letras;
    }
    private string constraintLettersTher(int edition)
    {
        string letras = "";
        string[] lets = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
        System.Collections.Generic.List<string> s = new System.Collections.Generic.List<string>(lets);
        System.Collections.Generic.List<string> s2 = new System.Collections.Generic.List<string>();
        int cont = 0;
            
            //Get Products from Database:
            PEVTableAdapters.AlphabetTableAdapter alphabetAdp = new PEVTableAdapters.AlphabetTableAdapter();
            PEV.AlphabetDataTable alphabetTable = alphabetAdp.getTheraAlphabet(edition);
            foreach (DataRow aRo in alphabetTable)
            {
                PEV.AlphabetRow alphabetRows = (PEV.AlphabetRow)aRo;
                s2.Add(alphabetRows.LETTER);
            }
            foreach (string letter  in s)
	        {
                cont++;
                 
            if (s2.Contains(letter)){
                letras = letras + "\n" + "<th width='20' height='20' scope='col'><a href='" + letter + "-indatc.htm' title='" + letter + "' target='main'><img src='../letras/" + letter + "1.gif' name='Image1" + cont + "'  onMouseOver='MM_swapImage('Image1" + cont + "','','../letras/" + letter + "2.gif',1)' onMouseOut='MM_swapImgRestore()' border='0'></a></th>";
            }
            else
            {
                letras = letras + "\n" + "<th width='20' height='20' scope='col'><a href='indatcno.htm' title='" + letter + "' target='main'><img src='../letras/" + letter + "1.gif' name='Image1" + cont + "'  onMouseOver='MM_swapImage('Image1" + cont + "','','../letras/" + letter + "2.gif',1)' onMouseOut='MM_swapImgRestore()' border='0'></a></th>";
            }

	        }

        return letras;
    }

    private string constraintLettersProds(int edition)
    {
        string letras = "";
        string[] lets = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
        int cont = 0;
        foreach (string aRow in lets)
        {
            cont++;
            //Get Products from Database:
            PEVTableAdapters.ProductsTableAdapter prodsAdp = new PEVTableAdapters.ProductsTableAdapter();

            PEV.ProductsDataTable prodTable = prodsAdp.getProductsByLetter(edition, aRow);
            if (prodTable.Rows.Count > 0)
            {
                letras = letras + "\n" + "<th width='20' height='20' scope='col'><a href='"+aRow+"-gral.htm' title='"+aRow+"' target='main'><img src='../letras/"+aRow+"1.gif' name='Image1"+cont+"'  onMouseOver='MM_swapImage('Image1"+cont+"','','../letras/"+aRow+"2.gif',1)' onMouseOut='MM_swapImgRestore()' border='0'></a></th>";
                
            }
            else
            {
                letras = letras + "\n" + "<th width='20' height='20' scope='col'><a href='indprno.htm' title=" + aRow + " target='main'><img src='../letras/" + aRow + "1.gif' name='Image1" + cont + "' border='0' onMouseOver='MM_swapImage('Image1" + cont + "','','../letras/" + aRow.ToLower() + "2.gif',1)' onMouseOut='MM_swapImgRestore()'></a></th>";
            }

        }

        return letras;
    }
    private string constraintLettersSubs(int edition)
    {
        string letras = "";
        string[] lets = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
        int cont = 0;
        foreach (string aRow in lets)
        {
            cont++;
            //Get Products from Database:
            PEVTableAdapters.ActiveSubstancesTableAdapter subsAdp = new PEVTableAdapters.ActiveSubstancesTableAdapter();
            PEV.ActiveSubstancesDataTable subsTable = subsAdp.getSubstance(edition, aRow);
            if (subsTable.Rows.Count > 0)
            {
                letras = letras + "\n" + "<th width='20' height='20' scope='col'><a href='"+aRow+"-indsus.htm' title='"+aRow+"' target='main'><img src='../letras/"+aRow+"1.gif' name='Image1"+cont+"' border='0' onMouseOver='MM_swapImage('Image1"+cont+"','','../letras/"+aRow+"2.gif',1)' onMouseOut='MM_swapImgRestore()'></a></th>";

            }
            else
            {
                letras = letras + "\n" + "<th width='20' height='20' scope='col'><a href='indsusno.htm' title='" + aRow + "' target='main'><img src='../letras/" + aRow + "1.gif' name='Image1" + cont + "' border='0' onMouseOver='MM_swapImage('Image1" + cont + "','','../letras/" + aRow + "2.gif',1)' onMouseOut='MM_swapImgRestore()'></a></th>"; ;
            }

        }

        return letras;
    }
    private string constraintLettersSpec(int edition)
    {
        string letras = "";
        string[] lets = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
        int cont = 0;
        foreach (string aRow in lets)
        {
            cont++;
            //Get Products from Database:
            PEVTableAdapters.SpeciesTableAdapter spAdp = new PEVTableAdapters.SpeciesTableAdapter();
            PEV.SpeciesDataTable spTable = spAdp.getSpecies(edition, aRow);
            if (spTable.Rows.Count > 0)
            {
                letras = letras + "\n" + " <th width='20' height='20' scope='col'><a href='" + aRow + "-specie.htm' title='" + aRow + "' target='main'><img src='../letras/" + aRow + "1.gif' name='Image1" + cont + "'  onMouseOver='MM_swapImage('Image1" + cont + "','','../letras/" + aRow + "2.gif',1)' onMouseOut='MM_swapImgRestore()' border='0'></a></th>";

            }
            else
            {
                letras = letras + "\n" + " <th width='20' height='20' scope='col'><a href='indspeno.htm' title='" + aRow + "' target='main'><img src='../letras/" + aRow + "1.gif' name='Image1" + cont + "'  onMouseOver='MM_swapImage('Image1" + cont + "','','../letras/" + aRow + "2.gif',1)' onMouseOut='MM_swapImgRestore()' border='0'></a></th>";
            }

        }

        return letras;
    }
    private string constraintLettersNutr(int edition)
    {
        string letras = "";
        string[] lets = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
        int cont = 0;
        foreach (string aRow in lets)
        {
            cont++;
            //Get Products from Database:
            PEVTableAdapters.ProductsTableAdapter prodsAdp = new PEVTableAdapters.ProductsTableAdapter();
            PEV.ProductsDataTable prodTable = prodsAdp.getNutrProdsByLetter(edition, aRow);
            if (prodTable.Rows.Count > 0)
            {
                letras = letras + "\n" + "<th width='20' height='20' scope='col'><a href='" + aRow + "-nutr.htm' title='" + aRow + "' target='main'><img src='../letras/" + aRow + "1.gif' name='Image1" + cont + "'  onMouseOver='MM_swapImage('Image11','','../letras/" + aRow + "2.gif',1)' onMouseOut='MM_swapImgRestore()' border='0'></a></th>";

            }
            else
            {
                letras = letras + "\n" + "<th width='20' height='20' scope='col'><a href='indmarno.htm' title='" + aRow + "' target='main'><img src='../letras/" + aRow + "1.gif' name='Image1" + cont + "'  onMouseOver='MM_swapImage('Image11','','../letras/" + aRow + "2.gif',1)' onMouseOut='MM_swapImgRestore()' border='0'></a></th>";
            }

        }

        return letras;
    }

    ////////////////////////////////////////////////////////////////// INDICES NUEVA PLANTILLA //////////////////////////////////////////////////////////////////

    /////////////////////////// INDICE DE LABORATORIOS Y TERCERAS ///////////////////////////

    public void getHtmlFilesIndLabsN(int editionId, string title, string path)
    {
        //get Alphabet from Database:

        PEVTableAdapters.AlphabetTableAdapter alphabetAdp = new PEVTableAdapters.AlphabetTableAdapter();
        PEV.AlphabetDataTable alphabetTable = alphabetAdp.getLabsAlphabet(editionId);

        foreach (DataRow aRow in alphabetTable.Rows)
        {
            PEV.AlphabetRow alphabetRow = (PEV.AlphabetRow)aRow;

            //Get Products from Database:
            PEVTableAdapters.LaboratoriesTableAdapter labsAdp = new PEVTableAdapters.LaboratoriesTableAdapter();

            PEV.LaboratoriesDataTable labsTable = labsAdp.getLabsByLetter(editionId, alphabetRow.LETTER);

            // Get the html file template to save the correspond letter html file:

            StringBuilder sbHtmlTempale = new StringBuilder(this.getHtmlTemplate());
            sbHtmlTempale.Replace("@@@TituloIndice@@@", this.replaceAccentsToHtmlCodes(title.ToUpper()));


            foreach (DataRow labRow in labsTable.Rows)
            {

                PEV.LaboratoriesRow laboratoryRow = (PEV.LaboratoriesRow)labRow;

                sbHtmlTempale.Append(this.labsLinkN(laboratoryRow));

                this.getHtmlLabsN(editionId, laboratoryRow, path);

            }

            sbHtmlTempale.Append(" ");

          //  sbHtmlTempale.Append("\r\n</td></tr></table>");


            sbHtmlTempale.Append("\r\n </article> \n </div>\n </body></html>");

            // Save the htm file:
            sbHtmlTempale.Replace("@@@letters@@@", constraintLettersLabsN(editionId));

            sbHtmlTempale.Replace("@@@let@@@",  alphabetRow[0].ToString());
            

            this.saveHtmlFile("indlab" + alphabetRow.LETTER.ToLower() + ".htm", sbHtmlTempale.ToString()); 
        } 

    }


    protected string labsLinkN(PEV.LaboratoriesRow labRow)
    {
        StringBuilder sb = new StringBuilder();

        sb.Append("<p class=\"nombre-Lab\"><a href=\"./" + labRow.DivisionId.ToString() + ".htm\" >" +
            this.replaceAccentsToHtmlCodes(labRow.DivisionName.ToUpper()) + "</a></p>" + System.Environment.NewLine);

        return sb.ToString();
    }

    private void getHtmlLabsN(int editionId, PEV.LaboratoriesRow labRow, string fileTemplate)
    {
        StringBuilder sbHtmlTemplete = new StringBuilder(this.getHtmlTemplate(fileTemplate));


        sbHtmlTemplete.Append("<div class=\"NomLab\">" + labRow.DivisionName.ToUpper() + Environment.NewLine + "</div> \n <br /> \n </td></tr>" + Environment.NewLine);


       // sbHtmlTemplete.Append("<table id=\"table2\" width=\"800\" >" + Environment.NewLine + "<tr>" + Environment.NewLine + "<td width=\"80%\">" + Environment.NewLine);

        sbHtmlTemplete.Append( "<tr>" + Environment.NewLine + "<td width=\"80%\">" + Environment.NewLine);

        sbHtmlTemplete.Append(this.getInformationN(labRow.DivisionId) + Environment.NewLine);

        sbHtmlTemplete.Append("  " + Environment.NewLine);
        sbHtmlTemplete.Append("<td width=\"20%\" align=\"Center\">@@@image@@@</td>" + Environment.NewLine + "</tr>" + Environment.NewLine + "\n </table> </td> \n </tr> \n <tr> \n <td>" + Environment.NewLine);

        sbHtmlTemplete.Append(" <div style=\"text-align:center; padding-bottom:15px; padding-top:15px;\" > <hr style=\"width:90%; border: 1px solid grey; \">" + Environment.NewLine + " </div> ");

        sbHtmlTemplete.Append("</td></tr> " + Environment.NewLine);

        sbHtmlTemplete.Append(" \n </table>" + Environment.NewLine);

      //  sbHtmlTemplete.Append("<tr><td>" + Environment.NewLine);
    //  sbHtmlTemplete.Append("<div style=\"margin-left:60px; margin-right:10px; width:95%;    line-height:25px;\">" + Environment.NewLine);



        PEVTableAdapters.CategoriesTableAdapter catAdp = new PEVTableAdapters.CategoriesTableAdapter();
        PEV.CategoriesDataTable catTable = catAdp.getCategoriesByDivision(editionId, labRow.DivisionId);

        foreach (DataRow catRow in catTable.Rows)
        {
            PEV.CategoriesRow categoryRow = (PEV.CategoriesRow)catRow;

            //sbHtmlTemplete.Append("<tr><td>" + Environment.NewLine);

            sbHtmlTemplete.Append("  \n <div>" + Environment.NewLine);

            sbHtmlTemplete.Append("<p class=\"Lab_division\">" + categoryRow.CategoryName.ToUpper() + "</p>" + Environment.NewLine);


            sbHtmlTemplete.Append("</div>" + Environment.NewLine);

            //sbHtmlTemplete.Append("</td></tr>" + Environment.NewLine);

            PEVTableAdapters.ProductsTableAdapter prodAdp = new PEVTableAdapters.ProductsTableAdapter();
            PEV.ProductsDataTable prodTable = prodAdp.getProductsByDivision(editionId, labRow.DivisionId, categoryRow.CategoryId);

            sbHtmlTemplete.Append("<div style=\"margin-left:60px; margin-right:10px; width:95%;    line-height:30px;\">" + Environment.NewLine);

            foreach (DataRow prodRow in prodTable.Rows)
            {
                PEV.ProductsRow productRow = (PEV.ProductsRow)prodRow;

                sbHtmlTemplete.Append(" " + Environment.NewLine);
                

                if (productRow.TYPEINEDITION == 1)
                {
                    sbHtmlTemplete.Append("<a style=\"TEXT-DECORATION: none; \" target=\"_self\" class=\"Lab_productoLink\" href=\"../productos/" + productRow.ProductId.ToString() + "_" + productRow.PharmaFormId.ToString() + "_" + productRow.DivisionId + ".htm\">");
                    sbHtmlTemplete.Append("<b>* " + productRow.ProductName.ToUpper() + ".</b> ");
                    sbHtmlTemplete.Append(productRow.ProductDescription + ". <i>" + productRow.PharmaForm + "</i></a> \n <br>");
                }
                else
                {
                    sbHtmlTemplete.Append("<span class=\"Lab_productoNoLink\">");
                    sbHtmlTemplete.Append("<b> " + productRow.ProductName.ToUpper() + ".</b> ");
                    sbHtmlTemplete.Append(productRow.ProductDescription + ". <i>" + productRow.PharmaForm + "</i></span> \n <br>");
                }

                //sbHtmlTemplete.Append(" </td></tr>" + Environment.NewLine);

                sbHtmlTemplete.Append("   " + Environment.NewLine);

            }

            sbHtmlTemplete.Append("  \n </div>" + Environment.NewLine);
        }

        sbHtmlTemplete.Append("   ");

        PEVTableAdapters.AdverstmentTableAdapter advAdp = new PEVTableAdapters.AdverstmentTableAdapter();
        PEV.AdverstmentDataTable advTbl = advAdp.GetAdverstmentByDivByEdition(labRow.DivisionId, editionId);
        string adv = "<div align=\"center\">";
        foreach (DataRow dr in advTbl.Rows)
        {
            PEV.AdverstmentRow adRow = (PEV.AdverstmentRow)dr;
            adv += "<img src='" + adRow.BaseUrl + adRow.AdFile + "'><br/>";
        }
        adv += "</div> ";
        sbHtmlTemplete.Append("\r\n ");
        sbHtmlTemplete.Append(" ");
        sbHtmlTemplete.Append(adv);
        sbHtmlTemplete.Append("</body></html>");
        sbHtmlTemplete.Replace("@@@image@@@", getImageDivisionN(labRow.DivisionId));
        sbHtmlTemplete.Replace(".. <i>", ". <i>");
        sbHtmlTemplete.Replace(". <i>Sin Asignar</i>", "");
        sbHtmlTemplete.Replace("..</b>", ".</b>");
        sbHtmlTemplete.Replace(" .</b>", ".</b>");
        sbHtmlTemplete.Replace(".</b> . <i>", ".</b>  <i>");
        // Save the htm file:
        this.saveHtmlFile(labRow.DivisionId.ToString() + ".htm", sbHtmlTemplete.ToString());

    }

    private string getInformationN(int divisionId)
    {
        StringBuilder sb = new StringBuilder();

        PEVTableAdapters.DivisionInformationTableAdapter divInfAdp = new PEVTableAdapters.DivisionInformationTableAdapter();
        PEV.DivisionInformationDataTable divInfTable = divInfAdp.getAddressByDivision(divisionId);

        foreach (DataRow aRow in divInfTable.Rows)
        {
            PEV.DivisionInformationRow divRow = (PEV.DivisionInformationRow)aRow;

            sb.Append(this.setInformationN(divRow));

            PEV.DivisionInformationDataTable branchRows = divInfAdp.getAddressByParent(divRow.DivisionId);

            foreach (DataRow sRow in branchRows)
            {
                PEV.DivisionInformationRow branchRow = (PEV.DivisionInformationRow)sRow;

                //if (!branchRow.IsTextNull())
                sb.Append("<p class=\"Lab_Datos\"><b>" + sRow[15].ToString() + "</b></p>" + Environment.NewLine);

                sb.Append(this.setInformationN(branchRow));

            }

        }

        return sb.ToString();
    }

    private string setInformationN(PEV.DivisionInformationRow divRow)
    {
        StringBuilder sb = new StringBuilder();

        sb.Append("<table width=\"100%\">" + Environment.NewLine + "<tr>" + Environment.NewLine + "<td>" + Environment.NewLine);

        sb.Append("<p class=\"Lab_Datos\">" + divRow.Address + "<br />" + Environment.NewLine);

        if (!divRow.IsSuburbNull())
            sb.Append(divRow.Suburb + "<br />" + Environment.NewLine);

        if (!divRow.IsZipCodeNull())
           sb.Append( "<i>Apartado A&eacute;reo: </i>" +divRow.ZipCode + "<br /> ");

        if (!divRow.IsCityNull())
            sb.Append(divRow.City);
        
        if (!divRow.IsStateNull())
            sb.Append(", " + divRow.State + "<br />" + Environment.NewLine);
         

        if (!divRow.IsTelephoneNull())
            sb.Append("<br /> \n <i>Tel.: </i>" + divRow.Telephone + "<br />" + Environment.NewLine);

        if (!divRow.IsFaxNull())
            sb.Append("<i>Fax: </i>" + divRow.Fax + "<br />" + Environment.NewLine);

        if (!divRow.IsLadaNull())
            sb.Append(divRow.Lada + "<br />" + Environment.NewLine);

        if (!divRow.IsEmailNull())
            sb.Append(this.setEmailN(divRow.Email) + "<br />" + Environment.NewLine);

        if (!divRow.IsWebNull())
            sb.Append("<a style=\"color:#333333; font-style:italic; font-weight: bold; font-size:15px;\" href=\"http://" + divRow.Web.Trim() + "\" target=\"_blank\" >" + divRow.Web.Trim() + "</a>" + Environment.NewLine);

        //sb.Append("</p>" + Environment.NewLine + "</td>" + Environment.NewLine + "</tr>" + Environment.NewLine + "</table>" + Environment.NewLine);

        sb.Append("</p>" + Environment.NewLine + "</td>" + Environment.NewLine + " " + Environment.NewLine + " " + Environment.NewLine);

        return sb.ToString();
    }

    private string setEmailN(string email)
    {
        string cadena = "";

        if (email.Contains("/"))
        {
            string[] emails = email.Split('/');

            foreach (string mail in emails)
            {
                cadena = "<a style=\"color:#333333; font-style:italic; font-weight: bold; font-size:15px;\" href=\"mailto:" + mail.Trim() + "\" >" + mail.Trim() + "</a> / " + Environment.NewLine;
            }


        }
        else
            cadena = "<a style=\"color:#333333; font-style:italic; font-weight: bold; font-size:15px;\" href=\"mailto:" + email.Trim() + "\" >" + email.Trim() + "</a> " + Environment.NewLine;


        return cadena;

    }

    private string getImageDivisionN(int division)
    {
        PEVTableAdapters.DivisionInformationTableAdapter divInfAdp = new PEVTableAdapters.DivisionInformationTableAdapter();
        PEV.DivisionInformationDataTable divInfTable = divInfAdp.getAddressByDivision(division);
        string images = "";
        foreach (DataRow aRow in divInfTable.Rows)
        {
            PEV.DivisionInformationRow divRow = (PEV.DivisionInformationRow)aRow;
            if (!(divRow.IsImageNull()))
            {
                char[] sp = { ',' };
                string[] imag = divRow.Image.Split(sp);
                foreach (string value in imag)
                {
                    images += "<img src=\"logos/" + value.Trim() + "\" /><br/>";
                }
            }
        }

        return images;
    }

    private string constraintLettersLabsN(int edition)
    {
        string letras = "";
        string[] lets = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
        int cont = 0;
        foreach (string aRow in lets)
        {
            cont++;
            //Get Products from Database:
            PEVTableAdapters.LaboratoriesTableAdapter labsAdp = new PEVTableAdapters.LaboratoriesTableAdapter();

            PEV.LaboratoriesDataTable labsTable = labsAdp.getLabsByLetter(edition, aRow);

            if (labsTable.Rows.Count > 0)
            {
                letras = letras + "\n" + "<a href=\"indlab" + aRow.ToLower() + ".htm\" title=\"" + aRow + "\"  target=\"carga-info\"><li class=\"letra\"><img src=\"../../img/letra-" + aRow + ".png\" alt=\"\"></li></a> \n";
                    
                    //<img src='../letras/" + aRow + "1.gif' name='Image1" + cont + "' border='0' onMouseOver='MM_swapImage('Image1" + cont + "','','../letras/" + aRow.ToLower() + "2.gif',1)' onMouseOut='MM_swapImgRestore()'></a></th>";
            }
            else
            {
                letras = letras + "\n" + "<a href=\"indlabno.htm\" title=\"" + aRow + "\" target=\"carga-info\"><li class=\"letra\"><img src=\"../../img/letra-" + aRow + ".png\"  alt=\"\"></li></a> \n";
            }

        }

   

        return letras;
    }


    /////////////////////////// INDICE DE GENERAL ///////////////////////////

    public void getHtmlFilesIndProN(int editionId, string title)
    {

        //get Alphabet from Database:

        PEVTableAdapters.AlphabetTableAdapter alphabetAdp = new PEVTableAdapters.AlphabetTableAdapter();
        PEV.AlphabetDataTable alphabetTable = alphabetAdp.getAlphabet(editionId);


        foreach (DataRow aRow in alphabetTable.Rows)
        {

            PEV.AlphabetRow alphabetRow = (PEV.AlphabetRow)aRow;

            //Get Products from Database:
            PEVTableAdapters.ProductsTableAdapter prodsAdp = new PEVTableAdapters.ProductsTableAdapter();

            PEV.ProductsDataTable prodTable = prodsAdp.getProductsByLetter(editionId, alphabetRow.LETTER);

            // Get the html file template to save the correspond letter html file:

            StringBuilder sbHtmlTempale = new StringBuilder(this.getHtmlTemplate());
            sbHtmlTempale.Replace("@@@TituloIndice@@@", this.replaceAccentsToHtmlCodes(title.ToUpper()));

            foreach (DataRow prodRow in prodTable.Rows)
            {

                PEV.ProductsRow productRow = (PEV.ProductsRow)prodRow;

                sbHtmlTempale.Append(this.productLinkN(productRow));
            }


            sbHtmlTempale.Replace(".. <i>", ". <i>");
            sbHtmlTempale.Replace(". <i>Sin Asignar</i>", "");
            sbHtmlTempale.Replace("..</b>", ".</b>");
            sbHtmlTempale.Replace(" .</b>", ".</b>");
            sbHtmlTempale.Replace(".</b> . <i>", ".</b>  <i>");
            sbHtmlTempale.Replace("</b>. . <i>", "</b>. <i>");
            sbHtmlTempale.Replace("<i>Sin Asignar. </i>", " ");
            sbHtmlTempale.Replace("’", "'");
            sbHtmlTempale.Replace("\r\n</div> \n </body></html>", "");
            sbHtmlTempale.Replace(".</b>.", "</b>.");
            sbHtmlTempale.Replace(" </b>.", "</b>.");
             

            sbHtmlTempale.Append("\r\n </article> ");
        

            // Save the htm file:
            sbHtmlTempale.Replace("@@@letters@@@", constraintLettersProdsN(editionId));

            sbHtmlTempale.Replace("@@@let@@@", alphabetRow[0].ToString());

            this.saveHtmlFile(alphabetRow.LETTER + "-gral.htm", sbHtmlTempale.ToString());

        }


    }

    protected string productLinkN(PEV.ProductsRow prodRow)
    {
        StringBuilder sb = new StringBuilder();

        if (prodRow.TYPEINEDITION == 1)
        {
            sb.Append("\r\n<p class=\"productoG\"><a class=\"linksSections\" href=\"../productos/" + prodRow.ProductId + "_" + prodRow.PharmaFormId + "_" +  prodRow.DivisionId + ".htm\" ><b>* " +
                this.replaceAccentsToHtmlCodes(prodRow.ProductName) + "</b>. ");
            if (!prodRow.IsProductDescriptionNull())
            {
                sb.Append(this.replaceAccentsToHtmlCodes(prodRow.ProductDescription) + ". ");
            }

            sb.Append("<i>" +
            this.replaceAccentsToHtmlCodes(prodRow.PharmaForm) + ". </i> <b>" +
            this.replaceAccentsToHtmlCodes(prodRow.DivisionName) + "</b></a></p> \n");

        }
        else
        {
            sb.Append("\r\n<p class=\"prodNoLink\"><b>" + this.replaceAccentsToHtmlCodes(prodRow.ProductName) + "</b>. " +
                this.replaceAccentsToHtmlCodes(prodRow.ProductDescription) + ". <i>" + this.replaceAccentsToHtmlCodes(prodRow.PharmaForm) + ".</i> <b>" + this.replaceAccentsToHtmlCodes(prodRow.DivisionName) + "</b></p> \n");
        }

    
        

        return sb.ToString();
    }

    private string constraintLettersProdsN(int edition)
    {
        string letras = "";
        string[] lets = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
        int cont = 0;
        foreach (string aRow in lets)
        {
            cont++;
            //Get Products from Database:
            PEVTableAdapters.ProductsTableAdapter prodsAdp = new PEVTableAdapters.ProductsTableAdapter();

            PEV.ProductsDataTable prodTable = prodsAdp.getProductsByLetter(edition, aRow);
            if (prodTable.Rows.Count > 0)
            {
                letras = letras + "\n" + "<a href=\"" + aRow + "-gral.htm\" title=\"" + aRow + "\"  target=\"carga-info\"> <li class=\"letra\"><img src=\"../../img/letra-" + aRow + ".png\" alt=\"\"></li></a> \n";

             }
            else
            {
                letras = letras + "\n" + "<a href=\"indprno.htm\" title=" + aRow + " target=\"carga-info\"> <li class=\"letra\"><img src=\"../../img/letra-" + aRow.ToLower() + ".png\" alt=\"\"></li></a> \n";
            }

        }

        return letras;
    }



    /////////////////////////// INDICE DE GENERAL DIVISION PECUARIA ///////////////////////////

    public void getHtmlFilesIndProNDP(int editionId, string title)
    {

        //get Alphabet from Database:

        PEVTableAdapters.AlphabetTableAdapter alphabetAdp = new PEVTableAdapters.AlphabetTableAdapter();
        PEV.AlphabetDataTable alphabetTable = alphabetAdp.getAlphabetDivPec(editionId);


        foreach (DataRow aRow in alphabetTable.Rows) 
        {

            PEV.AlphabetRow alphabetRow = (PEV.AlphabetRow)aRow;

            //Get Products from Database:
            PEVTableAdapters.ProductsTableAdapter prodsAdp = new PEVTableAdapters.ProductsTableAdapter();

            PEV.ProductsDataTable prodTable = prodsAdp.getDivPecProductsLetter(editionId, alphabetRow.LETTER);

            // Get the html file template to save the correspond letter html file:

            StringBuilder sbHtmlTempale = new StringBuilder(this.getHtmlTemplate());
            sbHtmlTempale.Replace("@@@TituloIndice@@@", this.replaceAccentsToHtmlCodes(title.ToUpper()));

            foreach (DataRow prodRow in prodTable.Rows)
            {

                PEV.ProductsRow productRow = (PEV.ProductsRow)prodRow;

                sbHtmlTempale.Append(this.productLinkNDP(productRow));
            }


            sbHtmlTempale.Replace(".. <i>", ". <i>");
            sbHtmlTempale.Replace(". <i>Sin Asignar</i>", "");
            sbHtmlTempale.Replace("..</b>", ".</b>");
            sbHtmlTempale.Replace(" .</b>", ".</b>");
            sbHtmlTempale.Replace(".</b> . <i>", ".</b>  <i>");
            sbHtmlTempale.Replace("</b>. . <i>", "</b>. <i>");
            sbHtmlTempale.Replace("<i>Sin Asignar. </i>", " ");
            sbHtmlTempale.Replace("’", "'");
            sbHtmlTempale.Replace("\r\n</div> \n </body></html>", "");
            sbHtmlTempale.Replace(".</b>.", "</b>.");
            sbHtmlTempale.Replace(" </b>.", "</b>.");




            sbHtmlTempale.Append("\r\n </article> ");


            // Save the htm file:
            sbHtmlTempale.Replace("@@@letters@@@", constraintLettersProdsNDP(editionId));

            sbHtmlTempale.Replace("@@@let@@@", alphabetRow[0].ToString());

            this.saveHtmlFile(alphabetRow.LETTER + "-gral.htm", sbHtmlTempale.ToString());

        }


    }

    protected string productLinkNDP(PEV.ProductsRow prodRow)
    {
        StringBuilder sb = new StringBuilder();

        if (prodRow.TYPEINEDITION == 1)
        {
            sb.Append("\r\n<p class=\"productoG\"><a class=\"linksSections\" href=\"../productos/" + prodRow.ProductId + "_" + prodRow.PharmaFormId + "_" + prodRow.DivisionId + ".htm\" ><b>* " +
                this.replaceAccentsToHtmlCodes(prodRow.ProductName) + "</b>. ");
            if (!prodRow.IsProductDescriptionNull())
            {
                sb.Append(this.replaceAccentsToHtmlCodes(prodRow.ProductDescription) + ". ");
            }

            sb.Append("<i>" +
            this.replaceAccentsToHtmlCodes(prodRow.PharmaForm) + ". </i> <b>" +
            this.replaceAccentsToHtmlCodes(prodRow.DivisionName) + "</b></a></p> \n");

        }
        else
        {
            sb.Append("\r\n<p class=\"prodNoLink\"><b>" + this.replaceAccentsToHtmlCodes(prodRow.ProductName) + "</b>. " +
                this.replaceAccentsToHtmlCodes(prodRow.ProductDescription) + ". <i>" + this.replaceAccentsToHtmlCodes(prodRow.PharmaForm) + ".</i> <b>" + this.replaceAccentsToHtmlCodes(prodRow.DivisionName) + "</b></p> \n");
        }




        return sb.ToString();
    }

    private string constraintLettersProdsNDP(int edition)
    {
        string letras = "";
        string[] lets = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
        int cont = 0;
        foreach (string aRow in lets)
        {
            cont++;
            //Get Products from Database:
            PEVTableAdapters.ProductsTableAdapter prodsAdp = new PEVTableAdapters.ProductsTableAdapter();

            PEV.ProductsDataTable prodTable = prodsAdp.getDivPecProductsLetter(edition, aRow);
            if (prodTable.Rows.Count > 0)
            {
                letras = letras + "\n" + "<a href=\"" + aRow + "-gral.htm\" title=\"" + aRow + "\"  target=\"carga-info\"> <li class=\"letra\"><img src=\"../../img/letra-" + aRow + ".png\" alt=\"\"></li></a> \n";

            }
            else
            {
                letras = letras + "\n" + "<a href=\"indprno.htm\" title=" + aRow + " target=\"carga-info\"> <li class=\"letra\"><img src=\"../../img/letra-" + aRow.ToLower() + ".png\" alt=\"\"></li></a> \n";
            }

        }

        return letras;
    }


    /////////////////////////// INDICE DE VACUNACION ///////////////////////////

    public void getHtmlFilesIndVacunNew(int editionId, string title)
    {

        PEVTableAdapters.AlphabetTableAdapter alphabetAdp = new PEVTableAdapters.AlphabetTableAdapter();
        PEV.AlphabetDataTable alphabetTable = alphabetAdp.GetVacunAlphabet(editionId);
        StringBuilder sbHtmlTempale = new StringBuilder(this.getHtmlTemplate());
        sbHtmlTempale.Replace("@@@TituloIndice@@@", this.replaceAccentsToHtmlCodes(title.ToUpper()));
        foreach (DataRow aRow in alphabetTable.Rows)
        {
            PEV.AlphabetRow alphabetRow = (PEV.AlphabetRow)aRow;
            PEVTableAdapters.ActiveSubstancesTableAdapter actsubs = new PEVTableAdapters.ActiveSubstancesTableAdapter();
            PEV.ActiveSubstancesDataTable acts = actsubs.getSubstancesVacunation(editionId, alphabetRow.LETTER);
            foreach (DataRow sub in acts)
            {
                PEV.ActiveSubstancesRow SubsRow = (PEV.ActiveSubstancesRow)sub;

                sbHtmlTempale.Append("\n <article class=\"grupo-compa\"> \n	<h2 class=\"subtitulo\">" + SubsRow.ACTIVESUBSTANCE.ToUpper() + "</h2> \n <ul class=\"listado-indices\"> \n");
				 

                PEVTableAdapters.ProductsTableAdapter prodsAdp = new PEVTableAdapters.ProductsTableAdapter();
                PEV.ProductsDataTable prodTable = prodsAdp.getVacunProducts(editionId, SubsRow.ID);
                foreach (DataRow prodRow in prodTable.Rows)
                {

                    PEV.ProductsRow productRow = (PEV.ProductsRow)prodRow;
 			 

                    sbHtmlTempale.Append("");
                    sbHtmlTempale.Append("<li>");
                    sbHtmlTempale.Append(this.productLinkVN(productRow));
                   

                }
                    sbHtmlTempale.Append("</ul> \n");
                    sbHtmlTempale.Append("</article> \n");
            }
            //Get Products from Database:
  

            // Get the html file template to save the correspond letter html file:


            // Save the htm file:
        }
        sbHtmlTempale.Append("\r\n</article> \n </div></body></html>");
        this.saveHtmlFile("indVacun.htm", sbHtmlTempale.ToString());


    }

    protected string productLinkVN(PEV.ProductsRow prodRow)
    {
        StringBuilder sb = new StringBuilder();

        if (prodRow.TYPEINEDITION == 1)
        {
            sb.Append("\r\n<p class=\"listado-vacunacion\"><a class=\"linksSections\" href=\"../productos/" + prodRow.ProductId + "_" + prodRow.PharmaFormId + "_" + prodRow.DivisionId + ".htm\" ><b>* " +
                this.replaceAccentsToHtmlCodes(prodRow.ProductName) + "</b>. ");
            if (!prodRow.IsProductDescriptionNull())
            {
                sb.Append(this.replaceAccentsToHtmlCodes(prodRow.ProductDescription) + ". ");
            }

            sb.Append("<i>" +
            this.replaceAccentsToHtmlCodes(prodRow.PharmaForm) + ". </i> <b>" +
            this.replaceAccentsToHtmlCodes(prodRow.DivisionName) + "</b></a></p> \n </li> \n");
        }
        else
        {
            sb.Append("\r\n<p class=\"prodNoLink\"><b>" + this.replaceAccentsToHtmlCodes(prodRow.ProductName) + "</b>. " +
                this.replaceAccentsToHtmlCodes(prodRow.ProductDescription) + ". <i>" + this.replaceAccentsToHtmlCodes(prodRow.PharmaForm) + ".</i> <b>" + this.replaceAccentsToHtmlCodes(prodRow.DivisionName) + "</b></p>");
        }

        
        return sb.ToString();
    }


    /////////////////////////// INDICE DE SUSTANCIAS ///////////////////////////

    public void getHtmlFilesIndSubsN(int editonId, string title)
    {

        //get Alphabet from Database:

        PEVTableAdapters.AlphabetTableAdapter alphabetAdp = new PEVTableAdapters.AlphabetTableAdapter();
        PEV.AlphabetDataTable alphabetTable = alphabetAdp.getSubAlphabet(editonId);

        foreach (DataRow aRow in alphabetTable.Rows)
        {

            PEV.AlphabetRow alphabetRows = (PEV.AlphabetRow)aRow;

            //Get Substances from Database:
            PEVTableAdapters.ActiveSubstancesTableAdapter subsAdp = new PEVTableAdapters.ActiveSubstancesTableAdapter();
            PEV.ActiveSubstancesDataTable subsTable = subsAdp.getSubstance(editonId, alphabetRows.LETTER);

            // Get the html file template to save the correspond letter html file:

            StringBuilder sbHtmlTempale = new StringBuilder(this.getHtmlTemplate());
            sbHtmlTempale.Replace("@@@TituloIndice@@@", this.replaceAccentsToHtmlCodes(title.ToUpper()));

            foreach (DataRow prodSusRow in subsTable.Rows)
            {
                PEV.ActiveSubstancesRow actSubRow = (PEV.ActiveSubstancesRow)prodSusRow;

                sbHtmlTempale.Append(this.sustanciasLinkN(actSubRow));


                //Get productos from database
                //sbHtmlTempale.Append("\r\n<blockquote>");

                sbHtmlTempale.Append("\r\n<div class=\"panel\"> ");



                PEVTableAdapters.ProdSus1TableAdapter prodSoloSus = new PEVTableAdapters.ProdSus1TableAdapter();
                PEV.ProdSus1DataTable prodSoloSusTable = prodSoloSus.getProdSusSolo(editonId, actSubRow.ACTIVESUBSTANCE.ToString());


                //// Get the html file template to save the correspond letter html file:

                if (prodSoloSusTable.Rows.Count > 0)
                {
                    sbHtmlTempale.Append("\r\n <p class=\"tipo-sustancia\">SOLOS</p>\n");

                    foreach (DataRow prdSoloSusRow in prodSoloSusTable.Rows)
                    {


                        PEV.ProdSus1Row prdSusSoloRow = (PEV.ProdSus1Row)prdSoloSusRow;

                        sbHtmlTempale.Append(this.prdActSubsLinkN(prdSusSoloRow));
                    }
                }

                PEVTableAdapters.ProdSus2TableAdapter prosCombSusAct = new PEVTableAdapters.ProdSus2TableAdapter();
                PEV.ProdSus2DataTable prodCombSusTable = prosCombSusAct.getProdComb(editonId, actSubRow.ACTIVESUBSTANCE.ToString());

                if (prodCombSusTable.Rows.Count > 0)
                {
                    sbHtmlTempale.Append("\r\n <p class=\"tipo-sustancia\"> COMBINADOS </p> \n");

                    foreach (DataRow prdCombSusRow in prodCombSusTable.Rows)
                    {


                        PEV.ProdSus2Row prdSusCombRow = (PEV.ProdSus2Row)prdCombSusRow;

                        sbHtmlTempale.Append(this.prdSusComLinkN(prdSusCombRow));


                    }
                }

                sbHtmlTempale.Append("\r\n ");
                sbHtmlTempale.Append("</div>");
                sbHtmlTempale.Append("\r\n ");
            }


            sbHtmlTempale.Replace(".. <i>", ". <i>");
            sbHtmlTempale.Replace(". <i>Sin Asignar</i>", "");
            sbHtmlTempale.Replace("..</b>", ".</b>");
            sbHtmlTempale.Replace(" .</b>", ".</b>");
            sbHtmlTempale.Replace(".</b> . <i>", ".</b>  <i>");

           sbHtmlTempale.Append(" <script> ");
           sbHtmlTempale.Append("\n     var acc = document.getElementsByClassName(\"acordeon\"); ");
           sbHtmlTempale.Append("\n     var i;");
           sbHtmlTempale.Append("\n ");
           sbHtmlTempale.Append("\n     for (i = 0; i < acc.length; i++) { ");
           sbHtmlTempale.Append("\n         acc[i].onclick = function () { ");
           sbHtmlTempale.Append("\n             this.classList.toggle(\"active\"); ");
           sbHtmlTempale.Append("\n             this.nextElementSibling.classList.toggle(\"show\"); ");
           sbHtmlTempale.Append("\n         } ");
           sbHtmlTempale.Append("\n     } ");
           sbHtmlTempale.Append("\n </script> ");

           sbHtmlTempale.Append("\r\n </article> \n </div> \n </body></html>");

            // Save the htm file:
            sbHtmlTempale.Replace("@@@letters@@@", constraintLettersSubsN(editonId));
            sbHtmlTempale.Replace("@@@let@@@", alphabetRows.LETTER);


            this.saveHtmlFile(alphabetRows.LETTER + "-indsus.htm", sbHtmlTempale.ToString());
 
        }

    }

    protected string sustanciasLinkN(PEV.ActiveSubstancesRow actSubRow)
    {
        StringBuilder sb = new StringBuilder();

        sb.Append("  <button class=\"acordeon\"> ");
         

        sb.Append(this.replaceAccentsToHtmlCodes(actSubRow.ACTIVESUBSTANCE) + " ");

        sb.Append("\r\n</button>");


        sb.Append("\r\n ");
       // sb.Append(actSubRow.ID + "_divLevelChildren_0\" style=\"text-align:left; display:none;\">  ");

        return sb.ToString();

    }

    protected string prdActSubsLinkN(PEV.ProdSus1Row prdSusSoloRow)
    {

        StringBuilder sb = new StringBuilder();

        sb.Append(" \n <div class=\"SubstancesProd\"> \n<p class=\"item-acordeonSub\"> ");
        sb.Append("<a class=\"link-itemSub\" href=\"../productos/" + prdSusSoloRow.productid + "_" + prdSusSoloRow.PharmaFormId + "_" + prdSusSoloRow.DivisionId + ".htm\" target=\"_self\" style=\"text-decoration:none;\" >"
        + "<b>" + this.replaceAccentsToHtmlCodes(prdSusSoloRow.nombre) + "</b> <i>" + this.replaceAccentsToHtmlCodes(prdSusSoloRow.PharmaForms) + ".</i> <b>" + this.replaceAccentsToHtmlCodes(prdSusSoloRow.laboratorio) + "</b></a> </p> \n" + System.Environment.NewLine);

       sb.Append("   </div> ");
        return sb.ToString();

    }

    protected string prdSusComLinkN(PEV.ProdSus2Row prdSusComRow)
    {
        StringBuilder sb = new StringBuilder();

        sb.Append("\n <div class=\"SubstancesProd\"> \n <p class=\"item-acordeonSub\"> ");

        sb.Append("<a class=\"link-itemSub\" href=\"../productos/" + prdSusComRow.productid + "_" + prdSusComRow.PharmaFormId + "_" + prdSusComRow.DivisionId + ".htm\"  target=\"_self\" style=\"text-decoration:none; \" >"
             + "<b>" + this.replaceAccentsToHtmlCodes(prdSusComRow.nombre) + "</b> " + this.replaceAccentsToHtmlCodes(prdSusComRow.Sustancias) + ". <i>" + this.replaceAccentsToHtmlCodes(prdSusComRow.PharmaForms) + ".</i> <b> " + this.replaceAccentsToHtmlCodes(prdSusComRow.laboratorio) + "</b></a> </p>\n" + System.Environment.NewLine);

        sb.Append("   </div> ");

        return sb.ToString();

        
    }

    private string constraintLettersSubsN(int edition)
    {
        string letras = "";
        string[] lets = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
        int cont = 0;
        foreach (string aRow in lets)
        {
            cont++;
            //Get Products from Database:
            PEVTableAdapters.ActiveSubstancesTableAdapter subsAdp = new PEVTableAdapters.ActiveSubstancesTableAdapter();
            PEV.ActiveSubstancesDataTable subsTable = subsAdp.getSubstance(edition, aRow);
            if (subsTable.Rows.Count > 0)
            {
                letras = letras + "\n" + "<a href=\"" + aRow + "-indsus.htm\" title=\"" + aRow + "\" target=\"carga-info\"> <li class=\"letra\"><img src=\"../../img/letra-" + aRow + ".png\" alt=\"\"></li></a> \n";

            }
            else
            {
                letras = letras + "\n" + "<a href=\"indsusno.htm\" title=\"" + aRow + "\" target=\"carga-info\"> <li class=\"letra\"><img src=\"../../img/letra-" + aRow + ".png\" alt=\"\"></li></a> \n";
            }

        }

        return letras;
    }

    /////////////////////////// INDICE DE ATC ///////////////////////////

    public void getHtmlFilesIndTheraN(int editionId, string title)
    {

        //get Alphabet from Database:

        PEVTableAdapters.AlphabetTableAdapter alphabetAdp = new PEVTableAdapters.AlphabetTableAdapter();
        PEV.AlphabetDataTable alphabetTable = alphabetAdp.getTheraAlphabet(editionId);

        foreach (DataRow aRow in alphabetTable.Rows)
        {

            PEV.AlphabetRow alphabetRows = (PEV.AlphabetRow)aRow;

            //Get Therapeutic from Database:

            PEVTableAdapters.TMPTHERATableAdapter teraAdp = new PEVTableAdapters.TMPTHERATableAdapter();
            PEV.TMPTHERADataTable theraTable = teraAdp.getTheraByLetter(alphabetRows.LETTER);


            // Get the html file template to save the correspond letter html file:

            StringBuilder sbHtmlTempale = new StringBuilder(this.getHtmlTemplate());
            sbHtmlTempale.Replace("@@@TituloIndice@@@", this.replaceAccentsToHtmlCodes(title.ToUpper()));

            foreach (DataRow teraRow in theraTable.Rows)
            {

                PEV.TMPTHERARow theraRow = (PEV.TMPTHERARow)teraRow;

                sbHtmlTempale.Append(this.teraLinkN(theraRow, editionId));
                 
            }


            sbHtmlTempale.Replace(".. <i>", ". <i>");
            sbHtmlTempale.Replace(". <i>Sin Asignar</i>", "");
            sbHtmlTempale.Replace("..</b>", ".</b>");
            sbHtmlTempale.Replace(" .</b>", ".</b>");
            sbHtmlTempale.Replace(".</b> . <i>", ".</b>  <i>");
            sbHtmlTempale.Replace("..</b>", ".</b>");

            sbHtmlTempale.Append(" <script> ");
            sbHtmlTempale.Append("\n     var acc = document.getElementsByClassName(\"acordeon\"); ");
            sbHtmlTempale.Append("\n     var i;");
            sbHtmlTempale.Append("\n ");
            sbHtmlTempale.Append("\n     for (i = 0; i < acc.length; i++) { ");
            sbHtmlTempale.Append("\n         acc[i].onclick = function () { ");
            sbHtmlTempale.Append("\n             this.classList.toggle(\"active\"); ");
            sbHtmlTempale.Append("\n             this.nextElementSibling.classList.toggle(\"show\"); ");
            sbHtmlTempale.Append("\n         } ");
            sbHtmlTempale.Append("\n     } ");
            sbHtmlTempale.Append("\n </script> ");

            sbHtmlTempale.Append("\r\n </article> \n </div> \n </body></html>");

            // Save the htm file:
            sbHtmlTempale.Replace("@@@letters@@@", constraintLettersTherN(editionId));
            sbHtmlTempale.Replace("@@@let@@@", alphabetRows.LETTER );
            this.saveHtmlFile(alphabetRows.LETTER + "-indatc.htm", sbHtmlTempale.ToString());
        }
    }

    protected string teraLinkN(PEV.TMPTHERARow terapeuticRow, int editionId)
    {

        StringBuilder sb = new StringBuilder();

        sb.Append("  <button class=\"acordeon\"> ");
 
        sb.Append(this.replaceAccentsToHtmlCodes(terapeuticRow.TherapeuticName.ToUpper()) + "</button>\r\n");

        sb.Append( " <div class=\"panel\"> " + Environment.NewLine);

        //sb.Append("<div id=\"rptLevel_0_ctl" + terapeuticRow.TherapeuticId + "_divLevelChildren_0\" style=\"text-align:left; display:none;\" class=\"rubro\">");

        PEVTableAdapters.ProductTheraTableAdapter information = new PEVTableAdapters.ProductTheraTableAdapter();
        PEV.ProductTheraDataTable informationTable = information.getInformation(editionId, terapeuticRow.TherapeuticId);

        //// Get the html file template to save the correspond letter html file:


        foreach (DataRow infRow in informationTable.Rows)
        {

            PEV.ProductTheraRow terUseRow = (PEV.ProductTheraRow)infRow;

   
            sb.Append(this.InfTeraLinkN(terUseRow));

    
        }

        PEVTableAdapters.TMPTHERATableAdapter teraAdp = new PEVTableAdapters.TMPTHERATableAdapter();
        PEV.TMPTHERADataTable theraTable = teraAdp.GetTheraByParentId(terapeuticRow.TherapeuticId);

        sb.Append(" " + Environment.NewLine);

        foreach (DataRow teraRow in theraTable.Rows)
        {

            PEV.TMPTHERARow theraRow = (PEV.TMPTHERARow)teraRow;
 
              sb.Append(this.teraLinkN(theraRow, editionId));

        

        }
         

        sb.Append(" </div>" + Environment.NewLine);

        sb.Append(" " + Environment.NewLine);


        return sb.ToString();

    }

    protected string InfTeraLinkN(PEV.ProductTheraRow infTeraRow)
    {
        StringBuilder sb = new StringBuilder();

        sb.Append("\n <div class=\"atc_producto\"> \n <p class=\"item-acordeonATC\"><a class=\"atc_producto\" href=\"../productos/" + infTeraRow.PRODUCTID + "_" + infTeraRow.PharmaFormId + "_" + infTeraRow.DivisionId + ".htm\" style=\"text-decoration:none\">"
             + "<b>" + this.replaceAccentsToHtmlCodes(infTeraRow.BRAND) + ".</b> " + this.replaceAccentsToHtmlCodes(infTeraRow.ACTIVESUBSTANCES) + ". <i>" +
             this.replaceAccentsToHtmlCodes(infTeraRow.PHARMAFORMS) + ".</i> " + "<b>" + this.replaceAccentsToHtmlCodes(infTeraRow.LABORATORYNAME) + "</b></a> \n </p> \n </div>" + Environment.NewLine);


        return sb.ToString();
    }
     

    private string constraintLettersTherN(int edition)
    {
        string letras = "";
        string[] lets = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
        System.Collections.Generic.List<string> s = new System.Collections.Generic.List<string>(lets);
        System.Collections.Generic.List<string> s2 = new System.Collections.Generic.List<string>();
        int cont = 0;

        //Get Products from Database:
        PEVTableAdapters.AlphabetTableAdapter alphabetAdp = new PEVTableAdapters.AlphabetTableAdapter();
        PEV.AlphabetDataTable alphabetTable = alphabetAdp.getTheraAlphabet(edition);
        foreach (DataRow aRo in alphabetTable)
        {
            PEV.AlphabetRow alphabetRows = (PEV.AlphabetRow)aRo;
            s2.Add(alphabetRows.LETTER);
        }
        foreach (string letter in s)
        {
            cont++;

            if (s2.Contains(letter))
            {
                letras = letras + "\n" + "<a href=\"" + letter + "-indatc.htm\" title=\"" + letter + "\" target=\"carga-info\"> <li class=\"letra\"><img src=\"../../img/letra-" + letter + ".png\" alt=\"\"></li></a> \n";

                //letras = letras + "\n" + "<a href=\"" + aRow + "-indsus.htm\" title=\"" + aRow + "\" target=\"carga-info\"> <li class=\"letra\"><img src=\"../../img/letra-" + aRow + ".png\" alt=\"\"></li></a> \n";

            }
            else
            {
                letras = letras + "\n" + "<a href=\"indatcno.htm\" title=\"" + letter + "\" target=\"carga-info\"> <li class=\"letra\"> <img src=\"../../img/letra-" + letter + ".png\" alt=\"\"></li></a> \n";
            }

        }

        return letras;
    }


////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////// 
        
        #endregion
    
    #region Links

    protected string speciesLink(PEV.SpeciesRow speciesRow)
    {
        StringBuilder sb = new StringBuilder();

        if (speciesRow.SpecieName == "INCUBADORAS" || speciesRow.SpecieName == "INSTALACIONES PECUARIAS" || speciesRow.SpecieName == "INSTALACIONES Y EQUIPO")
        {
            sb.Append("<a class=\"Marca_productoLink\" href=\"../especies/" + speciesRow.SpecieId + "_e.htm\" style='text-decoration:none; line-height:1.8;'>"
                + this.replaceAccentsToHtmlCodes(speciesRow.SpecieName) + "</a><br/>" + System.Environment.NewLine);
        }
        else
        {
            sb.Append("<a class=\"Marca_productoLink\" href=\"../especies/" + speciesRow.SpecieId + ".htm\" style='text-decoration:none; line-height:1.8;'>"
                            + this.replaceAccentsToHtmlCodes(speciesRow.SpecieName) + "</a><br/>" + System.Environment.NewLine);
        }
            return sb.ToString();
        
    }

    protected string productSpecLink(PEV.ProductBySpecieRow prodSpecRow)
    {
        

        StringBuilder sb = new StringBuilder();
        sb.Append("<a class=\"Marca_productoLink\" href=\"../productos/" + prodSpecRow.ProductId + "_" + prodSpecRow.PharmaFormId + ".htm\" style='text-decoration:none; '>"
        + "* <b>" + this.replaceAccentsToHtmlCodes(prodSpecRow.ProductName) + ".</b> " + this.replaceAccentsToHtmlCodes(prodSpecRow.Substance) + ". <i>" + this.replaceAccentsToHtmlCodes(prodSpecRow.PharmaForm) + ".</i> " + "<b>" + this.replaceAccentsToHtmlCodes(prodSpecRow.DivisionName) + "</b></a><br/>" + System.Environment.NewLine);

        return sb.ToString();
    }

    protected string productLink(PEV.ProductsRow prodRow)
      
    {
        StringBuilder sb = new StringBuilder();
        
        if (prodRow.TYPEINEDITION == 1)          

          
        {
            sb.Append("\r\n<p class=\"prodLink\"><a class=\"linksSections\" href=\"../productos/" + prodRow.ProductId +"_" + prodRow.PharmaFormId + ".htm\" style='text-decoration:none'><b>* " +
                this.replaceAccentsToHtmlCodes(prodRow.ProductName) + "</b>. " );
            if (!prodRow.IsProductDescriptionNull())
	            {
                    sb.Append(this.replaceAccentsToHtmlCodes(prodRow.ProductDescription)+". ");
            	}
                
                sb.Append( "<i>"+
                this.replaceAccentsToHtmlCodes(prodRow.PharmaForm) + ". </i> <b>" +
                this.replaceAccentsToHtmlCodes(prodRow.DivisionName) + "</b></a></p>");
        }
        else
        {
            sb.Append("\r\n<p class=\"prodNoLink\"><b>" + this.replaceAccentsToHtmlCodes(prodRow.ProductName) + "</b>. " +
                this.replaceAccentsToHtmlCodes(prodRow.ProductDescription) + ". <i>" + this.replaceAccentsToHtmlCodes(prodRow.PharmaForm) + ".</i> <b>" + this.replaceAccentsToHtmlCodes(prodRow.DivisionName) + "</b></p>");
        }

        return sb.ToString();
    }

    protected string sustanciasLink(PEV.ActiveSubstancesRow actSubRow)
    {
        StringBuilder sb = new StringBuilder();

        sb.Append("<div style='overflow:auto;'> ");

        //sb.Append("<p><a href=\"#\" id=\"rptLevel_0_ctl00_displayChildren_0\" target='_self' class=\"\" onClick=\"document.getElementById('rptLevel_0_ctl00_z_divLevelChildren_0').style.display='block'; guardSearch('rptLevel_0_ctl00_z_divLevelChildren_0', 1); return false;\" onDblClick=\"document.getElementById('rptLevel_0_ctl00_z_divLevelChildren_0').style.display='none'; guardSearch('rptLevel_0_ctl00_z_divLevelChildren_0', 0); return false\"> ");

        sb.Append("<p><a href=\"#rptLevel_0_ct_" + actSubRow.ID + "_displayChildren_0\" id=\"rptLevel_0_ct_");
        sb.Append(actSubRow.ID + "_displayChildren_0\" target='_self' class=\"sustancia\" onClick=\"document.getElementById('rptLevel_0_ct");
        sb.Append(actSubRow.ID + "_divLevelChildren_0').style.display='block'; guardSearch('rptLevel_0_ct"); 
        sb.Append(actSubRow.ID + "_divLevelChildren_0', 1); return false;\" onDblClick=\"document.getElementById('rptLevel_0_ct");
        sb.Append(actSubRow.ID + "_divLevelChildren_0').style.display='none'; guardSearch('rptLevel_0_ct");
        sb.Append(actSubRow.ID + "_divLevelChildren_0', 0); return false\"> ");
        
        sb.Append(this.replaceAccentsToHtmlCodes(actSubRow.ACTIVESUBSTANCE) + "</a></p>\r\n");

        sb.Append("\r\n<blockquote>");
        
        
        sb.Append("\r\n<div id=\"rptLevel_0_ct");
        sb.Append(actSubRow.ID + "_divLevelChildren_0\" style=\"text-align:left; display:none;\">  ");                  
        
        return sb.ToString();
    
    }

    protected string prdActSubsLink(PEV.ProdSus1Row prdSusSoloRow)              
    {

            StringBuilder sb = new StringBuilder();


            sb.Append("<tr><td><a class=\"Sust_ProductoLink\" href=\"../productos/" + prdSusSoloRow.productid + "_" + prdSusSoloRow.PharmaFormId + ".htm\" id=\"displayChildren_1\" target='_self' style='text-decoration:none; margin-left: 15px;' class=\"Sust_ProductoLink\">"
            + "<b>" + this.replaceAccentsToHtmlCodes(prdSusSoloRow.nombre) + "</b> <i>" + this.replaceAccentsToHtmlCodes(prdSusSoloRow.PharmaForms) + ".</i> <b>" +  this.replaceAccentsToHtmlCodes(prdSusSoloRow.laboratorio) + "</b></a></td></tr>" + System.Environment.NewLine);
             

            return sb.ToString();
       
    }

    protected string prdSusComLink(PEV.ProdSus2Row prdSusComRow)
    {
        StringBuilder sb = new StringBuilder();


        sb.Append("<tr><td><a class=\"Sust_ProductoLink\" href=\"../productos/" + prdSusComRow.productid + "_" + prdSusComRow.PharmaFormId + ".htm\" id=\"displayChildren_1\" target='_self' style='text-decoration:none; margin-left: 15px;' class=\"Sust_ProductoLink\">"
             + "<b>" + this.replaceAccentsToHtmlCodes(prdSusComRow.nombre) + "</b> " + this.replaceAccentsToHtmlCodes(prdSusComRow.Sustancias) + ". <i>" + this.replaceAccentsToHtmlCodes(prdSusComRow.PharmaForms) + ".<i> <b> " + this.replaceAccentsToHtmlCodes(prdSusComRow.laboratorio) + "</b></a></td></tr>" + System.Environment.NewLine);

        return sb.ToString();

    
    }

    protected string teraLink(PEV.TMPTHERARow terapeuticRow, int editionId)
    {

        StringBuilder sb = new StringBuilder();

        sb.Append("<div style='overflow:auto;'> ");

        sb.Append("<a href=\"#rptLevel_0_ct" + terapeuticRow.TherapeuticId + "_displaychildren_0\" id=\"rptLevel_0_ct");
        sb.Append(terapeuticRow.TherapeuticId + "_displaychildren_0\" target='_self' class=\"atc_level\" onClick=\"document.getElementById('rptLevel_0_ctl");
        sb.Append(terapeuticRow.TherapeuticId + "_divLevelChildren_0').style.display='block'; guardSearch('rptLevel_0_ct");
        sb.Append(terapeuticRow.TherapeuticId + "_divLevelChildren_0', 1); return false;\" onDblClick=\"document.getElementById('rptLevel_0_ctl");
        sb.Append(terapeuticRow.TherapeuticId + "_divLevelChildren_0').style.display='none'; guardSearch('rptLevel_0_ctl");
        sb.Append(terapeuticRow.TherapeuticId + "_divLevelChildren_0', 0); return false\"> ");

        sb.Append(this.replaceAccentsToHtmlCodes(terapeuticRow.TherapeuticName.ToUpper()) + "</a>\r\n");

        sb.Append("<br />" + Environment.NewLine + "<br />" + Environment.NewLine);

        sb.Append("<div id=\"rptLevel_0_ctl" + terapeuticRow.TherapeuticId + "_divLevelChildren_0\" style=\"text-align:left; display:none;\" class=\"rubro\">");

        PEVTableAdapters.ProductTheraTableAdapter information = new PEVTableAdapters.ProductTheraTableAdapter();
        PEV.ProductTheraDataTable informationTable = information.getInformation(editionId, terapeuticRow.TherapeuticId);

        //// Get the html file template to save the correspond letter html file:


        foreach (DataRow infRow in informationTable.Rows)
        {

            PEV.ProductTheraRow terUseRow = (PEV.ProductTheraRow)infRow;

            sb.Append("<blockquote>" + Environment.NewLine);

            sb.Append("<div class=\"atc_producto\" style='text-align:left;' >" + Environment.NewLine);

            sb.Append(this.InfTeraLink(terUseRow));

            sb.Append("</div>" + Environment.NewLine);

            sb.Append("</blockquote>" + Environment.NewLine + " " + Environment.NewLine);

        }

        PEVTableAdapters.TMPTHERATableAdapter teraAdp = new PEVTableAdapters.TMPTHERATableAdapter();
        PEV.TMPTHERADataTable theraTable = teraAdp.GetTheraByParentId(terapeuticRow.TherapeuticId);

        foreach (DataRow teraRow in theraTable.Rows)
        {

            PEV.TMPTHERARow theraRow = (PEV.TMPTHERARow)teraRow;

            sb.Append("<blockquote>" + Environment.NewLine);

            sb.Append(this.teraLink(theraRow, editionId));

            sb.Append("</blockquote>" + Environment.NewLine);

        }


        
        sb.Append("</div>" + Environment.NewLine);

        sb.Append("</div>" + Environment.NewLine);

 
        return sb.ToString();

    }

    protected string InfTeraLink(PEV.ProductTheraRow infTeraRow)
    {
        StringBuilder sb = new StringBuilder();

        sb.Append("<a class=\"atc_producto\" href=\"../productos/" + infTeraRow.PRODUCTID + "_" + infTeraRow.PharmaFormId + ".htm\" style='text-decoration:none'>"
             + "<b>" + this.replaceAccentsToHtmlCodes(infTeraRow.BRAND) + ".</b> " + this.replaceAccentsToHtmlCodes(infTeraRow.ACTIVESUBSTANCES) +  ". <i>" + 
             this.replaceAccentsToHtmlCodes(infTeraRow.PHARMAFORMS) + ".</i> " + "<b>" + this.replaceAccentsToHtmlCodes(infTeraRow.LABORATORYNAME) + "</b></a>" + Environment.NewLine);
         

        return sb.ToString();
    }

    protected string labsLink(PEV.LaboratoriesRow labRow)
    {
        StringBuilder sb = new StringBuilder();

        sb.Append("<p class=\"Lab_laboratorio\"><a href=\"./" + labRow.DivisionId.ToString() + ".htm\" >" + 
            this.replaceAccentsToHtmlCodes(labRow.DivisionName.ToUpper()) + "</a></p>" + System.Environment.NewLine);

        return sb.ToString();
    }

    #endregion

    #region Private Methods
    private string getImagesLabs() { 

    return "";
    }

    private void getHtmlLabs(int editionId, PEV.LaboratoriesRow labRow, string fileTemplate)
    {
        StringBuilder sbHtmlTemplete = new StringBuilder(this.getHtmlTemplate(fileTemplate));

        sbHtmlTemplete.Append("<table id=\"table2\" width=\"800\" >" + Environment.NewLine + "<tr>" + Environment.NewLine + "<td width=\"70%\">" + Environment.NewLine);

        sbHtmlTemplete.Append("<div class=\"NomLab\">" + labRow.DivisionName.ToUpper() + "<br />" + Environment.NewLine + "</div>" + Environment.NewLine);

        sbHtmlTemplete.Append(this.getInformation(labRow.DivisionId) + Environment.NewLine);

        sbHtmlTemplete.Append("\r\n</td>" + Environment.NewLine);
        sbHtmlTemplete.Append("<td width=\"30%\" aling=\"center\">@@@image@@@</td>" + Environment.NewLine + "</tr>" + Environment.NewLine + "</table>" + Environment.NewLine);

        sbHtmlTemplete.Append("<hr>" + Environment.NewLine);

        sbHtmlTemplete.Append("</td></tr>" + Environment.NewLine);
        
        sbHtmlTemplete.Append(" " + Environment.NewLine);

        sbHtmlTemplete.Append("<tr><td>" + Environment.NewLine);
        sbHtmlTemplete.Append("<table style=\"margin-left:30px; margin-right:10px; width:95%;    line-height:20px;\">" + Environment.NewLine);

                

        PEVTableAdapters.CategoriesTableAdapter catAdp = new PEVTableAdapters.CategoriesTableAdapter();
        PEV.CategoriesDataTable catTable = catAdp.getCategoriesByDivision(editionId, labRow.DivisionId);

        foreach(DataRow catRow in catTable.Rows)
        {
            PEV.CategoriesRow categoryRow = (PEV.CategoriesRow)catRow;

            sbHtmlTemplete.Append("<tr><td>" + Environment.NewLine);

            sbHtmlTemplete.Append("<p class=\"Lab_division\">" + categoryRow.CategoryName.ToUpper() + "</p>" + Environment.NewLine);

            sbHtmlTemplete.Append("</td></tr>" + Environment.NewLine);

            PEVTableAdapters.ProductsTableAdapter prodAdp = new PEVTableAdapters.ProductsTableAdapter();
            PEV.ProductsDataTable prodTable = prodAdp.getProductsByDivision(editionId, labRow.DivisionId, categoryRow.CategoryId);

            foreach(DataRow prodRow in prodTable.Rows)
            {
                PEV.ProductsRow productRow = (PEV.ProductsRow)prodRow;

                sbHtmlTemplete.Append("<tr><td>" + Environment.NewLine);

                if (productRow.TYPEINEDITION == 1)
                {
                    sbHtmlTemplete.Append("<a style=\"TEXT-DECORATION: none; \" target=\"_self\" class=\"Lab_productoLink\" href=\"../productos/" + productRow.ProductId.ToString() + "_" + productRow.PharmaFormId.ToString() + ".htm\">");
                    sbHtmlTemplete.Append("<b>* " + productRow.ProductName.ToUpper() + ".</b> ");
                    sbHtmlTemplete.Append(productRow.ProductDescription + ". <i>" + productRow.PharmaForm + "</i></a>");
                }
                else
                {
                    sbHtmlTemplete.Append("<span class=\"Lab_productoNoLink\">");
                    sbHtmlTemplete.Append("<b> " + productRow.ProductName.ToUpper() + ".</b> ");
                    sbHtmlTemplete.Append(productRow.ProductDescription + ". <i>" + productRow.PharmaForm + "</i></span>");
                }

                sbHtmlTemplete.Append(" </td></tr>" + Environment.NewLine);


            }

        }

        sbHtmlTemplete.Append("</table> \r\n</td></tr>");

        PEVTableAdapters.AdverstmentTableAdapter advAdp = new PEVTableAdapters.AdverstmentTableAdapter();
        PEV.AdverstmentDataTable advTbl = advAdp.GetAdverstmentByDivByEdition(labRow.DivisionId, editionId);
        string adv = "<div align=\"center\">";
        foreach (DataRow dr in advTbl.Rows)
        {
            PEV.AdverstmentRow adRow = (PEV.AdverstmentRow)dr;
            adv+="<img src='"+adRow.BaseUrl+adRow.AdFile+"'><br/>";
        }
        adv += "</div> ";
        sbHtmlTemplete.Append("\r\n</table>");
        sbHtmlTemplete.Append("\r\n ");
        sbHtmlTemplete.Append(adv);
        sbHtmlTemplete.Append("</body></html>");
        sbHtmlTemplete.Replace("@@@image@@@", getImageDivision(labRow.DivisionId));
        sbHtmlTemplete.Replace(".. <i>", ". <i>");
        sbHtmlTemplete.Replace(". <i>Sin Asignar</i>", "");
        sbHtmlTemplete.Replace("..</b>", ".</b>");
        sbHtmlTemplete.Replace(" .</b>", ".</b>");
        sbHtmlTemplete.Replace(".</b> . <i>", ".</b>  <i>");
        // Save the htm file:
        this.saveHtmlFile(labRow.DivisionId.ToString() + ".htm", sbHtmlTemplete.ToString());

    }
    private string getImageDivision(int division){
        PEVTableAdapters.DivisionInformationTableAdapter divInfAdp = new PEVTableAdapters.DivisionInformationTableAdapter();
        PEV.DivisionInformationDataTable divInfTable = divInfAdp.getAddressByDivision(division);
        string images = "";
        foreach (DataRow aRow in divInfTable.Rows)
        {
            PEV.DivisionInformationRow divRow = (PEV.DivisionInformationRow)aRow;
            if (!(divRow.IsImageNull()))
            {
            char[] sp = {','};
            string[] imag = divRow.Image.Split(sp);
            foreach (string value in imag)
            {
             images+="<img src=\"logos/"+value.Trim()+"\" /><br/>";
            }
            }
        }

        return images;
    }
    private string getInformation(int divisionId)
    {
        StringBuilder sb = new StringBuilder();
       
        PEVTableAdapters.DivisionInformationTableAdapter divInfAdp = new PEVTableAdapters.DivisionInformationTableAdapter();
        PEV.DivisionInformationDataTable divInfTable = divInfAdp.getAddressByDivision(divisionId);

        foreach (DataRow aRow in divInfTable.Rows)
        {
            PEV.DivisionInformationRow divRow = (PEV.DivisionInformationRow)aRow;

            sb.Append(this.setInformation(divRow));

            PEV.DivisionInformationDataTable branchRows = divInfAdp.getAddressByParent(divRow.DivisionId);

            foreach (DataRow sRow in branchRows)
            {
                PEV.DivisionInformationRow branchRow = (PEV.DivisionInformationRow)sRow;

                //if (!branchRow.IsTextNull())
                    sb.Append("<p class=\"Lab_Datos\"><b>" + sRow[15].ToString() + "</b></p>" + Environment.NewLine);

                sb.Append(this.setInformation(branchRow));

            }

        }

        return sb.ToString();
    }

    private string setInformation(PEV.DivisionInformationRow divRow)
    {
        StringBuilder sb = new StringBuilder();

        sb.Append("<table>" + Environment.NewLine + "<tr>" + Environment.NewLine + "<td>" + Environment.NewLine);

        sb.Append("<p class=\"Lab_Datos\">" + divRow.Address + "<br />" + Environment.NewLine);
        
        if(!divRow.IsSuburbNull())
            sb.Append(divRow.Suburb + "<br />" + Environment.NewLine);
        
        if(!divRow.IsZipCodeNull())
            sb.Append(divRow.ZipCode + "<br /> ");
                
        if (!divRow.IsCityNull())
            sb.Append(divRow.City);
        if(!divRow.IsStateNull())
        sb.Append(", " + divRow.State + "<br />" + Environment.NewLine);

        if (!divRow.IsTelephoneNull())
            sb.Append(divRow.Telephone + "<br />" + Environment.NewLine);

        if (!divRow.IsFaxNull())
            sb.Append(divRow.Fax + "<br />" + Environment.NewLine);

        if (!divRow.IsLadaNull())
            sb.Append(divRow.Lada + "<br />" + Environment.NewLine);

        if (!divRow.IsEmailNull())
            sb.Append(this.setEmail(divRow.Email) + "<br />" + Environment.NewLine);

        if (!divRow.IsWebNull())
            sb.Append("<a href=\"http://" + divRow.Web.Trim() + "\" target=\"_blank\" >" + divRow.Web.Trim() + "</a>" + Environment.NewLine);

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
                cadena = "<a href=\"mailto:" + mail.Trim() + "\" >" + mail.Trim() + "</a> / " + Environment.NewLine;
            }


        }
        else
            cadena = "<a href=\"mailto:" + email.Trim() + "\" >" + email.Trim() + "</a> " + Environment.NewLine;


        return cadena;

    }



    #endregion
    
}
