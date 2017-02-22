using System;
using System.Collections.Generic;
using System.Text;

namespace PLMClientsBusinessLogicComponent
{
    public class ClientAddressesBLC
    {
        #region Constructors

        private ClientAddressesBLC(){}
        
        #endregion

        #region Public Methods

        public void addAddress(PLMClientsBusinessEntities.ClientAddressInfo BEntity)
        {
            PLMClientsDataAccessComponent.ClientAddressesDALC.Instance.insert(BEntity);
        }

        public void removeAddress(PLMClientsBusinessEntities.ClientAddressInfo BEntity)
        {
            PLMClientsDataAccessComponent.ClientAddressesDALC.Instance.delete(BEntity);
        }

        public void removeAddress(int clientId)
        {
            if(clientId <= 0)
                throw new ArgumentException("The client does not exist.");

            PLMClientsDataAccessComponent.ClientAddressesDALC.Instance.delete(clientId);
        }
        
        public PLMClientsBusinessEntities.ClientAddressInfo getAddress(int clientId, int addressId)
        {
            if (clientId <= 0 || addressId <= 0)
                throw new ArgumentException("The client or address does not exist.");

            return PLMClientsDataAccessComponent.ClientAddressesDALC.Instance.getOne(clientId, addressId);
        }

        #endregion

        public static readonly ClientAddressesBLC Instance = new ClientAddressesBLC();

    }
}
