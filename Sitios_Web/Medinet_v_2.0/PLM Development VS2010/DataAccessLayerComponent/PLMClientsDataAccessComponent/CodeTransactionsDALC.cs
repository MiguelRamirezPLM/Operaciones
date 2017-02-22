using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using PLMClientsBusinessEntities;

namespace PLMClientsDataAccessComponent
{
    public sealed class CodeTransactionsDALC : PLMClientsDataAccessAdapter<CodeTransactionInfo>
    {
        #region Constructors

        private CodeTransactionsDALC() { }

        #endregion

        #region Public Methods

        public override int insert(CodeTransactionInfo businessEntity)
        {
            DbCommand dbCmd = CodeTransactionsDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDCodeTransactions");

            // Add the parameters:
            CodeTransactionsDALC.InstanceDatabase.AddParameter(dbCmd, "@Return", DbType.Int32,
                ParameterDirection.ReturnValue, string.Empty, DataRowVersion.Current, 0);
            CodeTransactionsDALC.InstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Create);
            CodeTransactionsDALC.InstanceDatabase.AddParameter(dbCmd, "@codeId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.CodeId);
            CodeTransactionsDALC.InstanceDatabase.AddParameter(dbCmd, "@transactionId", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.TransactionId);

            //Insert record:
            CodeTransactionsDALC.InstanceDatabase.ExecuteNonQuery(dbCmd);

            return Convert.ToInt32(dbCmd.Parameters["@Return"].Value); 
        
        }

        public override void delete(CodeTransactionInfo businessEntity)
        {
            DbCommand dbCmd = CodeTransactionsDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDCodeTransactions");

            // Add the parameters:
            CodeTransactionsDALC.InstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Delete);
            CodeTransactionsDALC.InstanceDatabase.AddParameter(dbCmd, "@codeId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.CodeId);
            CodeTransactionsDALC.InstanceDatabase.AddParameter(dbCmd, "@transactionId", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.TransactionId);

            //Delete record:
            CodeTransactionsDALC.InstanceDatabase.ExecuteNonQuery(dbCmd);

        }

        public CodeTransactionInfo getOne(CodeTransactionInfo businessEntity)
        {
            DbCommand dbCmd = CodeTransactionsDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDCodeTransactions");

            // Add the parameters:
            CodeTransactionsDALC.InstanceDatabase.AddParameter(dbCmd, "@Return", DbType.Int32,
                ParameterDirection.ReturnValue, string.Empty, DataRowVersion.Current, 0);
            CodeTransactionsDALC.InstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Read);
            CodeTransactionsDALC.InstanceDatabase.AddParameter(dbCmd, "@codeId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.CodeId);
            CodeTransactionsDALC.InstanceDatabase.AddParameter(dbCmd, "@transactionId", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.TransactionId);

            //Read record:
            using (IDataReader dataReader = CodeTransactionsDALC.InstanceDatabase.ExecuteReader(dbCmd))
            {
                if (dataReader.Read())
                {
                    return this.getFromDataReader(dataReader);
                }
                else
                {
                    return null;
                }
            }
        }

        #endregion

        #region Protected Methods

        protected override CodeTransactionInfo getFromDataReader(IDataReader current)
        {
            CodeTransactionInfo record = new CodeTransactionInfo();

            record.CodeId = Convert.ToInt32(current["CodeId"]);
            record.TransactionId = Convert.ToByte(current["TransactionId"]);
            record.TranDate = Convert.ToDateTime(current["TranDate"]);

            return record;
        }

        #endregion

        public static readonly CodeTransactionsDALC Instance = new CodeTransactionsDALC();

    }
}
