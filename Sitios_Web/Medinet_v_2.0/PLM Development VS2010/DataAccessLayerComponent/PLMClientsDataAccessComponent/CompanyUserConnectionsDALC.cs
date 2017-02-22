using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using PLMClientsBusinessEntities;

namespace PLMClientsDataAccessComponent
{
    public class CompanyUserConnectionsDALC : PLMClientsDataAccessAdapter<CompanyUserConnectionsInfo>
    {

        #region Constructors

        private CompanyUserConnectionsDALC() { }

        #endregion

        #region Public Methods

        public override int insert(PLMClientsBusinessEntities.CompanyUserConnectionsInfo BEntity)
        {
            DbCommand dbCmd = CompanyUserConnectionsDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDCompanyUserConnections");

            // Add the parameters:
            CompanyUserConnectionsDALC.InstanceDatabase.AddParameter(dbCmd, "@Return", DbType.Int32,
                ParameterDirection.ReturnValue, string.Empty, DataRowVersion.Current, 0);
            CompanyUserConnectionsDALC.InstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Create);
            CompanyUserConnectionsDALC.InstanceDatabase.AddParameter(dbCmd, "@userConnectionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, -1);
            CompanyUserConnectionsDALC.InstanceDatabase.AddParameter(dbCmd, "@distributionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, BEntity.DistributionId);
            CompanyUserConnectionsDALC.InstanceDatabase.AddParameter(dbCmd, "@companyUserId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, BEntity.CompanyUserId);
            CompanyUserConnectionsDALC.InstanceDatabase.AddParameter(dbCmd, "@codeId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, BEntity.CodeId);
            CompanyUserConnectionsDALC.InstanceDatabase.AddParameter(dbCmd, "@sessionId", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, BEntity.SessionId);
            
            if (BEntity.IP != null)
                CompanyUserConnectionsDALC.InstanceDatabase.AddParameter(dbCmd, "@ip", DbType.AnsiString,
                    ParameterDirection.Input, string.Empty, DataRowVersion.Current, BEntity.IP);

            CompanyUserConnectionsDALC.InstanceDatabase.ExecuteNonQuery(dbCmd);

            return Convert.ToInt32(dbCmd.Parameters["@Return"].Value);
        }

        public override PLMClientsBusinessEntities.CompanyUserConnectionsInfo getOne(int userConnectionId)
        {
            DbCommand dbCmd = CompanyUserConnectionsDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDCompanyUserConnections");

            // Add the parameters:
            CompanyUserConnectionsDALC.InstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Read);
            CompanyUserConnectionsDALC.InstanceDatabase.AddParameter(dbCmd, "@userConnectionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, userConnectionId);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = CompanyUserConnectionsDALC.InstanceDatabase.ExecuteReader(dbCmd))
            {
                if (dataReader.Read())
                    return this.getFromDataReader(dataReader);
                else
                    return null;
            }
        }

        public bool checkSession(string code, int sessionTime)
        {
            DbCommand dbCmd = CompanyUserConnectionsDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spCheckSession");

            // Add the parameters:
            CompanyUserConnectionsDALC.InstanceDatabase.AddParameter(dbCmd, "@Return", DbType.Int32,
                ParameterDirection.ReturnValue, string.Empty, DataRowVersion.Current, 0);
            CompanyUserConnectionsDALC.InstanceDatabase.AddParameter(dbCmd, "@codeString", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, code);
            CompanyUserConnectionsDALC.InstanceDatabase.AddParameter(dbCmd, "@sessionTime", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, sessionTime);

            CompanyUserConnectionsDALC.InstanceDatabase.ExecuteNonQuery(dbCmd);

            return Convert.ToInt32(dbCmd.Parameters["@Return"].Value) > 0 ? true : false;

        }

        #endregion

        #region Protected Methods

        protected override PLMClientsBusinessEntities.CompanyUserConnectionsInfo getFromDataReader(IDataReader current)
        {
            CompanyUserConnectionsInfo record = new CompanyUserConnectionsInfo();

            record.UserConnectionId = Convert.ToInt32(current["UserConnectionId"]);
            record.DistributionId = Convert.ToInt32(current["DistributionId"]);
            record.CompanyUserId = Convert.ToInt32(current["CompanyUserId"]);
            record.CodeId = Convert.ToInt32(current["CodeId"]);
            record.SessionId = Convert.ToByte(current["SessionId"]);

            if (current["DateConnection"] != System.DBNull.Value)
                record.DateConnection = Convert.ToDateTime(current["DateConnection"]);

            if (current["IP"] != System.DBNull.Value)
                record.IP = current["IP"].ToString();

            return record;
        }

        #endregion

        public static readonly CompanyUserConnectionsDALC Instance = new CompanyUserConnectionsDALC();

    }
}
