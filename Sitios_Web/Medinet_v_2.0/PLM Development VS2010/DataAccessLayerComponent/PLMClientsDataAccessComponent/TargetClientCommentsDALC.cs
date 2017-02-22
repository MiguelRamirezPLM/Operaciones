using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using PLMClientsBusinessEntities;

namespace PLMClientsDataAccessComponent
{
    public class TargetClientCommentsDALC : PLMClientsDataAccessAdapter<TargetClientCommentsInfo>
    {

        #region Constructors

        private TargetClientCommentsDALC() { }

        #endregion

        #region Public Methods

        public override int insert(PLMClientsBusinessEntities.TargetClientCommentsInfo BEntity)
        {
            DbCommand dbCmd = TargetClientCommentsDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDTargetClientComments");

            // Add the parameters:
            TargetClientCommentsDALC.InstanceDatabase.AddParameter(dbCmd, "@Return", DbType.Int32,
                ParameterDirection.ReturnValue, string.Empty, DataRowVersion.Current, 0);
            TargetClientCommentsDALC.InstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Create);
            TargetClientCommentsDALC.InstanceDatabase.AddParameter(dbCmd, "@clientId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, BEntity.ClientId);
            TargetClientCommentsDALC.InstanceDatabase.AddParameter(dbCmd, "@codeId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, BEntity.CodeId);
            TargetClientCommentsDALC.InstanceDatabase.AddParameter(dbCmd, "@targetId", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, BEntity.TargetId);
            TargetClientCommentsDALC.InstanceDatabase.AddParameter(dbCmd, "@deviceId", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, BEntity.DeviceId);
            TargetClientCommentsDALC.InstanceDatabase.AddParameter(dbCmd, "@commentId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, BEntity.CommentId);

            //Insert record:
            TargetClientCommentsDALC.InstanceDatabase.ExecuteNonQuery(dbCmd);

            return Convert.ToInt32(dbCmd.Parameters["@Return"].Value);
        }

        #endregion

        public static readonly TargetClientCommentsDALC Instance = new TargetClientCommentsDALC();

    }
}
