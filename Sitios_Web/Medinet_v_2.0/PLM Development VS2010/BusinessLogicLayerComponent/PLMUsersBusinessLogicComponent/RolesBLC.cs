using System;
using System.Collections.Generic;
using System.Text;
using PLMUserBusinessEntries;

namespace PLMUsersBusinessLogicComponent
{
    public sealed class RolesBLC
    {        
        #region Constructors

        private RolesBLC() { }

        #endregion

        #region Public Methods

        public List<RoleInfo> getRoles()
        {
            return PLMUsersDataAccessComponent.RoleDALC.Instance.getRoles();
        }

        public void insert(RoleInfo businessEntity)
        {
            PLMUsersDataAccessComponent.RoleDALC.Instance.insert(businessEntity);
        }

        #endregion

        public static readonly RolesBLC Instance = new RolesBLC();
    }
}

  