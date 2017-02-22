using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PLMClientsBusinessEntities;

namespace PLMClientsBusinessLogicComponent
{
    public class CountriesBLC
    {

        #region Constructors

        private CountriesBLC() { }

        #endregion

        #region Public Methods

        public List<CountryInfo> getCountries()
        {
            return PLMClientsDataAccessComponent.CountriesDALC.Instance.getAll();
        }

        public List<CountryMobileInfo> getCountries(PLMClientsBusinessEntities.Catalogs.TargetOutputs targetId)
        {
            List<CountryMobileInfo> countriesMobile = PLMClientsDataAccessComponent.CountriesDALC.Instance.getCountriesByTarget((byte)targetId);

            //Obtiene la edición correspondiente al páis:
            this.addEdition(countriesMobile, targetId);

            //Se agrega el menú correspondiente a cada páis:
            this.addMenues(countriesMobile, targetId);

            return countriesMobile; 
        }

        public CountryInfo getCountry(int countryId)
        {
            return PLMClientsDataAccessComponent.CountriesDALC.Instance.getOne(countryId);
        }

        #endregion

        #region Private Methods

        private void addEdition(List<CountryMobileInfo> countriesMobile, PLMClientsBusinessEntities.Catalogs.TargetOutputs targetId)
        {
            foreach (CountryMobileInfo country in countriesMobile)
            {
                MobileEditionInfo edition = PLMClientsBusinessLogicComponent.EditionsBLC.Instance.getByTargetByCountry((byte)targetId, country.ID);

                if (edition != null)
                {
                    country.BaseUrl = PLMUsersBusinessLogicComponent.ApplicationUsersBLC.Instance.getApplication(System.Configuration.ConfigurationManager.AppSettings["hashkey"]).RootUrl +
                        country.CountryName + "/" + country.BaseUrl;

                    country.CSSUrl = PLMUsersBusinessLogicComponent.ApplicationUsersBLC.Instance.getApplication(System.Configuration.ConfigurationManager.AppSettings["hashkey"]).RootUrl +
                        country.CountryName + "/StyleSheet/" + System.Configuration.ConfigurationManager.AppSettings["CSSFile"];

                    country.ContentImagesUrl = PLMUsersBusinessLogicComponent.ApplicationUsersBLC.Instance.getApplication(System.Configuration.ConfigurationManager.AppSettings["hashkey"]).RootUrl.Replace(
                        "Tools/", "") + "PharmaSearchEngine/" + MedinetDataAccessComponent.CountriesDALC.Instance.getOne(MedinetDataAccessComponent.EditionsDALC.Instance.getByISBN(
                        edition.ISBN).CountryId).CountryName + "/" + MedinetDataAccessComponent.BooksDALC.Instance.getOne(MedinetDataAccessComponent.EditionsDALC.Instance.getByISBN(
                        edition.ISBN).BookId).ShortName + MedinetDataAccessComponent.EditionsDALC.Instance.getByISBN(edition.ISBN).NumberEdition + "/img/";

                    country.CountryId = MedinetDataAccessComponent.EditionsDALC.Instance.getByISBN(edition.ISBN).CountryId;
                    country.EditionId = MedinetDataAccessComponent.EditionsDALC.Instance.getByISBN(edition.ISBN).EditionId;
                    country.ISBN = edition.ISBN;
                }
            }
        }

        private void addMenues(List<CountryMobileInfo> countriesMobile, PLMClientsBusinessEntities.Catalogs.TargetOutputs targetId)
        {
            foreach (CountryMobileInfo country in countriesMobile)
                country.Menues = PLMClientsDataAccessComponent.MenuesDALC.Instance.getByTargetByEdition(country.ISBN, (byte)targetId);
        }

        #endregion

        public static readonly CountriesBLC Instance = new CountriesBLC();

    }
}
