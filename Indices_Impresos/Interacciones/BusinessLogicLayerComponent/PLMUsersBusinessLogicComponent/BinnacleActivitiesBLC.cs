using System;
using System.Collections.Generic;
using System.Text;
using PLMUserBusinessEntries;

namespace PLMUsersBusinessLogicComponent
{
    public sealed class BinnacleActivitiesBLC
    {
        #region Constructors

        private BinnacleActivitiesBLC() { }

        #endregion

        #region Public Methods

        public List<BinnacleActivitiesInfo> getBinnacleActivities()
        {
            return PLMUsersDataAccessComponent.BinnacleActivitiesDALC.Instance.getBinnacleActivities();
        }

        public void insert(BinnacleActivitiesInfo businessEntity)
        {
            PLMUsersDataAccessComponent.BinnacleActivitiesDALC.Instance.insert(businessEntity);
        }

        public void update(BinnacleActivitiesInfo businessEntity)
        {
            PLMUsersDataAccessComponent.BinnacleActivitiesDALC.Instance.update(businessEntity);
        }

        #endregion
        
        public static readonly BinnacleActivitiesBLC Instance = new BinnacleActivitiesBLC();
    }
}