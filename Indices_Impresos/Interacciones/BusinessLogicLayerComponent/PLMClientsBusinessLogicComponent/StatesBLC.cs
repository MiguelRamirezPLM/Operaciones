using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PLMClientsBusinessEntities;

namespace PLMClientsBusinessLogicComponent
{
    public class StatesBLC
    {

        #region Constructors

        private StatesBLC() { }

        #endregion

        #region Public Methods

        public List<StateInfo> getStateByCountry(byte countryId)
        {
            return PLMClientsDataAccessComponent.StatesDALC.Instance.getByCountry(countryId);
        }

        public StateInfo getState(int stateId)
        {
            return PLMClientsDataAccessComponent.StatesDALC.Instance.getOne(stateId);
        }

        public List<StateInfo> getStatesByClientAdvisor(int clientId)
        {
            return PLMClientsDataAccessComponent.StatesDALC.Instance.getByClientAdvisor(clientId);
        }

        #endregion

        public static readonly StatesBLC Instance = new StatesBLC();

    }
}
