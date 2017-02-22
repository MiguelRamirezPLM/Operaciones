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

        //sb.Append("\nSelect Distinct Substring(ProductName,1,1) ");
        //sb.Append("\nFrom Products ");
        //sb.Append("\nOrder by 1");

        //DEAQ 22 MEXICO
        sb.Append("\nSelect Distinct Substring(ProductName,1,1) ");
        sb.Append("\nFrom plm_vwProductsByEdition ");
        sb.Append("\nWhere EditionId = " + editionId );
        sb.Append("\nOrder By 1 ");

        DataSet ds = IndiceGeneral.DEAQInstanceDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());

        return ds.Tables[0];

    }

    public DataTable getProducts(string letter, int editionId)
    {
        StringBuilder sb = new StringBuilder();
        /*
        sb.Append("\nSelect Distinct p.Nombre,Registro = Case When Registro = 'No Tiene' Then '' ");
        sb.Append("\nWhen Registro = Null Then '' When Registro = 'Reg.' Then '' else Registro End ");
        sb.Append("\n,dbo.deaq_dfGetUsosByProduct(p.IDProd) as Usos, ");
        sb.Append("\ndbo.deaq_dfGetFormaByProduct(p.IDProd) as FFarmaceutica,p.Laboratorio,p.Pagina ");
        sb.Append("\nFrom Producto p");
        //sb.Append("\nLeft Join RelFFarmaceutica rf On(p.IdProd = rf.IdProducto) ");
        //sb.Append("\nLeft Join FFarmaceutica f On(rf.IdFFarmaceutica = f.IdFF) ");
        sb.Append("\nWhere p.Nombre Like ('" + letter + "%') Order by 1");
         */
        
        //sb.Append("\nSELECT DISTINCT p.ProductName AS Nombre, p.Register AS Registro, "); 
        //sb.Append("\ndbo.plm_dfGetAgrochemicalUsesByProduct(p.productID) AS Usos, ");
        //sb.Append("\ndbo.plm_dfGetPharmaFormsByDiffPagedProduct(p.ProductID, pp.EditionID, pp.Page) AS FFarmaceutica, ");
        //sb.Append("\nd.ShortName AS Laboratorio, pp.Page AS Pagina FROM Products p ");
        //sb.Append("\nINNER JOIN ProductPharmaForms ppf ON (p.ProductID = ppf.ProductID) ");
        //sb.Append("\nINNER JOIN PharmaForms pf ON (ppf.PharmaFormID = pf.PharmaFormID) ");
        //sb.Append("\nINNER JOIN ParticipantProducts pp ON (p.ProductId = pp.ProductId AND ppf.PharmaFormID = pp.PharmaFormID) ");
        //sb.Append("\nINNER JOIN Divisions d ON (pp.DivisionId = d.DivisionId) ");
        //sb.Append("\nWHERE pp.EditionId = 2 AND p.ProductName COLLATE SQL_LATIN1_GENERAL_CP1_CI_AI LIKE '" + letter + "%'");
        //sb.Append("\nUNION ");
        //sb.Append("\nSELECT DISTINCT p.ProductName AS Nombre, p.Register AS Registro, ");
        //sb.Append("\ndbo.plm_dfGetAgrochemicalUsesByProduct(p.productID) AS Usos, ");
        //sb.Append("\ndbo.plm_dfGetPharmaFormsByProduct(p.ProductID, pp.EditionID) AS FFarmaceutica, ");
        //sb.Append("\nd.ShortName AS Laboratorio, '' AS Pagina FROM Products p ");
        //sb.Append("\nINNER JOIN ProductPharmaForms ppf ON (p.ProductID = ppf.ProductID)");
        //sb.Append("\nINNER JOIN PharmaForms pf ON (ppf.PharmaFormID = pf.PharmaFormID) ");
        //sb.Append("\nINNER JOIN MentionatedProducts pp ON (p.ProductId = pp.ProductId AND ppf.PharmaFormID = pp.PharmaFormID) ");
        //sb.Append("\nINNER JOIN Divisions d ON (pp.DivisionId = d.DivisionId) ");
        //sb.Append("\nWHERE pp.EditionID = 2 AND p.ProductName COLLATE SQL_LATIN1_GENERAL_CP1_CI_AI LIKE '" + letter + "%'");
        //sb.Append("\nORDER BY 1 ");

        //DEAQ22 Mexico
        sb.Append("\nSELECT DISTINCT p.ProductName AS Nombre, p.Register AS Registro, ");
        sb.Append("\ndbo.plm_dfGetAgrochemicalUsesByProduct(p.ProductId) AS Usos, ");
        sb.Append("\ndbo.plm_dfGetPharmaFormsByDiffPagedProduct(p.ProductId, p.EditionID, p.Page) AS FFarmaceutica, ");
        sb.Append("\np.DivisionShortName AS Laboratorio, p.Page AS Pagina, p.ProductId ");
        sb.Append("\nFROM plm_vwProductsByEdition p ");
        sb.Append("\nWHERE p.EditionId = " + editionId + " AND p.ProductName COLLATE SQL_LATIN1_GENERAL_CP1_CI_AI LIKE '" + letter + "%' and p.TypeInEdition = 'P' ");
        sb.Append("\nUNION ");
        sb.Append("\nSELECT DISTINCT p.ProductName AS Nombre, p.Register AS Registro, ");
        sb.Append("\ndbo.plm_dfGetAgrochemicalUsesByProduct(p.productID) AS Usos, ");
        sb.Append("\ndbo.plm_dfGetPharmaFormsByProduct(p.ProductID, p.EditionID) AS FFarmaceutica, ");
        sb.Append("\np.DivisionShortName AS Laboratorio, '' AS Pagina, p.ProductId ");
        sb.Append("\nFROM plm_vwProductsByEdition p ");
        sb.Append("\nWHERE p.EditionId = " + editionId + " AND p.ProductName COLLATE SQL_LATIN1_GENERAL_CP1_CI_AI LIKE '" + letter + "%' and p.TypeInEdition = 'M' ");
        sb.Append("\nORDER BY 1 ");
                             

        DataSet ds = IndiceGeneral.DEAQInstanceDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());

        return ds.Tables[0];

    }

    #endregion

    public static readonly IndiceGeneral Instance = new IndiceGeneral();

}
