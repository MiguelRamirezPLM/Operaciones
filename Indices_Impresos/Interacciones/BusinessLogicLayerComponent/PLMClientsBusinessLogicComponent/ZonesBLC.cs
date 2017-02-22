using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PLMClientsBusinessEntities;

namespace PLMClientsBusinessLogicComponent
{
    public class ZonesBLC
    {

        #region Constructors

        private ZonesBLC() { }

        #endregion

        #region Public Methods

        public List<PLMClientsBusinessEntities.ZonesInfo> getZones()
        {
            return PLMClientsDataAccessComponent.ZonesDALC.Instance.getZones();
        }

        public List<PLMClientsBusinessEntities.ZonesInfo> getZonesByPrefixByTarget(string prefix, byte targetId)
        {
            return PLMClientsDataAccessComponent.ZonesDALC.Instance.getZonesByPrefixByTarget(prefix, targetId);
        }

        #endregion

        public static readonly ZonesBLC Instance = new ZonesBLC();

    }
}
