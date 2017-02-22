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

public class GetPEV : PEVDataAccessAdapter < object>
{
    #region constructor
     public GetPEV()
	  { }
    #endregion

     ///////////////////////////////////////// INDICE GENERAL /////////////////////////////////////////

    public DataTable getAlphabet(int editionId)
    {
        StringBuilder sb = new StringBuilder();

        sb.Append("SELECT DISTINCT SUBSTRING(PRODUCTNAME COLLATE SQL_Latin1_General_CP1_CI_AI,1,1) , countryId ");
        sb.Append("\n FROM plm_vwProductsByEdition ");
        sb.Append("\n WHERE EDITIONID = " + editionId + " ");
        sb.Append("\n ORDER BY 1");

        DataSet ds =GetPEV.DEFInstanceDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());

        return ds.Tables[0];
    }

    public DataTable getPoducts(int editionId, string letra)
    {
        StringBuilder sb = new StringBuilder();

        sb.Append("\n SELECT	DISTINCT V.PRODUCTID,");
        sb.Append("\n UPPER(V.PRODUCTNAME) AS PRODUCTNAME,");
        sb.Append("\n V.ProductDescription, ");
        sb.Append("\n CASE WHEN TYPEINEDITION = 'P' ");
        sb.Append("\n THEN dbo.plm_dfGetPharmaFormsByDiffPagedProduct(V.PRODUCTID, V.EDITIONID,V.PAGE) ");
        sb.Append("\n ELSE dbo.[plm_dfGetPharmaFormsByDiffPageMentionated](V.PRODUCTID, V.EDITIONID)END as PHARMAFORM, ");
       //sb.Append("\n dbo.plm_dfGetActiveSubsByProduct(V.PRODUCTID) AS SUBSTANCE,");  
        sb.Append("\n UPPER(V.DIVISIONSHORTNAME) AS DIVISION,");
        sb.Append("\n v.PAGE, v.CountryId");
        sb.Append("\n FROM plm_vwProductsByEdition V ");
        sb.Append("\n WHERE V.EDITIONID=" + editionId + " AND V.PRODUCTNAME COLLATE SQL_Latin1_General_CP1_CI_AI LIKE ('" + letra + "%') And V.PRODUCTACTIVE = 1 ");
        sb.Append("\n ORDER BY 2 ");
 
        DataSet ds = GetPEV.DEFInstanceDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());

        return ds.Tables[0];
    }

    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


    ///////////////////////////////////////// INDICE DE SUSTANCIAS /////////////////////////////////////////

    public DataTable getAlphabetBySub(int edition)
    {
        StringBuilder sb = new StringBuilder();


        sb.Append("\n SELECT DISTINCT SUBSTRING(S.ACTIVESUBSTANCENAME COLLATE SQL_Latin1_General_CP1_CI_AI, 1,1) , 1 ");
        sb.Append("\n FROM dbo.plm_vwProductsByEdition V ");
        sb.Append("\n INNER JOIN PRODUCTSUBSTANCES PS ON V.PRODUCTID = PS.PRODUCTID ");
        sb.Append("\n INNER JOIN ACTIVESUBSTANCES S ON PS.ACTIVESUBSTANCEID = S.ACTIVESUBSTANCEID ");
        sb.Append("\n WHERE V.EDITIONID = " + edition.ToString() + " AND V.TypeInEdition='P' ");
        sb.Append("\n ORDER BY 1");

        DataSet ds = GetPEV.DEFInstanceDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());

        return ds.Tables[0];
    }

    public DataTable getSubstances(int edition, string letra)
    {
        StringBuilder sb = new StringBuilder();

        sb.Append("\n SELECT DISTINCT S.ACTIVESUBSTANCENAME AS ACTIVESUBSTANCE, ");
        sb.Append("\n S.ACTIVESUBSTANCEID AS ID ");
        sb.Append("\n FROM dbo.plm_vwProductsByEdition V ");
        sb.Append("\n INNER JOIN PRODUCTSUBSTANCES PS ON (V.PRODUCTID = PS.PRODUCTID) ");
        sb.Append("\n INNER JOIN ACTIVESUBSTANCES S ON PS.ACTIVESUBSTANCEID = S.ACTIVESUBSTANCEID ");
        sb.Append("\n WHERE V.EDITIONID= " + edition.ToString() + " AND S.ACTIVESUBSTANCENAME COLLATE SQL_Latin1_General_CP1_CI_AI LIKE ('" + letra + "%') and TypeInEdition='P'");
        sb.Append("\n ORDER BY 1");

        DataSet ds = GetPEV.DEFInstanceDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());

        return ds.Tables[0];
    }

    public DataTable getInfor(int editionId, string activeSubsId)
    {
        StringBuilder sb = new StringBuilder();
           
        sb.Append("\n SELECT DISTINCT V.PRODUCTID, ");
        sb.Append("\n UPPER(V.PRODUCTNAME) AS PRODUCTNAME, ");
        sb.Append("\n dbo.plm_dfGetActiveSubstancesByProduct(V.PRODUCTID,PS.ACTIVESUBSTANCEID) AS ACTIVESUBSTANCES, ");
       // sb.Append("\n dbo.plm_dfGetPharmaFormsByDiffPagedProduct(V.PRODUCTID, V.EDITIONID, V.PAGE) AS PHARMAFORM, ");
        sb.Append("\n dbo.plm_dfGetPharmaFormsByPagedProduct(V.PRODUCTID, V.EDITIONID, V.DIVISIONID)  AS PHARMAFORM, ");
        sb.Append("\n UPPER(V.DIVISIONSHORTNAME) AS Division, V.Page AS PAGE ");
        sb.Append("\n FROM dbo.plm_vwProductsByEdition V ");
        sb.Append("\n INNER JOIN PRODUCTSUBSTANCES PS ON (v.PRODUCTID = PS.PRODUCTID)  ");
        sb.Append("\n INNER JOIN ACTIVESUBSTANCES ASUB ON (PS.ACTIVESUBSTANCEID = ASUB.ACTIVESUBSTANCEID) ");
        sb.Append("\n WHERE V.EDITIONID = " + editionId.ToString() + " AND ASUB.[ACTIVESUBSTANCEID] = " + activeSubsId + " and V.TypeInEdition='P'");
        sb.Append("\n ORDER BY 2,4");

        DataSet ds = GetPEV.DEFInstanceDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());

        return ds.Tables[0];
    }


    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


    ///////////////////////////////////////// INDICE DE ESPECIES /////////////////////////////////////////

    public DataTable getSpecies(int editionId)
    {
        StringBuilder sb = new StringBuilder();

        //PEV COLOMBIA
        sb.Append("\n SELECT DISTINCT S.SPECIEID, S.SPECIENAME ");
        sb.Append("\n FROM PLM_VWPRODUCTSBYEDITION V ");
        sb.Append("\n INNER JOIN PRODUCTSPECIES PS ON V.PRODUCTID=PS.PRODUCTID ");
        sb.Append("\n INNER JOIN SPECIES S ON PS.SPECIEID = S.SPECIEID ");
        sb.Append("\n WHERE V.EDITIONID = " + editionId + "and v.TypeInEdition ='p' ");
        sb.Append("\n ORDER BY 2 ");

        ////PEV 31 MÉXICO
        //sb.Append("\n SELECT DISTINCT S.SPECIEID, S.SPECIENAME ");
        //sb.Append("\n  FROM PRODUCTS P ");
        //sb.Append("\n  INNER JOIN PRODUCTSPECIES PS ON P.PRODUCTID=PS.PRODUCTID ");
        //sb.Append("\n  INNER JOIN SPECIES S ON S.SPECIEID = PS.SPECIEID ");
        //sb.Append("\n  INNER JOIN PLM_VWProductsByEdition V ON V.PRODUCTID = P.PRODUCTID ");
        //sb.Append("\n  WHERE P.COUNTRYID=10 and v.TypeInEdition = 'P' ");
        //sb.Append("\n  ORDER BY 2  ");

        ////PEV32 MÉXICO
        //sb.Append("\n SELECT DISTINCT S.SPECIEID, S.SPECIENAME ");
        //sb.Append("\n FROM PRODUCTS P ");
        //sb.Append("\n INNER JOIN PRODUCTSPECIES PS ON P.PRODUCTID=PS.PRODUCTID ");
        //sb.Append("\n INNER JOIN SPECIES S ON S.SPECIEID = PS.SPECIEID ");
        //sb.Append("\n INNER JOIN PLM_VWProductsByEdition V ON V.PRODUCTID = P.PRODUCTID ");
        //sb.Append("\n WHERE P.COUNTRYID=10 and v.TypeInEdition = 'P' AND v.EditionId = " + editionId.ToString());
        //sb.Append("\n UNION ");
        //sb.Append("\n SELECT DISTINCT e.EQUIPMENTID, e.EQUIPMENTNAME ");
        //sb.Append("\n FROM PRODUCTS pp ");
        //sb.Append("\n INNER JOIN ProductEquipment pe On pp.ProductId = pe.ProductId ");
        //sb.Append("\n INNER JOIN Equipment e On pe.EquipmentId = e.EquipmentId ");
        //sb.Append("\n INNER JOIN plm_vwProductsByEdition t On pp.ProductId = t.ProductId ");
        //sb.Append("\n Where pp.CountryId = 12 and t.TypeInEdition = 'P' and t.EditionId = " + editionId.ToString());
        //sb.Append("\n Order By 2 ");


        DataSet ds = GetPEV.DEFInstanceDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());

        return ds.Tables[0];
    }
    public DataTable getProductsbySpecies(int edition, int specieId)
    {
        StringBuilder sb = new StringBuilder();

        //PEV COLOMBIA
        //sb.Append("\n SELECT DISTINCT V.PRODUCTNAME, ");
        //sb.Append("\n dbo.plm_dfGetPharmaFormsByPagedProduct(V.PRODUCTID, V.EDITIONID, V.DIVISIONID) AS PHARMAFORM, ");
        //sb.Append("\n V.DIVISIONSHORTNAME as Division ");
        //sb.Append("\n FROM PLM_VWPRODUCTSBYEDITION V ");
        //sb.Append("\n INNER JOIN PRODUCTSPECIES PS ON V.PRODUCTID = PS.PRODUCTID ");
        //sb.Append("\n WHERE V.EDITIONID = " + edition + "   AND PS.SPECIEID = " + specieId);
        //sb.Append("\n ORDER BY 1");

        //PEV 31 MÉXICO
        sb.Append("\n SELECT DISTINCT V.PRODUCTID,V.PRODUCTNAME, ");
        sb.Append("\n dbo.plm_dfGetActiveSubsByProduct(V.PRODUCTID) AS PRODUCTDESCRIPTION, ");
        sb.Append("\n dbo.plm_dfGetPharmaFormsByDiffPagedProduct(V.PRODUCTID, V.EDITIONID, V.PAGE) AS PHARMAFORM, ");
        sb.Append("\n V.DIVISIONSHORTNAME AS DIVISION,V.PAGE ");
        sb.Append("\n FROM dbo.plm_vwProductsByEdition V ");
        sb.Append("\n INNER JOIN PRODUCTSPECIES PS ON V.PRODUCTID= PS.PRODUCTID ");
        sb.Append("\n WHERE EDITIONID =" + edition.ToString() + " AND PS.SPECIEID=" + specieId + " AND V.TypeInEdition='P' ");
        sb.Append("\n ORDER BY 2 ");

        //PEV 32 MÉXICO
        //sb.Append("\n SELECT DISTINCT V.PRODUCTID,V.PRODUCTNAME, ");
        //sb.Append("\n dbo.plm_dfGetActiveSubsByProduct(V.PRODUCTID) AS PRODUCTDESCRIPTION, ");
        //sb.Append("\n dbo.plm_dfGetPharmaFormsByDiffPagedProduct(V.PRODUCTID, V.EDITIONID, V.PAGE) AS PHARMAFORM, ");
        //sb.Append("\n V.DIVISIONSHORTNAME AS DIVISION,V.PAGE ");
        //sb.Append("\n FROM dbo.plm_vwProductsByEdition V ");
        //sb.Append("\n INNER JOIN PRODUCTSPECIES PS ON V.PRODUCTID= PS.PRODUCTID ");
        //sb.Append("\n WHERE EDITIONID = " + edition.ToString() + " AND PS.SPECIEID = " + specieId.ToString() + " AND V.TypeInEdition='P' ");
        //sb.Append("\n UNION ");
        //sb.Append("\n SELECT DISTINCT V.PRODUCTID,V.PRODUCTNAME, ");
        //sb.Append("\n dbo.plm_dfGetActiveSubsByProduct(V.PRODUCTID) AS PRODUCTDESCRIPTION, ");
        //sb.Append("\n dbo.plm_dfGetPharmaFormsByDiffPagedProduct(V.PRODUCTID, V.EDITIONID, V.PAGE) AS PHARMAFORM, ");
        //sb.Append("\n V.DIVISIONSHORTNAME AS DIVISION,V.PAGE ");
        //sb.Append("\n FROM dbo.plm_vwProductsByEdition V ");
        //sb.Append("\n INNER JOIN ProductEquipment pe ON V.PRODUCTID= pe.PRODUCTID ");
        //sb.Append("\n WHERE EDITIONID = " + edition.ToString() + " AND PE.EquipmentId = " + specieId.ToString() + " AND V.TypeInEdition='P' ");
        //sb.Append("\n ORDER BY 2 ");

        DataSet ds = GetPEV.DEFInstanceDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());

        return ds.Tables[0];
    }

    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


    ///////////////////////////////////////// INDICE DE ATC /////////////////////////////////////////

    public void deleteTheraTmp(string tt)
    {
        StringBuilder sb = new StringBuilder();

        //ATCNut

        GetPEV.DEFInstanceDatabase.ExecuteNonQuery(CommandType.Text, "IF OBJECT_ID('dbo." + tt + "', 'U') IS NOT NULL DROP TABLE dbo." + tt + "");

 

      //  DataSet ds = GetPEV.DEFInstanceDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());
    }


    public void generateTheraTmp(int editionId, string tt, string typet)
    {
        StringBuilder sb = new StringBuilder();

        //ATCNut

        GetPEV.DEFInstanceDatabase.ExecuteNonQuery(CommandType.Text, "IF OBJECT_ID('dbo." + tt + "', 'U') IS NOT NULL DROP TABLE dbo." + tt + "");

        if (typet == "")
        {
            sb.Append(" Select Distinct t.* into #tmpthera from plm_vwProductsByEdition v");
            sb.Append("\n inner join ProductTherapeuticUses pt on v.ProductId = pt.ProductId");
            sb.Append("\n inner join TherapeuticUses t on pt.TherapeuticId = t.TherapeuticId");
            sb.Append("\n where  v.EditionId = " + editionId.ToString());
        }
        else if (typet == "ATCNut")
        {
            sb.Append(" Select Distinct t.* into #tmpthera from plm_vwProductsByEdition v");
            sb.Append("\n inner join ProductTherapeuticUses pt on v.ProductId = pt.ProductId");
            sb.Append("\n inner join TherapeuticUses t on pt.TherapeuticId = t.TherapeuticId");
            sb.Append("\n where categoryId = 102 and v.EditionId = " + editionId.ToString());
        }
        else if (typet == "ATCC")
        {
            sb.Append(" Select Distinct t.* into #tmpthera from plm_vwProductsByEdition v");
            sb.Append("\n inner join ProductTherapeuticUses pt on v.ProductId = pt.ProductId");
            sb.Append("\n inner join TherapeuticUses t on pt.TherapeuticId = t.TherapeuticId");
            sb.Append("\n where categoryId not in (102) and v.EditionId = " + editionId.ToString());
        }
         
        sb.Append(" Select distinct tu.*  into #tmpthera1 from #tmpthera t ");
        sb.Append("\n inner join TherapeuticUses tu on t.ParentId = tu.TherapeuticId ");


        sb.Append("\n Select distinct tu.* into #tmpthera2  ");
        sb.Append("\n from #tmpthera1 t ");
        sb.Append("\n inner join TherapeuticUses tu on t.ParentId = tu.TherapeuticId ");


        sb.Append("\n Select distinct tu.* into #tmpthera3 ");
        sb.Append("\n from #tmpthera2 t ");
        sb.Append("\n inner join TherapeuticUses tu on t.ParentId = tu.TherapeuticId ");

        sb.Append("\n Select distinct tu.* into #tmpthera4 ");
        sb.Append("\n from #tmpthera3 t ");
        sb.Append("\n inner join TherapeuticUses tu on t.ParentId = tu.TherapeuticId ");

        sb.Append("\n Select distinct tu.* into #tmpthera5 ");
        sb.Append("\n from #tmpthera4 t ");
        sb.Append("\n inner join TherapeuticUses tu on t.ParentId = tu.TherapeuticId ");

        sb.Append(" Select * into " + tt + " from #tmpthera ");
        sb.Append("\n union ");
        sb.Append("\n Select * from #tmpthera1 ");
        sb.Append("\n union ");
        sb.Append("\n Select * from #tmpthera2 ");
        sb.Append("\n union ");
        sb.Append("\n Select * from #tmpthera3 ");
        sb.Append("\n union ");
        sb.Append("\n Select * from #tmpthera4 ");
        sb.Append("\n union ");
        sb.Append("\n Select * from #tmpthera5 ");

        sb.Append("\n drop table #tmpthera ");
        sb.Append("\n drop table #tmpthera1 ");
        sb.Append("\n drop table #tmpthera2 ");
        sb.Append("\n drop table #tmpthera3 ");
        sb.Append("\n drop table #tmpthera4 ");
        sb.Append("\n drop table #tmpthera5 ");


        DataSet ds = GetPEV.DEFInstanceDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());
    }

    public DataTable getAlphabet(string tt)
    {
        StringBuilder sb = new StringBuilder();

        sb.Append("SELECT THERAPEUTICID, THERAPEUTICNAME, PARENTID ");
        sb.Append("FROM " + tt + " ");
        sb.Append("WHERE PARENTID IS NULL or ParentId = 0 ");
        sb.Append("ORDER BY 2");

        DataSet ds = GetPEV.DEFInstanceDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());

        return ds.Tables[0];
    }

    public DataTable getInformation(int therapeuticId, int editionId)
    {
        StringBuilder sb = new StringBuilder();

        sb.Append("\nSELECT	DISTINCT V.PRODUCTID, ");
        sb.Append("\nUPPER(V.PRODUCTNAME) AS PRODUCTNAME,(v.ProductDescription), ");
        sb.Append("\ndbo.plm_dfGetActiveSubsByProduct(V.PRODUCTID) AS ACTIVESUBSTANCES, ");
        sb.Append("\ndbo.plm_dfGetPharmaFormsByPagedProduct(V.PRODUCTID, V.EDITIONID, V.DIVISIONID) AS PHARMAFORMS, ");
        sb.Append("\nUPPER(V.DIVISIONSHORTNAME) AS LABORATORYNAME, ");
        sb.Append("\nV.Page");
        sb.Append("\nFROM dbo.plm_vwProductsByEdition V ");
        sb.Append("\nINNER JOIN ProductTherapeuticUses PT ON V.PRODUCTID= PT.PRODUCTID ");
        sb.Append("\nINNER JOIN TherapeuticUses T ON PT.THERAPEUTICID = T.THERAPEUTICID ");
        sb.Append("\nWHERE V.EDITIONID= " + editionId.ToString() + " AND T.THERAPEUTICID= " + therapeuticId.ToString() + " ");
        sb.Append("\nAND TYPEINEDITION = 'P' ");
        sb.Append("\nORDER BY 2 ");

        DataSet ds = GetPEV.DEFInstanceDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());

        return ds.Tables[0];

    }

    public DataTable getTherapeuticsByParentId(int parentId, string tt)
    {

        //if(parentId == 108)
        //{

        //}
        StringBuilder sb = new StringBuilder();

        sb.Append("\nSELECT  DISTINCT  THERAPEUTICID,  THERAPEUTICNAME, PARENTID ");
        sb.Append("\nFROM " + tt + " T ");
        sb.Append("\nWHERE T.PARENTID =  " + parentId.ToString() + " ");
        sb.Append("\nORDER BY 2 ");

        DataSet ds = GetPEV.DEFInstanceDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());

        return ds.Tables[0];

    }
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


    ///////////////////////////////////////// INDICE DE PRODUCTOS NUEVOS /////////////////////////////////////////

    public DataTable getNewProducts(int edition)
    {
        StringBuilder sb = new StringBuilder();

        //PEV 31 MÉXICO

        sb.Append("\n SELECT DISTINCT V.PRODUCTID,V.PRODUCTNAME, ");
        sb.Append("\n dbo.plm_dfGetPharmaFormsByDiffPagedProduct(V.PRODUCTID, V.EDITIONID, V.PAGE) AS PHARMAFORM, ");
        sb.Append("\n dbo.plm_dfGetActiveSubsByProduct(V.PRODUCTID) AS PRODUCTSUBSTANCES, ");
        sb.Append("\n V.DIVISIONSHORTNAME AS DIVISION,V.PAGE ");
        sb.Append("\n FROM dbo.plm_vwProductsByEdition V ");
        //sb.Append("\n INNER JOIN PRODUCTSPECIES PS ON V.PRODUCTID= PS.PRODUCTID ");
        sb.Append("\n INNER JOIN NEWPRODUCTS NP ON NP.PRODUCTID = V.PRODUCTID    and v.EditionId = np.EditionId ");
        sb.Append("\n WHERE V.EDITIONID = " + edition.ToString() + " AND TypeInEdition = 'P'");
        sb.Append("\n ORDER BY 2 ");

        DataSet ds = GetPEV.DEFInstanceDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());

        return ds.Tables[0];
    }

    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


    ///////////////////////////////////////// INDICE DE VACUNACION /////////////////////////////////////////

    public DataTable getAlphabetVac(int edition)
    {
        StringBuilder sb = new StringBuilder();

        sb.Append("\n SELECT DISTINCT SUBSTRING(S.ACTIVESUBSTANCENAME COLLATE SQL_Latin1_General_CP1_CI_AI , 1,1), 1 ");
        sb.Append("\n FROM dbo.plm_vwProductsByEdition V ");
        sb.Append("\n INNER JOIN PRODUCTSUBSTANCES PS ON V.PRODUCTID = PS.PRODUCTID ");
        sb.Append("\n INNER JOIN ACTIVESUBSTANCES S ON PS.ACTIVESUBSTANCEID = S.ACTIVESUBSTANCEID ");
        sb.Append("\n WHERE V.EDITIONID = " + edition.ToString() + " AND V.TypeInEdition='P' And V.CategoryId = 23 ");
        sb.Append("\n ORDER BY 1");

        DataSet ds = GetPEV.DEFInstanceDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());

        return ds.Tables[0];
    }

    public DataTable getSubstancesVac(int edition, string letra)
    {
        StringBuilder sb = new StringBuilder();

        sb.Append("\n SELECT DISTINCT S.ACTIVESUBSTANCENAME AS ACTIVESUBSTANCE, ");
        sb.Append("\n S.ACTIVESUBSTANCEID AS ID ");
        sb.Append("\n FROM dbo.plm_vwProductsByEdition V ");
        sb.Append("\n INNER JOIN PRODUCTSUBSTANCES PS ON (V.PRODUCTID = PS.PRODUCTID) ");
        sb.Append("\n INNER JOIN ACTIVESUBSTANCES S ON PS.ACTIVESUBSTANCEID = S.ACTIVESUBSTANCEID ");
        sb.Append("\n WHERE V.EDITIONID= " + edition.ToString() + " And V.CategoryId = 23 AND S.ACTIVESUBSTANCENAME COLLATE SQL_Latin1_General_CP1_CI_AI LIKE ('" + letra + "%') and TypeInEdition='P'");
        sb.Append("\nORDER BY 1");

        DataSet ds = GetPEV.DEFInstanceDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());

        return ds.Tables[0];
    }

    public DataTable getInforVac(int editionId, string activeSubsId)
    {
        StringBuilder sb = new StringBuilder();

        sb.Append("\n SELECT DISTINCT V.PRODUCTNAME,V.PRODUCTDESCRIPTION, ");
        sb.Append("\n dbo.plm_dfGetPharmaFormsByPagedProduct(V.PRODUCTID, V.EDITIONID, V.DIVISIONID) AS PHARMAFORM, ");
        sb.Append("\n V.DIVISIONSHORTNAME");
        sb.Append("\n FROM PLM_VWPRODUCTSBYEDITION V ");
        sb.Append("\n INNER JOIN PRODUCTSUBSTANCES PS ON V.PRODUCTID = PS.PRODUCTID  ");
        sb.Append("\n WHERE V.EDITIONID = " + editionId.ToString() + " AND V.CATEGORYID = 23 AND PS.[ACTIVESUBSTANCEID] = " + activeSubsId + " and V.TypeInEdition='P'");
        sb.Append("\n ORDER BY 1,4");

        DataSet ds = GetPEV.DEFInstanceDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());

        return ds.Tables[0];
    }

    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    public DataTable getAlphabetThera(int edition)
    {
        StringBuilder sb = new StringBuilder();

        sb.Append("");
        sb.Append("");
        sb.Append("");
        sb.Append("");

        DataSet ds = GetPEV.DEFInstanceDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());

        return ds.Tables[0];
    }

     

    public DataTable getPoductsCol(int editionId, string letra)
    {
        StringBuilder sb = new StringBuilder();

        sb.Append("\n SELECT	DISTINCT V.PRODUCTID,");
        sb.Append("\n UPPER(V.PRODUCTNAME) AS PRODUCTNAME,");
        sb.Append("\n dbo.plm_dfGetActiveSubsByProduct(V.PRODUCTID) AS SUBSTANCE,");
        sb.Append("\n dbo.plm_dfGetPharmaFormsByPagedProduct(V.PRODUCTID, V.EDITIONID,V.DIVISIONID) as PHARMAFORM, ");
        sb.Append("\n UPPER(V.DIVISIONSHORTNAME) AS DIVISIONNAME , v.CountryId");
        sb.Append("\n FROM plm_vwProductsByEdition V ");
        sb.Append("\n WHERE V.EDITIONID=" + editionId + " AND V.PRODUCTNAME COLLATE SQL_Latin1_General_CP1_CI_AI LIKE ('" + letra + "%') and V.PRODUCTACTIVE = 1 ");
        sb.Append("\n ORDER BY 2 ");


        DataSet ds = GetPEV.DEFInstanceDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());

        return ds.Tables[0];
    } 

    public DataTable getProductsBySubstance(int edition, string Sub)
    {
        StringBuilder sb = new StringBuilder();

        sb.Append("");
        sb.Append("");
        sb.Append("");
        sb.Append("");

        DataSet ds = GetPEV.DEFInstanceDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());

        return ds.Tables[0];
    }
    
 

    public static readonly GetPEV Instance = new GetPEV();
}
