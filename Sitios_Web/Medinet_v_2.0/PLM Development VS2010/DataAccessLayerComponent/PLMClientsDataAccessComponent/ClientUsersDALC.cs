using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using PLMClientsBusinessEntities;

namespace PLMClientsDataAccessComponent
{
    public sealed class ClientUsersDALC : PLMClientsDataAccessAdapter<ClientUserInfo>
    {
        #region Constructors

        private ClientUsersDALC() { }

        #endregion

        #region Public Methods

        public override int insert(ClientUserInfo businessEntity)
        {
            DbCommand dbCmd = ClientUsersDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDClientUsers");

            // Add the parameters:
            ClientUsersDALC.InstanceDatabase.AddParameter(dbCmd, "@Return", DbType.Int32,
                ParameterDirection.ReturnValue, string.Empty, DataRowVersion.Current, 0);
            ClientUsersDALC.InstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Create);
            ClientUsersDALC.InstanceDatabase.AddParameter(dbCmd, "@clientId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ClientId);
            ClientUsersDALC.InstanceDatabase.AddParameter(dbCmd, "@userId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.UserId);

            //Insert record:
            ClientUsersDALC.InstanceDatabase.ExecuteNonQuery(dbCmd);

            return Convert.ToInt32(dbCmd.Parameters["@Return"].Value);
 
        }

        public override void delete(ClientUserInfo businessEntity)
        {
            DbCommand dbCmd = ClientUsersDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDClientUsers");

            // Add the parameters:
            ClientUsersDALC.InstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Delete);
            ClientUsersDALC.InstanceDatabase.AddParameter(dbCmd, "@clientId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ClientId);
            ClientUsersDALC.InstanceDatabase.AddParameter(dbCmd, "@userId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.UserId);

            //Delete record:
            ClientUsersDALC.InstanceDatabase.ExecuteNonQuery(dbCmd);
        }

        #endregion

        #region Protected Methods

        protected override ClientUserInfo getFromDataReader(IDataReader current)
        {
            ClientUserInfo record = new ClientUserInfo();

            record.ClientId = Convert.ToInt32(current["ClientId"]);
            record.UserId = Convert.ToInt32(current["UserId"]);

            return record;
        }

        #endregion

        public static readonly ClientUsersDALC Instance = new ClientUsersDALC();

    }
}
