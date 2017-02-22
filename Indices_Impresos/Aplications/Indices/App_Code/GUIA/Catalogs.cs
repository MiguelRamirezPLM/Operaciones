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

/// <summary>
/// Descripción breve de Catalogs
/// </summary>
public class Catalogs : GUIADataAccessAdapter<object>
{
	private Catalogs()
	{ }
    #region Productos

    public DataTable getLetter()
    {
        StringBuilder sb = new StringBuilder();

        sb.Append("\nSELECT  top 1 *  ");
        sb.Append("\nFROM Alphabet ");
        sb.Append("\n ORDER BY 2 ");

        DataSet ds = Catalogs.GuiaProdInstanceDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());

        return ds.Tables[0];
    }

    public DataTable getProducts(string letra)
    {
        StringBuilder sb = new StringBuilder();

        //sb.Append("Select DISTINCT PRODUCTO,p.ID From [PRODUCTOS Y SUBPRODUCTOS] p ");
        //sb.Append("\nInner join [tabla1] t On(p.ID = t.[PRODUCTOS Y SUBPRODUCTOSid_])");
        //sb.Append("\nInner join [EMPRESAS] e On(t.[EMPRESASid_] = e.[ID]) ");
        //sb.Append("\nWhere [EDICIÓN] = '56' AND SUBPRODUCTO LIKE '*#*' AND PRODUCTO COLLATE SQL_Latin1_General_CP1_CI_AI LIKE ('" + letra + "%') ");
        //sb.Append("\nOrder by PRODUCTO ");

          sb.Append("\nselect distinct p.productname,p.productId ");
          sb.Append("\nfrom clientProducts cp ");
          sb.Append("\ninner join products p on(cp.productId = p.productId)");
          sb.Append("\ninner join clients  c on (cp.clientid = c.clientId)  ");
          sb.Append("\nwhere  c.EditionId=5 and cp.active=1 and p.active=1 and c.active=1 and p.productname COLLATE SQL_Latin1_General_CP1_CI_AI LIKE ('" + letra + "%') and c.page is not null order by ProductName");
                        

        //DataSet ds = Catalogs.GuiaProdInstanceDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());
        DataSet ds = IndiceGuia.GuiaAliInstanceDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());
        return ds.Tables[0];
    }

    public DataTable getSubProducts(string prod)//(int productId)
    {
        StringBuilder sb = new StringBuilder();

        //sb.Append("Select DISTINCT SUBPRODUCTO,p.ID From [PRODUCTOS Y SUBPRODUCTOS] p");
        //sb.Append("\nInner join [tabla1] t On(p.ID = t.[PRODUCTOS Y SUBPRODUCTOSid_])");
        //sb.Append("\nInner join [EMPRESAS] e On(t.[EMPRESASid_] = e.[ID]) ");
        //sb.Append("\nWhere [EDICIÓN] = '56' AND PRODUCTO  = '" + prod + "' AND SUBPRODUCTO NOT LIKE '*#*'");
        //sb.Append("\nOrder by SUBPRODUCTO ");

        sb.Append("\n select distinct sp.description,p.productId,sp.subproductId");
        sb.Append("\nfrom clientProducts cp ");
        sb.Append("\ninner join products p on(cp.productId = p.productId)");
        sb.Append("\ninner join clients  c on (cp.clientid = c.clientId) ");
        sb.Append("\nleft join subproducts sp on (cp.subproductId =sp.subproductId)");
        sb.Append("\nwhere   c.EditionId=5 and cp.active=1 and p.active=1 and c.active=1 and p.productId="+prod.ToString()+" and cp.subproductId is not null and c.page is not null ");




        //DataSet ds = Catalogs.GuiaProdInstanceDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());
        DataSet ds = IndiceGuia.GuiaAliInstanceDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());
        return ds.Tables[0];
    }

    public DataTable getBrandsByProdId(int prodid)
    {
        StringBuilder sb = new StringBuilder();

        //sb.Append("Select DISTINCT [Nombre CORTO de la Empresa - GUIA 54] AS EMPRESA From [EMPRESAS] e");
        //sb.Append("\nInner join [tabla1] t On(e.ID = t.[EMPRESASid_]) ");
        //sb.Append("\nWhere [EDICIÓN] = '56' AND t.[PRODUCTOS Y SUBPRODUCTOSid_] = " + prodid);
        //sb.Append("\nOrder by 1");

        sb.Append(" select distinct c.shortname as companyname,c.page");
        sb.Append("\nfrom clientProducts cp ");
        sb.Append("\ninner join products p on(cp.productId = p.productId)");
        sb.Append("\ninner join clients  c on (cp.clientid = c.clientId) ");
        sb.Append("\nwhere   c.EditionId=5 and cp.active=1 and p.active=1 and c.active=1 and p.productId="+ prodid.ToString()+" and cp.subproductId is null and c.page is not null");
         
          



        //DataSet ds = Catalogs.GuiaProdInstanceDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());
        DataSet ds = IndiceGuia.GuiaAliInstanceDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());

        return ds.Tables[0];
    }
    public DataTable getSBrandsByProdId(int prodid,int subpro)
    {
        StringBuilder sb = new StringBuilder();

        sb.Append(" select distinct c.shortname as companyName,c.page");
        sb.Append("\nfrom clientProducts cp ");
        sb.Append("\ninner join products p on(cp.productId = p.productId)");
        sb.Append("\ninner join clients  c on (cp.clientid = c.clientId) ");
        sb.Append("\nwhere   c.EditionId=5 and cp.active=1 and p.active=1  and p.productId=" + prodid.ToString() + " and cp.subproductId=" + subpro.ToString() + " and c.active=1 and c.page is not null");

        DataSet ds = IndiceGuia.GuiaAliInstanceDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());

        return ds.Tables[0];
    }

    #endregion

    #region Medicamentos Hospitalarios

    public DataTable getLaboratories()
    {
        StringBuilder sb = new StringBuilder();

        sb.Append("SELECT DISTINCT C.CLIENTID, C.COMPANYNAME,C.[ADDRESS],C.SUBURB,C.POSTALCODE,C.CITY, ");
        sb.Append("\n S.StateNAME,T.PHONE,TF.PHONEFAX,F.FAX,L.LADA,o.Lads,C.EMAIL,C.WEB ");
        sb.Append("\nFROM CLIENTS C ");
        sb.Append("\nINNER JOIN DRUGS M ON(C.CLIENTID = M.CLIENTID) ");
        sb.Append("\nINNER JOIN STATES S ON(C.STATEID = S.STATEID) ");
        sb.Append("\nLEFT JOIN TELEFONO T ON(C.CLIENTID = T.CLIENTID) ");
        sb.Append("\nLEFT JOIN TELFAX TF ON(C.CLIENTID = TF.CLIENTID) ");
        sb.Append("\nLEFT JOIN FAX F ON(C.CLIENTID = F.CLIENTID) ");
        sb.Append("\nLEFT JOIN LADA L ON(C.CLIENTID = L.CLIENTID)");
        sb.Append("\n left join oxidom O on(c.clientId = o.clientId)");
        sb.Append("\nWHERE EDITIONID = 5 ");
        sb.Append("\nORDER BY COMPANYNAME ");

        DataSet ds = Catalogs.GuiaAliInstanceDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());

        return ds.Tables[0];
    }

    public DataTable getSucursales(int clientId)
    {
        StringBuilder sb = new StringBuilder();

        sb.Append("SELECT DISTINCT C.CLIENTID, C.COMPANYNAME,C.[ADDRESS],C.SUBURB,C.POSTALCODE,C.CITY, ");
        sb.Append("\n S.StateNAME,T.PHONE,TF.PHONEFAX,F.FAX,L.LADA,o.Lads,C.EMAIL,C.WEB ");
        sb.Append("\nFROM CLIENTS C ");
        sb.Append("\nLEFT JOIN DRUGS M ON(C.CLIENTID = M.CLIENTID) ");
        sb.Append("\nLEFT JOIN STATES S ON(C.STATEID = S.STATEID) ");
        sb.Append("\nLEFT JOIN TELEFONO T ON(C.CLIENTID = T.CLIENTID) ");
        sb.Append("\nLEFT JOIN TELFAX TF ON(C.CLIENTID = TF.CLIENTID) ");
        sb.Append("\nLEFT JOIN FAX F ON(C.CLIENTID = F.CLIENTID) ");
        sb.Append("\nLEFT JOIN LADA L ON(C.CLIENTID = L.CLIENTID)");
        sb.Append("\n left join oxidom O on(c.clientId = o.clientId)");
        sb.Append("\nWHERE EDITIONID = 5 AND C.CLIENTCODE IN (270,528,536) AND C.CLIENTIDPARENT = " + clientId);
        sb.Append("\nORDER BY COMPANYNAME ");

        DataSet ds = Catalogs.GuiaAliInstanceDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());

        return ds.Tables[0];
    }


    public DataTable getPhones(int clientId)
    {
        StringBuilder sb = new StringBuilder();

        sb.Append("SELECT DISTINCT CP.PHONETYPEID,CP.LADA,CP.NUMBER ");
        sb.Append("\nFROM CLIENTS C ");
        sb.Append("\nINNER JOIN CLIENTPHONES CP ON(C.CLIENTID = CP.CLIENTID) ");
        sb.Append("\nWHERE EDITIONID = 5 AND CP.CLIENTID = " + clientId);

        DataSet ds = Catalogs.GuiaAliInstanceDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());

        return ds.Tables[0];
    }

    public DataTable getMedicines(int clientId)
    {
        StringBuilder sb = new StringBuilder();

        sb.Append("SELECT DISTINCT M.DRUGNAME,M.ACTIVESUBSTANCE,M.PHARMACEUTICFORM,M.PRESENTATION ");
        sb.Append("\nFROM CLIENTS C ");
        sb.Append("\nINNER JOIN Drugs M ON(C.CLIENTID = M.CLIENTID) ");
        sb.Append("\nWHERE EDITIONID = 5 AND C.CLIENTID = " + clientId);
        sb.Append("\nORDER BY 1");

        DataSet ds = Catalogs.GuiaAliInstanceDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());

        return ds.Tables[0];
    }

    #endregion

    #region Anunciantes

    public DataTable getCompanies(string letter)
    {
        StringBuilder sb = new StringBuilder();

        sb.Append("SELECT DISTINCT C.CLIENTID, C.COMPANYNAME,C.[ADDRESS],C.SUBURB,C.POSTALCODE,C.CITY, ");
        sb.Append("\n S.StateNAME,T.PHONE,TF.PHONEFAX,F.FAX,L.LADA,o.Lads,C.EMAIL,C.WEB ");
        sb.Append("\nFROM CLIENTS C ");
        sb.Append("\nINNER JOIN STATES S ON(C.STATEID = S.STATEID) ");
        sb.Append("\nLEFT JOIN TELEFONO T ON(C.CLIENTID = T.CLIENTID) ");
        sb.Append("\nLEFT JOIN TELFAX TF ON(C.CLIENTID = TF.CLIENTID) ");
        sb.Append("\nLEFT JOIN FAX F ON(C.CLIENTID = F.CLIENTID) ");
        sb.Append("\nLEFT JOIN LADA L ON(C.CLIENTID = L.CLIENTID)");
        sb.Append("\n left join oxidom O on(c.clientId = o.clientId)");
        sb.Append("\nWHERE EDITIONID = 5 AND C.CLIENTTYPEID = 2 AND C.COMPANYNAME COLLATE SQL_Latin1_General_CP1_CI_AI LIKE ('" + letter + "%') ");
        sb.Append("\nORDER BY COMPANYNAME ");

        DataSet ds = Catalogs.GuiaAliInstanceDatabase.ExecuteDataSet(CommandType.Text,sb.ToString());

        return ds.Tables[0];
    }
    

    #endregion

    #region Directorio Internacional

    public DataTable getCompaniesInt(string letter)
    {
        StringBuilder sb = new StringBuilder();

        //sb.Append("SELECT DISTINCT C.CLIENTID, C.COMPANYNAME,C.[ADDRESS],C.POSTALCODE,C.CITY, ");
        //sb.Append("\n S.StateNAME,CT.COUNTRYNAME,T.PHONE,TF.PHONEFAX,F.FAX,L.LADA,C.EMAIL,C.WEB ");
        //sb.Append("\nFROM CLIENTS C ");
        //sb.Append("\nLEFT JOIN STATES S ON(C.STATEID = S.STATEID) ");
        //sb.Append("\nLEFT JOIN COUNTRIES CT ON(C.COUNTRYID = CT.COUNTRYID) ");
        //sb.Append("\nLEFT JOIN TELEFONO T ON(C.CLIENTID = T.CLIENTID) ");
        //sb.Append("\nLEFT JOIN TELFAX TF ON(C.CLIENTID = TF.CLIENTID) ");
        //sb.Append("\nLEFT JOIN FAX F ON(C.CLIENTID = F.CLIENTID) ");
        //sb.Append("\nLEFT JOIN LADA L ON(C.CLIENTID = L.CLIENTID)");
        //sb.Append("\nWHERE EDITIONID = 5 AND C.CLIENTTYPEID = 4 AND C.COMPANYNAME COLLATE SQL_Latin1_General_CP1_CI_AI LIKE ('" + letter + "%') ");
        //sb.Append("\nORDER BY COMPANYNAME ");


        //sb.Append("\n  SELECT DISTINCT C.CLIENTID, C.COMPANYNAME,C.[ADDRESS],C.POSTALCODE,C.CITY, ");
        //sb.Append("\n S.StateNAME,CT.COUNTRYNAME,Tmp.PHONE,TmtF.TelFAX,tmf.FAX,tml.Lads,C.EMAIL,C.WEB ");
        //sb.Append("\nFROM CLIENTS C ");
        //sb.Append("\n LEFT JOIN STATES S ON(C.STATEID = S.STATEID) ");
        //sb.Append("\n LEFT JOIN COUNTRIES CT ON(C.COUNTRYID = CT.COUNTRYID) ");
        //sb.Append("\nleft join( ");
        //sb.Append("\nSELECT 'Tel.: '+ NUMBER as Phone,ClientId");
        //sb.Append("\nFROM CLIENTPHONES ");
        //sb.Append("\nWHERE PHONETYPEID = 1	) tmp  on(c.clientId = tmp.clientId)");
        //sb.Append("\nleft join( ");
        //sb.Append("\nSELECT 'Fax.: '+ NUMBER as FAX,ClientId");
        //sb.Append("\nFROM CLIENTPHONES  ");
        //sb.Append("\nWHERE PHONETYPEID = 3	) tmf  on(c.clientId = tmf.clientId)");
        //sb.Append("\nleft join( ");
        //sb.Append("\nSELECT 'Tel/fax.: '+ NUMBER as telFAX,ClientId");
        //sb.Append("\nFROM CLIENTPHONES  ");
        //sb.Append("\nWHERE PHONETYPEID = 2	) tmtf  on(c.clientId = tmtf.clientId)");
        //sb.Append("\nleft join( ");
        //sb.Append("\nSELECT 'lada sin costo.: '+ NUMBER as Lads,ClientId");
        //sb.Append("\nFROM CLIENTPHONES  ");
        //sb.Append("\nWHERE PHONETYPEID = 4	) tml  on(c.clientId = tml.clientId)");

        //sb.Append("\n WHERE EDITIONID = 5 AND C.CLIENTTYPEID = 4 and clientIdParent is null AND C.COMPANYNAME COLLATE SQL_Latin1_General_CP1_CI_AI LIKE ('" + letter + "%') ");
        //sb.Append("\nORDER BY COMPANYNAME ");




        sb.Append("SELECT DISTINCT C.Orden, C.[Nombre de la EMPRESA INTERNACIONAL],C.[Dirección],C.[Código Postal],C.[Ciudad], ");
        sb.Append("\nc.[Estado],C.[País],c.[Teléfono],c.[Tel/Fax],c.FAX,C.[Lada sin costo],C.[E-mail],C.[página WEB] ");
        sb.Append("\nFROM DirectorioI C ");
        sb.Append("\nWHERE   C.[Nombre de la EMPRESA INTERNACIONAL]COLLATE SQL_Latin1_General_CP1_CI_AI LIKE ('" + letter + "%')");
        sb.Append("\nORDER BY c.[Nombre de la EMPRESA INTERNACIONAL]");




        DataSet ds = Catalogs.GuiaAliInstanceDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());

        return ds.Tables[0];
    }

    public DataTable getCompaniesByParent(int clientId)
    {
        StringBuilder sb = new StringBuilder();

        //sb.Append("SELECT DISTINCT C.CLIENTID, C.COMPANYNAME,C.[ADDRESS],C.SUBURB,C.POSTALCODE,C.CITY, ");
        //sb.Append("\n S.StateNAME,CT.COUNTRYNAME,T.PHONE,TF.PHONEFAX,F.FAX,L.LADA,C.EMAIL,C.WEB ");
        //sb.Append("\nFROM CLIENTS C ");
        //sb.Append("\nLEFT JOIN STATES S ON(C.STATEID = S.STATEID) ");
        //sb.Append("\nLEFT JOIN COUNTRIES CT ON(C.COUNTRYID = CT.COUNTRYID) ");
        //sb.Append("\nLEFT JOIN TELEFONO T ON(C.CLIENTID = T.CLIENTID) ");
        //sb.Append("\nLEFT JOIN TELFAX TF ON(C.CLIENTID = TF.CLIENTID) ");
        //sb.Append("\nLEFT JOIN FAX F ON(C.CLIENTID = F.CLIENTID) ");
        //sb.Append("\nLEFT JOIN LADA L ON(C.CLIENTID = L.CLIENTID)");
        //sb.Append("\nWHERE EDITIONID = 5 AND C.ClientId = " + clientId);
        //sb.Append("\nORDER BY COMPANYNAME ");

        //sb.Append("\n  SELECT DISTINCT C.CLIENTID, C.COMPANYNAME,C.[ADDRESS],C.POSTALCODE,C.CITY, ");
        //sb.Append("\n S.StateNAME,CT.COUNTRYNAME,Tmp.PHONE,TmtF.TelFAX,tmf.FAX,tml.Lads,C.EMAIL,C.WEB ");
        //sb.Append("\nFROM CLIENTS C ");
        //sb.Append("\n LEFT JOIN STATES S ON(C.STATEID = S.STATEID) ");
        //sb.Append("\n LEFT JOIN COUNTRIES CT ON(C.COUNTRYID = CT.COUNTRYID) ");
        //sb.Append("\nleft join( ");
        //sb.Append("\nSELECT 'Tel.: '+ NUMBER as Phone,ClientId");
        //sb.Append("\nFROM CLIENTPHONES ");
        //sb.Append("\nWHERE PHONETYPEID = 1	) tmp  on(c.clientId = tmp.clientId)");
        //sb.Append("\nleft join( ");
        //sb.Append("\nSELECT 'Fax.: '+ NUMBER as FAX,ClientId");
        //sb.Append("\nFROM CLIENTPHONES  ");
        //sb.Append("\nWHERE PHONETYPEID = 3	) tmf  on(c.clientId = tmf.clientId)");
        //sb.Append("\nleft join( ");
        //sb.Append("\nSELECT 'Tel/fax.: '+ NUMBER as telFAX,ClientId");
        //sb.Append("\nFROM CLIENTPHONES  ");
        //sb.Append("\nWHERE PHONETYPEID = 2	) tmtf  on(c.clientId = tmtf.clientId)");
        //sb.Append("\nleft join( ");
        //sb.Append("\nSELECT 'lada sin costo.: '+ NUMBER as Lads,ClientId");
        //sb.Append("\nFROM CLIENTPHONES  ");
        //sb.Append("\nWHERE PHONETYPEID = 4	) tml  on(c.clientId = tml.clientId)");
        //sb.Append("\nWHERE EDITIONID = 5 AND C.ClientIdParent = " + clientId);
        //sb.Append("\nORDER BY COMPANYNAME ");






        sb.Append("\nSELECT DISTINCT C.Orden, C.[Nombre de la EMPRESA INTERNACIONAL],C.[Dirección],C.[Código Postal],C.[Ciudad], ");
        sb.Append("\nc.[Estado],C.[País],c.[Teléfono],c.[Tel/Fax],c.FAX,C.[Lada sin costo],C.[E-mail],C.[página WEB] ");
        sb.Append("\nFROM DirectorioI C ");
        sb.Append("\nWHERE   C.Orden =" + clientId.ToString());
        sb.Append("\nORDER BY C.[Nombre de la EMPRESA INTERNACIONAL]");


        DataSet ds = Catalogs.GuiaAliInstanceDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());

        return ds.Tables[0];
    }

    public DataTable getProductByClient(int clientId)
    {
        StringBuilder sb = new StringBuilder();

        //sb.Append("SELECT DISTINCT P.PRODUCTID,P.PRODUCTNAME ");
        //sb.Append("\nFROM PRODUCTS P");
        //sb.Append("\nINNER JOIN CLIENTPRODUCTS CP ON(P.PRODUCTID = CP.PRODUCTID) ");
        //sb.Append("\nWHERE P.ACTIVE = 1 AND CP.ACTIVE = 1 and parentID is  null AND CP.CLIENTID = " + clientId);
        //sb.Append("\nORDER BY P.PRODUCTNAME ");


        sb.Append("\nSELECT DISTINCT P.PRODUCID,P.PRODUCTNAME ");
        sb.Append("\nFROM PRODUCTSDI  P");
        sb.Append("\nINNER JOIN CLIENTPRODUCTSDI CP ON(P.PRODUCID = CP.PRODUCTID) ");
        sb.Append("\n  WHERE CP.CLIENTID =" + clientId.ToString() + " and parentId is null");
        sb.Append("\nORDER BY P.PRODUCTNAME ");

        



        DataSet ds = Catalogs.GuiaAliInstanceDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());

        return ds.Tables[0];

    }

    public DataTable getProductByIdByClientId(int productId, int clientId)
    {
        StringBuilder sb = new StringBuilder();

        //sb.Append("SELECT DISTINCT P.PRODUCTID,P.PRODUCTNAME ");
        //sb.Append("\nFROM PRODUCTS P");
        //sb.Append("\nINNER JOIN CLIENTPRODUCTS CP ON(P.PRODUCTID = CP.PRODUCTID) ");
        //sb.Append("\nWHERE P.ACTIVE = 1 AND CP.ACTIVE = 1 AND CP.CLIENTID = " + clientId);
        //sb.Append("\nAND P.PARENTID = " + productId);
        //sb.Append("\nORDER BY P.PRODUCTNAME ");

        sb.Append("SELECT DISTINCT P.PRODUCID,P.PRODUCtNAME ");
        sb.Append("\nFROM PRODUCTSDI P");
        sb.Append("\nINNER JOIN CLIENTPRODUCTSDI CP ON(P.PRODUCID = CP.PRODUCTID) ");
        sb.Append("\nWHERE  CP.CLIENTID = " + clientId);
        sb.Append("\nAND P.PARENTID = " + productId);
        sb.Append("\nORDER BY P.PRODUCTNAME ");


        
        DataSet ds = Catalogs.GuiaAliInstanceDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());

        return ds.Tables[0];
    }

    //GUIA COLOMBIA
    public DataTable getSpecialitiesByEdition(int editionId)
    {
        StringBuilder sb = new StringBuilder();

        sb.Append("Select Distinct p.SpecialityId, p.Description As SpecialityName ");
        sb.Append("\nFrom Specialities p ");
        sb.Append("\nInner Join ClientSpecialities cp On p.SpecialityId = cp.SpecialityId ");
        sb.Append("\nInner Join Clients c ON cp.ClientId = c.ClientId ");
        sb.Append("\nWhere c.EditionId = " + editionId + " AND p.Description > 'j' ");
        sb.Append("\nOrder By p.Description ");

        DataSet ds = Catalogs.GuiaAliInstanceDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());

        return ds.Tables[0];
    }

    public DataTable getClientsBySpeciality(int editionId, int specialityId)
    {
        StringBuilder sb = new StringBuilder();

        sb.Append("Select Distinct c.ClientId, c.ShortName, c.City, c.Address ");
        sb.Append("\nFrom Clients c ");
        sb.Append("\nInner Join ClientSpecialities cp On c.ClientId = cp.ClientId ");
        sb.Append("\nWhere cp.SpecialityId = " + specialityId + " and c.EditionId = " + editionId);
        sb.Append("\nOrder By c.ShortName ");


        DataSet ds = Catalogs.GuiaAliInstanceDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());

        return ds.Tables[0];
    }

    public DataTable getPhonesByClient(int editionId, int clientId)
    {
        StringBuilder sb = new StringBuilder();

        sb.Append("Select Distinct ch.Number, pt.PhoneTypeID ");
        sb.Append("\nFrom ClientPhones ch ");
        sb.Append("\nInner Join Clients c On ch.ClientId = c.clientId ");
        sb.Append("\nInner Join PhoneTypes pt On ch.PhoneTypeId = pt.PhoneTypeID ");
        sb.Append("\nWhere ch.ClientId = " + clientId + " And c.EditionId = " + editionId);
        sb.Append("\nOrder By pt.PhoneTypeID ");

        DataSet ds = Catalogs.GuiaAliInstanceDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());

        return ds.Tables[0];
    }

    #endregion

    public static readonly Catalogs Instance = new Catalogs();

}
