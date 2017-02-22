using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PLMClientsBusinessEntities;

namespace PLMClientsBusinessLogicComponent
{
    public class LicensesBLC
    {

        #region Constructors

        private LicensesBLC() { }

        #endregion

        #region Public Methods

        public int addLicense(PLMClientsBusinessEntities.LicensesInfo BEntity)
        {
            return PLMClientsDataAccessComponent.LicensesDALC.Instance.insert(BEntity);
        }

        public void updateLicense(PLMClientsBusinessEntities.LicensesInfo BEntity)
        {
            PLMClientsDataAccessComponent.LicensesDALC.Instance.update(BEntity);
        }

        public PLMClientsBusinessEntities.LicensesInfo getLicense(int licenseId)
        {
            return PLMClientsDataAccessComponent.LicensesDALC.Instance.getOne(licenseId);
        }

        public PLMClientsBusinessEntities.LicensesInfo getLicenseByKey(string licenseKey)
        {
            if (string.IsNullOrEmpty(licenseKey))
                throw new ArgumentNullException("LicenseKey is null");

            return PLMClientsDataAccessComponent.LicensesDALC.Instance.getByLicenseKey(licenseKey);
        }

        public bool checkLicense(string licenseKey, string codePrefix)
        {
            if (string.IsNullOrEmpty(licenseKey) || string.IsNullOrEmpty(codePrefix))
                throw new ArgumentNullException("LicenseKey or CodePrefix is null");

            return PLMClientsDataAccessComponent.LicensesDALC.Instance.checkLicense(licenseKey, codePrefix);
        }

        public int getNumberInstallations(string licenseKey, string codePrefix)
        {
            if (string.IsNullOrEmpty(licenseKey) || string.IsNullOrEmpty(codePrefix))
                throw new ArgumentNullException("LicenseKey or CodePrefix is null");

            return PLMClientsDataAccessComponent.LicensesDALC.Instance.getNumberInstallations(licenseKey, codePrefix);
        }

        public PLMClientsBusinessEntities.LicensesInfo getLicenseByClientIdByPrefix(int clientId, int prefix)
        {
            if(clientId == 0 || prefix == 0)
                throw new ArgumentNullException("clientId or CodePrefix is null");

            return PLMClientsDataAccessComponent.LicensesDALC.Instance.getLicenseByClientIdByPrefix(clientId,prefix);
        }

        public PLMClientsBusinessEntities.LicensesInfo generateLicense()
        {
           string license = _createLicense.generateLicense();
           int validate = 1;
           do
           {
               PLMClientsBusinessEntities.LicensesInfo validateLicense = new PLMClientsBusinessEntities.LicensesInfo();
               if(PLMClientsDataAccessComponent.LicensesDALC.Instance.getByLicenseKey(license) == null)
               {
                   validateLicense.LicenseKey = license;
                   validate = 0;
                   return validateLicense;
               }
           } while (validate == 1)  ;
           
           return null;
        }

        public int updateHWIdentifierByLicenseId (string license, string HWIdentifier)
        {
            if(string.IsNullOrEmpty(license) && string.IsNullOrEmpty(HWIdentifier))
                throw new ArgumentNullException("license or HWIdentifier is null");
                
          
          return  PLMClientsDataAccessComponent.LicensesDALC.Instance.updateLicenseDownloadClient(license, HWIdentifier);
        }

        public void insertDistributionLicenses(PLMClientsBusinessEntities.DistributionLicensesInfo BEntity)
        {
            if (BEntity.DistributionId == 0 || BEntity.LicenseId == 0 || BEntity.PrefixId == 0 || BEntity.TargetId == 0)
                throw new ArgumentNullException("BEntity is null");
            else
                PLMClientsDataAccessComponent.DistributionLicensesDALC.Instance.insert(BEntity);

        }

        public List<LicensesInfo> getLicenses()
        {
            return PLMClientsDataAccessComponent.LicensesDALC.Instance.getLicenses();
        }

        public void insert(LicensesInfo businessEntity)
        {
            PLMClientsDataAccessComponent.LicensesDALC.Instance.insert(businessEntity);
        }

        #endregion

        #region private methods
        public string createLicense()
        {
            return _createLicense.generateLicense();
        }
        #endregion

        public static readonly LicensesBLC Instance = new LicensesBLC();
        private PLMLicenseComponent.LicenseComponent _createLicense = new PLMLicenseComponent.LicenseComponent();
    }
}
