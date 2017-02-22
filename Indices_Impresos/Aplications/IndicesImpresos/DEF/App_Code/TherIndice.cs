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
/// Descripción breve de TherIndice
/// </summary>
public class TherIndice : PLMDataAccessAdapter<object>
{
    #region Constructors

    private TherIndice()
	{ }

    #endregion

    public DataTable getTherapeutics(string letra, int editionId)
    {
        StringBuilder sb = new StringBuilder();

        sb.Append("\nSELECT DISTINCT TER.[SPANISHDESCRIPTION] AS THERAPEUTICS,TER.THERAPEUTICKEY,TER.THERAPEUTICID ");
        sb.Append("\nFROM PLM_VWPRODUCTSBYEDITION V ");
        sb.Append("\nINNER JOIN PRODUCTTHERAPEUTICS PT ON(V.PRODUCTID = PT.PRODUCTID) ");
        sb.Append("\nINNER JOIN THERAPEUTICS TER ON(PT.THERAPEUTICID = TER.THERAPEUTICID) ");
        sb.Append("\nWHERE TER.ACTIVE = 1 AND V.EDITIONID = " + editionId + " AND TER.[SPANISHDESCRIPTION] LIKE ('" + letra + "%') ");
        sb.Append("\nORDER BY 2 ");

        DataSet ds = TherIndice.DEFInstanceDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());

        return ds.Tables[0];
    }

    public DataTable getTherapeuticsByParentId(int parentId, int editionId)
    {
        StringBuilder sb = new StringBuilder();

        sb.Append("\nSELECT T.[SPANISHDESCRIPTION],T.THERAPEUTICKEY,T.THERAPEUTICID ");
        sb.Append("\nFROM TMPTHERA TER ");
        sb.Append("\nINNER JOIN THERAPEUTICS T ON (TER.THERAPEUTICID = T.THERAPEUTICID) ");
        sb.Append("\nWHERE T.ACTIVE = 1 AND  T.PARENTID = " + parentId);
        sb.Append("\nORDER BY LEN(T.THERAPEUTICKEY),SUBSTRING(T.THERAPEUTICKEY,1,1),SUBSTRING(T.THERAPEUTICKEY,2,1),SUBSTRING(T.THERAPEUTICKEY,3,1),SUBSTRING(T.THERAPEUTICKEY,4,1),SUBSTRING(T.THERAPEUTICKEY,5,1) ");

        DataSet ds = TherIndice.DEFInstanceDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());

        return ds.Tables[0];

    }

    public DataTable getInformation(int therapeuticId, int editionId)
    {
        StringBuilder sb = new StringBuilder();

        //sb.Append("\nSELECT DISTINCT V.PRODUCTID,UPPER(V.BRAND) AS BRAND, dbo.plm_dfGetActiveSubsByProduct(V.PRODUCTID) AS ACTIVESUBSTANCES, ");
        //sb.Append("dbo.plm_dfGetPharmaFormsByDiffPagedProduct(V.PRODUCTID,V.EDITIONID,V.PAGE) AS PHARMAFORMS,UPPER(V.DIVISIONSHORTNAME) AS LABORATORYNAME,V.PAGE AS PAGE ");
        //sb.Append("\nFROM PLM_VWPRODUCTSBYEDITION V ");
        //sb.Append("\nINNER JOIN PRODUCTTHERAPEUTICS PT ON (V.PRODUCTID = PT.PRODUCTID AND V.PHARMAFORMID = PT.PHARMAFORMID) ");
        //sb.Append("\nINNER JOIN THERAPEUTICS TER ON (PT.THERAPEUTICID = TER.THERAPEUTICID) ");
        //sb.Append("\nINNER JOIN TMPTHERA THE ON(TER.THERAPEUTICID = THE.THERAPEUTICID) ");
        //sb.Append("\nWHERE V.EDITIONID = " + editionId + " AND V.TYPEINEDITION = 'P' AND TER.THERAPEUTICID = " + therapeuticId);
        //sb.Append("\nORDER BY 2 ");

        //PEDIATRIA 2011
        sb.Append("\nSELECT DISTINCT V.PRODUCTID,UPPER(V.BRAND) AS BRAND, dbo.plm_dfGetActiveSubsByProduct(V.PRODUCTID) AS ACTIVESUBSTANCES, ");
        sb.Append("\ndbo.plm_dfGetPharmaFormsByPagedProduct(V.PRODUCTID,V.EDITIONID,V.DIVISIONID) AS PHARMAFORMS, ");
        sb.Append("\nCASE WHEN V.DivisionId IN (83,100,2335,2336) ");
        sb.Append("\nTHEN V.LaboratoryName ELSE UPPER(V.DivisionShortName) END AS LABORATORYNAME, ");
        sb.Append("\ndbo.plm_dfGetPagesByProduct(V.PRODUCTID, V.EDITIONID, V.DIVISIONID) AS PAGE ");
        sb.Append("\nFROM PLM_VWPRODUCTSBYEDITION V ");
        sb.Append("\nINNER JOIN PRODUCTTHERAPEUTICS PT ON (V.PRODUCTID = PT.PRODUCTID AND V.PHARMAFORMID = PT.PHARMAFORMID) ");
        sb.Append("\nINNER JOIN THERAPEUTICS TER ON (PT.THERAPEUTICID = TER.THERAPEUTICID) ");
        sb.Append("\nINNER JOIN TMPTHERA THE ON(TER.THERAPEUTICID = THE.THERAPEUTICID) ");
        sb.Append("\nWHERE V.EDITIONID = " + editionId + " AND V.TYPEINEDITION = 'P' AND TER.THERAPEUTICID = " + therapeuticId);
        sb.Append("\nORDER BY 2 ");

        DataSet ds = TherIndice.DEFInstanceDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());

        return ds.Tables[0];

    }

    public DataTable getAlphabet(int editionId)
    {
        StringBuilder sb = new StringBuilder();

        sb.Append("SELECT T.[SPANISHDESCRIPTION],T.THERAPEUTICKEY,T.THERAPEUTICID ");
        sb.Append("FROM TMPTHERA P INNER JOIN THERAPEUTICS T ON(P.THERAPEUTICID = T.THERAPEUTICID) ");
        sb.Append("WHERE P.PARENTID IS NULL");// AND T.THERAPEUTICKEY < 'J' ");// AND T.THERAPEUTICKEY > 'C' AND T.THERAPEUTICKEY < 'K' ");
        sb.Append(" ORDER BY 2");

        DataSet ds = TherIndice.DEFInstanceDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());

        return ds.Tables[0];
    }



    public static readonly TherIndice Instance = new TherIndice();
}
