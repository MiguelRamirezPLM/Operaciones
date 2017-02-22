using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MedinetBusinessEntries;
using PLMClientsBusinessEntities;
using System.Xml;

namespace OSMobilesBusinessLogicComponent
{
    public class CatalogsBLC
    {
        #region Constructors

        private CatalogsBLC() { }

        #endregion 

        #region Public Methods

        public List<CountryMobileInfo> getCountries(PLMClientsBusinessEntities.Catalogs.OSMobileDevices osMobileId)
        {
            List<CountryMobileInfo> countriesMobile = new List<CountryMobileInfo>();   

            //Get all Countries from Medinet:
            this.addCountries(countriesMobile, OSMobilesDataAccessComponent.MenuesDALC.Instance.getMobileCountries((byte)osMobileId));

            //Obtiene la edición correspondiente al páis:
            this.addEdition(countriesMobile, osMobileId);

            //Se agrega el menú correspondiente a cada páis:
            this.addMenues(countriesMobile, osMobileId);

            return countriesMobile;
        }

        public List<BannerInfo> getBannersByDevice(string isbn, PLMClientsBusinessEntities.Catalogs.OSMobileDevices osMobileId, string applicationKey)
        { 
            if(isbn == null || (byte)osMobileId == 0)
                throw new ArgumentException("The ISBN or Mobile Device ID does not exist.");


            List<BannerInfo> banners = OSMobilesDataAccessComponent.BannersDALC.Instance.getBannerByDevice(isbn, osMobileId);

            if (banners != null)
                foreach (PLMClientsBusinessEntities.BannerInfo banner in banners)
                {
                    banner.BaseUrl = PLMUsersBusinessLogicComponent.ApplicationUsersBLC.Instance.getApplication(applicationKey).RootUrl +
                        MedinetDataAccessComponent.CountriesDALC.Instance.getOne(MedinetDataAccessComponent.EditionsDALC.Instance.getByISBN(isbn).CountryId).CountryName +
                        "/" + banner.BaseUrl;
                }


            return banners;

        }

        public PLMClientsBusinessEntities.ContactInfo getContactByCountry(string id)
        {
            if (id == null || id == "" || id.Length > 3)
                throw new ArgumentException("The Country ID does not exist.");

            return PLMInfoDataAccessComponent.ContactDALC.Instance.getContactInfoByCountry(id);
        }

        public List<PLMClientsBusinessEntities.MobileMenuesInfo> getMenues(string isbn, PLMClientsBusinessEntities.Catalogs.OSMobileDevices osMobileId, string applicationKey)
        {

            List<PLMClientsBusinessEntities.MobileMenuesInfo> menues = OSMobilesDataAccessComponent.MenuesDALC.Instance.getByOSMobileByEdition(isbn, (byte)osMobileId);

            if(menues != null)
                foreach (PLMClientsBusinessEntities.MobileMenuesInfo menu in menues)
                {
                    menu.BaseUrl = PLMUsersBusinessLogicComponent.ApplicationUsersBLC.Instance.getApplication(applicationKey).RootUrl + menu.BaseUrl;
                }

            return menues;
        }

        public MobileEditionInfo getEditionByDevice(PLMClientsBusinessEntities.Catalogs.OSMobileDevices osMobileId, string country)
        {
            return OSMobilesDataAccessComponent.OsMobileEditionsDALC.Instance.getByDeviceByCountry((byte)osMobileId, country);
        }

        public string getTerms()
        {
            return System.Configuration.ConfigurationManager.AppSettings["Terms"];
        }

        #endregion

        #region Private Methods

        private void addCountries(List<CountryMobileInfo> countriesMobile, List<PLMClientsBusinessEntities.CountryInfo> countries)
        {
            foreach (CountryInfo country in countries)
            {
                CountryMobileInfo countryItem = new CountryMobileInfo();

                countryItem.CountryId = Convert.ToByte(country.CountryId);
                countryItem.CountryName = country.CountryName;
                countryItem.ID = country.ID;

                countriesMobile.Add(countryItem);

            }


        }

        private void addEdition(List<CountryMobileInfo> countriesMobile, PLMClientsBusinessEntities.Catalogs.OSMobileDevices osMobileId)
        {
            foreach (CountryMobileInfo country in countriesMobile)
            {
                MobileEditionInfo edition = this.getEditionByDevice(osMobileId, country.ID);

                if (edition != null)
                {
                    country.FileName = edition.FileName;
                    country.BaseUrl = PLMUsersBusinessLogicComponent.ApplicationUsersBLC.Instance.getApplication(System.Configuration.ConfigurationManager.AppSettings["hashkey"]).RootUrl +
                        country.CountryName + "/" + edition.BaseUrl;

                    country.CountryId = MedinetDataAccessComponent.EditionsDALC.Instance.getByISBN(edition.ISBN).CountryId;
                    country.EditionId = MedinetDataAccessComponent.EditionsDALC.Instance.getByISBN(edition.ISBN).EditionId;
                    country.ISBN = edition.ISBN;

                }
            }

        }

        private void addMenues(List<CountryMobileInfo> countriesMobile, PLMClientsBusinessEntities.Catalogs.OSMobileDevices osMobileId)
        {
            foreach (CountryMobileInfo country in countriesMobile)
                country.Menues = OSMobilesDataAccessComponent.MenuesDALC.Instance.getByOSMobileByEdition(country.ISBN, (byte)osMobileId);
        }

        #endregion


        public static readonly CatalogsBLC Instance = new CatalogsBLC();
    }
}
