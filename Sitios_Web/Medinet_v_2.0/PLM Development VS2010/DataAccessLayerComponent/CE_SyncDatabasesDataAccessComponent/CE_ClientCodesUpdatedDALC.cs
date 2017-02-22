using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using SyncDatabaseBusinessEntries;

namespace CE_SyncDatabasesDataAccessComponent
{
    public class CE_ClientCodesUpdatedDALC : CE_SyncDatabasesDataAccessAdapter<ClientCodesUpdatedInfo>
    {
        #region Constructors

        private CE_ClientCodesUpdatedDALC() { }

        #endregion

        #region Public Methods

        public override int insert(ClientCodesUpdatedInfo businessEntity)
        {
            CE_ClientCodesUpdatedDALC.SyncInstanceDatabase.CreateConnection();

            StringBuilder sb = new StringBuilder();

            sb.Append("\nINSERT INTO [ClientCodesUpdated]([CodeString],[PackSqlTextId],[SentDate],[UpdateDate],[Confirmed]) ");
            sb.Append("\nVALUES('" + businessEntity.CodeString + "'," + businessEntity.PackSqlTextId + ",'" + businessEntity.SentDate.ToShortDateString() + "' ");
            sb.Append(",GETDATE(),0) ");

            int result = CE_ClientCodesUpdatedDALC.SyncInstanceDatabase.ExecuteNonQuery(CommandType.Text, sb.ToString());

            CE_ClientCodesUpdatedDALC.SyncInstanceDatabase.CloseSharedConnection();

            return result;
        }

        public List<ClientCodesUpdatedInfo> getUpdatesExecuted(string code)
        {
            CE_ClientCodesUpdatedDALC.SyncInstanceDatabase.CreateConnection();

            StringBuilder sb = new StringBuilder();

            sb.Append("\nSelect ClientCodeUpdId, CodeString, PackSqlTextId, SentDate, UpdateDate, Confirmed ");
            sb.Append("\nFrom ClientCodesUpdated ");
            sb.Append("\nWhere Confirmed = 0 And CodeString = '" + code + "' ");
            sb.Append("\nOrder By ClientCodeUpdId ");

            List<ClientCodesUpdatedInfo> BECollection = new List<ClientCodesUpdatedInfo>();

            using(IDataReader dataReader = CE_ClientCodesUpdatedDALC.SyncInstanceDatabase.ExecuteReader(CommandType.Text, sb.ToString()))
            {
                while(dataReader.Read())
                    BECollection.Add(this.getFromDataReader(dataReader));
            }
            
            CE_ClientCodesUpdatedDALC.SyncInstanceDatabase.CloseSharedConnection();

            return BECollection;
        }

        public override void update(ClientCodesUpdatedInfo businessEntity)
        {
            CE_ClientCodesUpdatedDALC.SyncInstanceDatabase.CreateConnection();

            StringBuilder sb = new StringBuilder();

            sb.Append("\nUpdate ClientCodesUpdated ");
            sb.Append("\nSet Confirmed = 1 ");
            sb.Append("\nWhere PackSqlTextId = " + businessEntity.PackSqlTextId + " and CodeString = '" + businessEntity.CodeString + "' ");

            CE_ClientCodesUpdatedDALC.SyncInstanceDatabase.ExecuteNonQuery(CommandType.Text, sb.ToString());

            CE_ClientCodesUpdatedDALC.SyncInstanceDatabase.CloseSharedConnection();

        }

        public override void delete(int pk)
        {
            CE_ClientCodesUpdatedDALC.SyncInstanceDatabase.CreateConnection();

            StringBuilder sb = new StringBuilder();

            sb.Append("\nDelete ClientCodesUpdated Where ClientCodeUpdId = " + pk);

            CE_ClientCodesUpdatedDALC.SyncInstanceDatabase.ExecuteNonQuery(CommandType.Text, sb.ToString());

            CE_ClientCodesUpdatedDALC.SyncInstanceDatabase.CloseSharedConnection();

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

        public static readonly CE_ClientCodesUpdatedDALC Instance = new CE_ClientCodesUpdatedDALC();
    }
}
