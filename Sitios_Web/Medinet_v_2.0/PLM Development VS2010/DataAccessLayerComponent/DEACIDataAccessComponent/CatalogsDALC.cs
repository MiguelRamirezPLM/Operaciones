using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DEACIBusinessEntries;

namespace DEACIDataAccessComponent
{
    public class CatalogsDALC
    {

        #region Constructor

        private CatalogsDALC() { }

        #endregion

        #region Public Methods

        //Retrieves information from a country by ID
        public CountryInfo rocGetCountry(string id)
        {
            DEACIDataClassesDataContext db = new DEACIDataClassesDataContext();

            var country = from countryRow in db.roc_spGetCountry(id)
                          select new CountryInfo
                          {
                              CountryId = countryRow.CountryId,
                              Name = countryRow.CountryName,
                              Id = countryRow.ID,
                              Active = countryRow.Active
                          };

            List<CountryInfo> countryInformation = country.ToList();

            return countryInformation.Count() > 0 ? countryInformation[0] : null;
        }

        #region States

        //Retrieves all States By Country
        public List<StateInfo> rocGetStates(int countryId)
        {
            DEACIDataClassesDataContext db = new DEACIDataClassesDataContext();

            var state = from stateRow in db.roc_spGetStates(countryId)
                        select new StateInfo
                        {
                            StateId = stateRow.StateId,
                            Name = stateRow.Name,
                            CountryId = stateRow.CountryId,
                            Active = stateRow.Active
                        };

            List<StateInfo> stateInformation = state.ToList();

            return stateInformation.Count() > 0 ? stateInformation : null;
        }

        //Retrieves all States By Country and StateId
        public StateInfo rocGetState(int countryId, int stateId)
        {
            DEACIDataClassesDataContext db = new DEACIDataClassesDataContext();

            var state = from stateRow in db.roc_spGetState(countryId, stateId)
                        select new StateInfo
                        {
                            StateId = stateRow.StateId,
                            Name = stateRow.Name,
                            CountryId = stateRow.CountryId,
                            Active = stateRow.Active
                        };

            List<StateInfo> states = state.ToList();

            return states.Count() > 0 ? states[0] : null;
        }

        #endregion

        #region Cities

        //Retrieves all Cities By State
        public List<CityInfo> rocGetCities(int stateId)
        {
            DEACIDataClassesDataContext db = new DEACIDataClassesDataContext();

            var city = from cityRow in db.roc_spGetCities(stateId)
                       select new CityInfo
                       {
                           CityId = cityRow.CityId,
                           Name = cityRow.Name,
                           StateId = cityRow.StateId,
                           Active = cityRow.Active
                       };

            List<CityInfo> cityInformation = city.ToList();

            return cityInformation.Count() > 0 ? cityInformation : null;

        }

        //Retrieves all Cities By Country
        public DEACIBusinessEntries.ROC.CityByCountryInfo rocGetCity(int countryId, int cityId)
        {
            DEACIDataClassesDataContext db = new DEACIDataClassesDataContext();

            var city = from cityRow in db.roc_spGetCity(countryId, cityId)
                       select new DEACIBusinessEntries.ROC.CityByCountryInfo
                       {
                           CityId = cityRow.CityId,
                           CityName = cityRow.City,
                           StateId = cityRow.StateId,
                           StateName = cityRow.State,
                           CountryName = cityRow.CountryName
                       };

            List<DEACIBusinessEntries.ROC.CityByCountryInfo> cityInformation = city.ToList();

            return cityInformation.Count() > 0 ? cityInformation[0] : null;

        }

        #endregion 

        #region Phones

        //Retrives all Phones By Company
        public List<DEACIBusinessEntries.ROC.CompanyPhoneInfo> rocGetPhones(int companyId)
        {
            DEACIDataClassesDataContext db = new DEACIDataClassesDataContext();

            var phones = from phonesRow in db.roc_spGetPhones(companyId)
                         select new DEACIBusinessEntries.ROC.CompanyPhoneInfo
                         {
                             Description = phonesRow.Description,
                             PhoneValue = phonesRow.PhoneValue
                         };

            List<DEACIBusinessEntries.ROC.CompanyPhoneInfo> phonesinformation = phones.ToList();

            return phonesinformation.Count() > 0 ? phonesinformation : null;

        }

