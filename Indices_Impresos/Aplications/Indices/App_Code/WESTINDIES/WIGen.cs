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

public class WIGen : WIDataAccessAdapter<object>
{
    #region Constructors

    public WIGen()
    { }

    #endregion

    #region Public Methods

    public DataTable getAlphabet(int editionId)
    {
        StringBuilder sb = new StringBuilder();

        sb.Append("SELECT DISTINCT SUBSTRING(BRAND,1,1) ");
        sb.Append("FROM PLM_VWPRODUCTSBYEDITION ");
        sb.Append("WHERE EditionId = " + editionId + " AND ProductActive = 1 and SUBSTRING(BRAND,1,1) <> 'Á'");
        sb.Append("ORDER BY 1");

        DataSet ds = WIGen.DEF55InstanceDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());

        return ds.Tables[0];

    }

    public DataTable getProducts(int editionId, string letra)
    {
        StringBuilder sb = new StringBuilder();

        sb.Append("\nSelect Distinct P.ProductId,Upper(P.Brand)as Brand,P.ProductDescription,");
        sb.Append("\ndbo.plm_dfGetActiveSubsByProduct(P.ProductId) AS ACTIVESUBSTANCES, ");
        sb.Append("\ndbo.plm_dfGetPharmaFormsByProductC (P.PRODUCTID,P.EDITIONID,dbo.plm_dfGetCountriesByProduct (P.PRODUCTID,P.PHARMAFORMID),P.DIVISIONID,P.CATEGORYID) AS PHARMAFORMS, ");
        sb.Append("\nP.DIVISIONSHORTNAME AS LABORATORYNAME, dbo.plm_dfGetPagesByProduct(P.PRODUCTID,P.EDITIONID,P.DIVISIONID) AS PAGE, ");
        sb.Append("\ndbo.plm_dfGetCountriesByProduct (P.PRODUCTID,P.PHARMAFORMID) as Countries ");
        sb.Append("\nFROM PLM_VWPRODUCTSBYEDITION P ");
        sb.Append("\nWHERE P.PRODUCTACTIVE = 1 AND P.PHARMAACTIVE = 1 AND P.EDITIONID = " + editionId + " AND ");
               
        if (letra.Equals("N") || letra.Equals("Ñ"))
        {
            sb.Append("\nP.Brand LIKE ('" + letra + "%') ");
        }
        else
        {
            sb.Append("\nP.Brand COLLATE SQL_Latin1_General_CP1_CI_AI LIKE ('" + letra + "%')");

        }

        sb.Append(" ORDER BY 2");

        DataSet ds = WIGen.DEF55InstanceDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());

        return ds.Tables[0];

    }

    #endregion

    public static readonly WIGen Instance = new WIGen();

}
