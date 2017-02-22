using System;
using System.Collections.Generic;
using System.Text;
using PLMUserBusinessEntries;

namespace PLMUsersBusinessLogicComponent
{
    public sealed class UsersBLC
    {
        #region Constructors

        private UsersBLC() { }

        #endregion

        #region Public Methods

        public CountriesByUserInfo getUserC(string nick, string pass)
        {
            return PLMUsersDataAccessComponent.UsersDALC.Instance.getUserContries(nick, pass);
        }

        public UserInfo getUser(string nick, string pass)
        {
            return PLMUsersDataAccessComponent.UsersDALC.Instance.getUser(nick, pass);
        }

        public UserInfo getUser(string mail)
        {
            return PLMUsersDataAccessComponent.UsersDALC.Instance.getUser(mail);
        }

        public void insert(UserInfo businessEntity)
        {
            PLMUsersDataAccessComponent.UsersDALC.Instance.insert(businessEntity);            
        }

        public List<UserInfo> getUsers()
        {
            return PLMUsersDataAccessComponent.UsersDALC.Instance.getUsers();
        }

        public List<UserInfo> getUsersByApplication()
        {
            return PLMUsersDataAccessComponent.UsersDALC.Instance.getUsersByApplication();
        }

        public UserInfo getOne(int userId)
        {
            return PLMUsersDataAccessComponent.UsersDALC.Instance.getOne(userId);
        }

        #endregion


        public static readonly UsersBLC Instance = new UsersBLC();
    }
}
