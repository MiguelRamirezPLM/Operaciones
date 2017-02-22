using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PLMClientsBusinessEntities;

namespace PLMClientsBusinessLogicComponent
{
    public class DistributionLicensesBLC
    {

        #region Constructors

        private DistributionLicensesBLC() { }

        #endregion

        #region Public Methods

        public PLMClientsBusinessEntities.DistributionLicensesInfo getDistributionLicense(byte targetId, int licenseId)
        {
            return PLMClientsDataAccessComponent.DistributionLicensesDALC.Instance.getDistributionLicense(targetId, licenseId);
        }

        public void insert(DistributionLicensesInfo businessEntity)
        {
            PLMClientsDataAccessComponent.DistributionLicensesDALC.Instance.insert(businessEntity);
        }

        #endregion

        public static readonly DistributionLicensesBLC Instance = new DistributionLicensesBLC();

    }
}
