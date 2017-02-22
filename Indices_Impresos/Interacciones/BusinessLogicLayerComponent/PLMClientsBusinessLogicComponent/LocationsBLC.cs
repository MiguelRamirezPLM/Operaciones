using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PLMClientsBusinessEntities;

namespace PLMClientsBusinessLogicComponent
{
    public class LocationsBLC
    {

        #region Constructors

        private LocationsBLC() { }

        #endregion

        #region Public Methods

        public List<PLMClientsBusinessEntities.LocationsInfo> getLocationsByPrefixByZone(string prefix, byte targetId, byte zoneId)
        {
            return PLMClientsDataAccessComponent.LocationsDALC.Instance.getLocationsByPrefixByZone(prefix, targetId, zoneId);
        }

        public List<PLMClientsBusinessEntities.LocationsInfo> getLocationsByState(int stateId)
        {
            return PLMClientsDataAccessComponent.LocationsDALC.Instance.getLocationsByState(stateId);
        }

        #endregion

        public static readonly LocationsBLC Instance = new LocationsBLC();

    }
}
