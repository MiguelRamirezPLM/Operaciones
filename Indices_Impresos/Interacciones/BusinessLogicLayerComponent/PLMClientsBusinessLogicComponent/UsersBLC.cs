using System;
using System.Collections.Generic;
using System.Text;
using PLMClientsBusinessEntities;

namespace PLMClientsBusinessLogicComponent
{
    public class UsersBLC
    {
        #region Constructors

        private UsersBLC() { }

        #endregion

        #region Public Methods

        public int addUser(UserInfo BEntity)
        {
            return PLMClientsDataAccessComponent.UsersDALC.Instance.insert(BEntity);
        }

        public void updateUser(UserInfo BEntity)
        {
            PLMClientsDataAccessComponent.UsersDALC.Instance.update(BEntity);
        }

        public void removeUser(int userId)
        {
            if (userId <= 0)
                throw new ArgumentException("The user does not exist.");

            PLMClientsDataAccessComponent.UsersDALC.Instance.delete(userId);
        }

        public UserInfo getUser(int userId)
        {
            if (userId <= 0)
                throw new ArgumentException("The user does not exist.");

            return PLMClientsDataAccessComponent.UsersDALC.Instance.getOne(userId);
        }

        public UserByClient getUser(string nickName, string password)
        {
            return PLMClientsDataAccessComponent.UsersDALC.Instance.getByNickNameByPass(nickName, password);
        }

        public UserByClient getUserByMail(string email)
        {
            return PLMClientsDataAccessComponent.UsersDALC.Instance.getByEmail(email);
        }

        public UserByClient getUserByCode(string nickName, string codeString)
        {
            return PLMClientsDataAccessComponent.UsersDALC.Instance.getByNickNameByCode(nickName, codeString);
        }

        public UserByCode getUserByKey(string codeString)
        {
            return PLMClientsDataAccessComponent.UsersDALC.Instance.getByCode(codeString);
        }

        public UserByCode getUserByKey(string codeString, string nickName)
        {
            return PLMClientsDataAccessComponent.UsersDALC.Instance.getByCode(codeString, nickName);
        }

        public UserByCode getUserByKey(int userId)
        {
            if (userId <= 0)
                throw new ArgumentException("The user does not exist.");

            return PLMClientsDataAccessComponent.UsersDALC.Instance.getByCode(userId);
        }

        public UserByClient getClientByUser(int userId)
        {
            if (userId <= 0)
                throw new ArgumentException("The user does not exist.");

            return PLMClientsDataAccessComponent.UsersDALC.Instance.getByUser(userId);
        }

        public bool chekNickName(string nickName)
        {
            return PLMClientsDataAccessComponent.UsersDALC.Instance.checkNickName(nickName);
        }

        public UserInfo getByNickName(string nickName)
        {
            if (string.IsNullOrEmpty(nickName) || string.IsNullOrWhiteSpace(nickName))
                throw new ArgumentException("Missing parameter", "nickName");

            return PLMClientsDataAccessComponent.UsersDALC.Instance.getByNickName(nickName);
        }

        #endregion

        public static readonly UsersBLC Instance = new UsersBLC();

    }
}
