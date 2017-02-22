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
/// Descripción breve de VMGenIndice
/// </summary>
public class VMGenIndice : VMGDataAccessAdapter<object>
{
    public VMGenIndice()
    {

    }

    public DataTable getAlphabet(int editionId)
    {
        StringBuilder sb = new StringBuilder();


        sb.Append("\nSELECT DISTINCT SUBSTRING(BRAND,1,1) ");
        sb.Append("\nFROM PLM_VWPRODUCTSBYEDITION ");
        sb.Append("\nWHERE EDITIONID = " + editionId + " AND TYPEINEDITION = 'P' AND SUBSTRING(BRAND,1,1) NOT IN ('Á','É','Ó') ");
        sb.Append("\nORDER BY 1");

        DataSet ds = VMGenIndice.VMGInstanceDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());

        return ds.Tables[0];
    }

    public DataTable getProducts(int editionId, string letra)
    {
        StringBuilder sb = new StringBuilder();

        sb.Append("\nSELECT  DISTINCT ");
        sb.Append("\nUPPER(v.Brand) AS BRAND,");
        sb.Append("\ndbo.plm_dfGetPharmaFormsByProductName(v.editionId, v.brand)as PHARMAFORMS,");
        sb.Append("\nv.Page AS PAGE");
        sb.Append("\nFROM plm_vwProductsByEdition v");
        sb.Append("\nWHERE EditionId =" + editionId + " AND TYPEINEDITION = 'P'");

        if (letra.Equals("N") || letra.Equals("Ñ"))
        {
            sb.Append("\nAND V.BRAND LIKE ('" + letra + "%') And V.productActive = 1 ");
        }
        else
        {
            sb.Append("\nAND V.BRAND COLLATE SQL_Latin1_General_CP1_CI_AI LIKE ('" + letra + "%') And V.productActive = 1 ");
        }

        sb.Append("\nORDER BY 1");

        DataSet ds = VMGenIndice.VMGInstanceDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());
            

        return ds.Tables[0];
    }
    public static readonly VMGenIndice Instance = new VMGenIndice();
    
}