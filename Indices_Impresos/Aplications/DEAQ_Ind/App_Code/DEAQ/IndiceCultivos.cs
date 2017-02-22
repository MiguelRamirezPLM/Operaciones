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

public class IndiceCultivos : DEAQDataAccessAdapter<object>
{
    #region Constructors

    public IndiceCultivos(){}

    #endregion

    #region Public Methods

    public DataTable getAlphabet(int editionId)
    {
        StringBuilder sb = new StringBuilder();

        sb.Append("SELECT DISTINCT Substring(CROPNAME COLLATE SQL_Latin1_General_CP1_CI_AI ,1,1) letter, pp.CountryId FROM Crops c ");
        sb.Append("\nINNER JOIN ProductCrops pc ON c.CropId = pc.CropId ");
        sb.Append("\nINNER JOIN plm_vwProductsByEdition pp On pc.ProductId = pp.ProductId ");
        sb.Append("\nWHERE pp.EditionId = " + editionId + " and pp.TYPEINEDITION = 'p' -- And pp.CategoryId   in (2) ");
        sb.Append("\nORDER BY 1 ");

        DataSet ds = IndiceCultivos.DEAQInstanceDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());

        return ds.Tables[0];

    }

    public DataTable getCultivo(string letter, int editionId)
    {
        StringBuilder sb = new StringBuilder();

        sb.Append("SELECT DISTINCT C.CROPID AS IDCul, C.CROPNAME AS Cultivo, PP.CountryId FROM CROPS C ");
        sb.Append("\nINNER JOIN PRODUCTCROPS PC ON C.CROPID = PC.CROPID ");
        sb.Append("\nINNER JOIN plm_vwProductsByEdition PP ON PC.PRODUCTID = PP.PRODUCTID ");
        sb.Append("\nWHERE PP.EDITIONID = " + editionId + " And pp.TYPEINEDITION = 'p' AND C.CROPNAME COLLATE SQL_Latin1_General_CP1_CI_AI LIKE '" + letter + "%' ");
        sb.Append("\n --And pp.CategoryId in (2) ");
        sb.Append("\n  ORDER BY 2 ");

        DataSet ds = IndiceCultivos.DEAQInstanceDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());

        return ds.Tables[0];
    }

    public DataTable getProducts(int cultId, int editionId)
    {
        StringBuilder sb = new StringBuilder();
   
        sb.Append("\n SELECT DISTINCT P.PRODUCTNAME AS Nombre, ");
        sb.Append("\n dbo.plm_dfGetActiveSubstancesByProduct(p.PRODUCTID,'') Subst, ");
        sb.Append("\n dbo.plm_dfGetTopAgrochemicalUsesByProduct(P.PRODUCTID) AS Usos, ");
        sb.Append("\n p.DivisionShortName AS Laboratorio, ");
        sb.Append("\n p.PAGE AS Pagina, p.ProductId ");
        sb.Append("\n , p.ProductDescription --//Solo para peru");
        sb.Append("\n From plm_vwProductsByEdition p ");
        sb.Append("\n Inner Join ProductCrops pc On p.ProductId = pc.ProductId ");
        sb.Append("\n Where p.EditionId = " + editionId + "  And p.TypeInEdition = 'P' And pc.CropId =  " + cultId);
        sb.Append("\n  -- And p.CategoryId in (2)  ");
        sb.Append("\n   Order by 1 ");
        DataSet ds = IndiceCultivos.DEAQInstanceDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());

        return ds.Tables[0];
    }

    ////////////////////////////////////////////////////////////////// Colombia //////////////////////////////////////////////////////////////////

    public DataTable getCultivoCol(string letter, int editionId)
    {
        StringBuilder sb = new StringBuilder();

        sb.Append("SELECT DISTINCT C.CROPID AS IDCul, C.CROPNAME AS Cultivo, pp.CountryId FROM CROPS C ");
        sb.Append("\n INNER JOIN PRODUCTCROPS PC ON C.CROPID = PC.CROPID ");
        sb.Append("\n INNER JOIN plm_vwProductsByEdition PP ON PC.PRODUCTID = PP.PRODUCTID ");
        sb.Append("\n WHERE PP.EDITIONID = " + editionId + " And pp.TYPEINEDITION = 'p' AND C.CROPNAME COLLATE SQL_Latin1_General_CP1_CI_AI LIKE '" + letter + "%' ");
        sb.Append("\n And pp.CategoryId not in (2) ");
        sb.Append("\n  ORDER BY 2 ");

        DataSet ds = IndiceCultivos.DEAQInstanceDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());

        return ds.Tables[0];
    }

    public DataTable getProductsCol(int cultId, int editionId)
    {
        StringBuilder sb = new StringBuilder(); 

        sb.Append("\n SELECT DISTINCT P.PRODUCTNAME AS Nombre, null as Sust, ");
        sb.Append("\n dbo.plm_dfGetTopAgrochemicalUsesByProduct(P.PRODUCTID) AS Usos, ");
        sb.Append("\n p.DivisionShortName AS Laboratorio, ");
        sb.Append("\n p.PAGE AS Pagina, p.ProductId ");
        sb.Append("\n , p.ProductDescription --//Solo para peru");
        sb.Append("\n From plm_vwProductsByEdition p ");
        sb.Append("\n Inner Join ProductCrops pc On p.ProductId = pc.ProductId ");
        sb.Append("\n Where p.EditionId = " + editionId + "  And p.TypeInEdition = 'P' And pc.CropId =  " + cultId);
        sb.Append("\n And p.CategoryId not in (2)  ");
        sb.Append("\n   Order by 1 ");
        DataSet ds = IndiceCultivos.DEAQInstanceDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());

        return ds.Tables[0];
    }

    ////////////////////////////////////////////////////////////////// SEMILLAS //////////////////////////////////////////////////////////////////

    public DataTable getAlphabetSeeds(int editionId)
    {
        StringBuilder sb = new StringBuilder();

        sb.Append("SELECT DISTINCT Substring(CROPNAME COLLATE SQL_Latin1_General_CP1_CI_AI ,1,1) letter, pp.CountryId FROM Crops c ");
        sb.Append("\nINNER JOIN ProductCrops pc ON c.CropId = pc.CropId ");
        sb.Append("\nINNER JOIN plm_vwProductsByEdition pp On pc.ProductId = pp.ProductId ");
        sb.Append("\nWHERE pp.EditionId = " + editionId + " and pp.TYPEINEDITION = 'p' And pp.CategoryId   in (2) ");
        sb.Append("\nORDER BY 1 ");

        DataSet ds = IndiceCultivos.DEAQInstanceDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());

        return ds.Tables[0];

    }

    public DataTable getCultivoSeeds(string letter, int editionId)
    {
        StringBuilder sb = new StringBuilder();

        sb.Append("SELECT DISTINCT C.CROPID AS IDCul, C.CROPNAME AS Cultivo, pp.CountryId FROM CROPS C ");
        sb.Append("\n INNER JOIN PRODUCTCROPS PC ON C.CROPID = PC.CROPID ");
        sb.Append("\n INNER JOIN plm_vwProductsByEdition PP ON PC.PRODUCTID = PP.PRODUCTID ");
        sb.Append("\n WHERE PP.EDITIONID = " + editionId + " And pp.TYPEINEDITION = 'p' AND C.CROPNAME COLLATE SQL_Latin1_General_CP1_CI_AI LIKE '" + letter + "%' ");
        sb.Append("\n And pp.CategoryId in (2) ");
        sb.Append("\n  ORDER BY 2 ");

        DataSet ds = IndiceCultivos.DEAQInstanceDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());

        return ds.Tables[0];
    }

    public DataTable getProductsSeeds(int cultId, int editionId)
    {
        StringBuilder sb = new StringBuilder();

        sb.Append("\n SELECT DISTINCT P.PRODUCTNAME AS Nombre, ");
        sb.Append("\n dbo.plm_dfGetTopAgrochemicalUsesByProduct(P.PRODUCTID) AS Usos, ");
        sb.Append("\n p.DivisionShortName AS Laboratorio, ");
        sb.Append("\n p.PAGE AS Pagina, p.ProductId ");
        sb.Append("\n , p.ProductDescription --//Solo para peru");
        sb.Append("\n From plm_vwProductsByEdition p ");
        sb.Append("\n Inner Join ProductCrops pc On p.ProductId = pc.ProductId ");
        sb.Append("\n Where p.EditionId = " + editionId + "  And p.TypeInEdition = 'P' And pc.CropId =  " + cultId);
        sb.Append("\n And p.CategoryId in (2)  ");
        sb.Append("\n   Order by 1 ");
        DataSet ds = IndiceCultivos.DEAQInstanceDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());

        return ds.Tables[0];
    }

    #endregion

    public static readonly IndiceCultivos Instance = new IndiceCultivos();
}
