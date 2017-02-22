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
/// Descripción breve de ASIndice
/// </summary>
public class ASIndice : PLMDataAccessAdapter<object>
{

    #region Constructors
    
    private ASIndice()
	{ }

    #endregion

    public DataTable getActiveSubs(string letra, int editionId)
    {
        StringBuilder sb = new StringBuilder();

        sb.Append("\nSELECT DISTINCT ASUB.[DESCRIPTION] AS ACTIVESUBSTANCE,ASUB.ACTIVESUBSTANCEID AS ID ");
        sb.Append("\nFROM PLM_VWPRODUCTSBYEDITION V ");
        sb.Append("\nINNER JOIN PRODUCTSUBSTANCES PS ON (V.PRODUCTID = PS.PRODUCTID) ");
        sb.Append("\nINNER JOIN ACTIVESUBSTANCES ASUB ON (PS.ACTIVESUBSTANCEID = ASUB.ACTIVESUBSTANCEID) ");
        sb.Append("\nWHERE EDITIONID = " + editionId + " AND ASUB.ACTIVE = 1 AND TYPEINEDITION = 'P' AND ");
        sb.Append("\nASUB.[DESCRIPTION] COLLATE SQL_Latin1_General_CP1_CI_AI LIKE ('" + letra + "%') AND V.PAGE IS NOT NULL");
        sb.Append("\nORDER BY 1");

        DataSet ds = ASIndice.DEFInstanceDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());

        return ds.Tables[0];

    }

    public DataTable getInfor(int activeSubsId, int editionId)
    {
        StringBuilder sb = new StringBuilder();

        sb.Append("\nSELECT DISTINCT V.PRODUCTID,UPPER(V.BRAND) AS BRAND, SUBSTRING(V.[PRODUCTDESCRIPTION],1,1) + LOWER(SUBSTRING(v.[PRODUCTDESCRIPTION],2,LEN(V.[PRODUCTDESCRIPTION])))AS [PRODUCTDESCRIPTION],");
        sb.Append("\ndbo.plm_dfGetActiveSubstancesByProduct(V.PRODUCTID,PS.ACTIVESUBSTANCEID) AS ACTIVESUBSTANCES, ");
        sb.Append("\ndbo.plm_dfGetPharmaFormsByPagedProduct(V.PRODUCTID, V.EDITIONID, V.DIVISIONID) as PHARMAFORMS, ");
        sb.Append("\nCASE WHEN V.DivisionId IN (83,100,2335,2336) ");
        sb.Append("\nTHEN V.LaboratoryName ELSE V.DivisionShortName END AS LABORATORYNAME, dbo.plm_dfGetPagesByProduct(V.PRODUCTID,V.EDITIONID, V.DIVISIONID) AS PAGE ");
        sb.Append("\nFROM PLM_VWPRODUCTSBYEDITION V ");
        sb.Append("\nINNER JOIN PRODUCTSUBSTANCES PS ON (v.PRODUCTID = PS.PRODUCTID) ");
        sb.Append("\nINNER JOIN ACTIVESUBSTANCES ASUB ON (PS.ACTIVESUBSTANCEID = ASUB.ACTIVESUBSTANCEID)  ");
        sb.Append("\nWHERE V.EDITIONID = " + editionId + " AND TYPEINEDITION = 'P' AND ASUB.[ACTIVESUBSTANCEID] =" + activeSubsId);
        sb.Append("\n AND V.PAGE IS NOT NULL ORDER BY 2,4");

        DataSet ds = ASIndice.DEFInstanceDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());

        return ds.Tables[0];
    }

    public DataTable getAlphabet(int editionId)
    {
        StringBuilder sb = new StringBuilder();

        sb.Append("\nSELECT DISTINCT SUBSTRING(ASUB.[DESCRIPTION],1,1) AS LETTER,1 ");
        sb.Append("\nFROM PLM_VWPRODUCTSBYEDITION V ");
        sb.Append("\nINNER JOIN PRODUCTSUBSTANCES PS ON (V.PRODUCTID = PS.PRODUCTID) ");
        sb.Append("\nINNER JOIN ACTIVESUBSTANCES ASUB ON (PS.ACTIVESUBSTANCEID = ASUB.ACTIVESUBSTANCEID) ");
        sb.Append("\nWHERE V.EDITIONID = " + editionId + " AND ASUB.ACTIVE = 1 AND TYPEINEDITION = 'P'  ");
        sb.Append("\nAND SUBSTRING(ASUB.[DESCRIPTION],1,1) <= 'z' ");
        //sb.Append("\nAND SUBSTRING(ASUB.[DESCRIPTION],1,1)  > 'm' and SUBSTRING(ASUB.[DESCRIPTION],1,1) <= 'z' ");
        sb.Append("\nAND SUBSTRING(ASUB.[DESCRIPTION],1,1) NOT IN ('Á', 'É', 'Í', 'Ó', 'Ú') AND V.PAGE IS NOT NULL");
        sb.Append("\nORDER BY 1 ");

        DataSet ds = ASIndice.DEFInstanceDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());

        return ds.Tables[0];
    }


    public static readonly ASIndice Instance = new ASIndice();
}
