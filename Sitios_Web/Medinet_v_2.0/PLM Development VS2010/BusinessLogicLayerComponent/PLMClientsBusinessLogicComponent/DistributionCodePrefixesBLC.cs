using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PLMClientsBusinessEntities;

namespace PLMClientsBusinessLogicComponent
{
    public class DistributionCodePrefixesBLC
    {

        #region Constructors

        private DistributionCodePrefixesBLC() { }

        #endregion

        #region Public Methods

        public PLMClientsBusinessEntities.DistributionCodePrefixesInfo getDistributionCodePrefix(int prefixId)
        {
            if (prefixId <= 0)
                throw new Exception("The Prefix does not exist.");
            else
                return PLMClientsDataAccessComponent.DistributionCodePrefixesDALC.Instance.getDistributionCodePrefix(prefixId);
        }

        public PLMClientsBusinessEntities.DistributionCodePrefixesInfo getDistributionCodePrefix(int distributionId, int prefixId, byte targetId)
        {
            if (distributionId <= 0 || prefixId <= 0 || targetId <= 0)
                throw new Exception("The Distribution or Prefix or Target does not exist.");
            else
                return PLMClientsDataAccessComponent.DistributionCodePrefixesDALC.Instance.getDistributionCodePrefix(distributionId, prefixId, targetId);
        }


        public void insert(DistributionCodePrefixesInfo businessEntity)
        {
            PLMClientsDataAccessComponent.DistributionCodePrefixesDALC.Instance.insert(businessEntity);
        }

        
        public List<DistributionCodePrefixInfo> getDistributionsCountries(int countryId)
        {
            if (countryId <= 0)
                throw new ArgumentException("The distribution does not exist.");

            return PLMClientsDataAccessComponent.DistributionCountriesDALC.Instance.getDistributionCountriesAll(countryId);

        }

        public List<DistributionCodePrefixInfo> getPrefixDistribution(int countryId, int distributionId)

        {
            if (countryId <= 0 || distributionId <= 0)
                throw new ArgumentException("The prefix does not exist.");

            return PLMClientsDataAccessComponent.DistributionPrefixesDALC.Instance.getPrefixAll(countryId, distributionId);

        }


        public List<DistributionCodePrefixInfo> getTargetDistribution(int distributionId, int prefixId, int countryId)
        {
            if (distributionId <= 0 || prefixId <= 0)
                throw new ArgumentException("The target does not exist.");

            return PLMClientsDataAccessComponent.DistributionTargetDALC.Instance.getTargetAll(distributionId, prefixId, countryId);
        }

        public PLMClientsBusinessEntities.DistributionCodePrefixInfo getTarget(int distributionId, int prefixId, int countryId, int targetId)
        {
            return PLMClientsDataAccessComponent.DistributionTargetDALC.Instance.getTarget(distributionId, prefixId, countryId, targetId);
        }

        #endregion

        public static readonly DistributionCodePrefixesBLC Instance = new DistributionCodePrefixesBLC();

    }
}
