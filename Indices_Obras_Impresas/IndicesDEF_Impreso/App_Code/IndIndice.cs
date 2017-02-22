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
/// Descripción breve de IndIndice
/// </summary>
public class IndIndice : PLMDataAccessAdapter<object>
{
    #region Constructors

    private IndIndice()
	{ }

    #endregion

    public DataTable getIndications(string letra, int editionId, string tipoProducto)
    {
        StringBuilder sb = new StringBuilder();

        sb.Append("\nSELECT DISTINCT UPPER(IND.[DESCRIPTION]) AS INDICATIONS,IND.INDICATIONID ");
        sb.Append("\nFROM PLM_VWPRODUCTSBYEDITION V ");
        sb.Append("INNER JOIN PRODUCTINDICATIONS PIN ON (V.PRODUCTID = PIN.PRODUCTID) ");
        sb.Append("INNER JOIN INDICATIONS IND ON(PIN.INDICATIONID = IND.INDICATIONID) ");
        sb.Append("\nWHERE V.TYPEINEDITION = 'P' AND V.EDITIONID = " + editionId + " AND IND.ACTIVE = 1 AND ");
        sb.Append("\nIND.[DESCRIPTION] COLLATE SQL_Latin1_General_CP1_CI_AI LIKE ('" + letra + "%') ");
        //sb.Append("\nAND IND.INDICATIONID NOT IN ('') ");
        sb.Append("\nAND IND.IndicationId NOT IN ('','25','66','73','80','83','92','119','121','122','123','130','132','136','145','155','156','157','159','160','162','163','165','166','173','174','177', ");
        sb.Append("\n'179','184','185','187','194','205','217','218','220','221','233','236','246','253','258','260','261','262','266','267','268','269','272','273','289', ");
        sb.Append("\n'297','329','334','336','387','401','411','412','420','443','445','456','473','485','493','499','527','529','532','533','548','558','562','586','587', ");
        sb.Append("\n'589','600','617','622','661','676','687','688','705','709','712','726','738','749','750','755','757','758','771','779','800','805','816','817','821', ");
        sb.Append("\n'824','826','827','828','838','864','869','870','874','876','877','878','900','912','924','935','949','956','970','971','976','977','997','1008','1011', ");
        sb.Append("\n'1013','1014','1016','1018','1019','1035','1036','1042','1046','1047','1071','1080','1084','1088','1093','1126','1144','1148','1155','1205','1216','1228', ");
        sb.Append("\n'1229','1231','1277','1356','1373','1414','1495','1573','1618','1672','1683','1685','1686','1689','1690','1698','1705','1708','1712','1713','1718','1719', ");
        sb.Append("\n'1722','1732','1733','1736','1777','1799','1832','1833','1834','1835','1893','1943','1944','1949','1962','2240','2252','2259','2425','2619','2623','2625', ");
        sb.Append("\n'2626','2627','2628','2635','2771','3161','3292','3307','3311','3603','3738','3755','3758','3859','3867','3887','3898','3903','4216','4341','4585','4688', ");
        sb.Append("\n'4750','4767','4790','4895','4958','5007','5135','5401','5985','6018','6038','6194','6389','6411','6500','6568','7845','9455','1741') ");

        if (tipoProducto != "")
            sb.Append("\nAND V.ProductType COLLATE SQL_Latin1_General_CP1_CI_AI LIKE '" + tipoProducto + "'");

        sb.Append("ORDER BY 1 ");

        DataSet ds = IndIndice.DEFInstanceDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());

