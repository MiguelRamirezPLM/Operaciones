using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PLMClientsBusinessEntities;

namespace PLMClientsBusinessLogicComponent
{
    public class LicenseTargetCodesBLC
    {

        #region Constructors

        private LicenseTargetCodesBLC() { }

        #endregion

        #region Public Methods

        public int addLicenseToTargetOutput(PLMClientsBusinessEntities.LicenseTargetCodesInfo BEntity)
        {
            return PLMClientsDataAccessComponent.LicenseTargetCodesDALC.Instance.insert(BEntity);
        }

        public void removeLicenseToTargetOutput(PLMClientsBusinessEntities.LicenseTargetCodesInfo BEntity)
        {
            PLMClientsDataAccessComponent.LicenseTargetCodesDALC.Instance.delete(BEntity);
        }

        public PLMClientsBusinessEntities.LicenseTargetCodesInfo getLicenseByPrefixByHwIdentifier(string prefix, string hwIdentifier, int licenseId)
        {
            return PLMClientsDataAccessComponent.LicenseTargetCodesDALC.Instance.getLicenseByPrefixByHwIdentifier(prefix, hwIdentifier, licenseId);
        }

        public PLMClientsBusinessEntities.LicenseDetailInfo getLicenseByCodeByDevice(int clientId, int codeId, byte targetId, byte deviceId, int licenseId)
        {
            return PLMClientsDataAccessComponent.LicenseTargetCodesDALC.Instance.getLicenseByCodeByDevice(clientId, codeId, targetId, deviceId, licenseId);
        }

        #endregion

        public static readonly LicenseTargetCodesBLC Instance = new LicenseTargetCodesBLC();

    }
}
