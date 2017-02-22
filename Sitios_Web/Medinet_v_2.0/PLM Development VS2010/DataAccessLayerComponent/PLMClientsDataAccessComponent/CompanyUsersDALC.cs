using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using PLMClientsBusinessEntities;

namespace PLMClientsDataAccessComponent
{
    public class CompanyUsersDALC : PLMClientsDataAccessAdapter<CompanyUsersInfo>
    {

        #region Constructors

        private CompanyUsersDALC() { }

        #endregion

        #region Public Methods

        public override int insert(CompanyUsersInfo businessEntity)
        {
            DbCommand dbCmd = CompanyUsersDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spCRUDCompanyUsers");

            // Add the parameters:
            CompanyUsersDALC.InstanceDatabase.AddParameter(dbCmd, "@Return", DbType.Int32,
                ParameterDirection.ReturnValue, string.Empty, DataRowVersion.Current, 0);
            CompanyUsersDALC.InstanceDatabase.AddParameter(dbCmd, "@CRUDType", DbType.Byte,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, CRUD.Create);
            CompanyUsersDALC.InstanceDatabase.AddParameter(dbCmd, "@companyUserId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, -1);
            CompanyUsersDALC.InstanceDatabase.AddParameter(dbCmd, "@companyClientId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.CompanyClientId);
            CompanyUsersDALC.InstanceDatabase.AddParameter(dbCmd, "@firstName", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.FirstName);
            CompanyUsersDALC.InstanceDatabase.AddParameter(dbCmd, "@lastName", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.LastName);
            CompanyUsersDALC.InstanceDatabase.AddParameter(dbCmd, "@userName", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.UserName);
            CompanyUsersDALC.InstanceDatabase.AddParameter(dbCmd, "@userPassword", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.UserPassword);
            CompanyUsersDALC.InstanceDatabase.AddParameter(dbCmd, "@active", DbType.Boolean,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, businessEntity.Active);

            //Insert record:
            CompanyUsersDALC.InstanceDatabase.ExecuteNonQuery(dbCmd);
            return Convert.ToInt32(dbCmd.Parameters["@Return"].Value); 
        }

        public bool checkUserByCompany(int companyClientId, string userName)
        {
            DbCommand dbCmd = CompanyUsersDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spCheckUserByCompany");

            // Add the parameters:
            CompanyUsersDALC.InstanceDatabase.AddParameter(dbCmd, "@Return", DbType.Byte,
                ParameterDirection.ReturnValue, string.Empty, DataRowVersion.Current, 0);
            CompanyUsersDALC.InstanceDatabase.AddParameter(dbCmd, "@companyClientId", DbType.Int32,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, companyClientId);
            CompanyUsersDALC.InstanceDatabase.AddParameter(dbCmd, "@userName", DbType.AnsiString,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, userName);

            CompanyUsersDALC.InstanceDatabase.ExecuteNonQuery(dbCmd);

            return Convert.ToInt32(dbCmd.Parameters["@Return"].Value) > 0;
        }

        public PLMClientsBusinessEntities.PharmacyUserInfo getPharmacyUser(string userName, string userPassword)
        {
            PLMClientsBusinessEntities.PharmacyUserInfo pharmacyUserInfo = new PLMClientsBusinessEntities.PharmacyUserInfo();

            DbCommand dbCmd = CompanyUsersDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spGetPharmacyUser");

            // Add the parameters:
            CompanyUsersDALC.InstanceDatabase.AddParameter(dbCmd, "@userName", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, userName);
            CompanyUsersDALC.InstanceDatabase.AddParameter(dbCmd, "@userPassword", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, userPassword);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = CompanyUsersDALC.InstanceDatabase.ExecuteReader(dbCmd))
            {
                if (dataReader.Read())
                {
                    pharmacyUserInfo.CompanyUserId = Convert.ToInt32(dataReader["CompanyUserId"]);
                    pharmacyUserInfo.CompanyClientId = Convert.ToInt32(dataReader["CompanyClientId"]);
                    pharmacyUserInfo.FirstName = dataReader["FirstName"].ToString();
                    pharmacyUserInfo.LastName = dataReader["LastName"].ToString();
                    pharmacyUserInfo.UserName = dataReader["UserName"].ToString();
                    pharmacyUserInfo.UserPassword = dataReader["UserPassword"].ToString();
                    pharmacyUserInfo.CodeId = Convert.ToInt32(dataReader["CodeId"]);
                    pharmacyUserInfo.CodeString = dataReader["CodeString"].ToString();
                    pharmacyUserInfo.Active = Convert.ToBoolean(dataReader["Active"]);

                    return pharmacyUserInfo;
                }
                else
                    return null;
            }
        }

        public PLMClientsBusinessEntities.PharmacyUserInfo getPharmacyUserByCode(string code)
        {
            PLMClientsBusinessEntities.PharmacyUserInfo pharmacyUserInfo = new PLMClientsBusinessEntities.PharmacyUserInfo();

            DbCommand dbCmd = CompanyUsersDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spGetPharmacyUser");

            // Add the parameters:
            CompanyUsersDALC.InstanceDatabase.AddParameter(dbCmd, "@codeString", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, code);
            
            // Get the result set from the stored procedure:
            using (IDataReader dataReader = CompanyUsersDALC.InstanceDatabase.ExecuteReader(dbCmd))
            {
                if (dataReader.Read())
                {
                    pharmacyUserInfo.CompanyUserId = Convert.ToInt32(dataReader["CompanyUserId"]);
                    pharmacyUserInfo.CompanyClientId = Convert.ToInt32(dataReader["CompanyClientId"]);
                    pharmacyUserInfo.FirstName = dataReader["FirstName"].ToString();
                    pharmacyUserInfo.LastName = dataReader["LastName"].ToString();
                    pharmacyUserInfo.UserName = dataReader["UserName"].ToString();
                    pharmacyUserInfo.UserPassword = dataReader["UserPassword"].ToString();
                    pharmacyUserInfo.CodeId = Convert.ToInt32(dataReader["CodeId"]);
                    pharmacyUserInfo.CodeString = dataReader["CodeString"].ToString();
                    pharmacyUserInfo.Active = Convert.ToBoolean(dataReader["Active"]);

                    return pharmacyUserInfo;
                }
                else
                    return null;
            }
        }

        public PLMClientsBusinessEntities.WebApplicationUsersInfo getWebApplicationUserByCode(string code)
        {
            PLMClientsBusinessEntities.WebApplicationUsersInfo webApplicationUserInfo = new PLMClientsBusinessEntities.WebApplicationUsersInfo();

            DbCommand dbCmd = CompanyUsersDALC.InstanceDatabase.GetStoredProcCommand("dbo.plm_spGetWebApplicationUsers");

            // Add the parameters:
            CompanyUsersDALC.InstanceDatabase.AddParameter(dbCmd, "@codeString", DbType.String,
                ParameterDirection.Input, string.Empty, DataRowVersion.Current, code);

            // Get the result set from the stored procedure:
            using (IDataReader dataReader = CompanyUsersDALC.InstanceDatabase.ExecuteReader(dbCmd))
            {
                if (dataReader.Read())
                {
                    webApplicationUserInfo.CompanyClientId = Convert.ToInt32(dataReader["CompanyClientId"]);
                    webApplicationUserInfo.CompanyName = dataReader["CompanyName"].ToString();
                    webApplicationUserInfo.CCTypeId = Convert.ToByte(dataReader["CCTypeId"]);
                    webApplicationUserInfo.CCTypeName = dataReader["CCTypeName"].ToString();
                    webApplicationUserInfo.CodeId = Convert.ToInt32(dataReader["CodeId"]);
                    webApplicationUserInfo.CodeString = dataReader["CodeString"].ToString();
                    webApplicationUserInfo.EditionId = Convert.ToInt32(dataReader["EditionId"]);
                    webApplicationUserInfo.Active = Convert.ToBoolean(dataReader["Active"]);

                    return webApplicationUserInfo;
                }
                else
                    return null;
            }
        }

        #endregion

        public static readonly CompanyUsersDALC Instance = new CompanyUsersDALC();

    }
}