        return ds.Tables[0];
    }

    public DataTable getInformation(int indicationId, int editionId, string tipoProducto)
    {
        StringBuilder sb = new StringBuilder();

        sb.Append("\n SELECT DISTINCT v.PRODUCTID,UPPER(V.BRAND) AS BRAND,dbo.plm_dfGetPharmaFormsByPagedProduct(V.PRODUCTID, V.EDITIONID, V.DIVISIONID) AS PHARMAFORMS,dbo.plm_dfGetPagesByProduct(V.PRODUCTID,V.EDITIONID, V.DIVISIONID) AS PAGE  ");
        sb.Append("\nFROM PLM_VWPRODUCTSBYEDITION V ");
        sb.Append("\nINNER JOIN PRODUCTINDICATIONS PIN ON(V.PRODUCTID = PIN.PRODUCTID) ");
        sb.Append("\nINNER JOIN INDICATIONS IND ON(PIN.INDICATIONID = IND.INDICATIONID) ");
        sb.Append("\nWHERE V.TYPEINEDITION = 'P' AND V.EDITIONID = " + editionId + " AND IND.INDICATIONID = " + indicationId);

        if (tipoProducto != "")
            sb.Append("\nAND V.ProductType COLLATE SQL_Latin1_General_CP1_CI_AI LIKE '" + tipoProducto + "'");

        sb.Append("\nORDER BY 2 ");

        DataSet ds = IndIndice.DEFInstanceDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());

        return ds.Tables[0];
    }

    public DataTable getAlphabet(int editionId, string tipoProducto)
    {
        StringBuilder sb = new StringBuilder();

        //sb.Append("\nSELECT DISTINCT SUBSTRING(IND.[DESCRIPTION],1,1) AS LETTER,1 ");
        //sb.Append("\nFROM PLM_VWPRODUCTSBYEDITION V ");
        //sb.Append("\nINNER JOIN PRODUCTINDICATIONS PIN ON (V.PRODUCTID = PIN.PRODUCTID) ");
        //sb.Append("\nINNER JOIN INDICATIONS IND ON(PIN.INDICATIONID = IND.INDICATIONID) ");
        //sb.Append("\nWHERE V.EDITIONID = " + editionId + " AND IND.ACTIVE = 1 ");
        ////sb.Append("\nAND SUBSTRING(IND.[DESCRIPTION],1,1) > 'm' ");// AND SUBSTRING(IND.[DESCRIPTION],1,1) <= 'm' ");
        //sb.Append("\nAND SUBSTRING(IND.[DESCRIPTION],1,1) NOT IN ('Á', 'É', 'Í', 'Ó', 'Ú') ");

        //if (tipoProducto != "")
        //    sb.Append("\nAND V.ProductType COLLATE SQL_Latin1_General_CP1_CI_AI LIKE '" + tipoProducto + "'");

        //sb.Append("\nORDER BY 1");

        sb.Append("\nSELECT DISTINCT SUBSTRING(IND.[DESCRIPTION],1,1) AS LETTER,1 ");
        sb.Append("\nFROM PLM_VWPRODUCTSBYEDITION V ");
        sb.Append("\nINNER JOIN PRODUCTINDICATIONS PIN ON (V.PRODUCTID = PIN.PRODUCTID) ");
        sb.Append("\nINNER JOIN INDICATIONS IND ON(PIN.INDICATIONID = IND.INDICATIONID) ");
        sb.Append("\nWHERE V.EDITIONID = " + editionId + " AND IND.ACTIVE = 1 ");
        sb.Append("\nAND SUBSTRING(IND.[DESCRIPTION],1,1) >= 'A' "); 
        sb.Append("\nAND SUBSTRING(IND.[DESCRIPTION],1,1) NOT IN ('Á', 'É', 'Í', 'Ó', 'Ú') ");
        sb.Append("\nAND IND.IndicationId NOT IN ('25','66','73','80','83','92','119','121','122','123','130','132','136','145','155','156','157','159','160','162','163','165','166','173','174','177', ");
        sb.Append("\n'179','184','185','187','194','205','217','218','220','221','233','236','246','253','258','260','261','262','266','267','268','269','272','273','289', ");
        sb.Append("\n'297','329','334','336','387','401','411','412','420','443','445','456','473','485','493','499','527','529','532','533','548','558','562','586','587', ");
        sb.Append("\n'589','600','617','622','661','676','687','688','705','709','712','726','738','749','750','755','757','758','771','779','800','805','816','817','821', ");
        sb.Append("\n'824','826','827','828','838','864','869','870','874','876','877','878','900','912','924','935','949','956','970','971','976','977','997','1008','1011', ");
        sb.Append("\n'1013','1014','1016','1018','1019','1035','1036','1042','1046','1047','1071','1080','1084','1088','1093','1126','1144','1148','1155','1205','1216','1228', ");
        sb.Append("\n'1229','1231','1277','1356','1373','1414','1495','1573','1618','1672','1683','1685','1686','1689','1690','1698','1705','1708','1712','1713','1718','1719', ");
        sb.Append("\n'1722','1732','1733','1736','1777','1799','1832','1833','1834','1835','1893','1943','1944','1949','1962','2240','2252','2259','2425','2619','2623','2625', ");
        sb.Append("\n'2626','2627','2628','2635','2771','3161','3292','3307','3311','3603','3738','3755','3758','3859','3867','3887','3898','3903','4216','4341','4585','4688', ");
        sb.Append("\n'4750','4767','4790','4895','4958','5007','5135','5401','5985','6018','6038','6194','6389','6411','6500','6568','7845','9455','1741') ");
        
        if (tipoProducto != "")
            sb.Append("\nAND V.ProductType COLLATE SQL_Latin1_General_CP1_CI_AI LIKE '" + tipoProducto + "'");

        sb.Append("\nORDER BY 1 ");

        

        DataSet ds = ASIndice.DEFInstanceDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());

        return ds.Tables[0];
    }


    public static readonly IndIndice Instance = new IndIndice();
}