        //Retrieves all Phones By Company and By Type
        public DEACIBusinessEntries.ROC.CompanyPhoneInfo rocGetPhone(int companyId, byte phoneTypeId)
        {
            DEACIDataClassesDataContext db = new DEACIDataClassesDataContext();

            var phone = from phonRow in db.roc_spgetPhone(companyId, phoneTypeId)
                        select new DEACIBusinessEntries.ROC.CompanyPhoneInfo
                        {
                            Description = phonRow.Description,
                            PhoneValue = phonRow.PhoneValue
                        };

            List<DEACIBusinessEntries.ROC.CompanyPhoneInfo> phonInformation = phone.ToList();

            return phonInformation.Count() > 0 ? phonInformation[0] : null;
        }

        #endregion

        //Retrieves all Editions By Country
        public EditionInfo rocGetEdition(int countryId)
        {
            DEACIDataClassesDataContext db = new DEACIDataClassesDataContext();

            var edition = from editonRow in db.roc_spGetEdition(countryId)
                          select new EditionInfo
                          {
                              EditionId = editonRow.EditionId,
                              CountryId = editonRow.CountryId,
                              ParentId = editonRow.ParentId,
                              BookId = editonRow.BookId,
                              NumberEdition = editonRow.NumberEdition,
                              ISBN = editonRow.ISBN,
                              BarCode = editonRow.BarCode,
                              Active = editonRow.Active
                          };
            List<EditionInfo> editionInformation = edition.ToList();

            return editionInformation.Count() > 0 ? editionInformation[0] : null;
        }

        //Retrives all Advertisements By City
        public List<DEACIBusinessEntries.ROC.AdvertisementByCompanyInfo> rocGetAdvertisement(int cityId, int editionId, byte companyTypeId)
        {
            DEACIDataClassesDataContext db = new DEACIDataClassesDataContext();

            var advert = from advertRow in db.roc_spGetAdvertisement(cityId, editionId, companyTypeId)
                          select new DEACIBusinessEntries.ROC.AdvertisementByCompanyInfo
                          {
                              CompanyId = advertRow.CompanyId,
                              CompanyTypeId = advertRow.CompanyTypeId,
                              CompanyIdParent = advertRow.CompanyIdParent,
                              Address = advertRow.Address,
                              Suburb = advertRow.Suburb,
                              Ubication = advertRow.Ubication,
                              PostalCode = advertRow.PostalCode,
                              Email = advertRow.Email,
                              Web = advertRow.Web,
                              Contact = advertRow.Contact,
                              CompanyName = advertRow.CompanyName,
                              CompanyShortName = advertRow.CompanyShortName,
                              CityId = advertRow.CityId,
                              ClientID = advertRow.Client_ID,
                              Active = advertRow.Active,
                              EditionId = advertRow.EditionId,
                              HTMLFile = advertRow.HtmlFile,
                              CountryId = advertRow.CountryId,
                              ParentId = advertRow.ParentId,
                              BookId = advertRow.BookId,
                              NumberEdition = advertRow.NumberEdition,
                              ISBN = advertRow.ISBN,
                              BarCode = advertRow.Barcode

                          };

            List<DEACIBusinessEntries.ROC.AdvertisementByCompanyInfo> advertisementInformation = advert.ToList();

            return advertisementInformation.Count() > 0 ? advertisementInformation : null;



        }

        //Retrieves all Advertisements By Company and Edition
        public List<DEACIBusinessEntries.ROC.AdvertisementByEditionInfo> rocGetAdvertisementsByCompany(int companyId, int editionId)
        {
            DEACIDataClassesDataContext db = new DEACIDataClassesDataContext();

            var adv = from advertisementInfo in db.roc_spGetAdvertisementsByCompany(companyId, editionId)
                      select new DEACIBusinessEntries.ROC.AdvertisementByEditionInfo
                      {
                          AdvId = advertisementInfo.AdvId,
                          HiredSpace = advertisementInfo.HiredSpace,
                          AdvFile = advertisementInfo.AdvFile,
                          CompanyId = advertisementInfo.CompanyId,
                          EditionId = advertisementInfo.EditionId
                      };

            List<DEACIBusinessEntries.ROC.AdvertisementByEditionInfo> advertisements = adv.ToList();

            return advertisements.Count() > 0 ? advertisements : null;

        }

        #endregion

        public static readonly CatalogsDALC Instance = new CatalogsDALC();

    }
}
