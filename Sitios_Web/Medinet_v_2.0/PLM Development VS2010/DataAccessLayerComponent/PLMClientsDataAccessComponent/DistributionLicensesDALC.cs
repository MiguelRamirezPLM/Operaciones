using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using PLMClientsBusinessEntities;

namespace PLMClientsDataAccessComponent
{
    public class DistributionLicensesDALC : PLMClientsDataAccessAdapter<DistributionLicensesInfo>
    {

        #region Constructors

        private DistributionLicensesDALC() { }

        #endregion

        #region Public Methods

        public PLMClientsBusinessEntities.DistributionLicensesInfo getDistributionLicense(byte targetId, int licenseId)
        {
            DbCommand dbCmd = DistributionLicensesDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spGetDistributionLicense");

            // Add the parameters:
            DistributionLicensesDALC.InstanceDatabase.AddParameter(dbCmd, "@targetId", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, targetId);
            DistributionLicensesDALC.InstanceDatabase.AddParameter(dbCmd, "@licenseId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, licenseId);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = DistributionLicensesDALC.InstanceDatabase.ExecuteReader(dbCmd))
            {
                if (dataReader.Read())
                    return this.getFromDataReader(dataReader);
                else
                    return null;
            }
        }

        public override int insert (PLMClientsBusinessEntities.DistributionLicensesInfo BEntity )
        {
            DbCommand dbCmd = LicensesDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDDistributionLicenses");

            // Add the parameters:
            LicensesDALC.InstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Create);
            LicensesDALC.InstanceDatabase.AddParameter(dbCmd, "@distribution", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, BEntity.DistributionId);
            LicensesDALC.InstanceDatabase.AddParameter(dbCmd, "@license", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, BEntity.LicenseId);
            LicensesDALC.InstanceDatabase.AddParameter(dbCmd, "@prefix", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, BEntity.PrefixId);
            LicensesDALC.InstanceDatabase.AddParameter(dbCmd, "@target", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, BEntity.TargetId);

            //Insert record:
            LicensesDALC.InstanceDatabase.ExecuteNonQuery(dbCmd);
             int i = 1;
             return  i;
     
        }
        #endregion

        #region Protected Methods

        protected override DistributionLicensesInfo getFromDataReader(IDataReader current)
        {
            DistributionLicensesInfo record = new DistributionLicensesInfo();

            record.DistributionId = Convert.ToInt32(current["DistributionId"]);
            record.PrefixId = Convert.ToInt32(current["PrefixId"]);
            record.TargetId = Convert.ToByte(current["TargetId"]);
            record.LicenseId = Convert.ToInt32(current["LicenseId"]);
            
            return record;
        }

        #endregion

        public static readonly DistributionLicensesDALC Instance = new DistributionLicensesDALC();

    }
}
