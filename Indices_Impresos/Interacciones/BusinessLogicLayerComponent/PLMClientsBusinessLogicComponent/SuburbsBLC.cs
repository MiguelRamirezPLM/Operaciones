using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PLMClientsBusinessEntities;

namespace PLMClientsBusinessLogicComponent
{
    public class SuburbsBLC
    {
         #region Constructors

        private SuburbsBLC() { }

        #endregion

        public List<SuburbsInfo> getSuburbsByLocation(int locationId) 
        {
            return PLMClientsDataAccessComponent.SuburbsDALC.Instance.getSuburbsByLocations(locationId);
        }

        public List<SuburbZipCodeInfo> getSuburbsZipCodeByLocation(int locationId,int suburbId)
        {
            return PLMClientsDataAccessComponent.SuburbsDALC.Instance.getSuburbsZipCodeByLocation(locationId,suburbId);
        }

        public static readonly SuburbsBLC Instance = new SuburbsBLC();
    }
}
