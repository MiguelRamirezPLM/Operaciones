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
/// Descripción breve de GenCAD
/// </summary>
public class GenCAD : CADDataAccessAdapter<object>
{
    #region Constructors

    private GenCAD(){}

    #endregion

    #region Public Methods

    public DataTable getAlphabet(int editionId)
    {
        StringBuilder sb = new StringBuilder();

        sb.Append("SELECT DISTINCT SUBSTRING(BRAND,1,1) ");
        sb.Append("FROM PLM_VWPRODUCTSBYEDITION ");
        sb.Append("WHERE EDITIONID = " + editionId + " AND SUBSTRING(BRAND,1,1) <> 'Á'");
        sb.Append("ORDER BY 1");

        DataSet ds = GenCAD.CAD40InstanceDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());

        return ds.Tables[0];

    }

    public DataTable getProducts(string letra, int editionId)
    {
        StringBuilder sb = new StringBuilder();

        sb.Append("\nSELECT DISTINCT PRODUCTID,UPPER(BRAND) AS BRAND,PRODUCTDESCRIPTION, ");
        sb.Append("\ndbo.plm_dfGetActiveSubsByProduct(PRODUCTID) AS ACTIVESUBSTANCES, ");
        sb.Append("\ndbo.plm_dfGetPharmaFormsByProductC (PRODUCTID,EDITIONID,dbo.plm_dfGetCountriesByProduct (PRODUCTID,PHARMAFORMID)) AS PHARMAFORMS, ");
        sb.Append("\nDIVISIONSHORTNAME AS LABORATORYNAME, dbo.plm_dfGetPagesByProduct(PRODUCTID,EDITIONID,DIVISIONID) AS PAGE, ");
        sb.Append("\ndbo.plm_dfGetCountriesByProduct (PRODUCTID,PHARMAFORMID) as Countries ");
        sb.Append("\nFROM PLM_VWPRODUCTSBYEDITION ");
        sb.Append("\nWHERE TYPEINEDITION = 'P' AND EDITIONID = " + editionId);


        if (letra.Equals("N") || letra.Equals("Ñ"))
            sb.Append("\n AND Brand LIKE ('" + letra + "%') ");
            
        else
            sb.Append("AND Brand COLLATE SQL_Latin1_General_CP1_CI_AI LIKE ('" + letra + "%')");
        

        sb.Append("\nORDER BY 2");

        DataSet ds = GenCAD.CAD40InstanceDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());

        return ds.Tables[0];

    }

    #endregion

    public static readonly GenCAD Instance = new GenCAD();

}
