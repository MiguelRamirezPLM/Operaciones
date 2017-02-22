using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using  MedinetBusinessEntries;
using System.Data.Common;
using System.Data;
namespace MedinetDataAccessComponent
{
    public class HypersensibilitiesDALC : MedinetDataAccessAdapter<MedinetBusinessEntries.HypersensibilitiesInfo>
    {
        #region Constructors

        private HypersensibilitiesDALC() { }

        #endregion

          public List<HypersensibilitiesInfo> getHypersensibilities(string search) {
                    DbCommand dbCmd = HypersensibilitiesDALC.MedInstanceDatabase.GetStoredProcCommand("dbo.plm_spGetHypersensibilitiesCatalog");
                    HypersensibilitiesDALC.MedInstanceDatabase.AddParameter(dbCmd, "@description", DbType.String,
                      ParameterDirection.Input, string.Empty, DataRowVersion.Current, search);
            List<MedinetBusinessEntries.HypersensibilitiesInfo> BECollection = new List<MedinetBusinessEntries.HypersensibilitiesInfo>();
            using (IDataReader dataReader = HypersensibilitiesDALC.MedInstanceDatabase.ExecuteReader(dbCmd))
            {
                MedinetBusinessEntries.HypersensibilitiesInfo record;

                // Iterates through records:
                while (dataReader.Read())
                {
                    record = new MedinetBusinessEntries.HypersensibilitiesInfo();
                    record.HypersensibilityId = Convert.ToInt32(dataReader["HypersensibilityId"]);
                    record.HypersensibilityName = dataReader["HypersensibilityName"].ToString();
                    BECollection.Add(record);
                }
            }
            return BECollection;
        }


        public static readonly HypersensibilitiesDALC Instance = new HypersensibilitiesDALC();
        
    }
}
