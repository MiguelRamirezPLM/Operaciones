using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using PLMClientsBusinessEntities;

namespace PLMClientsDataAccessComponent
{
    public class DistributionCountriesDALC : PLMClientsDataAccessAdapter<DistributionCodePrefixInfo>
    {

        #region Constructors

        private DistributionCountriesDALC() { }

        #endregion

        #region Public Methods

        public List<DistributionCodePrefixInfo> getDistributionCountriesAll(int countryId)
        {
            List<DistributionCodePrefixInfo> BECollection = new List<DistributionCodePrefixInfo>();

            // Get the resultset from the stored procedure:
            using (IDataReader dataReader = DistributionCountriesDALC.InstanceDatabase.ExecuteReader(
                DistributionPrefixesDALC.InstanceDatabase.GetStoredProcCommand("plm_spGetDistributionCountries", countryId)))
            {
                // Iterates through records:
                while (dataReader.Read())
                    BECollection.Add(this.getFromDataReader(dataReader));
            }

            return BECollection;

        }

      

        #endregion


        protected override DistributionCodePrefixInfo getFromDataReader(IDataReader current)
        {
            DistributionCodePrefixInfo record = new DistributionCodePrefixInfo();

            record.DistributionId = Convert.ToInt32(current["DistributionId"]);

           record.DistributionName = current["DistributionName"].ToString();
            //if (current["TargetId"] != DBNull.Value) record.TargetId = current["TargetId"].ToString();

            // record.Active = Convert.ToBoolean(current["Active"]);

            return record;
        }
        public static readonly DistributionCountriesDALC Instance = new DistributionCountriesDALC();
    }
}