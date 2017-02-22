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

 
public class InformacionCultivos : DEAQDataAccessAdapter<object>
{
	 
    #region Constructors

    public InformacionCultivos() { }

    #endregion

    #region Public Methods 

    public DataTable getAlphabet()
    {
       
        StringBuilder sb = new StringBuilder();

        sb.Append("\n select distinct  top 1 SUBSTRING (c.CropName, 1,1) as Letter ");
        sb.Append("\n from StateCrops sc");
        sb.Append("\n inner join States s on sc.StateId = s.StateId");
        sb.Append("\n inner join Crops c on sc.CropId = c.CropId");
        sb.Append("\n order by Letter");
 
        DataSet ds = InformacionCultivos.DEAQInstanceDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());

        return ds.Tables[0];
    }

    public DataTable getCultivos (string letter)
    {
        StringBuilder sb = new StringBuilder();

            sb.Append("\n select distinct c.CropName,  c.ScientificName , sc.CropId");
            sb.Append("\n from StateCrops sc ");
            sb.Append("\n inner join States s on sc.StateId = s.StateId ");
            sb.Append("\n inner join Crops c on sc.CropId = c.CropId ");
            sb.Append("\n inner join CropPests cp on sc.CropId = cp.CropId ");
            sb.Append("\n where c.CropName COLLATE SQL_Latin1_General_CP1_CI_AI like ('" + letter + "%') ");
            sb.Append("\n order by c.CropName ");

            DataSet ds = InformacionCultivos.DEAQInstanceDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());

            return ds.Tables[0];

    }

    public DataTable getPlagasCultivos(int cropsId)
    {
        StringBuilder sb = new StringBuilder();

        sb.Append("\n select distinct p.PestName, p.ScientificName, p.PestId, cp.CropId, cpi.ImageName");
            sb.Append("\n from CropPests cp");
            sb.Append("\n inner join Pests p on cp.PestId = p.PestId");
            sb.Append("\n inner join CropPestImages cpi on cp.CropId = cpi.CropId  and cp.PestId = cpi.PestId");
            sb.Append("\n inner join CropFightPests cfp on cp.PestId = cfp.PestId and cp.CropId = cfp.CropId");
            sb.Append("\n where cp.CropId = " + cropsId);
            sb.Append("\n order by p.PestName");

            DataSet ds = InformacionCultivos.DEAQInstanceDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());

            return ds.Tables[0];

    }



    public DataTable getPlagasCultivosInfo(int cropsId, int pestId)
    {
        StringBuilder sb = new StringBuilder();

        sb.Append("\n select  distinct f.FightName, a.ActiveSubstanceName, CAST(CAST(cfp.Description AS text) AS varchar(max)) Description, cfp.CropId , cfp.PestId ");
        sb.Append("\n from CropFightPests cfp ");
        sb.Append("\n inner join ActiveSubstances a on cfp.ActiveSubstanceId = a.ActiveSubstanceId ");
        sb.Append("\n inner join FightTypes f on cfp.FightTypeId = f.FightTypeId ");
        sb.Append("\n where  cfp.CropId = " + cropsId + " and PestId = " + pestId);
        sb.Append("\n order by 1 ");

        DataSet ds = InformacionCultivos.DEAQInstanceDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());

        return ds.Tables[0];

    }


    public DataTable getEnfermedadesCultivos(int cropsId)
    {
        StringBuilder sb = new StringBuilder();

        sb.Append("\n select distinct d.DiseaseName, d.ScientificName, d.DiseaseId, cd.CropId, cdi.ImageName ");
        sb.Append("\n from CropDiseases cd ");
        sb.Append("\n inner join Diseases d on cd.DiseaseId = d.DiseaseId ");
        sb.Append("\n inner join CropDiseaseImages cdi on cd.CropId = cdi.CropId and cd.DiseaseId = cdi.DiseaseId ");
        sb.Append("\n inner join CropFightDiseases cfd on cd.CropId = cfd.CropId and cd.DiseaseId = cfd.DiseaseId ");
        sb.Append("\n where cd.CropId =  " + cropsId);
        sb.Append("\n order by d.DiseaseName ");


        DataSet ds = InformacionCultivos.DEAQInstanceDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());

        return ds.Tables[0];

    }

    public DataTable getEnfermedadesCultivosInfo(int cropsId, int diseaseId)
    {
        StringBuilder sb = new StringBuilder();

        sb.Append("\n select  distinct f.FightName, a.ActiveSubstanceName, CAST(CAST(cfd.Description AS text) AS varchar(max)) Description, cfd.CropId , cfd.DiseaseId "); 
        sb.Append("\n from CropFightDiseases cfd ");
        sb.Append("\n join ActiveSubstances a on cfd.ActiveSubstanceId = a.ActiveSubstanceId ");
        sb.Append("\n join FightTypes f on cfd.FightTypeId = f.FightTypeId ");
        sb.Append("\n where  cfd.CropId = " + cropsId + "  and DiseaseId = " + diseaseId);
        sb.Append("\n order by 1");

        DataSet ds = InformacionCultivos.DEAQInstanceDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());

        return ds.Tables[0];

    }


    public DataTable getMalezasCultivos(int cropsId)
    {
        StringBuilder sb = new StringBuilder();

        sb.Append("\n select distinct w.WeedName, w.ScientificName, w.WeedId, cw.CropId, cwi.ImageName ");
        sb.Append("\n from CropWeeds cw  ");
        sb.Append("\n inner join Weeds w on cw.WeedId = w.WeedId ");
        sb.Append("\n inner join CropWeedImages cwi on cw.CropId = cwi.CropId and cw.WeedId = cwi.WeedId ");
        sb.Append("\n inner join CropFightWeeds cfw on cw.CropId = cfw.CropId and cw.WeedId = cfw.WeedId ");
        sb.Append("\n where cw.CropId = " + cropsId );
        sb.Append("\n order by w.WeedName");



        DataSet ds = InformacionCultivos.DEAQInstanceDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());

        return ds.Tables[0];

    }


    public DataTable getMalezasCultivosInfo(int cropsId, int weedId)
    {
        StringBuilder sb = new StringBuilder();

        sb.Append("\n select  distinct f.FightName, a.ActiveSubstanceName, CAST(CAST(cfw.Description AS text) AS varchar(max)) Description, cfw.CropId , cfw.WeedId ");
        sb.Append("\n from CropFightWeeds cfw  ");
        sb.Append("\n join ActiveSubstances a on cfw.ActiveSubstanceId = a.ActiveSubstanceId ");
        sb.Append("\n join FightTypes f on cfw.FightTypeId = f.FightTypeId  ");
        sb.Append("\n where  cfw.CropId =" + cropsId + " and WeedId = " + weedId);
        sb.Append("\n order by 1 ");

        DataSet ds = InformacionCultivos.DEAQInstanceDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());

        return ds.Tables[0];

    }

    #endregion

    public static readonly InformacionCultivos Instance = new InformacionCultivos();


}