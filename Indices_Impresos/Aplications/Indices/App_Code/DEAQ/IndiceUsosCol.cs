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

        sb.Append("\nSELECT DISTINCT Substring(AGROCHEMICALUSENAME,1,1)  FROM AGROCHEMICALUSES U ");
        sb.Append("\nINNER JOIN PRODUCTAGROCHEMICALUSES PU ON U.AGROCHEMICALUSEID = PU.AGROCHEMICALUSEID ");
        sb.Append("\nINNER JOIN PARTICIPANTPRODUCTS PP ON PU.PRODUCTID = PP.PRODUCTID ");
        sb.Append("\nWHERE PP.EDITIONID= " + editionId);
        sb.Append("\nORDER BY 1 ");

        DataSet ds = IndiceUsosCol.DEAQInstanceDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());

        return ds.Tables[0];

    }

    public DataTable getUsos(string letter, int editionId)
    {
        StringBuilder sb = new StringBuilder();

        sb.Append("SELECT DISTINCT U.AGROCHEMICALUSEID AS IdUsos, UPPER(U.AGROCHEMICALUSENAME) AS Usos FROM AGROCHEMICALUSES U ");
        sb.Append("\nINNER JOIN PRODUCTAGROCHEMICALUSES PU ON U.AGROCHEMICALUSEID = PU.AGROCHEMICALUSEID ");
        sb.Append("\nINNER JOIN PARTICIPANTPRODUCTS PP ON PU.PRODUCTID = PP.PRODUCTID ");
        sb.Append("\nWHERE PP.EDITIONID=" + editionId + "AND U.AGROCHEMICALUSENAME LIKE ('" + letter + "%') AND U.PARENTID IS NULL ");
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
        sb.Append("\nWHERE PP.EDITIONID=" + editionId + "AND U.PARENTID = " + parentId);
        sb.Append("\nORDER BY 2 ");

        DataSet ds = IndiceUsosCol.DEAQInstanceDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());

        return ds.Tables[0];
    }

    public DataTable getProducts(int idUso, int editionId)
    {
        StringBuilder sb = new StringBuilder();
        
        sb.Append("\nSELECT	DISTINCT P.PRODUCTNAME AS Nombre, ");
        sb.Append("\ndbo.plm_dfGetActiveSubstancesByProduct(P.PRODUCTID, '') AS Sustancias, ");
        sb.Append("\ndbo.plm_dfGetPharmaFormsByDiffPagedProduct(P.PRODUCTID, PP.EDITIONID, PP.PAGE) AS FFarmaceutica, ");
        sb.Append("\nD.SHORTNAME AS Laboratorio, ");
        sb.Append("\nPP.PAGE AS Pagina ");
        sb.Append("\nFROM PRODUCTS P ");
        sb.Append("\nINNER JOIN PRODUCTPHARMAFORMS PPF ON P.PRODUCTID = PPF.PRODUCTID ");
        sb.Append("\nINNER JOIN PARTICIPANTPRODUCTS PP ON P.PRODUCTID = PP.PRODUCTID AND PPF.PHARMAFORMID = PP.PHARMAFORMID ");
        sb.Append("\nINNER JOIN PRODUCTAGROCHEMICALUSES PU ON PP.PRODUCTID = PU.PRODUCTID ");
        sb.Append("\nINNER JOIN DIVISIONS D ON PP.DIVISIONID = D.DIVISIONID ");
        sb.Append("\nWHERE EDITIONID=" + editionId + " AND AGROCHEMICALUSEID = " + idUso.ToString() + " ");
        sb.Append("\nORDER BY 1 ");
        
        DataSet ds = IndiceUsos.DEAQInstanceDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());

        return ds.Tables[0];
    }

    #endregion

    public static readonly IndiceUsosCol Instance = new IndiceUsosCol();

}
