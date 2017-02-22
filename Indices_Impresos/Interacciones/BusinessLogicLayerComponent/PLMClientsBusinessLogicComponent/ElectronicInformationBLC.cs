using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PLMClientsBusinessEntities;

namespace PLMClientsBusinessLogicComponent
{
    public class ElectronicInformationBLC
    {

        #region Constructors

        private ElectronicInformationBLC() { }

        #endregion

        #region Public Methods

        public List<PLMClientsBusinessEntities.ElectronicInformationByTargetInfo> getInformationByType(byte informationTypeId, string country, string applicationKey)
        {
            List<PLMClientsBusinessEntities.ElectronicInformationByTargetInfo> electronicList = new List<ElectronicInformationByTargetInfo>();

            electronicList = PLMClientsDataAccessComponent.ElectronicInformationDALC.Instance.getInformationByType(informationTypeId, country);

            if (electronicList != null)
                foreach (PLMClientsBusinessEntities.ElectronicInformationByTargetInfo electronic in electronicList)
                {
                    electronic.BaseUrl = PLMUsersBusinessLogicComponent.ApplicationUsersBLC.Instance.getApplication(applicationKey).RootUrl +
                        PLMClientsDataAccessComponent.CountriesDALC.Instance.getOne((int)ClientsBLC.Instance.getCountryByID(country)).CountryName +
                        "/" + electronic.BaseUrl;
                }
            return electronicList;
        }

        public List<PLMClientsBusinessEntities.ElectronicInformationByTargetInfo> getInformationByPrefix(string prefix, string country, string applicationKey)
        {
            List<PLMClientsBusinessEntities.ElectronicInformationByTargetInfo> electronicList = new List<ElectronicInformationByTargetInfo>();

            electronicList = PLMClientsDataAccessComponent.ElectronicInformationDALC.Instance.getInformationByPrefix(prefix, country);

            if (electronicList != null)
                foreach (PLMClientsBusinessEntities.ElectronicInformationByTargetInfo electronic in electronicList)
                {
                    electronic.BaseUrl = PLMUsersBusinessLogicComponent.ApplicationUsersBLC.Instance.getApplication(applicationKey).RootUrl +
                        PLMClientsDataAccessComponent.CountriesDALC.Instance.getOne((int)ClientsBLC.Instance.getCountryByID(country)).CountryName +
                        "/" + electronic.BaseUrl;
                }
            return electronicList;
        }

        public List<PLMClientsBusinessEntities.ElectronicInformationByTargetInfo> getInformationByPrefixByTarget(string prefix, byte targetId, string country, string applicationKey)
        {
            List<PLMClientsBusinessEntities.ElectronicInformationByTargetInfo> electronicList = new List<ElectronicInformationByTargetInfo>();

            electronicList = PLMClientsDataAccessComponent.ElectronicInformationDALC.Instance.getInformationByPrefixByTarget(prefix, targetId, country);

            if (electronicList != null)
                foreach (PLMClientsBusinessEntities.ElectronicInformationByTargetInfo electronic in electronicList)
                {
                    electronic.BaseUrl = PLMUsersBusinessLogicComponent.ApplicationUsersBLC.Instance.getApplication(applicationKey).RootUrl +
                        PLMClientsDataAccessComponent.CountriesDALC.Instance.getOne((int)ClientsBLC.Instance.getCountryByID(country)).CountryName +
                        "/" + electronic.BaseUrl;
                }
            return electronicList;
        }

        public List<PLMClientsBusinessEntities.ElectronicInformationByTargetInfo> getInformationByPrefixByType(string prefix, byte targetId, byte informationTypeId, string country, string applicationKey)
        {
            List<PLMClientsBusinessEntities.ElectronicInformationByTargetInfo> electronicList = new List<ElectronicInformationByTargetInfo>();

            electronicList = PLMClientsDataAccessComponent.ElectronicInformationDALC.Instance.getInformationByPrefixByType(prefix, targetId, informationTypeId, country);

            if (electronicList != null)
                foreach (PLMClientsBusinessEntities.ElectronicInformationByTargetInfo electronic in electronicList)
                {
                    electronic.BaseUrl = PLMUsersBusinessLogicComponent.ApplicationUsersBLC.Instance.getApplication(applicationKey).RootUrl +
                        PLMClientsDataAccessComponent.CountriesDALC.Instance.getOne((int)ClientsBLC.Instance.getCountryByID(country)).CountryName +
                        "/" + electronic.BaseUrl;
                }
            return electronicList;
        }

