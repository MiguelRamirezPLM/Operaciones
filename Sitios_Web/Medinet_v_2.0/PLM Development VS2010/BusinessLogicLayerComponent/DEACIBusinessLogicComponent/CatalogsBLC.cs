using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DEACIBusinessLogicComponent
{
    public class CatalogsBLC
    {

        #region Constructor

        private CatalogsBLC() { }

        #endregion

        #region Public Methods

        //Retrieves information from a country by ID
        public DEACIBusinessEntries.CountryInfo rocGetCountry(string code, string id)
        {
            PLMClientsBusinessEntities.ValidCodeInfo validCode = new PLMClientsBusinessEntities.ValidCodeInfo();
            validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

            if (validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
            {
                return DEACIDataAccessComponent.CatalogsDALC.Instance.rocGetCountry(id);
            }
            else
            {
                throw new ApplicationException("El código no es válido o se encuentra inactivo.");
            }
        }

        #region States

        //Retrieves all States By Country
        public List<DEACIBusinessEntries.StateInfo> rocGetStates(string code, int countryId)
        {
            if (countryId < 1)
            {
                throw new ArgumentException("The country does not exist.");
            }
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                    return DEACIDataAccessComponent.CatalogsDALC.Instance.rocGetStates(countryId);
                }
                else
                {
                    throw new ApplicationException("El código no es válido o se encuentra inactivo.");
                }
            }
        }

        //Retrieves all States By Country and StateId
        public DEACIBusinessEntries.StateInfo rocGetState(string code, int countryId, int stateId)
        {
            if (countryId < 1 || stateId < 1)
            {
                throw new ArgumentException("The Country or State does not exist.");
            }
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                    return DEACIDataAccessComponent.CatalogsDALC.Instance.rocGetState(countryId, stateId);
                }
                else
                {
                    throw new ApplicationException("El código no es válido o se encuentra inactivo.");
                }
            }
        }

        #endregion

        #region Cities

        //Retrieves all Cities By State
        public List<DEACIBusinessEntries.CityInfo> rocGetCities(string code, int stateId)
        {
            if (stateId < 1)
            {
                throw new ArgumentException("The State does not Exist.");
            }
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                    return DEACIDataAccessComponent.CatalogsDALC.Instance.rocGetCities(stateId);
                }
                else
                {
                    throw new ApplicationException("El código no es válido o se encuentra inactivo.");
                }
            }
        }

        //Retrieves all Cities By Country
        public DEACIBusinessEntries.ROC.CityByCountryInfo rocGetCity(string code, int countryId, int cityId)
        {
            if (countryId < 1 || cityId < 1)
            {
                throw new ArgumentException("The Country or City does not exist.");
            }
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                    return DEACIDataAccessComponent.CatalogsDALC.Instance.rocGetCity(countryId, cityId);
                }
                else
                {
                    throw new ApplicationException("El código no es válido o se encuentra inactivo.");
                }
            }
        }

        #endregion

        #region Phones

        //Retrives all Phones By Company
        public List<DEACIBusinessEntries.ROC.CompanyPhoneInfo> rocGetPhones(string code, int companyId)
        {
            if (companyId <= 0)
            {
                throw new ArgumentException("The company does not exist.");
            }
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                    return DEACIDataAccessComponent.CatalogsDALC.Instance.rocGetPhones(companyId);
                  
                }
                else
                {
                    throw new ApplicationException("El código no es válido o se encuentra inactivo.");
                }
            }
        }

        //Retrieves all Phones By Company and By Type
        public DEACIBusinessEntries.ROC.CompanyPhoneInfo rocGetPhone(string code, int companyId, byte phoneTypeId)
        {
            if (companyId <= 0 || phoneTypeId == 0)
            {
                throw new ArgumentException("The company or companyType does not Exist.");
            }
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                    return DEACIDataAccessComponent.CatalogsDALC.Instance.rocGetPhone(companyId, phoneTypeId);
                     
                }
                else
                {
                    throw new ApplicationException("El código no es válido o se encuentra inactivo.");
                }
            }
        }

        #endregion

        //Retrieves all Editions By Country
        public DEACIBusinessEntries.EditionInfo rocGetEdition(string code, int countryId)
        {
            if (countryId <= 0)
            {
                throw new ArgumentException("The country does not exist.");
            }
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                    return DEACIDataAccessComponent.CatalogsDALC.Instance.rocGetEdition(countryId);

                }
                else
                {
                    throw new ApplicationException("El código no es válido o se encuentra inactivo.");
                }
            }
        }

        //Retrives all Advertisements By City
        public List<DEACIBusinessEntries.ROC.AdvertisementByCompanyInfo> rocGetAdvertisement(string code, int cityId, int editionId, byte companyTypeId)
        {
            if (cityId <= 0 || editionId <=0 || companyTypeId == 0)
            {
                throw new ArgumentException("The city,  edition or companyType does not exist.");
            }
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                    
                    return DEACIDataAccessComponent.CatalogsDALC.Instance.rocGetAdvertisement(cityId, editionId, companyTypeId);
                   
                }
                else
                {
                    throw new ApplicationException("El código no es válido o se encuentra inactivo.");
                }
            }

        }

        //Retrieves all Advertisements By Company and Edition
        public List<DEACIBusinessEntries.ROC.AdvertisementByEditionInfo> rocGetAdvertisementsByCompany(string code, int companyId, int editionId)
        {
            if (companyId < 1 || editionId < 1)
            {
                throw new ArgumentException("The Company or Edition does not exist.");
            }
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                    List<DEACIBusinessEntries.ROC.AdvertisementByEditionInfo> advs = DEACIDataAccessComponent.CatalogsDALC.Instance.rocGetAdvertisementsByCompany(companyId, editionId);

                    if(advs != null)
                        foreach(DEACIBusinessEntries.ROC.AdvertisementByEditionInfo adv in advs)
                             adv.AdvFile = System.Configuration.ConfigurationManager.AppSettings["BaseUrl"] + adv.AdvFile;

                    return advs;
                } 
                else
                {
                    throw new ApplicationException("El código no es válido o se encuentra inactivo.");
                }
            }
        }

        #endregion

        public static readonly CatalogsBLC Instance = new CatalogsBLC();

    }
}
