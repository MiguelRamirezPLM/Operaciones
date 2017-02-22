using System;
using System.Collections.Generic;
using System.Text;

namespace PLMClientsBusinessLogicComponent
{
    public class ProfessionClientsBLC
    {
        #region Constructors

        private ProfessionClientsBLC() { }

        #endregion

        #region Public Methods

        public void addProfession(PLMClientsBusinessEntities.ProfessionClientInfo BEntity)
        {
            PLMClientsDataAccessComponent.ProfessionClientsDALC.Instance.insert(BEntity);
        }

        public void removeProfession(PLMClientsBusinessEntities.ProfessionClientInfo BEntity)
        {
            PLMClientsDataAccessComponent.ProfessionClientsDALC.Instance.delete(BEntity);
        }

        public void updateProfession(PLMClientsBusinessEntities.ProfessionClientInfo BEntity)
        {
            PLMClientsDataAccessComponent.ProfessionClientsDALC.Instance.update(BEntity);
        }

        public PLMClientsBusinessEntities.ProfessionClientInfo getProfessionByClient(int clientId)
        {
            if (clientId <= 0)
                throw new ArgumentException("The client does not exist.");

            return PLMClientsDataAccessComponent.ProfessionClientsDALC.Instance.getByClient(clientId);
        }

        #endregion

        public static readonly ProfessionClientsBLC Instance = new ProfessionClientsBLC();

    }
}
