using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using PLMClientsBusinessEntities;

namespace PLMClientsDataAccessComponent
{
   
    public sealed class ClientBranchesDALC : PLMClientsDataAccessAdapter<CompanyClientBranchesInfo>
    {
        #region Constructors

        private ClientBranchesDALC() { }

        #endregion

        #region Public Methods
          public override int insert(CompanyClientBranchesInfo businessEntity)
        {
            DbCommand dbCmd = ClientBranchesDALC.InstanceDatabase.GetStoredProcCommand("plm_spCRUDCompanyClientsBranches");

            // Add the parameters:
            ClientBranchesDALC.InstanceDatabase.AddParameter(dbCmd, "@Return", DbType.Int32,
                ParameterDirection.ReturnValue, string.Empty, DataRowVersion.Current, 0);
            ClientBranchesDALC.InstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Create);
            ClientBranchesDALC.InstanceDatabase.AddParameter(dbCmd, "@branchId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, -1);
            ClientBranchesDALC.InstanceDatabase.AddParameter(dbCmd, "@branchKey", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.BranchKey);
            ClientBranchesDALC.InstanceDatabase.AddParameter(dbCmd, "@branchName", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.BranchName);
            ClientBranchesDALC.InstanceDatabase.AddParameter(dbCmd, "@webPage", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.WebPage);
            ClientBranchesDALC.InstanceDatabase.AddParameter(dbCmd, "@email", DbType.AnsiString,
                 ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.Email);
            ClientBranchesDALC.InstanceDatabase.AddParameter(dbCmd, "@logo", DbType.AnsiString,
                 ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.Logo);
            ClientBranchesDALC.InstanceDatabase.AddParameter(dbCmd, "@companyClientId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.CompanyClientId);
            ClientBranchesDALC.InstanceDatabase.AddParameter(dbCmd, "@addressId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.AddressId);
            ClientBranchesDALC.InstanceDatabase.AddParameter(dbCmd, "@attentionSchedules", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.AttentionSchedules);
            ClientBranchesDALC.InstanceDatabase.AddParameter(dbCmd, "@HomeService", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.HomeService);


            //Insert record:
            ClientBranchesDALC.InstanceDatabase.ExecuteNonQuery(dbCmd);

            businessEntity.BranchId = Convert.ToInt32(dbCmd.Parameters["@Return"].Value);

            return Convert.ToInt32(dbCmd.Parameters["@Return"].Value);
        }
 #endregion

        #region Protected Methods

        protected override CompanyClientBranchesInfo getFromDataReader(IDataReader current)
        {
            CompanyClientBranchesInfo record = new CompanyClientBranchesInfo();

            record.BranchId = Convert.ToInt32(current[".BranchId"]);
            record.BranchName = current["BranchName"].ToString();
            record.WebPage = current["WebPage"].ToString();
            record.Email = current["Email"].ToString();
            record.BaseUrl = current["BaseUrl"].ToString();
            record.Logo = current["Logo"].ToString();

            record.CompanyClientId = Convert.ToInt32(current["CompanyClientId"]);
            record.AddressId = Convert.ToInt32(current["AddressId"]);
            record.AttentionSchedules= current["AttentionSchedules"].ToString();
            record.HomeService = current["HomeService"].ToString();
            record.Active = Convert.ToBoolean(current["Active"]);
            
            return record;

        }

        #endregion

        public static readonly ClientBranchesDALC Instance = new ClientBranchesDALC();

    }
}
