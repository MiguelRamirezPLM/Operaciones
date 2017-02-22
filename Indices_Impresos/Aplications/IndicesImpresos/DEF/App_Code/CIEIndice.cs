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

    public DataTable getCIEByParentId(int parentId, int editionId)
    {
        StringBuilder sb = new StringBuilder();

        sb.Append("\n select distinct i.SpanishDescription, i.ICDKey, i.ICDId,  i.ICDKey + ' - ' + i.SpanishDescription from plm_vwProductsByEdition t ");
            sb.Append("\n join ProductICD picd on t.ProductId= picd.ProductId and  t.pharmaformid = picd.pharmaformid ");
            sb.Append("\n join ICD i on picd.ICDId=i.ICDId ");
            sb.Append("\n where t.EditionId= " + editionId + " and i.ParentId=" + parentId);            
            sb.Append("\n order by i.icdkey ");


        DataSet ds = CIEIndice.DEFInstanceDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());

        return ds.Tables[0];

    }

    public DataTable getCIEByParentIdN2(int parentId, int editionId)
    {
        StringBuilder sb = new StringBuilder();


        sb.Append("\n select distinct  icd1.SpanishDescription, icd1.ICDKey, icd1.ICDId,  icd1.ICDKey + ' - ' + icd1.SpanishDescription  ");
            sb.Append("\n from plm_vwProductsByEdition t  ");
            sb.Append("\n join ProductICD picd on t.ProductId= picd.ProductId and  t.pharmaformid = picd.pharmaformid  ");
            sb.Append("\n join ICD icd on picd.ICDId=icd.ICDId ");   
            sb.Append("\n join ICD icd1 on icd.ICDId= icd1.ParentId  ");
            sb.Append("\n  join ProductICD pic on icd1.ICDId=pic.ICDId ");
            sb.Append("\n where t.EditionId= " + editionId + "  and icd1.ParentId =" + parentId );
            sb.Append("\n ORDER by 2 ");
 

        DataSet ds = CIEIndice.DEFInstanceDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());

        return ds.Tables[0];

    }

    public DataTable getInformation(int ICDId, int editionId)
    {
        StringBuilder sb = new StringBuilder();

        sb.Append("\n select distinct t.Brand,");
        sb.Append("\n dbo.plm_dfGetCountriesByProduct (t.ProductId,t.PharmaFormId) as Countries, "); 
        sb.Append("\n dbo.plm_dfGetActiveSubsByProduct(t.ProductId) AS ACTIVESUBSTANCES , ");
        sb.Append("\n dbo.plm_dfGetPharmaFormsByPagedProduct(t.ProductId, t.EditionId, t.DivisionId) as PHARMAFORMS , t.LaboratoryName, ");
        sb.Append("\n dbo.plm_dfGetPagesByProduct(t.ProductId, t.EditionId, t.DivisionId) AS PAGE ");
        sb.Append("\n from plm_vwProductsByEdition t ");
        sb.Append("\n join ProductICD picd on t.ProductId= picd.ProductId and  t.pharmaformid = picd.pharmaformid ");        
        sb.Append("\n where t.EditionId=" + editionId + " and t.typeInEdition = 'p' and page is not null and picd.ICDId=" + ICDId);
        sb.Append("\n ORDER by 1");

        DataSet ds = CIEIndice.DEFInstanceDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());

        return ds.Tables[0];

    }

 
    public DataTable getAlphabetCIE(int editionId)
    {
        StringBuilder sb = new StringBuilder();

        sb.Append("Select Distinct i.SpanishDescription, i.ICDKey, i.ICDId, i.ICDKey + ' - ' + i.SpanishDescription from icd i ");
        sb.Append("left join ICD i1 on i.ICDId = i1.ParentId ");
        sb.Append("left join icd i2 on i1.ICDId = i2.ParentId ");
        sb.Append("where i.ParentId is null ");
        sb.Append("and (i.ICDId in ( SElect Distinct ICDId from tmpCIE t)");
        sb.Append("or i1.ICDId in ( SElect Distinct ICDId from tmpCIE t)");
        sb.Append("or i2.ICDId in ( SElect Distinct ICDId from tmpCIE t))");
        sb.Append("order by i.ICDKey");

        DataSet ds = CIEIndice.DEFInstanceDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());

        return ds.Tables[0];
    }

    public void generateCIETmp(int editionId)
    {
        StringBuilder sb = new StringBuilder();

        CIEIndice.DEFInstanceDatabase.ExecuteNonQuery(CommandType.Text, "IF OBJECT_ID('dbo.tmpCIE', 'U') IS NOT NULL DROP TABLE dbo.tmpCIE");

        sb.Append(" Select Distinct i.* ");
        sb.Append("  into tmpCIE ");
        sb.Append("  from plm_vwProductsByEdition v ");
        sb.Append("  inner join ProductICD picd on v.ProductId = picd.ProductId ");
        sb.Append("					        and v.PharmaFormId = picd.PharmaFormId ");
        sb.Append("inner join ICD i on picd.ICDId = i.ICDId ");
        sb.Append(" where v.EditionId = " + editionId );
        sb.Append(" and i.SpanishDescription not like '%No aplica%'");

        DataSet ds = CIEIndice.DEFInstanceDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());

    }

    public void cleanCIETmp() 
    {
        CIEIndice.DEFInstanceDatabase.ExecuteDataSet(CommandType.Text, "IF OBJECT_ID('dbo.tmpCIE', 'U') IS NOT NULL DROP TABLE dbo.tmpCIE");
    }

    public static readonly CIEIndice Instance = new CIEIndice();
}
