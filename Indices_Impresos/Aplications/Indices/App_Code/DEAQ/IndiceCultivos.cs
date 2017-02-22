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

        sb.Append("SELECT DISTINCT Substring(CROPNAME,1,1) FROM Crops c ");
        sb.Append("\nINNER JOIN ProductCrops pc ON c.CropId = pc.CropId ");
        sb.Append("\nINNER JOIN plm_vwProductsByEdition pp On pc.ProductId = pp.ProductId ");
        sb.Append("\nWHERE pp.EditionId = " + editionId);
        sb.Append("\nORDER BY 1 ");

        DataSet ds = IndiceCultivos.DEAQInstanceDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());

        return ds.Tables[0];

    }

    public DataTable getCultivo(string letter, int editionId)
    {
        StringBuilder sb = new StringBuilder();

        sb.Append("SELECT DISTINCT C.CROPID AS IDCul, C.CROPNAME AS Cultivo FROM CROPS C ");
        sb.Append("\nINNER JOIN PRODUCTCROPS PC ON C.CROPID = PC.CROPID ");
        sb.Append("\nINNER JOIN plm_vwProductsByEdition PP ON PC.PRODUCTID = PP.PRODUCTID ");
        sb.Append("\nWHERE PP.EDITIONID = " + editionId + " AND C.CROPNAME COLLATE SQL_Latin1_General_CP1_CI_AI LIKE '" + letter + "%' ");
        sb.Append("\nORDER BY 2 ");

        DataSet ds = IndiceCultivos.DEAQInstanceDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());

        return ds.Tables[0];
    }

    public DataTable getProducts(int cultId, int editionId)
    {
        StringBuilder sb = new StringBuilder();

        sb.Append("\nSELECT DISTINCT P.PRODUCTNAME AS Nombre, ");
        //sb.Append("\ndbo.plm_dfGetTopActiveSubsByProduct(P.PRODUCTID) AS Sustancias, ");
        sb.Append("\ndbo.plm_dfGetTopAgrochemicalUsesByProduct(P.PRODUCTID) AS Usos, ");
        sb.Append("\np.DivisionShortName AS Laboratorio, ");
        sb.Append("\np.PAGE AS Pagina, p.ProductId ");
        sb.Append("\nFrom plm_vwProductsByEdition p ");
        sb.Append("\nInner Join ProductCrops pc On p.ProductId = pc.ProductId ");
        sb.Append("\nWhere p.EditionId = " + editionId + " And p.TypeInEdition = 'P' And p.CategoryId NOT IN(2) And pc.CropId = " + cultId);
        sb.Append("\nOrder by 1 ");


        DataSet ds = IndiceCultivos.DEAQInstanceDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());

        return ds.Tables[0];
    }


    #endregion

    public static readonly IndiceCultivos Instance = new IndiceCultivos();
}
