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
/// Descripción breve de GenIndice
/// </summary>
public class GenIndice : PLMDataAccessAdapter<object>
{

    //int editionId = 48;

    #region Constructors

    public GenIndice() { }

    #endregion

    public DataTable getAlphabet(int editionId)
    {
        StringBuilder sb = new StringBuilder();

        sb.Append("SELECT DISTINCT SUBSTRING(BRAND,1,1) as Letter ");
        sb.Append("FROM PLM_VWPRODUCTSBYEDITION ");
        sb.Append("WHERE EDITIONID = " + editionId + " AND TYPEINEDITION = 'P' ");
        sb.Append("ORDER BY 1");

        DataSet ds = GenIndice.DEFInstanceDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());

        return ds.Tables[0];

    }

    public DataTable getAlphabetC(int editionId)
    {
        StringBuilder sb = new StringBuilder();

                sb.Append(" select distinct SUBSTRING(BRAND,1,1) as Letter ");
                sb.Append("\n from plm_vwProductsByEdition  v ");
                sb.Append("\n left join productSubstances ps on v.ProductId = ps.ProductId  ");
                sb.Append("\n left join activeSubstances s on ps.ActiveSubstanceId = s.ActiveSubstanceId  ");
                sb.Append("\n where EditionId =  " + editionId);
                sb.Append("\n and  (dbo.plm_dfGetICDContraindicationsByProduct(v.ProductId, v.pharmaformId, v.divisionId, v.CategoryId, ps.activeSubstanceId, v.EditionId)<> '' or ");
                sb.Append("\n dbo.plm_dfGetPhysiologicalContraindicationsByProduct(v.ProductId, v.pharmaformId, v.divisionId, v.CategoryId, ps.activeSubstanceId, v.EditionId) <> '' or ");
                sb.Append("\n dbo.plm_dfGetHypersensibilityContraindicationsByProduct(v.ProductId, v.pharmaformId, v.divisionId, v.CategoryId, ps.activeSubstanceId, v.EditionId)  <> '' or ");
                sb.Append("\n dbo.plm_dfGetPharmaGroupContraindicationsByProduct(v.ProductId, v.pharmaformId, v.divisionId, v.CategoryId, ps.activeSubstanceId, v.EditionId) <> '' ) ");
                sb.Append("\n AND TYPEINEDITION = 'P'  ");
                sb.Append("\n ORDER BY 1   ");


          DataSet ds = GenIndice.DEFInstanceDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());

        return ds.Tables[0];

    }

    public DataTable getSubstances(int editionId)
    {
        StringBuilder sb = new StringBuilder();

        sb.Append("IF OBJECT_ID('tmpReference') IS NOT NULL   ");
        sb.Append("\n DROP TABLE  tmpReference; ");

        DataSet dsdelete = GenIndice.DEFInstanceDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());

        sb.Append(" SELECT distinct   ClinicalReference,  st, ActiveSubstanceId , FoodId,  IMASeverityId, ClinicalReferenceId  ");
        sb.Append("\n into tmpReference	 ");
        sb.Append("\n FROM (select distinct  cr.ClinicalReference , cr.ClinicalReferenceId , 1 as st , imsf.ActiveSubstanceId, imsf.FoodId, imsf.IMASeverityId ");
        sb.Append("\n from IMSubstanceFoods imsf  ");
        sb.Append("\n inner join Foods f on imsf.FoodId = f.FoodId ");
        sb.Append("\n inner join IMASeverities imas on imsf.IMASeverityId = imas.IMASeverityId");
        sb.Append("\n inner join IMSubstanceFoodReferences imsfr on imsf.ActiveSubstanceId = imsfr.ActiveSubstanceId ");
        sb.Append("\n and imsf.FoodId = imsfr.FoodId ");
        sb.Append("\n and imsf.IMASeverityId = imsfr.IMASeverityId ");
        sb.Append("\n inner join ClinicalReferences cr on imsfr.ClinicalReferenceId = cr.ClinicalReferenceId	");
        sb.Append("\n union ");
        sb.Append("\n select distinct   cr.ClinicalReference , cr.ClinicalReferenceId, 2 as st , imsft.ActiveSubstanceId, imsft.FoodSubtypeId, imsft.IMASeverityId");
        sb.Append("\n from IMSubstanceFoodSubtypes imsft");
        sb.Append("\n inner join FoodSubtypes fst on imsft.FoodSubtypeId = fst.FoodSubtypeId");
        sb.Append("\n inner join IMASeverities imas on imsft.IMASeverityId = imas.IMASeverityId");
        sb.Append("\n inner join IMSubstanceFoodSubtypeReferences imsfsr on imsft.ActiveSubstanceId = imsfsr.ActiveSubstanceId");
        sb.Append("\n and imsft.FoodSubtypeId = imsfsr.FoodSubtypeId");
        sb.Append("\n and imsft.IMASeverityId = imsfsr.IMASeverityId");
        sb.Append("\n inner join ClinicalReferences cr on imsfsr.ClinicalReferenceId = cr.ClinicalReferenceId ) AS CONSULTA");


        DataSet ds1 = GenIndice.DEFInstanceDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());


        sb.Append(" select distinct a.Description,a.ActiveSubstanceId, dbo.[plm_dfGetATCPharmacologicalGroupsBySubs](a.ActiveSubstanceId) as ATCFG ");
        sb.Append("\n from IMSubstanceFoods imsf  ");
        sb.Append("\n inner join ActiveSubstances a on imsf.ActiveSubstanceId = a.ActiveSubstanceId  ");
        sb.Append("\n union ");
        sb.Append("\n select distinct    a.Description, a.ActiveSubstanceId, dbo.[plm_dfGetATCPharmacologicalGroupsBySubs](a.ActiveSubstanceId) as ATCFG ");
        sb.Append("\n from IMSubstanceFoodSubtypes imsft ");
        sb.Append("\n inner join ActiveSubstances a on imsft.ActiveSubstanceId = a.ActiveSubstanceId  ");
        sb.Append("\n order by a.Description  ");
    
        DataSet ds = GenIndice.DEFInstanceDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());

        return ds.Tables[0];

    }


    public DataTable getIMSubstancesF(int substanceId)
    {
        StringBuilder sbDelete = new StringBuilder();

        sbDelete.Append("IF OBJECT_ID('tmpREF') IS NOT NULL   ");
        sbDelete.Append("\n DROP TABLE  tmpREF; ");

        DataSet dsdelete = GenIndice.DEFInstanceDatabase.ExecuteDataSet(CommandType.Text, sbDelete.ToString());


        StringBuilder sbREF = new StringBuilder();

        sbREF.Append(" select distinct ROW_NUMBER() over(ORDER BY   ClinicalReferenceId) num, ClinicalReferenceId , activesubstanceid, ");
        sbREF.Append("\n   ClinicalReference, count (activesubstanceid) c ");
        sbREF.Append("\n into tmpREF   ");
        sbREF.Append("\n from  tmpReference");
        sbREF.Append("\n where activesubstanceid = " + substanceId);
        sbREF.Append("\n group by ClinicalReferenceId , activesubstanceid,  ClinicalReference");

        DataSet dsREF = GenIndice.DEFInstanceDatabase.ExecuteDataSet(CommandType.Text, sbREF.ToString());

        StringBuilder sbf = new StringBuilder();

        sbf.Append(" select distinct f.Description, imsf.ClinicalSummary , ");
        sbf.Append("\n case imas.IMASeverity  when 'Verde|Sugerencia' then 'VerdeSugerencia'");
        sbf.Append("\n when 'Verde|Espaciar' then 'VerdeEspaciar'");
        sbf.Append("\n else imas.IMASeverity END IMASeverity, dbo.[plm_dfGetClinicalReferenceSubstanceFoodType](imsf.ActiveSubstanceId, imsf.FoodId, imsf.IMASeverityId, 1 ) as Reference  ");
        sbf.Append("\n from IMSubstanceFoods imsf ");
        sbf.Append("\n inner join Foods f on imsf.FoodId = f.FoodId");
        sbf.Append("\n inner join IMASeverities imas on imsf.IMASeverityId = imas.IMASeverityId");
        sbf.Append("\n inner join IMSubstanceFoodReferences imsfr on imsf.ActiveSubstanceId = imsfr.ActiveSubstanceId");
        sbf.Append("\n and imsf.FoodId = imsfr.FoodId");
        sbf.Append("\n and imsf.IMASeverityId = imsfr.IMASeverityId");
        sbf.Append("\n inner join ClinicalReferences cr on imsfr.ClinicalReferenceId = cr.ClinicalReferenceId	");
        sbf.Append("\n where imsf.ActiveSubstanceId = " + substanceId);
        sbf.Append("\n union");
        sbf.Append("\n select distinct  fst.Description , imsft.ClinicalSummary, ");
        sbf.Append("\n case imas.IMASeverity  when 'Verde|Sugerencia' then 'VerdeSugerencia'");
        sbf.Append("\n when 'Verde|Espaciar' then 'VerdeEspaciar'");
        sbf.Append("\n else imas.IMASeverity  END IMASeverity, dbo.[plm_dfGetClinicalReferenceSubstanceFoodType](imsft.ActiveSubstanceId, imsft.FoodSubtypeId, imsft.IMASeverityId,2) as Reference  ");
        sbf.Append("\n from IMSubstanceFoodSubtypes imsft");
        sbf.Append("\n inner join FoodSubtypes fst on imsft.FoodSubtypeId = fst.FoodSubtypeId");
        sbf.Append("\n inner join IMASeverities imas on imsft.IMASeverityId = imas.IMASeverityId");
        sbf.Append("\n inner join IMSubstanceFoodSubtypeReferences imsfsr on imsft.ActiveSubstanceId = imsfsr.ActiveSubstanceId");
        sbf.Append("\n and imsft.FoodSubtypeId = imsfsr.FoodSubtypeId");
        sbf.Append("\n and imsft.IMASeverityId = imsfsr.IMASeverityId");
        sbf.Append("\n inner join ClinicalReferences cr on imsfsr.ClinicalReferenceId = cr.ClinicalReferenceId	");
        sbf.Append("\n where imsft.ActiveSubstanceId = " + substanceId);
        sbf.Append("\n order by 1  ");



        DataSet ds = GenIndice.DEFInstanceDatabase.ExecuteDataSet(CommandType.Text, sbf.ToString());

        return ds.Tables[0];

    }

    public DataTable getIMSubstancesR(int substanceId)
    {
        StringBuilder sb = new StringBuilder();

        sb.Append(" SELECT  distinct ROW_NUMBER() over(ORDER BY   ClinicalReferenceId) num, ClinicalReference, ClinicalReferenceId ");
       sb.Append("\n FROM (select distinct  cr.ClinicalReference , cr.ClinicalReferenceId ");
       sb.Append("\n from IMSubstanceFoods imsf  ");
       sb.Append("\n inner join Foods f on imsf.FoodId = f.FoodId ");
       sb.Append("\n inner join IMASeverities imas on imsf.IMASeverityId = imas.IMASeverityId");
       sb.Append("\n inner join IMSubstanceFoodReferences imsfr on imsf.ActiveSubstanceId = imsfr.ActiveSubstanceId ");
						 sb.Append("\n and imsf.FoodId = imsfr.FoodId ");
						 sb.Append("\n and imsf.IMASeverityId = imsfr.IMASeverityId ");
       sb.Append("\n inner join ClinicalReferences cr on imsfr.ClinicalReferenceId = cr.ClinicalReferenceId		");								  
       sb.Append("\n where imsf.ActiveSubstanceId =  " + substanceId );
       sb.Append("\n union ");
       sb.Append("\n select distinct   cr.ClinicalReference , cr.ClinicalReferenceId");
       sb.Append("\n from IMSubstanceFoodSubtypes imsft");
       sb.Append("\n inner join FoodSubtypes fst on imsft.FoodSubtypeId = fst.FoodSubtypeId");
       sb.Append("\n inner join IMASeverities imas on imsft.IMASeverityId = imas.IMASeverityId");
       sb.Append("\n inner join IMSubstanceFoodSubtypeReferences imsfsr on imsft.ActiveSubstanceId = imsfsr.ActiveSubstanceId");
						  sb.Append("\n and imsft.FoodSubtypeId = imsfsr.FoodSubtypeId");
						  sb.Append("\n and imsft.IMASeverityId = imsfsr.IMASeverityId");
       sb.Append("\n inner join ClinicalReferences cr on imsfsr.ClinicalReferenceId = cr.ClinicalReferenceId");				
       sb.Append("\n where imsft.ActiveSubstanceId = " + substanceId +" ) AS CONSULTA");
       sb.Append("\n order by 2");

   


        DataSet ds = GenIndice.DEFInstanceDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());

        return ds.Tables[0];

    }
    public DataTable getProducts(int editionId, string letra)
    {
        StringBuilder sb = new StringBuilder();

        sb.Append("\nSELECT DISTINCT V.PRODUCTID,UPPER(V.BRAND) AS BRAND, SUBSTRING(V.[PRODUCTDESCRIPTION],1,1) + LOWER(SUBSTRING(v.[PRODUCTDESCRIPTION],2,LEN(V.[PRODUCTDESCRIPTION])))AS [PRODUCTDESCRIPTION],");
        sb.Append("\ndbo.plm_dfGetActiveSubsByProduct(V.PRODUCTID) AS ACTIVESUBSTANCES, ");
        sb.Append("\ndbo.plm_dfGetPharmaFormsByPagedProduct(V.PRODUCTID, V.EDITIONID, V.DIVISIONID) as PHARMAFORMS, ");
        sb.Append("\nCASE WHEN V.DivisionId IN (83,100,2335,2336) ");
        sb.Append("\nTHEN V.LaboratoryName ELSE V.DivisionShortName END AS LABORATORYNAME, dbo.plm_dfGetPagesByProduct(V.PRODUCTID,V.EDITIONID, V.DIVISIONID) AS PAGE ");
        sb.Append("\nFROM PLM_VWPRODUCTSBYEDITION V ");
        sb.Append("\nWHERE V.EDITIONID = " + editionId + " AND TYPEINEDITION = 'P' ");


        if (letra.Equals("N") || letra.Equals("Ñ"))
        {
            sb.Append(" AND V.BRAND LIKE ('" + letra + "%') And V.productActive = 1 ");
        }
        else
        {
            sb.Append("AND V.BRAND COLLATE SQL_Latin1_General_CP1_CI_AI LIKE ('" + letra + "%') And V.productActive = 1 ");
        }

        sb.Append(" ORDER BY 2");

        DataSet ds = GenIndice.DEFInstanceDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());

        return ds.Tables[0];

    }

    public DataTable getProductInteractions(int editionId,string letra)
    {
        StringBuilder sb = new StringBuilder();

        //sb.Append("SELECT [Brand] ");
        //sb.Append("\n,[PharmaForm] ");
        //sb.Append("\n,[ActiveSusbtance] ");
        //sb.Append("\n,[Therapeutic] ");
        //sb.Append("\n,[PharmacologicalGroups] ");
        //sb.Append("\n,[Interactions] ");
        //sb.Append("\n,[otherElement] ");
        //sb.Append("\nFROM [tmpSNCIM2] "); 

sb.Append("Select	Distinct ");
sb.Append("\nv.Brand, ");
sb.Append("\nv.PharmaForm, ");
sb.Append("\ndbo.plm_dfGetActiveSubsByProduct(v.ProductId) as ActiveSusbtance, ");
sb.Append("\ndbo.[plm_dfGetTherapeuticsByProduct](v.productId, v.PharmaFormId) as Therapeutic,	 ");	
sb.Append("\ndbo.plm_dfGetPharmaGroupsInteractionsByProduct(v.ProductId, v.pharmaformId, v.divisionId, v.CategoryId, ps.activeSubstanceId, v.EditionId) as PharmacologicalGroups, ");
sb.Append("\ndbo.[plm_dfGetActiveSubsInteractionsByProduct](v.ProductId, v.pharmaformId, v.divisionId, v.CategoryId, ps.activeSubstanceId, v.EditionId) as Interactions, ");
sb.Append("\ndbo.[plm_dfGetOtherInteractionsByProduct](v.ProductId, v.pharmaformId, v.divisionId, v.CategoryId, ps.activeSubstanceId, v.EditionId) as otherElement ");

sb.Append("\nfrom plm_vwproductsByEdition v  ");
sb.Append("\nleft join productSubstances ps on v.ProductId = ps.ProductId  ");
sb.Append("\nleft join activeSubstances s on ps.ActiveSubstanceId = s.ActiveSubstanceId  ");

sb.Append("\nwhere v.editionId = " + editionId);
sb.Append("\nand ( dbo.plm_dfGetPharmaGroupsInteractionsByProduct(v.ProductId, v.pharmaformId, v.divisionId, v.CategoryId, ps.activeSubstanceId, v.EditionId) <> ''  ");
sb.Append("\nor dbo.[plm_dfGetActiveSubsInteractionsByProduct](v.ProductId, v.pharmaformId, v.divisionId, v.CategoryId, ps.activeSubstanceId, v.EditionId) <> '' ");
sb.Append("\nor dbo.[plm_dfGetOtherInteractionsByProduct](v.ProductId, v.pharmaformId, v.divisionId, v.CategoryId, ps.activeSubstanceId, v.EditionId) <> '') ");
sb.Append("\n and v.brand like '" + letra + "%' ");
sb.Append("\norder by v.brand, v.pharmaform ");


        
//sb.Append("Select	Distinct ");
//sb.Append("\nv.Brand,");
//sb.Append("\nv.PharmaForm,");
//sb.Append("\ndbo.plm_dfGetActiveSubsByProduct(v.ProductId) as ActiveSusbtance,");
//sb.Append("\nt.therapeuticKey + ' ' + t.SpanishDescription as Therapeutic,");		
//sb.Append("\ndbo.plm_dfGetPharmaGroupsInteractionsByProduct(pcs.ProductId, pcs.ActiveSubstanceId, pcs.ProductContentId, v.editionId) as PharmacologicalGroups,");
//sb.Append("\ndbo.plm_dfGetActiveSubsInteractionsByProduct(pcs.ProductId, pcs.ActiveSubstanceId, pcs.ProductContentId, v.editionId) as Interactions,");
//sb.Append("\ndbo.[plm_dfGetOtherInteractionsByProduct](pcs.ProductId, pcs.ActiveSubstanceId, pcs.ProductContentId, v.editionId) as otherElement ");
//sb.Append("\nfrom plm_vwproductsByEdition v ");
//sb.Append("\nleft join productSubstances ps on v.ProductId = ps.ProductId ");
//sb.Append("\nleft join activeSubstances s on ps.ActiveSubstanceId = s.ActiveSubstanceId ");
//sb.Append("\nleft join productContents pc on v.EditionId = pc.EditionId ");
//sb.Append("\nand v.DivisionId = pc.DivisionId");
//sb.Append("\nand v.CategoryId = pc.CategoryId");
//sb.Append("\nand v.ProductId = pc.ProductId");
//sb.Append("\nand v.PharmaFormId = pc.PharmaFormId");
//sb.Append("\ninner join productContentSubstances pcs on pc.ProductContentId = pcs.ProductContentId");
//sb.Append("\nand pc.ProductId = pcs.ProductId");
//sb.Append("\nand ps.ActiveSubstanceId = pcs.ActiveSubstanceId");
//sb.Append("\nleft join productTherapeutics pt on v.productId = pt.productId");
//sb.Append("\nand v.PharmaFormId = pt.PharmaFormId");
//sb.Append("\nleft join therapeutics t on pt.TherapeuticId = t.TherapeuticId");
//sb.Append("\nwhere v.editionId = " + editionId );
//sb.Append("\nand ( dbo.plm_dfGetPharmaGroupsInteractionsByProduct(pcs.ProductId, pcs.ActiveSubstanceId, pcs.ProductContentId, v.editionId) <> '' ");
//sb.Append("\nor dbo.plm_dfGetActiveSubsInteractionsByProduct(pcs.ProductId, pcs.ActiveSubstanceId, pcs.ProductContentId, v.editionId) <> ''");
//sb.Append("\nor dbo.[plm_dfGetOtherInteractionsByProduct](pcs.ProductId, pcs.ActiveSubstanceId, pcs.ProductContentId, v.editionId) <> '')");
////sb.Append("\nand v.brand like '" + letra + "%'");
//sb.Append("\norder by v.brand, v.pharmaform");


        //if (letra.Equals("N") || letra.Equals("Ñ"))
        //{
        //    sb.Append(" AND V.BRAND LIKE ('" + letra + "%') And V.productActive = 1 ");
        //}
        //else
        //{
        //    sb.Append("AND V.BRAND COLLATE SQL_Latin1_General_CP1_CI_AI LIKE ('" + letra + "%') And V.productActive = 1 ");
        //}
                
        DataSet ds = GenIndice.DEFInstanceDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());

        return ds.Tables[0];
    }

    public DataTable getProductContraindications(int editionId, string letra)
    {
        StringBuilder sb = new StringBuilder();
         

        sb.Append("select distinct  v.Brand, v.PharmaForm, ");
        sb.Append("\n dbo.plm_dfGetActiveSubsByProduct(v.ProductId) as ActiveSusbtance,");
        sb.Append("\n dbo.plm_dfGetICDContraindicationsByProduct(v.ProductId, v.pharmaformId, v.divisionId, v.CategoryId, ps.activeSubstanceId, v.EditionId)  as ICDContraindications,");
        sb.Append("\n dbo.plm_dfGetPhysiologicalContraindicationsByProduct(v.ProductId, v.pharmaformId, v.divisionId, v.CategoryId, ps.activeSubstanceId, v.EditionId)  as PhysiologicalContraindications,");
        sb.Append("\n dbo.plm_dfGetHypersensibilityContraindicationsByProduct(v.ProductId, v.pharmaformId, v.divisionId, v.CategoryId, ps.activeSubstanceId, v.EditionId)  as HypersensibilityContraindications,");
        sb.Append("\n dbo.plm_dfGetPharmaGroupContraindicationsByProduct(v.ProductId, v.pharmaformId, v.divisionId, v.CategoryId, ps.activeSubstanceId, v.EditionId)  as PharmaGroupContraindications");
        sb.Append("\n , dbo.plm_dfGetContraindicationsPrecautionByProduct (v.ProductId, v.pharmaformId, v.divisionId, v.CategoryId, v.EditionId)  as Precautions ");
        sb.Append("\n , dbo.plm_dfGetContraindicationsByProduct (v.ProductId, v.pharmaformId, v.divisionId, v.CategoryId, v.EditionId)  as Contraindications ");
        sb.Append("\n ,di.ImageName ");
        sb.Append("\n from plm_vwProductsByEdition  v ");
        sb.Append("\n left join productSubstances ps on v.ProductId = ps.ProductId  ");
        sb.Append("\n left join activeSubstances s on ps.ActiveSubstanceId = s.ActiveSubstanceId   ");
        sb.Append("\n left join DivisionImages di on v.DivisionId = di.DivisionId ");
        sb.Append("\n where EditionId =" + editionId);
        sb.Append("\n and  (dbo.plm_dfGetICDContraindicationsByProduct(v.ProductId, v.pharmaformId, v.divisionId, v.CategoryId, ps.activeSubstanceId, v.EditionId)<> '' or ");
        sb.Append("\n dbo.plm_dfGetPhysiologicalContraindicationsByProduct(v.ProductId, v.pharmaformId, v.divisionId, v.CategoryId, ps.activeSubstanceId, v.EditionId) <> '' or ");
        sb.Append("\n dbo.plm_dfGetHypersensibilityContraindicationsByProduct(v.ProductId, v.pharmaformId, v.divisionId, v.CategoryId, ps.activeSubstanceId, v.EditionId)  <> '' or ");
        sb.Append("\n dbo.plm_dfGetPharmaGroupContraindicationsByProduct(v.ProductId, v.pharmaformId, v.divisionId, v.CategoryId, ps.activeSubstanceId, v.EditionId) <> '' ) ");
        sb.Append("\nand v.brand like '" + letra + "%'"); 
        sb.Append("\norder by v.brand, v.pharmaform ");


        DataSet ds = GenIndice.DEFInstanceDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());

        return ds.Tables[0];
    }

    /////////////////////////////////////////////////////// Productos ATC-ICD ///////////////////////////////////////////////////////

    public DataTable getAlpha(int editionId)
    {
        StringBuilder sb = new StringBuilder();

        sb.Append(" select distinct SUBSTRING (Brand COLLATE SQL_Latin1_General_CP1_CI_AI, 1,1) from plm_vwProductsByEdition v ");
        sb.Append("\n where EditionId = " + editionId);
        sb.Append("\n order by 1");

        DataSet ds = GenIndice.DEFInstanceDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());

        return ds.Tables[0];
    }
    public DataTable getProductATCICD(int editionId, string letra)
    {
        StringBuilder sb = new StringBuilder();

        sb.Append(" select distinct  v.ProductId ,v.Brand, v.PharmaFormId, v.PharmaForm, v.ProductDescription , dbo.plm_dfGetActiveSubsByProduct(v.productid) ActiveSubstance,v.DivisionName, ");
        sb.Append("\n dbo.plm_dfGetTherapeuticsByProductColombia(v.ProductId, PharmaFormId) ATC , dbo.plm_dfGetICDByProductColombia(v.ProductId) ICD");
        sb.Append("\n from plm_vwProductsByEdition v		");
        sb.Append("\n where v.EditionId = " + editionId + " and v.brand like '" + letra + "%'"); 
        sb.Append("\n order by v.Brand, v.PharmaForm");

        DataSet ds = GenIndice.DEFInstanceDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());

        return ds.Tables[0];

     }

    #region Recent Products

    public DataTable getRecentProducts(int editionId)
    {
        StringBuilder sb = new StringBuilder();

        sb.Append("Select Distinct Brand,PharmaForm, ");
        sb.Append("\nCASE WHEN DivisionId IN (83,100,2335,2336) ");
        sb.Append("\nTHEN LaboratoryName ELSE DivisionShortName END AS DivisionName, ");
        sb.Append("\ndbo.plm_dfGetActiveSubsByProduct(ProductId) AS Substances,Page ");
        sb.Append("\nFrom plm_vwProductsByEdition ");
        sb.Append("\nWhere NewProduct = 1 and EditionId = " + editionId);
        sb.Append("\nOrder By Brand,PharmaForm ");

        DataSet ds = GenIndice.DEFInstanceDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());

        return ds.Tables[0];


    }

    #endregion

    public static readonly GenIndice Instance = new GenIndice();
}