        public List<PLMClientsBusinessEntities.ElectronicInformationByTargetInfo> getInformationBySection(string prefix, byte targetId, string country, int sectionId, string resolutionKey, string applicationKey)
        {
            if (prefix == null || targetId <= 0 || country == null || sectionId <= 0 || resolutionKey == null)
            {
                throw new ArgumentException("The prefix or target or country or section or resolution  does not exist");
            }
            else
            {
                List<PLMClientsBusinessEntities.ElectronicInformationByTargetInfo> electronicList = new List<ElectronicInformationByTargetInfo>();

                electronicList = PLMClientsDataAccessComponent.ElectronicInformationDALC.Instance.getInformationBySection(prefix, targetId, country, sectionId, resolutionKey);

                if (electronicList != null)
                    foreach (PLMClientsBusinessEntities.ElectronicInformationByTargetInfo electronic in electronicList)
                    {
                        //electronic.BaseUrl = PLMUsersBusinessLogicComponent.ApplicationUsersBLC.Instance.getApplication(applicationKey).RootUrl +
                        //    PLMClientsDataAccessComponent.CountriesDALC.Instance.getOne((int)ClientsBLC.Instance.getCountryByID(country)).CountryName +
                        //    "/" + electronic.BaseUrl + ResolutionScreensBLC.Instance.getByResolutionKey(resolutionKey).BaseUrl + "/";

                        electronic.BaseUrl = PLMUsersBusinessLogicComponent.ApplicationUsersBLC.Instance.getApplication(applicationKey).RootUrl +
                            PLMClientsDataAccessComponent.CountriesDALC.Instance.getOne((int)ClientsBLC.Instance.getCountryByID(country)).CountryName +
                            "/" + electronic.BaseUrl + electronic.ResolutionBaseUrl + "/";
                    }
                return electronicList;
            }
        }

        //New
        public List<PLMClientsBusinessEntities.ElectronicInformationByTargetInfo> getInformationByPrefixByTypeBySpeciality(string prefix, byte targetId, byte informationTypeId, string country, int specialityId, string applicationKey)
        {
            List<PLMClientsBusinessEntities.ElectronicInformationByTargetInfo> electronicList = new List<ElectronicInformationByTargetInfo>();

            electronicList = PLMClientsDataAccessComponent.ElectronicInformationDALC.Instance.getInformationByPrefixByTypeBySpeciality(prefix, targetId, informationTypeId, country, specialityId);

            if (electronicList != null)
                foreach (PLMClientsBusinessEntities.ElectronicInformationByTargetInfo electronic in electronicList)
                {
                    electronic.BaseUrl = PLMUsersBusinessLogicComponent.ApplicationUsersBLC.Instance.getApplication(applicationKey).RootUrl +
                        PLMClientsDataAccessComponent.CountriesDALC.Instance.getOne((int)ClientsBLC.Instance.getCountryByID(country)).CountryName +
                        "/" + electronic.BaseUrl;
                }
            return electronicList;
        }

        public List<PLMClientsBusinessEntities.ElectronicInformationByTargetInfo> getInformationByPrefixByTypeByICD(string prefix, byte targetId, byte informationTypeId, string country, int icdId, string applicationKey)
        {
            List<PLMClientsBusinessEntities.ElectronicInformationByTargetInfo> electronicList = new List<ElectronicInformationByTargetInfo>();

            electronicList = PLMClientsDataAccessComponent.ElectronicInformationDALC.Instance.getInformationByPrefixByTypeByICD(prefix, targetId, informationTypeId, country, icdId);

            if (electronicList != null)
                foreach (PLMClientsBusinessEntities.ElectronicInformationByTargetInfo electronic in electronicList)
                {
                    electronic.BaseUrl = PLMUsersBusinessLogicComponent.ApplicationUsersBLC.Instance.getApplication(applicationKey).RootUrl +
                        PLMClientsDataAccessComponent.CountriesDALC.Instance.getOne((int)ClientsBLC.Instance.getCountryByID(country)).CountryName +
                        "/" + electronic.BaseUrl;
                }

            return electronicList;

        }

        public List<PLMClientsBusinessEntities.ElectronicInformationByTargetInfo> getMedicalGuidelinesByText(string prefix, string text)
        {
            List<PLMClientsBusinessEntities.ElectronicInformationByTargetInfo> electronicList = new List<ElectronicInformationByTargetInfo>();

            electronicList = PLMClientsDataAccessComponent.ElectronicInformationDALC.Instance.getMedicalGuidelinesByText(prefix, text);


            //if (electronicList != null)
            //    foreach (PLMClientsBusinessEntities.ElectronicInformationByTargetInfo electronic in electronicList)
            //    {
            //        electronic.BaseUrl = PLMUsersBusinessLogicComponent.ApplicationUsersBLC.Instance.getApplication(applicationKey).RootUrl +
            //            PLMClientsDataAccessComponent.CountriesDALC.Instance.getOne((int)ClientsBLC.Instance.getCountryByID(country)).CountryName +
            //            "/" + electronic.BaseUrl;
            //    }
            return electronicList;
        }

        #endregion

        public static readonly ElectronicInformationBLC Instance = new ElectronicInformationBLC();

    }
}
