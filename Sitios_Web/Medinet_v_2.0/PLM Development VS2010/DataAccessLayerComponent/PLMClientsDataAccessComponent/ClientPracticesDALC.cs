using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using PLMClientsBusinessEntities;

namespace PLMClientsDataAccessComponent
{
    public sealed class ClientPracticesDALC : PLMClientsDataAccessAdapter<ClientPracticeInfo>
    {
        #region Constructors

        private ClientPracticesDALC() { }

        #endregion

        #region Public Methods

        public override int insert(ClientPracticeInfo businessEntity)
        {
            DbCommand dbCmd = ClientPracticesDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDClientPractices");

            // Add the parameters:
            ClientPracticesDALC.InstanceDatabase.AddParameter(dbCmd, "@Return", DbType.Int32,
                ParameterDirection.ReturnValue, string.Empty, DataRowVersion.Current, 0);
            ClientPracticesDALC.InstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Create);
            ClientPracticesDALC.InstanceDatabase.AddParameter(dbCmd, "@clientId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ClientId);
            ClientPracticesDALC.InstanceDatabase.AddParameter(dbCmd, "@practiceId", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.PracticeId);
            
            //Insert record:
            ClientPracticesDALC.InstanceDatabase.ExecuteNonQuery(dbCmd);

            return Convert.ToInt32(dbCmd.Parameters["@Return"].Value);   
        }

        public override void delete(ClientPracticeInfo businessEntity)
        {
            DbCommand dbCmd = ClientPracticesDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDClientPractices");

            // Add the parameters:
            ClientPracticesDALC.InstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Delete);
            ClientPracticesDALC.InstanceDatabase.AddParameter(dbCmd, "@clientId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ClientId);
            ClientPracticesDALC.InstanceDatabase.AddParameter(dbCmd, "@practiceId", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.PracticeId);

            //Delete record:
            ClientPracticesDALC.InstanceDatabase.ExecuteNonQuery(dbCmd); 
        }

        public override void delete(int clientId)
        {
            DbCommand dbCmd = ClientPracticesDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDClientPractices");

            // Add the parameters:
            ClientPracticesDALC.InstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Delete);
            ClientPracticesDALC.InstanceDatabase.AddParameter(dbCmd, "@clientId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, clientId);

            //Delete record:
            ClientPracticesDALC.InstanceDatabase.ExecuteNonQuery(dbCmd); 
        }

        public List<ClientPracticeInfo> getByClient(int clientId)
        {
            DbCommand dbCmd = ClientPracticesDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spGetPracticesByClient");

            // Add the parameters:
            ClientPracticesDALC.InstanceDatabase.AddParameter(dbCmd, "@clientId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, clientId);

            List<ClientPracticeInfo> BECollection = new List<ClientPracticeInfo>();

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = ClientPracticesDALC.InstanceDatabase.ExecuteReader(dbCmd))
            {
                while(dataReader.Read())
                    BECollection.Add(this.getFromDataReader(dataReader));
            }

            return BECollection;

        }

        public bool checkPractice(int clientId, byte practiceId)
        {
            DbCommand dbCmd = ClientPracticesDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spGetPracticesByClient");

            // Add the parameters:
            ClientPracticesDALC.InstanceDatabase.AddParameter(dbCmd, "@Return", DbType.Int32,
                ParameterDirection.ReturnValue, string.Empty, DataRowVersion.Current, 0);
            ClientPracticesDALC.InstanceDatabase.AddParameter(dbCmd, "@clientId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, clientId);
            ClientPracticesDALC.InstanceDatabase.AddParameter(dbCmd, "@practiceId", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, practiceId);
            
            ClientPracticesDALC.InstanceDatabase.ExecuteNonQuery(dbCmd);

            return Convert.ToInt32(dbCmd.Parameters["@Return"].Value) > 0;
        }


        #endregion

        #region Protected Methods

        protected override ClientPracticeInfo getFromDataReader(IDataReader current)
        {
            ClientPracticeInfo record = new ClientPracticeInfo();

            record.ClientId = Convert.ToInt32(current["ClientId"]);
            record.PracticeId = Convert.ToByte(current["PracticeId"]);

            return record;
        }

        #endregion

        public static readonly ClientPracticesDALC Instance = new ClientPracticesDALC();

    }
}
