using System;
using System.Collections.Generic;
using System.Text;
using PLMClientsBusinessEntities;

namespace PLMClientsBusinessLogicComponent
{
    public class ClientUsersBLC
    {
        #region Constructors

        private ClientUsersBLC() { }

        #endregion 

        #region Public Methods

        public void addUserToClient(ClientUserInfo BEntity)
        {
            PLMClientsDataAccessComponent.ClientUsersDALC.Instance.insert(BEntity);
        }

        public void removeUserToClient(ClientUserInfo BEntity)
        {
            PLMClientsDataAccessComponent.ClientUsersDALC.Instance.delete(BEntity);
        }

        #endregion

        public static readonly ClientUsersBLC Instance = new ClientUsersBLC();

    }
}
