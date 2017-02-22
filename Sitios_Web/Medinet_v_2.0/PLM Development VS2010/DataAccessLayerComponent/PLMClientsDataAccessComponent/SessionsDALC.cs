using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using PLMClientsBusinessEntities;

namespace PLMClientsDataAccessComponent
{
    public sealed class SessionsDALC : PLMClientsDataAccessAdapter<UserSessionInfo>
    {
        #region Constructors

        private SessionsDALC() { }

        #endregion

        #region Public Methods

        public int checkSessionStatus(int userId, int sessionTime)
        {
            DbCommand dbCmd = SessionsDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spCheckStatus");

            //Add the parameters:
            SessionsDALC.InstanceDatabase.AddParameter(dbCmd, "@Return", DbType.Int32,
                ParameterDirection.ReturnValue, string.Empty, DataRowVersion.Current, 0);
            SessionsDALC.InstanceDatabase.AddParameter(dbCmd, "@userId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, userId);
            SessionsDALC.InstanceDatabase.AddParameter(dbCmd, "@sessionTime", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, sessionTime);

            SessionsDALC.InstanceDatabase.ExecuteNonQuery(dbCmd);

            return Convert.ToInt32(dbCmd.Parameters["@Return"].Value);

        }

        public List<UserSessionInfo> getByUserId(int userId)
        {
            DbCommand dbCmd = SessionsDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spGetSessionsByUser");

            // Add the parameters:
            SessionsDALC.InstanceDatabase.AddParameter(dbCmd, "@userId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, userId);

            List<UserSessionInfo> BECollection = new List<UserSessionInfo>();

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = SessionsDALC.InstanceDatabase.ExecuteReader(dbCmd))
            {
                while (dataReader.Read())
                    BECollection.Add(this.getFromDataReader(dataReader));
            }

            return BECollection;
        }

        public override int insert(UserSessionInfo businessEntity)
        {
            DbCommand dbCmd = SessionsDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDUserSessions");

            // Add the parameters:
            SessionsDALC.InstanceDatabase.AddParameter(dbCmd, "@Return", DbType.Int32,
                ParameterDirection.ReturnValue, string.Empty, DataRowVersion.Current, 0);
            SessionsDALC.InstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Create);
            SessionsDALC.InstanceDatabase.AddParameter(dbCmd, "@userSessionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, -1);
            SessionsDALC.InstanceDatabase.AddParameter(dbCmd, "@userId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.UserId);
            SessionsDALC.InstanceDatabase.AddParameter(dbCmd, "@sessionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.SessionId);
            
            //Insert record:
            SessionsDALC.InstanceDatabase.ExecuteNonQuery(dbCmd);

            return Convert.ToInt32(dbCmd.Parameters["@Return"].Value);   
        }

        #endregion

        #region Protected Methods

        protected override UserSessionInfo getFromDataReader(IDataReader current)
        {
            UserSessionInfo record = new UserSessionInfo();

            record.UserSessionId = Convert.ToInt32(current["UserSessionId"]);
            record.UserId = Convert.ToInt32(current["UserId"]);
            record.SessionId = Convert.ToInt32(current["SessionId"]);
            record.SessionDate = Convert.ToDateTime(current["SessionDate"]);

            return record;
        }

        #endregion

        public static readonly SessionsDALC Instance = new SessionsDALC();
    }
}
