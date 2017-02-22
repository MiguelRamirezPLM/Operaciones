using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.ComponentModel;

using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;


/// <summary>
/// Descripción breve de GetData
/// </summary>
public class GetData
{
	private GetData()
	{
	}

    public PharmaSearchEngine.ActiveSubstanceInfo[] generateSubstance()
    {
        PharmaSearchEngine.ActiveSubstanceInfo[] data = pse.getSubstances("", 11, true, 19, true, "");
        
        return data;
    }

    public PharmaSearchEngine.ProductByEditionInfo[] generateProductsBySubstance(int substanceId) 
    {
        PharmaSearchEngine.ProductByEditionInfo[]  data = pse.getDrugsBySubstance("", 11, true, 19, true, substanceId, true);

        return data;
    }

    public PharmaSearchEngine.IndicationInfo[] generateIndication()
    {
        PharmaSearchEngine.IndicationInfo[] data = pse.getIndications("", 11, true, 19, true, "");

        return data;
    }

    public PharmaSearchEngine.ProductByEditionInfo[] getProductsByIndication(int indicationId)
    {
        PharmaSearchEngine.ProductByEditionInfo[] data = pse.getDrugsByIndication("", 11, true, 19, true, indicationId, true);

        return data;
    }

    public PharmaSearchEngine.DivisionByEditionInfo[] generateLab()
    {
        PharmaSearchEngine.DivisionByEditionInfo[] data = pse.getLabs("", 11, true, 19, true, "");

        return data;
    }

    public PharmaSearchEngine.DivisionInformationInfo generateLabInformation(int divisionId)
    {
        PharmaSearchEngine.DivisionInformationInfo data = pse.getLabInformation("", divisionId, true);

        return data;
    }

    public PharmaSearchEngine.ProductByEditionInfo[] generateProduct()
    {
        PharmaSearchEngine.ProductByEditionInfo[] data = pse.getDrugs("", 11, true, 19, true, "");

        return data;
    }

    public PharmaSearchEngine.AttributeByProductInfo[] generateAttributesByProduct(int productId, int divisionId, int categoryId, int pharmaFormId)
    {
        PharmaSearchEngine.AttributeByProductInfo[] data = pse.getAttributesByDrug("", 19, true, divisionId, true, categoryId, true, productId, true, pharmaFormId, true);

        return data;
    }

    

    public string generateSubstanceByProduct(int productId)
    {
        string sSQL = "Select Distinct dbo.plm_dfGetActiveSubsByProduct(" + productId.ToString() +  ") " +
                      "  from dbo.plm_vwProductsByEdition v " +
                      "  where editionid= 19";

        return DB.ExecuteDataSet(CommandType.Text, sSQL).Tables[0].Rows[0][0].ToString();
    }



    private PharmaSearchEngine.iOSPharmaSearchEngine pse = new PharmaSearchEngine.iOSPharmaSearchEngine();

    protected static readonly Database DB = DatabaseFactory.CreateDatabase("MedinetConnectionString");
    
    public static readonly GetData Instance = new GetData();


}
