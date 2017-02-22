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
/// Descripción breve de FarmaPrecio
/// </summary>
public class FarmaPrecio : PLMDataAccessAdapter<object>
{

    #region Constructors

    public FarmaPrecio()
    {

    }

    #endregion

    public DataTable getAlphabet(int countryId)
    {
        StringBuilder sb = new StringBuilder();

        // Todos los productos con presentacion 
        //sb.Append("\n select distinct  SUBSTRING(v.Brand COLLATE SQL_Latin1_General_CP1_CI_AI ,1,1) as Letter");
        //sb.Append("\n from plm_vwProductsByEdition v ");
        //sb.Append("\n inner join Presentations p on v.ProductId = p.ProductId");
        //sb.Append("\n and v.PharmaFormId = p.PharmaFormId");
        //sb.Append("\n and v.CategoryId = p.CategoryId");
        //sb.Append("\n and v.DivisionId = p.DivisionId");
        //sb.Append("\n inner join ProductPrices pp on p.PresentationId = pp.PresentationId");
        //sb.Append("\n inner join ProductBarCodes pb on pp.PresentationId = pb.PresentationId");
        //sb.Append("\n and pp.BarCodeId = pb.BarCodeId");
        //sb.Append("\n where v.TypeInEdition ='P' and v.CountryId = " + countryId);
        //sb.Append("\n order by 1");

        // PRODUCTOS CON CONTENIDO Y SKU

        sb.Append("\n select distinct SUBSTRING(v.Brand COLLATE SQL_Latin1_General_CP1_CI_AI ,1,1) as Letter");
        sb.Append("\n from plm_vwProductsByEdition v ");
        sb.Append("\n inner join ProductContents pc on v.ProductId = pc.ProductId");
        sb.Append("\n					  and v.PharmaFormId = pc.PharmaFormId");
        sb.Append("\n					  and v.CategoryId = pc.CategoryId");
        sb.Append("\n					  and v.DivisionId = pc.DivisionId");
        sb.Append("\n					  and v.EditionId = pc.EditionId");
        sb.Append("\n inner join Presentations p on v.ProductId = p.ProductId");
        sb.Append("\n				   and v.PharmaFormId = p.PharmaFormId");
        sb.Append("\n				   and v.CategoryId = p.CategoryId");
        sb.Append("\n				   and v.DivisionId = p.DivisionId");
        sb.Append("\n inner join ProductBarCodes pb on p.PresentationId = pb.PresentationId");
        sb.Append("\n where v.CountryId = " + countryId + " and v.TypeInEdition ='P' ");
        sb.Append("\n order by 1");

         DataSet dsp = FarmaPrecio.DEFInstanceDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());

