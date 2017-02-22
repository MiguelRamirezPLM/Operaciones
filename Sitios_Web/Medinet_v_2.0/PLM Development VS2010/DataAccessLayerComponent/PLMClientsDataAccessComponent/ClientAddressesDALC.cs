using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using PLMClientsBusinessEntities;

namespace PLMClientsDataAccessComponent
{
    public sealed class ClientAddressesDALC : PLMClientsDataAccessAdapter<ClientAddressInfo>
    {
        #region Construtors

        private ClientAddressesDALC() { }

        #endregion

        #region Public Methods

        public override int insert(ClientAddressInfo businessEntity)
        {
            DbCommand dbCmd = ClientAddressesDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDClientAddresses");

            // Add the parameters:
            ClientAddressesDALC.InstanceDatabase.AddParameter(dbCmd, "@Return", DbType.Int32,
                ParameterDirection.ReturnValue, string.Empty, DataRowVersion.Current, 0);
            ClientAddressesDALC.InstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Create);
            ClientAddressesDALC.InstanceDatabase.AddParameter(dbCmd, "@clientId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ClientId);
            ClientAddressesDALC.InstanceDatabase.AddParameter(dbCmd, "@addressId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.AddressId);

            //Insert record:
            ClientAddressesDALC.InstanceDatabase.ExecuteNonQuery(dbCmd);

            return Convert.ToInt32(dbCmd.Parameters["@Return"].Value); 
        }

        public override void delete(ClientAddressInfo businessEntity)
        {
            DbCommand dbCmd = ClientAddressesDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDClientAddresses");

            // Add the parameters:
           
            ClientAddressesDALC.InstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Delete);
            ClientAddressesDALC.InstanceDatabase.AddParameter(dbCmd, "@clientId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ClientId);
            ClientAddressesDALC.InstanceDatabase.AddParameter(dbCmd, "@addressId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.AddressId);

            //Delete record:
            ClientAddressesDALC.InstanceDatabase.ExecuteNonQuery(dbCmd);
        }

        public override void delete(int clientId)
        {
            DbCommand dbCmd = ClientAddressesDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDClientAddresses");

            // Add the parameters:

            ClientAddressesDALC.InstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Delete);
            ClientAddressesDALC.InstanceDatabase.AddParameter(dbCmd, "@clientId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, clientId);

            //Delete record:
            ClientAddressesDALC.InstanceDatabase.ExecuteNonQuery(dbCmd);
        }

        public ClientAddressInfo getOne(int clientId, int addressId)
        {
            DbCommand dbCmd = ClientAddressesDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDClientAddresses");

            // Add the parameters:
            ClientAddressesDALC.InstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Read);
            ClientAddressesDALC.InstanceDatabase.AddParameter(dbCmd, "@clientId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, clientId);
            ClientAddressesDALC.InstanceDatabase.AddParameter(dbCmd, "@addressId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, addressId);
            
            // Get the result set from the stored procedure:
            using (IDataReader dataReader = ClientsDALC.InstanceDatabase.ExecuteReader(dbCmd))
            {
                if (dataReader.Read())
                    return this.getFromDataReader(dataReader);

                else
                    return null;
            }
        }

        #endregion

        #region Protected  Methods

        protected override ClientAddressInfo getFromDataReader(IDataReader current)
        {
            ClientAddressInfo record = new ClientAddressInfo();

            record.ClientId = Convert.ToInt32(current["ClientId"]);
            record.AddressId = Convert.ToInt32(current["AddressId"]);

            return record;

        }

        #endregion

        public static readonly ClientAddressesDALC Instance = new ClientAddressesDALC();

    }
}
