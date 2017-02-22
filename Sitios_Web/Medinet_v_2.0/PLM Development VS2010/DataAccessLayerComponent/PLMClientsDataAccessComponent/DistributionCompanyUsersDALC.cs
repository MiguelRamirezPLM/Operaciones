using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using PLMClientsBusinessEntities;

namespace PLMClientsDataAccessComponent
{
    public class DistributionCompanyUsersDALC : PLMClientsDataAccessAdapter<DistributionCompanyUsersInfo>
    {

        #region Constructors

        private DistributionCompanyUsersDALC() { }

        #endregion

        #region Public Methods

        public override int insert(DistributionCompanyUsersInfo businessEntity)
        {
            DbCommand dbCmd = DistributionCompanyUsersDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDDistributionCompanyUsers");

            // Add the parameters:
            DistributionCompanyUsersDALC.InstanceDatabase.AddParameter(dbCmd, "@Return", DbType.Int32,
                ParameterDirection.ReturnValue, string.Empty, DataRowVersion.Current, 0);
            DistributionCompanyUsersDALC.InstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Create);
            DistributionCompanyUsersDALC.InstanceDatabase.AddParameter(dbCmd, "@distributionId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.DistributionId);
            DistributionCompanyUsersDALC.InstanceDatabase.AddParameter(dbCmd, "@companyUserId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.CompanyUserId);
            DistributionCompanyUsersDALC.InstanceDatabase.AddParameter(dbCmd, "@codeId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.CodeId);
            DistributionCompanyUsersDALC.InstanceDatabase.AddParameter(dbCmd, "@initialDate", DbType.DateTime,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.InitialDate);
            DistributionCompanyUsersDALC.InstanceDatabase.AddParameter(dbCmd, "@finalDate", DbType.DateTime,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.FinalDate);
            DistributionCompanyUsersDALC.InstanceDatabase.AddParameter(dbCmd, "@active", DbType.Boolean,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.Active);

            DistributionCompanyUsersDALC.InstanceDatabase.ExecuteNonQuery(dbCmd);

            return Convert.ToInt32(dbCmd.Parameters["@Return"].Value);
        }

        #endregion

        public static readonly DistributionCompanyUsersDALC Instance = new DistributionCompanyUsersDALC();

    }
}
