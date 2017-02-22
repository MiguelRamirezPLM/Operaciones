using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GuiaBusinessLogicComponent
{
    public class CatalogMethods
    {
        #region Constructors

        private CatalogMethods() { }

        #endregion

        #region Public Methods

        #region Countries

        public GuiaBusinessEntries.CountryInfo getCountry(string code, byte countryId)
        {
            return GuiaDataAccessComponent.Queries.Instance.getCountryById(countryId);
        }

        #endregion

        #region States

        public GuiaBusinessEntries.StateInfo getState(string code, int stateId)
        {
            return GuiaDataAccessComponent.Queries.Instance.getStateById(stateId);
        }

        #endregion

        #region Editions

        public GuiaBusinessEntries.EditionInfo getEdition(string code)
        {

            PLMClientsBusinessEntities.ValidCodeInfo validCode = new PLMClientsBusinessEntities.ValidCodeInfo();
            validCode = PLMClientsBusinessLogicComponent.CodesBLC.Instance.validCode(code);

            if (validCode.CodeStatusId == PLMClientsBusinessEntities.Catalogs.CodeStatus.ACTIVO)
            {
                PLMClientsBusinessEntities.EditionInfo edition = PLMClientsBusinessLogicComponent.CatalogsBLC.Instance.getEdition(code);

                if (edition != null)
                {
                    return GuiaDataAccessComponent.Queries.Instance.getEditionByISBN(edition.ISBN);
                }
                else
                    throw new ApplicationException("El código no tiene asociada ninguna edición.");
            }
            else
            {
                throw new ApplicationException("El código no es válido o se encuentra inactivo.");
            }

            
        }

        #endregion

        #region Clients

        public int getNumberOfClients(string code, int editionId, byte clientTypeId)
        {
            return GuiaDataAccessComponent.Queries.Instance.getNumberOfClientsByEditionByType(editionId, clientTypeId);
        }

        public GuiaBusinessEntries.ClientInfo getClient(string code, int clientId)
        {
            return GuiaDataAccessComponent.Queries.Instance.getClientById(clientId);
        }

        public List<GuiaBusinessEntries.ClientTypeInfo> getClientTypes(string code)
        {
            return GuiaDataAccessComponent.Queries.Instance.getClientTypes();
        }

        public List<GuiaBusinessEntries.ClientInfo> getClientsByType(string code, int editionId, byte clientTypeId)
        {
            return GuiaDataAccessComponent.Queries.Instance.getClientsByEditionByType(editionId, clientTypeId);
        }

        public List<GuiaBusinessEntries.ClientInfo> getClientsByLetter(string code, int editionId, byte clientTypeId, string letter)
        {
            return GuiaDataAccessComponent.Queries.Instance.getClientsByName(editionId, clientTypeId, letter);
        }

        public List<GuiaBusinessEntries.ClientInfo> getClientsByText(string code, int editionId, byte clientTypeId, string text)
        {
            return GuiaDataAccessComponent.Queries.Instance.getClientsByText(editionId, clientTypeId, text);
        }

        public List<GuiaBusinessEntries.BrandDetailInfo> getBrandsByClient(string code, int editionId, int clientId)
        {
            return GuiaDataAccessComponent.Queries.Instance.getBrandsByClient(editionId, clientId);
        }

        public List<GuiaBusinessEntries.DrugDetailInfo> getDrugs(string code, int editionId, string text)
        {
            return GuiaDataAccessComponent.Queries.Instance.getDrugs(editionId, text);
        }

        public List<GuiaBusinessEntries.PhoneTypeInfo> getPhoneTypes(string code)
        {
            return GuiaDataAccessComponent.Queries.Instance.getPhoneTypes();
        }

        public List<GuiaBusinessEntries.PhonesByClientInfo> getPhonesByClient(string code, int clientId)
        {
            return GuiaDataAccessComponent.Queries.Instance.getPhonesByClient(clientId);
        }

        #endregion

        #region Drugs

        public List<GuiaBusinessEntries.DrugInfo> getDrugsByClientId(string code, int clientId)
        {
            return GuiaDataAccessComponent.Queries.Instance.getDrugsByClientId(clientId);
        }

        #endregion

        #region ROC

        public List<GuiaBusinessEntries.ClientsByEditionInfo> getClientsByType(string code, int editionId, byte clientTypeId, int numberPage, int page)
        {
            return GuiaDataAccessComponent.Queries.Instance.getClientsByTypeByEdition(editionId, clientTypeId, numberPage, page);
        }

        public List<GuiaBusinessEntries.ClientsByEditionInfo> getClientsByLetter(string code, int editionId, byte clientTypeId, string letter, int numberPage, int page)
        {
            return GuiaDataAccessComponent.Queries.Instance.getClientsByTypeByLetter(editionId, clientTypeId, letter, numberPage, page);
        }

        public List<GuiaBusinessEntries.ClientsByEditionInfo> getClientsByText(string code, int editionId, byte clientTypeId, string text, int numberPage, int page)
        {
            return GuiaDataAccessComponent.Queries.Instance.getClientsByTypeByText(editionId, clientTypeId, text, numberPage, page);
        }


        public List<GuiaBusinessEntries.ProductsByClientInfo> getProductsByClient(string code, int clientId, int? top)
        {
            return GuiaDataAccessComponent.Queries.Instance.getProductsByClient(clientId, top);
        }

        public List<GuiaBusinessEntries.ProductsByEditionInfo> getProductsByText(string code, int editionId, string text, int numberPage, int page)
        {
            return GuiaDataAccessComponent.Queries.Instance.getProductsByText(editionId, text, numberPage, page);
        }

        public List<GuiaBusinessEntries.BrandsByEditionInfo> getBrandsByText(string code, int editionId, string text, int numberPage, int page)
        {
            return GuiaDataAccessComponent.Queries.Instance.getBrandsByText(editionId, text, numberPage, page);
        }


        public List<GuiaBusinessEntries.SpecialityInfo> getSpecialities(string code)
        {
            return GuiaDataAccessComponent.Queries.Instance.getSpecialities();
        }

        public List<GuiaBusinessEntries.ClientsByEditionInfo> getClientsBySpeciality(string code, int editionId, int specialityId, int numberPage, int page)
        {
            return GuiaDataAccessComponent.Queries.Instance.getClientsBySpeciality(editionId, specialityId, numberPage, page);
        }

        public List<GuiaBusinessEntries.ClientsByEditionInfo> getClientsBySpecByStateByText(string code, int editionId, int specialityId, int stateId, int numberPage, int page, string text)
        {
            return GuiaDataAccessComponent.Queries.Instance.getClientsBySpecByStateByText(editionId, specialityId, stateId, text, numberPage, page);
        }

        public List<GuiaBusinessEntries.ClientsByEditionInfo> getClientsBySpecByState(string code, int editionId, int specialityId, int stateId, int numberPage, int page)
        {
            return GuiaDataAccessComponent.Queries.Instance.getClientsBySpecByState(editionId, specialityId, stateId, numberPage, page);
        }

        public List<GuiaBusinessEntries.ClientsByEditionInfo> getClientsBySpecByText(string code, int editionId, int specialityId, int numberPage, int page, string text)
        {
            return GuiaDataAccessComponent.Queries.Instance.getClientsBySpecByText(editionId, specialityId, numberPage, page, text);
        }

        public List<GuiaBusinessEntries.StateInfo> getStatesByCountry(string code, byte countryId)
        {
            return GuiaDataAccessComponent.Queries.Instance.getStatesByCountry(countryId);
        }

        public List<GuiaBusinessEntries.BrandsByEditionInfo> getBrands(string code, int editionId, int numberPage, int page)
        {
            return GuiaDataAccessComponent.Queries.Instance.getBrands(editionId, numberPage, page);
        }

        public List<GuiaBusinessEntries.BrandsByEditionInfo> getBrandsByLetter(string code, int editionId, int numberPage, int page, string letter)
        {
            return GuiaDataAccessComponent.Queries.Instance.getBrandsByLetter(editionId, letter, numberPage, page);
        }

        public List<GuiaBusinessEntries.BrandsByEditionInfo> getBrandsByEditionByText(string code, int editionId, int numberPage, int page, string text)
        {
            return GuiaDataAccessComponent.Queries.Instance.getBrandsByEditionByText(editionId, text, numberPage, page);
        }

        public List<GuiaBusinessEntries.ProductInfo> getProducts(string code, int editionId, int numberPage, int page)
        {
            return GuiaDataAccessComponent.Queries.Instance.getProducts(editionId, numberPage, page);
        }

        public List<GuiaBusinessEntries.ProductInfo> getProductsByLetter(string code, int editionId, int numberPage, int page, string letter)
        {
            return GuiaDataAccessComponent.Queries.Instance.getProductsByLetter(editionId, numberPage, page, letter);
        }

        public List<GuiaBusinessEntries.ProductInfo> getProductsByEditionByText(string code, int editionId, int numberPage, int page, string text)
        {
            return GuiaDataAccessComponent.Queries.Instance.getProductsByEditionByText(editionId, numberPage, page, text);
        }

        public List<GuiaBusinessEntries.ClientByBrandInfo> getClientsByBrand(string code, int editionId, int brandId, int? top)
        {
            return GuiaDataAccessComponent.Queries.Instance.getClientsByBrand(editionId, brandId, top);
        }

        public List<GuiaBusinessEntries.SubProductByProductInfo> getSubProductByProduct(string code, int editionId, int productId, int numberPage, int page)
        {
            return GuiaDataAccessComponent.Queries.Instance.getSubProductsByProduct(editionId, productId, numberPage, page);
        }

        public List<GuiaBusinessEntries.ClientByProductInfo> getClientsWithoutSubProduct(string code, int editionId, int productId, int numberPage, int page)
        {
            return GuiaDataAccessComponent.Queries.Instance.getClientsWithoutSubProduct(editionId, productId, numberPage, page);
        }


        public GuiaBusinessEntries.SpecialityInfo getSpeciality(int specialityId)
        {
            return GuiaDataAccessComponent.Queries.Instance.getSpecialityById(specialityId);
        }

        public GuiaBusinessEntries.ProductInfo getProduct(string code, int editionId, int productId, int numberPage, int page)
        {
            return GuiaDataAccessComponent.Queries.Instance.getProductById(editionId, productId, numberPage, page);
        }

        public GuiaBusinessEntries.BrandsByEditionInfo getBrand(string code, int editionId, int brandId, int numberPage, int page)
        {
            return GuiaDataAccessComponent.Queries.Instance.getBrandById(editionId, brandId, numberPage, page);
        }


        public List<GuiaBusinessEntries.AdvertisementInfo> getAdvertisementsByClient(int clientId)
        {
            return GuiaDataAccessComponent.Queries.Instance.getAdvertByClient(clientId);
        }


        public List<GuiaBusinessEntries.ClientsByEditionInfo> getClientsByFullText(string code, int editionId, byte clientTypeId, string text, int numberPage, int page)
        {
            return GuiaDataAccessComponent.Queries.Instance.getClientsByFullText(editionId, clientTypeId, text, numberPage, page);
        }

        public List<GuiaBusinessEntries.ClientSpecialityInfo> getClientsAndSpecialityByText(string code, int editionId, int numberByPage, int page, string text)
        {
            return GuiaDataAccessComponent.Queries.Instance.getClientsAndSpecialityByText(editionId, numberByPage, page, text);
        }

        public List<GuiaBusinessEntries.ClientSpecialityInfo> getClientsAndSpecialityByFullText(string code, int editionId, int numberByPage, int page, string fullText)
        {
            return GuiaDataAccessComponent.Queries.Instance.getClientsAndSpecialityByFullText(editionId, numberByPage, page, fullText);
        }

        public List<GuiaBusinessEntries.ProductsByEditionInfo> getProductsByFullText(string code, int editionId, string text, int numberPage, int page)
        {
            return GuiaDataAccessComponent.Queries.Instance.getProductsByFullText(editionId, text, numberPage, page);
        }

        public List<GuiaBusinessEntries.BrandsByEditionInfo> getBrandsByFullText(string code, int editionId, string text, int numberPage, int page)
        {
            return GuiaDataAccessComponent.Queries.Instance.getBrandsByFullText(editionId, text, numberPage, page);
        }

        public List<GuiaBusinessEntries.ClientsByEditionInfo> getClientsBySpecByStateByFullText(string code, int editionId, int specialityId, int stateId, string text, int numberPage, int page)
        {
            return GuiaDataAccessComponent.Queries.Instance.getClientsBySpecByStateByFullText(editionId, specialityId, stateId, text, numberPage, page);
        }

        public List<GuiaBusinessEntries.ClientsByEditionInfo> getClientsBySpecByFullText(string code, int editionId, int specialityId, string text, int numberPage, int page)
        {
            return GuiaDataAccessComponent.Queries.Instance.getClientsBySpecByFullText(editionId, specialityId, numberPage, page, text);
        }

        #endregion 

        #region International

        public List<GuiaBusinessEntries.IntClientByEditionInfo> getInternational(string code, int editionId, int numberByPage, int page)
        {
            return GuiaDataAccessComponent.Queries.Instance.getInternational(editionId, numberByPage, page);
        }

        public List<GuiaBusinessEntries.IntClientByEditionInfo> getInternationalByLetter(string code, int editionId, int numberByPage, int page, string letter)
        {
            return GuiaDataAccessComponent.Queries.Instance.getInternationalByLetter(editionId, numberByPage, page, letter);
        }

        public List<GuiaBusinessEntries.IntClientByEditionInfo> getInternationalByText(string code, int editionId, int numberByPage, int page, string text)
        {
            return GuiaDataAccessComponent.Queries.Instance.getInternationalByText(editionId, numberByPage, page, text);
        }

        public List<GuiaBusinessEntries.IntClientByEditionInfo> getInternationalByFullText(string code, int editionId, int numberByPage, int page, string text)
        {
            return GuiaDataAccessComponent.Queries.Instance.getInternationalByFullText(editionId, numberByPage, page, text);
        }

        public GuiaBusinessEntries.IntAddressByClientInfo getAddressByInternationalId(string code, int clientId)
        {
            return GuiaDataAccessComponent.Queries.Instance.getAddressByInternationalId(clientId);
        }

        public List<GuiaBusinessEntries.IntPhonesByClientInfo> getPhonesByInternationalId(string code, int clientId)
        {
            return GuiaDataAccessComponent.Queries.Instance.getPhonesByInternationalId(clientId);
        
        }

        public List<GuiaBusinessEntries.CategoryInfo> getInternationalCategories(string code)
        {
            return GuiaDataAccessComponent.Queries.Instance.getInternationalCategories();
        }

        public List<GuiaBusinessEntries.CategoryInfo> getInternationalCategoriesByParentId(string code, int parentId)
        {
            return GuiaDataAccessComponent.Queries.Instance.getInternationalCategoriesByParentId(parentId);
        }

        public GuiaBusinessEntries.CategoryInfo getInternationalCategoriesByClientId(string code, int clientId)
        {
            return GuiaDataAccessComponent.Queries.Instance.getInternationalCategoriesByClientId(clientId);
        }

        public List<GuiaBusinessEntries.IntProductsByClientInfo> getInternationalProductsByClientAndCategory(string code, int clientId, int categoryId)
        {
            return GuiaDataAccessComponent.Queries.Instance.getInternationalProductsByClientAndCategory(clientId, categoryId);
        }

        public List<GuiaBusinessEntries.CategoryInfo> getInternationalSubcategoriesByClientId(string code, int clientId)
        {
            return GuiaDataAccessComponent.Queries.Instance.getInternationalSubcategoriesByClientId(clientId);
        }

        public List<GuiaBusinessEntries.IntProductsByClientInfo> getInternationalProductsByParentId(string code, int clientId, int categoryId, int productParentId)
        {
            return GuiaDataAccessComponent.Queries.Instance.getInternationalProductsByParentId(clientId, categoryId, productParentId);
        }

        public List<GuiaBusinessEntries.CategoryInfo> getCategoryById(string code, int categoryId)
        {
            return GuiaDataAccessComponent.Queries.Instance.getCategoryById(categoryId);
        }

        public GuiaBusinessEntries.IntClientInfo getInternationalById(string code, int intClientId)
        {
            return GuiaDataAccessComponent.Queries.Instance.getIntClientById(intClientId);
        }

        #endregion




        #endregion

        public static readonly CatalogMethods Instance = new CatalogMethods();
    }
}
