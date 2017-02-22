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

        public List<PLMClientsBusinessEntities.AddressInfo> getAddressesByClient(int clientId)
        {
            return PLMClientsDataAccessComponent.AddressesDALC.Instance.getAddressesByClient(clientId);
        }

        public void saveAddresses(int clientId, List<PLMClientsBusinessEntities.AddressInfo> addresses)
        {

            string addressesStr = null;
            
            if (addresses.Count > 0)
            {
                PLMClientsBusinessEntities.AddressInfo address = new PLMClientsBusinessEntities.AddressInfo();

                foreach (PLMClientsBusinessEntities.AddressInfo item in addresses)
                {

                    if (item.PhoneOne != null)
                        address.PhoneOne = item.PhoneOne;
                    else
                        address.PhoneOne = "NULL";

                    if (item.PhoneTwo != null)
                        address.PhoneTwo = item.PhoneTwo;
                    else
                        address.PhoneTwo = "NULL";

                    if (item.Fax != null)
                        address.Fax = item.Fax;
                    else
                        address.Fax = "NULL";

                    if (item.InternalNumber != null)
                        address.InternalNumber = item.InternalNumber;
                    else
                        address.InternalNumber = "NULL";

                    if (item.StateId != null)
                        address.StateId = item.StateId;

                    if (item.StateName != null)
                        address.StateName = item.StateName;
                    else
                        address.StateName = "NULL";

                    if (item.SuburbId != null)
                        address.SuburbId = item.SuburbId;

                    if (item.Suburb != null)
                        address.Suburb = item.Suburb;
                    else
                        address.Suburb = "NULL";

                    if (item.LocationId != null)
                        address.LocationId = item.LocationId;

                    if (item.Location != null)
                        address.Location = item.Location;
                    else
                        address.Location = "NULL";

                    if (item.ZipCodeId != null)
                        address.ZipCodeId = item.ZipCodeId;

                    if (item.ZipCode != null)
                        address.ZipCode = item.ZipCode;
                    else
                        address.ZipCode = "NULL";

                    address.Street = item.Street;

                    // @addresses = '''phone1'',''phone2'',null,''callepRUEBA'',''numint'',9,''statename'',32471,''subname'',740,''locname'',15868,''zip''|

                    addressesStr += "'" + address.PhoneOne.ToString() + "','" + address.PhoneTwo.ToString() + "','" + address.Fax.ToString() + "','"
                            + address.Street.ToString() + "','" + address.InternalNumber.ToString() + "',"
                            + address.StateId.ToString() + ",'" + address.StateName.ToString() + "',"
                            + address.SuburbId.ToString() + ",'" + address.Suburb.ToString() + "',"
                            + address.LocationId.ToString() + ",'" + address.Location.ToString() + "',"
                            + address.ZipCodeId.ToString() + ",'" + address.ZipCode.ToString() + "'|";
                }

                addressesStr = addressesStr.Substring(0, addressesStr.Length - 1);

            }


            PLMClientsDataAccessComponent.AddressesDALC.Instance.saveAddresses(clientId, addressesStr);
        }

        #endregion

        public static readonly AddressesBLC Instance = new AddressesBLC();

    }
}
