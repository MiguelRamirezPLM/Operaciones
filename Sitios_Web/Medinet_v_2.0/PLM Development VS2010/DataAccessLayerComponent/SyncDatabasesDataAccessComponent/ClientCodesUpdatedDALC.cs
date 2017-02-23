using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using SyncDatabaseBusinessEntries;

namespace SyncDatabasesDataAccessComponent
{
    public class ClientCodesUpdatedDALC :SyncDatabasesDataAccessAdapter<ClientCodesUpdatedInfo>
    {
        #region Constructors

        private ClientCodesUpdatedDALC() { }

        #endregion

        #region Public Methods

        public List<ClientCodesUpdatedInfo> saveUpdates(ClientCodesUpdatedInfo[] updates)
        {
            string updatesId = "";

            foreach (ClientCodesUpdatedInfo update in updates)
            {
                if(updatesId != "")
                    updatesId = updatesId + "," + this.insert(update).ToString();
                else
                    updatesId = this.insert(update).ToString();
            }

            return this.getById(updatesId);
        }

        public override int insert(ClientCodesUpdatedInfo businessEntity)
        {
            DbCommand dbCmd = ClientCodesUpdatedDALC.SyncDatabaseInstance.GetStoredProcCommand("dbo.plm_spCRUDClientCodesUpdated");

            ClientCodesUpdatedDALC.SyncDatabaseInstance.AddParameter(dbCmd, "@Return", DbType.Int32,
                ParameterDirection.ReturnValue, string.Empty, DataRowVersion.Current, 0);
            ClientCodesUpdatedDALC.SyncDatabaseInstance.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Create);
            ClientCodesUpdatedDALC.SyncDatabaseInstance.AddParameter(dbCmd, "@clientCodeUpdId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, -1);
            ClientCodesUpdatedDALC.SyncDatabaseInstance.AddParameter(dbCmd, "@codeString", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.CodeString);
            ClientCodesUpdatedDALC.SyncDatabaseInstance.AddParameter(dbCmd, "@packSqlTextId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.PackSqlTextId);
            ClientCodesUpdatedDALC.SyncDatabaseInstance.AddParameter(dbCmd, "@sentDate", DbType.DateTime,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.SentDate);
            if (businessEntity.UpdateDate != null)
                ClientCodesUpdatedDALC.SyncDatabaseInstance.AddParameter(dbCmd, "@updateDate", DbType.DateTime,
                    ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.UpdateDate);

            ClientCodesUpdatedDALC.SyncDatabaseInstance.ExecuteNonQuery(dbCmd);

            businessEntity.ClientCodeUpdId = Convert.ToInt32(dbCmd.Parameters["@Return"].Value);

            return businessEntity.ClientCodeUpdId;       
        }

        public List<ClientCodesUpdatedInfo> getById(string ids)
        {
            DbCommand dbCmd = ClientCodesUpdatedDALC.SyncDatabaseInstance.GetStoredProcCommand("dbo.plm_spGetClientCodeUpdates");

            ClientCodesUpdatedDALC.SyncDatabaseInstance.AddParameter(dbCmd, "@ids", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, ids);
            
            List<ClientCodesUpdatedInfo> BECollection = new List<ClientCodesUpdatedInfo>();

            using (IDataReader dataReader = ClientCodesUpdatedDALC.SyncDatabaseInstance.ExecuteReader(dbCmd))
            {
                while (dataReader.Read())
                    BECollection.Add(this.getFromDataReader(dataReader));

            }

            return BECollection;
        }

        #endregion

        #region Protected Methods

        protected override ClientCodesUpdatedInfo getFromDataReader(IDataReader current)
        {
            ClientCodesUpdatedInfo record = new ClientCodesUpdatedInfo();

            record.ClientCodeUpdId = Convert.ToInt32(current["ClientCodeUpdId"]);
            record.CodeString = current["CodeString"].ToString();
            record.PackSqlTextId = Convert.ToInt32(current["PackSqlTextId"]);
            record.SentDate = Convert.ToDateTime(current["SentDate"]);

            if (current["UpdateDate"] != System.DBNull.Value)
                record.UpdateDate = Convert.ToDateTime(current["UpdateDate"]);

            record.Confirmed = Convert.ToBoolean(current["Confirmed"]);

            return record;
        }

        #endregion

        public static readonly ClientCodesUpdatedDALC Instance = new ClientCodesUpdatedDALC();
    }
}
