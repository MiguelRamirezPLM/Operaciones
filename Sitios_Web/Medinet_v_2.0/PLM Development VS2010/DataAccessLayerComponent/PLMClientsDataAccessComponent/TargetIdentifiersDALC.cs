using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using PLMClientsBusinessEntities;

namespace PLMClientsDataAccessComponent
{
    public class TargetIdentifiersDALC : PLMClientsDataAccessAdapter<TargetIdentifiersInfo>
    {

        #region Constructors

        private TargetIdentifiersDALC() { }

        #endregion

        #region Public Methods

        public override int insert(PLMClientsBusinessEntities.TargetIdentifiersInfo BEntity)
        {
            DbCommand dbCmd = TargetIdentifiersDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDTargetIdentifiers");

            // Add the parameters:
            TargetIdentifiersDALC.InstanceDatabase.AddParameter(dbCmd, "@Return", DbType.Int32,
                ParameterDirection.ReturnValue, string.Empty, DataRowVersion.Current, 0);
            TargetIdentifiersDALC.InstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Create);
            TargetIdentifiersDALC.InstanceDatabase.AddParameter(dbCmd, "@targetId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, BEntity.TargetId);
            TargetIdentifiersDALC.InstanceDatabase.AddParameter(dbCmd, "@deviceId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, BEntity.DeviceId);
            
            //Insert record:
            TargetIdentifiersDALC.InstanceDatabase.ExecuteNonQuery(dbCmd);

            return Convert.ToInt32(dbCmd.Parameters["@Return"].Value);
        }

        public override void delete(TargetIdentifiersInfo BEntity)
        {
            DbCommand dbCmd = TargetIdentifiersDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDTargetIdentifiers");

            // Add the parameters:
            TargetIdentifiersDALC.InstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Delete);
            TargetIdentifiersDALC.InstanceDatabase.AddParameter(dbCmd, "@targetId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, BEntity.TargetId);
            TargetIdentifiersDALC.InstanceDatabase.AddParameter(dbCmd, "@deviceId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, BEntity.DeviceId);

            //Delete record:
            TargetIdentifiersDALC.InstanceDatabase.ExecuteNonQuery(dbCmd);
        }

        public override TargetIdentifiersInfo getOne(TargetIdentifiersInfo BEntity)
        {
            DbCommand dbCmd = TargetIdentifiersDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDTargetIdentifiers");

            // Add the parameters:
            TargetIdentifiersDALC.InstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Read);
            TargetIdentifiersDALC.InstanceDatabase.AddParameter(dbCmd, "@targetId", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, BEntity.TargetId);
            TargetIdentifiersDALC.InstanceDatabase.AddParameter(dbCmd, "@deviceId", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, BEntity.DeviceId);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = TargetIdentifiersDALC.InstanceDatabase.ExecuteReader(dbCmd))
            {
                if (dataReader.Read())
                    return this.getFromDataReader(dataReader);
                else
                    return null;
            }
        }

        public PLMClientsBusinessEntities.TargetIdentifiersInfo getDeviceByTarget(byte targetId)
        {
            DbCommand dbCmd = TargetIdentifiersDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDTargetIdentifiers");

            // Add the parameters:
            TargetIdentifiersDALC.InstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Read);
            TargetIdentifiersDALC.InstanceDatabase.AddParameter(dbCmd, "@targetId", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, targetId);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = TargetIdentifiersDALC.InstanceDatabase.ExecuteReader(dbCmd))
            {
                if (dataReader.Read())
                    return this.getFromDataReader(dataReader);
                else
                    return null;
            }
        }

        #endregion

        #region Protected Methods

        protected override PLMClientsBusinessEntities.TargetIdentifiersInfo getFromDataReader(IDataReader current)
        {
            PLMClientsBusinessEntities.TargetIdentifiersInfo record = new PLMClientsBusinessEntities.TargetIdentifiersInfo();

            record.TargetId = Convert.ToByte(current["TargetId"]);
            record.DeviceId = Convert.ToByte(current["DeviceId"]);

            return record;
        }

        #endregion

        public static readonly TargetIdentifiersDALC Instance = new TargetIdentifiersDALC();

    }
}
