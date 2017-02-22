using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using PLMClientsBusinessEntities;

namespace PLMClientsDataAccessComponent
{
    public sealed class LicensesDALC : PLMClientsDataAccessAdapter<LicensesInfo>
    {

        #region Constructors

        private LicensesDALC() { }

        #endregion

        #region Public Methods

        public override int insert(LicensesInfo BEntity)
        {

            DbCommand dbCmd = LicensesDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDLicenses");

            // Add the parameters:
            LicensesDALC.InstanceDatabase.AddParameter(dbCmd, "@Return", DbType.Int32,
                ParameterDirection.ReturnValue, string.Empty, DataRowVersion.Current, 0);
            LicensesDALC.InstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Create);
            LicensesDALC.InstanceDatabase.AddParameter(dbCmd, "@licenseId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, -1);
            LicensesDALC.InstanceDatabase.AddParameter(dbCmd, "@licenseKey", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, BEntity.LicenseKey);
            LicensesDALC.InstanceDatabase.AddParameter(dbCmd, "@currentInstallation", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, BEntity.CurrentInstallation);
            LicensesDALC.InstanceDatabase.AddParameter(dbCmd, "@duration", DbType.Decimal,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, BEntity.Duration);
            LicensesDALC.InstanceDatabase.AddParameter(dbCmd, "@active", DbType.Boolean,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, BEntity.Active);

            //Insert record:
            LicensesDALC.InstanceDatabase.ExecuteNonQuery(dbCmd);

            return Convert.ToInt32(dbCmd.Parameters["@Return"].Value);
        }

        public override void update(LicensesInfo BEntity)
        {
            DbCommand dbCmd = LicensesDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDLicenses");

            // Add the parameters:
            LicensesDALC.InstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Update);
            LicensesDALC.InstanceDatabase.AddParameter(dbCmd, "@licenseId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, BEntity.LicenseId);
            LicensesDALC.InstanceDatabase.AddParameter(dbCmd, "@licenseKey", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, BEntity.LicenseKey);
            LicensesDALC.InstanceDatabase.AddParameter(dbCmd, "@currentInstallation", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, BEntity.CurrentInstallation);
            LicensesDALC.InstanceDatabase.AddParameter(dbCmd, "@duration", DbType.Decimal,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, BEntity.Duration);
            LicensesDALC.InstanceDatabase.AddParameter(dbCmd, "@active", DbType.Boolean,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, BEntity.Active);

            //Update record:
            LicensesDALC.InstanceDatabase.ExecuteNonQuery(dbCmd);
        }

        public override LicensesInfo getOne(int pk)
        {
            DbCommand dbCmd = LicensesDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDLicenses");

            // Add the parameters:
            LicensesDALC.InstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Read);
            LicensesDALC.InstanceDatabase.AddParameter(dbCmd, "@licenseId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, pk);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = LicensesDALC.InstanceDatabase.ExecuteReader(dbCmd))
            {
                if (dataReader.Read())
                    return this.getFromDataReader(dataReader);

                else
                    return null;
            }
        }

        public LicensesInfo getByLicenseKey(string licenseKey)
        {
            DbCommand dbCmd = LicensesDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spGetLicenseByKey");

            // Add the parameters:
            LicensesDALC.InstanceDatabase.AddParameter(dbCmd, "@licenseKey", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, licenseKey);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = LicensesDALC.InstanceDatabase.ExecuteReader(dbCmd))
            {
                if (dataReader.Read())
                    return this.getFromDataReader(dataReader);

                else
                    return null;
            }   
        }

        public bool checkLicense(string licenseKey, string codePrefix)
        {
            DbCommand dbCmd = LicensesDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spCheckLicense");

            // Add the parameters:
            LicensesDALC.InstanceDatabase.AddParameter(dbCmd, "@Return", DbType.Int32,
                ParameterDirection.ReturnValue, string.Empty, DataRowVersion.Current, 0);
            LicensesDALC.InstanceDatabase.AddParameter(dbCmd, "@licenseKey", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, licenseKey);
            LicensesDALC.InstanceDatabase.AddParameter(dbCmd, "@codePrefix", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, codePrefix);

            LicensesDALC.InstanceDatabase.ExecuteNonQuery(dbCmd);

            return Convert.ToInt32(dbCmd.Parameters["@Return"].Value) > 0 ? true : false;
        }

