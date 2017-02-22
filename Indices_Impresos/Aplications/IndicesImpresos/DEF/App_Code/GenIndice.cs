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
/// Descripción breve de GenIndice
/// </summary>
public class GenIndice : PLMDataAccessAdapter<object>
{

    //int editionId = 48;

    #region Constructors

    public GenIndice() { }

    #endregion

    public DataTable getAlphabet(int editionId, string tipoProducto)
    {
        StringBuilder sb = new StringBuilder();

        sb.Append("\nSELECT DISTINCT SUBSTRING(BRAND,1,1) ");
        sb.Append("\nFROM PLM_VWPRODUCTSBYEDITION V");
        sb.Append("\nWHERE V.EDITIONID = " + editionId + " AND V.TYPEINEDITION = 'P' AND SUBSTRING(V.BRAND,1,1) NOT IN ('Á') ");

        if (tipoProducto != "")
               sb.Append("\nAND V.ProductType COLLATE SQL_Latin1_General_CP1_CI_AI LIKE '" + tipoProducto + "'");

        sb.Append("\nORDER BY 1");

        DataSet ds = GenIndice.DEFInstanceDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());

        return ds.Tables[0];

    }

    public DataTable getProducts(int editionId, string letra, string tipoProducto)
    {
        StringBuilder sb = new StringBuilder();

        //sb.Append("\nSELECT DISTINCT V.PRODUCTID,UPPER(V.BRAND) AS BRAND, SUBSTRING(V.[PRODUCTDESCRIPTION],1,1) + LOWER(SUBSTRING(V.[PRODUCTDESCRIPTION],2,LEN(V.[PRODUCTDESCRIPTION])))AS [PRODUCTDESCRIPTION], ");
        //sb.Append("\ndbo.plm_dfGetActiveSubsByProduct(V.PRODUCTID) AS ACTIVESUBSTANCES, ");
        //sb.Append("\ndbo.plm_dfGetPharmaFormsByPagedProduct(V.PRODUCTID, V.EDITIONID,V.DIVISIONID) as PHARMAFORMS,CASE WHEN V.DIVISIONSHORTNAME = 'LEMERY' OR V.DIVISIONSHORTNAME = 'IVAX' THEN 'TEVA' WHEN V.DIVISIONSHORTNAME = 'ITALMEX' OR V.DIVISIONSHORTNAME = 'UNDRA' THEN V.LABORATORYNAME ELSE UPPER(V.DIVISIONSHORTNAME) END AS LABORATORYNAME, dbo.plm_dfGetPagesByProduct(V.PRODUCTID,V.EDITIONID,V.DIVISIONID) AS PAGE ");
        //sb.Append("\nFROM PLM_VWPRODUCTSBYEDITION V ");
        ////sb.Append("\nLEFT JOIN PRODUCTDESCRIPTION PD on (v.productid= pd.productid)");
        //sb.Append("\nWHERE V.EDITIONID = " + editionId + " AND TYPEINEDITION = 'P' ");

        //sb.Append("\nSELECT DISTINCT V.PRODUCTID, ");
        //sb.Append("\nUPPER(V.BRAND) AS BRAND, SUBSTRING(V.[PRODUCTDESCRIPTION],1,1) + LOWER(SUBSTRING(V.[PRODUCTDESCRIPTION],2,LEN(V.[PRODUCTDESCRIPTION])))AS [PRODUCTDESCRIPTION], ");
        //sb.Append("\ndbo.plm_dfGetActiveSubsByProduct(V.PRODUCTID) AS ACTIVESUBSTANCES, ");
        //sb.Append("\ndbo.plm_dfGetPharmaFormsByDiffPagedProduct(V.PRODUCTID, V.EDITIONID,V.PAGE) as PHARMAFORMS, ");
        //sb.Append("\nV.DIVISIONSHORTNAME AS LABORATORYNAME, ");
        //sb.Append("\nV.PAGE AS PAGE ");
        //sb.Append("\nFROM PLM_VWPRODUCTSBYEDITION V ");
        //sb.Append("\nWHERE V.EDITIONID = " + editionId + " AND TYPEINEDITION = 'P' ");

        sb.Append("\nSELECT DISTINCT V.PRODUCTID,UPPER(V.BRAND) AS BRAND, SUBSTRING(V.[PRODUCTDESCRIPTION],1,1) + LOWER(SUBSTRING(v.[PRODUCTDESCRIPTION],2,LEN(V.[PRODUCTDESCRIPTION])))AS [PRODUCTDESCRIPTION],");
        sb.Append("\ndbo.plm_dfGetActiveSubsByProduct(V.PRODUCTID) AS ACTIVESUBSTANCES, ");
        sb.Append("\ndbo.plm_dfGetPharmaFormsByPagedProduct(V.PRODUCTID, V.EDITIONID, V.DIVISIONID) as PHARMAFORMS, ");
        sb.Append("\nCASE WHEN V.DivisionId IN (83,100,2335,2336) ");
        sb.Append("\nTHEN V.LaboratoryName ELSE V.DivisionShortName END AS LABORATORYNAME, dbo.plm_dfGetPagesByProduct(V.PRODUCTID,V.EDITIONID, V.DIVISIONID) AS PAGE ");
        sb.Append("\nFROM PLM_VWPRODUCTSBYEDITION V ");
        sb.Append("\nWHERE V.EDITIONID = " + editionId + " AND TYPEINEDITION = 'P' AND V.PAGE IS NOT NULL ");


        if (letra.Equals("N") || letra.Equals("Ñ"))
        {
            sb.Append(" AND V.BRAND LIKE ('" + letra + "%') And V.productActive = 1 ");
        }
        else
        {
            sb.Append(" AND V.BRAND COLLATE SQL_Latin1_General_CP1_CI_AI LIKE ('" + letra + "%') And V.productActive = 1 ");
        }

        if (tipoProducto != "")
            sb.Append("\n AND V.ProductType COLLATE SQL_Latin1_General_CP1_CI_AI LIKE '" + tipoProducto + "'");

        sb.Append(" ORDER BY 2");

        DataSet ds = GenIndice.DEFInstanceDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());

        return ds.Tables[0];

    }

    #region Recent Products

    public DataTable getRecentProducts(int editionId)
    {
        StringBuilder sb = new StringBuilder();

        sb.Append("Select Distinct Brand,PharmaForm, ");
        sb.Append("\nCASE WHEN DivisionId IN (83,100,2335,2336) ");
        sb.Append("\nTHEN LaboratoryName ELSE DivisionShortName END AS DivisionName, ");
        sb.Append("\ndbo.plm_dfGetActiveSubsByProduct(ProductId) AS Substances,Page ");
        sb.Append("\nFrom plm_vwProductsByEdition ");
        sb.Append("\nWhere NewProduct = 1 and EditionId = " + editionId);
        sb.Append("\nOrder By Brand,PharmaForm ");

        DataSet ds = GenIndice.DEFInstanceDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());

        return ds.Tables[0];


    }

    #endregion

    public static readonly GenIndice Instance = new GenIndice();
}
