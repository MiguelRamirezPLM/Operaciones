using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using PLMClientsBusinessEntities;

namespace PLMClientsBusinessLogicComponent
{
    public sealed class StateOSMobileUserCodesBLC
    {
        #region Constructors

        private StateOSMobileUserCodesBLC() { }

        #endregion

        #region Public Methods

        public void addMobileToState(StateOSMobileUserCodeInfo businessEntity)
        {
            PLMClientsDataAccessComponent.StateOSMobileUserCodesDALC.Instance.insert(businessEntity);
        }

        public void removeMobileToState(StateOSMobileUserCodeInfo businessEntity)
        {
            PLMClientsDataAccessComponent.StateOSMobileUserCodesDALC.Instance.delete(businessEntity);
        }

        public StateOSMobileUserCodeInfo getMobile(StateOSMobileUserCodeInfo businessEntity)
        {
            return PLMClientsDataAccessComponent.StateOSMobileUserCodesDALC.Instance.getOne(businessEntity);
        }

        public StateOSMobileUserCodeInfo getMobile(byte osMobileId, int codeId, int userId)
        {
            return PLMClientsDataAccessComponent.StateOSMobileUserCodesDALC.Instance.getByMobile(osMobileId, codeId, userId);
        }

        #endregion

        public static readonly StateOSMobileUserCodesBLC Instance = new StateOSMobileUserCodesBLC();

    }
}
