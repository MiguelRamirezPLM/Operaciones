using System;
using System.Collections.Generic;
using System.Text;

namespace PLMClientsBusinessLogicComponent
{
    public class AddressesBLC
    {
        #region Constructors

        private AddressesBLC() { }

        #endregion

        #region Public Methods

        public int addAddress(PLMClientsBusinessEntities.AddressInfo BEntity)
        {
            return PLMClientsDataAccessComponent.AddressesDALC.Instance.insert(BEntity);
        }

        public void updateAddress(PLMClientsBusinessEntities.AddressInfo BEntity)
        {
            PLMClientsDataAccessComponent.AddressesDALC.Instance.update(BEntity);
        }

        public void removeAddress(int addressId)
        {
            if (addressId <= 0)
                throw new ArgumentException("The address does not exist.");

            PLMClientsDataAccessComponent.AddressesDALC.Instance.delete(addressId);
        }

        public PLMClientsBusinessEntities.AddressInfo getAddress(int addressId)
        {
            if (addressId <= 0)
                throw new ArgumentException("The address does not exist.");

            return PLMClientsDataAccessComponent.AddressesDALC.Instance.getOne(addressId);
        }

        public PLMClientsBusinessEntities.AddressByClient getAddressByClient(int clientId)
        {
            return PLMClientsDataAccessComponent.AddressesDALC.Instance.getByClient(clientId);
        }

        #endregion

        public static readonly AddressesBLC Instance = new AddressesBLC();

    }
}
