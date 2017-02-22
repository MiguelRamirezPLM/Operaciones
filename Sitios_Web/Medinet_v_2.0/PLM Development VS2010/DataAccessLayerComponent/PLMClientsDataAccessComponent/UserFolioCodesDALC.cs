using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using PLMClientsBusinessEntities;

namespace PLMClientsDataAccessComponent
{
    public sealed class UserFolioCodesDALC : PLMClientsDataAccessAdapter<UserFolioCodeInfo>
    {
        #region Constructors

        private UserFolioCodesDALC() { }

        #endregion

        #region Public Methods

        public override int insert(UserFolioCodeInfo businessEntity)
        {
            DbCommand dbCmd = UserFolioCodesDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDUserFolioCodes");

            // Add the parameters:
            UserFolioCodesDALC.InstanceDatabase.AddParameter(dbCmd, "@Return", DbType.Int32,
                ParameterDirection.ReturnValue, string.Empty, DataRowVersion.Current, 0);
            UserFolioCodesDALC.InstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Create);
            UserFolioCodesDALC.InstanceDatabase.AddParameter(dbCmd, "@userId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.UserId);
            UserFolioCodesDALC.InstanceDatabase.AddParameter(dbCmd, "@codeId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.CodeId);
            UserFolioCodesDALC.InstanceDatabase.AddParameter(dbCmd, "@folioId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.FolioId);

            //Insert record:
            UserFolioCodesDALC.InstanceDatabase.ExecuteNonQuery(dbCmd);

            return Convert.ToInt32(dbCmd.Parameters["@Return"].Value); 
        }

        public override void delete(UserFolioCodeInfo businessEntity)
        {
            DbCommand dbCmd = UserFolioCodesDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDUserFolioCodes");

            // Add the parameters:
            UserFolioCodesDALC.InstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Delete);
            UserFolioCodesDALC.InstanceDatabase.AddParameter(dbCmd, "@userId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.UserId);
            UserFolioCodesDALC.InstanceDatabase.AddParameter(dbCmd, "@codeId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.CodeId);
            UserFolioCodesDALC.InstanceDatabase.AddParameter(dbCmd, "@folioId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.FolioId);

            //Delete record:
            UserFolioCodesDALC.InstanceDatabase.ExecuteNonQuery(dbCmd);

        }

        public bool assignFolio(int folioId)
        {
            DbCommand dbCmd = UserFolioCodesDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spCheckUserFolio");

            // Add the parameters:
            UserFolioCodesDALC.InstanceDatabase.AddParameter(dbCmd, "@Return", DbType.Byte,
                ParameterDirection.ReturnValue, string.Empty, DataRowVersion.Current, 0);
            UserFolioCodesDALC.InstanceDatabase.AddParameter(dbCmd, "@folioId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, folioId);

            UserFolioCodesDALC.InstanceDatabase.ExecuteNonQuery(dbCmd);

            return Convert.ToInt32(dbCmd.Parameters["@Return"].Value) > 0;
        }

        #endregion

        #region Protected Methods

        protected override UserFolioCodeInfo getFromDataReader(IDataReader current)
        {
            UserFolioCodeInfo record = new UserFolioCodeInfo();

            record.UserId = Convert.ToInt32(current["UserId"]);
            record.CodeId = Convert.ToInt32(current["CodeId"]);
            record.FolioId = Convert.ToInt32(current["FolioId"]);

            return record;
        }

        #endregion

        public static readonly UserFolioCodesDALC Instance = new UserFolioCodesDALC();

    }
}
