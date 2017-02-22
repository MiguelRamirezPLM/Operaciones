using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PLMClientsBusinessLogicComponent
{
    public class UserDevicesBLC
    {
        #region Constructors

        private UserDevicesBLC() { }

        #endregion

        #region Public Methods

        public void addDevice(PLMClientsBusinessEntities.UserDeviceInfo businessEntity)
        {
            PLMClientsDataAccessComponent.UserDevicesDALC.Instance.insert(businessEntity);
        }

        public void removeDevice(PLMClientsBusinessEntities.UserDeviceInfo businessEntity)
        {
            PLMClientsDataAccessComponent.UserDevicesDALC.Instance.delete(businessEntity);
        }

        #endregion

        public static readonly UserDevicesBLC Instance = new UserDevicesBLC();

    }
}
