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

public class IndiceSustancias : DEAQDataAccessAdapter<object>
{
    #region Constructors

    public IndiceSustancias() { }

    #endregion

    #region Public Methods 

    public DataTable getAlphabet(int editionId)
    {
        StringBuilder sb = new StringBuilder();

        sb.Append("\nSELECT DISTINCT SUBSTRING(A.ACTIVESUBSTANCENAME,1,1) ");
        sb.Append("\nFROM PLM_VWPRODUCTSBYEDITION V ");
        sb.Append("\nINNER JOIN PRODUCTSUBSTANCES PS ON V.PRODUCTID = PS.PRODUCTID ");
        sb.Append("\nINNER JOIN ACTIVESUBSTANCES A ON PS.ACTIVESUBSTANCEID = A.ACTIVESUBSTANCEID ");
        sb.Append("\nWHERE V.PRODUCTACTIVE = 1 AND V.EDITIONID = " + editionId + " AND A.ACTIVE = 1 AND SUBSTRING(A.ACTIVESUBSTANCENAME,1,1) NOT IN ('Á','É','Ó')");
        

        DataSet ds = IndiceSustancias.DEAQInstanceDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());

        return ds.Tables[0];
    }

    public DataTable getSubstances(string letter, int editionId)
    {
        StringBuilder sb = new StringBuilder();

        sb.Append("\nSELECT DISTINCT A.ACTIVESUBSTANCEID,UPPER(A.ACTIVESUBSTANCENAME) as IActivo ");
        sb.Append("\nFROM PLM_VWPRODUCTSBYEDITION V ");
        sb.Append("\nINNER JOIN PRODUCTSUBSTANCES PS ON V.PRODUCTID = PS.PRODUCTID ");
        sb.Append("\nINNER JOIN ACTIVESUBSTANCES A ON PS.ACTIVESUBSTANCEID = A.ACTIVESUBSTANCEID ");
        sb.Append("\nWHERE V.PRODUCTACTIVE = 1 AND V.EDITIONID = " + editionId + " AND A.ACTIVE = 1 ");
        sb.Append("\nAND A.ACTIVESUBSTANCENAME COLLATE SQL_Latin1_General_CP1_CI_AI Like ('" + letter + "%') ");
        sb.Append("\n");
        
        DataSet ds = IndiceSustancias.DEAQInstanceDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());

        return ds.Tables[0];

    }

    public DataTable getProducts(int idIngAct, int editionId)
    {
        StringBuilder sb = new StringBuilder();

        sb.Append("\nSELECT DISTINCT UPPER(V.PRODUCTNAME) As Nombre, ");
        sb.Append("\ndbo.plm_dfGetActiveSubstancesByProduct(V.PRODUCTID, PS.ACTIVESUBSTANCEID) As Sustancias, ");
        sb.Append("\ndbo.plm_dfGetPharmaFormsByDiffPagedProduct(V.PRODUCTID, V.EDITIONID, V.PAGE) As FFarmaceutica, ");
        //sb.Append("\ndbo.plm_dfGetAgrochemicalUsesByProduct(V.PRODUCTID, V.EDITIONID, V.PAGE), ");
        sb.Append("\nV.DIVISIONSHORTNAME As Laboratorio,V.PAGE ");
        sb.Append("\n,V.ProductDescription ");//Caso PERU
        sb.Append("\nFROM PLM_VWPRODUCTSBYEDITION V ");
        sb.Append("\nINNER JOIN PRODUCTSUBSTANCES PS ON V.PRODUCTID = PS.PRODUCTID ");
        sb.Append("\nWHERE V.EDITIONID = " + editionId + " AND PS.ACTIVESUBSTANCEID = " + idIngAct);
        sb.Append("\nORDER BY 1");

        DataSet ds = IndiceSustancias.DEAQInstanceDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());

        return ds.Tables[0];
    }


    #endregion

    public static readonly IndiceSustancias Instance = new IndiceSustancias();
}