         return dsp.Tables[0];

    }


    public DataTable getProducts(int countryId, string letter)
    {
        StringBuilder sb = new StringBuilder();

        // Todos los productos con presentacion 
        //sb.Append("\n select distinct  v.ProductId, v.Brand,v.DivisionId,v.DivisionShortName , v.CategoryId,dbo.plm_dfGetActiveSubsByProduct(v.ProductId) as  ActiveSubstances, v.ProductDescription");
        //sb.Append("\n from plm_vwProductsByEdition v");
        //sb.Append("\n inner join Presentations p on v.ProductId = p.ProductId");
        //sb.Append("\n  and v.PharmaFormId = p.PharmaFormId");
        //sb.Append("\n  and v.CategoryId = p.CategoryId");
        //sb.Append("\n and v.DivisionId = p.DivisionId");
        //sb.Append("\ninner join ProductPrices pp on p.PresentationId = pp.PresentationId");
        //sb.Append("\ninner join ProductBarCodes pb on pp.PresentationId = pb.PresentationId");
        //sb.Append("\n  and pp.BarCodeId = pb.BarCodeId");
        //sb.Append("\n where v.TypeInEdition ='P' and v.CountryId = " + countryId + "and v.Brand COLLATE SQL_Latin1_General_CP1_CI_AI LIKE '" + letter + "%'");
        //sb.Append("\norder by v.Brand");

        // PRODUCTOS CON CONTENIDO Y SKU

        sb.Append("\n select distinct  v.ProductId, v.Brand,v.DivisionId,v.DivisionShortName , v.CategoryId,dbo.plm_dfGetActiveSubsByProduct(v.ProductId) as  ActiveSubstances, v.ProductDescription, v.PharmaFormId");
        sb.Append("\n from plm_vwProductsByEdition v ");
        sb.Append("\n inner join ProductContents pc on v.ProductId = pc.ProductId");
        sb.Append("\n 				  and v.PharmaFormId = pc.PharmaFormId");
        sb.Append("\n 				  and v.CategoryId = pc.CategoryId");
        sb.Append("\n				  and v.DivisionId = pc.DivisionId");
        sb.Append("\n				  and v.EditionId = pc.EditionId");
        sb.Append("\n inner join Presentations p on v.ProductId = p.ProductId");
        sb.Append("\n				   and v.PharmaFormId = p.PharmaFormId");
        sb.Append("\n				   and v.CategoryId = p.CategoryId");
        sb.Append("\n				   and v.DivisionId = p.DivisionId");
        sb.Append("\n inner join ProductBarCodes pb on p.PresentationId = pb.PresentationId");
        sb.Append("\n where v.TypeInEdition ='P' and pb.AveragePrice != 0 and v.CountryId = " + countryId + " and v.Brand COLLATE SQL_Latin1_General_CP1_CI_AI LIKE '" + letter + "%'");
        sb.Append("\n order by v.Brand");
         

        DataSet dsp = FarmaPrecio.DEFInstanceDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());

        return dsp.Tables[0];

    }

    public DataTable getProductPresentations(int productId, int divisionId,  int categoryId, int pharmaForm)
    {
        StringBuilder sb = new StringBuilder();

        // Todos los productos con presentacion 

        //sb.Append("\n select  distinct  p.PresentationId ,p.Presentation,Round(CONVERT(money,pp.Price,4),2) Price ,   pb.AveragePrice, pp.BarCodeId");
        //sb.Append("\n from plm_vwProductsByEdition v");
        //sb.Append("\n inner join Presentations p on v.ProductId = p.ProductId");
        //sb.Append("\n	  and v.PharmaFormId = p.PharmaFormId");
        //sb.Append("\n     and v.CategoryId = p.CategoryId");
        //sb.Append("\n     and v.DivisionId = p.DivisionId");
        //sb.Append("\n inner join ProductPrices pp on p.PresentationId = pp.PresentationId");
        //sb.Append("\n inner join ProductBarCodes pb on pp.PresentationId = pb.PresentationId");
        //sb.Append("\n     and pp.BarCodeId = pb.BarCodeId");
        //sb.Append("\n where p.ProductId = " + productId + " and p.DivisionId = " + divisionId + "  and p.CategoryId =" + categoryId);
        //sb.Append("\n order by p.Presentation");

        // PRODUCTOS CON CONTENIDO Y SKU

        sb.Append("\n select  distinct  p.PresentationId ,p.Presentation,  pb.AveragePrice");
        sb.Append("\n from plm_vwProductsByEdition v ");
        sb.Append("\n inner join ProductContents pc on v.ProductId = pc.ProductId");
        sb.Append("\n 			  and v.CategoryId = pc.CategoryId");
        sb.Append("\n 			  and v.DivisionId = pc.DivisionId");
        sb.Append("\n 			  and v.EditionId = pc.EditionId");
        sb.Append("\n inner join Presentations p on v.ProductId = p.ProductId");
        sb.Append("\n 		   and v.PharmaFormId = p.PharmaFormId");
        sb.Append("\n 		   and v.CategoryId = p.CategoryId");
        sb.Append("\n 		   and v.DivisionId = p.DivisionId");
        sb.Append("\n inner join ProductBarCodes pb on p.PresentationId = pb.PresentationId");
        sb.Append("\n where  p.ProductId = " + productId + " and p.DivisionId = " + divisionId + "  and p.CategoryId =" + categoryId + "and v.TypeInEdition ='P'  and pb.AveragePrice != 0 ");
        sb.Append("\n order by p.Presentation");


        DataSet dsp = FarmaPrecio.DEFInstanceDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());

        return dsp.Tables[0];

    }

    public static readonly FarmaPrecio Instance = new FarmaPrecio();
}
