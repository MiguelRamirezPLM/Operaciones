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

public class IndiceGeneral : DEAQDataAccessAdapter<object>
{
    #region Constructors

    public IndiceGeneral() { }

    #endregion

    #region Public Methods

    public DataTable getAlphabet(int editionId)
    {
        StringBuilder sb = new StringBuilder();
 
        sb.Append("\nSelect Distinct Substring(ProductName COLLATE SQL_Latin1_General_CP1_CI_AI ,1,1), CountryId ");
        sb.Append("\nFrom plm_vwProductsByEdition ");
        sb.Append("\nWhere EditionId = " + editionId +  "");
        sb.Append("\nOrder By 1 ");

        DataSet ds = IndiceGeneral.DEAQInstanceDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());

        return ds.Tables[0];

    }

    public DataTable getProducts(string letter, int editionId)
    {
        StringBuilder sb = new StringBuilder();
        
     

        sb.Append("\n SELECT DISTINCT p.ProductName AS Nombre, p.Register AS Registro,");
        sb.Append("\n dbo.plm_dfGetAgrochemicalUsesByProduct(p.ProductId) AS Usos,");
        sb.Append("\n dbo.plm_dfGetPharmaFormsByDiffProduct(p.ProductId, p.EditionID) AS FFarmaceutica,");
        sb.Append("\n p.DivisionShortName as Laboratorio,");
        sb.Append("\n p.Page AS Pagina, p.ProductId ");
        sb.Append("\n FROM plm_vwProductsByEdition p ");
        sb.Append("\n WHERE p.EditionId = " + editionId + "  AND p.ProductName COLLATE SQL_LATIN1_GENERAL_CP1_CI_AI LIKE '" + letter + "%' and p.TypeInEdition = 'P' ");
        sb.Append("\n UNION ");
        sb.Append("\n SELECT DISTINCT p.ProductName AS Nombre, p.Register AS Registro, ");
        sb.Append("\n dbo.plm_dfGetAgrochemicalUsesByProduct(p.productID) AS Usos, ");
        sb.Append("\n dbo.plm_dfGetPharmaFormsByProduct(p.ProductID, p.EditionID) AS FFarmaceutica,");
        sb.Append("\n p.DivisionShortName AS Laboratorio, '' AS Pagina, p.ProductId ");
        sb.Append("\n FROM plm_vwProductsByEdition p ");
        sb.Append("\n WHERE p.EditionId = " + editionId + "  AND p.ProductName COLLATE SQL_LATIN1_GENERAL_CP1_CI_AI LIKE '" + letter + "%' and p.TypeInEdition = 'M' ");
        sb.Append("\n ORDER BY 1 ");                            

        DataSet ds = IndiceGeneral.DEAQInstanceDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());

        return ds.Tables[0];

    }

    public DataTable getProductsM(string letter, int editionId)
    {
        StringBuilder sb = new StringBuilder();



        sb.Append("\n SELECT DISTINCT p.ProductName AS Nombre, p.Register AS Registro,");
        sb.Append("\n dbo.plm_dfGetAgrochemicalUsesByProduct(p.ProductId) AS Usos,");
        sb.Append("\n dbo.plm_dfGetPharmaFormsByDiffPagedProduct(p.ProductId, p.EditionID, p.Page) AS FFarmaceutica,");
        sb.Append("\n p.DivisionShortName as Laboratorio,");
        sb.Append("\n p.Page AS Pagina, p.ProductId ");
        sb.Append("\n FROM plm_vwProductsByEdition p ");
        sb.Append("\n WHERE p.EditionId = " + editionId + "  AND p.ProductName COLLATE SQL_LATIN1_GENERAL_CP1_CI_AI LIKE '" + letter + "%' and p.TypeInEdition = 'P' and p.CategoryId != 2");
        sb.Append("\n UNION ");
        sb.Append("\n SELECT DISTINCT p.ProductName AS Nombre, p.Register AS Registro, ");
        sb.Append("\n dbo.plm_dfGetAgrochemicalUsesByProduct(p.productID) AS Usos, ");
        sb.Append("\n dbo.plm_dfGetPharmaFormsByProduct(p.ProductID, p.EditionID) AS FFarmaceutica,");
        sb.Append("\n p.DivisionShortName AS Laboratorio, '' AS Pagina, p.ProductId ");
        sb.Append("\n FROM plm_vwProductsByEdition p ");
        sb.Append("\n WHERE p.EditionId = " + editionId + "  AND p.ProductName COLLATE SQL_LATIN1_GENERAL_CP1_CI_AI LIKE '" + letter + "%' and p.TypeInEdition = 'M' and p.CategoryId != 2");
        sb.Append("\n ORDER BY 1 ");

        DataSet ds = IndiceGeneral.DEAQInstanceDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());

        return ds.Tables[0];

    }


    ////////////////////////////////////////////////////////////// SEMILLAS //////////////////////////////////////////////////////////////

    public DataTable getAlphabetSeeds(int editionId)
    {
        StringBuilder sb = new StringBuilder();

        sb.Append("\nSelect Distinct Substring(ProductName COLLATE SQL_Latin1_General_CP1_CI_AI ,1,1), CountryId ");
        sb.Append("\nFrom plm_vwProductsByEdition ");
        sb.Append("\nWhere EditionId = " + editionId + " and CategoryId = 2 ");
        sb.Append("\nOrder By 1 ");

        DataSet ds = IndiceGeneral.DEAQInstanceDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());

        return ds.Tables[0];

    }

    public DataTable getProductsMSeeds(string letter, int editionId)
    {
        StringBuilder sb = new StringBuilder(); 
        sb.Append("\n SELECT DISTINCT p.ProductName AS Nombre, p.Register AS Registro,");
        sb.Append("\n dbo.plm_dfGetAgrochemicalUsesByProduct(p.ProductId) AS Usos,");
        sb.Append("\n dbo.plm_dfGetPharmaFormsByDiffPagedProduct(p.ProductId, p.EditionID, p.Page) AS FFarmaceutica,");
        sb.Append("\n p.DivisionShortName as Laboratorio,");
        sb.Append("\n p.Page AS Pagina, p.ProductId ");
        sb.Append("\n FROM plm_vwProductsByEdition p ");
        sb.Append("\n WHERE p.EditionId = " + editionId + "  AND p.ProductName COLLATE SQL_LATIN1_GENERAL_CP1_CI_AI LIKE '" + letter + "%' and p.TypeInEdition = 'P' and p.CategoryId = 2");
        sb.Append("\n UNION ");
        sb.Append("\n SELECT DISTINCT p.ProductName AS Nombre, p.Register AS Registro, ");
        sb.Append("\n dbo.plm_dfGetAgrochemicalUsesByProduct(p.productID) AS Usos, ");
        sb.Append("\n dbo.plm_dfGetPharmaFormsByProduct(p.ProductID, p.EditionID) AS FFarmaceutica,");
        sb.Append("\n p.DivisionShortName AS Laboratorio, '' AS Pagina, p.ProductId ");
        sb.Append("\n FROM plm_vwProductsByEdition p ");
        sb.Append("\n WHERE p.EditionId = " + editionId + "  AND p.ProductName COLLATE SQL_LATIN1_GENERAL_CP1_CI_AI LIKE '" + letter + "%' and p.TypeInEdition = 'M' and p.CategoryId = 2");
        sb.Append("\n ORDER BY 1 ");

        DataSet ds = IndiceGeneral.DEAQInstanceDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());

        return ds.Tables[0];

    }



    #endregion

    public static readonly IndiceGeneral Instance = new IndiceGeneral();

}
