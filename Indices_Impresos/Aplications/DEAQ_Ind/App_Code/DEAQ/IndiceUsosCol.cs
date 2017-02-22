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


public class IndiceUsosCol : DEAQDataAccessAdapter<object>
{
    #region Constructors

    private IndiceUsosCol(){}

    #endregion

    #region Public Methods

    public DataTable getAlphabet(int editionId)
    {
        StringBuilder sb = new StringBuilder(); 

        sb.Append("\nSELECT DISTINCT SUBSTRING(AGROCHEMICALUSENAME COLLATE SQL_Latin1_General_CP1_CI_AI ,1,1) ");
        sb.Append("\nFROM dbo.plm_vwProductsByEdition V ");
        sb.Append("\nINNER JOIN PRODUCTAGROCHEMICALUSES PU ON (V.PRODUCTID = PU.PRODUCTID) ");
        sb.Append("\nINNER JOIN AGROCHEMICALUSES U ON (PU.AGROCHEMICALUSEID = U.AGROCHEMICALUSEID) ");
        sb.Append("\nWHERE V.EDITIONID =  " + editionId + "  AND U.ACTIVE= 1 AND V.TYPEINEDITION = 'P' AND V.CATEGORYID not IN (2) ");
        sb.Append("\nORDER BY 1 ");

        DataSet ds = IndiceUsosCol.DEAQInstanceDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());

