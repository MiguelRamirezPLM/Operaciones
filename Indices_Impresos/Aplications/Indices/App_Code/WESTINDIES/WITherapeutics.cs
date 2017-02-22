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


public class WITherapeutics : WIDataAccessAdapter<object>
{
    #region Constructors

    public WITherapeutics()
    {}

    #endregion

    #region Public Methods

    public DataTable getAlphabet()
    {
        StringBuilder sb = new StringBuilder();

        sb.Append("SELECT [DESCRIPTION],THERAPEUTICKEY,THERAPEUTICID ");
        sb.Append("FROM TMPTHERA ");
        sb.Append("WHERE PARENTID IS NULL ");
        sb.Append("ORDER BY 2");

        DataSet ds = WITherapeutics.DEF55InstanceDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());

        return ds.Tables[0];
    }

    public DataTable getTherapeutics(int editionId, string letra)
    {
        StringBuilder sb = new StringBuilder();

        sb.Append("SELECT DISTINCT TER.[DESCRIPTION] AS THERAPEUTICS,TER.THERAPEUTICKEY,TER.THERAPEUTICID ");
        sb.Append("FROM plm_veProductsByEdition PP ");
        sb.Append("INNER JOIN PRODUCTTHERAPEUTICS PT ON(PP.PRODUCTID = PT.PRODUCTID and PP.PharmaFormId = PT.PharmaFormId) ");
        sb.Append("INNER JOIN THERAPEUTICS TER ON(PT.THERAPEUTICID = TER.THERAPEUTICID) ");
        sb.Append("WHERE TER.ACTIVE = 1 AND PP.EDITIONID = " + editionId +" AND TER.[DESCRIPTION] LIKE ('" + letra + "%') ");
        sb.Append("ORDER BY 2 ");

        DataSet ds = WITherapeutics.DEF55InstanceDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());

        return ds.Tables[0];
    }

    public DataTable getTherapeuticsByParentId(int parentId)
    {
        StringBuilder sb = new StringBuilder();

        sb.Append("SELECT   TER.[DESCRIPTION],TER.THERAPEUTICKEY,TER.THERAPEUTICID ");
        sb.Append("FROM TMPTHERA TER ");
        sb.Append("INNER JOIN THERAPEUTICS T ON (TER.THERAPEUTICID = T.THERAPEUTICID) ");
        sb.Append("WHERE T.ACTIVE = 1 AND TER.PARENTID = " + parentId);
        sb.Append(" ORDER BY LEN(TER.THERAPEUTICKEY),SUBSTRING(TER.THERAPEUTICKEY,1,1),SUBSTRING(TER.THERAPEUTICKEY,1,2),SUBSTRING(TER.THERAPEUTICKEY,1,3),SUBSTRING(TER.THERAPEUTICKEY,1,4),SUBSTRING(TER.THERAPEUTICKEY,1,5) ");

        DataSet ds = WITherapeutics.DEF55InstanceDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());

        return ds.Tables[0];

    }

    public DataTable getInformation(int editionId, int therapeuticId)
    {
        StringBuilder sb = new StringBuilder();

        sb.Append("SELECT DISTINCT P.PRODUCTID,UPPER(P.BRAND) AS BRAND,dbo.plm_dfGetActiveSubsByProduct(P.ProductId) AS ACTIVESUBSTANCES,");
        sb.Append("dbo.plm_dfGetPharmaFormsByProductTheraC (P.ProductId,P.EditionID,PT.TherapeuticId,dbo.plm_dfGetCountriesByProduct (P.PRODUCTID,P.PHARMAFORMID)) AS PHARMAFORMS,UPPER(P.DivisionSHORTNAME) AS LABORATORYNAME,dbo.plm_dfGetPagesByProduct(P.PRODUCTID,P.EDITIONID,P.DIVISIONID) AS PAGE, ");
        sb.Append("dbo.plm_dfGetCountriesByProduct (P.PRODUCTID,P.PHARMAFORMID) as Countries  ");
        sb.Append("\nFROM PLM_VWPRODUCTSBYEDITION P ");
        sb.Append("INNER JOIN PRODUCTTHERAPEUTICS PT ON(P.PRODUCTID = PT.PRODUCTID and P.PharmaFormId = PT.PharmaFormId) ");
        sb.Append("WHERE P.PRODUCTACTIVE = 1 AND P.PHARMAACTIVE = 1 AND P.EDITIONID = " + editionId + " AND PT.THERAPEUTICID = " + therapeuticId);
        sb.Append(" ORDER BY 2 ");

        DataSet ds = WITherapeutics.DEF55InstanceDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());

        return ds.Tables[0];

    }

    #endregion

    public static readonly WITherapeutics Instance = new WITherapeutics();

}
