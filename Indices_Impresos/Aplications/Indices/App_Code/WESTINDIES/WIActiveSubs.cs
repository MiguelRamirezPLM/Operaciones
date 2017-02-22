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


public class WIActiveSubs : WIDataAccessAdapter<object>
{
    #region Constructors

    public WIActiveSubs()
    { }

    #endregion

    #region Public Methods

    public DataTable getAlphabet(int editioId)
    {
        StringBuilder sb = new StringBuilder();

        sb.Append("SELECT DISTINCT SUBSTRING(ASUB.[ENGLISHDESCRIPTION],1,1) AS LETTER,1 ");
        sb.Append("FROM plm_vwProductsByEdition PP ");
        sb.Append("INNER JOIN PRODUCTSUBSTANCES PS ON (PP.PRODUCTID = PS.PRODUCTID) ");
        sb.Append("INNER JOIN ACTIVESUBSTANCES ASUB ON (PS.ACTIVESUBSTANCEID = ASUB.ACTIVESUBSTANCEID) ");
        sb.Append("WHERE PP.EDITIONID = " + editioId +" AND ASUB.ACTIVE = 1 AND ");
        sb.Append("SUBSTRING(ASUB.[ENGLISHDESCRIPTION],1,1) NOT IN ('Á','É','Ó') ");
        sb.Append("ORDER BY 1");

        DataSet ds = WIActiveSubs.DEF55InstanceDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());

        return ds.Tables[0];
    }

    public DataTable getSubstances(int editionId, string letra)
    {
        StringBuilder sb = new StringBuilder();

        sb.Append("SELECT DISTINCT ASUB.[EnglishDescription] AS ACTIVESUBSTANCE,ASUB.ACTIVESUBSTANCEID AS ID ");
        sb.Append("FROM plm_vwProductsByEdition PP ");
        sb.Append("INNER JOIN PRODUCTSUBSTANCES PS ON (PP.PRODUCTID = PS.PRODUCTID) ");
        sb.Append("INNER JOIN ACTIVESUBSTANCES ASUB ON (PS.ACTIVESUBSTANCEID = ASUB.ACTIVESUBSTANCEID) ");
        sb.Append("WHERE EDITIONID = " + editionId +" AND ASUB.ACTIVE = 1 AND ");
        sb.Append("ASUB.[ENGLISHDESCRIPTION] COLLATE SQL_Latin1_General_CP1_CI_AI LIKE ('" + letra + "%') ");


        DataSet ds = WIActiveSubs.DEF55InstanceDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());

        return ds.Tables[0];
    }

    public DataTable getProducts(int editionId, int activeSubstanceId)
    {
        StringBuilder sb = new StringBuilder();

        sb.Append("SELECT DISTINCT P.PRODUCTID,UPPER(P.BRAND) AS BRAND,P.[PRODUCTDESCRIPTION],dbo.plm_dfGetActiveSubstancesByProduct(P.ProductId,PS.ActiveSubstanceId) AS ACTIVESUBSTANCES,");
        sb.Append("dbo.plm_dfGetPharmaFormsByProductC (P.ProductId,P.EditionID,dbo.plm_dfGetCountriesByProduct (P.PRODUCTID,P.PHARMAFORMID),P.DivisionId,P.CategoryId) AS PHARMAFORMS,UPPER(P.DivisionSHORTNAME) AS LABORATORYNAME,dbo.plm_dfGetPagesByProduct(P.PRODUCTID,P.EDITIONID,P.DIVISIONID) AS PAGE, ");
        sb.Append("dbo.plm_dfGetCountriesByProduct (P.PRODUCTID,P.PHARMAFORMID) as Countries ");
        sb.Append("FROM plm_vwProductsByEdition P ");
        sb.Append("INNER JOIN PRODUCTSUBSTANCES PS ON(P.PRODUCTID = PS.PRODUCTID) ");
        sb.Append("WHERE P.EDITIONID = " + editionId + " AND PS.[ACTIVESUBSTANCEID] = " + activeSubstanceId);
        sb.Append(" ORDER BY 2,4");

        DataSet ds = WIActiveSubs.DEF55InstanceDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());

        return ds.Tables[0];
    }

    #endregion

    public static readonly WIActiveSubs Instance = new WIActiveSubs();
}
