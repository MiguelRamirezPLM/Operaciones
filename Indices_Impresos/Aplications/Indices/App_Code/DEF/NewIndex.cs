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
/// Descripción breve de NewIndex
/// </summary>
public class NewIndex : PLMDataAccessAdapter<object>
{
    #region Constructors

    private NewIndex(){}

    #endregion


    public DataTable getNewProducts()
    {
        StringBuilder sb = new StringBuilder();

        sb.Append("SELECT DISTINCT P.PRODUCTID, UPPER(P.BRAND) AS BRAND,PC.[PRODUCTDESCRIPTION] as Description,dbo.plm_dfGetActiveSubsByProduct(P.ProductId) AS SUBSTANCE, ");
        sb.Append("dbo.plm_dfGetPharmaFormsByPagedProduct (P.ProductId,PPV.VERSIONID) AS PHARMAFORMS,UPPER(L.LABORATORYNAME) AS LABORATORY, dbo.plm_dfGetPagesByProduct(P.Productid,PPV.VersionId) AS PAGE ");
        sb.Append("FROM (SELECT DISTINCT PRODUCTID,PHARMAFORMID FROM PARTICIPANTPRODUCTS WHERE EDITIONID = 1) PP INNER JOIN PRODUCTS P ON(PP.PRODUCTID = P.PRODUCTID) ");
        sb.Append("INNER JOIN NEWPRODUCTS NP ON(PP.PRODUCTID = NP.PRODUCTID AND PP.PHARMAFORMID = NP.PHARMAFORMID) ");
        sb.Append("INNER JOIN PRODUCTVERSIONPAGES PPV ON (PP.PRODUCTID = PPV.PRODUCTID AND PP.PHARMAFORMID = PPV.PHARMAFORMID) ");
        sb.Append("INNER JOIN PRODUCTDESCRIPTIONS PC ON (PP.PRODUCTID = PC.PRODUCTID) ");
        sb.Append("INNER JOIN PRODUCTPHARMAFORMS PPF ON (PP.PRODUCTID = PPF.PRODUCTID AND PP.PHARMAFORMID = PPF.PHARMAFORMID) ");
        sb.Append("INNER JOIN PHARMACEUTICALFORMS PF ON (PP.PHARMAFORMID = PF.PHARMAFORMID) ");
        sb.Append("INNER JOIN LABORATORIES L ON (P.LABORATORYID = L.LABORATORYID) ");
        sb.Append("WHERE P.COUNTRYID = 11 AND PPV.VERSIONID = 1 ");
        sb.Append("ORDER BY 2");

        DataSet ds = NewIndex.DEFInstanceDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());

        return ds.Tables[0];

    }


    public static readonly NewIndex Instance = new NewIndex();    

}
