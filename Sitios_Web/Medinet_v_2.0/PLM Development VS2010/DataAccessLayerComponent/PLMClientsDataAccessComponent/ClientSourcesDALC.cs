using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;

using PLMClientsBusinessEntities;

namespace PLMClientsDataAccessComponent
{
    public sealed class ClientSourcesDALC : PLMClientsDataAccessAdapter<ClientSourceInfo>
    {
        #region Constructors

        private ClientSourcesDALC() { }

        #endregion

        #region Public methods

        public override int insert(ClientSourceInfo businessEntity)
        {
            DbCommand dbCmd = ClientSourcesDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDClientSources");

            // Add the parameters:
            ClientSourcesDALC.InstanceDatabase.AddParameter(dbCmd, "@Return", DbType.Int32,
                ParameterDirection.ReturnValue, string.Empty, DataRowVersion.Current, 0);
            ClientSourcesDALC.InstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Create);
            ClientSourcesDALC.InstanceDatabase.AddParameter(dbCmd, "@clientId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ClientId);
            ClientSourcesDALC.InstanceDatabase.AddParameter(dbCmd, "@entrySourceId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.EntrySourceId);

            //Insert record:
            ClientSourcesDALC.InstanceDatabase.ExecuteNonQuery(dbCmd);

            return Convert.ToInt32(dbCmd.Parameters["@Return"].Value);   
        }

        public override void delete(ClientSourceInfo businessEntity)
        {
            DbCommand dbCmd = ClientSourcesDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDClientSources");

            // Add the parameters:
            ClientSourcesDALC.InstanceDatabase.AddParameter(dbCmd, "@Return", DbType.Int32,
                ParameterDirection.ReturnValue, string.Empty, DataRowVersion.Current, 0);
            ClientSourcesDALC.InstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Delete);
            ClientSourcesDALC.InstanceDatabase.AddParameter(dbCmd, "@clientId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ClientId);
            ClientSourcesDALC.InstanceDatabase.AddParameter(dbCmd, "@entrySourceId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.EntrySourceId);

            //Delete record:
            ClientSourcesDALC.InstanceDatabase.ExecuteNonQuery(dbCmd);
        }

        #endregion

        public static readonly ClientSourcesDALC Instance = new ClientSourcesDALC();
    }
}
