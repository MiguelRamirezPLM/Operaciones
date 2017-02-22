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

    public DataTable getIndications(string letra, int editionId)
    {
        StringBuilder sb = new StringBuilder();

        sb.Append("\nSELECT DISTINCT UPPER(IND.[DESCRIPTION]) AS INDICATIONS,IND.INDICATIONID ");
        sb.Append("\nFROM PLM_VWPRODUCTSBYEDITION V ");
        sb.Append("INNER JOIN PRODUCTINDICATIONS PIN ON (V.PRODUCTID = PIN.PRODUCTID) ");
        sb.Append("INNER JOIN INDICATIONS IND ON(PIN.INDICATIONID = IND.INDICATIONID) ");
        sb.Append("\nWHERE V.TYPEINEDITION = 'P' AND V.EDITIONID = " + editionId + " AND IND.ACTIVE = 1 AND ");
        sb.Append("IND.[DESCRIPTION] COLLATE SQL_Latin1_General_CP1_CI_AI LIKE ('" + letra + "%') ");
        sb.Append("ORDER BY 1 ");

        DataSet ds = IndIndice.DEFInstanceDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());

        return ds.Tables[0];
    }

    public DataTable getInformation(int indicationId, int editionId)
    {
        StringBuilder sb = new StringBuilder();

        sb.Append("\n SELECT DISTINCT v.PRODUCTID,UPPER(V.BRAND) AS BRAND,dbo.plm_dfGetPharmaFormsByPagedProduct(V.PRODUCTID, V.EDITIONID, V.DIVISIONID) AS PHARMAFORMS,dbo.plm_dfGetPagesByProduct(V.PRODUCTID,V.EDITIONID, V.DIVISIONID) AS PAGE  ");
        sb.Append("\nFROM PLM_VWPRODUCTSBYEDITION V ");
        sb.Append("\nINNER JOIN PRODUCTINDICATIONS PIN ON(V.PRODUCTID = PIN.PRODUCTID) ");
        sb.Append("\nINNER JOIN INDICATIONS IND ON(PIN.INDICATIONID = IND.INDICATIONID) ");
        sb.Append("\nWHERE V.TYPEINEDITION = 'P' AND V.EDITIONID = " + editionId + " AND IND.INDICATIONID = " + indicationId);
        sb.Append("\nORDER BY 2 ");

        DataSet ds = IndIndice.DEFInstanceDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());

        return ds.Tables[0];
    }

    public DataTable getAlphabet(int editionId)
    {
        StringBuilder sb = new StringBuilder();

        sb.Append("\nSELECT DISTINCT SUBSTRING(IND.[DESCRIPTION],1,1) AS LETTER,1 ");
        sb.Append("\nFROM PLM_VWPRODUCTSBYEDITION V ");
        sb.Append("\nINNER JOIN PRODUCTINDICATIONS PIN ON (V.PRODUCTID = PIN.PRODUCTID) ");
        sb.Append("\nINNER JOIN INDICATIONS IND ON(PIN.INDICATIONID = IND.INDICATIONID) ");
        sb.Append("\nWHERE V.EDITIONID = " + editionId + " AND IND.ACTIVE = 1 ");
        sb.Append("\nAND SUBSTRING(IND.[DESCRIPTION],1,1) NOT IN ('Á', 'É', 'Í', 'Ó', 'Ú') "); //AND SUBSTRING(IND.[DESCRIPTION],1,1) > 'L' "); // AND SUBSTRING(IND.[DESCRIPTION],1,1) < 'M'");
        sb.Append("\nORDER BY 1");

        DataSet ds = ASIndice.DEFInstanceDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());

        return ds.Tables[0];
    }


    public static readonly IndIndice Instance = new IndIndice();
}
