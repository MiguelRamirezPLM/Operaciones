using System;
using System.Collections.Generic;
using System.Text;
using PLMUserBusinessEntries;

namespace PLMUsersBusinessLogicComponent
{
    public sealed class BinnaclesBLC
    {
        #region Constructors

        private BinnaclesBLC() { }

        #endregion

        #region Public Methods

        public List<BinnaclesInfo> getBinnacles()
        {
            return PLMUsersDataAccessComponent.BinnaclesDALC.Instance.getBinnacles();
        }

        public BinnaclesInfo getBinnacle(int binnacleId)
        {
            if (binnacleId <= 0)
                throw new ArgumentException("The binnacles does not exist.");

            return
                PLMUsersDataAccessComponent.BinnaclesDALC.Instance.getOne(binnacleId);        
        }

        public List<BinnaclesStatusNameInfo> getBinnaclesStatusNameByUser(int userId)
        {
            return PLMUsersDataAccessComponent.BinnaclesStatusNameDALC.Instance.getBinnaclesByUser(userId);
        }

        public void insert(BinnaclesInfo businessEntity)
        {
            PLMUsersDataAccessComponent.BinnaclesDALC.Instance.insert(businessEntity);
        }

        public void update(BinnaclesInfo businessEntity)
        {
            PLMUsersDataAccessComponent.BinnaclesDALC.Instance.update(businessEntity);
        }

        public BinnaclesInfo getOne(int binnacleId)
        {
            return PLMUsersDataAccessComponent.BinnaclesDALC.Instance.getOne(binnacleId);
        }

        public List<BinnaclesInfo> getBinnaclesByUser(int userId)
        {
            return PLMUsersDataAccessComponent.BinnaclesDALC.Instance.getBinnaclesByUser(userId);
        }

        #endregion
        
        public static readonly BinnaclesBLC Instance = new BinnaclesBLC();
    }
}
