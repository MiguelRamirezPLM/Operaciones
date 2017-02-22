using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using PLMClientsBusinessEntities;

namespace PLMClientsDataAccessComponent
{
    public class ClientBodyMassIndexesDALC : PLMClientsDataAccessAdapter<ClientBodyMassIndexesInfo>
    {

        #region Constructors

        private ClientBodyMassIndexesDALC() { }

        #endregion

        #region Public Methods

        public override int insert(ClientBodyMassIndexesInfo BEntity)
        {
            DbCommand dbCmd = ClientBodyMassIndexesDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDClientBodyMassIndexes");

            // Add the parameters:
            ClientBodyMassIndexesDALC.InstanceDatabase.AddParameter(dbCmd, "@Return", DbType.Int32,
                ParameterDirection.ReturnValue, string.Empty, DataRowVersion.Current, 0);
            ClientBodyMassIndexesDALC.InstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Create);
            ClientBodyMassIndexesDALC.InstanceDatabase.AddParameter(dbCmd, "@clientId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, BEntity.ClientId);
            ClientBodyMassIndexesDALC.InstanceDatabase.AddParameter(dbCmd, "@codeId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, BEntity.CodeId);
            ClientBodyMassIndexesDALC.InstanceDatabase.AddParameter(dbCmd, "@targetId", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, BEntity.TargetId);
            ClientBodyMassIndexesDALC.InstanceDatabase.AddParameter(dbCmd, "@deviceId", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, BEntity.DeviceId);
            ClientBodyMassIndexesDALC.InstanceDatabase.AddParameter(dbCmd, "@indexId", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, BEntity.IndexId);

            //Insert record:
            ClientBodyMassIndexesDALC.InstanceDatabase.ExecuteNonQuery(dbCmd);

            return Convert.ToInt32(dbCmd.Parameters["@Return"].Value);
        }

        public override void delete(ClientBodyMassIndexesInfo BEntity)
        {
            DbCommand dbCmd = ClientBodyMassIndexesDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDClientBodyMassIndexes");

            // Add the parameters:
            ClientBodyMassIndexesDALC.InstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Delete);
            ClientBodyMassIndexesDALC.InstanceDatabase.AddParameter(dbCmd, "@clientId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, BEntity.ClientId);
            ClientBodyMassIndexesDALC.InstanceDatabase.AddParameter(dbCmd, "@codeId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, BEntity.CodeId);
            ClientBodyMassIndexesDALC.InstanceDatabase.AddParameter(dbCmd, "@targetId", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, BEntity.TargetId);
            ClientBodyMassIndexesDALC.InstanceDatabase.AddParameter(dbCmd, "@deviceId", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, BEntity.DeviceId);
            ClientBodyMassIndexesDALC.InstanceDatabase.AddParameter(dbCmd, "@indexId", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, BEntity.IndexId);

            //Delete record:
            ClientBodyMassIndexesDALC.InstanceDatabase.ExecuteNonQuery(dbCmd);
        }

        #endregion

        public static readonly ClientBodyMassIndexesDALC Instance = new ClientBodyMassIndexesDALC();

    }
}
