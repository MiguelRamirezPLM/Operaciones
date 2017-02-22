using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using PLMClientsBusinessEntities;

namespace PLMClientsDataAccessComponent
{
    public sealed class UserDevicesDALC : PLMClientsDataAccessAdapter<UserDeviceInfo>
    {
        #region Constructors

        private UserDevicesDALC() { }

        #endregion

        #region Public Methods

        public override int insert(UserDeviceInfo businessEntity)
        {
            DbCommand dbCmd = UserDevicesDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDUserDevices");

            // Add the parameters:
            UserDevicesDALC.InstanceDatabase.AddParameter(dbCmd, "@Return", DbType.Int32,
                ParameterDirection.ReturnValue, string.Empty, DataRowVersion.Current, 0);
            UserDevicesDALC.InstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Create);
            UserDevicesDALC.InstanceDatabase.AddParameter(dbCmd, "@userId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.UserId);
            UserDevicesDALC.InstanceDatabase.AddParameter(dbCmd, "@codeId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.CodeId);
            UserDevicesDALC.InstanceDatabase.AddParameter(dbCmd, "@deviceId", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.DeviceId);

            if (businessEntity.MacAddress != null)
                UserDevicesDALC.InstanceDatabase.AddParameter(dbCmd, "@macAddress", DbType.AnsiString,
                    ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.MacAddress);

            UserDevicesDALC.InstanceDatabase.AddParameter(dbCmd, "@active", DbType.Boolean,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.Active);

            
            //Insert record:
            UserDevicesDALC.InstanceDatabase.ExecuteNonQuery(dbCmd);

            return Convert.ToInt32(dbCmd.Parameters["@Return"].Value);  
        }

        public override void delete(UserDeviceInfo businessEntity)
        {
            DbCommand dbCmd = UserDevicesDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDUserDevices");

            // Add the parameters:
            UserDevicesDALC.InstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Delete);
            UserDevicesDALC.InstanceDatabase.AddParameter(dbCmd, "@userId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.UserId);
            UserDevicesDALC.InstanceDatabase.AddParameter(dbCmd, "@codeId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.CodeId);
            UserDevicesDALC.InstanceDatabase.AddParameter(dbCmd, "@deviceId", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.DeviceId);

            //Delete record:
            UserDevicesDALC.InstanceDatabase.ExecuteNonQuery(dbCmd);
        }

        #endregion

        #region Protected Methods

        protected override UserDeviceInfo getFromDataReader(IDataReader current)
        {
            UserDeviceInfo record = new UserDeviceInfo();

            record.UserId = Convert.ToInt32(current["UserId"]);
            record.CodeId = Convert.ToInt32(current["CodeId"]);
            record.DeviceId = Convert.ToByte(current["DeviceId"]);

            if (current["MacAddress"] != System.DBNull.Value)
                record.MacAddress = current["MacAddress"].ToString();

            if (current["AddedDate"] != System.DBNull.Value)
                record.AddedDate = Convert.ToDateTime(current["AddedDate"]);

            record.Active = Convert.ToBoolean(current["Active"]);

            return record;
        }


        #endregion

        public static readonly UserDevicesDALC Instance = new UserDevicesDALC();

    }
}
