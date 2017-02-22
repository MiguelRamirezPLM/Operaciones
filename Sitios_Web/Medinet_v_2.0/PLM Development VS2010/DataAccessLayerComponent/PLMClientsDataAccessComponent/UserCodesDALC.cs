using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using PLMClientsBusinessEntities;

namespace PLMClientsDataAccessComponent
{
    public sealed class UserCodesDALC : PLMClientsDataAccessAdapter<UserCodeInfo>
    {
        #region Constructors

        private UserCodesDALC() { }

        #endregion

        #region Public Methods

        public override int insert(UserCodeInfo businessEntity)
        {
            DbCommand dbCmd = UserCodesDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDUserCodes");

            // Add the parameters:
            UserCodesDALC.InstanceDatabase.AddParameter(dbCmd, "@Return", DbType.Int32,
                ParameterDirection.ReturnValue, string.Empty, DataRowVersion.Current, 0);
            UserCodesDALC.InstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Create);
            UserCodesDALC.InstanceDatabase.AddParameter(dbCmd, "@userId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.UserId);
            UserCodesDALC.InstanceDatabase.AddParameter(dbCmd, "@codeId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.CodeId);

            if (businessEntity.InitialDate != null)
                UserCodesDALC.InstanceDatabase.AddParameter(dbCmd, "@initialDate", DbType.DateTime,
                    ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.InitialDate);

            if (businessEntity.FinalDate != null)
                UserCodesDALC.InstanceDatabase.AddParameter(dbCmd, "@finalDate", DbType.DateTime,
                    ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.FinalDate);

            //Insert record:
            UserCodesDALC.InstanceDatabase.ExecuteNonQuery(dbCmd);

            return Convert.ToInt32(dbCmd.Parameters["@Return"].Value); 
        }

        public override void delete(UserCodeInfo businessEntity)
        {
            DbCommand dbCmd = UserCodesDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDUserCodes");

            // Add the parameters:
            UserCodesDALC.InstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Delete);
            UserCodesDALC.InstanceDatabase.AddParameter(dbCmd, "@userId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.UserId);
            UserCodesDALC.InstanceDatabase.AddParameter(dbCmd, "@codeId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.CodeId);

            //Delete record:
            UserCodesDALC.InstanceDatabase.ExecuteNonQuery(dbCmd);

        }

        public bool checkUser(int userId, int codeId)
        {
            DbCommand dbCmd = UserCodesDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spCheckUserCodes");

            //Add the parameters:
            UserCodesDALC.InstanceDatabase.AddParameter(dbCmd, "@Return", DbType.Int32,
                ParameterDirection.ReturnValue, string.Empty, DataRowVersion.Current, 0);
            UserCodesDALC.InstanceDatabase.AddParameter(dbCmd, "@userId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, userId);
            UserCodesDALC.InstanceDatabase.AddParameter(dbCmd, "@codeId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, codeId);

            UserCodesDALC.InstanceDatabase.ExecuteNonQuery(dbCmd);

            return Convert.ToInt32(dbCmd.Parameters["@Return"].Value) > 0;

        }

        #endregion

        #region Protected Methods

        protected override UserCodeInfo getFromDataReader(IDataReader current)
        {
            UserCodeInfo record = new UserCodeInfo();

            record.UserId = Convert.ToInt32(current["UserId"]);
            record.CodeId = Convert.ToInt32(current["CodeId"]);

            if(current["InitialDate"] != System.DBNull.Value)
                record.InitialDate = Convert.ToDateTime(current["IntialDate"]);

            if(current["FinalDate"] != System.DBNull.Value)
                record.FinalDate = Convert.ToDateTime(current["FinalDate"]);

            return record;
        }

        #endregion

        public static readonly UserCodesDALC Instance = new UserCodesDALC();

    }
}
