using System;
using System.Collections.Generic;
using System.Text;

namespace PLMClientsBusinessLogicComponent
{
    public class SessionsBLC
    {

        #region Constructors

        private SessionsBLC() { }

        #endregion

        #region Public Methods

        public int checkSession(int userId, int sessionTime)
        {
            if (userId <= 0 || sessionTime <= 0)
                throw new ArgumentException("The user does not exist.");

            return PLMClientsDataAccessComponent.SessionsDALC.Instance.checkSessionStatus(userId, sessionTime);

        }

        public List<PLMClientsBusinessEntities.UserSessionInfo> getSessions(int userId)
        {
            if (userId <= 0)
                throw new ArgumentException("The user does not exist.");

            return PLMClientsDataAccessComponent.SessionsDALC.Instance.getByUserId(userId);
        }

        public int addSession(PLMClientsBusinessEntities.UserSessionInfo BEntity)
        {
            return PLMClientsDataAccessComponent.SessionsDALC.Instance.insert(BEntity);
        }

        #endregion

        public static readonly SessionsBLC Instance = new SessionsBLC();

    }
}
