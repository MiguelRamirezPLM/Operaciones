using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using PLMUserBusinessEntries;

namespace PLMUsersDataAccessComponent
{
    public class BinnaclesDALC : PLMUsersDataAccesAdapter<BinnaclesInfo>
    {
        #region Constructors

        private BinnaclesDALC() { }

        #endregion

        #region Public Methods

        public List<BinnaclesInfo> getBinnacles()
        {
            DbCommand dbCmd = BinnaclesDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spGetBinnacles");

            List<BinnaclesInfo> BECollection = new List<BinnaclesInfo>();

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = BinnaclesDALC.InstanceDatabase.ExecuteReader(dbCmd))
            {
                // Iterates through records:
                while (dataReader.Read())
                {
                    BECollection.Add(this.getFromDataReader(dataReader));
                }
            }
            return BECollection;
        }

        public List<BinnaclesInfo> getBinnaclesByUser(int userId)
        {
            DbCommand dbCmd = BinnaclesDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spGetBinnaclesByUser");

            BinnaclesDALC.InstanceDatabase.AddParameter(dbCmd, "@userId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, userId);

            List<BinnaclesInfo> BECollection = new List<BinnaclesInfo>();

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = BinnaclesDALC.InstanceDatabase.ExecuteReader(dbCmd))
            {
                // Iterates through records:
                while (dataReader.Read())
                {
                    BECollection.Add(this.getFromDataReader(dataReader));
                }
            }
            return BECollection;
        }

        public override BinnaclesInfo getOne(int pk)
        {
            DbCommand dbCmd = BinnaclesDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDBinnacles");

            //Add the parameters:
            BinnaclesDALC.InstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, UsersDALC.CRUD.Read);
            BinnaclesDALC.InstanceDatabase.AddParameter(dbCmd, "@binnacleId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, pk);

            using (IDataReader dataReader = UsersDALC.InstanceDatabase.ExecuteReader(dbCmd))
            {
                if (dataReader.Read())
                    return this.getFromDataReader(dataReader);
                else
                    return null;
            }
        }

        public override int insert(BinnaclesInfo businessEntity)
        {
            DbCommand dbCmd = BinnaclesDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDBinnacles");

            // Add the parameters:
            BinnaclesDALC.InstanceDatabase.AddParameter(dbCmd, "@Return", DbType.Int32,
                ParameterDirection.ReturnValue, string.Empty, DataRowVersion.Current, 0);
            BinnaclesDALC.InstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Create);
            BinnaclesDALC.InstanceDatabase.AddParameter(dbCmd, "@binnacleId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, -1);
            BinnaclesDALC.InstanceDatabase.AddParameter(dbCmd, "@userId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.UserId);
            BinnaclesDALC.InstanceDatabase.AddParameter(dbCmd, "@statusId", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.StatusId);
            BinnaclesDALC.InstanceDatabase.AddParameter(dbCmd, "@binnacleDescription", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.BinnacleDescription);
            BinnaclesDALC.InstanceDatabase.AddParameter(dbCmd, "@InitialDate", DbType.DateTime,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.InitialDate);
            BinnaclesDALC.InstanceDatabase.AddParameter(dbCmd, "@FinalDate", DbType.DateTime,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.FinalDate);
            BinnaclesDALC.InstanceDatabase.AddParameter(dbCmd, "@comment", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.Comment);
            BinnaclesDALC.InstanceDatabase.AddParameter(dbCmd, "@active", DbType.Boolean,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.Active);

            //Insert record:
            BinnaclesDALC.InstanceDatabase.ExecuteNonQuery(dbCmd);

            businessEntity.BinnacleId = Convert.ToInt32(dbCmd.Parameters["@Return"].Value);

            return Convert.ToInt32(dbCmd.Parameters["@Return"].Value);
        }

        public override void update(BinnaclesInfo businessEntity)
        {
            DbCommand dbCmd = BinnaclesDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDBinnacles");

            // Add the parameters:
            BinnaclesDALC.InstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Update);
            BinnaclesDALC.InstanceDatabase.AddParameter(dbCmd, "@binnacleId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.BinnacleId);
            BinnaclesDALC.InstanceDatabase.AddParameter(dbCmd, "@userId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.UserId);
            BinnaclesDALC.InstanceDatabase.AddParameter(dbCmd, "@statusId", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.StatusId);
            BinnaclesDALC.InstanceDatabase.AddParameter(dbCmd, "@binnacleDescription", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.BinnacleDescription);
            BinnaclesDALC.InstanceDatabase.AddParameter(dbCmd, "@InitialDate", DbType.DateTime,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.InitialDate);
            BinnaclesDALC.InstanceDatabase.AddParameter(dbCmd, "@FinalDate", DbType.DateTime,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.FinalDate);
            BinnaclesDALC.InstanceDatabase.AddParameter(dbCmd, "@comment", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.Comment);
            BinnaclesDALC.InstanceDatabase.AddParameter(dbCmd, "@active", DbType.Boolean,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.Active);

            //Update record:
            BinnaclesDALC.InstanceDatabase.ExecuteNonQuery(dbCmd);
        }

         #endregion

        #region Protected Methods

        protected override BinnaclesInfo getFromDataReader(IDataReader current)
        {
            BinnaclesInfo record = new BinnaclesInfo();

            record.BinnacleId = Convert.ToInt32(current["BinnacleId"]);
            record.UserId = Convert.ToInt32(current["UserId"]);
            record.StatusId = Convert.ToByte(current["StatusId"]);
            record.BinnacleDescription = current["BinnacleDescription"].ToString();
            record.Comment = current["Comment"].ToString();
            record.Active = Convert.ToBoolean(current["Active"]);
            record.InitialDate = Convert.ToDateTime(current["InitialDate"]);
            record.FinalDate = Convert.ToDateTime(current["FinalDate"]);
            //record.StatusName = current["StatusName"].ToString();

            return record;
        }

        #endregion

        public static readonly BinnaclesDALC Instance = new BinnaclesDALC();
    }
}
