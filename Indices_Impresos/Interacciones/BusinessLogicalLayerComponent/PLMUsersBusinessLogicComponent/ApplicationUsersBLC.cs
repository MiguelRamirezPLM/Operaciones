using System;
using System.Collections.Generic;
using System.Text;
using PLMUserBusinessEntries;

namespace PLMUsersBusinessLogicComponent
{
    public sealed class ApplicationUsersBLC
    {
        #region Constructors

        private ApplicationUsersBLC() { }

        #endregion

        #region Public Methods

        public ApplicationInfo getApplication(string hash)
        {
            return PLMUsersDataAccessComponent.ApplicationDALC.Instance.getOne(hash);
        }

        public ApplicationInfo getApplDocument(int applicationId)
        {
            return PLMUsersDataAccessComponent.ApplicationDALC.Instance.getOne(applicationId);
        }

        public ApplicationUserInfo getUserAppl(int applId, int userId)
        {
            return PLMUsersDataAccessComponent.ApplicationUserDALC.Instance.getOne(applId, userId);
        }

        public RoleInfo getRole(int userId, string hashKey)
        {
            if (userId <= 0)
                throw new ArgumentException("The user does not exist.");

            return PLMUsersDataAccessComponent.RoleDALC.Instance.getByUser(userId, hashKey);
        }

        public List<DocumentInfo> getDocument(int userId)
        {
            if (userId <= 0)
                throw new ArgumentException("The user does not exist.");

            return PLMUsersDataAccessComponent.DocumentDALC.Instance.getDocumentFile(userId);
                
        }

        #endregion


        public static readonly ApplicationUsersBLC Instance = new ApplicationUsersBLC();

    }
}
