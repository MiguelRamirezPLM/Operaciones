using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using PLMUserBusinessEntries;

namespace PLMUsersDataAccessComponent
{
    public class BinnacleActivitiesDALC : PLMUsersDataAccesAdapter<BinnacleActivitiesInfo>
    {
        #region Constructors

        private BinnacleActivitiesDALC() { }

        #endregion

        #region Public Methods

        public BinnacleActivitiesInfo getOne(BinnacleActivitiesInfo businessEntity)
        {
            DbCommand dbCmd = BinnacleActivitiesDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDBinnacleActivities");

            //Add the parameters:
            BinnacleActivitiesDALC.InstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, UsersDALC.CRUD.Read);
            BinnacleActivitiesDALC.InstanceDatabase.AddParameter(dbCmd, "@binnacleId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.BinnacleId);
            BinnacleActivitiesDALC.InstanceDatabase.AddParameter(dbCmd, "@activityId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ActivityId);

            using (IDataReader dataReader = UsersDALC.InstanceDatabase.ExecuteReader(dbCmd))
            {
                if (dataReader.Read())
                    return this.getFromDataReader(dataReader);
                else
                    return null;
            }
        }

        public override int insert(BinnacleActivitiesInfo businessEntity)
        {
            DbCommand dbCmd = BinnacleActivitiesDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDBinnacleActivities");

            // Add the parameters:
            BinnacleActivitiesDALC.InstanceDatabase.AddParameter(dbCmd, "@Return", DbType.Int32,
                ParameterDirection.ReturnValue, string.Empty, DataRowVersion.Current, 0);
            BinnacleActivitiesDALC.InstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Create);
            BinnacleActivitiesDALC.InstanceDatabase.AddParameter(dbCmd, "@binnacleId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.BinnacleId);
            BinnacleActivitiesDALC.InstanceDatabase.AddParameter(dbCmd, "@activityId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ActivityId);
            BinnacleActivitiesDALC.InstanceDatabase.AddParameter(dbCmd, "@comment", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.Comment);

            //Insert record:
            BinnacleActivitiesDALC.InstanceDatabase.ExecuteNonQuery(dbCmd);
            businessEntity.BinnacleId = Convert.ToInt32(dbCmd.Parameters["@Return"].Value);
            return Convert.ToInt32(dbCmd.Parameters["@Return"].Value);
        }

        public override void update(BinnacleActivitiesInfo businessEntity)
        {
            DbCommand dbCmd = BinnacleActivitiesDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDBinnacles");

            // Add the parameters:
            BinnacleActivitiesDALC.InstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Update);
            BinnacleActivitiesDALC.InstanceDatabase.AddParameter(dbCmd, "@binnacleId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.BinnacleId);
            BinnacleActivitiesDALC.InstanceDatabase.AddParameter(dbCmd, "@activityId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ActivityId);
            BinnacleActivitiesDALC.InstanceDatabase.AddParameter(dbCmd, "@comment", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.Comment);

            //Update record:
            BinnacleActivitiesDALC.InstanceDatabase.ExecuteNonQuery(dbCmd);
        }

        public List<BinnacleActivitiesInfo> getBinnacleActivities()
        {
            DbCommand dbCmd = BinnacleActivitiesDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spGetBinnacleActivities");

            List<BinnacleActivitiesInfo> BECollection = new List<BinnacleActivitiesInfo>();

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = BinnacleActivitiesDALC.InstanceDatabase.ExecuteReader(dbCmd))
            {
                // Iterates through records:
                while (dataReader.Read())
                {
                    BECollection.Add(this.getFromDataReader(dataReader));
                }
            }
            return BECollection;
        }

         #endregion

        #region Protected Methods

        protected override BinnacleActivitiesInfo getFromDataReader(IDataReader current)
        {
            BinnacleActivitiesInfo record = new BinnacleActivitiesInfo();

            record.BinnacleId = Convert.ToInt32(current["BinnacleId"]);
            record.ActivityId = Convert.ToInt32(current["ActivityId"]);
            if (current["Comment"] != System.DBNull.Value)
                record.Comment = current["Comment"].ToString();

            return record;
        }

        #endregion

        public static readonly BinnacleActivitiesDALC Instance = new BinnacleActivitiesDALC();
    }
}

   