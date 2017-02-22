using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using PLMClientsBusinessEntities;

namespace PLMClientsDataAccessComponent
{
    public class DistributionTargetDALC : PLMClientsDataAccessAdapter<DistributionCodePrefixInfo>
    {

        #region Constructors

        private DistributionTargetDALC() { }

        #endregion

        #region Public Methods


        public List<DistributionCodePrefixInfo> getTargetAll(int distributionId, int prefixId, int countryId)
        {
            List<DistributionCodePrefixInfo> BECollection = new List<DistributionCodePrefixInfo>();

            // Get the resultset from the stored procedure:
            using (IDataReader dataReader = DistributionTargetDALC.InstanceDatabase.ExecuteReader(
                DistributionTargetDALC.InstanceDatabase.GetStoredProcCommand("plm_spGetDistributionTarget", distributionId, prefixId, countryId)))
            {
                // Iterates through records:
                while (dataReader.Read())
                    BECollection.Add(this.getFromDataReader(dataReader));
            }

            return BECollection;

        }


        public DistributionCodePrefixInfo getTarget(int distributionId, int prefixId, int countryId, int targetId)
        {
            DbCommand dbCmd = DistributionTargetDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spGetTarget");

            //Add the parameters:

            DistributionTargetDALC.InstanceDatabase.AddParameter(dbCmd, "@distributionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, distributionId);
            DistributionTargetDALC.InstanceDatabase.AddParameter(dbCmd, "@prefixId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, prefixId);
            DistributionTargetDALC.InstanceDatabase.AddParameter(dbCmd, "@countryId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, countryId);

            DistributionTargetDALC.InstanceDatabase.AddParameter(dbCmd, "@targetId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, targetId);
            using (
                IDataReader dataReader = DistributionTargetDALC.InstanceDatabase.ExecuteReader(dbCmd))
            {
                if (dataReader.Read())
                    return this.getFromDataReader(dataReader);
                else
                    return null;
            }
        }

        #endregion

        protected override DistributionCodePrefixInfo getFromDataReader(IDataReader current)
        {
            DistributionCodePrefixInfo record = new DistributionCodePrefixInfo();

            record.TargetId = Convert.ToByte(current["TargetId"]);
            record.DistributionId = Convert.ToInt32(current["DistributionId"]);
            record.PrefixId = Convert.ToInt32(current["PrefixId"]);
            record.TargetName = current["TargetName"].ToString();

            //if (current["TargetId"] != DBNull.Value) record.TargetId = current["TargetId"].ToString();

            //record.Active = Convert.ToBoolean(current["Active"]);

            return record;
        }
        public static readonly DistributionTargetDALC Instance = new DistributionTargetDALC();
    }
}
