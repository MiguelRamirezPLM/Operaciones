using System;
using System.Collections.Generic;
using System.Text;
using PLMClientsBusinessEntities;

namespace PLMClientsBusinessLogicComponent
{
    public class DistributionsBLC
    {
        #region Constructors

        private DistributionsBLC() { }

        #endregion


        #region Public Methods

        public List<DistributionsInfo> getDistributions()
        {
            return PLMClientsDataAccessComponent.DistributionsDALC.Instance.getDistributions();
        }

        public void insert(DistributionsInfo businessEntity) 
        {
            PLMClientsDataAccessComponent.DistributionsDALC.Instance.insert(businessEntity);
        }

        public DistributionsInfo getDistribution(int distributionId)
        {
            if (distributionId <= 0)
                throw new ArgumentException("The distribution does not exist.");

            PLMClientsBusinessEntities.DistributionsInfo m = new DistributionsInfo();

            m.DistributionId = distributionId;

            PLMClientsBusinessEntities.DistributionsInfo di = PLMClientsDataAccessComponent.DistributionsDALC.Instance.getOne(distributionId);

            return di;
        }

/*
        public DistributionsInfo getCodePrefix(string codePrefix)
        {
            return PLMClientsDataAccessComponent.DistributionsDALC.Instance.getByPrefixName(codePrefix);
        }
*/
        public void updateCode(DistributionsInfo BEntity)
        {
            PLMClientsDataAccessComponent.DistributionsDALC.Instance.update(BEntity);
        }
/*
        public DistributionsInfo getCodePrefixByParent(int parentId)
        {
            if (parentId <= 0)
                throw new ArgumentException("The prefix does not exist.");

            return PLMClientsDataAccessComponent.DistributionsDALC.Instance.getByParentId(parentId);
        }

        public DistributionsInfo getByCode(String codeString)
        {
            if (codeString == null)
                throw new ArgumentException("The prefix does not exist.");

            return PLMClientsDataAccessComponent.DistributionsDALC.Instance.getByCode(codeString);
        }
*/
        #endregion

        public static readonly DistributionsBLC Instance = new DistributionsBLC();
    }
}
