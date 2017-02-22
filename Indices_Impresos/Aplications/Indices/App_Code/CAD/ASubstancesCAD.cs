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


public class ASubstancesCAD : CADDataAccessAdapter<object>
{
    #region Constructors

    public ASubstancesCAD(){ }

    #endregion

    #region Public Methods

    public DataTable getActiveSubs(string letra, int editionId)
    {
        StringBuilder sb = new StringBuilder();

        sb.Append("\nSELECT DISTINCT ASUB.[DESCRIPTION] AS ACTIVESUBSTANCE,ASUB.ACTIVESUBSTANCEID AS ID ");
        sb.Append("\nFROM PLM_VWPRODUCTSBYEDITION VW ");
        sb.Append("\nINNER JOIN PRODUCTSUBSTANCES PS ON (VW.PRODUCTID = PS.PRODUCTID) ");
        sb.Append("\nINNER JOIN ACTIVESUBSTANCES ASUB ON (PS.ACTIVESUBSTANCEID = ASUB.ACTIVESUBSTANCEID) ");
        sb.Append("\nWHERE VW.EDITIONID = " + editionId + " AND ASUB.ACTIVE = 1 AND ASUB.[DESCRIPTION] COLLATE SQL_Latin1_General_CP1_CI_AI LIKE ('" + letra + "%') ");
        //sb.Append(" UNION ");
        //sb.Append("SELECT DESCRIPTION AS ACTIVESUBSTANCE,ACTIVESUBSTANCEID ");
        //sb.Append("FROM ACTIVESUBSTANCES WHERE ENUNCIATIVE = 1 ORDER BY 1");
        sb.Append(" ORDER BY 1");

        DataSet ds = ASubstancesCAD.CAD40InstanceDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());

        return ds.Tables[0];

    }

    public DataTable getInfor(int activeSubsId, int editionId)
    {
        StringBuilder sb = new StringBuilder();

        sb.Append("\nSELECT DISTINCT VW.PRODUCTID,UPPER(VW.BRAND) AS BRAND,VW.[PRODUCTDESCRIPTION], ");
        sb.Append("\ndbo.plm_dfGetActiveSubstancesByProduct(VW.PRODUCTID,PS.ActiveSubstanceId) AS ACTIVESUBSTANCES, ");
        sb.Append("\ndbo.plm_dfGetPharmaFormsByProductC(VW.PRODUCTID,VW.EDITIONID,dbo.plm_dfGetCountriesByProduct (VW.PRODUCTID,VW.PHARMAFORMID)) AS PHARMAFORMS, ");
        sb.Append("\nUPPER(VW.DIVISIONSHORTNAME) AS LABORATORYNAME,dbo.plm_dfGetPagesByProduct(VW.PRODUCTID,VW.EDITIONID,VW.DIVISIONID) AS PAGE, ");
        sb.Append("\ndbo.plm_dfGetCountriesByProduct (VW.PRODUCTID,VW.PHARMAFORMID) as Countries ");
        sb.Append("\nFROM PLM_VWPRODUCTSBYEDITION VW ");
        sb.Append("\nINNER JOIN PRODUCTSUBSTANCES PS ON (VW.PRODUCTID = PS.PRODUCTID) ");
        sb.Append("\nINNER JOIN ACTIVESUBSTANCES ASUB ON (PS.ACTIVESUBSTANCEID = ASUB.ACTIVESUBSTANCEID) ");
        sb.Append("\nWHERE VW.EDITIONID = " + editionId +" AND VW.TYPEINEDITION = 'P' AND ASUB.[ACTIVESUBSTANCEID] = " + activeSubsId);
        sb.Append("\nORDER BY 2,4");

        DataSet ds = ASubstancesCAD.CAD40InstanceDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());

        return ds.Tables[0];
    }

    public DataTable getAlphabet(int editionId)
    {
        StringBuilder sb = new StringBuilder();

        sb.Append("\nSELECT DISTINCT SUBSTRING(ASUB.[DESCRIPTION],1,1) AS LETTER,1 ");
        sb.Append("\nFROM PLM_VWPRODUCTSBYEDITION VW ");
        sb.Append("\nINNER JOIN PRODUCTSUBSTANCES PS ON (VW.PRODUCTID = PS.PRODUCTID) ");
        sb.Append("\nINNER JOIN ACTIVESUBSTANCES ASUB ON (PS.ACTIVESUBSTANCEID = ASUB.ACTIVESUBSTANCEID) ");
        sb.Append("\nWHERE EDITIONID = " + editionId +" AND ASUB.ACTIVE = 1 AND VW.TYPEINEDITION = 'P' AND SUBSTRING(ASUB.[DESCRIPTION],1,1) NOT IN ('Á','É','Ó') ");
        sb.Append("\nORDER BY 1");

        DataSet ds = ASubstancesCAD.CAD40InstanceDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());

        return ds.Tables[0];
    }

    #endregion

    public static readonly ASubstancesCAD Instance = new ASubstancesCAD();
}
