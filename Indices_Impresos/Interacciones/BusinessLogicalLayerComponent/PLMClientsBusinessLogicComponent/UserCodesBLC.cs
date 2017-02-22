using System;
using System.Collections.Generic;
using System.Text;
using PLMClientsBusinessEntities;

namespace PLMClientsBusinessLogicComponent
{
    public class UserCodesBLC
    {
        #region Constructors

        private UserCodesBLC() { }

        #endregion

        #region Public Methods

        public void addCodeToUser(UserCodeInfo BEntity)
        {
            PLMClientsDataAccessComponent.UserCodesDALC.Instance.insert(BEntity);
        }

        public void removeCodeToUser(UserCodeInfo BEntity)
        {
            PLMClientsDataAccessComponent.UserCodesDALC.Instance.delete(BEntity);
        }

        public bool checkUser(int userId, int codeId)
        {
            if (userId <= 0 || codeId <= 0)
                throw new ArgumentException("The user or code does not exist.");

            return PLMClientsDataAccessComponent.UserCodesDALC.Instance.checkUser(userId, codeId);
        }

        #endregion

        public static readonly UserCodesBLC Instance = new UserCodesBLC();

    }
}