        return ds.Tables[0];

    }

    public DataTable getUsos(string letter, int editionId)
    {
        StringBuilder sb = new StringBuilder(); 

        sb.Append("\nSELECT DISTINCT U.AGROCHEMICALUSEID AS IdUsos, UPPER(U.AGROCHEMICALUSENAME) AS Usos ");
        sb.Append("\nFROM dbo.plm_vwProductsByEdition V ");
        sb.Append("\nINNER JOIN PRODUCTAGROCHEMICALUSES PU ON (V.PRODUCTID = PU.PRODUCTID)");
        sb.Append("\nINNER JOIN AGROCHEMICALUSES U ON (PU.AGROCHEMICALUSEID = U.AGROCHEMICALUSEID)");
        sb.Append("\nWHERE V.EDITIONID= " + editionId + "   AND U.ACTIVE=1  AND V.TYPEINEDITION = 'P' ");
        sb.Append("\nAND U.AGROCHEMICALUSENAME COLLATE SQL_Latin1_General_CP1_CI_AI LIKE ('" + letter + "%') AND V.CATEGORYID not IN (2) ");
        sb.Append("\nORDER BY 2 ");

        DataSet ds = IndiceUsosCol.DEAQInstanceDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());

        return ds.Tables[0];
    }

    public DataTable getSubUsos(int parentId, int editionId)
    {
        StringBuilder sb = new StringBuilder();

        sb.Append("SELECT DISTINCT U.AGROCHEMICALUSEID AS IdUsos, UPPER(U.AGROCHEMICALUSENAME) AS Usos FROM AGROCHEMICALUSES U ");
        sb.Append("\nINNER JOIN PRODUCTAGROCHEMICALUSES PU ON U.AGROCHEMICALUSEID = PU.AGROCHEMICALUSEID ");
        sb.Append("\nINNER JOIN PARTICIPANTPRODUCTS PP ON PU.PRODUCTID = PP.PRODUCTID ");
        sb.Append("\nWHERE PP.EDITIONID=" + editionId + "   AND U.PARENTID = " + parentId);
        sb.Append("\nAND pp.CATEGORYID not IN (2)  ORDER BY 2 ");

        DataSet ds = IndiceUsosCol.DEAQInstanceDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());

        return ds.Tables[0];
    }

    public DataTable getProducts(int idUso, int editionId)
    {
        StringBuilder sb = new StringBuilder(); 

        sb.Append("\n SELECT DISTINCT  UPPER(V.PRODUCTNAME) AS NOMBRE,");
        sb.Append("\n dbo.plm_dfGetActiveSubstancesByProduct(V.PRODUCTID, '') AS Sustancias,");
        sb.Append("\n dbo.plm_dfGetPharmaFormsByDiffProduct(V.PRODUCTID, V.EDITIONID) AS FFarmaceutica,");
        sb.Append("\n V.DIVISIONSHORTNAME AS LABORATORIO,");
        sb.Append("\n V.PAGE AS PAGINA ");
        sb.Append("\n FROM dbo.plm_vwProductsByEdition V ");
        sb.Append("\n INNER JOIN PRODUCTAGROCHEMICALUSES PSA ON (V.PRODUCTID = PSA.PRODUCTID) ");
        sb.Append("\n INNER JOIN AGROCHEMICALUSES AU ON (PSA.AGROCHEMICALUSEID = AU.AGROCHEMICALUSEID)");
        sb.Append("\n WHERE V.EDITIONID = " + editionId + "  AND V.TYPEINEDITION = 'P' AND AU.AGROCHEMICALUSEID = " + idUso.ToString() + " ");
        sb.Append("\n  AND V.CATEGORYID not IN (2)  ");
        sb.Append("\n ORDER BY 1");

        
        DataSet ds = IndiceUsos.DEAQInstanceDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());

        return ds.Tables[0];
    }

    //////////////////////////////////////////////////////////////// SEMILLAS ////////////////////////////////////////////////////////////////

    public DataTable getAlphabetSeeds(int editionId)
    {
        StringBuilder sb = new StringBuilder();

        sb.Append("\nSELECT DISTINCT SUBSTRING(AGROCHEMICALUSENAME COLLATE SQL_Latin1_General_CP1_CI_AI ,1,1) ");
        sb.Append("\nFROM dbo.plm_vwProductsByEdition V ");
        sb.Append("\nINNER JOIN PRODUCTAGROCHEMICALUSES PU ON (V.PRODUCTID = PU.PRODUCTID) ");
        sb.Append("\nINNER JOIN AGROCHEMICALUSES U ON (PU.AGROCHEMICALUSEID = U.AGROCHEMICALUSEID) ");
        sb.Append("\nWHERE V.EDITIONID =  " + editionId + "  AND U.ACTIVE= 1 AND V.TYPEINEDITION = 'P' AND V.CATEGORYID IN (2) ");
        sb.Append("\nORDER BY 1 ");

        DataSet ds = IndiceUsosCol.DEAQInstanceDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());

        return ds.Tables[0];

    }

    public DataTable getUsosSeeds(string letter, int editionId)
    {
        StringBuilder sb = new StringBuilder();

        sb.Append("\nSELECT DISTINCT U.AGROCHEMICALUSEID AS IdUsos, UPPER(U.AGROCHEMICALUSENAME) AS Usos ");
        sb.Append("\nFROM dbo.plm_vwProductsByEdition V ");
        sb.Append("\nINNER JOIN PRODUCTAGROCHEMICALUSES PU ON (V.PRODUCTID = PU.PRODUCTID)");
        sb.Append("\nINNER JOIN AGROCHEMICALUSES U ON (PU.AGROCHEMICALUSEID = U.AGROCHEMICALUSEID)");
        sb.Append("\nWHERE V.EDITIONID= " + editionId + "   AND U.ACTIVE=1  AND V.TYPEINEDITION = 'P' ");
        sb.Append("\nAND U.AGROCHEMICALUSENAME COLLATE SQL_Latin1_General_CP1_CI_AI LIKE ('" + letter + "%') AND V.CATEGORYID IN (2) ");
        sb.Append("\nORDER BY 2 ");

        DataSet ds = IndiceUsosCol.DEAQInstanceDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());

        return ds.Tables[0];
    }

    public DataTable getSubUsosSeeds(int parentId, int editionId)
    {
        StringBuilder sb = new StringBuilder();

        sb.Append("SELECT DISTINCT U.AGROCHEMICALUSEID AS IdUsos, UPPER(U.AGROCHEMICALUSENAME) AS Usos FROM AGROCHEMICALUSES U ");
        sb.Append("\nINNER JOIN PRODUCTAGROCHEMICALUSES PU ON U.AGROCHEMICALUSEID = PU.AGROCHEMICALUSEID ");
        sb.Append("\nINNER JOIN PARTICIPANTPRODUCTS PP ON PU.PRODUCTID = PP.PRODUCTID ");
        sb.Append("\nWHERE PP.EDITIONID=" + editionId + "   AND U.PARENTID = " + parentId);
        sb.Append("\nAND pp.CATEGORYID IN (2)  ORDER BY 2 ");

        DataSet ds = IndiceUsosCol.DEAQInstanceDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());

        return ds.Tables[0];
    }

    public DataTable getProductsSeeds(int idUso, int editionId)
    {
        StringBuilder sb = new StringBuilder();

        sb.Append("\n SELECT DISTINCT  UPPER(V.PRODUCTNAME) AS NOMBRE,");
        sb.Append("\n dbo.plm_dfGetActiveSubstancesByProduct(V.PRODUCTID, '') AS Sustancias,");
        sb.Append("\n dbo.plm_dfGetPharmaFormsByDiffProduct(V.PRODUCTID, V.EDITIONID) AS FFarmaceutica,");
        sb.Append("\n V.DIVISIONSHORTNAME AS LABORATORIO,");
        sb.Append("\n V.PAGE AS PAGINA ");
        sb.Append("\n FROM dbo.plm_vwProductsByEdition V ");
        sb.Append("\n INNER JOIN PRODUCTAGROCHEMICALUSES PSA ON (V.PRODUCTID = PSA.PRODUCTID) ");
        sb.Append("\n INNER JOIN AGROCHEMICALUSES AU ON (PSA.AGROCHEMICALUSEID = AU.AGROCHEMICALUSEID)");
        sb.Append("\n WHERE V.EDITIONID = " + editionId + "  AND V.TYPEINEDITION = 'P' AND AU.AGROCHEMICALUSEID = " + idUso.ToString() + " ");
        sb.Append("\n  AND V.CATEGORYID IN (2)  ");
        sb.Append("\n ORDER BY 1");


        DataSet ds = IndiceUsos.DEAQInstanceDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());

        return ds.Tables[0];
    }

    #endregion

    public static readonly IndiceUsosCol Instance = new IndiceUsosCol();

}
