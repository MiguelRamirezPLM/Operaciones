using System.Collections.Generic;
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

    ////////////////////// Indices Anunciantes 

    public void getHtmlFilesInd(int editionId, int clientTypeId)
    {
       //get Alphabet from Database:

        NewGuiaTableAdapters.AlphabetClientsTableAdapter alphabetAdp = new NewGuiaTableAdapters.AlphabetClientsTableAdapter();
        NewGuia.AlphabethDataTable alphabetTable = alphabetAdp.GetAlphabetAnun(editionId, clientTypeId);

        String htmlLetras = "";
        String htmlAnunciantes = "";
        int val = 0;
       
        foreach (DataRow aRow in alphabetTable.Rows)
        {
            foreach (DataRow aaRow in alphabetTable.Rows)
            {
                val++;
                NewGuia.AlphabethRow aalphabetRow = (NewGuia.AlphabethRow)aaRow;
                htmlLetras += "<th width='20' height='20' scope='col'><a href='" + aalphabetRow[0].ToString() + "-general.htm' title='" + aalphabetRow[0].ToString() + "'target='adentro'><img src='../letras/" + aalphabetRow[0].ToString() + "1.gif' style='border:none'  name='Image1" + val.ToString() + "' id='Image1" + val.ToString() + "'  onmouseover='MM_swapImage('Image1" + val.ToString() + "','','../letras/" + aalphabetRow[0].ToString() + "2.gif',1)' onmouseout='MM_swapImgRestore()' /></a></th>\n";
            }
            val = 0;

            NewGuia.AlphabethRow alphabetRow = (NewGuia.AlphabethRow)aRow;
            StringBuilder sbHtmlTemplete = new StringBuilder(this.getHtmlTemplate());

            //Get Products from Database
   
            NewGuiaTableAdapters.GetClientsAlphabetTableAdapter spAdp = new NewGuiaTableAdapters.GetClientsAlphabetTableAdapter();
            NewGuia.ClientsDataTable spTable = spAdp.GetAnunciante(editionId, alphabetRow.LETTER.ToString(), clientTypeId);

            //Get the html file template to save the correspond letter html file
                foreach (DataRow spRow in spTable.Rows)
            {
                NewGuia.ClientsRow clientRow = (NewGuia.ClientsRow)spRow;
                htmlAnunciantes += "\n <a class=\"Companies\" href='../Anunciantes/"+clientRow[1].ToString()+".htm' style='text-decoration:none'>" + clientRow[0].ToString() +"</a><br>";
            }

                sbHtmlTemplete.Replace("@@Letras@@", this.replaceAccentsToHtmlCodes(htmlLetras));
                sbHtmlTemplete.Replace("@@Parrafo@@", this.replaceAccentsToHtmlCodes("<p class='MainLetter' align='center'>- " + alphabetRow[0].ToString() + " -</p>"));
                sbHtmlTemplete.Replace("@@Clientes@@", this.replaceAccentsToHtmlCodes(htmlAnunciantes));
            
            this.saveHtmlFile(alphabetRow.LETTER + "-general.htm", this.replaceAccentsToHtmlCodes(sbHtmlTemplete.ToString()));
            htmlAnunciantes = "";
            htmlLetras = "";

        }

    }

    public void getHtmlFilesDataClient(int editionId, int clientTypeId)
    {

        NewGuiaTableAdapters.AlphabetClientsTableAdapter alphabetAdp = new NewGuiaTableAdapters.AlphabetClientsTableAdapter();
        NewGuia.AlphabethDataTable alphabetTable = alphabetAdp.GetAlphabetAnun(editionId, clientTypeId);
        String mails = "";
        String webs = ""; 

        //Get Products from Database
         
        NewGuiaTableAdapters.DatosClientsTableAdapter spAdp = new NewGuiaTableAdapters.DatosClientsTableAdapter();
        NewGuia.DatosClientsDataTable spTable = spAdp.GetDatosCliente(editionId, clientTypeId);

        //Get the html file template to save the correspond letter html file
        foreach (DataRow spRow in spTable.Rows)
        {
            StringBuilder sbHtmlTemplete = new StringBuilder(this.getHtmlTemplate());
            NewGuia.DatosClientsRow clientRow = (NewGuia.DatosClientsRow)spRow;
            if (clientRow.IsImageNull())
            {
                sbHtmlTemplete.Replace("@@image@@", "");
            }
            else
            {
                sbHtmlTemplete.Replace("@@image@@", "&nbsp;<img border='0' src='../logos/" + clientRow.Image + "'><br>");
            }
            sbHtmlTemplete.Replace("@@Titulo@@", this.replaceAccentsToHtmlCodes("<title>" + clientRow.CompanyName + "</title>"));
            sbHtmlTemplete.Replace("@@Span@@", this.replaceAccentsToHtmlCodes("<span class='CompanyName'>" + clientRow.CompanyName + "</span>"));
            sbHtmlTemplete.Replace("@@Direccion@@", this.replaceAccentsToHtmlCodes("<br>" + clientRow.Address));
            sbHtmlTemplete.Replace("@@Colonia@@", this.replaceAccentsToHtmlCodes("<br>" + clientRow.Suburb));
            sbHtmlTemplete.Replace("@@CP@@", this.replaceAccentsToHtmlCodes("<br>" + clientRow.CP));

            if (!clientRow.IsCityNull())
            {
                sbHtmlTemplete.Replace("@@Ciudad@@", this.replaceAccentsToHtmlCodes("<br>" + clientRow.City));
            }
            if (!clientRow.IsPHONENull())
            {
                sbHtmlTemplete.Replace("@@Telefono@@", this.replaceAccentsToHtmlCodes("<br>" + clientRow.PHONE));
            }

            if (!clientRow.IsPHONEFAXNull())
            {
                sbHtmlTemplete.Replace("@@TelFax@@", this.replaceAccentsToHtmlCodes("<br>" + clientRow.PHONEFAX));
            }
            if (!clientRow.IsFAXNull())
            {
                sbHtmlTemplete.Replace("@@Fax@@", this.replaceAccentsToHtmlCodes("<br>" + clientRow.FAX));
            }
            if (!clientRow.IsLADANull())
            {
                sbHtmlTemplete.Replace("@@Lada@@", this.replaceAccentsToHtmlCodes("<br>" + clientRow.LADA));
            }
            if (!clientRow.IsOXIDOMNull())
            {
                sbHtmlTemplete.Replace("@@Oxidom@@", this.replaceAccentsToHtmlCodes("<br>" + clientRow.OXIDOM));
            }

            if (!clientRow.IsEmailNull())
            {


                if (clientRow.Email.Contains(";"))
                {
                    Char[] spl = { ';' };
                    String[] arr = clientRow.Email.Split(spl);

                    foreach (String item in arr)
                    {
                        mails += "<a href='mailto:" + item.Replace(" ", "") + "'><span style='text-decoration: none'>" + item.Replace(" ", "") + "</span></a>;&nbsp;";

                    }
                    mails = mails.Substring(0, mails.Length - 7);
                    if (mails.Length > 5)
                    {


                        sbHtmlTemplete.Replace("@@Email@@", this.replaceAccentsToHtmlCodes("<br>Email: " + mails));
                    }
                }
                else
                {
                    if (clientRow.Email.Length > 5)
                    {


                        sbHtmlTemplete.Replace("@@Email@@", this.replaceAccentsToHtmlCodes("<br>Email: <a href='mailto:" + clientRow.Email.Replace(" ", "") + "'><span style='text-decoration: none'>" + clientRow.Email.Replace(" ", "") + "</span></a>"));
                    }
                }
            }
            if (!clientRow.IsWebNull())
            {


                if (clientRow.Web.Contains(";"))
                {
                    Char[] spl = { ';' };
                    String[] arr = clientRow.Web.Split(spl);
                    foreach (String item in arr)
                    {
                        webs += "<a href='http:" + item.Replace(" ", "") + "' target='_BLANK'><span style='text-decoration: none'>" + item.Replace(" ", "") + "</span></a></span>;&nbsp;";
                    }
                    webs = webs.Substring(0, webs.Length - 7);
                    if (webs.Length > 5)
                    {


                        sbHtmlTemplete.Replace("@@Web@@", this.replaceAccentsToHtmlCodes("<br>Web: " + webs));
                    }
                }
                else
                {
                    if (clientRow.Web.Length > 5)
                    {

                        sbHtmlTemplete.Replace("@@Web@@", this.replaceAccentsToHtmlCodes("<br>Web: <a href='http:" + clientRow.Web.Replace(" ", "") + "' target='_BLANK'><span style='text-decoration: none'>" + clientRow.Web.Replace(" ", "") + "</span></a></span>"));
                    }
                }

            }


            NewGuiaTableAdapters.BrandsClientsTableAdapter mcTA = new NewGuiaTableAdapters.BrandsClientsTableAdapter();
            NewGuia.BrandsClientsDataTable mcDT = mcTA.GetMarcasClient(editionId, clientRow.ClientId);

            if (mcDT.Rows.Count > 0)
            {
                sbHtmlTemplete.Replace("@@Marcas@@", this.replaceAccentsToHtmlCodes("<br><br><a href='../marcas/" + clientRow.ClientId + ".htm'><span style='text-decoration: none'>Ver Marcas</span></a></span></td>"));
            }
            else
            {
                String Marcas = "";
                foreach (DataRow drow in mcDT.Rows)
                {

                    NewGuia.BrandsClientsRow mc = (NewGuia.BrandsClientsRow)drow;
                    Marcas += "<a href='../marcas/" + mc.BRANDID + ".htm' style='text-decoration:none'>" + mc.BRANDNAME + "</a><br>\n";
                }
                sbHtmlTemplete.Replace("@@Marcas@@", this.replaceAccentsToHtmlCodes(Marcas));
            }

            NewGuiaTableAdapters.ProductsTableAdapter prTA = new NewGuiaTableAdapters.ProductsTableAdapter();
            NewGuia.ProductsDataTable prDT = prTA.GetProductClients(editionId, clientRow.ClientId, 2);

            if (prDT.Rows.Count > 0)
            {
                if (!(prDT.Rows[0][0].ToString().Equals("SIN PRODUCTOS")))
                {
                    sbHtmlTemplete.Replace("@@Productos@@", this.replaceAccentsToHtmlCodes("<br><br><a href='../productos/" + clientRow.ClientId + ".htm'><span style='text-decoration: none'>Ver Productos</span></a></span></td>"));
                }

            }
            else
            {
               
                NewGuiaTableAdapters.DrugsTableAdapter DrugTA = new NewGuiaTableAdapters.DrugsTableAdapter();
                NewGuia.DrugsDataTable DrugDT = DrugTA.GetMedicamHosp(editionId, clientRow.ClientId);


                if (DrugDT.Rows.Count > 0)
                {
                    String Drugs = "<b><hr></b><br><div class='StateTitle'>Productos:</div><br>";
                    foreach (DataRow drRow in DrugDT.Rows)
                    {
                        NewGuia.DrugsRow drugRow = (NewGuia.DrugsRow)drRow;
                        Drugs += "\n <a class=\"marcasL\"  href='../productos/" + clientRow.ClientId + "_" + drugRow.ProductId + '_' + drugRow.PharmaFormId + '_' + drugRow.PresentationId + ".htm' style='text-decoration:none'>" + drugRow.ProductName + "</a><br>";

                    }
                    sbHtmlTemplete.Replace("@@Hospitalarios@@", this.replaceAccentsToHtmlCodes(Drugs));
                }
                else
                {

                    string Productos = "";
                    foreach (DataRow drPr in prDT.Rows)
                    {
                        NewGuia.ProductsRow prRow = (NewGuia.ProductsRow)drPr;
                        Productos += "<span class='ProductName'>" + prRow.PRODUCTNAME + "</span><br><br>\n";

                        NewGuiaTableAdapters.SubProductsTableAdapter spTA = new NewGuiaTableAdapters.SubProductsTableAdapter();
                        NewGuia.SubProductsDataTable spDT = spTA.GetSubProducts(editionId, clientRow.ClientId, prRow.PRODUCTID);

                        foreach (DataRow drSP in spDT.Rows)
                        {
                            NewGuia.SubProductsRow SupRow = (NewGuia.SubProductsRow)drSP;
                            Productos += "<span class='SubProduct'>&#8226;&nbsp;" + SupRow.PRODUCTNAME + "</span><br><br>";
                        }
                    }
                    sbHtmlTemplete.Replace("@@Productos@@", this.replaceAccentsToHtmlCodes(Productos));
                }

            }

            NewGuiaTableAdapters.DatosClientsTableAdapter dcTA = new NewGuiaTableAdapters.DatosClientsTableAdapter();
            NewGuia.DatosClientsDataTable dcDT = dcTA.GetDatosClienteByParentId(editionId, 1, clientRow.ClientId);

            if (dcDT.Rows.Count > 0)
            {
                sbHtmlTemplete.Replace("@@Sucursales@@", this.replaceAccentsToHtmlCodes("<br><br><a href='../Anunciantes/sucursal/" + clientRow.ClientId + ".htm'><span style='text-decoration: none'>Ver Sucursales</span></a></span></td>"));
            } 

            NewGuiaTableAdapters.AdvertisementsTableAdapter adTA = new NewGuiaTableAdapters.AdvertisementsTableAdapter();
            NewGuia.AdvertisementsDataTable adDT = adTA.GetImagesAnunciantes(editionId, clientRow.ClientId);

            String AnunciosImg = "";
            foreach (DataRow ad in adDT.Rows)
            {
                NewGuia.AdvertisementsRow AdvRow = (NewGuia.AdvertisementsRow)ad;
                AnunciosImg += "<p align='center'><img border='0' src='../images/" + AdvRow.AdvertName + "' ></p><br>";
            }
            sbHtmlTemplete.Replace("@@Anuncios@@", this.replaceAccentsToHtmlCodes(AnunciosImg));
            sbHtmlTemplete.Replace("@@Web@@", "");
            sbHtmlTemplete.Replace("@@Email@@", "");
            sbHtmlTemplete.Replace("@@Oxidom@@", "");
            sbHtmlTemplete.Replace("@@Lada@@", "");
            sbHtmlTemplete.Replace("@@Telefono@@", "");
            sbHtmlTemplete.Replace("@@Ciudad@@", "");
            sbHtmlTemplete.Replace("@@TelFax@@", "");
            sbHtmlTemplete.Replace("@@Fax@@", "");
            sbHtmlTemplete.Replace("@@Productos@@", "");
            sbHtmlTemplete.Replace("@@Hospitalarios@@", "");
            sbHtmlTemplete.Replace("@@Sucursales@@", "");
            AnunciosImg = "";

            mails = "";
            webs = "";
             
            this.saveHtmlFile(clientRow.ClientId + ".htm", sbHtmlTemplete.ToString());

        }

    }

    public void getHtmlMarcasClient(int editionId, int clientTypeId)
    {

        //get Alphabet from Database:

        NewGuiaTableAdapters.AlphabetClientsTableAdapter alphabetAdp = new NewGuiaTableAdapters.AlphabetClientsTableAdapter();
        NewGuia.AlphabethDataTable alphabetTable = alphabetAdp.GetAlphabetAnun(editionId, clientTypeId);

        String Marcas = "";
       
        foreach (DataRow aRow in alphabetTable.Rows)
        {
        
            NewGuia.AlphabethRow alphabetRow = (NewGuia.AlphabethRow)aRow;
            //Get Products from Database

            NewGuiaTableAdapters.GetClientsAlphabetTableAdapter spAdp = new NewGuiaTableAdapters.GetClientsAlphabetTableAdapter();
            NewGuia.ClientsDataTable spTable = spAdp.GetAnunciante(editionId, alphabetRow.LETTER.ToString(), clientTypeId);

            //Get the html file template to save the correspond letter html file

            foreach (DataRow spRow in spTable.Rows)
            {
                StringBuilder sbHtmlTemplete = new StringBuilder(this.getHtmlTemplate());
                NewGuia.ClientsRow clientRow = (NewGuia.ClientsRow)spRow;

                NewGuiaTableAdapters.BrandsClientsTableAdapter sdMaC = new NewGuiaTableAdapters.BrandsClientsTableAdapter();
                NewGuia.BrandsClientsDataTable sdMacT = sdMaC.GetMarcasClient(editionId, clientRow.ClientId);

                sbHtmlTemplete.Replace("@@Titulo@@", this.replaceAccentsToHtmlCodes("<title>" + clientRow.CompanyName + "</title>"));
                sbHtmlTemplete.Replace("@@Parrafo@@", this.replaceAccentsToHtmlCodes("<p class='MainLetter'>MARCAS DE " + clientRow.CompanyName + "</p><hr>"));

                foreach (DataRow drow in sdMacT.Rows)
                {

                    NewGuia.BrandsClientsRow mc = (NewGuia.BrandsClientsRow)drow;

                    Marcas += "<a class=\"marcasL\" href='../marcas/marcas/" + mc.BRANDID + ".htm' style='text-decoration:none'>" + mc.BRANDNAME + "</a><br>\n";

                }

                sbHtmlTemplete.Replace("@@Marcas@@", this.replaceAccentsToHtmlCodes(Marcas));
                Marcas = "";
                this.saveHtmlFile(clientRow.ClientId + ".htm", sbHtmlTemplete.ToString());
            }

        }

    }
       
    public void getHtmlClientMarcas(int editionId, int clientTypeId)
    {

        //get Alphabet from Database:

        NewGuiaTableAdapters.AlphabetClientsTableAdapter alphabetAdp = new NewGuiaTableAdapters.AlphabetClientsTableAdapter();
        NewGuia.AlphabethDataTable alphabetTable = alphabetAdp.GetAlphabetAnun(editionId, clientTypeId);

        String Anunciantes = "";
       

        foreach (DataRow aRow in alphabetTable.Rows)
        {
          
            NewGuia.AlphabethRow alphabetRow = (NewGuia.AlphabethRow)aRow;

            NewGuiaTableAdapters.GetClientsAlphabetTableAdapter spAdp = new NewGuiaTableAdapters.GetClientsAlphabetTableAdapter();
            NewGuia.ClientsDataTable spTable = spAdp.GetAnunciante(editionId, alphabetRow.LETTER.ToString(), clientTypeId);

            //Get the html file template to save the correspond letter html file

            foreach (DataRow spRow in spTable.Rows)
            {

                NewGuia.ClientsRow clientRow = (NewGuia.ClientsRow)spRow;

                NewGuiaTableAdapters.BrandsClientsTableAdapter sdMaC = new NewGuiaTableAdapters.BrandsClientsTableAdapter();
                NewGuia.BrandsClientsDataTable sdMacT = sdMaC.GetMarcasClient(editionId, clientRow.ClientId);
                
                foreach (DataRow drow in sdMacT.Rows)
                {

                    StringBuilder sbHtmlTemplete = new StringBuilder(this.getHtmlTemplate());
                    NewGuia.BrandsClientsRow mc = (NewGuia.BrandsClientsRow)drow;

                    NewGuiaTableAdapters.ClientBrandsTableAdapter mCTA = new NewGuiaTableAdapters.ClientBrandsTableAdapter();
                    NewGuia.ClientBrandsDataTable mcDT = mCTA.GetClienteMarcas(editionId, mc.BRANDID);

                    sbHtmlTemplete.Replace("@@Parrafo@@", this.replaceAccentsToHtmlCodes("<p class='MainLetter'>"+mc.BRANDNAME+"</p><b><hr></b><br>"));
                    string im="";
                    im="<p align=center><table border=0 with=100% style=\"margin-left:25px;\" ><tr><td align=center><p align=center>";
                    if (!(mc.logo.Contains("Sin_Logo")))
	                {
		                im+="<img border=0 src='../../logos/"+mc.logo+"'></td></tr></table></p><br>";
	                }else
	                {
                        im += "</td></tr></table></p><br>";
	                }
                    
                    sbHtmlTemplete.Replace("@@Parrafo2@@", this.replaceAccentsToHtmlCodes(im));
                    foreach (DataRow drCm in mcDT)
                    {
                        NewGuia.ClientBrandsRow CmRow = (NewGuia.ClientBrandsRow)drCm;
 
                        NewGuiaTableAdapters.ClientBrandsTypesTableAdapter cbTA = new NewGuiaTableAdapters.ClientBrandsTypesTableAdapter();
                        NewGuia.ClientBrandsTypesDataTable cbDT = cbTA.GetTipoDistribuidor(editionId, mc.BRANDID,CmRow.ClientId );

                        foreach (DataRow drCB in cbDT.Rows)
                        {
                            NewGuia.ClientBrandsTypesRow cbRow = (NewGuia.ClientBrandsTypesRow)drCB;
                            if (!cbRow.IsClientBrandTypeIdNull())
                            {
                               
                                if (cbRow.ClientBrandTypeId.ToString().Equals("1"))
                                 {
                                    sbHtmlTemplete.Replace("@@Distribuidor@@", this.replaceAccentsToHtmlCodes("<br><br><p class='DistribFoot'>* DISTRIBUIDOR AUTORIZADO</p>"));
                                    Anunciantes += "<a class=\"marcasL\" href='../../Anunciantes/" + CmRow.ClientId + ".htm' style='text-decoration:none; text-transform: uppercase; '>*&nbsp&nbsp " + CmRow.CompanyName + "</a><br>\n";
                                 }
                                 if (cbRow.ClientBrandTypeId.ToString().Equals("2"))
                                 {
                                     Anunciantes += "<a class=\"marcasL\" href='../../Anunciantes/" + CmRow.ClientId + ".htm' style='text-decoration:none; text-transform: uppercase; '>** " + CmRow.CompanyName + "</a><br>\n";
                                 sbHtmlTemplete.Replace("@@Distribuidor@@", this.replaceAccentsToHtmlCodes("<br><br><p class='DistribFoot'>** REPRESENTANTE EXCLUSIVO</p>"));
                                 }
                            }
                            else
                            {
                                Anunciantes += "<a class=\"marcasL\" href='../../Anunciantes/" + CmRow.ClientId + ".htm' style='text-decoration:none; text-transform: uppercase; '>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; " + CmRow.CompanyName + "</a><br>\n";
                            }

                        }
                       
                        
                    }
                   
                    
                    sbHtmlTemplete.Replace("@@Anunciantes@@", this.replaceAccentsToHtmlCodes(Anunciantes));
                    sbHtmlTemplete.Replace("@@Distribuidor@@", "");
                    Anunciantes = "";
                    this.saveHtmlFile(mc.BRANDID+ ".htm", sbHtmlTemplete.ToString());
                }
                
            } 

        }

    }
    
    public void getHtmlProductsClients(int editionId, int clientTypeId)
    {
        //get Alphabet from Database:

        NewGuiaTableAdapters.AlphabetClientsTableAdapter alphabetAdp = new NewGuiaTableAdapters.AlphabetClientsTableAdapter();
        NewGuia.AlphabethDataTable alphabetTable = alphabetAdp.GetAlphabetAnun(editionId, clientTypeId);
        
        String Productos = "";
    
        foreach (DataRow aRow in alphabetTable.Rows)
        {
            NewGuia.AlphabethRow alphabetRow = (NewGuia.AlphabethRow)aRow;
            //Get Products from Database
            
            NewGuiaTableAdapters.GetClientsAlphabetTableAdapter spAdp = new NewGuiaTableAdapters.GetClientsAlphabetTableAdapter();
            NewGuia.ClientsDataTable spTable = spAdp.GetAnunciante(editionId, alphabetRow.LETTER.ToString(), clientTypeId);

            //Get the html file template to save the correspond letter html file

            foreach (DataRow spRow in spTable.Rows)
            {
                StringBuilder sbHtmlTemplete = new StringBuilder(this.getHtmlTemplate());
                NewGuia.ClientsRow clientRow = (NewGuia.ClientsRow)spRow;

                sbHtmlTemplete.Replace("@@Titulo@@", this.replaceAccentsToHtmlCodes("<title>" + clientRow.CompanyName + " - Productos</title>"));
                sbHtmlTemplete.Replace("@@Parrafo@@", this.replaceAccentsToHtmlCodes("<p class='MainLetter'>PRODUCTOS DE " + clientRow.CompanyName + "</p>\n <b><hr></b><br><br> "));

                NewGuiaTableAdapters.ProductsTableAdapter prTA = new NewGuiaTableAdapters.ProductsTableAdapter();
                NewGuia.ProductsDataTable prDT = prTA.GetProductClients(editionId, clientRow.ClientId, 2);
                
                foreach (DataRow drPr in prDT.Rows)
                {
                    NewGuia.ProductsRow prRow = (NewGuia.ProductsRow)drPr;
                                       
                    Productos+= "<span class='ProductName'>"+prRow.PRODUCTNAME+"</span><br><br>\n";

                    NewGuiaTableAdapters.SubProductsTableAdapter spTA = new NewGuiaTableAdapters.SubProductsTableAdapter();
                    NewGuia.SubProductsDataTable spDT = spTA.GetSubProducts(editionId, clientRow.ClientId, prRow.PRODUCTID);

                    foreach (DataRow drSP in spDT.Rows)
                    {
                        NewGuia.SubProductsRow SupRow = (NewGuia.SubProductsRow)drSP;
                        Productos += "\n <span class='SubProduct'> &#8226;&nbsp;"+SupRow.PRODUCTNAME+"</span><br><br>";
                    }
                   
                }

                sbHtmlTemplete.Replace("@@Productos@@", this.replaceAccentsToHtmlCodes(Productos));
                this.saveHtmlFile(clientRow.ClientId + ".htm", sbHtmlTemplete.ToString());
                Productos = "";
                
            }

        }

    }

    public void getHtmlProductosHospitalarios(int editionId)
    {

        NewGuiaTableAdapters.DatosClientsTableAdapter dcTA = new NewGuiaTableAdapters.DatosClientsTableAdapter();
        NewGuia.DatosClientsDataTable dcDT = dcTA.GetDatosCliente(editionId, 2);

        foreach (DataRow dr in dcDT.Rows)
        {

            NewGuia.DatosClientsRow dcRow = (NewGuia.DatosClientsRow)dr;
  
            NewGuiaTableAdapters.DrugsTableAdapter drTA = new NewGuiaTableAdapters.DrugsTableAdapter();
            NewGuia.DrugsDataTable drDT = drTA.GetMedicamHosp(editionId, dcRow.ClientId);

            foreach (DataRow drDR in drDT.Rows)
            {
                string mails = "", webs = "";
                NewGuia.DrugsRow drRow = (NewGuia.DrugsRow)drDR;
                StringBuilder sbHtmlTemplete = new StringBuilder(this.getHtmlTemplate());
                sbHtmlTemplete.Replace("@@titulo@@", this.replaceAccentsToHtmlCodes("<title>" + drRow.ProductName + "</title>"));

                if (dcRow.IsImageNull())
                {
                    sbHtmlTemplete.Replace("@@image@@", "");
                }
                else
                {
                    sbHtmlTemplete.Replace("@@image@@", this.replaceAccentsToHtmlCodes("<p align='left'>&nbsp;<img border='0' src='../logos/" + dcRow.Image + "'></p>"));
                }

                
                sbHtmlTemplete.Replace("@@laboratorio@@", this.replaceAccentsToHtmlCodes("<span class='CompanyName'>" + dcRow.CompanyName + "</span>"));
                sbHtmlTemplete.Replace("@@direccion@@", this.replaceAccentsToHtmlCodes("<br>" + dcRow.Address + "<br>" + dcRow.Suburb));
                if (!dcRow.IsCityNull())
                {
                    sbHtmlTemplete.Replace("@@ciudad@@", this.replaceAccentsToHtmlCodes("<br>" + dcRow.City));
                }
                if (!dcRow.IsPHONENull())
                {
                    sbHtmlTemplete.Replace("@@telefono@@", this.replaceAccentsToHtmlCodes("<br>" + dcRow.PHONE));
                }

                if (!dcRow.IsPHONEFAXNull())
                {
                    sbHtmlTemplete.Replace("@@telfax@@", this.replaceAccentsToHtmlCodes("<br>" + dcRow.PHONEFAX));
                }
                if (!dcRow.IsFAXNull())
                {
                    sbHtmlTemplete.Replace("@@fax@@", this.replaceAccentsToHtmlCodes("<br>" + dcRow.FAX));
                }
                if (!dcRow.IsLADANull())
                {
                    sbHtmlTemplete.Replace("@@lada@@", this.replaceAccentsToHtmlCodes("<br>" + dcRow.LADA));
                }
                if (!dcRow.IsOXIDOMNull())
                {
                    sbHtmlTemplete.Replace("@@oxidom@@", this.replaceAccentsToHtmlCodes("<br>" + dcRow.OXIDOM));
                }

                if (!dcRow.IsEmailNull())
                {


                    if (dcRow.Email.Contains(";"))
                    {
                        Char[] spl = { ';' };
                        String[] arr = dcRow.Email.Split(spl);

                        foreach (String item in arr)
                        {
                            mails += "<a href='mailto:" + item.Replace(" ", "") + "'><span style='text-decoration: none'>" + item.Replace(" ", "") + "</span></a>;&nbsp;";

                        }
                        mails = mails.Substring(0, mails.Length - 7);
                        if (mails.Length > 5)
                        {


                            sbHtmlTemplete.Replace("@@email@@", this.replaceAccentsToHtmlCodes("<br>Email: " + mails));
                        }
                    }
                    else
                    {
                        if (dcRow.Email.Length > 5)
                        {


                            sbHtmlTemplete.Replace("@@email@@", this.replaceAccentsToHtmlCodes("<br>Email: <a href='mailto:" + dcRow.Email.Replace(" ", "") + "'><span style='text-decoration: none'>" + dcRow.Email.Replace(" ", "") + "</span></a>"));
                        }
                    }
                }
                if (!dcRow.IsWebNull())
                {


                    if (dcRow.Web.Contains(";"))
                    {
                        Char[] spl = { ';' };
                        String[] arr = dcRow.Web.Split(spl);
                        foreach (String item in arr)
                        {
                            webs += "<a href='http:" + item.Replace(" ", "") + "' target='_BLANK'><span style='text-decoration: none'>" + item.Replace(" ", "") + "</span></a></span>;&nbsp;";
                        }
                        webs = webs.Substring(0, webs.Length - 7);
                        if (webs.Length > 5)
                        {


                            sbHtmlTemplete.Replace("@@web@@", this.replaceAccentsToHtmlCodes("<br>Web: " + webs));
                        }
                    }
                    else
                    {
                        if (dcRow.Web.Length > 5)
                        {

                            sbHtmlTemplete.Replace("@@web@@", this.replaceAccentsToHtmlCodes("<br>Web: <a href='http:" + dcRow.Web.Replace(" ", "") + "' target='_BLANK'><span style='text-decoration: none'>" + dcRow.Web.Replace(" ", "") + "</span></a></span>"));
                        }
                    }

                }

                sbHtmlTemplete.Replace("@@producto@@", this.replaceAccentsToHtmlCodes("<div class='Lab_Data'> \n <span class='MedProdName'>" + drRow.ProductName + "<br> \n <i>" + drRow.ActiveSubstance + "</i></span><br>"));
                sbHtmlTemplete.Replace("@@forma@@", this.replaceAccentsToHtmlCodes("<span class='MedProdData'>" + drRow.PharmaForm + "<br>" + drRow.Presentation + "</span> \n </div>"));
                sbHtmlTemplete.Replace("@@web@@", "");
                sbHtmlTemplete.Replace("@@email@@", "");
                sbHtmlTemplete.Replace("@@oxidom@@", "");
                sbHtmlTemplete.Replace("@@lada@@", "");
                sbHtmlTemplete.Replace("@@telefono@@", "");
                sbHtmlTemplete.Replace("@@Ciudad@@", "");
                sbHtmlTemplete.Replace("@@telfax@@", "");
                sbHtmlTemplete.Replace("@@fax@@", "");
                this.saveHtmlFile(dcRow.ClientId + "_" + drRow.ProductId + "_" + drRow.PharmaFormId + "_" + drRow.PresentationId + ".htm", this.replaceAccentsToHtmlCodes(sbHtmlTemplete.ToString()));

            }

        }
    }

    public void getHtmlSucursalesAnunciantes(int editionId)
    {

        //get Alphabet from Database:


        NewGuiaTableAdapters.AlphabetClientsTableAdapter alphabetAdp = new NewGuiaTableAdapters.AlphabetClientsTableAdapter();
        NewGuia.AlphabethDataTable alphabetTable = alphabetAdp.GetAlphabetAnun(editionId, 1);

        String htmlsucur = "";
        string mails = "";
        string webs = "";
        //Get Products from Database

        NewGuiaTableAdapters.DatosClientsTableAdapter spAdp = new NewGuiaTableAdapters.DatosClientsTableAdapter();
        NewGuia.DatosClientsDataTable spTable = spAdp.GetDatosCliente(editionId, 1);

        //Get the html file template to save the correspond letter html file
        foreach (DataRow spRow in spTable.Rows)
        {
            StringBuilder sbHtmlTemplete = new StringBuilder(this.getHtmlTemplate());
            NewGuia.DatosClientsRow clientRow = (NewGuia.DatosClientsRow)spRow;


            NewGuiaTableAdapters.DatosClientsTableAdapter dcTA = new NewGuiaTableAdapters.DatosClientsTableAdapter();
            NewGuia.DatosClientsDataTable dcDT = dcTA.GetDatosClienteByParentId(editionId, 1, clientRow.ClientId);

            sbHtmlTemplete.Replace("@@titulo@@", "<title>" + clientRow.CompanyName + " - Sucursales</title>");
            foreach (DataRow dcDR in dcDT.Rows)
            {
                NewGuia.DatosClientsRow dcRow = (NewGuia.DatosClientsRow)dcDR;
                htmlsucur += "<span class='BranchName'>" + dcRow.CompanyName + "</span><br>";
                htmlsucur += "\n\n <br><div class='Lab_Data'> <span>";
                if (!dcRow.IsCityNull())
                {
                    htmlsucur += "<b>" + dcRow.StateName.ToUpper() + "</b>";
                }
                if (dcRow.Address.Length > 0)
                {
                    htmlsucur += "<br>" + dcRow.Address;
                    //htmlsucur += "<br>" +  replace dcRow.Address +  Replace(" ", "") ;
                }
                if (!dcRow.IsSuburbNull())
                {
                    if (dcRow.Suburb.Length > 0)
                    {
                        htmlsucur += "<br>" + dcRow.Suburb;
                    }

                }
                if(dcRow.CP != "")
                {
                    htmlsucur += "<br>" + dcRow.CP;
                }

                if (!dcRow.IsCityNull())
                {
                    htmlsucur += "<br>" + dcRow.City;
                }
                if (!dcRow.IsPHONENull())
                {
                    htmlsucur += "<br> Tel&eacute;fono(s): " + dcRow.PHONE;
                }
                if (!dcRow.IsPHONEFAXNull())
                {
                    htmlsucur += "<br> Tel&eacute;fono / Fax: " + dcRow.PHONEFAX;
                }
                if (!dcRow.IsFAXNull())
                {
                    htmlsucur += "<br> Fax: " + dcRow.FAX;
                }
                if (!dcRow.IsLADANull())
                {
                    htmlsucur += "<br> Lada sin costo: " + dcRow.LADA;
                }
                if (!dcRow.IsOXIDOMNull())
                {
                    htmlsucur += "<br>" + dcRow.OXIDOM;
                }
                if (!dcRow.IsEmailNull())
                {
                    if (dcRow.Email.Contains(";"))
                    {
                        Char[] spl = { ';' };
                        String[] arr = dcRow.Email.Split(spl);

                        foreach (String item in arr)
                        {
                            mails += "<a href='mailto:" + item.Replace(" ", "") + "'><span style='text-decoration: none'>" + item.Replace(" ", "") + "</span></a>;&nbsp;";
                        }
                        mails = mails.Substring(0, mails.Length - 7);
                        if (mails.Length > 5)
                        {
                            htmlsucur += "<br> Email: " + mails;
                        }
                    }
                    else
                    {
                        if (dcRow.Email.Length > 5)
                        {
                            htmlsucur += "<br>Email: <a href='mailto:" + dcRow.Email.Replace(" ", "") + "'><span style='text-decoration: none'>" + dcRow.Email.Replace(" ", "") + "</span></a>";
                        }
                    }
                }
                if (!dcRow.IsWebNull())
                {
                    if (dcRow.Web.Contains(";"))
                    {
                        Char[] spl = { ';' };
                        String[] arr = dcRow.Web.Split(spl);
                        foreach (String item in arr)
                        {
                            webs += "<a href='http:" + item.Replace(" ", "") + "' target='_BLANK'><span style='text-decoration: none'>" + item.Replace(" ", "") + "</span></a></span>;&nbsp;";
                        }
                        webs = webs.Substring(0, webs.Length - 7);
                        if (webs.Length > 5)
                        {
                            htmlsucur += "<br>Web: " + webs;
                        }
                    }
                    else
                    {
                        if (dcRow.Web.Length > 5)
                        {
                            htmlsucur += "<br>Web: <a href='http:" + dcRow.Web.Replace(" ", "") + "' target='_BLANK'><span style='text-decoration: none'>" + dcRow.Web.Replace(" ", "") + "</span></a></span>";
                        }
                    }
                }
                webs = ""; mails = "";
                htmlsucur += "</div><br><br>";
            }

            sbHtmlTemplete.Replace("@@Sucursales@@", this.replaceAccentsToHtmlCodes(htmlsucur));
            htmlsucur = "";

            this.saveHtmlFile(clientRow.ClientId + ".htm", sbHtmlTemplete.ToString());

        }


    }


    ////////////////////// Indices Anunciantes Internacionales


    public void getHtmlClientsInternacional(int editionid, int tipoInt)
    {
        
        NewGuiaTableAdapters.AlphabetClientsTableAdapter alTA = new NewGuiaTableAdapters.AlphabetClientsTableAdapter();
        NewGuia.AlphabethDataTable alDT = alTA.GetAlphabetInternacional(editionid, tipoInt);

        string htmlletras = "";
        string htmlclientes = "";
        int cont=0;
        foreach (DataRow drAL in alDT.Rows)
        {
            StringBuilder sbHtmlTemplete = new StringBuilder(this.getHtmlTemplate());
            NewGuia.AlphabethRow alRow2 = (NewGuia.AlphabethRow)drAL;

            NewGuiaTableAdapters.GetClientsAlphabetTableAdapter icTA = new NewGuiaTableAdapters.GetClientsAlphabetTableAdapter();
            NewGuia.ClientsDataTable icDT = icTA.GetClientsInternacional(editionid, alRow2.LETTER.ToString(), tipoInt);

            foreach (DataRow aaRow in alDT.Rows)
            {
                NewGuia.AlphabethRow alRow = (NewGuia.AlphabethRow)aaRow;
                cont++;
                NewGuia.AlphabethRow aalphabetRow = (NewGuia.AlphabethRow)aaRow;
                htmlletras += "<th width='20' height='20' scope='col'><a href='" + alRow.LETTER + "-gral.htm' title='" + alRow.LETTER + "'target='adentro'><img src='../letras/" + alRow.LETTER + "1.gif' style='border:none'  name='Image1" + cont + "' id='Image1" + cont + "'  onmouseover='MM_swapImage('Image1" + cont + "','','../letras/" + alRow.LETTER + "2.gif',1)' onmouseout='MM_swapImgRestore()' /></a></th>";
            }
            cont = 0;
            sbHtmlTemplete.Replace("@@titulo@@", "<title>"+alRow2.LETTER+"</title>");
            sbHtmlTemplete.Replace("@@parrafo@@", "<p class='MainLetter'>- "+alRow2.LETTER+" -</p>");
            sbHtmlTemplete.Replace("@@letras@@",this.replaceAccentsToHtmlCodes(htmlletras));
            foreach (DataRow icDR in icDT.Rows)
            {
                NewGuia.ClientsRow icRow = (NewGuia.ClientsRow)icDR;

                htmlclientes += "\n <a class=\"Companies\"  href='" + icRow.ClientId + ".htm' style='text-decoration:none'> " + icRow.CompanyName + "</a><br>";
            }
            sbHtmlTemplete.Replace("@@clientes@@",this.replaceAccentsToHtmlCodes(htmlclientes)); 
             this.saveHtmlFile(alRow2.LETTER + "-gral.htm", sbHtmlTemplete.ToString());
            htmlclientes = "";
            htmlletras = "";
        }
     
    }

    public void getHtmlDataClientInternacional(int editionId, int tipoInt)
        {
            String mails = "";
            String webs = "";
            //Get Products from Database
        
            NewGuiaTableAdapters.DatosClientsTableAdapter dciTA = new NewGuiaTableAdapters.DatosClientsTableAdapter();
            NewGuia.DatosClientsDataTable dciDT = dciTA.GetDatosCliente(editionId, tipoInt);
            //Get the html file template to save the correspond letter html file

            foreach (DataRow spRow in dciDT.Rows)
            {
                StringBuilder sbHtmlTemplete = new StringBuilder(this.getHtmlTemplate());
                NewGuia.DatosClientsRow clientRow = (NewGuia.DatosClientsRow)spRow;
                sbHtmlTemplete.Replace("@@Titulo@@", this.replaceAccentsToHtmlCodes("<title>" + clientRow.CompanyName + "</title>"));
                sbHtmlTemplete.Replace("@@Span@@", this.replaceAccentsToHtmlCodes("<span class='CompanyName'>" + clientRow.CompanyName + "</span>"));
                sbHtmlTemplete.Replace("@@Direccion@@", this.replaceAccentsToHtmlCodes("<br> \n <div class=\"infoCompany\"> \n <span class='Lab_DataI'> " + clientRow.Address));
                try
                {

                    sbHtmlTemplete.Replace("@@Colonia@@", this.replaceAccentsToHtmlCodes("<br>" + clientRow.City));
                }
                catch (Exception)
                {
                    if (clientRow.IsImageNull())
                    {
                        sbHtmlTemplete.Replace("@@Colonia@@", "");
                    }
                    else
                    {
                        sbHtmlTemplete.Replace("@@Colonia@@", this.replaceAccentsToHtmlCodes("<br>" + clientRow.CountryName));
                    }
                }

                if (!clientRow.IsPHONENull())
                {
                    sbHtmlTemplete.Replace("@@Telefono@@", this.replaceAccentsToHtmlCodes("<br>" + clientRow.PHONE));
                }
                else
                {
                    sbHtmlTemplete.Replace("@@Telefono@@", "");
                }

                if (!clientRow.IsPHONEFAXNull())
                {
                    sbHtmlTemplete.Replace("@@TelFax@@", this.replaceAccentsToHtmlCodes("<br>" + clientRow.PHONEFAX));
                }
                else
                {
                    sbHtmlTemplete.Replace("@@TelFax@@", "");
                }
                
                if (!clientRow.IsFAXNull())
                {
                    sbHtmlTemplete.Replace("@@Fax@@", this.replaceAccentsToHtmlCodes("<br>" + clientRow.FAX));
                }
                else
                {
                    sbHtmlTemplete.Replace("@@Fax@@", "");
                }

                if (!clientRow.IsLADANull())
                {
                    sbHtmlTemplete.Replace("@@Lada@@", this.replaceAccentsToHtmlCodes("<br>" + clientRow.LADA));
                }
                else
                {
                    sbHtmlTemplete.Replace("@@Lada@@", "");
                }

                if (!clientRow.IsEmailNull())

                { 
                    if (clientRow.Email.Contains(";"))
                    {
                        Char[] spl = { ';' };
                        String[] arr = clientRow.Email.Split(spl);

                        foreach (String item in arr)
                        {
                            mails += "<a href='mailto:" + item.Replace(" ", "") + "'><span style='text-decoration: none'>" + item.Replace(" ", "") + "</span></a>;&nbsp;";

                        }
                        mails = mails.Substring(0, mails.Length - 7);
                        if (mails.Length > 5)
                        {


                            sbHtmlTemplete.Replace("@@Email@@", this.replaceAccentsToHtmlCodes("<br>Email: " + mails));
                        }
                    }
                    else
                    {
                        if (clientRow.Email.Length > 5)
                        {


                            sbHtmlTemplete.Replace("@@Email@@", this.replaceAccentsToHtmlCodes("<br>Email: <a href='mailto:" + clientRow.Email.Replace(" ", "") + "'><span style='text-decoration: none'>" + clientRow.Email.Replace(" ", "") + "</span></a>"));
                        }
                    }
                    sbHtmlTemplete.Replace("@@Email@@", "");
                }
           

                if (!clientRow.IsWebNull())
                {


                    if (clientRow.Web.Contains(";"))
                    {
                        Char[] spl = { ';' };
                        String[] arr = clientRow.Web.Split(spl);
                        foreach (String item in arr)
                        {
                            webs += "<a href='http:" + item.Replace(" ", "") + "' target='_BLANK'><span style='text-decoration: none'>" + item.Replace(" ", "") + "</span></a></span>;&nbsp;";
                        }
                        webs = webs.Substring(0, webs.Length - 7);
                        if (webs.Length > 5)
                        {


                            sbHtmlTemplete.Replace("@@Web@@", this.replaceAccentsToHtmlCodes("<br>Web: " + webs));
                        }
                    }
                    else
                    {
                        if (clientRow.Web.Length > 5)
                        {

                            sbHtmlTemplete.Replace("@@Web@@", this.replaceAccentsToHtmlCodes("<br>Web: <a href='http:" + clientRow.Web.Replace(" ", "") + "' target='_BLANK'><span style='text-decoration: none'>" + clientRow.Web.Replace(" ", "") + "</span></a> "));
                        }
                    }

                    sbHtmlTemplete.Replace("@@Web@@", "");
                }

                NewGuiaTableAdapters.ProductsTableAdapter ipTA = new NewGuiaTableAdapters.ProductsTableAdapter();
                NewGuia.ProductsDataTable ipDT = ipTA.GetProductClients(editionId, clientRow.ClientId, 3);


                String htmlProds = "";
                foreach (DataRow drPR in ipDT.Rows)
                {
                    NewGuia.ProductsRow prRow = (NewGuia.ProductsRow)drPR;
                    htmlProds += "<span class='ProductI'>" + prRow.PRODUCTNAME + "</span><br>";

                }
                if (htmlProds.Length > 5)
                {
                    sbHtmlTemplete.Replace("@@Productos@@", "<br>\n <div class='StateTitleMH'>Producto(s):</div><br>\n " + this.replaceAccentsToHtmlCodes(htmlProds));
                }

                sbHtmlTemplete.Replace("@@Web@@", "");
                sbHtmlTemplete.Replace("@@Email@@", "");
                sbHtmlTemplete.Replace("@@Oxidom@@", "");
                sbHtmlTemplete.Replace("@@Lada@@", "");
                sbHtmlTemplete.Replace("@@Telefono@@", "");
                sbHtmlTemplete.Replace("@@Ciudad@@", "");
                sbHtmlTemplete.Replace("@@TelFax@@", "");
                sbHtmlTemplete.Replace("@@Fax@@", "");
                sbHtmlTemplete.Replace("@@Productos@@", "");

                mails = "";
                webs = "";

                //    sbHtmlTemplete.Append("</td></tr></table>");

                //sbHtmlTemplete.Append("</div></body></html>");


                this.saveHtmlFile(clientRow.ClientId + ".htm", sbHtmlTemplete.ToString());

            }


        }


    ////////////////////// Indices Especialidades

    public void getHtmlSpecialities(int editionId)
    {

        //get Alphabet from Database:

        NewGuiaTableAdapters.SpecialitiesTableAdapter spTA = new NewGuiaTableAdapters.SpecialitiesTableAdapter();
        NewGuia.SpecialitiesDataTable spDT = spTA.GetSpecialities(editionId);
        
        String htmlespecialidades = "";
        StringBuilder sbHtmlTemplete = new StringBuilder(this.getHtmlTemplate());
        foreach (DataRow spdr in spDT.Rows)
        {

            NewGuia.SpecialitiesRow spRow = (NewGuia.SpecialitiesRow)spdr;

            htmlespecialidades += "<a class=\"Specialities\" target='_self' href='gral" + spRow.SpecialityId + ".htm' style='text-decoration:none'>" + spRow.Description + "</a><br>";
            //Get Products from Database

        }
        sbHtmlTemplete.Replace("@@especialidades@@", this.replaceAccentsToHtmlCodes(htmlespecialidades));
        this.saveHtmlFile("especialidades.htm", this.replaceAccentsToHtmlCodes(sbHtmlTemplete.ToString()));

    }

    public void getHtmlStatesSpeciality(int editionId)
    {
        //get Alphabet from Database:

        NewGuiaTableAdapters.SpecialitiesTableAdapter spTA = new NewGuiaTableAdapters.SpecialitiesTableAdapter();
        NewGuia.SpecialitiesDataTable spDT = spTA.GetSpecialities(editionId);

        String htmlESTADOS = "";

        foreach (DataRow spdr in spDT.Rows)
        {
            StringBuilder sbHtmlTemplete = new StringBuilder(this.getHtmlTemplate());
            NewGuia.SpecialitiesRow spRow = (NewGuia.SpecialitiesRow)spdr;

            NewGuiaTableAdapters.SpecialitiesStatesTableAdapter stTA = new NewGuiaTableAdapters.SpecialitiesStatesTableAdapter();
            NewGuia.SpecialitiesStatesDataTable stDT = stTA.GetEstadosEspecialidad(editionId, spRow.SpecialityId);

            sbHtmlTemplete.Replace("@@titulo@@", "<title>" + spRow.Description + "</title>");
            sbHtmlTemplete.Replace("@@parrafo@@", "<p class='MainLetter'>" + spRow.Description + " </p><b><hr></b><BR>");
            foreach (DataRow sdDR in stDT.Rows)
            {
                NewGuia.SpecialitiesStatesRow stRow = (NewGuia.SpecialitiesStatesRow)sdDR;
                htmlESTADOS += "<a class=\"Specialities\" target='_self' href='Gral_" + spRow.SpecialityId + "/" + stRow.StateId + ".htm' style='text-decoration:none'>" + stRow.StateName + "</a><br>";
            }


            //Get Products from Database
            sbHtmlTemplete.Replace("@@estado@@", this.replaceAccentsToHtmlCodes(htmlESTADOS));
            this.saveHtmlFile("gral" + spRow.SpecialityId + ".htm", this.replaceAccentsToHtmlCodes(sbHtmlTemplete.ToString()));
            htmlESTADOS = "";
        }

    }
    
    public void getHtmlClientStateSpeciality(int editionId)
    {
        //get Alphabet from Database:

        NewGuiaTableAdapters.SpecialitiesTableAdapter spTA = new NewGuiaTableAdapters.SpecialitiesTableAdapter();
        NewGuia.SpecialitiesDataTable spDT = spTA.GetSpecialities(editionId);

        String htmlclientes = "";
        int value = 1;
        foreach (DataRow spdr in spDT.Rows)
        {
            
            NewGuia.SpecialitiesRow spRow = (NewGuia.SpecialitiesRow)spdr;
            
            NewGuiaTableAdapters.SpecialitiesStatesTableAdapter stTA = new NewGuiaTableAdapters.SpecialitiesStatesTableAdapter();
            NewGuia.SpecialitiesStatesDataTable stDT = stTA.GetEstadosEspecialidad(editionId,spRow.SpecialityId);

            foreach (DataRow sdDR in stDT.Rows)
            {
                StringBuilder sbHtmlTemplete = new StringBuilder(this.getHtmlTemplate());
                NewGuia.SpecialitiesStatesRow stRow = (NewGuia.SpecialitiesStatesRow)sdDR;

                NewGuiaTableAdapters.ClientsBySpacialityTableAdapter clTA = new NewGuiaTableAdapters.ClientsBySpacialityTableAdapter();
                NewGuia.ClientsBySpacialityDataTable clDT = clTA.GetClientsByStateBySpeciality(editionId, stRow.StateId, spRow.SpecialityId);

                sbHtmlTemplete.Replace("@@titulo@@", "<title>"+spRow.Description+" -" + stRow.StateName + "</title>");
                sbHtmlTemplete.Replace("@@parrafo@@", "<p class='MainLetter'>"+spRow.Description+"</p><b><hr></b>\n <br><div class='StateTitle'>"+stRow.StateName+"</div><BR>");
                foreach (DataRow clDR in clDT.Rows)
                {
                    value++;
                    NewGuia.ClientsBySpacialityRow clRow = (NewGuia.ClientsBySpacialityRow)clDR;

                    if (clRow.ClientId ==	1	|| clRow.ClientId ==	2	|| clRow.ClientId ==	3	|| clRow.ClientId ==	4	||  clRow.ClientId ==	5	||
                                clRow.ClientId ==	6	||
                                clRow.ClientId ==	7	||
                                clRow.ClientId ==	9	||
                                clRow.ClientId ==	10	||
                                clRow.ClientId ==	12	||
                                clRow.ClientId ==	13	||
                                clRow.ClientId ==	14	||
                                clRow.ClientId ==	15	||
                                clRow.ClientId ==	16	||
                                clRow.ClientId ==	17	||
                                clRow.ClientId ==	18	||
                                clRow.ClientId ==	19	||
                                clRow.ClientId ==	22	||
                                clRow.ClientId ==	23	||
                                clRow.ClientId ==	24	||
                                clRow.ClientId ==	25	||
                                clRow.ClientId ==	26	||
                                clRow.ClientId ==	27	||
                                clRow.ClientId ==	28	||
                                clRow.ClientId ==	29	||
                                clRow.ClientId ==	30	||
                                clRow.ClientId ==	31	||
                                clRow.ClientId ==	32	||
                                clRow.ClientId ==	33	||
                                clRow.ClientId ==	34	||
                                clRow.ClientId ==	35	||
                                clRow.ClientId ==	36	||
                                clRow.ClientId ==	37	||
                                clRow.ClientId ==	38	||
                                clRow.ClientId ==	40	||
                                clRow.ClientId ==	41	||
                                clRow.ClientId ==	42	||
                                clRow.ClientId ==	43	||
                                clRow.ClientId ==	44	||
                                clRow.ClientId ==	45	||
                                clRow.ClientId ==	46	||
                                clRow.ClientId ==	47	||
                                clRow.ClientId ==	48	||
                                clRow.ClientId ==	50	||
                                clRow.ClientId ==	53	||
                                clRow.ClientId ==	54	||
                                clRow.ClientId ==	55	||
                                clRow.ClientId ==	56	||
                                clRow.ClientId ==	58	||
                                clRow.ClientId ==	59	||
                                clRow.ClientId ==	60	||
                                clRow.ClientId ==	62	||
                                clRow.ClientId ==	63	||
                                clRow.ClientId ==	64	||
                                clRow.ClientId ==	65	||
                                clRow.ClientId ==	66	||
                                clRow.ClientId ==	67	||
                                clRow.ClientId ==	68	||
                                clRow.ClientId ==	69	||
                                clRow.ClientId ==	70	||
                                clRow.ClientId ==	71	||
                                clRow.ClientId ==	72	||
                                clRow.ClientId ==	73	||
                                clRow.ClientId ==	74	||
                                clRow.ClientId ==	75	||
                                clRow.ClientId ==	76	||
                                clRow.ClientId ==	77	||
                                clRow.ClientId ==	78	||
                                clRow.ClientId ==	79	||
                                clRow.ClientId ==	80	||
                                clRow.ClientId ==	81	||
                                clRow.ClientId ==	82	||
                                clRow.ClientId ==	83	||
                                clRow.ClientId ==	84	||
                                clRow.ClientId ==	85	||
                                clRow.ClientId ==	87	||
                                clRow.ClientId ==	88	||
                                clRow.ClientId ==	89	||
                                clRow.ClientId ==	90	||
                                clRow.ClientId ==	91	||
                                clRow.ClientId ==	92	||
                                clRow.ClientId ==	94	||
                                clRow.ClientId ==	95	||
                                clRow.ClientId ==	96	||
                                clRow.ClientId ==	97	||
                                clRow.ClientId ==	99	||
                                clRow.ClientId ==	100	||
                                clRow.ClientId ==	101	||
                                clRow.ClientId ==	102	||
                                clRow.ClientId ==	103	||
                                clRow.ClientId ==	105	||
                                clRow.ClientId ==	106	||
                                clRow.ClientId ==	107	||
                                clRow.ClientId ==	108	||
                                clRow.ClientId ==	109	||
                                clRow.ClientId ==	110	||
                                clRow.ClientId ==	111	||
                                clRow.ClientId ==	112	||
                                clRow.ClientId ==	113	||
                                clRow.ClientId ==	115	||
                                clRow.ClientId ==	116	||
                                clRow.ClientId ==	117	||
                                clRow.ClientId ==	118	||
                                clRow.ClientId ==	119	||
                                clRow.ClientId ==	120	||
                                clRow.ClientId ==	122	||
                                clRow.ClientId ==	123	||
                                clRow.ClientId ==	124	||
                                clRow.ClientId ==	125	||
                                clRow.ClientId ==	126	||
                                clRow.ClientId ==	127	||
                                clRow.ClientId ==	128	||
                                clRow.ClientId ==	130	||
                                clRow.ClientId ==	131	||
                                clRow.ClientId ==	132	||
                                clRow.ClientId ==	133	||
                                clRow.ClientId ==	134	||
                                clRow.ClientId ==	138	||
                                clRow.ClientId ==	593	||
                                clRow.ClientId ==	5930	||
                                clRow.ClientId ==	5931	||
                                clRow.ClientId ==	5932	||
                                clRow.ClientId ==	5933	||
                                clRow.ClientId ==	5934	||
                                clRow.ClientId ==	5935	||
                                clRow.ClientId ==	5936	||
                                clRow.ClientId ==	5937	||
                                clRow.ClientId ==	5938	||
                                clRow.ClientId ==	5939	||
                                clRow.ClientId ==	5979	||
                                clRow.ClientId ==	1288	||
                                clRow.ClientId ==	1891	||
                                clRow.ClientId ==	2657	||
                                clRow.ClientId ==	3505	||
                                clRow.ClientId ==	3664	||
                                clRow.ClientId ==	3868	||
                                clRow.ClientId ==	3932	||
                                clRow.ClientId ==	4666 )
                    {

                        htmlclientes += "\n <a  class=\"Specialities\" target='_self' href='../../sucursal/" + clRow.ClientId + ".htm'  style='text-decoration:none; text-transform:uppercase; '>" + clRow.CompanyName + "</a><br>";
                    }
                    else
                    {
                        htmlclientes += "\n <a class=\"Specialities\" target='_self' href='../../sucursal/" + clRow.ClientId + '_' + clRow.AddressId + ".htm'  style='text-decoration:none'>" + clRow.CompanyName + "</a><br>";
                    }
                }
                sbHtmlTemplete.Replace("@@cliente@@", this.replaceAccentsToHtmlCodes(htmlclientes));

                FileStream htmlFile = new FileStream("C:/GUIA61/Disco/Especialidades/Gral_"+spRow.SpecialityId + "\\" + stRow.StateId+ ".htm", FileMode.Create);
                StreamWriter sw = new StreamWriter(htmlFile);
                sw.Write(this.replaceAccentsToHtmlCodes(sbHtmlTemplete.ToString()));
                sw.Close();
                sw.Dispose();
             
                htmlclientes = "";    
            }


            //Get Products from Database
            
        }

    }

    public void getHtmlClientByState(int editionId)
    {
        //get Alphabet from Database:

        NewGuiaTableAdapters.SpecialitiesTableAdapter spTA = new NewGuiaTableAdapters.SpecialitiesTableAdapter();
        NewGuia.SpecialitiesDataTable spDT = spTA.GetSpecialities(editionId);

        String mails = "";
        String webs = "";
        int value = 1;
        foreach (DataRow spdr in spDT.Rows)
        {

            NewGuia.SpecialitiesRow spRow = (NewGuia.SpecialitiesRow)spdr;
            NewGuiaTableAdapters.SpecialitiesStatesTableAdapter stTA = new NewGuiaTableAdapters.SpecialitiesStatesTableAdapter();
            NewGuia.SpecialitiesStatesDataTable stDT = stTA.GetEstadosEspecialidad(editionId, spRow.SpecialityId);

            foreach (DataRow sdDR in stDT.Rows)
            {

                NewGuia.SpecialitiesStatesRow stRow = (NewGuia.SpecialitiesStatesRow)sdDR;
                NewGuiaTableAdapters.ClientsBySpacialityTableAdapter clTA = new NewGuiaTableAdapters.ClientsBySpacialityTableAdapter();
                NewGuia.ClientsBySpacialityDataTable clDT = clTA.GetClientsByStateBySpeciality(editionId, stRow.StateId, spRow.SpecialityId);

                foreach (DataRow clDR in clDT.Rows)
                {
                    value++;


                    NewGuia.ClientsBySpacialityRow clRow = (NewGuia.ClientsBySpacialityRow)clDR;
                    NewGuiaTableAdapters.DatosClientsTableAdapter dcDA = new NewGuiaTableAdapters.DatosClientsTableAdapter();
                    NewGuia.DatosClientsDataTable dcDT = dcDA.GetDatosClienteByClientId(editionId,  clRow.ClientId);

                    foreach (DataRow dcDR in dcDT.Rows)
                    {
                        StringBuilder sbHtmlTemplete = new StringBuilder(this.getHtmlTemplate());
                        NewGuia.DatosClientsRow dcRow = (NewGuia.DatosClientsRow)dcDR;


                        sbHtmlTemplete.Replace("@@titulo@@", this.replaceAccentsToHtmlCodes("<title>"+dcRow.CompanyName+" - Sucursales</title>"));
                        sbHtmlTemplete.Replace("@@span@@", this.replaceAccentsToHtmlCodes("<span class='BranchName'>"+dcRow.CompanyName+"</span><br>"));

                        if (!(dcRow.StateName == null))
                        {
                            sbHtmlTemplete.Replace("@@estado@@", this.replaceAccentsToHtmlCodes(dcRow.StateName));
                        }

                        
                        
                        if (!(dcRow.Address==null))
                        {
                            sbHtmlTemplete.Replace("@@direccion@@", this.replaceAccentsToHtmlCodes("<br>" + dcRow.Address));    
                        }
                        if (!dcRow.IsSuburbNull())
                        {
                        sbHtmlTemplete.Replace("@@colonia@@", this.replaceAccentsToHtmlCodes("<br>" + dcRow.Suburb));    
                        }

                        if (dcRow.CP !="")
                        {
                            sbHtmlTemplete.Replace("@@cp@@", this.replaceAccentsToHtmlCodes("<br>" + dcRow.CP));
                        }


                        if (!dcRow.IsCityNull())
                        {
                            sbHtmlTemplete.Replace("@@ciudad@@", this.replaceAccentsToHtmlCodes("<br>" + dcRow.City));    
                        }
                        if (!dcRow.IsPHONENull())
                        {
                            sbHtmlTemplete.Replace("@@telefono@@", this.replaceAccentsToHtmlCodes("<br>" + dcRow.PHONE));    
                        }
                        if (!dcRow.IsFAXNull())
                        {
                            sbHtmlTemplete.Replace("@@fax@@", this.replaceAccentsToHtmlCodes("<br>" + dcRow.FAX));    
                        }
                        if (!dcRow.IsPHONEFAXNull())
                        {
                            sbHtmlTemplete.Replace("@@telfax@@", this.replaceAccentsToHtmlCodes("<br>" + dcRow.PHONEFAX));    
                        }
                        if (!dcRow.IsLADANull())
                        {
                            sbHtmlTemplete.Replace("@@lada@@", this.replaceAccentsToHtmlCodes("<br>" + dcRow.LADA));    
                        }
                        if (!dcRow.IsOXIDOMNull())
                        {
                            sbHtmlTemplete.Replace("@@oxidom@@", this.replaceAccentsToHtmlCodes("<br>" + dcRow.OXIDOM));    
                        }

                        
                        if (!dcRow.IsEmailNull())
                        {


                            if (dcRow.Email.Contains(";"))
                            {
                                Char[] spl = { ';' };
                                String[] arr = dcRow.Email.Split(spl);

                                foreach (String item in arr)
                                {
                                    mails += "<a href='mailto:" + item.Replace(" ", "") + "'><span style='text-decoration: none'>" + item.Replace(" ", "") + "</span></a>;&nbsp;";

                                }
                                mails = mails.Substring(0, mails.Length - 7);
                                if (mails.Length > 5)
                                {


                                    sbHtmlTemplete.Replace("@@email@@", this.replaceAccentsToHtmlCodes("<br>Email: " + mails));
                                }
                            }
                            else
                            {
                                if (dcRow.Email.Length > 5)
                                {


                                    sbHtmlTemplete.Replace("@@email@@", this.replaceAccentsToHtmlCodes("<br>Email: <a href='mailto:" + dcRow.Email.Replace(" ", "") + "'><span style='text-decoration: none'>" + dcRow.Email.Replace(" ", "") + "</span></a>"));
                                }
                            }
                        }
                        if (!dcRow.IsWebNull())
                        {


                            if (dcRow.Web.Contains(";"))
                            {
                                Char[] spl = { ';' };
                                String[] arr = dcRow.Web.Split(spl);
                                foreach (String item in arr)
                                {
                                    webs += "<a href='http:" + item.Replace(" ", "") + "' target='_BLANK'><span style='text-decoration: none'>" + item.Replace(" ", "") + "</span></a></span>;&nbsp;";
                                }
                                webs = webs.Substring(0, webs.Length - 7);
                                if (webs.Length > 5)
                                {


                                    sbHtmlTemplete.Replace("@@web@@", this.replaceAccentsToHtmlCodes("<br>Web: " + webs));
                                }
                            }
                            else
                            {
                                if (dcRow.Web.Length > 5)
                                {

                                    sbHtmlTemplete.Replace("@@web@@", this.replaceAccentsToHtmlCodes("<br>Web: <a href='http:" + dcRow.Web.Replace(" ", "") + "' target='_BLANK'><span style='text-decoration: none'>" + dcRow.Web.Replace(" ", "") + "</span></a></span>"));
                                }
                            }
                        }
                        sbHtmlTemplete.Replace("@@web@@", "");
                        sbHtmlTemplete.Replace("@@colonia@@", "");
                        sbHtmlTemplete.Replace("@@email@@", "");
                        sbHtmlTemplete.Replace("@@oxidom@@", "");
                        sbHtmlTemplete.Replace("@@lada@@", "");
                        sbHtmlTemplete.Replace("@@telefono@@", "");
                        sbHtmlTemplete.Replace("@@ciudad@@", "");
                        sbHtmlTemplete.Replace("@@cp@@", "");
                        sbHtmlTemplete.Replace("@@telfax@@", "");
                        sbHtmlTemplete.Replace("@@fax@@", "");
                        sbHtmlTemplete.Replace("@@productos@@", "");
                        sbHtmlTemplete.Replace("@@hospitalarios@@", "");
                       // string nameF = dcRow.ClientId.ToString() + '_'+ dcRow.AddressId.ToString();
                        if (dcRow.ClientId ==	1	||
                                        dcRow.ClientId ==	2	||
                                        dcRow.ClientId ==	3	||
                                        dcRow.ClientId ==	4	||
                                        dcRow.ClientId ==	5	||
                                        dcRow.ClientId ==	6	||
                                        dcRow.ClientId ==	7	||
                                        dcRow.ClientId ==	9	||
                                        dcRow.ClientId ==	10	||
                                        dcRow.ClientId ==	12	||
                                        dcRow.ClientId ==	13	||
                                        dcRow.ClientId ==	14	||
                                        dcRow.ClientId ==	15	||
                                        dcRow.ClientId ==	16	||
                                        dcRow.ClientId ==	17	||
                                        dcRow.ClientId ==	18	||
                                        dcRow.ClientId ==	19	||
                                        dcRow.ClientId ==	22	||
                                        dcRow.ClientId ==	23	||
                                        dcRow.ClientId ==	24	||
                                        dcRow.ClientId ==	25	||
                                        dcRow.ClientId ==	26	||
                                        dcRow.ClientId ==	27	||
                                        dcRow.ClientId ==	28	||
                                        dcRow.ClientId ==	29	||
                                        dcRow.ClientId ==	30	||
                                        dcRow.ClientId ==	31	||
                                        dcRow.ClientId ==	32	||
                                        dcRow.ClientId ==	33	||
                                        dcRow.ClientId ==	34	||
                                        dcRow.ClientId ==	35	||
                                        dcRow.ClientId ==	36	||
                                        dcRow.ClientId ==	37	||
                                        dcRow.ClientId ==	38	||
                                        dcRow.ClientId ==	40	||
                                        dcRow.ClientId ==	41	||
                                        dcRow.ClientId ==	42	||
                                        dcRow.ClientId ==	43	||
                                        dcRow.ClientId ==	44	||
                                        dcRow.ClientId ==	45	||
                                        dcRow.ClientId ==	46	||
                                        dcRow.ClientId ==	47	||
                                        dcRow.ClientId ==	48	||
                                        dcRow.ClientId ==	50	||
                                        dcRow.ClientId ==	53	||
                                        dcRow.ClientId ==	54	||
                                        dcRow.ClientId ==	55	||
                                        dcRow.ClientId ==	56	||
                                        dcRow.ClientId ==	58	||
                                        dcRow.ClientId ==	59	||
                                        dcRow.ClientId ==	60	||
                                        dcRow.ClientId ==	62	||
                                        dcRow.ClientId ==	63	||
                                        dcRow.ClientId ==	64	||
                                        dcRow.ClientId ==	65	||
                                        dcRow.ClientId ==	66	||
                                        dcRow.ClientId ==	67	||
                                        dcRow.ClientId ==	68	||
                                        dcRow.ClientId ==	69	||
                                        dcRow.ClientId ==	70	||
                                        dcRow.ClientId ==	71	||
                                        dcRow.ClientId ==	72	||
                                        dcRow.ClientId ==	73	||
                                        dcRow.ClientId ==	74	||
                                        dcRow.ClientId ==	75	||
                                        dcRow.ClientId ==	76	||
                                        dcRow.ClientId ==	77	||
                                        dcRow.ClientId ==	78	||
                                        dcRow.ClientId ==	79	||
                                        dcRow.ClientId ==	80	||
                                        dcRow.ClientId ==	81	||
                                        dcRow.ClientId ==	82	||
                                        dcRow.ClientId ==	83	||
                                        dcRow.ClientId ==	84	||
                                        dcRow.ClientId ==	85	||
                                        dcRow.ClientId ==	87	||
                                        dcRow.ClientId ==	88	||
                                        dcRow.ClientId ==	89	||
                                        dcRow.ClientId ==	90	||
                                        dcRow.ClientId ==	91	||
                                        dcRow.ClientId ==	92	||
                                        dcRow.ClientId ==	94	||
                                        dcRow.ClientId ==	95	||
                                        dcRow.ClientId ==	96	||
                                        dcRow.ClientId ==	97	||
                                        dcRow.ClientId ==	99	||
                                        dcRow.ClientId ==	100	||
                                        dcRow.ClientId ==	101	||
                                        dcRow.ClientId ==	102	||
                                        dcRow.ClientId ==	103	||
                                        dcRow.ClientId ==	105	||
                                        dcRow.ClientId ==	106	||
                                        dcRow.ClientId ==	107	||
                                        dcRow.ClientId ==	108	||
                                        dcRow.ClientId ==	109	||
                                        dcRow.ClientId ==	110	||
                                        dcRow.ClientId ==	111	||
                                        dcRow.ClientId ==	112	||
                                        dcRow.ClientId ==	113	||
                                        dcRow.ClientId ==	115	||
                                        dcRow.ClientId ==	116	||
                                        dcRow.ClientId ==	117	||
                                        dcRow.ClientId ==	118	||
                                        dcRow.ClientId ==	119	||
                                        dcRow.ClientId ==	120	||
                                        dcRow.ClientId ==	122	||
                                        dcRow.ClientId ==	123	||
                                        dcRow.ClientId ==	124	||
                                        dcRow.ClientId ==	125	||
                                        dcRow.ClientId ==	126	||
                                        dcRow.ClientId ==	127	||
                                        dcRow.ClientId ==	128	||
                                        dcRow.ClientId ==	130	||
                                        dcRow.ClientId ==	131	||
                                        dcRow.ClientId ==	132	||
                                        dcRow.ClientId ==	133	||
                                        dcRow.ClientId ==	134	||
                                        dcRow.ClientId ==	138	||
                                        dcRow.ClientId ==	593	||
                                        dcRow.ClientId ==	5930	||
                                        dcRow.ClientId ==	5931	||
                                        dcRow.ClientId ==	5932	||
                                        dcRow.ClientId ==	5933	||
                                        dcRow.ClientId ==	5934	||
                                        dcRow.ClientId ==	5935	||
                                        dcRow.ClientId ==	5936	||
                                        dcRow.ClientId ==	5937	||
                                        dcRow.ClientId ==	5938	||
                                        dcRow.ClientId ==	5939	||
                                        dcRow.ClientId ==	5979	||
                                        dcRow.ClientId ==	1288	||
                                        dcRow.ClientId ==	1891	||
                                        dcRow.ClientId ==	2657	||
                                        dcRow.ClientId ==	3505	||
                                        dcRow.ClientId ==	3664	||
                                        dcRow.ClientId ==	3868	||
                                        dcRow.ClientId ==	3932	||
                                        dcRow.ClientId ==	4666	
                                        )
                        {
                            this.saveHtmlFile(dcRow.ClientId + ".htm", this.replaceAccentsToHtmlCodes(sbHtmlTemplete.ToString()));
                            mails = "";
                            webs = "";
                        }
                        else
                        {
                            this.saveHtmlFile(dcRow.ClientId.ToString() + '_' + dcRow.AddressId.ToString() + ".htm", this.replaceAccentsToHtmlCodes(sbHtmlTemplete.ToString()));
                              mails = "";
                              webs = "";
                        }
                            
                            
                    }
                }

            }
            
        }

    }


    ////////////////////// Indice Productos y Servicios

    public void getHtmlproductServicios(int editionId)
    {

        //get Alphabet from Database:

        NewGuiaTableAdapters.AlphabetClientsTableAdapter alphabetAdp = new NewGuiaTableAdapters.AlphabetClientsTableAdapter();
        NewGuia.AlphabethDataTable alphabetTable = alphabetAdp.GetAlphabetProds(editionId);

        String htmlLetras = "";
        String htmlproductos = "";
        int val = 0;

        foreach (DataRow aRow in alphabetTable.Rows)
        {
            NewGuia.AlphabethRow alphabetRow = (NewGuia.AlphabethRow)aRow;
            StringBuilder sbHtmlTemplete = new StringBuilder(this.getHtmlTemplate());
            sbHtmlTemplete.Replace("@@titulo@@", this.replaceAccentsToHtmlCodes("<title>" + alphabetRow.LETTER + "</title>"));
            sbHtmlTemplete.Replace("@@parrafo@@", this.replaceAccentsToHtmlCodes("<p class='MainLetter'>- " + alphabetRow.LETTER + " -</p>"));

            foreach (DataRow aaRow in alphabetTable.Rows)
            {
                val++;
                NewGuia.AlphabethRow aalphabetRow = (NewGuia.AlphabethRow)aaRow;
                htmlLetras += "<th width='20' height='20' scope='col'><a href='" + aalphabetRow.LETTER + "-general.htm' title='" + aalphabetRow.LETTER + "'target='adentro'><img src='../letras/" + aalphabetRow.LETTER + "1.gif' style='border:none'  name='Image1" + val + "' id='Image1" + val + "'  onmouseover='MM_swapImage('Image1" + val + "','','../letras/" + aalphabetRow.LETTER + "2.gif',1)' onmouseout='MM_swapImgRestore()' /></a></th>\n";
            }
            val = 0;

            //Get Products from Database

            NewGuiaTableAdapters.ProductsTableAdapter prAdp = new NewGuiaTableAdapters.ProductsTableAdapter();
            NewGuia.ProductsDataTable prTable = prAdp.GetProductsServicios(editionId, alphabetRow.LETTER);

            //Get the html file template to save the correspond letter html file
            foreach (DataRow spRow in prTable.Rows)
            {
                NewGuia.ProductsRow prRow = (NewGuia.ProductsRow)spRow;
                htmlproductos += "<a class=\"marcasL\" href='prods/" + prRow.PRODUCTID + ".htm' style='text-decoration:none'>    " + prRow.PRODUCTNAME + "</a><br>";
            }

            sbHtmlTemplete.Replace("@@letras@@", this.replaceAccentsToHtmlCodes(htmlLetras));
            sbHtmlTemplete.Replace("@@producto@@", this.replaceAccentsToHtmlCodes(htmlproductos));


            this.saveHtmlFile(alphabetRow.LETTER + "-general.htm", this.replaceAccentsToHtmlCodes(sbHtmlTemplete.ToString()));
            htmlproductos = "";
            htmlLetras = "";

        }

    }

    public void getHtmlproductServiciosSubProductos(int editionId)
    {

        //get Alphabet from Database:
        
        NewGuiaTableAdapters.AlphabetClientsTableAdapter alphabetAdp = new NewGuiaTableAdapters.AlphabetClientsTableAdapter();
        NewGuia.AlphabethDataTable alphabetTable = alphabetAdp.GetAlphabetProds(editionId);

        String htmlproductos = "";

        foreach (DataRow aRow in alphabetTable.Rows)
        {
            NewGuia.AlphabethRow alphabetRow = (NewGuia.AlphabethRow)aRow;
            //Get Products from Database

            NewGuiaTableAdapters.ProductsTableAdapter prTA = new NewGuiaTableAdapters.ProductsTableAdapter();
            NewGuia.ProductsDataTable prDT = prTA.GetProductsServicios(editionId, alphabetRow.LETTER);
            //Get the html file template to save the correspond letter html file

            foreach (DataRow spRow in prDT.Rows)
            {
                StringBuilder sbHtmlTemplete = new StringBuilder(this.getHtmlTemplate());

                NewGuia.ProductsRow prRow = (NewGuia.ProductsRow)spRow;
                NewGuiaTableAdapters.ClientsProductsSubProductsTableAdapter cpsTA = new NewGuiaTableAdapters.ClientsProductsSubProductsTableAdapter();


                sbHtmlTemplete.Replace("@@titulo@@", this.replaceAccentsToHtmlCodes("<title>" + prRow.PRODUCTNAME + "</title>"));
                sbHtmlTemplete.Replace("@@parrafo@@", this.replaceAccentsToHtmlCodes("<p class='MainLetter'>" + prRow.PRODUCTNAME + "</p>"));

                NewGuia.ClientsProductsSubProductsDataTable cpsDT = cpsTA.GetClientesByProducts(editionId, prRow.PRODUCTID);

                if (cpsDT.Rows.Count == 0)

                //if(cpsDT.DescriptionColumn == null)
                {
                    NewGuiaTableAdapters.Clients1TableAdapter cl1TA = new NewGuiaTableAdapters.Clients1TableAdapter();
                    NewGuia.Clients1DataTable cl1DT = cl1TA.GetClientProduct(editionId, prRow.PRODUCTID);

                    foreach (DataRow cl1DR in cl1DT.Rows)
                    {
                        NewGuia.Clients1Row cl1Row = (NewGuia.Clients1Row)cl1DR;
                        htmlproductos += " <a class=\"infoCompanyPYS\"  href='../../anunciantes/" + cl1Row.ClientId + ".htm' style='text-decoration:none'>" + cl1Row.CompanyName + "</a><br><br>";

                    }
                }
                else
                {
                    foreach (DataRow cpsDR in cpsDT.Rows)
                    {

                        NewGuia.ClientsProductsSubProductsRow cpsRow = (NewGuia.ClientsProductsSubProductsRow)cpsDR;

                        if (cpsRow.PRODUCTID != 0)
                        {
                            htmlproductos += "<span class='ProductName'>" + cpsRow.Description + "</span><br><br>";
                        }


                        NewGuiaTableAdapters.Clients1TableAdapter cl1TA = new NewGuiaTableAdapters.Clients1TableAdapter();
                        NewGuia.Clients1DataTable cl1DT = cl1TA.GetClientProduct(editionId, cpsRow.PRODUCTID);

                        foreach (DataRow cl1DR in cl1DT.Rows)
                        {
                            NewGuia.Clients1Row cl1Row = (NewGuia.Clients1Row)cl1DR;
                            htmlproductos += " <a class=\"infoCompanyPYS\" href='../../anunciantes/" + cl1Row.ClientId + ".htm' style='text-decoration:none; text-transform:uppercase;'>" + cl1Row.CompanyName + "</a><br><br>";

                        }


                    }
                }


                sbHtmlTemplete.Replace("@@producto@@", this.replaceAccentsToHtmlCodes(htmlproductos));
                this.saveHtmlFile(prRow.PRODUCTID + ".htm", this.replaceAccentsToHtmlCodes(sbHtmlTemplete.ToString()));
                htmlproductos = "";

            }

        }

    }
   
    ////////////////////// Indice marcas


    public void getHtmlIndiceMarcas(int editionId)
    {

        //get Alphabet from Database:
        
        NewGuiaTableAdapters.AlphabetClientsTableAdapter alphabetAdp = new NewGuiaTableAdapters.AlphabetClientsTableAdapter();
        NewGuia.AlphabethDataTable alphabetTable = alphabetAdp.GetAlphabetMarcas(editionId);

        String htmlLetras = "";
        String htmlAnunciantes = "";
        int val = 0;

        foreach (DataRow aRow in alphabetTable.Rows)
        {
            foreach (DataRow aaRow in alphabetTable.Rows)
            {
                val++;
                NewGuia.AlphabethRow aalphabetRow = (NewGuia.AlphabethRow)aaRow;
                htmlLetras += "<th width='20' height='20' scope='col'><a href='" + aalphabetRow[0].ToString() + "-gral.htm' title='" + aalphabetRow[0].ToString() + "'target='adentro'><img src='../letras/" + aalphabetRow[0].ToString() + "1.gif' style='border:none'  name='Image1" + val.ToString() + "' id='Image1" + val.ToString() + "'  onmouseover='MM_swapImage('Image1" + val.ToString() + "','','../letras/" + aalphabetRow[0].ToString() + "2.gif',1)' onmouseout='MM_swapImgRestore()' /></a></th>\n";
            }
            val = 0;

            NewGuia.AlphabethRow alphabetRow = (NewGuia.AlphabethRow)aRow;
            StringBuilder sbHtmlTemplete = new StringBuilder(this.getHtmlTemplate());


            //Get Products from Database 

            NewGuiaTableAdapters.BrandsTableAdapter bTA = new NewGuiaTableAdapters.BrandsTableAdapter();
            NewGuia.BrandsDataTable bDT = bTA.GetMarcas(editionId, alphabetRow.LETTER);


            //Get the html file template to save the correspond letter html file
            foreach (DataRow spRow in bDT.Rows)
            {
                NewGuia.BrandsRow clientRow = (NewGuia.BrandsRow)spRow;
                htmlAnunciantes += "\n <a class=\"marcasL\" href='../marcas/marcas/" + clientRow.BrandId + ".htm' style='text-decoration:none'>" + clientRow.BrandName + "</a><br>";
            }
            sbHtmlTemplete.Replace("@@titulo@@", "<title>" + alphabetRow.LETTER + "</title>");
            sbHtmlTemplete.Replace("@@letras@@", this.replaceAccentsToHtmlCodes(htmlLetras));
            sbHtmlTemplete.Replace("@@parrafo@@", this.replaceAccentsToHtmlCodes("<p class='MainLetter' align='center'>- " + alphabetRow.LETTER + " -</p>"));
            sbHtmlTemplete.Replace("@@marca@@", this.replaceAccentsToHtmlCodes(htmlAnunciantes));
            //    sbHtmlTemplete.Append("</td></tr></table>");

            //sbHtmlTemplete.Append("</div></body></html>");


            this.saveHtmlFile(alphabetRow.LETTER + "-gral.htm", this.replaceAccentsToHtmlCodes(sbHtmlTemplete.ToString()));
            htmlAnunciantes = "";
            htmlLetras = "";

        }

    }


    ////////////////////// Indice Zona Geografica


    public void getHtmlIndiceGeografic(int editionId)
    {

        //get Alphabet from Database:
        String htmlestados = "";
    
        NewGuiaTableAdapters.SpecialitiesStatesTableAdapter stTA = new NewGuiaTableAdapters.SpecialitiesStatesTableAdapter();
        NewGuia.SpecialitiesStatesDataTable stDT = stTA.GetEstadosAnunciantes(editionId,0);

        foreach (DataRow dr in stDT.Rows)
        {
            NewGuia.SpecialitiesStatesRow stRow = (NewGuia.SpecialitiesStatesRow)dr;
            htmlestados += "<a class=\"Companies\" target='_self' href='" + stRow.StateId + ".htm' style='text-decoration:none'>" + stRow.StateName + "</a><br>";
        }
        
            StringBuilder sbHtmlTemplete = new StringBuilder(this.getHtmlTemplate());

            sbHtmlTemplete.Replace("@@estado@@", this.replaceAccentsToHtmlCodes(htmlestados));
            
            this.saveHtmlFile("edo_anun.htm", this.replaceAccentsToHtmlCodes(sbHtmlTemplete.ToString()));
        
        

    }

    public void getHtmlIndiceGeograficXestado(int editionId)
    {

        //get Alphabet from Database:
        String htmlclientes = "";

        NewGuiaTableAdapters.SpecialitiesStatesTableAdapter stTA = new NewGuiaTableAdapters.SpecialitiesStatesTableAdapter();
        NewGuia.SpecialitiesStatesDataTable stDT = stTA.GetEstadosAnunciantes(editionId, 0);

        foreach (DataRow dr in stDT.Rows)
        {
            NewGuia.SpecialitiesStatesRow stRow = (NewGuia.SpecialitiesStatesRow)dr;


            NewGuiaTableAdapters.GetClientsAlphabetTableAdapter clTA = new NewGuiaTableAdapters.GetClientsAlphabetTableAdapter();
            NewGuia.ClientsDataTable clDT = clTA.GetClientesXestado(editionId, stRow.StateId);

            foreach (DataRow drcl in clDT.Rows)
            {
                NewGuia.ClientsRow clRow = (NewGuia.ClientsRow)drcl;
                htmlclientes += "<a class=\"Companies\" href='../" + clRow.ClientId + ".htm' style='text-decoration:none'>" + clRow.CompanyName + "</a><br>";
                
            }
            StringBuilder sbHtmlTemplete = new StringBuilder(this.getHtmlTemplate());

        sbHtmlTemplete.Replace("@@titulo@@", "<title>"+stRow.StateName+"</title>");
        sbHtmlTemplete.Replace("@@parrafo@@", "<div class='StateTitle'>   "+stRow.StateName.ToUpper()+"  </div><BR>");
        sbHtmlTemplete.Replace("@@anunciante@@", this.replaceAccentsToHtmlCodes(htmlclientes));
        this.saveHtmlFile(stRow.StateId+".htm", this.replaceAccentsToHtmlCodes(sbHtmlTemplete.ToString())); 
            htmlclientes = "";


        }

            }

   

    


    #endregion

}
