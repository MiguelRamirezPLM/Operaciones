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
public class TherIndiceVMG : VMGDataAccessAdapter<object>
{
    #region Constructors

    private TherIndiceVMG()
    { }

    #endregion

    public DataTable getAlphabet(int editionId)
    {
        StringBuilder sb = new StringBuilder();

        sb.Append("SELECT T.[SPANISHDESCRIPTION],T.THERAPEUTICKEY,T.THERAPEUTICID ");
        sb.Append("FROM TMPTHERA P INNER JOIN THERAPEUTICS T ON(P.THERAPEUTICID = T.THERAPEUTICID) ");
        sb.Append("WHERE P.PARENTID IS NULL");// AND T.THERAPEUTICKEY > 'C' AND T.THERAPEUTICKEY < 'K' ");//AND EDITIONID = " + editionId.ToString());
        sb.Append(" ORDER BY 2");

        DataSet ds = TherIndiceVMG.VMGInstanceDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());

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

        DataSet ds = TherIndiceVMG.VMGInstanceDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());

        return ds.Tables[0];

    }

    public DataTable getInformation(int therapeuticId, int editionId)
    {
        StringBuilder sb = new StringBuilder();

        sb.Append("\nSELECT DISTINCT UPPER(V.BRAND) AS BRAND, ");
        sb.Append("dbo.plm_dfGetPharmaFormsByProductNameByThera(V.EDITIONID,V.BRAND, THE.THERAPEUTICID) AS PHARMAFORMS, V.PAGE AS PAGE ");
        sb.Append("\nFROM PLM_VWPRODUCTSBYEDITION V ");
        sb.Append("\nINNER JOIN PRODUCTTHERAPEUTICS PT ON (V.PRODUCTID = PT.PRODUCTID AND V.PHARMAFORMID = PT.PHARMAFORMID) ");
        sb.Append("\nINNER JOIN THERAPEUTICS TER ON (PT.THERAPEUTICID = TER.THERAPEUTICID) ");
        sb.Append("\nINNER JOIN TMPTHERA THE ON(TER.THERAPEUTICID = THE.THERAPEUTICID) ");
        sb.Append("\nWHERE V.EDITIONID = " + editionId + " AND V.TYPEINEDITION = 'P' AND TER.THERAPEUTICID = " + therapeuticId);
        sb.Append("\nORDER BY 1 ");

        DataSet ds = TherIndiceVMG.VMGInstanceDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());

        return ds.Tables[0];

    }



    public static readonly TherIndiceVMG Instance = new TherIndiceVMG();
}
