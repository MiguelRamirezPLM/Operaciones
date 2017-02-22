using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DEIABusinessLogicComponent
{
    public class CatalogsBLC
    {
        
        #region Constructors

        private CatalogsBLC() { }

        #endregion

        #region Public Methods

        //Retrieves Information From Editions
        public DEIABusinessEntries.EditionInfo rocGetEdition(string code)
        {
            PLMClientsBusinessEntities.ValidCodeInfo validCode = new PLMClientsBusinessEntities.ValidCodeInfo();
            validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

            if (validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
            {
                return DEIADataAccessComponent.CatalogsDALC.Instace.rocGetEdition(); 
            }
            else
            {
                throw new ApplicationException("El código no es válido o se encuentra inactivo.");
            }
        }

        //Retrieves all Phones by Company
        public List<DEIABusinessEntries.ROC.CompanyPhoneInfo> rocGetPhonesByCompany(string code, int companyId)
        {
            if (companyId <= 0)
            {
                throw new ArgumentException("The company does not exist");
            }
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                    return DEIADataAccessComponent.CatalogsDALC.Instace.rocGetPhonesByCompany(companyId);
                }
                else
                {
                    throw new ApplicationException("El código no es válido o se encuentra inactivo.");
                }
            }
        }

        #region States

        //Retrieves all States whith Companies By Sections
        public List<DEIABusinessEntries.StateInfo> rocGetStatesWithCompaniesBySection(string code, int sectionId, int EditionId)
        {
            if (sectionId <= 0 || EditionId <= 0)
            {
                throw new ArgumentException("The section or edition does not exist");
            }
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                    return DEIADataAccessComponent.CatalogsDALC.Instace.rocGetStatesWithCompaniesBySection(sectionId, EditionId);
                }
                else
                {
                    throw new ApplicationException("El código no es válido o se encuentra inactivo.");
                }
            }
        }
        
        //Retrieves all States by StateId
        public DEIABusinessEntries.StateInfo rocGetState(string code, int stateId)
        {
            if (stateId <= 0)
            {
                throw new ArgumentException("The state does not exist");
            }
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                    return DEIADataAccessComponent.CatalogsDALC.Instace.rocGetState(stateId);
                }
                else
                {
                    throw new ApplicationException("El código no es válido o se encuentra inactivo.");
                }
            }
        }

        #endregion

        #region Cities

        //Retrieves all Information from City
        public DEIABusinessEntries.CityInfo rocGetCity(string code, int cityId)
        {
            if (cityId <= 0)
            {
                throw new ArgumentException("The city does not exist");
            }
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                    return DEIADataAccessComponent.CatalogsDALC.Instace.rocGetCity(cityId);
                }
                else
                {
                    throw new ApplicationException("El código no es válido o se encuentra inactivo.");
                }
            }
        }
       
        //Retrieves all Cities With Companies by State
        public List<DEIABusinessEntries.CityInfo> rocGetCitiesWithCompaniesByState(string code, int stateId, int sectionId, int editionId) 
        {
            if (stateId <= 0 || sectionId <= 0 || editionId <= 0)
            {
                throw new ArgumentException("The state, section or edition does not exist");
            }
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                    return DEIADataAccessComponent.CatalogsDALC.Instace.rocGetCitiesWithCompaniesByState(stateId, sectionId, editionId);
                }
                else
                {
                    throw new ApplicationException("El código no es válido o se encuentra inactivo.");
                }
            }
        }

        #endregion

        #region Advertisements

        //Retrieves all Advertisements by CompanyId
        public List<DEIABusinessEntries.AdvertisementInfo> rocGetAdvertisementsByCompanyId(string code, int companyId)
        {

            if (companyId <= 0)
                throw new ArgumentException("The company does not exist");
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {
                    List<DEIABusinessEntries.AdvertisementInfo> advs = DEIADataAccessComponent.CatalogsDALC.Instace.rocGetAdvertisementsByCompanyId(companyId);

                    if (advs != null)
                        foreach (DEIABusinessEntries.AdvertisementInfo adv in advs)
                            adv.AdvFile = System.Configuration.ConfigurationManager.AppSettings["BaseUrl"] + adv.AdvFile;

                    return advs;

                }
                else
                    throw new ApplicationException("El código no es válido o se encuentra inactivo.");
            }

        }

        //Retrieves all Advertisements by SectionId
        public List<DEIABusinessEntries.AdvertisementInfo> rocGetAdvertisementsBySectionId(string code, int sectionId)
        {

            if (sectionId <= 0)
                throw new ArgumentException("The section does not exist");
            else
            {
                PLMClientsBusinessEntities.ValidCodeInfo validCode = new PLMClientsBusinessEntities.ValidCodeInfo();
                validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

                if (validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
                {

                    List<DEIABusinessEntries.AdvertisementInfo> advs = DEIADataAccessComponent.CatalogsDALC.Instace.rocGetAdvertisementsBySectionId(sectionId);

                    if (advs != null)
                        foreach (DEIABusinessEntries.AdvertisementInfo adv in advs)
                            adv.AdvFile = System.Configuration.ConfigurationManager.AppSettings["BaseUrl"] + adv.AdvFile;

                    return advs;

                }
                else
                    throw new ApplicationException("El código no es válido o se encuentra inactivo.");
            }
        }

        #endregion

        #endregion

        public static readonly CatalogsBLC Instance = new CatalogsBLC();
    }
}
