using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using PLMUserBusinessEntries;

namespace PLMUsersDataAccessComponent
{
    public class ActivityLogsDALC : PLMUsersDataAccesAdapter<ActivityLogInfo>
    {
        
        #region Constructors

        private ActivityLogsDALC() { }

        #endregion

        #region Public Methods

        public override int insert(ActivityLogInfo businessEntity)
        {
            DbCommand dbCmd = ActivityLogsDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDActivityLogs");
            //Add the parameters: 
            ActivityLogsDALC.InstanceDatabase.AddParameter(dbCmd, "@Return", DbType.Int32,
                ParameterDirection.ReturnValue, string.Empty, DataRowVersion.Current, 0);
            ActivityLogsDALC.InstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, ActivityLogsDALC.CRUD.Create);
            ActivityLogsDALC.InstanceDatabase.AddParameter(dbCmd, "@userId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.UserId);
            ActivityLogsDALC.InstanceDatabase.AddParameter(dbCmd, "@tableId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.TableId);
            ActivityLogsDALC.InstanceDatabase.AddParameter(dbCmd, "@operationId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.OperationId);
            ActivityLogsDALC.InstanceDatabase.AddParameter(dbCmd, "@primaryKeyAffected", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.PrimaryKeyAffected);
            ActivityLogsDALC.InstanceDatabase.AddParameter(dbCmd, "@fieldsAffected", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.FieldsAffected);
            ApplicationDALC.InstanceDatabase.AddParameter(dbCmd, "@hashKey", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.HashKey);

            //Insert Record:
            ActivityLogsDALC.InstanceDatabase.ExecuteNonQuery(dbCmd);

            businessEntity.ActivityLogId = Convert.ToInt32(dbCmd.Parameters["@Return"].Value);

            return businessEntity.ActivityLogId;
            
        }

        #endregion



        public static readonly ActivityLogsDALC Instance = new ActivityLogsDALC();

    }
}