        public int getNumberInstallations(string licenseKey, string codePrefix)
        {
            DbCommand dbCmd = LicensesDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spGetLicenseNumberInstallations");

            // Add the parameters:
            LicensesDALC.InstanceDatabase.AddParameter(dbCmd, "@Return", DbType.Int32,
                ParameterDirection.ReturnValue, string.Empty, DataRowVersion.Current, 0);
            LicensesDALC.InstanceDatabase.AddParameter(dbCmd, "@licenseKey", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, licenseKey);
            LicensesDALC.InstanceDatabase.AddParameter(dbCmd, "@codePrefix", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, codePrefix);

            LicensesDALC.InstanceDatabase.ExecuteNonQuery(dbCmd);

            return Convert.ToInt32(dbCmd.Parameters["@Return"].Value);
        }

        public LicensesInfo getLicenseByClientIdByPrefix(int clientId, int prefix)
        {
            DbCommand dbCmd = LicensesDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spGetLicenseByClientIdByPrefixId");

            // Add the parameters:
            LicensesDALC.InstanceDatabase.AddParameter(dbCmd, "@clientid", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, clientId);
            LicensesDALC.InstanceDatabase.AddParameter(dbCmd, "@prefixid", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, prefix);

            LicensesDALC.InstanceDatabase.ExecuteNonQuery(dbCmd);

            using (IDataReader dataReader = LicensesDALC.InstanceDatabase.ExecuteReader(dbCmd))
            {
                if (dataReader.Read())
                    return this.getFromDataReader(dataReader);

                else
                    return null;
            }
        }

        public int updateLicenseDownloadClient(string license, string HWIdentifier)
        {
            DbCommand dbCmd = LicensesDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDLicenseClientDownload");

            // Add the parameters:

            LicensesDALC.InstanceDatabase.AddParameter(dbCmd, "@Return", DbType.Int32,
                ParameterDirection.ReturnValue, string.Empty, DataRowVersion.Current, 0);
            LicensesDALC.InstanceDatabase.AddParameter(dbCmd, "@licensekey", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, license);
            LicensesDALC.InstanceDatabase.AddParameter(dbCmd, "@hwidentifier", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, HWIdentifier);


            //Update record:
            LicensesDALC.InstanceDatabase.ExecuteNonQuery(dbCmd);
            return Convert.ToInt32(dbCmd.Parameters["@Return"].Value);  
        }

        public List<LicensesInfo> getLicenses()
        {
            DbCommand dbCmd = LicensesDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spGetLicenses");

            List<LicensesInfo> BECollection = new List<LicensesInfo>();

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = LicensesDALC.InstanceDatabase.ExecuteReader(dbCmd))
            {
                // Iterates through records:
                while (dataReader.Read())
                {
                    BECollection.Add(this.getFromDataReader(dataReader));
                }
            }

            return BECollection;
        }
        
        #endregion

        #region Protected Methods

        protected override LicensesInfo getFromDataReader(IDataReader current)
        {
            LicensesInfo record = new LicensesInfo();

            record.LicenseId = Convert.ToInt32(current["LicenseId"]);
            //record.DistributionId = Convert.ToInt32(current["DistributionId"]);
            //record.PrefixId = Convert.ToInt32(current["PrefixId"]);
            //record.TargetId = Convert.ToByte(current["TargetId"]);
            record.CurrentInstallation = Convert.ToByte(current["CurrentInstallation"]);
            record.Duration = Convert.ToDecimal(current["Duration"]);
            record.Active = Convert.ToBoolean(current["Active"]);
            
            if(current["LicenseKey"] != null)
                record.LicenseKey = current["LicenseKey"].ToString();

            return record;
        }

        #endregion

        public static readonly LicensesDALC Instance = new LicensesDALC();
    }
}
