using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using SyncDatabaseBusinessEntries;

namespace CE_SyncDatabasesDataAccessComponent
{
    public class CE_ClientCodesErrorsDALC : CE_SyncDatabasesDataAccessAdapter<ClientCodesErrorInfo>
    {
        #region Constructors

        private CE_ClientCodesErrorsDALC() { }

        #endregion

        #region Public Methods

        public override int insert(ClientCodesErrorInfo businessEntity)
        {
            CE_ClientCodesErrorsDALC.SyncInstanceDatabase.CreateConnection();

            StringBuilder sb = new StringBuilder();

            sb.Append("\nINSERT INTO [ClientCodesErrors]([CodeString],[PackSqlTextId],[ErrorMessage],[ErrorDate],[Confirmed]) ");
            sb.Append("\nVALUES('" + businessEntity.CodeString + "'," + businessEntity.PackSqlTextId + ",'" + businessEntity.ErrorMessage + "' ");
            sb.Append(",'" +  businessEntity.ErrorDate + "',0) ");

            int result = CE_ClientCodesErrorsDALC.SyncInstanceDatabase.ExecuteNonQuery(CommandType.Text, sb.ToString());

            CE_ClientCodesErrorsDALC.SyncInstanceDatabase.CloseSharedConnection();

            return result;
        }

        public List<ClientCodesErrorInfo> getErrors(string code)
        {
            CE_ClientCodesErrorsDALC.SyncInstanceDatabase.CreateConnection();

            StringBuilder sb = new StringBuilder();

            sb.Append("\nSelect ClientCodeErrorId, CodeString, PackSqlTextId, ErrorMessage, ErrorDate, Confirmed ");
            sb.Append("\nFrom ClientCodesErrors ");
            sb.Append("\nWhere Confirmed = 0 And CodeString = '" + code + "' ");
            sb.Append("\nOrder By ClientCodeErrorId ");

            List<ClientCodesErrorInfo> BECollection = new List<ClientCodesErrorInfo>();

            using(IDataReader dataReader = CE_ClientCodesErrorsDALC.SyncInstanceDatabase.ExecuteReader(CommandType.Text, sb.ToString()))
            {
                while(dataReader.Read())
                    BECollection.Add(this.getFromDataReader(dataReader));
            }
            
            CE_ClientCodesErrorsDALC.SyncInstanceDatabase.CloseSharedConnection();

            return BECollection;
        }

        public override void update(ClientCodesErrorInfo businessEntity)
        {
            CE_ClientCodesErrorsDALC.SyncInstanceDatabase.CreateConnection();

            StringBuilder sb = new StringBuilder();

            sb.Append("\nUpdate ClientCodesErrors ");
            sb.Append("\nSet Confirmed = 1 ");
            sb.Append("\nWhere PackSqlTextId = " + businessEntity.PackSqlTextId + " and CodeString = '" + businessEntity.CodeString + "' ");

            CE_ClientCodesErrorsDALC.SyncInstanceDatabase.ExecuteNonQuery(CommandType.Text, sb.ToString());
        }

        public override void delete(int pk)
        {
            CE_ClientCodesErrorsDALC.SyncInstanceDatabase.CreateConnection();

            StringBuilder sb = new StringBuilder();

            sb.Append("\nDelete ClientCodesErrors Where ClientCodeErrorId = " + pk);

            CE_ClientCodesErrorsDALC.SyncInstanceDatabase.ExecuteNonQuery(CommandType.Text, sb.ToString());

            CE_ClientCodesErrorsDALC.SyncInstanceDatabase.CloseSharedConnection();
        }

        #endregion

        #region Protected Methods

        protected override ClientCodesErrorInfo getFromDataReader(IDataReader current)
        {
            ClientCodesErrorInfo record = new ClientCodesErrorInfo();

            record.ClientCodeErrorId = Convert.ToInt32(current["ClientCodeErrorId"]);
            record.CodeString = current["CodeString"].ToString();
            record.PackSqlTextId = Convert.ToInt32(current["PackSqlTextId"]);
            record.ErrorMessage = current["ErrorMessage"].ToString();
            record.ErrorDate = Convert.ToDateTime(current["ErrorDate"]);
            record.Confirmed = Convert.ToBoolean(current["Confirmed"]);

            return record;
        }

        #endregion

        public static readonly CE_ClientCodesErrorsDALC Instance = new CE_ClientCodesErrorsDALC();
    }
}
