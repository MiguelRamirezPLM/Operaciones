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
/// Descripción breve de GuiaDataAccess
/// </summary>
public class GuiaDataAccess : PLMDataAccessAdapter<object>
{
    #region Constructors

    private GuiaDataAccess() { }

    #endregion

    #region Public methods

    public DataTable getCompanies()
    {
        StringBuilder sb = new StringBuilder();

        sb.Append("SELECT * FROM Empresas ORDER BY [Nombre LARGO - GUIA 53]");

        DataSet ds = GuiaDataAccess.GuiaInstanceDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());

        return ds.Tables[0];
    }

    public DataTable getBrands(int companyId)
    {
        StringBuilder sb = new StringBuilder();

        sb.Append("\nSELECT tmp.MARCA, tmp.[Nombre del Archivo], CARTA.CARTA");
        sb.Append("\nFROM (");
        sb.Append("\nSELECT MARCAS.Marca, MARCAS.[Nombre del Archivo], Tabla1.MARCASId_, Tabla1.CARTA");
        sb.Append("\nFROM MARCAS");
        sb.Append("\nINNER JOIN Tabla1 ON MARCAS.ID = Tabla1.MARCASId_");
        sb.Append("\nWHERE EMPRESASId_ = " + companyId.ToString() + " ) tmp");
        sb.Append("\nINNER JOIN CARTA ON (tmp.CARTA = CARTA.ID)");
        sb.Append("\nORDER BY MARCA");

        DataSet ds = GuiaDataAccess.GuiaInstanceDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());

        return ds.Tables[0];
    }

    public DataTable getCompany(int companyId)
    {
        StringBuilder sb = new StringBuilder();

        sb.Append("\nSELECT * FROM EMPRESAS WHERE ID = " + companyId.ToString());

        DataSet ds = GuiaDataAccess.GuiaInstanceDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());

        return ds.Tables[0];
    }

    #endregion

    public static readonly GuiaDataAccess Instance = new GuiaDataAccess();
}
