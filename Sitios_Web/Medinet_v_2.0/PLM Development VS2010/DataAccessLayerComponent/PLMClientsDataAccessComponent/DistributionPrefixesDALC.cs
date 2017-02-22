using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using PLMClientsBusinessEntities;

namespace PLMClientsDataAccessComponent
{
    public class DistributionPrefixesDALC : PLMClientsDataAccessAdapter<DistributionCodePrefixInfo>
    {

        #region Constructors

        private DistributionPrefixesDALC() { }

        #endregion

        #region Public Methods

        public List<DistributionCodePrefixInfo> getPrefixAll(int countryId, int distributionId)
        {
            List<DistributionCodePrefixInfo> BECollection = new List<DistributionCodePrefixInfo>();

            // Get the resultset from the stored procedure:
            using (IDataReader dataReader = DistributionPrefixesDALC.InstanceDatabase.ExecuteReader(
                DistributionPrefixesDALC.InstanceDatabase.GetStoredProcCommand("plm_spGetDistributionPrefix", countryId, distributionId)))
            {
                // Iterates through records:
                while (dataReader.Read())
                    BECollection.Add(this.getFromDataReader(dataReader));
            }

            return BECollection;

        }

       

        protected override DistributionCodePrefixInfo getFromDataReader(IDataReader current)
        {
            DistributionCodePrefixInfo record = new DistributionCodePrefixInfo();

            record.DistributionId = Convert.ToInt32(current["DistributionId"]);

            if (current["PrefixId"] != DBNull.Value)
                record.PrefixId = Convert.ToInt32(current["PrefixId"]);

             record.PrefixDescription = current["PrefixDescription"].ToString();
            //if (current["TargetId"] != DBNull.Value) record.TargetId = current["TargetId"].ToString();

            // record.Active = Convert.ToBoolean(current["Active"]);

            return record;
        }

        #endregion

        public static readonly DistributionPrefixesDALC Instance = new DistributionPrefixesDALC();
    }
}