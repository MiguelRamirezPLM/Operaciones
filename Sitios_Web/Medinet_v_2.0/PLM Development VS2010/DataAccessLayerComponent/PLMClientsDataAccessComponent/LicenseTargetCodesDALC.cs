using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using PLMClientsBusinessEntities;

namespace PLMClientsDataAccessComponent
{
    public class LicenseTargetCodesDALC : PLMClientsDataAccessAdapter<LicenseTargetCodesInfo>
    {

        #region Constructors

        private LicenseTargetCodesDALC() { }

        #endregion

        #region Public Methods

        public override int insert(PLMClientsBusinessEntities.LicenseTargetCodesInfo BEntity)
        {
            DbCommand dbCmd = LicenseTargetCodesDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDLicenseTargetCodes");

            // Add the parameters:
            LicenseTargetCodesDALC.InstanceDatabase.AddParameter(dbCmd, "@Return", DbType.Int32,
                ParameterDirection.ReturnValue, string.Empty, DataRowVersion.Current, 0);
            LicenseTargetCodesDALC.InstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Create);
            LicenseTargetCodesDALC.InstanceDatabase.AddParameter(dbCmd, "@clientId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, BEntity.ClientId);
            LicenseTargetCodesDALC.InstanceDatabase.AddParameter(dbCmd, "@codeId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, BEntity.CodeId);
            LicenseTargetCodesDALC.InstanceDatabase.AddParameter(dbCmd, "@targetId", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, BEntity.TargetId);
            LicenseTargetCodesDALC.InstanceDatabase.AddParameter(dbCmd, "@deviceId", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, BEntity.DeviceId);
            LicenseTargetCodesDALC.InstanceDatabase.AddParameter(dbCmd, "@licenseId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, BEntity.LicenseId);

            if(BEntity.FinalDate != null)
                LicenseTargetCodesDALC.InstanceDatabase.AddParameter(dbCmd, "@finalDate", DbType.DateTime, 
                    ParameterDirection.Input, string.Empty, DataRowVersion.Current, BEntity.FinalDate);


            //Insert record:
            LicenseTargetCodesDALC.InstanceDatabase.ExecuteNonQuery(dbCmd);

            return Convert.ToInt32(dbCmd.Parameters["@Return"].Value);
            
        }

        public override void delete(LicenseTargetCodesInfo BEntity)
        {
            DbCommand dbCmd = LicenseTargetCodesDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDLicenseTargetCodes");

            // Add the parameters:
            LicenseTargetCodesDALC.InstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Delete);
            LicenseTargetCodesDALC.InstanceDatabase.AddParameter(dbCmd, "@clientId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, BEntity.ClientId);
            LicenseTargetCodesDALC.InstanceDatabase.AddParameter(dbCmd, "@codeId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, BEntity.CodeId);
            LicenseTargetCodesDALC.InstanceDatabase.AddParameter(dbCmd, "@targetId", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, BEntity.TargetId);
            LicenseTargetCodesDALC.InstanceDatabase.AddParameter(dbCmd, "@deviceId", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, BEntity.DeviceId);
            LicenseTargetCodesDALC.InstanceDatabase.AddParameter(dbCmd, "@licenseId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, BEntity.LicenseId);

            //Delete record:
            LicenseTargetCodesDALC.InstanceDatabase.ExecuteNonQuery(dbCmd);
        }

        public PLMClientsBusinessEntities.LicenseTargetCodesInfo getLicenseByPrefixByHwIdentifier(string prefix, string hwIdentifier, int licenseId)
        {
            DbCommand dbCmd = LicenseTargetCodesDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spGetLicenseByPrefixByHwIdentifier");

            // Add the parameters:
            LicenseTargetCodesDALC.InstanceDatabase.AddParameter(dbCmd, "@prefix", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, prefix);
            LicenseTargetCodesDALC.InstanceDatabase.AddParameter(dbCmd, "@hwIdentifier", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, hwIdentifier);
            LicenseTargetCodesDALC.InstanceDatabase.AddParameter(dbCmd, "@licenseId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, licenseId);

            PLMClientsBusinessEntities.LicenseTargetCodesInfo licenseInfo = new PLMClientsBusinessEntities.LicenseTargetCodesInfo();

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = LicenseTargetCodesDALC.InstanceDatabase.ExecuteReader(dbCmd))
            {
                if (dataReader.Read())
                {
                    licenseInfo.ClientId = Convert.ToInt32(dataReader["ClientId"]);
                    licenseInfo.CodeId = Convert.ToInt32(dataReader["CodeId"]);
                    licenseInfo.TargetId = Convert.ToByte(dataReader["TargetId"]);
                    licenseInfo.DeviceId = Convert.ToByte(dataReader["DeviceId"]);
                    licenseInfo.LicenseId = Convert.ToInt32(dataReader["LicenseId"]);
                    licenseInfo.InitialDate = Convert.ToDateTime(dataReader["InitialDate"]);
                    licenseInfo.FinalDate = Convert.ToDateTime(dataReader["FinalDate"]);

                    return licenseInfo;
                }
                else
                    return null;
            }
        }

        public PLMClientsBusinessEntities.LicenseDetailInfo getLicenseByCodeByDevice(int clientId, int codeId, byte targetId, byte deviceId, int licenseId)
        {
            DbCommand dbCmd = LicenseTargetCodesDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spGetLicenseByCodeByDevice");

            // Add the parameters:
            LicenseTargetCodesDALC.InstanceDatabase.AddParameter(dbCmd, "@clientId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, clientId);
            LicenseTargetCodesDALC.InstanceDatabase.AddParameter(dbCmd, "@codeId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, codeId);
            LicenseTargetCodesDALC.InstanceDatabase.AddParameter(dbCmd, "@targetId", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, targetId);
            LicenseTargetCodesDALC.InstanceDatabase.AddParameter(dbCmd, "@deviceId", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, deviceId);
            LicenseTargetCodesDALC.InstanceDatabase.AddParameter(dbCmd, "@licenseId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, licenseId);

            PLMClientsBusinessEntities.LicenseDetailInfo licenseDetailInfo = new PLMClientsBusinessEntities.LicenseDetailInfo();

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = LicenseTargetCodesDALC.InstanceDatabase.ExecuteReader(dbCmd))
            {
                if (dataReader.Read())
                {
                    licenseDetailInfo.ClientId = Convert.ToInt32(dataReader["ClientId"]);
                    licenseDetailInfo.CodeId = Convert.ToInt32(dataReader["CodeId"]);
                    licenseDetailInfo.DistributionId = Convert.ToInt32(dataReader["DistributionId"]);
                    licenseDetailInfo.PrefixId = Convert.ToInt32(dataReader["PrefixId"]);
                    licenseDetailInfo.TargetId = Convert.ToByte(dataReader["TargetId"]);
                    licenseDetailInfo.DeviceId = Convert.ToByte(dataReader["DeviceId"]);
                    licenseDetailInfo.LicenseId = Convert.ToInt32(dataReader["LicenseId"]);
                    licenseDetailInfo.InitialDate = Convert.ToDateTime(dataReader["InitialDate"]);
                    licenseDetailInfo.FinalDate = Convert.ToDateTime(dataReader["FinalDate"]);
                    licenseDetailInfo.CodeString = dataReader["CodeString"].ToString();
                    licenseDetailInfo.PrefixName = dataReader["PrefixName"].ToString();

                    return licenseDetailInfo;
                }
                else
                    return null;
            }
        }

        #endregion

        public static readonly LicenseTargetCodesDALC Instance = new LicenseTargetCodesDALC();

    }
}
