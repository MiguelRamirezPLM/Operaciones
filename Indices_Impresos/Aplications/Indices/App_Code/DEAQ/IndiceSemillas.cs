using System;
using System.Data;
using System.Configuration;
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
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public class IndiceSemillas : DEAQDataAccessAdapter<object>
{
    #region Constructors

    public IndiceSemillas(){}

    #endregion

    #region Public Methods

    public DataTable getSeeds(int editionId)
    {
        StringBuilder sb = new StringBuilder();

        sb.Append("\nSelect Distinct s.SeedId, s.SeedName ");
        sb.Append("\nFrom plm_vwProductsByEdition p ");
        sb.Append("\nInner Join ProductSeeds ps on p.ProductId = ps.ProductId ");
        sb.Append("\nInner Join Seeds s on ps.SeedId = s.SeedId ");
        sb.Append("\nWhere p.EditionId = " + editionId + " And TypeInEdition = 'P' ");
        sb.Append("\nOrder By 2 ");

        DataSet ds = IndiceSemillas.DEAQInstanceDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());

        return ds.Tables[0];
    }

    public DataTable getProducts(int seedId, int editionId)
    {
        StringBuilder sb = new StringBuilder();

        sb.Append("\nSelect Distinct p.ProductId, p.ProductName, p.DivisionShortName, p.Page ");
        sb.Append("\nFrom plm_vwProductsByEdition p ");
        sb.Append("\nInner Join ProductSeeds ps on p.ProductId = ps.ProductId ");
        sb.Append("\nWhere p.EditionId = " + editionId + " And TypeInEdition = 'P' and ps.SeedId = " + seedId );
        sb.Append("\nOrder by 2 ");

        DataSet ds = IndiceSemillas.DEAQInstanceDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());

        return ds.Tables[0];
    }

    #endregion

    public static readonly IndiceSemillas Instance = new IndiceSemillas();

}
