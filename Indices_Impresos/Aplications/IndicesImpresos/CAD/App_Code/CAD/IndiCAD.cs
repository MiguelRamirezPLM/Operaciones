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
/// Descripción breve de IndiCAD
/// </summary>
public class IndiCAD : CADDataAccessAdapter<object>
{
    #region Constructors

    private IndiCAD(){ }

    #endregion

    #region Public Methods

    public DataTable getIndications(string letra, int editionId)
    {
        StringBuilder sb = new StringBuilder();

        sb.Append("\nSELECT DISTINCT UPPER(IND.[DESCRIPTION]) AS INDICATIONS,IND.INDICATIONID ");
        sb.Append("\nFROM PLM_VWPRODUCTSBYEDITION VW ");
        sb.Append("\nINNER JOIN PRODUCTINDICATIONS PIN ON (VW.PRODUCTID = PIN.PRODUCTID) ");
        sb.Append("\nINNER JOIN INDICATIONS IND ON(PIN.INDICATIONID = IND.INDICATIONID) ");
        sb.Append("\nWHERE VW.EDITIONID = " + editionId + " AND VW.TYPEINEDITION = 'P' AND IND.ACTIVE = 1 AND IND.[DESCRIPTION] COLLATE SQL_Latin1_General_CP1_CI_AI LIKE ('" + letra + "%') ");
        sb.Append("\nORDER BY 1 ");

        DataSet ds = IndiCAD.CAD40InstanceDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());

        return ds.Tables[0];
    }

    public DataTable getInformation(int indicationId, int editionId)
    {
        StringBuilder sb = new StringBuilder();

        sb.Append("\nSELECT DISTINCT VW.PRODUCTID,UPPER(VW.BRAND) AS BRAND, ");
        sb.Append("\ndbo.plm_dfGetPharmaFormsByProductC (VW.ProductId,VW.EDITIONID,dbo.plm_dfGetCountriesByProduct (VW.PRODUCTID,VW.PHARMAFORMID)) AS PHARMAFORMS, ");
        sb.Append("\ndbo.plm_dfGetPagesByProduct(VW.PRODUCTID,VW.EDITIONID,VW.DIVISIONID) AS PAGE, ");
        sb.Append("\ndbo.plm_dfGetCountriesByProduct (VW.PRODUCTID,VW.PHARMAFORMID) as Countries ");
        sb.Append("\nFROM PLM_VWPRODUCTSBYEDITION VW ");
        sb.Append("\nINNER JOIN PRODUCTINDICATIONS PIN ON(VW.PRODUCTID = PIN.PRODUCTID) ");
        sb.Append("\nINNER JOIN INDICATIONS IND ON(PIN.INDICATIONID = IND.INDICATIONID) ");
        sb.Append("\nWHERE VW.EDITIONID = " + editionId + " AND VW.TYPEINEDITION = 'P'  AND   Page is not null AND IND.INDICATIONID = " + indicationId);
        sb.Append("\nORDER BY 2 ");

        DataSet ds = IndiCAD.CAD40InstanceDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());

        return ds.Tables[0];
    }

    public DataTable getAlphabet(int editionId)
    {
        StringBuilder sb = new StringBuilder();

        sb.Append("\nSELECT DISTINCT SUBSTRING(IND.[DESCRIPTION],1,1) AS LETTER,1 ");
        sb.Append("\nFROM PLM_VWPRODUCTSBYEDITION VW ");
        sb.Append("\nINNER JOIN PRODUCTINDICATIONS PIN ON (VW.PRODUCTID = PIN.PRODUCTID) ");
        sb.Append("\nINNER JOIN INDICATIONS IND ON(PIN.INDICATIONID = IND.INDICATIONID) ");
        sb.Append("\nWHERE VW.EDITIONID = " + editionId + " AND VW.TYPEINEDITION = 'P' AND IND.ACTIVE = 1 AND SUBSTRING(IND.[DESCRIPTION],1,1) NOT IN ('Á','Ú') ");
        sb.Append("\nORDER BY 1");

        DataSet ds = IndiCAD.CAD40InstanceDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());

        return ds.Tables[0];
    }

    #endregion

    public static readonly IndiCAD Instance = new IndiCAD();
}
