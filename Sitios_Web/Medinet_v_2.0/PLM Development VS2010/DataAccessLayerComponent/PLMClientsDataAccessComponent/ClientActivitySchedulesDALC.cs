using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using PLMClientsBusinessEntities;

namespace PLMClientsDataAccessComponent
{
    public class ClientActivitySchedulesDALC : PLMClientsDataAccessAdapter<ClientActivitySchedulesInfo>
    {

        #region Constructors

        private ClientActivitySchedulesDALC() { }

        #endregion

        #region Public Methods

        public override int insert(ClientActivitySchedulesInfo BEntity)
        {
            DbCommand dbCmd = ClientActivitySchedulesDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDClientActivitySchedules");

            // Add the parameters:
            ClientActivitySchedulesDALC.InstanceDatabase.AddParameter(dbCmd, "@Return", DbType.Int32,
                ParameterDirection.ReturnValue, string.Empty, DataRowVersion.Current, 0);
            ClientActivitySchedulesDALC.InstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Create);
            ClientActivitySchedulesDALC.InstanceDatabase.AddParameter(dbCmd, "@clientId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, BEntity.ClientId);
            ClientActivitySchedulesDALC.InstanceDatabase.AddParameter(dbCmd, "@codeId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, BEntity.CodeId);
            ClientActivitySchedulesDALC.InstanceDatabase.AddParameter(dbCmd, "@targetId", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, BEntity.TargetId);
            ClientActivitySchedulesDALC.InstanceDatabase.AddParameter(dbCmd, "@deviceId", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, BEntity.DeviceId);
            ClientActivitySchedulesDALC.InstanceDatabase.AddParameter(dbCmd, "@activityTypeId", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, BEntity.ActivityTypeId);
            ClientActivitySchedulesDALC.InstanceDatabase.AddParameter(dbCmd, "@activitySchedule", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, BEntity.ActivitySchedule);

            //Insert record:
            ClientActivitySchedulesDALC.InstanceDatabase.ExecuteNonQuery(dbCmd);

            return Convert.ToInt32(dbCmd.Parameters["@Return"].Value);
        }

        public override void update(ClientActivitySchedulesInfo BEntity)
        {
            DbCommand dbCmd = ClientActivitySchedulesDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDClientActivitySchedules");

            // Add the parameters:
            ClientActivitySchedulesDALC.InstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Update);
            ClientActivitySchedulesDALC.InstanceDatabase.AddParameter(dbCmd, "@clientId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, BEntity.ClientId);
            ClientActivitySchedulesDALC.InstanceDatabase.AddParameter(dbCmd, "@codeId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, BEntity.CodeId);
            ClientActivitySchedulesDALC.InstanceDatabase.AddParameter(dbCmd, "@targetId", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, BEntity.TargetId);
            ClientActivitySchedulesDALC.InstanceDatabase.AddParameter(dbCmd, "@deviceId", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, BEntity.DeviceId);
            ClientActivitySchedulesDALC.InstanceDatabase.AddParameter(dbCmd, "@activityTypeId", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, BEntity.ActivityTypeId);
            ClientActivitySchedulesDALC.InstanceDatabase.AddParameter(dbCmd, "@activitySchedule", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, BEntity.ActivitySchedule);

            //Update record:
            ClientActivitySchedulesDALC.InstanceDatabase.ExecuteNonQuery(dbCmd);
        }

        public override void delete(ClientActivitySchedulesInfo BEntity)
        {
            DbCommand dbCmd = ClientActivitySchedulesDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDClientActivitySchedules");

            // Add the parameters:
            ClientActivitySchedulesDALC.InstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Delete);
            ClientActivitySchedulesDALC.InstanceDatabase.AddParameter(dbCmd, "@clientId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, BEntity.ClientId);
            ClientActivitySchedulesDALC.InstanceDatabase.AddParameter(dbCmd, "@codeId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, BEntity.CodeId);
            ClientActivitySchedulesDALC.InstanceDatabase.AddParameter(dbCmd, "@targetId", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, BEntity.TargetId);
            ClientActivitySchedulesDALC.InstanceDatabase.AddParameter(dbCmd, "@deviceId", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, BEntity.DeviceId);
            ClientActivitySchedulesDALC.InstanceDatabase.AddParameter(dbCmd, "@activityTypeId", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, BEntity.ActivityTypeId);

            //Delete record:
            ClientActivitySchedulesDALC.InstanceDatabase.ExecuteNonQuery(dbCmd);
        }

        #endregion

        public static readonly ClientActivitySchedulesDALC Instance = new ClientActivitySchedulesDALC();

    }
}
