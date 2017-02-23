using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using PLMUserBusinessEntries;

namespace PLMUsersDataAccessComponent
{
    public sealed class ActivitySessionsDALC : PLMUsersDataAccesAdapter<ActivitySessionInfo>
    {

        #region Constructors

        private ActivitySessionsDALC() { }

        #endregion

        #region Public Methods

        public override int insert(ActivitySessionInfo businessEntity)
        {
            DbCommand dbCmd = ActivitySessionsDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDActivitySessions");
            //Add the parameters: 
            ActivitySessionsDALC.InstanceDatabase.AddParameter(dbCmd, "@Return", DbType.Int32,
                ParameterDirection.ReturnValue, string.Empty, DataRowVersion.Current, 0);
            ActivitySessionsDALC.InstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, ActivitySessionsDALC.CRUD.Create);
            ActivitySessionsDALC.InstanceDatabase.AddParameter(dbCmd, "@applicationId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.ApplicationId);
            ActivitySessionsDALC.InstanceDatabase.AddParameter(dbCmd, "@userId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.UserId);
            
            //Insert Record:
            ActivitySessionsDALC.InstanceDatabase.ExecuteNonQuery(dbCmd);

            businessEntity.ActivitySessionId = Convert.ToInt32(dbCmd.Parameters["@Return"].Value);

            return businessEntity.ActivitySessionId;
            
        }

        #endregion

        public static readonly ActivitySessionsDALC Instance = new ActivitySessionsDALC();

    }
}
