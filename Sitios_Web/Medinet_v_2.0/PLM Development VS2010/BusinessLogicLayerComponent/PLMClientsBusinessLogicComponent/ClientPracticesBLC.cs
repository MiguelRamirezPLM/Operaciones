using System;
using System.Collections.Generic;
using System.Text;

namespace PLMClientsBusinessLogicComponent
{
    public class ClientPracticesBLC
    {
        #region Constructors

        private ClientPracticesBLC() { }

        #endregion

        #region Public Methods

        public void addPractice(PLMClientsBusinessEntities.ClientPracticeInfo BEntity)
        {
            PLMClientsDataAccessComponent.ClientPracticesDALC.Instance.insert(BEntity);
        }

        public void removePractice(PLMClientsBusinessEntities.ClientPracticeInfo BEntity)
        {
            PLMClientsDataAccessComponent.ClientPracticesDALC.Instance.delete(BEntity);
        }

        public void removePractice(int clientId)
        {
            if (clientId <= 0)
                throw new ArgumentException("The client does not exist.");

            PLMClientsDataAccessComponent.ClientPracticesDALC.Instance.delete(clientId);
        }

        public List<PLMClientsBusinessEntities.ClientPracticeInfo> getPracticeByUser(int clientId)
        {
            if (clientId <= 0)
                throw new ArgumentException("The client does not exist.");

            return PLMClientsDataAccessComponent.ClientPracticesDALC.Instance.getByClient(clientId);
        }

        public bool checkPractice(int clientId, byte practiceId)
        {
            if (clientId <= 0 || practiceId == 0)
                throw new ArgumentException("The client or practice type does not exist.");

            return PLMClientsDataAccessComponent.ClientPracticesDALC.Instance.checkPractice(clientId, practiceId);
        }

        #endregion

        public static readonly ClientPracticesBLC Instance = new ClientPracticesBLC();
    }
}
