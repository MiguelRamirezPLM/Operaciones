using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PLMClientsBusinessEntities;

namespace PLMClientsBusinessLogicComponent
{
    public class CompanyUsersBLC
    {

        #region Constructors

        private CompanyUsersBLC() { }

        #endregion

        #region Public Methods

        public int addCompanyUser(PLMClientsBusinessEntities.CompanyUsersInfo companyUserInfo)
        {
            if(checkUserByCompany(companyUserInfo.CompanyClientId, companyUserInfo.UserName) == true)
                throw new ArgumentException("The user name already exists for this company.");

            return PLMClientsDataAccessComponent.CompanyUsersDALC.Instance.insert(companyUserInfo);
        }

        public bool checkUserByCompany(int companyClientId, string userName)
        {
            return PLMClientsDataAccessComponent.CompanyUsersDALC.Instance.checkUserByCompany(companyClientId, userName);
        }

        public PLMClientsBusinessEntities.PharmacyUserInfo getPharmacyUser(string userName, string userPassword)
        {
            return PLMClientsDataAccessComponent.CompanyUsersDALC.Instance.getPharmacyUser(userName, userPassword);
        }

        public PLMClientsBusinessEntities.PharmacyUserInfo getPharmacyUserByCode(string code)
        {
            return PLMClientsDataAccessComponent.CompanyUsersDALC.Instance.getPharmacyUserByCode(code);
        }

        public PLMClientsBusinessEntities.WebApplicationUsersInfo getWebApplicationUserByCode(string code)
        {
            return PLMClientsDataAccessComponent.CompanyUsersDALC.Instance.getWebApplicationUserByCode(code);
        }

        public PLMClientsBusinessEntities.PharmacyCompanyUsersInfo getCompanyUsersByCode(string blockString)
        {
            return PLMClientsDataAccessComponent.CompanyUsersDALC.Instance.getCompanyUsersByCode(blockString);
        }

        #endregion

        public static readonly CompanyUsersBLC Instance = new CompanyUsersBLC();

    }
}
