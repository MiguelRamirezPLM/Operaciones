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
public class CIEIndice : PLMDataAccessAdapter<object>
{
    #region Constructors

    private CIEIndice()
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

        DataSet ds = CIEIndice.DEFInstanceDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());

        return ds.Tables[0];
    }

    public DataTable getCIEByParentId(int parentId, int editionId)
    {
        StringBuilder sb = new StringBuilder();

        sb.Append("\n select distinct  icd.SpanishDescription, icd.ICDKey, icd.ICDId ");
    sb.Append("\n from plm_vwProductsByEdition t ");
    sb.Append("\n join ProductICD picd on t.ProductId= picd.ProductId ");
    sb.Append("\n join ICD icd on picd.ICDId=icd.ICDId   ");
    sb.Append("\n join ICD icd1 on icd.ICDId= icd1.ParentId ");
    sb.Append("\n join ICD icd2 on icd.ParentId= icd2.ICDId");
    sb.Append("\n join ProductICD pic on icd.ICDId=picd.ICDId");
    sb.Append("\n where t.EditionId= " + editionId + "  and icd.ParentId=" + parentId);
    sb.Append("\n ORDER by 1");

        DataSet ds = CIEIndice.DEFInstanceDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());

        return ds.Tables[0];

    }

    public DataTable getCIEByParentIdN2(int parentId, int editionId)
    {
        StringBuilder sb = new StringBuilder();


        sb.Append("\n select distinct  icd1.SpanishDescription, icd1.ICDKey, icd1.ICDId  ");
            sb.Append("\n from plm_vwProductsByEdition t  ");
            sb.Append("\n join ProductICD picd on t.ProductId= picd.ProductId  ");
            sb.Append("\n join ICD icd on picd.ICDId=icd.ICDId ");   
            sb.Append("\n join ICD icd1 on icd.ICDId= icd1.ParentId  ");
            sb.Append("\n  join ProductICD pic on icd1.ICDId=pic.ICDId ");
            sb.Append("\n where t.EditionId= " + editionId + "  and icd1.ParentId =" + parentId );
            sb.Append("\n ORDER by 1 ");
 

        DataSet ds = CIEIndice.DEFInstanceDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());

        return ds.Tables[0];

    }

    public DataTable getInformation(int ICDId, int editionId)
    {
        StringBuilder sb = new StringBuilder();


        sb.Append("\n select distinct t.Brand,");
        sb.Append("\n dbo.plm_dfGetActiveSubsByProduct(t.ProductId) AS ACTIVESUBSTANCES , ");
        sb.Append("\n dbo.plm_dfGetPharmaFormsByPagedProduct(t.ProductId, t.EditionId, t.DivisionId) as PHARMAFORMS , t.LaboratoryName, ");
        sb.Append("\n dbo.plm_dfGetPagesByProduct(t.ProductId, t.EditionId, t.DivisionId) AS PAGE ");
        sb.Append("\n from plm_vwProductsByEdition t ");
        sb.Append("\n join ProductICD picd on t.ProductId= picd.ProductId   ");
        sb.Append("\n join ParticipantProducts pp on t.ProductId=pp.ProductId");
        sb.Append("\n where t.EditionId=" + editionId + "    and picd.ICDId=" + ICDId + "  and pp.EditionId= " + editionId);
        sb.Append("\n ORDER by 1");

        DataSet ds = CIEIndice.DEFInstanceDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());

        return ds.Tables[0];

    }


    public DataTable getAlphabetCIE(int editionId)
    {
        StringBuilder sb = new StringBuilder();

        sb.Append(" select distinct icd2.SpanishDescription, icd2.ICDKey, icd2.ICDId ");
        sb.Append(" from plm_vwProductsByEdition t ");
        sb.Append(" join ProductICD picd on t.ProductId= picd.ProductId ");
        sb.Append(" join ICD icd on picd.ICDId=icd.ICDId   ");
        sb.Append(" join ICD icd1 on icd.ICDId= icd1.ParentId ");
        sb.Append(" join ICD icd2 on icd.ParentId= icd2.ICDId ");
        sb.Append(" where t.EditionId= " + editionId +"  and icd2.ParentId is null");
        sb.Append("  order by 1 ");

        DataSet ds = CIEIndice.DEFInstanceDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());

        return ds.Tables[0];
    }

    public static readonly CIEIndice Instance = new CIEIndice();
}
