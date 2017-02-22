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
/// Descripción breve de TheraCAD
/// </summary>
public class TheraCAD : CADDataAccessAdapter<object>
{
    #region Constructors

    private TheraCAD(){ }

    #endregion

    #region Public Methods
    
    public DataTable getTherapeuticsByParentId(int parentId)
    {
        StringBuilder sb = new StringBuilder();

        //sb.Append("SELECT  DISTINCT  TER.[DESCRIPTION],TER.THERAPEUTICKEY,TER.THERAPEUTICID ");
        ////sb.Append("LEN(TER.THERAPEUTICKEY),SUBSTRING(TER.THERAPEUTICKEY,1,1),SUBSTRING(TER.THERAPEUTICKEY,2,1),SUBSTRING(TER.THERAPEUTICKEY,3,1),SUBSTRING(TER.THERAPEUTICKEY,4,1) ");
        //sb.Append("FROM TMPTHERA TER ");
        //sb.Append("INNER JOIN THERAPEUTICS T ON (TER.THERAPEUTICID = T.THERAPEUTICID) ");
        //sb.Append("WHERE T.ACTIVE = 1 AND TER.PARENTID = " + parentId);
        //sb.Append(" ORDER BY LEN(T.THERAPEUTICKEY),SUBSTRING(T.THERAPEUTICKEY,1,1),SUBSTRING(T.THERAPEUTICKEY,2,1),SUBSTRING(T.THERAPEUTICKEY,3,1),SUBSTRING(T.THERAPEUTICKEY,4,1),SUBSTRING(T.THERAPEUTICKEY,5,1) ");

        sb.Append("SELECT TER.[DESCRIPTION],TER.THERAPEUTICKEY,TER.THERAPEUTICID ");
        sb.Append("FROM TMPTHERA TER  ");
        sb.Append("INNER JOIN THERAPEUTICS T ON (TER.THERAPEUTICID = T.THERAPEUTICID)  ");
        sb.Append("WHERE T.ACTIVE = 1 AND  T.PARENTID = " + parentId);
        sb.Append("ORDER BY LEN(T.THERAPEUTICKEY),SUBSTRING(T.THERAPEUTICKEY,1,1),SUBSTRING(T.THERAPEUTICKEY,2,1),SUBSTRING(T.THERAPEUTICKEY,3,1),SUBSTRING(T.THERAPEUTICKEY,4,1),SUBSTRING(T.THERAPEUTICKEY,5,1) ");


        DataSet ds = TheraCAD.CAD40InstanceDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());

        return ds.Tables[0];

    }

    public DataTable getInformation(int therapeuticId, int editionId)
    {
        StringBuilder sb = new StringBuilder();

        sb.Append("\nSELECT DISTINCT VW.PRODUCTID,UPPER(VW.BRAND) AS BRAND, ");
        sb.Append("\ndbo.plm_dfGetActiveSubsByProduct(VW.ProductId) AS ACTIVESUBSTANCES, ");
        //sb.Append("\ndbo.plm_dfGetPharmaFormsByProductTheraC (VW.ProductId,VW.EDITIONID,TER.THERAPEUTICID,VW.PAGE,dbo.plm_dfGetCountriesByProduct (VW.PRODUCTID,VW.PHARMAFORMID),VW.DIVISIONID,VW.CATEGORYID) AS PHARMAFORMS, ");
        sb.Append("\ndbo.plm_dfGetPharmaFormsByProductTheraC (VW.ProductId,VW.EDITIONID,TER.THERAPEUTICID,dbo.plm_dfGetCountriesByProduct(VW.PRODUCTID,VW.PHARMAFORMID)) AS PHARMAFORMS, ");
        sb.Append("\nUPPER(VW.DIVISIONSHORTNAME) AS LABORATORYNAME, VW.PAGE, ");
        sb.Append("\ndbo.plm_dfGetCountriesByProduct (VW.PRODUCTID,VW.PHARMAFORMID) as Countries ");
        sb.Append("\nFROM PLM_VWPRODUCTSBYEDITION VW ");
        sb.Append("\nINNER JOIN PRODUCTTHERAPEUTICS PT ON (VW.PRODUCTID = PT.PRODUCTID AND VW.PHARMAFORMID = PT.PHARMAFORMID) ");
        sb.Append("\nINNER JOIN THERAPEUTICS TER ON (PT.THERAPEUTICID = TER.THERAPEUTICID) ");
        sb.Append("\nWHERE VW.EDITIONID = " + editionId + " AND VW.DIVISIONACTIVE = 1 AND VW.TYPEINEDITION = 'P' AND   Page is not null  AND TER.THERAPEUTICID = " + therapeuticId);
        sb.Append("\nORDER BY 2 ");

        DataSet ds = TheraCAD.CAD40InstanceDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());

        return ds.Tables[0];

    }

    public DataTable getAlphabet()
    {
        StringBuilder sb = new StringBuilder();

        sb.Append("SELECT [DESCRIPTION],THERAPEUTICKEY,THERAPEUTICID ");
        sb.Append("FROM TMPTHERA ");
        sb.Append("WHERE PARENTID IS NULL ");
        sb.Append("ORDER BY 2");

        DataSet ds = TheraCAD.CAD40InstanceDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());

        return ds.Tables[0];
    }

    #endregion

    public static readonly TheraCAD Instance = new TheraCAD();
}
