using System;
using System.Collections.Generic;
using System.Text;
using PLMUserBusinessEntries;

namespace PLMUsersBusinessLogicComponent
{
    public sealed class UserProjectsBLC
    {
        #region Constructors

        private UserProjectsBLC() { }

        #endregion

        #region Public Methods

        public List<UserProjectInfo> getUserProjects()
        {
            return PLMUsersDataAccessComponent.UserProjectsDALC.Instance.getUserProjects();
        }

        public void insert(UserProjectInfo businessEntity)
        {
            PLMUsersDataAccessComponent.UserProjectsDALC.Instance.insert(businessEntity);
        }

        #endregion

        public static readonly UserProjectsBLC Instance = new UserProjectsBLC();

    }
}
