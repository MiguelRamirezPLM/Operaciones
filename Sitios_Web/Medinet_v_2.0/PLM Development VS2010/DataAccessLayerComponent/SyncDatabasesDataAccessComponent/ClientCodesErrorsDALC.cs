using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using SyncDatabaseBusinessEntries;

namespace SyncDatabasesDataAccessComponent
{
    public class ClientCodesErrorsDALC : SyncDatabasesDataAccessAdapter<ClientCodesErrorInfo>
    {
        #region Constructors

        private ClientCodesErrorsDALC() { }

        #endregion

        #region Public Methods

        public List<ClientCodesErrorInfo> saveErrors(ClientCodesErrorInfo[] errors)
        {
            string errorsId = "";

            foreach (ClientCodesErrorInfo error in errors)
            {
                if (errorsId != "")
                    errorsId = errorsId + "," + this.insert(error).ToString();
                else
                    errorsId = this.insert(error).ToString();
            }

            return this.getById(errorsId);
        }

        public override int insert(ClientCodesErrorInfo businessEntity)
        {
            DbCommand dbCmd = ClientCodesErrorsDALC.SyncDatabaseInstance.GetStoredProcCommand("dbo.plm_spCRUDClientCodesErrors");

            ClientCodesErrorsDALC.SyncDatabaseInstance.AddParameter(dbCmd, "@Return", DbType.Int32,
                ParameterDirection.ReturnValue, string.Empty, DataRowVersion.Current, 0);
            ClientCodesErrorsDALC.SyncDatabaseInstance.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Create);
            ClientCodesErrorsDALC.SyncDatabaseInstance.AddParameter(dbCmd, "@ClientCodeErrorId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, -1);
            ClientCodesErrorsDALC.SyncDatabaseInstance.AddParameter(dbCmd, "@codeString", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.CodeString);
            ClientCodesErrorsDALC.SyncDatabaseInstance.AddParameter(dbCmd, "@packSqlTextId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.PackSqlTextId);
            ClientCodesErrorsDALC.SyncDatabaseInstance.AddParameter(dbCmd, "@errorMessage", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ErrorMessage);
            ClientCodesErrorsDALC.SyncDatabaseInstance.AddParameter(dbCmd, "@errorDate", DbType.DateTime,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ErrorDate);

            ClientCodesErrorsDALC.SyncDatabaseInstance.ExecuteNonQuery(dbCmd);

            businessEntity.ClientCodeErrorId = Convert.ToInt32(dbCmd.Parameters["@Return"].Value);

            return businessEntity.ClientCodeErrorId;
        }

        public List<ClientCodesErrorInfo> getById(string ids)
        {
            DbCommand dbCmd = ClientCodesErrorsDALC.SyncDatabaseInstance.GetStoredProcCommand("dbo.plm_spGetClientCodesErrors");

            ClientCodesErrorsDALC.SyncDatabaseInstance.AddParameter(dbCmd, "@ids", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, ids);

            List<ClientCodesErrorInfo> BECollection = new List<ClientCodesErrorInfo>();

            using (IDataReader dataReader = ClientCodesErrorsDALC.SyncDatabaseInstance.ExecuteReader(dbCmd))
            {
                while (dataReader.Read())
                    BECollection.Add(this.getFromDataReader(dataReader));

            }

            return BECollection;
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

        public static readonly ClientCodesErrorsDALC Instance = new ClientCodesErrorsDALC();

    }
}
