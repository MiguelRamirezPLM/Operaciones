using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using PLMClientsBusinessEntities;

namespace PLMClientsDataAccessComponent
{
    public class TargetClientCodesDALC : PLMClientsDataAccessAdapter<TargetClientCodesInfo>
    {

        #region Constructors

        private TargetClientCodesDALC() { }

        #endregion

        #region Public Methods

        public override int insert(TargetClientCodesInfo BEntity)
        {
            DbCommand dbCmd = TargetClientCodesDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDTargetClientCodes");

            // Add the parameters:
            TargetClientCodesDALC.InstanceDatabase.AddParameter(dbCmd, "@Return", DbType.Int32,
                ParameterDirection.ReturnValue, string.Empty, DataRowVersion.Current, 0);
            TargetClientCodesDALC.InstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Create);
            TargetClientCodesDALC.InstanceDatabase.AddParameter(dbCmd, "@clientId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, BEntity.ClientId);
            TargetClientCodesDALC.InstanceDatabase.AddParameter(dbCmd, "@codeId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, BEntity.CodeId);
            TargetClientCodesDALC.InstanceDatabase.AddParameter(dbCmd, "@targetId", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, BEntity.TargetId);
            TargetClientCodesDALC.InstanceDatabase.AddParameter(dbCmd, "@deviceId", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, BEntity.DeviceId);
            TargetClientCodesDALC.InstanceDatabase.AddParameter(dbCmd, "@hwIdentifier", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, BEntity.HWIdentifier);

            //Insert record:
            TargetClientCodesDALC.InstanceDatabase.ExecuteNonQuery(dbCmd);

            return Convert.ToInt32(dbCmd.Parameters["@Return"].Value);
        }

        public override void update(TargetClientCodesInfo BEntity)
        {
            DbCommand dbCmd = TargetClientCodesDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDTargetClientCodes");

            // Add the parameters:
            TargetClientCodesDALC.InstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Update);
            TargetClientCodesDALC.InstanceDatabase.AddParameter(dbCmd, "@clientId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, BEntity.ClientId);
            TargetClientCodesDALC.InstanceDatabase.AddParameter(dbCmd, "@codeId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, BEntity.CodeId);
            TargetClientCodesDALC.InstanceDatabase.AddParameter(dbCmd, "@targetId", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, BEntity.TargetId);
            TargetClientCodesDALC.InstanceDatabase.AddParameter(dbCmd, "@deviceId", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, BEntity.DeviceId);
            TargetClientCodesDALC.InstanceDatabase.AddParameter(dbCmd, "@hwIdentifier", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, BEntity.HWIdentifier);
            TargetClientCodesDALC.InstanceDatabase.AddParameter(dbCmd, "@installationDate", DbType.DateTime,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, BEntity.InstallationDate);
            TargetClientCodesDALC.InstanceDatabase.AddParameter(dbCmd, "@active", DbType.Boolean,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, BEntity.Active);

            //Insert record:
            TargetClientCodesDALC.InstanceDatabase.ExecuteNonQuery(dbCmd);

        }

        public override void delete(TargetClientCodesInfo BEntity)
        {
            DbCommand dbCmd = TargetClientCodesDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDTargetClientCodes");

            // Add the parameters:
            TargetClientCodesDALC.InstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Delete);
            TargetClientCodesDALC.InstanceDatabase.AddParameter(dbCmd, "@clientId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, BEntity.ClientId);
            TargetClientCodesDALC.InstanceDatabase.AddParameter(dbCmd, "@codeId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, BEntity.CodeId);
            TargetClientCodesDALC.InstanceDatabase.AddParameter(dbCmd, "@targetId", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, BEntity.TargetId);
            TargetClientCodesDALC.InstanceDatabase.AddParameter(dbCmd, "@deviceId", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, BEntity.DeviceId);

            //Delete record:
            TargetClientCodesDALC.InstanceDatabase.ExecuteNonQuery(dbCmd);
        }

        public PLMClientsBusinessEntities.TargetClientCodesInfo getByPrefixByHwIdentifier(string codePrefix, string hwIdentifier)
        {
            DbCommand dbCmd = TargetClientCodesDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spGetCodeByHwIdByPrefix");

            // Add the parameters:
            TargetClientCodesDALC.InstanceDatabase.AddParameter(dbCmd, "@codePrefix", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, codePrefix);
            TargetClientCodesDALC.InstanceDatabase.AddParameter(dbCmd, "@hwIdentifier", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, hwIdentifier);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = TargetClientCodesDALC.InstanceDatabase.ExecuteReader(dbCmd))
            {
                if (dataReader.Read())
                    return this.getFromDataReader(dataReader);
                else
                    return null;
            }
        }

        public PLMClientsBusinessEntities.TargetClientCodesInfo getTargetClientByCode(string code)
        {
            DbCommand dbCmd = TargetClientCodesDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spGetTargetClientByCode");

            // Add the parameters:
            TargetClientCodesDALC.InstanceDatabase.AddParameter(dbCmd, "@code", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, code);
            // Get the result set from the stored procedure:
            using (IDataReader dataReader = TargetClientCodesDALC.InstanceDatabase.ExecuteReader(dbCmd))
            {
                if (dataReader.Read())
                    return this.getFromDataReader(dataReader);
                else
                    return null;
            }
        }

        #endregion

        #region Protected Methods

        protected override PLMClientsBusinessEntities.TargetClientCodesInfo getFromDataReader(IDataReader current)
        {
            TargetClientCodesInfo record = new TargetClientCodesInfo();

            record.ClientId = Convert.ToInt32(current["ClientId"]);
            record.CodeId = Convert.ToInt32(current["CodeId"]);
            record.TargetId = Convert.ToByte(current["TargetId"]);
            record.DeviceId = Convert.ToByte(current["DeviceId"]);

            if (current["HWIdentifier"] != null)
                record.HWIdentifier = current["HWIdentifier"].ToString();
            
            if (current["InstallationDate"] != null)
                record.InstallationDate = Convert.ToDateTime(current["InstallationDate"]);
            
            if (current["Active"] != null)
                record.Active = Convert.ToBoolean(current["Active"]);

            return record;
        }

        #endregion

        public static readonly TargetClientCodesDALC Instance = new TargetClientCodesDALC();

    }
}
