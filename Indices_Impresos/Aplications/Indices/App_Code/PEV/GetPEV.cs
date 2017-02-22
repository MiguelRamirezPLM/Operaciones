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
    public DataTable getAlphabet(int editionId)
    {
        StringBuilder sb = new StringBuilder();

        sb.Append("SELECT DISTINCT SUBSTRING(PRODUCTNAME,1,1) ");
        sb.Append("FROM plm_vwProductsByEdition ");
        sb.Append("WHERE EDITIONID = " + editionId + " ");
        sb.Append("ORDER BY 1");

        DataSet ds =GetPEV.DEFInstanceDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());

        return ds.Tables[0];

    }
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
    public DataTable getAlphabetBySub(int edition)
    {
        StringBuilder sb = new StringBuilder();


        sb.Append("\n SELECT DISTINCT SUBSTRING(S.ACTIVESUBSTANCENAME, 1,1), 1 ");
        sb.Append("\n FROM dbo.plm_vwProductsByEdition V ");
        sb.Append("\n INNER JOIN PRODUCTSUBSTANCES PS ON V.PRODUCTID = PS.PRODUCTID ");
        sb.Append("\n INNER JOIN ACTIVESUBSTANCES S ON PS.ACTIVESUBSTANCEID = S.ACTIVESUBSTANCEID ");
        sb.Append("\n WHERE V.EDITIONID = " + edition.ToString() + " AND V.TypeInEdition='P' ");
        sb.Append("\n ORDER BY 1");

        DataSet ds = GetPEV.DEFInstanceDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());

        return ds.Tables[0];
    }
    public DataTable getAlphabetVac(int edition)
    {
        StringBuilder sb = new StringBuilder();

        sb.Append("\n SELECT DISTINCT SUBSTRING(S.ACTIVESUBSTANCENAME, 1,1), 1 ");
        sb.Append("\n FROM dbo.plm_vwProductsByEdition V ");
        sb.Append("\n INNER JOIN PRODUCTSUBSTANCES PS ON V.PRODUCTID = PS.PRODUCTID ");
        sb.Append("\n INNER JOIN ACTIVESUBSTANCES S ON PS.ACTIVESUBSTANCEID = S.ACTIVESUBSTANCEID ");
        sb.Append("\n WHERE V.EDITIONID = " + edition.ToString() + " AND V.TypeInEdition='P' And V.CategoryId = 23 ");
        sb.Append("\n ORDER BY 1");

        DataSet ds = GetPEV.DEFInstanceDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());

        return ds.Tables[0];
    }

    public DataTable getPoducts(int editionId, string letra)
    {
        StringBuilder sb = new StringBuilder();

        sb.Append("\nSELECT	DISTINCT V.PRODUCTID,");
        sb.Append("\nUPPER(V.PRODUCTNAME) AS PRODUCTNAME,");
        sb.Append("\nV.ProductDescription, ");
        sb.Append("\nCASE WHEN TYPEINEDITION = 'P' ");
	    sb.Append("\nTHEN dbo.plm_dfGetPharmaFormsByDiffPagedProduct(V.PRODUCTID, V.EDITIONID,V.PAGE) ");
        sb.Append("\nELSE dbo.[plm_dfGetPharmaFormsByDiffPageMentionated](V.PRODUCTID, V.EDITIONID)END as PHARMAFORM, ");
        //sb.Append("\ndbo.plm_dfGetActiveSubsByProduct(V.PRODUCTID) AS SUBSTANCE,");
        //sb.Append("\ndbo.plm_dfGetPharmaFormsByDiffPagedProduct(V.PRODUCTID, V.EDITIONID,V.PAGE) as PHARMAFORM, ");
        sb.Append("\nUPPER(V.DIVISIONSHORTNAME) AS DIVISION,");
        sb.Append("\nv.PAGE");
        sb.Append("\nFROM plm_vwProductsByEdition V ");
        sb.Append("\nWHERE V.EDITIONID=" + editionId + " AND V.PRODUCTNAME COLLATE SQL_Latin1_General_CP1_CI_AI LIKE ('" + letra + "%') And V.PRODUCTACTIVE = 1 ");
        sb.Append("\nORDER BY 2 ");

        //sb.Append("\nSELECT DISTINCT ");
        //sb.Append("\n TYPEINEDITION, ");
        //sb.Append("\n PRODUCTNAME, ");
        //sb.Append("\n PRODUCTDESCRIPTION, ");
        //sb.Append("\n dbo.plm_dfGetPharmaFormsByDiffPagedProduct(PRODUCTID, EDITIONID, PAGE) as PHARMAFORM, ");
        //sb.Append("\n DIVISIONNAME, ");
        //sb.Append("\n PAGE ");
        //sb.Append("\n FROM dbo.plm_vwProductsByEdition");
        //sb.Append("\n WHERE EDITIONID = " + editionId.ToString() + " ");
        //sb.Append("\n AND PRODUCTNAME COLLATE SQL_Latin1_General_CP1_CI_AI LIKE ('" + letra + "%') ");
        //sb.Append("\n AND PRODUCTACTIVE = 1  ");
        //sb.Append("\n ORDER BY PRODUCTNAME ");

        DataSet ds = GetPEV.DEFInstanceDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());

        return ds.Tables[0];
    }
    public DataTable getPoductsCol(int editionId, string letra)
    {
        StringBuilder sb = new StringBuilder();

        sb.Append("\nSELECT	DISTINCT V.PRODUCTID,");
        sb.Append("\nUPPER(V.PRODUCTNAME) AS PRODUCTNAME,");
        sb.Append("\ndbo.plm_dfGetActiveSubsByProduct(V.PRODUCTID) AS SUBSTANCE,");
        sb.Append("\ndbo.plm_dfGetPharmaFormsByPagedProduct(V.PRODUCTID, V.EDITIONID,V.DIVISIONID) as PHARMAFORM, ");
        sb.Append("\nUPPER(V.DIVISIONSHORTNAME) AS DIVISIONNAME ");
        sb.Append("\nFROM plm_vwProductsByEdition V ");
        sb.Append("\nWHERE V.EDITIONID=" + editionId + " AND V.PRODUCTNAME COLLATE SQL_Latin1_General_CP1_CI_AI LIKE ('" + letra + "%') And V.PRODUCTACTIVE = 1 ");
        sb.Append("\nORDER BY 2 ");
               

        DataSet ds = GetPEV.DEFInstanceDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());

        return ds.Tables[0];
    }
    public DataTable getSpecies(int editionId)
    {
        StringBuilder sb = new StringBuilder();

        //PEV COLOMBIA
        //sb.Append("\n SELECT DISTINCT S.SPECIEID, S.SPECIENAME ");
        //sb.Append("\n FROM PLM_VWPRODUCTSBYEDITION V ");
        //sb.Append("\n INNER JOIN PRODUCTSPECIES PS ON V.PRODUCTID=PS.PRODUCTID ");
        //sb.Append("\n INNER JOIN SPECIES S ON PS.SPECIEID = S.SPECIEID ");
        //sb.Append("\n WHERE V.EDITIONID = " + editionId + " AND V.CATEGORYID = 102");
        //sb.Append("\n ORDER BY 2 ");

        //PEV 31 MÉXICO
        //sb.Append("\n SELECT DISTINCT S.SPECIEID, S.SPECIENAME ");
        //sb.Append("\n  FROM PRODUCTS P ");
        //sb.Append("\n  INNER JOIN PRODUCTSPECIES PS ON P.PRODUCTID=PS.PRODUCTID ");
        //sb.Append("\n  INNER JOIN SPECIES S ON S.SPECIEID = PS.SPECIEID ");
        //sb.Append("\n  INNER JOIN PLM_VWProductsByEdition V ON V.PRODUCTID = P.PRODUCTID ");
        //sb.Append("\n  WHERE P.COUNTRYID=10 and v.TypeInEdition = 'P' ");
        //sb.Append("\n  ORDER BY 2  ");

        //PEV32 MÉXICO
        sb.Append("\n SELECT DISTINCT S.SPECIEID, S.SPECIENAME ");
        sb.Append("\n FROM PRODUCTS P ");
        sb.Append("\n INNER JOIN PRODUCTSPECIES PS ON P.PRODUCTID=PS.PRODUCTID ");
        sb.Append("\n INNER JOIN SPECIES S ON S.SPECIEID = PS.SPECIEID ");
        sb.Append("\n INNER JOIN PLM_VWProductsByEdition V ON V.PRODUCTID = P.PRODUCTID ");
        sb.Append("\n WHERE P.COUNTRYID=10 and v.TypeInEdition = 'P' AND v.EditionId = " + editionId.ToString() );
        sb.Append("\n UNION ");
        sb.Append("\n SELECT DISTINCT e.EQUIPMENTID, e.EQUIPMENTNAME ");
        sb.Append("\n FROM PRODUCTS pp ");
        sb.Append("\n INNER JOIN ProductEquipment pe On pp.ProductId = pe.ProductId ");
        sb.Append("\n INNER JOIN Equipment e On pe.EquipmentId = e.EquipmentId ");
        sb.Append("\n INNER JOIN plm_vwProductsByEdition t On pp.ProductId = t.ProductId ");
        sb.Append("\n Where pp.CountryId = 10 and t.TypeInEdition = 'P' and t.EditionId = " + editionId.ToString() );
        sb.Append("\n Order By 2 ");


        DataSet ds = GetPEV.DEFInstanceDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());

        return ds.Tables[0];
    }
    public DataTable getProductsbySpecies(int edition, int specieId)
    {
        StringBuilder sb = new StringBuilder();

       //PEV COLOMBIA
        //sb.Append("\nSELECT DISTINCT V.PRODUCTNAME, ");
        //sb.Append("\ndbo.plm_dfGetPharmaFormsByPagedProduct(V.PRODUCTID, V.EDITIONID, V.DIVISIONID) AS PHARMAFORM, ");
        //sb.Append("\nV.DIVISIONSHORTNAME ");
        //sb.Append("\nFROM PLM_VWPRODUCTSBYEDITION V ");
        //sb.Append("\nINNER JOIN PRODUCTSPECIES PS ON V.PRODUCTID = PS.PRODUCTID ");
        //sb.Append("\nWHERE V.EDITIONID = " + edition + " AND V.CATEGORYID = 102 AND PS.SPECIEID = " + specieId);
        //sb.Append("\nORDER BY 1");

        //PEV 31 MÉXICO
        //sb.Append("\n SELECT DISTINCT V.PRODUCTID,V.PRODUCTNAME, ");
        //sb.Append("\n dbo.plm_dfGetActiveSubsByProduct(V.PRODUCTID) AS PRODUCTDESCRIPTION, ");
        //sb.Append("\n dbo.plm_dfGetPharmaFormsByDiffPagedProduct(V.PRODUCTID, V.EDITIONID, V.PAGE) AS PHARMAFORM, ");
        //sb.Append("\n V.DIVISIONSHORTNAME AS DIVISION,V.PAGE ");
        //sb.Append("\n FROM dbo.plm_vwProductsByEdition V ");
        //sb.Append("\n INNER JOIN PRODUCTSPECIES PS ON V.PRODUCTID= PS.PRODUCTID ");
        //sb.Append("\n WHERE EDITIONID =" + edition.ToString() + " AND PS.SPECIEID=" + specieId + " AND V.TypeInEdition='P' ");
        //sb.Append("\n ORDER BY 2 ");

        //PEV 32 MÉXICO
        sb.Append("\n SELECT DISTINCT V.PRODUCTID,V.PRODUCTNAME, ");
        sb.Append("\n dbo.plm_dfGetActiveSubsByProduct(V.PRODUCTID) AS PRODUCTDESCRIPTION, ");
        sb.Append("\n dbo.plm_dfGetPharmaFormsByDiffPagedProduct(V.PRODUCTID, V.EDITIONID, V.PAGE) AS PHARMAFORM, ");
        sb.Append("\n V.DIVISIONSHORTNAME AS DIVISION,V.PAGE ");
        sb.Append("\n FROM dbo.plm_vwProductsByEdition V ");
        sb.Append("\n INNER JOIN PRODUCTSPECIES PS ON V.PRODUCTID= PS.PRODUCTID ");
        sb.Append("\n WHERE EDITIONID = " + edition.ToString() + " AND PS.SPECIEID = " + specieId.ToString() + " AND V.TypeInEdition='P' ");
        sb.Append("\n UNION ");
        sb.Append("\n SELECT DISTINCT V.PRODUCTID,V.PRODUCTNAME, ");
        sb.Append("\n dbo.plm_dfGetActiveSubsByProduct(V.PRODUCTID) AS PRODUCTDESCRIPTION, ");
        sb.Append("\n dbo.plm_dfGetPharmaFormsByDiffPagedProduct(V.PRODUCTID, V.EDITIONID, V.PAGE) AS PHARMAFORM, ");
        sb.Append("\n V.DIVISIONSHORTNAME AS DIVISION,V.PAGE ");
        sb.Append("\n FROM dbo.plm_vwProductsByEdition V ");
        sb.Append("\n INNER JOIN ProductEquipment pe ON V.PRODUCTID= pe.PRODUCTID ");
        sb.Append("\n WHERE EDITIONID = " + edition.ToString() + " AND PE.EquipmentId = " + specieId.ToString() + " AND V.TypeInEdition='P' ");
        sb.Append("\n ORDER BY 2 ");

        DataSet ds = GetPEV.DEFInstanceDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());

        return ds.Tables[0];
    }

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
		sb.Append("\n INNER JOIN NEWPRODUCTS NP ON NP.PRODUCTID = V.PRODUCTID  ");
        sb.Append("\n WHERE V.EDITIONID = " + edition.ToString() + " AND TypeInEdition = 'P'");
        sb.Append("\n ORDER BY 2 ");

        DataSet ds = GetPEV.DEFInstanceDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());

        return ds.Tables[0];
    }

    public DataTable getSubstances(int edition, string letra)
    {
        StringBuilder sb = new StringBuilder();

        sb.Append("\nSELECT DISTINCT S.ACTIVESUBSTANCENAME AS ACTIVESUBSTANCE, ");
		sb.Append("\nS.ACTIVESUBSTANCEID AS ID ");
        sb.Append("\nFROM dbo.plm_vwProductsByEdition V ");
        sb.Append("\nINNER JOIN PRODUCTSUBSTANCES PS ON (V.PRODUCTID = PS.PRODUCTID) ");
        sb.Append("\nINNER JOIN ACTIVESUBSTANCES S ON PS.ACTIVESUBSTANCEID = S.ACTIVESUBSTANCEID ");
        sb.Append("\nWHERE V.EDITIONID= " + edition.ToString() + " AND S.ACTIVESUBSTANCENAME COLLATE SQL_Latin1_General_CP1_CI_AI LIKE ('" + letra + "%') and TypeInEdition='P'");
        sb.Append("\nORDER BY 1");
        
        DataSet ds = GetPEV.DEFInstanceDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());

        return ds.Tables[0];
    }
    public DataTable getSubstancesVac(int edition, string letra)
    {
        StringBuilder sb = new StringBuilder();

        sb.Append("\nSELECT DISTINCT S.ACTIVESUBSTANCENAME AS ACTIVESUBSTANCE, ");
        sb.Append("\nS.ACTIVESUBSTANCEID AS ID ");
        sb.Append("\nFROM dbo.plm_vwProductsByEdition V ");
        sb.Append("\nINNER JOIN PRODUCTSUBSTANCES PS ON (V.PRODUCTID = PS.PRODUCTID) ");
        sb.Append("\nINNER JOIN ACTIVESUBSTANCES S ON PS.ACTIVESUBSTANCEID = S.ACTIVESUBSTANCEID ");
        sb.Append("\nWHERE V.EDITIONID= " + edition.ToString() + " And V.CategoryId = 23 AND S.ACTIVESUBSTANCENAME COLLATE SQL_Latin1_General_CP1_CI_AI LIKE ('" + letra + "%') and TypeInEdition='P'");
        sb.Append("\nORDER BY 1");

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
    public DataTable getInfor( int editionId,string activeSubsId)
    {
        StringBuilder sb = new StringBuilder();

        //sb.Append("\n SELECT	DISTINCT V.PRODUCTID, ");
        //sb.Append("\nUPPER(V.PRODUCTNAME) AS PRODUCTNAME, ");
        //sb.Append("\n'' AS PRODUCTDESCRIPTION, ");
        //sb.Append("\ndbo.plm_dfGetActiveSubstancesByProduct(V.PRODUCTID,PS.ACTIVESUBSTANCEID) AS ACTIVESUBSTANCES, ");
        //sb.Append("\ndbo.plm_dfGetPharmaFormsByDiffPagedProduct(V.PRODUCTID, V.EDITIONID, V.PAGE) AS PHARMAFORM, ");
        //sb.Append("\nUPPER(V.DIVISIONSHORTNAME) AS Division, '' AS PAGE ");
        //sb.Append("\nFROM dbo.plm_vwProductsByEdition V ");
        //sb.Append("\nINNER JOIN PRODUCTSUBSTANCES PS ON (v.PRODUCTID = PS.PRODUCTID)  ");
        //sb.Append("\nINNER JOIN ACTIVESUBSTANCES ASUB ON (PS.ACTIVESUBSTANCEID = ASUB.ACTIVESUBSTANCEID) ");
        //sb.Append("\nWHERE V.EDITIONID = " + editionId.ToString() + " AND ASUB.[ACTIVESUBSTANCEID] = " + activeSubsId + " ");
        //sb.Append("\nORDER BY 2,4");

        //PEV 31        
        sb.Append("\n SELECT DISTINCT V.PRODUCTID, ");
		sb.Append("\nUPPER(V.PRODUCTNAME) AS PRODUCTNAME, ");
		sb.Append("\ndbo.plm_dfGetActiveSubstancesByProduct(V.PRODUCTID,PS.ACTIVESUBSTANCEID) AS ACTIVESUBSTANCES, ");
		sb.Append("\ndbo.plm_dfGetPharmaFormsByDiffPagedProduct(V.PRODUCTID, V.EDITIONID, V.PAGE) AS PHARMAFORM, ");
		sb.Append("\nUPPER(V.DIVISIONSHORTNAME) AS Division, V.Page AS PAGE ");
        sb.Append("\nFROM dbo.plm_vwProductsByEdition V ");
        sb.Append("\nINNER JOIN PRODUCTSUBSTANCES PS ON (v.PRODUCTID = PS.PRODUCTID)  ");
        sb.Append("\nINNER JOIN ACTIVESUBSTANCES ASUB ON (PS.ACTIVESUBSTANCEID = ASUB.ACTIVESUBSTANCEID) ");
        sb.Append("\nWHERE V.EDITIONID = " + editionId.ToString() + " AND ASUB.[ACTIVESUBSTANCEID] = " + activeSubsId + " and V.TypeInEdition='P'");
        sb.Append("\nORDER BY 2,4");

        DataSet ds = GetPEV.DEFInstanceDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());

        return ds.Tables[0];
    }
    public DataTable getInforVac(int editionId, string activeSubsId)
    {
        StringBuilder sb = new StringBuilder();

        sb.Append("\n SELECT DISTINCT V.PRODUCTNAME,V.PRODUCTDESCRIPTION, ");
        sb.Append("\ndbo.plm_dfGetPharmaFormsByPagedProduct(V.PRODUCTID, V.EDITIONID, V.DIVISIONID) AS PHARMAFORM, ");
        sb.Append("\n V.DIVISIONSHORTNAME");
        sb.Append("\nFROM PLM_VWPRODUCTSBYEDITION V ");
        sb.Append("\nINNER JOIN PRODUCTSUBSTANCES PS ON V.PRODUCTID = PS.PRODUCTID  ");
        sb.Append("\nWHERE V.EDITIONID = " + editionId.ToString() + " AND V.CATEGORYID = 23 AND PS.[ACTIVESUBSTANCEID] = " + activeSubsId + " and V.TypeInEdition='P'");
        sb.Append("\nORDER BY 2,4");

        DataSet ds = GetPEV.DEFInstanceDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());

        return ds.Tables[0];
    }

    // THERAPEUTICS

    public DataTable getTherapeuticsByParentId(int parentId)
    {
        StringBuilder sb = new StringBuilder();

        sb.Append("\nSELECT  DISTINCT  THERAPEUTICID, DESCRIPTION As THERAPEUTICNAME, PARENTID ");
        sb.Append("\nFROM TMPTHERA T ");
        sb.Append("\nWHERE T.PARENTID =  " + parentId.ToString()+ " ");
        sb.Append("\nORDER BY 2 ");

        DataSet ds = GetPEV.DEFInstanceDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());

        return ds.Tables[0];

    }

    public DataTable getInformation(int therapeuticId, int editionId)
    {
        StringBuilder sb = new StringBuilder();
        
        sb.Append("\nSELECT	DISTINCT V.PRODUCTID, ");
	    sb.Append("\nUPPER(V.PRODUCTNAME) AS PRODUCTNAME, ");
		sb.Append("\ndbo.plm_dfGetActiveSubsByProduct(V.PRODUCTID) AS ACTIVESUBSTANCES, ");
		sb.Append("\ndbo.plm_dfGetPharmaFormsByPagedProduct(V.PRODUCTID, V.EDITIONID, V.DIVISIONID) AS PHARMAFORMS, ");
		sb.Append("\nUPPER(V.DIVISIONSHORTNAME) AS LABORATORYNAME, ");
        sb.Append("\nV.Page");
        sb.Append("\nFROM dbo.plm_vwProductsByEdition V ");
        sb.Append("\nINNER JOIN PRODUCTTHERAPEUTICUSES PT ON V.PRODUCTID= PT.PRODUCTID ");
        sb.Append("\nINNER JOIN THERAPEUTICUSES T ON PT.THERAPEUTICID = T.THERAPEUTICID ");
        sb.Append("\nWHERE V.EDITIONID= " + editionId.ToString() + " AND T.THERAPEUTICID= " + therapeuticId.ToString() + " ");
        sb.Append("\nAND TYPEINEDITION = 'P'");
        sb.Append("\nORDER BY 2 ");



        DataSet ds = GetPEV.DEFInstanceDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());

        return ds.Tables[0];

    }

    public DataTable getAlphabet()
    {
        StringBuilder sb = new StringBuilder();

        sb.Append("SELECT THERAPEUTICID, [DESCRIPTION] As THERAPEUTICNAME, PARENTID ");
        sb.Append("FROM TMPTHERA ");
        sb.Append("WHERE PARENTID IS NULL ");
        sb.Append("ORDER BY 2");

        DataSet ds = GetPEV.DEFInstanceDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());

        return ds.Tables[0];
    }


    public static readonly GetPEV Instance = new GetPEV();
}
