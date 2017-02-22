using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PLMClientsBusinessEntities;

namespace PLMClientsBusinessLogicComponent
{
    public class ResidenceLevelsBLC
    {

        #region Constructors

        private ResidenceLevelsBLC() { }

        #endregion

        #region Public Methods

        public PLMClientsBusinessEntities.ResidenceLevelsInfo getResidenceLevel(byte residenceId)
        {
            return PLMClientsDataAccessComponent.ResidenceLevelsDALC.Instance.getResidenceLevel(residenceId);
        }

        public PLMClientsBusinessEntities.ResidenceLevelsInfo getResidenceLevelByKey(string residenceKey)
        {
            return PLMClientsDataAccessComponent.ResidenceLevelsDALC.Instance.getResidenceLevel(residenceKey);
        }

        public List<PLMClientsBusinessEntities.ResidenceLevelsInfo> getResidenceLevelsBySpeciality(short specialityId)
        {
            return PLMClientsDataAccessComponent.ResidenceLevelsDALC.Instance.getResidenceLevelsBySpeciality(specialityId);
        }

        public PLMClientsBusinessEntities.ResidenceLevelsInfo getResidenceLevelByClient(int clientId)
        {
            return PLMClientsDataAccessComponent.ResidenceLevelsDALC.Instance.getResidenceLevelByClient(clientId);
        }

        #endregion

        public static readonly ResidenceLevelsBLC Instance = new ResidenceLevelsBLC();

    }
}
