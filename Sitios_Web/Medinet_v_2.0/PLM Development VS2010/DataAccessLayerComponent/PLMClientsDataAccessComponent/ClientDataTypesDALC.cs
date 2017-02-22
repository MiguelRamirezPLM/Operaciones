using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using PLMClientsBusinessEntities;

namespace PLMClientsDataAccessComponent
{
    public class ClientDataTypesDALC : PLMClientsDataAccessAdapter<ClientDataTypesInfo>
    {

        #region Constructors

        private ClientDataTypesDALC() { }

        #endregion

        #region Public Methods

        public override int insert(ClientDataTypesInfo BEntity)
        {
            DbCommand dbCmd = ClientDataTypesDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDClientDataTypes");

            // Add the parameters:
            ClientDataTypesDALC.InstanceDatabase.AddParameter(dbCmd, "@Return", DbType.Int32,
                ParameterDirection.ReturnValue, string.Empty, DataRowVersion.Current, 0);
            ClientDataTypesDALC.InstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Create);
            ClientDataTypesDALC.InstanceDatabase.AddParameter(dbCmd, "@clientId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, BEntity.ClientId);
            ClientDataTypesDALC.InstanceDatabase.AddParameter(dbCmd, "@codeId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, BEntity.CodeId);
            ClientDataTypesDALC.InstanceDatabase.AddParameter(dbCmd, "@targetId", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, BEntity.TargetId);
            ClientDataTypesDALC.InstanceDatabase.AddParameter(dbCmd, "@deviceId", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, BEntity.DeviceId);
            ClientDataTypesDALC.InstanceDatabase.AddParameter(dbCmd, "@dataTypeId", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, BEntity.DataTypeId);
            ClientDataTypesDALC.InstanceDatabase.AddParameter(dbCmd, "@dataValue", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, BEntity.DataValue);

            //Insert record:
            ClientDataTypesDALC.InstanceDatabase.ExecuteNonQuery(dbCmd);

            return Convert.ToInt32(dbCmd.Parameters["@Return"].Value);
        }

        public override void update(ClientDataTypesInfo BEntity)
        {
            DbCommand dbCmd = ClientDataTypesDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDClientDataTypes");

            // Add the parameters:
            ClientDataTypesDALC.InstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Update);
            ClientDataTypesDALC.InstanceDatabase.AddParameter(dbCmd, "@clientId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, BEntity.ClientId);
            ClientDataTypesDALC.InstanceDatabase.AddParameter(dbCmd, "@codeId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, BEntity.CodeId);
            ClientDataTypesDALC.InstanceDatabase.AddParameter(dbCmd, "@targetId", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, BEntity.TargetId);
            ClientDataTypesDALC.InstanceDatabase.AddParameter(dbCmd, "@deviceId", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, BEntity.DeviceId);
            ClientDataTypesDALC.InstanceDatabase.AddParameter(dbCmd, "@dataTypeId", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, BEntity.DataTypeId);
            ClientDataTypesDALC.InstanceDatabase.AddParameter(dbCmd, "@dataValue", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, BEntity.DataValue);

            //Update record:
            ClientDataTypesDALC.InstanceDatabase.ExecuteNonQuery(dbCmd);
        }

        public override void delete(ClientDataTypesInfo BEntity)
        {
            DbCommand dbCmd = ClientDataTypesDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDClientDataTypes");

            // Add the parameters:
            ClientDataTypesDALC.InstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Delete);
            ClientDataTypesDALC.InstanceDatabase.AddParameter(dbCmd, "@clientId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, BEntity.ClientId);
            ClientDataTypesDALC.InstanceDatabase.AddParameter(dbCmd, "@codeId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, BEntity.CodeId);
            ClientDataTypesDALC.InstanceDatabase.AddParameter(dbCmd, "@targetId", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, BEntity.TargetId);
            ClientDataTypesDALC.InstanceDatabase.AddParameter(dbCmd, "@deviceId", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, BEntity.DeviceId);
            ClientDataTypesDALC.InstanceDatabase.AddParameter(dbCmd, "@dataTypeId", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, BEntity.DataTypeId);

            //Delete record:
            ClientDataTypesDALC.InstanceDatabase.ExecuteNonQuery(dbCmd);
        }

        #endregion

        public static readonly ClientDataTypesDALC Instance = new ClientDataTypesDALC();

    }
}
