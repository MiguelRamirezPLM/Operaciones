using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using PLMUserBusinessEntries;

namespace PLMUsersDataAccessComponent
{
    public sealed class ErrorsDALC : PLMUsersDataAccesAdapter<ErrorInfo>
    {
        #region Constructors

        private ErrorsDALC() { }

        #endregion

        #region Public Methods

        public override int insert(ErrorInfo businessEntity)
        {
            DbCommand dbCmd = ErrorsDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDErrors");
            //Add the parameters: 
            ErrorsDALC.InstanceDatabase.AddParameter(dbCmd, "@Return", DbType.Int32,
                ParameterDirection.ReturnValue, string.Empty, DataRowVersion.Current, 0);
            ErrorsDALC.InstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, ErrorsDALC.CRUD.Create);
            ErrorsDALC.InstanceDatabase.AddParameter(dbCmd, "@applicationId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ApplicationId);
            ErrorsDALC.InstanceDatabase.AddParameter(dbCmd, "@folio", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.Folio);
            ErrorsDALC.InstanceDatabase.AddParameter(dbCmd, "@message", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.Message);
            ErrorsDALC.InstanceDatabase.AddParameter(dbCmd, "@stackTrace", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.StackTrace);
            ApplicationDALC.InstanceDatabase.AddParameter(dbCmd, "@status", DbType.Boolean,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.Status);

            //Insert Record:
            ErrorsDALC.InstanceDatabase.ExecuteNonQuery(dbCmd);

            businessEntity.ErrorId = Convert.ToInt32(dbCmd.Parameters["@Return"].Value);

            return businessEntity.ErrorId;

        }

        public override ErrorInfo getOne(int pk)
        {
            DbCommand dbCmd = ErrorsDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDErrors");

            // Add the parameters:
            ErrorsDALC.InstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, ApplicationDALC.CRUD.Read);
            ErrorsDALC.InstanceDatabase.AddParameter(dbCmd, "@errorId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, pk);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = ErrorsDALC.InstanceDatabase.ExecuteReader(dbCmd))
            {
                if (dataReader.Read())
                    return this.getFromDataReader(dataReader);
                else
                    return null;
            }
        }

        #endregion

        #region Protected Methods

        protected override ErrorInfo getFromDataReader(IDataReader current)
        {
            ErrorInfo record = new ErrorInfo();

            record.ErrorId = Convert.ToInt32(current["ErrorId"]);
            record.ApplicationId = Convert.ToInt32(current["ApplicationId"]);
            record.Folio = current["Folio"].ToString();
            record.Date = Convert.ToDateTime(current["Date"]);
            record.Message = current["Message"].ToString();
            record.StackTrace = current["StackTrace"].ToString();
            record.Status = Convert.ToBoolean(current["Status"]);

            return record;
        }

        #endregion

        public static readonly ErrorsDALC Instance = new ErrorsDALC();


    }
}
